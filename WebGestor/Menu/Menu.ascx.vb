Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Data
Imports WebGestor
Partial Class Menu_Menu
    Inherits System.Web.UI.UserControl
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            Try
                Call Carga_Detalle_Menu()
                Me.Page.Session.Timeout = 1080
            Catch Ex As SqlException
                'lblMensaje.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & ex.Message
            Catch Ex As Exception
                'lblMensaje.Text = "Ha ocurrido un error la Aplicacion:<br>" & ex.Message
            Finally
                'Cn.Close()
            End Try
        End If
    End Sub
    Private Sub Carga_Detalle_Menu()
        If Carga_Menu1.Rows.Count > 0 Then
            For Each drMenuItem As Data.DataRow In Carga_Menu1.Rows
                Dim mnuMenuItem2 As New MenuItem
                'If Existe_Pagina(drMenu Item("PAGINA_ASP").ToString()) = True Then
                mnuMenuItem2.Value = drMenuItem("ITEM_CODIGO").ToString
                mnuMenuItem2.Text = drMenuItem("ITEM_NOMBRE").ToString
                mnuMenuItem2.ImageUrl = "~/Fotos/16 (Back).ico"
                'mnuMenuItem2.NavigateUrl = drMenuItem("ITEM_PAGINA").ToString()
                MenuA.Items.Add(mnuMenuItem2)
                'End If
            Next
        End If
    End Sub
    Public Function Carga_Menu1() As DataTable
        Carga_Menu1 = Nothing
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim Da As SqlDataAdapter
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT ME.ITEM_CODIGO, M.ITEM_NOMBRE, M.ITEM_PAGINA  FROM  TBMENU_ITEMS_EMPRESA ME INNER JOIN TBMENU_ITEMS M ON ME.ITEM_CODIGO = M.ITEM_CODIGO" _
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
    Protected Sub MenuA_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles MenuA.MenuItemClick
        Dim obj As New Listados
        Dim dt As DataTable
        Dim pag As String = ""
        Dim selectedItem As MenuItem = MenuA.SelectedItem
        Session("MenuNom") = MenuA.SelectedItem.Text
        Session("MenuCod") = MenuA.SelectedValue.Trim
        dt = obj.Devolver_CodMenu(MenuA.SelectedValue.Trim)
        If dt.Rows.Count = 1 Then
            For Each dr As DataRow In dt.Rows
                pag = Nu(dr("ITEM_PAGINA"))
            Next
        End If
        dt = Nothing
        Response.Redirect("~/" & pag)
    End Sub
End Class

