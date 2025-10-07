Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class AdminProblemas_DefineTablas2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Ficha.ActiveTabIndex = 0
                Ficha_ActiveTabChanged(sender, e)
                Ficha.Height = 310
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Protected Sub btnListarE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarE.Click
        Call Llenar_GrillaE()
    End Sub
    Private Sub Llenar_GrillaE()
        Try
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New clsMesaAyuda
            dtListado = obj.MALista_Empresa(Session("Ruta_Emp"), Session("CodEmpresa"))
            FlexE.DataSource = dtListado
            FlexE.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        Dim obj As New clsMesaAyuda
        If Ficha.ActiveTabIndex = 0 Then
            Call Llenar_GrillaE()
            Ficha.Height = 310
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call Llenar_GrillaO()
            Call obj.MACargar_Empresa(cboEmpresa, Session("Ruta_Emp"), Session("CodEmpresa"))
            Ficha.Height = 310
        End If
        If Ficha.ActiveTabIndex = 2 Then
            Call Llenar_GrillaP()
            Ficha.Height = 310
        End If
        If Ficha.ActiveTabIndex = 3 Then
            Ficha.Height = 310
            Call Llenar_GrillaC()
            Call LlenaComboItem("TBOPC373", cboCTipo)
            cboCInicia.Items.Clear()
            cboCInicia.Items.Add("SI")
            cboCInicia.SelectedValue = "SI"
            cboCInicia.Items.Add("NO")
            cboCInicia.SelectedValue = "NO"
            cboCTipo.Items.Add("< Seleccionar >")
            cboCTipo.SelectedValue = "< Seleccionar >"
            cboCInicia.Items.Add("< Seleccionar >")
            cboCInicia.SelectedValue = "< Seleccionar >"
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnListarO_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Llenar_GrillaO()
    End Sub
    Private Sub Llenar_GrillaO()
        Try
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New clsMesaAyuda
            dtListado = obj.MALista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"))
            FlexO.DataSource = dtListado
            FlexO.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorO.Visible = True
            lblErrorO.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorO.Visible = True
            lblErrorO.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexE_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexE.PageIndexChanging
        lblError.Text = ""
        FlexE.PageIndex = e.NewPageIndex
        Call Llenar_GrillaE()
    End Sub
    Protected Sub FlexO_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexO.PageIndexChanging
        lblErrorO.Text = ""
        FlexO.PageIndex = e.NewPageIndex
        Call Llenar_GrillaO()
    End Sub
    Protected Sub btnListarP_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Llenar_GrillaP()
    End Sub
    Private Sub Llenar_GrillaP()
        Try
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New clsMesaAyuda
            dtListado = obj.MALista_Puesto(Session("Ruta_Emp"), Session("CodEmpresa"))
            FlexP.DataSource = dtListado
            FlexP.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorP.Visible = True
            lblErrorP.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorP.Visible = True
            lblErrorP.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexP_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexP.PageIndexChanging
        lblErrorP.Text = ""
        FlexP.PageIndex = e.NewPageIndex
        Call Llenar_GrillaP()
    End Sub
    Protected Sub btnListarC_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Llenar_GrillaC()
    End Sub
    Private Sub Llenar_GrillaC()
        Try
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New clsMesaAyuda
            dtListado = obj.MALista_Criterio(Session("CodEmpresa"), Session("Ruta_Emp"))
            FlexC.DataSource = dtListado
            FlexC.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorC.Visible = True
            lblErrorC.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorC.Visible = True
            lblErrorC.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub FlexC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexC.PageIndexChanging
        lblErrorC.Text = ""
        FlexC.PageIndex = e.NewPageIndex
        Call Llenar_GrillaC()
    End Sub
    Protected Sub btnENuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoE.Visible = True
        btnENuevo.Enabled = False
        lblEtiquetaE.Text = "Nueva Empresa"
        txtENombre.Text = ""
        txtCodEmpresa.Text = ""
        Ficha.Height = 390
    End Sub
    Protected Sub btnECancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoE.Visible = False
        btnENuevo.Enabled = True
        lblEtiquetaE.Text = ""
        txtENombre.Text = ""
        txtCodEmpresa.Text = ""
        FlexE.Enabled = True
        Ficha.Height = 310
    End Sub
    Protected Sub FlexE_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexE.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiquetaE.Text = "Editar Empresa"
            lblIngresoE.Visible = True
            txtCodEmpresa.Text = FlexE.Rows(Index).Cells(1).Text.Trim
            txtENombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexE.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            FlexE.Enabled = False
            Ficha.Height = 390
        End If
    End Sub
    Protected Sub btnEGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        Dim pCodEmpresa As Double = 0
        Dim dt As New DataTable
        Dim obj As New clsMesaAyuda
        If txtENombre.Text.Trim = "" Then lblError.Text = "Debe ingresar el Nombre de la Empresa." : Exit Sub
        Try
            If lblEtiquetaE.Text = "Nueva Empresa" Then
                dt = obj.MAConsulta_ExisteEmpresa(txtENombre.Text.Trim, Session("Ruta_Emp"), Session("CodEmpresa"))
                If dt.Rows.Count = 0 Then
                    obj.MAInsUpd_Empresa(pCodEmpresa, txtENombre.Text.Trim, HttpContext.Current.User.Identity.Name, "1", Session("Ruta_Emp"), Session("CodEmpresa"))
                Else
                    dt = Nothing
                    lblError.Text = "Nombre de la Empresa ya existe." : Exit Sub
                End If
                dt = Nothing
            ElseIf lblEtiquetaE.Text = "Editar Empresa" Then
                pCodEmpresa = txtCodEmpresa.Text.Trim
                obj.MAInsUpd_Empresa(pCodEmpresa, txtENombre.Text.Trim, HttpContext.Current.User.Identity.Name, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
            End If
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnECancelar_Click(sender, e)
        btnListarE_Click(sender, e)
    End Sub
    Protected Sub btnONuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorO.Text = ""
        lblEtiquetaO.Text = "Nueva Oficina"
        btnONuevo.Enabled = False
        lblIngresoO.Visible = True
        txtONombre.Text = ""
        txtOCodInt.Text = ""
        txtCodOficina.Text = ""
        cboEmpresa.Items.Add("< Seleccionar >")
        cboEmpresa.SelectedValue = "< Seleccionar >"
        Ficha.Height = 420
    End Sub
    Protected Sub btnOCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorO.Text = ""
        lblEtiquetaO.Text = ""
        btnONuevo.Enabled = True
        lblIngresoO.Visible = False
        txtONombre.Text = ""
        txtOCodInt.Text = ""
        txtCodOficina.Text = ""
        cboEmpresa.Items.Add("< Seleccionar >")
        cboEmpresa.SelectedValue = "< Seleccionar >"
        FlexO.Enabled = True
        Ficha.Height = 310
    End Sub
    Protected Sub btnOGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorO.Text = ""
        If txtOCodInt.Text.Trim = "" Then lblErrorO.Text = "Falta ingresar Codigo Interno." : Exit Sub
        If txtONombre.Text.Trim = "" Then lblErrorO.Text = "Falta ingresar Nombre." : Exit Sub
        If cboEmpresa.SelectedValue.Trim = "< Seleccionar >" Then lblErrorO.Text = "Falta ingresar Empresa." : Exit Sub
        Dim pCodOficina As Double = 0
        Dim dt As New DataTable
        Dim obj As New clsMesaAyuda
        Try
            If lblEtiquetaO.Text = "Nueva Oficina" Then
                dt = obj.MAConsulta_ExisteOficina(txtOCodInt.Text.Trim, Session("Ruta_Emp"), Session("CodEmpresa"))
                If dt.Rows.Count = 0 Then
                    obj.MAInsUpd_Oficina(pCodOficina, txtOCodInt.Text.Trim, txtONombre.Text.Trim, cboEmpresa.SelectedValue.Trim, HttpContext.Current.User.Identity.Name, "1", Session("Ruta_Emp"), Session("CodEmpresa"))
                Else
                    lblErrorO.Text = "El Codigo de Oficina ya existe." : Exit Sub
                End If
            Else
                'dt = obj.CasConsulta_ExisteOficina(txtOCodInt.Text.Trim)
                'If dt.Rows.Count = 0 Then
                pCodOficina = txtCodOficina.Text.Trim
                obj.MAInsUpd_Oficina(pCodOficina, txtOCodInt.Text.Trim, txtONombre.Text.Trim, cboEmpresa.SelectedValue.Trim, HttpContext.Current.User.Identity.Name, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
                'Else
                '    lblErrorO.Text = "El Codigo de Oficina ya existes." : Exit Sub
                'End If
            End If
        Catch Ex As SqlException
            lblErrorO.Visible = True
            lblErrorO.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorO.Visible = True
            lblErrorO.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnListarO_Click(sender, e)
        btnOCancelar_Click(sender, e)
    End Sub
    Protected Sub FlexO_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexO.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorO.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiquetaO.Text = "Editar Oficina"
            lblIngresoO.Visible = True
            txtOCodInt.Text = FlexO.Rows(Index).Cells(1).Text.Trim
            txtONombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexO.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtCodOficina.Text = FlexO.Rows(Index).Cells(4).Text.Trim
            cboEmpresa.SelectedValue = FlexO.Rows(Index).Cells(5).Text.Trim
            FlexO.Enabled = False
            Ficha.Height = 420
        End If
    End Sub
    Protected Sub btnPNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorP.Text = ""
        lblEtiquetaP.Text = "Nuevo Puesto"
        btnPNuevo.Enabled = False
        lblIngresoP.Visible = True
        txtPNombre.Text = ""
        txtPCodInterno.Text = ""
        txtCodPuesto.Text = ""
        Ficha.Height = 390
    End Sub
    Protected Sub btnPCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorP.Text = ""
        lblEtiquetaP.Text = ""
        btnPNuevo.Enabled = True
        lblIngresoP.Visible = False
        txtPNombre.Text = ""
        txtCodPuesto.Text = ""
        FlexP.Enabled = True
        Ficha.Height = 310
    End Sub
    Protected Sub btnPGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            btnPNuevo.Enabled = True
            Dim pcodigo As Double = 0
            Dim obj As New clsMesaAyuda
            Dim dt As New DataTable
            If txtPNombre.Text.Trim = "" Then lblErrorP.Text = "Ingresar Nombre." : Exit Sub
            If lblEtiquetaP.Text = "Nuevo Puesto" Then
                dt = obj.MAConsulta_ExistePuetso(txtPNombre.Text.Trim, txtPCodInterno.Text.Trim, "1", Session("Ruta_Emp"), Session("CodEmpresa"))
                If dt.Rows.Count = 0 Then
                    obj.MAInsUpd_Puesto(pcodigo, txtPNombre.Text.Trim, txtPCodInterno.Text.Trim, "1", Session("Ruta_Emp"), Session("CodEmpresa"))
                Else
                    dt = Nothing
                    lblErrorP.Text = "El Nombre del Oficina ya existe." : Exit Sub
                End If
                dt = Nothing
            ElseIf lblEtiquetaP.Text = "Editar Puesto" Then
                pcodigo = txtCodPuesto.Text.Trim
                obj.MAInsUpd_Puesto(pcodigo, txtPNombre.Text.Trim, txtPCodInterno.Text.Trim, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
            End If
        Catch Ex As SqlException
            lblErrorP.Visible = True
            lblErrorP.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorP.Visible = True
            lblErrorP.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnListarP_Click(sender, e)
        btnPCancelar_Click(sender, e)
    End Sub
    Protected Sub FlexP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorP.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiquetaP.Text = "Editar Puesto"
            lblIngresoP.Visible = True
            txtPNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtPCodInterno.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtCodPuesto.Text = FlexP.Rows(Index).Cells(1).Text.Trim
            FlexP.Enabled = False
            Ficha.Height = 390
        End If
    End Sub
    Protected Sub btnCNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorC.Text = ""
        lblEtiquetaC.Text = "Nuevo Criterio"
        btnCNuevo.Enabled = False
        lblIngresoC.Visible = True
        txtCDescripcion.Text = ""
        txtCodCriterio.Text = ""
        cboCTipo.SelectedValue = "< Seleccionar >"
        cboCInicia.SelectedValue = "< Seleccionar >"
        Ficha.Height = 420
    End Sub
    Protected Sub btnCCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCCancelar.Click
        lblErrorC.Text = ""
        lblEtiquetaC.Text = ""
        btnCNuevo.Enabled = True
        lblIngresoC.Visible = False
        txtCDescripcion.Text = ""
        txtCodCriterio.Text = ""
        cboCTipo.SelectedValue = "< Seleccionar >"
        cboCInicia.SelectedValue = "< Seleccionar >"
        FlexC.Enabled = True
        Ficha.Height = 310
    End Sub
    Protected Sub btnCGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim Inicia As String = ""
            Dim obj As New clsMesaAyuda
            Dim pCodigo As Double = 0
            Dim dt As DataTable
            If txtCDescripcion.Text.Trim = "" Then lblErrorC.Text = "Ingrese Descrripción." : Exit Sub
            If cboCTipo.SelectedValue = "< Seleccionar >" Then lblErrorC.Text = "Seleccione Tipo." : Exit Sub
            If cboCInicia.SelectedValue = "< Seleccionar >" Then lblErrorC.Text = "Seleccione donde Iniciar." : Exit Sub
            If cboCInicia.SelectedValue <> "< Seleccionar >" Then
                If cboCInicia.SelectedValue = "SI" Then Inicia = "S" Else Inicia = "N"
            End If
            If lblEtiquetaC.Text = "Nuevo Criterio" Then
                dt = obj.MAConsulta_ExisteCriterio(txtCDescripcion.Text.Trim, cboCTipo.SelectedValue.Trim, Session("Ruta_Emp"), Session("CodEmpresa"))
                If dt.Rows.Count = 0 Then
                    obj.MAInsUpd_Criterio(Session("CodEmpresa"), cboCTipo.SelectedValue.Trim, pCodigo, txtCDescripcion.Text.Trim, Inicia, "1", Session("Ruta_Emp"))
                Else
                    dt = Nothing
                    lblErrorC.Text = "Criterio ya existe." : Exit Sub
                End If
            ElseIf lblEtiquetaC.Text = "Editar Criterio" Then
                pCodigo = txtCodCriterio.Text.Trim
                obj.MAInsUpd_Criterio(Session("CodEmpresa"), cboCTipo.SelectedValue.Trim, pCodigo, txtCDescripcion.Text.Trim, Inicia, "2", Session("Ruta_Emp"))
            End If
        Catch Ex As SqlException
            lblErrorC.Visible = True
            lblErrorC.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorC.Visible = True
            lblErrorC.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnListarC_Click(sender, e)
        btnCCancelar_Click(sender, e)
    End Sub
    Protected Sub FlexC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorC.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiquetaC.Text = "Editar Criterio"
            lblIngresoC.Visible = True
            txtCDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexC.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtCodCriterio.Text = FlexC.Rows(Index).Cells(2).Text.Trim
            cboCTipo.SelectedValue = FlexC.Rows(Index).Cells(5).Text.Trim
            cboCInicia.SelectedValue = IIf(FlexC.Rows(Index).Cells(4).Text.Trim = "S", "SI", IIf(FlexC.Rows(Index).Cells(4).Text.Trim = "N", "NO", ""))
            FlexC.Enabled = False
            Ficha.Height = 420
        End If
    End Sub
End Class
