Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Public Class ModuloGeneral
    'Prc_DatosEmpresa
    Public Function Datos_Empresa(ByVal psConexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_DatosEmpresa", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_DatosEmpresa")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_UsuarioxCategoria(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                              ByVal psCategoria As String, ByVal psUsuario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPLISTA_EXISTE_USUARIOXCATEGORIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = psCategoria
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTA_EXISTE_USUARIOXCATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_Usuario_Categoria(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                               ByVal psCategoria As String, ByVal psUsuario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINS_USUARIO_CATEGORIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = psCategoria
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINS_USUARIO_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Delete_Usuario_Categoria(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                               ByVal psCategoria As String, ByVal psUsuario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPDEL_USUARIO_CATEGORIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = psCategoria
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPDEL_USUARIO_CATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_UsuarioxCategoria(ByVal psConexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPLISTA_CATEGORIAXUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTA_CATEGORIAXUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_DOCUMENTOS(ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("PRC_LISTA_DOCUMENTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_LISTA_DOCUMENTOS")
        Da.Fill(Dt)
        Return Dt
    End Function


    Public Function Lista_UsuarioxCategoria_Marcar(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                   ByVal psCategoria As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPLISTA_USUARIO_XCATEGORIA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Categoria", SqlDbType.VarChar).Value = psCategoria
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTA_USUARIO_XCATEGORIA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BDC_InsUpd_Contador(ByVal pCodEmpresa As String, ByVal pCodBaseDatos As Double,
                                        ByVal pTipoModificacion As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPBDC_INSUPD_ACTCONTADOR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBaseDatos", SqlDbType.Int).Value = pCodBaseDatos
        Cmd.Parameters.Add("@TipoModificacion", SqlDbType.VarChar).Value = pTipoModificacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPBDC_INSUPD_ACTCONTADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BDC_Lista_ReiniciarContador(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPBDC_LISTA_PARAREINICIAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPBDC_LISTA_PARAREINICIAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BDC_InsUpd_FechaContador(ByVal pFecha As String, ByVal pFechaFin As String,
                                             ByVal pTipoModificacion As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPBDC_INSUPD_ACTFECHACONTADOR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@TipoModificacion", SqlDbType.VarChar).Value = pTipoModificacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPBDC_INSUPD_ACTFECHACONTADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BDC_Top10(ByVal pCodEmpresa As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPBDC_LISTA_TOP10", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPBDC_LISTA_TOP10")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BDC_Lista_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodAplicativo As Double,
                                        ByVal pCodProducto As Double, ByVal pCodSubProd As Double,
                                        ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPBDC_LISTA_BASEDATOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Int).Value = pCodSubProd
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPBDC_LISTA_BASEDATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BDC_InsUpd_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodBaseDatos As Double,
                                         ByVal pCodAplicativo As Double, ByVal pCodProducto As Double,
                                         ByVal pCodSubProd As Double, ByVal pTransaccion As String,
                                         ByVal pConsulta As String, ByVal pSolucion As String,
                                         ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPBDC_INSUPD_BASEDATOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
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
        Dim Dt As New DataTable("SPBDC_INSUPD_BASEDATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_TablasEspeciales(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPTBESP_LISTA_TBESPECIAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPTBESP_LISTA_TBESPECIAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Existe_TablaEspecial(ByVal pPrefijo As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPTBESP_EXISTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Prefijo", SqlDbType.VarChar).Value = pPrefijo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPTBESP_EXISTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insert_TablaEspecial(ByVal pDescripcion As String,
                                         ByVal pPrefijo As String, ByVal pTabla1 As String,
                                         ByVal pTabla2 As String, ByVal pTabla3 As String,
                                         ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPTBESP_INSERT_DESCRIPCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Descrip", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@Prefijo", SqlDbType.VarChar).Value = pPrefijo
        Cmd.Parameters.Add("@Tabla1", SqlDbType.VarChar).Value = pTabla1
        Cmd.Parameters.Add("@Tabla2", SqlDbType.VarChar).Value = pTabla2
        Cmd.Parameters.Add("@Tabla3", SqlDbType.VarChar).Value = pTabla3
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPTBESP_INSERT_DESCRIPCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Update_TablaEspecial(ByVal dCodigo As Double, ByVal pDescripcion As String,
                                         ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SPTBESP_UPDATE_DESCRIPCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = dCodigo
        Cmd.Parameters.Add("@Descrip", SqlDbType.VarChar).Value = pDescripcion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPTBESP_UPDATE_DESCRIPCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_TablasEspeciales(ByVal CodigoTablas As String, ByVal TablasUso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBCTABLAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@TABLAS_CODIGO", SqlDbType.VarChar).Value = CodigoTablas
        Cmd.Parameters.Add("@TABLAS_USO", SqlDbType.VarChar).Value = TablasUso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBCTABLAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_TablasElementos(ByVal elementos As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBCELEMEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ELEMEN_TABLA", SqlDbType.VarChar).Value = elementos
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBCELEMEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_TablasEspeciales_Uso(ByVal CodigoTablas As String, ByVal TablasUso As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBCTABLAS_USO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@TABLAS_CODIGO", SqlDbType.VarChar).Value = CodigoTablas
        Cmd.Parameters.Add("@TABLAS_USO", SqlDbType.VarChar).Value = TablasUso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBCTABLAS_USO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_TablasEspeciales(ByVal TABLAS_CODIGO As String, ByVal TABLAS_DESCRIPCION As String, ByVal TABLAS_USO As String,
                                              ByVal TABLAS_SYS_CRE As String, ByVal TABLAS_SYS_MOD As String, ByVal TABLAS_VER As String, ByVal USER As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_INS_TBCTABLAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@TABLAS_CODIGO", SqlDbType.VarChar).Value = TABLAS_CODIGO
        Cmd.Parameters.Add("@TABLAS_DESCRIPCION", SqlDbType.VarChar).Value = TABLAS_DESCRIPCION
        Cmd.Parameters.Add("@TABLAS_USO", SqlDbType.VarChar).Value = TABLAS_USO
        Cmd.Parameters.Add("@TABLAS_SYS_CRE", SqlDbType.VarChar).Value = TABLAS_SYS_CRE
        Cmd.Parameters.Add("@TABLAS_SYS_MOD", SqlDbType.VarChar).Value = TABLAS_SYS_MOD
        Cmd.Parameters.Add("@TABLAS_VER", SqlDbType.VarChar).Value = TABLAS_VER
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = USER
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBCTABLAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Update_TablasEspeciales(ByVal TABLAS_CODIGO As String, ByVal TABLAS_DESCRIPCION As String, ByVal TABLAS_USO As String,
                                            ByVal TABLAS_SYS_CRE As String, ByVal TABLAS_SYS_MOD As String, ByVal TABLAS_SYS_EST As String,
                                            ByVal TABLAS_VER As String, ByVal USER As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_UPD_TBCTABLAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@TABLAS_CODIGO", SqlDbType.VarChar).Value = TABLAS_CODIGO
        Cmd.Parameters.Add("@TABLAS_DESCRIPCION", SqlDbType.VarChar).Value = TABLAS_DESCRIPCION
        Cmd.Parameters.Add("@TABLAS_USO", SqlDbType.VarChar).Value = TABLAS_USO
        Cmd.Parameters.Add("@TABLAS_SYS_CRE", SqlDbType.VarChar).Value = TABLAS_SYS_CRE
        Cmd.Parameters.Add("@TABLAS_SYS_MOD", SqlDbType.VarChar).Value = TABLAS_SYS_MOD
        Cmd.Parameters.Add("@TABLAS_SYS_EST", SqlDbType.VarChar).Value = TABLAS_SYS_EST
        Cmd.Parameters.Add("@TABLAS_VER", SqlDbType.VarChar).Value = TABLAS_VER
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = USER
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_UPD_TBCTABLAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Insertar_TablasElementos(ByVal ELEMEN_TABLA As String, ByVal ELEMEN_CODIGO As String, ByVal ELEMEN_VALOR As String,
                                             ByVal ELEMEN_SYS_CRE As String, ByVal ELEMEN_SYS_MOD As String, ByVal ELEMEN_CODIGO_MINIS As String,
                                             ByVal ELEMEN_VALOR_MINIS As String, ByVal USER As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_INS_TBCELEMEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ELEMEN_TABLA", SqlDbType.VarChar).Value = ELEMEN_TABLA
        Cmd.Parameters.Add("@ELEMEN_CODIGO", SqlDbType.VarChar).Value = ELEMEN_CODIGO
        Cmd.Parameters.Add("@ELEMEN_VALOR", SqlDbType.VarChar).Value = ELEMEN_VALOR
        Cmd.Parameters.Add("@ELEMEN_SYS_CRE", SqlDbType.VarChar).Value = ELEMEN_SYS_CRE
        Cmd.Parameters.Add("@ELEMEN_SYS_MOD", SqlDbType.VarChar).Value = ELEMEN_SYS_MOD
        Cmd.Parameters.Add("@ELEMEN_CODIGO_MINIS", SqlDbType.VarChar).Value = ELEMEN_CODIGO_MINIS
        Cmd.Parameters.Add("@ELEMEN_VALOR_MINIS", SqlDbType.VarChar).Value = ELEMEN_VALOR_MINIS
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = USER
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBCELEMEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Update_TablasElementos(ByVal ELEMEN_TABLA As String, ByVal ELEMEN_CODIGO As String, ByVal ELEMEN_VALOR As String,
                                           ByVal ELEMEN_SYS_CRE As String, ByVal ELEMEN_SYS_MOD As String, ByVal ELEMEN_SYS_EST As String, ByVal ELEMEN_CODIGO_MINIS As String,
                                           ByVal ELEMEN_VALOR_MINIS As String, ByVal USER As String, ByVal ELEMEN_COD As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_UPD_TBCELEMEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ELEMEN_TABLA", SqlDbType.VarChar).Value = ELEMEN_TABLA
        Cmd.Parameters.Add("@ELEMEN_CODIGO", SqlDbType.VarChar).Value = ELEMEN_CODIGO
        Cmd.Parameters.Add("@ELEMEN_VALOR", SqlDbType.VarChar).Value = ELEMEN_VALOR
        Cmd.Parameters.Add("@ELEMEN_SYS_CRE", SqlDbType.VarChar).Value = ELEMEN_SYS_CRE
        Cmd.Parameters.Add("@ELEMEN_SYS_MOD", SqlDbType.VarChar).Value = ELEMEN_SYS_MOD
        Cmd.Parameters.Add("@ELEMEN_SYS_EST", SqlDbType.VarChar).Value = ELEMEN_SYS_EST
        Cmd.Parameters.Add("@ELEMEN_CODIGO_MINIS", SqlDbType.VarChar).Value = ELEMEN_CODIGO_MINIS
        Cmd.Parameters.Add("@ELEMEN_VALOR_MINIS", SqlDbType.VarChar).Value = ELEMEN_VALOR_MINIS
        Cmd.Parameters.Add("@ELEMEN_COD", SqlDbType.VarChar).Value = ELEMEN_COD
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = USER
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_UPD_TBCELEMEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Delete_TablasElementos(ByVal ELEMEN_TABLA As String, ByVal ELEMEN_CODIGO As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_DEL_TBCELEMEN", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@ELEMEN_TABLA", SqlDbType.VarChar).Value = ELEMEN_TABLA
        Cmd.Parameters.Add("@ELEMEN_CODIGO", SqlDbType.VarChar).Value = ELEMEN_CODIGO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_DEL_TBCELEMEN")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Update_TablasEspeciales_Uso(ByVal TABLAS_CODIGO As String, ByVal TABLAS_USO As String,
                                                ByVal TABLAS_VER As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_UPD_TBCTABLAS_USO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@TABLAS_CODIGO", SqlDbType.VarChar).Value = TABLAS_CODIGO
        Cmd.Parameters.Add("@TABLAS_USO", SqlDbType.VarChar).Value = TABLAS_USO
        Cmd.Parameters.Add("@TABLAS_VER", SqlDbType.VarChar).Value = TABLAS_VER
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_UPD_TBCTABLAS_USO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Documentos(ByVal CONEXION As String, ByVal TEMA_AYUDA_CODIGO As Double, ByVal TEMA_AYUDA_TIPOINGRESO As String,
                                   ByVal TEMA_AYUDA_TIPODOC As String, ByVal TEMA_AYUDA_CATEGORIA As String,
                                   ByVal TEMA_AYUDA_FECHA_INGRESO As String, ByVal TEMA_AYUDA_OFICINA As Double,
                                   ByVal TEMA_AYUDA_NOMBRE_DOC As String, ByVal TEMA_AYUDA_DESCRIPCION As String,
                                   ByVal TEMA_AYUDA_INSPECCION As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(CONEXION)
        Dim Cmd As New SqlCommand("SP_INS_TBTEMA_AYUDA_GENERAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@TEMA_AYUDA_CODIGO", SqlDbType.Float).Value = TEMA_AYUDA_CODIGO
        Cmd.Parameters.Add("@TEMA_AYUDA_TIPOINGRESO", SqlDbType.VarChar).Value = TEMA_AYUDA_TIPOINGRESO
        Cmd.Parameters.Add("@TEMA_AYUDA_TIPODOC", SqlDbType.VarChar).Value = TEMA_AYUDA_TIPODOC
        Cmd.Parameters.Add("@TEMA_AYUDA_CATEGORIA", SqlDbType.VarChar).Value = TEMA_AYUDA_CATEGORIA
        Cmd.Parameters.Add("@TEMA_AYUDA_FECHA_INGRESO", SqlDbType.VarChar).Value = TEMA_AYUDA_FECHA_INGRESO
        Cmd.Parameters.Add("@TEMA_AYUDA_OFICINA", SqlDbType.Float).Value = TEMA_AYUDA_OFICINA
        Cmd.Parameters.Add("@TEMA_AYUDA_NOMBRE_DOC", SqlDbType.VarChar).Value = TEMA_AYUDA_NOMBRE_DOC
        Cmd.Parameters.Add("@TEMA_AYUDA_DESCRIPCION", SqlDbType.VarChar).Value = TEMA_AYUDA_DESCRIPCION
        Cmd.Parameters.Add("@TEMA_AYUDA_INSPECCION", SqlDbType.Float).Value = TEMA_AYUDA_INSPECCION
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_TBTEMA_AYUDA_GENERAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Ayuda_General(ByVal psConexion As String, ByVal pdOficina As Double, ByVal FechaIng As String,
                                         ByVal FechaFin As String, ByVal NroInspecc As Double, ByVal psTipoIngreso As String,
                                         ByVal psCodEmpresa As String, ByVal psUsuario As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPLISTA_TEMA_AYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@Oficina", SqlDbType.Float).Value = pdOficina
        Cmd.Parameters.Add("@FechaIng", SqlDbType.VarChar).Value = FechaIng
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = FechaFin
        Cmd.Parameters.Add("@NroInspec", SqlDbType.Float).Value = NroInspecc
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = psTipoIngreso
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = psUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTA_TEMA_AYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_TemaAyuda(ByVal Ruta_GrEmp As String, ByVal pCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SPLISTA_TEMA_AYUDA_XCODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTA_TEMA_AYUDA_XCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Busca_Extension(ByVal pExtension As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SP_LISTA_EXTENSION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Extension", SqlDbType.VarChar).Value = pExtension
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTA_EXTENSION")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_ObtenerDatos_Personal
    'Prc_Obtener_DatosOfcina
    Public Function Obtener_DatosOficina(ByVal psCodGrupo As Double, ByVal psCodEmpresa As String, ByVal psCodOficina As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Obtener_DatosOfcina", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodOficina", SqlDbType.VarChar).Value = psCodOficina
        Cmd.Parameters.Add("@GE", SqlDbType.Float).Value = psCodGrupo
        Cmd.Parameters.Add("@GEE", SqlDbType.VarChar).Value = psCodEmpresa 'CodGrupo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Obtener_DatosOfcina")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Obtener_DatosPersonal(ByVal psCodGrupo As Double, ByVal psCodEmpresa As String, ByVal psCodPersonal As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_ObtenerDatos_Personal", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Cmd.Parameters.Add("@GE", SqlDbType.Float).Value = psCodGrupo
        Cmd.Parameters.Add("@GEE", SqlDbType.VarChar).Value = psCodEmpresa 'CodGrupo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_ObtenerDatos_Personal")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Personal_xCargo(ByVal psCodGrupo As Double, ByVal psCodEmpresa As String, ByVal pdCodCargo As Double) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("PRC_LISTA_PERSONAL_XCARGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = psCodGrupo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa 'CodGrupo
        Cmd.Parameters.Add("@CodCargo ", SqlDbType.Float).Value = pdCodCargo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("PRC_LISTA_PERSONAL_XCARGO")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_Lista_Cargo
    Public Function Lista_Cargo(ByVal psCodGrupo As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_Lista_Cargo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGE", SqlDbType.Float).Value = psCodGrupo
        Cmd.Parameters.Add("@CodGEE", SqlDbType.VarChar).Value = psCodEmpresa 'CodGrupo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Lista_Cargo")
        Da.Fill(Dt)
        Return Dt
    End Function


End Class