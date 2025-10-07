Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
'Imports Microsoft.Office.Interop.Outlook
Imports System.Runtime.InteropServices
Imports System.IO

Partial Class CRM_CRM_Registrar
    Inherits System.Web.UI.Page
    Private ObjList As New ClsGtp_Listados
    Private ObjProceso As New ClsGtp_Procesos
    Dim lsNroticket As String = ""

    Protected Sub cmdBorrar_Click(sender As Object, e As EventArgs) Handles cmdBorrar.Click
        lblErrorInc.Text = ""
        lblMensaje.Text = ""
        txtDescripcion.Text = ""
        txtSolucion.Text = ""
        Me.Page.Session.Timeout = 1080
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                cboComponente.Items.Add("< Seleccionar >") : cboComponente.SelectedValue = "< Seleccionar >"
                cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
                cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
                cboElemento.Enabled = False
                cboElemento2.Enabled = False
                txtAperturaFecha.Text = FormatoFecha(FechaActual())
                txtHoraApertura.Text = FormatoHoraSeg(HoraActual(True))
                DdlCriticidad.Items.Clear()
                Call LlenaComboItem("TBOPC479", DdlCriticidad)
                DdlCanal.Items.Clear()
                Call LlenaComboItem("TBOPC474", DdlCanal)
                DdlCanal.SelectedValue = "1"
                txtRuc.Focus()
                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim dt As DataTable
                dt = ObjList.GTP_ListaClientes_Top1(Session("Ruta_Emp"), "", "", "")
                If dt.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dt.Rows
                        txtRuc.Text = Nu(dr(0))
                        txtRazon.Text = Nu(dr(1))
                        lblCodCliente.Text = Nu(dr(10))
                        txtEstadoCliente.Text = Nu(dr(12))
                        lblCodEstado.Text = Nu(dr(11))
                        Exit For
                    Next
                End If
                dt = Nothing
                If lblCodCliente.Text <> "" Then
                    Call Cargar_contactos(lblCodCliente.Text)
                End If
                If DdlContacto.Items.Count > 0 Then
                    DdlContacto.SelectedIndex = 0
                    DdlContacto_SelectedIndexChanged(sender, e)
                    ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                    If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                    DdlProceso_SelectedIndexChanged(sender, e)
                    If cboComponente.SelectedValue <> "< Seleccionar >" Then cboComponente_SelectedIndexChanged(sender, e)
                End If


                Dim nroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
                Dim mostrar As String = Convert.ToString(Request.QueryString("OfnoiafRFS"))
                If nroTicket <> "" Then
                    Dim obj As New Cls_Relacion_Ticket
                    Dim dtT As New DataTable
                    dtT = obj.Mostrar_Ticket(Session("Ruta_emp"), nroTicket)
                    If dtT.Rows.Count > 0 Then
                        Dim dbRow As DataRow = dtT.Rows(0)
                        lsNroticket = nroTicket
                        txtIncidente.Text = nroTicket
                        txtAperturaFecha.Text = DateTime.Now.ToString("dd/MM/yyyy")
                        txtHoraApertura.Text = DateTime.Now.ToString("hh:mm:ss")
                        txtRuc.Text = dbRow("TBTICKET_CLIENTE_CIF")
                        txtRazon.Text = dbRow("TBTICKET_CLIENTE_NOMBRE")
                        lblCodCliente.Text = dbRow("TBTICKET_CLIENTE_CODIGO")
                        Cargar_contactos(lblCodCliente.Text)
                        DdlContacto.SelectedValue = dbRow("TICKET_CONTACTO")
                        txtTelefono.Text = dbRow("TBTICKET_CONTACTO_TELEF1")
                        txtEmail.Text = dbRow("TBTICKET_CONTACTO_EMAIL")
                        Call LlenaComboItem("TBOPC475", DdlEstado)
                        DdlEstado.SelectedValue = "< Seleccionar >"
                        Call LlenaComboItem("TBOPC473", DdlProceso)
                        DdlProceso.SelectedValue = "< Seleccionar >"
                        txtEstadoCliente.Text = ""
                        If mostrar <> "5" Then
                            txtMotivo.Text = dbRow("TICKET_MOTIVO")
                            txtDescripcion.Text = dbRow("TICKET_DESCRIPCION")
                            txtSolucion.Text = dbRow("TICKET_SOLUCION")
                            DdlCriticidad.SelectedValue = dbRow("TICKET_PRIORIDAD")
                            DdlEstado.SelectedValue = dbRow("TICKET_ESTADO")
                            txtEstadoCliente.Text = dbRow("ESTADO_CLIENTE")
                            DdlProceso.SelectedValue = dbRow("TICKET_PROCESO")
                            DdlProceso_SelectedIndexChanged(sender, e)
                            If IsDBNull(dbRow("TICKET_TIPO_ORIG")) = False Then
                                cboComponente.SelectedValue = dbRow("TICKET_TIPO_ORIG")
                                cboComponente_SelectedIndexChanged(sender, e)
                                If IsDBNull(dbRow("TICKET_PROBLEMA1_ORIG")) = False Then
                                    cboElemento.SelectedValue = dbRow("TICKET_PROBLEMA1_ORIG")
                                    cboElemento_SelectedIndexChanged(sender, e)
                                    If IsDBNull(dbRow("TICKET_PROBLEMA2_ORIG")) = False Then
                                        cboElemento2.SelectedValue = dbRow("TICKET_PROBLEMA2_ORIG")
                                    End If
                                End If
                            End If
                            Trackings.Visible = True
                            dt = obj.Listar_Traking_Correos_Enviados(Session("Ruta_emp"), nroTicket)
                            GvTrackingCorreo.DataSource = dt
                            GvTrackingCorreo.DataBind()
                            dt = obj.Listar_Traking_Acciones(Session("Ruta_emp"), nroTicket)
                            GvTrackingAcciones.DataSource = dt
                            GvTrackingAcciones.DataBind()

                            dt = obj.Buscar_Campos_Ticket(Session("Ruta_emp"), nroTicket)
                            Dim dbRowt As DataRow = dt.Rows(0)
                            Dim tipoTicket As String = dbRowt("TICKET_TIPO")

                            dt = obj.Listar_Procedimientos(Session("Ruta_emp"), tipoTicket)
                            If dt.Rows.Count > 0 Then
                                GvProcedimientosSeguir.DataSource = dt
                                GvProcedimientosSeguir.DataBind()
                            End If

                            If GvProcedimientosSeguir.Rows.Count > 0 Then
                                ProcedimientosSeguir.Visible = True
                            End If
                        End If
                        cmdResolver.Text = "Editar"
                    End If
                End If
            Catch Ex As SqlException
                lblErrorInc.Visible = True
                lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
                'Catch Ex As Exception
                'lblErrorInc.Visible = True
                'lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub

    Private Sub Cargar_contactos(ByVal psCodCliente As String)
        Try
            Dim dt As DataTable
            DdlContacto.Items.Clear()
            dt = ObjList.GTP_ListaContactos_xCliente(Session("Ruta_Emp"), lblCodCliente.Text)
            DdlContacto.DataSource = dt
            DdlContacto.DataTextField = "CONTACTO"
            DdlContacto.DataValueField = "TBTICKET_CONTACTO_CODIGO"
            DdlContacto.DataBind()
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            'Catch Ex As Exception
            '    lblErrorInc.Visible = True
            '    lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub DdlCanal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlCanal.SelectedIndexChanged
        If DdlCanal.Items.Count = 0 Then Exit Sub
        BtnCorreo.Visible = False
        If DdlCanal.SelectedValue = "1" Then
            BtnCorreo.Visible = True
        ElseIf DdlCanal.SelectedValue = "3" Then
            '
        End If
    End Sub
    Protected Sub cboElemento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboElemento.SelectedIndexChanged
        Call LLenaComboItemTabEsp(cboElemento2, cboComponente.SelectedValue, cboElemento.SelectedValue, "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 3, "0001", Session("Ruta_emp"))
        Dim contador As Integer = cboElemento2.Items.Count()
        If contador = 0 Then
            cboElemento2.Items.Clear()
        End If
        If cboElemento.Items.Count > 0 Then cboElemento.Enabled = True Else cboElemento.Enabled = False
        If cboElemento2.Items.Count > 0 Then cboElemento2.Enabled = True Else cboElemento2.Enabled = False
    End Sub

    Private Sub cboComponente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboComponente.SelectedIndexChanged
        Call LLenaComboItemTabEsp(cboElemento, cboComponente.SelectedValue, "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 2, "0001", Session("Ruta_emp"))
        Dim contador As Integer = cboElemento.Items.Count()
        If contador = 0 Then
            cboElemento.Items.Clear()
            cboElemento2.Items.Clear()
            cboElemento.Items.Add("< Seleccionar >")
            cboElemento.SelectedValue = "< Seleccionar >"
            cboElemento2.Items.Add("< Seleccionar >")
            cboElemento2.SelectedValue = "< Seleccionar >"
        Else
            cboElemento.SelectedValue = "< Seleccionar >"
            cboElemento2.Items.Add("< Seleccionar >")
            cboElemento2.SelectedValue = "< Seleccionar >"
        End If
        If cboElemento.Items.Count > 0 Then cboElemento.Enabled = True Else cboElemento.Enabled = False
        If cboElemento2.Items.Count > 0 Then cboElemento2.Enabled = True Else cboElemento2.Enabled = False
    End Sub
    Protected Sub DdlContacto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlContacto.SelectedIndexChanged
        Dim dt As DataTable
        Try
            dt = ObjList.GTP_Datos_Contacto(Session("Ruta_Emp"), DdlContacto.SelectedValue)
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtEmail.Text = Nu(dr(9))
                    txtTelefono.Text = Nu(dr(7))
                Next
            End If
            dt = Nothing
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            'Catch Ex As Exception
            '    lblErrorInc.Visible = True
            '    lblErrorInc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub DdlProceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProceso.SelectedIndexChanged
        Dim obj As New ClsGtp_Procesos
        cboComponente.Items.Clear()
        cboElemento.Items.Clear()
        cboElemento2.Items.Clear()
        obj.LLenaComboItemTabEspRelacionProceso(Session("Ruta_emp"), cboComponente, "", "", "TBESP_GTP1", DdlProceso.SelectedValue, "0001", 1)
        Dim contador As Integer = cboComponente.Items.Count()
        If contador > 0 Then
            cboComponente.SelectedValue = "< Seleccionar >"
            cboElemento.Items.Add("< Seleccionar >")
            cboElemento.SelectedValue = "< Seleccionar >"
            cboElemento2.Items.Add("< Seleccionar >")
            cboElemento2.SelectedValue = "< Seleccionar >"
        Else
            cboComponente.Items.Clear()
            cboElemento.Items.Clear()
            cboElemento2.Items.Clear()
            cboComponente.Items.Add("< Seleccionar >")
            cboComponente.SelectedValue = "< Seleccionar >"
            cboElemento.Items.Add("< Seleccionar >")
            cboElemento.SelectedValue = "< Seleccionar >"
            cboElemento2.Items.Add("< Seleccionar >")
            cboElemento2.SelectedValue = "< Seleccionar >"
        End If
        If cboElemento.Items.Count > 0 Then cboElemento.Enabled = True Else cboElemento.Enabled = False
        If cboElemento2.Items.Count > 0 Then cboElemento2.Enabled = True Else cboElemento2.Enabled = False
        Call obj.GTP_LlenaComboItem_Proceso("TBOPC475", DdlEstado, DdlProceso.SelectedValue, Session("SiglaGrupoEmpresa"), "TBTICKET_RELACION_PROCESO_ESTADO")
    End Sub
    Protected Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Flex.DataSource = Nothing
        Flex.DataBind()
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New Listados
        lblErrorInc.Text = ""
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Integer : pCodSubProd = 0
        If cboComponente.SelectedValue <> "< Seleccionar >" Then lblComponente.Text = cboComponente.SelectedValue
        If cboElemento.SelectedValue <> "< Seleccionar >" Then lblElemento.Text = cboElemento.SelectedValue
        If cboElemento2.SelectedValue <> "< Seleccionar >" Then lblElemento2.Text = cboElemento2.SelectedValue
        If chkFiltros.Checked = True And Nz(lblComponente.Text) <> 0 Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = Nz(lblComponente.Text)
        If chkFiltros.Checked = True And Nz(lblElemento.Text) <> 0 Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = Nz(lblElemento.Text)
        If chkFiltros.Checked = True And Nz(lblElemento2.Text) <> 0 Then pCodSubProd = 0 Else pCodSubProd = Nz(lblElemento2.Text)
        Try
            Flex.DataSource = Cargar_BD(pCodApli, pCodProducto, pCodSubProd)
            Flex.DataBind()
        Catch Ex As SqlException
            lblErrorInc.Visible = True
            lblErrorInc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Finally
        End Try
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
        If Trim(txtBuscador.Text.Trim) <> "" And rd1.Checked = False And rd0.Checked=False  Then lblErrorInc.Text = "Debe seleccionar un Modo de Busqueda." : Exit Function
        Campo1 = "UPPER(CARCON_TRANSACCION) LIKE "
        Campo2 = "UPPER(CARCON_CONSULTA) LIKE "
        If rd1.Checked = True Then Opera = " AND " Else Opera = " OR "
        If Trim(txtBuscador.Text.Trim) <> "" Then
            Filtros1 = ArmaFiltros(txtBuscador.Text.Trim, Campo1, Opera)
            Filtros2 = ArmaFiltros(txtBuscador.Text.Trim, Campo2, Opera)
        End If
        Cn2.Open()
        cmdSql.Connection = Cn2
        cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[Lista]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[Lista]"
        cmdSql.ExecuteNonQuery()
        cmdSql.CommandText = "CREATE VIEW Lista AS SELECT CC.EMPRESA_CODIGO, CC.CARCON_SYS_EST, CC.CARCON_CODIGO, CC.CARCON_APLICATIVO, P1.NIVEL1_DESCRIP, CC.CARCON_PRODUCTO, " _
                        & " (SELECT NIVEL2_DESCRIP From dbo.TBESP_GTP2 WHERE (NIVEL2_CODIGO = CC.CARCON_PRODUCTO)) AS PRODUCTO, CC.CARCON_SUBPRODUCTO, " _
                        & " (SELECT NIVEL3_DESCRIP From dbo.TBESP_GTP3 WHERE (NIVEL3_CODIGO = CC.CARCON_SUBPRODUCTO)) AS SUBPRODUCTO, " _
                        & " CC.CARCON_TRANSACCION, CC.CARCON_CONSULTA, CC.CARCON_SOLUCION " _
                        & " FROM dbo.TBTICKET_CARTERA_CONSULTA AS CC INNER JOIN dbo.TBESP_GTP1 AS P1 " _
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

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then '"&gt;"
            If Flex.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtDescripcion.Text = txtDescripcion.Text.Trim & " " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            If Flex.Rows(Index).Cells(6).Text <> "&nbsp;" Then txtSolucion.Text = txtSolucion.Text.Trim & " " & Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            If Flex.Rows(Index).Cells(10).Text <> "&nbsp;" Then lblCodConsulta.Text = Flex.Rows(Index).Cells(10).Text
            If Flex.Rows(Index).Cells(7).Text = "&nbsp;" Then
                cboComponente.SelectedValue = "< Seleccionar >"
            ElseIf Nz(Flex.Rows(Index).Cells(7).Text) = 0 Or Flex.Rows(Index).Cells(7).Text = "" Then
                cboComponente.SelectedValue = "< Seleccionar >"
            Else
                cboComponente.SelectedValue = Flex.Rows(Index).Cells(7).Text
                lblComponente.Text = Flex.Rows(Index).Cells(7).Text
                cboComponente_SelectedIndexChanged(sender, e)
            End If
            If Flex.Rows(Index).Cells(8).Text = "&nbsp;" Then
                cboElemento.SelectedValue = "< Seleccionar >"
            ElseIf Nz(Flex.Rows(Index).Cells(8).Text) = 0 Or Flex.Rows(Index).Cells(8).Text = "" Then
                cboElemento.SelectedValue = "< Seleccionar >"
            Else
                If cboElemento.Items.FindByValue(Flex.Rows(Index).Cells(8).Text) IsNot Nothing Then
                    cboElemento.SelectedValue = Flex.Rows(Index).Cells(8).Text
                    lblElemento.Text = Flex.Rows(Index).Cells(8).Text
                    cboElemento_SelectedIndexChanged(sender, e)
                End If
            End If
            If Flex.Rows(Index).Cells(9).Text = "&nbsp;" Then
                cboElemento2.SelectedValue = "< Seleccionar >"
            ElseIf Nz(Flex.Rows(Index).Cells(9).Text) = 0 Or Flex.Rows(Index).Cells(9).Text = "" Then
                cboElemento2.SelectedValue = "< Seleccionar >"
            Else
                If cboElemento2.Items.FindByValue(Flex.Rows(Index).Cells(9).Text) IsNot Nothing Then
                    cboElemento2.SelectedValue = Flex.Rows(Index).Cells(9).Text
                    lblElemento2.Text = Flex.Rows(Index).Cells(9).Text
                End If
            End If

            Flex.DataSource = Nothing
            Flex.DataBind()
            txtBuscador.Text = ""
            chkFiltros.Checked = False
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscar').modal('hide');", True)
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Private Sub btnListarTI_Click(sender As Object, e As EventArgs) Handles btnListarTI.Click
        'btnListarTI
        lblErrorInc.Text = ""
        Dim ObjCont As New clsCont_Listados
        Try
            Dim obj As New clsInv_Listados
            FlexTI.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            FlexTI.DataSource = ObjList.GTP_Lista_BusClientes(Session("Ruta_Emp"), txtBusRazon.Value, txtBusRuc.Value)
            FlexTI.DataBind()
        Catch ex As SqlException
            lblErrorInc.Text = ex.Message
            'Catch ex As Exception
            '    lblErrorInc.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub FlexTI_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexTI.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "AceptarTI" Then
            lblCodCliente.Text = ""
            txtRuc.Text = ""
            txtRazon.Text = ""
            txtRuc.Text = FlexTI.Rows(Index).Cells(1).Text
            txtRazon.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTI.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            lblCodCliente.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTI.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtEstadoCliente.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTI.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            lblCodEstado.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTI.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexTI.DataSource = Nothing
            FlexTI.DataBind()
            Call Cargar_contactos(lblCodCliente.Text)
            If DdlContacto.Items.Count > 0 Then
                DdlContacto.SelectedIndex = 0
                DdlContacto_SelectedIndexChanged(sender, e)
                ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                DdlProceso_SelectedIndexChanged(sender, e)
                If cboComponente.SelectedValue <> "< Seleccionar >" Then cboComponente_SelectedIndexChanged(sender, e)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCliente').modal('hide');", True)
        End If
    End Sub

    Private Sub Limpiar(sender As Object, e As EventArgs)
        lblErrorInc.Text = ""
        txtIncidente.Text = ""
        lblMensaje.Text = ""
        txtAperturaFecha.Text = FormatoFecha(FechaActual())
        txtHoraApertura.Text = FormatoHoraSeg(HoraActual(True))
        cboComponente.Items.Add("< Seleccionar >") : cboComponente.SelectedValue = "< Seleccionar >"
        cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
        cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        cboElemento.Enabled = False
        cboElemento2.Enabled = False
        DdlCriticidad.Items.Clear()
        Call LlenaComboItem("TBOPC479", DdlCriticidad)
        DdlCanal.Items.Clear()
        Call LlenaComboItem("TBOPC474", DdlCanal)
        DdlCanal.SelectedValue = "1"
        txtRuc.Focus()
        txtRuc.Text = ""
        lblCodCliente.Text = ""
        txtEstadoCliente.Text = ""
        lblCodEstado.Text = ""
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim dt As DataTable

        dt = ObjList.GTP_ListaClientes_Top1(Session("Ruta_Emp"), "", "", "")
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                txtRuc.Text = Nu(dr(0))
                txtRazon.Text = Nu(dr(1))
                lblCodCliente.Text = Nu(dr(10))
                txtEstadoCliente.Text = Nu(dr(12))
                lblCodEstado.Text = Nu(dr(11))
                Exit For
            Next
        End If
        dt = Nothing
        If lblCodCliente.Text <> "" Then
            Call Cargar_contactos(lblCodCliente.Text)
        End If
        If DdlContacto.Items.Count > 0 Then
            DdlContacto.SelectedIndex = 0
            DdlContacto_SelectedIndexChanged(sender, e)
            ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
            If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
            DdlProceso_SelectedIndexChanged(sender, e)
            If cboComponente.SelectedValue <> "< Seleccionar >" Then cboComponente_SelectedIndexChanged(sender, e)
        End If
        txtMotivo.Text = ""
        txtDescripcion.Text = ""
        txtSolucion.Text = ""
        lblCodConsulta.Text = ""
        lblElemento.Text = ""
        lblElemento2.Text = ""
        lblComponente.Text = ""
        lblCodEstado.Text = ""

    End Sub
    Protected Sub FlexTI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FlexTI.SelectedIndexChanged

    End Sub
    Protected Sub cmdResolver_Click(sender As Object, e As EventArgs) Handles cmdResolver.Click
        lblErrorInc.Text = ""
        lblMensaje.Text = ""
        Dim CodGarantia As String = ""
        Dim ValorSys As String = ""
        Dim XAccion As String = ""
        Dim XObserva As String = ""
        Dim Tipo1 As String = ""
        Dim Tipo2 As String = ""
        Dim Tipo3 As String = ""
        If lblCodCliente.Text = "" Then lblErrorInc.Text = "Es necesario seleccionar al proveedor del Ticket"
        If DdlContacto.SelectedValue = "< Seleccionar >" Then lblErrorInc.Text = "Es necesario seleccionar al contacto del ticket."
        If DdlProceso.SelectedValue = "< Seleccionar >" Then lblErrorInc.Text = "Es necesario saber el proceso del Ticket"
        If DdlCanal.SelectedValue = "< Seleccionar >" Then lblErrorInc.Text = "Es necesario saber el canal del Ticket"
        If cboComponente.SelectedValue = "< Seleccionar >" Then lblErrorInc.Text = "Falta definir el Tipo de Petición del Ticket para poder guardar el registro"
        If txtMotivo.Text = "" Then lblErrorInc.Text = "Ingresar el Motivo."
        If txtDescripcion.Text = "" Then lblErrorInc.Text = "Ingresar la Descripción."
        If txtSolucion.Text = "" Then lblErrorInc.Text = "Ingresar Solución."
        If DdlCriticidad.SelectedValue = "< Seleccionar >" Then lblErrorInc.Text = "Seleccionar criticidad."
        If DdlEstado.SelectedValue = "< Seleccionar >" Then lblErrorInc.Text = "Seleccionar estado."
        If DdlEstado.SelectedValue = "5" Or DdlEstado.SelectedValue = "6" Then
            'If txtRep_CodReferencia.Text = "" Then lblErrorInc.Text = "Ingresar código de Referencia."
        End If
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : Cn2.Open() : Cn3.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3
        If cmdResolver.Text <> "Editar" Then
            CmdGlobal.CommandText = "SELECT MAX(TICKET_CODIGO) FROM TBTICKET "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtIncidente.Text = Nz(Rs(0)) + 1
                End While
            Else
                txtIncidente.Text = "00001"
            End If
            Rs.Close()
        End If

        Dim psFechaReg As String = ""
        Dim psHoraReg As String = ""
        Dim psFechaApertura As String = ""
        Dim psHoraApertura As String = ""
        Dim psHoraAperturaSeg As String = ""
        Dim psValorsys As String = ""
        Dim psHoraServer As String = HoraActual()
        Dim psHoraServerSeg As String = HoraActual(True)
        Dim psFechaServer As String = FechaActual()
        psFechaReg = psFechaServer
        psHoraReg = psHoraServer
        psHoraAperturaSeg = Left(txtHoraApertura.Text, 2) & Mid(txtHoraApertura.Text, 4, 2) & Mid(txtHoraApertura.Text, 7, 2)
        psFechaApertura = Right(txtAperturaFecha.Text, 4) & Mid(txtAperturaFecha.Text, 4, 2) & Left(txtAperturaFecha.Text, 2)
        psHoraApertura = Left(txtHoraApertura.Text, 2) & Mid(txtHoraApertura.Text, 4, 2)

        psValorsys = Session("User") & psFechaReg & psFechaReg

        Dim psNroEvento As String : psNroEvento = "NULL"
        Dim psCodigoCC As String : psCodigoCC = "NULL"

        If cboComponente.SelectedValue = "< Seleccionar >" Then Tipo1 = "NULL" Else Tipo1 = "'" & cboComponente.SelectedValue & "'"
        If cboElemento.SelectedValue = "< Seleccionar >" Then Tipo2 = "NULL" Else Tipo2 = "'" & cboElemento.SelectedValue & "'"
        If cboElemento2.SelectedValue = "< Seleccionar >" Then Tipo3 = "NULL" Else Tipo3 = "'" & cboElemento2.SelectedValue & "'"

        If cmdResolver.Text = "Editar" Then

            CmdGlobal.CommandText = " UPDATE TBTICKET SET TICKET_TIPO = " & Tipo1 & ", TICKET_PROBLEMA1 = " & Tipo2 & ", TICKET_PROBLEMA2 = " & Tipo3 & ", " _
                                  & " TICKET_PROVEEDOR = " & lblCodCliente.Text & " , TICKET_CONTACTO = '" & DdlContacto.SelectedValue & "', " _
                                  & " TICKET_PROCESO = '" & DdlProceso.SelectedValue & "', TICKET_CANAL = '" & DdlCanal.SelectedValue & "',  " _
                                  & " TICKET_MOTIVO = '" & QuitaComilla(txtMotivo.Text) & "', TICKET_DESCRIPCION = '" & QuitaComilla(txtDescripcion.Text) & "', " _
                                  & " TICKET_SOLUCION = '" & QuitaComilla(txtSolucion.Text) & "', " _
                                  & " TICKET_ESTADO = '" & DdlEstado.SelectedValue & "', TICKET_SYS_MOD = '" & ValorSys & "' " _
                                  & " WHERE TICKET_CODIGO=" & txtIncidente.Text
            CmdGlobal.ExecuteNonQuery()

        Else
            CmdGlobal.CommandText = " INSERT INTO TBTICKET(TICKET_CODIGO, TICKET_REG_USUARIO, TICKET_REG_FECHA, TICKET_REG_HORA, " _
                                  & " TICKET_REPORTA_USUARIO, TICKET_REPORTA_FECHA, TICKET_REPORTA_HORA,  " _
                                  & " TICKET_TIPO, TICKET_PROBLEMA1, TICKET_PROBLEMA2, TICKET_PROVEEDOR, TICKET_CONTACTO, " _
                                  & " TICKET_PROCESO, TICKET_CANAL, TICKET_MOTIVO, TICKET_DESCRIPCION, TICKET_SOLUCION, " _
                                  & " TICKET_ESTADO, TICKET_SYS_CRE,TICKET_SYS_EST, TICKET_INICIALLAMADA_N1, " _
                                  & " TICKET_FINLLAMADA_N1,TICKET_ESTADO_FECHA,TICKET_ESTADO_HORA, TICKET_PRIORIDAD ) " _
                                  & " VALUES(" & txtIncidente.Text & ", '" & Session("User") & "', '" & FechaActual() & "', '" & HoraActual() & "', " _
                                  & " '" & txtRuc.Text & "', '" & psFechaApertura & "','" & psHoraAperturaSeg & "', " _
                                  & " " & Tipo1 & "," & Tipo2 & "," & Tipo3 & ", " & lblCodCliente.Text & ", '" & DdlContacto.SelectedValue & "', " _
                                  & " '" & DdlProceso.SelectedValue & "','" & DdlCanal.SelectedValue & "', " _
                                  & " '" & QuitaComilla(txtMotivo.Text) & "','" & QuitaComilla(txtDescripcion.Text) & "','" & QuitaComilla(txtSolucion.Text) & "', " _
                                  & " '" & DdlEstado.SelectedValue & "','" & ValorSys & "','0', '" & psHoraAperturaSeg & "', '" & HoraActual() & "', " _
                                  & " '" & psFechaApertura & "','" & psHoraApertura & "', '" & DdlCriticidad.SelectedValue & "')"
            CmdGlobal.ExecuteNonQuery()
        End If

        If ChkCosto.Checked = True Then
            CmdGlobal.CommandText = " UPDATE TBTICKET SET TICKET_CCOSTO_CODIGO = " & LblCodCCosto.Text & " " _
                              & " WHERE TICKET_CODIGO=" & txtIncidente.Text
            CmdGlobal.ExecuteNonQuery()
        End If

        If lblCodConsulta.Text <> "" Then
            CmdGlobal.CommandText = " UPDATE TBTICKET SET TICKET_CARCON_CODIGO = " & lblCodConsulta.Text & " " _
                                  & " WHERE TICKET_CODIGO=" & txtIncidente.Text
            CmdGlobal.ExecuteNonQuery()
        End If
        'If LTrim(RTrim((lblDuraRegistro.Caption))) <> "" And Nu(lblDuraRegistro.Caption) <> "NULL" Then

        '    CmdGlobal.CommandText = " UPDATE TBTICKET SET TICKET_DURACION_REAL = '" & lblDuraRegistro.Caption & "' " _
        '                          & " WHERE TICKET_CODIGO=" & txtRep_Codigo.Text
        '    CmdGlobal.ExecuteNonQuery()
        'End Iflblcodconsulta
        CmdGlobal.CommandText = " UPDATE TBTICKET SET " _
                              & " TICKET_ASIGNADO_PERSONA = '" & Session("User") & "', " _
                              & " TICKET_ASIGNADO_FECHA = '" & psFechaApertura & "', TICKET_ASIGVISTO_FECHA = '" & psFechaApertura & "', " _
                              & " TICKET_ASIGNADO_HORA = '" & psHoraApertura & "', TICKET_ASIGVISTO_HORA = '" & psHoraApertura & "' " _
                              & " WHERE TICKET_CODIGO=" & txtIncidente.Text
        CmdGlobal.ExecuteNonQuery()

        Dim pd_Secuencia_2 As String = ""

        Dim pd_Secuencia As String = ""

        Dim txtAcc_Secuencia As String = ""
        Dim pd_Secuencia_Accion As String = ""

        If cmdResolver.Text <> "Editar" Then
            '    ''traking de acciones 3 registrar ticket
            pd_Secuencia_Accion = ""
            CmdGlobal.CommandText = "SELECT MAX(ACCION_SECUENCIA) FROM TBTICKET_TRAKING_ACCION WHERE TICKET_CODIGO=" & txtIncidente.Text
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pd_Secuencia_Accion = Nz(Rs(0)) + 1
                End While
            Else
                pd_Secuencia_Accion = "1"
            End If
            Rs.Close()
            CmdGlobal.CommandText = " INSERT INTO TBTICKET_TRAKING_ACCION ( TICKET_CODIGO, ACCION_SECUENCIA, ACCION_CODIGO, ACCION_FECHA, ACCION_HORA, ACCION_USER, ACCION_REFERENCIA, ACCION_CONTACTO,ACCION_CORREO) " _
                              & " VALUES (" & txtIncidente.Text & ", " & pd_Secuencia_Accion & ", '5', '" & FechaActual() & "', '" & HoraActual() & "', '" & Session("User") & "', " & txtIncidente.Text & ", '" & DdlContacto.SelectedValue & "', '" & txtEmail.Text & "')"
            CmdGlobal.ExecuteNonQuery()

            pd_Secuencia_2 = ""
            CmdGlobal.CommandText = "SELECT MAX(TRAKING_SECUENCIA) FROM TBTICKET_TRAKING_ASIGNACION WHERE TICKET_CODIGO=" & txtIncidente.Text
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pd_Secuencia_2 = Nz(Rs(0)) + 1
                End While
            Else
                pd_Secuencia_2 = "1"
            End If
            Rs.Close()

            CmdGlobal.CommandText = "INSERT INTO TBTICKET_TRAKING_ASIGNACION(TICKET_CODIGO, TRAKING_SECUENCIA, TRAKING_REG_FECHA, TRAKING_REG_HORA, TRAKING_REG_USUARIO, TRAKING_ASESOR, TRAKING_SYS_EST)" _
                              & " VALUES (" & txtIncidente.Text & "," & pd_Secuencia_2 & ",'" & psFechaApertura & "','" & psHoraApertura & "','" & Session("User") & "','" & Session("User") & "','0') "
            CmdGlobal.ExecuteNonQuery()

            pd_Secuencia = ""

            CmdGlobal.CommandText = "SELECT MAX(REGISTRO_SECUENCIA) FROM TBTICKET_TRAKING WHERE APROB_CODIGO='" & txtIncidente.Text & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pd_Secuencia = Nz(Rs(0)) + 1
                End While
            Else
                pd_Secuencia = "1"
            End If
            Rs.Close()

            CmdGlobal.CommandText = "INSERT INTO TBTICKET_TRAKING(REGISTRO_SECUENCIA,EMPRESA_CODIGO, APROB_CODIGO, FECHA_REGISTRO, HORA_REGISTRO, ESTADO_REGISTRO,USUARIO_REGISTRO, REGISTRO_TIPO )" _
                                  & " VALUES ('" & pd_Secuencia & "','" & Session("CodEmpresa") & "'," & txtIncidente.Text & ",'" & psFechaApertura & "','" & psHoraApertura & "','" & DdlEstado.SelectedValue & "','" & Session("User") & "','1') "
            CmdGlobal.ExecuteNonQuery()

            txtAcc_Secuencia = ""
            CmdGlobal.CommandText = "SELECT MAX(TICKETD_SECUENCIA) FROM TBTICKET_DETALLE WHERE TICKET_CODIGO='" & txtIncidente.Text & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtAcc_Secuencia = Nz(Rs(0)) + 1
                End While
            Else
                txtAcc_Secuencia = "001"
            End If
            Rs.Close()

            CmdGlobal.CommandText = " INSERT INTO TBTICKET_DETALLE(EMPRESA_CODIGO, TICKET_CODIGO,TICKETD_SECUENCIA , TICKETD_ACCION_DESCRIPCION," _
                                  & " TICKETD_USUARIO_ACCION, TICKETD_FECHA_ACCION,TICKETD_HORA_ACCION, TICKETD_SYS_EST, TICKETD_ACCION_HORA_FIN, TICKETD_ACCION_ESTADO, TICKETD_CANAL, TICKETD_ESTADO) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & txtIncidente.Text & "," & txtAcc_Secuencia & ",'" & QuitaComilla(txtSolucion.Text) & "'," _
                                  & " '" & Session("User") & "','" & psFechaApertura & "','" & psHoraAperturaSeg & "','0', '" & HoraActual() & "','0','" & DdlCanal.SelectedValue & "','" & DdlEstado.SelectedValue & "')"
            CmdGlobal.ExecuteNonQuery()

        End If
        ''
        CmdGlobal.CommandText = " UPDATE TBTICKET SET TICKET_TIPO_ORIG = " & Tipo1 & ", " _
                              & " TICKET_PROBLEMA1_ORIG=" & Tipo2 & ", " _
                              & " TICKET_PROBLEMA2_ORIG=" & Tipo3 & " " _
                              & " WHERE TICKET_CODIGO=" & txtIncidente.Text
        CmdGlobal.ExecuteNonQuery()
        CmdGlobal.CommandText = " UPDATE TBTICKET SET " _
                              & " TICKET_VISTO_FECHA='" & psFechaApertura & "', " _
                              & " TICKET_VISTO_HORA='" & psHoraAperturaSeg & "' " _
                              & " WHERE TICKET_CODIGO=" & txtIncidente.Text
        CmdGlobal.ExecuteNonQuery()

        If DdlEstado.SelectedValue = "2" Then
            CmdGlobal.CommandText = " UPDATE TBTICKET SET TICKET_USUARIO_RESUELVE = '" & Session("User") & "', " _
                                  & " TICKET_SOLUCION_FECHA = '" & psFechaApertura & "', " _
                                  & " TICKET_SOLUCION_HORA = '" & psHoraApertura & "' WHERE TICKET_CODIGO=" & txtIncidente.Text
            CmdGlobal.ExecuteNonQuery()
        End If

        'guardar tracking de llamada

        Dim psCanalSistema As String : psCanalSistema = DdlCanal.SelectedValue
        Dim psCodTrack As String : psCodTrack = ""

        CmdGlobal.CommandText = " SELECT MAX(TRAKING_CODIGO) FROM TBTICKET_TRAKING_LLAMADAS"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psCodTrack = Nz(Rs(0)) + 1
            End While
        Else
            psCodTrack = "1"
        End If
        Rs.Close()

        Dim ls_CodLlamada As String = ""
        If (psCanalSistema = "3" Or psCanalSistema = "4") And ls_CodLlamada = "" Then
            CmdGlobal.CommandText = " INSERT INTO TBTICKET_TRAKING_LLAMADAS (EMPRESA_CODIGO, TICKET_CODIGO, TRAKING_TIPO , " _
                                  & " TRAKING_USER, TRAKING_FECHA, TRAKING_HORA, TRAKING_SYS_EST, TRAKING_CODIGO, TRAKING_CONTACTO ,TRAKING_EMAIL) " _
                                  & " VALUES ('" & Session("CodEmpresa") & "', " & txtIncidente.Text & ", '" & psCanalSistema & "', " _
                                  & " '" & Session("User") & "', '" & psFechaApertura & "', '" & psHoraApertura & "', '0'," & psCodTrack & ", '" & DdlContacto.SelectedValue & "','" & txtEmail.Text & "')"
            CmdGlobal.ExecuteNonQuery()
        End If

        ''ACTUALIZAR CLIENTES
        If ls_CodLlamada <> "" Then
            CmdGlobal.CommandText = "SELECT MAX(ACCION_SECUENCIA) FROM TBTICKET_TRAKING_ACCION WHERE TICKET_CODIGO=" & txtIncidente.Text
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pd_Secuencia_Accion = Nz(Rs(0)) + 1
                End While
            Else
                pd_Secuencia_Accion = "1"
            End If
            Rs.Close()

            CmdGlobal.CommandText = "SELECT * FROM TBTICKET_TRAKING_ACCION WHERE ACCION_CODIGO ='15' AND ACCION_REFERENCIA = " & ls_CodLlamada
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " UPDATE TBTICKET_TRAKING_ACCION SET TICKET_CODIGO=" & txtIncidente.Text & ", " _
                                      & " ACCION_SECUENCIA = " & pd_Secuencia_Accion & " " _
                                      & " WHERE ACCION_CODIGO ='15' AND ACCION_REFERENCIA = " & ls_CodLlamada
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()

        End If


        Dim psNroTicket As String : psNroTicket = txtIncidente.Text
        'RELACIONAR DOCUMENTOS

        lblMensaje.Text = "Su Nro de Ticket es el " & txtIncidente.Text

        If cmdResolver.Text = "Editar" Then
            Response.Redirect("~/CRM/CRM_Relacion_Ticket.aspx")
        End If


        Call Limpiar(sender, e)


    End Sub
    Protected Sub cmdLimpiar_Click(sender As Object, e As EventArgs) Handles cmdLimpiar.Click
        Call Limpiar(sender, e)
    End Sub
    Protected Sub txtRuc_TextChanged(sender As Object, e As EventArgs) Handles txtRuc.TextChanged
        Call Cargar_DatosCliente(sender, e)
    End Sub

    Private Sub Cargar_DatosCliente(sender As Object, e As EventArgs)
        lblErrorInc.Text = ""
        Try
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim dt As DataTable
            dt = ObjList.GTP_Lista_BusClientes(Session("Ruta_Emp"), "", txtRuc.Text)
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtRuc.Text = Nu(dr(0))
                    txtRazon.Text = Nu(dr(1))
                    lblCodCliente.Text = Nu(dr(10))
                    txtEstadoCliente.Text = Nu(dr(12))
                    lblCodEstado.Text = Nu(dr(11))
                    Exit For
                Next
            End If
            dt = Nothing
            If lblCodCliente.Text <> "" Then
                Call Cargar_contactos(lblCodCliente.Text)
                If DdlContacto.Items.Count > 0 Then
                    DdlContacto.SelectedIndex = 0
                    DdlContacto_SelectedIndexChanged(sender, e)
                    ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                    If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                    DdlProceso_SelectedIndexChanged(sender, e)
                    If cboComponente.SelectedValue <> "< Seleccionar >" Then cboComponente_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As SqlException
            lblErrorInc.Text = ex.Message
            'Catch ex As Exception
            '    lblErrorInc.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub GvProcedimientosSeguir_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvProcedimientosSeguir.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Relacion_Ticket
        Dim dt As New DataTable
        Dim codTarea As String = GvProcedimientosSeguir.Rows(Index).Cells(3).Text.ToString()
        If e.CommandName = "Aceptar" Then
            dt = obj.Listar_Tareas(Session("Ruta_emp"), codTarea)
            GvTareasRealizar.DataSource = dt
            GvTareasRealizar.DataBind()
            If GvTareasRealizar.Rows.Count > 0 Then
                TareasRealizar.Visible = True
            Else
                TareasRealizar.Visible = False
            End If
        End If
    End Sub

    Protected Sub BtnCorreo_Click(sender As Object, e As EventArgs) Handles BtnCorreo.Click
        ''Dim outlookApp As Application = Nothing
        ''Dim mail As MailItem = Nothing

        ''Try
        ''    ' Obtener la instancia actual de Outlook
        ''    'outlookApp = Marshal.GetActiveObject("Outlook.Application")

        ''    Try
        ''        outlookApp = Marshal.GetActiveObject("Outlook.Application")
        ''    Catch ex As COMException
        ''        outlookApp = New Application()
        ''    End Try

        ''    ' Obtener la ventana activa
        ''    Dim activeExplorer As Explorer = outlookApp.ActiveExplorer()
        ''    txtMotivo.Text = ""
        ''    ' Obtener el correo actualmente seleccionado
        ''    If activeExplorer IsNot Nothing AndAlso activeExplorer.Selection.Count > 0 Then
        ''        Dim selectedObject As Object = activeExplorer.Selection.Item(1)

        ''        If TypeOf selectedObject Is MailItem Then
        ''            mail = CType(selectedObject, MailItem)

        ''            txtMotivo.Text = " Contacto: " & mail.SenderName & vbCrLf
        ''            txtMotivo.Text = txtMotivo.Text & " Recibido el: " & mail.ReceivedTime & vbCrLf
        ''            txtMotivo.Text = txtMotivo.Text & " Para: " & mail.To & vbCrLf
        ''            txtMotivo.Text = txtMotivo.Text & " Asunto: " & mail.Subject & vbCrLf
        ''            txtMotivo.Text = txtMotivo.Text & " Mensaje: " & vbCrLf & mail.Body & vbCrLf

        ''        Else
        ''            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El elemento seleccionado no es un correo electrónico.');", True)
        ''        End If
        ''    Else
        ''        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay ningún correo electrónico seleccionado.');", True)
        ''    End If

        ''Catch ex As COMException
        ''    ' Manejo de excepciones específicas de COM
        ''    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Error de COM: " & ex.Message & ".');", True)
        ''    'Response.Write("Error de COM: " & ex.Message)
        ''Catch ex As UnauthorizedAccessException
        ''    ' Manejo de excepciones de permisos
        ''    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Error de acceso: " & ex.Message & ".');", True)
        ''    'Response.Write("Error de acceso: " & ex.Message)
        ''Finally
        ''    ' Liberar objetos COM
        ''    If mail IsNot Nothing Then Marshal.ReleaseComObject(mail)
        ''    If outlookApp IsNot Nothing Then Marshal.ReleaseComObject(outlookApp)

        ''    mail = Nothing
        ''    outlookApp = Nothing
        ''End Try


        'Dim host As String = "imap-mail.outlook.com"
        'Dim port As Integer = 993
        'Dim useSsl As Boolean = True
        'txtCorreoAsunto.Text = ""
        'txtCorreoFrom.Text = ""
        'txtCorreoFecha.Text = ""
        'txtCorreoBody.Text = ""
        'TxtContactocorreo.Text = ""
        '' Establece las credenciales de inicio de sesión del correo
        'Dim username As String = "selm_03@hotmail.com"
        'Dim password As String = "M4rt1n4Lu1s2920"

        'Using client As New ImapClient
        '    ' Conecta al servidor IMAP
        '    client.Connect(host, port, SecureSocketOptions.SslOnConnect)

        '    ' Autentica con las credenciales de inicio de sesión
        '    client.Authenticate(username, password)

        '    ' Selecciona la carpeta de bandeja de entrada
        '    Dim inbox = client.Inbox
        '    inbox.Open(FolderAccess.ReadOnly)

        '    '' Obtiene los mensajes de correo electrónico
        '    'Dim messages = inbox.Fetch(0, -2, MessageSummaryItems.UniqueId Or MessageSummaryItems.Envelope)

        '    ' Obtiene la fecha actual y la fecha de inicio del día actual
        '    Dim currentDate As Date = DateTime.Now.Date
        '    Dim startDate As Date = currentDate.AddDays(0) ' Fecha de inicio del día anterior

        '    'Dim fechaActual As DateTime = DateTime.Now.Date

        '    '' Definir el rango de búsqueda para los mensajes del día
        '    'Dim fechaInicio As DateTime = fechaActual
        '    'Dim fechaFin As DateTime = fechaActual.AddDays(1)


        '    ' Obtiene los UIDs de los mensajes de correo electrónico del día actual
        '    Dim uids As UniqueIdSet = client.Inbox.Search(SearchQuery.DeliveredAfter(startDate))
        '    Dim messages As List(Of MimeMessage) = New List(Of MimeMessage)()

        '    ' Descarga los mensajes utilizando los UIDs obtenidos
        '    For Each uid As UniqueId In uids
        '        Dim message As MimeMessage = client.Inbox.GetMessage(uid)
        '        messages.Add(message)
        '    Next


        '    ' Itera sobre los mensajes y muestra información básica
        '    For Each message As MimeMessage In messages

        '        Dim oosubject As String = message.Subject
        '        Dim oofrom As String = message.From.ToString
        '        Dim oodate As String = message.Date.ToString
        '        Dim oobody As String
        '        If message.TextBody = Nothing Then
        '            oobody = ""
        '        Else
        '            oobody = message.TextBody.ToString
        '        End If
        '        Dim oosender As String = message.From.ToString


        '        ' Puedes hacer algo con la información del mensaje, como mostrarla en la página
        '        txtCorreoAsunto.Text = oosubject
        '        Dim ps As Integer

        '        ps = InStr(oofrom, "<")
        '        txtCorreoFrom.Text = Mid(oofrom, ps + 1)
        '        txtCorreoFecha.Text = Left(oodate, 20).ToString
        '        txtCorreoBody.Text = oobody
        '        TxtContactocorreo.Text = Mid(oosender, 1, ps - 1).ToString
        '    Next

        '    ' Cierra la conexión con el servidor IMAP
        '    client.Disconnect(True)
        '    btnEvento_Click(sender, e)

        'End Using

    End Sub

    Sub btnEvento_Click(sender As Object, e As EventArgs) Handles btnEvento.Click
        ModalPopupExtender3.TargetControlID = "btnEvento"
        ModalPopupExtender3.Show()
    End Sub
    Private Sub btnAceptarCorreo_Click(sender As Object, e As EventArgs) Handles btnAceptarCorreo.Click
        If txtCorreoBody.Text = "" Then Exit Sub
        txtMotivo.Text = " Contacto: " & TxtContactocorreo.Text & vbCrLf
        txtMotivo.Text = txtMotivo.Text & " Recibido el: " & txtCorreoFecha.Text & vbCrLf
        txtMotivo.Text = txtMotivo.Text & " Para: " & txtCorreoFrom.Text & vbCrLf
        txtMotivo.Text = txtMotivo.Text & " Asunto: " & txtCorreoAsunto.Text & vbCrLf
        txtMotivo.Text = txtMotivo.Text & " Mensaje: " & vbCrLf & txtCorreoBody.Text & vbCrLf

        txtCorreoAsunto.Text = ""
        txtCorreoFrom.Text = ""
        txtCorreoFecha.Text = ""
        txtCorreoBody.Text = ""
        TxtContactocorreo.Text = ""
        ModalPopupExtender3.Hide()
    End Sub

    Protected Sub ChkCosto_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCosto.CheckedChanged
        If ChkCosto.Checked = True Then
            TxtCodInternoCC.Text = "" : TxtCodInternoCC.Enabled = True
            TxtDescripcionCC.Text = "" : TxtDescripcionCC.Enabled = True
            BtnBuscarCC.Enabled = True
        Else
            TxtCodInternoCC.Text = "" : TxtCodInternoCC.Enabled = False
            TxtDescripcionCC.Text = "" : TxtDescripcionCC.Enabled = False
            BtnBuscarCC.Enabled = False
        End If
    End Sub

    Private Sub BtnBuscarCC_Click(sender As Object, e As EventArgs) Handles BtnBuscarCC.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodigo As Double = 0
        Dim objCont As New clsCont_Listados
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""
        Dim obj As New clsInv_Listados

        If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
        descripcion = BuscarDescripcion.Value.Trim.ToString
        dt = obj.Lista_BusquedaCentroCosto(psconexion, Session("CodEmpresa"), psBusCodInterno, descripcion)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodInternoCC.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtDescripcionCC.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            LblCodCCosto.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        End If
    End Sub
    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('hide');", True)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Call Limpiar_Popup()
    End Sub

    Private Sub cmdBuscar_Click(sender As Object, e As EventArgs) Handles cmdBuscar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscar').modal('show');", True)
    End Sub

    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscar').modal('hide');", True)
    End Sub

    Private Sub btnDatos_Click(sender As Object, e As EventArgs) Handles btnDatos.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCliente').modal('show');", True)
    End Sub

    Private Sub btnCerrarTI_Click(sender As Object, e As EventArgs) Handles btnCerrarTI.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCliente').modal('hide');", True)
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Call Limpiar()
        lblIngreso.Visible = True
        lblEtiqueta.Text = "Ingresar Base de Datos"
        Try
            Dim obj As New ClsGtp_Procesos
            obj.LLenaComboItemTabEspRelacionProceso(Session("Ruta_emp"), cboAplicativo, "", "", "TBESP_GTP1", DdlProceso.SelectedValue, "0001", 1)
            Dim contador As Integer = cboAplicativo.Items.Count()
            If contador > 0 Then
                cboAplicativo.Items.Add("< Seleccionar >")
                cboAplicativo.SelectedValue = "< Seleccionar >"
            Else
                cboAplicativo.Items.Clear()
            End If
            'Call LLenaComboItemTabEsp(cboAplicativo, "", "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call cboAplicativo_SelectedIndexChanged(sender, e)
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
            cboProducto.Enabled = False
            cboSubProd.Enabled = False

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Private Sub Limpiar()
        lblError.Text = ""
        cboAplicativo.Items.Clear()
        cboProducto.Items.Clear()
        cboSubProd.Items.Clear()
        lblEtiqueta.Text = ""
        btnListar.Enabled = False
        BtnNuevo.Enabled = False
        btnMGuardar.Visible = True
        btnCancelar.Visible = True
        lblEtiqueta.Visible = True
        lblEtiqueta1.Visible = True
        lblEtiqueta2.Visible = True
        lblEtiqueta3.Visible = True
        lblEtiqueta4.Visible = True
        lblEtiqueta5.Visible = True
        lblEtiqueta6.Visible = True
        cboAplicativo.Visible = True
        cboProducto.Visible = True
        cboSubProd.Visible = True
        txtTransaccion.Visible = True : txtTransaccion.Text = ""
        txtConsulta.Visible = True : txtConsulta.Text = ""
        txtMSolucion.Visible = True : txtMSolucion.Text = ""
    End Sub
    Protected Sub cboAplicativo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAplicativo.SelectedIndexChanged
        cboProducto.Items.Clear()
        cboSubProd.Items.Clear()
        cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
        cboProducto.Enabled = False
        cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        cboSubProd.Enabled = False
        Call LLenaComboItemTabEsp(cboProducto, cboAplicativo.SelectedValue.Trim, "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboAplicativo.SelectedValue = "< Seleccionar >" Then
            cboProducto.Enabled = False
            cboProducto.Items.Add("< Seleccionar >") : cboProducto.SelectedValue = "< Seleccionar >"
            cboSubProd.Enabled = False
        Else
            cboProducto.Enabled = True
            cboSubProd.Enabled = False
        End If
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboProducto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProducto.SelectedIndexChanged
        cboSubProd.Items.Clear()
        cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        cboSubProd.Enabled = False
        If cboProducto.SelectedIndex = -1 Or cboProducto.Items.Count = 0 Then Exit Sub
        If cboProducto.Items(cboProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(cboSubProd, cboAplicativo.SelectedValue.Trim, cboProducto.SelectedValue.Trim, "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboProducto.SelectedValue = "< Seleccionar >" Then
            cboSubProd.Enabled = False
            cboSubProd.Items.Add("< Seleccionar >") : cboSubProd.SelectedValue = "< Seleccionar >"
        Else
            cboSubProd.Enabled = True
        End If
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub btnMGuardar_Click(sender As Object, e As EventArgs) Handles btnMGuardar.Click
        Dim pCodApli As Integer : pCodApli = 0
        Dim pCodProducto As Integer : pCodProducto = 0
        Dim pCodSubProd As Double : pCodSubProd = 0
        Dim pCodBaseDatos As Integer : pCodBaseDatos = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim strSaveFileAs As String = ""
        Dim strSaveFileAsOrigen As String = ""
        Dim dt As New DataTable
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        lblError.Text = ""
        If cboAplicativo.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Aplicatico.')", True)
            'lblError.Text = lblError.Text & " <br> - Seleccionar Aplicatico."
        ElseIf Trim(txtTransaccion.Text) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Transacción.')", True)
            'lblError.Text = lblError.Text & " <br> - Ingresar Transacción."
        ElseIf Trim(txtConsulta.Text) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Consulta.')", True)
            'lblError.Text = lblError.Text & " <br> - Ingresar Consulta."
        ElseIf Trim(txtMSolucion.Text) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Solución.')", True)
            'lblError.Text = lblError.Text & " <br> - Ingresar Solución."
        ElseIf lblError.Text.Trim <> "" Then
            lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
            Exit Sub
        Else
            Dim obj As New ClsCRM_BaseDatos
            If (cboAplicativo.SelectedValue = "< Seleccionar >") Then pCodApli = 0 : pCodProducto = 0 : pCodSubProd = 0 Else pCodApli = cboAplicativo.SelectedValue.Trim
            If (cboProducto.SelectedValue = "< Seleccionar >") Then pCodProducto = 0 : pCodSubProd = 0 Else pCodProducto = cboProducto.SelectedValue.Trim
            If (cboSubProd.SelectedValue = "< Seleccionar >") Then pCodSubProd = 0 Else pCodSubProd = cboSubProd.SelectedValue.Trim
            Try
                If lblEtiqueta.Text = "Ingresar Base de Datos" Then
                    Cn.Open()
                    CmdGlobal.Connection = Cn
                    CmdGlobal.CommandText = " SELECT MAX(CARCON_CODIGO) FROM TBTICKET_CARTERA_CONSULTA "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            pCodBaseDatos = Nz(Rs(0)) + 1
                        End While
                    Else
                        pCodBaseDatos = 1
                    End If
                    Rs.Close()
                    obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, 0, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtMSolucion.Text), "1", Session("Ruta_Emp"))
                    If pCodProducto <> 0 Then obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, 0, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtMSolucion.Text), "2", Session("Ruta_Emp"))
                    If pCodSubProd <> 0 Then obj.CasInsUpd_BaseDatos(Session("CodEmpresa"), pCodBaseDatos, pCodApli, pCodProducto, pCodSubProd, Trim(txtTransaccion.Text), Trim(txtConsulta.Text), Trim(txtMSolucion.Text), "2", Session("Ruta_Emp"))
                    Call BtnMCancelar_Click(sender, e)
                    Call btnListar_Click(sender, e)
                End If

            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
            Finally
            End Try
        End If
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub BtnMCancelar_Click(sender As Object, e As EventArgs) Handles BtnMCancelar.Click
        lblEtiqueta.Text = ""
        btnListar.Enabled = True
        BtnNuevo.Enabled = True
        btnMGuardar.Visible = False
        btnCancelar.Visible = False
        lblEtiqueta.Visible = False
        lblEtiqueta1.Visible = False
        lblEtiqueta2.Visible = False
        lblEtiqueta3.Visible = False
        lblEtiqueta4.Visible = False
        lblEtiqueta5.Visible = False
        lblEtiqueta6.Visible = False
        lblError.Text = ""
        cboAplicativo.Visible = False
        cboProducto.Visible = False
        cboSubProd.Visible = False
        txtTransaccion.Visible = False : txtTransaccion.Text = ""
        txtConsulta.Visible = False : txtConsulta.Text = ""
        txtMSolucion.Visible = False : txtMSolucion.Text = ""
        lblIngreso.Visible = False
    End Sub

    Private Sub BtnNuevaTE_Click(sender As Object, e As EventArgs) Handles BtnNuevaTE.Click
        lblTabla1.Text = "TBESP_GTP1"
        lblTabla2.Text = "TBESP_GTP2"
        lblTabla3.Text = "TBESP_GTP3"
        lblIngresoTE.Visible = False
        btnTENuevo.Enabled = False
        cboTabla.Items.Clear()
        cboTabla.Items.Add(lblTabla1.Text.Trim)
        cboTabla.Items.Add(lblTabla2.Text.Trim)
        cboTabla.Items.Add(lblTabla3.Text.Trim)
        cboTabla.Items.Add("< Seleccionar >") : cboTabla.SelectedValue = "< Seleccionar >"
        cboTabla.Enabled = True
        btnTEGuardar.Visible = False
        btnTECancelar.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTablaEsp').modal('show');", True)
    End Sub

    Protected Sub cboTabla_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTabla.SelectedIndexChanged
        'FlexTE.DataSource = Llenar_TablaEspecial(Right(cboTabla.SelectedItem.Text.Trim, 1))
        'FlexTE.DataBind()
        If Right(cboTabla.SelectedItem.Text.Trim, 1) = "1" Or Right(cboTabla.SelectedItem.Text.Trim, 1) = "2" Or Right(cboTabla.SelectedItem.Text.Trim, 1) = "3" Then btnTENuevo.Enabled = True Else btnTENuevo.Enabled = False
        cboNivel1.Items.Add("< Seleccionar >") : cboNivel1.SelectedValue = "< Seleccionar >"
    End Sub
    Public Function Llenar_TablaEspecial(ByVal Tabla As String) As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim dt As New DataTable
        Dim dtListado As New DataTable
        Dim dRow As Data.DataRow
        Dim obj As New ModuloGeneral
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        Try
            If Tabla = "1" Or Tabla = "2" Or Tabla = "3" Then btnTENuevo.Enabled = True Else btnTENuevo.Enabled = False
            If Tabla = "1" Then
                FlexTE.Columns(0).HeaderText = "Nivel 1"
                FlexTE.Columns(1).HeaderText = "" : FlexTE.Columns(2).HeaderText = ""
                FlexTE.Columns(3).HeaderText = ""
                FlexTE.Columns(0).ItemStyle.Width = 600
                FlexTE.Columns(1).ItemStyle.Width = 0 : FlexTE.Columns(2).ItemStyle.Width = 0
                FlexTE.Columns(3).ItemStyle.Width = 0
                Dim Rs As SqlClient.SqlDataReader
                Dim i As Integer = 0
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT NIVEL1_CODIGO,NIVEL1_DESCRIP From " & lblTabla1.Text.Trim & " WHERE (NIVEL1_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') ORDER BY NIVEL1_DESCRIP"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        dRow = dt.NewRow()
                        dRow(0) = Nu(Rs!NIVEL1_DESCRIP)
                        dRow(1) = Nu(Rs!NIVEL1_CODIGO)
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
                cboNivel1.Items.Add("< Seleccionar >") : cboNivel1.SelectedValue = "< Seleccionar >"
            ElseIf Tabla = "2" Then
                FlexTE.Columns(0).HeaderText = "Nivel 1"
                FlexTE.Columns(1).HeaderText = "Nivel 2" : FlexTE.Columns(2).HeaderText = ""
                FlexTE.Columns(3).HeaderText = ""
                FlexTE.Columns(0).ItemStyle.Width = 300
                FlexTE.Columns(1).ItemStyle.Width = 300 : FlexTE.Columns(2).ItemStyle.Width = 0
                FlexTE.Columns(3).ItemStyle.Width = 0
                Dim Rs As SqlClient.SqlDataReader
                Dim i As Integer = 0
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP,TB2.NIVEL1_CODIGO,TB2.NIVEL2_CODIGO " _
                                      & " FROM " & lblTabla2.Text.Trim & " TB2 INNER JOIN " & lblTabla1.Text.Trim & " TB1 " _
                                      & " ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                                      & " WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0')  " _
                                      & " AND (TB2.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') " _
                                      & " ORDER BY TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        dRow = dt.NewRow()
                        dRow(0) = Nu(Rs!NIVEL1_DESCRIP)
                        dRow(1) = Nu(Rs!NIVEL2_DESCRIP)
                        dRow(2) = Nu(Rs!NIVEL1_CODIGO)
                        dRow(3) = Nu(Rs!NIVEL2_CODIGO)
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
            ElseIf Tabla = "3" Then
                FlexTE.Columns(0).HeaderText = "Nivel 1" : FlexTE.Columns(1).HeaderText = "Nivel 2" : FlexTE.Columns(2).HeaderText = "Nivel 3"
                FlexTE.Columns(2).ItemStyle.Width = 150
                Dim Rs As SqlClient.SqlDataReader
                Dim i As Integer = 0
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " SELECT TB3.NIVEL3_NS_DHM,TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP,TB3.NIVEL3_DESCRIP, TB2.NIVEL1_CODIGO,TB2.NIVEL2_CODIGO , TB3.NIVEL3_CODIGO " _
                                      & " FROM " & lblTabla2.Text.Trim & " TB2 INNER JOIN " & lblTabla1.Text.Trim & " TB1 ON TB2.EMPRESA_CODIGO=TB1.EMPRESA_CODIGO AND TB2.NIVEL1_CODIGO = TB1.NIVEL1_CODIGO " _
                                      & " INNER JOIN " & lblTabla3.Text.Trim & " TB3 ON TB2.EMPRESA_CODIGO=TB3.EMPRESA_CODIGO AND TB2.NIVEL2_CODIGO = TB3.NIVEL2_CODIGO " _
                                      & " WHERE (TB1.NIVEL1_SYS_EST = '0') AND (TB2.NIVEL2_SYS_EST = '0') AND (TB3.NIVEL3_SYS_EST = '0')  AND (TB2.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') " _
                                      & " ORDER BY TB1.NIVEL1_DESCRIP, TB2.NIVEL2_DESCRIP, TB3.NIVEL3_DESCRIP "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        dRow = dt.NewRow()
                        dRow(0) = Nu(Rs!NIVEL1_DESCRIP)
                        dRow(1) = Nu(Rs!NIVEL2_DESCRIP)
                        dRow(2) = Nu(Rs!NIVEL3_DESCRIP)
                        dRow(3) = Nu(Rs!NIVEL1_CODIGO)
                        dt.Rows.Add(dRow)
                    End While
                End If
                Rs.Close()
            Else
                dt = Nothing
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
        Return dt
    End Function

    Private Sub btnTENuevo_Click(sender As Object, e As EventArgs) Handles btnTENuevo.Click
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            lblIngresoTE.Visible = True
            lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial"
            txtTECodigo.Text = ""
            txtTEDescripcion.Text = ""
            If Right(cboTabla.SelectedItem.Text.Trim, 1) = "1" Then
                cboNivel1.Enabled = False : cboNivel2.Enabled = False
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL1_CODIGO) FROM " & cboTabla.SelectedItem.Text
            ElseIf Right(cboTabla.SelectedItem.Text.Trim, 1) = "2" Then
                cboNivel1.Enabled = True
                cboNivel2.Enabled = False
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL2_CODIGO) FROM " & cboTabla.SelectedItem.Text
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            ElseIf Right(cboTabla.SelectedItem.Text.Trim, 1) = "3" Then
                cboNivel1.Enabled = True
                cboNivel2.Enabled = True
                Cn.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = "SELECT MAX(NIVEL3_CODIGO) FROM " & cboTabla.SelectedItem.Text
                Call LLenaComboItemTabEsp(cboNivel1, "", "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            End If
            If CmdGlobal.CommandText = "" Then Exit Sub
            cboNivel2.Items.Add("< Seleccionar >") : cboNivel2.SelectedValue = "< Seleccionar >"
            btnTENuevo.Enabled = False
            btnTEGuardar.Visible = True
            btnTECancelar.Visible = True
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtTECodigo.Text = Nz(Rs(0)) + 1
                End While
            Else
                txtTECodigo.Text = "1"
            End If
            Rs.Close()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub cboNivel1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboNivel1.SelectedIndexChanged
        Try
            cboNivel2.Items.Clear()
            If cboNivel1.SelectedValue = "< Seleccionar >" Then cboNivel2.Items.Add("< Seleccionar >") : cboNivel2.SelectedValue = "< Seleccionar >" : Exit Sub
            Call LLenaComboItemTabEsp(cboNivel2, cboNivel1.SelectedValue.Trim, "", lblTabla1.Text.Trim, lblTabla2.Text.Trim, lblTabla3.Text.Trim, 2, Session("CodEmpresa"), Session("Ruta_Emp"))

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub btnTECancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTECancelar.Click
        btnTENuevo.Enabled = True
        btnTEGuardar.Visible = False
        btnTECancelar.Visible = False
        FlexTE.Enabled = True
        lblIngresoTE.Visible = False
    End Sub
    Protected Sub btnTEGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTEGuardar.Click
        Dim Ntb As String
        Dim nTiempo As String
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim dCodigo As Double = 0
        Dim dNivel1 As Double = 0
        Dim dNivel2 As Double = 0
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Ntb = Right(cboTabla.SelectedItem.Text, 1)
            dCodigo = txtTECodigo.Text.Trim
            If Ntb = "2" And cboNivel1.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe escoger el Primer Nivel para poder guardar.')", True)
            ElseIf Ntb = "3" And cboNivel1.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe escoger el Primer Nivel para poder guardar.')", True)
            ElseIf Ntb = "3" And cboNivel2.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe escoger el Segundo Nivel para poder guardar.')", True)
            ElseIf txtTEDescripcion.Text.Trim = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No podrá guardar hasta que no le haya ingresado una descripción.')", True)
            End If
            If Ntb = "2" Then dNivel1 = cboNivel1.SelectedValue.Trim
            If Ntb = "3" Then dNivel2 = cboNivel2.SelectedValue.Trim
            nTiempo = ""
            Dim psConsulta As String = ""
            If lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial" And UCase(txtTEDescripcion.Text.Trim) <> UCase(txtTEDescripcionE.Text.Trim) Then
                If Ntb = 1 Then
                    CmdGlobal.CommandText = "SELECT * FROM " & cboTabla.SelectedItem.Text & " WHERE (NIVEL1_DESCRIP)='" & UCase(txtTEDescripcion.Text.Trim) & "' AND NIVEL1_SYS_EST='0' AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                ElseIf Ntb = 2 Then
                    CmdGlobal.CommandText = "SELECT * FROM " & cboTabla.SelectedItem.Text & " WHERE (NIVEL2_DESCRIP)='" & UCase(txtTEDescripcion.Text.Trim) & "' AND NIVEL2_SYS_EST='0' AND NIVEL1_CODIGO=" & dNivel1 & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                ElseIf Ntb = 3 Then
                    CmdGlobal.CommandText = "SELECT * FROM " & cboTabla.SelectedItem.Text & " WHERE (NIVEL3_DESCRIP)='" & UCase(txtTEDescripcion.Text.Trim) & "' AND NIVEL3_SYS_EST='0' AND NIVEL2_CODIGO=" & dNivel2 & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                End If
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psConsulta = "Error"
                    End While
                End If
                Rs.Close()
            End If
            If psConsulta = "Error" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se ha encontrado una descripcion igual, verificar o cambiar para poder guardar.')", True)
            Else
                If lblEtiquetaTE.Text = "Nuevo Elemento de la Tabla Especial" Then
                    If Ntb = 1 Then
                        CmdGlobal2.CommandText = "INSERT INTO " & cboTabla.SelectedItem.Text & "(NIVEL1_CODIGO, NIVEL1_DESCRIP,NIVEL1_SYS_EST,EMPRESA_CODIGO) VALUES(" & dCodigo & ",'" & txtTEDescripcion.Text.Trim & "','0','" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                        If DdlProceso.SelectedValue <> "< Seleccionar >" Then
                            CmdGlobal2.CommandText = "INSERT INTO TBTICKET_RELACION_PROCESO_GTP1 (GTP1_CODIGO, PROCESO_CODIGO) VALUES(" & dCodigo & ",'" & DdlProceso.SelectedValue & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                    ElseIf Ntb = 2 Then
                        CmdGlobal2.CommandText = "INSERT INTO " & cboTabla.SelectedItem.Text & "(NIVEL1_CODIGO,NIVEL2_CODIGO, NIVEL2_DESCRIP,NIVEL2_SYS_EST,EMPRESA_CODIGO) VALUES(" & dNivel1 & "," & dCodigo & ",'" & txtTEDescripcion.Text.Trim & "','0','" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    ElseIf Ntb = 3 Then
                        CmdGlobal2.CommandText = "INSERT INTO " & cboTabla.SelectedItem.Text & "(NIVEL2_CODIGO,NIVEL3_CODIGO, NIVEL3_DESCRIP,NIVEL3_SYS_EST,EMPRESA_CODIGO,NIVEL3_NS_DHM) VALUES(" & dNivel2 & "," & dCodigo & ",'" & txtTEDescripcion.Text.Trim & "','0','" & Session("CodEmpresa") & "','" & nTiempo & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                End If
                'cboTabla_SelectedIndexChanged(sender, e)
                btnTECancelar_Click(sender, e)
                Actualizar_Tablas(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub BtnTECerrar_Click(sender As Object, e As EventArgs) Handles BtnTECerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTablaEsp').modal('hide');", True)
    End Sub
    Private Sub Actualizar_Tablas(sender As Object, e As EventArgs)
        cboComponente.Items.Clear()
        cboElemento.Items.Clear()
        cboElemento2.Items.Clear()
        Dim obj As New ClsGtp_Procesos
        obj.LLenaComboItemTabEspRelacionProceso(Session("Ruta_emp"), cboComponente, "", "", "TBESP_GTP1", DdlProceso.SelectedValue, "0001", 1)
        cboComponente.SelectedValue = "< Seleccionar >"
        cboElemento.Items.Add("< Seleccionar >") : cboElemento.SelectedValue = "< Seleccionar >"
        cboElemento2.Items.Add("< Seleccionar >") : cboElemento2.SelectedValue = "< Seleccionar >"
        Call cboComponente_SelectedIndexChanged(sender, e)
        cboElemento.Enabled = False
        cboSubProd.Enabled = False
    End Sub
End Class
