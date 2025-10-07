Imports System.IO
Partial Class Inventario_Inventario_Descargas
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Genera el archivo Excel


        Dim savePath As String = Server.MapPath("~/Inventario/GuiaInterna/")
        Dim fileName As String = "GuiaInterna_Nro_" & Session("CodGuia") & ".pdf" ' "Informe_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".pdf"
        Dim fullPath As String = Path.Combine(savePath, fileName)

        ' Descargar el PDF generado
        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.TransmitFile(fullPath)
        Response.End()

    End Sub
End Class
