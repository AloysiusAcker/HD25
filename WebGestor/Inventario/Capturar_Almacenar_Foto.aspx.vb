Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Data
Imports System.IO
Imports WebGestor
Imports AspNet
Partial Class Capturar_Almacenar_Foto
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New Cls_Catalogo
            Dim dt As New DataTable
            txtCodArt.Value = Session("CodArt")
            txtNomArt.Value = Session("NombreArt")
            txtNomImg.Value = Session("NombreImg")
            If txtNomImg.Value = "" Then
                txtNomImg.Value = Session("NombreArt")
            End If
            TxtRutaServidor.Value = Session("Ruta_Emp")
        End If
    End Sub

    <WebMethod>
    Public Shared Function GuardarImagen(imageData As String, ByVal paraCodArt As String, ByVal paraRuta As String, ByVal paraNomImg As String) As String
        Using connection As New SqlConnection(paraRuta)
            Using command As New SqlCommand("UPDATE TBINV_ARTICULOS SET ART_IMG_NOM = '" & paraNomImg & "', ART_IMG = @Imagen WHERE ART_CODIGO = " & paraCodArt & " ", connection)
                command.Parameters.AddWithValue("@Imagen", Convert.FromBase64String(imageData.Split(",")(1)))
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
        Return "Imagen guardada con éxito."

    End Function
End Class
