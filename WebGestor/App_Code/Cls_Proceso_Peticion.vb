Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Proceso_Peticion
    Public Function Lista_Proceso_Peticion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_RELACION_PROCESO_PETICION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_RELACION_PROCESO_PETICION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Peticion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_PETICION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_PETICION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Proceso_Peticion(ByVal psConexion As String, ByVal TipoPeticion As String,
                                        ByVal TipoProceso As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INSERTAR_RELACION_PP", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TIPOPETICION", TipoPeticion)
        Cmd.Parameters.AddWithValue("@TIPOPROCESO", TipoProceso)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INSERTAR_RELACION_PP")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Eliminar_Relacion_Proceso_Peticion(ByVal psConexion As String, ByVal TipoPeticion As String, ByVal TipoProceso As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ELIMINAR_RELACION_PP", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TIPOPETICION", TipoPeticion)
        Cmd.Parameters.AddWithValue("@TIPOPROCESO", TipoProceso)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ELIMINAR_RELACION_PP")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
