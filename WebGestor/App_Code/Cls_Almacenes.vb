Imports System.Data.SqlClient
Imports System.Data
Public Class Cls_Almacenes

    Public Function Codigo2(ByVal psConexion As String) As String
        Dim TxtCodigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(ALMACEN_CODIGO) FROM TBINV_ALMACENES"
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

    Public Function Registra_Almacen(ByVal psConexion As String, ByVal Codigo As String,
                                     ByVal Descripcion As String, ByVal Planta As String,
                                     ByVal CodCCS As String, ByVal Direccion As String,
                                     ByVal Tipo As String, ByVal Modo As String, ByVal Baja As String,
                                     ByVal psDpto As String, ByVal psProv As String, ByVal psDistrito As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Almacen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_ALMACEN", Codigo)
        Cmd.Parameters.AddWithValue("@DESC_ALMACEN", Descripcion)
        Cmd.Parameters.AddWithValue("@PLANTA_ALMACEN", Planta)
        Cmd.Parameters.AddWithValue("@COD_CENCOS", CodCCS)
        Cmd.Parameters.AddWithValue("@DIR_ALMACEN", Direccion)
        Cmd.Parameters.AddWithValue("@TIPO_ALMACEN", Tipo)
        Cmd.Parameters.AddWithValue("@MODO_ALMACEN", Modo)
        Cmd.Parameters.AddWithValue("@BAJA_ALMACEN", Baja)
        Cmd.Parameters.AddWithValue("@DPTO", psDpto)
        Cmd.Parameters.AddWithValue("@PROVINCIA", psProv)
        Cmd.Parameters.AddWithValue("@DISTRITO", psDistrito)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Agregar_Almacen")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Filtrar_Descripcion_Almacen(ByVal psConexion As String, ByVal FiltroDesc As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_TBINV_FILTRAR_DESCRIPCION_ALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@FILTRODESC", FiltroDesc)
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_TBINV_FILTRAR_DESCRIPCION_ALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Actualiza_Almacen(ByVal psConexion As String, ByVal Codigo As String,
                                      ByVal Descripcion As String, ByVal Planta As String,
                                      ByVal CodCCS As String, ByVal Direccion As String,
                                      ByVal Tipo As String, ByVal Modo As String, ByVal Baja As String,
                                      ByVal psDpto As String, ByVal psProv As String, ByVal psDistrito As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Actualizar_Almacen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_ALMACEN", Codigo)
        Cmd.Parameters.AddWithValue("@DESC_ALMACEN", Descripcion)
        Cmd.Parameters.AddWithValue("@PLANTA_ALMACEN", Planta)
        Cmd.Parameters.AddWithValue("@COD_CENCOS", CodCCS)
        Cmd.Parameters.AddWithValue("@DIR_ALMACEN", Direccion)
        Cmd.Parameters.AddWithValue("@TIPO_ALMACEN", Tipo)
        Cmd.Parameters.AddWithValue("@MODO_ALMACEN", Modo)
        Cmd.Parameters.AddWithValue("@BAJA_ALMACEN", Baja)
        Cmd.Parameters.AddWithValue("@DPTO", psDpto)
        Cmd.Parameters.AddWithValue("@PROVINCIA", psProv)
        Cmd.Parameters.AddWithValue("@DISTRITO", psDistrito)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Actualizar_Almacen")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Elimina_Almacen(ByVal psConexion As String, ByVal Codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Almacen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_ALMACEN", Codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Eliminar_Almacen")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Almacenes_Combos(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Almacenes_Combos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_ALMACEN", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Almacenes_Combos")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Almacenes(ByVal psConexion As String, ByVal Descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Lista_Almacenes", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@descrip", Descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Lista_Almacenes")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Tipo(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_ListaTipo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_ListaTipo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Departamento(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_ListaDepartamentos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_ListaDepartamentos")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Buscar_Ceco(ByVal psConexion As String, ByVal codigo As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarCC", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CECO", codigo)
        Cmd.Parameters.AddWithValue("@DESC_CECO", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarCC")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Buscar_Cecose(ByVal psConexion As String, ByVal codC As String, ByVal codS As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_BuscarCCS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_CECO", codC)
        Cmd.Parameters.AddWithValue("@COD_CECOSE", codS)
        Cmd.Parameters.AddWithValue("@DESC_CECOSE", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_BuscarCCS")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
