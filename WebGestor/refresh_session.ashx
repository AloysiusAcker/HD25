<%@ WebHandler Language="VB" CodeBehind="refresh_session.ashx.vb" Class="WebGestor.refresh_session" %>



Imports System.Web
Imports System.Web.Services

Public Class refresh_session
    Implements System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim usr As String = Convert.ToString(context.Session("Ruta_Emp"))
        context.Response.Clear()
        context.Response.ClearHeaders()
        context.Response.ClearContent()
        context.Response.ContentType = "text/javascript"
        context.Response.Write("alert('" & usr & "');")
        context.Response.Flush()
        context.Response.End()
    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class