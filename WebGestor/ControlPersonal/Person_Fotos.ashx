<%@ WebHandler Language="VB" Class="Person_Fotos" %>

Imports System
Imports System.Web
Imports WebGestor
Imports System.Data.SqlClient

Public Class Person_Fotos : Implements IHttpHandler


    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim usuarioId As String = context.Request.QueryString("id")

        If String.IsNullOrEmpty(usuarioId) Then
            context.Response.StatusCode = 400 ' Bad Request
            context.Response.Write("ID de usuario requerido")
            Return
        End If

        If Not String.IsNullOrEmpty(usuarioId) Then
            Using connection As New SqlConnection(Ruta_GrEmp)
                Using command As New SqlCommand("SELECT PERSON_IMAGEN FROM TBPERSONAL WHERE PERSON_CODIGO = @Id", connection)
                    command.Parameters.AddWithValue("@Id", usuarioId)
                    connection.Open()

                    Dim imageBytes As Byte() = Nothing
                    Dim result As Object = command.ExecuteScalar()

                    If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
                        imageBytes = DirectCast(result, Byte())
                        context.Response.ContentType = "image/jpeg"
                        context.Response.BinaryWrite(imageBytes)
                    End If

                End Using
            End Using
        End If


    End Sub

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class