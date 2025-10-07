Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress
Imports DevExpress.Web
Partial Class CRM_CRM_DxLista_CuadroMando
    Inherits System.Web.UI.Page
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
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Private Sub CRM_CRM_DxLista_CuadroMando_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtPadreID As DataTable
        dtPadreID = TablaMenu()

        Dim dtHijos As DataTable

        For Each drow As DataRow In dtPadreID.Rows
            Dim itemData As String = drow("descripcion").ToString
            Dim mainItem = ASPxMenu2.Items.Add(itemData)
            mainItem.NavigateUrl = drow("PAGINA_ASPX").ToString
            Dim psNivelI As Integer = CDbl(drow("MENWEB_NIVEL").ToString) + 1
            Dim psNivel As String = psNivelI
            dtHijos = TablaSubMenu(drow("CODIGO").ToString, psNivel)
            For Each drowH As DataRow In dtHijos.Rows
                Dim itemDataSub As String = drowH("descripcion").ToString
                Dim mainSubItem = mainItem.Items.Add(itemDataSub)
                mainSubItem.NavigateUrl = drowH("PAGINA_ASPX").ToString
            Next
        Next
    End Sub
End Class
