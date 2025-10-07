Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Public Class Cls_Inventario
    Public Function Lista_Inventario_Ubicacion(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Lista_Inventario_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Lista_Inventario_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Lista_Inventario_Ubicacion_Estadistica(ByVal psConexion As String, ByVal pCodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Invnetario_Lista_Ubicaciones", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Invnetario_Lista_Ubicaciones")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_Inventario_Resumen_Costos(ByVal psConexion As String, ByVal pCodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_inventario_ResumenCostos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_inventario_ResumenCostos")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inventario_Monitoreo_Oficinas
    Public Function Lista_Inventario_Monitoreo(ByVal psConexion As String, ByVal pFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Monitoreo_Oficinas", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Monitoreo_Oficinas")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Inventario_Monitoreo_xOficina(ByVal psConexion As String, ByVal pCodUbicaInv As Double, ByVal pFecha As String, ByVal pFechafin As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Monitoreo_xOficinas", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbicaInv", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@psFechaFin", SqlDbType.VarChar).Value = pFechafin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Monitoreo_xOficinas")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Inventario_Monitoreo_xOficina2(ByVal psConexion As String, ByVal pCodUbicaInv As Double, ByVal pFecha As String, ByVal pFechafin As String, ByVal pEstado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Monitoreo_xOficinas2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbicaInv", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@psFechaFin", SqlDbType.VarChar).Value = pFechafin '
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Monitoreo_xOficinas2")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_Inventario_Monitoreo_xOficina3(ByVal psConexion As String, ByVal pCodUbicaInv As Double, ByVal pFecha As String, ByVal pFechafin As String, ByVal pEstado As String,
                                                         ByVal pCodInventario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Monitoreo_xOficinas3", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbicaInv", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@psFechaFin", SqlDbType.VarChar).Value = pFechafin '
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@CodInv", SqlDbType.VarChar).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Monitoreo_xOficinas3")
        Da.Fill(Dt)
        Return Dt
    End Function '@CodInv
    Public Function Lista_Inventario_Monitoreo_Resumen(ByVal psConexion As String, ByVal pCodUbicaInv As Double, ByVal pFecha As String, ByVal pFechafin As String, ByVal pEstado As String,
                                                         ByVal pCodInventario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Monitoreo_Resumen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbicaInv", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@psFechaFin", SqlDbType.VarChar).Value = pFechafin '
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@CodInv", SqlDbType.VarChar).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Monitoreo_Resumen")
        Da.Fill(Dt)
        Return Dt
    End Function '@CodInv  

    Public Function Lista_Inventario_Monitoreo_xOficina_Exportar(ByVal psConexion As String, ByVal pCodUbicaInv As Double, ByVal pFecha As String, ByVal pFechafin As String, ByVal pEstado As String,
                                                         ByVal pCodInventario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Monitoreo_xOficinas_Exportar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodUbicaInv", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@psFechaFin", SqlDbType.VarChar).Value = pFechafin '
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@CodInv", SqlDbType.VarChar).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Monitoreo_xOficinas_Exportar")
        Da.Fill(Dt)
        Return Dt
    End Function '@CodInv
    Public Function Inventariados_Nuevos(ByVal psConexion As String, ByVal pCodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_BienesNuevos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_BienesNuevos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventariados_Ok(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pFechaIni As String, ByVal pFechaFin As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_BienesOk", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_BienesOk")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Invenatrio_EquiposNoEncontrados_xubi(ByVal psConexion As String, ByVal pCodUbicaInv As Double,
                                                         ByVal pCodArt As String, ByVal psSerieNumerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaBienes_xUbi", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.VarChar).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaBienes_xUbi")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Invenatrio_EquiposNoEncontrados_xubi_C(ByVal psConexion As String, ByVal pCodUbicaInv As Double,
                                                         ByVal pCodArt As String, ByVal psSerieNumerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaBienes_xUbi_C", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.VarChar).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaBienes_xUbi_C")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Invenatrio_Conciliar_EquiposNoEncontrados(ByVal psConexion As String, ByVal pCodUbicaInv As Double,
                                                         ByVal pCodArt As String, ByVal psSerieNumerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Conciliar_ListaBienes_NoInventariados", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.VarChar).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Conciliar_ListaBienes_NoInventariados")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Invenatrio_Conciliar_EquiposNoEncontrados_C(ByVal psConexion As String, ByVal pCodUbicaInv As Double,
                                                         ByVal pCodArt As String, ByVal psSerieNumerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Conciliar_ListaBienes_NoInventariados_C", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.VarChar).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Conciliar_ListaBienes_NoInventariados_C")
        Da.Fill(Dt)
        Return Dt
    End Function '

    Public Function Invenatrio_Conciliar_Listas(ByVal psConexion As String, ByVal pCodUbicaInv As Double,
                                                ByVal pCodArt As String, ByVal pCodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Concliar_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Concliar_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Invenatrio_Lista_Oficina_SinUbicacion(ByVal psConexion As String, ByVal pCodUbicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Lista_Oficinas_SinUbicaciones", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.Float).Value = pCodUbicacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Lista_Oficinas_SinUbicaciones")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Invenatrio_Conciliar_Listas_Exportar(ByVal psConexion As String, ByVal pCodUbicaInv As Double,
                                                         ByVal pCodArt As String, ByVal pCodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Concliar_Lista_Exportar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Concliar_Lista_Exportar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_Bienes_NoEncontrados(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                    ByVal pCodUbicaInv As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Bienes_No_Encontrados", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Inventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Bienes_No_Encontrados")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_Bienes_NoEncontrados_Exportar(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                             ByVal pCodUbicaInv As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Bienes_No_Encontrados_Exportar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Inventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@InvCodUbica", SqlDbType.Float).Value = pCodUbicaInv
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Bienes_No_Encontrados_Exportar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_TablaTemporal(ByVal psConexion As String, ByVal pdSerieNumerar As Double,
                                             ByVal pdPlacaNro As Double, ByVal psSerieNro As String) As DataTable
        '
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_PlacaTemporal", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Serie_numerar", SqlDbType.Float).Value = pdSerieNumerar
        Cmd.Parameters.Add("@Placa_nro", SqlDbType.Float).Value = pdPlacaNro
        Cmd.Parameters.Add("@Serie_nro", SqlDbType.VarChar).Value = psSerieNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_PlacaTemporal")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventariados_CargaTabla_BienesOk(ByVal psConexion As String, ByVal pUser As String, ByVal pFecha As String, ByVal pHora As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_BienesOk_CargarTabla", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = pHora
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_BienesOk_CargarTabla")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_InterfaceGPS_CargaTabla_BienesOk(ByVal psConexion As String, ByVal pUser As String, ByVal pFecha As String, ByVal pHora As String,
                                                                ByVal pdMovNro As Double, ByVal pdCodCCosto As Double, ByVal psFechaIni As String, ByVal psFechaFin As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_GPS_BienesOk_CargarTabla", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = pHora
        Cmd.Parameters.Add("@MovNro", SqlDbType.Int).Value = pdMovNro
        Cmd.Parameters.Add("@CodCCosto", SqlDbType.Float).Value = pdCodCCosto
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_GPS_BienesOk_CargarTabla")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_Gps(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_Bienes", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_Bienes")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_InterfaceGps(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal pdMovNro As Double, ByVal psTipoMov As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_Lista_Bienes", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@MovNro", SqlDbType.Float).Value = pdMovNro
        Cmd.Parameters.Add("@MovTipo", SqlDbType.VarChar).Value = psTipoMov
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_Lista_Bienes")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_MovimientoGPS_xNro(ByVal psConexion As String, ByVal pdMovNro As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_ListaMov_xNro", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@MovNro", SqlDbType.Float).Value = pdMovNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_ListaMov_xNro")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Movimiento_InterfaceGps(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_ListaMov", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_ListaMov")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_PlacasNoMover_InterfaceGps(ByVal psConexion As String, ByVal pMovNro As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_PlacasNoMover", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@MovNro", SqlDbType.Float).Value = pMovNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_PlacasNoMover")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_BienesNoEncontrados_Gps(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_Bienes_NoEncontrados", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_Bienes_NoEncontrados")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_GpsNoEncontrados_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_BienesNoEncontrados_INF", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_BienesNoEncontrados_INF")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_Gps_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_Bienes_INF", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_Bienes_INF")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_GpsNoEncontrados_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_BienesNoEncontrados_Mob", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_BienesNoEncontrados_Mob")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_Gps_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_Bienes_MOB", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_Bienes_MOB")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_BienesNoencontrados_Gps201_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_ListaNoEncontrados_Bienes201_MOB", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_ListaNoEncontrados_Bienes201_MOB")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_Gps201_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_Bienes201_MOB", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_Bienes201_MOB")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Lista_BienesNoEncontrado_Gps201_Inf(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_ListaNoEncontrado_Bienes201_INF", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_ListaNoEncontrado_Bienes201_INF")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_Gps201_Inf(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Lista_Bienes201_INF", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Lista_Bienes201_inf")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Gps_Generar501_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Genera501_Mob", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Genera501_Mob")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function GpsInterface_Generar501_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double,
                                                ByVal psFecha As String, ByVal pdMovNro As Double, ByVal psTipoMov As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_Genera501_Mob", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@MovNro", SqlDbType.Float).Value = pdMovNro
        Cmd.Parameters.Add("@TipoMov", SqlDbType.VarChar).Value = psTipoMov
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_Genera501_Mob")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Gps_Generar501_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Genera501_Inf", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Genera501_Inf")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function GpsInterface_Generar501_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double,
                                                ByVal psFecha As String, ByVal pdMovNro As Double, ByVal psTipoMov As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_Genera501_Inf", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha '@MovNro
        Cmd.Parameters.Add("@MovNro", SqlDbType.Float).Value = pdMovNro
        Cmd.Parameters.Add("@TipoMov", SqlDbType.VarChar).Value = psTipoMov
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_Genera501_Inf")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GpsNoEncontrado_Generar501_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_GpsNoEncontrado_Genera501_Mob", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_GpsNoEncontrado_Genera501_Mob")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GpsNoEncontrado_Generar501_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_GpsNoEncontrado_Genera501_Inf", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_GpsNoEncontrado_Genera501_Inf")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Gps_Generar201_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Genera201_Inf", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Genera201_Inf")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GpsInterface_Generar201_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double,
                                                ByVal psFecha As String, ByVal pdMovNro As Double, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_Genera201_Inf", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@MovNro", SqlDbType.Float).Value = pdMovNro
        Cmd.Parameters.Add("@TipoMov", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_Genera201_Inf")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Gps_Generar201_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Genera201_Mob", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Genera201_Mob")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GpsInterface_Generar201_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double,
                                                ByVal psFecha As String, ByVal pdMovNro As Double, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_InterfaceGps_Genera201_Mob", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@MovNro", SqlDbType.Float).Value = pdMovNro
        Cmd.Parameters.Add("@TipoMov", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_InterfaceGps_Genera201_Mob")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GpsNoEncontrado_Generar201_INF(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_GpsNoEncontrados_Genera201_Inf", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_GpsNoEncontrados_Genera201_Inf")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GpsNoEncontrado_Generar201_MOB(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInv_Ubica As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_GpsNoEncontrados_Genera201_Mob", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@FechaMov", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_GpsNoEncontrados_Genera201_Mob")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_Gps_xSerieEquipo(ByVal psConexion As String, ByVal pSerie_Equipo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_GpsCarga_Buscar_xPlaca", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Placa_Nro", SqlDbType.Float).Value = pSerie_Equipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_GpsCarga_Buscar_xPlaca")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Bienes_Gps_xSerieEquipoNoEncontrado(ByVal psConexion As String, ByVal pSerie_Equipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_CargaGps_Buscar_xPlacaNoEncontrada", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Placa_Nro", SqlDbType.VarChar).Value = pSerie_Equipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_CargaGps_Buscar_xPlacaNoEncontrada")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BuscarCaracteristica_xCecoseInterno(ByVal psConexion As String, ByVal psCecoseInterno As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Gps_Buscar_xCentroCosto", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Cecose_Cod_Interno", SqlDbType.VarChar).Value = psCecoseInterno
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Gps_Buscar_xCentroCosto")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Conciliacion_Lista_Inventariado(ByVal psConexion As String, ByVal pCodInv_Ubica As Double, ByVal pPlaca_Nro As Double,
                                                    ByVal pSerie_nro As String, pArt_Codigo As Double, ByVal pArt_Descripcion As String,
                                                    ByVal pCodUbicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Conciliacion_CambioEstado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInv_Ubica
        Cmd.Parameters.Add("@Placa_Nro", SqlDbType.Float).Value = pPlaca_Nro
        Cmd.Parameters.Add("@Serie_nro", SqlDbType.VarChar).Value = pSerie_nro
        Cmd.Parameters.Add("@Art_Codigo", SqlDbType.Float).Value = pArt_Codigo
        Cmd.Parameters.Add("@Art_Descripcion", SqlDbType.VarChar).Value = pArt_Descripcion
        Cmd.Parameters.Add("@CodUbicacion", SqlDbType.VarChar).Value = pCodUbicacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Conciliacion_CambioEstado")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Inventario(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = "0001"
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Devolver_UltimaObservacion(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Invenatrio_Observacion_UltimoRegistro", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@OBSERV_INVUBICA", SqlDbType.Float).Value = pdCodUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Invenatrio_Observacion_UltimoRegistro")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inventario_SinAccesoUbica_UltimoRegistro

    Public Function Devolver_UltimoRegistro_UbicacionsinAcceso(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_SinAccesoUbica_UltimoRegistro", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INVUBICA_CODIGO", SqlDbType.Float).Value = pdCodUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_SinAccesoUbica_UltimoRegistro")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Observacion(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodUbica As Double,
                                         ByVal psDetalle As String, ByVal psSysCre As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Invenatrio_Observacion_Insertar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@OBSERV_INVUBICA", SqlDbType.Float).Value = pdCodUbica
        Cmd.Parameters.Add("@OBSERV_DETALLE", SqlDbType.VarChar).Value = psDetalle
        Cmd.Parameters.Add("@OBSERV_SYS_CRE", SqlDbType.VarChar).Value = psSysCre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Invenatrio_Observacion_Insertar")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inventario_SinAccesoUbicacion_Lista
    Public Function Lista_Observacion(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Invenatrio_Observacion_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@OBSERV_INVUBICA", SqlDbType.Float).Value = pdCodUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Invenatrio_Observacion_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Ubicaciones_SinAcceso(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodUbica As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_SinAccesoUbicacion_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INVUBICA_CODIGO", SqlDbType.Float).Value = pdCodUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_SinAccesoUbicacion_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inventario_UbicacionSinAcceso_Insertar
    Public Function Insertar_Ubicaciones_SinAcceso(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodUbica As Double, ByVal psDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_UbicacionSinAcceso_Insertar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INVUBICA_CODIGO", SqlDbType.Float).Value = pdCodUbica
        Cmd.Parameters.Add("@INVUBICA_DESCRIPCION", SqlDbType.VarChar).Value = psDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_UbicacionSinAcceso_Insertar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inv_Lista_Flujo_Atencion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                             ByVal psTipoGuia As String, ByVal psFechaIni As String, ByVal psFechaFin As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_Flujo_Atencion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@TipoGuia", SqlDbType.VarChar).Value = psTipoGuia
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_Flujo_Atencion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Registra_Inventario(ByVal psConexion As String, ByVal Codigo As Double,
                                        ByVal Fecha As String, ByVal Descripcion As String,
                                        ByVal Responsable As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InventarioCod", SqlDbType.Float).Value = Codigo
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = Fecha
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@Responsable", SqlDbType.VarChar).Value = Responsable
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Agregar_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualiza_Inventario(ByVal psConexion As String, ByVal Codigo As String,
                                        ByVal Fecha As String, ByVal Descripcion As String,
                                        ByVal Responsable As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Actualizar_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InventarioCod", SqlDbType.VarChar).Value = Codigo
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = Fecha
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = Descripcion
        Cmd.Parameters.Add("@Responsable", SqlDbType.VarChar).Value = Responsable
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Actualizar_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InterfaceGps_CargaTablaMov(ByVal psConexion As String, ByVal pUser As String, ByVal pFecha As String, ByVal pHora As String,
                                                                ByVal pdMovNro As Double, ByVal pdCodCCosto As Double, ByVal psFechaIni As String, ByVal psFechaFin As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_GPS_CargarTablaMovimientos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = pHora
        Cmd.Parameters.Add("@MovNro", SqlDbType.Int).Value = pdMovNro
        Cmd.Parameters.Add("@CodCCosto", SqlDbType.Float).Value = pdCodCCosto
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_GPS_CargarTablaMovimientos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Eliminar_Inventario(ByVal psConexion As String,
                                        ByVal Codigo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Eliminar_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@InventarioCod", SqlDbType.VarChar).Value = Codigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Eliminar_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_NoEncontrados_xUsuario(ByVal pConexion As String, ByVal pSerieNumerar As Double, ByVal pSerie_Nro As String,
                                                      ByVal pPlaca_Nro As Double, ByVal pInvUbiCodigo As Double, ByVal pUbica_Tipo As String,
                                                      ByVal pUbicaCodigo As Double, ByVal pUsuario As String, ByVal pSysCre As String) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_NoEncontrados_xUsuario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Serie_Numerar", SqlDbType.Float).Value = pSerieNumerar
        Cmd.Parameters.Add("@Serie_Nro", SqlDbType.VarChar).Value = pSerie_Nro
        Cmd.Parameters.Add("@Placa_Nro", SqlDbType.Float).Value = pPlaca_Nro
        Cmd.Parameters.Add("@InvUbi_Codigo", SqlDbType.Float).Value = pInvUbiCodigo
        Cmd.Parameters.Add("@Ubica_Tipo", SqlDbType.VarChar).Value = pUbica_Tipo
        Cmd.Parameters.Add("@Ubica_Codigo", SqlDbType.Float).Value = pUbicaCodigo
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@SysCre", SqlDbType.VarChar).Value = pSysCre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_NoEncontrados_xUsuario")
        Da.Fill(Dt)
        Return Dt
    End Function

    '
    Public Function Inventario_NoEncontrados_Lista_xUsuario(ByVal psConexion As String, ByVal psUsuario As String,
                                                            ByVal psNomArt As String, ByVal pdCodArt As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_NoEncontrados_Lista_xUsuario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psUsuario
        Cmd.Parameters.Add("@ArtNombre", SqlDbType.VarChar).Value = psNomArt
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_NoEncontrados_Lista_xUsuario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Codigo(ByVal psConexion As String) As String
        Dim TxtCodigo As Integer = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " SELECT MAX(ISNULL(INVENT_CODIGO,0)) FROM TBINVENTARIO"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows = True Then
                While Rs.Read
                    TxtCodigo = 1 + Nz(Rs(0))
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
