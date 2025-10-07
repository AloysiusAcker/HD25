Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Menu_PagMenu
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim obj As New Listados
        Dim dt As New Data.DataTable
        Dim nPag As String = ""
        Dim nTPag As String = ""
        Dim Existe As Integer = 0
        If Not Page.IsPostBack Then
            Dim sUrl As String = MyBase.Request.FilePath
            Dim sPag As String = Mid(sUrl, sUrl.LastIndexOf("/") + 2)

            If sUrl <> HttpContext.Current.Request.ApplicationPath & "/Menu/_Default.aspx" And _
               sUrl <> HttpContext.Current.Request.ApplicationPath & "/Menu/Detalle.aspx" And _
               sUrl <> HttpContext.Current.Request.ApplicationPath & "/Menu/IngresarElemento.aspx" Then
                Session("MenuCod") = ""
                Session("MenuNom") = ""
                Session("MenuCodElement") = ""
            End If
            lblFecha.InnerText = Format(CDate(FormatoFecha(FechaActual())), "dddd, dd 'de' MMMM 'de' yyyy")
            If Session("UserFirmado") = "N" Or Session("UserFirmado") Is Nothing Then
                Hyperlink1.Visible = True
                Hyperlink3.Visible = False
            Else
                Hyperlink3.Visible = True
                Hyperlink1.Visible = True
                lblAgrup.InnerText = IIf(Session("NombreGrupoEmpresa") <> "", Session("NombreGrupoEmpresa") & " - " & Session("NombreEmpresa"), "")
            End If 'Establece el Título de la página a cargar

            ' Establecer los estilos 
            Hyperlink1.CssClass = IIf(Hyperlink1.PostBackUrl = sPag, "MenuSelected", "MenuUnselected")
            Hyperlink2.CssClass = IIf(Hyperlink2.PostBackUrl = sPag, "MenuSelected", "MenuUnselected")
        End If
    End Sub
    
End Class