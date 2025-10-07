Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class PersonalAgenda_Agenda_Citas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.Enabled = False
            Ficha.ActiveTabIndex = 0
            Ficha_ActiveTabChanged(sender, e)
            Dim codArea As String = Convert.ToString(Request.QueryString("Ni830dHuciPLO"))
            Dim codAsignado As String = Convert.ToString(Request.QueryString("8JAsd0hfiuF"))
            If codArea <> "" Then
                cboAArea.SelectedValue = codArea
                cboAArea_SelectedIndexChanged(sender, e)
                cboAPersonal.SelectedValue = codAsignado
                cboAPersonal_SelectedIndexChanged(sender, e)
            End If
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            txtAFecha.Text = FormatoFecha(FechaActual)
            Call LlenaAno(cboAAno)
            cboAAno.SelectedValue = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Call Personal_Area()
            cboAPersonal.Items.Add("< Seleccionar >") : cboAPersonal.SelectedValue = "< Seleccionar >"
            cboATAtencion.Items.Add("< Seleccionar >") : cboATAtencion.SelectedValue = "< Seleccionar >"

            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha.Enabled = True
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call Hacer_Cita()
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.Enabled = True
        End If
    End Sub
    Private Sub Personal_Area()
        lblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = "SELECT AREA_CODIGO,AREA_NOMBRE FROM TBPERSONAL_DEFINE_AREA WHERE GRPOEMPRESA_CODIGO = '" & Session("CodGrupoEmpresa") & "' AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND AREA_SYS_EST='0'"
            Rs = cmdGlobal.ExecuteReader
            cboAArea.DataSource = Rs
            cboAArea.DataTextField = "AREA_NOMBRE"
            cboAArea.DataValueField = "AREA_CODIGO"
            cboAArea.DataBind()
            Rs.Close()
            cboAArea.Items.Add("< Seleccionar >") : cboAArea.SelectedValue = "< Seleccionar >"
            cboAPersonal.Items.Add("< Seleccionar >") : cboAPersonal.SelectedValue = "< Seleccionar >"
            cboATAtencion.Items.Add("< Seleccionar >") : cboATAtencion.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub cboAArea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAArea.SelectedIndexChanged
        lblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            cboAPersonal.Items.Clear()
            cboATAtencion.Items.Clear()
            If cboAArea.SelectedValue <> "< Seleccionar >" Then
                cmdGlobal.CommandText = " SELECT PERSON_PERSONAL,(SELECT person_apepat + ' ' + person_apemat + ', ' + person_nombres From TBPERSONAL WHERE person_codigo = PERSON_PERSONAL)  AS NOMBRE_PERSONAL " _
                                      & " From TBPERSONAL_AREAS WHERE (PERSON_AREA_SYS_EST = '0') AND (AREA_CODIGO = '" & cboAArea.SelectedValue.Trim & "') "
                cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY NOMBRE_PERSONAL"
                Rs = cmdGlobal.ExecuteReader
                cboAPersonal.DataSource = Rs
                cboAPersonal.DataTextField = "Nombre_personal"
                cboAPersonal.DataValueField = "PERSON_PERSONAL"
                cboAPersonal.DataBind()
                Rs.Close()
            End If
            cboAPersonal.Items.Add("< Seleccionar >") : cboAPersonal.SelectedValue = "< Seleccionar >"
            cboATAtencion.Items.Add("< Seleccionar >") : cboATAtencion.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub cboAPersonal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAPersonal.SelectedIndexChanged
        lblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim psAño As String = ""
        Dim TipoA As String = ""
        Dim Cadena As String = ""
        Dim Dia1 As String = ""
        Dim i As Long = 0
        Dim HorAtencion As String = ""
        Dim NomTipo As String = ""
        Dim Hora1 As String = ""
        Dim Hora2 As String = ""
        Dim Hora11 As String = ""
        Dim Hora22 As String = ""
        Dim pdNroFilas As Long = 0
        Dim Sql As String = ""
        Dim a As Long = 0
        Call LlenaMes(cboAMes, True)
        Dim psMes As String = Mid(FechaActual, 5, 2)
        cboAMes.SelectedValue = psMes
        cboAMes_SelectedIndexChanged(sender, e)
        Try
            psAño = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Cn.Open() : cmdGlobal.Connection = Cn
            cboATAtencion.Items.Clear()
            If cboAPersonal.SelectedValue = "< Seleccionar >" Then Exit Sub
            cmdGlobal.CommandText = "SELECT DISTINCT ATEN_TIPO,(SELECT elemen_valor FROM tbCelemen  WHERE ELEMEN_TABLA = 'TBOPC202' AND ELEMEN_CODIGO = ATEN_TIPO) AS TIPOA " _
                & " FROM TBPERSONAL_HORATENCION WHERE (ATEN_AÑO = '" & psAño & "') AND (ATEN_PERSONAL = '" & cboAPersonal.SelectedValue.Trim & "') AND (ATEN_SYS_EST = '0') AND (ATEN_AREA='" & cboAArea.SelectedValue.Trim & "') ORDER BY TIPOA"
            Rs = cmdGlobal.ExecuteReader
            cboATAtencion.DataSource = Rs
            cboATAtencion.DataTextField = "TipoA"
            cboATAtencion.DataValueField = "ATEN_TIPO"
            cboATAtencion.DataBind()
            Rs.Close()
            cboATAtencion.Items.Add("< Seleccionar >") : cboATAtencion.SelectedValue = "< Seleccionar >"
            Dim dtListado As New DataTable
            Dim drT As DataRow
            pdNroFilas = 0
            Sql = " SELECT ATEN_TIPO,  (SELECT ELEMEN_VALOR  From TBCELEMEN  WHERE ELEMEN_CODIGO = ATEN_TIPO AND  ELEMEN_TABLA = 'TBOPC202') AS ATIPO,  " _
                & " ATEN_DIA,ATEN_HOR_INI , ATEN_HOR_FIN From TBPERSONAL_HORATENCION WHERE (ATEN_AÑO = '" & psAño & "') AND (ATEN_SYS_EST = '0') AND (ATEN_PERSONAL = '" & cboAPersonal.SelectedValue.Trim & "') " _
                & " AND (ATEN_AREA='" & cboAArea.SelectedValue.Trim & "') ORDER BY ATIPO,ATEN_DIA,ATEN_HOR_INI , ATEN_HOR_FIN"
            cmdGlobal.CommandText = Sql
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdNroFilas = pdNroFilas + 1
                End While
            End If
            Rs.Close()
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            'dtListado.Columns.Add("c4")
            'dtListado.Columns.Add("c5")
            cmdGlobal.CommandText = Sql
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    a = a + 1
                    If TipoA <> Nu(Rs!ATEN_TIPO) Then
                        If TipoA <> "" Then
                            If Cadena <> "" Then Cadena = Cadena & Chr(13)
                            Dia1 = Comprension_Dias(Dia1) 'NomTipo & " : " & Chr(13) & 
                            HorAtencion = Cadena & "   " & Dia1 & " : " & Hora11 & " a " & Hora22
                            i = i + 1
                            drT = dtListado.NewRow()
                            drT("c1") = i
                            drT("c2") = NomTipo
                            drT("c3") = HorAtencion
                            'drT("c4") = Hora11
                            'drT("c5") = Hora22
                            dtListado.Rows.Add(drT)
                        End If
                        NomTipo = Nu(Rs!ATIPO)
                        TipoA = Nu(Rs!ATEN_TIPO)
                        Cadena = ""
                        Dia1 = Nombre_Dia(Nu(Rs!ATEN_DIA), False)
                        If Hora1 <> Nu(Rs!ATEN_HOR_INI) Or Hora2 <> Nu(Rs!ATEN_HOR_FIN) Then
                            Hora1 = Nu(Rs!ATEN_HOR_INI) : Hora11 = Left(Nu(Rs!ATEN_HOR_INI), 2) & ":" & Right(Nu(Rs!ATEN_HOR_INI), 2)
                            Hora2 = Nu(Rs!ATEN_HOR_FIN) : Hora22 = Left(Nu(Rs!ATEN_HOR_FIN), 2) & ":" & Right(Nu(Rs!ATEN_HOR_FIN), 2)
                        Else
                        End If
                        If a = pdNroFilas Then
                            If Cadena <> "" Then Cadena = Cadena & Chr(13)
                            Dia1 = Comprension_Dias(Dia1) 'NomTipo & " : " & Chr(13) & 
                            HorAtencion = Cadena & "   " & Dia1 & " : " & Hora11 & " a " & Hora22
                            i = i + 1
                            drT = dtListado.NewRow()
                            drT("c1") = i
                            drT("c2") = NomTipo
                            drT("c3") = HorAtencion
                            dtListado.Rows.Add(drT)
                        End If
                    Else
                        If Hora1 <> Nu(Rs!ATEN_HOR_INI) Or Hora2 <> Nu(Rs!ATEN_HOR_FIN) Then
                            If Cadena <> "" Then Cadena = Cadena & Chr(13)
                            Dia1 = Comprension_Dias(Dia1)
                            Cadena = Cadena & "   " & Dia1 & " : " & Hora11 & " a " & Hora22
                            Hora1 = Nu(Rs!ATEN_HOR_INI) : Hora11 = Left(Nu(Rs!ATEN_HOR_INI), 2) & ":" & Right(Nu(Rs!ATEN_HOR_INI), 2)
                            Hora2 = Nu(Rs!ATEN_HOR_FIN) : Hora22 = Left(Nu(Rs!ATEN_HOR_FIN), 2) & ":" & Right(Nu(Rs!ATEN_HOR_FIN), 2)
                            Dia1 = ""
                        End If
                        If Dia1 <> "" Then Dia1 = Dia1 & ", "
                        Dia1 = Dia1 & Nombre_Dia(Nu(Rs!ATEN_DIA), False)
                        If a = pdNroFilas Then
                            If Cadena <> "" Then Cadena = Cadena & Chr(13)
                            Dia1 = Comprension_Dias(Dia1) 'NomTipo & " : " & Chr(13) & 
                            HorAtencion = Cadena & "   " & Dia1 & " : " & Hora11 & " a " & Hora22
                            i = i + 1
                            drT = dtListado.NewRow()
                            drT("c1") = i
                            drT("c2") = NomTipo
                            drT("c3") = HorAtencion
                            dtListado.Rows.Add(drT)
                        End If
                    End If
                End While
            End If
            Rs.Close()
            FlexHorario.DataSource = dtListado
            FlexHorario.DataBind()
            Sql = " SELECT NCIT_NRO_CITAS FROM TBPERSONAL_HORATENCION_TXC " _
                & " WHERE NCIT_AÑO='" & psAño & "' AND NCIT_PERSONAL='" & cboAPersonal.SelectedValue.Trim & "' " _
                & " AND (NCIT_AREA='" & cboAArea.SelectedValue.Trim & "')"
            cmdGlobal.CommandText = Sql
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtAMinutoCita.Text = IIf(Nu(Rs(0)) = "", "", Nu(Rs(0)))
                End While
            End If
            Rs.Close()
            If cboATAtencion.Items.Count > 1 Then
                'cboATAtencion.SelectedValue = "3"
                'Call Llenar_Disponibilidad(psAño)
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub Llenar_Disponibilidad(ByVal psAño As String)
        btnACita.Enabled = False
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim RsA As SqlClient.SqlDataReader
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim RsA2 As SqlClient.SqlDataReader
        Dim dtLisSemanal1 As New DataTable
        Dim dtLisDisponible As New DataTable
        Dim drSemanal1 As DataRow
        Dim drDispo As DataRow
        Dim Sql As String = ""
        Dim psHor1 As String = ""
        Dim psHor2 As String = ""
        Dim psHor3 As String = ""
        Dim psHor4 As String = ""
        'semanal 1
        dtLisSemanal1.Columns.Add("cDe") 'de
        dtLisSemanal1.Columns.Add("cA") 'a
        dtLisSemanal1.Columns.Add("c1") 'Marcar si esta ocupado
        'disponibilidad
        dtLisDisponible.Columns.Add("c1") 'hora inicio
        dtLisDisponible.Columns.Add("c2") 'fora fin
        Dim psDia As String = ""
        Dim psFecha As String = ""
        Dim psDiaAten As Long = 0
        Dim i As Long = 0
        Dim dia As String = ""
        Dim psDispoDia As String = ""
        Dim cv As String = ""
        Dim r As Long = 0
        Dim a As Long = 0
        Dim Fx As Long = 0
        Dim psFechaCita As String = Right(txtAFecha.Text, 4) + Mid(txtAFecha.Text, 4, 2) + Left(txtAFecha.Text, 2)
        psFecha = CDate(txtAFecha.Text)
        psDiaAten = Weekday(psFecha)
        If psDiaAten = "1" Then psDia = "7"
        If psDiaAten <> "1" Then psDia = psDiaAten - 1
        'abrir conexiones
        Cn.Open() : Cn2.Open()
        cmdGlobal.Connection = Cn : cmdGlobal2.Connection = Cn2
        cmdGlobal.CommandText = " SELECT ATEN_HOR_INI, ATEN_HOR_FIN " _
                              & " From TBPERSONAL_HORATENCION " _
                              & " WHERE (ATEN_AÑO = '" & psAño & "') " _
                              & " AND (ATEN_PERSONAL = '" & cboAPersonal.SelectedValue.Trim & "') " _
                              & " AND (ATEN_TIPO = '" & cboATAtencion.SelectedValue.Trim & "') " _
                              & " AND (ATEN_DIA = '" & psDia & "') AND  (ATEN_SYS_EST = '0') " _
                              & " AND (ATEN_AREA='" & cboAArea.SelectedValue.Trim & "') " _
                              & " ORDER BY ATEN_HOR_INI, ATEN_HOR_FIN"
        RsA = cmdGlobal.ExecuteReader
        If RsA.HasRows Then
            While RsA.Read
                i = i + 1
                If i = 1 Then psHor1 = Nu(RsA!ATEN_HOR_INI) : psHor2 = Nu(RsA!ATEN_HOR_FIN)
                If i = 2 Then psHor3 = Nu(RsA!ATEN_HOR_INI) : psHor4 = Nu(RsA!ATEN_HOR_FIN)
            End While
            psDispoDia = "Día " + Nombre_Dia(psDia, False) + " " + dia
            If psHor1 <> "" And psHor2 <> "" Then
                For a = Val(psHor1) To Val(psHor2) - 5 Step 5
                    cv = Llenar_Ceros(a, 4)
                    If Left(cv, 2) = "24" Then cv = "00" + Right(cv, 2)
                    If Val(Right(cv, 2)) <= 55 Then
                        drSemanal1 = dtLisSemanal1.NewRow()
                        drSemanal1("cDe") = FormatoHora(cv)
                        cv = Llenar_Ceros(a + 5, 4)
                        If Val(Right(cv, 2)) = 60 Then
                            cv = Llenar_Ceros(Val(Left(cv, 2)) + 1, 2) + "00"
                            drSemanal1("cA") = FormatoHora(cv)
                            dtLisSemanal1.Rows.Add(drSemanal1)
                        Else
                            cv = Val(cv) + 5
                            If Len(cv) = 3 Then
                                cv = Llenar_Ceros(cv, 4)
                            Else
                                cv = cv
                            End If
                            drSemanal1("cA") = FormatoHora(cv)
                            dtLisSemanal1.Rows.Add(drSemanal1)
                        End If
                    End If
                Next
            End If
            If psHor3 <> "" And psHor4 <> "" Then
                For a = Val(psHor1) To Val(psHor2) - 5 Step 5
                    cv = Llenar_Ceros(a, 4)
                    If Left(cv, 2) = "24" Then cv = "00" + Right(cv, 2)
                    If Val(Right(cv, 2)) <= 55 Then
                        drSemanal1 = dtLisSemanal1.NewRow()
                        drSemanal1("cDe") = FormatoHora(cv)
                        cv = Llenar_Ceros(a + 5, 4)
                        If Val(Right(cv, 2)) = 60 Then
                            cv = Llenar_Ceros(Val(Left(cv, 2)) + 1, 2) + "00"
                            drSemanal1("cA") = FormatoHora(cv)
                            dtLisSemanal1.Rows.Add(drSemanal1)
                        Else
                            cv = Val(cv) + 5
                            If Len(cv) = 3 Then
                                cv = Llenar_Ceros(cv, 4)
                            Else
                                cv = cv
                            End If
                            drSemanal1("cA") = FormatoHora(cv)
                            dtLisSemanal1.Rows.Add(drSemanal1)
                        End If
                    End If
                Next
            End If
        End If
        RsA.Close()
        FlexHSemana.DataSource = dtLisSemanal1
        FlexHSemana.DataBind()
        Dim j As Long = 0
        Dim psHoraDe As String = ""
        Dim psHoraA As String = ""
        Dim psHoraIni As String = ""
        Dim psHoraIniGrilla As String = ""
        Dim psHoraFinGrilla As String = ""
        Dim psHoraFin As String = ""
        cmdGlobal2.CommandText = "SELECT AGEN_HORA_INI, AGEN_HORA_FIN FROM TBPERSONAL_AGENDA WHERE (AGEN_AÑO = '" & psAño & "') AND (AGEN_PERSONAL = '" & cboAPersonal.SelectedValue.Trim & "') AND " _
                   & "(AGEN_TIPO_ATENCION = '" & cboATAtencion.SelectedValue.Trim & "') AND  (AGEN_FECHA = '" & psFechaCita & "') AND (AGEN_SYS_EST = '0') AND " _
                   & "(AGEN_AREA='" & cboAArea.SelectedValue.Trim & "') AND (AGEN_ESTADO<>'6' AND AGEN_ESTADO<>'7') ORDER BY AGEN_HORA_INI"
        RsA2 = cmdGlobal2.ExecuteReader
        If RsA2.HasRows Then
            While RsA2.Read
                For a = Nz(RsA2!AGEN_HORA_INI) To Nz(RsA2!AGEN_HORA_FIN) - 5 Step 5
                    cv = Llenar_Ceros(a, 4)
                    If Val(Right(cv, 2)) <= 55 Then
                        If FlexHSemana.Rows.Count > 0 Then
                            For j = 0 To FlexHSemana.Rows.Count - 1
                                psHoraDe = Left(FlexHSemana.Rows(j).Cells(0).Text, 2) + Right(FlexHSemana.Rows(j).Cells(0).Text, 2)
                                psHoraA = Left(FlexHSemana.Rows(j).Cells(1).Text, 2) + Right(FlexHSemana.Rows(j).Cells(1).Text, 2)
                                If Val(cv) = Val(psHoraDe) Then FlexHSemana.Rows(j).Cells(2).Text = "x"
                                If Val(cv) = Val(psHoraA) Then FlexHSemana.Rows(j).Cells(2).Text = "x"
                            Next
                        End If
                    End If
                Next
            End While
            psHoraIni = "" : psHoraFin = ""
            For j = 0 To FlexHSemana.Rows.Count - 1
                If psHoraIni = "" Then
                    If Replace(FlexHSemana.Rows(j).Cells(2).Text, "&nbsp;", "") = "" Then
                        psHoraIni = FlexHSemana.Rows(j).Cells(0).Text
                    End If
                End If
                If FlexHSemana.Rows(j).Cells(2).Text = "x" Then
                    psHoraFin = FlexHSemana.Rows(j).Cells(1).Text
                    psHoraIniGrilla = psHoraIni
                    psHoraFinGrilla = psHoraFin
                    psHoraIni = "" : psHoraFin = ""
                End If
                If psHoraIniGrilla <> "" And psHoraFinGrilla <> "" Then
                    If Val(Left(psHoraIniGrilla, 2) + Right(psHoraIniGrilla, 2)) < Val(Left(psHoraFinGrilla, 2) + Right(psHoraFinGrilla, 2)) Then
                        drDispo = dtLisDisponible.NewRow()
                        drDispo("c1") = psHoraIniGrilla
                        drDispo("c2") = psHoraFinGrilla
                        dtLisDisponible.Rows.Add(drDispo)
                    End If
                End If
                If j = FlexHSemana.Rows.Count - 1 Then
                    If Val(Left(psHoraIniGrilla, 2) + Right(psHoraIniGrilla, 2)) < Val(Left(FlexHSemana.Rows(j).Cells(1).Text, 2) + Right(FlexHSemana.Rows(j).Cells(1).Text, 2)) Then
                        drDispo = dtLisDisponible.NewRow()
                        drDispo("c1") = psHoraIni
                        drDispo("c2") = FlexHSemana.Rows(j).Cells(1).Text
                        dtLisDisponible.Rows.Add(drDispo)
                    End If
                End If
            Next
        Else
            If psHor1 <> "" And psHor2 <> "" Then
                drDispo = dtLisDisponible.NewRow()
                drDispo("c1") = Left(psHor1, 2) + ":" + Right(psHor1, 2)
                drDispo("c2") = Left(psHor2, 2) + ":" + Right(psHor2, 2)
                dtLisDisponible.Rows.Add(drDispo)
                If psHor3 <> "" And psHor4 <> "" Then
                    drDispo = dtLisDisponible.NewRow()
                    drDispo("c1") = Left(psHor3, 2) + ":" + Right(psHor3, 2)
                    drDispo("c2") = Left(psHor4, 2) + ":" + Right(psHor4, 2)
                    dtLisDisponible.Rows.Add(drDispo)
                End If
            End If
        End If
        RsA2.Close()
        FlexDispo.DataSource = dtLisDisponible
        FlexDispo.DataBind()
        If FlexDispo.Rows.Count > 0 Then btnACita.Enabled = True
    End Sub
    Protected Sub btnAListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cmdGlobal2 As New SqlCommand
        Dim RsA As SqlClient.SqlDataReader
        'Dim RsA3 As SqlClient.SqlDataReader
        Dim dtListado As New DataTable
        Dim drT As DataRow
        Dim psFechaCita As String = ""
        Dim psFechaCitaFin As String = ""
        Dim psAño As String = ""
        Dim AreaA As String = ""
        Dim i As Long = 0
        Dim NomUsurio As String = ""
        Dim psHorario As String = ""
        Dim psPersona As String = ""
        Dim psAsunto As String = ""
        Dim psModoCita As String = ""
        Dim psObsCita As String = ""
        Try
            If cboATAtencion.SelectedValue.Trim = "< Seleccionar >" Then lblError.Text = "Seleccionar el Tipo de atención." : Exit Sub
            psAño = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Cn.Open() : cmdGlobal.Connection = Cn
            Cn2.Open() : cmdGlobal2.Connection = Cn2
            dtListado.Columns.Add("c0") '#
            dtListado.Columns.Add("c1") 'hORARIO Y eSTADO
            dtListado.Columns.Add("c2") 'PRESONA
            dtListado.Columns.Add("c3") 'ASUNTO
            dtListado.Columns.Add("c4") 'MODO DE CITA
            dtListado.Columns.Add("c5") 'OBS CITA
            psFechaCita = Right(txtAFecha.Text, 4) + Mid(txtAFecha.Text, 4, 2) + Left(txtAFecha.Text, 2)
            psFechaCitaFin = Right(txtAFechaFin.Text, 4) + Mid(txtAFechaFin.Text, 4, 2) + Left(txtAFechaFin.Text, 2)
            cmdGlobal.CommandText = " SELECT AGEN_NRO_CITA ,AGEN_FECHA, AGEN_HORA_INI, AGEN_HORA_FIN," _
                                  & " AGEN_PERSONAL_CODIGO,(SELECT PERSON_APEPAT + ' ' + PERSON_APEMAT +', ' + PERSON_NOMBRES From TBPERSONAL Where PERSON_CODIGO = AGEN_PERSONAL_CODIGO AND (AGEN_TIPO_ATENCION = '5' OR AGEN_GRAL_SUBTIPO_ATEN='5'))  AS NOMBRE_PERSONAL, AGEN_PUBLICO_NOMBRE," _
                                  & " AGEN_GRAL_SUBTIPO_ATEN, AGEN_ASUNTO,AGEN_OBSERVACION , AGEN_ESTADO,(SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO=AGEN_USUARIO) AS NOM_USUARIO1,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_CODIGO=AGEN_ESTADO AND ELEMEN_TABLA='TBOPC204') AS AAESTADO, " _
                                  & " (SELECT USUARI_APEPAT+' '+USUARI_APEMAT+', '+USUARI_NOMBRES FROM bdSEGURIDADGRUPOEMPS.dbo.TBUSUARI WHERE USUARI_CODIGO=AGEN_USUARIO  AND USUARI_TIPO=AGEN_TIPOUSU) AS NOM_USUARIO2,(SELECT ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA='TBOPC203' AND ELEMEN_CODIGO=AGEN_MODO_CITA) AS MODO_CITA,AGEN_CITA_REPROG,AGEN_COMPORT " _
                                  & " From TBPERSONAL_AGENDA WHERE (AGEN_AÑO = '" & psAño & "') AND (AGEN_PERSONAL = '" & cboAPersonal.SelectedValue.Trim & "') AND (AGEN_TIPO_ATENCION = '" & cboATAtencion.SelectedValue.Trim & "') AND  (AGEN_FECHA BETWEEN '" & psFechaCita & "' AND '" & psFechaCita & "') AND (AGEN_SYS_EST = '0') AND (AGEN_AREA='" & cboAArea.SelectedValue.Trim & "') ORDER BY AGEN_HORA_INI"
            RsA = cmdGlobal.ExecuteReader
            If RsA.HasRows Then
                While RsA.Read
                    If Nu(RsA!NOM_USUARIO1) = "" Then
                        NomUsurio = Nu(RsA!NOM_USUARIO2)
                    Else
                        NomUsurio = Nu(RsA!NOM_USUARIO1)
                    End If
                    psHorario = Left(Nu(RsA!AGEN_HORA_INI), 2) + ":" + Right(Nu(RsA!AGEN_HORA_INI), 2)
                    psHorario = psHorario & Chr(13) & Left(Nu(RsA!AGEN_HORA_FIN), 2) + ":" + Right(Nu(RsA!AGEN_HORA_FIN), 2)
                    psHorario = psHorario & Chr(13) & Chr(13) & Nu(RsA!AAESTADO)
                    If cboATAtencion.SelectedValue.Trim = "1" Or cboATAtencion.SelectedValue.Trim = 2 Then
                    ElseIf cboATAtencion.SelectedValue.Trim = 5 Then
                        psPersona = Nu(RsA!Nombre_personal)
                    ElseIf cboATAtencion.SelectedValue.Trim = 3 Then
                        psPersona = Nu(RsA!AGEN_PUBLICO_NOMBRE)
                    ElseIf cboATAtencion.SelectedValue.Trim = 4 Or cboATAtencion.SelectedValue.Trim = 6 Or cboATAtencion.SelectedValue.Trim = 7 Or cboATAtencion.SelectedValue.Trim = 8 Or cboATAtencion.SelectedValue.Trim = 9 Or cboATAtencion.SelectedValue.Trim = 10 Or cboATAtencion.SelectedValue.Trim = 11 Then
                        If Nu(RsA!AGEN_GRAL_SUBTIPO_ATEN) = "1" Or Nu(RsA!AGEN_GRAL_SUBTIPO_ATEN) = "2" Then
                        ElseIf Nu(RsA!AGEN_GRAL_SUBTIPO_ATEN) = "5" Then
                            psPersona = Nu(RsA!Nombre_personal) 'Nu(RsA!AGEN_PERSONAL_CODIGO) + "   " +
                        ElseIf Nu(RsA!AGEN_GRAL_SUBTIPO_ATEN) = "3" Then
                            psPersona = Nu(RsA!AGEN_PUBLICO_NOMBRE)
                        End If
                    End If
                    psAsunto = (Nu(RsA!AGEN_ASUNTO))
                    psModoCita = Nu(RsA!MODO_CITA) & IIf(Nu(RsA!AGEN_COMPORT) = "S", " [PROB. COMPORTAMIENTO]", "")
                    psObsCita = Nu(RsA!AGEN_OBSERVACION)
                    If Nu(RsA!AGEN_CITA_REPROG) = "S" Then psObsCita = psObsCita & Chr(13) & " (Cita Reprogramada)"
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c0") = i
                    drT("c1") = psHorario
                    drT("c2") = psPersona
                    drT("c3") = psAsunto
                    drT("c4") = psModoCita
                    drT("c5") = psObsCita
                    dtListado.Rows.Add(drT)
                End While
            Else
                'AreaA = ""
                'For i = 0 To cboATAtencion.Items.Count - 1
                '    If AreaA <> "" Then AreaA = AreaA & " AND "
                '    AreaA = AreaA & " AGEN_TIPO_ATENCION <> '" & cboATAtencion.SelectedValue.Trim & "' "
                'Next
                'If AreaA <> "" Then
                '    cmdGlobal2.CommandText = "SELECT * FROM TBPERSONAL_AGENDA WHERE (AGEN_AÑO = '" & psAño & "') AND (AGEN_PERSONAL = '" & cboAPersonal.SelectedValue.Trim & "') AND (AGEN_FECHA BETWEEN '" & psFechaCita & "' AND '" & psFechaCita & "') AND (AGEN_SYS_EST = '0') AND (AGEN_AREA='" & cboAArea.SelectedValue.Trim & "')"
                '    cmdGlobal2.CommandText = cmdGlobal2.CommandText & " AND (" & AreaA & ")"
                '    RsA3 = cmdGlobal2.ExecuteReader
                '    If RsA3.HasRows Then
                '        While RsA3.Read
                '            lblError.Text = "Se ha encontrado citas pero que no podran ser mostrardas por la razón de que se definieron en un tipo de atención diferente al que posee ahora."
                '        End While
                '    End If
                '    RsA3.Close()
                'End If
            End If
            RsA.Close()
            FlexCitas.DataSource = dtListado
            FlexCitas.DataBind()
            'cboATAtencion.SelectedValue = "3"
            Call Llenar_Disponibilidad(psAño)
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Protected Sub cboAMes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        Dim dtListado As New DataTable
        Dim drT As DataRow = Nothing
        Dim i As Long = 0
        Dim dia As String = ""
        Dim Mm As String = ""
        Dim nAño As String = ""
        Dim Fec As String = ""
        Dim Fl As Long = 0
        Dim Dd As String = ""
        Dim ii As Long = 0
        Dim psfecha As Date
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            dtListado.Columns.Add("c0") 'lun
            dtListado.Columns.Add("c1") 'mar
            dtListado.Columns.Add("c2") 'mie
            dtListado.Columns.Add("c3") 'jue
            dtListado.Columns.Add("c4") 'vie
            dtListado.Columns.Add("c5") 'sab
            dtListado.Columns.Add("c6") 'dom
            nAño = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Mm = Llenar_Ceros(cboAMes.SelectedValue.Trim, 2)
            Dim dom As String = "No"
            Fl = 1
            For i = 1 To 31
                dia = i
                Fec = Llenar_Ceros(dia, 2) & "/" & Mm & "/" & nAño
                If IsDate(Fec) = True Then
                    psfecha = CDate(Fec)
                    If i = 1 Then drT = dtListado.NewRow()
                    If i > 1 And dom = "Si" Then drT = dtListado.NewRow()
                    If Weekday(psfecha) = 2 Then 'lunes
                        drT("c0") = i : dom = "No"
                    ElseIf Weekday(psfecha) = 3 Then 'martes
                        drT("c1") = i : dom = "No"
                    ElseIf Weekday(psfecha) = 4 Then 'miercoles
                        drT("c2") = i : dom = "No"
                    ElseIf Weekday(psfecha) = 5 Then 'jueves
                        drT("c3") = i : dom = "No"
                    ElseIf Weekday(psfecha) = 6 Then 'viernes
                        drT("c4") = i : dom = "No"
                    ElseIf Weekday(psfecha) = 7 Then 'sabado
                        drT("c5") = i : dom = "No"
                    ElseIf Weekday(psfecha) = 1 Then 'domingo
                        drT("c6") = i : dom = "Si"
                        If Fl < 5 Then Fl = Fl + 1 Else Fl = 1
                    End If
                    If dom = "Si" Then dtListado.Rows.Add(drT)
                    If i = 31 And dom = "No" Then dtListado.Rows.Add(drT)
                Else
                    dtListado.Rows.Add(drT)
                    Exit For
                End If
            Next
            FlexMes.DataSource = dtListado
            FlexMes.DataBind()
            'MARCAR FECHA CON CITA
            Dim r As Long = 0
            Dim a As Long = 0
            Dim Salir As Boolean = False
            Dim psAño As String = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
            Cn.Open() : cmdGlobal.Connection = Cn
            If cboAPersonal.SelectedValue.Trim <> "< Seleccionar >" Then
                cmdGlobal.CommandText = " SELECT distinct AGEN_FECHA From TBPERSONAL_AGENDA WHERE (AGEN_AÑO = '" & psAño & "') AND (AGEN_PERSONAL = '" & cboAPersonal.SelectedValue.Trim & "') " _
                    & "AND (AGEN_SYS_EST = '0') AND (SUBSTRING(AGEN_FECHA, 5, 2) = '" & Mm & "') AND (left(AGEN_FECHA,4)='" & nAño & "') AND (AGEN_AREA='" & cboAArea.SelectedValue.Trim & "') order by 1"
                Rs = cmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        Salir = False
                        For r = 0 To FlexMes.Rows.Count - 1
                            For a = 0 To FlexMes.Columns.Count - 1
                                If Llenar_Ceros(FlexMes.Rows(r).Cells(a).Text, 2) = Right(Nu(Rs!AGEN_FECHA), 2) Then
                                    FlexMes.Rows(r).Cells(a).Text = "(" & FlexMes.Rows(r).Cells(a).Text & ")"
                                    FlexMes.Rows(r).Cells(a).ForeColor = Drawing.Color.Purple
                                    FlexMes.Rows(r).Cells(a).Font.Bold = True
                                    Salir = True
                                    Exit For
                                End If
                            Next
                            If Salir = True Then Exit For
                        Next
                    End While
                End If
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnACita_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If txtAMinutoCita.Text.Trim = "" Then lblError.Text = "No puede hacer citas debido que no posee un Nro de Minutos aproximados por Cita" : Exit Sub
        If cboAPersonal.SelectedValue.Trim <> "< Seleccionar >" And cboATAtencion.SelectedValue.Trim <> "< Seleccionar >" Then
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.TabIndex = "1"
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Private Sub Hacer_Cita()
        lblRError.Text = ""
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            cmdGlobal.CommandText = " SELECT person_codigo, person_apepat + ' ' + person_apemat + ', ' + person_nombres AS NOMBRE_PERSONAL" _
                                  & " From TBPERSONAL WHERE person_codigo = '" & cboAPersonal.SelectedValue.Trim & "'   "
            cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY NOMBRE_PERSONAL"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtRPersonal.Text = Nu(Rs!NOMBRE_PERSONAL)
                    txtRCodPersonal.Text = Nu(Rs!PERSON_CODIGO)
                End While
            End If
            Rs.Close()
            cmdGlobal.CommandText = "SELECT AREA_CODIGO,AREA_NOMBRE " _
                                  & " FROM TBPERSONAL_DEFINE_AREA " _
                                  & " WHERE GRPOEMPRESA_CODIGO = '" & Session("CodGrupoEmpresa") & "' " _
                                  & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                                  & " AND AREA_SYS_EST='0' AND AREA_CODIGO = '" & cboAArea.SelectedValue.Trim & "'"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    txtRArea.Text = Nu(Rs!AREA_NOMBRE)
                    txtRCodArea.Text = Nu(Rs!AREA_CODIGO)
                End While
            End If
            Rs.Close()
            txtRFecha.Text = txtAFecha.Text
            txtRMinCita.Text = txtAMinutoCita.Text.Trim
            txtRApePat.ReadOnly = True : txtRApeMat.ReadOnly = True : txtRNombres.ReadOnly = True : txtREmpresa.ReadOnly = True
            Call LlenaComboItem("TBOPC001", cboRTipoPer)
            txtRAsunto.Text = "" : txtRObs.Text = "" : txtRGrabar.Text = ""
            txtRApePat.Text = "" : txtRApeMat.Text = "" : txtRNombres.Text = "" : txtREmpresa.Text = ""
            cboRTipoPer.SelectedValue = "< Seleccionar >"
            cboRModoCita.SelectedValue = "(Seleccionar)"
            cboRTipoPer.Enabled = False
        Catch ex As SqlException
            lblRError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblRError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnRGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblRError.Text = ""
        If cboRCita.SelectedValue = "(Seleccionar)" Then lblRError.Text = "<br> - Seleccionar para quien es la cita."
        If cboRCita.SelectedValue = "5" And txtRCodRazon.Text = "" Then lblRError.Text = lblRError.Text & "<br> - Es dato necesario saber para quien se saca cita, ubique al Personal"
        If cboRTipoPer.SelectedValue = "< Seleccionar >" Then lblRError.Text = lblRError.Text & "<br> - Debe de seleccionar el tipo de persona para guardar"
        If txtRApePat.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Es dato necesario saber el apellido de la persona."
        If txtRNombres.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Es dato necesario saber el nombre de la persona."
        If txtRAsunto.Text.Trim = "" Then lblRError.Text = lblRError.Text & "<br> - Es dato necesario saber que asunto se tratara en la cita"
        If cboRModoCita.SelectedValue = "(Seleccionar)" Then lblRError.Text = lblRError.Text & "<br> - Debe de seleccionar el Modo de la Cita para guardar"
        Dim Cn As New SqlConnection(Ruta_GrEmp)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cmdGlobal2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim psFecha As String = Right(txtRFecha.Text, 4) + Mid(txtRFecha.Text, 4, 2) + Left(txtRFecha.Text, 2)
        Dim psNroCita As String = ""
        Dim psAño As String = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
        Dim psHComienza As String = Left(cboRComienza.SelectedValue.Trim, 2) + Right(cboRComienza.SelectedValue.Trim, 2)
        Dim psHTermina As String = Left(cboRTermina.SelectedValue.Trim, 2) + Right(cboRTermina.SelectedValue.Trim, 2)
        Dim psUser As String = HttpContext.Current.User.Identity.Name
        Dim psNomPublico As String = ""
        Dim psSubTipo As String = ""
        Dim psCodTipoA As String = cboATAtencion.SelectedValue.Trim
        Dim psCodDato As String = ""
        Dim ValorSys As String = FechaActual() + HoraActual() + HttpContext.Current.User.Identity.Name
        If lblRError.Text <> "" Then
            lblRError.Text = "Existen las sgtes. observaciones: " & lblRError.Text
            Exit Sub
        End If
        Try
            Cn.Open() : cmdGlobal.Connection = Cn
            Cn2.Open() : cmdGlobal2.Connection = Cn2
            cmdGlobal.CommandText = "SELECT MAX(AGEN_NRO_CITA) FROM TBPERSONAL_AGENDA"
            Rs = cmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psNroCita = Nz(Rs(0)) + 1
                End While
            Else
                psNroCita = 1
            End If
            Rs.Close()
            If psCodTipoA = "4" Or psCodTipoA = "6" Or psCodTipoA = "7" Or psCodTipoA = "8" Or psCodTipoA = "9" Or psCodTipoA = "10" Or psCodTipoA = "11" Then
                psSubTipo = psCodTipoA
            Else
                psSubTipo = "NULL"
            End If
            Dim psCodPersona As String = ""
            If txtRCodRazon.Text = "" Then
                psCodPersona = "NULL"
            Else
                psCodPersona = txtRCodRazon.Text
            End If
            psNomPublico = txtRApePat.Text & " " & txtRApeMat.Text & ", " & txtRNombres.Text.Trim
            cmdGlobal.CommandText = " INSERT INTO TBPERSONAL_AGENDA (AGEN_AÑO, AGEN_PERSONAL, AGEN_TIPO_ATENCION,AGEN_FECHA, AGEN_NRO_CITA," _
                                  & " AGEN_HORA_INI,AGEN_HORA_FIN,AGEN_ASUNTO,AGEN_OBSERVACION, AGEN_SYS_EST, AGEN_USUARIO, AGEN_EMPRESA, " _
                                  & " AGEN_PUBLICO_NOMBRE,AGEN_PERSONAL_CODIGO,AGEN_GRAL_SUBTIPO_ATEN,AGEN_AREA,AGEN_MODO_CITA,AGEN_ESTADO,AGEN_CITA_REPROG,AGEN_TIPOPER, " _
                                  & " AGEN_NOMBRES, AGEN_APEPAT, AGEN_APEMAT) " _
                                  & " VALUES('" & psAño & "','" & txtRCodPersonal.Text.Trim & "','" & cboATAtencion.SelectedValue.Trim & "','" & psFecha & "'," & psNroCita & "," _
                                  & " '" & psHComienza & "','" & psHTermina & "','" & txtRAsunto.Text.Trim & "','" & txtRObs.Text.Trim & "','0','" & psUser & "', '" & txtREmpresa.Text.Trim & "'," _
                                  & " '" & psNomPublico & "'," & psCodPersona & "," & psSubTipo & "," & txtRCodArea.Text.Trim & ",'" & cboRModoCita.SelectedValue.Trim & "'," _
                                  & " '" & IIf(psFecha < FechaActual(), "2", "0") & "'," & IIf(chkCitaRepro.Checked = True, "'S'", "NULL") & ",'" & cboRTipoPer.SelectedValue.Trim & "', " _
                                  & " '" & txtRNombres.Text.Trim & "','" & txtRApePat.Text.Trim & "', '" & txtRApeMat.Text.Trim & "')"
            cmdGlobal.ExecuteNonQuery()
            If txtRGrabar.Text = "N" And cboRCita.SelectedValue.Trim = "3" And txtRCodRazon.Text.Trim <> "" Then
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
                                              & " DATOPER_NOMBRES = '" & txtRNombres.Text.Trim & "' " _
                                              & " WHERE DATOPER_CODIGO = " & txtRCodRazon.Text.Trim & " AND DATOPER_SYS_EST = '0' " _
                                              & " AND DATOPER_TIPO = '" & cboRTipoPer.SelectedValue.Trim & "'"
                        cmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
            ElseIf txtRGrabar.Text = "" And cboRCita.SelectedValue.Trim = "3" Then
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
                cmdGlobal.CommandText = " UPDATE TBPERSONAL_AGENDA SET AGEN_PERSONAL_CODIGO = " & psCodDato & " " _
                                      & " WHERE AGEN_NRO_CITA = " & psNroCita
                cmdGlobal.ExecuteNonQuery()
            End If
            Call btnRCancelar_Click(sender, e)
        Catch ex As SqlException
            lblRError.Text = "Ha ocurrido un error en la base de datos:" & ex.Message
        Catch Ex As Exception
            lblRError.Text = "Ha ocurrido un error en la aplicación:" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cboRCita_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboRCita.SelectedValue = "5" Then 'personal
            cboRTipoPer.Enabled = False : cboRTipoPer.SelectedValue = "< Seleccionar >"
            txtRApePat.ReadOnly = True : txtRApeMat.ReadOnly = True : txtRNombres.ReadOnly = True : txtREmpresa.ReadOnly = True
            txtRAsunto.Text = "" : txtRObs.Text = "" : txtRCodRazon.Text = "" : txtREmpresa.Text = ""
            cboRModoCita.SelectedValue = "(Seleccionar)"
            cboBusTipoPer.Items.Clear()
            cboBusTipoPer.Items.Add("< Seleccionar >") : cboBusTipoPer.SelectedValue = "< Seleccionar >"
            cboBusTipoPer.Enabled = False
            txtBusApePat.Text = ""
            txtRGrabar.Text = ""
            lblBP1.Text = "Listado del Personal"
        ElseIf cboRCita.SelectedValue = "3" Then
            cboRTipoPer.Enabled = True : cboRTipoPer.SelectedValue = "< Seleccionar >"
            txtRApePat.ReadOnly = False : txtRApeMat.ReadOnly = False : txtRNombres.ReadOnly = False : txtREmpresa.ReadOnly = False
            txtRAsunto.Text = "" : txtRObs.Text = "" : txtRCodRazon.Text = "" : txtREmpresa.Text = ""
            cboRModoCita.SelectedValue = "(Seleccionar)"
            Call LlenaComboItem("TBOPC001", cboBusTipoPer) : cboBusTipoPer.SelectedValue = "< Seleccionar >"
            cboBusTipoPer.Enabled = True
            txtBusApePat.Text = ""
            txtRGrabar.Text = ""
            lblBP1.Text = "Listado de Personas"
        End If
    End Sub
    Protected Sub btnRCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtRAsunto.Text = "" : txtRObs.Text = "" : txtREmpresa.Text = ""
        txtRApePat.Text = "" : txtRApeMat.Text = "" : txtRNombres.Text = ""
        cboRTipoPer.SelectedValue = "< Seleccionar >"
        cboRModoCita.SelectedValue = "(Seleccionar)"
        cboRCita.SelectedValue = "(Seleccionar)"
        cboRComienza.Items.Clear()
        cboRTermina.Items.Clear()
        txtRGrabar.Text = ""
        btnAListar_Click(sender, e)
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        'Ficha_ActiveTabChanged(sender, e)
    End Sub
    Protected Sub btnBPListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If cboRCita.SelectedValue = "3" Then
            Call Lista_Persona()
            ModalPopupExtender1.Show()
        ElseIf cboRCita.SelectedValue = "5" Then
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
    Protected Sub btnBPCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        cboBusTipoPer.SelectedValue = "< Seleccionar >"
        txtBusApePat.Text = ""
        FlexP.DataSource = Nothing
        FlexP.DataBind()
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub FlexP_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexP.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        txtRGrabar.Text = ""
        'lblPError.Text = ""
        'If FlexP.Rows(Index).Cells(2).Text = "SI" Then lblPError.Text = "El personal escogido ya se encuentra registrado como usuario del sistema." : Exit Sub
        If e.CommandName = "Aceptar" Then
            If cboRCita.SelectedValue = "3" Then
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
            ElseIf cboRCita.SelectedValue = "5" Then
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
    Protected Sub dtpFecha_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtAFecha.Text = Format(dtpFecha.SelectedDate, "dd/MM/yyyy")
        Call btnAListar_Click(sender, e)
    End Sub
    Protected Sub dtpFecha_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles dtpFecha.DayRender
        '
    End Sub
    Protected Sub cboATAtencion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim psAño As String = AñoActual(Session("CodEmpresa"), Session("Ruta_Emp"))
        btnAListar_Click(sender, e)
        If cboATAtencion.SelectedValue.Trim <> "< Seleccionar >" Then Call Llenar_Disponibilidad(psAño)
    End Sub
    Protected Sub FlexDispo_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexDispo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Cita" Then
            If txtAMinutoCita.Text.Trim = "" Then lblError.Text = "No puede hacer citas debido que no posee un Nro de Minutos aproximados por Cita" : Exit Sub
            If cboAPersonal.SelectedValue.Trim <> "< Seleccionar >" And cboATAtencion.SelectedValue.Trim <> "< Seleccionar >" Then
                Ficha.ActiveTabIndex = 0
                Ficha.ActiveTabIndex = 1
                Ficha.Enabled = True
                Ficha_ActiveTabChanged(sender, e)
                Call Hacer_Cita()
                Dim i As Long = 0
                cboRComienza.Items.Clear()
                cboRTermina.Items.Clear()
                Dim dtCombo As New DataTable : Dim drCombo As DataRow
                Dim a As Long = 0 : Dim cv As String = "" : Dim Variable As String = ""
                dtCombo.Columns.Add("c0")
                drCombo = dtCombo.NewRow()
                drCombo("c0") = FlexDispo.Rows(Index).Cells(1).Text
                dtCombo.Rows.Add(drCombo)
                Dim HoraIni As String = "" : Dim HoraFin As String = ""
                HoraIni = Left(FlexDispo.Rows(Index).Cells(1).Text, 2) + Right(FlexDispo.Rows(Index).Cells(1).Text, 2)
                HoraFin = Left(FlexDispo.Rows(Index).Cells(2).Text, 2) + Right(FlexDispo.Rows(Index).Cells(2).Text, 2)
                For a = Val(HoraIni) To Val(HoraFin) - 5 Step 5
                    Variable = a
                    cv = Llenar_Ceros(Variable, 4)
                    If Val(Right(cv, 2)) > 0 And Val(Right(cv, 2)) <= 55 Then
                        drCombo = dtCombo.NewRow()
                        drCombo("c0") = FormatoHora(cv)
                        dtCombo.Rows.Add(drCombo)
                    End If
                    If Val(Right(cv, 2)) = 55 Then
                        drCombo = dtCombo.NewRow()
                        drCombo("c0") = Llenar_Ceros(Val(Left(cv, 2)) + 1, 2) + ":" + "00"
                        dtCombo.Rows.Add(drCombo)
                    End If
                Next
                'dtCombo.Columns.Add("c0")
                drCombo = dtCombo.NewRow()
                drCombo("c0") = FlexDispo.Rows(Index).Cells(2).Text
                dtCombo.Rows.Add(drCombo)
                cboRComienza.DataSource = dtCombo
                cboRComienza.DataTextField = "c0"
                cboRComienza.DataValueField = "c0"
                cboRComienza.DataBind()
                cboRTermina.DataSource = dtCombo
                cboRTermina.DataTextField = "c0"
                cboRTermina.DataValueField = "c0"
                cboRTermina.DataBind()
                cboRComienza.SelectedValue = FlexDispo.Rows(Index).Cells(1).Text
                Dim Mx As Long = 0
                Dim FinCita As String = ""
                If FlexDispo.Rows(Index).Cells(1).Text <> "" Then
                    Mx = Val(Left(FlexDispo.Rows(Index).Cells(1).Text, 2)) * 60
                    Mx = Mx + Val(Right(FlexDispo.Rows(Index).Cells(1).Text, 2)) + Val(txtRMinCita.Text)
                    FinCita = Convertir_Hora(Mx)
                    FinCita = Left(FinCita, 2) + ":" + Right(FinCita, 2)
                End If
                cboRTermina.SelectedValue = FinCita
            End If
        End If
    End Sub

    Protected Sub FlexP_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub dtpFecha_SelectionChanged1(sender As Object, e As EventArgs) Handles dtpFecha.SelectionChanged

    End Sub

    Private Sub FlexMes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles FlexMes.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onMouseOver", "this.style.cursor='pointer'")
            Dim Index As Integer = 0
            For Each cell As TableCell In e.Row.Cells
                cell.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(FlexMes, "select$" & e.Row.RowIndex.ToString() & "," & Index))
                Index += 1
            Next
        End If
    End Sub
End Class
