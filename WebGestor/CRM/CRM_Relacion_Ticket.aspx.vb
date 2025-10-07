Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.Diagnostics
Imports System.IO

Partial Class CRM_CRM_Relacion_Ticket
    Inherits System.Web.UI.Page
    Private ObjList As New ClsGtp_Listados
    Private ObjProceso As New ClsGtp_Procesos
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblError.Text = ""
            Me.Page.Session.Timeout = 1080
            Call LlenaComboItem("TBOPC475", DdlEstado)
            DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            DdlElemento.Items.Add("< Seleccionar >") : DdlElemento.SelectedValue = "< Seleccionar >"
            DdlElemento2.Items.Add("< Seleccionar >") : DdlElemento2.SelectedValue = "< Seleccionar >"
            chkCliente.Checked = True
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim dt As DataTable
            dt = ObjList.GTP_ListaClientes_Top1(Session("Ruta_Emp"), "", "", "")
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtRuc.Text = Nu(dr(0))
                    txtRazon.Text = Nu(dr(1))
                    lblCodCliente.Text = Nu(dr(10))
                    lblCodEstado.Text = Nu(dr(11))
                    Exit For '
                Next
            End If
            dt = Nothing
            If lblCodEstado.Text <> "" Then
                ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                DdlProceso.Items.Add("< Seleccionar >") : DdlProceso.SelectedValue = "< Seleccionar >"
                If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                DdlProceso_SelectedIndexChanged(sender, e)
                If DdlComponente.SelectedValue <> "< Seleccionar >" Then DdlComponente_SelectedIndexChanged(sender, e)
            Else
                Call LlenaComboItem("TBOPC473", DdlProceso)
            End If
            DdlEstado.SelectedValue = "1"
            BtnListar_Click(sender, e)
            Llenar_Combos_Accion_Varias()
        End If
    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click

        LblError.Text = ""
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim dRow As DataRow
        dt.Columns.Add("C1")
        dt.Columns.Add("C2")
        dt.Columns.Add("C3")
        dt.Columns.Add("C4")
        dt.Columns.Add("C5")
        dt.Columns.Add("C6")
        dt.Columns.Add("C7")
        dt.Columns.Add("C8")
        dt.Columns.Add("C9")
        dt.Columns.Add("C10")
        'dt.Columns.Add("C11")
        'dt.Columns.Add("C12")
        'dt.Columns.Add("C13")
        'dt.Columns.Add("C14")
        dt.Columns.Add("C15")
        dt.Columns.Add("C16")
        dt.Columns.Add("C17")
        dt.Columns.Add("C18")
        dt.Columns.Add("C19")
        dt.Columns.Add("C20")
        'Ficha.Visible = False
        Try
            dtLista = Cargar_BD()
            If dtLista.Rows.Count > 0 Then
                For Each dr As DataRow In dtLista.Rows
                    dRow = dt.NewRow
                    dRow("c1") = Formato_Digito(Nu(dr("TICKET_CODIGO")), 5)
                    dRow("c2") = FormatoFecha(Nu(dr("TICKET_REPORTA_FECHA")))
                    dRow("c3") = FormatoHora(Nu(dr("TICKET_REPORTA_HORA")))
                    dRow("C4") = Nu(dr("CANAL"))
                    dRow("C5") = Nu(dr("NIVEL1_DESCRIP"))
                    dRow("C6") = Nu(dr("NOM_PROB1")) & IIf(Nu(dr("NOM_PROB2")) = "", "", " ; " + Nu(dr("NOM_PROB2")))
                    dRow("C7") = Nu(dr("PESTADO"))
                    dRow("C8") = Nu(dr("TICKET_MOTIVO"))
                    dRow("C9") = Nu(dr("TICKET_DESCRIPCION"))
                    dRow("C10") = Nu(dr("TICKET_SOLUCION"))
                    'dRow("C11") = FormatoFecha(Nu(dr("TICKET_VISTO_FECHA")))
                    'dRow("C12") = FormatoHora(Nu(dr("TICKET_VISTO_HORA")))
                    'dRow("C13") = FormatoFecha(Nu(dr("TICKET_ESTADO_FECHA")))
                    'dRow("C14") = FormatoHora(Nu(dr("TICKET_ESTADO_HORA")))
                    dRow("C15") = Nu(dr("PROCESO"))
                    dRow("C16") = Nu(dr("TICKET_PROCESO"))
                    dRow("C17") = Nu(dr("TICKET_ESTADO"))
                    dRow("C18") = Nu(dr("PERSON_ASIG_CLIENTE"))
                    dRow("C19") = Nu(dr("TBTICKET_CLIENTE_ASIGNADO_A"))
                    dRow("C20") = Nu(dr("TBTICKET_CLIENTE_CODIGO"))
                    dt.Rows.Add(dRow)
                Next
            End If
            GwLista.DataSource = dt
            GwLista.DataBind()
            LblTotalGWL.Text = " " + CStr(dt.Rows.Count())
            LblTotalGW.Visible = True
            LblTotalGWL.Visible = True

            'TablaClientes.Style.Add("height", "500px")
            'TablaClientes.Style.Add("width", "1000px")
            'TablaClientes.Style.Add("overflow", "auto")
            'TablaClientes.Style.Add("padding-left", "15px")
            'TablaClientes.Style.Add("margin-left", "15px")

            dt = Nothing
            GvTraking.DataSource = dt
            GvTraking.DataBind()
            GvCorreo.DataSource = dt
            GvCorreo.DataBind()
            GvLlamada.DataSource = dt
            GvLlamada.DataBind()
            GvAsignados.DataSource = dt
            GvAsignados.DataBind()
            GvEstados.DataSource = dt
            GvEstados.DataBind()
        Catch Ex As SqlException
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub


    Private Function Cargar_BD() As DataTable

        Call Crear_Vista_CRM()
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
        Dim Filtros1 As String : Filtros1 = ""
        Dim Filtros2 As String : Filtros2 = ""
        Dim cmdSql As New SqlCommand
        'Opera = " OR "
        Cargar_BD = Nothing
        Sql = " select * FROM V_CRM_LISTA ORDER BY TICKET_ESTADO_FECHA + TICKET_ESTADO_HORA DESC "
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Me.Page.Session.Timeout = 1080
        Return Dt
    End Function
    Private Sub Crear_Vista_CRM()
        Dim psFechaIni As String = ""
        If txtFechaIni.Text <> "" Then psFechaIni = Right(txtFechaIni.Text, 4) & Mid(txtFechaIni.Text, 4, 2) & Left(txtFechaIni.Text, 2)
        Dim psFechaFin As String = ""
        If txtFechaFin.Text <> "" Then psFechaFin = Right(txtFechaFin.Text, 4) & Mid(txtFechaFin.Text, 4, 2) & Left(txtFechaFin.Text, 2)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Cn.Open()
        cmdSql.Connection = Cn
        cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_CRM_LISTA]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_CRM_LISTA]"
        cmdSql.ExecuteNonQuery()
        cmdSql.CommandText = " CREATE VIEW V_CRM_LISTA AS SELECT '' AS MARCA, '' AS TIEMPO_ABIERTO_ESTADO,'' AS EST_FUERA_TIEMPO,'' AS TIEMPO_ACTUAL_TICKET,'' AS SLA,'' AS FUERA_TIEMPO,D.REGISTRO_OBSERVACION, B.TBTICKET_CLIENTE_CODIGO,TBTICKET_CLIENTE_ASIGNADO_A, " _
                            & " TICKET_PROGRAMA_FECHA,TICKET_DURACION,TICKET_DURACION_REAL,TICKET_PROGRAMA_HORA,TICKET_PROVEEDOR, TICKET_SOLUCION, TICKET_REPORTA_USUARIO, " _
                            & " TICKET_CONTACTO, TBTICKET_CONTACTO_APEPAT, TBTICKET_CONTACTO_APEMAT, TBTICKET_CONTACTO_NOMBRES, TBTICKET_CONTACTO_TELEF1, TBTICKET_CONTACTO_CEL1, TBTICKET_CONTACTO_EMAIL, TBTICKET_CLIENTE_CIF, TBTICKET_CLIENTE_NOMBRE, " _
                            & " TBTICKET_CLIENTE_COD_GPS, TBTICKET_CLIENTE_GRUPO, TBTICKET_CLIENTE_SOCIEDAD, TBTICKET_CLIENTE_GMI, TBTICKET_CLIENTE_MRHOJA_ENTRADA, TBTICKET_CLIENTE_MODO_FACTURACION, TBTICKET_CLIENTE_FECINICIO_ADQUIRA, TBTICKET_CLIENTE_USUA_ADQUIRA, " _
                            & " E.PERSON_APEPAT + ' ' + E.PERSON_APEMAT + ', ' + E.PERSON_NOMBRES AS PERSONAL1, TICKET_TIPO, P.NIVEL1_DESCRIP, P.COLOR_CODIGO, TICKET_PROBLEMA1, G.USUARI_APEPAT + ' ' + G.USUARI_APEMAT + ', ' + G.USUARI_NOMBRES AS PERSONAL2, TICKET_CODIGO," _
                            & " TICKET_PROBLEMA2, H.NIVEL2_DESCRIP AS NOM_PROB1, TICKET_ESTADO_FECHA, TICKET_ESTADO_HORA, I.NIVEL3_DESCRIP AS NOM_PROB2, I.NIVEL3_NS_DHM AS NIVELSERV, TICKET_DESCRIPCION, TICKET_MOTIVO, TICKET_PROCESO, TICKET_CANAL, TICKET_REPORTA_FECHA, " _
                            & " TICKET_REPORTA_HORA,TICKET_ESTADO,TICKET_SYS_EST,TICKET_SOLUCION_FECHA, TICKET_SOLUCION_HORA, TICKET_VISTO_FECHA, TICKET_VISTO_HORA, TICKET_ASIGNADO_FECHA,TICKET_ASIGNADO_HORA,TICKET_ASIGNADO_PERSONA,TICKET_ASIGVISTO_FECHA, TICKET_ASIGVISTO_HORA, " _
                            & " F.PERSON_APEPAT+' '+F.PERSON_APEMAT+', '+F.PERSON_NOMBRES AS PERSON_ASIG1, J.USUARI_APEPAT + ' ' + J.USUARI_APEMAT + ', ' + J.USUARI_NOMBRES AS PERSON_ASIG2, K.USUARI_APEPAT + ' ' + K.USUARI_APEMAT + ', ' + K.USUARI_NOMBRES AS PERSON_ASIG_CLIENTE," _
                            & " L.NIVEL1_DESCRIP AS NOM_PROB_ORIG,TICKET_TIPO_ORIG, M.NIVEL2_DESCRIP AS NOM_PROB_ORIG1, TICKET_PROBLEMA1_ORIG, N.NIVEL3_DESCRIP AS NOM_PROB_ORIG2, TICKET_PROBLEMA2_ORIG, " _
                            & " TBOPC480.ELEMEN_VALOR AS ESTADO_CLIENTE, TBOPC475.ELEMEN_VALOR AS PESTADO, TBOPC469.ELEMEN_VALOR AS ECONFORME, TBOPC473.ELEMEN_VALOR AS PROCESO,  TBOPC474.ELEMEN_VALOR AS CANAL " _
                            & " From TBTICKET AP INNER JOIN TBESP_GTP1 P ON AP.TICKET_TIPO = NIVEL1_CODIGO INNER JOIN TBTICKET_CLIENTE AS B ON B.TBTICKET_CLIENTE_CODIGO = AP.TICKET_PROVEEDOR  " _
                            & " AND B.TBTICKET_SYS_EST='0' INNER JOIN TBTICKET_CLIENTE_CONTACTO AS C ON C.TBTICKET_CLIENTE_CODIGO = B.TBTICKET_CLIENTE_CODIGO AND C.TBTICKET_CONTACTO_CODIGO = AP.TICKET_CONTACTO " _
                            & " AND TBTICKET_CONTACTO_SYS_EST ='0' LEFT JOIN TBTICKET_TRAKING AS D ON D.APROB_CODIGO = AP.TICKET_CODIGO AND D.FECHA_REGISTRO = AP.TICKET_ESTADO_FECHA AND D.ESTADO_REGISTRO = AP.TICKET_ESTADO " _
                            & " AND D.HORA_REGISTRO = AP.TICKET_ESTADO_HORA LEFT JOIN BDGRUPOEMPRESAS.DBO.TBPERSONAL AS E ON E.PERSON_CODIGO = AP.TICKET_REPORTA_USUARIO " _
                            & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBPERSONAL AS F ON F.PERSON_CODIGO = AP.TICKET_ASIGNADO_PERSONA LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS G ON G.USUARI_CODIGO = AP.TICKET_REPORTA_USUARIO AND LEFT(TICKET_REPORTA_USUARIO,4)='1111' " _
                            & " LEFT JOIN TBESP_GTP2 AS H ON H.NIVEL2_CODIGO = AP.TICKET_PROBLEMA1 LEFT JOIN TBESP_GTP3 AS I ON I.NIVEL3_CODIGO = AP.TICKET_PROBLEMA2 " _
                            & " LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS J ON J.USUARI_CODIGO = AP.TICKET_ASIGNADO_PERSONA LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS K ON K.USUARI_CODIGO = B.TBTICKET_CLIENTE_ASIGNADO_A " _
                            & " LEFT JOIN TBESP_GTP1 AS L ON L.NIVEL1_CODIGO = AP.TICKET_TIPO_ORIG LEFT JOIN TBESP_GTP2 AS M ON M.NIVEL2_CODIGO = AP.TICKET_PROBLEMA1_ORIG " _
                            & " LEFT JOIN TBESP_GTP3 AS N ON N.NIVEL3_CODIGO = AP.TICKET_PROBLEMA2_ORIG LEFT JOIN BDGrupoEmpresas.dbo.TBCELEMEN AS TBOPC480 ON TBOPC480.ELEMEN_TABLA = 'TBOPC480' AND TBOPC480.ELEMEN_CODIGO = B.TBTICKET_CLIENTE_ESTADO " _
                            & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC475 ON TBOPC475.ELEMEN_TABLA = 'TBOPC475' AND TBOPC475.ELEMEN_CODIGO = AP.TICKET_ESTADO " _
                            & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC469 ON TBOPC469.ELEMEN_TABLA = 'TBOPC469' AND TBOPC469.ELEMEN_CODIGO = AP.TICKET_REPORTA_USUARIO_CONFORMIDAD " _
                            & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC473 ON TBOPC473.ELEMEN_TABLA = 'TBOPC473' AND TBOPC473.ELEMEN_CODIGO = AP.TICKET_PROCESO " _
                            & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC474 ON TBOPC474.ELEMEN_TABLA = 'TBOPC474' AND TBOPC474.ELEMEN_CODIGO = AP.TICKET_CANAL " _
                            & " Where Not (TICKET_TIPO Is Null) "
        If psFechaIni <> "" And psFechaFin = "" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_REPORTA_FECHA = '" & psFechaIni & "'"
        If psFechaIni <> "" And psFechaFin <> "" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_REPORTA_FECHA BETWEEN '" & psFechaIni & "' AND '" & psFechaFin & "'"
        If DdlEstado.SelectedValue <> "< Todos >" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_ESTADO = '" & DdlEstado.SelectedValue & "'"
        If DdlProceso.SelectedValue <> "< Seleccionar >" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_proceso = '" & DdlProceso.SelectedValue & "'"
        If DdlComponente.SelectedValue <> "< Seleccionar >" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_TIPO = '" & DdlComponente.SelectedValue & "'"
        If DdlElemento.SelectedValue <> "< Seleccionar >" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_PROBLEMA1 = '" & DdlElemento.SelectedValue & "'"
        If DdlElemento2.SelectedValue <> "< Seleccionar >" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_PROBLEMA2 = '" & DdlElemento2.SelectedValue & "'"
        If lblCodCliente.Text <> "" Then cmdSql.CommandText = cmdSql.CommandText & " AND B.TBTICKET_CLIENTE_CODIGO = '" & lblCodCliente.Text & "'"
        'cmdSql.CommandText = cmdSql.CommandText & " ORDER BY TICKET_REPORTA_FECHA + TICKET_REPORTA_HORA DESC "
        cmdSql.ExecuteNonQuery()
    End Sub
    Protected Sub BtnListarTI_Click(sender As Object, e As EventArgs) Handles btnListarTI.Click
        LblError.Text = ""
        Dim ObjCont As New ClsCont_Listados
        Try
            Dim obj As New clsInv_Listados
            FlexTI.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            FlexTI.DataSource = ObjList.GTP_Lista_BusClientes(Session("Ruta_Emp"), txtBusRazon.Text, txtBusRuc.Text)
            FlexTI.DataBind()
            If FlexTI.Rows.Count > 1 Then
                LblTotalFlexL.Text = "Hay " & FlexTI.Rows.Count & " registros."
            ElseIf FlexTI.Rows.Count = 1 Then
                LblTotalFlexL.Text = "Hay 1 registro."
            Else
                LblTotalFlexL.Text = "No hay registros."
            End If
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub BtnDatos_Click(sender As Object, e As EventArgs) Handles btnDatos.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPopupExtender2').modal('show');", True)
    End Sub

    Private Sub BtnCerrarTI_Click(sender As Object, e As EventArgs) Handles btnCerrarTI.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPopupExtender2').modal('hide');", True)
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
            lblCodEstado.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTI.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If lblCodEstado.Text <> "" Then
                ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                DdlProceso.Items.Add("< Seleccionar >") : DdlProceso.SelectedValue = "< Seleccionar >"
                If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                DdlProceso_SelectedIndexChanged(sender, e)
                If DdlComponente.SelectedValue <> "< Seleccionar >" Then DdlComponente_SelectedIndexChanged(sender, e)
            Else
                Call LlenaComboItem("TBOPC473", DdlProceso)
            End If
            BtnListar_Click(sender, e)
            FlexTI.DataSource = Nothing
            FlexTI.DataBind()
            LblTotalFlexL.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPopupExtender2').modal('hide');", True)
        End If
    End Sub

    Private Sub DdlProceso_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProceso.SelectedIndexChanged
        If DdlProceso.Items.Count = 0 Then Exit Sub
        DdlComponente.Items.Clear()
        DdlElemento.Items.Clear()
        DdlElemento2.Items.Clear()
        If DdlProceso.SelectedValue = "< Seleccionar >" Then
            DdlEstado.Items.Clear()
            Call LlenaComboItem("TBOPC475", DdlEstado)
            DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            DdlElemento.Items.Add("< Seleccionar >") : DdlElemento.SelectedValue = "< Seleccionar >"
            DdlElemento2.Items.Add("< Seleccionar >") : DdlElemento2.SelectedValue = "< Seleccionar >"
            DdlEstado.SelectedValue = "1"
            BtnListar_Click(sender, e)
        Else
            DdlElemento.Items.Add("< Seleccionar >") : DdlElemento.SelectedValue = "< Seleccionar >"
            DdlElemento2.Items.Add("< Seleccionar >") : DdlElemento2.SelectedValue = "< Seleccionar >"
            DdlElemento.Enabled = False
            DdlElemento2.Enabled = False
            DdlEstado.Items.Clear()
            ObjProceso.LLenaComboItemTabEspRelacionProceso(Session("Ruta_Emp"), DdlComponente, "", "", "TBESP_GTP1", DdlProceso.SelectedValue, Session("CodEmpresa"), "1")
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC475", DdlEstado, DdlProceso.SelectedValue, Session("SiglaGrupoEmpresa"), "TBTICKET_RELACION_PROCESO_ESTADO")
            DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
            If DdlEstado.Items.Count > 0 Then
                DdlEstado.SelectedIndex = 0
            End If
            If DdlComponente.Items.Count > 0 Then
                DdlElemento.Enabled = True
            End If
        End If
    End Sub

    Private Sub DdlComponente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlComponente.SelectedIndexChanged
        If DdlComponente.Items.Count = 0 Then Exit Sub
        DdlElemento.Items.Clear()
        DdlElemento2.Items.Clear()
        DdlElemento.Items.Add("< Seleccionar >") : DdlElemento.SelectedValue = "< Seleccionar >"
        DdlElemento2.Items.Add("< Seleccionar >") : DdlElemento2.SelectedValue = "< Seleccionar >"
        If DdlComponente.SelectedValue <> "< Seleccionar >" Then
            LLenaComboItemTabEsp(DdlElemento, DdlComponente.SelectedValue, "", "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
            If DdlElemento.Items.Count > 0 Then DdlElemento.Enabled = True
        End If
    End Sub

    Private Sub DdlElemento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlElemento.SelectedIndexChanged
        If DdlElemento.Items.Count = 0 Then Exit Sub
        DdlElemento2.Items.Clear()
        DdlElemento2.Items.Add("< Seleccionar >") : DdlElemento2.SelectedValue = "< Seleccionar >"
        If DdlElemento.SelectedValue <> "< Seleccionar >" Then
            LLenaComboItemTabEsp(DdlElemento2, DdlComponente.SelectedValue, DdlElemento.SelectedValue, "TBESP_GTP1", "TBESP_GTP2", "TBESP_GTP3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
            If DdlElemento2.Items.Count > 0 Then DdlElemento2.Enabled = True
        End If
    End Sub
    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        DdlEstado.Items.Clear()
        DdlProceso.Items.Clear()
        DdlComponente.Items.Clear()
        DdlElemento.Items.Clear()
        DdlElemento2.Items.Clear()
        Call LlenaComboItem("TBOPC475", DdlEstado)
        Call LlenaComboItem("TBOPC473", DdlProceso)
        DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
        DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
        DdlElemento.Items.Add("< Seleccionar >") : DdlElemento.SelectedValue = "< Seleccionar >"
        DdlElemento2.Items.Add("< Seleccionar >") : DdlElemento2.SelectedValue = "< Seleccionar >"
        FlexTI.DataSource = Nothing
        FlexTI.DataBind()
        GwLista.DataSource = Nothing
        GwLista.DataBind()
        txtRuc.Text = ""
        txtRazon.Text = ""
        lblCodCliente.Text = ""
        lblCodEstado.Text = ""
        DdlEstado.SelectedValue = "1"
        BtnListar_Click(sender, e)
    End Sub
    Private Sub Cargar_DatosCliente(sender As Object, e As EventArgs)
        LblError.Text = ""
        Try
            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim dt As DataTable
            dt = ObjList.GTP_Lista_BusClientes(Session("Ruta_Emp"), "", txtRuc.Text)
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    txtRuc.Text = Nu(dr(0))
                    txtRazon.Text = Nu(dr(1))
                    lblCodCliente.Text = Nu(dr(10))
                    lblCodEstado.Text = Nu(dr(11))
                    Exit For
                Next
            End If
            dt = Nothing
            DdlComponente.Items.Clear()
            DdlElemento.Items.Clear()
            DdlElemento2.Items.Clear()
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            DdlElemento.Items.Add("< Seleccionar >") : DdlElemento.SelectedValue = "< Seleccionar >"
            DdlElemento2.Items.Add("< Seleccionar >") : DdlElemento2.SelectedValue = "< Seleccionar >"
            If lblCodCliente.Text <> "" Then
                ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                DdlProceso.Items.Add("< Seleccionar >") : DdlProceso.SelectedValue = "< Seleccionar >"
                If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                DdlProceso_SelectedIndexChanged(sender, e)
                If DdlComponente.SelectedValue <> "< Seleccionar >" Then DdlComponente_SelectedIndexChanged(sender, e)
            End If
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub TxtRuc_TextChanged(sender As Object, e As EventArgs) Handles txtRuc.TextChanged
        Call Cargar_DatosCliente(sender, e)
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub ChkCliente_CheckedChanged(sender As Object, e As EventArgs) Handles chkCliente.CheckedChanged
        If chkCliente.Checked = True Then
            txtRuc.ReadOnly = False
            txtRazon.ReadOnly = False
            btnDatos.Enabled = True
            txtRuc.Text = ""
            txtRazon.Text = ""
            lblCodCliente.Text = ""
        Else
            txtRuc.ReadOnly = True
            txtRazon.ReadOnly = True
            btnDatos.Enabled = False
            txtRuc.Text = ""
            txtRazon.Text = ""
            lblCodCliente.Text = ""
        End If
        BtnListar_Click(sender, e)
    End Sub

    Private Sub GwLista_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GwLista.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblError.Text = ""
        Dim psCodTicket As Double = 0
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        dt = Nothing
        GvTraking.DataSource = dt
        GvTraking.DataBind()
        GvCorreo.DataSource = dt
        GvCorreo.DataBind()
        GvLlamada.DataSource = dt
        GvLlamada.DataBind()
        GvAsignados.DataSource = dt
        GvAsignados.DataBind()
        GvEstados.DataSource = dt
        GvEstados.DataBind()
        'Ficha.Visible = False
        If e.CommandName = "Accion" Then
            LblNroTicket.Text = GwLista.Rows(Index).Cells(4).Text
            LblCodProceso.Text = GwLista.Rows(Index).Cells(15).Text
            LblCodEstadoMM.Text = GwLista.Rows(Index).Cells(16).Text
            LblPersonal.Text = GwLista.Rows(Index).Cells(18).Text
            LblElemento.Text = GwLista.Rows(Index).Cells(6).Text
            LblFechaReporta.Text = GwLista.Rows(Index).Cells(2).Text
            LblHoraReporta.Text = GwLista.Rows(Index).Cells(3).Text
            lblCodCliente.Text = GwLista.Rows(Index).Cells(19).Text

            Dim obj As New Cls_Cliente
            Dim psconexion As String = Session("Ruta_Emp")
            dt = obj.Llenar_Acciones(psconexion)

            GvListaAcciones.DataSource = dt
            GvListaAcciones.DataBind()

            If dt.Rows.Count > 1 Then
                LblTotalAccionL.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf GvListaAcciones.Rows.Count = 1 Then
                LblTotalAccionL.Text = "Hay 1 registro."
            Else
                LblTotalAccionL.Text = "No hay registros."
            End If

            TxtNroTicketAT.Text = LblNroTicket.Text
            TxtNroTicketRT.Text = LblNroTicket.Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
        ElseIf e.CommandName = "Mostrar Ticket" Then
            Response.Redirect("~/CRM/CRM_Registrar.aspx?WpkDi=" + GwLista.Rows(Index).Cells(4).Text.ToString())
        ElseIf e.CommandName = "Lista Documentos" Then
            LblNroTicket.Text = GwLista.Rows(Index).Cells(4).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalListaDocumentos').modal('show');", True)
            Listar_Documentos()
            LblTituloDcumentos.Text = "Relación de Documentos del Ticket " + LblNroTicket.Text
        ElseIf e.CommandName = "Tracking" Then
            Salida1.Visible = False
            Salida2.Visible = False
            Salida3.Visible = False
            psCodTicket = GwLista.Rows(Index).Cells(4).Text
            dt = ObjList.CRM_Tracking_Acciones(Session("CodEmpresa"), Session("Ruta_Emp"), psCodTicket)
            GvTraking.DataSource = dt
            GvTraking.DataBind()
            dt = Nothing
            dt = ObjList.CRM_Tracking_Correos(Session("CodEmpresa"), Session("Ruta_Emp"), psCodTicket)
            GvCorreo.DataSource = dt
            GvCorreo.DataBind()
            dt = Nothing
            dt = ObjList.CRM_Tracking_Llamadas(Session("CodEmpresa"), Session("Ruta_Emp"), psCodTicket)
            GvLlamada.DataSource = dt
            GvLlamada.DataBind()
            dt = Nothing
            dt = ObjList.CRM_Tracking_Asignados(Session("CodEmpresa"), Session("Ruta_Emp"), psCodTicket)
            GvAsignados.DataSource = dt
            GvAsignados.DataBind()
            dt = Nothing
            dt = ObjList.CRM_Tracking_Estados(Session("CodEmpresa"), Session("Ruta_Emp"), psCodTicket)
            GvEstados.DataSource = dt
            GvEstados.DataBind()
            'Ficha.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTraking').modal('show');", True)

        End If
    End Sub
    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click

        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        GwLista.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(GwLista)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=relacionticket.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Private Sub BtnCancelarAccion_Click(sender As Object, e As EventArgs) Handles BtnCancelarAccion.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('hide');", True)
    End Sub

    Private Sub GvListaAcciones_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaAcciones.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Relacion_Ticket
        Dim obj1 As New Cls_Estado_Ticket
        Dim obj2 As New Cls_Enviar_Email
        Dim dt As New DataTable
        Dim dbRow As DataRow
        Dim dtLista As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As String = LblCodProceso.Text.ToString
        Dim Estado As String = LblCodEstadoMM.Text.ToString
        Dim accion As String = GvListaAcciones.Rows(Index).Cells(1).Text

        If e.CommandName = "Seleccionar" Then

            If accion = 11 Then 'cambio etsdao
                CodAcción.Text = accion
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').one('hidden.bs.modal', function() { $('#ModalCambiarEstado').modal('show'); }).modal('hide');", True)
                dt = obj1.Llenar_Combo_Estado_Ticket(psconexion, Codigo)
                DdlEstadoModal.DataSource = dt
                DdlEstadoModal.DataValueField = "ELEMEN_CODIGO"
                DdlEstadoModal.DataTextField = "ELEMEN_VALOR"
                DdlEstadoModal.DataBind()
                DdlEstadoModal.Items.Add("< Seleccionar >")
                DdlEstadoModal.SelectedValue = "< Seleccionar >"
                TxtHoraCE.Value = FormatoHoraSeg(HoraActual) ' DateTime.Now.ToShortTimeString()
                TxtFechaCE.Value = DateTime.Now.ToString("yyyy-MM-dd")
                TxtNroTicketCA.Text = LblNroTicket.Text
            ElseIf accion = 14 Then 'acciones varias
                CodAcción.Text = accion
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').one('hidden.bs.modal', function() { $('#ModalAplicarAccionesVarias').modal('show'); }).modal('hide');", True)
                TxtHoraAV.Value = DateTime.Now.ToShortTimeString()
                TxtFechaAV.Value = DateTime.Now.ToString("yyyy-MM-dd")
                TxtNroAccionAV.Text = obj.Codigo(psconexion, Codigo)
                Listar_Acciones_Varios()
            ElseIf accion = 10 Then 'reabrir ticket
                If Estado.ToString = ("2") Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').one('hidden.bs.modal', function() { $('#ModalReabrirTicket').modal('show'); }).modal('hide');", True)
                    TxtHoraRT.Value = DateTime.Now.ToShortTimeString()
                    TxtFechaRT.Value = DateTime.Now.ToString("yyyy-MM-dd")
                Else
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('El Ticket está abierto');", True)
                End If
            ElseIf accion = 13 Then 'reasignar o asignar
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').one('hidden.bs.modal', function() { $('#ModalReasignarAsignarTicket').modal('show'); }).modal('hide');", True)
                Listar_Usuarios_Ticket()
            ElseIf accion = 1 Or accion = 7 Or accion = 15 Or accion = 4 Then '4 enviar email 15 llamada entrante 7 llamar por telefono
                dt = obj2.Buscar_Campos_Cliente(psconexion, lblCodCliente.Text)
                dbRow = dt.Rows(0)
                If accion = 1 Then ' actualizar cliente
                    Response.Redirect("~/Contabilidad/Contabilidad_Personas.aspx?WpkDi=" + dbRow("TBTICKET_CLIENTE_CODPERSONA").ToString + "&KllPd0s=" + dbRow("TBTICKET_CLIENTE_CIF").ToString +
                                      "&Ni830dHuciPLO=" + dbRow("TBTICKET_CLIENTE_CIF").ToString + "&093008roijfoiJ_sfF=" + dbRow("TBTICKET_CLIENTE_NOMBRE").ToString +
                                      "&Lpoeh58FJIJS0lk=" + lblCodCliente.Text + "&KJoi09jdJA90dIW=" + LblNroTicket.Text)
                ElseIf accion = 7 Or accion = 15 Or accion = 4 Then
                    If Nu(dbRow("TBTICKET_CLIENTE_CODPERSONA")) = "" Then
                        ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Debe actualizar datos del Ticket');", True)
                    Else
                        If accion = 7 Then
                            Response.Redirect("~/CallCenter/CallCenter_Pantalla_Operador.aspx?WpkDi=" + "LLS" + "&Lpoeh58FJIJS0lk=" + dbRow("TBTICKET_CLIENTE_CODPERSONA") + "&Ni830dHuciPLO=" + lblCodCliente.Text + "&KJoi09jdJA90dIW=" + LblNroTicket.Text)
                        ElseIf accion = 4 Then
                            Response.Redirect("CRM_Enviar_Email.aspx?WpkDi=" + LblNroTicket.Text + "&OfnoiafRFS=" + LblElemento.Text + "&ASIFasfajsAS=" + LblFechaReporta.Text + "&ioanfmOAISN=" + LblHoraReporta.Text + "&aDFWQASRAF=" + lblCodCliente.Text)
                        ElseIf accion = 15 Then
                            Response.Redirect("~/CallCenter/CallCenter_Pantalla_Operador.aspx?WpkDi=" + "LLE" + "&Lpoeh58FJIJS0lk=" + dbRow("TBTICKET_CLIENTE_CODPERSONA") + "&Ni830dHuciPLO=" + lblCodCliente.Text + "&KJoi09jdJA90dIW=" + LblNroTicket.Text)
                        End If
                    End If
                End If
            ElseIf accion = 17 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_Recepcion_Registrar.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 18 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_Almacen_Salida.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 19 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_GenerarLista.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 20 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_Lista_EquiposATratar.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 3 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Dim NroTicket As String = LblNroTicket.Text.ToString
                Response.Redirect("~/CRM/Documento_TemasAyuda.aspx?WpkDi=" + NroTicket + "&param2=CRM/CRM_Relacion_Ticket.aspx")
            ElseIf accion = 24 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_CCosto_Salida.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 25 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_Ingreso_Bienes_Desuso.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 26 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_Recepcion_IngSeries.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 27 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_GenerarGuia_SinSalida.aspx?WpkDi=" + LblNroTicket.Text)
            ElseIf accion = 28 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAsignarAcciones').modal('show');", True)
                Response.Redirect("~/Inventario/Inventario_SalidasEnv_xRecibir.aspx?WpkDi=" + LblNroTicket.Text)
            End If
        End If
    End Sub

    Sub llamar()
        Dim dialer As New TAPI3Lib.RequestMakeCall()
        dialer.MakeCall("923916673", "Daniel", "Randie", "Hola")
    End Sub

    Private Sub BtnLlamar_Click(sender As Object, e As EventArgs) Handles BtnLlamar.Click
        llamar()
    End Sub

    Private Sub BtnCerrarEstado_Click(sender As Object, e As EventArgs) Handles BtnCerrarEstado.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCambiarEstado').one('hidden.bs.modal', function() { $('#ModalAsignarAcciones').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnCerrarAV_Click(sender As Object, e As EventArgs) Handles BtnCerrarAV.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAplicarAccionesVarias').one('hidden.bs.modal', function() { $('#ModalAsignarAcciones').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnGuardarEstado_Click(sender As Object, e As EventArgs) Handles BtnGuardarEstado.Click
        Dim obj As New Cls_Relacion_Ticket
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Estado As String = ""
        If DdlEstadoModal.SelectedValue.ToString <> "< Seleccionar >" Then
            Estado = DdlEstadoModal.SelectedValue.ToString
        End If
        Dim Fecha As String = TxtFechaCE.Value.ToString
        Dim Hora As String = TxtHoraCE.Value.ToString
        Dim Observacion As String = TxtObservacion.Value.ToString
        Dim Codigo As Double = 0
        If LblNroTicket.Text.ToString <> "" Then
            Codigo = Nz(LblNroTicket.Text.ToString)
        End If
        Dim EstadoReferencia As Double = 0
        If LblCodEstadoMM.Text.ToString <> "" Then
            EstadoReferencia = Nz(LblCodEstadoMM.Text.ToString)
        End If
        Dim dt As DataTable

        If Estado.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Estado');", True)
        ElseIf Fecha.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una Fecha Valida');", True)
        ElseIf Hora.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Hora valida');", True)
        Else
            Dim anio As String = Fecha.Substring(0, 4)
            Dim mes As String = Fecha.Substring(5, 2)
            Dim dia As String = Fecha.Substring(8, 2)
            Fecha = anio + mes + dia
            Dim h As String = Hora.Substring(0, 2)
            Dim m As String = Hora.Substring(3, 2)
            Hora = h + m
            obj.Actualizar_Estado_Ticket(psconexion, Estado, Fecha, Hora, Codigo)
            dt = obj.Lista_Relacion_Estado_Ticket_Cliente(psconexion, Estado)
            If dt.Rows.Count > 0 Then
                obj.Atualizar_Estado_Ticket_Cliente(psconexion, Estado, Codigo)
            End If
            obj.Actualizar_Traking_Estado_Ticket(psconexion, Hora, Fecha, Codigo, Estado)

            obj.Ingresar_Accion_Traking_Estado_Cliente(psconexion, Codigo, Fecha, Hora, Estado, Session("User"), Observacion)

            obj.Ingresar_Traking_Accion_Estado(psconexion, Codigo, Fecha, Hora, Session("User"), EstadoReferencia)

            BtnListar_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCambiarEstado').one('hidden.bs.modal', function() { $('#ModalAsignarAcciones').modal('show'); }).modal('hide');", True)
        End If
    End Sub
    Protected Sub Llenar_Combos_Accion_Varias()
        Dim obj As New Cls_Relacion_Ticket
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Canal(psconexion)
        DdlCanalAV.DataSource = dt
        DdlCanalAV.DataValueField = "ELEMEN_CODIGO"
        DdlCanalAV.DataTextField = "ELEMEN_VALOR"
        DdlCanalAV.DataBind()
        DdlCanalAV.Items.Add("< Seleccionar >")
        DdlCanalAV.SelectedValue = "< Seleccionar >"

        dt = obj.Llenar_Combo_Accion(psconexion)
        DdlAccionAV.DataSource = dt
        DdlAccionAV.DataValueField = "ELEMEN_CODIGO"
        DdlAccionAV.DataTextField = "ELEMEN_VALOR"
        DdlAccionAV.DataBind()
        DdlAccionAV.Items.Add("< Seleccionar >")
        DdlAccionAV.SelectedValue = "< Seleccionar >"

        dt = obj.Llenar_Combo_Contacto(psconexion)
        DdlContactoAV.DataSource = dt
        DdlContactoAV.DataValueField = "ELEMEN_CODIGO"
        DdlContactoAV.DataTextField = "ELEMEN_VALOR"
        DdlContactoAV.DataBind()
        DdlContactoAV.Items.Add("< Seleccionar >")
        DdlContactoAV.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub Listar_Acciones_Varios()
        Dim obj As New Cls_Relacion_Ticket
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim NroTicket As String = LblNroTicket.Text.ToString
        dt = obj.Lista_Acciones_Varios(psconexion, NroTicket)
        GvListaAccionesVarias.DataSource = dt
        GvListaAccionesVarias.DataBind()
    End Sub
    Private Sub BtnGuardarAV_Click(sender As Object, e As EventArgs) Handles BtnGuardarAV.Click
        Dim obj As New Cls_Relacion_Ticket
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim NroTicket As String = LblNroTicket.Text.ToString
        Dim Fecha As String = TxtFechaAV.Value.ToString
        Dim Hora As String = TxtHoraAV.Value.ToString
        Dim Correlativo As String = TxtNroAccionAV.Text.ToString
        Dim Canal As String = DdlCanalAV.SelectedValue.ToString
        Dim UsuarioAccion As String = LblPersonal.Text.ToString
        Dim Accion As String = DdlAccionAV.SelectedValue.ToString
        Dim Contacto As String = DdlContactoAV.SelectedValue.ToString
        Dim DescripcionAccion As String = TxtDescripcionAccion.Value.ToString
        Dim TicketEstado As String = LblCodEstadoMM.Text.ToString

        If Fecha.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una Fecha Valida');", True)
        ElseIf Hora.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese una Hora valida');", True)
        ElseIf Canal.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Seleccione un Canal');", True)
        ElseIf Accion.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Seleccione una Acción');", True)
        ElseIf Contacto.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Seleccione un Contacto');", True)
        Else
            Dim anio As String = Fecha.Substring(0, 4)
            Dim mes As String = Fecha.Substring(5, 2)
            Dim dia As String = Fecha.Substring(8, 2)
            Fecha = anio + mes + dia
            Dim h As String = Hora.Substring(0, 2)
            Dim m As String = Hora.Substring(3, 2)
            Hora = h + m
            obj.Ingresar_Acciones_Varias(psconexion, NroTicket, Correlativo, Accion, DescripcionAccion, Canal, UsuarioAccion, Fecha, Hora, "", Contacto, TicketEstado)
            Limpiar_Cajas_Acciones_Varias_Modal()
            Listar_Acciones_Varios()
        End If
    End Sub

    Private Sub BtnNuevaAccionAV_Click(sender As Object, e As EventArgs) Handles BtnNuevaAccionAV.Click
        Limpiar_Cajas_Acciones_Varias_Modal()
    End Sub

    Protected Sub Limpiar_Cajas_Acciones_Varias_Modal()
        Dim obj As New Cls_Relacion_Ticket
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As String = LblNroTicket.Text.ToString
        TxtHoraAV.Value = DateTime.Now.ToShortTimeString()
        TxtFechaAV.Value = DateTime.Now.ToString("yyyy-MM-dd")
        TxtNroAccionAV.Text = obj.Codigo(psconexion, Codigo)
        DdlCanalAV.SelectedValue = "< Seleccionar >"
        DdlAccionAV.SelectedValue = "< Seleccionar >"
        DdlContactoAV.SelectedValue = "< Seleccionar >"
        TxtDescripcionAccion.Value = ""
        Listar_Acciones_Varios()
    End Sub

    Private Sub BtnCerrarRT_Click(sender As Object, e As EventArgs) Handles BtnCerrarRT.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalReabrirTicket').one('hidden.bs.modal', function() { $('#ModalAsignarAcciones').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnGuardarRT_Click(sender As Object, e As EventArgs) Handles BtnGuardarRT.Click
        Dim obj As New Cls_Relacion_Ticket
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim FechaReabierto As String = TxtFechaRT.Value.ToString
        Dim HoraReabierto As String = TxtHoraRT.Value.ToString
        Dim Usuario As String = LblPersonal.Text.ToString
        Dim EstadoFecha As String = LblFechaEstado.Text.ToString
        Dim EstadHora As String = LblHoraEstado.Text.ToString
        Dim Motivo As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(TxtMotivoRT.Value.ToString.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Dim NroTicket As String = LblNroTicket.Text.ToString

        Dim anio As String = FechaReabierto.Substring(0, 4)
        Dim mes As String = FechaReabierto.Substring(5, 2)
        Dim dia As String = FechaReabierto.Substring(8, 2)
        FechaReabierto = anio + mes + dia
        Dim h As String = HoraReabierto.Substring(0, 2)
        Dim m As String = HoraReabierto.Substring(3, 2)
        HoraReabierto = h + m

        Dim anio2 As String = EstadoFecha.Substring(6, 4)
        Dim mes2 As String = EstadoFecha.Substring(3, 2)
        Dim dia2 As String = EstadoFecha.Substring(0, 2)
        EstadoFecha = anio2 + mes2 + dia2
        Dim h2 As String = EstadHora.Substring(0, 2)
        Dim m2 As String = EstadHora.Substring(3, 2)
        EstadHora = h2 + m2

        obj.Reabrir_Ticket(psconexion, FechaReabierto, HoraReabierto, Usuario, EstadoFecha, EstadHora, Motivo, NroTicket)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalReabrirTicket').modal('hide');", True)
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub Listar_Usuarios_Ticket()
        Dim obj As New Cls_Relacion_Ticket
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Listar_Usuarios_Ticket(psconexion)
        GvListaUsuarios.DataSource = dt
        GvListaUsuarios.DataBind()

        If dt.Rows.Count > 1 Then
            LblTotalUsuariosL.Text = "Hay " & dt.Rows.Count & " registros."
        ElseIf GvListaAcciones.Rows.Count = 1 Then
            LblTotalUsuariosL.Text = "Hay 1 registro."
        Else
            LblTotalUsuariosL.Text = "No hay registros."
        End If
    End Sub
    Private Sub BtnCerrarAT_Click(sender As Object, e As EventArgs) Handles BtnCerrarAT.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalReasignarAsignarTicket').one('hidden.bs.modal', function() { $('#ModalAsignarAcciones').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub GvListaUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaUsuarios.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim CodUsuario As String = GvListaUsuarios.Rows(Index).Cells(1).Text
        LblCodUsuario.Text = CodUsuario.ToString
        Dim obj As New Cls_Relacion_Ticket
        Dim psconexion As String = Session("Ruta_Emp")
        Dim NroTicket As String = LblNroTicket.Text.ToString
        Dim Usuario As String = LblPersonal.Text.ToString
        Dim Fecha As String = DateTime.Now.ToString("yyyy-MM-dd")
        Dim Hora As String = DateTime.Now.ToShortTimeString()
        Dim Asesor As String = LblCodUsuario.Text.ToString
        If e.CommandName = "Aceptar" Then
            Dim anio As String = Fecha.Substring(0, 4)
            Dim mes As String = Fecha.Substring(5, 2)
            Dim dia As String = Fecha.Substring(8, 2)
            Fecha = anio + mes + dia
            Dim h As String = Hora.Substring(0, 2)
            Dim m As String = Hora.Substring(3, 2)
            Hora = h + m

            obj.Asignar_Ticket(psconexion, NroTicket, Usuario, Fecha, Fecha, Hora, Hora)
            obj.Insertar_Asignacion(psconexion, NroTicket, Fecha, Hora, Asesor, Asesor)
            obj.Insertar_Acciones_Ticket(psconexion, "13", NroTicket, Fecha, Hora, Session("User"), Asesor)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalReasignarAsignarTicket').modal('hide');", True)
            BtnListar_Click(sender, e)
        End If
    End Sub


    Protected Sub Listar_Ticket_Reprogramado()
        Dim obj As New Cls_Relacion_Ticket
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Listar_Ticket_Reprogramado(psconexion)
        GvListaReprogramados.DataSource = dt
        GvListaReprogramados.DataBind()

        If dt.Rows.Count > 1 Then
            LblTotalReprogramadosL.Text = "Hay " & dt.Rows.Count & " registros."
        ElseIf GvListaAcciones.Rows.Count = 1 Then
            LblTotalReprogramadosL.Text = "Hay 1 registro."
        Else
            LblTotalReprogramadosL.Text = "No hay registros."
        End If
    End Sub

    Private Sub BtnReprogramados_Click(sender As Object, e As EventArgs) Handles BtnReprogramados.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTicketsReprogramados').modal('show');", True)
    End Sub

    Private Sub BtnCerrarReprogramados_Click(sender As Object, e As EventArgs) Handles BtnCerrarReprogramados.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTicketsReprogramados').modal('hide');", True)
    End Sub

    Private Sub BtnListarReprogramados_Click(sender As Object, e As EventArgs) Handles BtnListarReprogramados.Click
        Listar_Ticket_Reprogramado()
    End Sub

    Protected Sub Listar_Documentos()
        Dim obj As New Cls_Documentos
        Dim dt As New DataTable
        Dim NroTicket As Double = Nz(LblNroTicket.Text.ToString)
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Documentos(psconexion, Session("User"), NroTicket)
        GvListaDocumentos.DataSource = dt
        GvListaDocumentos.DataBind()

        If dt.Rows.Count > 1 Then
            LblTotalDocumentosL.Text = "Hay " & dt.Rows.Count & " registros."
        ElseIf GvListaAcciones.Rows.Count = 1 Then
            LblTotalDocumentosL.Text = "Hay 1 registro."
        Else
            LblTotalDocumentosL.Text = "No hay registros."
        End If

        TablaListaDocumentos.Style.Add("width", "600px")
        TablaListaDocumentos.Style.Add("overflow", "auto")
        TablaListaDocumentos.Style.Add("padding-left", "0px")
    End Sub

    Private Sub BtnCerrarDocumentos_Click(sender As Object, e As EventArgs) Handles BtnCerrarDocumentos.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalListaDocumentos').modal('hide');", True)
    End Sub

    Private Sub BtnAgregarDocumentos_Click(sender As Object, e As EventArgs) Handles BtnAgregarDocumentos.Click
        Dim NroTicket As String = LblNroTicket.Text.ToString
        Response.Redirect("~/CRM/Documento_TemasAyuda.aspx?WpkDi=" + NroTicket + "&param2=CRM/CRM_Relacion_Ticket.aspx")
    End Sub

    Private Sub GvListaDocumentos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaDocumentos.RowCommand

        Dim dt As New DataTable
        Dim psCodModulo As String = "18"
        Dim psNombreCarpeta As String = ""
        Dim psNombreTabla As String = ""
        Dim psCodTablaRelacion As String = ""
        Dim Ruta_Final As String = Server.MapPath("Temas")
        Dim objSeg As New ModuloSeguridad
        If e.CommandName = "OpenFile" Then
            ' Obtener la ruta del archivo del CommandArgument
            Dim filePath As String = e.CommandArgument.ToString()
            dt = objSeg.Obtener_TablaRelacion(Session("Ruta_Emp"), psCodModulo, "")
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    psNombreTabla = dr(1).ToString : psNombreCarpeta = dr(4).ToString : psCodTablaRelacion = dr(6).ToString
                Next
            End If
            If psNombreCarpeta <> "" Then
                Ruta_Final = Server.MapPath("Temas\" & psNombreCarpeta)
            End If
            Ruta_Final = Ruta_Final & "\" & filePath

            If File.Exists(Ruta_Final) Then
                Response.Redirect("~/CRM/CRM_Exportar.aspx?parametro2=" & Server.UrlEncode(Ruta_Final))
            End If

        End If
    End Sub
    Private Sub BtnCerrarTraking_Click(sender As Object, e As EventArgs) Handles BtnCerrarTraking.Click
        Dim dtdatos As New DataTable
        dtdatos = Nothing
        Salida1.Visible = False
        Salida2.Visible = False
        Salida3.Visible = False
        Recepcion1.Visible = False
        Recepcion2.Visible = False
        Recepcion3.Visible = False
        CentroCosto.Visible = False
        FlexRecep.DataSource = dtdatos
        FlexRecep.DataBind()
        FlexRecepDet.DataSource = dtdatos
        FlexRecepDet.DataBind()
        gridSalida.DataSource = dtdatos
        gridSalida.DataBind()
        gridSalidaEq.DataSource = dtdatos
        gridSalidaEq.DataBind()
        gridSalidaAcc.DataSource = dtdatos
        gridSalidaAcc.DataBind()
        gridCC.DataSource = dtdatos
        gridCC.DataBind()
        grdCCDetAcc.DataSource = dtdatos
        grdCCDetAcc.DataBind()
        grdCCDet.DataSource = dtdatos
        grdCCDet.DataBind()
        lblgrdCCDet.Visible = False

        lblgrdCCDetAcc.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTraking').modal('hide');", True)
    End Sub

    Private Sub GvTraking_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvTraking.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInv_Listados
        Dim psCodSalida As Double = 0
        Dim dtDatos As New DataTable
        dtDatos = Nothing
        Salida1.Visible = False
        Salida2.Visible = False
        Salida3.Visible = False

        Recepcion1.Visible = False
        Recepcion2.Visible = False
        Recepcion3.Visible = False
        CentroCosto.Visible = False
        CentroCostoAcc.Visible = False
        CentroCostoEq.Visible = False
        FlexRecep.DataSource = dtDatos
        FlexRecep.DataBind()
        FlexRecepDet.DataSource = dtDatos
        FlexRecepDet.DataBind()
        gridSalida.DataSource = dtDatos
        gridSalida.DataBind()
        gridSalidaEq.DataSource = dtDatos
        gridSalidaEq.DataBind()
        gridSalidaAcc.DataSource = dtDatos
        gridSalidaAcc.DataBind()
        gridCC.DataSource = dtDatos
        gridCC.DataBind()
        grdCCDetAcc.DataSource = dtDatos
        grdCCDetAcc.DataBind()
        grdCCDet.DataSource = dtDatos
        grdCCDet.DataBind()
        lblgrdCCDet.Visible = False

        lblgrdCCDetAcc.Visible = False
        If e.CommandName = "Detalle" Then
            If GvTraking.Rows(Index).Cells(8).Text = "3" Then
                psCodSalida = GvTraking.Rows(Index).Cells(7).Text

                Dim objDoc As New Cls_Documentos
                Dim objSeg As New ModuloSeguridad
                Dim dt As New DataTable
                Dim psNombreArchivo As String = ""
                Dim pdCodDocumento As Double = Nz(GvTraking.Rows(Index).Cells(7).Text)
                Dim psconexion As String = Session("Ruta_Emp")
                dt = objDoc.Buscar_Documento(psconexion, Session("User"), pdCodDocumento)
                For Each dr As DataRow In dt.Rows
                    psNombreArchivo = Nu(dr("TEMA_AYUDA_NOMBRE_DOC"))
                Next

                dt = Nothing

                Dim psCodModulo As String = "18"
                Dim psNombreCarpeta As String = ""
                Dim psNombreTabla As String = ""
                Dim psCodTablaRelacion As String = ""
                Dim Ruta_Final As String = Server.MapPath("Temas")
                dt = objSeg.Obtener_TablaRelacion(Session("Ruta_Emp"), psCodModulo, "")
                If dt.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dt.Rows
                        psNombreTabla = dr(1).ToString : psNombreCarpeta = dr(4).ToString : psCodTablaRelacion = dr(6).ToString
                    Next
                End If
                If psNombreCarpeta <> "" Then
                    Ruta_Final = Server.MapPath("Temas\" & psNombreCarpeta)
                End If
                Ruta_Final = Ruta_Final & "\" & psNombreArchivo
                If File.Exists(Ruta_Final) Then
                    Response.Redirect("~/CRM/CRM_Exportar.aspx?parametro2=" & Server.UrlEncode(Ruta_Final))
                End If

            ElseIf GvTraking.Rows(Index).Cells(8).Text = "18" Then
                lblCodSalida.Text = "Nro de Salida " & GvTraking.Rows(Index).Cells(7).Text
                Salida1.Visible = True
                Salida2.Visible = True
                Salida3.Visible = True
                psCodSalida = GvTraking.Rows(Index).Cells(7).Text
                gridSalida.DataSource = obj.Lista_SalidaAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "", "", "", "")
                gridSalida.DataBind()
                dtDatos = obj.Lista_DetalleSinSeries_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "1")
                If dtDatos.Rows.Count > 0 Then
                    gridSalidaAcc.DataSource = dtDatos
                    gridSalidaAcc.DataBind()
                End If
                'Label2
                If dtDatos.Rows.Count > 0 Then
                    Label2.Visible = True
                Else
                    Label2.Visible = False
                End If
                dtDatos = Nothing

                dtDatos = obj.Lista_Detalle_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "1")
                gridSalidaEq.DataSource = dtDatos
                gridSalidaEq.DataBind()

                If dtDatos.Rows.Count > 0 Then
                    LblEtiq35.Visible = True
                Else
                    LblEtiq35.Visible = False
                End If
                dtDatos = Nothing
            ElseIf GvTraking.Rows(Index).Cells(8).Text = "17" Then
                LblCodRecepcion.Text = "Nro de Recepción " & GvTraking.Rows(Index).Cells(7).Text
                psCodSalida = GvTraking.Rows(Index).Cells(7).Text
                Recepcion1.Visible = True
                FlexRecep.DataSource = obj.Lista_xCodRecepcion(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida)
                FlexRecep.DataBind()
                Dim dt As New DataTable
                dt = obj.Lista_Recepcion_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida)
                If dt.Rows.Count > 0 Then
                    Recepcion2.Visible = True
                    Recepcion3.Visible = True
                    FlexRecepDet.DataSource = dt
                    FlexRecepDet.DataBind()
                    If dt.Rows.Count > 0 Then
                        lblRegDet.Text = "Cantidad de Registros: " & dt.Rows.Count
                    End If
                End If
            ElseIf GvTraking.Rows(Index).Cells(8).Text = "24" Then
                CentroCostoAcc.Visible = True
                    CentroCostoEq.Visible = True
                    CentroCosto.Visible = True
                    psCodSalida = GvTraking.Rows(Index).Cells(7).Text
                    lblCodSalidaCC.Text = "Nro de Salida " & GvTraking.Rows(Index).Cells(7).Text
                    gridCC.DataSource = obj.Lista_SalidacCCosto(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "", "", "", "")
                    gridCC.DataBind()

                    dtDatos = obj.Lista_DetalleSinSeries_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "2")
                    If dtDatos.Rows.Count > 0 Then
                        grdCCDetAcc.DataSource = dtDatos
                        grdCCDetAcc.DataBind()
                    End If
                    'Label2
                    If dtDatos.Rows.Count > 0 Then
                        lblgrdCCDetAcc.Visible = True
                        grdCCDetAcc.Visible = True
                    Else
                        lblgrdCCDetAcc.Visible = False
                        grdCCDetAcc.Visible = False
                    End If
                    dtDatos = Nothing

                    dtDatos = obj.Lista_Detalle_xSalida(Session("Ruta_Emp"), Session("CodEmpresa"), psCodSalida, "2")
                    grdCCDet.DataSource = dtDatos
                    grdCCDet.DataBind()

                    If dtDatos.Rows.Count > 0 Then
                        lblgrdCCDet.Visible = True
                        grdCCDet.Visible = True
                    Else
                        lblgrdCCDet.Visible = False
                        grdCCDet.Visible = False
                    End If
                    dtDatos = Nothing
                End If
            End If
    End Sub
End Class
