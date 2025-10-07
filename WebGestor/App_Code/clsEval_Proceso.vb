Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor

Public Class ClsEval_Proceso
    'prc_Chequeo_Lista_Proceso
    Public Function Lista_Proceso(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_Chequeo_Lista_Proceso", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_Chequeo_Lista_Proceso")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProceso_ListaTodo
    Public Function Lista_Evaluacion_ConFiltros(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodDM As String, ByVal psCodProceso As Double, ByVal psEstado As String, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaTodo", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDM", SqlDbType.VarChar).Value = psCodDM
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaTodo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Evaluacion(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Lista", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProceso_ListaReclamo
    Public Function Lista_Reclamo(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaReclamo", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaReclamo")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function ListaDetalle_Reclamo(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal pdCodReclamo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaReclamo_Detalle", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodReclamo", SqlDbType.Float).Value = pdCodReclamo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaReclamo_Detalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Evaluacion_xDM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodDM As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Lista_xDM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodDM", SqlDbType.VarChar).Value = psCodDM
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Lista_xDM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Evaluacion_xCodEval(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodEval As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Lista_xCodEval", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodEval", SqlDbType.Int).Value = psCodEval
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Lista_xCodEval")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_EvaluacionxOficina(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal pCodOficina As Double, ByVal psCodProceso As Double, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaxOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pCodOficina
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaxOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function Lista_PromedioFinalOficina(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal pCodOficina As Double, ByVal psCodProceso As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_PromedioFinalxProceso", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = pCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_PromedioFinalxProceso")
        Da.Fill(Dt)
        Return Dt
    End Function


    'prc_EvaluacionProceso_ListaTareas_RptaJaladas
    Public Function Lista_Tareas(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodProceso As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_Chequeo_Lista_Tareas", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_Chequeo_Lista_Tareas")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Evaluacion_Proceso_PromedioxOficina
    Public Function Evaluacion_PromedioxOficina(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodOficina As Double, ByVal psCodProceso As Double, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_PromedioxOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = psCodOficina
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_PromedioxOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Ultima_Evaluacion(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodPregunta As Double, ByVal psCodEvaluacion As Double, ByVal psPlanAccion As String, ByVal psCodtienda As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Des", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPregunta", SqlDbType.Float).Value = psCodPregunta
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = psCodEvaluacion '@PlanAccion
        Cmd.Parameters.Add("@PlanAccion", SqlDbType.VarChar).Value = psPlanAccion '
        Cmd.Parameters.Add("@CodTienda", SqlDbType.Float).Value = psCodtienda
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Des")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Chequeo_Lista_ActividadesxTarea
    Public Function Lista_ActividadesxTarea_CantEval(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodTarea As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Chequeo_Lista_ActividadesxTarea_CantEval", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodTareaDet", SqlDbType.Float).Value = psCodTarea
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Chequeo_Lista_ActividadesxTarea_CantEval")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProceso_ListaActividad_xAct_xCodEval
    Public Function Lista_ActividadesxTarea_xCodEval(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodEval As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaActividad_xAct_xCodEval", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = psCodEval
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaActividad_xAct_xCodEval")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProceso_ListaRespuestasJalados_xOficina
    Public Function ResultadoTotalxAct_xPregunta(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodEval As Double, ByVal psCodAct As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_TotalResultado_xPregunta", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = psCodEval
        Cmd.Parameters.Add("@CodAct", SqlDbType.Float).Value = psCodAct
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_TotalResultado_xPregunta")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Actividades_xTipoEval(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psTipoEval_Codigo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProcesos_TipoEval_ListaDetalle", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@TipoEval_Codigo", SqlDbType.Float).Value = psTipoEval_Codigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProcesos_TipoEval_ListaDetalle")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_EvaluacionProcesos_TipoEval_ListaDetalle

    Public Function Lista_Actividades_xTipoEval(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodTarea As Double, ByVal psTipoEval_Codigo As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProcesos_TipoEval_ListaDetalle", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodTarea", SqlDbType.Float).Value = psCodTarea
        Cmd.Parameters.Add("@TipoEval_Codigo", SqlDbType.Float).Value = psTipoEval_Codigo
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProcesos_TipoEval_ListaDetalle")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProcesos_TipoEval_ListaDetalle
    Public Function Lista_Actividades_xTarea(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodTarea As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Chequeo_Lista_ActividadesxTarea", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodTarea", SqlDbType.Float).Value = psCodTarea
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Chequeo_Lista_ActividadesxTarea")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Actividades_xActividad(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodActividad As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Chequeo_Lista_ActividadesxActividad", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodActividad", SqlDbType.Float).Value = psCodActividad
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Chequeo_Lista_ActividadesxActividad")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Oficina(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Lista_EvaluacionxOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Lista_EvaluacionxOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_EvaluacionOficina(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double, ByVal psCodProceso As Double, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_xOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_xOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Registrar_Evalucion(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                        ByVal pdCodProceso As Double, ByVal pdCodOficina As Double,
                                        ByVal psResponsable As String, ByVal psFecha As String,
                                        ByVal psFechaReg As String, ByVal psHoraReg As String,
                                        ByVal psUserReg As String, ByVal psEstado As String,
                                        ByVal pssysEst As String, ByVal pssysCre As String,
                                        ByVal psTipoEval As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Registro", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@EMPRESA_CODIGO", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@EVALUACION_PROCESO", SqlDbType.Float).Value = pdCodProceso
        Cmd.Parameters.Add("@EVALUACION_OFICINA", SqlDbType.Float).Value = pdCodOficina
        Cmd.Parameters.Add("@EVALUACION_RESPONSABLE", SqlDbType.VarChar).Value = psResponsable
        Cmd.Parameters.Add("@EVALUACION_FECHA", SqlDbType.VarChar).Value = psFecha
        Cmd.Parameters.Add("@EVALUACION_FECHA_REG", SqlDbType.VarChar).Value = psFechaReg
        Cmd.Parameters.Add("@EVALUACION_HORA_REG", SqlDbType.VarChar).Value = psHoraReg
        Cmd.Parameters.Add("@EVALUACION_USER_REG", SqlDbType.VarChar).Value = psUserReg
        Cmd.Parameters.Add("@EVALUACION_ESTADO", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@EVALUACION_SYS_EST", SqlDbType.VarChar).Value = pssysEst
        Cmd.Parameters.Add("@EVALUACION_SYS_CRE", SqlDbType.VarChar).Value = pssysCre
        Cmd.Parameters.Add("@EVALUACION_TIPO", SqlDbType.Int).Value = psTipoEval
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Registro")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_Resultado(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodEval As Double, ByVal psCodPregunta As Double,
                                         ByVal psCodProceso As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Resultado", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = psCodEval
        Cmd.Parameters.Add("@CodPreguntaExcepto", SqlDbType.Float).Value = psCodPregunta
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Resultado")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Evaluacion_Proceso_Accion
    Public Function Evaluacion_PlanAccion(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodEval As Double, ByVal psCodPregunta As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_Accion", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = psCodEval
        Cmd.Parameters.Add("@CodPregunta", SqlDbType.Float).Value = psCodPregunta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_Accion")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_UpdEstado(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodEval As Double, ByVal psEstado As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_UpdEstado", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = psCodEval
        Cmd.Parameters.Add("@estado", SqlDbType.Float).Value = psEstado
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_UpdEstado")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_RptaErroneas(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodProceso As Double, ByVal psTop10 As Integer, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Sql As String = " SELECT top " & psTop10 & "  EVALPRO_PREGUNTA,'' AS COLOR, TAREADET_NOMBRE as nombre, EVALUACION_PROCESO, COUNT(EVALPRO_RESPUESTA ) AS CANT " _
                          & " FROM TBEVALUACION_PROCESOS_DETALLE AS A INNER JOIN TBCHEQUEO_TAREA_DETALLE AS B ON A.EVALPRO_PREGUNTA = TAREADET_CODIGO  " _
                          & " Inner join TBEVALUACION_PROCESO  as c on a.EVALUACION_CODIGO = c.EVALUACION_CODIGO " _
                          & " WHERE EVALPRO_RESPUESTA = '0' And c.EVALUACION_ESTADO = '3' and substring(EVALUACION_FECHA,1,4) = '" & psAño & "' And RTrim(Convert(nChar(10),EVALUACION_PROCESO )) " _
                          & " Like Case " & psCodProceso & "  When '0' Then '%' Else RTrim(Convert(nChar(10)," & psCodProceso & " ))  end " _
                          & " GROUP BY EVALPRO_PREGUNTA, TAREADET_NOMBRE, EVALUACION_PROCESO order by COUNT(EVALPRO_RESPUESTA ) desc"
        Dim Cmd As New SqlCommand(Sql, Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RptaErrorneas")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function EvalProcesos_RptaJalados_xOficina_top(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psTop10 As Integer) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim sql As String = "SELECT top " & psTop10 & " COUNT(CANTEVAL) AS CANT ,  EVALUACION_PROCESO ,PROCESO_NOMBRE, TAREA_CODIGO, TAREA_NOMBRE, EVALPRO_PREGUNTA, PREGUNTA_NOMBRE as TAREADET_NOMBRE, tarea_orden , tareadet_orden  " _
                          & " From  V_ListaTop_RptaJalados  GROUP BY  EVALUACION_PROCESO ,PROCESO_NOMBRE, TAREA_CODIGO, TAREA_NOMBRE, EVALPRO_PREGUNTA, PREGUNTA_NOMBRE,tarea_orden , tareadet_orden " _
                          & " ORDER BY COUNT(CANTEVAL) DESC  "
        Dim Cmd As New SqlCommand(sql, Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("EvalProcesos_RptaJalados_xOficina_top")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function EvalProcesos_RptaJalados_xOficina(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal pdTareaCod As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim sql As String = "SELECT  CANTEVAL AS CANT ,  TAREA_CODIGO, TAREA_NOMBRE, EVALPRO_PREGUNTA, PREGUNTA_NOMBRE as TAREADET_NOMBRE, tarea_orden , tareadet_orden  " _
                          & " From  V_ListaTop_RptaJalados_top where tarea_codigo = " & pdTareaCod & " GROUP BY  CANTEVAL,TAREA_CODIGO, TAREA_NOMBRE, EVALPRO_PREGUNTA, PREGUNTA_NOMBRE,tarea_orden , tareadet_orden " _
                          & " ORDER BY CANTEVAL DESC  "
        Dim Cmd As New SqlCommand(sql, Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("EvalProcesos_RptaJalados_xOficina")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function EvalProcesos_RptaJalados_ListaAct(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal pdTareaCod As Double, ByVal pdNumReg As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim sql As String = "SELECT num_reg, CANTEVAL AS CANT ,  TAREA_CODIGO, TAREA_NOMBRE, EVALPRO_PREGUNTA, PREGUNTA_NOMBRE as TAREADET_NOMBRE, tarea_orden , tareadet_orden  " _
                          & " From  V_ListaTop_RptaJalados_top where tarea_codigo = " & pdTareaCod & " and num_reg = " & pdNumReg & " GROUP BY num_reg, CANTEVAL,TAREA_CODIGO, TAREA_NOMBRE, EVALPRO_PREGUNTA, PREGUNTA_NOMBRE,tarea_orden , tareadet_orden " _
                          & " ORDER BY CANTEVAL DESC  "
        Dim Cmd As New SqlCommand(sql, Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("EvalProcesos_RptaJalados_ListaAct")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function EvalProcesos_RptaJalados_ListaTareas(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim sql As String = "SELECT  num_reg, CANTEVAL AS CANT  ,  TAREA_CODIGO, TAREA_NOMBRE,tarea_orden   " _
                          & " From  V_ListaTop_RptaJalados_top GROUP BY num_reg, CANTEVAL,  TAREA_CODIGO, TAREA_NOMBRE,tarea_orden " _
                          & " ORDER BY  CANTEVAL  DESC"
        Dim Cmd As New SqlCommand(sql, Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("EvalProcesos_RptaJalados_ListaTareas")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_RptaErroneas_xRMDMTienda(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodProceso As Double,
                                                        ByVal psTop10 As Integer, ByVal psCodRM As String, ByVal psCodDM As String, ByVal psCodtienda As Double, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Sql As String = " SELECT top " & psTop10 & "  EVALPRO_PREGUNTA,'' AS COLOR, TAREADET_NOMBRE as nombre, EVALUACION_PROCESO, COUNT(EVALPRO_RESPUESTA ) AS CANT " _
                          & " FROM TBEVALUACION_PROCESOS_DETALLE AS A INNER JOIN TBCHEQUEO_TAREA_DETALLE AS B ON A.EVALPRO_PREGUNTA = TAREADET_CODIGO  " _
                          & " Inner join TBEVALUACION_PROCESO  as c on a.EVALUACION_CODIGO = c.EVALUACION_CODIGO " _
                          & " WHERE EVALPRO_RESPUESTA = '0' And c.EVALUACION_ESTADO = '3' and substring(EVALUACION_FECHA,1,4) = '" & psAño & "' And RTrim(Convert(nChar(10),EVALUACION_PROCESO )) " _
                          & " Like Case " & psCodProceso & "  When '0' Then '%' Else RTrim(Convert(nChar(10)," & psCodProceso & " ))  end "
        If psCodtienda <> 0 Then Sql = Sql & " and EVALUACION_OFICINA =  " & psCodtienda
        If psCodRM = "" And psCodDM <> "" And psCodtienda = 0 Then Sql = Sql & " And EVALUACION_RESPONSABLE In (" & psCodDM & ")"
        Sql = Sql & " GROUP BY EVALPRO_PREGUNTA, TAREADET_NOMBRE, EVALUACION_PROCESO order by COUNT(EVALPRO_RESPUESTA ) desc"
        Dim Cmd As New SqlCommand(Sql, Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RptaErrorneas")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Evaluacion_Proceso_RptaErrorneas
    Public Function Evaluacion_RptaErroneas_Original(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodPorceso As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RptaErrorneas", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodPorceso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RptaErrorneas")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_Puntaje(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodPregunta As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_PreguntaPuntaje", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPregunta", SqlDbType.Float).Value = psCodPregunta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_PreguntaPuntaje")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_PuntajexActividad(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodPregunta As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_ActrividadPuntaje", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPregunta", SqlDbType.Float).Value = psCodPregunta
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_ActrividadPuntaje")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Evaluacion_Proceso_RealcionRMDM
    Public Function Evaluacion_ListaRelacion_RMDM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RealcionRMDM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RealcionRMDM")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Evaluacion_Proceso_RealcionOficinaDM
    Public Function Evaluacion_ListaRelacion_OficinaDM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RealcionOficinaDM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RealcionOficinaDM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_ListaRelacion_OficinaXDM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double, ByVal psCodDM As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RealcionOficinaxDM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodDM", SqlDbType.VarChar).Value = psCodDM
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RealcionOficinaxDM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_ListaRelacion_RMDM_xRM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double, ByVal psCodRM As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RealcionRMDM_xRM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa 'psCodRM
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodRM", SqlDbType.VarChar).Value = psCodRM
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RealcionRMDM_xRM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_Insert_RMDAM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodRM As String, ByVal psCodDM As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evalucion_Proceso_Insert_RMDM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa 'psCodRM
        Cmd.Parameters.Add("@CodRM", SqlDbType.VarChar).Value = psCodRM
        Cmd.Parameters.Add("@CodDM", SqlDbType.VarChar).Value = psCodDM
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evalucion_Proceso_Insert_RMDM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_Delete_RMDAM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodRM As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evalucion_Proceso_Delete_RMDM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa 'psCodRM
        Cmd.Parameters.Add("@CodRM", SqlDbType.VarChar).Value = psCodRM
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evalucion_Proceso_Delete_RMDM")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Evaluacion_Delete_OficinaDM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodDM As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evalucion_Proceso_Delete_DMOFICINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa 'psCodRM
        Cmd.Parameters.Add("@CodRM", SqlDbType.VarChar).Value = psCodDM
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evalucion_Proceso_Delete_DMOFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Evalucion_Proceso_Insert_DMOFICINA
    Public Function Evaluacion_Insert_OficinaDM(ByVal psConexion As String, ByVal psCodDM As String, ByVal psCodOficina As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evalucion_Proceso_Insert_DMOFICINA", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodDM", SqlDbType.VarChar).Value = psCodDM 'psCodOficina
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = psCodOficina
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evalucion_Proceso_Insert_DMOFICINA")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProceso_DetallexNumEval
    Public Function Result_EvaluacionxNumEval(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodEval As Double, ByVal psNumEval As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_DetallexNumEval", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = psCodEval
        Cmd.Parameters.Add("@NumEval", SqlDbType.Float).Value = psNumEval
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_DetallexNumEval")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Dashboard(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_Estadistica", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_Estadistica")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Dashboard_xRM(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodRm As String, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_EstadisticaxRM", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodRM", SqlDbType.VarChar).Value = psCodRm
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_EstadisticaxRM")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_Evaluacion_Proceso_Estadistica_xTienda
    Public Function Lista_Dashboard_xTienda(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodTienda As Double, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_Estadistica_xTienda", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodTienda", SqlDbType.Float).Value = psCodTienda
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_Estadistica_xTienda")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Dashboard_RMDM_xTienda(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double, ByVal psNombreTienda As String, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RelacionRMDMxOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@NombreTienda", SqlDbType.VarChar).Value = psNombreTienda
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RelacionRMDMxOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Dashboard_RMDM_xProceso(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double, ByVal psNombreTienda As String, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RelacionRMDMxOficina_Qasa", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@NombreTienda", SqlDbType.VarChar).Value = psNombreTienda
        Cmd.Parameters.Add("@Año ", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RelacionRMDMxOficina_Qasa")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Dashboard_XRMDM_xTienda(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double, ByVal psCodDMRM As String, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RelacionRMDMxOficina_2", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodRMDM ", SqlDbType.VarChar).Value = psCodDMRM
        Cmd.Parameters.Add("@Año ", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RelacionRMDMxOficina_2")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Dashboard_XRMDM_xTienda_QASA(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodGrupoEmpresa As Double, ByVal psCodDMRM As String, ByVal psAño As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_Evaluacion_Proceso_RelacionRMDMxOficina_3", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodGrupoEmpresa", SqlDbType.Float).Value = psCodGrupoEmpresa
        Cmd.Parameters.Add("@CodRMDM ", SqlDbType.VarChar).Value = psCodDMRM
        Cmd.Parameters.Add("@Año ", SqlDbType.VarChar).Value = psAño
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Evaluacion_Proceso_RelacionRMDMxOficina_3")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_EvaluacionProceso_Puntaje_xNroEval

    Public Function Puntaje_xEvaluacion(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodProceso As Double, ByVal CodEval As Double) As DataTable
        'Prc_EvaluacionProceso_Puntaje
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Puntaje_xNroEval", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = pdCodProceso
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = CodEval
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Puntaje_xNroEval")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Puntaje_xProceso(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal pdCodProceso As Double) As DataTable
        'Prc_EvaluacionProceso_Puntaje
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Puntaje", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = pdCodProceso
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Puntaje")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProceso_TotalPreg_xPuntaje
    Public Function TotalPreg_xPuntaje_xNroEval(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal CodEval As Double, ByVal CodPreguntaExcepto As Double, ByVal CodPuntaje As String) As DataTable
        'Prc_EvaluacionProceso_Puntaje
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_TotalPreg_xPuntaje", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = CodEval
        Cmd.Parameters.Add("@CodPreguntaExcepto", SqlDbType.Float).Value = CodPreguntaExcepto
        Cmd.Parameters.Add("@CodPuntaje", SqlDbType.VarChar).Value = CodPuntaje
        '@CodPuntaje
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_TotalPreg_xPuntaje")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_EvaluacionProceso_TotalPreguntas
    Public Function TotalPreguntas_xProceso(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal CodProceso As Double, ByVal CodPreguntaExcepto As Double, ByVal CodEval As Double) As DataTable
        'Prc_EvaluacionProceso_Puntaje
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_TotalPreguntas", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = CodProceso
        Cmd.Parameters.Add("@CodPreguntaExcepto", SqlDbType.Float).Value = CodPreguntaExcepto
        Cmd.Parameters.Add("@CodEval", SqlDbType.Float).Value = CodEval
        '@CodPuntaje
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_TotalPreguntas")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function EvalLista_RptaJalados_xOficina(ByVal psCodEmpresa As String, ByVal psConexion As String, ByVal CodProceso As Double, ByVal psAño As String, ByVal psMesIni As String, ByVal psMesFin As String, ByVal CodOficina As Double, ByVal psDm As String) As DataTable
        'Prc_EvaluacionProceso_Puntaje
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaRespuestasJalados_xOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = psCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = CodProceso
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Cmd.Parameters.Add("@MesIni", SqlDbType.VarChar).Value = psMesIni
        Cmd.Parameters.Add("@MesFin", SqlDbType.VarChar).Value = psMesFin
        Cmd.Parameters.Add("@CodOficina", SqlDbType.Float).Value = CodOficina
        Cmd.Parameters.Add("@CodRm_f", SqlDbType.VarChar).Value = psDm
        Cmd.Parameters.Add("@CodTienda", SqlDbType.Float).Value = CodOficina
        '@CodPuntaje
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaRespuestasJalados_xOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Tareas_RptaJaladas(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodProceso As Double, ByVal psAño As String, ByVal psMesIni As String, ByVal psMesFin As String, ByVal psDm As String, ByVal pdCodTienda As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("prc_EvaluacionProceso_ListaTareas_RptaJaladas", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Cmd.Parameters.Add("@MesIni", SqlDbType.VarChar).Value = psMesIni
        Cmd.Parameters.Add("@MesFin", SqlDbType.VarChar).Value = psMesFin
        Cmd.Parameters.Add("@CodRm_f", SqlDbType.VarChar).Value = psDm
        Cmd.Parameters.Add("@CodTienda", SqlDbType.Float).Value = pdCodTienda
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("prc_EvaluacionProceso_ListaTareas_RptaJaladas")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_ActxTarea_RptaJaladas(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodProceso As Double, ByVal psCodTarea As Double, ByVal psAño As String, ByVal psMesIni As String, ByVal psMesFin As String, ByVal psDm As String, ByVal pdCodTienda As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaActividades_RptaJalados", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@CodTarea", SqlDbType.Float).Value = psCodTarea
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Cmd.Parameters.Add("@MesIni", SqlDbType.VarChar).Value = psMesIni
        Cmd.Parameters.Add("@MesFin", SqlDbType.VarChar).Value = psMesFin
        Cmd.Parameters.Add("@CodRm_f", SqlDbType.VarChar).Value = psDm
        Cmd.Parameters.Add("@CodTienda", SqlDbType.Float).Value = pdCodTienda
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaActividades_RptaJalados")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function Lista_Oficina_RptaJaladas(ByVal pCodEmpresa As String, ByVal psConexion As String, ByVal psCodProceso As Double, ByVal psAño As String, ByVal psMesIni As String, ByVal psMesFin As String, ByVal psDm As String, ByVal pdCodTienda As Double) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaOficina_RptaJalados", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodProceso", SqlDbType.Float).Value = psCodProceso
        Cmd.Parameters.Add("@Año", SqlDbType.VarChar).Value = psAño
        Cmd.Parameters.Add("@MesIni", SqlDbType.VarChar).Value = psMesIni
        Cmd.Parameters.Add("@MesFin", SqlDbType.VarChar).Value = psMesFin
        Cmd.Parameters.Add("@CodRm_f", SqlDbType.VarChar).Value = psDm
        Cmd.Parameters.Add("@CodTienda", SqlDbType.Float).Value = pdCodTienda
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaOficina_RptaJalados")
        Da.Fill(Dt)
        Return Dt
    End Function

    'Prc_EvaluacionProcesos_TipoEval_Lista
    Public Function Lista_TipoEvaluacion(ByVal pCodEmpresa As String, ByVal psConexion As String) As DataTable
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProcesos_TipoEval_Lista", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProcesos_TipoEval_Lista")
        Da.Fill(Dt)
        Return Dt
    End Function

    Public Function Lista_Oficina_DistanciaxPersonal(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                     ByVal pCodGrupo As Double, ByVal pLatitud As Double,
                                                     ByVal pLongitud As Double, ByVal psDistancia As String) As DataTable

        'Prc_EvalProceso_Distancia_OficinaxPersonal
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_EvalProceso_Distancia_OficinaxPersonal", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodGrupo", SqlDbType.Float).Value = pCodGrupo
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@latitud1 ", SqlDbType.Float).Value = pLatitud
        Cmd.Parameters.Add("@longitud1", SqlDbType.Float).Value = pLongitud
        Cmd.Parameters.Add("@Unidad_Metrica", SqlDbType.VarChar).Value = psDistancia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvalProceso_Distancia_OficinaxPersonal")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvalProceso_Distancia_PersonalxOficina
    Public Function Lista_Personal_DistanciaxOficina(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                     ByVal pCodGrupo As Double, ByVal pLatitud As Double,
                                                     ByVal pLongitud As Double, ByVal psDistancia As String) As DataTable

        'Prc_EvalProceso_Distancia_OficinaxPersonal
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_EvalProceso_Distancia_PersonalxOficina", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@latitud1 ", SqlDbType.Float).Value = pLatitud
        Cmd.Parameters.Add("@longitud1", SqlDbType.Float).Value = pLongitud
        Cmd.Parameters.Add("@Unidad_Metrica", SqlDbType.VarChar).Value = psDistancia
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvalProceso_Distancia_PersonalxOficina")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvalProceso_Distancia_PersonalxOficina_Filtro_xCargoEstado
    Public Function Lista_Personal_DistanciaxOficina_Filtro(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                     ByVal pCodGrupo As Double, ByVal pLatitud As Double,
                                                     ByVal pLongitud As Double, ByVal psDistancia As String,
                                                     ByVal pdCargo As Double, ByVal psEstado As String) As DataTable

        'Prc_EvalProceso_Distancia_OficinaxPersonal
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cmd As New SqlCommand("Prc_EvalProceso_Distancia_PersonalxOficina_Filtro_xCargoEstado", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@latitud1 ", SqlDbType.Float).Value = pLatitud
        Cmd.Parameters.Add("@longitud1", SqlDbType.Float).Value = pLongitud
        Cmd.Parameters.Add("@Unidad_Metrica", SqlDbType.VarChar).Value = psDistancia
        Cmd.Parameters.Add("@CodCargo", SqlDbType.Float).Value = pdCargo
        Cmd.Parameters.Add("@Estado", SqlDbType.VarChar).Value = psEstado
        Cmd.Parameters.Add("@CodGE", SqlDbType.Float).Value = pCodGrupo
        Cmd.Parameters.Add("@CodGEE", SqlDbType.VarChar).Value = pCodEmpresa
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvalProceso_Distancia_PersonalxOficina_Filtro_xCargoEstado")
        Da.Fill(Dt)
        Return Dt
    End Function
    '
    Public Function EvalProcesos_Insertar_PrevencionCovid(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                          ByVal psCodPersonal As String, ByVal pFecha As String,
                                                          ByVal pHora As String, ByVal pTempInicial As Double,
                                                          ByVal pTos As String, ByVal pDolorGarganta As String,
                                                          ByVal pEstornudo As String, ByVal pDificultaRespiratoria As String,
                                                          ByVal pDolorMuscular As String, ByVal pTempMedioTurno As Double,
                                                          ByVal pTempFinal As Double, psObservacion As String, ByVal pUser As String) As DataTable

        'Prc_EvalProceso_Distancia_OficinaxPersonal
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Insert_PrevencionCovid19", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa ", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = pHora
        Cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = pUser
        Cmd.Parameters.Add("@TempInicial", SqlDbType.Float).Value = pTempInicial
        Cmd.Parameters.Add("@Tos", SqlDbType.VarChar).Value = pTos
        Cmd.Parameters.Add("@DolorGarganta", SqlDbType.VarChar).Value = pDolorGarganta
        Cmd.Parameters.Add("@Estornudo", SqlDbType.VarChar).Value = pEstornudo
        Cmd.Parameters.Add("@DificultaRespiratoria", SqlDbType.VarChar).Value = pDificultaRespiratoria
        Cmd.Parameters.Add("@DolorMuscular", SqlDbType.VarChar).Value = pDolorMuscular
        Cmd.Parameters.Add("@TempMedioTurno", SqlDbType.Float).Value = pTempMedioTurno
        Cmd.Parameters.Add("@TempFinal", SqlDbType.Float).Value = pTempFinal '@Observacion
        Cmd.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = psObservacion '@Observacion
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Insert_PrevencionCovid19")
        Da.Fill(Dt)
        Return Dt
    End Function
    'Prc_EvaluacionProceso_Update_PrevencionCovid19

    Public Function EvalProcesos_Update_PrevencionCovid(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                          ByVal psCodPersonal As String, ByVal pFecha As String,
                                                          ByVal pHora As String, ByVal pTempMedioTurno As Double,
                                                          ByVal pTempFinal As Double) As DataTable

        'Prc_EvalProceso_Distancia_OficinaxPersonal
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Update_PrevencionCovid19", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa ", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Cmd.Parameters.Add("@Hora", SqlDbType.VarChar).Value = pHora
        Cmd.Parameters.Add("@TempMedioTurno", SqlDbType.Float).Value = pTempMedioTurno
        Cmd.Parameters.Add("@TempFinal", SqlDbType.Float).Value = pTempFinal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Update_PrevencionCovid19")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function EvalProcesos_PrevencionCovid_ExistePersonalFecha(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                                     ByVal psCodPersonal As String, ByVal pFecha As String) As DataTable

        'Prc_EvalProceso_Distancia_OficinaxPersonal
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_Prevencion_ExistePersonalFecha", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa ", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Cmd.Parameters.Add("@Fecha", SqlDbType.VarChar).Value = pFecha
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_Prevencion_ExistePersonalFecha")
        Da.Fill(Dt)
        Return Dt
    End Function
    Public Function EvalProcesos_PrevencionCovid_ListaEncuesta(ByVal pCodEmpresa As String, ByVal psConexion As String,
                                                               ByVal psCodPersonal As String) As DataTable

        'Prc_EvalProceso_Distancia_OficinaxPersonal
        Dim Cn As New SqlConnection(psConexion)
        Dim Cmd As New SqlCommand("Prc_EvaluacionProceso_ListaPrevencion_xPersonal", Cn) With {
        .CommandType = CommandType.StoredProcedure}
        Cmd.Parameters.Add("@CodEmpresa ", SqlDbType.VarChar).Value = pCodEmpresa
        Cmd.Parameters.Add("@CodPersonal", SqlDbType.VarChar).Value = psCodPersonal
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_EvaluacionProceso_ListaPrevencion_xPersonal")
        Da.Fill(Dt)
        Return Dt
    End Function
End Class