Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inspeccion_Lista_OfPendiente
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.Height = 420
            Ficha.ActiveTabIndex = 0
            lblError.Text = ""
            cboVerificado.SelectedValue = "(Seleccionar)"
            cboEstado.SelectedValue = "(Seleccionar)"
            cboTipificacion.Items.Clear()
            Call LlenaComboItem("TBOPC388", cboTipificacion, psCnGrEmp)
            cboTipificacion.Items.Add("< Seleccionar >") : cboTipificacion.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim pdCodOficina As Double = 0
        Dim pVerificado As String = ""
        Dim pEstado As String = ""
        Dim psTipificacion As String = ""
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Text = ""
        If txtUbicacion.Text.Trim <> "" Then pdCodOficina = txtUbicacion.Text.Trim Else pdCodOficina = 0
        If cboVerificado.SelectedValue <> "(Seleccionar)" Then pVerificado = cboVerificado.SelectedValue.Trim
        If cboEstado.SelectedValue <> "(Seleccionar)" Then pEstado = cboEstado.SelectedValue.Trim
        If pdCodOficina = 0 And pVerificado = "" And pEstado = "" And cboTipificacion.SelectedValue = "< Seleccionar >" Then lblError.Text = "Ingresar al menos una busqueda." : Exit Sub
        If cboTipificacion.SelectedValue <> "< Seleccionar >" Then psTipificacion = cboTipificacion.SelectedValue.Trim
        Try
            Flex.DataSource = obj.Lista_CentroCostos(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, "2", pEstado, pdCodOficina, pVerificado, psTipificacion)
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        '
    End Sub
    Private Sub Exportar_Excel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Flex.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(Flex)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=PendienteOficina.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Private Sub Exportar_Excel2(ByVal dt As DataTable)
        Dim StwWriter As New System.IO.StringWriter
        Dim htwWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(StwWriter)
        Dim dgGrid As DataGrid = New DataGrid
        dgGrid.DataSource = dt
        dgGrid.HeaderStyle.Font.Bold = True
        dgGrid.DataBind()
        dgGrid.RenderControl(htwWriter)
        Response.ContentType = "application/vnd.ms-excel"
        Me.EnableViewState = False
        Response.Write(StwWriter.ToString)
        Response.End()
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
    End Sub
    Protected Sub optUbicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUbicacion.Text = ""
        txtUbiCodigo.Text = ""
        txtUbiDescripcion.Text = ""
    End Sub
    Protected Sub txtUbiCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUbicacion.Text = ""
        txtUbiDescripcion.Text = ""
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtUbicacion.Text = ""
            txtUbiCodigo.Text = ""
            txtUbiDescripcion.Text = ""
            txtUbiCodigo.Text = FlexUbicacion.Rows(Index).Cells(1).Text
            txtUbiDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            ModalPopupExtender2.Hide()
        End If
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexUbicacion.DataBind()
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInspeccion_InsUpdDel
        Dim codigo As Double = 0
        Dim fechaProg As String = ""
        Dim sysestado As String = "0"
        Dim user As String = HttpContext.Current.User.Identity.Name
        Dim HoraProg As String = ""
        Dim TiempoProg As String = ""
        Dim opt As Boolean = False
        Dim codOficina As Double = 0
        codOficina = Nz(txtcodOficina.Text.Trim)
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Numero As String : Numero = ""

        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT MAX(INSPEC_CODIGO) FROM TBINV_INSPECCION WHERE EMPRESA_CODIGO='0001'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                Numero = Nz(Rs(0)) + 1
            End While
        Else
            Numero = 1
        End If
        LblNumero.Text = Numero
        If CboTipo.SelectedValue.Trim = "< Seleccionar >" Then lblMensaje.Text = "Falta seleccionar Tipo de Servicio" : Exit Sub
        If cboTecnico.SelectedValue.Trim = "< Seleccionar >" Then lblMensaje.Text = "Falta seleccionar Tipo Persona" : Exit Sub
        If txtTipoPersona.Text.Trim = "" Then lblMensaje.Text = " <br> Ingresar Persona" : Exit Sub
        If txtTipoNombre.Text.Trim = "" Then lblMensaje.Text = " <br> Ingresar Persona" : Exit Sub
        If txtOficina.Text.Trim = "" Then lblMensaje.Text = " <br> Ingresar Oficina" : Exit Sub
        If txtOficinaDesc.Text.Trim = "" Then lblMensaje.Text = " <br> Ingresar Oficina" : Exit Sub
        If txtFechaProgramada.Text.Trim <> "" Then fechaProg = txtFechaProgramada.Text.Trim Else lblMensaje.Text = " <br> Ingresar Fecha Programada" : Exit Sub
        If txtHoraProg.Text.Trim <> "__:__" Then HoraProg = txtHoraProg.Text.Trim Else lblMensaje.Text = " <br> Ingresar Hora Programada" : Exit Sub
        'If txtTiempoProgramado.Text.Trim <> "" Then TiempoProg = txtTiempoProgramado.Text.Trim Else lblMensaje.Text = " <br> Ingresar Tiempo Programado" : Exit Sub
        If cboPrioridad.SelectedValue.Trim = "( Seleccionar )" Then lblMensaje.Text = "Falta Prioridad" : Exit Sub
        fechaProg = Right(txtFechaProgramada.Text.Trim, 4) & Mid(txtFechaProgramada.Text.Trim, 4, 2) & Left(txtFechaProgramada.Text.Trim, 2)
        HoraProg = Left(txtHoraProg.Text.Trim, 2) & Right(txtHoraProg.Text.Trim, 2)
        TiempoProg = Left(txtTiempoProgramado.Text.Trim, 2) & Right(txtTiempoProgramado.Text.Trim, 2)
        Try
            obj.Ins_Inspeccion(psConexion, Session("CodEmpresa"), codigo, Numero, CboTipo.SelectedValue.Trim, fechaProg, _
                 HoraProg, txtTecnico.Text.Trim, codOficina, "1", "0", cboTecnico.SelectedValue.Trim, txtObservacion.Text.Trim, _
                 TiempoProg, user, txtObjetivo.Text.Trim, txtDescrip.Text.Trim, cboPrioridad.SelectedValue.Trim, cboMotivo.SelectedValue.Trim, 0, "NO")
            lblMensaje.Text = "Servicio Nro " + LblNumero.Text + " Registrado"
            Call Limpiar()
        Catch ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            lblMensaje.Text = "Ha ocurrido un error en la aplicación:" & ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Limpiar()
    End Sub
    Private Sub Limpiar()
        txtnumero.Text = ""
        txtFechaProgramada.Text = ""
        txtHoraProg.Text = "__:__"
        txtObservacion.Text = ""
        txtTecnico.Text = ""
        txtTipoNombre.Text = ""
        txtTecnico.Text = ""
        txtTiempoProgramado.Text = "__:__"
        txtTipoPersona.Text = ""
        txtObservacion.Text = ""
        txtObjetivo.Text = ""
        txtDescrip.Text = ""
        CboTipo.SelectedValue = "< Seleccionar >"
        cboTecnico.SelectedValue = "< Seleccionar >"
        cboPrioridad.SelectedValue = "( Seleccionar )"
        cboMotivo.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInv_Listados
        Dim pdCodOficina As Double = 0
        If e.CommandName = "Registrar" Then
            Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            txtHoraProg.Text = "__:__"
            txtHoraProg.Enabled = True
            txtTiempoProgramado.Text = "__:__"
            txtTiempoProgramado.Enabled = True
            cboTecnico.ClearSelection() : Call LlenaComboItem("TBOPC378", cboTecnico, psCnGrEmp)
            CboTipo.ClearSelection() : Call LlenaComboItem("TBOPC381", CboTipo, psCnGrEmp)
            cboMotivo.ClearSelection() : Call LlenaComboItem("TBOPC386", cboMotivo, psCnGrEmp)
            CboTipo.Items.Add("< Seleccionar >") : CboTipo.SelectedValue = "< Seleccionar >"
            cboTecnico.Items.Add("< Seleccionar >") : cboTecnico.SelectedValue = "< Seleccionar >"
            cboMotivo.Items.Add("< Seleccionar >") : cboMotivo.SelectedValue = "< Seleccionar >"
            Call Limpiar()
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.Height = 500
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            txtcodOficina.Text = Flex.Rows(Index).Cells(12).Text
            txtOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtOficinaDesc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If txtcodOficina.Text.Trim <> "" Then pdCodOficina = txtcodOficina.Text.Trim Else pdCodOficina = 0
            FlexROficina.DataSource = obj.Lista_CentroCostos(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, "2", "", pdCodOficina, "", "")
            FlexROficina.DataBind()
        End If
    End Sub
    Protected Sub btnListarTipoPers_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblMensaje.Text = ""
        If cboTecnico.SelectedValue.Trim = "3" Then
            Call listarTipoPersonaXProveedor()
        Else
            Call listarTipoPersonaXTecnico()
        End If
        ModalPopupExtender1.Show()
    End Sub
    Private Sub listarTipoPersonaXProveedor()
        Dim obj As New clsInspeccion_Listado
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexTipoPers.DataSource = obj.Lista_TipoPersona(psConexion, Session("CodEmpresa"), txtRuc.Text.Trim, txtRazonSocial.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            lblMensaje.Text = "Ha ocurrido un error en la aplicación:" & ex.Message
        End Try
    End Sub
    Private Sub listarTipoPersonaXTecnico()
        Dim obj As New clsInspeccion_Listado
        Dim psCodGrupoEmp As Double = 0
        psCodGrupoEmp = Session("CodGrupoEmpresa")
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexTipoPers.DataSource = obj.Lista_TipoPersonaTecnico(psConexion, psCodGrupoEmp, Session("CodEmpresa"), txtRuc.Text.Trim, txtRazonSocial.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            lblMensaje.Text = "Ha ocurrido un error en la aplicación:" & ex.Message
        End Try
    End Sub
    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTipoPers.RowCommand
        Try
            lblMensaje.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtTecnico.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtTipoNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                FlexTipoPers.DataSource = Nothing
                FlexTipoPers.DataBind()
                txtRuc.Text = ""
                txtRazonSocial.Text = ""
                ModalPopupExtender1.Hide()
            End If
        Catch ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch ex As Exception
            lblMensaje.Text = "Ha ocurrido un error en la aplicación:" & ex.Message
        End Try
    End Sub
    Protected Sub FlexTipoPers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Limpiar()
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.Height = 420
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
    End Sub
End Class
