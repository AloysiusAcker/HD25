Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Mantenimiento_TablasEspeciales_Activar
    Inherits System.Web.UI.Page
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        lblNuevaTabla.Visible = False
    End Sub
    Protected Sub btnGrabaNuevaTabla_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objIns As New ModuloGeneral
        If cboUsoTabla.SelectedValue = "Externo" Then txtUso.Text = "1" Else txtUso.Text = "0"
        Try
            If opcTablas.SelectedValue.Trim = 0 Then
                If cboVerTabla.SelectedValue = "SI" Then
                    objIns.Update_TablasEspeciales_Uso(txtCodTabla.Text.Trim, txtUso.Text.Trim, "S")
                ElseIf cboVerTabla.SelectedValue = "NO" Then
                    objIns.Update_TablasEspeciales_Uso(txtCodTabla.Text.Trim, txtUso.Text.Trim, "N")
                End If
            ElseIf opcTablas.SelectedValue.Trim = 1 Then
                If cboVerTabla.SelectedValue = "SI" Then
                    objIns.Update_TablasEspeciales_Uso(txtCodTabla.Text.Trim, txtUso.Text.Trim, "S")
                ElseIf cboVerTabla.SelectedValue = "NO" Then
                    objIns.Update_TablasEspeciales_Uso(txtCodTabla.Text.Trim, txtUso.Text.Trim, "N")
                End If
            End If
            txtVerTabla.Text = ""
            Call opcTablas_SelectedIndexChanged(sender, e)
            lblNuevaTabla.Visible = False
            lblRegistroTabla.Visible = True
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim dRow As DataRow
        dt.Columns.Add("TABLAS_CODIGO")
        dt.Columns.Add("TABLAS_DESCRIPCION")
        dt.Columns.Add("TABLAS_SYS_EST")
        dt.Columns.Add("TABLAS_VER")
        Dim objList As New ModuloGeneral
        If Not Page.IsPostBack Then
            Try
                dtLista = objList.Listado_TablasEspeciales_Uso("", "1")
                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows
                        dRow = dt.NewRow
                        dRow("TABLAS_CODIGO") = Nu(dr("TABLAS_CODIGO"))
                        dRow("TABLAS_DESCRIPCION") = Nu(dr("TABLAS_DESCRIPCION"))
                        dRow("TABLAS_SYS_EST") = Nu(dr("TABLAS_SYS_EST"))
                        If Nu(dr("TABLAS_VER")) = "S" Or Nu(dr("TABLAS_VER")) = "s" Then
                            dRow("TABLAS_VER") = "SI"
                        ElseIf Nu(dr("TABLAS_VER")) = "N" Or Nu(dr("TABLAS_VER")) = "n" Then
                            dRow("TABLAS_VER") = "NO"
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
                FlexTablasEspeciales.DataSource = dt
                FlexTablasEspeciales.DataBind()
                lblRegistroTabla.Text = "Se encontrarón " & FlexTablasEspeciales.Rows.Count & " registros."
            Catch ex As SqlException
                lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
            Catch ex As Exception
                lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
            End Try
        End If
    End Sub
    Protected Sub opcTablas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles opcTablas.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim dRow As DataRow
        FlexTablasEspeciales.DataSource = Nothing
        FlexTablasEspeciales.DataBind()
        dt.Columns.Add("TABLAS_CODIGO")
        dt.Columns.Add("TABLAS_DESCRIPCION")
        dt.Columns.Add("TABLAS_SYS_EST")
        dt.Columns.Add("TABLAS_VER")
        Dim objList As New ModuloGeneral
        Try
            If opcTablas.SelectedValue.Trim = 0 Then
                dtLista = objList.Listado_TablasEspeciales_Uso("", "1")
                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows
                        dRow = dt.NewRow
                        dRow("TABLAS_CODIGO") = Nu(dr("TABLAS_CODIGO"))
                        dRow("TABLAS_DESCRIPCION") = Nu(dr("TABLAS_DESCRIPCION"))
                        dRow("TABLAS_SYS_EST") = Nu(dr("TABLAS_SYS_EST"))
                        If Nu(dr("TABLAS_VER")) = "S" Or Nu(dr("TABLAS_VER")) = "s" Then
                            dRow("TABLAS_VER") = "SI"
                        ElseIf Nu(dr("TABLAS_VER")) = "N" Or Nu(dr("TABLAS_VER")) = "n" Then
                            dRow("TABLAS_VER") = "NO"
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
                FlexTablasEspeciales.DataSource = dt
                FlexTablasEspeciales.DataBind()
                lblRegistroTabla.Text = "Se encontrarón " & FlexTablasEspeciales.Rows.Count & " registros."
                lblNuevaTabla.Visible = False
            ElseIf opcTablas.SelectedValue.Trim = 1 Then
                dtLista = objList.Listado_TablasEspeciales_Uso("", "0")
                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows
                        dRow = dt.NewRow
                        dRow("TABLAS_CODIGO") = Nu(dr("TABLAS_CODIGO"))
                        dRow("TABLAS_DESCRIPCION") = Nu(dr("TABLAS_DESCRIPCION"))
                        dRow("TABLAS_SYS_EST") = Nu(dr("TABLAS_SYS_EST"))
                        If Nu(dr("TABLAS_VER")) = "S" Or Nu(dr("TABLAS_VER")) = "s" Then
                            dRow("TABLAS_VER") = "SI"
                        ElseIf Nu(dr("TABLAS_VER")) = "N" Or Nu(dr("TABLAS_VER")) = "n" Then
                            dRow("TABLAS_VER") = "NO"
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
                FlexTablasEspeciales.DataSource = dt
                FlexTablasEspeciales.DataBind()
                lblRegistroTabla.Text = "Se encontrarón " & FlexTablasEspeciales.Rows.Count & " registros."
                lblNuevaTabla.Visible = False
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub FlexTablasEspeciales_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim objList As New clsInspeccion_Listado
        Dim objIns As New Insertar
        Try
            If e.CommandName = "Editar" Then
                lblNuevaTabla.Visible = True
                lblRegistroTabla.Text = ""
                txtCodTabla.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasEspeciales.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtDescTabla.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasEspeciales.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                cboVerTabla.SelectedValue = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasEspeciales.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtVerTabla.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasEspeciales.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                If opcTablas.SelectedValue.Trim = 0 Then
                    cboUsoTabla.SelectedValue = "Externo"
                ElseIf opcTablas.SelectedValue.Trim = 1 Then
                    cboUsoTabla.SelectedValue = "Interno"
                End If
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        End Try
    End Sub
End Class
