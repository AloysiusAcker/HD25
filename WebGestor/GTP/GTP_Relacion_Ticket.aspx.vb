Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class GTP_GTP_Relacion_Ticket
    Inherits System.Web.UI.Page
    Private ObjList As New ClsGtp_Listados
    Private ObjProceso As New ClsGtp_Procesos
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblError.Text = ""
            lblRegistro.Text = ""
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
                    Exit For
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
            'BtnListar_Click(sender, e)
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
        dt.Columns.Add("C11")
        dt.Columns.Add("C12")
        dt.Columns.Add("C13")
        dt.Columns.Add("C14")
        dt.Columns.Add("C15")
        dt.Columns.Add("C16")
        dt.Columns.Add("C17")
        dt.Columns.Add("C18")
        dt.Columns.Add("C19")
        dt.Columns.Add("C20")
        dt.Columns.Add("C21")
        dt.Columns.Add("C22")
        dt.Columns.Add("C23")
        dt.Columns.Add("C24")
        dt.Columns.Add("C25")
        dt.Columns.Add("C26")
        dt.Columns.Add("C27")
        dt.Columns.Add("C28")
        dt.Columns.Add("C29")
        dt.Columns.Add("C30")
        dt.Columns.Add("C31")
        dt.Columns.Add("C32")
        dt.Columns.Add("C33")
        dt.Columns.Add("C34")
        dt.Columns.Add("C35")
        dt.Columns.Add("C36")
        dt.Columns.Add("C37")
        dt.Columns.Add("C38")
        dt.Columns.Add("C39")
        dt.Columns.Add("C40")
        dt.Columns.Add("C41")
        Try
            dtLista = Cargar_BD()
            If dtLista.Rows.Count > 0 Then
                For Each dr As DataRow In dtLista.Rows
                    dRow = dt.NewRow

                    dRow("c1") = Nu(dr("COD_TICKET"))
                    dRow("c3") = Nu(dr!FECHA_REPORTA)
                    dRow("c4") = Nu(dr!HORA_REPORTA)
                    dRow("c5") = Nu(dr!TBTICKET_CLIENTE_NOMBRE)
                    dRow("c6") = Nu(dr!PERSON_ASIG_CLIENTE)
                    dRow("c7") = Nu(dr!TBTICKET_CLIENTE_GRUPO)
                    dRow("c8") = Nu(dr!ESTADO_CLIENTE)

                    dRow("c9") = Nu(dr!CONTACTO)
                    dRow("c10") = Nu(dr!Proceso)
                    dRow("c11") = Nu(dr!CANAL)
                    dRow("c12") = Nu(dr!NIVEL1_DESCRIP)
                    dRow("c13") = Nu(dr!NOM_PROB1)
                    dRow("c14") = Nu(dr!pEstado)
                    dRow("c15") = Nu(dr!TICKET_MOTIVO)
                    dRow("c16") = Nu(dr!TICKET_DESCRIPCION)
                    dRow("c17") = Nu(dr!TICKET_SOLUCION)
                    dRow("c18") = Nu(dr!FECHA_VISTO)
                    dRow("c19") = Nu(dr!HORA_VISTO)
                    dRow("c20") = Nu(dr!FECHA_ASIGNADO)
                    dRow("c21") = Nu(dr!HORA_ASIGNADO)
                    dRow("c22") = Nu(dr!FECHA_ASIGVISTO)
                    dRow("c23") = Nu(dr!HORA_ASIGVISTO)
                    dRow("c24") = Nu(dr!FECHA_SOLUCION)
                    dRow("c25") = Nu(dr!HORA_SOLUCION)
                    dRow("c26") = Nu(dr!TICKET_ESTADO)
                    dRow("c27") = Nu(dr!TICKET_SYS_EST)
                    dRow("c28") = Nu(dr!TICKET_TIPO)
                    dRow("c29") = Nu(dr!FECHA_ESTADO)
                    dRow("c30") = Nu(dr!HORA_ESTADO)
                    dRow("c32") = Nu(dr!TICKET_ESTADO_FECHA)
                    dRow("c34") = Nu(dr!Duracion_ticket)
                    dRow("c38") = Nu(dr!PERSON_ASIG2)
                    dRow("c39") = Nu(dr!TICKET_ASIGNADO_PERSONA)
                    dRow("c40") = Nu(dr!REGISTRO_OBSERVACION)
                    dRow("c41") = Nu(dr!TBTICKET_CLIENTE_CIF)

                    dt.Rows.Add(dRow)
                Next
            End If
            GwLista.DataSource = dt
            GwLista.DataBind()
            dt.Dispose()
            lblRegistro.Text = "Se encontraron " & GwLista.Rows.Count & " registros"
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
        Sql = " select * FROM V_CRM_LISTA  ORDER BY TICKET_REPORTA_FECHA + TICKET_REPORTA_HORA DESC "
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
        cmdSql.CommandText = " CREATE VIEW V_CRM_LISTA AS  Select  Right('0000000' + CONVERT(VARCHAR(10), TICKET_CODIGO), 7) AS COD_TICKET,'' AS MARCA, SUBSTRING(TICKET_REPORTA_FECHA,7,2)+'/'+SUBSTRING(TICKET_REPORTA_FECHA,5,2)+'/'+SUBSTRING(TICKET_REPORTA_FECHA,1,4) AS FECHA_REPORTA, SUBSTRING(TICKET_REPORTA_HORA,1,2)+':'+SUBSTRING(TICKET_REPORTA_HORA,3,2)+':'+SUBSTRING(TICKET_REPORTA_HORA,5,2) AS HORA_REPORTA, " _
                              & " TBTICKET_CLIENTE_NOMBRE, K.USUARI_APEPAT + ' ' + K.USUARI_APEMAT + ', ' + K.USUARI_NOMBRES AS PERSON_ASIG_CLIENTE, TBTICKET_CLIENTE_GRUPO, TBOPC480.ELEMEN_VALOR AS ESTADO_CLIENTE, TBTICKET_CONTACTO_APEPAT+' '+TBTICKET_CONTACTO_APEMAT+' '+TBTICKET_CONTACTO_NOMBRES AS CONTACTO, TBOPC473.ELEMEN_VALOR AS PROCESO, TBOPC474.ELEMEN_VALOR AS CANAL  , " _
                              & " P.NIVEL1_DESCRIP, H.NIVEL2_DESCRIP+', '+ I.NIVEL3_DESCRIP AS NOM_PROB1, TBOPC475.ELEMEN_VALOR AS PESTADO, TICKET_MOTIVO, TICKET_DESCRIPCION, TICKET_SOLUCION, SUBSTRING(TICKET_VISTO_FECHA,7,2)+'/'+SUBSTRING(TICKET_VISTO_FECHA,5,2)+'/'+SUBSTRING(TICKET_VISTO_FECHA,1,4) AS FECHA_VISTO, " _
                              & " SUBSTRING(TICKET_VISTO_HORA,1,2)+':'+SUBSTRING(TICKET_VISTO_HORA,3,2)+':'+SUBSTRING(TICKET_VISTO_HORA,5,2) AS HORA_VISTO, SUBSTRING(TICKET_ASIGNADO_FECHA,7,2)+'/'+SUBSTRING(TICKET_ASIGNADO_FECHA,5,2)+'/'+SUBSTRING(TICKET_ASIGNADO_FECHA,1,4) AS FECHA_ASIGNADO, " _
                              & " SUBSTRING(TICKET_ASIGNADO_HORA,1,2)+':'+SUBSTRING(TICKET_ASIGNADO_HORA,3,2)+':'+SUBSTRING(TICKET_ASIGNADO_HORA,5,2) AS HORA_ASIGNADO, SUBSTRING(TICKET_ASIGVISTO_FECHA,7,2)+'/'+SUBSTRING(TICKET_ASIGVISTO_FECHA,5,2)+'/'+SUBSTRING(TICKET_ASIGVISTO_FECHA,1,4) AS FECHA_ASIGVISTO, " _
                              & " SUBSTRING(TICKET_ASIGVISTO_HORA,1,2)+':'+SUBSTRING(TICKET_ASIGVISTO_HORA,3,2)+':'+SUBSTRING(TICKET_ASIGVISTO_HORA,5,2) AS HORA_ASIGVISTO, SUBSTRING(TICKET_SOLUCION_FECHA,7,2)+'/'+SUBSTRING(TICKET_SOLUCION_FECHA,5,2)+'/'+SUBSTRING(TICKET_SOLUCION_FECHA,1,4) AS FECHA_SOLUCION, " _
                              & " SUBSTRING(TICKET_SOLUCION_HORA,1,2)+':'+SUBSTRING(TICKET_SOLUCION_HORA,3,2)+':'+SUBSTRING(TICKET_SOLUCION_HORA,5,2) AS HORA_SOLUCION, TICKET_ESTADO, TICKET_SYS_EST, TICKET_TIPO, SUBSTRING(TICKET_ESTADO_FECHA,7,2)+'/'+SUBSTRING(TICKET_ESTADO_FECHA,5,2)+'/'+SUBSTRING(TICKET_ESTADO_FECHA,1,4) AS FECHA_ESTADO, " _
                              & " SUBSTRING(TICKET_ESTADO_HORA,1,2)+':'+SUBSTRING(TICKET_ESTADO_HORA,3,2) AS HORA_ESTADO,'' AS CAMPO2, TICKET_ESTADO_FECHA, '' AS CAMPO3, CASE  ISNULL(TICKET_DURACION,'') WHEN '' THEN '' ELSE ISNULL(TICKET_DURACION,'') + 'Minutos' end Duracion_ticket,'' AS CAMPO4,'' AS CAMPO5,'' AS CAMPO6, J.USUARI_APEPAT + ' ' + J.USUARI_APEMAT + ', ' + J.USUARI_NOMBRES AS PERSON_ASIG2, " _
                              & " TICKET_ASIGNADO_PERSONA, D.REGISTRO_OBSERVACION, TBTICKET_CLIENTE_CIF, TICKET_ESTADO_HORA, TICKET_DURACION_REAL,TICKET_REPORTA_FECHA ,TICKET_REPORTA_HORA " _
                              & " From TBTICKET AP INNER JOIN TBESP_GTP1 P ON AP.TICKET_TIPO = NIVEL1_CODIGO and nivel1_sys_est ='0' INNER JOIN TBTICKET_CLIENTE AS B ON B.TBTICKET_CLIENTE_CODIGO = AP.TICKET_PROVEEDOR  " _
                              & " AND B.TBTICKET_SYS_EST='0' INNER JOIN TBTICKET_CLIENTE_CONTACTO AS C ON C.TBTICKET_CLIENTE_CODIGO = B.TBTICKET_CLIENTE_CODIGO AND C.TBTICKET_CONTACTO_CODIGO = AP.TICKET_CONTACTO " _
                              & " AND TBTICKET_CONTACTO_SYS_EST ='0' LEFT JOIN TBTICKET_TRAKING AS D ON D.APROB_CODIGO = AP.TICKET_CODIGO AND D.FECHA_REGISTRO = AP.TICKET_ESTADO_FECHA AND D.ESTADO_REGISTRO = AP.TICKET_ESTADO " _
                              & " AND D.HORA_REGISTRO = AP.TICKET_ESTADO_HORA LEFT JOIN BDGRUPOEMPRESAS.DBO.TBPERSONAL AS E ON E.PERSON_CODIGO = AP.TICKET_REPORTA_USUARIO " _
                              & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBPERSONAL AS F ON F.PERSON_CODIGO = AP.TICKET_ASIGNADO_PERSONA LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS G ON G.USUARI_CODIGO = AP.TICKET_REPORTA_USUARIO AND LEFT(TICKET_REPORTA_USUARIO,4)='1111' " _
                              & " LEFT JOIN TBESP_GTP2 AS H ON H.NIVEL2_CODIGO = AP.TICKET_PROBLEMA1 LEFT JOIN TBESP_GTP3 AS I ON I.NIVEL3_CODIGO = AP.TICKET_PROBLEMA2 " _
                              & " LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS J ON J.USUARI_CODIGO = AP.TICKET_ASIGNADO_PERSONA LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS K ON K.USUARI_CODIGO = B.TBTICKET_CLIENTE_ASIGNADO_A " _
                              & " LEFT JOIN TBESP_GTP1 AS L ON L.NIVEL1_CODIGO = AP.TICKET_TIPO_ORIG and L.nivel1_sys_est ='0'  LEFT JOIN TBESP_GTP2 AS M ON M.NIVEL2_CODIGO = AP.TICKET_PROBLEMA1_ORIG " _
                              & " LEFT JOIN TBESP_GTP3 AS N ON N.NIVEL3_CODIGO = AP.TICKET_PROBLEMA2_ORIG LEFT JOIN BDGrupoEmpresas.dbo.TBCELEMEN AS TBOPC480 ON TBOPC480.ELEMEN_TABLA = 'TBOPC480' AND TBOPC480.ELEMEN_CODIGO = B.TBTICKET_CLIENTE_ESTADO " _
                              & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC475 ON TBOPC475.ELEMEN_TABLA = 'TBOPC475' AND TBOPC475.ELEMEN_CODIGO = AP.TICKET_ESTADO " _
                              & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC469 ON TBOPC469.ELEMEN_TABLA = 'TBOPC469' AND TBOPC469.ELEMEN_CODIGO = AP.TICKET_REPORTA_USUARIO_CONFORMIDAD " _
                              & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC473 ON TBOPC473.ELEMEN_TABLA = 'TBOPC473' AND TBOPC473.ELEMEN_CODIGO = AP.TICKET_PROCESO " _
                              & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC474 ON TBOPC474.ELEMEN_TABLA = 'TBOPC474' AND TBOPC474.ELEMEN_CODIGO = AP.TICKET_CANAL " _
                              & " Where Not (TICKET_TIPO Is Null) "
        If DdlComponente.SelectedValue <> "< Seleccionar >" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_TIPO = '" & DdlComponente.SelectedValue & "'"
        If DdlProceso.SelectedValue <> "< Seleccionar >" Then
            cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_proceso = '" & DdlProceso.SelectedValue & "'"
        Else
            cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_PROCESO,'')= IsNull(TICKET_PROCESO,'') "
        End If
        If DdlElemento.SelectedValue <> "< Seleccionar >" Then
            cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_PROBLEMA1 = '" & DdlElemento.SelectedValue & "'"
        Else
            cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_PROBLEMA1,'')= IsNull(TICKET_PROBLEMA1,'') "
        End If
        If DdlEstado.SelectedValue <> "< Todos >" Then
            cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_ESTADO = '" & DdlEstado.SelectedValue & "'"
        Else
            cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_ESTADO,'')= IsNull(TICKET_ESTADO,'') "
        End If
        If chkAnulados.Checked = False Then cmdSql.CommandText = cmdSql.CommandText & " AND (TICKET_SYS_EST='0') "
        If chkAnulados.Checked = True Then cmdSql.CommandText = cmdSql.CommandText & " AND (TICKET_SYS_EST='1') "

        If lblCodCliente.Text <> "" Then
            cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_PROVEEDOR = " & lblCodCliente.Text
        Else
            cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_PROVEEDOR,'')= IsNull(TICKET_PROVEEDOR,'') "
        End If

        cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_ASIGNADO_PERSONA,'')= IsNull(TICKET_ASIGNADO_PERSONA,'') and IsNull(TICKET_REPORTA_USUARIO,'')= IsNull(TICKET_REPORTA_USUARIO,'') "
        If psFechaIni <> "" And psFechaFin = "" Then cmdSql.CommandText = cmdSql.CommandText & " And TICKET_REPORTA_FECHA = '" & psFechaIni & "'"
        If psFechaIni <> "" And psFechaFin <> "" Then cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_REPORTA_FECHA BETWEEN '" & psFechaIni & "' AND '" & psFechaFin & "'"
        cmdSql.ExecuteNonQuery()

    End Sub

    Protected Sub GwLista_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GwLista.SelectedIndexChanged

    End Sub
    Protected Sub btnListarTI_Click(sender As Object, e As EventArgs) Handles btnListarTI.Click
        LblError.Text = ""
        Dim ObjCont As New clsCont_Listados
        Try
            Dim obj As New clsInv_Listados
            FlexTI.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            FlexTI.DataSource = ObjList.GTP_Lista_BusClientes(Session("Ruta_Emp"), txtBusRazon.Text, txtBusRuc.Text)
            FlexTI.DataBind()
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexTI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FlexTI.SelectedIndexChanged

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
            ModalPopupExtender2.Hide()
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
    Protected Sub txtRuc_TextChanged(sender As Object, e As EventArgs) Handles txtRuc.TextChanged
        Call Cargar_DatosCliente(sender, e)
        BtnListar_Click(sender, e)
    End Sub
    Protected Sub chkCliente_CheckedChanged(sender As Object, e As EventArgs) Handles chkCliente.CheckedChanged
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
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        If e.CommandName = "Accion" Then
            LblError.Text = ""
            Dim dRow As DataRow
            dt.Columns.Add("C1")
            dt.Columns.Add("C2")
            dt.Columns.Add("C3")
            dt.Columns.Add("C4")
            dt.Columns.Add("C5")
            dt.Columns.Add("C6")
            dt.Columns.Add("C7")
            dt.Columns.Add("C8")
            Try
                dtLista = ObjList.GTP_Lista_AccionesxTicket(Session("CodEmpresa"), Session("Ruta_Emp"), GwLista.Rows(Index).Cells(1).Text)
                If dtLista.Rows.Count > 0 Then
                    For Each dr As DataRow In dtLista.Rows
                        dRow = dt.NewRow
                        dRow("c2") = Formato_Digito(Nu(dr("ACCION_SECUENCIA")), 3)
                        dRow("c1") = Formato_Digito(Nu(dr("TICKET_CODIGO")), 5)
                        dRow("c3") = Nu(dr("ACCION"))
                        dRow("c4") = FormatoFecha(Nu(dr("ACCION_FECHA")))
                        dRow("C5") = FormatoHora(Nu(dr("ACCION_HORA")))
                        dRow("C6") = Nu(dr("USUARIO"))
                        dRow("C7") = Nu(dr("ETIQUETA_REFERENCIA"))
                        dRow("C8") = Formato_Digito(Nu(dr("COD_REFERENCIA")), 3)
                        dt.Rows.Add(dRow)
                    Next
                End If

                GvAcciones.DataSource = dt
                GvAcciones.DataBind()
                lblRegistro.Text = "Se encontraron " & GwLista.Rows.Count & " registros"

            Catch Ex As SqlException
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
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

    'Private Sub GwLista_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GwLista.PageIndexChanging
    '    LblError.Text = ""
    '    GwLista.PageIndex = e.NewPageIndex
    '    BtnListar_Click(sender, e)
    'End Sub

End Class
