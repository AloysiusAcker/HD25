Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Estado_Ticket
    Public Function Lista_Estado_Tiempo(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_ESTADO_TIEMPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_ESTADO_TIEMPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Estado_Relacion(ByVal psConexion As String, ByVal EstadoTicket As String,
                                        ByVal EstadoRelacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INGRESAR_ESTADO_RELACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADOTICKET", EstadoTicket)
        Cmd.Parameters.AddWithValue("@ESTADORELACION", EstadoRelacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INGRESAR_ESTADO_RELACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Estado_Tiempo(ByVal psConexion As String, ByVal ticket As String, ByVal dias As String,
                                                 ByVal horas As String, ByVal minutos As String, ByVal total As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INGRESAR_ESTADO_TIEMPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TICKET", ticket)
        Cmd.Parameters.AddWithValue("@DIAS", dias)
        Cmd.Parameters.AddWithValue("@HORAS", horas)
        Cmd.Parameters.AddWithValue("@MINUTOS", minutos)
        Cmd.Parameters.AddWithValue("@TOTAL", total)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INGRESAR_ESTADO_TIEMPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Actualizar_Estado_Tiempo(ByVal psConexion As String, ByVal ticket As String, ByVal dias As String,
                                                 ByVal horas As String, ByVal minutos As String, ByVal total As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ACTUALIZAR_ESTADO_TIEMPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TICKET", ticket)
        Cmd.Parameters.AddWithValue("@DIAS", dias)
        Cmd.Parameters.AddWithValue("@HORAS", horas)
        Cmd.Parameters.AddWithValue("@MINUTOS", minutos)
        Cmd.Parameters.AddWithValue("@TOTAL", total)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ACTUALIZAR_ESTADO_TIEMPO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Eliminar_Relacion(ByVal psConexion As String, ByVal Estado As String, ByVal EstadoRelacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_ELIMINAR_RELACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ESTADO", Estado)
        Cmd.Parameters.AddWithValue("@ESTADORELACION", EstadoRelacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_ELIMINAR_RELACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Estado_Ticket(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_ESTADO_TICKET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_ESTADO_TICKET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Tipo_Procesos(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Tipo_Procesos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Tipo_Procesos")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
