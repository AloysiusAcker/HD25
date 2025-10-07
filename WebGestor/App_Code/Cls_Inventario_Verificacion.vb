Imports System.Data
Imports System.Data.SqlClient
Public Class Cls_Inventario_Verificacion



    Public Function Lista_Inventario_Verificacion(ByVal psConexion As String, ByVal codigo As Double,
                                                  ByVal tipo As String, ByVal ubicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Lista_Inventario_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@COD_INVENTARIO", SqlDbType.Float).Value = codigo
        Cmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = tipo
        Cmd.Parameters.Add("@UBICACION", SqlDbType.Float).Value = ubicacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Lista_Inventario_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_Articulos_xInventario(ByVal psConexion As String, ByVal codigoInventario As Double,
                                                  ByVal ubicatipo As String, ByVal ubicaCodigo As Double, ByVal pdCodInv_Ubica As Double, ByVal psCodUbicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Articulos_xInventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Cod_Inventario", SqlDbType.Float).Value = codigoInventario
        Cmd.Parameters.Add("@UbicaTipo", SqlDbType.VarChar).Value = ubicatipo
        Cmd.Parameters.Add("@UbicaCodigo", SqlDbType.Float).Value = ubicaCodigo
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pdCodInv_Ubica
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.Float).Value = psCodUbicacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Articulos_xInventario")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Articulos_xPlacar(ByVal psConexion As String, ByVal codigoInventario As Double,
                                                  ByVal pdNomArt As String, ByVal pdCodArt As Double, ByVal pdCodInv_Ubica As Double, ByVal psCodUbicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Inventario_xPlacar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Cod_Inventario", SqlDbType.Float).Value = codigoInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pdCodInv_Ubica
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.Float).Value = psCodUbicacion
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@ArtNombre", SqlDbType.VarChar).Value = pdNomArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Inventario_xPlacar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Articulos_BienesxPlacar(ByVal psConexion As String, ByVal codigoInventario As Double,
                                                  ByVal pdNomArt As String, ByVal pdCodArt As Double, ByVal pdCodInv_Ubica As Double, ByVal psCodUbicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Inventario_BienesxPlacar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Cod_Inventario", SqlDbType.Float).Value = codigoInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pdCodInv_Ubica
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.Float).Value = psCodUbicacion
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@ArtNombre", SqlDbType.VarChar).Value = pdNomArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Inventario_BienesxPlacar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ListaTop5_Inventario_Verificacion(ByVal psConexion As String, ByVal codigo As String,
                                                  ByVal tipo As String, ByVal ubicacion As String, ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaTop5_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@COD_INVENTARIO", SqlDbType.VarChar).Value = codigo
        Cmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = tipo
        Cmd.Parameters.Add("@UBICACION", SqlDbType.VarChar).Value = ubicacion
        Cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaTop5_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inventario_ListaBienes_Inventariado
    Public Function ListaBienes_Inventariados(ByVal psConexion As String, ByVal codigo As Double,
                                              ByVal tipo As String, ByVal ubicacion As Double, ByVal pPlacaNro As String, ByVal pdNroInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaBienes_Inventariado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@COD_INVENTARIO", SqlDbType.Float).Value = codigo
        Cmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = tipo
        Cmd.Parameters.Add("@UBICACION", SqlDbType.Float).Value = ubicacion
        Cmd.Parameters.Add("@NroPlaca", SqlDbType.Float).Value = pPlacaNro '@InventarioNro
        Cmd.Parameters.Add("@InventarioNro", SqlDbType.Float).Value = pdNroInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaBienes_Inventariado")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Inventario_ListaTodoBienInventariado
    Public Function ListaBienes_TodosInventariados(ByVal psConexion As String, ByVal codigo As Double,
                                              ByVal tipo As String, ByVal ubicacion As Double, ByVal pPlacaNro As Double,
                                              ByVal psFechaini As String, ByVal psFechaFin As String, ByVal pdNroInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaTodoBienInventariado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@COD_INVENTARIO", SqlDbType.Float).Value = codigo
        Cmd.Parameters.Add("@TIPO", SqlDbType.VarChar).Value = tipo
        Cmd.Parameters.Add("@UBICACION", SqlDbType.Float).Value = ubicacion
        Cmd.Parameters.Add("@NroPlaca", SqlDbType.Float).Value = pPlacaNro
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaini
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin '@NroInventario
        Cmd.Parameters.Add("@NroInventario", SqlDbType.Float).Value = pdNroInventario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaTodoBienInventariado")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_NoInventario_Verificacion(ByVal psConexion As String, ByVal codigo As String,
                                                  ByVal tipo As String, ByVal ubicacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Lista_NoInventario_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_INVENTARIO", codigo)
        Cmd.Parameters.AddWithValue("@TIPO", tipo)
        Cmd.Parameters.AddWithValue("@UBICACION", ubicacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Lista_NoInventario_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function '

    Public Function Inventario_InsUpdLog(ByVal psConexion As String, ByVal psTipoRegistro As String, ByVal pdInvCodigo As Double,
                                         ByVal pdInvUbicCodigo As Double, ByVal psUser As String, ByVal psFecha As String,
                                         ByVal psHoraIni As String, ByVal psHoraFin As String, ByVal pdSerieNumerar As Double,
                                         ByVal psTipoEntrada As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Verificaion_InsUpdLog", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@LOGINV_TIPO_REGISTRO", SqlDbType.VarChar).Value = psTipoRegistro
        Cmd.Parameters.Add("@LOGINV_INV_CODIGO", SqlDbType.Float).Value = pdInvCodigo
        Cmd.Parameters.Add("@LOGINV_INV_UBICODIGO", SqlDbType.Float).Value = pdInvUbicCodigo
        Cmd.Parameters.Add("@LOGINV_USER", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@LOGINV_FECHA_VERIFICA", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@LOGINV_HORA_VERIFICA", SqlDbType.VarChar).Value = psHoraIni
        Cmd.Parameters.Add("@LOGINV_HORA_FIN", SqlDbType.VarChar).Value = psHoraFin
        Cmd.Parameters.Add("@LOGINV_SERIE_NUMERAR", SqlDbType.Float).Value = pdSerieNumerar
        Cmd.Parameters.Add("@TipoEntrada", SqlDbType.VarChar).Value = psTipoEntrada
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Verificaion_InsUpdLog")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Bien_Inventariado(ByVal psConexion As String, ByVal pCodEmpresa As String,
                                          ByVal psInvUbicTipo As String, ByVal pdInvUbicCodigo As Double,
                                          ByVal pdSerieNumerar As Double, ByVal pdInvCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_BienExiste", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@InvUbicTipo", SqlDbType.VarChar).Value = psInvUbicTipo
        Cmd.Parameters.Add("@InvUbicCodigo", SqlDbType.Float).Value = pdInvUbicCodigo
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.Float).Value = pdSerieNumerar
        Cmd.Parameters.Add("@InvCodigo", SqlDbType.Float).Value = pdInvCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_BienExiste")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ListaUbicacionInventario_xCodigo(ByVal psConexion As String, ByVal pCodEmpresa As String, ByVal pdCodInventario As Double,
                                          ByVal psInvUbicTipo As String, ByVal pdInvUbicCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaUbicacion_xCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pdCodInventario
        Cmd.Parameters.Add("@UbicTipo", SqlDbType.VarChar).Value = psInvUbicTipo
        Cmd.Parameters.Add("@UbicCodigo", SqlDbType.Float).Value = pdInvUbicCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaUbicacion_xCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Resumen_Invenatrio_xUbicacion(ByVal psConexion As String, ByVal pCodEmpresa As String,
                                          ByVal psInvUbicTipo As String, ByVal pdInvUbicCodigo As Double,
                                          ByVal pdCodInventario As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Resumen_InvUbic_XEstado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pdCodInventario
        Cmd.Parameters.Add("@UbicTipo", SqlDbType.VarChar).Value = psInvUbicTipo
        Cmd.Parameters.Add("@UbicCodigo", SqlDbType.Float).Value = pdInvUbicCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Resumen_InvUbic_XEstado")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_BienExiste(ByVal psConexion As String, ByVal pCodEmpresa As String,
                                      ByVal psNroSerie As String, ByVal pdNroPlaca As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_INDIVIDUAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@NroSerie", SqlDbType.VarChar).Value = psNroSerie
        Cmd.Parameters.Add("@NroPlaca", SqlDbType.Float).Value = pdNroPlaca
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_INDIVIDUAL")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_ResumenCantxArt_xUbicacion(ByVal psConexion As String, ByVal pCodEmpresa As String,
                                                          ByVal pCodInventario As Double, ByVal pCodUbicacionInv As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ResumenCantxArticulo_xUbicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodUbicacionInv", SqlDbType.Float).Value = pCodUbicacionInv
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ResumenCantxArticulo_xUbicacion")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inventario_Resumen_xArtxEstado

    Public Function Inventario_Resumen_xArtxEstado(ByVal psConexion As String, ByVal pCodEmpresa As String,
                                                          ByVal pCodInventario As Double, ByVal pCodUbicacionInv As Double,
                                                          ByVal pCodArt As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Resumen_xArtxEstado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodUbicacionInv", SqlDbType.Float).Value = pCodUbicacionInv
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Resumen_xArtxEstado")
        Da.Fill(Dt)
        Return Dt
    End Function '

    Public Function Inventario_Verificacion_ListaxArticulo(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                           ByVal pCodUbicacionInv As Double, ByVal pCodArticulo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Verificacion_ListaxArt", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodUbicacionInv", SqlDbType.Float).Value = pCodUbicacionInv
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pCodArticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Verificacion_ListaxArt")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inventario_Verificacion_Otros_ListaxArt

    Public Function Inventario_VerificacionOtros_ListaxArticulo(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                           ByVal pCodUbicacionInv As Double, ByVal pCodArticulo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Verificacion_Otros_ListaxArt", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodUbicacionInv", SqlDbType.Float).Value = pCodUbicacionInv
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pCodArticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Verificacion_Otros_ListaxArt")
        Da.Fill(Dt)
        Return Dt
    End Function '[Prc_Inventario_Verificacion_Nuevos_ListaxArt

    Public Function Inventario_VerificacionNuevos_ListaxArticulo(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                           ByVal pCodUbicacionInv As Double, ByVal pCodArticulo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Verificacion_Nuevos_ListaxArt", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodUbicacionInv", SqlDbType.Float).Value = pCodUbicacionInv
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pCodArticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Verificacion_Nuevos_ListaxArt")
        Da.Fill(Dt)
        Return Dt
    End Function '[

    Public Function Inventario_ListaNoInventariado(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                   ByVal psCodInvUbica As Double, ByVal pdCodArt As Double, ByVal psNombreArt As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Inventario_Verificacion_NoInventariados", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.VarChar).Value = pCodInventario
        Cmd.Parameters.Add("@CodInvUbic", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@ArtNombre", SqlDbType.VarChar).Value = psNombreArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Inventario_Verificacion_NoInventariados")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_Equipos_Inventariados_xEstado(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                        ByVal psCodInvUbica As Double, ByVal pdCodArt As Double, ByVal psNombreArt As String, ByVal psCodEstado As String, ByVal psTipoUbica As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Lista_Inventario_Verificacion_xEstado]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInvUbic", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@ArtNombre", SqlDbType.VarChar).Value = psNombreArt
        Cmd.Parameters.Add("@CodEstado", SqlDbType.VarChar).Value = psCodEstado
        Cmd.Parameters.Add("@TipoUbica", SqlDbType.VarChar).Value = psTipoUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Lista_Inventario_Verificacion_xEstado]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventariados_Bienes_xPlaca(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal psCodInvUbica As Double,
                                                 ByVal psUbicacion As String, ByVal psTipoUbica As String, pPlacaNro As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Inventario_Informe_xPlaca]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Cod_Inventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar).Value = psUbicacion
        Cmd.Parameters.Add("@TipoUbica", SqlDbType.VarChar).Value = psTipoUbica
        Cmd.Parameters.Add("@PlacaNro", SqlDbType.Float).Value = pPlacaNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Inventario_Informe_xPlaca]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventariados_Bienes_xPlacar(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal psCodInvUbica As Double,
                                                 ByVal psUbicacion As String, ByVal psTipoUbica As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Inventario_Informe_BienesxPlacar]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Cod_Inventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@Ubicacion", SqlDbType.VarChar).Value = psUbicacion
        Cmd.Parameters.Add("@TipoUbica", SqlDbType.VarChar).Value = psTipoUbica
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Inventario_Informe_BienesxPlacar]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Equipos_Inventariados_xEstadoExportar(ByVal psConexion As String, ByVal pCodInventario As Double,
                                                        ByVal psCodInvUbica As Double, ByVal pdCodArt As Double, ByVal psNombreArt As String, ByVal psCodEstado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("[Prc_Lista_Inventario_Verificacion_ExportarxEstado]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInvUbic", SqlDbType.Float).Value = psCodInvUbica
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@ArtNombre", SqlDbType.VarChar).Value = psNombreArt
        Cmd.Parameters.Add("@CodEstado", SqlDbType.VarChar).Value = psCodEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Lista_Inventario_Verificacion_ExportarxEstado]")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Inventario_Verificacion_Nuevos(ByVal psConexion As String, ByVal codigo As Double,
                                                  ByVal tipo As String, ByVal ubicacion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Lista_Inventario_Verificacion_Nuevos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_INVENTARIO", codigo)
        Cmd.Parameters.AddWithValue("@TIPO", tipo)
        Cmd.Parameters.AddWithValue("@UBICACION", ubicacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Lista_Inventario_Verificacion_Nuevos")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Inventario_BienesOtros_Paraconciliar(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInvUbica As Double,
                                                         ByVal pUbicatipo As String, ByVal pUbicaCodigo As Double, ByVal pdCodarticulo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Conciliar_ListaBienes_Inventariado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInvUbica
        Cmd.Parameters.Add("@OficinaTipo", SqlDbType.VarChar).Value = pUbicatipo
        Cmd.Parameters.Add("@OficinaCodigo", SqlDbType.VarChar).Value = pUbicaCodigo
        Cmd.Parameters.Add("@codArticulo", SqlDbType.Float).Value = pdCodarticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Conciliar_ListaBienes_Inventariado")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_BienesNuevos(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInvUbica As Double,
                                                  ByVal pUbicatipo As String, ByVal pUbicaCodigo As Double, ByVal pdCodarticulo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Inventario_BienesNuevos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInvUbica
        Cmd.Parameters.Add("@OficinaTipo", SqlDbType.VarChar).Value = pUbicatipo
        Cmd.Parameters.Add("@OficinaCodigo", SqlDbType.VarChar).Value = pUbicaCodigo
        Cmd.Parameters.Add("@codArticulo", SqlDbType.Float).Value = pdCodarticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Inventario_BienesNuevos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_BienesNoconsiderados(ByVal psConexion As String, ByVal pCodInventario As Double, ByVal pCodInvUbica As Double,
                                                  ByVal pUbicatipo As String, ByVal pUbicaCodigo As Double, ByVal pdCodarticulo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_Inventario_BienesNoconsiderados", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pCodInventario
        Cmd.Parameters.Add("@CodInv_Ubica", SqlDbType.Float).Value = pCodInvUbica
        Cmd.Parameters.Add("@OficinaTipo", SqlDbType.VarChar).Value = pUbicatipo
        Cmd.Parameters.Add("@OficinaCodigo", SqlDbType.VarChar).Value = pUbicaCodigo '@codArticulo
        Cmd.Parameters.Add("@codArticulo", SqlDbType.Float).Value = pdCodarticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Inventario_BienesNoconsiderados")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Inventario_Verificacion_Otros(ByVal psConexion As String, ByVal codigo As String,
                                                  ByVal tipo As String, ByVal ubicacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Lista_Inventario_Verificacion_Otros", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@COD_INVENTARIO", codigo)
        Cmd.Parameters.AddWithValue("@TIPO", tipo)
        Cmd.Parameters.AddWithValue("@UBICACION", ubicacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Lista_Inventario_Verificacion_Otros")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Almacenes_Inventario_Verificacion(ByVal psConexion As String, ByVal inventario As Double,
                                               ByVal codigo As Double, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Almacenes_Inventario_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INVENTARIO", SqlDbType.Float).Value = inventario
        Cmd.Parameters.Add("@CODIGO", SqlDbType.Float).Value = codigo
        Cmd.Parameters.Add("@DESCRIPCION", SqlDbType.VarChar).Value = descripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Almacenes_Inventario_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_CentroC_Inventario_Verificacion(ByVal psConexion As String, ByVal inventario As Double,
                                               ByVal codigo As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_CentroC_Inventario_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_CentroC_Inventario_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listar_Ubicaciones_Inventario_Verificacion(ByVal psConexion As String, ByVal inventario As String,
                                               ByVal codigo As String, ByVal descripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Ubicaciones_Inventario_Verificacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@CODIGO", codigo)
        Cmd.Parameters.AddWithValue("@DESCRIPCION", descripcion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Ubicaciones_Inventario_Verificacion")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Combo_Inventario(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Combo_Estado(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Estado", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Estado")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Llenar_Combo_Personal(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Llenar_Combo_Personal", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Llenar_Combo_Personal")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Verificacion_Articulos_Inventario(ByVal psConexion As String, ByVal inventario As String,
                                               ByVal placa As String, ByVal serie As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Verificacion_Articulos_Inventario", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@NROPLACA", placa)
        Cmd.Parameters.AddWithValue("@NROSERIE", serie)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Verificacion_Articulos_Inventario")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Agregar_Articulo_Detalle(ByVal psConexion As String, ByVal inventario As String,
                                               ByVal placa As String, ByVal serie As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Articulo_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@NROPLACA", placa)
        Cmd.Parameters.AddWithValue("@NROSERIE", serie)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Agregar_Articulo_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Buscar_Serie_Numerar(ByVal psConexion As String, ByVal placa As Double,
                                                  ByVal serie As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Buscar_Serie_Numerar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NRO_PLACA", SqlDbType.Float).Value = placa
        Cmd.Parameters.Add("@NRO_SERIE", SqlDbType.VarChar).Value = serie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Buscar_Serie_Numerar")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_ListaPendiente_CargaMasiva(ByVal psConexion As String, ByVal pd_IncCodUbica As Double, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_CargaMasiva_Pendiente", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInvUbica", SqlDbType.Float).Value = pd_IncCodUbica '@tipo
        Cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_CargaMasiva_Pendiente")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Cargar_Datos_Bien(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal numerar As Double,
                                      ByVal psArtDescripcion As String, ByVal psSerieNro As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_VERIFICAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@ArtDescripcion", SqlDbType.VarChar).Value = psArtDescripcion
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.Float).Value = numerar
        Cmd.Parameters.Add("@SerieNRO", SqlDbType.VarChar).Value = psSerieNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_VERIFICAR")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Cargar_Articulos1(ByVal psConexion As String, ByVal numerar As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Listar_Articulos_Tabla1", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@NUMERAR", numerar)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Listar_Articulos_Tabla1")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Agregar_Articulos(ByVal psConexion As String, ByVal inventario As String,
                                        ByVal numerar As String, ByVal placa As String,
                                        ByVal serie As String, ByVal codRelacionado As String,
                                        ByVal estado As String, ByVal responsable As String,
                                        ByVal codArticulo As String, ByVal tipoArticulo As String,
                                        ByVal custodia As String, ByVal tipo As String,
                                        ByVal codArea As String, ByVal observacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Agregar_Inventario_Articulo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@INVENTARIO", inventario)
        Cmd.Parameters.AddWithValue("@SERIE_NUMERAR", numerar)
        Cmd.Parameters.AddWithValue("@NRO_PLACA", placa)
        Cmd.Parameters.AddWithValue("@NRO_SERIE", serie)
        Cmd.Parameters.AddWithValue("@COD_RELACIONADO", codRelacionado)
        Cmd.Parameters.AddWithValue("@EST_EQUIPO", estado)
        Cmd.Parameters.AddWithValue("@SERIE_RESPONSABLE", responsable)
        Cmd.Parameters.AddWithValue("@ART_CODIGO", codArticulo)
        Cmd.Parameters.AddWithValue("@ART_TIPO", tipoArticulo)
        Cmd.Parameters.AddWithValue("@SERIE_CUST_CCOSTO", custodia)
        Cmd.Parameters.AddWithValue("@SERIE_CUST_TIPO", tipo)
        Cmd.Parameters.AddWithValue("@SERIE_AREA", codArea)
        Cmd.Parameters.AddWithValue("@SERIE_RESP_OBS", observacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Agregar_Inventario_Articulo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Actualizar_Articulos(ByVal psConexion As String, ByVal numerar As String,
                                        ByVal placa As String, ByVal serie As String,
                                        ByVal codRelacionado As String, ByVal estado As String,
                                        ByVal responsable As String, ByVal codArticulo As String,
                                        ByVal custodia As String, ByVal tipo As String,
                                        ByVal codArea As String, ByVal observacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Proc_Actualizar_Articulos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@SERIE_NUMERAR", numerar)
        Cmd.Parameters.AddWithValue("@NRO_PLACA", placa)
        Cmd.Parameters.AddWithValue("@NRO_SERIE", serie)
        Cmd.Parameters.AddWithValue("@COD_RELACIONADO", codRelacionado)
        Cmd.Parameters.AddWithValue("@EST_EQUIPO", estado)
        Cmd.Parameters.AddWithValue("@SERIE_RESPONSABLE", responsable)
        Cmd.Parameters.AddWithValue("@ART_CODIGO", codArticulo)
        Cmd.Parameters.AddWithValue("@SERIE_CUST_CCOSTO", custodia)
        Cmd.Parameters.AddWithValue("@SERIE_CUST_TIPO", tipo)
        Cmd.Parameters.AddWithValue("@SERIE_AREA", codArea)
        Cmd.Parameters.AddWithValue("@SERIE_RESP_OBS", observacion)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Proc_Actualizar_Articulos")
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
            CmdGlobal.CommandText = "SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_0001"
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