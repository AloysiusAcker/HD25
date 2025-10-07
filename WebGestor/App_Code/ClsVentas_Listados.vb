Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class ClsVentas_Listados
    Public Function PtoVenta_ListaCaja(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_PtoVenta_ListaCaja", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_PtoVenta_ListaCaja")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_PtoVenta_Lista_Articulos
    Public Function PtoVenta_ListaArticulos(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                            ByVal psArtCod As Double, ByVal psArtDescripcion As String, ByVal psArtNroParte As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_PtoVenta_Lista_Articulos", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@ART_CODIGO", SqlDbType.Float).Value = psArtCod
        Cmd.Parameters.Add("@ART_DESCRIPCION", SqlDbType.VarChar).Value = psArtDescripcion
        Cmd.Parameters.Add("@ART_CODEQUIVA", SqlDbType.VarChar).Value = psArtNroParte
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_PtoVenta_Lista_Articulos")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Ventas_StockActual_xArticulo
    Public Function Obtener_StockActual_xCodArt(ByVal psConexion As String, ByVal pdArtCodigo As Double) As Double

        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim RsC As SqlDataReader
        Obtener_StockActual_xCodArt = 0
        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " Select SUM(SAA_STOCK_ACTUAL) - ISNULL(SAA_PARATRANSITO,0) As CANT " _
                              & " From dbo.TBINV_STOCK_ARTICULOS_ALMACEN S " _
                              & " WHERE (ARTICULO_CODIGO = " & pdArtCodigo & ") And (UBICACT_TIPO = '1') AND (ALMACEN_CODIGO='1')" _
                              & " GROUP BY UBICACT_TIPO, ARTICULO_CODIGO,SAA_PARATRANSITO,ALMACEN_CODIGO"
        RsC = CmdGlobal.ExecuteReader
        If RsC.HasRows Then
            While RsC.Read
                Obtener_StockActual_xCodArt = Nz(RsC!CANT)
            End While
        End If
        RsC.Close()
        Cn.Close()
    End Function
    'Prc_Ventas_PrecioUnit_xArticulo    '
    Public Function ListaTelefonos_xCliente(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                            ByVal pdClienteCodigo As Double, ByVal psTelefono As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_ListaTelefono_xCliente", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodCliente", SqlDbType.Float).Value = pdClienteCodigo
        Cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = psTelefono
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_ListaTelefono_xCliente")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Ventas_Insert_OportunidadSeguimiento(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                         ByVal pdCodOportunidad As Double, ByVal pSeguiFecha As String, ByVal pSeguiHora As String,
                                                         ByVal pSeguiTipo As String, ByVal pSeguiDetalle As String, ByVal pUser As String,
                                                         ByVal pProxFecha As String, ByVal pProxHora As String, ByVal pProxAcc As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_Insert_OportunidadSeguimiento", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodOportunidad", SqlDbType.Float).Value = pdCodOportunidad
        Cmd.Parameters.Add("@SeguiFecha", SqlDbType.VarChar).Value = pSeguiFecha
        Cmd.Parameters.Add("@SeguiHora", SqlDbType.VarChar).Value = pSeguiHora
        Cmd.Parameters.Add("@SeguiTipo", SqlDbType.VarChar).Value = pSeguiTipo
        Cmd.Parameters.Add("@SeguiDetalle", SqlDbType.VarChar).Value = pSeguiDetalle
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@ProxFecha", SqlDbType.VarChar).Value = pProxFecha
        Cmd.Parameters.Add("@ProxHora", SqlDbType.VarChar).Value = pProxHora
        Cmd.Parameters.Add("@ProxAcc", SqlDbType.VarChar).Value = pProxAcc
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_Insert_OportunidadSeguimiento")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function PrecioVenta_xCodArt(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdArtCodigo As Double) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_PrecioUnit_xArticulo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@ArtCodigo", SqlDbType.Float).Value = pdArtCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_PrecioUnit_xArticulo")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Ventas_Lista_Oportunidades

    Public Function Ventas_ListaOportunidades(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                              ByVal pFechaIni As String, ByVal pFechaFin As String, ByVal pCodVendedor As String) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_Lista_Oportunidades", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@CodVendedor", SqlDbType.VarChar).Value = pCodVendedor
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_Lista_Oportunidades")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Ventas_ListaSeguimiento_xOportunidad(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodOportunidad As Double) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_Oportunidades_ListaSeguimiento", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodOportunidad", SqlDbType.Float).Value = pdCodOportunidad
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_Oportunidades_ListaSeguimiento")
        Da.Fill(Dt)
        Return Dt

    End Function
    Public Function Ventas_Lista_xOportunidad(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal pdCodOportunidad As Double) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_Lista_xOportunidad", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodOportunidad", SqlDbType.Float).Value = pdCodOportunidad
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_Lista_xOportunidad")
        Da.Fill(Dt)
        Return Dt

    End Function
    Public Function Ventas_InsertarOportunidades(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                 ByVal pdOportunidad_Cod As Double, ByVal pOportunidad_Req As String, ByVal pOportunidad_Fecha As String,
                                                 ByVal pOportunidad_Detalle As String, ByVal pOportunidad_Vendedor As String, ByVal pCliente_Ruc As String,
                                                 ByVal pCliente_Razonsocial As String, ByVal pCliente_Direccion As String, ByVal pCliente_Pais As String,
                                                 ByVal pCliente_Dpto As String, ByVal pCliente_Prov As String, ByVal pCliente_Dist As String,
                                                 ByVal pCont_ApePat As String, ByVal pCont_ApeMat As String, ByVal pcont_Nombres As String,
                                                 ByVal pCont_Email As String, ByVal pCont_Telef As String, ByVal pCont_telef2 As String, ByVal pUser As String) As DataTable

        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Ventas_Insert_Oportunidades", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Oportunidad_Cod", SqlDbType.Float).Value = pdOportunidad_Cod
        Cmd.Parameters.Add("@Oportunidad_Req", SqlDbType.VarChar).Value = pOportunidad_Req
        Cmd.Parameters.Add("@Oportunidad_Fecha", SqlDbType.VarChar).Value = pOportunidad_Fecha
        Cmd.Parameters.Add("@Oportunidad_Detalle", SqlDbType.VarChar).Value = pOportunidad_Detalle
        Cmd.Parameters.Add("@Oportunidad_Vendedor", SqlDbType.VarChar).Value = pOportunidad_Vendedor
        Cmd.Parameters.Add("@Cliente_Ruc", SqlDbType.VarChar).Value = pCliente_Ruc
        Cmd.Parameters.Add("@Cliente_Razonsocial", SqlDbType.VarChar).Value = pCliente_Razonsocial
        Cmd.Parameters.Add("@Cliente_Direccion", SqlDbType.VarChar).Value = pCliente_Direccion
        Cmd.Parameters.Add("@Cliente_Pais", SqlDbType.VarChar).Value = pCliente_Pais
        Cmd.Parameters.Add("@Cliente_Dpto", SqlDbType.VarChar).Value = pCliente_Dpto
        Cmd.Parameters.Add("@Cliente_Prov", SqlDbType.VarChar).Value = pCliente_Prov
        Cmd.Parameters.Add("@Cliente_Dist", SqlDbType.VarChar).Value = pCliente_Dist
        Cmd.Parameters.Add("@Cont_ApePat", SqlDbType.VarChar).Value = pCont_ApePat
        Cmd.Parameters.Add("@Cont_ApeMat", SqlDbType.VarChar).Value = pCont_ApeMat
        Cmd.Parameters.Add("@cont_Nombres", SqlDbType.VarChar).Value = pcont_Nombres
        Cmd.Parameters.Add("@Cont_Email", SqlDbType.VarChar).Value = pCont_Email
        Cmd.Parameters.Add("@Cont_Telef", SqlDbType.VarChar).Value = pCont_Telef
        Cmd.Parameters.Add("@Cont_telef2", SqlDbType.VarChar).Value = pCont_telef2
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Ventas_Insert_Oportunidades")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Obtener_ValorIgv(ByVal psConexion As String) As Double
        Dim Cn As New SqlClient.SqlConnection(Ruta_GrEmp)
        Dim CmdGlobal As New SqlCommand
        Dim RsC As SqlDataReader
        Obtener_ValorIgv = 0
        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE (ELEMEN_TABLA = 'TBOPC356') AND ELEMEN_CODIGO = 1"
        RsC = CmdGlobal.ExecuteReader
        If RsC.HasRows Then
            While RsC.Read
                Obtener_ValorIgv = Nz(RsC!ELEMEN_VALOR) / 100
            End While
        End If
        RsC.Close()
        Cn.Close()
    End Function
    Public Sub Cargar_TipoDocumento(ByVal cbo As DropDownList, ByVal pCodEmpresa As String, ByVal Conexion As String, ByVal psAño As String)
        Dim Cn As New SqlConnection(Conexion)
        cbo.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT DOC_CODIGO, DOC_DOCUMENTO " _
                              & " From dbo.TBDOCUMENTOS " _
                              & " WHERE (DOC_EMPRESA = '" & pCodEmpresa & "') AND (DOC_AÑO ='" & psAño & "') AND (DOC_SYS_EST = '0')"
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            cbo.DataSource = cmdSql.ExecuteReader
            cbo.DataTextField = "DOC_DOCUMENTO"
            cbo.DataValueField = "DOC_CODIGO"
            cbo.DataBind()
            cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
End Class