Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Contabilidad_PCGR
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Panel1.Visible = False
            cboAño.Items.Clear()
            Call LlenaAno(cboAño)
            cboAño.SelectedValue = CInt(Left(FechaActual, 4))
            cboAño.Focus()
            Call cboAño_SelectedIndexChanged(sender, e)
        End If
    End Sub
    Protected Sub cboAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAño.SelectedIndexChanged
        Call Cargar_Plan()
    End Sub
    Private Sub Cargar_Plan()
        Try
            Flex.DataSource = Cargar_datos()
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
            'FlexBusCta.DataSource = Cargar_datos()
            'FlexBusCta.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Cargar_Plan()
    End Sub
    Private Function Cargar_datos() As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Sql = " SELECT P.PLAN_CODIGO, P.PLAN_CUENTA, P.PLAN_COD_NIVEL, P.PLAN_NOMBRE_CUENTA, P.PLAN_NIVEL_CUENTA," _
            & " P.PLAN_TIPO_CUENTA, P.PLAN_TIPO_SALDO, PLAN_TIPO_ANALISIS, PLAN_CENTRO_COSTOS, PLAN_ASIENTO_DESTINO, " _
            & " PLAN_CUENTA_DEUDORA, PLAN_CUENTA_ACREEDORA, PLAN_PRESUPUESTO, PLAN_FLUJOCAJA, PLAN_CBANCO_CODIGO, PLAN_CODIGO_IMPUESTO," _
            & " (SELECT V.PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " V WHERE V.PLAN_AÑO = '" & cboAño.SelectedValue & "' AND V.PLAN_CODIGO=P.PLAN_CUENTA_DEUDORA) AS CUENTA_DEUDORA, " _
            & " (SELECT V.PLAN_CUENTA FROM TBPCGR_" & Session("CodEmpresa") & " V WHERE V.PLAN_AÑO = '" & cboAño.SelectedValue & "' AND V.PLAN_CODIGO=P.PLAN_CUENTA_ACREEDORA) AS CUENTA_ACREEDORA, " _
            & " (SELECT B.BANCO_NOMBRE FROM TBBANCOS B WHERE B.BANCO_CODIGO = (SELECT C.BANCO_CODIGO FROM TBBANCOS_CUENTAS C WHERE C.CBAN_CODIGO=PLAN_CBANCO_CODIGO)) AS NOMBRE_BANCO, " _
            & " (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA='TBOPC018' AND ELEMEN_CODIGO=PLAN_CODIGO_IMPUESTO) AS IMPUESTON " _
            & " FROM TBPCGR_" & Session("CodEmpresa") & " P WHERE (PLAN_AÑO = '" & cboAño.SelectedValue & "') AND (PLAN_SYS_EST = '0') " _
            & " ORDER BY PLAN_CUENTA"
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Return Dt
    End Function
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        lblIngresar2.Visible = True
        lblIngresar3.Visible = True
        lblIngresar.Visible = True
        lblEtiqueta.Text = "Nueva Cuenta"
        txtCta.Visible = True
        txtNivel.Visible = True
        lbl40.Visible = True
        lbl41.Visible = True
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        lblIngresar2.Visible = False
    End Sub
End Class
