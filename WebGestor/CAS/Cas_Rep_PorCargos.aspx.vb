Imports System.Data
Imports System.Configuration
Imports WebCas
Imports System.Data.SqlClient

Partial Class Cas_Rep_PorCargos
    Inherits System.Web.UI.Page

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("Cas_Relacion_Estadisticas.aspx")
    End Sub
End Class
