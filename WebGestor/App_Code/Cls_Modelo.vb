Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Modelo

    Public Function Filtrar_Descripcion_Modelo(ByVal psConexion As String, ByVal FiltroDesc As String, ByVal codMarca As String, ByVal codModelo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_TBINV_FILTRAR_DESCRIPCION_MODELO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@FILTRODESC", SqlDbType.VarChar).Value = FiltroDesc
        Cmd.Parameters.Add("@COD_MARCA", SqlDbType.Float).Value = codMarca
        Cmd.Parameters.Add("@COD_MODELO", SqlDbType.Float).Value = codModelo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_TBINV_FILTRAR_DESCRIPCION_MODELO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function CodigoModelo(ByVal psConexion As String, ByVal codMod As String) As String
        Dim codigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(ARTMOD_CODIGO) FROM TBINV_ARTICULO_MODELO WHERE ARTMAR_CODIGO like '" + codMod + "'"
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

    Public Function Buscar_Modelo(ByVal psConexion As String, ByVal codigoMo As String, ByVal descripcion As String, ByVal codMar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarModelo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MOD", codigoMo)
        Cmd.Parameters.AddWithValue("@DESCRIPC", descripcion)
        Cmd.Parameters.AddWithValue("@COD_MARC", codMar)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarModelo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Marcas_Modelo(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Marca_Modelo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_MARCA", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Marca_Modelo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Agregar_Marcas_Modelo(ByVal psConexion As String, ByVal codModelo As String, ByVal codMarca As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Marca_Modelo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodModelo", codModelo)
        Cmd.Parameters.AddWithValue("@CodMarca", codMarca)
        Cmd.Parameters.AddWithValue("@Descripcion", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Agregar_Marca_Modelo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualizar_Marcas_Modelo(ByVal psConexion As String, ByVal codModelo As String, ByVal codMarca As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Actualizar_Marca_Modelo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodModelo", codModelo)
        Cmd.Parameters.AddWithValue("@CodMarca", codMarca)
        Cmd.Parameters.AddWithValue("@Descripcion", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Actualizar_Marca_Modelo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Eliminar_Marcas_Modelo(ByVal psConexion As String, ByVal codModelo As String, ByVal codMarca As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Marca_Modelo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodModelo", codModelo)
        Cmd.Parameters.AddWithValue("@CodMarca", codMarca)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Eliminar_Marca_Modelo")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
