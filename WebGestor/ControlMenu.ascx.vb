Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Data
Imports WebGestor
Partial Class ControlMenu
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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call Carga_Detalle_Menu()
        End If
    End Sub
    Public Function Carga_Menu() As DataTable
        Carga_Menu = Nothing
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim Da As SqlDataAdapter
        Dim Perfiles As String
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT DISTINCT UP.USUPER_CODUSU, UP.PERFIL_CODUNICO, P.PERFIL_CODIGO" _
                        & " FROM TBUSUPER UP INNER JOIN TBPERFIL P ON UP.PERFIL_CODUNICO = P.PERFIL_CODUNICO " _
                        & " WHERE (UP.USUPER_CODUSU = '" & HttpContext.Current.User.Identity.Name & "') AND (UP.USUPER_SYS_EST = '0') AND (P.PERFIL_SYS_EST = '0') AND (P.PERFIL_CODIGO = '099' OR P.PERFIL_CODIGO = '098')  AND (P.GRPOEMPRESA_CODIGO =  " & Session("CodGrupoEmpresa") & ") AND (P.EMPRESA_CODIGO =  '" & Session("CodEmpresa") & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then 'saber si tiene el perfil 099
                Rs.Close()


                CmdGlobal.CommandText = " SELECT DISTINCT B.BOTON_CODIGO, B.BOTON_DESCRIPCION AS DESCRIPCION,B.BOTON_CODIGO,F.FORM_NOMBRE+'.aspx' AS PAGINA_ASP  " _
                         & " FROM TBDESCRIP_BOTON B INNER JOIN TBSISTEMA_FORMS F ON B.BOTON_COD_FORM = F.FORM_CODIGO" _
                         & " INNER JOIN TBSISTEMA_MODULOS_MODINTEG MM ON F.FORM_COD_MODULO = MM.MOD_CODIGO" _
                         & " INNER JOIN BDGrupoEmpresas.dbo.TBGRUPOEMPRESAS_MODINTEG GEM ON MM.MODINTEG_CODIGO = GEM.MODINTEG_CODIGO" _
                         & " INNER JOIN TBUSUARI_GRPOEMPS UGE ON GEM.GE_CODIGO = UGE.GRPOEMPRESA_CODIGO" _
                         & " WHERE (F.FORM_SYS_EST = '0') AND (B.BOTON_SYS_EST = '0') AND (UGE.USUARI_CODIGO = '" & HttpContext.Current.User.Identity.Name & "')  ORDER BY B.BOTON_CODIGO"
                Da = New SqlDataAdapter(CmdGlobal)
                Da.Fill(dt)
            Else
                Rs.Close()
                CmdGlobal.CommandText = "SELECT UP.PERFIL_CODUNICO FROM TBUSUPER UP INNER JOIN TBPERFIL P ON UP.PERFIL_CODUNICO = P.PERFIL_CODUNICO " _
                        & " WHERE (UP.USUPER_CODUSU = '11119999') AND (P.PERFIL_SYS_EST = '0') AND  (UP.USUPER_SYS_EST = '0')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    Perfiles = ""
                    While Rs.Read
                        If Perfiles <> "" Then Perfiles = Perfiles & " OR "
                        Perfiles = Perfiles & "BARRA.PERFIL_CODUNICO=" & Nu(Rs!PERFIL_CODUNICO)
                    End While
                    Rs.Close()
                    'CmdGlobal.CommandText = " SELECT UP.USUPER_CODUSU,  P.PAG_DESCRIPCION , P.PAG_NOMBRE AS PAGINA_ASP, P.PAG_CODIGO" _
                    '                  & " FROM dbo.TBUSUPER AS UP INNER JOIN dbo.TBRELACION_PERFILPAG AS BARRA INNER JOIN " _
                    '                  & " dbo.TBPAGINAS AS P ON BARRA.PAG_CODIGO = P.PAG_CODIGO ON UP.PERFIL_CODUNICO = BARRA.PERFIL_CODUNICO " _
                    '                  & " WHERE (P.PAG_SYS_EST='0') AND (P.PAG_ESTADO='0')  AND (BARRA.BARRA_SYS_EST = '0') AND (" & Perfiles & ")  ORDER BY PAG_DESCRIPCION"
                    'Da = New SqlDataAdapter(CmdGlobal)
                    CmdGlobal.CommandText = "SELECT DISTINCT B.BOTON_CODIGO,B.BOTON_DESCRIPCION AS DESCRIPCION, F.FORM_CODIGO, F.FORM_NOMBRE+'.aspx' AS PAGINA_ASP, BARRA.PERFIL_CODUNICO" _
                      & " FROM TBDESCRIP_BOTON B INNER JOIN TBSISTEMA_FORMS F ON B.BOTON_COD_FORM = F.FORM_CODIGO INNER JOIN  TBBARRAS_PERFILES BARRA ON B.BOTON_CODIGO = BARRA.BP_BOTON " _
                      & " WHERE (B.BOTON_SYS_EST = '0') AND (F.FORM_SYS_EST = '0') AND (BARRA.BP_SYS_EST = '0') AND (" & Perfiles & ")  ORDER BY DESCRIPCION"
                    Da = New SqlDataAdapter(CmdGlobal)
                    Da.Fill(dt)
                Else
                    'MsgBox("No tiene Páginas Definidas.")
                    'Response.Redirect("Default.aspx")
                End If
            End If
            Return dt
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function
    Private Sub Carga_Detalle_Menu()
        Dim mnuMenuItem As New MenuItem
        For Each drMenuItem As Data.DataRow In Carga_Menu.Rows
            If Existe_Pagina(("PAGINA_ASP").ToString()) = True Then
                mnuMenuItem.Value = drMenuItem("BOTON_CODIGO").ToString
                mnuMenuItem.Text = drMenuItem("DESCRIPCION").ToString
                mnuMenuItem.ImageUrl = "~/Fotos/16 (Back).ico"
                mnuMenuItem.NavigateUrl = drMenuItem("PAGINA_ASP").ToString()
                Menu1.Items.Add(mnuMenuItem)
            End If
        Next
        'If i = 1 Then MyDataRow(0) = "Pagina Principal" : MyDataRow(1) = "Inicio_Encuestas.aspx"
        'If i = 2 Then MyDataRow(0) = "Encuestas" : MyDataRow(1) = "encuestas.aspx"
        'If i = 3 Then MyDataRow(0) = "Realizadas" : MyDataRow(1) = "Encuestas_Realizadas.aspx"
        'If i = 4 Then MyDataRow(0) = "Anónimos" : MyDataRow(1) = "encuestas_anonimos.aspx"
        'mnuMenuItem.Value = 3
        'mnuMenuItem.Text = "Encuestas Realizadas"
        'mnuMenuItem.ImageUrl = "~/Fotos/16 (Back).ico"
        'mnuMenuItem.NavigateUrl = "Encuestas_Realizadas.aspx"
        'mnuMenuItem.Enabled = False
        'Menu1.Items.Add(mnuMenuItem)
        'mnuMenuItem.Value = 4
        'mnuMenuItem.Text = "Encuestas Anónimos"
        'mnuMenuItem.ImageUrl = "~/Fotos/16 (Back).ico"
        'mnuMenuItem.NavigateUrl = "Encuestas_Anonimos.aspx"
        'mnuMenuItem.Enabled = True
        'Menu1.Items.Add(mnuMenuItem)
        'If Session("UserFirmado") = "S" Then
        '    mnuMenuItem.Enabled = True
        'End If
    End Sub
End Class
