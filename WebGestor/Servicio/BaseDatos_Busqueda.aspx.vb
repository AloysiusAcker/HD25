Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class BaseDatos_Busqueda
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
                btnTop10_Click(sender, e)
                Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), psConexion)
                cboAplicativo.SelectedValue = "< Seleccionar >"
                Call cboAplicativo_SelectedIndexChanged(sender, e)
                cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
                cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
                cboProducto.Enabled = False
                cboSubProducto.Enabled = False
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
    Private Function Cargar_datos(ByVal pCodApli As Double, ByVal pCodProducto As Double, ByVal pCodSubProd As Double, ByVal pCodSolucion As Double) As DataTable
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        Dim Cn2 As New SqlConnection(psConexion)
        Dim Sql As String : Sql = ""
        Dim Filtros1 As String : Filtros1 = ""
        Dim Filtros2 As String : Filtros2 = ""
        Dim Opera As String
        Dim Campo1 As String
        Dim Campo2 As String
        Dim cmdSql As New SqlCommand
        Cargar_datos = Nothing
        'Opera = " OR "
        If Trim(txtBuscador.Text.Trim) <> "" And optModoBus.SelectedIndex = -1 Then lblError.Text = "Debe seleccionar un Modo de Busqueda." : Exit Function
        Campo1 = "UPPER(CARCON_TRANSACCION) LIKE "
        Campo2 = "UPPER(CARCON_CONSULTA) LIKE "
        If optModoBus.SelectedValue = 1 Then Opera = " AND " Else Opera = " OR "
        If Trim(txtBuscador.Text.Trim) <> "" Then
            Filtros1 = ArmaFiltros(txtBuscador.Text.Trim, Campo1, Opera)
            Filtros2 = ArmaFiltros(txtBuscador.Text.Trim, Campo2, Opera)
        End If
        Cn2.Open()
        cmdSql.Connection = Cn2
        cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[Lista]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[Lista]"
        cmdSql.ExecuteNonQuery()
        cmdSql.CommandText = "CREATE VIEW Lista AS SELECT CC.EMPRESA_CODIGO, CC.CARCON_SYS_EST, CC.CARCON_CODIGO, CC.CARCON_APLICATIVO, P1.NIVEL1_DESCRIP, CC.CARCON_PRODUCTO, " _
                        & " (SELECT NIVEL2_DESCRIP From dbo.TBESP_CAS2 WHERE (NIVEL2_CODIGO = CC.CARCON_PRODUCTO)) AS PRODUCTO, CC.CARCON_SUBPRODUCTO, " _
                        & " (SELECT NIVEL3_DESCRIP From dbo.TBESP_CAS3 WHERE (NIVEL3_CODIGO = CC.CARCON_SUBPRODUCTO)) AS SUBPRODUCTO, " _
                        & " CC.CARCON_TRANSACCION, CC.CARCON_CONSULTA, CC.CARCON_SOLUCION " _
                        & " FROM dbo.TBCAS_CARTERA_CONSULTA AS CC INNER JOIN dbo.TBESP_CAS1 AS P1 " _
                        & " ON CC.EMPRESA_CODIGO = P1.EMPRESA_CODIGO AND CC.CARCON_APLICATIVO = P1.NIVEL1_CODIGO " _
                        & " WHERE (CC.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CC.CARCON_SYS_EST = '0') " _
                        & " AND (P1.NIVEL1_SYS_EST = '0') AND (P1.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
        If pCodApli <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND  (CARCON_APLICATIVO = " & pCodApli & ") "
        If pCodProducto <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND (CARCON_PRODUCTO   = " & pCodProducto & ") "
        If pCodSubProd <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND (CARCON_SUBPRODUCTO= " & pCodSubProd & ")"
        If pCodSolucion <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND (CARCON_CODIGO= " & pCodSolucion & ")"
        cmdSql.ExecuteNonQuery()
        Sql = " select NIVEL1_DESCRIP, Producto,CARCON_APLICATIVO, subproducto, CARCON_TRANSACCION,CARCON_SUBPRODUCTO, CARCON_CONSULTA, CARCON_SOLUCION,CARCON_PRODUCTO, CARCON_CODIGO " _
            & " FROM Lista WHERE (EMPRESA_CODIGO = '0001') AND (CARCON_SYS_EST = '0')"
        If Trim(txtBuscador.Text.Trim) <> "" Then Sql = Sql & " AND " & Filtros1
        If Trim(txtBuscador.Text.Trim) <> "" Then Sql = Sql & " OR " & Filtros2
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Return Dt
    End Function
    Private Sub Llenar_Grilla()
        Dim obj As New Listados
        lblError.Text = ""
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Integer : pCodSubProd = 0
        Try
            If (cboAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboAplicativo.SelectedValue.Trim
            If (cboProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboProducto.SelectedValue.Trim
            If (cboSubProducto.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboSubProducto.SelectedValue.Trim
            lblDetalle.Visible = False
            DetalleLista.DataSource = Nothing
            DetalleLista.DataBind()
            Flex.DataSource = Cargar_datos(pCodApli, pCodProducto, pCodSubProd, 0)
            Flex.DataBind()
            lblCount.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        If Session("Modo") = "S" Then btnTop10_Click(sender, e) Else Call Llenar_Grilla()
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Session("Modo") = "N"
        Call Llenar_Grilla()
    End Sub
    Protected Sub cboAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAplicativo.SelectedIndexChanged
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Visible = False
        cboProducto.Items.Clear()
        cboSubProducto.Items.Clear()
        cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
        cboProducto.Enabled = False
        cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        cboSubProducto.Enabled = False
        If cboAplicativo.SelectedIndex = -1 Or cboAplicativo.Items.Count = 0 Then Exit Sub
        If cboAplicativo.Items(cboAplicativo.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboProducto, cboAplicativo.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), psConexion)
        If cboAplicativo.SelectedValue = "< Seleccionar >" Then
            cboProducto.Enabled = False
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProducto.Enabled = False
            cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        Else
            cboProducto.Enabled = True
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProducto.Enabled = False
            cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub cboProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProducto.SelectedIndexChanged
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Visible = False
        cboSubProducto.Items.Clear()
        cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        cboSubProducto.Enabled = False
        If cboProducto.SelectedIndex = -1 Or cboProducto.Items.Count = 0 Then Exit Sub
        If cboProducto.Items(cboProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboSubProducto, cboAplicativo.SelectedValue.Trim, cboProducto.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), psConexion)
        If cboProducto.SelectedValue = "< Seleccionar >" Then
            cboSubProducto.Enabled = False
            cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        Else
            cboSubProducto.Enabled = True
            cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pCodSol As String
        Dim dt As New DataTable
        Dim pFecha As String
        Dim pFechaFin As String
        Dim pFechaMes As String
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If e.CommandName = "Detalle" Then
            lblDetalle.Visible = True
            pCodSol = Flex.Rows(Index).Cells(7).Text
            Dim obj As New ModuloGeneral
            Dim obj2 As New Listados
            pFecha = Left(FechaActual, 4) & Mid(FechaActual, 5, 2) & "02"
            pFechaFin = DateAdd("d", -1, FormatoFecha(pFecha))
            pFechaMes = DateAdd("m", 1, pFechaFin)
            pFechaMes = Right(pFechaMes, 4) & Mid(pFechaMes, 4, 2) & Left(pFechaMes, 2)
            dt = obj.BDC_Lista_ReiniciarContador(psConexion)
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    If FechaActual() > Nu(drMenuItem("FECHA_FIN")) Then
                        obj.BDC_InsUpd_Contador(Session("CodEmpresa"), pCodSol, "0", psConexion)
                        obj.BDC_InsUpd_FechaContador(pFecha, pFechaMes, "0", psConexion)
                    End If
                Next
            Else
                obj.BDC_InsUpd_FechaContador(pFecha, pFechaMes, "1", psConexion)
            End If
            dt = Nothing
            obj.BDC_InsUpd_Contador(Session("CodEmpresa"), pCodSol, "1", psConexion)
            Try
                DetalleLista.DataSource = Cargar_datos(0, 0, 0, pCodSol)
                DetalleLista.DataBind()
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
    Protected Sub btnTop10_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ModuloGeneral
        Try
            Session("Modo") = "S"
            lblError.Text = ""
            lblDetalle.Visible = False
            DetalleLista.DataSource = Nothing
            DetalleLista.DataBind()
            Flex.DataSource = obj.BDC_Top10("0001", ConfigurationManager.AppSettings("cnTecnicos"))
            Flex.DataBind()
            lblCount.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
End Class
