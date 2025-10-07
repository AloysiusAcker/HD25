<%@ WebHandler Language="VB" Class="RecepcionHandler" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient

Public Class RecepcionHandler : Implements IHttpHandler

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim Ruta_conexion As String = context.Request.QueryString("Ruta")
        Dim id As String = context.Request.QueryString("id")

        If Not String.IsNullOrEmpty(id) Then
            Using connection As New SqlConnection(Ruta_conexion)
                Using command As New SqlCommand("SELECT RECEP_IMAGEN_OC FROM TBINV_ALMACEN_RECEPCION WHERE RECEP_CODIGO = @Id", connection)
                    command.Parameters.AddWithValue("@Id", id)
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