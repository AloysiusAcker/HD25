<%@ WebHandler Language="VB" Class="ProductoSku" %>

Imports System
Imports System.Web
Imports System.Data.SqlClient
Public Class ProductoSku : Implements IHttpHandler


    'Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest
    '    Dim Ruta_conexion As String = context.Request.QueryString("Ruta")
    '    Dim id As String = context.Request.QueryString("id")

    '    If Not String.IsNullOrEmpty(id) Then
    '        Using connection As New SqlConnection(Ruta_conexion)
    '            Using command As New SqlCommand("SELECT ART_IMAGEN FROM  TBINV_ARTICULOS_IMAGENES WHERE ART_CODIGO = @Id", connection)
    '                command.Parameters.AddWithValue("@Id", id)
    '                connection.Open()

    '                Dim imageBytes As Byte() = Nothing
    '                Dim result As Object = command.ExecuteScalar()

    '                If result IsNot Nothing AndAlso result IsNot DBNull.Value Then
    '                    imageBytes = DirectCast(result, Byte())
    '                    context.Response.ContentType = "image/jpeg"
    '                    context.Response.BinaryWrite(imageBytes)
    '                End If

    '            End Using
    '        End Using
    '    End If
    'End Sub

    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim ruta As String = context.Request.QueryString("Ruta")
        Dim id As String = context.Request.QueryString("id")
        'Dim Ruta As String = "workstation id=;packet size=4096;user id=sa;data source=tecn2024a\bbva;persist security info=False; POOLING=FALSE;initial catalog=BDGEmpresa1bb"
        ' Aquí debes implementar la lógica para obtener la imagen desde la base de datos
        Dim imageData As Byte() = ObtenerImagenDesdeBaseDatos(id, ruta)

        If imageData IsNot Nothing Then
            context.Response.ContentType = "image/jpeg"
            context.Response.BinaryWrite(imageData)
        Else
            context.Response.StatusCode = 404
        End If
    End Sub

    Private Function ObtenerImagenDesdeBaseDatos(id As String, ByVal psConexion As String) As Byte()
        ' Implementa la lógica para obtener la imagen desde la base de datos
        Using connection As New SqlConnection(psConexion)
            connection.Open()
            Using command As New SqlCommand("SELECT ART_IMAGEN FROM  TBINV_ARTICULOS_IMAGENES WHERE ART_CODIGO = @Id", connection)
                command.Parameters.AddWithValue("@Id", id)
                Dim result As Object = command.ExecuteScalar()
                If result IsNot DBNull.Value Then
                    Return DirectCast(result, Byte())
                End If
            End Using
        End Using
        Return Nothing
    End Function

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property


End Class