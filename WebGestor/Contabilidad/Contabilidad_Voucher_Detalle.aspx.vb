Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Contabilidad_Voucher_Detalle
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim Año As String = Request.QueryString("pAño")
            cboFlujoCaja.Items.Clear()
            cboAsientos.Items.Clear()
            cboPeriodos.Items.Clear()
            Call LlenaAsientos(Año)
            Call LlenaPeriodos(Año)
            Call LlenaDocumentos(Año)
            Call LlenaCentroCostos(Año)
            Call LlenaFlujoCaja(Año)
            txtImporte.Text = ""
            Call LlenaComboItem("TBOPC015", cboMoneda)
            cboMoneda.Items.Add("(Seleccionar)") : cboMoneda.SelectedValue = "(Seleccionar)"
            txtFechaReg.Value = FormatoFecha(FechaActual)
            txtFechaDoc.Value = FormatoFecha(FechaActual)
            txtFechaVcto.Value = FormatoFecha(FechaActual)
            txtImporte.Text = "0.00"
            lblDiferencia.Text = "0.00"
            lblTotDebe.Text = "0.00"
            lblTotHaber.Text = "0.00"
            cboPeriodos_SelectedIndexChanged(sender, e)
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
            Ficha.Enabled = False
            If Session("Nuevo_Reg") = "S" Then Call LlenarGrilla()
            If Session("Nuevo_Reg") = "SP" Then Call Nueva_Parte_Voucher(sender, e)
        End If
    End Sub
    Private Function Hallar_Valor_Venta(ByVal Fecha As String) As String
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim pFecha As String
        Dim Rs As SqlDataReader
        pFecha = Right(Fecha, 4) & Mid(Fecha, 4, 2) & Left(Fecha, 2)
        Hallar_Valor_Venta = "0.0000"
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT TIPCAM_VENTA FROM TBTIPCAMBIO WHERE (TIPCAM_FECHA = '" & pFecha & "')"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                Hallar_Valor_Venta = Format(Nz(Rs!TIPCAM_VENTA), "0.000#")
            End While
        End If
        Rs.Close() : Cn.Close()
    End Function
    Private Sub LlenaAsientos(ByVal CAño As String)
        Try
            Dim obj As New clsCont_Listados
            cboAsientos.DataSource = obj.Cont_ListaAsientos(Session("CodEmpresa"), CAño, "No", "0", Session("Ruta_Emp"))
            cboAsientos.DataTextField = "ASIENTO_DESCRIPCION"
            cboAsientos.DataValueField = "ASIENTO_CODIGO"
            cboAsientos.DataBind()
            cboAsientos.Items.Add("(Seleccionar)") : cboAsientos.SelectedValue = "(Seleccionar)"
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenaDocumentos(ByVal CAño As String)
        Try
            Dim obj As New clsCont_Listados
            cboTipoDoc.DataSource = obj.Cont_ListaDocumentos(Session("CodEmpresa"), CAño, Session("Ruta_Emp"))
            cboTipoDoc.DataTextField = "DOC_DOCUMENTO"
            cboTipoDoc.DataValueField = "DOC_CODIGO"
            cboTipoDoc.DataBind()
            cboTipoDoc.Items.Add("(Seleccionar)") : cboTipoDoc.SelectedValue = "(Seleccionar)"
            cboTipoDocRef.DataSource = obj.Cont_ListaDocumentos(Session("CodEmpresa"), CAño, Session("Ruta_Emp"))
            cboTipoDocRef.DataTextField = "DOC_DOCUMENTO"
            cboTipoDocRef.DataValueField = "DOC_CODIGO"
            cboTipoDocRef.DataBind()
            cboTipoDocRef.Items.Add("(Seleccionar)") : cboTipoDocRef.SelectedValue = "(Seleccionar)"
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenaCentroCostos(ByVal CAño As String)
        Try
            Dim obj As New clsCont_Listados
            cboCentroCosto.DataSource = obj.Cont_ListaCentroCostos(Session("CodEmpresa"), CAño, "R", Session("Ruta_Emp"))
            cboCentroCosto.DataTextField = "CCOSTO"
            cboCentroCosto.DataValueField = "CCOSTO_CODIGO"
            cboCentroCosto.DataBind()
            cboCentroCosto.Items.Add("(Seleccionar)") : cboCentroCosto.SelectedValue = "(Seleccionar)"
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenaFlujoCaja(ByVal CAño As String)
        Try
            Dim obj As New clsCont_Listados
            cboFlujoCaja.DataSource = obj.Cont_ListaFlujoCaja(Session("CodEmpresa"), CAño, Session("Ruta_Emp"))
            cboFlujoCaja.DataTextField = "FLUJOCAJA"
            cboFlujoCaja.DataValueField = "FLUCAJA_CODIGO"
            cboFlujoCaja.DataBind()
            cboFlujoCaja.Items.Add("(Seleccionar)") : cboFlujoCaja.SelectedValue = "(Seleccionar)"
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenaPeriodos(ByVal CAño As String)
        Try
            Dim PerActual As Integer
            Dim obj As New clsCont_Listados
            Dim dt As New DataTable
            cboPeriodos.DataSource = obj.Cont_ListaPeriodos(Session("CodEmpresa"), CAño, "No", "0", Session("Ruta_Emp"))
            cboPeriodos.DataTextField = "PERIODO_NOMBRE"
            cboPeriodos.DataValueField = "PER_PERIODO"
            cboPeriodos.DataBind()
            dt = obj.Cont_ListaPeriodos(Session("CodEmpresa"), CAño, "No", "0", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    If Nu(drMenuItem("PER_ACTUAL")) = "S" Then PerActual = Nu(drMenuItem("PER_PERIODO"))
                Next
            End If
            dt = Nothing
            cboPeriodos.SelectedValue = PerActual
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboAsientos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAsientos.SelectedIndexChanged
        Dim obj As New clsCont_Listados
        Dim dt As New DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Dim cAño As String = Request.QueryString("pAño")
        lblPrefVoucher.Text = ""
        txtNroVoucher.Text = ""
        If cboAsientos.SelectedValue = "(Seleccionar)" Then Exit Sub
        dt = obj.Cont_ListaAsientos(Session("CodEmpresa"), cAño, "Si", cboAsientos.SelectedValue.Trim, Session("Ruta_Emp"))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                lblPrefVoucher.Text = Nu(dr("ASIENTO_PREFIJO")) & Format(Month(txtFechaReg.Text), "00")
            Next
        End If
        dt = Nothing
        dt = CodVoucher(cAño)
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                txtNroVoucher.Text = Format(Nz(dr("Voucher")) + 1, "0000")
            Next
        End If
        dt = Nothing
    End Sub
    Function CodVoucher(ByVal vAño As String) As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Sql = "SELECT MAX(RIGHT(COMPROB_NRO_VOUCHER, 4)) as Voucher FROM TBCOMPROB_" & Session("CodEmpresa") & vAño & " WHERE " _
        & " (COMPROB_PERIODO = '" & cboPeriodos.SelectedValue.Trim & "') AND (COMPROB_ASIENTO_CODIGO = '" & cboAsientos.SelectedValue.Trim & "')"
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Return Dt
    End Function
    Protected Sub cboPeriodos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodos.SelectedIndexChanged
        Dim obj As New clsCont_Listados
        Dim dt As New DataTable
        Dim cAño As String = Request.QueryString("pAño")
        lblFIni.Text = ""
        lblFFin.Text = ""
        If cboPeriodos.SelectedValue = ("Seleccionar") Then Exit Sub
        dt = obj.Cont_ListaPeriodos(Session("CodEmpresa"), cAño, "Si", cboPeriodos.SelectedValue.Trim, Session("Ruta_Emp"))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                lblFIni.Text = FormatoFecha(Nu(dr("PER_FECHAINI")))
                lblFFin.Text = FormatoFecha(Nu(dr("PER_FECHAFIN")))
            Next
        End If
        dt = Nothing
        If Format(txtFechaReg.Value, "yyyymmdd") < Format(lblFIni.Text, "yyyymmdd") Then
            txtFechaReg.MaxDate = txtFechaReg.MinDate
            txtFechaReg.MaxDate = lblFFin.Text
            txtFechaReg.MinDate = lblFIni.Text
            'txtFechaDoc.MaxDate = txtFechaReg.MinDate
            'txtFechaDoc.MaxDate = lblFFin.Text
            'txtFechaDoc.MinDate = lblFIni.Text
            'txtFechaVcto.MaxDate = txtFechaReg.MinDate
            'txtFechaVcto.MaxDate = lblFFin.Text
            'txtFechaVcto.MinDate = lblFIni.Text
        Else
            txtFechaReg.MaxDate = lblFFin.Text
            txtFechaReg.MinDate = lblFIni.Text
            'txtFechaDoc.MaxDate = lblFFin.Text
            'txtFechaDoc.MinDate = lblFIni.Text
            'txtFechaVcto.MaxDate = lblFFin.Text
            'txtFechaVcto.MinDate = lblFIni.Text
        End If
        txtFechaReg.Value = lblFIni.Text
        txtFechaDoc.Value = txtFechaReg.Value
        txtFechaVcto.Value = txtFechaReg.Value
        txtValorVenta.Text = Hallar_Valor_Venta(txtFechaDoc.Text)
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New Listados
        lblError.Text = ""
        Try
            FlexCuenta.DataSource = Cargar_BD()
            FlexCuenta.DataBind()
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Function Cargar_BD() As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cAño As String = Request.QueryString("pAño")
        Dim Sql As String : Sql = ""
        Sql = " SELECT PLAN_CODIGO, PLAN_CUENTA, PLAN_COD_NIVEL,PLAN_NOMBRE_CUENTA,PLAN_NIVEL_CUENTA,PLAN_CUENTA_DEUDORA,PLAN_CUENTA_ACREEDORA,PLAN_ASIENTO_DESTINO,PLAN_CENTRO_COSTOS,PLAN_PRESUPUESTO,PLAN_FLUJOCAJA, " _
            & "(SELECT V.PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " V WHERE V.PLAN_AÑO = '" & cAño & "' AND V.PLAN_CODIGO=P.PLAN_CUENTA_DEUDORA) AS CUENTA_DEUDORA, " _
            & "(SELECT V.PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " V WHERE V.PLAN_AÑO = '" & cAño & "' AND V.PLAN_CODIGO=P.PLAN_CUENTA_ACREEDORA) AS CUENTA_ACREDORA " _
            & " FROM TBPCGR_" & Session("CodEmpresa") & " P WHERE (PLAN_AÑO = '" & cAño & "') AND (PLAN_SYS_EST = '0') "
        If txtBusCuenta.Text <> "" Then Sql = Sql & " AND PLAN_CUENTA LIKE '" & txtBusCuenta.Text & "%'"
        Sql = Sql & "ORDER BY PLAN_CUENTA"
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Return Dt
    End Function
    Protected Sub FlexCuenta_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexCuenta.PageIndexChanging
        lblError.Text = ""
        FlexCuenta.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub FlexCuenta_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexCuenta.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            lblErrorC.Text = ""
            If FlexCuenta.Rows(Index).Cells(3).Text <> "R" Then
                lblErrorC.Text = "Sólo pueden aceptarse cuentas con Nivel de Cuenta REGISTRO" & Chr(13) & "para que sea una cuenta aceptable en los ingresos de los Comprobantes."
            End If
            If lblErrorC.Text <> "" Then Exit Sub
            If FlexCuenta.Rows(Index).Cells(4).Text <> "&nbsp;" Then lblCodCuenta.Text = FlexCuenta.Rows(Index).Cells(4).Text
            If FlexCuenta.Rows(Index).Cells(5).Text <> "&nbsp;" Then lblCtaDeudora.Text = FlexCuenta.Rows(Index).Cells(5).Text
            If FlexCuenta.Rows(Index).Cells(6).Text <> "&nbsp;" Then lblCtaAcreedora.Text = FlexCuenta.Rows(Index).Cells(6).Text
            If FlexCuenta.Rows(Index).Cells(7).Text <> "&nbsp;" Then lblCentroCosto.Text = FlexCuenta.Rows(Index).Cells(7).Text
            If FlexCuenta.Rows(Index).Cells(8).Text <> "&nbsp;" Then lblPresupuesto.Text = FlexCuenta.Rows(Index).Cells(8).Text
            If FlexCuenta.Rows(Index).Cells(9).Text <> "&nbsp;" Then lblFlujoCaja.Text = FlexCuenta.Rows(Index).Cells(9).Text
            If FlexCuenta.Rows(Index).Cells(1).Text <> "&nbsp;" Then txtCuenta.Text = FlexCuenta.Rows(Index).Cells(1).Text
            Ficha.Enabled = False
            If lblCentroCosto.Text = "S" Then
                Ficha.Enabled = True
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            End If
            If lblFlujoCaja.Text = "S" Then
                Ficha.Enabled = True
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            End If
            If lblPresupuesto.Text = "S" Then
                Ficha.Enabled = True
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
            End If
            If lblPresupuesto.Text = "S" And lblCentroCosto.Text = "S" Then
                Ficha.Enabled = True
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
            End If
            ModalPopupExtender1.Hide()
        End If
    End Sub
    Protected Sub btnListaPer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListaPer.Click
        Dim obj As New Listados
        lblError.Text = ""
        Try
            FlexPersonas.DataSource = Cargar_Personas()
            FlexPersonas.DataBind()
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Function Cargar_Personas() As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Sql = " SELECT PERSONA_RUC, PERSONA_RAZON_SOCIAL,PERSONA_CODIGO, PERSONA_TIPO, PERSONA_PROVEE, " _
            & " (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA='TBOPC001' AND ELEMEN_CODIGO=PERSONA_TIPO) AS TIPOP " _
            & "  FROM TBDATA_PERSONAS WHERE (PERSONA_SYS_EST = '0') AND  (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' )"
        If txtBusRUC.Text <> "" Then Sql = Sql & " AND (PERSONA_RUC like '" & txtBusRUC.Text & "%')"
        Sql = Sql & " ORDER BY PERSONA_RAZON_SOCIAL"
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Return Dt
    End Function
    Protected Sub FlexPersonas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexPersonas.PageIndexChanging
        lblError.Text = ""
        FlexPersonas.PageIndex = e.NewPageIndex
        Call btnListaPer_Click(sender, e)
    End Sub
    Protected Sub FlexPersonas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPersonas.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "AceptarPer" Then
            If FlexPersonas.Rows(Index).Cells(2).Text <> "&nbsp;" Then txtRuc.Text = FlexPersonas.Rows(Index).Cells(2).Text
            If FlexPersonas.Rows(Index).Cells(4).Text <> "&nbsp;" Then lblCodPersona.Text = FlexPersonas.Rows(Index).Cells(4).Text
            ModalPopupExtender2.Hide()
        End If
    End Sub
    Protected Sub cboMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMoneda.SelectedIndexChanged
        If Session("Nuevo_Reg") = "S" Then Call LlenarGrilla()
        If Session("Nuevo_Reg") = "SP" Then Call Inserta_Grilla_Nueva_Parte()
    End Sub
    Private Sub Inserta_Grilla_Nueva_Parte()
        Dim CuentaA As String, CuentaD As String
        Dim cAño As String = Request.QueryString("pAño")
        Dim dt As New DataTable
        Dim dRow As Data.DataRow
        Dim i As Integer : i = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Call Hallar_Detalle_Comprob(lblPrefVoucher.Text & txtNroVoucher.Text)
        'If lblCentroCosto.Text = "S" Then FraCCosto.Visible = True : FraCCosto.ZOrder(1)
        'If lblPresupuesto.Text = "S" Then FraPresupuesto.Visible = True : FraCCosto.ZOrder(1) : FraPresupuesto.ZOrder(1)
        'If lblFlujoCaja.Text = "S" Then FraFlujoCaja.Visible = True : FraFlujoCaja.ZOrder(1)
        CuentaA = "" : CuentaD = ""
        If lblCtaAcreedora.Text <> "" And lblCtaDeudora.Text <> "" Then
            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " WHERE PLAN_AÑO='" & cAño & "' AND  PLAN_CODIGO='" & lblCtaAcreedora.Text & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CuentaA = Nu(Rs!PLAN_CUENTA)
                End While
            End If
            Rs.Close() : Cn.Close()
            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " WHERE PLAN_AÑO='" & cAño & "' AND  PLAN_CODIGO='" & lblCtaDeudora.Text & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CuentaA = Nu(Rs!PLAN_CUENTA)
                End While
            End If
            Rs.Close() : Cn.Close()
        End If
        dt.Columns.Add("C1")
        dt.Columns.Add("C2")
        dt.Columns.Add("C3")
        dt.Columns.Add("C4")
        dt.Columns.Add("C5")
        dt.Columns.Add("C6")
        dt.Columns.Add("C7")
        dt.Columns.Add("C8")
        If Flex.Rows.Count > 0 Then
            For i = 0 To Flex.Rows.Count - 1
                dRow = dt.NewRow
                dRow("C1") = Flex.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
                dRow("C2") = Flex.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
                dRow("C3") = Flex.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
                dRow("C4") = Flex.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
                dRow("C5") = Flex.Rows(i).Cells(4).Text.Replace("&nbsp;", "")
                dRow("C6") = Flex.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
                dRow("C7") = Flex.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
                dRow("C8") = Flex.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
                dt.Rows.Add(dRow)
            Next
        End If
        If cboMoneda.SelectedValue = "1" Then
            lblSigno.Text = UCase("$.")
            dRow = dt.NewRow
            dRow("C2") = IIf(txtCuenta.Text = "", "", txtCuenta.Text)
            dRow("C5") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
            dRow("C6") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
            dRow("C7") = "x"
            dt.Rows.Add(dRow)
            If CuentaD <> "" And CuentaA <> "" Then
                dRow = dt.NewRow
                dRow("C2") = CuentaD
                dRow("C5") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C6") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C7") = lblCtaDeudora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "D", "H")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
                dRow("C2") = CuentaA
                dRow("C5") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C6") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C7") = lblCtaAcreedora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "H", "D")
                dt.Rows.Add(dRow)
            End If
        ElseIf cboMoneda.SelectedValue = 2 Then
            lblSigno.Text = UCase("s/.")
            dRow = dt.NewRow
            dRow("C2") = IIf(txtCuenta.Text = "", "", txtCuenta.Text)
            dRow("C3") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
            dRow("C4") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
            dRow("C7") = "x"
            dt.Rows.Add(dRow)
            If CuentaD <> "" And CuentaA <> "" Then
                dRow = dt.NewRow
                dRow("C2") = CuentaD
                dRow("C3") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C4") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C7") = lblCtaDeudora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "D", "H")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
                dRow("C2") = CuentaA
                dRow("C5") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C6") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C7") = lblCtaAcreedora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "H", "D")
                dt.Rows.Add(dRow)
            End If
        End If
        Flex.DataSource = dt
        Flex.DataBind()
        Call Totales_Comprobante()
    End Sub
    Private Sub LlenarGrilla()
        Dim CuentaA As String, CuentaD As String
        Dim cAño As String = Request.QueryString("pAño")
        Dim dt As New DataTable
        Dim dRow As Data.DataRow
        Dim i As Integer : i = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        If cboMoneda.SelectedValue = "(Seleccionar)" Then Exit Sub
        CuentaA = "" : CuentaD = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        If lblCtaAcreedora.Text <> "" And lblCtaDeudora.Text <> "" Then
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " WHERE PLAN_AÑO='" & cAño & "' AND  PLAN_CODIGO='" & lblCtaAcreedora.Text & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CuentaA = Nu(Rs!PLAN_CUENTA)
                End While
            End If
            Rs.Close()
            Cn.Close()
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " WHERE PLAN_AÑO='" & cAño & "' AND  PLAN_CODIGO='" & lblCtaDeudora.Text & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CuentaA = Nu(Rs!PLAN_CUENTA)
                End While
            End If
            Rs.Close() : Cn.Close()
        End If
        dt.Columns.Add("C1")
        dt.Columns.Add("C2")
        dt.Columns.Add("C3")
        dt.Columns.Add("C4")
        dt.Columns.Add("C5")
        dt.Columns.Add("C6")
        dt.Columns.Add("C7")
        dt.Columns.Add("C8")
        If Flex.Rows.Count > 0 Then
            For i = 0 To Flex.Rows.Count - 1
                dRow = dt.NewRow
                dRow("C1") = Flex.Rows(i).Cells(0).Text.Replace("&nbsp;", "")
                dRow("C2") = Flex.Rows(i).Cells(1).Text.Replace("&nbsp;", "")
                dRow("C3") = Flex.Rows(i).Cells(2).Text.Replace("&nbsp;", "")
                dRow("C4") = Flex.Rows(i).Cells(3).Text.Replace("&nbsp;", "")
                dRow("C5") = Flex.Rows(i).Cells(4).Text.Replace("&nbsp;", "")
                dRow("C6") = Flex.Rows(i).Cells(5).Text.Replace("&nbsp;", "")
                dRow("C7") = Flex.Rows(i).Cells(6).Text.Replace("&nbsp;", "")
                dRow("C8") = Flex.Rows(i).Cells(7).Text.Replace("&nbsp;", "")
                dt.Rows.Add(dRow)
            Next
        End If
        If cboMoneda.SelectedValue = "1" Then
            lblSigno.Text = UCase("$.")
            dRow = dt.NewRow
            dRow("C2") = IIf(txtCuenta.Text = "", "", txtCuenta.Text)
            dRow("C5") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
            dRow("C6") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
            dRow("C7") = "x"
            dt.Rows.Add(dRow)
            If CuentaD <> "" And CuentaA <> "" Then
                dRow = dt.NewRow
                dRow("C2") = CuentaD
                dRow("C5") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C6") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C7") = lblCtaDeudora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "D", "H")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
                dRow("C2") = CuentaA
                dRow("C5") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C6") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C7") = lblCtaAcreedora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "H", "D")
                dt.Rows.Add(dRow)
            End If
        ElseIf cboMoneda.SelectedValue = 2 Then
            lblSigno.Text = UCase("s/.")
            dRow = dt.NewRow
            dRow("C2") = IIf(txtCuenta.Text = "", "", txtCuenta.Text)
            dRow("C3") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
            dRow("C4") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
            dRow("C7") = "x"
            dt.Rows.Add(dRow)
            If CuentaD <> "" And CuentaA <> "" Then
                dRow = dt.NewRow
                dRow("C2") = CuentaD
                dRow("C3") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C4") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C7") = lblCtaDeudora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "D", "H")
                dt.Rows.Add(dRow)
                dRow = dt.NewRow
                dRow("C2") = CuentaA
                dRow("C5") = IIf(opt.SelectedIndex = "1", txtImporte.Text, "")
                dRow("C6") = IIf(opt.SelectedIndex = "0", txtImporte.Text, "")
                dRow("C7") = lblCtaAcreedora.Text
                dRow("C8") = IIf(opt.SelectedIndex = "0", "H", "D")
                dt.Rows.Add(dRow)
            End If
        End If
        Flex.DataSource = dt
        Flex.DataBind()
        Call Totales_Comprobante()
    End Sub
    Private Sub Nueva_Parte_Voucher(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cAño As String = Request.QueryString("pAño")
        Dim Codigo As String = Request.QueryString("CodComprob")
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT *,(SELECT ASIENTO_DESCRIPCION FROM TBASIENTOS WHERE (ASIENTO_CODIGO = COMPROB_ASIENTO_CODIGO) AND (ASIENTO_AÑO = '" & cAño & "') AND (ASIENTO_EMPRESA = '" & Session("CodEmpresa") & "')) AS ASIENTOC, " _
        & " (SELECT DOC_DOCUMENTO FROM TBDOCUMENTOS WHERE DOC_EMPRESA='" & Session("CodEmpresa") & "' AND DOC_AÑO='" & cAño & "' AND DOC_CODIGO=COMPROB_DOC_CODIGO) AS DOCC, " _
        & " (SELECT DOC_DOCUMENTO FROM TBDOCUMENTOS WHERE DOC_EMPRESA='" & Session("CodEmpresa") & "' AND DOC_AÑO='" & cAño & "' AND DOC_CODIGO=COMPROB_DOC_REF) AS DOCC2, " _
        & " (SELECT PERSONA_RUC FROM TBDATA_PERSONAS WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND PERSONA_CODIGO=COMPROB_RUC_PERSONA) AS RUC_PERSONA " _
        & " FROM TBCOMPROB_" & Session("CodEmpresa") & cAño & " WHERE COMPROB_NUMERAR='" & Codigo & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                cboPeriodos.SelectedValue = Nz(Rs!COMPROB_PERIODO)
                cboPeriodos_SelectedIndexChanged(sender, e)
                txtFechaReg.Value = FormatoFecha(Nu(Rs!COMPROB_FEC_REGISTRO))
                txtValorVenta.Text = Format(Nz(Rs!COMPROB_TIPOCAM), "0.000#")
                cboAsientos.SelectedValue = Nu(Rs!COMPROB_ASIENTO_CODIGO)
                lblPrefVoucher.Text = Mid(Nu(Rs!COMPROB_NRO_VOUCHER), 1, Len(Nu(Rs!COMPROB_NRO_VOUCHER)) - 4)
                txtNroVoucher.Text = Right(Nu(Rs!COMPROB_NRO_VOUCHER), 4)
                cboMoneda.SelectedValue = Nz(Rs!COMPROB_MONEDA)
                opt.SelectedValue = 0
                txtFechaDoc.Value = FormatoFecha(Nu(Rs!COMPROB_FEC_DOC))
                txtFechaVcto.Value = FormatoFecha(Nu(Rs!COMPROB_FEC_VCTO))
                cboTipoDoc.SelectedValue = Nu(Rs!COMPROB_DOC_CODIGO)
                txtNroDoc.Text = Nu(Rs!COMPROB_NRO_DOC)
                cboTipoDocRef.SelectedValue = Nu(Rs!COMPROB_DOC_REF)
                txtNroDocRef.Text = Nu(Rs!COMPROB_NRO_DOC_REF)
                txtRuc.Text = Nu(Rs!RUC_PERSONA)
                lblCodPersona.Text = Nu(Rs!COMPROB_RUC_PERSONA)
                txtGlosa.Text = Nu(Rs!COMPROB_GLOSA)
                lblCodCuenta.Text = ""
                lblCentroCosto.Text = ""
                lblPresupuesto.Text = ""
                lblFlujoCaja.Text = ""
                lblCtaAcreedora.Text = ""
                lblCtaDeudora.Text = ""
                lblSigno.Text = ""
                Flex.DataSource = Nothing
                Flex.DataBind()
                If cboMoneda.SelectedValue = 1 Then lblSigno.Text = UCase("$.")
                If cboMoneda.SelectedValue = 2 Then lblSigno.Text = UCase("s.")
                cboPeriodos.Enabled = False
                txtFechaReg.Enabled = False
                txtValorVenta.ReadOnly = True
                txtNroVoucher.ReadOnly = True
                cboMoneda.Enabled = False
                cboAsientos.Enabled = False
                txtFechaDoc.Enabled = False
                txtFechaVcto.Enabled = False
                txtRuc.ReadOnly = True
            End While
        Else
            lblError.Text = "Error, no se enconstró el registro."
        End If
        Rs.Close() : Cn.Close()
        opt_SelectedIndexChanged(sender, e)
    End Sub
    Private Sub Totales_Comprobante()
        Dim m1 As Double, m2 As Double
        Dim E As Integer, e1, e2 As Integer
        lblTotDebe.Text = ""
        lblTotHaber.Text = ""
        lblDiferencia.Text = ""
        If cboMoneda.SelectedValue = "(Seleccionar)" Then Exit Sub
        With Flex
            m1 = 0 : m2 = 0 : e1 = 0 : e2 = 0
            If cboMoneda.SelectedValue = 1 Then
                If Flex.Rows.Count > 0 Then
                    For E = 0 To .Rows.Count - 1
                        If .Rows(E).Cells(4).Text.Replace("&nbsp;", "") <> "" Then
                            If IsNumeric(.Rows(E).Cells(4).Text) = True Then m1 = m1 + CDbl(.Rows(E).Cells(4).Text) : e1 = 1
                        End If
                        If .Rows(E).Cells(5).Text.Replace("&nbsp;", "") <> "" Then
                            If IsNumeric(.Rows(E).Cells(5).Text) = True Then m2 = m2 + CDbl(.Rows(E).Cells(5).Text) : e2 = 1
                        End If
                    Next
                    If e1 = 1 Then lblTotDebe.Text = Format(m1, "0.00#")
                    If e2 = 1 Then lblTotHaber.Text = Format(m2, "0.00#")
                    If (e1 = 0 And e2 = 1) Or (e1 = 1 And e2 = 0) Then
                        lblTotDebe.Text = Format(m1, "0.00#")
                        lblTotHaber.Text = Format(m2, "0.00#")
                    End If
                End If
            ElseIf cboMoneda.SelectedValue = 2 Then
                If Flex.Rows.Count > 0 Then
                    For E = 0 To .Rows.Count - 1
                        If Flex.Rows(E).Cells(2).Text.Replace("&nbsp;", "") <> "" Then m1 = m1 + CDbl(.Rows(E).Cells(2).Text) : e1 = 1
                        If Flex.Rows(E).Cells(3).Text.Replace("&nbsp;", "") <> "" Then m2 = m2 + CDbl(.Rows(E).Cells(3).Text) : e2 = 1
                    Next
                    If e1 = 1 Then lblTotDebe.Text = Format(m1, "0.00#")
                    If e2 = 1 Then lblTotHaber.Text = Format(m2, "0.00#")
                    If (e1 = 0 And e2 = 1) Or (e1 = 1 And e2 = 0) Then
                        lblTotDebe.Text = Format(m1, "0.00#")
                        lblTotHaber.Text = Format(m2, "0.00#")
                    End If
                End If
            End If
        End With
        Dim Dif As Double
        If (lblTotDebe.Text <> "" And lblTotHaber.Text <> "") And (lblTotDebe.Text <> "0.00#" And lblTotHaber.Text <> "0.00#") Then
            Dif = Nz(lblTotDebe.Text) - Nz(lblTotHaber.Text)
            lblDiferencia.Text = Format(Dif, "0.00#")
        End If
    End Sub
    Private Sub Hallar_Detalle_Comprob(ByVal NroVoucher As String)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cAño As String = Request.QueryString("pAño")
        Dim Codigo As String = Request.QueryString("CodComprob")
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim dt As New DataTable
        Dim dRow As Data.DataRow
        Dim DebeDol As Double
        Dim HaberDol As Double
        Dim DebeSol As Double
        Dim HaberSol As Double
        Cn.Open() : CmdGlobal.Connection = Cn
        dt.Columns.Add("C1")
        dt.Columns.Add("C2")
        dt.Columns.Add("C3")
        dt.Columns.Add("C4")
        dt.Columns.Add("C5")
        dt.Columns.Add("C6")
        dt.Columns.Add("C7")
        dt.Columns.Add("C8")
        CmdGlobal.CommandText = "SELECT C.COMPROB_PLAN_CODIGO, P.PLAN_CUENTA,C.COMPROB_IMPORTE_DEBE_S,C.COMPROB_IMPORTE_HABER_S,C.COMPROB_IMPORTE_DEBE_D, " _
            & " C.COMPROB_IMPORTE_HABER_D FROM TBCOMPROB_" & Session("CodEmpresa") & cAño & " C INNER JOIN TBPCGR_" & Session("CodEmpresa") & " P ON C.COMPROB_PLAN_CODIGO = P.PLAN_CODIGO " _
            & " WHERE (C.COMPROB_NRO_VOUCHER = '" & NroVoucher & "') AND (C.COMPROB_SYS_EST = '0') AND (C.COMPROB_PERIODO = '" & cboPeriodos.SelectedValue & "') AND (P.PLAN_AÑO = '" & cAño & "') "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                If cboMoneda.SelectedValue = 1 Then
                    lblSigno.Text = UCase("$.")
                ElseIf cboMoneda.SelectedValue = 2 Then
                    lblSigno.Text = UCase("s/.")
                Else
                    Rs.Close() : Cn.Close() : Exit Sub
                End If
                If Nu(Rs!COMPROB_IMPORTE_DEBE_D) <> "" Then
                    DebeDol = Format(Nz(Rs!COMPROB_IMPORTE_DEBE_D), "0.00#")
                Else
                    DebeDol = 0
                End If
                If Nu(Rs!COMPROB_IMPORTE_HABER_D) <> "" Then
                    HaberDol = Format(Nz(Rs!COMPROB_IMPORTE_HABER_D), "0.00#")
                Else
                    HaberDol = 0
                End If
                If Nu(Rs!COMPROB_IMPORTE_DEBE_S) <> "" Then
                    DebeSol = Format(Nz(Rs!COMPROB_IMPORTE_DEBE_S), "0.00#")
                Else
                    DebeSol = 0
                End If
                If Nu(Rs!COMPROB_IMPORTE_HABER_S) <> "" Then
                    HaberSol = Format(Nz(Rs!COMPROB_IMPORTE_HABER_S), "0.00#")
                Else
                    HaberSol = 0
                End If
                If cboMoneda.SelectedValue = 1 Then
                    dRow = dt.NewRow
                    dRow("C1") = ""
                    dRow("C2") = Nu(Rs!PLAN_CUENTA)
                    dRow("C3") = ""
                    dRow("C4") = ""
                    dRow("C5") = IIf(DebeDol = 0, "", Format(DebeDol, "0.00#"))
                    dRow("C6") = IIf(HaberDol = 0, "", Format(HaberDol, "0.00#"))
                    dRow("C7") = "x"
                    dRow("C8") = ""
                    dt.Rows.Add(dRow)
                ElseIf cboMoneda.SelectedValue = 2 Then
                    dRow = dt.NewRow
                    dRow("C1") = ""
                    dRow("C2") = Nu(Rs!PLAN_CUENTA)
                    dRow("C3") = IIf(DebeSol = 0, "", Format(DebeSol, "0.00#"))
                    dRow("C4") = IIf(HaberSol = 0, "", Format(HaberSol, "0.00#"))
                    dRow("C5") = ""
                    dRow("C6") = ""
                    dRow("C7") = "x"
                    dRow("C8") = ""
                    dt.Rows.Add(dRow)
                End If
            End While
        End If
        Flex.DataSource = dt
        Flex.DataBind()
        dt = Nothing
        Rs.Close() : Cn.Close()
        Call Totales_Comprobante()
    End Sub
    Protected Sub txtImporte_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtImporte.TextChanged
        If txtImporte.Text <> "" Then
            If Not IsNumeric(txtImporte.Text) Then
                lblError.Text = "Importe NO valido"
                txtImporte.Text = "0.00"
                Exit Sub
            End If
        End If
        If Session("Nuevo_Reg") = "S" Then Call LlenarGrilla()
        If Session("Nuevo_Reg") = "SP" Then Call Inserta_Grilla_Nueva_Parte()
    End Sub
    Protected Sub btnImporte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImporte.Click
        If Session("Nuevo_Reg") = "S" Then Call LlenarGrilla()
        If Session("Nuevo_Reg") = "SP" Then Call Inserta_Grilla_Nueva_Parte()
    End Sub
    Protected Sub opt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opt.SelectedIndexChanged
        If Session("Nuevo_Reg") = "S" Then Call LlenarGrilla()
        If Session("Nuevo_Reg") = "SP" Then Call Inserta_Grilla_Nueva_Parte()
    End Sub
    Private Function Hallar_CodNumerar(ByVal pAño As String) As String
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Hallar_CodNumerar = 1
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & Session("CodEmpresa") & pAño
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                Hallar_CodNumerar = Nz(Rs(0)) + 1
            End While
        Else : Hallar_CodNumerar = 1
        End If
        Rs.Close() : Cn.Close()
    End Function
    Private Function Voucher(ByVal pAño As String) As String
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Voucher = ""
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT DISTINCT COMPROB_NRO_VOUCHER, COMPROB_PERIODO From dbo.TBCOMPROB_" & Session("CodEmpresa") & pAño & " WHERE COMPROB_NRO_VOUCHER <> '" & lblPrefVoucher.Text & txtNroVoucher.Text & "' AND COMPROB_RUC_PERSONA= '" & lblCodPersona.Text & "' AND COMPROB_DOC_CODIGO ='" & cboTipoDoc.SelectedValue & "' AND COMPROB_NRO_DOC='" & txtNroDoc.Text.Trim & "' AND (COMPROB_SYS_EST = '0') AND (COMPROB_ASIENTO_CODIGO= '" & cboAsientos.SelectedValue.Trim & "')"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                Voucher = "Documento ingresado. " & Nu(Rs!COMPROB_NRO_VOUCHER)
            End While
        End If
        Rs.Close() : Cn.Close()
    End Function
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim nCod As Integer
        Dim Resp As String, Doc As String, DocR As String, CCosto As String, Presup As String, Flujo As String
        Dim ValorSys As String
        Dim A As Integer
        Dim CodNumerar As Integer
        ValorSys = ""
        If cboPeriodos.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Debe seleccionar el Periodo que ingresa el Comprobante"
        If Len(Trim(txtValorVenta.Text)) = 0 Or Val(txtValorVenta.Text) = 0 Then lblError.Text = lblError.Text & " <br> - Debe de ingresar el valor de venta de la moneda Dolar"
        If cboAsientos.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Falta seleccionar el Tipo de Asiento que pertenece el Comprobante, por favor seleccionar"
        If lblPrefVoucher.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Error, no existe la momenclatura o prefijo del asiento para el Comprobante"
        If txtNroVoucher.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Falta ingresar el Nro del Comprobante, por favor ingresar"
        Resp = Verificador_Cuenta(txtCuenta.Text)
        If Resp = "1" Then lblError.Text = lblError.Text & " <br> - Falta completar la Cuenta."
        If Resp = "3" Then lblError.Text = lblError.Text & " <br> - La Cuenta ingresada NO existe, por favor verificar o cambiar la Cuenta"
        If Resp = "5" Then lblError.Text = lblError.Text & " <br> - La Cuenta ingresada NO es válida, no puede ser cero," & Chr(13) & "por favor verificar o cambiar la Cuenta"
        If Resp = "6" Then lblError.Text = lblError.Text & " <br> - La Cuenta ingresada NO es válida, no puede saltear un nivel, un nivel no puede" & Chr(13) & "comenzar de cero, por favor verificar o cambiar la Cuenta"
        If Resp = "7" Then lblError.Text = lblError.Text & " <br> - La Cuenta ingresada NO corresponde a una cuenta con nivel" & Chr(13) & "de cuenta REGISTRO, por favor verificar o cambiar la Cuenta"
        If cboMoneda.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Es neceseario saber el tipo de moneda con el que se ingresa el Comprobante"
        If txtImporte.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Falta ingresar el importe para el Comprobante"
        If cboTipoDoc.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Falta seleccionar el tipo de documento."
        If txtNroDoc.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Falta ingresar el Nro del Documento seleccionado, por favor ingresar"
        If cboTipoDocRef.SelectedValue <> "(Seleccionar)" And txtNroDocRef.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Si va ha existir para el Comprobante un tipo de documento referencial" & Chr(13) & "deberá de ingresar el Nro del Doc. Referencial."
        If cboTipoDocRef.SelectedValue = "(Seleccionar)" And txtNroDocRef.Text.Trim <> "" Then lblError.Text = lblError.Text & " <br> - Si va ha existir para el Comprobante un tipo de documento referencial" & Chr(13) & "deberá de seleccionar Tipo de Doc. Referencial."
        If txtGlosa.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Falta ingresar la Glosa para el Comprobante, por favor ingresar"
        CCosto = "NULL"
        If lblCentroCosto.Text = "S" Then
            If cboCentroCosto.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Falta seleccionar el Centro de Costo"
            A = InStr(1, cboCentroCosto.SelectedValue, "(")
            If Mid(cboCentroCosto.SelectedValue, A + 1, 1) <> "R" Then lblError.Text = lblError.Text & " <br> - El Centro de Costo debe ser de Nivel R, por favor cambiar"
            CCosto = "'" & cboCentroCosto.SelectedValue & "'"
        End If
        Presup = "NULL"
        If lblPresupuesto.Text = "S" Then
            If cboPartPresupuestaria.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Falta seleccionar la Partida Presupuestaria"
            A = InStr(1, cboPartPresupuestaria.SelectedValue, "(")
            If Mid(cboPartPresupuestaria.SelectedValue, A + 1, 1) <> "R" Then lblError.Text = lblError.Text & " <br> - La Partida Presupuestaria debe ser de Nivel R, por favor cambiar"
            Presup = "'" & cboPartPresupuestaria.SelectedValue & "'"
        End If
        Flujo = "NULL"
        If lblFlujoCaja.Text = "S" Then
            If cboFlujoCaja.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Falta seleccionar Flujo Caja"
            Flujo = "'" & cboFlujoCaja.SelectedValue & "'"
        End If
        'ValorSys = ValorSistema
        If Session("Nuevo_Reg") = "S" Then Call LlenarGrilla()
        'If Session("Nuevo_Reg") = "SP" Then Call ListarGrilla()
        Resp = Verifica_Ruc()
        If Resp = "1" Then txtRuc.Text = "" 'MsgBox "Falta ingresar el Ruc, por favor ingresar", vbExclamation, Me.Caption: txtRuc.SetFocus: Exit Sub
        If Resp = "2" Then lblError.Text = lblError.Text & " <br> - El RUC debe ser de 11 digitos, por favor completar"
        If Resp = "3" Then txtRuc.Focus() : Exit Sub
        If Resp = "4" Then Exit Sub
        If lblError.Text.Trim <> "" Then
            lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
            Exit Sub
        End If
        lblError.Text = ""
        Doc = "" : DocR = ""
        Doc = cboTipoDoc.SelectedValue.Trim
        DocR = IIf(cboTipoDocRef.SelectedValue.Trim = "(Seleccionar)", "", cboTipoDocRef.SelectedValue)
        Dim cAño As String = Request.QueryString("pAño")
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal3 As New SqlCommand
        Dim Cn4 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal4 As New SqlCommand
        Dim Cn5 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal5 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim FechaDoc As String
        Dim FechaVcto As String
        Dim FechaReg As String
        Dim VoucherIngr As String
        VoucherIngr = Voucher(cAño)
        If VoucherIngr <> "" Then lblError.Text = VoucherIngr : Exit Sub
        CodNumerar = Hallar_CodNumerar(cAño)
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Cn4.Open() : CmdGlobal4.Connection = Cn4
        Cn5.Open() : CmdGlobal5.Connection = Cn5
        FechaDoc = Right(txtFechaDoc.Text.Trim, 4) & Mid(txtFechaDoc.Text.Trim, 4, 2) & Left(txtFechaDoc.Text.Trim, 2)
        FechaVcto = Right(txtFechaVcto.Text.Trim, 4) & Mid(txtFechaVcto.Text.Trim, 4, 2) & Left(txtFechaVcto.Text.Trim, 2)
        FechaReg = Right(txtFechaReg.Text.Trim, 4) & Mid(txtFechaReg.Text.Trim, 4, 2) & Left(txtFechaReg.Text.Trim, 2)
        If Session("Nuevo_Reg") = "S" Or Session("Nuevo_Reg") = "SP" Then
            'SE GUARDA CENTRO DE COSTO SÓLO A LA CUENTA PRINCIPAL
            CmdGlobal2.CommandText = "INSERT INTO TBCOMPROB_" & Session("CodEmpresa") & cAño _
            & "(COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER,COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
            & "COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, COMPROB_DOC_CODIGO,COMPROB_NRO_DOC, COMPROB_DOC_REF,  COMPROB_NRO_DOC_REF, COMPROB_RUC_PERSONA, " _
            & "COMPROB_GLOSA, COMPROB_CENTRO_COSTO, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE,COMPROB_OPCION,COMPROB_PART_PRESUPUESTARIA,COMPROB_FLUJOCAJA,COMPROB_ESTADO) " _
            & "VALUES('" & cboPeriodos.SelectedValue.Trim & "','" & CodNumerar & "','" & cboAsientos.SelectedValue & "','" & (lblPrefVoucher.Text & txtNroVoucher.Text) & "'," _
            & "'" & lblCodCuenta.Text & "','" & cboMoneda.SelectedValue & "','" & txtValorVenta.Text & "'," _
            & "'" & FechaDoc & "','" & FechaVcto & "','" & FechaReg & "','" & Doc & "'," _
            & "'" & txtNroDoc.Text.Trim & "'," & IIf(DocR = "", "NULL", "'" & DocR & "'") & "," & IIf(DocR = "", "NULL", txtNroDocRef.Text) & ",'" & lblCodPersona.Text & "'," _
            & "'" & Trim(txtGlosa.Text) & "'," & CCosto & ",'0','" & ValorSys & "','" & txtImporte.Text & "','" & IIf(opt.SelectedValue = 0, "D", "H") & "'," & Presup & ", " & Flujo & ",'1')"
            CmdGlobal2.ExecuteNonQuery()
            If cboMoneda.SelectedValue = 1 Then
                If opt.SelectedValue = 1 Then
                    CmdGlobal3.CommandText = " UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D='" & IIf(cboAsientos.SelectedValue = "99", "0.00", txtImporte.Text) & "',COMPROB_IMPORTE_DEBE_D=NULL," _
                                           & " COMPROB_IMPORTE_HABER_S='" & Format(CDbl(txtImporte.Text) * CDbl(txtValorVenta.Text), "0.0#") & "',COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & CodNumerar & "'"
                    CmdGlobal3.ExecuteNonQuery()
                ElseIf opt.SelectedValue = 0 Then
                    CmdGlobal3.CommandText = " UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(cboAsientos.SelectedValue = 99, "0.00", txtImporte.Text) & "'," _
                                           & " COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & Format(CDbl(txtImporte.Text) * CDbl(txtValorVenta.Text), "0.0#") & "' WHERE COMPROB_NUMERAR='" & CodNumerar & "'"
                    CmdGlobal3.ExecuteNonQuery()
                End If
            ElseIf cboMoneda.SelectedValue = 2 Then
                If opt.SelectedValue = 1 Then
                    CmdGlobal3.CommandText = " UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D='" & IIf(cboAsientos.SelectedValue = "99", "0.00", Format(CDbl(txtImporte.Text) / CDbl(txtValorVenta.Text), "0.0#")) & "',COMPROB_IMPORTE_DEBE_D=NULL," _
                                           & " COMPROB_IMPORTE_HABER_S='" & txtImporte.Text & "',COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & CodNumerar & "'"
                    CmdGlobal3.ExecuteNonQuery()
                ElseIf opt.SelectedValue = 0 Then
                    CmdGlobal3.CommandText = " UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(cboAsientos.SelectedValue = 99, "0.00", Format(CDbl(txtImporte.Text) / CDbl(txtValorVenta.Text), "0.0#")) & "'," _
                                           & " COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & txtImporte.Text & "' WHERE COMPROB_NUMERAR='" & CodNumerar & "'"
                    CmdGlobal3.ExecuteNonQuery()
                End If
            End If
            nCod = Hallar_CodNumerar(cAño)
            Dim i As Integer
            'GUARDAR LAS CUENTAS ACREDEDORAS Y DEUDORAS
            If Flex.Rows.Count > 2 Then
                For i = 1 To Flex.Rows.Count - 1
                    If Flex.Rows(i).Cells(6).Text <> "x" Then
                        nCod = nCod + 1
                        CmdGlobal2.CommandText = "INSERT INTO TBCOMPROB_" & Session("CodEmpresa") & cAño _
                                               & "(COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER,COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
                                               & "COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, COMPROB_DOC_CODIGO,COMPROB_NRO_DOC, COMPROB_DOC_REF,  COMPROB_NRO_DOC_REF, COMPROB_RUC_PERSONA, " _
                                               & "COMPROB_GLOSA,COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE,COMPROB_OPCION,COMPROB_RELAC_COMPROB) " _
                                               & "VALUES('" & cboPeriodos.SelectedValue & "','" & nCod & "','" & cboAsientos.SelectedValue & "','" & (lblPrefVoucher.Text & txtNroVoucher.Text) & "'," _
                                               & "'" & Flex.Rows(i).Cells(6).Text & "','" & cboMoneda.SelectedValue & "','" & txtValorVenta.Text & "'," _
                                               & "'" & FechaDoc & "','" & FechaVcto & "','" & FechaReg & "','" & Doc & "'," _
                                               & "'" & txtNroDoc.Text.Trim & "'," & IIf(DocR = "", "NULL", "'" & DocR & "'") & "," & IIf(DocR = "", "NULL", "'" & txtNroDocRef.Text & "'") & ",'" & lblCodPersona.Text & "'," _
                                               & "'" & txtGlosa.Text.Trim & "','0','" & ValorSys & "','" & txtImporte.Text & "','" & Flex.Rows(i).Cells(7).Text & "','" & CodNumerar & "')"
                        CmdGlobal2.ExecuteNonQuery()
                        'GUARDAR CENTRO DE COSTO A LAS CUENTAS ACREDEDORAS O DEUDORAS SI ESTAN TIENEN SI CENTRO DE COSTO
                        CmdGlobal.CommandText = "SELECT PLAN_CENTRO_COSTOS,PLAN_PRESUPUESTO,PLAN_FLUJOCAJA FROM TBPCGR_" & Session("CodEmpresa") & " WHERE (PLAN_CODIGO = " & Flex.Rows(i).Cells(6).Text & ") AND (PLAN_AÑO = '" & cAño & "') AND (PLAN_SYS_EST = '0') "
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                If Nu(Rs!PLAN_CENTRO_COSTOS) = "S" Then
                                    CmdGlobal5.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_CENTRO_COSTO=" & CCosto & " WHERE COMPROB_NUMERAR='" & nCod & "'"
                                    CmdGlobal5.ExecuteNonQuery()
                                End If
                                If Nu(Rs!PLAN_PRESUPUESTO) = "S" Then
                                    CmdGlobal5.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_PART_PRESUPUESTARIA=" & Presup & " WHERE COMPROB_NUMERAR='" & nCod & "'"
                                    CmdGlobal5.ExecuteNonQuery()
                                End If
                                If Nu(Rs!PLAN_FLUJOCAJA) = "S" Then
                                    CmdGlobal5.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_FLUJOCAJA=" & Flujo & " WHERE COMPROB_NUMERAR='" & nCod & "'"
                                    CmdGlobal5.ExecuteNonQuery()
                                End If
                            End While
                        End If
                        Rs.Close()
                        If cboMoneda.SelectedValue = 1 Then
                            If Flex.Rows(i).Cells(7).Text = "H" Then
                                CmdGlobal4.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D='" & IIf(cboAsientos.SelectedValue = 99, "0.00", txtImporte) & "',COMPROB_IMPORTE_DEBE_D=NULL," _
                                & "COMPROB_IMPORTE_HABER_S='" & Format(CDbl(txtImporte.Text) * CDbl(txtValorVenta.Text), "0.0#") & "',COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & nCod & "'"
                                CmdGlobal4.ExecuteNonQuery()
                            ElseIf Flex.Rows(i).Cells(7).Text = "D" Then
                                CmdGlobal4.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(cboAsientos.SelectedValue = 99, "0.00", txtImporte) & "'," _
                                & "COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & Format(CDbl(txtImporte.Text) * CDbl(txtValorVenta.Text), "0.0#") & "' WHERE COMPROB_NUMERAR='" & nCod & "'"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                        ElseIf cboMoneda.SelectedValue = 2 Then
                            If Flex.Rows(i).Cells(7).Text = "H" Then
                                CmdGlobal4.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D='" & IIf(cboAsientos.SelectedValue = 99, "0.00", Format(CDbl(txtImporte.Text) / CDbl(txtValorVenta.Text), "0.0#")) & "',COMPROB_IMPORTE_DEBE_D=NULL," _
                               & "COMPROB_IMPORTE_HABER_S='" & txtImporte.Text & "',COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & nCod & "'"
                                CmdGlobal4.ExecuteNonQuery()
                            ElseIf Flex.Rows(i).Cells(7).Text = "D" Then
                                CmdGlobal4.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(cboAsientos.SelectedValue = 99, "0.00", Format(CDbl(txtImporte.Text) / CDbl(txtValorVenta.Text), "0.0#")) & "'," _
                                & "COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & txtImporte.Text & "' WHERE COMPROB_NUMERAR='" & nCod & "'"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                        End If
                    End If
                Next
            End If
            Dim mDif As Double, mDs As Double, mDd As Double
            If lblDiferencia.Text.Trim = "" Then lblDiferencia.Text = 0
            mDif = lblDiferencia.Text
            If cboMoneda.SelectedValue = 1 Then 'DOLARES
                mDd = mDif
                mDs = mDif * CDbl(txtValorVenta.Text)
                mDs = CDbl(Format(mDs, "0.0#"))
                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_DIFERENCIA_S='" & mDs & "',COMPROB_DIFERENCIA_D='" & mDd & "' WHERE COMPROB_NRO_VOUCHER='" & (lblPrefVoucher.Text & txtNroVoucher.Text) & "' AND COMPROB_PERIODO='" & cboPeriodos.SelectedValue & "'"
                CmdGlobal.ExecuteNonQuery()
            ElseIf cboMoneda.SelectedValue = 2 Then 'SOLARES
                mDd = mDif / CDbl(txtValorVenta.Text)
                mDd = CDbl(Format(mDd, "0.0#"))
                mDs = mDif
                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & Session("CodEmpresa") & cAño & " SET COMPROB_DIFERENCIA_S='" & mDs & "',COMPROB_DIFERENCIA_D='" & mDd & "' WHERE COMPROB_NRO_VOUCHER='" & (lblPrefVoucher.Text & txtNroVoucher.Text) & "' AND COMPROB_PERIODO='" & cboPeriodos.SelectedValue & "'"
                CmdGlobal.ExecuteNonQuery()
            End If
        Else
            '
        End If
    End Sub
    Private Function Verificador_Cuenta(ByVal CadVerif As String) As String
        Dim ii As Integer, aa As Integer
        Dim Cad As String
        Dim cAño As String = Request.QueryString("pAño")
        lblCodCuenta.Text = ""
        lblCentroCosto.Text = ""
        lblPresupuesto.Text = ""
        lblCtaDeudora.Text = ""
        lblCtaAcreedora.Text = ""
        Verificador_Cuenta = ""
        For ii = 1 To Len(CadVerif)
            If Len(Trim(Mid(CadVerif, ii, 1))) = 0 Then Verificador_Cuenta = "1" : Exit Function
        Next
        'VERIFICAR QUE LA CUENTA NO SALTEE NINGÚN NIVEL Y NO SEA DE CEROS
        Cad = ""
        For ii = 1 To Len(CadVerif)
            If Trim(Mid(CadVerif, ii, 1)) = "." Then
            Else
                Cad = Cad & Trim(Mid(CadVerif, ii, 1))
            End If
        Next
        If Val(Cad) = 0 Then Verificador_Cuenta = "5" : Exit Function
        Cad = "" : aa = 0
        For ii = 1 To Len(CadVerif)
            If Trim(Mid(CadVerif, ii, 1)) = "." Then
                If Val(Cad) = 0 Then aa = ii : Exit For
                Cad = ""
            Else
                Cad = Cad & Trim(Mid(CadVerif, ii, 1))
            End If
        Next
        If aa <> 0 Then
            Cad = ""
            For ii = ii - 1 To Len(CadVerif)
                If Trim(Mid(CadVerif, ii, 1)) = "." Then
                    If Val(Cad) > 0 Then Verificador_Cuenta = "6" : Exit Function
                    Cad = ""
                Else
                    Cad = Cad & Trim(Mid(CadVerif, ii, 1))
                    If Len(CadVerif) = ii Then
                        If Val(Cad) > 0 Then Verificador_Cuenta = "6" : Exit Function
                    End If
                End If
            Next
        End If
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT * FROM TBPCGR_" & Session("CodEmpresa") & "  WHERE (PLAN_AÑO = '" & cAño & "') AND (PLAN_SYS_EST = '0')  AND (PLAN_CUENTA='" & CadVerif & "')"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                If Nu(Rs!PLAN_NIVEL_CUENTA) = "R" Then
                    Verificador_Cuenta = ""
                    lblCodCuenta.Text = Nu(Rs!PLAN_CODIGO)
                    lblCtaDeudora.Text = IIf(Nu(Rs!PLAN_ASIENTO_DESTINO) = "S", Nu(Rs!PLAN_CUENTA_DEUDORA), "")
                    lblCtaAcreedora.Text = IIf(Nu(Rs!PLAN_ASIENTO_DESTINO) = "S", Nu(Rs!PLAN_CUENTA_ACREEDORA), "")
                    lblCentroCosto.Text = IIf(Nu(Rs!PLAN_CENTRO_COSTOS) = "", "N", Nu(Rs!PLAN_CENTRO_COSTOS))
                    lblPresupuesto.Text = IIf(Nu(Rs!PLAN_PRESUPUESTO) = "", "N", Nu(Rs!PLAN_PRESUPUESTO))
                Else
                    Verificador_Cuenta = "7"
                End If
            End While
        Else
            Verificador_Cuenta = "3"
        End If
        Rs.Close() : Cn.Close()
    End Function
    Private Function Verifica_Ruc() As String
        lblCodPersona.Text = ""
        Verifica_Ruc = ""
        If Len(txtRuc.Text) = 0 Then Verifica_Ruc = "1" : Exit Function
        If Len(txtRuc.Text) <> 11 Then Verifica_Ruc = "2" : Exit Function
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT * FROM TBDATA_PERSONAS WHERE (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND PERSONA_RUC='" & txtRuc.Text & "' AND PERSONA_SYS_EST='0'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                lblCodPersona.Text = Nu(Rs!PERSONA_CODIGO)
            End While
        Else
            If lblError.Text = "El RUC ingresado no pertenece a ninguna persona registrada de la Empresa." Then
                Verifica_Ruc = "4"
                Exit Function
            Else
                Verifica_Ruc = "3"
            End If
        End If
        Rs.Close() : Cn.Close()
    End Function
    Protected Sub txtFechaDoc_ValueChanged(ByVal sender As Object, ByVal e As Infragistics.WebUI.WebSchedule.WebDateChooser.WebDateChooserEventArgs) Handles txtFechaDoc.ValueChanged
        txtValorVenta.Text = Hallar_Valor_Venta(txtFechaDoc.Text)
    End Sub
End Class

