Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Mantenimiento_TablasEspeciales
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.Height = 550
            Ficha.ActiveTabIndex = 0
            Ficha.Height = 400
            Dim objList As New ModuloGeneral
            Try
                FlexTablasExternas.DataSource = objList.Listado_TablasEspeciales("", "1")
                FlexTablasExternas.DataBind()
                lblRegistroTabla.Text = "Se encontrarón " & FlexTablasExternas.Rows.Count & " elementos."
            Catch ex As Exception

            End Try
        End If
    End Sub
    Protected Sub FlexTablasExternas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim objList As New ModuloGeneral
        Dim objIns As New ModuloGeneral
        Try
            If e.CommandName = "Mant" Then
                txtEditar.Text = ""
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
                Ficha.ActiveTabIndex = 1
                txtCodMant.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasExternas.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDescMant.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasExternas.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                FlexEmentosTablas.DataSource = objList.Listado_TablasElementos(txtCodMant.Text)
                FlexEmentosTablas.DataBind()
                LblRegistroElemento.Text = "Se encontrarón " & FlexEmentosTablas.Rows.Count & " elementos."
                LblRegistroElemento.Visible = True
                Ficha.Height = 400
            ElseIf e.CommandName = "Editar" Then
                Ficha.Height = 500
                txtEditar.Text = "Editar"
                lblNuevaTabla.Visible = True
                txtCodTabla.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasExternas.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDescTabla.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasExternas.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            ElseIf e.CommandName = "Borrar" Then
                Ficha.Height = 400
                txtCodTablaBorrarLog.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTablasExternas.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If opcTablas.SelectedIndex = 0 Then
                    objIns.Update_TablasEspeciales(txtCodTablaBorrarLog.Text.Trim, "", "1", "", "", "1", "S", HttpContext.Current.User.Identity.Name)
                ElseIf opcTablas.SelectedIndex = 1 Then
                    objIns.Update_TablasEspeciales(txtCodTablaBorrarLog.Text.Trim, "", "0", "", "", "1", "S", HttpContext.Current.User.Identity.Name)
                End If
                Call opcTablas_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnRegresarTablas_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0
        txtCodElem.Text = ""
        txtDesElem.Text = ""
        txtVal1Elem.Text = ""
        txtVal2Elem.Text = ""
        txtCodTabla.Text = ""
        txtDescTabla.Text = ""
        txtCodElem.Enabled = False
        txtDesElem.Enabled = False
        txtVal1Elem.Enabled = False
        txtVal2Elem.Enabled = False
        lblEditarElementos.Visible = False
        lblNuevaTabla.Visible = False
        LblRegistroElemento.Text = ""
        Ficha.Height = 400
    End Sub
    Protected Sub btnNuevaTabla_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblNuevaTabla.Visible = True
        lblRegistroTabla.Visible = True
        txtCodTabla.Text = ""
        txtDescTabla.Text = ""
        txtEditar.Text = ""
        Ficha.Height = 500
        Dim dt As New DataTable
        Dim objList As New ModuloGeneral
        Try
            If opcTablas.SelectedIndex = 0 Then
                dt = objList.Listado_TablasEspeciales("", "1")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        txtCodTabla.Text = dr("ULTIMO")
                        Exit Sub
                    Next
                End If
            ElseIf opcTablas.SelectedIndex = 1 Then
                dt = objList.Listado_TablasEspeciales("", "0")
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        txtCodTabla.Text = dr("ULTIMO")
                        Exit Sub
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblNuevaTabla.Visible = False
        txtCodTabla.Text = ""
        txtDescTabla.Text = ""
        Ficha.Height = 400
    End Sub
    Protected Sub btnGrabaNuevaTabla_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objIns As New ModuloGeneral
        If txtEditar.Text = "Editar" Then
            Try
                If opcTablas.SelectedIndex = 0 Then
                    objIns.Update_TablasEspeciales(txtCodTabla.Text.Trim, txtDescTabla.Text.Trim, "1", "", "", "0", "S", HttpContext.Current.User.Identity.Name)
                ElseIf opcTablas.SelectedIndex = 1 Then
                    objIns.Update_TablasEspeciales(txtCodTabla.Text.Trim, txtDescTabla.Text.Trim, "0", "", "", "0", "S", HttpContext.Current.User.Identity.Name)
                End If
            Catch ex As Exception

            End Try
        Else
            Try
                If opcTablas.SelectedIndex = 0 Then
                    objIns.Insertar_TablasEspeciales("", txtDescTabla.Text.Trim, "1", "", "", "N", HttpContext.Current.User.Identity.Name)
                ElseIf opcTablas.SelectedIndex = 1 Then
                    objIns.Insertar_TablasEspeciales("", txtDescTabla.Text.Trim, "0", "", "", "N", HttpContext.Current.User.Identity.Name)
                End If
            Catch ex As Exception

            End Try
        End If
        txtCodTabla.Text = ""
        txtDescTabla.Text = ""
        lblRegistroTabla.Visible = True
        lblNuevaTabla.Visible = False
        Call opcTablas_SelectedIndexChanged(sender, e)
    End Sub
    Protected Sub btnGuardarElem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objIns As New ModuloGeneral
        Dim objList As New ModuloGeneral
        Dim dt As New DataTable
        If txtEditarElem.Text = "Editar" Then
            Try
                objIns.Update_TablasElementos(txtCodMant.Text.Trim, txtCodElem.Text.Trim, _
                             txtDesElem.Text.Trim, "", "", "0", txtVal1Elem.Text.Trim, _
                             txtVal2Elem.Text.Trim, HttpContext.Current.User.Identity.Name, txtCodigoElemen.Text.Trim)
                FlexEmentosTablas.DataSource = objList.Listado_TablasElementos(txtCodMant.Text)
                FlexEmentosTablas.DataBind()
                LblRegistroElemento.Text = "Se encontrarón " & FlexEmentosTablas.Rows.Count & " elementos."
                lblEditarElementos.Visible = True
            Catch ex As Exception

            End Try
        Else
            dt = objList.Listado_TablasElementos(txtCodMant.Text)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If Nu(dr("ELEMEN_CODIGO")) = txtCodElem.Text.Trim Then
                        LblRegistroElemento.Text = "Codigo Existente"
                        Exit Sub
                    End If
                Next
            End If
            Try
                objIns.Insertar_TablasElementos(txtCodMant.Text.Trim, txtCodElem.Text.Trim, _
                txtDesElem.Text.Trim, "", "", txtVal1Elem.Text.Trim, _
                txtVal2Elem.Text.Trim, HttpContext.Current.User.Identity.Name)
                FlexEmentosTablas.DataSource = objList.Listado_TablasElementos(txtCodMant.Text)
                FlexEmentosTablas.DataBind()
                LblRegistroElemento.Text = "Se encontrarón " & FlexEmentosTablas.Rows.Count & " elementos."
                lblEditarElementos.Visible = True
            Catch ex As Exception
            End Try
        End If
        txtCodElem.Text = ""
        txtDesElem.Text = ""
        txtVal1Elem.Text = ""
        txtVal2Elem.Text = ""
        LblRegistroElemento.Visible = True
        txtCodElem.Enabled = False
        txtDesElem.Enabled = False
        txtVal1Elem.Enabled = False
        txtVal2Elem.Enabled = False
        btnCancelarElem_Click(sender, e)
    End Sub
    Protected Sub btnCancelarElem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtCodElem.Text = ""
        txtDesElem.Text = ""
        txtVal1Elem.Text = ""
        txtVal2Elem.Text = ""
        lblEditarElementos.Visible = False
        Ficha.Height = 400
    End Sub
    Protected Sub opcTablas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objList As New ModuloGeneral
        FlexTablasExternas.DataSource = Nothing
        FlexTablasExternas.DataBind()
        Try
            If opcTablas.SelectedIndex = 0 Then
                FlexTablasExternas.DataSource = objList.Listado_TablasEspeciales("", "1")
                FlexTablasExternas.DataBind()
                lblRegistroTabla.Text = "Se encontrarón " & FlexTablasExternas.Rows.Count & " registros."
                'lblRegistroTabla.Text = ""
                lblNuevaTabla.Visible = False
            ElseIf opcTablas.SelectedIndex = 1 Then
                FlexTablasExternas.DataSource = objList.Listado_TablasEspeciales("", "0")
                FlexTablasExternas.DataBind()
                lblRegistroTabla.Text = "Se encontrarón " & FlexTablasExternas.Rows.Count & " registros."
                'lblRegistroTabla.Text = ""
                lblNuevaTabla.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub btnNuevoElemento_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtCodElem.Enabled = True
        txtDesElem.Enabled = True
        txtVal1Elem.Enabled = True
        txtVal2Elem.Enabled = True
        txtEditarElem.Text = ""
        lblEditarElementos.Visible = True
        lblEtElemento.Text = "Ingresar Elemento"
        Ficha.Height = 600
    End Sub
    Protected Sub FlexEmentosTablas_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        txtCodElem.Enabled = True
        txtDesElem.Enabled = True
        txtVal1Elem.Enabled = True
        txtVal2Elem.Enabled = True
        txtEditarElem.Text = ""
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim objList As New ModuloGeneral
        Dim objIns As New ModuloGeneral
        If e.CommandName = "Editar" Then
            Ficha.Height = 600
            lblEtElemento.Text = "Editar Elemento"
            txtEditarElem.Text = "Editar"
            lblEditarElementos.Visible = True
            Try
                txtCodElem.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEmentosTablas.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDesElem.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEmentosTablas.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtVal1Elem.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEmentosTablas.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtVal2Elem.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEmentosTablas.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtCodigoElemen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEmentosTablas.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Catch ex As Exception

            End Try
        ElseIf e.CommandName = "Borrar" Then
            Ficha.Height = 400
            txtCodElemBorrar.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEmentosTablas.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Try
                objIns.Update_TablasElementos(txtCodMant.Text.Trim, "", _
                                             "", "", "", "1", "", _
                                             "", HttpContext.Current.User.Identity.Name, txtCodElemBorrar.Text.Trim)
                FlexEmentosTablas.DataSource = objList.Listado_TablasElementos(txtCodMant.Text)
                FlexEmentosTablas.DataBind()
                LblRegistroElemento.Text = "Se encontrarón " & FlexTablasExternas.Rows.Count & " registros."
            Catch ex As Exception
            End Try
        ElseIf e.CommandName = "BorradoFisico" Then
            Ficha.Height = 400
            txtCodElemBorrar.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEmentosTablas.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Try
                objIns.Delete_TablasElementos(txtCodMant.Text.Trim, txtCodElemBorrar.Text.Trim)
                FlexEmentosTablas.DataSource = objList.Listado_TablasElementos(txtCodMant.Text)
                FlexEmentosTablas.DataBind()
                LblRegistroElemento.Text = "Se encontrarón " & FlexTablasExternas.Rows.Count & " registros."
            Catch ex As Exception
            End Try
        End If
    End Sub

End Class
