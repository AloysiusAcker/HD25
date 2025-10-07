Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Ubicacion
    Public Function Lista_Define_Ubicaciones(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Lista_Ubicaciones]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Lista_Ubicaciones]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Registra_Ubicaciones(ByVal psConexion As String, ByVal Codigo As Double,
                                        ByVal Descripcion As String, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_Agregar_Ubicaciones]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Decimal).Value = Codigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_Agregar_Ubicaciones]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Filtrar_Descripcion_Ubicacion(ByVal psConexion As String, ByVal FiltroDesc As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[prc_TBINV_FILTRAR_DESCRIPCION_UBICACION]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@FILTRODESC", FiltroDesc)
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[prc_TBINV_FILTRAR_DESCRIPCION_UBICACION]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualiza_Ubicaciones(ByVal psConexion As String, ByVal Codigo As String,
                                        ByVal Descripcion As String, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_Actualizar_Ubicaciones]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Decimal).Value = Codigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_Actualizar_Ubicaciones]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Eliminar_Ubicaciones(ByVal psConexion As String,
                                        ByVal Codigo As String, ByVal psdescripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_Eliminar_Ubicaciones]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@Codigo", Codigo) '@Descripcion
        Cmd.Parameters.AddWithValue("@Descripcion", psdescripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_Eliminar_Ubicaciones]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Codigo_Ubicaciones(ByVal psConexion As String) As String
        Dim TxtCodigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(UBICACION_CODIGO) FROM TBAREA_UBICACION"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    TxtCodigo = 1 + Rs(0)
                End While
            Else
                TxtCodigo = 1
            End If
            Rs.Close()

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try

        Return TxtCodigo
    End Function
End Class
