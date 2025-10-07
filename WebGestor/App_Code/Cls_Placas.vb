Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Placas
    Public Function Lista_Recepcion(ByVal psConexion As String, ByVal pscod_almacen As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Lista_Almacen_Recepcion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@mostrar_almacen", pscod_almacen)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Lista_Almacen_Recepcion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Detalle_Recepcion(ByVal psConexion As String, ByVal pscod_recepcion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Lista_Almacen_Recepcion_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@cod_recepcion", pscod_recepcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Lista_Almacen_Recepcion_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Combo_Almacen(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Combo_Almacen_Recepcion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Combo_Almacen_Recepcion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Combo_Tipo_Placa(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Combo_Tipo_Placa", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Combo_Tipo_Placa")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Monstrar_Ultima_Placa(ByVal psConexion As String, ByVal psTipoPlaca As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ultima_Placa", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@TipoPlaca", psTipoPlaca)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ultima_Placa")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Generar_Placa(ByVal psConexion As String, ByVal psNumPlaca As String, ByVal psSerieNum As String, ByVal TipoArt As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Inventario_Generar_Placa", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@PlacaNro", psNumPlaca)
        Cmd.Parameters.AddWithValue("@SERIENUMERAR", psSerieNum)
        Cmd.Parameters.AddWithValue("@Tipo_Art", TipoArt)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable("Proc_Inventario_Generar_Placa")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Verificar_Placa(ByVal psconexion As String, ByVal NumPlaca As String) As DataTable
        Dim cn As New SqlConnection(psconexion)
        Dim Cmd As New SqlCommand("Proc_Inventario_Verificar_Placa", cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@PlacaNro", NumPlaca)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable("Proc_Inventario_Verificar_Placa")
        Da.Fill(dt)
        Return dt
    End Function

    Public Function Actualizar_Ultima_Placa(ByVal psconexion As String, ByVal ulPlaca As String, ByVal tipoArt As String) As DataTable
        Dim cn As New SqlConnection(psconexion)
        Dim Cmd As New SqlCommand("Proc_Inventario_Update_Ultima_Placa", cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@ultimaplacagenerada", ulPlaca)
        Cmd.Parameters.AddWithValue("@TipoPlaca", tipoArt)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable("Proc_Inventario_Update_Ultima_Placa")
        Da.Fill(dt)
        Return dt
    End Function
    Public Function Borrar_Placa(ByVal psconexion As String, ByVal psSerieNum As String, ByVal TipoArt As String) As DataTable
        Dim cn As New SqlConnection(psconexion)
        Dim Cmd As New SqlCommand("Proc_Inventario_Eliminar_Placa", cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@SERIENUMERAR", psSerieNum)
        Cmd.Parameters.AddWithValue("@Tipo_Art", TipoArt)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim dt As New DataTable("Proc_Inventario_Eliminar_Placa")
        Da.Fill(dt)
        Return dt
    End Function

    Public Function Lista_Placa(ByVal psConexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_ListarPlacaIn_Prop", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CodEmpresa", psCodEmpresa)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_ListarPlacaIn_Prop")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Sit(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_llenarSituacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_llenarSituacion")
        Da.Fill(Dt)
        Return Dt
    End Function




    Public Function Lista_Prop(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_LlenarCombo_Propietario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure



        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_LlenarCombo_Propietario")
        Da.Fill(Dt)
        Return Dt
    End Function









    Public Function RegistrarPlaca_Prop(ByVal psConexion As String, ByVal Codigo As String,
                                       ByVal plaIn As String, ByVal plaFn As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Proc_InsertPlacaIni_Prop]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@v_codPla", Codigo)
        Cmd.Parameters.AddWithValue("@v_placaini", plaIn)
        Cmd.Parameters.AddWithValue("@v_placafin", plaFn)

        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Proc_InsertPlacaIni_Prop]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function EliminarPlaca_Prop(ByVal psConexion As String, ByVal codigo As String) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_EliminaPlacaIProp", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@v_codPla", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_EliminaPlacaIProp")
        Da.Fill(Dt)
        Return Dt

    End Function

    Public Function ActualizaPlaca_Prop(ByVal psConexion As String, ByVal Codigo As String,
                                       ByVal plaIn As String, ByVal plaFn As String) As DataTable


        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_ActualizaPlacaIProp", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@v_codPla", Codigo)
        Cmd.Parameters.AddWithValue("@v_placaini", plaIn)
        Cmd.Parameters.AddWithValue("@v_placafin", plaFn)

        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_ActualizaPlacaIProp")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
