Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Inventario_Inventario_SalidaIngreso_Alavez
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Dim CodSalida As String = ""
    Dim i As Long = 0
    Dim oFuncInv As New clsInv_Procesos
    Dim a As Long = i
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim dt As New DataTable
            lblerror.text = ""
            Dim psconexion As String = Session("Ruta_Emp")
            TxtFecha.Text = FormatoFecha(FechaActual)
            TxtHora.Text = FormatoHoraSeg(HoraActual(True))
            Call LLenar_TipoaArticulo()
            DdlTipoBA.SelectedValue = "< Seleccionar >"
            Session("Fin") = ""
            optOrigen.Checked = True : Session("TipoOrigen") = "1" : Lbl6.Text = "Origen : " : BtnBuscarOrigen.Enabled = True
            RbDestino.Checked = True : Session("TipoDestino") = "1" : lblDestino.Text = "Destino : " : BtnBuscaDetino.Enabled = True
            Call Carga_Motivos()
            DdlMotivo.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Private Sub LLenar_TipoaArticulo()
        Dim dt As New DataTable
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
        Dim psConexion As String = Session("Ruta_Emp")
        Dim Cn As New SqlConnection(psConexion)
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        DdlMotivo.Items.Clear()
        Try

            If optIngreso.Checked = True Then
                obj.Llena_Motivo_Ing(Session("Ruta_Emp"), Session("CodEmpresa"), DdlMotivo)
            Else
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = " SELECT DISTINCT MAINSA_MOTIVO_TRASLADO, (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC217' AND ELEMEN_CODIGO = MAINSA_MOTIVO_TRASLADO) AS MOTIVO_TRASLADO " _
                                   & " FROM TBINV_MATRIZ_INGRESOSALIDA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (MAINSA_TIPO_MOVIMIENTO = 'S') AND (MAINSA_UBICACION1 = '1') AND (MAINSA_UBICACION2 = '" & IIf(RbDestino.Checked = True, "1", IIf(RbDestino2.Checked = True, "2", IIf(RbDestino3.Checked = True, "3", IIf(RbDestino4.Checked = True, "6", IIf(RbDestino5.Checked = True, "5", ""))))) & "') ORDER BY MOTIVO_TRASLADO"
                Rs = cmdSql.ExecuteReader()
                DdlMotivo.DataSource = Rs
                DdlMotivo.DataTextField = "MOTIVO_TRASLADO"
                DdlMotivo.DataValueField = "MAINSA_MOTIVO_TRASLADO"
                DdlMotivo.DataBind()

                DdlMotivo.Items.Add("< Seleccionar >")
                DdlMotivo.SelectedValue = "< Seleccionar >"
            End If
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch Ex As Exception
            LblError.Text = Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub
    Private Sub BtnIngcant_Click(sender As Object, e As EventArgs) Handles BtnIngcant.Click
        If Val(TxtCantidad.Text) = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Primero debe ingresar la cantidad antes de elegir el producto.');", True)
        Else
            BtnLimpiar.Enabled = False
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
        End If
    End Sub
    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        LblCodClasificacionBA.Text = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        Dim dtArt As New DataTable
        dtArt = Nothing
        GvBusArticulo.datasource = dtArt
        GvBusArticulo.databind()
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Call Limpiar_Cajas_Buscar_Articulos()
        BtnLimpiar.Enabled = True
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        'Dim dt As New DataTable
        'Dim psListaArt As String = "1"
        'Dim psListaMarca As String = "1"
        'Dim psListaModelo As String = "1"
        'Dim psconexion As String = Session("Ruta_Emp")
        'Dim Codigo As String = TxtCodArticuloBA.Value.ToString
        'Dim Clasificacion As String = LblCodClasificacionBA.Text.ToString

        'Dim Descripcion As String = TxtDescripcionBA.Value.ToString
        'Dim Tipo As String = DdlTipoBA.SelectedValue.ToString
        'Dim NuPart As String = TxtNumParteBA.Value.ToString
        'Dim CodEs As String = TxtCodEspecificoBA.Value.ToString
        'Dim marca As String = LblCodMarcaBA.Text.ToString
        'Dim modelo As String = LblCodModeloBA.Text.ToString

        'If marca <> "" Then psListaMarca = ""
        'If modelo <> "" Then psListaModelo = ""
        'If Codigo <> "" Then psListaArt = ""
        'If Tipo = "< Seleccionar >" Then Tipo = ""

        'dt = objCat.Bus_Articulo(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)

        'If dt.Rows.Count > 0 Then
        '    GvBusArticulo.DataSource = dt
        '    GvBusArticulo.DataBind()
        'Else
        '    GvBusArticulo.DataSource = Nothing
        '    GvBusArticulo.DataBind()
        'End If


        Try
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

            GvBusArticulo.DataSource = dtColum
            GvBusArticulo.DataBind()



        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try

    End Sub

    Protected Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        LblError.Text = ""
        TxtCantidad.Text = ""
        TxtNroPlaca.Text = ""
        TxtNroSerie.Text = ""
        txtCodOrigen.Text = ""
        txtDescripcion.Text = ""
        TxtDestinoCodigo.Text = ""
        TxtDestinoDescripcion.Text = ""
        lblCodDestino.Text = ""
        lblCodOrigen.Text = ""
        Lbl6.Text = "Origen"
        lblDestino.Text = "Destino"
        Session("TipoDestino") = ""
        Session("TipoOrigen") = ""
        optOrigen.Checked = False
        optOrigen2.Checked = False
        RbDestino.Checked = False
        RbDestino2.Checked = False
        RbDestino3.Checked = False
        RbDestino4.Checked = False
        RbDestino5.Checked = False
        DdlMotivo.SelectedValue = "< Seleccionar >"
        Dim dt As New DataTable
        dt = Nothing
        GvListaArticulos.DataSource = dt
        GvListaArticulos.DataBind()
    End Sub

    Private Sub BtnBuscarOrigen_Click(sender As Object, e As EventArgs) Handles BtnBuscarOrigen.Click
        TituloPopup.Text = Lbl6.Text
        Session("TipoBusqueda") = "Origen"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub optOrigen_CheckedChanged(sender As Object, e As EventArgs) Handles optOrigen.CheckedChanged
        txtCodOrigen.Text = ""
        lblCodOrigen.Text = ""
        txtDescripcion.Text = ""
        If optOrigen.Checked = "true" Then
            Call Carga_Motivos()
            Session("TipoOrigen") = "1"
            BtnBuscarOrigen.Enabled = True
            Lbl6.Text = "Origen : "
        End If
    End Sub

    Private Sub optOrigen2_CheckedChanged(sender As Object, e As EventArgs) Handles optOrigen2.CheckedChanged
        txtCodOrigen.Text = ""
        lblCodOrigen.Text = ""
        txtDescripcion.Text = ""
        If optOrigen2.Checked = "true" Then
            Call Carga_Motivos()
            Session("TipoOrigen") = "2"
            BtnBuscarOrigen.Enabled = True
            Lbl6.Text = "Origen : "
        End If
    End Sub

    Private Sub BtnBuscaDetino_Click(sender As Object, e As EventArgs) Handles BtnBuscaDetino.Click
        TituloPopup.Text = lblDestino.Text
        Session("TipoBusqueda") = "Destino"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub RbDestino_CheckedChanged(sender As Object, e As EventArgs) Handles RbDestino.CheckedChanged
        TxtDestinoCodigo.Text = ""
        TxtDestinoDescripcion.Text = ""
        lblCodDestino.Text = ""
        If RbDestino.Checked = "true" Then
            Call Carga_Motivos()
            Session("TipoDestino") = "1"
            BtnBuscaDetino.Enabled = True
            lblDestino.Text = "Destino : "
        End If
    End Sub

    Private Sub RbDestino2_CheckedChanged(sender As Object, e As EventArgs) Handles RbDestino2.CheckedChanged
        TxtDestinoCodigo.Text = ""
        TxtDestinoDescripcion.Text = ""
        lblCodDestino.Text = ""
        If RbDestino2.Checked = "true" Then
            Call Carga_Motivos()
            Session("TipoDestino") = "2"
            BtnBuscaDetino.Enabled = True
            lblDestino.Text = "Destino : "
        End If
    End Sub

    Private Sub RbDestino3_CheckedChanged(sender As Object, e As EventArgs) Handles RbDestino3.CheckedChanged
        TxtDestinoCodigo.Text = ""
        TxtDestinoDescripcion.Text = ""
        lblCodDestino.Text = ""
        If RbDestino3.Checked = "true" Then
            Call Carga_Motivos()
            Session("TipoDestino") = "3"
            BtnBuscaDetino.Enabled = True
            lblDestino.Text = "Destino : "
        End If
    End Sub

    Private Sub RbDestino4_CheckedChanged(sender As Object, e As EventArgs) Handles RbDestino4.CheckedChanged
        TxtDestinoCodigo.Text = ""
        TxtDestinoDescripcion.Text = ""
        lblCodDestino.Text = ""
        If RbDestino4.Checked = "true" Then
            Call Carga_Motivos()
            Session("TipoDestino") = "6"
            BtnBuscaDetino.Enabled = True
            lblDestino.Text = "Destino : "
        End If
    End Sub

    Private Sub RbDestino5_CheckedChanged(sender As Object, e As EventArgs) Handles RbDestino5.CheckedChanged
        TxtDestinoCodigo.Text = ""
        TxtDestinoDescripcion.Text = ""
        lblCodDestino.Text = ""
        If RbDestino5.Checked = "true" Then
            Call Carga_Motivos()
            Session("TipoDestino") = "5"
            BtnBuscaDetino.Enabled = True
            lblDestino.Text = "Destino : "
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodigo As Double = 0
        Dim objCont As New clsCont_Listados
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""
        If (Session("TipoOrigen") = "1" And Session("TipoBusqueda") = "Origen") Or (Session("TipoDestino") = "1" And Session("TipoBusqueda") = "Destino") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodigo = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaAlmacen(psconexion, Session("CodEmpresa"), psBusCodigo, descripcion)
        ElseIf (Session("TipoOrigen") = "2" And Session("TipoBusqueda") = "Origen") Or (Session("TipoDestino") = "2" And Session("TipoBusqueda") = "Destino") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaCentroCosto(psconexion, Session("CodEmpresa"), psBusCodInterno, descripcion)
        ElseIf (Session("TipoOrigen") = "3" And Session("TipoBusqueda") = "Origen") Or (Session("TipoDestino") = "3" And Session("TipoBusqueda") = "Destino") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "2")
        ElseIf Session("TipoDestino") = "6" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "1")
        ElseIf Session("TipoDestino") = "5" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "5")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And Session("TipoBusqueda") = "Origen" Then
            txtCodOrigen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodOrigen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        ElseIf e.CommandName = "Aceptar" And Session("TipoBusqueda") = "Destino" Then
            TxtDestinoCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtDestinoDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodDestino.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        End If
    End Sub
    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('hide');", True)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Call Limpiar_Popup()
    End Sub

    Private Sub GvBusArticulo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusArticulo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dt As New DataTable
        Dim drT As DataRow

        If e.CommandName = "Aceptar" Then
            If Session("BuscarArticulo") = "Si" Then
                txtArtCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                txtArtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                lblTipoArt.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                Session("BuscarArticulo") = "No"
                BtnLimpiar.Enabled = True
                Limpiar_Cajas_Buscar_Articulos()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
            Else
                dt.Columns.Add("COD_ARTICULO")
                dt.Columns.Add("ART_CODEQUIVA")
                dt.Columns.Add("ART_DESCRIPCION")
                dt.Columns.Add("Art_sku")
                dt.Columns.Add("STOCK")
                dt.Columns.Add("CANTIDAD")
                dt.Columns.Add("SERIE_NRO")
                dt.Columns.Add("PLACA_NRO")
                dt.Columns.Add("TipoBien")
                dt.Columns.Add("TIPO_UBICACION")
                dt.Columns.Add("COD_ALMACEN")
                dt.Columns.Add("ALMACEN_NOMBRE")
                dt.Columns.Add("ubicact_codigo")
                dt.Columns.Add("ubicact_tipo")
                dt.Columns.Add("Serie_Numerar")
                dt.Columns.Add("Art_tipo")

                For Each row As GridViewRow In GvListaArticulos.Rows
                    drT = dt.NewRow()
                    drT("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("Art_sku") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("STOCK") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("CANTIDAD") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("Serie_Numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("Art_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    dt.Rows.Add(drT)
                Next
                drT = dt.NewRow()
                drT("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("Art_sku") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("STOCK") = ""
                drT("CANTIDAD") = TxtCantidad.Text
                drT("SERIE_NRO") = ""
                drT("PLACA_NRO") = ""
                drT("TipoBien") = ""
                drT("TIPO_UBICACION") = ""
                drT("COD_ALMACEN") = ""
                drT("ALMACEN_NOMBRE") = ""
                drT("ubicact_codigo") = ""
                drT("ubicact_tipo") = ""
                drT("Serie_Numerar") = ""
                drT("Art_tipo") = lblTipoArt.Text
                dt.Rows.Add(drT)

                GvListaArticulos.DataSource = dt
                GvListaArticulos.DataBind()

                BtnLimpiar.Enabled = True
                Limpiar_Cajas_Buscar_Articulos()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
            End If
        End If

    End Sub

    Private Sub BuscarBien_xPlacaSerie(Optional ps_SerieNro As String = "", Optional ps_PlacaNro As Double = 0)
        Dim psSerie As String = ""
        Dim psPlaca As Double = 0
        Dim dtDatos As New DataTable
        'Lista_Equipos_MoverUno
        Dim dt As New DataTable
        Dim drT As DataRow
        Dim psMensaje As String = ""

        If ps_SerieNro <> "" Then
            psSerie = ps_SerieNro
        End If
        If ps_PlacaNro > 0 Then
            psPlaca = ps_PlacaNro
        End If

        If psSerie <> "" And psPlaca = 0 Then
            For i = 0 To GvListaArticulos.Rows.Count - 1
                If psSerie = GvListaArticulos.Rows(i).Cells(7).Text.Trim Then
                    psMensaje = "alert('El Nro. de serie " & psSerie & " ya esta ingresado.');"
                End If
            Next
        ElseIf psPlaca <> 0 And psSerie = "" Then
            For i = 0 To GvListaArticulos.Rows.Count - 1
                If psPlaca = Nz(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(i).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")) Then
                    psMensaje = "alert('El Nro. de placa " & psPlaca & " ya esta ingresado.');"
                End If
            Next
        End If

        If psMensaje <> "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", psMensaje, True)
        Else

            dt.Columns.Add("COD_ARTICULO")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("ART_SKU")
            dt.Columns.Add("STOCK")
            dt.Columns.Add("CANTIDAD")
            dt.Columns.Add("SERIE_NRO")
            dt.Columns.Add("PLACA_NRO")
            dt.Columns.Add("TipoBien")
            dt.Columns.Add("TIPO_UBICACION")
            dt.Columns.Add("COD_ALMACEN")
            dt.Columns.Add("ALMACEN_NOMBRE")
            dt.Columns.Add("ubicact_codigo")
            dt.Columns.Add("ubicact_tipo")
            dt.Columns.Add("Serie_Numerar")
            dt.Columns.Add("art_Tipo")

            dtDatos = obj.Lista_Equipos_MoverUno(Session("Ruta_Emp"), Session("CodEmpresa"), psSerie, psPlaca)

            For Each row As GridViewRow In GvListaArticulos.Rows
                drT = dt.NewRow()
                drT("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ART_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("STOCK") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("CANTIDAD") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("Serie_Numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                drT("Art_Tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                dt.Rows.Add(drT)
            Next


            If dtDatos.Rows.Count > 0 And optIngreso2.Checked = True Then
                For Each drow As DataRow In dtDatos.Rows
                    drT = dt.NewRow()
                    drT("COD_ARTICULO") = Nu(drow("COD_ARTICULO"))
                    drT("ART_CODEQUIVA") = Nu(drow("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drow("ART_DESCRIPCION"))
                    drT("ART_SKU") = Nu(drow("ART_SKU"))
                    drT("STOCK") = ""
                    drT("CANTIDAD") = "1"
                    drT("SERIE_NRO") = Nu(drow("SERIE_NRO"))
                    drT("PLACA_NRO") = Nu(drow("PLACA_NRO"))
                    drT("TipoBien") = Nu(drow("TipoBien"))
                    drT("TIPO_UBICACION") = Nu(drow("TIPO_UBICACION"))
                    drT("COD_ALMACEN") = Nu(drow("COD_ALMACEN"))
                    drT("ALMACEN_NOMBRE") = Nu(drow("ALMACEN_NOMBRE"))
                    drT("ubicact_codigo") = Nu(drow("ubicact_codigo"))
                    drT("ubicact_tipo") = Nu(drow("ubicact_tipo"))
                    drT("Serie_Numerar") = Nu(drow("Serie_Numerar"))
                    drT("Art_Tipo") = Nu(drow("Art_Tipo"))
                    dt.Rows.Add(drT)
                Next
            ElseIf psPlaca <> 0 And optIngreso.Checked = True Then
                drT = dt.NewRow()
                drT("COD_ARTICULO") = txtArtCodigo.Text
                drT("ART_CODEQUIVA") = ""
                drT("ART_DESCRIPCION") = txtArtDescripcion.Text
                drT("ART_SKU") = ""
                drT("STOCK") = ""
                drT("CANTIDAD") = "1"
                drT("SERIE_NRO") = psPlaca
                drT("PLACA_NRO") = psPlaca
                drT("TipoBien") = ""
                drT("TIPO_UBICACION") = ""
                drT("COD_ALMACEN") = ""
                drT("ALMACEN_NOMBRE") = ""
                drT("ubicact_codigo") = ""
                drT("ubicact_tipo") = ""
                drT("Serie_Numerar") = ""
                drT("Art_Tipo") = lblTipoArt.Text
                dt.Rows.Add(drT)
            ElseIf psSerie <> "" And optIngreso.Checked = True Then
                drT = dt.NewRow()
                drT("COD_ARTICULO") = txtArtCodigo.Text
                drT("ART_CODEQUIVA") = ""
                drT("ART_DESCRIPCION") = txtArtDescripcion.Text
                drT("ART_SKU") = ""
                drT("STOCK") = ""
                drT("CANTIDAD") = "1"
                drT("SERIE_NRO") = psSerie
                drT("PLACA_NRO") = ""
                drT("TipoBien") = ""
                drT("TIPO_UBICACION") = ""
                drT("COD_ALMACEN") = ""
                drT("ALMACEN_NOMBRE") = ""
                drT("ubicact_codigo") = ""
                drT("ubicact_tipo") = ""
                drT("Serie_Numerar") = ""
                drT("Art_Tipo") = lblTipoArt.Text
                dt.Rows.Add(drT)
            End If
            GvListaArticulos.DataSource = dt
            GvListaArticulos.DataBind()
            TxtNroPlaca.Text = ""
            TxtNroSerie.Text = ""
        End If
    End Sub

    Private Sub TxtNroSerie_TextChanged(sender As Object, e As EventArgs) Handles TxtNroSerie.TextChanged
        If TxtNroSerie.Text.Trim <> "" Then
            Call BuscarBien_xPlacaSerie(TxtNroSerie.Text.Trim)
        End If
    End Sub

    Private Sub TxtNroPlaca_TextChanged(sender As Object, e As EventArgs) Handles TxtNroPlaca.TextChanged
        If TxtNroPlaca.Text.Trim <> "" Then
            Call BuscarBien_xPlacaSerie("", Val(TxtNroPlaca.Text.Trim))
        End If
    End Sub
    Protected Sub BtnCargaArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargaArchivo.Click
        If optIngreso.Checked = True And txtArtCodigo.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el codigo del articulo.');", True)
        ElseIf optIngreso2.Checked = True Or optIngreso.Checked = True Then
            If fileUpload.HasFile Then
                ' Obtiene el nombre del archivo y su extensión
                Dim fileName As String = Path.GetFileName(fileUpload.PostedFile.FileName)
                Dim fileExtension As String = Path.GetExtension(fileName)

                ' Verifica que el archivo sea un archivo de texto
                If fileExtension.ToLower() = ".txt" Then
                    ' Lee el contenido del archivo de texto
                    Dim fileContent As String = ""
                    Using reader As New StreamReader(fileUpload.PostedFile.InputStream)
                        While Not reader.EndOfStream
                            ' Lee cada línea del archivo y agrega un salto de línea
                            fileContent = reader.ReadLine()
                            ' Actualiza el contenido del UpdatePanel
                            Call BuscarBien_xPlacaSerie("", CDbl(Val(fileContent)))
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

        End If
    End Sub

    Protected Sub BtnCargaSeries_Click(sender As Object, e As EventArgs) Handles BtnCargaSeries.Click
        If fileUpload.HasFile Then
            ' Obtiene el nombre del archivo y su extensión
            Dim fileName As String = Path.GetFileName(fileUpload.PostedFile.FileName)
            Dim fileExtension As String = Path.GetExtension(fileName)

            ' Verifica que el archivo sea un archivo de texto
            If fileExtension.ToLower() = ".txt" Then
                ' Lee el contenido del archivo de texto
                Dim fileContent As String = ""
                Using reader As New StreamReader(fileUpload.PostedFile.InputStream)
                    While Not reader.EndOfStream
                        ' Lee cada línea del archivo y agrega un salto de línea
                        fileContent = reader.ReadLine()
                        ' Actualiza el contenido del UpdatePanel
                        Call BuscarBien_xPlacaSerie(fileContent)
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
    Protected Sub BtnEjecutar_Click(sender As Object, e As EventArgs) Handles BtnEjecutar.Click

        Dim pdCantBien As Double = 0
        pdCantBien = 0
        Dim objProceso As New clsInv_Procesos
        Dim objGeneral As New ModuloGeneral
        For i = 0 To GvListaArticulos.Rows.Count - 1
            pdCantBien = pdCantBien + CDbl(Nz(GvListaArticulos.Rows(i).Cells(6).Text.Trim))
        Next
        Dim psCodSalida As String = ""
        Dim psCodRecepcion As String : psCodRecepcion = ""
        Dim psTipoDestino As String = ""
        Dim ValorSys As String : ValorSys = FechaActual() + HoraActual() + Session("User")
        Dim psRucEmpresa As String = ""
        Dim StockAc As Double = 0
        Dim CantIng As Double = 0
        Dim psRTipoOrigen As String = ""
        Dim psProveedor As String = ""
        Dim lblNroMovimiento As String = ""
        'psRucEmpresa = Ruc_Empresa
        Dim psCodRecepcion2 As String = ""
        psRTipoOrigen = IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", ""))
        If optIngreso.Checked = True Then
            psTipoDestino = IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", ""))
        Else
            psTipoDestino = IIf(RbDestino.Checked = True, "1", IIf(RbDestino2.Checked = True, "2", IIf(RbDestino3.Checked = True, "3", IIf(RbDestino4.Checked = True, "6", IIf(RbDestino5.Checked = True, "5", "")))))
        End If

        Dim dtEmpresa As New DataTable
        dtEmpresa = objGeneral.Datos_Empresa(Session("Ruta_Emp"), Session("CodEmpresa"))
        For Each dr As DataRow In dtEmpresa.Rows
            psRucEmpresa = Nu(dr("emp_ruc"))
        Next
        dtEmpresa = Nothing

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim RsRecep As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2

        Dim psFechaFormato As String = ""
        Dim psHoraFormato As String = ""
        psFechaFormato = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        psHoraFormato = Mid(TxtHora.Text, 1, 2) + Mid(TxtHora.Text, 4, 2)
        If lblCodOrigen.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar Origen.');", True)
        ElseIf lblCodDestino.Text = "" And optIngreso2.Checked = True Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar Destino.');", True)
        ElseIf DdlMotivo.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, seleccionar el motivo de la salida o ingreso.');", True)
        ElseIf pdCantBien = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar bienes.');", True)
        Else
            Dim TotArt As Double = 0
            Dim psCant As Double = 0
            With GvListaArticulos
                If .Rows.Count = 0 Then LblError.Text = LblError.Text & "<br> - No hay detalle de recepción que guardar."
                For i = 0 To .Rows.Count - 1
                    psCant = Nz(GvListaArticulos.Rows(i).Cells(6).Text)
                    If psCant > "0" Then
                        TotArt = TotArt + psCant
                    End If
                Next
            End With
            If optIngreso.Checked = True Then

                CmdGlobal.CommandText = " SELECT PERSONA_CODIGO FROM TBDATA_PERSONAS WHERE PERSONA_SYS_EST = '0' AND PERSONA_TIPO = '2' AND PERSONA_RUC = '" & psRucEmpresa & "' "
                RsRecep = CmdGlobal.ExecuteReader
                If RsRecep.HasRows Then
                    While RsRecep.Read
                        psProveedor = Nu(RsRecep(0))
                    End While
                End If
                RsRecep.Close()

                CmdGlobal.CommandText = "SELECT isnull(MAX(RECEP_CODIGO),0) FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodRecepcion = Nz(Rs(0)) + 1
                    End While
                Else
                    psCodRecepcion = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,  ALTIBI_CODIGO, RECEP_PROYECTO, RECEP_FECHA_REC , RECEP_HORA_REC, RECEP_TIPODOC,  " _
                                  & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, " _
                                  & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO, RECEP_CREADO_DESDE) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodRecepcion & "," & lblCodOrigen.Text & ", '1','1', '" & psFechaFormato & "', '" & psHoraFormato & "', '7', " _
                                  & " '" & psFechaFormato & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "',''," & GvListaArticulos.Rows.Count - 1 & ",'2'," _
                                  & " '0','" & ValorSys & "'," & pdCantBien & "," & pdCantBien & ",0,0,'N','" & DdlMotivo.SelectedValue & "','','1', '', '" & psRTipoOrigen & "','I')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal2.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                        & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                        & "'" & Session("CodEmpresa") & "'," & psCodRecepcion & ",1," & txtArtCodigo.Text & "," & pdCantBien & " ," & pdCantBien & "," _
                                        & " 0 ,0," & pdCantBien & ",'1','0','" & DdlMotivo.SelectedValue & "','N')"
                CmdGlobal2.ExecuteNonQuery()
                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") AND (UBICACT_TIPO='" & psTipoDestino & "')" _
                                    & " AND (ARTICULO_CODIGO = " & txtArtCodigo.Text & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                RsRecep = CmdGlobal.ExecuteReader
                If RsRecep.HasRows Then
                    While RsRecep.Read
                        StockAc = Nz(RsRecep!SAA_STOCK_ACTUAL)
                        StockAc = StockAc + pdCantBien
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") AND (UBICACT_TIPO='" & psRTipoOrigen & "')" _
                                                          & " AND (ARTICULO_CODIGO = " & txtArtCodigo.Text & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                Else
                    CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                          & "VALUES('" & psRTipoOrigen & "'," & lblCodOrigen.Text & "," & txtArtCodigo.Text.Trim & "," & pdCantBien & ",'0','" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End If
                RsRecep.Close()
                CmdGlobal.CommandText = "SELECT isnull(MAX(MOV_NRO),0) FROM TBINV_MOVIMIENTO_GENERAL "
                RsRecep = CmdGlobal.ExecuteReader
                If RsRecep.HasRows Then
                    While RsRecep.Read
                        lblNroMovimiento = Nu(RsRecep(0)) + 1
                    End While
                Else
                    lblNroMovimiento = "00000001"
                End If
                RsRecep.Close()
                '1: INGRESO, 2:SALIDA
                'Call Movimiento_Kardex(psCodRecepcion2, "20", Flex.TextMatrix(i, 1), IIf(optOrigen(1).value = True, "1", IIf(optOrigen(2).value = True, "2", "")), lblCodOrigen.Caption, "", "", "", "1", txtFechaRecep, CantIng)
                'Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecepcion, DdlMotivo.SelectedValue, GvListaArticulos.Rows(i).Cells(1).Text.Trim, psTipoDestino, lblCodDestino.Text, "", "", "", "1", TxtFecha.Text.Trim, 1)
                CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO,CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                      & " values('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psRTipoOrigen & "','" & lblCodOrigen.Text & "','" & psRTipoOrigen & "','" & lblCodOrigen.Text & "','" & psCodRecepcion & "','" & txtArtCodigo.Text & "','" & pdCantBien & "','" & ValorSys & "','2','" & DdlMotivo.SelectedValue & "','" & FechaActual() & "','0')"
                CmdGlobal.ExecuteNonQuery()
                Dim SerieNum As Double = 0
                With GvListaArticulos
                    For n = 0 To .Rows.Count - 1
                        If .Rows(n).Cells(16).Text = "73" Or .Rows(n).Cells(16).Text = "64" Then
                            CmdGlobal.CommandText = "SELECT isnull(MAX(SERIE_NUMERAR),0) FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & ""
                            RsRecep = CmdGlobal.ExecuteReader
                            If RsRecep.HasRows Then
                                While RsRecep.Read
                                    SerieNum = Nz(RsRecep(0)) + 1
                                End While
                            Else
                                SerieNum = 1
                            End If
                            RsRecep.Close()
                            CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "(SERIE_NUMERAR, RECEP_CODIGO, ARTICULO_CODIGO,SERIE_NRO,UBICACT_TIPO,UBICACT_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO,CRITI_CODIGO,CONFIDENCIALIDAD,DISPONIBILIDAD,SERIE_ESTADO,TIPO_GARANTIA) " _
                                          & "VALUES(" & SerieNum & "," & psCodRecepcion & "," & .Rows(n).Cells(1).Text & ",'" & .Rows(n).Cells(7).Text & "','" & psRTipoOrigen & "'," & lblCodOrigen.Text & ",'N','" & ValorSys & "','0','S','1','2','1','2','0','')"
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL) " _
                                              & "VALUES(" & SerieNum & ",'" & psRTipoOrigen & "'," & lblCodOrigen.Text.Trim & ",'0','0','" & ValorSys & "','" & FechaActual() & "','1'," & psCodRecepcion & ")"
                            CmdGlobal.ExecuteNonQuery()
                            objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, psRTipoOrigen, lblCodOrigen.Text, psRTipoOrigen, lblCodOrigen.Text, SerieNum, Session("User"))
                        ElseIf .Rows(n).Cells(16).Text = "88" Then
                            CmdGlobal.CommandText = "SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & ""
                            RsRecep = CmdGlobal.ExecuteReader
                            If RsRecep.HasRows Then
                                While RsRecep.Read
                                    SerieNum = Nz(RsRecep(0)) + 1
                                End While
                            Else
                                SerieNum = 1
                            End If
                            RsRecep.Close()
                            CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "(SERIE_NUMERAR, RECEP_CODIGO, ARTICULO_CODIGO,SERIE_NRO,UBICACT_TIPO,UBICACT_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO,CRITI_CODIGO,CONFIDENCIALIDAD,DISPONIBILIDAD,SERIE_ESTADO,TIPO_GARANTIA) " _
                                          & "VALUES(" & SerieNum & "," & psCodRecepcion & "," & .Rows(n).Cells(1).Text & ",'" & .Rows(n).Cells(7).Text & "','" & psRTipoOrigen & "'," & lblCodOrigen.Text & ",'N','" & ValorSys & "','0','S','1','2','1','2','0','')"
                            CmdGlobal.ExecuteNonQuery()
                            If Nz(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(.Rows(n).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")) > 0 Then
                                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET PLACA_NRO = " & Nz(.Rows(n).Cells(7).Text) & " WHERE SERIE_NUMERAR = " & SerieNum
                                CmdGlobal.ExecuteNonQuery()
                            End If
                            objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, psRTipoOrigen, lblCodOrigen.Text, psRTipoOrigen, lblCodOrigen.Text, SerieNum, Session("User"))
                            CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL) " _
                                              & "VALUES(" & SerieNum & ",'" & psRTipoOrigen & "'," & lblCodOrigen.Text.Trim & ",'0','0','" & ValorSys & "','" & FechaActual() & "','1'," & psCodRecepcion & ")"
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    Next
                End With
                BtnLimpiar_Click(sender, e)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('se genero la recepcion Nro. " & psCodRecepcion & ".');", True)
            Else
                If psTipoDestino = "1" Or psTipoDestino = "2" Then
                    CmdGlobal.CommandText = " SELECT PERSONA_CODIGO FROM TBDATA_PERSONAS WHERE PERSONA_SYS_EST = '0' AND PERSONA_TIPO = '2' AND PERSONA_RUC = '" & psRucEmpresa & "' "
                    RsRecep = CmdGlobal.ExecuteReader
                    If RsRecep.HasRows Then
                        While RsRecep.Read
                            psProveedor = Nu(RsRecep(0))
                        End While
                    End If
                    RsRecep.Close()

                    CmdGlobal.CommandText = "SELECT MAX(RECEP_CODIGO) FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodRecepcion = Nz(Rs(0)) + 1
                        End While
                    Else
                        psCodRecepcion = 1
                    End If
                    Rs.Close()
                    CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,  ALTIBI_CODIGO, RECEP_PROYECTO, RECEP_FECHA_REC , RECEP_HORA_REC, RECEP_TIPODOC,  " _
                                      & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, " _
                                      & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO, RECEP_CREADO_DESDE) " _
                                      & " VALUES('" & Session("CodEmpresa") & "'," & psCodRecepcion & "," & lblCodDestino.Text & ", '1','1', '" & psFechaFormato & "', '" & psHoraFormato & "', '7', " _
                                      & " '" & psFechaFormato & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "',''," & GvListaArticulos.Rows.Count - 1 & ",'2'," _
                                      & " '0','" & ValorSys & "'," & pdCantBien & "," & pdCantBien & ",0,0,'N','8','','1', '', '" & IIf(RbDestino.Checked = True, "1", IIf(RbDestino2.Checked = True, "2", IIf(RbDestino3.Checked = True, "3", IIf(RbDestino4.Checked = True, "6", IIf(RbDestino5.Checked = True, "5", ""))))) & "','I')"
                    CmdGlobal.ExecuteNonQuery()
                End If

                For i = 0 To GvListaArticulos.Rows.Count - 1
                    If GvListaArticulos.Rows(i).Cells(14).Text.Trim = psTipoDestino And GvListaArticulos.Rows(i).Cells(13).Text.Trim = lblCodDestino.Text Then
                    Else
                        If Replace(GvListaArticulos.Rows(i).Cells(15).Text.Trim, "&nbsp;", "") <> "" Then
                            Call Despacho_unoxuno(GvListaArticulos.Rows(i).Cells(14).Text.Trim, TxtFecha.Text, lblCodDestino.Text, psTipoDestino, DdlMotivo.SelectedValue, GvListaArticulos.Rows(i).Cells(13).Text.Trim, GvListaArticulos.Rows(i).Cells(14).Text.Trim, GvListaArticulos.Rows(i).Cells(1).Text.Trim)
                            psCodSalida = CodSalida
                        End If
                        a = a + 1
                        If psTipoDestino = "1" Or psTipoDestino = "2" Then

                            CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & psCodRecepcion & " AND ARTICULO_CODIGO= " & GvListaArticulos.Rows(i).Cells(1).Text.Trim
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_XREC = " & Nz(Rs!RECEPD_CANT_XREC) + 1 & ",RECEPD_CANT_ING = " & Nz(Rs!RECEPD_CANT_XREC) + 1 & ",RECEPD_CANT_REC = " & Nz(Rs!RECEPD_CANT_REC) + 1 & " " _
                                                      & " WHERE RECEP_CODIGO = " & psCodRecepcion & "  AND  ARTICULO_CODIGO = " & GvListaArticulos.Rows(i).Cells(1).Text.Trim
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                                Rs.Close()
                            Else
                                Rs.Close()
                                CmdGlobal.CommandText = "SELECT MAX(RECEPD_ITEM) FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & psCodRecepcion
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        CmdGlobal2.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                          & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                          & "'" & Session("CodEmpresa") & "'," & psCodRecepcion & "," & Nz(Rs(0)) + 1 & "," & GvListaArticulos.Rows(i).Cells(1).Text.Trim & "," & GvListaArticulos.Rows(i).Cells(6).Text.Trim & " ," & GvListaArticulos.Rows(i).Cells(6).Text.Trim & "," _
                                                          & " 0 ,0,1,'1','0','5','N')"
                                        CmdGlobal2.ExecuteNonQuery()
                                    End While
                                Else
                                    CmdGlobal2.CommandText = "INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                          & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                          & "'" & Session("CodEmpresa") & "'," & psCodRecepcion & ",1," & GvListaArticulos.Rows(i).Cells(1).Text.Trim & "," & GvListaArticulos.Rows(i).Cells(6).Text.Trim & " ," & GvListaArticulos.Rows(i).Cells(6).Text.Trim & "," _
                                                          & " 0 ,0,1,'1','0','5','N')"
                                    CmdGlobal2.ExecuteNonQuery()
                                End If
                                Rs.Close()
                            End If
                        End If
                        If Replace(GvListaArticulos.Rows(i).Cells(15).Text.Trim, "&nbsp;", "") = "" Then
                            StockAc = 0
                            CantIng = 0
                            If CDbl(Nz(GvListaArticulos.Rows(i).Cells(6).Text.Trim)) > CDbl(Nz(Replace(GvListaArticulos.Rows(i).Cells(5).Text.Trim, "&nbsp;", ""))) Then
                                CantIng = CDbl(Nz(GvListaArticulos.Rows(i).Cells(6).Text.Trim)) - CDbl(Nz(Replace(GvListaArticulos.Rows(i).Cells(5).Text.Trim, "&nbsp;", "")))
                            End If
                            lblNroMovimiento = ""
                            psCodRecepcion2 = ""
                            If CantIng > 0 Then
                                CmdGlobal.CommandText = "SELECT MAX(RECEP_CODIGO) FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                RsRecep = CmdGlobal.ExecuteReader
                                If RsRecep.HasRows Then
                                    While RsRecep.Read
                                        psCodRecepcion2 = Nu(RsRecep(0)) + 1
                                    End While
                                Else
                                    psCodRecepcion = 1
                                End If
                                RsRecep.Close()
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,  ALTIBI_CODIGO, RECEP_PROYECTO, RECEP_FECHA_REC , RECEP_HORA_REC, RECEP_TIPODOC,  " _
                                                  & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, " _
                                                  & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO, RECEP_CREADO_DESDE) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodRecepcion2 & "," & lblCodOrigen.Text & ", '1','1', '" & psFechaFormato & "', '" & psHoraFormato & "', '7', " _
                                                  & " '" & psFechaFormato & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','',1,'2'," _
                                                  & " '0','" & ValorSys & "'," & CantIng & "," & CantIng & ",0,0,'N','20','','1', '', '" & IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")) & "','I')"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                      & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                      & "'" & Session("CodEmpresa") & "'," & psCodRecepcion2 & ",1," & Nz(GvListaArticulos.Rows(i).Cells(1).Text.Trim) & "," & CantIng & " ," & CantIng & "," _
                                                      & " 0 ,0," & CantIng & ",'1','0','20','N')"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") AND (UBICACT_TIPO='" & IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")) & "')" _
                                    & " AND (ARTICULO_CODIGO = " & GvListaArticulos.Rows(i).Cells(1).Text.Trim & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                RsRecep = CmdGlobal.ExecuteReader
                                If RsRecep.HasRows Then
                                    While RsRecep.Read
                                        StockAc = Nz(RsRecep!SAA_STOCK_ACTUAL)
                                        StockAc = StockAc + CantIng
                                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodOrigen.Text & ") AND (UBICACT_TIPO='" & IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")) & "')" _
                                                          & " AND (ARTICULO_CODIGO = " & GvListaArticulos.Rows(i).Cells(1).Text.Trim & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                        CmdGlobal2.ExecuteNonQuery()
                                    End While
                                Else
                                    CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                          & "VALUES('" & IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")) & "'," & lblCodOrigen.Text & "," & GvListaArticulos.Rows(i).Cells(1).Text.Trim & "," & CantIng & ",'0','" & Session("CodEmpresa") & "')"
                                    CmdGlobal2.ExecuteNonQuery()
                                End If
                                RsRecep.Close()
                                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                RsRecep = CmdGlobal.ExecuteReader
                                If RsRecep.HasRows Then
                                    While RsRecep.Read
                                        lblNroMovimiento = Nu(RsRecep(0)) + 1
                                    End While
                                Else
                                    lblNroMovimiento = "00000001"
                                End If
                                RsRecep.Close()
                                '1: INGRESO, 2:SALIDA
                                'Call Movimiento_Kardex(psCodRecepcion2, "20", Flex.TextMatrix(i, 1), IIf(optOrigen(1).value = True, "1", IIf(optOrigen(2).value = True, "2", "")), lblCodOrigen.Caption, "", "", "", "1", txtFechaRecep, CantIng)
                                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecepcion2, "20", GvListaArticulos.Rows(i).Cells(1).Text.Trim, IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")), lblCodOrigen.Text, "", "", "", "1", TxtFecha.Text.Trim, CantIng)
                                CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO,CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                      & " values('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")) & "','" & lblCodOrigen.Text & "','','','" & psCodRecepcion2 & "','" & GvListaArticulos.Rows(i).Cells(1).Text.Trim & "','" & CantIng & "','" & ValorSys & "','2','20','" & FechaActual() & "','0')"
                                CmdGlobal.ExecuteNonQuery()
                            End If
                        End If
                        If Replace(GvListaArticulos.Rows(i).Cells(14).Text.Trim, "&nbsp;", "") <> "" And (psTipoDestino = "1" Or psTipoDestino = "2") Then
                            CmdGlobal.CommandText = " INSERT INTO TBINV_RECEPCION_DETALLE_SERIES (EMPRESA_CODIGO, RECEP_CODIGO, SERIE_NUMERAR, SERIE_ORIG_TIPO, SERIE_ORIG_CODIGO) " _
                                                  & " VALUES ('" & Session("CodEmpresa") & "', " & psCodRecepcion & ", " & GvListaArticulos.Rows(i).Cells(15).Text.Trim & ", '" & IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")) & "', " & lblCodOrigen.Text & ")"
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    End If
                Next

                Dim psNroItem As Double = 0
                Dim psIngresoCodSal As String = ""
                psIngresoCodSal = ""
                If psCodRecepcion <> "" Then
                    CmdGlobal.CommandText = "SELECT MAX(RECEPD_ITEM) FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO = " & psCodRecepcion
                    RsRecep = CmdGlobal.ExecuteReader
                    If RsRecep.HasRows Then
                        While RsRecep.Read
                            psNroItem = Nz(RsRecep(0))
                        End While
                    End If
                    RsRecep.Close()
                    CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_NRO_ITEM = " & psNroItem & " WHERE RECEP_CODIGO = " & psCodRecepcion
                    CmdGlobal.ExecuteNonQuery()
                    If psCodRecepcion <> "" And CodSalida <> "" Then
                        CmdGlobal.CommandText = " UPDATE TBINV_RECEPCION_DETALLE_SERIES SET salida_codigo = " & CodSalida & " WHERE RECEP_CODIGO = " & psCodRecepcion
                        CmdGlobal.ExecuteNonQuery()
                        psIngresoCodSal = "1"
                    End If
                End If
                Call GenerarSalidaFinal()

                If psCodRecepcion <> "" And CodSalida <> "" And psIngresoCodSal = "" Then
                    CmdGlobal.CommandText = " UPDATE TBINV_RECEPCION_DETALLE_SERIES SET salida_codigo = " & CodSalida & " WHERE RECEP_CODIGO = " & psCodRecepcion
                    CmdGlobal.ExecuteNonQuery()
                End If
                Session("CodSalida") = CodSalida

                LblTituloModal.Text = "Nro. Salida de " & IIf(optOrigen.Checked = True, "Almacén ", IIf(optOrigen2.Checked = True, "CCostos ", "")) & Llenar_Ceros(CodSalida, 6)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModalGuia').modal('show');", True)

            End If
        End If
    End Sub

    Private Sub GenerarSalidaFinal()

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim RsRecep As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
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
        psTipoOrigen = IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", ""))
        psTipoDestino = IIf(RbDestino.Checked = True, "1", IIf(RbDestino2.Checked = True, "2", IIf(RbDestino3.Checked = True, "3", IIf(RbDestino4.Checked = True, "6", IIf(RbDestino5.Checked = True, "5", "")))))
        If psTipoDestino = "1" Then psDestinoAlm = lblCodDestino.Text
        If psTipoDestino = "2" Then psDestinoCC = lblCodDestino.Text
        If psTipoDestino = "3" Then DesCodProveedor = lblCodDestino.Text
        If psTipoDestino = "6" Then DesCodCliente = lblCodDestino.Text
        If psTipoDestino = "5" Then DesCodPersona = lblCodDestino.Text
        If lblCodDestino.Text <> "" Then psCodDestino = lblCodDestino.Text
        If lblCodOrigen.Text <> "" Then psCodOrigen = lblCodOrigen.Text
        StockAc = 0
        CodSalida = ""
        Dim objProceso As New clsInv_Procesos
        Dim psRecepcion As String : psRecepcion = ""
        i = 0
        Dim psProveedor As String = ""
        Dim psCodRecepcion As String = ""

        cant = 0
        For i = 0 To GvListaArticulos.Rows.Count - 1
            cant = cant + Nz(GvListaArticulos.Rows(i).Cells(6).Text.Trim)
        Next

        Dim psFechaFormato As String = ""
        Dim psHoraFormato As String = ""
        psFechaFormato = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        psHoraFormato = Mid(TxtHora.Text, 1, 2) + Mid(TxtHora.Text, 4, 2)
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
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                           & " ALMACEN_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO,PERSONA_CODIGO_DESTINO,PROVEEDOR_CODIGO_DESTINO, CLIENTE_CODIGO_DESTINO, " _
                           & " DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                           & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                           & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "'," & HoraActual() & ",'" & Session("User") & "','" & psTipoDestino & "'," _
                           & " " & psDestinoAlm & "," & psDestinoCC & ", " & DesCodPersona & ", " & DesCodProveedor & ", " & DesCodCliente & ", " _
                           & " '3','0'," & cant & "," & cant & "," & cant & ",0," & psCodOrigen & ", '" & psFechaFormato & "','" & psHoraFormato & "','" & DdlMotivo.SelectedValue.Trim & "','" & ValorSys & "')"
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
                        & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL,OSAL_SYS_REC) " _
                        & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & psTipoDestino & "'," _
                        & " " & psDestinoAlm & "," & psDestinoCC & ", " & DesCodProveedor & ", " & DesCodCliente & ", " & DesCodPersona & ", " _
                        & " '3','0'," & cant & "," & cant & ",0,'" & psCodOrigen & "'," _
                        & " '" & psFechaFormato & "','" & HoraActual() & "','" & DdlMotivo.SelectedValue.Trim & "', '" & ValorSys & "')"
            CmdGlobal.ExecuteNonQuery()
        End If
        Dim psCodAllSal As String = ""
        CmdGlobal.CommandText = "SELECT MAX(ALLSAL_CODIGO) FROM TBINV_SALIDA_MOTIVO"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psCodAllSal = Nz(Rs(0)) + 1
            End While
        Else
            psCodAllSal = 1
        End If
        Rs.Close
        CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                      & " VALUES ('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & psCodDespacho & ",'" & DdlMotivo.SelectedValue.Trim & "','" & IIf(optOrigen.Checked = True, "1", "2") & "'," & lblCodOrigen.Text & ", " _
                      & " '" & psTipoDestino & "'," & lblCodDestino.Text & ",'" & FechaActual() & "','" & HoraActual() & "','3','0','" & psFechaFormato & "')"
        CmdGlobal.ExecuteNonQuery()
        CodSalida = psCodDespacho
        Dim psCantItem As Double : psCantItem = 0
        Dim psItemSerie As Integer : psItemSerie = 0
        Dim psItemAcc As Integer : psItemAcc = 0

        For i = 0 To GvListaArticulos.Rows.Count - 1
            psSerieNumerar = Replace(GvListaArticulos.Rows(i).Cells(15).Text.Trim, "&nbsp;", "")
            psCodArt = GvListaArticulos.Rows(i).Cells(1).Text.Trim
            psCantItem = CDbl(Nz(GvListaArticulos.Rows(i).Cells(6).Text.Trim))
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
                    objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, IIf(optOrigen.Checked = True, "1", "2"), lblCodOrigen.Text, psTipoDestino, psCodDestino, psSerieNumerar, Session("User"))
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

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, DdlMotivo.Text, "2", TxtFecha.Text.Trim, psCantItem)
                CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                              & " VALUES ('" & Session("CodEmpresa") & "'," & lblNroMovimiento & ",'2','" & psTipoOrigen & "','" & psCodOrigen & "', " _
                              & " " & psCodArt & "," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & psFechaFormato & "','0','" & psCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
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

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, DdlMotivo.Text, "1", TxtFecha.Text.Trim, psCantItem)
                CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                              & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & psFechaFormato & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodOrigen & "')"
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
                    objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, IIf(optOrigen.Checked = True, "1", "2"), lblCodOrigen.Text, psTipoDestino, psCodDestino, psSerieNumerar, Session("User"))
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

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, DdlMotivo.Text, "2", TxtFecha.Text.Trim, psCantItem)
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & psCodOrigen & "', " _
                              & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & psFechaFormato & "','0','" & psCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
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

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, DdlMotivo.Text, "1", TxtFecha.Text.Trim, psCantItem)
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                      & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                                      & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue.Trim & "','" & psFechaFormato & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodOrigen & "')"
                CmdGlobal.ExecuteNonQuery()
            End If
        Next
    End Sub

    Private Sub Despacho_unoxuno(ByVal psSerieCodigo As String, ByVal psFecha As String, ByVal psDestino As String,
                             ByVal psTipoDestino As String, ByVal psMotivo As String, ByVal psUbicaCodigo As String,
                             ByVal psUbicaTipo As String, ByVal psCodArticulo As String)

        Dim ValorSys As String : ValorSys = FechaActual() + HoraActual() + Session("User")
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
        lblCodAlmacen = lblCodOrigen.Text
        psTipoOrigen = IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", ""))
        If psTipoOrigen = "1" Then psOrigenAlm = lblCodAlmacen
        If psTipoOrigen = "2" Then psOrigenCC = lblCodAlmacen
        If psTipoDestino = "1" Then psDestinoAlm = psDestino
        If psTipoDestino = "2" Then psDestinoCC = psDestino
        If psTipoDestino = "3" Then DesCodProveedor = psDestino
        If psTipoDestino = "4" Then DesCodCliente = psDestino
        If psTipoDestino = "5" Then DesCodPersona = psDestino
        If psTipoOrigen = "1" Then psUbicaAlm = lblCodAlmacen
        If psTipoOrigen = "2" Then psUbicaCC = lblCodAlmacen
        CodSalida = ""
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
        Dim RsRecep As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim objProceso As New clsInv_Procesos
        Dim psFechaFormato As String = ""
        Dim psHoraFormato As String = ""
        psFechaFormato = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        psHoraFormato = Mid(TxtHora.Text, 1, 2) + Mid(TxtHora.Text, 4, 2)

        psSerieNumerar = psSerieCodigo
        psCodArt = psCodArticulo
        psRecepcion = ""
        If psTipoOrigen = psTipoDestino And lblCodAlmacen = psCodDestino Then
        ElseIf psUbicaCodigo = "" And psUbicaTipo = "" Then
            CmdGlobal.CommandText = "SELECT MAX(RECEP_CODIGO) FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            RsRecep = CmdGlobal.ExecuteReader
            If RsRecep.HasRows Then
                While RsRecep.Read
                    psCodRecepcion = Nu(RsRecep(0)) + 1
                End While
            Else
                psCodRecepcion = 1
            End If
            RsRecep.Close()
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,  ALTIBI_CODIGO, RECEP_PROYECTO, RECEP_FECHA_REC , RECEP_HORA_REC, RECEP_TIPODOC,  " _
                              & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, " _
                              & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO, RECEP_CREADO_DESDE) " _
                              & " VALUES('" & Session("CodEmpresa") & "'," & psCodRecepcion & "," & lblCodOrigen.Text & ", '1','1', '" & psFechaFormato & "', '" & psHoraFormato & "', '7', " _
                              & " '" & psFechaFormato & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','',1,'2'," _
                              & " '0','" & ValorSys & "',1,1,0,0,'N','20','','1', '', '" & IIf(optOrigen.Checked = True, "1", IIf(optOrigen2.Checked = True, "2", "")) & "','I')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                  & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                  & "'" & Session("CodEmpresa") & "'," & psCodRecepcion & ",1," & psCodArt & ",1 ,1," _
                                  & " 0 ,0,1,'1','0','20','N')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                    CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & lblCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                          & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            Else
                CmdGlobal2.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                      & " VALUES(" & lblCodAlmacen & ",'" & psTipoOrigen & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                CmdGlobal2.ExecuteNonQuery()
            End If
            Rs.Close()

            'MOVIMIENTO GENERAL
            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
            RsRecep = CmdGlobal.ExecuteReader
            If RsRecep.HasRows Then
                While RsRecep.Read
                    lblNroMovimiento = Nu(RsRecep(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            RsRecep.Close()
            'Call Movimiento_Kardex(psCodRecepcion, "20", psCodArt, psTipoOrigen, lblCodAlmacen, "3", "", "POR INVENTARIO", "1", txtFechaRecep, 1)
            Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, "20", psCodArt, psTipoOrigen, lblCodAlmacen, psTipoDestino, psCodDestino, "Por Inventario", "1", TxtFecha.Text.Trim, 1)

            CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                  & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                  & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                                  & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaFormato & "','0','" & lblCodDespacho & "','" & psTipoDestino & "','" & psCodDestino & "')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & psTipoOrigen & "',UBICACT_CODIGO=" & lblCodAlmacen & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                  & " VALUES ('" & psSerieNumerar & "','" & psTipoOrigen & "'," & lblCodAlmacen & ",'20','0','" & ValorSys & "','" & Format(TxtFecha.Text, "yyyymmdd") & "','1','" & psCodRecepcion & "')"
            CmdGlobal.ExecuteNonQuery()
            objProceso.Guardar_UltimosMovimiento_paraGPS(Session("Ruta_Emp"), Session("CodEmpresa"), 0, FechaActual, IIf(optOrigen.Checked = True, "1", "2"), lblCodOrigen.Text, psTipoOrigen, lblCodAlmacen, psSerieNumerar, Session("User"))
            ' GoTo SalidaBien
        ElseIf psTipoOrigen <> psUbicaTipo Or lblCodAlmacen <> psUbicaCodigo Then
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
                                       & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                       & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "'," & HoraActual() & ",'" & Session("User") & "','" & psUbicaTipo & "'," _
                                       & " " & psUbicaCC & ", " & psUbicaAlm & ", " _
                                       & " '2', '0', 1, 1, 0, 1, " & psUbicaCodigo & ", '" & psFechaFormato & "', '" & psHoraFormato & "', '" & psMotivo & "', '" & ValorSys & "')"
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
                Rs.Close

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

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, DdlMotivo.Text, "2", TxtFecha.Text.Trim, 1)
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
                Rs.Close

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

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoOrigen, lblCodAlmacen, psUbicaTipo, psUbicaCodigo, DdlMotivo.Text, "1", TxtFecha.Text.Trim, 1)
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
                Rs.Close

                CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, " _
                                                  & " CECOSE_CODIGO_DESTINO, ALMACEN_CODIGO_DESTINO, OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                                  & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & psTipoOrigen & "'," _
                                                  & " " & psUbicaCC & "," & psUbicaAlm & ",'2','0',1,0,1,'" & psUbicaCodigo & "'," _
                                                  & " '" & psFechaFormato & "','" & HoraActual() & "','" & psMotivo & "')"
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
                Rs.Close
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
                Rs.Close

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
                Rs.Close

                'Call Movimiento_Kardex(lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, cboRMotivo, "2", txtFechaRecep, 1)
                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoOrigen, lblCodAlmacen, DdlMotivo.Text, "2", TxtFecha.Text.Trim, 1)

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
                Rs.Close

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
                Rs.Close

                'Call Movimiento_Kardex(lblCodDespacho, "20", psCodArt, "1", psCodDestino, psTipoOrigen, lblCodAlmacen, cboRMotivo, "1", txtFechaRecep, 1)

                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, lblCodAlmacen, DdlMotivo.Text, "1", TxtFecha.Text.Trim, 1)
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

    Protected Sub btnRedirectYes_Click(sender As Object, e As EventArgs) Handles btnRedirectYes.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        If Session("CodSalida") = "" Then Exit Sub
        Session("TipoGuia") = "1"
        Cn.Open() : CmdGlobal.Connection = Cn
        If optOrigen.Checked = True Then
            Session("TipoSalida") = "1"
            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_TIPO_DOC_SALIDA = '1' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & Session("CodSalida")
            CmdGlobal.ExecuteNonQuery()
        ElseIf optOrigen2.Checked = True Then
            Session("TipoSalida") = "2"
            CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_TIPO_DOC_SALIDA = '1' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & Session("CodSalida")
            CmdGlobal.ExecuteNonQuery()
        End If

        Session("ProcesoEjecutado") = Nothing
        Dim valor As String = Session("CodSalida")
        Dim valor2 As String = Session("TipoSalida")
        Session("PaginaViene") = "Inventario_SalidaIngresao_Alavez.aspx"

        ' Redireccionar a la página de destino pasando el valor como parámetro de consulta en la URL
        Response.Redirect("~/Inventario/Inventario_GenerarGuia.aspx?parametro=" & Server.UrlEncode(valor) & "&parametro2=" & Server.UrlEncode(valor2))

    End Sub

    Protected Sub btnRedirectNo_Click(sender As Object, e As EventArgs) Handles btnRedirectNo.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        If Session("CodSalida") = "" Then Exit Sub
        Session("TipoGuia") = "2"
        Cn.Open() : CmdGlobal.Connection = Cn
        If optOrigen2.Checked = True Then
            Session("TipoSalida") = "2"
            CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_TIPO_DOC_SALIDA = '2' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND OSAL_CODIGO=" & Session("CodSalida")
            CmdGlobal.ExecuteNonQuery()
        ElseIf optOrigen.Checked = True Then
            Session("TipoSalida") = "1"
            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_TIPO_DOC_SALIDA = '2' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & Session("CodSalida")
            CmdGlobal.ExecuteNonQuery()
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModalGuia').modal('hide');", True)

        Session("ProcesoEjecutado") = Nothing
        Dim valor As String = Session("CodSalida")
        Dim valor2 As String = Session("TipoSalida")
        Session("PaginaViene") = "Inventario_SalidaIngresao_Alavez.aspx"

        ' Redireccionar a la página de destino pasando el valor como parámetro de consulta en la URL
        Response.Redirect("~/Inventario/Inventario_GenerarGuia.aspx?parametro=" & Server.UrlEncode(valor) & "&parametro2=" & Server.UrlEncode(valor2))

    End Sub

    Private Sub GvListaArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaArticulos.RowCommand
        Dim dt As New DataTable
        Dim drt As DataRow

        dt.Columns.Add("COD_ARTICULO")
        dt.Columns.Add("ART_CODEQUIVA")
        dt.Columns.Add("ART_DESCRIPCION")
        dt.Columns.Add("ART_SKU")
        dt.Columns.Add("STOCK")
        dt.Columns.Add("CANTIDAD")
        dt.Columns.Add("SERIE_NRO")
        dt.Columns.Add("PLACA_NRO")
        dt.Columns.Add("TipoBien")
        dt.Columns.Add("TIPO_UBICACION")
        dt.Columns.Add("COD_ALMACEN")
        dt.Columns.Add("ALMACEN_NOMBRE")
        dt.Columns.Add("ubicact_codigo")
        dt.Columns.Add("ubicact_tipo")
        dt.Columns.Add("Serie_Numerar")
        dt.Columns.Add("ART_TIPO")

        For Each row As GridViewRow In GvListaArticulos.Rows
            drT = dt.NewRow()
            drT("COD_ARTICULO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("ART_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("STOCK") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("CANTIDAD") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("TipoBien") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(9).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("TIPO_UBICACION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(10).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("COD_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(11).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("ALMACEN_NOMBRE") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(12).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("ubicact_codigo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("ubicact_tipo") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(14).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("Serie_Numerar") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(15).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("ART_TIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            dt.Rows.Add(drt)
        Next

        If e.CommandName = "QuitarArt" Then
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            ' Asegúrate de que rowIndex esté dentro del rango válido de filas.
            If rowIndex >= 0 AndAlso rowIndex < dt.Rows.Count Then
                dt.Rows.RemoveAt(rowIndex) ' Elimina la fila del DataTable.
                GvListaArticulos.DataSource = dt ' Vuelve a vincular el GridView para reflejar el cambio.
                GvListaArticulos.DataBind()
            End If
        End If
    End Sub

    Private Sub BtnBuscaMarcaBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarcaBA.Click

    End Sub
    Private Sub btnBusArticulo_Click(sender As Object, e As EventArgs) Handles btnBusArticulo.Click
        Session("BuscarArticulo") = "Si"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
    End Sub

    Private Sub optIngreso_CheckedChanged(sender As Object, e As EventArgs) Handles optIngreso.CheckedChanged
        Articulo.Visible = True
        txtArtCodigo.Text = ""
        txtArtDescripcion.Text = ""
        Call Carga_Motivos()
    End Sub

    Private Sub optIngreso2_CheckedChanged(sender As Object, e As EventArgs) Handles optIngreso2.CheckedChanged

        Articulo.Visible = False
        txtArtCodigo.Text = ""
        txtArtDescripcion.Text = ""
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
        TituloPopupp.Text = "Busca Clasificaciones"
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)

    End Sub
    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalArticulo').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub
    Private Sub btnModalBuscarClas_Click(sender As Object, e As EventArgs) Handles btnModalBuscarClas.Click
        PopularRootLevel()
    End Sub

    Private Sub trvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles trvClasificacion.TreeNodePopulate
        Dim obj As New Cls_Clasificacion
        Dim dt As DataTable = obj.NumeroNodo(Session("Ruta_Emp"), CInt(e.Node.Value))
        Dim dbRow As DataRow = dt.Rows(0)
        Dim nivelPrincipal As Integer = CInt(dbRow(1).ToString)
        Dim nodo As Integer = CInt(dbRow(0).ToString) + 1
        Dim nodoAyuda As Integer = CInt(dbRow(0).ToString)
        Dim codigo As Integer = CInt(e.Node.Value)
        If nodo = 2 Then
            dt = obj.NodosHijos1(Session("Ruta_Emp"), nivelPrincipal, nodo)
            NodosPopulares(dt, e.Node.ChildNodes)
        Else
            dt = obj.NodosHijos(Session("Ruta_Emp"), nivelPrincipal, nodo, nodoAyuda, codigo)
            NodosPopulares(dt, e.Node.ChildNodes)
        End If
    End Sub

    Private Sub PopularRootLevel()
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))

        Dim objComand As New SqlCommand(" Select CLAS_CODIGO As CODIGO, " +
                                        " CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " +
                                        " (SELECT count(clas_codigo) " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL1=c1.CLAS_CODIGO and clas_cod_nivel = 2 ) as CountHijos " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c1  WHERE CLAS_COD_NIVEL=1 and clas_sys_est = '0' ORDER BY CLAS_NUMERACION", objConn)
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()

        da.Fill(dt)
        NodosPopulares(dt, trvClasificacion.Nodes)
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
    Private Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        trvClasificacion.SelectedNode.Selected = True
        TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
        Dim psNumero As Integer = 0
        lblCodClas.Text = trvClasificacion.SelectedValue
        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalArticulo').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub


End Class
