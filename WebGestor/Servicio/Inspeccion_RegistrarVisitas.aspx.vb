Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Inspeccion_RegistrarVisitas
    Inherits System.Web.UI.Page
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
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
        'txtcodOficina.Text = ""
        txtOficina.Text = ""
        txtOficinaDesc.Text = ""
        txtTiempoProgramado.Text = "__:__"
        txtTipoPersona.Text = ""
        txtObservacion.Text = ""
        txtObjetivo.Text = ""
        txtDescrip.Text = ""
        CboTipo.SelectedValue = "< Seleccionar >"
        cboTecnico.SelectedValue = "< Seleccionar >"
        cboPrioridad.SelectedValue = "< Seleccionar >"
        cboMotivo.SelectedValue = "< Seleccionar >"
        txtOficina.BackColor = Drawing.Color.White
        txtOficinaDesc.BackColor = Drawing.Color.White
        Flex.DataSource = Nothing
        Flex.DataBind()
        lblPendiente.Visible = False
        lblEtiqPendiente.Visible = False
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
        If cboPrioridad.SelectedValue.Trim = "< Seleccionar >" Then lblMensaje.Text = "Falta Prioridad" : Exit Sub
        fechaProg = Right(txtFechaProgramada.Text.Trim, 4) & Mid(txtFechaProgramada.Text.Trim, 4, 2) & Left(txtFechaProgramada.Text.Trim, 2)
        HoraProg = Left(txtHoraProg.Text.Trim, 2) & Right(txtHoraProg.Text.Trim, 2)
        TiempoProg = Left(txtTiempoProgramado.Text.Trim, 2) & Right(txtTiempoProgramado.Text.Trim, 2)
        Try
            obj.Ins_Inspeccion(psConexion, Session("CodEmpresa"), codigo, Numero, CboTipo.SelectedValue.Trim, fechaProg, _
                 HoraProg, txtTecnico.Text.Trim, codOficina, "1", "0", cboTecnico.SelectedValue.Trim, txtObservacion.Text.Trim, _
                 TiempoProg, user, txtObjetivo.Text.Trim, txtDescrip.Text.Trim, cboPrioridad.SelectedValue.Trim, cboMotivo.SelectedValue.Trim, 0, "NO")
            lblMensaje.Text = "Servicio Nro " + LblNumero.Text + " Registrado"
            Call Limpiar()
        Catch ex As Exception
            lblMensaje.Text = "Error"
        Finally
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.Title = "Servicio Registrar"
            Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
            txtHoraProg.Text = "__:__"
            txtHoraProg.Enabled = True
            txtTiempoProgramado.Text = "__:__"
            txtTiempoProgramado.Enabled = True
            Call LlenaComboItem("TBOPC378", cboTecnico, psCnGrEmp)
            Call LlenaComboItem("TBOPC381", CboTipo, psCnGrEmp)
            Call LlenaComboItem("TBOPC386", cboMotivo, psCnGrEmp)
            CboTipo.Items.Add("< Seleccionar >") : CboTipo.SelectedValue = "< Seleccionar >"
            cboTecnico.Items.Add("< Seleccionar >") : cboTecnico.SelectedValue = "< Seleccionar >"
            cboMotivo.Items.Add("< Seleccionar >") : cboMotivo.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnListarOf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarOf.Click
        Dim obj As New clsInv_Listados
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexOf.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCodigo.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexOf.DataBind()
            ModalPopupExtender1.Show()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub FlexOf_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim obj As New clsInv_Listados
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            Dim pdCodOficina As Double = 0
            If e.CommandName = "Aceptar" Then
                Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
                txtOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOf.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtOficinaDesc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOf.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtcodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOf.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                FlexOf.DataSource = Nothing
                FlexOf.DataBind()
                txtBusCodigo.Text = ""
                txtBusDescripcion.Text = ""
                If txtcodOficina.Text.Trim <> "" Then pdCodOficina = txtcodOficina.Text.Trim Else pdCodOficina = 0
                Flex.DataSource = obj.Lista_CentroCostos(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, "2", "1", pdCodOficina, "", "")
                Flex.DataBind()
                If Flex.Rows.Count > 0 Then
                    lblPendiente.Visible = True
                    lblEtiqPendiente.Visible = True
                    txtOficina.BackColor = Drawing.Color.Red
                    txtOficinaDesc.BackColor = Drawing.Color.Red
                Else
                    lblPendiente.Visible = False
                    lblEtiqPendiente.Visible = False
                    txtOficina.BackColor = Drawing.Color.White
                    txtOficinaDesc.BackColor = Drawing.Color.White
                End If
                ModalPopupExtender1.Hide()
            End If
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        '
    End Sub
    Protected Sub btnListarTipoPers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarTipoPers.Click
        If cboTecnico.SelectedValue.Trim = "3" Then
            listarTipoPersonaXProveedor()
        Else
            listarTipoPersonaXTecnico()
        End If
        ModalPopupExtender2.Show()
    End Sub
    Private Sub listarTipoPersonaXProveedor()
        Dim obj As New clsInspeccion_Listado
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexTipoPers.DataSource = obj.Lista_TipoPersona(psConexion, Session("CodEmpresa"), txtRuc.Text.Trim, txtRazonSocial.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub

    Private Sub listarTipoPersonaXTecnico()
        Dim obj As New clsInspeccion_Listado
        Dim psCodGrupoEmp As Double = 0
        psCodGrupoEmp = Session("CodGrupoEmpresa") 'psCodGrupoEmp, Session("CodEmpresa"),
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexTipoPers.DataSource = obj.Lista_TipoPersonaTecnico(psConexion, psCodGrupoEmp, Session("CodEmpresa"), txtRuc.Text.Trim, txtRazonSocial.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnBuscarTipoPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarTipoPersona.Click
        'Dim obj As New clsInspeccion_Listado
        'Try
        '    FlexTipoPers.DataSource = obj.Lista_TipoPersona(Session("Ruta_Emp"), Session("CodEmpresa"), txtRuc.Text.Trim, txtRazonSocial.Text.Trim)
        '    FlexTipoPers.DataBind()
        'Catch ex As SqlException
        '    'lblError.Text = ex.Message
        'Catch ex As Exception
        '    'lblError.Text = ex.Message
        'End Try
    End Sub
    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTipoPers.RowCommand
        Try
            'lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                'txtCodIntOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOf.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtTecnico.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtTipoNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                FlexTipoPers.DataSource = Nothing
                FlexTipoPers.DataBind()
                txtRuc.Text = ""
                txtRazonSocial.Text = ""
                ModalPopupExtender2.Hide()
            End If
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub

    Protected Sub cboTecnico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
End Class
