Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class CMenuLeft
    Inherits System.Web.UI.UserControl

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            Call Carga_Detalle_Menu()
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Public Function Carga_Menu1() As DataTable
        Carga_Menu1 = Nothing
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim Da As SqlDataAdapter
        Dim Perfiles As String
        Dim ST As String = Ruta_Ng
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = "SELECT DISTINCT UP.USUPER_CODUSU, UP.PERFIL_CODUNICO, P.PERFIL_CODIGO " _
                    & " FROM TBUSUPER UP INNER JOIN TBPERFIL P ON UP.PERFIL_CODUNICO = P.PERFIL_CODUNICO " _
                    & " WHERE (UP.USUPER_CODUSU = '" & HttpContext.Current.User.Identity.Name & "') AND " _
                    & " (UP.USUPER_SYS_EST = '0') AND (P.PERFIL_SYS_EST = '0') AND (P.PERFIL_CODIGO = '099' OR P.PERFIL_CODIGO = '098') "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then 'saber si tiene el perfil 099
                Rs.Close()
                CmdGlobal.CommandText = " SELECT DISTINCT UP.USUPER_CODUSU,P.PAG_DESCRIPCION,P.PAG_NOMBRE AS PAGINA_ASP,P.PAG_CODIGO " _
                                      & " FROM dbo.TBUSUPER AS UP INNER JOIN dbo.TBRELACION_PERFILPAG AS RP INNER JOIN " _
                                      & " dbo.TBPAGINAS AS P ON RP.PAG_CODIGO = P.PAG_CODIGO ON UP.PERFIL_CODUNICO = RP.PERFIL_CODUNICO " _
                                      & " WHERE (P.PAG_SYS_EST='0') AND (P.PAG_ESTADO='0') AND (BARRA_SYS_EST='0') AND (UP.USUPER_CODUSU =  '" & HttpContext.Current.User.Identity.Name & "')"
                Da = New SqlDataAdapter(CmdGlobal)
                Da.Fill(dt)
            Else
                Rs.Close()
                CmdGlobal.CommandText = "SELECT UP.PERFIL_CODUNICO FROM TBUSUPER UP INNER JOIN TBPERFIL P ON UP.PERFIL_CODUNICO = P.PERFIL_CODUNICO " _
                        & " WHERE (UP.USUPER_CODUSU = '" & HttpContext.Current.User.Identity.Name & "') AND (P.PERFIL_SYS_EST = '0') AND  (UP.USUPER_SYS_EST = '0')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    Perfiles = ""
                    While Rs.Read
                        If Perfiles <> "" Then Perfiles = Perfiles & " OR "
                        Perfiles = Perfiles & "BARRA.PERFIL_CODUNICO=" & Nu(Rs!PERFIL_CODUNICO)
                    End While
                    Rs.Close()
                    CmdGlobal.CommandText = " SELECT DISTINCT UP.USUPER_CODUSU,  P.PAG_DESCRIPCION , P.PAG_NOMBRE AS PAGINA_ASP, P.PAG_CODIGO" _
                                      & " FROM dbo.TBUSUPER AS UP INNER JOIN dbo.TBRELACION_PERFILPAG AS BARRA INNER JOIN " _
                                      & " dbo.TBPAGINAS AS P ON BARRA.PAG_CODIGO = P.PAG_CODIGO ON UP.PERFIL_CODUNICO = BARRA.PERFIL_CODUNICO " _
                                      & " WHERE (P.PAG_SYS_EST='0') AND (P.PAG_ESTADO='0')  AND (BARRA.BARRA_SYS_EST = '0') AND (" & Perfiles & ")  AND (UP.USUPER_CODUSU =  '" & HttpContext.Current.User.Identity.Name & "') ORDER BY PAG_DESCRIPCION"
                    Da = New SqlDataAdapter(CmdGlobal)
                    Da.Fill(dt)
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
        'If Carga_Menu1.Rows.Count > 0 Then
        '    Dim sb As New StringBuilder()

        '    For Each drMenuItem As DataRow In Carga_Menu1.Rows
        '        Dim menuItemValue As String = drMenuItem("PAG_CODIGO").ToString()
        '        Dim menuItemText As String = drMenuItem("PAG_DESCRIPCION").ToString()
        '        Dim menuItemUrl As String = drMenuItem("PAGINA_ASP").ToString()

        '        'Agrega cada elemento del menú al StringBuilder
        '        sb.AppendFormat("<li><a href='{0}'>{1}</a></li>", menuItemUrl, menuItemText)

        '        'Puedes descomentar las líneas siguientes si deseas manejar submenús
        '        'Dim subMenuItems As DataTable = ObtenerSubMenuItems(menuItemValue)
        '        'AgregaSubMenuItems(sb, subMenuItems)
        '    Next


        '    'Obtén la referencia al contenedor <ul> y agrega el contenido generado
        '    If customMenu IsNot Nothing Then
        '        customMenu.InnerHtml = sb.ToString()
        '    End If
        'End If

        If Carga_Menu1.Rows.Count > 0 Then
            For Each drMenuItem As Data.DataRow In Carga_Menu1.Rows
                Dim mnuMenuItem2 As New MenuItem
                'mnuMenuItem2.Value = drMenuItem("MENWEB_CODIGO").ToString
                'mnuMenuItem2.Text = drMenuItem("MENWEB_DESCRIPCION").ToString
                mnuMenuItem2.Value = drMenuItem("PAG_CODIGO").ToString
                mnuMenuItem2.Text = drMenuItem("PAG_DESCRIPCION").ToString
                mnuMenuItem2.NavigateUrl = drMenuItem("PAGINA_ASP").ToString()
                'mnuMenuItem2.ImageUrl = "~/Fotos/16 (Back).ico"
                Menu2.Items.Add(mnuMenuItem2)
                'Call Carga_MenuHijos(mnuMenuItem2, drMenuItem("MENWEB_CODIGO").ToString)
                'End If
            Next
        End If

    End Sub ' Agrega cada elemento del menú al StringBuilder

    Private Sub Carga_MenuHijos(ByRef mnuMenuItem2 As MenuItem, ByVal psMenuCod As String)
        If Tabla_MenuHijos(psMenuCod).Rows.Count > 0 Then
            For Each drMenuItem As Data.DataRow In Tabla_MenuHijos(psMenuCod).Rows
                Dim mnuMenuItem3 As New MenuItem
                'If Existe_Pagina(drMenu Item("PAGINA_ASP").ToString()) = True Then
                mnuMenuItem3.Value = drMenuItem("CODIGO").ToString
                mnuMenuItem3.Text = drMenuItem("DESCRIPCION").ToString
                'mnuMenuItem3.ImageUrl = "~/Fotos/16 (Back).ico"
                mnuMenuItem3.NavigateUrl = drMenuItem("PAGINA_ASP").ToString()
                mnuMenuItem2.ChildItems.Add(mnuMenuItem3)
                'Menu2.Items.Add(mnuMenuItem3)
                'End If
            Next
        End If
    End Sub
    Function Tabla_MenuHijos(ByVal psMenuCod As String) As DataTable
        Tabla_MenuHijos = Nothing
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
            CmdGlobal.CommandText = "SELECT UP.PERFIL_CODUNICO FROM TBUSUPER UP INNER JOIN TBPERFIL P ON UP.PERFIL_CODUNICO = P.PERFIL_CODUNICO " _
                                    & " WHERE (UP.USUPER_CODUSU = '" & HttpContext.Current.User.Identity.Name & "') AND (P.PERFIL_SYS_EST = '0') AND  (UP.USUPER_SYS_EST = '0')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                Perfiles = ""
                While Rs.Read
                    If Perfiles <> "" Then Perfiles = Perfiles & " OR "
                    Perfiles = Perfiles & "BARRA.PERFIL_CODUNICO=" & Nu(Rs!PERFIL_CODUNICO)
                End While
                Rs.Close()
                CmdGlobal.CommandText = " SELECT DISTINCT UP.USUPER_CODUSU, P.PAG_DESCRIPCION, P.PAG_NOMBRE AS PAGINA_ASP, P.PAG_CODIGO, BARRA.PERFIL_CODUNICO, " _
                                      & " MW.MENWEB_DESCRIPCION AS DESCRIPCION, MW.MENWEB_CODIGO AS CODIGO, MW.MENWEB_CODIGO_N1, MW.MENWEB_NIVEL " _
                                      & " FROM dbo.TBUSUPER AS UP INNER JOIN dbo.TBRELACION_PERFILPAG AS BARRA INNER JOIN dbo.TBPAGINAS AS P " _
                                      & " ON BARRA.PAG_CODIGO = P.PAG_CODIGO ON UP.PERFIL_CODUNICO = BARRA.PERFIL_CODUNICO INNER JOIN " _
                                      & " dbo.TBSISTEMA_MENU_WEB AS MW ON BARRA.PAG_CODIGO = MW.PAGINA_CODIGO " _
                                      & " WHERE (P.PAG_SYS_EST = '0') AND (P.PAG_ESTADO = '0') AND (BARRA.BARRA_SYS_EST = '0') " _
                                      & " AND (UP.USUPER_CODUSU = '" & HttpContext.Current.User.Identity.Name & "') AND (" & Perfiles & ")  AND " _
                                      & " (MW.MENWEB_NIVEL = 2) AND (MENWEB_CODIGO_N1 = " & psMenuCod & " ) ORDER BY PAG_DESCRIPCION "
                Da = New SqlDataAdapter(CmdGlobal)
                Da.Fill(dt)
            End If
            Return dt
        Catch Ex As SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Function


End Class

