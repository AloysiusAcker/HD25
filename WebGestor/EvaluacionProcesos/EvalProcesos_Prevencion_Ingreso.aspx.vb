Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports WebGestor
Partial Class EvaluacionProcesos_EvalProcesos_Prevencion_Ingreso
    Inherits System.Web.UI.Page
    Dim ObjSeg As New ModuloSeguridad
    Dim objProceso As New ClsEval_Proceso
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            TxtApellidos.Text = ""
            TxtDNI.Text = ""
            TxtNombres.Text = ""
            BtnReporte.Visible = True
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim dt As New DataTable
        LblError.Text = ""
        dt = Nothing
        Flex.DataSource = dt
        Flex.DataBind()
        Try
            dt = ObjSeg.Buscar_Personal(Session("CodGrupoEmpresa"), Session("CodEmpresa"), TxtDNI.Text, TxtNombres.Text, TxtApellidos.Text)
            Flex.DataSource = dt
            Flex.DataBind()
            DivFlex.Visible = True
            LblRegistro.Text = "Se encontraron " & dt.Rows.Count & " registros."
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dt As New DataTable
        Dim psFechaNac As Date
        Dim pdAñosActual As Double
        dt = Nothing
        LblError.Text = ""
        Try
            If e.CommandName = "Editar" Then
                DivBusqueda.Visible = False
                DivIngreso.Visible = True
                DivDatosPersonal.Visible = True
                BtnListar.Enabled = False
                BtnGuardar.Visible = True
                BtnCancelar.Visible = True
                BtnReporte.Visible = True
                TxtCodigo.Text = Flex.Rows(Index).Cells(2).Text
                dt = ObjSeg.Buscar_xPersonal(Session("CodGrupoEmpresa"), Session("CodEmpresa"), TxtCodigo.Text)
                If dt.Rows.Count > 0 Then
                    For Each dra As DataRow In dt.Rows
                        TxtNombresCompleto.Text = Nu(dra("NOMBRES"))
                        TxtPuesto.Text = Nu(dra("PUESTO"))
                        TxtDNI2.Text = Nu(dra("DNI"))
                        TxtDistrito.Text = Nu(dra("DISTRITO"))
                        TxtSede.Text = Nu(dra("RESTAURANTE_SEDE"))
                        If Nu(dra("PERSON_FECNAC")) <> "" Then
                            psFechaNac = FormatoFecha(Nu(dra("PERSON_FECNAC")))
                            If CDbl(Mid(Nz(dra("PERSON_FECNAC")), 5, 2)) < CDbl(Mid(FechaActual, 5, 2)) Then
                                pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4))
                            ElseIf CDbl(Mid(Nz(dra("PERSON_FECNAC")), 5, 2)) > CDbl(Mid(FechaActual, 5, 2)) Then
                                pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4)) - 1
                            ElseIf CDbl(Mid(Nz(dra("PERSON_FECNAC")), 5, 2)) = CDbl(Mid(FechaActual, 5, 2)) Then
                                If CDbl(Mid(Nz(dra("PERSON_FECNAC")), 7, 2)) < CDbl(Mid(FechaActual, 7, 2)) Then
                                    pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4))
                                ElseIf CDbl(Mid(Nz(dra("PERSON_FECNAC")), 7, 2)) > CDbl(Mid(FechaActual, 7, 2)) Then
                                    pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4)) - 1
                                End If
                            End If
                        End If
                        TxtEdad.Text = pdAñosActual & " años."
                    Next
                End If
                TxtFecha.Text = FormatoFecha(FechaActual)
                TxtHora.Text = FormatoHora(HoraActual)
                TxtApellidos.Text = ""
                TxtDNI.Text = ""
                TxtNombres.Text = ""
                Flex.DataSource = dt
                Flex.DataBind()
                LblRegistro.Text = ""
                DivFlex.Visible = False

                dt = objProceso.EvalProcesos_PrevencionCovid_ExistePersonalFecha(Session("CodEmpresa"), Session("Ruta_Emp"), TxtCodigo.Text, FechaActual)
                If dt.Rows.Count = 1 Then
                    For Each dra As DataRow In dt.Rows
                        TxtTemperatura.Text = Nz(dra("PREVENCION_TEMPERATURA_INI"))
                        TxtTempMT.Text = Nz(dra("PREVENCION_TEMPERATURA_MEDIOTURNO"))
                        TxtTempFinal.Text = Nz(dra("PREVENCION_TEMPERATURA_FINAL"))
                        DdlTOS.SelectedValue = Nu(dra("PREVENCION_TOS"))
                        DdlDolorG.SelectedValue = Nu(dra("PREVENCION_DOLOR_GARGANTA"))
                        DdlDifRespirar.SelectedValue = Nu(dra("PREVENCION_ESTORNUDO_CONGNASAL"))
                        DdlEstornudos.SelectedValue = Nu(dra("PREVENCION_DIFICULTAD_RESPIRATORIA"))
                        DdlMalestar.SelectedValue = Nu(dra("PREVENCION_DOLORES_MUSCULARES"))
                    Next
                End If

            End If
            If e.CommandName = "Detalle" Then
                DivBusqueda.Visible = False
                DivIngreso.Visible = False
                DivDatosPersonal.Visible = True
                DivDetalle.Visible = True
                BtnListar.Enabled = False
                BtnGuardar.Visible = False
                BtnCancelar.Visible = True
                BtnReporte.Visible = True
                Dim a As Long = 0
                Dim dtListado As New DataTable
                TxtCodigo.Text = Flex.Rows(Index).Cells(2).Text
                dt = ObjSeg.Buscar_xPersonal(Session("CodGrupoEmpresa"), Session("CodEmpresa"), TxtCodigo.Text)
                If dt.Rows.Count > 0 Then
                    For Each dra As DataRow In dt.Rows
                        TxtNombresCompleto.Text = Nu(dra("NOMBRES"))
                        TxtPuesto.Text = Nu(dra("PUESTO"))
                        TxtDNI2.Text = Nu(dra("DNI"))
                        TxtDistrito.Text = Nu(dra("DISTRITO"))
                        TxtSede.Text = Nu(dra("RESTAURANTE_SEDE"))
                        If Nu(dra("PERSON_FECNAC")) <> "" Then
                            psFechaNac = FormatoFecha(Nu(dra("PERSON_FECNAC")))
                            If CDbl(Mid(Nz(dra("PERSON_FECNAC")), 5, 2)) < CDbl(Mid(FechaActual, 5, 2)) Then
                                pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4))
                            ElseIf CDbl(Mid(Nz(dra("PERSON_FECNAC")), 5, 2)) > CDbl(Mid(FechaActual, 5, 2)) Then
                                pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4)) - 1
                            ElseIf CDbl(Mid(Nz(dra("PERSON_FECNAC")), 5, 2)) = CDbl(Mid(FechaActual, 5, 2)) Then
                                If CDbl(Mid(Nz(dra("PERSON_FECNAC")), 7, 2)) < CDbl(Mid(FechaActual, 7, 2)) Then
                                    pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4))
                                ElseIf CDbl(Mid(Nz(dra("PERSON_FECNAC")), 7, 2)) > CDbl(Mid(FechaActual, 7, 2)) Then
                                    pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dra("PERSON_FECNAC")), 1, 4)) - 1
                                End If
                            End If
                        End If
                        TxtEdad.Text = pdAñosActual & " años."
                    Next
                End If
                dtListado.Columns.Add("C1")
                dtListado.Columns.Add("C2")
                dtListado.Columns.Add("C3")
                dtListado.Columns.Add("C4")
                dtListado.Columns.Add("C5")
                dtListado.Columns.Add("C6")
                dtListado.Columns.Add("C7")
                'dtListado.Columns.Add("C2")
                Dim drT As DataRow
                dt = ObjSeg.Buscar_xPersonal(Session("CodGrupoEmpresa"), Session("CodEmpresa"), TxtCodigo.Text)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        drT = dtListado.NewRow()
                        drT("C1") = "Nombres y Apellidos :"
                        drT("C2") = Nu(dr("NOMBRES"))
                        drT("C3") = "DNI :"
                        drT("C4") = Nu(dr("DNI"))
                        If Nu(dr!PERSON_FECNAC) <> "" Then
                            psFechaNac = FormatoFecha(Nu(dr("PERSON_FECNAC")))
                            If CDbl(Mid(Nz(dr("PERSON_FECNAC")), 5, 2)) < CDbl(Mid(FechaActual, 5, 2)) Then
                                pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dr("PERSON_FECNAC")), 1, 4))
                            ElseIf CDbl(Mid(Nz(dr("PERSON_FECNAC")), 5, 2)) > CDbl(Mid(FechaActual, 5, 2)) Then
                                pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dr("PERSON_FECNAC")), 1, 4)) - 1
                            ElseIf CDbl(Mid(Nz(dr("PERSON_FECNAC")), 5, 2)) = CDbl(Mid(FechaActual, 5, 2)) Then
                                If CDbl(Mid(Nz(dr("PERSON_FECNAC")), 7, 2)) < CDbl(Mid(FechaActual, 7, 2)) Then
                                    pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dr("PERSON_FECNAC")), 1, 4))
                                ElseIf CDbl(Mid(Nz(dr("PERSON_FECNAC")), 7, 2)) > CDbl(Mid(FechaActual, 7, 2)) Then
                                    pdAñosActual = CDbl(Mid(FechaActual, 1, 4)) - CDbl(Mid(Nz(dr("PERSON_FECNAC")), 1, 4)) - 1
                                End If
                            End If
                        End If
                        drT("C5") = "EDAD :" '
                        drT("C6") = pdAñosActual & " años."
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Restaurante/Sede :"
                        drT("C2") = Nu(dr("RESTAURANTE_SEDE"))
                        drT("C3") = "Puesto :"
                        drT("C4") = Nu(dr("PUESTO"))
                        drT("C5") = "RAZON SOCIAL :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Distrito donde reside :"
                        drT("C2") = Nu(dr("DISTRITO"))
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Toma de temperatura :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Responda si tiene algunos de estos sintomas :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Tos :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Dolor de garganta :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Estornudos y congestion nasal :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Dificultad respiratoria :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Dolores musculares o malestar general :"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = ""
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = ""
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Firma Gerente"
                        dtListado.Rows.Add(drT)
                        drT = dtListado.NewRow()
                        drT("C1") = "Firma Trabajador"
                        dtListado.Rows.Add(drT)
                    Next
                End If
                gvDetalle.DataSource = dtListado
                gvDetalle.DataBind()
                'gwLista.DataSource = dt
                'gwLista.DataBind()
                Dim pTempInicial As String = ""
                Dim pFecha As String = ""
                Dim pTos As String = ""
                Dim pEstornudo As String = ""
                Dim pDolorGarganta As String = ""
                Dim pDificultad As String = ""
                Dim pDolorMuscular As String = ""
                Dim pObservacion As String = ""
                Dim pTempMedioT As String = ""
                Dim pTempFinal As String = ""
                dt = Nothing
                a = 0

                dt = objProceso.EvalProcesos_PrevencionCovid_ListaEncuesta(Session("CodEmpresa"), Session("Ruta_Emp"), Flex.Rows(Index).Cells(2).Text)
                If dt.Rows.Count > 0 Then
                    For Each dra As DataRow In dt.Rows
                        a = a + 1
                        gvDetalle.Rows(3).Cells(a).Text = "Temp. Inicial: " & Nu(dra("PREVENCION_TEMPERATURA_INI")) & "<BR>" & "Hora: " & Nu(dra("HORA_INI"))
                        gvDetalle.Rows(4).Cells(a).Text = "Fecha: " & Nu(dra("FECHA"))
                        gvDetalle.Rows(5).Cells(a).Text = Nu(dra("PREVENCION_TOS"))
                        gvDetalle.Rows(6).Cells(a).Text = Nu(dra("PREVENCION_DOLOR_GARGANTA"))
                        gvDetalle.Rows(7).Cells(a).Text = Nu(dra("PREVENCION_ESTORNUDO_CONGNASAL"))
                        gvDetalle.Rows(8).Cells(a).Text = Nu(dra("PREVENCION_DIFICULTAD_RESPIRATORIA"))
                        gvDetalle.Rows(9).Cells(a).Text = Nu(dra("PREVENCION_DOLORES_MUSCULARES"))
                        gvDetalle.Rows(10).Cells(a).Text = "Temp. Medio Turno: " & Nu(dra("PREVENCION_TEMPERATURA_MEDIOTURNO")) & "<BR>" & "Hora: " & Nu(dra("HORA_MEDIOTURNO"))
                        gvDetalle.Rows(11).Cells(a).Text = "Temp. Final: " & Nu(dra("PREVENCION_TEMPERATURA_FINAL")) & "<BR>" & "Hora: " & Nu(dra("HORA_FINAL"))
                        gvDetalle.Rows(12).Cells(a).Text = "_____________________"
                        gvDetalle.Rows(13).Cells(a).Text = "_____________________"
                    Next
                End If

                TxtApellidos.Text = ""
                TxtDNI.Text = ""
                TxtNombres.Text = ""
                dt = Nothing
                Flex.DataSource = dt
                Flex.DataBind()
                LblRegistro.Text = ""
                DivFlex.Visible = False
            End If
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        DivBusqueda.Visible = True
        DivDatosPersonal.Visible = False
        DivIngreso.Visible = False
        BtnListar.Enabled = True
        DivDetalle.Visible = False
        BtnGuardar.Visible = False
        BtnCancelar.Visible = False
        BtnReporte.Visible = True
        DivFlex.Visible = False
        TxtCodigo.Text = ""
        TxtNombresCompleto.Text = ""
        TxtPuesto.Text = ""
        TxtSede.Text = ""
        TxtFecha.Text = ""
        TxtHora.Text = ""
        TxtObs.Text = ""
        TxtTemperatura.Text = "0"
        TxtTempMT.Text = "0"
        TxtTempFinal.Text = "0"
        DdlTOS.SelectedValue = "NO"
        DdlDolorG.SelectedValue = "NO"
        DdlDifRespirar.SelectedValue = "NO"
        DdlEstornudos.SelectedValue = "NO"
        DdlMalestar.SelectedValue = "NO"
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Dim pdTempInicial As Double = 0
        Dim pdTempMTurno As Double = 0
        Dim pdTempFinal As Double = 0
        Try
            If Nz(TxtTemperatura.Text) <> 0 Then pdTempInicial = CDbl(TxtTemperatura.Text)
            If Nz(TxtTempMT.Text) <> 0 Then pdTempMTurno = CDbl(TxtTempMT.Text)
            If Nz(TxtTempFinal.Text) <> 0 Then pdTempFinal = CDbl(TxtTempFinal.Text)
            Dim dt As New DataTable
            dt = objProceso.EvalProcesos_PrevencionCovid_ExistePersonalFecha(Session("CodEmpresa"), Session("Ruta_Emp"), TxtCodigo.Text, FechaActual)
            If dt.Rows.Count = 0 Then
                If Nz(TxtTemperatura.Text) = 0 Then LblError.Text = "Debe ingresar la temperatura inicial del trabajador." : Exit Sub
                objProceso.EvalProcesos_Insertar_PrevencionCovid(Session("CodEmpresa"), Session("Ruta_Emp"), TxtCodigo.Text, TxtFecha.Text, TxtHora.Text,
                                                         pdTempInicial, DdlTOS.Text, DdlDolorG.Text, DdlEstornudos.Text, DdlDifRespirar.Text,
                                                         DdlMalestar.Text, pdTempMTurno, pdTempFinal, TxtObs.Text, Session("User"))
            Else
                If Nz(TxtTempMT.Text) = 0 And Nz(TxtTempFinal.Text) = 0 Then LblError.Text = "Debe ingresar la temperatura Final o del medio turno del trabajador." : Exit Sub
                objProceso.EvalProcesos_Update_PrevencionCovid(Session("CodEmpresa"), Session("Ruta_Emp"), TxtCodigo.Text, TxtFecha.Text, TxtHora.Text, pdTempMTurno, pdTempFinal)
            End If
            BtnCancelar_Click(sender, e)
            LblError.Text = "Los datos del trabajador han sido guardados." : Exit Sub
        Catch ex As SqlException
            LblError.Text = "Se ha producido un errir en la base de datos. " & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un errir en la aplicacion. " & ex.Message
        Finally
        End Try
    End Sub

    Private Sub BtnReporte_Click(sender As Object, e As EventArgs) Handles BtnReporte.Click
        If gvDetalle.Visible = False Then Exit Sub
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        gvDetalle.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gvDetalle)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=PREVENCION_COVID19.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
