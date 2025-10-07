Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Servicios_SIntegral_Servicio_Proveedor
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.TabIndex = "0" Then
            Call LlenaComboItem("TBOPC414", cboBusSecEconomico)
            Call LlenaComboItem("TBOPC414", cboSecEconomico)
            lblEtqRelacionar.Visible = True
            txtProveedor.Enabled = False
            cboSecEconomico.Enabled = False
            btnBusProveedor.Enabled = False
            btnGuardar.Enabled = False
            btnCancelar.Enabled = False
            btnIngListar.Enabled = False
            lblEtq2.Enabled = False
            lblEtq3.Enabled = False
            lblEtq4.Enabled = False
        End If
    End Sub
    Protected Sub btnListarProvee_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        Call Listar_Proveedor()
        ModalPopupExtender1.Show()
    End Sub
    Private Sub Listar_Proveedor()
        'lblError.Text = ""
        'Dim obj As New clsServicio
        'Dim dt As DataTable
        'Try
        '    dt = obj.Listar_Proveedor(Session("Ruta_Emp"), Session("CodEmpresa"), txtBusApePat.Text.Trim)
        '    FlexP.DataSource = dt
        '    FlexP.DataBind()
        '    'lblRegistro.Text = "Se encontrarón " & dt.Rows.Count & " registros."
        '    dt = Nothing
        'Catch ex As SqlException
        '    lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        'Catch ex As Exception
        '    lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        'End Try
    End Sub
    Protected Sub btnBusCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        txtCodProveedor.Text = ""
        txtProveedor.Text = ""
        cboSecEconomico.SelectedValue = "< Seleccionar >"
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        FlexServicio.DataSource = Nothing
        FlexServicio.DataBind()
        txtProveedor.Enabled = False '
        cboSecEconomico.Enabled = False
        btnBusProveedor.Enabled = False
        btnGuardar.Enabled = False
        btnCancelar.Enabled = False
        btnIngListar.Enabled = False
        lblEtqRelacionar.Visible = True
        lblEtq2.Enabled = False
        lblEtq3.Enabled = False
        lblEtq4.Enabled = False
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'lblError.Text = ""
        'Dim obj As New clsServicio
        'Dim dt As DataTable
        'Dim psTipoSector As String = ""
        'Try
        '    If cboBusSecEconomico.SelectedValue = "< Seleccionar >" Then
        '        psTipoSector = ""
        '    Else
        '        psTipoSector = cboBusSecEconomico.SelectedValue.Trim
        '    End If
        '    dt = obj.Listar_Relacion(Session("Ruta_Emp"), Session("CodEmpresa"), psTipoSector)
        '    Flex.DataSource = dt
        '    Flex.DataBind()
        '    dt = Nothing
        '    ModalPopupExtender1.Hide()
        'Catch ex As SqlException
        '    lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        'Catch ex As Exception
        '    lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        'End Try
    End Sub
    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        lblEtqRelacionar.Visible = True
        cboSecEconomico.SelectedValue = "< Seleccionar >"
        txtProveedor.Text = ""
        txtCodProveedor.Text = ""
        FlexServicio.DataSource = Nothing
        FlexServicio.DataBind()
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        txtBusApePat.Text = ""
        txtProveedor.Enabled = True
        cboSecEconomico.Enabled = True
        btnBusProveedor.Enabled = True
        btnGuardar.Enabled = True
        btnCancelar.Enabled = True
        btnIngListar.Enabled = True
        lblEtq2.Enabled = True
        lblEtq3.Enabled = True
        lblEtq4.Enabled = True
    End Sub
    Protected Sub btnIngListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'lblError.Text = ""
        'Dim obj As New clsServicio
        'Dim dt As DataTable
        'Dim psTipoSector As String = ""
        'Try
        '    If cboSecEconomico.SelectedValue = "< Seleccionar >" Then
        '        psTipoSector = ""
        '    Else
        '        psTipoSector = cboSecEconomico.SelectedValue.Trim
        '    End If
        '    dt = obj.Listar_Servicio(Session("Ruta_Emp"), Session("CodEmpresa"), psTipoSector)
        '    FlexServicio.DataSource = dt
        '    FlexServicio.DataBind()
        '    dt = Nothing
        '    Call Marcar_Servicio()
        'Catch ex As SqlException
        '    lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        'Catch ex As Exception
        '    lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        'End Try
    End Sub
    Protected Sub FlexP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Aceptar" Then
            txtCodProveedor.Text = FlexP.Rows(Index).Cells(1).Text
            txtProveedor.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtProveedor.Text = txtProveedor.Text & " " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtProveedor.Text = txtProveedor.Text & ", " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            FlexServicio.DataSource = Nothing
            FlexServicio.DataBind()
            txtBusApePat.Text = ""
            FlexP.DataSource = Nothing
            FlexP.DataBind()
            ModalPopupExtender1.Hide()
        End If
    End Sub
    Private Sub Marcar_Servicio()
        'lblError.Text = ""
        'Dim Check As CheckBox
        'Dim CodProveedor As Double = 0
        'Dim i As Integer
        'Dim dt As New Data.DataTable
        'CodProveedor = txtCodProveedor.Text.Trim
        'Try
        '    Dim obj As New clsServicio
        '    dt = obj.Listar_Servicio_xProveedor(Session("Ruta_Emp"), Session("CodEmpresa"), CodProveedor)
        '    For Each dr As Data.DataRow In dt.Rows
        '        For i = 0 To FlexServicio.Rows.Count - 1
        '            If FlexServicio.Rows(i).Cells(3).Text = dr("SERVICIO_CODIGO").ToString Then
        '                Check = CType(FlexServicio.Rows(i).Cells(1).FindControl("chkServ"), CheckBox)
        '                Check.Checked = True
        '                Check.Enabled = False
        '            End If
        '        Next
        '    Next
        '    dt = Nothing
        'Catch ex As SqlException
        '    lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        'Catch ex As Exception
        '    lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        'End Try
    End Sub

    Protected Sub FlexP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim obj As New clsServicio
        'Dim Servicio As CheckBox
        'Dim CodProveedor As Double = 0
        'Dim CodServicio As Double = 0
        'Dim i As Long = 0
        'Dim a As Long = 0
        'Try
        '    If txtCodProveedor.Text.Trim = "" And txtProveedor.Text.Trim = "" Then lblError.Text = "<br> - Ingresar el Proveedor."
        '    CodProveedor = txtCodProveedor.Text.Trim
        '    For i = 0 To FlexServicio.Rows.Count - 1
        '        Servicio = FlexServicio.Rows(i).Cells(1).FindControl("chkServ")
        '        If Servicio.Checked = True And Servicio.Enabled = True Then a = 1 : Exit For
        '    Next
        '    If a = 0 Then lblError.Text = lblError.Text & "<br> - Debe de marcar al menos un Servicio."
        '    If lblError.Text.Trim <> "" Then
        '        lblError.Text = "Se han encontrado las sgtes. observaciones: " & lblError.Text.Trim
        '        Exit Sub
        '    End If
        '    For i = 0 To FlexServicio.Rows.Count - 1
        '        Servicio = FlexServicio.Rows(i).Cells(1).FindControl("chkServ")
        '        If Servicio.Checked = True And Servicio.Enabled = True Then
        '            CodServicio = FlexServicio.Rows(i).Cells(3).Text
        '            obj.Ins_Servicio_xProveedor(Session("Ruta_Emp"), Session("CodEmpresa"), CodServicio, CodProveedor)
        '        End If
        '    Next
        '    btnCancelar_Click(sender, e)
        '    btnListar_Click(sender, e)
        'Catch ex As SqlException
        '    lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        'Catch ex As Exception
        '    lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        'End Try
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        'Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        'lblError.Text = ""
        'Dim obj As New clsServicio
        'Dim CodProveedor As Double = 0
        'Dim CodServicio As Double = 0
        'Try
        '    If e.CommandName = "Quitar" Then
        '        CodServicio = Flex.Rows(Index).Cells(6).Text
        '        CodProveedor = Flex.Rows(Index).Cells(1).Text
        '        obj.Del_Servicio_xProveedor(Session("Ruta_Emp"), Session("CodEmpresa"), CodServicio, CodProveedor)
        '        btnListar_Click(sender, e)
        '    End If
        'Catch ex As SqlException
        '    lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        'Catch ex As Exception
        '    lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        'End Try
    End Sub
End Class
