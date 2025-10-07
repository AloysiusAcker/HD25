Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Script.Serialization
Imports System.Web.Script.Services
Imports System.Web.Services
Structure Ubicacion
    Public IdUbicacion As String
    Public Nombre As String
    Public Descripcion As String
    Public Foto As String
    Public Lat As String
    Public Lng As String
End Structure

Partial Class EvaluacionProcesos_GoogleMaps_Prueba
    Inherits System.Web.UI.Page

    Private Sub EvaluacionProcesos_GoogleMaps_Prueba_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    <WebMethod>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function BuscarUbicacion(ByVal idUbicacion As Integer) As String
        Dim cnn As SqlConnection = New SqlConnection("Data Source=HACDATA17\pruebas;Initial Catalog = BDGrupoEmpresas; User Id=sa")

        Try
            cnn.Open()
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.Connection = cnn
            cmd.CommandType = CommandType.Text
            cmd.CommandText = " select OFICINA_CODIGO, OFICINA_DIRECCION,OFICINA_NOMBRE,OFICINA_LATITUD,OFICINA_LONGITUD " _
                            & " from TBPERSONAL_DEFINE_OFICINA where OFICINA_CODIGO=@id_ubicacion"
            cmd.Parameters.Add("@id_ubicacion", SqlDbType.Int).Value = idUbicacion
            'Dim dr As SqlDataReader = cmd.ExecuteReader()
            'Dim result As List(Of Ubicacion) = New List(Of Ubicacion)()

            Dim dr As SqlDataReader = cmd.ExecuteReader()
            Dim result As Ubicacion = New Ubicacion()

            If dr.Read() Then
                result = New Ubicacion() With {
                .IdUbicacion = dr("OFICINA_CODIGO").ToString(),
                .Nombre = dr("OFICINA_DIRECCION").ToString(),
                .Descripcion = dr("OFICINA_NOMBRE").ToString(),
                .Foto = dr("foto").ToString(),
                .Lat = dr("OFICINA_LATITUD").ToString(),
                .Lng = dr("OFICINA_LONGITUD").ToString()
            }
            End If

            dr.Close()
            Return New JavaScriptSerializer().Serialize(result)
        Catch ex As Exception
            Throw (ex)
        Finally
            cnn.Close()
            cnn.Dispose()
        End Try
    End Function

End Class
