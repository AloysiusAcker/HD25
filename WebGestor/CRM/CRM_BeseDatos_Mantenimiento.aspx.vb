Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class CRM_CRM_BeseDatos_Mantenimiento
    Inherits System.Web.UI.Page
    Dim ObjList As New ClsCRM_BaseDatos

    Function TablaMenu() As DataTable
        TablaMenu = Nothing
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn

            CmdGlobal.CommandText = " SELECT MW.MENWEB_DESCRIPCION AS DESCRIPCION, MW.MENWEB_CODIGO AS CODIGO , " _
                                    & " (SELECT PAG_NOMBRE  FROM TBPAGINAS WHERE PAG_CODIGO = PAGINA_CODIGO AND PAG_SYS_EST = '0') AS PAGINA_ASPX,  " _
                                    & " MW.MENWEB_NIVEL, MW.MENWEB_CODIGO_N1,  MW.MENWEB_CODIGO_N2 " _
                                    & " FROM dbo.TBSISTEMA_MENU_WEB AS MW " _
                                    & " WHERE (MENWEB_SYS_EST ='0' ) and  MW.MENWEB_NIVEL ='0' "
            CmdGlobal.CommandText = CmdGlobal.CommandText & " ORDER BY DESCRIPCION "
            Da = New SqlDataAdapter(CmdGlobal)
            Da.Fill(dt)

            dt.PrimaryKey = New DataColumn() {dt.Columns("CODIGO")}
            Return dt
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Function TablaSubMenu(ByVal psMenuCod As String, ByVal psMenuNivel As String) As DataTable
        TablaSubMenu = Nothing
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim Da As SqlDataAdapter
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn

            CmdGlobal.CommandText = " SELECT MW.MENWEB_DESCRIPCION AS DESCRIPCION, MW.MENWEB_CODIGO AS CODIGO , " _
                                    & " (SELECT PAG_NOMBRE  FROM TBPAGINAS WHERE PAG_CODIGO = PAGINA_CODIGO AND PAG_SYS_EST = '0') AS PAGINA_ASPX,  " _
                                    & " MW.MENWEB_NIVEL, MW.MENWEB_CODIGO_N1,  MW.MENWEB_CODIGO_N2 " _
                                    & " FROM dbo.TBSISTEMA_MENU_WEB AS MW " _
                                    & " WHERE (MENWEB_SYS_EST ='0' ) and MW.MENWEB_NIVEL = '" & psMenuNivel & "'"
            If psMenuCod <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " and MW.MENWEB_CODIGO_N" & psMenuNivel & " = " & psMenuCod
            CmdGlobal.CommandText = CmdGlobal.CommandText & " ORDER BY DESCRIPCION "
            Da = New SqlDataAdapter(CmdGlobal)
            Da.Fill(dt)

            dt.PrimaryKey = New DataColumn() {dt.Columns("CODIGO")}
            Return dt

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
    End Function

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Call Limpiar()
        lblIngreso.Visible = True
        lblEtiqueta.Text = "Ingresar Base de Datos"
        Try
            Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            Dim contador As Integer = cboAplicativo.Items.Count()
            If contador > 0 Then
                cboAplicativo.SelectedValue = "< Seleccionar >"
            Else
                cboAplicativo.Items.Clear()
            End If
            Call cboAplicativo_SelectedIndexChanged(sender, e)
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
            cboProducto.Enabled = False
            cboSubProd.Enabled = False

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Limpiar()
        lblEtiqueta.Text = ""
        btnListar.Enabled = False
        btnNuevo.Enabled = False
        btnGuardar.Visible = True
        btnCancelar.Visible = True
        lblEtiqueta.Visible = True
        lblEtiqueta1.Visible = True
        lblEtiqueta2.Visible = True
        lblEtiqueta3.Visible = True
        lblEtiqueta4.Visible = True
        lblEtiqueta5.Visible = True
        lblEtiqueta6.Visible = True
        cboAplicativo.Visible = True
        cboProducto.Visible = True
        cboSubProd.Visible = True
        txtTransaccion.Visible = True : txtTransaccion.Text = ""
        txtConsulta.Visible = True : txtConsulta.Text = ""
        txtSolucion.Visible = True : txtSolucion.Text = ""
    End Sub
    Protected Sub cboAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAplicativo.SelectedIndexChanged
        lblError.Visible = False
        cboProducto.Items.Clear()
        cboSubProd.Items.Clear()
        cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
        cboProducto.Enabled = False
        cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        cboSubProd.Enabled = False
        Call LLenaComboItemTabEsp(cboProducto, cboAplicativo.SelectedValue.Trim, "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboAplicativo.SelectedValue = "< Seleccionar >" Then
            cboProducto.Enabled = False
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Enabled = False
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboProducto.Enabled = True
            cboSubProd.Enabled = False
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProducto.SelectedIndexChanged
        lblError.Visible = False
        cboSubProd.Items.Clear()
        cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        cboSubProd.Enabled = False
        If cboProducto.SelectedIndex = -1 Or cboProducto.Items.Count = 0 Then Exit Sub
        If cboProducto.Items(cboProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboSubProd, cboAplicativo.SelectedValue.Trim, cboProducto.SelectedValue.Trim, "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboProducto.SelectedValue = "< Seleccionar >" Then
            cboSubProd.Enabled = False
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboSubProd.Enabled = True
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New ClsCRM_BaseDatos
        lblError.Text = ""
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Integer : pCodSubProd = 0
        If (cboBusAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboBusAplicativo.SelectedValue.Trim
        If (cboBusProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboBusProducto.SelectedValue.Trim
        If (cboBusSubProd.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboBusSubProd.SelectedValue.Trim
        Try
            Flex.DataSource = obj.CasLista_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), pCodApli, pCodProducto, pCodSubProd)
            Flex.DataBind()
            'grid.DataSource = obj.CasLista_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), pCodApli, pCodProducto, pCodSubProd)
            'grid.DataBind()
            If obj.CasLista_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), pCodApli, pCodProducto, pCodSubProd).Rows.Count > 0 Then
                lblCount.Text = "Total Registros : " & obj.CasLista_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), pCodApli, pCodProducto, pCodSubProd).Rows.Count
            Else
                lblCount.Text = "No se encontraron registros"
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Call LLenaComboItemTabEsp(cboBusAplicativo, "", "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
                cboBusAplicativo.SelectedValue = "< Seleccionar >"
                cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
                cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
                Call cboBusAplicativo_SelectedIndexChanged(sender, e)
                cboBusProducto.Enabled = False
                cboBusSubProd.Enabled = False

            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
            Finally
            End Try
            Call Llenar_Grilla()
            'Dim dtPadreID As DataTable
            'dtPadreID = TablaMenu()

            'Dim dtHijos As DataTable

            'For Each drow As DataRow In dtPadreID.Rows
            '    Dim itemData As String = drow("descripcion").ToString
            '    Dim mainItem = ASPxMenu2.Items.Add(itemData)
            '    mainItem.NavigateUrl = drow("PAGINA_ASPX").ToString
            '    Dim psNivelI As Integer = CDbl(drow("MENWEB_NIVEL").ToString) + 1
            '    Dim psNivel As String = psNivelI
            '    dtHijos = TablaSubMenu(drow("CODIGO").ToString, psNivel)
            '    For Each drowH As DataRow In dtHijos.Rows
            '        Dim itemDataSub As String = drowH("descripcion").ToString
            '        Dim mainSubItem = mainItem.Items.Add(itemDataSub)
            '        mainSubItem.NavigateUrl = drowH("PAGINA_ASPX").ToString
            '    Next
            'Next
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboBusAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBusAplicativo.SelectedIndexChanged
        lblError.Visible = False
        cboBusProducto.Items.Clear()
        cboBusSubProd.Items.Clear()
        cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
        cboBusProducto.Enabled = False
        cboBusSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        cboBusSubProd.Enabled = False
        If cboBusAplicativo.SelectedValue <> "< Seleccionar >" Then
            Call LLenaComboItemTabEsp(cboBusProducto, cboBusAplicativo.SelectedValue.Trim, "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
            If cboBusAplicativo.SelectedValue = "< Seleccionar >" Then
                cboBusProducto.Enabled = False
                cboBusProducto.SelectedValue = "< Seleccionar >"
                cboBusSubProd.Enabled = False
                cboBusSubProd.SelectedValue = "< Seleccionar >"
            Else
                cboBusProducto.Enabled = True
                cboBusSubProd.Enabled = False
            End If
        End If
        btnListar_Click(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboBusProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBusProducto.SelectedIndexChanged
        lblError.Visible = False
        cboBusSubProd.Items.Clear()
        cboBusSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        cboBusSubProd.Enabled = False
        If cboBusProducto.SelectedIndex = -1 Or cboBusProducto.Items.Count = 0 Then Exit Sub
        If cboBusProducto.Items(cboBusProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboBusSubProd, cboBusAplicativo.SelectedValue.Trim, cboBusProducto.SelectedValue.Trim, "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboBusProducto.SelectedValue = "< Seleccionar >" Then
            cboBusSubProd.Enabled = False
            cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboBusSubProd.Enabled = True
        End If
        btnListar_Click(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        lblEtiqueta.Text = ""
        btnListar.Enabled = True
        btnNuevo.Enabled = True
        btnGuardar.Visible = False
        btnCancelar.Visible = False
        lblEtiqueta.Visible = False
        lblEtiqueta1.Visible = False
        lblEtiqueta2.Visible = False
        lblEtiqueta3.Visible = False
        lblEtiqueta4.Visible = False
        lblEtiqueta5.Visible = False
        lblEtiqueta6.Visible = False
        cboAplicativo.Visible = False
        cboProducto.Visible = False
        cboSubProd.Visible = False
        txtTransaccion.Visible = False : txtTransaccion.Text = ""
        txtConsulta.Visible = False : txtConsulta.Text = ""
        txtSolucion.Visible = False : txtSolucion.Text = ""
        lblIngreso.Visible = False
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Double : pCodSubProd = 0
        Dim pCodBaseDatos As Integer : pCodBaseDatos = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim strSaveFileAs As String = ""
        Dim strSaveFileAsOrigen As String = ""
        Dim dt As New DataTable
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        lblError.Text = ""
        If cboAplicativo.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & " <br> - Seleccionar Aplicatico."
        If Trim(txtTransaccion.Text) = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Transacción."
        If Trim(txtConsulta.Text) = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Consulta."
        If Trim(txtSolucion.Text) = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Solución."
        If lblError.Text.Trim <> "" Then
            lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
            Exit Sub
        End If
        Dim obj As New ClsCRM_BaseDatos
        If (cboAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboAplicativo.SelectedValue.Trim
        If (cboProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboProducto.SelectedValue.Trim
        If (cboSubProd.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboSubProd.SelectedValue.Trim
        Try
            If lblEtiqueta.Text = "Ingresar Base de Datos" Then
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT MAX(CARCON_CODIGO) FROM TBTICKET_CARTERA_CONSULTA "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pCodBaseDatos = Nz(Rs(0)) + 1
                    End While
                Else
                    pCodBaseDatos = 1
                End If
                Rs.Close()
                obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, 0, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "1", Session("Ruta_Emp"))
                If pCodProducto <> 0 Then obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                If pCodSubProd <> 0 Then obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, pCodSubProd, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                Call btnCancelar_Click(sender, e)
                Call btnListar_Click(sender, e)
            Else
                pCodBaseDatos = txtCodConsulta.Text
                obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, 0, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                If pCodProducto <> 0 Then obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                If pCodSubProd <> 0 Then obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, pCodSubProd, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                Call btnCancelar_Click(sender, e)
                Call btnListar_Click(sender, e)
            End If
            Dim psArchivo As String = ""
            Dim psRuta As String = ""
            For i = 0 To GvArchivo.Rows.Count - 1
                If Replace(GvArchivo.Rows(i).Cells(2).Text, "&nbsp;", "") <> "" And Replace(GvArchivo.Rows(i).Cells(3).Text, "&nbsp;", "") = "" Then
                    psRuta = "\\" & NomServer & "\Temas_" & Session("SiglaGrupoEmpresa")
                    psArchivo = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                    ObjList.CRM_InsUpd_Archivo(Session("CodEmpresa"), Session("Ruta_Emp"), pCodBaseDatos, psArchivo, psRuta, "", "", Session("User"))

                    If Directory.Exists(Dir(psRuta)) = True Then
                        strSaveFileAs = psRuta & "\" & psArchivo
                    Else
                        Directory.CreateDirectory(psRuta)
                        strSaveFileAs = psRuta & "\" & psArchivo
                    End If

                    strSaveFileAsOrigen = Server.MapPath("Temas_" & Session("SiglaGrupoEmpresa")) & "\" & psArchivo
                    FileCopy(strSaveFileAsOrigen, strSaveFileAs)
                    'Kill(strSaveFileAsOrigen)

                End If
            Next
            dt = Nothing

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
        'Response.Redirect("Cas_Definicion_CarteraConsulta.aspx")
    End Sub
    Private Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblError.Text = ""
        Dim pdCodCartera As String = ""
        Dim psRuta As String = ""
        Dim pCodigo As String = ""
        Dim Fila As GridViewRow
        Dim dtListado As New DataTable
        Dim dt As New DataTable
        'Dim Rs As SqlDataReader
        Dim psCodCartera As String = ""
        If e.CommandName = "Editar" Then
            lblIngreso.Visible = True
            Call Limpiar()
            lblEtiqueta.Text = "Edición de Base de Datos"
            psCodCartera = Flex.Rows(Index).Cells(8).Text
            txtCodConsulta.Text = Flex.Rows(Index).Cells(8).Text
            Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 1, Session("CodEmpresa"), Session("Ruta_Emp")) '"&#241;"
            If Flex.Rows(Index).Cells(9).Text <> "&nbsp;" Then
                If cboAplicativo.Items.FindByValue(Flex.Rows(Index).Cells(9).Text) IsNot Nothing Then
                    cboAplicativo.SelectedValue = Flex.Rows(Index).Cells(9).Text
                    cboAplicativo_SelectedIndexChanged(sender, e)
                End If
            End If
            If Flex.Rows(Index).Cells(10).Text <> "&nbsp;" Then
                If cboProducto.Items.FindByValue(Flex.Rows(Index).Cells(10).Text) IsNot Nothing Then
                    cboProducto.SelectedValue = Flex.Rows(Index).Cells(10).Text
                    cboProducto_SelectedIndexChanged(sender, e)
                End If
            End If
            If Flex.Rows(Index).Cells(11).Text <> "&nbsp;" Then
                If cboSubProd.Items.FindByValue(Flex.Rows(Index).Cells(11).Text) IsNot Nothing Then
                    cboSubProd.SelectedValue = Flex.Rows(Index).Cells(11).Text
                End If
            End If
            txtTransaccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtConsulta.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtSolucion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            dt = ObjList.Crm_Busqueda_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), psCodCartera)
            GvArchivo.DataSource = dt
            GvArchivo.DataBind()
            psRuta = "Temas_" & Session("SiglaGrupoEmpresa")
            Dim psEtiqueta As String = "Ver"
            For i = 0 To GvArchivo.Rows.Count - 1
                pCodigo = GvArchivo.Rows(i).Cells(4).Text.Trim
                dtListado = ObjList.Crm_BD_MuestraArchivo_xCodigo(pCodigo, Session("Ruta_Emp"), Session("CodEmpresa"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = GvArchivo.Rows(i)
                        Dim lbl As HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='" & psRuta & "\" & Nu(drMenuItem("ARCHIVO")) & "'TARGET='_blank'>" & psEtiqueta & "</A>"
                    Next
                End If
            Next
        End If
        If e.CommandName = "Archivos" Then
            LblEtiq35.Text = ""
            pdCodCartera = Flex.Rows(Index).Cells(8).Text
            TxtMAplicativo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            TxtMProducto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            TxtMSubProducto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            TxtMTransac.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            TxtMConsulta.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            TxtMSolucion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            dt = ObjList.Crm_Busqueda_BaseDatos(Session("CodEmpresa"), Session("Ruta_Emp"), pdCodCartera)
            FlexDetalle.DataSource = dt
            FlexDetalle.DataBind()
            psRuta = "Temas_" & Session("SiglaGrupoEmpresa")
            For i = 0 To FlexDetalle.Rows.Count - 1
                pCodigo = FlexDetalle.Rows(i).Cells(3).Text.Trim
                dtListado = ObjList.Crm_BD_MuestraArchivo_xCodigo(pCodigo, Session("Ruta_Emp"), Session("CodEmpresa"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = FlexDetalle.Rows(i)
                        Dim lbl As HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='" & psRuta & "\" & Nu(drMenuItem("ARCHIVO")) & "'TARGET='_blank'>" & Nu(drMenuItem("ARCHIVO")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count = 1 Then
                    LblEtiq35.Text = "Hay 1 archivo."
                Else
                    LblEtiq35.Text = "Hay " & dt.Rows.Count & " archivos."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
            End If
        End If
    End Sub
    Protected Sub BtnArchivo_Click(sender As Object, e As EventArgs) Handles BtnArchivo.Click

        Dim i As Integer = 0
        Dim dtListado As New Data.DataTable
        Dim drT As Data.DataRow
        Dim pCodigo As String = ""
        Dim objCas As New ModuloCas
        Dim Fila As GridViewRow
        Dim psRuta As String = ""
        lblError.Text = ""
        dtListado.Columns.Add("ARCHIVO")
        dtListado.Columns.Add("CARCON_CODIGO")
        dtListado.Columns.Add("CODIGO")

        Try

            If FileUpload1.HasFile = False Then
                lblError.Text = "Seleccionar un archivo..."
            Else

                If GvArchivo.Rows.Count > 0 Then
                    For i = 0 To GvArchivo.Rows.Count - 1
                        If FileUpload1.FileName = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") Then
                            lblError.Text = "El archivo ya se encuentra." : Exit Sub
                        End If
                    Next
                End If

                If GvArchivo.Rows.Count > 0 Then
                    For i = 0 To GvArchivo.Rows.Count - 1
                        drT = dtListado.NewRow()
                        drT("ARCHIVO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        drT("CARCON_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        drT("CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        dtListado.Rows.Add(drT)
                    Next
                End If

                Dim strSaveFileAs As String = ""

                strSaveFileAs = Server.MapPath("Temas_" & Session("SiglaGrupoEmpresa")) ' "\\" & NomServer & "\Temas_" & Session("SiglaGrupoEmpresa")  ' "\\DATA\\Archivos\" + Upload.FileName 

                If Directory.Exists(Dir(strSaveFileAs)) = True Then
                    FileUpload1.SaveAs(strSaveFileAs & "\" & FileUpload1.FileName)
                Else
                    Directory.CreateDirectory(strSaveFileAs)
                    FileUpload1.SaveAs(strSaveFileAs & "\" & FileUpload1.FileName)
                End If

                drT = dtListado.NewRow()
                drT("ARCHIVO") = FileUpload1.FileName  'FuArchivo.FileName.ToString
                drT("CARCON_CODIGO") = ""
                drT("CODIGO") = ""
                dtListado.Rows.Add(drT)

                GvArchivo.DataSource = dtListado
                GvArchivo.DataBind()
                Dim psEtiqueta As String = "Ver"
                psRuta = "Temas_" & Session("SiglaGrupoEmpresa")
                For i = 0 To GvArchivo.Rows.Count - 1
                    pCodigo = Replace(GvArchivo.Rows(i).Cells(4).Text.Trim, "&nbsp;", "")
                    If pCodigo <> "" Then dtListado = ObjList.Crm_BD_MuestraArchivo_xCodigo(pCodigo, Session("Ruta_Emp"), Session("CodEmpresa"))
                    If dtListado.Rows.Count > 0 Then
                        For Each drMenuItem As Data.DataRow In dtListado.Rows
                            Fila = GvArchivo.Rows(i)
                            Dim lbl As HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                            lbl.InnerHtml = "</b><A href='" & psRuta & "\" & Nu(drMenuItem("ARCHIVO")) & "'TARGET='_blank'>" & psEtiqueta & "</A>"
                        Next
                    End If
                    dtListado = Nothing
                Next
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Exportar_Excel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Flex.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(Flex)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Base de Datos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub cboBusSubProd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBusSubProd.SelectedIndexChanged

        btnListar_Click(sender, e)
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub

    Private Sub BtnNuevaTE_Click(sender As Object, e As EventArgs) Handles BtnNuevaTE.Click
        lblTabla1.Text = "TBESP_GTP1"
        lblTabla2.Text = "TBESP_GTP2"
        lblTabla3.Text = "TBESP_GTP3"
        lblIngresoTE.Visible = False
        btnTENuevo.Enabled = False
        cboTabla.Items.Clear()
        cboTabla.Items.Add(lblTabla1.Text.Trim)
        cboTabla.Items.Add(lblTabla2.Text.Trim)
        cboTabla.Items.Add(lblTabla3.Text.Trim)
        cboTabla.Items.Add("< Seleccionar >") : cboTabla.SelectedValue = "< Seleccionar >"
        cboTabla.Enabled = True
        btnTEGuardar.Visible = False
        btnTECancelar.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTablaEsp').modal('show');", True)
    End Sub
    Protected Sub cboTabla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTabla.SelectedIndexChanged
        'FlexTE.DataSource = Llenar_TablaEspecial(Right(cboTabla.SelectedItem.Text.Trim, 1))
        'FlexTE.DataBind()
        If Right(cboTabla.SelectedItem.Text.Trim, 1) = "1" Or Right(cboTabla.SelectedItem.Text.Trim, 1) = "2" Or Right(cboTabla.SelectedItem.Text.Trim, 1) = "3" Then btnTENuevo.Enabled = True Else btnTENuevo.Enabled = False
        cboNivel1.Items.Add("< Seleccionar >") : cboNivel1.SelectedValue = "< Seleccionar >"
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
        Try
            If Tabla = "1" Or Tabla = "2" Or Tabla = "3" Then btnTENuevo.Enabled = True Else btnTENuevo.Enabled = False
            If Tabla = "1" Then
                FlexTE.Columns(0).HeaderText = "Nivel 1"
                FlexTE.Columns(1).HeaderText = "" : FlexTE.Columns(2).HeaderText = ""
                FlexTE.Columns(3).HeaderText = ""
                FlexTE.Columns(0).ItemStyle.Width = 600
                FlexTE.Columns(1).ItemStyle.Width = 0 : FlexTE.Columns(2).ItemStyle.Width = 0
                FlexTE.Columns(3).ItemStyle.Width = 0
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
                FlexTE.Columns(0).HeaderText = "Nivel 1"
                FlexTE.Columns(1).HeaderText = "Nivel 2" : FlexTE.Columns(2).HeaderText = ""
                FlexTE.Columns(3).HeaderText = ""
                FlexTE.Columns(0).ItemStyle.Width = 300
                FlexTE.Columns(1).ItemStyle.Width = 300 : FlexTE.Columns(2).ItemStyle.Width = 0
                FlexTE.Columns(3).ItemStyle.Width = 0
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
                FlexTE.Columns(0).HeaderText = "Nivel 1" : FlexTE.Columns(1).HeaderText = "Nivel 2" : FlexTE.Columns(2).HeaderText = "Nivel 3"
                FlexTE.Columns(2).ItemStyle.Width = 150
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
                        dRow(3) = Nu(Rs!NIVEL1_CODIGO)
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
            Else
                dt = Nothing
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
        Return dt
    End Function

    Private Sub btnTENuevo_Click(sender As Object, e As EventArgs) Handles btnTENuevo.Click
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            lblIngresoTE.Visible = True
            lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial"
            txtTECodigo.Text = ""
            txtTEDescripcion.Text = ""
            If Right(cboTabla.SelectedItem.Text.Trim, 1) = "1" Then
                cboNivel1.Enabled = False : cboNivel2.Enabled = False
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL1_CODIGO) FROM " & cboTabla.SelectedItem.Text
            ElseIf Right(cboTabla.SelectedItem.Text.Trim, 1) = "2" Then
                cboNivel1.Enabled = True
                cboNivel2.Enabled = False
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL2_CODIGO) FROM " & cboTabla.SelectedItem.Text
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            ElseIf Right(cboTabla.SelectedItem.Text.Trim, 1) = "3" Then
                cboNivel1.Enabled = True
                cboNivel2.Enabled = True
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL3_CODIGO) FROM " & cboTabla.SelectedItem.Text
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            End If
            If CmdGlobal.CommandText = "" Then Exit Sub
            cboNivel2.Items.Add("< Seleccionar >") : cboNivel2.SelectedValue = "< Seleccionar >"
            btnTENuevo.Enabled = False
            btnTEGuardar.Visible = True
            btnTECancelar.Visible = True
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtTECodigo.Text = Nz(Rs(0)) + 1
                End While
            Else
                txtTECodigo.Text = "1"
            End If
            Rs.Close()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboNivel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNivel1.SelectedIndexChanged
        Try
            cboNivel2.Items.Clear()
            If cboNivel1.SelectedValue = "< Seleccionar >" Then cboNivel2.Items.Add("< Seleccionar >") : cboNivel2.SelectedValue = "< Seleccionar >" : Exit Sub
            Call LLenaComboItemTabEsp(cboNivel2, cboNivel1.SelectedValue.Trim, "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 2, Session("CodEmpresa"), Session("Ruta_Emp"))

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub btnTECancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTECancelar.Click
        btnTENuevo.Enabled = True
        btnTEGuardar.Visible = False
        btnTECancelar.Visible = False
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
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Ntb = Right(cboTabla.SelectedItem.Text, 1)
            dCodigo = txtTECodigo.Text.Trim
            If Ntb = "2" And cboNivel1.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe escoger el Primer Nivel para poder guardar.')", True)
            ElseIf Ntb = "3" And cboNivel1.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe escoger el Primer Nivel para poder guardar.')", True)
            ElseIf Ntb = "3" And cboNivel2.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe escoger el Segundo Nivel para poder guardar.')", True)
            ElseIf txtTEDescripcion.Text.Trim = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No podrá guardar hasta que no le haya ingresado una descripción.')", True)
            End If
            If Ntb = "2" Then dNivel1 = cboNivel1.SelectedValue.Trim
            If Ntb = "3" Then dNivel2 = cboNivel2.SelectedValue.Trim
            nTiempo = ""
            Dim psConsulta As String = ""
            If lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial" And UCase(txtTEDescripcion.Text.Trim) <> UCase(txtTEDescripcionE.Text.Trim) Then
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
                        psConsulta = "Error"
                    End While
                End If
                Rs.Close()
            End If
            If psConsulta = "Error" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se ha encontrado una descripcion igual, verificar o cambiar para poder guardar.')", True)
            Else
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
                End If
                'cboTabla_SelectedIndexChanged(sender, e)
                btnTECancelar_Click(sender, e)
                Actualizar_Tablas(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub BtnTECerrar_Click(sender As Object, e As EventArgs) Handles BtnTECerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTablaEsp').modal('hide');", True)
    End Sub
    Private Sub Actualizar_Tablas(sender As Object, e As EventArgs)
        cboAplicativo.Items.Clear()
        cboProducto.Items.Clear()
        cboSubProd.Items.Clear()
        Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
        cboAplicativo.SelectedValue = "< Seleccionar >"
        cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
        cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        Call cboAplicativo_SelectedIndexChanged(sender, e)
        cboProducto.Enabled = False
        cboSubProd.Enabled = False
    End Sub
End Class
