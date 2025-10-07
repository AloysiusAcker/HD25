Imports System.Data.SqlClient
Imports WebGestor
Imports System.Data
Partial Class SegSistema_Mant_Perfiles
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.Height = 300
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Try
            Ficha.Height = 420
            lblPEtiqueta.Text = "Ingresar Nuevo Perfil"
            lblPUError.Text = ""
            txtPCodUnico.Text = ""
            txtPDescripcion.Text = ""
            txtCodPerfil.Text = ""
            lblDefinePerfil.Visible = True
            Call Llenar_Datos()
            cboGrpoEmp_SelectedIndexChanged(sender, e)
            cboGrpoEmp.Items.Add("< Seleccionar >") : cboGrpoEmp.SelectedValue = "< Seleccionar >"
            cboEmp.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
            cboModInteg.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblPUError.Text = ex.Message
        Catch ex As Exception
            lblPUError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Llenar_Datos()
        Dim objSeg As New ModuloSeguridad
        cboGrpoEmp.Items.Clear() : cboEmp.Items.Clear() : cboModInteg.Items.Clear()
        cboGrpoEmp.DataSource = objSeg.Lista_GrupoEmpresa(HttpContext.Current.User.Identity.Name, "1")
        cboGrpoEmp.DataTextField = "GE_NOMBRE"
        cboGrpoEmp.DataValueField = "GRPOEMPRESA_CODIGO"
        cboGrpoEmp.DataBind()
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub FlexPU_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexPU.PageIndexChanging
        'lblPUError.Text = ""
        'FlexPU.PageIndex = e.NewPageIndex
        'Call Lista_Perfiles()
    End Sub
    Protected Sub FlexPU_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPU.RowCommand
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodUnicoPerfil As String : CodUnicoPerfil = ""
            Dim CodModInteg As String : CodModInteg = ""
            Dim CodPerfil As String : CodPerfil = ""
            Dim Perfil As String : Perfil = ""
            If e.CommandName = "AsignarPag" Then
                txtModIntegAP.Text = FlexPU.Rows(Index).Cells(9).Text
                txtCodUnicoAP.Text = FlexPU.Rows(Index).Cells(10).Text
                txtAPCodPerfil.Text = FlexPU.Rows(Index).Cells(5).Text
                txtAPPerfil.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexPU.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                Ficha.Height = 300
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
                Ficha_ActiveTabChanged(sender, e)
            ElseIf e.CommandName = "Editar" Then
                Ficha.Height = 420
                lblDefinePerfil.Visible = True
                lblPEtiqueta.Text = "Editar Perfil"
                Call Llenar_Datos()
                txtCodPerfil.Text = FlexPU.Rows(Index).Cells(10).Text
                txtPCodUnico.Text = FlexPU.Rows(Index).Cells(5).Text
                txtPDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexPU.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                cboGrpoEmp.SelectedValue = FlexPU.Rows(Index).Cells(7).Text : cboGrpoEmp_SelectedIndexChanged(sender, e)
                cboEmp.SelectedValue = FlexPU.Rows(Index).Cells(8).Text
                cboModInteg.SelectedValue = FlexPU.Rows(Index).Cells(9).Text
            End If
        Catch ex As SqlException
            lblPUError.Text = ex.Message
        Catch ex As Exception
            lblPUError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Private Sub Lista_Perfiles()
        Try
            Dim objSeg As New ModuloSeguridad
            FlexPU.DataSource = objSeg.Lista_Perfiles(HttpContext.Current.User.Identity.Name, "2")
            FlexPU.DataBind()
        Catch ex As SqlException
            lblPUError.Text = ex.Message
        Catch ex As Exception
            lblPUError.Text = ex.Message
        Finally
            '
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Lista_Perfiles()
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call Listar_PagxPerfil()
            Call Marcar_Pag()
        End If
    End Sub
    Protected Sub cboGrpoEmp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrpoEmp.SelectedIndexChanged
        Try
            lblPUError.Text = ""
            Dim objSeg As New ModuloSeguridad
            Dim CodGrupoEmp As Double
            If cboGrpoEmp.SelectedValue = "< Seleccionar >" Then
                cboEmp.Enabled = False : cboModInteg.Enabled = False
            Else
                CodGrupoEmp = cboGrpoEmp.SelectedValue.Trim
                cboEmp.Items.Clear() : cboModInteg.Items.Clear()
                cboEmp.DataSource = objSeg.Lista_Empresa(HttpContext.Current.User.Identity.Name, CodGrupoEmp, "1")
                cboEmp.DataTextField = "GEE_NOMBRE"
                cboEmp.DataValueField = "EMPRESA_CODIGO"
                cboEmp.DataBind()
                cboModInteg.DataSource = objSeg.Lista_ModuloIntegracion("2", CodGrupoEmp)
                cboModInteg.DataTextField = "MODINTEG_NOMBRE"
                cboModInteg.DataValueField = "MODINTEG_CODIGO"
                cboModInteg.DataBind()
                cboEmp.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
                cboModInteg.Items.Add("< Seleccionar >") : cboModInteg.SelectedValue = "< Seleccionar >"
                cboEmp.Enabled = True : cboModInteg.Enabled = True
            End If
        Catch ex As SqlException
            lblPUError.Text = ex.Message
        Catch ex As Exception
            lblPUError.Text = ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnPGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim objSeg As New ModuloSeguridad
            Dim dt As DataTable
            Dim CodModInteg As Double = 0
            Dim CodGrpoEmp As Double = 0
            Dim CodPerfil As Double = 0
            lblPUError.Text = ""
            If cboGrpoEmp.SelectedValue = "< Seleccionar >" Then lblPUError.Text = "Falta seleccionar Grupo de Empresa." : Exit Sub
            If cboEmp.SelectedValue = "< Seleccionar >" Then lblPUError.Text = "Falta seleccionar Empresa." : Exit Sub
            If cboModInteg.SelectedValue = "< Seleccionar >" Then lblPUError.Text = "Falta seleccionar Módulo de Integración." : Exit Sub
            If txtPCodUnico.Text.Trim = "" Then lblPUError.Text = "Falta ingresar código del Perfil y debe ser de 3 digitos." : Exit Sub
            If Len(txtPCodUnico.Text.Trim) <> 3 Then lblPUError.Text = "El codigo del Perfil debe ser de 3 digitos." : Exit Sub
            If txtPDescripcion.Text.Trim = "" Then lblPUError.Text = "Falta ingresar la descripción del perfil." : Exit Sub
            If lblPEtiqueta.Text.Trim = "Ingresar Nuevo Perfil" Then
                CodModInteg = cboModInteg.SelectedValue.Trim
                CodGrpoEmp = cboGrpoEmp.SelectedValue.Trim
                dt = objSeg.Existe_Perfil(txtPCodUnico.Text.Trim, CodModInteg, CodGrpoEmp, cboEmp.SelectedValue.Trim, "1")
                If dt.Rows.Count > 0 Then
                    lblPUError.Text = "Ya existe la definición." & Chr(13) & "Favor ingrese una que no exista." : Exit Sub
                Else
                    objSeg.InsUpd_Perfil(CodPerfil, txtPCodUnico.Text.Trim, txtPDescripcion.Text.Trim, CodModInteg, CodGrpoEmp, cboEmp.SelectedValue.Trim, "1")
                End If
            ElseIf lblPEtiqueta.Text.Trim = "Editar Perfil" Then
                CodPerfil = txtCodPerfil.Text.Trim
                objSeg.InsUpd_Perfil(CodPerfil, txtPCodUnico.Text.Trim, txtPDescripcion.Text.Trim, CodModInteg, CodGrpoEmp, cboEmp.SelectedValue.Trim, "2")
            End If
            btnCancelar_Click(sender, e)
            Call Lista_Perfiles()
        Catch ex As SqlException
            lblPUError.Text = ex.Message
        Catch ex As Exception
            lblPUError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblDefinePerfil.Visible = False
        Ficha.Height = 300
        lblPUError.Text = ""
        txtPCodUnico.Text = ""
        txtPDescripcion.Text = ""
        txtCodPerfil.Text = ""
        cboGrpoEmp.Items.Clear() : cboEmp.Items.Clear() : cboModInteg.Items.Clear()
        cboGrpoEmp.Items.Add("< Seleccionar >") : cboGrpoEmp.SelectedValue = "< Seleccionar >"
        cboEmp.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
        cboModInteg.Items.Add("< Seleccionar >") : cboEmp.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub Listar_PagxPerfil()
        Dim ModInteg As Double = 0
        ModInteg = txtModIntegAP.Text.Trim
        Try
            Dim objSeg As New ModuloSeguridad
            FlexPag.DataSource = objSeg.Listar_PaginasxModInteg(ModInteg)
            FlexPag.SelectedIndex = -1
            FlexPag.DataBind()
        Catch ex As SqlException
            lblAPError.Text = ex.Message
        Catch Ex As Exception
            lblAPError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    Private Sub Marcar_Pag()
        Dim Check As CheckBox
        Dim CodUnicoPerfil As Double = 0
        Dim i As Integer
        Dim dt As New Data.DataTable
        CodUnicoPerfil = txtCodUnicoAP.Text.Trim
        Try
            Dim obj As New ModuloSeguridad
            dt = obj.Listar_PaginasxPerfiles(CodUnicoPerfil)
            For Each dr As Data.DataRow In dt.Rows
                For i = 0 To FlexPag.Rows.Count - 1
                    If FlexPag.Rows(i).Cells(2).Text = dr("PAG_CODIGO").ToString Then
                        Check = CType(FlexPag.Rows(i).Cells(1).FindControl("chkPag"), CheckBox)
                        Check.Checked = True
                        Check.Enabled = False
                    End If
                Next
            Next
        Catch ex As SqlException
            lblAPError.Text = ex.Message
        Catch ex As Exception
            lblAPError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnAPGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim a As Integer : a = 0
        lblAPError.Text = ""
        Dim Actividad As CheckBox
        Dim CodUnicoPerfil As Double = 0
        CodUnicoPerfil = txtCodUnicoAP.Text.Trim
        For i = 0 To FlexPag.Rows.Count - 1
            Actividad = FlexPag.Rows(i).Cells(1).FindControl("chkpag")
            If Actividad.Checked = True And Actividad.Enabled = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblAPError.Text = "Debe de marcar al menos una actividad."
        If lblAPError.Text <> "" Then
            Exit Sub
        End If
        lblAPError.Text = ""
        Try
            For i = 0 To FlexPag.Rows.Count - 1
                Actividad = FlexPag.Rows(i).Cells(1).FindControl("chkPag")
                If Actividad.Checked = True And Actividad.Enabled = True Then
                    Dim obj As New ModuloSeguridad
                    obj.Insertar_PaginaxPerfil(CodUnicoPerfil, FlexPag.Rows(i).Cells(2).Text)
                End If
            Next
        Catch ex As SqlException
            lblAPError.Text = ex.Message
        Catch ex As Exception
            lblAPError.Text = ex.Message
        Finally
        End Try
        Call Listar_PagxPerfil()
        Call Marcar_Pag()
    End Sub
    Protected Sub FlexPag_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexPag.PageIndexChanging
        'lblAPError.Text = ""
        'FlexPag.PageIndex = e.NewPageIndex
        'Call Listar_PagxPerfil()
        'Call Marcar_Pag()
    End Sub
    Protected Sub FlexPag_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPag.RowCommand
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodPagina As String : CodPagina = ""
            Dim CodPerfil As String : CodPerfil = ""
            CodPerfil = txtCodUnicoAP.Text.Trim
            If e.CommandName = "Quitar" Then
                Dim obj As New ModuloSeguridad
                obj.Delete_PaginaxPerfil(CodPerfil, FlexPag.Rows(Index).Cells(2).Text)
                Call Listar_PagxPerfil()
                Call Marcar_Pag()
            End If
        Catch ex As SqlException
            lblPUError.Text = ex.Message
        Catch ex As Exception
            lblPUError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
End Class
