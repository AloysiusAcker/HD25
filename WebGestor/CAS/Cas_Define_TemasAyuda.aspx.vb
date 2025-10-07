Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Cas_Define_TemasAyuda
    Inherits System.Web.UI.Page
    Protected Sub CmdListarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListarTA.Click
        Call Llenar_Grilla_TA()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Call Llenar_Grilla_TA()
                Call LlenaComboItem("tbopc331", cboTipo)
                Call LlenaComboItem("tbopc332", cboClasif)
            Catch Ex As SqlException
                lblErrorTA.Visible = True
                lblErrorTA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblErrorTA.Visible = True
                lblErrorTA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Private Sub Llenar_Grilla_TA()
        Try
            Dim dtListado As New DataTable
            Dim obj As New ModuloCas
            Dim i As Integer
            Dim pCodigo As Double
            Dim Fila As GridViewRow
            dtListado = obj.CasLista_TemaAyuda(Session("Ruta_Emp"))
            FlexTA.DataSource = dtListado
            FlexTA.DataBind()
            dtListado = Nothing
            For i = 0 To FlexTA.Rows.Count - 1
                pCodigo = FlexTA.Rows(i).Cells(7).Text.Trim
                dtListado = obj.CasLista_TemaAyuda(pCodigo,Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = FlexTA.Rows(i)
                        FlexTA.Rows(i).Cells(11).Text = Nu(drMenuItem("TEMA_NOMBRE_DOC")).Length
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("TEMA_NOMBRE_DOC")) & "'TARGET='_blank'>" & Nu(drMenuItem("TEMA_NOMBRE_DOC")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch Ex As SqlException
            lblErrorTA.Visible = True
            lblErrorTA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorTA.Visible = True
            lblErrorTA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnTANuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTANuevo.Click
        lblEtiqueta.Text = "Ingresar Tema de Ayuda"
        txtTADescripcion.Text = "" : txtTADescripcion.Enabled = True
        cboClasif.Enabled = True : cboClasif.SelectedValue = "< Seleccionar >"
        cboTipo.Enabled = True : cboTipo.SelectedValue = "< Seleccionar >"
        Upload.Enabled = True : btnCancelarTA.Enabled = True : btnGuardarTA.Enabled = True
    End Sub
    Protected Sub BtnCancelarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarTA.Click
        lblEtiqueta.Text = ""
        txtTADescripcion.Text = "" : txtTADescripcion.Enabled = False
        cboClasif.Enabled = False : cboClasif.SelectedValue = "< Seleccionar >"
        cboTipo.Enabled = False : cboTipo.SelectedValue = "< Seleccionar >"
        Upload.Enabled = False : btnCancelarTA.Enabled = False : btnGuardarTA.Enabled = False
    End Sub
    Protected Sub FlexTA_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexTA.PageIndexChanging
        lblErrorTA.Text = ""
        FlexTA.PageIndex = e.NewPageIndex
        Call Llenar_Grilla_TA()
    End Sub
    Protected Sub FlexTA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTA.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorTA.Text = ""
        Dim obj As New ModuloCas
        Dim CmdGlobal As New SqlCommand
        Dim CodTemaAyuda As Double : CodTemaAyuda = 0
        If e.CommandName = "Ver" Then
            Response.Clear()
            Response.ContentType = "application/doc"
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + FlexTA.Rows(Index).Cells(4).Text)
            Response.Flush()
            Response.WriteFile("\\data\Temas\" & FlexTA.Rows(Index).Cells(4).Text.Trim)
            Response.End()
        ElseIf e.CommandName = "Quitar" Then
            CodTemaAyuda = FlexTA.Rows(Index).Cells(7).Text.Trim
            obj.InsUpd_TemaAyuda(CodTemaAyuda, "", "", "", "", HttpContext.Current.User.Identity.Name, "3",Session("Ruta_Emp"))
            Call Llenar_Grilla_TA()
        End If
    End Sub
End Class
