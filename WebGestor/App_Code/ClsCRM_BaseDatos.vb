Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Public Class ClsCRM_BaseDatos
    Public Function BaseDatos_Contador(ByVal pCodEmpresa As String, ByVal pdConexion As String, ByVal pCodBaseDatos As Double, ByVal pTipoModificacion As String) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPINSUPD_ACTCONTADOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBaseDatos", SqlDbType.Int).Value = pCodBaseDatos
        Cmd.Parameters.Add("@TipoModificacion", SqlDbType.VarChar).Value = pTipoModificacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPINSUPD_ACTCONTADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Crm_Busqueda_BaseDatos(ByVal pCodEmpresa As String, ByVal pdConexion As String, ByVal psCodBDC As Double) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("Prc_Crm_ListaArchivo_xBaseConocimiento", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBdc", SqlDbType.Float).Value = psCodBDC
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Crm_ListaArchivo_xBaseConocimiento")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Crm_BD_MuestraArchivo_xCodigo(ByVal pCodigo As Double, ByVal Conexion As String, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Crm_BaseConocimiento_ArchivoXCodigo", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Crm_BaseConocimiento_ArchivoXCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_FechaContador(ByVal pFecha As String, ByVal pdConexion As String, ByVal pFechaFin As String, ByVal pTipoModificacion As String) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPINSUPD_ACTFECHACONTADOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@TipoModificacion", SqlDbType.VarChar).Value = pTipoModificacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPINSUPD_ACTFECHACONTADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_ResumenMes(ByVal pCodEmpresa As String, ByVal pdConexion As String, ByVal pAño As String, ByVal pMes As String) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPINSUPD_RESUMEN_XMES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@Mes", SqlDbType.VarChar).Value = pMes
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPINSUPD_RESUMEN_XMES")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function CasLista_ReiniciarContador(ByVal pdConexion As String) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTP_PARAREINICIAR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTP_PARAREINICIAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_BaseDatosTop10(ByVal pCodEmpresa As String, ByVal pdConexion As String) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPBASECONOCIMIENTO_TOP10", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPBASECONOCIMIENTO_TOP10")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_BaseDatos(ByVal pCodEmpresa As String, ByVal pdConexion As String, ByVal pCodAplicativo As Double, ByVal pCodProducto As Double, ByVal pCodSubProd As Double) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_BASECONOCIMIENTOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Int).Value = pCodSubProd
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_BASECONOCIMIENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Aplicativos(ByVal pCodEmpresa As String, ByVal pdConexion As String, ByVal pCodAplicativo As Double) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_APLICATIVOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_APLICATIVOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Productos(ByVal pCodEmpresa As String, ByVal pdConexion As String, ByVal pCodAplicativo As Double, ByVal pCodProducto As Double) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_PRODUCTOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_PRODUCTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_BaseDatos(ByVal pCodEmpresa As String, ByVal pdConexion As String, ByVal pCodProducto As Double, ByVal pCodSubProd As Double) As DataTable
        Dim Cn As New SqlConnection(pdConexion)
        Dim Cmd As New SqlCommand("PRC_GTPLISTA_SUBPRODUCTOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPLISTA_SUBPRODUCTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasInsUpd_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodBaseDatos As Double,
                                ByVal pCodAplicativo As Double, ByVal pCodProducto As Double,
                                ByVal pCodSubProd As Double, ByVal pTransaccion As String,
                                ByVal pConsulta As String, ByVal pSolucion As String,
                                ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("PRC_GTPINSUPD_BASEDATOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBaseDatos", SqlDbType.Int).Value = pCodBaseDatos
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Int).Value = pCodSubProd
        Cmd.Parameters.Add("@Transaccion", SqlDbType.VarChar).Value = pTransaccion
        Cmd.Parameters.Add("@Consulta", SqlDbType.VarChar).Value = pConsulta
        Cmd.Parameters.Add("@Solucion", SqlDbType.VarChar).Value = pSolucion
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_GTPINSUPD_BASEDATOS")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function CRM_InsUpd_Archivo(ByVal psCodEmpresa As String, ByVal conexion As String,
                                       ByVal psCodBDC As Double, ByVal psArchivo As String,
                                       ByVal psRuta As String, ByVal psFecha As String,
                                       ByVal psHora As String, ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(conexion)
        Dim Cmd As New SqlCommand("Prc_Crm_BaseConocimiento_InsArchivo", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodBDC", SqlDbType.Float).Value = psCodBDC
        Cmd.Parameters.Add("@Archivo", SqlDbType.VarChar).Value = psArchivo
        Cmd.Parameters.Add("@Ruta", SqlDbType.VarChar).Value = psRuta
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = psHora
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Crm_BaseConocimiento_InsArchivo")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
