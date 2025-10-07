Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Cas_Lista_Enlace_TemasAyuda
    Inherits System.Web.UI.Page
    Protected Sub cmdListarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListarTA.Click
        Call Llenar_Grilla_TA()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Ficha.ActiveTabIndex = "0"
                Ficha_ActiveTabChanged(sender, e)
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
                pCodigo = FlexTA.Rows(i).Cells(6).Text.Trim
                dtListado = obj.CasLista_TemaAyuda(pCodigo,Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = FlexTA.Rows(i)
                        FlexTA.Rows(i).Cells(10).Text = Nu(drMenuItem("TEMA_NOMBRE_DOC")).Length
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
    Protected Sub FlexTA_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexTA.PageIndexChanging
        lblErrorTA.Text = ""
        FlexTA.PageIndex = e.NewPageIndex
        Call Llenar_Grilla_TA()
    End Sub
    Protected Sub FlexTA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTA.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorTA.Text = ""
        If e.CommandName = "Ver" Then
            Response.Clear()
            Response.ContentType = "application/doc"
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + FlexTA.Rows(Index).Cells(3).Text)
            Response.Flush()
            Response.WriteFile("\\data\Temas\" & FlexTA.Rows(Index).Cells(3).Text.Trim)
            Response.End()
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = "0" Then
            Call Llenar_Grilla_TA()
        End If
        If Ficha.ActiveTabIndex = "1" Then
            Call Llenar_Grilla()
        End If
    End Sub
    Protected Sub cmdListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Try
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New ModuloCas
            Dim i As Integer
            Dim pCodigo As Double
            Dim Fila As GridViewRow
            dtListado = obj.CasLista_Enlace(Session("Ruta_Emp"))
            Flex.DataSource = dtListado
            Flex.DataBind()
            dtListado = Nothing
            For i = 0 To Flex.Rows.Count - 1
                pCodigo = Flex.Rows(i).Cells(0).Text.Trim
                dtListado = obj.CasLista_Enlace(pCodigo, Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = Flex.Rows(i)
                        Flex.Rows(i).Cells(3).Text = Nu(drMenuItem("ENLACE_URL")).Length
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Abrir"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='http://" & Nu(drMenuItem("ENLACE_URL")) & "'TARGET='_blank'>" & Nu(drMenuItem("ENLACE_URL")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
End Class
