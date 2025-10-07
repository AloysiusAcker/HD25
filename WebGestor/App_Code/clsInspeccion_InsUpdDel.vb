Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor

Public Class clsInspeccion_InsUpdDel
    Public Function Upd_Inspeccion_Edicion2(ByVal CONEXION As String, ByVal CodEmpresa As String,
                   ByVal INSPEC_CODIGO As Double, ByVal INSPEC_TIPOPER As String,
                   ByVal INSPEC_OFICINA As Double, ByVal INSPEC_TIPO As String,
                   ByVal TECNICO As String, ByVal FechaProg As String, ByVal HoraProg As String) As DataTable
        Dim Cn As New SqlConnection(CONEXION)
        Dim Cmd As New SqlCommand("SP_INS_UPD_TBINV_INSPECCION_EDICION2", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = INSPEC_CODIGO
        Cmd.Parameters.Add("@INSPEC_TIPOPER", SqlDbType.VarChar).Value = INSPEC_TIPOPER
        Cmd.Parameters.Add("@INSPEC_OFICINA", SqlDbType.Float).Value = INSPEC_OFICINA
        Cmd.Parameters.Add("@INSPEC_TIPO", SqlDbType.VarChar).Value = INSPEC_TIPO
        Cmd.Parameters.Add("@INSPEC_TECNICO", SqlDbType.VarChar).Value = TECNICO
        Cmd.Parameters.Add("@INSPEC_PROG_FECHA", SqlDbType.VarChar).Value = FechaProg
        Cmd.Parameters.Add("@INSPEC_PROG_HORA", SqlDbType.VarChar).Value = HoraProg
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_UPD_TBINV_INSPECCION_EDICION2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Inspeccion_Participante(ByVal CONEXION As String, ByVal CodEmpresa As String,
                   ByVal INSPEC_CODIGO As Double, ByVal INSPART_CODIGO As Double,
                    ByVal Ruc As String, ByVal RazonSocial As String, ByVal Encargado As String) As DataTable
        Dim Cn As New SqlConnection(CONEXION)
        Dim Cmd As New SqlCommand("SP_INSPECC_INS_TBINV_INSPECCION_PARTICIPANTE", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        'Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Int).Value = CodGrupoEmpresa
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Int).Value = INSPEC_CODIGO
        Cmd.Parameters.Add("@INSPART_CODIGO", SqlDbType.Int).Value = INSPART_CODIGO
        Cmd.Parameters.Add("@INSPART_CODINTERNO", SqlDbType.VarChar).Value = Ruc
        Cmd.Parameters.Add("@INSPART_NOMBRE", SqlDbType.VarChar).Value = RazonSocial
        Cmd.Parameters.Add("@INSPART_ENCARGADO", SqlDbType.VarChar).Value = Encargado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INSPECC_INS_TBINV_INSPECCION_PARTICIPANTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Inspeccion_Participante(ByVal CONEXION As String, ByVal CodEmpresa As String,
                   ByVal INSPEC_CODIGO As Double) As DataTable
        Dim Cn As New SqlConnection(CONEXION)
        Dim Cmd As New SqlCommand("SP_INSPECC_DEL_TBINV_INSPECCION_PARTICIPANTE", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Int).Value = INSPEC_CODIGO
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INSPECC_DEL_TBINV_INSPECCION_PARTICIPANTE")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Inspeccion(ByVal CONEXION As String, ByVal CodEmpresa As String,
                                      ByVal INSPEC_CODIGO As Double, ByVal INSPEC_FECHA_REALIZADA As String,
                                      ByVal INSPEC_INI_HORA As String, ByVal INSPEC_FIN_HORA As String,
                                      ByVal INSPEC_HORA_LLEGADA As String, ByVal INSPEC_HORA_EXTRA As String,
                                      ByVal INSPEC_MOVILIDAD As Double, ByVal INSPEC_DOCREFERENCIA As String,
                                      ByVal INSEPC_OBJETIVO As String, ByVal INSPEC_OBJETIVO_ESTADO As String,
                                      ByVal INSPEC_FIN_FECHA As String, ByVal OBSERVACION As String,
                                      ByVal TRABAJOREALIZADO As String, ByVal MOVILIDAD_VUELTA As Double,
                                      ByVal MOVILIDAD_DESCRIPCION As String, ByVal INSPEC_ESTADO_FINAL As String,
                                      ByVal INSPEC_ESTADO_FINAL_OBS As String, ByVal INSPEC_ESTADO_FINAL_TIPIFICACION As String,
                                      ByVal INSPEC_FECHA_SOLUCION_POSIBLE As String, ByVal INSPEC_RESPONSABLE As Double) As DataTable
        Dim Cn As New SqlConnection(CONEXION)
        Dim Cmd As New SqlCommand("SP_INS_UPD_TBINV_INSPECCION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Int).Value = INSPEC_CODIGO
        Cmd.Parameters.Add("@INSPEC_FECHA_REALIZADA", SqlDbType.VarChar).Value = INSPEC_FECHA_REALIZADA
        Cmd.Parameters.Add("@INSPEC_INI_HORA", SqlDbType.VarChar).Value = INSPEC_INI_HORA
        Cmd.Parameters.Add("@INSPEC_FIN_HORA", SqlDbType.VarChar).Value = INSPEC_FIN_HORA
        Cmd.Parameters.Add("@INSPEC_HORA_LLEGADA ", SqlDbType.VarChar).Value = INSPEC_HORA_LLEGADA
        Cmd.Parameters.Add("@INSPEC_HORA_EXTRA", SqlDbType.VarChar).Value = INSPEC_HORA_EXTRA
        Cmd.Parameters.Add("@INSPEC_MOVILIDAD", SqlDbType.Float).Value = INSPEC_MOVILIDAD
        Cmd.Parameters.Add("@INSPEC_DOCREFERENCIA", SqlDbType.VarChar).Value = INSPEC_DOCREFERENCIA
        Cmd.Parameters.Add("@INSEPC_OBJETIVO", SqlDbType.VarChar).Value = INSEPC_OBJETIVO
        Cmd.Parameters.Add("@INSPEC_OBJETIVO_ESTADO", SqlDbType.VarChar).Value = INSPEC_OBJETIVO_ESTADO
        Cmd.Parameters.Add("@INSPEC_FIN_FECHA", SqlDbType.VarChar).Value = INSPEC_FIN_FECHA
        Cmd.Parameters.Add("@INSPEC_OBS", SqlDbType.VarChar).Value = OBSERVACION
        Cmd.Parameters.Add("@INSPEC_TRABREALIZADO", SqlDbType.VarChar).Value = TRABAJOREALIZADO
        Cmd.Parameters.Add("@INSPEC_MOVILIDAD_VUELTA", SqlDbType.Float).Value = MOVILIDAD_VUELTA
        Cmd.Parameters.Add("@INSPEC_MOVILIDAD_DESCRIPCION", SqlDbType.VarChar).Value = MOVILIDAD_DESCRIPCION
        Cmd.Parameters.Add("@INSPEC_ESTADO_FINAL", SqlDbType.VarChar).Value = INSPEC_ESTADO_FINAL
        Cmd.Parameters.Add("@INSPEC_ESTADO_FINAL_OBS", SqlDbType.VarChar).Value = INSPEC_ESTADO_FINAL_OBS
        Cmd.Parameters.Add("@INSPEC_ESTADO_FINAL_TIPIFICACION", SqlDbType.VarChar).Value = INSPEC_ESTADO_FINAL_TIPIFICACION
        Cmd.Parameters.Add("@INSPEC_FECHA_SOLUCION_POSIBLE", SqlDbType.VarChar).Value = INSPEC_FECHA_SOLUCION_POSIBLE
        Cmd.Parameters.Add("@INSPEC_RESPONSABLE", SqlDbType.Float).Value = INSPEC_RESPONSABLE
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_INS_UPD_TBINV_INSPECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Inspeccion(ByVal CONEXION As String, ByVal CodEmpresa As String,
                     ByVal codigo_inspe As Double, ByVal Numero As String,
                     ByVal Tipo As String, ByVal fechaProg As String,
                     ByVal HoraProg As String, ByVal tecnico As String,
                     ByVal oficina As Double, ByVal inspEstado As String,
                     ByVal sysEstado As String, ByVal tipoOper As String,
                     ByVal obs As String, ByVal tiempoProg As String,
                     ByVal user As String, ByVal objetivo As String,
                     ByVal descripcion As String, ByVal prioridad As String,
                     ByVal motivo As String, ByVal psSerieNumerar As Double,
                     ByVal psTipoMantenimiento As String) As DataTable
        Dim Cn As New SqlConnection(CONEXION)
        Dim Cmd As New SqlCommand("SPTBINV_INSPECCION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@codigo_inspe", SqlDbType.Int).Value = codigo_inspe
        Cmd.Parameters.Add("@INSPEC_NRO", SqlDbType.VarChar).Value = Numero
        Cmd.Parameters.Add("@INSPEC_TIPO", SqlDbType.VarChar).Value = Tipo
        Cmd.Parameters.Add("@INSPEC_PROG_FECHA", SqlDbType.VarChar).Value = fechaProg
        Cmd.Parameters.Add("@INSPEC_PROG_HORA", SqlDbType.VarChar).Value = HoraProg
        Cmd.Parameters.Add("@INSPEC_TECNICO", SqlDbType.VarChar).Value = tecnico
        Cmd.Parameters.Add("@INSPEC_OFICINA", SqlDbType.Float).Value = oficina
        Cmd.Parameters.Add("@INSPEC_ESTADO", SqlDbType.VarChar).Value = inspEstado
        Cmd.Parameters.Add("@INSPEC_SYS_EST", SqlDbType.VarChar).Value = sysEstado
        Cmd.Parameters.Add("@INSPEC_TIPOPER", SqlDbType.VarChar).Value = tipoOper
        Cmd.Parameters.Add("@INSPEC_OBS", SqlDbType.VarChar).Value = obs
        Cmd.Parameters.Add("@INSPEC_PROG_TIEMPO", SqlDbType.VarChar).Value = tiempoProg
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = user
        Cmd.Parameters.Add("@INSEPC_OBJETIVO", SqlDbType.VarChar).Value = objetivo
        Cmd.Parameters.Add("@INSPEC_DESCRIPCION", SqlDbType.VarChar).Value = descripcion
        Cmd.Parameters.Add("@INSPEC_PRIORIDAD", SqlDbType.VarChar).Value = prioridad
        Cmd.Parameters.Add("@INSPEC_MOTIVO", SqlDbType.VarChar).Value = motivo
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.VarChar).Value = psSerieNumerar
        Cmd.Parameters.Add("@MANTENIMIENTO", SqlDbType.VarChar).Value = psTipoMantenimiento
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPTBINV_INSPECCION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Inspeccion_Movilidad_Desplazamiento(ByVal CONEXION As String, ByVal CodEmpresa As String,
               ByVal INSPEC_CODIGO As Double, ByVal INSPEC_MOVILIDAD_IDA_2 As Double,
               ByVal INSPEC_MOVILIDAD_VUELTA_2 As Double, ByVal INSPEC_MOVILIDAD_DESCRIPCION_2 As String, ByVal Procede As String) As DataTable
        Dim Cn As New SqlConnection(CONEXION)
        Dim Cmd As New SqlCommand("SP_UPDATE_MOVILIDAD_DESPLAZAMIENTO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = CodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = INSPEC_CODIGO
        Cmd.Parameters.Add("@INSPEC_MOVILIDAD_IDA_2", SqlDbType.Float).Value = INSPEC_MOVILIDAD_IDA_2
        Cmd.Parameters.Add("@INSPEC_MOVILIDAD_VUELTA_2", SqlDbType.Float).Value = INSPEC_MOVILIDAD_VUELTA_2
        Cmd.Parameters.Add("@INSPEC_MOVILIDAD_DESCRIPCION_2", SqlDbType.VarChar).Value = INSPEC_MOVILIDAD_DESCRIPCION_2
        Cmd.Parameters.Add("@INSPEC_MOV_PROCEDE", SqlDbType.VarChar).Value = Procede
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SP_UPDATE_MOVILIDAD_DESPLAZAMIENTO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Detalle_Equipo(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                       ByVal psCodInspeccion As Double, ByVal psCampo As Double,
                                       ByVal psValor As String, ByVal psObs As String,
                                       ByVal psTipoCampo As String, ByVal psSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_INS_DETALLE_EQUIPO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = psCodInspeccion
        Cmd.Parameters.Add("@INSPECDET_CAMPO", SqlDbType.Float).Value = psCampo
        Cmd.Parameters.Add("@INSPECDET_VALOR", SqlDbType.VarChar).Value = psValor
        Cmd.Parameters.Add("@INSPECDET_OBS", SqlDbType.VarChar).Value = psObs
        Cmd.Parameters.Add("@INSPECDET_TIPOCAMPO", SqlDbType.VarChar).Value = psTipoCampo
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_INS_DETALLE_EQUIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Detalle_Oficina(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                       ByVal psCodInspeccion As Double, ByVal psCampo As Double,
                                       ByVal psValor As String, ByVal psObs As String,
                                       ByVal psTipoCampo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_INS_DETALLE_OFICINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = psCodInspeccion
        Cmd.Parameters.Add("@INSPECDET_CAMPO", SqlDbType.Float).Value = psCampo
        Cmd.Parameters.Add("@INSPECDET_VALOR", SqlDbType.VarChar).Value = psValor
        Cmd.Parameters.Add("@INSPECDET_OBS", SqlDbType.VarChar).Value = psObs
        Cmd.Parameters.Add("@INSPECDET_TIPOCAMPO", SqlDbType.VarChar).Value = psTipoCampo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_INS_DETALLE_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Detalle_Equipo(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                       ByVal psCodInspeccion As Double, ByVal psSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_DEL_DETALLE_EQUIPO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = psCodInspeccion
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_DEL_DETALLE_EQUIPO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Detalle_Oficina(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                       ByVal psCodInspeccion As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_DEL_DETALLE_OFICINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = psCodInspeccion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_DEL_DETALLE_OFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Detalle_Observacion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                            ByVal psCodInspeccion As Double, ByVal psSerieNumerar As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_DEL_DETALLE_OBSERVACION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = psCodInspeccion
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = psSerieNumerar
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_DEL_DETALLE_OBSERVACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Detalle_Observacion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                            ByVal psCodInspeccion As Double, ByVal psSerieNumerar As Double,
                                            ByVal psOservacion As String, ByVal psTipoCampo As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_INS_DETALLE_OBSERVACION", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@INSPEC_CODIGO", SqlDbType.Float).Value = psCodInspeccion
        Cmd.Parameters.Add("@SERIE_NUMERAR", SqlDbType.Float).Value = psSerieNumerar
        Cmd.Parameters.Add("@INSPDET_OBS", SqlDbType.VarChar).Value = psOservacion
        Cmd.Parameters.Add("@INSPDET_TIPOCAMPO", SqlDbType.VarChar).Value = psTipoCampo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_INS_DETALLE_OBSERVACION")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Oficina_Tablero(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                        ByVal psTta As String, ByVal psTsi As String,
                                        ByVal pdCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_UPD_OFTABLERO", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pdCodOficina
        Cmd.Parameters.Add("@Tta", SqlDbType.VarChar).Value = psTta
        Cmd.Parameters.Add("@Tsi", SqlDbType.VarChar).Value = psTsi
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_UPD_OFTABLERO")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_Ip(ByVal psConexion As String, ByVal psCodEmpresa As String,
                           ByVal pdSerieNumerar As Double, ByVal psNroIp As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_INS_IPUPS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.Float).Value = pdSerieNumerar
        Cmd.Parameters.Add("@NroIp", SqlDbType.VarChar).Value = psNroIp
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_INS_IPUPS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Del_Ip(ByVal psConexion As String, ByVal psCodEmpresa As String,
                           ByVal pdSerieNumerar As Double, ByVal psNroIp As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINV_DEL_IPUPS", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@SerieNumerar", SqlDbType.Float).Value = pdSerieNumerar
        Cmd.Parameters.Add("@NroIp", SqlDbType.VarChar).Value = psNroIp
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINV_DEL_IPUPS")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ins_OfVerifica(ByVal psConexion As String, ByVal psCodEmpresa As String,
                           ByVal pdCodOficina As Double, ByVal psFecha As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_INS_OFVERIFICA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pdCodOficina
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = psFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_INS_OFVERIFICA")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Upd_Of_EstadoFinal(ByVal psConexion As String, ByVal psCodEmpresa As String,
                           ByVal pdCodOficina As Double, ByVal psEstado As String,
                           ByVal psObs As String, ByVal psTipificacion As String,
                           ByVal psFecSolucion As String, ByVal pdResponsable As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("SPINSPECCION_UPD_OFESTADOFINAL", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pdCodOficina
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@Obs", SqlDbType.VarChar).Value = psObs
        Cmd.Parameters.Add("@Tipificacion", SqlDbType.VarChar).Value = psTipificacion
        Cmd.Parameters.Add("@FecSolucion", SqlDbType.VarChar).Value = psFecSolucion
        Cmd.Parameters.Add("@Responsable", SqlDbType.Float).Value = pdResponsable
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("SPINSPECCION_UPD_OFESTADOFINAL")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class