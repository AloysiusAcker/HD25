Imports System.Data
Imports System.Data.SqlClient
Public Class ClsAgencia

    Public Function Agencia_ListaEmpleadores(ByVal psConexion As String, ByVal pSexo As String, ByVal pAgencia_FechaIni As String,
                                             ByVal pAgencia_FechaFin As String, ByVal pAgencia_EstCivil As String,
                                             ByVal pAgencia_Apellido As String, ByVal pAgencia_DocNro As String, ByVal pAgencia_Estado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Agencia_ListaEmpleadores", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Agencia_Sexo", SqlDbType.VarChar).Value = pSexo
        Cmd.Parameters.Add("@Agencia_FechaIni", SqlDbType.VarChar).Value = pAgencia_FechaIni
        Cmd.Parameters.Add("@Agencia_FechaFin", SqlDbType.VarChar).Value = pAgencia_FechaFin
        Cmd.Parameters.Add("@Agencia_EstCivil", SqlDbType.VarChar).Value = pAgencia_EstCivil
        Cmd.Parameters.Add("@Agencia_Apellido", SqlDbType.VarChar).Value = pAgencia_Apellido
        Cmd.Parameters.Add("@Agencia_DocNro", SqlDbType.VarChar).Value = pAgencia_DocNro
        Cmd.Parameters.Add("@Agencia_Estado", SqlDbType.VarChar).Value = pAgencia_Estado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Agencia_ListaEmpleadores")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Agencia_ListaRequerimiento_xEmpleador(ByVal psConexion As String, ByVal pEmpleador As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Agencia_Requerimientos_xEmpleador", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@pdEmpleador", SqlDbType.Float).Value = pEmpleador
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Agencia_Requerimientos_xEmpleador")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    '
    Public Function Agencia_Insert_Empleador(ByVal psConexion As String, ByVal pApePat As String,
                                             ByVal pApeMat As String, ByVal pNombres As String,
                                             ByVal pRazonsocial As String, ByVal pUser As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Agencia_Insert_Empleadores", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ApePat", SqlDbType.VarChar).Value = pApePat
        Cmd.Parameters.Add("@Apemat", SqlDbType.VarChar).Value = pApeMat
        Cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = pNombres
        Cmd.Parameters.Add("@Razonsocial", SqlDbType.VarChar).Value = pRazonsocial
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Agencia_Insert_Empleadores")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Agencia_Update_Empleador(ByVal psConexion As String, ByVal pCodEmpleador As Double, ByVal pApePat As String,
                                             ByVal pApeMat As String, ByVal pNombres As String, ByVal pRazonsocial As String, ByVal pUser As String,
                                             ByVal pFechaNac As String, ByVal pEstCivil As String, ByVal pDirecDpto As String, ByVal pDirecProv As String,
                                             ByVal pDirecDist As String, ByVal pDireccion As String, ByVal pDirecTipo As String, pSexo As String,
                                             ByVal pDirecInterior As String, ByVal pDirecMz As String, ByVal pDirecLote As String, ByVal pDirecUrb As String,
                                             ByVal pDirecReferencia As String, ByVal pSeEntero As String, ByVal pRefirio As String, ByVal pAdultoDescrip As String,
                                             ByVal pAdultoCant As Integer, ByVal pNiñosDescrip As String, ByVal pNiñosCant As Integer, ByVal pMascotaDescrip As String,
                                             ByVal pMascotaCant As Integer, ByVal pNacUbigeo As String, ByVal pNacDpto As String, ByVal pNacProv As String,
                                             ByVal pNacDist As String, ByVal pObservacion As String, ByVal pEmail As String, ByVal pCasaTipo As String,
                                             ByVal pCasaNroPisos As Integer, ByVal pCasaNroSala As Integer, ByVal pCasaNroComedor As Integer, ByVal pCasaNroCocina As Integer,
                                             ByVal pCasaNroDormitorio As Integer, ByVal pCasaNroBaños As Integer, ByVal pTrabDescrip As String, ByVal pTrabCant As Integer,
                                             ByVal pOtros As String, ByVal pTelefonos As String, ByVal pDocTipo As String, ByVal pDocNro As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Agencia_Update_Empleadores", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpleador", SqlDbType.Float).Value = pCodEmpleador
        Cmd.Parameters.Add("@ApePat", SqlDbType.VarChar).Value = pApePat
        Cmd.Parameters.Add("@Apemat", SqlDbType.VarChar).Value = pApeMat
        Cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = pNombres
        Cmd.Parameters.Add("@Razonsocial", SqlDbType.VarChar).Value = pRazonsocial
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@FechaNac", SqlDbType.VarChar).Value = pFechaNac
        Cmd.Parameters.Add("@EstCivil", SqlDbType.VarChar).Value = pEstCivil
        Cmd.Parameters.Add("@DirecDpto", SqlDbType.VarChar).Value = pDirecDpto
        Cmd.Parameters.Add("@DirecProv", SqlDbType.VarChar).Value = pDirecProv
        Cmd.Parameters.Add("@DirecDist", SqlDbType.VarChar).Value = pDirecDist
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = pDireccion
        Cmd.Parameters.Add("@DirecTipo", SqlDbType.VarChar).Value = pDirecTipo
        Cmd.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = pSexo
        Cmd.Parameters.Add("@DirecInterior", SqlDbType.VarChar).Value = pDirecInterior
        Cmd.Parameters.Add("@DirecMz", SqlDbType.VarChar).Value = pDirecMz
        Cmd.Parameters.Add("@DirecLote", SqlDbType.VarChar).Value = pDirecLote
        Cmd.Parameters.Add("@DirecUrb", SqlDbType.VarChar).Value = pDirecUrb
        Cmd.Parameters.Add("@DirecReferencia", SqlDbType.VarChar).Value = pDirecReferencia
        Cmd.Parameters.Add("@SeEntero", SqlDbType.VarChar).Value = pSeEntero
        Cmd.Parameters.Add("@Refirio", SqlDbType.VarChar).Value = pRefirio
        Cmd.Parameters.Add("@AdultoDescrip", SqlDbType.VarChar).Value = pAdultoDescrip
        Cmd.Parameters.Add("@AdultoCant", SqlDbType.Int).Value = pAdultoCant
        Cmd.Parameters.Add("@NiñosDescrip", SqlDbType.VarChar).Value = pNiñosDescrip
        Cmd.Parameters.Add("@NiñosCant", SqlDbType.Int).Value = pNiñosCant
        Cmd.Parameters.Add("@MascotaDescrip", SqlDbType.VarChar).Value = pMascotaDescrip
        Cmd.Parameters.Add("@MascotaCant", SqlDbType.Int).Value = pMascotaCant
        Cmd.Parameters.Add("@NacUbigeo", SqlDbType.VarChar).Value = pNacUbigeo
        Cmd.Parameters.Add("@NacDpto", SqlDbType.VarChar).Value = pNacDpto
        Cmd.Parameters.Add("@NacProv", SqlDbType.VarChar).Value = pNacProv
        Cmd.Parameters.Add("@NacDist", SqlDbType.VarChar).Value = pNacDist
        Cmd.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = pObservacion
        Cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = pEmail
        Cmd.Parameters.Add("@CasaTipo", SqlDbType.VarChar).Value = pCasaTipo
        Cmd.Parameters.Add("@CasaNroPisos", SqlDbType.Int).Value = pCasaNroPisos
        Cmd.Parameters.Add("@CasaNroSala", SqlDbType.Int).Value = pCasaNroSala
        Cmd.Parameters.Add("@CasaNroComedor", SqlDbType.Int).Value = pCasaNroComedor
        Cmd.Parameters.Add("@CasaNroCocina", SqlDbType.Int).Value = pCasaNroCocina
        Cmd.Parameters.Add("@CasaNroDormitorio", SqlDbType.Int).Value = pCasaNroDormitorio
        Cmd.Parameters.Add("@CasaNroBaños", SqlDbType.Int).Value = pCasaNroBaños
        Cmd.Parameters.Add("@TrabDescrip", SqlDbType.VarChar).Value = pTrabDescrip
        Cmd.Parameters.Add("@TrabCant", SqlDbType.Int).Value = pTrabCant
        Cmd.Parameters.Add("@Otros", SqlDbType.VarChar).Value = pOtros
        Cmd.Parameters.Add("@Telefonos", SqlDbType.VarChar).Value = pTelefonos '@
        Cmd.Parameters.Add("@DocTipo", SqlDbType.VarChar).Value = pDocTipo
        Cmd.Parameters.Add("@DocNro", SqlDbType.VarChar).Value = pDocNro
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Agencia_Update_Empleadores")
        Da.Fill(Dt)
        Return Dt
    End Function


End Class
