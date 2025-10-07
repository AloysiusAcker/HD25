Imports WebGestor
Imports System.Data
Imports system.Data.SqlClient
Partial Class Inspeccion_Resumen
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtFechaIni.Text = FormatoFecha(FechaActual)
            btnListar_Click(sender, e)
        End If
    End Sub
    Private Sub Lista_Inspeccion()
        Dim obj As New clsInspeccion_Listado
        Dim pdCodOficina As Double = 0
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        If txtCodOficina.Text.Trim <> "" Then pdCodOficina = txtCodOficina.Text.Trim
        If txtFechaIni.Text.Trim <> "" And txtFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
            FechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
        ElseIf txtFechaIni.Text.Trim <> "" And txtFechaFin.Text.Trim = "" Then
            FechaIni = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
            FechaFin = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
        ElseIf txtFechaIni.Text.Trim = "" And txtFechaFin.Text.Trim = "" Then
            FechaIni = "20100101"
            FechaFin = "21000101"
        ElseIf txtFechaIni.Text.Trim = "" And txtFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
            FechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
        End If
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            Flex.DataSource = obj.Lista_Inspeccion(psConexion, Session("CodEmpresa"), FechaIni, FechaFin, pdCodOficina)
            Flex.DataBind()
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Resumen.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Lista_Inspeccion()
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Lista_Inspeccion()
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Exportar_Excel()
    End Sub

    Protected Sub btnListarOf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarOf.Click
        Dim obj As New clsInv_Listados
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexOf.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCodigo.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexOf.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub FlexOf_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexOf.RowCommand
        Try
            lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtCodIntOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOf.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOf.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtCodOficina.Text = FlexOf.Rows(Index).Cells(3).Text.Trim
                FlexOf.DataSource = Nothing
                FlexOf.DataBind()
                txtBusCodigo.Text = ""
                txtBusDescripcion.Text = ""
                ModalPopupExtender1.Hide()
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub txtCodIntOficina_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtCodOficina.Text = ""
        txtOficina.Text = ""
    End Sub

    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

    End Sub

    Protected Sub txtBusCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBusCodigo.TextChanged

    End Sub
End Class

