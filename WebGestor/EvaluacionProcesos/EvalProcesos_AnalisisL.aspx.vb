Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_AnalisisL
    Inherits System.Web.UI.Page
    Dim obj As New ClsEval_Proceso
    Dim objGrupoEmp As New ModuloGeneral
    Dim objSeg As New ModuloSeguridad
    Dim Fn As New clsEval_Proceso_Funciones
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            Call Cargar_RM(ddlRM, 9)
            Call Cargar_RM(ddlDM, 10)
            Call Cargar_RM(ddlTRM, 9)
            Call Cargar_RM(ddlTDM, 10)
            Call Lista_Oficinatodo(ddlTienda)
            Call Cargar_RM(ddlSoaRM, 9)
            Call Cargar_RM(ddlSoaDM, 10)
            Call Lista_Oficinatodo(ddlSoaTienda)
            Call Cargar_RM(ddlRMQasa, 9)
            Call Cargar_RM(ddlDMQasa, 10)
            Call Lista_Oficinatodo(ddlTiendaQasa)
            DdlAño.Items.Clear()
            Call LlenaAno(DdlAño)
            DdlAño.SelectedValue = CInt(Left(FechaActual, 4))
            DdlAño.Focus()
            DdlAño2.Items.Clear()
            Call LlenaAno(DdlAño2)
            DdlAño2.SelectedValue = CInt(Left(FechaActual, 4))
            DdlAño2.Focus()
            DdlAño3.Items.Clear()
            Call LlenaAno(DdlAño3)
            DdlAño3.SelectedValue = CInt(Left(FechaActual, 4))
            DdlAño3.Focus()
            DdlAño4.Items.Clear()
            Call LlenaAno(DdlAño4)
            DdlAño4.SelectedValue = CInt(Left(FechaActual, 4))
            DdlAño4.Focus()
            BtnListar_Click(sender, e)
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

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim dt As New DataTable
        lblError.Text = ""
        Dim psDm As String = ""
        dt = Nothing
        gwLista.DataSource = dt
        gwLista.DataBind()
        Try
            If ddlRM.SelectedValue <> "< Total Sistema >" And ddlDM.SelectedValue = "< Total Sistema >" Then
                dt = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlRM.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If psDm <> "" Then psDm = psDm & ","
                        psDm = psDm & dr("c4")
                    Next
                End If
                dt = Nothing
                dt = obj.Lista_Dashboard_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), psDm, DdlAño.Text)
                gwLista.DataSource = dt
                gwLista.DataBind()
            ElseIf ddlRM.SelectedValue <> "< Total Sistema >" And ddlDM.SelectedValue <> "< Total Sistema >" Then
                psDm = ddlDM.SelectedValue
                dt = obj.Lista_Dashboard_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), psDm, DdlAño.Text)
                gwLista.DataSource = dt
                gwLista.DataBind()
            ElseIf ddlRM.SelectedValue = "< Total Sistema >" And ddlDM.SelectedValue <> "< Total Sistema >" Then
                psDm = ddlDM.SelectedValue
                dt = obj.Lista_Dashboard_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), psDm, DdlAño.Text)
                gwLista.DataSource = dt
                gwLista.DataBind()
            Else
                dt = obj.Lista_Dashboard(Session("CodEmpresa"), Session("Ruta_Emp"), DdlAño.Text)
                gwLista.DataSource = dt
                gwLista.DataBind()
            End If
            Dim a As Long = 0
            Dim psPorcAprobadas As Decimal = 0
            For i = 0 To gwLista.Rows.Count - 1
                For a = 4 To 9
                    If Nz(gwLista.Rows(i).Cells(3).Text) > 0 And Nz(gwLista.Rows(i).Cells(7).Text) > 0 Then
                        psPorcAprobadas = (Nz(gwLista.Rows(i).Cells(2).Text) / Nz(gwLista.Rows(i).Cells(3).Text)) * 100
                        gwLista.Rows(i).Cells(4).Text = Decimal.Round(psPorcAprobadas, 2)
                        psPorcAprobadas = (Nz(gwLista.Rows(i).Cells(6).Text) / Nz(gwLista.Rows(i).Cells(7).Text)) * 100
                        gwLista.Rows(i).Cells(8).Text = Decimal.Round(psPorcAprobadas, 2)
                        If a <> 6 And a <> 7 Then
                            If (gwLista.Rows(i).Cells(a).Text >= 90) Or (gwLista.Rows(i).Cells(a).Text = 0) Then
                                gwLista.Rows(i).Cells(a).BackColor = Drawing.Color.LimeGreen
                                If (a = 4 Or a = 8) And gwLista.Rows(i).Cells(a).Text = 0 Then gwLista.Rows(i).Cells(a).BackColor = Drawing.Color.Red
                                gwLista.Rows(i).Cells(a).Font.Bold = True
                            Else
                                gwLista.Rows(i).Cells(a).BackColor = Drawing.Color.Red
                                gwLista.Rows(i).Cells(a).Font.Bold = True
                            End If
                        End If
                    End If
                Next
            Next
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub ddlRM_SelectedIndexChanged(sender As Object, e As EventArgs)
        lblError.Text = ""
        Try
            If ddlRM.SelectedValue <> "< Total Sistema >" Then
                ddlDM.Items.Clear()
                ddlDM.DataSource = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlRM.SelectedValue)
                ddlDM.DataTextField = "c3"
                ddlDM.DataValueField = "c4"
                ddlDM.DataBind()
                ddlDM.Items.Add("< Total Sistema >")
                ddlDM.SelectedValue = "< Total Sistema >"
            Else
                Call Cargar_RM(ddlDM, 10)
            End If
            BtnListar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub ddlTDM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTDM.SelectedIndexChanged
        Dim pdCodDm As String = ""
        If ddlTDM.SelectedValue <> "< Total Sistema >" Then
            pdCodDm = ddlTDM.SelectedValue
            Call Fn.Llenar_Oficina(ddlTienda, Session("CodEmpresa"), Session("CodGrupoEmpresa"), Session("Ruta_Emp"), pdCodDm)
            ddlTienda.Items.Add("< Total Sistema >")
            ddlTienda.SelectedValue = "< Total Sistema >"
        Else
            Lista_Oficinatodo(ddlTienda)
        End If
        BtnTLista_Click(sender, e)
    End Sub
    Protected Sub ddlTRM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTRM.SelectedIndexChanged
        lblErrorT.Text = ""
        Try
            If ddlTRM.SelectedValue <> "< Total Sistema >" Then
                ddlTDM.Items.Clear()
                ddlTDM.DataSource = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlTRM.SelectedValue)
                ddlTDM.DataTextField = "c3"
                ddlTDM.DataValueField = "c4"
                ddlTDM.DataBind()
                ddlTDM.Items.Add("< Total Sistema >")
                ddlTDM.SelectedValue = "< Total Sistema >"
                ddlTienda.Items.Clear()
                ddlTienda.Items.Add("< Total Sistema >")
                ddlTienda.SelectedValue = "< Total Sistema >"
            Else
                Call Cargar_RM(ddlTDM, 10)
                Call Lista_Oficinatodo(ddlTienda)
            End If
            BtnTLista_Click(sender, e)
        Catch ex As SqlException
            lblErrorT.Text = ex.Message
        Catch ex As Exception
            lblErrorT.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnTLista_Click(sender As Object, e As EventArgs) Handles BtnTLista.Click
        Dim dt As New DataTable
        lblErrorT.Text = ""
        Dim psTienda As Double = 0
        dt = Nothing
        Dim psDm As String = ""
        gwLista.DataSource = dt
        gwLista.DataBind()
        Try
            If ddlTienda.SelectedValue <> "< Total Sistema >" Then
                psTienda = ddlTienda.SelectedValue
                dt = obj.Lista_Dashboard_xTienda(Session("CodEmpresa"), Session("Ruta_Emp"), psTienda, DdlAño2.Text)
                dgwListaTienda.DataSource = dt
                dgwListaTienda.DataBind()
            Else
                If ddlTRM.SelectedValue <> "< Total Sistema >" And ddlTDM.SelectedValue = "< Total Sistema >" Then
                    dt = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlTRM.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            If psDm <> "" Then psDm = psDm & ","
                            psDm = psDm & dr("c4")
                        Next
                    End If
                    dt = Nothing
                    dt = obj.Lista_Dashboard_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), psDm, DdlAño2.Text)
                    dgwListaTienda.DataSource = dt
                    dgwListaTienda.DataBind()
                ElseIf ddlTRM.SelectedValue <> "< Total Sistema >" And ddlTDM.SelectedValue <> "< Total Sistema >" Then
                    psDm = ddlTDM.SelectedValue
                    dt = obj.Lista_Dashboard_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), psDm, DdlAño2.Text)
                    dgwListaTienda.DataSource = dt
                    dgwListaTienda.DataBind()
                ElseIf ddlTRM.SelectedValue = "< Total Sistema >" And ddlTDM.SelectedValue <> "< Total Sistema >" Then
                    psDm = ddlTDM.SelectedValue
                    dt = obj.Lista_Dashboard_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), psDm, DdlAño2.Text)
                    dgwListaTienda.DataSource = dt
                    dgwListaTienda.DataBind()
                Else
                    dt = obj.Lista_Dashboard(Session("CodEmpresa"), Session("Ruta_Emp"), DdlAño2.Text)
                    dgwListaTienda.DataSource = dt
                    dgwListaTienda.DataBind()
                End If
            End If
            Dim a As Long = 0
            For i = 0 To dgwListaTienda.Rows.Count - 1
                For a = 2 To 3
                    If (dgwListaTienda.Rows(i).Cells(a).Text >= 90) Or (dgwListaTienda.Rows(i).Cells(a).Text = 0) Then
                        dgwListaTienda.Rows(i).Cells(a).BackColor = Drawing.Color.LimeGreen
                        dgwListaTienda.Rows(i).Cells(a).Font.Bold = True
                    Else
                        dgwListaTienda.Rows(i).Cells(a).BackColor = Drawing.Color.Red
                        dgwListaTienda.Rows(i).Cells(a).Font.Bold = True
                    End If
                Next
            Next
        Catch ex As SqlException
            lblErrorT.Text = ex.Message
        Catch ex As Exception
            lblErrorT.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub dgwListaTienda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgwListaTienda.SelectedIndexChanged

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Evaluacion_Promedio_RMDM_xTienda
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim psNombreTienda As String = ""
        Dim psDm As String = ""
        lblErrorSoa.Text = ""
        Session("datos") = Nothing
        Try 'Lista_Evaluacion_xDM
            If ddlSoaTienda.SelectedValue <> "< Total Sistema >" Then
                psNombreTienda = ddlSoaTienda.SelectedValue
            End If
            If ddlSoaRM.SelectedValue <> "< Total Sistema >" And ddlSoaDM.SelectedValue = "< Total Sistema >" And ddlSoaTienda.SelectedValue = "< Total Sistema >" Then
                dt = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlSoaRM.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If psDm <> "" Then psDm = psDm & ","
                        psDm = psDm & dr("c4")
                    Next
                End If
                dt = Nothing
                dt = obj.Lista_Dashboard_XRMDM_xTienda(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), psDm, DdlAño3.Text)
                gwListaSoa.DataSource = dt
                gwListaSoa.DataBind()
            ElseIf ddlSoaDM.SelectedValue <> "< Total Sistema >" And ddlSoaTienda.SelectedValue = "< Total Sistema >" Then
                psDm = ddlSoaDM.SelectedValue
                dt = obj.Lista_Dashboard_XRMDM_xTienda(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), psDm, DdlAño3.Text)
                gwListaSoa.DataSource = dt
                gwListaSoa.DataBind()
            Else
                dt = obj.Lista_Dashboard_RMDM_xTienda(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), psNombreTienda, DdlAño3.Text)
                gwListaSoa.DataSource = dt
                gwListaSoa.DataBind()
            End If
            Session("datos") = dt
            Call Promedio_xProcesos(gwListaSoa)
            Dim a As Long = 0
            For i = 0 To gwListaSoa.Rows.Count - 1
                For a = 1 To 12
                    If (gwListaSoa.Rows(i).Cells(2 + a).Text = 0) Then
                        gwListaSoa.Rows(i).Cells(2 + a).ForeColor = Drawing.Color.White
                        gwListaSoa.Rows(i).Cells(2 + a).Font.Bold = True
                    ElseIf (gwListaSoa.Rows(i).Cells(2 + a).Text >= 90) Then
                        gwListaSoa.Rows(i).Cells(2 + a).BackColor = Drawing.Color.LimeGreen
                        gwListaSoa.Rows(i).Cells(2 + a).Font.Bold = True
                    Else
                        gwListaSoa.Rows(i).Cells(2 + a).BackColor = Drawing.Color.Red
                        gwListaSoa.Rows(i).Cells(2 + a).Font.Bold = True
                    End If
                Next
            Next
            Dim psValor As Double = 0
            Dim psValorError As Double = 0
            Dim psSumValor As Double = 0
            If chkDesaprob.Checked = True Then
                For i = 0 To gwListaSoa.Rows.Count - 1
                    psValor = 0 : psSumValor = 0 : psValorError = 0
                    For a = 1 To 12
                        If Nz(gwListaSoa.Rows(i).Cells(2 + a).Text) > 0 Then
                            psValor = Nz(gwListaSoa.Rows(i).Cells(2 + a).Text)
                            psSumValor = psSumValor + Nz(gwListaSoa.Rows(i).Cells(2 + a).Text)
                            If psValor <= 90 Then
                                psValorError = 1
                            Else
                                psValor = 0
                            End If
                        End If
                    Next
                    If psValor = 0 And psValorError = 0 Then gwListaSoa.Rows(i).Visible = False
                    If psSumValor = 0 Then gwListaSoa.Rows(i).Visible = False
                Next
            End If
        Catch ex As SqlException
            lblErrorSoa.Text = ex.Message
        Catch ex As Exception
            lblErrorSoa.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub ddlSoaRM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSoaRM.SelectedIndexChanged
        lblErrorSoa.Text = ""
        gwListaSoa.DataSource = Nothing
        gwListaSoa.DataBind()
        Try
            If ddlSoaRM.SelectedValue <> "< Total Sistema >" Then
                ddlSoaDM.Items.Clear()
                ddlSoaDM.DataSource = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlSoaRM.SelectedValue)
                ddlSoaDM.DataTextField = "c3"
                ddlSoaDM.DataValueField = "c4"
                ddlSoaDM.DataBind()
                ddlSoaDM.Items.Add("< Total Sistema >")
                ddlSoaDM.SelectedValue = "< Total Sistema >"
                ddlSoaTienda.Items.Clear()
                ddlSoaTienda.Items.Add("< Total Sistema >")
                ddlSoaTienda.SelectedValue = "< Total Sistema >"
            Else
                Call Cargar_RM(ddlSoaDM, 10)
                Call Lista_Oficinatodo(ddlSoaTienda)
            End If
            Call Button1_Click(sender, e)
        Catch ex As SqlException
            lblErrorT.Text = ex.Message
        Catch ex As Exception
            lblErrorT.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub ddlSoaTienda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSoaTienda.SelectedIndexChanged
        Call Button1_Click(sender, e)
    End Sub
    Sub ddlSoaDM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSoaDM.SelectedIndexChanged
        Dim pdCodDm As String = ""
        If ddlSoaDM.SelectedValue <> "< Total Sistema >" Then
            ddlSoaTienda.Items.Clear()
            pdCodDm = ddlSoaDM.SelectedValue
            Call Fn.Llenar_Oficina(ddlSoaTienda, Session("CodEmpresa"), Session("CodGrupoEmpresa"), Session("Ruta_Emp"), pdCodDm)
            ddlSoaTienda.Items.Add("< Total Sistema >")
            ddlSoaTienda.SelectedValue = "< Total Sistema >"
        Else
            Lista_Oficinatodo(ddlSoaTienda)
        End If
        Call Button1_Click(sender, e)
    End Sub
    Protected Sub chkDesaprob_CheckedChanged(sender As Object, e As EventArgs) Handles chkDesaprob.CheckedChanged
        Call Button1_Click(sender, e)
    End Sub
    Private Sub Ficha_ActiveTabChanged(sender As Object, e As EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call BtnListar_Click(sender, e)
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call BtnTLista_Click(sender, e)
        End If
        If Ficha.ActiveTabIndex = 2 Then
            Call Button1_Click(sender, e)
        End If
        If Ficha.ActiveTabIndex = 3 Then
            Call BtnListarQasa_Click(sender, e)
        End If
    End Sub

    Private Sub ddlRMQasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRMQasa.SelectedIndexChanged
        lblErrorQ.Text = ""
        gwListaQasa.DataSource = Nothing
        gwListaQasa.DataBind()
        Try
            If ddlRMQasa.SelectedValue <> "< Total Sistema >" Then
                ddlDMQasa.Items.Clear()
                ddlDMQasa.DataSource = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlRMQasa.SelectedValue)
                ddlDMQasa.DataTextField = "c3"
                ddlDMQasa.DataValueField = "c4"
                ddlDMQasa.DataBind()
                ddlDMQasa.Items.Add("< Total Sistema >")
                ddlDMQasa.SelectedValue = "< Total Sistema >"
                ddlTiendaQasa.Items.Clear()
                ddlTiendaQasa.Items.Add("< Total Sistema >")
                ddlTiendaQasa.SelectedValue = "< Total Sistema >"
            Else
                Call Cargar_RM(ddlDMQasa, 10)
                Call Lista_Oficinatodo(ddlTiendaQasa)
            End If
            Call BtnListarQasa_Click(sender, e)
        Catch ex As SqlException
            lblErrorT.Text = ex.Message
        Catch ex As Exception
            lblErrorT.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnListarQasa_Click(sender As Object, e As EventArgs) Handles BtnListarQasa.Click
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim psNombreTienda As String = ""
        Dim psDm As String = ""
        lblErrorQ.Text = ""
        Session("datos") = Nothing
        Try 'Lista_Evaluacion_xDM
            If ddlTiendaQasa.SelectedValue <> "< Total Sistema >" Then
                psNombreTienda = ddlTiendaQasa.SelectedValue
            End If
            If ddlRMQasa.SelectedValue <> "< Total Sistema >" And ddlDMQasa.SelectedValue = "< Total Sistema >" And ddlTiendaQasa.SelectedValue = "< Total Sistema >" Then
                dt = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlRMQasa.SelectedValue)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If psDm <> "" Then psDm = psDm & ","
                        psDm = psDm & dr("c4")
                    Next
                End If
                dt = Nothing
                dt = obj.Lista_Dashboard_XRMDM_xTienda_QASA(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), psDm, DdlAño4.Text)
                gwListaQasa.DataSource = dt
                gwListaQasa.DataBind()
            ElseIf ddlDMQasa.SelectedValue <> "< Total Sistema >" And ddlTiendaQasa.SelectedValue = "< Total Sistema >" Then
                psDm = ddlDMQasa.SelectedValue
                dt = obj.Lista_Dashboard_XRMDM_xTienda_QASA(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), psDm, DdlAño4.Text)
                gwListaQasa.DataSource = dt
                gwListaQasa.DataBind()
            Else
                dt = obj.Lista_Dashboard_RMDM_xProceso(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), psNombreTienda, DdlAño4.Text)
                gwListaQasa.DataSource = dt
                gwListaQasa.DataBind()
            End If
            Session("datos") = dt
            Call Promedio_xProcesos(gwListaQasa)
            Dim a As Long = 0
            For i = 0 To gwListaQasa.Rows.Count - 1
                For a = 1 To 12
                    If (gwListaQasa.Rows(i).Cells(2 + a).Text = 0) Then
                        gwListaQasa.Rows(i).Cells(2 + a).ForeColor = Drawing.Color.White
                        gwListaQasa.Rows(i).Cells(2 + a).Font.Bold = True
                    ElseIf (gwListaQasa.Rows(i).Cells(2 + a).Text >= 90) Then
                        gwListaQasa.Rows(i).Cells(2 + a).BackColor = Drawing.Color.LimeGreen
                        gwListaQasa.Rows(i).Cells(2 + a).Font.Bold = True
                    Else
                        gwListaQasa.Rows(i).Cells(2 + a).BackColor = Drawing.Color.Red
                        gwListaQasa.Rows(i).Cells(2 + a).Font.Bold = True
                    End If
                Next
            Next
            Dim psValor As Double = 0
            Dim psValorError As Double = 0
            Dim psSumValor As Double = 0
            If chkDesaprobQasa.Checked = True Then
                For i = 0 To gwListaQasa.Rows.Count - 1
                    psValor = 0 : psSumValor = 0 : psValorError = 0
                    For a = 1 To 12
                        If Nz(gwListaQasa.Rows(i).Cells(2 + a).Text) > 0 Then
                            psValor = Nz(gwListaQasa.Rows(i).Cells(2 + a).Text)
                            psSumValor = psSumValor + Nz(gwListaQasa.Rows(i).Cells(2 + a).Text)
                            If psValor <= 90 Then
                                psValorError = 1
                            Else
                                psValor = 0
                            End If
                        End If
                    Next
                    If psValor = 0 And psValorError = 0 Then gwListaQasa.Rows(i).Visible = False
                    If psSumValor = 0 Then gwListaQasa.Rows(i).Visible = False
                Next
            End If
        Catch ex As SqlException
            lblErrorQ.Text = ex.Message
        Catch ex As Exception
            lblErrorQ.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub Promedio_xProcesos(ByVal gw As GridView)
        Dim pdCantDatos As Double = 0
        Dim psSumValor As Double = 0
        Dim psPromedio As Decimal = 0
        Dim dtListado As New DataTable
        Dim drT As DataRow
        dtListado = Session("datos")
        Dim psColumna As String = ""
        drT = dtListado.NewRow()
        For a = 1 To 12
            psSumValor = 0 : pdCantDatos = 0 : psPromedio = 0
            For i = 0 To gw.Rows.Count - 1
                If Nz(gw.Rows(i).Cells(2 + a).Text) > 0 Then
                    pdCantDatos = pdCantDatos + 1
                    psSumValor = psSumValor + Nz(gw.Rows(i).Cells(2 + a).Text)
                End If
            Next
            drT("DM") = "Total sistema"
            If pdCantDatos = 0 And psSumValor = 0 Then
                psPromedio = 0
            Else
                psPromedio = psSumValor / pdCantDatos
            End If
            If a < 10 Then
                psColumna = "C0" & a
                drT(psColumna) = Decimal.Round(psPromedio, 2)
            Else
                psColumna = "C" & a
                drT(psColumna) = Decimal.Round(psPromedio, 2)
            End If
        Next
        dtListado.Rows.Add(drT)
        gw.DataSource = dtListado
        gw.DataBind()
    End Sub
    Protected Sub chkDesaprobQasa_CheckedChanged(sender As Object, e As EventArgs) Handles chkDesaprobQasa.CheckedChanged
        Call BtnListarQasa_Click(sender, e)
    End Sub

    Private Sub ddlDMQasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDMQasa.SelectedIndexChanged
        Dim pdCodDm As String = ""
        If ddlDMQasa.SelectedValue <> "< Total Sistema >" Then
            ddlTiendaQasa.Items.Clear()
            pdCodDm = ddlDMQasa.SelectedValue
            Call Fn.Llenar_Oficina(ddlSoaTienda, Session("CodEmpresa"), Session("CodGrupoEmpresa"), Session("Ruta_Emp"), pdCodDm)
            ddlTiendaQasa.Items.Add("< Total Sistema >")
            ddlTiendaQasa.SelectedValue = "< Total Sistema >"
        Else
            Lista_Oficinatodo(ddlTiendaQasa)
        End If
        Call BtnListarQasa_Click(sender, e)
    End Sub

    Private Sub ddlTiendaQasa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTiendaQasa.SelectedIndexChanged
        Call BtnListarQasa_Click(sender, e)
    End Sub

    Private Sub ddlDM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDM.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub

    Private Sub DdlAño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño.SelectedIndexChanged
        BtnListar_Click(sender, e)
    End Sub

    Private Sub DdlAño2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño2.SelectedIndexChanged
        BtnTLista_Click(sender, e)
    End Sub

    Private Sub DdlAño3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño3.SelectedIndexChanged
        Button1_Click(sender, e)
    End Sub

    Private Sub DdlAño4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño4.SelectedIndexChanged
        BtnListarQasa_Click(sender, e)
    End Sub
    Protected Sub ddlTienda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTienda.SelectedIndexChanged
        BtnTLista_Click(sender, e)
    End Sub
End Class
