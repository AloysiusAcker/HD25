Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Text
Imports System.IO
Partial Class Cas_RelacionIncidentes
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
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
    Protected Sub chkImportancia_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkImportancia.CheckedChanged
        If chkImportancia.Checked = True Then cboImportancia.Enabled = True Else cboImportancia.Enabled = False
    End Sub
    Protected Sub chkComponente_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkComponente.CheckedChanged
        If chkComponente.Checked = True Then cboComponente.Enabled = True Else cboComponente.Enabled = False
    End Sub
    Protected Sub cboElemento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboElemento.SelectedIndexChanged
        If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then
            If chkElemento.Checked = True Then cboElemento.Enabled = True Else cboElemento.Enabled = False
        Else
            chkElemento.Checked = False
        End If
    End Sub
    Protected Sub chkEstado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkEstado.CheckedChanged
        If chkEstado.Checked = True Then cboEstado.Enabled = True Else cboEstado.Enabled = False
    End Sub
    Protected Sub chkTipo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkTipo.CheckedChanged
        If chkTipo.Checked = True Then cboTipo.Enabled = True Else cboTipo.Enabled = False
    End Sub
    Private Sub Llenar_Grilla()
        Try
            Dim dt As New DataTable
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
            If chkEstado.Checked = True And cboEstado.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Estado." : Exit Sub
            If chkComponente.Checked = True And cboComponente.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Componente." : Exit Sub
            If chkTipo.Checked = True And cboTipo.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Tipo." : Exit Sub
            If chkElemento.Checked = True And cboElemento.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Elemento." : Exit Sub
            If chkImportancia.Checked = True And cboImportancia.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Importancia." : Exit Sub
            If chkEstado.Checked = True Then pEstado = cboEstado.SelectedValue.Trim
            If txtIncidente.Text <> "" Then pCodigo = txtIncidente.Text.Trim
            If chkImportancia.Checked = True And cboImportancia.SelectedValue <> "< Seleccionar >" Then pImportancia = cboImportancia.SelectedValue.Trim
            If chkTipo.Checked = True And cboTipo.SelectedValue <> "< Seleccionar >" Then pTipo = cboTipo.SelectedValue.Trim
            If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then pComponente = cboComponente.SelectedValue.Trim
            If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then
                If chkElemento.Checked = True And cboElemento.SelectedValue <> "< Seleccionar >" Then
                    pComponente = cboComponente.SelectedValue.Trim
                    pElemento = cboElemento.SelectedValue.Trim
                End If
            End If
            If txtFechaD.Text <> "" Then pFechaIni = Right(txtFechaD.Text, 4) & Mid(txtFechaD.Text, 4, 2) & Left(txtFechaD.Text, 2)
            If txtFechaA.Text <> "" Then pFechaFin = Right(txtFechaA.Text, 4) & Mid(txtFechaA.Text, 4, 2) & Left(txtFechaA.Text, 2)

            dt.Columns.Add("APROB_FECHA_REPORTA")
            dt.Columns.Add("APROB_HORA_REPORTA")
            dt.Columns.Add("pEstado")
            dt.Columns.Add("PRIORIDAD")
            dt.Columns.Add("APROB_CODIGO")
            dt.Columns.Add("NIVEL1_DESCRIP")
            dt.Columns.Add("NOM_PROB1_NOM_PROB2")
            dt.Columns.Add("APROB_PROBLEMA_DESCRIPCION")
            dt.Columns.Add("APROB_USUARIO_REPORTA")
            dt.Columns.Add("TBCAS_PERSONA_APELLIDOS")
            dt.Columns.Add("BANCO_OFICINA")
            dt.Columns.Add("APROB_ASIGNADO_PERSONA")
            dt.Columns.Add("APROB_ESTADO")
            dt.Columns.Add("APROB_TIPO")
            dt.Columns.Add("COD_PROBLEMA")
            dt.Columns.Add("APROB_PROBLEMA1")
            dt.Columns.Add("APROB_PROBLEMA2")
            dt.Columns.Add("APROB_PRIORIDAD")
            dt.Columns.Add("INC_TELEFONO")
            dt.Columns.Add("APROB_ASIGNADO_TIPO")
            dt.Columns.Add("APROB_USUARIO_REGISTRA")
            dt.Columns.Add("INC_SEGUIMIENTO")
            dt.Columns.Add("SEGUIMIENTO")

            dtListado = obj.CasLista_Incidentes(Session("CodEmpresa"), pEstado, pCodigo, pImportancia, pComponente, pElemento, pFechaIni, pFechaFin, pTipo,Session("Ruta_Emp"))
            If dtListado.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dtListado.Rows
                    dRow = dt.NewRow
                    dRow("APROB_FECHA_REPORTA") = FormatoFecha(Nu(drMenuItem("APROB_FECHA_REPORTA")))
                    dRow("APROB_HORA_REPORTA") = Left(Nu(drMenuItem("APROB_HORA_REPORTA")), 2) + ":" + Right(Nu(drMenuItem("APROB_HORA_REPORTA")), 2)
                    dRow("pEstado") = Nu(drMenuItem("pEstado"))
                    dRow("PRIORIDAD") = Nu(drMenuItem("PRIORIDAD"))
                    dRow("APROB_CODIGO") = Nu(drMenuItem("APROB_CODIGO"))
                    dRow("NIVEL1_DESCRIP") = Nu(drMenuItem("NIVEL1_DESCRIP"))
                    dRow("NOM_PROB1_NOM_PROB2") = Nu(drMenuItem("NOM_PROB1")) & IIf(Nu(drMenuItem("NOM_PROB2")) = "", "", " ; " + Nu(drMenuItem("NOM_PROB2")))
                    dRow("APROB_PROBLEMA_DESCRIPCION") = Nu(drMenuItem("APROB_PROBLEMA_DESCRIPCION"))
                    dRow("APROB_USUARIO_REPORTA") = Nu(drMenuItem("APROB_USUARIO_REPORTA"))
                    dRow("TBCAS_PERSONA_APELLIDOS") = Nu(drMenuItem("TBCAS_PERSONA_APELLIDOS")) & ", " & Nu(drMenuItem("TBCAS_PERSONA_NOMBRE"))
                    dRow("BANCO_OFICINA") = IIf(Nu(drMenuItem("BANCO_OFICINA")) = "", Nu(drMenuItem("BANCO_OFICINA2")), Nu(drMenuItem("BANCO_OFICINA")))
                    dRow("APROB_ASIGNADO_PERSONA") = Nu(drMenuItem("APROB_ASIGNADO_PERSONA"))
                    dRow("APROB_ESTADO") = Nu(drMenuItem("APROB_ESTADO"))
                    dRow("APROB_TIPO") = Nu(drMenuItem("APROB_TIPO"))
                    dRow("COD_PROBLEMA") = Nu(drMenuItem("APROB_CODIGO"))
                    dRow("APROB_PROBLEMA1") = Nu(drMenuItem("APROB_PROBLEMA1"))
                    dRow("APROB_PROBLEMA2") = Nu(drMenuItem("APROB_PROBLEMA2"))
                    dRow("APROB_PRIORIDAD") = Nu(drMenuItem("APROB_PRIORIDAD"))
                    dRow("INC_TELEFONO") = IIf(Nu(drMenuItem("INC_TELEFONO")) = "", Nu(drMenuItem("TBCAS_TELEFONO")) & " - " & Nu(drMenuItem("TBCAS_ANEXO")), Nu(drMenuItem("INC_TELEFONO")))
                    dRow("APROB_ASIGNADO_TIPO") = Nu(drMenuItem("APROB_ASIGNADO_TIPO"))
                    dRow("APROB_USUARIO_REGISTRA") = Nu(drMenuItem("APROB_USUARIO_REGISTRA")) & " - " & Nu(drMenuItem("NOMBRESU"))
                    dRow("INC_SEGUIMIENTO") = Nu(drMenuItem("INC_SEGUIMIENTO"))
                    dRow("SEGUIMIENTO") = Nu(drMenuItem("SEGUIMIENTO"))
                    dt.Rows.Add(dRow)
                Next
            End If
            dtListado = Nothing
            Flex.DataSource = dt
            Flex.DataBind()

        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub cmdListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListar.Click
        lblError.Text = ""
        Call Llenar_Grilla()
    End Sub
    Protected Sub btnExpportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExpportar.Click
        lblError.Text = ""
        Dim dt As New DataTable
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
        If chkEstado.Checked = True And cboEstado.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Estado." : Exit Sub
        If chkComponente.Checked = True And cboComponente.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Componente." : Exit Sub
        If chkTipo.Checked = True And cboTipo.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Tipo." : Exit Sub
        If chkElemento.Checked = True And cboElemento.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Elemento." : Exit Sub
        If chkImportancia.Checked = True And cboImportancia.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar Importancia." : Exit Sub
        If chkEstado.Checked = True Then pEstado = cboEstado.SelectedValue.Trim
        If txtIncidente.Text <> "" Then pCodigo = txtIncidente.Text.Trim
        If chkImportancia.Checked = True And cboImportancia.SelectedValue <> "< Seleccionar >" Then pImportancia = cboImportancia.SelectedValue.Trim
        If chkTipo.Checked = True And cboTipo.SelectedValue <> "< Seleccionar >" Then pTipo = cboTipo.SelectedValue.Trim
        If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then pComponente = cboComponente.SelectedValue.Trim
        If chkComponente.Checked = True And cboComponente.SelectedValue <> "< Seleccionar >" Then
            If chkElemento.Checked = True And cboElemento.SelectedValue <> "< Seleccionar >" Then
                pComponente = cboComponente.SelectedValue.Trim
                pElemento = cboElemento.SelectedValue.Trim
            End If
        End If
        If txtFechaD.Text <> "" Then pFechaIni = Right(txtFechaD.Text, 4) & Mid(txtFechaD.Text, 4, 2) & Left(txtFechaD.Text, 2)
        If txtFechaA.Text <> "" Then pFechaFin = Right(txtFechaA.Text, 4) & Mid(txtFechaA.Text, 4, 2) & Left(txtFechaA.Text, 2)
        dt = obj.CasLista_IncidenteAExportar(Session("CodEmpresa"), pEstado, pCodigo, pImportancia, pComponente, pElemento, pFechaIni, pFechaFin, pTipo,Session("Ruta_Emp"))
        Call Exportar_Excel(dt)
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Exportar_Excel(ByVal dt As DataTable)
        Dim StwWriter As New System.IO.StringWriter
        Dim htwWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(StwWriter)
        Dim dgGrid As GridView = New GridView
        dgGrid.DataSource = dt
        dgGrid.HeaderStyle.Font.Bold = True
        dgGrid.DataBind()
        dgGrid.RenderControl(htwWriter)
        Response.ContentType = "application/vnd.ms-excel"
        Me.EnableViewState = False
        Response.Write(StwWriter.ToString)
        Response.End()
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Try
            Dim pCodigo As Double : pCodigo = 0
            If e.CommandName = "Solucion" Then
                pCodigo = Flex.Rows(Index).Cells(6).Text.Trim
                FlexDet.DataSource = Lista_Solucion(Session("CodEmpresa"), pCodigo, Flex.Rows(Index).Cells(14).Text.Trim, Flex.Rows(Index).Cells(23).Text.Trim, Session("Ruta_Emp"))
                FlexDet.DataBind()
            End If
            If e.CommandName = "Mostrar" Then
                btnExpportar.Visible = False
                txtNIncidente.Text = Flex.Rows(Index).Cells(6).Text.Trim
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
                Ficha.Height = 550
                btnRegresar.Visible = True
                Call Tipos_Criterio("2", cboNImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
                Call Tipos_Criterio("1", cboNTipo, Session("CodEmpresa"), Session("Ruta_Emp"))
                Ficha_ActiveTabChanged(sender, e)
                btnExpportar.Visible = False
            End If
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
    Protected Sub cboComponente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Visible = False
        cboElemento.Items.Clear()
        cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
        cboElemento.Enabled = False
        Call LLenaComboItemTabEsp(cboElemento, cboComponente.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        cboElemento.Enabled = False
        cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub chkElemento_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkElemento.CheckedChanged
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
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = "0" Then
            Ficha.Height = 550
            txtFechaD.Text = FormatoFecha(FechaActual())
            Call LlenaComboItem("TBOPC323", cboEstado)
            Call Tipos_Criterio("2", cboImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call Tipos_Criterio("1", cboTipo, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call LLenaComboItemTabEsp(cboComponente, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call Cargar_Informacion(sender, e)
            btnExpportar.Visible = False
            btnExpportar.Enabled = False
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        btnExpportar.Visible = True
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.Height = 550
    End Sub
    Private Sub Cargar_Informacion(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim P1 As String = "0"
        Dim P2 As String = "0"
        Dim P3 As String = "0"
        Dim pCodIncidente As Double
        Dim dt As DataTable
        Dim obj As New ModuloCas
        Dim pCodGrupo As Double = 0
        If txtNIncidente.Text.Trim = "" Then Exit Sub
        pCodIncidente = txtNIncidente.Text.Trim
        Session("IncCodigo") = txtNIncidente.Text.Trim
        txtIniLlamada.Text = ""
        txtIniLlamada.Text = FormatoHoraSeg(HoraActual(True))
        Try
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
                    cboNTipo.SelectedValue = Nu(dr("INC_TIPO")) : cboNTipo.Enabled = False
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
                Next
            End If
            dt = Nothing
        Catch Ex As SqlException
            lblIError.Visible = True
            lblIError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblIError.Visible = True
            lblIError.Text = "Ha ocurrido un error en la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
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
        Me.Page.Session.Timeout = 1080
    End Sub
End Class
