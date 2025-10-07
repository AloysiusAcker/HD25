Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inspeccion_Centro_Costos
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Try
            FlexCentroCostos.DataSource = obj.Listar_Centro_Costos(psconexion, Session("CodEmpresa"), txtCodInterno.Text.Trim, txtDescripcion.Text.Trim)
            FlexCentroCostos.DataBind()
            lblNumCentroCostos.Visible = True
            lblNumCentroCostos.Text = "Se encontraron " & FlexCentroCostos.Rows.Count & " registros"
        Catch ex As SqlException
            lblErrorCosto.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorCosto.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub FlexCentroCostos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim obj As New clsInspeccion_Listado
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Try
            If e.CommandName = "Seccion" Then
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
                Ficha.Height = 320
                Ficha.ActiveTabIndex = 1
                LblNumSeccion.Visible = True
                lblIngresoSeccion.Visible = False
                LblNumSeccion.Text = "Se encontraron " & FlexCostoSeccion.Rows.Count & " registros"
                txtCodInternoSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDescSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtCodigoCostosSecc.Text = FlexCentroCostos.Rows(Index).Cells(11).Text.Trim()
                Call Llenar_Seccion()
            End If
            If e.CommandName = "Editar" Then
                lblIngresoCentroCostos.Visible = True
                lblEtiqCentroCosto.Text = "Editar Centro de Costo"
                Ficha.Height = 470
                txtCodCentroCostos.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDescripCentroCostos.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtPisoCentroCosto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDireccCentroCosto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtEdificioCentroCosto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtUbicaCentroCosto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtRucCentroCosto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtCodCostos.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCentroCostos.Rows(Index).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            End If
        Catch ex As SqlException
            lblErrorCosto.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorCosto.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.Height = 370
            Ficha.ActiveTabIndex = 0
            Call LlenaComboItem("TBOPC327", cboEstabSeccion, psCnGrEmp)
            cboEstabSeccion.Items.Add("< Seleccionar >")
            cboEstabSeccion.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 370
        Ficha.ActiveTabIndex = 0
        lblIngresoCentroCostos.Visible = False
    End Sub
    Sub Llenar_Seccion()
        Dim obj As New clsInv_Listados
        Dim CodCentroCosto As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If txtCodigoCostosSecc.Text.Trim <> "" Then CodCentroCosto = txtCodigoCostosSecc.Text
        Try
            FlexCostoSeccion.DataSource = obj.Lista_xCentroCostos(psConexion, Session("CodEmpresa"), CodCentroCosto)
            FlexCostoSeccion.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnNuevoSeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblEtiqSeccion.Text = "Nueva Sección"
        lblIngresoSeccion.Visible = True
        Ficha.Height = 470
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoCentroCostos.Visible = True
        Ficha.Height = 470
        lblEtiqCentroCosto.Text = "Nuevo Centro de Costo"
    End Sub
    Protected Sub btnCancelarCentroCostos_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoCentroCostos.Visible = False
        Call Limpiar_CentroCostos()
    End Sub
    Protected Sub btnCancelarSeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoSeccion.Visible = False
        LblRegistroSeccion.Visible = False
        Call Limpiar_Seccion()
    End Sub
    Protected Sub btnGrabarSeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New Insertar
        Dim objList As New clsInspeccion_Listado
        Dim objLogis_Upd As New clsLogis_InsUpDel
        Dim objInv_lis As New clsInv_Listados
        Dim objInsUpdDel As New clsInspeccion_InsUpdDel
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim codCentroCostos As Double = 0
        Dim codSeccion As Double = 0
        Dim psTta As String = ""
        Dim psTsi As String = ""
        codSeccion = Nz(txtCodigoCostosSecc.Text.Trim)
        codCentroCostos = Nz(txtCodigoSeccion.Text.Trim)
        Try
            If lblEtiqSeccion.Text = "Nueva Sección" Then
                If cboTta.SelectedValue <> "< Seleccionar >" Then psTta = cboTta.SelectedValue.Trim Else psTta = ""
                If cboTsi.SelectedValue <> "< Seleccionar >" Then psTsi = cboTsi.SelectedValue.Trim Else psTsi = ""
                objLogis_Upd.Insertar_Costos_Seccion(psConexion, Session("CodEmpresa"), 0, codSeccion, _
                                            txtCodSeccion.Text.Trim, txtDescCostosSeccion.Text.Trim, txtRucCostosSeccion.Text.Trim, _
                                            cboEstabSeccion.SelectedValue, txtDireccSeccion.Text.Trim, txtPisoSeccion.Text.Trim, _
                                            txtEdificioSeccion.Text.Trim, txtUbicaSeccion.Text.Trim, txtHallSeccion.Text.Trim, psTta, psTsi)
            Else
                If cboTta.SelectedValue <> "< Seleccionar >" Then psTta = cboTta.SelectedValue.Trim Else psTta = ""
                If cboTsi.SelectedValue <> "< Seleccionar >" Then psTsi = cboTsi.SelectedValue.Trim Else psTsi = ""
                objLogis_Upd.Update_Seccion(psConexion, Session("CodEmpresa"), codCentroCostos, txtCodSeccion.Text.Trim, _
                                             txtDescCostosSeccion.Text.Trim, txtRucCostosSeccion.Text.Trim, cboEstabSeccion.SelectedValue.Trim, _
                                             txtDireccSeccion.Text.Trim, txtPisoSeccion.Text.Trim, txtEdificioSeccion.Text.Trim, _
                                             txtUbicaSeccion.Text.Trim, txtHallSeccion.Text.Trim, psTta, psTsi)
            End If
            Call Limpiar_Seccion()
            lblIngresoSeccion.Visible = False
            FlexCostoSeccion.DataSource = objInv_lis.Lista_xCentroCostos(psConexion, Session("CodEmpresa"), codSeccion)
            FlexCostoSeccion.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnGrabarCentroCostos_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New Insertar
        Dim objLogisUpd As New clsLogis_InsUpDel
        Dim codCentroCostos As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        codCentroCostos = Nz(txtCodCostos.Text.Trim)
        Try
            If lblEtiqCentroCosto.Text = "Nuevo Centro de Costo" Then
                objLogisUpd.Insertar_Centro_Costos(psConexion, Session("CodEmpresa"), codCentroCostos, _
                        txtCodCentroCostos.Text.Trim, txtDescripCentroCostos.Text.Trim, txtPisoCentroCosto.Text.Trim, _
                        txtDireccCentroCosto.Text.Trim, txtEdificioCentroCosto.Text.Trim, txtUbicaCentroCosto.Text.Trim, _
                        txtRucCentroCosto.Text.Trim)
            ElseIf lblEtiqCentroCosto.Text = "Editar Centro de Costo" Then
                objLogisUpd.Update_CentroCostos(psConexion, Session("CodEmpresa"), codCentroCostos, txtCodCentroCostos.Text.Trim, _
                                                txtDescripCentroCostos.Text.Trim, txtPisoCentroCosto.Text.Trim, txtDireccCentroCosto.Text.Trim, _
                                                txtEdificioCentroCosto.Text.Trim, txtUbicaCentroCosto.Text.Trim, txtRucCentroCosto.Text.Trim)
            End If
            Call btnListar_Click(sender, e)
            Call Limpiar_CentroCostos()
            lblIngresoCentroCostos.Visible = False
        Catch ex As SqlException
            lblErrorCosto.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorCosto.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Sub Limpiar_CentroCostos()
        Ficha.Height = 370
        txtCodCentroCostos.Text = ""
        txtDescripCentroCostos.Text = ""
        txtPisoCentroCosto.Text = ""
        txtDireccCentroCosto.Text = ""
        txtEdificioCentroCosto.Text = ""
        txtUbicaCentroCosto.Text = ""
        txtRucCentroCosto.Text = ""
    End Sub
    Sub Limpiar_Seccion()
        txtCodigoSeccion.Text = ""
        Ficha.Height = 320
        txtCodSeccion.Text = ""
        txtDescCostosSeccion.Text = ""
        txtRucCostosSeccion.Text = ""
        cboEstabSeccion.SelectedValue = "< Seleccionar >"
        txtDireccSeccion.Text = ""
        txtPisoSeccion.Text = ""
        txtEdificioSeccion.Text = ""
        txtUbicaSeccion.Text = ""
        txtHallSeccion.Text = ""
        cboTta.SelectedValue = "< Seleccionar >"
        cboTsi.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub FlexCostoSeccion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexCostoSeccion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Editar" Then
            lblEtiqSeccion.Text = "Editar Sección"
            lblIngresoSeccion.Visible = True
            Ficha.Height = 470
            txtCodigoSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtCodSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDescCostosSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtRucCostosSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtEdificioSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtUbicaSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtHallSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtPisoSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDireccSeccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") <> "" Then
                cboTta.SelectedValue = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Else
                cboTta.SelectedValue = "< Seleccionar >"
            End If
            If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") <> "" Then
                cboTsi.SelectedValue = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Else
                cboTsi.SelectedValue = "< Seleccionar >"
            End If
            If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") <> "" Then
                cboEstabSeccion.SelectedValue = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCostoSeccion.Rows(Index).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Else
                cboEstabSeccion.SelectedValue = "< Seleccionar >"
            End If
        End If
    End Sub
    Protected Sub btnBusCListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            Dim obj As New clsInv_Listados
            FlexBusCCosto.DataSource = Nothing
            FlexBusCCosto.DataBind()
            FlexBusCCosto.DataSource = obj.Listar_Centro_Costos(psConexion, Session("CodEmpresa"), txtBusCCod.Text.Trim, txtBusCDescripcion.Text.Trim)
            FlexBusCCosto.DataBind()
            ModalPopupExtender1.Show()
        Catch ex As SqlException
            lblErrorCosto.Text = ex.Message
        Catch ex As Exception
            lblErrorCosto.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexBusCCosto_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexBusCCosto.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtCodInterno.Text = ""
            txtCodigoCostos.Text = ""
            txtDescripcion.Text = ""
            txtCodInterno.Text = FlexBusCCosto.Rows(Index).Cells(1).Text
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexBusCCosto.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtCodigoCostos.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexBusCCosto.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexBusCCosto.DataSource = Nothing
            FlexBusCCosto.DataBind()
            ModalPopupExtender1.Hide()
        End If
    End Sub
    Protected Sub txtCodInterno_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCodInterno.TextChanged
        txtCodInterno.Text = ""
        txtCodigoCostos.Text = ""
        txtDescripcion.Text = ""
    End Sub
End Class
