Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class PersonalAgenda_Agenda_Entrevista
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call LlenaAno(cboEntAño)
            cboEntAño.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            cboEntPersonal.Items.Clear()
            txtEntFecha.Text = FormatoFecha(FechaActual)
            Call Llenar_Personal()
            FlexCita.DataSource = Nothing
            FlexCita.DataBind()
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
            btnCal.Visible = False
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.TabIndex = "0" Then
            txtEntFecha.Text = FormatoFecha(FechaActual)
            optEntTipo.SelectedValue = "0"
            optEntTipo_SelectedIndexChanged(sender, e)
        End If
        If Ficha.TabIndex = "1" Then
            chkBus1.Checked = False : chkBus2.Checked = False
            chkBus3.Checked = False : chkBus4.Checked = False : chkBus5.Checked = False
            cboBus1.Items.Clear() : cboBus2.Items.Clear()
            cboBus3.Items.Clear() : cboBus4.Items.Clear()
            cboBus1.Enabled = False : cboBus2.Enabled = False
            cboBus3.Enabled = False : cboBus4.Enabled = False
            txtBusFecha1.Text = FormatoFecha(FechaActual)
            txtBusFecha2.Text = FormatoFecha(FechaActual)
            txtBusFecha1.Enabled = False
            txtBusFecha2.Enabled = False
            cboBus1.Items.Add("< Seleccionar >") : cboBus1.SelectedValue = "< Seleccionar >"
            cboBus2.Items.Add("< Seleccionar >") : cboBus2.SelectedValue = "< Seleccionar >"
            cboBus3.Items.Add("< Seleccionar >") : cboBus3.SelectedValue = "< Seleccionar >"
            cboBus4.Items.Add("< Seleccionar >") : cboBus4.SelectedValue = "< Seleccionar >"
            FlexBus.DataSource = Nothing
            FlexBus.DataBind()
        End If
    End Sub
    Protected Sub btnBusListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusListar.Click
        lblHError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs2 As SqlClient.SqlDataReader
        Dim i As Long = 0
        Dim psFecha1 As String = Right(txtBusFecha1.Text, 4) + Mid(txtBusFecha1.Text, 4, 2) + Left(txtBusFecha1.Text, 2)
        Dim psFecha2 As String = Right(txtBusFecha2.Text, 4) + Mid(txtBusFecha2.Text, 4, 2) + Left(txtBusFecha2.Text, 2)
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            Cn2.Open() : cmdGlobal2.Connection = Cn2
            Dim dtListado As New DataTable
            Dim drT As DataRow
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            dtListado.Columns.Add("c5")
            dtListado.Columns.Add("c6")
            dtListado.Columns.Add("c7")
            dtListado.Columns.Add("c8")
            dtListado.Columns.Add("c9")
            dtListado.Columns.Add("c10")
            dtListado.Columns.Add("c11")
            dtListado.Columns.Add("c12")
            dtListado.Columns.Add("c13")
            dtListado.Columns.Add("c14")
            dtListado.Columns.Add("c15")
            cmdGlobal.CommandText = "SELECT ENT.ENT_NUMERAR, ENT.ENT_AREA,DA.AREA_NOMBRE, " _
                            & "ENT.ENT_PERSONAL_HACE_ENT, ENT.ENT_FECHA, ENT.ENT_HORA_INI,ENT.ENT_HORA_FIN, ENT.ENT_AQUIEN,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO = ENT.ENT_AQUIEN AND ELEMEN_TABLA = 'TBOPC206') AS NOM_QUIEN," _
                            & "ENT.ENT_TIPO1,(SELECT NIVEL1_DESCRIP FROM BDGEMPRESA" & Session("SiglaGrupoEmpresa") & ".dbo.TBESP_ENT1 WHERE NIVEL1_CODIGO = ENT.ENT_TIPO1) AS NOM_TIPO1,ENT.ENT_TIPO2,(SELECT NIVEL2_DESCRIP FROM BDGEMPRESA" & Session("SiglaGrupoEmpresa") & ".dbo.TBESP_ENT2 WHERE NIVEL2_CODIGO = ENT.ENT_TIPO2) AS NOM_TIPO2," _
                            & "ENT.ENT_TIPO3,(SELECT NIVEL3_DESCRIP FROM BDGEMPRESA" & Session("SiglaGrupoEmpresa") & ".dbo.TBESP_ENT3 WHERE NIVEL3_CODIGO = ENT.ENT_TIPO3) AS NOM_TIPO3,ENT.ENT_MODO,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO = ENT.ENT_MODO AND ELEMEN_TABLA = 'TBOPC203') AS NOM_MODO," _
                            & "ENT.ENT_ASUNTO, ENT.ENT_ACUERDO1, ENT.ENT_ACUERDO2,ENT.ENT_OBSERVACION, ENT_PROX_CITA, ENT_PROX_FECHA,ENT_PROX_HORA_INI, ENT_PROX_HORA_FIN, ENT_ESTADO," _
                            & "(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO = ENT.ENT_ESTADO AND ELEMEN_TABLA = 'TBOPC204') AS NOM_ESTADO," _
                            & "ENT.ENT_PERSONAL,(SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO=ENT_PERSONAL) AS NOM_PERSONAL," _
                            & "ENT.ENT_PUBLICO,ENT.ENT_NRO_CITA,ENT.ENT_MED_FECHA_SINTOMA,ENT.ENT_MED_DESCANSO,ENT.ENT_MED_FECHA_DES_INI,ENT.ENT_MED_FECHA_DES_FIN,ENT.ENT_PROX_NRO_CITA " _
                            & "FROM TBPERSONAL_ENTREVISTAS ENT INNER JOIN TBPERSONAL_DEFINE_AREA DA ON DA.AREA_CODIGO = ENT.ENT_AREA WHERE (ENT.ENT_AÑO='" & cboEntAño.SelectedValue.Trim & "') AND (ENT.ENT_PERSONAL_HACE_ENT='" & cboEntPersonal.SelectedValue.Trim & "') AND ENT.ENT_SYS_EST='0' AND DA.AREA_SYS_EST = '0' " _
                            & " AND DA.GRPOEMPRESA_CODIGO='" & Session("CodGrupoEmpresa") & "' AND DA.EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            If chkBus1.Checked = True Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENT_AQUIEN='" & cboBus1.SelectedValue.Trim & "')"
            If chkBus2.Checked = True Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENT_MODO='" & cboBus2.SelectedValue.Trim & "')"
            If chkBus3.Checked = True Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENT_TIPO1='" & cboBus3.SelectedValue.Trim & "')"
            If chkBus4.Checked = True Then
                If txtBusFecha2.Text = "" Then
                    cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENT_FECHA='" & psFecha1 & "')"
                Else
                    cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENT_FECHA BETWEEN '" & psFecha1 & "' AND '" & psFecha2 & "')"
                End If
            End If
            Dim psProxCita As String = ""
            Dim psParticipantes As String = ""
            If chkBus5.Checked = True Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENT_PERSONAL='" & cboBus4.SelectedValue.Trim & "')"
            cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY ENT_FECHA,ENT_HORA_INI,ENT_HORA_FIN,NOM_QUIEN"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = i
                    drT("c2") = Right(Rs!ENT_FECHA, 2) & Chr(13) & Nombre_Mes(Mid(Rs!ENT_FECHA, 5, 2), True) & Chr(13) & Left(Rs!ENT_FECHA, 4)
                    drT("c12") = Nu(Rs!AREA_NOMBRE)
                    drT("c3") = Nu(Rs!NOM_QUIEN)
                    drT("c13") = Nu(Rs!ENT_NUMERAR)
                    drT("c4") = "De " & Left(Nu(Rs!ENT_HORA_INI), 2) + ":" + Right(Nu(Rs!ENT_HORA_INI), 2) & " A " & Left(Nu(Rs!ENT_HORA_FIN), 2) + ":" + Right(Nu(Rs!ENT_HORA_FIN), 2)
                    If Nz(Rs!ENT_AQUIEN) = 5 Then
                        drT("c5") = Nu(Rs!NOM_PERSONAL) 'Nu(RsA!ENT_PERSONAL) + "   " +
                    ElseIf Nz(Rs!ENT_AQUIEN) = 3 Then
                        drT("c5") = Nu(Rs!ENT_PUBLICO)
                    End If
                    drT("c6") = Nu(Rs!NOM_MODO)
                    drT("c7") = Nu(Rs!NOM_TIPO1)
                    drT("c8") = Nu(Rs!ENT_ASUNTO)
                    drT("c9") = Nu(Rs!ENT_ACUERDO1)
                    If Nu(Rs!ENT_ACUERDO2) <> "" Then drT("c9") = Nu(Rs!ENT_ACUERDO1) & ". " & Nu(Rs!ENT_ACUERDO2)
                    If Nu(Rs!ENT_OBSERVACION) <> "" Then
                        drT("c14") = Nu(Rs!ENT_OBSERVACION)
                    End If
                    psProxCita = ""
                    If Nu(Rs!ENT_PROX_CITA) = "S" Then
                        psProxCita = "Día " & FormatoFecha(Nu(Rs!ENT_PROX_FECHA)) & " ;Horario " & Left(Nu(Rs!ENT_PROX_HORA_INI), 2) & ":" & Right(Nu(Rs!ENT_PROX_HORA_INI), 2) & " a " & Left(Nu(Rs!ENT_PROX_HORA_FIN), 2) & ":" & Right(Nu(Rs!ENT_PROX_HORA_FIN), 2)
                        cmdGlobal2.CommandText = "SELECT (SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA='TBOPC204' AND ELEMEN_CODIGO=AGEN_ESTADO) AS NOM_EST,AGEN_SYS_EST FROM TBPERSONAL_AGENDA WHERE AGEN_NRO_CITA='" & Nu(Rs!ENT_PROX_NRO_CITA) & "'"
                        Rs2 = cmdGlobal2.ExecuteReader
                        If Rs2.HasRows Then
                            While Rs2.Read
                                psProxCita = psProxCita & " - Cita " & IIf(Nu(Rs2!AGEN_SYS_EST) = "1", "Borrado", Nu(Rs2!NOM_EST))
                            End While
                        End If
                        Rs2.Close()
                    End If
                    drT("c15") = psProxCita
                    drT("c10") = IIf(Nu(Rs!ENT_NRO_CITA) = "", "Sin Cita / Libre", "Cita Programada")
                    psParticipantes = ""
                    cmdGlobal2.CommandText = "SELECT PART_PERSONA,PART_APELLIDOS,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC205' AND ELEMEN_CODIGO = PART_PERSONA) AS PERSONA " _
                    & " FROM TBPERSONAL_ENTREVISTAS_DET WHERE (ENT_NUMERAR = '" & Nu(Rs!ENT_NUMERAR) & "') ORDER BY PERSONA"
                    Rs2 = cmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            If psParticipantes <> "" Then psParticipantes = psParticipantes & "; "
                            psParticipantes = psParticipantes & Nu(Rs2!PERSONA) & " : " & Nu(Rs2!PART_APELLIDOS)
                        End While
                    End If
                    Rs2.Close()
                    drT("c11") = psParticipantes
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexBus.DataSource = dtListado
            FlexBus.DataBind()
        Catch ex As SqlException
            lblHError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblHError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub btnHistorial_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnHistorial.Click
        lblError.Text = ""
        If cboEntPersonal.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar al Personal para ver su Historial de Entrevista." : Exit Sub
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
        Ficha.TabIndex = "1"
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    Protected Sub chkBus1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBus1.CheckedChanged
        If chkBus1.Checked = False Then
            cboBus1.Enabled = False
            cboBus1.Items.Clear()
            cboBus1.Items.Add("< Seleccionar >") : cboBus1.SelectedValue = "< Seleccionar >"
        ElseIf chkBus1.Checked = True Then
            cboBus1.Enabled = True
            cboBus1.Items.Clear()
            Call LlenaComboItem("TBOPC206", cboBus1)
        End If
    End Sub
    Protected Sub chkBus2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBus2.CheckedChanged
        If chkBus2.Checked = False Then
            cboBus2.Enabled = False
            cboBus2.Items.Clear()
            cboBus2.Items.Add("< Seleccionar >") : cboBus2.SelectedValue = "< Seleccionar >"
        ElseIf chkBus2.Checked = True Then
            cboBus2.Enabled = True
            cboBus2.Items.Clear()
            Call LlenaComboItem("TBOPC203", cboBus2)
        End If
    End Sub
    Protected Sub chkBus3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBus3.CheckedChanged
        If chkBus3.Checked = False Then
            cboBus3.Enabled = False
            cboBus3.Items.Clear()
            cboBus3.Items.Add("< Seleccionar >") : cboBus3.SelectedValue = "< Seleccionar >"
        ElseIf chkBus3.Checked = True Then
            cboBus3.Enabled = True
            cboBus3.Items.Clear()
            Call LLenaComboItemTabEsp(cboBus3, "", "", "TBESP_ENT1", "TBESP_ENT2", "TBESP_ENT3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
        End If
    End Sub
    Protected Sub chkBus4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBus4.CheckedChanged
        If chkBus4.Checked = True Then
            txtBusFecha1.Enabled = True
            txtBusFecha2.Enabled = True
        Else
            txtBusFecha1.Enabled = False
            txtBusFecha2.Enabled = False
        End If
    End Sub
    Protected Sub chkBus5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkBus5.CheckedChanged
        If chkBus5.Checked = False Then
            cboBus4.Enabled = False
            cboBus4.Items.Clear()
            cboBus4.Items.Add("< Seleccionar >") : cboBus4.SelectedValue = "< Seleccionar >"
        ElseIf chkBus5.Checked = True Then
            cboBus4.Enabled = True
            cboBus4.Items.Clear()
            Call Llenar_EntPersonal()
        End If
    End Sub
    Private Sub Llenar_EntPersonal()
        lblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT DISTINCT ENT.ENT_PERSONAL, (SELECT (PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES) FROM TBPERSONAL WHERE PERSON_CODIGO=ENT.ENT_PERSONAL) AS NOM_PERSONAL " _
            & " FROM TBPERSONAL_ENTREVISTAS ENT INNER JOIN TBPERSONAL_DEFINE_AREA A ON ENT.ENT_AREA=A.AREA_CODIGO WHERE A.AREA_SYS_EST='0' AND A.GRPOEMPRESA_CODIGO='" & Session("CodGrupoEmpresa") & "' AND A.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND " _
            & " (ENT.ENT_AÑO = '" & cboEntAño.SelectedValue.Trim & "') AND (ENT.ENT_PERSONAL_HACE_ENT = '" & cboEntPersonal.SelectedValue.Trim & "') AND (ENT.ENT_AQUIEN = '5') AND (ENT.ENT_ESTADO = '1') AND (ENT.ENT_SYS_EST = '0')"
            Rs = cmdGlobal.ExecuteReader
            cboBus4.DataSource = Rs
            cboBus4.DataTextField = "NOM_PERSONAL"
            cboBus4.DataValueField = "ENT_PERSONAL"
            cboBus4.DataBind()
            Rs.Close()
            cboBus4.Items.Add("< Seleccionar >") : cboBus4.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub Llenar_Personal()
        lblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            cboEntPersonal.Items.Clear()
            cmdGlobal.CommandText = " SELECT DISTINCT PERSON_PERSONAL, " _
                                  & " (SELECT (PERSON_APEPAT + ' ' + PERSON_APEMAT +', ' + PERSON_NOMBRES) From TBPERSONAL WHERE PERSON_CODIGO = PERSON_PERSONAL) AS NOMBRE_PERSONAL " _
                                  & " FROM TBPERSONAL_AREAS PA INNER JOIN TBPERSONAL_DEFINE_AREA A ON PA.AREA_CODIGO=A.AREA_CODIGO " _
                                  & " WHERE A.GRPOEMPRESA_CODIGO = '" & Session("CodGrupoEmpresa") & "' AND A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' ORDER BY NOMBRE_PERSONAL"
            Rs = cmdGlobal.ExecuteReader
            cboEntPersonal.DataSource = Rs
            cboEntPersonal.DataTextField = "NOMBRE_PERSONAL"
            cboEntPersonal.DataValueField = "PERSON_PERSONAL"
            cboEntPersonal.DataBind()
            Rs.Close()
            cboEntPersonal.Items.Add("< Seleccionar >") : cboEntPersonal.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub optEntTipo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optEntTipo.SelectedIndexChanged
        If optEntTipo.SelectedIndex = "0" Then
            lblEntEtq3.Visible = False
            txtEntFecha.Visible = False
            btnCal.Visible = False
            btnEntNuevo.Visible = True
            FlexCita.DataSource = Nothing
            FlexCita.DataBind()
        Else
            lblEntEtq3.Visible = True
            txtEntFecha.Visible = True
            btnCal.Visible = True
            btnEntNuevo.Visible = False
            FlexCita.DataSource = Nothing
            FlexCita.DataBind()
            Call Listar_Citas()
        End If
    End Sub
    Protected Sub btnBusRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBusRegresar.Click
        lblHError.Text = ""
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.TabIndex = "0"
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    Protected Sub cboEntPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboEntPersonal.SelectedValue <> "< Seleccionar >" Then
            txtEntPersonal.Text = cboEntPersonal.SelectedValue.Trim
        Else
            txtEntPersonal.Text = ""
        End If
    End Sub
    Protected Sub Cal1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cal1.SelectionChanged
        txtEntFecha.Text = Cal1.SelectedDate
        lblCalendario.Visible = False
        Call Listar_Citas()
    End Sub
    Protected Sub btnCal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If lblCalendario.Visible = True Then
            lblCalendario.Visible = False
        ElseIf lblCalendario.Visible = False Then
            lblCalendario.Visible = True
        End If
    End Sub
    Private Sub Listar_Citas()
        lblError.Text = ""
        If cboEntPersonal.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar al Personal para listar las Citas." : Exit Sub
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim i As Long = 0
        Dim NomUsuario As String = ""
        Dim psObs As String = ""
        Dim psFechaCita As String = Right(txtEntFecha.Text, 4) + Mid(txtEntFecha.Text, 4, 2) + Left(txtEntFecha.Text, 2)
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            Dim dtListado As New DataTable
            Dim drT As DataRow
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            dtListado.Columns.Add("c5")
            dtListado.Columns.Add("c6")
            dtListado.Columns.Add("c7")
            dtListado.Columns.Add("c8")
            dtListado.Columns.Add("c9")
            cmdGlobal.CommandText = " SELECT AGEN.AGEN_NRO_CITA, AGEN_TIPOPER, AGEN.AGEN_FECHA, AGEN.AGEN_HORA_INI,AGEN.AGEN_HORA_FIN, AGEN.AGEN_AREA,DA.AREA_NOMBRE,AGEN.AGEN_MODO_CITA," _
                                  & " AGEN.AGEN_TIPO_ATENCION,(SELECT elemen_valor From TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC202' AND ELEMEN_CODIGO = AGEN.AGEN_TIPO_ATENCION) AS TIPOAT, " _
                                  & " AGEN.AGEN_PERSONAL_CODIGO,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From TBPERSONAL WHERE PERSON_CODIGO = AGEN.AGEN_PERSONAL_CODIGO AND (AGEN.AGEN_TIPO_ATENCION = '5' OR AGEN.AGEN_GRAL_SUBTIPO_ATEN = '5')) AS NOMBRE_PERSONAL, AGEN.AGEN_PUBLICO_NOMBRE," _
                                  & " AGEN.AGEN_GRAL_SUBTIPO_ATEN, AGEN.AGEN_ASUNTO,AGEN.AGEN_OBSERVACION,AGEN.AGEN_COMPORT,AGEN.AGEN_ESTADO,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO=AGEN.AGEN_ESTADO AND ELEMEN_TABLA='TBOPC204') AS AAESTADO,AGEN.AGEN_CITA_REPROG," _
                                  & " (SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From TBPERSONAL WHERE PERSON_CODIGO = AGEN_USUARIO AND AGEN.AGEN_TIPOUSU='1') AS NOM_USUARIO1," _
                                  & " (SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.dbo.TBUSUARI WHERE USUARI_CODIGO = AGEN.AGEN_USUARIO AND USUARI_TIPO=AGEN.AGEN_TIPOUSU) AS NOM_USUARIO2," _
                                  & " (SELECT ELEMEN_VALOR From TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC203' AND ELEMEN_CODIGO = AGEN.AGEN_MODO_CITA) AS MODO_CITA,AGEN.AGEN_COMPORT " _
                                  & " From TBPERSONAL_AGENDA AGEN INNER JOIN TBPERSONAL_DEFINE_AREA DA ON DA.AREA_CODIGO = AGEN.AGEN_AREA " _
                                  & " WHERE (AGEN.AGEN_PERSONAL = '" & txtEntPersonal.Text.Trim & "') AND (AGEN.AGEN_SYS_EST = '0') AND (AGEN.AGEN_AÑO = '" & cboEntAño.SelectedValue.Trim & "') AND (AGEN.AGEN_FECHA = '" & psFechaCita & "') AND (AGEN.AGEN_ESTADO = '0' OR AGEN.AGEN_ESTADO='2') AND DA.AREA_SYS_EST = '0'" _
                                  & " AND DA.GRPOEMPRESA_CODIGO = '" & Session("CodGrupoEmpresa") & "' AND DA.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' ORDER BY AGEN.AGEN_HORA_INI,TIPOAT"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c1") = i
                    drT("c2") = Nu(Rs!AGEN_NRO_CITA)
                    drT("c3") = Left(Nu(Rs!AGEN_HORA_INI), 2) + ":" + Right(Nu(Rs!AGEN_HORA_INI), 2) & Chr(13) & Left(Nu(Rs!AGEN_HORA_FIN), 2) + ":" + Right(Nu(Rs!AGEN_HORA_FIN), 2) & Chr(13) & Nu(Rs!AAESTADO)
                    drT("c4") = Nu(Rs!AREA_NOMBRE)
                    drT("c5") = Nu(Rs!TIPOAT)
                    If Nz(Rs!AGEN_TIPO_ATENCION) = 5 Then
                        NomUsuario = Nu(Rs!AGEN_PERSONAL_CODIGO) + "   " + Nu(Rs!Nombre_personal)
                    ElseIf Nz(Rs!AGEN_TIPO_ATENCION) = 3 Then
                        NomUsuario = Nu(Rs!AGEN_PUBLICO_NOMBRE)
                    ElseIf Nz(Rs!AGEN_TIPO_ATENCION) = 4 Or (Nz(Rs!AGEN_TIPO_ATENCION) >= 6 And Nz(Rs!AGEN_TIPO_ATENCION) <= 11) Then
                        If Nu(Rs!AGEN_GRAL_SUBTIPO_ATEN) = "5" Then
                            NomUsuario = Nu(Rs!AGEN_PERSONAL_CODIGO) + "   " + Nu(Rs!Nombre_personal)
                        ElseIf Nu(Rs!AGEN_GRAL_SUBTIPO_ATEN) = "3" Then
                            NomUsuario = Nu(Rs!AGEN_PUBLICO_NOMBRE)
                        End If
                    End If
                    drT("c6") = NomUsuario
                    drT("c7") = Nu(Rs!AGEN_ASUNTO)
                    drT("c8") = Nu(Rs!MODO_CITA) ' & IIf(Nu(RsA!AGEN_COMPORT) = "S", " [PROB. COMPORTAMIENTO]", "")
                    psObs = Nu(Rs!AGEN_OBSERVACION)
                    If Nu(Rs!AGEN_CITA_REPROG) = "S" Then psObs = psObs & " (Cita Reprogramada)"
                    drT("c9") = Nu(Rs!AGEN_OBSERVACION)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexCita.DataSource = dtListado
            FlexCita.DataBind()
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub btnEntNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboEntPersonal.SelectedValue = "< Seleccionar >" Then lblError.Text = "Seleccionar al Personal para efectuar la Entrevista." : Exit Sub
        Call Limpiar()
        lblError.Text = ""
        Dim obj As New ModuloSeguridad
        Try
            Call LlenarCajas_Personal()
            Call Llenar_Area()
            lblRError.Text = ""
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
            Ficha.TabIndex = "2"
            Ficha_ActiveTabChanged(sender, e)
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub LlenarCajas_Personal()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Cn.Open() : cmdGlobal.Connection = Cn
        cmdGlobal.CommandText = " SELECT person_codigo, person_apepat + ' ' + person_apemat + ', ' + person_nombres AS NOMBRE_PERSONAL" _
                              & " From TBPERSONAL WHERE person_codigo = '" & cboEntPersonal.SelectedValue.Trim & "'   "
        cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY NOMBRE_PERSONAL"
        Rs = cmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                txtRPersonal.Text = Nu(Rs!NOMBRE_PERSONAL)
                txtRCodPersonal.Text = Nu(Rs!PERSON_CODIGO)
            End While
        End If
        Rs.Close()
    End Sub
    Private Sub Llenar_Area()
        cboRArea.Items.Clear()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Cn.Open() : cmdGlobal.Connection = Cn
        cmdGlobal.CommandText = " SELECT DA.AREA_CODIGO, DA.AREA_NOMBRE " _
                              & " FROM dbo.TBPERSONAL_AREAS  AREAS " _
                              & " INNER JOIN dbo.TBPERSONAL_DEFINE_AREA  DA " _
                              & " ON DA.AREA_CODIGO = AREAS.AREA_CODIGO " _
                              & " WHERE (AREAS.PERSON_PERSONAL = '" & cboEntPersonal.SelectedValue.Trim & "') " _
                              & " AND (AREAS.PERSON_AREA_SYS_EST = '0') " _
                              & " AND (DA.AREA_SYS_EST = '0') " _
                              & " AND (DA.GRPOEMPRESA_CODIGO ='" & Session("CodGrupoEmpresa") & "') " _
                              & " AND (DA.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
        Rs = cmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                Dim Item As New ListItem
                Item.Text = Nu(Rs!AREA_NOMBRE).ToString
                Item.Value = Nu(Rs!AREA_CODIGO).ToString
                cboRArea.Items.Add(Item)
            End While
        End If
        Rs.Close()
        cboRArea.Items.Add("< Seleccionar >") : cboRArea.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub Limpiar()
        txtRFecha.Enabled = True
        txtRCodCita.Text = "" : txtREstadoCita.Text = ""
        lblRError.Text = "" : txtRPersonal.Text = "" : txtRApePat.Text = ""
        txtRApeMat.Text = "" : txtRNombres.Text = "" : txtREmpresa.Text = ""
        txtRFecha.Text = "" : txtRAsunto.Text = "" : txtRAcuerdo.Text = ""
        txtRObs.Text = "" : txtRParticipante.Text = "" : txtBusApePat.Text = ""
        txtRGrabar.Text = "" : txtRCodArea.Text = "" : txtRCodPersonal.Text = ""
        txtRCodRazon.Text = "" : txtRComienza.Text = "07:00" : txtRTermina.Text = "07:00"
        txtRApePat.ReadOnly = True : txtRApeMat.ReadOnly = True : cboRTipoPer.Enabled = False
        txtRNombres.ReadOnly = True : txtREmpresa.ReadOnly = True
        cboRTipoPer.Items.Clear() : cboRArea.Items.Clear()
        cboREnt1.Items.Clear() : cboREnt2.Items.Clear() : cboREnt3.Items.Clear()
        cboRParticipante.Items.Clear() : cboBusTipoPer.Items.Clear() : cboREnt.Items.Clear()
        Call LlenaComboItem("TBOPC206", cboREnt)
        Call LlenaComboItem("TBOPC001", cboRTipoPer)
        Call LlenaComboItem("TBOPC001", cboBusTipoPer)
        Call LlenaComboItem("TBOPC205", cboRParticipante)
        Call LLenaComboItemTabEsp(cboREnt1, "", "", "TBESP_ENT1", "TBESP_ENT2", "TBESP_ENT3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
        cboRModoEnt.SelectedValue = "(Seleccionar)"
        cboRArea.Items.Add("< Seleccionar >") : cboRArea.SelectedValue = "< Seleccionar >"
        cboREnt2.Items.Add("< Seleccionar >") : cboREnt2.SelectedValue = "< Seleccionar >"
        cboREnt3.Items.Add("< Seleccionar >") : cboREnt3.SelectedValue = "< Seleccionar >"
        chkEntrevistador.Checked = False : chkEntrevistado.Checked = False
        FlexParticipante.DataSource = Nothing : FlexParticipante.DataBind()
        FlexP.DataSource = Nothing : FlexP.DataBind()
        txtRFecha.Text = FormatoFecha(FechaActual)
        btnRBuscar.Enabled = False
    End Sub
    Protected Sub btnRCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblHError.Text = ""
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.TabIndex = "0"
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    Protected Sub cboREnt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        cboRTipoPer.Enabled = False : cboRTipoPer.SelectedValue = "< Seleccionar >"
        txtRApePat.ReadOnly = True : txtRApeMat.ReadOnly = True
        txtRNombres.ReadOnly = True : txtREmpresa.ReadOnly = True
        txtRAsunto.Text = "" : txtRObs.Text = "" : txtRCodRazon.Text = ""
        txtREmpresa.Text = "" : txtBusApePat.Text = "" : txtRGrabar.Text = ""
        lblRError.Text = "" : txtRApePat.Text = ""
        txtRApeMat.Text = "" : txtRNombres.Text = "" : txtREmpresa.Text = ""
        cboRModoEnt.SelectedValue = "(Seleccionar)"
        cboBusTipoPer.SelectedValue = "< Seleccionar >"
        cboBusTipoPer.Enabled = False
        btnRBuscar.Enabled = False
        If cboREnt.SelectedValue = "5" Then 'personal
            lblBP1.Text = "Listado del Personal"
            btnRBuscar.Enabled = True
        ElseIf cboREnt.SelectedValue = "3" Then
            cboRTipoPer.Enabled = True : cboRTipoPer.SelectedValue = "< Seleccionar >"
            txtRApePat.ReadOnly = False : txtRApeMat.ReadOnly = False
            txtRNombres.ReadOnly = False : txtREmpresa.ReadOnly = False
            cboRModoEnt.SelectedValue = "(Seleccionar)" : cboBusTipoPer.Enabled = True
            cboBusTipoPer.SelectedValue = "< Seleccionar >"
            lblBP1.Text = "Listado de Personas"
            btnRBuscar.Enabled = True
        End If
    End Sub
    Protected Sub btnBPListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboREnt.SelectedValue = "3" Then
            Call Lista_Persona()
            ModalPopupExtender1.Show()
        ElseIf cboREnt.SelectedValue = "5" Then
            Call Listar_Personal()
            ModalPopupExtender1.Show()
        End If
    End Sub
    Private Sub Lista_Persona()
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            dtListado.Columns.Add("PERSON_CODIGO")
            dtListado.Columns.Add("TIPO_PER")
            dtListado.Columns.Add("PERSON_APEPAT")
            dtListado.Columns.Add("PERSON_APEMAT")
            dtListado.Columns.Add("PERSON_NOMBRES")
            dtListado.Columns.Add("EMPRESA")
            dtListado.Columns.Add("TIPO_CODPER")
            Cn.Open() : Cn2.Open()
            cmdGlobal.Connection = Cn : cmdGlobal2.Connection = Cn2
            cmdGlobal.CommandText = " SELECT DATOPER_CODIGO, DATOPER_TIPO, DATOPER_EMPRESA , DATOPER_APEPAT, DATOPER_APEMAT, DATOPER_NOMBRES, DATOPER_TIPO_DOC, DATOPER_NRO_DOC, " _
                & " (SELECT ELEMEN_VALOR From dbo.TBCELEMEN WHERE (ELEMEN_CODIGO = DP.DATOPER_TIPO) AND (ELEMEN_TABLA = 'TBOPC001')) AS TIPOPERSONA, " _
                & " (SELECT ELEMEN_VALOR FROM dbo.TBCELEMEN WHERE (ELEMEN_TABLA = 'TBOPC009') AND (ELEMEN_CODIGO = DP.DATOPER_TIPO_DOC)) AS TIPODOCIDE " _
                & " FROM dbo.TBVISITAS_DATAPERSONA AS DP WHERE (DATOPER_SYS_EST = '0') "
            If cboBusTipoPer.SelectedValue <> "< Seleccionar >" Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND DATOPER_TIPO = '" & cboBusTipoPer.SelectedValue.Trim & "'"
            If txtBusApePat.Text.Trim <> "" Then cmdGlobal.CommandText = cmdGlobal.CommandText & " AND UPPER(DATOPER_APEPAT) LIKE '" & UCase(txtBusApePat.Text.Trim) & "%'"
            cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY DATOPER_EMPRESA , DATOPER_APEPAT, DATOPER_APEMAT, DATOPER_NOMBRES"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("PERSON_CODIGO") = Nu(Rs!DATOPER_CODIGO)
                    drT("TIPO_PER") = Nu(Rs!tipopersona)
                    drT("PERSON_APEPAT") = Nu(Rs!DATOPER_APEPAT)
                    drT("PERSON_APEMAT") = Nu(Rs!DATOPER_APEMAT)
                    drT("PERSON_NOMBRES") = Nu(Rs!DATOPER_NOMBRES)
                    drT("EMPRESA") = Nu(Rs!DATOPER_EMPRESA)
                    drT("TIPO_CODPER") = Nu(Rs!DATOPER_TIPO)
                    dtListado.Rows.Add(drT)
                End While
            End If
            Rs.Close()
            FlexP.DataSource = dtListado
            FlexP.DataBind()
        Catch ex As SqlException
            lblRError.Text = ex.Message
        Catch Ex As Exception
            lblRError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Listar_Personal()
        Dim psCodGrupo As Double = 0
        psCodGrupo = Session("CodGrupoEmpresa")
        Try
            'Dim obj As New clsControlPersonal
            'FlexP.DataSource = obj.Listar_Personal("0", "00", txtBusApePat.Text.Trim, "", "", psCodGrupo, Session("CodEmpresa"), "1")
            'FlexP.SelectedIndex = -1
            'FlexP.DataBind()
        Catch ex As SqlException
            lblRError.Text = ex.Message
        Catch Ex As Exception
            lblRError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        txtRGrabar.Text = ""
        lblRError.Text = ""
        'If FlexP.Rows(Index).Cells(2).Text = "SI" Then lblPError.Text = "El personal escogido ya se encuentra registrado como usuario del sistema." : Exit Sub
        If e.CommandName = "Aceptar" Then
            If cboREnt.SelectedValue = "3" Then
                If FlexP.Rows(Index).Cells(1).Text <> "&nbsp;" Then txtRCodRazon.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(3).Text <> "&nbsp;" Then txtRApePat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(4).Text <> "&nbsp;" Then txtRApeMat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtRNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(6).Text <> "&nbsp;" Then txtREmpresa.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(7).Text <> "&nbsp;" Then cboRTipoPer.SelectedValue = UCase(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"))
                ModalPopupExtender1.Hide()
                cboBusTipoPer.SelectedValue = "< Seleccionar >"
                txtBusApePat.Text = ""
                txtRGrabar.Text = "N"
                FlexP.DataSource = Nothing
                FlexP.DataBind()
            ElseIf cboREnt.SelectedValue = "5" Then
                If FlexP.Rows(Index).Cells(1).Text <> "&nbsp;" Then txtRCodRazon.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(3).Text <> "&nbsp;" Then txtRApePat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(4).Text <> "&nbsp;" Then txtRApeMat.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If FlexP.Rows(Index).Cells(5).Text <> "&nbsp;" Then txtRNombres.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexP.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                ModalPopupExtender1.Hide()
                cboBusTipoPer.SelectedValue = "< Seleccionar >"
                txtBusApePat.Text = ""
                txtRGrabar.Text = ""
                FlexP.DataSource = Nothing
                FlexP.DataBind()
            End If
        End If
    End Sub
    Protected Sub cboRArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblRError.Text = ""
        If cboRArea.SelectedValue <> "< Seleccionar >" Then
            txtRCodArea.Text = cboRArea.SelectedValue.Trim
        Else
            txtRCodArea.Text = ""
        End If
    End Sub
    Protected Sub btnRGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblRError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim NroMin As Long = 0
        Dim NroEnt As String = ""
        Dim NomPublico As String = ""
        Dim CodPersonal As String = ""
        Dim CodAlumno As String = ""
        Dim SubTipo As String = ""
        Dim Tipo3 As String = ""
        Dim ValorSys As String = FechaActual() + HoraActual() + HttpContext.Current.User.Identity.Name
        Dim psAño As String = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
        Dim psHoraIni As String = ""
        Dim psHoraFin As String = ""
        Dim psFechaEnt As String = ""
        Dim psNroCita As String = ""
        Dim i As Long = 0
        Dim psCodDato As String = ""
        Try
            If cboRArea.SelectedValue.Trim = "< Seleccionar >" Then lblRError.Text = "<br> - Debe de escoger de que área proviene la Entrevista"
            If cboREnt.SelectedValue.Trim = "< Seleccionar >" Then lblRError.Text = lblRError.Text & "<br> - Debe de existir con Quién se Entrevista"
            NroMin = DateDiff("n", txtRComienza.Text, txtRTermina.Text)
            If NroMin <= 0 Then lblRError.Text = lblRError.Text & "<br> - Verificar los intervalos de Tiempo, deben de existir entre ellos por lo menos un minuto y" & Chr(13) & "la hora de Inicio no debe ser menor a la Hora Termino"
            If cboRModoEnt.SelectedValue.Trim = "(Seleccionar)" Then lblRError.Text = lblRError.Text & "<br> - Debe de existir en que Modo se da la Entrevista"
            If cboREnt1.SelectedValue.Trim = "< Seleccionar >" Then lblRError.Text = lblRError.Text & "<br> - Es necesario saber el Tipo de Entrevista"
            If cboREnt2.SelectedValue.Trim = "< Seleccionar >" Then lblRError.Text = lblRError.Text & "<br> - Es necesario saber el Asunto Específico de la Entrevista"
            If txtRAsunto.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Debe de ingresar obligatoriamente la descripción del Asunto de la Entrevista"
            If txtRAcuerdo.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Debe de ingresar obligatoriamente el ó los acuerdos llegados en la Entrevista"
            If FlexParticipante.Rows.Count < 1 Then lblRError.Text = lblRError.Text & "<br> - En la Entrevista es indispensable tener como mínimo un participante, por favor de ingresar"
            If cboREnt.SelectedValue.Trim = "1" Or cboREnt.SelectedValue.Trim = "2" Then
                NomPublico = "NULL"
                CodPersonal = "NULL"
            ElseIf cboREnt.SelectedValue.Trim = "3" Then
                If txtRApePat.Text.Trim = "" Or txtRApeMat.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Debe de ingresar los Apellidos de la Persona para la Entrevista"
                If txtRNombres.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Debe de ingresar los Nombres de la Persona para la Entrevista"
                NomPublico = txtRApePat.Text.Trim & " " & txtRApeMat.Text.Trim & ", " & txtRNombres.Text.Trim
                CodPersonal = txtRCodRazon.Text.Trim
            ElseIf cboREnt.SelectedValue.Trim = "5" Then
                If txtRCodPersonal.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Debe de seleccionar al personal para la Entrevista"
                NomPublico = "NULL"
                CodPersonal = txtRCodRazon.Text.Trim
            End If
            If lblRError.Text.Trim <> "" Then
                lblRError.Text = "Exiten las sgtes. observaciones: " & lblRError.Text.Trim
                Exit Sub
            End If
            If cboREnt3.SelectedValue.Trim = "< Seleccionar >" Then Tipo3 = "NULL" Else Tipo3 = cboREnt3.SelectedValue.Trim
            Cn.Open() : cmdGlobal.Connection = Cn
            Cn2.Open() : cmdGlobal2.Connection = Cn2
            cmdGlobal.CommandText = " SELECT MAX(ENT_NUMERAR) FROM TBPERSONAL_ENTREVISTAS "
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    NroEnt = Nz(Rs(0)) + 1
                End While
            Else
                NroEnt = "1"
            End If
            Rs.Close()
            psHoraIni = Left(txtRComienza.Text.Trim, 2) & Right(txtRComienza.Text.Trim, 2)
            psHoraFin = Left(txtRTermina.Text.Trim, 2) & Right(txtRTermina.Text.Trim, 2)
            psFechaEnt = Right(txtRFecha.Text.Trim, 4) & Mid(txtRFecha.Text.Trim, 4, 2) & Left(txtRFecha.Text.Trim, 2)
            psNroCita = txtRCodCita.Text.Trim
            cmdGlobal.CommandText = " INSERT INTO TBPERSONAL_ENTREVISTAS (ENT_AÑO,ENT_NUMERAR, ENT_AREA,ENT_PERSONAL_HACE_ENT, ENT_FECHA, ENT_HORA_INI,ENT_HORA_FIN, ENT_AQUIEN, ENT_TIPO1, ENT_TIPO2, " _
                                  & " ENT_TIPO3, ENT_MODO, ENT_ASUNTO, ENT_ACUERDO1, ENT_OBSERVACION,ENT_PROX_CITA,ENT_PROX_FECHA,ENT_PROX_HORA_INI, ENT_PROX_HORA_FIN, ENT_ESTADO," _
                                  & " ENT_SYS_EST, ENT_SYS_CRE,ENT_PUBLICO, ENT_PERSONAL, ENT_NOMBRES, ENT_APEPAT, ENT_APEMAT, ENT_EMPRESA) " _
                                  & " VALUES('" & psAño & "'," & NroEnt & "," & cboRArea.SelectedValue.Trim & ",'" & txtRCodPersonal.Text.Trim & "','" & psFechaEnt & "'," _
                                  & " '" & psHoraIni & "','" & psHoraFin & "','" & cboREnt.SelectedValue.Trim & "'," & cboREnt1.SelectedValue.Trim & "," _
                                  & " " & cboREnt2.SelectedValue.Trim & "," & Tipo3 & ",'" & cboRModoEnt.SelectedValue.Trim & "','" & txtRAsunto.Text.Trim & "','" & txtRAcuerdo.Text.Trim & "'," _
                                  & " '" & txtRObs.Text.Trim & "','N',''," _
                                  & " '','','1','0','" & ValorSys & "'," _
                                  & " '" & NomPublico & "','" & CodPersonal & "', '" & txtRNombres.Text.Trim & "', '" & txtRApePat.Text.Trim & "', '" & txtRApeMat.Text.Trim & "', '" & txtREmpresa.Text.Trim & "')"
            cmdGlobal.ExecuteNonQuery()
            If psNroCita <> "" Then
                cmdGlobal.CommandText = " UPDATE TBPERSONAL_ENTREVISTAS SET ENT_NRO_CITA = " & psNroCita & " WHERE ENT_NUMERAR = " & NroEnt
                cmdGlobal.ExecuteNonQuery()
                cmdGlobal.CommandText = " UPDATE TBPERSONAL_AGENDA SET AGEN_ESTADO='" & IIf(txtREstadoCita.Text.Trim = "0", "1", IIf(txtREstadoCita.Text.Trim = "2", "5", "1")) & "'  WHERE  AGEN_NRO_CITA='" & psNroCita & "'"
                cmdGlobal.ExecuteNonQuery()
            End If
            cmdGlobal.CommandText = "DELETE FROM TBPERSONAL_ENTREVISTAS_DET WHERE ENT_NUMERAR='" & NroEnt & "'"
            cmdGlobal.ExecuteNonQuery()
            With FlexParticipante
                For i = 0 To .Rows.Count - 1
                    cmdGlobal.CommandText = "INSERT INTO TBPERSONAL_ENTREVISTAS_DET (ENT_NUMERAR,PART_PERSONA,PART_APELLIDOS) VALUES('" & NroEnt & "','" & .Rows(i).Cells(4).Text & "','" & .Rows(i).Cells(3).Text & "')"
                    cmdGlobal.ExecuteNonQuery()
                Next
            End With
            If txtRGrabar.Text = "N" And cboREnt.SelectedValue.Trim = "3" And txtRCodRazon.Text.Trim <> "" Then
                cmdGlobal.CommandText = " SELECT * FROM TBVISITAS_DATAPERSONA " _
                    & " WHERE DATOPER_CODIGO = " & txtRCodRazon.Text.Trim & " AND DATOPER_SYS_EST = '0' " _
                    & " AND DATOPER_TIPO = '" & cboRTipoPer.SelectedValue.Trim & "'"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        cmdGlobal2.CommandText = " UPDATE TBVISITAS_DATAPERSONA SET " _
                                              & " DATOPER_EMPRESA = '" & txtREmpresa.Text.Trim & "', " _
                                              & " DATOPER_APEPAT = '" & txtRApePat.Text.Trim & "', " _
                                              & " DATOPER_APEMAT = '" & txtRApeMat.Text.Trim & "', " _
                                              & " DATOPER_NOMBRES = '" & txtRNombres.Text.Trim & "', " _
                                              & " WHERE DATOPER_CODIGO = " & txtRCodRazon.Text.Trim & " AND DATOPER_SYS_EST = '0' " _
                                              & " AND DATOPER_TIPO = '" & cboRTipoPer.SelectedValue.Trim & "'"
                        cmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
            ElseIf txtRGrabar.Text = "" And cboREnt.SelectedValue.Trim = "3" Then
                cmdGlobal.CommandText = " SELECT MAX(DATOPER_CODIGO) FROM TBVISITAS_DATAPERSONA "
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodDato = Nz(Rs(0)) + 1
                    End While
                Else
                    psCodDato = 1
                End If
                Rs.Close()
                cmdGlobal.CommandText = " INSERT INTO TBVISITAS_DATAPERSONA (DATOPER_CODIGO, DATOPER_TIPO,  " _
                                      & " DATOPER_EMPRESA, DATOPER_APEPAT, DATOPER_APEMAT, DATOPER_NOMBRES, DATOPER_SYS_EST, DATOPER_SYS_CRE)  VALUES " _
                                      & " (" & psCodDato & ", '" & cboRTipoPer.SelectedValue.Trim & "',  " _
                                      & " '" & txtREmpresa.Text.Trim & "', '" & txtRApePat.Text.Trim & "', '" & txtRApeMat.Text.Trim & "', '" & txtRNombres.Text.Trim & "', '0', '" & ValorSys & "')"
                cmdGlobal.ExecuteNonQuery()
                cmdGlobal.CommandText = " UPDATE TBPERSONAL_ENTREVISTAS SET ENT_PERSONAL = " & psCodDato & " " _
                                      & " WHERE ENT_NUMERAR = " & NroEnt
                cmdGlobal.ExecuteNonQuery()
            End If
            btnRCancelar_Click(sender, e)
        Catch ex As SqlException
            lblRError.Text = ex.Message
        Catch Ex As Exception
            lblRError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnBPCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        cboBusTipoPer.SelectedValue = "< Seleccionar >"
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub cboRTipoPer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'txtRApePat.Text = "" : txtRApeMat.Text = ""
        'txtRNombres.Text = "" : txtREmpresa.Text = ""
        'txtRCodRazon.Text = ""
        'cboRTipoPer.Enabled = False
        'cboRTipoPer.SelectedValue = "< Seleccionar >"
        'If cboRTipoPer.SelectedValue.Trim = "3" Then
        '    cboRTipoPer.Enabled = True
        '    txtRApePat.ReadOnly = False : txtRApeMat.ReadOnly = False
        '    txtRNombres.ReadOnly = False : txtREmpresa.ReadOnly = False
        'ElseIf cboRTipoPer.SelectedValue.Trim = "5" Then
        '    txtRApePat.ReadOnly = True : txtRApeMat.ReadOnly = True
        '    txtRNombres.ReadOnly = True : txtREmpresa.ReadOnly = True
        'End If
    End Sub
    Protected Sub cboREnt1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Visible = False
        cboREnt2.Items.Clear()
        cboREnt3.Items.Clear()
        cboREnt2.Items.Add("< Seleccionar >") : cboREnt2.SelectedValue = "< Seleccionar >"
        cboREnt3.Items.Add("< Seleccionar >") : cboREnt3.SelectedValue = "< Seleccionar >"
        If cboREnt1.SelectedValue = "< Seleccionar >" Then Exit Sub
        Call LLenaComboItemTabEsp(cboREnt2, cboREnt1.SelectedValue.Trim, "", "TBESP_ENT1", "TBESP_ENT2", "TBESP_ENT3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
    End Sub
    Protected Sub cboREnt2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        cboREnt3.Items.Clear()
        If cboREnt1.SelectedValue = "< Seleccionar >" Or cboREnt2.SelectedValue = "< Seleccionar >" Then Exit Sub
        Call LLenaComboItemTabEsp(cboREnt3, cboREnt1.SelectedValue.Trim, cboREnt2.SelectedValue.Trim, "TBESP_ENT1", "TBESP_ENT2", "TBESP_ENT3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
    End Sub
    Protected Sub chkEntrevistador_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkEntrevistador.Checked = False Then
            chkEntrevistado.Checked = False
            txtRParticipante.Text = ""
        Else
            chkEntrevistado.Checked = False
            txtRParticipante.Text = txtRPersonal.Text.Trim
        End If
    End Sub
    Protected Sub chkEntrevistado_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If chkEntrevistado.Checked = False Then
            chkEntrevistador.Checked = False
            txtRParticipante.Text = ""
        Else
            chkEntrevistador.Checked = False
            txtRParticipante.Text = txtRApePat.Text.Trim & " " & txtRApeMat.Text.Trim & ", " & txtRNombres.Text.Trim
        End If
    End Sub
    Protected Sub btnRAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dRow As Data.DataRow
        Dim dt As New DataTable
        Dim i As Long = 0
        Dim a As Long = 0
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        Try
            If cboRParticipante.SelectedValue = "< Seleccionar >" Then lblRError.Text = "Seleccionar Tipo de Participante" : Exit Sub
            If txtRParticipante.Text.Trim = "" Then lblRError.Text = "Ingresar Apellidos y nombres del Participante" : Exit Sub
            For i = 0 To FlexParticipante.Rows.Count - 1
                a = a + 1
                dRow = dt.NewRow
                dRow("c1") = a
                dRow("c2") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexParticipante.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                dRow("c3") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexParticipante.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                dRow("c4") = Nu(FlexParticipante.Rows(i).Cells(4).Text.Trim)
                dt.Rows.Add(dRow)
            Next
            a = a + 1
            dRow = dt.NewRow
            dRow("c1") = a
            dRow("c2") = cboRParticipante.SelectedItem.Text
            dRow("c3") = txtRParticipante.Text.Trim
            dRow("c4") = cboRParticipante.SelectedValue.Trim
            dt.Rows.Add(dRow)
            FlexParticipante.DataSource = dt
            FlexParticipante.DataBind()
            cboRParticipante.SelectedValue = "< Seleccionar >"
            txtRParticipante.Text = ""
            chkEntrevistador.Checked = False
            chkEntrevistado.Checked = False
        Catch ex As SqlException
            lblRError.Text = ex.Message
        Catch Ex As Exception
            lblRError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexParticipante_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexParticipante.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dRow As Data.DataRow
        Dim dt As New DataTable
        Dim i As Long = 0 : Dim a As Long = 0
        dt.Columns.Add("c1")
        dt.Columns.Add("c2")
        dt.Columns.Add("c3")
        dt.Columns.Add("c4")
        Try
            If e.CommandName = "Quitar" Then
                FlexParticipante.Rows(index).Cells(1).Text = ""
                FlexParticipante.Rows(index).Cells(2).Text = ""
                FlexParticipante.Rows(index).Cells(3).Text = ""
                FlexParticipante.Rows(index).Cells(4).Text = ""
                For i = 0 To FlexParticipante.Rows.Count - 1
                    If FlexParticipante.Rows(i).Cells(1).Text <> "" Then
                        a = a + 1
                        dRow = dt.NewRow
                        dRow("c1") = a
                        dRow("c2") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexParticipante.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        dRow("c3") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexParticipante.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        dRow("c4") = Nu(FlexParticipante.Rows(i).Cells(4).Text.Trim)
                        dt.Rows.Add(dRow)
                    End If
                Next
                If dt.Rows.Count = 0 Then dt = Nothing
                FlexParticipante.DataSource = dt
                FlexParticipante.DataBind()
            End If
        Catch ex As SqlException
            lblRError.Text = ex.Message
        Catch Ex As Exception
            lblRError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexCita_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexCita.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Call Limpiar()
        lblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim AquienEnt As String = ""
        Try
            If e.CommandName = "Entrevista" Then
                Cn.Open() : cmdGlobal.Connection = Cn
                Call Llenar_Area()
                Call LlenarCajas_Personal()
                cmdGlobal.CommandText = " SELECT AGEN.AGEN_NRO_CITA, AGEN_TIPOPER, AGEN.AGEN_FECHA, AGEN.AGEN_HORA_INI,AGEN.AGEN_HORA_FIN, AGEN.AGEN_AREA,DA.AREA_NOMBRE,AGEN.AGEN_MODO_CITA," _
                                      & " AGEN.AGEN_TIPO_ATENCION,(SELECT elemen_valor From TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC202' AND ELEMEN_CODIGO = AGEN.AGEN_TIPO_ATENCION) AS TIPOAT, " _
                                      & " AGEN.AGEN_PERSONAL_CODIGO,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From TBPERSONAL WHERE PERSON_CODIGO = AGEN.AGEN_PERSONAL_CODIGO AND (AGEN.AGEN_TIPO_ATENCION = '5' OR AGEN.AGEN_GRAL_SUBTIPO_ATEN = '5')) AS NOMBRE_PERSONAL, AGEN.AGEN_PUBLICO_NOMBRE," _
                                      & " AGEN.AGEN_GRAL_SUBTIPO_ATEN, AGEN.AGEN_ASUNTO,AGEN.AGEN_OBSERVACION,AGEN.AGEN_COMPORT,AGEN.AGEN_ESTADO,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO=AGEN.AGEN_ESTADO AND ELEMEN_TABLA='TBOPC204') AS AAESTADO,AGEN.AGEN_CITA_REPROG," _
                                      & " (SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT + ', ' + PERSON_NOMBRES From TBPERSONAL WHERE PERSON_CODIGO = AGEN_USUARIO AND AGEN.AGEN_TIPOUSU='1') AS NOM_USUARIO1," _
                                      & " (SELECT USUARI_APEPAT + ' ' + USUARI_APEMAT + ', ' + USUARI_NOMBRES From BDSEGURIDADGRUPOEMPS.dbo.TBUSUARI WHERE USUARI_CODIGO = AGEN.AGEN_USUARIO AND USUARI_TIPO=AGEN.AGEN_TIPOUSU) AS NOM_USUARIO2," _
                                      & " (SELECT ELEMEN_VALOR From TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC203' AND ELEMEN_CODIGO = AGEN.AGEN_MODO_CITA) AS MODO_CITA,AGEN.AGEN_COMPORT,AGEN_NOMBRES,AGEN_APEPAT,AGEN_APEMAT,AGEN_EMPRESA " _
                                      & " From TBPERSONAL_AGENDA AGEN INNER JOIN TBPERSONAL_DEFINE_AREA DA ON DA.AREA_CODIGO = AGEN.AGEN_AREA " _
                                      & " WHERE (AGEN.AGEN_NRO_CITA = " & FlexCita.Rows(index).Cells(2).Text & ") "
                Rs = cmdGlobal.ExecuteReader()
                If Rs.HasRows Then
                    While Rs.Read
                        cboRArea.SelectedValue = Nu(Rs!AGEN_AREA)
                        If (Nz(Rs!AGEN_TIPO_ATENCION) = 1 Or Nz(Rs!AGEN_TIPO_ATENCION) = 2) Or (Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN) = 1 Or Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN) = 2) Then
                            AquienEnt = IIf(Nu(Rs!AGEN_GRAL_SUBTIPO_ATEN) = "", Nz(Rs!AGEN_TIPO_ATENCION), Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN))
                        ElseIf Nz(Rs!AGEN_TIPO_ATENCION) = 3 Or Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN) = 3 Then
                            AquienEnt = IIf(Nu(Rs!AGEN_GRAL_SUBTIPO_ATEN) = "", Nz(Rs!AGEN_TIPO_ATENCION), Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN))
                        ElseIf Nz(Rs!AGEN_TIPO_ATENCION) = 5 Or Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN) = 5 Then
                            AquienEnt = IIf(Nu(Rs!AGEN_GRAL_SUBTIPO_ATEN) = "", Nz(Rs!AGEN_TIPO_ATENCION), Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN))
                        ElseIf Nz(Rs!AGEN_TIPO_ATENCION) = 11 Or Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN) = 11 Then
                            AquienEnt = IIf(Nu(Rs!AGEN_GRAL_SUBTIPO_ATEN) = "", Nz(Rs!AGEN_TIPO_ATENCION), Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN))
                        ElseIf Nz(Rs!AGEN_TIPO_ATENCION) = 12 Or Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN) = 12 Then
                            AquienEnt = IIf(Nu(Rs!AGEN_GRAL_SUBTIPO_ATEN) = "", Nz(Rs!AGEN_TIPO_ATENCION), Nz(Rs!AGEN_GRAL_SUBTIPO_ATEN))
                        Else
                            Exit Sub
                        End If
                        cboREnt.SelectedValue = AquienEnt
                        cboREnt_SelectedIndexChanged(sender, e)
                        txtRCodCita.Text = Nu(Rs!AGEN_NRO_CITA)
                        cboRModoEnt.SelectedValue = Nu(Rs!AGEN_MODO_CITA)
                        If Nu(Rs!AGEN_TIPOPER) <> "" Then cboRTipoPer.SelectedValue = Nu(Rs!AGEN_TIPOPER)
                        txtREstadoCita.Text = Nu(Rs!AGEN_ESTADO)
                        txtRCodRazon.Text = Nu(Rs!AGEN_PERSONAL_cODIGO)
                        txtRNombres.Text = Nu(Rs!AGEN_NOMBRES)
                        txtRApePat.Text = Nu(Rs!AGEN_APEPAT)
                        txtRApeMat.Text = Nu(Rs!AGEN_APEMAT)
                        txtREmpresa.Text = Nu(Rs!AGEN_EMPRESA)
                        txtRFecha.Text = Right(Nu(Rs!AGEN_FECHA), 2) & "/" & Mid(Nu(Rs!AGEN_FECHA), 5, 2) & "/" & Left(Nu(Rs!AGEN_FECHA), 4)
                        txtRComienza.Text = Left(Nu(Rs!AGEN_HORA_INI), 2) & ":" & Right(Nu(Rs!AGEN_HORA_INI), 2)
                        txtRTermina.Text = Left(Nu(Rs!AGEN_HORA_FIN), 2) & ":" & Right(Nu(Rs!AGEN_HORA_FIN), 2)
                        txtRAsunto.Text = Nu(Rs!AGEN_ASUNTO)
                        If Nu(Rs!AGEN_PERSONAL_CODIGO) <> "" Then txtRCodRazon.Text = Nu(Rs!AGEN_PERSONAL_CODIGO) : txtRGrabar.Text = "N"
                        txtRFecha.Enabled = False
                    End While
                End If
                Rs.Close()
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
                Ficha.TabIndex = "2"
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
End Class
