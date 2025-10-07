Imports System.Data.SqlClient
Imports System.Web.Security
Imports WebGestor
Imports System.Data
Partial Class AdminProblemas_Creacion2
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
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Introducir aquí el código de usuario para inicializar la página
        If Not Page.IsPostBack Then
            If User.Identity.Name = "" Then
                Session.Clear()
                FormsAuthentication.SignOut()
                Response.Redirect("TerminaSesion.aspx")
                Exit Sub
            End If
            lblUsuarioCodigo.Text = User.Identity.Name
            Session("ParamPage") = "AP2"

            txtFechaIni.Text = FormatoFecha(FechaActual())
            txtFechaFin.Text = txtFechaIni.Text
            'txtFecha.TodaysDate = CDate(FormatoFecha(lblFechaIni.Text))
            'txtFecha.SelectedDate = txtFecha.TodaysDate
            chk1.Enabled = True
            chk2.Enabled = True
            lblSeguridad.Text = Session("VSeg")
            If lblSeguridad.Text = "SR" Then 'solo reporta
                lblTitulo.InnerText = "Administración de Problemas - Problemas Reportados"
                chk1.Checked = True
                chk2.Checked = True
                chk3.Checked = True
                chk4.Checked = True
                chk5.Checked = True
                Flex.Columns(28).Visible = False
                Flex.Columns(22).Visible = False
            ElseIf lblSeguridad.Text = "TD" Then
                lblTitulo.InnerText = "Administración de Problemas"
                chk1.Checked = True
                Flex.Columns(28).Visible = True
                Flex.Columns(22).Visible = True
            ElseIf lblSeguridad.Text = "SO" Then
                lblTitulo.InnerText = "Administración de Problemas - Soluciona"
                chk1.Enabled = False
                chk2.Enabled = False
                chk3.Checked = True
                Flex.Columns(28).Visible = True
                Flex.Columns(22).Visible = True
            End If
        End If
    End Sub
    Sub OpcionesFlex(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Rs As SqlDataReader
        Dim bolError As Boolean
        Dim i As Integer
        lblMensaje2.Text = ""
        lblMensaje2.ForeColor = Drawing.Color.Black
        Flex2.Visible = False
        If e.Item.Cells.Count < 5 Then Exit Sub
        If e.Item.Cells(5).Text = "" Then Exit Sub
        If e.CommandName <> "MostrarAcciones" Then Exit Sub
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
            Cn.Open()
            Dim Sql As String = "SELECT DPROB_SECUENCIA, DPROB_ACCION,(SELECT ITEM_VALOR From TBADMIN_PROBLEMAS_ITEMS I WHERE I.EMPRESA_CODIGO=PD.EMPRESA_CODIGO AND I.ITEM_CODIGO = PD.DPROB_ACCION) AS XACCION," _
                              & "DPROB_ACCION_DESCRIPCION, DPROB_OBSERVACION,(SELECT ITEM_VALOR From TBADMIN_PROBLEMAS_ITEMS I WHERE I.EMPRESA_CODIGO=PD.EMPRESA_CODIGO AND I.ITEM_CODIGO = PD.DPROB_OBSERVACION) AS XObserva," _
                              & "DPROB_OBSERVACION_DESCRIPCION,DPROB_USUARIO_ACCION,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From BDGRUPOEMPRESAS.DBO.TBPERSONAL  WHERE PERSON_CODIGO = DPROB_USUARIO_ACCION) AS PERSONAL1," _
                              & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI U WHERE USUARI_CODIGO = DPROB_USUARIO_ACCION) AS PERSONAL2," _
                              & " DPROB_FECHA_ACCION,DPROB_HORA_ACCION,DPROB_SECUENCIA_ESTADO FROM TBADMIN_PROBLEMAS_DETALLE PD WHERE (DPROB_CODIGO = '" & e.Item.Cells(5).Text & "') AND (DPROB_SYS_EST = '0') AND (PD.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            Sql = Sql & " AND DPROB_SECUENCIA_ESTADO='0'"
            Sql = Sql & " ORDER BY DPROB_SECUENCIA"
            Dim cmdSql As New SqlCommand(Sql, Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                Flex2.Visible = True
                lblMensaje2.Text = "Acciones tomadas del Problema Nº " & e.Item.Cells(5).Text
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
            Else
                bolError = True
            End If
        Catch Ex As SqlException
            lblMensaje2.ForeColor = Drawing.Color.Red
            lblMensaje2.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblMensaje2.ForeColor = Drawing.Color.Red
            lblMensaje2.Text = "Ha ocurrido un error la Aplicacion:<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
        If bolError = True Then lblMensaje2.Text = "No se encontraron Acciones."
    End Sub
    Sub MyFlex_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        lblMensaje2.Text = ""
        Flex2.Visible = False
        Flex.CurrentPageIndex = e.NewPageIndex
        lblMensaje.Text = ""
        Flex.PagerStyle.HorizontalAlign = HorizontalAlign.Center
        Flex.PagerStyle.VerticalAlign = VerticalAlign.Middle
        Flex.DataSource = Carga_Problemas()
        Flex.DataBind()
        Dim n As Integer, Ver As Boolean
        Dim Fila As DataGridItem
        For n = 0 To Flex.Items.Count - 1
            Ver = False
            If Flex.Items(n).Cells(27).Text = "1" And lblSeguridad.Text = "TD" Then
                Ver = True
            End If
            If Flex.Items(n).Cells(27).Text = "3" And (lblSeguridad.Text = "SO" Or (lblSeguridad.Text = "TD" And lblUsuarioCodigo.Text = Flex.Items(n).Cells(23).Text)) Then
                Ver = True
            End If
            Fila = Flex.Items(n)
            Dim nSiVer As LinkButton = CType(Fila.FindControl("cmdVer"), LinkButton)
            If Not nSiVer Is Nothing Then
                nSiVer.Visible = Ver
            End If
            Dim nVisto As Label = CType(Fila.FindControl("lblVisto"), Label)
            If Not nVisto Is Nothing Then
                If Flex.Items(n).Cells(27).Text = "2" Or Flex.Items(n).Cells(27).Text = "4" Or Flex.Items(n).Cells(27).Text = "5" Or Flex.Items(n).Cells(27).Text = "0" Then
                    nVisto.Visible = True
                Else
                    nVisto.Visible = False
                End If
            End If
            If Flex.Items(n).Cells(1).Text <> "" And Flex.Items(n).Cells(2).Text <> "" And Flex.Items(n).Cells(3).Text <> "" Then
                Flex.Items(n).Cells(4).BackColor = Drawing.Color.FromArgb(CLng(Flex.Items(n).Cells(1).Text), CLng(Flex.Items(n).Cells(2).Text), CLng(Flex.Items(n).Cells(3).Text))
            End If
        Next
    End Sub
    Private Sub Listar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Listar.Click
        lblMensaje2.Text = ""
        If txtFechaIni.Text = "" And txtFechaFin.Text <> "" Then lblMensaje.Text = "Debe ingresar la fecha de inicio." : Exit Sub
        If txtFechaIni.Text > txtFechaFin.Text Then lblMensaje.Text = "La fecha inicio debe ser menor a la fecha fin." : Exit Sub
        Flex2.Visible = False
        lblMensaje.Text = ""
        FlexProb.DataSource = Carga_Problemas()
        FlexProb.DataBind()

        'Dim n As Integer, Ver As Boolean
        'Dim Fila As DataGridItem
        'For n = 0 To Flex.Items.Count - 1
        '    Ver = False
        '    If Flex.Items(n).Cells(27).Text = "1" And lblSeguridad.Text = "TD" Then
        '        Ver = True
        '    End If
        '    If Flex.Items(n).Cells(27).Text = "3" And (lblSeguridad.Text = "SO" Or (lblSeguridad.Text = "TD" And lblUsuarioCodigo.Text = Flex.Items(n).Cells(23).Text)) Then
        '        Ver = True
        '    End If
        '    Fila = Flex.Items(n)
        '    Dim nSiVer As LinkButton = CType(Fila.FindControl("cmdVer"), LinkButton)
        '    If Not nSiVer Is Nothing Then
        '        nSiVer.Visible = Ver
        '    End If
        '    Dim nVisto As Label = CType(Fila.FindControl("lblVisto"), Label)
        '    If Not nVisto Is Nothing Then
        '        If Flex.Items(n).Cells(27).Text = "2" Or Flex.Items(n).Cells(27).Text = "4" Or Flex.Items(n).Cells(27).Text = "5" Or Flex.Items(n).Cells(27).Text = "0" Then
        '            nVisto.Visible = True
        '        Else
        '            nVisto.Visible = False
        '        End If
        '    End If
        '    If Flex.Items(n).Cells(1).Text <> "" And Flex.Items(n).Cells(2).Text <> "" And Flex.Items(n).Cells(3).Text <> "" Then
        '        Flex.Items(n).Cells(4).BackColor = Drawing.Color.FromArgb(CLng(Flex.Items(n).Cells(1).Text), CLng(Flex.Items(n).Cells(2).Text), CLng(Flex.Items(n).Cells(3).Text))
        '    End If
        'Next
        Listar_Problemas()
    End Sub
    Private Function Cargar_Prob() As DataTable
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Sql As String : Sql = ""
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
        If SqlEst = "" Then lblMensaje.Text = "No hay estado de problema marcado, favor de hacerlo para poder listar." : Exit Function
        Dim dt As New DataTable
        Dim FechaIni As String = ""
        Dim FechaFin As String = ""
        FechaIni = Right(txtFechaIni.Text, 4) & Mid(txtFechaIni.Text, 4, 2) & Left(txtFechaIni.Text, 2)
        FechaFin = Right(txtFechaFin.Text, 4) & Mid(txtFechaFin.Text, 4, 2) & Left(txtFechaFin.Text, 2)
        Sql = " SELECT APROB_USUARIO_REPORTA,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From BDGRUPOEMPRESAS.DBO.TBPERSONAL WHERE PERSON_CODIGO = APROB_USUARIO_REPORTA) AS PERSONAL1," _
            & "APROB_TIPO,NIVEL1_DESCRIP,P.COLOR_CODIGO,P.COLOR_ROJO,P.COLOR_VERDE,P.COLOR_AZUL,APROB_PROBLEMA1,(SELECT NIVEL2_DESCRIP FROM TBESP_PRO2 WHERE NIVEL2_CODIGO=APROB_PROBLEMA1) AS NOM_PROB1, " _
            & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE USUARI_CODIGO = APROB_USUARIO_REPORTA AND LEFT(APROB_USUARIO_REPORTA,4)='1111') AS PERSONAL2, APROB_CODIGO, APROB_PRIORIDAD," _
            & "APROB_PROBLEMA2,(SELECT NIVEL3_DESCRIP FROM TBESP_PRO3 WHERE NIVEL3_CODIGO=APROB_PROBLEMA2) AS NOM_PROB2 , APROB_PROBLEMA_DESCRIPCION," _
            & "APROB_FECHA_REPORTA, APROB_HORA_REPORTA,APROB_ESTADO,APROB_SYS_EST,APROB_FECHA_SOLUCION,APROB_HORA_SOLUCION, " _
            & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_ESTADO AND ELEMEN_TABLA='TBOPC185') AS PESTADO,APROB_FECHA_VISTO, APROB_HORA_VISTO, " _
            & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_CONFORMIDAD_USUARIOREP AND ELEMEN_TABLA='TBOPC057') AS ECONFORME, " _
            & "APROB_FECHA_ASIGNADO,APROB_HORA_ASIGNADO,APROB_ASIGNADO_PERSONA,APROB_FECHA_ASIGVISTO,APROB_HORA_ASIGVISTO,(SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES FROM BDGRUPOEMPRESAS.DBO.TBPERSONAL WHERE PERSON_CODIGO=APROB_ASIGNADO_PERSONA) AS PERSON_ASIG1," _
            & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE USUARI_CODIGO = APROB_ASIGNADO_PERSONA AND LEFT(APROB_ASIGNADO_PERSONA,4)='1111') AS PERSON_ASIG2, " _
            & "APROB_TIPO_ORIG,(SELECT NIVEL1_DESCRIP FROM TBESP_PRO1 WHERE NIVEL1_CODIGO=APROB_TIPO_ORIG) AS NOM_PROB_ORIG,APROB_PROBLEMA1_ORIG,(SELECT NIVEL2_DESCRIP FROM TBESP_PRO2 WHERE NIVEL2_CODIGO=APROB_PROBLEMA1_ORIG) AS NOM_PROB_ORIG1,APROB_PROBLEMA2_ORIG,(SELECT NIVEL3_DESCRIP FROM TBESP_PRO3 WHERE NIVEL3_CODIGO=APROB_PROBLEMA2_ORIG) AS NOM_PROB_ORIG2 " _
            & "From TBADMIN_PROBLEMAS AP INNER JOIN TBESP_PRO1 P ON NIVEL1_CODIGO=APROB_TIPO WHERE NOT(APROB_TIPO IS NULL) AND (AP.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
        If FechaIni <> "" Then
            If FechaIni <> "" And FechaFin <> "" Then
                Sql = Sql & " AND APROB_FECHA_REPORTA between '" & FechaIni & "' and '" & FechaFin & "' "
            ElseIf FechaIni <> "" And FechaFin = "" Then
                Sql = Sql & " AND APROB_FECHA_REPORTA = '" & FechaIni & "'"
            End If
        End If
        Sql = Sql & " AND (" & SqlEst & ")"
        If lblSeguridad.Text = "SR" Then  'solo reporta
            Sql = Sql & "AND (APROB_USUARIO_REPORTA='" & lblUsuarioCodigo.Text & "')"
        ElseIf lblSeguridad.Text = "SO" Then
            Sql = Sql & "AND (APROB_ASIGNADO_PERSONA='" & lblUsuarioCodigo.Text & "')"
        End If
        Sql = Sql & " AND (APROB_SYS_EST='0') "
        Sql = Sql & " ORDER BY APROB_FECHA_REPORTA+APROB_HORA_REPORTA DESC"
        Dim Cmd As New SqlCommand(Sql, Cn)
        Dim Da As New SqlDataAdapter(Cmd)
        Da.Fill(Dt)
        Return Dt
    End Function
    Private Sub Listar_Problemas()
        lblMensaje.Text = ""
        'If txtActividades.Text = "" Then lblMensaje.Text = "Debe ingresar la Actividad a agregar."
        'If txtFechaAccion.Text = "" Then lblMensaje.Text = "Debe ingresar la Fecha de Inicio."
        'If lblMensaje.Text <> "" Then Exit Sub
        Dim dt As New Data.DataTable
        Dim dt2 As New Data.DataTable
        Dim i As Integer : i = 0
        dt.Columns.Add("APROB_CODIGO")
        dt.Columns.Add("APROB_FECHA_REPORTA")
        dt.Columns.Add("APROB_HORA_REPORTA")
        dt.Columns.Add("NIVEL1_DESCRIP")
        dt.Columns.Add("APROB_FECHA_VISTO")
        dt.Columns.Add("APROB_HORA_VISTO")
        dt.Columns.Add("APROB_FECHA_ASIGNADO")
        dt.Columns.Add("APROB_HORA_ASIGNADO")
        dt.Columns.Add("PERSON_ASIG1")
        dt.Columns.Add("APROB_FECHA_ASIGVISTO")
        dt.Columns.Add("APROB_HORA_ASIGVISTO")
        dt.Columns.Add("APROB_FECHA_SOLUCION")
        dt.Columns.Add("APROB_HORA_SOLUCION")
        dt.Columns.Add("PESTADO")
        dt.Columns.Add("APROB_PRIORIDAD")
        dt.Columns.Add("NOM_PROB1")
        dt.Columns.Add("APROB_PROBLEMA_DESCRIPCION")
        dt.Columns.Add("ECONFORME")
        dt.Columns.Add("NOM_PROB_ORIG")
        dt.Columns.Add("NOM_PROB_ORIG1")
        dt.Columns.Add("COLOR_ROJO")
        dt.Columns.Add("COLOR_VERDE")
        dt.Columns.Add("COLOR_AZUL")
        dt.Columns.Add("PERSONAL1")
        dt.Columns.Add("APROB_ASIGNADO_PERSONA")
        dt.Columns.Add("APROB_ESTADO")
        Dim dr As Data.DataRow
        For Each drItem As Data.DataRow In Cargar_Prob.Rows
            dr = dt.NewRow
            dr("APROB_CODIGO") = Format(Nz(drItem("APROB_CODIGO").ToString), "000000")
            dr("APROB_FECHA_REPORTA") = Right(Nu(drItem("APROB_FECHA_REPORTA")), 2) + " " + Nombre_Mes(Mid(Nu(drItem("APROB_FECHA_REPORTA")), 5, 2), False) & " " & Left(Nu(drItem("APROB_FECHA_REPORTA")), 4)
            dr("APROB_HORA_REPORTA") = FormatoHora(Nu(drItem("APROB_HORA_REPORTA")))
            dr("NIVEL1_DESCRIP") = Nu(drItem("NIVEL1_DESCRIP"))
            dr("APROB_FECHA_VISTO") = IIf(Nu(drItem("APROB_FECHA_VISTO")) = "", "", Right(Nu(drItem("APROB_FECHA_VISTO")), 2) + " " + Nombre_Mes(Mid(Nu(drItem("APROB_FECHA_VISTO")), 5, 2), False) & " " & Left(Nu(drItem("APROB_FECHA_VISTO")), 4))
            dr("APROB_HORA_VISTO") = IIf(Nu(drItem("APROB_HORA_VISTO")) = "", "", Left(Nu(drItem("APROB_HORA_VISTO")), 2) + ":" + Right(Nu(drItem("APROB_HORA_VISTO")), 2))
            dr("APROB_FECHA_ASIGNADO") = IIf(Nu(drItem("APROB_FECHA_ASIGNADO")) = "", "", Right(Nu(drItem("APROB_FECHA_ASIGNADO")), 2) + " " + Nombre_Mes(Mid(Nu(drItem("APROB_FECHA_ASIGNADO")), 5, 2), False) & " " & Left(Nu(drItem("APROB_FECHA_ASIGNADO")), 4))
            dr("APROB_HORA_ASIGNADO") = FormatoHora(Nu(drItem("APROB_HORA_ASIGNADO")))
            If Nu(drItem("APROB_ASIGNADO_PERSONA")) = "" Then
                dr("PERSON_ASIG1") = ""
                dr("APROB_ASIGNADO_PERSONA") = ""
            Else
                dr("PERSON_ASIG1") = IIf(Nu(drItem("PERSON_ASIG1")) = "", Nu(drItem("PERSON_ASIG2")), Nu(drItem("PERSON_ASIG1")))
                dr("APROB_ASIGNADO_PERSONA") = Nu(drItem("APROB_ASIGNADO_PERSONA"))
            End If
            dr("APROB_FECHA_ASIGVISTO") = IIf(Nu(drItem("APROB_FECHA_ASIGVISTO")) = "", "", Right(Nu(drItem("APROB_FECHA_ASIGVISTO")), 2) + " " + Nombre_Mes(Mid(Nu(drItem("APROB_FECHA_ASIGVISTO")), 5, 2), False) & " " & Left(Nu(drItem("APROB_FECHA_ASIGVISTO")), 4))
            dr("APROB_HORA_ASIGVISTO") = FormatoHora(Nu(drItem("APROB_HORA_ASIGVISTO")))
            dr("APROB_FECHA_SOLUCION") = IIf(Nu(drItem("APROB_FECHA_SOLUCION")) = "", "", Right(Nu(drItem("APROB_FECHA_SOLUCION")), 2) + " " + Nombre_Mes(Mid(Nu(drItem("APROB_FECHA_SOLUCION")), 5, 2), False) & " " & Left(Nu(drItem("APROB_FECHA_SOLUCION")), 4))
            dr("APROB_HORA_SOLUCION") = FormatoHora(Nu(drItem("APROB_HORA_SOLUCION")))
            dr("PESTADO") = Nu(drItem("PESTADO"))
            dr("APROB_PRIORIDAD") = Nu(drItem("APROB_PRIORIDAD"))
            dr("NOM_PROB1") = Nu(drItem("NOM_PROB1")) & IIf(Nu(drItem("NOM_PROB2")) = "", "", " ; " + Nu(drItem("NOM_PROB2")))
            dr("APROB_PROBLEMA_DESCRIPCION") = Nu(drItem("APROB_PROBLEMA_DESCRIPCION"))
            dr("ECONFORME") = Nu(drItem("ECONFORME"))
            If Nu(drItem("APROB_ESTADO")) = "0" Then 'PROB CERRADO
                dr("NOM_PROB_ORIG") = Nu(drItem("NOM_PROB_ORIG"))
                dr("NOM_PROB_ORIG1") = Nu(drItem("NOM_PROB_ORIG1")) & IIf(Nu(drItem("NOM_PROB_ORIG2")) = "", "", " ; " + Nu(drItem("NOM_PROB_ORIG2")))
            End If
            If Nu(drItem("COLOR_CODIGO")) <> "" Then
                dr("COLOR_ROJO") = Nu(drItem("COLOR_ROJO"))
                dr("COLOR_VERDE") = Nu(drItem("COLOR_VERDE"))
                dr("COLOR_AZUL") = Nu(drItem("COLOR_AZUL"))
            End If
            dr("PERSONAL1") = IIf(Nu(drItem("PERSONAL1")) = "", Nu(drItem("PERSONAL2")), Nu(drItem("PERSONAL1")))
            dr("APROB_ESTADO") = Nu(drItem("APROB_ESTADO"))
            dt.Rows.Add(dr)
        Next
        dr = dt.NewRow
        dt.Rows.Add(dr)
        FlexProb.DataSource = dt
        FlexProb.DataBind()
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

        dt.Columns.Add("C1", GetType(String))
        dt.Columns.Add("C2", GetType(String))
        dt.Columns.Add("C3", GetType(String))
        dt.Columns.Add("C4", GetType(String))
        dt.Columns.Add("C5", GetType(String))
        dt.Columns.Add("C6", GetType(String))
        dt.Columns.Add("C7", GetType(String))
        dt.Columns.Add("C8", GetType(String))
        dt.Columns.Add("C9", GetType(String))
        dt.Columns.Add("C10", GetType(String))
        dt.Columns.Add("C11", GetType(String))
        dt.Columns.Add("C12", GetType(String))
        dt.Columns.Add("C13", GetType(String))
        dt.Columns.Add("C14", GetType(String))
        dt.Columns.Add("C15", GetType(String))
        dt.Columns.Add("C16", GetType(String))
        dt.Columns.Add("C17", GetType(String))
        dt.Columns.Add("C18", GetType(String))
        dt.Columns.Add("C19", GetType(String))
        dt.Columns.Add("C20", GetType(String))
        dt.Columns.Add("C21", GetType(String))
        dt.Columns.Add("cr", GetType(String))
        dt.Columns.Add("cv", GetType(String))
        dt.Columns.Add("ca", GetType(String))
        dt.Columns.Add("C22", GetType(String))
        dt.Columns.Add("C23", GetType(String))
        dt.Columns.Add("C24", GetType(String))
        Try
            Cn.Open()
            Dim Sql As String = "SELECT APROB_USUARIO_REPORTA,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From BDGRUPOEMPRESAS.DBO.TBPERSONAL WHERE PERSON_CODIGO = APROB_USUARIO_REPORTA) AS PERSONAL1," _
                                & "APROB_TIPO,NIVEL1_DESCRIP,P.COLOR_CODIGO,P.COLOR_ROJO,P.COLOR_VERDE,P.COLOR_AZUL,APROB_PROBLEMA1,(SELECT NIVEL2_DESCRIP FROM TBESP_PRO2 WHERE NIVEL2_CODIGO=APROB_PROBLEMA1) AS NOM_PROB1, " _
                                & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE USUARI_CODIGO = APROB_USUARIO_REPORTA AND LEFT(APROB_USUARIO_REPORTA,4)='1111') AS PERSONAL2, APROB_CODIGO, APROB_PRIORIDAD," _
                                & "APROB_PROBLEMA2,(SELECT NIVEL3_DESCRIP FROM TBESP_PRO3 WHERE NIVEL3_CODIGO=APROB_PROBLEMA2) AS NOM_PROB2 , APROB_PROBLEMA_DESCRIPCION," _
                                & "APROB_FECHA_REPORTA, APROB_HORA_REPORTA,APROB_ESTADO,APROB_SYS_EST,APROB_FECHA_SOLUCION,APROB_HORA_SOLUCION, " _
                                & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_ESTADO AND ELEMEN_TABLA='TBOPC185') AS PESTADO,APROB_FECHA_VISTO, APROB_HORA_VISTO, " _
                                & "(SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_CODIGO=APROB_CONFORMIDAD_USUARIOREP AND ELEMEN_TABLA='TBOPC057') AS ECONFORME, " _
                                & "APROB_FECHA_ASIGNADO,APROB_HORA_ASIGNADO,APROB_ASIGNADO_PERSONA,APROB_FECHA_ASIGVISTO,APROB_HORA_ASIGVISTO,(SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES FROM BDGRUPOEMPRESAS.DBO.TBPERSONAL WHERE PERSON_CODIGO=APROB_ASIGNADO_PERSONA) AS PERSON_ASIG1," _
                                & "(SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.DBO.TBUSUARI WHERE USUARI_CODIGO = APROB_ASIGNADO_PERSONA AND LEFT(APROB_ASIGNADO_PERSONA,4)='1111') AS PERSON_ASIG2, " _
                                & "APROB_TIPO_ORIG,(SELECT NIVEL1_DESCRIP FROM TBESP_PRO1 WHERE NIVEL1_CODIGO=APROB_TIPO_ORIG) AS NOM_PROB_ORIG,APROB_PROBLEMA1_ORIG,(SELECT NIVEL2_DESCRIP FROM TBESP_PRO2 WHERE NIVEL2_CODIGO=APROB_PROBLEMA1_ORIG) AS NOM_PROB_ORIG1,APROB_PROBLEMA2_ORIG,(SELECT NIVEL3_DESCRIP FROM TBESP_PRO3 WHERE NIVEL3_CODIGO=APROB_PROBLEMA2_ORIG) AS NOM_PROB_ORIG2 " _
                                & "From TBADMIN_PROBLEMAS AP INNER JOIN TBESP_PRO1 P ON NIVEL1_CODIGO=APROB_TIPO WHERE NOT(APROB_TIPO IS NULL) AND (AP.EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            If FechaIni <> "" Then
                If FechaIni <> "" And FechaFin <> "" Then
                    Sql = Sql & " AND APROB_FECHA_REPORTA between '" & FechaIni & "' and '" & FechaFin & "' "
                ElseIf FechaIni <> "" And FechaFin = "" Then
                    Sql = Sql & " AND APROB_FECHA_REPORTA = '" & FechaIni & "'"
                End If
            End If
            Sql = Sql & " AND (" & SqlEst & ")"
            If lblSeguridad.Text = "SR" Then  'solo reporta
                Sql = Sql & "AND (APROB_USUARIO_REPORTA='" & lblUsuarioCodigo.Text & "')"
            ElseIf lblSeguridad.Text = "SO" Then
                Sql = Sql & "AND (APROB_ASIGNADO_PERSONA='" & lblUsuarioCodigo.Text & "')"
            End If
            Sql = Sql & " AND (APROB_SYS_EST='0') "
            Sql = Sql & " ORDER BY APROB_FECHA_REPORTA+APROB_HORA_REPORTA DESC"
            Dim cmdSql As New SqlCommand(Sql, Cn)
            Rs = cmdSql.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    dr = dt.NewRow()
                    dr(0) = i.ToString
                    dr(1) = Format(Nz(Rs!APROB_CODIGO), "00000")
                    dr(2) = Right(Nu(Rs!APROB_FECHA_REPORTA), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_REPORTA), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_REPORTA), 4)
                    dr(3) = FormatoHora(Nu(Rs!APROB_HORA_REPORTA))
                    dr(4) = Nu(Rs!NIVEL1_DESCRIP)
                    dr(5) = IIf(Nu(Rs!APROB_FECHA_VISTO) = "", "", Right(Nu(Rs!APROB_FECHA_VISTO), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_VISTO), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_VISTO), 4))
                    dr(6) = IIf(Nu(Rs!APROB_HORA_VISTO) = "", "", Left(Nu(Rs!APROB_HORA_VISTO), 2) + ":" + Right(Nu(Rs!APROB_HORA_VISTO), 2))
                    dr(7) = IIf(Nu(Rs!APROB_FECHA_ASIGNADO) = "", "", Right(Nu(Rs!APROB_FECHA_ASIGNADO), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_ASIGNADO), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_ASIGNADO), 4))
                    dr(8) = FormatoHora(Nu(Rs!APROB_HORA_ASIGNADO))
                    If Nu(Rs!APROB_ASIGNADO_PERSONA) = "" Then
                        dr(9) = ""
                        dr(25) = ""
                    Else
                        dr(9) = IIf(Nu(Rs!PERSON_ASIG1) = "", Nu(Rs!PERSON_ASIG2), Nu(Rs!PERSON_ASIG1))
                        dr(25) = Nu(Rs!APROB_ASIGNADO_PERSONA)
                    End If
                    dr(10) = IIf(Nu(Rs!APROB_FECHA_ASIGVISTO) = "", "", Right(Nu(Rs!APROB_FECHA_ASIGVISTO), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_ASIGVISTO), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_ASIGVISTO), 4))
                    dr(11) = FormatoHora(Nu(Rs!APROB_HORA_ASIGVISTO))
                    dr(12) = IIf(Nu(Rs!APROB_FECHA_SOLUCION) = "", "", Right(Nu(Rs!APROB_FECHA_SOLUCION), 2) + " " + Nombre_Mes(Mid(Nu(Rs!APROB_FECHA_SOLUCION), 5, 2), False) & " " & Left(Nu(Rs!APROB_FECHA_SOLUCION), 4))
                    dr(13) = FormatoHora(Nu(Rs!APROB_HORA_SOLUCION))
                    dr(14) = Nu(Rs!PESTADO)
                    dr(15) = Nu(Rs!APROB_PRIORIDAD)
                    dr(16) = Nu(Rs!NOM_PROB1) & IIf(Nu(Rs!NOM_PROB2) = "", "", " ; " + Nu(Rs!NOM_PROB2))
                    dr(17) = Nu(Rs!APROB_PROBLEMA_DESCRIPCION)
                    dr(18) = Nu(Rs!ECONFORME)
                    If Nu(Rs!APROB_ESTADO) = "0" Then 'PROB CERRADO
                        dr(19) = Nu(Rs!NOM_PROB_ORIG)
                        dr(20) = Nu(Rs!NOM_PROB_ORIG1) & IIf(Nu(Rs!NOM_PROB_ORIG2) = "", "", " ; " + Nu(Rs!NOM_PROB_ORIG2))
                    End If
                    If Nu(Rs!COLOR_CODIGO) <> "" Then
                        dr(21) = Nu(Rs!COLOR_ROJO)
                        dr(22) = Nu(Rs!COLOR_VERDE)
                        dr(23) = Nu(Rs!COLOR_AZUL)
                    End If
                    dr(24) = IIf(Nu(Rs!PERSONAL1) = "", Nu(Rs!PERSONAL2), Nu(Rs!PERSONAL1))
                    dr(26) = Nu(Rs!APROB_ESTADO)
                    dt.Rows.Add(dr)
                End While
                Carga_Problemas = New DataView(dt)
            Else
                bolError = True
            End If
        Catch Ex As SqlException
            lblMensaje.Text = "Ha ocurrido un error" ' en el registro de la Base de Datos:<br>" & ex.Message
        Catch Ex As Exception
            lblMensaje.Text = "Ha ocurrido un error" ' la Aplicacion:<br>" & ex.Message
        Finally
            Cn.Close()
        End Try
        If bolError = True Then lblMensaje.Text = "No se encontraron problemas."
    End Function
    Private Sub Flex_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Flex.ItemCommand
        Dim Fecha As String = FechaActual()
        Dim Hora As String = HoraActual()
        Dim Visto As Boolean = False
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        'If e.CommandName = "MostrarAcciones" Then Exit Sub
        If e.CommandName <> "SiVisto" Then Exit Sub
        Cn.Open()
        CmdGlobal.Connection = Cn
        If e.Item.Cells(27).Text = "1" And lblSeguridad.Text = "TD" Then  'SI EL PROBLEMA ES NO VISTO Y VIGENTE
            CmdGlobal.CommandText = "UPDATE TBADMIN_PROBLEMAS SET APROB_ESTADO='2',APROB_FECHA_VISTO='" & Fecha & "', APROB_HORA_VISTO='" & Left(Hora, 4) & "' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND APROB_CODIGO=" & e.Item.Cells(5).Text
            If CmdGlobal.ExecuteNonQuery() <> 0 Then
                e.Item.Cells(27).Text = "2"
                e.Item.Cells(18).Text = "Abierto Visto"
                e.Item.Cells(9).Text = Right(Fecha, 2) + " " + Nombre_Mes(Mid(Fecha, 5, 2), False) & " " & Left(Fecha, 4)
                e.Item.Cells(10).Text = FormatoHora(Hora)
                Visto = True
            End If
        End If
        If e.Item.Cells(27).Text = "3" And (lblSeguridad.Text = "SO" Or (lblSeguridad.Text = "TD" And lblUsuarioCodigo.Text = e.Item.Cells(23).Text)) Then  'SI EL PROBLEMA ES NO VISTO Y VIGENTE
            CmdGlobal.CommandText = "UPDATE TBADMIN_PROBLEMAS SET APROB_ESTADO='4',APROB_FECHA_ASIGVISTO='" & Fecha & "', APROB_HORA_ASIGVISTO='" & Left(Hora, 4) & "' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND APROB_CODIGO=" & e.Item.Cells(5).Text
            If CmdGlobal.ExecuteNonQuery() <> 0 Then
                e.Item.Cells(27).Text = "4"
                e.Item.Cells(18).Text = "Asignado Visto"
                e.Item.Cells(14).Text = Right(Fecha, 2) + " " + Nombre_Mes(Mid(Fecha, 5, 2), False) & " " & Left(Fecha, 4)
                e.Item.Cells(15).Text = FormatoHora(Hora)
                Visto = True
            End If
        End If
        Cn.Close()
        If Visto = True Then
            Dim Fila As DataGridItem
            Fila = Flex.Items(e.Item.ItemIndex)
            Dim nSiVer As LinkButton = CType(Fila.FindControl("cmdVer"), LinkButton)
            If Not nSiVer Is Nothing Then nSiVer.Visible = False
            Dim nVisto As Label = CType(Fila.FindControl("lblVisto"), Label)
            If Not nVisto Is Nothing Then nVisto.Visible = True
        End If
    End Sub
    Private Sub Nuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nuevo.Click
        Response.Redirect("AdminProblemas_Reportar.aspx")
    End Sub
    Protected Sub FlexProb_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexProb.PageIndexChanging
        lblMensaje.Text = ""
        FlexProb.PageIndex = e.NewPageIndex
        Listar_Problemas()
    End Sub
    Protected Sub FlexProb_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexProb.RowCommand
        'Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        'Dim Cant As Integer : Cant = 0
        'Dim dt As New DataTable
        'Dim CodProv As Integer : CodProv = 0
        'Dim obj As New Listados
        'If e.CommandName = "Mostrar" Then
        '    lblCodPedido.Text = FlexProb.Rows(Index).Cells(1).Text
        '    Cant = 0
        '    Try
        '        If dt.Rows.Count > 0 Then
        '            For Each drMenuItem As Data.DataRow In dt.Rows
        '                If Nu(drMenuItem("ESTADO")) = "2" Then Cant = Cant + 1
        '            Next
        '        End If
        '        dt = Nothing
        '    Catch ex As SqlException
        '        lblMensaje2.Text = ex.Message
        '    Catch ex As Exception
        '        lblMensaje2.Text = ex.Message
        '    Finally
        '    End Try
        'End If
    End Sub
End Class
