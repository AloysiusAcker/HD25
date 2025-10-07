Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Inventario_CCosto_Salida
    Inherits System.Web.UI.Page
    Dim DiasFuturoRegistrarFecha As Int16
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
            If NroTicket <> "" Then
                Session("TicketNro") = NroTicket
            Else
                Session("TicketNro") = String.Empty
            End If
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
            Call Carga_Motivos()
            lblFechaDevol.Visible = False
            txtFechaDevol.Visible = False
            _Ubica4.Visible = False
            Call FlexBusBlanco(_BusEq.ID)
            Call FlexBusBlanco(_DetalleEq.ID)
            Call FlexBusBlanco(_BusAc.ID)
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
    Private Sub Carga_Motivos()
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        cboMotivo.Items.Clear()
        lblFechaDevol.Visible = False
        txtFechaDevol.Visible = False
        _Ubica4.Visible = False
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = "SELECT DISTINCT MAINSA_MOTIVO_TRASLADO, (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC217' AND ELEMEN_CODIGO = MAINSA_MOTIVO_TRASLADO) AS MOTIVO_TRASLADO" _
                               & " FROM TBINV_MATRIZ_INGRESOSALIDA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (MAINSA_TIPO_MOVIMIENTO = 'S') AND (MAINSA_UBICACION1 = '2') AND (MAINSA_UBICACION2 = '" & IIf(OptDestino.SelectedIndex = 0, "1", "2") & "') ORDER BY MOTIVO_TRASLADO"
            Rs = cmdSql.ExecuteReader()
            cboMotivo.DataSource = Rs
            cboMotivo.DataTextField = "MOTIVO_TRASLADO"
            cboMotivo.DataValueField = "MAINSA_MOTIVO_TRASLADO"
            cboMotivo.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub FlexBusBlanco(ByVal nFlex As String)
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
        Dim da As SqlDataAdapter
        Dim ds As New DataSet
        Dim Sql As String = ""
        Try
            Select Case nFlex
                Case "_BusEq"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS SERIE_NRO, '' AS PLACA_NRO, '' AS SERIE_NUMERAR, '' AS REEN_NUMERO, '' AS AVERIA "
                Case "_DetalleEq"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS SERIE_NRO, '' AS PLACA_NRO, '' AS SERIE_NUMERAR, '' AS FUNCION, '' AS COD_FUNCION, '' AS REEN_NUMERO, '' AS AVERIA, '' AS FALLA_AVERIA, '' AS COD_FALLA, '' AS DET_AVERIA "
                Case "_BusAc"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS STOCK_ACTUAL, '' AS NRO_REEN_SIN_SERIE"
                Case "_DetalleAc"
                    Sql = "SELECT '' AS ARTICULO_CODIGO, '' AS ART_DESCRIPCION, '' AS STOCK_ACTUAL, '' AS CANT_SALIDA, '' AS NRO_REEN_SIN_SERIE"
            End Select
            da = New SqlDataAdapter(Sql, Cn)
            da.Fill(ds, "TBINV_ARTICULOS_SERIES")
            Select Case nFlex
                Case "_BusEq"
                    _BusEq.DataSource = ""
                    _BusEq.DataBind()
                    lblCountBusEq.Text = "Registros: 0"
                Case "_DetalleEq"
                    _DetalleEq.DataSource = ""
                    _DetalleEq.DataBind()
                    If cboMotivo.Text <> "" Then
                        If cboMotivo.SelectedValue.ToString = "13" Or cboMotivo.SelectedValue.ToString = "17" Then
                            _DetalleEq.Columns(10).Visible = True
                            _DetalleEq.Columns(12).Visible = True
                        Else
                            _DetalleEq.Columns(10).Visible = False
                            _DetalleEq.Columns(12).Visible = False
                        End If
                    Else
                        _DetalleEq.Columns(10).Visible = False
                        _DetalleEq.Columns(12).Visible = False
                    End If
                    _DetalleAc.DataBind()
                Case "_BusAc"
                    _BusAc.DataSource = ""
                    _BusAc.DataBind()
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
        If lblCodOrigen.Text.ToString = "" Then
            lblCountBusEq.Text = "Seleccionar Origen"
            Exit Sub
        End If
        If lblCodDestino.Text.Trim = "" Then
            lblCountBusEq.Text = "Seleccionar Destino"
            Exit Sub
        End If
        If cboMotivo.Text = "" Then lblCountBusEq.Text = "Seleccionar Motivo" : cboMotivo.Focus() : Exit Sub
        If cboMotivo.SelectedIndex = -1 Then lblCountBusEq.Text = "Seleccionar Motivo" : cboMotivo.Focus() : Exit Sub

        Try
            Select Case cboMotivo.SelectedValue
                Case "3" 'DEVOLUCION POR PRESTAMO
                    'listar los prestados q faltan devolver
                    Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION, S.SERIE_NRO, S.PLACA_NRO,LTRIM(RTRIM(STR(S.SERIE_NUMERAR))) AS SERIE_NUMERAR, " _
                        & " '' AS REEN_NUMERO, '' AS AVERIA " _
                        & " FROM TBINV_PRESTAMO C INNER JOIN TBINV_PRESTAMO_DETALLE D ON C.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND C.PRESTA_CODIGO = D.PRESTA_CODIGO INNER JOIN " _
                        & " TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO ON C.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND  D.SERIE_NUMERAR = S.SERIE_NUMERAR " _
                        & " AND C.CECOSE_CODIGO_DESTINO = S.UBICACT_CODIGO AND  C.PRESTA_TIPODESTINO = S.UBICACT_TIPO " _
                        & " WHERE (S.UBICACT_CODIGO = " & lblCodOrigen.Text & ") AND (S.SERIE_SYS_EST = '0') AND (S.UBICACT_TIPO = '2') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND " _
                        & " (D.PREDET_ESTADO_PRESTAMO = '1') AND (C.PRESTA_TIPO_MOVIMIENTO = 'S')"
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '1') AND (C.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    Else
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '2') AND (C.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                Case "12" 'DEVOLUCION REEMPLAZO POR CAMBIO
                    'LISTAR LOS REEMPLAZOS QUE FALTAN DEVOLVER TIPO 1
                    Sql = " SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION,S.SERIE_NRO,S.PLACA_NRO, LTRIM(RTRIM(STR(R.SERIE_NUMERAR_REEMPLAZANTE))) AS SERIE_NUMERAR," _
                        & " LTRIM(RTRIM(STR(REEM_NRO))) AS REEN_NUMERO, '' AS AVERIA, " _
                        & " R.REEM_TIPO_DESTINO, R.REEM_CODIGO_DESTINO, R.REEM_TIPO_ORIGEN, R.REEM_CODIGO_ORIGEN, R.REEM_TIPO" _
                        & " FROM dbo.TBINV_REEMPLAZOS R INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S ON R.SERIE_NUMERAR_REEMPLAZANTE = S.SERIE_NUMERAR INNER JOIN" _
                        & " dbo.TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO " _
                        & " WHERE (R.REEM_TIPO_DESTINO='2')AND (R.REEM_CODIGO_DESTINO='" & lblCodOrigen.Text & "') AND (R.REEM_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0')" _
                        & " AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (A.ART_SYS_EST = '0') AND (R.REEM_ESTADO_1 = '2') AND (R.REEM_ESTADO_2 = '1') AND (R.REEM_TIPO='1') AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '1') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    Else
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
                        & " (R.REEM_ESTADO_1 = '2') AND (R.REEM_ESTADO_2 = '1') AND (R.REEM_TIPO = '2') AND (R.REEM_TIPO_DESTINO='2') AND (R.REEM_CODIGO_DESTINO='" & lblCodOrigen.Text & "') AND R.EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '1') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    Else
                        Sql = Sql & " AND (R.REEM_TIPO_ORIGEN = '2') AND (R.REEM_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                Case Else
                    Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),S.ARTICULO_CODIGO),8) AS  ARTICULO_CODIGO,ISNULL(A.ART_DESCRIPCION,'')+' '+ISNULL(S.SERIE_CARACTERISTICAS,'') AS ART_DESCRIPCION,S.SERIE_NRO,S.PLACA_NRO, LTRIM(RTRIM(STR(S.SERIE_NUMERAR))) AS SERIE_NUMERAR, " _
                        & " '' AS REEN_NUMERO, '' AS AVERIA " _
                        & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " S INNER JOIN TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO" _
                        & " WHERE (S.UBICACT_CODIGO =" & lblCodOrigen.Text & ") AND (S.SERIE_SYS_EST = '0') AND (S.UBICACT_TIPO = '2') AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') "
                    'mostrar q no este devolucion
                    Sql2 = " AND ISNULL((SELECT Y.SERIE_NUMERAR FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (Y.PREDET_ESTADO_PRESTAMO = '1') AND (X.PRESTA_TIPO_MOVIMIENTO = 'S') "
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPOORIGEN = '1') AND (X.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    Else
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPOORIGEN = '2') AND (X.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    End If
                    Sql2 = Sql2 & " AND Y.SERIE_NUMERAR = S.SERIE_NUMERAR),'') = ''"
                    Sql = Sql & Sql2
                    'mostrar no ESTEN  PRESTADOS
                    Sql2 = " AND ISNULL((SELECT Y.SERIE_NUMERAR FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (Y.PREDET_ESTADO_PRESTAMO = '1') AND (X.PRESTA_TIPO_MOVIMIENTO = 'S') "
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    Else
                        Sql2 = Sql2 & " AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
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
            Sql = Sql & " AND ART_DESCRIPCION LIKE @NomArticulo"
            Sql = Sql & " AND SERIE_NRO LIKE @SerieArticulo"
            Sql = Sql & " AND LTRIM(RTRIM(STR(ISNULL(PLACA_NRO,'')))) LIKE @Placa"
            Sql = Sql & " ORDER BY A.ART_DESCRIPCION, S.SERIE_NRO"

            Dim ds As DataSet
            Dim Cn As SqlConnection
            Dim da As SqlDataAdapter

            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            Cn = New SqlConnection(psConexion)
            da = New SqlDataAdapter(Sql, Cn)

            da.SelectCommand.Parameters.Add(New SqlParameter("@CodArticulo", SqlDbType.VarChar, 8))
            da.SelectCommand.Parameters("@CodArticulo").Value = txtCodigoArt.Text.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@NomArticulo", SqlDbType.VarChar, 80))
            da.SelectCommand.Parameters("@NomArticulo").Value = txtNomArt.Text.Trim & "%"

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
            Call FlexBusBlanco(_BusEq.ID)
            lblCountBusEq.Text = "Registros 0"
        Else
            lblCountBusEq.Text = "Registros " & _BusEq.Rows.Count.ToString
        End If
    End Sub

    Protected Sub _BuscarEq_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles _BuscarEq.Command

    End Sub

    Protected Sub _BusEq_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles _BusEq.RowCommand
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
                    ReDim Preserve arrSelec(13, f)
                    arrSelec(0, i) = .Rows(i).Cells(1).Text.Trim 'ARTICULO_CODIGO
                    arrSelec(1, i) = .Rows(i).Cells(2).Text.Trim 'ART_DESCRIPCION
                    arrSelec(2, i) = .Rows(i).Cells(3).Text.Trim 'SERIE_NRO
                    arrSelec(3, i) = .Rows(i).Cells(4).Text.Trim 'PLACA_NRO
                    arrSelec(4, i) = .Rows(i).Cells(5).Text.Trim 'SERIE_NUMERAR
                    Dim cFuncion As DropDownList = _DetalleEq.Rows(i).Cells(6).FindControl("cboFuncion")
                    arrSelec(5, i) = cFuncion.SelectedValue.Trim  'COD_FUNCION
                    arrSelec(6, i) = cFuncion.SelectedIndex.ToString  'combo funcion index
                    arrSelec(7, i) = .Rows(i).Cells(8).Text.Trim 'REEN_NUMERO
                    arrSelec(8, i) = .Rows(i).Cells(9).Text.Trim 'AVERIA
                    Dim cAveria As DropDownList = _DetalleEq.Rows(i).Cells(10).FindControl("cboAveria")
                    arrSelec(9, i) = cAveria.SelectedValue.Trim  'COD_FALLA
                    arrSelec(10, i) = cAveria.SelectedIndex.ToString   'combo averia index
                    Dim tDetAveria As TextBox = _DetalleEq.Rows(i).Cells(12).FindControl("txtDetAveria")
                    arrSelec(11, i) = tDetAveria.Text.Trim  'text averia
                    arrSelec(12, i) = ""
                Next
                Session("CountArrayEq") = f.ToString
                'End If
            End With
            f = f + 1
            ReDim Preserve arrSelec(13, f)
            arrSelec(0, f) = _BusEq.Rows(Index).Cells(1).Text.Trim
            arrSelec(1, f) = _BusEq.Rows(Index).Cells(2).Text.Trim
            arrSelec(2, f) = _BusEq.Rows(Index).Cells(3).Text.Trim
            arrSelec(3, f) = _BusEq.Rows(Index).Cells(4).Text.Trim
            arrSelec(4, f) = _BusEq.Rows(Index).Cells(5).Text.Trim
            arrSelec(5, f) = String.Empty
            arrSelec(6, f) = "0"
            arrSelec(7, f) = _BusEq.Rows(Index).Cells(6).Text.Trim
            arrSelec(8, f) = _BusEq.Rows(Index).Cells(7).Text.Trim
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
                _dr(2) = arrSelec(2, ii).Trim
                _dr(3) = arrSelec(3, ii).Trim
                _dr(4) = arrSelec(4, ii).Trim
                _dr(5) = arrSelec(7, ii).Trim
                _dr(6) = arrSelec(8, ii).Trim
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
                    Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
                    Call LlenaComboItem("TBOPC230", cFuncion, psCnGrEmp)
                    cFuncion.Items.Insert(0, "")
                    Call LlenaComboItem("TBOPC236", cAveria, psCnGrEmp)
                    If arrSelec(6, i).Trim <> "" Then cFuncion.SelectedIndex = arrSelec(6, i).Trim
                    If arrSelec(10, i).Trim <> "" Then cAveria.SelectedIndex = arrSelec(10, i).Trim
                    tDetAveria.Text = arrSelec(11, i).Trim
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
                        & " (D.PREDET_ESTADO_PRESTAMO IN ('1','2','4')) AND (C.PRESTA_TIPO_MOVIMIENTO = 'S') AND (C.PRESTA_TIPODESTINO = '2')"
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '1') AND (C.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    Else
                        Sql = Sql & " AND (C.PRESTA_TIPOORIGEN = '2') AND (C.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                    Sql = Sql & " GROUP BY A.EMPRESA_CODIGO, C.CECOSE_CODIGO_DESTINO, D.ARTICULO_CODIGO, A.ART_DESCRIPCION HAVING (C.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ") AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') "
                Case 12 'DEVOLUCION REEMPLAZO POR CAMBIO
                    'LISTAR LOS REEMPLAZOS QUE FALTAN DEVOLVER TIPO 1
                    Sql = " SELECT  RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_DESCRIPCION, SUM(ISNULL(RS.REEMSIN_CANT_FALT_DEVOLVER, 0) - ISNULL(RS.REEMSIN_CANT_XDEVOLVER, 0)) AS STOCK_ACTUAL   " _
                        & " FROM TBINV_ARTICULOS A INNER JOIN TBINV_REEMPLAZOS_SINSERIE RS ON A.EMPRESA_CODIGO = RS.EMPRESA_CODIGO AND A.ART_CODIGO = RS.ART_CODIGO INNER JOIN " _
                        & " TBINV_STOCK_ARTICULOS_ALMACEN D ON A.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND " _
                        & " Rs.ART_CODIGO = D.ARTICULO_CODIGO And Rs.REEMSIN_COD_DESTINO = D.ALMACEN_CODIGO And Rs.REEMSIN_TIPO_DESTINO = D.UBICACT_TIPO " _
                        & " WHERE (RS.REEMSIN_ESTADO_2 IN ('1','2','4')) AND (RS.REEMSIN_TIPO_DESTINO = '2') AND (RS.REEMSIN_ESTADO_1 = '2')"
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql = Sql & " AND (RS.REEMSIN_TIPO_ORIGEN = '1') AND (RS.REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ")"
                    Else
                        Sql = Sql & " AND (RS.REEMSIN_TIPO_ORIGEN = '2') AND (RS.REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ")"
                    End If
                    Sql = Sql & " GROUP BY A.EMPRESA_CODIGO,RS.REEMSIN_COD_DESTINO,A.ART_DESCRIPCION,D.ARTICULO_CODIGO HAVING (RS.REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ") AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                Case Else
                    Sql = "SELECT RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) AS ARTICULO_CODIGO, A.ART_DESCRIPCION, ISNULL(D.SKSSCC_STOCK_ACTUAL,0) " _
                        & " - ISNULL(" _
                        & " (SELECT SUM(PREDET_CANT_FALT_DEVOLVER)+SUM(PREDET_CANT_XDEVOLVER) FROM TBINV_PRESTAMO X INNER JOIN TBINV_PRESTAMO_DETALLE_SINSERIE Y ON X.EMPRESA_CODIGO = Y.EMPRESA_CODIGO AND X.PRESTA_CODIGO = Y.PRESTA_CODIGO AND (X.PRESTA_TIPO_MOVIMIENTO = 'S' AND Y.ARTICULO_CODIGO = D.ARTICULO_CODIGO)" ' AND (Y.PREDET_ESTADO_PRESTAMO = '1')"
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql = Sql & " AND (X.PRESTA_TIPOORIGEN = '1') AND (X.ALMACEN_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    Else 'Seccion CC
                        Sql = Sql & " AND (X.PRESTA_TIPOORIGEN = '2') AND (X.CECOSE_CODIGO_ORIGEN = " & lblCodDestino.Text & ") AND (X.PRESTA_TIPODESTINO = '2') AND (X.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ")"
                    End If
                    Sql = Sql & "),0) - ISNULL(" _
                        & " (SELECT SUM(REEMSIN_CANT_FALT_DEVOLVER)+SUM(REEMSIN_CANT_XDEVOLVER) FROM TBINV_REEMPLAZOS_SINSERIE RS WHERE D.ARTICULO_CODIGO = RS.ART_CODIGO "
                    If OptDestino.SelectedIndex = 0 Then 'almacen
                        Sql = Sql & "  AND (REEMSIN_TIPO_ORIGEN = '1') AND (REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ") AND (REEMSIN_TIPO_DESTINO = '2') AND (REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ")"
                    Else 'Seccion CC
                        Sql = Sql & " AND (REEMSIN_TIPO_ORIGEN = '2') AND (REEMSIN_COD_ORIGEN = " & lblCodDestino.Text & ") AND (REEMSIN_TIPO_DESTINO = '2') AND (REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ")"
                    End If
                    Sql = Sql & " ),0) AS STOCK_ACTUAL " _
                        & " FROM TBINV_STOCK_SINSERIE_CCOSTO D INNER JOIN TBINV_ARTICULOS A ON D.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND D.ARTICULO_CODIGO = A.ART_CODIGO " _
                        & " WHERE (D.CECOSE_CODIGO = " & lblCodOrigen.Text & ") AND (D.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (D.SKSSCC_SYS_EST = '0') AND (A.ART_SYS_EST = '0')  " 'TIPO ACCESORIO  AND (A.ART_TIPO = 87)
                    'al stock actual le restamos los articulos q se tienen q devolver y/o falta devolver
            End Select
            Sql = Sql & " AND RIGHT('00000000'+CONVERT(VARCHAR(20),D.ARTICULO_CODIGO),8) LIKE @CodArticulo "
            Sql = Sql & " AND A.ART_DESCRIPCION LIKE @NomArticulo"
            Sql = Sql & " ORDER BY A.ART_DESCRIPCION"

            Dim ds As DataSet
            Dim Cn As SqlConnection
            Dim da As SqlDataAdapter

            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            Cn = New SqlConnection(psConexion)
            da = New SqlDataAdapter(Sql, Cn)

            da.SelectCommand.Parameters.Add(New SqlParameter("@CodArticulo", SqlDbType.VarChar, 8))
            da.SelectCommand.Parameters("@CodArticulo").Value = txtCodigoAc.Text.Trim & "%"

            da.SelectCommand.Parameters.Add(New SqlParameter("@NomArticulo", SqlDbType.VarChar, 80))
            da.SelectCommand.Parameters("@NomArticulo").Value = txtNomAc.Text.Trim & "%"

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
            Call FlexBusBlanco(_BusAc.ID)
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
    Protected Sub OptDestino_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OptDestino.SelectedIndexChanged
        Session("TipoDestino") = ""
        If OptDestino.SelectedIndex = 0 Then
            Session("TipoDestino") = "Almacen"
        Else
            Session("TipoDestino") = "CentroCosto"
        End If
        Call Carga_Motivos()
        txtDesCodExterno.Text = ""
        txtDesDescrip.Text = ""
        lblCodDestino.Text = ""
        Session("DestinoDescrip") = ""
        Session("DestinoCodExt") = ""
        lblFechaDevol.Visible = False
        txtFechaDevol.Visible = False
        _Ubica4.Visible = False
        Call FlexBusBlanco(_BusEq.ID)
        Call FlexBusBlanco(_DetalleEq.ID)
        Call FlexBusBlanco(_BusAc.ID)
        Call FlexBusBlanco(_DetalleAc.ID)
        Session("ArrayEq") = String.Empty
        Session("ArrayAc") = String.Empty
        Session("CountArrayEq") = "-1"
        Session("CountArrayAc") = "-1"
    End Sub
    Private Sub Carga_Funcion(ByVal Cbo As DropDownList)
        'Dim Cn As New SqlConnection(System.Configuration.ConfigurationManager.AppSettings("strConexion_GE"))
        Dim psCnGrEmp As String = Ruta_GrEmp 'ConfigurationManager.AppSettings("cnTecnicosGrEmp")
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)
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
        _Ubica4.Visible = False
        Select Case cboMotivo.SelectedValue.ToString
            Case "1"
                lblFechaDevol.Visible = True
                txtFechaDevol.Visible = True
                txtFechaDevol.Text = FormatoFecha(FechaActual())
                _Ubica4.Visible = True
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
        Call FlexBusBlanco(_BusEq.ID)
        Call FlexBusBlanco(_DetalleEq.ID)
        Call FlexBusBlanco(_BusAc.ID)
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
                    Dim psCnGrEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
                    Call LlenaComboItem("TBOPC230", cFuncion, psCnGrEmp)
                    cFuncion.Items.Insert(0, "")
                    Call LlenaComboItem("TBOPC236", cAveria, psCnGrEmp)
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
        Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader

        Dim FechaSal As String = txtFecha.Text.Trim
        Dim HoraSal As String = txtHora.Text.Trim

        Dim CanEnvEq As Integer = _DetalleEq.Rows.Count
        Dim CanEnvAc As Integer = _DetalleAc.Rows.Count

        Dim FechaServer As String = FechaActual()
        Dim HoraServer As String = HoraActual()
        Dim ValorSys As String = FechaServer & HoraServer & lblUsuario.Text

        Dim Estado As String = "2"
        Dim SysEst As String = "0"
        Dim Motivo As String = cboMotivo.SelectedValue

        Dim DesCodAlmacen As String = "NULL"
        Dim DesCodSeccion As String = "NULL"

        Dim TotArt As Long
        Dim i As Integer = 0
        Dim Item As Integer = 0
        Dim Stock As Double = 0
        Dim StockAc As Double = 0

        Dim CodArticulo As String = ""
        Dim NroMovimiento As String = ""

        lblMensaje.Text = ""

        If lblCodOrigen.Text = "" Then lblMensaje.Text = " <br> - Seleccionar Origen."
        If lblCodDestino.Text = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Seleccionar Destino."
        If OptDestino.SelectedValue = "2" Then
            If lblCodDestino.Text <> "" And lblCodOrigen.Text = lblCodDestino.Text Then lblMensaje.Text = lblMensaje.Text & " <br> - Origen y Destino no pueden ser los mismos."
        End If

        If FechaSal = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Ingresar Fecha Salida"
        If HoraSal = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Ingresar Hora Salida"
        If Not IsDate(FechaSal) Then lblMensaje.Text = lblMensaje.Text & " <br> - Fecha no válida."
        If Not IsDate(HoraSal) Then lblMensaje.Text = lblMensaje.Text & " <br> - Hora no válida."

        'PUEDE HACER SALIDA A 3 DIAS A FUTURO
        Dim sFecha As Date
        sFecha = DateAdd("d", DiasFuturoRegistrarFecha, FormatoFecha(FechaActual()))
        If Format(FechaSal, "yyyymmdd") > Format(sFecha, "yyyymmdd") And DiasFuturoRegistrarFecha > 0 Then
            lblMensaje.Text = lblMensaje.Text & " <br> - La Fecha de Salida solo puede ser " & DiasFuturoRegistrarFecha & " dias a futuro."
        End If

        If txtFechaDevol.Visible = True Then
            If txtFechaDevol.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Ingresar Fecha Devolución"
            If Not IsDate(txtFechaDevol.Text.Trim) Then lblMensaje.Text = lblMensaje.Text & " <br> - La Fecha Devolución no válida"
            If Format(Convert.ToDateTime(txtFechaDevol.Text), "yyyymmdd") >= Format(Convert.ToDateTime(FechaSal), "yyyymmdd") Then
            Else
                lblMensaje.Text = lblMensaje.Text & " <br> - La fecha a devolver el prestamo debe ser igual o después a la fecha de la salida."
            End If
        End If

        If CanEnvEq = 0 And CanEnvAc = 0 Then lblMensaje.Text = lblMensaje.Text & " <br> - No hay detalle de salida que guardar."

        TotArt = 0
        With _DetalleEq
            If Motivo = "13" Or Motivo = "17" Then
                For i = 0 To .Rows.Count - 1
                    Dim cAveria As DropDownList = .Rows(i).Cells(10).FindControl("cboAveria")
                    Dim tDetAveria As TextBox = .Rows(i).Cells(12).FindControl("txtDetAveria")
                    If cAveria.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Todos los Equipos deben tener Tipo de Falla."
                    If tDetAveria.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Todos los Equipos deben tener Detalle de la Averia."
                    If cAveria.Text.Trim = "" Or tDetAveria.Text.Trim = "" Then Exit For
                Next
            End If
            TotArt = .Rows.Count
        End With

        With _DetalleAc
            For i = 0 To .Rows.Count - 1
                Dim tCantDetAc As TextBox = .Rows(i).Cells(4).FindControl("txtCantSal")
                If Not IsNumeric(tCantDetAc.Text) Then tCantDetAc.Text = "0" Else tCantDetAc.Text = Format(Convert.ToDouble(tCantDetAc.Text), "0")
                If Convert.ToDouble(tCantDetAc.Text) <= 0 Then lblMensaje.Text = lblMensaje.Text & " <br> - Todos los Accesorios deben tener cantidades a salir." : Exit For
                If Convert.ToDouble(tCantDetAc.Text) > Convert.ToDouble(.Rows(i).Cells(3).Text) Then lblMensaje.Text = lblMensaje.Text & " <br> La cantidad a salir debe ser menor o igual a su Stock disponible." : Exit For
                TotArt = TotArt + tCantDetAc.Text
            Next
        End With

        If txtPerEnvia.Text.Trim = "" Then lblMensaje.Text = lblMensaje.Text & " <br> - Debe ingresar el nombre de la persona que envia la salida"

        If lblMensaje.Text <> "" Then
            lblMensaje.Text = "Existe las siguientes observaciones, favor de corregir:" & lblMensaje.Text
            Exit Sub
        End If

        Try
            Cn.Open()
            Cn2.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal2.Connection = Cn2
            CmdGlobal.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblCodigo.Text = Format(CLng(Nz(Rs(0))) + 1, "000000")
                End While
            Else
                lblCodigo.Text = "00001"
            End If
            Rs.Close()

            If OptDestino.SelectedValue = "1" Then
                DesCodAlmacen = lblCodDestino.Text
            Else
                DesCodSeccion = lblCodDestino.Text
            End If

            Item = 0
            CmdGlobal.CommandText = "INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO, OSAL_FECHA,OSAL_HORA, OSAL_USUARIO, OSAL_FECHA_SAL, OSAL_HORA_SAL,OSAL_TIPODESTINO," _
                                   & "ALMACEN_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO, CECOSE_CODIGO_ORIGEN,OSAL_CANT_ENV, OSAL_CANT_REC,OSAL_CANT_FALT_REC,OSAL_ESTADO, OSAL_OBSERVACION, OSAL_SYS_EST, OSAL_MOTIVO_GRAL,OSAL_TIPO_DOC_SALIDA,OSAL_PERSONA_ENVIA) " _
                                   & "VALUES('" & Session("CodEmpresa") & "'," & CLng(lblCodigo.Text) & ",'" & FechaServer & "','" & HoraServer & "','" & lblUsuario.Text & "','" & Format(Convert.ToDateTime(FechaSal), "yyyyMMdd") & "','" & Left(HoraSal, 2) & Right(HoraSal, 2) & "','" & OptDestino.SelectedValue & "'," _
                                   & DesCodAlmacen & "," & DesCodSeccion & "," & CLng(lblCodOrigen.Text) & "," & TotArt & ",0," & TotArt & ",'" & Estado & "','" & txtObs.Text.Trim & "','" & SysEst & "','" & Motivo & "','" & OptDocSalida.SelectedValue & "','" & txtPerEnvia.Text.Trim & "')"
            If CmdGlobal.ExecuteNonQuery() < 1 Then
                Cn.Close()
                Cn2.Close()
                lblError.Text = "Ha ocurrido un error, no se ha podido guardar la salida."
                Exit Sub
            End If

            With _DetalleEq
                For i = 0 To .Rows.Count - 1
                    Item = Item + 1
                    'DET = "SELECT '1' AS ARTICULO_CODIGO, '2' AS ART_DESCRIPCION, '3' AS SERIE_NRO, '4' AS PLACA_NRO, '5' AS SERIE_NUMERAR, '6' AS FUNCION, '7' AS COD_FUNCION, '8' AS REEN_NUMERO, '9' AS AVERIA, '10' AS FALLA_AVERIA, '11' AS COD_FALLA, '12' AS DET_AVERIA "
                    Dim cFuncion As DropDownList = .Rows(i).Cells(10).FindControl("cboFuncion")
                    CmdGlobal.CommandText = "INSERT TBINV_CCOSTO_SALIDA_DET( EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK,RECIBIDA_OK,OSALD_SYS_EST,OSALD_MOTIVO,OSALD_FUNCION) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & CLng(lblCodigo.Text) & "," & Item & "," & .Rows(i).Cells(5).Text & ",'S','N','0','" & Motivo & "','" & cFuncion.SelectedValue.Trim & "')"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='0',UBICACT_CODIGO=NULL,UBICACT_SYS='" & ValorSys & "' WHERE SERIE_NUMERAR=" & .Rows(i).Cells(5).Text 'TIPO 0: EN TRANSITO
                    CmdGlobal.ExecuteNonQuery()

                    CodArticulo = .Rows(i).Cells(1).Text

                    'paso 1
                    'se agrego para poder tener la informacion de stock de centro de costo en una misma tabla dependiendo del tipo de ubicacion
                    'INGRESO EN STOCK ALMACEN
                    CmdGlobal.CommandText = "SELECT ISNULL(SAA_STOCK_ACTUAL,0) AS SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") " _
                                          & " AND (ARTICULO_CODIGO = " & CodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (UBICACT_TIPO='2')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            Stock = Rs("SAA_STOCK_ACTUAL") - 1 'SALIDA
                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & Stock & " WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") " _
                                                  & " AND (ARTICULO_CODIGO = " & CodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (UBICACT_TIPO='2')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    Rs.Close()

                    'paso 2
                    'aqui se guardara el movimiento de ingreso al centro de costo
                    'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                    CmdGlobal.CommandText = "SELECT ISNULL(NRO_ARTICULO,0) AS NRO_ARTICULO FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & CodArticulo & ") AND (MOV_NRO='" & NroMovimiento & "') AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND MOV_SYS_EST='0'"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            CmdGlobal2.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET NRO_ARTICULO =" & Rs("NRO_ARTICULO") + 1 & " WHERE (CODIGO_ARTICULO = " & CodArticulo & ") AND (MOV_NRO='" & NroMovimiento & "') AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND MOV_SYS_EST='0'"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        Rs.Close()
                        NroMovimiento = "00000001"
                        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                NroMovimiento = Rs(0) + 1
                            End While
                        End If
                        '1: INGRESO, 2:SALIDA
                        CmdGlobal2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                               & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                               & " values('" & Session("CodEmpresa") & "','" & NroMovimiento & "','2','2','" & lblCodOrigen.Text & "','" & OptDestino.SelectedValue & "','" & lblCodDestino.Text & "', " _
                                               & " '" & lblCodigo.Text & "','" & CodArticulo & "','1','" & ValorSys & "','2','" & Motivo & "','" & FechaServer & "','0')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()
                Next
            End With
            With _DetalleAc
                For i = 0 To .Rows.Count - 1
                    Item = Item + 1
                    CodArticulo = .Rows(i).Cells(1).Text
                    Dim tCantDetAc As TextBox = .Rows(i).Cells(4).FindControl("txtCantSal")
                    'DETALLE = Sql = "SELECT '1' AS ARTICULO_CODIGO, '2' AS ART_DESCRIPCION, '3' AS STOCK_ACTUAL, '4' AS CANT_SALIDA, '5' AS NRO_REEN_SIN_SERIE"
                    CmdGlobal.CommandText = "INSERT TBINV_CCOSTO_SALIDA_DET_SINSERIE(EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN,ARTICULO_CODIGO,OSALD_CANT_ENV,OSALD_CANT_REC,OSALD_CANT_FALT_REC ,OSALD_SYS_EST,OSALD_MOTIVO,OSALD_FUNCION) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & lblCodigo.Text & "," & Item & "," & CodArticulo & "," & tCantDetAc.Text & ",0," & tCantDetAc.Text & ",'0','" & Motivo & "','')"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "SELECT ISNULL(SKSSCC_STOCK_ACTUAL,0) AS SKSSCC_STOCK_ACTUAL FROM TBINV_STOCK_SINSERIE_CCOSTO WHERE (CECOSE_CODIGO = " & lblCodOrigen.Text & ") " _
                        & " AND (ARTICULO_CODIGO = " & CodArticulo & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Rs("SKSSCC_STOCK_ACTUAL") - CDbl(tCantDetAc.Text)
                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_SINSERIE_CCOSTO SET SKSSCC_STOCK_ACTUAL=" & StockAc & " WHERE (CECOSE_CODIGO = " & lblCodOrigen.Text & ") " _
                                                  & " AND (ARTICULO_CODIGO = " & CodArticulo & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        CmdGlobal2.CommandText = "INSERT TBINV_STOCK_SINSERIE_CCOSTO(CECOSE_CODIGO, ARTICULO_CODIGO,SKSSCC_STOCK_ACTUAL,SKSSCC_SYS_EST,EMPRESA_CODIGO) " _
                                              & "VALUES(" & lblCodOrigen.Text & "," & CodArticulo & "," & CDbl(tCantDetAc.Text) & ",'0','" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()
                    'INGRESO EN STOCK ALMACEN
                    CmdGlobal.CommandText = "SELECT ISNULL(SAA_STOCK_ACTUAL,0) AS SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") " _
                        & " AND (ARTICULO_CODIGO = " & CodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (UBICACT_TIPO='2')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            Stock = Rs("SAA_STOCK_ACTUAL") - CDbl(tCantDetAc.Text) 'SALIDA
                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & Stock & " WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") " _
                                                  & " AND (ARTICULO_CODIGO = " & CodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (UBICACT_TIPO='2')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    Rs.Close()
                    'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                    CmdGlobal.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & CodArticulo & ") AND (MOV_NRO='" & NroMovimiento & "') AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND MOV_SYS_EST='0'"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                    Else
                        Rs.Close()
                        NroMovimiento = "00000001"
                        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                NroMovimiento = Rs(0) + 1
                            End While
                        End If
                        '1: INGRESO, 2:SALIDA
                        CmdGlobal2.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                               & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                               & " values('" & Session("CodEmpresa") & "','" & NroMovimiento & "','2','2','" & lblCodOrigen.Text & "','" & OptDestino.SelectedValue & "','" & lblCodDestino.Text & "', " _
                                               & " '" & lblCodigo.Text & "','" & CodArticulo & "','" & tCantDetAc.Text & "','" & ValorSys & "','2','" & Motivo & "','" & FechaServer & "','0')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()
                Next
            End With
            Dim QxDev As Long
            Dim QSalida As Long
            Dim NroPrestamo As Long
            NroPrestamo = 1
            CmdGlobal.CommandText = "SELECT MAX(PRESTA_CODIGO) FROM TBINV_PRESTAMO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    NroPrestamo = Nz(Rs(0)) + 1
                End While
            End If
            Rs.Close()
            Select Case Motivo
                Case "1" 'prestamo
                    CmdGlobal2.CommandText = "INSERT INTO TBINV_PRESTAMO(EMPRESA_CODIGO, PRESTA_CODIGO, PRESTA_TIPO_MOVIMIENTO, PRESTA_TIPOORIGEN, OSAL_CODIGO, RECEP_CODIGO,DESP_CODIGO," _
                                          & " ALMACEN_CODIGO_ORIGEN,CECOSE_CODIGO_ORIGEN, PROVEEDOR_CODIGO_ORIGEN, PRESTA_TIPODESTINO,ALMACEN_CODIGO_DESTINO, CECOSE_CODIGO_DESTINO," _
                                          & " PROVEEDOR_CODIGO_DESTINO, PRESTA_MOTIVO) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & NroPrestamo & ",'S','2'," & lblCodigo.Text & ",NULL,NULL," _
                                          & " NULL," & lblCodOrigen.Text & ",NULL,'" & OptDestino.SelectedValue & "'," & DesCodAlmacen & "," & DesCodSeccion & "," _
                                          & " NULL,'" & Motivo & "') "
                    CmdGlobal2.ExecuteNonQuery()
                    Item = 0
                    With _DetalleEq
                        For i = 0 To .Rows.Count - 1 'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devolver parcial
                            Item = Item + 1
                            CmdGlobal2.CommandText = "INSERT TBINV_PRESTAMO_DETALLE(EMPRESA_CODIGO,PRESTA_CODIGO, PREDET_CODIGO,SERIE_NUMERAR, PREDET_SYS_REGISTRO, PREDET_ESTADO_ENVIO, PREDET_SYS_ENVIO, PREDET_ESTADO_PRESTAMO, " _
                                                  & " PREDET_SYS_PRESTAMO, PREDET_FECHA_PORDEVOLVER, PREDET_SYS_DEVOLUCION) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & NroPrestamo & "," & Item & "," & .Rows(i).Cells(5).Text & ",'" & ValorSys & "','0','" & ValorSys & "','0'," _
                                                  & " NULL,'" & Format(Convert.ToDateTime(txtFechaDevol.Text), "yyyymmdd") & "',NULL)"
                            CmdGlobal2.ExecuteNonQuery()
                        Next
                    End With
                    With _DetalleAc
                        For i = 0 To .Rows.Count - 1 'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devolver parcial
                            Item = Item + 1
                            Dim tCantDetAc As TextBox = .Rows(i).Cells(4).FindControl("txtCantSal")
                            CmdGlobal2.CommandText = "INSERT TBINV_PRESTAMO_DETALLE_SINSERIE(EMPRESA_CODIGO,PRESTA_CODIGO, PREDET_CODIGO,ARTICULO_CODIGO,PREDET_CANTXPRESTAR,PREDET_CANT_PRESTADA,PREDET_CANT_XDEVOLVER,PREDET_CANT_FALT_DEVOLVER,PREDET_CANT_DEVUELTA ,PREDET_SYS_REGISTRO, PREDET_ESTADO_ENVIO, PREDET_SYS_ENVIO, PREDET_ESTADO_PRESTAMO, " _
                                                  & " PREDET_SYS_PRESTAMO, PREDET_FECHA_PORDEVOLVER, PREDET_SYS_DEVOLUCION) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & NroPrestamo & "," & Item & "," & .Rows(i).Cells(1).Text & "," & tCantDetAc.Text & ",0,0,0,0,'" & ValorSys & "','0','" & ValorSys & "','0'," _
                                                  & " NULL,'" & Format(Convert.ToDateTime(txtFechaDevol.Text), "yyyymmdd") & "',NULL)"
                            CmdGlobal2.ExecuteNonQuery()
                        Next
                    End With
                Case 2
                Case 3 'devolucion por prestamo
                    With _DetalleEq
                        For i = 0 To .Rows.Count - 1 'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                            If OptDestino.SelectedValue = "1" Then 'almacen
                                CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '2',OSAL_CODIGO_DEVOL =" & lblCodigo.Text & ",DESP_CODIGO_DEVOL = NULL,RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                      & " AND (B.PREDET_ESTADO_PRESTAMO = '1') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & lblCodDestino.Text & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ") AND (B.SERIE_NUMERAR = " & .Rows(i).Cells(5).Text & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            Else 'ccosto
                                CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '2',OSAL_CODIGO_DEVOL =" & lblCodigo.Text & ",DESP_CODIGO_DEVOL = NULL,RECEP_CODIGO_DEVOL = NULL FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                                      & " AND (B.PREDET_ESTADO_PRESTAMO = '1') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & lblCodDestino.Text & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ") AND (B.SERIE_NUMERAR = " & .Rows(i).Cells(5).Text & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                        Next
                    End With
                    'LO Q RESPECTA A DEVOLUCION DE PRESTAMO ES DIFERENTE PUEDE SER ACUMULATIVA, Y POR LO TANTO SE DEBE DEVOLVER EN PROPORCION A LO PRESTADO
                    With _DetalleAc
                        For i = 0 To .Rows.Count - 1 'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                            'QSalida = .Rows(i).Cells(4).Text
                            Dim tCantDetAc As TextBox = .Rows(i).Cells(4).FindControl("txtCantSal")
                            QSalida = tCantDetAc.Text
                            If OptDestino.SelectedValue = "1" Then 'almacen
                                CmdGlobal.CommandText = " SELECT A.PRESTA_CODIGO, ISNULL(B.PREDET_CANT_PRESTADA,0) AS PREDET_CANT_PRESTADA FROM TBINV_PRESTAMO A INNER JOIN TBINV_PRESTAMO_DETALLE_SINSERIE B ON A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO WHERE (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                    & " AND (B.PREDET_ESTADO_PRESTAMO IN ('1','2')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & lblCodDestino.Text & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ") AND (B.ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") ORDER BY A.PRESTA_CODIGO"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        If Rs("PREDET_CANT_PRESTADA") < QSalida Then
                                            QxDev = Rs("PREDET_CANT_PRESTADA")
                                            QSalida = QSalida - QxDev
                                        Else
                                            QxDev = QSalida
                                        End If
                                        CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_ESTADO_PRESTAMO WHEN '1' THEN '2' ELSE PREDET_ESTADO_PRESTAMO END), " _
                                                              & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) + " & QxDev & ", " _
                                                              & " OSAL_CODIGO_DEVOL =" & lblCodigo.Text & ",DESP_CODIGO_DEVOL = NULL,RECEP_CODIGO_DEVOL = NULL WHERE PRESTA_CODIGO = " & Rs!PRESTA_CODIGO & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ""
                                        CmdGlobal2.ExecuteNonQuery()
                                    End While
                                End If
                                Rs.Close()
                            Else 'ccosto
                                CmdGlobal.CommandText = " SELECT A.PRESTA_CODIGO, ISNULL(B.PREDET_CANT_PRESTADA,0) AS PREDET_CANT_PRESTADA FROM TBINV_PRESTAMO A INNER JOIN TBINV_PRESTAMO_DETALLE_SINSERIE B ON A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO WHERE (A.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                    & " AND (B.PREDET_ESTADO_PRESTAMO IN ('1','2')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & lblCodDestino.Text & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & lblCodOrigen.Text & ") AND (B.ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ") ORDER BY A.PRESTA_CODIGO"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        If Rs!PREDET_CANT_PRESTADA < QSalida Then
                                            QxDev = Rs!PREDET_CANT_PRESTADA
                                            QSalida = QSalida - QxDev
                                        Else
                                            QxDev = QSalida
                                        End If
                                        CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_ESTADO_PRESTAMO WHEN '1' THEN '2' ELSE PREDET_ESTADO_PRESTAMO END), " _
                                                             & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) + " & QxDev & ", " _
                                                             & " OSAL_CODIGO_DEVOL =" & lblCodigo.Text & ",DESP_CODIGO_DEVOL = NULL,RECEP_CODIGO_DEVOL = NULL WHERE PRESTA_CODIGO = " & Rs!PRESTA_CODIGO & " AND EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND ARTICULO_CODIGO = " & .Rows(i).Cells(1).Text & ""
                                        CmdGlobal2.ExecuteNonQuery()
                                    End While
                                End If
                                Rs.Close()
                            End If
                        Next
                    End With
                Case 4
                Case 5
                Case 6
                Case 7
                Case 8
                Case 9
                Case 10
                Case 11
                    'paso 3
                    'cambiara el estado1 de 2: equipo reemplazante recibido a 3: equipo reemplazado enviado y se ingresara el nro de salida del CC
                Case 12 'DEVOLUCION REEMPLAZO X CAMBIO
                    With _DetalleEq
                        For i = 0 To .Rows.Count - 1
                            CmdGlobal.CommandText = "SELECT * FROM TBINV_REEMPLAZOS WHERE REEM_NRO = '" & .Rows(i).Cells(8).Text & "' AND REEM_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = "UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='3' , REEM_SYS_MOD='" & ValorSys & "',NRO_SALIDA_CC='" & lblCodigo.Text & "' WHERE REEM_NRO='" & .Rows(i).Cells(8).Text & "' AND REEM_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()
                        Next
                    End With
                    With _DetalleAc
                        For i = 1 To .Rows.Count - 1
                            Dim tCantDetAc As TextBox = .Rows(i).Cells(4).FindControl("txtCantSal")
                            QSalida = tCantDetAc.Text
                            If OptDestino.SelectedValue = "1" Then 'almacen
                                CmdGlobal2.CommandText = " SELECT ISNULL(REEMSIN_CANT_REEMPLAZADA,0) AS REEMSIN_CANT_REEMPLAZADA,REEMSIN_NRO FROM TBINV_REEMPLAZOS_SINSERIE WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                    & " AND (REEMSIN_ESTADO_2 IN ('1','2','4')) AND (REEMSIN_TIPO_ORIGEN = '1') AND (REEMSIN_COD_ORIGEN =" & lblCodDestino.Text & ") AND (REEMSIN_TIPO_DESTINO = '2') AND (REEMSIN_COD_DESTINO = " & lblCodOrigen.Text & ") AND (ART_CODIGO = " & .Rows(i).Cells(1).Text & ") "
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        If Rs!REEMSIN_CANT_REEMPLAZADA < QSalida Then
                                            QxDev = Rs!REEMSIN_CANT_REEMPLAZADA
                                            QSalida = QSalida - QxDev
                                        Else
                                            QxDev = QSalida
                                        End If
                                        CmdGlobal2.CommandText = " UPDATE TBINV_REEMPLAZOS_SINSERIE SET REEMSIN_ESTADO_2 = (CASE REEMSIN_ESTADO_2 WHEN '1' THEN '2' ELSE REEMSIN_ESTADO_2 END), " _
                                                              & " REEMSIN_CANT_XDEVOLVER = ISNULL(REEMSIN_CANT_XDEVOLVER,0) + " & QxDev & ", REEMSIN_FECHA_DEVOL='" & FechaServer & "'," _
                                                              & " SALIDA_CC =" & lblCodigo.Text & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND REEMSIN_NRO='" & Rs!REEMSIN_NRO & "'"
                                        CmdGlobal2.ExecuteNonQuery()
                                    End While
                                End If
                                Rs.Close()
                            Else
                            End If
                        Next
                    End With
                    'paso3
                    'cambiara el estado1 de la averia de 0:equipo averiado no enviado a 1:equipo averiado enviado ademas se ingresaran la fecha, falla, detalle dependiendo del nro de la averia
                    'en la tabla de reemplazos cambiara el estado1 de 2: equipo reemplazante recibido a 3: equipo reemplazado enviado y se ingresara el nro de salida del CC dependiendo del nro de reemplazo
                Case 13 'DEVOLUCION X AVERIA
                    With _DetalleEq
                        For i = 0 To .Rows.Count - 1
                            Dim cAveria As DropDownList = .Rows(i).Cells(10).FindControl("cboAveria")
                            Dim tDetAveria As TextBox = .Rows(i).Cells(12).FindControl("txtDetAveria")
                            CmdGlobal2.CommandText = "SELECT * FROM TBINV_AVERIA WHERE AVERIA_NRO ='" & .Rows(i).Cells(9).Text & "' AND AVERIA_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='1' ,AVERIA_FECHA ='" & Format(FechaSal, "yyyymmdd") & "',AVERIA_TIPO ='" & cAveria.SelectedValue & "', " _
                                                  & " AVERIA_DETALLE_USUARIO='" & tDetAveria.Text.Trim & "', AVERIA_SYS_MOD='" & ValorSys & "', SALIDA_NRO ='" & lblCodigo.Text & "' WHERE AVERIA_NRO ='" & .Rows(i).Cells(9).Text & "' AND AVERIA_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()
                            CmdGlobal2.CommandText = "SELECT * FROM TBINV_REEMPLAZOS WHERE REEM_NRO = '" & .Rows(i).Cells(8).Text & "' AND REEM_SYS_EST='0' AND AVERIA_NRO ='" & .Rows(i).Cells(9).Text & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='3' , REEM_SYS_MOD='" & ValorSys & "',NRO_SALIDA_CC='" & lblCodigo.Text & "' WHERE REEM_NRO='" & .Rows(i).Cells(8).Text & "' AND REEM_SYS_EST='0' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()
                        Next
                    End With
                Case 17
                    Dim NroAveria As Long = 1
                    With _DetalleEq
                        For i = 0 To .Rows.Count - 1
                            CmdGlobal2.CommandText = "SELECT MAX(AVERIA_NRO) FROM TBINV_AVERIA"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    NroAveria = Nz(Rs(0)) + 1
                                End While
                            End If
                            Rs.Close()
                            Dim cAveria As DropDownList = .Rows(i).Cells(10).FindControl("cboAveria")
                            Dim tDetAveria As TextBox = .Rows(i).Cells(12).FindControl("txtDetAveria")
                            CmdGlobal2.CommandText = " INSERT INTO TBINV_AVERIA (EMPRESA_CODIGO, AVERIA_NRO, AVERIA_FECHA, AVERIA_TIPO, SALIDA_NRO, " _
                                                  & " AVERIA_DETALLE_USUARIO, AVERIA_SERIE_NUMERAR, AVERIA_ESTADO_1, AVERIA_ESTADO_2, AVERIA_TIPO_ORIGEN, AVERIA_CODIGO_ORIGEN, " _
                                                  & " AVERIA_TIPO_DESTINO, AVERIA_CODIGO_DESTINO, AVERIA_SYS_CRE, AVERIA_SYS_EST,AVERIA_DEVOLVER_CC) " _
                                                  & " VALUES ('" & Session("CodEmpresa") & "'," & NroAveria & ",'" & FechaServer & "','" & cAveria.SelectedValue & "','" & lblCodigo.Text & "', " _
                                                  & " '" & tDetAveria.Text.Trim & "','" & .Rows(i).Cells(5).Text & "','1','1','2','" & lblCodOrigen.Text & "', " _
                                                  & " '" & OptDestino.SelectedValue & "','" & lblCodDestino.Text & "','" & ValorSys & "','0','1')"
                            CmdGlobal2.ExecuteNonQuery()
                        Next
                    End With
            End Select

            Dim objProceso As New clsInv_Procesos
            If Session("TicketNro") <> "" Then
                Dim psNroTicket As String = ""
                Dim psConexion2 As String = ""
                psConexion2 = Session("Ruta_Emp")
                psNroTicket = Session("TicketNro")
                objProceso.Guardar_RelacionTicket(psConexion2, psNroTicket, "24", Nz(lblCodigo.Text), Session("User"))

                CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_TICKET = " & psNroTicket & " WHERE OSAL_CODIGO = " & lblCodigo.Text
                CmdGlobal.ExecuteNonQuery()
            End If
            objProceso.Recepcion_Automatica_CentroCosto(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodigo.Text, HttpContext.Current.User.Identity.Name, "", "")
            'Response.Redirect("Inventario_CCosto_Salidas.aspx")

            Session("CodSalida") = lblCodigo.Text.Trim

            LblTituloModal.Text = "Nro. Salida de C.Costo " & Llenar_Ceros(Session("CodSalida"), 6)
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
            _Ubica4.Visible = False
            Call FlexBusBlanco(_BusEq.ID)
            Call FlexBusBlanco(_DetalleEq.ID)
            Call FlexBusBlanco(_BusAc.ID)
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
            Session("TicketNro") = String.Empty
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
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

        Session("TipoSalida") = "2"
        CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_TIPO_DOC_SALIDA = '1' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & Session("CodSalida")
        CmdGlobal.ExecuteNonQuery()


        Session("ProcesoEjecutado") = Nothing
        Dim valor As String = Session("CodSalida")
        Dim valor2 As String = Session("TipoSalida")
        Session("PaginaViene") = "Inventario_CCosto_Salida.aspx"

        ' Redireccionar a la página de destino pasando el valor como parámetro de consulta en la URL
        Response.Redirect("~/Inventario/Inventario_GenerarGuia.aspx?parametro=" & Server.UrlEncode(valor) & "&parametro2=" & Server.UrlEncode(valor2))

    End Sub

    Protected Sub btnRedirectNo_Click(sender As Object, e As EventArgs) Handles btnRedirectNo.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        If Session("CodSalida") = "" Then Exit Sub
        Session("TipoGuia") = "2"
        Cn.Open() : CmdGlobal.Connection = Cn
        Session("TipoSalida") = "2"
        CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_TIPO_DOC_SALIDA = '2' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & Session("CodSalida")
        CmdGlobal.ExecuteNonQuery()

        'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModalGuia').modal('hide');", True)

        Session("ProcesoEjecutado") = Nothing
        Dim valor As String = Session("CodSalida")
        Dim valor2 As String = Session("TipoSalida")
        Session("PaginaViene") = "Inventario_CCosto_Salida.aspx"
        ' Redireccionar a la página de destino pasando el valor como parámetro de consulta en la URL
        Response.Redirect("~/Inventario/Inventario_GenerarGuia.aspx?parametro=" & Server.UrlEncode(valor) & "&parametro2=" & Server.UrlEncode(valor2))

    End Sub
    Protected Sub _Ubica1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Ubica1.Click
        lblError.Text = ""
        lblBusCentroCosto.Visible = True
        lblEtq_BusDestino.Text = "Busqueda de Centro de Costo"
        Session("TipoBus") = "Origen"
        txtOrigDescrip.Text = ""
        txtOrigCodExt.Text = ""
        lblCodOrigen.Text = ""
        lblBusCentroCosto.Visible = True
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As New clsInv_Listados
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            Dim pdCodAlmacen As Double = 0
            If Session("TipoBus") = "Origen" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
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
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
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
                FlexUbicacion.DataBind()
                lblBusCentroCosto.Visible = False
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
                lblBusCentroCosto.Visible = False
            End If
        End If
    End Sub
    Protected Sub _Ubica2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Ubica2.Click
        If txtOrigCodExt.Text = "" And lblCodOrigen.Text = "" Then lblError.Text = "Debe ingresar el Origen." : Exit Sub
        lblBusCentroCosto.Visible = True
        If Session("TipoDestino") = "Almacen" Then
            lblEtq_BusDestino.Text = "Busqueda de Almacén"
        ElseIf Session("TipoDestino") = "CentroCosto" Then
            lblEtq_BusDestino.Text = "Busqueda de Centro de Costos"
        End If
        Session("TipoBus") = "Destino"
        txtDesDescrip.Text = ""
        txtDesCodExterno.Text = ""
        lblCodDestino.Text = ""
        lblBusCentroCosto.Visible = True
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
    End Sub
    Protected Sub btnUbiCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblBusCentroCosto.Visible = False
    End Sub
    Protected Sub _Ubica3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Ubica3.Click
        If lblFecIni.Visible = True Then
            lblFecIni.Visible = False
        Else
            lblFecIni.Visible = True
        End If
    End Sub
    Protected Sub _Ubica4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles _Ubica4.Click
        If lblFechafin.Visible = True Then
            lblFechafin.Visible = False
        Else
            lblFechafin.Visible = True
        End If
    End Sub
    Protected Sub dtpFechaFin_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFechaDevol.Text = Format(Me.dtpFechaFin.SelectedDate, "dd/MM/yyyy")
        lblFechafin.Visible = False
    End Sub
    Protected Sub dtpFecha_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtFecha.Text = Format(Me.dtpFecha.SelectedDate, "dd/MM/yyyy")
        lblFecIni.Visible = False
    End Sub
End Class
