Imports System.Data
Imports WebGestor

Partial Class CallCenter_CallCenter_Pantalla_Operador
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            TxtUsuarioLLE.InnerText = Session("User")
            TxtHoraInicioLLE.InnerText = DateTime.Now.ToString("hh:mm")

            Session("Llamada") = Convert.ToString(Request.QueryString("WpkDi"))
            Dim codPersona As String = Convert.ToString(Request.QueryString("Lpoeh58FJIJS0lk"))
            Dim codCliente As String = Convert.ToString(Request.QueryString("Ni830dHuciPLO"))
            Dim nroTicket As String = Convert.ToString(Request.QueryString("KJoi09jdJA90dIW"))
            If codPersona <> "" Then
                If nroTicket = "" Then
                    Session("Redireccion") = "CLIENTE"
                Else
                    Session("Redireccion") = "TICKET"
                    TxtNroTicket.InnerText = nroTicket
                End If
                Dim obj As New Cls_Cliente
                Dim obj1 As New Cls_Pantalla_Operador
                Dim dt As New DataTable
                Dim dbRow As DataRow
                TxtCodPersonaLLE.Value = codPersona
                dt = obj.Lista_Datos_Clientes(Session("Ruta_emp"), codPersona, "")
                If dt.Rows.Count > 0 Then
                    dbRow = dt.Rows(0)
                    TxtDireccionLLE.Value = Nu(dbRow("PERSONA_DIRECCION"))
                    TxtCodInternoLLE.Value = CStr("000" + obj1.codigoInterno(Session("Ruta_emp")))
                    Dim c As Integer = TxtCodInternoLLE.Value.Length()
                    TxtCodInternoLLE.Value = TxtCodInternoLLE.Value.Substring(c - 4, 4)
                    TxtCodClienteLLE.InnerText = codCliente
                    If Session("Llamada") = "LLE" Then
                        TituloLlamada.Text = "Llamada Entrante"
                    ElseIf Session("Llamada") = "LLS" Then
                        TituloLlamada.Text = "Llamada Por Teléfono"
                    End If
                    dt = obj1.Listar_Llamadas_Anteriores(Session("Ruta_emp"), codPersona)
                    GvLlamadasAnterioresLLE.DataSource = dt
                    GvLlamadasAnterioresLLE.DataBind()
                End If
            End If
            If Session("HoraInicio") Is Nothing Then Session("HoraInicio") = DateTime.Now.ToString("hh:mm")
            If Session("HoraInicio") IsNot Nothing Then TxtHoraInicioLLE.InnerText = Session("HoraInicio")
            Llenar_Combos()
        End If
    End Sub

    '---------------------------- LLENAR COMBOS ----------------------------'
    Sub Llenar_Combos()
        Dim obj2 As New Cls_Cliente
        Dim dt As New DataTable
        Dim cliente As String = TxtCodPersonaLLE.Value.Trim.ToString()

        Call LlenaComboItem("TBOPC520", DdlAccionesLLE, "")

        dt = obj2.Lista_Contacto_Personas(Session("Ruta_emp"), cliente, "%")
        DdlContactoLLE.DataSource = dt
        DdlContactoLLE.DataValueField = "CONTACTO_CODIGO"
        DdlContactoLLE.DataTextField = "NOMBRES"
        DdlContactoLLE.DataBind()
        If DdlContactoLLE.Items.Count > 0 Then
            DdlContactoLLE.Items.Add("< Seleccionar >")
            DdlContactoLLE.SelectedValue = "< Seleccionar >"
        End If

        Call LlenaComboItem("TBOPC473", DdlProcesoLLE, "")
    End Sub

    Private Sub DdlTipoContactoLLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoContactoLLE.SelectedIndexChanged
        Dim obj As New Cls_Pantalla_Operador
        Dim dt As New DataTable
        Dim tipoContacto As String = DdlTipoContactoLLE.SelectedValue

        dt = obj.Llenar_Combo_Respuesta(Session("Ruta_emp"), tipoContacto)
        If dt.Rows.Count() > 0 Then
            DdlRespuestaLLE.DataSource = dt
            DdlRespuestaLLE.DataValueField = "COD_RESPUESTA"
            DdlRespuestaLLE.DataTextField = "RESPUESTA_DETALLE"
            DdlRespuestaLLE.DataBind()
            DdlRespuestaLLE.Items.Add("< Seleccionar >")
            DdlRespuestaLLE.SelectedValue = "< Seleccionar >"
        Else
            DdlRespuestaLLE.Items.Clear()
        End If
    End Sub

    Private Sub DdlProcesoLLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProcesoLLE.SelectedIndexChanged
        Dim obj As New ClsGtp_Procesos
        obj.LLenaComboItemTabEspRelacionProceso(Session("Ruta_emp"), DdlTipoPeticionLLE, "", "", "TBESP_GTP1", DdlProcesoLLE.SelectedValue, "0001", 1)
        Dim contador As Integer = DdlTipoPeticionLLE.Items.Count()
        If contador > 0 Then
            DdlTipoPeticionLLE.Items.Add("< Seleccionar >")
            DdlTipoPeticionLLE.SelectedValue = "< Seleccionar >"
        Else
            DdlTipoPeticionLLE.Items.Clear()
        End If
        DdlElemento1LLE.Items.Clear()
        DdlElemento2LLE.Items.Clear()
    End Sub

    Private Sub DdlTipoPeticionLLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoPeticionLLE.SelectedIndexChanged
        Call LLenaComboItemTabEsp(DdlElemento1LLE, DdlTipoPeticionLLE.SelectedValue, "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 2, "0001", Session("Ruta_emp"))
        Dim contador As Integer = DdlElemento1LLE.Items.Count()
        If contador = 0 Then
            DdlElemento1LLE.Items.Clear()
        End If
        DdlElemento2LLE.Items.Clear()
    End Sub

    Private Sub DdlElemento1LLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlElemento1LLE.SelectedIndexChanged
        Call LLenaComboItemTabEsp(DdlElemento2LLE, DdlTipoPeticionLLE.SelectedValue, DdlElemento1LLE.SelectedValue, "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 3, "0001", Session("Ruta_emp"))
        Dim contador As Integer = DdlElemento2LLE.Items.Count()
        If contador = 0 Then
            DdlElemento2LLE.Items.Clear()
        End If
    End Sub

    Private Sub DdlContactoLLE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlContactoLLE.SelectedIndexChanged
        Dim obj2 As New Cls_Cliente
        Dim dt As New DataTable
        Dim cliente As String = TxtCodPersonaLLE.Value.Trim.ToString()

        dt = obj2.Lista_Contacto_Personas(Session("Ruta_emp"), cliente, DdlContactoLLE.SelectedValue)
        If dt.Rows.Count > 0 Then
            Dim dbRow As DataRow = dt.Rows(0)
            TxtEmailLLE.Value = dbRow("CONTACTO_EMAIL")
            If Nu(dbRow("CONTACTO_CEL1")) <> "" Then Celular1.Text = dbRow("CONTACTO_CEL1") : Celular1.Visible = True
            If Nu(dbRow("CONTACTO_CEL2")) <> "" Then Celular2.Text = dbRow("CONTACTO_CEL2") : Celular2.Visible = True
            If Nu(dbRow("CONTACTO_TELEF1")) <> "" Then Telefono1.Text = dbRow("CONTACTO_TELEF1") : Telefono1.Visible = True
            If Nu(dbRow("CONTACTO_TELEF2")) <> "" Then Telefono2.Text = dbRow("CONTACTO_TELEF2") : Telefono2.Visible = True
            If Nu(dbRow("CONTACTO_TELEF3")) <> "" Then Telefono3.Text = dbRow("CONTACTO_TELEF3") : Telefono3.Visible = True
        Else
            Celular1.Text = "" : Celular1.Visible = False
            Celular2.Text = "" : Celular2.Visible = False
            Telefono1.Text = "" : Telefono1.Visible = False
            Telefono2.Text = "" : Telefono2.Visible = False
            Telefono3.Text = "" : Telefono3.Visible = False
            TxtEmailLLE.Value = ""
        End If
    End Sub

    '---------------------------- LLENAR COMBOS ----------------------------'

    Private Sub BtnGuardarLLE_Click(sender As Object, e As EventArgs) Handles BtnGuardarLLE.Click
        Dim obj As New Cls_Pantalla_Operador
        Dim obj1 As New Cls_Cliente
        Dim dt As New DataTable
        Dim dbRow As DataRow
        Dim codCentral As String = TxtCodPersonaLLE.Value.ToString()
        Dim codPersona As String = TxtCodPersonaLLE.Value.ToString()
        Dim codCliente As String = TxtCodClienteLLE.InnerText.ToString()
        Dim fecha As String = FechaActual().ToString()
        Dim hora As String = HoraActual().ToString()
        Dim operador As String = "1"
        Dim fecCarga As String = FechaActual().ToString()
        Dim fecActualizar As String = FechaActual().ToString()
        Dim fecLlamada As String = FechaActual().ToString()
        Dim horaLlamada As String = HoraActual().ToString()
        Dim estCartera As String = "1"
        Dim estado As String = "1"
        Dim estAtencion As String = "0"
        Dim estOperacion As String = "0"
        Dim vecesLlamada As String = "0"
        Dim fecProceso As String = FechaActual().ToString()
        Dim tipoPersona As String = DdlTipoContactoLLE.SelectedValue.ToString()
        Dim codPostal As String = ""
        Dim codEstado As String = "1"
        Dim procServidor As String = "0"
        Dim tipoNegocio As String = "1"
        Dim estProceso As String = "1"
        Dim persContacto As String = DdlContactoLLE.SelectedValue.ToString()
        Dim fecCompromiso As String = FechaActual().ToString()
        Dim horaAllamar As String = HoraActual().ToString()
        Dim fecAllamar As String = FechaActual().ToString()
        Dim telfQllamar As String = ""
        Dim nombrePersona As String = DdlContactoLLE.SelectedItem.ToString()
        Dim observacion As String = TxtObservacionLLE.Value.ToString()
        Dim horaInicio As String = Session("HoraInicio").ToString().Substring(0, 2) + Session("HoraInicio").ToString().Substring(3, 2)
        Dim horafin As String = DateTime.Now.ToString("hhmm")
        Dim codRespuesta As String = DdlRespuestaLLE.SelectedValue.ToString()
        Dim telfAyuda As String = ""
        Dim telfNoExiste As String = TxtTelefonoNoExisteLLE.Value.ToString()
        Dim telfNuevo As String = TxtTelefonoNuevoLLE.Value.ToString()
        Dim accion As String = ""

        Dim numeroLlamar As String = ""

        If Celular1.Checked = True Then
            numeroLlamar = Celular1.Text
        ElseIf Celular2.Checked = True Then
            numeroLlamar = Celular2.Text
        ElseIf Telefono1.Checked = True Then
            numeroLlamar = Telefono1.Text
        ElseIf Telefono2.Checked = True Then
            numeroLlamar = Telefono2.Text
        ElseIf Telefono3.Checked = True Then
            numeroLlamar = Telefono3.Text
        End If

        If Session("Llamada") = "LLE" Then
            accion = "15"
        ElseIf Session("Llamada") = "LLS" Then
            accion = "7"
        End If

        dt = obj1.Lista_Datos_Clientes(Session("Ruta_emp"), codPersona, "")
        dbRow = dt.Rows(0)
        Dim ruc As String = Nu(dbRow("PERSONA_RUC"))
        Dim razSocial As String = Nu(dbRow("PERSONA_RAZON_SOCIAL"))
        Dim direccion As String = Nu(dbRow("PERSONA_DIRECCION"))
        Dim telefono As String = Nu(dbRow("PERSONA_TELF1"))
        Dim codDpto As String = Nu(dbRow("PERSONA_DPTO"))
        Dim codProvincia As String = Nu(dbRow("PERSONA_PROV"))
        Dim codDistrito As String = Nu(dbRow("PERSONA_DIST"))
        Dim ubigeo As String = Left(Nu(dbRow("PERSONA_DPTO")), 2) + Mid(Nu(dbRow("PERSONA_PROV")), 3, 2) + Right(Nu(dbRow("PERSONA_DIST")), 2)
        Dim callFono1 As String = Nu(dbRow("PERSONA_TELF2"))
        Dim callFono2 As String = Nu(dbRow("PERSONA_TELF_OF"))
        Dim callFono3 As String = Nu(dbRow("PERSONA_TELF_CELULAR"))
        Dim callFono4 As String = ""
        Dim callFono5 As String = ""
        Dim nroTicket As String = TxtNroTicket.InnerText.ToString()
        Dim nomAccion As String = Session("Redireccion")
        Dim correo As String = TxtEmailLLE.Value.ToString()

        If persContacto = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Contacto');", True)
        ElseIf tipoPersona = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Tipo de Persona');", True)
        ElseIf codRespuesta = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Respuesta');", True)
        ElseIf observacion = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Obsercación');", True)
        ElseIf ChkTelefonoNuevoLLE.Checked = False And ChkTelefonoNoExisteLLE.Checked = False And ChkClienteInubicableLLE.Checked = False Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar un tipo de Teléfono');", True)
        Else
            If ChkTelefonoNuevoLLE.Checked = True Then
                telfAyuda = "TN"
            ElseIf ChkTelefonoNoExisteLLE.Checked = True Then
                telfAyuda = "TNE"
            ElseIf ChkClienteInubicableLLE.Checked = True Then
                telfAyuda = "CI"
            End If

            If TxtTelefonoNuevoLLE.Value = "" And ChkTelefonoNuevoLLE.Checked = True Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Teléfono Nuevo');", True)
            ElseIf TxtTelefonoNoExisteLLE.Value = "" And ChkTelefonoNoExisteLLE.Checked = True Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Teléfono No Existente');", True)
            ElseIf (TxtTelefonoNuevoLLE.Value.Length() <> 9 Or TxtTelefonoNuevoLLE.Value.Length() <> 7) And ChkTelefonoNuevoLLE.Checked = True Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Teléfono Nuevo debe ser de 7 o 9 dígitos');", True)
            ElseIf (TxtTelefonoNoExisteLLE.Value.Length() <> 9 Or TxtTelefonoNoExisteLLE.Value.Length() <> 7) And ChkTelefonoNuevoLLE.Checked = True Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Teléfono No Existente debe ser de 7 o 9 dígitos');", True)
            Else
                Try
                    If TxtTelefonoNuevoLLE.Value <> "" Then Convert.ToInt64(TxtTelefonoNuevoLLE.Value)
                    If TxtTelefonoNoExisteLLE.Value <> "" Then Convert.ToInt64(TxtTelefonoNoExisteLLE.Value)
                    dt = obj.Registrar_Llamada(Session("Ruta_emp"), codCentral, fecha, hora, Session("User"), operador, fecCarga, fecActualizar,
                                                fecLlamada, horaLlamada, estCartera, estado, estAtencion, estOperacion, vecesLlamada, fecProceso, tipoPersona,
                                                ruc, razSocial, direccion, telefono, codPostal, codEstado, codDpto, codProvincia, codDistrito, ubigeo,
                                                callFono1, callFono2, callFono3, callFono4, callFono5, procServidor, tipoNegocio, estProceso, persContacto,
                                                fecCompromiso, fecAllamar, horaAllamar, telfQllamar, nombrePersona, observacion, horaInicio, horafin, codRespuesta,
                                                telfAyuda, telfNoExiste, telfNuevo, accion, codCliente, nroTicket, nomAccion, correo)

                    dbRow = dt.Rows(0)
                    If dbRow(0).ToString() = "1" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresado Correctamente');", True)
                    ElseIf dbRow(0).ToString() = "2" Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El registro ya existe');", True)
                    End If
                    dt = obj.Listar_Llamadas_Anteriores(Session("Ruta_emp"), codPersona)
                    GvLlamadasAnterioresLLE.DataSource = dt
                    GvLlamadasAnterioresLLE.DataBind()
                Catch ex As Exception
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Teléfono debe ser un número');", True)
                End Try
            End If
        End If
    End Sub
End Class
