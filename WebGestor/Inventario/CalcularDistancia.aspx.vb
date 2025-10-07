Imports System
Imports System.Collections.Generic
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor

Public Class CalcularDistancia
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Coordenadas de la ubicación central (latitud, longitud)
            Dim latitudCentral As Double = -12.094
            Dim longitudCentral As Double = -77.02132
            Dim CentroCosto As String = "PE11000486"
            Dim Descripcion As String = "Oficina San Isidro"
            Dim Distancia As Double = 0

            Dim ubicacionCentral As New Tuple(Of Double, Double, String, String, Double)(latitudCentral, longitudCentral, CentroCosto, Descripcion, Distancia)

            ' Lista de las 50 ubicaciones con sus coordenadas
            Dim ubicaciones As New List(Of Tuple(Of Double, Double, String, String, Double))()
            ' Agrega las ubicaciones aquí

            Dim dtlatitudes As New DataTable
            dtlatitudes = ListaOficinas()

            For Each dr As DataRow In dtlatitudes.Rows
                ubicaciones.Add(New Tuple(Of Double, Double, String, String, Double)(Nz(dr("LATITUD")), Nz(dr("LONGITUD")), Nu(dr("codigo")), Nu(dr("nombres")), Nz(dr("Distancia"))))
            Next

            ' Calcular distancias entre la ubicación central y las 50 ubicaciones
            Dim distancias As New List(Of Double)()
            For Each ubicacion In ubicaciones
                distancias.Add(CalcularDistancia(ubicacionCentral, ubicacion))
            Next

            ' Crear una lista de índices ordenados por distancia
            Dim indicesOrdenados As List(Of Integer) = Enumerable.Range(0, ubicaciones.Count).ToList()
            indicesOrdenados.Sort(Function(i, j) distancias(i).CompareTo(distancias(j)))

            ' Inicializar grupos vacíos
            ' Inicializar grupos vacíos
            Dim grupos As New List(Of Tuple(Of Tuple(Of Double, Double, String, String, Double), Tuple(Of Double, Double, String, String, Double)))()

            ' Bucle para agrupar las ubicaciones
            For i As Integer = 0 To ubicaciones.Count - 1 Step 2
                If i + 1 < ubicaciones.Count Then
                    ' Crear una tupla que contenga las dos ubicaciones y agregarla a la lista de grupos
                    Dim ubicacion1 As Tuple(Of Double, Double, String, String, Double) = ubicaciones(i)
                    Dim ubicacion2 As Tuple(Of Double, Double, String, String, Double) = ubicaciones(i + 1)
                    Dim grupo As Tuple(Of Tuple(Of Double, Double, String, String, Double), Tuple(Of Double, Double, String, String, Double)) = New Tuple(Of Tuple(Of Double, Double, String, String, Double), Tuple(Of Double, Double, String, String, Double))(ubicacion1, ubicacion2)
                    grupos.Add(grupo)
                Else
                    ' Si queda una ubicación sin agrupar, puedes manejarla de la forma que desees
                    ' En este ejemplo, la ignoramos
                End If
            Next

            ' Establecer los datos del Repeater
            rptGroups.DataSource = grupos
            rptGroups.DataBind()

        End If
    End Sub

    Private Function CalcularDistancia(ubicacion1 As Tuple(Of Double, Double, String, String, Double), ubicacion2 As Tuple(Of Double, Double, String, String, Double)) As Double
        ' Coordenadas de la primera ubicación
        Dim radioTierra As Double = 6371.0

        ' Convertir las coordenadas de grados a radianes
        Dim latitud1Rad As Double = ConvertDegreesToRadians(ubicacion1.Item1)
        Dim longitud1Rad As Double = ConvertDegreesToRadians(ubicacion1.Item2)
        Dim latitud2Rad As Double = ConvertDegreesToRadians(ubicacion2.Item1)
        Dim longitud2Rad As Double = ConvertDegreesToRadians(ubicacion2.Item2)

        ' Calcular la diferencia de latitud y longitud
        Dim deltaLatitud As Double = latitud2Rad - latitud1Rad
        Dim deltaLongitud As Double = longitud2Rad - longitud1Rad

        ' Calcular la distancia utilizando la fórmula haversine
        Dim a As Double = Math.Sin(deltaLatitud / 2) * Math.Sin(deltaLatitud / 2) + Math.Cos(latitud1Rad) * Math.Cos(latitud2Rad) * Math.Sin(deltaLongitud / 2) * Math.Sin(deltaLongitud / 2)
        Dim c As Double = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a))
        Dim distancia As Double = radioTierra * c

        Return distancia
    End Function

    Private Function ConvertDegreesToRadians(degrees As Double) As Double
        Return degrees * Math.PI / 180.0
    End Function

    Private Function ListaOficinas() As DataTable
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmd As New SqlCommand("Prc_Inventario_Distancia_CentralxOficina", cn)
        cmd.CommandType = CommandType.StoredProcedure
        Dim da As New SqlDataAdapter()
        da.SelectCommand = cmd
        Dim tabla As New DataTable
        da.Fill(tabla)
        Return tabla
    End Function


End Class
