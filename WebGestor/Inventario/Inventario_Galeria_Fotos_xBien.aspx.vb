
Imports System.Data.SqlClient

Partial Class Inventario_Inventario_Galeria_Fotos_xBien
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Call Llenar_Galeria()
        End If
    End Sub

    Private Function ObtenerDatosDesdeLaBaseDeDatos() As List(Of Photo)
        Dim photos As New List(Of Photo)()

        Dim connectionString As String = Session("Ruta_Emp")
        Using connection As New SqlConnection(connectionString)
            connection.Open()
            Dim query As String = "SELECT serie_imagen as Imagen,serie_imagen_nom Descripcion FROM TBINV_ARTICULOS_SERIES_IMAGEN where serie_numerar = " & Session("SerieNumerar")
            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        Dim photo As New Photo()
                        ' Obtiene la imagen como tipo byte array
                        If Not reader.IsDBNull(reader.GetOrdinal("Imagen")) Then
                            photo.Imagen = DirectCast(reader("Imagen"), Byte())
                        End If
                        photo.Descripcion = Convert.ToString(reader("Descripcion"))
                        ' Puedes agregar más propiedades según tus necesidades

                        photos.Add(photo)
                    End While
                End Using
            End Using
        End Using

        Return photos
    End Function

    Private Sub Llenar_Galeria()
        Try
            Dim photoList As List(Of Photo) = ObtenerDatosDesdeLaBaseDeDatos()
            ' Llena el repeater con los datos
            rptPhotos.DataSource = photoList
            rptPhotos.DataBind()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Public Class Photo
        Public Property Imagen As Byte()
        Public Property Descripcion As String
    End Class
End Class
