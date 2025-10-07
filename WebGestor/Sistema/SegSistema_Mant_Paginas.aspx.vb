Imports System.Data.SqlClient
Imports WebGestor
Imports System.Data
Partial Class SegSistema_Mant_Paginas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                lblError.Text = ""
                Dim dt As New DataTable
                Dim objSeg As New ModuloSeguridad
                Call Listar_Pagina()
                cboModulo.Items.Clear()
                dt = objSeg.Lista_Modulo()
                cboModulo.DataSource = dt
                cboModulo.DataTextField = "MOD_NOMBRE"
                cboModulo.DataValueField = "MOD_CODIGO"
                cboModulo.DataBind()
                cboModulo.Items.Add("< Seleccionar >") : cboModulo.SelectedValue = "< Seleccionar >"
            Catch ex As SqlException
                lblError.Text = ex.Message
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
                '
            End Try
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Listar_Pagina()
    End Sub
    Private Sub Listar_Pagina()
        Try
            lblError.Text = ""
            Dim objSeg As New ModuloSeguridad
            Flex.DataSource = objSeg.Lista_Pagina
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        lblError.Text = ""
        lblEtiqueta.Text = "Nueva Página"
        lblDefinePagina.Visible = True
        txtPagina.Text = ""
        txtCodPagina.Text = ""
        txtNomPag.Text = ""
        txtDescripcion.Text = ""
        'cboEstado.SelectedValue = "< Seleccionar >"
        'cboDisposicion.SelectedValue = "< Seleccionar >"
        'cboModulo.SelectedValue = "< Seleccionar >"
        'cboTipo.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        lblDefinePagina.Visible = False
        txtPagina.Text = ""
        txtNomPag.Text = ""
        txtCodPagina.Text = ""
        txtDescripcion.Text = ""
        cboEstado.SelectedValue = "< Seleccionar >"
        cboDisposicion.SelectedValue = "< Seleccionar >"
        cboModulo.SelectedValue = "< Seleccionar >"
        cboTipo.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Listar_Pagina()
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Try
            lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Editar" Then
                lblDefinePagina.Visible = True
                lblEtiqueta.Text = "Editar Página"
                txtCodPagina.Text = Flex.Rows(Index).Cells(3).Text
                txtPagina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtNomPag.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                cboTipo.SelectedValue = Flex.Rows(Index).Cells(10).Text
                cboEstado.SelectedValue = Flex.Rows(Index).Cells(8).Text
                cboDisposicion.SelectedValue = Flex.Rows(Index).Cells(9).Text
                cboModulo.SelectedValue = Flex.Rows(Index).Cells(1).Text
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblError.Text = ""
            Dim dt As New DataTable
            Dim objSeg As New ModuloSeguridad
            Dim CodModulo As Double = 0
            Dim CodPagina As Double = 0
            If txtPagina.Text.Trim = "" Then lblError.Text = "Debe de tener un nombre" : Exit Sub
            If cboEstado.SelectedValue = "< Seleccionar >" Then lblError.Text = "Debe de tener un estado" : Exit Sub
            If cboDisposicion.SelectedValue = "< Seleccionar >" Then lblError.Text = "Debe de tener una disposición" : Exit Sub
            If cboModulo.SelectedValue = "< Seleccionar >" Then lblError.Text = "Debe de Seleccionar a que modulo pertenece el Botón" : Exit Sub
            If lblEtiqueta.Text = "Nueva Página" Or (lblEtiqueta.Text = "Editar Página" And UCase(txtNomPag.Text.Trim) <> UCase(txtPagina.Text.Trim)) Then
                dt = objSeg.Existe_Pagina(txtPagina.Text.Trim, "1")
                If dt.Rows.Count = 0 Then
                Else
                    lblError.Text = "El Nombre del Formulario ingresado ya existe, por favor ingresar otro" : Exit Sub
                End If
            End If
            CodModulo = cboModulo.SelectedValue.Trim
            If lblEtiqueta.Text = "Nueva Página" Then
                objSeg.InsUpd_Pagina(CodPagina, txtPagina.Text.Trim, txtDescripcion.Text.Trim, cboEstado.SelectedValue.Trim, cboTipo.SelectedValue.Trim, cboDisposicion.SelectedValue.Trim, "", CodModulo, HttpContext.Current.User.Identity.Name, "1")
            ElseIf lblEtiqueta.Text = "Editar Página" Then
                CodPagina = txtCodPagina.Text.Trim
                objSeg.InsUpd_Pagina(CodPagina, txtPagina.Text.Trim, txtDescripcion.Text.Trim, cboEstado.SelectedValue.Trim, cboTipo.SelectedValue.Trim, cboDisposicion.SelectedValue.Trim, "", CodModulo, HttpContext.Current.User.Identity.Name, "2")
            End If
            Call Listar_Pagina()
            btnCancelar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
End Class
