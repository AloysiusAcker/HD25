Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class ClsSIntegral
    Public Function Listar_ServiciosPublicados(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                               ByVal psTipoSector As Double, ByVal pdTipo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SP_SERVINTEGRAL_LIS_SERVICIOS_PUBLICADOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@TipoSector", SqlDbType.Float).Value = psTipoSector
        Cmd.Parameters.Add("@Tipo", SqlDbType.Float).Value = pdTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_SERVINTEGRAL_LIS_SERVICIOS_PUBLICADOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_DetalleServicio(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                           ByVal psTipoSector As Double, ByVal pdTipo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SP_SERVINTEGRAL_LIS_SERVDETALLE", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@TipoSector", SqlDbType.Float).Value = psTipoSector
        Cmd.Parameters.Add("@Tipo", SqlDbType.Float).Value = pdTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_SERVINTEGRAL_LIS_SERVDETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_DetalleServicio_XCodigo(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                   ByVal psNroServicio As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SP_SERVINTEGRAL_LIS_SERVDETALLE_XCODIGO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@NroServicio", SqlDbType.Float).Value = psNroServicio
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_SERVINTEGRAL_LIS_SERVDETALLE_XCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Proveedor(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                     ByVal psApePat As String, ByVal psTipoPer As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SP_SERVINTEGRAL_LIS_PROVEEDOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@ApePat", SqlDbType.VarChar).Value = psApePat
        Cmd.Parameters.Add("@TipoPer", SqlDbType.VarChar).Value = psTipoPer
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_SERVINTEGRAL_LIS_PROVEEDOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Servicio(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                 ByVal pdCodServicio As Double, ByVal pdPrecio As Double,
                                 ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SP_SERVINTEGRAL_INS_SERVICIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodServicio", SqlDbType.Float).Value = pdCodServicio
        Cmd.Parameters.Add("@Precio", SqlDbType.Float).Value = pdPrecio
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_SERVINTEGRAL_INS_SERVICIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_ServDetalle(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                    ByVal pdSector As Double, ByVal psTipo As Double, ByVal pdTipo2 As Double,
                                    ByVal pdCodProveedor As Double, ByVal psServDescrip As String,
                                    ByVal psPrecio As Double, ByVal psDireccion As String,
                                    ByVal psPais As String, ByVal psDpto As String,
                                    ByVal psProv As String, ByVal psDist As String,
                                    ByVal psObs As String, ByVal psFoto As String,
                                    ByVal psFecInicia As String, ByVal psFecTermina As String,
                                    ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SP_SERVINTEGRAL_INS_SERVDETALLE", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Sector", SqlDbType.Float).Value = pdSector
        Cmd.Parameters.Add("@Tipo", SqlDbType.Float).Value = psTipo
        Cmd.Parameters.Add("@Tipo2", SqlDbType.Float).Value = pdTipo2
        Cmd.Parameters.Add("@CodProveedor", SqlDbType.Float).Value = pdCodProveedor
        Cmd.Parameters.Add("@ServDescrip", SqlDbType.VarChar).Value = psServDescrip
        Cmd.Parameters.Add("@ServPrecio", SqlDbType.VarChar).Value = psPrecio
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = psDireccion
        Cmd.Parameters.Add("@Pais", SqlDbType.VarChar).Value = psPais
        Cmd.Parameters.Add("@Dpto", SqlDbType.VarChar).Value = psDpto
        Cmd.Parameters.Add("@Prov", SqlDbType.VarChar).Value = psProv
        Cmd.Parameters.Add("@Dist", SqlDbType.VarChar).Value = psDist
        Cmd.Parameters.Add("@ServObs", SqlDbType.VarChar).Value = psObs
        Cmd.Parameters.Add("@Foto", SqlDbType.VarChar).Value = psFoto
        Cmd.Parameters.Add("@FecInicia", SqlDbType.VarChar).Value = psFecInicia
        Cmd.Parameters.Add("@FecTermina", SqlDbType.VarChar).Value = psFecTermina
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_SERVINTEGRAL_INS_SERVDETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class