Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.IO

Public Class CRM_Enviar_Email
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim DbRow As DataRow
            Dim DbRowBusc As DataRow
            Dim dt As DataTable
            Dim dtBusc As DataTable
            Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
            Dim Elemento As String = Convert.ToString(Request.QueryString("OfnoiafRFS"))
            Dim FechaReporta As String = Convert.ToString(Request.QueryString("ASIFasfajsAS"))
            Dim HoraReporta As String = Convert.ToString(Request.QueryString("ioanfmOAISN"))
            Dim CodCliente As String = Convert.ToString(Request.QueryString("aDFWQASRAF"))
            Dim obj As New Cls_Relacion_Ticket
            Dim obj1 As New Cls_Enviar_Email
            Dim psconexion As String = Session("Ruta_Emp")
            dt = obj.Buscar_Campos_Ticket(psconexion, NroTicket)
            dtBusc = obj1.Buscar_Campos_Cliente(psconexion, CodCliente)
            DbRow = dt.Rows(0)
            DbRowBusc = dtBusc.Rows(0)
            If CodCliente <> "" Then
                LblCodCliente.Text = DbRowBusc("TBTICKET_CLIENTE_CODPERSONA")
            End If

            If NroTicket <> "" Then
                TxtAsunto.Text = "Ticket N° " + NroTicket + " : " + DbRow("TBTICKET_CLIENTE_NOMBRE") + " - " + Elemento

                LblUsuario.Text = DbRow("TICKET_ASIGNADO_PERSONA")
                txtNroTicket.Text = NroTicket
                TxtCliente.Value = DbRow("TBTICKET_CLIENTE_CIF") + " - " + DbRow("TBTICKET_CLIENTE_NOMBRE")

                TxtMensaje.Value = " " + vbCrLf + "------------------------------------------------------------------" + vbCrLf +
                    "Ticket N° " + NroTicket + vbCrLf + "Fecha y Hora Reporta : " +
                    FechaReporta + " - " + HoraReporta + vbCrLf +
                    "Proveedor : " + DbRow("TBTICKET_CLIENTE_CIF") + " - " + DbRow("TBTICKET_CLIENTE_NOMBRE") + vbCrLf +
                    "Contacto : " + DbRow("TBTICKET_CONTACTO_APEPAT") + " " + DbRow("TBTICKET_CONTACTO_APEMAT") + " " +
                    DbRow("TBTICKET_CONTACTO_NOMBRES") + vbCrLf + "Proceso del Ticket : " +
                    DbRow("PROCESO") + vbCrLf + "Canal de Ticket : " + DbRow("CANAL") + vbCrLf +
                    "Tipo de Ticket : " + DbRow("NOM_PROB_ORIG") + vbCrLf +
                    "Concepto del Ticket : " + DbRow("NOM_PROB1") + ", " + DbRow("NOM_PROB2") + vbCrLf +
                    "Motivo del Ticket : " + DbRow("TICKET_MOTIVO") + vbCrLf +
                    "Descripción del Ticket : " + DbRow("TICKET_DESCRIPCION") + vbCrLf +
                    "Solución del Ticket : " + DbRow("TICKET_SOLUCION") + vbCrLf +
                    "Estado : " + DbRow("PESTADO")
            End If
            Llenar_Combo_Informacion()
            Llenar_Combo_Tipo_Correo()
            Buscar_Ultimo_Envio()
            Buscar_Numero_Envios()
        End If
    End Sub

    Private Sub BtnEnviarCorreo_Click(sender As Object, e As EventArgs) Handles BtnEnviarCorreo.Click
        Dim obj As New Cls_Enviar_Email
        Dim Saludo As String = TxtSaludo.Text.ToString
        Dim Mensaje As String = TxtMensaje.Value.ToString
        Dim Despedida As String = TxtDespedida.Value.ToString
        Dim Firma As String = TxtFirma.Value.ToString
        Dim Asunto As String = TxtAsunto.Text.ToString
        Dim Destinatario As String = TxtPara.Text.ToString
        Dim psconexion As String = Session("Ruta_Emp")
        Dim CodRelacion As String = LblCodRelacion.Text.ToString
        Dim CodAplicacion As String = LblCodAplicacion.Text.ToString
        Dim NroTicket As String = txtNroTicket.Text.ToString
        Dim Fecha As String = TxtUltimoEnvio.Text.ToString
        Dim Hora As String = LblHoraEnvio.Text.ToString
        Dim NroVez As String = TxtNroEnvios.Text.ToString
        Dim CodCliente As String = LblCodCliente.Text.ToString


        'Dim ad() As String
        'ad = Adjunto.Split("\")
        'Dim c As Integer = ad.Count()
        'Adjunto = ad(0)
        'For index = 1 To c - 1
        '    If Not (index > (c - 5) And index < (c - 1)) Then
        '        Adjunto += "\" + ad(index)
        '    End If
        'Next


        Dim NVez = NroVez + 1

        Dim dt As New DataTable
        Dim DbRow As DataRow
        dt.Columns.Add("c1")
        dt = obj.Listar_Cuerpo_Email(Session("Ruta_emp"))
        DbRow = dt.Rows(0)
        Dim TipoCorreo As String = DbRow("CORREO_TIPO")


        Dim obj1 As New Cls_Relacion_Ticket
        Dim dtE As New DataTable
        Dim DbRowE As DataRow
        dtE.Columns.Add("c1")
        dtE = obj1.Buscar_Campos_Ticket(psconexion, NroTicket)
        DbRowE = dtE.Rows(0)
        Dim Estado As String = DbRowE("PESTADO")

        Dim anio As String = Fecha.Substring(0, 4)
        Dim mes As String = Fecha.Substring(5, 2)
        Dim dia As String = Fecha.Substring(8, 2)
        Fecha = anio + mes + dia
        Dim h As String = Hora.Substring(0, 2)
        Dim m As String = Hora.Substring(3, 2)
        Hora = h + m

        Dim CorreoSysCre = Fecha + Hora + Session("User")

        obj.Ayuda_Relacion_Datos(psconexion, CodRelacion, CodAplicacion)

        If Destinatario.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Ingrese por lo menos un destinatario');", True)
        ElseIf Asunto.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Ingresar Asunto');", True)
        ElseIf Saludo.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Ingresar Saludo');", True)
        ElseIf Mensaje.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Ingresar Mensaje');", True)
        ElseIf Despedida.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Ingresar Despedida');", True)
        ElseIf Firma.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Ingresar Firma');", True)
        Else
            Dim archivo As HttpPostedFile
            Dim Adjunto() As String = Nothing
            Dim Imagen As String = ""
            Dim ImagenFirma As String = ""

            If GvArchivosAdjuntos.Rows.Count > 0 Then
                Dim archivosAdjuntos As String = ""
                For index = 0 To GvArchivosAdjuntos.Rows.Count - 1
                    If index = 0 Then
                        archivosAdjuntos = Path.Combine(Path.GetTempPath(), GvArchivosAdjuntos.Rows(index).Cells(0).Text)
                    Else
                        archivosAdjuntos += ";" + Path.Combine(Path.GetTempPath(), GvArchivosAdjuntos.Rows(index).Cells(0).Text)
                    End If
                Next
                Adjunto = archivosAdjuntos.Split(";")
            End If

            If UploadImagen.HasFile Then
                archivo = UploadImagen.PostedFile
                If File.Exists(Path.Combine(Path.GetTempPath(), archivo.FileName)) = True Then
                    Imagen = Path.Combine(Path.GetTempPath(), archivo.FileName)
                Else
                    archivo.SaveAs(Path.Combine(Path.GetTempPath(), archivo.FileName))
                    Imagen = Path.Combine(Path.GetTempPath(), archivo.FileName)
                End If
            End If

            If UploadFirmaImagen.HasFile Then
                archivo = UploadFirmaImagen.PostedFile
                If File.Exists(Path.Combine(Path.GetTempPath(), archivo.FileName)) = True Then
                    ImagenFirma = Path.Combine(Path.GetTempPath(), archivo.FileName)
                Else
                    archivo.SaveAs(Path.Combine(Path.GetTempPath(), archivo.FileName))
                    ImagenFirma = Path.Combine(Path.GetTempPath(), archivo.FileName)
                End If
            End If

            Dim enviar As String = obj.EnviarCorreo(psconexion, Saludo, Mensaje, Imagen, Despedida, Firma, ImagenFirma, Asunto, Destinatario, Adjunto)

            If enviar = "1" Then
                'obj.Insertar_Tabla_Correos(psconexion, NroTicket, Destinatario, Fecha, Hora, Estado, NVez, TipoCorreo, Mensaje, "")
                'obj.Insertar_Traking_Accion(psconexion, NroTicket, Fecha, Hora, Session("User"))
                'obj.Insertar_Equipo_Lista_Correo(psconexion, CodCliente, Fecha, Hora, Session("User"), Destinatario, "", Asunto, Asunto, NVez, Fecha + Hora + Session("User"))
                Response.Redirect("CRM_Relacion_Ticket.aspx")
            Else
                ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Error al Enviar Email');", True)
            End If
        End If
    End Sub

    Public Sub Agregar()
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim i As Long = 0
        Dim a As Long = 0
        Dim Ruta As String = Path.GetDirectoryName(FileUploadAdjuntar.PostedFile.FileName)
        dtListado.Columns.Add("c1")
        If GvArchivosAdjuntos.Rows.Count > 0 Then
            For i = 0 To GvArchivosAdjuntos.Rows.Count - 1
                a = a + 1
                drT = dtListado.NewRow()
                drT("c1") = GvArchivosAdjuntos.Rows(i).Cells(0)
                dtListado.Rows.Add(drT)
            Next
        End If
        drT = dtListado.NewRow()
        drT("c1") = Ruta
        dtListado.Rows.Add(drT)
        GvArchivosAdjuntos.DataSource = dtListado
        GvArchivosAdjuntos.DataBind()
    End Sub

    Protected Sub Llenar_Combo_Informacion()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Informacion(psconexion)
        DdlInformacion.DataSource = dt
        DdlInformacion.DataValueField = "ELEMEN_CODIGO"
        DdlInformacion.DataTextField = "ELEMEN_VALOR"
        DdlInformacion.DataBind()
        DdlInformacion.Items.Add("< Seleccionar >")
        DdlInformacion.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub DdlInformacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInformacion.SelectedIndexChanged
        If DdlInformacion.SelectedValue = "< Seleccionar >" Then
            GvListaInformacion.DataSource = Nothing
            GvListaInformacion.DataBind()
        ElseIf DdlInformacion.SelectedValue = 1 Then
            Listar_Empleados()
        ElseIf DdlInformacion.SelectedValue = 2 Then
            Listar_Contactos()
        ElseIf DdlInformacion.SelectedValue = 3 Then
            Listar_Documentos()
        ElseIf DdlInformacion.SelectedValue = 4 Then
            Listar_Cuerpo_Email()

        End If
        TablaListaInformacion.Style.Add("height", "330px")
        TablaListaInformacion.Style.Add("width", "500px")
        TablaListaInformacion.Style.Add("overflow", "auto")
        TablaListaInformacion.Style.Add("padding-left", "0px")
        TablaListaInformacion.Style.Add("margin-left", "18px")
    End Sub

    Public Sub Listar_Empleados()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim drT As DataRow
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        dt.Columns.Add("c5")
        dt.Columns.Add("c6")
        dt.Columns.Add("c7")
        dt.Columns.Add("c8")
        dt.Columns.Add("c9")
        dt.Columns.Add("c10")
        dtListado = obj.Listar_Empleados(Session("Ruta_emp"))
        If dtListado.Columns.Count > 0 Then
            For Each dr As DataRow In dtListado.Rows
                drT = dt.NewRow
                drT("c1") = Nu(dr("PERSON_CODIGO"))
                drT("c2") = Nu(dr("Empleado"))
                drT("c3") = Nu(dr("PERSON_EMAIL"))
                drT("c4") = ""
                drT("c5") = ""
                drT("c6") = ""
                drT("c7") = ""
                drT("c8") = ""
                drT("c9") = ""
                drT("c10") = ""
                dt.Rows.Add(drT)
            Next
        End If
        GvListaInformacion.Columns(0).HeaderText = ""
        GvListaInformacion.Columns(1).HeaderText = "Código"
        GvListaInformacion.Columns(2).HeaderText = "Nombres y Apellidos"
        GvListaInformacion.Columns(3).HeaderText = "Correo Electrónico"
        GvListaInformacion.Columns(4).HeaderText = ""
        GvListaInformacion.Columns(5).HeaderText = ""
        GvListaInformacion.Columns(6).HeaderText = ""
        GvListaInformacion.Columns(7).HeaderText = ""
        GvListaInformacion.Columns(8).HeaderText = ""
        GvListaInformacion.Columns(9).HeaderText = ""
        GvListaInformacion.DataSource = dt
        GvListaInformacion.DataBind()
    End Sub

    Public Sub Listar_Documentos()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim CodUsuario As String = LblUsuario.Text.ToString
        Dim drT As DataRow
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        dt.Columns.Add("c5")
        dt.Columns.Add("c6")
        dt.Columns.Add("c7")
        dt.Columns.Add("c8")
        dt.Columns.Add("c9")
        dt.Columns.Add("c10")
        dtListado = obj.Listar_Documentos(Session("Ruta_emp"), CodUsuario, "%")
        If dtListado.Columns.Count > 0 Then
            For Each dr As DataRow In dtListado.Rows
                drT = dt.NewRow
                drT("c1") = Nu(dr("TA_APLICACION_DESCRIPCION"))
                drT("c2") = Nu(dr("TEMA_AYUDA_DESCRIPCION"))
                drT("c3") = Nu(dr("TEMA_AYUDA_NOMBRE_DOC"))
                drT("c4") = Nu(dr("TIPOINGRESO"))
                drT("c5") = Nu(dr("TA_APLICACION_CODIGO"))
                drT("c6") = Nu(dr("TEMA_AYUDA_TABLA_RELACION"))
                drT("c7") = ""
                drT("c8") = ""
                drT("c9") = ""
                drT("c10") = ""
                dt.Rows.Add(drT)
            Next
        End If

        GvListaInformacion.Columns(0).HeaderText = ""
        GvListaInformacion.Columns(1).HeaderText = "Aplicación"
        GvListaInformacion.Columns(2).HeaderText = "Descripción"
        GvListaInformacion.Columns(3).HeaderText = "Nombre"
        GvListaInformacion.Columns(4).HeaderText = "Tipo de Ingreso"
        GvListaInformacion.Columns(5).HeaderText = ""
        GvListaInformacion.Columns(6).HeaderText = ""
        GvListaInformacion.Columns(7).HeaderText = ""
        GvListaInformacion.Columns(8).HeaderText = ""
        GvListaInformacion.Columns(9).HeaderText = ""
        GvListaInformacion.DataSource = dt
        GvListaInformacion.DataBind()
    End Sub

    Public Sub Listar_Contactos()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim CodCliente As String = LblCodCliente.Text.ToString
        Dim drT As DataRow
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        dt.Columns.Add("c5")
        dt.Columns.Add("c6")
        dt.Columns.Add("c7")
        dt.Columns.Add("c8")
        dt.Columns.Add("c9")
        dt.Columns.Add("c10")
        dtListado = obj.Listar_Contactos(Session("Ruta_emp"), CodCliente)
        If dtListado.Columns.Count > 0 Then
            For Each dr As DataRow In dtListado.Rows
                drT = dt.NewRow
                drT("c1") = Nu(dr("Contacto"))
                drT("c2") = Nu(dr("CONTACTO_EMAIL"))
                drT("c3") = Nu(dr("CONTACTO_PUESTO_2"))
                drT("c4") = ""
                drT("c5") = ""
                drT("c6") = ""
                drT("c7") = ""
                drT("c8") = ""
                drT("c9") = ""
                drT("c10") = ""
                dt.Rows.Add(drT)
            Next
        End If

        GvListaInformacion.Columns(0).HeaderText = ""
        GvListaInformacion.Columns(1).HeaderText = "Nombres y Apellidos"
        GvListaInformacion.Columns(2).HeaderText = "Correo Electrónico"
        GvListaInformacion.Columns(3).HeaderText = "Cargo"
        GvListaInformacion.Columns(4).HeaderText = ""
        GvListaInformacion.Columns(5).HeaderText = ""
        GvListaInformacion.Columns(6).HeaderText = ""
        GvListaInformacion.Columns(7).HeaderText = ""
        GvListaInformacion.Columns(8).HeaderText = ""
        GvListaInformacion.Columns(9).HeaderText = ""
        GvListaInformacion.DataSource = dt
        GvListaInformacion.DataBind()
    End Sub

    Public Sub Listar_Cuerpo_Email()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim drT As DataRow
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        dt.Columns.Add("c5")
        dt.Columns.Add("c6")
        dt.Columns.Add("c7")
        dt.Columns.Add("c8")
        dt.Columns.Add("c9")
        dt.Columns.Add("c10")
        dtListado = obj.Listar_Cuerpo_Email(Session("Ruta_emp"))
        If dtListado.Rows.Count > 0 Then
            For Each dr As DataRow In dtListado.Rows
                drT = dt.NewRow
                drT("c1") = Nu(dr("Negocio"))
                drT("c2") = Nu(dr("CORREO_NOMBRE"))
                drT("c3") = Nu(dr("CORREO_ASUNTO"))
                drT("c4") = Nu(dr("CORREO_SALUDO"))
                drT("c5") = Nu(dr("CORREO_CUERPO"))
                drT("c6") = Nu(dr("CORREO_DESPEDIDA"))
                drT("c7") = Nu(dr("CORREO_FIRMA"))
                drT("c8") = Nu(dr("CORREO_FIRMA_IMAGEN"))
                drT("c9") = Nu(dr("CORREO_IMAGEN"))
                drT("c10") = ""
                dt.Rows.Add(drT)
            Next
        End If
        GvListaInformacion.Columns(0).HeaderText = ""
        GvListaInformacion.Columns(1).HeaderText = "Tipo de Negocio"
        GvListaInformacion.Columns(2).HeaderText = "Descripción"
        GvListaInformacion.Columns(3).HeaderText = "Asunto"
        GvListaInformacion.Columns(4).HeaderText = "Saludo"
        GvListaInformacion.Columns(5).HeaderText = "Cuerpo"
        GvListaInformacion.Columns(6).HeaderText = "Despedida"
        GvListaInformacion.Columns(7).HeaderText = "Firma"
        GvListaInformacion.Columns(8).HeaderText = "Imagen Firma"
        GvListaInformacion.Columns(9).HeaderText = "Imagen Correo"
        GvListaInformacion.DataSource = dt
        GvListaInformacion.DataBind()
    End Sub

    Private Sub BtnAdjuntarTraking_Click(sender As Object, e As EventArgs) Handles BtnAdjuntarTraking.Click
        Dim dt As DataTable
        Dim obj As New Cls_Enviar_Email
        Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
        Dim Elemento As String = Convert.ToString(Request.QueryString("OfnoiafRFS"))
        Dim FechaReporta As String = Convert.ToString(Request.QueryString("ASIFasfajsAS"))
        Dim HoraReporta As String = Convert.ToString(Request.QueryString("ioanfmOAISN"))
        Dim CodCliente As String = Convert.ToString(Request.QueryString("aDFWQASRAF"))
        Dim psconexion As String = Session("Ruta_Emp")

        If RBCorreo.Checked And LblRepeatCorreo.Text = "0" Then

            TxtMensaje.Value += vbCrLf + vbCrLf + "                                        TRAKING DE CORREOS" +
                      vbCrLf + "=====================================================================" +
                      vbCrLf + "#    N° Ticket         Persona Enviada                   Fecha                  Hora                     Estado" + vbCrLf +
                      "=====================================================================" + vbCrLf
            Dim Correlativo As Integer = 1

            dt = obj.Lista_Traking_Correo(psconexion, NroTicket)
            For Each dbR As DataRow In dt.Rows
                TxtMensaje.Value += CStr(Correlativo) + "    " + NroTicket + "         " + dbR("PERSONA_ENVIADA") + "       " + dbR("FECHA") + "        " +
                      dbR("HORA") + "          " + dbR("APROB_ESTADO") + vbCrLf
                Correlativo = Correlativo + 1
            Next
            LblRepeatCorreo.Text = "1"
            TxtMensaje.Value += "====================================================================="
        ElseIf RBAcciones.Checked And LblRepeatAcciones.Text = "0" Then
            TxtMensaje.Value += vbCrLf + vbCrLf + "                                        TRAKING DE ACCIONES" +
                                  vbCrLf + "========================================================================" +
                                  vbCrLf + "#    N° Ticket      Fecha       Hora        Acción      Referencia      Código                  Usuario" + vbCrLf +
                                  "========================================================================" + vbCrLf
            Dim Correlativo As Integer = 1

            dt = obj.Lista_Traking_Acciones(psconexion, NroTicket)
            For Each dbR As DataRow In dt.Rows
                TxtMensaje.Value += CStr(Correlativo) + "    " + NroTicket + "      " + dbR("ACCION_FECHA") + "       " +
                                dbR("ACCION_HORA") + "    " + dbR("ACCION") + "    " + Nu(dbR("ETIQUETA_REFERENCIA")) +
                                "       " + Nu(dbR("COD_REFERENCIA")) + "                  " + dbR("USUARIO") + vbCrLf

                Correlativo = Correlativo + 1
            Next
            LblRepeatAcciones.Text = "1"
            TxtMensaje.Value += "====================================================================="
        ElseIf RBEstados.Checked And LblRepeatEstado.Text = "0" Then
            TxtMensaje.Value += vbCrLf + vbCrLf + "                                        TRAKING DE ESTADOS" +
                                  vbCrLf + "========================================================================" +
                                  vbCrLf + "#    N° Ticket      Fecha       Hora        Estado                  Usuario                  Observación" + vbCrLf +
                                  "========================================================================" + vbCrLf
            Dim Correlativo As Integer = 1

            dt = obj.Lista_Traking_Estado(psconexion, NroTicket)
            For Each dbR As DataRow In dt.Rows
                TxtMensaje.Value += CStr(Correlativo) + "    " + NroTicket + "      " + dbR("FECHA_REGISTRO") + "       " +
                                dbR("HORA_REGISTRO") + "        " + dbR("PESTADO") + "                  " + dbR("USUARIO") +
                                "                  " + dbR("REGISTRO_OBSERVACION") + vbCrLf
                Correlativo = Correlativo + 1
            Next
            LblRepeatEstado.Text = "1"
            TxtMensaje.Value += "====================================================================="
        End If
        Llenar_Combo_Informacion()
    End Sub

    Protected Sub Llenar_Combo_Tipo_Correo()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Tipo_Correo(psconexion)
        DdlTipoCorreo.DataSource = dt
        DdlTipoCorreo.DataValueField = "ADMIN_TCORREO_CODIGO"
        DdlTipoCorreo.DataTextField = "ADMIN_TCORREO_DESCRIPCION"
        DdlTipoCorreo.DataBind()
        DdlTipoCorreo.Items.Add("< Seleccionar >")
        DdlTipoCorreo.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub BtnLimpiarEmail_Click(sender As Object, e As EventArgs) Handles BtnLimpiarEmail.Click
        Limpiar_Cajas_Email()
    End Sub

    Private Sub Limpiar_Cajas_Email()
        TxtPara.Text = ""
        TxtCopia.Text = ""
        TxtAsunto.Text = ""
        TxtSaludo.Text = ""
        TxtMensaje.Value = ""
        TxtDespedida.Value = ""
        TxtFirma.Value = ""
        LblRepeatAcciones.Text = "0"
        LblRepeatCorreo.Text = "0"
        LblRepeatEstado.Text = "0"
    End Sub


    Protected Sub Buscar_Ultimo_Envio()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim DbRow As DataRow
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Buscar_Ultimo_Envio(psconexion)

        DbRow = dt.Rows(0)

        TxtUltimoEnvio.Text = DbRow(0)
        LblHoraEnvio.Text = DbRow(1)
    End Sub

    Protected Sub Buscar_Numero_Envios()
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim DbRow As DataRow
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Buscar_Numero_Envios(psconexion)

        DbRow = dt.Rows(0)

        TxtNroEnvios.Text = DbRow(0)

    End Sub

    Private Sub GvListaInformacion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaInformacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Enviar_Email
        Dim dt As New DataTable
        If DdlInformacion.SelectedValue = 1 Then
            If e.CommandName = "Carga" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCargaDatosEmpleados').modal('show');", True)
                LblIndex.Text = Index.ToString
                LblCodAplicacion.Text = GvListaInformacion.Rows(Index).Cells(5).Text
                LblCodRelacion.Text = GvListaInformacion.Rows(Index).Cells(6).Text
            End If

        ElseIf DdlInformacion.SelectedValue = 2 Then
            If e.CommandName = "Carga" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCargaDatosContactos').modal('show');", True)
                LblIndex.Text = Index.ToString
            End If

        ElseIf DdlInformacion.SelectedValue = 3 Then
            If e.CommandName = "Carga" Then
                Dim dt1 As New DataTable
                dt1.Columns.Add("c1")
                Dim DtRow As DataRow = dt1.NewRow
                If GvArchivosAdjuntos.Rows.Count > 0 Then
                    For i = 0 To GvArchivosAdjuntos.Rows.Count - 1
                        DtRow = dt1.NewRow
                        DtRow("c1") = GvArchivosAdjuntos.Rows(i).Cells(0).Text.ToString()
                        dt1.Rows.Add(DtRow)
                    Next
                End If
                DtRow = dt1.NewRow
                DtRow("c1") = GvListaInformacion.Rows(Index).Cells(3).Text.ToString()
                dt1.Rows.Add(DtRow)
                GvArchivosAdjuntos.DataSource = dt1
                GvArchivosAdjuntos.DataBind()
            End If
        ElseIf DdlInformacion.SelectedValue = 4 Then

            If e.CommandName = "Carga" Then

                TxtAsunto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtSaludo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtMensaje.Value += vbCrLf + vbCrLf + Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtDespedida.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                TxtFirma.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")

            End If
        End If
    End Sub

    Private Sub BtnCerrarCargaEmpleados_Click(sender As Object, e As EventArgs) Handles BtnCerrarCargaEmpleados.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCargaDatosEmpleados').modal('hide');", True)
    End Sub

    Private Sub BtnCerrarCargaContactos_Click(sender As Object, e As EventArgs) Handles BtnCerrarCargaContactos.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCargaDatosContactos').modal('hide');", True)
    End Sub

    Private Sub BtnAceptarCargaContactos_Click(sender As Object, e As EventArgs) Handles BtnAceptarCargaContactos.Click
        Dim Index As Integer = LblIndex.Text.ToString

        If RBContactosPara.Checked Then
            TxtPara.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        ElseIf RBContactosCopia.Checked Then
            TxtCopia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        ElseIf RBContactosNombre.Checked Then
            Dim valor As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Dim c As Long = InStr(valor, ",")

            TxtAsunto.Text = "Presentación " + valor.Substring(c + 1, valor.Length() - (c + 1)).Trim()
            TxtSaludo.Text = "Estimados Sres: " + valor.Substring(c + 1, valor.Length() - (c + 1)).Trim()
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCargaDatosContactos').modal('hide');", True)
    End Sub

    Private Sub BtnAceptarCargaEmpleados_Click(sender As Object, e As EventArgs) Handles BtnAceptarCargaEmpleados.Click
        Dim Index As Integer = LblIndex.Text.ToString

        If RBEmpleadosPara.Checked Then
            TxtPara.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        ElseIf RBEmpleadosCopia.Checked Then
            TxtCopia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaInformacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCargaDatosEmpleados').modal('hide');", True)
    End Sub

    Private Sub BtnAdjuntarArchivos_Click(sender As Object, e As EventArgs) Handles BtnAdjuntarArchivos.Click

        Dim archivo As HttpPostedFile

        archivo = FileUploadAdjuntar.PostedFile

        Dim dt1 As New DataTable
        dt1.Columns.Add("c1")
        Dim DtRow As DataRow = dt1.NewRow
        If FileUploadAdjuntar.HasFile Then
            If GvArchivosAdjuntos.Rows.Count > 0 Then
                For i = 0 To GvArchivosAdjuntos.Rows.Count - 1
                    DtRow = dt1.NewRow
                    DtRow("c1") = GvArchivosAdjuntos.Rows(i).Cells(0).Text.ToString()
                    dt1.Rows.Add(DtRow)
                Next
            End If
            If File.Exists(Path.Combine(Path.GetTempPath(), archivo.FileName)) = False Then
                archivo.SaveAs(Path.Combine(Path.GetTempPath(), archivo.FileName))
            End If
            DtRow = dt1.NewRow
            DtRow("c1") = FileUploadAdjuntar.PostedFile.FileName

            dt1.Rows.Add(DtRow)
            GvArchivosAdjuntos.DataSource = dt1
            GvArchivosAdjuntos.DataBind()
        End If
    End Sub
End Class