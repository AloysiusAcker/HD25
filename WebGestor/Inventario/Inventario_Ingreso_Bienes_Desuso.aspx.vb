Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports OfficeOpenXml

Partial Class Inventario_Inventario_Ingreso_Bienes_Desuso
    Inherits System.Web.UI.Page
    Dim CodSalida As Double = 0
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim NroTicket As String = Convert.ToString(Request.QueryString("WpkDi"))
            If NroTicket <> "" Then
                Session("TicketNro") = NroTicket
            Else
                Session("TicketNro") = String.Empty
            End If
            Dim obj As New Cls_Catalogo
            Dim dt As New DataTable
            Dim psconexion As String = Session("Ruta_Emp")
            txtFecRegistra.Text = FormatoFecha(FechaActual)
            txtHoraRegistra.Text = FormatoHoraSeg(HoraActual(True))
            txtFechaRecep.Text = FormatoFecha(FechaActual)
            dt = obj.Lista_Tipo(psconexion)
            Call Carga_Motivos("S", "1", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
            Call LlenaComboItem("TBOPC062", cboTipoDoc)
            Me.Page.Session.Timeout = 1080
            Session("TipoDestino") = "Almacen"
            Llenar_Combos()
            Lista_Bienes_No_Procesados()
            Dim lst1 As New ListItem : lst1.Text = "Despacho" : lst1.Value = 1 : DdlServicio.Items.Add(lst1)
            Dim lst2 As New ListItem : lst2.Text = "Recepción" : lst2.Value = 2 : DdlServicio.Items.Add(lst2)
            DdlServicio.Items.Add("< Seleccionar >") : DdlServicio.SelectedValue = "< Seleccionar >"
            Call LlenaComboItem("TBOPC365", DdlOperativa)
            Call LlenaComboItem("TBOPC364", DdlEstado)
        End If
    End Sub

    Private Sub Lista_Bienes_No_Procesados()

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim RsL As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2

        Dim dt As New DataTable
        Dim drT As DataRow
        dt.Columns.Add("COD_ARTICULO")
        dt.Columns.Add("ART_CODEQUIVA")
        dt.Columns.Add("ART_DESCRIPCION")
        dt.Columns.Add("CANT")
        dt.Columns.Add("SERIE_NRO")
        dt.Columns.Add("PLACA_NRO")
        dt.Columns.Add("TipoBien")
        dt.Columns.Add("TIPO_UBICACION")
        dt.Columns.Add("COD_ALMACEN")
        dt.Columns.Add("ALMACEN_NOMBRE")
        dt.Columns.Add("SERIE_FECHA_ADQ")
        dt.Columns.Add("SERIE_SKU")
        dt.Columns.Add("SERIE_ORDEN_COMPRA")
        dt.Columns.Add("SERIE_GUIA")
        dt.Columns.Add("ARTICULO_REFERENCIA")
        dt.Columns.Add("Ubicact_tipo")
        dt.Columns.Add("Ubicact_codigo")
        dt.Columns.Add("serie_numerar")
        dt.Columns.Add("art_tipo")
        dt.Columns.Add("CS")
        dt.Columns.Add("Fecha_Servicio")
        dt.Columns.Add("Centro_Costo")

        Dim dtDatos As New DataTable
        Dim obj As New clsInv_Listados
        Dim numerar As Double = 0

        CmdGlobal.CommandText = " SELECT SERIE_NUMERAR,SERIE_NRO, PLACA_NRO, A.ART_CODIGO, A.ART_DESCRIPCION,ART_CODEQUIVA,BIEN_NUEVO,PLACA_TSG, CANTIDAD " _
                              & " FROM V_TBINV_LISTASERIE_" & Session("User") & " AS U INNER JOIN  TBINV_ARTICULOS AS A ON A.ART_CODIGO = U.ART_CODIGO " _
                              & " ORDER BY CORRELATIVO DESC"
        RsL = CmdGlobal.ExecuteReader
        If RsL.HasRows Then
            While RsL.Read
                If Nu(RsL!PLACA_NRO) <> "" Or Nu(RsL!Serie_Nro) <> "" Then
                    dtDatos = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), Nu(RsL!Serie_Nro), Nz(RsL!PLACA_NRO))

                    If dtDatos.Rows.Count > 0 Then
                        For Each drow As DataRow In dtDatos.Rows
                            drT = dt.NewRow()
                            drT("COD_ARTICULO") = Nu(drow("COD_ARTICULO"))
                            drT("ART_CODEQUIVA") = Nu(drow("ART_CODEQUIVA"))
                            drT("ART_DESCRIPCION") = Nu(drow("ART_DESCRIPCION"))
                            drT("CANT") = 1
                            drT("SERIE_NRO") = Nu(drow("SERIE_NRO"))
                            drT("PLACA_NRO") = Nu(drow("PLACA_NRO"))
                            drT("TipoBien") = Nu(drow("TipoBien"))
                            drT("TIPO_UBICACION") = Nu(drow("TIPO_UBICACION"))
                            drT("COD_ALMACEN") = Nu(drow("COD_ALMACEN"))
                            drT("ALMACEN_NOMBRE") = Nu(drow("ALMACEN_NOMBRE"))
                            drT("SERIE_FECHA_ADQ") = Nu(drow("SERIE_FECHA_ADQ"))
                            drT("SERIE_SKU") = Nu(drow("SERIE_SKU"))
                            drT("SERIE_ORDEN_COMPRA") = Nu(drow("SERIE_ORDEN_COMPRA"))
                            drT("SERIE_GUIA") = Nu(drow("SERIE_GUIA"))
                            drT("ARTICULO_REFERENCIA") = Nu(drow("ARTICULO_REFERENCIA"))
                            drT("Ubicact_tipo") = Nu(drow("ubicact_tipo"))
                            drT("ubicact_codigo") = Nu(drow("Ubicact_codigo"))
                            drT("serie_numerar") = Nu(drow("serie_numerar"))
                            drT("art_tipo") = Nu(drow("art_tipo"))
                            drT("CS") = Nu(RsL("BIEN_NUEVO"))
                            drT("Fecha_Servicio") = Nu(drow("Fecha_Servicio"))
                            drT("Centro_Costo") = Nu(drow("Centro_Costo"))
                            dt.Rows.Add(drT)
                        Next
                    End If
                End If
            End While
        End If
        RsL.Close()

        GvListaBienes.DataSource = dt
        GvListaBienes.DataBind()
        If dt.Rows.Count > 1 Then
            LblRegistroE.Text = "Hay " & dt.Rows.Count & " registros"
        ElseIf dt.Rows.Count = 1 Then
            LblRegistroE.Text = "Hay 1 registro"
        ElseIf dt.Rows.Count = 0 Then
            LblRegistroE.Text = ""
        End If

    End Sub

    Protected Sub Llenar_Combos()
        Dim objC As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Try
            dt = objC.Lista_Tipo(Session("Ruta_Emp"))
            DdlTipoBA.DataSource = dt
            DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
            DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
            DdlTipoBA.DataBind()
            DdlTipoBA.Items.Add("< Seleccionar >")
            DdlTipoBA.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Sub Carga_Motivos(ByVal TipoMov As String, ByVal TipoOrigen As String, ByVal TipoDestino As String)
        Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
        Dim Cn As New SqlConnection(psConexion)

        'Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        DdlMotivo.Items.Clear()
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            cmdSql.CommandText = " SELECT DISTINCT MAINSA_MOTIVO_TRASLADO, (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC217' AND ELEMEN_CODIGO = MAINSA_MOTIVO_TRASLADO) AS MOTIVO_TRASLADO" _
                               & " FROM TBINV_MATRIZ_INGRESOSALIDA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (MAINSA_TIPO_MOVIMIENTO = 'S') AND (MAINSA_UBICACION1 = '" & TipoOrigen & "') AND " _
                               & " (MAINSA_UBICACION2 = '" & TipoDestino & "') ORDER BY MOTIVO_TRASLADO"
            Rs = cmdSql.ExecuteReader()
            DdlMotivo.DataSource = Rs
            DdlMotivo.DataTextField = "MOTIVO_TRASLADO"
            DdlMotivo.DataValueField = "MAINSA_MOTIVO_TRASLADO"
            DdlMotivo.DataBind()

            DdlMotivo.Items.Add("< Seleccionar >")
            DdlMotivo.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch Ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & Ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        If txtPlaca.Text = "" Then
            '
        End If
    End Sub

    Private Sub txtPlaca_TextChanged(sender As Object, e As EventArgs) Handles txtPlaca.TextChanged
        Dim pdPlaca As Double = 0
        If txtPlaca.Text <> "" Then
            pdPlaca = Nz(txtPlaca.Text)
            Call Recibir_Bien("", "", "", "", "", pdPlaca)
            txtPlaca.Text = ""
        End If
    End Sub
    Private Sub Recibir_Bien(ByVal psSku As String, ByVal psOc As String, ByVal psRef As String, ByVal psGuia As String, ByVal psSerieNro As String,
                             Optional pdPlacaNro As Double = 0, Optional pdCantidad As Double = 0, Optional psFecha As String = "", Optional psCC As String = "")

        Dim obj As New clsInv_Listados
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dt2 As New DataTable
        Dim dtDatos As New DataTable
        Dim placa As Double = 0
        Dim serie As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim drT As DataRow
        Dim drT2 As DataRow
        Dim drT3 As DataRow
        Try
            LblRegistroE.Text = ""
            LblRegistroNE.Text = ""
            LblRegistroCant.Text = ""
            Cn.Open()
            CmdGlobal.Connection = Cn

            dt.Columns.Add("COD_ARTICULO")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("CANT")
            dt.Columns.Add("SERIE_NRO")
            dt.Columns.Add("PLACA_NRO")
            dt.Columns.Add("TipoBien")
            dt.Columns.Add("TIPO_UBICACION")
            dt.Columns.Add("COD_ALMACEN")
            dt.Columns.Add("ALMACEN_NOMBRE")
            dt.Columns.Add("SERIE_FECHA_ADQ")
            dt.Columns.Add("SERIE_SKU")
            dt.Columns.Add("SERIE_ORDEN_COMPRA")
            dt.Columns.Add("SERIE_GUIA")
            dt.Columns.Add("ARTICULO_REFERENCIA")
            dt.Columns.Add("Ubicact_tipo")
            dt.Columns.Add("Ubicact_codigo")
            dt.Columns.Add("serie_numerar")
            dt.Columns.Add("art_tipo")
            dt.Columns.Add("CS")
            dt.Columns.Add("Fecha_Servicio")
            dt.Columns.Add("Centro_Costo")


            dt1.Columns.Add("COD_ARTICULO")
            dt1.Columns.Add("ART_CODEQUIVA")
            dt1.Columns.Add("ART_DESCRIPCION")
            dt1.Columns.Add("CANT")
            dt1.Columns.Add("SERIE_NRO")
            dt1.Columns.Add("PLACA_NRO")
            dt1.Columns.Add("TipoBien")
            dt1.Columns.Add("TIPO_UBICACION")
            dt1.Columns.Add("COD_ALMACEN")
            dt1.Columns.Add("ALMACEN_NOMBRE")
            dt1.Columns.Add("SERIE_FECHA_ADQ")
            dt1.Columns.Add("SERIE_SKU")
            dt1.Columns.Add("SERIE_ORDEN_COMPRA")
            dt1.Columns.Add("SERIE_GUIA")
            dt1.Columns.Add("ARTICULO_REFERENCIA")
            dt1.Columns.Add("Ubicact_tipo")
            dt1.Columns.Add("Ubicact_codigo")
            dt1.Columns.Add("serie_numerar")
            dt1.Columns.Add("art_tipo")
            dt1.Columns.Add("CS")
            dt1.Columns.Add("Fecha_Servicio")
            dt1.Columns.Add("Centro_Costo")

            dt2.Columns.Add("COD_ARTICULO")
            dt2.Columns.Add("ART_CODEQUIVA")
            dt2.Columns.Add("ART_DESCRIPCION")
            dt2.Columns.Add("CANT")
            dt2.Columns.Add("TipoBien")
            dt2.Columns.Add("TIPO_UBICACION")
            dt2.Columns.Add("COD_ALMACEN")
            dt2.Columns.Add("ALMACEN_NOMBRE")
            dt2.Columns.Add("SERIE_FECHA_ADQ")
            dt2.Columns.Add("SERIE_SKU")
            dt2.Columns.Add("SERIE_ORDEN_COMPRA")
            dt2.Columns.Add("SERIE_GUIA")
            dt2.Columns.Add("ARTICULO_REFERENCIA")
            dt2.Columns.Add("Ubicact_tipo")
            dt2.Columns.Add("Ubicact_codigo")
            dt2.Columns.Add("serie_numerar")
            dt2.Columns.Add("art_tipo")
            dt2.Columns.Add("CS")
            dt2.Columns.Add("Fecha_Servicio")
            dt2.Columns.Add("Centro_Costo")


            Dim psCantReg As Double = 0
            Dim psCantReg2 As Double = 0

            For Each row As GridViewRow In GvListaNoEncontrados.Rows
                drT2 = dt1.NewRow()
                psCantReg2 = psCantReg2 + 1
                drT2("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("CANT") = 1
                drT2("SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("SERIE_FECHA_ADQ") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("SERIE_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("SERIE_ORDEN_COMPRA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("SERIE_GUIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("ARTICULO_REFERENCIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("Ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("serie_numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("art_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("CS") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("Fecha_Servicio") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(22).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT2("Centro_Costo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(23).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                dt1.Rows.Add(drT2)
            Next

            For Each row As GridViewRow In GvListaBienes.Rows
                drT = dt.NewRow()
                psCantReg = psCantReg + 1
                drT("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("CANT") = 1
                drT("SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("SERIE_FECHA_ADQ") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("SERIE_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("SERIE_ORDEN_COMPRA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("SERIE_GUIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ARTICULO_REFERENCIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("Ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("serie_numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("art_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("CS") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("Fecha_Servicio") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("Centro_Costo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(22).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                dt.Rows.Add(drT)
            Next

            For Each row As GridViewRow In GvListaCantidades.Rows
                drT3 = dt2.NewRow()
                drT3("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("CANT") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("SERIE_FECHA_ADQ") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("SERIE_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("SERIE_ORDEN_COMPRA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("SERIE_GUIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("ARTICULO_REFERENCIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("Ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("serie_numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("art_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("CS") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("Fecha_Servicio") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT3("Centro_Costo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                dt2.Rows.Add(drT3)
            Next

            dtDatos = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), psSerieNro, pdPlacaNro)
            Dim numerar As Double = 0
            If dtDatos.Rows.Count > 0 Then
                For Each drow As DataRow In dtDatos.Rows
                    drT = dt.NewRow()
                    psCantReg = psCantReg + 1
                    drT("COD_ARTICULO") = Nu(drow("COD_ARTICULO"))
                    drT("ART_CODEQUIVA") = Nu(drow("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drow("ART_DESCRIPCION"))
                    drT("CANT") = 1
                    drT("SERIE_NRO") = Nu(drow("SERIE_NRO"))
                    drT("PLACA_NRO") = Nu(drow("PLACA_NRO"))
                    drT("TipoBien") = Nu(drow("TipoBien"))
                    drT("TIPO_UBICACION") = Nu(drow("TIPO_UBICACION"))
                    drT("COD_ALMACEN") = Nu(drow("COD_ALMACEN"))
                    drT("ALMACEN_NOMBRE") = Nu(drow("ALMACEN_NOMBRE"))
                    drT("SERIE_FECHA_ADQ") = Nu(drow("SERIE_FECHA_ADQ"))
                    If Nu(drow("SERIE_SKU")) = "" Then
                        drT("SERIE_SKU") = psSku
                    Else
                        drT("SERIE_SKU") = Nu(drow("SERIE_SKU"))
                    End If
                    If Nu(drow("SERIE_ORDEN_COMPRA")) = "" Then
                        drT("SERIE_ORDEN_COMPRA") = psOc
                    Else
                        drT("SERIE_ORDEN_COMPRA") = Nu(drow("SERIE_ORDEN_COMPRA"))
                    End If
                    If Nu(drow("SERIE_GUIA")) = "" Then
                        drT("SERIE_GUIA") = psGuia
                    Else
                        drT("SERIE_GUIA") = Nu(drow("SERIE_GUIA"))
                    End If
                    If Nu(drow("ARTICULO_REFERENCIA")) = "" Then
                        drT("ARTICULO_REFERENCIA") = psRef
                    Else
                        drT("ARTICULO_REFERENCIA") = Nu(drow("ARTICULO_REFERENCIA"))
                    End If
                    drT("Ubicact_tipo") = Nu(drow("ubicact_tipo"))
                    drT("ubicact_codigo") = Nu(drow("Ubicact_codigo"))
                    drT("serie_numerar") = Nu(drow("serie_numerar"))
                    drT("art_tipo") = Nu(drow("art_tipo"))
                    If Session("TipoCarga") = "Txt" Then
                        drT("Fecha_Servicio") = txtFechaRecep.Text
                    Else
                        drT("Fecha_Servicio") = psFecha
                    End If
                    drT("Centro_Costo") = psCC
                    dt.Rows.Add(drT)
                Next
            ElseIf dtDatos.Rows.Count = 0 And psSerieNro <> "" And pdPlacaNro <> 0 Then
                If psSerieNro = "" And pdPlacaNro = 0 Then
                Else
                    drT2 = dt1.NewRow()
                    drT2("CANT") = 1
                    drT2("SERIE_NRO") = psSerieNro
                    If pdPlacaNro <> 0 Then
                        drT2("PLACA_NRO") = pdPlacaNro
                    End If
                    drT2("SERIE_SKU") = psSku
                    drT2("SERIE_ORDEN_COMPRA") = psOc
                    drT2("SERIE_GUIA") = psGuia
                    drT2("ARTICULO_REFERENCIA") = psRef
                    drT2("CS") = "S"
                    drT2("Fecha_Servicio") = psFecha
                    drT2("Centro_Costo") = psCC
                    dt1.Rows.Add(drT2)
                End If
            ElseIf dtDatos.Rows.Count = 0 And psSerieNro <> "" And pdPlacaNro = 0 Then
                If psSerieNro = "" And pdPlacaNro = 0 Then
                Else
                    drT2 = dt1.NewRow()
                    drT2("CANT") = 1
                    drT2("SERIE_NRO") = psSerieNro
                    If pdPlacaNro <> 0 Then
                        drT2("PLACA_NRO") = pdPlacaNro
                    End If
                    drT2("SERIE_SKU") = psSku
                    drT2("SERIE_ORDEN_COMPRA") = psOc
                    drT2("SERIE_GUIA") = psGuia
                    drT2("ARTICULO_REFERENCIA") = psRef
                    drT2("CS") = "S"
                    drT2("Fecha_Servicio") = psFecha
                    drT2("Centro_Costo") = psCC
                    dt1.Rows.Add(drT2)
                End If
            End If

            If pdCantidad > 0 Then
                dtDatos = obj.Busca_Articulo_Sku(Session("Ruta_Emp"), Session("CodEmpresa"), psSku)
                If dtDatos.Rows.Count > 0 Then
                    For Each drow As DataRow In dtDatos.Rows
                        drT3 = dt2.NewRow()
                        drT3("COD_ARTICULO") = Nu(drow("COD_ARTICULO"))
                        drT3("ART_CODEQUIVA") = Nu(drow("ART_CODEQUIVA"))
                        drT3("ART_DESCRIPCION") = Nu(drow("ART_DESCRIPCION"))
                        drT3("CANT") = pdCantidad
                        drT3("SERIE_SKU") = psSku
                        drT3("SERIE_ORDEN_COMPRA") = psOc
                        drT3("SERIE_GUIA") = psGuia
                        drT3("ARTICULO_REFERENCIA") = psRef
                        drT3("art_tipo") = Nu(drow("art_tipo"))
                        drT3("CS") = "C"
                        drT3("Fecha_Servicio") = psFecha
                        drT3("Centro_Costo") = psCC
                        dt2.Rows.Add(drT3)
                    Next
                Else
                    drT3 = dt2.NewRow()
                    drT3("CANT") = pdCantidad
                    drT3("SERIE_SKU") = psSku
                    drT3("SERIE_ORDEN_COMPRA") = psOc
                    drT3("SERIE_GUIA") = psGuia
                    drT3("ARTICULO_REFERENCIA") = psRef
                    drT3("CS") = "C"
                    drT3("Fecha_Servicio") = psFecha
                    drT3("Centro_Costo") = psCC
                    dt2.Rows.Add(drT3)
                End If
            End If

            GvListaBienes.DataSource = dt
            GvListaBienes.DataBind()
            GvListaNoEncontrados.DataSource = dt1
            GvListaNoEncontrados.DataBind()
            GvListaCantidades.DataSource = dt2
            GvListaCantidades.DataBind()
            If GvListaBienes.Rows.Count > 0 Then
                GvListaBienes.Visible = True
                If dt.Rows.Count > 1 Then
                    LblRegistroE.Text = "Hay " & dt.Rows.Count & " bienes encontrados"
                ElseIf dt.Rows.Count = 1 Then
                    LblRegistroE.Text = "Hay 1 bien encontrado"
                ElseIf dt.Rows.Count = 0 Then
                    LblRegistroE.Text = ""
                End If
            End If

            If GvListaNoEncontrados.Rows.Count > 0 Then
                GvListaNoEncontrados.Visible = True
                If dt1.Rows.Count > 1 Then
                    LblRegistroNE.Text = "Hay " & dt1.Rows.Count & " registros"
                ElseIf dt1.Rows.Count = 1 Then
                    LblRegistroNE.Text = "Hay 1 bien registros"
                ElseIf dt1.Rows.Count = 0 Then
                    LblRegistroNE.Text = ""
                End If
            End If

            If GvListaCantidades.Rows.Count > 0 Then
                GvListaCantidades.Visible = True
                If dt2.Rows.Count > 1 Then
                    LblRegistroCant.Text = "Hay " & dt2.Rows.Count & " registros"
                ElseIf dt2.Rows.Count = 1 Then
                    LblRegistroCant.Text = "Hay 1 bien registros"
                ElseIf dt2.Rows.Count = 0 Then
                    LblRegistroCant.Text = ""
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Protected Sub txtNroSerie_TextChanged(sender As Object, e As EventArgs) Handles txtNroSerie.TextChanged
        If txtNroSerie.Text <> "" Then
            Call Recibir_Bien("", "", "", "", txtNroSerie.Text)
            txtNroSerie.Text = ""
        End If
    End Sub

    Private Sub BtnCargarPlacas_Click(sender As Object, e As EventArgs) Handles BtnCargarPlacas.Click
        Session("TipoCarga") = ""
        If FileUpload1.HasFile Then
            ' Obtiene el nombre del archivo y su extensión
            Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim fileExtension As String = Path.GetExtension(fileName)
            Dim psDatosCompletos As String = ""
            If DdlServicio.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            If DdlMotivo.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            If DdlEstado.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            If TxtDesCodigo.Text = "" Then psDatosCompletos = "No"
            If cboTipoDoc.Visible = True Then
                If cboTipoDoc.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            End If
            If txtNroDoc.Text = "" Then psDatosCompletos = "No"
            If txtNroOC.Text = "" Then psDatosCompletos = "No"
            Dim psNroGuia As String = ""
            If txtSerieDoc.Text = "" And txtNroDoc.Text <> "" Then
                psNroGuia = txtNroDoc.Text
            ElseIf txtSerieDoc.Text <> "" And txtNroDoc.Text <> "" Then
                psNroGuia = txtSerieDoc.Text & "-" & txtNroDoc.Text
            End If

            If psDatosCompletos = "No" Then
                If DdlServicio.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona Despacho o Recepción.');", True)
                ElseIf DdlMotivo.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un motivo.');", True)
                ElseIf DdlEstado.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona condición.');", True)
                ElseIf TxtDesCodigo.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el destino.');", True)
                ElseIf cboTipoDoc.SelectedValue = "< Seleccionar >" And cboTipoDoc.Visible = True Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un tipo de documento.');", True)
                ElseIf txtNroDoc.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar número de documento.');", True)
                ElseIf txtNroOC.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar número de orden de compra.');", True)
                End If
            Else
                ' Verifica que el archivo sea un archivo de texto
                If fileExtension.ToLower() = ".txt" Then
                    ' Lee el contenido del archivo de texto
                    Dim fileContent As String = ""
                    Using reader As New StreamReader(FileUpload1.PostedFile.InputStream)
                        While Not reader.EndOfStream
                            ' Lee cada línea del archivo y agrega un salto de línea
                            fileContent = reader.ReadLine()
                            ' Actualiza el contenido del UpdatePanel
                            Session("TipoCarga") = "Txt"
                            Call Recibir_Bien("", txtNroOC.Text, "", psNroGuia, "", CDbl(Val(fileContent)))
                        End While
                    End Using
                    '' Muestra el contenido en la página
                Else
                    Session("Fin") = ""
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El archivo seleccionado no es un archivo de texto válido.');", True)
                End If
            End If
        Else
            Session("Fin") = ""
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un archivo.');", True)
        End If
    End Sub
    Private Sub BtnCargarSeries_Click(sender As Object, e As EventArgs) Handles BtnCargarSeries.Click
        Session("TipoCarga") = ""
        If FileUpload1.HasFile Then
            ' Obtiene el nombre del archivo y su extensión
            Dim fileName As String = Path.GetFileName(FileUpload1.PostedFile.FileName)
            Dim fileExtension As String = Path.GetExtension(fileName)
            Dim psDatosCompletos As String = ""
            If DdlServicio.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            If DdlMotivo.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            If DdlEstado.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            If TxtDesCodigo.Text = "" Then psDatosCompletos = "No"
            If cboTipoDoc.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
            If txtNroDoc.Text = "" Then psDatosCompletos = "No"
            If txtNroOC.Text = "" Then psDatosCompletos = "No"
            Dim psNroGuia As String = ""
            If txtSerieDoc.Text = "" And txtNroDoc.Text <> "" Then
                psNroGuia = txtNroDoc.Text
            ElseIf txtSerieDoc.Text <> "" And txtNroDoc.Text <> "" Then
                psNroGuia = txtSerieDoc.Text & "-" & txtNroDoc.Text
            End If
            If psDatosCompletos = "No" Then
                If DdlServicio.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona Despacho o Recepción.');", True)
                ElseIf DdlMotivo.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un motivo.');", True)
                ElseIf DdlEstado.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un estado.');", True)
                ElseIf TxtDesCodigo.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el destino.');", True)
                ElseIf cboTipoDoc.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un tipo de documento.');", True)
                ElseIf txtNroDoc.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar número de documento.');", True)
                ElseIf txtNroOC.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar número de orden de compra.');", True)
                End If
            Else
                ' Verifica que el archivo sea un archivo de texto
                If fileExtension.ToLower() = ".txt" Then
                    ' Lee el contenido del archivo de texto
                    Dim fileContent As String = ""
                    Using reader As New StreamReader(FileUpload1.PostedFile.InputStream)
                        While Not reader.EndOfStream
                            ' Lee cada línea del archivo y agrega un salto de línea
                            fileContent = reader.ReadLine()
                            ' Actualiza el contenido del UpdatePanel
                            Session("TipoCarga") = "Txt"
                            Call Recibir_Bien("", txtNroOC.Text, "", psNroGuia, fileContent)
                        End While
                    End Using
                    '' Muestra el contenido en la página
                Else
                    Session("Fin") = ""
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El archivo seleccionado no es un archivo de texto válido.');", True)
                End If
            End If
        Else
            Session("Fin") = ""
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un archivo.');", True)
        End If
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        Call Carga_Motivos("S", "1", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        TxtDesCodExterno.Text = ""
        TxtDesDescrip.Text = ""
        TxtDesCodigo.Text = ""
        Session("TipoDestino") = "Almacen"
        Session("DestinoDescrip") = ""
        Session("DestinoCodExt") = ""
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        Call Carga_Motivos("S", "1", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        TxtDesCodExterno.Text = ""
        TxtDesDescrip.Text = ""
        TxtDesCodigo.Text = ""
        Session("TipoDestino") = "CentroCosto"
        Session("DestinoDescrip") = ""
        Session("DestinoCodExt") = ""
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        If Session("TipoDestino") = "Almacen" Then
            lblEtq_BusDestino.Text = "Busqueda de Almacén"
        ElseIf Session("TipoDestino") = "CentroCosto" Then
            lblEtq_BusDestino.Text = "Busqueda de Centro de Costos"
        End If
        Session("TipoBus") = "Destino"
        TxtDesDescrip.Text = ""
        TxtDesCodExterno.Text = ""
        TxtDesCodigo.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub

    Private Sub btnUbiListar_Click(sender As Object, e As EventArgs) Handles btnUbiListar.Click
        Try
            Dim psConexion As String = Session("Ruta_Emp")
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            Dim pdCodAlmacen As Double = 0
            If Session("TipoDestino") = "CentroCosto" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            ElseIf Session("TipoDestino") = "Almacen" Then
                If txtBusCod.Text = "" Then pdCodAlmacen = 0 Else pdCodAlmacen = txtBusCod.Text
                FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Sub btnUbiCerrar_Click(sender As Object, e As EventArgs) Handles btnUbiCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
    End Sub

    Private Sub FlexUbicacion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            If Session("TipoBus") = "Origen" Then
                TxtOriCodigo.Text = ""
                TxtOrigCodInt.Text = ""
                TxtOrigDescripcion.Text = ""
                Session("OrigenCodExt") = FlexUbicacion.Rows(Index).Cells(1).Text
                Session("OrigenDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                Session("OrigenCodigo") = FlexUbicacion.Rows(Index).Cells(3).Text
                TxtOrigDescripcion.Text = Session("OrigenDescrip")
                TxtOrigCodInt.Text = Session("OrigenCodExt")
                TxtOriCodigo.Text = Session("OrigenCodigo")
                FlexUbicacion.DataSource = Nothing
                FlexUbicacion.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
            Else
                TxtDesDescrip.Text = ""
                TxtDesCodExterno.Text = ""
                TxtDesCodigo.Text = ""
                Session("DestinoCodExt") = FlexUbicacion.Rows(Index).Cells(1).Text
                Session("DestinoDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                Session("DestinoCodigo") = FlexUbicacion.Rows(Index).Cells(3).Text
                TxtDesDescrip.Text = Session("DestinoDescrip")
                TxtDesCodExterno.Text = Session("DestinoCodExt")
                TxtDesCodigo.Text = Session("DestinoCodigo")
                FlexUbicacion.DataSource = Nothing
                FlexUbicacion.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
            End If
        End If
    End Sub

    Private Sub GvListaBienes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaBienes.RowCommand
        Dim arrSelec(,) As String
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "QuitarFila" Then
            f = -1
            Erase arrSelec
            Session("CountArrayEq") = "-1"
            With GvListaBienes
                For i = 0 To .Rows.Count - 1
                    If i <> index Then
                        f = f + 1
                        ReDim Preserve arrSelec(23, f)
                        arrSelec(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(5, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(6, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(10, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(11, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(12, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(13, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(14, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(15, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(16, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(17, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(18, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(19, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(20, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(21, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(22, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(22).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    End If
                Next
            End With
            Session("CountArrayEq") = f.ToString

            Dim dt As New DataTable
            Dim _dr As DataRow
            dt.Columns.Add("COD_ARTICULO")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("CANT")
            dt.Columns.Add("SERIE_NRO")
            dt.Columns.Add("PLACA_NRO")
            dt.Columns.Add("TipoBien")
            dt.Columns.Add("TIPO_UBICACION")
            dt.Columns.Add("COD_ALMACEN")
            dt.Columns.Add("ALMACEN_NOMBRE")
            dt.Columns.Add("SERIE_FECHA_ADQ")
            dt.Columns.Add("SERIE_SKU")
            dt.Columns.Add("SERIE_ORDEN_COMPRA")
            dt.Columns.Add("SERIE_GUIA")
            dt.Columns.Add("ARTICULO_REFERENCIA")
            dt.Columns.Add("Ubicact_tipo")
            dt.Columns.Add("Ubicact_codigo")
            dt.Columns.Add("serie_numerar")
            dt.Columns.Add("art_tipo")
            dt.Columns.Add("CS")
            dt.Columns.Add("Fecha_Servicio")
            dt.Columns.Add("Centro_Costo")
            For i = 0 To f
                _dr = dt.NewRow()
                _dr("COD_ARTICULO") = arrSelec(1, i).Trim
                _dr("ART_CODEQUIVA") = arrSelec(2, i).Trim
                _dr("ART_DESCRIPCION") = arrSelec(3, i).Trim
                _dr("CANT") = arrSelec(4, i).Trim
                If arrSelec(5, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(5, i)) AndAlso arrSelec(5, i).Trim() <> "" Then
                    _dr("SERIE_NRO") = arrSelec(5, i).Trim
                End If
                If arrSelec(6, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(6, i)) AndAlso arrSelec(6, i).Trim() <> "" Then
                    _dr("PLACA_NRO") = arrSelec(6, i).Trim
                End If
                If arrSelec(7, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(7, i)) AndAlso arrSelec(7, i).Trim() <> "" Then
                    _dr("TipoBien") = arrSelec(7, i).Trim
                End If
                If arrSelec(8, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(8, i)) AndAlso arrSelec(8, i).Trim() <> "" Then
                    _dr("TIPO_UBICACION") = arrSelec(8, i).Trim
                End If
                If arrSelec(9, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(9, i)) AndAlso arrSelec(9, i).Trim() <> "" Then
                    _dr("COD_ALMACEN") = arrSelec(9, i).Trim
                End If
                If arrSelec(10, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(10, i)) AndAlso arrSelec(10, i).Trim() <> "" Then
                    _dr("ALMACEN_NOMBRE") = arrSelec(10, i).Trim
                End If
                If arrSelec(11, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(11, i)) AndAlso arrSelec(11, i).Trim() <> "" Then
                    _dr("SERIE_FECHA_ADQ") = arrSelec(11, i).Trim
                End If
                If arrSelec(12, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(12, i)) AndAlso arrSelec(12, i).Trim() <> "" Then
                    _dr("SERIE_SKU") = arrSelec(12, i).Trim
                End If
                If arrSelec(13, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(13, i)) AndAlso arrSelec(13, i).Trim() <> "" Then
                    _dr("SERIE_ORDEN_COMPRA") = arrSelec(13, i).Trim
                End If
                If arrSelec(14, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(14, i)) AndAlso arrSelec(14, i).Trim() <> "" Then
                    _dr("SERIE_GUIA") = arrSelec(14, i).Trim
                End If
                If arrSelec(15, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(15, i)) AndAlso arrSelec(15, i).Trim() <> "" Then
                    _dr("ARTICULO_REFERENCIA") = arrSelec(15, i).Trim
                End If
                If arrSelec(16, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(16, i)) AndAlso arrSelec(16, i).Trim() <> "" Then
                    _dr("Ubicact_tipo") = arrSelec(16, i).Trim
                End If
                If arrSelec(17, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(17, i)) AndAlso arrSelec(17, i).Trim() <> "" Then
                    _dr("Ubicact_codigo") = arrSelec(17, i).Trim
                End If
                If arrSelec(18, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(18, i)) AndAlso arrSelec(18, i).Trim() <> "" Then
                    _dr("serie_numerar") = arrSelec(18, i).Trim
                End If
                If arrSelec(19, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(19, i)) AndAlso arrSelec(19, i).Trim() <> "" Then
                    _dr("art_tipo") = arrSelec(19, i).Trim
                End If
                If arrSelec(20, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(20, i)) AndAlso arrSelec(20, i).Trim() <> "" Then
                    _dr("CS") = arrSelec(20, i).Trim
                End If
                If arrSelec(21, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(21, i)) AndAlso arrSelec(21, i).Trim() <> "" Then
                    _dr("Fecha_Servicio") = arrSelec(21, i).Trim
                End If
                If arrSelec(22, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(22, i)) AndAlso arrSelec(22, i).Trim() <> "" Then
                    _dr("Centro_Costo") = arrSelec(22, i).Trim
                End If
                dt.Rows.Add(_dr)
            Next
            Session("ArrayEq") = arrSelec
            GvListaBienes.DataSource = New DataView(dt)
            GvListaBienes.DataBind()

            If GvListaBienes.Rows.Count > 1 Then
                LblRegistroE.Text = "Hay " & GvListaBienes.Rows.Count & " registros."
            ElseIf GvListaBienes.Rows.Count = 1 Then
                LblRegistroE.Text = "Hay 1 registro."
            ElseIf GvListaBienes.Rows.Count = 0 Then
                LblRegistroE.Text = ""
            End If
        End If
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        Dim dt As New DataTable
        dt = Nothing
        GvListaBienes.DataSource = dt
        GvListaBienes.DataBind()
        GvListaNoEncontrados.DataSource = dt
        GvListaNoEncontrados.DataBind()
        GvListaCantidades.DataSource = dt
        GvListaCantidades.DataBind()
        TxtDesCodExterno.Text = ""
        TxtDesDescrip.Text = ""
        TxtDesCodigo.Text = ""
        Session("TipoDestino") = "Almacen"
        Session("TipoOrigen") = "Almacen"
        Session("DestinoDescrip") = ""
        Session("DestinoCodExt") = ""
        Session("OrigenDescrip") = ""
        Session("OrigenCodExt") = ""
        DdlEstado.SelectedValue = "< Seleccionar >"
        DdlOperativa.SelectedValue = "< Seleccionar >"
        LblRegistroCant.Text = ""
        LblRegistroE.Text = ""
        LblRegistroNE.Text = ""
        cboTipoDoc.SelectedValue = "< Seleccionar >"
        txtNroDoc.Text = ""
        txtSerieDoc.Text = ""
        TxtColSerie.Text = ""
        TxtcolPlaca.Text = ""
        TxtIni.Text = ""
        Txtfin.Text = ""
        TxtColCant.Text = ""
        TxtColGuia.Text = ""
        TxtColOC.Text = ""
        TxtColRef.Text = ""
        TxtColSku.Text = ""
        TxtOriCodigo.Text = ""
        TxtOrigCodInt.Text = ""
        TxtOrigDescripcion.Text = ""
        TxtReferencia.Text = ""
        RBAlmacen.Checked = True
        RbOrigAlmacen.Checked = True
        divOrigen.Visible = False
        id_GuiaRecep.Visible = True
        id_Proveedor.Visible = True
        id_GuiaNumero.Visible = False
        TxtGuiaNumero.Text = ""
        TxtGuiaSerie.Text = ""
        txtNroDoc.Text = ""
        txtSerieDoc.Text = ""
        txtProvCodigo.Text = ""
        txtProvNombre.Text = ""
        txtProvRuc.Text = ""
        txtNroOC.Text = ""
        cboTipoDoc.SelectedValue = "< Seleccionar >"
        Call Carga_Motivos("S", "1", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmdSql As New SqlCommand

        Try
            Cn.Open()
            cmdSql.Connection = Cn

            cmdSql.CommandText = " DELETE FROM V_TBINV_LISTASERIE_" & Session("User") & " "
            cmdSql.ExecuteNonQuery()
            cmdSql.CommandText = " DELETE FROM V_TBINV_INGRESO_BIENES "
            cmdSql.ExecuteNonQuery()
            Cn.Close()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch Ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & Ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
    End Sub

    Private Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click

        Dim pdCantBien As Double
        pdCantBien = 0
        For i = 0 To GvListaBienes.Rows.Count - 1
            pdCantBien = pdCantBien + CDbl(Nz(GvListaBienes.Rows(i).Cells(4).Text))
        Next
        For i = 0 To GvListaNoEncontrados.Rows.Count - 1
            pdCantBien = pdCantBien + CDbl(Nz(GvListaNoEncontrados.Rows(i).Cells(5).Text))
        Next
        For i = 0 To GvListaCantidades.Rows.Count - 1
            pdCantBien = pdCantBien + CDbl(Nz(GvListaCantidades.Rows(i).Cells(5).Text))
        Next
        Dim ofun As New clsInv_Procesos
        If pdCantBien = 0 Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay equipo que ingresar')", True)

        Dim pdCodSalida As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn4 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim RsRecep As SqlDataReader
        Dim Rs As SqlDataReader
        Dim RsDatos As SqlDataReader
        Dim pdCodRecepcion As Double = 0
        Dim psTipoDestino As String = ""
        If RBAlmacen.Checked = True Then psTipoDestino = "1"
        If RBCentroC.Checked = True Then psTipoDestino = "2"
        Dim psTipoOrigen As String = ""
        If RbOrigAlmacen.Checked = True Then psTipoOrigen = "1"
        If RbOrigCC.Checked = True Then psTipoOrigen = "2"
        Dim ValorSys As String = ""
        ValorSys = Session("User") & FechaActual() & HoraActual()
        Dim psRucEmpresa As String = ""
        Dim StockAc As Double = 0
        Dim lblNroMovimiento As Double = 0
        Dim objEmp As New ModuloGeneral
        Dim dtEmpresa As New DataTable
        dtEmpresa = objEmp.Datos_Empresa(Session("Ruta_Emp"), Session("CodEmpresa"))
        For Each dr As DataRow In dtEmpresa.Rows
            psRucEmpresa = Nu(dr("Emp_Ruc"))
        Next
        Dim psProveedor As Double = 0

        Dim psDatosCompletos As String = ""

        Cn.Open()
        Cn2.Open()
        Cn3.Open()
        Cn4.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal2.Connection = Cn2
        CmdGlobal3.Connection = Cn3
        CmdGlobal4.Connection = Cn4


        If Existe_Tabla("V_TBINV_INGRESO_BIENES", Session("Ruta_Emp")) = True Then
            CmdGlobal.CommandText = " DELETE FROM V_TBINV_INGRESO_BIENES "
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " DROP TABLE V_TBINV_INGRESO_BIENES "
            CmdGlobal.ExecuteNonQuery()
        End If

        CmdGlobal.CommandText = " CREATE TABLE [dbo].[V_TBINV_INGRESO_BIENES]([REGISTRO_NRO] [float] NULL, [ARTICULO_CODIGO] [float] NULL, [ARTICULO_CANTIDAD] [float] NULL, " _
                              & " [SERIE_NRO] [varchar](50) NULL, [PLACA_NRO] [float] NULL,	[FECHA_ADQ] [varchar](50) NULL,	[SERIE_SKU] [varchar](50) NULL,	[SERIE_OC] [varchar](50) NULL, " _
                              & " [SERIE_GUIA] [varchar](50) NULL, [SERIE_REFERENCIA] [varchar](500) NULL, [SERIE_UBICACT_TIPO] [varchar](2) NULL,	[SERIE_UBICACT_CODIGO] [float] NULL, " _
                              & " [FECHA_SAL_REC] [varchar](50) NULL, [CENTRO_COSTO] [varchar](500) NULL, [SERIE_CS] [varchar](2) NULL, [SERIE_NUMERAR] [float] NULL, " _
                              & " [SERIE_ART_TIPO] [float] NULL, [ESTADO_EQUIPO] [varchar](50) NULL, [CONDICION_EQUIPO] [varchar](50) NULL, [EQUIPO_OPERATIVO] [varchar](50) NULL, [SERIE_REFERENCIA] [VARCHAR(500)] ) ON [PRIMARY] "
        CmdGlobal.ExecuteNonQuery()

        Dim pdCantBienNew As Double = 0
        Dim pdCantBienesTodos As Double = 0
        pdCantBienNew = GvListaNoEncontrados.Rows.Count
        pdCantBienesTodos = Nz(GvListaBienes.Rows.Count) + Nz(GvListaNoEncontrados.Rows.Count) + Nz(GvListaCantidades.Rows.Count)

        Dim pArrayNew(,) As String

        Dim psTabla As String = ""

        Dim pArrayTodos(,) As String
        pArrayTodos = New String(pdCantBienesTodos, 16) {}
        ReDim pArrayTodos(pdCantBienesTodos, 16)
        Dim f As Long = 0
        f = -1
        Erase pArrayNew
        With GvListaNoEncontrados
            For i = 0 To .Rows.Count - 1

                f = f + 1
                ReDim Preserve pArrayNew(23, f)
                pArrayNew(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(5, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(6, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(10, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(11, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(12, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(13, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(14, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(15, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(16, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(17, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(18, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(19, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(20, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(21, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(22).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(22, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(23).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")

            Next
        End With
        With GvListaBienes
            For i = 0 To .Rows.Count - 1
                f = f + 1
                ReDim Preserve pArrayNew(23, f)
                pArrayNew(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(5, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(6, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(10, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(11, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(12, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(13, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(14, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(15, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(16, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(17, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(18, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(19, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(20, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(21, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(22, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(22).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Next
        End With

        With GvListaCantidades
            For i = 0 To .Rows.Count - 1
                f = f + 1
                ReDim Preserve pArrayNew(23, f)
                pArrayNew(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(10, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(11, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(12, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(13, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(14, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(15, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(16, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(17, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(18, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(19, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(20, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(21, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                pArrayNew(22, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Next
        End With
        Dim psCondicionEquipo As String = ""
        If DdlEstado.SelectedValue <> "< Seleccionar >" Then
            psCondicionEquipo = DdlEstado.SelectedValue
        End If
        Dim psOperativaEquipo As String = ""
        If DdlOperativa.SelectedValue <> "< Seleccionar >" Then
            psOperativaEquipo = DdlOperativa.SelectedValue
        End If
        Dim psFechaExcel As String = ""
        For I = 0 To pdCantBienesTodos - 1
            psFechaExcel = Right(pArrayNew(21, I), 4) & Mid(pArrayNew(21, I), 4, 2) & Left(pArrayNew(21, I), 2)
            CmdGlobal.CommandText = " INSERT INTO V_TBINV_INGRESO_BIENES (REGISTRO_NRO, ARTICULO_CODIGO, ARTICULO_CANTIDAD, SERIE_NRO, FECHA_ADQ, SERIE_SKU, SERIE_OC, SERIE_GUIA, SERIE_REFERENCIA, SERIE_UBICACT_TIPO, FECHA_SAL_REC, CENTRO_COSTO, SERIE_CS, SERIE_ART_TIPO, CONDICION_EQUIPO, EQUIPO_OPERATIVO, SERIE_REFERENCIA) " _
                                  & " VALUES (" & I & ", " & pArrayNew(1, I) & ", " & pArrayNew(4, I) & ", '" & pArrayNew(5, I) & "', '" & pArrayNew(11, I) & "', '" & pArrayNew(12, I) & "', '" & pArrayNew(13, I) & "', '" & pArrayNew(14, I) & "', '" & pArrayNew(15, I) & "', '" & pArrayNew(16, I) & "',  '" & psFechaExcel & "', '" & pArrayNew(22, I) & "', '" & pArrayNew(20, I) & "', " & pArrayNew(19, I) & ", '" & psCondicionEquipo & "','" & psOperativaEquipo & "', '" & TxtReferencia.Text & "' ) "
            CmdGlobal.ExecuteNonQuery()
            If pArrayNew(17, I) <> "" Then
                CmdGlobal.CommandText = " UPDATE V_TBINV_INGRESO_BIENES SET SERIE_UBICACT_CODIGO = " & pArrayNew(4, I) & "  WHERE REGISTRO_NRO = " & I
                CmdGlobal.ExecuteNonQuery()
            End If
            If pArrayNew(6, I) <> "" Then
                CmdGlobal.CommandText = " UPDATE V_TBINV_INGRESO_BIENES SET PLACA_NRO = " & pArrayNew(6, I) & "  WHERE REGISTRO_NRO = " & I
                CmdGlobal.ExecuteNonQuery()
            End If
            If pArrayNew(18, I) <> "" Then
                CmdGlobal.CommandText = " UPDATE V_TBINV_INGRESO_BIENES SET SERIE_NUMERAR = " & pArrayNew(18, I) & "  WHERE REGISTRO_NRO = " & I
                CmdGlobal.ExecuteNonQuery()
            End If
        Next

        Dim psNroOc As String = ""
        Dim psNroOCAnterior As String = ""
        Dim psLong As Double = 0
        Dim psVariable As String = ""
        If DdlServicio.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
        If DdlMotivo.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
        If TxtDesCodigo.Text = "" Then psDatosCompletos = "No"
        If DdlEstado.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
        Dim psRecepcionesGeneradas As String = ""
        Dim psFechaRecepcion As String = ""
        Dim psFechaRegistro As String = ""
        Dim psHoraRecepcion As String = ""
        psFechaRegistro = Right(txtFecRegistra.Text, 4) + Mid(txtFecRegistra.Text, 4, 2) + Left(txtFecRegistra.Text, 2)
        psFechaRecepcion = Right(txtFechaRecep.Text, 4) + Mid(txtFechaRecep.Text, 4, 2) + Left(txtFechaRecep.Text, 2)
        psHoraRecepcion = Left(txtHoraRegistra.Text, 2) + Mid(txtFecRegistra.Text, 4, 2)
        Dim pd_ItemNro As Double = 0
        Dim pd_ItemCant As Double = 0
        Dim psSerieGuia As String = ""
        Dim psNumeroGuia As String = ""
        Dim psCantidades As Double = 0
        Try

            If psDatosCompletos = "No" Then
                If DdlServicio.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar el Tipo de Servicio')", True)
                End If
                If DdlMotivo.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar el Motivo')", True)
                End If
                If DdlEstado.SelectedValue = "< Seleccionar >" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Condición')", True)
                End If
                If TxtDesCodigo.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar el Destino')", True)
                End If
            Else
                Dim psContador As Double = 0
                Dim psCantidadReg As Double = 0
                psCantidadReg = GvListaBienes.Rows.Count + GvListaCantidades.Rows.Count + GvListaNoEncontrados.Rows.Count
                If DdlServicio.SelectedValue = "2" Then

                    CmdGlobal4.CommandText = " SELECT REGISTRO_NRO, ARTICULO_CODIGO, ARTICULO_CANTIDAD, SERIE_NRO, PLACA_NRO, FECHA_ADQ, SERIE_SKU, " _
                                           & " SERIE_OC, SERIE_GUIA, SERIE_REFERENCIA, SERIE_UBICACT_TIPO, SERIE_UBICACT_CODIGO, FECHA_SAL_REC, " _
                                           & " CENTRO_COSTO, SERIE_CS,SERIE_NUMERAR, ESTADO_EQUIPO, CONDICION_EQUIPO, equipo_operativo, SERIE_REFERENCIA FROM V_TBINV_INGRESO_BIENES ORDER BY FECHA_SAL_REC, SERIE_GUIA, SERIE_ART_TIPO "
                    RsDatos = CmdGlobal4.ExecuteReader

                    If RsDatos.HasRows Then
                        While RsDatos.Read
                            psContador = psContador + 1
                            If Nu(RsDatos("FECHA_SAL_REC")) <> "" Then
                                txtFechaRecep.Text = FormatoFecha(Nu(RsDatos("FECHA_SAL_REC")))
                            Else
                                txtFechaRecep.Text = FormatoFecha(FechaActual())
                            End If
                            psCantidades = psCantidades + CDbl(Nz(RsDatos("ARTICULO_CANTIDAD")))
                            psFechaRecepcion = Right(txtFechaRecep.Text, 4) + Mid(txtFechaRecep.Text, 4, 2) + Left(txtFechaRecep.Text, 2)
                            If Nu(RsDatos("SERIE_GUIA")) <> psNroOCAnterior Or psContador = psCantidadReg Then
                                If psNroOCAnterior <> "" And psContador <> psCantidadReg Then
                                    CmdGlobal.CommandText = "SELECT ARTICULO_CODIGO, ART_TIPO FROM TBINV_ALMACEN_RECEPCION_DET AS RD INNER JOIN TBINV_ARTICULOS AS ART ON RD.ARTICULO_CODIGO = ART.ART_CODIGO WHERE RECEP_CODIGO = " & pdCodRecepcion
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            If Nz(Rs("ART_TIPO")) <> 87 Then
                                                CmdGlobal3.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_INGRESAR_SERIE = 'S'  WHERE RECEP_CODIGO = " & pdCodRecepcion & " AND ARTICULO_CODIGO = " & Nz(Rs("ARTICULO_CODIGO"))
                                                CmdGlobal3.ExecuteNonQuery()
                                            End If
                                        End While
                                    End If
                                    Rs.Close()

                                    CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & pdCodRecepcion
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            CmdGlobal3.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                            RsRecep = CmdGlobal3.ExecuteReader
                                            If RsRecep.HasRows Then
                                                While RsRecep.Read
                                                    lblNroMovimiento = Nz(RsRecep(0)) + 1
                                                End While
                                            Else
                                                lblNroMovimiento = "00000001"
                                            End If
                                            RsRecep.Close()
                                            '1: INGRESO, 2:SALIDA
                                            ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecepcion, "8", Nu(Rs("ARTICULO_CODIGO")), psTipoDestino, TxtDesCodigo.Text, "", "", "", "1", txtFechaRecep.Text, Nz(Rs("RECEPD_CANT_REC")))

                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO,CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                        & " values('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "','" & TxtDesCodigo.Text & "','','','" & pdCodRecepcion & "'," & Nz(Rs("ARTICULO_CODIGO")) & ",'" & CDbl(Nz(Rs("RECEPD_CANT_REC"))) & "','" & ValorSys & "','2','" & DdlMotivo.SelectedValue & "','" & psFechaRecepcion & "','0')"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    End If
                                    Rs.Close()
                                End If
                                If Nu(RsDatos("SERIE_GUIA")) <> psNroOCAnterior Then
                                    psNroOCAnterior = Nu(RsDatos("SERIE_GUIA"))
                                    pd_ItemCant = 0
                                    pd_ItemNro = 0
                                    CmdGlobal.CommandText = " SELECT PERSONA_CODIGO FROM TBDATA_PERSONAS WHERE PERSONA_SYS_EST = '0' AND PERSONA_TIPO = '2' AND PERSONA_RUC = '" & psRucEmpresa & "' "
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            psProveedor = Nu(Rs(0))
                                        End While
                                    End If
                                    Rs.Close()

                                    CmdGlobal.CommandText = "SELECT MAX(RECEP_CODIGO) FROM TBINV_ALMACEN_RECEPCION WHERE  EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            pdCodRecepcion = Nz(Rs(0)) + 1
                                        End While
                                    Else
                                        pdCodRecepcion = 1
                                    End If
                                    Rs.Close()
                                    If psRecepcionesGeneradas <> "" Then psRecepcionesGeneradas = psRecepcionesGeneradas & " , "
                                    psRecepcionesGeneradas = psRecepcionesGeneradas & Llenar_Ceros(pdCodRecepcion, 8)
                                    CmdGlobal.CommandText = "SELECT ARTICULO_CODIGO, SUM(ARTICULO_CANTIDAD) AS CANT FROM V_TBINV_INGRESO_BIENES  WHERE SERIE_GUIA = '" & Nu(RsDatos("SERIE_GUIA")) & "' group by articulo_codigo "
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            pd_ItemNro = pd_ItemNro + 1
                                            pd_ItemCant = pd_ItemCant + CDbl(Nz(Rs("CANT")))
                                        End While
                                    End If
                                    Rs.Close()

                                    CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,  ALTIBI_CODIGO, RECEP_PROYECTO, RECEP_FECHA_REC , RECEP_HORA_REC, RECEP_TIPODOC,  " _
                                                  & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, RECEP_NRO_OC, " _
                                                  & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO, RECEP_CREADO_DESDE, RECEP_REFERENCIA) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & pdCodRecepcion & "," & TxtDesCodigo.Text & ", '1','1', '" & psFechaRecepcion & "', '" & psHoraRecepcion & "', '7',  " _
                                                  & " '" & psFechaRecepcion & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "',''," & pd_ItemNro & ",'2', '" & Nu(RsDatos("SERIE_OC")) & "'," _
                                                  & " '0','" & ValorSys & "'," & pd_ItemCant & "," & pd_ItemCant & ",0,0,'N','5','1', '3', '" & psTipoDestino & "','IB', '" & Nu(RsDatos("SERIE_REFERENCIA")) & "' )"
                                    CmdGlobal.ExecuteNonQuery()

                                    If Nu(RsDatos("SERIE_GUIA")) <> "" Then
                                        psVariable = Nu(RsDatos("SERIE_GUIA"))
                                        psLong = InStr(psVariable, "-")
                                        If psLong = 0 Then
                                            psNumeroGuia = psVariable
                                        Else
                                            psSerieGuia = Left(psVariable, psLong - 1)
                                            psNumeroGuia = Mid(psVariable, psLong + 1)
                                        End If

                                        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_TIPODOC = '3', RECEP_DOC_SERIE='" & psSerieGuia & "', RECEP_DOC_NUMERACION='" & psNumeroGuia & "' WHERE RECEP_CODIGO = '" & pdCodRecepcion & "' "
                                        CmdGlobal.ExecuteNonQuery()
                                    End If
                                    If txtProvCodigo.Text <> "" Then
                                        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_PROVEEDOR = " & txtProvCodigo.Text & "  WHERE RECEP_CODIGO = '" & pdCodRecepcion & "' "
                                        CmdGlobal.ExecuteNonQuery()
                                    End If

                                    If Session("TicketNro") <> "" And pdCodRecepcion <> 0 Then
                                        Dim psNroTicket As String = ""
                                        Dim psConexion As String = ""
                                        psConexion = Session("Ruta_Emp")
                                        psNroTicket = Session("TicketNro")
                                        ofun.Guardar_RelacionTicket(psConexion, psNroTicket, "25", pdCodRecepcion, Session("User"))
                                        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_TICKET = " & psNroTicket & " WHERE RECEP_CODIGO = " & pdCodRecepcion
                                        CmdGlobal.ExecuteNonQuery()
                                    End If
                                End If
                            End If

                            Dim psOcupado As Double = 0
                            Dim psDisponible As Double = 0

                            Dim a As Double = 0
                            'For i = 0 To GvListaBienes.Rows.Count - 1
                            If Nu(RsDatos("SERIE_CS")) <> "C" Then
                                If Nu(RsDatos("SERIE_CS")) <> "" Then
                                    CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " Set  " _
                                                          & " RECEP_CODIGO = '" & pdCodRecepcion & "' " _
                                                          & " WHERE SERIE_NUMERAR = " & Nz(RsDatos("SERIE_NUMERAR"))
                                    CmdGlobal.ExecuteNonQuery()
                                End If
                                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " Set SERIE_ESTADO_VALIDADO='1', " _
                                                      & " SERIE_SKU = '" & Nu(RsDatos("SERIE_SKU")) & "', " _
                                                      & " SERIE_ORDEN_COMPRA = '" & Nu(RsDatos("SERIE_OC")) & "', " _
                                                      & " SERIE_GUIA = '" & Nu(RsDatos("SERIE_GUIA")) & "', " _
                                                      & " EQUIPO_CONDICION = '" & Nu(RsDatos("CONDICION_EQUIPO")) & "', " _
                                                      & " EQUIPO_OPERATIVO = '" & Nu(RsDatos("EQUIPO_OPERATIVO")) & "', " _
                                                      & " SERIE_REFERENCIA = '" & Nu(RsDatos("SERIE_REFERENCIA")) & "' " _
                                                      & " WHERE SERIE_NUMERAR = " & Nz(RsDatos("SERIE_NUMERAR"))
                                CmdGlobal.ExecuteNonQuery()
                                If Nz(RsDatos("SERIE_NUMERAR")) <> 0 Then
                                    CmdGlobal.CommandText = " INSERT INTO TBINV_RECEPCION_DETALLE_SERIES (EMPRESA_CODIGO, RECEP_CODIGO, SERIE_NUMERAR, SERIE_ORIG_TIPO, SERIE_ORIG_CODIGO, salida_codigo, recep_orden_compra) " _
                                                        & " VALUES ('" & Session("CodEmpresa") & "', " & pdCodRecepcion & ", " & Nz(RsDatos("SERIE_NUMERAR")) & ", '" & Nu(RsDatos("SERIE_UBICACT_TIPO")) & "', " & Nz(RsDatos("SERIE_UBICACT_CODIGO")) & "," & pdCodSalida & ",'" & Nu(RsDatos("SERIE_OC")) & "')"
                                    CmdGlobal.ExecuteNonQuery()
                                End If
                                Call Ingreso_Equipo_AAlmacen(Nu(RsDatos("SERIE_NUMERAR")), DdlEstado.SelectedValue, "", "", psFechaRecepcion,
                                                            TxtDesCodigo.Text, "", "V", psTipoDestino, "",
                                                            "", "", DdlMotivo.SelectedValue,,,, Nu(RsDatos("SERIE_REFERENCIA")))
                                pdCodSalida = CodSalida
                                CmdGlobal.CommandText = " DELETE FROM V_TBINV_LISTASERIE_" & Session("User") & " where SERIE_NUMERAR = " & Nz(RsDatos("SERIE_NUMERAR"))
                                CmdGlobal.ExecuteNonQuery()
                            End If
                            If Nz(RsDatos("SERIE_UBICACT_CODIGO")) <> TxtDesCodigo.Text Or Nu(RsDatos("SERIE_UBICACT_TIPO")) <> psTipoDestino Then
                                a = a + 1
                                CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & pdCodRecepcion & " AND ARTICULO_CODIGO=" & Nz(RsDatos("ARTICULO_CODIGO"))
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_XREC = " & Nz(Rs!RECEPD_CANT_XREC) + Nz(RsDatos("ARTICULO_CANTIDAD")) & ",RECEPD_CANT_ING = " & Nz(Rs!RECEPD_CANT_XREC) + Nz(RsDatos("ARTICULO_CANTIDAD")) & ",RECEPD_CANT_REC = " & Nz(Rs!RECEPD_CANT_REC) + Nz(RsDatos("ARTICULO_CANTIDAD")) & " " _
                                                        & " WHERE RECEP_CODIGO = " & pdCodRecepcion & "  AND  ARTICULO_CODIGO = " & Nz(RsDatos("ARTICULO_CODIGO"))
                                        CmdGlobal2.ExecuteNonQuery()
                                    End While
                                    Rs.Close()
                                Else
                                    Rs.Close()
                                    CmdGlobal2.CommandText = "SELECT MAX(RECEPD_ITEM) FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & pdCodRecepcion
                                    Rs = CmdGlobal2.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                            & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                            & "'" & Session("CodEmpresa") & "'," & pdCodRecepcion & "," & Nz(Rs(0)) + 1 & "," & Nz(RsDatos("ARTICULO_CODIGO")) & "," & Nz(RsDatos("ARTICULO_CANTIDAD")) & " ," & Nz(RsDatos("ARTICULO_CANTIDAD")) & "," _
                                                            & " 0 ,0,1,'1','0','5','N')"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    Else
                                        CmdGlobal3.CommandText = "INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                            & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                            & "'" & Session("CodEmpresa") & "'," & pdCodRecepcion & ",1," & Nz(RsDatos("ARTICULO_CODIGO")) & "," & Nz(RsDatos("ARTICULO_CANTIDAD")) & " ," & Nz(RsDatos("ARTICULO_CANTIDAD")) & "," _
                                                            & " 0 ,0,1,'1','0','5','N')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    Rs.Close()
                                End If
                                If Nu(RsDatos("SERIE_CS")) = "C" Then
                                    StockAc = 0
                                    lblNroMovimiento = 0
                                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & TxtDesCodigo.Text & ") AND (UBICACT_TIPO='" & psTipoDestino & "')" _
                                                        & " AND (ARTICULO_CODIGO = " & Nz(RsDatos("ARTICULO_CODIGO")) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL)
                                            StockAc = StockAc + CDbl(Nz(RsDatos("ARTICULO_CANTIDAD")))
                                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & TxtDesCodigo.Text & ") AND (UBICACT_TIPO='" & psTipoDestino & "')" _
                                                            & " AND (ARTICULO_CODIGO = " & Nz(RsDatos("ARTICULO_CODIGO")) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                            CmdGlobal2.ExecuteNonQuery()
                                        End While
                                    Else
                                        CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                            & "VALUES('" & psTipoDestino & "'," & TxtDesCodigo.Text & "," & Nz(RsDatos("ARTICULO_CODIGO")) & "," & Nz(RsDatos("ARTICULO_CANTIDAD")) & ",'0','" & Session("CodEmpresa") & "')"
                                        CmdGlobal2.ExecuteNonQuery()
                                    End If
                                    Rs.Close()
                                End If
                                If pdCodSalida <> 0 And Nu(RsDatos("SERIE_CS")) <> "C" Then
                                End If
                            End If
                            If psCantidadReg = psContador Then

                                CmdGlobal.CommandText = "SELECT ARTICULO_CODIGO, ART_TIPO FROM TBINV_ALMACEN_RECEPCION_DET AS RD INNER JOIN TBINV_ARTICULOS AS ART ON RD.ARTICULO_CODIGO = ART.ART_CODIGO WHERE RECEP_CODIGO = " & pdCodRecepcion
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        If Nz(Rs("ART_TIPO")) <> 87 Then
                                            CmdGlobal3.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_INGRESAR_SERIE = 'S'  WHERE RECEP_CODIGO = " & pdCodRecepcion & " AND ARTICULO_CODIGO = " & Nz(Rs("ARTICULO_CODIGO"))
                                            CmdGlobal3.ExecuteNonQuery()
                                        End If
                                    End While
                                End If
                                Rs.Close()

                                CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & pdCodRecepcion
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        CmdGlobal3.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                        RsRecep = CmdGlobal3.ExecuteReader
                                        If RsRecep.HasRows Then
                                            While RsRecep.Read
                                                lblNroMovimiento = Nz(RsRecep(0)) + 1
                                            End While
                                        Else
                                            lblNroMovimiento = "00000001"
                                        End If
                                        RsRecep.Close()
                                        '1: INGRESO, 2:SALIDA
                                        ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecepcion, "8", Nu(Rs("ARTICULO_CODIGO")), psTipoDestino, TxtDesCodigo.Text, "", "", "", "1", txtFechaRecep.Text, Nz(Rs("RECEPD_CANT_REC")))

                                        CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO,CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                    & " values('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "','" & TxtDesCodigo.Text & "','','','" & pdCodRecepcion & "'," & Nz(Rs("ARTICULO_CODIGO")) & ",'" & CDbl(Nz(Rs("RECEPD_CANT_REC"))) & "','" & ValorSys & "','2','" & DdlMotivo.SelectedValue & "','" & psFechaRecepcion & "','0')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End While
                                End If
                                Rs.Close()
                            End If
                        End While
                    End If
                    RsDatos.Close()

                    CmdGlobal.CommandText = " DELETE FROM V_TBINV_LISTASERIE_" & Session("User") & " "
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " DELETE FROM V_TBINV_INGRESO_BIENES "
                    CmdGlobal.ExecuteNonQuery()

                    CmdGlobal.ExecuteNonQuery()
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Recepción generadas : " & psRecepcionesGeneradas & "')", True)

                ElseIf DdlServicio.SelectedValue = "1" Then

                    CmdGlobal4.CommandText = " SELECT REGISTRO_NRO, ARTICULO_CODIGO, ARTICULO_CANTIDAD, SERIE_NRO, PLACA_NRO, FECHA_ADQ, SERIE_SKU, " _
                                           & " SERIE_OC, SERIE_GUIA, SERIE_REFERENCIA, SERIE_UBICACT_TIPO, SERIE_UBICACT_CODIGO, FECHA_SAL_REC, " _
                                           & " CENTRO_COSTO, SERIE_CS,SERIE_NUMERAR,equipo_operativo, condicion_equipo, SERIE_REFERENCIA FROM V_TBINV_INGRESO_BIENES ORDER BY FECHA_SAL_REC , SERIE_GUIA  "
                    RsDatos = CmdGlobal4.ExecuteReader
                    If RsDatos.HasRows Then
                        While RsDatos.Read
                            If Nu(RsDatos("FECHA_SAL_REC")) <> "" Then
                                txtFechaRecep.Text = FormatoFecha(Nu(RsDatos("FECHA_SAL_REC")))
                            Else
                                txtFechaRecep.Text = FormatoFecha(FechaActual())
                            End If
                            psCantidades = psCantidades + CDbl(Nz(RsDatos("ARTICULO_CANTIDAD")))
                            psFechaRecepcion = Right(txtFechaRecep.Text, 4) + Mid(txtFechaRecep.Text, 4, 2) + Left(txtFechaRecep.Text, 2)
                            If Nu(RsDatos("SERIE_GUIA")) <> psNroOCAnterior Then
                                psNroOCAnterior = Nu(RsDatos("SERIE_GUIA"))
                                If Nu(RsDatos("SERIE_UBICACT_TIPO")) <> "" And Nu(RsDatos("SERIE_UBICACT_CODIGO")) <> "" Then
                                    Despacho_unoxuno(Nu(RsDatos("SERIE_NUMERAR")), psFechaRecepcion, "", "", DdlMotivo.SelectedValue, Nu(RsDatos("SERIE_UBICACT_CODIGO")), Nu(RsDatos("SERIE_UBICACT_TIPO")), Nu(RsDatos("ARTICULO_CODIGO")), Nu(RsDatos("SERIE_REFERENCIA")))
                                Else
                                    CmdGlobal.CommandText = "SELECT MAX(RECEP_CODIGO) FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                        While Rs.Read
                                            pdCodRecepcion = Nz(Rs(0)) + 1
                                        End While
                                    Else
                                        pdCodRecepcion = 1
                                    End If
                                    Rs.Close()
                                    CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,  ALTIBI_CODIGO, RECEP_PROYECTO, RECEP_FECHA_REC , RECEP_HORA_REC, RECEP_TIPODOC,  " _
                                                            & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, RECEP_NRO_OC, " _
                                                            & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO, RECEP_CREADO_DESDE, RECEP_REFERENCIA) " _
                                                            & " VALUES('" & Session("CodEmpresa") & "'," & pdCodRecepcion & "," & TxtOriCodigo.Text & ", '1','1', '" & psFechaRegistro & "', '" & psHoraRecepcion & "', '7',  " _
                                                            & " '" & psFechaRecepcion & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "',''," & GvListaNoEncontrados.Rows.Count - 1 & ",'2', '" & txtNroOC.Text & "'," _
                                                            & " '0','" & ValorSys & "'," & GvListaNoEncontrados.Rows.Count & "," & GvListaNoEncontrados.Rows.Count & ",0,0,'N','5','1', '', '" & psTipoOrigen & "','IB', '" & TxtReferencia.Text & "')"
                                    CmdGlobal.ExecuteNonQuery()
                                    CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_TIPODOC = '3', RECEP_DOC_SERIE='" & TxtGuiaSerie.Text.Trim & "', RECEP_DOC_NUMERACION='" & TxtGuiaNumero.Text.Trim & "' WHERE RECEP_CODIGO = '" & pdCodRecepcion & "' "
                                    CmdGlobal.ExecuteNonQuery()

                                    If txtProvCodigo.Text <> "" Then
                                        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_PROVEEDOR = " & txtProvCodigo.Text & "  WHERE RECEP_CODIGO = '" & pdCodRecepcion & "' "
                                        CmdGlobal.ExecuteNonQuery()
                                    End If

                                    For i = 0 To GvListaNoEncontrados.Rows.Count - 1
                                        CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & pdCodRecepcion & " AND ARTICULO_CODIGO='" & GvListaNoEncontrados.Rows(i).Cells(2).Text & "'"
                                        RsRecep = CmdGlobal.ExecuteReader
                                        If RsRecep.HasRows Then
                                            While RsRecep.Read
                                                CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_XREC = " & Nz(RsRecep!RECEPD_CANT_XREC) + 1 & ",RECEPD_CANT_REC = " & Nz(RsRecep!RECEPD_CANT_REC) + 1 & " " _
                                                                        & " WHERE RECEP_CODIGO = " & pdCodRecepcion & "  AND  ARTICULO_CODIGO = " & GvListaNoEncontrados.Rows(i).Cells(2).Text
                                                CmdGlobal2.ExecuteNonQuery()
                                            End While
                                            RsRecep.Close()
                                        Else
                                            RsRecep.Close()
                                            CmdGlobal.CommandText = "SELECT isnull(MAX(RECEPD_ITEM),0) FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & pdCodRecepcion
                                            RsRecep = CmdGlobal.ExecuteReader
                                            If RsRecep.HasRows Then
                                                While RsRecep.Read
                                                    CmdGlobal2.CommandText = "INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                                            & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                                            & "'" & Session("CodEmpresa") & "'," & pdCodRecepcion & "," & Nz(RsRecep(0)) + 1 & "," & GvListaNoEncontrados.Rows(i).Cells(2).Text & ",1 ,1," _
                                                                            & " 0 ,0,1,'1','0','5','N')"
                                                    CmdGlobal2.ExecuteNonQuery()
                                                End While
                                            Else
                                                CmdGlobal2.CommandText = "INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                                        & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                                        & "'" & Session("CodEmpresa") & "'," & pdCodRecepcion & ",1," & GvListaNoEncontrados.Rows(i).Cells(2).Text & ",1 ,1," _
                                                                        & " 0 ,0,1,'1','0','5','N')"
                                                CmdGlobal2.ExecuteNonQuery()
                                            End If
                                            RsRecep.Close()
                                        End If
                                        CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET RECEP_CODIGO = " & pdCodRecepcion & " WHERE SERIE_NUMERAR = " & GvListaNoEncontrados.Rows(i).Cells(19).Text
                                        CmdGlobal3.ExecuteNonQuery()
                                        CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " " _
                                                                & " set SERIE_SKU = '" & GvListaNoEncontrados.Rows(i).Cells(13).Text & "', " _
                                                                & " SERIE_ORDEN_COMPRA = '" & GvListaNoEncontrados.Rows(i).Cells(14).Text & "', " _
                                                                & " SERIE_GUIA = '" & GvListaNoEncontrados.Rows(i).Cells(15).Text & "', " _
                                                                & " EQUIPO_CONDICION = '1', " _
                                                                & " EQUIPO_OPERATIVO = '1', " _
                                                                & " SERIE_REFERENCIA = '" & TxtReferencia.Text & "' " _
                                                                & " WHERE SERIE_NUMERAR = " & GvListaNoEncontrados.Rows(i).Cells(19).Text
                                        CmdGlobal3.ExecuteNonQuery()
                                        CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & psTipoOrigen & "',UBICACT_CODIGO=" & Nz(TxtOriCodigo.Text) & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & GvListaNoEncontrados.Rows(i).Cells(19).Text
                                        CmdGlobal3.ExecuteNonQuery()
                                        CmdGlobal3.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                                                & " VALUES ('" & GvListaNoEncontrados.Rows(i).Cells(19).Text & "','" & psTipoDestino & "'," & Nz(TxtOriCodigo.Text) & ",'" & DdlMotivo.SelectedValue & "','0','" & ValorSys & "','" & psFechaRecepcion & "','1','" & pdCodRecepcion & "')"
                                        CmdGlobal3.ExecuteNonQuery()

                                    Next

                                    CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & pdCodRecepcion
                                    RsRecep = CmdGlobal.ExecuteReader
                                    If RsRecep.HasRows Then
                                        While RsRecep.Read

                                            CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & TxtOriCodigo.Text & ") AND (UBICACT_TIPO='" & psTipoOrigen & "')" _
                                                                    & " AND (ARTICULO_CODIGO = " & Nu(RsRecep("ARTICULO_CODIGO")) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                            Rs = CmdGlobal.ExecuteReader
                                            If Rs.HasRows Then
                                                While Rs.Read
                                                    StockAc = Nz(Rs!SAA_STOCK_ACTUAL)
                                                    StockAc = StockAc + CDbl(Nz(RsRecep("RECEPD_CANT_REC")))
                                                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & TxtOriCodigo.Text & ") AND (UBICACT_TIPO='" & psTipoOrigen & "')" _
                                                                                & " AND (ARTICULO_CODIGO = " & Nu(RsRecep("ARTICULO_CODIGO")) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                                    CmdGlobal2.ExecuteNonQuery()
                                                End While
                                            Else
                                                CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                                                & "VALUES('" & psTipoOrigen & "'," & TxtOriCodigo.Text & "," & Nu(RsRecep("ARTICULO_CODIGO")) & "," & Nz(RsRecep("RECEPD_CANT_REC")) & ",'0','" & Session("CodEmpresa") & "')"
                                                CmdGlobal2.ExecuteNonQuery()
                                            End If
                                            Rs.Close()

                                            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                            Rs = CmdGlobal.ExecuteReader
                                            If Rs.HasRows Then
                                                While Rs.Read
                                                    lblNroMovimiento = Nz(Rs(0)) + 1
                                                End While
                                            Else
                                                lblNroMovimiento = "00000001"
                                            End If
                                            Rs.Close()
                                            '1: INGRESO, 2:SALIDA
                                            ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodRecepcion, DdlMotivo.SelectedValue, Nu(RsRecep("ARTICULO_CODIGO")), psTipoOrigen, TxtOriCodigo.Text, "", "", "", "1", txtFechaRecep.Text, Nz(RsRecep("RECEPD_CANT_REC")))

                                            CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO,CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                                            & " values('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoOrigen & "','" & TxtOriCodigo.Text & "','','','" & pdCodRecepcion & "','" & Nu(RsRecep("ARTICULO_CODIGO")) & "','" & CDbl(Nz(RsRecep("RECEPD_CANT_REC"))) & "','" & ValorSys & "','2','" & DdlMotivo.SelectedValue & "','" & psFechaRecepcion & "','0')"
                                            CmdGlobal.ExecuteNonQuery()
                                        End While
                                    End If
                                    RsRecep.Close()

                                End If


                            End If
                        End While
                    End If

                    'generar la salida 

                    GenerarSalidaFinal(pArrayNew, pdCantBienesTodos)

                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Su numero de Despacho es el : " & Session("CodSalida") & "')", True)
                    Session("CodSalida") = ""
                End If

                BtnLimpiar_Click(sender, e)

            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch Ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & Ex.Message & "')", True)
        Finally
            Cn.Close()
        End Try
    End Sub

    Private Sub GenerarSalidaFinal(ByVal pArrayP As String(,), ByVal pdCantBienes As Double)
        Dim ofun As New clsInv_Procesos
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn4 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim RsRecep As SqlDataReader
        Dim RsDatos As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Cn4.Open() : CmdGlobal4.Connection = Cn4
        Dim ValorSys As String : ValorSys = FechaActual() + HoraActual() + Session("User")
        Dim psCodCECosto As String : psCodCECosto = ""
        Dim psCodSeccion As String : psCodSeccion = ""
        Dim psCodArt As String : psCodArt = ""
        Dim psSerieNumerar As String
        Dim psSerieNro As String : psSerieNro = ""
        Dim psPlacaNro As String : psPlacaNro = ""
        Dim lblNroMovimiento As String : lblNroMovimiento = ""
        Dim StockAc As Double : StockAc = 0
        Dim cant As Double : cant = 0
        Dim i As Long : i = 0
        Dim psTipoDestino As String
        Dim psTipoOrigen As String : psTipoOrigen = ""
        Dim psCodOrigen As String : psCodOrigen = ""
        Dim psCodDespacho As String : psCodDespacho = ""
        Dim psCodDestino As String : psCodDestino = ""
        Dim psDestinoAlm As String : psDestinoAlm = "NULL"
        Dim psDestinoCC As String : psDestinoCC = "NULL"
        Dim DesCodProveedor As String : DesCodProveedor = "NULL"
        Dim DesCodCliente As String : DesCodCliente = "NULL"
        Dim DesCodPersona As String : DesCodPersona = "NULL"
        psTipoOrigen = IIf(RbOrigAlmacen.Checked = True, "1", IIf(RbOrigCC.Checked = True, "2", ""))
        psTipoDestino = IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", ""))
        If psTipoDestino = "1" Then psDestinoAlm = TxtDesCodigo.Text
        If psTipoDestino = "2" Then psDestinoCC = TxtDesCodigo.Text
        If TxtDesCodigo.Text <> "" Then psCodDestino = TxtDesCodigo.Text
        If TxtOriCodigo.Text <> "" Then psCodOrigen = TxtOriCodigo.Text
        StockAc = 0
        CodSalida = 0
        Dim objProceso As New clsInv_Procesos
        Dim psRecepcion As String : psRecepcion = ""
        i = 0
        Dim psProveedor As String = ""
        Dim psCodRecepcion As String = ""

        cant = 0
        For i = 0 To GvListaBienes.Rows.Count - 1
            cant = cant + Nz(GvListaBienes.Rows(i).Cells(4).Text.Trim)
        Next
        Dim psCodAllSal As String = ""
        Dim psContador As Double = 0
        Dim psCantidades As Double = 0
        Dim psFechaSalida As String = ""
        Dim psAnteriorCC As String = ""
        Dim psFechaFormato As String = ""
        Dim psHoraFormato As String = ""
        Session("CodSalida") = ""
        psFechaFormato = Mid(txtFechaRecep.Text, 7, 4) + Mid(txtFechaRecep.Text, 4, 2) + Mid(txtFechaRecep.Text, 1, 2)
        psHoraFormato = Mid(txtHoraRegistra.Text, 1, 2) + Mid(txtHoraRegistra.Text, 4, 2)
        CmdGlobal4.CommandText = " SELECT REGISTRO_SALIDA, REGISTRO_NRO, ARTICULO_CODIGO, ARTICULO_CANTIDAD, SERIE_NRO, PLACA_NRO, FECHA_ADQ, SERIE_SKU, " _
                                           & " SERIE_OC, SERIE_GUIA, SERIE_REFERENCIA, SERIE_UBICACT_TIPO, SERIE_UBICACT_CODIGO, FECHA_SAL_REC, " _
                                           & " CENTRO_COSTO, SERIE_CS,SERIE_NUMERAR, SERIE_REFERENCIA FROM V_TBINV_INGRESO_BIENES ORDER BY FECHA_SAL_REC , CENTRO_COSTO  "
        RsDatos = CmdGlobal4.ExecuteReader
        If RsDatos.HasRows Then
            While RsDatos.Read
                If Nu(RsDatos("CENTRO_COSTO")) <> psAnteriorCC Then
                    psAnteriorCC = Nu(RsDatos("CENTRO_COSTO"))

                    CmdGlobal.CommandText = "SELECT * FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CECOSE_COD_INTERNO =  '" & Nu(RsDatos("CENTRO_COSTO")) & "' "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodDestino = Nz(Rs("CECOSE_CODIGO"))
                        End While
                    End If
                    Rs.Close()
                    psTipoDestino = "2"
                    psContador = psContador + 1
                    If Nu(RsDatos("FECHA_SAL_REC")) <> "" Then
                        txtFechaRecep.Text = FormatoFecha(Nu(RsDatos("FECHA_SAL_REC")))
                    Else
                        txtFechaRecep.Text = FormatoFecha(FechaActual())
                    End If
                    psCantidades = psCantidades + CDbl(Nz(RsDatos("ARTICULO_CANTIDAD")))
                    psFechaSalida = Right(txtFechaRecep.Text, 4) + Mid(txtFechaRecep.Text, 4, 2) + Left(txtFechaRecep.Text, 2)
                    If psTipoOrigen = "1" Then
                        '-----------------------SALIDA DE ALMACEN
                        CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        RsRecep = CmdGlobal.ExecuteReader
                        If RsRecep.HasRows Then
                            While RsRecep.Read
                                psCodDespacho = Nz(RsRecep(0)) + 1
                            End While
                        Else
                            psCodDespacho = 1
                        End If
                        RsRecep.Close()
                        If Session("CodSalida") <> "" Then Session("CodSalida") = Session("CodSalida") & " , "
                        Session("CodSalida") = Session("CodSalida") & Llenar_Ceros(psCodDespacho, 8)


                        CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                       & " CECOSE_CODIGO_DESTINO, " _
                                       & " DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                       & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC, DESP_REFERENCIA) " _
                                       & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "'," & HoraActual() & ",'" & Session("User") & "','2'," _
                                       & " " & psCodDestino & ", " _
                                       & " '3','0'," & cant & "," & cant & "," & cant & ",0," & psCodOrigen & ", '" & psFechaSalida & "','" & HoraActual() & "','" & DdlMotivo.SelectedValue.Trim & "','" & ValorSys & "', '" & TxtReferencia.Text & "')"
                        CmdGlobal.ExecuteNonQuery()
                    ElseIf psTipoOrigen = "2" Then 'SALIDA DE CENTRO DE COSTO
                        CmdGlobal.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & Session("Codempresa") & "'"
                        RsRecep = CmdGlobal.ExecuteReader
                        If RsRecep.HasRows Then
                            While RsRecep.Read
                                psCodDespacho = Nz(RsRecep(0)) + 1
                            End While
                        Else
                            psCodDespacho = 1
                        End If
                        RsRecep.Close()
                        CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, " _
                                    & " ALMACEN_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO, OSAL_PROVEEDOR_CODIGO, OSAL_CLIENTE_CODIGO, OSAL_PERSONA_CODIGO, " _
                                    & " OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                    & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL,OSAL_SYS_REC, OSAL_REFERENCIA) " _
                                    & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','2'," _
                                    & " " & psDestinoAlm & "," & psCodDestino & ", " & DesCodProveedor & ", " & DesCodCliente & ", " & DesCodPersona & ", " _
                                    & " '3','0'," & cant & "," & cant & ",0,'" & psCodOrigen & "'," _
                                    & " '" & psFechaSalida & "','" & HoraActual() & "','" & DdlMotivo.SelectedValue.Trim & "', '" & ValorSys & "')"
                        CmdGlobal.ExecuteNonQuery()
                    End If

                    CmdGlobal.CommandText = "SELECT MAX(ALLSAL_CODIGO) FROM TBINV_SALIDA_MOTIVO"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodAllSal = Nz(Rs(0)) + 1
                        End While
                    Else
                        psCodAllSal = 1
                    End If
                    Rs.Close()
                    CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                  & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                  & " VALUES ('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & psCodDespacho & ",'" & DdlMotivo.SelectedValue.Trim & "','" & psTipoOrigen & "'," & TxtOriCodigo.Text & ", " _
                                  & " '" & psTipoDestino & "'," & psCodDestino & ",'" & FechaActual() & "','" & HoraActual() & "','3','0','" & psFechaFormato & "')"
                    CmdGlobal.ExecuteNonQuery()
                    CodSalida = psCodDespacho

                End If
                Dim psCantItem As Double : psCantItem = 0
                Dim psItemSerie As Integer : psItemSerie = 0
                Dim psItemAcc As Integer : psItemAcc = 0

                psSerieNumerar = Nz(RsDatos("SERIE_NUMERAR"))
                psCodArt = Nz(RsDatos("ARTICULO_CODIGO")) '
                psCantItem = Nz(RsDatos("ARTICULO_CANTIDAD"))

                If psTipoOrigen = "1" Then
                    '-----------------------SALIDA DE ALMACEN
                    If psSerieNumerar <> "" Then
                        psItemSerie = psItemSerie + 1
                        CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                          & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & ",1," & psSerieNumerar & ",'" & ValorSys & "'," _
                                          & " '" & ValorSys & "','2','1','0')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ,DESPD_SYS_REC, DESPD_MODO_RECIBIDO) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & "," & psItemSerie & "," & psSerieNumerar & ",'S','0',NULL,'" & DdlMotivo.SelectedValue.Trim & "','S','" & ValorSys & "','M')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & psTipoDestino & "',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                                  & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'" & DdlMotivo.SelectedValue.Trim & "','0','" & ValorSys & "','" & psFechaFormato & "','1','" & psCodDespacho & "')"
                        CmdGlobal.ExecuteNonQuery()

                        CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " " _
                                              & " set SERIE_SKU = '" & Nu(RsDatos("SERIE_SKU")) & "', " _
                                              & " SERIE_GUIA = '" & Nu(RsDatos("SERIE_GUIA")) & "' " _
                                              & " WHERE SERIE_NUMERAR = " & Nz(RsDatos("SERIE_NUMERAR"))
                        CmdGlobal.ExecuteNonQuery()
                        'objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, psTipoOrigen, TxtOriCodigo.Text, psTipoDestino, psCodDestino, psSerieNumerar, Session("User"))
                    Else
                        psItemAcc = psItemAcc + 1
                        CmdGlobal.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET_SINSERIE( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM,ARTICULO_CODIGO,DESPD_CANTXDESP,DESPD_CANT_DESP,DESPD_CANT_REC,DESPD_CANT_FALT_REC,DESPD_SYS_EST,DESPD_MOTIVO) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & "," & psItemAcc & "," & psCodArt & "," & psCantItem & "," & psCantItem & "," & psCantItem & ",0,'0','" & DdlMotivo.SelectedValue.Trim & "')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                                          & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                                          & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & psItemAcc & "," & psCodArt & "," & psCantItem & "," & psCantItem & "," _
                                          & " " & psCantItem & "," & psCantItem & ",0,'2','1','0')"
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    'STOCK
                    StockAc = 0

                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodOrigen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - psCantItem

                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodOrigen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    Rs.Close()

                    'MOVIMIENTO GENERAL
                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblNroMovimiento = Nz(Rs(0)) + 1
                        End While
                    Else
                        lblNroMovimiento = 1
                    End If
                    Rs.Close()

                    'Call Movimiento_Kardex(psCodDespacho, cboRMotivo.ItemData(cboRMotivo.ListIndex), psCodArt, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, cboRMotivo, "2", txtFechaRecep, psCantItem)

                    Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, DdlMotivo.Text, "2", txtFechaRecep.Text.Trim, psCantItem)
                    CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                    & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                    & " VALUES ('" & Session("CodEmpresa") & "'," & lblNroMovimiento & ",'2','" & psTipoOrigen & "','" & psCodOrigen & "', " _
                                    & " " & psCodArt & "," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & Format(txtFechaRecep.Text, "yyyymmdd") & "','0','" & psCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                    CmdGlobal.ExecuteNonQuery()
                    '--------------------------recepcion en ccosto O ALMACEN
                    'STOCK
                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + psCantItem
                            CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        CmdGlobal2.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                        & " VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & "," & psCantItem & ",'0','" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()

                    'MOVIMIENTO GENERAL
                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblNroMovimiento = Nz(Rs(0)) + 1
                        End While
                    Else
                        lblNroMovimiento = 1
                    End If
                    Rs.Close()
                    'Call Movimiento_Kardex(psCodDespacho, cboRMotivo.ItemData(cboRMotivo.ListIndex), psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, cboRMotivo, "1", txtFechaRecep, psCantItem)

                    Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, DdlMotivo.Text, "1", txtFechaRecep.Text.Trim, psCantItem)
                    CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                    & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                    & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                                    & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & psFechaSalida & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodOrigen & "')"
                    CmdGlobal.ExecuteNonQuery()
                ElseIf psTipoOrigen = "2" Then 'SALIDA DE CENTRO DE COSTO
                    If psSerieNumerar <> "" Then
                        psItemSerie = psItemSerie + psCantItem
                        CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO,OSALD_SYS_REC ,OSALD_MODO_RECIBIDO) " _
                                        & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",1," & psSerieNumerar & ",'S','S','0','" & DdlMotivo.SelectedValue.Trim & "','" & ValorSys & "','A')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & psTipoDestino & "',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL)" _
                                                & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'" & DdlMotivo.SelectedValue.Trim & "','0','" & ValorSys & "','" & psFechaFormato & "','2','" & psCodDespacho & "')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " " _
                                              & " set SERIE_SKU = '" & Nu(RsDatos("SERIE_SKU")) & "', " _
                                              & " SERIE_GUIA = '" & Nu(RsDatos("SERIE_GUIA")) & "' " _
                                              & " WHERE SERIE_NUMERAR = " & Nz(RsDatos("SERIE_NUMERAR"))
                        CmdGlobal.ExecuteNonQuery()
                        'objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, psSerieNumerar, Session("User"))
                    Else
                        psItemAcc = psItemAcc + psCantItem
                        CmdGlobal.CommandText = "INSERT TBINV_CCOSTO_SALIDA_DET_SINSERIE(EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN,ARTICULO_CODIGO,OSALD_CANT_ENV,OSALD_CANT_REC,OSALD_CANT_FALT_REC ,OSALD_SYS_EST,OSALD_MOTIVO,OSALD_FUNCION) " _
                                        & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & "," & psItemAcc & "," & psCodArt & "," & psCantItem & "," & psCantItem & ",0,'0','" & DdlMotivo.SelectedValue.Trim & "','')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_SINSERIE_CCOSTO WHERE (CECOSE_CODIGO = " & psCodOrigen & ") " _
                                                & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SKSSCC_STOCK_ACTUAL) - psCantItem
                                CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_SINSERIE_CCOSTO SET SKSSCC_STOCK_ACTUAL=" & StockAc & " WHERE (CECOSE_CODIGO = " & psCodOrigen & ") " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        End If
                        Rs.Close()        '
                    End If
                    'STOCK
                    CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodOrigen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - psCantItem
                            CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodOrigen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    Rs.Close()

                    'MOVIMIENTO GENERAL
                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblNroMovimiento = Nz(Rs(0)) + 1
                        End While
                    Else
                        lblNroMovimiento = 1
                    End If
                    Rs.Close()

                    'Call Movimiento_Kardex(psCodDespacho, cboRMotivo.ItemData(cboRMotivo.ListIndex), psCodArt, psTipoOrigen, psCodOrigen, "1", psCodDestino, cboRMotivo, "2", txtFechaRecep, psCantItem)

                    Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, DdlMotivo.Text, "2", txtFechaRecep.Text.Trim, psCantItem)
                    CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                    & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                    & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & psCodOrigen & "', " _
                                    & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & psFechaSalida & "','0','" & psCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                    CmdGlobal.ExecuteNonQuery()
                    '--------------------------recepcion en ccosto O ALMACEN
                    'STOCK
                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + psCantItem
                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                        & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & "," & psCantItem & ",'0','" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()

                    'MOVIMIENTO GENERAL
                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblNroMovimiento = Nz(Rs(0)) + 1
                        End While
                    Else
                        lblNroMovimiento = 1
                    End If
                    Rs.Close()

                    'Call Movimiento_Kardex(psCodDespacho, cboRMotivo.ItemData(cboRMotivo.ListIndex), psCodArt, "1", psCodDestino, psTipoOrigen, psCodOrigen, cboRMotivo, "1", txtFechaRecep, psCantItem)

                    Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, DdlMotivo.Text, "1", txtFechaRecep.Text.Trim, psCantItem)
                    CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                            & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                            & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                                            & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & psFechaFormato & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodOrigen & "')"
                    CmdGlobal.ExecuteNonQuery()
                End If
                CmdGlobal.CommandText = " UPDATE V_TBINV_INGRESO_BIENES SET REGISTRO_SALIDA = " & psCodDespacho & "  WHERE REGISTRO_NRO = " & Nz(RsDatos("REGISTRO_NRO"))
                CmdGlobal.ExecuteNonQuery()
            End While
        End If
        RsDatos.Close()

        GenerarGuia(psCodDespacho, TxtDesCodigo.Text, TxtDesCodExterno.Text, psTipoDestino, "", pdCantBienes, pArrayP)

    End Sub


    Private Sub Despacho_unoxuno(ByVal psSerieCodigo As String, ByVal psFecha As String, ByVal psDestino As String,
                             ByVal psTipoDestino As String, ByVal psMotivo As String, ByVal psUbicaCodigo As String,
                             ByVal psUbicaTipo As String, ByVal psCodArticulo As String, Optional pReferencia As String = "")

        Dim ValorSys As String : ValorSys = FechaActual() + HoraActual() + Session("User")
        Dim psCodCECosto As String : psCodCECosto = ""
        Dim psCodSeccion As String : psCodSeccion = ""
        Dim psCodArt As String : psCodArt = ""
        Dim psSerieNumerar As String : psSerieNumerar = psSerieCodigo
        Dim psSerieNro As String : psSerieNro = ""
        Dim psPlacaNro As String : psPlacaNro = ""
        Dim psT As String : psT = ""
        Dim psFechaAdq As String : psFechaAdq = ""
        Dim ofun As New clsInv_Procesos
        Dim lblNroMovimiento As String : lblNroMovimiento = ""
        Dim StockAc As Double : StockAc = 0
        Dim cant As Double : cant = 0
        Dim i As Long : i = 0
        Dim psTipoOrigen As String : psTipoOrigen = ""
        Dim lblCodAlmacen As String : lblCodAlmacen = ""
        Dim lblCodDespacho As String : lblCodDespacho = ""
        Dim psCodDestino As String : psCodDestino = ""
        Dim psOrigenAlm As String : psOrigenAlm = "NULL"
        Dim psOrigenCC As String : psOrigenCC = "NULL"
        Dim psDestinoAlm As String : psDestinoAlm = "NULL"
        Dim psDestinoCC As String : psDestinoCC = "NULL"
        Dim psUbicaAlm As String : psUbicaAlm = "NULL"
        Dim psUbicaCC As String : psUbicaCC = "NULL"
        Dim DesCodProveedor As String : DesCodProveedor = "NULL"
        Dim DesCodCliente As String : DesCodCliente = "NULL"
        Dim DesCodPersona As String : DesCodPersona = "NULL"
        lblCodAlmacen = TxtOriCodigo.Text
        If RbOrigAlmacen.Checked = True Then psTipoOrigen = "1"
        If RbOrigCC.Checked = True Then psTipoOrigen = "2"
        If psTipoOrigen = "1" Then psOrigenAlm = lblCodAlmacen
        If psTipoOrigen = "2" Then psOrigenCC = lblCodAlmacen
        If psTipoDestino = "1" Then psDestinoAlm = psDestino
        If psTipoDestino = "2" Then psDestinoCC = psDestino
        If psTipoDestino = "3" Then DesCodProveedor = psDestino
        If psTipoDestino = "4" Then DesCodCliente = psDestino
        If psTipoDestino = "5" Then DesCodPersona = psDestino
        If psTipoOrigen = "1" Then psUbicaAlm = lblCodAlmacen
        If psTipoOrigen = "2" Then psUbicaCC = lblCodAlmacen
        CodSalida = 0
        If psDestino <> "" Then psCodDestino = psDestino
        StockAc = 0
        Dim psRecepcion As String : psRecepcion = ""
        i = 0
        Dim psProveedor As String = ""
        Dim psCodRecepcion As String = ""
        Dim psCodAllSal As String = ""
        Dim dtArt As New DataTable


        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim objProceso As New clsInv_Procesos
        Dim psFechaFormato As String = ""
        Dim psHoraFormato As String = ""
        psFechaFormato = Mid(txtFechaRecep.Text, 7, 4) + Mid(txtFechaRecep.Text, 4, 2) + Mid(txtFechaRecep.Text, 1, 2)
        psHoraFormato = Mid(txtHoraRegistra.Text, 1, 2) + Mid(txtHoraRegistra.Text, 4, 2)

        psSerieNumerar = psSerieCodigo
        psCodArt = psCodArticulo
        psRecepcion = ""
        If psTipoOrigen <> psUbicaTipo Or lblCodAlmacen <> psUbicaCodigo Then
            If psUbicaTipo = "1" Then
                '-----------------------SALIDA DE ALMACEN
                CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblCodDespacho = Nu(Rs(0)) + 1
                    End While
                Else
                    lblCodDespacho = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = "SELECT MAX(ALLSAL_CODIGO) FROM TBINV_SALIDA_MOTIVO"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodAllSal = Nu(Rs(0)) + 1
                    End While
                Else
                    psCodAllSal = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                      & " VALUES ('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & lblCodDespacho & ",'" & psMotivo & "','1'," & psUbicaCodigo & ", " _
                                      & " '" & psTipoOrigen & "'," & lblCodAlmacen & ",'" & psFechaFormato & "','" & HoraActual() & "','3','0','" & psFechaFormato & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                      & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                      & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & ",1," & psSerieNumerar & ",'" & ValorSys & "'," _
                                      & " '" & ValorSys & "','2','1','0')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                       & " CECOSE_CODIGO_DESTINO,ALMACEN_CODIGO_DESTINO, " _
                                       & " DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                       & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC,DESP_REFERENCIA) " _
                                       & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "'," & HoraActual() & ",'" & Session("User") & "','" & psTipoOrigen & "'," _
                                       & " " & psUbicaCC & ", " & psUbicaAlm & ", " _
                                       & " '2', '0', 1, 1, 0, 1, " & psUbicaCodigo & ", '" & psFechaFormato & "', '" & psHoraFormato & "', '" & psMotivo & "', '" & ValorSys & "', '" & pReferencia & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','0',NULL,'" & psMotivo & "','N')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                'STOCK
                StockAc = 0
                CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psUbicaCodigo & ") AND (UBICACT_TIPO='" & psUbicaTipo & "') " _
                                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psUbicaCodigo & ") AND (UBICACT_TIPO='" & psUbicaTipo & "') " _
                                                     & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()

                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()

                'Call Movimiento_Kardex(lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, cboRMotivo, "2", FormatoFecha(psFecha), 1)

                Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, DdlMotivo.Text, "2", txtFechaRecep.Text.Trim, 1)
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psUbicaTipo & "','" & psUbicaCodigo & "', " _
                                              & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaFormato & "','0','" & lblCodDespacho & "','" & psTipoOrigen & "'," & lblCodAlmacen & ")"
                CmdGlobal.ExecuteNonQuery()
                '--------------------------recepcion en ccosto O ALMACEN
                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & Session("CodEmpressa") & "' AND DESP_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='3',DESP_CANT_REC='1',DESP_CANT_FALT_REC='0' WHERE DESP_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                CmdGlobal.ExecuteNonQuery()
                'STOCK
                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                     & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                Else
                    CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                     & "VALUES(" & lblCodAlmacen & ",'" & psTipoOrigen & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End If
                Rs.Close()

                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()
                'Call Movimiento_Kardex(lblCodDespacho, psMotivo, psCodArt, psTipoOrigen, lblCodAlmacen, psUbicaTipo, psUbicaCodigo, cboRMotivo, "1", FormatoFecha(psFecha), 1)

                Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoOrigen, lblCodAlmacen, psUbicaTipo, psUbicaCodigo, DdlMotivo.Text, "1", txtFechaRecep.Text.Trim, 1)
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                       & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                       & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoOrigen & "'," & lblCodAlmacen & ", " _
                                       & " " & psCodArt & ",'1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaFormato & "','0','" & lblCodDespacho & "','" & psUbicaTipo & "','" & psUbicaCodigo & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & psTipoOrigen & "',UBICACT_CODIGO=" & lblCodAlmacen & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                              & " VALUES ('" & psSerieNumerar & "','" & psTipoOrigen & "'," & lblCodAlmacen & ",'" & psMotivo & "','0','" & ValorSys & "','" & psFechaFormato & "','1','" & lblCodDespacho & "')"
                CmdGlobal.ExecuteNonQuery()
                objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, psSerieNumerar, Session("User"))
                '      GoTo SalidaBien
            ElseIf psUbicaTipo = "2" Then  'SALIDA DE CENTRO DE COSTO
                CmdGlobal.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblCodDespacho = Nz(Rs(0)) + 1
                    End While
                Else
                    lblCodDespacho = 1
                End If
                Rs.Close()

                CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, " _
                                                  & " CECOSE_CODIGO_DESTINO, ALMACEN_CODIGO_DESTINO, OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                                  & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL, OSAL_REFERENCIA) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & psTipoOrigen & "'," _
                                                  & " " & psUbicaCC & "," & psUbicaAlm & ",'2','0',1,0,1,'" & psUbicaCodigo & "'," _
                                                  & " '" & psFechaFormato & "','" & HoraActual() & "','" & psMotivo & "', '" & pReferencia & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "SELECT MAX(ALLSAL_CODIGO) FROM TBINV_SALIDA_MOTIVO"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodAllSal = Nz(Rs(0)) + 1
                    End While
                Else
                    psCodAllSal = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                      & " VALUES ('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & lblCodDespacho & ",'" & psMotivo & "','2'," & psUbicaCodigo & ", " _
                                      & " '" & psTipoOrigen & "'," & lblCodAlmacen & ",'" & psFechaFormato & "','" & HoraActual() & "','3','0','" & psFechaFormato & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                      & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                      & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & ",1," & psSerieNumerar & ",'" & ValorSys & "'," _
                                      & " '" & ValorSys & "','2','1','0')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','N','0','" & psMotivo & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()

                'STOCK
                CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psUbicaCodigo & ") AND (UBICACT_TIPO='" & psUbicaTipo & "') " _
                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psUbicaCodigo & ") AND (UBICACT_TIPO='" & psUbicaTipo & "') " _
                                                     & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()

                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()

                'Call Movimiento_Kardex(lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, cboRMotivo, "2", txtFechaRecep, 1)
                Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, DdlMotivo.Text, "2", txtFechaRecep.Text.Trim, 1)

                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psUbicaTipo & "','" & psUbicaCodigo & "', " _
                                              & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaFormato & "','0','" & lblCodDespacho & "','" & psTipoOrigen & "'," & lblCodAlmacen & ")"
                CmdGlobal.ExecuteNonQuery()
                '--------------------------recepcion en ccosto O ALMACEN
                CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET  SET RECIBIDA_OK ='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO='M' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA  SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='3',OSAL_CANT_REC='1',OSAL_CANT_FALT_REC='0' WHERE OSAL_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                CmdGlobal.ExecuteNonQuery()
                'STOCK
                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                              & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                Else
                    CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                          & "VALUES(" & lblCodAlmacen & ",'" & psTipoOrigen & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End If
                Rs.Close()

                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()

                'Call Movimiento_Kardex(lblCodDespacho, "20", psCodArt, "1", psCodDestino, psTipoOrigen, lblCodAlmacen, cboRMotivo, "1", txtFechaRecep, 1)

                Call ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, lblCodAlmacen, DdlMotivo.Text, "1", txtFechaRecep.Text.Trim, 1)
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoOrigen & "'," & lblCodAlmacen & ", " _
                                              & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaFormato & "','0','" & lblCodDespacho & "','" & psUbicaTipo & "','" & psUbicaCodigo & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & psTipoOrigen & "',UBICACT_CODIGO=" & lblCodAlmacen & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL)" _
                                              & " VALUES ('" & psSerieNumerar & "','" & psTipoOrigen & "'," & lblCodAlmacen & ",'" & psMotivo & "','0','" & ValorSys & "','" & psFechaFormato & "','2','" & lblCodDespacho & "')"
                CmdGlobal.ExecuteNonQuery()
                objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, psSerieNumerar, Session("User"))
                '       GoTo SalidaBien
            End If
        Else
            '
        End If

        If lblCodDespacho <> "" Then CodSalida = lblCodDespacho

    End Sub



    Private Sub GenerarGuia(ByVal psCodDespacho As String, ByVal psDestinoCodigo As String, ByVal psDestinoCodInterno As String, ByVal psDestino As String, ByVal psDestinoDireccion As String, ByVal psCantProductos As Integer, ByRef pArrayP As String(,), Optional psObservacion As String = "")
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn4 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim RsDatos As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Cn4.Open() : CmdGlobal4.Connection = Cn4
        Dim ValorSys As String
        ValorSys = FechaActual() & HoraActual() & Session("User")

        Dim psTipoOrigen As String = ""
        If RbOrigAlmacen.Checked = True Then psTipoOrigen = "1"
        If RbOrigCC.Checked = True Then psTipoOrigen = "2"
        Dim psTipoDestino As String = ""
        If RBAlmacen.Checked = True Then psTipoDestino = "1"
        If RBCentroC.Checked = True Then psTipoDestino = "2"
        Dim psGuiaNro2 As Double = 0
        Dim psGuiaNro As String = ""
        psGuiaNro = TxtGuiaNumero.Text
        Dim psGuiaCodigo As String = ""
        Dim psGuiaAnterior As String = ""
        Dim psVariable As String = ""
        Dim psLong As Double = 0
        Dim psNumeroGuia As String = ""
        Dim psSerieGuia As String = ""
        Dim psFechaFormato As String = ""
        Dim psHoraFormato As String = ""
        Dim psFechaGuia As String = ""

        Dim i As Double = 0
        psFechaFormato = Mid(txtFechaRecep.Text, 7, 4) + Mid(txtFechaRecep.Text, 4, 2) + Mid(txtFechaRecep.Text, 1, 2)
        psHoraFormato = Mid(txtHoraRegistra.Text, 1, 2) + Mid(txtHoraRegistra.Text, 4, 2)
        CmdGlobal4.CommandText = " SELECT REGISTRO_SALIDA, REGISTRO_NRO, ARTICULO_CODIGO, ARTICULO_CANTIDAD, SERIE_NRO, PLACA_NRO, FECHA_ADQ, SERIE_SKU, " _
                                           & " SERIE_OC, SERIE_GUIA, SERIE_REFERENCIA, SERIE_UBICACT_TIPO, SERIE_UBICACT_CODIGO, FECHA_SAL_REC, " _
                                           & " CENTRO_COSTO, SERIE_CS,SERIE_NUMERAR FROM V_TBINV_INGRESO_BIENES ORDER BY FECHA_SAL_REC , CENTRO_COSTO  "
        RsDatos = CmdGlobal4.ExecuteReader
        If RsDatos.HasRows Then
            While RsDatos.Read
                i = i + 1
                If Nu(RsDatos("SERIE_GUIA")) <> psGuiaAnterior Then
                    psGuiaAnterior = Nu(RsDatos("SERIE_GUIA"))
                    psVariable = Nu(RsDatos("SERIE_GUIA"))
                    psDestinoCodigo = Nu(RsDatos("CENTRO_COSTO"))
                    psLong = InStr(psVariable, "-")
                    If psLong = 0 Then
                        psNumeroGuia = psVariable
                    Else
                        psSerieGuia = Left(psVariable, psLong - 1)
                        psNumeroGuia = Mid(psVariable, psLong + 1)
                    End If
                    If Nu(RsDatos("FECHA_SAL_REC")) <> "" Then
                        txtFechaRecep.Text = FormatoFecha(Nu(RsDatos("FECHA_SAL_REC")))
                    Else
                        txtFechaRecep.Text = FormatoFecha(FechaActual())
                    End If
                    psFechaGuia = Mid(txtFechaRecep.Text, 7, 4) + Mid(txtFechaRecep.Text, 4, 2) + Mid(txtFechaRecep.Text, 1, 2)


                    CmdGlobal.CommandText = "SELECT isnull(MAX(GUIREM_CODIGO),0) FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psGuiaCodigo = Llenar_Ceros(Nz(Rs(0)) + 1, 10)
                        End While
                    Else
                        psGuiaCodigo = "0000000001"
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = "SELECT * FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CECOSE_COD_INTERNO =  " & psDestinoCodigo
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psDestinoCodigo = Nz(Rs("CECOSE_CODIGO"))
                            psDestino = Nu(Rs("CECOSE_DESCRIPCION"))
                            psDestinoDireccion = Nu(Rs("CECOSE_DIRECCION"))
                        End While
                    End If
                    Rs.Close()
                    psGuiaNro = psNumeroGuia

                    CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUIREM_TIPO, GUIREM_SERIE,GUIREM_NUMERO, GUIREM_SYS_EST,GUIREM_SYS_CRE,GUIREM_RECEPCIONADA,GUIREM_ESTADO) " _
                                        & " VALUES(" & psGuiaCodigo & ",'1','" & psSerieGuia & "','" & Trim(psGuiaNro) & "','0','" & ValorSys & "','0','0')"
                    CmdGlobal.ExecuteNonQuery()

                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_SERIE SET GURESE_VALOR_INICIAL = " & Val(psGuiaNro) + 1 & "  WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND GURESE_NUMERO = '" & psSerieGuia & "' AND GURESE_TIPO_DOC='09'"
                    CmdGlobal.ExecuteNonQuery()

                    Dim psModalidadTransporte As String = "2"
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaGuia & "', GUIREM_HORA='" & psHoraFormato & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaGuia & "', GUIREM_HORA_TRASLADO='" & psHoraFormato & "', " _
                                        & " GUIREM_TIPO_REMITENTE='" & psTipoOrigen & "', ALMACEN_CODIGO_REMITENTE=" & TxtOriCodigo.Text & ",CECOSE_CODIGO_REMITENTE = NULL,GUIREM_CURRIER = NULL,GUIREM_ESTADO_ENTREGA='1',GUIREM_ESTADO_SITUACION='2'," _
                                        & " GUIREM_DIRECCION_PARTIDA ='',GUIREM_TIPO_DESTINATARIO='" & psTipoDestino & "',ALMACEN_CODIGO_DESTINATARIO=NULL," _
                                        & " CECOSE_CODIGO_DESTINATARIO=" & psDestinoCodigo & ", GUIREM_NOMBRE_DESTINATARIO = '" & Trim(psDestino) & "', GUIREM_DIRECCION_LLEGADA ='" & Trim(psDestinoDireccion) & "'," _
                                        & " GUIREM_MOTIVO_TRASLADO='7' ,TRANSPORTISTA_MODALIDAD = '" & psModalidadTransporte & "',GUIREM_DIRECCION_PARTIDA_UBIGEO = '' " _
                                        & " WHERE GUIREM_CODIGO = " & psGuiaCodigo
                    CmdGlobal.ExecuteNonQuery()

                    'If ls_CodTicket <> "" Then  'NUM_TICKET
                    '    'Call Ticket_GrabarTrackingAcciones(ls_CodTicket, ls_CodAccion, Format(txtFechaRecep, "yyyymmdd"), Format(txtHoraRecep, "hhmm"), psGuiaCodigo, "NUM_TICKET", "GUIREM_CODIGO", "TBINV_GUIA_REMISION_" & SistCodEmpresa)
                    'End If
                    'CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  " _
                    '        & " TRANSPORTISTA_RUC='" & TxtRucTrasnportista.Text & "',TRANSPORTISTA_RAZONSOCIAL='" & TxtRazonTransportista.Text & "'," _
                    '        & " VEHICU_PLACA='" & TxtNroPlaca.Text & "',VEHICU_MARCA='" & TxtMarca.Text & "',VEHICU_CERT_INSCP='" & TxtCertInscripcion.Text & "',CHOFER_DNI='" & TxtChoferDNI & "',CHOFER_NOMBRES='" & TxtChoferNombre.Text & "',CHOFER_LICENCIA='" & TxtLicencia.Text & "', VEHI_CONFIGURACION = '" & TxtconfVehicular.Text & "', " _
                    '        & " GUIREM_SYS_MOD='" & ValorSys & "'  WHERE GUIREM_CODIGO = " & psGuiaCodigo
                    'CmdGlobal.ExecuteNonQuery()

                End If
                Dim psObsGuia As String = ""
                'psObsGuia = psObservacion & " / " & pArrayP(i, 4)
                CmdGlobal2.CommandText = " UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  " _
                                          & " GUIREM_OBSERVACION='" & psObsGuia & "'  WHERE GUIREM_CODIGO = " & psGuiaCodigo
                CmdGlobal2.ExecuteNonQuery()
                If Nz(RsDatos("SERIE_NUMERAR")) <> 0 Then
                    If Nz(RsDatos("PLACA_NRO")) <> 0 Then
                        CmdGlobal2.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO, ARTICULO_CODIGO,GUREDE_CANTIDAD,SERIE_NUMERAR,SERIE_NRO,PLACA_NRO) " _
                                            & " VALUES(" & psGuiaCodigo & "," & i & "," & Nz(RsDatos("REGISTRO_SALIDA")) & "," & Nz(RsDatos("ARTICULO_CODIGO")) & "," & Nz(RsDatos("ARTICULO_CANTIDAD")) & ", " & Nz(RsDatos("SERIE_NUMERAR")) & ", '" & Nu(RsDatos("SERIE_NRO")) & "', " & Nz(RsDatos("PLACA_NRO")) & ")"
                        CmdGlobal2.ExecuteNonQuery()
                    Else
                        CmdGlobal2.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO, ARTICULO_CODIGO,GUREDE_CANTIDAD,SERIE_NUMERAR,SERIE_NRO) " _
                                            & " VALUES(" & psGuiaCodigo & "," & i & "," & Nz(RsDatos("REGISTRO_SALIDA")) & "," & Nz(RsDatos("ARTICULO_CODIGO")) & "," & Nz(RsDatos("ARTICULO_CANTIDAD")) & ", " & Nz(RsDatos("SERIE_NUMERAR")) & ", '" & Nu(RsDatos("SERIE_NRO")) & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                Else
                    CmdGlobal2.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_SINSERIE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO,ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                        & " VALUES(" & psGuiaCodigo & "," & i & "," & Nz(RsDatos("REGISTRO_SALIDA")) & "," & Nz(RsDatos("ARTICULO_CODIGO")) & "," & Nz(RsDatos("ARTICULO_CANTIDAD")) & ")"
                    CmdGlobal2.ExecuteNonQuery()
                End If


                CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET GUIREM_CODIGO=" & psGuiaCodigo & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO = " & Nz(RsDatos("REGISTRO_SALIDA"))
                CmdGlobal2.ExecuteNonQuery()

                'If ls_CodTicket <> "" Then
                '    CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_TICKET = " & ls_CodTicket & " " _
                '                      & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND DESP_CODIGO = " & pArrayP(i, 1)
                '    CmdGlobal2.ExecuteNonQuery()
                'End If
            End While
        End If
        RsDatos.Close()
    End Sub




    Private Sub Ingreso_Equipo_AAlmacen(ByVal psSerieCodigo As String, ByVal psEstado As String, ByVal psZona As String, ByVal psObs As String,
                                        ByVal psFecha As String, ByVal psDestino As String, ByVal psVolumen As String, ByVal psViene As String,
                                        ByVal psTipoDestino As String, ByVal psValidado As String, ByVal psDatoaValidar As String, ByVal psDescripcion As String,
                                        Optional psMotivo As String = "", Optional psMotivoDesc As String = "", Optional psCodGuia As String = "", Optional pCodRecepcion As Double = 0, Optional pReferencia As String = "")
        Dim ffCn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim ffCn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim ffCn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim ffCn4 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim ffCmdGlobal As New SqlCommand
        Dim ffCmdGlobal2 As New SqlCommand
        Dim ffCmdGlobal3 As New SqlCommand
        Dim ffCmdGlobal4 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim ValorSys As String : ValorSys = Session("User") & FechaActual() & HoraActual()
        Dim psCodCECosto As String : psCodCECosto = ""
        Dim psCodSeccion As String : psCodSeccion = ""
        Dim psCodArt As String : psCodArt = ""
        Dim psSerieNumerar As String : psSerieNumerar = psSerieCodigo
        Dim psSerieNro As String : psSerieNro = ""
        Dim psPlacaNro As String : psPlacaNro = ""
        Dim psT As String : psT = ""
        Dim psFechaAdq As String : psFechaAdq = ""
        Dim lblNroMovimiento As String : lblNroMovimiento = ""
        Dim StockAc As Double : StockAc = 0
        Dim cant As Double : cant = 0
        Dim i As Long : i = 0
        Dim psTipoOrigen As String : psTipoOrigen = ""
        Dim lblCodAlmacen As String : lblCodAlmacen = ""
        Dim lblCodDespacho As String : lblCodDespacho = ""
        Dim psCodDestino As String : psCodDestino = "NULL"
        Dim psCodDestinoAlm As String : psCodDestinoAlm = "NULL"
        Dim psCodDestinoCC As String : psCodDestinoCC = "NULL"
        Dim psCodOrigenAlm As String : psCodOrigenAlm = "NULL"
        Dim psCodOrigenCC As String : psCodOrigenCC = "NULL"
        CodSalida = 0
        If psDestino <> "" Then psCodDestino = psDestino
        If psTipoDestino = "2" Then psCodDestinoCC = psCodDestino
        If psTipoDestino = "1" Then psCodDestinoAlm = psCodDestino
        StockAc = 0
        Dim psFechaRecepcion As String = ""
        psFechaRecepcion = psFecha
        Dim psHoraRecepcion As String = ""
        psHoraRecepcion = Left(txtHoraRegistra.Text, 2) + Mid(txtFecRegistra.Text, 4, 2)
        Dim oFun As New clsInv_Procesos
        Dim psRecepcion As String : psRecepcion = ""
        i = 0
        ffCn.Open() : ffCmdGlobal.Connection = ffCn
        ffCn2.Open() : ffCmdGlobal2.Connection = ffCn2
        ffCn3.Open() : ffCmdGlobal3.Connection = ffCn3
        ffCn4.Open() : ffCmdGlobal4.Connection = ffCn4


        ffCmdGlobal.CommandText = " SELECT SERIE_NRO, SERIE_NUMERAR, UBICACT_TIPO, UBICACT_CODIGO,ARTICULO_CODIGO " _
                              & " FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " WHERE SERIE_NUMERAR = " & psSerieNumerar
        Rs2 = ffCmdGlobal.ExecuteReader
        If Rs2.HasRows Then
            While Rs2.Read
                lblCodAlmacen = Nu(Rs2!ubicact_codigo)
                psTipoOrigen = Nu(Rs2!ubicact_tipo)
                psSerieNumerar = Nu(Rs2!Serie_Numerar)
                psCodArt = Nu(Rs2!ARTICULO_CODIGO)
                psRecepcion = ""
                If psTipoOrigen = psTipoDestino And lblCodAlmacen = psCodDestino Then
                ElseIf psTipoOrigen = "" And lblCodAlmacen = "" Then ' SOLO INGRESO NO HAY SALIDA
                    ffCmdGlobal2.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                           & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    Rs = ffCmdGlobal2.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                            ffCmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                                 & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                            ffCmdGlobal3.ExecuteNonQuery()
                        End While
                    Else
                        ffCmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                 & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                        ffCmdGlobal3.ExecuteNonQuery()
                    End If
                    Rs.Close()

                    ''MOVIMIENTO GENERAL
                    'ffCmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                    'Rs = ffCmdGlobal2.ExecuteReader
                    'If Rs.HasRows Then
                    '    While Rs.Read
                    '        lblNroMovimiento = Nz(Rs(0)) + 1
                    '    End While
                    'Else
                    '    lblNroMovimiento = 1
                    'End If
                    'Rs.Close()
                    'oFun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, lblCodAlmacen, psMotivoDesc, "1", txtFechaRecep.Text, 1)

                    'ffCmdGlobal3.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                    '               & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                    '               & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                    '               & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaRecepcion & "','0','" & lblCodDespacho & "','" & psTipoOrigen & "','" & lblCodAlmacen & "')"
                    'ffCmdGlobal3.ExecuteNonQuery()
                    ffCmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                    ffCmdGlobal3.ExecuteNonQuery()
                    ffCmdGlobal3.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                          & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'" & psMotivo & "','0','" & ValorSys & "','" & psFechaRecepcion & "','1','" & lblCodDespacho & "')"
                    ffCmdGlobal3.ExecuteNonQuery()
                Else
                    If psTipoOrigen = "1" Then

                        'If psTipoOrigen <> psTipoDestino And lblCodAlmacen <> psCodDestino Then

                        'End If
                        '-----------------------SALIDA DE ALMACEN
                        ffCmdGlobal2.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                lblCodDespacho = Nz(Rs(0)) + 1
                            End While
                        Else
                            lblCodDespacho = 1
                        End If

                        Rs.Close()
                        ffCmdGlobal2.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA_SAL, DESP_HORA_SAL, DESP_USUARIO, DESP_TIPODESTINO," _
                                    & " CECOSE_CODIGO_DESTINO,ALMACEN_CODIGO_DESTINO,DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                    & " DESP_FECHA,DESP_HORA,DESP_MOTIVO_GRAL,DESP_SYS_EJEC, DESP_REFERENCIA) " _
                                    & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & psFechaRecepcion & "','" & psHoraRecepcion & "','" & Session("User") & "','" & psTipoDestino & "'," _
                                    & " " & psCodDestinoCC & "," & psCodDestinoAlm & ",'2','0',1,1,0,1," & lblCodAlmacen & "," _
                                    & " '" & FechaActual() & "','" & HoraActual() & "','" & psMotivo & "','" & ValorSys & "', '" & pReferencia & "')"
                        ffCmdGlobal2.ExecuteNonQuery()

                        If psCodGuia <> "" Then
                            ffCmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET GUIREM_CODIGO = " & psCodGuia & ", DESP_TIPO_DOC_SALIDA_NRO = " & psCodGuia & " WHERE DESP_CODIGO = " & lblCodDespacho
                            ffCmdGlobal2.ExecuteNonQuery()

                            If psSerieNumerar <> "" Then
                                ffCmdGlobal2.CommandText = " UPDATE TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & " SET DESP_CODIGO = " & lblCodDespacho & " WHERE GUIREM_CODIGO = " & psCodGuia & " AND SERIE_NUMERAR = " & psSerieNumerar : ffCmdGlobal.ExecuteNonQuery()
                            End If
                        End If
                        ffCmdGlobal2.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                                & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','0',NULL,'" & psMotivo & "','N')"
                        ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                        'STOCK descontar origen
                        StockAc = 0
                        ffCmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                                ffCmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                ffCmdGlobal3.ExecuteNonQuery()

                            End While
                        End If
                        Rs.Close()

                        'MOVIMIENTO GENERAL descontar origen
                        ffCmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                lblNroMovimiento = Nz(Rs(0)) + 1
                            End While
                        Else
                            lblNroMovimiento = 1
                        End If
                        Rs.Close()

                        oFun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoOrigen, lblCodAlmacen, psTipoDestino, psCodDestino, psMotivoDesc, "2", txtFechaRecep.Text, 1)

                        ffCmdGlobal2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                            & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                            & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & lblCodAlmacen & "', " _
                                            & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaRecepcion & "','0','" & lblCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                        ffCmdGlobal2.ExecuteNonQuery()
                        '--------------------------recepcion en ccosto O ALMACEN
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='3',DESP_CANT_REC='1',DESP_CANT_FALT_REC='0' WHERE DESP_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        ffCmdGlobal2.ExecuteNonQuery()
                        'STOCK aumentar en destino
                        ffCmdGlobal2.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                                ffCmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                ffCmdGlobal3.ExecuteNonQuery()

                            End While
                        Else
                            ffCmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                    & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                            ffCmdGlobal3.ExecuteNonQuery()
                        End If
                        Rs.Close()

                        ''MOVIMIENTO GENERAL aumentar en destino
                        'ffCmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        'Rs = ffCmdGlobal2.ExecuteReader
                        'If Rs.HasRows Then
                        '    While Rs.Read
                        '        lblNroMovimiento = Nz(Rs(0)) + 1
                        '    End While
                        'Else
                        '    lblNroMovimiento = 1
                        'End If
                        'Rs.Close()

                        'oFun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, lblCodAlmacen, psMotivoDesc, "1", txtFechaRecep.Text, 1)

                        'ffCmdGlobal2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                        '            & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                        '            & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                        '            & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaRecepcion & "','0','" & lblCodDespacho & "','" & psTipoOrigen & "','" & lblCodAlmacen & "')"
                        'ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                            & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'" & psMotivo & "','0','" & ValorSys & "','" & psFechaRecepcion & "','1','" & lblCodDespacho & "')"
                        ffCmdGlobal2.ExecuteNonQuery()
                    ElseIf psTipoOrigen = "2" Then 'SALIDA DE CENTRO DE COSTO
                        ffCmdGlobal2.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                lblCodDespacho = Nz(Rs(0)) + 1
                            End While
                        Else
                            lblCodDespacho = 1
                        End If
                        Rs.Close()
                        ffCmdGlobal2.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, " _
                                                  & " CECOSE_CODIGO_DESTINO, ALMACEN_CODIGO_DESTINO, OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                                  & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL, OSAL_REFERENCIA) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & psTipoDestino & "'," _
                                                  & " " & psCodDestinoCC & "," & psCodDestinoAlm & ",'2','0',1,0,1,'" & lblCodAlmacen & "'," _
                                                  & " '" & psFechaRecepcion & "','" & psHoraRecepcion & "','" & psMotivo & "', '" & pReferencia & "')"
                        ffCmdGlobal2.ExecuteNonQuery()

                        If psCodGuia <> "" Then
                            ffCmdGlobal2.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET GUIREM_CODIGO = " & psCodGuia & " WHERE OSAL_CODIGO = " & lblCodDespacho
                            ffCmdGlobal2.ExecuteNonQuery()

                            If psSerieNumerar <> "" Then
                                ffCmdGlobal2.CommandText = " UPDATE TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & " SET OSAL_CODIGO = " & lblCodDespacho & " WHERE GUIREM_CODIGO = " & psCodGuia & " AND SERIE_NUMERAR = " & psSerieNumerar : ffCmdGlobal2.ExecuteNonQuery()
                            End If
                        End If
                        ffCmdGlobal2.CommandText = "INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO) " _
                                                & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','N','0','" & psMotivo & "')"
                        ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()

                        'STOCK
                        ffCmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                                ffCmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                ffCmdGlobal3.ExecuteNonQuery()

                            End While
                        End If
                        Rs.Close()

                        'MOVIMIENTO GENERAL
                        ffCmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                lblNroMovimiento = Nz(Rs(0)) + 1
                            End While
                        Else
                            lblNroMovimiento = 1
                        End If
                        Rs.Close()

                        oFun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoOrigen, lblCodAlmacen, psTipoDestino, psCodDestino, psMotivoDesc, "2", txtFechaRecep.Text, 1)

                        ffCmdGlobal2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                            & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                            & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & lblCodAlmacen & "', " _
                                            & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaRecepcion & "','0','" & lblCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                        ffCmdGlobal2.ExecuteNonQuery()
                        '--------------------------recepcion en ccosto O ALMACEN
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET  SET RECIBIDA_OK ='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO='M' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_CCOSTO_SALIDA  SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='3',OSAL_CANT_REC='1',OSAL_CANT_FALT_REC='0' WHERE OSAL_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        ffCmdGlobal2.ExecuteNonQuery()
                        'STOCK
                        ffCmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        Rs = ffCmdGlobal2.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                                ffCmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                ffCmdGlobal3.ExecuteNonQuery()
                            End While
                        Else
                            ffCmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                        & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                            ffCmdGlobal3.ExecuteNonQuery()
                        End If
                        Rs.Close()

                        ''MOVIMIENTO GENERAL
                        'ffCmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                        'Rs = ffCmdGlobal2.ExecuteReader
                        'If Rs.HasRows Then
                        '    While Rs.Read
                        '        lblNroMovimiento = Nz(Rs(0)) + 1
                        '    End While
                        'Else
                        '    lblNroMovimiento = 1
                        'End If
                        'Rs.Close()

                        'oFun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, "1", psCodDestino, psTipoOrigen, lblCodAlmacen, psMotivoDesc, "1", txtFechaRecep.Text, 1)

                        'ffCmdGlobal2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                        '                    & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                        '                    & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                        '                    & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaRecepcion & "','0','" & lblCodDespacho & "','" & psTipoOrigen & "','" & lblCodAlmacen & "')"
                        'ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                        ffCmdGlobal2.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL)" _
                                            & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'" & psMotivo & "','0','" & ValorSys & "','" & psFechaRecepcion & "','2','" & lblCodDespacho & "')"
                        ffCmdGlobal2.ExecuteNonQuery()
                    End If
                End If
                If psViene <> "G" Then
                    ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_CARGADO_INV = '2' WHERE SERIE_NUMERAR=" & psSerieNumerar
                    ffCmdGlobal2.ExecuteNonQuery()
                    ffCmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ESTADO_EQUIPO = '" & psEstado & "', " _
                                  & " SERIE_RESPONSABLE_OBSERVACION = '" & psObs & "', SERIE_CUSTODIA_FECHAFIN = '" & psFecha & "', SERIE_VALIDADO = '" & psValidado & "', SERIE_DATOS_AVALIDAR = '" & psDatoaValidar & "', SERIE_DATOS_DESCRIPCION = '" & psDescripcion & "' " _
                                  & " WHERE SERIE_NUMERAR=" & psSerieNumerar
                    ffCmdGlobal2.ExecuteNonQuery()

                    If psValidado = "1" Then
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ESTADO_VALIDADO = '1' WHERE SERIE_NUMERAR=" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                    End If
                    If Nz(psVolumen) > 0 Then
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_VOLUMEN = '" & psVolumen & "' WHERE SERIE_NUMERAR=" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                    End If
                    If psZona <> "" Then
                        ffCmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ZONA = " & psZona & " WHERE SERIE_NUMERAR=" & psSerieNumerar
                        ffCmdGlobal2.ExecuteNonQuery()
                    End If
                End If
                CodSalida = Nz(lblCodDespacho)
            End While
        Else
            Exit Sub
        End If
        Rs2.Close()

    End Sub

    Private Sub GvListaNoEncontrados_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaNoEncontrados.RowCommand
        Dim arrSelec(,) As String
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Session("NuevaSerie") = ""
        Session("NuevaPlaca") = ""
        If e.CommandName = "QuitarFila" Then
            f = -1
            Erase arrSelec
            Session("CountArrayEq") = "-1"
            With GvListaNoEncontrados
                For i = 0 To .Rows.Count - 1
                    If i <> index Then
                        f = f + 1
                        ReDim Preserve arrSelec(23, f)
                        arrSelec(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(5, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(6, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(10, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(11, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(12, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(13, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(14, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(15, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(16, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(17, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(18, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(19, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(20, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(21, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(22).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(22, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(23).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    End If
                Next
            End With
            Session("CountArrayEq") = f.ToString

            Dim dt As New DataTable
            Dim _dr As DataRow
            dt.Columns.Add("COD_ARTICULO")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("CANT")
            dt.Columns.Add("SERIE_NRO")
            dt.Columns.Add("PLACA_NRO")
            dt.Columns.Add("TipoBien")
            dt.Columns.Add("TIPO_UBICACION")
            dt.Columns.Add("COD_ALMACEN")
            dt.Columns.Add("ALMACEN_NOMBRE")
            dt.Columns.Add("SERIE_FECHA_ADQ")
            dt.Columns.Add("SERIE_SKU")
            dt.Columns.Add("SERIE_ORDEN_COMPRA")
            dt.Columns.Add("SERIE_GUIA")
            dt.Columns.Add("ARTICULO_REFERENCIA")
            dt.Columns.Add("Ubicact_tipo")
            dt.Columns.Add("Ubicact_codigo")
            dt.Columns.Add("serie_numerar")
            dt.Columns.Add("art_tipo")
            dt.Columns.Add("CS")
            dt.Columns.Add("Fecha_Servicio")
            dt.Columns.Add("Centro_Costo")
            For i = 0 To f
                _dr = dt.NewRow()
                If arrSelec(1, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(1, i)) AndAlso arrSelec(1, i).Trim() <> "" Then
                    _dr("COD_ARTICULO") = arrSelec(1, i).Trim
                End If
                If arrSelec(2, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(2, i)) AndAlso arrSelec(2, i).Trim() <> "" Then
                    _dr("ART_CODEQUIVA") = arrSelec(2, i).Trim
                End If
                If arrSelec(3, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(3, i)) AndAlso arrSelec(3, i).Trim() <> "" Then
                    _dr("ART_DESCRIPCION") = arrSelec(3, i).Trim
                End If
                If arrSelec(4, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(4, i)) AndAlso arrSelec(4, i).Trim() <> "" Then
                    _dr("CANT") = arrSelec(4, i).Trim
                End If
                If arrSelec(5, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(5, i)) AndAlso arrSelec(5, i).Trim() <> "" Then
                    _dr("SERIE_NRO") = arrSelec(5, i).Trim
                End If
                If arrSelec(6, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(6, i)) AndAlso arrSelec(6, i).Trim() <> "" Then
                    _dr("PLACA_NRO") = arrSelec(6, i).Trim
                End If
                If arrSelec(7, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(7, i)) AndAlso arrSelec(7, i).Trim() <> "" Then
                    _dr("TipoBien") = arrSelec(7, i).Trim
                End If
                If arrSelec(8, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(8, i)) AndAlso arrSelec(8, i).Trim() <> "" Then
                    _dr("TIPO_UBICACION") = arrSelec(8, i).Trim
                End If
                If arrSelec(9, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(9, i)) AndAlso arrSelec(9, i).Trim() <> "" Then
                    _dr("COD_ALMACEN") = arrSelec(9, i).Trim
                End If
                If arrSelec(10, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(10, i)) AndAlso arrSelec(10, i).Trim() <> "" Then
                    _dr("ALMACEN_NOMBRE") = arrSelec(10, i).Trim
                End If
                If arrSelec(11, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(11, i)) AndAlso arrSelec(11, i).Trim() <> "" Then
                    _dr("SERIE_FECHA_ADQ") = arrSelec(11, i).Trim
                End If
                If arrSelec(12, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(12, i)) AndAlso arrSelec(12, i).Trim() <> "" Then
                    _dr("SERIE_SKU") = arrSelec(12, i).Trim
                End If
                If arrSelec(13, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(13, i)) AndAlso arrSelec(13, i).Trim() <> "" Then
                    _dr("SERIE_ORDEN_COMPRA") = arrSelec(13, i).Trim
                End If
                If arrSelec(14, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(14, i)) AndAlso arrSelec(14, i).Trim() <> "" Then
                    _dr("SERIE_GUIA") = arrSelec(14, i).Trim
                End If
                If arrSelec(15, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(15, i)) AndAlso arrSelec(15, i).Trim() <> "" Then
                    _dr("ARTICULO_REFERENCIA") = arrSelec(15, i).Trim
                End If
                If arrSelec(16, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(16, i)) AndAlso arrSelec(16, i).Trim() <> "" Then
                    _dr("Ubicact_tipo") = arrSelec(16, i).Trim
                End If
                If arrSelec(17, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(17, i)) AndAlso arrSelec(17, i).Trim() <> "" Then
                    _dr("Ubicact_codigo") = arrSelec(17, i).Trim
                End If
                If arrSelec(18, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(18, i)) AndAlso arrSelec(18, i).Trim() <> "" Then
                    _dr("serie_numerar") = arrSelec(18, i).Trim
                End If
                If arrSelec(19, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(19, i)) AndAlso arrSelec(19, i).Trim() <> "" Then
                    _dr("art_tipo") = arrSelec(19, i).Trim
                End If
                If arrSelec(20, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(20, i)) AndAlso arrSelec(20, i).Trim() <> "" Then
                    _dr("CS") = arrSelec(20, i).Trim
                End If
                If arrSelec(21, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(21, i)) AndAlso arrSelec(21, i).Trim() <> "" Then
                    _dr("Fecha_Servicio") = arrSelec(21, i).Trim
                End If
                If arrSelec(22, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(22, i)) AndAlso arrSelec(22, i).Trim() <> "" Then
                    _dr("Centro_Costo") = arrSelec(22, i).Trim
                End If
                dt.Rows.Add(_dr)
            Next
            Session("ArrayEq") = arrSelec
            GvListaNoEncontrados.DataSource = New DataView(dt)
            GvListaNoEncontrados.DataBind()

            If GvListaNoEncontrados.Rows.Count > 1 Then
                LblRegistroNE.Text = "Hay " & GvListaNoEncontrados.Rows.Count & " registros."
            ElseIf GvListaNoEncontrados.Rows.Count = 1 Then
                LblRegistroNE.Text = "Hay 1 registro."
            ElseIf GvListaNoEncontrados.Rows.Count = 0 Then
                LblRegistroNE.Text = ""
            End If

        End If
        If e.CommandName = "IngArt" Then
            Dim psNroPlaca As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaNoEncontrados.Rows(index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Dim psNroSerie As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaNoEncontrados.Rows(index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Dim psSku As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaNoEncontrados.Rows(index).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Session("NuevaSerie") = psNroSerie
            Session("NuevaPlaca") = psNroPlaca
            Limpiar_Cajas_Buscar_Articulos()
            TxtSku.Value = psSku
            Session("Viene") = "Grilla"
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
        End If
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
    End Sub
    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Dim obj As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim objListaInv As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim psListaArt As String = "1"
        Dim psListaMarca As String = "1"
        Dim psListaModelo As String = "1"
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        If TxtCodArticuloBA.Value <> "" Then
            pdCodArt = Nz(TxtCodArticuloBA.Value.ToString)
        End If
        Dim clasificacion As String = ""
        Dim psDescripcion As String = TxtDescripcionBA.Value.ToString
        Dim tipo As String = DdlTipoBA.SelectedValue.ToString
        Dim numPart As String = TxtNumParteBA.Value.ToString
        Dim especifico As String = TxtCodEspecificoBA.Value.ToString
        Dim psSku As String = ""
        Dim marca As Double = 0
        Dim modelo As Double = 0
        Dim pdCodUbicacion As Double = 0

        If marca <> 0 Then psListaMarca = ""
        If modelo <> 0 Then psListaModelo = ""
        If pdCodArt <> 0 Then psListaArt = ""
        If tipo = "< Seleccionar >" Then tipo = ""

        Dim psCodArtSku As String = ""

        If TxtSku.Value <> "" Then
            psSku = TxtSku.Value
        End If

        Dim drT As DataRow
        Dim dtColum As New DataTable
        Try

            dtColum.Columns.Add("ART_CODIGO")
            dtColum.Columns.Add("ART_CODEQUIVA")
            dtColum.Columns.Add("ART_DESCRIPCION")
            dtColum.Columns.Add("TIPO_ART")
            dtColum.Columns.Add("ART_TIPO")
            dtColum.Columns.Add("ART_SKU")

            If psSku <> "" Then

                Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim CmdGlobal2 As New SqlCommand
                Cn.Open() : CmdGlobal.Connection = Cn
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

            dt = obj.Lista_ArticuloxBusqueda(psconexion, pdCodArt, clasificacion, psDescripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
            If dt.Rows.Count > 0 Then
                For Each drDato As DataRow In dt.Rows
                    drT = dtColum.NewRow()
                    drT("ART_CODIGO") = Nu(drDato("ART_CODIGO"))
                    drT("ART_CODEQUIVA") = Nu(drDato("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drDato("ART_DESCRIPCION"))
                    drT("TIPO_ART") = Nu(drDato("TIPO_ART"))
                    drT("ART_TIPO") = Nu(drDato("ART_TIPO"))
                    drT("ART_SKU") = Nu(drDato("ART_SKU"))
                    dtColum.Rows.Add(drT)
                Next
            End If

            GvBuscarArticulos.DataSource = dtColum
            GvBuscarArticulos.DataBind()
            If dtColum.Rows.Count > 1 Then
                LblCantArtReg.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dtColum.Rows.Count = 1 Then
                LblCantArtReg.Text = "Hay 1 registro."
            ElseIf dtColum.Rows.Count = 0 Then
                LblCantArtReg.Text = "No hay registro."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub GvBuscarArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim arrSelec(,) As String
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim psNroPlaca As String = ""
        Dim pdNroSerie As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim RsRecep As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim psValorSys As String = ""
        psValorSys = Session("User") & FechaActual() & HoraActual()
        Dim psSerieNumerar As Double = 0
        Dim psRegularizar As String = ""
        Dim psNroReg As Double = 0


        Dim dtC As New DataTable
        Dim drTC As DataRow
        dtC.Columns.Add("COD_ARTICULO")
        dtC.Columns.Add("ART_CODEQUIVA")
        dtC.Columns.Add("ART_DESCRIPCION")
        dtC.Columns.Add("CANT")
        dtC.Columns.Add("TipoBien")
        dtC.Columns.Add("TIPO_UBICACION")
        dtC.Columns.Add("COD_ALMACEN")
        dtC.Columns.Add("ALMACEN_NOMBRE")
        dtC.Columns.Add("SERIE_FECHA_ADQ")
        dtC.Columns.Add("SERIE_SKU")
        dtC.Columns.Add("SERIE_ORDEN_COMPRA")
        dtC.Columns.Add("SERIE_GUIA")
        dtC.Columns.Add("ARTICULO_REFERENCIA")
        dtC.Columns.Add("Ubicact_tipo")
        dtC.Columns.Add("Ubicact_codigo")
        dtC.Columns.Add("serie_numerar")
        dtC.Columns.Add("art_tipo")
        dtC.Columns.Add("CS")
        dtC.Columns.Add("Fecha_Servicio")
        dtC.Columns.Add("Centro_Costo")

        If e.CommandName = "Aceptar" Then
            If Session("Viene") = "Cantidad" Then
                For Each row As GridViewRow In GvListaCantidades.Rows
                    drTC = dtC.NewRow()
                    drTC("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("CANT") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_FECHA_ADQ") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_ORDEN_COMPRA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_GUIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ARTICULO_REFERENCIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("Ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("serie_numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("art_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("CS") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("Fecha_Servicio") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("Centro_Costo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    dtC.Rows.Add(drTC)
                Next

                drTC = dtC.NewRow()
                drTC("COD_ARTICULO") = GvBuscarArticulos.Rows(Index).Cells(1).Text
                drTC("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drTC("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drTC("CANT") = Nz(TxtCantIng.Text)
                drTC("SERIE_SKU") = ""
                drTC("SERIE_ORDEN_COMPRA") = txtNroOC.Text
                If txtSerieDoc.Text = "" Then
                    drTC("SERIE_GUIA") = txtNroDoc.Text
                Else
                    drTC("SERIE_GUIA") = txtSerieDoc.Text & "-" & txtNroDoc.Text
                End If
                drTC("ARTICULO_REFERENCIA") = ""
                drTC("Fecha_Servicio") = txtFechaRecep.Text
                drTC("art_tipo") = GvBuscarArticulos.Rows(Index).Cells(5).Text
                drTC("CS") = "C"
                dtC.Rows.Add(drTC)

                CmdGlobal.CommandText = " SELECT max(CORRELATIVO) FROM V_TBINV_LISTASERIE_" & Session("User") & " "
                RsRecep = CmdGlobal.ExecuteReader
                If RsRecep.HasRows Then
                    While RsRecep.Read
                        psNroReg = Nz(RsRecep(0)) + 1
                    End While
                Else
                    psNroReg = 1
                End If

                RsRecep.Close()
                CmdGlobal.CommandText = " SELECT * FROM V_TBINV_LISTASERIE_" & Session("User") & " WHERE ART_CODIGO  = " & GvBuscarArticulos.Rows(Index).Cells(1).Text
                RsRecep = CmdGlobal.ExecuteReader
                If RsRecep.HasRows Then
                Else
                    CmdGlobal2.CommandText = " INSERT INTO V_TBINV_LISTASERIE_" & Session("User") & " (CORRELATIVO,ART_CODIGO,   BIEN_NUEVO, CANTIDAD) VALUES (" & psNroReg & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & ", 'C'," & Nz(TxtCantIng.Text) & ") "
                    CmdGlobal2.ExecuteNonQuery()

                End If
                RsRecep.Close()

                GvListaCantidades.DataSource = dtC
                GvListaCantidades.DataBind()

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
            ElseIf Session("Viene") = "CantidadGrilla" Then

                For Each row As GridViewRow In GvListaCantidades.Rows
                    drTC = dtC.NewRow()
                    drTC("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("CANT") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_FECHA_ADQ") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_ORDEN_COMPRA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("SERIE_GUIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ARTICULO_REFERENCIA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("Ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("serie_numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("art_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("CS") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("Fecha_Servicio") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drTC("Centro_Costo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")

                    If drTC("SERIE_SKU") = TxtSku.Value Then
                        drTC("COD_ARTICULO") = GvBuscarArticulos.Rows(Index).Cells(1).Text
                        drTC("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        drTC("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        drTC("art_tipo") = GvBuscarArticulos.Rows(Index).Cells(5).Text
                        drTC("CS") = "C"
                    End If
                    dtC.Rows.Add(drTC)
                Next
                
                GvListaCantidades.DataSource = dtC
                GvListaCantidades.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)

            Else
                f = -1
                Erase arrSelec
                Session("CountArrayEq") = "-1"
                With GvListaNoEncontrados
                    For i = 0 To .Rows.Count - 1
                        f = f + 1
                        ReDim Preserve arrSelec(23, f)
                        pdNroSerie = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        psNroPlaca = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        If Session("Viene") = "Fuera" Then
                            CmdGlobal.CommandText = " SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa")
                            RsRecep = CmdGlobal.ExecuteReader
                            If RsRecep.HasRows Then
                                While RsRecep.Read
                                    psSerieNumerar = Nz(RsRecep(0)) + 1
                                End While
                            Else
                                psSerieNumerar = 1
                            End If
                            RsRecep.Close()

                            If pdNroSerie = "" And psNroPlaca = "" Then psRegularizar = "A"
                            If psNroPlaca = "" Then psRegularizar = "P"
                            If pdNroSerie = "" Then psRegularizar = "S"
                            If psNroPlaca = "" Then
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "(SERIE_NUMERAR, ARTICULO_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO, SERIE_REGULARIZAR, SERIE_NRO, SERIE_ESTADO) VALUES(" & psSerieNumerar & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & ",'N','" & psValorSys & "','0','S','1', '" & psRegularizar & "', '" & pdNroSerie & "', '0')"
                                CmdGlobal.ExecuteNonQuery()
                            Else
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "(SERIE_NUMERAR, ARTICULO_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO, SERIE_REGULARIZAR, SERIE_NRO,PLACA_NRO, SERIE_ESTADO) VALUES(" & psSerieNumerar & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & ",'N','" & psValorSys & "','0','S','1', '" & psRegularizar & "', '" & pdNroSerie & "', " & psNroPlaca & ", '0')"
                                CmdGlobal.ExecuteNonQuery()
                            End If
                            CmdGlobal.CommandText = " SELECT max(CORRELATIVO) FROM V_TBINV_LISTASERIE_" & Session("User") & " "
                            RsRecep = CmdGlobal.ExecuteReader
                            If RsRecep.HasRows Then
                                While RsRecep.Read
                                    psNroReg = Nz(RsRecep(0)) + 1
                                End While
                            Else
                                psNroReg = 1
                            End If
                            RsRecep.Close()
                            CmdGlobal.CommandText = " SELECT * FROM V_TBINV_LISTASERIE_" & Session("User") & " WHERE SERIE_NUMERAR = " & psSerieNumerar
                            RsRecep = CmdGlobal.ExecuteReader
                            If RsRecep.HasRows Then
                            Else
                                If psNroPlaca = "" Then
                                    CmdGlobal2.CommandText = " INSERT INTO V_TBINV_LISTASERIE_" & Session("User") & " (CORRELATIVO,ART_CODIGO, SERIE_NUMERAR, SERIE_NRO,  BIEN_NUEVO, CANTIDAD) VALUES (" & psNroReg & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & "," & psSerieNumerar & ", '" & pdNroSerie & "', 'S',1) "
                                    CmdGlobal2.ExecuteNonQuery()
                                Else
                                    CmdGlobal2.CommandText = " INSERT INTO V_TBINV_LISTASERIE_" & Session("User") & " (CORRELATIVO,ART_CODIGO, SERIE_NUMERAR, SERIE_NRO, PLACA_NRO , BIEN_NUEVO, CANTIDAD) VALUES (" & psNroReg & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & "," & psSerieNumerar & ", '" & pdNroSerie & "', " & psNroPlaca & ", 'S',1) "
                                    CmdGlobal2.ExecuteNonQuery()
                                End If
                            End If
                            RsRecep.Close()
                            GvListaNoEncontrados.Rows(i).Cells(2).Text = GvBuscarArticulos.Rows(Index).Cells(1).Text
                            GvListaNoEncontrados.Rows(i).Cells(3).Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                            GvListaNoEncontrados.Rows(i).Cells(4).Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                            GvListaNoEncontrados.Rows(i).Cells(19).Text = psSerieNumerar
                            GvListaNoEncontrados.Rows(i).Cells(20).Text = GvBuscarArticulos.Rows(Index).Cells(5).Text
                            GvListaNoEncontrados.Rows(i).Cells(21).Text = "S"


                        Else
                            If pdNroSerie = Session("NuevaSerie") And psNroPlaca = Session("NuevaPlaca") Then
                                CmdGlobal.CommandText = " SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa")
                                RsRecep = CmdGlobal.ExecuteReader
                                If RsRecep.HasRows Then
                                    While RsRecep.Read
                                        psSerieNumerar = Nz(RsRecep(0)) + 1
                                    End While
                                Else
                                    psSerieNumerar = 1
                                End If
                                RsRecep.Close()

                                If pdNroSerie = "" And psNroPlaca = "" Then psRegularizar = "A"
                                If psNroPlaca = "" Then psRegularizar = "P"
                                If pdNroSerie = "" Then psRegularizar = "S"
                                If psNroPlaca = "" Then
                                    CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "(SERIE_NUMERAR, ARTICULO_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO, SERIE_REGULARIZAR, SERIE_NRO, SERIE_ESTADO) VALUES(" & psSerieNumerar & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & ",'N','" & psValorSys & "','0','S','1', '" & psRegularizar & "', '" & pdNroSerie & "', '0')"
                                    CmdGlobal.ExecuteNonQuery()
                                Else
                                    CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "(SERIE_NUMERAR, ARTICULO_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO, SERIE_REGULARIZAR, SERIE_NRO,PLACA_NRO, SERIE_ESTADO) VALUES(" & psSerieNumerar & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & ",'N','" & psValorSys & "','0','S','1', '" & psRegularizar & "', '" & pdNroSerie & "', " & psNroPlaca & ", '0')"
                                    CmdGlobal.ExecuteNonQuery()
                                End If
                                CmdGlobal.CommandText = " SELECT max(CORRELATIVO) FROM V_TBINV_LISTASERIE_" & Session("User") & " "
                                RsRecep = CmdGlobal.ExecuteReader
                                If RsRecep.HasRows Then
                                    While RsRecep.Read
                                        psNroReg = Nz(RsRecep(0)) + 1
                                    End While
                                Else
                                    psNroReg = 1
                                End If
                                RsRecep.Close()
                                CmdGlobal.CommandText = " SELECT * FROM V_TBINV_LISTASERIE_" & Session("User") & " WHERE SERIE_NUMERAR = " & psSerieNumerar
                                RsRecep = CmdGlobal.ExecuteReader
                                If RsRecep.HasRows Then
                                Else
                                    If psNroPlaca = "" Then
                                        CmdGlobal2.CommandText = " INSERT INTO V_TBINV_LISTASERIE_" & Session("User") & " (CORRELATIVO,ART_CODIGO, SERIE_NUMERAR, SERIE_NRO,  BIEN_NUEVO, CANTIDAD) VALUES (" & psNroReg & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & "," & psSerieNumerar & ", '" & pdNroSerie & "', 'S',1) "
                                        CmdGlobal2.ExecuteNonQuery()
                                    Else
                                        CmdGlobal2.CommandText = " INSERT INTO V_TBINV_LISTASERIE_" & Session("User") & " (CORRELATIVO,ART_CODIGO, SERIE_NUMERAR, SERIE_NRO, PLACA_NRO , BIEN_NUEVO, CANTIDAD) VALUES (" & psNroReg & "," & GvBuscarArticulos.Rows(Index).Cells(1).Text & "," & psSerieNumerar & ", '" & pdNroSerie & "', " & psNroPlaca & ", 'S',1) "
                                        CmdGlobal2.ExecuteNonQuery()
                                    End If
                                End If
                                RsRecep.Close()
                                GvListaNoEncontrados.Rows(i).Cells(2).Text = GvBuscarArticulos.Rows(Index).Cells(1).Text
                                GvListaNoEncontrados.Rows(i).Cells(3).Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                                GvListaNoEncontrados.Rows(i).Cells(4).Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                                GvListaNoEncontrados.Rows(i).Cells(19).Text = psSerieNumerar
                                GvListaNoEncontrados.Rows(i).Cells(20).Text = GvBuscarArticulos.Rows(Index).Cells(5).Text
                                GvListaNoEncontrados.Rows(i).Cells(21).Text = "S"
                            End If

                        End If

                        arrSelec(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(5, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(6, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(10, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(11, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(12, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(13, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(14, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(15, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(16, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(17, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(18, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(19, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(20, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(21, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(22).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(22, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(23).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")

                    Next
                End With
                Session("CountArrayEq") = f.ToString

                Dim dt As New DataTable
                Dim _dr As DataRow
                dt.Columns.Add("COD_ARTICULO")
                dt.Columns.Add("ART_CODEQUIVA")
                dt.Columns.Add("ART_DESCRIPCION")
                dt.Columns.Add("CANT")
                dt.Columns.Add("SERIE_NRO")
                dt.Columns.Add("PLACA_NRO")
                dt.Columns.Add("TipoBien")
                dt.Columns.Add("TIPO_UBICACION")
                dt.Columns.Add("COD_ALMACEN")
                dt.Columns.Add("ALMACEN_NOMBRE")
                dt.Columns.Add("SERIE_FECHA_ADQ")
                dt.Columns.Add("SERIE_SKU")
                dt.Columns.Add("SERIE_ORDEN_COMPRA")
                dt.Columns.Add("SERIE_GUIA")
                dt.Columns.Add("ARTICULO_REFERENCIA")
                dt.Columns.Add("Ubicact_tipo")
                dt.Columns.Add("Ubicact_codigo")
                dt.Columns.Add("serie_numerar")
                dt.Columns.Add("art_tipo")
                dt.Columns.Add("CS")
                dt.Columns.Add("Fecha_Servicio")
                dt.Columns.Add("Centro_Costo")
                For i = 0 To f
                    _dr = dt.NewRow()
                    If arrSelec(1, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(1, i)) AndAlso arrSelec(1, i).Trim() <> "" Then
                        _dr("COD_ARTICULO") = arrSelec(1, i).Trim
                    End If
                    If arrSelec(2, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(2, i)) AndAlso arrSelec(2, i).Trim() <> "" Then
                        _dr("ART_CODEQUIVA") = arrSelec(2, i).Trim
                    End If
                    If arrSelec(3, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(3, i)) AndAlso arrSelec(3, i).Trim() <> "" Then
                        _dr("ART_DESCRIPCION") = arrSelec(3, i).Trim
                    End If
                    If arrSelec(4, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(4, i)) AndAlso arrSelec(4, i).Trim() <> "" Then
                        _dr("CANT") = arrSelec(4, i).Trim
                    End If
                    If arrSelec(5, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(5, i)) AndAlso arrSelec(5, i).Trim() <> "" Then
                        _dr("SERIE_NRO") = arrSelec(5, i).Trim
                    End If
                    If arrSelec(6, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(6, i)) AndAlso arrSelec(6, i).Trim() <> "" Then
                        _dr("PLACA_NRO") = arrSelec(6, i).Trim
                    End If
                    If arrSelec(7, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(7, i)) AndAlso arrSelec(7, i).Trim() <> "" Then
                        _dr("TipoBien") = arrSelec(7, i).Trim
                    End If
                    If arrSelec(8, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(8, i)) AndAlso arrSelec(8, i).Trim() <> "" Then
                        _dr("TIPO_UBICACION") = arrSelec(8, i).Trim
                    End If
                    If arrSelec(9, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(9, i)) AndAlso arrSelec(9, i).Trim() <> "" Then
                        _dr("COD_ALMACEN") = arrSelec(9, i).Trim
                    End If
                    If arrSelec(10, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(10, i)) AndAlso arrSelec(10, i).Trim() <> "" Then
                        _dr("ALMACEN_NOMBRE") = arrSelec(10, i).Trim
                    End If
                    If arrSelec(11, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(11, i)) AndAlso arrSelec(11, i).Trim() <> "" Then
                        _dr("SERIE_FECHA_ADQ") = arrSelec(11, i).Trim
                    End If
                    If arrSelec(12, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(12, i)) AndAlso arrSelec(12, i).Trim() <> "" Then
                        _dr("SERIE_SKU") = arrSelec(12, i).Trim
                    End If
                    If arrSelec(13, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(13, i)) AndAlso arrSelec(13, i).Trim() <> "" Then
                        _dr("SERIE_ORDEN_COMPRA") = arrSelec(13, i).Trim
                    End If
                    If arrSelec(14, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(14, i)) AndAlso arrSelec(14, i).Trim() <> "" Then
                        _dr("SERIE_GUIA") = arrSelec(14, i).Trim
                    End If
                    If arrSelec(15, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(15, i)) AndAlso arrSelec(15, i).Trim() <> "" Then
                        _dr("ARTICULO_REFERENCIA") = arrSelec(15, i).Trim
                    End If
                    If arrSelec(16, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(16, i)) AndAlso arrSelec(16, i).Trim() <> "" Then
                        _dr("Ubicact_tipo") = arrSelec(16, i).Trim
                    End If
                    If arrSelec(17, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(17, i)) AndAlso arrSelec(17, i).Trim() <> "" Then
                        _dr("Ubicact_codigo") = arrSelec(17, i).Trim
                    End If
                    If arrSelec(18, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(18, i)) AndAlso arrSelec(18, i).Trim() <> "" Then
                        _dr("serie_numerar") = arrSelec(18, i).Trim
                    End If
                    If arrSelec(19, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(19, i)) AndAlso arrSelec(19, i).Trim() <> "" Then
                        _dr("art_tipo") = arrSelec(19, i).Trim
                    End If
                    If arrSelec(20, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(20, i)) AndAlso arrSelec(20, i).Trim() <> "" Then
                        _dr("CS") = arrSelec(20, i).Trim
                    End If
                    If arrSelec(21, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(21, i)) AndAlso arrSelec(21, i).Trim() <> "" Then
                        _dr("Fecha_Servicio") = arrSelec(21, i).Trim
                    End If
                    If arrSelec(22, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(22, i)) AndAlso arrSelec(22, i).Trim() <> "" Then
                        _dr("Centro_Costo") = arrSelec(22, i).Trim
                    End If
                    dt.Rows.Add(_dr)
                Next
                Session("ArrayEq") = arrSelec
                GvListaNoEncontrados.DataSource = New DataView(dt)
                GvListaNoEncontrados.DataBind()

                If GvListaNoEncontrados.Rows.Count > 1 Then
                    LblRegistroNE.Text = "Hay " & GvListaNoEncontrados.Rows.Count & " registros."
                ElseIf GvListaNoEncontrados.Rows.Count = 1 Then
                    LblRegistroNE.Text = "Hay 1 registro."
                ElseIf GvListaNoEncontrados.Rows.Count = 0 Then
                    LblRegistroNE.Text = ""
                End If
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)


            End If

        End If
        Limpiar_Cajas_Buscar_Articulos()
    End Sub
    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        LblCantArtReg.Text = ""
        TxtDescripcionBA.Value = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtClasificacionBA.Value = ""
        lblCodClas.Text = ""
        LblCodClasificacionBA.Text = ""
        TxtSku.Value = ""
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
    End Sub

    Private Sub BtnCargaExcel_Click(sender As Object, e As EventArgs) Handles BtnCargaExcel.Click
        Session("TipoCarga") = ""
        Try

            Dim filaIni As Long = 0
            Dim filafin As Long = 0
            Dim colCCPlaca As Long = 0
            Dim colCCSerie As Long = 0
            Dim colCCSku As Long = 0
            Dim colCCRef As Long = 0
            Dim colCCOc As Long = 0
            Dim colCCGuia As Long = 0
            Dim colCCCant As Long = 0
            Dim colCCFecha As Long = 0
            Dim colCCentroC As Long = 0
            colCCPlaca = Nz(TxtcolPlaca.Text)
            colCCSerie = Nz(TxtColSerie.Text)
            colCCSku = Nz(TxtColSku.Text)
            colCCRef = Nz(TxtColRef.Text)
            colCCOc = Nz(TxtColOC.Text)
            colCCGuia = Nz(TxtColguia.Text)
            colCCCant = Nz(TxtColCant.Text)
            filaIni = Nz(TxtIni.Text)
            filafin = Nz(Txtfin.Text)
            colCCFecha = Nz(TxtColFecha.Text)
            colCCentroC = Nz(TxtColCC.Text)

            If colCCPlaca = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna de la placa');", True)
            ElseIf colCCSerie = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna de la serie');", True)
            ElseIf colCCSku = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna del SKU');", True)
            ElseIf colCCRef = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna de la referencia');", True)
            ElseIf colCCOc = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna de la orden de compra');", True)
            ElseIf colCCGuia = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna de la guia');", True)
            ElseIf colCCCant = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna de la cantidad');", True)
            ElseIf colCCFecha = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna de la Fecha de Recepción y/0 Despacho');", True)
            ElseIf colCCentroC = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la columna del Centro de Costo');", True)
            ElseIf filaIni = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la fila donde comienza el excel');", True)
            ElseIf filafin = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar la fila donde termina el excel');", True)
            Else
                If FileUpload1.HasFile AndAlso FileUpload1.FileName.EndsWith(".xlsx") Then
                    Dim archivo As HttpPostedFile = FileUpload1.PostedFile
                    Dim rutaArchivo As String = Server.MapPath("~/Inventario/GuiaRemision/CargaMasiva/" + FileUpload1.FileName)
                    Dim psValoresExcel As String = ""

                    Dim carpeta As String = Server.MapPath("~/Inventario/GuiaRemision/CargaMasiva/")

                    ' Verificar si la carpeta existe
                    If Not Directory.Exists(carpeta) Then
                        ' Crear la carpeta
                        Directory.CreateDirectory(carpeta)
                    End If

                    archivo.SaveAs(rutaArchivo)
                    Dim valor As String = String.Empty
                    Dim valorSerie As String = String.Empty
                    Dim valorCant As String = String.Empty
                    Dim valorReferencia As String = String.Empty
                    Dim valorSKU As String = String.Empty
                    Dim valorOC As String = String.Empty
                    Dim valorGuia As String = String.Empty
                    Dim valorCC As String = String.Empty
                    Dim valorFecha As String = String.Empty

                    ' Leer el archivo Excel
                    Using package As New ExcelPackage(New FileInfo(rutaArchivo))
                        Dim workbook As ExcelWorkbook = package.Workbook
                        If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                            Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)

                            ' Recorrer las celdas del archivo Excel
                            For row As Integer = filaIni To filafin
                                If worksheet.Cells(row, colCCPlaca).Value IsNot Nothing And worksheet.Cells(row, colCCSerie).Value IsNot Nothing Then

                                    Dim celda As ExcelRange = worksheet.Cells(row, colCCPlaca)
                                    Dim celdaSerie As ExcelRange = worksheet.Cells(row, colCCSerie)
                                    Dim celdaRef As ExcelRange = worksheet.Cells(row, colCCRef)
                                    Dim celdaSKU As ExcelRange = worksheet.Cells(row, colCCSku)
                                    Dim celdaOC As ExcelRange = worksheet.Cells(row, colCCOc)
                                    Dim celdaGuia As ExcelRange = worksheet.Cells(row, colCCGuia)
                                    Dim celdaCC As ExcelRange = worksheet.Cells(row, colCCentroC)
                                    Dim celdaFecha As ExcelRange = worksheet.Cells(row, colCCFecha)

                                    If celda.Value IsNot Nothing Then
                                        valor = celda.Value.ToString()
                                    End If

                                    If celdaSerie.Value IsNot Nothing Then
                                        valorSerie = QuitaComilla(celdaSerie.Value.ToString())
                                    End If

                                    If celdaRef.Value IsNot Nothing Then
                                        valorReferencia = QuitaComilla(celdaRef.Value.ToString())
                                    End If

                                    If celdaSKU.Value IsNot Nothing Then
                                        valorSKU = QuitaComilla(celdaSKU.Value.ToString())
                                    End If

                                    If celdaOC.Value IsNot Nothing Then
                                        valorOC = QuitaComilla(celdaOC.Value.ToString())
                                    End If

                                    If celdaGuia.Value IsNot Nothing Then
                                        valorGuia = QuitaComilla(celdaGuia.Value.ToString())
                                    End If

                                    If celdaCC.Value IsNot Nothing Then
                                        valorCC = QuitaComilla(celdaCC.Value.ToString())
                                    End If

                                    If celdaFecha.Value IsNot Nothing Then
                                        valorFecha = QuitaComilla(Left(celdaFecha.Value.ToString(), 10))
                                    End If

                                    Call Recibir_Bien(valorSKU, valorOC, valorReferencia, valorGuia, valorSerie, CDbl(Val(valor)), 0, valorFecha, valorCC)

                                ElseIf worksheet.Cells(row, colCCPlaca).Value Is Nothing And worksheet.Cells(row, colCCSerie).Value IsNot Nothing Then

                                    Dim celdaSerie As ExcelRange = worksheet.Cells(row, colCCSerie)
                                    Dim celdaRef As ExcelRange = worksheet.Cells(row, colCCRef)
                                    Dim celdaSKU As ExcelRange = worksheet.Cells(row, colCCSku)
                                    Dim celdaOC As ExcelRange = worksheet.Cells(row, colCCOc)
                                    Dim celdaGuia As ExcelRange = worksheet.Cells(row, colCCGuia)
                                    Dim celdaCC As ExcelRange = worksheet.Cells(row, colCCentroC)
                                    Dim celdaFecha As ExcelRange = worksheet.Cells(row, colCCFecha)

                                    If celdaSerie.Value IsNot Nothing Then
                                        valorSerie = QuitaComilla(celdaSerie.Value.ToString())
                                    End If

                                    If celdaRef.Value IsNot Nothing Then
                                        valorReferencia = QuitaComilla(celdaRef.Value.ToString())
                                    End If

                                    If celdaSKU.Value IsNot Nothing Then
                                        valorSKU = QuitaComilla(celdaSKU.Value.ToString())
                                    End If

                                    If celdaOC.Value IsNot Nothing Then
                                        valorOC = QuitaComilla(celdaOC.Value.ToString())
                                    End If

                                    If celdaGuia.Value IsNot Nothing Then
                                        valorGuia = QuitaComilla(celdaGuia.Value.ToString())
                                    End If

                                    If celdaCC.Value IsNot Nothing Then
                                        valorCC = QuitaComilla(celdaCC.Value.ToString())
                                    End If

                                    If celdaFecha.Value IsNot Nothing Then
                                        valorFecha = QuitaComilla(Left(celdaFecha.Value.ToString(), 10))
                                    End If

                                    Call Recibir_Bien(valorSKU, valorOC, valorReferencia, valorGuia, valorSerie, 0, 0, valorFecha, valorCC)

                                ElseIf worksheet.Cells(row, colCCPlaca).Value Is Nothing And worksheet.Cells(row, colCCSerie).Value Is Nothing Then
                                    If worksheet.Cells(row, colCCCant).Value IsNot Nothing Then

                                        Dim celdaCant As ExcelRange = worksheet.Cells(row, colCCCant)
                                        Dim celdaRef As ExcelRange = worksheet.Cells(row, colCCRef)
                                        Dim celdaSKU As ExcelRange = worksheet.Cells(row, colCCSku)
                                        Dim celdaOC As ExcelRange = worksheet.Cells(row, colCCOc)
                                        Dim celdaGuia As ExcelRange = worksheet.Cells(row, colCCGuia)
                                        Dim celdaCC As ExcelRange = worksheet.Cells(row, colCCentroC)
                                        Dim celdaFecha As ExcelRange = worksheet.Cells(row, colCCFecha)

                                        If celdaCant.Value IsNot Nothing Then
                                            valorCant = celdaCant.Value.ToString()
                                        End If

                                        If celdaRef.Value IsNot Nothing Then
                                            valorReferencia = QuitaComilla(celdaRef.Value.ToString())
                                        End If

                                        If celdaSKU.Value IsNot Nothing Then
                                            valorSKU = QuitaComilla(celdaSKU.Value.ToString())
                                        End If

                                        If celdaOC.Value IsNot Nothing Then
                                            valorOC = QuitaComilla(celdaOC.Value.ToString())
                                        End If

                                        If celdaGuia.Value IsNot Nothing Then
                                            valorGuia = QuitaComilla(celdaGuia.Value.ToString())
                                        End If

                                        If celdaCC.Value IsNot Nothing Then
                                            valorCC = QuitaComilla(celdaCC.Value.ToString())
                                        End If

                                        If celdaFecha.Value IsNot Nothing Then
                                            valorFecha = QuitaComilla(Left(celdaFecha.Value.ToString(), 10))
                                        End If

                                        Call Recibir_Bien(valorSKU, valorOC, valorReferencia, valorGuia, "", 0, valorCant, valorFecha, valorCC)

                                    End If
                                End If
                            Next
                        End If

                    End Using
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar el archivo a cargar');", True)
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnBuscarArt_Click(sender As Object, e As EventArgs) Handles BtnBuscarArt.Click
        Session("Viene") = "Fuera"
        Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
    End Sub

    Private Sub btnBus_Click(sender As Object, e As EventArgs) Handles btnBus.Click
        lblEtq_BusDestino.Text = "Busqueda de Proveedores"
        txtProvCodigo.Text = ""
        txtProvRuc.Text = ""
        txtProvNombre.Text = ""
        FlexTipoPers.DataSource = Nothing
        FlexTipoPers.DataBind()
        txtRucTipoPers.Text = ""
        txtRazonSocialTipoPers.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusquedaProv').modal('show');", True)
    End Sub
    Protected Sub btnListaProveedor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListaProveedor.Click
        Try
            Dim psConexion As String = Session("Ruta_Emp")
            Dim obj As New clsInv_Listados
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            FlexTipoPers.DataSource = obj.Lista_Proveedor(psConexion, Session("CodEmpresa"), txtRucTipoPers.Text.Trim, txtRazonSocialTipoPers.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTipoPers.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtProvCodigo.Text = ""
            txtProvRuc.Text = ""
            txtProvNombre.Text = ""
            Session("DestinoCodExt") = FlexTipoPers.Rows(Index).Cells(1).Text
            Session("DestinoDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&amp;", "&")
            Session("DestinoCodigo") = FlexTipoPers.Rows(Index).Cells(3).Text
            txtProvNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Session("DestinoDescrip"), "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&amp;", "&")
            txtProvRuc.Text = Session("DestinoCodExt")
            txtProvCodigo.Text = Session("DestinoCodigo")
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusquedaProv').modal('hide');", True)
        End If
    End Sub
    Protected Sub btnCerrar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar2.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
    End Sub

    Private Sub BtnIngCant_Click(sender As Object, e As EventArgs) Handles BtnIngCant.Click
        Dim psDatosCompletos As String = ""
        If DdlServicio.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
        If DdlMotivo.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
        If DdlEstado.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
        If TxtDesCodigo.Text = "" Then psDatosCompletos = "No"
        If cboTipoDoc.SelectedValue = "< Seleccionar >" Then psDatosCompletos = "No"
        If txtNroDoc.Text = "" Then psDatosCompletos = "No"
        If txtNroOC.Text = "" Then psDatosCompletos = "No"
        If psDatosCompletos = "No" Then
            If DdlServicio.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona Despacho o Recepción.');", True)
            ElseIf DdlMotivo.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un motivo.');", True)
            ElseIf DdlEstado.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un estado.');", True)
            ElseIf TxtDesCodigo.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el destino.');", True)
            ElseIf cboTipoDoc.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un tipo de documento.');", True)
            ElseIf txtNroDoc.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar número de documento.');", True)
            ElseIf txtNroOC.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar número de orden de compra.');", True)
            End If
        Else
            Session("Viene") = "Cantidad"
            Limpiar_Cajas_Buscar_Articulos()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
        End If
    End Sub

    Private Sub GvListaCantidades_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaCantidades.RowCommand
        Dim arrSelec(,) As String
        Dim i As Integer = 0
        Dim f As Integer = 0
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Session("NuevaSerie") = ""
        Session("NuevaPlaca") = ""
        If e.CommandName = "QuitarFila" Then
            f = -1
            Erase arrSelec
            Session("CountArrayEq") = "-1"
            With GvListaCantidades
                For i = 0 To .Rows.Count - 1
                    If i <> index Then
                        f = f + 1
                        ReDim Preserve arrSelec(21, f)
                        arrSelec(1, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(2, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(3, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(4, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(5, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(6, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(7, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(8, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(9, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(10, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(11, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(12, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(13, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(14, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(15, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(16, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(17).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(17, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(18).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(18, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(19).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(19, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(20).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                        arrSelec(20, f) = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(i).Cells(21).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    End If
                Next
            End With
            Session("CountArrayEq") = f.ToString

            Dim dt As New DataTable
            Dim _dr As DataRow
            dt.Columns.Add("COD_ARTICULO")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("CANT")
            dt.Columns.Add("TipoBien")
            dt.Columns.Add("TIPO_UBICACION")
            dt.Columns.Add("COD_ALMACEN")
            dt.Columns.Add("ALMACEN_NOMBRE")
            dt.Columns.Add("SERIE_FECHA_ADQ")
            dt.Columns.Add("SERIE_SKU")
            dt.Columns.Add("SERIE_ORDEN_COMPRA")
            dt.Columns.Add("SERIE_GUIA")
            dt.Columns.Add("ARTICULO_REFERENCIA")
            dt.Columns.Add("Ubicact_tipo")
            dt.Columns.Add("Ubicact_codigo")
            dt.Columns.Add("serie_numerar")
            dt.Columns.Add("art_tipo")
            dt.Columns.Add("CS")
            dt.Columns.Add("Fecha_Servicio")
            dt.Columns.Add("Centro_Costo")
            For i = 0 To f
                _dr = dt.NewRow()
                If arrSelec(1, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(1, i)) AndAlso arrSelec(1, i).Trim() <> "" Then
                    _dr("COD_ARTICULO") = arrSelec(1, i).Trim
                End If
                If arrSelec(2, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(2, i)) AndAlso arrSelec(2, i).Trim() <> "" Then
                    _dr("ART_CODEQUIVA") = arrSelec(2, i).Trim
                End If
                If arrSelec(3, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(3, i)) AndAlso arrSelec(3, i).Trim() <> "" Then
                    _dr("ART_DESCRIPCION") = arrSelec(3, i).Trim
                End If
                If arrSelec(4, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(4, i)) AndAlso arrSelec(4, i).Trim() <> "" Then
                    _dr("CANT") = arrSelec(4, i).Trim
                End If
                If arrSelec(5, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(5, i)) AndAlso arrSelec(5, i).Trim() <> "" Then
                    _dr("TipoBien") = arrSelec(5, i).Trim
                End If
                If arrSelec(6, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(6, i)) AndAlso arrSelec(6, i).Trim() <> "" Then
                    _dr("TIPO_UBICACION") = arrSelec(6, i).Trim
                End If
                If arrSelec(7, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(7, i)) AndAlso arrSelec(7, i).Trim() <> "" Then
                    _dr("COD_ALMACEN") = arrSelec(7, i).Trim
                End If
                If arrSelec(8, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(8, i)) AndAlso arrSelec(8, i).Trim() <> "" Then
                    _dr("ALMACEN_NOMBRE") = arrSelec(8, i).Trim
                End If
                If arrSelec(9, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(9, i)) AndAlso arrSelec(9, i).Trim() <> "" Then
                    _dr("SERIE_FECHA_ADQ") = arrSelec(9, i).Trim
                End If
                If arrSelec(10, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(10, i)) AndAlso arrSelec(10, i).Trim() <> "" Then
                    _dr("SERIE_SKU") = arrSelec(10, i).Trim
                End If
                If arrSelec(11, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(11, i)) AndAlso arrSelec(11, i).Trim() <> "" Then
                    _dr("SERIE_ORDEN_COMPRA") = arrSelec(11, i).Trim
                End If
                If arrSelec(12, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(12, i)) AndAlso arrSelec(12, i).Trim() <> "" Then
                    _dr("SERIE_GUIA") = arrSelec(12, i).Trim
                End If
                If arrSelec(13, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(13, i)) AndAlso arrSelec(13, i).Trim() <> "" Then
                    _dr("ARTICULO_REFERENCIA") = arrSelec(13, i).Trim
                End If
                If arrSelec(14, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(14, i)) AndAlso arrSelec(14, i).Trim() <> "" Then
                    _dr("Ubicact_tipo") = arrSelec(14, i).Trim
                End If
                If arrSelec(15, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(15, i)) AndAlso arrSelec(15, i).Trim() <> "" Then
                    _dr("Ubicact_codigo") = arrSelec(15, i).Trim
                End If
                If arrSelec(16, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(16, i)) AndAlso arrSelec(16, i).Trim() <> "" Then
                    _dr("serie_numerar") = arrSelec(16, i).Trim
                End If
                If arrSelec(17, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(17, i)) AndAlso arrSelec(17, i).Trim() <> "" Then
                    _dr("art_tipo") = arrSelec(17, i).Trim
                End If
                If arrSelec(18, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(18, i)) AndAlso arrSelec(18, i).Trim() <> "" Then
                    _dr("CS") = arrSelec(18, i).Trim
                End If
                If arrSelec(19, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(19, i)) AndAlso arrSelec(19, i).Trim() <> "" Then
                    _dr("Fecha_Servicio") = arrSelec(19, i).Trim
                End If
                If arrSelec(20, i) IsNot Nothing AndAlso Not IsDBNull(arrSelec(20, i)) AndAlso arrSelec(20, i).Trim() <> "" Then
                    _dr("Centro_Costo") = arrSelec(20, i).Trim
                End If
                dt.Rows.Add(_dr)
            Next
            Session("ArrayEq") = arrSelec
            GvListaCantidades.DataSource = New DataView(dt)
            GvListaCantidades.DataBind()

            If GvListaCantidades.Rows.Count > 1 Then
                LblRegistroCant.Text = "Hay " & GvListaCantidades.Rows.Count & " registros."
            ElseIf GvListaCantidades.Rows.Count = 1 Then
                LblRegistroCant.Text = "Hay 1 registro."
            ElseIf GvListaCantidades.Rows.Count = 0 Then
                LblRegistroCant.Text = ""
            End If

        End If
        If e.CommandName = "IngArt" Then
            Limpiar_Cajas_Buscar_Articulos()
            Session("Viene") = "CantidadGrilla"
            TxtDescripcionBA.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCantidades.Rows(index).Cells(14).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtSku.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCantidades.Rows(index).Cells(11).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
        End If
    End Sub

    Private Sub BtnNuevoBA_Click(sender As Object, e As EventArgs) Handles BtnNuevoBA.Click
        Dim obj As New Cls_Catalogo
        Dim psCodClasif As Double = 0
        Dim pdCodArt As Double = 0
        Dim pdTipoArt As Double = 0
        Try
            If DdlTipoBA.SelectedValue <> "< Seleccionar >" Then
                pdTipoArt = Nz(DdlTipoBA.SelectedValue)
            End If
            pdCodArt = obj.Codigo(Session("Ruta_Emp"))
            If lblCodClas.Text <> "" Then psCodClasif = lblCodClas.Text
            Dim psArtDescripcion As String = ""
            If TxtDescripcionBA.Value <> "" Then psArtDescripcion = TxtDescripcionBA.Value
            If psArtDescripcion = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar descripción del bien.');", True)
            ElseIf psCodClasif = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Clasificación.');", True)
            ElseIf pdTipoArt = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Tipo.');", True)
            Else
                obj.RegistrarCatalogo(Session("Ruta_Emp"), pdCodArt, pdTipoArt, psCodClasif, 0, 0, 0, psArtDescripcion, Left(psArtDescripcion, 19), TxtNumParteBA.Value, "", 34, 0, "", 0, 0, 0, 0, 0, Session("User"), TxtSku.Value)
            End If

            BtnNuevoBA.Visible = True
            BtnBuscarBA_Click(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        TrvClasificacion.Nodes.Clear()
    End Sub
    Private Sub BtnBuscaClasificacion_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacion.Click
        PopularRootLevel()
    End Sub
    Private Sub TrvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles TrvClasificacion.TreeNodePopulate
        NodosHijos(CInt(e.Node.Value), e.Node)
    End Sub
    Private Sub NodosHijos(ByVal nodoPadreId As Integer, ByVal nodePadre As TreeNode)
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))

        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO, CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " _
                                      & " (SELECT count(clas_codigo) FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL2=c1.CLAS_CODIGO and clas_cod_nivel = 3 ) as CountHijos " _
                                      & " FROM TBINV_ARTICULO_CLASIFICACION c1 WHERE CLAS_NIVEL1=@parentID and clas_cod_nivel = 2 ORDER BY CLAS_NUMERACION", objConn)

        objComand.Parameters.Add("@parentID", SqlDbType.Int).Value = nodoPadreId
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()
        da.Fill(dt)

        NodosPopulares(dt, nodePadre.ChildNodes)
    End Sub
    Protected Sub TrvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TrvClasificacion.SelectedNodeChanged
        TrvClasificacion.SelectedNode.Selected = True
        TxtClasificacionBA.Value = TrvClasificacion.SelectedNode.Text
        Dim psNumero As Integer = 0
        lblCodClas.Text = TrvClasificacion.SelectedValue
        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        TrvClasificacion.Nodes.Clear()
    End Sub

    Private Sub PopularRootLevel()
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))

        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO, CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion,  " _
                                      & " (SELECT count(clas_codigo) frOM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL1=c1.CLAS_CODIGO and clas_cod_nivel = 2 ) as CountHijos " _
                                      & " FROM TBINV_ARTICULO_CLASIFICACION c1  WHERE CLAS_COD_NIVEL=1 ORDER BY CLAS_NUMERACION", objConn)
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()

        da.Fill(dt)
        NodosPopulares(dt, TrvClasificacion.Nodes)
    End Sub
    Private Sub NodosPopulares(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        nodes.Clear()
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode()
            tn.Text = dr("clasificacion").ToString()
            tn.Value = dr("CODIGO").ToString()
            nodes.Add(tn)
            tn.PopulateOnDemand = (CInt(dr("CountHijos")) > 0)
        Next
    End Sub

    Private Sub DdlServicio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlServicio.SelectedIndexChanged

        If DdlServicio.SelectedValue = "2" Then
            RbOrigAlmacen.Enabled = False
            RbOrigCC.Enabled = False
            BtnBusOrigen.Enabled = False
            TxtOriCodigo.Text = ""
            TxtOrigCodInt.Text = ""
            TxtOrigDescripcion.Text = ""
            RbOrigAlmacen.Checked = True
            RbOrigCC.Checked = False
            RBAlmacen.Checked = True
            RBCentroC.Checked = False
            divOrigen.Visible = False
            id_GuiaRecep.Visible = True
            id_Proveedor.Visible = True
            id_GuiaNumero.Visible = False
            TxtGuiaNumero.Text = ""
            TxtGuiaSerie.Text = ""
            txtNroOC.Text = ""
            txtProvCodigo.Text = ""
            txtProvNombre.Text = ""
            txtProvRuc.Text = ""
            txtSerieDoc.Text = ""
            txtNroDoc.Text = ""
            cboTipoDoc.SelectedValue = "< Seleccionar >"
            Session("TipoDestino") = "Almacen"
            Session("TipoOrigen") = "Almacen"
            Lbletiqueta2.Text = "Fecha Recepción"
        ElseIf DdlServicio.SelectedValue = "1" Then
            RbOrigAlmacen.Enabled = True
            RbOrigCC.Enabled = True
            BtnBusOrigen.Enabled = True
            TxtOriCodigo.Text = ""
            TxtOrigCodInt.Text = ""
            TxtOrigDescripcion.Text = ""
            TxtDesCodExterno.Text = ""
            TxtDesCodigo.Text = ""
            TxtDesDescrip.Text = ""
            RbOrigAlmacen.Checked = True
            RbOrigCC.Checked = False
            RBAlmacen.Checked = True
            RBCentroC.Checked = False
            divOrigen.Visible = True
            id_GuiaRecep.Visible = False
            id_Proveedor.Visible = False
            id_GuiaNumero.Visible = True
            TxtGuiaNumero.Text = ""
            TxtGuiaSerie.Text = ""
            txtNroOC.Text = ""
            txtProvCodigo.Text = ""
            txtProvNombre.Text = ""
            txtProvRuc.Text = ""
            txtSerieDoc.Text = ""
            txtNroDoc.Text = ""
            cboTipoDoc.SelectedValue = "< Seleccionar >"
            Session("TipoOrigen") = "Almacen"
            Session("TipoDestino") = "Almacen"
            Lbletiqueta2.Text = "Fecha Despacho"
        End If
    End Sub

    Private Sub RbOrigAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RbOrigAlmacen.CheckedChanged
        Call Carga_Motivos("S", IIf(RbOrigAlmacen.Checked = True, "1", IIf(RbOrigCC.Checked = True, "2", "")), IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        TxtOriCodigo.Text = ""
        TxtOrigCodInt.Text = ""
        TxtOrigDescripcion.Text = ""
        Session("TipoOrigen") = "Almacen"
        Session("OrigenDescrip") = ""
        Session("OrigenCodExt") = ""
    End Sub

    Private Sub RbOrigCC_CheckedChanged(sender As Object, e As EventArgs) Handles RbOrigCC.CheckedChanged
        Call Carga_Motivos("S", IIf(RbOrigAlmacen.Checked = True, "1", IIf(RbOrigCC.Checked = True, "2", "")), IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        TxtOriCodigo.Text = ""
        TxtOrigCodInt.Text = ""
        TxtOrigDescripcion.Text = ""
        Session("TipoOrigen") = "CentroCosto"
        Session("OrigenDescrip") = ""
        Session("OrigenCodExt") = ""
    End Sub

    Private Sub BtnBusOrigen_Click(sender As Object, e As EventArgs) Handles BtnBusOrigen.Click
        If Session("TipoOrigen") = "Almacen" Then
            lblEtq_BusDestino.Text = "Busqueda de Almacén"
        ElseIf Session("TipoOrigen") = "CentroCosto" Then
            lblEtq_BusDestino.Text = "Busqueda de Centro de Costos"
        End If
        Session("TipoBus") = "Origen"
        TxtOrigDescripcion.Text = ""
        TxtOrigCodInt.Text = ""
        TxtOriCodigo.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub
    Protected Sub btnProcesar_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Aquí iría el código para procesar lo que sea necesario
        System.Threading.Thread.Sleep(3000) ' Simula una espera de 3 segundos.
    End Sub
End Class
