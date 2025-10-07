Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Proyectos
    Public Function Lista_Proyectos(ByVal psConexion As String, ByVal año As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Proyectos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@AÑO", año)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Proyectos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Combo(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Comboproyectos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Comboproyectos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Registra_Proyecto(ByVal psConexion As String,
                                       ByVal Año As String,
                                       ByVal Codigo As String,
                                       ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Proyectos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@Año", Año)
        Cmd.Parameters.AddWithValue("@Codigo", Codigo)
        Cmd.Parameters.AddWithValue("@Descripcion", Descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Agregar_Proyectos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Filtrar_Descripcion_Proyecto(ByVal psConexion As String, ByVal FiltroDesc As String, ByVal psCodigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_TBINV_FILTRAR_DESCRIPCION_PROYECTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@FILTRODESC", FiltroDesc)
        Cmd.Parameters.AddWithValue("@CODIGO", FiltroDesc)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_TBINV_FILTRAR_DESCRIPCION_PROYECTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Actualiza_Proyecto(ByVal psConexion As String,
                                       ByVal Año As String,
                                       ByVal Codigo As String,
                                       ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Actualizar_Proyecto", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@Año", Año)
        Cmd.Parameters.AddWithValue("@Codigo", Codigo)
        Cmd.Parameters.AddWithValue("@Descripcion", Descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Actualizar_Proyecto")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Eliminar_Proyecto(ByVal psConexion As String,
                                    ByVal Codigo As String, ByVal Año As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Proyecto", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@Codigo", Codigo)
        Cmd.Parameters.AddWithValue("@Año", Año)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Eliminar_Proyecto")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CodigoProy(ByVal psConexion As String) As String
        Dim TxtCodigoProy As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(ISNULL(CONVERT(FLOAT,PROYECTO_CODIGO),0)) FROM TBINV_ALMACEN_RECEPCION_PROYECTO"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    TxtCodigoProy = 1 + Rs(0)
                End While
            Else
                TxtCodigoProy = 1
            End If
            Rs.Close()

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try

        Return TxtCodigoProy
    End Function
End Class