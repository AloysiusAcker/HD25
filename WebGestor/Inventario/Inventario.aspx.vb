Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports WebGestor
Imports System.IO
Partial Class Inventario_Inventario
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblFecha.InnerText = Format(CDate(FormatoFecha(FechaActual())), "dddd, dd 'de' MMMM 'de' yyyy")
            If Session("UserFirmado") = "N" Or Session("UserFirmado") Is Nothing Then
                Inicio.Visible = True
                Cerrar.Visible = False
                btnCambioPass.Visible = False
            Else
                Cerrar.Visible = True
                Inicio.Visible = False
                btnCambioPass.Visible = True
                lblAgrup.InnerText = IIf(Session("NombreGrupoEmpresa") <> "", Session("NombreGrupoEmpresa") & " - " & Session("NombreEmpresa"), "")
            End If
            lblError.Text = ""
            lblRegistro.Text = ""
            lblRegDetalle.Text = ""
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        lblError.Text = ""
        lblRegDetalle.Text = ""
        FlexDet.DataSource = Nothing
        FlexDet.DataBind()
        Dim i As Integer = 0
        Dim psConexion As String = Session("Ruta_Emp")
        Try
            Flex.DataSource = obj.Lista_Equipos_aEnviar(Session("Ruta_Emp"), Session("CodEmpresa"))
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psCodRecep As Double = 0
        lblError.Text = ""
        Dim dt As DataTable
        If e.CommandName = "Exportar" Then
            psCodRecep = Flex.Rows(Index).Cells(3).Text
            Dim sb As StringBuilder = New StringBuilder()
            Dim sw As IO.StringWriter = New IO.StringWriter(sb)
            Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
            Dim pagina As Page = New Page
            Dim filename As String = "Lista_Nro_" & Flex.Rows(Index).Cells(3).Text & ".xls"
            Dim form = New HtmlForm
            FlexDet.EnableViewState = False
            pagina.EnableEventValidation = False
            pagina.DesignerInitialize()
            pagina.Controls.Add(form)
            form.Controls.Add(FlexDet)
            pagina.RenderControl(htw)
            Response.Clear()
            Response.Buffer = True
            Response.ContentType = "application/vnd.ms-excel"
            Response.AddHeader("Content-Disposition", "Attachment;filename=" + filename)
            Response.Charset = "UTF-8"
            Response.ContentEncoding = Encoding.Default
            Response.Write(sb.ToString())
            Response.End()
        ElseIf e.CommandName = "Enviar" Then
            psCodRecep = Flex.Rows(Index).Cells(3).Text
            dt = obj.Lista_Equipos_aEnviar_Det(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
            FlexDet.DataSource = dt
            FlexDet.DataBind()
            lblRegDetalle.Text = "Lista Nro. " & Flex.Rows(Index).Cells(3).Text & " con " & dt.Rows.Count & " registros. "
            lblCodLista.Text = psCodRecep
            txtAsunto.Text = lblRegDetalle.Text
            DivCorreo.Visible = True
            txtMensaje.Text = ""
            txtPara.Text = ""
            lstFiles.DataSource = Nothing
            lstFiles.DataBind()
        ElseIf e.CommandName = "Detalle" Then
            psCodRecep = Flex.Rows(Index).Cells(3).Text
            dt = obj.Lista_Equipos_aEnviar_Det(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
            FlexDet.DataSource = dt
            FlexDet.DataBind()
            lblRegDetalle.Text = "Lista Nro. " & Flex.Rows(Index).Cells(3).Text & " con " & dt.Rows.Count & " registros. "
        End If
    End Sub
    Private Sub EnviodeCorreo(ByVal psTo As String, ByVal psCC As String, ByVal psFrom As String, ByVal psSubject As String, ByVal psBody As String)
        Dim correo As New MailMessage()
        Me.Page.Session.Timeout = 1080
        correo.From = New MailAddress(psFrom)
        correo.To.Add(psTo)
        correo.CC.Add(psCC)
        correo.Subject = psSubject
        correo.Body = psBody
        correo.IsBodyHtml = True
        Dim smtp As New SmtpClient
        smtp.Host = "smtp.gmail.com"
        smtp.Credentials = New System.Net.NetworkCredential("soporte.tecnico.tecnologias@gmail.com", "hacc2010")
        smtp.Port = 587
        smtp.EnableSsl = True
        Try
            smtp.Send(correo)
            lblError.Text = "Mensaje enviado satisfactoriamente"
        Catch ex As Exception
            lblError.Text = "ERROR: " & ex.Message
        End Try
    End Sub

    Private tempPath As String = "~/uploads/temp"

    Protected Sub cmdAddFile_Click(sender As Object, e As EventArgs)

        Dim f As FileUpload = FileUpload1

        ' No se hace nada si no hay fichero
        If Not f.HasFile Then
            Return
        End If

        ' Se crea un Item para el ListBox
        ' - Value: Nombre del fichero
        ' - Text : Texto para mostrar
        Dim item As New ListItem()
        item.Value = f.FileName
        item.Text = f.FileName & " (" & f.FileContent.Length.ToString("N0") & " bytes)."

        ' Se sube el fichero a la carpeta temporal
        f.SaveAs(Server.MapPath(Path.Combine(tempPath, item.Value)))

        ' Se deja el nombre del fichero en el ListBox
        lstFiles.Items.Add(item)

    End Sub

    Protected Sub cmdDelFile_Click(sender As Object, e As EventArgs)
        Dim lb As ListBox = lstFiles
        ' Se comprueba que exista algún item seleccionado
        If lb.SelectedValue = Nothing Then
            Return
        End If

        ' Se elimina el fichero seleccionado
        borraEntrada(lb.SelectedItem.Value)

    End Sub

    ''' <summary>
    ''' Elimina el fichero de la carpeta temporal y del ListBox.
    ''' </summary>
    ''' <param name="fileName"></param>
    Private Sub borraEntrada(fileName As String)
        Dim fichero As String = Server.MapPath(Path.Combine(tempPath, fileName))
        File.Delete(fichero)

        Dim l As ListItem = lstFiles.Items.FindByValue(fileName)
        If l IsNot Nothing Then
            lstFiles.Items.Remove(l)
        End If

    End Sub

    Private Sub enviaCorreo()
        Using message As MailMessage = New MailMessage()
            ' Dirección de destino
            message.From = New MailAddress("soporte.tecnico.tecnologias@gmail.com")
            message.To.Add(txtPara.Text)
            ' Asunto
            message.Subject = txtAsunto.Text
            ' Mensaje
            message.Body = txtMensaje.Text

            ' Se recuperan los ficheros
            For Each l As ListItem In lstFiles.Items
                ' Lectura del nombre del fichero
                Dim fichero As String = Server.MapPath(Path.Combine(tempPath, l.Value))

                ' Adjuntado del fichero a la colección Attachments
                message.Attachments.Add(New Attachment(fichero))

            Next

            ' Se envía el mensaje y se informa al usuario
            Dim mensaje As String = String.Empty
            Try
                Dim smtp As New SmtpClient
                smtp.Host = "smtp.gmail.com"
                smtp.Credentials = New Net.NetworkCredential("soporte.tecnico.tecnologias@gmail.com", "hacc2010")
                smtp.Port = 587
                smtp.EnableSsl = True
                smtp.Send(message)
                mensaje = "Correo enviado con éxito"

            Catch ex As Exception
                mensaje = "Ocurrió un error: " & ex.Message

            End Try
            lblError.Text = mensaje

        End Using

        ' Se borran los ficheros de la carpeta temporal
        While lstFiles.Items.Count > 0
            borraEntrada(lstFiles.Items(0).Value)
        End While

    End Sub

    Protected Sub BtnEnviarCorreo_Click(sender As Object, e As EventArgs) Handles BtnEnviarCorreo.Click
        lblError.Text = ""
        If txtPara.Text = "" Then lblError.Text = "Falta Ingresar el correo." : Exit Sub
        If txtAsunto.Text = "" Then lblError.Text = "Falta ingresar el asunto del correo." : Exit Sub
        If txtMensaje.Text = "" Then lblError.Text = "Falta ingresar el mensaje del correo." : Exit Sub
        Dim psCodCorreo As String = ""
        Dim Rs As SqlDataReader
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Cn.Open() : Cn2.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
        Try
            Call enviaCorreo()
            CmdGlobal.CommandText = " update TBINV_EQUIPOSLISTA_ATRATAR set EQATARTAR_ESTADO = '2' where EQATRATAR_CODIGO = " & lblCodLista.Text
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " SELECT MAX( ENVCORREO_CODIGO) " _
                                   & " FROM TBINV_EQUIPOSLISTA_ATRATAR_ENVIO "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodCorreo = Nz(Rs(0)) + 1
                End While
            End If
            Rs.Close()
            CmdGlobal2.CommandText = " INSERT INTO  TBINV_EQUIPOSLISTA_ATRATAR_ENVIO ( EMPRESA_CODIGO, EQATRATAR_CODIGO, ENVCORREO_CODIGO, ENVCORREO_PARA, ENVCORREO_ASUNTO, " _
                                   & " ENVCORREO_MENSAJE, ENVCORREO_FECHA, ENVCORREO_HORA, ENVCORREO_USER, ENVCORREO_ESTADO, ENVCORREO_SYS_EST) " _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodLista.Text & ", " & psCodCorreo & ", '" & txtPara.Text & "', '" & txtAsunto.Text & "', " _
                                   & " '" & txtMensaje.Text & "','" & FechaActual() & "', '" & HoraActual() & "', '" & Session("User") & "', '1', '0') "
            CmdGlobal2.ExecuteNonQuery()
            DivCorreo.Visible = False
            lblRegDetalle.Text = ""
            FlexDet.DataSource = Nothing
            FlexDet.DataBind()
            btnListar_Click(sender, e)
            lblError.Text = "Correo enviado con éxito"
        Catch ex As Exception
            lblError.Text = "ERROR: " & ex.Message
        End Try
    End Sub
End Class
