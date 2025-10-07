Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Menu_MenuPrincipal
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call Carga_Detalle_Menu()
        End If
    End Sub
    Public Function Carga_Menu() As DataTable
        Carga_Menu = Nothing
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim Da As SqlDataAdapter
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT ME.ITEM_CODIGO, M.ITEM_NOMBRE  FROM  TBMENU_ITEMS_EMPRESA ME INNER JOIN TBMENU_ITEMS M ON ME.ITEM_CODIGO = M.ITEM_CODIGO" _
                                   & " WHERE (ME.ITEM_SYS_EST = '0') AND (M.ITEM_SYS_EST = '0') AND (ME.GRPOEMPRESA_CODIGO = " & Session("CodGrupoEmpresa") & ") AND (ME.EMPRESA_CODIGO =  '" & Session("CodEmpresa") & "')  ORDER BY M.ITEM_ORDEN"
            Da = New SqlDataAdapter(CmdGlobal)
            Da.Fill(dt)
            Return dt
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Private Sub Carga_Detalle_Menu()
        For Each drMenuItem As Data.DataRow In Carga_Menu.Rows
            Dim mnuMenuItem As New MenuItem
            mnuMenuItem.Value = drMenuItem("ITEM_CODIGO").ToString
            mnuMenuItem.Text = drMenuItem("ITEM_NOMBRE").ToString
            mnuMenuItem.ImageUrl = "~/Fotos/16 (Back).ico"
            MenuPrincipal.Items.Add(mnuMenuItem)
            'End If
        Next
    End Sub
    Protected Sub MenuPrincipal_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles MenuPrincipal.MenuItemClick
        Dim obj As New Listados
        Dim obj2 As New Listados
        Dim dt As New Data.DataTable
        Dim nPag As String = ""
        Dim nTPag As String = ""
        Dim Existe As Integer = 0
        If e.CommandName = "" Then
            Session("MenuCod") = MenuPrincipal.SelectedValue
            Session("MenuNom") = MenuPrincipal.SelectedItem.Text
            Dim sUrl As String = MyBase.Request.FilePath
            dt = obj.Listar_Pagina(Session("CodEmpresa"), Session("CodGrupoEmpresa"), Session("MenuCod"), "S")
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    nTPag = HttpContext.Current.Request.ApplicationPath & "/Menu/" & Nu(drMenuItem("ITEM_PAGINA"))
                    If sUrl = nTPag Then Existe = Existe + 1
                Next
            End If
            dt = Nothing 'HttpContext.Current.Request.ApplicationPath & "/Menu/_Default.aspx" Or sUrl = HttpContext.Current.Request.ApplicationPath & "/Menu/Detalle.aspx"
            If sUrl = HttpContext.Current.Request.ApplicationPath & "/Menu/Detalle.aspx" Then Existe = Existe + 1
            If Existe > 0 Then
                dt = obj.Listar_Pagina(Session("CodEmpresa"), Session("CodGrupoEmpresa"), Session("MenuCod"), "N")
                If dt.Rows.Count = 1 Then
                    For Each drMenuItem As Data.DataRow In dt.Rows
                        nPag = Nu(drMenuItem("ITEM_PAGINA"))
                    Next
                End If
                dt = Nothing
                Response.Redirect(nPag)
            Else
                dt = obj.Listar_Pagina(Session("CodEmpresa"), Session("CodGrupoEmpresa"), Session("MenuCod"), "N")
                If dt.Rows.Count = 1 Then
                    For Each drMenuItem As Data.DataRow In dt.Rows
                        nPag = "Menu/" & Nu(drMenuItem("ITEM_PAGINA"))
                    Next
                End If
                dt = Nothing
                Response.Redirect(nPag)
            End If
        End If
    End Sub
End Class
