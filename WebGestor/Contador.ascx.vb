
Partial Class Contador
    Inherits System.Web.UI.UserControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbl.Text = Application("Contador") 'Session("contador") 'Application("Contador")
    End Sub
    Public Property Valor() As String
        Get
            Return lbl.Text
        End Get
        Set(ByVal value As String)
            lbl.Text = Application("Contador")
        End Set
    End Property
End Class
