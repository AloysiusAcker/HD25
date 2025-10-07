Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Archivo_Telefonica
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblRegistro.Text = ""
            txtFecha.Text = FormatoFecha(FechaActual)
            txtFechaFin.Text = FormatoFecha(FechaActual)
            Call Lista()
        End If
    End Sub
    Private Sub Lista()
        Dim obj As New clsInv_Listados
        lblRegistro.Text = ""
        Dim FechaEntrega As String : FechaEntrega = ""
        Dim FechaFin As String : FechaFin = ""
        FechaEntrega = Right(txtFecha.Text, 4) + Mid(txtFecha.Text, 4, 2) + Left(txtFecha.Text, 2)
        If txtFechaFin.Text <> "" Then FechaFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        Try
            Flex.DataSource = obj.Lista_ArchivoTelefonica(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, FechaFin)
            Flex.DataBind()
            lblRegistro.Text = "Registros Encontrados : " & Flex.Rows.Count
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Lista()
    End Sub
    Private Sub Exportar_Excel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Flex.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(Flex)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Archivo_Telefonica.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Private Sub Exportar_Excel2(ByVal dt As DataTable)
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

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
    End Sub
End Class
