Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class clsControlPersonal '49267
    Public Function InsUpd_PersonalEmpresa(ByVal pUser As String, ByVal pCodPersonal As String,
                                           ByVal dCodGE As Double, ByVal pCodGEE As String,
                                           ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STINSUPD_PERSONALEMPRESA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@CodGE", SqlDbType.Float).Value = dCodGE
        Cmd.Parameters.Add("@CodGEE", SqlDbType.VarChar).Value = pCodGEE
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERSONALEMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Personal(ByVal pUser As String, ByVal pCodPersonal As String,
                                    ByVal dCodGE As Double, ByVal pCodGEE As String,
                                    ByVal pTipoIngreso As String, Optional ByVal pCodEstado As String = "",
                                    Optional ByVal pApePat As String = "", Optional ByVal pApeMat As String = "",
                                    Optional ByVal pNombres As String = "", Optional ByVal pSexo As String = "",
                                    Optional ByVal pEMail As String = "", Optional ByVal pPais As String = "",
                                    Optional ByVal pDireccion As String = "", Optional ByVal pCodInterno As String = "") As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STINSUPD_PERSONAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@CodGE", SqlDbType.Float).Value = dCodGE
        Cmd.Parameters.Add("@CodGEE", SqlDbType.VarChar).Value = pCodGEE
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@CodEstado", SqlDbType.VarChar).Value = pCodEstado
        Cmd.Parameters.Add("@ApePat", SqlDbType.VarChar).Value = pApePat
        Cmd.Parameters.Add("@apeMat", SqlDbType.VarChar).Value = pApeMat
        Cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = pNombres
        Cmd.Parameters.Add("@Sexo", SqlDbType.VarChar).Value = pSexo
        Cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = pEMail
        Cmd.Parameters.Add("@Pais", SqlDbType.VarChar).Value = pPais
        Cmd.Parameters.Add("@Direccion", SqlDbType.VarChar).Value = pDireccion
        Cmd.Parameters.Add("@codInterno", SqlDbType.VarChar).Value = pCodInterno
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_PersonalUbigeo(ByVal pCodPersonal As String, ByVal pDpto As String,
                                          ByVal pProv As String, ByVal pDist As String,
                                          ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STINSUPD_PERSONALUBIGEO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Cmd.Parameters.Add("@Dpto", SqlDbType.Float).Value = pDpto
        Cmd.Parameters.Add("@Prov", SqlDbType.VarChar).Value = pProv
        Cmd.Parameters.Add("@Dist", SqlDbType.VarChar).Value = pDist
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERSONALUBIGEO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Ultimo_PUsuario(ByVal pAño As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp) 'INS_ULTIMO_PUSUARIO
        Dim Cmd As New SqlCommand("STINS_ULTIMO_PUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINS_ULTIMO_PUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Ultimo_PUsuario(ByVal pCodigo As String, ByVal pAño As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STUPD_ULTIMO_PUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = pCodigo
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STUPD_ULTIMO_PUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Ultimo_PUsuario2(ByVal pNumCorr As String, ByVal pAño As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STUPD_ULTIMO_PUSUARIO2", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@NumCorr", SqlDbType.VarChar).Value = pNumCorr
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STUPD_ULTIMO_PUSUARIO2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_Personal(ByVal pUsuario As String, ByVal pTipoConsulta As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("VERIFICA_PERSONAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = pTipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("VERIFICA_PERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Asistencia(ByVal psCodEmpresa As String, ByVal pdCodGrupoEmpresa As Double,
                                     ByVal psFechaIni As String, ByVal psFechaFin As String,
                                     ByVal psCodPersonal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_CONTROL_LISTA_ASISTENCIA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdCodGrupoEmpresa
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_CONTROL_LISTA_ASISTENCIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Asistencia_Variable(ByVal psCodEmpresa As String, ByVal pdCodGrupoEmpresa As Double,
                                     ByVal psCodPersonal As String, ByVal psNumeroDia As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_CONTROL_LISTA_ASISTENCIA_VARIABLE", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdCodGrupoEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Cmd.Parameters.Add("@NumeroDia", SqlDbType.VarChar).Value = psNumeroDia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_CONTROL_LISTA_ASISTENCIA_VARIABLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Asistencia_Diferido(ByVal psCodEmpresa As String, ByVal pdCodGrupoEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_CONTROL_LISTA_ASISTENCIA_DIRECTA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_CONTROL_LISTA_ASISTENCIA_DIRECTA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Horario_xCargo(ByVal psCodEmpresa As String, ByVal pdCodGrupoEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_CONTROL_LISTA_HORARIO_XCARGO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdCodGrupoEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_CONTROL_LISTA_HORARIO_XCARGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Cargo(ByVal psCodEmpresa As String, ByVal pdCodGrupoEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_CONTROL_LISTA_CARGO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdCodGrupoEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_CONTROL_LISTA_CARGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Personal(ByVal pSysEst As String, ByVal pCodest As String,
                                    ByVal pApepat As String, ByVal pApemat As String,
                                    ByVal pNombres As String, ByVal dCodGrupoEmpresa As Double,
                                    ByVal pCodempresa As String, ByVal pTipoConsulta As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STLISTA_PERSONAL2", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@SysEst", SqlDbType.VarChar).Value = pSysEst
        Cmd.Parameters.Add("@Codest", SqlDbType.VarChar).Value = pCodest
        Cmd.Parameters.Add("@Apepat", SqlDbType.VarChar).Value = pApepat
        Cmd.Parameters.Add("@Apemat", SqlDbType.VarChar).Value = pApemat
        Cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = pNombres
        Cmd.Parameters.Add("@GE", SqlDbType.Float).Value = dCodGrupoEmpresa
        Cmd.Parameters.Add("@GEE", SqlDbType.VarChar).Value = pCodempresa
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_PERSONAL2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Personal(ByVal pCodGrupo As Double, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STLISTA_PERSONAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = pCodGrupo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_PERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Personal_Asisencia(ByVal psCodEmpresa As String, ByVal pdCodGrupoEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_CONTROL_LISTA_PERSONAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pdCodGrupoEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_CONTROL_LISTA_PERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
