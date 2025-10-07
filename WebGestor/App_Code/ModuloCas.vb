Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class ModuloCas
    Public Function InsUpd_FechaContador(ByVal pFecha As String, ByVal pFechaFin As String,
                                         ByVal pTipoModificacion As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("INSUPD_ACTFECHACONTADOR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@TipoModificacion", SqlDbType.VarChar).Value = pTipoModificacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("INSUPD_ACTFECHACONTADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function BaseDatos_Contador(ByVal pCodEmpresa As String, ByVal pCodBaseDatos As Double,
                                       ByVal pTipoModificacion As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("INSUPD_ACTCONTADOR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodBaseDatos", SqlDbType.Int).Value = pCodBaseDatos
        Cmd.Parameters.Add("@TipoModificacion", SqlDbType.VarChar).Value = pTipoModificacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("INSUPD_ACTCONTADOR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodBaseDatos As Double,
                                ByVal pCodAplicativo As Double, ByVal pCodProducto As Double,
                                ByVal pCodSubProd As Double, ByVal pTransaccion As String,
                                ByVal pConsulta As String, ByVal pSolucion As String,
                                ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("INSUPD_BASEDATOS", Cn)
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
        Dim Dt As New DataTable("INSUPD_BASEDATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Incidente(ByVal pCodEmpresa As String, ByVal pTipoProb As Double,
                                ByVal pUser As String, ByVal pPeCodInterno As String,
                                ByVal pRep_Codigo As Double, ByVal pRep_Prioridad As String,
                                ByVal pTipo2 As Double, ByVal pTipo3 As Double,
                                ByVal pRep_Descrip As String, ByVal pTipo As String,
                                ByVal pTipoIngreso As String, ByVal pCodOficina As Double,
                                ByVal pTelefActual As String, ByVal pEstado As String,
                                ByVal pAsignado As String, ByVal pIniciaLlamada As String,
                                ByVal pSeguimiento As String, ByVal pMotivo As String,
                                ByVal pUserRedirec As String, ByVal pAsigTipo As String,
                                ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_INCIDENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoProb", SqlDbType.Float).Value = pTipoProb
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@PeCodInterno", SqlDbType.VarChar).Value = pPeCodInterno
        Cmd.Parameters.Add("@Rep_Codigo", SqlDbType.Float).Value = pRep_Codigo
        Cmd.Parameters.Add("@Rep_Prioridad", SqlDbType.VarChar).Value = pRep_Prioridad
        Cmd.Parameters.Add("@Tipo2", SqlDbType.Float).Value = pTipo2
        Cmd.Parameters.Add("@Tipo3", SqlDbType.Float).Value = pTipo3
        Cmd.Parameters.Add("@Rep_Descrip", SqlDbType.VarChar).Value = pRep_Descrip
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pCodOficina
        Cmd.Parameters.Add("@TelefActual", SqlDbType.VarChar).Value = pTelefActual
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@Asignado", SqlDbType.VarChar).Value = pAsignado
        Cmd.Parameters.Add("@IniciaLlamada", SqlDbType.VarChar).Value = pIniciaLlamada
        Cmd.Parameters.Add("@Seguimiento", SqlDbType.VarChar).Value = pSeguimiento
        Cmd.Parameters.Add("@Motivo", SqlDbType.VarChar).Value = pMotivo
        Cmd.Parameters.Add("@UserRedirec", SqlDbType.VarChar).Value = pUserRedirec
        Cmd.Parameters.Add("@AsigTipo", SqlDbType.VarChar).Value = pAsigTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_INCIDENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_IncidenteDetalle(ByVal pCodEmpresa As String, ByVal pCodIncidente As Double,
                                ByVal pSolucion As String, ByVal pUser As String,
                                ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_INCIDENTEDETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodIncidente", SqlDbType.Float).Value = pCodIncidente
        Cmd.Parameters.Add("@Solucion", SqlDbType.VarChar).Value = pSolucion
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.Float).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_INCIDENTEDETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Personas(ByVal persona_codigo As Double, ByVal persona_Usuario As String,
                                ByVal persona_nombre As String, ByVal Persona_Apellidos As String,
                                ByVal oficina As Double, ByVal Puesto As Double,
                                ByVal telefono As String, ByVal anexo As String,
                                ByVal correo As String, ByVal CodBanca As Double,
                                ByVal Filler As String, ByVal Antiguedad As Decimal,
                                ByVal Territorio As Double, ByVal TipoIngreso As String,
                                ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_CASPERSONA", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@persona_codigo", SqlDbType.Float).Value = persona_codigo
        Cmd.Parameters.Add("@persona_Usuario", SqlDbType.VarChar).Value = persona_Usuario
        Cmd.Parameters.Add("@persona_nombre", SqlDbType.VarChar).Value = persona_nombre
        Cmd.Parameters.Add("@Persona_Apellidos", SqlDbType.VarChar).Value = Persona_Apellidos
        Cmd.Parameters.Add("@oficina", SqlDbType.Float).Value = oficina
        Cmd.Parameters.Add("@Puesto", SqlDbType.Float).Value = Puesto
        Cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = telefono
        Cmd.Parameters.Add("@anexo", SqlDbType.VarChar).Value = anexo
        Cmd.Parameters.Add("@correo", SqlDbType.VarChar).Value = correo
        Cmd.Parameters.Add("@CodBanca", SqlDbType.Float).Value = CodBanca
        Cmd.Parameters.Add("@Filler", SqlDbType.VarChar).Value = Filler
        Cmd.Parameters.Add("@Antiguedad", SqlDbType.Decimal).Value = Antiguedad
        Cmd.Parameters.Add("@Territorio", SqlDbType.Float).Value = Territorio
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = TipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_CASPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Aviso(ByVal pCodAviso As Double, ByVal pTipoAviso As String,
                                 ByVal pDescrip As String, ByVal pEstadoAviso As String,
                                 ByVal pUser As String, ByVal pTipoIngreso As String,
                                 ByVal Conexion As String, ByVal psDetalle As String,
                                 ByVal pdCodAplicativo As Double, ByVal pdCodProducto As Double,
                                 ByVal pdCodSubProducto As Double, ByVal pdCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_AVISO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodAviso", SqlDbType.Float).Value = pCodAviso
        Cmd.Parameters.Add("@TipoAviso", SqlDbType.VarChar).Value = pTipoAviso
        Cmd.Parameters.Add("@Descrip", SqlDbType.VarChar).Value = pDescrip
        Cmd.Parameters.Add("@EstadoAviso", SqlDbType.VarChar).Value = pEstadoAviso
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@Detalle", SqlDbType.VarChar).Value = psDetalle
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Float).Value = pdCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Float).Value = pdCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Float).Value = pdCodSubProducto
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pdCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_AVISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_AvisoArchivo(ByVal psCodEmpresa As String, ByVal conexion As String,
                                        ByVal psCodAviso As Double, ByVal psArchivo As String,
                                        ByVal psRuta As String, ByVal psFecha As String,
                                        ByVal psHora As String, ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(conexion)
        Dim Cmd As New SqlCommand("Prc_Cas_Aviso_InsArchivo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodAviso", SqlDbType.Float).Value = psCodAviso
        Cmd.Parameters.Add("@Archivo", SqlDbType.VarChar).Value = psArchivo
        Cmd.Parameters.Add("@Ruta", SqlDbType.VarChar).Value = psRuta
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = psHora
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cas_Aviso_InsArchivo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_PuclicaAviso(ByVal pCodAviso As Double, ByVal pUser As String,
                                        ByVal pTipoIngreso As String, ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_PUBLICARAVISO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodAviso", SqlDbType.Float).Value = pCodAviso
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_PUBLICARAVISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ListaArchivo_xAviso(ByVal psCodEmpresa As String, ByVal psConexion As String,
                                        ByVal psCodAviso As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Cas_ListaArchivo_xAviso", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodAviso", SqlDbType.Float).Value = psCodAviso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cas_ListaArchivo_xAviso")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function InsUpd_Enlace(ByVal pCodigo As Double, ByVal pDescripcion As String,
                                ByVal pUrl As String, ByVal pTipoIngreso As String,
                                ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_ENLACE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@Url", SqlDbType.VarChar).Value = pUrl
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_ENLACE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Empresa(ByVal pCodigo As Double, ByVal pNombre As String,
                                ByVal pUser As String, ByVal pTipoIngreso As String,
                                ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_EMPRESA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_EMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Oficina(ByVal pCodigo As Double, ByVal pCodigoint As String,
                                   ByVal pNombre As String, ByVal pEmpresa As String,
                                   ByVal pUser As String, ByVal pTipoIngreso As String,
                                   ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_OFICINA", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Codigoint", SqlDbType.VarChar).Value = pCodigoint
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@Empresa", SqlDbType.VarChar).Value = pEmpresa
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Puesto(ByVal pCodigo As Double, ByVal pNombre As String,
                                  ByVal pCodInterno As String, ByVal pTipoIngreso As String,
                                  ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_PUESTO", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_PUESTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Territorio(ByVal pCodTerritorio As Double, ByVal pCodInterno As String,
                                      ByVal pNombre As String, ByVal pTipoIngreso As String,
                                      ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_TERRITORIO", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Territorio", SqlDbType.Float).Value = pCodTerritorio
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_TERRITORIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Criterio(ByVal pCodEmpresa As String, ByVal pTipo As String,
                                   ByVal pCodigo As Double, ByVal pDescripcion As String,
                                   ByVal pInicia As String, ByVal pTipoIngreso As String,
                                   ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_CRITERIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@Inicia", SqlDbType.VarChar).Value = pInicia
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_CRITERIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_Grupo(ByVal pCodigo As Double, ByVal pNombre As String,
                                 ByVal User As String, ByVal pTipoIngreso As String,
                                 ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_GRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = pNombre
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = User
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_GRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_RelacionGrupo(ByVal pCodGrupo As Double, ByVal pUsuario As String,
                                   ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_RELACIONGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = pCodGrupo
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_RELACIONGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_UsuarioNivel(ByVal pNivel As String, ByVal pUsuario As String,
                                   ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_USUARIONIVEL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Nivel", SqlDbType.Float).Value = pNivel
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_USUARIONIVEL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_ComponentexGrupo(ByVal pCodigo As Double, ByVal CodComponente As Double,
                                   ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_COMPONENTEXGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@CodComponente", SqlDbType.Float).Value = CodComponente
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_COMPONENTEXGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function InsUpd_TemaAyuda(ByVal pCodigo As Double, ByVal pClasificacion As String,
                                   ByVal pTipo As String, ByVal pNombreArchivo As String,
                                   ByVal pDescripcion As String, ByVal pUser As String,
                                   ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_INSUPD_TEMAYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Cmd.Parameters.Add("@Clasificacion", SqlDbType.VarChar).Value = pClasificacion
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar).Value = pNombreArchivo
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_INSUPD_TEMAYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Listados
    Public Function CasLista_ReiniciarContador(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("CASLISTA_PARAREINICIAR", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("CASLISTA_PARAREINICIAR")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_BaseDatosTop10(ByVal pCodEmpresa As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("CASLISTA_TOP10", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("CASLISTA_TOP10")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodAplicativo As Double, _
                                       ByVal pCodProducto As Double, ByVal pCodSubProd As Double, _
                                       ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("CASLISTA_BASEDATOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Int).Value = pCodSubProd
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("CASLISTA_BASEDATOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Aplicativos(ByVal pCodEmpresa As String, ByVal pCodAplicativo As Double, _
                                         ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("CASLISTA_APLICATIVOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("CASLISTA_APLICATIVOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Productos(ByVal pCodEmpresa As String, ByVal pCodAplicativo As Double, _
                                       ByVal pCodProducto As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("CASLISTA_PRODUCTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Int).Value = pCodAplicativo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("CASLISTA_PRODUCTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_BaseDatos(ByVal pCodEmpresa As String, ByVal pCodProducto As Double, _
                                       ByVal pCodSubProd As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("CASLISTA_SUBPRODUCTOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Int).Value = pCodProducto
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("CASLISTA_SUBPRODUCTOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Incidentes(ByVal pCodEmpresa As String, ByVal pIncEstado As String, _
                                        ByVal pIncCodigo As Double, ByVal pIncImportancia As String, _
                                        ByVal pIncComponente As Double, ByVal pIncElemento As Double, _
                                        ByVal pFechaIni As String, ByVal pFechaFin As String, _
                                        ByVal pIncTipo As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_INCIDENTES_WEB", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@IncEstado", SqlDbType.VarChar).Value = pIncEstado
        Cmd.Parameters.Add("@IncCodigo", SqlDbType.Float).Value = pIncCodigo
        Cmd.Parameters.Add("@IncImportancia", SqlDbType.VarChar).Value = pIncImportancia
        Cmd.Parameters.Add("@IncComponente", SqlDbType.Float).Value = pIncComponente
        Cmd.Parameters.Add("@IncElemento", SqlDbType.Float).Value = pIncElemento
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@IncTipo", SqlDbType.VarChar).Value = pIncTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_INCIDENTES_WEB")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_IncidenteAExportar(ByVal pCodEmpresa As String, ByVal pIncEstado As String, _
                                                ByVal pIncCodigo As Double, ByVal pIncImportancia As String, _
                                                ByVal pIncComponente As Double, ByVal pIncElemento As Double, _
                                                ByVal pFechaIni As String, ByVal pFechaFin As String, _
                                                ByVal pIncTipo As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXPORTAR_INCIDENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@IncEstado", SqlDbType.VarChar).Value = pIncEstado
        Cmd.Parameters.Add("@IncCodigo", SqlDbType.Float).Value = pIncCodigo
        Cmd.Parameters.Add("@IncImportancia", SqlDbType.VarChar).Value = pIncImportancia
        Cmd.Parameters.Add("@IncComponente", SqlDbType.Float).Value = pIncComponente
        Cmd.Parameters.Add("@IncElemento", SqlDbType.Float).Value = pIncElemento
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@IncTipo", SqlDbType.VarChar).Value = pIncTipo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXPORTAR_INCIDENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_IncidentesUsuario(ByVal pCodEmpresa As String, ByVal pIncEstado As String, _
                                               ByVal pIncCodigo As Double, ByVal pIncImportancia As String, _
                                               ByVal pIncComponente As Double, ByVal pIncElemento As Double, _
                                               ByVal pIncTipo As String, ByVal pUser As String, _
                                               ByVal pTipoAsig As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_INCIDENTES_N2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@IncEstado", SqlDbType.VarChar).Value = pIncEstado
        Cmd.Parameters.Add("@IncCodigo", SqlDbType.Float).Value = pIncCodigo
        Cmd.Parameters.Add("@IncImportancia", SqlDbType.VarChar).Value = pIncImportancia
        Cmd.Parameters.Add("@IncComponente", SqlDbType.Float).Value = pIncComponente
        Cmd.Parameters.Add("@IncElemento", SqlDbType.Float).Value = pIncElemento
        Cmd.Parameters.Add("@IncTipo", SqlDbType.VarChar).Value = pIncTipo
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoAsig", SqlDbType.VarChar).Value = pTipoAsig
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_INCIDENTES_N2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Personas(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_PERSONAS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_PERSONAS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_IncidenteDetalle(ByVal CodEmpresa As String, ByVal CodIncidente As Double, _
                                              ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_INCIDENTEDETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@CodIncidente", SqlDbType.Float).Value = CodIncidente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_INCIDENTEDETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Enlace(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_ENLACE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_ENLACE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Enlace(ByVal pCodigo As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_ENLACExCODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_ENLACExCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_TemaAyuda(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_TEMAAYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_TEMAAYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Cas_ListaArchivo_xCodigo
    Public Function BCD_MuestraArchivo_xCodigo(ByVal pCodigo As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Cas_BaseConocimiento_ArchivoXCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cas_BaseConocimiento_ArchivoXCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_TemaAyuda(ByVal pCodigo As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_TEMAAYUDAxCODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_TEMAAYUDAxCODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Aviso(ByVal pUser As String, ByVal pEstUsuario As String,
                                   ByVal pTipoAviso As String, ByVal pEstAviso As String,
                                   ByVal pTipoListado As String, ByVal Conexion As String,
                                   ByVal pdCodAplicativo As Double, ByVal pdCodProducto As Double,
                                   ByVal pdCodSubProducto As Double, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_AVISOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@EstUsuario", SqlDbType.VarChar).Value = pEstUsuario
        Cmd.Parameters.Add("@TipoAviso", SqlDbType.VarChar).Value = pTipoAviso
        Cmd.Parameters.Add("@EstAviso", SqlDbType.VarChar).Value = pEstAviso
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoListado
        Cmd.Parameters.Add("@CodAplicativo", SqlDbType.Float).Value = pdCodAplicativo
        Cmd.Parameters.Add("@CodProducto", SqlDbType.Float).Value = pdCodProducto
        Cmd.Parameters.Add("@CodSubProd", SqlDbType.Float).Value = pdCodSubProducto
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_AVISOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_xAviso(ByVal pUser As String, ByVal pCodAviso As Double,
                                    ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_CasLista_xNroAviso", Cn)
        Cmd.CommandType = CommandType.StoredProcedure '@User
        Cmd.Parameters.Add("@NroAviso", SqlDbType.Float).Value = pCodAviso
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_CasLista_xNroAviso")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Empresa(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_EMPRESA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_EMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Oficina(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_OFICINA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Puesto(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_PUESTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_PUESTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Criterio(ByVal CodEmpresa As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_CRITERIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = CodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_CRITERIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_TIndividual(ByVal pCodComponente As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_TINDIVIDUAL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodComponente", SqlDbType.Float).Value = pCodComponente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_TINDIVIDUAL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_TGrupo(ByVal pCodComponente As Double, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_TGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodComponente", SqlDbType.Float).Value = pCodComponente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_TGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_UsuarioXNivel(ByVal pNivel As String, ByVal pTipoListado As String, _
                                           ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTAUSUARIO_XNIVEL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Nivel", SqlDbType.VarChar).Value = pNivel
        Cmd.Parameters.Add("@TipoListado", SqlDbType.VarChar).Value = pTipoListado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTAUSUARIO_XNIVEL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Grupo(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_GRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_GRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_ComponenteGrupo(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_COMPONENTEGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_COMPONENTEGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_UsuarioGrupo(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_USUARIOGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_USUARIOGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_UsuarioNivel(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_USUARIONIVEL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_USUARIONIVEL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_Bandeja(ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_BANDEJA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_BANDEJA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_xIncidente(ByVal pCodEmpresa As String, ByVal pCodIncidente As Double, _
                                        ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_XINCIDENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodIncidente", SqlDbType.Float).Value = pCodIncidente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_XINCIDENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_xIncidente_Solucion(ByVal pCodEmpresa As String, ByVal pCodIncidente As Double, _
                                                 ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_LISTA_XINCIDENTE_SOLUCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodIncidente", SqlDbType.Float).Value = pCodIncidente
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_LISTA_XINCIDENTE_SOLUCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasLista_IncxComponente(ByVal pCodEmpresa As String, ByVal pFechaIni As String, _
                                        ByVal pFechaFin As String, ByVal pEstado As String, _
                                        ByVal pImportancia As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("[TBCAS_LISTA_INCXCOMPONENTE]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@Importancia", SqlDbType.VarChar).Value = pImportancia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[TBCAS_LISTA_INCXCOMPONENTE]")
        Da.Fill(Dt)
        Return Dt
    End Function
    'CONSULTA
    Public Function CasConsulta_ExistePersona(ByVal Usuario As String, ByVal persona_nombre As String, _
                                      ByVal Persona_Apellidos As String, ByVal TipoIngreso As String, _
                                              ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEPERSONA", Cn)
        Cmd.CommandTimeout = 50
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = Usuario
        Cmd.Parameters.Add("@persona_nombre", SqlDbType.VarChar).Value = persona_nombre
        Cmd.Parameters.Add("@Persona_Apellidos", SqlDbType.VarChar).Value = Persona_Apellidos
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = TipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteEmpresa(ByVal Nombre As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEEMPRESA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEEMPRESA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteOficina(ByVal Codigoint As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEOFICINA", Cn)
        Cmd.CommandTimeout = 50
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Codigoint", SqlDbType.VarChar).Value = Codigoint
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEOFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExistePuetso(ByVal Nombre As String, ByVal pCodInterno As String, _
                                             ByVal pTipoIngreso As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEPUESTO", Cn)
        Cmd.CommandTimeout = 50
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Cmd.Parameters.Add("@TipoIngreso", SqlDbType.VarChar).Value = pTipoIngreso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEPUESTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteGrupo(ByVal CodGrupo As Double, ByVal Nombre As String, _
                                            ByVal TipoConsulta As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEGRUPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = CodGrupo
        Cmd.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = Nombre
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = TipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEGRUPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteUsuario(ByVal Usuario_Accion As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Usuario_Accion", SqlDbType.VarChar).Value = Usuario_Accion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteCriterio(ByVal pDescripcion As String, ByVal pTipo As String, _
                                               ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTECRITERIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = pDescripcion
        Cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = pTipo
        Dim Dt As New DataTable("TBCAS_EXISTECRITERIO")
        Dim Da As New SqlDataAdapter(Cmd)
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteGrupoxComponente(ByVal CodGrupo As Double, ByVal CodComponente As Double, _
                                                       ByVal TipoConsulta As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEGRUPO_XCOMPONENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = CodGrupo
        Cmd.Parameters.Add("@CodComponente", SqlDbType.Float).Value = CodComponente
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = TipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEGRUPO_XCOMPONENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteGrupoxUsuario(ByVal CodGrupo As Double, ByVal pUser As String, _
                                                    ByVal pTipoConsulta As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEGRUPO_XUSUARIO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = CodGrupo
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TipoConsulta", SqlDbType.VarChar).Value = pTipoConsulta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEGRUPO_XUSUARIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteAviso(ByVal pUser As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEAVISO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEAVISO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteN1Usuario(ByVal pUser As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("[TBCAS_EXISTEUSUARIOxN]", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("[TBCAS_EXISTEUSUARIOxN]")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteUsuarioNivel(ByVal pNivel As String, ByVal pUsuario As String, _
                                                   ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTEUSUARIONIVEL", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@Nivel", SqlDbType.VarChar).Value = pNivel
        Cmd.Parameters.Add("@Usuario", SqlDbType.VarChar).Value = pUsuario
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTEUSUARIONIVEL")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_CantInc(ByVal pCodEmpresa As String, ByVal pFechaIni As String, _
                                        ByVal pFechaFin As String, ByVal pEstado As String, _
                                        ByVal pImportancia As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_COUNT_INCIDENTE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = pFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = pFechaFin
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = pEstado
        Cmd.Parameters.Add("@Importancia", SqlDbType.VarChar).Value = pImportancia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_COUNT_INCIDENTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteTerritorio(ByVal pCodInterno As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTETERRITORIO", Cn)
        Cmd.CommandTimeout = 100
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodInterno", SqlDbType.VarChar).Value = pCodInterno
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTETERRITORIO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function CasConsulta_ExisteTemaAyuda(ByVal pNombreArchivo As String, ByVal Conexion As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("TBCAS_EXISTETEMAYUDA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@NombreArchivo", SqlDbType.VarChar).Value = pNombreArchivo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("TBCAS_EXISTETEMAYUDA")
        Da.Fill(Dt)
        Return Dt
    End Function

    '

    Public Function ListaArchivo_xBDC(ByVal psCodEmpresa As String, ByVal psConexion As String,
                                        ByVal psCodBDC As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Cas_ListaArchivo_xBaseConocimiento", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodBdc", SqlDbType.Float).Value = psCodBDC
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cas_ListaArchivo_xBaseConocimiento")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_Cas_ListaArchivo_xCodigo
    Public Function Aviso_MuestraArchivo_xCodigo(ByVal pCodigo As Double, ByVal Conexion As String, ByVal psCodEmpresa As String) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("Prc_Cas_ListaArchivo_xCodigo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodArchivo", SqlDbType.Float).Value = pCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cas_ListaArchivo_xCodigo")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function InsUpd_BaseConicimientoArchivo(ByVal psCodEmpresa As String, ByVal conexion As String,
                                                    ByVal psCodBDC As Double, ByVal psArchivo As String,
                                                    ByVal psRuta As String, ByVal psFecha As String,
                                                    ByVal psHora As String, ByVal psUser As String) As DataTable
        Dim Cn As New SqlConnection(conexion)
        Dim Cmd As New SqlCommand("Prc_Cas_BaseConocimiento_InsArchivo", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodBDC", SqlDbType.Float).Value = psCodBDC
        Cmd.Parameters.Add("@Archivo", SqlDbType.VarChar).Value = psArchivo
        Cmd.Parameters.Add("@Ruta", SqlDbType.VarChar).Value = psRuta
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = psUser
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = psHora
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Cas_BaseConocimiento_InsArchivo")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    'Prc_Cas_ListaArchivo_xBaseConocimiento]

End Class