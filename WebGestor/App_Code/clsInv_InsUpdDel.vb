Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class clsInv_InsUpdDel
    Public Function Ins_Articulos_Series_Ubic(ByVal Conexion As String, ByVal SERIE_NUMERAR As Double,
                                              ByVal UBIC_TIPO As String, ByVal UBIC_CODIGO As Double, ByVal ESTADO As String,
                                              ByVal SYS_CRE As String, ByVal INGRESO_FECHA As String, ByVal INGRESO_TIPO As String,
                                              ByVal NRO_ING_SAL As Double, ByVal User As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBINV_ARTICULOS_SERIES_UBIC", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = SERIE_NUMERAR
        Cmd.Parameters.Add("@UBIC_TIPO", SqlDbType.VarChar).Value = UBIC_TIPO
        Cmd.Parameters.Add("@UBIC_CODIGO", SqlDbType.Float).Value = UBIC_CODIGO
        Cmd.Parameters.Add("@ESTADO", SqlDbType.VarChar).Value = ESTADO
        Cmd.Parameters.Add("@SYS_CRE", SqlDbType.VarChar).Value = SYS_CRE
        Cmd.Parameters.Add("@INGRESO_FECHA", SqlDbType.VarChar).Value = INGRESO_FECHA
        Cmd.Parameters.Add("@INGRESO_TIPO", SqlDbType.VarChar).Value = INGRESO_TIPO
        Cmd.Parameters.Add("@NRO_ING_SAL", SqlDbType.Float).Value = NRO_ING_SAL
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = User
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBINV_ARTICULOS_SERIES_UBIC")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GuardarImagen(ByVal psConexion As String, ByVal Codigo As String,
                                      ByVal img As Byte(), ByVal nomImg As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PROC_INV_UPD_ARTICULOS_IMAGEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", Codigo)
        Cmd.Parameters.AddWithValue("@NOM_IMAGEN", nomImg)
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PROC_INV_UPD_ARTICULOS_IMAGEN")
        Da.Fill(Dt)
        Return Dt
    End Function '


    Public Function GuardarImagenGuia(ByVal psConexion As String, ByVal Codigo As String,
                                      ByVal img As Byte(), ByVal nomImg As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_INV_UPD_GUIA_IMAGEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", Codigo)
        Cmd.Parameters.AddWithValue("@NOM_IMAGEN", nomImg)
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_UPD_GUIA_IMAGEN")
        Da.Fill(Dt)
        Return Dt
    End Function '[PRC_INV_UPD_RECEPCION_IMAGENOC]
    Public Function GuardarImagenRecep_OC(ByVal psConexion As String, ByVal Codigo As String,
                                      ByVal img As Byte(), ByVal nomImg As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_INV_UPD_RECEPCION_IMAGENOC", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", Codigo)
        Cmd.Parameters.AddWithValue("@NOM_IMAGEN", nomImg)
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_UPD_RECEPCION_IMAGENOC")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function GuardarImagenRecep_Guia(ByVal psConexion As String, ByVal Codigo As String,
                                      ByVal img As Byte(), ByVal nomImg As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_INV_UPD_RECEPCION_IMAGENGUIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.AddWithValue("@CODIGO", Codigo)
        Cmd.Parameters.AddWithValue("@NOM_IMAGEN", nomImg)
        Dim imageParam As SqlParameter = Cmd.Parameters.Add("@IMAGEN", System.Data.SqlDbType.Image)
        imageParam.Value = img
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_UPD_RECEPCION_IMAGENGUIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Movimiento_General(ByVal Conexion As String, ByVal EMPRESA_CODIGO As String,
                                           ByVal MOV_NRO As Double, ByVal MOV_TIPO As String, ByVal TIPO_UBICACT As String,
                                           ByVal CODIGO_UBICACT As Double, ByVal CODIGO_ARTICULO As Double, ByVal NRO_ARTICULO As Double,
                                           ByVal MOV_SYS_CRE As String, ByVal MOV_ESTADO As String, ByVal MOV_MOTIVO As String,
                                           ByVal MOV_FECHA As String, ByVal CODIGO_TRANS As Double, ByVal User As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBINV_MOVIMIENTO_GENERAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EMPRESA_CODIGO
        Cmd.Parameters.Add("@MOV_NRO", SqlDbType.Float).Value = MOV_NRO
        Cmd.Parameters.Add("@MOV_TIPO", SqlDbType.VarChar).Value = MOV_TIPO
        Cmd.Parameters.Add("@TIPO_UBICACT", SqlDbType.VarChar).Value = TIPO_UBICACT
        Cmd.Parameters.Add("@CODIGO_UBICACT", SqlDbType.Float).Value = CODIGO_UBICACT
        Cmd.Parameters.Add("@CODIGO_ARTICULO", SqlDbType.Float).Value = CODIGO_ARTICULO
        Cmd.Parameters.Add("@NRO_ARTICULO", SqlDbType.Float).Value = NRO_ARTICULO
        Cmd.Parameters.Add("@MOV_SYS_CRE", SqlDbType.VarChar).Value = MOV_SYS_CRE
        Cmd.Parameters.Add("@MOV_ESTADO", SqlDbType.VarChar).Value = MOV_ESTADO
        Cmd.Parameters.Add("@MOV_MOTIVO", SqlDbType.VarChar).Value = MOV_MOTIVO
        Cmd.Parameters.Add("@MOV_FECHA", SqlDbType.VarChar).Value = MOV_FECHA
        Cmd.Parameters.Add("@CODIGO_TRANS", SqlDbType.Float).Value = CODIGO_TRANS
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = User
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBINV_MOVIMIENTO_GENERAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Articulos_Almacen(ByVal Conexion As String, ByVal EMPRESA_CODIGO As String,
                                          ByVal ALMACEN_CODIGO As Double, ByVal UBICACT_TIPO As String, ByVal ARTICULO_CODIGO As Double,
                                          ByVal SAA_STOCK_ACTUAL As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBINV_STOCK_ARTICULOS_ALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EMPRESA_CODIGO
        Cmd.Parameters.Add("@ALMACEN_CODIGO", SqlDbType.Float).Value = ALMACEN_CODIGO
        Cmd.Parameters.Add("@UBICACT_TIPO", SqlDbType.VarChar).Value = UBICACT_TIPO
        Cmd.Parameters.Add("@ARTICULO_CODIGO", SqlDbType.Float).Value = ARTICULO_CODIGO
        Cmd.Parameters.Add("@SAA_STOCK_ACTUAL", SqlDbType.Float).Value = SAA_STOCK_ACTUAL
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBINV_STOCK_ARTICULOS_ALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Articulos_Series(ByVal Conexion As String, ByVal SERIE_NUMERAR As Double,
                                         ByVal RECEP_CODIGO As Double, ByVal ARTICULO_CODIGO As Double, ByVal SERIE_NRO As String, ByVal SERIE_SOBRANTE As String,
                                         ByVal PLACA_NRO As Double, ByVal UBICACT_TIPO As String, ByVal UBICACT_CODIGO As Double, ByVal UBICACT_SYS As String,
                                         ByVal SERIE_SYS_CRE As String, ByVal SERIE_NUEVO As String, ByVal ALTIBI_CODIGO As Double, ByVal SERIE_INGRESO As String,
                                         ByVal PROVEEDOR As Double, ByVal SERIE_ESTADO As String, ByVal ESTADO_EQUIPO As String, ByVal ESTADO_BATERIA As String,
                                         ByVal User As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBINV_ARTICULOS_SERIES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = SERIE_NUMERAR
        Cmd.Parameters.Add("@RECEP_CODIGO", SqlDbType.Float).Value = RECEP_CODIGO
        Cmd.Parameters.Add("@ARTICULO_CODIGO", SqlDbType.Float).Value = ARTICULO_CODIGO
        Cmd.Parameters.Add("@SERIE_NRO", SqlDbType.VarChar).Value = SERIE_NRO
        Cmd.Parameters.Add("@SERIE_SOBRANTE", SqlDbType.VarChar).Value = SERIE_SOBRANTE
        Cmd.Parameters.Add("@PLACA_NRO", SqlDbType.Float).Value = PLACA_NRO
        Cmd.Parameters.Add("@UBICACT_TIPO", SqlDbType.VarChar).Value = UBICACT_TIPO
        Cmd.Parameters.Add("@UBICACT_CODIGO", SqlDbType.Float).Value = UBICACT_CODIGO
        Cmd.Parameters.Add("@UBICACT_SYS", SqlDbType.VarChar).Value = UBICACT_SYS
        Cmd.Parameters.Add("@SERIE_SYS_CRE", SqlDbType.VarChar).Value = SERIE_SYS_CRE
        Cmd.Parameters.Add("@SERIE_NUEVO", SqlDbType.VarChar).Value = SERIE_NUEVO
        Cmd.Parameters.Add("@ALTIBI_CODIGO", SqlDbType.Float).Value = ALTIBI_CODIGO
        Cmd.Parameters.Add("@SERIE_INGRESO", SqlDbType.VarChar).Value = SERIE_INGRESO
        Cmd.Parameters.Add("@PROVEEDOR", SqlDbType.Float).Value = PROVEEDOR
        Cmd.Parameters.Add("@SERIE_ESTADO", SqlDbType.VarChar).Value = SERIE_ESTADO
        Cmd.Parameters.Add("@ESTADO_EQUIPO", SqlDbType.VarChar).Value = ESTADO_EQUIPO
        Cmd.Parameters.Add("@ESTADO_BATERIA", SqlDbType.VarChar).Value = ESTADO_BATERIA
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = User
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBINV_ARTICULOS_SERIES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Almacen_Recepcion_Det(ByVal Conexion As String, ByVal EmpresaCod As String,
                                              ByVal RECEP_CODIGO As Double, ByVal RECEPD_ITEM As Double, ByVal ARTICULO_CODIGO As Double,
                                              ByVal RECEPD_CANT_XREC As Double, ByVal RECEPD_CANT_REC As Double, ByVal RECEPD_CANT_FALT_REC As Double, ByVal RECEPD_CANT_SOBR As Double,
                                              ByVal RECEPD_ESTADO As String, ByVal RECEPD_MOTIVO As String, ByVal RECEPD_INGRESAR_SERIE As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBINV_ALMACEN_RECEPCION_DET", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EmpresaCod
        Cmd.Parameters.Add("@RECEP_CODIGO", SqlDbType.Float).Value = RECEP_CODIGO
        Cmd.Parameters.Add("@RECEPD_ITEM", SqlDbType.Float).Value = RECEPD_ITEM
        Cmd.Parameters.Add("@ARTICULO_CODIGO", SqlDbType.Float).Value = ARTICULO_CODIGO
        Cmd.Parameters.Add("@RECEPD_CANT_XREC", SqlDbType.Float).Value = RECEPD_CANT_XREC
        Cmd.Parameters.Add("@RECEPD_CANT_REC", SqlDbType.Float).Value = RECEPD_CANT_REC
        Cmd.Parameters.Add("@RECEPD_CANT_FALT_REC", SqlDbType.Float).Value = RECEPD_CANT_FALT_REC
        Cmd.Parameters.Add("@RECEPD_CANT_SOBR", SqlDbType.Float).Value = RECEPD_CANT_SOBR
        Cmd.Parameters.Add("@RECEPD_ESTADO", SqlDbType.VarChar).Value = RECEPD_ESTADO
        Cmd.Parameters.Add("@RECEPD_MOTIVO", SqlDbType.VarChar).Value = RECEPD_MOTIVO
        Cmd.Parameters.Add("@RECEPD_INGRESAR_SERIE", SqlDbType.VarChar).Value = RECEPD_INGRESAR_SERIE
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBINV_ALMACEN_RECEPCION_DET")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Almacen_Recepcion(ByVal Conexion As String, ByVal EmpresaCod As String,
                                          ByVal RECEP_CODIGO As Double, ByVal RECEP_TIPODESTINO As Double, ByVal ALMACEN_CODIGO As Double, ByVal RECEP_PROVEEDOR As Double,
                                          ByVal RECEP_ESTADO As String, ByVal RECEP_SYS_CRE As String, ByVal RECEP_MOTIVO_GRAL As String,
                                          ByVal ALTIBI_CODIGO As Double, ByVal RECEP_ESTADO_CEPRO As String, ByVal RECEP_FEC_EMI_DOC As String,
                                          ByVal RECEP_FECHA_REG As String, ByVal User As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_INS_TBINV_ALMACEN_RECEPCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EmpresaCod
        Cmd.Parameters.Add("@RECEP_CODIGO", SqlDbType.Float).Value = RECEP_CODIGO
        Cmd.Parameters.Add("@RECEP_TIPODESTINO", SqlDbType.VarChar).Value = RECEP_TIPODESTINO
        Cmd.Parameters.Add("@ALMACEN_CODIGO", SqlDbType.Float).Value = ALMACEN_CODIGO
        Cmd.Parameters.Add("@RECEP_PROVEEDOR", SqlDbType.Float).Value = RECEP_PROVEEDOR
        Cmd.Parameters.Add("@RECEP_ESTADO", SqlDbType.VarChar).Value = RECEP_ESTADO
        Cmd.Parameters.Add("@RECEP_SYS_CRE", SqlDbType.VarChar).Value = RECEP_SYS_CRE
        Cmd.Parameters.Add("@RECEP_MOTIVO_GRAL", SqlDbType.VarChar).Value = RECEP_MOTIVO_GRAL
        Cmd.Parameters.Add("@ALTIBI_CODIGO", SqlDbType.Float).Value = ALTIBI_CODIGO
        Cmd.Parameters.Add("@RECEP_ESTADO_CEPRO", SqlDbType.VarChar).Value = RECEP_ESTADO_CEPRO
        Cmd.Parameters.Add("@RECEP_FEC_EMI_DOC", SqlDbType.VarChar).Value = RECEP_FEC_EMI_DOC
        Cmd.Parameters.Add("@RECEP_FECHA_REG", SqlDbType.VarChar).Value = RECEP_FECHA_REG
        Cmd.Parameters.Add("@USER", SqlDbType.VarChar).Value = User
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBINV_ALMACEN_RECEPCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Movimiento(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                   ByVal pCodDevolucion As Double, ByVal pCodArticulo As Double,
                                   ByVal pCodDestino As Double, ByVal pTipoOrigen As String,
                                   ByVal pCodOrigen As Double, ByVal pUser As String,
                                   ByVal pCantidad As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_MOVIMIENTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pCodArticulo
        Cmd.Parameters.Add("@CodDestino", SqlDbType.Float).Value = pCodDestino
        Cmd.Parameters.Add("@TipoOrigen", SqlDbType.VarChar).Value = pTipoOrigen
        Cmd.Parameters.Add("@CodOrigen", SqlDbType.Float).Value = pCodOrigen
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@Cantidad", SqlDbType.Float).Value = pCantidad
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_MOVIMIENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Articulo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                 ByVal psUser As String, ByVal pdCodArticulo As Double,
                                 ByVal psDescripcion As String, ByVal psNroParte As String,
                                 ByVal pdClasif As Double, ByVal pdTipo As Double,
                                 ByVal pdUnidad As Double, ByVal pdMarca As Double,
                                 ByVal pdModelo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_ARTICULO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pdCodArticulo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Cmd.Parameters.Add("@NroParte", SqlDbType.VarChar).Value = psNroParte
        Cmd.Parameters.Add("@Clasif", SqlDbType.Float).Value = pdClasif
        Cmd.Parameters.Add("@Tipo", SqlDbType.Float).Value = pdTipo
        Cmd.Parameters.Add("@Unidad", SqlDbType.Float).Value = pdUnidad
        Cmd.Parameters.Add("@Marca", SqlDbType.Float).Value = pdMarca
        Cmd.Parameters.Add("@Modelo", SqlDbType.Float).Value = pdModelo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_ARTICULO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Marca(ByVal Conexion As String, ByVal pCodEmpresa As String,
                              ByVal psUser As String, ByVal psDescripcion As String,
                              ByVal pdCodClasif As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_MARCA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Cmd.Parameters.Add("@CodClas", SqlDbType.Float).Value = pdCodClasif
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_MARCA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Modelo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                               ByVal psUser As String, ByVal psDescripcion As String,
                               ByVal pdCodClasif As Double, ByVal pdCodMarca As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_MODELO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Cmd.Parameters.Add("@CodClas", SqlDbType.Float).Value = pdCodClasif
        Cmd.Parameters.Add("@CodMarca", SqlDbType.Float).Value = pdCodMarca
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_MODELO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_PersonaFecha(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                     ByVal pCodPersona As Double, ByVal pFechaEntrega As String,
                                     ByVal pNroPedido As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_PERFECHA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodPer", SqlDbType.Float).Value = pCodPersona
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Cmd.Parameters.Add("@NroPedido", SqlDbType.VarChar).Value = pNroPedido
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_PERFECHA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_GuiaRemiArchivo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pCodGuia As Double, ByVal pNombArchivo As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_GUIREMARCHIVO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Cmd.Parameters.Add("@NombArchivo", SqlDbType.VarChar).Value = pNombArchivo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_GUIREMARCHIVO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Series(ByVal Conexion As String, ByVal pCodEmpresa As String,
                               ByVal pCodSerieNumerar As Double, ByVal pCodRecepcion As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_BORRAR_SERIES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.Float).Value = pCodSerieNumerar
        Cmd.Parameters.Add("@CodRecep", SqlDbType.Float).Value = pCodRecepcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_BORRAR_SERIES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_AlmacenxUsuario(ByVal Conexion As String, ByVal pCodAlmacen As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_ALMACENXUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@AlmCodigo", SqlDbType.Float).Value = pCodAlmacen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_ALMACENXUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_PedArchivo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                   ByVal pCodPedido As Double, ByVal pTipoArchivo As String,
                                   ByVal pNombArchivo As String, ByVal pArchDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_PEDIDOARCHIVO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPedido", SqlDbType.Float).Value = pCodPedido
        Cmd.Parameters.Add("@TipoArchivo", SqlDbType.VarChar).Value = pTipoArchivo
        Cmd.Parameters.Add("@NombArchivo", SqlDbType.VarChar).Value = pNombArchivo
        Cmd.Parameters.Add("@ArchDescripcion", SqlDbType.VarChar).Value = pArchDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_PEDIDOARCHIVO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Telefonica_Agenda(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                          ByVal pARCHIVO_FECHA As String, ByVal pARCHIVO_NROPEDIDO As String,
                                          ByVal pARCHIVO_FRANJAHORARIA As String, ByVal pARCHIVO_FECHA_AGENDA As String,
                                          ByVal pARCHIVO_DOCTIPO As String, ByVal pARCHIVO_DOCNRO As String,
                                          ByVal pARCHIVO_ZONAL As String, ByVal pARCHIVO_CIUDADZONAL As String,
                                          ByVal pARCHIVO_CLIENTE As String, ByVal pARCHIVO_PRODUCTO As String,
                                          ByVal pARCHIVO_PAQUETE As String, ByVal pARCHIVO_TELEFONO_FIJO As String,
                                          ByVal pARCHIVO_ZONA_CIUDAD As String, ByVal pARCHIVO_DISTRITO As String,
                                          ByVal pARCHIVO_DIRECCION As String, ByVal pARCHIVO_REFERENCIA As String,
                                          ByVal pARCHIVO_CORREO As String, ByVal pARCHIVO_PERSONA_CONTACTO As String,
                                          ByVal pARCHIVO_TELEFONO_CONTACTO As String, ByVal pARCHIVO_CELULAR_CONTACTO As String,
                                          ByVal pARCHIVO_TIPOPRODUCTO As String, ByVal pARCHIVO_NOMBRE_PAQUETE As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_CARGA_AGENDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@ARCHIVO_FECHA", SqlDbType.VarChar).Value = pARCHIVO_FECHA
        Cmd.Parameters.Add("@ARCHIVO_NROPEDIDO", SqlDbType.VarChar).Value = pARCHIVO_NROPEDIDO
        Cmd.Parameters.Add("@ARCHIVO_FRANJAHORARIA", SqlDbType.VarChar).Value = pARCHIVO_FRANJAHORARIA
        Cmd.Parameters.Add("@ARCHIVO_FECHA_AGENDA", SqlDbType.VarChar).Value = pARCHIVO_FECHA_AGENDA
        Cmd.Parameters.Add("@ARCHIVO_DOCTIPO", SqlDbType.VarChar).Value = pARCHIVO_DOCTIPO
        Cmd.Parameters.Add("@ARCHIVO_DOCNRO", SqlDbType.VarChar).Value = pARCHIVO_DOCNRO
        Cmd.Parameters.Add("@ARCHIVO_ZONAL", SqlDbType.VarChar).Value = pARCHIVO_ZONAL
        Cmd.Parameters.Add("@ARCHIVO_CIUDADZONAL", SqlDbType.VarChar).Value = pARCHIVO_CIUDADZONAL
        Cmd.Parameters.Add("@ARCHIVO_CLIENTE", SqlDbType.VarChar).Value = pARCHIVO_CLIENTE
        Cmd.Parameters.Add("@ARCHIVO_PRODUCTO", SqlDbType.VarChar).Value = pARCHIVO_PRODUCTO
        Cmd.Parameters.Add("@ARCHIVO_PAQUETE", SqlDbType.VarChar).Value = pARCHIVO_PAQUETE
        Cmd.Parameters.Add("@ARCHIVO_TELEFONO_FIJO", SqlDbType.VarChar).Value = pARCHIVO_TELEFONO_FIJO
        Cmd.Parameters.Add("@ARCHIVO_ZONA_CIUDAD", SqlDbType.VarChar).Value = pARCHIVO_ZONA_CIUDAD
        Cmd.Parameters.Add("@ARCHIVO_DISTRITO", SqlDbType.VarChar).Value = pARCHIVO_DISTRITO
        Cmd.Parameters.Add("@ARCHIVO_DIRECCION", SqlDbType.VarChar).Value = pARCHIVO_DIRECCION
        Cmd.Parameters.Add("@ARCHIVO_REFERENCIA", SqlDbType.VarChar).Value = pARCHIVO_REFERENCIA
        Cmd.Parameters.Add("@ARCHIVO_CORREO", SqlDbType.VarChar).Value = pARCHIVO_CORREO
        Cmd.Parameters.Add("@ARCHIVO_PERSONA_CONTACTO", SqlDbType.VarChar).Value = pARCHIVO_PERSONA_CONTACTO
        Cmd.Parameters.Add("@ARCHIVO_TELEFONO_CONTACTO", SqlDbType.VarChar).Value = pARCHIVO_TELEFONO_CONTACTO
        Cmd.Parameters.Add("@ARCHIVO_CELULAR_CONTACTO", SqlDbType.VarChar).Value = pARCHIVO_CELULAR_CONTACTO
        Cmd.Parameters.Add("@ARCHIVO_TIPOPRODUCTO", SqlDbType.VarChar).Value = pARCHIVO_TIPOPRODUCTO
        Cmd.Parameters.Add("@ARCHIVO_NOMBRE_PAQUETE", SqlDbType.VarChar).Value = pARCHIVO_NOMBRE_PAQUETE
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_CARGA_AGENDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Telefonica_Personas(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                            ByVal pCodPersona As Double, ByVal pTipoDoc As String,
                                            ByVal pNroDoc As String, ByVal pCliente As String,
                                            ByVal pDireccion As String, ByVal pProv As String,
                                            ByVal pDist As String, ByVal pTelefFijo As String,
                                            ByVal pTelef2 As String, ByVal pContacto As String,
                                            ByVal pReferencia As String, ByVal pTelef3 As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_PERSONAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPersona", SqlDbType.Float).Value = pCodPersona
        Cmd.Parameters.Add("@TipoDoc", SqlDbType.VarChar).Value = pTipoDoc
        Cmd.Parameters.Add("@NroDoc", SqlDbType.VarChar).Value = pNroDoc
        Cmd.Parameters.Add("@Cliente", SqlDbType.VarChar).Value = pCliente
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = pDireccion
        Cmd.Parameters.Add("@Prov", SqlDbType.VarChar).Value = pProv
        Cmd.Parameters.Add("@Dist", SqlDbType.VarChar).Value = pDist
        Cmd.Parameters.Add("@TelefFijo", SqlDbType.VarChar).Value = pTelefFijo
        Cmd.Parameters.Add("@Telef2", SqlDbType.VarChar).Value = pTelef2
        Cmd.Parameters.Add("@Contacto", SqlDbType.VarChar).Value = pContacto
        Cmd.Parameters.Add("@Referencia", SqlDbType.VarChar).Value = pReferencia
        Cmd.Parameters.Add("@Telef3", SqlDbType.VarChar).Value = pTelef3
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_PERSONAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Telefonica_Pedido(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                          ByVal pCodPersona As Double, ByVal pNroPedido As String,
                                          ByVal pFecEntrega As String, ByVal pContacto As String,
                                          ByVal pReferencia As String, ByVal pTelefFijo As String,
                                          ByVal pTelef2 As String, ByVal pCantEq As Double,
                                          ByVal pFranjaHoraria As String, ByVal pUser As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_PEDIDO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPersona", SqlDbType.Float).Value = pCodPersona
        Cmd.Parameters.Add("@NroPedido", SqlDbType.VarChar).Value = pNroPedido
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFecEntrega
        Cmd.Parameters.Add("@Contacto", SqlDbType.VarChar).Value = pContacto
        Cmd.Parameters.Add("@Referencia", SqlDbType.VarChar).Value = pReferencia
        Cmd.Parameters.Add("@TelefFijo", SqlDbType.VarChar).Value = pTelefFijo
        Cmd.Parameters.Add("@Telef2", SqlDbType.VarChar).Value = pTelef2
        Cmd.Parameters.Add("@CantEq", SqlDbType.Float).Value = pCantEq
        Cmd.Parameters.Add("@FranjaHoraria", SqlDbType.VarChar).Value = pFranjaHoraria
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_PEDIDO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Devolucion(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                   ByVal pCodDevolucion As Double, ByVal pFechaDev As String,
                                   ByVal pHoraDev As String, ByVal pUser As String,
                                   ByVal pCodDestino As Double, ByVal pTipoOrigen As String,
                                   ByVal pCodOrigen As Double, ByVal pCantidad As Double,
                                   ByVal pObs As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_DEVOLUCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@FechaDev", SqlDbType.VarChar).Value = pFechaDev
        Cmd.Parameters.Add("@HoraDev", SqlDbType.VarChar).Value = pHoraDev
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@CodDestino", SqlDbType.Float).Value = pCodDestino
        Cmd.Parameters.Add("@TipoOrigen", SqlDbType.VarChar).Value = pTipoOrigen
        Cmd.Parameters.Add("@CodOrigen", SqlDbType.Float).Value = pCodOrigen
        Cmd.Parameters.Add("@Cantidad", SqlDbType.Float).Value = pCantidad
        Cmd.Parameters.Add("@Obs", SqlDbType.VarChar).Value = pObs
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_DEVOLUCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Devolucion_Detalle(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodDevolucion As Double, ByVal pCodEquipo As Double,
                                           ByVal pItem As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_DEVOLUCION_DETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@CodEquipo", SqlDbType.Float).Value = pCodEquipo
        Cmd.Parameters.Add("@Item", SqlDbType.Float).Value = pItem
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_DEVOLUCION_DETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Devolucion_Detalle_SinSerie(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                                    ByVal pCodDevolucion As Double, ByVal pCodArt As Double,
                                                    ByVal pItem As Double, ByVal pCant As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INS_DEVOLUCION_DETALLE_SINSERIES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@Item", SqlDbType.Float).Value = pItem
        Cmd.Parameters.Add("@Cant", SqlDbType.Float).Value = pCant
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_DEVOLUCION_DETALLE_SINSERIES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_StockActual(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                       ByVal pCodDevolucion As Double, ByVal pCodArt As Double,
                                       ByVal pCodDestino As Double, ByVal pTipoOrigen As String,
                                       ByVal pCodOrigen As Double, ByVal pCantidad As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INSUPD_STOCKACTUAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@CodDestino", SqlDbType.Float).Value = pCodDestino
        Cmd.Parameters.Add("@TipoOrigen", SqlDbType.VarChar).Value = pTipoOrigen
        Cmd.Parameters.Add("@CodOrigen", SqlDbType.Float).Value = pCodOrigen
        Cmd.Parameters.Add("@Cantidad", SqlDbType.Float).Value = pCantidad
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INSUPD_STOCKACTUAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_UbicEquipo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                      ByVal pCodDevolucion As Double, ByVal pCodEquipo As Double,
                                      ByVal pCodDestino As Double, ByVal pUser As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_INSUPD_UBICACIONEQUIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@CodEquipo", SqlDbType.Float).Value = pCodEquipo
        Cmd.Parameters.Add("@CodDestino", SqlDbType.Float).Value = pCodDestino
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INSUPD_UBICACIONEQUIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Devolucion(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                   ByVal pCodDevolucion As Double, ByVal pCantidad As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_UPD_DEVOLUCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@Cantidad", SqlDbType.Float).Value = pCantidad
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_UPD_DEVOLUCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Salida(ByVal Conexion As String, ByVal pCodEmpresa As String,
                               ByVal pCodDevolucion As Double, ByVal pCodSalida As Double,
                               ByVal pFechaDev As String, ByVal pCodEquipo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_UPD_SALIDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@FechaDev", SqlDbType.VarChar).Value = pFechaDev
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@CodEquipo", SqlDbType.Float).Value = pCodEquipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_UPD_SALIDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Salida_SinSerie(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pCodDevolucion As Double, ByVal pCodSalida As Double,
                                        ByVal pFechaDev As String, ByVal pCodArt As Double,
                                        ByVal pdCant As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_UPD_SALIDA_SINSERIE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDevolucion", SqlDbType.Float).Value = pCodDevolucion
        Cmd.Parameters.Add("@FechaDev", SqlDbType.VarChar).Value = pFechaDev
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Cmd.Parameters.Add("@Cant", SqlDbType.Float).Value = pdCant
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_UPD_SALIDA_SINSERIE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_GuiaRemision(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                     ByVal pCodGuia As Double, ByVal pEstadoEntrega As String,
                                     ByVal pPerAsig As String, ByVal pHora As String,
                                     ByVal pFechaProg As String, ByVal pTipoObs As String,
                                     ByVal pObs As String, ByVal pCodLiquidacion As String,
                                     ByVal pCodPedido As Double, ByVal pFechaReg As String,
                                     ByVal pTipoLlamada As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_UPD_GUIAREMISION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EstadoEntrega", SqlDbType.VarChar).Value = pEstadoEntrega
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Cmd.Parameters.Add("@PerAsignada", SqlDbType.VarChar).Value = pPerAsig
        Cmd.Parameters.Add("@HoraSalida", SqlDbType.VarChar).Value = pHora
        Cmd.Parameters.Add("@FechaProg", SqlDbType.VarChar).Value = pFechaProg
        Cmd.Parameters.Add("@TipoObs", SqlDbType.VarChar).Value = pTipoObs
        Cmd.Parameters.Add("@Obs", SqlDbType.VarChar).Value = pObs
        Cmd.Parameters.Add("@CodLiquidacion", SqlDbType.VarChar).Value = pCodLiquidacion
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPedido", SqlDbType.Float).Value = pCodPedido
        Cmd.Parameters.Add("@FechaReg", SqlDbType.VarChar).Value = pFechaReg
        Cmd.Parameters.Add("@TipoLlamada", SqlDbType.VarChar).Value = pTipoLlamada
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_UPD_GUIAREMISION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_GuiaRemiArchivo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pCodGuia As Double, ByVal pCodArchivo As Double,
                                        ByVal pNombArchivo As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_UPD_GUIREMARCHIVO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Cmd.Parameters.Add("@CodArch", SqlDbType.Float).Value = pCodArchivo
        Cmd.Parameters.Add("@NombArchivo", SqlDbType.VarChar).Value = pNombArchivo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_UPD_GUIREMARCHIVO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_TelefonicaDatos(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pNroPedido As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_UPD_TELEFONICADATOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@NroPedido", SqlDbType.VarChar).Value = pNroPedido
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_UPD_TELEFONICADATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Articulos_Almacen(ByVal Conexion As String, ByVal EMPRESA_CODIGO As String,
                                          ByVal ALMACEN_CODIGO As Double, ByVal UBICACT_TIPO As String, ByVal ARTICULO_CODIGO As Double,
                                          ByVal SAA_STOCK_ACTUAL As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_UPD_TBINV_STOCK_ARTICULOS_ALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EMPRESA_CODIGO
        Cmd.Parameters.Add("@ALMACEN_CODIGO", SqlDbType.Float).Value = ALMACEN_CODIGO
        Cmd.Parameters.Add("@UBICACT_TIPO", SqlDbType.VarChar).Value = UBICACT_TIPO
        Cmd.Parameters.Add("@ARTICULO_CODIGO", SqlDbType.Float).Value = ARTICULO_CODIGO
        Cmd.Parameters.Add("@SAA_STOCK_ACTUAL", SqlDbType.Float).Value = SAA_STOCK_ACTUAL
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_UPD_TBINV_STOCK_ARTICULOS_ALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_AlmacenUsuario(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEL_ALMACENXUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEL_ALMACENXUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_CantAccesorio(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                      ByVal pdCodRecep As Double, ByVal pdCodArt As Double,
                                      ByVal pdCantRec As Double, ByVal pdCantFalta As Double,
                                      ByVal pdCantParcia As Double, ByVal pdCantSob As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_UPD_RECEP_CANTACC", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@CodRecep", SqlDbType.Float).Value = pdCodRecep
        Cmd.Parameters.Add("@CantRec", SqlDbType.Float).Value = pdCantRec
        Cmd.Parameters.Add("@CantFalta", SqlDbType.Float).Value = pdCantFalta
        Cmd.Parameters.Add("@CantParcial", SqlDbType.Float).Value = pdCantParcia
        Cmd.Parameters.Add("@CantSob", SqlDbType.Float).Value = pdCantSob
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_UPD_RECEP_CANTACC")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Salida_Upd_CantAccesorio(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                             ByVal pdCodSalida As Double, ByVal pdCodArt As Double,
                                             ByVal pdCantRec As Double, ByVal pdCantFalta As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Upd_Salida_CantAcc", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pdCodArt
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pdCodSalida
        Cmd.Parameters.Add("@CantRec", SqlDbType.Float).Value = pdCantRec
        Cmd.Parameters.Add("@CantFalta", SqlDbType.Float).Value = pdCantFalta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Upd_Salida_CantAcc")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Upd_Clasificacion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                      ByVal pdCodClasif As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Clasificacion_Eliminar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodClasif", SqlDbType.Float).Value = pdCodClasif
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Clasificacion_Eliminar")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Existe_articulo(ByVal Conexion As String, ByVal pCodEmpresa As String, ByVal pdArtCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Existe_Articulo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Art_Codigo", SqlDbType.Float).Value = pdArtCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Existe_Articulo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsDevolver_UltimaPlaca(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Crear_Placa", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Crear_Placa")
        Da.Fill(Dt)
        Return Dt
    End Function

End Class
