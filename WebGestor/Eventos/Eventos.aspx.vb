Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Eventos_Eventos
    Inherits System.Web.UI.Page

    Dim obj As New Cls_Eventos
    Dim objSeg As New ModuloSeguridad

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LlenaComboItem("TBOPC562", DdlTipo)
            Call LlenaComboItem("TBOPC006", DdlEvPais) : DdlEvPais.Items.Add("< Seleccionar >") : DdlEvPais.SelectedValue = "< Seleccionar >"
            If DdlEvPais.Items.Count > 0 Then DdlEvPais.SelectedValue = "51" : DdlEvPais_SelectedIndexChanged(sender, e)
        End If
    End Sub
    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            Dim obj As New Cls_Eventos
            GvEventos.DataSource = obj.Lista_Eventos(Session("Ruta_Emp"), Session("CodEmpresa"))
            GvEventos.DataBind()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Limpiar()
        TxtEvCodigo.Text = obj.Ultimo_Evento(Session("Ruta_Emp"))
        Session("EditarEvento") = "No"
        Dim dt As New DataTable
        dt = objSeg.Listar_Usuarios_SinAdm(Ruta_Ng)
        DdlResponsable.DataSource = dt
        DdlResponsable.DataValueField = "USUARI_CODIGO"
        DdlResponsable.DataTextField = "NOMBRES"
        DdlResponsable.DataBind()
        DdlResponsable.Items.Add("< Seleccionar >") : DdlResponsable.SelectedValue = "< Seleccionar >"
        DivEvento.Visible = True
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Limpiar()
        DivEvento.Visible = False
    End Sub

    Private Sub Limpiar()
        TxtEvCodigo.Text = ""
        TxtEvContacto.Text = ""
        TxtEvContactoTelef.Text = ""
        TxtEvDescripcion.Text = ""
        TxtEvNombre.Text = ""
        TxtEvObjetivo.Text = ""
        DdlTipo.SelectedValue = "< Seleccionar >"
        TxtFechaIni.Text = ""
        TxtFechaFin.Text = ""
        TxtHoraIni.Text = ""
        TxtHoraFin.Text = ""
        TxtEvDireccion.Text = ""
        DdlEvDpto.Items.Clear()
        DdlEvProv.Items.Clear()
        DdlEvDist.Items.Clear()
        DdlEvDpto.Enabled = False
        DdlEvProv.Items.Add("< Seleccionar >") : DdlEvProv.SelectedValue = "< Seleccionar >"
        DdlEvProv.Enabled = False
        DdlEvDist.Items.Add("< Seleccionar >") : DdlEvDist.SelectedValue = "< Seleccionar >"
        DdlEvDist.Enabled = False
        If DdlEvPais.SelectedValue = "< Seleccionar >" Then Exit Sub
        If DdlEvPais.SelectedIndex = -1 Or DdlEvPais.Items.Count = 0 Then Exit Sub
        If DdlEvPais.SelectedValue = "51" Then
            Call LlenaComboItem("TBOPC002", DdlEvDpto)
            DdlEvDpto.Items.Add("< Seleccionar >") : DdlEvDpto.SelectedValue = "< Seleccionar >"
            DdlEvDpto.Enabled = True
        End If
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Try
            If DdlTipo.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar tipo de evento');", True)
            ElseIf TxtEvNombre.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el nombre del evento');", True)
            ElseIf TxtEvObjetivo.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el objetivo del evento');", True)
            ElseIf TxtEvDescripcion.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la descripción del evento');", True)
            ElseIf TxtFechaIni.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Fecha que inicia el evento');", True)
            ElseIf TxtFechaFin.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Fecha que termina el evento');", True)
            ElseIf TxtHoraIni.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Hora que inicia el evento');", True)
            ElseIf TxtHoraFin.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Hora que termina el evento');", True)
            ElseIf DdlResponsable.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar al responsable del evento');", True)
            ElseIf TxtEvContacto.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la persona de contacto del evento');", True)
            ElseIf TxtEvDireccion.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la dirección del evento');", True)
            ElseIf DdlEvDpto.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar el departamento del evento');", True)
            ElseIf DdlEvProv.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar la provincia del evento');", True)
            ElseIf DdlEvDist.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar el distrito del evento');", True)
            Else
                Dim psFechaIni As String = ""
                Dim psFechaFin As String = ""
                Dim psHoraIni As String = ""
                Dim psHoraFin As String = ""
                Dim psPais As String = ""
                If DdlEvPais.SelectedValue <> "< Seleccionar >" Then psPais = DdlEvPais.SelectedValue
                Dim psDpto As String = ""
                If DdlEvDpto.SelectedValue <> "< Seleccionar >" Then psDpto = DdlEvDpto.SelectedValue
                Dim psProv As String = ""
                If DdlEvProv.SelectedValue <> "< Seleccionar >" Then psProv = DdlEvProv.SelectedValue
                Dim psDist As String = ""
                If DdlEvDist.SelectedValue <> "< Seleccionar >" Then psDist = DdlEvDist.SelectedValue

                If TxtFechaIni.Text <> "" Then
                    psFechaIni = Left(TxtFechaIni.Text, 4) & Mid(TxtFechaIni.Text, 6, 2) & Right(TxtFechaIni.Text, 2)
                End If
                If TxtFechaFin.Text <> "" Then
                    psFechaFin = Left(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 6, 2) & Right(TxtFechaFin.Text, 2)
                End If
                If TxtHoraIni.Text <> "" Then
                    psHoraIni = Left(TxtHoraIni.Text, 2) & Right(TxtHoraIni.Text, 2)
                End If
                If TxtHoraFin.Text <> "" Then
                    psHoraFin = Left(TxtHoraFin.Text, 2) & Right(TxtHoraFin.Text, 2)
                End If
                If Session("EditarEvento") = "No" Then
                    obj.Insertar_Eventos(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), psFechaIni, psFechaFin, psHoraIni, psHoraFin, DdlTipo.SelectedValue, TxtEvNombre.Text.Trim, TxtEvObjetivo.Text.Trim, TxtEvDescripcion.Text.Trim, TxtEvContacto.Text.Trim, TxtEvContactoTelef.Text.Trim, DdlResponsable.SelectedValue, TxtEvDireccion.Text.Trim, psPais, psDpto, psProv, psDist)
                Else
                    Dim pdCodEvento As Integer = 0
                    pdCodEvento = Nz(TxtEvCodigo.Text)
                    obj.Actualizar_Eventos(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), psFechaIni, psFechaFin, psHoraIni, psHoraFin, DdlTipo.SelectedValue, TxtEvNombre.Text.Trim, TxtEvObjetivo.Text.Trim, TxtEvDescripcion.Text.Trim, TxtEvContacto.Text.Trim, TxtEvContactoTelef.Text.Trim, DdlResponsable.SelectedValue, pdCodEvento, TxtEvDireccion.Text.Trim, psPais, psDpto, psProv, psDist)
                End If
                Limpiar()
                BtnCancelar_Click(sender, e)
                BtnListar_Click(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try

    End Sub

    Protected Sub GvEventos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvEventos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim cn As String = Session("Ruta_Emp")
        Try
            If e.CommandName = "Edita" Then
                Call Limpiar()
                Dim dt As New DataTable
                dt = objSeg.Listar_Usuarios_SinAdm(Ruta_Ng)
                DdlResponsable.DataSource = dt
                DdlResponsable.DataValueField = "USUARI_CODIGO"
                DdlResponsable.DataTextField = "NOMBRES"
                DdlResponsable.DataBind()
                DdlResponsable.Items.Add("< Seleccionar >") : DdlResponsable.SelectedValue = "< Seleccionar >"
                Session("EditarEvento") = "Si"
                DivEvento.Visible = True
                TxtEvCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvEventos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                dt = obj.Lista_XCodEventos(Session("Ruta_Emp"), Session("CodEmpresa"), Nz(TxtEvCodigo.Text))
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        TxtEvContacto.Text = Nu(dr("EVENTO_CONTACTO"))
                        TxtEvContactoTelef.Text = Nu(dr("EVENTO_CONTACTO_TELEFONO"))
                        TxtEvDescripcion.Text = Nu(dr("EVENTO_DESCRIPCION"))
                        TxtEvNombre.Text = Nu(dr("EVENTO_NOMBRE"))
                        TxtEvObjetivo.Text = Nu(dr("EVENTO_OBJETIVO"))
                        TxtFechaFin.Text = Left(Nu(dr("EVENTO_FECHA_FIN")), 4) & "-" & Mid(Nu(dr("EVENTO_FECHA_FIN")), 5, 2) & "-" & Right(Nu(dr("EVENTO_FECHA_FIN")), 2)
                        TxtFechaIni.Text = Left(Nu(dr("EVENTO_FECHA_INI")), 4) & "-" & Mid(Nu(dr("EVENTO_FECHA_INI")), 5, 2) & "-" & Right(Nu(dr("EVENTO_FECHA_INI")), 2)
                        TxtHoraFin.Text = Nu(dr("HORA_TERMINA_EVENTO"))
                        TxtHoraIni.Text = Nu(dr("HORA_INICIA_EVENTO"))
                        TxtEvDireccion.Text = Nu(dr("EVENTO_DIRECCION"))
                        DdlResponsable.SelectedValue = Nu(dr("EVENTO_RESPONSABLE"))
                        DdlTipo.SelectedValue = Nu(dr("EVENTO_TIPO"))
                        TxtEvDireccion.Text = Nu(dr("EVENTO_DIRECCION"))
                        DdlEvPais.SelectedValue = Nu(dr("EVENTO_PAIS"))
                        If DdlEvPais.SelectedValue <> "< Seleccionar >" Then DdlEvPais_SelectedIndexChanged(sender, e)
                        DdlEvDpto.SelectedValue = Nu(dr("EVENTO_DPTO"))
                        If DdlEvDpto.SelectedValue <> "< Seleccionar >" Then DdlEvDpto_SelectedIndexChanged(sender, e)
                        DdlEvProv.SelectedValue = Nu(dr("EVENTO_PROV"))
                        If DdlEvProv.SelectedValue <> "< Seleccionar >" Then DdlEvProv_SelectedIndexChanged(sender, e)
                        DdlEvDist.SelectedValue = Nu(dr("EVENTO_DIST"))
                    Next
                End If

            ElseIf e.CommandName = "Participantes" Then
                Dim pdCodEvento As Integer = 0
                TxtMEventoCodigo.Text = GvEventos.Rows(Index).Cells(2).Text
                TxtMEventoNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvEventos.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtMEvnetoTipo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvEventos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                Dim dt As New DataTable
                pdCodEvento = Nz(TxtMEventoCodigo.Text)
                dt = obj.Lista_XCodEventos(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodEvento)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        TxtMEventoDescripcion.Text = Nu(dr("EVENTO_DESCRIPCION"))
                        TxtMEventoObjetivo.Text = Nu(dr("EVENTO_OBJETIVO"))
                        TxtMEventoFechaTermina.Text = FormatoFecha(Nu(dr("EVENTO_FECHA_FIN")))
                        TxtMEventoFechaInicia.Text = FormatoFecha(Nu(dr("EVENTO_FECHA_INI")))
                        TxtMEventoHoraFin.Text = Nu(dr("HORA_TERMINA_EVENTO"))
                        TxtMEventoHoraInicia.Text = Nu(dr("HORA_INICIA_EVENTO"))
                        TxtMEventoDireccion.Text = Nu(dr("EVENTO_DIRECCION"))
                        If Nu(dr("PDPTO")) <> "" Then TxtMEventoDireccion.Text = TxtMEventoDireccion.Text & ", " & Nu(dr("PDPTO"))
                        If Nu(dr("PPROV")) <> "" Then TxtMEventoDireccion.Text = TxtMEventoDireccion.Text & ", " & Nu(dr("PPROV"))
                        If Nu(dr("PDIST")) <> "" Then TxtMEventoDireccion.Text = TxtMEventoDireccion.Text & ", " & Nu(dr("PDIST"))
                        TxtMEventoContacto.Text = Nu(dr("EVENTO_CONTACTO"))
                        TxtMEventoContactoTelef.Text = Nu(dr("EVENTO_CONTACTO_TELEFONO"))
                    Next
                End If
                dt = Nothing
                dt = objSeg.Listar_Usuarios_SinAdm(Ruta_Ng)
                DdlUsuario.DataSource = dt
                DdlUsuario.DataValueField = "USUARI_CODIGO"
                DdlUsuario.DataTextField = "NOMBRES"
                DdlUsuario.DataBind()
                DdlUsuario.Items.Add("< Seleccionar >") : DdlUsuario.SelectedValue = "< Seleccionar >"
                Call Marcar_Usuario(pdCodEvento)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUsuario').modal('show');", True)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub Marcar_Usuario(ByVal pdCodEvento As Integer)
        Dim dt As New DataTable
        dt = obj.Lista_Participantes_xEventos(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodEvento)
        gvUsuario.DataSource = dt
        gvUsuario.DataBind()
        If dt.Rows.Count = 0 Then
            LblRegParticipantes.Text = "No hay participantes"
        ElseIf dt.Rows.Count = 1 Then
            LblRegParticipantes.Text = "Hay 1 participante"
        ElseIf dt.Rows.Count > 1 Then
            LblRegParticipantes.Text = "Hay " & dt.Rows.Count & " participantes"
        End If
    End Sub

    Private Sub BtnRelacionCerrar_Click(sender As Object, e As EventArgs) Handles BtnRelacionCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUsuario').modal('hide');", True)
    End Sub

    Private Sub BtnMCancelar_Click(sender As Object, e As EventArgs) Handles BtnMCancelar.Click
        DdlUsuario.SelectedValue = "< Seleccionar >"
        'TxtMFechaFin.Text = ""
        TxtMFechaIni.Text = ""
        TxtMHoraFin.Text = ""
        TxtMHoraIni.Text = ""
        DivParticipantes.Visible = False
    End Sub

    Private Sub BtnMGuardar_Click(sender As Object, e As EventArgs) Handles BtnMGuardar.Click

        Try
            If DdlUsuario.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar un participante');", True)
            ElseIf TxtMFechaIni.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Fecha');", True)
            ElseIf TxtMHoraIni.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Hora de entrada del participante');", True)
            ElseIf TxtMHoraFin.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Hora de salida del participante');", True)
            Else
                Dim psUsuarioRepetido As String = "No"
                Dim psFechaPart As String = ""
                For i = 0 To gvUsuario.Rows.Count - 1
                    psFechaPart = Right(gvUsuario.Rows(i).Cells(3).Text.Trim, 4) & "-" & Mid(gvUsuario.Rows(i).Cells(3).Text.Trim, 4, 2) & "-" & Left(gvUsuario.Rows(i).Cells(3).Text.Trim, 2)

                    If gvUsuario.Rows(i).Cells(1).Text = DdlUsuario.SelectedValue And TxtMFechaIni.Text = psFechaPart Then
                        psUsuarioRepetido = "Si"
                        GoTo salir
                    End If
                Next
salir:
                If psUsuarioRepetido = "Si" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El participante y la fecha a ingresar ya se encuentra registrado.');", True)
                Else
                    Dim psFechaIni As String = ""
                    Dim psFechaFin As String = ""
                    Dim psHoraIni As String = ""
                    Dim psHoraFin As String = ""

                    If TxtMFechaIni.Text <> "" Then
                        psFechaIni = Left(TxtMFechaIni.Text, 4) & Mid(TxtMFechaIni.Text, 6, 2) & Right(TxtMFechaIni.Text, 2)
                    End If
                    If TxtMHoraIni.Text <> "" Then
                        psHoraIni = Left(TxtMHoraIni.Text, 2) & Right(TxtMHoraIni.Text, 2)
                    End If
                    If TxtMHoraFin.Text <> "" Then
                        psHoraFin = Left(TxtMHoraFin.Text, 2) & Right(TxtMHoraFin.Text, 2)
                    End If
                    Dim pdCodEvento As Integer = 0
                    pdCodEvento = Nz(TxtMEventoCodigo.Text)
                    obj.Insertar_Participantes_xEventos(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodEvento, DdlUsuario.SelectedValue, psFechaIni, psHoraIni, psHoraFin, Session("User"))

                    Call Marcar_Usuario(pdCodEvento)

                    DdlUsuario.SelectedValue = "< Seleccionar >"
                    TxtMFechaIni.Text = ""
                    TxtMHoraFin.Text = ""
                    TxtMHoraIni.Text = ""

                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnParticipantes_Click(sender As Object, e As EventArgs) Handles BtnParticipantes.Click
        DdlUsuario.SelectedValue = "< Seleccionar >"
        'TxtMFechaFin.Text = ""
        TxtMFechaIni.Text = ""
        TxtMHoraFin.Text = ""
        TxtMHoraIni.Text = ""
        DivParticipantes.Visible = True
    End Sub

    Private Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click
        BtnMCancelar_Click(sender, e)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensaje').modal('hide');", True)
    End Sub

    Private Sub BtnSi_Click(sender As Object, e As EventArgs) Handles BtnSi.Click
        TxtMHoraFin.Text = ""
        TxtMHoraIni.Text = ""
        TxtMFechaIni.Text = ""
        DdlUsuario.SelectedValue = "< Seleccionar >"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensaje').modal('hide');", True)
    End Sub

    Private Sub gvUsuario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUsuario.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable
        Dim pdCodEvento As Integer = 0
        Dim psUsuario As String = ""
        Dim psFecha As String = ""
        Try

            If e.CommandName = "Quitar" Then
                pdCodEvento = Nz(TxtMEventoCodigo.Text)
                psUsuario = (gvUsuario.Rows(Index).Cells(1).Text)
                psFecha = Mid(gvUsuario.Rows(Index).Cells(3).Text, 7, 4) & Mid(gvUsuario.Rows(Index).Cells(3).Text, 4, 2) & Left(gvUsuario.Rows(Index).Cells(3).Text, 2)
                obj.Eliminar_Participantes(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodEvento, psUsuario, psFecha)
                Call Marcar_Usuario(pdCodEvento)
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub DdlEvDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlEvDpto.SelectedIndexChanged
        DdlEvProv.Items.Clear()
        DdlEvDist.Items.Clear()
        DdlEvProv.Enabled = False
        DdlEvDist.Items.Add("< Seleccionar >") : DdlEvDist.SelectedValue = "< Seleccionar >"
        DdlEvDist.Enabled = False
        If DdlEvDpto.SelectedIndex = -1 Or DdlEvDpto.Items.Count = 0 Then Exit Sub
        If DdlEvDpto.Items(DdlEvDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", DdlEvProv, Left(DdlEvDpto.SelectedValue, 2), "PR")
        DdlEvProv.Items.Add("< Seleccionar >") : DdlEvProv.SelectedValue = "< Seleccionar >"
        If DdlEvDpto.SelectedValue <> "< Seleccionar >" Then DdlEvProv.Enabled = True
    End Sub

    Private Sub DdlEvProv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlEvProv.SelectedIndexChanged
        DdlEvDist.Items.Clear()
        DdlEvDist.Enabled = False
        DdlEvDist.Items.Add("< Seleccionar >") : DdlEvDist.SelectedValue = "< Seleccionar >"
        If DdlEvProv.SelectedIndex = -1 Or DdlEvProv.Items.Count = 0 Then Exit Sub
        If DdlEvProv.Items(DdlEvProv.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", DdlEvDist, Left(DdlEvDpto.SelectedValue, 2) + Mid(DdlEvProv.SelectedValue, 3, 2), "DS")
        DdlEvDist.Items.Add("< Seleccionar >") : DdlEvDist.SelectedValue = "< Seleccionar >"
        If DdlEvProv.SelectedValue <> "< Seleccionar >" Then DdlEvDist.Enabled = True
    End Sub

    Private Sub DdlEvPais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlEvPais.SelectedIndexChanged
        DdlEvDpto.Items.Clear()
        DdlEvProv.Items.Clear()
        DdlEvDist.Items.Clear()
        DdlEvDpto.Enabled = False
        DdlEvProv.Items.Add("< Seleccionar >") : DdlEvProv.SelectedValue = "< Seleccionar >"
        DdlEvProv.Enabled = False
        DdlEvDist.Items.Add("< Seleccionar >") : DdlEvDist.SelectedValue = "< Seleccionar >"
        DdlEvDist.Enabled = False
        If DdlEvPais.SelectedValue = "< Seleccionar >" Then Exit Sub
        If DdlEvPais.SelectedIndex = -1 Or DdlEvPais.Items.Count = 0 Then Exit Sub
        If DdlEvPais.SelectedValue = "51" Then
            Call LlenaComboItem("TBOPC002", DdlEvDpto)
            DdlEvDpto.Items.Add("< Seleccionar >") : DdlEvDpto.SelectedValue = "< Seleccionar >"
            DdlEvDpto.Enabled = True
        End If
    End Sub
End Class
