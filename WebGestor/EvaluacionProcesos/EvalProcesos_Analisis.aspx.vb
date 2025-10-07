Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_Analisis
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            DdlAño.Items.Clear()
            Call LlenaAno(DdlAño)
            DdlAño.SelectedValue = CInt(Left(FechaActual, 4))
            DdlAño.Focus()
            Call Cargar_Proceso(ddlProceso)
            Call Listar_Oficina()
        End If
    End Sub
    Private Sub Listar_Oficina()
        Dim ObjProceso As New ClsEval_Proceso
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim dtPromd As New DataTable
        Dim psCodProceso As Double = 7
        Dim i As Integer = 0
        lblError.Text = ""
        dtListado.Columns.Add("Oficina_nombre")
        dtListado.Columns.Add("Oficina_codigo")
        dtListado.Columns.Add("c1")
        If ddlProceso.Text <> " " Then psCodProceso = ddlProceso.SelectedValue
        Try
            dt = ObjProceso.Lista_EvaluacionOficina(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), psCodProceso, DdlAño.Text)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    drT = dtListado.NewRow()
                    drT("Oficina_nombre") = Nu(dr("OFICINA_NOMBRE"))
                    drT("Oficina_codigo") = Nu(dr("OFICINA_CODIGO"))
                    dtPromd = ObjProceso.Evaluacion_PromedioxOficina(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(dr("OFICINA_CODIGO")), psCodProceso, DdlAño.Text)
                    If dtPromd.Rows.Count > 0 Then
                        For Each dr2 As DataRow In dtPromd.Rows
                            drT("c1") = Nz(dr2("PROMEDIO_FINAL")) & "%"
                        Next
                    End If
                    dtListado.Rows.Add(drT)
                Next
            End If
            gwLista.DataSource = dtListado
            gwLista.DataBind()
            dt = Nothing
            dtListado = Nothing
            Dim a As Integer = 0
            Dim psProceso As String = ""
            Dim psFecha As String = ""
            Dim psEstado As String = ""
            Dim psPromedio As String = ""
            For i = 0 To gwLista.Rows.Count - 1
                a = 0
                dt = ObjProceso.Lista_EvaluacionxOficina(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(gwLista.Rows(i).Cells(1).Text), psCodProceso, DdlAño.Text)
                If dt.Rows.Count > 0 Then
                    For Each dra As DataRow In dt.Rows
                        a = a + 1
                        If a < 7 Then
                            psProceso = "txtCodEval" & a
                            psFecha = "txtFecha" & a
                            psEstado = "txtEstado" & a
                            psPromedio = "txtPromedio" & a
                            Dim txtCodEval As TextBox = gwLista.Rows(i).Cells(2 + a).FindControl(psProceso)
                            Dim txtFecha As TextBox = gwLista.Rows(i).Cells(2 + a).FindControl(psFecha)
                            Dim txtEstado As TextBox = gwLista.Rows(i).Cells(2 + a).FindControl(psEstado)
                            Dim txtPromedio As TextBox = gwLista.Rows(i).Cells(2 + a).FindControl(psPromedio)
                            txtCodEval.Text = "" : txtFecha.Text = "" : txtEstado.Text = "" : txtPromedio.Text = ""
                            txtCodEval.Text = "Proceso " & Nu(dra("NombreProceso"))
                            txtFecha.Text = "Fecha Eval. : " & Nu(dra("fecha_eval"))
                            txtEstado.Text = "Estado : " & Nu(dra("Estado"))
                            dtListado = ObjProceso.Evaluacion_Resultado(Session("CodEmpresa"), Session("Ruta_Emp"), Nz(dra("evaluacion_codigo")), 218, Nz(dra("CodProceso")))
                            If dtListado.Rows.Count > 0 Then
                                For Each drResul As DataRow In dtListado.Rows
                                    If Not IsDBNull(drResul(0)) Then
                                        txtPromedio.Text = drResul(0) & "%"
                                    End If
                                Next
                            End If
                        Else
                            a = 7
                        End If
                    Next
                End If
            Next
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub Cargar_Proceso(ByVal combo As DropDownList)

        Dim objSeg As New ClsEval_Proceso
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = objSeg.Lista_Proceso(Session("CodEmpresa"), Session("Ruta_Emp"))
        combo.DataTextField = "NombreProceso"
        combo.DataValueField = "CodProceso"
        combo.DataBind()
        combo.SelectedValue = 7

    End Sub
    Protected Sub ddlProceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProceso.SelectedIndexChanged
        Call Listar_Oficina()
    End Sub

    Private Sub DdlAño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño.SelectedIndexChanged
        Call Listar_Oficina()
    End Sub
End Class
