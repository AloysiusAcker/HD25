
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_Relacionar
    Inherits System.Web.UI.Page

    Dim obj As New ClsEval_Proceso
    Dim objGrupoEmp As New ModuloGeneral
    Dim objSeg As New ModuloSeguridad
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            'Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub

    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            divAsignar.Visible = False
            Call Cargar_RM(ddlRM, 9)
            Call Listar_DM()
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call Listar_OficinaxDM()
            Call Cargar_RM(DdlDM, 10)
        End If
    End Sub

    Private Sub Listar_OficinaxDM()
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try
            dt = obj.Evaluacion_ListaRelacion_OficinaDM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"))
            GwListaOficinaxDM.DataSource = dt
            GwListaOficinaxDM.DataBind()
            dt = Nothing

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    '
    Private Sub Listar_OficinaxDM_Agregar()
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try
            dt = objSeg.Listar_Oficina(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
            GwOficinas.DataSource = dt
            GwOficinas.DataBind()
            dt = Nothing

            dt = obj.Evaluacion_ListaRelacion_OficinaXDM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), DdlDM.SelectedValue)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    For i = 0 To GwOficinas.Rows.Count - 1
                        If GwOficinas.Rows(i).Cells(1).Text = dr("c4").ToString Then
                            Dim Check = CType(GwOficinas.Rows(i).Cells(0).FindControl("chkOf"), CheckBox)
                            Check.Checked = True
                            Exit For
                        End If
                    Next
                Next
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Cargar_RM(ByVal ddl As DropDownList, ByVal psCodCargo As Double)
        ddl.Items.Clear() 'Listar_Usuarios
        ddl.DataSource = objGrupoEmp.Lista_Personal_xCargo(Session("CodGrupoEmpresa"), Session("CodEmpresa"), psCodCargo)
        ddl.DataTextField = "NOMBRE_PERSONAL"
        ddl.DataValueField = "PERSON_CODIGO"
        ddl.DataBind()
        ddl.Items.Add(" ")
        ddl.SelectedValue = " "
    End Sub
    Private Sub Listar_DM()
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try
            dt = obj.Evaluacion_ListaRelacion_RMDM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"))
            gwLista.DataSource = dt
            gwLista.DataBind()

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub Listar_PersonalDM()
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try
            dt = objGrupoEmp.Lista_Personal_xCargo(Session("CodGrupoEmpresa"), Session("CodEmpresa"), 10)
            GwListaDM.DataSource = dt
            GwListaDM.DataBind()
            dt = Nothing

            dt = obj.Evaluacion_ListaRelacion_RMDM_xRM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlRM.SelectedValue)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    For i = 0 To GwListaDM.Rows.Count - 1
                        If GwListaDM.Rows(i).Cells(1).Text = dr("c4").ToString Then
                            Dim Check = CType(GwListaDM.Rows(i).Cells(0).FindControl("chkUser"), CheckBox)
                            Check.Checked = True
                            Exit For
                        End If
                    Next
                Next
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnAsignar_Click(sender As Object, e As EventArgs) Handles BtnAsignar.Click
        divAsignar.Visible = True
        ddlRM.SelectedValue = " "
        GwLista.DataSource = Nothing
        GwLista.DataBind()
    End Sub
    Protected Sub ddlRM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRM.SelectedIndexChanged
        Call Listar_PersonalDM()
    End Sub
    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        divAsignar.Visible = False
        ddlRM.SelectedValue = " "
        GwListaDM = Nothing
    End Sub
    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim UserRM As String = ""
        UserRM = ddlRM.Text
        Dim Usuario As CheckBox
        If UserRM = " " Then lblError.Text = "Debe seleccionar al Personal RM." : Exit Sub
        For i = 0 To GwListaDM.Rows.Count - 1
            Usuario = GwListaDM.Rows(i).Cells(0).FindControl("chkUser")
            If Usuario.Checked = True And Usuario.Enabled = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblError.Text = lblError.Text & "Debe de marcar al menos una DM." : Exit Sub
        lblError.Text = ""
        obj.Evaluacion_Delete_RMDAM(Session("CodEmpresa"), Session("Ruta_Emp"), ddlRM.SelectedValue)
        Try
            For i = 0 To GwListaDM.Rows.Count - 1
                Usuario = GwListaDM.Rows(i).Cells(0).FindControl("chkUser")
                If Usuario.Checked = True Then
                    obj.Evaluacion_Insert_RMDAM(Session("CodEmpresa"), Session("Ruta_Emp"), ddlRM.SelectedValue, GwListaDM.Rows(i).Cells(1).Text)
                End If
            Next
            Call BtnCancelar_Click(sender, e)
            Call Listar_DM()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnAsignarOf_Click(sender As Object, e As EventArgs) Handles BtnAsignarOf.Click
        divOficina.Visible = True
        DdlDM.SelectedValue = " "
        GwOficinas.DataSource = Nothing
        GwOficinas.DataBind()
    End Sub
    Protected Sub DdlDM_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDM.SelectedIndexChanged
        Call Listar_OficinaxDM_Agregar()
    End Sub
    Protected Sub BtnCancelarOf_Click(sender As Object, e As EventArgs) Handles BtnCancelarOf.Click
        divOficina.Visible = False

    End Sub

    Private Sub BtnGuardarOf_Click(sender As Object, e As EventArgs) Handles BtnGuardarOf.Click
        Dim i As Integer
        Dim a As Integer : a = 0
        lblError.Text = ""
        Dim UserDM As String = ""
        UserDM = DdlDM.Text
        Dim Oficina As CheckBox
        If UserDM = " " Then lblError.Text = "Debe seleccionar al Personal DM." : Exit Sub
        For i = 0 To GwOficinas.Rows.Count - 1
            Oficina = GwOficinas.Rows(i).Cells(0).FindControl("chkOf")
            If Oficina.Checked = True And Oficina.Enabled = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblError.Text = lblError.Text & "Debe de marcar al menos una Oficina." : Exit Sub
        lblError.Text = ""
        obj.Evaluacion_Delete_OficinaDM(Session("CodEmpresa"), Session("Ruta_Emp"), DdlDM.SelectedValue)
        Try
            For i = 0 To GwOficinas.Rows.Count - 1
                Oficina = GwOficinas.Rows(i).Cells(0).FindControl("chkOf")
                If Oficina.Checked = True Then
                    obj.Evaluacion_Insert_OficinaDM(Session("Ruta_Emp"), DdlDM.SelectedValue, Nz(GwOficinas.Rows(i).Cells(1).Text))
                End If
            Next
            Call BtnCancelarOf_Click(sender, e)
            Call Listar_OficinaxDM()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub GwLista_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GwLista.SelectedIndexChanged

    End Sub
End Class
