Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Servicios_SIntegral_Servicio
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Listar_Servicio()
    End Sub
    Private Sub Listar_Servicio()
        Try
            Dim obj As New clsSIntegral
            Dim pdSector As Double = 0
            Dim pdTipo As Double = 0
            Dim dt As DataTable
            If cboSector.SelectedValue <> "< Seleccionar >" Then pdSector = cboSector.SelectedValue.Trim
            If cboTipo.SelectedValue <> "< Seleccionar >" Then pdTipo = cboTipo.SelectedValue.Trim
            dt = obj.Listar_DetalleServicio(Session("Ruta_Emp"), Session("CodEmpresa"), pdSector, pdTipo)
            Flex.DataSource = dt
            Flex.DataBind()
            If dt.Rows.Count > 0 Then
                lblRegistro.Text = "Se encontrarón " & dt.Rows.Count & " registros"
            Else
                lblRegistro.Text = "No hay Registros."
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:<br> " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:<br> " & ex.Message
        End Try
    End Sub
    Protected Sub cboSector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSector.SelectedIndexChanged
        lblError.Visible = False
        cboTipo.Items.Clear()
        cboTipo.Enabled = False
        If cboSector.SelectedIndex = -1 Or cboSector.Items.Count = 0 Then Exit Sub
        If cboSector.SelectedValue = "< Seleccionar >" Then Exit Sub
        Call LLenaComboItemTabEsp(cboTipo, cboSector.SelectedValue.Trim, "", "TBESP_SER1", "TBESP_SER2", "TBESP_SER3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboSector.SelectedValue = "< Seleccionar >" Then
            cboTipo.Enabled = False
        Else
            cboTipo.Enabled = True
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.TabIndex = "0" Then
            Call LLenaComboItemTabEsp(cboSector, "", "", "TBESP_SER1", "TBESP_SER2", "TBESP_SER3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call cboSector_SelectedIndexChanged(sender, e)
            cboTipo.Items.Add("< Seleccionar >") : cboTipo.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorDet.Text = ""
        If e.CommandName = "Detalle" Then
            Dim dt As DataTable
            Dim obj As New clsSIntegral
            Dim psNroServicio As Double = 0
            txtNroServicio.Text = Flex.Rows(Index).Cells(8).Text
            psNroServicio = txtNroServicio.Text
            dt = obj.Listar_DetalleServicio_XCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), psNroServicio)
            If dt.Rows.Count = 1 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtFecInicia.Text = FormatoFecha(Nu(dr("SERVDET_FECHA_INICIA")))
                    txtFecTermina.Text = FormatoFecha(Nu(dr("SERVDET_FECHA_TERMINA")))
                    txtSectorEconomico.Text = Nu(dr("SECTOR"))
                    txtTipo.Text = Nu(dr("TIPO"))
                    If Nu(dr("TIPO2")) <> "" Then txtTipo.Text = txtTipo.Text & " - " & Nu(dr("TIPO2"))
                    txtDescripcion.Text = Nu(dr("SERVDET_DESCRIPCION"))
                    txtProveedor.Text = Nu(dr("PROVEEDOR"))
                    txtDireccion.Text = Nu(dr("SERVDET_DIRECCION"))
                    If Nu(dr("PPAIS")) <> "" Then txtDireccion.Text = txtDireccion.Text & " - " & Nu(dr("PPAIS"))
                    If Nu(dr("PDPTO")) <> "" Then txtDireccion.Text = txtDireccion.Text & " " & Nu(dr("PDPTO"))
                    If Nu(dr("PPROV")) <> "" Then txtDireccion.Text = txtDireccion.Text & " " & Nu(dr("PPROV"))
                    If Nu(dr("PDIST")) <> "" Then txtDireccion.Text = txtDireccion.Text & " " & Nu(dr("PDIST"))
                    txtObservacion.Text = Nu(dr("SERVDET_OBSERVACION"))
                    txtPrecioProveedor.Text = Nz(dr("SERVDET_PRECIO"))
                    txtEstado.Text = Nu(dr("ESTADO"))
                    If Nu(dr("SERVDET_ESTADO")) <> "1" Then btnPublicar.Enabled = False Else btnPublicar.Enabled = True
                Next
            End If
            dt = Nothing
            txtPrecio.Text = "0.00"
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.TabIndex = "1"
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.TabIndex = "0" : Call Listar_Servicio()
    End Sub
    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub btnPublicar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim psPrecio As Double = 0
            Dim psCodServicio As Double = 0
            psCodServicio = CDbl(txtNroServicio.Text.Trim)
            Dim obj As New clsSIntegral
            obj.Ins_Servicio(Session("Ruta_Emp"), Session("CodEmpresa"), psCodServicio, psPrecio, HttpContext.Current.User.Identity.Name)
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.TabIndex = "0"
        Catch ex As SqlException
            lblErrorDet.Text = "Ha ocurrido un error en la base de datos:<br> " & ex.Message
        Catch ex As Exception
            lblErrorDet.Text = "Ha ocurrido un error en la aplicación:<br> " & ex.Message
        End Try
    End Sub
End Class
