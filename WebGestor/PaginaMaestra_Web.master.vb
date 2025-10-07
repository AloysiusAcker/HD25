Imports WebGestor
Imports System.Data
Partial Class PaginaMaestra_Web
    Inherits System.Web.UI.MasterPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblAgrup.InnerText = NomEmpresa

    End Sub
End Class

