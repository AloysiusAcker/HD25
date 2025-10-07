Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inventario_Garantia_Equipos
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim pdCodArt As Double = 0
        Dim pdCodProveedor As Double = 0
        lblError.Text = ""
        If txtArtCodigo.Text.Trim <> "" Then pdCodArt = txtArtCodigo.Text.Trim
        If txtProvCodigo.Text.Trim <> "" Then pdCodProveedor = txtProvCodigo.Text.Trim
        Try
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            Flex.DataSource = obj.Lista_Garantia_Equipos(psConexion, Session("CodEmpresa"), txtSerie.Text.Trim, pdCodProveedor, pdCodArt)
            Flex.DataBind()
            lblRegistro.Visible = True
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.Height = 500
            Ficha.ActiveTabIndex = 0
        End If
    End Sub
    Protected Sub btnListarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As New clsInv_Listados
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            If txtPArtCodigo.Text.Trim <> "" Then pdCodArt = txtPArtCodigo.Text.Trim
            FlexArt.DataSource = obj.BuscarX_Articulos(psConexion, Session("CodEmpresa"), pdCodArt, txtPArtDescripcion.Text.Trim, "")
            FlexArt.DataBind()
            ModalPopupExtender1.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexArt_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexArt.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtArtDescripcion.Text = ""
            txtArtCodigo.Text = ""
            txtArtCodigo.Text = FlexArt.Rows(Index).Cells(1).Text
            txtArtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexArt.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            ModalPopupExtender1.Hide()
            txtPArtCodigo.Text = ""
            txtPArtDescripcion.Text = ""
            FlexArt.DataSource = Nothing
            FlexArt.DataBind()
        End If
    End Sub
    Protected Sub txtArtCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtArtCodigo.TextChanged
        txtArtCodigo.Text = ""
        txtArtDescripcion.Text = ""
    End Sub
    Protected Sub btnListarProv_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As New clsInv_Listados
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            FlexProv.DataSource = obj.Lista_Proveedor(psConexion, Session("CodEmpresa"), txtPRuc.Text.Trim, txtPRazonSocial.Text.Trim)
            FlexProv.DataBind()
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexProv_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexProv.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtProvRuc.Text = ""
            txtProvRazonSocial.Text = ""
            txtProvCodigo.Text = ""
            txtProvCodigo.Text = FlexProv.Rows(Index).Cells(3).Text
            txtProvRazonSocial.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexProv.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtProvRuc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexProv.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            ModalPopupExtender2.Hide()
            txtPRuc.Text = ""
            txtPRazonSocial.Text = ""
            FlexProv.DataSource = Nothing
            FlexProv.DataBind()
        End If
    End Sub
    Protected Sub FlexProv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub txtProvRuc_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtProvRuc.TextChanged
        txtProvRuc.Text = ""
        txtProvRazonSocial.Text = ""
        txtProvCodigo.Text = ""
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Garantia de Equipos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Exportar_Excel()
    End Sub
    Protected Sub btnCerrarProv_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPRuc.Text = ""
        txtPRazonSocial.Text = ""
        FlexProv.DataSource = Nothing
        FlexProv.DataBind()
        ModalPopupExtender2.Hide()
    End Sub

    Protected Sub btnCerrarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPArtCodigo.Text = ""
        txtPArtDescripcion.Text = ""
        FlexArt.DataSource = Nothing
        FlexArt.DataBind()
        ModalPopupExtender1.Hide()
    End Sub
End Class
