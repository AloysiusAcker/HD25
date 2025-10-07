Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data '981030741
Partial Class AdminProblemas_Creacion_Edicion
    Inherits System.Web.UI.Page
#Region " Código generado por el Diseñador de Web Forms "

    'El Diseñador de Web Forms requiere esta llamada.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTA: el Diseñador de Web Forms necesita la siguiente declaración del marcador de posición.
    'No se debe eliminar o mover.

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: el Diseñador de Web Forms requiere esta llamada de método
        'No la modifique con el editor de código.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            Page.Title = "Mesa de Ayuda - Editar Fecha"
            If User.Identity.Name = "" Then
                Session.Clear()
                FormsAuthentication.SignOut()
                Response.Redirect("TerminaSesion.aspx")
                Exit Sub
            End If
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha.Width = 570
            Ficha_ActiveTabChanged(sender, e)
            lblUsuarioCodigo.Text = User.Identity.Name
            Session("ParamPage") = "AP"
            txtFechaIni.Text = FormatoFecha(FechaActual())
            txtFechaFin.Text = txtFechaIni.Text
            lblHabilita.Text = Session("VHab")
            Dim fun As New clsMesaAyuda
            lblProbError.Text = ""
            fun.MATipos_Criterio("1", cboTipoProb, Session("CodEmpresa"), Session("Ruta_Emp"))
            cboTipoProb.Items.Add("< Seleccionar >") : cboTipoProb.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Sub OpcionesFlex(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        '
    End Sub
    Private Sub Llenar_Grilla_TA()
        Try
            Dim dtListado As New DataTable
            Dim obj As New clsMesaAyuda
            Dim i As Integer
            Dim pCodigo As Double
            Dim pdCodProb As Double = txtProb2.Text.Trim
            Dim Fila As GridViewRow
            dtListado = obj.MALista_Archivo_xProblema(pdCodProb, Session("CodEmpresa"), Session("Ruta_Emp"))
            FlexTA.DataSource = dtListado
            FlexTA.DataBind()
            dtListado = Nothing
            For i = 0 To FlexTA.Rows.Count - 1
                pCodigo = FlexTA.Rows(i).Cells(7).Text.Trim
                dtListado = obj.MALista_XTemaAyuda(pCodigo, Session("CodEmpresa"), Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = FlexTA.Rows(i)
                        'FlexTA.Rows(i).Cells(11).Text = Nu(drMenuItem("TEMA_NOMBRE_DOC")).Length
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Archivos/" & Nu(drMenuItem("TEMA_NOMBRE_DOC")) & "'TARGET='_blank'>" & Nu(drMenuItem("TEMA_NOMBRE_DOC")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch Ex As SqlException
            lblErrorAcc.Visible = True
            lblErrorAcc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorAcc.Visible = True
            lblErrorAcc.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Listar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Listar.Click
        Dim psFecIni As String = Right(txtFechaIni.Text, 4) + Mid(txtFechaIni.Text, 4, 2) + Left(txtFechaIni.Text, 2)
        Dim psFecFin As String = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        lblMensaje2.Text = ""
        If txtFechaIni.Text = "" And txtFechaFin.Text <> "" Then lblMensaje.Text = "Debe ingresar la fecha de inicio." : Exit Sub
        If psFecIni > psFecFin Then lblMensaje.Text = "La fecha inicio debe ser menor a la fecha fin." : Exit Sub
        fraFlex2.Visible = False
        Flex2.Visible = False
        lblMensaje.ForeColor = Drawing.Color.Red
        lblMensaje.Text = ""
        lblProbError.Text = ""
        FlexProb.DataSource = Carga_Problemas()
        FlexProb.DataBind()
        lblMensaje.Text = "Se encontrarón " & FlexProb.Rows.Count & " registros."
        lblMensaje.ForeColor = Drawing.Color.Maroon
    End Sub
    Private Function Carga_Problemas() As ICollection
        Dim SqlEst As String = ""
        Carga_Problemas = Nothing
        If chk0.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='0'"
        End If
        If chk1.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='1'"
        End If
        If chk2.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='2'"
        End If
        If chk3.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='3'"
        End If
        If chk4.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='4'"
        End If
        If chk5.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='5'"
        End If
        If SqlEst = "" Then lblMensaje.Text = "No hay estado de problema marcado, favor de hacerlo para poder listar." : Exit Function
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim bolError As Boolean
        Dim i As Integer
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim FechaIni As String = ""
        Dim FechaFin As String = ""
        FechaIni = Right(txtFechaIni.Text, 4) & Mid(txtFechaIni.Text, 4, 2) & Left(txtFechaIni.Text, 2)
        FechaFin = Right(txtFechaFin.Text, 4) & Mid(txtFechaFin.Text, 4, 2) & Left(txtFechaFin.Text, 2)
        dt.Columns.Add("c1", GetType(String))
        dt.Columns.Add("c2", GetType(String))
        dt.Columns.Add("c3", GetType(String))
        dt.Columns.Add("c4", GetType(String))
        dt.Columns.Add("c5", GetType(String))
        dt.Columns.Add("c6", GetType(String))
        dt.Columns.Add("c7", GetType(String))
        dt.Columns.Add("c8", GetType(String))
        dt.Columns.Add("c9", GetType(String))
        dt.Columns.Add("c10", GetType(String))
        dt.Columns.Add("c11", GetType(String))
        dt.Columns.Add("c12", GetType(String))
        dt.Columns.Add("c13", GetType(String))
        dt.Columns.Add("c14", GetType(String))
        dt.Columns.Add("c15", GetType(String))
        dt.Columns.Add("c16", GetType(String))
        dt.Columns.Add("c17", GetType(String))
        dt.Columns.Add("c18", GetType(String))
        Try
            Cn.Open()
            Dim Sql As String = "SELECT APROB_USUARIO_REPORTA,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From BDGRUPOEMPRESAS.dbo.TBPERSONAL WHERE PERSON_CODIGO = APROB_USUARIO_REPORTA) AS PERSONAL1," _
                              & "(SELECT ADMCRI_DESCRIPCION From TBADMIN_CRITERIOS WHERE ADMCRI_CODIGO = AP.APROB_TIPO2 AND ADMCRI_TIPO = '1' AND ADMCRI_SYS_EST='0' AND EMPRESA_CODIGO=AP.EMPRESA_CODIGO) AS TIPO_PROB, " _
                              & "APROB_TIPO,NIVEL1_DESCRIP,P.COLOR_CODIGO,P.COLOR_ROJO,P.COLOR_VERDE,P.COLOR_AZUL,APROB_PROBLEMA1,(SELECT NIVEL2_DESCRIP FROM TBESP_PRO2 WHERE NIVEL2_CODIGO=APROB_PROBLEMA1) AS NOM_PROB1, " _
                              & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE USUARI_CODIGO = APROB_USUARIO_REPORTA AND LEFT(APROB_USUARIO_REPORTA,4)='1111') AS PERSONAL2, APROB_CODIGO, APROB_PRIORIDAD," _
                              & "(SELECT APERSONA_APELLIDOS + ',' + APERSONA_NOMBRE From TBADMIN_PERSONA WHERE APERSONA_USUARIO = APROB_USUARIO_REPORTA ) AS PERSONAL3, " _
                              & "APROB_PROBLEMA2,(SELECT NIVEL3_DESCRIP FROM TBESP_PRO3 X WHERE NIVEL3_CODIGO=APROB_PROBLEMA2) AS NOM_PROB2 , APROB_PROBLEMA_DESCRIPCION," _
                              & "APROB_FECHA_REPORTA, APROB_HORA_REPORTA,APROB_ESTADO,APROB_SYS_EST,APROB_FECHA_SOLUCION,APROB_HORA_SOLUCION, " _
                              & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_ESTADO AND ELEMEN_TABLA='TBOPC185') AS PESTADO,APROB_FECHA_VISTO, APROB_HORA_VISTO, " _
                              & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_CONFORMIDAD_USUARIOREP AND ELEMEN_TABLA='TBOPC057') AS ECONFORME " _
                              & "From TBADMIN_PROBLEMAS AP INNER JOIN TBESP_PRO1 P ON NIVEL1_CODIGO=APROB_TIPO WHERE NOT(APROB_TIPO IS NULL) AND (AP.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            If txtBusCodProb.Text.Trim <> "" Then Sql = Sql & " AND APROB_CODIGO = " & txtBusCodProb.Text.Trim & ""
            If cboTipoProb.SelectedValue <> "< Seleccionar >" Then Sql = Sql & " AND APROB_TIPO2 = '" & cboTipoProb.SelectedValue.Trim & "'"
            If FechaIni <> "" Then
                If FechaIni <> "" And FechaFin <> "" Then
                    Sql = Sql & " AND APROB_FECHA_REPORTA between '" & FechaIni & "' and '" & FechaFin & "' "
                ElseIf FechaIni <> "" Then
                    Sql = Sql & " AND APROB_FECHA_REPORTA = '" & FechaIni & "'"
                End If
            End If
            Sql = Sql & " AND (" & SqlEst & ")"
            If lblHabilita.Text = "V" Then Sql = Sql & " AND (APROB_USUARIO_REPORTA='" & lblUsuarioCodigo.Text & "')"
            Sql = Sql & " AND (APROB_SYS_EST='0') "
            Sql = Sql & " ORDER BY APROB_FECHA_REPORTA+APROB_HORA_REPORTA DESC"
            Dim cmdSql As New SqlCommand(Sql, Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    dr = dt.NewRow()
                    dr("c1") = i.ToString
                    dr("c2") = Format(Nz(Rs!APROB_CODIGO), "00000")
                    dr("c3") = Right(Nu(Rs!APROB_FECHA_REPORTA), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_REPORTA), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_REPORTA), 4)
                    dr("c4") = Left(Nu(Rs!APROB_HORA_REPORTA), 2) + ":" + Right(Nu(Rs!APROB_HORA_REPORTA), 2)
                    dr("c5") = Nu(Rs!TIPO_PROB)
                    dr("c6") = IIf(Nu(Rs!APROB_FECHA_VISTO) = "", "", Right(Nu(Rs!APROB_FECHA_VISTO), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_VISTO), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_VISTO), 4))
                    dr("c7") = IIf(Nu(Rs!APROB_HORA_VISTO) = "", "", Left(Nu(Rs!APROB_HORA_VISTO), 2) + ":" + Right(Nu(Rs!APROB_HORA_VISTO), 2))
                    dr("c8") = IIf(Nu(Rs!APROB_FECHA_SOLUCION) = "", "", Right(Nu(Rs!APROB_FECHA_SOLUCION), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_SOLUCION), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_SOLUCION), 4))
                    dr("c9") = IIf(Nu(Rs!APROB_HORA_SOLUCION) = "", "", Left(Nu(Rs!APROB_HORA_SOLUCION), 2) + ":" + Right(Nu(Rs!APROB_HORA_SOLUCION), 2))
                    dr("c10") = Nu(Rs!PESTADO)
                    dr("c11") = Nu(Rs!APROB_PRIORIDAD)
                    dr("c12") = Nu(Rs!NIVEL1_DESCRIP)
                    dr("c17") = Nu(Rs!NOM_PROB1)
                    dr("c18") = Nu(Rs!NOM_PROB2)
                    dr("c13") = Nu(Rs!APROB_PROBLEMA_DESCRIPCION)
                    If Nu(Rs!PERSONAL1) = "" Then dr("c14") = Nu(Rs!PERSONAL2)
                    If Nu(Rs!PERSONAL2) = "" Then dr("c14") = Nu(Rs!PERSONAL1)
                    If Nu(Rs!PERSONAL1) = "" And Nu(Rs!PERSONAL2) = "" Then dr("c14") = Nu(Rs!PERSONAL3)
                    dr("c15") = Nu(Rs!APROB_ESTADO)
                    dr("c16") = Nu(Rs!APROB_SYS_EST)
                    dt.Rows.Add(dr)
                End While
                Carga_Problemas = New DataView(dt)
            Else
                bolError = True
            End If
        Catch Ex As SqlException
            lblProbError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblProbError.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        If bolError = True Then lblMensaje.Text = "No se encontraron problemas."
    End Function
    Private Sub Nuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nuevo.Click
        Response.Redirect("AdminProblemas_Registrar.aspx")
    End Sub
    Private Sub Cargar_Acciones()
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim bolError As Boolean
        Dim i As Integer
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add("c1", GetType(String))
        dt.Columns.Add("c2", GetType(String))
        dt.Columns.Add("c3", GetType(String))
        dt.Columns.Add("c4", GetType(String))
        dt.Columns.Add("c5", GetType(String))
        dt.Columns.Add("c6", GetType(String))
        dt.Columns.Add("c7", GetType(String))
        dt.Columns.Add("c8", GetType(String))
        Try
            Dim obj As New clsMesaAyuda
            Cn.Open()
            Dim Sql As String = "SELECT DPROB_SECUENCIA, DPROB_ACCION,(SELECT ITEM_VALOR From TBADMIN_PROBLEMAS_ITEMS I WHERE I.EMPRESA_CODIGO=PD.EMPRESA_CODIGO AND I.ITEM_CODIGO = PD.DPROB_ACCION) AS XACCION," _
                                & "DPROB_ACCION_DESCRIPCION, DPROB_OBSERVACION,(SELECT ITEM_VALOR From TBADMIN_PROBLEMAS_ITEMS I WHERE I.EMPRESA_CODIGO=PD.EMPRESA_CODIGO AND I.ITEM_CODIGO = PD.DPROB_OBSERVACION) AS XObserva," _
                                & "DPROB_OBSERVACION_DESCRIPCION,DPROB_USUARIO_ACCION,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From BDGRUPOEMPRESAS.DBO.TBPERSONAL  WHERE PERSON_CODIGO = DPROB_USUARIO_ACCION) AS PERSONAL1," _
                                & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI U WHERE USUARI_CODIGO = DPROB_USUARIO_ACCION) AS PERSONAL2," _
                                & " DPROB_FECHA_ACCION,DPROB_HORA_ACCION,DPROB_SECUENCIA_ESTADO FROM TBADMIN_PROBLEMAS_DETALLE PD WHERE (DPROB_CODIGO = " & txtBusCodProb.Text.Trim & ") AND (DPROB_SYS_EST = '0') AND (PD.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            Sql = Sql & " AND DPROB_SECUENCIA_ESTADO='0'"
            Sql = Sql & " ORDER BY DPROB_SECUENCIA"
            Dim cmdSql As New SqlCommand(Sql, Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                fraFlex2.Visible = True
                Flex2.Visible = True
                While Rs.Read
                    i = i + 1
                    dr = dt.NewRow()
                    dr(0) = Format(Nz(Rs!DPROB_SECUENCIA), "000")
                    dr(1) = Right(Nu(Rs!DPROB_FECHA_ACCION), 2) + " " + Nombre_Mes(Mid(Nu(Rs!DPROB_FECHA_ACCION), 5, 2), False) & " " & Left(Nu(Rs!DPROB_FECHA_ACCION), 4)
                    dr(2) = Left(Nu(Rs!DPROB_HORA_ACCION), 2) + ":" + Right(Nu(Rs!DPROB_HORA_ACCION), 2)
                    dr(3) = Nu(Rs!XAccion)
                    dr(4) = Nu(Rs!DPROB_ACCION_DESCRIPCION)
                    dr(5) = Nu(Rs!XObserva)
                    dr(6) = Nu(Rs!DPROB_OBSERVACION_DESCRIPCION)
                    dr(7) = IIf(Nu(Rs!PERSONAL1) = "", Nu(Rs!PERSONAL2), Nu(Rs!PERSONAL1))
                    dt.Rows.Add(dr)
                End While
                Flex2.DataSource = New DataView(dt)
                Flex2.DataBind()
                lblMensaje2.Text = "Acciones tomadas del Problema Nº " & Flex2.Items.Count
            Else
                bolError = True
            End If
            Cn.Close()
        Catch Ex As SqlException
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
        If bolError = True Then lblMensaje2.Text = "No se encontraron Acciones."
    End Sub
    Protected Sub FlexProb_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexProb.RowCommand
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim bolError As Boolean
        Dim i As Integer
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblMensaje2.Text = ""
        lblMensaje2.ForeColor = Drawing.Color.Black
        fraFlex2.Visible = False
        Flex2.Visible = False
        Dim dt As New DataTable
        Dim dr As DataRow
        dt.Columns.Add("c1", GetType(String))
        dt.Columns.Add("c2", GetType(String))
        dt.Columns.Add("c3", GetType(String))
        dt.Columns.Add("c4", GetType(String))
        dt.Columns.Add("c5", GetType(String))
        dt.Columns.Add("c6", GetType(String))
        dt.Columns.Add("c7", GetType(String))
        dt.Columns.Add("c8", GetType(String))
        lblProbError.Text = ""
        Try
            Dim obj As New clsMesaAyuda
            Dim pdCodProb As Double = FlexProb.Rows(Index).Cells(4).Text
            If e.CommandName = "Acciones" Then
                Cn.Open()
                Dim Sql As String = "SELECT DPROB_SECUENCIA, DPROB_ACCION,(SELECT ITEM_VALOR From TBADMIN_PROBLEMAS_ITEMS I WHERE I.EMPRESA_CODIGO=PD.EMPRESA_CODIGO AND I.ITEM_CODIGO = PD.DPROB_ACCION) AS XACCION," _
                                    & "DPROB_ACCION_DESCRIPCION, DPROB_OBSERVACION,(SELECT ITEM_VALOR From TBADMIN_PROBLEMAS_ITEMS I WHERE I.EMPRESA_CODIGO=PD.EMPRESA_CODIGO AND I.ITEM_CODIGO = PD.DPROB_OBSERVACION) AS XObserva," _
                                    & "DPROB_OBSERVACION_DESCRIPCION,DPROB_USUARIO_ACCION,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From BDGRUPOEMPRESAS.DBO.TBPERSONAL  WHERE PERSON_CODIGO = DPROB_USUARIO_ACCION) AS PERSONAL1," _
                                    & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI U WHERE USUARI_CODIGO = DPROB_USUARIO_ACCION) AS PERSONAL2," _
                                    & " DPROB_FECHA_ACCION,DPROB_HORA_ACCION,DPROB_SECUENCIA_ESTADO FROM TBADMIN_PROBLEMAS_DETALLE PD WHERE (DPROB_CODIGO = " & FlexProb.Rows(Index).Cells(4).Text & ") AND (DPROB_SYS_EST = '0') AND (PD.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                Sql = Sql & " AND DPROB_SECUENCIA_ESTADO='0'"
                Sql = Sql & " ORDER BY DPROB_SECUENCIA"
                Dim cmdSql As New SqlCommand(Sql, Cn)
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    fraFlex2.Visible = True
                    Flex2.Visible = True
                    While Rs.Read
                        i = i + 1
                        dr = dt.NewRow()
                        dr(0) = Format(Nz(Rs!DPROB_SECUENCIA), "000")
                        dr(1) = Right(Nu(Rs!DPROB_FECHA_ACCION), 2) + " " + Nombre_Mes(Mid(Nu(Rs!DPROB_FECHA_ACCION), 5, 2), False) & " " & Left(Nu(Rs!DPROB_FECHA_ACCION), 4)
                        dr(2) = Left(Nu(Rs!DPROB_HORA_ACCION), 2) + ":" + Right(Nu(Rs!DPROB_HORA_ACCION), 2)
                        dr(3) = Nu(Rs!XAccion)
                        dr(4) = Nu(Rs!DPROB_ACCION_DESCRIPCION)
                        dr(5) = Nu(Rs!XObserva)
                        dr(6) = Nu(Rs!DPROB_OBSERVACION_DESCRIPCION)
                        dr(7) = IIf(Nu(Rs!PERSONAL1) = "", Nu(Rs!PERSONAL2), Nu(Rs!PERSONAL1))
                        dt.Rows.Add(dr)
                    End While
                    Flex2.DataSource = New DataView(dt)
                    Flex2.DataBind()
                    lblMensaje2.Text = "Acciones tomadas del Problema Nº " & Flex2.Items.Count
                Else
                    bolError = True
                End If
                Cn.Close()
            End If
            If e.CommandName = "Asignar" Then
                If FlexProb.Rows(Index).Cells(20).Text = "1" Then lblMensaje2.Text = "No puede Asignar el problema se encuentra Anulado." : Exit Sub
                If FlexProb.Rows(Index).Cells(19).Text = "0" Then lblMensaje2.Text = "El problema esta Cerrado." : Exit Sub
                If FlexProb.Rows(Index).Cells(19).Text = "3" Then lblMensaje2.Text = "El problema esta asignado." : Exit Sub
                If FlexProb.Rows(Index).Cells(19).Text = "4" Then lblMensaje2.Text = "El problema esta asignado visto." : Exit Sub
                If FlexProb.Rows(Index).Cells(19).Text = "5" Then lblMensaje2.Text = "El problema esta asignado con acción." : Exit Sub
                If FlexProb.Rows(Index).Cells(19).Text = "6" Then lblMensaje2.Text = "El problema esta reabierto." : Exit Sub
                Call Carga_Datos(pdCodProb)
                Call Carga_Personal(cboPersonal)
                txtFecV1.Text = FormatoFecha(FechaActual)
                txtHoraV1.Text = FormatoHora(HoraActual)
                txtFecA1.Text = FormatoFecha(FechaActual)
                txtHoraA1.Text = FormatoHora(HoraActual)
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
                Ficha.Width = 570
            End If
            If e.CommandName = "Aplicar" Then
                If FlexProb.Rows(Index).Cells(19).Text = "0" Then lblMensaje2.Text = "El problema esta Cerrado." : Exit Sub
                If FlexProb.Rows(Index).Cells(19).Text = "1" Then lblMensaje2.Text = "No tiene persona asignada." : Exit Sub
                If FlexProb.Rows(Index).Cells(19).Text = "2" Then lblMensaje2.Text = "No tiene persona asignada." : Exit Sub
                lblErrorAcc.Text = ""
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
                Ficha.Width = 570
                Call Carga_Datos(pdCodProb)
                Call LLena_Item_Tipo_Prob(lblTipoProblemaA.Text, "1", cboAccion)
                Call LLena_Item_Tipo_Prob(lblTipoProblemaA.Text, "2", cboCausa)
                txtFecAcc.Text = FormatoFecha(FechaActual)
                txtHorAcc.Text = FormatoHora(HoraActual)
                txtDescripAcc.Text = "" : txtDescripCausa.Text = ""
                txtPerCodigoAcc.Text = Session("User")
                txtPerNombreAcc.Text = Mid(Session("UserNombre"), 13)
                Cn.Open()
                Dim Sql As String = "SELECT MAX(DPROB_SECUENCIA) FROM TBADMIN_PROBLEMAS_detalle WHERE DPROB_CODIGO='" & pdCodProb & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                Dim cmdSql As New SqlCommand(Sql, Cn)
                Rs = cmdSql.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        txtNroAcc.Text = Format(Nz(Rs(0)) + 1, "000")
                    End While
                Else
                    txtNroAcc.Text = "001"
                End If
                Rs.Close()
                Cn.Close()
                Call Llenar_Grilla_TA()
            End If
        Catch Ex As SqlException
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
        If bolError = True Then lblMensaje2.Text = "No se encontraron Acciones."
    End Sub
    Private Sub LLena_Item_Tipo_Prob(ByVal Tipo_Prob As String, ByVal Tipo_Item As String, ByVal cbo As DropDownList)
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdGlobal As New SqlCommand
        lblErrorAcc.Text = ""
        Try
            cbo.Items.Clear()
            cn.Open()
            cmdGlobal.Connection = cn
            cmdGlobal.CommandText = "SELECT ITEM_VALOR,ITEM_CODIGO FROM TBADMIN_PROBLEMAS_ITEMS WHERE ITEM_APROB_TIPO='" & Tipo_Prob & "' AND ITEM_TIPO='" & Tipo_Item & "' AND ITEM_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' ORDER BY ITEM_VALOR"
            cbo.DataSource = cmdGlobal.ExecuteReader
            cbo.DataTextField = "ITEM_VALOR"
            cbo.DataValueField = "ITEM_CODIGO"
            cbo.DataBind()
            If Tipo_Item = "1" Then cbo.Items.Add("< Ingresar Acción >") : cbo.SelectedValue = "< Ingresar Acción >"
            If Tipo_Item = "2" Then cbo.Items.Add("< Ingresar Causa >") : cbo.SelectedValue = "< Ingresar Causa >"
            cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
            cn.Close()
        Catch Ex As SqlException
            lblErrorAcc.ForeColor = Drawing.Color.Red
            lblErrorAcc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorAcc.ForeColor = Drawing.Color.Red
            lblErrorAcc.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Carga_Personal(ByVal cbo As DropDownList)
        Dim obj As New clsMesaAyuda
        Dim dt As DataTable
        Try
            dt = obj.MALista_PersonalSol(Session("Ruta_Emp"), Session("CodEmpresa"))
            cbo.DataSource = dt
            cbo.DataTextField = "PERSONAL1"
            cbo.DataValueField = "PSOL_PERSONAL"
            cbo.DataBind()
            cbo.Items.Add("< Seleccionar >") : cbo.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlException
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Carga_Datos(ByVal pCodProblema As Double)
        Dim obj As New clsMesaAyuda
        Dim dt As DataTable
        Try
            dt = obj.MAConsulta_xProblema(Session("Ruta_Emp"), Session("CodEmpresa"), pCodProblema)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtProb1.Text = Format(Nz(dr("APROB_CODIGO")), "00000")
                    txtFecR1.Text = Right(Nu(dr("APROB_FECHA_REPORTA")), 2) + " " + Nombre_Mes(Mid(Nu(dr("APROB_FECHA_REPORTA")), 5, 2), False) & " " & Left(Nu(dr("APROB_FECHA_REPORTA")), 4)
                    txtHoraR1.Text = Left(Nu(dr("APROB_HORA_REPORTA")), 2) + ":" + Right(Nu(dr("APROB_HORA_REPORTA")), 2)
                    txtTipoP1.Text = Nu(dr("NIVEL1_DESCRIP"))
                    txtFecV1.Text = IIf(Nu(dr("APROB_FECHA_VISTO")) = "", "", Right(Nu(dr("APROB_FECHA_VISTO")), 2) + " " + Nombre_Mes(Mid(Nu(dr("APROB_FECHA_VISTO")), 5, 2), False) & " " & Left(Nu(dr("APROB_FECHA_VISTO")), 4))
                    txtHoraV1.Text = IIf(Nu(dr("APROB_HORA_VISTO")) = "", "", Left(Nu(dr("APROB_HORA_VISTO")), 2) + ":" + Right(Nu(dr("APROB_HORA_VISTO")), 2))
                    txtEstado1.Text = Nu(dr("PESTADO"))
                    txtPrior1.Text = Nu(dr("APROB_PRIORIDAD"))
                    txtConceptoP1.Text = Nu(dr("NOM_PROB1")) & IIf(Nu(dr("NOM_PROB2")) = "", "", " ; " + Nu(dr("NOM_PROB2")))
                    txtDescripcionP1.Text = Nu(dr("APROB_PROBLEMA_DESCRIPCION"))
                    If Nu(dr!PERSONAL1) = "" Then txtApe1.Text = Nu(dr("PERSONAL2"))
                    If Nu(dr!PERSONAL2) = "" Then txtApe1.Text = Nu(dr("PERSONAL1"))
                    If Nu(dr!PERSONAL1) = "" And Nu(dr!PERSONAL2) = "" Then txtApe1.Text = Nu(dr("PERSONAL3"))
                    txtCodInterno1.Text = Nu(dr("APROB_USUARIO_REPORTA"))
                    txtProb2.Text = Format(Nz(dr("APROB_CODIGO")), "00000")
                    txtFecR2.Text = Right(Nu(dr("APROB_FECHA_REPORTA")), 2) + " " + Nombre_Mes(Mid(Nu(dr("APROB_FECHA_REPORTA")), 5, 2), False) & " " & Left(Nu(dr("APROB_FECHA_REPORTA")), 4)
                    txtHoraR2.Text = Left(Nu(dr("APROB_HORA_REPORTA")), 2) + ":" + Right(Nu(dr("APROB_HORA_REPORTA")), 2)
                    txtTipoP2.Text = Nu(dr("NIVEL1_DESCRIP"))
                    txtFecV2.Text = IIf(Nu(dr("APROB_FECHA_VISTO")) = "", "", Right(Nu(dr("APROB_FECHA_VISTO")), 2) + " " + Nombre_Mes(Mid(Nu(dr("APROB_FECHA_VISTO")), 5, 2), False) & " " & Left(Nu(dr("APROB_FECHA_VISTO")), 4))
                    txtHoraV2.Text = IIf(Nu(dr("APROB_HORA_VISTO")) = "", "", Left(Nu(dr("APROB_HORA_VISTO")), 2) + ":" + Right(Nu(dr("APROB_HORA_VISTO")), 2))
                    txtEstado2.Text = Nu(dr("PESTADO"))
                    txtPrior2.Text = Nu(dr("APROB_PRIORIDAD"))
                    txtConceptoP2.Text = Nu(dr("NOM_PROB1")) & IIf(Nu(dr("NOM_PROB2")) = "", "", " ; " + Nu(dr("NOM_PROB2")))
                    txtDescripcionP2.Text = Nu(dr("APROB_PROBLEMA_DESCRIPCION"))
                    If Nu(dr!PERSONAL1) = "" Then txtApe2.Text = Nu(dr("PERSONAL2"))
                    If Nu(dr!PERSONAL2) = "" Then txtApe2.Text = Nu(dr("PERSONAL1"))
                    If Nu(dr!PERSONAL1) = "" And Nu(dr!PERSONAL2) = "" Then txtApe2.Text = Nu(dr("PERSONAL3"))
                    txtCodInterno2.Text = Nu(dr("APROB_USUARIO_REPORTA"))
                    If Nu(dr!PER_SOLUCIONA) = "" Then txtPerSoluciona2.Text = Nu(dr("PER_SOLUCIONA2"))
                    If Nu(dr!PER_SOLUCIONA2) = "" Then txtPerSoluciona2.Text = Nu(dr("PER_SOLUCIONA"))
                    txtFecA2.Text = Right(Nu(dr("APROB_FECHA_ASIGNADO")), 2) + " " + Nombre_Mes(Mid(Nu(dr("APROB_FECHA_ASIGNADO")), 5, 2), False) & " " & Left(Nu(dr("APROB_FECHA_ASIGNADO")), 4)
                    txtHoraA2.Text = Left(Nu(dr("APROB_HORA_ASIGNADO")), 2) + ":" + Right(Nu(dr("APROB_HORA_ASIGNADO")), 2)
                    lblTipoProblemaA.Text = Nu(dr("APROB_TIPO"))
                    lblTipoProb.Text = lblTipoProblemaA.Text
                    lblTipoProb2.Text = Nu(dr("APROB_PROBLEMA1"))
                    lblTipoProb3.Text = Nu(dr("APROB_PROBLEMA2"))
                Next
            End If
            dt = Nothing
        Catch Ex As SqlException
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblProbError.ForeColor = Drawing.Color.Red
            lblProbError.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexProb_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Listar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.Width = 570
        lblProbError.Text = ""
    End Sub
    Protected Sub btnGuadar1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsMesaAyuda
        Dim dt As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim pdCodProb As Double = txtProb1.Text.Trim
        Dim psFechaV As String = Right(txtFecV1.Text.Trim, 4) + Mid(txtFecV1.Text.Trim, 4, 2) + Left(txtFecV1.Text.Trim, 2)
        Dim psHoraV As String = Left(txtHoraV1.Text.Trim, 2) + Right(txtHoraV1.Text.Trim, 2)
        Dim psFecA As String = Right(txtFecA1.Text.Trim, 4) + Mid(txtFecA1.Text.Trim, 4, 2) + Left(txtFecA1.Text.Trim, 2)
        Dim psHoraA As String = Left(txtHoraA1.Text.Trim, 2) + Right(txtHoraA1.Text.Trim, 2)
        lblErrorAsig.Text = ""
        Try
            If cboPersonal.SelectedValue = "< Seleccionar >" Then lblMensaje2.Text = "Falta seleccionar la persona." : Exit Sub
            dt = obj.MAConsulta_Problema(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodProb)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If Nu(dr("APROB_ASIGNADO_PERSONA")) <> cboPersonal.SelectedValue.Trim Then
                        obj.MAUpdate_xProblemaNoVisto(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodProb, psFechaV, psHoraV)
                        obj.MAUpdate_ProblemaAsignado(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodProb, cboPersonal.SelectedValue.Trim, psFecA, psHoraA)
                        If Nu(dr("APROB_ASIGNADO_PERSONA")) <> "" Then
                            obj.MAInsert_ProblemaAsignado(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodProb, Nu(dr("APROB_ASIGNADO_PERSONA")), Nu(dr("APROB_FECHA_ASIGNADO")), Nu(dr("APROB_HORA_ASIGNADO")), Nu(dr("APROB_ESTADO")), Nu(dr("APROB_FECHA_ASIGVISTO")), Nu(dr("APROB_HORA_ASIGVISTO")), User.Identity.Name)
                        End If
                        Cn.Open()
                        CmdGlobal.Connection = Cn
                        If cboPersonal.SelectedValue.Trim = User.Identity.Name Then
                            CmdGlobal.CommandText = "UPDATE TBADMIN_PROBLEMAS SET APROB_ESTADO='4',APROB_FECHA_ASIGVISTO='" & psFecA & "', APROB_HORA_ASIGVISTO='" & psHoraA & "' WHERE APROB_CODIGO=" & pdCodProb & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            CmdGlobal.ExecuteNonQuery()
                        Else
                            CmdGlobal.CommandText = "UPDATE TBADMIN_PROBLEMAS SET APROB_ESTADO='3',APROB_FECHA_ASIGVISTO=NULL, APROB_HORA_ASIGVISTO=NULL WHERE APROB_CODIGO=" & pdCodProb & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            CmdGlobal.ExecuteNonQuery()
                        End If
                        Cn.Close()
                    Else
                        lblMensaje2.Text = "No hay cambios que guardar."
                    End If
                Next
            End If
            dt = Nothing
        Catch Ex As SqlException
            lblErrorAsig.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorAsig.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnRegresar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar2.Click
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.Width = 570
        lblProbError.Text = ""
    End Sub
    Private Sub Lista_Items(ByVal psTipoItem As String, ByVal pdTipoProb As Double)
        Dim obj As New clsMesaAyuda
        FlexItem.DataSource = obj.MALista_Item(Session("Ruta_Emp"), Session("CodEmpresa"), pdTipoProb, psTipoItem)
        FlexItem.DataBind()
        lblRegistroItem.Text = "Se han encontrado " & FlexItem.Rows.Count & " registros."
    End Sub
    Protected Sub btnIngAcc_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub
    Protected Sub btnIngCausa_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub
    Protected Sub btnNuevoItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblRegItem.Visible = True
        txtDescripItem.Text = ""
        lblEtiqItem.Text = "Nuevo Item"
        If lblTipoItem.Text = "1" Then ModalPopupExtender1.Show()
        If lblTipoItem.Text = "2" Then ModalPopupExtender2.Show()
    End Sub
    Protected Sub btnCancelarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblRegItem.Visible = False
        txtDescripItem.Text = ""
        If lblTipoItem.Text = "1" Then ModalPopupExtender1.Show()
        If lblTipoItem.Text = "2" Then ModalPopupExtender2.Show()
    End Sub
    Protected Sub btnGuardarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsMesaAyuda
        Dim pdTipoProb As Double = 0
        If lblTipoProb.Text.Trim <> "" Then pdTipoProb = lblTipoProb.Text
        Dim pdCodItem As Double = 0
        lblErrorItem.Text = ""
        Try
            If lblEtiqItem.Text = "Nuevo Item" Then
                lblCodItem.Text = obj.MAUltimo_Item(Session("Ruta_Emp"), Session("CodEmpresa"))
                pdCodItem = Nz(lblCodItem.Text.Trim) + 1
                obj.MAInsert_Item(Session("Ruta_Emp"), Session("CodEmpresa"), pdTipoProb, pdCodItem, txtDescripItem.Text.Trim, lblTipoItem.Text)
            ElseIf lblEtiqItem.Text = "Editar Item" Then
                pdCodItem = lblCodItem.Text.Trim
                obj.MAUpdate_Item(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodItem, txtDescripItem.Text.Trim)
            End If
            If lblTipoItem.Text = "1" Then Call LLena_Item_Tipo_Prob(lblTipoProb.Text, "1", cboAccion) : ModalPopupExtender1.Show()
            If lblTipoItem.Text = "2" Then Call LLena_Item_Tipo_Prob(lblTipoProb.Text, "2", cboCausa) : ModalPopupExtender2.Show()
            lblRegItem.Visible = False
            txtDescripItem.Text = ""
        Catch Ex As SqlException
            lblErrorItem.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorItem.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnListarItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim pdTipoProb As Double = 0
        If lblTipoProb.Text.Trim <> "" Then pdTipoProb = lblTipoProb.Text
        Try
            Call Lista_Items(lblTipoItem.Text, pdTipoProb)
            If lblTipoItem.Text = "1" Then ModalPopupExtender1.Show()
            If lblTipoItem.Text = "2" Then ModalPopupExtender2.Show()
        Catch Ex As SqlException
            lblErrorItem.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorItem.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexItem_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexItem.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Try
            If e.CommandName = "Editar" Then
                lblEtiqItem.Text = "Editar Item"
                lblCodItem.Text = FlexItem.Rows(Index).Cells(1).Text.Trim
                txtDescripItem.Text = FlexItem.Rows(Index).Cells(2).Text.Trim
            End If
        Catch Ex As SqlException
            lblErrorItem.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorItem.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnGuadar2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsMesaAyuda
        Dim pdCodProb As Double = 0
        Dim pdTipoProb1 As Double = 0
        Dim pdTipoProb2 As Double = 0
        Dim pdTipoProb3 As Double = 0
        Dim psAccion As String = ""
        Dim psCausa As String = ""
        Dim psEstado As String = ""
        Dim psFechaAcc As String = Right(txtFecAcc.Text.Trim, 4) + Mid(txtFecAcc.Text.Trim, 4, 2) + Left(txtFecAcc.Text.Trim, 2)
        Dim psHoraAcc As String = Left(txtHorAcc.Text.Trim, 2) + Right(txtHorAcc.Text.Trim, 2)
        If cboAccion.SelectedValue.Trim = "< Seleccionar >" Or cboAccion.SelectedValue.Trim = "< Ingresar Acción >" Then lblErrorAcc.Text = "Le recordamos que debe aplicar una acción." : Exit Sub
        If cboAccion.SelectedValue.Trim <> "< Seleccionar >" And cboAccion.SelectedValue.Trim <> "< Ingresar Acción >" Then psAccion = cboAccion.SelectedValue.Trim
        If cboCausa.SelectedValue.Trim <> "< Seleccionar >" And cboAccion.SelectedValue.Trim <> "< Ingresar Causa >" Then psCausa = cboCausa.SelectedValue.Trim
        If optCierre.SelectedValue.Trim = "No" Then psEstado = "5"
        If optCierre.SelectedValue.Trim = "Si" Then psEstado = "0"
        If optCierre.SelectedValue.Trim = "A" Then psEstado = "0"
        pdCodProb = txtProb2.Text.Trim
        pdTipoProb1 = lblTipoProblemaA.Text.Trim
        pdTipoProb2 = lblTipoProb2.Text.Trim
        pdTipoProb3 = lblTipoProb3.Text.Trim
        Try
            obj.MAInsert_ProblemaDetalle(Session("CodEmpresa"), Session("Ruta_Emp"), pdCodProb, psAccion, _
                                         txtDescripAcc.Text.Trim, psCausa, txtDescripCausa.Text.Trim, _
                                         Session("User"), psEstado, pdTipoProb1, pdTipoProb2, pdTipoProb3, psFechaAcc, psHoraAcc)
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            txtBusCodProb.Text = pdCodProb
            If optCierre.SelectedValue.Trim = "A" Then
                'ABRIR PROBLEMA
            End If
            Listar_Click(sender, e)
            Call Cargar_Acciones()
        Catch Ex As SqlException
            lblErrorAcc.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorAcc.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboAccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboAccion.SelectedValue = "< Ingresar Acción >" Then
            lblErrorAcc.Text = ""
            lblTipoItem.Text = "1"
            btnIngAcc.Enabled = True
            lblEtqMantenimiento.Text = "Mantenimiento de Acciones"
            Dim pdTipoProb As Double = 0
            If lblTipoProb.Text.Trim <> "" Then pdTipoProb = lblTipoProb.Text
            Call Lista_Items(lblTipoItem.Text, pdTipoProb)
        Else
            lblErrorAcc.Text = ""
            lblTipoItem.Text = ""
            btnIngAcc.Enabled = False
            lblEtqMantenimiento.Text = ""
            Dim pdTipoProb As Double = 0
            If lblTipoProb.Text.Trim <> "" Then pdTipoProb = lblTipoProb.Text
            Call Lista_Items(lblTipoItem.Text, pdTipoProb)
        End If
    End Sub
    Protected Sub cboCausa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboCausa.SelectedValue = "< Ingresar Causa >" Then
            lblErrorAcc.Text = ""
            lblTipoItem.Text = "2"
            btnIngCausa.Enabled = True
            lblEtqMantenimiento.Text = "Mantenimiento de Causas"
            Dim pdTipoProb As Double = 0
            If lblTipoProb.Text.Trim <> "" Then pdTipoProb = lblTipoProb.Text
            Call Lista_Items(lblTipoItem.Text, pdTipoProb)
        Else
            lblErrorAcc.Text = ""
            lblTipoItem.Text = ""
            btnIngCausa.Enabled = False
            lblEtqMantenimiento.Text = ""
            Dim pdTipoProb As Double = 0
            If lblTipoProb.Text.Trim <> "" Then pdTipoProb = lblTipoProb.Text
            Call Lista_Items(lblTipoItem.Text, pdTipoProb)
        End If
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
    End Sub
    Private Sub Exportar_Excel()
        Dim SqlEst As String = ""
        If chk0.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='0'"
        End If
        If chk1.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='1'"
        End If
        If chk2.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='2'"
        End If
        If chk3.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='3'"
        End If
        If chk4.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='4'"
        End If
        If chk5.Checked = True Then
            If SqlEst <> "" Then SqlEst = SqlEst & " OR "
            SqlEst = SqlEst & "APROB_ESTADO='5'"
        End If
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim bolError As Boolean
        Dim i As Integer

        Dim dt As New DataTable
        Dim dr As DataRow
        Dim FechaIni As String = ""
        Dim FechaFin As String = ""
        FechaIni = Right(txtFechaIni.Text, 4) & Mid(txtFechaIni.Text, 4, 2) & Left(txtFechaIni.Text, 2)
        FechaFin = Right(txtFechaFin.Text, 4) & Mid(txtFechaFin.Text, 4, 2) & Left(txtFechaFin.Text, 2)
        dt.Columns.Add("#", GetType(String))
        dt.Columns.Add("Nro_Problema", GetType(String))
        dt.Columns.Add("Fecha_Reporta", GetType(String))
        dt.Columns.Add("Hora_Reporta", GetType(String))
        dt.Columns.Add("Tipo_Problema", GetType(String))
        dt.Columns.Add("Prioridad", GetType(String))
        dt.Columns.Add("Concepto_Problema", GetType(String))
        dt.Columns.Add("Elemento", GetType(String))
        dt.Columns.Add("Elemento_2", GetType(String))
        dt.Columns.Add("Descripcion_Problema", GetType(String))
        dt.Columns.Add("Estado", GetType(String))
        dt.Columns.Add("Persona_Reporta", GetType(String))
        dt.Columns.Add("Fecha_Visto", GetType(String))
        dt.Columns.Add("Hora_Visto", GetType(String))
        dt.Columns.Add("Fecha_Solucion", GetType(String))
        dt.Columns.Add("Hora_Solucion", GetType(String))
        Try
            Cn.Open()
            Dim Sql As String = "SELECT APROB_USUARIO_REPORTA,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From BDGRUPOEMPRESAS.dbo.TBPERSONAL WHERE PERSON_CODIGO = APROB_USUARIO_REPORTA) AS PERSONAL1," _
                              & "(SELECT ADMCRI_DESCRIPCION From TBADMIN_CRITERIOS WHERE ADMCRI_CODIGO = AP.APROB_TIPO2 AND ADMCRI_TIPO = '1' AND ADMCRI_SYS_EST='0' AND EMPRESA_CODIGO=AP.EMPRESA_CODIGO) AS TIPO_PROB, " _
                              & "APROB_TIPO,NIVEL1_DESCRIP,P.COLOR_CODIGO,P.COLOR_ROJO,P.COLOR_VERDE,P.COLOR_AZUL,APROB_PROBLEMA1,(SELECT NIVEL2_DESCRIP FROM TBESP_PRO2 WHERE NIVEL2_CODIGO=APROB_PROBLEMA1) AS NOM_PROB1, " _
                              & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE USUARI_CODIGO = APROB_USUARIO_REPORTA AND LEFT(APROB_USUARIO_REPORTA,4)='1111') AS PERSONAL2, APROB_CODIGO, APROB_PRIORIDAD," _
                              & "(SELECT APERSONA_APELLIDOS + ',' + APERSONA_NOMBRE From TBADMIN_PERSONA WHERE APERSONA_USUARIO = APROB_USUARIO_REPORTA ) AS PERSONAL3, " _
                              & "APROB_PROBLEMA2,(SELECT NIVEL3_DESCRIP FROM TBESP_PRO3 X WHERE NIVEL3_CODIGO=APROB_PROBLEMA2) AS NOM_PROB2 , APROB_PROBLEMA_DESCRIPCION," _
                              & "APROB_FECHA_REPORTA, APROB_HORA_REPORTA,APROB_ESTADO,APROB_SYS_EST,APROB_FECHA_SOLUCION,APROB_HORA_SOLUCION, " _
                              & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_ESTADO AND ELEMEN_TABLA='TBOPC185') AS PESTADO,APROB_FECHA_VISTO, APROB_HORA_VISTO, " _
                              & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_CONFORMIDAD_USUARIOREP AND ELEMEN_TABLA='TBOPC057') AS ECONFORME " _
                              & "From TBADMIN_PROBLEMAS AP INNER JOIN TBESP_PRO1 P ON NIVEL1_CODIGO=APROB_TIPO WHERE NOT(APROB_TIPO IS NULL) AND (AP.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            If txtBusCodProb.Text.Trim <> "" Then Sql = Sql & " AND APROB_CODIGO = " & txtBusCodProb.Text.Trim & ""
            If cboTipoProb.SelectedValue <> "< Seleccionar >" Then Sql = Sql & " AND APROB_TIPO2 = '" & cboTipoProb.SelectedValue.Trim & "'"
            If FechaIni <> "" Then
                If FechaIni <> "" And FechaFin <> "" Then
                    Sql = Sql & " AND APROB_FECHA_REPORTA between '" & FechaIni & "' and '" & FechaFin & "' "
                ElseIf FechaIni <> "" Then
                    Sql = Sql & " AND APROB_FECHA_REPORTA = '" & FechaIni & "'"
                End If
            End If
            Sql = Sql & " AND (" & SqlEst & ")"
            If lblHabilita.Text = "V" Then Sql = Sql & " AND (APROB_USUARIO_REPORTA='" & lblUsuarioCodigo.Text & "')"
            Sql = Sql & " AND (APROB_SYS_EST='0') "
            Sql = Sql & " ORDER BY APROB_FECHA_REPORTA+APROB_HORA_REPORTA DESC"
            Dim cmdSql As New SqlCommand(Sql, Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    dr = dt.NewRow()
                    dr("#") = i.ToString
                    dr("Nro_Problema") = Format(Nz(Rs!APROB_CODIGO), "00000")
                    dr("Fecha_Reporta") = Right(Nu(Rs!APROB_FECHA_REPORTA), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_REPORTA), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_REPORTA), 4)
                    dr("Hora_Reporta") = Left(Nu(Rs!APROB_HORA_REPORTA), 2) + ":" + Right(Nu(Rs!APROB_HORA_REPORTA), 2)
                    dr("Tipo_Problema") = Nu(Rs!TIPO_PROB)
                    dr("Fecha_Visto") = IIf(Nu(Rs!APROB_FECHA_VISTO) = "", "", Right(Nu(Rs!APROB_FECHA_VISTO), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_VISTO), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_VISTO), 4))
                    dr("Hora_Visto") = IIf(Nu(Rs!APROB_HORA_VISTO) = "", "", Left(Nu(Rs!APROB_HORA_VISTO), 2) + ":" + Right(Nu(Rs!APROB_HORA_VISTO), 2))
                    dr("Fecha_Solucion") = IIf(Nu(Rs!APROB_FECHA_SOLUCION) = "", "", Right(Nu(Rs!APROB_FECHA_SOLUCION), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_SOLUCION), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_SOLUCION), 4))
                    dr("Hora_Solucion") = IIf(Nu(Rs!APROB_HORA_SOLUCION) = "", "", Left(Nu(Rs!APROB_HORA_SOLUCION), 2) + ":" + Right(Nu(Rs!APROB_HORA_SOLUCION), 2))
                    dr("Estado") = Nu(Rs!PESTADO)
                    dr("Prioridad") = Nu(Rs!APROB_PRIORIDAD)
                    dr("Concepto_Problema") = Nu(Rs!NIVEL1_DESCRIP)
                    dr("Elemento") = Nu(Rs!NOM_PROB1)
                    dr("Elemento_2") = Nu(Rs!NOM_PROB2)
                    dr("Descripcion_Problema") = Nu(Rs!APROB_PROBLEMA_DESCRIPCION)
                    If Nu(Rs!PERSONAL1) = "" Then dr("Persona_Reporta") = Nu(Rs!PERSONAL2)
                    If Nu(Rs!PERSONAL2) = "" Then dr("Persona_Reporta") = Nu(Rs!PERSONAL1)
                    If Nu(Rs!PERSONAL1) = "" And Nu(Rs!PERSONAL2) = "" Then dr("Persona_Reporta") = Nu(Rs!PERSONAL3)
                    dt.Rows.Add(dr)
                End While
            Else
                bolError = True
            End If
        Catch Ex As SqlException
            lblProbError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblProbError.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
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
End Class
