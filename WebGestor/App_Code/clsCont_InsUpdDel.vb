Imports System.Data.SqlClient
Imports System.Data
Public Class ClsCont_InsUpdDel
    Public Function Cont_InsUpd_FlujoCaja(ByVal pCodEmpresa As String, ByVal pAño As String,
                                          ByVal pCodInterno As String, ByVal pDescripcion As String,
                                          ByVal pTipoFc As String, ByVal pTipo As String,
                                          ByVal pCodigo As Double, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_FLUJOCAJA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@TipoFC", SqlDbType.VarChar).Value = pTipoFc
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_FLUJOCAJA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_InsUpd_CentroCosto(ByVal pCodEmpresa As String, ByVal pAño As String,
                                            ByVal pOrganigrama As String, ByVal pDescripcion As String,
                                            ByVal pNivelOrden As String, ByVal pTipo As String,
                                            ByVal pCodigo As Double, ByVal pNivel As String,
                                            ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_CENTROCOSTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@Organigrama", SqlDbType.VarChar).Value = pOrganigrama
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@NivelOrden", SqlDbType.VarChar).Value = pNivelOrden
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Nivel", SqlDbType.VarChar).Value = pNivel
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_CENTROCOSTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_InsUpd_Personas(ByVal pCodigo As Double, ByVal pRuc As String,
                                         ByVal pRazon As String, ByVal pApepat As String,
                                         ByVal pApemat As String, ByVal pNombres As String,
                                         ByVal pContacto As String, ByVal pTipoPer As String,
                                         ByVal pTipoCliente As String, ByVal pGiro As String,
                                         ByVal pDireccion As String, ByVal pPais As String,
                                         ByVal pDpto As String, ByVal pProv As String,
                                         ByVal pDist As String, ByVal pEmail As String,
                                         ByVal pWeb As String, ByVal pCodEmpresa As String,
                                         ByVal pEmail2 As String, ByVal pWeb2 As String,
                                         ByVal pTelf1 As String, ByVal pTelf2 As String,
                                         ByVal pTelf_Of As String, ByVal pAnexo_Of As String,
                                         ByVal pTelf_Celu As String, ByVal pFax1 As String,
                                         ByVal pFax2 As String, ByVal pCertIncrip As String,
                                         ByVal pCateg As String, ByVal pTipo As String,
                                         ByVal psFormaPago As String, ByVal psConexion As String,
                                         ByVal psSectorEconomico As String, ByVal psRubro As String,
                                         ByVal psDniContacto As String, ByVal psPuesto As String, ByVal psGrabaContacto As String, ByVal psValorSys As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_PERSONAS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Ruc", SqlDbType.VarChar).Value = pRuc
        Cmd.Parameters.Add("@Razon", SqlDbType.VarChar).Value = pRazon
        Cmd.Parameters.Add("@Apepat", SqlDbType.VarChar).Value = pApepat
        Cmd.Parameters.Add("@Apemat", SqlDbType.VarChar).Value = pApemat
        Cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = pNombres
        Cmd.Parameters.Add("@Contacto", SqlDbType.VarChar).Value = pContacto
        Cmd.Parameters.Add("@TipoPer", SqlDbType.VarChar).Value = pTipoPer
        Cmd.Parameters.Add("@TipoCliente", SqlDbType.VarChar).Value = pTipoCliente
        Cmd.Parameters.Add("@Giro", SqlDbType.VarChar).Value = pGiro
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = pDireccion
        Cmd.Parameters.Add("@Pais", SqlDbType.VarChar).Value = pPais
        Cmd.Parameters.Add("@Dpto", SqlDbType.VarChar).Value = pDpto
        Cmd.Parameters.Add("@Prov", SqlDbType.VarChar).Value = pProv
        Cmd.Parameters.Add("@Dist", SqlDbType.VarChar).Value = pDist
        Cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = pEmail
        Cmd.Parameters.Add("@Web", SqlDbType.VarChar).Value = pWeb
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Email2", SqlDbType.VarChar).Value = pEmail2
        Cmd.Parameters.Add("@Web2", SqlDbType.VarChar).Value = pWeb2
        Cmd.Parameters.Add("@Telf1", SqlDbType.VarChar).Value = pTelf1
        Cmd.Parameters.Add("@Telf2", SqlDbType.VarChar).Value = pTelf2
        Cmd.Parameters.Add("@Telf_Of", SqlDbType.VarChar).Value = pTelf_Of
        Cmd.Parameters.Add("@Anexo_Of", SqlDbType.VarChar).Value = pAnexo_Of
        Cmd.Parameters.Add("@Telf_Celu", SqlDbType.VarChar).Value = pTelf_Celu
        Cmd.Parameters.Add("@Fax1", SqlDbType.VarChar).Value = pFax1
        Cmd.Parameters.Add("@Fax2", SqlDbType.VarChar).Value = pFax2
        Cmd.Parameters.Add("@CertIncrip", SqlDbType.VarChar).Value = pCertIncrip
        Cmd.Parameters.Add("@Categ", SqlDbType.VarChar).Value = pCateg
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@FormaPago", SqlDbType.VarChar).Value = psFormaPago
        Cmd.Parameters.Add("@SectorEconomico", SqlDbType.VarChar).Value = psSectorEconomico
        Cmd.Parameters.Add("@Rubro", SqlDbType.VarChar).Value = psGrabaContacto
        Cmd.Parameters.Add("@DniContacto", SqlDbType.VarChar).Value = psDniContacto
        Cmd.Parameters.Add("@Puesto", SqlDbType.VarChar).Value = psPuesto
        Cmd.Parameters.Add("@GrabaContacto", SqlDbType.VarChar).Value = psGrabaContacto
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psValorSys
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_PERSONAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_Upd_Persona_FecNac(ByVal psCodEmpresa As String, ByVal psFecNac As String,
                                            ByVal psCodCliente As Double, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Cont_PersonaFecNac", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@FecNacimiento", SqlDbType.VarChar).Value = psFecNac
        Cmd.Parameters.Add("@CodCliente", SqlDbType.Float).Value = psCodCliente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cont_PersonaFecNac")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_InsUpd_TipoCambio(ByVal pFecha As String, ByVal pHora As String,
                                           ByVal pUser As String, ByVal pCompra As Decimal,
                                           ByVal pVenta As Decimal, ByVal pTipoIngreso As String,
                                           ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_TIPOCAMBIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = pHora
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@Compra", SqlDbType.Decimal).Value = pCompra
        Cmd.Parameters.Add("@Venta", SqlDbType.Decimal).Value = pVenta
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_TIPOCAMBIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_InsUpd_Aduana(ByVal pCodEmpresa As String, ByVal pAño As String,
                                           ByVal pCodAduana As String, ByVal pDescripcion As String,
                                           ByVal pUser As String, ByVal pTipoIngreso As String,
                                           ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_ADUANA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@CodAduana", SqlDbType.VarChar).Value = pCodAduana
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_ADUANA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_InsUpd_MedioPago(ByVal pCodEmpresa As String, ByVal pAño As String,
                                           ByVal pCodMedioPago As String, ByVal pDescripcion As String,
                                           ByVal pUser As String, ByVal pTipoIngreso As String,
                                           ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_MEDIOPAGO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@CodMedioPago", SqlDbType.VarChar).Value = pCodMedioPago
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_MEDIOPAGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_InsUpd_Periodo(ByVal pCodEmpresa As String, ByVal pAño As String,
                                        ByVal pTipoPer As String, ByVal pNroPeriodo As String,
                                        ByVal pPeriodo As String, ByVal pNombre As String,
                                        ByVal pFechaIni As String, ByVal pFechaFin As String,
                                        ByVal pPerActual As String, ByVal pTipoIngreso As String,
                                        ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_PERIODO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@TipoPer", SqlDbType.VarChar).Value = pTipoPer
        Cmd.Parameters.Add("@NroPeriodo", SqlDbType.VarChar).Value = pNroPeriodo
        Cmd.Parameters.Add("@Periodo", SqlDbType.VarChar).Value = pPeriodo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@PerActual", SqlDbType.VarChar).Value = pPerActual
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_PERIODO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_InsUpd_CuentaBanco(ByVal pCodEmpresa As String, ByVal pCodBanco As Double,
                                            ByVal pBancoNombre As String, ByVal pMoneda As String,
                                            ByVal pTipo As String, ByVal pNroCuenta As String,
                                            ByVal pUser As String, ByVal pTipoIngreso As String,
                                            ByVal pCBan_Codigo As Double, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_INSUPD_CUENTABANCO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBanco", SqlDbType.Float).Value = pCodBanco
        Cmd.Parameters.Add("@BancoNombre", SqlDbType.VarChar).Value = pBancoNombre
        Cmd.Parameters.Add("@Moneda", SqlDbType.VarChar).Value = pMoneda
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@NroCuenta", SqlDbType.VarChar).Value = pNroCuenta
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@CBan_Codigo", SqlDbType.Float).Value = pCBan_Codigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_INSUPD_CUENTABANCO")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
