Imports System.Data
Imports System.Data.SqlClient
Namespace WebGestor
    Public Module Funciones
        Public Function Llenar_Ceros(ByVal psCadena As String, ByVal pdDigitos As Integer) As String
            Llenar_Ceros = ""
            Dim i As Integer
            Dim psLongC As Integer = Len(psCadena)
            For i = psLongC + 1 To pdDigitos
                Llenar_Ceros = Llenar_Ceros + "0"
            Next
            Llenar_Ceros = Llenar_Ceros + psCadena
        End Function
        Public Function Comprension_Dias(ByVal CadenaDia As String) As String
            Select Case CadenaDia
                Case Is = "Lun, Mar, Mie, Jue, Vie, Sab, Dom" : Comprension_Dias = "Lun a Dom"
                Case Is = "Lun, Mar, Mie, Jue, Vie, Sab" : Comprension_Dias = "Lun a Sab"
                Case Is = "Lun, Mar, Mie, Jue, Vie" : Comprension_Dias = "Lun a Vie"
                Case Is = "Lun, Mar, Mie, Jue" : Comprension_Dias = "Lun a Jue"
                Case Is = "Lun, Mar, Mie" : Comprension_Dias = "Lun a Mie"
                Case Is = "Lun, Mar" : Comprension_Dias = "Lun y Mar"
                Case Is = "Lun, Mie" : Comprension_Dias = "Lun y Mie"
                Case Is = "Lun, Jue" : Comprension_Dias = "Lun y Jue"
                Case Is = "Lun, Vie" : Comprension_Dias = "Lun y Vie"
                Case Is = "Lun, Sab" : Comprension_Dias = "Lun y Sab"
                Case Is = "Mar, Mie, Jue, Vie, Sab" : Comprension_Dias = "Mar a Sab"
                Case Is = "Mar, Mie, Jue, Vie" : Comprension_Dias = "Mar a Vie"
                Case Is = "Mar, Mie, Jue" : Comprension_Dias = "Mar a Jue"
                Case Is = "Mar, Mie" : Comprension_Dias = "Mar y Mie"
                Case Is = "Mar, Jue" : Comprension_Dias = "Mar y Jue"
                Case Is = "Mar, Vie" : Comprension_Dias = "Mar y Vie"
                Case Is = "Mar, Sab" : Comprension_Dias = "Mar y Sab"
                Case Is = "Mie, Jue, Vie, Sab" : Comprension_Dias = "Mie a Sab"
                Case Is = "Mie, Jue, Vie" : Comprension_Dias = "Mie a Vie"
                Case Is = "Mie, Jue" : Comprension_Dias = "Mie y Jue"
                Case Is = "Mie, Vie" : Comprension_Dias = "Mie y Vie"
                Case Is = "Mie, Sab" : Comprension_Dias = "Mie y Sab"
                Case Is = "Jue, Vie, Sab" : Comprension_Dias = "Jue a Sab"
                Case Is = "Jue, Vie" : Comprension_Dias = "Jue y Vie"
                Case Is = "Jue, Sab" : Comprension_Dias = "Jue y Sab"
                Case Is = "Vie, Sab" : Comprension_Dias = "Jue a Sab"

                Case Is = "Lun, Mie, Vie" : Comprension_Dias = "Lun, Mie y Vie"
                Case Is = "Mar, Jue, Sab" : Comprension_Dias = "Mar, Jue y Sab"
                Case Else
                    Comprension_Dias = CadenaDia
            End Select
        End Function
        Public Function Convertir_Hora(ByVal NumMin As Integer) As String
            Dim Hx As String
            Hx = Llenar_Ceros(Trim(Str((NumMin \ 60))), 2) + Llenar_Ceros(Trim(Str((NumMin Mod 60))), 2)
            Convertir_Hora = Llenar_Ceros(Hx, 4)
            If Left(Convertir_Hora, 2) = "24" Then Convertir_Hora = "00" + Right(Convertir_Hora, 2)
        End Function
        Function AñoActual(ByVal CodEmpresa As String, ByVal psConexion As String) As String
            Dim obj As New clsCont_Listados
            Dim dt As DataTable
            Dim Año As String = ""
            dt = obj.Cont_AñoActual(CodEmpresa, psConexion)
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    Año = Nu(drMenuItem("AÑO"))
                Next
            End If
            dt = Nothing
            AñoActual = Año
        End Function
        Public Function Formato_Digito(ByVal psValor As String, ByVal pdCantDigitos As Long) As String
            Formato_Digito = ""
            Dim i As Long = 0
            Dim pdCantLong As Long = 0
            pdCantLong = Len(psValor)
            For i = pdCantLong To pdCantDigitos - 1
                Formato_Digito = Formato_Digito & "0"
            Next
            Formato_Digito = Formato_Digito & psValor
        End Function
        Function Genera_Codigo_NoPersonal(ByVal Guardar As String) As String
            Genera_Codigo_NoPersonal = ""
            Dim obj As New ModuloSeguridad
            Dim dt As New Data.DataTable
            Dim NumCod As Integer
            dt = obj.Consigue_UltimoNUsuario
            If dt.Rows.Count = 1 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    obj.Ingresar_UltimoNUsuario(Format(Nz(drMenuItem("USUARIO")) + 1, "0000"))
                Next
            End If
            dt = Nothing
            dt = obj.Extraer_UltimoNUsuario
            If dt.Rows.Count = 0 Then
                If Guardar = "S" Then
                    obj.Ingresar_UltimoNUsuario("0002")
                End If
                Genera_Codigo_NoPersonal = "11110001"
            Else
                For Each drMenuItem As Data.DataRow In dt.Rows
                    If Guardar = "S" Then
                        NumCod = Nz(drMenuItem("CONUDS_CORR"))
                        drMenuItem("CONUDS_CORR") = Cadena_Num_Corr(NumCod)
                        drMenuItem("CONUDS_CORR") = Val(drMenuItem("CONUDS_CORR")) + 1
                        Genera_Codigo_NoPersonal = "1111" & Format(NumCod, "0000")
                    Else
                        NumCod = Nz(drMenuItem("CONUDS_CORR"))
                        Genera_Codigo_NoPersonal = "1111" & Format(NumCod, "0000")
                    End If
                Next
            End If
        End Function
        Function Genera_CodUni_Personal(ByVal Guardar As String) As String
            Genera_CodUni_Personal = ""
            Dim obj As New ModuloSeguridad
            Dim dt As New Data.DataTable
            Dim Año As String
            Dim FechaServer As String : FechaServer = FechaActual()
            Año = Left(FechaServer, 4)
            Dim NumCorr As Integer
            dt = obj.Consigue_UltimoPUsuario(Año)
            If dt.Rows.Count = 1 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    If IsDBNull(drMenuItem("USUARIO")) = False Then
                        obj.Upd_Ultimo_PUsuario(Format(drMenuItem("USUARIO") + 1, "0000"), Año)
                    End If
                Next
            End If
            dt = Nothing
            dt = obj.Extrae_UltimoPUsuario(Año)
            If dt.Rows.Count = 0 Then
                If Guardar = "S" Then
                    obj.Ins_Ultimo_PUsuario(Año)
                End If
                Genera_CodUni_Personal = Trim(Año) + Trim("0001")
            Else
                For Each drMenuItem As Data.DataRow In dt.Rows
                    If Guardar = "S" Then
                        NumCorr = Format(Nz(drMenuItem("CONPSA_CORR")) + 1, "0000")
                        obj.Upd_Ultimo_PUsuario2(Format(NumCorr, "0000"), Año)
                    End If
                    Genera_CodUni_Personal = Trim(Año) + Trim(drMenuItem("CONPSA_CORR"))
                Next
            End If
        End Function
        Function ArmaFiltros(ByVal Texto As String, ByVal Campo As String, ByVal Operador As String) As String
            Dim Ncomas As Integer
            Dim variable As String
            Dim posicion As Integer
            Dim Palabra As String
            ArmaFiltros = ""
            Dim i As Integer
            If Len(Texto) > 0 Then
                Ncomas = 0
                For i = 1 To Len(Texto)
                    If Mid(Texto, i, 1) = "," Then Ncomas = Ncomas + 1
                Next
                variable = UCase(Tildes(Texto))
                For i = 1 To Ncomas + 1
                    posicion = InStr(variable, ",")
                    If posicion = 0 Then Palabra = variable Else Palabra = Left(variable, posicion - 1)
                    If posicion = 0 Then variable = "" Else variable = Mid(variable, posicion + 1)
                    ArmaFiltros = ArmaFiltros & Campo & "'%" & Palabra & "%'"
                    If variable <> "" Then ArmaFiltros = ArmaFiltros & Operador
                Next
            End If
            ArmaFiltros = ArmaFiltros
        End Function
        Function Tildes(ByVal Texto As String) As String
            Dim i As Integer, str As String
            Tildes = ""
            If Texto = "" Then Tildes = "" : Exit Function
            Texto = LCase(Texto)
            For i = 1 To Len(Texto)
                str = Mid(Texto, i, 1)
                If str = "a" Or str = "á" Then
                    str = "[aá]"
                ElseIf str = "e" Or str = "é" Then
                    str = "[eé]"
                ElseIf str = "i" Or str = "í" Then
                    str = "[ií]"
                ElseIf str = "o" Or str = "ó" Then
                    str = "[oó]"
                ElseIf str = "u" Or str = "ú" Then
                    str = "[uú]"
                End If
                Tildes = Tildes & str
            Next i
        End Function
        Public Function Existe_Tabla(ByVal NomTable As String, ByVal Ruta As String) As Boolean
            Dim CnExiste As New SqlClient.SqlConnection(Ruta)
            Dim RsExiste As SqlClient.SqlDataReader
            Dim CmdExiste As New SqlClient.SqlCommand
            'On Error GoTo Tables
            Existe_Tabla = False
            CnExiste.Open()
            CmdExiste.CommandText = "select * from sysobjects where id = object_id(N'[dbo].[" & NomTable & "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1"
            CmdExiste.Connection = CnExiste
            RsExiste = CmdExiste.ExecuteReader
            If RsExiste.HasRows Then
                While RsExiste.Read
                    Existe_Tabla = True
                End While
            End If
            RsExiste.Close()
            CnExiste.Close()
            Exit Function
            'Tables:
        End Function
        Public Function FormatoFecha(ByVal CodFecha As String, Optional ByVal DigAño2 As Boolean = False) As String
            Dim Dia As String, Mes As String, Año As String
            Dim FechaIns As String
            If CodFecha = "" Then
                FormatoFecha = ""
            Else
                Dia = Mid(CodFecha, 7, 2)
                Mes = Mid(CodFecha, 5, 2)
                If DigAño2 = True Then Año = Mid(CodFecha, 3, 2) Else Año = Mid(CodFecha, 1, 4)
                FechaIns = Dia + "/" + Mes + "/" + Año
                FormatoFecha = FechaIns 'Format(FechaIns, "dd/mm/yyyy")
            End If
        End Function
        Public Function FormatoHora(ByVal CadHora As String) As String
            If CadHora = "" Then Return "" : Exit Function
            FormatoHora = Left(CadHora, 2) & ":" & Right(CadHora, 2)
        End Function
        Public Function FormatoHoraSeg(ByVal CadHora As String) As String
            If CadHora = "" Then Return "" : Exit Function
            FormatoHoraSeg = Left(CadHora, 2) & ":" & Mid(CadHora, 3, 2) & ":" & Right(CadHora, 2)
        End Function
        Public Sub Parametros_Variables_Page(ByVal nCodBtn As String, ByRef vHab As String, ByRef vSeg As String)
            Dim myConnection As New SqlClient.SqlConnection(Ruta_Ng)
            Dim myCommand As New SqlClient.SqlCommand("SELECT * FROM TBDESCRIP_BOTON WHERE BOTON_SYS_EST='0' AND BOTON_CODIGO='" & nCodBtn & "'", myConnection)
            Dim myReader As SqlClient.SqlDataReader
            vHab = "F" : vSeg = ""
            On Error GoTo Proceso
            myConnection.Open()
            myReader = myCommand.ExecuteReader()
            While myReader.Read()
                If Nu(myReader!BOTON_HABILITA) = "0" Then vHab = "V"
                If Nu(myReader!BOTON_VG_SEGURIDAD) <> "" Then vSeg = Trim(Nu(myReader!BOTON_VG_SEGURIDAD))
            End While
            myReader.Close()
            myConnection.Close()
            Exit Sub
Proceso:
            If myConnection.State = ConnectionState.Open Then myConnection.Close()
        End Sub
        Public Function Nu(ByVal Valor As Object) As String
            If IsDBNull(Valor) Then
                Nu = ""
            Else
                Nu = Trim(Valor)
            End If
        End Function
        Public Function Nz(ByVal VarValue As Object) As Double
            If IsDBNull(VarValue) Then
                Nz = 0
            ElseIf CStr(VarValue) = "" Then
                Nz = 0
            Else
                Nz = CDbl(VarValue)
            End If
        End Function
        Public Function Solo_Texto(ByVal Cadena As String) As String
            Dim Cad As String
            Dim Xr As Integer
            Cad = Cadena
            For Xr = 1 To Len(Cad)
                If Asc(Mid(Cadena, Xr, 1)) = 13 Or Asc(Mid(Cadena, Xr, 1)) = 10 Then
                    Cadena = Mid(Cadena, 1, Xr - 1) & " " & Mid(Cadena, Xr + 1)
                End If
            Next
            Solo_Texto = Trim(Cadena)
        End Function
        Public Function FechaActual(Optional ByVal AddYear As Integer = 0) As String
            Dim myConnection_FA As New SqlClient.SqlConnection(Ruta_Ng)
            Dim myCommand_FA As New SqlClient.SqlCommand("SELECT GETDATE()", myConnection_FA)
            Dim myReader_Fa As SqlClient.SqlDataReader
            Dim Fecha As String = ""
            On Error GoTo Proceso
            myConnection_FA.Open()
            myReader_Fa = myCommand_FA.ExecuteReader()
            While myReader_Fa.Read()
                Fecha = Format(Year(myReader_Fa.GetDateTime(0)), "0000") + AddYear & Format(Month(myReader_Fa.GetDateTime(0)), "00") & Format(Day(myReader_Fa.GetDateTime(0)), "00")
            End While
            myReader_Fa.Close()
            myConnection_FA.Close()
            Return Fecha
            Exit Function
Proceso:
            If myConnection_FA.State = ConnectionState.Open Then myConnection_FA.Close()
        End Function
        Public Function HoraActual(Optional ByVal Completo As Boolean = False) As String
            Dim myConnection_FA As New SqlClient.SqlConnection(Ruta_Ng)
            Dim myCommand_FA As New SqlClient.SqlCommand("SELECT GETDATE()", myConnection_FA)
            Dim myReader_Fa As SqlClient.SqlDataReader
            Dim Hora As String = ""
            On Error GoTo Proceso
            myConnection_FA.Open()
            myReader_Fa = myCommand_FA.ExecuteReader()
            While myReader_Fa.Read()
                If Completo = False Then
                    Hora = Format(Hour(myReader_Fa.GetDateTime(0)), "00") + Format(Minute(myReader_Fa.GetDateTime(0)), "00")
                Else
                    Hora = Format(Hour(myReader_Fa.GetDateTime(0)), "00") + Format(Minute(myReader_Fa.GetDateTime(0)), "00") + Format(Second(myReader_Fa.GetDateTime(0)), "00")
                End If
            End While
            myReader_Fa.Close()
            myConnection_FA.Close()
            Return Hora
            Exit Function
Proceso:
            If myConnection_FA.State = ConnectionState.Open Then myConnection_FA.Close()
        End Function
        Public Function Nombre_Mes(ByVal NMes As String, ByVal Largo As Boolean) As String
            Dim NameMes As String = ""
            Select Case NMes
                Case "1", "01" : NameMes = IIf(Largo = True, "Enero", "Ene")
                Case "2", "02" : NameMes = IIf(Largo = True, "Febrero", "Feb")
                Case "3", "03" : NameMes = IIf(Largo = True, "Marzo", "Mar")
                Case "4", "04" : NameMes = IIf(Largo = True, "Abril", "Abr")
                Case "5", "05" : NameMes = IIf(Largo = True, "Mayo", "May")
                Case "6", "06" : NameMes = IIf(Largo = True, "Junio", "Jun")
                Case "7", "07" : NameMes = IIf(Largo = True, "Julio", "Jul")
                Case "8", "08" : NameMes = IIf(Largo = True, "Agosto", "Ago")
                Case "9", "09" : NameMes = IIf(Largo = True, "Septiembe", "Set")
                Case "10", "10" : NameMes = IIf(Largo = True, "Octubre", "Oct")
                Case "11", "11" : NameMes = IIf(Largo = True, "Noviembre", "Nov")
                Case "12", "12" : NameMes = IIf(Largo = True, "Diciembre", "Dic")
            End Select
            Return NameMes
        End Function
        Public Function Nombre_Dia(ByVal NDia As String, ByVal largo As Boolean) As String
            Dim NameDia As String = ""
            Select Case NDia
                Case "2", "02" : NameDia = IIf(largo = True, "Lunes", "Lun")
                Case "3", "03" : NameDia = IIf(largo = True, "Martes", "Mar")
                Case "4", "04" : NameDia = IIf(largo = True, "Miércoles", "Mie")
                Case "5", "05" : NameDia = IIf(largo = True, "Jueves", "Jue")
                Case "6", "06" : NameDia = IIf(largo = True, "Viernes", "Vie")
                Case "7", "07" : NameDia = IIf(largo = True, "Sábado", "Sab")
                Case "1", "01" : NameDia = IIf(largo = True, "Domingo", "Dom")
            End Select
            Return NameDia
        End Function
        Public Function Existe_Pagina(ByVal nPage As String) As Boolean
            Existe_Pagina = False
            Select Case nPage
                Case Is = "Cas_BusquedaBaseDatos.aspx" : Existe_Pagina = True
                Case Is = "Cas_Definicion_CarteraConsulta.aspx" : Existe_Pagina = True
                Case Is = "Cas_Registra_Incidentes.aspx" : Existe_Pagina = True
                Case Is = "Cas_RelacionIncidentes.aspx" : Existe_Pagina = True
                Case Is = "Cas_Relacion_Reportes.aspx" : Existe_Pagina = True
                Case Is = "Cas_Define_personas.aspx" : Existe_Pagina = True
                Case Is = "Cas_Define_Grupo.aspx" : Existe_Pagina = True
                Case Is = "Cas_ListaIncidentes_Nivel2.aspx" : Existe_Pagina = True
                Case Is = "Cas_ListaIncidentes_GrupoNivel2.aspx" : Existe_Pagina = True
                    'Case Is = "SegSistema_Mant_Usuarios.aspx" : Existe_Pagina = True
            End Select
        End Function
        Public Sub LlenaMes(ByVal Cbo As DropDownList, ByVal Largo As Boolean)
            Dim m As Integer
            Dim NM As String = ""
            For m = 1 To 12
                Select Case m
                    Case 1 : NM = IIf(Largo = True, "Enero", "Ene")
                    Case 2 : NM = IIf(Largo = True, "Febrero", "Feb")
                    Case 3 : NM = IIf(Largo = True, "Marzo", "Mar")
                    Case 4 : NM = IIf(Largo = True, "Abril", "Abr")
                    Case 5 : NM = IIf(Largo = True, "Mayo", "May")
                    Case 6 : NM = IIf(Largo = True, "Junio", "Jun")
                    Case 7 : NM = IIf(Largo = True, "Julio", "Jul")
                    Case 8 : NM = IIf(Largo = True, "Agosto", "Ago")
                    Case 9 : NM = IIf(Largo = True, "Setiembre", "Set")
                    Case 10 : NM = IIf(Largo = True, "Octubre", "Oct")
                    Case 11 : NM = IIf(Largo = True, "Noviembre", "Nov")
                    Case 12 : NM = IIf(Largo = True, "Diciembre", "Dic")
                End Select
                Dim Item As New ListItem
                Item.Text = NM
                Item.Value = Format(m, "00")
                Cbo.Items.Add(Item)
            Next
        End Sub
        Public Sub LlenaHora(ByVal Cbo As DropDownList)
            Dim h As Integer
            For h = 1 To 23
                Dim Item As New ListItem
                Item.Text = Format(h, "00")
                Item.Value = Format(h, "00")
                Cbo.Items.Add(Item)
            Next
        End Sub
        Public Sub LlenaMinuto(ByVal Cbo As DropDownList)
            Dim m As Integer
            For m = 1 To 59
                Dim Item As New ListItem
                Item.Text = Format(m, "00")
                Item.Value = Format(m, "00")
                Cbo.Items.Add(Item)
            Next
        End Sub
        Public Sub LlenaDia(ByVal Cbo As DropDownList)
            Dim d As Integer
            For d = 1 To 31
                Dim Item As New ListItem
                Item.Text = Format(d, "00")
                Item.Value = Format(d, "00")
                Cbo.Items.Add(Item)
            Next
        End Sub
        Public Sub LlenaAno(ByVal Cbo As DropDownList)
            Dim a As Integer
            For a = 2020 To CInt(Left(FechaActual, 4))
                Dim Item As New ListItem
                Item.Text = a.ToString
                Item.Value = a.ToString
                Cbo.Items.Add(Item)
            Next
        End Sub
        Public Sub LlenaCheckBoxItem(ByVal nTabla As String, ByVal Cbo As CheckBoxList, Optional ByVal psConexion As String = "")
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim Cn2 As New SqlConnection(psConexion)
            Cbo.Items.Clear()
            Try
                If psConexion = "" Then Cn.Open()
                If psConexion <> "" Then Cn2.Open()
                Dim Sql As String = ""
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                Sql = "SELECT DISTINCT ELEMEN_VALOR,ELEMEN_CODIGO FROM TBCELEMEN WHERE ELEMEN_TABLA='" & nTabla & "' ORDER BY ELEMEN_VALOR"
                If psConexion = "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn)
                If psConexion <> "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn2)
                Cbo.DataSource = cmdSql.ExecuteReader
                Cbo.DataTextField = "ELEMEN_VALOR"
                Cbo.DataValueField = "ELEMEN_CODIGO"
                Cbo.DataBind()
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                If psConexion = "" Then Cn.Close()
                If psConexion <> "" Then Cn2.Close()
            End Try
        End Sub
        Public Sub LlenaComboItemBox(ByVal nTabla As String, ByVal Cbo As CheckBoxList, Optional ByVal psConexion As String = "", Optional ByVal psSeleccionar As String = "")
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim Cn2 As New SqlConnection(psConexion)
            Cbo.Items.Clear()
            Try
                If psConexion = "" Then Cn.Open()
                If psConexion <> "" Then Cn2.Open()
                Dim Sql As String = ""
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                Sql = "SELECT DISTINCT ELEMEN_VALOR,ELEMEN_CODIGO FROM TBCELEMEN WHERE ELEMEN_TABLA='" & nTabla & "' ORDER BY ELEMEN_VALOR"
                If psConexion = "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn)
                If psConexion <> "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn2)
                Cbo.DataSource = cmdSql.ExecuteReader
                Cbo.DataTextField = "ELEMEN_VALOR"
                Cbo.DataValueField = "ELEMEN_CODIGO"
                Cbo.DataBind()
                If psSeleccionar <> "" Then
                    Cbo.Items.Add("< " & psSeleccionar & " >") : Cbo.SelectedValue = "< " & psSeleccionar & " >"
                Else
                    'Cbo.Items.Add("< Seleccionar >") : Cbo.SelectedValue = "< Seleccionar >"
                End If
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                If psConexion = "" Then Cn.Close()
                If psConexion <> "" Then Cn2.Close()
            End Try
        End Sub
        Public Sub LlenaComboItem(ByVal nTabla As String, ByVal Cbo As DropDownList, Optional ByVal psConexion As String = "", Optional ByVal psSeleccionar As String = "")
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim Cn2 As New SqlConnection(psConexion)
            Cbo.Items.Clear()
            Try
                If psConexion = "" Then Cn.Open()
                If psConexion <> "" Then Cn2.Open()
                Dim Sql As String = ""
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                Sql = "SELECT DISTINCT ELEMEN_VALOR,ELEMEN_CODIGO FROM TBCELEMEN WHERE ELEMEN_TABLA='" & nTabla & "' ORDER BY ELEMEN_VALOR"
                If psConexion = "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn)
                If psConexion <> "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn2)
                Cbo.DataSource = cmdSql.ExecuteReader
                Cbo.DataTextField = "ELEMEN_VALOR"
                Cbo.DataValueField = "ELEMEN_CODIGO"
                Cbo.DataBind()
                If psSeleccionar <> "" Then
                    Cbo.Items.Add("< " & psSeleccionar & " >") : Cbo.SelectedValue = "< " & psSeleccionar & " >"
                Else
                    Cbo.Items.Add("< Seleccionar >") : Cbo.SelectedValue = "< Seleccionar >"
                End If
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                If psConexion = "" Then Cn.Close()
                If psConexion <> "" Then Cn2.Close()
            End Try
        End Sub
        Public Sub LlenaComboItem3(ByVal nTabla As String, ByVal Cbo As DropDownList, Optional ByVal psConexion As String = "")
            Dim Cn As New SqlConnection(Ruta_GrEmp)
            Dim Cn2 As New SqlConnection(psConexion)
            Cbo.Items.Clear()
            Try
                If psConexion = "" Then Cn.Open()
                If psConexion <> "" Then Cn2.Open()
                Dim Sql As String = ""
                Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
                Sql = "SELECT DISTINCT ELEMEN_VALOR,ELEMEN_CODIGO,ELEMEN_VALOR+' - '+ELEMEN_CODIGO AS DESCRIPCION FROM TBCELEMEN WHERE ELEMEN_TABLA='" & nTabla & "' ORDER BY ELEMEN_VALOR"
                If psConexion = "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn)
                If psConexion <> "" Then cmdSql = New SqlClient.SqlCommand(Sql, Cn2)
                Cbo.DataSource = cmdSql.ExecuteReader
                Cbo.DataTextField = "DESCRIPCION"
                Cbo.DataValueField = "ELEMEN_CODIGO"
                Cbo.DataBind()
                Cbo.Items.Add("< Seleccionar >") : Cbo.SelectedValue = "< Seleccionar >"
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                If psConexion = "" Then Cn.Close()
                If psConexion <> "" Then Cn2.Close()
            End Try
        End Sub
        Function Cadena_Num_Corr(ByVal NumeroCorr As Integer) As String
            Dim CadenaNum As String : CadenaNum = ""
            Select Case NumeroCorr
                Case 1 To 9
                    CadenaNum = Trim("000") + Trim(Str(NumeroCorr))
                Case 10 To 99
                    CadenaNum = Trim("00") + Trim(Str(NumeroCorr))
                Case 100 To 999
                    CadenaNum = Trim("0") + Trim(Str(NumeroCorr))
                Case 1000 To 9999
                    CadenaNum = Trim(Str(NumeroCorr))
            End Select
            Cadena_Num_Corr = CadenaNum
        End Function
        Public Function QuitaComilla(ByVal psDato As String) As String
            Dim Datos As String : Datos = psDato
            Dim lsCadena As String : lsCadena = ""
            Dim icontador As Integer
            For icontador = 1 To Len(Trim(Datos))
                If Mid(Datos, icontador, 1) = "'" Then
                    lsCadena = lsCadena + " "
                Else
                    lsCadena = lsCadena + Mid(Datos, icontador, 1)
                End If
            Next
            QuitaComilla = lsCadena
        End Function
        Public Function QuitaPunto(ByVal psDato As String) As String
            Dim Datos As String : Datos = psDato
            Dim lsCadena As String : lsCadena = ""
            Dim icontador As Integer
            For icontador = 1 To Len(Trim(Datos))
                If Mid(Datos, icontador, 1) = "." Then
                    lsCadena = lsCadena + ""
                Else
                    lsCadena = lsCadena + Mid(Datos, icontador, 1)
                End If
            Next
            QuitaPunto = lsCadena
        End Function
        Public Sub LlenaComboItem2(ByVal Campo As String, ByVal Combo As DropDownList, ByVal CampoFiltrar As String, ByVal CualEs As String)
            Dim cierra As Boolean
            Dim CnTE As New SqlConnection(Ruta_GrEmp)
            Dim RsTE As SqlClient.SqlDataReader
            Dim CmdTE As New SqlClient.SqlCommand
            Try
                CnTE.Open()
                CmdTE.Connection = CnTE

                Combo.Items.Clear()
                Dim Item1 As New ListItem
                Item1.Text = "< Seleccionar >"
                Item1.Value = "0"
                Combo.Items.Add(Item1)
                cierra = False
                If CualEs = "PR" Then
                    If Len(CampoFiltrar) <> 2 Then Exit Sub
                End If
                If CualEs = "DS" Then
                    If Len(CampoFiltrar) <> 4 Then Exit Sub
                End If
                If CualEs = "PSUB" Then
                    If Len(CampoFiltrar) <> 3 Then Exit Sub
                End If
                If CualEs = "OSUB" Then
                    If Len(CampoFiltrar) <> 4 Then Exit Sub
                End If
                CmdTE.CommandText = "SELECT ELEMEN_VALOR,ELEMEN_CODIGO FROM TBCELEMEN WHERE ELEMEN_TABLA='" & Campo & "'"
                If CampoFiltrar <> "" Then CmdTE.CommandText = CmdTE.CommandText & " AND ELEMEN_CODIGO LIKE '" & CampoFiltrar & "%'"
                RsTE = CmdTE.ExecuteReader
                If RsTE.HasRows Then
                    While RsTE.Read
                        Dim Item As New ListItem
                        Item.Text = Nu(RsTE(0))
                        Item.Value = Nu(RsTE(1))
                        Combo.Items.Add(Item)
                    End While
                End If
                RsTE.Close()
                CnTE.Close()
            Catch Ex As SqlClient.SqlException
            Catch Ex As Exception
            Finally
                CnTE.Close()
            End Try
        End Sub
    End Module
End Namespace
