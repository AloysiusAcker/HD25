
Partial Class CasReporte_IncidentexComponente
    Inherits System.Web.UI.Page

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("Cas_Relacion_Reportes.aspx")
    End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    '    'CrystalReportViewer1.RefreshReport()
    'End Sub
End Class
