Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_Generar
    Inherits System.Web.UI.Page
    Dim ObjProceso As New ClsEval_Proceso
    Dim FnProceso As New clsEval_Proceso_Funciones
    Dim objSeg As New ModuloSeguridad
    Dim objGrupoEmp As New ModuloGeneral
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblCodProceso.Text = ""
            lblError.Text = ""
            lblEtiqueta.Text = ""
            Call FnProceso.CargarDM(DdlBusResponsable, 10, Session("CodEmpresa"), Session("CodGrupoEmpresa"))
            ' Llenar_TipoEval
            Call FnProceso.Llenar_TipoEval(DdlTipoEval, Session("CodEmpresa"), Session("Ruta_Emp"))
            DdlBusResponsable.Items.Add("< Todos >")
            DdlBusResponsable.SelectedValue = Session("User")
            If DdlBusResponsable.SelectedValue <> "< Todos >" Then
                Call Listar_Proceso_xDM(DdlBusResponsable.SelectedValue)
            Else
                Call Listar_Proceso()
            End If
        End If
    End Sub
    Private Sub Listar_Proceso()
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try 'Lista_Evaluacion_xDM
            dt = ObjProceso.Lista_Evaluacion(Session("CodEmpresa"), Session("Ruta_Emp"))
            gwLista.DataSource = dt
            gwLista.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Listar_Proceso_xDM(ByVal psCodDM As String)
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        Try 'Lista_Evaluacion_xDM
            dt = ObjProceso.Lista_Evaluacion_xDM(Session("CodEmpresa"), Session("Ruta_Emp"), psCodDM)
            gwLista.DataSource = dt
            gwLista.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnProgramar_Click(sender As Object, e As EventArgs) Handles BtnProgramar.Click
        divRegistro.Visible = Visible
        txtFecha.Text = FormatoFecha(FechaActual)
        Call FnProceso.Llenar_Proceso(ddlProceso, Session("CodEmpresa"), Session("Ruta_Emp"))
        Call FnProceso.Llenar_Proceso_Check(chkProceso, Session("CodEmpresa"), Session("Ruta_Emp"))
        Call FnProceso.CargarDM(DdlResponsable, 10, Session("CodEmpresa"), Session("CodGrupoEmpresa"))
        If DdlResponsable.Items.FindByValue(Session("User")) IsNot Nothing Then DdlResponsable.SelectedValue = Session("User")
        Call DdlResponsable_SelectedIndexChanged(sender, e)
        lblEtiqueta.Text = "Nueva Evalución"
    End Sub
    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        divRegistro.Visible = False
        ddlProceso.Items.Clear()
        ddlOficina.Items.Clear() : ddlResponsable.Items.Clear()
        ddlOficina.Items.Add("< Seleccionar >")
        ddlOficina.SelectedValue = "< Seleccionar >"
        ddlProceso.Items.Add("< Seleccionar >")
        ddlProceso.SelectedValue = "< Seleccionar >"
        ddlResponsable.Items.Add("< Seleccionar >")
        ddlResponsable.SelectedValue = "< Seleccionar >"
        lblEtiqueta.Text = ""
    End Sub
    Private Sub Llenar_Oficina()
        ddlOficina.Items.Clear()
        ddlOficina.DataSource = ObjProceso.Evaluacion_ListaRelacion_OficinaXDM(Session("CodEmpresa"), Session("Ruta_Emp"), Session("CodGrupoEmpresa"), ddlResponsable.SelectedValue)
        ddlOficina.DataTextField = "c3"
        ddlOficina.DataValueField = "c4"
        ddlOficina.DataBind()
        ddlOficina.Items.Add("< Seleccionar >")
        ddlOficina.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub Llenar_Responsable()
        ddlResponsable.Items.Clear()
        ddlResponsable.DataSource = objSeg.Listar_Usuarios()
        ddlResponsable.DataTextField = "nombre"
        ddlResponsable.DataValueField = "codigo"
        ddlResponsable.DataBind()
        ddlResponsable.Items.Add("< Seleccionar >")
        ddlResponsable.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            lblError.Text = ""
            Dim dt As New DataTable
            Dim CodOficina As Double = 0
            Dim CodProceso As Double = 0
            Dim psResponsable As String = ""
            Dim psFecha As String = ""
            Dim psFechaReg As String = ""
            Dim psHoraReg As String = ""
            Dim psValorsys As String = ""
            Dim psUser As String = Session("User")
            Dim pdCantSelecc As Double = 0
            If chkProceso.Items.Count > 0 Then
                For i = 1 To chkProceso.Items.Count - 1
                    If chkProceso.Items(i).Selected Then
                        pdCantSelecc = pdCantSelecc + 1
                    End If
                Next
            End If
            If pdCantSelecc = 0 Then lblError.Text = "Debe elegir un proceso" : Exit Sub
            If ddlOficina.Text = "< Seleccionar >" Then lblError.Text = "Debe elegir una opficina" : Exit Sub
            If ddlResponsable.Text = "< Seleccionar >" Then lblError.Text = "Debe seleccionar a un responsable" : Exit Sub
            CodOficina = ddlOficina.SelectedValue
            psFecha = Right(txtFecha.Text, 4) & Mid(txtFecha.Text, 4, 2) & Left(txtFecha.Text, 2)
            psFechaReg = Right(FechaActual(), 4) & Mid(FechaActual(), 4, 2) & Left(FechaActual(), 2)
            psHoraReg = Left(HoraActual, 2) & Right(HoraActual, 2)
            psValorsys = psUser & psFechaReg & psFechaReg
            Dim psTipoEval As Integer = 0
            If chkTipo.Checked = True And DdlTipoEval.SelectedValue <> "< Seleccionar >" Then psTipoEval = DdlTipoEval.SelectedValue
            psResponsable = ddlResponsable.SelectedValue
            If lblEtiqueta.Text = "Nueva Evalución" Then
                If chkProceso.Items.Count > 0 Then
                    For i = 1 To chkProceso.Items.Count - 1
                        CodProceso = chkProceso.Items(i).Value
                        If chkProceso.Items(i).Selected Then
                            ObjProceso.Registrar_Evalucion(Session("CodEmpresa"), Session("Ruta_Emp"), CodProceso, CodOficina, psResponsable, psFecha, psFechaReg, psHoraReg, psUser, "1", "0", psValorsys, psTipoEval)
                        End If
                    Next
                End If
            ElseIf lblEtiqueta.Text = "Editar Evalución" Then
                    'CodPagina = txtCodPagina.Text.Trim
                    'objSeg.InsUpd_Pagina(CodPagina, txtPagina.Text.Trim, txtDescripcion.Text.Trim, cboEstado.SelectedValue.Trim, cboTipo.SelectedValue.Trim, cboDisposicion.SelectedValue.Trim, "", CodModulo, HttpContext.Current.User.Identity.Name, "2")
                End If
            Call Listar_Proceso()
            btnCancelar_Click(sender, e)
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
        dt = Nothing
        Try
            Dim psCodProceso As Double = 0
            lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            If e.CommandName = "Editar" Then
                lblCodEval.Text = gwLista.Rows(Index).Cells(4).Text
                lblCodProceso.Text = gwLista.Rows(Index).Cells(0).Text
                ddlOficina.SelectedValue = gwLista.Rows(Index).Cells(0).Text
                ddlProceso.SelectedValue = gwLista.Rows(Index).Cells(0).Text
                ddlResponsable.SelectedValue = gwLista.Rows(Index).Cells(0).Text
                txtFecha.Text = ""
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub DdlResponsable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlResponsable.SelectedIndexChanged
        Call Llenar_Oficina()
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        If DdlBusResponsable.SelectedValue <> "< Todos >" Then
            Call Listar_Proceso_xDM(DdlBusResponsable.SelectedValue)
        Else
            Call Listar_Proceso()
        End If
    End Sub
    Protected Sub DdlBusResponsable_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlBusResponsable.SelectedIndexChanged
        If DdlBusResponsable.SelectedValue <> "< Todos >" Then
            Call Listar_Proceso_xDM(DdlBusResponsable.SelectedValue)
        Else
            Call Listar_Proceso()
        End If
    End Sub
    Protected Sub chkTipo_CheckedChanged(sender As Object, e As EventArgs) Handles chkTipo.CheckedChanged
        If chkTipo.Checked = True Then
            DdlTipoEval.Enabled = True
            DdlTipoEval.SelectedValue = "< Seleccionar >"
        Else
            DdlTipoEval.Enabled = False
            DdlTipoEval.SelectedValue = "< Seleccionar >"
        End If
    End Sub
End Class
