Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class AdminProblemas_Define_BaseDatos
    Inherits System.Web.UI.Page
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Call Limpiar()
        lblIngreso.Visible = True
        lblEtiqueta.Text = "Ingresar Base de Datos"
        Try
            lblError.Text = ""
            Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call cboAplicativo_SelectedIndexChanged(sender, e)
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
            cboCategoria.SelectedValue = "< Seleccionar >"
            cboProducto.Enabled = False
            cboSubProd.Enabled = False
        Catch Ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
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
        cboCategoria.SelectedValue = "< Seleccionar >"
        lblError.Text = ""
    End Sub
    Protected Sub cboAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAplicativo.SelectedIndexChanged
        lblError.Visible = False
        cboProducto.Items.Clear()
        cboSubProd.Items.Clear()
        cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
        cboProducto.Enabled = False
        cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        cboSubProd.Enabled = False
        Call LLenaComboItemTabEsp(cboProducto, cboAplicativo.SelectedValue.Trim, "", "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
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
        Call LLenaComboItemTabEsp(cboSubProd, cboAplicativo.SelectedValue.Trim, cboProducto.SelectedValue.Trim, "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
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
        Dim obj As New clsMesaAyuda
        lblError.Text = ""
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Integer : pCodSubProd = 0
        If (cboBusAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboBusAplicativo.SelectedValue.Trim
        If (cboBusProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboBusProducto.SelectedValue.Trim
        If (cboBusSubProd.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboBusSubProd.SelectedValue.Trim
        Try
            Flex.DataSource = obj.MALista_BaseDatos(Session("CodEmpresa"), pCodApli, pCodProducto, pCodSubProd, Session("Ruta_Emp"), Session("User"))
            Flex.DataBind()
            If Flex.Rows.Count > 0 Then
                lblCount.Text = "Total Registros : " & Flex.Rows.Count
            Else
                lblCount.Text = "No se encontraron registros"
            End If
        Catch Ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
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
            Page.Title = "Mesa de Ayuda - Registro Base de Datos"
            Try
                lblError.Text = ""
                cboCategoria.Items.Clear()
                Call LlenaComboItem3("TBOPC413", cboCategoria)
                Call LLenaComboItemTabEsp(cboBusAplicativo, "", "", "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
                cboBusAplicativo.SelectedValue = "< Seleccionar >"
                Call cboBusAplicativo_SelectedIndexChanged(sender, e)
                cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
                cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
                cboBusProducto.Enabled = False
                cboBusSubProd.Enabled = False
            Catch Ex As SqlException
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Call Llenar_Grilla()
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboBusAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboBusAplicativo.SelectedIndexChanged
        lblError.Visible = False
        cboBusProducto.Items.Clear()
        cboBusSubProd.Items.Clear()
        cboBusProducto.Items.Add("< Seleccionar >") : cboBusProducto.SelectedValue = "< Seleccionar >"
        cboBusProducto.Enabled = False
        cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        cboBusSubProd.Enabled = False
        Call LLenaComboItemTabEsp(cboBusProducto, cboBusAplicativo.SelectedValue.Trim, "", "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
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
        lblError.Visible = False
        cboBusSubProd.Items.Clear()
        cboBusSubProd.Items.Add("< Seleccionar >") : cboBusSubProd.SelectedValue = "< Seleccionar >"
        cboBusSubProd.Enabled = False
        If cboBusProducto.SelectedIndex = -1 Or cboBusProducto.Items.Count = 0 Then Exit Sub
        If cboBusProducto.Items(cboBusProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboBusSubProd, cboBusAplicativo.SelectedValue.Trim, cboBusProducto.SelectedValue.Trim, "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
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
        lblError.Text = ""
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Double : pCodSubProd = 0
        Dim pCodBaseDatos As Integer : pCodBaseDatos = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        lblError.Text = ""
        If cboAplicativo.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & " <br> - Seleccionar Aplicatico."
        If cboCategoria.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & " <br> - Seleccionar Categoria."
        If Trim(txtTransaccion.Text) = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Transacción."
        If Trim(txtConsulta.Text) = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Consulta."
        If Trim(txtSolucion.Text) = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Solución."
        If lblError.Text.Trim <> "" Then
            lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
            Exit Sub
        End If
        Dim obj As New clsMesaAyuda
        If (cboAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboAplicativo.SelectedValue.Trim
        If (cboProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboProducto.SelectedValue.Trim
        If (cboSubProd.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboSubProd.SelectedValue.Trim
        Try
            If lblEtiqueta.Text = "Ingresar Base de Datos" Then
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT MAX(ACARCON_CODIGO) FROM TBADMIN_CARTERA_CONSULTA "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pCodBaseDatos = Nz(Rs(0)) + 1
                    End While
                Else
                    pCodBaseDatos = 1
                End If
                Rs.Close()
                obj.MAInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, 0, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "1", Session("Ruta_Emp"), cboCategoria.SelectedValue.Trim)
                If pCodProducto <> 0 Then obj.MAInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"), cboCategoria.SelectedValue.Trim)
                If pCodSubProd <> 0 Then obj.MAInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, pCodSubProd, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"), cboCategoria.SelectedValue.Trim)
                Call btnCancelar_Click(sender, e)
                Call btnListar_Click(sender, e)
            Else
                pCodBaseDatos = txtCodConsulta.Text
                obj.MAInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, 0, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"), cboCategoria.SelectedValue.Trim)
                If pCodProducto <> 0 Then obj.MAInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"), cboCategoria.SelectedValue.Trim)
                If pCodSubProd <> 0 Then obj.MAInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, pCodSubProd, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtSolucion.Text), "2", Session("Ruta_Emp"), cboCategoria.SelectedValue.Trim)
                Call btnCancelar_Click(sender, e)
                Call btnListar_Click(sender, e)
            End If
        Catch Ex As SqlException
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
        'Response.Redirect("Cas_Definicion_CarteraConsulta.aspx")
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        'Dim Rs As SqlDataReader
        If e.CommandName = "Editar" Then
            lblIngreso.Visible = True
            Call Limpiar()
            lblEtiqueta.Text = "Edición de Base de Datos"
            txtCodConsulta.Text = Flex.Rows(Index).Cells(8).Text
            Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_PRO1", "TBESP_PRO2", "TBESP_PRO3", 1, Session("CodEmpresa"), Session("Ruta_Emp")) '"&#241;"
            If Flex.Rows(Index).Cells(9).Text <> "&nbsp;" Then cboAplicativo.SelectedValue = Flex.Rows(Index).Cells(9).Text : cboAplicativo_SelectedIndexChanged(sender, e)
            If Flex.Rows(Index).Cells(10).Text <> "&nbsp;" Then cboProducto.SelectedValue = Flex.Rows(Index).Cells(10).Text : cboProducto_SelectedIndexChanged(sender, e)
            If Flex.Rows(Index).Cells(11).Text <> "&nbsp;" Then cboSubProd.SelectedValue = Flex.Rows(Index).Cells(11).Text
            If Flex.Rows(Index).Cells(12).Text <> "&nbsp;" Then cboCategoria.SelectedValue = Flex.Rows(Index).Cells(12).Text Else cboCategoria.SelectedValue = "< Seleccionar >"
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
