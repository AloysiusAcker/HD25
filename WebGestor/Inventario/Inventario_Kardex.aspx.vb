Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inventario_Kardex
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("UnaVez") = "NO"
            Session("UnaVezLista") = "NO"
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim fInv As New clsInv_Procesos
        Dim psArticulo As String = ""
        Dim pdCodUbica As Double = 0
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim dRow As DataRow
        Dim psCodArticulo As String = ""
        Dim pdSaldo As Double = 0
        Dim ListaArt As String = ""
        Dim psFecha As String = ""
        Dim psFechaanterior As String = ""
        Dim psFechaDate As Date = Date.Now
        Dim psFechaFin As String = ""
        Try
            psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
            If TxtFecha.Text = "" Then
                psFecha = "20230101"
            Else
                psFechaDate = DateAdd("d", -1, CDate(TxtFecha.Text))
                psFechaanterior = Mid(psFechaDate, 7, 4) + Mid(psFechaDate, 4, 2) + Mid(psFechaDate, 1, 2)
            End If
            If TxtFechaFin.Text = "" Then
                psFechaFin = FechaActual()
            Else
                psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
            End If
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            LblError.Text = ""
            If cboUbica.SelectedValue = "< Seleccionar >" Then LblError.Text = "<br> - Seleccionar Tipo de Movimiento."
            'If txtUbicaCodigo.Text = "" Then lblError.Text = lblError.Text & "<br> - Seleccionar Ubicación."
            If txtUbicaCodigo.Text <> "" Then pdCodUbica = Nz(txtUbicaCodigo.Text.Trim)
            If txtArtCodigo.Text.Trim = "" Then
                '    If Left(fInv.Lista_Articulos_xClasif(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name), 13) = "En la función" Then
                '        lblError.Text = lblError.Text & "<br> - " & fInv.Lista_Articulos_xClasif(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
                '    Else
                '        ListaArt = fInv.Lista_Articulos_xClasif(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
                '    End If
            ElseIf txtArtCodigo.Text.Trim <> "" Then
                '    If Left(fInv.Verificar_ArtExiste(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim), 13) = "En la función" Then
                '        lblError.Text = lblError.Text & "<br> - " & fInv.Verificar_ArtExiste(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim)
                '    ElseIf fInv.Verificar_ArtExiste(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim) = "SI" Then
                ListaArt = txtArtCodigo.Text.Trim
                '    Else
                '        lblError.Text = lblError.Text & "<br> - No se encontró artículo."
                '    End If
            End If
            'If ListaArt = "" Then LblError.Text = LblError.Text & "<br> - No se encontró artículo."
            If LblError.Text <> "" Then
                LblError.Text = "Se han encontrado las sgtes. observaciones: " & LblError.Text.Trim
                Exit Sub
            End If
            dt.Columns.Add("MOV_ARTCODIGO")
            dt.Columns.Add("MOV_ARTDESCRIPCION")
            dt.Columns.Add("MOV_FECHA")
            dt.Columns.Add("MOV_INGRESO")
            dt.Columns.Add("MOV_SALIDA")
            dt.Columns.Add("MOV_SALDO")
            dt.Columns.Add("MOV_ORIGEN_DESTINO")
            dt.Columns.Add("MOV_MOTIVO")
            dt.Columns.Add("MOV_STOCK")

            Dim Cn As New SqlConnection(Session("Ruta_Emp"))
            Dim cmdSql As New SqlCommand
            Dim dr As SqlDataReader
            Dim pscodArt As Double = 0
            Dim psTipoUbica As String = ""
            Dim psCodUbica As Double = 0
            If txtArtCodigo.Text <> "" Then
                pscodArt = txtArtCodigo.Text
            End If
            If cboUbica.SelectedValue <> "< Seleccionar >" Then
                psTipoUbica = cboUbica.SelectedValue
            End If
            If txtUbicaCodigo.Text <> "" Then psCodUbica = Nz(txtUbicaCodigo.Text.Trim)
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = " select * from  fu_Lista_Kardex_Cantidades_22 ('" & Session("CodEmpresa") & "', " & pscodArt & ", '" & psTipoUbica & "', " & psCodUbica & " , " & psFecha & ", '" & psFechaFin & "','" & psFechaanterior & "') " _
                               & " order by k_art_nombre, k_art_fecha , k_art_tipo_mov"
            dr = cmdSql.ExecuteReader
            If dr.HasRows Then
                While dr.Read '
                    If psCodArticulo <> "" Then
                        If Nu(dr("k_art_codigo")) <> psCodArticulo Then
                            dRow = dt.NewRow
                            dRow("MOV_ARTCODIGO") = "-----------------"
                            dRow("MOV_ARTDESCRIPCION") = "------------------------------------------------------------"
                            dRow("MOV_FECHA") = "-----------------"
                            dRow("MOV_INGRESO") = "------------"
                            dRow("MOV_SALIDA") = "------------"
                            dRow("MOV_SALDO") = "------------"
                            dRow("MOV_ORIGEN_DESTINO") = "------------------------------------------------------------"
                            dRow("MOV_MOTIVO") = "-----------------------------------------------"
                            dRow("MOV_STOCK") = ""
                            dt.Rows.Add(dRow)
                            pdSaldo = 0
                        End If
                    End If
                    dRow = dt.NewRow
                    dRow("MOV_ARTCODIGO") = Formato_Digito(Nu(dr("k_art_codigo")), 8)
                    dRow("MOV_ARTDESCRIPCION") = Nu(dr("k_art_nombre"))
                    dRow("MOV_FECHA") = Nu(dr("k_art_fecha_mov"))
                    dRow("MOV_INGRESO") = IIf(Nu(dr("k_art_tipo_mov")) = "1", Nz(dr("k_art_cant_ent")), IIf(Nu(dr("k_art_tipo_mov")) = "3", Nz(dr("k_art_cant_ent")), "--"))
                    dRow("MOV_SALIDA") = IIf(Nu(dr("k_art_tipo_mov")) = "2", Nz(dr("k_art_cant_sal")), IIf(Nu(dr("k_art_tipo_mov")) = "4", Nz(dr("k_art_cant_sal")), "--"))
                    dRow("MOV_SALDO") = Nz(dr("k_art_saldo")) 'k_art_destino_origen
                    If Nu(dr("k_art_codigo")) <> psCodArticulo Then pdSaldo = 0
                    If Nu(dr("k_art_tipo_mov")) = "1" Then
                        pdSaldo = pdSaldo + Nz(dr("k_art_cant_ent"))
                        dRow("MOV_SALDO") = pdSaldo
                    ElseIf Nu(dr("k_art_tipo_mov")) = "2" Then
                        pdSaldo = pdSaldo - Nz(dr("k_art_cant_sal"))
                        dRow("MOV_SALDO") = pdSaldo
                    ElseIf Nu(dr("k_art_tipo_mov")) = "3" Then
                        pdSaldo = pdSaldo + Nz(dr("k_art_cant_ent"))
                        dRow("MOV_SALDO") = pdSaldo
                    ElseIf Nu(dr("k_art_tipo_mov")) = "4" Then
                        pdSaldo = pdSaldo - Nz(dr("k_art_cant_sal"))
                        dRow("MOV_SALDO") = pdSaldo
                    End If
                    dRow("MOV_ORIGEN_DESTINO") = Nu(dr("k_art_destino_origen"))

                    dRow("MOV_MOTIVO") = Nu(dr("k_art_motivo"))
                    psCodArticulo = Nu(dr("k_art_codigo"))
                    If dRow("MOV_INGRESO") <> "" Or dRow("MOV_SALIDA") <> "" Then
                        dt.Rows.Add(dRow)
                    End If
                    dRow("MOV_STOCK") = Nu(dr("K_ART_STOCK_ACTUAL"))
                End While
            End If
            dr.Close()

            'dtLista = obj.Lista_Kardex(psConexion, Session("CodEmpresa"), cboUbica.SelectedValue.Trim, pdCodUbica, ListaArt)
            'If dtLista.Rows.Count > 0 Then
            '    For Each dr As DataRow In dtLista.Rows
            '        If psCodArticulo <> "" Then
            '            If Nu(dr("CODIGO_ARTICULO")) <> psCodArticulo Then
            '                dRow = dt.NewRow
            '                dRow("MOV_ARTCODIGO") = "-----------------"
            '                dRow("MOV_ARTDESCRIPCION") = "------------------------------------------------------------"
            '                dRow("MOV_FECHA") = "-----------------"
            '                dRow("MOV_INGRESO") = "------------"
            '                dRow("MOV_SALIDA") = "------------"
            '                dRow("MOV_SALDO") = "------------"
            '                dRow("MOV_ORIGEN_DESTINO") = "------------------------------------------------------------"
            '                dRow("MOV_MOTIVO") = "-----------------------------------------------"
            '                dt.Rows.Add(dRow)
            '            End If
            '        End If
            '        dRow = dt.NewRow
            '        dRow("MOV_ARTCODIGO") = Formato_Digito(Nu(dr("CODIGO_ARTICULO")), 8)
            '        dRow("MOV_ARTDESCRIPCION") = Nu(dr("ART_DESCRIPCION"))
            '        dRow("MOV_FECHA") = Nu(dr("FECHA_MOV"))
            '        dRow("MOV_INGRESO") = IIf(Nu(dr("MOV_TIPO")) = "1", Nz(dr("NRO_ARTICULO")), IIf(Nu(dr("MOV_TIPO")) = "3", Nz(dr("NRO_ARTICULO")), "--"))
            '        dRow("MOV_SALIDA") = IIf(Nu(dr("MOV_TIPO")) = "2", Nz(dr("NRO_ARTICULO")), IIf(Nu(dr("MOV_TIPO")) = "4", Nz(dr("NRO_ARTICULO")), "--"))
            '        If Nu(dr("CODIGO_ARTICULO")) <> psCodArticulo Then pdSaldo = 0
            '        If Nu(dr("MOV_TIPO")) = "1" Then
            '            pdSaldo = pdSaldo + Nz(dr("NRO_ARTICULO"))
            '            dRow("MOV_SALDO") = pdSaldo
            '        ElseIf Nu(dr("MOV_TIPO")) = "2" Then
            '            pdSaldo = pdSaldo - Nz(dr("NRO_ARTICULO"))
            '            dRow("MOV_SALDO") = pdSaldo
            '        ElseIf Nu(dr("MOV_TIPO")) = "3" Then
            '            pdSaldo = pdSaldo + Nz(dr("NRO_ARTICULO"))
            '            dRow("MOV_SALDO") = pdSaldo
            '        ElseIf Nu(dr("MOV_TIPO")) = "4" Then
            '            pdSaldo = pdSaldo - Nz(dr("NRO_ARTICULO"))
            '            dRow("MOV_SALDO") = pdSaldo
            '        End If
            '        If Nu(dr("MOV_TIPO")) = "1" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "1" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("Almacen"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "1" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "2" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("CCosto"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "1" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "3" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("Proveedor"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "2" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "4" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("Equipo")) & " - " & Nu(dr("SERIE"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "2" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "1" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("Almacen"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "2" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "2" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("CCosto"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "2" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "3" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("Proveedor"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "3" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "4" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("Equipo")) & " - " & Nu(dr("SERIE"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "4" And Nu(dr("TIPO_ORIGEN_DESTINO")) = "4" Then
            '            dRow("MOV_ORIGEN_DESTINO") = Nu(dr("Equipo")) & " - " & Nu(dr("SERIE"))
            '        End If
            '        If Nu(dr("MOV_TIPO")) = "2" Then
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_SALIDA"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "1" And Nu(dr("TIPO_UBICACT")) = "1" Then
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_ENTRADA_ALM"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "1" And Nu(dr("TIPO_UBICACT")) = "2" Then
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_ENTRADA_CC"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "3" And Nu(dr("TIPO_UBICACT")) = "4" Then
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_ENTCOM"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "3" And Nu(dr("TIPO_UBICACT")) = "1" Then
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_ENTCOM1"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "4" And Nu(dr("TIPO_UBICACT")) = "4" Then
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_SALCOM"))
            '        ElseIf Nu(dr("MOV_TIPO")) = "4" And Nu(dr("TIPO_UBICACT")) = "1" Then
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_SALCOM_ALM"))
            '        Else
            '            dRow("MOV_MOTIVO") = Nu(dr("MOTIVO_ENTRADA_CC2"))
            '        End If
            '        psCodArticulo = Nu(dr("CODIGO_ARTICULO"))
            '        dt.Rows.Add(dRow)
            '    Next
            'End If
            Flex.DataSource = dt
            Flex.DataBind()
            Call Calcular_Saldo()
            LblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
        Catch ex As SqlException
            LblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            LblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Private Sub Calcular_Saldo()
        Dim Saldo As Integer = 0
        Dim pTotalAnt As Double : pTotalAnt = 0
        Dim pCantAnt As Double : pCantAnt = 0
        Dim pPrecioUnitAnt As Double : pPrecioUnitAnt = 0
        Dim pdPrecio As Double : pdPrecio = 0
        Dim CodArt As String
        With Flex
            For i = 0 To .Rows.Count - 1
                If Flex.Rows(i).Cells(0).Text <> "-----------------" And Flex.Rows(i).Cells(0).Text <> "" Then
                    CodArt = Flex.Rows(i).Cells(0).Text
                    If CodArt <> Flex.Rows(i).Cells(0).Text Then
                        pCantAnt = 0
                        pPrecioUnitAnt = 0
                        pTotalAnt = 0
                    End If '
                    If Flex.Rows(i).Cells(3).Text <> "--" Then
                        Flex.Rows(i).Cells(5).Text = Format(pCantAnt + CDbl(Nz(Flex.Rows(i).Cells(3).Text)), "0.##0")
                    ElseIf Flex.Rows(i).Cells(4).Text <> "--" Then
                        Flex.Rows(i).Cells(5).Text = Format(pCantAnt - CDbl(Nz(Flex.Rows(i).Cells(4).Text)), "0.##0")
                    End If
                    pCantAnt = Nz(Flex.Rows(i).Cells(5).Text)
                Else
                    pCantAnt = 0
                    pPrecioUnitAnt = 0
                    pTotalAnt = 0
                End If
            Next
        End With



    End Sub
    Protected Sub cboUbica_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUbica.SelectedIndexChanged
        If cboUbica.SelectedValue.Trim = "1" Then
            lblEtiqUbicacion.Text = "Búsqueda de Almacén"
            btnBusUbicacion.Enabled = True
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboUbica.SelectedValue.Trim = "2" Then
            lblEtiqUbicacion.Text = "Búsqueda de Sección"
            btnBusUbicacion.Enabled = True
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboUbica.SelectedValue.Trim = "< Seleccionar >" Then
            lblEtiqUbicacion.Text = "Búsqueda"
            btnBusUbicacion.Enabled = False
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        End If
    End Sub
    Protected Sub btnUbicListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbicListar.Click
        Dim obj As New clsInv_Listados
        Dim pdCodAlmacen As Double = 0
        Try
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            If cboUbica.SelectedValue.Trim = "2" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusUbicCodInterno.Value.Trim, txtBusUbicDescripcion.Value.Trim)
                FlexUbicacion.DataBind()
            ElseIf cboUbica.SelectedValue.Trim = "1" Then
                If txtBusUbicCodInterno.Value.Trim <> "" Then pdCodAlmacen = txtBusUbicCodInterno.Value.Trim
                FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusUbicDescripcion.Value.Trim)
                FlexUbicacion.DataBind()
            End If
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnListarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarArt.Click
        Try
            Dim obj As New clsInv_Listados
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            If txtPArtCodigo.Value.Trim <> "" Then pdCodArt = txtPArtCodigo.Value.Trim
            FlexArt.DataSource = obj.BuscarX_Articulos(psConexion, Session("CodEmpresa"), pdCodArt, txtPArtDescripcion.Value.Trim, "")
            FlexArt.DataBind()
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub

    Protected Sub FlexArt_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexArt.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtArtDescripcion.Text = ""
            txtArtCodigo.Text = ""
            txtArtCodigo.Text = FlexArt.Rows(Index).Cells(1).Text
            txtArtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexArt.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtPArtCodigo.Value = ""
            txtPArtDescripcion.value = ""
            FlexArt.DataSource = Nothing
            FlexArt.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
        End If
    End Sub
    Protected Sub btnCerrarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrarArt.Click
        txtPArtCodigo.Value = ""
        txtPArtDescripcion.Value = ""
        FlexArt.DataSource = Nothing
        FlexArt.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
    End Sub
    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLimpiar.Click
        txtArtDescripcion.Text = ""
        txtArtCodigo.Text = ""
        txtUbicaCodigo.Text = ""
        txtUbicaCodInterno.Text = ""
        txtUbicaCodigo.Text = ""
        LblError.Text = ""
        txtUbicaDescripcion.Text = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        LblRegistro.Text = ""
        txtBusUbicCodInterno.Value = ""
        txtBusUbicDescripcion.Value = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        cboUbica.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub btnBusUbicacion_Click(sender As Object, e As EventArgs) Handles btnBusUbicacion.Click
        lblEtiqUbicacion2.Text = lblEtiqUbicacion.Text
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub btnUbicCerrar_Click(sender As Object, e As EventArgs) Handles btnUbicCerrar.Click
        Call Limpiar_Popup()
    End Sub
    Protected Sub Limpiar_Popup()
        txtBusUbicCodInterno.Value = ""
        txtBusUbicDescripcion.Value = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('hide');", True)
    End Sub

    Private Sub FlexUbicacion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Try
            txtUbicaCodInterno.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtUbicaDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtUbicaCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub

    Private Sub btnBusArticulo_Click(sender As Object, e As EventArgs) Handles btnBusArticulo.Click
        Session("BuscarArticulo") = "Si"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Call Exportar()
    End Sub
    Private Sub Exportar()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=Kardex.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
