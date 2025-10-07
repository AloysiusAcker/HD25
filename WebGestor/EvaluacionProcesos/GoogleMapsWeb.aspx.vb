Imports System
Imports System.Globalization

Partial Class EvaluacionProcesos_GoogleMapsWeb
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Public Function obtenerLatLng_PorDir(ByVal Direccion As String) As String
        Dim HTML As String = String.Empty
        Dim resaux As String = String.Empty
        HTML = obtenerHTMLdeunaURl("https://www.google.com.ar/maps/place/" & Direccion)

        If HTML IsNot Nothing Then
            HTML = Regex.Split(HTML, "viewport:{center:{").ToList()(1)
            resaux = HTML.Substring(0, HTML.IndexOf("},span:{lat:"))
            resaux = Regex.Replace(resaux, "lat:", "")
            resaux = Regex.Replace(resaux, "lng:", "")
        End If

        Return resaux
    End Function

End Class
