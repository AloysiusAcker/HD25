Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_LisaRptaJalados
    Inherits System.Web.UI.Page
    Dim ObjProceso As New ClsEval_Proceso
    Dim FnProceso As New clsEval_Proceso_Funciones
    Dim objGrupoEmp As New ModuloGeneral
    Dim objSeg As New ModuloSeguridad
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            Call Cargar_RM(DdlRM, 9)
            Call Cargar_RM(DdlDM, 10)
            Call Lista_Oficinatodo(DdlTienda)
            Call FnProceso.Llenar_Proceso(DdlProceso, Session("CodEmpresa"), Session("Ruta_Emp"))
            DdlProceso.SelectedValue = "< Seleccionar >"
            DdlAño.Items.Clear()
            Call LlenaAno(DdlAño)
            DdlAño.SelectedValue = CInt(Left(FechaActual, 4))
            DdlAño.Focus()
            DdlMes.Items.Clear()
            Call LlenaMes(DdlMes, True)
            DdlMes.SelectedValue = CInt(Mid(FechaActual, 5, 2))
            DdlMesFin.Items.Clear()
            Call LlenaMes(DdlMesFin, True)
            DdlMesFin.SelectedValue = CInt(Mid(FechaActual, 5, 2))
            DdlTop.Items.Clear()
            DdlProceso.SelectedValue = "7"
            Dim i As Integer
            For i = 1 To 20
                Dim Item As New ListItem
                Item.Text = i
                Item.Value = i
                DdlTop.Items.Add(Item)
            Next
            DdlTop.SelectedValue = 5
            DdlProceso_SelectedIndexChanged(sender, e)
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Private Sub Lista_Oficinatodo(ByVal ddl As DropDownList)
        ddl.Items.Clear()
        ddl.DataSource = objSeg.Listar_Oficina(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
        ddl.DataTextField = "OFICINA_NOMBRE"
        ddl.DataValueField = "OFICINA_CODIGO"
        ddl.DataBind()
        ddl.Items.Add("< Total Sistema >")
        ddl.SelectedValue = "< Total Sistema >"
    End Sub
    Private Sub Cargar_RM(ByVal ddl As DropDownList, ByVal psCodCargo As Double)
        ddl.Items.Clear() 'Listar_Usuarios
        ddl.DataSource = objGrupoEmp.Lista_Personal_xCargo(Session("CodGrupoEmpresa"), Session("CodEmpresa"), psCodCargo)
        ddl.DataTextField = "NOMBRE_PERSONAL"
        ddl.DataValueField = "PERSON_CODIGO"
        ddl.DataBind()
        ddl.Items.Add("< Total Sistema >")
        ddl.SelectedValue = "< Total Sistema >"
    End Sub
    Private Sub Lista_Tareas(ByVal psCodProceso As Double)
        lblError.Text = ""
        Dim dt As New DataTable
        Dim dtTarea As New DataTable
        Dim dtActividades As New DataTable
        Dim dtListado As New DataTable
        Dim i As Integer = 0
        Dim ii As Integer = 0
        Dim drT As DataRow
        Dim psDm As String = ""
        Dim cmdGlobal As New SqlCommand
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        dtListado.Columns.Add("c1")
        dtListado.Columns.Add("c2")
        Dim pdCodTienda As Double = 0
        If DdlTienda.SelectedValue <> "< Total Sistema >" Then pdCodTienda = DdlTienda.SelectedValue

        Dim psMes As String = ""
        Dim psMesFin As String = ""

        psMes = Llenar_Ceros(DdlMes.SelectedValue, 2)
        psMesFin = Llenar_Ceros(DdlMesFin.SelectedValue, 2)

        Dim pdTop As Double = 0
        pdTop = Nz(DdlTop.SelectedValue)

        cn.Open()
        cmdGlobal.Connection = cn
        If Existe_Tabla("V_ListaTop_RptaJalados", Session("Ruta_Emp")) = False Then
            cmdGlobal.CommandText = " CREATE TABLE V_ListaTop_RptaJalados (canteval FLOAT, EVALUACION_PROCESO FLOAT,PROCESO_NOMBRE VARCHAR(250), " _
                                  & " EVALUACION_OFICINA FLOAT ,OFICINA_NOMBRE VARCHAR(250), TAREA_CODIGO FLOAT ,TAREA_NOMBRE VARCHAR(250), " _
                                  & " EVALPRO_PREGUNTA FLOAT ,PREGUNTA_NOMBRE VARCHAR(5000), tarea_orden float, tareadet_orden float) "
            cmdGlobal.ExecuteNonQuery()
        End If


        cmdGlobal.CommandText = " DELETE FROM V_ListaTop_RptaJalados "
        cmdGlobal.ExecuteNonQuery()

        If DdlRM.SelectedValue = "< Total Sistema >" And DdlDM.SelectedValue = "< Total Sistema >" Then
            dt = ObjProceso.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), "")
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If psDm <> "" Then psDm = psDm & ","
                    psDm = psDm & dr("c4")
                Next
            End If
            dt = Nothing
        ElseIf DdlRM.SelectedValue <> "< Total Sistema >" And DdlDM.SelectedValue = "< Total Sistema >" Then
            dt = ObjProceso.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), DdlRM.SelectedValue)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If psDm <> "" Then psDm = psDm & ","
                    psDm = psDm & dr("c4")
                Next
            End If
            dt = Nothing
        ElseIf DdlRM.SelectedValue <> "< Total Sistema >" And DdlDM.SelectedValue <> "< Total Sistema >" Then
            psDm = DdlDM.SelectedValue
        ElseIf DdlRM.SelectedValue = "< Total Sistema >" And DdlDM.SelectedValue <> "< Total Sistema >" Then
            psDm = DdlDM.SelectedValue
        End If



        dt = ObjProceso.EvalLista_RptaJalados_xOficina(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso, DdlAño.Text, DdlAño.Text + psMes, DdlAño.Text + psMesFin, pdCodTienda, psDm)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                cmdGlobal.CommandText = " INSERT INTO V_ListaTop_RptaJalados (canteval , EVALUACION_PROCESO ,PROCESO_NOMBRE, " _
                                        & " EVALUACION_OFICINA  ,OFICINA_NOMBRE , TAREA_CODIGO ,TAREA_NOMBRE, EVALPRO_PREGUNTA ,PREGUNTA_NOMBRE,tarea_orden , tareadet_orden ) " _
                                        & " VALUES ( " & Nz(dr("canteval")) & " , " & Nz(dr("EVALUACION_PROCESO")) & " ,'" & Nu(dr("PROCESO_NOMBRE")) & "', " _
                                        & " " & Nz(dr("EVALUACION_OFICINA")) & ", '" & Nu(dr("OFICINA_NOMBRE")) & "', " & Nz(dr("TAREA_CODIGO")) & " ,'" & Nu(dr("TAREA_NOMBRE")) & "', " _
                                        & " " & Nz(dr("EVALPRO_PREGUNTA")) & ", '" & Nu(dr("TAREADET_NOMBRE")) & "'," & Nz(dr("tarea_orden")) & ", " & Nz(dr("tareadet_orden")) & " )"
                cmdGlobal.ExecuteNonQuery()
            Next
        End If
        dt = Nothing


        If Existe_Tabla("V_ListaTop_RptaJalados_top", Session("Ruta_Emp")) = False Then
            cmdGlobal.CommandText = " CREATE TABLE V_ListaTop_RptaJalados_top (num_reg float, canteval FLOAT, EVALUACION_PROCESO FLOAT,PROCESO_NOMBRE VARCHAR(250), " _
                                  & " EVALUACION_OFICINA FLOAT ,OFICINA_NOMBRE VARCHAR(250), TAREA_CODIGO FLOAT ,TAREA_NOMBRE VARCHAR(250), " _
                                  & " EVALPRO_PREGUNTA FLOAT ,PREGUNTA_NOMBRE VARCHAR(5000), tarea_orden float, tareadet_orden float) "
            cmdGlobal.ExecuteNonQuery()
        End If

        cmdGlobal.CommandText = " DELETE FROM V_ListaTop_RptaJalados_top  "
        cmdGlobal.ExecuteNonQuery()
        i = 0

        If pdTop > 0 Then
            dt = ObjProceso.EvalProcesos_RptaJalados_xOficina_top(Session("CodEmpresa"), Session("Ruta_Emp"), pdTop)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    cmdGlobal.CommandText = " INSERT INTO V_ListaTop_RptaJalados_top (num_reg, canteval , EVALUACION_PROCESO ,PROCESO_NOMBRE, " _
                                            & " TAREA_CODIGO ,TAREA_NOMBRE, EVALPRO_PREGUNTA ,PREGUNTA_NOMBRE,tarea_orden , tareadet_orden ) " _
                                            & " VALUES ( " & i & "," & Nz(dr("CANT")) & " , " & Nz(dr("EVALUACION_PROCESO")) & " ,'" & Nu(dr("PROCESO_NOMBRE")) & "', " _
                                            & " " & Nz(dr("TAREA_CODIGO")) & " ,'" & Nu(dr("TAREA_NOMBRE")) & "', " _
                                            & " " & Nz(dr("EVALPRO_PREGUNTA")) & ", '" & Nu(dr("TAREADET_NOMBRE")) & "'," & Nz(dr("tarea_orden")) & ", " & Nz(dr("tareadet_orden")) & " )"
                    cmdGlobal.ExecuteNonQuery()
                Next
            End If
            dt = Nothing
        End If

        dt = Nothing
        gwLista.DataSource = dt
        gwLista.DataBind()

        gwLista.Columns.Clear()

        Dim columnac1 As New BoundField
        columnac1.HeaderText = "Tarea"
        columnac1.DataField = "c1"
        gwLista.Columns.Add(columnac1)
        Dim columnac2 As New BoundField
        columnac2.HeaderText = ""
        columnac2.DataField = "c2"
        columnac2.ItemStyle.ForeColor = System.Drawing.Color.White
        columnac2.ItemStyle.Width = 0
        gwLista.Columns.Add(columnac2)

        'Prc_EvaluacionProceso_ListaOficina_RptaJalados
        i = 2
        drT = dtListado.NewRow()
        drT("c1") = ""
        drT("c2") = ""
        Dim pdColumna As String = "c"
        dt = ObjProceso.Lista_Oficina_RptaJaladas(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso, DdlAño.Text, DdlAño.Text + psMes, DdlAño.Text + psMesFin, psDm, pdCodTienda)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                i = i + 1
                pdColumna = "c" & i
                dtListado.Columns.Add(pdColumna)
                Dim columna As New BoundField
                columna.HeaderText = Nu(dr("OFICINA_NOMBRE"))
                columna.DataField = pdColumna
                columna.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                gwLista.Columns.Add(columna)
                drT(pdColumna) = Nu(dr("EVALUACION_OFICINA"))
            Next
        End If
        dt = Nothing

        Dim columnacT As New BoundField
        columnacT.HeaderText = "Total"
        columnacT.DataField = "cT"
        columnacT.ItemStyle.HorizontalAlign = HorizontalAlign.Right
        gwLista.Columns.Add(columnacT)
        dtListado.Columns.Add("cT")
        drT("cT") = ""

        dtListado.Rows.Add(drT)

        i = 0
        Try
            If pdTop = 0 Then dt = ObjProceso.Lista_Tareas_RptaJaladas(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso, DdlAño.Text, DdlAño.Text + psMes, DdlAño.Text + psMesFin, psDm, pdCodTienda)
            If pdTop > 0 Then dt = ObjProceso.EvalProcesos_RptaJalados_ListaTareas(Session("CodEmpresa"), Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = Nu(dr("TAREA_ORDEN")) & " .- " & Nu(dr("TAREA_NOMBRE"))
                    drT("c2") = Nu(dr("TAREA_CODIGO"))
                    If pdTop = 0 Then dtTarea = ObjProceso.Lista_ActxTarea_RptaJaladas(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso, Nz(dr("TAREA_CODIGO")), DdlAño.Text, DdlAño.Text + psMes, DdlAño.Text + psMesFin, psDm, pdCodTienda)
                    If pdTop > 0 Then dtTarea = ObjProceso.EvalProcesos_RptaJalados_ListaAct(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(dr("TAREA_CODIGO")), Nz(dr("num_reg")))
                    If dtTarea.Rows.Count > 0 Then
                        dtListado.Rows.Add(drT)
                        For Each drTarea As DataRow In dtTarea.Rows
                            ii = ii + 1
                            drT = dtListado.NewRow()
                            drT("c1") = ".           ." & Nu(dr("TAREA_ORDEN")) & "." & Nu(drTarea("TAREADET_ORDEN")) & " .- " & Nu(drTarea("TAREADET_NOMBRE"))
                            drT("c2") = Nu(drTarea("EVALPRO_PREGUNTA"))
                            dtActividades = Nothing
                            dtListado.Rows.Add(drT)
                        Next
                    End If
                    dtTarea = Nothing
                Next
            End If

            Dim columna As New BoundField
            Dim psCantEval As Double = 0
            Dim ia As Integer = 0
            Dim Colum As Integer = 0

            gwLista.DataSource = dtListado
            gwLista.DataBind()

            If ii > 1 Then lblRegistro.Text = "Se encontrarón " & ii & " registros."
            If ii = 1 Then lblRegistro.Text = "Se encontró " & ii & " registro."
            If ii = 0 Then lblRegistro.Text = "No se encontrarón registros."

            If gwLista.Rows.Count - 1 > 0 Then
                For a = 2 To gwLista.Columns.Count - 2
                    gwLista.Rows(0).Cells(a).ForeColor = System.Drawing.Color.White
                    If Replace(gwLista.Rows(0).Cells(a).Text, "&nbsp;", "") = "" Then
                        pdColumna = "c" & a
                        gwLista.Columns(a).Visible = False
                    End If
                Next
            End If

            Dim pdCodOficina As Double = 0
            For a = 0 To gwLista.Columns.Count - 2
                If Replace(gwLista.Rows(0).Cells(a).Text, "&nbsp;", "") <> "" Then
                    pdCodOficina = gwLista.Rows(0).Cells(a).Text
                    dtTarea = ObjProceso.EvalLista_RptaJalados_xOficina(Session("CodEmpresa"), Session("Ruta_Emp"), psCodProceso, DdlAño.Text, DdlAño.Text + psMes, DdlAño.Text + psMesFin, pdCodOficina, psDm)
                    If dtTarea.Rows.Count > 0 Then
                        For Each drTarea As DataRow In dtTarea.Rows
                            For i = 0 To gwLista.Rows.Count - 1
                                If Replace(gwLista.Rows(i).Cells(0).Text, "&nbsp;", "") <> "" Then
                                    If Left(gwLista.Rows(i).Cells(0).Text, 1) <> "." Then
                                        gwLista.Rows(i).Cells(0).Font.Bold = True
                                        gwLista.Rows(i).Cells(0).Font.Size = "11"
                                    End If
                                End If
                                If gwLista.Rows(i).Cells(1).Text = Nu(drTarea("EVALPRO_PREGUNTA")) Then
                                    gwLista.Rows(i).Cells(a).Text = Nu(drTarea("canteval"))
                                    gwLista.Rows(i).Cells(a).ForeColor = System.Drawing.Color.Black
                                End If
                            Next
                        Next
                    End If
                    dtTarea = Nothing
                End If
            Next
            Dim pdTotalRpta As Double = 0
            For i = 1 To gwLista.Rows.Count - 1
                If Replace(gwLista.Rows(i).Cells(0).Text, "&nbsp;", "") <> "" Then
                    If Left(gwLista.Rows(i).Cells(0).Text, 1) <> "." Then
                        gwLista.Rows(i).Cells(0).Font.Bold = True
                        gwLista.Rows(i).Cells(0).Font.Size = "10"
                    End If
                End If
                pdTotalRpta = 0
                For a = 2 To gwLista.Columns.Count - 2
                    If Replace(gwLista.Rows(i).Cells(a).Text, "&nbsp;", "") <> "" Then
                        pdTotalRpta = pdTotalRpta + CDbl(Nz(gwLista.Rows(i).Cells(a).Text))
                    End If
                Next
                gwLista.Rows(i).Cells(gwLista.Columns.Count - 1).Text = IIf(pdTotalRpta = 0, "", pdTotalRpta)
            Next

            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click

        If DdlProceso.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Proceso"
        If DdlMes.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Mes"
        Dim pdCodProceso As Double = 0
        If DdlProceso.SelectedValue <> "< Seleccionar >" Then pdCodProceso = Nz(DdlProceso.SelectedValue)
        Call Lista_Tareas(pdCodProceso)

    End Sub
    Protected Sub DdlAño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub DdlProceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProceso.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub DdlMes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlMes.SelectedIndexChanged
        DdlMesFin.SelectedValue = DdlMes.SelectedValue
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub DdlMesFin_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlMesFin.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub DdlRM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlRM.SelectedIndexChanged
        lblError.Text = ""
        Try
            If DdlRM.SelectedValue <> "< Total Sistema >" Then
                DdlDM.Items.Clear()
                DdlDM.DataSource = ObjProceso.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), DdlRM.SelectedValue)
                DdlDM.DataTextField = "c3"
                DdlDM.DataValueField = "c4"
                DdlDM.DataBind()
                DdlDM.Items.Add("< Total Sistema >")
                DdlDM.SelectedValue = "< Total Sistema >"
            Else
                Call Cargar_RM(DdlDM, 10)
            End If
            BtnListar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub DdlDM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDM.SelectedIndexChanged
        Dim pdCodDm As String = ""
        If DdlDM.SelectedValue <> "< Total Sistema >" Then
            pdCodDm = DdlDM.SelectedValue
            Call FnProceso.Llenar_Oficina(DdlTienda, Session("CodEmpresa"), Session("CodGrupoEmpresa"), Session("Ruta_Emp"), pdCodDm)
            DdlTienda.Items.Add("< Total Sistema >")
            DdlTienda.SelectedValue = "< Total Sistema >"
        Else
            Lista_Oficinatodo(DdlTienda)
        End If
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub DdlTienda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTienda.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub DdlTop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTop.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub
End Class
