Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Contabilidad_Voucher
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cboAño.Items.Clear()
            cboMoneda.Items.Clear()
            cboAsientos.Items.Clear()
            cboPeriodos.Items.Clear()
            cboMes.Items.Clear()
            Call LlenaMes(cboMes, True)
            cboMes.Items.Add("(Seleccionar)") : cboMes.SelectedValue = "(Seleccionar)"
            Call LlenaComboItem("TBOPC015", cboMoneda)
            cboMoneda.Items.Add("(Seleccionar)") : cboMoneda.SelectedValue = "(Seleccionar)"
            Call LlenaAno(cboAño)
            cboAño.SelectedValue = CInt(Left(FechaActual, 4))
            cboAño.Focus()
            Call cboAño_SelectedIndexChanged(sender, e)
            chkPeriodo.Checked = True
            Call chkPeriodo_CheckedChanged(sender, e)
        End If
    End Sub
    Protected Sub chkMes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMes.CheckedChanged
        If chkMes.Checked = True Then cboMes.Enabled = True Else cboMes.Enabled = False
        cboMes.SelectedValue = "(Seleccionar)"
    End Sub
    Protected Sub chkPeriodo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPeriodo.CheckedChanged
        If cboAño.Text = "" Then Exit Sub
        If chkPeriodo.Checked = True Then cboPeriodos.Enabled = True Else cboPeriodos.Enabled = False
    End Sub
    Protected Sub chkMoneda_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkMoneda.CheckedChanged
        If chkMoneda.Checked = True Then cboMoneda.Enabled = True Else cboMoneda.Enabled = False
        cboMoneda.SelectedValue = "(Seleccionar)"
    End Sub
    Protected Sub chkAsiento_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAsiento.CheckedChanged
        If chkAsiento.Checked = True Then cboAsientos.Enabled = True Else cboAsientos.Enabled = False
        cboAsientos.SelectedValue = "(Seleccionar)"
    End Sub
    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAño.SelectedIndexChanged
        Call LlenaPeriodos()
        Call LlenaAsientos()
    End Sub
    Private Sub LlenaAsientos()
        Try
            Dim obj As New clsCont_Listados
            cboAsientos.DataSource = obj.Cont_ListaAsientos(Session("CodEmpresa"), cboAño.SelectedValue.Trim, "No", "0", Session("Ruta_Emp"))
            cboAsientos.DataTextField = "NOMBRE"
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
    Private Sub LlenaPeriodos()
        Try
            Dim PerActual As Integer
            Dim obj As New clsCont_Listados
            Dim dt As New DataTable
            cboPeriodos.DataSource = obj.Cont_ListaPeriodos(Session("CodEmpresa"), cboAño.SelectedValue.Trim, "No", "0", Session("Ruta_Emp"))
            cboPeriodos.DataTextField = "PERIODO_NOMBRE"
            cboPeriodos.DataValueField = "PER_PERIODO"
            cboPeriodos.DataBind()
            dt = obj.Cont_ListaPeriodos(Session("CodEmpresa"), cboAño.SelectedValue.Trim, "No", "0", Session("Ruta_Emp"))
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
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Listar_Voucher()
    End Sub
    Private Function Cargar_datos() As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Sql = " SELECT V.COMPROB_PERIODO, V.COMPROB_NUMERAR,V.COMPROB_ASIENTO_CODIGO, V.COMPROB_NRO_VOUCHER,V.COMPROB_PLAN_CODIGO, P.PLAN_CUENTA, V.COMPROB_MONEDA, " _
            & " (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC015' AND ELEMEN_CODIGO = V.COMPROB_MONEDA) AS MONEDAV, " _
            & " SUBSTRING(V.COMPROB_FEC_REGISTRO,7,2)+'/'+SUBSTRING(V.COMPROB_FEC_REGISTRO,5,2)+'/'+SUBSTRING(V.COMPROB_FEC_REGISTRO,3,2) AS FEC_REG, " _
            & " SUBSTRING(V.COMPROB_FEC_DOC,7,2)+'/'+SUBSTRING(V.COMPROB_FEC_DOC,5,2)+'/'+SUBSTRING(V.COMPROB_FEC_DOC,3,2) AS FEC_DOC, " _
            & " SUBSTRING(V.COMPROB_FEC_VCTO,7,2)+'/'+SUBSTRING(V.COMPROB_FEC_VCTO,5,2)+'/'+SUBSTRING(V.COMPROB_FEC_VCTO,3,2) AS FEC_VCTO, " _
            & " V.COMPROB_TIPOCAM, V.COMPROB_FEC_REGISTRO, V.COMPROB_FEC_DOC, V.COMPROB_FEC_VCTO, D.DOC_DOCUMENTO, V.COMPROB_DOC_CODIGO, V.COMPROB_NRO_DOC, V.COMPROB_RUC_PERSONA , " _
            & " (SELECT R.PERSONA_RUC FROM TBDATA_PERSONAS R WHERE V.COMPROB_RUC_PERSONA = R.PERSONA_CODIGO AND R.EMPRESA_CODIGO=D.DOC_EMPRESA) AS RUC,  " _
            & " V.COMPROB_GLOSA, V.COMPROB_CENTRO_COSTO, V.COMPROB_OPCION, V.COMPROB_IMPORTE, V.COMPROB_DOC_REF, V.COMPROB_NRO_DOC_REF,  " _
            & " (SELECT CCOSTO_DESCRIPCION FROM TBCENTROCOSTOS WHERE CCOSTO_CODIGO = V.COMPROB_CENTRO_COSTO AND CCOSTO_EMPRESA = '" & Session("CodEmpresa") & "' AND CCOSTO_AÑO= '" & cboAño.SelectedValue.Trim & "' ) AS CENTRO_COSTOV, " _
            & " (SELECT PRES_DESCRIPCION FROM TBPRESUPUESTO_" & Session("CodEmpresa") & "  WHERE PRES_CODIGO = V.COMPROB_PART_PRESUPUESTARIA AND PRES_AÑO = '" & cboAño.SelectedValue.Trim & "') AS PART_PRESU," _
            & " COMPROB_IMPORTE_DEBE_S, COMPROB_IMPORTE_HABER_S, COMPROB_IMPORTE_DEBE_D, COMPROB_IMPORTE_HABER_D, COMPROB_RELAC_COMPROB, COMPROB_DIFERENCIA_D, COMPROB_DIFERENCIA_S, " _
            & " (SELECT FLUCAJA_DESCRIPCION FROM TBFLUJOCAJA F WHERE  F.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND F.FLUCAJA_CODIGO=V.COMPROB_FLUJOCAJA AND FLUCAJA_AÑO='" & cboAño.SelectedValue.Trim & "') AS FLUJOCAJA " _
            & " FROM TBCOMPROB_" & Session("CodEmpresa") & cboAño.SelectedValue.Trim & " V INNER JOIN  TBPCGR_" & Session("CodEmpresa") & " P ON  V.COMPROB_PLAN_CODIGO = P.PLAN_CODIGO  " _
            & " INNER JOIN  TBDOCUMENTOS D ON  V.COMPROB_DOC_CODIGO = D.DOC_CODIGO " _
            & " WHERE  P.PLAN_AÑO = '" & cboAño.SelectedValue.Trim & "' " _
            & " AND  D.DOC_AÑO  = '" & cboAño.SelectedValue.Trim & "' " _
            & " AND  D.DOC_EMPRESA = '" & Session("CodEmpresa") & "' " _
            & " AND V.COMPROB_SYS_EST = '0'"
        If chkPeriodo.Checked = True And cboPeriodos.SelectedValue.Trim <> "(Seleccionar)" Then Sql = Sql & " AND (V.COMPROB_PERIODO='" & cboPeriodos.SelectedValue.Trim & "') "
        If chkMoneda.Checked = True And cboMoneda.SelectedValue.Trim <> "(Seleccionar)" Then Sql = Sql & " AND (V.COMPROB_MONEDA='" & cboMoneda.SelectedValue.Trim & "') "
        If chkMes.Checked = True And cboMes.SelectedValue.Trim <> "(Seleccionar)" Then Sql = Sql & " AND (SUBSTRING(V.COMPROB_FEC_REGISTRO,5,2)='" & Format(cboMes.SelectedValue.Trim, "00") & "') "
        If chkAsiento.Checked = True And cboAsientos.SelectedValue.Trim <> "(Seleccionar)" Then Sql = Sql & " AND V.COMPROB_ASIENTO_CODIGO ='" & cboAsientos.SelectedValue.Trim & "'"
        Sql = Sql & " ORDER BY COMPROB_PERIODO,COMPROB_NRO_VOUCHER,COMPROB_NUMERAR"
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Return Dt
    End Function
    Private Sub Listar_Voucher()
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As Data.DataRow
        Dim NroVoucher As String : NroVoucher = ""
        Dim Diferencia As String : Diferencia = ""
        Dim i As Integer : i = 0
        dt2.Columns.Add("COMPROB_PERIODO")
        dt2.Columns.Add("FEC_REG")
        dt2.Columns.Add("COMPROB_ASIENTO_CODIGO")
        dt2.Columns.Add("COMPROB_NRO_VOUCHER")
        dt2.Columns.Add("PLAN_CUENTA")
        dt2.Columns.Add("MONEDAV")
        dt2.Columns.Add("COMPROB_IMPORTE_DEBE_S")
        dt2.Columns.Add("COMPROB_IMPORTE_HABER_S")
        dt2.Columns.Add("COMPROB_IMPORTE_DEBE_D")
        dt2.Columns.Add("COMPROB_IMPORTE_HABER_D")
        dt2.Columns.Add("COMPROB_TIPOCAM")
        dt2.Columns.Add("FEC_DOC")
        dt2.Columns.Add("FEC_VCTO")
        dt2.Columns.Add("COMPROB_DOC_CODIGO")
        dt2.Columns.Add("COMPROB_NRO_DOC")
        dt2.Columns.Add("COMPROB_DOC_REF")
        dt2.Columns.Add("COMPROB_NRO_DOC_REF")
        dt2.Columns.Add("RUC")
        dt2.Columns.Add("COMPROB_GLOSA")
        dt2.Columns.Add("CENTRO_COSTOV")
        dt2.Columns.Add("COMPROB_NUMERAR")
        dt2.Columns.Add("COMPROB_RELAC_COMPROB")
        dt2.Columns.Add("COMPROB_DIFERENCIA_D")
        dt2.Columns.Add("PART_PRESU")
        dt2.Columns.Add("FLUJOCAJA")
        dt = Cargar_datos()
        If dt.Rows.Count > 0 Then
            For Each drMenuItem As Data.DataRow In dt.Rows
                If NroVoucher <> Nu(drMenuItem("COMPROB_NRO_VOUCHER")) Then
                    If dt2.Rows.Count >= 2 Then
                        dRow = dt2.NewRow
                        dRow("COMPROB_IMPORTE_DEBE_S") = "===============" : dRow("COMPROB_IMPORTE_HABER_S") = "==============="
                        dRow("COMPROB_IMPORTE_DEBE_D") = "===============" : dRow("COMPROB_IMPORTE_HABER_D") = "==============="
                        dt2.Rows.Add(dRow)
                        dt2 = Total_Comprobante(NroVoucher, dt2.Rows.Count, Diferencia, dt2)
                        dRow = dt2.NewRow
                        dt2.Rows.Add(dRow)
                    End If
                    NroVoucher = Nu(drMenuItem("COMPROB_NRO_VOUCHER"))
                    If Nu(drMenuItem("COMPROB_MONEDA")) = "1" Then
                        Diferencia = UCase("$.  ") & Format(Nz(drMenuItem("COMPROB_DIFERENCIA_D")), "0.00")
                    Else
                        Diferencia = UCase("S/.  ") & Format(Nz(drMenuItem("COMPROB_DIFERENCIA_S")), "0.00")
                    End If
                End If
                dRow = dt2.NewRow
                i = i + 1
                dRow("COMPROB_PERIODO") = Nu(drMenuItem("COMPROB_PERIODO"))
                dRow("FEC_REG") = Nu(drMenuItem("FEC_REG"))
                dRow("COMPROB_ASIENTO_CODIGO") = Nu(drMenuItem("COMPROB_ASIENTO_CODIGO"))
                dRow("COMPROB_NRO_VOUCHER") = Nu(drMenuItem("COMPROB_NRO_VOUCHER"))
                dRow("PLAN_CUENTA") = Nu(drMenuItem("PLAN_CUENTA"))
                dRow("MONEDAV") = Nu(drMenuItem("MONEDAV"))
                dRow("COMPROB_IMPORTE_DEBE_S") = Nz(drMenuItem("COMPROB_IMPORTE_DEBE_S"))
                dRow("COMPROB_IMPORTE_HABER_S") = Nz(drMenuItem("COMPROB_IMPORTE_HABER_S"))
                dRow("COMPROB_IMPORTE_DEBE_D") = Nz(drMenuItem("COMPROB_IMPORTE_DEBE_D"))
                dRow("COMPROB_IMPORTE_HABER_D") = Nz(drMenuItem("COMPROB_IMPORTE_HABER_D"))
                dRow("COMPROB_TIPOCAM") = Nz(drMenuItem("COMPROB_TIPOCAM"))
                dRow("FEC_DOC") = Nu(drMenuItem("FEC_DOC"))
                dRow("FEC_VCTO") = Nu(drMenuItem("FEC_VCTO"))
                dRow("COMPROB_DOC_CODIGO") = Nu(drMenuItem("COMPROB_DOC_CODIGO"))
                dRow("COMPROB_NRO_DOC") = Nu(drMenuItem("COMPROB_NRO_DOC"))
                dRow("COMPROB_DOC_REF") = Nu(drMenuItem("COMPROB_DOC_REF"))
                dRow("COMPROB_NRO_DOC_REF") = Nu(drMenuItem("COMPROB_NRO_DOC_REF"))
                dRow("RUC") = Nu(drMenuItem("RUC"))
                dRow("COMPROB_GLOSA") = Nu(drMenuItem("COMPROB_GLOSA"))
                dRow("CENTRO_COSTOV") = Nu(drMenuItem("CENTRO_COSTOV"))
                dRow("COMPROB_NUMERAR") = Nu(drMenuItem("COMPROB_NUMERAR"))
                dRow("COMPROB_RELAC_COMPROB") = Nz(drMenuItem("COMPROB_RELAC_COMPROB"))
                dRow("COMPROB_DIFERENCIA_D") = Nz(drMenuItem("COMPROB_DIFERENCIA_D"))
                dRow("PART_PRESU") = Nu(drMenuItem("PART_PRESU"))
                dRow("FLUJOCAJA") = Nu(drMenuItem("FLUJOCAJA"))
                dt2.Rows.Add(dRow)
                If dt.Rows.Count = i Then
                    dRow = dt2.NewRow
                    dRow("COMPROB_IMPORTE_DEBE_S") = "===============" : dRow("COMPROB_IMPORTE_HABER_S") = "==============="
                    dRow("COMPROB_IMPORTE_DEBE_D") = "===============" : dRow("COMPROB_IMPORTE_HABER_D") = "==============="
                    dt2.Rows.Add(dRow)
                    dt2 = Total_Comprobante(NroVoucher, dt2.Rows.Count, Diferencia, dt2)
                End If
            Next
        End If
        Flex.DataSource = dt2
        Flex.DataBind()
        'If Flex.Rows.Count > 0 Then Flex.Rows(Flex.Rows.Count - 1).Cells(3).BackColor = Drawing.Color.White
        'If Flex.Rows.Count > 0 And Flex.Rows(0).Cells(3).Text = "" Then Flex.Rows(0).Cells(2).BackColor = Drawing.Color.SeaGreen
    End Sub
    Function Total_Comprobante(ByVal NroVoucher As String, ByVal nF As String, ByVal Dif As String, ByVal datos As DataTable) As DataTable
        Dim m1 As Double, m2 As Double, m3 As Double, m4 As Double
        Dim dRowT As Data.DataRow
        If datos.Rows.Count > 0 Then
            For Each dr As Data.DataRow In datos.Rows
                If Nu(dr("COMPROB_NRO_VOUCHER")) = NroVoucher Then
                    If Nz(dr("COMPROB_IMPORTE_DEBE_S")) <> 0 Then m1 = m1 + CDbl(dr("COMPROB_IMPORTE_DEBE_S"))
                    If Nz(dr("COMPROB_IMPORTE_HABER_S")) <> 0 Then m2 = m2 + CDbl(dr("COMPROB_IMPORTE_HABER_S"))
                    If Nz(dr("COMPROB_IMPORTE_DEBE_D")) <> 0 Then m3 = m3 + CDbl(dr("COMPROB_IMPORTE_DEBE_D"))
                    If Nz(dr("COMPROB_IMPORTE_HABER_D")) <> 0 Then m4 = m4 + CDbl(dr("COMPROB_IMPORTE_HABER_D"))
                End If
            Next
        End If
        dRowT = datos.NewRow
        dRowT("MONEDAV") = "TOTAL :"
        dRowT("COMPROB_ASIENTO_CODIGO") = "DIFER. :"
        dRowT("COMPROB_NRO_VOUCHER") = Dif
        dRowT("COMPROB_IMPORTE_DEBE_S") = FormatoNumero(m1)
        dRowT("COMPROB_IMPORTE_HABER_S") = FormatoNumero(m2)
        dRowT("COMPROB_IMPORTE_DEBE_D") = FormatoNumero(m3)
        dRowT("COMPROB_IMPORTE_HABER_D") = FormatoNumero(m4)
        datos.Rows.Add(dRowT)
        Return datos
    End Function
    Private Function FormatoNumero(ByVal Num As Double) As String
        Dim sNum As String, ii As Integer
        sNum = Trim(str(Num))
        ii = InStr(1, sNum, ".")
        If ii = 0 Then
            If Len(sNum) <= 3 Then FormatoNumero = Format(Num, "0.00") Else FormatoNumero = Format(Num, "0,000.00")
        Else
            sNum = Left(sNum, ii - 1)
            If Len(sNum) <= 3 Then FormatoNumero = Format(Num, "0.00") Else FormatoNumero = Format(Num, "0,000.00")
        End If
    End Function
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Listar_Voucher()
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Dim Año As String : Año = ""
        Session("Nuevo_Reg") = "S"
        Año = cboAño.SelectedValue.Trim
        Response.Redirect("Contabilidad_Voucher_Detalle.aspx?pAño=" & Año & "")
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim Año As String : Año = ""
        Dim CodNumerar As String
        If e.CommandName = "Continuar" Then
            Try
                If Flex.Rows(Index).Cells(22).Text = "0" Then
                    CodNumerar = Flex.Rows(Index).Cells(21).Text
                Else
                    CodNumerar = Flex.Rows(Index).Cells(22).Text
                End If
                Session("Nuevo_Reg") = "SP"
                Año = cboAño.SelectedValue.Trim
                Response.Redirect("Contabilidad_Voucher_Detalle.aspx?pAño=" & Año & "&CodComprob=" & CodNumerar & "")
            Catch ex As SqlException
                lblError.Text = ex.Message
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
            End Try
        End If
    End Sub

End Class
