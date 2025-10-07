Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Inventario_Almacen_Salida
    Inherits System.Web.UI.Page
    Dim DiasFuturoRegistrarFecha As Int16
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
            Session("TicketNro") = NroTicket
            lblCodigo.Text = ""
            lblFecha.Text = FormatoFecha(FechaActual)
            lblHora.Text = FormatoHora(HoraActual)
            If txtFecha.Text.Trim = "" Then txtFecha.Text = lblFecha.Text

            If txtHora.Text.Trim = "" Then txtHora.Text = lblHora.Text
            txtDesCodExterno.Text = ""
            txtOrigCodExt.Text = ""
            txtDesDescrip.Text = ""
            txtOrigDescrip.Text = ""
            txtPerEnvia.Text = ""
            lblCodOrigen.Text = ""
            lblCodDestino.Text = ""
            txtObs.Text = ""
            lblUsuario.Text = User.Identity.Name
            Call LLenar_TipoaArticulo()
            Call Carga_Motivos()
            lblFechaDevol.Visible = False
            txtFechaDevol.Visible = False
            Call FlexBusBlanco(GvBusEquipo.ID)
            Call FlexBusBlanco(_DetalleEq.ID)
            Call FlexBusBlanco(GvBusAcc.ID)
            Call FlexBusBlanco(_DetalleAc.ID)
            Session("ArrayEq") = String.Empty
            Session("ArrayAc") = String.Empty
            Session("CountArrayEq") = "-1"
            Session("CountArrayAc") = "-1"

            Session("TipoDestino") = "Almacen"

            Session("OrigenDescrip") = String.Empty
            Session("OrigenCodExt") = String.Empty
            Session("DestinoDescrip") = String.Empty
            Session("DestinoCodExt") = String.Empty
        Else
            txtOrigDescrip.Text = Session("OrigenDescrip")
            txtOrigCodExt.Text = Session("OrigenCodExt")

            txtDesDescrip.Text = Session("DestinoDescrip")
            txtDesCodExterno.Text = Session("DestinoCodExt")
        End If
    End Sub
    Private Sub LLenar_TipoaArticulo()
        Dim dt As New DataTable
        Dim objCat As New Cls_Catalogo
        dt = Nothing
        dt = objCat.Lista_Tipo(Session("Ruta_Emp"))
        DdlTipoBA.DataSource = dt
        DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipoBA.DataBind()
        DdlTipoBA.Items.Add("< Seleccionar >")
        DdlTipoBA.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub Carga_Motivos()
        Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)

        'Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        cboMotivo.Items.Clear()
        lblFechaDevol.Visible = False
        txtFechaDevol.Visible = False
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = " SELECT DISTINCT MAINSA_MOTIVO_TRASLADO, (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC217' AND ELEMEN_CODIGO = MAINSA_MOTIVO_TRASLADO) AS MOTIVO_TRASLADO" _
                               & " FROM TBINV_MATRIZ_INGRESOSALIDA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (MAINSA_TIPO_MOVIMIENTO = 'S') AND (MAINSA_UBICACION1 = '1') AND " _
                               & " (MAINSA_UBICACION2 = '" & IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")) & "') ORDER BY MOTIVO_TRASLADO"
            Rs = cmdSql.ExecuteReader()
            cboMotivo.DataSource = Rs
            cboMotivo.DataTextField = "MOTIVO_TRASLADO"
            cboMotivo.DataValueField = "MAINSA_MOTIVO_TRASLADO"
            cboMotivo.DataBind()

            cboMotivo.Items.Add("< Seleccionar >")
            cboMotivo.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub FlexBusBlanco(ByVal nFlex As String)
        'Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        Dim da As SqlDataAdapter
        Dim ds As New DataSet
        Dim Sql As String = ""
        Try
            Select Case nFlex
                Case "GvBusEquipo"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS SERIE_NRO, '' AS PLACA_NRO, '' AS SERIE_NUMERAR, '' AS REEN_NUMERO, '' AS AVERIA "
                Case "_DetalleEq"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS SERIE_NRO, '' AS PLACA_NRO, '' AS SERIE_NUMERAR, '' AS FUNCION, '' AS COD_FUNCION, '' AS REEN_NUMERO, '' AS AVERIA, '' AS FALLA_AVERIA, '' AS COD_FALLA, '' AS DET_AVERIA "
                Case "GvBusAcc"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS STOCK_ACTUAL, '' AS NRO_REEN_SIN_SERIE"
                Case "_DetalleAc"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS STOCK_ACTUAL, '' AS CANT_SALIDA, '' AS NRO_REEN_SIN_SERIE"
            End Select
            da = New SqlDataAdapter(Sql, Cn)
            da.Fill(ds, "TBINV_ARTICULOS_SERIES")
            Select Case nFlex
                Case "GvBusEquipo"
                    GvBusEquipo.DataSource = ""
                    GvBusEquipo.DataBind()
                    lblCountBusEq.Text = "Registros: 0"
                Case "_DetalleEq"
                    _DetalleEq.DataSource = ""
                    _DetalleEq.DataBind()
                    If cboMotivo.Text <> "" Then
                        If cboMotivo.SelectedValue.ToString = "13" Or cboMotivo.SelectedValue.ToString = "17" Then
                            _DetalleEq.Columns(11).Visible = True
                            _DetalleEq.Columns(13).Visible = True
                        Else
                            _DetalleEq.Columns(11).Visible = False
                            _DetalleEq.Columns(13).Visible = False
                        End If
                    Else
                        _DetalleEq.Columns(11).Visible = False
                        _DetalleEq.Columns(13).Visible = False
                    End If
                    _DetalleAc.DataBind()
                Case "GvBusAcc"
                    GvBusAcc.DataSource = ""
                    GvBusAcc.DataBind()
                    lblCountBusAc.Text = "Registros: 0"
                Case "_DetalleAc"
                    _DetalleAc.DataSource = ""
                    _DetalleAc.DataBind()
            End Select
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub _BuscarEq_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _BuscarEq.Click
        Dim Sql As String = ""
        Dim Sql2 As String = ""
        lblCountBusEq.Text = "Registros 0"

        If txtCodigoArt.Text.Trim <> "" Then
            If IsNumeric(txtCodigoArt.Text.Trim) Then
                txtCodigoArt.Text = Format(CLng(txtCodigoArt.Text), "00000000")
            End If
        End If

        Try
            Select Case cboMotivo.SelectedValue
                Case "3" 'DEVOLUCION POR PRESTAMO
                    'listar los prestados q faltan devolver
                    Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION, S.SERIE_NRO, S.PLACA_NRO,LTRIM(RTRIM(STR(S.SERIE_NUMERAR))) AS SERIE_NUMERAR, " _
                        & " '' AS REEN_NUMERO, '' AS AVERIA " _
                        & " FROM TBINV_PRESTAMO C INNER JOIN TBINV_PRESTAMO_DETALLE D ON C.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND C.PRESTA_CODIGO = D.PRESTA_CODIGO INNER JOIN " _
                        & " TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO ON C.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND  D.SERIE_NUMERAR = S.SERIE_NUMERAR " _
                        & " AND C.CECOSE_CODIGO_DESTINO = S.UBICACT_CODIGO AND  C.PRESTA_TIPODESTINO = S.UBICACT_TIPO " _
                        & " WHERE (S.UBICACT_CODIGO = " & lblCodOrigen.Text & ") AND (S.SERIE_SYS_EST = '0') AND (S.UBICACT_TIPO = '1') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND " _
                        & " (D.PREDET_ESTADO_PRESTAMO = '1') AND (C.PRESTA_TIPO_MOVIMIENTO = 'S')"
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '1') AND (C.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    ElseIf RBCentroC.Checked = True Then
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '2') AND (C.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                Case "12" 'DEVOLUCION REEMPLAZO POR CAMBIO
                    'LISTAR LOS REEMPLAZOS QUE FALTAN DEVOLVER TIPO 1
                    Sql = " SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION,S.SERIE_NRO,S.PLACA_NRO, LTRIM(RTRIM(STR(R.SERIE_NUMERAR_REEMPLAZANTE))) AS SERIE_NUMERAR," _
                        & " LTRIM(RTRIM(STR(REEM_NRO))) AS REEN_NUMERO, '' AS AVERIA, " _
                        & " R.REEM_TIPO_DESTINO, R.REEM_CODIGO_DESTINO, R.REEM_TIPO_ORIGEN, R.REEM_CODIGO_ORIGEN, R.REEM_TIPO" _
                        & " FROM dbo.TBINV_REEMPLAZOS R INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON R.SERIE_NUMERAR_REEMPLAZANTE = S.SERIE_NUMERAR INNER JOIN" _
                        & " dbo.TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO " _
                        & " WHERE (R.REEM_TIPO_DESTINO='1')AND (R.REEM_CODIGO_DESTINO='" & lblCodOrigen.Text & "') AND (R.REEM_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0')" _
                        & " AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (A.ART_SYS_EST = '0') AND (R.REEM_ESTADO_1 = '2') AND (R.REEM_ESTADO_2 = '1') AND (R.REEM_TIPO='1') AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '1') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    ElseIf RBCentroC.Checked = True Then
                        Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '2') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                Case "13" 'DEVOLUCION POR AVERIA
                    'LISTAR LOS REEMPLAZOS A DEVOLVER DE TIPO 2
                    Sql = " SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO,ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION, S.SERIE_NRO, S.PLACA_NRO, LTRIM(RTRIM(STR(R.SERIE_NUMERAR_REEMPLAZANTE))) AS SERIE_NUMERAR," _
                        & " LTRIM(RTRIM(STR(R.REEM_NRO))) AS REEN_NUMERO, R.REEM_TIPO,LTRIM(RTRIM(STR(R.AVERIA_NRO))) AS AVERIA," _
                        & " R.REEM_TIPO_DESTINO, R.REEM_CODIGO_DESTINO, R.REEM_TIPO_ORIGEN, R.REEM_CODIGO_ORIGEN " _
                        & " FROM dbo.TBINV_ARTICULOS A INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON A.ART_CODIGO = S.ARTICULO_CODIGO INNER JOIN " _
                        & " dbo.TBINV_REEMPLAZOS R ON S.SERIE_NUMERAR = R.SERIE_NUMERAR_REEMPLAZADO " _
                        & " WHERE (R.REEM_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (A.ART_SYS_EST = '0') AND " _
                        & " (R.REEM_ESTADO_1 = '2') AND (R.REEM_ESTADO_2 = '1') AND (R.REEM_TIPO = '2') AND (R.REEM_TIPO_DESTINO='1') AND (R.REEM_CODIGO_DESTINO='" & lblCodOrigen.Text & "') AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '1') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    ElseIf RBCentroC.Checked = True Then
                        Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '2') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                Case Else
                    Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS  ARTICULO_CODIGO,ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION,S.SERIE_NRO,S.PLACA_NRO, LTRIM(RTRIM(STR(S.SERIE_NUMERAR))) AS SERIE_NUMERAR, " _
                        & " '' AS REEN_NUMERO, '' AS AVERIA " _
                        & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO" _
                        & " WHERE (S.UBICACT_CODIGO =" & lblCodOrigen.Text & ") AND (S.SERIE_SYS_EST = '0') AND (S.UBICACT_TIPO = '1') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') "
                    'mostrar q no este devolucion
                    Sql2 = " AND ISNULL((SELECT Y.SERIE_NUMERAR FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (Y.PREDET_ESTADO_PRESTAMO = '1') AND (X.PRESTA_TIPO_MOVIMIENTO = 'S') "
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPOORIGEN = '1') AND (X.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    ElseIf RBCentroC.Checked = True Then
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPOORIGEN = '2') AND (X.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    End If
                    Sql2 = Sql2 & " AND Y.SERIE_NUMERAR = S.SERIE_NUMERAR),'') = ''"
                    Sql = Sql & Sql2
                    'mostrar no ESTEN  PRESTADOS
                    Sql2 = " AND ISNULL((SELECT Y.SERIE_NUMERAR FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (Y.PREDET_ESTADO_PRESTAMO = '1') AND (X.PRESTA_TIPO_MOVIMIENTO = 'S') "
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    ElseIf RBCentroC.Checked = True Then
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    End If
                    Sql2 = Sql2 & " AND Y.SERIE_NUMERAR = S.SERIE_NUMERAR),'') = ''"
                    Sql = Sql & Sql2
                    'MOSTRAR LO QUE NO ESTAN DEVUELTO POR REEM POR CAMBIO
                    Sql2 = " AND ((SELECT R.SERIE_NUMERAR_REEMPLAZANTE FROM TBINV_REEMPLAZOS R WHERE R.SERIE_NUMERAR_REEMPLAZANTE = S.SERIE_NUMERAR AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND R.REEM_ESTADO_1 = '2' AND R.REEM_ESTADO_2 = '1') IS NULL) "
                    Sql = Sql & Sql2
                    'MOSTRAR LO QUE NO ESTAN AVERIADOS
                    Sql2 = " AND ((SELECT AV.AVERIA_SERIE_NUMERAR FROM TBINV_AVERIA AV WHERE AV.AVERIA_SERIE_NUMERAR = S.SERIE_NUMERAR AND AV.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND AV.AVERIA_ESTADO_1 = '0' AND AV.AVERIA_ESTADO_2 = '1') IS NULL)"
                    Sql = Sql & Sql2
            End Select
            Sql = Sql & " AND RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) LIKE @CodArticulo"
            Sql = Sql & " AND ART_DESCRIPCION LIKE '%' + @NomArticulo"
            Sql = Sql & " AND SERIE_NRO LIKE @SerieArticulo"
            Sql = Sql & " AND LTRIM(RTRIM(STR(ISNULL(PLACA_NRO,'')))) LIKE @Placa"
            Sql = Sql & " ORDER BY A.ART_DESCRIPCION, S.SERIE_NRO"

            Dim ds As DataSet
            'Dim Cn As SqlConnection
            Dim da As SqlDataAdapter

            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            Dim Cn As New SqlConnection(psConexion)
            'Cn = New SqlConnection(Session("Ruta_Emp"))
            da = New SqlDataAdapter(Sql, Cn)

            da.SelectCommand.Parameters.Add(New SqlParameter("@CodArticulo", SqlDbType.VarChar, 8))
            da.SelectCommand.Parameters("@CodArticulo").Value = txtCodigoArt.Text.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@NomArticulo", SqlDbType.VarChar, 80))
            da.SelectCommand.Parameters("@NomArticulo").Value = "%" & txtNomArt.Text.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@SerieArticulo", SqlDbType.VarChar, 30))
            da.SelectCommand.Parameters("@SerieArticulo").Value = txtSerieArt.Text.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@Placa", SqlDbType.VarChar, 30))
            da.SelectCommand.Parameters("@Placa").Value = txtPlaca.Text.Trim & "%"

            ds = New DataSet()
            da.Fill(ds, "TBCLIENTE")

            _BusEq.DataSource = ds.Tables(0).DefaultView
            _BusEq.DataBind()

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
        If _BusEq.Rows.Count = 0 Then
            'Call FlexBusBlanco(_BusEq.ID)
            lblCountBusEq.Text = "Registros 0"
        Else
            lblCountBusEq.Text = "Registros " & _BusEq.Rows.Count.ToString
        End If
    End Sub

    Protected Sub _BusEq_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles _BusEq.RowCommand
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim ii As Integer = 0
        Dim Existe As Boolean = False
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim arrSelec(,) As String
        lblCountBusEq.Text = ""
        'Session("ArrayEq") = String.Empty
        'Session("CountArrayEq") = -1
        If e.CommandName = "AgregarFila" Then
            f = -1
            With _DetalleEq
                i = CLng(Session("CountArrayEq"))
                If i > -1 Then
                    arrSelec = Session("ArrayEq")
                    For ii = 0 To i
                        If arrSelec(4, ii) = _BusEq.Rows(Index).Cells(5).Text.Trim And arrSelec(12, ii) = "" Then
                            lblCountBusEq.Text = "Ya se encuentra en la lista."
                            Exit Sub
                        End If
                    Next
                End If
                f = -1
                Erase arrSelec
                Session("CountArrayEq") = "-1"
                For i = 0 To .Rows.Count - 1
                    f = f + 1
                    ReDim Preserve arrSelec(14, f)
                    arrSelec(0, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ARTICULO_CODIGO
                    arrSelec(1, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ART_DESCRIPCION
                    arrSelec(2, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ART_SKU
                    arrSelec(3, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'SERIE_NRO
                    arrSelec(4, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'PLACA_NRO
                    arrSelec(5, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'SERIE_NUMERAR
                    Dim cFuncion As DropDownList = _DetalleEq.Rows(i).Cells(7).FindControl("cboFuncion")
                    arrSelec(6, i) = cFuncion.SelectedValue.Trim  'COD_FUNCION
                    arrSelec(7, i) = cFuncion.SelectedIndex.ToString  'combo funcion index
                    arrSelec(8, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'REEN_NUMERO
                    arrSelec(9, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'AVERIA
                    Dim cAveria As DropDownList = _DetalleEq.Rows(i).Cells(11).FindControl("cboAveria")
                    arrSelec(10, i) = cAveria.SelectedValue.Trim  'COD_FALLA
                    arrSelec(11, i) = cAveria.SelectedIndex.ToString   'combo averia index
                    Dim tDetAveria As TextBox = _DetalleEq.Rows(i).Cells(13).FindControl("txtDetAveria")
                    arrSelec(12, i) = tDetAveria.Text.Trim  'text averia
                    arrSelec(13, i) = ""
                Next
                Session("CountArrayEq") = f.ToString
                'End If
            End With
            f = f + 1
            ReDim Preserve arrSelec(13, f)
            arrSelec(0, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(_BusEq.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            arrSelec(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(_BusEq.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ARTICULO_CODIGO
            arrSelec(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(_BusEq.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            If _BusEq.Rows(Index).Cells(4).Text.Trim = "&nbsp;" Then
                arrSelec(3, f) = String.Empty
            Else
                arrSelec(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(_BusEq.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            End If
            If _BusEq.Rows(Index).Cells(5).Text.Trim = "&nbsp;" Then
                arrSelec(4, f) = String.Empty
            Else
                arrSelec(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(_BusEq.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            End If

            arrSelec(5, f) = String.Empty
            arrSelec(6, f) = "0"
            arrSelec(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(_BusEq.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            arrSelec(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(_BusEq.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            arrSelec(9, f) = String.Empty
            arrSelec(10, f) = "0"
            arrSelec(11, f) = String.Empty
            arrSelec(12, f) = String.Empty 'n eliminado
            Session("CountArrayEq") = f.ToString

            Dim _dt As New DataTable
            Dim _dr As DataRow
            _dt.Columns.Add("ARTICULO_CODIGO", GetType(String))
            _dt.Columns.Add("ART_DESCRIPCION", GetType(String))
            _dt.Columns.Add("SERIE_NRO", GetType(String))
            _dt.Columns.Add("PLACA_NRO", GetType(String))
            _dt.Columns.Add("SERIE_NUMERAR", GetType(String))
            _dt.Columns.Add("REEN_NUMERO", GetType(String))
            _dt.Columns.Add("AVERIA", GetType(String))
            For ii = 0 To f
                _dr = _dt.NewRow()
                _dr(0) = arrSelec(0, ii).Trim
                _dr(1) = arrSelec(1, ii).Trim
                If arrSelec(2, ii) Is Nothing Then
                    _dr(2) = String.Empty
                Else
                    _dr(2) = arrSelec(2, ii).Trim
                End If
                If arrSelec(3, ii) Is Nothing Then
                    _dr(3) = String.Empty
                Else
                    _dr(3) = arrSelec(3, ii).Trim
                End If
                If arrSelec(4, ii) Is Nothing Then
                    _dr(4) = String.Empty
                Else
                    _dr(4) = arrSelec(4, ii).Trim
                End If
                If arrSelec(7, ii) Is Nothing Then
                    _dr(5) = String.Empty
                Else
                    _dr(5) = arrSelec(7, ii).Trim
                End If
                If arrSelec(8, ii) Is Nothing Then
                    _dr(6) = String.Empty
                Else
                    _dr(6) = arrSelec(8, ii).Trim
                End If
                _dt.Rows.Add(_dr)
            Next
            Session("ArrayEq") = arrSelec
            _DetalleEq.DataSource = New DataView(_dt)
            _DetalleEq.DataBind()

            With _DetalleEq
                For i = 0 To .Rows.Count - 1
                    Dim cFuncion As DropDownList = .Rows(i).Cells(7).FindControl("cboFuncion")
                    Dim cAveria As DropDownList = .Rows(i).Cells(11).FindControl("cboAveria")
                    Dim tDetAveria As TextBox = .Rows(i).Cells(13).FindControl("txtDetAveria")
                    Call LlenaComboItem("TBOPC230", cFuncion)
                    cFuncion.Items.Insert(0, "")
                    Call LlenaComboItem("TBOPC236", cAveria)
                    If arrSelec(7, i).Trim <> "" Then cFuncion.SelectedIndex = arrSelec(7, i).Trim
                    If arrSelec(11, i).Trim <> "" Then cAveria.SelectedIndex = arrSelec(11, i).Trim
                    tDetAveria.Text = arrSelec(12, i).Trim
                Next
            End With
            If lblCountBusEq.Text = "" Then lblCountBusEq.Text = "Registros: " & _BusEq.Rows.Count.ToString
        End If
    End Sub

    Protected Sub _BuscarAc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _BuscarAc.Click
        Dim Sql As String = ""
        Dim Sql2 As String = ""
        lblCountBusAc.Text = "Registros 0"

        If txtCodigoAc.Text.Trim <> "" Then
            If IsNumeric(txtCodigoAc.Text.Trim) Then
                txtCodigoAc.Text = Format(CLng(txtCodigoAc.Text), "00000000")
            End If
        End If
        If lblCodOrigen.Text = "" Then
            lblCountBusAc.Text = "Seleccionar Origen"
            Exit Sub
        End If
        If lblCodDestino.Text = "" Then
            lblCountBusAc.Text = "Seleccionar Destino"
            Exit Sub
        End If
        If cboMotivo.Text = "" Then lblCountBusAc.Text = "Seleccionar Motivo" : cboMotivo.Focus() : Exit Sub
        If cboMotivo.SelectedIndex = -1 Then lblCountBusAc.Text = "Seleccionar Motivo" : cboMotivo.Focus() : Exit Sub

        Try
            Select Case cboMotivo.SelectedValue
                Case 3 'DEVOLUCION POR PRESTAMO
                    'listar los prestados q faltan devolver
                    Sql = " SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_DESCRIPCION, SUM(ISNULL(PREDET_CANT_FALT_DEVOLVER,0)-ISNULL(PREDET_CANT_XDEVOLVER,0)) AS  STOCK_ACTUAL " _
                        & " FROM TBINV_PRESTAMO C INNER JOIN TBINV_PRESTAMO_DETALLE_SINSERIE D ON C.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND C.PRESTA_CODIGO = D.PRESTA_CODIGO " _
                        & " INNER JOIN TBINV_STOCK_SINSERIE_CCOSTO S ON S.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND S.ARTICULO_CODIGO = D.ARTICULO_CODIGO AND S.CECOSE_CODIGO = C.CECOSE_CODIGO_DESTINO INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO AND S.EMPRESA_CODIGO = A.EMPRESA_CODIGO " _
                        & " WHERE  " _
                        & " (D.PREDET_ESTADO_PRESTAMO IN ('1','2','4')) AND (C.PRESTA_TIPO_MOVIMIENTO = 'S') AND (C.PRESTA_TIPODESTINO = '1')"
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '1') AND (C.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    ElseIf RBCentroC.Checked = True Then
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '2') AND (C.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                    Sql = Sql & " GROUP BY A.EMPRESA_CODIGO, C.CECOSE_CODIGO_DESTINO, D.ARTICULO_CODIGO, A.ART_DESCRIPCION HAVING (C.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ") AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') "
                Case 12 'DEVOLUCION REEMPLAZO POR CAMBIO
                    'LISTAR LOS REEMPLAZOS QUE FALTAN DEVOLVER TIPO 1
                    Sql = " SELECT  RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_DESCRIPCION, SUM(ISNULL(RS.REEMSIN_CANT_FALT_DEVOLVER, 0) - ISNULL(RS.REEMSIN_CANT_XDEVOLVER, 0)) AS STOCK_ACTUAL   " _
                        & " FROM TBINV_ARTICULOS A INNER JOIN TBINV_REEMPLAZOS_SINSERIE RS ON A.EMPRESA_CODIGO = RS.EMPRESA_CODIGO AND A.ART_CODIGO = RS.ART_CODIGO INNER JOIN " _
                        & " TBINV_STOCK_ARTICULOS_ALMACEN D ON A.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND " _
                        & " Rs.ART_CODIGO = D.ARTICULO_CODIGO And Rs.REEMSIN_COD_DESTINO = D.ALMACEN_CODIGO And Rs.REEMSIN_TIPO_DESTINO = D.UBICACT_TIPO " _
                        & " WHERE (RS.REEMSIN_ESTADO_2 IN ('1','2','4')) AND (RS.REEMSIN_TIPO_DESTINO = '2') AND (RS.REEMSIN_ESTADO_1 = '1')"
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql = Sql & " AND (RS.REEMSIN_TIPO_ORIGEN = '1') AND (RS.REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ")"
                    ElseIf RBCentroC.Checked = True Then
                        Sql = Sql & " AND (RS.REEMSIN_TIPO_ORIGEN = '2') AND (RS.REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                    Sql = Sql & " GROUP BY A.EMPRESA_CODIGO,RS.REEMSIN_COD_DESTINO,A.ART_DESCRIPCION,D.ARTICULO_CODIGO HAVING (RS.REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ") AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                Case Else
                    Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_DESCRIPCION, ISNULL(D.SAA_STOCK_ACTUAL,0) " _
                        & " - ISNULL(" _
                        & " (SELECT SUM(PREDET_CANT_FALT_DEVOLVER)+SUM(PREDET_CANT_XDEVOLVER) FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE_SINSERIE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (X.PRESTA_TIPO_MOVIMIENTO = 'S' AND Y.ARTICULO_CODIGO = D.ARTICULO_CODIGO)" ' AND (Y.PREDET_ESTADO_PRESTAMO = '1')"
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql = Sql & " AND (X.PRESTA_TIPOORIGEN = '1') AND (X.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    ElseIf RBCentroC.Checked = True Then  'Seccion CC
                        Sql = Sql & " AND (X.PRESTA_TIPOORIGEN = '2') AND (X.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    End If
                    Sql = Sql & "),0) - ISNULL(" _
                        & " (SELECT SUM(REEMSIN_CANT_FALT_DEVOLVER)+SUM(REEMSIN_CANT_XDEVOLVER) FROM TBINV_REEMPLAZOS_SINSERIE RS WHERE D.ARTICULO_CODIGO = RS.ART_CODIGO "
                    If RBAlmacen.Checked = True Then 'almacen
                        Sql = Sql & "  AND (REEMSIN_TIPO_ORIGEN = '1') AND (REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ") AND (REEMSIN_TIPO_DESTINO = '1') AND (REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ")"
                    ElseIf RBCentroC.Checked = True Then  'Seccion CC
                        Sql = Sql & " AND (REEMSIN_TIPO_ORIGEN = '2') AND (REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ") AND (REEMSIN_TIPO_DESTINO = '1') AND (REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ")"
                    End If
                    Sql = Sql & " ),0) AS STOCK_ACTUAL " _
                        & " FROM TBINV_STOCK_ARTICULOS_ALMACEN D INNER JOIN TBINV_ARTICULOS A ON D.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND D.ARTICULO_CODIGO = A.ART_CODIGO " _
                        & " WHERE (D.ALMACEN_CODIGO = " & lblCodOrigen.Text & ") AND UBICACT_TIPO = '1' AND (D.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (D.SAA_SYS_EST = '0') AND (A.ART_SYS_EST = '0')  AND (A.ART_TIPO IN( 87,127,90))  " 'TIPO ACCESORIO  AND (A.ART_TIPO = 87)
                    'al stock actual le restamos los articulos q se tienen q devolver y/o falta devolver
            End Select
            Sql = Sql & " AND RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) LIKE @CodArticulo "
            Sql = Sql & " AND A.ART_DESCRIPCION LIKE  @NomArticulo"
            Sql = Sql & " ORDER BY A.ART_DESCRIPCION"

            Dim ds As DataSet
            'Dim Cn As SqlConnection
            Dim da As SqlDataAdapter

            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            Dim Cn As New SqlConnection(psConexion)
            'Cn = New SqlConnection(Session("Ruta_Emp"))
            da = New SqlDataAdapter(Sql, Cn)

            da.SelectCommand.Parameters.Add(New SqlParameter("@CodArticulo", SqlDbType.VarChar, 8))
            da.SelectCommand.Parameters("@CodArticulo").Value = txtCodigoAc.Text.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@NomArticulo", SqlDbType.VarChar, 80))
            da.SelectCommand.Parameters("@NomArticulo").Value = "%" & txtNomAc.Text.Trim & "%"

            ds = New DataSet()
            da.Fill(ds, "TBINV_ARTICULOS")

            _BusAc.DataSource = ds.Tables(0).DefaultView
            _BusAc.SelectedIndex = -1
            _BusAc.DataBind()

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
        If _BusAc.Rows.Count = 0 Then
            'Call FlexBusBlanco(_BusAc.ID)
            lblCountBusAc.Text = "Registros: 0"
        Else
            lblCountBusAc.Text = "Registros: " & _BusAc.Rows.Count.ToString
        End If
    End Sub

    Protected Sub _BusAc_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles _BusAc.RowCommand
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim ii As Integer = 0
        Dim Existe As Boolean = False
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim arrSelec(,) As String
        If e.CommandName = "AgregarFila" Then
            f = -1
            With _DetalleAc
                If CLng(_BusAc.Rows(index).Cells(3).Text) <= 0 Then
                    lblCountBusAc.Text = "No hay Stock disponible para el Accesorio."
                    Exit Sub
                End If
                i = CLng(Session("CountArrayAc"))
                If i > -1 Then
                    arrSelec = Session("ArrayAc")
                    For ii = 0 To i
                        If arrSelec(0, ii) = _BusAc.Rows(index).Cells(1).Text.Trim And arrSelec(5, ii) = "" Then
                            lblCountBusAc.Text = "Ya se encuentra en la lista."
                            Exit Sub
                        End If
                    Next
                End If
                f = -1
                Erase arrSelec
                Session("CountArrayAc") = "-1"
                For i = 0 To .Rows.Count - 1
                    f = f + 1
                    ReDim Preserve arrSelec(6, f)
                    arrSelec(0, i) = .Rows(i).Cells(1).Text.Trim 'ARTICULO_CODIGO
                    arrSelec(1, i) = .Rows(i).Cells(2).Text.Trim 'ART_DESCRIPCION
                    arrSelec(2, i) = .Rows(i).Cells(3).Text.Trim 'STOCK ACTUAL
                    Dim tCantSal As TextBox = _DetalleAc.Rows(i).Cells(4).FindControl("txtCantSal")
                    arrSelec(3, i) = tCantSal.Text.Trim  'text averia
                    arrSelec(4, i) = String.Empty
                    arrSelec(5, i) = String.Empty
                Next
                Session("CountArrayAc") = f.ToString
                'End If
            End With
            f = f + 1
            ReDim Preserve arrSelec(6, f)
            arrSelec(0, f) = _BusAc.Rows(index).Cells(1).Text.Trim
            arrSelec(1, f) = _BusAc.Rows(index).Cells(2).Text.Trim
            arrSelec(2, f) = _BusAc.Rows(index).Cells(3).Text.Trim
            arrSelec(3, f) = "0"
            arrSelec(4, f) = String.Empty
            arrSelec(5, f) = String.Empty 'n eliminado
            Session("CountArrayAc") = f.ToString

            Dim _dt As New DataTable
            Dim _dr As DataRow
            _dt.Columns.Add("ARTICULO_CODIGO", GetType(String))
            _dt.Columns.Add("ART_DESCRIPCION", GetType(String))
            _dt.Columns.Add("STOCK_ACTUAL", GetType(String))
            For ii = 0 To f
                _dr = _dt.NewRow()
                _dr(0) = arrSelec(0, ii)
                _dr(1) = arrSelec(1, ii)
                _dr(2) = arrSelec(2, ii)
                _dt.Rows.Add(_dr)
            Next
            Session("ArrayAc") = arrSelec
            _DetalleAc.DataSource = New DataView(_dt)
            _DetalleAc.DataBind()

            With _DetalleAc
                For i = 0 To .Rows.Count - 1
                    Dim tCantSal As TextBox = .Rows(i).Cells(3).FindControl("txtCantSal")
                    tCantSal.Text = arrSelec(3, i).Trim
                    If tCantSal.Text.Trim = "" Then tCantSal.Text = "0"
                Next
            End With
            If lblCountBusAc.Text = "" Then lblCountBusAc.Text = "Registros: " & _BusAc.Rows.Count.ToString
        End If
    End Sub

    Private Sub Carga_Funcion(ByVal Cbo As DropDownList)
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        'Dim Cn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConexion_GE"))
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        Cbo.Items.Clear()
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT ELEMEN_CODIGO,ELEMEN_VALOR FROM TBCELEMEN WHERE ELEMEN_TABLA='TBOPC230' ORDER BY ELEMEN_VALOR"
            Rs = cmdSql.ExecuteReader()
            Cbo.DataSource = Rs
            Cbo.DataTextField = "ELEMEN_VALOR"
            Cbo.DataValueField = "ELEMEN_CODIGO"
            Cbo.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub

    Protected Sub cboMotivo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboMotivo.SelectedIndexChanged
        lblFechaDevol.Visible = False
        txtFechaDevol.Visible = False
        Select Case cboMotivo.SelectedValue.ToString
            Case "1"
                lblFechaDevol.Visible = True
                txtFechaDevol.Visible = True
                txtFechaDevol.Text = FormatoFecha(FechaActual())
            Case 2
            Case 3
            Case 4
            Case 5
            Case 6
            Case 7
            Case 8
            Case 9
            Case 10
            Case 11
            Case 12
            Case 13
        End Select
        Call FlexBusBlanco(GvBusEquipo.ID)
        Call FlexBusBlanco(_DetalleEq.ID)
        Call FlexBusBlanco(GvBusAcc.ID)
        Call FlexBusBlanco(_DetalleAc.ID)
        Session("ArrayEq") = String.Empty
        Session("ArrayAc") = String.Empty
        Session("CountArrayEq") = "-1"
        Session("CountArrayAc") = "-1"
    End Sub

    Protected Sub _DetalleEq_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles _DetalleEq.RowCommand
        Dim arrSelec(,) As String
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "QuitarFila" Then
            f = -1
            Erase arrSelec
            Session("CountArrayEq") = "-1"
            With _DetalleEq
                For i = 0 To .Rows.Count - 1
                    If i <> index Then
                        f = f + 1
                        ReDim Preserve arrSelec(13, f)
                        arrSelec(0, f) = .Rows(i).Cells(1).Text.Trim 'ARTICULO_CODIGO
                        arrSelec(1, f) = .Rows(i).Cells(2).Text.Trim 'ART_DESCRIPCION
                        arrSelec(2, f) = .Rows(i).Cells(3).Text.Trim 'SERIE_NRO
                        arrSelec(3, f) = .Rows(i).Cells(4).Text.Trim 'PLACA_NRO
                        arrSelec(4, f) = .Rows(i).Cells(5).Text.Trim 'SERIE_NUMERAR
                        Dim cFuncion As DropDownList = _DetalleEq.Rows(i).Cells(6).FindControl("cboFuncion")
                        arrSelec(5, f) = cFuncion.SelectedValue.Trim  'COD_FUNCION
                        arrSelec(6, f) = cFuncion.SelectedIndex.ToString  'combo funcion index
                        arrSelec(7, f) = .Rows(i).Cells(8).Text.Trim 'REEN_NUMERO
                        arrSelec(8, f) = .Rows(i).Cells(9).Text.Trim 'AVERIA
                        Dim cAveria As DropDownList = _DetalleEq.Rows(i).Cells(10).FindControl("cboAveria")
                        arrSelec(9, f) = cAveria.SelectedValue.Trim  'COD_FALLA
                        arrSelec(10, f) = cAveria.SelectedIndex.ToString   'combo averia index
                        Dim tDetAveria As TextBox = _DetalleEq.Rows(i).Cells(12).FindControl("txtDetAveria")
                        arrSelec(11, f) = tDetAveria.Text.Trim  'text averia
                        arrSelec(12, f) = String.Empty
                    End If
                Next
            End With
            Session("CountArrayEq") = f.ToString

            Dim _dt As New DataTable
            Dim _dr As DataRow
            _dt.Columns.Add("ARTICULO_CODIGO", GetType(String))
            _dt.Columns.Add("ART_DESCRIPCION", GetType(String))
            _dt.Columns.Add("SERIE_NRO", GetType(String))
            _dt.Columns.Add("PLACA_NRO", GetType(String))
            _dt.Columns.Add("SERIE_NUMERAR", GetType(String))
            _dt.Columns.Add("REEN_NUMERO", GetType(String))
            _dt.Columns.Add("AVERIA", GetType(String))
            For i = 0 To f
                _dr = _dt.NewRow()
                _dr(0) = arrSelec(0, i).Trim
                _dr(1) = arrSelec(1, i).Trim
                _dr(2) = arrSelec(2, i).Trim
                _dr(3) = arrSelec(3, i).Trim
                _dr(4) = arrSelec(4, i).Trim
                _dr(5) = arrSelec(7, i).Trim
                _dr(6) = arrSelec(8, i).Trim
                _dt.Rows.Add(_dr)
            Next
            Session("ArrayEq") = arrSelec
            _DetalleEq.DataSource = New DataView(_dt)
            _DetalleEq.DataBind()

            With _DetalleEq
                For i = 0 To .Rows.Count - 1
                    Dim cFuncion As DropDownList = .Rows(i).Cells(6).FindControl("cboFuncion")
                    Dim cAveria As DropDownList = .Rows(i).Cells(10).FindControl("cboAveria")
                    Dim tDetAveria As TextBox = .Rows(i).Cells(12).FindControl("txtDetAveria")
                    Call LlenaComboItem("TBOPC230", cFuncion)
                    cFuncion.Items.Insert(0, "")
                    Call LlenaComboItem("TBOPC236", cAveria)
                    If arrSelec(6, i).Trim <> "" Then cFuncion.SelectedIndex = arrSelec(6, i).Trim
                    If arrSelec(10, i).Trim <> "" Then cAveria.SelectedIndex = arrSelec(10, i).Trim
                    tDetAveria.Text = arrSelec(11, i).Trim
                Next
            End With
        End If
    End Sub

    Protected Sub _DetalleAc_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles _DetalleAc.RowCommand
        Dim arrSelec(,) As String
        Dim i As Integer = 0
        'Dim ii As Integer = 0
        Dim f As Integer = 0
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "QuitarFila" Then
            With _DetalleAc
                f = -1
                Erase arrSelec
                Session("CountArrayAc") = "-1"
                For i = 0 To .Rows.Count - 1
                    If i <> index Then
                        f = f + 1
                        ReDim Preserve arrSelec(6, f)
                        arrSelec(0, i) = .Rows(i).Cells(1).Text.Trim 'ARTICULO_CODIGO
                        arrSelec(1, i) = .Rows(i).Cells(2).Text.Trim 'ART_DESCRIPCION
                        arrSelec(2, i) = .Rows(i).Cells(3).Text.Trim 'STOCK ACTUAL
                        Dim tCantSal As TextBox = _DetalleAc.Rows(i).Cells(4).FindControl("txtCantSal")
                        arrSelec(3, i) = tCantSal.Text.Trim  'text averia
                        arrSelec(4, i) = String.Empty
                        arrSelec(5, i) = String.Empty
                    End If
                Next
            End With
            Session("CountArrayAc") = f.ToString

            Dim _dt As New DataTable
            Dim _dr As DataRow
            _dt.Columns.Add("ARTICULO_CODIGO", GetType(String))
            _dt.Columns.Add("ART_DESCRIPCION", GetType(String))
            _dt.Columns.Add("STOCK_ACTUAL", GetType(String))
            For i = 0 To f
                _dr = _dt.NewRow()
                _dr(0) = arrSelec(0, i)
                _dr(1) = arrSelec(1, i)
                _dr(2) = arrSelec(2, i)
                _dt.Rows.Add(_dr)
            Next
            Session("ArrayAc") = arrSelec
            _DetalleAc.DataSource = New DataView(_dt)
            _DetalleAc.DataBind()

            With _DetalleAc
                For i = 0 To .Rows.Count - 1
                    Dim tCantSal As TextBox = .Rows(i).Cells(3).FindControl("txtCantSal")
                    tCantSal.Text = arrSelec(3, i).Trim
                    If tCantSal.Text.Trim = "" Then tCantSal.Text = "0"
                Next
            End With
        End If
    End Sub

    Protected Sub _Grabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Grabar.Click
        lblError.Text = ""
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        'Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim Cn4 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim Rs3 As SqlDataReader

        Dim FechaSal As String = Right(txtFecha.Text.Trim, 4) + Mid(txtFecha.Text.Trim, 4, 2) + Left(txtFecha.Text.Trim, 2)
        Dim HoraSal As String = Left(txtHora.Text.Trim, 2) + Mid(txtHora.Text.Trim, 4, 2)
        Dim FechaDevol As String = Right(txtFechaDevol.Text.Trim, 4) + Mid(txtFechaDevol.Text.Trim, 4, 2) + Left(txtFechaDevol.Text.Trim, 2)

        Dim CanEnvEq As Integer = _DetalleEq.Rows.Count
        Dim CanEnvAc As Integer = _DetalleAc.Rows.Count

        Dim FechaServer As String = FechaActual()
        Dim HoraServer As String = HoraActual()

        Dim Estado As String = "2"
        Dim SysEst As String = "0"
        Dim Motivo As String = cboMotivo.SelectedValue

        Dim DesCodAlmacen As String = "NULL"
        Dim DesCodSeccion As String = "NULL"
        Dim DesCodProveedor As String = "NULL"
        Dim DesCodEquipo As String = "NULL"
        Dim DesCodCliente As String = "NULL"
        Dim DesCodPersona As String = "NULL"

        Dim TotArt As Long
        Dim i As Integer = 0
        Dim Item As Integer = 0
        Dim Stock As Double = 0

        Dim CodArticulo As String = ""
        Dim NroMovimiento As String = ""

        Dim CodReemplazo As String = ""
        Dim CodDevolucion As String = ""
        Dim CodMantenimiento As String = ""
        Dim CodAveria As String = ""

        lblMensaje.Text = ""

        If lblCodOrigen.Text = "" Then lblMensaje.Text = " <br> - Seleccionar Origen."
        If lblCodDestino.Text = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Seleccionar Destino."
        If RBAlmacen.Checked = True Then
            If lblCodDestino.Text <> "" And lblCodOrigen.Text = lblCodDestino.Text Then lblMensaje.Text = lblMensaje.Text & " <br> - Origen y Destino no pueden ser los mismos."
        End If

        If FechaSal = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Ingresar Fecha Salida"
        If HoraSal = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Ingresar Hora Salida"
        If Not IsDate(txtFecha.Text.Trim) Then lblMensaje.Text = lblMensaje.Text & " <br> - Fecha no válida."
        If Not IsDate(txtHora.Text.Trim) Then lblMensaje.Text = lblMensaje.Text & " <br> - Hora no válida."

        'PUEDE HACER SALIDA A 3 DIAS A FUTURO
        Dim sFecha As Date
        Dim sFecha2 As String
        sFecha = DateAdd("d", DiasFuturoRegistrarFecha, FormatoFecha(FechaActual()))
        sFecha2 = Right(sFecha, 4) + Mid(sFecha, 4, 2) + Left(sFecha, 2)
        If FechaSal > sFecha2 And DiasFuturoRegistrarFecha > 0 Then
            lblMensaje.Text = lblMensaje.Text & " <br> - La Fecha de Salida solo puede ser " & DiasFuturoRegistrarFecha & " dias a futuro."
        End If


        Dim sFecha_Dev As String = ""

        If txtFechaDevol.Visible = True Then
            sFecha_Dev = Right(txtFechaDevol.Text.Trim, 4) + Mid(txtFechaDevol.Text.Trim, 4, 2) + Left(txtFechaDevol.Text.Trim, 2)
            If txtFechaDevol.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Ingresar Fecha Devolución"
            If Not IsDate(txtFechaDevol.Text.Trim) Then lblMensaje.Text = lblMensaje.Text & " <br> - La Fecha Devolución no válida"
            If sFecha_Dev >= FechaSal Then
            Else
                lblMensaje.Text = lblMensaje.Text & " <br> - La fecha a devolver el prestamo debe ser igual o después a la fecha de la salida."
            End If
        End If

        If CanEnvEq = 0 And CanEnvAc = 0 Then lblMensaje.Text = lblMensaje.Text & " <br> - No hay detalle de salida que guardar."

        TotArt = 0
        With _DetalleEq
            If Motivo = "13" Or Motivo = "17" Then
                For i = 0 To .Rows.Count - 1
                    Dim cAveria As DropDownList = .Rows(i).Cells(11).FindControl("cboAveria")
                    Dim tDetAveria As TextBox = .Rows(i).Cells(13).FindControl("txtDetAveria")
                    If cAveria.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Todos los Equipos deben tener Tipo de Falla."
                    If tDetAveria.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Todos los Equipos deben tener Detalle de la Averia."
                    If cAveria.Text.Trim = "" Or tDetAveria.Text.Trim = "" Then Exit For
                Next
            End If
            TotArt = .Rows.Count
        End With

        With _DetalleAc
            For i = 0 To .Rows.Count - 1
                Dim tCantDetAc As TextBox = .Rows(i).Cells(5).FindControl("txtCantSal")
                If Not IsNumeric(tCantDetAc.Text) Then tCantDetAc.Text = "0" Else tCantDetAc.Text = Format(Convert.ToDouble(tCantDetAc.Text), "0")
                If Convert.ToDouble(tCantDetAc.Text) <= 0 Then lblMensaje.Text = lblMensaje.Text & " <br> - Todos los Accesorios deben tener cantidades a salir." : Exit For
                If Convert.ToDouble(tCantDetAc.Text) > Convert.ToDouble(.Rows(i).Cells(4).Text) Then lblMensaje.Text = lblMensaje.Text & " <br> La cantidad a salir debe ser menor o igual a su Stock disponible." : Exit For
                TotArt = TotArt + tCantDetAc.Text
            Next
        End With

        If txtPerEnvia.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Debe ingresar el nombre de la persona que envia la salida"

        If lblMensaje.Text <> "" Then
            lblMensaje.Text = "Existe las siguientes observaciones, favor de corregir:" & lblMensaje.Text
            Exit Sub
        End If

        Try
            Dim ValorSys As String
            ValorSys = FechaServer & HoraServer & lblUsuario.Text
            Dim Tipo As String
            Cn.Open()
            Cn2.Open()
            Cn3.Open()
            Cn4.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal2.Connection = Cn2
            CmdGlobal3.Connection = Cn3
            CmdGlobal4.Connection = Cn4

            CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblCodigo.Text = Format(CLng(Nz(Rs(0))) + 1, "000000")
                End While
            Else
                lblCodigo.Text = "00001"
            End If
            Rs.Close()

            If RBAlmacen.Checked = True Then
                DesCodAlmacen = lblCodDestino.Text
            ElseIf RBCentroC.Checked = True Then
                DesCodSeccion = lblCodDestino.Text
            End If

            Item = 0
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                  & " ALMACEN_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO, DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                  & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_OBSERVACION,DESP_MOTIVO_GRAL,DESP_PERSONA_ENVIA) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & lblCodigo.Text.Trim & ",'" & FechaServer & "','" & HoraServer & "','" & User.Identity.Name & "','" & IIf(RBAlmacen.Checked = True, "1", "2") & "'," _
                                  & " " & DesCodAlmacen & "," & DesCodSeccion & ", '1','0'," & TotArt & ",0,0,0," & lblCodOrigen.Text.Trim & "," _
                                  & " '" & FechaSal & "','" & HoraSal & "','" & txtObs.Text.Trim & "','" & cboMotivo.SelectedValue.Trim & "','" & txtPerEnvia.Text.Trim & "')"
            CmdGlobal.ExecuteNonQuery()
            With _DetalleEq
                For i = 0 To _DetalleEq.Rows.Count - 1
                    Item = Item + 1
                    Dim cFuncion As DropDownList = CType(_DetalleEq.Rows(i).Cells(7).FindControl("cboFuncion"), DropDownList)
                    If cFuncion.SelectedValue.Trim <> "< Seleccionar >" Then
                        CmdGlobal.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,DESPD_FUNCION) " _
                                              & " VALUES('" & Session("CodEmpresa") & "'," & lblCodigo.Text.Trim & "," & Item & "," & _DetalleEq.Rows(i).Cells(6).Text & ",'N','0',NULL,'" & cboMotivo.SelectedValue.Trim & "','" & cFuncion.SelectedValue.Trim & "')"
                        CmdGlobal.ExecuteNonQuery()
                    Else
                        CmdGlobal.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO) " _
                                              & " VALUES('" & Session("CodEmpresa") & "'," & lblCodigo.Text.Trim & "," & Item & "," & _DetalleEq.Rows(i).Cells(6).Text & ",'N','0',NULL,'" & cboMotivo.SelectedValue.Trim & "')"
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    'CAMPO SERIE_PARATRANSITO: ARTICULO_SERIE COMPROMETIDO PARA Q ENTRE EN TRANSITO (EL TRANSITO ES CUANDO YA ESTA ENVIADO Y FALTA RECIBIR)
                    CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & _DetalleEq.Rows(i).Cells(6).Text
                    CmdGlobal.ExecuteNonQuery()
                    If cboMotivo.SelectedValue.Trim = 6 Or cboMotivo.SelectedValue.Trim = 11 Then 'SALIDA AVERIADO O REEMPLAZO
                        ''paso 1: motivo de salida: reemplazo por cambio o por averia
                        CmdGlobal.CommandText = "SELECT MAX(REEM_NRO) FROM TBINV_REEMPLAZOS"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CodReemplazo = Nz(Rs(0)) + 1
                            End While
                        Else
                            CodReemplazo = 1
                        End If
                        Rs.Close()
                        Dim cMotivo As DropDownList = _DetalleEq.Rows(i).Cells(11).FindControl("cboAveria")
                        Dim tDetReemplazo As TextBox = _DetalleEq.Rows(i).Cells(13).FindControl("txtDetAveria")
                        CmdGlobal.CommandText = " INSERT INTO TBINV_REEMPLAZOS(EMPRESA_CODIGO,REEM_NRO, NRO_SALIDA_ALM, REEM_TIPO, REEM_DETALLE, REEM_OBSERVACION,SERIE_NUMERAR_REEMPLAZANTE, REEM_PLACA_REEMPLAZANTE," _
                                              & " REEM_ESTADO_1, REEM_ESTADO_2, REEM_SYS_EST, REEM_SYS_CRE, REEM_TIPO_DESTINO," _
                                              & " REEM_CODIGO_DESTINO,REEM_TIPO_ORIGEN, REEM_CODIGO_ORIGEN,REEM_FECHA)" _
                                              & " VALUES ('" & Session("CodEmpresa") & "','" & CodReemplazo & "','" & lblCodigo.Text.Trim & "','" & IIf(cboMotivo.SelectedValue.Trim = "6", "1", "2") & "', '" & cMotivo.SelectedValue.Trim & "','" & tDetReemplazo.Text.Trim & "','" & _DetalleEq.Rows(i).Cells(6).Text & "','" & _DetalleEq.Rows(i).Cells(5).Text & "', " _
                                              & " '0','1','0','" & ValorSys & "','" & IIf(RBAlmacen.Checked = True, "1", "2") & "', " _
                                              & " '" & lblCodDestino.Text.Trim & "','1','" & lblCodOrigen.Text.Trim & "','" & FechaServer & "')"
                        CmdGlobal.ExecuteNonQuery()
                        'If cMotivo.SelectedValue.Trim <> "" Then
                        '    CmdGlobal.CommandText = " UPDATE TBINV_REEMPLAZOS SET REEM_PLACA_REEMPLAZADO = '" & .TextMatrix(i, 12) & "',SERIE_NUMERAR_REEMPLAZADO='" & .TextMatrix(i, 13) & "' WHERE NRO_SALIDA_ALM='" & lblCodigo & "' AND SERIE_NUMERAR_REEMPLAZANTE ='" & _DetalleEq.Rows(i).Cells(5).Text & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        '    CmdGlobal.ExecuteNonQuery()
                        'End If
                    ElseIf cboMotivo.SelectedValue.Trim = 2 Then
                        ''paso 1:
                        'motivo de salida: por reparacion aqui se ingresara el tipo y el codigo de ubicacion del envio, el detalle por soto, el nro de salida del almacen dependiendo del nro de la averia
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_AVERIA WHERE AVERIA_NRO ='" & _DetalleEq.Rows(i).Cells(9).Text & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='2' AND AVERIA_ESTADO_2='1' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                Dim tDetReemplazo As TextBox = _DetalleEq.Rows(i).Cells(12).FindControl("txtDetAveria")
                                CmdGlobal.CommandText = " UPDATE TBINV_AVERIA SET AVERIA_DETALLE_SOTO='" & tDetReemplazo.Text.Trim & "', SALIDA_NRO_ALM='" & lblCodigo.Text.Trim & "', AVERIA_SYS_MOD='" & ValorSys & "' , " _
                                                      & " AVERIA_TIPO_TENVIO='" & IIf(RBAlmacen.Checked = True, "1", "2") & "', AVERIA_CODIGO_TENVIO='" & lblCodDestino.Text.Trim & "'" _
                                                      & " WHERE AVERIA_NRO='" & _DetalleEq.Rows(i).Cells(9).Text & "' AND AVERIA_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                CmdGlobal.ExecuteNonQuery()
                            End While
                        End If
                        Rs.Close()
                    ElseIf cboMotivo.SelectedValue.Trim = 3 Or cboMotivo.SelectedValue.Trim = 14 Or cboMotivo.SelectedValue.Trim = 15 Or cboMotivo.SelectedValue.Trim = 25 Then
                        ''            Dim Tipo As String
                        Tipo = cboMotivo.SelectedValue.Trim
                        CmdGlobal.CommandText = " SELECT MAX(DEVOL_NRO) FROM TBINV_EQUIPOS_DEVUELTOS "
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CodDevolucion = Nz(Rs(0)) + 1
                            End While
                        Else
                            CodDevolucion = 1
                        End If
                        Rs.Close()
                        CmdGlobal.CommandText = " INSERT INTO TBINV_EQUIPOS_DEVUELTOS (DEVOL_NRO,SALIDA_NRO,SERIE_NUMERAR,DEVOL_OBSEVACION, " _
                                              & " DEVOL_TIPO,DEVOL_ESTADO,DEVOL_SYS_EST,DEVOL_SYS_CRE)" _
                                              & " VALUES ('" & CodDevolucion & "','" & lblCodigo.Text.Trim & "','" & _DetalleEq.Rows(i).Cells(6).Text & "','" & txtObs.Text.Trim & "', " _
                                              & " '" & IIf(Tipo = "3", "1", IIf(Tipo = "14", "2", IIf(Tipo = "15", "3", IIf(Tipo = "25", "4", "")))) & "','0','0','" & ValorSys & "') "
                        CmdGlobal.ExecuteNonQuery()
                    ElseIf cboMotivo.SelectedValue.Trim = 16 Then ' MANTENIMIENTO EN PROVEEDOR DE EQUIPOS AVERIADOS
                        CmdGlobal.CommandText = " SELECT MAX(MANTEN_NRO) FROM TBINV_EQUIPOS_MANTENIMIENTOS "
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CodMantenimiento = Nz(Rs(0)) + 1
                            End While
                        Else
                            CodMantenimiento = 1
                        End If
                        Rs.Close()
                        CmdGlobal.CommandText = " INSERT INTO TBINV_EQUIPOS_MANTENIMIENTOS (EMPRESA_CODIGO,MANTEN_NRO,SALIDA_NRO,SERIE_NUMERAR,MANTEN_OBSERVACION, " _
                                              & " MANTEN_ESTADO,MANTEN_SYS_EST,MANTEN_SYS_CRE,MANTEN_PROVEEDOR)" _
                                              & " VALUES ('" & Session("CodEmpresa") & "','" & CodMantenimiento & "','" & lblCodigo.Text.Trim & "','" & _DetalleEq.Rows(i).Cells(6).Text & "','" & txtObs.Text.Trim & "', " _
                                              & " '0','0','" & ValorSys & "','" & lblCodDestino.Text.Trim & "') "
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_AVERIA WHERE AVERIA_ESTADO_1='2' AND AVERIA_ESTADO_2='1' AND AVERIA_SYS_EST='0' AND AVERIA_SERIE_NUMERAR=" & _DetalleEq.Rows(i).Cells(6).Text
                        Rs2 = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CmdGlobal2.CommandText = " UPDATE TBINV_AVERIA SET  " _
                                                      & " SALIDA_NRO_ALM=" & lblCodigo.Text.Trim & ", " _
                                                      & " AVERIA_TIPO_TENVIO= '3', " _
                                                      & " AVERIA_CODIGO_TENVIO=" & lblCodDestino.Text.Trim & ", " _
                                                      & " AVERIA_SYS_MOD='" & ValorSys & "'  " _
                                                      & " WHERE AVERIA_NRO=" & Nz(Rs2!AVERIA_NRO) & " AND AVERIA_SYS_EST='0'"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal2.CommandText = "SELECT MAX(AVERIA_NRO) FROM TBINV_AVERIA"
                            Rs = CmdGlobal2.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CodAveria = Nz(Rs(0)) + 1
                                End While
                            Else
                                CodAveria = 1
                            End If
                            Rs.Close()
                            CmdGlobal2.CommandText = " INSERT INTO TBINV_AVERIA (EMPRESA_CODIGO, AVERIA_NRO, AVERIA_FECHA,  SALIDA_NRO_ALM, " _
                                                  & " AVERIA_SERIE_NUMERAR, AVERIA_ESTADO_1, AVERIA_ESTADO_2, AVERIA_TIPO_ORIGEN, AVERIA_CODIGO_ORIGEN, " _
                                                  & " AVERIA_SYS_CRE, AVERIA_SYS_EST, AVERIA_TIPO_DESTINO,AVERIA_CODIGO_DESTINO) " _
                                                  & " VALUES ('" & Session("CodEmpresa") & "','" & CodAveria & "','" & FechaServer & "','" & lblCodigo.Text.Trim & "', " _
                                                  & " '" & _DetalleEq.Rows(i).Cells(6).Text & "','0','1','1','" & lblCodOrigen.Text.Trim & "', " _
                                                  & " '" & ValorSys & "','0','" & IIf(RBAlmacen.Checked = True, "1", "2") & "','" & lblCodDestino.Text.Trim & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                        Rs2.Close()
                    ElseIf cboMotivo.SelectedValue.Trim = 17 Then 'x averia
                        CmdGlobal.CommandText = "SELECT MAX(AVERIA_NRO) FROM TBINV_AVERIA"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CodAveria = Nz(Rs(0)) + 1
                            End While
                        Else
                            CodAveria = 1
                        End If
                        Rs.Close()
                        Dim cAveria As DropDownList = .Rows(i).Cells(11).FindControl("cboAveria")
                        Dim tDetAveria As TextBox = _DetalleEq.Rows(i).Cells(13).FindControl("txtDetAveria")
                        CmdGlobal.CommandText = " INSERT INTO TBINV_AVERIA (EMPRESA_CODIGO, AVERIA_NRO, AVERIA_FECHA, AVERIA_TIPO, SALIDA_NRO_ALM, " _
                                              & " AVERIA_DETALLE_SOTO, AVERIA_SERIE_NUMERAR, AVERIA_ESTADO_1, AVERIA_ESTADO_2, AVERIA_TIPO_ORIGEN, AVERIA_CODIGO_ORIGEN, " _
                                              & " AVERIA_TIPO_TENVIO, AVERIA_CODIGO_TENVIO, AVERIA_SYS_CRE, AVERIA_SYS_EST, " _
                                              & " AVERIA_TIPO_DESTINO,AVERIA_CODIGO_DESTINO) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "','" & CodAveria & "','" & FechaServer & "','" & cAveria.SelectedValue.Trim & "','" & lblCodigo.Text.Trim & "', " _
                                              & " '" & tDetAveria.Text.Trim & "','" & _DetalleEq.Rows(i).Cells(6).Text & "','0','1','1','" & lblCodOrigen.Text.Trim & "', " _
                                              & " '" & IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")) & "','" & lblCodDestino.Text.Trim & "','" & ValorSys & "','0'," _
                                              & " '1','" & lblCodOrigen.Text.Trim & "')"
                        CmdGlobal.ExecuteNonQuery()
                    ElseIf cboMotivo.SelectedValue.Trim = 18 Then 'devolucion x reparacion
                        CmdGlobal.CommandText = " UPDATE TBINV_AVERIA SET SALIDA_ASOTO='" & lblCodigo.Text.Trim & "', AVERIA_SYS_MOD='" & ValorSys & "' " _
                                              & " WHERE AVERIA_NRO='" & _DetalleEq.Rows(i).Cells(10).Text & "' AND AVERIA_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        CmdGlobal.ExecuteNonQuery()
                    ElseIf cboMotivo.SelectedValue.Trim = 19 Then 'devolucion EQUIPO REPARADO
                        CmdGlobal.CommandText = " UPDATE TBINV_AVERIA SET SALIDA_DEVOLVER_ALM='" & lblCodigo.Text.Trim & "', AVERIA_SYS_MOD='" & ValorSys & "' " _
                                              & " WHERE AVERIA_NRO='" & _DetalleEq.Rows(i).Cells(10).Text & "' AND AVERIA_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND AVERIA_SERIE_NUMERAR='" & _DetalleEq.Rows(i).Cells(6).Text & "'"
                        CmdGlobal.ExecuteNonQuery()
                    End If
                Next
            End With
            Item = 0
            With _DetalleAc
                For i = 0 To _DetalleAc.Rows.Count - 1
                    Item = Item + 1
                    Dim txtCantSal As TextBox = _DetalleAc.Rows(i).Cells(5).FindControl("txtCantSal")
                    CmdGlobal.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET_SINSERIE( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM,ARTICULO_CODIGO,DESPD_CANTXDESP,DESPD_CANT_DESP,DESPD_CANT_REC,DESPD_CANT_FALT_REC,DESPD_SYS_EST,DESPD_MOTIVO) " _
                          & " VALUES('" & Session("CodEmpresa") & "'," & lblCodigo.Text & "," & Item & "," & _DetalleAc.Rows(i).Cells(1).Text & "," & Nz(txtCantSal.Text) & ",0,0,0,'0','" & cboMotivo.SelectedValue.Trim & "')"
                    CmdGlobal.ExecuteNonQuery()
                    'CAMPO SAA_PARATRANSITO: CANT DE ARTICULOS COMPROMETIDOS PARA Q ENTRE EN TRANSITO (EL TRANSITO ES CUANDO YA ESTA ENVIADO Y FALTA RECIBIR)
                    CmdGlobal.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_PARATRANSITO = ISNULL(SAA_PARATRANSITO,0) + " & Nz(txtCantSal.Text) & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND UBICACT_TIPO='1' AND ALMACEN_CODIGO=" & lblCodOrigen.Text & " AND ARTICULO_CODIGO = " & _DetalleAc.Rows(i).Cells(1).Text
                    CmdGlobal.ExecuteNonQuery()
                Next
            End With
            ''ENVIO DE MERCADERIA
            Dim TipoOperac As String, NomOperac As String
            Dim ItemPrestamo As Integer
            Dim ItemAlquiler As Integer
            Dim StockAc As Double = 0
            'Dim CodAllSal As String
            'PUEDE SALIDA 3 DIAS A FUTURO
            Dim psCodOrigenC As String = ""
            Dim psCodMotivoC As String = ""
            CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodOrigenC = Nu(Rs!ALMACEN_ORIGEN)
                    psCodMotivoC = Nu(Rs!DESP_MOTIVO_GRAL)
                End While
            End If
            Rs.Close()
            Dim obj As New clsInv_Procesos

            If Session("TicketNro") <> "" Then
                Dim psNroTicket As String = ""
                psNroTicket = Session("TicketNro")
                Dim psConexion2 As String = ""
                psConexion2 = Session("Ruta_Emp")
                obj.Guardar_RelacionTicket(psConexion2, psNroTicket, "18", Nz(lblCodigo.Text), Session("User"))

                CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_TICKET = " & psNroTicket & " WHERE DESP_CODIGO = " & lblCodigo.Text
                CmdGlobal.ExecuteNonQuery()
            End If

            Dim pValorVenta As String = ""
            Dim psAño As String = ""
            Dim psPer As String = ""
            Dim psCodTrans As String = ""
            Dim psCodTipoTrans As String = "1"
            Dim psVale As String = ""
            Dim psCodMov As String = ""
            Dim psCodTransD As String = ""
            Dim psSalidaNumerar As String = ""
            Dim psCodigoDestino As String = ""
            Dim psNroMovimiento As String = ""
            Dim a As Integer = 0
            Dim psCodMotivo As String = ""
            Dim objCont As New clsCont_Funciones
            Dim objProceso As New clsInv_Procesos
            Dim psFechaTC As String = Right(txtFecha.Text.Trim, 4) + Mid(txtFecha.Text.Trim, 4, 2) + Left(txtFecha.Text.Trim, 2)
            pValorVenta = objCont.Hallar_Valor_Venta(Session("Ruta_Emp"), psFechaTC)
            Call objProceso.Actualizar_CostoArticulo(psConexion, Session("CodEmpresa"), lblCodigo.Text.Trim, "2", psCodMotivoC, "1", psCodOrigenC, pValorVenta, "", "1")
            CmdGlobal2.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
            Rs2 = CmdGlobal2.ExecuteReader
            If Rs2.HasRows Then
                While Rs2.Read
                    If Nu(Rs2!DESP_ESTADO) <> "1" Then
                        lblMensaje.Text = "La Salida ya está enviada." : Exit Sub
                    Else
                        'datos para el movimiento
                        Dim TotalArt As Long
                        psAño = objCont.AñoSistema(Session("Ruta_Emp"), Session("CodEmpresa"))
                        psPer = ""
                        CmdGlobal.CommandText = "SELECT PER_PERIODO FROM TBPERIODIFICACION WHERE (PER_EMPRESA = '" & Session("CodEmpresa") & "') AND (PER_AÑO = '" & psAño & "') AND (PER_ACTUAL = 'S') AND (PER_SYS_EST = '0')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psPer = Nu(Rs!PER_PERIODO)
                            End While
                        End If
                        Rs.Close()
                        If psPer = "" Then lblMensaje.Text = "No se ha podido encontrar periodo contable actual." : Exit Sub
                        psCodTrans = ""
                        TipoOperac = objProceso.Tipo_OperacSal(Nu(Rs2!DESP_MOTIVO_GRAL))
                        NomOperac = objProceso.Nombre_OperacSal(Nu(Rs2!DESP_MOTIVO_GRAL))
                        CmdGlobal.CommandText = " SELECT TRANS_CODIGO,TRANS_DESCRIPCION FROM TBINV_TRANSACCIONES_ALMACEN WHERE TRANS_SYS_EST='0' AND " _
                                              & " TRANS_TIPO='" & psCodTipoTrans & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND " & TipoOperac & "='S' ORDER BY TRANS_CODIGO"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psCodTrans = Nu(Rs!TRANS_codigo)
                            End While
                        End If
                        Rs.Close()
                        If psCodTrans = "" Then lblMensaje.Text = "No se ha podido transacción " & NomOperac & "." : Exit Sub
                        psVale = ""
                        CmdGlobal.CommandText = " SELECT MAX(M.MOVAL_NRO_VALE) FROM TBINV_MOVIMIENTOS_ALMACEN M INNER JOIN TBINV_TRANSACCIONES_ALMACEN T ON M.TRANS_CODIGO = T.TRANS_CODIGO AND M.EMPRESA_CODIGO=T.EMPRESA_CODIGO " _
                                              & " WHERE (T.TRANS_TIPO = '" & psCodTipoTrans & "') AND (M.EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (ALMACEN_CODIGO='" & Nu(Rs2!ALMACEN_ORIGEN) & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psVale = Format(Nz(Rs(0)) + 1, "00000000")
                            End While
                        Else
                            psVale = "00000001"
                        End If
                        Rs.Close()
                        psCodMov = ""
                        CmdGlobal.CommandText = "SELECT MAX(MOVAL_CODIGO) FROM TBINV_MOVIMIENTOS_ALMACEN WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psCodMov = Format(Nz(Rs(0)) + 1, "00000000")
                            End While
                        Else
                            psCodMov = "00000001"
                        End If
                        Rs.Close()
                        psCodTransD = ""
                        CmdGlobal.CommandText = " SELECT TRANSD_CODIGO,TRANSD_VALOR FROM TBINV_TRANS_ALMACEN_DETALLE WHERE (TRANSD_DETALLE = '2') AND " _
                                              & " (TRANS_CODIGO = " & psCodTrans & ") AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' ORDER BY TRANSD_CODIGO"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psCodTransD = Nu(Rs!TRANSD_CODIGO)
                            End While
                        End If
                        Rs.Close()
                        TotalArt = Nz(Rs2!DESP_CANTXDESP)
                        CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTOS_ALMACEN(EMPRESA_CODIGO, MOVAL_CODIGO,ALMACEN_CODIGO,MOVAL_SYS_EST,MOVAL_SYS_CRE) " _
                                              & "VALUES('" & Session("CodEmpresa") & "'," & psCodMov & ",'" & Nu(Rs2!ALMACEN_ORIGEN) & "','0','" & ValorSys & "')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "UPDATE TBINV_MOVIMIENTOS_ALMACEN SET CONTABLE_AÑO='" & psAño & "',CONTABLE_PERIODO=" & psPer & "," _
                                              & "TRANS_CODIGO=" & psCodTrans & ",MOVAL_NRO_VALE='" & psVale & "',MOVAL_FECHA='" & FechaServer & "'," _
                                              & "MOVAL_SYS_MOD='" & ValorSys & "',MOVAL_TOTAL_ART=" & TotalArt & " WHERE MOVAL_CODIGO=" & psCodMov & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        CmdGlobal.ExecuteNonQuery()
                        'INSERTAR O ACTUALIZAR EL DETALLE ARTICULOS ::::::::::::::::::::::::::::::::::::::::::::::::::  ARTICULOS Q USAN SERIE
                        A = 0
                        CmdGlobal3.CommandText = "SELECT S.ARTICULO_CODIGO, COUNT(DD.DESPD_ITEM) AS CANT " _
                            & " FROM TBINV_ALMACEN_DESPACHO_DET DD INNER JOIN TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR" _
                            & " WHERE (DD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')  GROUP BY S.ARTICULO_CODIGO, DD.DESP_CODIGO  HAVING (DD.DESP_CODIGO = " & lblCodigo.Text.Trim & ")"
                        Rs3 = CmdGlobal3.ExecuteReader
                        If Rs3.HasRows Then
                            While Rs3.Read
                                If Nz(Rs3("CANT")) > 0 Then
                                    a = a + 1
                                    CmdGlobal.CommandText = "SELECT * FROM TBINV_MOV_ALMACEN_ARTICULOS WHERE (MOVAL_CODIGO =" & psCodMov & ") AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (MOVALA_SYS_EST='0')"
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            CmdGlobal4.CommandText = "UPDATE TBINV_MOV_ALMACEN_ARTICULOS SET MOVALA_ART_CANTIDAD=" & Nz(Rs!MOVALA_ART_CANTIDAD) + CDbl(Nz(Rs3("CANT"))) & ",MOVALA_ART_ORDEN=" & a & " WHERE (MOVAL_CODIGO =" & psCodMov & ") AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (MOVALA_SYS_EST='0')"
                                            CmdGlobal4.ExecuteNonQuery()
                                        End While
                                    Else
                                        CmdGlobal4.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_ARTICULOS(MOVAL_CODIGO, ARTICULO_CODIGO,MOVALA_ART_CANTIDAD, MOVALA_ART_ORDEN,EMPRESA_CODIGO,MOVALA_SYS_EST) " _
                                                              & "VALUES(" & psCodMov & "," & Nz(Rs3!ARTICULO_CODIGO) & "," & Nz(Rs3("CANT")) & "," & a & ",'" & Session("CodEmpresa") & "','0')"
                                        CmdGlobal4.ExecuteNonQuery()
                                    End If
                                    Rs.Close()
                                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                        & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL)
                                            If psCodTipoTrans = "0" Then  'INGRESO
                                                StockAc = StockAc + CDbl(Nz(Rs3("CANT")))
                                            Else 'SALIDA
                                                StockAc = StockAc - CDbl(Nz(Rs3("CANT")))
                                            End If
                                            CmdGlobal4.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                                                  & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                            CmdGlobal4.ExecuteNonQuery()
                                        End While
                                    End If
                                    Rs.Close()
                                    If Nu(Rs2!DESP_MOTIVO_GRAL) = "5" Then 'revisar esta tabla
                                        CmdGlobal.CommandText = "SELECT MAX(SALXT_NUMERAR) FROM TBINV_MOV_ALMACEN_SALXTRANS WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                psSalidaNumerar = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            psSalidaNumerar = "1"
                                        End If
                                        Rs.Close()
                                        CmdGlobal.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_SALXTRANS(EMPRESA_CODIGO,SALXT_NUMERAR,DESP_CODIGO," _
                                                              & "ART_CODIGO, CANT_SALIDA,ALMACEN_MOV_CODIGO,USUARIO,FECHA,HORA) " _
                                                              & "VALUES('" & Session("CodEmpresa") & "'," & psSalidaNumerar & "," & lblCodigo.Text.Trim & "," _
                                                              & Nz(Rs3!ARTICULO_CODIGO) & "," & CDbl(Nz(Rs3("CANT"))) & "," & psCodMov & ",'" & lblUsuario.Text.Trim & "','" & FechaServer & "','" & HoraServer & "')"
                                        CmdGlobal.ExecuteNonQuery()
                                    End If
                                    'paso2: se guarda el movimiento de la salida por cualquier motivo
                                    'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                                    If Nu(Rs2!DESP_TIPODESTINO) = "1" Then
                                        psCodigoDestino = Nu(Rs2!ALMACEN_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "2" Then
                                        psCodigoDestino = Nu(Rs2!CECOSE_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "3" Then
                                        psCodigoDestino = Nu(Rs2!PROVEEDOR_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "4" Then
                                        psCodigoDestino = Nu(Rs2!EQUIPO_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "5" Then
                                        psCodigoDestino = Nu(Rs2!PERSONA_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "6" Then
                                        psCodigoDestino = Nu(Rs2!CLIENTE_CODIGO_DESTINO)
                                    End If
                                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            psNroMovimiento = Nz(Rs(0)) + 1
                                        End While
                                    Else
                                        psNroMovimiento = "00000001"
                                    End If
                                    Rs.Close()
                                    '1:INGRESO , 2: SALIDA
                                    psCodMotivo = cboMotivo.SelectedValue.Trim
                                    If psCodMotivo = "21" Or psCodMotivo = "16" Or psCodMotivo = "25" Or psCodMotivo = "26" Or psCodMotivo = "27" Or psCodMotivo = "33" Or psCodMotivo = "4" Or psCodMotivo = "34" Or (psCodMotivo = "1" And Nu(Rs2!DESP_TIPODESTINO)) Then
                                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text.Trim, psCodMotivo, Nu(Rs3!ARTICULO_CODIGO), "1", Nu(Rs2!ALMACEN_ORIGEN), Nu(Rs2!DESP_TIPODESTINO), psCodigoDestino, "", "2", psFechaTC, CDbl(Nz(Rs3("CANT"))))
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                              & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','2','1','" & Nu(Rs2!ALMACEN_ORIGEN) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','" & lblCodigo.Text.Trim & "','" & Nz(Rs3!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs3("CANT"))) & "','" & ValorSys & "','3','" & psCodMotivo & "','" & FechaServer & "','0')"
                                        CmdGlobal4.ExecuteNonQuery()
                                    Else
                                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text.Trim, psCodMotivo, Nu(Rs3!ARTICULO_CODIGO), "1", Nu(Rs2!ALMACEN_ORIGEN), Nu(Rs2!DESP_TIPODESTINO), psCodigoDestino, "", "2", psFechaTC, CDbl(Nz(Rs3("CANT"))))
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                              & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','2','1','" & Nu(Rs2!ALMACEN_ORIGEN) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','" & lblCodigo.Text.Trim & "','" & Nz(Rs3!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs3("CANT"))) & "','" & ValorSys & "','2','" & psCodMotivo & "','" & FechaServer & "','0')"
                                        CmdGlobal.ExecuteNonQuery()
                                    End If
                                    If psCodMotivo = "16" Or psCodMotivo = "25" Or psCodMotivo = "26" Or psCodMotivo = "27" Or psCodMotivo = "33" Or psCodMotivo = "34" Or psCodMotivo = "4" Or (psCodMotivo = "1" And Nu(Rs2!DESP_TIPODESTINO)) Then  'MANTENIMIENTO EN PROVEEDOR
                                        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                psNroMovimiento = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            psNroMovimiento = "00000001"
                                        End If
                                        Rs.Close()
                                        '1:INGRESO , 2: SALIDA
                                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text.Trim, psCodMotivo, Nu(Rs3!ARTICULO_CODIGO), Nu(Rs2!DESP_TIPODESTINO), psCodigoDestino, "1", Nu(Rs2!ALMACEN_ORIGEN), "", "1", psFechaTC, CDbl(Nz(Rs3("CANT"))))
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                              & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','1','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','1','" & Nu(Rs2!ALMACEN_ORIGEN) & "','" & lblCodigo.Text.Trim & "','" & Nz(Rs3!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs3("CANT"))) & "','" & ValorSys & "','3','" & psCodMotivo & "','" & FechaServer & "','0')"
                                        CmdGlobal.ExecuteNonQuery()
                                        CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodigoDestino & ") AND (UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "')" _
                                            & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + CDbl(Nz(Rs3("CANT")))
                                                CmdGlobal4.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodigoDestino & ") AND (UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "')" _
                                                                      & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                                CmdGlobal4.ExecuteNonQuery()
                                            End While
                                        Else
                                            CmdGlobal4.CommandText = " INSERT INTO TBINV_STOCK_ARTICULOS_ALMACEN (SAA_STOCK_ACTUAL,ALMACEN_CODIGO,UBICACT_TIPO,EMPRESA_CODIGO,ARTICULO_CODIGO,SAA_SYS_EST)" _
                                                                  & " VALUES (" & CDbl(Nz(Rs3("CANT"))) & ",'" & lblCodDestino.Text.Trim & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & Session("CodEmpresa") & "'," & Nu(Rs3!ARTICULO_CODIGO) & ",'0')"
                                            CmdGlobal4.ExecuteNonQuery()
                                        End If
                                        Rs.Close()
                                    End If
                                End If
                            End While
                        End If
                        Rs3.Close()

                        'INSERTAR O ACTUALIZAR EL DETALLE ARTICULOS :::::::::::::::::::::::::::::::::::::::::::::::::: ARTICULOS Q NO USAN SERIE
                        CmdGlobal3.CommandText = "SELECT DD.ARTICULO_CODIGO, DD.DESPD_CANTXDESP AS CANT " _
                            & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE DD INNER JOIN TBINV_ARTICULOS A ON DD.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND DD.ARTICULO_CODIGO = A.ART_CODIGO " _
                            & " WHERE (DD.DESP_CODIGO = " & lblCodigo.Text.Trim & ")"
                        Rs3 = CmdGlobal3.ExecuteReader
                        If Rs3.HasRows Then
                            While Rs3.Read
                                If Nz(Rs3("CANT")) > 0 Then
                                    'GUARDADA MOV ALMACEN
                                    a = a + 1
                                    CmdGlobal.CommandText = "SELECT * FROM TBINV_MOV_ALMACEN_ARTICULOS WHERE (MOVAL_CODIGO =" & psCodMov & ") AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (MOVALA_SYS_EST='0')"
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            CmdGlobal4.CommandText = "UPDATE TBINV_MOV_ALMACEN_ARTICULOS SET MOVALA_ART_CANTIDAD=" & Nz(Rs!MOVALA_ART_CANTIDAD) + CDbl(Nz(Rs3("CANT"))) & ",MOVALA_ART_ORDEN=" & a & " WHERE (MOVAL_CODIGO =" & psCodMov & ") AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (MOVALA_SYS_EST='0')"
                                            CmdGlobal4.ExecuteNonQuery()
                                        End While
                                    Else
                                        CmdGlobal4.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_ARTICULOS(MOVAL_CODIGO, ARTICULO_CODIGO,MOVALA_ART_CANTIDAD, MOVALA_ART_ORDEN,EMPRESA_CODIGO,MOVALA_SYS_EST) " _
                                                              & "VALUES(" & psCodMov & "," & Nz(Rs3!ARTICULO_CODIGO) & "," & Nz(Rs3("CANT")) & "," & a & ",'" & Session("CodEmpresa") & "','0')"
                                        CmdGlobal4.ExecuteNonQuery()
                                    End If
                                    Rs.Close()
                                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                        & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL)
                                            If psCodTipoTrans = "0" Then  'INGRESO
                                                StockAc = StockAc + CDbl(Nz(Rs3("CANT")))
                                            Else 'SALIDA
                                                StockAc = StockAc - CDbl(Nz(Rs3("CANT")))
                                            End If
                                            CmdGlobal4.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1') " _
                                                                  & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                            CmdGlobal4.ExecuteNonQuery()
                                        End While
                                    End If
                                    Rs.Close()
                                    If Nu(Rs2!DESP_MOTIVO_GRAL) = "5" Then 'revisar esta tabla
                                        CmdGlobal.CommandText = "SELECT MAX(SALXT_NUMERAR) FROM TBINV_MOV_ALMACEN_SALXTRANS WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                psSalidaNumerar = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            psSalidaNumerar = "1"
                                        End If
                                        Rs.Close()
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOV_ALMACEN_SALXTRANS(EMPRESA_CODIGO,SALXT_NUMERAR,DESP_CODIGO," _
                                                              & " ART_CODIGO, CANT_SALIDA,ALMACEN_MOV_CODIGO,USUARIO,FECHA,HORA) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & psSalidaNumerar & "," & lblCodigo.Text.Trim & "," _
                                                              & " " & Nz(Rs3!ARTICULO_CODIGO) & "," & CDbl(Nz(Rs3("CANT"))) & "," & psCodMov & ",'" & lblUsuario.Text.Trim & "','" & FechaServer & "','" & HoraServer & "')"
                                        CmdGlobal.ExecuteNonQuery()
                                    End If
                                    '==================================================INGRESO A LA TABLA MOVIENTO GENERAL
                                    'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                                    If Nu(Rs2!DESP_TIPODESTINO) = "1" Then
                                        psCodigoDestino = Nu(Rs2!ALMACEN_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "2" Then
                                        psCodigoDestino = Nu(Rs2!CECOSE_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "3" Then
                                        psCodigoDestino = Nu(Rs2!PROVEEDOR_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "4" Then
                                        psCodigoDestino = Nu(Rs2!EQUIPO_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "5" Then
                                        psCodigoDestino = Nu(Rs2!PERSONA_CODIGO_DESTINO)
                                    ElseIf Nu(Rs2!DESP_TIPODESTINO) = "6" Then
                                        psCodigoDestino = Nu(Rs2!CLIENTE_CODIGO_DESTINO)
                                    End If
                                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            psNroMovimiento = Nz(Rs(0)) + 1
                                        End While
                                    Else
                                        psNroMovimiento = "00000001"
                                    End If
                                    Rs.Close()
                                    '1:INGRESO , 2: SALIDA
                                    If psCodMotivo = "21" Or psCodMotivo = "16" Or psCodMotivo = "25" Or psCodMotivo = "26" Or psCodMotivo = "27" Or
                                       psCodMotivo = "33" Or psCodMotivo = "4" Or psCodMotivo = "34" Or (psCodMotivo = "1" And Nu(Rs2!DESP_TIPODESTINO) = "6") Or
                                       (psCodMotivo = "6" And Nu(Rs2!DESP_TIPODESTINO) = "5") Or (psCodMotivo = "6" And Nu(Rs2!DESP_TIPODESTINO) = "6") Then
                                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text.Trim, psCodMotivo, Nu(Rs3!ARTICULO_CODIGO), "1", Nu(Rs2!ALMACEN_ORIGEN), Nu(Rs2!DESP_TIPODESTINO), psCodigoDestino, "", "2", psFechaTC, CDbl(Nz(Rs3("CANT"))))
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                              & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','2','1','" & Nu(Rs2!ALMACEN_ORIGEN) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','" & lblCodigo.Text.Trim & "','" & Nz(Rs3!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs3("CANT"))) & "','" & ValorSys & "','3','" & psCodMotivo & "','" & FechaServer & "','0')"
                                        CmdGlobal.ExecuteNonQuery()
                                    Else
                                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text.Trim, psCodMotivo, Nu(Rs3!ARTICULO_CODIGO), "1", Nu(Rs2!ALMACEN_ORIGEN), Nu(Rs2!DESP_TIPODESTINO), psCodigoDestino, "", "2", psFechaTC, CDbl(Nz(Rs3("CANT"))))
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                              & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','2','1','" & Nu(Rs2!ALMACEN_ORIGEN) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','" & lblCodigo.Text.Trim & "','" & Nz(Rs3!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs3("CANT"))) & "','" & ValorSys & "','2','" & psCodMotivo & "','" & FechaServer & "','0')"
                                        CmdGlobal.ExecuteNonQuery()
                                    End If
                                    If psCodMotivo = "16" Or psCodMotivo = "25" Or psCodMotivo = "26" Or psCodMotivo = "27" Or psCodMotivo = "33" Or
                                       psCodMotivo = "34" Or psCodMotivo = "4" Or (psCodMotivo = "1" And Nu(Rs2!DESP_TIPODESTINO) = "6") Or
                                       (psCodMotivo = "6" And Nu(Rs2!DESP_TIPODESTINO) = "5") Or (psCodMotivo = "6" And Nu(Rs2!DESP_TIPODESTINO) = "6") Then 'MANTENIMIENTO EN PROVEEDOR
                                        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                psNroMovimiento = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            psNroMovimiento = "00000001"
                                        End If
                                        Rs.Close()
                                        '1:INGRESO , 2: SALIDA
                                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text.Trim, psCodMotivo, Nu(Rs3!ARTICULO_CODIGO), Nu(Rs2!DESP_TIPODESTINO), psCodigoDestino, "1", Nu(Rs2!ALMACEN_ORIGEN), "", "1", psFechaTC, CDbl(Nz(Rs3("CANT"))))
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                              & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','1','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','1','" & Nu(Rs2!ALMACEN_ORIGEN) & "','" & lblCodigo.Text.Trim & "','" & Nz(Rs3!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs3("CANT"))) & "','" & ValorSys & "','3','" & psCodMotivo & "','" & FechaServer & "','0')"
                                        CmdGlobal.ExecuteNonQuery()
                                        CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodigoDestino & ") AND (UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "')" _
                                            & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + CDbl(Nz(Rs3("CANT")))
                                                CmdGlobal4.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodDestino.Text.Trim & ") AND (UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "')" _
                                                                      & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                                CmdGlobal4.ExecuteNonQuery()
                                            End While
                                        Else
                                            CmdGlobal4.CommandText = " INSERT INTO TBINV_STOCK_ARTICULOS_ALMACEN (SAA_STOCK_ACTUAL,ALMACEN_CODIGO,UBICACT_TIPO,EMPRESA_CODIGO,ARTICULO_CODIGO,SAA_SYS_EST)" _
                                                                  & " VALUES (" & CDbl(Nz(Rs3("CANT"))) & ",'" & lblCodDestino.Text.Trim & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & Session("CodEmpresa") & "'," & Nu(Rs3!ARTICULO_CODIGO) & ",'0')"
                                            CmdGlobal4.ExecuteNonQuery()
                                        End If
                                        Rs.Close()
                                    End If
                                End If
                            End While
                        End If
                        Rs3.Close()

                        CmdGlobal.CommandText = "DELETE FROM TBINV_MOV_ALMACEN_REFERENCIA WHERE MOVAL_CODIGO=" & psCodMov & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        CmdGlobal.ExecuteNonQuery()
                        If psCodTransD <> "" Then
                            CmdGlobal.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_REFERENCIA(MOVAL_CODIGO, TRANS_CODIGO, TRANS_REF_CODIGO,MOVALREF_VALOR,EMPRESA_CODIGO,MOVALREF_SYS_EST) " _
                                                  & "VALUES(" & psCodMov & "," & psCodTrans & "," & psCodTransD & ",'" & lblCodigo.Text.Trim & "','" & Session("CodEmpresa") & "','0')"
                            CmdGlobal.ExecuteNonQuery()
                        End If
                        'COLOCAR LA SALIDA DE ALMACEN ESTADO ENVIADO
                        ' depende del motivo si es diferente de salida por componente
                        If psCodMotivo = "16" Or psCodMotivo = "25" Or psCodMotivo = "26" Or psCodMotivo = "27" Or psCodMotivo = "33" Or psCodMotivo = "39" Or psCodMotivo = "40" Or psCodMotivo = "8" Or
                           psCodMotivo = "34" Or psCodMotivo = "5" Or psCodMotivo = "4" Or (psCodMotivo = "1" And Nu(Rs2!DESP_TIPODESTINO) = "6") Or
                           (psCodMotivo = "6" And Nu(Rs2!DESP_TIPODESTINO) = "5") Or (psCodMotivo = "6" And Nu(Rs2!DESP_TIPODESTINO) = "6") Then 'mantenimiento en proveedor
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_FECHA_SAL='" & FechaSal & "',DESP_HORA_SAL='" & HoraSal & "',DESP_ESTADO='3',DESP_SYS_EJEC='" & ValorSys & "',DESP_CANT_DESP=DESP_CANTXDESP,DESP_CANT_REC=DESP_CANTXDESP,DESP_TIPO_DOC_SALIDA='" & OptDocSalida.SelectedValue.Trim & "',DESP_SYS_REC='" & ValorSys & "',DESP_CANT_FALT_REC=0 WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET DESPD_OK='S',RECIBIDA_OK='S',DESPD_SYS_REC='" & ValorSys & "', DESPD_MODO_RECIBIDO='M'  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET_SINSERIE SET DESPD_CANT_DESP=DESPD_CANTXDESP,DESPD_CANT_REC=DESPD_CANTXDESP,DESPD_CANT_FALT_REC=0 WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                        ElseIf psCodMotivo = "21" Then 'si es salida por componte solo hacia un equipo
                            Dim CompoCod As Integer
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_FECHA_SAL='" & FechaSal & "',DESP_HORA_SAL='" & HoraSal & "',DESP_ESTADO='3',DESP_SYS_EJEC='" & ValorSys & "',DESP_CANT_DESP=DESP_CANTXDESP,DESP_CANT_REC=DESP_CANTXDESP,DESP_TIPO_DOC_SALIDA='2',DESP_SYS_REC='" & ValorSys & "',DESP_CANT_FALT_REC=0 WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET DESPD_OK='S',RECIBIDA_OK='S',DESPD_SYS_REC='" & ValorSys & "', DESPD_MODO_RECIBIDO='M'  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET_SINSERIE SET DESPD_CANT_DESP=DESPD_CANTXDESP,DESPD_CANT_REC=DESPD_CANTXDESP,DESPD_CANT_FALT_REC=0 WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal3.CommandText = " SELECT S.SERIE_NUMERAR,S.ARTICULO_CODIGO, COUNT(DD.DESPD_ITEM) AS CANT " _
                                                   & " FROM TBINV_ALMACEN_DESPACHO_DET DD INNER JOIN TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR" _
                                                   & " WHERE (DD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')  GROUP BY S.ARTICULO_CODIGO, DD.DESP_CODIGO,S.SERIE_NUMERAR  HAVING (DD.DESP_CODIGO = " & lblCodigo.Text.Trim & ")"
                            Rs3 = CmdGlobal3.ExecuteReader
                            If Rs3.HasRows Then
                                While Rs3.Read
                                    If Nz(Rs3("CANT")) > 0 Then
                                        CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & Nu(Rs2!EQUIPO_CODIGO_DESTINO) & ") AND (UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "')" _
                                                              & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + CDbl(Nz(Rs3("CANT")))
                                                CmdGlobal4.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(Rs2!EQUIPO_CODIGO_DESTINO) & ") AND (UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "') " _
                                                                  & " AND (ARTICULO_CODIGO = " & Nz(Rs3!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                                CmdGlobal4.ExecuteNonQuery()
                                            End While
                                        Else
                                            CmdGlobal4.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                                   & " VALUES(" & Nu(Rs2!EQUIPO_CODIGO_DESTINO) & "," & Nu(Rs2!DESP_TIPODESTINO) & "," & Nz(Rs3!ARTICULO_CODIGO) & "," & CDbl(Nz(Rs3("CANT"))) & ",'0','" & Session("CodEmpresa") & "')"
                                            CmdGlobal4.ExecuteNonQuery()
                                        End If
                                        Rs.Close()
                                        CmdGlobal.CommandText = "SELECT MAX(SERCOM_ID) FROM TBINV_ARTICULOS_SERIES_COMPON_" & Session("CodEmpresa") & " "
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                CompoCod = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            CompoCod = 1
                                        End If
                                        Rs.Close()
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_COMPON_" & Session("CodEmpresa") & "(SERCOM_ID,SERIE_NUMERAR_EQUIPO, " _
                                                              & " SERIE_NUMERAR_COMPONENTE,SERCOM_SYS_EST,ARTICULO_CODIGO_COMPONENTE,SERCOM_SYS_CRE) " _
                                                              & " VALUES(" & CompoCod & "," & Nu(Rs2!EQUIPO_CODIGO_DESTINO) & ",'" & Nu(Rs3!Serie_Numerar) & "','0','" & Nu(Rs3!ARTICULO_CODIGO) & "','" & ValorSys & "')"
                                        CmdGlobal.ExecuteNonQuery()
                                        CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_SYS='" & ValorSys & "',UBICACT_TIPO='4', UBICACT_CODIGO='" & Nu(Rs2!EQUIPO_CODIGO_DESTINO) & "' WHERE SERIE_NUMERAR ='" & Nu(Rs3!Serie_Numerar) & "'"
                                        CmdGlobal.ExecuteNonQuery()
                                        'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                                        If Nu(Rs2!DESP_TIPODESTINO) = "1" Then
                                            psCodigoDestino = Nu(Rs2!ALMACEN_CODIGO_DESTINO)
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "2" Then
                                            psCodigoDestino = Nu(Rs2!CECOSE_CODIGO_DESTINO)
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "3" Then
                                            psCodigoDestino = Nu(Rs2!PROVEEDOR_CODIGO_DESTINO)
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "4" Then
                                            psCodigoDestino = Nu(Rs2!EQUIPO_CODIGO_DESTINO)
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "5" Then
                                            psCodigoDestino = Nu(Rs2!PERSONA_CODIGO_DESTINO)
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "6" Then
                                            psCodigoDestino = Nu(Rs2!CLIENTE_CODIGO_DESTINO)
                                        End If
                                        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                        Rs = CmdGlobal.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                psNroMovimiento = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            psNroMovimiento = "00000001"
                                        End If
                                        Rs.Close()
                                        '1:INGRESO , 2: SALIDA
                                        'Call Movimiento_Kardex(lblCodigo3, lblCodMotivo.Caption, Nu(Rs3!ARTICULO_CODIGO), Nu(Rs2!DESP_TIPODESTINO), lblCodigoDestino.Caption, "1", Nu(Rs2!ALMACEN_ORIGEN), txtMotivo.Text, "1", txtFecha3, CDbl(Nz(Rs3("CANT"))))
                                        Call objProceso.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text.Trim, psCodMotivo, Nu(Rs3!ARTICULO_CODIGO), Nu(Rs2!DESP_TIPODESTINO), psCodigoDestino, "1", Nu(Rs2!ALMACEN_ORIGEN), "", "1", psFechaTC, CDbl(Nz(Rs3("CANT"))))
                                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                              & " values('" & Session("CodEmpresa") & "','" & psNroMovimiento & "','1','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','1','" & Nu(Rs2!ALMACEN_ORIGEN) & "','" & lblCodigo.Text.Trim & "','" & Nz(Rs3!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs3("CANT"))) & "','" & ValorSys & "','3','" & psCodMotivo & "','" & FechaServer & "','0')"
                                        CmdGlobal.ExecuteNonQuery()
                                    End If
                                End While
                            End If
                            Rs3.Close()
                        Else
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_FECHA_SAL='" & FechaSal & "',DESP_HORA_SAL='" & HoraSal & "',DESP_ESTADO='2',DESP_SYS_EJEC='" & ValorSys & "',DESP_CANT_DESP=DESP_CANTXDESP,DESP_CANT_FALT_REC=DESP_CANTXDESP,DESP_TIPO_DOC_SALIDA='" & OptDocSalida.SelectedValue.Trim & "' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET DESPD_OK='S',RECIBIDA_OK='N' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET_SINSERIE SET DESPD_CANT_DESP=DESPD_CANTXDESP,DESPD_CANT_FALT_REC=DESPD_CANTXDESP WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            CmdGlobal.ExecuteNonQuery()
                        End If
                        '-----------------------------------------------------
                        Dim NroPrestamo As Long
                        Dim NroAlquiler As Long
                        Dim CodAlmacen As String, CodSeccion As String, Proveedor As String, Equipo As String, Cliente As String, CodPersona As String
                        ItemPrestamo = 0
                        ItemAlquiler = 0
                        Dim CodDestino As String = ""
                        Dim CodAllSal As String = ""
                        If Nu(Rs2!ALMACEN_CODIGO_DESTINO) = "" Then CodAlmacen = "NULL" Else CodDestino = Nu(Rs2!ALMACEN_CODIGO_DESTINO) : CodAlmacen = Nu(Rs2!ALMACEN_CODIGO_DESTINO)
                        If Nu(Rs2!CECOSE_CODIGO_DESTINO) = "" Then CodSeccion = "NULL" Else CodDestino = Nu(Rs2!CECOSE_CODIGO_DESTINO) : CodSeccion = Nu(Rs2!CECOSE_CODIGO_DESTINO)
                        If Nu(Rs2!PROVEEDOR_CODIGO_DESTINO) = "" Then Proveedor = "NULL" Else CodDestino = Nu(Rs2!PROVEEDOR_CODIGO_DESTINO) : Proveedor = Nu(Rs2!PROVEEDOR_CODIGO_DESTINO)
                        If Nu(Rs2!EQUIPO_CODIGO_DESTINO) = "" Then Equipo = "NULL" Else CodDestino = Nu(Rs2!EQUIPO_CODIGO_DESTINO) : Equipo = Nu(Rs2!EQUIPO_CODIGO_DESTINO)
                        If Nu(Rs2!PERSONA_CODIGO_DESTINO) = "" Then CodPersona = "NULL" Else CodDestino = Nu(Rs2!PERSONA_CODIGO_DESTINO) : CodPersona = Nu(Rs2!PERSONA_CODIGO_DESTINO)
                        If Nu(Rs2!CLIENTE_CODIGO_DESTINO) = "" Then Cliente = "NULL" Else CodDestino = Nu(Rs2!CLIENTE_CODIGO_DESTINO) : Cliente = Nu(Rs2!CLIENTE_CODIGO_DESTINO)
                        CmdGlobal.CommandText = "SELECT MAX(PRESTA_CODIGO) FROM TBINV_PRESTAMO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                NroPrestamo = Nz(Rs(0)) + 1
                            End While
                        Else
                            NroPrestamo = 1
                        End If
                        Rs.Close()
                        CmdGlobal.CommandText = "SELECT MAX(ALQUILER_CODIGO) FROM TBINV_ALQUILER WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                NroAlquiler = Nz(Rs(0)) + 1
                            End While
                        Else
                            NroAlquiler = 1
                        End If
                        Rs.Close()
                        CmdGlobal.CommandText = "SELECT MAX(ALLSAL_CODIGO) FROM TBINV_SALIDA_MOTIVO"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CodAllSal = Nz(Rs(0)) + 1
                            End While
                        Else
                            CodAllSal = 1
                        End If
                        Rs.Close()
                        Select Case psCodMotivo
                            Case "1" 'prestamo
                                CmdGlobal.CommandText = "INSERT INTO TBINV_PRESTAMO(EMPRESA_CODIGO, PRESTA_CODIGO, PRESTA_TIPO_MOVIMIENTO, PRESTA_TIPOORIGEN, OSAL_CODIGO, RECEP_CODIGO,DESP_CODIGO," _
                                                      & " ALMACEN_CODIGO_ORIGEN,CECOSE_CODIGO_ORIGEN, PROVEEDOR_CODIGO_ORIGEN, PRESTA_TIPODESTINO,ALMACEN_CODIGO_DESTINO, CECOSE_CODIGO_DESTINO,CLIENTE_CODIGO_DESTINO," _
                                                      & " PROVEEDOR_CODIGO_DESTINO, PRESTA_MOTIVO,PRESTA_TIPO) " _
                                                      & " VALUES('" & Session("CodEmpresa") & "'," & NroPrestamo & ",'S','1',NULL,NULL," & lblCodigo.Text.Trim & "," _
                                                      & Nu(Rs2!ALMACEN_ORIGEN) & ",NULL,NULL,'" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodAlmacen & "," & CodSeccion & "," & Cliente & "," _
                                                      & " NULL,'" & psCodMotivo & "','1') "
                                CmdGlobal.ExecuteNonQuery()
                                'paso 3
                                'se cambiara el estado1 de 2:equipo averiado recibido a 3:equipo averiado enviado a reparaciones
                            Case "2"
                                CmdGlobal.CommandText = "SELECT * FROM TBINV_AVERIA WHERE SALIDA_NRO_ALM ='" & lblCodigo.Text.Trim & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='2' AND AVERIA_ESTADO_2='1' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        CmdGlobal.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='3', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_NRO_ALM='" & lblCodigo.Text.Trim & "' AND AVERIA_ESTADO_1='2' AND AVERIA_ESTADO_2='1' AND AVERIA_SYS_EST ='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                        CmdGlobal.ExecuteNonQuery()
                                    End While
                                End If
                                Rs.Close()
                            Case 3
                            Case 4
                                CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                                      & " VALUES ('" & Session("CodEmpresa") & "'," & CodAllSal & "," & lblCodigo.Text.Trim & ",'" & psCodMotivo & "','1'," & Nu(Rs2!ALMACEN_ORIGEN) & ", " _
                                                      & " '" & Nu(Rs2!DESP_TIPODESTINO) & "'," & lblCodDestino.Text.Trim & ",'" & FechaServer & "','" & HoraServer & "','3','0','" & FechaDevol & "')"
                                CmdGlobal.ExecuteNonQuery()
                            Case "6"
                                CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                                      & " VALUES ('" & Session("CodEmpresa") & "'," & CodAllSal & "," & lblCodigo.Text.Trim & ",'" & psCodMotivo & "','1'," & Nu(Rs2!ALMACEN_ORIGEN) & ", " _
                                                      & " '" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodDestino & ",'" & FechaServer & "','" & HoraServer & "','3','0','" & FechaDevol & "')"
                                CmdGlobal.ExecuteNonQuery()
                            Case "17"
                                CmdGlobal.CommandText = "SELECT * FROM TBINV_AVERIA WHERE SALIDA_NRO_ALM ='" & lblCodigo.Text.Trim & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='0' AND AVERIA_ESTADO_2='1' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        CmdGlobal4.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='3', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_NRO_ALM='" & lblCodigo.Text.Trim & "' AND AVERIA_ESTADO_1='0' AND AVERIA_ESTADO_2='1' AND AVERIA_SYS_EST ='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                        CmdGlobal4.ExecuteNonQuery()
                                    End While
                                End If
                                Rs.Close()
                            Case "18" 'devolucion por reparacion
                                CmdGlobal.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='6', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_ASOTO='" & lblCodigo.Text.Trim & "' AND AVERIA_ESTADO_1='4' AND AVERIA_ESTADO_2 IN ('5','6') AND AVERIA_SYS_EST ='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                CmdGlobal.ExecuteNonQuery()
                            Case 19 'devolucion DE EQUIPO REPARADO
                                '2:SALIDA A DEVOLUCION,3:DEVUELTO
                                CmdGlobal.CommandText = " UPDATE TBINV_AVERIA SET  AVERIA_DEVOLVER_CC='2', AVERIA_SYS_MOD='" & ValorSys & "' " _
                                                      & " WHERE SALIDA_DEVOLVER_ALM='" & lblCodigo.Text.Trim & "' AND AVERIA_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND AVERIA_DEVOLVER_CC='1'"
                                CmdGlobal.ExecuteNonQuery()
                            Case "26"
                                CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                                      & " VALUES ('" & Session("CodEmpresa") & "'," & CodAllSal & "," & lblCodigo.Text.Trim & ",'" & psCodMotivo & "','1'," & Nu(Rs2!ALMACEN_ORIGEN) & ", " _
                                                      & " '" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodDestino & ",'" & FechaServer & "','" & HoraServer & "','3','0','" & FechaDevol & "')"
                                CmdGlobal.ExecuteNonQuery()
                            Case "27"
                                CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                                      & " VALUES ('" & Session("CodEmpresa") & "'," & CodAllSal & "," & lblCodigo.Text.Trim & ",'" & psCodMotivo & "','1'," & Nu(Rs2!ALMACEN_ORIGEN) & ", " _
                                                      & " '" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodDestino & ",'" & FechaServer & "','" & HoraServer & "','3','0','" & FechaDevol & "')"
                                CmdGlobal.ExecuteNonQuery()
                            Case "33"
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ALQUILER (EMPRESA_CODIGO,ALQUILER_CODIGO,ALQUILER_ORIGEN_TIPO,ALQUILER_ORIGEN_CODIGO," _
                                                      & " ALQUILER_DESTINO_TIPO,ALQUILER_DESTINO_CODIGO,ALQUILER_FECHAREG,ALQUILER_HORAREG,ALQUILER_ESTADO,ALQUILER_SYS_EST,DESP_CODIGO) " _
                                                      & " VALUES ('" & Session("CodEmpresa") & "'," & NroAlquiler & ",'1'," & Nu(Rs2!ALMACEN_ORIGEN) & "," _
                                                      & " '" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodDestino & "," & FechaServer & "," & HoraActual(True) & ",'1','0'," & lblCodigo.Text.Trim & ")"
                                CmdGlobal.ExecuteNonQuery()
                            Case Else
                                CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                                      & " VALUES ('" & Session("CodEmpresa") & "'," & CodAllSal & "," & lblCodigo.Text.Trim & ",'" & psCodMotivo & "','1'," & Nu(Rs2!ALMACEN_ORIGEN) & ", " _
                                                      & " '" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodDestino & ",'" & FechaActual() & "','" & HoraServer & "','3','0','" & FechaDevol & "')"
                                CmdGlobal.ExecuteNonQuery()
                        End Select
                        Dim ItemAllSal As Long
                        '::::::::::::::::::::::::::::::::::::::::::::::::::  ARTICULOS Q USAN SERIE
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO_DET WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                If psCodMotivo = "21" Then 'si es una salida por componente
                                    CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & Nu(Rs2!EQUIPO_CODIGO_DESTINO) & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                    CmdGlobal3.ExecuteNonQuery()
                                Else
                                    CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='0',UBICACT_CODIGO=NULL,UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                    CmdGlobal3.ExecuteNonQuery()
                                    '-----------------------------------------------------
                                    ItemPrestamo = ItemPrestamo + 1 'continua abajo
                                    ItemAlquiler = ItemAlquiler + 1 'continua abajo
                                    ItemAlquiler = ItemAlquiler + 1 'continua abajo
                                    Select Case psCodMotivo
                                        Case "1" 'prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = "INSERT TBINV_PRESTAMO_DETALLE(EMPRESA_CODIGO,PRESTA_CODIGO, PREDET_CODIGO,SERIE_NUMERAR, PREDET_SYS_REGISTRO, PREDET_ESTADO_ENVIO, PREDET_SYS_ENVIO, PREDET_ESTADO_PRESTAMO, " _
                                                                  & " PREDET_SYS_PRESTAMO, PREDET_FECHA_PORDEVOLVER, PREDET_SYS_DEVOLUCION) " _
                                                                  & " VALUES('" & Session("CodEmpresa") & "'," & NroPrestamo & "," & ItemPrestamo & "," & Nz(Rs!Serie_Numerar) & ",'" & ValorSys & "','0','" & ValorSys & "','0'," _
                                                                  & " NULL,'" & FechaDevol & "',NULL)"
                                            CmdGlobal3.ExecuteNonQuery()
                                            If Nu(Rs2!DESP_TIPODESTINO) = "6" Then
                                                'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                                CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND B.DESP_CODIGO=" & lblCodigo.Text.Trim & " AND A.SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                                                CmdGlobal3.ExecuteNonQuery()
                                                CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                      & " VALUES('" & Nz(Rs!Serie_Numerar) & "','6','" & Cliente & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                CmdGlobal3.ExecuteNonQuery()
                                                CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & Cliente & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                                CmdGlobal3.ExecuteNonQuery()
                                            End If
                                        Case 2
                                        Case 3 'devolucion por prestamo
                                            '                    DEVOLUCION X PRESTAMO A PROVEEDORES
                                            CmdGlobal3.CommandText = " SELECT * FROM TBINV_EQUIPOS_DEVUELTOS WHERE SALIDA_NRO ='" & lblCodigo.Text.Trim & "'"
                                            Rs3 = CmdGlobal3.ExecuteReader
                                            If Rs3.HasRows Then
                                                While Rs.Read
                                                    CmdGlobal4.CommandText = " UPDATE TBINV_EQUIPOS_DEVUELTOS SET DEVOL_ESTADO ='1',DEVOL_SYS_MOD='" & ValorSys & "',DEVOL_FECHA='" & Format(txtFecha.Text.Trim, "yyyymmdd") & "' WHERE SALIDA_NRO='" & lblCodigo.Text.Trim & "'"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='3',UBICACT_CODIGO='" & Proveedor & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = 'N' WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                                                    CmdGlobal4.ExecuteNonQuery()

                                                    CmdGlobal4.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                          & " VALUES('" & Nz(Rs!Serie_Numerar) & "','3','" & Proveedor & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                    CmdGlobal.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs3.Close()
                                            If Nu(Rs2!DESP_TIPODESTINO) = "1" Then 'almacen
                                                CmdGlobal3.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '2',OSAL_CODIGO_DEVOL = NULL,DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ",RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                       & " AND (B.PREDET_ESTADO_PRESTAMO = '1') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & CodAlmacen & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.SERIE_NUMERAR = " & Nz(Rs!Serie_Numerar) & ")"
                                                CmdGlobal3.ExecuteNonQuery()
                                            ElseIf Nu(Rs2!DESP_TIPODESTINO) = "2" Then 'ccosto
                                                CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '2',OSAL_CODIGO_DEVOL = NULL, DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ", RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                      & " AND (B.PREDET_ESTADO_PRESTAMO = '1') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & CodSeccion & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.SERIE_NUMERAR = " & Nz(Rs!Serie_Numerar) & ")"
                                                CmdGlobal3.ExecuteNonQuery()
                                            ElseIf Nu(Rs2!DESP_TIPODESTINO) = "3" Then 'PROVEEDOR
                                                CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '2',OSAL_CODIGO_DEVOL = NULL, DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ", RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                      & " AND (B.PREDET_ESTADO_PRESTAMO = '1') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '3') AND (A.PROVEEDOR_CODIGO_ORIGEN =" & Proveedor & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.SERIE_NUMERAR = " & Nz(Rs!Serie_Numerar) & ")"
                                                CmdGlobal3.ExecuteNonQuery()
                                            ElseIf Nu(Rs2!DESP_TIPODESTINO) = "6" Then 'Cliente
                                                CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '2',OSAL_CODIGO_DEVOL = NULL, DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ", RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                      & " AND (B.PREDET_ESTADO_PRESTAMO = '1') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '6') AND (A.PROVEEDOR_CODIGO_ORIGEN =" & Cliente & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.SERIE_NUMERAR = " & Nz(Rs!Serie_Numerar) & ")"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End If
                                        Case 6
                                            CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                                                  & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                                                  & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nz(Rs!Serie_Numerar) & ",'" & ValorSys & "'," _
                                                                  & " '" & ValorSys & "','2','1','0')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            If Nu(Rs2!DESP_TIPODESTINO) = "4" Or Nu(Rs2!DESP_TIPODESTINO) = "5" Then
                                                CmdGlobal3.CommandText = "UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='4', REEM_ESTADO_2='2', REEM_SYS_MOD='" & ValorSys & "' WHERE NRO_SALIDA_ALM='" & lblCodigo.Text.Trim & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND (SERIE_NUMERAR_REEMPLAZANTE = " & Nz(Rs!Serie_Numerar) & ")"
                                                CmdGlobal3.ExecuteNonQuery()
                                            Else
                                                CmdGlobal3.CommandText = "UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='1',REEM_SYS_MOD='" & ValorSys & "' WHERE NRO_SALIDA_ALM='" & lblCodigo.Text.Trim & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND (SERIE_NUMERAR_REEMPLAZANTE = " & Nz(Rs!Serie_Numerar) & ")"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End If
                                            If Nu(Rs2!DESP_TIPODESTINO) = "6" Or Nu(Rs2!DESP_TIPODESTINO) = "5" Then
                                                If Nu(Rs2!DESP_TIPODESTINO) = "6" Then
                                                    CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                          & " VALUES('" & Nz(Rs!Serie_Numerar) & "','6','" & Cliente & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                    CmdGlobal3.ExecuteNonQuery()
                                                    CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & Cliente & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                                    CmdGlobal3.ExecuteNonQuery()
                                                ElseIf Nu(Rs2!DESP_TIPODESTINO) = "5" Then
                                                    CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                          & " VALUES('" & Nz(Rs!Serie_Numerar) & "','5','" & CodPersona & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                    CmdGlobal3.ExecuteNonQuery()
                                                    CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & CodPersona & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                                    CmdGlobal3.ExecuteNonQuery()
                                                End If
                                            End If
                                        Case 11
                                            CmdGlobal3.CommandText = "UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='1',REEM_SYS_MOD='" & ValorSys & "' WHERE NRO_SALIDA_ALM='" & lblCodigo.Text.Trim & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND (SERIE_NUMERAR_REEMPLAZANTE = " & Nz(Rs!Serie_Numerar) & ")"
                                            CmdGlobal3.ExecuteNonQuery()
                                        Case 14 'DEVOLUCION POR DEMOSTRACION
                                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_EQUIPOS_DEVUELTOS WHERE SALIDA_NRO ='" & lblCodigo.Text.Trim & "'"
                                            Rs3 = CmdGlobal3.ExecuteReader
                                            If Rs3.HasRows Then
                                                While Rs3.Read
                                                    CmdGlobal4.CommandText = " UPDATE TBINV_EQUIPOS_DEVUELTOS SET DEVOL_ESTADO ='1',DEVOL_SYS_MOD='" & ValorSys & "',DEVOL_FECHA='" & Format(txtFecha.Text.Trim, "yyyymmdd") & "' WHERE SALIDA_NRO='" & lblCodigo.Text.Trim & "'"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='3',UBICACT_CODIGO='" & Proveedor & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = 'N' WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST, INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                          & " VALUES('" & Nz(Rs!Serie_Numerar) & "','3','" & Proveedor & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs3.Close()
                                        Case 15 'DEVOLUCION X RESPALDO
                                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_EQUIPOS_DEVUELTOS WHERE SALIDA_NRO ='" & lblCodigo.Text.Trim & "'"
                                            Rs3 = CmdGlobal3.ExecuteReader
                                            If Rs3.HasRows Then
                                                While Rs3.Read
                                                    CmdGlobal4.CommandText = " UPDATE TBINV_EQUIPOS_DEVUELTOS SET DEVOL_ESTADO ='1',DEVOL_SYS_MOD='" & ValorSys & "',DEVOL_FECHA='" & FechaSal & "' WHERE SALIDA_NRO='" & lblCodigo.Text.Trim & "'"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='3',UBICACT_CODIGO='" & Proveedor & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = 'N' WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                           & " VALUES('" & Nz(Rs!Serie_Numerar) & "','3','" & Proveedor & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs3.Close()
                                        Case 16 'MANTENIMIENTO EN PROVEEDORES
                                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_EQUIPOS_MANTENIMIENTOS WHERE SALIDA_NRO ='" & lblCodigo.Text.Trim & "'"
                                            Rs3 = CmdGlobal3.ExecuteReader
                                            If Rs3.HasRows Then
                                                While Rs3.Read
                                                    CmdGlobal4.CommandText = " UPDATE TBINV_EQUIPOS_MANTENIMIENTOS SET MANTEN_ESTADO ='1',MANTEN_SYS_MOD='" & ValorSys & "',MANTEN_FECHA='" & FechaSal & "' WHERE SALIDA_NRO='" & lblCodigo.Text.Trim & "' AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND MANTEN_SYS_EST='0'"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='3',UBICACT_CODIGO='" & Proveedor & "',UBICACT_SYS='" & ValorSys & "' WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                          & " VALUES('" & Nz(Rs!Serie_Numerar) & "','3','" & Proveedor & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs3.Close()
                                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_AVERIA WHERE SALIDA_NRO_ALM ='" & lblCodigo.Text.Trim & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1 IN ('0','2') AND AVERIA_ESTADO_2='1' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND AVERIA_SERIE_NUMERAR='" & Nz(Rs!Serie_Numerar) & "'"
                                            Rs3 = CmdGlobal3.ExecuteReader
                                            If Rs3.HasRows Then
                                                While Rs3.Read
                                                    CmdGlobal4.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='3', AVERIA_SYS_MOD='" & ValorSys & "' WHERE AVERIA_NRO=" & Nz(Rs3!AVERIA_NRO) & " AND SALIDA_NRO_ALM='" & lblCodigo.Text.Trim & "' AND AVERIA_SYS_EST ='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'  AND AVERIA_SERIE_NUMERAR='" & Nz(Rs!Serie_Numerar) & "'"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs3.Close()
                                        Case 25 'DEVOLUCION DEFINITIVA 'SERIE_ESTADO=2 DEVUELTOS devol_estado=1 recibido
                                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_EQUIPOS_DEVUELTOS WHERE SALIDA_NRO ='" & lblCodigo.Text.Trim & "'"
                                            Rs3 = CmdGlobal3.ExecuteReader
                                            If Rs3.HasRows Then
                                                While Rs3.Read
                                                    CmdGlobal4.CommandText = " UPDATE TBINV_EQUIPOS_DEVUELTOS SET DEVOL_ESTADO ='1',DEVOL_SYS_MOD='" & ValorSys & "',DEVOL_FECHA='" & FechaSal & "' WHERE SALIDA_NRO='" & lblCodigo.Text.Trim & "'"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='3',UBICACT_CODIGO='" & Proveedor & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = 'N',SERIE_ESTADO='2' WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar)
                                                    CmdGlobal4.ExecuteNonQuery()
                                                    CmdGlobal4.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                          & " VALUES('" & Nz(Rs!Serie_Numerar) & "','3','" & Proveedor & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                                    CmdGlobal4.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs3.Close()
                                        Case "26"
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                                                  & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                                                  & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nz(Rs!Serie_Numerar) & ",'" & ValorSys & "'," _
                                                                  & " '" & ValorSys & "','2','1','0')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                  & " VALUES('" & Nz(Rs!Serie_Numerar) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & IIf(Nu(Rs2!DESP_TIPODESTINO) = "5", CodPersona, Cliente) & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & IIf(Nu(Rs2!DESP_TIPODESTINO) = "5", CodPersona, Cliente) & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                            CmdGlobal3.ExecuteNonQuery()
                                        Case "27"
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                                                  & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                                                  & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nz(Rs!Serie_Numerar) & ",'" & ValorSys & "'," _
                                                                  & " '" & ValorSys & "','2','1','0')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                  & " VALUES('" & Nz(Rs!Serie_Numerar) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & IIf(Nu(Rs2!DESP_TIPODESTINO) = "5", CodPersona, Cliente) & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & IIf(Nu(Rs2!DESP_TIPODESTINO) = "5", CodPersona, Cliente) & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL, FECHA_SALIDA_G = '" & FechaSal & "' WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                            CmdGlobal3.ExecuteNonQuery()
                                        Case "33"
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = " INSERT TBINV_ALQUILER_DETALLE(EMPRESA_CODIGO,ALQUI_CODIGO, ALQUIDET_CODIGO,SERIE_NUMERAR, ALQUIDET_SYS_REG,ALQUIDET_ESTADO_ENVIO, ALQUIDET_SYS_ENVIO, ALQUIDET_ESTADO_ALQUILER, " _
                                                                  & " ALQUIDET_SYS_ALQUILER, ALQUIDET_FECHA_XDEVOLVER,ALQUIDET_SYS_DEVOLUCION ) " _
                                                                  & " VALUES('" & Session("CodEmpresa") & "'," & NroAlquiler & "," & ItemAlquiler & "," & Nz(Rs!Serie_Numerar) & ",'" & ValorSys & "','1','" & ValorSys & "','1'," _
                                                                  & " '" & ValorSys & "','" & FechaDevol & "',NULL)"
                                            CmdGlobal3.ExecuteNonQuery()
                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                  & " VALUES('" & Nz(Rs!Serie_Numerar) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "','" & CodDestino & "','" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, "1", Nu(Rs2!ALMACEN_ORIGEN), Nu(Rs2!DESP_TIPODESTINO), CodDestino, Nz(Rs!Serie_Numerar), Session("User"))
                                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & CodDestino & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                            CmdGlobal3.ExecuteNonQuery()
                                        Case 34
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                                                  & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                                                  & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nz(Rs!Serie_Numerar) & ",'" & ValorSys & "'," _
                                                                  & " '" & ValorSys & "','2','1','0')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                  & " VALUES('" & Nz(Rs!Serie_Numerar) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodDestino & ",'" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, "1", Nu(Rs2!ALMACEN_ORIGEN), Nu(Rs2!DESP_TIPODESTINO), CodDestino, Nz(Rs!Serie_Numerar), Session("User"))
                                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & CodDestino & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                            CmdGlobal3.ExecuteNonQuery()
                                        Case Else
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                                                  & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                                                  & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nz(Rs!Serie_Numerar) & ",'" & ValorSys & "'," _
                                                                  & " '" & ValorSys & "','2','1','0')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                                                  & " VALUES('" & Nz(Rs!Serie_Numerar) & "','" & Nu(Rs2!DESP_TIPODESTINO) & "'," & CodDestino & ",'" & psCodMotivo & "','" & ValorSys & "','0','" & FechaServer & "','1','" & lblCodigo.Text.Trim & "','" & psCodMotivo & "')"
                                            CmdGlobal3.ExecuteNonQuery()
                                            objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, "1", Nu(Rs2!ALMACEN_ORIGEN), Nu(Rs2!DESP_TIPODESTINO), CodDestino, Nz(Rs!Serie_Numerar), Session("User"))
                                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!DESP_TIPODESTINO) & "',UBICACT_CODIGO='" & CodDestino & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL WHERE SERIE_NUMERAR=" & Nz(Rs!Serie_Numerar) 'TIPO 0: EN TRANSITO
                                            CmdGlobal3.ExecuteNonQuery()
                                    End Select
                                End If
                            End While
                        End If
                        Rs.Close()
                        ':::::::::::::::::::::::::::::::::::::::::::::::::: ARTICULOS Q NO USAN SERIE
                        Dim CantPorDevolver As Long = 0
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_PARATRANSITO = ISNULL(SAA_PARATRANSITO,0) - " & Nu(Rs!DESPD_CANT_DESP) & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND ARTICULO_CODIGO =" & Nu(Rs!ARTICULO_CODIGO) & " AND (UBICACT_TIPO='1') AND (ALMACEN_CODIGO=" & Nu(Rs2!ALMACEN_ORIGEN) & ")"
                                CmdGlobal3.ExecuteNonQuery()
                                '-----------------------------------------------------
                                ItemPrestamo = ItemPrestamo + 1 'continua d arriba
                                ItemAllSal = ItemAllSal + 1
                                Select Case psCodMotivo
                                    Case "1" 'prestamo
                                        'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                        CmdGlobal3.CommandText = "INSERT TBINV_PRESTAMO_DETALLE_SINSERIE(EMPRESA_CODIGO,PRESTA_CODIGO, PREDET_CODIGO,ARTICULO_CODIGO, PREDET_SYS_REGISTRO, PREDET_ESTADO_ENVIO, PREDET_SYS_ENVIO, PREDET_ESTADO_PRESTAMO, " _
                                                              & " PREDET_SYS_PRESTAMO, PREDET_FECHA_PORDEVOLVER, PREDET_SYS_DEVOLUCION,PREDET_CANTXPRESTAR,PREDET_CANT_PRESTADA,PREDET_CANT_FALT_DEVOLVER,PREDET_CANT_DEVUELTA) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & NroPrestamo & "," & ItemPrestamo & "," & Nu(Rs!ARTICULO_CODIGO) & ",'" & ValorSys & "','0','" & ValorSys & "','0'," _
                                                              & " NULL,'" & FechaDevol & "',NULL," & Nz(Rs!DESPD_CANT_DESP) & ",0,0,0)"
                                        CmdGlobal3.ExecuteNonQuery()
                                        If Nu(Rs2!DESP_TIPODESTINO) = "6" Then
                                            CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_CANT_PRESTADA = PREDET_CANTXPRESTAR, PREDET_CANT_XDEVOLVER = 0, PREDET_CANT_FALT_DEVOLVER = PREDET_CANTXPRESTAR, PREDET_CANT_DEVUELTA = 0, PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1', PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE_SINSERIE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND B.DESP_CODIGO=" & lblCodigo.Text.Trim & " AND A.ARTICULO_CODIGO =" & Nu(Rs!ARTICULO_CODIGO)
                                            CmdGlobal3.ExecuteNonQuery()
                                        End If
                                    Case 2
                                    Case 3 'devolucion por prestamo
                                        'colocar el prestamo Por Devolver(2) solo la primera vez, si la devol. del prestamo no es total se quedara con el estado Devuelto Parcial(4) en recepcion
                                        If Nu(Rs2!DESP_TIPODESTINO) = "1" Then 'almacen
                                            CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_ESTADO_PRESTAMO WHEN '1' THEN '2' ELSE PREDET_ESTADO_PRESTAMO END), " _
                                                                 & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) + " & Nz(Rs!DESPD_CANT_DESP) & ", " _
                                                                 & " OSAL_CODIGO_DEVOL = NULL,DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ",RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE_SINSERIE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                 & " AND (B.PREDET_ESTADO_PRESTAMO IN ('1','2','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & CodAlmacen & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ")"
                                            CmdGlobal3.ExecuteNonQuery()
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "2" Then 'ccosto
                                            CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_ESTADO_PRESTAMO WHEN '1' THEN '2' ELSE PREDET_ESTADO_PRESTAMO END), " _
                                                                 & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) + " & Nz(Rs!DESPD_CANT_DESP) & ", " _
                                                                 & " OSAL_CODIGO_DEVOL = NULL, DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ", RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                 & " AND (B.PREDET_ESTADO_PRESTAMO IN ('1','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & CodSeccion & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ")"
                                            CmdGlobal3.ExecuteNonQuery()
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "3" Then 'proveedor
                                            CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_ESTADO_PRESTAMO WHEN '1' THEN '2' ELSE PREDET_ESTADO_PRESTAMO END), " _
                                                                 & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) + " & Nz(Rs!DESPD_CANT_DESP) & ", " _
                                                                 & " OSAL_CODIGO_DEVOL = NULL, DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ", RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                 & " AND (B.PREDET_ESTADO_PRESTAMO IN ('1','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '3') AND (A.PROVEEDOR_CODIGO_ORIGEN =" & Proveedor & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ")"
                                            CmdGlobal3.ExecuteNonQuery()
                                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "6" Then 'Cliente
                                            CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_ESTADO_PRESTAMO WHEN '1' THEN '2' ELSE PREDET_ESTADO_PRESTAMO END), " _
                                                                 & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) + " & Nz(Rs!DESPD_CANT_DESP) & ", " _
                                                                 & " OSAL_CODIGO_DEVOL = NULL, DESP_CODIGO_DEVOL = " & lblCodigo.Text.Trim & ", RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                                 & " AND (B.PREDET_ESTADO_PRESTAMO IN ('1','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '6') AND (A.PROVEEDOR_CODIGO_ORIGEN =" & Cliente & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & Nu(Rs2!ALMACEN_ORIGEN) & ") AND (B.ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ")"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End If
                                    Case 4
                                        'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                        CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                                                              & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nu(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & "," _
                                                              & " " & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & ",0,'2','1','0')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Case 6
                                        CmdGlobal3.CommandText = " UPDATE TBINV_REEMPLAZOS_SINSERIE SET REEMSIN_ESTADO_1='1', REEMSIN_ESTADO_2='1', REEMSIN_SYS_MOD='" & ValorSys & "' WHERE SALIDA_ALM ='" & lblCodigo.Text.Trim & "' AND ART_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & " " _
                                                              & " AND REEMSIN_ESTADO_1='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND REEMSIN_SYS_EST='0'"
                                        CmdGlobal3.ExecuteNonQuery()
                                        CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                                                              & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nu(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & "," _
                                                              & " " & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & ",0,'2','1','0')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Case 25 'DEVOLUCION DEFINITIVA 'SERIE_ESTADO=2 DEVUELTOS devol_estado=1 recibido
                                        CmdGlobal3.CommandText = "SELECT * FROM TBINV_ACCESORIOS_DEVUELTOS WHERE SALIDA_NRO ='" & lblCodigo.Text.Trim & "'"
                                        Rs3 = CmdGlobal3.ExecuteReader
                                        If Rs3.HasRows Then
                                            While Rs3.Read
                                                CmdGlobal4.CommandText = " UPDATE TBINV_ACCESORIOS_DEVUELTOS SET DEVOL_ESTADO ='1',DEVOL_SYS_MOD='" & ValorSys & "',DEVOL_FECHA='" & Format(txtFecha.Text.Trim, "yyyymmdd") & "' WHERE SALIDA_NRO='" & lblCodigo.Text.Trim & "'"
                                                CmdGlobal4.ExecuteNonQuery()
                                            End While
                                        End If
                                        Rs3.Close()
                                    Case "26"
                                        'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                        CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                                                              & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nu(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & "," _
                                                              & " " & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & ",0,'2','1','0')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Case "27"
                                        'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                        CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                                                              & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nu(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & "," _
                                                              & " " & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & ",0,'2','1','0')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Case "33"
                                        'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                        CmdGlobal3.CommandText = " INSERT TBINV_ALQUILER_DETALLE_SINSERIE(EMPRESA_CODIGO,ALQUILER_CODIGO, ALQUIDET_CODIGO,ARTICULO_CODIGO, ALQUIDET_SYS_REG, ALQUIDET_ESTADO_ENVIO, ALQUIDET_SYS_ENVIO, ALQUIDET_ESTADO_ALQUILER, " _
                                                              & " ALQUIDET_SYS_ALQUILER, ALQUIDET_FECHA_XDEVOLVER, ALQUIDET_SYS_DEVOLUCION,ALQUIDET_CANT_XALQUILAR,ALQUIDET_CANT_ALQUILADA,ALQUIDET_CANT_FALT_DEVOLVER,ALQUIDET_CANT_DEVUELTA) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & NroAlquiler & "," & ItemAlquiler & "," & Nu(Rs!ARTICULO_CODIGO) & ",'" & ValorSys & "','1','" & ValorSys & "','1'," _
                                                              & " '" & ValorSys & "','" & FechaDevol & "',NULL," & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & ",0)"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Case Else
                                        'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                        CmdGlobal3.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                                                              & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                                                              & " VALUES('" & Session("CodEmpresa") & "'," & CodAllSal & "," & ItemAllSal & "," & Nu(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & "," _
                                                              & " " & Nz(Rs!DESPD_CANT_DESP) & "," & Nz(Rs!DESPD_CANT_DESP) & ",0,'2','1','0')"
                                        CmdGlobal3.ExecuteNonQuery()
                                End Select
                            End While
                        End If
                        Rs.Close()
                        '=============================================================RECEPCION AUTOMATICA
                        Dim RecepAuto As Boolean
                        RecepAuto = False
                        'saber si el movimiento es directo
                        If Nu(Rs2!DESP_TIPODESTINO) = "1" Then 'almacen
                            CmdGlobal.CommandText = " SELECT AL.ALMACEN_CODIGO, AL.ALMACEN_NOMBRE, AL.CECOSE_CODIGO, AL.ALMACEN_MODO " _
                                & " FROM dbo.TBINV_ALMACENES AL " _
                                & " WHERE (AL.ALMACEN_SYS_EST = '0') AND (AL.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (AL.ALMACEN_CODIGO = '" & Nu(Rs2!ALMACEN_CODIGO_DESTINO) & "')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    If Nu(Rs!ALMACEN_MODO) = "1" Then RecepAuto = True
                                End While
                            End If
                            Rs.Close()
                        ElseIf Nu(Rs2!DESP_TIPODESTINO) = "2" Then 'ccosto
                            CmdGlobal3.CommandText = "SELECT A.CECOSE_CODIGO, A.CECOSE_EDIFICIO FROM TBLOGIS_CENTRO_COSTO_SECCION A WHERE CECOSE_MODO_RECIBIR = '1' AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (A.CECOSE_CODIGO = '" & Nu(Rs2!CECOSE_CODIGO_DESTINO) & "') "
                            Rs3 = CmdGlobal3.ExecuteReader
                            If Rs3.HasRows Then
                                While Rs3.Read
                                    RecepAuto = True
                                End While
                            End If
                            Rs3.Close()
                            If RecepAuto = False Then RecepAuto = True
                        End If
                        If RecepAuto = True Then
                            Dim SerieEncontrada As String
                            SerieEncontrada = 0
                            CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO_DET WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & lblCodigo.Text.Trim
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal3.CommandText = "SELECT SERIE_NUMERAR_REEMPLAZADO FROM TBINV_REEMPLAZOS WHERE REEM_SYS_EST ='0' AND NRO_SALIDA_ALM='" & lblCodigo.Text.Trim & "' AND SERIE_NUMERAR_REEMPLAZANTE= '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND NOT(SERIE_NUMERAR_REEMPLAZADO IS NULL) AND (SERIE_NUMERAR_REEMPLAZADO<>'')"
                                    Rs3 = CmdGlobal3.ExecuteReader
                                    If Rs3.HasRows Then
                                        While Rs3.Read
                                            SerieEncontrada = 1
                                        End While
                                    Else
                                        SerieEncontrada = 0
                                    End If
                                    Rs3.Close()
                                End While
                            End If
                            Rs.Close()
                            If SerieEncontrada = 1 Then
                                objProceso.RecepcionAutomatica(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), "1", Nu(Rs2!ALMACEN_ORIGEN), "", Nu(Rs2!DESP_TIPODESTINO), Nu(Rs2!ALMACEN_CODIGO_DESTINO), Nu(Rs2!CECOSE_CODIGO_DESTINO), lblCodigo.Text.Trim, Nu(Rs2!DESP_MOTIVO_GRAL), txtFecha.Text)
                            ElseIf Nu(Rs2!DESP_MOTIVO_GRAL) <> 6 And Nu(Rs2!DESP_MOTIVO_GRAL) <> 11 Then
                                objProceso.RecepcionAutomatica(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), "1", Nu(Rs2!ALMACEN_ORIGEN), "", Nu(Rs2!DESP_TIPODESTINO), Nu(Rs2!ALMACEN_CODIGO_DESTINO), Nu(Rs2!CECOSE_CODIGO_DESTINO), lblCodigo.Text.Trim, Nu(Rs2!DESP_MOTIVO_GRAL), txtFecha.Text)
                            End If
                        End If
                    End If
                End While
            End If
            ''====================================
            '
            Session("CodSalida") = lblCodigo.Text.Trim
            LblTituloModal.Text = "Nro. Salida de Almacén " & Llenar_Ceros(Session("CodSalida"), 6)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModalGuia').modal('show');", True)

            lblCodigo.Text = ""
            lblFecha.Text = FormatoFecha(FechaActual)
            lblHora.Text = FormatoHora(HoraActual)
            If txtFecha.Text.Trim = "" Then txtFecha.Text = lblFecha.Text
            If txtHora.Text.Trim = "" Then txtFecha.Text = lblHora.Text
            txtDesCodExterno.Text = ""
            txtOrigCodExt.Text = ""
            lblCodOrigen.Text = ""
            lblCodDestino.Text = ""
            txtDesDescrip.Text = ""
            txtOrigDescrip.Text = ""
            txtPerEnvia.Text = ""
            txtObs.Text = ""
            lblUsuario.Text = User.Identity.Name
            Call Carga_Motivos()
            lblFechaDevol.Visible = False
            txtFechaDevol.Visible = False
            Call FlexBusBlanco(GvBusEquipo.ID)
            Call FlexBusBlanco(_DetalleEq.ID)
            Call FlexBusBlanco(GvBusAcc.ID)
            Call FlexBusBlanco(_DetalleAc.ID)
            Session("ArrayEq") = String.Empty
            Session("ArrayAc") = String.Empty
            Session("CountArrayEq") = "-1"
            Session("CountArrayAc") = "-1"
            Session("TipoDestino") = "Almacen"

            Session("OrigenDescrip") = String.Empty
            Session("OrigenCodExt") = String.Empty
            Session("OrigenCodigo") = String.Empty
            Session("DestinoDescrip") = String.Empty
            Session("DestinoCodExt") = String.Empty
            Session("DestinoCodigo") = String.Empty
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)

        Finally
            Cn.Close()
            Cn2.Close()
        End Try
    End Sub

    Protected Sub btnRedirectYes_Click(sender As Object, e As EventArgs) Handles btnRedirectYes.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        If Session("CodSalida") = "" Then Exit Sub
        Session("TipoGuia") = "1"
        Cn.Open() : CmdGlobal.Connection = Cn

        Session("TipoSalida") = "1"
        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_TIPO_DOC_SALIDA = '1' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & Session("CodSalida")
        CmdGlobal.ExecuteNonQuery()


        Dim valor As String = Session("CodSalida")
        Dim valor2 As String = Session("TipoSalida")
        Session("PaginaViene") = "Inventario_Almacen_Salida.aspx"
        Session("ProcesoEjecutado") = Nothing
        ' Redireccionar a la página de destino pasando el valor como parámetro de consulta en la URL
        Response.Redirect("~/Inventario/Inventario_GenerarGuia.aspx?parametro=" & Server.UrlEncode(valor) & "&parametro2=" & Server.UrlEncode(valor2))

    End Sub

    Protected Sub btnRedirectNo_Click(sender As Object, e As EventArgs) Handles btnRedirectNo.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        If Session("CodSalida") = "" Then Exit Sub
        Session("TipoGuia") = "2"
        Cn.Open() : CmdGlobal.Connection = Cn
        Session("TipoSalida") = "1"
        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_TIPO_DOC_SALIDA = '2' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & Session("CodSalida")
        CmdGlobal.ExecuteNonQuery()

        Session("ProcesoEjecutado") = Nothing
        'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModalGuia').modal('hide');", True)

        Dim valor As String = Session("CodSalida")
        Dim valor2 As String = Session("TipoSalida")
        Session("PaginaViene") = "Inventario_Almacen_Salida.aspx"
        ' Redireccionar a la página de destino pasando el valor como parámetro de consulta en la URL
        Response.Redirect("~/Inventario/Inventario_GenerarGuia.aspx?parametro=" & Server.UrlEncode(valor) & "&parametro2=" & Server.UrlEncode(valor2))

    End Sub

    Protected Sub _Ubica1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Ubica1.Click
        lblError.Text = ""
        lblEtq_BusDestino.Text = "Busqueda de Almacén"
        Session("TipoBus") = "Origen"
        txtOrigDescrip.Text = ""
        txtOrigCodExt.Text = ""
        lblCodOrigen.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub

    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim psConexion As String = Session("Ruta_Emp")
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            Dim pdCodAlmacen As Double = 0
            If txtBusCod.Text <> "" Then pdCodAlmacen = txtBusCod.Text
            If Session("TipoBus") = "Origen" Then
                FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            Else
                If Session("TipoDestino") = "CentroCosto" Then
                    FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
                    FlexUbicacion.DataBind()
                ElseIf Session("TipoDestino") = "Almacen" Then
                    If txtBusCod.Text = "" Then pdCodAlmacen = 0 Else pdCodAlmacen = txtBusCod.Text
                    FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusDescripcion.Text.Trim)
                    FlexUbicacion.DataBind()
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)

        Finally
        End Try
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            If Session("TipoBus") = "Origen" Then
                lblCodOrigen.Text = ""
                txtOrigCodExt.Text = ""
                txtOrigDescrip.Text = ""
                Session("OrigenCodExt") = FlexUbicacion.Rows(Index).Cells(1).Text
                Session("OrigenDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                Session("OrigenCodigo") = FlexUbicacion.Rows(Index).Cells(3).Text
                lblCodOrigen.Text = Session("OrigenCodigo")
                txtOrigCodExt.Text = Session("OrigenCodExt")
                txtOrigDescrip.Text = Session("OrigenDescrip")
                FlexUbicacion.DataSource = Nothing
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
                FlexUbicacion.DataBind()
            ElseIf Session("TipoBus") = "Destino" Then
                txtDesDescrip.Text = ""
                txtDesCodExterno.Text = ""
                lblCodDestino.Text = ""
                Session("DestinoCodExt") = FlexUbicacion.Rows(Index).Cells(1).Text
                Session("DestinoDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                Session("DestinoCodigo") = FlexUbicacion.Rows(Index).Cells(3).Text
                txtDesDescrip.Text = Session("DestinoDescrip")
                txtDesCodExterno.Text = Session("DestinoCodExt")
                lblCodDestino.Text = Session("DestinoCodigo")
                FlexUbicacion.DataSource = Nothing
                FlexUbicacion.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
            End If
        End If
    End Sub
    Protected Sub _Ubica2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Ubica2.Click
        If txtOrigCodExt.Text = "" And lblCodOrigen.Text = "" Then lblError.Text = "Debe ingresar el Origen." : Exit Sub
        If Session("TipoDestino") = "Almacen" Then
            lblEtq_BusDestino.Text = "Busqueda de Almacén"
        ElseIf Session("TipoDestino") = "CentroCosto" Then
            lblEtq_BusDestino.Text = "Busqueda de Centro de Costos"
        End If
        Session("TipoBus") = "Destino"
        txtDesDescrip.Text = ""
        txtDesCodExterno.Text = ""
        lblCodDestino.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub
    Protected Sub btnUbiCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        Session("TipoDestino") = "Almacen"
        Call Carga_Motivos()
        txtDesCodExterno.Text = ""
        txtDesDescrip.Text = ""
        lblCodDestino.Text = ""
        Session("DestinoDescrip") = ""
        Session("DestinoCodExt") = ""
        lblFechaDevol.Visible = False
        txtFechaDevol.Visible = False
        Call FlexBusBlanco(GvBusEquipo.ID)
        Call FlexBusBlanco(_DetalleEq.ID)
        Call FlexBusBlanco(GvBusAcc.ID)
        Call FlexBusBlanco(_DetalleAc.ID)
        Session("ArrayEq") = String.Empty
        Session("ArrayAc") = String.Empty
        Session("CountArrayEq") = "-1"
        Session("CountArrayAc") = "-1"
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        Session("TipoDestino") = "CentroCosto"
        Call Carga_Motivos()
        txtDesCodExterno.Text = ""
        txtDesDescrip.Text = ""
        lblCodDestino.Text = ""
        Session("DestinoDescrip") = ""
        Session("DestinoCodExt") = ""
        lblFechaDevol.Visible = False
        txtFechaDevol.Visible = False
        Call FlexBusBlanco(GvBusEquipo.ID)
        Call FlexBusBlanco(_DetalleEq.ID)
        Call FlexBusBlanco(GvBusAcc.ID)
        Call FlexBusBlanco(_DetalleAc.ID)
        Session("ArrayEq") = String.Empty
        Session("ArrayAc") = String.Empty
        Session("CountArrayEq") = "-1"
        Session("CountArrayAc") = "-1"
    End Sub

    Private Sub BtnAgregarEq_Click(sender As Object, e As EventArgs) Handles BtnAgregarEq.Click

        If lblCodOrigen.Text.ToString = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Origen');", True)
        ElseIf lblCodDestino.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Destino');", True)
        ElseIf cboMotivo.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Motivo');", True)
        Else

            Session("Busqueda") = ""
            txtSerieArt.Visible = True
            txtPlaca.Visible = True
            Label10.Visible = True
            Label11.Visible = True
            Call Limpiar()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticuloEq').modal('show');", True)
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusEquipo').modal('show');", True)
        End If

    End Sub

    Private Sub _CerrarEq_Click(sender As Object, e As EventArgs) Handles _CerrarEq.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusEquipo').modal('hide');", True)
    End Sub

    Private Sub BtnAgregarAcc_Click(sender As Object, e As EventArgs) Handles BtnAgregarAcc.Click
        If lblCodOrigen.Text.ToString = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Origen');", True)
        ElseIf lblCodDestino.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Destino');", True)
        ElseIf cboMotivo.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Motivo');", True)
        Else
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusAcc').modal('show');", True)
            Session("Busqueda") = "Accesorio"
            txtSerieArt.Visible = False
            txtPlaca.Visible = False
            Label10.Visible = False
            Label11.Visible = False
            Call Limpiar()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticuloEq').modal('show');", True)
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticuloEq').modal('show');", True)
    End Sub

    Private Sub _CerrarAc_Click(sender As Object, e As EventArgs) Handles _CerrarAc.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusAcc').modal('hide');", True)
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticuloEq').modal('hide');", True)
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Dim Sql As String = ""
        Dim Sql2 As String = ""
        lblCountBusEq.Text = "Registros 0"

        If txtCodigoArt.Text.Trim <> "" Then
            If IsNumeric(txtCodigoArt.Text.Trim) Then
                txtCodigoArt.Text = Format(CLng(txtCodigoArt.Text), "00000000")
            End If
        End If
        Dim psCodArtSku As String = ""
        Dim psSku As String = ""
        If TxtSku.Value <> "" Then
            psSku = TxtSku.Value
        End If
        Dim psDescripcion As String = ""
        Try
            If psSku <> "" Then

                Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim CmdGlobal2 As New SqlCommand
                Cn3.Open() : CmdGlobal.Connection = Cn3
                Cn2.Open() : CmdGlobal2.Connection = Cn2
                Dim Rs As SqlDataReader

                CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS WHERE UPPER(ART_SKU) = '" & UCase(psSku) & "'  "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodArtSku = Nu(Rs("ART_CODIGO"))
                        psDescripcion = Nu(Rs("ART_DESCRIPCION"))
                        TxtDescripcionBA.Value = Nu(Rs("ART_DESCRIPCION"))
                    End While
                End If
                Rs.Close()
                If psCodArtSku = "" Then

                    CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS_IMAGENES WHERE ART_SKU = '" & psSku & "'  "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psDescripcion = Nu(Rs("ART_DESCRIPCION"))
                            TxtDescripcionBA.Value = Nu(Rs("ART_DESCRIPCION"))
                        End While
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS WHERE UPPER(ART_DESCRIPCION) = '" & UCase(TxtDescripcionBA.Value) & "'  "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodArtSku = Nu(Rs("ART_CODIGO"))
                            CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS SET ART_SKU = '" & psSku & "' WHERE ART_CODIGO =  " & psCodArtSku
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    Rs.Close()
                End If
            End If

            Select Case cboMotivo.SelectedValue
                Case "3" 'DEVOLUCION POR PRESTAMO
                    'listar los prestados q faltan devolver

                    If Session("Busqueda") = "Accesorio" Then
                        Sql = " SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_SKU, A.ART_DESCRIPCION, SUM(ISNULL(PREDET_CANT_FALT_DEVOLVER,0)-ISNULL(PREDET_CANT_XDEVOLVER,0)) AS  STOCK_ACTUAL, ART_CODEQUIVA, " _
                            & " (SELECT ELEMENTO_DESCRIPCION FROM TBINV_TABLAS_INFO I WHERE ELEMENTO_CODUNICO = ART_TIPO AND I.EMPRESA_CODIGO  = A.EMPRESA_CODIGO) AS TIPO_ART,ART_TIPO " _
                            & " FROM TBINV_PRESTAMO C INNER JOIN TBINV_PRESTAMO_DETALLE_SINSERIE D ON C.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND C.PRESTA_CODIGO = D.PRESTA_CODIGO " _
                            & " INNER JOIN TBINV_STOCK_SINSERIE_CCOSTO S ON S.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND S.ARTICULO_CODIGO = D.ARTICULO_CODIGO AND S.CECOSE_CODIGO = C.CECOSE_CODIGO_DESTINO INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO AND S.EMPRESA_CODIGO = A.EMPRESA_CODIGO " _
                            & " inner join dbo.TBINV_ARTICULO_CLASIFICACION AC ON AC.CLAS_CODIGO = A.ART_CLASIFICACION " _
                            & " WHERE  " _
                            & " (D.PREDET_ESTADO_PRESTAMO IN ('1','2','4')) AND (C.PRESTA_TIPO_MOVIMIENTO = 'S') AND (C.PRESTA_TIPODESTINO = '1')"
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '1') AND (C.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        ElseIf RBCentroC.Checked = True Then
                            Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '2') AND (C.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        End If
                        Sql = Sql & " GROUP BY A.EMPRESA_CODIGO, C.CECOSE_CODIGO_DESTINO, D.ARTICULO_CODIGO, A.ART_DESCRIPCION HAVING (C.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ") AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') "
                    Else
                        Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_SKU,  ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION, S.SERIE_NRO, S.PLACA_NRO,LTRIM(RTRIM(STR(S.SERIE_NUMERAR))) AS SERIE_NUMERAR, ART_CODEQUIVA," _
                            & " (SELECT ELEMENTO_DESCRIPCION FROM TBINV_TABLAS_INFO I WHERE ELEMENTO_CODUNICO = ART_TIPO AND I.EMPRESA_CODIGO  = A.EMPRESA_CODIGO) AS TIPO_ART,ART_TIPO,'' AS REEN_NUMERO, '' AS AVERIA, '1'  AS STOCK_ACTUAL " _
                            & " FROM TBINV_PRESTAMO C INNER JOIN TBINV_PRESTAMO_DETALLE D ON C.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND C.PRESTA_CODIGO = D.PRESTA_CODIGO INNER JOIN " _
                            & " TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO ON C.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND  D.SERIE_NUMERAR = S.SERIE_NUMERAR " _
                            & " AND C.CECOSE_CODIGO_DESTINO = S.UBICACT_CODIGO AND  C.PRESTA_TIPODESTINO = S.UBICACT_TIPO " _
                            & " inner join dbo.TBINV_ARTICULO_CLASIFICACION AC ON AC.CLAS_CODIGO = A.ART_CLASIFICACION " _
                            & " WHERE (S.UBICACT_CODIGO = " & lblCodOrigen.Text & ") AND (S.SERIE_SYS_EST = '0') AND (S.UBICACT_TIPO = '1') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND " _
                            & " (D.PREDET_ESTADO_PRESTAMO = '1') AND (C.PRESTA_TIPO_MOVIMIENTO = 'S')"
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '1') AND (C.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        ElseIf RBCentroC.Checked = True Then
                            Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '2') AND (C.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        End If
                    End If
                Case "12" 'DEVOLUCION REEMPLAZO POR CAMBIO
                    'LISTAR LOS REEMPLAZOS QUE FALTAN DEVOLVER TIPO 1
                    If Session("Busqueda") = "Accesorio" Then
                        Sql = " SELECT  RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_SKU, A.ART_DESCRIPCION, SUM(ISNULL(RS.REEMSIN_CANT_FALT_DEVOLVER, 0) - ISNULL(RS.REEMSIN_CANT_XDEVOLVER, 0)) AS STOCK_ACTUAL, ART_CODEQUIVA,   " _
                            & " (SELECT ELEMENTO_DESCRIPCION FROM TBINV_TABLAS_INFO I WHERE ELEMENTO_CODUNICO = ART_TIPO AND I.EMPRESA_CODIGO  = A.EMPRESA_CODIGO) AS TIPO_ART, ART_TIPO " _
                            & " FROM TBINV_ARTICULOS A INNER JOIN TBINV_REEMPLAZOS_SINSERIE RS ON A.EMPRESA_CODIGO = RS.EMPRESA_CODIGO AND A.ART_CODIGO = RS.ART_CODIGO INNER JOIN " _
                            & " TBINV_STOCK_ARTICULOS_ALMACEN D ON A.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND " _
                            & " Rs.ART_CODIGO = D.ARTICULO_CODIGO And Rs.REEMSIN_COD_DESTINO = D.ALMACEN_CODIGO And Rs.REEMSIN_TIPO_DESTINO = D.UBICACT_TIPO " _
                            & " inner join dbo.TBINV_ARTICULO_CLASIFICACION AC ON AC.CLAS_CODIGO = A.ART_CLASIFICACION " _
                            & " WHERE (RS.REEMSIN_ESTADO_2 IN ('1','2','4')) AND (RS.REEMSIN_TIPO_DESTINO = '2') AND (RS.REEMSIN_ESTADO_1 = '1')"
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql = Sql & " AND (RS.REEMSIN_TIPO_ORIGEN = '1') AND (RS.REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ")"
                        ElseIf RBCentroC.Checked = True Then
                            Sql = Sql & " AND (RS.REEMSIN_TIPO_ORIGEN = '2') AND (RS.REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ")"
                        End If
                        Sql = Sql & " GROUP BY A.EMPRESA_CODIGO,RS.REEMSIN_COD_DESTINO,A.ART_DESCRIPCION,D.ARTICULO_CODIGO HAVING (RS.REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ") AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                    Else
                        Sql = " SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_SKU, ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION,S.SERIE_NRO,S.PLACA_NRO, LTRIM(RTRIM(STR(R.SERIE_NUMERAR_REEMPLAZANTE))) AS SERIE_NUMERAR,ART_CODEQUIVA," _
                            & " (SELECT ELEMENTO_DESCRIPCION FROM TBINV_TABLAS_INFO I WHERE ELEMENTO_CODUNICO = ART_TIPO AND I.EMPRESA_CODIGO  = A.EMPRESA_CODIGO) AS TIPO_ART,ART_TIPO,LTRIM(RTRIM(STR(REEM_NRO))) AS REEN_NUMERO, '' AS AVERIA, " _
                            & " R.REEM_TIPO_DESTINO, R.REEM_CODIGO_DESTINO, R.REEM_TIPO_ORIGEN, R.REEM_CODIGO_ORIGEN, R.REEM_TIPO, '1'  AS STOCK_ACTUAL " _
                            & " FROM dbo.TBINV_REEMPLAZOS R INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON R.SERIE_NUMERAR_REEMPLAZANTE = S.SERIE_NUMERAR INNER JOIN" _
                            & " dbo.TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO " _
                            & " inner join dbo.TBINV_ARTICULO_CLASIFICACION AC ON AC.CLAS_CODIGO = A.ART_CLASIFICACION " _
                            & " WHERE (R.REEM_TIPO_DESTINO='1')AND (R.REEM_CODIGO_DESTINO='" & lblCodOrigen.Text & "') AND (R.REEM_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0')" _
                            & " AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (A.ART_SYS_EST = '0') AND (R.REEM_ESTADO_1 = '2') AND (R.REEM_ESTADO_2 = '1') AND (R.REEM_TIPO='1') AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '1') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        ElseIf RBCentroC.Checked = True Then
                            Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '2') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        End If
                    End If
                Case "13" 'DEVOLUCION POR AVERIA
                    'LISTAR LOS REEMPLAZOS A DEVOLVER DE TIPO 2

                    If Session("Busqueda") = "Accesorio" Then
                    Else
                        Sql = " SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_SKU, ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION, S.SERIE_NRO, S.PLACA_NRO, LTRIM(RTRIM(STR(R.SERIE_NUMERAR_REEMPLAZANTE))) AS SERIE_NUMERAR,ART_CODEQUIVA," _
                            & " LTRIM(RTRIM(STR(R.REEM_NRO))) AS REEN_NUMERO, R.REEM_TIPO,LTRIM(RTRIM(STR(R.AVERIA_NRO))) AS AVERIA," _
                            & " (SELECT ELEMENTO_DESCRIPCION FROM TBINV_TABLAS_INFO I WHERE ELEMENTO_CODUNICO = ART_TIPO AND I.EMPRESA_CODIGO  = A.EMPRESA_CODIGO) AS TIPO_ART,ART_TIPO,R.REEM_TIPO_DESTINO, R.REEM_CODIGO_DESTINO, R.REEM_TIPO_ORIGEN, R.REEM_CODIGO_ORIGEN " _
                            & " FROM dbo.TBINV_ARTICULOS A INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON A.ART_CODIGO = S.ARTICULO_CODIGO INNER JOIN " _
                            & " dbo.TBINV_REEMPLAZOS R ON S.SERIE_NUMERAR = R.SERIE_NUMERAR_REEMPLAZADO " _
                            & " inner join dbo.TBINV_ARTICULO_CLASIFICACION AC ON AC.CLAS_CODIGO = A.ART_CLASIFICACION " _
                            & " WHERE (R.REEM_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (A.ART_SYS_EST = '0') AND " _
                            & " (R.REEM_ESTADO_1 = '2') AND (R.REEM_ESTADO_2 = '1') AND (R.REEM_TIPO = '2') AND (R.REEM_TIPO_DESTINO='1') AND (R.REEM_CODIGO_DESTINO='" & lblCodOrigen.Text & "') AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '1') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        ElseIf RBCentroC.Checked = True Then
                            Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '2') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                        End If
                    End If
                Case Else
                    If Session("Busqueda") = "Accesorio" Then
                        Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_DESCRIPCION, ISNULL(D.SAA_STOCK_ACTUAL,0) " _
                            & " - ISNULL(" _
                            & " (SELECT SUM(PREDET_CANT_FALT_DEVOLVER)+SUM(PREDET_CANT_XDEVOLVER) FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE_SINSERIE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (X.PRESTA_TIPO_MOVIMIENTO = 'S' AND Y.ARTICULO_CODIGO = D.ARTICULO_CODIGO)" ' AND (Y.PREDET_ESTADO_PRESTAMO = '1')"
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql = Sql & " AND (X.PRESTA_TIPOORIGEN = '1') AND (X.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                        ElseIf RBCentroC.Checked = True Then  'Seccion CC
                            Sql = Sql & " AND (X.PRESTA_TIPOORIGEN = '2') AND (X.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                        End If
                        Sql = Sql & "),0) - ISNULL(" _
                            & " (SELECT SUM(REEMSIN_CANT_FALT_DEVOLVER)+SUM(REEMSIN_CANT_XDEVOLVER) FROM TBINV_REEMPLAZOS_SINSERIE RS WHERE D.ARTICULO_CODIGO = RS.ART_CODIGO "
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql = Sql & "  AND (REEMSIN_TIPO_ORIGEN = '1') AND (REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ") AND (REEMSIN_TIPO_DESTINO = '1') AND (REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ")"
                        ElseIf RBCentroC.Checked = True Then  'Seccion CC
                            Sql = Sql & " AND (REEMSIN_TIPO_ORIGEN = '2') AND (REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ") AND (REEMSIN_TIPO_DESTINO = '1') AND (REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ")"
                        End If
                        Sql = Sql & " ),0) AS STOCK_ACTUAL,ART_CODEQUIVA, A.ART_SKU, " _
                            & " (SELECT ELEMENTO_DESCRIPCION FROM TBINV_TABLAS_INFO I WHERE ELEMENTO_CODUNICO = ART_TIPO AND I.EMPRESA_CODIGO  = A.EMPRESA_CODIGO) AS TIPO_ART, ART_TIPO " _
                            & " FROM TBINV_STOCK_ARTICULOS_ALMACEN D INNER JOIN TBINV_ARTICULOS A ON D.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND D.ARTICULO_CODIGO = A.ART_CODIGO " _
                            & " inner join dbo.TBINV_ARTICULO_CLASIFICACION AC ON AC.CLAS_CODIGO = A.ART_CLASIFICACION " _
                            & " WHERE (D.ALMACEN_CODIGO = " & lblCodOrigen.Text & ") AND UBICACT_TIPO = '1' AND (D.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (D.SAA_SYS_EST = '0') AND (A.ART_SYS_EST = '0')  AND (A.ART_TIPO IN( 87,127,90))  " 'TIPO ACCESORIO  AND (A.ART_TIPO = 87)
                        'al stock actual le restamos los articulos q se tienen q devolver y/o falta devolver
                    Else
                        Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS  ARTICULO_CODIGO, A.ART_SKU, ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION,S.SERIE_NRO,S.PLACA_NRO, LTRIM(RTRIM(STR(S.SERIE_NUMERAR))) AS SERIE_NUMERAR, ART_CODEQUIVA," _
                            & " (SELECT ELEMENTO_DESCRIPCION FROM TBINV_TABLAS_INFO I WHERE ELEMENTO_CODUNICO = ART_TIPO AND I.EMPRESA_CODIGO  = A.EMPRESA_CODIGO) AS TIPO_ART,ART_TIPO,'' AS REEN_NUMERO, '' AS AVERIA, '1'  AS STOCK_ACTUAL " _
                            & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO" _
                            & " inner join dbo.TBINV_ARTICULO_CLASIFICACION AC ON AC.CLAS_CODIGO = A.ART_CLASIFICACION " _
                            & " WHERE (S.UBICACT_CODIGO =" & lblCodOrigen.Text & ") AND (S.SERIE_SYS_EST = '0') AND (S.UBICACT_TIPO = '1') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') "
                        'mostrar q no este devolucion
                        Sql2 = " AND ISNULL((SELECT Y.SERIE_NUMERAR FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (Y.PREDET_ESTADO_PRESTAMO = '1') AND (X.PRESTA_TIPO_MOVIMIENTO = 'S') "
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql2 = Sql2 & " AND (X.PRESTA_TIPOORIGEN = '1') AND (X.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                        ElseIf RBCentroC.Checked = True Then
                            Sql2 = Sql2 & " AND (X.PRESTA_TIPOORIGEN = '2') AND (X.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                        End If
                        Sql2 = Sql2 & " AND Y.SERIE_NUMERAR = S.SERIE_NUMERAR),'') = ''"
                        Sql = Sql & Sql2
                        'mostrar no ESTEN  PRESTADOS
                        Sql2 = " AND ISNULL((SELECT Y.SERIE_NUMERAR FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (Y.PREDET_ESTADO_PRESTAMO = '1') AND (X.PRESTA_TIPO_MOVIMIENTO = 'S') "
                        If RBAlmacen.Checked = True Then 'almacen
                            Sql2 = Sql2 & " AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                        ElseIf RBCentroC.Checked = True Then
                            Sql2 = Sql2 & " AND (X.PRESTA_TIPODESTINO = '1') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                        End If
                        Sql2 = Sql2 & " AND Y.SERIE_NUMERAR = S.SERIE_NUMERAR),'') = ''"
                        Sql = Sql & Sql2
                        'MOSTRAR LO QUE NO ESTAN DEVUELTO POR REEM POR CAMBIO
                        Sql2 = " AND ((SELECT R.SERIE_NUMERAR_REEMPLAZANTE FROM TBINV_REEMPLAZOS R WHERE R.SERIE_NUMERAR_REEMPLAZANTE = S.SERIE_NUMERAR AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND R.REEM_ESTADO_1 = '2' AND R.REEM_ESTADO_2 = '1') IS NULL) "
                        Sql = Sql & Sql2
                        'MOSTRAR LO QUE NO ESTAN AVERIADOS
                        Sql2 = " AND ((SELECT AV.AVERIA_SERIE_NUMERAR FROM TBINV_AVERIA AV WHERE AV.AVERIA_SERIE_NUMERAR = S.SERIE_NUMERAR AND AV.EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND AV.AVERIA_ESTADO_1 = '0' AND AV.AVERIA_ESTADO_2 = '1') IS NULL)"
                        Sql = Sql & Sql2
                    End If
            End Select

            Sql = Sql & " AND RIGHT('00000000'+CONVERT(VARCHAR(20),ARTICULO_CODIGO),8) LIKE @CodArticulo"
            Sql = Sql & " AND ART_DESCRIPCION LIKE @NomArticulo"
            Sql = Sql & " And isnull( A.ART_CODESPECIF ,'') LIKE @V_CODESP"
            Sql = Sql & " And isnull( A.ART_CODEQUIVA ,'') LIKE @V_PARTE"
            Sql = Sql & " And isnull(a.art_tipo,'') like @V_TIPO"

            If Session("Busqueda") = "Accesorio" Then
            Else
                Sql = Sql & " AND SERIE_NRO LIKE @SerieArticulo"
                Sql = Sql & " AND LTRIM(RTRIM(STR(ISNULL(PLACA_NRO,'')))) LIKE @Placa"
            End If
            Sql = Sql & " And AC.CLAS_NUMERO Like @V_CLAS_NUMERO"

            If Session("Busqueda") = "Accesorio" Then
                Sql = Sql & " ORDER BY A.ART_DESCRIPCION"
            Else
                Sql = Sql & " ORDER BY A.ART_DESCRIPCION, S.SERIE_NRO"

            End If

            Dim psTipoart As String = ""
            If DdlTipoBA.SelectedValue <> "< Seleccionar >" Then
                psTipoart = DdlTipoBA.SelectedValue
            End If

            Dim ds As DataSet
            'Dim Cn As SqlConnection
            Dim da As SqlDataAdapter

            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            Dim Cn As New SqlConnection(psConexion)
            'Cn = New SqlConnection(Session("Ruta_Emp"))
            da = New SqlDataAdapter(Sql, Cn)

            da.SelectCommand.Parameters.Add(New SqlParameter("@CodArticulo", SqlDbType.VarChar, 8))
            da.SelectCommand.Parameters("@CodArticulo").Value = TxtCodArticuloBA.Value.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@NomArticulo", SqlDbType.VarChar, 80))
            da.SelectCommand.Parameters("@NomArticulo").Value = "%" & TxtDescripcionBA.Value.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@V_CODESP", SqlDbType.VarChar, 30))
            da.SelectCommand.Parameters("@V_CODESP").Value = TxtCodEspecificoBA.Value.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@V_PARTE", SqlDbType.VarChar, 30))
            da.SelectCommand.Parameters("@V_PARTE").Value = TxtNumParteBA.Value.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@V_TIPO", SqlDbType.VarChar, 30))
            da.SelectCommand.Parameters("@V_TIPO").Value = psTipoart & "%"
            If Session("Busqueda") = "Accesorio" Then
            Else
                da.SelectCommand.Parameters.Add(New SqlParameter("@SerieArticulo", SqlDbType.VarChar, 30))
                da.SelectCommand.Parameters("@SerieArticulo").Value = txtSerieArt.Text.Trim & "%"

                da.SelectCommand.Parameters.Add(New SqlParameter("@Placa", SqlDbType.VarChar, 30))
                da.SelectCommand.Parameters("@Placa").Value = txtPlaca.Text.Trim & "%"
            End If
            da.SelectCommand.Parameters.Add(New SqlParameter("@V_CLAS_NUMERO", SqlDbType.VarChar, 30))
            da.SelectCommand.Parameters("@V_CLAS_NUMERO").Value = lblCodClas.Text.Trim & "%"

            ds = New DataSet()
            da.Fill(ds, "TBCLIENTE")

            If Session("Busqueda") = "Accesorio" Then
                GvBusAcc.DataSource = ds.Tables(0).DefaultView
                GvBusAcc.DataBind()
                GvBusAcc.Visible = True
                GvBusEquipo.Visible = False
                If GvBusAcc.Rows.Count = 0 Then
                    Call FlexBusBlanco(GvBusAcc.ID)
                    lblCountBusEq.Text = "Registros 0"
                Else
                    lblCountBusEq.Text = "Registros " & GvBusAcc.Rows.Count.ToString
                End If
            Else
                GvBusEquipo.DataSource = ds.Tables(0).DefaultView
                GvBusEquipo.DataBind()
                GvBusEquipo.Visible = True
                GvBusAcc.Visible = False
                If GvBusEquipo.Rows.Count = 0 Then
                    Call FlexBusBlanco(GvBusEquipo.ID)
                    lblCountBusEq.Text = "Registros 0"
                Else
                    lblCountBusEq.Text = "Registros " & GvBusEquipo.Rows.Count.ToString
                End If
            End If

            If Session("Busqueda") = "Accesorio" Then
                If GvBusAcc.Rows.Count = 0 Then
                    Call FlexBusBlanco(GvBusAcc.ID)
                    lblCountBusEq.Text = "Registros 0"
                Else
                    lblCountBusEq.Text = "Registros " & GvBusAcc.Rows.Count.ToString
                End If
            Else
                If GvBusEquipo.Rows.Count = 0 Then
                    Call FlexBusBlanco(GvBusEquipo.ID)
                    lblCountBusEq.Text = "Registros 0"
                Else
                    lblCountBusEq.Text = "Registros " & _BusEq.Rows.Count.ToString
                End If
            End If


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        Finally
        End Try
    End Sub
    Private Sub Limpiar()
        Dim dt As New DataTable
        dt = Nothing

        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtDescripcionBA.Value = ""
        lblCodClas.Text = ""
        LblCodClasificacionBA.Text = ""
        TxtNumParteBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        TxtSku.Value = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        txtSerieArt.Text = ""
        txtPlaca.Text = ""

        GvBusEquipo.DataSource = dt
        GvBusEquipo.DataBind()
        GvBusAcc.DataSource = dt
        GvBusAcc.DataBind()
    End Sub

    Private Sub GvBusEquipo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusEquipo.RowCommand
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim ii As Integer = 0
        Dim Existe As Boolean = False
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim arrSelec(,) As String
        lblCountBusEq.Text = ""
        If e.CommandName = "AgregarFila" Then
            f = -1
            With _DetalleEq
                i = CLng(Session("CountArrayEq"))
                If i > -1 Then
                    arrSelec = Session("ArrayEq")
                    For ii = 0 To i
                        If arrSelec(5, ii) = GvBusEquipo.Rows(Index).Cells(8).Text.Trim And arrSelec(13, ii) = "" Then
                            lblCountBusEq.Text = "Ya se encuentra en la lista."
                            Exit Sub
                        End If
                    Next
                End If
                f = -1
                Erase arrSelec
                Session("CountArrayEq") = "-1"
                For i = 0 To .Rows.Count - 1
                    f = f + 1
                    ReDim Preserve arrSelec(14, f)
                    arrSelec(0, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ARTICULO_CODIGO
                    arrSelec(1, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ART_DESCRIPCION
                    arrSelec(2, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ART_SKU
                    arrSelec(3, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'SERIE_NRO
                    arrSelec(4, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'PLACA_NRO
                    arrSelec(5, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'SERIE_NUMERAR
                    Dim cFuncion As DropDownList = _DetalleEq.Rows(i).Cells(7).FindControl("cboFuncion")
                    arrSelec(6, i) = cFuncion.SelectedValue.Trim  'COD_FUNCION
                    arrSelec(7, i) = cFuncion.SelectedIndex.ToString  'combo funcion index
                    arrSelec(8, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'REEN_NUMERO
                    arrSelec(9, i) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'AVERIA
                    Dim cAveria As DropDownList = _DetalleEq.Rows(i).Cells(11).FindControl("cboAveria")
                    arrSelec(10, i) = cAveria.SelectedValue.Trim  'COD_FALLA
                    arrSelec(11, i) = cAveria.SelectedIndex.ToString   'combo averia index
                    Dim tDetAveria As TextBox = _DetalleEq.Rows(i).Cells(13).FindControl("txtDetAveria")
                    arrSelec(12, i) = tDetAveria.Text.Trim  'text averia
                    arrSelec(13, i) = ""
                Next
                Session("CountArrayEq") = f.ToString
                'End If
            End With
            f = f + 1
            ReDim Preserve arrSelec(14, f)
            arrSelec(0, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            arrSelec(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&") 'ARTICULO_CODIGO
            arrSelec(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            arrSelec(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            If GvBusEquipo.Rows(Index).Cells(7).Text.Trim = "&nbsp;" Then
                arrSelec(4, f) = String.Empty
            Else
                arrSelec(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            End If
            If GvBusEquipo.Rows(Index).Cells(8).Text.Trim = "&nbsp;" Then
                arrSelec(5, f) = String.Empty
            Else
                arrSelec(5, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            End If

            arrSelec(6, f) = String.Empty
            arrSelec(7, f) = "0"
            arrSelec(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            arrSelec(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusEquipo.Rows(Index).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            arrSelec(10, f) = String.Empty
            arrSelec(11, f) = "0"
            arrSelec(12, f) = String.Empty
            arrSelec(13, f) = String.Empty 'n eliminado
            Session("CountArrayEq") = f.ToString

            Dim _dt As New DataTable
            Dim _dr As DataRow
            _dt.Columns.Add("ARTICULO_CODIGO", GetType(String))
            _dt.Columns.Add("ART_DESCRIPCION", GetType(String))
            _dt.Columns.Add("ART_SKU", GetType(String))
            _dt.Columns.Add("SERIE_NRO", GetType(String))
            _dt.Columns.Add("PLACA_NRO", GetType(String))
            _dt.Columns.Add("SERIE_NUMERAR", GetType(String))
            _dt.Columns.Add("REEN_NUMERO", GetType(String))
            _dt.Columns.Add("AVERIA", GetType(String))
            For ii = 0 To f
                _dr = _dt.NewRow()
                _dr(0) = arrSelec(0, ii).Trim
                _dr(1) = arrSelec(1, ii).Trim
                If arrSelec(2, ii) Is Nothing Then
                    _dr(2) = String.Empty
                Else
                    _dr(2) = arrSelec(2, ii).Trim
                End If
                If arrSelec(3, ii) Is Nothing Then
                    _dr(3) = String.Empty
                Else
                    _dr(3) = arrSelec(3, ii).Trim
                End If
                If arrSelec(4, ii) Is Nothing Then
                    _dr(4) = String.Empty
                Else
                    _dr(4) = arrSelec(4, ii).Trim
                End If
                If arrSelec(5, ii) Is Nothing Then
                    _dr(5) = String.Empty
                Else
                    _dr(5) = arrSelec(5, ii).Trim
                End If
                If arrSelec(8, ii) Is Nothing Then
                    _dr(6) = String.Empty
                Else
                    _dr(6) = arrSelec(8, ii).Trim
                End If
                If arrSelec(9, ii) Is Nothing Then
                    _dr(7) = String.Empty
                Else
                    _dr(7) = arrSelec(9, ii).Trim
                End If
                _dt.Rows.Add(_dr)
            Next
            Session("ArrayEq") = arrSelec
            _DetalleEq.DataSource = New DataView(_dt)
            _DetalleEq.DataBind()

            With _DetalleEq
                For i = 0 To .Rows.Count - 1
                    Dim cFuncion As DropDownList = .Rows(i).Cells(7).FindControl("cboFuncion")
                    Dim cAveria As DropDownList = .Rows(i).Cells(11).FindControl("cboAveria")
                    Dim tDetAveria As TextBox = .Rows(i).Cells(13).FindControl("txtDetAveria")
                    Call LlenaComboItem("TBOPC230", cFuncion)
                    cFuncion.Items.Insert(0, "")
                    Call LlenaComboItem("TBOPC236", cAveria)
                    If arrSelec(6, i).Trim <> "" Then cFuncion.SelectedIndex = arrSelec(7, i).Trim
                    If arrSelec(10, i).Trim <> "" Then cAveria.SelectedIndex = arrSelec(11, i).Trim
                    tDetAveria.Text = arrSelec(12, i).Trim
                Next
            End With
            If lblCountBusEq.Text = "" Then lblCountBusEq.Text = "Registros: " & GvBusEquipo.Rows.Count.ToString
        End If
    End Sub

    Private Sub GvBusAcc_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusAcc.RowCommand
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim ii As Integer = 0
        Dim Existe As Boolean = False
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim arrSelec(,) As String
        If e.CommandName = "AgregarFila" Then
            f = -1
            With _DetalleAc
                If CLng(GvBusAcc.Rows(index).Cells(6).Text) <= 0 Then
                    lblCountBusAc.Text = "No hay Stock disponible para el Accesorio."
                    Exit Sub
                End If
                i = CLng(Session("CountArrayAc"))
                If i > -1 Then
                    arrSelec = Session("ArrayAc")
                    For ii = 0 To i
                        If arrSelec(0, ii) = GvBusAcc.Rows(index).Cells(1).Text.Trim And arrSelec(6, ii) = "" Then
                            lblCountBusAc.Text = "Ya se encuentra en la lista."
                            Exit Sub
                        End If
                    Next
                End If
                f = -1
                Erase arrSelec
                Session("CountArrayAc") = "-1"
                For i = 0 To .Rows.Count - 1
                    f = f + 1
                    ReDim Preserve arrSelec(6, f)
                    arrSelec(0, i) = .Rows(i).Cells(1).Text.Trim 'ARTICULO_CODIGO
                    arrSelec(1, i) = .Rows(i).Cells(2).Text.Trim 'ART_DESCRIPCION
                    arrSelec(2, i) = .Rows(i).Cells(3).Text.Trim 'ART_SKU
                    arrSelec(3, i) = .Rows(i).Cells(4).Text.Trim 'STOCK ACTUAL
                    Dim tCantSal As TextBox = _DetalleAc.Rows(i).Cells(5).FindControl("txtCantSal")
                    arrSelec(4, i) = tCantSal.Text.Trim  'text averia
                    arrSelec(5, i) = String.Empty
                    arrSelec(6, i) = String.Empty
                Next
                Session("CountArrayAc") = f.ToString
                'End If
            End With
            f = f + 1
            ReDim Preserve arrSelec(6, f)
            arrSelec(0, f) = GvBusAcc.Rows(index).Cells(1).Text.Trim
            arrSelec(1, f) = GvBusAcc.Rows(index).Cells(2).Text.Trim
            arrSelec(2, f) = GvBusAcc.Rows(index).Cells(4).Text.Trim
            arrSelec(3, f) = GvBusAcc.Rows(index).Cells(6).Text.Trim
            arrSelec(4, f) = "0"
            arrSelec(5, f) = String.Empty
            arrSelec(6, f) = String.Empty 'n eliminado
            Session("CountArrayAc") = f.ToString

            Dim _dt As New DataTable
            Dim _dr As DataRow
            _dt.Columns.Add("ARTICULO_CODIGO", GetType(String))
            _dt.Columns.Add("ART_DESCRIPCION", GetType(String))
            _dt.Columns.Add("ART_SKU", GetType(String))
            _dt.Columns.Add("STOCK_ACTUAL", GetType(String))
            For ii = 0 To f
                _dr = _dt.NewRow()
                _dr(0) = arrSelec(0, ii)
                _dr(1) = arrSelec(1, ii)
                _dr(2) = arrSelec(2, ii)
                _dr(3) = arrSelec(3, ii)
                _dt.Rows.Add(_dr)
            Next
            Session("ArrayAc") = arrSelec
            _DetalleAc.DataSource = New DataView(_dt)
            _DetalleAc.DataBind()

            With _DetalleAc
                For i = 0 To .Rows.Count - 1
                    Dim tCantSal As TextBox = .Rows(i).Cells(5).FindControl("txtCantSal")
                    tCantSal.Text = arrSelec(4, i).Trim
                    If tCantSal.Text.Trim = "" Then tCantSal.Text = "0"
                Next
            End With
            If lblCountBusAc.Text = "" Then lblCountBusAc.Text = "Registros: " & _BusAc.Rows.Count.ToString
        End If
    End Sub
End Class
