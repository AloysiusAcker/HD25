Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class ModuloSeguridad
    Public Function Listar_Usuarios_SinAdm(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("STLISTA_USUARIOS_SINADM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_USUARIOS_SINADM")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Lista_Oficina
    'Prc_BuscarUsuarioSistema
    Public Function Listar_Oficina(ByVal psCodEmpresa As String, ByVal psCodGrupoEmp As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Lista_Oficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmp
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Lista_Oficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    'TBPERSONAL_DEFINE_OFICINA_CANAL

    Public Function Insert_OficinaCanal(ByVal psCodOficina As Double, ByVal psCanal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Insert_Oficina_Canal", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = psCodOficina
        Cmd.Parameters.Add("@Canal", SqlDbType.VarChar).Value = psCanal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Insert_Oficina_Canal")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Existe_Canal_xOficina
    Public Function Existe_CanalxOficina(ByVal psCodOficina As Double, ByVal psCanal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Existe_Canal_xOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = psCodOficina
        Cmd.Parameters.Add("@Canal", SqlDbType.VarChar).Value = psCanal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Existe_Canal_xOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Existe_Oficina
    Public Function Existe_Oficina(ByVal psCodGrupoEmp As Double, ByVal psCodEmpresa As String,
                                   ByVal psCodInterno As String, ByVal psNombre As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Existe_Oficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@GE", SqlDbType.Float).Value = psCodGrupoEmp
        Cmd.Parameters.Add("@GEE", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = psCodInterno
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = psNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Existe_Oficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Lista_Canal_xOficina
    Public Function Lista_CanalxOficina(ByVal psCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Lista_Canal_xOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = psCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Canal_xOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Delete_Canal_xOficina
    Public Function Delete_CanalxOficina(ByVal psCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Delete_Canal_xOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = psCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Delete_Canal_xOficina")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function BuscarDatos_xOficina(ByVal psCodEmpresa As String, ByVal psCodGrupoEmp As Double, ByVal pdCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Lista_xOfcina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@GEE", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@GE", SqlDbType.Float).Value = psCodGrupoEmp
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pdCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_xOfcina")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_InsertUpdate_Oficina
    Public Function InsUpd_Oficina(ByVal psCodEmpresa As String, ByVal psCodGrupoEmp As Double,
                                   ByVal pdCodOficina As Double, ByVal psCodInterno As String,
                                   ByVal psNombre As String, ByVal psDireccion As String,
                                   ByVal psDpto As String, ByVal psProv As String, ByVal psDist As String,
                                   ByVal psLatitud As String, ByVal psLongitud As String,
                                   ByVal pstipo As String, ByVal psCanal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_InsertUpdate_Oficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@GE", SqlDbType.Float).Value = psCodGrupoEmp
        Cmd.Parameters.Add("@GEE", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Of_Codigo", SqlDbType.Float).Value = pdCodOficina
        Cmd.Parameters.Add("@Of_CodInterno", SqlDbType.VarChar).Value = psCodInterno
        Cmd.Parameters.Add("@Of_Nombre", SqlDbType.VarChar).Value = psNombre
        Cmd.Parameters.Add("@Of_Direccion", SqlDbType.VarChar).Value = psDireccion
        Cmd.Parameters.Add("@Of_Dpto", SqlDbType.VarChar).Value = psDpto
        Cmd.Parameters.Add("@Of_Prov", SqlDbType.VarChar).Value = psProv
        Cmd.Parameters.Add("@Of_Dist", SqlDbType.VarChar).Value = psDist
        Cmd.Parameters.Add("@Of_Latitud", SqlDbType.VarChar).Value = psLatitud
        Cmd.Parameters.Add("@Of_Longitud", SqlDbType.VarChar).Value = psLongitud
        Cmd.Parameters.Add("@Of_Tipo", SqlDbType.VarChar).Value = pstipo
        Cmd.Parameters.Add("@Of_Canal", SqlDbType.VarChar).Value = psCanal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_InsertUpdate_Oficina")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Busca_UsuarioSistema(ByVal psCodInterno As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("Prc_BuscarUsuarioSistema", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = psCodInterno
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_BuscarUsuarioSistema")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Usuarios() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_USUARIOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_USUARIOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Usuarios_NoPersonal() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_USUARIOS_NOPERSONAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_USUARIOS_NOPERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Modalidad() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_MODALIDAD", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_MODALIDAD")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_NivelAcc() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_NIVELACC", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_NIVELACC")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ingresar_UltimoNUsuario(ByVal pCodigo As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STUPD_ULTIMO_NUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STUPD_ULTIMO_NUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Extrae_UltimaOficina() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("Prc_Extrae_UltimaOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Extrae_UltimaOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Consigue_UltimoPUsuario(ByVal pAño As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STULTIMO_PUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STULTIMO_PUSUARIO")
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
    Public Function Insertar_UserGrpoEmps(ByVal pUserCodigo As String, ByVal pGrpoEmpsCodigo As Double, ByVal pEmpresaCodigo As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINS_USUARI_GRPOEMPS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@UserCodigo", SqlDbType.VarChar).Value = pUserCodigo
        Cmd.Parameters.Add("@GrpoEmpsCodigo", SqlDbType.Int).Value = pGrpoEmpsCodigo
        Cmd.Parameters.Add("@EmpresaCodigo", SqlDbType.VarChar).Value = pEmpresaCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINS_USUARI_GRPOEMPS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Subscriptor(ByVal pUserCodigo As String, ByVal pUserNombre As String, ByVal pUserApepat As String,
                                     ByVal pUserApemat As String, ByVal pUserTelef As String, ByVal pUserCorreo As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINS_SUBSCRIPTOR", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@UserCodigo", SqlDbType.VarChar).Value = pUserCodigo
        Cmd.Parameters.Add("@UserNombre", SqlDbType.VarChar).Value = pUserNombre
        Cmd.Parameters.Add("@UserApepat", SqlDbType.VarChar).Value = pUserApepat
        Cmd.Parameters.Add("@UserApemat", SqlDbType.VarChar).Value = pUserApemat
        Cmd.Parameters.Add("@UserTelef", SqlDbType.VarChar).Value = pUserTelef
        Cmd.Parameters.Add("@UserCorreo", SqlDbType.VarChar).Value = pUserCorreo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINS_SUBSCRIPTOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Visitas() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINS_VISITAS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINS_VISITAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Modificar_Visitas() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STUPD_VISITAS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STUPD_VISITAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Extraer_UltimoNUsuario() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STEXTRAE_ULTIMO_NUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STEXTRAE_ULTIMO_NUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Consigue_UltimoNUsuario() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STULTIMO_NUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STULTIMO_NUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Visitas() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTAR_VISITAS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTAR_VISITAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Verificar_Correo(ByVal pCorreoElectronico As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STVERIFICAR_CORREO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CorreoElectronico", SqlDbType.VarChar).Value = pCorreoElectronico
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STVERIFICAR_CORREO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Usuarios(ByVal pEsPersonal As String, ByVal pCodUsuario As String, ByVal pCodInterno As String,
                                    ByVal pApepat As String, ByVal pApemat As String, ByVal pNombres As String,
                                    ByVal pFechaIni As String, ByVal pFechaFin As String, ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_USUARIOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@TipoPer", SqlDbType.VarChar).Value = pEsPersonal
        Cmd.Parameters.Add("@Codigo", SqlDbType.VarChar).Value = pCodUsuario
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@ApePat", SqlDbType.VarChar).Value = pApepat
        Cmd.Parameters.Add("@ApeMat", SqlDbType.VarChar).Value = pApemat
        Cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = pNombres
        Cmd.Parameters.Add("@FechaInicio", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_USUARIOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Verifica_CodInterno_Usuario(ByVal psCodInterno As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STVERIFICA_CODINTERNO_USUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psCodInterno
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STVERIFICA_CODINTERNO_USUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Extrae_UltimoPUsuario(ByVal pAño As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STEXTRAE_ULTIMO_PUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = pAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STEXTRAE_ULTIMO_PUSUARIO")
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
    Public Function Existe_PerfilxUsuario(ByVal pCodUsuario As String, ByVal pCodPerfil As Double, ByVal pTipoconsulta As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STEXISTE_PERFILXUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pCodUsuario
        Cmd.Parameters.Add("@CodPerfil", SqlDbType.Float).Value = pCodPerfil
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = pTipoconsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STEXISTE_PERFILXUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_Pagina(ByVal pNombrePag As String, ByVal pTipoConsulta As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STEXISTE_PAGINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@NombrePag", SqlDbType.VarChar).Value = pNombrePag
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = pTipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STEXISTE_PAGINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Perfiles(ByVal pUser As String, ByVal pTipoConsulta As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_PERFILES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = pTipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_PERFILES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_PerfilxUsuarios(ByVal pCodUnicoPerfil As Integer, ByVal pUsuario As String, ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_PERFILXUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUnicoPerfil", SqlDbType.Int).Value = pCodUnicoPerfil
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERFILXUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Perfil(ByVal pCodPerfil As Double, ByVal pCodUnico As String,
                                  ByVal pDescripcion As String, ByVal pCodModInteg As Double,
                                  ByVal pCodGrpoEmp As Double, ByVal pCodEmpresa As String,
                                  ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_PERFIL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodPerfil", SqlDbType.Float).Value = pCodPerfil
        Cmd.Parameters.Add("@CodUnico", SqlDbType.VarChar).Value = pCodUnico
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@CodModInteg", SqlDbType.Float).Value = pCodModInteg
        Cmd.Parameters.Add("@CodGrpoEmp", SqlDbType.Float).Value = pCodGrpoEmp
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERFIL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_Perfil(ByVal pCodUnicoPerfil As String, ByVal pCodModInteg As Double,
                                  ByVal pCodGrpoEmp As Double, ByVal pCodEmpresa As String,
                                  ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STEXISTE_PERFIL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUnicoPerfil", SqlDbType.VarChar).Value = pCodUnicoPerfil
        Cmd.Parameters.Add("@CodModInteg", SqlDbType.Float).Value = pCodModInteg
        Cmd.Parameters.Add("@CodGrpoEmp", SqlDbType.Float).Value = pCodGrpoEmp
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERFIL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_GrupoEmpresa(ByVal pUsuario As String, ByVal pTipoListado As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_GRUPOEMPRESA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoListado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_GRUPOEMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Empresa(ByVal pUsuario As String, ByVal pCodGrupoEmp As Double, ByVal pTipoListado As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_EMPRESA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pCodGrupoEmp
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoListado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_EMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_UsuarioxGrpoEmp(ByVal pUsuario As String, ByVal pCodGrupoEmpresa As Double, ByVal pCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STEXISTE_USUARIOXGRUPOEMP", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STEXISTE_USUARIOXGRUPOEMP")
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
    Public Function Lista_GrupoEmpresa_xUsuario(ByVal pUsuario As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_GRUPOEMPRESAXUSUARIO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_GRUPOEMPRESAXUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ModuloIntegracion(ByVal pTipoListado As String, ByVal pCodGrupoEmp As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("STLISTA_MODULO_INTEGRACION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pCodGrupoEmp
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoListado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_MODULO_INTEGRACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_PerfilxModIntegracion(ByVal pCodEmpresa As String, ByVal pCodGrupoEmp As Double, ByVal pCodModIntegracion As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_PERFILXMODINTEGRACION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = pCodGrupoEmp
        Cmd.Parameters.Add("@CodModIntegracion", SqlDbType.Float).Value = pCodModIntegracion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_PERFILXMODINTEGRACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Pagina() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_PAGINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_PAGINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Modulo() As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_MODULOS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_MODULOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Pagina(ByVal pCodPagina As Double, ByVal pNombrePag As String, ByVal pDescriPag As String,
                                  ByVal pEstadoPag As String, ByVal pTipoPag As String, ByVal pDisposPag As String,
                                  ByVal pObservPag As String, ByVal pModulo As Double, ByVal User As String,
                                  ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_PAGINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodPagina", SqlDbType.Float).Value = pCodPagina
        Cmd.Parameters.Add("@NombrePag", SqlDbType.VarChar).Value = pNombrePag
        Cmd.Parameters.Add("@DescriPag", SqlDbType.VarChar).Value = pDescriPag
        Cmd.Parameters.Add("@EstadoPag", SqlDbType.VarChar).Value = pEstadoPag
        Cmd.Parameters.Add("@TipoPag", SqlDbType.VarChar).Value = pTipoPag
        Cmd.Parameters.Add("@DisposPag", SqlDbType.VarChar).Value = pDisposPag
        Cmd.Parameters.Add("@ObservPag", SqlDbType.VarChar).Value = pObservPag
        Cmd.Parameters.Add("@Modulo", SqlDbType.Float).Value = pModulo
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = User
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PAGINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_PaginaxPerfil(ByVal pCodUnicoPerfil As Integer, ByVal pCodPag As Integer) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUP_PERFILPAGINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUnicoPerfil", SqlDbType.Int).Value = pCodUnicoPerfil
        Cmd.Parameters.Add("@CodPag", SqlDbType.Int).Value = pCodPag
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERFILPAGINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_PaginasxModInteg(ByVal pModulo As Integer) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_PAGINAS_XMODINTEG", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodModInteg", SqlDbType.Int).Value = pModulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_PAGINAS_XMODINTEG")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_PaginasxPerfiles(ByVal pPerfil As Integer) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STLISTA_PAGINAS_XPERFILES", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodPerfil", SqlDbType.Int).Value = pPerfil
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STLISTA_PAGINAS_XPERFILES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_PaginaxPerfil(ByVal pCodUnicoPerfil As Integer, ByVal pCodPag As Integer) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_PERFILPAGINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUnicoPerfil", SqlDbType.Int).Value = pCodUnicoPerfil
        Cmd.Parameters.Add("@CodPag", SqlDbType.Int).Value = pCodPag
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_PERFILPAGINA")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Delete_PaginaxPerfil(ByVal pCodUnicoPerfil As Integer, ByVal pCodPag As Integer) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_QUITARPERFILPAGINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUnicoPerfil", SqlDbType.Int).Value = pCodUnicoPerfil
        Cmd.Parameters.Add("@CodPag", SqlDbType.Int).Value = pCodPag
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_QUITARPERFILPAGINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_IngresarPassword(ByVal pPassInicial As String, ByVal pCodUsuario As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STUPD_INGRESARPASSAWORD", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@PassInicial", SqlDbType.VarChar).Value = pPassInicial
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pCodUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STUPD_INGRESARPASSAWORD")
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

    'Prc_Buscar_Personal
    Public Function Buscar_Personal(ByVal dCodGrupoEmpresa As Double, ByVal pCodempresa As String,
                                    ByVal pDni As String, ByVal pNombres As String,
                                    ByVal pApellidos As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Buscar_Personal", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = dCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodempresa
        Cmd.Parameters.Add("@Dni", SqlDbType.VarChar).Value = pDni
        Cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = pNombres
        Cmd.Parameters.Add("@Apellidos", SqlDbType.VarChar).Value = pApellidos
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Buscar_Personal")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Buscar_XPersonal
    Public Function Buscar_xPersonal(ByVal dCodGrupoEmpresa As Double, ByVal pCodempresa As String,
                                    ByVal pCodPersonal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Buscar_XPersonal", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = dCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodempresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = pCodPersonal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Buscar_XPersonal")
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
    Public Function Existe_RelacionModInteg(ByVal dCodModulo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STEXISTE_MODULO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodModulo", SqlDbType.Float).Value = dCodModulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STEXISTE_MODULO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_ModInteg(ByVal dCodModulo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STEXISTE_MODULOINTEG", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodModulo", SqlDbType.Float).Value = dCodModulo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STEXISTE_MODULOINTEG")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_ModInteg(ByVal dCodModulo As Double, ByVal dCodModInteg As Double, ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_MODINTEGRACION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodModulo", SqlDbType.Float).Value = dCodModulo
        Cmd.Parameters.Add("@CodModInteg", SqlDbType.Float).Value = dCodModInteg
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_MODINTEGRACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Modulo(ByVal dCodModulo As Double, ByVal pnombre As String, ByVal pdescripcion As String,
                                  ByVal pEstado As String, ByVal pUser As String, ByVal pTipoIngreso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("STINSUPD_MODULO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodModulo", SqlDbType.Float).Value = dCodModulo
        Cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = pnombre
        Cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = pdescripcion
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("STINSUPD_MODULO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function List_AreaxPErsonal(ByVal psCodPersonal As String, ByVal psCodEmpresa As String,
                                       ByVal psCodGrupoEmpresa As Double, ByVal psConexion As String) As DataTable
        Dim CnA As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SP_LIST_AREAXPERSONAL", CnA) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LIST_AREAXPERSONAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Obtener_Sigla(ByVal psCodGrupoEmpresa As Double) As DataTable
        Dim CnA As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_GENERAL_OBTENER_SIGLA", CnA) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_GENERAL_OBTENER_SIGLA")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Obtener_ModuloxPagina(ByVal psPagNombre As String) As DataTable
        Dim CnA As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("PRC_PAGINA_OBTENER_MODULO", CnA) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@Nombre_Pag", SqlDbType.VarChar).Value = psPagNombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_PAGINA_OBTENER_MODULO")
        Da.Fill(Dt)
        Return Dt
    End Function
    'PRC_TEMAYUDA_RELACION_DATOS_XMODULO
    Public Function Obtener_TablaRelacion(ByVal psConexion As String, ByVal psCodModulo As Double, ByVal psTablaRelacion As String) As DataTable
        Dim CnA As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_TEMAYUDA_RELACION_DATOS_XMODULO", CnA) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodModulo", SqlDbType.Float).Value = psCodModulo
        Cmd.Parameters.Add("@TablaRelacion", SqlDbType.VarChar).Value = psTablaRelacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_TEMAYUDA_RELACION_DATOS_XMODULO")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Usuarios_Perfil400
    Public Function Usuarios_Perfil400(ByVal pCodUsuario As String) As DataTable
        Dim CnA As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("Prc_Usuarios_Perfil400", CnA) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodUsuario", SqlDbType.VarChar).Value = pCodUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Usuarios_Perfil400")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class