Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Person_Record_Asistencia
    Inherits System.Web.UI.Page
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            Flex.DataSource = Nothing
            Flex.DataBind()
            FlexRecord.DataSource = Nothing
            FlexRecord.DataBind()
            FlexDetalle.DataSource = Nothing
            FlexDetalle.DataBind()
            Call Listar_Asistencia()
            FlexRecord.DataSource = Listar_record()
            FlexRecord.DataBind()
            lblRegistro.Text = "Se han encontrado " & FlexRecord.Rows.Count & " registros."
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub Exportar_Excel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Flex.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(FlexDetalle)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Record de Asistencia.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Function Listar_record() As DataTable
        Listar_record = Nothing
        Dim Rs As SqlDataReader
        Dim nDiasTarde As Double : nDiasTarde = 0
        Dim MinTarde As Double : MinTarde = 0
        Dim MinTrabajados As Double : MinTrabajados = 0
        Dim MinExtras As Double : MinExtras = 0
        Dim MinDife As Double : MinDife = 0
        Dim MinExtrasServicio As Double : MinExtrasServicio = 0
        Dim cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim psFechaIni As String = ""
        Dim psFechaFin As String = ""
        If txtFechaIni.Text.Trim <> "" Then psFechaIni = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
        If txtFechaFin.Text.Trim <> "" Then psFechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
        cn.Open() : cmdGlobal.Connection = cn
        cn2.Open() : cmdGlobal2.Connection = cn2
        If Existe_Tabla("V_RECORD_ASISTENCIA_" & Session("CodEmpresa") & Session("User") & "", Ruta_GrEmp) = True Then
            cmdGlobal.CommandText = " drop TABLE [dbo].[V_RECORD_ASISTENCIA_" & Session("CodEmpresa") & Session("User") & "] "
            cmdGlobal.ExecuteNonQuery()
        End If
        If Existe_Tabla("V_RECORD_ASISTENCIA_" & Session("CodEmpresa") & Session("User") & "", Ruta_GrEmp) = False Then
            cmdGlobal.CommandText = " CREATE TABLE [dbo].[V_RECORD_ASISTENCIA_" & Session("CodEmpresa") & Session("User") & "] ([RECORD_USUARIO] [varchar] (8) NULL , " _
                                  & " [RECORD_NOMBRE] [varchar] (150) NULL ,[RECORD_CARGO] [VARCHAR] (150) NULL, [RECORD_DIASTRABAJADOS] [FLOAT] NULL , " _
                                  & " [RECORD_DIASTARDANZAS] [FLOAT] NULL ,[RECORD_MINTARDANZAS] [FLOAT] NULL , " _
                                  & " [RECORD_HORASTRABAJADAS] [FLOAT] NULL ,[RECORD_HORASEXTRAS] [float] NULL,[RECORD_HORASEXTRAS_SERVICIO] [float] NULL,[latitud] [decimal] (10,8) NULL,[longitud] [decimal] (10,8) NULL) ON [PRIMARY]"
            cmdGlobal.ExecuteNonQuery()
        End If
        cmdGlobal.CommandText = " DELETE FROM V_RECORD_ASISTENCIA_" & Session("CodEmpresa") & Session("User")
        cmdGlobal.ExecuteNonQuery()
        cmdGlobal.CommandText = " SELECT DISTINCT ES.ENTSAL_CODIGO, P.PERSON_APEPAT + ' ' + P.PERSON_APEMAT + ', ' + P.PERSON_NOMBRES AS NOMBRESP," _
                              & " (SELECT CARGO_NOMBRE From dbo.TBPERSONAL_DEFINE_CARGO WHERE (CARGO_CODIGO = PE.PERSON_CARGO) AND (GRPOEMPRESA_CODIGO = " & Session("CodGrupoEmpresa") & ") AND (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')) AS CARGO, " _
                              & " ES.ENTSAL_TIPO, (SELECT DISTINCT COUNT(ENTSAL_CODIGO) FROM dbo.TBREG_ENTSAL AS ES1 WHERE "
        If psFechaIni = "" And psFechaFin = "" Then
        Else
            If psFechaIni <> "" And psFechaFin = "" Then
                cmdGlobal.CommandText = cmdGlobal.CommandText & " (ES1.ENTSAL_FECHA='" & psFechaIni & "') AND"
            ElseIf txtFechaIni.Text.Trim = "" And psFechaFin <> "" Then
                cmdGlobal.CommandText = cmdGlobal.CommandText & " (ES1.ENTSAL_FECHA='" & psFechaFin & "') AND "
            Else
                cmdGlobal.CommandText = cmdGlobal.CommandText & " (ES1.ENTSAL_FECHA BETWEEN '" & psFechaIni & "' AND '" & psFechaFin & "') AND"
            End If
        End If
        cmdGlobal.CommandText = cmdGlobal.CommandText & " (ENTSAL_CODIGO = ES.ENTSAL_CODIGO) AND (ENTSAL_TARDE = 'S') AND (ENTSAL_TIPO = 'Normal') AND (GRPOEMPRESA_CODIGO = ES.GRPOEMPRESA_CODIGO) AND (EMPRESA_CODIGO = ES.EMPRESA_CODIGO)) AS NTARDANZA, " _
                              & " (SELECT COUNT(DISTINCT  ENTSAL_FECHA) FROM dbo.TBREG_ENTSAL AS ES1 WHERE "
        If psFechaIni = "" And psFechaFin = "" Then
        Else
            If psFechaFin = "" And psFechaIni <> "" Then
                cmdGlobal.CommandText = cmdGlobal.CommandText & " (ES1.ENTSAL_FECHA='" & psFechaIni & "') AND"
            ElseIf psFechaIni = "" And psFechaFin <> "" Then
                cmdGlobal.CommandText = cmdGlobal.CommandText & " (ES1.ENTSAL_FECHA='" & psFechaFin & "') AND "
            Else
                cmdGlobal.CommandText = cmdGlobal.CommandText & " (ES1.ENTSAL_FECHA BETWEEN '" & psFechaIni & "' AND '" & psFechaFin & "') AND"
            End If
        End If
        cmdGlobal.CommandText = cmdGlobal.CommandText & " (ENTSAL_CODIGO = ES.ENTSAL_CODIGO) AND (GRPOEMPRESA_CODIGO = ES.GRPOEMPRESA_CODIGO) AND (EMPRESA_CODIGO = ES.EMPRESA_CODIGO)) AS NDIASTRABAJADAS,es.entsal_latitud as latitud, es.entsal_longitud as longitud  " _
                              & " FROM dbo.TBREG_ENTSAL AS ES INNER JOIN dbo.TBPERSONAL_EMPRESAS AS PE ON ES.ENTSAL_CODIGO = PE.PERSONAL_CODIGO AND " _
                              & " ES.GRPOEMPRESA_CODIGO = PE.GRPOEMPRESA_CODIGO AND ES.EMPRESA_CODIGO = PE.EMPRESA_CODIGO INNER JOIN dbo.TBPERSONAL AS P ON PE.PERSONAL_CODIGO = P.PERSON_CODIGO " _
                              & " WHERE (ES.ENTSAL_TIPO = 'Normal') AND (ES.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (ES.ENTSAL_SYS_EST = '0') AND " _
                              & " (ES.GRPOEMPRESA_CODIGO = " & Session("CodGrupoEmpresa") & " )"
        If psFechaIni = "" And psFechaFin = "" Then
        Else
            If psFechaFin = "" And psFechaIni <> "" Then
                cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENTSAL_FECHA='" & psFechaIni & "')"
            ElseIf psFechaIni = "" And psFechaFin <> "" Then
                cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENTSAL_FECHA='" & psFechaFin & "')"
            Else
                cmdGlobal.CommandText = cmdGlobal.CommandText & " AND (ENTSAL_FECHA BETWEEN '" & psFechaIni & "' AND '" & psFechaFin & "')"
            End If
        End If
        cmdGlobal.CommandText = cmdGlobal.CommandText & " ORDER BY ES.ENTSAL_CODIGO"
        Dim i As Integer = 0
        Rs = cmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                MinTarde = 0 : MinTrabajados = 0 : MinExtras = 0 : nDiasTarde = 0 : MinDife = 0 : MinExtrasServicio = 0
                For i = 0 To Flex.Rows.Count - 1
                    If Flex.Rows(i).Cells(2).Text = Nu(Rs!ENTSAL_CODIGO) Then
                        MinTarde = MinTarde + Val(Flex.Rows(i).Cells(8).Text)
                        MinTrabajados = MinTrabajados + Val(Flex.Rows(i).Cells(9).Text)
                        MinExtras = MinExtras + Val(Flex.Rows(i).Cells(10).Text)
                        MinExtrasServicio = MinExtrasServicio + Val(Flex.Rows(i).Cells(16).Text)
                        MinDife = ((Val(Left(Flex.Rows(i).Cells(6).Text, 2)) * 60) + Val(Right(Flex.Rows(i).Cells(6).Text, 2))) - ((Val(Left(Flex.Rows(i).Cells(4).Text, 2)) * 60) + Val(Right(Flex.Rows(i).Cells(4).Text, 2)))
                        If Flex.Rows(i).Cells(12).Text = "Normal" Then
                            If CDbl(Nz(Replace(Flex.Rows(i).Cells(15).Text, "&nbsp;", ""))) < MinDife Then nDiasTarde = nDiasTarde + 1
                        End If
                    End If
                Next
                cmdGlobal2.CommandText = " INSERT INTO V_RECORD_ASISTENCIA_" & Session("CodEmpresa") & Session("User") & " (RECORD_USUARIO,RECORD_NOMBRE,RECORD_CARGO,RECORD_DIASTRABAJADOS,RECORD_DIASTARDANZAS,RECORD_MINTARDANZAS,RECORD_HORASTRABAJADAS,RECORD_HORASEXTRAS,RECORD_HORASEXTRAS_SERVICIO, latitud,longitud)" _
                                      & " VALUES ('" & Nu(Rs!ENTSAL_CODIGO) & "','" & Nu(Rs!NOMBRESP) & "','" & Nu(Rs!Cargo) & "'," & Nz(Rs!NDIASTRABAJADAS) & "," & nDiasTarde & "," & MinTarde & "," & MinTrabajados & "," & MinExtras & "," & MinExtrasServicio & "," & Nz(Rs!latitud) & "," & Nz(Rs!longitud) & ")"
                cmdGlobal2.ExecuteNonQuery()
            End While
        End If
        Rs.Close()
        Dim hora As Double
        Dim Min As Double
        Dim dtListado As New DataTable
        Dim drT As DataRow
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
        Min = 0 : hora = 0 : i = 0
        cmdGlobal.CommandText = " SELECT RECORD_USUARIO, RECORD_NOMBRE, RECORD_CARGO, RECORD_DIASTRABAJADOS, RECORD_DIASTARDANZAS, RECORD_MINTARDANZAS,RECORD_HORASEXTRAS_SERVICIO, " _
                              & " RECORD_HORASTRABAJADAS , RECORD_HORASEXTRAS,latitud, longitud From dbo.V_RECORD_ASISTENCIA_" & Session("CodEmpresa") & Session("User")
        Rs = cmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                i = i + 1
                drT = dtListado.NewRow()
                drT("c0") = i
                drT("c1") = Nu(Rs!RECORD_USUARIO)
                drT("c2") = Nu(Rs!RECORD_NOMBRE)
                drT("c3") = Nu(Rs!RECORD_CARGO)
                drT("c4") = Nu(Rs!RECORD_DIASTRABAJADOS)
                drT("c5") = Nu(Rs!RECORD_DIASTARDANZAS)
                Min = 0 : hora = 0
                Min = Val(Nz(Rs!RECORD_MINTARDANZAS)) Mod 60
                hora = Val(Nz(Rs!RECORD_MINTARDANZAS)) \ 60
                drT("c7") = Nz(Rs!RECORD_MINTARDANZAS) & " minutos"
                Min = 0 : hora = 0
                Min = Val(Nz(Rs!RECORD_HORASTRABAJADAS)) Mod 60
                hora = Val(Nz(Rs!RECORD_HORASTRABAJADAS)) \ 60
                drT("c6") = Llenar_Ceros(hora, 2) & " Horas y " & Llenar_Ceros(Min, 2) & " minutos"
                Min = 0 : hora = 0
                Min = Val(Nz(Rs!RECORD_HORASEXTRAS)) Mod 60
                hora = Val(Nz(Rs!RECORD_HORASEXTRAS)) \ 60
                drT("c8") = Llenar_Ceros(hora, 2) & " Horas y " & Llenar_Ceros(Min, 2) & " minutos"
                Min = 0 : hora = 0
                Min = Val(Nz(Rs!RECORD_HORASEXTRAS_SERVICIO)) Mod 60
                hora = Val(Nz(Rs!RECORD_HORASEXTRAS_SERVICIO)) \ 60
                drT("c9") = Llenar_Ceros(hora, 2) & " Horas y " & Llenar_Ceros(Min, 2) & " minutos"
                dtListado.Rows.Add(drT)
            End While
        End If
        Rs.Close()
        Return dtListado
    End Function
    Function Pasar_Asistemcia() As DataTable
        Dim Fecha As Date
        Dim numeroD As String
        Dim cn As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal As New SqlCommand
        Dim cn2 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal2 As New SqlCommand
        Dim cn3 As New SqlConnection(Ruta_GrEmp)
        Dim cmdGlobal3 As New SqlCommand
        Dim psFechaIni As String = "20110101"
        Dim psFechaFin As String = "21001231"
        Dim obj As New clsControlPersonal
        Dim drT As DataRow
        Dim dtListado As New DataTable
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        cn.Open() : cmdGlobal.Connection = cn
        cn2.Open() : cmdGlobal2.Connection = cn2
        cn3.Open() : cmdGlobal2.Connection = cn3
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
        If txtFechaIni.Text.Trim <> "" And txtFechaFin.Text.Trim = "" Then
            psFechaIni = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
            psFechaFin = psFechaIni
        ElseIf txtFechaFin.Text.Trim <> "" And txtFechaIni.Text.Trim = "" Then
            psFechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
            psFechaIni = psFechaFin
        ElseIf txtFechaFin.Text.Trim <> "" And txtFechaIni.Text.Trim <> "" Then
            psFechaIni = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
            psFechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
        Else
            psFechaIni = "20110101"
            psFechaFin = "21001231"
        End If
        Dim i As Integer = 0
        dt = obj.Lista_Asistencia(Session("CodEmpresa"), Session("CodGrupoEmpresa"), psFechaIni, psFechaFin, "")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                i = i + 1
                drT = dtListado.NewRow()
                drT("c0") = i
                drT("c1") = Nu(dr("NOMBRESP"))
                drT("c2") = Nu(dr("ENTSAL_CODIGO"))
                drT("c3") = Nu(dr("Tipo")) 'ENTSAL_TIPO
                drT("c12") = Nu(dr("ENTSAL_TIPO"))
                drT("c14") = Nu(dr("ENTSAL_FECHA"))
                If Nu(dr("FIJO")) = "X" Then
                    drT("c4") = Left(Nu(dr("HORA_ENTRADA")), 2) + ":" + Right(Nu(dr("HORA_ENTRADA")), 2)
                    drT("c5") = Left(Nu(dr("HORA_SALIDA")), 2) + ":" + Right(Nu(dr("HORA_SALIDA")), 2)
                    drT("c11") = Nu(dr("REFRIGERIO"))
                    drT("c15") = Nu(dr("TOLERANCIA"))
                ElseIf Nu(dr("variable")) = "X" Then
                    Fecha = CDate(FormatoFecha(Nu(dr("ENTSAL_FECHA"))))
                    numeroD = Weekday(Fecha)
                    dt2 = obj.Lista_Asistencia_Variable(Session("CodEmpresa"), Session("CodGrupoEmpresa"), Nu(dr("ENTSAL_CODIGO")), numeroD)
                    If dt2.Rows.Count > 0 Then
                        For Each dr2 As DataRow In dt2.Rows
                            drT("c4") = Left(Nu(dr2("HV_HORA_ENTRADA")), 2) + ":" + Right(Nu(dr2("HV_HORA_ENTRADA")), 2)
                            drT("c5") = Left(Nu(dr2("HV_HORA_SALIDA")), 2) + ":" + Right(Nu(dr2("HV_HORA_SALIDA")), 2)
                            drT("c11") = Nu(dr2("HV_MINUTOS_REFRIGERIO"))
                            drT("c15") = Nu(dr2("HV_MINUTOS_TOLERANCIA"))
                        Next
                    End If
                    dt2 = Nothing
                End If
                If Nu(dr("ENTSAL_TIPO")) = "Normal" Then
                    If Nu(dr("ENTSAL_INGRESO_HORA")) <> "" Then drT("c6") = Left(Nu(dr("ENTSAL_INGRESO_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_INGRESO_HORA")), 2)
                    If Nu(dr("ENTSAL_SALIDA_HORA")) <> "" Then drT("c7") = Left(Nu(dr("ENTSAL_SALIDA_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_SALIDA_HORA")), 2)
                    drT("c13") = ""
                Else
                    If Nu(dr("ENTSAL_PERMISO_INGRESO_HORA")) <> "" Then drT("c7") = Left(Nu(dr("ENTSAL_PERMISO_INGRESO_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_PERMISO_INGRESO_HORA")), 2)
                    If Nu(dr("ENTSAL_PERMISO_SALIDA_HORA")) <> "" Then drT("c6") = Left(Nu(dr("ENTSAL_PERMISO_SALIDA_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_PERMISO_SALIDA_HORA")), 2)
                    If Nu(dr("ENTSAL_TIPO")) = "3" Then drT("c13") = Nu(dr("ENTSAL_NUMERO_SERVICIO"))
                End If
                dtListado.Rows.Add(drT)
            Next
        End If
        Return dtListado
    End Function
    Private Sub Listar_Asistencia()
        Dim CantReg As Integer = 0
        Flex.DataSource = Pasar_Asistemcia()
        Flex.DataBind()
        Call Calcula_Min_Entrada(0, Pasar_Asistemcia.Rows.Count - 1)
        Call Calcula_Horas_Trabajadas(0, Pasar_Asistemcia.Rows.Count, 0)
    End Sub
    Private Sub Calcula_Min_Entrada(ByVal I1 As Integer, ByVal I2 As Integer)
        Dim HEntTrab, HEntCol As String
        Dim MinDife, ii As Integer
        Dim dt As New DataTable
        If Flex.Rows.Count > 0 Then
            For ii = 0 To Flex.Rows.Count - 1
                If Flex.Rows(ii).Cells(12).Text = "Normal" Then
                    Flex.Rows(ii).Cells(10).Text = ""
                    HEntTrab = Flex.Rows(ii).Cells(4).Text
                    HEntCol = Flex.Rows(ii).Cells(6).Text
                    If HEntTrab <> "&nbsp;" And HEntCol <> "&nbsp;" Then
                        MinDife = ((Val(Left(HEntCol, 2)) * 60) + Val(Right(HEntCol, 2))) - ((Val(Left(HEntTrab, 2)) * 60) + Val(Right(HEntTrab, 2)))
                        If MinDife < 0 Then
                            Flex.Rows(ii).Cells(8).Text = ""
                        ElseIf MinDife = 0 Then
                            Flex.Rows(ii).Cells(8).Text = ""
                        Else
                            If Flex.Rows(ii).Cells(15).Text <> "&nbsp;" Then
                                If Val(Flex.Rows(ii).Cells(15).Text) >= MinDife Then
                                    Flex.Rows(ii).Cells(8).Text = ""
                                Else
                                    Flex.Rows(ii).Cells(8).Text = MinDife ' - (Val(.TextMatrix(ii, 15)))
                                End If
                            Else
                                Flex.Rows(ii).Cells(8).Text = MinDife
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    Private Sub Calcula_Horas_Trabajadas(ByVal I1 As Integer, ByVal I2 As Integer, ByVal ColPosMouse As Integer)
        Dim HIniTrab, HFinTrab As String
        Dim MinTotales, Min, Hor, ii As Integer
        If Flex.Rows.Count > 0 Then
            For ii = 0 To Flex.Rows.Count - 1
                If Flex.Rows(ii).Cells(12).Text = "Normal" Or Flex.Rows(ii).Cells(12).Text = "Permiso" Then
                    Flex.Rows(ii).Cells(9).Text = ""
                    Flex.Rows(ii).Cells(10).Text = ""
                    If Flex.Rows(ii).Cells(4).Text <> "&nbsp;" And Flex.Rows(ii).Cells(5).Text <> "&nbsp;" Then ' si hay un horario de trabajo
                        If Flex.Rows(ii).Cells(6).Text <> "&nbsp;" And Flex.Rows(ii).Cells(7).Text <> "&nbsp;" Then ' si hay un hora de entrada y salida
                            If Val(Left(Flex.Rows(ii).Cells(7).Text, 2) & Right(Flex.Rows(ii).Cells(7).Text, 2)) <= Val(Left(Flex.Rows(ii).Cells(5).Text, 2) & Right(Flex.Rows(ii).Cells(5).Text, 2)) Then 'si la salida de la empresa <= a la salida normal
                                HFinTrab = Flex.Rows(ii).Cells(7).Text 'salida de la empresa
                            Else
                                HFinTrab = Flex.Rows(ii).Cells(5).Text 'salida normal
                            End If
                            If Val(Left(Flex.Rows(ii).Cells(6).Text, 2) & Right(Flex.Rows(ii).Cells(6).Text, 2)) <= Val(Left(Flex.Rows(ii).Cells(4).Text, 2) & Right(Flex.Rows(ii).Cells(4).Text, 2)) Then 'si el ingreso a la empresa <= a la ingreso normal
                                HIniTrab = Flex.Rows(ii).Cells(4).Text 'ingreso normal
                            Else
                                HIniTrab = Flex.Rows(ii).Cells(6).Text 'ingreso a la empresa
                            End If
                            If HFinTrab <> "" And HIniTrab <> "" Then
                                MinTotales = (((Val(Left(HFinTrab, 2)) * 60) + Val(Right(HFinTrab, 2))) - ((Val(Left(HIniTrab, 2)) * 60) + Val(Right(HIniTrab, 2)))) - (Val(Flex.Rows(ii).Cells(11).Text) * 60)
                                Min = MinTotales Mod 60
                                Hor = MinTotales \ 60
                                Flex.Rows(ii).Cells(9).Text = MinTotales 'Hor & ":" & Min
                            End If
                        End If
                    End If
                End If
                'hrs_extras=hora_salida- horario_salida, hora_salida > horario_salida
                If Flex.Rows(ii).Cells(12).Text = "Permiso" Then
                    If Flex.Rows(ii).Cells(7).Text <> "&nbsp;" Then
                        If Val(Left(Flex.Rows(ii).Cells(7).Text, 2) & Right(Flex.Rows(ii).Cells(7).Text, 2)) > Val(Left(Flex.Rows(ii).Cells(5).Text, 2) & Right(Flex.Rows(ii).Cells(5).Text, 2)) Then
                            MinTotales = (((Val(Left(Flex.Rows(ii).Cells(7).Text, 2)) * 60) + Val(Right(Flex.Rows(ii).Cells(7).Text, 2))) - ((Val(Left(Flex.Rows(ii).Cells(5).Text, 2)) * 60) + Val(Right(Flex.Rows(ii).Cells(5).Text, 2))))
                            If MinTotales > 0 Then
                                Min = Format(MinTotales Mod 60, "00")
                                Hor = Format(MinTotales \ 60, "00")
                                Flex.Rows(ii).Cells(10).Text = MinTotales 'Hor & ":" & Min
                            End If
                        End If
                    End If
                ElseIf Flex.Rows(ii).Cells(12).Text = "Normal" Then
                    If Flex.Rows(ii).Cells(7).Text <> "&nbsp;" Then
                        If Val(Left(Flex.Rows(ii).Cells(7).Text, 2) & Right(Flex.Rows(ii).Cells(7).Text, 2)) > Val(Left(Flex.Rows(ii).Cells(5).Text, 2) & Right(Flex.Rows(ii).Cells(5).Text, 2)) Then
                            MinTotales = (((Val(Left(Flex.Rows(ii).Cells(7).Text, 2)) * 60) + Val(Right(Flex.Rows(ii).Cells(7).Text, 2))) - ((Val(Left(Flex.Rows(ii).Cells(5).Text, 2)) * 60) + Val(Right(Flex.Rows(ii).Cells(5).Text, 2))))
                            If MinTotales > 0 Then
                                Min = Format(MinTotales Mod 60, "00")
                                Hor = Format(MinTotales \ 60, "00")
                                Flex.Rows(ii).Cells(16).Text = MinTotales 'Hor & ":" & Min
                            End If
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtFechaIni.Text = FormatoFecha(FechaActual)
        End If
    End Sub
    Protected Sub FlexRecord_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexRecord.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim CodModulo As String : CodModulo = ""
        Dim Fecha As Date
        Dim numeroD As Integer
        FlexDetalle.DataSource = Nothing
        FlexDetalle.DataBind()
        Try
            If e.CommandName = "Detalle" Then
                Dim Rs As SqlDataReader
                Dim cn As New SqlConnection(Ruta_GrEmp)
                Dim cmdGlobal As New SqlCommand
                Dim cn2 As New SqlConnection(Ruta_GrEmp)
                Dim cmdGlobal2 As New SqlCommand
                Dim cn3 As New SqlConnection(Ruta_GrEmp)
                Dim cmdGlobal3 As New SqlCommand
                Dim psFechaIni As String = "20110101"
                Dim psFechaFin As String = "21001231"
                Dim obj As New clsControlPersonal
                Dim drT As DataRow
                Dim dtListado As New DataTable
                Dim dt As New DataTable
                Dim dt2 As New DataTable
                Dim dt3 As New DataTable
                cn.Open() : cmdGlobal.Connection = cn
                cn2.Open() : cmdGlobal2.Connection = cn2
                cn3.Open() : cmdGlobal2.Connection = cn3
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
                If txtFechaIni.Text.Trim <> "" And txtFechaFin.Text.Trim = "" Then
                    psFechaIni = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
                    psFechaFin = psFechaIni
                ElseIf txtFechaFin.Text.Trim <> "" And txtFechaIni.Text.Trim = "" Then
                    psFechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
                    psFechaIni = psFechaFin
                ElseIf txtFechaFin.Text.Trim <> "" And txtFechaIni.Text.Trim <> "" Then
                    psFechaIni = Right(txtFechaIni.Text.Trim, 4) + Mid(txtFechaIni.Text.Trim, 4, 2) + Left(txtFechaIni.Text.Trim, 2)
                    psFechaFin = Right(txtFechaFin.Text.Trim, 4) + Mid(txtFechaFin.Text.Trim, 4, 2) + Left(txtFechaFin.Text.Trim, 2)
                Else
                    psFechaIni = "20110101"
                    psFechaFin = "21001231"
                End If
                Dim i As Integer = 0
                drT = dtListado.NewRow()
                drT("c1") = "Resumen:"
                drT("c2") = FlexRecord.Rows(Index).Cells(2).Text
                drT("c3") = FlexRecord.Rows(Index).Cells(3).Text
                drT("c4") = "Dias Trab.: "
                drT("c5") = FlexRecord.Rows(Index).Cells(5).Text
                drT("c6") = "Dias Tarde: "
                drT("c7") = FlexRecord.Rows(Index).Cells(6).Text
                drT("c9") = FlexRecord.Rows(Index).Cells(8).Text
                drT("c10") = FlexRecord.Rows(Index).Cells(7).Text
                drT("c11") = FlexRecord.Rows(Index).Cells(10).Text
                drT("c12") = FlexRecord.Rows(Index).Cells(9).Text
                dtListado.Rows.Add(drT)
                drT = dtListado.NewRow()
                dtListado.Rows.Add(drT)
                dt = obj.Lista_Asistencia(Session("CodEmpresa"), Session("CodGrupoEmpresa"), psFechaIni, psFechaFin, FlexRecord.Rows(Index).Cells(2).Text)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        i = i + 1
                        drT = dtListado.NewRow()
                        drT("c0") = i
                        drT("c1") = FormatoFecha(Nu(dr("ENTSAL_FECHA")))
                        drT("c2") = Nu(dr("Tipo")) 'ENTSAL_TIPO
                        drT("c13") = ""
                        drT("c14") = Nu(dr("ENTSAL_TIPO"))
                        If Nu(dr("FIJO")) = "X" Then
                            drT("c3") = Left(Nu(dr("HORA_ENTRADA")), 2) + ":" + Right(Nu(dr("HORA_ENTRADA")), 2)
                            drT("c4") = Left(Nu(dr("HORA_SALIDA")), 2) + ":" + Right(Nu(dr("HORA_SALIDA")), 2)
                            drT("c5") = Nu(dr("REFRIGERIO"))
                            drT("c6") = Nu(dr("TOLERANCIA"))
                        ElseIf Nu(dr("variable")) = "X" Then
                            Fecha = CDate(FormatoFecha(Nu(dr("ENTSAL_FECHA"))))
                            numeroD = Weekday(Fecha)
                            cmdGlobal.CommandText = "SELECT HV_HORA_ENTRADA, HV_HORA_SALIDA,HV_MINUTOS_TOLERANCIA,HV_MINUTOS_REFRIGERIO,HV_MINUTOS_REFRIGERIO FROM TBINTEGRAN_ASISTENCIA_VARIABLE " _
                                  & " WHERE (HV_PERSONAL = '" & Nu(dr("ENTSAL_CODIGO")) & "') AND (HV_NRO_DIA = '" & numeroD & "') AND (HV_SYS_EST = '0') AND GRPOEMPRESA_CODIGO=" & Session("CodGrupoEmpresa") & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
                            Rs = cmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    drT("c3") = Left(Nu(Rs("HV_HORA_ENTRADA")), 2) + ":" + Right(Nu(Rs("HV_HORA_ENTRADA")), 2)
                                    drT("c4") = Left(Nu(Rs("HV_HORA_SALIDA")), 2) + ":" + Right(Nu(Rs("HV_HORA_SALIDA")), 2)
                                    drT("c5") = Nu(Rs("HV_MINUTOS_REFRIGERIO"))
                                    drT("c6") = Nu(Rs("HV_MINUTOS_TOLERANCIA"))
                                End While
                            End If
                            Rs.Close()
                        End If
                        If Nu(dr("ENTSAL_TIPO")) = "Normal" Then
                            If Nu(dr("ENTSAL_INGRESO_HORA")) <> "" Then drT("c7") = Left(Nu(dr("ENTSAL_INGRESO_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_INGRESO_HORA")), 2)
                            If Nu(dr("ENTSAL_SALIDA_HORA")) <> "" Then drT("c8") = Left(Nu(dr("ENTSAL_SALIDA_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_SALIDA_HORA")), 2)
                        Else
                            If Nu(dr("ENTSAL_PERMISO_INGRESO_HORA")) <> "" Then drT("c7") = Left(Nu(dr("ENTSAL_PERMISO_INGRESO_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_PERMISO_INGRESO_HORA")), 2)
                            If Nu(dr("ENTSAL_PERMISO_SALIDA_HORA")) <> "" Then drT("c8") = Left(Nu(dr("ENTSAL_PERMISO_SALIDA_HORA")), 2) + ":" + Right(Nu(dr("ENTSAL_PERMISO_SALIDA_HORA")), 2)
                            If Nu(dr("ENTSAL_TIPO")) = "3" Then drT("c13") = Nu(dr("ENTSAL_NUMERO_SERVICIO"))
                            If Nu(dr("ENTSAL_TIPO")) = "4" Then drT("c15") = Nu(dr("ENTSAL_PERMISO_MOTIVO"))
                        End If
                        drT("c16") = Nu(dr("ENTSAL_LATITUD"))
                        drT("c17") = Nu(dr("ENTSAL_LONGITUD"))
                        dtListado.Rows.Add(drT)
                    Next
                End If
                dt = Nothing
                FlexDetalle.DataSource = Nothing
                FlexDetalle.DataBind()
                FlexDetalle.DataSource = dtListado
                FlexDetalle.DataBind()
                If FlexDetalle.Rows.Count > 0 Then
                    For i = 0 To FlexDetalle.Rows.Count - 1
                        If i = 0 Then
                            FlexDetalle.Rows(i).ForeColor = Drawing.Color.RoyalBlue
                            'FlexDetalle.Rows(i).Font.Bold = True
                            Exit Sub
                        End If
                    Next
                End If
                If FlexDetalle.Rows.Count = 0 Then lblDetalle.Text = "No hay detalle"
                Call Calcula_Min_Entrada2(0, FlexDetalle.Rows.Count - 1)
                Call Calcula_Horas_Trabajadas2(0, FlexDetalle.Rows.Count - 1)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub Calcula_Min_Entrada2(ByVal I1 As Integer, ByVal I2 As Integer)
        Dim HEntTrab, HEntCol As String
        Dim MinDife, ii As Integer
        For ii = I1 To I2
            If FlexDetalle.Rows(ii).Cells(14).Text = "Normal" Then
                HEntTrab = FlexDetalle.Rows(ii).Cells(3).Text
                HEntCol = FlexDetalle.Rows(ii).Cells(7).Text
                If HEntTrab <> "" And HEntCol <> "" Then
                    MinDife = ((Val(Left(HEntCol, 2)) * 60) + Val(Right(HEntCol, 2))) - ((Val(Left(HEntTrab, 2)) * 60) + Val(Right(HEntTrab, 2)))
                    If MinDife < 0 Then
                        FlexDetalle.Rows(ii).Cells(9).Text = ""
                    ElseIf MinDife = 0 Then
                        FlexDetalle.Rows(ii).Cells(9).Text = ""
                    Else
                        If FlexDetalle.Rows(ii).Cells(6).Text <> "" Then
                            If Val(FlexDetalle.Rows(ii).Cells(6).Text) >= MinDife Then
                                FlexDetalle.Rows(ii).Cells(9).Text = ""
                            Else
                                FlexDetalle.Rows(ii).Cells(9).Text = MinDife ' - (Val(.TextMatrix(ii, 15)))
                            End If
                        Else
                            FlexDetalle.Rows(ii).Cells(9).Text = MinDife
                        End If
                    End If
                End If
            End If
        Next
    End Sub
    Private Sub Calcula_Horas_Trabajadas2(ByVal I1 As Integer, ByVal I2 As Integer)
        Dim HIniTrab, HFinTrab As String
        Dim MinTotales, Min, Hor, ii As Integer
        For ii = I1 To I2
            If FlexDetalle.Rows(ii).Cells(14).Text = "Normal" Then
                FlexDetalle.Rows(ii).Cells(10).Text = ""
                FlexDetalle.Rows(ii).Cells(11).Text = ""
                FlexDetalle.Rows(ii).Cells(12).Text = ""
                If FlexDetalle.Rows(ii).Cells(3).Text <> "" And FlexDetalle.Rows(ii).Cells(4).Text <> "" Then ' si hay un horario de trabajo
                    If FlexDetalle.Rows(ii).Cells(7).Text <> "" And FlexDetalle.Rows(ii).Cells(8).Text <> "" Then ' si hay un hora de entrada y salida
                        If Val(Left(FlexDetalle.Rows(ii).Cells(8).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(8).Text, 2)) <= Val(Left(FlexDetalle.Rows(ii).Cells(4).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(4).Text, 2)) Then 'si la salida de la empresa <= a la salida normal
                            HFinTrab = FlexDetalle.Rows(ii).Cells(8).Text 'salida de la empresa
                        Else
                            HFinTrab = FlexDetalle.Rows(ii).Cells(4).Text 'salida normal
                        End If
                        If Val(Left(FlexDetalle.Rows(ii).Cells(7).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(7).Text, 2)) <= Val(Left(FlexDetalle.Rows(ii).Cells(3).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(3).Text, 2)) Then 'si el ingreso a la empresa <= a la ingreso normal
                            HIniTrab = FlexDetalle.Rows(ii).Cells(3).Text 'ingreso normal
                        Else
                            HIniTrab = FlexDetalle.Rows(ii).Cells(7).Text 'ingreso a la empresa
                        End If
                        If HFinTrab <> "" And HIniTrab <> "" Then
                            MinTotales = (((Val(Left(HFinTrab, 2)) * 60) + Val(Right(HFinTrab, 2))) - ((Val(Left(HIniTrab, 2)) * 60) + Val(Right(HIniTrab, 2)))) - (Val(FlexDetalle.Rows(ii).Cells(5).Text) * 60)
                            If MinTotales > 0 Then
                                Min = MinTotales Mod 60
                                Hor = MinTotales \ 60
                                FlexDetalle.Rows(ii).Cells(10).Text = Llenar_Ceros(Hor, 2) & ":" & Llenar_Ceros(Min, 2)
                            End If
                        End If
                    End If
                End If
            End If
            'hrs_extras=hora_salida- horario_salida, hora_salida > horario_salida
            If FlexDetalle.Rows(ii).Cells(14).Text = "Permiso" Then
                If FlexDetalle.Rows(ii).Cells(7).Text <> "" Then
                    If Val(Left(FlexDetalle.Rows(ii).Cells(7).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(7).Text, 2)) > Val(Left(FlexDetalle.Rows(ii).Cells(8).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(8).Text, 2)) Then
                        MinTotales = (((Val(Left(FlexDetalle.Rows(ii).Cells(7).Text, 2)) * 60) + Val(Right(FlexDetalle.Rows(ii).Cells(7).Text, 2))) - ((Val(Left(FlexDetalle.Rows(ii).Cells(4).Text, 2)) * 60) + Val(Right(FlexDetalle.Rows(ii).Cells(4).Text, 2))))
                        If MinTotales > 0 Then
                            Min = MinTotales Mod 60
                            Hor = MinTotales \ 60
                            FlexDetalle.Rows(ii).Cells(12).Text = Llenar_Ceros(Hor, 2) & ":" & Llenar_Ceros(Min, 2)
                        End If
                    End If
                End If
            ElseIf FlexDetalle.Rows(ii).Cells(14).Text = "Normal" Then
                If FlexDetalle.Rows(ii).Cells(8).Text <> "" Then
                    If Val(Left(FlexDetalle.Rows(ii).Cells(8).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(8).Text, 2)) > Val(Left(FlexDetalle.Rows(ii).Cells(4).Text, 2) & Right(FlexDetalle.Rows(ii).Cells(4).Text, 2)) Then
                        MinTotales = (((Val(Left(FlexDetalle.Rows(ii).Cells(8).Text, 2)) * 60) + Val(Right(FlexDetalle.Rows(ii).Cells(8).Text, 2))) - ((Val(Left(FlexDetalle.Rows(ii).Cells(4).Text, 2)) * 60) + Val(Right(FlexDetalle.Rows(ii).Cells(4).Text, 2))))
                        Min = MinTotales Mod 60
                        Hor = MinTotales \ 60
                        FlexDetalle.Rows(ii).Cells(11).Text = Llenar_Ceros(Hor, 2) & ":" & Llenar_Ceros(Min, 2)
                    End If
                End If
            End If
        Next
    End Sub
End Class
