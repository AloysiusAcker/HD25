<%@ WebHandler Language="VB" Class="fotoUpload" %>

Imports System
Imports System.Web

Public Class fotoUpload : Implements IHttpHandler, IRequiresSessionState

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim vDNI = context.Request.Form("hndDNI")

        context.Response.ContentType = "text/plain"
        Dim file As HttpPostedFile = context.Request.Files(0)
        If Not file Is Nothing And file.ContentLength > 0 Then
            Dim fname As String = vDNI.Trim() + ".jpg"
            Dim arch As String = context.Server.MapPath(IO.Path.Combine("~/Inventario/GUIAS/", fname))
            file.SaveAs(arch)
            context.Session("vImgPrev") = arch
            context.Response.Write(fname)
        End If


    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class

