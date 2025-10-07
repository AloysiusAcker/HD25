Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class MenuWeb_MenuWeb_Definicion
    Inherits System.Web.UI.Page
    Dim obj As New clsMenuWeb_Consultas
    Dim clFuncion As New clsMenuWeb_Funciones
    Dim clProceso As New clsGeneral_Proceso
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            Session("Inicia") = "Si"
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        'If Ficha.TabIndex = "0" Then
        If Session("Inicia") = "No" Then
            Session("Inicia") = "Si"
        Else
            lblIngresoParrafo.Visible = False
            Call Llenar_GrupoEmpresa(cboGrupoP, cboEmpresaP)
            lblItemIngreso.Visible = False
            'btnListarItem_Click(sender, e)
            Session("Inicia") = "Si"
            Call Llenar_GrupoEmpresa(cboGrupo, cboEmpresa)
            lblCategoria.Visible = False
            Session("Inicia") = "Si"
            Call Llenar_GrupoEmpresa(cboGrupoUtil, cboEmpresaUtil)
        End If
    End Sub
    Private Sub Listar_Parrafo()
        lblError.Text = ""
        Try
            If cboGrupoP.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Seleccionar Grupo."
            If cboEmpresaP.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Empresa."
            If lblError.Text <> "" Then
                lblError.Text = "Se han encontrado las sgtes. observaciones:" & lblError.Text
                Exit Sub
            End If
            Dim pdGrupo As Double = 0
            pdGrupo = cboGrupoP.SelectedValue.Trim
            Flex.DataSource = obj.Lista_Parrafo(pdGrupo, cboEmpresaP.SelectedValue.Trim)
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoParrafo.Visible = True
        lblError.Text = ""
        lblIngreso.Text = "Nuevo Párrafo"
        txtTitulo.Text = ""
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        Flex.Enabled = False
        Call Llenar_GrupoEmpresa(cboGrupoPIng, cboEmpresaPIng)
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Listar_Parrafo()
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoParrafo.Visible = False
        lblError.Text = ""
        Flex.Enabled = True
        txtTitulo.Text = ""
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        Call Listar_Parrafo()
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            txtCodigo.Text = Flex.Rows(Index).Cells(1).Text
            txtTitulo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            cboGrupoPIng.SelectedValue = Flex.Rows(Index).Cells(4).Text
            cboEmpresaPIng.SelectedValue = Flex.Rows(Index).Cells(5).Text
            lblIngreso.Text = "Editar Párrafo"
            lblIngresoParrafo.Visible = True
            Flex.Enabled = False
        End If
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblError.Text = ""
            If txtTitulo.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar el Título."
            If txtDescripcion.Text = "" Then lblError.Text = lblError.Text & "<br> - Ingresar la descripción."
            If cboGrupoPIng.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Grupo."
            If cboEmpresaPIng.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Empresa."
            If lblError.Text <> "" Then
                lblError.Text = "Se han encontrado las sgtes. observaciones : " & lblError.Text
                Exit Sub
            End If
            Dim pdGrupo As Double = 0
            pdGrupo = cboGrupoP.SelectedValue.Trim
            Dim codParrafo As Double = 0
            If lblIngreso.Text = "Nuevo Párrafo" Then
                obj.Ins_Parrafo(pdGrupo, cboEmpresaPIng.SelectedValue.Trim, txtTitulo.Text, txtDescripcion.Text, 0, "1")
            Else
                codParrafo = txtCodigo.Text
                obj.Ins_Parrafo(pdGrupo, cboEmpresaPIng.SelectedValue.Trim, txtTitulo.Text, txtDescripcion.Text, codParrafo, "2")
            End If
            Call Listar_Parrafo()
            btnCancelar_Click(sender, e)
         Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnListarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorItem.Text = ""
        Dim pdGrupo As Double = 0
        Try
            FlexItem.DataSource = obj.Lista_Items(Session("CodGrupoEmpresa"), Session("CodEmpresa"))
            FlexItem.DataBind()
        Catch ex As SqlException
            lblErrorItem.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorItem.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnNuevoItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblItemIngreso.Visible = True
        lblIngresoItem.Text = "Nuevo Items del Menú"
        lblErrorItem.Text = ""
        txtNombreItem.Text = ""
        txtPaginaItem.Text = ""
        txtCodigoItem.Text = ""
        FlexItemCampo.DataSource = Llenar_Campos()
        FlexItemCampo.DataBind()
        Try
            Dim dtListado As DataTable
            Dim a As Long = 0
            If FlexItemCampo.Rows.Count > 0 Then
                For a = 0 To FlexItemCampo.Rows.Count - 1
                    dtListado = obj.Lista_Campos_ItemElemento(FlexItemCampo.Rows(a).Cells(11).Text)
                    For Each dr As DataRow In dtListado.Rows
                        If Nu(dr("DATA_TYPE")) = "varchar" Then
                            FlexItemCampo.Rows(a).Cells(5).Text = Nu(dr("DATA_TYPE")) & " " & Nu(dr("CHARACTER_MAXIMUM_LENGTH"))
                        Else
                            FlexItemCampo.Rows(a).Cells(5).Text = Nu(dr("DATA_TYPE"))
                        End If
                    Next
                    dtListado = Nothing
                Next
            End If
        Catch ex As SqlException
            lblErrorItem.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorItem.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Function Llenar_Campos() As DataTable
        Llenar_Campos = Nothing
        Dim dt As New DataTable
        Dim dRow As Data.DataRow
        Dim i As Long = 0
        'cargar campos
        dt.Columns.Add("c0") 'correlativo
        dt.Columns.Add("c1") 'referencia del campo
        dt.Columns.Add("c2") 'tipo
        dt.Columns.Add("c3") 'etiqueta del campo
        dt.Columns.Add("c4") 'obligatorio
        dt.Columns.Add("c5") 'nombre del campo
        dt.Columns.Add("c6") 'nombre del campo
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Nombre" : dRow("c5") = "ELEMENTO_NOMBRE" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Categoría" : dRow("c5") = "ELEMENTO_CATEGORIA" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Descripción Breve" : dRow("c5") = "ELEMENTO_DESCRIP_CORTA" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Descripción Completa" : dRow("c5") = "ELEMENTO_DESCRIP_LARGA" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Link 1" : dRow("c5") = "ELEMENTO_LINK1" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Link 2" : dRow("c5") = "ELEMENTO_LINK2": dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Fecha1 (informativo)" : dRow("c5") = "ELEMENTO_FECHA1" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Fecha2 (inicial a mostrar)" : dRow("c5") = "ELEMENTO_FECHA2" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Fecha2 (limite a mostrar)" : dRow("c5") = "ELEMENTO_FECHA3" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Carga de Imagen" : dRow("c5") = "ELEMENTO_IMAGEN" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Completar 1 (Corto)" : dRow("c5") = "ELEMENTO_COMPLETAR1" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Completar 2 (Largo)" : dRow("c5") = "ELEMENTO_COMPLETAR2" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Completar 3 (Corto)" : dRow("c5") = "ELEMENTO_COMPLETAR3" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Completar 4 (Largo)" : dRow("c5") = "ELEMENTO_COMPLETAR4" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Completar 5 (Corto)" : dRow("c5") = "ELEMENTO_COMPLETAR5" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Carga de Archivo" : dRow("c5") = "ELEMENTO_ARCHIVO_NOMBRE" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        dRow = dt.NewRow
        i = i + 1 : dRow("c0") = i : dRow("c1") = "Código de Usuario" : dRow("c5") = "ELEMENTO_USUARIO" : dRow("c6") = "1"
        dt.Rows.Add(dRow)
        Return dt
    End Function
    Protected Sub btnCancelarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblItemIngreso.Visible = False
        lblErrorItem.Text = ""
        txtNombreItem.Text = ""
        txtPaginaItem.Text = ""
        txtCodigoItem.Text = ""
        FlexItemCampo.DataSource = Nothing
        FlexItemCampo.DataBind()
        FlexItem.Enabled = True
    End Sub
    Protected Sub FlexItemCampo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexItemCampo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorItem.Text = ""
        If e.CommandName = "MSi" Then
            Dim texto As TextBox = CType(FlexItemCampo.Rows(Index).Cells(7).FindControl("txtI"), TextBox)
            Dim chk As CheckBox = CType(FlexItemCampo.Rows(Index).Cells(1).FindControl("chkm"), CheckBox)
            FlexItemCampo.Rows(Index).Cells(8).Text = "No"
            FlexItemCampo.Rows(Index).Cells(12).Text = "2"
            texto.Text = ""
            texto.Enabled = True
            chk.Checked = True
        End If
        If e.CommandName = "MNo" Then
            Dim texto As TextBox = CType(FlexItemCampo.Rows(Index).Cells(7).FindControl("txtI"), TextBox)
            Dim chk As CheckBox = CType(FlexItemCampo.Rows(Index).Cells(1).FindControl("chkm"), CheckBox)
            FlexItemCampo.Rows(Index).Cells(8).Text = ""
            FlexItemCampo.Rows(Index).Cells(12).Text = "1"
            texto.Text = ""
            texto.Enabled = False
            chk.Checked = False
        End If
        If e.CommandName = "Si" Then
            Dim chk As CheckBox = CType(FlexItemCampo.Rows(Index).Cells(1).FindControl("chkm"), CheckBox)
            If chk.Checked = True Then
                FlexItemCampo.Rows(Index).Cells(8).Text = "Si"
            End If
        End If
        If e.CommandName = "No" Then
            Dim chk As CheckBox = CType(FlexItemCampo.Rows(Index).Cells(1).FindControl("chkm"), CheckBox)
            If chk.Checked = True Then
                FlexItemCampo.Rows(Index).Cells(8).Text = "No"
            End If
        End If
    End Sub
    Protected Sub FlexItem_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexItem.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim a As Long = 0
        lblErrorItem.Text = ""
        Try
            If e.CommandName = "Editar" Then
                FlexItem.Enabled = False
                lblIngresoItem.Text = "Editar Items del Menú"
                lblItemIngreso.Visible = True
                lblErrorItem.Text = ""
                txtNombreItem.Text = ""
                txtPaginaItem.Text = ""
                txtCodigoItem.Text = ""
                FlexItemCampo.DataSource = Llenar_Campos()
                FlexItemCampo.DataBind()
                txtCodigoItem.Text = FlexItem.Rows(Index).Cells(3).Text
                txtNombreItem.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItem.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtPaginaItem.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItem.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                FlexItemCampo.DataSource = Llenar_Campos()
                FlexItemCampo.DataBind()
                Dim psCodigo As Double = 0
                Dim dt As DataTable
                Dim dtListado As DataTable
                If FlexItemCampo.Rows.Count > 0 Then
                    For a = 0 To FlexItemCampo.Rows.Count - 1
                        dtListado = obj.Lista_Campos_ItemElemento(FlexItemCampo.Rows(a).Cells(11).Text)
                        For Each dr As DataRow In dtListado.Rows
                            If Nu(dr("DATA_TYPE")) = "varchar" Then
                                FlexItemCampo.Rows(a).Cells(5).Text = Nu(dr("DATA_TYPE")) & " " & Nu(dr("CHARACTER_MAXIMUM_LENGTH"))
                            Else
                                FlexItemCampo.Rows(a).Cells(5).Text = Nu(dr("DATA_TYPE"))
                            End If
                            FlexItemCampo.Rows(a).Cells(12).Text = "2"
                        Next
                    Next
                End If
                dtListado = Nothing
                psCodigo = txtCodigoItem.Text.Trim
                dt = obj.Lista_Campos_XItemElemento(psCodigo, "")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        For a = 0 To FlexItemCampo.Rows.Count - 1
                            If UCase(Nu(dr("CAMPO_NOMBRE"))) = UCase(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItemCampo.Rows(a).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")) Then
                                Dim chk As CheckBox = CType(FlexItemCampo.Rows(a).Cells(1).FindControl("chkm"), CheckBox)
                                Dim texto As TextBox = CType(FlexItemCampo.Rows(a).Cells(7).FindControl("txtI"), TextBox)
                                chk.Checked = True
                                texto.Enabled = True
                                FlexItemCampo.Rows(a).Cells(6).Text = Nu(dr("CAMPO_ETIQUETA"))
                                If Nu(dr("CAMPO_OBLIGATORIO")) = "S" Then
                                    FlexItemCampo.Rows(a).Cells(8).Text = "Si"
                                ElseIf Nu(dr("CAMPO_OBLIGATORIO")) = "N" Then
                                    FlexItemCampo.Rows(a).Cells(8).Text = "No"
                                Else
                                    FlexItemCampo.Rows(a).Cells(8).Text = ""
                                End If
                                'Exit For
                            End If
                        Next
                    Next
                End If
                dt = Nothing
            End If
        Catch ex As SqlException
            lblErrorItem.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorItem.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnGuardarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim psCodItem As Double = 0
            Dim dt As DataTable
            Dim a As Long = 0
            Dim p As Long = 0
            Dim psObligatorio As String = ""
            lblErrorItem.Text = ""
            If lblIngresoItem.Text = "Nuevo Items del Menú" Then
                If FlexItemCampo.Rows.Count > 0 Then
                    For a = 0 To FlexItemCampo.Rows.Count - 1
                        Dim chk As CheckBox = CType(FlexItemCampo.Rows(a).Cells(1).FindControl("chkm"), CheckBox)
                        If chk.Checked = True Then p = 1
                        Exit For
                    Next
                End If
                If FlexItemCampo.Rows.Count > 0 Then
                    For a = 0 To FlexItemCampo.Rows.Count - 1
                        Dim chk As CheckBox = CType(FlexItemCampo.Rows(a).Cells(1).FindControl("chkm"), CheckBox)
                        Dim texto As TextBox = CType(FlexItemCampo.Rows(a).Cells(7).FindControl("txtI"), TextBox)
                        If chk.Checked = True And texto.Text = "" Then
                            lblErrorItem.Text = "Todos los campos deben de tener etiqueta." : Exit Sub
                        End If
                    Next
                End If
                If txtNombreItem.Text = "" Then lblErrorItem.Text = "<br> - Ingresar Nombre."
                If txtPaginaItem.Text = "" Then lblErrorItem.Text = lblErrorItem.Text & "<br> - Ingresar Página."
                If txtNombreItem.Text.Trim <> "" Then
                    dt = obj.Busca_ItemxNombre(txtNombreItem.Text)
                    If dt.Rows.Count > 0 Then
                        lblErrorItem.Text = lblErrorItem.Text & "<br> - El Nombre ingresado ya existe, favor de verificar o cambiar."
                    Else
                        lblErrorItem.Text = ""
                    End If
                    dt = Nothing
                End If
                If lblErrorItem.Text <> "" Then
                    lblErrorItem.Text = "Se han encontrado las sgtes. observaciones: " & lblErrorItem.Text
                    Exit Sub
                End If
                dt = obj.Lista_UltimoCodigoMenu()
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        psCodItem = Nz(dr("CODIGO")) + 1
                    Next
                Else
                    psCodItem = 0
                End If
                dt = Nothing
                obj.Ins_Item(psCodItem, txtNombreItem.Text.Trim, txtPaginaItem.Text.Trim, FlexItemCampo.Rows.Count + 1)
            ElseIf lblIngresoItem.Text = "Editar Items del Menú" Then
                psCodItem = txtCodigoItem.Text.Trim
                obj.Upd_Item(psCodItem, txtNombreItem.Text.Trim, txtPaginaItem.Text.Trim)
            End If
            If FlexItemCampo.Rows.Count > 0 Then
                For a = 0 To FlexItemCampo.Rows.Count - 1
                    Dim texto As TextBox = CType(FlexItemCampo.Rows(a).Cells(7).FindControl("txtI"), TextBox)
                    Dim chk As CheckBox = CType(FlexItemCampo.Rows(a).Cells(1).FindControl("chkm"), CheckBox)
                    If chk.Checked = True Then
                        If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItemCampo.Rows(a).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") = "Si" Then
                            psObligatorio = "S"
                        ElseIf Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItemCampo.Rows(a).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") = "No" Then
                            psObligatorio = "N"
                        End If
                        dt = obj.Lista_Campos_XItemElemento(psCodItem, FlexItemCampo.Rows(a).Cells(11).Text)
                        If dt.Rows.Count > 0 Then
                            If texto.Text <> "" Then
                                obj.Upd_ItemCampo(psCodItem, texto.Text, psObligatorio, FlexItemCampo.Rows(a).Cells(11).Text)
                            Else
                                obj.Upd_ItemCampo(psCodItem, Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexItemCampo.Rows(a).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), psObligatorio, FlexItemCampo.Rows(a).Cells(11).Text)
                            End If
                        Else
                            If FlexItemCampo.Rows(a).Cells(12).Text = "2" Then
                                obj.Ins_ItemCampo(psCodItem, FlexItemCampo.Rows(a).Cells(0).Text, FlexItemCampo.Rows(a).Cells(11).Text, texto.Text, psObligatorio)
                            End If
                        End If
                        dt = Nothing
                    End If
                Next
            End If
            btnListarItem_Click(sender, e)
            btnCancelarItem_Click(sender, e)
        Catch ex As SqlException
            lblErrorItem.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorItem.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Private Sub Llenar_GrupoEmpresa(ByVal cboG As DropDownList, ByVal cboE As DropDownList)
        cboG.Items.Clear() : cboE.Items.Clear()
        If Session("Inicia") = "Si" Then
            Call clProceso.Llena_GrupoEmpresa(cboG, HttpContext.Current.User.Identity.Name)
            Session("Inicia") = "No"
        End If
        cboG.Items.Add("< Seleccionar >") : cboG.SelectedValue = "< Seleccionar >"
        cboE.Items.Add("< Seleccionar >") : cboE.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub cboGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
        If cboGrupo.SelectedValue <> "< Seleccionar >" Then
            Dim pdCodGrupo As Double = 0
            pdCodGrupo = cboGrupo.SelectedValue.Trim
            clProceso.Llena_Empresa(HttpContext.Current.User.Identity.Name, pdCodGrupo, cboEmpresa)
        End If
    End Sub
    Protected Sub btnListarCat_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorCat.Text = ""
        If cboGrupo.SelectedValue = "< Seleccionar >" Then lblErrorCat.Text = "<br> - Seleccionar Grupo."
        If cboEmpresa.SelectedValue = "< Seleccionar >" Then lblErrorCat.Text = lblErrorCat.Text & "<br> - Seleccionar Empresa."
        If lblErrorCat.Text <> "" Then
            lblErrorCat.Text = "Se han encontrado las sgtes. observaciones:" & lblErrorCat.Text
            Exit Sub
        End If
        Try
            Dim CodGrupoEmp As Double = 0
            CodGrupoEmp = cboGrupo.SelectedValue.Trim
            FlexCat.DataSource = obj.Lista_ItemCategoria(CodGrupoEmp, cboEmpresa.SelectedValue.Trim)
            FlexCat.DataBind()
        Catch ex As SqlException
            lblErrorCat.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorCat.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnNuevoCat_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorCat.Text = ""
        If cboGrupo.SelectedValue = "< Seleccionar >" Then lblErrorCat.Text = "<br> - Seleccionar Grupo."
        If cboEmpresa.SelectedValue = "< Seleccionar >" Then lblErrorCat.Text = lblErrorCat.Text & "<br> - Seleccionar Empresa."
        If lblErrorCat.Text <> "" Then
            lblErrorCat.Text = "Se han encontrado las sgtes. observaciones:" & lblErrorCat.Text
            Exit Sub
        End If
        lblCategoria.Visible = True
        lblCatEtiqueta.Text = "Nueva Categoría"
        txtCatNombre.Text = ""
        txtCatCodigo.Text = ""
        Try
            Dim pdGrupo As Double = 0
            pdGrupo = cboGrupo.SelectedValue.Trim
            Call clFuncion.Llena_ItemsMenu(cboCatItem, pdGrupo, cboEmpresa.SelectedValue.Trim)
        Catch ex As SqlException
            lblErrorCat.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorCat.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnCancelarCat_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorCat.Text = ""
        lblCategoria.Visible = False
        lblCatEtiqueta.Text = ""
        txtCatNombre.Text = ""
        txtCatCodigo.Text = ""
        FlexCat.Enabled = True
    End Sub
    Protected Sub btnGuardarCat_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        If cboCatItem.SelectedValue = "< Seleccionar >" Then lblErrorCat.Text = "<br> - Seleccionar Item."
        If txtCatNombre.Text.Trim = "" Then lblErrorCat.Text = lblErrorCat.Text & "<br> - Ingresar el nombre de la Categoría"
        If lblErrorCat.Text <> "" Then
            lblErrorCat.Text = "Se han encontrado las sgtes. observaciones: " & lblErrorCat.Text
            Exit Sub
        End If
        Try
            Dim pdCodGrupo As Double = 0
            Dim pdCodCategoria As Double = 0
            pdCodGrupo = cboGrupo.SelectedValue.Trim
            If lblCatEtiqueta.Text = "Nueva Categoría" Then
                obj.Ins_Categoria(pdCodGrupo, cboEmpresa.SelectedValue.Trim, pdCodCategoria, txtCatNombre.Text.Trim)
            ElseIf lblCatEtiqueta.Text = "Editar Categoría" Then
                pdCodCategoria = txtCatCodigo.Text.Trim
                obj.Upd_Categoria(pdCodGrupo, cboEmpresa.SelectedValue.Trim, pdCodCategoria, txtCatNombre.Text.Trim)
            End If
            btnCancelarCat_Click(sender, e)
            btnListarCat_Click(sender, e)
        Catch ex As SqlException
            lblErrorCat.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorCat.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub FlexCat_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexCat.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim a As Long = 0
        Dim dt As DataTable
        lblErrorItem.Text = ""
        Try
            If e.CommandName = "Editar" Then
                Dim pdGrupo As Double = 0
                pdGrupo = cboGrupo.SelectedValue.Trim
                Call clFuncion.Llena_ItemsMenu(cboCatItem, pdGrupo, cboEmpresa.SelectedValue.Trim)
                If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCat.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") <> "" Then
                    cboCatItem.SelectedValue = Nz(FlexCat.Rows(Index).Cells(2).Text.Trim)
                End If
                txtCatCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCat.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtCatNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCat.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                lblCatEtiqueta.Text = "Editar Categoría"
                FlexCat.Enabled = False
                lblCategoria.Visible = True
            End If
            If e.CommandName = "Eliminar" Then
                Dim pdCodCategoria As Double = 0
                pdCodCategoria = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexCat.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                dt = obj.Existe_Categoria(pdCodCategoria)
                If dt.Rows.Count > 0 Then
                    lblErrorCat.Text = "No puede eliminar la categoría seleccionada es utilizada en los registros de menú."
                    Exit Sub
                End If
                obj.Del_Categoria(pdCodCategoria)
                btnListarCat_Click(sender, e)
            End If
        Catch ex As SqlException
            lblErrorCat.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorCat.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboGrupoP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupoP.SelectedIndexChanged
        Try
            If cboGrupoP.SelectedValue <> "< Seleccionar >" Then
                Dim pdCodGrupo As Double = 0
                pdCodGrupo = cboGrupoP.SelectedValue.Trim
                clProceso.Llena_Empresa(HttpContext.Current.User.Identity.Name, pdCodGrupo, cboEmpresaP)
            End If
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboGrupoPIng_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupoPIng.SelectedIndexChanged
        Try
            If cboGrupoPIng.SelectedValue <> "< Seleccionar >" Then
                Dim pdCodGrupo As Double = 0
                pdCodGrupo = cboGrupoPIng.SelectedValue.Trim
                clProceso.Llena_Empresa(HttpContext.Current.User.Identity.Name, pdCodGrupo, cboEmpresaPIng)
            End If
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnListarUtil_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorUtil.Text = ""
        If cboGrupoUtil.SelectedValue = "< Seleccionar >" Then lblErrorUtil.Text = "<br> - Seleccionar Grupo."
        If cboEmpresaUtil.SelectedValue = "< Seleccionar >" Then lblErrorUtil.Text = lblErrorUtil.Text & "<br> - Seleccionar Empresa."
        If lblErrorUtil.Text <> "" Then
            lblErrorUtil.Text = "Se han encontrado las sgtes. observaciones:" & lblErrorUtil.Text
            Exit Sub
        End If
        Try
            FlexUtil.DataSource = obj.Lista_ItemGenerales()
            FlexUtil.DataBind()
            Call Marcar_Item()
        Catch ex As SqlException
            lblErrorUtil.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorUtil.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboGrupoUtil_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupoUtil.SelectedIndexChanged
        If cboGrupoUtil.SelectedValue <> "< Seleccionar >" Then
            Dim pdCodGrupo As Double = 0
            pdCodGrupo = cboGrupoUtil.SelectedValue.Trim
            clProceso.Llena_Empresa(HttpContext.Current.User.Identity.Name, pdCodGrupo, cboEmpresaUtil)
        End If
    End Sub
    Private Sub Marcar_Item()
        Dim Check As CheckBox
        Dim i As Integer
        Dim dt As New Data.DataTable
        lblErrorUtil.Text = ""
        Try
            Dim CodGrupoUtil As Double = 0
            CodGrupoUtil = cboGrupoUtil.SelectedValue.Trim
            dt = obj.Lista_ItemAUtilizar(CodGrupoUtil, cboEmpresaUtil.SelectedValue.Trim)
            For Each dr As Data.DataRow In dt.Rows
                For i = 0 To FlexUtil.Rows.Count - 1
                    If FlexUtil.Rows(i).Cells(2).Text = dr("ITEM_CODIGO").ToString Then
                        Check = CType(FlexUtil.Rows(i).Cells(1).FindControl("chkUsar"), CheckBox)
                        Check.Checked = True
                        Check.Enabled = False
                    End If
                Next
            Next
        Catch ex As SqlException
            lblErrorUtil.Text = ex.Message
        Catch ex As Exception
            lblErrorUtil.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnGuardarUtil_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarUtil.Click
        Dim i As Integer
        Dim a As Integer : a = 0
        Dim Usar As CheckBox
        Dim CodGrupoEmpresa As Double = 0
        Dim CodItem As Double = 0
        Dim Estado As String = ""
        Dim dt As DataTable
        lblErrorUtil.Text = ""
        CodGrupoEmpresa = cboGrupoUtil.SelectedValue.Trim
        For i = 0 To FlexUtil.Rows.Count - 1
            Usar = FlexUtil.Rows(i).Cells(1).FindControl("chkUsar")
            If Usar.Checked = True And Usar.Enabled = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblErrorUtil.Text = "Debe de marcar al menos una actividad." : Exit Sub
        Try
            For i = 0 To FlexUtil.Rows.Count - 1
                Usar = FlexUtil.Rows(i).Cells(1).FindControl("chkUsar")
                CodItem = FlexUtil.Rows(i).Cells(2).Text
                If Usar.Checked = True Then Estado = "0" Else Estado = "1"
                dt = obj.Lista_ItemVerificar(CodGrupoEmpresa, cboEmpresaUtil.SelectedValue.Trim, CodItem)
                If dt.Rows.Count > 0 Then
                    dt = Nothing
                    obj.Upd_ItemsAUtilizar(CodGrupoEmpresa, cboEmpresaUtil.SelectedValue.Trim, CodItem, Estado)
                Else
                    dt = Nothing
                    obj.Ins_ItemsAUtilizar(CodGrupoEmpresa, cboEmpresaUtil.SelectedValue.Trim, CodItem, Estado)
                End If
            Next
            Call btnListarUtil_Click(sender, e)
        Catch ex As SqlException
            lblErrorUtil.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorUtil.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub FlexUtil_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUtil.RowCommand
        Try
            Dim CodGrupoEmpresa As Double = 0
            Dim CodItem As Double = 0
            Dim Estado As String = ""
            lblErrorUtil.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            If e.CommandName = "Quitar" Then
                CodGrupoEmpresa = cboGrupoUtil.SelectedValue.Trim
                CodItem = FlexUtil.Rows(Index).Cells(2).Text
                Estado = "1"
                obj.Del_ItemsAUtilizar(CodGrupoEmpresa, cboEmpresaUtil.SelectedValue.Trim, CodItem, Estado)
            End If
            Call btnListarUtil_Click(sender, e)
        Catch ex As SqlException
            lblErrorUtil.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorUtil.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
End Class