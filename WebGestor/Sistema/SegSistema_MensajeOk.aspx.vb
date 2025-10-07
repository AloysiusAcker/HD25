Partial Class Sistema_SegSistema_MensajeOk
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Introducir aquí el código de usuario para inicializar la página
        lblMensaje.Text = Session("Mensaje")
        If Session("PageMensaje") = "3" Then
            HyperLink1.Visible = True
        ElseIf Session("PageMensaje") = "4" Then
            HyperLink1.Text = "Haga clic aquí para iniciar su sesión..."
            HyperLink1.Visible = True
        Else
            HyperLink1.Visible = False
        End If
    End Sub
End Class
