Imports System.IO
Partial Class CRM_CRM_Exportar
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Genera el archivo Excel

        Dim valor As String = Request.QueryString("parametro2") 'proveedor

        ' Abrir el archivo para descargarlo
        If File.Exists(valor) Then
            Response.ContentType = "application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & Path.GetFileName(valor))
            Response.TransmitFile(valor)
            Response.End()
        End If


    End Sub
End Class
