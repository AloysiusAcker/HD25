Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports WebGestor
Imports System.IO

Partial Class Inventario_Inventario_Lista_EquiposATratar
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
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
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
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
        Dim dt As DataTable
        If e.CommandName = "Detalle" Then
            psCodRecep = Flex.Rows(Index).Cells(2).Text
            dt = obj.Lista_Equipos_aEnviar_Det(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
            FlexDet.DataSource = dt
            FlexDet.DataBind()
            lblRegDetalle.Text = "Lista Nro. " & Flex.Rows(Index).Cells(2).Text & " con " & dt.Rows.Count & " registros. "
        End If
        If e.CommandName = "Enviar" Then
            psCodRecep = Flex.Rows(Index).Cells(2).Text
            dt = obj.Lista_Equipos_aEnviar_Det(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
            FlexDet.DataSource = dt
            FlexDet.DataBind()
            lblRegDetalle.Text = "Lista Nro. " & Flex.Rows(Index).Cells(2).Text & " con " & dt.Rows.Count & " registros. "
            ExportToExcel("Lista.xls", FlexDet)
            Call EnviodeCorreo("Sli@tsgestion.com", "hcornejo@tsgestion.com", "soporte.tecnico.tecnologias@gmail.com", "Lista Nro. " & Flex.Rows(Index).Cells(2).Text, lblRegDetalle.Text)
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
        correo.Attachments.Add(New Attachment("c:\STOCK.xls"))
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

    Private Sub exportar_excel(ByVal dt As DataTable)
        Dim StwWriter As New System.IO.StringWriter
        Dim htwWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(StwWriter)
        Dim dgGrid As DataGrid = New DataGrid
        dgGrid.DataSource = dt
        dgGrid.HeaderStyle.Font.Bold = True
        dgGrid.DataBind()
        dgGrid.RenderControl(htwWriter)
        Response.ContentType = "application/vnd.ms-excel"
        Me.EnableViewState = False
        Response.Write(StwWriter.ToString)
        Response.End()
    End Sub

    Private Sub ExportToExcel(ByVal nameReport As String, ByVal wControl As GridView)
        wControl.AllowPaging = False
        wControl.AllowSorting = False
        wControl.EditIndex = -1
        wControl.DataBind()
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}.xls", nameReport))
        Response.Charset = ""
        Response.ContentType = "application/vnd.xls"
        Dim StwWriter As New System.IO.StringWriter
        Dim htwWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(StwWriter)
        wControl.RenderControl(htwWriter)
        Response.Write(StwWriter.ToString())
        Response.End()
    End Sub
End Class
