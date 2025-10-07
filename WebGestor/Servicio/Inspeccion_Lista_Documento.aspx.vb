Imports System.Data.SqlClient
Imports WebGestor
Imports System.Data
Partial Class Inspeccion_Lista_Documento
    Inherits System.Web.UI.Page
    Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
    Dim psCodEmpresa As String = ConfigurationManager.AppSettings("CodEmpresa")
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla_TA()
    End Sub
    Private Sub Llenar_Grilla_TA()
        Dim NroInspecc As Double = 0
        Dim oficina As Double = 0
        oficina = Nz(txtcodOficina.Text.Trim)
        Dim dtListado As New DataTable
        Dim obj As New clsInspeccion_Listado
        Dim i As Integer
        Dim pCodigo As Double
        Dim Fila As GridViewRow
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        Dim psTipoIngreso As String = ""
        If cboPorTipoIng.SelectedValue <> "< Seleccionar >" Then
            psTipoIngreso = cboPorTipoIng.SelectedValue.Trim
        ElseIf cboPorTipoIng.SelectedValue = "< Seleccionar >" Then
            psTipoIngreso = ""
        End If
        If txtNroInspeccion.Text <> "" Then
            NroInspecc = Nz(txtNroInspeccion.Text.Trim)
        End If
        If txtPorFechaInicio.Text.Trim <> "" And txtPorFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
            FechaFin = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
        ElseIf txtPorFechaInicio.Text.Trim <> "" And txtPorFechaFin.Text.Trim = "" Then
            FechaIni = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
            FechaFin = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
        ElseIf txtPorFechaInicio.Text.Trim = "" And txtPorFechaFin.Text.Trim = "" Then
            FechaIni = "20100101"
            FechaFin = "21000101"
        ElseIf txtPorFechaInicio.Text.Trim = "" And txtPorFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
            FechaFin = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
        End If
        If txtcodOficina.Text.Trim <> "" Then oficina = txtcodOficina.Text.Trim
        Try
            dtListado = obj.Listar_Ayuda_General(psConexion, oficina, FechaIni, FechaFin, NroInspecc, psTipoIngreso, psCodEmpresa, User.Identity.Name)
            Flex.DataSource = dtListado
            Flex.DataBind()
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
            dtListado = Nothing
            For i = 0 To Flex.Rows.Count - 1
                pCodigo = Flex.Rows(i).Cells(0).Text.Trim
                dtListado = obj.Listar_TemaAyuda(psConexion, pCodigo)
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = Flex.Rows(i)
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "'TARGET='_blank'>" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch Ex As SqlException

        Catch Ex As Exception

        Finally
        End Try
    End Sub
    Protected Sub btnCerrarOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarOficina.Click

    End Sub
    Protected Sub btnListarOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarOficina.Click
        Dim obj As New clsInv_Listados
        Try
            FlexOficina.DataSource = obj.Lista_Oficina(psConexion, psCodEmpresa, txtBusCodigo.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexOficina.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
        ModalPopupExtender1.Show()
    End Sub
    Protected Sub FlexOficina_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtPorCodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtPorOficDescrip.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtcodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                FlexOficina.DataSource = Nothing
                FlexOficina.DataBind()
                txtBusCodigo.Text = ""
                txtBusDescripcion.Text = ""
                ModalPopupExtender1.Hide()
            End If
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub txtPorCodOficina_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtcodOficina.Text = ""
        txtPorCodOficina.Text = ""
        txtPorOficDescrip.Text = ""
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtNroInspeccion.Text = ""
            txtBusCodigo.Text = ""
            txtBusDescripcion.Text = ""
            txtcodOficina.Text = ""
            txtPorCodOficina.Text = ""
            txtPorFechaFin.Text = ""
            txtPorFechaInicio.Text = ""
            txtPorOficDescrip.Text = ""
            Call LlenaComboItem("TBOPC372", cboPorTipoIng)
            cboPorTipoIng.Items.Add("< Seleccionar >")
            cboPorTipoIng.SelectedValue = ("< Seleccionar >")
        End If
    End Sub
End Class

