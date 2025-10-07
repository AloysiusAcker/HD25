Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports WebGestor
Imports System.Web.Services
Partial Class EvaluacionProcesos_EvalProcesos
    Inherits System.Web.UI.Page

    Dim ObjProceso As New ClsEval_Proceso
    Dim FnProceso As New clsEval_Proceso_Funciones
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Listar_Proceso_xDM(Session("User"))
            lblCodProceso.Text = ""
            lblError.Text = ""
            lblEtiqueta.Text = ""
            Call FnProceso.CargarDM(DdlBusResponsable, 10, Session("CodEmpresa"), Session("CodGrupoEmpresa"))
            Call FnProceso.Llenar_Proceso(DdlProceso, Session("CodEmpresa"), Session("Ruta_Emp"))
            DdlBusResponsable.Items.Add("< Todos >")
            DdlBusResponsable.SelectedValue = Session("User")
            ddlEstado.SelectedValue = "1"
            DdlAño.Items.Clear()
            Call LlenaAno(DdlAño)
            DdlAño.SelectedValue = CInt(Left(FechaActual, 4))
            DdlAño.Focus()
            Call Listar_Proceso()
            Me.Page.Session.Timeout = 1080.0F
        End If
    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Cancelar_Click(sender, e)
        Call Listar_Proceso()
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Listar_Proceso_xDM(ByVal psCodDM As String)
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try 'Lista_Evaluacion_xDM
            dt = ObjProceso.Lista_Evaluacion_xDM(Session("CodEmpresa"), Session("Ruta_Emp"), psCodDM)
            gwLista.DataSource = dt
            gwLista.DataBind()
            Call Calcular_Puntaje()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub Calcular_Puntaje()
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Dim pdPromedioFinal As Double = 0
        Try

            For i = 0 To gwLista.Rows.Count - 1
                If i = 30 Then
                    lblError.Text = ""
                End If
                If gwLista.Rows(i).Cells(11).Text = "2" Then
                    If gwLista.Rows(i).Cells(2).Text = 9 Then
                        pdPromedioFinal = Calcular_Promedio_Conexion(Nz(gwLista.Rows(i).Cells(2).Text), Nz(gwLista.Rows(i).Cells(5).Text))
                        gwLista.Rows(i).Cells(10).Text = pdPromedioFinal & "%"
                        If pdPromedioFinal >= 90 Then
                            gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Green
                            gwLista.Rows(i).Cells(10).Font.Size = 14
                            gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                            gwLista.Rows(i).Cells(10).Font.Bold = True
                        Else
                            gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Red
                            gwLista.Rows(i).Cells(10).Font.Size = 14
                            gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                            gwLista.Rows(i).Cells(10).Font.Bold = True
                        End If
                    Else
                        dtListado = ObjProceso.Evaluacion_Resultado(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(Replace(gwLista.Rows(i).Cells(5).Text, "&nbsp;", "")), 218, Nz(gwLista.Rows(i).Cells(2).Text))
                        If dtListado.Rows.Count > 0 Then
                            For Each drResul As DataRow In dtListado.Rows
                                If Not IsDBNull(drResul(0)) Then
                                    gwLista.Rows(i).Cells(10).Text = drResul(0) & "%"
                                    If Nz(drResul(0).ToString) >= 90 Then
                                        gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Green
                                        gwLista.Rows(i).Cells(10).Font.Size = 14
                                        gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                                        gwLista.Rows(i).Cells(10).Font.Bold = True
                                    Else
                                        gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Red
                                        gwLista.Rows(i).Cells(10).Font.Size = 14
                                        gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                                        gwLista.Rows(i).Cells(10).Font.Bold = True
                                    End If
                                End If
                            Next
                        End If
                    End If
                End If
            Next
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Listar_Proceso()
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim psCodDm As String = ""
        Dim psCodProceso As Double = 0
        lblError.Text = ""
        Dim psEstado As String = ""
        Try
            If DdlBusResponsable.SelectedValue <> "< Todos >" Then
                psCodDm = DdlBusResponsable.SelectedValue
            End If
            If ddlEstado.SelectedValue <> "< Todos >" Then
                psEstado = ddlEstado.SelectedValue
            End If
            If DdlProceso.SelectedValue <> "< Seleccionar >" Then
                psCodProceso = DdlProceso.SelectedValue
            End If
            dt = ObjProceso.Lista_Evaluacion_ConFiltros(Session("CodEmpresa"), Session("Ruta_Emp"), psCodDm, psCodProceso, psEstado, DdlAño.Text)
            gwLista.DataSource = dt
            gwLista.DataBind()

            Call Calcular_Puntaje()
            Dim pdPromedioFinal As Double = 0
            Dim psPromedio As String = ""
            For i = 0 To gwLista.Rows.Count - 1
                If gwLista.Rows(i).Cells(11).Text <> "1" Then
                    If Right(gwLista.Rows(i).Cells(10).Text, 1) = "%" Then
                        psPromedio = Left(gwLista.Rows(i).Cells(10).Text, Len(gwLista.Rows(i).Cells(10).Text) - 1)
                        pdPromedioFinal = CDbl(Nz(psPromedio))
                    ElseIf Replace(gwLista.Rows(i).Cells(10).Text, "&nbsp;", "") = "" Then
                        pdPromedioFinal = 0
                    Else
                        pdPromedioFinal = Nz(gwLista.Rows(i).Cells(10).Text)
                    End If
                    gwLista.Rows(i).Cells(10).Text = pdPromedioFinal & "%"
                    If gwLista.Rows(i).Cells(2).Text = 9 Then
                        If pdPromedioFinal >= 90 Then
                            gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Green
                            gwLista.Rows(i).Cells(10).Font.Size = 14
                            gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                            gwLista.Rows(i).Cells(10).Font.Bold = True
                        Else
                            gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Red
                            gwLista.Rows(i).Cells(10).Font.Size = 14
                            gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                            gwLista.Rows(i).Cells(10).Font.Bold = True
                        End If
                    Else
                        If pdPromedioFinal >= 90 Then
                            gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Green
                            gwLista.Rows(i).Cells(10).Font.Size = 14
                            gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                            gwLista.Rows(i).Cells(10).Font.Bold = True
                        Else
                            gwLista.Rows(i).Cells(10).BackColor = Drawing.Color.Red
                            gwLista.Rows(i).Cells(10).Font.Size = 14
                            gwLista.Rows(i).Cells(10).ForeColor = Drawing.Color.White
                            gwLista.Rows(i).Cells(10).Font.Bold = True
                        End If
                    End If
                End If
            Next
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub gwLista_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gwLista.RowCommand

        Dim dt As New DataTable
        Dim psCodEval As Double = 0
        Dim psCodPreg As Double = 0
        Try
            Dim psCodProceso As Double = 0
            lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            psCodProceso = Replace(gwLista.Rows(Index).Cells(2).Text, "&nbsp;", "")
            lblCodEval.Text = Replace(gwLista.Rows(Index).Cells(5).Text, "&nbsp;", "")
            lblEstado.Text = Replace(gwLista.Rows(Index).Cells(11).Text, "&nbsp;", "")
            lblCodOficina.Text = Replace(gwLista.Rows(Index).Cells(13).Text, "&nbsp;", "")
            lblCodProceso.Text = psCodProceso
            gwListaDetalle.Visible = False
            gwListaDetallePlan.Visible = False
            gwListaPlanAccion.Visible = False
            Cancelar.Visible = False
            LnkCancelar.Visible = False
            LnkGuardar.Visible = False
            GuardarRptas.Visible = False
            Exportar.Visible = False
            Cerrar.Visible = False
            psCodEval = Nz(lblCodEval.Text)
            lblEtiqueta3.Visible = False
            lblEtiqueta2.Text = ""
            lblEtiqueta3.Text = ""
            txtResultado.Text = ""
            lblEtiqueta.Text = ""

            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Evaluar" Then
                lblEtiqueta.Visible = True
                lblEtiqueta.Text = "Evaluación Nro. " & Replace(gwLista.Rows(Index).Cells(5).Text, "&nbsp;", "") & " - " & Replace(gwLista.Rows(Index).Cells(3).Text, "&nbsp;", "")
                txtResultado.Text = Replace(gwLista.Rows(Index).Cells(10).Text, "&nbsp;", "")
                Lista_Tareas(psCodProceso)
                lblEtiqueta2.Text = "Resultado Final"
                lblEtiqueta2.Visible = True
                txtResultado.Visible = True
                txtResultado.ReadOnly = True
                gwListaDetalle.Visible = True
                Exportar.Visible = True
                Cancelar.Visible = True
                If lblEstado.Text <> "3" Then
                    GuardarRptas.Visible = True
                    Cerrar.Visible = True
                End If
                Call Lista_Puntaje_xProceso(Nz(gwLista.Rows(Index).Cells(2).Text), Nz(gwLista.Rows(Index).Cells(5).Text), gwLista.Rows(Index).Cells(11).Text)
            End If
            If e.CommandName = "Accion" And lblEstado.Text = "3" Then
                lblEtiqueta.Text = "Evaluación Nro. " & Replace(gwLista.Rows(Index).Cells(5).Text, "&nbsp;", "") & " - " & Replace(gwLista.Rows(Index).Cells(3).Text, "&nbsp;", "")
                lblEtiqueta3.Text = "Plan de Acción a tomar"
                lblEtiqueta3.Visible = True
                gwListaDetallePlan.Visible = True
                gwListaPlanAccion.Visible = True
                dt = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), 0, psCodEval, "S", Nz(lblCodOficina.Text))
                gwListaDetallePlan.DataSource = dt
                gwListaDetallePlan.DataBind()
                LnkCancelar.Visible = True
                Call Lista_PlanAccion(psCodEval)
            End If

            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub

    Private Sub Lista_Puntaje_xProceso(ByVal psCodProceso As Double, ByVal psCodEval As Double, ByVal psEstado As String)
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try 'Lista_Evaluacion_xDM
            Call Calcular_TotalPuntos(psCodProceso, psCodEval, psEstado)
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub Calcular_TotalPuntos(ByVal psCodProceso As Double, ByVal psCodEval As Double, ByVal psEstado As String)
        lblError.Text = ""

        Dim dt As New DataTable
        Dim dtTarea As New DataTable
        Dim dtActividades As New DataTable
        Dim dtListado As New DataTable
        Dim i As Integer = 0
        Dim ii As Integer = 0
        Dim psAplica As String = ""
        Dim drT As DataRow
        Dim cmdGlobal As New SqlCommand
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        dtListado.Columns.Add("Puntaje")
        dtListado.Columns.Add("Total")
        GvPuntaje.DataSource = Nothing
        GvPuntaje.DataBind()
        Dim psTipoEval_Proceso As String = ""
        Dim psTipoeval_Codigo As Double = 0
        Try
            cn.Open() : cmdGlobal.Connection = cn


            dt = ObjProceso.Lista_Evaluacion_xCodEval(Session("CodEmpresa"), Session("Ruta_Emp"), CInt(Nz(lblCodEval.Text)))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    psTipoeval_Codigo = Nz(dr("EVALUACION_TIPO"))
                    psTipoEval_Proceso = Nz(dr("TIPOEVAL_PROCESO"))
                Next
            End If
            dt = Nothing


            If psEstado <> "3" Then
                dt = ObjProceso.Puntaje_xProceso(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso)
            Else
                dt = ObjProceso.Puntaje_xEvaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso, psCodEval)
            End If
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("Puntaje") = Nu(dr("PUNTAJE"))
                    If Nu(dr("PUNTAJE")) = "No Aplica" Then psAplica = "SI"
                    dtTarea = ObjProceso.TotalPreg_xPuntaje_xNroEval(Session("CodEmpresa"), Session("Ruta_Emp"), psCodEval, 218, Nu(dr("PUNTAJE")))
                    If dtTarea.Rows.Count > 0 Then
                        For Each drTarea As DataRow In dtTarea.Rows
                            ii = ii + 1
                            drT("total") = Nu(drTarea("total"))
                            dtActividades = Nothing
                        Next
                    End If
                    dtListado.Rows.Add(drT)
                    dtTarea = Nothing
                Next
            End If

            If psAplica = "" Then
                drT = dtListado.NewRow
                drT("Puntaje") = "No Aplica"
                dtTarea = ObjProceso.TotalPreg_xPuntaje_xNroEval(Session("CodEmpresa"), Session("Ruta_Emp"), psCodEval, 218, "No Aplica")
                If dtTarea.Rows.Count > 0 Then
                    For Each drTarea As DataRow In dtTarea.Rows
                        ii = ii + 1
                        drT("total") = Nu(drTarea("total"))
                        dtActividades = Nothing
                    Next
                End If
                dtListado.Rows.Add(drT)
                dtTarea = Nothing
            End If
            'TotalPreguntas_xProceso
            drT = dtListado.NewRow
            drT("Puntaje") = "Total Preguntas"

            dtTarea = ObjProceso.TotalPreguntas_xProceso(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso, 218, psCodEval)
            If dtTarea.Rows.Count > 0 Then
                For Each drTarea As DataRow In dtTarea.Rows
                    ii = ii + 1
                    drT("total") = Nu(drTarea(0))
                    dtActividades = Nothing
                Next
            End If
            dtListado.Rows.Add(drT)
            dtTarea = Nothing

            drT = dtListado.NewRow
            drT("Puntaje") = "Total Promedio"
            drT("total") = txtResultado.Text
            dtListado.Rows.Add(drT)

            GvPuntaje.DataSource = dtListado
            GvPuntaje.DataBind()

            GvPuntaje.Visible = True

            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub

    Private Sub Lista_PlanAccion(ByVal psCodEval As Double)
        lblError.Text = ""

        Dim dt As New DataTable
        Dim dtTarea As New DataTable
        Dim dtActividades As New DataTable
        Dim dtListado As New DataTable
        Dim i As Integer = 0
        Dim ii As Integer = 0
        Dim drT As DataRow
        Dim cmdGlobal As New SqlCommand
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        dtListado.Columns.Add("c1")
        dtListado.Columns.Add("c2")
        dtListado.Columns.Add("c3")
        dtListado.Columns.Add("c4")
        dtListado.Columns.Add("c5")
        Try
            cn.Open() : cmdGlobal.Connection = cn
            dt = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), 0, psCodEval, "S", Nz(lblCodOficina.Text))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = Nu(dr("EVALPRO_PREGUNTA"))
                    dtTarea = ObjProceso.Evaluacion_PlanAccion(Session("CodEmpresa"), Session("Ruta_Emp"), psCodEval, Nz(dr("EVALPRO_PREGUNTA")))
                    If dtTarea.Rows.Count > 0 Then
                        For Each drTarea As DataRow In dtTarea.Rows
                            ii = ii + 1
                            drT("c2") = Nu(drTarea("ACCION_DESCRIPCION"))
                            drT("c4") = Nu(drTarea("Fecha"))
                            drT("c5") = Nu(drTarea("ACCION_ESTADO"))
                            dtActividades = Nothing
                        Next
                    End If
                    dtListado.Rows.Add(drT)
                    dtTarea = Nothing
                Next
            End If

            gwListaPlanAccion.DataSource = dtListado
            gwListaPlanAccion.DataBind()
            Call CargarDatosFaltantes_PlanAccion(lblCodEval.Text)
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Private Sub Lista_Tareas(ByVal psCodProceso As Double)
        lblError.Text = ""
        Dim dt As New DataTable
        Dim dtTarea As New DataTable
        Dim dtActividades As New DataTable
        Dim dtActividades2 As New DataTable
        Dim dtListado As New DataTable
        Dim i As Integer = 0
        Dim ii As Integer = 0
        Dim j As Integer = 0
        Dim drT As DataRow
        Dim cmdGlobal As New SqlCommand
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        dtListado.Columns.Add("c1")
        dtListado.Columns.Add("c2")
        dtListado.Columns.Add("c4")
        dtListado.Columns.Add("c5")
        dtListado.Columns.Add("c6")
        Dim psTipoEval_Proceso As String = ""
        Dim psTipoeval_Codigo As Double = 0
        'Lista_Evaluacion_xCodEval

        Try
            cn.Open() : cmdGlobal.Connection = cn
            dt = ObjProceso.Lista_Evaluacion_xCodEval(Session("CodEmpresa"), Session("Ruta_Emp"), CInt(Nz(lblCodEval.Text)))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    psTipoeval_Codigo = Nz(dr("EVALUACION_TIPO"))
                    psTipoEval_Proceso = Nz(dr("TIPOEVAL_PROCESO"))
                Next
            End If
            dt = Nothing


            If psTipoEval_Proceso > 0 Then
                dt = ObjProceso.Lista_Tareas(Session("CodEmpresa"), Session("Ruta_Emp"), psTipoEval_Proceso)
            Else
                dt = ObjProceso.Lista_Tareas(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso)
            End If
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = i & " .- " & Nu(dr("TAREA_NOMBRE"))
                    drT("c2") = ""
                    drT("c5") = Nu(dr("TAREA_CODIGO"))
                    drT("c6") = "F"
                    dtListado.Rows.Add(drT)
                    If psTipoEval_Proceso > 0 And psTipoeval_Codigo > 0 Then
                        dtTarea = ObjProceso.Lista_Actividades_xTipoEval(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(dr("TAREA_CODIGO")), psTipoeval_Codigo)
                    Else
                        dtTarea = ObjProceso.Lista_Actividades_xTarea(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(dr("TAREA_CODIGO")))
                    End If
                    If dtTarea.Rows.Count > 0 Then
                        For Each drTarea As DataRow In dtTarea.Rows
                            ii = ii + 1
                            drT = dtListado.NewRow()
                            drT("c1") = ".           ." & i.ToString & "." & ii.ToString & " .- " & Nu(drTarea("TAREADET_NOMBRE"))
                            drT("c5") = Nu(drTarea("TAREADET_CODIGO"))
                            dtActividades = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(drTarea("TAREADET_CODIGO")), 0, "N", Nz(lblCodOficina.Text))
                            If dtActividades.Rows.Count > 0 Then
                                For Each drAct As DataRow In dtActividades.Rows
                                    drT("c2") = Nu(drAct("EVALPRO_RESPUESTA")) '
                                Next
                            End If
                            dtActividades = Nothing
                            dtActividades = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(drTarea("TAREADET_CODIGO")), Nz(lblCodEval.Text), "N", Nz(lblCodOficina.Text))
                            If dtActividades.Rows.Count > 0 Then
                                For Each drAct2 As DataRow In dtActividades.Rows
                                    drT("c4") = Nu(drAct2("EVALPRO_OBSERVACION")) '
                                Next
                            End If
                            dtActividades = Nothing
                            'Lista_Actividades_xActividad
                            dtActividades = ObjProceso.Lista_Actividades_xActividad(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(drTarea("TAREADET_CODIGO")))
                            If dtActividades.Rows.Count > 0 Then
                                drT("c6") = "A"
                                dtListado.Rows.Add(drT)
                                For Each drAct As DataRow In dtActividades.Rows
                                    j = j + 1
                                    drT = dtListado.NewRow()
                                    drT("c1") = ".           ." & i.ToString & "." & ii.ToString & "." & j.ToString & " .- " & Nu(drAct("ACTIVIDAD_NOMBRE"))
                                    drT("c5") = Nu(drAct("ACTIVIDAD_CODIGO"))
                                    drT("c6") = Nu(drTarea("TAREADET_CODIGO"))
                                    dtActividades2 = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(drAct("ACTIVIDAD_CODIGO")), 0, "N", Nz(lblCodOficina.Text))
                                    If dtActividades2.Rows.Count > 0 Then
                                        For Each drAct2 As DataRow In dtActividades2.Rows
                                            drT("c2") = Nu(drAct2("EVALPRO_RESPUESTA")) '
                                        Next
                                    End If
                                    dtListado.Rows.Add(drT)
                                Next
                            Else
                                drT("c6") = "T"
                                dtListado.Rows.Add(drT)
                            End If
                            dtActividades = Nothing
                        Next
                    End If
                    dtTarea = Nothing
                Next
            End If

            Dim columna As New BoundField
            Dim psCantEval As Double = 0
            Dim ia As Integer = 0
            Dim Colum As Integer = 0
            Dim psColObs As String = ""

            gwListaDetalle.DataSource = dtListado
            gwListaDetalle.DataBind()
            gwListaDetalle.Visible = True
            dtListado = Nothing
            gwListaDetalle.Columns(6).Visible = False : gwListaDetalle.Columns(7).Visible = False
            gwListaDetalle.Columns(8).Visible = False : gwListaDetalle.Columns(9).Visible = False
            gwListaDetalle.Columns(10).Visible = False : gwListaDetalle.Columns(11).Visible = False
            gwListaDetalle.Columns(12).Visible = False : gwListaDetalle.Columns(13).Visible = False
            gwListaDetalle.Columns(14).Visible = False

            With gwListaDetalle
                For i = 0 To .Rows.Count - 1
                    psColObs = "txtObs"
                    Dim txtObs As TextBox = .Rows(i).Cells(3).FindControl(psColObs)
                    Dim CmbPreg As DropDownList = gwListaDetalle.Rows(i).Cells(2).FindControl("cmbRpta")
                    Dim CmbPreg2 As DropDownList = gwListaDetalle.Rows(i).Cells(6).FindControl("cmbRpta2")
                    Dim CmbPreg3 As DropDownList = gwListaDetalle.Rows(i).Cells(7).FindControl("cmbRpta3")
                    Dim CmbPreg4 As DropDownList = gwListaDetalle.Rows(i).Cells(8).FindControl("cmbRpta4")
                    Dim CmbPreg5 As DropDownList = gwListaDetalle.Rows(i).Cells(9).FindControl("cmbRpta5")
                    Dim CmbPreg6 As DropDownList = gwListaDetalle.Rows(i).Cells(10).FindControl("cmbRpta6")
                    Dim CmbPreg7 As DropDownList = gwListaDetalle.Rows(i).Cells(11).FindControl("cmbRpta7")
                    Dim CmbPreg8 As DropDownList = gwListaDetalle.Rows(i).Cells(12).FindControl("cmbRpta8")
                    Dim CmbPreg9 As DropDownList = gwListaDetalle.Rows(i).Cells(13).FindControl("cmbRpta9")
                    Dim CmbPreg10 As DropDownList = gwListaDetalle.Rows(i).Cells(14).FindControl("cmbRpta10")
                    dt = ObjProceso.Lista_ActividadesxTarea_CantEval(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(Replace(gwListaDetalle.Rows(i).Cells(4).Text, "&nbsp;", "")))
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            psCantEval = Nz(dr("TAREA_CANTEVAL"))
                        Next
                    End If
                    Colum = 0
                    For ia = 2 To psCantEval
                        If ia = 2 Then gwListaDetalle.Columns(6).Visible = True
                        If ia = 3 Then gwListaDetalle.Columns(7).Visible = True
                        If ia = 4 Then gwListaDetalle.Columns(8).Visible = True
                        If ia = 5 Then gwListaDetalle.Columns(9).Visible = True
                        If ia = 6 Then gwListaDetalle.Columns(10).Visible = True
                        If ia = 7 Then gwListaDetalle.Columns(11).Visible = True
                        If ia = 8 Then gwListaDetalle.Columns(12).Visible = True
                        If ia = 9 Then gwListaDetalle.Columns(13).Visible = True
                        If ia = 10 Then gwListaDetalle.Columns(14).Visible = True
                    Next
                    If gwListaDetalle.Rows(i).Cells(5).Text = "F" Then
                        CmbPreg.Visible = False : CmbPreg2.Visible = False : CmbPreg3.Visible = False
                        CmbPreg4.Visible = False : CmbPreg5.Visible = False : CmbPreg6.Visible = False
                        CmbPreg7.Visible = False : CmbPreg8.Visible = False : CmbPreg9.Visible = False
                        CmbPreg10.Visible = False : txtObs.Visible = False
                        gwListaDetalle.Rows(i).Cells(0).Font.Size = "12"
                        gwListaDetalle.Rows(i).Cells(0).Font.Bold = True
                    ElseIf gwListaDetalle.Rows(i).Cells(5).Text = "A" Then
                        CmbPreg.Enabled = False : CmbPreg2.Enabled = False : CmbPreg3.Enabled = False
                        CmbPreg4.Enabled = False : CmbPreg5.Enabled = False : CmbPreg6.Enabled = False
                        CmbPreg7.Enabled = False : CmbPreg8.Enabled = False : CmbPreg9.Enabled = False
                        CmbPreg10.Enabled = False : txtObs.Visible = False
                        gwListaDetalle.Rows(i).Cells(0).Font.Size = "11"
                        gwListaDetalle.Rows(i).Cells(0).Font.Bold = True
                        CmbPreg.Font.Size = "10"
                        CmbPreg.Font.Bold = True
                    End If
                Next
            End With

            Dim pdPromedioFinal As Double = 0
            If txtResultado.Text = "" Then
                If psCodProceso = 9 Then
                    pdPromedioFinal = Calcular_Promedio_Conexion(psCodProceso, Nz(lblCodEval.Text))
                    txtResultado.Text = pdPromedioFinal
                    If pdPromedioFinal >= 90 Then
                        txtResultado.BackColor = Drawing.Color.Green
                        txtResultado.ForeColor = Drawing.Color.White
                        txtResultado.Font.Bold = True
                    Else
                        txtResultado.BackColor = Drawing.Color.Red
                        txtResultado.ForeColor = Drawing.Color.White
                        txtResultado.Font.Bold = True
                    End If
                    txtResultado.Text = txtResultado.Text & " %"
                Else
                    dtListado = ObjProceso.Evaluacion_Resultado(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(lblCodEval.Text), 218, (psCodProceso))
                    If dtListado.Rows.Count > 0 Then
                        For Each drResul As DataRow In dtListado.Rows
                            If Not IsDBNull(drResul(0)) Then
                                txtResultado.Text = drResul(0)
                                If Nz(drResul(0).ToString) >= 90 Then
                                    txtResultado.BackColor = Drawing.Color.Green
                                    txtResultado.ForeColor = Drawing.Color.White
                                    txtResultado.Font.Bold = True
                                Else
                                    txtResultado.BackColor = Drawing.Color.Red
                                    txtResultado.ForeColor = Drawing.Color.White
                                    txtResultado.Font.Bold = True
                                End If
                                txtResultado.Text = txtResultado.Text & " %"
                            End If
                        Next
                    End If
                End If
            End If
            Call CargarDatosFaltantes_Evaluacion()
            Call Lista_Exportar(psCodProceso)
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub

    Private Sub Lista_Exportar(ByVal psCodProceso As Double)
        lblError.Text = ""
        Dim dt As New DataTable
        Dim dtTarea As New DataTable
        Dim dtActividades As New DataTable
        Dim dtListado As New DataTable
        Dim i As Integer = 0
        Dim ij As Integer = 0
        Dim ii As Integer = 0
        Dim drT As DataRow
        Dim cmdGlobal As New SqlCommand
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        dtListado.Columns.Add("c1")
        dtListado.Columns.Add("c2")
        dtListado.Columns.Add("c3")
        dtListado.Columns.Add("c4")
        dtListado.Columns.Add("c0")
        dtListado.Columns.Add("c5")
        dtListado.Columns.Add("c6")
        dtListado.Columns.Add("c7")
        dtListado.Columns.Add("c8")
        dtListado.Columns.Add("c9")
        dtListado.Columns.Add("c10")
        dtListado.Columns.Add("c11")
        dtListado.Columns.Add("c12")
        dtListado.Columns.Add("c13")
        Dim psCantEval As Double = 0
        Try
            cn.Open() : cmdGlobal.Connection = cn
            dt = ObjProceso.Lista_Tareas(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = i & " .- " & Nu(dr("TAREA_NOMBRE"))
                    drT("c2") = ""
                    drT("c0") = Nu(dr("TAREA_CODIGO"))
                    dtListado.Rows.Add(drT)
                    dtTarea = ObjProceso.Lista_Actividades_xTarea(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(dr("TAREA_CODIGO")))
                    If dtTarea.Rows.Count > 0 Then
                        For Each drTarea As DataRow In dtTarea.Rows
                            ii = ii + 1
                            drT = dtListado.NewRow()
                            drT("c1") = ".           ." & i.ToString & "." & ii.ToString & " .- " & Nu(drTarea("TAREADET_NOMBRE"))
                            drT("c0") = Nu(drTarea("TAREADET_CODIGO"))
                            dtActividades = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(drTarea("TAREADET_CODIGO")), 0, "N", Nz(lblCodOficina.Text))
                            If dtActividades.Rows.Count > 0 Then
                                For Each drAct As DataRow In dtActividades.Rows
                                    drT("c2") = Nu(drAct("EVALPRO_RESPUESTA")) '
                                Next
                            End If
                            dtActividades = Nothing
                            dtActividades = ObjProceso.Lista_ActividadesxTarea_CantEval(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(drTarea("TAREADET_CODIGO")))
                            If dtActividades.Rows.Count > 0 Then
                                For Each drCant As DataRow In dtActividades.Rows
                                    psCantEval = Nz(drCant("TAREA_CANTEVAL"))
                                Next
                            End If
                            If Nz(drTarea("TAREADET_CODIGO")) = 176 Then
                                ij = 1
                            End If
                            dtActividades = Nothing
                            dtActividades = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(drTarea("TAREADET_CODIGO")), Nz(lblCodEval.Text), "N", Nz(lblCodOficina.Text))
                            If dtActividades.Rows.Count > 0 Then
                                For Each drAct2 As DataRow In dtActividades.Rows
                                    If Not IsDBNull(drAct2("EVALPRO_RESPUESTA")) And Not IsDBNull(drAct2("EVALPRO_NUMERO")) Then
                                        If Nz(drAct2("EVALPRO_NUMERO")) = 1 Then
                                            drT("c3") = Nu(drAct2("EVALPRO_RESPUESTA"))
                                            drT("c4") = Nu(drAct2("EVALPRO_OBSERVACION"))
                                        End If
                                    End If
                                Next
                            End If
                            dtActividades = Nothing
                            dtListado.Rows.Add(drT)
                        Next
                    End If
                    dtTarea = Nothing
                Next
            End If

            Dim columna As New BoundField
            Dim ia As Integer = 0
            Dim Colum As Integer = 0

            gwListaExportar.DataSource = dtListado
            gwListaExportar.DataBind()
            gwListaExportar.Visible = True
            dtListado = Nothing
            gwListaExportar.Columns(5).Visible = False : gwListaExportar.Columns(6).Visible = False : gwListaExportar.Columns(7).Visible = False
            gwListaExportar.Columns(8).Visible = False : gwListaExportar.Columns(9).Visible = False
            gwListaExportar.Columns(10).Visible = False : gwListaExportar.Columns(11).Visible = False
            gwListaExportar.Columns(12).Visible = False : gwListaExportar.Columns(13).Visible = False

            For i = 0 To gwListaExportar.Rows.Count - 1
                dtActividades = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(Replace(gwListaDetalle.Rows(i).Cells(4).Text, "&nbsp;", "")), Nz(lblCodEval.Text), "N", Nz(lblCodOficina.Text))
                If dtActividades.Rows.Count > 0 Then
                    For Each drAct2 As DataRow In dtActividades.Rows
                        dt = ObjProceso.Lista_ActividadesxTarea_CantEval(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(Replace(gwListaDetalle.Rows(i).Cells(4).Text, "&nbsp;", "")))
                        If dt.Rows.Count > 0 Then
                            For Each drCant As DataRow In dt.Rows
                                psCantEval = Nz(drCant("TAREA_CANTEVAL"))
                            Next
                        End If
                        If Not IsDBNull(drAct2("EVALPRO_RESPUESTA")) Then
                            For a = 2 To psCantEval
                                If a = 2 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(5).Visible = True : gwListaExportar.Rows(i).Cells(5).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 3 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(6).Visible = True : gwListaExportar.Rows(i).Cells(6).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 4 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(7).Visible = True : gwListaExportar.Rows(i).Cells(7).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 5 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(8).Visible = True : gwListaExportar.Rows(i).Cells(8).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 6 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(9).Visible = True : gwListaExportar.Rows(i).Cells(9).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 7 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(10).Visible = True : gwListaExportar.Rows(i).Cells(10).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 8 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(11).Visible = True : gwListaExportar.Rows(i).Cells(11).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 9 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(12).Visible = True : gwListaExportar.Rows(i).Cells(12).Text = (drAct2("EVALPRO_RESPUESTA"))
                                If a = 10 And Nz(drAct2("EVALPRO_NUMERO")) = a Then gwListaExportar.Columns(13).Visible = True : gwListaExportar.Rows(i).Cells(13).Text = (drAct2("EVALPRO_RESPUESTA"))

                            Next
                        End If

                    Next
                End If
            Next
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub Cancelar_Click(sender As Object, e As EventArgs) Handles Cancelar.Click
        lblCodProceso.Text = ""
        lblError.Text = ""
        lblEtiqueta.Text = ""
        lblCodEval.Text = ""
        lblEstado.Text = ""
        gwListaDetalle.DataSource = Nothing
        gwListaDetalle.DataBind()
        gwListaDetalle.Visible = False
        lblEtiqueta2.Visible = False
        txtResultado.Visible = False
        Cancelar.Visible = False
        LnkCancelar.Visible = False
        Exportar.Visible = False
        LnkGuardar.Visible = False
        GuardarRptas.Visible = False
        Cerrar.Visible = False
        GvPuntaje.Visible = False
        GvPuntaje.DataSource = Nothing
        GvPuntaje.DataBind()
    End Sub

    Private Sub CargarDatosFaltantes_Evaluacion()
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim pdCodDet As Double = 0
        Dim dtActividades As New DataTable
        Dim dt As New DataTable
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""
        Dim psCantEval As Integer = 0
        Try
            For i = 0 To gwListaDetalle.Rows.Count - 1
                dtActividades = ObjProceso.Ultima_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(Replace(gwListaDetalle.Rows(i).Cells(4).Text, "&nbsp;", "")), Nz(lblCodEval.Text), "N", Nz(lblCodOficina.Text))
                If dtActividades.Rows.Count > 0 Then
                    For Each drAct2 As DataRow In dtActividades.Rows
                        Dim ddlValor As DropDownList = gwListaDetalle.Rows(i).Cells(2).FindControl("cmbRpta")
                        Dim CmbPreg2 As DropDownList = gwListaDetalle.Rows(i).Cells(6).FindControl("cmbRpta2")
                        Dim CmbPreg3 As DropDownList = gwListaDetalle.Rows(i).Cells(7).FindControl("cmbRpta3")
                        Dim CmbPreg4 As DropDownList = gwListaDetalle.Rows(i).Cells(8).FindControl("cmbRpta4")
                        Dim CmbPreg5 As DropDownList = gwListaDetalle.Rows(i).Cells(9).FindControl("cmbRpta5")
                        Dim CmbPreg6 As DropDownList = gwListaDetalle.Rows(i).Cells(10).FindControl("cmbRpta6")
                        Dim CmbPreg7 As DropDownList = gwListaDetalle.Rows(i).Cells(11).FindControl("cmbRpta7")
                        Dim CmbPreg8 As DropDownList = gwListaDetalle.Rows(i).Cells(12).FindControl("cmbRpta8")
                        Dim CmbPreg9 As DropDownList = gwListaDetalle.Rows(i).Cells(13).FindControl("cmbRpta9")
                        Dim CmbPreg10 As DropDownList = gwListaDetalle.Rows(i).Cells(14).FindControl("cmbRpta10")
                        If ddlValor.Visible = True Then
                            If Not IsDBNull(drAct2("EVALPRO_RESPUESTA")) Then
                                dt = ObjProceso.Lista_ActividadesxTarea_CantEval(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(Replace(gwListaDetalle.Rows(i).Cells(4).Text, "&nbsp;", "")))
                                If dt.Rows.Count > 0 Then
                                    For Each dr As DataRow In dt.Rows
                                        psCantEval = Nz(dr("TAREA_CANTEVAL"))
                                    Next
                                End If
                                If Nz(drAct2("EVALPRO_NUMERO")) = 1 Then ddlValor.SelectedValue = (drAct2("EVALPRO_RESPUESTA"))
                                For a = 2 To psCantEval
                                    If a = 2 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg2.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 3 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg3.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 4 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg4.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 5 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg5.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 6 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg6.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 7 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg7.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 8 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg8.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 9 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg9.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                    If a = 10 And Nz(drAct2("EVALPRO_NUMERO")) = a Then CmbPreg10.SelectedValue = Nz(drAct2("EVALPRO_RESPUESTA"))
                                Next
                            End If
                        End If
                    Next
                End If
            Next
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub CargarDatosFaltantes_PlanAccion(ByVal psCodEval As String)
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim pdCodDet As Double = 0
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""
        Try
            Cn.Open() : cmdSql.Connection = Cn
            For i = 0 To gwListaPlanAccion.Rows.Count - 1
                cmdSql.CommandText = "SELECT EVALUACION_CODIGO, ACCION_CODIGO, ACCION_QUIEN " _
                               & " FROM TBEVALUACION_PROCESO_ACCION_DETALLE " _
                               & " WHERE EVALUACION_CODIGO = " & Nz(lblCodEval.Text) & " and " _
                               & " ACCION_PREGUNTA = " & gwListaPlanAccion.Rows(i).Cells(0).Text
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        Dim ddlAcc As DropDownList = gwListaPlanAccion.Rows(i).Cells(1).FindControl("ddlAccion")
                        Dim ddlQuien As DropDownList = gwListaPlanAccion.Rows(i).Cells(1).FindControl("ddlQuien")
                        ddlAcc.SelectedValue = Nz(Rs("ACCION_CODIGO"))
                        ddlQuien.SelectedValue = Nz(Rs("ACCION_QUIEN"))
                    End While
                End If
                Rs.Close()
            Next
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub GuardarRptas_Click(sender As Object, e As EventArgs) Handles GuardarRptas.Click
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim psCodPregunta As Double = 0
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim cmdSql3 As New SqlCommand
        Dim obj As New ClsEval_Proceso
        Dim dt As New Data.DataTable
        Dim psCantEval As Integer = 0
        Dim pdCodDet As Double = 0
        Dim psTipoAct As String = ""
        'If a = 0 Then lblError.Text = "Debe de marcar al menos una actividad."
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""

        Try
            Cn2.Open() : cmdSql.Connection = Cn2
            Cn.Open() : cmdSql2.Connection = Cn
            Cn3.Open() : cmdSql3.Connection = Cn3
            cmdSql.CommandText = "SELECT MAX(EVALUACION_CODIGO) FROM TBEVALUACION_PROCESOS_DETALLE"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdCodDet = Nz(Rs(0)) + 1
                End While
            Else
                pdCodDet = 1
            End If
            Dim psValor As String = ""
            Rs.Close()
            cmdSql3.CommandText = " delete from TBEVALUACION_PROCESOS_DETALLE where EVALUACION_CODIGO =  " & lblCodEval.Text
            cmdSql3.ExecuteNonQuery()
            For i = 0 To gwListaDetalle.Rows.Count - 1
                Dim CmbPreg As DropDownList = gwListaDetalle.Rows(i).Cells(2).FindControl("cmbRpta")
                Dim CmbPreg2 As DropDownList = gwListaDetalle.Rows(i).Cells(6).FindControl("cmbRpta2")
                Dim CmbPreg3 As DropDownList = gwListaDetalle.Rows(i).Cells(7).FindControl("cmbRpta3")
                Dim CmbPreg4 As DropDownList = gwListaDetalle.Rows(i).Cells(8).FindControl("cmbRpta4")
                Dim CmbPreg5 As DropDownList = gwListaDetalle.Rows(i).Cells(9).FindControl("cmbRpta5")
                Dim CmbPreg6 As DropDownList = gwListaDetalle.Rows(i).Cells(10).FindControl("cmbRpta6")
                Dim CmbPreg7 As DropDownList = gwListaDetalle.Rows(i).Cells(11).FindControl("cmbRpta7")
                Dim CmbPreg8 As DropDownList = gwListaDetalle.Rows(i).Cells(12).FindControl("cmbRpta8")
                Dim CmbPreg9 As DropDownList = gwListaDetalle.Rows(i).Cells(13).FindControl("cmbRpta9")
                Dim CmbPreg10 As DropDownList = gwListaDetalle.Rows(i).Cells(14).FindControl("cmbRpta10")
                Dim txtObs As TextBox = gwListaDetalle.Rows(i).Cells(3).FindControl("txtObs")
                psCodPregunta = gwListaDetalle.Rows(i).Cells(4).Text
                'PRIMERA EVALUACION
                psTipoAct = Replace(gwListaDetalle.Rows(i).Cells(5).Text, "&nbsp;", "")
                'PRIMERA EVALUACION
                If psTipoAct = "" Then psTipoAct = "NULL"
                If CmbPreg.Enabled = False And psTipoAct = "A" Then
                    cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESOS_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, EVALPRO_CODIGO, EVALPRO_PROCESO, EVALPRO_PREGUNTA, EVALPRO_RESPUESTA, EVALPRO_OBSERVACION, EVALPRO_SYS_EST,EVALPRO_NUMERO) " _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdCodDet & ", " & lblCodProceso.Text & ", " & psCodPregunta & ", '" & CmbPreg.Text & "', '" & txtObs.Text & "', '0',1)"
                    cmdSql2.ExecuteNonQuery()
                End If
                If CmbPreg.Visible = True And CmbPreg.Enabled = True Then
                    dt = ObjProceso.Lista_ActividadesxTarea_CantEval(Session("CodEmpresa"), Session("Ruta_Emp"), gwListaDetalle.Rows(i).Cells(4).Text)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            psCantEval = Nz(dr("TAREA_CANTEVAL"))
                        Next
                    End If
                    If psTipoAct = "T" Then
                        cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESOS_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, EVALPRO_CODIGO, EVALPRO_PROCESO, EVALPRO_PREGUNTA, EVALPRO_RESPUESTA, EVALPRO_OBSERVACION, EVALPRO_SYS_EST,EVALPRO_NUMERO) " _
                                       & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdCodDet & ", " & lblCodProceso.Text & ", " & psCodPregunta & ", '" & CmbPreg.Text & "', '" & txtObs.Text & "', '0',1)"
                        cmdSql2.ExecuteNonQuery()
                    Else
                        cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESOS_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, EVALPRO_CODIGO, EVALPRO_PROCESO, EVALPRO_PREGUNTA, EVALPRO_RESPUESTA, EVALPRO_OBSERVACION, EVALPRO_SYS_EST,EVALPRO_NUMERO,EVALPRO_TIPO) " _
                                       & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdCodDet & ", " & lblCodProceso.Text & ", " & psCodPregunta & ", '" & CmbPreg.Text & "', '" & txtObs.Text & "', '0',1," & psTipoAct & ")"
                        cmdSql2.ExecuteNonQuery()
                    End If
                    For a = 2 To psCantEval
                        psValor = ""
                        If a = 2 Then psValor = CmbPreg2.Text
                        If a = 3 Then psValor = CmbPreg3.Text
                        If a = 4 Then psValor = CmbPreg4.Text
                        If a = 5 Then psValor = CmbPreg5.Text
                        If a = 6 Then psValor = CmbPreg6.Text
                        If a = 7 Then psValor = CmbPreg7.Text
                        If a = 8 Then psValor = CmbPreg8.Text
                        If a = 9 Then psValor = CmbPreg9.Text
                        If a = 10 Then psValor = CmbPreg10.Text
                        cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESOS_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, EVALPRO_CODIGO, EVALPRO_PROCESO, EVALPRO_PREGUNTA, EVALPRO_RESPUESTA, EVALPRO_OBSERVACION, EVALPRO_SYS_EST,EVALPRO_NUMERO) " _
                                           & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdCodDet & ", " & lblCodProceso.Text & ", " & psCodPregunta & ",'" & psValor & "', '" & txtObs.Text & "', '0'," & a & ")"
                        cmdSql2.ExecuteNonQuery()
                    Next
                    obj.Evaluacion_UpdEstado(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(lblCodEval.Text), "2")
                End If
            Next
            'Lista_ActividadesxTarea_xCodEval

            dt = Nothing
            Dim psCodAct As Double = 0
            Dim dtActP As New DataTable
            Dim pdCant As Integer = 0
            Dim pdCant1 As Integer = 0
            Dim psResultado As String = "0"
            dt = ObjProceso.Lista_ActividadesxTarea_xCodEval(Session("CodEmpresa"), Session("Ruta_Emp"), lblCodEval.Text)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    pdCant1 = 0
                    pdCant = 0
                    psResultado = "0"
                    psCodAct = Nz(dr("EVALPRO_TIPO"))
                    dtActP = ObjProceso.ResultadoTotalxAct_xPregunta(Session("CodEmpresa"), Session("Ruta_Emp"), lblCodEval.Text, psCodAct)
                    If dtActP.Rows.Count > 0 Then
                        For Each draa As DataRow In dtActP.Rows
                            If Nu(draa("EVALPRO_RESPUESTA")) = "1" Then pdCant1 = pdCant1 + 1 Else pdCant = pdCant + 1
                        Next
                        If pdCant = 0 Then psResultado = "1"
                        cmdSql3.CommandText = " UPDATE TBEVALUACION_PROCESOS_DETALLE SET EVALPRO_RESPUESTA = '" & psResultado & "'" _
                                            & " WHERE EVALUACION_CODIGO = " & lblCodEval.Text & " AND EVALPRO_PREGUNTA = " & psCodAct
                        cmdSql3.ExecuteNonQuery()
                    Else
                        cmdSql3.CommandText = " UPDATE TBEVALUACION_PROCESOS_DETALLE SET EVALPRO_RESPUESTA = '0'" _
                                            & " WHERE EVALUACION_CODIGO = " & lblCodEval.Text & " AND EVALPRO_PREGUNTA = " & psCodAct
                        cmdSql3.ExecuteNonQuery()
                    End If
                    dtActP = Nothing
                Next
            End If
            Cn.Close()
            Cn2.Close()
            Cn3.Close()
            lblCodProceso.Text = ""
            lblError.Text = ""
            lblEtiqueta.Text = ""
            lblCodEval.Text = ""
            lblEstado.Text = ""
            txtResultado.Visible = False
            txtResultado.ReadOnly = False
            lblEtiqueta2.Visible = False
            gwListaDetalle.DataSource = Nothing
            gwListaDetalle.DataBind()
            gwListaDetalle.Visible = False
            GuardarRptas.Visible = False
            Cancelar.Visible = False
            Cerrar.Visible = False
            Cancelar_Click(sender, e)
            Call Listar_Proceso()
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try

    End Sub
    Function Calcular_Promedio_Conexion(ByVal psCodProceso As Double, ByVal psCodeval As Double) As Double
        Dim psCanteval As Integer = 0
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim pdPromedioxEval As Double = 0
        Dim pdPromedioxEvalTotal As Double = 0
        Dim pdPromedioFinal As Double = 0
        Calcular_Promedio_Conexion = 0
        Try
            dt = ObjProceso.Lista_Tareas(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    psCanteval = Nz(dr("TAREA_CANTEVAL"))
                Next
            End If
            dt = Nothing
            For i = 1 To psCanteval
                pdPromedioxEval = 0
                dt = ObjProceso.Result_EvaluacionxNumEval(Session("CodEmpresa"), Session("Ruta_Emp"), psCodeval, i)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If dr(0).ToString = 0 Then
                            pdPromedioxEval = 0
                            Exit For
                        Else
                            pdPromedioxEval = 1
                        End If
                    Next
                End If
                pdPromedioxEvalTotal = pdPromedioxEvalTotal + pdPromedioxEval
            Next

            pdPromedioFinal = (pdPromedioxEvalTotal / psCanteval) * 100
            Calcular_Promedio_Conexion = pdPromedioFinal
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Function

    Private Sub gwListaPlanAccion_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gwListaPlanAccion.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim combo As DropDownList = DirectCast(e.Row.FindControl("ddlQuien"), DropDownList)
                combo.ClearSelection()
                If combo IsNot DBNull.Value Then
                    Cargar_Responsable(combo)
                End If
                Dim ddlAcc As DropDownList = DirectCast(e.Row.FindControl("ddlAccion"), DropDownList)
                ddlAcc.ClearSelection()
                If ddlAcc IsNot DBNull.Value Then
                    Call LlenaComboItem("TBOPC531", ddlAcc)
                End If
            End If
            Me.Page.Session.Timeout = 1080
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Cargar_Responsable(ByVal combo As DropDownList)

        Dim objSeg As New ModuloSeguridad
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = objSeg.Listar_Usuarios()
        combo.DataTextField = "nombre"
        combo.DataValueField = "codigo"
        combo.DataBind()
        combo.Items.Add("< Todos >")
        combo.SelectedValue = " "

    End Sub
    Protected Sub LnkCancelar_Click(sender As Object, e As EventArgs) Handles LnkCancelar.Click
        lblEtiqueta.Text = ""
        lblEtiqueta3.Text = ""
        gwListaDetallePlan.Visible = False
        gwListaPlanAccion.Visible = False
        Cancelar.Visible = False
        LnkCancelar.Visible = False
        LnkGuardar.Visible = False
        GuardarRptas.Visible = False
        Cerrar.Visible = False
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub gwListaPlanAccion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gwListaPlanAccion.RowCommand

        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim psCodPregunta As Double = 0
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim cmdSql3 As New SqlCommand
        Dim obj As New Listados
        Dim dt As New Data.DataTable
        Dim pdCodDet As Double = 0
        'If a = 0 Then lblError.Text = "Debe de marcar al menos una actividad."
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            If e.CommandName = "Guardar" Then
                Cn.Open() : cmdSql2.Connection = Cn
                Cn2.Open() : cmdSql.Connection = Cn2
                Cn3.Open() : cmdSql3.Connection = Cn3
                cmdSql.CommandText = " SELECT EVALUACION_CODIGO, ACCION_PREGUNTA " _
                                   & " FROM TBEVALUACION_PROCESO_ACCION_DETALLE " _
                                   & " WHERE EVALUACION_CODIGO = " & Nz(lblCodEval.Text) & " and " _
                                   & " ACCION_PREGUNTA = " & gwListaPlanAccion.Rows(Index).Cells(0).Text
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        Dim ddlAcc As DropDownList = gwListaPlanAccion.Rows(Index).Cells(1).FindControl("ddlAccion")
                        Dim txtAcc As TextBox = gwListaPlanAccion.Rows(Index).Cells(2).FindControl("txtAccion")
                        Dim ddlQuien As DropDownList = gwListaPlanAccion.Rows(Index).Cells(3).FindControl("ddlQuien")
                        Dim txtFecha As TextBox = gwListaPlanAccion.Rows(Index).Cells(4).FindControl("txtFecha")
                        Dim ddlEstado As DropDownList = gwListaPlanAccion.Rows(Index).Cells(5).FindControl("ddlEstado")
                        txtFecha.Text = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
                        cmdSql2.CommandText = " UPDATE TBEVALUACION_PROCESO_ACCION_DETALLE SET ACCION_CODIGO = '" & ddlAcc.SelectedValue & "', " _
                                            & " ACCION_DESCRIPCION = '" & txtAcc.Text & "', ACCION_QUIEN ='" & ddlQuien.SelectedValue & "', " _
                                            & " ACCION_CUANDO = '" & txtFecha.Text & "', ACCION_ESTADO = '" & ddlEstado.SelectedValue & "' " _
                                            & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                                            & " AND EVALUACION_CODIGO = " & lblCodEval.Text & " AND  ACCION_PREGUNTA = " & gwListaPlanAccion.Rows(Index).Cells(0).Text
                        cmdSql2.ExecuteNonQuery()

                    End While
                Else
                    Dim ddlAcc As DropDownList = gwListaPlanAccion.Rows(Index).Cells(1).FindControl("ddlAccion")
                    Dim txtAcc As TextBox = gwListaPlanAccion.Rows(Index).Cells(2).FindControl("txtAccion")
                    Dim ddlQuien As DropDownList = gwListaPlanAccion.Rows(Index).Cells(3).FindControl("ddlQuien")
                    Dim txtFecha As TextBox = gwListaPlanAccion.Rows(Index).Cells(4).FindControl("txtFecha")
                    Dim ddlEstado As DropDownList = gwListaPlanAccion.Rows(Index).Cells(5).FindControl("ddlEstado")
                    txtFecha.Text = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
                    cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESO_ACCION_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, " _
                                                & " ACCION_PREGUNTA, ACCION_CODIGO, ACCION_DESCRIPCION, ACCION_QUIEN, ACCION_CUANDO, " _
                                                & " ACCION_ESTADO) " _
                                                & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " _
                                                & " " & gwListaPlanAccion.Rows(Index).Cells(0).Text & ", '" & ddlAcc.SelectedValue & "', " _
                                                & " '" & txtAcc.Text & "', '" & ddlQuien.SelectedValue & "', '" & txtFecha.Text & "', " _
                                                & " '" & ddlEstado.SelectedValue & "')"
                    cmdSql2.ExecuteNonQuery()
                End If
                Rs.Close()
                Call ActualizarEstado_Plan()
            End If
            Call Lista_PlanAccion(Nz(lblCodEval.Text))
            Call Listar_Proceso()
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub ActualizarEstado_Plan()
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim Rs As SqlDataReader
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql2 As New SqlCommand
        Dim dt As New DataTable
        Dim pdCodDet As Double = 0
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""
        Try
            Cn.Open() : cmdSql.Connection = Cn
            Cn2.Open() : cmdSql2.Connection = Cn2
            For i = 0 To gwListaPlanAccion.Rows.Count - 1
                cmdSql.CommandText = "SELECT EVALUACION_CODIGO, ACCION_CODIGO, ACCION_QUIEN " _
                               & " FROM TBEVALUACION_PROCESO_ACCION_DETALLE " _
                               & " WHERE ACCION_ESTADO <> '3' AND EVALUACION_CODIGO = " & Nz(lblCodEval.Text)
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        cmdSql2.CommandText = "update TBEVALUACION_PROCESO_ACCION set planacc_estado='2' where evaluacion_codigo = " & Nz(lblCodEval.Text)
                        cmdSql2.ExecuteNonQuery()
                    End While
                Else
                    cmdSql2.CommandText = "update TBEVALUACION_PROCESO_ACCION set planacc_estado='3' where evaluacion_codigo = " & Nz(lblCodEval.Text)
                    cmdSql2.ExecuteNonQuery()
                End If
                Rs.Close()
            Next
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Cerrar_Click(sender As Object, e As EventArgs) Handles Cerrar.Click
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim psCodPregunta As Double = 0
        Dim psCodAccion As Double = 0
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn4 As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim cmdSql2 As New SqlCommand
        Dim cmdSql3 As New SqlCommand
        Dim cmdSql4 As New SqlCommand
        Dim obj As New ClsEval_Proceso
        Dim dt As New Data.DataTable
        Dim dtListado As New DataTable
        Dim pdCodDet As Double = 0
        'If a = 0 Then lblError.Text = "Debe de marcar al menos una actividad."
        If lblError.Text <> "" Then
            Exit Sub
        End If
        lblError.Text = ""
        Try
            Cn.Open() : cmdSql.Connection = Cn
            Cn2.Open() : cmdSql2.Connection = Cn2
            Cn3.Open() : cmdSql3.Connection = Cn3
            Cn4.Open() : cmdSql3.Connection = Cn4
            cmdSql.CommandText = "SELECT MAX(EVALUACION_CODIGO) FROM TBEVALUACION_PROCESOS_DETALLE"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdCodDet = Nz(Rs(0)) + 1
                End While
            Else
                pdCodDet = 1
            End If
            Rs.Close()
            cmdSql.CommandText = "SELECT MAX(PLANACC_CODIGO) FROM TBEVALUACION_PROCESO_ACCION"
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodAccion = Nz(Rs(0)) + 1
                End While
            Else
                psCodAccion = 1
            End If
            Rs.Close()
            cmdSql3.CommandText = " delete from TBEVALUACION_PROCESO_ACCION where EVALUACION_CODIGO =  " & lblCodEval.Text
            cmdSql3.ExecuteNonQuery()
            cmdSql3.CommandText = " delete from TBEVALUACION_PROCESO_PROMEDIO_DETALLE where EVALUACION_CODIGO =  " & lblCodEval.Text
            cmdSql3.ExecuteNonQuery()
            cmdSql3.CommandText = " delete from TBEVALUACION_PROCESOS_DETALLE where EVALUACION_CODIGO =  " & lblCodEval.Text
            cmdSql3.ExecuteNonQuery()
            cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESO_ACCION ( EMPRESA_CODIGO, EVALUACION_CODIGO, PLANACC_CODIGO, PLANACC_FECHA, PLANACC_HORA, PLANACC_USER, PLANACC_ESTADO, PLANACC_SYS_EST) " _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & "," & psCodAccion & ", '" & FechaActual() & "', '" & HoraActual() & "', '" & Session("USER") & "', '1', '0')"
            cmdSql2.ExecuteNonQuery()
            Dim psTipoAct As String = ""
            For i = 0 To gwListaDetalle.Rows.Count - 1
                Dim CmbPreg As DropDownList = gwListaDetalle.Rows(i).Cells(2).FindControl("cmbRpta")
                Dim txtObs As TextBox = gwListaDetalle.Rows(i).Cells(3).FindControl("txtObs")
                psCodPregunta = gwListaDetalle.Rows(i).Cells(4).Text

                psTipoAct = Replace(gwListaDetalle.Rows(i).Cells(5).Text, "&nbsp;", "")
                'PRIMERA EVALUACION
                If psTipoAct = "" Then psTipoAct = "NULL"

                If CmbPreg.Enabled = False And psTipoAct = "A" Then
                    cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESOS_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, EVALPRO_CODIGO, EVALPRO_PROCESO, EVALPRO_PREGUNTA, EVALPRO_RESPUESTA, EVALPRO_OBSERVACION, EVALPRO_SYS_EST,EVALPRO_NUMERO) " _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdCodDet & ", " & lblCodProceso.Text & ", " & psCodPregunta & ", '" & CmbPreg.Text & "', '" & txtObs.Text & "', '0',1)"
                    cmdSql2.ExecuteNonQuery()
                End If
                If CmbPreg.Enabled = True And CmbPreg.Visible = True Then
                    If psTipoAct = "T" Then
                        cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESOS_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, EVALPRO_CODIGO, EVALPRO_PROCESO, EVALPRO_PREGUNTA, EVALPRO_RESPUESTA, EVALPRO_OBSERVACION, EVALPRO_SYS_EST,EVALPRO_NUMERO) " _
                                       & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdCodDet & ", " & lblCodProceso.Text & ", " & psCodPregunta & ", '" & CmbPreg.Text & "', '" & txtObs.Text & "', '0',1)"
                        cmdSql2.ExecuteNonQuery()
                    Else
                        cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESOS_DETALLE ( EMPRESA_CODIGO, EVALUACION_CODIGO, EVALPRO_CODIGO, EVALPRO_PROCESO, EVALPRO_PREGUNTA, EVALPRO_RESPUESTA, EVALPRO_OBSERVACION, EVALPRO_SYS_EST,EVALPRO_NUMERO,EVALPRO_TIPO) " _
                                       & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdCodDet & ", " & lblCodProceso.Text & ", " & psCodPregunta & ", '" & CmbPreg.Text & "', '" & txtObs.Text & "', '0',1," & psTipoAct & ")"
                        cmdSql2.ExecuteNonQuery()
                    End If
                End If
                If CmbPreg.Text = "0" Then
                    cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESO_ACCION_DETALLE (EMPRESA_CODIGO, EVALUACION_CODIGO, PLANACC_CODIGO, ACCION_PREGUNTA, ACCION_ESTADO) " _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & psCodAccion & ", " & psCodPregunta & ", '1')"
                    cmdSql2.ExecuteNonQuery()
                End If
            Next


            dt = Nothing
            Dim psCodAct As Double = 0
            Dim dtActP As New DataTable
            Dim pdCant As Integer = 0
            Dim pdCant1 As Integer = 0
            Dim psResultado As String = "0"
            dt = ObjProceso.Lista_ActividadesxTarea_xCodEval(Session("CodEmpresa"), Session("Ruta_Emp"), lblCodEval.Text)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    pdCant1 = 0
                    pdCant = 0
                    psResultado = "0"
                    psCodAct = Nz(dr("EVALPRO_TIPO"))
                    dtActP = ObjProceso.ResultadoTotalxAct_xPregunta(Session("CodEmpresa"), Session("Ruta_Emp"), lblCodEval.Text, psCodAct)
                    If dtActP.Rows.Count > 0 Then
                        For Each draa As DataRow In dtActP.Rows
                            If Nu(draa("EVALPRO_RESPUESTA")) = "1" Then pdCant1 = pdCant1 + 1 Else pdCant = pdCant + 1
                        Next
                        If pdCant = 0 Then psResultado = "1"
                        cmdSql3.CommandText = " UPDATE TBEVALUACION_PROCESOS_DETALLE SET EVALPRO_RESPUESTA = '" & psResultado & "'" _
                                            & " WHERE EVALUACION_CODIGO = " & lblCodEval.Text & " AND EVALPRO_PREGUNTA = " & psCodAct
                        cmdSql3.ExecuteNonQuery()
                    Else
                        cmdSql3.CommandText = " UPDATE TBEVALUACION_PROCESOS_DETALLE SET EVALPRO_RESPUESTA = '0'" _
                                            & " WHERE EVALUACION_CODIGO = " & lblCodEval.Text & " AND EVALPRO_PREGUNTA = " & psCodAct
                        cmdSql3.ExecuteNonQuery()
                    End If
                    dtActP = Nothing
                Next
            End If

            dtListado = obj.Evaluacion_Resultado(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(lblCodEval.Text), 218, lblCodProceso.Text)
            Dim pdPromedioEval As Double = 0
            If dtListado.Rows.Count > 0 Then
                For Each drResul As DataRow In dtListado.Rows
                    If Not IsDBNull(drResul(0)) Then
                        pdPromedioEval = drResul(0)
                        cmdSql2.CommandText = " INSERT INTO TBEVALUACION_PROCESO_PROMEDIO_DETALLE (EMPRESA_CODIGO, EVALUACION_CODIGO, EVALUACION_PROMEDIO,EVALUACION_PROCESO) " _
                                   & " VALUES ('" & Session("CodEmpresa") & "', " & lblCodEval.Text & ", " & pdPromedioEval & ", " & Nz(lblCodProceso.Text) & ")"
                        cmdSql2.ExecuteNonQuery()
                    End If
                Next
            End If
            obj.Evaluacion_UpdEstado(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(lblCodEval.Text), "3")

            obj.Lista_PromedioFinalOficina(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(lblCodOficina.Text), Nz(lblCodProceso.Text))
            lblCodProceso.Text = ""
            lblError.Text = ""
            lblEtiqueta.Text = ""
            lblCodEval.Text = ""
            lblEstado.Text = ""
            txtResultado.Visible = False
            txtResultado.ReadOnly = False
            lblEtiqueta2.Visible = False
            gwListaDetalle.DataSource = Nothing
            gwListaDetalle.DataBind()
            gwListaDetalle.Visible = False
            GuardarRptas.Visible = False
            Cancelar.Visible = False
            Cerrar.Visible = False
            Call Listar_Proceso()
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub gwListaDetalle_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gwListaDetalle.RowDataBound
        Dim dt As New DataTable
        Dim psCantEval As Integer = 0
        Dim Colum As Integer = 0
        Dim i As Integer = 0
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim combo As DropDownList = DirectCast(e.Row.FindControl("cmbRpta"), DropDownList)
                Dim CmbPreg2 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta2"), DropDownList)
                Dim CmbPreg3 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta3"), DropDownList)
                Dim CmbPreg4 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta4"), DropDownList)
                Dim CmbPreg5 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta5"), DropDownList)
                Dim CmbPreg6 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta6"), DropDownList)
                Dim CmbPreg7 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta7"), DropDownList)
                Dim CmbPreg8 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta8"), DropDownList)
                Dim CmbPreg9 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta9"), DropDownList)
                Dim CmbPreg10 As DropDownList = DirectCast(e.Row.FindControl("cmbRpta10"), DropDownList)
                'Dim btnGuardar As Button = DirectCast(e.Row.FindControl("btnGuardar"), Button)
                Dim pdCodPregunta As Double = CType(e.Row.Cells(4).Text, Double)
                Dim psTipoAct As String = CType(e.Row.Cells(5).Text, String)
                If combo IsNot DBNull.Value Then
                    combo.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(combo, pdCodPregunta) Else Cargar_Rpta(combo, pdCodPregunta)
                    CmbPreg2.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg2, pdCodPregunta) Else Cargar_Rpta(CmbPreg2, pdCodPregunta)
                    CmbPreg3.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg3, pdCodPregunta) Else Cargar_Rpta(CmbPreg3, pdCodPregunta)
                    CmbPreg4.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg4, pdCodPregunta) Else Cargar_Rpta(CmbPreg4, pdCodPregunta)
                    CmbPreg5.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg5, pdCodPregunta) Else Cargar_Rpta(CmbPreg5, pdCodPregunta)
                    CmbPreg6.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg6, pdCodPregunta) Else Cargar_Rpta(CmbPreg6, pdCodPregunta)
                    CmbPreg7.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg7, pdCodPregunta) Else Cargar_Rpta(CmbPreg7, pdCodPregunta)
                    CmbPreg8.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg8, pdCodPregunta) Else Cargar_Rpta(CmbPreg8, pdCodPregunta)
                    CmbPreg9.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg9, pdCodPregunta) Else Cargar_Rpta(CmbPreg9, pdCodPregunta)
                    CmbPreg10.ClearSelection()
                    If psTipoAct <> "A" And psTipoAct <> "F" And psTipoAct <> "T" Then Cargar_Rpta_xActividad(CmbPreg10, pdCodPregunta) Else Cargar_Rpta(CmbPreg10, pdCodPregunta)
                End If
            End If

            Me.Page.Session.Timeout = 1080
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Cargar_Rpta(ByVal combo As DropDownList, ByVal psCodPregunta As Double)

        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = ObjProceso.Evaluacion_Puntaje(Session("CodEmpresa"), Session("Ruta_Emp"), psCodPregunta)
        combo.DataTextField = "PUNTAJE"
        combo.DataValueField = "PUNTAJE"
        combo.DataBind()
        combo.Items.Add("No Aplica")
        combo.SelectedValue = "No Aplica"
        combo.Items.Add(" ")
        combo.SelectedValue = " "

    End Sub
    'Evaluacion_PuntajexActividad
    Private Sub Cargar_Rpta_xActividad(ByVal combo As DropDownList, ByVal psCodPregunta As Double)

        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = ObjProceso.Evaluacion_PuntajexActividad(Session("CodEmpresa"), Session("Ruta_Emp"), psCodPregunta)
        combo.DataTextField = "PUNTAJE"
        combo.DataValueField = "PUNTAJE"
        combo.DataBind()
        combo.Items.Add("No Aplica")
        combo.SelectedValue = "No Aplica"
        combo.Items.Add(" ")
        combo.SelectedValue = " "

    End Sub
    Protected Sub DdlBusResponsable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlBusResponsable.SelectedIndexChanged
        BtnListar_Click(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub DdlProceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProceso.SelectedIndexChanged
        BtnListar_Click(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Exportar_Click(sender As Object, e As EventArgs) Handles Exportar.Click

        Dim sb As New StringBuilder()
        Dim sw As New StringWriter(sb)
        Dim htw As New HtmlTextWriter(sw)

        Dim page As New Page()
        Dim form As New HtmlForm()

        gwListaExportar.EnableViewState = False

        ' Deshabilitar la validación de eventos, sólo asp.net 2 
        page.EnableEventValidation = False

        ' Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD. 
        page.DesignerInitialize()

        page.Controls.Add(form)
        form.Controls.Add(gwListaExportar)

        page.RenderControl(htw)

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.[Default]
        Response.Write(sb.ToString())
        Response.[End]()
        Me.Page.Session.Timeout = 1080

    End Sub
    Protected Sub ddlEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEstado.SelectedIndexChanged
        BtnListar_Click(sender, e)
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub DdlAño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub gwListaDetalle_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gwListaDetalle.SelectedIndexChanged

    End Sub
End Class
