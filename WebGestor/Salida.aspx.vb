Partial Class Salida
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strUrl As String = IIf(Request.Params("strUrl") Is Nothing, "./", Request.Params("strUrl"))
        Session("UserNombre") = ""
        Session("UserFirmado") = ""
        Session("Codigo") = ""
        Session("User") = ""
        Session("CodEmpresa") = ""
        Session("codGrupoEmpresa") = ""
        Session("SiglaGrupoEmpresa") = ""
        Session("NombreGrupoEmpresa") = ""
        Session("NombreEmpresa") = ""
        Session("Ruta_Emp") = ""
        Session("MenuCod") = ""
        Session("MenuNom") = ""
        Session("MenuCodElement") = ""
        Session.Clear()
        FormsAuthentication.SignOut()
        Response.Redirect("PaginaPrincipal.aspx")
    End Sub
End Class
