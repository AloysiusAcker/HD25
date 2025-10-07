Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class clsLogis_Listado
    Public Function Lista_Datos_Oficina(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                        ByVal pdCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPLOGIS_DATOS_OFICINA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodSeccion", SqlDbType.Float).Value = pdCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLOGIS_DATOS_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Centro_Costos(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                        ByVal pdCodInterno As String, ByVal psDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Logis_CentroCostos_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pdCodInterno
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Logis_CentroCostos_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ListaTodo_Centro_Costos(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                        ByVal pdCodInterno As String, ByVal psDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Logis_CentroCostos_Seccion_ListaTodo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pdCodInterno
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Logis_CentroCostos_Seccion_ListaTodo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Busca_Centro_Costos_xCodigo(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                ByVal pdCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Logis_CentroCostos_BuscaxCodCC", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodCC", SqlDbType.Float).Value = pdCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Logis_CentroCostos_BuscaxCodCC")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Logis_CentroCostos_Seccion_BuscaxCod
    Public Function Busca_Centro_Costos_Seccion_xCodigo(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                        ByVal pdCodigo As Double, ByVal psCodSeccion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Logis_CentroCostos_Seccion_BuscaxCod", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodCC", SqlDbType.Float).Value = pdCodigo
        Cmd.Parameters.Add("@CodSec", SqlDbType.Float).Value = psCodSeccion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Logis_CentroCostos_Seccion_BuscaxCod")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Centro_Costos_Seccion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                        ByVal pdCodInterno As String, ByVal psDescripcion As String, ByVal psCodCC As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Logis_CentroCostos_Seccion_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodCC", SqlDbType.Float).Value = psCodCC
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pdCodInterno
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Logis_CentroCostos_Seccion_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_Logis_CentroCosto_Seccion_Eliminar

    Public Function Eliminar_Centro_Costos_Seccion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                   ByVal psCodCC As Double, ByVal psCodSec As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Logis_CentroCosto_Seccion_Eliminar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodCC", SqlDbType.Float).Value = psCodCC
        Cmd.Parameters.Add("@CodSec", SqlDbType.Float).Value = psCodSec
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Logis_CentroCosto_Seccion_Eliminar")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
