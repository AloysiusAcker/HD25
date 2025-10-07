Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Relacion_con_Procesos
    Public Function Lista_Relacion_Proceso(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LISTA_RELACION_PROCESO_PROCEDIMIENTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LISTA_RELACION_PROCESO_PROCEDIMIENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Estado_Relacion_Procesos(ByVal psConexion As String, ByVal Nivel1 As String,
                                                      ByVal Nivel2 As String, ByVal Nivel3 As String,
                                                      ByVal Proceso As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_INS_RELACION_PROCESOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NIVEL1", Nivel1)
        Cmd.Parameters.AddWithValue("@NIVEL2", Nivel2)
        Cmd.Parameters.AddWithValue("@NIVEL3", Nivel3)
        Cmd.Parameters.AddWithValue("@PROCESO", Proceso)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_INS_RELACION_PROCESOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Eliminar_Estado_Relacion_Procesos(ByVal psConexion As String, ByVal Nivel1 As String,
                                                      ByVal Nivel2 As String, ByVal Nivel3 As String,
                                                      ByVal Proceso As String) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_DEL_RELACION_PROCESOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NIVEL1", Nivel1)
        Cmd.Parameters.AddWithValue("@NIVEL2", Nivel2)
        Cmd.Parameters.AddWithValue("@NIVEL3", Nivel3)
        Cmd.Parameters.AddWithValue("@PROCESO", Proceso)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_DEL_RELACION_PROCESOS")
        Da.Fill(Dt)
        Return Dt

    End Function
    Public Function Llenar_Combo_Proceso(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_PROCESO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_PROCESO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Nivel1(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_NIVEL1", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_NIVEL1")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Nivel2(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_NIVEL2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_NIVEL2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llenar_Combo_Nivel3(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_LLENAR_COMBO_NIVEL3", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_LLENAR_COMBO_NIVEL3")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
