Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inspeccion_Lista_DatosAdicionales
    Inherits System.Web.UI.Page
    Private Sub Lista_Inspeccion()
        Dim obj As New clsInspeccion_Listado
        Dim pdCodOficina As Double = 0
        Dim NroInspeccion As Double = 0
        Dim Tecnico As String = "0"
        Dim TipoPersona As String = "0"
        Dim TipoInspeccion As String = "0"
        Dim TipoEstado As String = "0"
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        If txtNroInspeccion.Text.Trim <> "" Then NroInspeccion = txtNroInspeccion.Text.Trim
        If txtcodOficina.Text.Trim <> "" Then pdCodOficina = txtcodOficina.Text.Trim
        If cboTipoPersona.SelectedValue <> "< Seleccionar >" Then TipoPersona = cboTipoPersona.SelectedValue Else txtTecnico.Text = ""
        If txtTecnico.Text.Trim <> "" Then Tecnico = txtTecnico.Text.Trim
        If cboTipoInspeccion.SelectedValue <> "< Seleccionar >" Then TipoInspeccion = cboTipoInspeccion.SelectedValue
        If cboEstadoInspeccion.SelectedValue <> "< Seleccionar >" Then TipoEstado = cboEstadoInspeccion.SelectedValue
        If txtPorFechaInicio.Text.Trim <> "" And txtPorFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
            FechaFin = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
        ElseIf txtPorFechaInicio.Text.Trim <> "" And txtPorFechaFin.Text.Trim = "" Then
            FechaIni = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
            FechaFin = Right(txtPorFechaInicio.Text.Trim, 4) + Mid(txtPorFechaInicio.Text.Trim, 4, 2) + Left(txtPorFechaInicio.Text.Trim, 2)
        ElseIf txtPorFechaInicio.Text.Trim = "" And txtPorFechaFin.Text.Trim = "" Then
            FechaIni = "20100101"
            FechaFin = "21000101"
        ElseIf txtPorFechaInicio.Text.Trim = "" And txtPorFechaFin.Text.Trim <> "" Then
            FechaIni = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
            FechaFin = Right(txtPorFechaFin.Text.Trim, 4) + Mid(txtPorFechaFin.Text.Trim, 4, 2) + Left(txtPorFechaFin.Text.Trim, 2)
        End If
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim dRow As DataRow
        dt.Columns.Add("NRO_VISITA")
        dt.Columns.Add("TIPO")
        dt.Columns.Add("PERSONA_ASIG")
        dt.Columns.Add("OFICINA")
        dt.Columns.Add("FECHA_RELIZA")
        dt.Columns.Add("HORA_LLEGADA")
        dt.Columns.Add("HORA_INICIO")
        dt.Columns.Add("HORA_EXTRAS")
        dt.Columns.Add("HORA_FIN")
        dt.Columns.Add("FECHA_FIN")
        dt.Columns.Add("INSPEC_MOVILIDAD")
        dt.Columns.Add("INSPEC_MOVILIDAD_VUELTA")
        dt.Columns.Add("INSPEC_MOVILIDAD_DESCRIPCION")
        dt.Columns.Add("OBJETIVO")
        dt.Columns.Add("INSEPC_OBJETIVO")
        dt.Columns.Add("INSPEC_OBS")
        dt.Columns.Add("INSPEC_MOVILIDAD_IDA_2")
        dt.Columns.Add("INSPEC_MOVILIDAD_VUELTA_2")
        dt.Columns.Add("INSPEC_MOVILIDAD_DESCRIPCION_2")
        dt.Columns.Add("INSPEC_CODIGO")
        dt.Columns.Add("EXTRA_HORA_1")
        dt.Columns.Add("EXTRA_HORA_2")
        dt.Columns.Add("FECHA_PROG")
        dt.Columns.Add("HORA_PROG")
        dt.Columns.Add("INSPEC_MOV_PROCEDE")
        Try
            dtLista = obj.Listar_Inpeccion_Datos_Adicionales(psConexion, Session("CodEmpresa"), TipoPersona, _
                                                             TipoInspeccion, TipoEstado, FechaIni, FechaFin, pdCodOficina, Tecnico, NroInspeccion)
            If dtLista.Rows.Count > 0 Then
                For Each dr As DataRow In dtLista.Rows
                    dRow = dt.NewRow
                    dRow("NRO_VISITA") = Nu(dr("NRO_VISITA"))
                    dRow("TIPO") = Nu(dr("TIPO"))
                    dRow("PERSONA_ASIG") = Nu(dr("PERSONA_ASIG"))
                    dRow("OFICINA") = Nu(dr("OFICINA"))
                    If Nu(dr("INSPEC_FECHA_REALIZADA")) <> "" Then
                        dRow("FECHA_RELIZA") = Nu(dr("FECHA_RELIZA"))
                    Else
                        dRow("FECHA_RELIZA") = ""
                    End If
                    dRow("HORA_LLEGADA") = Nu(dr("HORA_LLEGADA"))
                    dRow("HORA_INICIO") = Nu(dr("HORA_INICIO"))
                    If Nu(dr("INSPEC_INI_HORA")) > "1800" Then
                        If Nu(dr("INSPEC_FIN_HORA")) = "0000" Then dr("INSPEC_FIN_HORA") = "2400"
                        dRow("HORA_EXTRAS") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "1800", Nu(dr("INSPEC_FIN_FECHA")), Nu(dr("INSPEC_FIN_HORA")))
                    End If
                    dRow("HORA_FIN") = Nu(dr("HORA_FIN"))
                    If Nu(dr("INSPEC_FIN_FECHA")) <> "" Then
                        dRow("FECHA_FIN") = Nu(dr("FECHA_FIN"))
                    Else
                        dRow("FECHA_FIN") = ""
                    End If
                    dRow("INSPEC_MOVILIDAD") = Nu(dr("INSPEC_MOVILIDAD"))
                    dRow("INSPEC_MOVILIDAD_VUELTA") = Nu(dr("INSPEC_MOVILIDAD_VUELTA"))
                    dRow("INSPEC_MOVILIDAD_DESCRIPCION") = Nu(dr("INSPEC_MOVILIDAD_DESCRIPCION"))
                    dRow("OBJETIVO") = Nu(dr("OBJETIVO"))
                    dRow("INSEPC_OBJETIVO") = Nu(dr("INSEPC_OBJETIVO"))
                    dRow("INSPEC_OBS") = Nu(dr("INSPEC_OBS"))
                    dRow("INSPEC_MOVILIDAD_IDA_2") = Nu(dr("INSPEC_MOVILIDAD_IDA_2"))
                    dRow("INSPEC_MOVILIDAD_VUELTA_2") = Nu(dr("INSPEC_MOVILIDAD_VUELTA_2"))
                    dRow("INSPEC_MOVILIDAD_DESCRIPCION_2") = Nu(dr("INSPEC_MOVILIDAD_DESCRIPCION_2"))
                    dRow("INSPEC_CODIGO") = Nu(dr("INSPEC_CODIGO"))
                    If Nu(dr("INSPEC_INI_HORA")) <= "2000" And Nu(dr("INSPEC_INI_HORA")) > "1800" Then
                        If Nu(dr("INSPEC_FIN_HORA")) <= "2000" Then
                            dRow("EXTRA_HORA_2") = ""
                            If Nu(dr("INSPEC_FIN_HORA")) <= "2000" And Nu(dr("INSPEC_FIN_HORA")) > "1800" Then
                                dRow("EXTRA_HORA_1") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "1800", Nu(dr("INSPEC_FIN_FECHA")), Nu(dr("INSPEC_FIN_HORA")))
                            ElseIf Nu(dr("INSPEC_FIN_HORA")) <= "0000" Then
                                dRow("EXTRA_HORA_1") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "1800", Nu(dr("INSPEC_FECHA_REALIZADA")), "2000")
                                dRow("EXTRA_HORA_2") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "2000", Nu(dr("INSPEC_FIN_FECHA")), "2400")
                            Else
                                dr("INSPEC_FIN_HORA") = "2000"
                                'dRow("EXTRA_HORA_1") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "1800", Nu(dr("INSPEC_FECHA_REALIZADA")), Nu(dr("INSPEC_FIN_HORA")))

                                dRow("EXTRA_HORA_1") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "1800", Nu(dr("INSPEC_FECHA_REALIZADA")), "2000")
                                dRow("EXTRA_HORA_2") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "2000", Nu(dr("INSPEC_FIN_FECHA")), Nu(dr("INSPEC_FIN_HORA")))
                            End If
                        ElseIf Nu(dr("INSPEC_FIN_HORA")) > "2000" Then
                            dRow("EXTRA_HORA_1") = "02:00"
                            dRow("EXTRA_HORA_2") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "2000", Nu(dr("INSPEC_FIN_FECHA")), Nu(dr("INSPEC_FIN_HORA")))
                        End If
                    ElseIf Nu(dr("INSPEC_INI_HORA")) > "2000" Then
                        dRow("EXTRA_HORA_1") = "02:00"
                        dRow("EXTRA_HORA_2") = Hallar_Diferencias(Nu(dr("INSPEC_FECHA_REALIZADA")), "2000", Nu(dr("INSPEC_FIN_FECHA")), Nu(dr("INSPEC_FIN_HORA")))
                    End If
                    dRow("FECHA_PROG") = Nu(dr("FECHA_PROG"))
                    dRow("HORA_PROG") = Nu(dr("HORA_PROG"))
                    dRow("INSPEC_MOV_PROCEDE") = Nu(dr("INSPEC_MOV_PROCEDE"))
                    dt.Rows.Add(dRow)
                Next
            End If
            Flex.DataSource = dt
            Flex.DataBind()
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
            Exit Sub
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Private Function Hallar_Diferencias(ByVal Fi As String, ByVal Hi As String, ByVal Ff As String, ByVal Hf As String) As String
        Dim MinDif As Long, hora As Long, Min As Long
        Dim DiasDif As Long
        Dim FechaI As String = ""
        Dim FechaF As String = ""
        On Error GoTo Hallar
        Hallar_Diferencias = ""
        FechaI = Right(Fi, 2) & "/" & Mid(Fi, 5, 2) & "/" & Left(Fi, 4)
        FechaF = Right(Ff, 2) & "/" & Mid(Ff, 5, 2) & "/" & Left(Ff, 4)
        Dim diaHora As Long
        If Fi = "" Or Hi = "" Or Ff = "" Or Hf = "" Then Exit Function
        If Fi = Ff Then
            If Hi = Hf Then
                Hallar_Diferencias = "00 Días 00 Hrs 00 Min"
                Hallar_Diferencias = 0
            Else
                MinDif = ((CInt(Left(Hf, 2)) * 60) + CInt(Right(Hf, 2))) - ((CInt(Left(Hi, 2)) * 60) + CInt(Right(Hi, 2)))
                hora = CInt(MinDif) \ 60
                Min = CInt(MinDif) Mod 60
                Hallar_Diferencias = Format(hora, "00") & ":" & Format(Min, "00")
                'Hallar_Diferencias = MinDif
            End If
        Else
            If Hi = Hf Then
                DiasDif = DateDiff("d", FechaI, FechaF)
                Hallar_Diferencias = Format(DiasDif, "00") & " Días 00 Hrs 00 Min"
                MinDif = CLng(DiasDif * 24 * 60)
            Else
                MinDif = ((CInt(Left(Hf, 2)) * 60) + CInt(Right(Hf, 2))) - ((CInt(Left(Hi, 2)) * 60) + CInt(Right(Hi, 2)))
                DiasDif = DateDiff("d", FechaI, FechaF)
                If DiasDif <> 1 Then
                    If MinDif > 0 Then
                        hora = CInt(MinDif) \ 60
                        Min = CInt(MinDif) Mod 60
                    Else
                        hora = (CInt(MinDif) \ 60) * -1
                        Min = (CInt(MinDif) Mod 60) * -1
                        hora = 24 - hora
                        Min = 60 - Min
                        DiasDif = DiasDif - 1
                        If hora = 24 Then hora = 23
                    End If
                    diaHora = DiasDif * 24
                    diaHora = diaHora + hora
                    Hallar_Diferencias = Format(diaHora, "00") & ":" & Format(Min, "00")
                    MinDif = CLng(CLng(DiasDif * 24 * 60) + (hora * 60) + Min)
                Else 'dif Días 1 dia
                    If MinDif > 0 Then 'paso realmente mas de un dia
                        hora = CInt(MinDif) \ 60
                        Min = CInt(MinDif) Mod 60
                        diaHora = 1 * 24
                        diaHora = diaHora + hora
                        Hallar_Diferencias = Format(diaHora, "00") & ":" & Format(Min, "00")
                    Else
                        hora = (CInt(MinDif) \ 60) * -1
                        Min = (CInt(MinDif) Mod 60) * -1
                        hora = 23 - hora
                        If Min > 0 Then Min = 60 - Min Else hora = hora + 1
                        Hallar_Diferencias = Format(hora, "00") & ":" & Format(Min, "00")
                    End If
                End If
            End If
        End If
        Exit Function
Hallar:
    End Function
    Protected Sub btnListarDatosAdicionales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarDatosAdicionales.Click
        Call Lista_Inspeccion()
    End Sub
    Protected Sub cboEstadoInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub cboTipoInspeccion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub cboTipoPersona_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipoPersona.SelectedIndexChanged
        If cboTipoPersona.SelectedValue <> "< Seleccionar >" Then
            txtRucTipoPersona.Text = ""
            txtRazonSocialTipoPersona.Text = ""
        Else
            txtRucTipoPersona.Text = ""
            txtRazonSocialTipoPersona.Text = ""
        End If
    End Sub
    Protected Sub btnListarOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarOficina.Click
        Dim obj As New clsInv_Listados
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexOficina.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCodigo.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexOficina.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
        ModalPopupExtender1.Show()
    End Sub
    Private Sub listarTipoPersonaXProveedor()
        Dim obj As New clsInspeccion_Listado
        Try
            Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
            FlexTipoPers.DataSource = obj.Lista_TipoPersona(psConexion, Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub listarTipoPersonaXTecnico()
        Dim obj As New clsInspeccion_Listado
        Dim psCodGrupoEmp As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        psCodGrupoEmp = Session("CodGrupoEmpresa") 'psCodGrupoEmp, Session("CodEmpresa"), 
        Try
            FlexTipoPers.DataSource = obj.Lista_TipoPersonaTecnico(psConexion, psCodGrupoEmp, Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnListarTipoPers_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarTipoPers.Click
        If cboTipoPersona.SelectedValue.Trim = "3" Then
            listarTipoPersonaXProveedor()
        ElseIf cboTipoPersona.SelectedValue.Trim = "< Seleccionar >" Then
            ModalPopupExtender2.Hide()
        Else
            listarTipoPersonaXTecnico()
        End If
        ModalPopupExtender2.Show()
    End Sub
    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            'lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtTecnico.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtRucTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtRazonSocialTipoPersona.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            End If
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            txtRucTipoPers.Text = ""
            txtRazonSocialTipoPers.Text = ""
            ModalPopupExtender2.Hide()
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub FlexOficina_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Aceptar" Then
                txtPorCodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtPorOficDescrip.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtcodOficina.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexOficina.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                FlexOficina.DataSource = Nothing
                FlexOficina.DataBind()
                txtBusCodigo.Text = ""
                txtBusDescripcion.Text = ""
                ModalPopupExtender1.Hide()
            End If
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnBuscarXOficina_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarXOficina.Click

    End Sub
    Protected Sub btnBuscarTipoPersona_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscarTipoPersona.Click

    End Sub
    Protected Sub txtPorCodOficina_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtcodOficina.Text = ""
        txtPorCodOficina.Text = ""
        txtPorOficDescrip.Text = ""
    End Sub
    Protected Sub txtRucTipoPersona_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRucTipoPersona.TextChanged
        txtRucTipoPersona.Text = ""
        txtRazonSocialTipoPersona.Text = ""
        txtTecnico.Text = ""
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
            Call LlenaComboItem("TBOPC379", cboEstadoInspeccion, psCnGrEmp)
            cboEstadoInspeccion.Items.Add("< Seleccionar >")
            cboEstadoInspeccion.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC381", cboTipoInspeccion, psCnGrEmp)
            cboTipoInspeccion.Items.Add("< Seleccionar >")
            cboTipoInspeccion.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC378", cboTipoPersona, psCnGrEmp)
            cboTipoPersona.Items.Add("< Seleccionar >")
            cboTipoPersona.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
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
        form.Controls.Add(Flex)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=DatosAdicionales.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            'lblError.Text = ""
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            If e.CommandName = "Editar" Then
                lblMovilidad.Visible = True
                txtNroVisita.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtMovilidadIda1.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtMovilidadVuelta1.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtMovilidadDescripcion1.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtNroInspecc.Text = Flex.Rows(Index).Cells(20).Text.Trim
                txtMovilidadIdaEditar.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtMovilidadVueltaEditar.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtMovilidadDescripcionEditar.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                txtProcede.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(23).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If txtProcede.Text = "SI" Then
                    OptProcede.SelectedIndex = 0
                Else
                    OptProcede.SelectedIndex = 1
                End If
                lblEditarMovilidad.Visible = False
            End If

        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch ex As Exception
            'lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnGuardarMovilidad_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New clsInspeccion_InsUpdDel
        Dim NroInsp As Double = 0
        Dim mov_ida2 As Double = 0
        Dim mov_vuelta2 As Double = 0
        Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
        NroInsp = txtNroInspecc.Text
        mov_ida2 = Nz(txtMovilidadIdaEditar.Text.Trim)
        mov_vuelta2 = Nz(txtMovilidadVueltaEditar.Text.Trim)
        Try
            If OptProcede.SelectedIndex <> 0 And OptProcede.SelectedIndex <> 1 Then lblEditarMovilidad.Text = "Debe de tener un estado" : Exit Sub
            If OptProcede.SelectedIndex = 0 Then
                obj.Upd_Inspeccion_Movilidad_Desplazamiento(psConexion, Session("CodEmpresa"), NroInsp, mov_ida2, mov_vuelta2, txtMovilidadDescripcionEditar.Text.Trim, "SI")
                lblEditarMovilidad.Visible = True
                lblEditarMovilidad.Text = "Movilidad Registrado"
                Call Lista_Inspeccion()
            ElseIf OptProcede.SelectedIndex = 1 Then
                obj.Upd_Inspeccion_Movilidad_Desplazamiento(psConexion, Session("CodEmpresa"), NroInsp, mov_ida2, mov_vuelta2, txtMovilidadDescripcionEditar.Text.Trim, "NO")
                lblEditarMovilidad.Visible = True
                lblEditarMovilidad.Text = "Movilidad Registrado"
                Call Lista_Inspeccion()
            End If
        Catch ex As Exception
            lblMovilidad.Visible = False
        End Try
        Call Limpiar_EdicionMovilidad()
        lblMovilidad.Visible = False
    End Sub
    Protected Sub btnCancelarMovilidad_Click1(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Limpiar_EdicionMovilidad()
        lblMovilidad.Visible = False
    End Sub
    Sub Limpiar_EdicionMovilidad()
        txtMovilidadIda1.Text = ""
        txtMovilidadIdaEditar.Text = ""
        txtMovilidadVuelta1.Text = ""
        txtMovilidadVueltaEditar.Text = ""
        txtMovilidadDescripcion1.Text = ""
        txtMovilidadDescripcionEditar.Text = ""
    End Sub

    Protected Sub btnCerrar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar2.Click

    End Sub
End Class
