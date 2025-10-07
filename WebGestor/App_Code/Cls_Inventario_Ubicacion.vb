Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Inventario_Ubicacion

    Public Function Llenar_Combo(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Inventario_Ubicacion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Lista_Inventario_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Lista_Inventario_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_InventarioUbicacion_xCodigo
    Public Function Inventario_ListaUbicaciones_xInventario(ByVal psConexion As String, ByVal psCodInventario As Double,
                                                            ByVal psCodInvUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_InventarioUbicacion_xCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = psCodInventario
        Cmd.Parameters.Add("@CodInvUbica", SqlDbType.Float).Value = psCodInvUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_InventarioUbicacion_xCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_InsertarCostos_xUbicacion(ByVal psConexion As String, ByVal psCodInvUbica As Double,
                                                         ByVal pdCosto_xBien As Decimal, pdCosto_Recojo As Decimal,
                                                         ByVal pdCosto_Movilidad As Decimal, pdCosto_Verificacion As Decimal,
                                                         ByVal pdCosto_Placado As Decimal) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_CostosIngreso_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInvUbica", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@Costo_xBien", SqlDbType.Decimal).Value = pdCosto_xBien
        Cmd.Parameters.Add("@Costo_Recojo", SqlDbType.Decimal).Value = pdCosto_Recojo
        Cmd.Parameters.Add("@Costo_Movilidad", SqlDbType.Decimal).Value = pdCosto_Movilidad
        Cmd.Parameters.Add("@Costo_Verificacion", SqlDbType.Decimal).Value = pdCosto_Verificacion
        Cmd.Parameters.Add("@Costo_Placado", SqlDbType.Decimal).Value = pdCosto_Placado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_CostosIngreso_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_Costos_xUbicacion(ByVal psConexion As String, ByVal psCodInvUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Costos_xUbicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInvUbica", SqlDbType.Float).Value = psCodInvUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Costos_xUbicacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_Ubicaciones_Personal(ByVal psConexion As String, ByVal psCodInvUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_Personal", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbica", SqlDbType.Float).Value = psCodInvUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_Personal")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Inventario_Ubicaciones_HorayFecha_Verifiacion(ByVal psConexion As String, ByVal psCodInvUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_FechayHora_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbica", SqlDbType.Float).Value = psCodInvUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_FechayHora_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_Ubicaciones_Personal_Verifiacion(ByVal psConexion As String, ByVal psCodInvUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_Personal_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbica", SqlDbType.Float).Value = psCodInvUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_Personal_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_Ubicaciones_InsPersonal(ByVal psConexion As String, ByVal psCodInvUbica As Double, ByVal psPersonal As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_InsPersonal", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInvUbi", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@PersonalInv", SqlDbType.VarChar).Value = psPersonal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_InsPersonal")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Cierre_Inventario_xUbicacion_(ByVal psConexion As String, ByVal psCodInvUbica As Double, ByVal psFechaCierre As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_Cierre", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInvUbi", SqlDbType.Float).Value = psCodInvUbica '@FechaCierre
        Cmd.Parameters.Add("@FechaCierre", SqlDbType.VarChar).Value = psFechaCierre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_Cierre")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_Inventario_Ubicacion_xCodigo
    Public Function Inventario_Ubicacion_xCodigo(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal psCodInvUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_xCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa '@CodEmpresa
        Cmd.Parameters.Add("@CodInvCodigo", SqlDbType.Float).Value = psCodInvUbica '@CodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_xCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_Ubicaciones_DelPersonal(ByVal psConexion As String, ByVal psCodInvUbica As Double, ByVal psPersonal As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_DelPersonal", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInvUbi", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@PersonalInv", SqlDbType.VarChar).Value = psPersonal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_DelPersonal")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Inventario_Ubicacion_Detalle(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Inventario_Ubicacion_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_INVENTARIO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Inventario_Ubicacion_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Almacenes_Inventario(ByVal psConexion As String, ByVal codigo As String,
                                               ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Almacenes_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Almacenes_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_CentroC_Inventario(ByVal psConexion As String, ByVal codigo As String,
                                               ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_CentroC_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_CentroC_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Ubicaciones_Inventario(ByVal psConexion As String, ByVal codigo As String,
                                               ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Ubicaciones_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Ubicaciones_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Agregar_Inventario_Ubicacion(ByVal psConexion As String, ByVal codigo As Double,
                                                 ByVal inventario As Double, ByVal tipo As String,
                                                 ByVal ubicacion As Double, ByVal responsable As String,
                                                 Optional psFechaPrograma As String = "") As DataTable
        Dim Dt As DataTable = Nothing
        Try
            Dim Cn As New SqlConnection(psConexion)
            Dim Cmd As New SqlCommand("Proc_Agregar_Inventario_Ubicacion", Cn)
            Cmd.CommandType = CommandType.StoredProcedure
            Cmd.Parameters.Add("@CODIGO", SqlDbType.Float).Value = codigo
            Cmd.Parameters.Add("@INVENTARIO", SqlDbType.Float).Value = inventario
            Cmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = tipo
            Cmd.Parameters.Add("@UBICACION", SqlDbType.Float).Value = ubicacion
            Cmd.Parameters.Add("@RESPONSABLE", SqlDbType.VarChar).Value = responsable
            Cmd.Parameters.Add("@FechaProgramacion", SqlDbType.VarChar).Value = psFechaPrograma
            Dim Da As New SqlDataAdapter(Cmd)
            Dt = New DataTable("Proc_Agregar_Inventario_Ubicacion")
            Da.Fill(Dt)
            Return Dt
        Catch ex As Exception
            Dt = Nothing
            Return Dt
        End Try
    End Function

    Public Function Inventario_Ubicacion_IngFechas(ByVal psConexion As String, ByVal pCodInvUbi As Double, ByVal psFechaprograma As String,
                                                   ByVal psFechaInicia As String, ByVal psFechaCierra As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_Modificar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInvUbi", SqlDbType.Float).Value = pCodInvUbi
        Cmd.Parameters.Add("@FechaProgramacion", SqlDbType.VarChar).Value = psFechaprograma
        Cmd.Parameters.Add("@FechaInicia", SqlDbType.VarChar).Value = psFechaInicia
        Cmd.Parameters.Add("@FechaCierre", SqlDbType.VarChar).Value = psFechaCierra
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_Modificar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Elimina_Inventario_Ubicacion(ByVal psConexion As String, ByVal codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Elimina_Inventario_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Elimina_Inventario_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Cargar_Equipos_Seriados(ByVal psConexion As String, ByVal inventario As String, ByVal codigo As String,
                                            ByVal tipo As String, ByVal articulo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Cargar_Equipos_Seriados", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@TIPO", tipo)
        Cmd.Parameters.AddWithValue("@ARTICULO", articulo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Cargar_Equipos_Seriados")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Cargar_Equipos_SeriadosU(ByVal psConexion As String, ByVal inventario As String, ByVal codigo As String,
                                             ByVal articulo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Cargar_Equipos_SeriadosU", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@ARTICULO", articulo)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Cargar_Equipos_SeriadosU")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Cargar_Accesorios(ByVal psConexion As String, ByVal inventario As Double, ByVal codigo As Double, ByVal tipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Cargar_Accesorios", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INVENTARIO", SqlDbType.Float).Value = inventario
        Cmd.Parameters.Add("@CODIGO", SqlDbType.Float).Value = codigo
        Cmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = tipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Cargar_Accesorios")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Eliminar_Detalle_Ubicacion(ByVal psConexion As String, ByVal inventario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Detalle_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Eliminar_Detalle_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualizar_Inventario_Ubicacion(ByVal psConexion As String, ByVal inventario As String, ByVal condicion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Actualizar_Inventario_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@CONDICION", condicion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Actualizar_Inventario_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Codigo(ByVal psConexion As String) As Double
        Dim TxtCodigo As Double = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT ISNULL(MAX(INVENTUBIC_CODIGO),0) FROM TBINVENTARIO_UBICACIONES"
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
    'Prc_Inventario_Gastos_Update
    Public Function Inventario_Gastos_xPersonal_Update(ByVal psConexion As String, ByVal pRegFecha As String, ByVal pRegUser As String,
                                                        ByVal pRegHora As String, ByVal pGastoCCosto As Double, ByVal pGastoTipo As String,
                                                        ByVal pGastoTipoMov As String, ByVal pGastoDocTipo As String, ByVal pGastoDocSerie As String,
                                                        ByVal pGastoDocNumero As Double, ByVal pGastoMoneda As String, ByVal pGastoImporte As Double,
                                                        ByVal pGastoGlosa As String, ByVal pValorSys As String, ByVal pGastoFecha As String, ByVal pNroRegistro As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gastos_Update", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NroRegistro", SqlDbType.Float).Value = pNroRegistro
        Cmd.Parameters.Add("@RegFecha", SqlDbType.VarChar).Value = pRegFecha
        Cmd.Parameters.Add("@RegUser", SqlDbType.VarChar).Value = pRegUser
        Cmd.Parameters.Add("@RegHora", SqlDbType.VarChar).Value = pRegHora
        Cmd.Parameters.Add("@GastoCCosto", SqlDbType.Float).Value = pGastoCCosto
        Cmd.Parameters.Add("@GastoTipo", SqlDbType.VarChar).Value = pGastoTipo
        Cmd.Parameters.Add("@GastoTipoMov", SqlDbType.VarChar).Value = pGastoTipoMov
        Cmd.Parameters.Add("@GastoDocTipo", SqlDbType.VarChar).Value = pGastoDocTipo
        Cmd.Parameters.Add("@GastoDocSerie", SqlDbType.VarChar).Value = pGastoDocSerie
        Cmd.Parameters.Add("@GastoDocNumero", SqlDbType.Decimal).Value = pGastoDocNumero
        Cmd.Parameters.Add("@GastoMoneda", SqlDbType.VarChar).Value = pGastoMoneda
        Cmd.Parameters.Add("@GastoImporte", SqlDbType.VarChar).Value = pGastoImporte
        Cmd.Parameters.Add("@GastoGlosa", SqlDbType.VarChar).Value = pGastoGlosa
        Cmd.Parameters.Add("@ValorSys", SqlDbType.VarChar).Value = pValorSys
        Cmd.Parameters.Add("@Gasto_Fecha", SqlDbType.VarChar).Value = pGastoFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gastos_Update")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_Gastos_xPersonal(ByVal psConexion As String, ByVal pRegFecha As String, ByVal pRegUser As String,
                                                ByVal pRegHora As String, ByVal pGastoCCosto As Double, ByVal pGastoTipo As String,
                                                ByVal pGastoTipoMov As String, ByVal pGastoDocTipo As String, ByVal pGastoDocSerie As String,
                                                ByVal pGastoDocNumero As Double, ByVal pGastoMoneda As String, ByVal pGastoImporte As Double,
                                                ByVal pGastoGlosa As String, ByVal pValorSys As String, ByVal pGastoFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gastos_Insert", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@RegFecha", SqlDbType.VarChar).Value = pRegFecha
        Cmd.Parameters.Add("@RegUser", SqlDbType.VarChar).Value = pRegUser
        Cmd.Parameters.Add("@RegHora", SqlDbType.VarChar).Value = pRegHora
        Cmd.Parameters.Add("@GastoCCosto", SqlDbType.Float).Value = pGastoCCosto
        Cmd.Parameters.Add("@GastoTipo", SqlDbType.VarChar).Value = pGastoTipo
        Cmd.Parameters.Add("@GastoTipoMov", SqlDbType.VarChar).Value = pGastoTipoMov
        Cmd.Parameters.Add("@GastoDocTipo", SqlDbType.VarChar).Value = pGastoDocTipo
        Cmd.Parameters.Add("@GastoDocSerie", SqlDbType.VarChar).Value = pGastoDocSerie
        Cmd.Parameters.Add("@GastoDocNumero", SqlDbType.Decimal).Value = pGastoDocNumero
        Cmd.Parameters.Add("@GastoMoneda", SqlDbType.VarChar).Value = pGastoMoneda
        Cmd.Parameters.Add("@GastoImporte", SqlDbType.VarChar).Value = pGastoImporte
        Cmd.Parameters.Add("@GastoGlosa", SqlDbType.VarChar).Value = pGastoGlosa
        Cmd.Parameters.Add("@ValorSys", SqlDbType.VarChar).Value = pValorSys
        Cmd.Parameters.Add("@Gasto_Fecha", SqlDbType.VarChar).Value = pGastoFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gastos_Insert")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Inventario_Gastos_Lista(ByVal psConexion As String, ByVal pUsuario As String,
                                            ByVal pFechaIni As String, ByVal pFechaFin As String, ByVal pTipo As String, ByVal pTipoMov As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gastos_Lista2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@GastoFechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@GastoFechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@GastoTipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@GastoTipoMov", SqlDbType.VarChar).Value = pTipoMov
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gastos_Lista2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_GastosLista_xCodigo(ByVal psConexion As String, ByVal pNroRegistro As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gastos_Lista_xCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGasto", SqlDbType.Float).Value = pNroRegistro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gastos_Lista_xCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GuardarImagen_Gastos(ByVal psConexion As String, ByVal pdCodRegistro As Double,
                                         ByVal img As Byte()) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gastos_Imagen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodRegistro", SqlDbType.Float).Value = pdCodRegistro
        Cmd.Parameters.Add("@Imagen", SqlDbType.Image).Value = img
        'Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        'imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gastos_Imagen")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
