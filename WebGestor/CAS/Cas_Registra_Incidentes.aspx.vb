Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.Net.Mail.MailMessage
Imports System.Net.NetworkCredential
Partial Class Cas_Registra_Incidentes
    Inherits System.Web.UI.Page
    Protected Sub optIncidente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If optIncidente.SelectedIndex = 0 Then
            Call Limpiar(sender, e)
            cboImportancia.Enabled = True
            cboTipo.Enabled = True
            txtIncidente.ReadOnly = True
            lblEtiqEstado.Visible = False
            lblCodEstado.Visible = False
            txtEstado.Visible = False
            txtUsuario.ReadOnly = False
            btnDatos.Enabled = True
            cmdResolver.Enabled = True
            cmdLimpiar.Enabled = True
            btnNotificar.Enabled = True
            btnTGrupo.Enabled = True
            btnTIndividual.Enabled = True
            cmdBorrar.Enabled = True
            btnBuscarInc.Enabled = False
            txtBuscador.Text = ""
            Flex.DataSource = Nothing
            Flex.DataBind()
            chkFiltros.Checked = False
            ModalPopupExtender1.Hide()
            cboOficina.SelectedValue = "< Seleccionar >"
            txtIncidente.Text = ""
            txtDescripcion.Text = "" : txtDescripcion.ReadOnly = False
            txtSolucion.Text = "" : txtSolucion.ReadOnly = False
            cboImportancia.SelectedValue = 3
            cboTipo.SelectedValue = 1
            txtUsuario.Focus()
        ElseIf optIncidente.SelectedIndex = 1 Then
            Call Limpiar(sender, e)
            cboImportancia.Enabled = False
            cboTipo.Enabled = False
            cboImportancia.SelectedValue = "< Seleccionar >"
            cboTipo.SelectedValue = "< Seleccionar >"
            txtIncidente.ReadOnly = False
            txtIncidente.Text = ""
            lblEtiqEstado.Visible = True
            lblCodEstado.Visible = False
            cboOficina.SelectedValue = "< Seleccionar >"
            txtEstado.Visible = True
            txtUsuario.ReadOnly = True
            btnDatos.Enabled = False
            cmdResolver.Enabled = False
            cmdLimpiar.Enabled = False
            btnNotificar.Enabled = False
            btnTGrupo.Enabled = False
            btnTIndividual.Enabled = False
            cmdBorrar.Enabled = False
            btnBuscarInc.Enabled = True
            txtBuscador.Text = ""
            Flex.DataSource = Nothing
            Flex.DataBind()
            chkFiltros.Checked = False
            ModalPopupExtender1.Hide()
            txtDescripcion.Text = "" : txtDescripcion.ReadOnly = True
            txtSolucion.Text = "" : txtSolucion.ReadOnly = False
            txtIncidente.Focus()
        End If
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Limpiar(ByVal sender As Object, ByVal e As System.EventArgs)
        lblCodOficina.Text = ""
        lblComponente.Text = ""
        lblElemento.Text = ""
        lblElemento2.Text = ""
        lblCodEstado.Text = ""
        txtEstado.Text = ""
        txtUsuario.Text = ""
        txtOficina.Text = ""
        txtNombre.Text = ""
        txtTelefono.Text = ""
        chkOficina.Checked = False
        chkOficina_CheckedChanged(sender, e)
        cboComponente.SelectedValue = "< Seleccionar >"
        cboElemento.SelectedValue = "< Seleccionar >"
        cboElemento2.SelectedValue = "< Seleccionar >"
        cboElemento.Enabled = False
        cboElemento2.Enabled = False
        cboImportancia.Items.Clear()
        cboTipo.Items.Clear()
        'LlenaComboItem("TBOPC322", cboImportancia)
        'LlenaComboItem("TBOPC329", cboTipo)
        Tipos_Criterio("2", cboImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
        Tipos_Criterio("1", cboTipo, Session("CodEmpresa"), Session("Ruta_Emp"))
        txtBuscador.Text = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        chkFiltros.Checked = False
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub cmdLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLimpiar.Click
        'Call Limpiar_Incidente(sender, e)
    End Sub
    Private Sub Limpiar_Incidente(ByVal sender As Object, ByVal e As System.EventArgs)
        System.Threading.Thread.Sleep(1)
        lblErrorInc.Text = ""
        lblMensaje.Text = ""
        txtIncidente.ReadOnly = True
        lblEtiqEstado.Visible = False
        lblCodEstado.Visible = False
        txtEstado.Visible = False
        txtUsuario.ReadOnly = False
        btnDatos.Enabled = True
        cboOficina.SelectedValue = "< Seleccionar >"
        txtDescripcion.Text = "" : txtDescripcion.ReadOnly = False
        txtSolucion.Text = "" : txtSolucion.ReadOnly = False
        Call Limpiar(sender, e)
        optIncidente.SelectedIndex = 0
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        txtBuscador.Text = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        chkFiltros.Checked = False
        ModalPopupExtender1.Hide()
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cmdBorrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdBorrar.Click
        lblErrorInc.Text = ""
        lblMensaje.Text = ""
        txtDescripcion.Text = ""
        txtSolucion.Text = ""
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Guardar(ByVal sender As Object, ByVal e As System.EventArgs, ByVal pEstado As String, Optional ByVal pAsignado As String = "")
        lblErrorInc.Text = ""
        Dim Tipo3 As Double, Tipo2 As Double
        Dim Reportar As String : Reportar = "0"
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim obj As New ModuloCas
        Dim pIniLlamada As String : pIniLlamada = ""
        Dim pCodComponente As Double
        Dim pCodIncidente As String : pCodIncidente = ""
        Try
            If optIncidente.SelectedIndex = 0 Then
                If txtUsuario.Text.Trim = "" Then lblErrorInc.Text = "Ingresar Usuario que Reporta el incidente." : Exit Sub
                If txtDescripcion.Text.Trim = "" Then Exit Sub
                If chkOficina.Checked = True And lblCodOficina.Text = "" And cboOficina.SelectedValue = "< Seleccionar >" And cboOficina.SelectedIndex <> -1 Then lblErrorInc.Text = "Debe Seleccionar la oficina." : Exit Sub
                If cboImportancia.SelectedValue = "< Seleccionar >" Or cboImportancia.SelectedIndex = -1 Then lblErrorInc.Text = "Es necesario saber la Importancia del incidente" : Exit Sub
                If cboTipo.SelectedValue = "< Seleccionar >" Or cboTipo.SelectedIndex = -1 Then lblErrorInc.Text = "Es necesario saber el tipo de incidente." : Exit Sub
                If lblComponente.Text.Trim = "" And cboComponente.SelectedValue = "< Seleccionar >" Or cboComponente.SelectedIndex = -1 Then lblErrorInc.Text = "Falta definir el Componente del Incidente para poder guardar el registro" : Exit Sub
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(APROB_CODIGO) FROM TBCAS_INCIDENTES WHERE EMPRESA_CODIGO='0001'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pCodIncidente = Nz(Rs(0)) + 1
                    End While
                Else
                    pCodIncidente = 1
                End If
                Rs.Close()
                If pCodIncidente = "" Then Exit Sub
                pCodComponente = lblComponente.Text
                pIniLlamada = Left(txtIniLlamada.Text.Trim, 2) & Mid(txtIniLlamada.Text.Trim, 4, 2) & Mid(txtIniLlamada.Text.Trim, 7, 2)
                If lblElemento.Text.Trim = "" Or cboElemento.SelectedValue = "< Seleccionar >" And cboElemento.SelectedIndex = -1 Then Tipo2 = 0 Else Tipo2 = cboElemento.SelectedValue.Trim
                If lblElemento2.Text.Trim = "" Or cboElemento2.SelectedValue = "< Seleccionar >" And cboElemento2.SelectedIndex = -1 Then Tipo3 = 0 Else Tipo3 = cboElemento2.SelectedValue.Trim
                obj.InsUpd_Incidente(Session("CodEmpresa"), pCodComponente, Session("User"), txtUsuario.Text.Trim, pCodIncidente, cboImportancia.SelectedValue.Trim, Tipo2, Tipo3, txtDescripcion.Text.Trim, cboTipo.SelectedValue.Trim, "1", 0, "", "", "", pIniLlamada, "0", "", "", "",Session("Ruta_Emp"))
                If cboOficina.SelectedValue <> "< Seleccionar >" And cboOficina.SelectedIndex <> -1 And lblCodOficina.Text <> "" Then
                    obj.InsUpd_Incidente(Session("CodEmpresa"), pCodComponente, Session("User"), txtUsuario.Text.Trim, pCodIncidente, cboImportancia.SelectedValue.Trim, Tipo2, Tipo3, txtDescripcion.Text.Trim, cboTipo.SelectedValue.Trim, "2", cboOficina.SelectedValue.Trim, txtTelefono.Text.Trim, "", "", pIniLlamada, "0", "", "", "",Session("Ruta_Emp"))
                Else
                    obj.InsUpd_Incidente(Session("CodEmpresa"), pCodComponente, Session("User"), txtUsuario.Text.Trim, pCodIncidente, cboImportancia.SelectedValue.Trim, Tipo2, Tipo3, txtDescripcion.Text.Trim, cboTipo.SelectedValue.Trim, "2", lblCodOficina.Text, txtTelefono.Text.Trim, "", "", pIniLlamada, "0", "", "", "",Session("Ruta_Emp"))
                End If
                obj.InsUpd_Incidente(Session("CodEmpresa"), pCodComponente, Session("User"), txtUsuario.Text.Trim, pCodIncidente, cboImportancia.SelectedValue.Trim, Tipo2, Tipo3, txtDescripcion.Text.Trim, cboTipo.SelectedValue.Trim, "3", 0, "", pEstado, "", pIniLlamada, "0", "", "", "",Session("Ruta_Emp"))
                If pEstado = "2" Or pEstado = "10" Then
                    obj.InsUpd_IncidenteDetalle(Session("CodEmpresa"), pCodIncidente, txtSolucion.Text.Trim, Session("User"), "1",Session("Ruta_Emp"))
                    obj.InsUpd_Incidente(Session("CodEmpresa"), pCodComponente, Session("User"), txtUsuario.Text.Trim, pCodIncidente, cboImportancia.SelectedValue.Trim, Tipo2, Tipo3, txtDescripcion.Text.Trim, cboTipo.SelectedValue.Trim, "4", 0, "", pEstado, "", pIniLlamada, "0", "", "", "",Session("Ruta_Emp"))
                ElseIf pEstado = "3" Or pEstado = "4" Then
                    obj.InsUpd_Incidente(Session("CodEmpresa"), pCodComponente, Session("User"), txtUsuario.Text.Trim, pCodIncidente, cboImportancia.SelectedValue.Trim, Tipo2, Tipo3, txtDescripcion.Text.Trim, cboTipo.SelectedValue.Trim, "5", 0, "", pEstado, pAsignado, pIniLlamada, "0", "", "", "",Session("Ruta_Emp"))
                    obj.InsUpd_IncidenteDetalle(Session("CodEmpresa"), pCodIncidente, txtSolucion.Text.Trim, Session("User"), "2",Session("Ruta_Emp"))
                End If
                txtIncidente.Text = pCodIncidente
            Else
                pCodIncidente = txtIncidente.Text.Trim
                If lblCodEstado.Text.Trim <> "2" And lblCodEstado.Text.Trim <> "5" And lblCodEstado.Text.Trim <> "6" And lblCodEstado.Text.Trim <> "10" Then
                    obj.InsUpd_Incidente(Session("CodEmpresa"), pCodComponente, Session("User"), txtUsuario.Text.Trim, pCodIncidente, cboImportancia.SelectedValue.Trim, "", "", txtDescripcion.Text.Trim, cboTipo.SelectedValue.Trim, "6", 0, "", lblCodEstado.Text, "", pIniLlamada, "0", "", "", "",Session("Ruta_Emp"))
                Else
                    lblErrorInc.Text = "No se puede enviar porque ya a sido " & txtEstado.Text.Trim & "." : Exit Sub
                End If
            End If
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cmdResolver_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdResolver.Click
        System.Threading.Thread.Sleep(100)
        lblMensaje.Text = ""
        Call Guardar(sender, e, 2)
        If txtIncidente.Text.Trim = "" Then Exit Sub
        lblMensaje.Text = "Su incidente es el " & txtIncidente.Text.Trim
        optIncidente.SelectedIndex = 0
        optIncidente_SelectedIndexChanged(sender, e)
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        txtUsuario.Focus()
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New Listados
        lblErrorInc.Text = ""
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Integer : pCodSubProd = 0
        If chkFiltros.Checked = True And Nz(lblComponente.Text) <> 0 Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = Nz(lblComponente.Text)
        If chkFiltros.Checked = True And Nz(lblElemento.Text) <> 0 Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = Nz(lblElemento.Text)
        If chkFiltros.Checked = True And Nz(lblElemento2.Text) <> 0 Then pCodSubProd = 0 Else pCodSubProd = Nz(lblElemento2.Text)
        Try
            Flex.DataSource = Cargar_BD(pCodApli, pCodProducto, pCodSubProd)
            Flex.DataBind()
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        ModalPopupExtender1.Show()
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
    End Sub
    Private Function Cargar_BD(ByVal pCodApli As Double, ByVal pCodProducto As Double, ByVal pCodSubProd As Double) As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Dim Filtros1 As String : Filtros1 = ""
        Dim Filtros2 As String : Filtros2 = ""
        Dim Opera As String
        Dim Campo1 As String
        Dim Campo2 As String
        Dim cmdSql As New SqlCommand
        'Opera = " OR "
        Cargar_BD = Nothing
        If Trim(txtBuscador.Text.Trim) <> "" And optModoBus.SelectedIndex = -1 Then lblErrorInc.Text = "Debe seleccionar un Modo de Busqueda." : Exit Function
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
                        & " AND (P1.NIVEL1_SYS_EST = '0') AND (P1.EMPRESA_CODIGO = '0001')"
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
    Protected Sub chkOficina_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkOficina.Checked = True Then
            cboOficina.SelectedValue = "< Seleccionar >"
            cboOficina.Enabled = True : txtTelefActual.Enabled = True
        Else
            cboOficina.SelectedValue = "< Seleccionar >"
            cboOficina.Enabled = False : txtTelefActual.Enabled = False
        End If
    End Sub
    Protected Sub cboComponente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboComponente.SelectedIndexChanged
        System.Threading.Thread.Sleep(50)
        'lblError.Visible = False
        lblComponente.Text = ""
        cboElemento.Items.Clear()
        cboElemento2.Items.Clear()
        'cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
        cboElemento.Enabled = False
        'cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        cboElemento2.Enabled = False
        If cboComponente.SelectedValue = "< Seleccionar >" Then lblComponente.Text = "" : lblElemento.Text = "" : lblElemento2.Text = "" Else lblComponente.Text = cboComponente.SelectedValue : lblElemento.Text = "" : lblElemento2.Text = ""
        Call LLenaComboItemTabEsp(cboElemento, cboComponente.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboComponente.SelectedValue = "< Seleccionar >" Then
            cboElemento.Enabled = False
            'cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
            cboElemento2.Enabled = False
            cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        Else
            cboElemento.Enabled = True
            'cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
            cboElemento2.Enabled = False
            cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        End If
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnBusIncidente.Attributes.Add("onclick", "window.open('Cas_RedireccionarIncidencia.aspx',null,'left=250, top=100, height=510, width= 550, status=no, resizable= no, scrollbars= no, toolbar= no,location= no, menubar= no');")
        If Not Page.IsPostBack Then
            Try
                Call LlenaComboItem("TBOPC333", cboTipoAviso)
                Call LlenaComboItem("TBOPC334", cboEstAviso)
                Call LlenaComboItem("TBOPC335", cboEstUsuario)
                cboEstUsuario.SelectedValue = "1"
                cboEstAviso.Items.Add("< Seleccionar >")
                cboEstAviso.SelectedValue = "< Seleccionar >"
                cboTipoAviso.Items.Add("< Seleccionar >")
                cboTipoAviso.SelectedValue = "< Seleccionar >"
                Call LLenaComboItemTabEsp(cboComponente, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
                cboComponente.SelectedValue = "< Seleccionar >"
                cboComponente_SelectedIndexChanged(sender, e)
                cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
                cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
                cboElemento.Enabled = False
                cboElemento2.Enabled = False
                cboImportancia.Items.Clear()
                Call Cargar_Oficina()
                Tipos_Criterio("2", cboImportancia, Session("CodEmpresa"), Session("Ruta_Emp"))
                Tipos_Criterio("1", cboTipo, Session("CodEmpresa"), Session("Ruta_Emp"))
                If Session("SiglaGrupoEmpresa") = "2ME" Then
                    lblImpacto.Visible = True
                    cboImpacto.Visible = True
                    Tipos_Criterio("3", cboImpacto, Session("CodEmpresa"), Session("Ruta_Emp"))
                Else
                    lblImpacto.Visible = False
                    cboImpacto.Visible = False
                End If
                'LlenaComboItem("TBOPC329", cboTipo)
                'LlenaComboItem("TBOPC322", cboImportancia)
                cboImportancia.SelectedValue = 3
                cboImpacto.SelectedValue = 1
                cboTipo.SelectedValue = 1
                optIncidente.SelectedIndex = 0
                optIncidente_SelectedIndexChanged(sender, e)
                Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
                txtUsuario.Focus()
            Catch Ex As SqlException
                lblErrorInc.Visible = True
                lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblErrorInc.Visible = True
                lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub cboElemento_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboElemento.SelectedIndexChanged
        'lblErrorInc.Visible = False
        cboElemento2.Items.Clear()
        'cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        cboElemento2.Enabled = False
        If cboElemento.SelectedValue = "< Seleccionar >" Then lblElemento.Text = "" : lblElemento2.Text = "" Else lblElemento.Text = cboElemento.SelectedValue : lblElemento2.Text = ""
        Call LLenaComboItemTabEsp(cboElemento2, cboComponente.SelectedValue.Trim, cboElemento.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboElemento.SelectedValue = "< Seleccionar >" Then
            cboElemento2.Enabled = False
            cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        Else
            cboElemento2.Enabled = True
            cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        End If
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnDatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDatos.Click
        System.Threading.Thread.Sleep(10)
        lblMensaje.Text = ""
        Call Cargar_Datos()
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Cargar_Datos()
        Try
            If txtUsuario.Text = "" Then
                txtOficina.Text = ""
                txtNombre.Text = ""
                txtTelefono.Text = ""
                lblCodOficina.Text = ""
            Else
                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim Rs As SqlDataReader
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT P.TBCAS_PERSONA_USUARIO, O.TBCAS_OFICINA_CODIGO_INTERNO,P.TBCAS_OFICINA, E.TBCAS_EMPRESA_NOMBRE, O.TBCAS_OFICINA_NOMBRE, P.TBCAS_TELEFONO,P.TBCAS_ANEXO,P.TBCAS_PERSONA_APELLIDOS, P.TBCAS_PERSONA_NOMBRE " _
                                      & " FROM dbo.TBCAS_OFICINAS AS O INNER JOIN dbo.TBCAS_PERSONA AS P " _
                                      & " ON O.TBCAS_OFICINA_CODIGO = P.TBCAS_OFICINA INNER JOIN dbo.TBCAS_EMPRESA AS E " _
                                      & " ON O.TBCAS_EMPRESA = E.TBCAS_EMPRESA_CODIGO " _
                                      & " WHERE TBCAS_PERSONA_USUARIO='" & Trim(txtUsuario.Text.Trim) & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        txtOficina.Text = " " & Nu(Rs!TBCAS_OFICINA_CODIGO_INTERNO) & " - " & Nu(Rs!TBCAS_OFICINA_NOMBRE)
                        txtNombre.Text = " " & Nu(Rs!TBCAS_PERSONA_APELLIDOS) & ", " & Nu(Rs!TBCAS_PERSONA_NOMBRE)
                        txtTelefono.Text = " " & Nu(Rs!TBCAS_TELEFONO) & " - " & Nu(Rs!TBCAS_ANEXO)
                        lblCodOficina.Text = Nu(Rs!TBCAS_OFICINA)
                        txtIniLlamada.Text = FormatoHoraSeg(HoraActual(True))
                    End While
                End If
                Rs.Close()
            End If
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblErrorInc.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then '"&gt;"
            If Flex.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtDescripcion.Text = txtDescripcion.Text.Trim & " " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            If Flex.Rows(Index).Cells(6).Text <> "&nbsp;" Then txtSolucion.Text = txtSolucion.Text.Trim & " " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            If Flex.Rows(Index).Cells(7).Text = "&nbsp;" Then
                'cboComponente.SelectedValue = "< Seleccionar >"
            ElseIf Nz(Flex.Rows(Index).Cells(7).Text) = 0 Or Flex.Rows(Index).Cells(7).Text = "" Then
                'cboComponente.SelectedValue = "< Seleccionar >"
            Else
                cboComponente.SelectedValue = Flex.Rows(Index).Cells(7).Text
                lblComponente.Text = Flex.Rows(Index).Cells(7).Text
                cboComponente_SelectedIndexChanged(sender, e)
            End If
            If Flex.Rows(Index).Cells(8).Text = "&nbsp;" Then
                'cboElemento.SelectedValue = "< Seleccionar >"
            ElseIf Nz(Flex.Rows(Index).Cells(8).Text) = 0 Or Flex.Rows(Index).Cells(8).Text = "" Then
                'cboElemento.SelectedValue = "< Seleccionar >"
            Else
                cboElemento.SelectedValue = Flex.Rows(Index).Cells(8).Text
                lblElemento.Text = Flex.Rows(Index).Cells(8).Text
                cboElemento_SelectedIndexChanged(sender, e)
            End If
            If Flex.Rows(Index).Cells(9).Text = "&nbsp;" Then
                'cboElemento2.SelectedValue = "< Seleccionar >"
            ElseIf Nz(Flex.Rows(Index).Cells(9).Text) = 0 Or Flex.Rows(Index).Cells(9).Text = "" Then
                'cboElemento2.SelectedValue = "< Seleccionar >"
            Else
                cboElemento2.SelectedValue = Flex.Rows(Index).Cells(9).Text
                lblElemento2.Text = Flex.Rows(Index).Cells(9).Text
            End If
            Flex.DataSource = Nothing
            Flex.DataBind()
            Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
            txtBuscador.Text = ""
            Flex.DataSource = Nothing
            Flex.DataBind()
            chkFiltros.Checked = False
            ModalPopupExtender1.Hide()
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub cboElemento2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboElemento2.SelectedIndexChanged
        'If cboElemento2.SelectedIndex = -1 Or cboElemento.Items.Count = 0 Then Exit Sub
        'If cboElemento2.Items(cboElemento.SelectedIndex).Value = "0" Then Exit Sub
        If cboElemento2.SelectedValue = "< Seleccionar >" Then lblElemento2.Text = "" Else lblElemento2.Text = cboElemento2.SelectedValue
    End Sub
    Protected Sub cboOficina_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboOficina.SelectedIndexChanged
        If cboOficina.SelectedValue = "< Seleccionar >" And cboOficina.SelectedIndex = -1 Then
            lblCodOficina.Text = ""
        Else
            lblCodOficina.Text = cboOficina.SelectedValue
        End If
    End Sub
    Protected Sub btnListarTI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarTI.Click
        Call Relacion_Usuarios()
    End Sub
    Private Sub Relacion_Usuarios()
        Dim obj As New ModuloCas
        lblErrorInc.Text = ""
        Dim pCodComponente As Double : pCodComponente = 0
        If cboComponente.SelectedValue.Trim <> "< Seleccionar >" Then
            pCodComponente = cboComponente.SelectedValue.Trim
        Else
            Exit Sub
        End If
        Try
            FlexTI.DataSource = obj.CasLista_TIndividual(pCodComponente,Session("Ruta_Emp"))
            FlexTI.DataBind()
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        ModalPopupExtender3.Show()
    End Sub
    Protected Sub FlexTI_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexTI.PageIndexChanging
        lblErrorInc.Text = ""
        FlexTI.PageIndex = e.NewPageIndex
        Call Relacion_Usuarios()
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub FlexTI_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTI.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblMensaje.Text = ""
        If e.CommandName = "AceptarTI" Then '"&gt;"
            Call Guardar(sender, e, 4, FlexTI.Rows(Index).Cells(1).Text.Trim)
            lblMensaje.Text = "Su incidente es el " & txtIncidente.Text.Trim
            optIncidente.SelectedIndex = 0
            optIncidente_SelectedIndexChanged(sender, e)
            ModalPopupExtender3.Hide()
            txtBuscador.Text = ""
            Flex.DataSource = Nothing
            Flex.DataBind()
            chkFiltros.Checked = False
            ModalPopupExtender1.Hide()
            FlexTI.DataSource = Nothing
            FlexTI.DataBind()
            Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub btnListarTG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarTG.Click
        Call Relacion_Grupo()
    End Sub
    Private Sub Relacion_Grupo()
        Dim obj As New ModuloCas
        lblErrorInc.Text = ""
        Dim pCodComponente As Double : pCodComponente = 0
        If cboComponente.SelectedValue.Trim <> "< Seleccionar >" Then
            pCodComponente = cboComponente.SelectedValue.Trim
        Else
            Exit Sub
        End If
        Try
            FlexTG.DataSource = obj.CasLista_TGrupo(pCodComponente,Session("Ruta_Emp"))
            FlexTG.DataBind()
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        ModalPopupExtender2.Show()
    End Sub
    Protected Sub FlexTG_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexTG.PageIndexChanging
        lblErrorInc.Text = ""
        FlexTG.PageIndex = e.NewPageIndex
        Call Relacion_Grupo()
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub FlexTG_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTG.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "AceptarTG" Then
            lblMensaje.Text = ""
            Call Guardar(sender, e, 3, FlexTG.Rows(Index).Cells(2).Text.Trim)
            lblMensaje.Text = "Su incidente es el " & txtIncidente.Text.Trim
            optIncidente.SelectedIndex = 0
            optIncidente_SelectedIndexChanged(sender, e)
            ModalPopupExtender2.Hide()
            txtBuscador.Text = ""
            Flex.DataSource = Nothing
            Flex.DataBind()
            chkFiltros.Checked = False
            ModalPopupExtender1.Hide()
            FlexTG.DataSource = Nothing
            FlexTG.DataBind()
            Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub btnNotificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNotificar.Click
        Dim pCorreo As String
        lblMensaje.Text = ""
        If txtUsuario.Text.Trim = "" Then lblErrorInc.Text = "Ingresar Usuario que Reporta el incidente." : Exit Sub
        pCorreo = txtUsuario.Text.Trim & "@grupobbva.com.pe"
        Call EnviodeCorreo("selm_03@hotmail.com", "", pCorreo, "Envio del Incidente", "Descripción del Problema:" & txtDescripcion.Text.Trim & " - Solución: " & txtSolucion.Text.Trim)
        Call Guardar(sender, e, 10)
        lblMensaje.Text = "Su incidente es el " & txtIncidente.Text.Trim
        optIncidente.SelectedIndex = 0
        optIncidente_SelectedIndexChanged(sender, e)
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub EnviodeCorreo(ByVal psTo As String, ByVal psCC As String, ByVal psFrom As String, ByVal psSubject As String, ByVal psBody As String)
        Dim correo As New MailMessage()
        correo.From = New MailAddress(psFrom)
        correo.To.Add(psTo)
        correo.Subject = psSubject
        correo.Body = psBody
        correo.IsBodyHtml = True

        Dim smtp As New SmtpClient
        smtp.Host = "smtp.gmail.com"
        smtp.Port = 25
        smtp.EnableSsl = True
        smtp.Credentials = New System.Net.NetworkCredential("slimorales.27@gmail.com", "elizabeth")

        Try
            smtp.Send(correo)
            lblErrorInc.Text = "Mensaje enviado satisfactoriamente"
        Catch ex As Exception
            lblErrorInc.Text = "ERROR: " & ex.Message
        End Try
    End Sub
    Protected Sub btnBListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBListar.Click
        Call ListaBandeja()
    End Sub
    Private Sub ListaBandeja()
        Dim obj As New ModuloCas
        lblErrorInc.Text = ""
        Try
            FlexB.DataSource = obj.CasLista_Bandeja(Session("Ruta_Emp"))
            FlexB.DataBind()
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        ModalPopupExtender4.Show()
    End Sub
    Protected Sub FlexB_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexB.PageIndexChanging
        lblErrorInc.Text = ""
        FlexB.PageIndex = e.NewPageIndex
        Call ListaBandeja()
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnAListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAListar.Click
        Call ListaAvisos()
    End Sub
    Private Sub ListaAvisos()
        Dim obj As New ModuloCas
        Dim pEstUsuario As String = "%"
        Dim pEstAviso As String = "%"
        Dim pTipoAviso As String = "%"
        Dim dt As New DataTable
        pEstUsuario = cboEstUsuario.SelectedValue.Trim
        If cboTipoAviso.SelectedValue.Trim <> "< Seleccionar >" Then pTipoAviso = cboTipoAviso.SelectedValue.Trim
        If cboEstAviso.SelectedValue.Trim <> "< Seleccionar >" Then pEstAviso = cboEstAviso.SelectedValue.Trim
        lblErrorInc.Text = ""
        Try
            FlexA.DataSource = obj.CasLista_Aviso(Session("User"), pEstUsuario, pTipoAviso, pEstAviso, "2",Session("Ruta_Emp"))
            FlexA.DataBind()
            dt = obj.CasConsulta_ExisteAviso(Session("User"),Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    obj.InsUpd_Aviso(Nu(dr("AVISO_NRO")), "", "", "", Session("User"), "3", Session("Ruta_Emp"), "")
                Next
            End If
            dt = Nothing
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        ModalPopupExtender5.Show()
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnBuscarInc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarInc.Click
        Dim P1 As String = "0"
        Dim P2 As String = "0"
        Dim P3 As String = "0"
        Dim pCodIncidente As Double
        Dim dt As DataTable
        Dim obj As New ModuloCas
        If txtIncidente.Text.Trim = "" Then Exit Sub
        Try
            If optIncidente.SelectedIndex = 1 Then
                pCodIncidente = txtIncidente.Text.Trim
                dt = obj.CasLista_xIncidente(Session("CodEmpresa"), pCodIncidente,Session("Ruta_Emp"))
                If dt.Rows.Count = 1 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtUsuario.Text = " " & Nu(dr("APROB_USUARIO_REPORTA"))
                        txtOficina.Text = " " & IIf(Nu(dr("BANCO_OFICINA")) = "", Nu(dr("BANCO_OFICINA2")), Nu(dr("BANCO_OFICINA")))
                        txtNombre.Text = " " & Nu(dr("TBCAS_PERSONA_APELLIDOS")) & ", " & Nu(dr("TBCAS_PERSONA_NOMBRE"))
                        txtTelefono.Text = " " & IIf(Nu(dr("INC_TELEFONO")) = "", Nu(dr("TBCAS_TELEFONO")) & " - " & Nu(dr("TBCAS_ANEXO")), Nu(dr("INC_TELEFONO")))
                        txtDescripcion.Text = " " & Nu(dr("APROB_PROBLEMA_DESCRIPCION"))
                        cboImportancia.SelectedValue = Nu(dr("APROB_PRIORIDAD"))
                        cboTipo.SelectedValue = Nu(dr("INC_TIPO"))
                        lblCodEstado.Text = Nu(dr("APROB_ESTADO"))
                        txtEstado.Text = Nu(dr("pEstado"))
                        Call LLenaComboItemTabEsp(cboComponente, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp")) '"&#241;"
                        cboComponente.SelectedValue = "< Seleccionar >"
                        If Nu(dr("APROB_TIPO")) <> "" Then cboComponente.SelectedValue = Nu(dr("APROB_TIPO")) : cboComponente_SelectedIndexChanged(sender, e)
                        If Nu(dr("APROB_PROBLEMA1")) <> "" Then cboElemento.SelectedValue = Nu(dr("APROB_PROBLEMA1")) : cboElemento_SelectedIndexChanged(sender, e)
                        If Nu(dr("APROB_PROBLEMA2")) <> "" Then cboElemento2.SelectedValue = Nu(dr("APROB_PROBLEMA2"))
                    Next
                End If
                dt = Nothing
                dt = obj.CasLista_xIncidente_Solucion(Session("CodEmpresa"), pCodIncidente,Session("Ruta_Emp"))
                If dt.Rows.Count = 1 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtSolucion.Text = " " & Nu(dr("DPROB_ACCION_DESCRIPCION"))
                    Next
                End If
                If lblCodEstado.Text.Trim = "2" Or lblCodEstado.Text.Trim = "5" Or lblCodEstado.Text.Trim = "6" Then
                    cmdResolver.Enabled = False
                Else
                    cmdResolver.Enabled = True
                End If
            End If
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnCerrarTG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarTG.Click
        FlexTG.DataSource = Nothing
        FlexTG.DataBind()
        Call AvisosPublicados(txtaviso, Session("User"),Session("Ruta_Emp"))
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Cargar_Oficina()
        Dim dt As New DataTable
        Dim obj As New ModuloCas
        cboOficina.Items.Clear()
        Try
            dt = obj.CasLista_Oficina(Session("Ruta_Emp"))
            cboOficina.DataSource = dt
            cboOficina.DataTextField = "DESCRIPCION"
            cboOficina.DataValueField = "TBCAS_OFICINA_CODIGO"
            cboOficina.DataBind()
            cboOficina.Items.Add("< Seleccionar >") : cboOficina.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en la Aplicacion :<br>" & Ex.Message
        Finally
            'Cn.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub btnAviso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAviso.Click
        Call ListaAvisos()
    End Sub
    Protected Sub btnBandeja_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBandeja.Click
        Call btnBandeja_Click(sender, e)
    End Sub
    Protected Sub btnSi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSi.Click
        Call Limpiar_Incidente(sender, e)
    End Sub
End Class
