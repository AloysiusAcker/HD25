Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Cas_Define_Grupo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Ficha.ActiveTabIndex = 0
                Ficha_ActiveTabChanged(sender, e)
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
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Ficha.Height = 330
            Call Listar_Grupo()
            btnGCancelar_Click(sender, e)
            btnRCCancelar_Click(sender, e)
            btnRUCancelar_Click(sender, e)
            btnUNCancelar_Click(sender, e)
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Ficha.Height = 370
            Call Listar_ComponenteGrupo()
            Call Cargar_Grupo(cboRCGrupo, Session("Ruta_Emp"))
            Call Cargar_Componente(cboRCComponente, Session("Ruta_Emp"))
            cboRCGrupo.Items.Add("< Seleccionar >")
            cboRCComponente.Items.Add("< Seleccionar >")
            cboRCComponente.SelectedValue = "< Seleccionar >"
            cboRCGrupo.SelectedValue = "< Seleccionar >"
            btnGCancelar_Click(sender, e)
            btnRCCancelar_Click(sender, e)
            btnRUCancelar_Click(sender, e)
            btnUNCancelar_Click(sender, e)
        End If
        If Ficha.ActiveTabIndex = 2 Then
            Ficha.Height = 470
            Call Listar_UsuarioGrupo()
            Call Cargar_Grupo(cboGrupoPer, Session("Ruta_Emp"))
            cboGrupoPer.Items.Add("< Seleccionar >")
            cboGrupoPer.SelectedValue = "< Seleccionar >"
            btnGCancelar_Click(sender, e)
            btnRCCancelar_Click(sender, e)
            btnRUCancelar_Click(sender, e)
            btnUNCancelar_Click(sender, e)
        End If
        If Ficha.ActiveTabIndex = 3 Then
            Ficha.Height = 470
            Call Listar_UsuarioNivel()
            Call LlenaComboItem("TBOPC324", cboUNivel)
            cboUNivel.Items.Add("< Seleccionar >")
            cboUNivel.SelectedValue = "< Seleccionar >"
            btnGCancelar_Click(sender, e)
            btnRCCancelar_Click(sender, e)
            btnRUCancelar_Click(sender, e)
            btnUNCancelar_Click(sender, e)
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnGListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Listar_Grupo()
    End Sub
    Private Sub Listar_Grupo()
        Try
            lblError.Text = ""
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New ModuloCas
            dtListado = obj.CasLista_Grupo(Session("Ruta_Emp"))
            FlexG.DataSource = dtListado
            FlexG.DataBind()
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
    Protected Sub btnRCListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Listar_ComponenteGrupo()
    End Sub
    Private Sub Listar_ComponenteGrupo()
        Try
            lblErrorRC.Text = ""
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New ModuloCas
            dtListado = obj.CasLista_ComponenteGrupo(Session("Ruta_Emp"))
            FlexRC.DataSource = dtListado
            FlexRC.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorRC.Visible = True
            lblErrorRC.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorRC.Visible = True
            lblErrorRC.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexG_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexG.PageIndexChanging
        lblError.Text = ""
        FlexG.PageIndex = e.NewPageIndex
        Call Listar_Grupo()
    End Sub
    Protected Sub FlexRC_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexRC.PageIndexChanging
        lblErrorRC.Text = ""
        FlexRC.PageIndex = e.NewPageIndex
        Call Listar_ComponenteGrupo()
    End Sub
    Protected Sub btnRUListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Listar_UsuarioGrupo()
    End Sub
    Private Sub Listar_UsuarioGrupo()
        Try
            lblErrorRU.Text = ""
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New ModuloCas
            dtListado = obj.CasLista_UsuarioGrupo(Session("Ruta_Emp"))
            FlexRU.DataSource = dtListado
            FlexRU.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorRU.Visible = True
            lblErrorRU.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorRU.Visible = True
            lblErrorRU.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexRU_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexRU.PageIndexChanging
        lblErrorRU.Text = ""
        FlexRU.PageIndex = e.NewPageIndex
        Call Listar_UsuarioGrupo()
    End Sub
    Protected Sub btnUNListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Listar_UsuarioNivel()
    End Sub
    Private Sub Listar_UsuarioNivel()
        Try
            lblErrorUN.Text = ""
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New ModuloCas
            dtListado = obj.CasLista_UsuarioNivel(Session("Ruta_Emp"))
            FlexUN.DataSource = dtListado
            FlexUN.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorUN.Visible = True
            lblErrorUN.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorUN.Visible = True
            lblErrorUN.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexUN_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexUN.PageIndexChanging
        lblErrorUN.Text = ""
        FlexUN.PageIndex = e.NewPageIndex
        Call Listar_UsuarioNivel()
    End Sub
    Protected Sub btnGNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoG.Visible = True
        lblEtiquetaG.Text = "Nuevo Grupo"
        txtGNombre.Text = ""
        txtCodGrupo.Text = ""
        btnGNuevo.Enabled = False
        FlexG.Enabled = False
    End Sub
    Protected Sub btnGCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoG.Visible = False
        lblEtiquetaG.Text = ""
        txtGNombre.Text = ""
        txtCodGrupo.Text = ""
        btnGNuevo.Enabled = True
        FlexG.Enabled = True
    End Sub
    Protected Sub btnGGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim pCodigo As Double = 0
            Dim obj As New ModuloCas
            Dim dt As DataTable
            If txtGNombre.Text.Trim = "" Then lblError.Text = "Ingresar Nombre de Grupo." : Exit Sub
            If lblEtiquetaG.Text = "Nuevo Grupo" Then
                dt = obj.CasConsulta_ExisteGrupo(pCodigo, txtGNombre.Text.Trim, "2",Session("Ruta_Emp"))
                If dt.Rows.Count = 0 Then
                    obj.InsUpd_Grupo(pCodigo, txtGNombre.Text.Trim, HttpContext.Current.User.Identity.Name, "1",Session("Ruta_Emp"))
                Else
                    lblError.Text = "El Grupo ya existe." : Exit Sub
                End If
            ElseIf lblEtiquetaG.Text = "Editar Grupo" Then
                pCodigo = txtCodGrupo.Text.Trim
                obj.InsUpd_Grupo(pCodigo, txtGNombre.Text.Trim, HttpContext.Current.User.Identity.Name, "2",Session("Ruta_Emp"))
            End If
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnGCancelar_Click(sender, e)
        btnGListar_Click(sender, e)
    End Sub
    Protected Sub FlexG_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexG.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiquetaG.Text = "Editar Grupo"
            lblIngresoG.Visible = True
            txtGNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexG.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtCodGrupo.Text = FlexG.Rows(Index).Cells(2).Text.Trim
            FlexG.Enabled = False
        ElseIf e.CommandName = "Eliminar" Then
            Try
                Dim obj As New ModuloCas
                Dim dt As DataTable
                Dim pCodGrupo As Double = 0
                pCodGrupo = FlexG.Rows(Index).Cells(2).Text.Trim
                dt = obj.CasConsulta_ExisteGrupoxComponente(pCodGrupo, 0, "1",Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then
                    lblError.Text = "El grupo tiene componentes asignados. No se puede eliminar." : Exit Sub
                End If
                dt = Nothing
                dt = obj.CasConsulta_ExisteGrupoxUsuario(pCodGrupo, "", "1",Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then
                    lblError.Text = "El grupo tiene Usuarios asignados. No se puede eliminar." : Exit Sub
                End If
                dt = Nothing
                obj.InsUpd_Grupo(pCodGrupo, "", "", "3",Session("Ruta_Emp"))
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            btnGListar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnRCNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRCNuevo.Click
        lblIngresoRC.Visible = True
        lblEtiquetaRC.Text = "Nueva Relación"
        cboRCGrupo.SelectedValue = "< Seleccionar >"
        cboRCComponente.SelectedValue = "< Seleccionar >"
        btnRCNuevo.Enabled = False
        FlexRC.Enabled = False
    End Sub
    Protected Sub btnRCCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoRC.Visible = False
        lblEtiquetaRC.Text = ""
        btnRCNuevo.Enabled = True
        FlexRC.Enabled = True
    End Sub
    Protected Sub FlexRU_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexRU.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorRU.Text = ""
        Dim CodGrupo As Double = 0
        If e.CommandName = "Eliminar" Then
            Try
                Dim obj As New ModuloCas
                CodGrupo = FlexRU.Rows(Index).Cells(4).Text
                obj.InsUpd_RelacionGrupo(CodGrupo, FlexRU.Rows(Index).Cells(2).Text, "2",Session("Ruta_Emp"))
            Catch Ex As SqlException
                lblErrorRU.Visible = True
                lblErrorRU.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblErrorRU.Visible = True
                lblErrorRU.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            btnRUListar_Click(sender, e)
        End If
    End Sub
    Protected Sub FlexRC_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexRC.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiquetaRC.Text = "Editar Grupo"
            lblIngresoRC.Visible = True
            cboRCComponente.SelectedValue = FlexRC.Rows(Index).Cells(4).Text.Trim
            cboRCGrupo.SelectedValue = FlexRC.Rows(Index).Cells(5).Text.Trim
            FlexRC.Enabled = False
        ElseIf e.CommandName = "Eliminar" Then
            Try
                Dim obj As New ModuloCas
                Dim pCodGrupo As Double = 0
                Dim pCodComponente As Double = 0
                pCodGrupo = FlexRC.Rows(Index).Cells(5).Text.Trim
                pCodComponente = FlexRC.Rows(Index).Cells(4).Text.Trim
                obj.InsUpd_ComponentexGrupo(pCodGrupo, pCodComponente, "2",Session("Ruta_Emp"))
            Catch Ex As SqlException
                lblErrorRC.Visible = True
                lblErrorRC.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblErrorRC.Visible = True
                lblErrorRC.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            btnRCListar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnRCGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As New ModuloCas
            Dim dt As DataTable
            If cboRCGrupo.SelectedValue.Trim = "< Seleccionar >" Then lblErrorRC.Text = "Falta Grupo." : Exit Sub
            If cboRCComponente.SelectedValue.Trim = "< Selccionar >" Then lblErrorRC.Text = "Falta Componente." : Exit Sub
            dt = obj.CasConsulta_ExisteGrupoxComponente(cboRCGrupo.SelectedValue.Trim, cboRCComponente.SelectedValue.Trim, "2",Session("Ruta_Emp"))
            If dt.Rows.Count = 0 Then
                obj.InsUpd_ComponentexGrupo(cboRCGrupo.SelectedValue.Trim, cboRCComponente.SelectedValue.Trim, "1",Session("Ruta_Emp"))
            Else
                lblErrorRC.Text = "La relación ya existe."
            End If
        Catch Ex As SqlException
            lblErrorRC.Visible = True
            lblErrorRC.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorRC.Visible = True
            lblErrorRC.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnRCCancelar_Click(sender, e)
        btnRCListar_Click(sender, e)
    End Sub
    Protected Sub btnRUAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblEtiquetaGRU.Text = "Asignar Personas"
        Ficha.Height = 600
        lblRelacionUsuario.Visible = True
        Call Cargar_Grupo(cboGrupoPer, Session("Ruta_Emp"))
        cboGrupoPer.SelectedValue = "< Seleccionar >"
        optPersonas.SelectedIndex = 0
        lblErrorRU.Text = ""
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnRUCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblEtiquetaGRU.Text = ""
        Ficha.Height = 470
        lblRelacionUsuario.Visible = False
        cboGrupoPer.SelectedValue = "< Seleccionar >"
        optPersonas.SelectedIndex = 0
        FlexPersonal.DataSource = Nothing
        FlexPersonal.DataBind()
    End Sub
    Protected Sub optPersonas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorRU.Text = ""
        Try
            Dim objCas As New ModuloCas
            If optPersonas.SelectedIndex = 0 And cboGrupoPer.SelectedValue <> "< Seleccionar >" Then
                FlexPersonal.DataSource = objCas.CasLista_UsuarioXNivel("", "2",Session("Ruta_Emp")) 'Lista personal de la empresa
                FlexPersonal.DataBind()
            ElseIf optPersonas.SelectedIndex = 1 And cboGrupoPer.SelectedValue <> "< Seleccionar >" Then
                FlexPersonal.DataSource = objCas.CasLista_UsuarioXNivel("", "3",Session("Ruta_Emp")) 'Lista usuarios externos del sistema
                FlexPersonal.DataBind()
            Else
                lblErrorRU.Text = "Debe seleccionar grupo."
            End If
            Call Marcar_Personal("Grupo")
        Catch Ex As SqlException
            lblErrorRU.Visible = True
            lblErrorRU.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorRU.Visible = True
            lblErrorRU.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboGrupoPer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboGrupoPer.SelectedValue <> "< Seleccionar >" Then
            lblErrorRU.Text = ""
            optPersonas_SelectedIndexChanged(sender, e)
            Call Marcar_Personal("Grupo")
        End If
    End Sub
    Private Sub Marcar_Personal(ByVal pTipo As String)
        Dim Check As CheckBox
        Dim CodGrupo As Double = 0
        Dim i As Integer
        Dim dt As New Data.DataTable
        Try
            Dim obj As New ModuloCas
            If pTipo = "Grupo" Then
                CodGrupo = cboGrupoPer.SelectedValue.Trim
                dt = obj.CasConsulta_ExisteGrupoxUsuario(CodGrupo, "", "3",Session("Ruta_Emp"))
                For Each dr As Data.DataRow In dt.Rows
                    For i = 0 To FlexPersonal.Rows.Count - 1
                        If FlexPersonal.Rows(i).Cells(1).Text = dr("USUARIO").ToString Then
                            Check = CType(FlexPersonal.Rows(i).Cells(0).FindControl("chkPer"), CheckBox)
                            Check.Checked = True
                            Check.Enabled = False
                        End If
                    Next
                Next
                dt = Nothing
            ElseIf pTipo = "Nivel" Then
                dt = obj.CasLista_UsuarioXNivel(cboUNivel.SelectedValue, "4",Session("Ruta_Emp"))
                For Each dr As Data.DataRow In dt.Rows
                    For i = 0 To FlexUNPersonal.Rows.Count - 1
                        If FlexUNPersonal.Rows(i).Cells(1).Text = dr("USUARIO").ToString Then
                            Check = CType(FlexUNPersonal.Rows(i).Cells(0).FindControl("chkPersonal"), CheckBox)
                            Check.Checked = True
                            Check.Enabled = False
                        End If
                    Next
                Next
                dt = Nothing
            End If
        Catch ex As SqlException
            lblErrorRU.Text = ex.Message
        Catch ex As Exception
            lblErrorRU.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnRUGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim a As Integer : a = 0
        lblErrorRU.Text = ""
        Dim Personal As CheckBox
        Dim CodGrupo As Double = 0
        CodGrupo = cboGrupoPer.SelectedValue.Trim
        For i = 0 To FlexPersonal.Rows.Count - 1
            Personal = FlexPersonal.Rows(i).Cells(1).FindControl("chkPer")
            If Personal.Checked = True And Personal.Enabled = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblErrorRU.Text = "Debe de marcar al menos un Personal."
        If lblErrorRU.Text <> "" Then
            Exit Sub
        End If
        lblErrorRU.Text = ""
        Try
            Dim obj As New ModuloCas
            Dim dt As New DataTable
            For i = 0 To FlexPersonal.Rows.Count - 1
                Personal = FlexPersonal.Rows(i).Cells(1).FindControl("chkPer")
                dt = obj.CasConsulta_ExisteGrupoxUsuario(CodGrupo, FlexPersonal.Rows(i).Cells(1).Text, "4",Session("Ruta_Emp"))
                If dt.Rows.Count = 0 Then
                    If Personal.Checked = True And Personal.Enabled = True Then
                        obj.InsUpd_RelacionGrupo(CodGrupo, FlexPersonal.Rows(i).Cells(1).Text, "1",Session("Ruta_Emp"))
                    End If
                End If
                dt = Nothing
            Next
        Catch ex As SqlException
            lblErrorRU.Text = ex.Message
        Catch ex As Exception
            lblErrorRU.Text = ex.Message
        Finally
        End Try
        Call Listar_UsuarioGrupo()
        Call Marcar_Personal("Grupo")
        'lblRelacionUsuario.Visible = False
        'Call Listar_UsuarioGrupo()
    End Sub
    Protected Sub btnUNCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblUsuariosNivel.Visible = False
        Ficha.Height = 470
        cboUNivel.SelectedValue = "< Seleccionar >"
        FlexUNPersonal.DataSource = Nothing
        FlexUNPersonal.DataBind()
        lblEtiquetaUN.Text = ""
        optUN.SelectedIndex = 0
        lblErrorUN.Text = ""
    End Sub
    Protected Sub btnUNAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblUsuariosNivel.Visible = True
        Ficha.Height = 600
        cboUNivel.SelectedValue = "< Seleccionar >"
        FlexUNPersonal.DataSource = Nothing
        FlexUNPersonal.DataBind()
        lblEtiquetaUN.Text = "Asignar Personal"
        optUN.SelectedIndex = 0
        lblErrorUN.Text = ""
    End Sub
    Protected Sub optUN_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorUN.Text = ""
        Try
            Dim objSeg As New ModuloSeguridad
            If optUN.SelectedIndex = 0 And cboUNivel.SelectedValue <> "< Seleccionar >" Then
                FlexUNPersonal.DataSource = objSeg.Listar_Personal("", "", "", "", "", Session("CodGrupoEmpresa"), Session("CodEmpresa"), "3") 'Lista personal de la empresa
                FlexUNPersonal.DataBind()
            ElseIf optUN.SelectedIndex = 1 And cboUNivel.SelectedValue <> "< Seleccionar >" Then
                FlexUNPersonal.DataSource = objSeg.Listar_Usuarios_NoPersonal 'Lista personal de la empresa
                FlexUNPersonal.DataBind()
            Else
                lblErrorUN.Text = "Debe seleccionar Nivel."
            End If
            Call Marcar_Personal("Nivel")
        Catch Ex As SqlException
            lblErrorUN.Visible = True
            lblErrorUN.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorUN.Visible = True
            lblErrorUN.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboUNivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboUNivel.SelectedValue <> "< Seleccionar >" Then
            lblErrorUN.Text = ""
            optUN_SelectedIndexChanged(sender, e)
            Call Marcar_Personal("Nivel")
        End If
    End Sub
    Protected Sub FlexUNPersonal_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexUNPersonal.PageIndexChanging
        lblErrorUN.Text = ""
        FlexUNPersonal.PageIndex = e.NewPageIndex
        optUN_SelectedIndexChanged(sender, e)
        Call Marcar_Personal("Nivel")
    End Sub
    Protected Sub FlexPersonal_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexPersonal.PageIndexChanging
        lblErrorRU.Text = ""
        FlexPersonal.PageIndex = e.NewPageIndex
        optPersonas_SelectedIndexChanged(sender, e)
        Call Marcar_Personal("Grupo")
    End Sub
    Protected Sub btnUNGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim a As Integer : a = 0
        lblErrorUN.Text = ""
        Dim Personal As CheckBox
        For i = 0 To FlexUNPersonal.Rows.Count - 1
            Personal = FlexUNPersonal.Rows(i).Cells(1).FindControl("chkPersonal")
            If Personal.Checked = True And Personal.Enabled = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblErrorUN.Text = "Debe de marcar al menos un Personal."
        If lblErrorUN.Text <> "" Then
            Exit Sub
        End If
        lblErrorUN.Text = ""
        Try
            Dim obj As New ModuloCas
            Dim dt As New DataTable
            For i = 0 To FlexUNPersonal.Rows.Count - 1
                Personal = FlexUNPersonal.Rows(i).Cells(1).FindControl("chkPersonal")
                dt = obj.CasConsulta_ExisteUsuarioNivel(cboUNivel.SelectedValue.Trim, FlexUNPersonal.Rows(i).Cells(1).Text,Session("Ruta_Emp"))
                If dt.Rows.Count = 0 Then
                    If Personal.Checked = True And Personal.Enabled = True Then
                        obj.InsUpd_UsuarioNivel(cboUNivel.SelectedValue.Trim, FlexUNPersonal.Rows(i).Cells(1).Text, "1",Session("Ruta_Emp"))
                    End If
                End If
                dt = Nothing
            Next
        Catch ex As SqlException
            lblErrorUN.Text = ex.Message
        Catch ex As Exception
            lblErrorUN.Text = ex.Message
        Finally
        End Try
        Call Listar_UsuarioNivel()
        Call Marcar_Personal("Nivel")
    End Sub
    Protected Sub FlexUN_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUN.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorUN.Text = ""
        If e.CommandName = "Eliminar" Then
            Try
                Dim obj As New ModuloCas
                obj.InsUpd_UsuarioNivel(FlexUN.Rows(Index).Cells(4).Text, FlexUN.Rows(Index).Cells(2).Text, "2",Session("Ruta_Emp"))
            Catch Ex As SqlException
                lblErrorUN.Visible = True
                lblErrorUN.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblErrorUN.Visible = True
                lblErrorUN.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            btnUNListar_Click(sender, e)
        End If
    End Sub
End Class