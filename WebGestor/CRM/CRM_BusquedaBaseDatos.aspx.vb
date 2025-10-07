Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor

Partial Class CRM_CRM_BusquedaBaseDatos
    Inherits System.Web.UI.Page
    Dim ObjList As New ClsCRM_BaseDatos
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'btnTop10_Click(sender, e)
            Try
                Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
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
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
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
                        & " (SELECT NIVEL2_DESCRIP From dbo.TBESP_GTP2 WHERE (NIVEL2_CODIGO = CC.CARCON_PRODUCTO)) AS PRODUCTO, CC.CARCON_SUBPRODUCTO, " _
                        & " (SELECT NIVEL3_DESCRIP From dbo.TBESP_GTP3 WHERE (NIVEL3_CODIGO = CC.CARCON_SUBPRODUCTO)) AS SUBPRODUCTO, " _
                        & " CC.CARCON_TRANSACCION, CC.CARCON_CONSULTA, CC.CARCON_SOLUCION " _
                        & " FROM dbo.TBTICKET_CARTERA_CONSULTA AS CC INNER JOIN dbo.TBESP_GTP1 AS P1 " _
                        & " ON CC.EMPRESA_CODIGO = P1.EMPRESA_CODIGO AND CC.CARCON_APLICATIVO = P1.NIVEL1_CODIGO " _
                        & " WHERE (CC.EMPRESA_CODIGO = '0001') AND (CC.CARCON_SYS_EST = '0') " _
                        & " AND (P1.NIVEL1_SYS_EST = '0') AND (P1.EMPRESA_CODIGO = '0001')"
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
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Integer : pCodSubProd = 0
        Dim pCodSol As String = ""
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim Fila As GridViewRow
        Dim psRuta As String = ""
        Dim pCodigo As String = ""
        Dim ii As Integer = 0
        If (cboAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboAplicativo.Items(cboAplicativo.SelectedIndex).Value
        If (cboProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboProducto.Items(cboProducto.SelectedIndex).Value
        If (cboSubProducto.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboSubProducto.Items(cboSubProducto.SelectedIndex).Value
        Try
            Flex.DataSource = Cargar_datos(pCodApli, pCodProducto, pCodSubProd, 0)
            Flex.DataBind()
            If Flex.Rows.Count = 1 Then
                For ii = 0 To Flex.Rows.Count - 1
                    pCodSol = Flex.Rows(ii).Cells(2).Text.Trim
                    DetalleLista.DataSource = Cargar_datos(0, 0, 0, pCodSol)
                    DetalleLista.DataBind()
                    dt = ObjList.Crm_Busqueda_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), pCodSol)
                    DetalleArchivo.DataSource = dt
                    DetalleArchivo.DataBind()
                    psRuta = "Temas_" & Session("SiglaGrupoEmpresa")
                    For i = 0 To DetalleArchivo.Rows.Count - 1
                        pCodigo = DetalleArchivo.Rows(i).Cells(2).Text.Trim
                        dtListado = ObjList.Crm_BD_MuestraArchivo_xCodigo(pCodigo, Session("Ruta_Emp"), Session("CodEmpresa"))
                        If dtListado.Rows.Count > 0 Then
                            For Each drMenuItem As Data.DataRow In dtListado.Rows
                                Fila = DetalleArchivo.Rows(i)
                                Dim lbl As HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                                lbl.InnerHtml = "</b><A href='" & psRuta & "\" & Nu(drMenuItem("ARCHIVO")) & "'TARGET='_blank'>" & Nu(drMenuItem("ARCHIVO")) & "</A>"
                            Next
                        End If
                        dtListado = Nothing
                    Next
                Next
            End If
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Session("Modo") = "N"
        lblError1.Text = ""
        lblMensaje.Text = "Lista : Base de Datos"
        If cboAplicativo.SelectedValue = "< Seleccionar >" And Len(txtBuscador.Text) = 0 Then lblError1.Text = "Seleccionar al menos el Aplicativo o ingresar una palabra para la búsqueda." : Exit Sub
        Call Llenar_Grilla()
        DetalleLista.DataSource = Nothing
        DetalleLista.DataBind()
        DetalleArchivo.DataSource = Nothing
        DetalleArchivo.DataBind()
        End Sub
    Protected Sub cboAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAplicativo.SelectedIndexChanged
        lblError.Visible = False
        cboProducto.Items.Clear()
        cboSubProducto.Items.Clear()
        cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
        cboProducto.Enabled = False
        cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        cboSubProducto.Enabled = False
        If cboAplicativo.SelectedValue = "< Seleccionar >" Or cboAplicativo.Items.Count = 0 Then btnListar_Click(sender, e) : Exit Sub
        If cboAplicativo.Items(cboAplicativo.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboProducto, cboAplicativo.Items(cboAplicativo.SelectedIndex).Value, "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        cboProducto.Enabled = True
        cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        cboSubProducto.Enabled = False
        btnListar_Click(sender, e)
    End Sub
    Protected Sub cboProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProducto.SelectedIndexChanged
        lblError.Visible = False
        cboSubProducto.Items.Clear()
        cboSubProducto.Items.Add("< Seleccionar >") : cboSubProducto.SelectedValue = "< Seleccionar >"
        cboSubProducto.Enabled = False
        If cboProducto.SelectedValue = "< Seleccionar >" Or cboProducto.Items.Count = 0 Then btnListar_Click(sender, e) : Exit Sub
        If cboProducto.Items(cboProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboSubProducto, cboAplicativo.Items(cboAplicativo.SelectedIndex).Value, cboProducto.Items(cboProducto.SelectedIndex).Value, "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        cboSubProducto.Enabled = True
        btnListar_Click(sender, e)
    End Sub
    Protected Sub btnTop10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTop10.Click

        txtBuscador.Text = ""
        lblError1.Text = ""
        DetalleLista.DataSource = Nothing
        DetalleLista.DataBind()
        Session("Modo") = "S"
        lblError.Text = ""
        lblMensaje.Text = "TOP 10"
        Try
            Flex.DataSource = ObjList.CasLista_BaseDatosTop10(Session("CodEmpresa"), Session("Ruta_Emp"))
            Flex.DataBind()
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btn_Regresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Response.Redirect("Cas_PaginaPrincipal.aspx")
    End Sub
    Protected Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click
        'Response.Redirect("ConsultaBDaspx.aspx")
    End Sub
    Protected Sub Flex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Flex.SelectedIndexChanged

    End Sub

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pCodSol As String
        Dim dt As New DataTable
        Dim pFecha As String
        Dim pFechaFin As String
        Dim pFechaMes As String
        Dim pMes As String
        Dim pAño As String
        Dim dtListado As New DataTable
        Dim Fila As GridViewRow
        Dim psRuta As String = ""
        Dim pCodigo As String = ""
        If e.CommandName = "Detalle" Then
            pCodSol = Flex.Rows(Index).Cells(2).Text
            pMes = Mid(FechaActual, 5, 2)
            pAño = Left(FechaActual, 4)
            pFecha = Left(FechaActual, 4) & Mid(FechaActual, 5, 2) & "02"
            pFechaFin = DateAdd("d", -1, FormatoFecha(pFecha))
            pFechaMes = DateAdd("m", 1, pFechaFin)
            pFechaMes = Right(pFechaMes, 4) & Mid(pFechaMes, 4, 2) & Left(pFechaMes, 2)
            dt = ObjList.CasLista_ReiniciarContador(Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    If FechaActual() > Nu(drMenuItem("FECHA_FIN")) Then
                        ObjList.BaseDatos_Contador(Session("CodEmpresa"), Session("Ruta_Emp"), pCodSol, "0")
                        ObjList.InsUpd_FechaContador(pFecha, Session("Ruta_Emp"), pFechaMes, "0")
                        ObjList.InsUpd_ResumenMes(Session("CodEmpresa"), Session("Ruta_Emp"), pAño, pMes)
                    End If
                Next
            Else
                ObjList.InsUpd_FechaContador(pFecha, Session("Ruta_Emp"), pFechaMes, "1")
            End If
            dt = Nothing
            ObjList.BaseDatos_Contador(Session("CodEmpresa"), Session("Ruta_Emp"), pCodSol, "1")
            Try
                DetalleLista.DataSource = Cargar_datos(0, 0, 0, pCodSol)
                DetalleLista.DataBind()
                dt = ObjList.Crm_Busqueda_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), pCodSol)
                DetalleArchivo.DataSource = dt
                DetalleArchivo.DataBind()
                psRuta = "Temas_" & Session("SiglaGrupoEmpresa")
                For i = 0 To DetalleArchivo.Rows.Count - 1
                    pCodigo = DetalleArchivo.Rows(i).Cells(2).Text.Trim
                    dtListado = ObjList.Crm_BD_MuestraArchivo_xCodigo(pCodigo, Session("Ruta_Emp"), Session("CodEmpresa"))
                    If dtListado.Rows.Count > 0 Then
                        For Each drMenuItem As Data.DataRow In dtListado.Rows
                            Fila = DetalleArchivo.Rows(i)
                            Dim lbl As HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                            lbl.InnerHtml = "</b><A href='" & psRuta & "\" & Nu(drMenuItem("ARCHIVO")) & "'TARGET='_blank'>" & Nu(drMenuItem("ARCHIVO")) & "</A>"
                        Next
                    End If
                    dtListado = Nothing
                Next
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
    Protected Sub cboSubProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSubProducto.SelectedIndexChanged
        btnListar_Click(sender, e)
    End Sub
End Class
