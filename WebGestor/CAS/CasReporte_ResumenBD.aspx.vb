
Partial Class CasReporte_ResumenBD
    Inherits System.Web.UI.Page

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("Cas_Relacion_Reportes.aspx")
    End Sub
End Class
