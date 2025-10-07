Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class ControlPersonal_Person_Control_Asistencia
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim Longitud As String = Request.Form("txtGeoInfoLon")
            Session("Longitud") = Longitud
            Dim Latitud As String = Request.Form("txtGeoInfoLat")
            Response.Write("<script>document.getElementById('txtGeoInfoLat').value = '" & Latitud & "';</script>")
            Session("Latitud") = Latitud
            txtFSistema.Text = FormatoFecha(FechaActual())
            txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
            'imgUsuario.ImageUrl = "~/Fotos/persona.jpg" ' Imagen por defecto si no hay ID
            txtCodigo.Text = Session("User")
            txtPassword.Text = "********"
        End If
    End Sub
    Private Function SistemaUsuario(ByVal pCodUsuario As String, ByVal pPassword As String) As String
        SistemaUsuario = ""
        Dim Cn As New SqlConnection(Ruta_Ng)
        Dim Cn2 As New SqlConnection(Ruta_GrEmp)
        Dim CmdGlobal_GpEmp As New SqlCommand
        Dim CmdGlobal_GpEmp2 As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Dim Rs2 As SqlClient.SqlDataReader
        Dim ApePat As String, ApeMat As String, Nombres As String
        ApePat = "" : ApeMat = "" : Nombres = ""
        LblError.Visible = True
        LblError.Text = ""
        Dim TipoGrupo As String = ""
        txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
        Try
            Cn.Open()
            CmdGlobal_GpEmp.Connection = Cn
            Cn2.Open()
            CmdGlobal_GpEmp2.Connection = Cn2
            CmdGlobal_GpEmp.CommandText = "SELECT * FROM TBUSUARI WHERE USUARI_CODIGO='" & pCodUsuario & "' AND USUARI_SYS_EST='0'"
            CmdGlobal_GpEmp.Connection = Cn
            Rs = CmdGlobal_GpEmp.ExecuteReader()
            If Rs.HasRows = False Then
                LblError.Text = "Es la primera vez que el usuario ingresa al sistema, no puede registrarse"
                SistemaUsuario = "2"
            Else
                While Rs.Read()
                    If Rs!USUARI_PASS = pPassword Then
                        'If (CLng(FechaActual()) - CLng(Rs!USUARI_FECPASS)) < 60 Then
                        If (CLng(FechaActual()) >= CLng(Rs!USUARI_FECINI)) And
                           (CLng(FechaActual()) <= CLng(Rs!USUARI_FECFIN)) Then
                            If VERIF_ACCESO_DIAHORA(Rs!USUARI_DIAHORACC) = True Then
                                If Rs!USUARI_PERCED = "N" Then
                                    ApePat = Rs!USUARI_APEPAT
                                    ApeMat = Rs!USUARI_APEMAT
                                    Nombres = Rs!USUARI_NOMBRES
                                Else
                                    'Call IniGrupoEmpresa()
                                    CmdGlobal_GpEmp2.CommandText = "SELECT PERSON_APEPAT,PERSON_APEMAT,PERSON_NOMBRES FROM TBPERSONAL " _
                                                       & "WHERE PERSON_CODIGO='" & pCodUsuario & "' AND PERSON_SYS_EST='0'"
                                    CmdGlobal_GpEmp2.Connection = Cn2
                                    Rs2 = CmdGlobal_GpEmp2.ExecuteReader()
                                    If Rs2.HasRows = True Then
                                        While Rs2.Read()
                                            ApePat = Rs2!PERSON_APEPAT
                                            ApeMat = Rs2!PERSON_APEMAT
                                            Nombres = Rs2!PERSON_NOMBRES
                                        End While
                                    End If
                                    Rs2.Close()
                                End If
                                txtNombApe.Text = ApePat & " " & ApeMat & " " & Nombres
                            Else
                                LblError.Text = ("El Usuario indicado, no puede accesar al sistema el día de hoy o en la hora actual," + Chr(13) + "consultar al Dpto. de Seguridad.")
                                SistemaUsuario = "3"
                            End If
                        Else
                            LblError.Text = ("El Usuario indicado, no puede accesar al sistema el día de hoy," + Chr(13) + "consultar al Dpto. de Seguridad.")
                            SistemaUsuario = "3"
                        End If
                    Else
                        SistemaUsuario = "2"
                        LblError.Text = "Password o Clave de Acceso equivocada, por favor corregir"
                    End If
                    'Else
                    '    lblError.Text = ("El Password ha expirado. Es necesario cambiar el password para poder luego ingresar el sistema.")
                    '    SistemaUsuario = "2"
                    'End If
                End While
            End If
            Rs.Close()
            Cn.Close()
        Catch Ex As SqlException
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
            Cn.Close()
        End Try
    End Function
    Private Function VERIF_ACCESO_DIAHORA(ByVal DiaHoraAcc) As Boolean
        Dim aa, Mm, Dd As String, Fech As String, Dia, hora
        VERIF_ACCESO_DIAHORA = False
        Fech = FechaActual()
        aa = Left(Fech, 4)
        Mm = Mid(Fech, 5, 2)
        Dd = Right(Fech, 2)
        Fech = Dd + "/" + Mm + "/" + aa
        Dia = Weekday(Fech, vbMonday)
        hora = CInt(Left(FechaActual, 2))
        If Mid(DiaHoraAcc, ((Dia - 1) * 24 + hora + 1), 1) = "X" Then
            VERIF_ACCESO_DIAHORA = True
        End If
    End Function
    Protected Sub btnVerificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnVerificar.Click
        If txtCodigo.Text.Trim = "" Then txtCodigo.Focus() : Exit Sub
        Dim obj As New ClsPersonal
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim Resp As String = ""
        Dim NoSelect As String : NoSelect = "SI"
        Dim i As Integer : i = 0
        Dim a As Integer : a = 0
        LblError.Visible = True
        LblError.Text = ""
        lstPermiso.Items.Clear()
        Dim psCodigoPersonal As String = ""
        Dim objSeg As New ModuloSeguridad
        Try
            txtHSistema.Text = FormatoHoraSeg(HoraActual(True))

            dt = objSeg.Busca_UsuarioSistema(txtCodigo.Text.Trim)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    psCodigoPersonal = dr("usuari_codigo")
                Next
            End If
            dt = Nothing

            If psCodigoPersonal = "" Then psCodigoPersonal = txtCodigo.Text.Trim


            dt = obj.Verificar_Personal(psCodigoPersonal, "3")
            If dt.Rows.Count = 0 Then
                LblError.Text = ("Codigo de Personal No existe, por favor corregir")
                txtCodigo.Focus()
            Else
                Resp = SistemaUsuario(psCodigoPersonal, txtPassword.Text)
                If Resp = "1" Then
                    txtCodigo.Focus()
                ElseIf Resp = "2" Then
                    txtPassword.Focus()
                End If
                If Not Resp = "" Then Exit Sub
                txtPassword.Focus()
                dt2 = obj.Lista_HoraEntSal(psCodigoPersonal.Trim)
                If dt2.Rows.Count > 0 Then
                    Session("Insertar") = "No"
                    FlexHora.DataSource = dt2
                    FlexHora.DataBind()
                    lstPermiso.Items.Add("Normal") : lstPermiso.SelectedValue = ("Normal")
                    FlexPermiso.DataSource = Nothing
                    FlexPermiso.DataBind()
                    dt3 = obj.Lista_PermisoHoraEntSal(psCodigoPersonal.Trim)
                    If dt3.Rows.Count > 0 Then
                        FlexPermiso.DataSource = dt3
                        FlexPermiso.DataBind()
                    End If
                    dt3 = Nothing
                    Call MostrarControl()
                    Session("Nuevo_Permiso") = "NO"
                    If FlexPermiso.Rows.Count > 0 Then Call Mostrar_Permiso()
                    lstPermiso_SelectedIndexChanged(sender, e)
                    If FlexHora.Rows.Count > 0 Then
                        If FlexHora.Rows(0).Cells(3).BackColor = Drawing.Color.White Then
                            If FlexHora.Rows(0).Cells(3).Text <> "" And FlexHora.Rows(0).Cells(3).Text <> "&nbsp;" Then a = a + 1
                        End If
                    End If
                    If FlexPermiso.Rows.Count > 0 Then
                        For i = 0 To FlexPermiso.Rows.Count - 1
                            If FlexPermiso.Rows(i).Cells(3).BackColor = Drawing.Color.White Then
                                If FlexPermiso.Rows(i).Cells(3).Text <> "" And FlexPermiso.Rows(i).Cells(3).Text <> "&nbsp;" Then a = a + 1
                            End If
                        Next
                    End If
                    If FlexHora.Rows.Count + FlexPermiso.Rows.Count = a Then NoSelect = "NO"
                    If NoSelect = "NO" Then
                        lstPermiso.Enabled = False
                        btnGrabar.Enabled = False
                        btnNuevoPermiso.Enabled = False
                    Else
                        lstPermiso.Enabled = True
                        btnGrabar.Enabled = True
                        btnNuevoPermiso.Enabled = True
                    End If
                Else
                    lstPermiso.Enabled = True
                    btnGrabar.Enabled = True
                    btnNuevoPermiso.Enabled = True
                    Session("Insertar") = "Si"
                    Call MostrarControl()
                End If
                dt2 = Nothing
            End If
            dt = Nothing

            Dim usuarioID As String = txtCodigo.Text.Trim()

            'If Not String.IsNullOrEmpty(usuarioID) Then
            '    Dim query As String = "SELECT PERSON_IMAGEN as imagen FROM TBPERSONAL WHERE PERSON_CODIGO  = @PERSON_CODIGO"
            '    Using connection As New SqlConnection(Ruta_GrEmp)
            '        Using cmd As New SqlCommand(query, connection)
            '            cmd.Parameters.Add("@PERSON_CODIGO", SqlDbType.VarChar).Value = usuarioID ' Ajusta el valor del ID según el registro que desees mostrar
            '            connection.Open()

            '            Using reader As SqlDataReader = cmd.ExecuteReader()
            '                If reader.Read() Then
            '                    If Not IsDBNull(reader("Imagen")) Then
            '                        Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
            '                        Dim base64String As String = Convert.ToBase64String(imageData)
            '                        imgUsuario.ImageUrl = "data:image/jpeg;base64," + base64String
            '                        imgUsuario.Visible = True
            '                        Session("NuevaImagen") = "No"
            '                    End If
            '                End If
            '            End Using
            '        End Using
            '    End Using
            '    'imgUsuario.ImageUrl = "~/ControlPersonal/PersonFoto.ashx?id=" & usuarioID
            'Else
            '    imgUsuario.ImageUrl = "~/Fotos/persona.jpg" ' Imagen por defecto si no hay ID
            'End If


        Catch Ex As SqlException
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            LblError.Visible = True
            LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub MostrarControl()
        'Dim obj As New Listados
        Dim dt As New DataTable
        Dim dRow As Data.DataRow
        Dim i As Integer : i = 0
        txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
        dt.Columns.Add("ENTSAL_TIPO")
        dt.Columns.Add("INGRESO_HORA")
        dt.Columns.Add("SALIDA_HORA")
        dt.Columns.Add("ENTSAL_CONTAR_TIPO")
        dt.Columns.Add("S")
        If FlexHora.Rows.Count > 0 Then
            For i = 0 To FlexHora.Rows.Count - 1
                dRow = dt.NewRow
                dRow("ENTSAL_TIPO") = FlexHora.Rows(i).Cells(0).Text
                dRow("INGRESO_HORA") = FlexHora.Rows(i).Cells(2).Text
                dRow("SALIDA_HORA") = IIf(FlexHora.Rows(i).Cells(3).Text = ":", "", FlexHora.Rows(i).Cells(3).Text.Replace("&nbsp;", ""))
                dRow("ENTSAL_CONTAR_TIPO") = FlexHora.Rows(i).Cells(4).Text
                dRow("S") = FlexHora.Rows(i).Cells(5).Text
                dt.Rows.Add(dRow)
            Next
        End If
        If FlexHora.Rows.Count = 0 Then
            dRow = dt.NewRow
            dRow("ENTSAL_TIPO") = "Normal"
            dRow("INGRESO_HORA") = Left(txtHSistema.Text.Trim, 5)
            dRow("SALIDA_HORA") = ""
            dRow("ENTSAL_CONTAR_TIPO") = "1"
            dRow("S") = "N"
            lstPermiso.Items.Add("Normal") : lstPermiso.SelectedValue = ("Normal")
            dt.Rows.Add(dRow)
        End If
        FlexHora.DataSource = dt
        FlexHora.DataBind()
        If FlexHora.Rows.Count > 0 Then FlexHora.Rows(FlexHora.Rows.Count - 1).Cells(3).BackColor = Drawing.Color.White
        If FlexHora.Rows.Count > 0 And FlexHora.Rows(0).Cells(3).Text = "" Then FlexHora.Rows(0).Cells(2).BackColor = Drawing.Color.SeaGreen
    End Sub
    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Dim FechaAct As String : FechaAct = ""
        Dim nIngHSalida As String : nIngHSalida = "NO"
        Dim HoraSistemaI As String : HoraSistemaI = ""
        Dim HoraSistemaS As String : HoraSistemaS = ""
        Dim HoraSistemaPI As String : HoraSistemaPI = ""
        Dim HoraSistemaPS As String : HoraSistemaPS = ""
        Dim obj As New ClsPersonal
        Dim i As Integer : i = 0
        LblError.Text = ""
        txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
        FechaAct = Right(txtFSistema.Text.Trim, 4) + Mid(txtFSistema.Text.Trim, 4, 2) + Left(txtFSistema.Text.Trim, 2)
        If lstPermiso.SelectedValue = "Normal" Then
            If FlexHora.Rows.Count > 0 Then
                If FlexHora.Rows(FlexHora.Rows.Count - 1).Cells(5).Text = "S" And FlexHora.Rows(FlexHora.Rows.Count - 1).Cells(3).Text <> "&nbsp;" And FlexHora.Rows(FlexHora.Rows.Count - 1).Cells(3).Text <> "" Then nIngHSalida = "SI"
            End If
            HoraSistemaI = IIf(FlexHora.Rows(0).Cells(2).Text = "&nbsp;", "", Left(FlexHora.Rows(0).Cells(2).Text, 2) + Right(FlexHora.Rows(0).Cells(2).Text, 2))
            HoraSistemaS = IIf(FlexHora.Rows(0).Cells(3).Text = "&nbsp;", "", Left(FlexHora.Rows(0).Cells(3).Text, 2) + Right(FlexHora.Rows(0).Cells(3).Text, 2))
            If Session("Insertar") = "Si" Then
                obj.Insertar_HoraIngreso(Session("CodGrupoEmpresa"), Session("CodEmpresa"), txtCodigo.Text.Trim, FechaAct, HoraSistemaI)
            ElseIf Session("Insertar") = "No" And nIngHSalida = "SI" Then
                If HoraSistemaS <> "" And HoraSistemaS <> "&nbsp;" Then obj.Ingresar_HoraSalida(Session("CodGrupoEmpresa"), Session("CodEmpresa"), txtCodigo.Text.Trim, FechaAct, HoraSistemaS)
            ElseIf nIngHSalida = "NO" Then
                LblError.Text = "Primero debe ingresar la Hora de Ingreso del Permiso " & FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(1).Text
                Exit Sub
            End If
        ElseIf Left(lstPermiso.SelectedValue, 7) = "Permiso" Then
            For i = 0 To FlexPermiso.Rows.Count - 1
                If FlexPermiso.Rows(i).Cells(5).Text = "N" Then
                    HoraSistemaPS = IIf(FlexPermiso.Rows(i).Cells(2).Text = "&nbsp;", "", Left(FlexPermiso.Rows(i).Cells(2).Text, 2) + Right(FlexPermiso.Rows(i).Cells(2).Text, 2))
                    obj.Ingresar_PermisoHoraSalida(Session("CodGrupoEmpresa"), Session("CodEmpresa"), txtCodigo.Text.Trim, FechaAct, HoraSistemaPS, FlexPermiso.Rows(i).Cells(4).Text)
                ElseIf FlexPermiso.Rows(i).Cells(5).Text = "S" Then
                    HoraSistemaPI = IIf(FlexPermiso.Rows(i).Cells(3).Text = "&nbsp;", "", Left(FlexPermiso.Rows(i).Cells(3).Text, 2) + Right(FlexPermiso.Rows(i).Cells(3).Text, 2))
                    If HoraSistemaPI <> "" And HoraSistemaPI <> "&nbsp;" Then obj.Ingresar_PermisoHoraIngreso(Session("CodGrupoEmpresa"), Session("CodEmpresa"), txtCodigo.Text.Trim, FechaAct, HoraSistemaPI, FlexPermiso.Rows(i).Cells(4).Text)
                End If
            Next
        End If
        Dim pdLatitud As Double = Nz(hfLatitud.Value)
        Dim pdlongitud As Double = Nz(hfLongitud.Value)
        'pdlongitud = Session("Longitud")
        'pdLatitud = Session("Latitud")
        'Dim latitud As String = hfLatitud.Value
        'Dim longitud As String = hfLongitud.Value
        Dim Cn As New SqlClient.SqlConnection(Ruta_GrEmp)
        If pdLatitud <> 0 And pdlongitud <> 0 Then
            Dim CmdGlobal As New SqlCommand
            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " UPDATE TBREG_ENTSAL SET ENTSAL_LATITUD =" & pdLatitud & "  , ENTSAL_LONGITUD=" & pdlongitud & " WHERE ENTSAL_FECHA ='" & FechaAct & "' AND ENTSAL_CODIGO ='" & txtCodigo.Text.Trim & "'"
            CmdGlobal.ExecuteNonQuery()
        End If


        Dim imagenBase64 As String = hfImagen.Value
        If imagenBase64 <> "" Then
            If String.IsNullOrEmpty(imagenBase64) Then
                Response.Write("<script>alert('No se capturó ninguna imagen.');</script>")
                Return
            End If

            ' Convertir la imagen base64 a bytes
            Dim imagenBytes As Byte() = Convert.FromBase64String(imagenBase64.Replace("data:image/png;base64,", ""))

            ' Guardar la imagen en la base de datos

            Dim cmdSql As New SqlCommand
            obj._Asistencia_GuardarImagen(txtCodigo.Text.Trim, FechaAct, imagenBytes)


        End If

        Response.Redirect("Person_Control_Asistencia.aspx")
    End Sub
    Protected Sub btnNuevoPermiso_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoPermiso.Click
        Dim nPermiso As String : nPermiso = ""
        LblError.Text = ""
        txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
        If FlexPermiso.Rows.Count > 0 Then
            If FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(3).BackColor = Drawing.Color.White And FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(5).Text = "S" And FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(3).Text <> "&nbsp;" And FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(3).Text <> "" Then nPermiso = "SI"
        Else
            nPermiso = "SI"
        End If
        If nPermiso = "SI" Then
            Session("Nuevo_Permiso") = "SI"
            Call Mostrar_Permiso()
            If FlexPermiso.Rows.Count > 0 And FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(5).Text = "N" And FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(3).Text = "&nbsp;" Then
                FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(2).BackColor = Drawing.Color.SeaGreen
                FlexHora.Rows(0).Cells(3).BackColor = Drawing.Color.White
            ElseIf FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(5).Text = "S" And FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(2).Text <> "" Then
                FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(3).BackColor = Drawing.Color.SeaGreen
                FlexHora.Rows(0).Cells(3).BackColor = Drawing.Color.White
            End If
        Else
            LblError.Text = "Primero tiene que ingresar la Hora de Ingreso del Permiso " & FlexPermiso.Rows(FlexPermiso.Rows.Count - 1).Cells(1).Text
        End If
    End Sub
    Private Sub Mostrar_Permiso()
        Dim dt As New DataTable
        Dim dRow As Data.DataRow
        Dim i As Integer : i = 0
        Dim NPer As Integer : NPer = 0
        Dim NTipo As Integer : NTipo = 1
        lstPermiso.Items.Clear()
        lstPermiso.Items.Add("Normal")
        dt.Columns.Add("ENTSAL_TIPO")
        dt.Columns.Add("PERMISO")
        dt.Columns.Add("PERMISO_SALIDA_HORA")
        dt.Columns.Add("PERMISO_INGRESO_HORA")
        dt.Columns.Add("ENTSAL_CONTAR_TIPO")
        dt.Columns.Add("S")
        txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
        If FlexPermiso.Rows.Count > 0 Then
            For i = 0 To FlexPermiso.Rows.Count - 1
                If FlexPermiso.Rows(i).Cells(0).Text = "Permiso" And FlexPermiso.Rows(i).Cells(5).Text = "S" Then
                    NPer = NPer + 1
                    dRow = dt.NewRow
                    dRow("ENTSAL_TIPO") = FlexPermiso.Rows(i).Cells(0).Text
                    dRow("PERMISO") = "Nro " & NPer
                    dRow("PERMISO_SALIDA_HORA") = FlexPermiso.Rows(i).Cells(2).Text
                    dRow("PERMISO_INGRESO_HORA") = IIf(FlexPermiso.Rows(i).Cells(3).Text = ":", "", FlexPermiso.Rows(i).Cells(3).Text.Replace("&nbsp;", ""))
                    dRow("ENTSAL_CONTAR_TIPO") = FlexPermiso.Rows(i).Cells(4).Text
                    NTipo = CDbl(FlexPermiso.Rows(i).Cells(4).Text)
                    dRow("S") = FlexPermiso.Rows(i).Cells(5).Text
                    lstPermiso.Items.Add("Permiso Nro " & NPer)
                    dt.Rows.Add(dRow)
                End If
            Next
        End If
        If Session("Nuevo_Permiso") = "SI" Then
            NPer = NPer + 1
            NTipo = NTipo + 1
            dRow = dt.NewRow
            dRow("ENTSAL_TIPO") = "Permiso"
            dRow("PERMISO") = "Nro " & NPer
            dRow("PERMISO_SALIDA_HORA") = Left(txtHSistema.Text.Trim, 5)
            dRow("PERMISO_INGRESO_HORA") = ""
            dRow("ENTSAL_CONTAR_TIPO") = NTipo
            dRow("S") = "N"
            lstPermiso.Items.Add("Permiso Nro " & NPer) : lstPermiso.SelectedValue = "Permiso Nro " & NPer
            dt.Rows.Add(dRow)
            FlexHora.Rows(0).Cells(3).Text = ""
        End If
        FlexPermiso.DataSource = dt
        FlexPermiso.DataBind()
        If FlexPermiso.Rows.Count > 0 Then
            For i = 0 To FlexPermiso.Rows.Count - 1
                FlexPermiso.Rows(i).Cells(3).BackColor = Drawing.Color.White
            Next
        End If
    End Sub
    Protected Sub lstPermiso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPermiso.SelectedIndexChanged
        Dim NPer As Integer : NPer = 0
        Dim nPerDel As Integer : nPerDel = 0
        Dim i As Integer : i = 0
        Dim dt As New DataTable
        Dim obj As New ClsPersonal
        Dim Seleccion As String : Seleccion = ""
        Dim Validar As String : Validar = ""
        LblError.Text = ""
        txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
        Seleccion = IIf(lstPermiso.SelectedValue = "", "Normal", lstPermiso.SelectedValue)
        If Seleccion = "Normal" Then Validar = Seleccion Else Validar = Left(Seleccion, 7)
        lstPermiso.Items.Clear() : lstPermiso.Items.Add("Normal")
        dt = obj.Lista_PermisoHoraEntSal(txtCodigo.Text.Trim)
        If dt.Rows.Count > 0 Then
            FlexPermiso.DataSource = dt
            FlexPermiso.DataBind()
        Else
            FlexPermiso.DataSource = Nothing
            FlexPermiso.DataBind()
        End If
        dt = Nothing
        Session("Nuevo_Permiso") = "NO"
        If FlexPermiso.Rows.Count > 0 Then Call Mostrar_Permiso()
        If Validar = "Normal" Then
            If FlexHora.Rows(0).Cells(3).Text = "&nbsp;" Or FlexHora.Rows(0).Cells(3).Text = "" Then
                FlexHora.Rows(0).Cells(3).Text = Left(txtHSistema.Text.Trim, 5)
                FlexHora.Rows(0).Cells(3).BackColor = Drawing.Color.SeaGreen
            End If
        ElseIf Validar = "Permiso" Then
            NPer = Mid(Seleccion, 13)
            For i = 0 To FlexPermiso.Rows.Count - 1
                If FlexPermiso.Rows(i).Cells(1).Text = "Nro " & NPer Then
                    If FlexPermiso.Rows(i).Cells(5).Text = "S" And FlexPermiso.Rows(i).Cells(2).Text <> "&nbsp;" And FlexPermiso.Rows(i).Cells(3).Text = "&nbsp;" Then
                        FlexPermiso.Rows(i).Cells(3).Text = Left(txtHSistema.Text.Trim, 5)
                        FlexPermiso.Rows(i).Cells(3).BackColor = Drawing.Color.SeaGreen
                        FlexHora.Rows(0).Cells(3).Text = ""
                        FlexHora.Rows(0).Cells(3).BackColor = Drawing.Color.White
                    End If
                    If FlexPermiso.Rows(i).Cells(2).Text = "&nbsp;" Or FlexPermiso.Rows(i).Cells(2).Text = "" Then
                        FlexPermiso.Rows(i).Cells(2).Text = Left(txtHSistema.Text.Trim, 5)
                        FlexPermiso.Rows(i).Cells(2).BackColor = Drawing.Color.SeaGreen
                    End If
                End If
            Next
        End If
        If Seleccion <> "" Then lstPermiso.SelectedValue = Seleccion
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Response.Redirect("Person_Control_Asistencia.aspx")
    End Sub

End Class
