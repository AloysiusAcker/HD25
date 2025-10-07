<%@ WebHandler Language="VB" Class="Foto_" %>

Imports System
Imports System.Web
Imports WebGestor

Public Class Foto_ : Implements IHttpHandler, IRequiresSessionState

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim tmp As Byte() = Nothing
        Dim cod As String = context.Request("cod").ToString()
        Dim oLst = CType(context.Session("Lista_PersonaBE"), List(Of PersonaBE))
        Dim reg = oLst.FirstOrDefault(Function(n) n.PERSON_C_CODIGO = cod)
        If Not reg Is Nothing Then
            If Not reg.PERSON_I_FOTO Is Nothing Then
                tmp = reg.PERSON_I_FOTO
            End If
        End If
        If Not tmp Is Nothing Then
            context.Response.ContentType = "image/jpeg"
            Dim theStream As IO.Stream = New IO.MemoryStream(tmp)
            theStream.Position = 0
            Dim bCant As Integer = 8192
            Dim buffer(bCant) As Byte
            Dim calc As Long = theStream.Length / bCant
            If theStream.Length Mod bCant > 0 Then
                calc = calc + 1
            End If
            Dim len As Integer
            For i As Long = 1 To calc
                len = theStream.Read(buffer, 0, buffer.Length)
                context.Response.OutputStream.Write(buffer, 0, len)
            Next
        Else
            context.Response.ContentType = "image/jpeg"
            context.Response.WriteFile("Fotos/persona.jpg")
        End If
    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
End Class