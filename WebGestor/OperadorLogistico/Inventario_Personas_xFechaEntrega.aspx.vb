Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Personas_xFechaEntrega
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
        DIV2.Visible = False
        DIV3.Visible = False
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New clsInv_Listados
        lblError.Text = ""
        Dim FechaEntrega As String : FechaEntrega = ""
        Dim FechaFin As String : FechaFin = ""
        FechaEntrega = Right(txtFecha.Text, 4) + Mid(txtFecha.Text, 4, 2) + Left(txtFecha.Text, 2)
        If txtFechaFin.Text <> "" Then FechaFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        Try
            Flex.DataSource = obj.Lista_PersonaxFecha(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, FechaFin, txtNroPedido.Text.Trim, txtNroSerie.Text.Trim)
            Flex.DataBind()
            'lblRegistro.Text = obj.Lista_PersonaxFecha(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, FechaFin, txtNroPedido.Text.Trim, txtNroSerie.Text.Trim).Rows.Count
            lblRegistro.Text = "Registros Encontrados : " & Flex.Rows.Count
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtNroPedido.Text = ""
            txtFecha.Text = FormatoFecha(FechaActual)
            txtFechaFin.Text = FormatoFecha(FechaActual)
            Call btnListar_Click(sender, e)
        End If
    End Sub
    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInv_Listados
        Dim dtArchivo As New DataTable
        Dim dtObs As New DataTable
        Dim pcodArchivo As Double : pcodArchivo = 0
        Dim i As Integer : i = 0
        Dim Fila As GridViewRow
        Dim pCodPedido As Double : pCodPedido = 0
        lblError.Text = ""
        DIV2.Visible = False
        DIV3.Visible = False
        If e.CommandName = "Archivo" Then
            DIV2.Visible = True
            DIV3.Visible = True
            pCodPedido = Flex.Rows(Index).Cells(17).Text
            dtArchivo = obj.Lista_ArchivosxPedido(Session("Ruta_Emp"), Session("CodEmpresa"), pCodPedido)
            FlexDet.DataSource = dtArchivo
            FlexDet.DataBind()
            dtArchivo = Nothing
            For i = 0 To FlexDet.Rows.Count - 1
                pcodArchivo = CDbl(FlexDet.Rows(i).Cells(0).Text.Trim.Replace("&nbsp;", "0"))
                dtArchivo = obj.Lista_PedidoArchivoxCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), pcodArchivo)
                If dtArchivo.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtArchivo.Rows
                        Fila = FlexDet.Rows(i)
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("ARCHIVO_NOMBRE")) & "'TARGET='_blank'>" & Nu(drMenuItem("ARCHIVO_NOMBRE")) & "</A>"
                    Next
                End If
                dtArchivo = Nothing
            Next
            dtObs = obj.Lista_ObsxPedido(Session("Ruta_Emp"), Session("CodEmpresa"), pCodPedido)
            FlexObs.DataSource = dtObs
            FlexObs.DataBind()
            dtObs = Nothing
        End If
    End Sub
    Private Sub Exportar_Excel(ByVal dt As DataTable)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Liquidacion.xls")
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
        lblError.Text = ""
        Dim dt As New DataTable
        Dim obj As New clsInv_Listados
        Dim FechaEntrega As String : FechaEntrega = ""
        Dim FechaFin As String : FechaFin = ""
        FechaEntrega = Right(txtFecha.Text, 4) + Mid(txtFecha.Text, 4, 2) + Left(txtFecha.Text, 2)
        If txtFechaFin.Text <> "" Then FechaFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        Try
            dt = obj.Lista_PersonaxFecha(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, FechaFin, txtNroPedido.Text.Trim, txtNroSerie.Text.Trim)
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Call Exportar_Excel(dt)
    End Sub
End Class
