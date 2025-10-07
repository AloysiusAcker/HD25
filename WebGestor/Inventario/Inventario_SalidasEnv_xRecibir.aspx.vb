Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO

Partial Class Inventario_Inventario_SalidasEnv_xRecibir
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objEmp As New ModuloGeneral
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ficha.ActiveTabIndex = 1 : ficha.ActiveTab.Enabled = False
            ficha.ActiveTabIndex = 0 : ficha.ActiveTab.Enabled = True
            ficha.ActiveTabIndex = 0 : ficha.TabIndex = "0"
            Me.Page.Session.Timeout = 1080
        End If
    End Sub

    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ficha.ActiveTabChanged
        If ficha.TabIndex = "0" Then
            BtnListarSalidas_Click(sender, e)
            GvSalidaAcc.DataSource = Nothing
            GvSalidaAcc.DataBind()
            GvSalidaBienes.DataSource = Nothing
            GvSalidaBienes.DataBind()
        ElseIf ficha.TabIndex = "1" Then

        End If
    End Sub
    Private Sub BtnListarSalidas_Click(sender As Object, e As EventArgs) Handles BtnListarSalidas.Click
        Dim pCodSalida As Double = 0
        Dim TipoLista As String = ""
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp")
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
        Dim psMotivo As String = ""
        Dim psEstado As String = ""
        psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        If TxtFechaFin.Text = "" Then
            psFechaFin = psFecha
        Else
            psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
        End If
        Try
            If DdlRemitente.SelectedValue = "1" Then
                gridSalida.DataSource = obj.Lista_SalidaEnviadaAlmacen(psConexion, Session("CodEmpresa"), pCodSalida, psFecha, psFechaFin, psMotivo, psEstado)
                gridSalida.DataBind()
            ElseIf DdlRemitente.SelectedValue = "2" Then
                gridSalida.DataSource = obj.Lista_SalidaEnviada_cCCosto(psConexion, Session("CodEmpresa"), pCodSalida, psFecha, psFechaFin, psMotivo, psEstado)
                gridSalida.DataBind()
            End If '
        Catch ex As SqlException

        Catch ex As Exception

        Finally
        End Try
    End Sub
    Private Sub gridSalida_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridSalida.RowCommand

        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Ingreso" Then
            ficha.ActiveTabIndex = 0 : ficha.ActiveTab.Enabled = False
            ficha.ActiveTabIndex = 1 : ficha.ActiveTab.Enabled = True
            ficha.ActiveTabIndex = 1
            LblCodDestino.Text = gridSalida.Rows(Index).Cells(13).Text
            LblCodOrigen.Text = gridSalida.Rows(Index).Cells(14).Text
            LblCodMotivo.Text = gridSalida.Rows(Index).Cells(15).Text
            TxtOrigenTipo.Text = IIf(gridSalida.Rows(Index).Cells(16).Text = "Salida Al", "Almacén", "Centro Costos")
            TxtOrigenCodigo.Text = gridSalida.Rows(Index).Cells(4).Text
            txtIngSalida.Text = Llenar_Ceros(gridSalida.Rows(Index).Cells(1).Text, 6)
            txtOrigenDescrip.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridSalida.Rows(Index).Cells(5).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&amp;", "&")
            TxtDestinoCodigo.Text = gridSalida.Rows(Index).Cells(7).Text
            TxtDestinoTipo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridSalida.Rows(Index).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&amp;", "&")
            TxtDestinoDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridSalida.Rows(Index).Cells(8).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&amp;", "&")
            Dim dt As New DataTable
            Dim pdCodSalida As Double = txtIngSalida.Text

            If DdlRemitente.SelectedValue = "1" Then
                dt = obj.Lista_SalidaEnviada_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodSalida)
                GvSalidaBienes.DataSource = dt
                GvSalidaBienes.DataBind()
                dt = Nothing

                dt = obj.Lista_SalidaEnviada_Detalle_Cantidades(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodSalida)
                gvCantidadesBienes.DataSource = dt
                gvCantidadesBienes.DataBind()

                dt = Nothing
                dt = obj.Lista_SalidaEnviada_Detalle_SinSerie(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodSalida)
                GvSalidaAcc.DataSource = dt
                GvSalidaAcc.DataBind()
                dt = Nothing
            ElseIf DdlRemitente.SelectedValue = "2" Then
                dt = obj.Lista_SalidaCCostoEnviada_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodSalida)
                GvSalidaBienes.DataSource = dt
                GvSalidaBienes.DataBind()
                dt = Nothing

                dt = obj.Lista_SalidaCCostoEnviada_Detalle_Cantidades(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodSalida)
                gvCantidadesBienes.DataSource = dt
                gvCantidadesBienes.DataBind()

                dt = Nothing
                dt = obj.Lista_SalidaCCostoEnviada_Detalle_SinSerie(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodSalida)
                GvSalidaAcc.DataSource = dt
                GvSalidaAcc.DataBind()
                dt = Nothing
            End If

            ficha.ActiveTabIndex = 0 : ficha.ActiveTab.Enabled = False
            ficha.ActiveTabIndex = 1 : ficha.ActiveTab.Enabled = True
            ficha.ActiveTabIndex = 1 : ficha.TabIndex = "1"
            If GvSalidaAcc.Rows.Count > 0 Then
                chkRecibirAcc.Visible = True
                GvSalidaAcc.Visible = True
            End If
            If GvSalidaBienes.Rows.Count > 0 Then
                GvSalidaBienes.Visible = True
            End If
            If gvCantidadesBienes.Rows.Count > 0 Then
                gvCantidadesBienes.Visible = True
            End If
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        ficha.ActiveTabIndex = 1 : ficha.ActiveTab.Enabled = False
        ficha.ActiveTabIndex = 0 : ficha.ActiveTab.Enabled = True
        ficha.ActiveTabIndex = 0 : ficha.TabIndex = "0"
        chkRecibirAcc.Visible = False
        'btnGuardarAccCant.Visible = False
        TxtOrigenCodigo.Text = ""
        txtOrigenDescrip.Text = ""
        TxtDestinoCodigo.Text = ""
        TxtDestinoDescripcion.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvSalidaAcc.DataSource = dt
        GvSalidaAcc.DataBind()
        GvSalidaBienes.DataSource = dt
        GvSalidaBienes.DataBind()
        Ficha_ActiveTabChanged(sender, e)
    End Sub

    'Protected Sub btnGuardarAccCant_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarAccCant.Click
    '    Dim ia As Integer
    '    Dim objUpd As New clsInv_InsUpdDel
    '    Dim pdCodSalida As Double = txtIngSalida.Text.Trim
    '    Dim pdCodArt As Double = 0
    '    Dim pdCantRec As Double = 0
    '    Dim pdCantFalta As Double = 0
    '    Dim pdCantParcial As Double = 0
    '    Dim pdCantSob As Double = 0
    '    If GvSalidaAcc.Rows.Count = 0 Then LblError.Text = "No hay Accesorios que recibir." : Exit Sub
    '    For ia = 0 To GvSalidaAcc.Rows.Count - 1
    '        pdCodArt = GvSalidaAcc.Rows(ia).Cells(1).Text.Trim
    '        pdCantRec = GvSalidaAcc.Rows(ia).Cells(5).Text.Trim
    '        pdCantFalta = GvSalidaAcc.Rows(ia).Cells(6).Text.Trim
    '        objUpd.Salida_Upd_CantAccesorio(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodSalida, pdCodArt, pdCantRec, pdCantFalta)
    '    Next
    '    Call Calculo_Cantidades_Recibidas()
    'End Sub
    Protected Sub chkRecibirAcc_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRecibirAcc.CheckedChanged
        Dim ia As Integer
        If GvSalidaAcc.Rows.Count = 0 Then LblError.Text = "No hay Accesorios que recibir." : Exit Sub
        For ia = 0 To GvSalidaAcc.Rows.Count - 1
            If chkRecibirAcc.Checked = True Then
                GvSalidaAcc.Rows(ia).Cells(5).Text = GvSalidaAcc.Rows(ia).Cells(4).Text
                GvSalidaAcc.Rows(ia).Cells(6).Text = "0"
                GvSalidaAcc.Rows(ia).Cells(7).Text = "X"
            Else
                GvSalidaAcc.Rows(ia).Cells(5).Text = "0"
                GvSalidaAcc.Rows(ia).Cells(6).Text = GvSalidaAcc.Rows(ia).Cells(4).Text
                GvSalidaAcc.Rows(ia).Cells(7).Text = ""
            End If
        Next
    End Sub
    Private Sub Calculo_Cantidades_Recibidas()
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim i As Long = 0
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        'CONTADOR POR ARTICULO Q NO NECESITA SERIE
        For i = 0 To GvSalidaAcc.Rows.Count - 1
            CmdGlobal.CommandText = "SELECT DESPD_CANT_REC,DESPD_CANT_FALT_REC FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE WHERE ARTICULO_CODIGO=" & GvSalidaAcc.Rows(i).Cells(1).Text & " AND DESP_CODIGO=" & txtIngSalida.Text.Trim & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    GvSalidaAcc.Rows(i).Cells(5).Text = Nz(Rs!DESPD_CANT_REC)
                    GvSalidaAcc.Rows(i).Cells(6).Text = Nz(Rs!DESPD_CANT_FALT_REC)
                End While
            Else
                GvSalidaAcc.Rows(i).Cells(5).Text = "0"
                GvSalidaAcc.Rows(i).Cells(6).Text = GvSalidaAcc.Rows(i).Cells(4).Text
            End If
            Rs.Close()
        Next
    End Sub

    Private Sub BtnEjecutar_Click(sender As Object, e As EventArgs) Handles BtnEjecutar.Click
        Dim Rs As SqlDataReader
        Dim objProceso As New clsInv_Procesos
        Dim Rs1 As SqlDataReader
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim aa As Integer, a As Integer
        Dim Stock As Double = 0
        Dim TotalArt As Long = 0
        Dim pdNroMov As Double = 0
        Dim ValorSys As String = ""
        aa = 0
        With GvSalidaBienes
            For i = 0 To .Rows.Count - 1
                If .Rows(i).Cells(6).Text = "X" Then aa = aa + 1
            Next
        End With
        'If aa = 0 Then
        With GvSalidaAcc
            For i = 0 To .Rows.Count - 1
                If .Rows(i).Cells(7).Text = "X" Then aa = aa + 1
            Next
        End With
        'End If
        If aa < (GvSalidaAcc.Rows.Count + GvSalidaBienes.Rows.Count) Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Tiene que Recibir todos los Equipos y/o Accesorios.')", True)
        ElseIf aa = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Equipos y/o Accesorios que recibir.')", True)
        Else
            With GvSalidaBienes
                For i = 1 To .Rows.Count - 1
                    If .Rows(i).Cells(6).Text = "X" Then TotalArt = TotalArt + 1
                Next
            End With
            With GvSalidaAcc
                For i = 1 To .Rows.Count - 1
                    If .Rows(i).Cells(7).Text = "X" Then TotalArt = TotalArt + Nz(.Rows(i).Cells(4).Text)
                Next
            End With
            ValorSys = Session("User") & FechaActual() & HoraActual()
            Dim pdItem As Double = 0

            '::::::::::::::::::::::::::::::::::::::: ARTICULOS Q USA SERIE
            If DdlRemitente.SelectedValue = "1" Then
                CmdGlobal.CommandText = " DELETE FROM TBINV_ALMACEN_DESPACHO_DET WHERE DESP_CODIGO=" & txtIngSalida.Text
                CmdGlobal.ExecuteNonQuery()
            Else
                CmdGlobal.CommandText = " DELETE FROM TBINV_CCOSTO_SALIDA_DET WHERE OSAL_CODIGO=" & txtIngSalida.Text
                CmdGlobal.ExecuteNonQuery()
            End If
            With GvSalidaBienes
                For i = 0 To .Rows.Count - 1
                    If .Rows(i).Cells(6).Text = "X" Then
                        pdItem = pdItem + 1
                        If DdlRemitente.SelectedValue = "1" Then 'O.D.
                            CmdGlobal.CommandText = " SELECT * FROM TBINV_ALMACEN_DESPACHO_DET WHERE  EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & txtIngSalida.Text & " AND SERIE_NUMERAR=" & .Rows(i).Cells(7).Text
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO = 'M'  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & txtIngSalida.Text & " AND SERIE_NUMERAR=" & .Rows(i).Cells(7).Text
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal2.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM,  DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ,DESPD_SYS_REC, DESPD_MODO_RECIBIDO,SERIE_NUMERAR) " _
                                                    & " VALUES('" & Session("CodEmpresa") & "'," & txtIngSalida.Text & "," & pdItem & ",'S','0'," & .Rows(i).Cells(1).Text & ",'" & LblCodMotivo.Text & "','S','" & ValorSys & "','M'," & .Rows(i).Cells(7).Text & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                            Rs.Close()
                        Else 'O.S.
                            CmdGlobal.CommandText = " SELECT * FROM TBINV_CCOSTO_SALIDA_DET WHERE  EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & txtIngSalida.Text & " AND SERIE_NUMERAR=" & .Rows(i).Cells(7).Text
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET SET RECIBIDA_OK='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO = 'M'  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & txtIngSalida.Text & " AND SERIE_NUMERAR=" & .Rows(i).Cells(7).Text
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal2.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, OSALD_ARTICULO_CODIGO, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO,OSALD_SYS_REC ,OSALD_MODO_RECIBIDO,SERIE_NUMERAR) " _
                                            & " VALUES('" & Session("CodEmpresa") & "'," & txtIngSalida.Text & "," & pdItem & "," & .Rows(i).Cells(1).Text & ",'S','S','0','" & LblCodMotivo.Text & "','" & ValorSys & "','A'," & .Rows(i).Cells(7).Text & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                            Rs.Close()
                        End If

                        'paso 1
                        'se agrego para poder tener la informacion de stock de centro de costo en una misma tabla dependiendo del tipo de ubicacion
                        'INGRESO EN STOCK ALMACEN
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & LblCodDestino.Text & ") AND (UBICACT_TIPO='2')" _
                        & " AND (ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                Stock = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                                CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & Stock & " WHERE (ALMACEN_CODIGO = " & LblCodDestino.Text & ") " _
                                                    & " AND (ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (UBICACT_TIPO='2')"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                & "VALUES('2'," & LblCodDestino.Text & "," & .Rows(i).Cells(1).Text & ",1,'0','" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                        Rs.Close()

                        'paso 2
                        'aqui se guardara el movimiento de ingreso al centro de costo
                        'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL=========================================================================

                        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                pdNroMov = Nz(Rs(0)) + 1
                            End While

                        Else
                            pdNroMov = 1
                        End If
                        Rs.Close()
                        '1: INGRESO, 2:SALIDA
                        'FALTA KARDEX
                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), txtIngSalida.Text, LblCodMotivo.Text, Nz(.Rows(i).Cells(1).Text), "2", LblCodDestino.Text, DdlRemitente.SelectedValue, LblCodOrigen.Text, "", "1", FechaActual, 1)

                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                            & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                            & " values('" & Session("CodEmpresa") & "'," & pdNroMov & ",'1','2','" & LblCodDestino.Text & "','" & DdlRemitente.SelectedValue & "','" & LblCodOrigen.Text & "', " _
                                            & " '" & txtIngSalida.Text & "','" & .Rows(i).Cells(1).Text & "','1','" & ValorSys & "','3','" & LblCodMotivo.Text & "','" & FechaActual() & "','0')"
                        CmdGlobal.ExecuteNonQuery()

                        Dim pstipoorigen As String = ""
                        Dim psTipoDetsino As String = ""
                        pstipoorigen = IIf(TxtOrigenTipo.Text = "Almacén", "1", "2")
                        psTipoDetsino = IIf(TxtDestinoTipo.Text = "Almacen", "1", "2")

                        If .Rows(i).Cells(11).Text <> LblCodOrigen.Text Then
                            objProceso.Invnetario_Salida_Ingreso_Automatico(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), .Rows(i).Cells(10).Text, pstipoorigen, .Rows(i).Cells(11).Text, LblCodOrigen.Text, .Rows(i).Cells(7).Text, .Rows(i).Cells(1).Text)
                        End If

                        '===================================================================================================================================================
                        CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='2',UBICACT_CODIGO=" & LblCodDestino.Text & ",UBICACT_SYS='" & ValorSys & "' WHERE SERIE_NUMERAR=" & .Rows(i).Cells(7).Text
                        CmdGlobal.ExecuteNonQuery()
                        'ESTADO: 0 primera vez, 1 EN TRANSITO,2 OK
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL,MOTIVO) " _
                                        & "VALUES(" & .Rows(i).Cells(1).Text & ",'2'," & LblCodDestino.Text & ",'2','0','" & ValorSys & "','" & FechaActual() & "','" & DdlRemitente.SelectedValue & "','" & txtIngSalida.Text & "','" & LblCodMotivo.Text & "')"
                        CmdGlobal.ExecuteNonQuery()
                        '-------------------------------------
                    End If
                Next
            End With
            a = 0
            Dim QARec As Long = 0
            Dim QRec As Long = 0
            Dim QFaltRec As Long = 0
            '::::::::::::::::::::::::::::::::::::::: ARTICULOS Q NO USA SERIE
            Dim StockAc As Double = 0

            With GvSalidaAcc
                For i = 0 To .Rows.Count - 1
                    If .Rows(i).Cells(7).Text = "X" Then
                        a = a + 1
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_SINSERIE_CCOSTO WHERE (CECOSE_CODIGO = " & LblCodDestino.Text & ") " _
                    & " AND (ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SKSSCC_STOCK_ACTUAL) + CDbl(Nz(.Rows(i).Cells(4).Text))
                                CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_SINSERIE_CCOSTO SET SKSSCC_STOCK_ACTUAL=" & StockAc & " WHERE (CECOSE_CODIGO = " & LblCodDestino.Text & ") " _
                                            & " AND (ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal2.CommandText = "INSERT TBINV_STOCK_SINSERIE_CCOSTO(CECOSE_CODIGO, ARTICULO_CODIGO,SKSSCC_STOCK_ACTUAL,SKSSCC_SYS_EST,EMPRESA_CODIGO) " _
                                            & "VALUES(" & LblCodDestino.Text & "," & .Rows(i).Cells(1).Text & "," & CDbl(Nz(.Rows(i).Cells(4).Text)) & ",'0','" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                        Rs.Close()
                        'INGRESO EN STOCK ALMACEN
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & LblCodDestino.Text & ") AND (UBICACT_TIPO='2')" _
                    & " AND (ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + CDbl(Nz(.Rows(i).Cells(4).Text))
                                CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & LblCodDestino.Text & ") " _
                                            & " AND (ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (UBICACT_TIPO='2')"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                            & "VALUES('2'," & LblCodDestino.Text & "," & .Rows(i).Cells(1).Text & ",'" & CDbl(Nz(.Rows(i).Cells(4).Text)) & "','0','" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                        Rs.Close()
                        'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL=========================================================================

                        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        Rs1 = CmdGlobal.ExecuteReader
                        If Rs1.HasRows Then
                            While Rs1.Read
                                pdNroMov = Nz(Rs1(0)) + 1
                            End While

                        Else
                            pdNroMov = 1
                        End If
                        Rs1.Close()
                        '1: INGRESO, 2:SALIDA
                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), txtIngSalida.Text, LblCodMotivo.Text, Nz(.Rows(i).Cells(1).Text), "2", LblCodDestino.Text, DdlRemitente.SelectedValue, LblCodOrigen.Text, "", "1", FechaActual, CDbl(Nz(Nz(.Rows(i).Cells(4).Text))))

                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                        & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                        & " values('" & Session("CodEmpresa") & "','" & pdNroMov & "','1','2','" & LblCodDestino.Text & "','" & DdlRemitente.SelectedValue & "','" & LblCodOrigen.Text & "', " _
                                        & " '" & txtIngSalida.Text & "'," & .Rows(i).Cells(1).Text & "," & Nz(.Rows(i).Cells(4).Text) & ",'" & ValorSys & "','3','" & LblCodMotivo.Text & "','" & FechaActual() & "','0')"
                        CmdGlobal.ExecuteNonQuery()
                        '===================================================================================================================================================
                        'INGRESO DE PERSONA QUE RECIBE
                        If DdlRemitente.SelectedValue = "1" Then
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET_SINSERIE SET DESPD_CANT_REC = DESPD_CANT_DESP, DESPD_CANT_FALT_REC = 0, DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO = 'M' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & txtIngSalida.Text & " AND ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text
                            CmdGlobal.ExecuteNonQuery()
                        Else
                            CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET_SINSERIE SET OSALD_CANT_REC = OSALD_CANT_ENV, OSALD_CANT_FALT_REC = 0, OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO = 'M' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & txtIngSalida.Text & " AND ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    End If
                Next
            End With
            Dim EstadoDesp As String = ""
            QARec = 0
            QRec = 0
            QFaltRec = 0
            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q USA SERIE
            'If DdlRemitente.SelectedValue = "1" Then
            '    CmdGlobal.CommandText = "SELECT SUM(CASE WHEN RECIBIDA_OK='N' THEN 1 ELSE 0 END) AS CFALT, SUM(CASE WHEN RECIBIDA_OK='S' THEN 1 ELSE 0 END) AS CREC,COUNT(RECIBIDA_OK) AS CAREC " _
            '    & " FROM TBINV_ALMACEN_DESPACHO_DET WHERE (DESP_CODIGO =" & txtIngSalida.Text & ") AND (DESPD_OK='S') AND (DESPD_SYS_EST='0')"
            'Else
            '    CmdGlobal.CommandText = "SELECT SUM(CASE WHEN RECIBIDA_OK='N' THEN 1 ELSE 0 END) AS CFALT, SUM(CASE WHEN RECIBIDA_OK='S' THEN 1 ELSE 0 END) AS CREC,COUNT(RECIBIDA_OK) AS CAREC " _
            '    & " FROM TBINV_CCOSTO_SALIDA_DET WHERE (OSAL_CODIGO =" & txtIngSalida.Text & ") AND (ENVIADA_OK='S') AND (OSALD_SYS_EST='0')"
            'End If
            'Rs1 = CmdGlobal.ExecuteReader
            'If Rs1.HasRows Then
            '    While Rs1.Read
            '        QARec = Nz(Rs1!CAREC)
            '        QRec = Nz(Rs1!CREC)
            '        QFaltRec = Nz(Rs1!CFALT)
            '    End While
            'End If
            'Rs1.Close()
            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q NO USA SERIE
            If DdlRemitente.SelectedValue = "1" Then
                CmdGlobal.CommandText = "SELECT SUM(DESPD_CANT_FALT_REC) AS CFALT, SUM(DESPD_CANT_REC) AS CREC, SUM(DESPD_CANT_DESP) AS CAREC " _
                & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE WHERE (DESP_CODIGO =" & txtIngSalida.Text & ") AND (DESPD_SYS_EST='0')"
            Else
                CmdGlobal.CommandText = "SELECT SUM(OSALD_CANT_FALT_REC) AS CFALT, SUM(OSALD_CANT_REC) AS CREC, SUM(OSALD_CANT_ENV) AS CAREC " _
                & " FROM TBINV_CCOSTO_SALIDA_DET_SINSERIE WHERE (OSAL_CODIGO =" & txtIngSalida.Text & ") AND (OSALD_SYS_EST='0')"
            End If
            Rs1 = CmdGlobal.ExecuteReader
            If Rs1.HasRows Then
                While Rs1.Read
                    QARec = QARec + Nz(Rs1!CAREC)
                    QRec = QRec + Nz(Rs1!CREC)
                    QFaltRec = QFaltRec + Nz(Rs1!CFALT)
                End While
            End If
            Rs1.Close()

            Dim pdCanAcc As Double = 0
            Dim pdCantBienes As Double = 0
            Dim pdCantTotalBienes As Double = 0
            Dim pdCantFaltaBienes As Double = 0
            For aa = 0 To gvCantidadesBienes.Rows.Count - 1
                pdCantTotalBienes = pdCantTotalBienes + Nz(gvCantidadesBienes.Rows(aa).Cells(4).Text)
            Next
            pdCantBienes = GvSalidaBienes.Rows.Count
            pdCantFaltaBienes = pdCantTotalBienes - pdCantBienes

            QARec = QARec + pdCantTotalBienes
            QRec = QRec + pdCantBienes
            QFaltRec = QFaltRec + pdCantFaltaBienes


            If QARec = QRec And QFaltRec = 0 Then EstadoDesp = "3" Else EstadoDesp = "4"
            If DdlRemitente.SelectedValue = "1" Then
                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='" & EstadoDesp & "',DESP_CANT_REC=" & QRec & ",DESP_CANT_FALT_REC=" & QFaltRec & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND  DESP_CODIGO=" & txtIngSalida.Text
            Else
                CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='" & EstadoDesp & "',OSAL_CANT_REC=" & QRec & ",OSAL_CANT_FALT_REC=" & QFaltRec & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND  OSAL_CODIGO=" & txtIngSalida.Text
            End If
            CmdGlobal.ExecuteNonQuery()
            ficha.ActiveTabIndex = 1 : ficha.ActiveTab.Enabled = False
            ficha.ActiveTabIndex = 0 : ficha.ActiveTab.Enabled = True
            ficha.ActiveTabIndex = 0 : ficha.TabIndex = "0"
            Ficha_ActiveTabChanged(sender, e)
            chkRecibirAcc.Checked = False
        End If
    End Sub

    Private Sub Recibir_Bien(ByVal psSerieNro As String, Optional pdPlacaNro As Double = 0)

        Dim obj As New clsInv_Listados
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim placa As Double = 0
        Dim serie As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Dim drT As DataRow
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn

            dt.Columns.Add("DESPD_ITEM")
            dt.Columns.Add("ARTICULO_CODIGO")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("SERIE_NRO")
            dt.Columns.Add("PLACA_NRO")
            dt.Columns.Add("Recibido")
            dt.Columns.Add("SERIE_NUMERAR")
            dt.Columns.Add("Ruc")
            dt.Columns.Add("Oficina")
            dt.Columns.Add("Ubicact_tipo")
            dt.Columns.Add("Ubicact_codigo")
            Dim psCantReg As Double = 0

            For Each row As GridViewRow In GvSalidaBienes.Rows
                drT = dt.NewRow()
                psCantReg = psCantReg + 1
                drT("DESPD_ITEM") = psCantReg
                drT("ARTICULO_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("Recibido") = "X"
                drT("SERIE_NUMERAR") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("Ruc") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("Oficina") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("Ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("Ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                dt.Rows.Add(drT)
            Next

            dt1 = obj.BuscarXSerie_Placa(Session("Ruta_Emp"), Session("CodEmpresa"), 0, pdPlacaNro, psSerieNro, 0)
            Dim numerar As Double = 0
            If dt1.Rows.Count > 0 Then
                For Each drow As DataRow In dt1.Rows
                    drT = dt.NewRow()
                    psCantReg = psCantReg + 1
                    drT("DESPD_ITEM") = psCantReg
                    drT("ARTICULO_CODIGO") = Nu(drow("ARTICULO_CODIGO"))
                    drT("ART_CODEQUIVA") = Nu(drow("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drow("ART_DESCRIPCION"))
                    drT("SERIE_NRO") = Nu(drow("SERIE_NRO"))
                    drT("PLACA_NRO") = Nu(drow("PLACA_NRO"))
                    drT("Recibido") = Nu(drow("Recibido"))
                    drT("SERIE_NUMERAR") = Nu(drow("SERIE_NUMERAR"))
                    drT("Ruc") = Nu(drow("Ruc"))
                    drT("Oficina") = Nu(drow("Oficina"))
                    drT("Ubicact_tipo") = Nu(drow("Ubicact_tipo"))
                    drT("Ubicact_codigo") = Nu(drow("Ubicact_codigo"))
                    dt.Rows.Add(drT)
                Next
            End If

            GvSalidaBienes.DataSource = dt
            GvSalidaBienes.DataBind()
            If GvSalidaBienes.Rows.Count > 0 Then
                GvSalidaBienes.Visible = True
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Sub TxtNroPlaca_TextChanged(sender As Object, e As EventArgs) Handles TxtNroPlaca.TextChanged
        Dim pdPlaca As Double = 0
        If TxtNroPlaca.Text <> "" Then
            pdPlaca = Nz(TxtNroPlaca.Text)
            Call Recibir_Bien("", pdPlaca)
            TxtNroPlaca.Text = ""
        End If
    End Sub

    Private Sub TxtNroSerie_TextChanged(sender As Object, e As EventArgs) Handles TxtNroSerie.TextChanged
        If TxtNroSerie.Text <> "" Then
            Call Recibir_Bien(TxtNroSerie.Text, 0)
            TxtNroSerie.Text = ""
        End If
    End Sub

    Private Sub BtnCargarPlacas_Click(sender As Object, e As EventArgs) Handles BtnCargarPlacas.Click
        If FileUpload1.HasFile Then
            ' Obtiene el nombre del archivo y su extensión
            Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim fileExtension As String = Path.GetExtension(fileName)

            ' Verifica que el archivo sea un archivo de texto
            If fileExtension.ToLower() = ".txt" Then
                ' Lee el contenido del archivo de texto
                Dim fileContent As String = ""
                Using reader As New StreamReader(FileUpload1.PostedFile.InputStream)
                    While Not reader.EndOfStream
                        ' Lee cada línea del archivo y agrega un salto de línea
                        fileContent = reader.ReadLine()
                        ' Actualiza el contenido del UpdatePanel
                        Call Recibir_Bien("", CDbl(Val(fileContent)))
                        Session("Fin") = "Si"
                    End While
                End Using
                '' Muestra el contenido en la página
            Else
                Session("Fin") = ""
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El archivo seleccionado no es un archivo de texto válido.');", True)
            End If
        Else
            Session("Fin") = ""
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un archivo.');", True)
        End If
    End Sub

    Private Sub BtnCargarSeries_Click(sender As Object, e As EventArgs) Handles BtnCargarSeries.Click
        If FileUpload1.HasFile Then
            ' Obtiene el nombre del archivo y su extensión
            Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim fileExtension As String = Path.GetExtension(fileName)

            ' Verifica que el archivo sea un archivo de texto
            If fileExtension.ToLower() = ".txt" Then
                ' Lee el contenido del archivo de texto
                Dim fileContent As String = ""
                Using reader As New StreamReader(FileUpload1.PostedFile.InputStream)
                    While Not reader.EndOfStream
                        ' Lee cada línea del archivo y agrega un salto de línea
                        fileContent = reader.ReadLine()
                        ' Actualiza el contenido del UpdatePanel
                        Call Recibir_Bien(fileContent, 0)
                        Session("Fin") = "Si"
                    End While
                End Using
                '' Muestra el contenido en la página
            Else
                Session("Fin") = ""
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El archivo seleccionado no es un archivo de texto válido.');", True)
            End If
        Else
            Session("Fin") = ""
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un archivo.');", True)
        End If
    End Sub
End Class

