Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inspeccion_Detalle
    Inherits System.Web.UI.Page
    Private dataSetArbol As System.Data.DataSet
    Private Sub Lista_Inspeccion()
        Dim obj As New clsInspeccion_Listado
        Dim pdCodOficina As Double = 0
        Dim NroInspeccion As Double = 0
        Dim Tecnico As String = "0"
        Dim TipoPersona As String = "0"
        Dim TipoInspeccion As String = "0"
        Dim TipoEstado As String = "0"
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If txtNroInspeccion.Text.Trim <> "" Then NroInspeccion = txtNroInspeccion.Text.Trim
        If txtcodOficina.Text.Trim <> "" Then pdCodOficina = txtcodOficina.Text.Trim
        If cboTipoPersona.SelectedValue <> "< Seleccionar >" Then TipoPersona = cboTipoPersona.SelectedValue Else txtTecnico.Text = ""
        If txtTecnico.Text.Trim <> "" Then Tecnico = txtTecnico.Text.Trim
        If cboTipoInspeccion.SelectedValue <> "< Seleccionar >" Then TipoInspeccion = cboTipoInspeccion.SelectedValue
        If cboEstadoInspeccion.SelectedValue <> "< Seleccionar >" Then TipoEstado = cboEstadoInspeccion.SelectedValue
        If txtPorFechaInicio.Text.Trim <> "" And txtPorFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
            FechaFin = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
        ElseIf txtPorFechaInicio.Text.Trim <> "" And txtPorFechaFin.Text.Trim = "" Then
            FechaIni = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
            FechaFin = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
        ElseIf txtPorFechaInicio.Text.Trim = "" And txtPorFechaFin.Text.Trim = "" Then
            FechaIni = "20100101"
            FechaFin = "21000101"
        ElseIf txtPorFechaInicio.Text.Trim = "" And txtPorFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
            FechaFin = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
        End If
        Try
            Flex.DataSource = obj.Listar_Filtros_Inpeccion(psConexion, Session("CodEmpresa"), TipoPersona, TipoInspeccion, TipoEstado, FechaIni, FechaFin, pdCodOficina, Tecnico, NroInspeccion)
            Flex.DataBind()
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
            Exit Sub
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        Flex.PageIndex = e.NewPageIndex
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim dt As DataTable
        Dim pdCodOficina As Double = 0
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInspeccion_Listado
        Dim objInv_lis As New clsInv_Listados
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
        lblError.Text = ""
        If e.CommandName = "Editar" Then
            lblEditarInspeccion.Visible = True
            lblEditarServicio.Visible = True
            cboEditarTipoInspecc.Items.Clear()
            cboEditarTipoPersona.Items.Clear()
            Call LlenaComboItem("TBOPC378", cboEditarTipoPersona, psCnGrEmp)
            Call LlenaComboItem("TBOPC381", cboEditarTipoInspecc, psCnGrEmp)
            Call Limpiar_edicion()
            lblEditarServicio.Text = "Editar Servicio"
            ModalPopupExtender1.Hide()
            ModalPopupExtender2.Hide()
            Ficha.Height = 650
            txtEditar.Text = "editar"
            txtEditarNroInspeccion.Text = Flex.Rows(Index).Cells(3).Text.Trim
            txtEditarFechaProg.Text = Flex.Rows(Index).Cells(4).Text.Trim
            txtEditarHoraProg.Text = Flex.Rows(Index).Cells(5).Text.Trim
            txtEditarOficina.Text = Flex.Rows(Index).Cells(15).Text.Trim
            txtEditarDescOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtEditarCodOficina.Text = Flex.Rows(Index).Cells(14).Text.Trim
            txtEditarTipoPersona.Text = Flex.Rows(Index).Cells(17).Text.Trim
            txtEditarTipoInspecc.Text = Flex.Rows(Index).Cells(18).Text.Trim
            txtEditarCodigoTecnico.Text = Flex.Rows(Index).Cells(19).Text.Trim
            dt = obj.Listar_Inpeccion(psConexion, txtEditarNroInspeccion.Text)
            If dt.Rows.Count = 1 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtEditarRucTipoPersona.Text = Nu(dr("RUC"))
                    txtEditarRazonSocialTipoPersona.Text = Nu(dr("PERSONA_ASIG"))
                    If Nu(dr("TIPOPER")) = "" Then cboEditarTipoPersona.SelectedValue = "< Seleccionar >" Else cboEditarTipoPersona.SelectedValue = Nu(dr("INSPEC_TIPOPER"))
                    If Nu(dr("TIPO")) = "" Then cboEditarTipoInspecc.SelectedValue = "< Seleccionar >" Else cboEditarTipoInspecc.SelectedValue = Nu(dr("INSPEC_TIPO"))
                Next
            End If
            If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") <> "" Then
                cboTta.SelectedValue = Flex.Rows(Index).Cells(20).Text.Trim
            Else
                cboTta.SelectedValue = "< Seleccionar >"
            End If
            If Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") <> "" Then
                cboTsi.SelectedValue = Flex.Rows(Index).Cells(21).Text.Trim
            Else
                cboTsi.SelectedValue = "< Seleccionar >"
            End If
        End If
        If e.CommandName = "Equipos" Then
            txtcodigoUbicacion.Text = Flex.Rows(Index).Cells(14).Text.Trim
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
            Ficha.Height = 550
            Panel3.Visible = False
            FlexArticulos.DataSource = objInv_lis.Listar_Equipos(psConexion, Session("CodEmpresa"), txtcodigoUbicacion.Text)
            FlexArticulos.DataBind()
            txtNroInspeccionEquipo.Text = Flex.Rows(Index).Cells(3).Text.Trim
            txtNroInpeccionDetalleCodigo.Text = txtNroInspeccion.Text
            txtDetalleNroInspeccion.Text = Flex.Rows(Index).Cells(3).Text.Trim
            txtRucOfic.Text = Flex.Rows(Index).Cells(15).Text.Trim
            txtDescripOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtCodInternoOficina.Text = Flex.Rows(Index).Cells(14).Text.Trim
            txtDetalleRucOficina.Text = Flex.Rows(Index).Cells(15).Text.Trim
            txtDetalleOficinaDescripc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtCodOficinaDetalle.Text = Flex.Rows(Index).Cells(14).Text.Trim
        End If
        If e.CommandName = "Adicionales" Then
            txtNroInspecc.Text = Flex.Rows(Index).Cells(3).Text.Trim
            txtUpdCodOficina.Text = Flex.Rows(Index).Cells(14).Text.Trim
            txtOfic.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.Height = 750
            txtObsEstadofinal.Text = ""
            txtFecSolPosible.Text = ""
            cboEstadoObjetivo.Items.Clear()
            Call LlenaComboItem("TBOPC380", cboEstadoObjetivo, psCnGrEmp)
            Call LlenaComboItem("TBOPC387", cboEstadoFinal, psCnGrEmp)
            Call LlenaComboItem("TBOPC388", cboTipificacion, psCnGrEmp)
            Call Lista_Responsable()
            cboResponsable.Items.Add("< Seleccionar >") : cboResponsable.SelectedValue = "< Seleccionar >"
            Dim NroInspeccion As Double = 0
            Dim dtPart As DataTable
            If txtNroInspecc.Text.Trim = "" Then Exit Sub
            Try
                NroInspeccion = txtNroInspecc.Text.Trim
                txtTipoPersona.Text = Flex.Rows(Index).Cells(12).Text.Trim
                dt = obj.Listar_Inpeccion(psConexion, NroInspeccion)
                dtPart = obj.Listado_Participantes(psConexion, NroInspeccion)
                If dt.Rows.Count = 1 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtFechareal.Text = FormatoFecha(Nu(dr("INSPEC_FECHA_REALIZADA")))
                        txtHoraInicio.Text = FormatoHora(Nu(dr("INSPEC_INI_HORA")))
                        txtHoraTermino.Text = FormatoHora(Nu(dr("INSPEC_FIN_HORA")))
                        txtHoraLlegada.Text = FormatoHora(Nu(dr("INSPEC_HORA_LLEGADA")))
                        txtHorasExtras.Text = FormatoHora(Nu(dr("INSPEC_HORA_EXTRA")))
                        txtMovilidad.Text = Nz(dr("INSPEC_MOVILIDAD"))
                        txtdocReferencia.Text = Nu(dr("INSPEC_DOCREFERENCIA"))
                        txtObjetivo.Text = Nu(dr("INSEPC_OBJETIVO"))
                        txtFechaFin.Text = FormatoFecha(Nu(dr("INSPEC_FIN_FECHA")))
                        txtTipoPerNombre.Text = Nu(dr("PERSONA_ASIG"))
                        txtrucProveedor.Text = Nu(dr("RUC"))
                        txtObservacion.Text = Nu(dr("INSPEC_OBS"))
                        txtTrabajoRealizado.Text = Nu(dr("INSPEC_TRABREALIZADO"))
                        txtMovilidadVuelta.Text = Nz(dr("INSPEC_MOVILIDAD_VUELTA"))
                        txtMovilidadDescripcion.Text = Nu(dr("INSPEC_MOVILIDAD_DESCRIPCION"))
                        txtObsEstadofinal.Text = Nu(dr("INSPEC_ESTADO_FINAL_OBS"))
                        If Nu(dr("INSPEC_INI_HORA")) = "" Then txtHoraInicio.Text = "08:00"
                        If Nu(dr("INSPEC_FIN_HORA")) = "" Then txtHoraTermino.Text = "08:00"
                        If Nu(dr("INSPEC_HORA_LLEGADA")) = "" Then txtHoraLlegada.Text = "08:00"
                        If Nu(dr("INSPEC_HORA_EXTRA")) = "" Then txtHorasExtras.Text = "00:00"
                        If Nu(dr("INSPEC_MOVILIDAD")) = "" Then txtMovilidad.Text = "0.00"
                        If Nu(dr("INSPEC_ESTADO_FINAL")) = "" Then cboEstadoFinal.SelectedValue = "< Seleccionar >" Else cboEstadoFinal.SelectedValue = Nu(dr("INSPEC_ESTADO_FINAL")) : cboEstadoFinal_SelectedIndexChanged(sender, e)
                        If Nu(dr("INSPEC_OBJETIVO_ESTADO")) = "" Then cboEstadoObjetivo.SelectedValue = "< Seleccionar >" Else cboEstadoObjetivo.SelectedValue = Nu(dr("INSPEC_OBJETIVO_ESTADO"))
                        If Nu(dr("INSPEC_FECHA_REALIZADA")) = "" Then txtFechareal.Text = FormatoFecha(FechaActual(), True)
                        If Nu(dr("INSPEC_MOVILIDAD_VUELTA")) = "" Then txtMovilidadVuelta.Text = "0.00"
                        If Nu(dr("INSPEC_ESTADO_FINAL_TIPIFICACION")) = "" Then cboTipificacion.SelectedValue = "< Seleccionar >" Else cboTipificacion.SelectedValue = Nu(dr("INSPEC_ESTADO_FINAL_TIPIFICACION"))
                        If Nu(dr("INSPEC_RESPONSABLE")) = "" Then cboResponsable.SelectedValue = "< Seleccionar >" Else cboResponsable.SelectedValue = Nu(dr("INSPEC_RESPONSABLE"))
                        If Nu(dr("INSPEC_FECHA_SOLUCION_POSIBLE")) <> "" Then txtFecSolPosible.Text = Nu(dr("FECHA_SOLUCION_POSIBLE"))
                    Next
                End If
                dt = Nothing
                pdCodOficina = Flex.Rows(Index).Cells(14).Text.Trim
                dt = obj.Ultima_Verificacion(psConexion, Session("CodEmpresa"), pdCodOficina)
                If dt.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dt.Rows
                        If Nu(dr("FECHA")) <> "" Then
                            txtFecVerificacion.Text = FormatoFecha(Nu(dr("FECHA")))
                            chkOfVerificacion.Checked = True
                        End If
                    Next
                Else
                    txtFecVerificacion.Text = ""
                    chkOfVerificacion.Checked = False
                End If
                If cboEstadoFinal.SelectedValue = "1" Then txtObsEstadofinal.Enabled = True Else txtObsEstadofinal.Enabled = False
                Call CargarGrillaParticipantesVacios()
            Catch Ex As SqlException
                lblError.Text = "Ha ocurrido un error en la base de datos: " & Ex.Message
            Catch Ex As Exception
                lblError.Text = "Ha ocurrido un error en la aplicación: " & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Private Sub Lista_Responsable()
        Dim obj As New clsInspeccion_Listado
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Try
            cboResponsable.DataSource = obj.Lista_Responsable_Solucion(psConexion, Session("CodEmpresa"), "", "")
            cboResponsable.DataTextField = "DESCRIPCION"
            cboResponsable.DataValueField = "CODIGO"
            cboResponsable.DataBind()
        Catch Ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & Ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
            Try
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = False
                Ficha.Height = 450
                Ficha.ActiveTabIndex = 0
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Call LlenaComboItem("TBOPC379", cboEstadoInspeccion, psCnGrEmp)
            cboEstadoInspeccion.Items.Add("< Seleccionar >") : cboEstadoInspeccion.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC381", cboTipoInspeccion, psCnGrEmp)
            cboTipoInspeccion.Items.Add("< Seleccionar >") : cboTipoInspeccion.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC378", cboTipoPersona, psCnGrEmp)
            cboTipoPersona.Items.Add("< Seleccionar >") : cboTipoPersona.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC383", cboEstadoEquipo, psCnGrEmp)
            cboEstadoEquipo.Items.Add("< Seleccionar >") : cboEstadoEquipo.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC384", cboEstadoBateria, psCnGrEmp)
            cboEstadoBateria.Items.Add("< Seleccionar >") : cboEstadoBateria.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 450
        lblError.Text = ""
        Call Lista_Inspeccion()
        lblEditarInspeccion.Visible = False
    End Sub
    Protected Sub btnListar_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Lista_Inspeccion()
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim obj As New clsInspeccion_InsUpdDel
        Dim objListado As New clsInspeccion_Listado
        Dim objInspInsUpdDel As New clsInspeccion_InsUpdDel
        Dim dt As DataTable
        Dim i As Integer = 0
        Dim fechareali As String = "20100101"
        Dim horaIni As String = ""
        Dim horaTermino As String = ""
        Dim horaLLegada As String = ""
        Dim horaExtras As String = ""
        Dim fechaFin As String = "20100101"
        Dim NroInsp As Double = 0
        Dim NroPart As Double = 0
        Dim movi As Double = 0
        Dim MovilidadVuelta As Double = 0
        Dim ruc As String = ""
        Dim rz As String = ""
        Dim encar As String = ""
        Dim psEstadoFinal As String = ""
        Dim psEstadoObjetivo As String = ""
        Dim pdCodOficina As Double = 0
        Dim psFechaVerificacion As String = ""
        Dim psTipificacion As String = ""
        Dim psFechaSolucion As String = ""
        Dim pdResponsable As Double = 0
        lblError.Text = ""
        pdCodOficina = txtUpdCodOficina.Text.Trim
        movi = Nz(txtMovilidad.Text.Trim)
        MovilidadVuelta = Nz(txtMovilidadVuelta.Text.Trim)
        NroInsp = Nz(txtNroInspecc.Text.Trim)
        fechareali = Right(txtFechareal.Text.Trim, 4) & Mid(txtFechareal.Text.Trim, 4, 2) & Left(txtFechareal.Text.Trim, 2)
        horaIni = Left(txtHoraInicio.Text.Trim, 2) & Right(txtHoraInicio.Text.Trim, 2)
        horaTermino = Left(txtHoraTermino.Text.Trim, 2) & Right(txtHoraTermino.Text.Trim, 2)
        horaLLegada = Left(txtHoraLlegada.Text.Trim, 2) & Right(txtHoraLlegada.Text.Trim, 2)
        horaExtras = Left(txtHorasExtras.Text.Trim, 2) & Right(txtHorasExtras.Text.Trim, 2)
        fechaFin = Right(txtFechaFin.Text.Trim, 4) & Mid(txtFechaFin.Text.Trim, 4, 2) & Left(txtFechaFin.Text.Trim, 2)
        If cboEstadoFinal.SelectedValue <> "< Seleccionar >" Then psEstadoFinal = cboEstadoFinal.SelectedValue.Trim
        If cboEstadoObjetivo.SelectedValue <> "< Seleccionar >" Then psEstadoObjetivo = cboEstadoObjetivo.SelectedValue.Trim
        If cboTipificacion.SelectedValue <> "< Seleccionar >" Then psTipificacion = cboTipificacion.SelectedValue.Trim
        If cboResponsable.SelectedValue <> "< Seleccionar >" Then pdResponsable = cboResponsable.SelectedValue.Trim
        If txtFecSolPosible.Text.Trim <> "" Then psFechaSolucion = Right(txtFecSolPosible.Text.Trim, 4) & Mid(txtFecSolPosible.Text.Trim, 4, 2) & Left(txtFecSolPosible.Text.Trim, 2)
        Try
            obj.Upd_Inspeccion(psConexion, Session("CodEmpresa"), txtNroInspecc.Text.Trim, fechareali, _
                                  horaIni, horaTermino, horaLLegada, horaExtras, movi, _
                                  txtdocReferencia.Text.Trim, txtObjetivo.Text.Trim, psEstadoObjetivo, fechaFin, _
                                  txtObservacion.Text.Trim, txtTrabajoRealizado.Text.Trim, MovilidadVuelta, txtMovilidadDescripcion.Text.Trim, _
                                  psEstadoFinal, txtObsEstadofinal.Text.Trim, psTipificacion, psFechaSolucion, pdResponsable)
            If cboEstadoFinal.SelectedValue <> "< Seleccionar >" Then
                objInspInsUpdDel.Upd_Of_EstadoFinal(psConexion, Session("CodEmpresa"), pdCodOficina, psEstadoFinal, txtObsEstadofinal.Text.Trim, psTipificacion, psFechaSolucion, pdResponsable)
            End If
            obj.Del_Inspeccion_Participante(psConexion, Session("CodEmpresa"), NroInsp)
            For i = 0 To FlexTipoParticipantes.Rows.Count - 1
                ruc = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoParticipantes.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                rz = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoParticipantes.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                encar = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoParticipantes.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                obj.Ins_Inspeccion_Participante(psConexion, Session("CodEmpresa"), NroInsp, NroPart, ruc, _
                rz, encar)
            Next
            If chkOfVerificacion.Checked = True Then
                psFechaVerificacion = Right(txtFecVerificacion.Text.Trim, 4) & Mid(txtFecVerificacion.Text.Trim, 4, 2) & Left(txtFecVerificacion.Text.Trim, 2)
                dt = objListado.Confirmar_Ultima_Verificacion(psConexion, Session("CodEmpresa"), pdCodOficina, psFechaVerificacion)
                If dt.Rows.Count = 0 Then
                    objInspInsUpdDel.Ins_OfVerifica(psConexion, Session("CodEmpresa"), pdCodOficina, psFechaVerificacion)
                End If
            End If
            Call Limpiar()
            Call Lista_Inspeccion()
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        Dim opt As Boolean = False
        Call agregar()
    End Sub
    Sub Limpiar()
        txtRuc.Text = ""
        txtRazonSocial.Text = ""
        txtEncargado.Text = ""
    End Sub
    Sub CargarGrillaParticipantesVacios()
        Dim Index As Integer = 1
        Dim dRow As Data.DataRow
        Dim dtP As DataTable
        Dim dtInsp As DataTable
        Dim obj As New clsInspeccion_Listado
        Dim NroInsp As Double = 0
        Dim NroPart As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        NroInsp = Nz(txtNroInspecc.Text.Trim)
        dtP = obj.Listado_Participantes(psConexion, NroInsp)
        dtInsp = obj.Listar_Inpeccion(psConexion, NroInsp)
        Try
            If dtP.Rows.Count > 0 Then
                FlexTipoParticipantes.DataSource = dtP
                FlexTipoParticipantes.DataBind()
            Else
                dRow = dtP.NewRow
                If txtTipoPersona.Text = "1" Then
                    dRow("INSPART_CODINTERNO") = Nz("20512087061")
                    dRow("INSPART_NOMBRE") = Nu("Tecnologias y Sistemas de Gestion SRL")
                    dRow("INSPART_ENCARGADO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(txtTipoPerNombre.Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") 'Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(txtTipoPerNombre.Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                    dRow("INSPEC_CODIGO") = txtNroInspecc.Text
                    dtP.Rows.Add(dRow)
                    FlexTipoParticipantes.DataSource = dtP
                    FlexTipoParticipantes.DataBind()
                Else
                    dRow("INSPART_CODINTERNO") = txtrucProveedor.Text
                    dRow("INSPART_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(txtTipoPerNombre.Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    dRow("INSPART_ENCARGADO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(txtTipoPerNombre.Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    dRow("INSPEC_CODIGO") = txtNroInspecc.Text
                    dtP.Rows.Add(dRow)
                    FlexTipoParticipantes.DataSource = dtP
                    FlexTipoParticipantes.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = "error"
        End Try
    End Sub
    Private Sub agregar()
        Dim i As Integer = 0
        Dim obj As New Insertar
        Dim dRow As Data.DataRow
        Dim dt As New DataTable
        Dim objListado As New clsInspeccion_Listado
        Dim NroInsp As Double = 0
        Dim NroPart As Double = 0
        NroInsp = Nz(txtNroInspecc.Text.Trim)
        dt.Columns.Add("INSPART_CODINTERNO")
        dt.Columns.Add("INSPART_NOMBRE")
        dt.Columns.Add("INSPART_ENCARGADO")
        dt.Columns.Add("INSPEC_CODIGO")
        Try
            If txtRuc.Text.Trim = "" Then lblError.Text = "Ingresar Ruc" : Exit Sub
            If txtRazonSocial.Text.Trim = "" Then lblError.Text = "Ingresar RazonSocial" : Exit Sub
            If txtEncargado.Text.Trim = "" Then lblError.Text = "Ingresar Encargado" : Exit Sub

            For i = 0 To FlexTipoParticipantes.Rows.Count - 1
                dRow = dt.NewRow
                dRow("INSPART_CODINTERNO") = Nu(FlexTipoParticipantes.Rows(i).Cells(1).Text.Trim)
                dRow("INSPART_NOMBRE") = Nu(FlexTipoParticipantes.Rows(i).Cells(2).Text.Trim)
                dRow("INSPART_ENCARGADO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoParticipantes.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                dRow("INSPEC_CODIGO") = NroInsp
                dt.Rows.Add(dRow)
            Next
            dRow = dt.NewRow
            dRow("INSPART_CODINTERNO") = txtRuc.Text.Trim
            dRow("INSPART_NOMBRE") = txtRazonSocial.Text.Trim
            dRow("INSPART_ENCARGADO") = txtEncargado.Text.Trim
            dRow("INSPEC_CODIGO") = NroInsp
            dt.Rows.Add(dRow)
            FlexTipoParticipantes.DataSource = dt
            FlexTipoParticipantes.DataBind()
        Catch ex As Exception
        End Try
        Call Limpiar()
    End Sub
    Protected Sub FlexTipoParticipantes_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTipoParticipantes.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Quitar" Then
            FlexTipoParticipantes.Rows(Index).Cells(6).Text = "1"
            Call ListarParticipantes()
        End If
    End Sub

    Private Sub ListarParticipantes()
        Dim dt As New DataTable
        Dim dRow As Data.DataRow
        Dim i As Integer = 0
        dt.Columns.Add("INSPART_CODINTERNO")
        dt.Columns.Add("INSPART_NOMBRE")
        dt.Columns.Add("INSPART_ENCARGADO")
        dt.Columns.Add("INSPEC_CODIGO")
        dt.Columns.Add("INSPART_CODIGO")
        dt.Columns.Add("C1")
        For i = 0 To FlexTipoParticipantes.Rows.Count - 1
            If FlexTipoParticipantes.Rows(i).Cells(6).Text = "0" Then
                dRow = dt.NewRow
                dRow("INSPART_CODINTERNO") = Nu(FlexTipoParticipantes.Rows(i).Cells(1).Text.Trim)
                dRow("INSPART_NOMBRE") = Nu(FlexTipoParticipantes.Rows(i).Cells(2).Text.Trim)
                dRow("INSPART_ENCARGADO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoParticipantes.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                dRow("INSPEC_CODIGO") = Nu(FlexTipoParticipantes.Rows(i).Cells(4).Text.Trim)
                dRow("INSPART_CODIGO") = Nu(FlexTipoParticipantes.Rows(i).Cells(5).Text.Trim)
                dRow("C1") = Nu(FlexTipoParticipantes.Rows(i).Cells(6).Text.Trim)
                dt.Rows.Add(dRow)
            End If
        Next
        FlexTipoParticipantes.DataSource = dt
        FlexTipoParticipantes.DataBind()
    End Sub
    Protected Sub btnBuscarXOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub

    Protected Sub btnListarOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Try
            FlexOficina.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCodigo.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexOficina.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub FlexOficina_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                If txtEditar.Text = "editar" Then
                    txtEditarOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtEditarDescOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtEditarCodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    cboTta.SelectedValue = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    cboTta.SelectedValue = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                Else
                    txtPorCodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtPorOficDescrip.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtcodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                End If
                FlexOficina.DataSource = Nothing
                FlexOficina.DataBind()
                txtBusCodigo.Text = ""
                txtBusDescripcion.Text = ""

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
    Protected Sub btnGrabarReg_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Protected Sub btnNuevoReg_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 500
    End Sub
    Protected Sub cboTipoInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Protected Sub cboEstadoInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Protected Sub btnListarTipoPers_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboTipoPersona.SelectedValue.Trim = "3" Then
            listarTipoPersonaXProveedor()
        Else
            listarTipoPersonaXTecnico()
        End If
        If cboEditarTipoPersona.SelectedValue.Trim = "3" Then
            listarTipoPersonaXProveedor()
        Else
            listarTipoPersonaXTecnico()
        End If
    End Sub
    Private Sub listarTipoPersonaXProveedor()
        Dim obj As New clsInspeccion_Listado
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Try
            FlexTipoPers.DataSource = obj.Lista_TipoPersona(psConexion, Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub listarTipoPersonaXTecnico()
        Dim obj As New clsInspeccion_Listado
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Try
            FlexTipoPers.DataSource = obj.Lista_TipoPersonaTecnico(psConexion, Session("CodGrupoEmpresa"), Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnListarTipoPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Protected Sub btnListarTipoPers_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtEditar.Text = "0" Then
            If txtcodigoPersona.Text = "3" Then
                listarTipoPersonaXProveedor()
            ElseIf txtcodigoPersona.Text = "1" Then
                listarTipoPersonaXTecnico()
            ElseIf cboTipoPersona.SelectedValue.Trim = "< Seleccionar >" Then
                ModalPopupExtender2.Hide()
            End If
            ModalPopupExtender2.Show()
        Else
            If cboEditarTipoPersona.SelectedValue.Trim = "3" Then
                listarTipoPersonaXProveedor()
            ElseIf cboEditarTipoPersona.SelectedValue.Trim = "< Seleccionar >" Then
                txtEditarRucTipoPersona.Text = ""
                txtEditarRazonSocialTipoPersona.Text = ""
                ModalPopupExtender4.Hide()
            Else
                listarTipoPersonaXTecnico()
            End If
            ModalPopupExtender4.Show()
        End If
    End Sub
    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            'lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                If txtEditar.Text = "editar" Then
                    txtEditarRucTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtEditarRazonSocialTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtEditarCodigoTecnico.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    If txtEditarRucTipoPersona.Text <> "" And txtEditarRazonSocialTipoPersona.Text <> "" Then
                        'LblEditarServicio.Text = ""
                    End If
                Else
                    txtTecnico.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtRucTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    txtRazonSocialTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                End If
                FlexTipoPers.DataSource = Nothing
                FlexTipoPers.DataBind()
                txtRucTipoPers.Text = ""
                txtRazonSocialTipoPers.Text = ""
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
    Protected Sub txtPorCodOficina_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtcodOficina.Text = ""
        txtPorCodOficina.Text = ""
        txtPorOficDescrip.Text = ""
    End Sub

    Protected Sub txtRucTipoPersona_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRucTipoPersona.Text = ""
        txtRazonSocialTipoPersona.Text = ""
        txtTecnico.Text = ""
    End Sub
    Protected Sub CheckBox1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInspeccion_Listado
        Dim Codarticulo As Double = 0
        Dim NroInspeccion As Double = 0
        Dim ubicacion As Double = 0
        lblEqDetalle.Text = "Ingresar Equipo"
        ChkIngresarEquipo.Visible = True
        lblEtiqNroIp.Visible = False : txtNroIp.Visible = False : txtNroIp.Text = ""
        lblEtiqEstEq.Visible = True : lblEtiqEstBat.Visible = True
        cboEstadoEquipo.Visible = True : cboEstadoBateria.Visible = True
        btnBuscarSerie.Enabled = True : btnBuscarXPlaca.Enabled = True : btnBuscarArticulo.Visible = True
        ChkIngresarEquipo.Checked = True
        lblDetalleEquipo.Visible = True
        lblDetalleInspeccion.Visible = False
        Codarticulo = Nz(txtCodArticulo.Text)
        ubicacion = Nz(txtcodigoUbicacion.Text)
        NroInspeccion = Nz(txtNroInspeccionEquipo.Text)
        NroInspeccion = Nz(txtNroInspeccionEquipoDetalle.Text)
        Call Limpiar_Equipos()
        Call Mostrar_Oficina()
    End Sub
    Private Function IngresarEquipo() As String
        IngresarEquipo = ""
        Dim objList As New clsInv_Listados
        Dim objIns As New clsInv_InsUpdDel
        Dim StockAc As Long : StockAc = 0
        Dim lblNroMovimiento As Double = 0
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim psEstadoEquipo As String = ""
        Dim psEstadoBateria As String = ""
        Dim psCodAlmacen As Double = 0
        Dim psProvee As Double = 0
        Dim pdCodArt As Double = 0
        Dim pdCodUbica As Double = 0
        Dim pdSerieNum As Double = 0
        Dim psUbicactCodigo As Double = 0
        Dim psRecep As Double = 0
        Dim Placa As Double = 0
        Dim pdStockActual As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If Trim(txtNroSerie.Text) = "" Then lblErrorEquipo.Text = "Ingresar el Nro de Serie."
        'If Trim(txtNroPlaca.Text) = "" Then lblErrorEquipo.Text = lblErrorEquipo.Text & "<br> - Ingresar el Nro de Placa."
        If Trim(txtCodigoEdicionArticulo.Text) = "" Then lblErrorEquipo.Text = lblErrorEquipo.Text & "<br> - Ingresar Artículo."
        If cboEstadoEquipo.SelectedValue = "< Seleccionar >" Then lblErrorEquipo.Text = lblErrorEquipo.Text & "<br> - Seleccionar el Estado del Equipo."
        If cboEstadoBateria.SelectedValue = "< Seleccionar >" Then lblErrorEquipo.Text = lblErrorEquipo.Text & "<br> - Seleccionar el Estado de la Bateria."
        If lblErrorEquipo.Text <> "" Then
            lblErrorEquipo.Text = "Existe las siguientes observaciones, favor de corregir:" & lblErrorEquipo.Text
            IngresarEquipo = "1"
            Exit Function
        End If
        '========================SI LA SERIE EXISTE
        dt = objList.Listado_Articulos_NroSerie(psConexion, txtNroSerie.Text.Trim)
        If dt.Rows.Count = 0 Then
            dt1 = objList.Devolver_UltimoCodRecepcion(psConexion)
            If dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    psRecep = Nz(dr("COD_RECEP")) + 1
                    txtRecep.Text = Nz(dr("COD_RECEP")) + 1
                Next
            End If
            dt1 = Nothing
            psCodAlmacen = Nz(txtALMACEN_CODIGO.Text.Trim)
            psProvee = Nz(txtRECEP_PROVEEDOR.Text.Trim)
            pdCodArt = Nz(txtCodigoEdicionArticulo.Text.Trim)
            pdCodUbica = Nz(txtCodInternoOficina.Text.Trim)
            If cboEstadoEquipo.SelectedValue <> "< Seleccionar >" Then psEstadoEquipo = cboEstadoEquipo.SelectedValue.Trim
            If cboEstadoBateria.SelectedValue <> "< Seleccionar >" Then psEstadoBateria = cboEstadoBateria.SelectedValue.Trim

            objIns.Ins_Almacen_Recepcion(psConexion, Session("CodEmpresa"), psRecep, 2, pdCodUbica, 0, _
                                              "2", "", "20", 1, "1", FechaActual, FechaActual, HttpContext.Current.User.Identity.Name)

            objIns.Ins_Almacen_Recepcion_Det(psConexion, Session("CodEmpresa"), psRecep, _
                                   1, pdCodArt, 1, 1, 0, 0, "2", "20", "N")

            dt1 = objList.Devolver_UltimoCodSerie(psConexion)
            If dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    pdSerieNum = Nz(dr("COD_SERIE")) + 1
                    txtSerieEnum.Text = Nz(dr("COD_SERIE")) + 1
                Next
            End If
            dt1 = Nothing

            Placa = Nz(txtNroPlaca.Text.Trim)
            objIns.Ins_Articulos_Series(psConexion, pdSerieNum, psRecep, pdCodArt, txtNroSerie.Text.Trim, _
                                             "N", Placa, "2", pdCodUbica, "", "", _
                                             "S", 1, "1", 0, "0", psEstadoEquipo, psEstadoBateria, HttpContext.Current.User.Identity.Name)
            objIns.Ins_Articulos_Series_Ubic(psConexion, pdSerieNum, "2", pdCodUbica, _
                                                  "20", "", FechaActual, "3", psRecep, HttpContext.Current.User.Identity.Name)

            'STOCK
            dt = objList.Listado_StockArticulos_Almacen(psConexion, Session("CodEmpresa"), _
                                                        pdCodUbica, "2", pdCodArt)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    pdStockActual = dr("SAA_STOCK_ACTUAL")
                Next
                objIns.Upd_Articulos_Almacen(psConexion, Session("CodEmpresa"), pdCodUbica, "2", pdCodArt, pdStockActual)
            Else
                objIns.Ins_Articulos_Almacen(psConexion, Session("CodEmpresa"), pdCodUbica, "2", pdCodArt, 1)
            End If

            'MOVIMIENTO GENERAL
            lblNroMovimiento = 0
            objIns.Ins_Movimiento_General(psConexion, Session("CodEmpresa"), lblNroMovimiento, "1", "2", pdCodUbica, pdCodArt, _
                                               1, "", "3", "20", FechaActual, psRecep, HttpContext.Current.User.Identity.Name)

        End If
        dt = Nothing
    End Function
    Protected Sub FlexArticulos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInv_Listados
        Dim dt As DataTable
        Dim Codarticulo As String = ""
        Dim CodOficina As Double = 0
        Dim i As Integer = 0
        Dim placa As Double = 0
        Dim serie As String = "0"
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
        CodOficina = Nz(txtCodInternoOficina.Text)
        lblErrorEquipo.Text = ""
        Try
            If e.CommandName = "Detalle" Then
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = True
                Ficha.Height = 550
                lblDetallCampoEquipo.Visible = True
                txtCodArticulo.Text = FlexArticulos.Rows(Index).Cells(7).Text.Trim
                Codarticulo = txtCodArticulo.Text
                txtUbicaTipo.Text = FlexArticulos.Rows(Index).Cells(8).Text.Trim
                txtNroInspeccionEquipoDetalle.Text = txtNroInspeccionEquipo.Text
                txtNroInspeccionDetalle.Text = txtNroInspeccion.Text
                placa = Nz(Replace(FlexArticulos.Rows(Index).Cells(5).Text.Trim, "&nbsp;", "0"))
                serie = FlexArticulos.Rows(Index).Cells(4).Text.Trim
                Call LlenaComboItem("TBOPC383", cboEstadoEquipoInsp, psCnGrEmp)
                Call LlenaComboItem("TBOPC384", cboEstadoBateriaInpec, psCnGrEmp)
                Call LlenaComboItem("TBOPC377", cboAreaInspeccion, psCnGrEmp)
                cboEstadoEquipoInsp.Items.Add("< Seleccionar >") : cboEstadoEquipoInsp.SelectedValue = "< Seleccionar >"
                cboEstadoBateriaInpec.Items.Add("< Seleccionar >") : cboEstadoBateriaInpec.SelectedValue = "< Seleccionar >"
                cboAreaInspeccion.Items.Add("< Seleccionar >") : cboAreaInspeccion.SelectedValue = 4
                dt = obj.BuscarXSerie_Placa(psConexion, Session("CodEmpresa"), CodOficina, placa, serie, Codarticulo)
                If dt.Rows.Count = 1 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtNroSerieDetalle.Text = Nu(dr("SERIE_NRO"))
                        txtArticuloDetalle.Text = Nu(dr("CODIGO_ARTICULO"))
                        txtDescArticuloDetalle.Text = Nu(dr("DESCRIPCION"))
                        txtOficinaDetalle.Text = Nu(dr("RUC"))
                        txtDescOficinaDetalle.Text = Nu(dr("OFICINA"))
                        txtNroPlacaDetalle.Text = Nu(dr("PLACA_NRO"))
                        txtMarcaDetalle.Text = Nu(dr("MARCA"))
                        txtModeloDetalle.Text = Nu(dr("MODELO"))
                        txtCodigoEdicionArticulo.Text = Nu(dr("ARTICULO_CODIGO"))
                        txtSerie_Numerar_Detalle.Text = Nu(dr("SERIE_NUMERAR"))
                        txtCodClasifica.Text = Nu(dr("ART_CLASIFICACION"))
                        If Nu(dr("ESTADO_EQUIPO")) = "" Then cboEstadoEquipoInsp.SelectedValue = "< Seleccionar >" Else cboEstadoEquipoInsp.SelectedValue = Nu(dr("ESTADO_EQUIPO"))
                        If Nu(dr("ESTADO_BATERIA")) = "" Then cboEstadoBateriaInpec.SelectedValue = "< Seleccionar >" Else cboEstadoBateriaInpec.SelectedValue = Nu(dr("ESTADO_BATERIA"))
                    Next
                End If
                cboAreaInspeccion_SelectedIndexChanged(sender, e)
            ElseIf e.CommandName = "Ip" Then
                lblDetalleEquipo.Visible = True
                lblDetalleInspeccion.Visible = False
                lblEqDetalle.Text = "Ingresar IP"
                ChkIngresarEquipo.Visible = False
                lblEtiqNroIp.Visible = True
                txtNroIp.Visible = True : txtNroIp.Text = ""
                lblEtiqEstEq.Visible = False : lblEtiqEstBat.Visible = False
                cboEstadoEquipo.Visible = False : cboEstadoBateria.Visible = False
                btnBuscarSerie.Enabled = False : btnBuscarXPlaca.Enabled = False : btnBuscarArticulo.Enabled = False
                txtCodArticulo.Text = FlexArticulos.Rows(Index).Cells(7).Text.Trim
                Codarticulo = txtCodArticulo.Text
                txtNroInspeccionEquipo.Text = txtNroInspeccionEquipo.Text
                placa = Nz(Replace(FlexArticulos.Rows(Index).Cells(5).Text.Trim, "&nbsp;", "0"))
                serie = FlexArticulos.Rows(Index).Cells(4).Text.Trim
                dt = obj.BuscarXSerie_Placa(psConexion, Session("CodEmpresa"), CodOficina, placa, serie, Codarticulo)
                If dt.Rows.Count = 1 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtNroSerie.Text = Nu(dr("SERIE_NRO"))
                        txtArticulo.Text = Nu(dr("CODIGO_ARTICULO"))
                        txtDescArticulo.Text = Nu(dr("DESCRIPCION"))
                        txtOficina.Text = Nu(dr("RUC"))
                        txtDescOficina.Text = Nu(dr("OFICINA"))
                        txtNroPlaca.Text = Nu(dr("PLACA_NRO"))
                        txtMarca.Text = Nu(dr("MARCA"))
                        txtModelo.Text = Nu(dr("MODELO"))
                        txtSerieEnum.Text = Nu(dr("SERIE_NUMERAR"))
                        txtNroIp.Text = Nu(dr("NRO_IP"))
                    Next
                End If
            End If
        Catch ex As SqlException
            lblErrorEquipo.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorEquipo.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnRegresarInspeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 450
        lblError.Text = ""
        lblDetalleEquipo.Visible = False
        Call Limpiar_Equipos()
        Call Lista_Inspeccion()
        lblEditarInspeccion.Visible = False
        lblDetalleInspeccion.Visible = False
    End Sub
    Protected Sub btnGrabarEquipo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Panel3.Visible = False
        Dim obj As New Insertar
        Dim objList As New clsInv_Listados
        Dim dtList As New DataTable
        Dim objProceso As New clsInv_Procesos
        Dim objInsUpdDel As New clsInspeccion_InsUpdDel
        Dim SerieEnum As Double = 0
        Dim NroPlaca As Double = 0
        Dim CodArticulo As Double = 0
        Dim Oficina As Double = 0
        Dim Equipo As String = "0"
        Dim Bateria As String = "0"
        Dim NroInspec As Double = 0
        Dim Codart As String = ""
        Dim CodOficina As Double = 0
        Dim placa As Double = 0
        Dim serie As String = "0"
        Dim dtSerie_Placa As New DataTable
        Dim codUbica As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        SerieEnum = Nz(txtSerieEnum.Text)
        CodArticulo = Nz(txtCodigoEdicionArticulo.Text)
        NroPlaca = Nz(txtNroPlaca.Text)
        Oficina = Nz(txtCodInternoOficina.Text)
        NroInspec = Nz(txtNroInspeccionEquipo.Text.Trim)
        placa = Nz(txtNroPlaca.Text.Trim)
        serie = txtNroSerie.Text.Trim
        CodOficina = Nz(txtCodInternoOficina.Text)
        Codart = txtArticulo.Text
        codUbica = Nz(txtcodigoUbicacion.Text.Trim)
        lblErrorEquipo.Text = ""
        If txtNroSerie.Text = "" Then lblErrorEquipo.Text = "<br> - Ingresar Nro de Serie."
        If txtArticulo.Text = "" And txtCodigoEdicionArticulo.Text = "" Then lblErrorEquipo.Text = lblErrorEquipo.Text & "<br> - Ingresar Artículo."
        If lblErrorEquipo.Text <> "" Then
            lblErrorEquipo.Text = "Se han encontrado las sgtes. observaciones: " & lblErrorEquipo.Text
            Exit Sub
        End If
        Try
            If lblEqDetalle.Text = "Ingresar Equipo" Then
                If ChkIngresarEquipo.Checked = True Then
                    dtList = objList.Listado_Articulos_NroSerie(psConexion, txtNroSerie.Text.Trim)
                    If dtList.Rows.Count = 0 Then
                        dtSerie_Placa = objList.BuscarXSerie_Placa(psConexion, Session("CodEmpresa"), CodOficina, placa, serie, Codart)
                        If dtSerie_Placa.Rows.Count = 1 Then
                            lblErrorEquipo.Visible = True
                            lblErrorEquipo.Text = "Equipo ya Existe"
                            Exit Sub
                        Else
                            If IngresarEquipo() = "1" Then
                                Exit Sub
                            Else
                                lblErrorEquipo.Visible = True
                                lblErrorEquipo.Text = "Equipo Registrado"
                                lblDetalleEquipo.Visible = False
                            End If
                        End If
                    End If
                Else
                    dtList = objList.Listado_Articulos_NroSerie(psConexion, txtNroSerie.Text.Trim)
                    If dtList.Rows.Count = 1 Then
                        If txtUbicaTipo.Text = "2" Then
                            If txtCodInternoOficina.Text <> txtcodigoUbicacion.Text Then
                                objProceso.Salida_Ingreso_Automatico(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtUbicaTipo.Text.Trim, "2", txtcodigoUbicacion.Text, _
                                                                      txtCodInternoOficina.Text, txtSerieEnum.Text, txtArticulo.Text)
                                lblErrorEquipo.Visible = True
                                lblErrorEquipo.Text = "Equipo Registrado"
                                lblDetalleEquipo.Visible = False
                            End If
                        Else
                            If txtCodInternoOficina.Text <> txtcodigoUbicacion.Text Then
                                objProceso.Salida_Ingreso_Automatico(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtUbicaTipo.Text.Trim, "2", txtcodigoUbicacion.Text, _
                                                                      txtCodInternoOficina.Text, txtSerieEnum.Text, txtArticulo.Text)
                                lblErrorEquipo.Visible = True
                                lblErrorEquipo.Text = "Equipo Registrado"
                                lblDetalleEquipo.Visible = False
                            End If
                        End If
                    End If
                End If
            ElseIf lblEqDetalle.Text = "Ingresar IP" Then
                If txtNroIp.Text.Trim = "" Then lblErrorEquipo.Text = "Ingresar nuemro de Ip." : Exit Sub
                objInsUpdDel.Del_Ip(psConexion, Session("CodEmpresa"), SerieEnum, txtNroIp.Text.Trim)
                objInsUpdDel.Ins_Ip(psConexion, Session("CodEmpresa"), SerieEnum, txtNroIp.Text.Trim)
            End If
            Dim psCodOfInsp As Double = 0
            psCodOfInsp = txtCodInternoOficina.Text
            FlexArticulos.DataSource = objList.Listar_Equipos(psConexion, Session("CodEmpresa"), psCodOfInsp)
            FlexArticulos.DataBind()
            btnCancelarEquipos_Click(sender, e)
        Catch ex As SqlException
            lblErrorEquipo.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorEquipo.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Protected Sub btnBuscarXPlaca_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim dt As DataTable
        Dim Placa As Double = 0
        Placa = Nz(txtNroPlaca.Text)
        Dim ubicacion As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        ubicacion = Nz(txtcodigoUbicacion.Text)
        dt = obj.BuscarXSerie_Placa(psConexion, Session("CodEmpresa"), ubicacion, Placa, 0, 0)
        If dt.Rows.Count = 1 Then
            For Each dr As Data.DataRow In dt.Rows
                lblErrorEquipo.Text = ""
                txtNroSerie.Text = Nu(dr("SERIE_NRO"))
                txtNroPlaca.Text = Nu(dr("PLACA_NRO"))
                txtArticulo.Text = Nu(dr("CODIGO_ARTICULO"))
                txtDescArticulo.Text = Nu(dr("DESCRIPCION"))
                txtOficina.Text = Nu(dr("RUC"))
                txtDescOficina.Text = Nu(dr("OFICINA"))
                txtSerieEnum.Text = Nu(dr("SERIE_NUMERAR"))
                txtcodigoUbicacion.Text = Nu(dr("UBICACT_CODIGO"))
                txtMarca.Text = Nu(dr("MARCA"))
                txtModelo.Text = Nu(dr("MODELO"))
                txtCodigoEdicionArticulo.Text = Nu(dr("ARTICULO_CODIGO"))
                txtCodigoMarca.Text = Nu(dr("CODMARCA"))
                txtCodigoModelo.Text = Nu(dr("CODMODELO"))
                txtUbicaTipo.Text = Nu(dr("UBICACT_TIPO"))
            Next
        Else
            Limpiar_Equipos_Placa()
            lblErrorEquipo.Visible = True
            lblErrorEquipo.Text = "No se ha Encontrado Equipo"
            txtSerieOPlaca.Text = "3"
            Call Mostrar_Oficina()
            Exit Sub
        End If
        txtSerieOPlaca.Text = "2"
    End Sub
    Protected Sub btnBuscarSerie_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim dt As DataTable
        Dim Placa As Double = 0
        Placa = Nz(txtNroPlaca.Text)
        Dim ubicacion As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        ubicacion = Nz(txtcodigoUbicacion.Text)
        dt = obj.BuscarXSerie_Placa(psConexion, Session("CodEmpresa"), ubicacion, 0, txtNroSerie.Text.Trim, 0)
        If dt.Rows.Count = 1 Then
            For Each dr As Data.DataRow In dt.Rows
                lblErrorEquipo.Text = ""
                txtNroSerie.Text = Nu(dr("SERIE_NRO"))
                txtNroPlaca.Text = Nu(dr("PLACA_NRO"))
                txtArticulo.Text = Nu(dr("CODIGO_ARTICULO"))
                txtDescArticulo.Text = Nu(dr("DESCRIPCION"))
                txtOficina.Text = Nu(dr("RUC"))
                txtDescOficina.Text = Nu(dr("OFICINA"))
                txtSerieEnum.Text = Nu(dr("SERIE_NUMERAR"))
                txtcodigoUbicacion.Text = Nu(dr("UBICACT_CODIGO"))
                txtMarca.Text = Nu(dr("MARCA"))
                txtModelo.Text = Nu(dr("MODELO"))
                txtCodigoEdicionArticulo.Text = Nu(dr("ARTICULO_CODIGO"))
                txtCodigoMarca.Text = Nu(dr("CODMARCA"))
                txtCodigoModelo.Text = Nu(dr("CODMODELO"))
                txtUbicaTipo.Text = Nu(dr("UBICACT_TIPO"))
            Next
        Else
            Call Limpiar_Equipos_Serie()
            lblErrorEquipo.Visible = True
            lblErrorEquipo.Text = "No se ha Encontrado Equipo"
            txtSerieOPlaca.Text = "3"
            Call Mostrar_Oficina()
            Exit Sub
        End If
        txtSerieOPlaca.Text = "1"
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Limpiar_Equipos()
    End Sub
    Sub Limpiar_Equipos()
        txtNroSerie.Text = ""
        txtNroPlaca.Text = ""
        txtMarca.Text = ""
        txtArticulo.Text = ""
        txtOficina.Text = ""
        txtDescOficina.Text = ""
        txtDescArticulo.Text = ""
        txtModelo.Text = ""
        cboEstadoEquipo.SelectedValue = "< Seleccionar >"
        cboEstadoBateria.SelectedValue = "< Seleccionar >"
        lblErrorEquipo.Text = ""
        txtSerieEnum.Text = ""
        txtCodigoEdicionArticulo.Text = ""
        FlexInspeccionCampos.Columns.Clear()
    End Sub
    Sub Limpiar_Equipos_Placa()
        txtNroSerie.Text = ""
        txtMarca.Text = ""
        txtArticulo.Text = ""
        txtOficina.Text = ""
        txtDescArticulo.Text = ""
        txtDescOficina.Text = ""
        txtModelo.Text = ""
        cboEstadoEquipo.SelectedValue = "< Seleccionar >"
        cboEstadoBateria.SelectedValue = "< Seleccionar >"
    End Sub
    Sub Limpiar_Equipos_Serie()
        txtNroPlaca.Text = ""
        txtMarca.Text = ""
        txtArticulo.Text = ""
        txtOficina.Text = ""
        txtDescArticulo.Text = ""
        txtDescOficina.Text = ""
        txtModelo.Text = ""
        cboEstadoEquipo.SelectedValue = "< Seleccionar >"
        cboEstadoBateria.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub btnCancelarEquipos_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Limpiar_Equipos()
        lblDetalleEquipo.Visible = False
        ModalPopupExtender5.Hide()
        Panel3.Visible = False
    End Sub
    Protected Sub btnCancelarEdicion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblEditarInspeccion.Visible = False
        Ficha.Height = 450
        Call Limpiar_edicion()
    End Sub
    Sub Limpiar_edicion()
        txtEditarOficina.Text = ""
        txtEditarDescOficina.Text = ""
        txtEditarRucTipoPersona.Text = ""
        txtEditarRazonSocialTipoPersona.Text = ""
        cboEditarTipoInspecc.SelectedValue = "< Seleccionar >"
        cboEditarTipoPersona.SelectedValue = "< Seleccionar >"
        txtEditar.Text = ""
        txtEditarCodOficina.Text = ""
        txtEditarTipoPersona.Text = ""
        txtEditarCodigoTecnico.Text = ""
        txtEditarTipoInspecc.Text = ""
        txtEditarFechaProg.Text = ""
        txtEditarHoraProg.Text = ""
        cboTta.SelectedValue = "< Seleccionar >"
        cboTsi.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub cboEditarTipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtEditarRucTipoPersona.Text = ""
        txtEditarRazonSocialTipoPersona.Text = ""
    End Sub
    Protected Sub cboEditarTipoInspecc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    End Sub
    Protected Sub cboEditarTipoPersona_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtEditarRucTipoPersona.Text = ""
        txtEditarRazonSocialTipoPersona.Text = ""
    End Sub
    Protected Sub btnBuscarArticulo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender5.Show()
        Panel3.Visible = True
    End Sub
    Sub Limpiar_BuscarArticulos()
        txtEdicionCodArticulo.Text = ""
        txtEdicionDescArticulo.Text = ""
        txtNroParte.Text = ""
    End Sub
    Protected Sub btnListarEdicionArt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim CodEdicionArticulo As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        CodEdicionArticulo = Nz(txtEdicionCodArticulo.Text)
        Try
            FlexEdicionArticulos.DataSource = obj.BuscarX_Articulos(psConexion, Session("CodEmpresa"), CodEdicionArticulo, txtEdicionDescArticulo.Text.Trim, txtNroParte.Text.Trim)
            FlexEdicionArticulos.DataBind()
            lblBARegistro.Text = "Se encontrarón " & FlexEdicionArticulos.Rows.Count & " registros."
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
        ModalPopupExtender5.Show()
    End Sub
    Sub Limpiar_Filtros()
        txtPorCodOficina.Text = ""
        txtPorOficDescrip.Text = ""
        cboTipoPersona.SelectedValue = "< Seleccionar >"
        txtRucTipoPersona.Text = ""
        txtRazonSocialTipoPersona.Text = ""
        cboEstadoInspeccion.SelectedValue = "< Seleccionar >"
        cboTipoInspeccion.SelectedValue = "< Seleccionar >"
        txtPorFechaInicio.Text = ""
        txtPorFechaFin.Text = ""
    End Sub
    Protected Sub FlexEdicionArticulos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            'lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtArticulo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEdicionArticulos.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDescArticulo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEdicionArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtCodigoEdicionArticulo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexEdicionArticulos.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                FlexEdicionArticulos.DataSource = Nothing
                FlexEdicionArticulos.DataBind()
                Call Limpiar_BuscarArticulos()
                ModalPopupExtender5.Hide()
            End If
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnEditarOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub btnEditarTipoPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ModalPopupExtender2.Hide()
    End Sub
    Protected Sub ChkIngresarEquipo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtSerieOPlaca.Text = "1" And ChkIngresarEquipo.Checked = True Then
            Call Limpiar_Equipos_Serie()
            Mostrar_Oficina()
        End If
        If txtSerieOPlaca.Text = "2" And ChkIngresarEquipo.Checked = True Then
            Call Limpiar_Equipos_Placa()
            Mostrar_Oficina()
        End If
        If ChkIngresarEquipo.Checked = True And txtSerieOPlaca.Text = "" Then
            Call Limpiar_Equipos()
            Mostrar_Oficina()
        End If
        If ChkIngresarEquipo.Checked = False Then
            txtSerieOPlaca.Text = ""
            Call Limpiar_Equipos()
        End If
    End Sub
    Protected Sub btnGuardarEdicion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New Insertar
        Dim objInsUpdDel As New clsInspeccion_InsUpdDel
        Dim NroInsp As Double = 0
        Dim CodOficina As Double = 0
        Dim fechareali As String = "20100101"
        Dim horaProg As String = ""
        Dim psTta As String = ""
        Dim psTsi As String = ""
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblEdError.Text = ""
        NroInsp = Nz(txtEditarNroInspeccion.Text.Trim)
        CodOficina = Nz(txtEditarCodOficina.Text.Trim)
        fechareali = Right(txtEditarFechaProg.Text.Trim, 4) & Mid(txtEditarFechaProg.Text.Trim, 4, 2) & Left(txtEditarFechaProg.Text.Trim, 2)
        horaProg = Left(txtEditarHoraProg.Text.Trim, 2) & Right(txtEditarHoraProg.Text.Trim, 2)
        Try
            If txtEditarOficina.Text = "" Or txtEditarDescOficina.Text = "" Then
                lblEdError.Text = "Ingresar Oficina" : Exit Sub
            End If
            If txtEditarRucTipoPersona.Text = "" And txtEditarRazonSocialTipoPersona.Text = "" Then
                lblEdError.Text = "Ingresar Tecnico" : Exit Sub
            End If
            If cboEditarTipoInspecc.SelectedValue = "< Seleccionar >" Then
                lblEdError.Text = "Ingresar Tipo" : Exit Sub
            End If
            If txtEditarFechaProg.Text = "" Then
                lblEdError.Text = "Ingresar Fecha Realizada" : Exit Sub
            End If
            If txtEditarHoraProg.Text = "" Then
                lblEdError.Text = "Ingresar Hora Programada" : Exit Sub
            End If
            objInsUpdDel.Upd_Inspeccion_Edicion2(psConexion, Session("CodEmpresa"), NroInsp, _
                                           txtEditarTipoPersona.Text.Trim, CodOficina, txtEditarTipoInspecc.Text.Trim, txtEditarCodigoTecnico.Text.Trim, fechareali, horaProg)
            If cboTta.SelectedValue <> "< Seleccionar >" Or cboTsi.SelectedValue <> "< Seleccionar >" Then
                If cboTta.SelectedValue <> "< Seleccionar >" Then psTta = cboTta.SelectedValue.Trim Else psTta = "NULL"
                If cboTsi.SelectedValue <> "< Seleccionar >" Then psTsi = cboTsi.SelectedValue.Trim Else psTsi = "NULL"
                objInsUpdDel.Upd_Oficina_Tablero(psConexion, Session("CodEmpresa"), cboTta.SelectedValue.Trim, cboTsi.SelectedValue.Trim, CodOficina)
            End If
            Call Limpiar_edicion()
            Call Lista_Inspeccion()
            lblEditarInspeccion.Visible = False
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub cboEditarTipoInspecc_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboEditarTipoInspecc.SelectedValue <> "< Seleccionar >" Then
            txtEditarTipoInspecc.Text = cboEditarTipoInspecc.SelectedValue
        Else
            txtEditarTipoInspecc.Text = ""
        End If
    End Sub
    Protected Sub cboEditarTipoPersona_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboEditarTipoPersona.SelectedValue <> "< Seleccionar >" Then
            txtEditarTipoPersona.Text = cboEditarTipoPersona.SelectedValue
            txtEditarRucTipoPersona.Text = ""
            txtEditarRazonSocialTipoPersona.Text = ""
            txtEditarCodigoTecnico.Text = ""
        Else
            txtEditarTipoPersona.Text = ""
            txtEditarRucTipoPersona.Text = ""
            txtEditarRazonSocialTipoPersona.Text = ""
            txtEditarCodigoTecnico.Text = ""
        End If
    End Sub
    Private Sub Lista_Observacion_Detalle(ByVal psCodInspec As Double, ByVal psSerieNumerar As Double, _
                                          ByVal psTextBox As TextBox, ByVal psTipoCampo As String)
        lblErrorDetalleEquipo.Text = ""
        Dim dt As DataTable
        Dim objLista As New clsInspeccion_Listado
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        psTextBox.Text = ""
        Try
            dt = objLista.Listar_Detalle_Observacion(psConexion, Session("CodEmpresa"), psCodInspec, psSerieNumerar, psTipoCampo)
            If dt.Rows.Count = 1 Then
                For Each dr As DataRow In dt.Rows
                    psTextBox.Text = Nu(dr("INSPDET_OBS"))
                Next
            End If
        Catch ex As SqlException
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Private Sub ListaDetalle_Equipo()
        lblErrorDetalleEquipo.Text = ""
        Dim dt As DataTable
        Dim obj As New clsInspeccion_Listado
        Dim i As Integer = 0
        Dim NroInspec As Double = 0
        Dim pdSerieNumerar As Double = 0
        Dim pdCodCampo As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        pdSerieNumerar = Nz(txtSerie_Numerar_Detalle.Text.Trim)
        NroInspec = Nz(txtNroInspeccionEquipoDetalle.Text.Trim)
        Try
            For i = 0 To FlexInspeccionCampos.Rows.Count - 1
                pdCodCampo = FlexInspeccionCampos.Rows(i).Cells(6).Text
                dt = obj.Listar_Inspeccion_Detalle_Equipo(psConexion, Session("CodEmpresa"), NroInspec, cboAreaInspeccion.SelectedValue, pdCodCampo, pdSerieNumerar)
                If dt.Rows.Count > 0 Then
                    For Each drow As Data.DataRow In dt.Rows
                        FlexInspeccionCampos.Rows(i).Cells(2).Text = Nu(drow("INSPECDET_VALOR"))
                        FlexInspeccionCampos.Rows(i).Cells(4).Text = Nu(drow("INSPECDET_OBS"))
                    Next
                End If
            Next
        Catch ex As SqlException
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboAreaInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorDetalleEquipo.Text = ""
        Dim obj As New clsInspeccion_Listado
        Dim NroInsp As Double = 0
        Dim SerieEnum As Double = 0
        Dim CodTipoClasif As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        txtObservacionDetalleCampo.Text = ""
        CodTipoClasif = Nz(txtCodClasifica.Text.Trim)
        SerieEnum = Nz(txtSerie_Numerar_Detalle.Text.Trim)
        NroInsp = Nz(txtNroInspeccionEquipoDetalle.Text.Trim)
        Try
            If cboAreaInspeccion.SelectedValue <> "< Seleccionar >" Then
                Call Lista_Observacion_Detalle(NroInsp, SerieEnum, txtObservacionDetalleCampo, cboAreaInspeccion.SelectedValue.Trim)
                FlexInspeccionCampos.DataSource = obj.Listar_Inspeccion_Campos(psConexion, Session("CodEmpresa"), _
                                                                               cboAreaInspeccion.SelectedValue, CodTipoClasif)
                FlexInspeccionCampos.DataBind()
                Call ListaDetalle_Equipo()
            End If
        Catch ex As SqlException
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnGuardarCamposInspeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New Insertar
        Dim i As Integer = 0
        Dim objInsUpdDel As New clsInspeccion_InsUpdDel
        Dim NroInsp As Double = 0
        Dim SerieEnum As Double = 0
        Dim tValor As TextBox
        Dim tObs As TextBox
        Dim DetCampo As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        SerieEnum = Nz(txtSerie_Numerar_Detalle.Text.Trim)
        NroInsp = Nz(txtNroInspeccionEquipoDetalle.Text.Trim)
        Try
            objInsUpdDel.Del_Detalle_Equipo(psConexion, Session("CodEmpresa"), NroInsp, SerieEnum)
            For i = 0 To FlexInspeccionCampos.Rows.Count - 1
                tValor = FlexInspeccionCampos.Rows(i).Cells(3).FindControl("txtValorDetalleEquipo")
                If tValor.Text.Trim = "" Then tValor.Text = FlexInspeccionCampos.Rows(i).Cells(2).Text
                tObs = FlexInspeccionCampos.Rows(i).Cells(5).FindControl("txtObsDetalleEquipo")
                If tObs.Text.Trim = "" Then tObs.Text = FlexInspeccionCampos.Rows(i).Cells(4).Text
                DetCampo = Nz(FlexInspeccionCampos.Rows(i).Cells(6).Text)
                If tValor.Text <> "" Or tObs.Text <> "" Then
                    objInsUpdDel.Ins_Detalle_Equipo(psConexion, Session("CodEmpresa"), NroInsp, _
                                                    DetCampo, tValor.Text, tObs.Text, cboAreaInspeccion.SelectedValue, SerieEnum)
                End If
            Next
            objInsUpdDel.Del_Detalle_Observacion(psConexion, Session("CodEmpresa"), NroInsp, SerieEnum)
            objInsUpdDel.Ins_Detalle_Observacion(psConexion, Session("CodEmpresa"), NroInsp, SerieEnum, _
                                                 txtObservacionDetalleCampo.Text.Trim, cboAreaInspeccion.SelectedValue.Trim)
            lblErrorDetalleEquipo.Visible = True
            lblErrorDetalleEquipo.Text = "Detalle de Inspeccion Registrado"
            txtDetalleValor.Text = ""
            txtDetalleCampo.Text = ""
            txtObservacionDetalleCampo.Text = ""
            txtObservacionDetalleCampo.Text = ""
            Call cboAreaInspeccion_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorDetalleEquipo.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnCancelarCamposInspeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Sub Mostrar_Oficina()
        Dim Ruc As String = ""
        Dim RazonSocial As String = ""
        Ruc = txtRucOfic.Text
        RazonSocial = txtDescripOficina.Text
        txtOficina.Text = Ruc
        txtDescOficina.Text = RazonSocial
    End Sub
    Protected Sub btnRegresarEquipos_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objInv_lis As New clsInv_Listados
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 550
        FlexArticulos.DataSource = objInv_lis.Listar_Equipos(psConexion, Session("CodEmpresa"), txtcodigoUbicacion.Text)
        FlexArticulos.DataBind()
    End Sub
    Protected Sub btnRegresarEquipo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 550
        lblError.Text = ""
        lblDetallCampoEquipo.Visible = False
        lblDetalleEquipo.Visible = False
        Call LimpiarDetalleEquipo()
    End Sub
    Sub LimpiarDetalleEquipo()
        txtNroSerieDetalle.Text = ""
        txtArticuloDetalle.Text = ""
        txtDescArticuloDetalle.Text = ""
        txtNroPlacaDetalle.Text = ""
        txtOficinaDetalle.Text = ""
        txtDescOficinaDetalle.Text = ""
        txtMarcaDetalle.Text = ""
        txtModeloDetalle.Text = ""
        cboAreaInspeccion.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub cboTipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboTipoPersona.SelectedValue = "3" Then
            txtcodigoPersona.Text = "3"
            txtRucTipoPersona.Text = ""
            txtRazonSocialTipoPersona.Text = ""
            txtEditar.Text = "0"
            btnBuscarTipoPersona.Enabled = True
        ElseIf cboTipoPersona.SelectedValue = "1" Then
            txtcodigoPersona.Text = "1"
            txtRucTipoPersona.Text = ""
            txtRazonSocialTipoPersona.Text = ""
            txtEditar.Text = "0"
            btnBuscarTipoPersona.Enabled = True
        Else
            txtRucTipoPersona.Text = ""
            txtRazonSocialTipoPersona.Text = ""
            btnBuscarTipoPersona.Enabled = False
        End If
    End Sub
    Protected Sub FlexInspeccionCampos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtDetalleCampo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexInspeccionCampos.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtDetalleValor.Text = CType(FlexInspeccionCampos.Rows(Index).Cells(4).FindControl("TextBox3"), TextBox).Text
                txtObservacionDetalleCampo.Text = CType(FlexInspeccionCampos.Rows(Index).Cells(6).FindControl("TextBox4"), TextBox).Text
            End If
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnDetalleInspeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer = 0
        Dim dt As New DataTable
        Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
        lblDetalleEquipo.Visible = False
        lblDetalleInspeccion.VISIBLE = True
        Call LlenaComboItem("TBOPC377", cboDetalleAreaInspeccion, psCnGrEmp)
        cboDetalleAreaInspeccion.Items.Add("< Seleccionar >")
        cboDetalleAreaInspeccion.SelectedValue = "< Seleccionar >"
        TextBox5.Text = ""
        For i = 0 To FlexDetalleXInspeccion.Rows.Count - 1
            FlexDetalleXInspeccion.Rows(i).Cells(0).Text = ""
            FlexDetalleXInspeccion.Rows(i).Cells(1).Text = ""
            FlexDetalleXInspeccion.Rows(i).Cells(2).Text = ""
            FlexDetalleXInspeccion.Rows(i).Cells(3).Text = ""
            FlexDetalleXInspeccion.Rows(i).Cells(4).Text = ""
            FlexDetalleXInspeccion.Rows(i).Cells(5).Text = ""
            FlexDetalleXInspeccion.Rows(i).Cells(6).Text = ""
        Next
    End Sub
    Protected Sub cboDetalleAreaInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInspeccion_Listado
        Dim NroInsp As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        NroInsp = Nz(txtDetalleNroInspeccion.Text.Trim)
        Try
            If cboDetalleAreaInspeccion.SelectedValue <> "< Seleccionar >" Then
                Call Lista_Observacion_Detalle(NroInsp, 0, TextBox5, cboDetalleAreaInspeccion.SelectedValue.Trim)
                FlexDetalleXInspeccion.DataSource = obj.Listar_Inspeccion_Campos_Oficina(psConexion, Session("CodEmpresa"), cboDetalleAreaInspeccion.SelectedValue)
                FlexDetalleXInspeccion.DataBind()
                Call ListaDetalle()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListaDetalle()
        Dim dt As DataTable
        Dim obj As New clsInspeccion_Listado
        Dim i As Integer = 0
        Dim NroInspec As Double = 0
        Dim pdCodCampo As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        NroInspec = Nz(txtDetalleNroInspeccion.Text.Trim)
        Try
            For i = 0 To FlexDetalleXInspeccion.Rows.Count - 1
                pdCodCampo = FlexDetalleXInspeccion.Rows(i).Cells(6).Text
                dt = obj.Listar_Inspeccion_Detalle_Oficina(psConexion, Session("CodEmpresa"), NroInspec, cboDetalleAreaInspeccion.SelectedValue, pdCodCampo)
                If dt.Rows.Count > 0 Then
                    For Each drow As Data.DataRow In dt.Rows
                        FlexDetalleXInspeccion.Rows(i).Cells(2).Text = Nu(drow("INSPECDET_VALOR"))
                        FlexDetalleXInspeccion.Rows(i).Cells(4).Text = Nu(drow("INSPECDET_OBS"))
                    Next
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub btnGuardarDetalleInspeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim objInsUpdDel As New clsInspeccion_InsUpdDel
        Dim i As Integer = 0
        Dim NroInspec As Double = 0
        Dim tValor As TextBox
        Dim tObs As TextBox
        Dim DetCampo As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        NroInspec = Nz(txtDetalleNroInspeccion.Text.Trim)
        lblErrorEquipo.Text = ""
        Try
            objInsUpdDel.Del_Detalle_Oficina(psConexion, Session("CodEmpresa"), NroInspec)
            For i = 0 To FlexDetalleXInspeccion.Rows.Count - 1
                tValor = FlexDetalleXInspeccion.Rows(i).Cells(3).FindControl("TxtValorDetalle")
                tObs = FlexDetalleXInspeccion.Rows(i).Cells(5).FindControl("txtObsDetalle")
                If tValor.Text.Trim = "" Then tValor.Text = FlexDetalleXInspeccion.Rows(i).Cells(2).Text
                If tObs.Text.Trim = "" Then tObs.Text = FlexDetalleXInspeccion.Rows(i).Cells(4).Text
                DetCampo = Nz(FlexDetalleXInspeccion.Rows(i).Cells(6).Text)
                If tValor.Text <> "" Or tObs.Text <> "" Then
                    objInsUpdDel.Ins_Detalle_Oficina(psConexion, Session("CodEmpresa"), NroInspec, _
                                                     DetCampo, tValor.Text, tObs.Text, cboDetalleAreaInspeccion.SelectedValue)
                End If
            Next
            objInsUpdDel.Del_Detalle_Observacion(psConexion, Session("CodEmpresa"), NroInspec, 0)
            objInsUpdDel.Ins_Detalle_Observacion(psConexion, Session("CodEmpresa"), NroInspec, 0, _
                                                 TextBox5.Text.Trim, cboDetalleAreaInspeccion.SelectedValue.Trim)
            Call cboDetalleAreaInspeccion_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblErrorEquipo.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblErrorEquipo.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnCancelarDetalleInspeccion_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblDetalleInspeccion.Visible = False
    End Sub
    Protected Sub btnIngresarDetalle_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim dt As New DataTable
        Dim Codarticulo As String = ""
        Dim placa As Double = 0
        Dim serie As String = "0"
        Dim CodOficina As Double = 0
        Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 550
        lblDetallCampoEquipo.Visible = True
        placa = Nz(txtNroPlaca.Text.Trim)
        serie = txtNroSerie.Text.Trim
        CodOficina = Nz(txtCodInternoOficina.Text)
        Codarticulo = Nz(txtCodigoEdicionArticulo.Text.Trim)
        txtNroInspeccionEquipoDetalle.Text = txtNroInspeccionEquipo.Text
        Call LlenaComboItem("TBOPC383", cboEstadoEquipoInsp, psCnGrEmp)
        Call LlenaComboItem("TBOPC384", cboEstadoBateriaInpec, psCnGrEmp)
        cboEstadoEquipoInsp.Items.Add("< Seleccionar >")
        cboEstadoEquipoInsp.SelectedValue = "< Seleccionar >"
        cboEstadoBateriaInpec.Items.Add("< Seleccionar >")
        cboEstadoBateriaInpec.SelectedValue = "< Seleccionar >"
        Try
            dt = obj.BuscarXSerie_Placa(psConexion, Session("CodEmpresa"), CodOficina, placa, serie, Codarticulo)
            If dt.Rows.Count = 1 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtNroSerieDetalle.Text = Nu(dr("SERIE_NRO"))
                    txtArticuloDetalle.Text = Nu(dr("CODIGO_ARTICULO"))
                    txtDescArticuloDetalle.Text = Nu(dr("DESCRIPCION"))
                    txtOficinaDetalle.Text = Nu(dr("RUC"))
                    txtDescOficinaDetalle.Text = Nu(dr("OFICINA"))
                    txtNroPlacaDetalle.Text = Nu(dr("PLACA_NRO"))
                    txtMarcaDetalle.Text = Nu(dr("MARCA"))
                    txtModeloDetalle.Text = Nu(dr("MODELO"))
                    txtCodigoEdicionArticulo.Text = Nu(dr("ARTICULO_CODIGO"))
                    txtSerie_Numerar_Detalle.Text = Nu(dr("SERIE_NUMERAR"))
                    txtCodClasifica.Text = Nu(dr("ART_CLASIFICACION"))
                    If Nu(dr("ESTADO_EQUIPO")) = "" Then cboEstadoEquipoInsp.SelectedValue = "< Seleccionar >" Else cboEstadoEquipoInsp.SelectedValue = Nu(dr("ESTADO_EQUIPO"))
                    If Nu(dr("ESTADO_BATERIA")) = "" Then cboEstadoBateriaInpec.SelectedValue = "< Seleccionar >" Else cboEstadoBateriaInpec.SelectedValue = Nu(dr("ESTADO_BATERIA"))
                Next
            End If
            Call btnCancelarCamposInspeccion_Click(sender, e)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Limpia_IngArt()
     End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged

    End Sub
    Protected Sub btnArtNuevo_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoArticulo.Visible = True
        Dim dt As New DataTable
        Dim pdCodArticulo As String = "0"
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        txtBArtClasif.Text = ""
        txtBArtCodigo.Text = ""
        txtBArtDescrip.Text = ""
        txtBArtParte.Text = ""
        txtBusCodigo.Text = ""
        Dim obj As New clsInv_Listados
        Try
            Call obj.Llena_TablaInformacion(psConexion, Session("CodEmpresa"), "4", cboBArtTipo)
            Call obj.Llena_TablaInformacion(psConexion, Session("CodEmpresa"), "11", cboBArtUnidad)
            cboBArtMarca.Items.Add("< Seleccionar >") : cboBArtMarca.SelectedValue = "< Seleccionar >"
            cboBArtModelo.Items.Add("< Seleccionar >") : cboBArtModelo.SelectedValue = "< Seleccionar >"
            Call Limpia_IngArt()
            dt = obj.Devolver_UltimoCodArticulo(psConexion, Session("Codempresa"))
            If dt.Rows.Count = 1 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    pdCodArticulo = Nz(drMenuItem("ART_CODIGO")) + 1
                Next
            End If
            dt = Nothing
            txtBArtCodigo.Text = Formato_Digito(pdCodArticulo, 8)
        Catch ex As SqlException
            lblbArtError.Text = ex.Message
        Catch ex As Exception
            lblbArtError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnBArtCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoArticulo.Visible = False
        lblArbol.Visible = False
        lstClasif.Nodes.Clear()
    End Sub
    Protected Sub cboBArtMarca_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInv_Listados
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If cboBArtMarca.SelectedValue <> "< Seleccionar >" Then
            Call obj.Llena_Modelo(psConexion, Session("CodEmpresa"), cboBArtMarca.SelectedValue.Trim, txtBArtCodClasif.Text, cboBArtModelo)
        Else
            Call obj.Llena_Modelo(psConexion, Session("CodEmpresa"), "0", "0", cboBArtModelo)
        End If
    End Sub
    Protected Sub btnClasif_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblArbol.Visible = True
        lstClasif.Nodes.Clear()
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Dim i As Double = 0
        Dim obj As New clsInv_Listados
        Dim dt_N1 As DataTable
        Dim grupo As String = ""
        Dim modulo As String = ""
        Dim nodoN2 As New TreeNode()
        Dim nodoN3 As New TreeNode()
        Dim nodoN4 As New TreeNode()
        Dim nodoN5 As New TreeNode()
        Dim nodoN6 As New TreeNode()
        Dim nodoN7 As New TreeNode()
        Dim nodoN8 As New TreeNode()
        Dim nodoN9 As New TreeNode()
        Dim nodoN10 As New TreeNode()
        Dim psAgregar As Boolean = True
        Dim psEntra As Boolean = True
        Try
            dt_N1 = obj.Llena_Clasif_N1(psConexion, Session("CodEmpresa"), 1)
            If dt_N1.Rows.Count > 0 Then
                For Each dr_N1 As Data.DataRow In dt_N1.Rows
                    nodoN2 = New TreeNode(dr_N1("CLAS_NUMERO").ToString() & " - " & dr_N1("CLAS_NOMBRE").ToString(), dr_N1("CLAS_CODIGO").ToString())
                    Dim dt_N2 As DataTable = obj.Llena_Clasif_N2(psConexion, Session("CodEmpresa"), 2, dr_N1("CLAS_CODIGO").ToString())
                    If dt_N2.Rows.Count > 0 Then
                        For Each dr_N2 As Data.DataRow In dt_N2.Rows
                            nodoN3 = New TreeNode(dr_N2("CLAS_NUMERO").ToString() & " - " & dr_N2("CLAS_NOMBRE").ToString(), dr_N2("CLAS_CODIGO").ToString())
                            Dim dt_N3 As DataTable = obj.Llena_Clasif_N3(psConexion, Session("CodEmpresa"), 3, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString())
                            If dt_N3.Rows.Count > 0 Then
                                For Each dr_N3 As Data.DataRow In dt_N3.Rows
                                    nodoN4 = New TreeNode(dr_N3("CLAS_NUMERO").ToString() & " - " & dr_N3("CLAS_NOMBRE").ToString(), dr_N3("CLAS_CODIGO").ToString())
                                    Dim dt_N4 As DataTable = obj.Llena_Clasif_N4(psConexion, Session("CodEmpresa"), 4, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString(), _
                                                                                 dr_N3("CLAS_CODIGO").ToString())
                                    If dt_N4.Rows.Count > 0 Then
                                        For Each dr_N4 As Data.DataRow In dt_N4.Rows
                                            nodoN5 = New TreeNode(dr_N4("CLAS_NUMERO").ToString() & " - " & dr_N4("CLAS_NOMBRE").ToString(), dr_N4("CLAS_CODIGO").ToString())
                                            Dim dt_N5 As DataTable = obj.Llena_Clasif_N5(psConexion, Session("CodEmpresa"), 5, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString(), _
                                                                                         dr_N3("CLAS_CODIGO").ToString(), dr_N4("CLAS_CODIGO").ToString())
                                            If dt_N5.Rows.Count > 0 Then
                                                For Each dr_N5 As Data.DataRow In dt_N5.Rows
                                                    nodoN6 = New TreeNode(dr_N5("CLAS_NUMERO").ToString() & " - " & dr_N5("CLAS_NOMBRE").ToString(), dr_N5("CLAS_CODIGO").ToString())
                                                    Dim dt_N6 As DataTable = obj.Llena_Clasif_N6(psConexion, Session("CodEmpresa"), 6, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString(), _
                                                                                                 dr_N3("CLAS_CODIGO").ToString(), dr_N4("CLAS_CODIGO").ToString(), dr_N5("CLAS_CODIGO").ToString())
                                                    If dt_N6.Rows.Count > 0 Then
                                                        For Each dr_N6 As Data.DataRow In dt_N6.Rows
                                                            nodoN7 = New TreeNode(dr_N6("CLAS_NUMERO").ToString() & " - " & dr_N6("CLAS_NOMBRE").ToString(), dr_N6("CLAS_CODIGO").ToString())
                                                            Dim dt_N7 As DataTable = obj.Llena_Clasif_N7(psConexion, Session("CodEmpresa"), 7, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString(), _
                                                                                                         dr_N3("CLAS_CODIGO").ToString(), dr_N4("CLAS_CODIGO").ToString(), dr_N5("CLAS_CODIGO").ToString(), _
                                                                                                         dr_N6("CLAS_CODIGO").ToString())
                                                            If dt_N7.Rows.Count > 0 Then
                                                                For Each dr_N7 As Data.DataRow In dt_N7.Rows
                                                                    nodoN8 = New TreeNode(dr_N7("CLAS_NUMERO").ToString() & " - " & dr_N7("CLAS_NOMBRE").ToString(), dr_N7("CLAS_CODIGO").ToString())
                                                                    Dim dt_N8 As DataTable = obj.Llena_Clasif_N8(psConexion, Session("CodEmpresa"), 8, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString(), _
                                                                                                                 dr_N3("CLAS_CODIGO").ToString(), dr_N4("CLAS_CODIGO").ToString(), dr_N5("CLAS_CODIGO").ToString(), _
                                                                                                                 dr_N6("CLAS_CODIGO").ToString(), dr_N7("CLAS_CODIGO").ToString())
                                                                    If dt_N8.Rows.Count > 0 Then
                                                                        For Each dr_N8 As Data.DataRow In dt_N8.Rows
                                                                            nodoN9 = New TreeNode(dr_N8("CLAS_NUMERO").ToString() & " - " & dr_N8("CLAS_NOMBRE").ToString(), dr_N8("CLAS_CODIGO").ToString())
                                                                            Dim dt_N9 As DataTable = obj.Llena_Clasif_N9(psConexion, Session("CodEmpresa"), 9, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString(), _
                                                                                                                         dr_N3("CLAS_CODIGO").ToString(), dr_N4("CLAS_CODIGO").ToString(), dr_N5("CLAS_CODIGO").ToString(), _
                                                                                                                         dr_N6("CLAS_CODIGO").ToString(), dr_N7("CLAS_CODIGO").ToString(), dr_N8("CLAS_CODIGO").ToString())
                                                                            If dt_N9.Rows.Count > 0 Then
                                                                                For Each dr_N9 As Data.DataRow In dt_N9.Rows
                                                                                    nodoN10 = New TreeNode(dr_N9("CLAS_NUMERO").ToString() & " - " & dr_N9("CLAS_NOMBRE").ToString(), dr_N9("CLAS_CODIGO").ToString())
                                                                                    Dim dt_N10 As DataTable = obj.Llena_Clasif_N10(psConexion, Session("CodEmpresa"), 10, dr_N1("CLAS_CODIGO").ToString(), dr_N2("CLAS_CODIGO").ToString(), _
                                                                                                                                 dr_N3("CLAS_CODIGO").ToString(), dr_N4("CLAS_CODIGO").ToString(), dr_N5("CLAS_CODIGO").ToString(), _
                                                                                                                                 dr_N6("CLAS_CODIGO").ToString(), dr_N7("CLAS_CODIGO").ToString(), dr_N8("CLAS_CODIGO").ToString(), _
                                                                                                                                 dr_N9("CLAS_CODIGO").ToString())
                                                                                    If dt_N10.Rows.Count > 0 Then
                                                                                        For Each dr_N10 As Data.DataRow In dt_N10.Rows
                                                                                            nodoN10.ChildNodes.Add(New TreeNode(dr_N10("CLAS_NUMERO").ToString() & " - " & dr_N10("CLAS_NOMBRE").ToString(), dr_N10("CLAS_CODIGO").ToString()))
                                                                                        Next
                                                                                    End If
                                                                                    nodoN9.ChildNodes.Add(nodoN10)
                                                                                Next
                                                                            End If
                                                                            nodoN8.ChildNodes.Add(nodoN9)
                                                                        Next
                                                                    End If
                                                                    nodoN7.ChildNodes.Add(nodoN8)
                                                                Next
                                                            End If
                                                            nodoN6.ChildNodes.Add(nodoN7)
                                                        Next
                                                    End If
                                                    nodoN5.ChildNodes.Add(nodoN6)
                                                Next
                                            End If
                                            nodoN4.ChildNodes.Add(nodoN5)
                                        Next
                                    End If
                                    nodoN3.ChildNodes.Add(nodoN4)
                                Next
                            End If
                            nodoN2.ChildNodes.Add(nodoN3)
                        Next
                    End If
                    lstClasif.Nodes.Add(nodoN2)
                Next
            End If
        Catch ex As SqlException
            lblbArtError.Text = ex.Message
        Catch ex As Exception
            lblbArtError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnBArtGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblbArtError.Text = ""
        Dim obj As New clsInv_InsUpdDel
        Dim pdCodArt As Double = 0
        Dim pdTipo As Double = 0
        Dim pdClasif As Double = 0
        Dim pdMarca As Double = 0
        Dim pdModelo As Double = 0
        Dim pdUnidad As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        lblbArtError.Text = ""
        If txtBArtCodigo.Text = "" Then lblbArtError.Text = " <br> - Debe ingresar el código del artículo."
        If cboBArtTipo.SelectedValue = "< Seleccionar >" Then lblbArtError.Text = lblbArtError.Text & " <br> - Debe seleccionar tipo de artículo."
        If txtBArtParte.Text = "" Then lblbArtError.Text = lblbArtError.Text & " <br> - Debe ingresar el Nro de parte del artículo."
        If txtBArtClasif.Text.Trim = "" And txtBArtCodClasif.Text = "" Then lblbArtError.Text = lblbArtError.Text & " <br> - Debe seleccionar clasificación del artículo."
        If txtBArtDescrip.Text = "" Then lblbArtError.Text = lblbArtError.Text & " <br> - Debe ingresar la descripción del artículo."
        If cboBArtUnidad.SelectedValue = "< Seleccionar >" Then lblbArtError.Text = lblbArtError.Text & " <br> - Debe seleccionar unidad de medida del artículo."
        If lblbArtError.Text <> "" Then
            lblbArtError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblbArtError.Text : Exit Sub
        End If
        pdCodArt = txtBArtCodigo.Text.Trim
        pdClasif = txtBArtCodClasif.Text.Trim
        If cboBArtTipo.SelectedValue = "< Seleccionar >" Then pdTipo = 0 Else pdTipo = cboBArtTipo.SelectedValue.Trim
        If cboBArtMarca.SelectedValue = "< Seleccionar >" Then pdMarca = 0 Else pdMarca = cboBArtMarca.SelectedValue.Trim
        If cboBArtModelo.SelectedValue = "< Seleccionar >" Then pdModelo = 0 Else pdModelo = cboBArtModelo.SelectedValue.Trim
        If cboBArtUnidad.SelectedValue = "< Seleccionar >" Then pdUnidad = 0 Else pdUnidad = cboBArtUnidad.SelectedValue.Trim
        Try
            obj.Ins_Articulo(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, _
                                   pdCodArt, txtBArtDescrip.Text.Trim, txtBArtParte.Text.Trim, pdClasif, pdTipo, _
                                   pdUnidad, pdMarca, pdModelo)
            lblIngresoArticulo.Visible = False
        Catch ex As SqlException
            lblbArtError.Text = ex.Message
        Catch ex As Exception
            lblbArtError.Text = ex.Message
        End Try
    End Sub
    Protected Sub lstClasif_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblbArtError.Text = ""
        Dim psCodClasif As Double = lstClasif.SelectedValue
        Dim dt As DataTable
        Dim dt_N As DataTable
        Dim i As Integer = 0
        Dim psCampo As String = ""
        Dim psDescripcion As String = ""
        Dim objLis As New clsInv_Listados
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        Try
            dt = objLis.Buscar_xCodClasif(Session("Ruta_Emp"), Session("CodEmpresa"), psCodClasif)
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    If dr("CLAS_COD_NIVEL") > 1 Then
                        For i = 2 To dr("CLAS_COD_NIVEL").ToString
                            psCampo = "CLAS_NIVEL" & i
                            dt_N = objLis.Buscar_xCodClasif(psConexion, Session("CodEmpresa"), dr(psCampo))
                            For Each dr_N As Data.DataRow In dt_N.Rows
                                If psDescripcion <> "" Then psDescripcion = psDescripcion & ", "
                                psDescripcion = psDescripcion & dr_N("CLAS_NOMBRE")
                            Next
                        Next
                    Else
                        lblbArtError.Text = "Debe seleccionar a partir del 2º Nivel." : Exit Sub
                    End If
                Next
            End If
            Dim psValor As String = lstClasif.SelectedNode.Text
            Dim posicion As Integer = InStr(psValor, "-")
            txtBArtClasif.Text = Left(psValor, posicion - 2)
            txtBArtCodClasif.Text = lstClasif.SelectedValue
            txtBArtDescripClasif.Text = psDescripcion
            txtBArtDescrip.Text = txtBArtDescripClasif.Text
            Dim obj As New clsInv_Listados
            Call obj.Llena_Marca(psConexion, Session("CodEmpresa"), lstClasif.SelectedValue, cboBArtMarca)
            lblArbol.Visible = False
            lstClasif.Nodes.Clear()
        Catch ex As SqlException
            lblbArtError.Text = ex.Message
        Catch ex As Exception
            lblbArtError.Text = ex.Message
        End Try
    End Sub
    Protected Sub FlexEdicionArticulos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub lstClasif_TreeNodeExpanded(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles lstClasif.TreeNodeExpanded

    End Sub
    Protected Sub btnBArtAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim psMarca As String = ""
        Dim psModelo As String = ""
        If cboBArtMarca.SelectedValue <> "< Seleccionar >" And cboBArtModelo.SelectedValue = "< Seleccionar >" Then
            psMarca = cboBArtMarca.SelectedItem.Text
            txtBArtDescrip.Text = txtBArtDescripClasif.Text & " " & psMarca
        ElseIf cboBArtMarca.SelectedValue <> "< Seleccionar >" And cboBArtModelo.SelectedValue <> "< Seleccionar >" Then
            psMarca = cboBArtMarca.SelectedItem.Text
            psModelo = cboBArtModelo.SelectedItem.Text
            txtBArtDescrip.Text = txtBArtDescripClasif.Text & " " & psMarca & " " & psModelo
        End If
    End Sub
    Protected Sub btnIngMarca_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtBArtClasif.Text.Trim = "" And txtBArtCodClasif.Text = "" Then lblbArtError.Text = "Debe seleccionar clasificación del artículo." : Exit Sub
        lblIngMarca.Visible = True
        txtMarDescripcion.Text = ""
        lblbArtError.Text = ""
    End Sub
    Protected Sub btnMarCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngMarca.Visible = False
        txtMarDescripcion.Text = ""
        lblbArtError.Text = ""
    End Sub
    Protected Sub btnMarGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim psCodClasif As Double = 0
        Dim obj As New clsInv_Listados
        Dim objInsUpDel As New clsInv_InsUpdDel
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        psCodClasif = txtBArtCodClasif.Text
        If txtMarDescripcion.Text = "" Then lblbArtError.Text = "Debe ingresar la descripción de la marca." : Exit Sub
        Try
            objInsUpDel.Ins_Marca(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, _
                               txtMarDescripcion.Text, psCodClasif)
            Call obj.Llena_Marca(psConexion, Session("CodEmpresa"), psCodClasif, cboBArtMarca)
            lblIngMarca.Visible = False
            txtMarDescripcion.Text = ""
            lblbArtError.Text = ""
        Catch ex As SqlException
            lblbArtError.Text = ex.Message
        Catch ex As Exception
            lblbArtError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnIngModelo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboBArtMarca.SelectedValue = "< Seleccionar >" Then lblbArtError.Text = "Debe seleccionar la marca." : Exit Sub
        lblIngModelo.Visible = True
        txtModDescripcion.Text = ""
        lblbArtError.Text = ""
    End Sub
    Protected Sub btnModCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngModelo.Visible = False
        txtModDescripcion.Text = ""
        lblbArtError.Text = ""
    End Sub
    Protected Sub btnModGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim psCodClasif As Double = 0
        Dim obj As New clsInv_Listados
        Dim objIns As New clsInv_InsUpdDel
        Dim psCodMarca As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        psCodClasif = txtBArtCodClasif.Text
        psCodMarca = cboBArtMarca.SelectedValue.Trim
        If txtModDescripcion.Text = "" Then lblbArtError.Text = "Debe ingresar la descripción del modelo." : Exit Sub
        Try
            objIns.Ins_Modelo(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, _
                               txtModDescripcion.Text, psCodClasif, psCodMarca)
            Call obj.Llena_Modelo(psConexion, Session("CodEmpresa"), cboBArtMarca.SelectedValue.Trim, txtBArtCodClasif.Text, cboBArtModelo)
            lblIngModelo.Visible = False
            txtModDescripcion.Text = ""
            lblbArtError.Text = ""
        Catch ex As SqlException
            lblbArtError.Text = ex.Message
        Catch ex As Exception
            lblbArtError.Text = ex.Message
        End Try
    End Sub
    Protected Sub FlexArticulos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnBuscarTipoPersona_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub
    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub txtEncargadoParticipante_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub cboEstadoFinal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboEstadoFinal.SelectedValue = "1" Then
            txtObsEstadofinal.Enabled = True
            cboResponsable.Enabled = True
            txtFecSolPosible.Enabled = True
            cboTipificacion.Enabled = True
        Else
            txtObsEstadofinal.Enabled = False
            cboResponsable.Enabled = False
            txtFecSolPosible.Enabled = False
            cboTipificacion.Enabled = False
        End If
    End Sub
    Protected Sub chkOfVerificacion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkOfVerificacion.Checked = True Then
            txtFecVerificacion.Enabled = True
        Else
            txtFecVerificacion.Enabled = False
        End If
    End Sub
End Class



