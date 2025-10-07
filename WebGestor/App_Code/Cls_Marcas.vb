Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Marcas

    Public Function Lista_Marcas(ByVal psConexion As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Marcas", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@Desc", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Marcas")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Filtrar_Descripcion_Marca(ByVal psConexion As String, ByVal FiltroDesc As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_TBINV_FILTRAR_DESCRIPCION_MARCA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@FILTRODESC", SqlDbType.VarChar).Value = FiltroDesc
        Cmd.Parameters.Add("@CODIGO", SqlDbType.Float).Value = codigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_TBINV_FILTRAR_DESCRIPCION_MARCA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Registra_Marca(ByVal psConexion As String, ByVal Codigo As String,
                                        ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Marca", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MARCA", Codigo)
        Cmd.Parameters.AddWithValue("@DESC_MARCA", Descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Agregar_Marca")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualiza_Marca(ByVal psConexion As String, ByVal Codigo As String,
                                    ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Actualizar_Marca", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MARCA", Codigo)
        Cmd.Parameters.AddWithValue("@DESC_MARCA", Descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Actualizar_Marca")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Eliminar_Marca(ByVal psConexion As String,
                                        ByVal Codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Marca", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MARCA", Codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Eliminar_Marca")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Buscar_Marca(ByVal psConexion As String, ByVal codigo As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarMarca", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MAR", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIP", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarMarca")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function CodigoMarca(ByVal psConexion As String) As String
        Dim codigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(ARTMAR_CODIGO) FROM TBINV_ARTICULO_MARCA"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    codigo = 1 + Rs(0)
                End While
            Else
                codigo = 1
            End If
            Rs.Close()

        Catch ex As SqlException
        Catch ex As Exception
        Finally
            Cn.Close()
        End Try

        Return codigo
    End Function
End Class
