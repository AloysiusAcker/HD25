Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Tablas_Especiales_Mantenimiento
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Private Sub Lista_Tablas()
        Try
            Dim obj As New ModuloGeneral
            Flex.DataSource = obj.Lista_TablasEspeciales(Session("Ruta_Emp"))
            Flex.DataBind()
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        lblTablaEspecial.Visible = True
        lblError.Text = ""
        lblEtiqueta.Text = "Ingresar Tabla Especial"
        txtDescripcion.Text = "" : lblCodigo.Text = "" : txtPrefijo.Text = ""
        Flex.Enabled = False : btnNuevo.Enabled = False
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Lista_Tablas()
            Ficha.ActiveTab.Enabled = True
            btnNuevo.Enabled = True
            Flex.Enabled = True
        End If
        If Ficha.ActiveTabIndex = 1 Then
            btnNuevoTE.Enabled = False
            lblIngresoTE.Visible = False
            cboTabla.Items.Clear()
            cboTabla.Items.Add(lblTabla1.Text.Trim)
            cboTabla.Items.Add(lblTabla2.Text.Trim)
            cboTabla.Items.Add(lblTabla3.Text.Trim)
            cboTabla.Items.Add("< Seleccionar >") : cboTabla.SelectedValue = "< Seleccionar >"
            cboTabla.Enabled = True
            btnRegresar.Enabled = True
            Ficha.ActiveTab.Enabled = True
            btnNuevo.Enabled = False
        End If
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        lblTablaEspecial.Visible = False
        lblError.Text = "" : txtDescripcion.Text = "" : lblEtiqueta.Text = ""
        lblCodigo.Text = "" : txtPrefijo.Text = ""
        Flex.Enabled = True : BtnNuevo.Enabled = True
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim dt As New DataTable
            Dim objG As New ModuloGeneral
            Dim dCodigo As Double = 0
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim cmdSql As New SqlCommand
            If txtDescripcion.Text.Trim = "" Then lblError.Text = "Es dato Obligatorio ingresar la Descripción ó Referencia" : Exit Sub
            If Len(txtPrefijo.Text.Trim) < 3 Then lblError.Text = "Es dato Obligatorio ingresar el Prefijo" : Exit Sub
            If lblEtiqueta.Text = "Ingresar Tabla Especial" Then
                dt = objG.Existe_TablaEspecial(txtPrefijo.Text.Trim, Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then
                    lblError.Text = "Se encontró que existen Tablas Especiales creadas con esa momenclatura," & Chr(13) & "verificar que lo ingresado no exista o cambiar la monenclatura" : Exit Sub
                End If
                objG.Insert_TablaEspecial(txtDescripcion.Text.Trim, txtPrefijo.Text.Trim, "TBESP_" & txtPrefijo.Text.Trim & "1", "TBESP_" & txtPrefijo.Text.Trim & "2", "TBESP_" & txtPrefijo.Text.Trim & "3", Session("Ruta_Emp"))
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[TBESP_" & txtPrefijo.Text.Trim & "1]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[TBESP_" & txtPrefijo.Text.Trim & "1]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[TBESP_" & txtPrefijo.Text.Trim & "2]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[TBESP_" & txtPrefijo.Text.Trim & "2]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[TBESP_" & txtPrefijo.Text.Trim & "3]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [dbo].[TBESP_" & txtPrefijo.Text.Trim & "3]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = "CREATE TABLE [dbo].[TBESP_" & txtPrefijo.Text.Trim & "1] ([NIVEL1_CODIGO] [float] NULL ,[EMPRESA_CODIGO] [varchar] (4) NULL,[NIVEL1_DESCRIP] [varchar] (200) NULL ,[NIVEL1_SYS_EST] [varchar] (1) NULL,[COLOR_CODIGO] [varchar] (50) NULL,[COLOR_ROJO] [varchar] (6) NULL,[COLOR_VERDE] [varchar] (6) NULL,[COLOR_AZUL] [varchar] (6) NULL) ON [PRIMARY]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = "CREATE TABLE [dbo].[TBESP_" & txtPrefijo.Text.Trim & "2] ([NIVEL1_CODIGO] [float] NULL ,[NIVEL2_CODIGO] [float] NULL ,[EMPRESA_CODIGO] [varchar] (4) NULL ,[NIVEL2_DESCRIP] [varchar] (200) NULL ,[NIVEL2_SYS_EST] [varchar] (1) NULL) ON [PRIMARY]"
                cmdSql.ExecuteNonQuery()
                cmdSql.CommandText = "CREATE TABLE [dbo].[TBESP_" & txtPrefijo.Text.Trim & "3] ([NIVEL2_CODIGO] [float] NULL ,[NIVEL3_CODIGO] [float] NULL ,[EMPRESA_CODIGO] [varchar] (4) NULL,[NIVEL3_DESCRIP] [varchar] (200) NULL ,[NIVEL3_SYS_EST] [varchar] (1) NULL,[NIVEL3_NS_DHM] [VARCHAR] (10) NULL) ON [PRIMARY]"
                cmdSql.ExecuteNonQuery()
            ElseIf lblEtiqueta.Text = "Editar Tabla Especial" Then
                dCodigo = lblCodigo.Text.Trim
                objG.Update_TablaEspecial(dCodigo, txtDescripcion.Text.Trim, Session("Ruta_Emp"))
            End If
            Call Lista_Tablas()
            btnCancelar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & ex.Message
        Finally

        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    'Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
    '    lblError.Text = ""
    '    Flex.PageIndex = e.NewPageIndex
    '    Call Lista_Tablas()
    'End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiqueta.Text = "Editar Tabla Especial"
            lblTablaEspecial.Visible = True
            lblCodigo.Text = Flex.Rows(Index).Cells(7).Text.Trim
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", "")
            txtPrefijo.Text = Flex.Rows(Index).Cells(2).Text.Trim
            Flex.Enabled = False
        ElseIf e.CommandName = "Tablas" Then
            lblTabla1.Text = Flex.Rows(Index).Cells(4).Text.Trim
            lblTabla2.Text = Flex.Rows(Index).Cells(5).Text.Trim
            lblTabla3.Text = Flex.Rows(Index).Cells(6).Text.Trim
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1
            Ficha_ActiveTabChanged(sender, e)
            Call Llenar_Combos()
        End If
    End Sub
    Protected Sub cboTabla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTabla.SelectedIndexChanged
        FlexTE.DataSource = Llenar_TablaEspecial(Right(cboTabla.SelectedItem.Text.Trim, 1))
        FlexTE.DataBind()
    End Sub
    Public Function Llenar_TablaEspecial(ByVal Tabla As String) As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim dRow As Data.DataRow
        Dim obj As New ModuloGeneral
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        dt.Columns.Add("c5")
        dt.Columns.Add("c6")
        dt.Columns.Add("c7")
        Try
            If Tabla = "1" Or Tabla = "2" Or Tabla = "3" Then btnNuevoTE.Enabled = True Else btnNuevoTE.Enabled = False
            If Tabla = "1" Then
                FlexTE.Columns(2).HeaderText = "Nivel 1"
                FlexTE.Columns(3).HeaderText = "" : FlexTE.Columns(4).HeaderText = ""
                FlexTE.Columns(5).HeaderText = "" : FlexTE.Columns(6).HeaderText = ""
                FlexTE.Columns(7).HeaderText = "" : FlexTE.Columns(8).HeaderText = ""
                FlexTE.Columns(2).ItemStyle.Width = 600
                FlexTE.Columns(3).ItemStyle.Width = 0 : FlexTE.Columns(4).ItemStyle.Width = 0
                FlexTE.Columns(5).ItemStyle.Width = 0 : FlexTE.Columns(6).ItemStyle.Width = 0
                FlexTE.Columns(7).ItemStyle.Width = 0 : FlexTE.Columns(8).ItemStyle.Width = 0
                Dim Rs As SqlClient.SqlDataReader
                Dim i As Integer = 0
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT NIVEL1_CODIGO,NIVEL1_DESCRIP From " & lblTabla1.Text.Trim & " WHERE (NIVEL1_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') ORDER BY NIVEL1_DESCRIP"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        dRow = dt.NewRow()
                        dRow(0) = Nu(Rs!NIVEL1_DESCRIP)
                        dRow(1) = Nu(Rs!NIVEL1_CODIGO)
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
                cboNivel1.Items.Add("< Seleccionar >") : cboNivel1.SelectedValue = "< Seleccionar >"
            ElseIf Tabla = "2" Then
                FlexTE.Columns(2).HeaderText = "Nivel 1"
                FlexTE.Columns(3).HeaderText = "Nivel 2" : FlexTE.Columns(4).HeaderText = ""
                FlexTE.Columns(5).HeaderText = "" : FlexTE.Columns(6).HeaderText = ""
                FlexTE.Columns(7).HeaderText = "" : FlexTE.Columns(8).HeaderText = ""
                FlexTE.Columns(2).ItemStyle.Width = 300
                FlexTE.Columns(3).ItemStyle.Width = 300 : FlexTE.Columns(4).ItemStyle.Width = 0
                FlexTE.Columns(5).ItemStyle.Width = 0 : FlexTE.Columns(6).ItemStyle.Width = 0
                FlexTE.Columns(7).ItemStyle.Width = 0 : FlexTE.Columns(8).ItemStyle.Width = 0
                Dim Rs As SqlClient.SqlDataReader
                Dim i As Integer = 0
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP,TB2.NIVEL1_CODIGO,TB2.NIVEL2_CODIGO " _
                                      & " FROM " & lblTabla2.Text.Trim & " TB2 INNER JOIN " & lblTabla1.Text.Trim & " TB1 " _
                                      & " ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                                      & " WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0')  " _
                                      & " AND (TB2.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') " _
                                      & " ORDER BY TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        dRow = dt.NewRow()
                        dRow(0) = Nu(Rs!NIVEL1_DESCRIP)
                        dRow(1) = Nu(Rs!NIVEL2_DESCRIP)
                        dRow(2) = Nu(Rs!NIVEL1_CODIGO)
                        dRow(3) = Nu(Rs!NIVEL2_CODIGO)
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
            ElseIf Tabla = "3" Then
                FlexTE.Columns(2).HeaderText = "Nivel 1" : FlexTE.Columns(3).HeaderText = "Nivel 2"
                FlexTE.Columns(4).HeaderText = "Nivel 3" : FlexTE.Columns(5).HeaderText = "Nivel de Servicio"
                FlexTE.Columns(6).HeaderText = "" : FlexTE.Columns(7).HeaderText = "" : FlexTE.Columns(8).HeaderText = ""
                FlexTE.Columns(2).ItemStyle.Width = 150 : FlexTE.Columns(3).ItemStyle.Width = 150
                FlexTE.Columns(4).ItemStyle.Width = 150 : FlexTE.Columns(5).ItemStyle.Width = 150
                FlexTE.Columns(6).ItemStyle.Width = 0 : FlexTE.Columns(7).ItemStyle.Width = 0 : FlexTE.Columns(8).ItemStyle.Width = 0
                Dim Rs As SqlClient.SqlDataReader
                Dim i As Integer = 0
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT TB3.NIVEL3_NS_DHM,TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP,TB3.NIVEL3_DESCRIP, TB2.NIVEL1_CODIGO,TB2.NIVEL2_CODIGO , TB3.NIVEL3_CODIGO " _
                                      & " FROM " & lblTabla2.Text.Trim & " TB2 INNER JOIN " & lblTabla1.Text.Trim & " TB1 ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                                      & " INNER JOIN " & lblTabla3.Text.Trim & " TB3 ON TB2.EMPRESA_CODIGO=TB3.EMPRESA_CODIGO AND TB2.NIVEL2_CODIGO = TB3.NIVEL2_CODIGO " _
                                      & " WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0') AND (TB3.NIVEL3_SYS_EST = '0')  AND (TB2.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') " _
                                      & " ORDER BY TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP, TB3.NIVEL3_DESCRIP "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        dRow = dt.NewRow()
                        dRow(0) = Nu(Rs!NIVEL1_DESCRIP)
                        dRow(1) = Nu(Rs!NIVEL2_DESCRIP)
                        dRow(2) = Nu(Rs!NIVEL3_DESCRIP)
                        If Len(Nu(Rs!NIVEL3_NS_DHM)) = 6 Then
                            dRow(3) = Left(Rs!NIVEL3_NS_DHM, 2) & " Dias " & Mid(Rs!NIVEL3_NS_DHM, 3, 2) & " Hrs " & Right(Rs!NIVEL3_NS_DHM, 2) & " Min"
                        End If
                        dRow(4) = Nu(Rs!NIVEL1_CODIGO)
                        dRow(5) = Nu(Rs!NIVEL2_CODIGO)
                        dRow(6) = Nu(Rs!NIVEL3_CODIGO)
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
            Else
                dt = Nothing
            End If
        Catch Ex As SqlException
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        Return dt
    End Function
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        FlexTE.Enabled = True
        lblIngresoTE.Visible = False
        FlexTE.DataSource = Nothing
        FlexTE.DataBind()
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0
        lblTablaEspecial.Visible = False
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    'Protected Sub FlexTE_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexTE.PageIndexChanging
    '    lblError.Text = ""
    '    FlexTE.PageIndex = e.NewPageIndex
    '    Call cboTabla_SelectedIndexChanged(sender, e)
    'End Sub
    Protected Sub btnNuevoTE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoTE.Click
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            lblIngresoTE.Visible = True
            lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial"
            txtTECodigo.Text = ""
            txtTEDescripcion.Text = ""
            If Right(cboTabla.SelectedItem.Text.Trim, 1) = "1" Then
                cboNivel1.Enabled = False : cboNivel2.Enabled = False : cboDias.Enabled = False
                cboHoras.Enabled = False : cboMin.Enabled = False
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL1_CODIGO) FROM " & cboTabla.SelectedItem.Text
            ElseIf Right(cboTabla.SelectedItem.Text.Trim, 1) = "2" Then
                cboNivel1.Enabled = True
                cboNivel2.Enabled = False
                cboDias.Enabled = False
                cboHoras.Enabled = False
                cboMin.Enabled = False
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL2_CODIGO) FROM " & cboTabla.SelectedItem.Text
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            ElseIf Right(cboTabla.SelectedItem.Text.Trim, 1) = "3" Then
                cboNivel1.Enabled = True
                cboNivel2.Enabled = True
                cboDias.Enabled = True
                cboHoras.Enabled = True
                cboMin.Enabled = True
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL3_CODIGO) FROM " & cboTabla.SelectedItem.Text
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            End If
            If CmdGlobal.CommandText = "" Then Exit Sub
            cboNivel2.Items.Add("< Seleccionar >") : cboNivel2.SelectedValue = "< Seleccionar >"
            btnNuevoTE.Enabled = False
            Flex.Enabled = False
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtTECodigo.Text = Nz(Rs(0)) + 1
                End While
            Else
                txtTECodigo.Text = "1"
            End If
            Rs.Close()
        Catch Ex As SqlException
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Llenar_Combos()
        cboDias.Items.Clear()
        cboHoras.Items.Clear()
        cboMin.Items.Clear()
        Dim i As Integer = 0
        Dim item As String = ""
        For i = 0 To 31
            item = Right("00" + CStr(i), 2)
            cboDias.Items.Add(item)
        Next
        For i = 0 To 24
            item = Right("00" + CStr(i), 2)
            cboHoras.Items.Add(item)
        Next
        For i = 0 To 59
            item = Right("00" + CStr(i), 2)
            cboMin.Items.Add(item)
        Next
        cboDias.Items.Add("- -") : cboDias.SelectedValue = "- -"
        cboHoras.Items.Add("- -") : cboHoras.SelectedValue = "- -"
        cboMin.Items.Add("- -") : cboMin.SelectedValue = "- -"
    End Sub
    Protected Sub cboNivel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNivel1.SelectedIndexChanged
        Try
            cboNivel2.Items.Clear()
            If cboNivel1.SelectedValue = "< Seleccionar >" Then cboNivel2.Items.Add("< Seleccionar >") : cboNivel2.SelectedValue = "< Seleccionar >" : Exit Sub
            Call LLenaComboItemTabEsp(cboNivel2, cboNivel1.SelectedValue.Trim, "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        Catch Ex As SqlException
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnTECancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTECancelar.Click
        lblErrorTE.Text = ""
        btnNuevoTE.Enabled = True
        FlexTE.Enabled = True
        lblIngresoTE.Visible = False
    End Sub
    Protected Sub btnTEGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTEGuardar.Click
        Dim Ntb As String
        Dim nTiempo As String
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim dCodigo As Double = 0
        Dim dNivel1 As Double = 0
        Dim dNivel2 As Double = 0
        lblErrorTE.Text = ""
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Ntb = Right(cboTabla.SelectedItem.Text, 1)
            dCodigo = txtTECodigo.Text.Trim
            If Ntb = "2" And cboNivel1.SelectedValue = "< Seleccionar >" Then lblErrorTE.Text = "Debe escoger el Primer Nivel para poder guardar." : Exit Sub
            If Ntb = "2" Then dNivel1 = cboNivel1.SelectedValue.Trim
            If Ntb = "3" And cboNivel1.SelectedValue = "< Seleccionar >" Then lblErrorTE.Text = "Debe escoger el Primer Nivel para poder guardar." : Exit Sub
            If Ntb = "3" And cboNivel2.SelectedValue = "< Seleccionar >" Then lblErrorTE.Text = "Debe escoger el Segundo Nivel para poder guardar." : Exit Sub
            If Ntb = "3" Then dNivel2 = cboNivel2.SelectedValue.Trim
            If txtTEDescripcion.Text.Trim = "" Then lblErrorTE.Text = "No podrá guardar hasta que no le haya ingresado una descripción" : Exit Sub
            nTiempo = ""
            If Ntb = "3" Then
                If (cboHoras.SelectedValue = "- -" And cboMin.SelectedValue <> "- -") Or (cboHoras.SelectedValue <> "- -" And cboMin.SelectedValue = "- -") Or
                   (cboHoras.SelectedValue = "- -" And cboDias.SelectedValue <> "- -") Or (cboHoras.SelectedValue <> "- -" And cboDias.SelectedValue = "- -") Or
                   (cboMin.SelectedValue = "- -" And cboDias.SelectedValue <> "- -") Or (cboMin.SelectedValue <> "- -" And cboDias.SelectedValue = "- -") Then lblErrorTE.Text = "El intervalo de tiempo no está correcto, favor de verificar o corregir." : Exit Sub
                If cboDias.SelectedValue = "- -" And cboHoras.SelectedValue = "- -" And cboMin.SelectedValue = "- -" Then
                Else
                    nTiempo = cboDias.SelectedItem.Text & cboHoras.SelectedItem.Text & cboMin.SelectedItem.Text
                End If
            End If
            If lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial" Or (lblEtiquetaTE.Text = "Editar Elemento de la Tabla Especial" And UCase(txtTEDescripcion.Text.Trim) <> UCase(txtTEDescripcionE.Text.Trim)) Then
                If Ntb = 1 Then
                    CmdGlobal.CommandText = "SELECT * FROM " & cboTabla.SelectedItem.Text & " WHERE (NIVEL1_DESCRIP)='" & UCase(txtTEDescripcion.Text.Trim) & "' AND NIVEL1_SYS_EST='0' AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                ElseIf Ntb = 2 Then
                    CmdGlobal.CommandText = "SELECT * FROM " & cboTabla.SelectedItem.Text & " WHERE (NIVEL2_DESCRIP)='" & UCase(txtTEDescripcion.Text.Trim) & "' AND NIVEL2_SYS_EST='0' AND NIVEL1_CODIGO=" & dNivel1 & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                ElseIf Ntb = 3 Then
                    CmdGlobal.CommandText = "SELECT * FROM " & cboTabla.SelectedItem.Text & " WHERE (NIVEL3_DESCRIP)='" & UCase(txtTEDescripcion.Text.Trim) & "' AND NIVEL3_SYS_EST='0' AND NIVEL2_CODIGO=" & dNivel2 & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                End If
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblErrorTE.Text = "Se ha encontrado una descripcion igual, verificar o cambiar para poder guardar" : Exit Sub
                    End While
                End If
                Rs.Close()
            End If
            If lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial" Then
                If Ntb = 1 Then
                    CmdGlobal2.CommandText = "INSERT INTO " & cboTabla.SelectedItem.Text & "(NIVEL1_CODIGO, NIVEL1_DESCRIP,NIVEL1_SYS_EST,EMPRESA_CODIGO) VALUES(" & dCodigo & ",'" & txtTEDescripcion.Text.Trim & "','0','" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                ElseIf Ntb = 2 Then
                    CmdGlobal2.CommandText = "INSERT INTO " & cboTabla.SelectedItem.Text & "(NIVEL1_CODIGO,NIVEL2_CODIGO, NIVEL2_DESCRIP,NIVEL2_SYS_EST,EMPRESA_CODIGO) VALUES(" & dNivel1 & "," & dCodigo & ",'" & txtTEDescripcion.Text.Trim & "','0','" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                ElseIf Ntb = 3 Then
                    CmdGlobal2.CommandText = "INSERT INTO " & cboTabla.SelectedItem.Text & "(NIVEL2_CODIGO,NIVEL3_CODIGO, NIVEL3_DESCRIP,NIVEL3_SYS_EST,EMPRESA_CODIGO,NIVEL3_NS_DHM) VALUES(" & dNivel2 & "," & dCodigo & ",'" & txtTEDescripcion.Text.Trim & "','0','" & Session("CodEmpresa") & "','" & nTiempo & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End If
            Else
                If Ntb = 1 Then
                    CmdGlobal2.CommandText = "UPDATE " & cboTabla.SelectedItem.Text & " SET NIVEL1_DESCRIP='" & txtTEDescripcion.Text.Trim & "' WHERE NIVEL1_CODIGO=" & dCodigo
                    CmdGlobal2.ExecuteNonQuery()
                ElseIf Ntb = 2 Then
                    CmdGlobal2.CommandText = "UPDATE " & cboTabla.SelectedItem.Text & " SET NIVEL2_DESCRIP='" & txtTEDescripcion.Text.Trim & "' WHERE NIVEL2_CODIGO=" & dCodigo
                    CmdGlobal2.ExecuteNonQuery()
                ElseIf Ntb = 3 Then
                    CmdGlobal2.CommandText = "UPDATE " & cboTabla.SelectedItem.Text & " SET NIVEL3_DESCRIP='" & txtTEDescripcion.Text.Trim & "',NIVEL3_NS_DHM='" & nTiempo & "' WHERE NIVEL3_CODIGO=" & dCodigo
                    CmdGlobal2.ExecuteNonQuery()
                End If
            End If
            cboTabla_SelectedIndexChanged(sender, e)
            btnTECancelar_Click(sender, e)
        Catch Ex As SqlException
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorTE.Visible = True
            lblErrorTE.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub FlexTE_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTE.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorTE.Text = ""
        If e.CommandName = "Editar" Then
            lblEtiquetaTE.Text = "Editar Elemento de la Tabla Especial"
            lblIngresoTE.Visible = True
            FlexTE.Enabled = False
            btnNuevoTE.Enabled = False
            Call Llenar_Combos()
            If Right(cboTabla.SelectedItem.Text, 1) = "1" Then
                cboNivel1.Enabled = False : cboNivel2.Enabled = False : cboDias.Enabled = False
                cboHoras.Enabled = False : cboMin.Enabled = False
                txtTECodigo.Text = FlexTE.Rows(Index).Cells(3).Text.Trim
                txtTEDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTE.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", "")
                txtTEDescripcionE.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTE.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", "")
                cboNivel1.SelectedValue = "< Seleccionar >"
                cboNivel2.SelectedValue = "< Seleccionar >"
            ElseIf Right(cboTabla.SelectedItem.Text, 1) = "2" Then
                cboNivel1.Enabled = True : cboNivel2.Enabled = False : cboDias.Enabled = False
                cboHoras.Enabled = False : cboMin.Enabled = False
                txtTECodigo.Text = FlexTE.Rows(Index).Cells(5).Text.Trim
                txtTEDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTE.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", "")
                txtTEDescripcionE.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTE.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", "")
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
                If FlexTE.Rows(Index).Cells(4).Text.Trim <> "&nbsp;" Then cboNivel1.SelectedValue = FlexTE.Rows(Index).Cells(4).Text.Trim : cboNivel1_SelectedIndexChanged(sender, e)
                cboNivel2.SelectedValue = "< Seleccionar >"
            ElseIf Right(cboTabla.SelectedItem.Text, 1) = "3" Then
                cboNivel1.Enabled = True : cboNivel2.Enabled = True : cboDias.Enabled = True
                cboHoras.Enabled = True : cboMin.Enabled = True
                txtTECodigo.Text = FlexTE.Rows(Index).Cells(8).Text.Trim
                txtTEDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTE.Rows(Index).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", "")
                txtTEDescripcionE.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTE.Rows(Index).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", "")
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
                If FlexTE.Rows(Index).Cells(6).Text.Trim <> "&nbsp;" Then cboNivel1.SelectedValue = FlexTE.Rows(Index).Cells(6).Text.Trim : cboNivel1_SelectedIndexChanged(sender, e)
                If FlexTE.Rows(Index).Cells(7).Text.Trim <> "&nbsp;" Then cboNivel2.SelectedValue = FlexTE.Rows(Index).Cells(7).Text.Trim
                If FlexTE.Rows(Index).Cells(5).Text.Trim <> "&nbsp;" Then cboDias.SelectedValue = Left(FlexTE.Rows(Index).Cells(5).Text.Trim, 2)
                If FlexTE.Rows(Index).Cells(5).Text.Trim <> "&nbsp;" Then cboHoras.SelectedValue = Mid(FlexTE.Rows(Index).Cells(5).Text.Trim, 9, 2)
                If FlexTE.Rows(Index).Cells(5).Text.Trim <> "&nbsp;" Then cboMin.SelectedValue = Mid(FlexTE.Rows(Index).Cells(5).Text.Trim, 16, 2)
            End If
        End If
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        If cboTabla.SelectedValue <> "< Seleccionar >" Then
            Dim NroTabla As String = Right(cboTabla.SelectedItem.Text.Trim, 1)
            Dim var_Tabla1 As String = lblTabla1.Text.Trim
            Dim var_Tabla2 As String = lblTabla2.Text.Trim
            Dim var_Tabla3 As String = lblTabla3.Text.Trim
            Response.Redirect("~/Sistema/SegSistem_Exportar_Datos.aspx?parametro=" & Server.UrlEncode(NroTabla) & "&var_Tabla1=" & Server.UrlEncode(var_Tabla1) & "&var_Tabla2=" & Server.UrlEncode(var_Tabla2) & "&var_Tabla3=" & Server.UrlEncode(var_Tabla3))
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay datos que exportar.');", True)
        End If
    End Sub
End Class
