Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Contabilidad_TablasContables2
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        Dim obj As New clsCont_Listados
        If Ficha.ActiveTabIndex = 0 Then
            cboAAño.Items.Clear()
            Call LlenaAno(cboAAño)
            cboAAño.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Call Lista_Aduana()
        End If
        If Ficha.ActiveTabIndex = 1 Then
            cboAñoP.Items.Clear()
            Call LlenaAno(cboAñoP)
            cboAñoP.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            cboAñoP.SelectedValue = CInt(Left(FechaActual, 4))
            cboAñoP.Focus()
            Call LlenaPartidaPresupuestaria()
        End If
        If Ficha.ActiveTabIndex = 2 Then
            cboMAño.Items.Clear()
            Call LlenaAno(cboMAño)
            cboMAño.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Call Lista_MedioPago()
        End If
        If Ficha.ActiveTabIndex = 3 Then
            Call Lista_CtaBancos()
            cboBMoneda.Items.Clear()
            cboBTipo.Items.Clear()
            Call LlenaComboItem("TBOPC017", cboBMoneda)
            Call LlenaComboItem("TBOPC016", cboBTipo)
            cboBMoneda.Items.Add("< Seleccionar >")
            cboBTipo.Items.Add("< Seleccionar >")
        End If
        'Implementado = 03/11/09
        If Ficha.ActiveTabIndex = 4 Then
            Me.cboAñoCB.Items.Clear()
            Call LlenaAno(Me.cboAñoCB)
            Me.cboAñoCB.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))

            cboCBPeriodo.DataSource = obj.Cont_ListaPeriodos(Session("CodEmpresa"), cboAñoCB.Text, "No", 0, Session("Ruta_Emp"))
            cboCBPeriodo.DataTextField = "PERIODO_NOMBRE"
            cboCBPeriodo.DataValueField = "PER_PERIODO"
            cboCBPeriodo.DataBind()
            'cboCBPeriodo.Items.Add("< Seleccionar >") : cboCBPeriodo.SelectedValue = "< Seleccionar >"
            cboCBCtaBnc.DataSource = obj.Cont_ListaCtaBancaria(Session("Ruta_Emp"))
            cboCBCtaBnc.DataTextField = "CUENTA"
            cboCBCtaBnc.DataValueField = "CBAN_CODIGO"
            cboCBCtaBnc.DataBind()
            'cboCBCtaBnc.Items.Add("< Seleccionar >") : cboCBCtaBnc.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Private Sub LlenaPartidaPresupuestaria()
        'Dim obj As New Listados
        'lblError.Text = ""
        'Try
        '    FlexP.DataSource = CargaPartidaPresupuestaria()
        '    FlexP.DataBind()
        'Catch Ex As SqlException
        '    lblError.Visible = True
        '    lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        'Catch Ex As Exception
        '    lblError.Visible = True
        '    lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        'Finally
        'End Try
    End Sub
    Private Function CargaPartidaPresupuestaria() As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Sql = " SELECT * FROM TBPRESUPUESTO_" & Session("CodEmpresa") & " " _
            & " WHERE PRES_SYS_EST='0' AND PRES_AÑO='" & cboAñoP.Text & "' ORDER BY PRES_CUENTA"
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Return Dt
    End Function
    Protected Sub cboAñoP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAñoP.SelectedIndexChanged
        Call LlenaPartidaPresupuestaria()
    End Sub
    Protected Sub btnANuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnANuevo.Click
        lblAIngreso.Visible = True
        lblAError.Text = ""
        lblAEtiqueta.Text = "Ingresar Aduana"
        txtCodAduana.ReadOnly = False
        txtCodAduana.Text = ""
        txtADescripcion.Text = ""
    End Sub
    Private Sub Lista_Aduana()
        Dim obj As New clsCont_Listados
        lblAError.Text = ""
        Try
            FlexA.DataSource = obj.Cont_ListaAduana(Session("CodEmpresa"), cboAAño.Text, Session("Ruta_Emp"))
            FlexA.DataBind()
        Catch Ex As SqlException
            lblAError.Visible = True
            lblAError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblAError.Visible = True
            lblAError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboAAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Lista_Aduana()
    End Sub
    Protected Sub btnPNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FraPIngreso.Visible = True
    End Sub
    Protected Sub btnAGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsCont_Listados
        Dim obj2 As New clsCont_InsUpdDel
        Dim dt As DataTable
        If txtCodAduana.Text.Trim = "" Then lblAError.Text = "ingrese Código Ciudad de 3 Dígitos." : Exit Sub
        If txtADescripcion.Text.Trim = "" Then lblAError.Text = "Falta ingresar Nombre de Aduana." : Exit Sub
        Try
            If lblAEtiqueta.Text = "Ingresar Aduana" Then
                dt = obj.Cont_ExisteAduana(Session("CodEmpresa"), txtADescripcion.Text.Trim, cboAAño.Text, "1", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblAError.Text = "La descripción ya existe." : Exit Sub
                dt = obj.Cont_ExisteAduana(Session("CodEmpresa"), txtCodAduana.Text.Trim, cboAAño.Text, "2", Session("Ruta_Emp"))
                If dt.Rows.Count = 0 Then
                    obj2.Cont_InsUpd_Aduana(Session("CodEmpresa"), cboAAño.Text, txtCodAduana.Text.Trim, txtADescripcion.Text.Trim, HttpContext.Current.User.Identity.Name, "1", Session("Ruta_Emp"))
                Else
                    lblAError.Text = "El codigo a Ingresar ya existe." : Exit Sub
                End If
                dt = Nothing
            ElseIf lblAEtiqueta.Text = "Editar Aduana" Then
                obj2.Cont_InsUpd_Aduana(Session("CodEmpresa"), cboAAño.Text, txtCodAduana.Text.Trim, txtADescripcion.Text.Trim, HttpContext.Current.User.Identity.Name, "2", Session("Ruta_Emp"))
            End If
            Call Lista_Aduana()
            btnACancelar_Click(sender, e)
        Catch Ex As SqlException
            lblAError.Visible = True
            lblAError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblAError.Visible = True
            lblAError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnACancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblAIngreso.Visible = False
        FlexA.Enabled = True
        btnANuevo.Enabled = True
        txtCodAduana.Text = ""
        txtADescripcion.Text = ""
    End Sub
    Protected Sub FlexA_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexA.PageIndexChanging
        lblAError.Text = ""
        FlexA.PageIndex = e.NewPageIndex
        Call Lista_Aduana()
    End Sub
    Protected Sub FlexA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexA.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblAError.Text = ""
        If e.CommandName = "Editar" Then
            lblAEtiqueta.Text = "Editar Aduana"
            lblAIngreso.Visible = True
            FlexA.Enabled = False
            txtCodAduana.ReadOnly = True
            txtCodAduana.Text = FlexA.Rows(Index).Cells(2).Text.Trim
            txtADescripcion.Text = FlexA.Rows(Index).Cells(3).Text.Trim
            btnANuevo.Enabled = True
        ElseIf e.CommandName = "Eliminar" Then
            Dim obj2 As New clsCont_InsUpdDel
            Try
                obj2.Cont_InsUpd_Aduana(Session("CodEmpresa"), cboAAño.Text, FlexA.Rows(Index).Cells(2).Text.Trim, "", HttpContext.Current.User.Identity.Name, "3", Session("Ruta_Emp"))
                Call Lista_Aduana()
            Catch Ex As SqlException
                lblAError.Visible = True
                lblAError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblAError.Visible = True
                lblAError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Protected Sub FlexP_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexP.PageIndexChanging
        'lblError.Text = ""
        FlexP.PageIndex = e.NewPageIndex
        Call LlenaPartidaPresupuestaria()
    End Sub
    Protected Sub cboMAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Lista_MedioPago()
    End Sub
    Private Sub Lista_MedioPago()
        Dim obj As New clsCont_Listados
        lblMError.Text = ""
        Try
            FlexM.DataSource = obj.Cont_ListaMedioPago(Session("CodEmpresa"), cboMAño.Text, Session("Ruta_Emp"))
            FlexM.DataBind()
        Catch Ex As SqlException
            lblMError.Visible = True
            lblMError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblMError.Visible = True
            lblMError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexM_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexM.PageIndexChanging
        lblMError.Text = ""
        FlexM.PageIndex = e.NewPageIndex
        Call Lista_MedioPago()
    End Sub
    Protected Sub btnMNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMIngreso.Visible = True
        lblMError.Text = ""
        lblMEtiqueta.Text = "Ingresar Medio de Pago"
        txtCodMedioPago.ReadOnly = False
        txtCodMedioPago.Text = ""
        txtMDescripcion.Text = ""
    End Sub
    Protected Sub FlexM_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexM.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblMError.Text = ""
        If e.CommandName = "Editar" Then
            lblMEtiqueta.Text = "Editar Medio de Pago"
            lblMIngreso.Visible = True
            FlexM.Enabled = False
            txtCodMedioPago.ReadOnly = True
            txtCodMedioPago.Text = FlexM.Rows(Index).Cells(2).Text.Trim
            txtMDescripcion.Text = FlexM.Rows(Index).Cells(3).Text.Trim
            btnMNuevo.Enabled = True
        ElseIf e.CommandName = "Eliminar" Then
            Dim obj2 As New clsCont_InsUpdDel
            Try
                obj2.Cont_InsUpd_MedioPago(Session("CodEmpresa"), cboMAño.Text, FlexM.Rows(Index).Cells(2).Text.Trim, "", HttpContext.Current.User.Identity.Name, "3", Session("Ruta_Emp"))
                Call Lista_MedioPago()
            Catch Ex As SqlException
                lblMError.Visible = True
                lblMError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblMError.Visible = True
                lblMError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Protected Sub btnMGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsCont_Listados
        Dim obj2 As New clsCont_InsUpdDel
        Dim dt As DataTable
        If txtCodMedioPago.Text.Trim = "" Then lblAError.Text = "Ingresar Código." : Exit Sub
        If txtMDescripcion.Text.Trim = "" Then lblAError.Text = "Ingresar Descripción." : Exit Sub
        Try
            If lblMEtiqueta.Text = "Ingresar Medio de Pago" Then
                dt = obj.Cont_ExisteMedioPago(Session("CodEmpresa"), txtMDescripcion.Text.Trim, cboMAño.Text, "1", Session("Ruta_Emp"))
                If dt.Rows.Count > 0 Then lblMError.Text = "La descripción ya existe." : Exit Sub
                dt = obj.Cont_ExisteMedioPago(Session("CodEmpresa"), txtCodMedioPago.Text.Trim, cboMAño.Text, "2", Session("Ruta_Emp"))
                If dt.Rows.Count = 0 Then
                    obj2.Cont_InsUpd_MedioPago(Session("CodEmpresa"), cboMAño.Text, txtCodMedioPago.Text.Trim, txtMDescripcion.Text.Trim, HttpContext.Current.User.Identity.Name, "1", Session("Ruta_Emp"))
                Else
                    lblMError.Text = "El codigo a Ingresar ya existe." : Exit Sub
                End If
                dt = Nothing
            ElseIf lblMEtiqueta.Text = "Editar Medio de Pago" Then
                obj2.Cont_InsUpd_MedioPago(Session("CodEmpresa"), cboMAño.Text, txtCodMedioPago.Text.Trim, txtMDescripcion.Text.Trim, HttpContext.Current.User.Identity.Name, "2", Session("Ruta_Emp"))
            End If
            Call Lista_MedioPago()
            btnMCancelar_Click(sender, e)
        Catch Ex As SqlException
            lblMError.Visible = True
            lblMError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblMError.Visible = True
            lblMError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnMCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMIngreso.Visible = False
        FlexM.Enabled = True
        btnMNuevo.Enabled = True
        txtCodMedioPago.Text = ""
        txtMDescripcion.Text = ""
    End Sub
    Protected Sub btnBNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblBIngreso.Visible = True
        lblBEtiqueta.Text = "Nueva Cuenta de Banco"
        btnBNuevo.Enabled = False
        FlexB.Enabled = False
        optBBanco.SelectedIndex = "0" : optBBanco_SelectedIndexChanged(sender, e)
        txtBCuenta.Text = ""
        txtBBancoNom.Text = ""
        cboBMoneda.SelectedValue = "< Seleccionar >"
        cboBTipo.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub Lista_CtaBancos()
        Dim obj As New clsCont_Listados
        lblMError.Text = ""
        Try
            FlexB.DataSource = obj.Cont_ListaCtaBancos(Session("CodEmpresa"), Session("Ruta_Emp"))
            FlexB.DataBind()
        Catch Ex As SqlException
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnBGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsCont_InsUpdDel
        Dim pCodBanco As Double = 0
        If optBBanco.SelectedIndex = "0" And cboBBancoNom.SelectedValue = "< Seleccionar >" Then lblBError.Text = "Falta selecciomar el banco." : Exit Sub
        If optBBanco.SelectedIndex = "1" And txtBBancoNom.Text.Trim = "" Then lblBError.Text = "Falta ingresar el banco." : Exit Sub
        If cboBMoneda.SelectedValue = "< Seleccionar >" Then lblBError.Text = "Falta seleccionat el tipo de moneda." : Exit Sub
        If cboBTipo.SelectedValue = "< Seleccionar >" Then lblBError.Text = "Falta seleccionat el tipo de cuenta." : Exit Sub
        If txtBCuenta.Text.Trim = "" Then lblBError.Text = "Falta ingresar el número de la cuenta." : Exit Sub
        Try
            If lblBEtiqueta.Text = "Nueva Cuenta de Banco" Then
                If optBBanco.SelectedIndex = "0" Then pCodBanco = cboBBancoNom.SelectedValue.Trim
                If optBBanco.SelectedIndex = "1" Then
                    obj.Cont_InsUpd_CuentaBanco(Session("CodEmpresa"), pCodBanco, txtBBancoNom.Text.Trim, cboBMoneda.SelectedValue.Trim, cboBTipo.SelectedValue.Trim, txtBCuenta.Text.Trim, HttpContext.Current.User.Identity.Name, "1", 0, Session("Ruta_Emp"))
                ElseIf optBBanco.SelectedIndex = "0" Then
                    obj.Cont_InsUpd_CuentaBanco(Session("CodEmpresa"), pCodBanco, "", cboBMoneda.SelectedValue.Trim, cboBTipo.SelectedValue.Trim, txtBCuenta.Text.Trim, HttpContext.Current.User.Identity.Name, "2", 0, Session("Ruta_Emp"))
                End If
            End If
            Call Lista_CtaBancos()
            btnBCancelar_Click(sender, e)
        Catch Ex As SqlException
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnBCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblBIngreso.Visible = False
        FlexB.Enabled = True
        btnBNuevo.Enabled = True
        txtBCuenta.Text = ""
        txtBBancoNom.Text = ""
        lblBError.Text = ""
    End Sub
    Protected Sub optBBanco_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If optBBanco.SelectedIndex = "0" Then
            cboBBancoNom.Enabled = True
            btnBBorrar.Enabled = True
            txtBBancoNom.Visible = False
            txtBBancoNom.Enabled = False
            Call Carga_Bancos()
        ElseIf optBBanco.SelectedIndex = "1" Then
            cboBBancoNom.Enabled = False
            btnBBorrar.Enabled = False
            txtBBancoNom.Visible = True
            txtBBancoNom.Enabled = True
            Call Carga_Bancos()
        End If
    End Sub
    Private Sub Carga_Bancos()
        Dim obj As New clsCont_Listados
        lblBError.Text = ""
        Try
            cboBBancoNom.DataSource = obj.Cont_ListaBancos(Session("CodEmpresa"), Session("Ruta_Emp"))
            cboBBancoNom.DataTextField = "BANCO_NOMBRE"
            cboBBancoNom.DataValueField = "BANCO_CODIGO"
            cboBBancoNom.DataBind()
        Catch Ex As SqlException
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnBBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsCont_Listados
        Dim obj2 As New clsCont_InsUpdDel
        Dim pCodBanco As Double
        Dim dt As DataTable
        Try
            dt = obj.Cont_ExisteCtaBanco(Session("CodEmpresa"), cboBBancoNom.SelectedValue.Trim, Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                lblBError.Text = "No puede borrar el Banco, se encuentra en uso."
            Else
                pCodBanco = cboBBancoNom.SelectedValue.Trim
                obj2.Cont_InsUpd_CuentaBanco(Session("CodEmpresa"), pCodBanco, "", "", "", "", HttpContext.Current.User.Identity.Name, "4", 0, Session("Ruta_Emp"))
            End If
        Catch Ex As SqlException
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        optBBanco_SelectedIndexChanged(sender, e)
    End Sub
    Protected Sub FlexB_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexB.PageIndexChanging
        lblBError.Text = ""
        FlexB.PageIndex = e.NewPageIndex
        Call Lista_CtaBancos()
    End Sub
    Protected Sub FlexB_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexB.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblMError.Text = ""
        If e.CommandName = "Eliminar" Then
            Dim obj As New clsCont_InsUpdDel
            Try
                obj.Cont_InsUpd_CuentaBanco(Session("CodEmpresa"), 0, "", "", "", "", HttpContext.Current.User.Identity.Name, "3", FlexB.Rows(Index).Cells(5).Text.Trim, Session("Ruta_Emp"))
                Call Lista_CtaBancos()
            Catch Ex As SqlException
                lblBError.Visible = True
                lblBError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblBError.Visible = True
                lblBError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Protected Sub cboCBPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Extraer_saldo()
    End Sub
    Protected Sub cboCBCtaBnc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Extraer_saldo()
    End Sub
    Private Sub Extraer_saldo()
        Dim obj As New clsCont_Listados
        Dim dt2 As New DataTable
        Dim codCuenta As Double = 0
        Dim codPer As Double = 0
        codCuenta = cboCBCtaBnc.SelectedValue.Trim
        codPer = cboCBPeriodo.SelectedValue.Trim
        Try
            dt2 = obj.Cont_ListaSaldo(codCuenta, cboAñoCB.Text.Trim, codPer, Session("Ruta_Emp"))
            If dt2.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt2.Rows
                    TxtCBSaldoBnc.Text = Nz(dr("CONB_SALDO"))
                Next
            Else
                TxtCBSaldoBnc.Text = ""
            End If
            dt2 = Nothing
        Catch Ex As SqlException
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblBError.Visible = True
            lblBError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboAñoCB_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class
