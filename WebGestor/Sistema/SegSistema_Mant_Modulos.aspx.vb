Imports WebGestor
Imports System.Data
Imports system.Data.SqlClient
Partial Class SegSistema_Mant_Modulos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Lista_Modulo()
        End If
    End Sub
    Private Sub Lista_Modulo()
        Flex.DataSource = Mostrar_Modulos()
        Flex.DataBind()
    End Sub
    Function Mostrar_Modulos() As DataTable
        Mostrar_Modulos = Nothing
        Dim objSeg As New ModuloSeguridad
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dRow As DataRow
        Dim dtListado As New DataTable
        Try
            dtListado.Columns.Add("MODINTEG_NOMBRE")
            dtListado.Columns.Add("MOD_CODIGO")
            dtListado.Columns.Add("MOD_NOMBRE")
            dtListado.Columns.Add("MOD_ESTADO")
            dtListado.Columns.Add("MOD_DESCRIPCION")
            dt = objSeg.Lista_Modulo
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    dRow = dtListado.NewRow
                    dt2 = objSeg.Existe_RelacionModInteg(Nz(dr("MOD_CODIGO")))
                    If dt2.Rows.Count > 0 Then
                        For Each dr2 As DataRow In dt2.Rows
                            If Nu(dRow("MODINTEG_NOMBRE")) <> "" Then dRow("MODINTEG_NOMBRE") = dRow("MODINTEG_NOMBRE") & Chr(13)
                            dRow("MODINTEG_NOMBRE") = dRow("MODINTEG_NOMBRE") & Nu(dr2("MODINTEG_NOMBRE"))
                            If Nu(dr2("MODINTEG_CODIGO")) <> "" Then
                                dRow("MODINTEG_NOMBRE") = dRow("MODINTEG_NOMBRE") + " (" + IIf(dr2("MODINTEG_INSTALADO") = "S", "Sí Inst.", "No Inst.") + ")"
                            End If
                        Next
                    End If
                    dt2 = Nothing
                    dRow("MOD_CODIGO") = Nu(dr("MOD_CODIGO"))
                    dRow("MOD_NOMBRE") = Nu(dr("MOD_NOMBRE"))
                    dRow("MOD_ESTADO") = IIf(Nu(dr("MOD_ESTADO")) = "S", "Sí", "No")
                    dRow("MOD_DESCRIPCION") = Nu(dr("MOD_DESCRIPCION"))
                    dtListado.Rows.Add(dRow)
                Next
            End If
            dt = Nothing
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
        Mostrar_Modulos = dtListado
    End Function
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Lista_Modulo()
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Flex.Enabled = False
        btnNuevo.Enabled = False
        lblModuloIntegracion.Visible = True
        lblMEtiqueta.Text = "Agregar Módulo"
        txtdescripcion.Text = "" : txtnombre.Text = ""
        lblcodigo.Text = Cod_Modulo() : lblError.Text = ""
        Call Carga_Modulo_Integracion()
    End Sub
    Private Sub Carga_Modulo_Integracion()
        Try
            Dim objseg As New ModuloSeguridad
            lstModulosInteg.DataSource = Nothing
            lstModulosInteg.DataBind()
            lstModulosInteg.DataSource = objseg.Lista_ModuloIntegracion("1", 0)
            lstModulosInteg.DataTextField = "MODINTEG_NOMBRE"
            lstModulosInteg.DataValueField = "MODINTEG_CODIGO"
            lstModulosInteg.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Try
            Dim i As Integer = 0
            Dim objSeg As New ModuloSeguridad
            Dim CodModulo As Double = 0
            If txtnombre.Text.Trim = "" Then lblError.Text = "Debe de tener un nombre" : Exit Sub
            If OptSN.SelectedIndex <> 0 And OptSN.SelectedIndex <> 1 Then lblError.Text = "Debe de tener un estado" : Exit Sub
            CodModulo = lblcodigo.Text.Trim
            If lblMEtiqueta.Text = "Agregar Módulo" Then
                objSeg.InsUpd_Modulo(CodModulo, txtnombre.Text.Trim, txtdescripcion.Text.Trim, IIf(OptSN.SelectedIndex = 0, "S", "N"), "", "1")
            ElseIf lblMEtiqueta.Text = "Editar Módulo" Then
                objSeg.InsUpd_Modulo(CodModulo, txtnombre.Text.Trim, txtdescripcion.Text.Trim, IIf(OptSN.SelectedIndex = 0, "S", "N"), "", "2")
            End If
            objSeg.InsUpd_ModInteg(CodModulo, 0, "2")
            For i = 0 To lstModulosInteg.Items.Count - 1
                If lstModulosInteg.Items(i).Selected = True Then
                    objSeg.InsUpd_ModInteg(CodModulo, lstModulosInteg.Items(i).Value, "1")
                End If
            Next
            Call Lista_Modulo()
            btnCancelar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        lblModuloIntegracion.Visible = False
        lblMEtiqueta.Text = "" : txtdescripcion.Text = ""
        txtnombre.Text = "" : lblcodigo.Text = "" : lblError.Text = ""
        lstModulosInteg.DataSource = Nothing
        lstModulosInteg.DataBind()
        Flex.Enabled = True
        btnNuevo.Enabled = True
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Try
            lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Editar" Then
                Flex.Enabled = False
                btnNuevo.Enabled = False
                lblModuloIntegracion.Visible = True
                lblMEtiqueta.Text = "Editar Módulo"
                lblcodigo.Text = Flex.Rows(Index).Cells(2).Text
                CodModulo = Flex.Rows(Index).Cells(2).Text
                txtnombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtdescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") = "Sí" Then OptSN.SelectedIndex = 0 Else OptSN.SelectedIndex = 1
                Call Carga_Modulo_Integracion()
                Call Cargar_ModInteg(CodModulo)
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Private Sub Cargar_ModInteg(ByVal dCodModulo As Double)
        Try
            lblError.Text = ""
            Dim i As Integer = 0
            Dim objSeg As New ModuloSeguridad
            Dim dt As New DataTable
            dt = objSeg.Existe_ModInteg(dCodModulo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    For i = 0 To lstModulosInteg.Items.Count - 1
                        If lstModulosInteg.Items(i).Value = dr("MODINTEG_CODIGO") Then lstModulosInteg.Items(i).Selected = True
                    Next
                Next
            End If
            dt = Nothing
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Function Cod_Modulo() As String
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Ruta_Ng)
        Dim cmdSql As New SqlCommand
        Dim obj As New Listados
        Dim dt As New Data.DataTable
        Dim sUrl As String = MyBase.Request.FilePath
        Try
            Cn2.Open()
            cmdSql.Connection = Cn2
            cmdSql.CommandText = "SELECT MAX(MOD_CODIGO) FROM TBSISTEMA_MODULOS"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    Cod_Modulo = Nz(Rs(0)) + 1
                End While
            Else
                Cod_Modulo = 1
            End If
            Rs.Close()
        Catch Ex As SqlException
            lblError.Text = Ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
            Cn2.Close()
        End Try
    End Function
End Class
