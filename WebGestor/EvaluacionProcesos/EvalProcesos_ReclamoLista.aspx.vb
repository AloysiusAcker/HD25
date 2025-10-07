Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_ReclamoLista
    Inherits System.Web.UI.Page
    Dim ObjProceso As New ClsEval_Proceso
    Dim FnProceso As New clsEval_Proceso_Funciones
    Dim objSeg As New ModuloSeguridad
    Dim objGrupoEmp As New ModuloGeneral
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblRegistro.Text = ""
            lblError.Text = ""
        End If
    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        lblError.Text = ""
        lblRegistro.Text = ""
        Try 'Lista_Evaluacion_xDM
            dt = ObjProceso.Lista_Reclamo(Session("CodEmpresa"), Session("Ruta_Emp"))
            gwLista.DataSource = dt
            gwLista.DataBind()
            lblRegistro.Text = "Se encontraron " & gwLista.Rows.Count & " registros."
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnRegistrar_Click(sender As Object, e As EventArgs) Handles BtnRegistrar.Click
        Response.Redirect("EvalProcesos_Reclamo.aspx")
    End Sub

    Private Sub gwLista_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gwLista.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pCodReclamo As Double = 0
        Dim dt As New DataTable
        If e.CommandName = "Detalle" Then
            pCodReclamo = gwLista.Rows(Index).Cells(2).Text
            Try
                DetalleLista.DataSource = ObjProceso.ListaDetalle_Reclamo(Session("CodEmpresa"), Session("Ruta_Emp"), pCodReclamo)
                DetalleLista.DataBind()
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub

End Class
