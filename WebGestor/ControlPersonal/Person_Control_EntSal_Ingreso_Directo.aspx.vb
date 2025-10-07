Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Person_Control_EntSal_Ingreso_Directo
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        lblError.Text = ""
        Try
            btnGuardar.Enabled = False
            Call Pasar_Asistencia()
            Call Habilitar_Controles()
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Private Sub Habilitar_Controles()
        Dim hEnt As TextBox
        Dim hSal As TextBox
        Dim i As Integer = 0
        Try
            btnGuardar.Enabled = False
            For i = 0 To Flex.Rows.Count - 1
                hEnt = CType(Flex.Rows(i).Cells(11).FindControl("txtHEnt"), TextBox)
                hSal = CType(Flex.Rows(i).Cells(13).FindControl("txtHSal"), TextBox)
                If Left(Flex.Rows(i).Cells(7).Text, 1) = "S" Then
                    hEnt.Enabled = True : hEnt.Text = "__:__"
                    hSal.Enabled = True : hSal.Text = "__:__"
                Else
                    hEnt.Enabled = False : hEnt.Text = "__:__"
                    hSal.Enabled = False : hSal.Text = "__:__"
                End If
            Next
            For i = 0 To Flex.Rows.Count - 1
                'If Left(Flex.Rows(i).Cells(7).Text, 1) = "S" Then
                btnGuardar.Enabled = True
                '    Exit For
                'End If
            Next
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            txtFecha.Text = FormatoFecha(FechaActual)
            Dim nDia As Integer = Weekday(CDate(FormatoFecha(FechaActual)))
            txtDia.Text = Nombre_Dia(nDia, True)
        End If
    End Sub
    Private Sub Pasar_Asistencia()
        Dim cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim psFechaIni As String = ""
        Dim obj As New clsControlPersonal
        Dim dt As New DataTable
        Dim i As Integer = 0
        Dim Rs As SqlDataReader
        Dim dtListado As New DataTable
        Dim drT As DataRow
        cn.Open() : cmdGlobal.Connection = cn
        cn2.Open() : cmdGlobal2.Connection = cn2
        Dim psFecha As Date
        psFecha = txtFecha.Text.Trim
        If txtFecha.Text.Trim <> "" Then psFechaIni = Right(txtFecha.Text.Trim, 4) + Mid(txtFecha.Text.Trim, 4, 2) + Left(txtFecha.Text.Trim, 2)
        Dim nDia As Integer = Weekday(psFecha)
        dtListado.Columns.Add("c0")
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
        dtListado.Columns.Add("c16")
        dtListado.Columns.Add("c17")
        dtListado.Columns.Add("c18")
        dt = obj.Lista_Asistencia_Diferido(Session("CodEmpresa"), Session("CodGrupoEmpresa"))
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                i = i + 1
                drT = dtListado.NewRow()
                drT("c0") = i
                drT("c1") = Nu(dr("NOMBRESP"))
                drT("c2") = Nu(dr("IA_CODIGO"))
                If Nu(dr("IA_HORARIO_FIJO")) = "X" Then
                    drT("c3") = Left(Nu(dr("IA_HORA_ENTRADA")), 2) + ":" + Right(Nu(dr("IA_HORA_ENTRADA")), 2)
                    drT("c4") = Left(Nu(dr("IA_HORA_SALIDA")), 2) + ":" + Right(Nu(dr("IA_HORA_SALIDA")), 2)
                    drT("c5") = IIf(Nu(dr("IA_MINUTOS_TOLERANCIA")) = "", "", Llenar_Ceros(Nu(dr("IA_MINUTOS_TOLERANCIA")), 2))
                    drT("c6") = IIf(Nu(dr("IA_MINUTOS_REFRIGERIO")) = "", "", Llenar_Ceros(Nu(dr("IA_MINUTOS_REFRIGERIO")), 2))
                ElseIf Nu(dr("IA_HORARIO_VARIABLE")) = "X" Then
                    cmdGlobal.CommandText = "SELECT HV_HORA_ENTRADA, HV_HORA_SALIDA,HV_MINUTOS_TOLERANCIA,HV_MINUTOS_REFRIGERIO FROM TBINTEGRAN_ASISTENCIA_VARIABLE " _
                          & " WHERE (HV_PERSONAL = '" & Nu(dr("IA_CODIGO")) & "') AND (HV_NRO_DIA = '" & nDia & "') AND (HV_SYS_EST = '0') AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                    Rs = cmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            drT("c3") = Left(Nu(Rs("HV_HORA_ENTRADA")), 2) + ":" + Right(Nu(Rs("HV_HORA_ENTRADA")), 2)
                            drT("c4") = Left(Nu(Rs("HV_HORA_SALIDA")), 2) + ":" + Right(Nu(Rs("HV_HORA_SALIDA")), 2)
                            drT("c5") = IIf(Nu(Rs("HV_MINUTOS_TOLERANCIA")) = "", "", Llenar_Ceros(Nu(Rs("HV_MINUTOS_TOLERANCIA")), 2))
                            drT("c6") = IIf(Nu(Rs("HV_MINUTOS_REFRIGERIO")) = "", "", Llenar_Ceros(Nu(Rs("HV_MINUTOS_REFRIGERIO")), 2))
                        End While
                    End If
                    Rs.Close()
                End If
                dtListado.Rows.Add(drT)
            Next
        End If
        dt = Nothing
        Flex.DataSource = dtListado
        Flex.DataBind()
        Dim n As Integer = 0
        Dim a As Integer = 0
        cmdGlobal.CommandText = "SELECT ENTSAL_CODIGO,ENTSAL_INGRESO_HORA, ENTSAL_SALIDA_HORA,(SELECT PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES FROM TBPERSONAL WHERE PERSON_CODIGO=ENTSAL_CODIGO) AS NOMBRESP, " _
        & " ENTSAL_TARDE,ENTSAL_MIN_TARDE,ENTSAL_TIPOMARCADO_ENT,ENTSAL_TIPOMARCADO_SAL FROM TBREG_ENTSAL WHERE (ENTSAL_FECHA = '" & psFechaIni & "') AND (ENTSAL_TIPO = 'Normal') AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' ORDER BY NOMBRESP"
        Rs = cmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                n = 0
                For a = 0 To Flex.Rows.Count - 1
                    If Flex.Rows(a).Cells(2).Text = Nu(Rs!ENTSAL_CODIGO) Then
                        If Nu(Rs!ENTSAL_INGRESO_HORA) <> "" Then Flex.Rows(a).Cells(10).Text = Left(Nu(Rs!ENTSAL_INGRESO_HORA), 2) + ":" + Right(Nu(Rs!ENTSAL_INGRESO_HORA), 2)
                        If Nu(Rs!ENTSAL_SALIDA_HORA) <> "" Then Flex.Rows(a).Cells(12).Text = Left(Nu(Rs!ENTSAL_SALIDA_HORA), 2) + ":" + Right(Nu(Rs!ENTSAL_SALIDA_HORA), 2)
                        If Nu(Rs!ENTSAL_INGRESO_HORA) <> "" Or Nu(Rs!ENTSAL_SALIDA_HORA) <> "" Then Flex.Rows(a).Cells(7).Text = "Sí"
                        Flex.Rows(a).Cells(20).Text = Nu(Rs!ENTSAL_TIPOMARCADO_ENT)
                        Flex.Rows(a).Cells(21).Text = Nu(Rs!ENTSAL_TIPOMARCADO_SAL)
                        n = 1
                        'GoTo ABC
                    End If
                Next
                'ABC:
                ''NO ENCONTRÓ AL PERSONAL
                'If n = 0 Then
                '    i = i + 1
                '    drT = dtListado.NewRow()
                '    drT("c0") = i
                '    .TextMatrix(.Rows - 1, 1) = Nu(Rs!NOMBRESP) : .Col = 1 : .Row = .Rows - 1 : .CellForeColor = lblColor1.BackColor : .Col = 2 : .CellForeColor = lblColor1.BackColor
                '    .TextMatrix(.Rows - 1, 2) = Nu(Rs!ENTSAL_CODIGO)
                '    If Nu(Rs!ENTSAL_INGRESO_HORA) <> "" Then .TextMatrix(.Rows - 1, 8) = Left(Nu(Rs!ENTSAL_INGRESO_HORA), 2) + ":" + Right(Nu(Rs!ENTSAL_INGRESO_HORA), 2)
                '    If Nu(Rs!ENTSAL_SALIDA_HORA) <> "" Then .TextMatrix(.Rows - 1, 9) = Left(Nu(Rs!ENTSAL_SALIDA_HORA), 2) + ":" + Right(Nu(Rs!ENTSAL_SALIDA_HORA), 2)
                '    If Nu(Rs!ENTSAL_INGRESO_HORA) <> "" Or Nu(Rs!ENTSAL_SALIDA_HORA) <> "" Then .TextMatrix(.Rows - 1, 7) = "Sí"
                '    .TextMatrix(.Rows - 1, 19) = Nu(Rs!ENTSAL_TIPOMARCADO_ENT)
                '    .TextMatrix(.Rows - 1, 20) = Nu(Rs!ENTSAL_TIPOMARCADO_SAL)
                '    dtListado.Rows.Add(drT)
                'End If
            End While
        End If
        Rs.Close()
        Call Calcula_Min_Entrada(0, Flex.Rows.Count - 1)
        Call Calcula_Min_Salida(0, Flex.Rows.Count - 1)
        Call Calcula_Horas_Trabajadas(0, Flex.Rows.Count - 1, 0)
    End Sub
    Private Sub Calcula_Min_Entrada(ByVal I1 As Integer, ByVal I2 As Integer)
        Dim HEntTrab, HEntCol As String
        Dim MinDife, ii As Integer
        With Flex
            For ii = I1 To I2
                .Rows(ii).Cells(14).Text = ""
                HEntTrab = .Rows(ii).Cells(3).Text
                HEntCol = .Rows(ii).Cells(10).Text
                If HEntTrab <> "&nbsp;" And HEntCol <> "&nbsp;" Then
                    MinDife = ((Val(Left(HEntCol, 2)) * 60) + Val(Right(HEntCol, 2))) - ((Val(Left(HEntTrab, 2)) * 60) + Val(Right(HEntTrab, 2)))
                    If MinDife < 0 Then
                        .Rows(ii).Cells(14).Text = MinDife * -1 & " min. Temprano" : Flex.Rows(ii).Cells(14).ForeColor = Drawing.Color.Black
                        .Rows(ii).Cells(16).Text = "N" : .Rows(ii).Cells(17).Text = ""
                    ElseIf MinDife = 0 Then
                        .Rows(ii).Cells(14).Text = "Hora Exacta" : Flex.Rows(ii).Cells(14).ForeColor = Drawing.Color.Black
                        .Rows(ii).Cells(16).Text = "N" : .Rows(ii).Cells(17).Text = ""
                    Else
                        If .Rows(ii).Cells(5).Text <> "&nbsp;" Then
                            If Val(.Rows(ii).Cells(5).Text) >= MinDife Then
                                .Rows(ii).Cells(14).Text = "Ingreso a Tiempo" : Flex.Rows(ii).Cells(14).ForeColor = Drawing.Color.Black
                                .Rows(ii).Cells(16).Text = "N" : .Rows(ii).Cells(17).Text = ""
                            Else
                                .Rows(ii).Cells(14).Text = MinDife & " min. Tarde" : Flex.Rows(ii).Cells(14).ForeColor = Drawing.Color.Red
                                .Rows(ii).Cells(16).Text = "S" : .Rows(ii).Cells(17).Text = MinDife
                            End If
                        Else
                            .Rows(ii).Cells(14).Text = MinDife & " min. Tarde" : Flex.Rows(ii).Cells(14).ForeColor = Drawing.Color.Red
                            .Rows(ii).Cells(16).Text = "S" : .Rows(ii).Cells(17).Text = MinDife
                        End If
                    End If
                End If
            Next
        End With
    End Sub
    Private Sub Calcula_Min_Salida(ByVal I1 As Integer, ByVal I2 As Integer)
        Dim HSalTrab, HSalCol As String
        Dim MinDife, ii As Integer
        With Flex
            For ii = I1 To I2
                .Rows(ii).Cells(15).Text = ""
                HSalTrab = .Rows(ii).Cells(4).Text
                HSalCol = .Rows(ii).Cells(12).Text
                If HSalTrab <> "&nbsp;" And HSalCol <> "&nbsp;" Then
                    MinDife = ((Val(Left(HSalCol, 2)) * 60) + Val(Right(HSalCol, 2))) - ((Val(Left(HSalTrab, 2)) * 60) + Val(Right(HSalTrab, 2)))
                    If MinDife < 0 Then
                        .Rows(ii).Cells(15).Text = MinDife * -1 & " min. Antes" : Flex.Rows(ii).Cells(15).ForeColor = Drawing.Color.Red
                    ElseIf MinDife = 0 Then
                        .Rows(ii).Cells(15).Text = "Hora Exacta" : Flex.Rows(ii).Cells(15).ForeColor = Drawing.Color.Black
                    Else
                        .Rows(ii).Cells(15).Text = MinDife & " min. después" : Flex.Rows(ii).Cells(15).ForeColor = Drawing.Color.Black
                    End If
                End If
            Next
        End With
    End Sub
    Private Sub Calcula_Horas_Trabajadas(ByVal I1 As Integer, ByVal I2 As Integer, ByVal ColPosMouse As Integer)
        Dim HIniTrab, HFinTrab As String
        Dim MinTotales, Min, Hor, ii As Integer
        With Flex
            For ii = I1 To I2
                .Rows(ii).Cells(18).Text = ""
                .Rows(ii).Cells(19).Text = ""
                If .Rows(ii).Cells(3).Text <> "&nbsp;" And .Rows(ii).Cells(4).Text <> "&nbsp;" Then ' si hay un horario de trabajo
                    If .Rows(ii).Cells(10).Text <> "" And .Rows(ii).Cells(12).Text <> "" Then ' si hay un hora de entrada y salida
                        If Val(Left(.Rows(ii).Cells(12).Text, 2) & Right(.Rows(ii).Cells(12).Text, 2)) <= Val(Left(.Rows(ii).Cells(4).Text, 2) & Right(.Rows(ii).Cells(4).Text, 2)) Then 'si la salida de la empresa <= a la salida normal
                            HFinTrab = .Rows(ii).Cells(12).Text 'salida de la empresa
                        Else
                            HFinTrab = .Rows(ii).Cells(4).Text 'salida normal
                        End If
                        If Val(Left(.Rows(ii).Cells(10).Text, 2) & Right(.Rows(ii).Cells(10).Text, 2)) <= Val(Left(.Rows(ii).Cells(3).Text, 2) & Right(.Rows(ii).Cells(3).Text, 2)) Then 'si el ingreso a la empresa <= a la ingreso normal
                            HIniTrab = .Rows(ii).Cells(3).Text 'ingreso normal
                        Else
                            HIniTrab = .Rows(ii).Cells(10).Text 'ingreso a la empresa
                        End If
                        If HFinTrab <> "&nbsp;" And HIniTrab <> "&nbsp;" Then
                            MinTotales = (((Val(Left(HFinTrab, 2)) * 60) + Val(Right(HFinTrab, 2))) - ((Val(Left(HIniTrab, 2)) * 60) + Val(Right(HIniTrab, 2)))) - (Val(.Rows(ii).Cells(6).Text) * 60)
                            Min = MinTotales Mod 60
                            Hor = MinTotales \ 60
                            .Rows(ii).Cells(18).Text = Llenar_Ceros(Hor, 2) & ":" & Llenar_Ceros(Min, 2)
                        End If
                    End If
                    'hrs_extras=hora_salida- horario_salida, hora_salida > horario_salida
                    If .Rows(ii).Cells(11).Text <> "&nbsp;" Then
                        If Val(Left(.Rows(ii).Cells(12).Text, 2) & Right(.Rows(ii).Cells(12).Text, 2)) > Val(Left(.Rows(ii).Cells(4).Text, 2) & Right(.Rows(ii).Cells(4).Text, 2)) Then
                            MinTotales = (((Val(Left(.Rows(ii).Cells(12).Text, 2)) * 60) + Val(Right(.Rows(ii).Cells(12).Text, 2))) - ((Val(Left(.Rows(ii).Cells(4).Text, 2)) * 60) + Val(Right(.Rows(ii).Cells(4).Text, 2))))
                            Min = MinTotales Mod 60
                            Hor = MinTotales \ 60
                            .Rows(ii).Cells(19).Text = Llenar_Ceros(Hor, 2) & ":" & Llenar_Ceros(Min, 2)
                        End If
                    End If
                End If
            Next
        End With
    End Sub
    Protected Sub btnMasivo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMasivo.Click
        Dim hSal As TextBox
        Dim ii As Integer
        With Flex
            For ii = 0 To .Rows.Count - 1
                hSal = CType(Flex.Rows(ii).Cells(13).FindControl("txtHSal"), TextBox)
                If .Rows(ii).Cells(3).Text <> "&nbsp;" And .Rows(ii).Cells(4).Text <> "&nbsp;" Then
                    If Left(.Rows(ii).Cells(7).Text, 1) = "S" And .Rows(ii).Cells(10).Text <> "&nbsp;" And .Rows(ii).Cells(12).Text = "&nbsp;" Then
                        hSal.Text = .Rows(ii).Cells(4).Text
                        .Rows(ii).Cells(22).Text = "V"
                        .Rows(ii).Cells(21).Text = "MDM"
                    End If
                End If
            Next
        End With
        btnGuardar.Enabled = True
        Call Calcula_Min_Salida(0, Flex.Rows.Count - 1)
        Call Calcula_Horas_Trabajadas(0, Flex.Rows.Count - 1, 0)
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        lblError.Text = ""
        Dim Rs As SqlDataReader
        Dim cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim ValorSys As String
        Dim h1, h2 As String
        Dim i As Integer = 0
        Dim psFechaIni As String = ""
        Dim hEnt As TextBox
        Dim hSal As TextBox
        If txtFecha.Text.Trim <> "" Then psFechaIni = Right(txtFecha.Text.Trim, 4) + Mid(txtFecha.Text.Trim, 4, 2) + Left(txtFecha.Text.Trim, 2)
        Try
            With Flex
                cn.Open() : cmdGlobal.Connection = cn
                cn2.Open() : cmdGlobal2.Connection = cn2
                If .Rows.Count > 0 Then
                    For i = 0 To .Rows.Count - 1
                        hEnt = CType(Flex.Rows(i).Cells(11).FindControl("txtHEnt"), TextBox)
                        hSal = CType(Flex.Rows(i).Cells(13).FindControl("txtHSal"), TextBox)
                        If hEnt.Text <> "__:__" Or hSal.Text <> "__:__" Then
                            .Rows(i).Cells(22).Text = "V"
                        End If
                    Next
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Cells(22).Text = "V" Then
                            If Left(.Rows(i).Cells(7).Text, 1) = "S" Then
                                hEnt = CType(Flex.Rows(i).Cells(11).FindControl("txtHEnt"), TextBox)
                                hSal = CType(Flex.Rows(i).Cells(13).FindControl("txtHSal"), TextBox)
                                If hEnt.Text = "__:__" And hSal.Text = "__:__" Then
                                    lblError.Text = "Si asistió, falta ingresar la hora de entrada y/o salida"
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                    ValorSys = FechaActual() & HoraActual() & Session("User")
                    For i = 0 To .Rows.Count - 1
                        If .Rows(i).Cells(22).Text = "V" Then
                            hEnt = CType(Flex.Rows(i).Cells(11).FindControl("txtHEnt"), TextBox)
                            hSal = CType(Flex.Rows(i).Cells(13).FindControl("txtHSal"), TextBox)
                            h1 = IIf(hEnt.Text = "__:__", "", Left(hEnt.Text, 2) & Right(hEnt.Text, 2))
                            h2 = IIf(hSal.Text = "__:__", "", Left(hSal.Text, 2) & Right(hSal.Text, 2))
                            If h1 = "" Then h1 = Left(Flex.Rows(i).Cells(10).Text, 2) & Right(Flex.Rows(i).Cells(10).Text, 2)
                            If h2 = "" Then h2 = Left(Flex.Rows(i).Cells(12).Text, 2) & Right(Flex.Rows(i).Cells(12).Text, 2)
                            If h1 = "" And h2 = "" Then
                                cmdGlobal.CommandText = "DELETE FROM TBREG_ENTSAL WHERE (ENTSAL_FECHA = '" & psFechaIni & "') AND (ENTSAL_CODIGO='" & .Rows(i).Cells(2).Text & "') AND (ENTSAL_TIPO = 'Normal') AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                                cmdGlobal.ExecuteNonQuery()
                            Else
                                cmdGlobal.CommandText = "SELECT * FROM TBREG_ENTSAL WHERE (ENTSAL_FECHA = '" & psFechaIni & "') AND (ENTSAL_CODIGO='" & .Rows(i).Cells(2).Text & "') AND (ENTSAL_TIPO = 'Normal') AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                                Rs = cmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        cmdGlobal2.CommandText = "UPDATE TBREG_ENTSAL SET ENTSAL_INGRESO_HORA='" & h1 & "',ENTSAL_SALIDA_HORA='" & h2 & "',ENTSAL_SYS_EST='0',ENTSAL_SYS_MOD='" & ValorSys & "'," _
                                        & "ENTSAL_TIPOMARCADO_SAL='" & .Rows(i).Cells(21).Text & "',ENTSAL_TIPOMARCADO_ENT='" & .Rows(i).Cells(20).Text & "' " _
                                        & " WHERE (ENTSAL_FECHA = '" & psFechaIni & "') AND (ENTSAL_CODIGO='" & .Rows(i).Cells(2).Text & "') AND (ENTSAL_TIPO = 'Normal') AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                                        cmdGlobal2.ExecuteNonQuery()
                                    End While
                                    Rs.Close()
                                Else
                                    Rs.Close()
                                    cmdGlobal2.CommandText = "INSERT INTO TBREG_ENTSAL(GRPOEMPRESA_CODIGO,EMPRESA_CODIGO,ENTSAL_CODIGO, ENTSAL_FECHA, ENTSAL_CONTAR_TIPO,ENTSAL_TIPO, ENTSAL_INGRESO_HORA,ENTSAL_SALIDA_HORA, ENTSAL_SYS_EST,ENTSAL_SYS_CRE,ENTSAL_TIPOMARCADO_SAL,ENTSAL_TIPOMARCADO_ENT) " _
                                    & " VALUES (" & Session("CodGrupoEmpresa") & ",'" & Session("CodEmpresa") & "','" & .Rows(i).Cells(2).Text & "', '" & psFechaIni & "',1, 'Normal', '" & h1 & "','" & h2 & "', '0', '" & ValorSys & "','" & .Rows(i).Cells(21).Text & "','" & .Rows(i).Cells(20).Text & "')"
                                    cmdGlobal2.ExecuteNonQuery()
                                End If
                            End If
                        End If
                    Next
                    Dim hEx As String = ""
                    Dim psTarde As String = "NULL"
                    Dim minTarde As String = "NULL"
                    For i = 0 To .Rows.Count - 1
                        hEx = ""
                        psTarde = ""
                        minTarde = "NULL"
                        If Replace(.Rows(i).Cells(19).Text, "&nbsp;", "") <> "" Then hEx = Left(.Rows(i).Cells(19).Text, 2) & Right(.Rows(i).Cells(19).Text, 2)
                        If Replace(.Rows(i).Cells(16).Text, "&nbsp;", "") <> "" Then psTarde = .Rows(i).Cells(16).Text
                        If Replace(.Rows(i).Cells(17).Text, "&nbsp;", "") <> "" Then minTarde = Left(.Rows(i).Cells(19).Text, 2) & Right(.Rows(i).Cells(17).Text, 2)
                        cmdGlobal.CommandText = " UPDATE TBREG_ENTSAL SET ENTSAL_TARDE='" & psTarde & "', " _
                                                  & " ENTSAL_MIN_TARDE=" & minTarde & ", " _
                                                  & " ENTSAL_HRS_EXTRAS='" & hEx & "' " _
                                                  & " WHERE (ENTSAL_FECHA = '" & psFechaIni & "') AND (ENTSAL_TIPO = 'Normal') " _
                                                  & " AND (ENTSAL_CODIGO='" & .Rows(i).Cells(2).Text & "') " _
                                                  & " AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " " _
                                                  & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                        cmdGlobal.ExecuteNonQuery()
                    Next
                End If
            End With
            btnListar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Try
            If e.CommandName = "Si" Then
                Flex.Rows(Index).Cells(7).Text = "Sí"
                btnGuardar.Enabled = True
                Flex.Rows(Index).Cells(10).Text = ""
                Flex.Rows(Index).Cells(12).Text = ""
                Flex.Rows(Index).Cells(14).Text = ""
                Flex.Rows(Index).Cells(15).Text = ""
                Flex.Rows(Index).Cells(16).Text = ""
                Flex.Rows(Index).Cells(17).Text = ""
                Flex.Rows(Index).Cells(18).Text = ""
                Flex.Rows(Index).Cells(19).Text = ""
                Flex.Rows(Index).Cells(20).Text = ""
                Flex.Rows(Index).Cells(21).Text = ""
                Flex.Rows(Index).Cells(22).Text = "V"
            ElseIf e.CommandName = "No" Then
                Flex.Rows(Index).Cells(7).Text = "No"
                btnGuardar.Enabled = True
                Flex.Rows(Index).Cells(10).Text = ""
                Flex.Rows(Index).Cells(12).Text = ""
                Flex.Rows(Index).Cells(14).Text = ""
                Flex.Rows(Index).Cells(15).Text = ""
                Flex.Rows(Index).Cells(16).Text = ""
                Flex.Rows(Index).Cells(17).Text = ""
                Flex.Rows(Index).Cells(18).Text = ""
                Flex.Rows(Index).Cells(19).Text = ""
                Flex.Rows(Index).Cells(20).Text = ""
                Flex.Rows(Index).Cells(21).Text = ""
                Flex.Rows(Index).Cells(22).Text = "V"
            End If
            Call Habilitar_Controles()
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub txtFecha_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFecha.TextChanged
        Dim psFecha As Date
        psFecha = txtFecha.Text.Trim
        Dim nDia As Integer = Weekday(psFecha)
        txtDia.Text = Nombre_Dia(nDia, True)
    End Sub
End Class
