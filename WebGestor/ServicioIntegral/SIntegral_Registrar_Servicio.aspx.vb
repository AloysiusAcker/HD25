Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class ServicioIntegral_SIntegral_Registrar_Servicio
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call LlenaComboItem("TBOPC001", cboTipoPer)
            Call LLenaComboItemTabEsp(cboSector, "", "", "TBESP_SER1", "TBESP_SER2", "TBESP_SER3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call cboSector_SelectedIndexChanged(sender, e)
            cboTipo.Items.Add("< Seleccionar >") : cboTipo.SelectedValue = "< Seleccionar >"
            cboTipo2.Items.Add("< Seleccionar >") : cboTipo2.SelectedValue = "< Seleccionar >"
            cboTipo.Enabled = False
            cboTipo2.Enabled = False
            cboPais.Items.Clear()
            Call LlenaComboItem("TBOPC006", cboPais) : cboPais.Items.Add("< Seleccionar >") : cboPais.SelectedValue = "< Seleccionar >"
            If cboPais.Items.Count > 0 Then cboPais.SelectedValue = "51" : cboPais_SelectedIndexChanged(sender, e)
            Call Limpiar(sender, e)
        End If
    End Sub
    Private Sub Limpiar(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        cboTipoPer.SelectedValue = "< Seleccionar >"
        cboSector.SelectedValue = "< Seleccionar >"
        cboTipo.SelectedValue = "< Seleccionar >"
        cboTipo2.SelectedValue = "< Seleccionar >"
        cboTipo.Enabled = False
        cboTipo2.Enabled = False
        txtProveedor.Text = ""
        txtCodProveedor.Text = ""
        txtDescripcion.Text = ""
        txtDireccion.Text = ""
        txtObservacion.Text = ""
        cboPais.SelectedValue = "51" : cboPais_SelectedIndexChanged(sender, e)
        cboDpto.SelectedValue = "< Seleccionar >"
        cboProv.SelectedValue = "< Seleccionar >"
        cboDist.SelectedValue = "< Seleccionar >"
        txtFecInicia.Text = FormatoFecha(FechaActual)
        txtFecTermina.Text = FormatoFecha(FechaActual)
        txtPrecio.Text = "0.00"
    End Sub
    Protected Sub cboSector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSector.SelectedIndexChanged
        lblError.Visible = False
        cboTipo.Items.Clear()
        cboTipo2.Items.Clear()
        cboTipo.Enabled = False
        cboTipo2.Enabled = False
        If cboSector.SelectedIndex = -1 Or cboSector.Items.Count = 0 Then Exit Sub
        If cboSector.SelectedValue = "< Seleccionar >" Then Exit Sub
        Call LLenaComboItemTabEsp(cboTipo, cboSector.SelectedValue.Trim, "", "TBESP_SER1", "TBESP_SER2", "TBESP_SER3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboSector.SelectedValue = "< Seleccionar >" Then
            cboTipo.Enabled = False
            cboTipo2.Enabled = False
            cboTipo2.Items.Add("< Seleccionar >") : cboTipo2.SelectedValue = "< Seleccionar >"
        Else
            cboTipo.Enabled = True
            cboTipo2.Enabled = False
            cboTipo2.Items.Add("< Seleccionar >") : cboTipo2.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub cboTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipo.SelectedIndexChanged
        lblError.Visible = False
        cboTipo2.Items.Clear()
        cboTipo2.Enabled = False
        If cboTipo.SelectedIndex = -1 Or cboTipo.Items.Count = 0 Then Exit Sub
        If cboTipo.Items(cboTipo.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboTipo2, cboSector.SelectedValue.Trim, cboTipo.SelectedValue.Trim, "TBESP_SER1", "TBESP_SER2", "TBESP_SER3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboTipo.SelectedValue = "< Seleccionar >" Then
            cboTipo2.Enabled = False
        Else
            cboTipo2.Enabled = True
        End If
    End Sub
    Protected Sub cboPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPais.SelectedIndexChanged
        Try
            lblError.Text = ""
            cboDpto.Items.Clear()
            cboProv.Items.Clear()
            cboDist.Items.Clear()
            cboDpto.Enabled = False
            cboProv.Items.Add("< Seleccionar >") : cboProv.SelectedValue = "< Seleccionar >"
            cboProv.Enabled = False
            cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
            cboDist.Enabled = False
            If cboPais.SelectedValue = "< Seleccionar >" Then Exit Sub
            If cboPais.SelectedIndex = -1 Or cboPais.Items.Count = 0 Then Exit Sub
            If cboPais.SelectedValue = "51" Then
                Call LlenaComboItem("TBOPC002", cboDpto)
                cboDpto.Items.Add("< Seleccionar >") : cboDpto.SelectedValue = "< Seleccionar >"
                cboDpto.Enabled = True
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDpto.SelectedIndexChanged
        cboProv.Items.Clear()
        cboDist.Items.Clear()
        cboProv.Enabled = False
        cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        cboDist.Enabled = False
        If cboDpto.SelectedIndex = -1 Or cboDpto.Items.Count = 0 Then Exit Sub
        If cboDpto.Items(cboDpto.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC003", cboProv, Left(cboDpto.SelectedValue, 2), "PR")
        If cboDpto.SelectedValue <> "< Seleccionar >" Then cboProv.Enabled = True
    End Sub
    Protected Sub cboProv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProv.SelectedIndexChanged
        cboDist.Items.Clear()
        cboDist.Enabled = False
        cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        If cboProv.SelectedIndex = -1 Or cboProv.Items.Count = 0 Then Exit Sub
        If cboProv.Items(cboProv.SelectedIndex).Value = "0" Then Exit Sub
        Call LlenaComboItem2("TBOPC004", cboDist, Left(cboDpto.SelectedValue, 2) + Mid(cboProv.SelectedValue, 3, 2), "DS")
        cboDist.Items.Add("< Seleccionar >") : cboDist.SelectedValue = "< Seleccionar >"
        If cboProv.SelectedValue <> "< Seleccionar >" Then cboDist.Enabled = True
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Call Limpiar(sender, e)
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        lblError.Text = ""
        lblProveedor.Visible = True
        txtBusApePat.Text = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        cboTipoPer.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        lblError.Text = ""
        Call Listar_Proveedor()
    End Sub
    Private Sub Listar_Proveedor()
        lblError.Text = ""
        Dim obj As New clsSIntegral
        Dim dt As DataTable
        Dim psTipoPer As String = ""
        Try
            If cboTipoPer.SelectedValue <> "< Seleccionar >" Then
                psTipoPer = cboTipoPer.SelectedValue.Trim
            End If
            dt = obj.Listar_Proveedor(Session("Ruta_Emp"), Session("CodEmpresa"), txtBusApePat.Text.Trim, psTipoPer)
            Flex.DataSource = dt
            Flex.DataBind()
            dt = Nothing
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        lblError.Text = ""
        txtBusApePat.Text = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        cboTipoPer.SelectedValue = "< Seleccionar >"
        lblProveedor.Visible = False
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Aceptar" Then
            txtCodProveedor.Text = Flex.Rows(Index).Cells(3).Text
            txtProveedor.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtBusApePat.Text = ""
            Flex.DataSource = Nothing
            Flex.DataBind()
            cboTipoPer.SelectedValue = "< Seleccionar >"
            lblProveedor.Visible = False
        End If
    End Sub
End Class
