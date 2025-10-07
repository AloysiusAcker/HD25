Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Cas_ListaIncidentes_GrupoNivel2
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
        FlexDet.DataSource = Nothing
        FlexDet.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.Height = 420
                Ficha.ActiveTabIndex = 0
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
            Dim dt As New DataTable
            Dim dtConsulta As New DataTable
            Dim dtListado As New DataTable
            Dim dRow As Data.DataRow
            Dim obj As New ModuloCas
            Dim i As Integer : i = 0
            Dim pEstado As String : pEstado = "0"
            Dim pCodigo As Integer : pCodigo = 0
            Dim pImportancia As String : pImportancia = "0"
            Dim pTipo As String : pTipo = "0"
            Dim pComponente As Integer : pComponente = 0
            Dim pElemento As Integer : pElemento = 0
            Dim pFechaIni As String : pFechaIni = "%"
            Dim pFechaFin As String : pFechaFin = "%"
            Dim pCodGrupo As Double = 0
            If chkComponente.Checked = True And cboComponente.SelectedValue = "< Seleccionar >" Then lblErrorF.Text = "Seleccionar Componente." : Exit Sub
            If chkTipo.Checked = True And cboTipo.SelectedValue = "< Seleccionar >" Then lblErrorF.Text = "Seleccionar Tipo." : Exit Sub
            If chkElemento.Checked = True And cboElemento.SelectedValue = "< Seleccionar >" Then lblErrorF.Text = "Seleccionar Elemento." : Exit Sub
            If chkImportancia.Checked = True And cboImportancia.SelectedValue = "< Seleccionar >" Then lblErrorF.Text = "Seleccionar Importancia." : Exit Sub
            If chkImportancia.Checked = True And cboImportancia.SelectedValue <> "< Seleccionar >" Then pImportancia = cboImportancia.SelectedValue.Trim
            If chkTipo.Checked = True And cboTipo.SelectedValue <> "< Seleccionar >" Then pTipo = cboTipo.SelectedValue.Trim
            If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then pComponente = cboComponente.SelectedValue.Trim
            If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then
                If chkElemento.Checked = True And cboElemento.SelectedValue <> "< Seleccionar >" Then
                    pComponente = cboComponente.SelectedValue.Trim
                    pElemento = cboElemento.SelectedValue.Trim
                End If
            End If
            dt.Columns.Add("COD_PROBLEMA")
            dt.Columns.Add("APROB_FECHA_REPORTA")
            dt.Columns.Add("APROB_HORA_REPORTA")
            dt.Columns.Add("pEstado")
            dt.Columns.Add("PRIORIDAD")
            dt.Columns.Add("NIVEL1_DESCRIP")
            dt.Columns.Add("NOM_PROB1_NOM_PROB2")
            dt.Columns.Add("APROB_PROBLEMA_DESCRIPCION")
            dt.Columns.Add("APROB_USUARIO_REPORTA")
            dt.Columns.Add("TBCAS_PERSONA_APELLIDOS")
            dt.Columns.Add("BANCO_OFICINA")
            dt.Columns.Add("APROB_ASIGNADO_PERSONA")
            dt.Columns.Add("NOMBRESU")
            dt.Columns.Add("APROB_ESTADO")
            dt.Columns.Add("INC_SEGUIMIENTO")
            dt.Columns.Add("APROB_REDIREC_PERSONA")
            dt.Columns.Add("APROB_USUARIO_REGISTRA")
            pCodGrupo = cboGrupo.SelectedValue.Trim
            dtListado = obj.CasLista_IncidentesUsuario(Session("CodEmpresa"), pEstado, pCodigo, pImportancia, pComponente, pElemento, pTipo, cboGrupo.SelectedValue.Trim, "3",Session("Ruta_Emp"))
            If dtListado.Rows.Count > 0 Then
                dtConsulta = obj.CasConsulta_ExisteGrupo(pCodGrupo, "2", "1",Session("Ruta_Emp"))
                If dtConsulta.Rows.Count = 1 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        If Nu(drMenuItem("APROB_ASIGNADO_PERSONA")) = cboGrupo.SelectedValue.Trim Then
                            dRow = dt.NewRow
                            dRow("COD_PROBLEMA") = Nu(drMenuItem("COD_PROBLEMA"))
                            dRow("APROB_FECHA_REPORTA") = FormatoFecha(Nu(drMenuItem("APROB_FECHA_REPORTA")))
                            dRow("APROB_HORA_REPORTA") = Left(Nu(drMenuItem("APROB_HORA_REPORTA")), 2) + ":" + Right(Nu(drMenuItem("APROB_HORA_REPORTA")), 2)
                            dRow("pEstado") = Nu(drMenuItem("pEstado"))
                            dRow("PRIORIDAD") = Nu(drMenuItem("PRIORIDAD"))
                            dRow("NIVEL1_DESCRIP") = Nu(drMenuItem("NIVEL1_DESCRIP"))
                            dRow("NOM_PROB1_NOM_PROB2") = Nu(drMenuItem("NOM_PROB1")) & IIf(Nu(drMenuItem("NOM_PROB2")) = "", "", " ; " + Nu(drMenuItem("NOM_PROB2")))
                            dRow("APROB_PROBLEMA_DESCRIPCION") = Nu(drMenuItem("APROB_PROBLEMA_DESCRIPCION"))
                            dRow("APROB_USUARIO_REPORTA") = Nu(drMenuItem("APROB_USUARIO_REPORTA"))
                            dRow("TBCAS_PERSONA_APELLIDOS") = Nu(drMenuItem("TBCAS_PERSONA_APELLIDOS")) & ", " & Nu(drMenuItem("TBCAS_PERSONA_NOMBRE"))
                            dRow("BANCO_OFICINA") = IIf(Nu(drMenuItem("BANCO_OFICINA")) = "", Nu(drMenuItem("BANCO_OFICINA2")), Nu(drMenuItem("BANCO_OFICINA")))
                            dRow("APROB_ASIGNADO_PERSONA") = Nu(drMenuItem("APROB_ASIGNADO_PERSONA"))
                            dRow("NOMBRESU") = Nu(drMenuItem("NOMBRESU"))
                            dRow("APROB_ESTADO") = Nu(drMenuItem("APROB_ESTADO"))
                            dRow("INC_SEGUIMIENTO") = Nu(drMenuItem("INC_SEGUIMIENTO"))
                            dRow("APROB_REDIREC_PERSONA") = Nu(drMenuItem("APROB_REDIREC_PERSONA"))
                            dRow("APROB_USUARIO_REGISTRA") = Nu(drMenuItem("APROB_USUARIO_REGISTRA"))
                            dt.Rows.Add(dRow)
                        End If
                    Next
                End If
            End If
            dtListado = Nothing
            Flex.DataSource = dt
            Flex.DataBind()
            For i = 0 To Flex.Rows.Count - 1
                If Flex.Rows(i).Cells(16).Text.Trim = "1" Then
                    Flex.Rows(i).BackColor = Drawing.Color.YellowGreen
                End If
            Next
        Catch Ex As SqlException
            lblErrorF.Visible = True
            lblErrorF.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorF.Visible = True
            lblErrorF.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblErrorF.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorF.Text = ""
        Dim pEstado As String = ""
        Dim pCodigo As Double : pCodigo = 0
        If e.CommandName = "Solucion" Then
            Try
                pCodigo = Flex.Rows(Index).Cells(2).Text.Trim
                FlexDet.DataSource = Lista_Solucion(Session("CodEmpresa"), pCodigo, Replace(Flex.Rows(Index).Cells(15).Text.Trim, "&nbsp;", ""), Replace(Flex.Rows(Index).Cells(16).Text.Trim, "&nbsp;", ""),Session("Ruta_Emp"))
                FlexDet.DataBind()
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
        If e.CommandName = "Mostrar" Then
            txtIncidente.Text = Flex.Rows(Index).Cells(2).Text.Trim
            txtNEstado.Text = Flex.Rows(Index).Cells(15).Text.Trim
            txtNSeguimiento.Text = Replace(Flex.Rows(Index).Cells(16).Text.Trim, "&nbsp;", "")
            txtUserRedirec.Text = Replace(Flex.Rows(Index).Cells(17).Text.Trim, "&nbsp;", "")
            txtUserRegistra.Text = Replace(Flex.Rows(Index).Cells(18).Text.Trim, "&nbsp;", "")
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.Height = 420
            btnGrabar.Visible = True
            btnRedireccionar.Visible = True
            btnTerminar.Visible = True
            btnRegresar.Visible = True
            Call Tipos_Criterio("2", cboNImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call Tipos_Criterio("1", cboNTipo, Session("CodEmpresa"), Session("Ruta_Emp"))
            Ficha_ActiveTabChanged(sender, e)
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Tipos_Criterio("2", cboImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call Tipos_Criterio("1", cboTipo, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call LLenaComboItemTabEsp(cboComponente, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
            Dim obj As New ModuloCas
            Dim dt As New DataTable
            Call Cargar_Grupo(cboGrupo, Session("Ruta_Emp"))
            dt = obj.CasConsulta_ExisteGrupoxUsuario(0, HttpContext.Current.User.Identity.Name, "2",Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    cboGrupo.SelectedValue = Nu(dr("GRUPO_COD"))
                Next
            End If
            dt = Nothing
            Call Llenar_Grilla()
            FlexDet.DataSource = Nothing
            FlexDet.DataBind()
            btnGrabar.Enabled = False
            btnRedireccionar.Enabled = False
            btnTerminar.Enabled = False
            btnRegresar.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha.Height = 420
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call Cargar_Informacion(sender, e, txtNEstado.Text.Trim, txtNSeguimiento.Text.Trim, txtUserRedirec.Text.Trim, txtUserRegistra.Text.Trim)
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.Height = 420
        End If
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim Problema As Double, Tipo2 As Double, Tipo3 As Double
        Dim obj As New ModuloCas
        Dim dt As New DataTable
        Dim pSeguimiento As String
        Dim pCodIncidente As Double
        Dim pIniLlamada As String
        lblError.Text = ""
        If cboNComponente.SelectedValue = "< Seleccionar >" Then lblError.Text = "Falta definir el Tipo de Problema para poder guardar el registro." : Exit Sub
        If txtNSolucion.Text = "" Then lblError.Text = "Debe ingresar la Solución del Problema." : Exit Sub
        Problema = cboNComponente.SelectedValue.Trim
        If cboNElemento.SelectedValue = "< Seleccionar >" Then Tipo2 = 0 Else Tipo2 = cboNElemento.SelectedValue.Trim
        If cboNElemento2.Text = "" Then
            cboNElemento2.Items.Add("< Seleccionar >")
            cboNElemento2.SelectedValue = "< Seleccionar >"
        End If
        If cboNElemento2.SelectedValue = "< Seleccionar >" Then Tipo3 = 0 Else Tipo3 = cboNElemento2.SelectedValue.Trim
        pCodIncidente = txtIncidente.Text.Trim
        Try
            dt = obj.CasLista_IncidenteDetalle(Session("CodEmpresa"), txtIncidente.Text.Trim,Session("Ruta_Emp"))
            If dt.Rows.Count = 1 Then
                obj.InsUpd_IncidenteDetalle(Session("CodEmpresa"), pCodIncidente, QuitaComilla(txtNSolucion.Text.Trim), HttpContext.Current.User.Identity.Name, "3",Session("Ruta_Emp"))
            Else
                obj.InsUpd_IncidenteDetalle(Session("CodEmpresa"), pCodIncidente, QuitaComilla(txtNSolucion.Text.Trim), HttpContext.Current.User.Identity.Name, "1",Session("Ruta_Emp"))
            End If
            dt = Nothing
            pIniLlamada = Right(txtIniLlamada.Text.Trim, 4) & Mid(txtIniLlamada.Text.Trim, 4, 2) & Left(txtIniLlamada.Text.Trim, 2)
            pSeguimiento = IIf(chkNSeguimiento.Checked = True, "1", "0")
            obj.InsUpd_Incidente(Session("CodEmpresa"), Problema, HttpContext.Current.User.Identity.Name, "", pCodIncidente, cboNImportancia.SelectedValue.Trim, Tipo2, Tipo3, QuitaComilla(txtNDescripcion.Text.Trim), "", "8", 0, "", "", "", pIniLlamada, pSeguimiento, "", "", "",Session("Ruta_Emp"))
            btnRegresar_Click(sender, e)
            txtIniLlamada.Text = "00:00:00"
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarR.Click
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 420
        btnListar_Click(sender, e)
    End Sub
    Private Sub Cargar_Informacion(ByVal sender As Object, ByVal e As System.EventArgs, ByVal psEstado As String, ByVal psSeguimiento As String, ByVal psUserRedirec As String, ByVal psUserRegistra As String)
        Dim P1 As String = "0"
        Dim P2 As String = "0"
        Dim P3 As String = "0"
        Dim pCodIncidente As Double
        Dim dt As DataTable
        Dim obj As New ModuloCas
        Dim pCodGrupo As Double = 0
        If txtIncidente.Text.Trim = "" Then Exit Sub
        pCodIncidente = txtIncidente.Text.Trim
        Session("IncCodigo") = txtIncidente.Text.Trim
        chkNModificar.Checked = False
        chkNModificar.Enabled = True
        chkNSeguimiento.Enabled = True
        lblRedireccion2.Text = ""
        txtIniLlamada.Text = ""
        pCodGrupo = IIf(psUserRedirec = "", 0, psUserRedirec)
        txtIniLlamada.Text = FormatoHoraSeg(HoraActual(True))
        dt = obj.CasConsulta_ExisteUsuario(HttpContext.Current.User.Identity.Name)
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                lblNombreUsuarioSistema.Text = " " + Nu(dr("USUARI_CODIGO")) + " - " + Nu(dr("NOMBRESU"))
            Next
        End If
        dt = Nothing
        dt = obj.CasConsulta_ExisteUsuario(psUserRegistra)
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                lblUserRegistra.Text = " " + Nu(dr("USUARI_CODIGO")) + " - " + Nu(dr("NOMBRESU"))
            Next
        End If
        dt = Nothing
        Try
            If psEstado <> "5" Or psEstado <> "6" Then
                dt = obj.CasConsulta_ExisteUsuario(psUserRegistra)
                If dt.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dt.Rows
                        lblUserRegistra.Text = " " + Nu(dr("usuari_codigo")) + " - " + Nu(dr("NOMBRESU"))
                    Next
                End If
                dt = Nothing
                chkNSeguimiento.Checked = IIf(psSeguimiento = "1" Or psSeguimiento = "2", 1, 0)
                If psEstado = "8" Then
                    lblRedireccion1.Visible = True
                    lblRedireccion2.Visible = True
                    dt = obj.CasConsulta_ExisteUsuario(psUserRedirec)
                    If dt.Rows.Count > 0 Then
                        For Each dr As Data.DataRow In dt.Rows
                            lblRedireccion2.Text = " " + Nu(dr("usuari_codigo")) + " - " + Nu(dr("NOMBRESU"))
                        Next
                    End If
                    dt = Nothing
                    If lblRedireccion2.Text = "" Then
                        dt = obj.CasConsulta_ExisteGrupo(psUserRedirec, "", "1",Session("Ruta_Emp"))
                        If dt.Rows.Count > 0 Then
                            For Each dr As Data.DataRow In dt.Rows
                                lblRedireccion2.Text = " " + Nu(dr("GRUPO_NOMBRE"))
                            Next
                        End If
                        dt = Nothing
                    End If
                Else
                    lblRedireccion1.Visible = False
                    lblRedireccion2.Visible = False
                End If
                If psSeguimiento = "1" Then
                    btnRedireccionar.Visible = False
                    btnRedireccionar.Enabled = False
                    btnTerminar.Visible = True
                    btnTerminar.Enabled = True
                    btnGrabar.Enabled = False
                    btnGrabar.Visible = False
                    btnRegresar.Enabled = True
                    chkNSeguimiento.Enabled = False
                Else
                    btnRedireccionar.Visible = True
                    btnRedireccionar.Enabled = True
                    btnTerminar.Visible = False
                    btnGrabar.Enabled = True
                    btnGrabar.Visible = True
                    btnRegresar.Enabled = True
                End If
                dt = obj.CasLista_xIncidente(Session("CodEmpresa"), pCodIncidente,Session("Ruta_Emp"))
                If dt.Rows.Count = 1 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtNUsuario.Text = " " & Nu(dr("APROB_USUARIO_REPORTA"))
                        txtNOficina.Text = " " & IIf(Nu(dr("BANCO_OFICINA")) = "", Nu(dr("BANCO_OFICINA2")), Nu(dr("BANCO_OFICINA")))
                        txtNNombre.Text = " " & Nu(dr("TBCAS_PERSONA_APELLIDOS")) & ", " & Nu(dr("TBCAS_PERSONA_NOMBRE"))
                        txtNTelefono.Text = " " & IIf(Nu(dr("INC_TELEFONO")) = "", Nu(dr("TBCAS_TELEFONO")) & " - " & Nu(dr("TBCAS_ANEXO")), Nu(dr("INC_TELEFONO")))
                        txtNDescripcion.Text = " " & Nu(dr("APROB_PROBLEMA_DESCRIPCION"))
                        Session("IncDescripcion") = " " & Nu(dr("APROB_PROBLEMA_DESCRIPCION"))
                        cboNImportancia.SelectedValue = Nu(dr("APROB_PRIORIDAD")) : cboNImportancia.Enabled = False
                        cboNTipo.SelectedValue = Nu(dr("INC_TIPO")) : cboTipo.Enabled = False
                        Call LLenaComboItemTabEsp(cboNComponente, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp")) '"&#241;"
                        'cboNComponente.Items.Add("< Seleccionar >") : cboNComponente.SelectedValue = "< Seleccionar >"
                        If Nu(dr("APROB_TIPO")) <> "" Then cboNComponente.SelectedValue = Nu(dr("APROB_TIPO")) : cboNComponente_SelectedIndexChanged(sender, e)
                        If Not IsDBNull(dr("APROB_PROBLEMA1")) Then
                            If Nu(dr("APROB_PROBLEMA1")) <> "" Then
                                If Nu(dr("APROB_PROBLEMA1")) <> 0.0 Then
                                    cboNElemento.SelectedValue = Nu(dr("APROB_PROBLEMA1")) : cboNElemento_SelectedIndexChanged(sender, e)
                                End If
                            End If
                        End If
                        If Not IsDBNull(dr("APROB_PROBLEMA2")) Then
                            If Nu(dr("APROB_PROBLEMA2")) <> "" Then
                                If Nu(dr("APROB_PROBLEMA2")) <> 0.0 Then
                                    cboNElemento2.SelectedValue = Nu(dr("APROB_PROBLEMA2"))
                                End If
                            End If
                        End If
                        cboNComponente.Enabled = False : cboNElemento.Enabled = False : cboNElemento2.Enabled = False
                    Next
                End If
                dt = Nothing
                dt = obj.CasLista_xIncidente_Solucion(Session("CodEmpresa"), pCodIncidente,Session("Ruta_Emp"))
                If dt.Rows.Count = 1 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtNSolucion.Text = " " & Nu(dr("DPROB_ACCION_DESCRIPCION"))
                        Session("IncSolucion") = " " & Nu(dr("DPROB_ACCION_DESCRIPCION"))
                    Next
                End If
                If psEstado = "3" Or psEstado = "4" Then
                    obj.InsUpd_Incidente(Session("CodEmpresa"), 0, HttpContext.Current.User.Identity.Name, 0, pCodIncidente, "", 0, 0, "", "", "7", 0, "", "", "", "", "0", "", "", "",Session("Ruta_Emp"))
                End If
            End If
        Catch Ex As SqlException
            lblErrorF.Visible = True
            lblErrorF.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorF.Visible = True
            lblErrorF.Text = "Ha ocurrido un error en la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub chkImportancia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkImportancia.Checked = True Then cboImportancia.Enabled = True Else cboImportancia.Enabled = False
    End Sub
    Protected Sub chkComponente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkComponente.Checked = True Then cboComponente.Enabled = True Else cboComponente.Enabled = False
    End Sub
    Protected Sub chkTipo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkTipo.Checked = True Then cboTipo.Enabled = True Else cboTipo.Enabled = False
    End Sub
    Protected Sub cboComponente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Visible = False
        cboElemento.Items.Clear()
        cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
        cboElemento.Enabled = False
        Call LLenaComboItemTabEsp(cboElemento, cboComponente.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        cboElemento.Enabled = False
        cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub cboElemento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then
            If chkElemento.Checked = True Then cboElemento.Enabled = True Else cboElemento.Enabled = False
        Else
            chkElemento.Checked = False
        End If
    End Sub
    Protected Sub chkElemento_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then
            If chkElemento.Checked = True Then
                cboElemento.Enabled = True
            Else
                cboElemento.Enabled = False
            End If
        Else
            chkElemento.Checked = False
        End If
    End Sub
    Protected Sub cboNComponente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Visible = False
        cboNElemento.Items.Clear()
        cboNElemento2.Items.Clear()
        cboNElemento.Enabled = False
        cboNElemento2.Enabled = False
        Call LLenaComboItemTabEsp(cboNElemento, cboNComponente.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboNComponente.SelectedValue = "< Seleccionar >" Then
            cboNElemento.Enabled = False
        Else
            cboNElemento.Enabled = True
        End If
        txtCodComponente.Text = IIf(cboNComponente.SelectedValue.Trim = "< Seleccionar >", 0, cboNComponente.SelectedValue.Trim)
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboNElemento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Visible = False
        cboNElemento2.Enabled = False
        cboNElemento2.Items.Clear()
        Call LLenaComboItemTabEsp(cboNElemento2, cboNComponente.SelectedValue.Trim, cboNElemento.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboNElemento.SelectedValue = "< Seleccionar >" Then
            cboNElemento2.Enabled = False
        Else
            cboNElemento2.Enabled = True
        End If
    End Sub
    Protected Sub chkNModificar_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkNModificar.Checked = True Then
            cboNComponente.Enabled = True
            cboNElemento.Enabled = True
            cboNElemento2.Enabled = True
        Else
            'btnGrabar_Click(sender, e)
            cboNComponente.Enabled = False
            cboNElemento.Enabled = False
            cboNElemento2.Enabled = False
        End If
    End Sub
    Protected Sub btnTerminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkNSeguimiento.Checked = True Then
            ModalPopupExtender3.Show()
        End If
    End Sub
    Protected Sub FlexG_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexG.PageIndexChanging
        lblErrorR.Text = ""
        FlexG.PageIndex = e.NewPageIndex
        Call optRedireccion_SelectedIndexChanged(sender, e)
    End Sub
    Protected Sub optRedireccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optRedireccion.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim dRow As Data.DataRow
        Dim obj As New ModuloCas
        Dim pCodComponente As Double : pCodComponente = 0

        pCodComponente = cboNComponente.SelectedValue.Trim
        dt.Columns.Add("GRUPO_COD")
        dt.Columns.Add("Grupo")
        dt.Columns.Add("Usuario")
        dt.Columns.Add("NOMBRESP")

        Try
            If optRedireccion.SelectedIndex = "0" Then
                FlexG.Columns(1).HeaderText = "Código"
                FlexG.Columns(2).HeaderText = "Nombre"
                FlexG.Columns(3).HeaderText = ""
                FlexG.Columns(4).HeaderText = ""
                FlexG.Columns(1).ItemStyle.Width = 50
                FlexG.Columns(2).ItemStyle.Width = 180
                FlexG.Columns(3).ItemStyle.Width = 0
                FlexG.Columns(4).ItemStyle.Width = 0
                dtListado = obj.CasLista_TGrupo(pCodComponente,Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        dRow = dt.NewRow
                        dRow("GRUPO_COD") = Nu(drMenuItem("GRUPO_COD"))
                        dRow("Grupo") = Nu(drMenuItem("GRUPO_NOMBRE"))
                        dt.Rows.Add(dRow)
                    Next
                End If
            ElseIf optRedireccion.SelectedIndex = "1" Then
                FlexG.Columns(1).ItemStyle.Width = 0
                FlexG.Columns(2).ItemStyle.Width = 0
                FlexG.Columns(3).ItemStyle.Width = 50
                FlexG.Columns(4).ItemStyle.Width = 180
                FlexG.Columns(1).HeaderText = ""
                FlexG.Columns(2).HeaderText = ""
                FlexG.Columns(3).HeaderText = "Código"
                FlexG.Columns(4).HeaderText = "Combres y Apellidos"
                dtListado = obj.CasLista_TIndividual(pCodComponente,Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        dRow = dt.NewRow
                        dRow("Usuario") = Nu(drMenuItem("Usuario"))
                        dRow("NOMBRESP") = Nu(drMenuItem("NOMBRESP"))
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If
            dtListado = Nothing
            FlexG.DataSource = dt
            FlexG.DataBind()
            dt = Nothing
        Catch Ex As SqlException
            lblErrorR.Visible = True
            lblErrorR.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorR.Visible = True
            lblErrorR.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnBListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBListar.Click
        Call Llenar_GrillaB()
    End Sub
    Private Sub Llenar_GrillaB()
        Dim obj As New Listados
        lblError.Text = ""
        Dim pCodApli As String : pCodApli = 0
        Dim pCodProducto As String : pCodProducto = 0
        Dim pCodSubProd As String : pCodSubProd = 0
        If chkFiltros.Checked = True Then
            If cboNComponente.SelectedValue = "< Seleccionar >" Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboNComponente.SelectedValue.Trim
            If cboNElemento.SelectedValue = "< Seleccionar >" Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboNElemento.SelectedValue.Trim
            If cboNElemento2.SelectedValue = "< Seleccionar >" Then
                pCodSubProd = 0
            Else
                pCodSubProd = cboNElemento2.SelectedValue.Trim
            End If
        End If
        Try
            FlexB.DataSource = Cargar_BD(pCodApli, pCodProducto, pCodSubProd)
            FlexB.DataBind()
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Function Cargar_BD(ByVal pCodApli As String, ByVal pCodProducto As String, ByVal pCodSubProd As String) As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Dim Filtros1 As String : Filtros1 = ""
        Dim Filtros2 As String : Filtros2 = ""
        Dim Opera As String
        Dim Campo1 As String
        Dim Campo2 As String
        Dim cmdSql As New SqlCommand
        Cargar_BD = Nothing
        'Opera = " OR "
        If Trim(txtBuscador.Text.Trim) <> "" And optModoBus.SelectedIndex = -1 Then lblError.Text = "Debe seleccionar un Modo de Busqueda." : Exit Function
        Campo1 = "UPPER(CARCON_TRANSACCION) LIKE "
        Campo2 = "UPPER(CARCON_CONSULTA) LIKE "
        If optModoBus.SelectedValue = 1 Then Opera = " AND " Else Opera = " OR "
        If Trim(txtBuscador.Text.Trim) <> "" Then
            Filtros1 = ArmaFiltros(txtBuscador.Text.Trim, Campo1, Opera)
            Filtros2 = ArmaFiltros(txtBuscador.Text.Trim, Campo2, Opera)
        End If
        Cn2.Open()
        cmdSql.Connection = Cn2
        cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[Lista]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[Lista]"
        cmdSql.ExecuteNonQuery()
        cmdSql.CommandText = "CREATE VIEW Lista AS SELECT CC.EMPRESA_CODIGO, CC.CARCON_SYS_EST, CC.CARCON_CODIGO, CC.CARCON_APLICATIVO, P1.NIVEL1_DESCRIP, CC.CARCON_PRODUCTO, " _
                        & " (SELECT NIVEL2_DESCRIP From dbo.TBESP_CAS2 WHERE (NIVEL2_CODIGO = CC.CARCON_PRODUCTO)) AS PRODUCTO, CC.CARCON_SUBPRODUCTO, " _
                        & " (SELECT NIVEL3_DESCRIP From dbo.TBESP_CAS3 WHERE (NIVEL3_CODIGO = CC.CARCON_SUBPRODUCTO)) AS SUBPRODUCTO, " _
                        & " CC.CARCON_TRANSACCION, CC.CARCON_CONSULTA, CC.CARCON_SOLUCION " _
                        & " FROM dbo.TBCAS_CARTERA_CONSULTA AS CC INNER JOIN dbo.TBESP_CAS1 AS P1 " _
                        & " ON CC.EMPRESA_CODIGO = P1.EMPRESA_CODIGO AND CC.CARCON_APLICATIVO = P1.NIVEL1_CODIGO " _
                        & " WHERE (CC.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CC.CARCON_SYS_EST = '0') " _
                        & " AND (P1.NIVEL1_SYS_EST = '0') AND (P1.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
        If pCodApli <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND  (CARCON_APLICATIVO = " & pCodApli & ") "
        If pCodProducto <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND (CARCON_PRODUCTO   = " & pCodProducto & ") "
        If pCodSubProd <> 0 Then cmdSql.CommandText = cmdSql.CommandText & " AND (CARCON_SUBPRODUCTO= " & pCodSubProd & ")"
        cmdSql.ExecuteNonQuery()
        Sql = " select NIVEL1_DESCRIP, Producto,CARCON_APLICATIVO, subproducto, CARCON_TRANSACCION,CARCON_SUBPRODUCTO, CARCON_CONSULTA, CARCON_SOLUCION,CARCON_PRODUCTO, CARCON_CODIGO " _
            & " FROM Lista WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CARCON_SYS_EST = '0')"
        If Trim(txtBuscador.Text.Trim) <> "" Then Sql = Sql & " AND " & Filtros1
        If Trim(txtBuscador.Text.Trim) <> "" Then Sql = Sql & " OR " & Filtros2
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Me.Page.Session.Timeout = 1080
        Return Dt
    End Function
    Protected Sub FlexB_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexB.PageIndexChanging
        lblError.Text = ""
        FlexB.PageIndex = e.NewPageIndex
        Call Llenar_GrillaB()
    End Sub
    Protected Sub FlexB_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexB.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then '"&gt;"
            If FlexB.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtNDescripcion.Text = txtNDescripcion.Text.Trim & ". " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexB.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            If FlexB.Rows(Index).Cells(6).Text <> "&nbsp;" Then txtNSolucion.Text = txtNSolucion.Text.Trim & ". " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexB.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            If FlexB.Rows(Index).Cells(7).Text = "&nbsp;" Then
                'cboComponente.SelectedValue = "< Seleccionar >"
            ElseIf Nz(FlexB.Rows(Index).Cells(7).Text) = 0 Or FlexB.Rows(Index).Cells(7).Text = "" Then
                'cboComponente.SelectedValue = "< Seleccionar >"
            Else
                cboNComponente.SelectedValue = FlexB.Rows(Index).Cells(7).Text
                cboNComponente_SelectedIndexChanged(sender, e)
            End If
            If FlexB.Rows(Index).Cells(8).Text = "&nbsp;" Then
                'cboElemento.SelectedValue = "< Seleccionar >"
            ElseIf Nz(FlexB.Rows(Index).Cells(8).Text) = 0 Or FlexB.Rows(Index).Cells(8).Text = "" Then
                'cboElemento.SelectedValue = "< Seleccionar >"
            Else
                cboNElemento.SelectedValue = FlexB.Rows(Index).Cells(8).Text
                cboNElemento_SelectedIndexChanged(sender, e)
            End If
            If FlexB.Rows(Index).Cells(9).Text = "&nbsp;" Then
                'cboElemento2.SelectedValue = "< Seleccionar >"
            ElseIf Nz(FlexB.Rows(Index).Cells(9).Text) = 0 Or FlexB.Rows(Index).Cells(9).Text = "" Then
                'cboElemento2.SelectedValue = "< Seleccionar >"
            Else
                cboNElemento2.SelectedValue = FlexB.Rows(Index).Cells(9).Text
            End If
            cboNElemento.Enabled = False
            cboNComponente.Enabled = False
            cboNElemento2.Enabled = False
            ModalPopupExtender2.Hide()
        End If
    End Sub
    Protected Sub btnRedireccionar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub
    Protected Sub txtMotivo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMotivo.TextChanged
        '
    End Sub
    Protected Sub cboNComponente_SelectedIndexChanged1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNComponente.SelectedIndexChanged
        '
    End Sub
    Protected Sub cboGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
        btnRedireccionar.Attributes.Add("onclick", "window.open('Cas_RedireccionarIncidencia.aspx?fnf=fnRedireccion&tit=Redireccionar Incidencia&tpb=Redirec&par1=R&Comp=" & txtCodComponente.Text.Trim & "&Grupo=" & cboGrupo.SelectedValue.Trim & "',null,'left=400, top=200, height=420, width= 360, status=no, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');")
    End Sub
    Protected Sub FlexG_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexG.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorF.Text = ""
        Dim pEstado As String = ""
        Dim pCodigo As Double : pCodigo = 0
        If txtMotivo.Text.Trim = "" Then lblErrorR.Text = "Debe ingresar el Motivo para Redireccionarlo." : Exit Sub
        If optRedireccion.SelectedIndex <> 1 And optRedireccion.SelectedIndex <> 0 Then lblError.Text = "Debe elegir hacia donde se va a Redireccionar la incidencia." : Exit Sub
        If e.CommandName = "Aceptar" Then
            Dim obj As New ModuloCas
            Dim dt As New DataTable
            Dim pdComponente As Double = 0
            Dim pdIncidente As Double = 0
            Dim pTipoUG As String
            Dim pCodigoUG As String
            Session("IncDescripcion") = txtNDescripcion.Text.Trim
            pdIncidente = Session("IncCodigo")
            pdComponente = cboNComponente.SelectedValue.Trim
            pTipoUG = IIf(optRedireccion.SelectedIndex = "0", "0", "1")
            pCodigoUG = IIf(optRedireccion.SelectedIndex = "0", FlexG.Rows(Index).Cells(1).Text, FlexG.Rows(Index).Cells(3).Text)
            If txtMotivo.Text.Trim = "" Then lblErrorR.Text = "Debe ingresar el Motivo para Redireccionarlo." : Exit Sub
            If optRedireccion.SelectedIndex <> 1 And optRedireccion.SelectedIndex <> 0 Then lblError.Text = "Debe elegir hacia donde se va a Redirecciopnar la incidencia." : Exit Sub
            obj.InsUpd_Incidente(Session("CodEmpresa"), pdComponente, pTipoUG, "", pdIncidente, "", 0, 0, QuitaComilla(Session("IncDescripcion")), "", "10", 0, "", "", "", "", "", txtMotivo.Text.Trim, pCodigoUG, IIf(optRedireccion.SelectedIndex = 0, "3", "4"),Session("Ruta_Emp"))
            Try
                If txtMotivo.Text.Trim <> "" Then
                    Session("IncSolucion") = txtNSolucion.Text.Trim
                    dt = obj.CasLista_IncidenteDetalle(Session("CodEmpresa"), pdIncidente,Session("Ruta_Emp"))
                    If dt.Rows.Count = 1 Then
                        obj.InsUpd_IncidenteDetalle(Session("CodEmpresa"), pdIncidente, QuitaComilla(Session("IncSolucion")), "", "5",Session("Ruta_Emp"))
                    Else
                        obj.InsUpd_IncidenteDetalle(Session("CodEmpresa"), pdIncidente, QuitaComilla(Session("IncSolucion")), "", "6",Session("Ruta_Emp"))
                    End If
                End If
            Catch ex As SqlException
                lblErrorR.Text = ex.Message
            Catch Ex As Exception
                lblErrorR.Text = Ex.Message
            Finally
            End Try
            btnRegresar_Click(sender, e)
            ModalPopupExtender1.Hide()
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnSS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSS.Click
        Dim obj As New ModuloCas
        Dim dt As New DataTable
        Dim pCodIncidente As Double = 0
        Dim pComponente As Double = 0
        Dim Tipo2 As Double = 0
        Dim Tipo3 As Double = 0
        pCodIncidente = txtIncidente.Text.Trim
        pComponente = cboNComponente.SelectedValue.Trim
        If cboNElemento.SelectedValue = "< Seleccionar >" Then Tipo2 = 0 Else Tipo2 = cboNElemento.SelectedValue.Trim
        If cboNElemento2.SelectedValue = "< Seleccionar >" Then Tipo3 = 0 Else Tipo3 = cboNElemento2.SelectedValue.Trim
        dt = obj.CasLista_IncidenteDetalle(Session("CodEmpresa"), txtIncidente.Text.Trim,Session("Ruta_Emp"))
        If dt.Rows.Count = 1 Then
            obj.InsUpd_IncidenteDetalle(Session("CodEmpresa"), pCodIncidente, QuitaComilla(txtNSolucion.Text.Trim), HttpContext.Current.User.Identity.Name, "4",Session("Ruta_Emp"))
            obj.InsUpd_Incidente(Session("CodEmpresa"), pComponente, HttpContext.Current.User.Identity.Name, "", pCodIncidente, "", Tipo2, Tipo3, txtNDescripcion.Text.Trim, "", "9", 0, "", "", "", "", "2", "", "", "",Session("Ruta_Emp"))
        Else
            Exit Sub
        End If
        btnRegresar_Click(sender, e)
        ModalPopupExtender3.Hide()
        ModalPopupExtender1.Hide()
        ModalPopupExtender2.Hide()
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnSN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSN.Click
        'Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        'Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        'btnListar_Click(sender, e)
        ModalPopupExtender3.Hide()
    End Sub
End Class
