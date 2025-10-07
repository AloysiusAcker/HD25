
Imports System.Data.SqlClient
Imports System.Data
Public Class ClsCont_Listados
    Public Function Cont_ListaPeriodos(ByVal pCodEmpresa As String, ByVal pAño As String,
                                       ByVal pConPeriodo As String, ByVal pCodPeriodo As String,
                                       ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTAPERIODOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@lstAño", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@ConPeriodo", SqlDbType.VarChar).Value = pConPeriodo
        Cmd.Parameters.Add("@CodPeriodo", SqlDbType.VarChar).Value = pCodPeriodo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTAPERIODOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    'IMPLEMENTADO 04/11/09
    Public Function Cont_ListaSaldo(ByVal pCbanCodigo As Double, ByVal pConbAño As String,
                                    ByVal pConbPeriodo As Double, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTESALDO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CbanCodigo", SqlDbType.Float).Value = pCbanCodigo
        Cmd.Parameters.Add("@ConbAño", SqlDbType.VarChar).Value = pConbAño
        Cmd.Parameters.Add("@ConbPeriodo", SqlDbType.Float).Value = pConbPeriodo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTESALDO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaCtaBancaria(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTA_CTABANCARIA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTA_CTABANCARIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaAduana(ByVal pCodEmpresa As String, ByVal pAño As String,
                                       ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTAADUANA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@lstAño", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTAADUANA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaMedioPago(ByVal pCodEmpresa As String, ByVal pAño As String,
                                       ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTA_MEDIOPAGO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTA_MEDIOPAGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaAsientos(ByVal pCodEmpresa As String, ByVal pAño As String,
                                       ByVal pConAsiento As String, ByVal pCodAsiento As String,
                                       ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTAASIENTOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@lstAño", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@ConAsiento", SqlDbType.VarChar).Value = pConAsiento
        Cmd.Parameters.Add("@CodAsiento", SqlDbType.VarChar).Value = pCodAsiento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTAASIENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteAsiento(ByVal pCodEmpresa As String, ByVal pAño As String,
                                       ByVal pDato As String, ByVal pTipo As String,
                                       ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTEASIENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@CodDato", SqlDbType.VarChar).Value = pDato
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTEASIENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteAduana(ByVal pCodEmpresa As String, ByVal pDato As String,
                                      ByVal pAño As String, ByVal pTipoDato As String,
                                      ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTEADUANA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Dato", SqlDbType.VarChar).Value = pDato
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@TipoDato", SqlDbType.VarChar).Value = pTipoDato
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTEADUANA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteMedioPago(ByVal pCodEmpresa As String, ByVal pDato As String,
                                         ByVal pAño As String, ByVal pTipoDato As String,
                                         ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTEMEDIOPAGO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Dato", SqlDbType.VarChar).Value = pDato
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@TipoDato", SqlDbType.VarChar).Value = pTipoDato
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTEMEDIOPAGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaDocumentos(ByVal pCodEmpresa As String, ByVal pAño As String,
                                         ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTADOC", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@lstAño", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTADOC")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteCtaBanco(ByVal pCodEmpresa As String, ByVal pCodBanco As Double,
                                        ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTECTABANCO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBanco", SqlDbType.Float).Value = pCodBanco
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTECTABANCO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaCtaBancos(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTACTABANCOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTACTABANCOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaBancos(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTABANCOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTABANCOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteDocumentos(ByVal pCodEmpresa As String, ByVal pAño As String,
                                          ByVal pDato As String, ByVal pTipo As String,
                                          ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTEDOCUMENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@CodDato", SqlDbType.VarChar).Value = pDato
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTEDOCUMENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaCentroCostos(ByVal pCodEmpresa As String, ByVal pAño As String,
                                           ByVal pNivelOrden As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTACENTROCOSTOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@lstAño", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@NivelOrden", SqlDbType.VarChar).Value = pNivelOrden
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTACENTROCOSTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteCentroCostos(ByVal pCodEmpresa As String, ByVal pAño As String,
                                            ByVal pCodDato As String, ByVal pTipo As String,
                                            ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTECENTROCOSTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@CodDato", SqlDbType.VarChar).Value = pCodDato
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTECENTROCOSTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_FechaInicio(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCAS_FECHAINICIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_FECHAINICIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_AñoActual(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCAS_AÑOACTUAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_AÑOACTUAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaFlujoCaja(ByVal pCodEmpresa As String, ByVal pAño As String,
                                        ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_FLUJOCAJA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@lstAño", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_FLUJOCAJA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteFlujoCaja(ByVal pCodEmpresa As String, ByVal pAño As String,
                                         ByVal pDato As String, ByVal pTipo As String,
                                         ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTEFLUJOCAJA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Cmd.Parameters.Add("@CodDato", SqlDbType.VarChar).Value = pDato
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTEFLUJOCAJA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExisteAñoMes(ByVal pAññ As String, ByVal pMm As String,
                                      ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTEAÑOMES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Aññ", SqlDbType.VarChar).Value = pAññ
        Cmd.Parameters.Add("@Mm", SqlDbType.VarChar).Value = pMm
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTEAÑOMES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaTipoCambio(ByVal pAññ As String, ByVal pMm As String,
                                         ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTATIPOCAMBIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Aññ", SqlDbType.VarChar).Value = pAññ
        Cmd.Parameters.Add("@Mm", SqlDbType.VarChar).Value = pMm
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTATIPOCAMBIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaPersonas(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTAPERSONAS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTAPERSONAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ListaClientes(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                       ByVal psRuc As String, ByVal psRazonSocial As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_LISTACLIENTES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_LISTACLIENTES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_BusquedaPersonas(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                          ByVal psRuc As String, ByVal psRazonSocial As String, ByVal psTipo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Busqueda_xTipoPersona", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@RUC", SqlDbType.VarChar).Value = psRuc
        Cmd.Parameters.Add("@Razon", SqlDbType.VarChar).Value = psRazonSocial
        Cmd.Parameters.Add("@tipo", SqlDbType.VarChar).Value = psTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Busqueda_xTipoPersona")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_BusquedaVehiculo(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                          ByVal psVehiPlaca As String, ByVal psVehiMarca As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Busqueda_Vehiculo", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@VehiPlaca", SqlDbType.VarChar).Value = psVehiPlaca
        Cmd.Parameters.Add("@VehiMarca", SqlDbType.VarChar).Value = psVehiMarca
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Busqueda_Vehiculo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_BusquedaChofer(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                          ByVal psChoferDni As String, ByVal psChoferNombres As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Inv_Busqueda_chofer", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@ChoferDni", SqlDbType.VarChar).Value = psChoferDni
        Cmd.Parameters.Add("@ChoferNombre", SqlDbType.VarChar).Value = psChoferNombres
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inv_Busqueda_chofer")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_ExistePersonas(ByVal pCodEmpresa As String, ByVal pRuc As String,
                                        ByVal pTipoPer As String, ByVal pTipo As String,
                                        ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_EXISTEPERSONA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Ruc", SqlDbType.VarChar).Value = pRuc
        Cmd.Parameters.Add("@TipoPer", SqlDbType.VarChar).Value = pTipoPer
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_EXISTEPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_Cont_ExisteTelefono_DireccionPersona
    Public Function Existe_TelefonoDierccionPersona(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                    ByVal psCodCliente As Double, ByVal psTelefono As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Cont_ExisteTelefono_DireccionPersona", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodCliente", SqlDbType.Float).Value = psCodCliente
        Cmd.Parameters.Add("@Telefono", SqlDbType.VarChar).Value = psTelefono
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cont_ExisteTelefono_DireccionPersona")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_DierccionPersona(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodCliente As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Cont_ExisteDireccionPersona", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodCliente", SqlDbType.Float).Value = psCodCliente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cont_ExisteDireccionPersona")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Obtener_UltimaPersona(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Cont_Obtener_UltimaPersona", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cont_Obtener_UltimaPersona")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Cont_Mascara(ByVal pCodEmpresa As String, ByVal pAño As String,
                                       ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("TBCONT_MASCARA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@lstAño", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCONT_MASCARA")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
