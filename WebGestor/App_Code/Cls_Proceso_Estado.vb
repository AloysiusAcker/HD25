Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Proceso_Estado
    Public Function Lista_Proceso_Estado(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_RELACION_PROCESO_ESTADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_RELACION_PROCESO_ESTADO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Proceso_Estado(ByVal psConexion As String, ByVal EstadoCodigo As String,
                                        ByVal ProcesoCodigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INSERTAR_RELACION_PE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADOCODIGO", EstadoCodigo)
        Cmd.Parameters.AddWithValue("@PROCESOCODIGO", ProcesoCodigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INSERTAR_RELACION_PE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Eliminar_Relacion(ByVal psConexion As String, ByVal EstadoCodigo As String, ByVal ProcesoCodigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ELIMINAR_RELACION_PE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADOCODIGO", EstadoCodigo)
        Cmd.Parameters.AddWithValue("@PROCESOCODIGO", ProcesoCodigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ELIMINAR_RELACION_PE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Estado_Procesos(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Estado_Procesos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Estado_Procesos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Accion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_ACCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_ACCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Accion(ByVal psConexion As String, ByVal EmpresaCodigo As String,
                                       ByVal TicketEstado As String,
                                       ByVal TicketProceso As String,
                                       ByVal TicketAccion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INSERTAR_ACCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@EMPRESACODIGO", EmpresaCodigo)
        Cmd.Parameters.AddWithValue("@TICKETESTADO", TicketEstado)
        Cmd.Parameters.AddWithValue("@TICKETPROCESO", TicketProceso)
        Cmd.Parameters.AddWithValue("@TICKETACCION", TicketAccion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INSERTAR_ACCION")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
