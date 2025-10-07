<%@ WebHandler Language="VB" Class="Manejador" %>

Imports System.Web
Imports System.Data

'Namespace WebGestor
Public Class Manejador : Implements IHttpHandler, System.Web.SessionState.IRequiresSessionState
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If context.Session("Registro") IsNot Nothing Then
            Dim tbRegistro As DataTable = CType(context.Session("Registro"), DataTable)
            Dim drRegistro As DataRow = tbRegistro.[Select](String.Format("ART_CODIGO={0}", context.Request.QueryString("ART_CODIGO")))(0)
            Dim imagen As Byte() = CType(drRegistro("art_img"), Byte())
            context.Response.ContentType = "image/jpg"
            context.Response.OutputStream.Write(imagen, 0, imagen.Length)
        End If
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
'End Namespace