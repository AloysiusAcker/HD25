Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.Web.Security
Public Class clsInspeccion_Listado
    Public Function Listar_Ayuda_General(ByVal psConexion As String, ByVal pdOficina As Double, ByVal FechaIng As String, _
                                         ByVal FechaFin As String, ByVal NroInspecc As Double, ByVal psTipoIngreso As String, _
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
    Public Function Listar_Inpeccion(ByVal pconexion As String, ByVal NroInsp As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPLISTADO_INSPECCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = NroInsp
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTADO_INSPECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_Participantes(ByVal pconexion As String, ByVal Participante As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPLISTADO_PARTICIPANTES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = Participante
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTADO_PARTICIPANTES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listado_Participantes_Codigo(ByVal pconexion As String, ByVal Participante As Double, ByVal CodParticipante As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPLISTADO_PARTICIPANTES_CODIGO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = Participante
        Cmd.Parameters.Add("@INSPART_CODIGO", SqlDbType.Float).Value = CodParticipante
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTADO_PARTICIPANTES_CODIGO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Inpeccion_Datos_Adicionales(ByVal pconexion As String, ByVal psCodEmpresa As String, _
                                                    ByVal INSPEC_TIPOPER As String, _
                                            ByVal INSPEC_TIPO As String, ByVal INSPEC_ESTADO As String, _
                                            ByVal FechaIni As String, ByVal FechaFin As String, _
                                            ByVal CodOficina As Double, ByVal Tecnico As String, _
                                            ByVal InspeccCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPLISTADO_INSPECCION_DATOS_ADICIONALES", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_TIPOPER", SqlDbType.VarChar).Value = INSPEC_TIPOPER
        Cmd.Parameters.Add("@INSPEC_TIPO", SqlDbType.VarChar).Value = INSPEC_TIPO
        Cmd.Parameters.Add("@INSPEC_ESTADO", SqlDbType.VarChar).Value = INSPEC_ESTADO
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = FechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = FechaFin
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = CodOficina
        Cmd.Parameters.Add("@INSPEC_TECNICO", SqlDbType.VarChar).Value = Tecnico
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = InspeccCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTADO_INSPECCION_DATOS_ADICIONALES")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Inpeccion_Desplazamiento(ByVal pconexion As String, ByVal psCodEmpresa As String, _
                                                ByVal INSPEC_TIPOPER As String, _
                                        ByVal INSPEC_TIPO As String, ByVal INSPEC_ESTADO As String, _
                                        ByVal FechaIni As String, ByVal FechaFin As String, _
                                        ByVal CodOficina As Double, ByVal Tecnico As String, _
                                        ByVal InspeccCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPLISTADO_INSPECCION_DATOS_ADICIONALES_DESPLAZAMIENTO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_TIPOPER", SqlDbType.VarChar).Value = INSPEC_TIPOPER
        Cmd.Parameters.Add("@INSPEC_TIPO", SqlDbType.VarChar).Value = INSPEC_TIPO
        Cmd.Parameters.Add("@INSPEC_ESTADO", SqlDbType.VarChar).Value = INSPEC_ESTADO
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = FechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = FechaFin
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = CodOficina
        Cmd.Parameters.Add("@INSPEC_TECNICO", SqlDbType.VarChar).Value = Tecnico
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = InspeccCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPLISTADO_INSPECCION_DATOS_ADICIONALES_DESPLAZAMIENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Inspeccion_Campos_Detalle2(ByVal pconexion As String, ByVal codempresa As String, _
    ByVal TipoCampo As String, ByVal TipoClasif As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SP_LISTA_TBINV_INSPECCION_CAMPOS_INSPEC2", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@INSPECEQ_TIPOINSP", SqlDbType.VarChar).Value = TipoCampo
        Cmd.Parameters.Add("@INSPECEQ_TIPO", SqlDbType.Float).Value = TipoClasif
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTA_TBINV_INSPECCION_CAMPOS_INSPEC2")
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
    Public Function Listado_Servicio_Viatico(ByVal Conexion As String, ByVal EMPRESA_CODIGO As String, _
                                             ByVal SERVIATICO_CODIGO As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBSERVICIO_VIATICO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EMPRESA_CODIGO
        Cmd.Parameters.Add("@SERVIATICO_CODIGO", SqlDbType.Float).Value = SERVIATICO_CODIGO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBSERVICIO_VIATICO")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Listado_Servicio_Viatico_Detalle(ByVal Conexion As String, ByVal EMPRESA_CODIGO As String, _
                                         ByVal SERVIATICO_CODIGO As Double) As DataTable
        Dim Cn As New SqlConnection(Conexion)
        Dim Cmd As New SqlCommand("SP_LISTADO_TBSERVICIO_VIATICO_DETALLE", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = EMPRESA_CODIGO
        Cmd.Parameters.Add("@SERVIATICO_CODIGO", SqlDbType.Float).Value = SERVIATICO_CODIGO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_LISTADO_TBSERVICIO_VIATICO_DETALLE")
        Da.Fill(Dt)
        Return Dt
    End Function
    '''''
    Public Function Lista_Inspeccion(ByVal psConexion As String, ByVal psCodEmpresa As String, _
                                     ByVal psFechaIni As String, ByVal psFechaFin As String, _
                                     ByVal pdCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = psFechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = psFechaFin
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pdCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_TipoPersona(ByVal psConexion As String, ByVal psCodEmpresa As String, _
                                     ByVal psCodigo As String, ByVal psRazonSocial As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTATIPOPERSONA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@empresa_codigo", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@persona_ruc", SqlDbType.VarChar).Value = psCodigo
        Cmd.Parameters.Add("@persona_razon_Social", SqlDbType.VarChar).Value = psRazonSocial
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTATIPOPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_TipoPersonaTecnico(ByVal psConexion As String, _
                                             ByVal psGrupoEmp As Double, ByVal psCodEmpresa As String, _
                                             ByVal personaCodigo As String, _
                                             ByVal apePater As String) As DataTable
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTATIPOPERSONA_TECNICO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psGrupoEmp
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@persona_codigo", SqlDbType.VarChar).Value = personaCodigo
        Cmd.Parameters.Add("@apepater", SqlDbType.VarChar).Value = apePater
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTATIPOPERSONA_TECNICO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_InpeccionXTipoInspeccion(ByVal pconexion As String, ByVal psCodEmpresa As String, _
                                                     ByVal INSPEC_TIPO As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_TIPOINSPECCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_TIPO", SqlDbType.VarChar).Value = INSPEC_TIPO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_TIPOINSPECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_InpeccionXEstadoInspeccion(ByVal pconexion As String, ByVal psCodEmpresa As String, _
                                                     ByVal INSPEC_ESTADO As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_ESTADOINSPECCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_ESTADO", SqlDbType.Float).Value = INSPEC_ESTADO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_ESTADOINSPECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_InpeccionXTipoPersona(ByVal pconexion As String, ByVal psCodEmpresa As String, _
                                                     ByVal INSPEC_TIPOPER As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_TIPOPERSONA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_TIPOPER", SqlDbType.VarChar).Value = INSPEC_TIPOPER
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_TIPOPERSONA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Filtros_Inpeccion(ByVal pconexion As String, ByVal psCodEmpresa As String, _
                                                    ByVal INSPEC_TIPOPER As String, _
                                            ByVal INSPEC_TIPO As String, ByVal INSPEC_ESTADO As String, _
                                            ByVal FechaIni As String, ByVal FechaFin As String, _
                                            ByVal CodOficina As Double, ByVal Tecnico As String, _
                                            ByVal InspeccCodigo As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_FILTROS_INSPECCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_TIPOPER", SqlDbType.VarChar).Value = INSPEC_TIPOPER
        Cmd.Parameters.Add("@INSPEC_TIPO", SqlDbType.VarChar).Value = INSPEC_TIPO
        Cmd.Parameters.Add("@INSPEC_ESTADO", SqlDbType.VarChar).Value = INSPEC_ESTADO
        Cmd.Parameters.Add("@FechaIni", SqlDbType.VarChar).Value = FechaIni
        Cmd.Parameters.Add("@FechaFin", SqlDbType.VarChar).Value = FechaFin
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = CodOficina
        Cmd.Parameters.Add("@INSPEC_TECNICO", SqlDbType.VarChar).Value = Tecnico
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = InspeccCodigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_FILTROS_INSPECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    '''''
    'Public Function Listar_Inspeccion_Campos_Detalle(ByVal pconexion As String, ByVal codempresa As String, _
    '                                                 ByVal InspecCodigo As Double, ByVal TipoCampo As String) As DataTable
    '    Dim Cn As New SqlConnection(pconexion)
    '    Dim Cmd As New SqlCommand("SP_LISTA_TBINV_INSPECCION_CAMPOS_INSPEC", Cn)
    '    Cmd.CommandType = CommandType.StoredProcedure
    '    Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
    '    Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = InspecCodigo
    '    Cmd.Parameters.Add("@INSPECDET_TIPOCAMPO", SqlDbType.VarChar).Value = TipoCampo
    '    Dim Da As New SqlDataAdapter(Cmd)
    '    Dim Dt As New DataTable("SP_LISTA_TBINV_INSPECCION_CAMPOS_INSPEC")
    '    Da.Fill(Dt)
    '    Return Dt
    'End Function
    Public Function Listar_Detalle_Observacion(ByVal psConexion As String, ByVal psCodEmpresa As String, _
                                               ByVal psCodInspeccion As Double, ByVal psSerie_Numera As Double, _
                                               ByVal psTipoCampo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_DETALLE_OBSERVACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.VarChar).Value = psCodInspeccion
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = psSerie_Numera
        Cmd.Parameters.Add("@INSPDET_TIPOCAMPO", SqlDbType.VarChar).Value = psTipoCampo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_DETALLE_OBSERVACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Inspeccion_Campos(ByVal pconexion As String, ByVal codempresa As String, _
                                             ByVal TipoCampo As String, ByVal TipoClasif As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_CAMPOS", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@INSPECEQ_TIPOINSP", SqlDbType.VarChar).Value = TipoCampo
        Cmd.Parameters.Add("@INSPECEQ_TIPO", SqlDbType.Float).Value = TipoClasif
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_CAMPOS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Inspeccion_Campos_Oficina(ByVal pconexion As String, ByVal codempresa As String, _
                                             ByVal TipoCampo As String) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_CAMPOS_OFICINA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@INSPECEQ_TIPOINSP", SqlDbType.VarChar).Value = TipoCampo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_CAMPOS_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Inspeccion_Detalle_Equipo(ByVal pconexion As String, ByVal codempresa As String, _
                                              ByVal InspecCodigo As Double, ByVal TipoCampo As String, _
                                              ByVal CodDetalleCampo As Double, ByVal psSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_DETALLE_EQUIPO", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = InspecCodigo
        Cmd.Parameters.Add("@INSPECDET_TIPOCAMPO", SqlDbType.VarChar).Value = TipoCampo
        Cmd.Parameters.Add("@INSPECDET_CAMPO", SqlDbType.Float).Value = CodDetalleCampo
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_DETALLE_EQUIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Listar_Inspeccion_Detalle_Oficina(ByVal pconexion As String, ByVal codempresa As String, _
                                              ByVal InspecCodigo As Double, ByVal TipoCampo As String, _
                                              ByVal CodDetalleCampo As Double) As DataTable
        Dim Cn As New SqlConnection(pconexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_DETALLE_OFICINA", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = codempresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = InspecCodigo
        Cmd.Parameters.Add("@INSPECDET_TIPOCAMPO", SqlDbType.VarChar).Value = TipoCampo
        Cmd.Parameters.Add("@INSPECDET_CAMPO", SqlDbType.Float).Value = CodDetalleCampo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_DETALLE_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ultima_Verificacion(ByVal psConexion As String, ByVal psCodEmpresa As String, _
                                        ByVal pdCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_ULTIMA_OFVERIFICACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodSeccion", SqlDbType.Float).Value = pdCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_ULTIMA_OFVERIFICACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Confirmar_Ultima_Verificacion(ByVal psConexion As String, ByVal psCodEmpresa As String, _
                                        ByVal pdCodOficina As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_BUSCA_OFVERIFICACION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodSeccion", SqlDbType.Float).Value = pdCodOficina
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_BUSCA_OFVERIFICACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Responsable_Solucion(ByVal psConexion As String, ByVal psCodEmpresa As String, _
                                     ByVal psCodigo As String, ByVal psRazonSocial As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_LISTA_RESPONSABLE_SOLUCION", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@empresa_codigo", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@persona_ruc", SqlDbType.VarChar).Value = psCodigo
        Cmd.Parameters.Add("@persona_razon_Social", SqlDbType.VarChar).Value = psRazonSocial
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_LISTA_RESPONSABLE_SOLUCION")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class
