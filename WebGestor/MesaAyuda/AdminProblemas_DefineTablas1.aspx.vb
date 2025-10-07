Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Web.Security
Imports System.IO.FileStream
Imports System.Net
Imports System.Drawing.Imaging
Partial Class AdminProblemas_DefineTablas1
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
                Ficha.ActiveTabIndex = 0
                Ficha.Height = 250
                Ficha_ActiveTabChanged(sender, e)
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
    Private Sub Llenar_Grilla()
        Try
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New clsMesaAyuda
            Dim i As Integer
            Dim pCodigo As Double
            Dim Fila As GridViewRow
            dtListado = obj.MALista_Enlace(Session("Ruta_Emp"), Session("CodEmpresa"))
            Flex.DataSource = dtListado
            Flex.DataBind()
            dtListado = Nothing
            For i = 0 To Flex.Rows.Count - 1
                pCodigo = Flex.Rows(i).Cells(1).Text.Trim
                dtListado = obj.MALista_Enlace(pCodigo, Session("Ruta_Emp"), Session("CodEmpresa"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = Flex.Rows(i)
                        Flex.Rows(i).Cells(4).Text = Nu(drMenuItem("ENLACE_URL")).Length
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
    Protected Sub cmdListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListar.Click
        Call Llenar_Grilla()
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Llenar_Grilla()
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call LlenaComboItem("TBOPC333", cboTipoAviso)
            Call LlenaComboItem("TBOPC334", cboEstadoAviso)
            cboTipoAviso.Items.Add("< Seleccionar >")
            cboEstadoAviso.Items.Add("< Seleccionar >")
            cboTipoAviso.SelectedValue = "< Seleccionar >"
            cboEstadoAviso.SelectedValue = "< Seleccionar >"
            Call Llenar_Grilla_A()
            Ficha.Height = 250
        End If
    End Sub
    Protected Sub cmdListarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListarA.Click
        Call Llenar_Grilla_A()
    End Sub
    Private Sub Llenar_Grilla_A()
        Try
            Dim dtListado As New DataTable
            Dim obj As New clsMesaAyuda
            dtListado = obj.MALista_Aviso("", "", "", "", "1", Session("Ruta_Emp"), Session("CodEmpresa"))
            FlexA.DataSource = dtListado
            FlexA.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexA_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexA.PageIndexChanging
        lblErrorA.Text = ""
        FlexA.PageIndex = e.NewPageIndex
        Call Llenar_Grilla_A()
    End Sub
    Protected Sub btnEGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsMesaAyuda
        Dim pCodigo As Double : pCodigo = 0
        If Len(txtUrl.Text.Trim) = 0 Then lblError.Text = "Falta ingresar la Dirección de la página." : Exit Sub
        If Len(txtDescripcion.Text.Trim) = 0 Then lblError.Text = "Falta ingresar la Descripción." : Exit Sub
        If lblEtiqueta.Text = "Nuevo Enlace" Then
            obj.MAInsUpd_Enlace(pCodigo, txtDescripcion.Text.Trim, txtUrl.Text.Trim, "1", Session("Ruta_Emp"), Session("CodEmpresa"))
        ElseIf lblEtiqueta.Text = "Editar Enlace" Then
            pCodigo = txtCodigo.Text.Trim
            obj.MAInsUpd_Enlace(pCodigo, txtDescripcion.Text.Trim, txtUrl.Text.Trim, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
        End If
        btnECancelar_Click(sender, e)
        cmdListar_Click(sender, e)
    End Sub
    Protected Sub btnENuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoE.Visible = True
        lblEtiqueta.Text = "Nuevo Enlace"
        btnENuevo.Enabled = False
        Ficha.Height = 360
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim Longitud As Integer
        If e.CommandName = "Editar" Then
            lblEtiqueta.Text = "Editar Enlace"
            lblIngresoE.Visible = True
            txtCodigo.Text = Flex.Rows(Index).Cells(1).Text.Trim
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            Longitud = Flex.Rows(Index).Cells(4).Text.Trim
            Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Flex.Rows(Index).FindControl("Abrir"), System.Web.UI.HtmlControls.HtmlGenericControl)
            txtUrl.Text = Mid(lbl.InnerText, 21, Longitud)
            Ficha.Height = 360
        End If
    End Sub
    Protected Sub btnECancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        txtUrl.Text = ""
        lblEtiqueta.Text = ""
        lblIngresoE.Visible = False
        lblError.Text = ""
        btnENuevo.Enabled = True
        Ficha.Height = 250
    End Sub
    Protected Sub btnNuevoAviso_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoAviso.Visible = True
        lblEtiquetaA.Text = "Nuevo Aviso"
        btnNuevoAviso.Enabled = False
        lblErrorA.Text = ""
        cboTipoAviso.SelectedValue = "< Seleccionar >"
        cboEstadoAviso.SelectedValue = "1"
        cboEstadoAviso.Enabled = False
        Ficha.Height = 350
    End Sub
    Protected Sub btnGuardarAviso_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorA.Text = ""
        Dim pCodAviso As Double = 0
        Dim obj As New clsMesaAyuda
        If txtDescripcionAviso.Text.Trim = "" Then lblErrorA.Text = "Debe ingresar la descripción del aviso." : Exit Sub
        If cboTipoAviso.SelectedValue = "< Selecccionar >" Then lblErrorA.Text = "Seleccionar Tipo." : Exit Sub
        If cboEstadoAviso.SelectedValue = "< Selecccionar >" Then lblErrorA.Text = "Seleccionar Estado." : Exit Sub
        Try
            If lblEtiquetaA.Text = "Nuevo Aviso" Then
                obj.MAInsUpd_Aviso(pCodAviso, cboTipoAviso.SelectedValue.Trim, txtDescripcionAviso.Text.Trim, cboEstadoAviso.SelectedValue.Trim, "", "1", Session("Ruta_Emp"), Session("CodEmpresa"))
            ElseIf lblEtiquetaA.Text = "Editar Aviso" Then
                pCodAviso = txtCodAviso.Text.Trim
                obj.MAInsUpd_Aviso(pCodAviso, cboTipoAviso.SelectedValue.Trim, txtDescripcionAviso.Text.Trim, cboEstadoAviso.SelectedValue.Trim, "", "2", Session("Ruta_Emp"), Session("CodEmpresa"))
            End If
        Catch Ex As SqlException
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnCancelarAviso_Click(sender, e)
        cmdListarA_Click(sender, e)
    End Sub
    Protected Sub btnCancelarAviso_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoAviso.Visible = False
        txtDescripcionAviso.Text = ""
        txtCodAviso.Text = ""
        btnNuevoAviso.Enabled = True
        lblErrorA.Text = ""
        FlexA.Enabled = True
        Ficha.Height = 250
    End Sub
    Protected Sub FlexA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexA.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorA.Text = ""
        If e.CommandName = "Editar" Then
            If FlexA.Rows(Index).Cells(8).Text.Trim <> "3" Then
                lblEtiquetaA.Text = "Editar Aviso"
                lblIngresoAviso.Visible = True
                txtCodAviso.Text = FlexA.Rows(Index).Cells(2).Text.Trim
                txtDescripcionAviso.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexA.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                cboTipoAviso.SelectedValue = FlexA.Rows(Index).Cells(3).Text.Trim
                cboEstadoAviso.SelectedValue = FlexA.Rows(Index).Cells(8).Text.Trim
                If cboEstadoAviso.SelectedValue = "0" Then cboEstadoAviso.Enabled = False
                FlexA.Enabled = False
            Else
                lblErrorA.Text = "El Aviso no se puede editar porque ha sido solucionado."
            End If
            Ficha.Height = 350
        ElseIf e.CommandName = "Publicar" Then
            If FlexA.Rows(Index).Cells(8).Text.Trim = "1" Then
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
                txtAvisoDescripcion.Text = FlexA.Rows(Index).Cells(7).Text.Trim
                txtAvisoCodigo.Text = FlexA.Rows(Index).Cells(2).Text.Trim
                cboANivel.SelectedValue = "< Seleccionar >"
                Call Llenar_GrillaUser()
                Ficha.Height = 350
            Else
                lblErrorA.Text = "El Aviso ya ha sido publicado."
            End If
        End If
    End Sub
    Private Sub Llenar_GrillaUser()
        Try
            Dim dtListado As New DataTable
            Dim obj As New clsMesaAyuda
            dtListado = obj.MALista_UsuarioXNivel("", "1", Session("Ruta_Emp"), Session("CodEmpresa"))
            FlexUser.DataSource = dtListado
            FlexUser.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorUser.Visible = True
            lblErrorUser.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorUser.Visible = True
            lblErrorUser.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnARegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    Protected Sub cboANivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim User As CheckBox
        Dim i As Integer
        For i = 0 To FlexUser.Rows.Count - 1
            If FlexUser.Rows(i).Cells(4).Text = cboANivel.SelectedValue.Trim Then
                User = FlexUser.Rows(i).Cells(0).FindControl("chk")
                User.Checked = True
            Else
                User = FlexUser.Rows(i).Cells(0).FindControl("chk")
                User.Checked = False
            End If
        Next
    End Sub
    Protected Sub chkMarcartodo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim User As CheckBox
        Dim i As Integer
        For i = 0 To FlexUser.Rows.Count - 1
            User = FlexUser.Rows(i).Cells(0).FindControl("chk")
            If chkMarcartodo.Checked = True Then User.Checked = True Else User.Checked = False
        Next
    End Sub
    Protected Sub btnPublicar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim a As Integer : a = 0
        Dim obj As New clsMesaAyuda
        lblErrorUser.Text = ""
        Dim User As New CheckBox
        For i = 0 To FlexUser.Rows.Count - 1
            User = FlexUser.Rows(i).Cells(0).FindControl("chk")
            If User.Checked = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblErrorUser.Text = "Debe de marcar al menos un usuario."
        If lblErrorUser.Text <> "" Then
            Exit Sub
        End If
        lblErrorUser.Text = ""
        Try
            For i = 0 To FlexUser.Rows.Count - 1
                User = FlexUser.Rows(i).Cells(0).FindControl("chk")
                If User.Checked = True And User.Enabled = True Then
                    obj.MAInsUpd_PuclicaAviso(txtAvisoCodigo.Text.Trim, FlexUser.Rows(i).Cells(1).Text.Trim, "1", Session("Ruta_Emp"), Session("CodEmpresa"))
                    obj.MAInsUpd_PuclicaAviso(txtAvisoCodigo.Text.Trim, FlexUser.Rows(i).Cells(1).Text.Trim, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
                End If
            Next
        Catch ex As SqlException
            lblErrorUser.Text = ex.Message
        Catch ex As Exception
            lblErrorUser.Text = ex.Message
        Finally
        End Try
        Call Llenar_GrillaUser()
        Call MarcarUser()
    End Sub
    Protected Sub FlexUser_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexUser.PageIndexChanging
        lblErrorA.Text = ""
        FlexA.PageIndex = e.NewPageIndex
        Call Llenar_GrillaUser()
    End Sub
    Private Sub MarcarUser()
        Dim Check As CheckBox
        Dim i As Integer
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn2.Open()
            CmdGlobal2.Connection = Cn2
            CmdGlobal2.CommandText = " SELECT USUARIO,ESTADO,AVISO_NRO " _
                                   & " FROM dbo.TBADMIN_AVISOS_PUBLICA"
            Rs = CmdGlobal2.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    For i = 0 To FlexUser.Rows.Count - 1
                        If FlexUser.Rows(i).Cells(1).Text = Nu(Rs(0)) And txtAvisoCodigo.Text.Trim = Nu(Rs(2)) Then
                            Check = CType(FlexUser.Rows(i).Cells(0).FindControl("chk"), CheckBox)
                            Check.Checked = True
                            Check.Enabled = False
                        End If
                    Next
                End While
            End If
            Rs.Close()
        Catch ex As SqlException
            lblErrorUser.Text = ex.Message
        Catch ex As Exception
            lblErrorUser.Text = ex.Message
        Finally
            Cn2.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
End Class
