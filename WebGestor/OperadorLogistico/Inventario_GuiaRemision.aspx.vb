Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_GuiaRemision
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New clsInv_Listados
        lblError.Text = ""
        Dim FechaEntrega : FechaEntrega = ""
        Dim FechaFin : FechaFin = ""
        FechaEntrega = Right(txtFecha.Text, 4) + Mid(txtFecha.Text, 4, 2) + Left(txtFecha.Text, 2)
        FechaFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        Dim pCodCourier As Double : pCodCourier = 0
        If txtCurrierRuc.Text <> "" Then
            If lblCodCurrier.Text <> "" Then pCodCourier = lblCodCurrier.Text
        Else
            pCodCourier = 0
        End If
        Try
            Flex.DataSource = obj.Lista_GuiaxCourier(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, pCodCourier, HttpContext.Current.User.Identity.Name, FechaFin)
            Flex.DataBind()
            lblRegistro.Text = obj.Lista_GuiaxCourier(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, pCodCourier, HttpContext.Current.User.Identity.Name, FechaFin).Rows.Count
            lblRegistro.Text = "Registros Encontrados : " & lblRegistro.Text
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim obj As New clsInventario_Listado
            'Dim dt As DataTable
            txtFecha.Text = ""
            'lblEstEnProceso.Visible = False
            'lblEstEntrega.Visible = False
            'dt = obj.Extrae_Curier(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
            'If dt.Rows.Count = 1 Then
            '    For Each drMenuItem As Data.DataRow In dt.Rows
            '        txtCurrierRuc.Text = Nu(drMenuItem("CURRIER_RUC"))
            '        txtCurrierRS.Text = Nu(drMenuItem("CURRIER_RAZONSOCIAL"))
            '        lblCodCurrier.Text = Nz(drMenuItem("CURRIER_CODIGO"))
            '    Next
            'Else
            lblCodCurrier.Text = 0
            'End If
            'dt = Nothing
            Call btnListar_Click(sender, e)
        End If
    End Sub
    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInv_Listados
        lblError.Text = ""
        txtFecha.Text = ""
        lblEstEnProceso.Visible = False
        lblEstEntrega.Visible = False
        If e.CommandName = "IngEstado" Then
            Try
                If Flex.Rows(Index).Cells(15).Text = "2" Then lblError.Text = "Ya se entregó el Pedido." : Exit Sub
                If Flex.Rows(Index).Cells(15).Text = "3" Then lblError.Text = "El Pedido ha sido rechazado." : Exit Sub
                If Flex.Rows(Index).Cells(15).Text = "8" Then lblError.Text = "El Pedido no ha sido entregado." : Exit Sub
                Flex.Enabled = False
                txtPerAsignada.Text = ""
                txtHora.Text = ""
                txtFecReprog1.Text = ""
                txtHoraReprog.Text = ""
                txtObs.Text = ""
                txtCodLiquida.Text = ""
                lblIngresarFecha.Visible = True
                lblError.Text = ""
                txtFecha.Text = ""
                lblEtiqueta.Text = "Cambiar Estado de Entrega de la Guía de Remisión"
                txtCodGuia.Text = Flex.Rows(Index).Cells(13).Text
                txtSerieGuia.Text = Flex.Rows(Index).Cells(5).Text.Replace("&nbsp;", "")
                txtNroGuia.Text = Flex.Rows(Index).Cells(6).Text.Replace("&nbsp;", "")
                txtDestinatario.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtCodDestino.Text = Flex.Rows(Index).Cells(8).Text.Replace("&nbsp;", "")
                txtEstado.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtCodPedido.Text = Flex.Rows(Index).Cells(14).Text
                txtCodEstado.Text = Flex.Rows(Index).Cells(15).Text
                txtCodCurrier.Text = Flex.Rows(Index).Cells(18).Text
                Call LlenaComboItem("TBOPC400", cboEstado)
                cboEstado.Items.Add("< Seleccionar >") : cboEstado.SelectedValue = "< Seleccionar >"
            Catch ex As SqlException
                lblError.Text = ex.Message
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
            End Try
        End If
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Flex.Enabled = True
        lblIngresarFecha.Visible = False
        lblError.Text = ""
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        System.Threading.Thread.Sleep(100)
        lblError.Text = ""
        If cboEstado.SelectedValue = "8" Then
            If txtCodEstado.Text <> "5" Then
                lblError.Text = "El estado anterior debe estar reagendado por segunda vez." : Exit Sub
            End If
        End If
        If cboEstado.SelectedValue = "< Seleccionar >" Then
            lblError.Text = "Seleccionar Estado de entrega" : Exit Sub
        End If
        If cboEstado.SelectedValue = "6" Then
            If txtPerAsignada.Text = "" Then lblError.Text = "Ingresar la persona asignada." : Exit Sub
            If txtHora.Text = "" Then lblError.Text = "Ingresar la hora de salida al cliente." : Exit Sub
        End If
        If cboEstado.SelectedValue = "2" Then
            If cboPai101.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Tipo de Llamada." : Exit Sub
        End If
        Dim CodGuia As Double : CodGuia = 0
        Dim CodCurrier As Double = 0
        CodGuia = txtCodGuia.Text.Trim
        CodCurrier = txtCodCurrier.Text.Trim
        Dim obj As New clsInv_Listados
        Dim objInsUpdDel As New clsInv_InsUpdDel
        Dim PerAsignada As String : PerAsignada = ""
        Dim HoraSalida As String : HoraSalida = ""
        Dim FechaProg As String : FechaProg = ""
        Dim TipoObs As String : TipoObs = ""
        Dim Obs As String : Obs = ""
        Dim CodLiquidacion As String : CodLiquidacion = ""
        Dim CodPedido As Double : CodPedido = 0
        Dim TipoLlamada As String : TipoLlamada = ""
        PerAsignada = txtPerAsignada.Text.Trim
        CodLiquidacion = txtCodLiquida.Text.Trim
        CodPedido = txtCodPedido.Text.Trim
        If cboEstado.SelectedValue.Trim = "6" Then HoraSalida = txtHora.Text.Trim Else HoraSalida = txtHoraReprog.Text.Trim
        FechaProg = Right(txtFecReprog1.Text.Trim, 4) & Mid(txtFecReprog1.Text.Trim, 4, 2) & Left(txtFecReprog1.Text.Trim, 2)
        If cboTipoObs.SelectedValue = "< Seleccionar >" Then
            TipoObs = ""
        Else
            TipoObs = cboTipoObs.SelectedValue.Trim
        End If
        If cboEstado.SelectedValue = "9" Then
            If TipoObs <> "4" Then Obs = "Se llamo al PAI. N° de PAI" & CodLiquidacion & "<br> - " & txtObs.Text.Trim
            Obs = "PAI N° " & CodLiquidacion & "<br> - " & txtObs.Text.Trim
            If TipoObs = "" Then TipoObs = "4"
        Else
            Obs = txtObs.Text.Trim
        End If
        If cboEstado.SelectedValue = "2" Then
            TipoLlamada = cboPai101.SelectedValue.Trim
        End If
        Dim objProcesos As New clsInv_Procesos
        Dim pdCodOrigen As Double : pdCodOrigen = 0
        Dim psTipoOrigen As String : psTipoOrigen = "5"
        Dim pdCodSalida As Double : pdCodSalida = 0
        Dim psTipoDestino As String : psTipoDestino = "1"
        Dim pdCodDestino As Double : pdCodDestino = 4
        Dim pdCodEquipo As Double : pdCodEquipo = 0
        Dim psUser As String : psUser = HttpContext.Current.User.Identity.Name
        Dim psFechaDev As String : psFechaDev = FechaProg
        Dim psHoraDev As String : psHoraDev = HoraActual()
        Dim dt As DataTable
        'COD DEL ORIGEN
        dt = obj.Devolver_CodPersona(Session("Ruta_Emp"), Session("CodEmpresa"), CodPedido)
        If dt.Rows.Count = 1 Then
            For Each drMenuItem As Data.DataRow In dt.Rows
                pdCodOrigen = Nz(drMenuItem("PER_CODIGO"))
            Next
        End If
        dt = Nothing
        'COD DEL DESTINO
        dt = obj.Devolver_CodAlmacen(Session("Ruta_Emp"), Session("Codempresa"), CodCurrier)
        If dt.Rows.Count = 1 Then
            For Each drMenuItem As Data.DataRow In dt.Rows
                pdCodDestino = Nz(drMenuItem("ALMACEN_CODIGO"))
            Next
        End If
        dt = Nothing
        Try
            objInsUpdDel.Upd_GuiaRemision(Session("Ruta_Emp"), Session("CodEmpresa"), CodGuia, cboEstado.SelectedValue.Trim, PerAsignada, HoraSalida, FechaProg, TipoObs, Obs, CodLiquidacion, CodPedido, FechaActual, TipoLlamada)
            If pdCodDestino <> "4" Then
                If cboEstado.SelectedValue.Trim = "3" Then
                    objProcesos.Pedido_Rechazado(Session("Ruta_Emp"), psTipoOrigen, pdCodOrigen, psTipoDestino, pdCodDestino, Session("CodEmpresa"), psFechaDev, psHoraDev, psUser, Obs, CodGuia)
                End If
                If cboEstado.SelectedValue.Trim = "8" Then
                    If txtCodEstado.Text = "5" Then
                        objProcesos.Pedido_Rechazado(Session("Ruta_Emp"), psTipoOrigen, pdCodOrigen, psTipoDestino, pdCodDestino, Session("CodEmpresa"), psFechaDev, psHoraDev, psUser, Obs, CodGuia)
                    End If
                End If
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
        Flex.Enabled = True
        lblIngresarFecha.Visible = False
        lblError.Text = ""
        btnListar_Click(sender, e)
    End Sub
    Protected Sub btnCListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCListar.Click
        Dim obj As New clsInv_Listados
        lblError.Text = ""
        Try
            FlexCurrier.DataSource = obj.Lista_CurrierAutorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
            FlexCurrier.DataBind()
            lblRegistro1.Text = obj.Lista_CurrierAutorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name).Rows.Count
            lblRegistro1.Text = "Registros Encontrados : " & lblRegistro1.Text
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally

        End Try
    End Sub
    Protected Sub FlexCurrier_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexCurrier.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            Try
                txtCurrierRuc.Text = FlexCurrier.Rows(Index).Cells(2).Text
                txtCurrierRS.Text = FlexCurrier.Rows(Index).Cells(3).Text.Replace("&nbsp;", "")
                lblCodCurrier.Text = FlexCurrier.Rows(Index).Cells(1).Text.Replace("&nbsp;", "")
            Catch ex As SqlException
                lblError.Text = ex.Message
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
            End Try
            FlexCurrier.DataSource = Nothing
            FlexCurrier.DataBind()
            txtCRuc.Text = ""
            txtCRazonSocial.Text = ""
            lblRegistro1.Text = ""
            ModalPopupExtender1.Hide()
        End If
    End Sub
    Protected Sub btnCCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCCerrar.Click
        FlexCurrier.DataSource = Nothing
        FlexCurrier.DataBind()
        txtCRuc.Text = ""
        txtCRazonSocial.Text = ""
        lblRegistro1.Text = ""
    End Sub
    Protected Sub cboEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboEstado.SelectedValue = "6" Then
            lblEstEnProceso.Visible = True
            lblEstEntrega.Visible = False
        Else
            lblEstEnProceso.Visible = False
            lblEstEntrega.Visible = True
            cboTipoObs.Items.Clear()
            cboPai101.Items.Clear()
            Call LlenaComboItem("TBOPC404", cboTipoObs)
            cboTipoObs.Items.Add("< Seleccionar >") : cboTipoObs.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC406", cboPai101)
            cboPai101.Items.Add("< Seleccionar >") : cboPai101.SelectedValue = "< Seleccionar >"
            lblEt15.Visible = False
            txtCodLiquida.Visible = False
            lblEt15.Visible = True
            txtCodLiquida.Visible = True
            lblEtqPai.Visible = False
            cboPai101.Visible = False
            txtFecReprog1.Text = FormatoFecha(FechaActual)
            txtHoraReprog.Text = FormatoHora(HoraActual)
        End If
        If cboEstado.SelectedValue = "2" Then
            lblEt15.Text = "N° Liquidación"
            txtCodLiquida.MaxLength = 10
            lblEtqPai.Visible = True
            cboPai101.Visible = True
            lblEt14.Text = "Fecha Entrega"
            lblEt16.Text = "Hora Entrega"
            txtFecReprog1.Text = FormatoFecha(FechaActual)
            txtHoraReprog.Text = FormatoHora(HoraActual)
            txtFecReprog1.Enabled = False
            txtHoraReprog.Enabled = False
        ElseIf cboEstado.SelectedValue = "3" Or cboEstado.SelectedValue = "8" Then
            lblEt14.Text = "Fecha Rechazo"
            lblEt16.Text = "Hora Rechaza"
            lblEt15.Text = "N° PAI"
            txtCodLiquida.MaxLength = 10
            txtFecReprog1.Text = FormatoFecha(FechaActual)
            txtHoraReprog.Text = FormatoHora(HoraActual)
            txtFecReprog1.Enabled = False
            txtHoraReprog.Enabled = False
        ElseIf cboEstado.SelectedValue = "4" Or cboEstado.SelectedValue = "5" Then
            lblEt14.Text = "Fec. Reagenda"
            lblEt16.Text = "Hora Reagenda"
            lblEt15.Text = "N° PAI"
            txtCodLiquida.MaxLength = 10
            txtFecReprog1.Text = FormatoFecha(FechaActual)
            txtHoraReprog.Text = FormatoHora(HoraActual)
            txtFecReprog1.ReadOnly = False
            txtHoraReprog.ReadOnly = False
            txtFecReprog1.Enabled = True
            txtHoraReprog.Enabled = True
        Else
            lblEt15.Text = "N° PAI"
            txtCodLiquida.MaxLength = 10
            lblEt14.Text = "Fecha Entrega"
            lblEt16.Text = "Hora Entrega"
            txtFecReprog1.Text = FormatoFecha(FechaActual)
            txtHoraReprog.Text = FormatoHora(HoraActual)
            txtFecReprog1.Enabled = True
            txtHoraReprog.Enabled = True
        End If
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Lista_Pedidos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
    End Sub
End Class
