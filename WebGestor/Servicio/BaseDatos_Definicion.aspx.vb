Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class BaseDatos_Definicion
    Inherits System.Web.UI.Page
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Call Limpiar()
        lblIngreso.visible = True
        lblEtiqueta.Text = "Ingresar Base de Datos"
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), psConexion)
            Call cboAplicativo_SelectedIndexChanged(sender, e)
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
            cboProducto.Enabled = False
            cboSubProd.Enabled = False
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
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
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Call LLenaComboItemTabEsp(cboProducto, cboAplicativo.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), psConexion)
        If cboAplicativo.SelectedValue = "< Seleccionar >" Then
            cboProducto.Enabled = False
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Enabled = False
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboProducto.Enabled = True
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Enabled = False
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
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
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Call LLenaComboItemTabEsp(cboSubProd, cboAplicativo.SelectedValue.Trim, cboProducto.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), psConexion)
        If cboProducto.SelectedValue = "< Seleccionar >" Then
            cboSubProd.Enabled = False
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboSubProd.Enabled = True
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New ModuloGeneral
        lblError.Text = ""
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Integer : pCodSubProd = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If (cboBusAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboBusAplicativo.SelectedValue.Trim
        If (cboBusProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboBusProducto.SelectedValue.Trim
        If (cboBusSubProd.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboBusSubProd.SelectedValue.Trim
        Try
            Flex.DataSource = obj.BDC_Lista_BaseDatos(Session("CodEmpresa"), pCodApli, pCodProducto, pCodSubProd, psConexion)
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
        Call Llenar_Grilla()
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
                Call LLenaComboItemTabEsp(cboBusAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), psConexion)
                cboBusAplicativo.SelectedValue = "< Seleccionar >"
                Call cboAplicativo_SelectedIndexChanged(sender, e)
                cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
                cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
                cboBusProducto.Enabled = False
                cboBusSubProd.Enabled = False
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboBusAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBusAplicativo.SelectedIndexChanged
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Visible = False
        cboBusProducto.Items.Clear()
        cboBusSubProd.Items.Clear()
        cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
        cboBusProducto.Enabled = False
        cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        cboBusSubProd.Enabled = False
        Call LLenaComboItemTabEsp(cboBusProducto, cboBusAplicativo.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), psConexion)
        If cboBusAplicativo.SelectedValue = "< Seleccionar >" Then
            cboBusProducto.Enabled = False
            cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
            cboBusSubProd.Enabled = False
            cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboBusProducto.Enabled = True
            cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
            cboBusSubProd.Enabled = False
            cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboBusProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBusProducto.SelectedIndexChanged
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Visible = False
        cboBusSubProd.Items.Clear()
        cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        cboBusSubProd.Enabled = False
        If cboBusProducto.SelectedIndex = -1 Or cboBusProducto.Items.Count = 0 Then Exit Sub
        If cboBusProducto.Items(cboBusProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboBusSubProd, cboBusAplicativo.SelectedValue.Trim, cboBusProducto.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), psConexion)
        If cboBusProducto.SelectedValue = "< Seleccionar >" Then
            cboBusSubProd.Enabled = False
            cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboBusSubProd.Enabled = True
            cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        End If
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
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
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
        Dim obj As New ModuloGeneral
        If (cboAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboAplicativo.SelectedValue.Trim
        If (cboProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboProducto.SelectedValue.Trim
        If (cboSubProd.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboSubProd.SelectedValue.Trim
        Try
            If lblEtiqueta.Text = "Ingresar Base de Datos" Then
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT MAX(CARCON_CODIGO) FROM TBCAS_CARTERA_CONSULTA "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pCodBaseDatos = Nz(Rs(0)) + 1
                    End While
                Else
                    pCodBaseDatos = 1
                End If
                Rs.Close()
                obj.BDC_InsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, 0, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "1", Session("Ruta_Emp"))
                If pCodProducto <> 0 Then obj.BDC_InsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                If pCodSubProd <> 0 Then obj.BDC_InsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, pCodSubProd, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                Call btnCancelar_Click(sender, e)
                Call btnListar_Click(sender, e)
            Else
                pCodBaseDatos = txtCodConsulta.Text
                obj.BDC_InsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, 0, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                If pCodProducto <> 0 Then obj.BDC_InsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                If pCodSubProd <> 0 Then obj.BDC_InsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, pCodSubProd, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"))
                Call btnCancelar_Click(sender, e)
                Call btnListar_Click(sender, e)
            End If
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            lblIngreso.Visible = True
            Call Limpiar()
            lblEtiqueta.Text = "Edición de Base de Datos"
            txtCodConsulta.Text = Flex.Rows(Index).Cells(7).Text
            Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), psConexion) '"&#241;"
            If Flex.Rows(Index).Cells(8).Text <> "&nbsp;" Then cboAplicativo.SelectedValue = Flex.Rows(Index).Cells(8).Text : cboAplicativo_SelectedIndexChanged(sender, e)
            If Flex.Rows(Index).Cells(9).Text <> "&nbsp;" Then cboProducto.SelectedValue = Flex.Rows(Index).Cells(9).Text : cboProducto_SelectedIndexChanged(sender, e)
            If Flex.Rows(Index).Cells(10).Text <> "&nbsp;" Then cboSubProd.SelectedValue = Flex.Rows(Index).Cells(10).Text
            txtTransaccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtConsulta.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtSolucion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
        End If
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
End Class
