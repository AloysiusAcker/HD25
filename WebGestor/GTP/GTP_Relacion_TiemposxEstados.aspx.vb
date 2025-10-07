Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class GTP_GTP_Relacion_TiemposxEstados
    Inherits System.Web.UI.Page
    Private ObjList As New ClsGtp_Listados
    Private ObjProceso As New ClsGtp_Procesos
    Private plNroEstados As Double = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblError.Text = ""
            lblRegistro.Text = ""
            Me.Page.Session.Timeout = 1080
            Call LlenaComboItem("TBOPC475", DdlEstado)
            DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            chkCliente.Checked = True
            If lblCodEstado.Text <> "" Then
                ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                DdlProceso.Items.Add("< Seleccionar >") : DdlProceso.SelectedValue = "< Seleccionar >"
                If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                DdlProceso_SelectedIndexChanged(sender, e)
            Else
                Call LlenaComboItem("TBOPC473", DdlProceso)
            End If
            DdlEstado.SelectedValue = "1"
            'BtnListar_Click(sender, e)
        End If
    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click

        LblError.Text = ""
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim dRow As DataRow
        dt.Columns.Add("C0") : dt.Columns.Add("C1") : dt.Columns.Add("C2")
        dt.Columns.Add("C3") : dt.Columns.Add("C4") : dt.Columns.Add("C5")
        dt.Columns.Add("C6") : dt.Columns.Add("C7") : dt.Columns.Add("C8")
        dt.Columns.Add("C9")
        dt.Columns.Add("C10") : dt.Columns.Add("C11") : dt.Columns.Add("C12")
        dt.Columns.Add("C13") : dt.Columns.Add("C14") : dt.Columns.Add("C15")
        dt.Columns.Add("C16") : dt.Columns.Add("C17") : dt.Columns.Add("C18")
        dt.Columns.Add("C19") : dt.Columns.Add("C20") : dt.Columns.Add("C21")
        dt.Columns.Add("C22") : dt.Columns.Add("C23") : dt.Columns.Add("C24")
        dt.Columns.Add("C25") : dt.Columns.Add("C26") : dt.Columns.Add("C27")
        dt.Columns.Add("C28") : dt.Columns.Add("C29") : dt.Columns.Add("C30")
        dt.Columns.Add("C31") : dt.Columns.Add("C32") : dt.Columns.Add("C33")
        dt.Columns.Add("C34") : dt.Columns.Add("C35") : dt.Columns.Add("C36")
        dt.Columns.Add("C37") : dt.Columns.Add("C38") : dt.Columns.Add("C39")
        dt.Columns.Add("C40") : dt.Columns.Add("C41")
        dt.Columns.Add("C42") : dt.Columns.Add("C43") : dt.Columns.Add("C44")
        dt.Columns.Add("C45") : dt.Columns.Add("C46") : dt.Columns.Add("C47")
        dt.Columns.Add("C48") : dt.Columns.Add("C49") : dt.Columns.Add("C50")
        dt.Columns.Add("C51") : dt.Columns.Add("C52") : dt.Columns.Add("C53")
        dt.Columns.Add("C54") : dt.Columns.Add("C55") : dt.Columns.Add("C56")
        dt.Columns.Add("C57") : dt.Columns.Add("C58") : dt.Columns.Add("C59")
        dt.Columns.Add("C60") : dt.Columns.Add("C61") : dt.Columns.Add("C62")
        dt.Columns.Add("C63") : dt.Columns.Add("C64") : dt.Columns.Add("C65")
        dt.Columns.Add("C66") : dt.Columns.Add("C67") : dt.Columns.Add("C68")
        dt.Columns.Add("C69") : dt.Columns.Add("C70") : dt.Columns.Add("C71")
        dt.Columns.Add("C72") : dt.Columns.Add("C73")
        dt.Columns.Add("C74") : dt.Columns.Add("C75") : dt.Columns.Add("C76")
        dt.Columns.Add("C77") : dt.Columns.Add("C78") : dt.Columns.Add("C79")
        dt.Columns.Add("C80") : dt.Columns.Add("C81") : dt.Columns.Add("C82")
        dt.Columns.Add("C83") : dt.Columns.Add("C84") : dt.Columns.Add("C85")
        dt.Columns.Add("C86") : dt.Columns.Add("C87") : dt.Columns.Add("C88")
        dt.Columns.Add("C89") : dt.Columns.Add("C90") : dt.Columns.Add("C91")
        dt.Columns.Add("C92") : dt.Columns.Add("C93") : dt.Columns.Add("C94")
        dt.Columns.Add("C95") : dt.Columns.Add("C96") : dt.Columns.Add("C97")
        dt.Columns.Add("C98") : dt.Columns.Add("C99") : dt.Columns.Add("C100")
        dt.Columns.Add("C101") : dt.Columns.Add("C102") : dt.Columns.Add("C103")
        dt.Columns.Add("C104") : dt.Columns.Add("C105")
        dt.Columns.Add("C106")
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open()

        cmdSql.Connection = Cn
        cmdSql.CommandText = " SELECT ELEMEN_VALOR, ELEMEN_CODIGO FROM TBCELEMEN "
        If DdlProceso.SelectedValue <> "< Seleccionar >" Then
            cmdSql.CommandText = cmdSql.CommandText & " inner join BDGEmpresa" & Session("SiglaGrupoEmpresa") & ".dbo.TBTICKET_RELACION_PROCESO_ESTADO on elemen_codigo = estado_codigo  " _
                               & " WHERE ELEMEN_TABLA='TBOPC475' AND ELEMEN_SYS_EST='0' and proceso_codigo = '" & DdlProceso.SelectedValue & "' ORDER BY convert(float,ELEMEN_CODIGO) ASC "
        Else
            cmdSql.CommandText = cmdSql.CommandText & " WHERE ELEMEN_TABLA = 'TBOPC475' AND ELEMEN_SYS_EST ='0' ORDER BY convert(float,ELEMEN_CODIGO) ASC"
        End If
        Rs = cmdSql.ExecuteReader
        plNroEstados = 0
        Dim aa As Integer = 10
        If Rs.HasRows Then
            While Rs.Read
                GwLista.Columns(aa).HeaderText = "Fecha de Inicio " & Nu(Rs("ELEMEN_VALOR"))
                GwLista.Columns(aa).Visible = True
                aa = aa + 1
            End While
        End If
        Rs.Close()
        Rs = cmdSql.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                GwLista.Columns(aa).HeaderText = Nu(Rs("ELEMEN_VALOR"))
                GwLista.Columns(aa).Visible = True
                aa = aa + 1
                GwLista.Columns(aa).HeaderText = Nu(Rs("ELEMEN_CODIGO"))
                GwLista.Columns(aa).HeaderStyle.ForeColor = Drawing.Color.White
                GwLista.Columns(aa).Visible = True
                GwLista.Columns(aa).ItemStyle.ForeColor = Drawing.Color.White
                GwLista.Columns(aa).ItemStyle.Width = 0
                aa = aa + 1
            End While
        End If
        Rs.Close()
        GwLista.Columns(aa).HeaderText = "Duración del Ticket en Minutos"
        GwLista.Columns(aa).Visible = True
        Dim j As Integer = 0
        Dim i As Integer = 0
        Dim pdColum As Integer = 0
        Try
            dtLista = Cargar_BD()
            If dtLista.Rows.Count > 0 Then
                For Each dr As DataRow In dtLista.Rows
                    dRow = dt.NewRow
                    j = j + 1
                    dRow("c0") = j
                    dRow("c1") = Nu(dr("COD_TICKET"))
                    dRow("c2") = Nu(dr!TBTICKET_CLIENTE_CIF)
                    dRow("c3") = Nu(dr!TBTICKET_CLIENTE_NOMBRE)
                    dRow("c4") = Nu(dr!TBTICKET_CLIENTE_COD_GPS)
                    dRow("c5") = Nu(dr!TBTICKET_CLIENTE_GRUPO)
                    dRow("c6") = Nu(dr!Proceso)
                    dRow("c7") = Nu(dr!NOM_PROB)
                    dRow("c8") = Nu(dr!PERSON_ASIG2)
                    dRow("c9") = Nu(dr!pEstado)
                    i = 8
                    For a = 10 To plNroEstados
                        i = i + 1
                        dRow("c" & a) = Nu(dr(i))
                        pdColum = a
                    Next
                    For a = plNroEstados + 10 To aa - 1
                        i = a - 1
                        dRow("c" & a) = Nu(dr(i))
                        i = i + 1
                        dRow("c" & a + 1) = Nu(dr(i))
                        pdColum = a
                    Next
                    dRow("c" & aa) = Nu(dr("TICKET_DURACION"))
                    dt.Rows.Add(dRow)
                Next
            End If
            GwLista.DataSource = dt
            GwLista.DataBind()
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
        Sql = " select * FROM V_GTP_LISTA_TIEMPOS  "
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable(Sql)
        Da.Fill(Dt)
        Me.Page.Session.Timeout = 1080
        Return Dt
    End Function
    Private Sub Crear_Vista_CRM()
        Dim psFechaIni As String = ""
        Dim SqlEstado As String = ""
        If txtFechaIni.Text <> "" Then psFechaIni = Right(txtFechaIni.Text, 4) & Mid(txtFechaIni.Text, 4, 2) & Left(txtFechaIni.Text, 2)
        Dim psFechaFin As String = ""
        If txtFechaFin.Text <> "" Then psFechaFin = Right(txtFechaFin.Text, 4) & Mid(txtFechaFin.Text, 4, 2) & Left(txtFechaFin.Text, 2)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdSql2 As New SqlCommand
        Cn2.Open()
        cmdSql2.Connection = Cn2
        cmdSql2.CommandText = " SELECT ELEMEN_VALOR, ELEMEN_CODIGO FROM TBCELEMEN "
        If DdlProceso.SelectedValue <> "< Seleccionar >" Then
            cmdSql2.CommandText = cmdSql2.CommandText & " inner join BDGEmpresa" & Session("SiglaGrupoEmpresa") & ".dbo.TBTICKET_RELACION_PROCESO_ESTADO on elemen_codigo = estado_codigo  " _
                               & " WHERE ELEMEN_TABLA='TBOPC475' AND ELEMEN_SYS_EST='0' and proceso_codigo = '" & DdlProceso.SelectedValue & "' ORDER BY convert(float,ELEMEN_CODIGO) ASC "
        Else
            cmdSql2.CommandText = cmdSql2.CommandText & " WHERE ELEMEN_TABLA = 'TBOPC475' AND ELEMEN_SYS_EST ='0' ORDER BY convert(float,ELEMEN_CODIGO) ASC"
        End If
        Rs = cmdSql2.ExecuteReader
        plNroEstados = 0
        If Rs.HasRows Then
            While Rs.Read
                plNroEstados = plNroEstados + 1
                If SqlEstado <> "" Then SqlEstado = SqlEstado & ","
                SqlEstado = SqlEstado & " (SELECT TOP 1 substring(FECHA_REGISTRO,7,2)+'/'+substring(FECHA_REGISTRO,5,2)+'/'+substring(FECHA_REGISTRO,1,4) From TBTICKET_TRAKING WHERE APROB_CODIGO = AP.TICKET_CODIGO  AND ESTADO_REGISTRO = '" & Nu(Rs!ELEMEN_CODIGO) & "' ORDER BY REGISTRO_SECUENCIA  ) AS FECHA_ESTADO_" & Nu(Rs!ELEMEN_CODIGO)
            End While
        End If
        Rs.Close()
        Rs = cmdSql2.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                If SqlEstado <> "" Then SqlEstado = SqlEstado & ","
                SqlEstado = SqlEstado & " (SELECT TOP 1 ESTADO_DURACION_LETRAS From TBTICKET_TRAKING WHERE APROB_CODIGO = AP.TICKET_CODIGO  AND ESTADO_REGISTRO = '" & Nu(Rs!ELEMEN_CODIGO) & "' ORDER BY REGISTRO_SECUENCIA DESC) AS DURACION_LETRA_" & Nu(Rs!ELEMEN_CODIGO) & ", (SELECT TOP 1 ESTDAO_DURACION_MINUTOS From TBTICKET_TRAKING WHERE APROB_CODIGO = AP.TICKET_CODIGO  AND ESTADO_REGISTRO = '" & Nu(Rs!ELEMEN_CODIGO) & "' ORDER BY REGISTRO_SECUENCIA DESC) AS DURACION_MIN_" & Nu(Rs!ELEMEN_CODIGO)
            End While
        End If
        Rs.Close()

        Cn.Open()
        cmdSql.Connection = Cn
        cmdSql.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_GTP_LISTA_TIEMPOS]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_GTP_LISTA_TIEMPOS]"
        cmdSql.ExecuteNonQuery()
        cmdSql.CommandText = " CREATE VIEW V_GTP_LISTA_TIEMPOS AS  SELECT Right('0000000' + CONVERT(VARCHAR(10), TICKET_CODIGO), 7) AS COD_TICKET,TBTICKET_CLIENTE_CIF, TBTICKET_CLIENTE_NOMBRE, TBTICKET_CLIENTE_COD_GPS, TBTICKET_CLIENTE_GRUPO, " _
                           & " TBOPC473.ELEMEN_VALOR AS PROCESO, P.NIVEL1_DESCRIP AS NOM_PROB, J.USUARI_APEPAT + ' ' + J.USUARI_APEMAT + ', ' + J.USUARI_NOMBRES AS PERSON_ASIG2, TBOPC475.ELEMEN_VALOR AS PESTADO, "
        cmdSql.CommandText = cmdSql.CommandText & SqlEstado & " , TICKET_DURACION  "
        cmdSql.CommandText = cmdSql.CommandText & " FROM TBTICKET AP INNER JOIN TBTICKET_CLIENTE AS B ON B.TBTICKET_CLIENTE_CODIGO = AP.TICKET_PROVEEDOR AND B.TBTICKET_SYS_EST='0' " _
                           & " INNER JOIN TBESP_GTP1 P ON AP.TICKET_TIPO = NIVEL1_CODIGO And nivel1_sys_est ='0' LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC473 ON TBOPC473.ELEMEN_TABLA = 'TBOPC473' AND TBOPC473.ELEMEN_CODIGO = AP.TICKET_PROCESO " _
                           & " LEFT JOIN BDGRUPOEMPRESAS.DBO.TBCELEMEN AS TBOPC475 ON TBOPC475.ELEMEN_TABLA = 'TBOPC475' AND TBOPC475.ELEMEN_CODIGO = AP.TICKET_ESTADO LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS J ON J.USUARI_CODIGO = AP.TICKET_ASIGNADO_PERSONA " _
                           & " LEFT JOIN BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI AS K ON K.USUARI_CODIGO = B.TBTICKET_CLIENTE_ASIGNADO_A WHERE TICKET_SYS_EST = '0' "
        If DdlComponente.SelectedValue <> "< Seleccionar >" Then
            cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_TIPO = '" & DdlComponente.SelectedValue & "'"
        Else
            cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_TIPO,'')= IsNull(TICKET_TIPO,'') "
        End If
        If DdlEstado.SelectedValue <> "< Todos >" Then
            cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_ESTADO = '" & DdlEstado.SelectedValue & "'"
        Else
            cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_ESTADO,'')= IsNull(TICKET_ESTADO,'') "
        End If
        If DdlProceso.SelectedValue <> "< Seleccionar >" Then
            cmdSql.CommandText = cmdSql.CommandText & " AND TICKET_proceso = '" & DdlProceso.SelectedValue & "'"
        Else
            cmdSql.CommandText = cmdSql.CommandText & " and IsNull(TICKET_PROCESO,'')= IsNull(TICKET_PROCESO,'') "
        End If
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
        If DdlProceso.SelectedValue = "< Seleccionar >" Then
            DdlEstado.Items.Clear()
            Call LlenaComboItem("TBOPC475", DdlEstado)
            DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            DdlEstado.SelectedValue = "1"
            BtnListar_Click(sender, e)
        Else
            DdlEstado.Items.Clear()
            ObjProceso.LLenaComboItemTabEspRelacionProceso(Session("Ruta_Emp"), DdlComponente, "", "", "TBESP_GTP1", DdlProceso.SelectedValue, Session("CodEmpresa"), "1")
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC475", DdlEstado, DdlProceso.SelectedValue, Session("SiglaGrupoEmpresa"), "TBTICKET_RELACION_PROCESO_ESTADO")
            DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
            If DdlEstado.Items.Count > 0 Then
                DdlEstado.SelectedIndex = 0
            End If
        End If
    End Sub
    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        DdlEstado.Items.Clear()
        DdlProceso.Items.Clear()
        DdlComponente.Items.Clear()
        Call LlenaComboItem("TBOPC475", DdlEstado)
        Call LlenaComboItem("TBOPC473", DdlProceso)
        DdlEstado.Items.Add("< Todos >") : DdlEstado.SelectedValue = "< Todos >"
        DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
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
            DdlComponente.Items.Add("< Seleccionar >") : DdlComponente.SelectedValue = "< Seleccionar >"
            If lblCodCliente.Text <> "" Then
                ObjProceso.GTP_LlenaComboItem_Proceso("TBOPC473", DdlProceso, lblCodEstado.Text, Session("SiglaGrupoEmpresa"), "TBTICKET_CLIENTE_RELACION_PROCESO")
                DdlProceso.Items.Add("< Seleccionar >") : DdlProceso.SelectedValue = "< Seleccionar >"
                If DdlProceso.Items.Count > 0 Then DdlProceso.SelectedIndex = 0
                DdlProceso_SelectedIndexChanged(sender, e)
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
    Protected Sub DdlComponente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlComponente.SelectedIndexChanged

    End Sub
End Class