Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Eventos_Eventos_xParticipantes
    Inherits System.Web.UI.Page

    Dim obj As New Cls_Eventos
    Dim objSeg As New ModuloSeguridad
    Dim objPer As New ClsPersonal
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            TxtPersonalCodigo.Text = Session("User")
            TxtPersonalNombres.Text = Mid(Session("UserNombre"), 14)
            Dim Longitud As String = Request.Form("txtGeoInfoLon")
            Session("Longitud") = Longitud
            Dim Latitud As String = Request.Form("txtGeoInfoLat")
            Session("Latitud") = Latitud
            hfLatitud.Value = Session("Latitud")
            hfLongitud.Value = Session("Longitud")
            txtFSistema.Text = FormatoFecha(FechaActual())
            txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
            'imgUsuario.ImageUrl = "~/Fotos/persona.jpg"
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        '   
        Try
            GvEventos.DataSource = obj.Eventos_xParticipantes(Session("Ruta_Emp"), Session("CodEmpresa"), 0, Session("User"))
            GvEventos.DataBind()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub GvEventos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvEventos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable
        Dim pdCodEvento As Integer = 0
        TxtRegistroFechas.Text = ""
        Try
            If e.CommandName = "Detalle" Then
                pdCodEvento = Nz(GvEventos.Rows(Index).Cells(13).Text.Trim)
                dt = obj.EventosDatos_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodEvento)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        TxtEvCodigo.Text = Llenar_Ceros(Nu(dr("EVENTO_CODIGO")), 4)
                        TxtEvTipo.Text = Nu(dr("TIPO_EVENTO"))
                        TxtEvNombre.Text = Nu(dr("EVENTO_NOMBRE"))
                        TxtEvObjetivo.Text = Nu(dr("EVENTO_OBJETIVO"))
                        TxtEvDescripcion.Text = Nu(dr("EVENTO_DESCRIPCION"))
                        TxtEvResponsable.Text = Nu(dr("RESPONSABLE"))
                        TxtEvContacto.Text = Nu(dr("EVENTO_CONTACTO"))
                        TxtEvContactoTelef.Text = Nu(dr("EVENTO_CONTACTO_TELEFONO"))
                        TxtFechaIni.Text = Nu(dr("FECHA_INICIA_EVENTO"))
                        TxtFechaFin.Text = Nu(dr("FECHA_TERMINA_EVENTO"))
                        TxtHoraIni.Text = Nu(dr("HORA_INICIA_EVENTO"))
                        TxtHoraFin.Text = Nu(dr("HORA_TERMINA_EVENTO"))
                        TxtFirmaHoraEnt.Text = Nu(dr("HORA_INGRESO_REAL"))
                        TxtFirmaHoraSal.Text = Nu(dr("HORA_SALIDA_REAL"))
                        TxtRegistroFechas.Text = Nu(dr("EVEPART_REGISTRO"))
                    Next
                End If
                TxtLatitud.Text = hfLatitud.Value
                TxtLongitud.Text = hfLongitud.Value
                TxtFirmaFecha.Text = FormatoFecha(FechaActual)
                If TxtFirmaHoraEnt.Text = "" Then
                    TxtFirmaHoraEnt.Text = FormatoHoraSeg(HoraActual(True))
                    TxtFirmaHoraSal.Text = ""
                ElseIf TxtFirmaHoraEnt.Text <> "" And TxtFirmaHoraSal.Text = "" Then
                    TxtFirmaHoraSal.Text = FormatoHoraSeg(HoraActual(True))
                End If
                DivEvento.Visible = True
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        DivEvento.Visible = False
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        txtHSistema.Text = FormatoHoraSeg(HoraActual(True))
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click

        If Session("Entra") = "" Then
            DivEvento.Visible = True

            Dim imagenBase64 As String = hfImagen.Value

            If String.IsNullOrEmpty(imagenBase64) Then
                Response.Write("<script>alert('No se capturó ninguna imagen.');</script>")
                Return
            End If
            ' Convertir Base64 a Bytes
            Dim imagenBytes As Byte() = Convert.FromBase64String(imagenBase64.Replace("data:image/png;base64,", ""))

            ' Guardar en la base de datos
            Dim nuevaId As Integer
            Dim psHoraReal As String = ""
            Dim psHoraSalida As String = ""
            Dim FechaAct As String = ""
            Dim HoraSistemaI As String = ""
            Dim HoraSistemaS As String = ""
            FechaAct = Right(txtFSistema.Text.Trim, 4) + Mid(txtFSistema.Text.Trim, 4, 2) + Left(txtFSistema.Text.Trim, 2)
            HoraSistemaI = Left(TxtFirmaHoraEnt.Text, 2) & Mid(TxtFirmaHoraEnt.Text, 4, 2)
            HoraSistemaS = Left(TxtFirmaHoraSal.Text, 2) & Mid(TxtFirmaHoraSal.Text, 4, 2)
            Dim pdLatitud As Double = Nz(hfLatitud.Value)
            Dim pdlongitud As Double = Nz(hfLongitud.Value)
            psHoraReal = Left(TxtFirmaHoraEnt.Text, 2) & Mid(TxtFirmaHoraEnt.Text, 4, 2) & Right(TxtFirmaHoraEnt.Text, 2)
            psHoraSalida = Left(TxtFirmaHoraSal.Text, 2) & Mid(TxtFirmaHoraSal.Text, 4, 2) & Right(TxtFirmaHoraSal.Text, 2)
            Using con As New SqlConnection(Session("Ruta_Emp"))
                Dim Sql As String = ""
                If psHoraReal <> "" And psHoraSalida = "" Then
                    If pdLatitud <> 0 And pdlongitud <> 0 Then
                        Sql = " UPDATE TBEVENTOS_PARTICIPANTES SET EVEPART_HORA_ENTRADA_REAL = '" & psHoraReal & "', " _
                            & " EVEPART_ENTRADA_LATITUD = " & pdLatitud & ", EVEPART_ENTRADA_LONGITUD =" & pdlongitud & ", EVEPART_FOTO_ENTRADA = @Foto " _
                            & " WHERE EVEPART_REGISTRO  = " & Nz(TxtRegistroFechas.Text) & " "
                    Else
                        Sql = " UPDATE TBEVENTOS_PARTICIPANTES SET EVEPART_HORA_ENTRADA_REAL = '" & psHoraReal & "', " _
                            & " EVEPART_FOTO_ENTRADA = @Foto " _
                            & " WHERE EVEPART_REGISTRO  = " & Nz(TxtRegistroFechas.Text) & " "
                    End If
                    objPer.Insertar_HoraIngreso(Session("CodGrupoEmpresa"), Session("CodEmpresa"), Session("User"), FechaAct, HoraSistemaI)
                ElseIf psHoraReal <> "" And psHoraSalida <> "" Then
                    If pdLatitud <> 0 And pdlongitud <> 0 Then
                        Sql = " UPDATE TBEVENTOS_PARTICIPANTES SET EVEPART_HORA_SALIDA_REAL = '" & psHoraSalida & "', " _
                            & " EVEPART_SALIDA_LATITUD = " & pdLatitud & ", EVEPART_SALIDA_LONGITUD = " & pdlongitud & ", EVEPART_FOTO_SALIDA = @Foto " _
                            & " WHERE EVEPART_REGISTRO  = " & Nz(TxtRegistroFechas.Text) & " "
                    Else
                        Sql = " UPDATE TBEVENTOS_PARTICIPANTES SET EVEPART_HORA_SALIDA_REAL = '" & psHoraReal & "', " _
                            & " EVEPART_FOTO_SALIDA = @Foto " _
                            & " WHERE EVEPART_REGISTRO  = " & Nz(TxtRegistroFechas.Text) & " "
                    End If
                    objPer.Ingresar_HoraSalida(Session("CodGrupoEmpresa"), Session("CodEmpresa"), Session("User"), FechaAct, HoraSistemaS)
                End If
                Using cmd As New SqlCommand(Sql, con)
                    cmd.Parameters.AddWithValue("@Foto", imagenBytes)
                    con.Open()
                    nuevaId = Convert.ToInt32(cmd.ExecuteScalar()) ' Obtener el ID de la imagen insertada
                End Using
                Session("Entra") = "Si"
            End Using

            If pdLatitud <> 0 And pdlongitud <> 0 Then
                Dim CmdGlobal As New SqlCommand
                Dim Cn As New SqlClient.SqlConnection(Ruta_GrEmp)
                Cn.Open() : CmdGlobal.Connection = Cn
                CmdGlobal.CommandText = " UPDATE TBREG_ENTSAL SET ENTSAL_LATITUD =" & pdLatitud & "  , ENTSAL_LONGITUD=" & pdlongitud & " WHERE ENTSAL_FECHA ='" & FechaAct & "' AND ENTSAL_CODIGO ='" & Session("User") & "'"
                CmdGlobal.ExecuteNonQuery()
            End If


            Dim ESimagenBase64 As String = hfImagen.Value
            If ESimagenBase64 <> "" Then
                If String.IsNullOrEmpty(imagenBase64) Then
                    Response.Write("<script>alert('No se capturó ninguna imagen.');</script>")
                    Return
                End If

                ' Convertir la imagen base64 a bytes
                Dim ESimagenBytes As Byte() = Convert.FromBase64String(imagenBase64.Replace("data:image/png;base64,", ""))

                ' Guardar la imagen en la base de datos

                Dim cmdSql As New SqlCommand
                objPer._Asistencia_GuardarImagen(Session("User").Trim, FechaAct, ESimagenBytes)

            End If

            BtnCancelar_Click(sender, e)
            BtnListar_Click(sender, e)
        Else
            Session("Entra") = ""
        End If
        ' Mostrar la imagen recién guardada
        'imgMostrada.ImageUrl = "ImageHandler.ashx?id=" & nuevaId
    End Sub
End Class
