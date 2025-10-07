Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class clsInv_Listados
    Public Sub Llena_TablaInformacion(ByVal Conexion As String, ByVal psCodEmpresa As String,
                              ByVal nTabla As String, ByVal Cbo As DropDownList)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT ELEMENTO_CODUNICO, ELEMENTO_CODIGO, ELEMENTO_DESCRIPCION, TABLA_CODIGO " _
                              & " FROM TBINV_TABLAS_INFO " _
                              & " WHERE ELEMENTO_SYS_EST = '0' " _
                              & " AND TABLA_CODIGO = '" & nTabla & "' " _
                              & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "'ORDER BY ELEMENTO_DESCRIPCION"
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            Cbo.DataSource = cmdSql.ExecuteReader
            Cbo.DataTextField = "ELEMENTO_DESCRIPCION"
            Cbo.DataValueField = "ELEMENTO_CODUNICO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    'Prc_Inventario_ListaCarga
    Public Function Lista_Carga(ByVal psConexion As String, ByVal psMarca As String, ByVal psDenominacion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaCarga", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Marca", SqlDbType.VarChar).Value = psMarca
        Cmd.Parameters.Add("@Denominacion", SqlDbType.VarChar).Value = psDenominacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaCarga")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_Prestamos(ByVal psConexion As String, ByVal pCodEmpresa As String, ByVal pTipoOrigen As String,
                                    ByVal pTipoDestino As String, ByVal pEstadoCod As String, ByVal pSerie_Nro As String,
                                    ByVal pPlacaNro As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_EQPRESTADOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoOrigen", SqlDbType.VarChar).Value = pTipoOrigen
        Cmd.Parameters.Add("@TipoDestino", SqlDbType.VarChar).Value = pTipoDestino
        Cmd.Parameters.Add("@EstadoCod", SqlDbType.VarChar).Value = pEstadoCod
        Cmd.Parameters.Add("@SerieNro", SqlDbType.VarChar).Value = pSerie_Nro
        Cmd.Parameters.Add("@PlacaNro", SqlDbType.VarChar).Value = pPlacaNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_EQPRESTADOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Prestamos_Accesorios(ByVal psConexion As String, ByVal pCodEmpresa As String, ByVal pTipoOrigen As String,
                                               ByVal pTipoDestino As String, ByVal pEstadoCod As String, ByVal pArtDescripcion As String,
                                               ByVal pArtCodigo As String, ByVal pFPrestamoIni As String, ByVal pFPrestamoFin As String,
                                               ByVal pFDevolIni As String, ByVal pFDevolFin As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_ACCPRESTADOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoOrigen", SqlDbType.VarChar).Value = pTipoOrigen
        Cmd.Parameters.Add("@TipoDestino", SqlDbType.VarChar).Value = pTipoDestino
        Cmd.Parameters.Add("@EstadoCod", SqlDbType.VarChar).Value = pEstadoCod
        Cmd.Parameters.Add("@ArtDescripcion", SqlDbType.VarChar).Value = pArtDescripcion
        Cmd.Parameters.Add("@ArtCodigo", SqlDbType.VarChar).Value = pArtCodigo
        Cmd.Parameters.Add("@FPrestamoIni", SqlDbType.VarChar).Value = pFPrestamoIni
        Cmd.Parameters.Add("@FPrestamoFin", SqlDbType.VarChar).Value = pFPrestamoFin
        Cmd.Parameters.Add("@FDevolIni", SqlDbType.VarChar).Value = pFDevolIni
        Cmd.Parameters.Add("@FDevolFin", SqlDbType.VarChar).Value = pFDevolFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_ACCPRESTADOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Pedidos(ByVal psConexion As String, ByVal psTipoPedido As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Pedido_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@TipoPedido", SqlDbType.VarChar).Value = psTipoPedido
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Pedido_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Tabla_movimientos_gps(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_LISTA_TABLA_MOVIMINETOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_LISTA_TABLA_MOVIMINETOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_DatosAdicionales_xBien(ByVal psConexion As String, ByVal pdSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Lista_DatosAdicionales_xBien", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.VarChar).Value = pdSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_DatosAdicionales_xBien")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_BorrarImagenesxBien(ByVal psConexion As String, ByVal pdNroImagen As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_BorrarImagen_xBien", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NroImagen", SqlDbType.Float).Value = pdNroImagen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_BorrarImagen_xBien")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_ListaImagenesxBien(ByVal psConexion As String, ByVal pdSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_ListaImagenesxBien", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Serie_Numerar", SqlDbType.Float).Value = pdSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_ListaImagenesxBien")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Denominacion_Carga(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Sql As String = " SELECT DISTINCT SERIE_DENOMINACION " _
                              & " FROM TBINV_ARTICULOS_SERIES_CARGA_DATOS " _
                              & " ORDER BY SERIE_DENOMINACION"
        Dim cmd As New SqlClient.SqlCommand(Sql, Cn)
        cmd.CommandType = CommandType.Text
        Dim Da As New SqlDataAdapter(cmd)
        Dim Dt As New DataTable("TBINV_ARTICULOS_SERIES_CARGA_DATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Marca_Carga(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Sql As String = " SELECT DISTINCT SERIE_MARCA " _
                              & " FROM TBINV_ARTICULOS_SERIES_CARGA_DATOS " _
                              & " ORDER BY SERIE_MARCA"
        Dim cmd As New SqlClient.SqlCommand(Sql, Cn)
        cmd.CommandType = CommandType.Text
        Dim Da As New SqlDataAdapter(cmd)
        Dim Dt As New DataTable("TBINV_ARTICULOS_SERIES_CARGA_DATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Sub Llena_Marca(ByVal Conexion As String, ByVal psCodEmpresa As String,
                           ByVal psCodClasif As String, ByVal Cbo As DropDownList)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = "SELECT ARTMAR_CODIGO, ARTMAR_DESCRIPCION " _
                              & " FROM TBINV_ARTICULO_MARCA " _
                              & " WHERE ARTMAR_SYS_EST = '0' " _
                              & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "' " _
                              & " AND ARTMAR_CLAS = " & psCodClasif & "  ORDER BY ARTMAR_DESCRIPCION"
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            Cbo.DataSource = cmdSql.ExecuteReader
            Cbo.DataTextField = "ARTMAR_DESCRIPCION"
            Cbo.DataValueField = "ARTMAR_CODIGO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Sub Llena_Almacen(ByVal Conexion As String, ByVal psCodEmpresa As String,
                             ByVal Cbo As DropDownList, ByVal psUser As String)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cbo.DataSource = Lista_AlmacenxUsuario(Conexion, psCodEmpresa, psUser)
            Cbo.DataTextField = "ALMACEN_NOMBRE"
            Cbo.DataValueField = "ALMACEN_CODIGO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Sub Llena_Motivo_Ing(ByVal Conexion As String, ByVal psCodEmpresa As String,
                                ByVal Cbo As DropDownList)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cbo.DataSource = Lista_Motivo_Ing(Conexion, psCodEmpresa)
            Cbo.DataTextField = "MOTIVO_TRASLADO"
            Cbo.DataValueField = "MAINSA_MOTIVO_TRASLADO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Sub Llena_Propietario(ByVal Conexion As String, ByVal psCodEmpresa As String,
                                 ByVal Cbo As DropDownList)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cbo.DataSource = Lista_Propietario(Conexion, psCodEmpresa)
            Cbo.DataTextField = "ALTIBI_DESCRIPCION"
            Cbo.DataValueField = "ALTIBI_CODIGO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Sub Llena_AñoProyecto(ByVal Conexion As String, ByVal psCodEmpresa As String,
                                 ByVal Cbo As DropDownList)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cbo.DataSource = Lista_Proyecto_Año(Conexion, psCodEmpresa)
            Cbo.DataTextField = "PROYECTO_AÑO"
            Cbo.DataValueField = "PROYECTO_AÑO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Sub Llena_Proyecto(ByVal Conexion As String, ByVal psCodEmpresa As String,
                              ByVal Cbo As DropDownList, ByVal psAño As String)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cbo.DataSource = Lista_Proyecto(Conexion, psCodEmpresa, psAño)
            Cbo.DataTextField = "PROYECTO_DESCRIPCION"
            Cbo.DataValueField = "PROYECTO_CODIGO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Sub Llena_Modelo(ByVal Conexion As String, ByVal psCodEmpresa As String,
                            ByVal psCodMarca As String, ByVal psCodClasif As String,
                            ByVal Cbo As DropDownList)
        Dim Cn As New SqlConnection(Conexion)
        Cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = "SELECT ARTMOD_CODIGO,ARTMAR_CODIGO, ARTMOD_DESCRIPCION " _
                              & " FROM TBINV_ARTICULO_MODELO " _
                              & " WHERE ARTMOD_SYS_EST = '0' and ARTMAR_CODIGO = " & psCodMarca & " " _
                              & " AND ARTMOD_CLAS = " & psCodClasif & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "' " _
                              & " ORDER BY ARTMOD_DESCRIPCION"
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            Cbo.DataSource = cmdSql.ExecuteReader
            Cbo.DataTextField = "ARTMOD_DESCRIPCION"
            Cbo.DataValueField = "ARTMOD_CODIGO"
            Cbo.DataBind()
            Cbo.Items.Add("< Seleccionar >")
            Cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Function Devolver_UltimoCodSerie(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_ULTIMO_CODSERIE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_ULTIMO_CODSERIE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devolver_UltimoCodRecepcion(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_ULTIMO_CODRECEP", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_ULTIMO_CODRECEP")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devolver_UltimoCodArticulo(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEVOLVER_CODARTICULO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEVOLVER_CODARTICULO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Buscar_xCodClasif(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdCodClasif As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_BUSCAR_CODCLASIF", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodClasif", SqlDbType.Float).Value = pdCodClasif
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_BUSCAR_CODCLASIF")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Proyecto_Año(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PROYECTOAÑO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PROYECTOAÑO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Proyecto(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                   ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PROYECTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PROYECTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Recepcion(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                    ByVal pdCodAlmacen As Double, ByVal pdCodRecep As String,
                                    ByVal psEstado As String, ByVal pdCodProv As Double,
                                    ByVal psFechaIni As String, ByVal psFechaFin As String,
                                    ByVal psMotivo As String, ByVal psNroOC As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_RECEPCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Cmd.Parameters.Add("@CodRecep", SqlDbType.VarChar).Value = pdCodRecep
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@CodProv", SqlDbType.Float).Value = pdCodProv
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Cmd.Parameters.Add("@CodMotivo", SqlDbType.VarChar).Value = psMotivo
        Cmd.Parameters.Add("@NroOC", SqlDbType.VarChar).Value = psNroOC '@NroOC
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_RECEPCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Recepcion_Item(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                         ByVal pdCodRecep As Double, ByVal pSerie As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_RECEPCION_ITEM", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodRecepcion", SqlDbType.Float).Value = pdCodRecep
        Cmd.Parameters.Add("@Serie", SqlDbType.VarChar).Value = pSerie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_RECEPCION_ITEM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Recepcion_Detalle(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                         ByVal pdCodRecep As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Recepcion_Detalle_Items", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        'Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        'Cmd.Parameters.Add("@CodRecepcion", SqlDbType.Float).Value = pdCodRecep
        Cmd.Parameters.Add("@CodRecep", SqlDbType.Float).Value = pdCodRecep
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Recepcion_Detalle_Items")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Recepcion_Detalle_xItem(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                         ByVal pdCodRecep As Double, ByVal pdCodArt As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_Recepcion_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodRecepcion", SqlDbType.Float).Value = pdCodRecep
        Cmd.Parameters.Add("@Codart", SqlDbType.Float).Value = pdCodArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_Recepcion_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Recepcion_Item_Serie(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pdCodRecep As Double, ByVal pCodArt As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_RECEPCION_ITEM_SERIE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodRecepcion", SqlDbType.Float).Value = pdCodRecep
        Cmd.Parameters.Add("@CodArt", SqlDbType.Float).Value = pCodArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_RECEPCION_ITEM_SERIE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ArtxCodigo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                     ByVal pdCodArticulo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_CONSULTA_ARTXCODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pdCodArticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_CONSULTA_ARTXCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Almacen(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_BusquedaAlmacen(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                     ByVal pdCodAlmacen As Double, ByVal psAlmacenNombre As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_BusquedaAlmacen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Cmd.Parameters.Add("@AlmacenNombre", SqlDbType.VarChar).Value = psAlmacenNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_BusquedaAlmacen")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_BusquedaCentroCosto(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                              ByVal pdCodInterno As String, ByVal psCCostoNombre As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_BusquedaCentroCosto", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pdCodInterno
        Cmd.Parameters.Add("@CCNombre", SqlDbType.VarChar).Value = psCCostoNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_BusquedaCentroCosto")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Propietario(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PROPIETARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PROPIETARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Motivo_Ing(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_MOTIVO_INGRESO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_MOTIVO_INGRESO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N1(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL1", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL1")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N2(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N3(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL3", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL3")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N4(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double, ByVal pdCod_N3 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL4", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Cmd.Parameters.Add("@Cod_N3", SqlDbType.Float).Value = pdCod_N3
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL4")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N5(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double, ByVal pdCod_N3 As Double,
                                        ByVal pdCod_N4 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL5", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Cmd.Parameters.Add("@Cod_N3", SqlDbType.Float).Value = pdCod_N3
        Cmd.Parameters.Add("@Cod_N4", SqlDbType.Float).Value = pdCod_N4
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL5")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N6(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double, ByVal pdCod_N3 As Double,
                                        ByVal pdCod_N4 As Double, ByVal pdCod_N5 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL6", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Cmd.Parameters.Add("@Cod_N3", SqlDbType.Float).Value = pdCod_N3
        Cmd.Parameters.Add("@Cod_N4", SqlDbType.Float).Value = pdCod_N4
        Cmd.Parameters.Add("@Cod_N5", SqlDbType.Float).Value = pdCod_N5
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL6")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N7(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double, ByVal pdCod_N3 As Double,
                                        ByVal pdCod_N4 As Double, ByVal pdCod_N5 As Double,
                                        ByVal pdCod_N6 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL7", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Cmd.Parameters.Add("@Cod_N3", SqlDbType.Float).Value = pdCod_N3
        Cmd.Parameters.Add("@Cod_N4", SqlDbType.Float).Value = pdCod_N4
        Cmd.Parameters.Add("@Cod_N5", SqlDbType.Float).Value = pdCod_N5
        Cmd.Parameters.Add("@Cod_N6", SqlDbType.Float).Value = pdCod_N6
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL7")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N8(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double, ByVal pdCod_N3 As Double,
                                        ByVal pdCod_N4 As Double, ByVal pdCod_N5 As Double,
                                        ByVal pdCod_N6 As Double, ByVal pdCod_N7 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL8", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Cmd.Parameters.Add("@Cod_N3", SqlDbType.Float).Value = pdCod_N3
        Cmd.Parameters.Add("@Cod_N4", SqlDbType.Float).Value = pdCod_N4
        Cmd.Parameters.Add("@Cod_N5", SqlDbType.Float).Value = pdCod_N5
        Cmd.Parameters.Add("@Cod_N6", SqlDbType.Float).Value = pdCod_N6
        Cmd.Parameters.Add("@Cod_N7", SqlDbType.Float).Value = pdCod_N7
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL8")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N9(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double, ByVal pdCod_N3 As Double,
                                        ByVal pdCod_N4 As Double, ByVal pdCod_N5 As Double,
                                        ByVal pdCod_N6 As Double, ByVal pdCod_N7 As Double,
                                        ByVal pdCod_N8 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL9", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Cmd.Parameters.Add("@Cod_N3", SqlDbType.Float).Value = pdCod_N3
        Cmd.Parameters.Add("@Cod_N4", SqlDbType.Float).Value = pdCod_N4
        Cmd.Parameters.Add("@Cod_N5", SqlDbType.Float).Value = pdCod_N5
        Cmd.Parameters.Add("@Cod_N6", SqlDbType.Float).Value = pdCod_N6
        Cmd.Parameters.Add("@Cod_N7", SqlDbType.Float).Value = pdCod_N7
        Cmd.Parameters.Add("@Cod_N8", SqlDbType.Float).Value = pdCod_N8
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL9")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Llena_Clasif_N10(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pdNivel As Double, ByVal pdCod_N1 As Double,
                                        ByVal pdCod_N2 As Double, ByVal pdCod_N3 As Double,
                                        ByVal pdCod_N4 As Double, ByVal pdCod_N5 As Double,
                                        ByVal pdCod_N6 As Double, ByVal pdCod_N7 As Double,
                                        ByVal pdCod_N8 As Double, ByVal pdCod_N9 As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARTCLASIFICACION_NIVEL10", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pdNivel
        Cmd.Parameters.Add("@Cod_N1", SqlDbType.Float).Value = pdCod_N1
        Cmd.Parameters.Add("@Cod_N2", SqlDbType.Float).Value = pdCod_N2
        Cmd.Parameters.Add("@Cod_N3", SqlDbType.Float).Value = pdCod_N3
        Cmd.Parameters.Add("@Cod_N4", SqlDbType.Float).Value = pdCod_N4
        Cmd.Parameters.Add("@Cod_N5", SqlDbType.Float).Value = pdCod_N5
        Cmd.Parameters.Add("@Cod_N6", SqlDbType.Float).Value = pdCod_N6
        Cmd.Parameters.Add("@Cod_N7", SqlDbType.Float).Value = pdCod_N7
        Cmd.Parameters.Add("@Cod_N8", SqlDbType.Float).Value = pdCod_N8
        Cmd.Parameters.Add("@Cod_N9", SqlDbType.Float).Value = pdCod_N9
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARTCLASIFICACION_NIVEL10")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Oficina(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                     ByVal psCodigo As String, ByVal psDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTAOFICINA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = psCodigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTAOFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Almacen(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                     ByVal psCodigo As Double, ByVal psDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTAALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = psCodigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTAALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Articulos(ByVal Ruta As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("LISTA_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTA_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_CentroCostos(ByVal Ruta As String, ByVal pCodEmpresa As String,
                                       ByVal pUser As String, ByVal pTipo As String,
                                       ByVal pEstado As String, ByVal pdCosOficina As Double,
                                       ByVal pVerificado As String, ByVal psTipificacion As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("SPINV_LISTA_CENTROCOSTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pdCosOficina
        Cmd.Parameters.Add("@Verificado", SqlDbType.VarChar).Value = pVerificado
        Cmd.Parameters.Add("@Tipificacion", SqlDbType.VarChar).Value = psTipificacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_CENTROCOSTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_StockArticulos(ByVal Ruta As String, ByVal pCodEmpresa As String, ByVal pCodAlmacen As Double, ByVal pCodArticulo As Double, ByVal pTipoLista As String, ByVal pCodClas As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("LISTA_STOCKARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Int).Value = pCodAlmacen
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Int).Value = pCodArticulo
        Cmd.Parameters.Add("@TipoLista", SqlDbType.VarChar).Value = pTipoLista
        Cmd.Parameters.Add("@CLAS_NUMERO", SqlDbType.VarChar).Value = pCodClas
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTA_STOCKARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ListaExportar_StockArticulos(ByVal Ruta As String, ByVal pCodEmpresa As String, ByVal pCodAlmacen As Double, ByVal pCodArticulo As Double, ByVal pTipoLista As String, ByVal pCodClas As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("LISTA_STOCKARTICULOS_EXPORTAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Int).Value = pCodAlmacen
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Int).Value = pCodArticulo
        Cmd.Parameters.Add("@TipoLista", SqlDbType.VarChar).Value = pTipoLista
        Cmd.Parameters.Add("@CLAS_NUMERO", SqlDbType.VarChar).Value = pCodClas
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTA_STOCKARTICULOS_EXPORTAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Equipos_aEnviar(ByVal Ruta As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("Prc_InvEnviar_ListaEquipos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_InvEnviar_ListaEquipos")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Equipos_aEnviar_Det(ByVal Ruta As String, ByVal pCodEmpresa As String, ByVal pdCodReg As Integer) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("Prc_InvEnviar_ListaEquipos_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodLista", SqlDbType.Int).Value = pdCodReg
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_InvEnviar_ListaEquipos_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Inv_ListaEquipos_AGenerar
    Public Function Lista_Equipos_aGenerar(ByVal Ruta As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("Prc_Inv_ListaEquipos_AGenerar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_ListaEquipos_AGenerar")
        Da.Fill(Dt)
        Return Dt
    End Function
    'SPINV_LISTA_EQUIPOS_ATRATAR_XSERIENUMERAR
    Public Function Lista_Equipos_aGenerar(ByVal Ruta As String, ByVal pCodEmpresa As String, ByVal psCodSerie As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_ATRATAR_XSERIENUMERAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSerie", SqlDbType.Float).Value = psCodSerie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_ATRATAR_XSERIENUMERAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    'SPINV_LISTA_EQUIPOS_ARECEPCIONAR
    Public Function Lista_Equipos_aRecepcionar(ByVal Ruta As String, ByVal pCodEmpresa As String, ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_ARECEPCIONAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_ARECEPCIONAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    'SPINV_LISTA_EQUIPOS_iNDIVIDUAL
    Public Function Lista_Equipos_MoverUno(ByVal Ruta As String, ByVal pCodEmpresa As String,
                                         ByVal pNroSerie As String, ByVal psPlacaNro As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_iNDIVIDUAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@NroSerie", SqlDbType.VarChar).Value = pNroSerie
        Cmd.Parameters.Add("@NroPlaca", SqlDbType.Float).Value = psPlacaNro  '@Antiguedad
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_iNDIVIDUAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Busca_Articulo_Sku(ByVal Ruta As String, ByVal pCodEmpresa As String, ByVal pArtSku As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("Prc_Inv_BuscaArticulo_xSku", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ArtSku", SqlDbType.VarChar).Value = pArtSku
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_BuscaArticulo_xSku")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Datos_Equipo_xSerie(ByVal Ruta As String, ByVal pCodEmpresa As String, ByVal psArtDescripcion As String,
                                         ByVal psSerieNumerar As Double, ByVal pNroSerie As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_VERIFICAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@ArtDescripcion", SqlDbType.VarChar).Value = psArtDescripcion
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.Float).Value = psSerieNumerar
        Cmd.Parameters.Add("@SerieNRO", SqlDbType.VarChar).Value = pNroSerie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_VERIFICAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Equipos_aTratar(ByVal Ruta As String, ByVal pCodEmpresa As String,
                                         ByVal pCodArticulo As Double, ByVal pTipoUbic As String,
                                         ByVal pCodigoUbic As Double, ByVal pTipoLista As String,
                                         ByVal pNroSerie As String, ByVal psTipoBien As String, ByVal pdAntiguedad As Integer, ByVal psPlacaNro As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_ATRATAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pCodArticulo
        Cmd.Parameters.Add("@TipoUbicacion", SqlDbType.VarChar).Value = pTipoUbic
        Cmd.Parameters.Add("@CodigoUbicacion", SqlDbType.Float).Value = pCodigoUbic
        Cmd.Parameters.Add("@TipoLista", SqlDbType.VarChar).Value = pTipoLista
        Cmd.Parameters.Add("@NroSerie", SqlDbType.VarChar).Value = pNroSerie
        Cmd.Parameters.Add("@TipoBien", SqlDbType.VarChar).Value = psTipoBien '@Antiguedad
        Cmd.Parameters.Add("@Antiguedad", SqlDbType.Int).Value = pdAntiguedad '@Antiguedad
        Cmd.Parameters.Add("@NroPlaca", SqlDbType.Int).Value = psPlacaNro  '@Antiguedad
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_ATRATAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_EquiposAlmacen(ByVal Ruta As String, ByVal pCodEmpresa As String,
                                         ByVal pCodArticulo As Double, ByVal pTipoUbic As String,
                                         ByVal pCodigoUbic As Double, ByVal pTipoLista As String,
                                         ByVal pNroSerie As String, ByVal psNroPlaca As Double, ByVal pCodRelacionado As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOSALMACEN2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pCodArticulo
        Cmd.Parameters.Add("@TipoUbicacion", SqlDbType.VarChar).Value = pTipoUbic
        Cmd.Parameters.Add("@CodigoUbicacion", SqlDbType.Float).Value = pCodigoUbic
        Cmd.Parameters.Add("@TipoLista", SqlDbType.VarChar).Value = pTipoLista
        Cmd.Parameters.Add("@NroSerie", SqlDbType.VarChar).Value = pNroSerie
        Cmd.Parameters.Add("@NroPlaca", SqlDbType.Float).Value = psNroPlaca
        Cmd.Parameters.Add("@CodRelacionado", SqlDbType.VarChar).Value = pCodRelacionado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOSALMACEN2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Regularizar_ListaEquipos_xUbicacionCarga(ByVal Ruta As String, ByVal pCodEmpresa As String,
                                                             ByVal pTipoUbic As String, ByVal pCodigoUbic As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("Prc_Inv_ListaEquiposUbicacion_xCarga", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Ubicac_Tipo", SqlDbType.VarChar).Value = pTipoUbic
        Cmd.Parameters.Add("@Ubicac_Codigo", SqlDbType.Float).Value = pCodigoUbic
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_ListaEquiposUbicacion_xCarga")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Regularizar_Diferencia_CeCosto(ByVal Ruta As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("Prc_Lista_Carga_Diferente_CeCosto", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        'Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        'Cmd.Parameters.Add("@Ubicac_Tipo", SqlDbType.VarChar).Value = pTipoUbic
        'Cmd.Parameters.Add("@Ubicac_Codigo", SqlDbType.Float).Value = pCodigoUbic
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Carga_Diferente_CeCosto")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Regularizar_Diferencia_Placas(ByVal Ruta As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("Prc_Lista_Diferencia_Placas", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Diferencia_Placas")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Almacenes(ByVal Ruta As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta)
        Dim Cmd As New SqlCommand("LISTA_ALMACENES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("LISTA_ALMACENES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_Serie(ByVal psConexion As String, ByVal NroSerie As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPBUSCAR_SERIE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@SERIE_NRO", SqlDbType.VarChar).Value = NroSerie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPBUSCAR_SERIE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Equipos(ByVal pconexion As String, ByVal psCodEmpresa As String,
                                   ByVal pdOficina As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPLISTADO_EQUIPOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@UbicCodigo", SqlDbType.Float).Value = pdOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTADO_EQUIPOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Articulo_Marca_Modelo(ByVal pconexion As String, ByVal codempresa As String, ByVal cod_articulo As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTA_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@ART_CODIGO", SqlDbType.VarChar).Value = cod_articulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTA_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BuscarXSerie_Placa(ByVal pconexion As String, ByVal CodEmpresa As String, ByVal UBICA As Double,
    ByVal PLACA_NRO As Double, ByVal SERIE_NRO As String, ByVal ARTICULO_CODIGO As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_BUSCAR_EQUIPO_SERIE_PLACA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@UBICACT_CODIGO", SqlDbType.Float).Value = UBICA
        Cmd.Parameters.Add("@PLACA_NRO", SqlDbType.Float).Value = PLACA_NRO
        Cmd.Parameters.Add("@SERIE_NRO", SqlDbType.VarChar).Value = SERIE_NRO
        Cmd.Parameters.Add("@ARTICULO_CODIGO", SqlDbType.Float).Value = ARTICULO_CODIGO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_BUSCAR_EQUIPO_SERIE_PLACA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BuscarX_Articulos(ByVal pconexion As String, ByVal Codempresa As String,
    ByVal CodArt As Double, ByVal DescArt As String, ByVal EquivaArt As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_ARTICULOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = Codempresa
        Cmd.Parameters.Add("@ART_CODIGO", SqlDbType.Float).Value = CodArt
        Cmd.Parameters.Add("@ART_DESCRIPCION", SqlDbType.VarChar).Value = DescArt
        Cmd.Parameters.Add("@ART_CODEQUIVA", SqlDbType.VarChar).Value = EquivaArt
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_ARTICULOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_ListaZona_xAlmacen

    Public Function ListarZona_xAlmacen(ByVal pconexion As String, ByVal codempresa As String,
                                        ByVal pdCodAlmacen As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("Prc_ListaZona_xAlmacen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_ListaZona_xAlmacen")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Centro_Costos(ByVal pconexion As String, ByVal codempresa As String,
    ByVal codInterno As String, ByVal descrip As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBLOGIS_CENTRO_COSTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@CCOSTO_COD_INTERNO", SqlDbType.VarChar).Value = codInterno
        Cmd.Parameters.Add("@CCOSTO_DESCRIPCION", SqlDbType.VarChar).Value = descrip
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBLOGIS_CENTRO_COSTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_xCentroCostos(ByVal pSConexion As String, ByVal psCodEmpresa As String,
                                          ByVal pdCodCentroCosto As Double) As DataTable
        Dim Cn As New SqlConnection(pSConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_XCENTROCOSTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodCentroCosto", SqlDbType.Float).Value = pdCodCentroCosto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_XCENTROCOSTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_Almacen_Recepcion(ByVal pconexion As String, ByVal codempresa As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBINV_ALMACEN_RECEPCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBINV_ALMACEN_RECEPCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_Articulos_Serie_Max(ByVal pconexion As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBINV_ARTICULOS_SERIES_MAX_SERIE_ENUM", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBINV_ARTICULOS_SERIES_MAX_SERIE_ENUM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_Articulos_NroSerie(ByVal pconexion As String, ByVal SERIE_NRO As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBINV_ARTICULOS_SERIES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@SERIE_NRO", SqlDbType.VarChar).Value = SERIE_NRO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBINV_ARTICULOS_SERIES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_MovGeneral_Max(ByVal pconexion As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBINV_MOVIMIENTO_GENERAL_MAX_MOV_NRO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBINV_MOVIMIENTO_GENERAL_MAX_MOV_NRO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_StockArticulos_Almacen(ByVal pconexion As String, ByVal codempresa As String,
    ByVal AlmacenCod As Double, ByVal UbicaTipo As String, ByVal ArticuloCod As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBINV_STOCK_ARTICULOS_ALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@ALMACEN_CODIGO", SqlDbType.Float).Value = AlmacenCod
        Cmd.Parameters.Add("@UBICACT_TIPO", SqlDbType.VarChar).Value = UbicaTipo
        Cmd.Parameters.Add("@ARTICULO_CODIGO", SqlDbType.Float).Value = ArticuloCod
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBINV_STOCK_ARTICULOS_ALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Garantia_Equipos(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psSerieNro As String,
                                           ByVal pdCodProveedor As Double, ByVal pdCodArticulo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_GARANTIA_EQUIPOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@SerieNro", SqlDbType.VarChar).Value = psSerieNro
        Cmd.Parameters.Add("@CodProveedor", SqlDbType.Float).Value = pdCodProveedor
        Cmd.Parameters.Add("@CodArticulo", SqlDbType.Float).Value = pdCodArticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_GARANTIA_EQUIPOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Proveedor(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                    ByVal psRuc As String, ByVal psRazonSocial As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PROVEEDOR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Ruc", SqlDbType.VarChar).Value = psRuc
        Cmd.Parameters.Add("@RazonSocial", SqlDbType.VarChar).Value = psRazonSocial
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PROVEEDOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Kardex(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                 ByVal psTipoUbica As String, ByVal pdCodUbica As Double,
                                 ByVal psArticulo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_KARDEX", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@TipoUbica", SqlDbType.VarChar).Value = psTipoUbica
        Cmd.Parameters.Add("@CodUbica", SqlDbType.Float).Value = pdCodUbica
        Cmd.Parameters.Add("@Articulo", SqlDbType.VarChar).Value = psArticulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_KARDEX")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_MovimientoEquipos(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                            ByVal psFechaIni As String, ByVal psFechaFin As String,
                                            ByVal psSerieNro As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_MOVIMIENTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Cmd.Parameters.Add("@SerieNro", SqlDbType.VarChar).Value = psSerieNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_MOVIMIENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_EquiposMantenimiento(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                               ByVal psSerie As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPMANTEN_LISTA_EQUIPOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Serie", SqlDbType.VarChar).Value = psSerie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPMANTEN_LISTA_EQUIPOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devuelve_Telefonica_Personas(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                            ByVal pCodPersona As Double, ByVal pTipoDoc As String,
                                            ByVal pNroDoc As String, ByVal pCliente As String,
                                            ByVal pDireccion As String, ByVal pProv As String,
                                            ByVal pDist As String, ByVal pTelefFijo As String,
                                            ByVal pTelef2 As String, ByVal pContacto As String,
                                            ByVal pReferencia As String, ByVal pTelef3 As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEVUELVE_CODPER", Cn)
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
        Dim Dt As New DataTable("SPINV_DEVUELVE_CODPER")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ultima_CodDevolucion(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEVUELVE_ULTIMADEVOL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEVUELVE_ULTIMADEVOL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ultima_CodPersona(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_ULTIMA_PERSONA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_ULTIMA_PERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Consulta_Garantia_xEquipo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal psNroSerie As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LIS_GARANTIAXEQUIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@SerieNro", SqlDbType.VarChar).Value = psNroSerie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LIS_GARANTIAXEQUIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Recepcion_Series_Exportar(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                                    ByVal pdCodRecep As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_RECEPCION_EXPORTAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodRecepcion", SqlDbType.Float).Value = pdCodRecep
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_RECEPCION_EXPORTAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Persona(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PERSONA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Extrae_Curier(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                  ByVal pUser As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_EXTRAE_CURIER", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_EXTRAE_CURIER")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ArchivoTelefonica(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pFechaEntrega As String, ByVal pFecFinEntrega As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ARCHIVO_TELEFONICA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFecFinEntrega
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ARCHIVO_TELEFONICA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_AgendaTelef(ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_AGENDATELEFONICA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_AGENDATELEFONICA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Guia(ByVal Conexion As String, ByVal pCodEmpresa As String,
                               ByVal pFechaEntrega As String, ByVal pCodCurrier As Double,
                               ByVal psFechaFin As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_GUIAREMISION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Cmd.Parameters.Add("@CodCurrier", SqlDbType.Float).Value = pCodCurrier
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_GUIAREMISION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GuiaxCourier(ByVal Conexion As String, ByVal pCodEmpresa As String,
                               ByVal pFechaEntrega As String, ByVal pCodCurrier As Double,
                               ByVal psUser As String, ByVal psFechaFin As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_GUIAREMISION_XCOURIER", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Cmd.Parameters.Add("@CodCurrier", SqlDbType.Float).Value = pCodCurrier
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_GUIAREMISION_XCOURIER")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GuiaArchivo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                      ByVal pFechaEntrega As String, ByVal pCodCurrier As Double,
                                      ByVal psFechaFin As String, ByVal psCodGuia As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_GUIAREMISION_ARCHIVO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Cmd.Parameters.Add("@CodCurrier", SqlDbType.Float).Value = pCodCurrier
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Cmd.Parameters.Add("@psGuiaCodigo", SqlDbType.VarChar).Value = psCodGuia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_GUIAREMISION_ARCHIVO")
        Da.Fill(Dt) 'psGuiaCodigo
        Return Dt
    End Function
    Public Function Lista_GuiaArchivoxCourier(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pFechaEntrega As String, ByVal psUser As String,
                                        ByVal psFechaFin As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_GUIAREMISION_ARCHIVO_XCOURIER", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_GUIAREMISION_ARCHIVO_XCOURIER")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_PedidoArchivoxCodigo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodArchivo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PEDIDO_ARCHIVOXCODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodArchivo", SqlDbType.Float).Value = pCodArchivo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PEDIDO_ARCHIVOXCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ArchivosxPedido(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                             ByVal pCodPedido As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PEDARCHIVO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPedido", SqlDbType.Float).Value = pCodPedido
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PEDARCHIVO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ObsxPedido(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                             ByVal pCodPedido As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PEDOBSERVACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPedido", SqlDbType.Float).Value = pCodPedido
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PEDOBSERVACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Currier(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                  ByVal pCurrierRuc As String, ByVal pCurrierRazonsocial As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_CURRIER", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CurrierRuc", SqlDbType.VarChar).Value = pCurrierRuc
        Cmd.Parameters.Add("@CurrierRS", SqlDbType.VarChar).Value = pCurrierRazonsocial
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_CURRIER")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_CurrierAutorizado(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                            ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_CURRIER_AUTORIZADO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_CURRIER_AUTORIZADO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_AlmacenxUsuario(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                          ByVal pUser As String, Optional ByVal pdCodAlmacen As Double = 0) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_ALMACENXUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_ALMACENXUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Insertar_UsuarioAlmacen(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                          ByVal pdCodAlmacen As Double, ByVal pCodUsuario As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Almacen_Usuario_Insertar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pCodUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Almacen_Usuario_Insertar")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Delete_UsuarioAlmacen(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                          ByVal pdCodAlmacen As Double, ByVal pCodUsuario As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Almacen_Usuario_Delete", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pCodUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Almacen_Usuario_Delete")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Usuario_xAlmacen(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                          ByVal pdCodAlmacen As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Almacen_Usuario_CodAlm", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Almacen_Usuario_CodAlm")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_PersonaxFecha(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pFechaEntrega As String, ByVal pFecFinEntrega As String,
                                        ByVal pNroPedido As String, ByVal pSerie As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_PERSONAXFECHA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Cmd.Parameters.Add("@FecFinEntrega", SqlDbType.VarChar).Value = pFecFinEntrega
        Cmd.Parameters.Add("@NroPedido", SqlDbType.VarChar).Value = pNroPedido
        Cmd.Parameters.Add("@Serie", SqlDbType.VarChar).Value = pSerie
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_PERSONAXFECHA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_PersonaFecha(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                   ByVal pCodPersona As Double, ByVal pFechaEntrega As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_EXISTE_PERFECHA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodPer", SqlDbType.Float).Value = pCodPersona
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FecEntrega", SqlDbType.VarChar).Value = pFechaEntrega
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_EXISTE_PERFECHA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_GuiaRemiArchivo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodGuia As Double, ByVal pCodArchivo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_EXISTE_GUIREMARCHIVO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Cmd.Parameters.Add("@CodArch", SqlDbType.Float).Value = pCodArchivo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_EXISTE_GUIREMARCHIVO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devolver_CodPersona(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodPedido As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEVOLVER_CODPERSONA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPedido", SqlDbType.Float).Value = pCodPedido
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEVOLVER_CODPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devolver_CodAlmacen(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                        ByVal pCodCurrier As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEVOLVER_CODALMACEN_CURRIER", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodCurrier", SqlDbType.Float).Value = pCodCurrier
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEVOLVER_CODALMACEN_CURRIER")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devolver_CodEquipo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEVOLVER_CODEQUIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEVOLVER_CODEQUIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Devolver_CodSalida(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodEquipo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_DEVOLVER_CODSALIDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodEquipo", SqlDbType.Float).Value = pCodEquipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEVOLVER_CODSALIDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GuiaRemision(ByVal Conexion As String, ByVal pCodEmpresa As String, ByVal pCodGuia As Double,
                                       ByVal psFechaIni As String, ByVal psFechafin As String, ByVal psTipoGuia As String,
                                       ByVal psRemTipo As String, ByVal psRemCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_GuiaRemision", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@psFechafin", SqlDbType.VarChar).Value = psFechafin
        Cmd.Parameters.Add("@TipoGuia", SqlDbType.VarChar).Value = psTipoGuia
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Cmd.Parameters.Add("@RemTipo", SqlDbType.VarChar).Value = psRemTipo
        Cmd.Parameters.Add("@RemCodigo", SqlDbType.Float).Value = psRemCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_GuiaRemision")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_SalidaEnviada_Detalle_SinSerie(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                                         ByVal pCodSalida As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDAS_DETALLE_SIN_SERIE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDAS_DETALLE_SIN_SERIE")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_SalidaEnviada_Detalle(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodSalida As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDAS_DETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDAS_DETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_SalidaEnviada_Detalle_Cantidades(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodSalida As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDAS_DETALLE_CANTIDADES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDAS_DETALLE_CANTIDADES")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_SalidaCCostoEnviada_Detalle(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodSalida As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDASCCOSTO_DETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDASCCOSTO_DETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_SalidaCCostoEnviada_Detalle_Cantidades(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodSalida As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDASCCOSTO_DETALLE_CANTIDADES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDASCCOSTO_DETALLE_CANTIDADES")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Lista_SalidaCCostoEnviada_Detalle_SinSerie(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodSalida As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDASCCOSTO_DETALLE_SIN_SERIE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDASCCOSTO_DETALLE_SIN_SERIE")
        Da.Fill(Dt)
        Return Dt
    End Function '

    Public Function Lista_SalidaEnviadaAlmacen(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodSalida As Double, ByVal psFechaIni As String, ByVal psFechafin As String, ByVal psMotivo As String, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDAS_ENVIADAS_ALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@psFechafin", SqlDbType.VarChar).Value = psFechafin
        Cmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = psMotivo
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDAS_ENVIADAS_ALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function '
    '
    Public Function Lista_SalidaAlmacen(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodSalida As Double, ByVal psFechaIni As String, ByVal psFechafin As String, ByVal psMotivo As String, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDAS_ALMACEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@psFechafin", SqlDbType.VarChar).Value = psFechafin
        Cmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = psMotivo
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDAS_ALMACEN")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function SalidaAlmacen_xSerieNumerar(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_SalidasAlmacen_xSerieNumerar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Serie_Numerar", SqlDbType.Float).Value = pSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_SalidasAlmacen_xSerieNumerar")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function SalidaCC_xSerieNumerar(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("[Prc_Inv_Lista_SalidasCCentro_xSerieNumerar]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Serie_Numerar", SqlDbType.Float).Value = pSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Inv_Lista_SalidasCCentro_xSerieNumerar]")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function SalidaRecepciones_xSerieNumerar(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("[Prc_Inv_Lista_SalidasRecepciones_xSerieNumerar]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Serie_Numerar", SqlDbType.Float).Value = pSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Inv_Lista_SalidasRecepciones_xSerieNumerar]")
        Da.Fill(Dt)
        Return Dt
    End Function '

    Public Function Datos_xTicket(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pTicket As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("[Prc_Crm_Ticket_Datos]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NroTicket", SqlDbType.Float).Value = pTicket
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[Prc_Crm_Ticket_Datos]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_SalidacCCosto(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodSalida As Double, ByVal psFechaIni As String, ByVal psFechafin As String, ByVal psMotivo As String, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDAS__CCOSTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@psFechafin", SqlDbType.VarChar).Value = psFechafin
        Cmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = psMotivo
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDAS__CCOSTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_SalidaEnviada_cCCosto(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodSalida As Double, ByVal psFechaIni As String, ByVal psFechafin As String, ByVal psMotivo As String, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_INV_LISTA_SALIDAS_ENVIADA_CCOSTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@psFechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@psFechafin", SqlDbType.VarChar).Value = psFechafin
        Cmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = psMotivo
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_INV_LISTA_SALIDAS_ENVIADA_CCOSTO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_GuiaRemision_Detalle(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_GuiaRemision_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_GuiaRemision_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GuiaRemision_Detalle_Acc(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                                   ByVal pCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_GuiaRemision_Detalle_Acc", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_GuiaRemision_Detalle_Acc")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Detalle_xSalida(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodSalida As Double, ByVal pTipoSalida As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_GuiaRemision_Detalle_xSalida", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@TipoSalida", SqlDbType.Float).Value = pTipoSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_GuiaRemision_Detalle_xSalida")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_DetalleSinSeries_xSalida(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodSalida As Double, ByVal pTipoSalida As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_GuiaRemision_DetalleSinSerie_xSalida", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodSalida", SqlDbType.Float).Value = pCodSalida
        Cmd.Parameters.Add("@TipoSalida", SqlDbType.Float).Value = pTipoSalida
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_GuiaRemision_DetalleSinSerie_xSalida")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GuiaRemision_xCodigo(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                               ByVal pCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_xGuiaRemision", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_xGuiaRemision")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_SalEquipos_xGuia(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_SALEQUIPO_XGUIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_SALEQUIPO_XGUIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_SalAccesorios_xGuia(ByVal Conexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_SALACCESORIOS_XGUIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = pCodGuia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_SALACCESORIOS_XGUIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GRxEquipo(ByVal Conexion As String, ByVal psSerieNro As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_GRXEQUIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@SerieNro", SqlDbType.VarChar).Value = psSerieNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_GRXEQUIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_xCodRecepcion(ByVal Conexion As String, ByVal psCodEmpresa As String,
                                        ByVal pdCodRecep As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPINV_BUSCA_RECEPCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodRecep", SqlDbType.Float).Value = pdCodRecep
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_BUSCA_RECEPCION")
        Da.Fill(Dt)
        Return Dt
    End Function 'Prc_Inv_Recepcion_Lista_Bienes
    Public Function Lista_bienes_xCodRecepcion(ByVal Conexion As String, ByVal psCodEmpresa As String,
                                               ByVal pdCodRecep As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Recepcion_Lista_Bienes", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodRecepcion", SqlDbType.Float).Value = pdCodRecep
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Recepcion_Lista_Bienes")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Inventario2(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Lista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function
    ' Prc_Inventario_BuscaUbic
    Public Function Lista_Inventario(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_BuscaUbic", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_BuscaUbic")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Inventario_BuscaUbic_Detalle
    Public Function Lista_Inventario_UbicDetalle(ByVal Conexion As String, ByVal psCodEmpresa As String, ByVal psCodUbic As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_BuscaUbic_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodUbic", SqlDbType.Float).Value = psCodUbic
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_BuscaUbic_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Inv_ListaGuiaRemisionTransportista
    Public Function Lista_Inventario_GuiaTransportista(ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_ListaGuiaRemisionTransportista", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_ListaGuiaRemisionTransportista")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GuiaTransportista_Detalle(ByVal Conexion As String, ByVal psCodEmpresa As String, ByVal psCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_ListaGuiaRemisionTransportista_Detalle", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGuiaT", SqlDbType.Float).Value = psCodGuia
        Dim Da As New SqlDataAdapter(Cmd) '@CodGuiaT
        Dim Dt As New DataTable("Prc_Inv_ListaGuiaRemisionTransportista_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Inv_Archivos_xGuiaRemision
    Public Function ListaArchivos_xGuiaRemision(ByVal Conexion As String, ByVal psCodEmpresa As String, ByVal psCodGuia As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Archivos_xGuiaRemision", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = psCodGuia
        Dim Da As New SqlDataAdapter(Cmd) '@CodGuiaT
        Dim Dt As New DataTable("Prc_Inv_Archivos_xGuiaRemision")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ListaDatos_xArchivos(ByVal Conexion As String, ByVal psCodEmpresa As String, ByVal psCodGuia As Double, ByVal psCodArchivo As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Inv_xArchivos_xGuiaRemision", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGuia", SqlDbType.Float).Value = psCodGuia
        Cmd.Parameters.Add("@CodArchivo", SqlDbType.Float).Value = psCodArchivo
        Dim Da As New SqlDataAdapter(Cmd) '@CodGuiaT
        Dim Dt As New DataTable("Prc_Inv_xArchivos_xGuiaRemision")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Ubicaciones(ByVal psConexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Ubicaciones2(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion2")
        Da.Fill(Dt)
        Return Dt
    End Function '

    Public Function Lista_Ubicaciones_xTipo(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inventario_Ubicacion_xTipo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Ubicacion_xTipo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inv_Lista_Articulos_xCodigo_xDescripcion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                             ByVal psArtCodigo As Double, ByVal psArtDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Lista_Articulo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@ArtCod", SqlDbType.Float).Value = psArtCodigo
        Cmd.Parameters.Add("@ArtDescripcion", SqlDbType.VarChar).Value = psArtDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Lista_Articulo")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Inv_Zona_ListaxAlmacen
    Public Function Inventario_Almacen_ListaZonas(ByVal pconexion As String, ByVal codempresa As String,
                                                  ByVal pdCodAlmacen As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Zona_ListaxAlmacen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pdCodAlmacen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Zona_ListaxAlmacen")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_Zona_UltimoRegistro(ByVal pConexion As String, ByVal pCodEmpresa As String,
                                                  ByVal pCodAlmacen As Double) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Zona_UltimoCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pCodAlmacen
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Zona_UltimoCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Inventario_Zona_Insertar(ByVal pConexion As String, ByVal pCodEmpresa As String,
                                             ByVal pCodAlmacen As Double, ByVal pCodZona As Double,
                                             ByVal pZonaNombre As String, ByVal pCodRegistro As Double) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Zona_Insertar", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pCodAlmacen
        Cmd.Parameters.Add("@CodZona", SqlDbType.Float).Value = pCodZona
        Cmd.Parameters.Add("@ZonaNombre", SqlDbType.VarChar).Value = pZonaNombre '
        Cmd.Parameters.Add("@CodRegistro", SqlDbType.Float).Value = pCodRegistro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Zona_Insertar")
        Da.Fill(Dt)
        Return Dt
    End Function '
    Public Function Inventario_Zona_Delete(ByVal pConexion As String, ByVal pCodEmpresa As String,
                                           ByVal pCodAlmacen As Double, ByVal pCodZona As Double) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Zona_Delete", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pCodAlmacen
        Cmd.Parameters.Add("@CodZona", SqlDbType.Float).Value = pCodZona
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Zona_Delete")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Inventario_Rack_UltimoRegistro(ByVal pConexion As String, ByVal pCodEmpresa As String,
                                                  ByVal pCodAlmacen As Double, ByVal pCodZona As Double) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Rack_UltimoCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodAlmacen", SqlDbType.Float).Value = pCodAlmacen
        Cmd.Parameters.Add("@CodZona", SqlDbType.Float).Value = pCodZona
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Rack_UltimoCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function '

    Public Function Inventario_Rack_Relacion_xZona(ByVal pConexion As String, ByVal pCodEmpresa As String,
                                                   ByVal pCodZonaCorrelativo As Double) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Zona_ListaRack", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodZonaCorrelativo", SqlDbType.Float).Value = pCodZonaCorrelativo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Zona_ListaRack")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Buscar_PlacaSerie(ByVal pConexion As String, ByVal pCodEmpresa As String,
                                      ByVal pNroSerie As String, ByVal pNroPlaca As Double) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("SPINV_LISTA_EQUIPOS_INDIVIDUAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@NroSerie", SqlDbType.VarChar).Value = pNroSerie
        Cmd.Parameters.Add("@NroPlaca", SqlDbType.Float).Value = pNroPlaca
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_LISTA_EQUIPOS_INDIVIDUAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Lista_Sku(ByVal pConexion As String, ByVal pCodEmpresa As String, ByVal pSku As String,
                              ByVal pFamilia As String, ByVal pDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_ListaArticulos_Sku", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Sku", SqlDbType.VarChar).Value = pSku
        Cmd.Parameters.Add("@Familia", SqlDbType.VarChar).Value = pFamilia
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_ListaArticulos_Sku")
        Da.Fill(Dt)
        Return Dt
    End Function

    '
    Public Function Lista_Sku_SinImagen(ByVal pConexion As String, ByVal pCodEmpresa As String, ByVal pSku As String,
                              ByVal pFamilia As String, ByVal pDescripcion As String) As DataTable
        Dim Cn As New SqlConnection(pConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_ListaArticulos_Sku_SinImagen", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Sku", SqlDbType.VarChar).Value = pSku
        Cmd.Parameters.Add("@Familia", SqlDbType.VarChar).Value = pFamilia
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_ListaArticulos_Sku_SinImagen")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class