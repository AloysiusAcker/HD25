Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Estado_Cliente
    Public Function Lista_Estado_Cliente(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_RELACION_PROCESO_ESTADO_CLIENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_RELACION_PROCESO_ESTADO_CLIENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Estado(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_ESTADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_ESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Estado_Relacion(ByVal psConexion As String, ByVal EstadoCodigo As String,
                                        ByVal ProcesoCodigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INSERTAR_RELACION_EC", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADOCODIGO", EstadoCodigo)
        Cmd.Parameters.AddWithValue("@PROCESOCODIGO", ProcesoCodigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INSERTAR_RELACION_EC")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Eliminar_Relacion(ByVal psConexion As String, ByVal EstadoCodigo As String, ByVal ProcesoCodigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ELIMINAR_RELACION_EC", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADOCODIGO", EstadoCodigo)
        Cmd.Parameters.AddWithValue("@PROCESOCODIGO", ProcesoCodigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ELIMINAR_RELACION_EC")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
