Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inspeccion_Lista_DatosAdicionales_Usuarios
    Inherits System.Web.UI.Page
    Private Sub Lista_Inspeccion()
        Dim obj As New clsInspeccion_Listado
        Dim pdCodOficina As Double = 0
        Dim NroInspeccion As Double = 0
        Dim Tecnico As String = "0"
        Dim TipoPersona As String = "0"
        Dim TipoInspeccion As String = "0"
        Dim TipoEstado As String = "0"
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If txtNroInspeccion.Text.Trim <> "" Then NroInspeccion = txtNroInspeccion.Text.Trim
        If txtcodOficina.Text.Trim <> "" Then pdCodOficina = txtcodOficina.Text.Trim
        If cboTipoPersona.SelectedValue <> "< Seleccionar >" Then TipoPersona = cboTipoPersona.SelectedValue Else txtTecnico.Text = ""
        If txtTecnico.Text.Trim <> "" Then Tecnico = txtTecnico.Text.Trim
        If cboTipoInspeccion.SelectedValue <> "< Seleccionar >" Then TipoInspeccion = cboTipoInspeccion.SelectedValue
        If cboEstadoInspeccion.SelectedValue <> "< Seleccionar >" Then TipoEstado = cboEstadoInspeccion.SelectedValue
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
        Try
            Flex.DataSource = obj.Listar_Inpeccion_Datos_Adicionales(psConexion, Session("CodEmpresa"), TipoPersona, _
            TipoInspeccion, TipoEstado, FechaIni, FechaFin, pdCodOficina, Tecnico, NroInspeccion)
            Flex.DataBind()
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
            Exit Sub
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnListarDatosAdicionales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarDatosAdicionales.Click
        Call Lista_Inspeccion()
    End Sub

    Protected Sub cboEstadoInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub cboTipoInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub cboTipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoPersona.SelectedIndexChanged
        If cboTipoPersona.SelectedValue <> "< Seleccionar >" Then
            txtRucTipoPersona.Text = ""
            txtRazonSocialTipoPersona.Text = ""
        Else
            txtRucTipoPersona.Text = ""
            txtRazonSocialTipoPersona.Text = ""
        End If
    End Sub

    Protected Sub btnListarOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarOficina.Click
        Dim obj As New clsInv_Listados
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexOficina.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCodigo.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexOficina.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
        ModalPopupExtender1.Show()
    End Sub
    Private Sub listarTipoPersonaXProveedor()
        Dim obj As New clsInspeccion_Listado
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexTipoPers.DataSource = obj.Lista_TipoPersona(psConexion, Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub listarTipoPersonaXTecnico()
        Dim obj As New clsInspeccion_Listado
        Dim psCodGrupoEmp As Double = 0
        psCodGrupoEmp = Session("CodGrupoEmpresa") 'psCodGrupoEmp, Session("CodEmpresa"), 
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexTipoPers.DataSource = obj.Lista_TipoPersonaTecnico(psConexion, psCodGrupoEmp, Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnListarTipoPers_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarTipoPers.Click
        If cboTipoPersona.SelectedValue.Trim = "3" Then
            listarTipoPersonaXProveedor()
        ElseIf cboTipoPersona.SelectedValue.Trim = "< Seleccionar >" Then
            ModalPopupExtender2.Hide()
        Else
            listarTipoPersonaXTecnico()
        End If
        ModalPopupExtender2.Show()
    End Sub

    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            'lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtTecnico.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtRucTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtRazonSocialTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            End If
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            txtRucTipoPers.Text = ""
            txtRazonSocialTipoPers.Text = ""
            ModalPopupExtender2.Hide()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
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

    Protected Sub btnBuscarXOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarXOficina.Click

    End Sub

    Protected Sub btnBuscarTipoPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarTipoPersona.Click

    End Sub

    Protected Sub txtPorCodOficina_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtcodOficina.Text = ""
        txtPorCodOficina.Text = ""
        txtPorOficDescrip.Text = ""
    End Sub

    Protected Sub txtRucTipoPersona_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRucTipoPersona.TextChanged
        txtRucTipoPersona.Text = ""
        txtRazonSocialTipoPersona.Text = ""
        txtTecnico.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
            Call LlenaComboItem("TBOPC379", cboEstadoInspeccion, psCnGrEmp)
            cboEstadoInspeccion.Items.Add("< Seleccionar >")
            cboEstadoInspeccion.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC381", cboTipoInspeccion, psCnGrEmp)
            cboTipoInspeccion.Items.Add("< Seleccionar >")
            cboTipoInspeccion.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC378", cboTipoPersona, psCnGrEmp)
            cboTipoPersona.Items.Add("< Seleccionar >")
            cboTipoPersona.SelectedValue = "< Seleccionar >"
        End If
    End Sub

    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=DatosAdicionalesXUsuario.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
