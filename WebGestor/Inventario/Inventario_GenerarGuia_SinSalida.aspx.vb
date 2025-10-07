Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Rectangle = iTextSharp.text.Rectangle
Imports iTextSharp.text.pdf.draw
Partial Class Inventario_Inventario_GenerarGuia_SinSalida
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Dim objEmp As New ModuloGeneral
    Dim CodSalida As String = ""
    Dim i As Long = 0
    Dim a As Long = i
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New Cls_Catalogo
            Dim dt As New DataTable

            Dim psconexion As String = Session("Ruta_Emp")

            TxtFecha.Text = FormatoFecha(FechaActual)
            TxtHora.Text = FormatoHoraSeg(HoraActual(True))
            TxtFechaTraslado.Text = FormatoFecha(FechaActual)
            TxtHoraTraslado.Text = FormatoHoraSeg(HoraActual(True))

            Call LlenaComboItem("TBOPC549", DdlModTransporte)
            Call LlenaComboItem("TBOPC222", DdlMotivoTraslado)
            Session("TipoDestinatario") = DdlDestinatario.SelectedValue
            Session("TipoRemitente") = DdlRemitente.SelectedValue
            BtnGuardar.Enabled = True

            If RbGuiaRem.Checked = True Then
                Session("TipoGuia") = "1"
                LblTitulo.Text = "Inventario - Genera Guía de Remisión"
                id_GuiaNumero.Visible = True
                id_DatosTransportista.Visible = True
                id_Transportista.Visible = True
                id_Vehiculo.Visible = True
                id_Chofer.Visible = True
                id_ModalidadTransporte.Visible = True
                id_MotivoTrasldo.Visible = True
                id_GuiaInterna.Visible = False
            Else
                Session("TipoGuia") = "2"
                LblTitulo.Text = "Inventario - Genera Guía Interna"
                id_GuiaInterna.Visible = True
                id_GuiaNumero.Visible = False
                id_DatosTransportista.Visible = False
                id_Transportista.Visible = False
                id_Vehiculo.Visible = False
                id_Chofer.Visible = False
                id_ModalidadTransporte.Visible = False
                id_MotivoTrasldo.Visible = False
            End If

            UpdatePanel1.Update()
            upSetSession.Update()
            UpdatePanel2.Update()
            UpdatePanel3.Update()
            UpdatePanel4.Update()
            UpdatePanel5.Update()
            Call LLenar_TipoaArticulo()
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
    Protected Sub TxtGuiaSerie_TextChanged(sender As Object, e As EventArgs) Handles TxtGuiaSerie.TextChanged
        Dim psCodGuia As String = ""
        If TxtGuiaSerie.Text.Trim = "" Then Exit Sub
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        CmdGlobal.CommandText = " SELECT DOC_CODIGO FROM TBDOCUMENTOS WHERE DOC_EMPRESA = '" & Session("CodEmpresa") & "' " _
                               & " AND DOC_SYS_EST ='0' AND DOC_AÑO='" & AñoActual(Session("CodEmpresa"), Session("Ruta_Emp")) & "' AND (DOC_CODIGO)='09' "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psCodGuia = Nu(Rs!DOC_CODIGO)
            End While
        End If
        Rs.Close()

        'verificar nro serie valida
        CmdGlobal.CommandText = "  SELECT GURESE_VALOR_INICIAL,GURESE_NUMERO FROM TBINV_GUIA_REMISION_SERIE WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                              & " AND GURESE_NUMERO LIKE '" & TxtGuiaSerie.Text & "%' AND GURESE_SYS_EST ='0' AND GURESE_TIPO_DOC='09'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                TxtGuiaSerie.Text = Nu(Rs!GURESE_NUMERO)
                TxtGuiaNumero.Text = Llenar_Ceros(Nz(Rs!GURESE_VALOR_INICIAL), 8)
            End While
        Else
            LblError.Text = "El Nro de Serie ingresada no es válido."
            TxtGuiaSerie.Text = ""
            TxtGuiaNumero.Text = ""
        End If
        Rs.Close()
        Cn.Close()

    End Sub
    Private Sub DdlDestinatario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDestinatario.SelectedIndexChanged
        TxtDestCodigo.Text = ""
        TxtDestDescripcion.Text = ""
        lblCodDestinatario.Text = ""
        Session("TipoDestinatario") = DdlDestinatario.SelectedValue
        BtnDestinatario.Enabled = True
        Call Carga_Motivos()
    End Sub

    Private Sub BtnDestinatario_Click(sender As Object, e As EventArgs) Handles BtnDestinatario.Click
        TituloPopup.Text = "Destinatario - Busqueda de " & DdlDestinatario.SelectedItem.Text
        Session("TipoBusqueda") = "Destinatario"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub BtnRemitente_Click(sender As Object, e As EventArgs) Handles BtnRemitente.Click
        TituloPopup.Text = "Destinatario - Busqueda de " & DdlRemitente.SelectedItem.Text
        Session("TipoBusqueda") = "Remitente"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
    End Sub

    Private Sub DdlRemitente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlRemitente.SelectedIndexChanged
        TxtRemCodigo.Text = ""
        txtRemDescripcion.Text = ""
        lblCodRemitente.Text = ""
        Session("TipoRemitente") = DdlRemitente.SelectedValue
        BtnDestinatario.Enabled = True
        Call Carga_Motivos()
    End Sub
    Private Sub Carga_Motivos()
        Dim psConexion As String = Session("Ruta_Emp")
        Dim Cn As New SqlConnection(psConexion)
        Dim cmdSql As New SqlCommand
        Dim Rs As SqlClient.SqlDataReader
        DdlMotivo.Items.Clear()
        Try

            Cn.Open()
            cmdSql.Connection = Cn
            If DdlRemitente.SelectedValue = "1" Then
                cmdSql.CommandText = " SELECT DISTINCT MAINSA_MOTIVO_TRASLADO, (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC217' AND ELEMEN_CODIGO = MAINSA_MOTIVO_TRASLADO) AS MOTIVO_TRASLADO" _
                               & " FROM TBINV_MATRIZ_INGRESOSALIDA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (MAINSA_TIPO_MOVIMIENTO = 'S') AND (MAINSA_UBICACION1 = '1') AND " _
                               & " (MAINSA_UBICACION2 = '" & DdlDestinatario.SelectedValue & "') ORDER BY MOTIVO_TRASLADO"
            ElseIf DdlRemitente.SelectedValue = "2" Then
                cmdSql.CommandText = "SELECT DISTINCT MAINSA_MOTIVO_TRASLADO, (SELECT ELEMEN_VALOR FROM BDGRUPOEMPRESAS.DBO.TBCELEMEN WHERE ELEMEN_TABLA = 'TBOPC217' AND ELEMEN_CODIGO = MAINSA_MOTIVO_TRASLADO) AS MOTIVO_TRASLADO" _
                               & " FROM TBINV_MATRIZ_INGRESOSALIDA WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (MAINSA_TIPO_MOVIMIENTO = 'S') AND (MAINSA_UBICACION1 = '2') AND (MAINSA_UBICACION2 = '" & DdlDestinatario.SelectedValue & "') ORDER BY MOTIVO_TRASLADO"
            End If
            Rs = cmdSql.ExecuteReader()
            DdlMotivo.DataSource = Rs
            DdlMotivo.DataTextField = "MOTIVO_TRASLADO"
            DdlMotivo.DataValueField = "MAINSA_MOTIVO_TRASLADO"
            DdlMotivo.DataBind()

            DdlMotivo.Items.Add("< Seleccionar >")
            DdlMotivo.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch Ex As Exception
            LblError.Text = Ex.Message
        Finally
            Cn.Close()
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Call Limpiar_Popup()
    End Sub
    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('hide');", True)
    End Sub

    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And Session("TipoBusqueda") = "Remitente" Then
            TxtRemCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtRemDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtPuntoPartida.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtRemUbigeo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodRemitente.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        ElseIf e.CommandName = "Aceptar" And Session("TipoBusqueda") = "Destinatario" Then
            TxtDestCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtDestDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtPuntoLlegada.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtDestUbigeo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodDestinatario.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodigo As Double = 0
        Dim objCont As New ClsCont_Listados
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""
        If (Session("TipoRemitente") = "1" And Session("TipoBusqueda") = "Remitente") Or (Session("TipoDestinatario") = "1" And Session("TipoBusqueda") = "Destinatario") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodigo = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaAlmacen(psconexion, Session("CodEmpresa"), psBusCodigo, descripcion)
        ElseIf (Session("TipoRemitente") = "2" And Session("TipoBusqueda") = "Remitente") Or (Session("TipoDestinatario") = "2" And Session("TipoBusqueda") = "Destinatario") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaCentroCosto(psconexion, Session("CodEmpresa"), psBusCodInterno, descripcion)
        ElseIf Session("TipoDestinatario") = "3" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "2")
        ElseIf Session("TipoDestinatario") = "6" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "1")
        ElseIf Session("TipoDestinatario") = "5" And Session("TipoBusqueda") = "Destino" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = objCont.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "5")
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub BtnTransporte_Click(sender As Object, e As EventArgs) Handles BtnTransporte.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('show');", True)
    End Sub

    Private Sub BtnVehiculo_Click(sender As Object, e As EventArgs) Handles BtnVehiculo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('show');", True)
    End Sub

    Private Sub BtnChofer_Click(sender As Object, e As EventArgs) Handles BtnChofer.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('show');", True)
    End Sub

    Private Sub BtnChoferBuscar_Click(sender As Object, e As EventArgs) Handles BtnChoferBuscar.Click
        Dim obj As New ClsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        psBusCodInterno = TxtBusChoferDni.Value.Trim.ToString
        descripcion = TxtBusChoferNombres.Value.Trim.ToString
        dt = obj.Cont_BusquedaChofer(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('show');", True)
        GvChofer.DataSource = dt
        GvChofer.DataBind()
    End Sub
    Protected Sub Limpiar_Popup_Chofer()
        TxtBusChoferDni.Value = ""
        TxtBusChoferNombres.Value = ""
        GvChofer.DataSource = Nothing
        GvChofer.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('hide');", True)
    End Sub

    Private Sub BtnChoferCerrar_Click(sender As Object, e As EventArgs) Handles BtnChoferCerrar.Click
        Call Limpiar_Popup_Chofer()
    End Sub

    Private Sub GvChofer_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvChofer.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtChoferDNI.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtChoferNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtLicencia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodChofer.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Chofer()

    End Sub
    Private Sub BtnVehiculoBuscar_Click(sender As Object, e As EventArgs) Handles BtnVehiculoBuscar.Click
        Dim obj As New ClsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        psBusCodInterno = TxtBusPlaca.Value.Trim.ToString
        descripcion = TxtBusMarca.Value.Trim.ToString
        dt = obj.Cont_BusquedaVehiculo(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('show');", True)
        GvVehiculo.DataSource = dt
        GvVehiculo.DataBind()
    End Sub

    Protected Sub Limpiar_Popup_Vehiculo()
        TxtBusPlaca.Value = ""
        TxtBusMarca.Value = ""
        GvVehiculo.DataSource = Nothing
        GvVehiculo.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('hide');", True)
    End Sub

    Private Sub BtnVehiculoCerrars_Click(sender As Object, e As EventArgs) Handles BtnVehiculoCerrar.Click
        Call Limpiar_Popup_Vehiculo()
    End Sub

    Private Sub GvVehiculo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvVehiculo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtNroPlaca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtMarca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtconfVehicular.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtRucTrasnportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtRazonTransportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtCertInscripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodVehiculo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Vehiculo()

    End Sub

    Private Sub BtnBusTransporte_Click(sender As Object, e As EventArgs) Handles BtnBusTransporte.Click
        Dim obj As New ClsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        If TxtBusTransRUC.Value.ToString <> "" Then psBusCodInterno = TxtBusTransRUC.Value
        descripcion = TxtBusTransRazon.Value.Trim.ToString
        dt = obj.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "4")

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('show');", True)
        GvTransporte.DataSource = dt
        GvTransporte.DataBind()
    End Sub

    Protected Sub Limpiar_Popup_Transporte()
        TxtBusTransRazon.Value = ""
        TxtBusTransRUC.Value = ""
        GvTransporte.DataSource = Nothing
        GvTransporte.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('hide');", True)
    End Sub

    Private Sub BtnCancelarTrans_Click(sender As Object, e As EventArgs) Handles BtnCancelarTrans.Click
        Call Limpiar_Popup_Transporte()
    End Sub

    Private Sub GvTransporte_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvTransporte.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtRucTrasnportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtRazonTransportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtCertInscripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodTrasporte.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Transporte()

    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2

        Dim ValorSys As String = ""
        Dim dtArt As New DataTable
        Dim drT As DataRow

        dtArt.Columns.Add("ART_CODIGO")
        dtArt.Columns.Add("ART_DESCRIPCION")
        dtArt.Columns.Add("ART_CODEQUIVA")
        dtArt.Columns.Add("ART_TIPO")
        dtArt.Columns.Add("CANT")
        dtArt.Columns.Add("ART_SKU")

        For Each row As GridViewRow In GvListaArticulos.Rows
            Dim txtValor As System.Web.UI.WebControls.TextBox = CType(row.FindControl("txtCant"), System.Web.UI.WebControls.TextBox)
            ' Aquí puedes acceder y manipular el valor del TextBox
            Dim valor As String = txtValor.Text

            drT = dtArt.NewRow()
            drT("ART_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("ART_TIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            If valor <> "" Then
                drT("CANT") = valor
            Else
                drT("CANT") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            End If
            drT("ART_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            dtArt.Rows.Add(drT)
        Next
        drT = dtArt.NewRow()
        GvListaArticulos.DataSource = dtArt
        GvListaArticulos.DataBind()

        Dim CodAlmacenRem As String = ""
        Dim CodSeccionRem As String = ""
        Dim CodAlmacenDes As String = ""
        Dim CodSeccionDes As String = ""
        Dim CodProvee As String = ""
        Dim CodCliente As String = ""
        Dim SalidaAlmacen As String = ""
        Dim SalidaCC As String = ""
        Dim psCantBien As Double = 0
        Dim cantAcc As Double = 0
        Dim cantItemAcc As Double = 0
        Dim psCodart As String = ""
        Dim cantSeries As Double = 0
        For i = 0 To GvListaArticulos.Rows.Count - 1
            cantAcc = cantAcc + Nz(GvListaArticulos.Rows(i).Cells(5).Text.Trim)
            cantItemAcc = cantItemAcc + 1
        Next

        If Session("TipoGuia") = "1" And Trim(TxtGuiaSerie.Text) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar la serie de la guía.');", True)
        ElseIf Session("TipoGuia") = "1" And Trim(TxtGuiaNumero.Text) = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el numero de la guía.');", True)
        ElseIf lblCodRemitente.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el remitente.');", True)
        ElseIf lblCodDestinatario.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el destinatario.');", True)
        ElseIf Session("TipoGuia") = "2" And TxtQuienRetira.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar persona quien recibe.');", True)
        ElseIf Session("TipoGuia") = "2" And TxtQuienRecibe.Text.Trim = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar persona quien entrega.');", True)
        ElseIf DdlMotivo.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, seleccionar el motivo de la salida.');", True)
        ElseIf cantSeries = 0 And cantAcc = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, ingresar el detalle de la guía.');", True)
        Else

            BtnGuardar.Enabled = False
            Dim CodPersona As String = ""
            Dim CodEquipo As String = ""
            ValorSys = FechaActual() & HoraActual() & Session("User")

            CodAlmacenRem = "NULL"
            CodSeccionRem = "NULL"
            CodPersona = "NULL"
            CodAlmacenDes = "NULL"
            CodSeccionDes = "NULL"
            CodCliente = "NULL"
            CodProvee = "NULL"
            CodEquipo = "NULL"

            Dim CantLineaGuia As Integer : CantLineaGuia = 34
            Dim psCantLinea As Double = 0
            CantLineaGuia = 24
            Dim iGuia As Long = 0
            Dim psCantGuia As Integer = 0
            Dim psCantSobra As Integer = 0
            Dim pdCantDetalle As Integer = 0
            Dim CodCurrier As String = ""
            Dim aa As Integer = 0
            Dim psGuiaSerie As Integer : psGuiaSerie = 0
            Dim psGuiaAcc As Integer : psGuiaAcc = 0
            psCantLinea = cantSeries + cantItemAcc
            psCantGuia = 1
            Dim psCodGuiaVarias As String = ""
            If psCantLinea > CantLineaGuia Then
                psCantGuia = psCantLinea / CantLineaGuia
                psCantSobra = psCantLinea - (CantLineaGuia * psCantGuia)
                If psCantSobra > 0 Then psCantGuia = psCantGuia + 1
            End If

            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se generará " & psCantGuia & " guías.');", True)

            pdCantDetalle = 0
            Dim a As Long : a = 0
            Dim pdCantDetalleGuia As Integer
            pdCantDetalleGuia = 0
            If DdlRemitente.SelectedValue = 1 Then 'S/ALMACEN
                CodAlmacenRem = lblCodRemitente.Text
            Else 'S/CC
                CodSeccionRem = lblCodRemitente.Text
            End If
            If DdlDestinatario.SelectedValue = 1 Then 'S/ALMACEN
                CodAlmacenDes = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 2 Then 'S/CC
                CodSeccionDes = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 6 Then 'cLIENTE
                CodCliente = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 5 Then 'cLIENTE
                CodPersona = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 3 Then
                CodProvee = lblCodDestinatario.Text
            ElseIf DdlDestinatario.SelectedValue = 3 Then
                CodEquipo = lblCodDestinatario.Text
            End If

            Dim psFechaEmision As String = ""
            Dim psHoraEmision As String = ""
            psFechaEmision = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
            psHoraEmision = Mid(TxtHora.Text, 1, 2) + Mid(TxtHora.Text, 4, 2) + Mid(TxtHora.Text, 7, 2)

            Dim psFechaTraslado As String = ""
            Dim psHoraTraslado As String = ""
            psFechaTraslado = Mid(TxtFechaTraslado.Text, 7, 4) + Mid(TxtFechaTraslado.Text, 4, 2) + Mid(TxtFechaTraslado.Text, 1, 2)
            psHoraTraslado = Mid(TxtHoraTraslado.Text, 1, 2) + Mid(TxtHoraTraslado.Text, 4, 2) + Mid(TxtHoraTraslado.Text, 7, 2)


            For a = 1 To psCantGuia
                'tipo de guia 1 guia remision 2 guia interna
                If Session("TipoGuia") = "1" Then

                    CmdGlobal.CommandText = "SELECT GUIREM_CODIGO FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " WHERE GUIREM_SERIE='" & Trim(TxtGuiaSerie.Text) & "' AND GUIREM_NUMERO='" & Trim(TxtGuiaNumero.Text) & "'"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('La serie y numero ingresado ya existe.');", True)
                            Exit Sub
                        End While
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = "SELECT MAX(GUIREM_CODIGO) FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            Session("CodGuia") = Llenar_Ceros(Nz(Rs(0)) + 1, 8)
                        End While
                    Else
                        Session("CodGuia") = "00000001"
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUIREM_TIPO, GUIREM_SERIE,GUIREM_NUMERO, GUIREM_SYS_EST,GUIREM_SYS_CRE,GUIREM_RECEPCIONADA,GUIREM_ESTADO) " _
                                               & " VALUES(" & Session("CodGuia") & ",'" & Session("TipoGuia") & "','" & Trim(TxtGuiaSerie.Text) & "','" & Trim(TxtGuiaNumero.Text) & "','0','" & ValorSys & "','0','0')"
                    CmdGlobal.ExecuteNonQuery()

                    CodCurrier = "NULL"
                    Session("NroGuiaRem") = Trim(TxtGuiaSerie.Text) & "-" & Trim(TxtGuiaNumero.Text)

                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_SERIE SET GURESE_VALOR_INICIAL = " & Val(TxtGuiaNumero.Text) + 1 & "  WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND GURESE_NUMERO = '" & TxtGuiaSerie.Text & "'AND GURESE_TIPO_DOC='09'"
                    CmdGlobal.ExecuteNonQuery()

                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                                & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & ",GUIREM_CURRIER = " & CodCurrier & ",GUIREM_ESTADO_ENTREGA='1',GUIREM_ESTADO_SITUACION='2'," _
                                & " GUIREM_DIRECCION_PARTIDA_UBIGEO='" & TxtRemUbigeo.Text & "',GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "',GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "',ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & ",EQUIPO_CODIGO_DESTINATARIO=" & CodEquipo & "," _
                                & " PERSONA_CODIGO_DESTINATARIO=" & CodPersona & ",CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ",CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",    PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ", GUIREM_NOMBRE_DESTINATARIO = '" & Trim(TxtDestDescripcion.Text) & "',GUIREM_DIRECCION_LLEGADA_UBIGEO='" & TxtDestUbigeo.Text & "', GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text) & "', TRANSPORTISTA_RUC='" & TxtRucTrasnportista.Text.Trim & "',TRANSPORTISTA_RAZONSOCIAL='" & TxtRazonTransportista.Text.Trim & "'," _
                                & " VEHICU_PLACA='" & TxtNroPlaca.Text & "',VEHICU_MARCA='" & TxtMarca.Text & "',VEHICU_CERT_INSCP='" & TxtCertInscripcion.Text & "',CHOFER_DNI='" & TxtChoferDNI.Text & "',CHOFER_NOMBRES='" & TxtChoferNombre.Text & "',CHOFER_LICENCIA='" & TxtLicencia.Text & "', " _
                                & " GUIREM_MOTIVO_DESCRIPCION='" & Trim(Solo_Texto(TxtMotivoDescripcion.Text)) & "' ,GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                                & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_PERSONA_RECIBE = NULL ,GUIREM_PERSONA_RETIRA=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                    If DdlModTransporte.SelectedValue <> "< Seleccionar >" Then
                        CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  TRANSPORTISTA_MODALIDAD='" & DdlModTransporte.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    If DdlMotivoTraslado.SelectedValue <> "< Seleccionar >" Then
                        CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_MOTIVO_TRASLADO='" & DdlMotivoTraslado.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_BULTO='" & TxtNroBulto.Text & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()

                Else
                    CmdGlobal.CommandText = "SELECT MAX(GUIREM_CODIGO) FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            Session("CodGuia") = Llenar_Ceros(Nz(Rs(0)) + 1, 8)
                        End While
                    Else
                        Session("CodGuia") = "00000001"
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUIREM_TIPO, GUIREM_SERIE,GUIREM_NUMERO, GUIREM_SYS_EST,GUIREM_SYS_CRE,GUIREM_RECEPCIONADA,GUIREM_ESTADO) " _
                                               & " VALUES(" & Session("CodGuia") & ",'" & Session("TipoGuia") & "',NULL,NULL,'0','" & ValorSys & "','0','0')"
                    CmdGlobal.ExecuteNonQuery()

                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                                & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",  CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & "," _
                                & " GUIREM_DIRECCION_PARTIDA_UBIGEO='" & TxtRemUbigeo.Text & "',GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "',GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "',ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & ",EQUIPO_CODIGO_DESTINATARIO=" & CodEquipo & "," _
                                & " PERSONA_CODIGO_DESTINATARIO=" & CodPersona & ",CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ",CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",   PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ", GUIREM_NOMBRE_DESTINATARIO = '" & Trim(TxtDestDescripcion.Text) & "' , GUIREM_DIRECCION_LLEGADA_UBIGEO='" & TxtDestUbigeo.Text & "',GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text) & "', TRANSPORTISTA_RUC=NULL,TRANSPORTISTA_RAZONSOCIAL=NULL," _
                                & " VEHICU_PLACA=NULL,VEHICU_MARCA=NULL,VEHICU_CERT_INSCP=NULL,CHOFER_DNI=NULL,CHOFER_NOMBRES=NULL,CHOFER_LICENCIA=NULL,GUIREM_MOTIVO_DESCRIPCION=NULL , GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                                & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_PERSONA_RECIBE = '" & Trim(TxtQuienRecibe.Text) & "' , GUIREM_PERSONA_RETIRA='" & Trim(TxtQuienRetira.Text) & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                    CantLineaGuia = 30

                End If

                iGuia = 0

                If a = 1 Then
                    iGuia = 0
                Else
                    aa = a - 1
                    iGuia = (CantLineaGuia * aa) + 1
                End If

                psCantBien = 0
                pdCantDetalle = 0
                If psGuiaAcc <= dtArt.Rows.Count - 1 Then
                    If pdCantDetalleGuia < CantLineaGuia Then
                        iGuia = psGuiaAcc
                        For Each drow As DataRow In dtArt.Rows

                            psCantBien = Nz(drow("Cant"))
                            psCodart = Nu(drow("ART_CODIGO"))
                            pdCantDetalle = pdCantDetalle + 1
                            pdCantDetalleGuia = pdCantDetalleGuia + 1
                            SalidaCC = "NULL"
                            SalidaAlmacen = "NULL"
                            CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO,OSAL_CODIGO,ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                                  & " VALUES(" & Session("CodGuia") & "," & pdCantDetalle & "," & SalidaAlmacen & "," & SalidaCC & "," & psCodart & "," & psCantBien & ")"
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_SINSERIE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO,OSAL_CODIGO, ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                                  & " VALUES(" & Session("CodGuia") & "," & pdCantDetalle & "," & SalidaAlmacen & "," & SalidaCC & "," & psCodart & "," & psCantBien & ")"
                            CmdGlobal.ExecuteNonQuery()
                            psGuiaAcc = psGuiaAcc + 1
                            If pdCantDetalle = CantLineaGuia And Session("TipoGuia") = "2" Then Exit For
                        Next
                    End If
                End If

                TxtGuiaNumero.Text = Llenar_Ceros(Val(TxtGuiaNumero.Text) + 1, 8)
                If psCodGuiaVarias <> "" Then psCodGuiaVarias = psCodGuiaVarias & " ,"
                psCodGuiaVarias = psCodGuiaVarias & Session("CodGuia")

            Next

            If Session("TipoGuia") = "1" Then 'con transportista,chofer y vehiculo

                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                            & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",  CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & ", GUIREM_CURRIER = " & CodCurrier & ",GUIREM_ESTADO_ENTREGA='1',GUIREM_ESTADO_SITUACION='2'," _
                            & " GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "' ,     GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "', ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & "," _
                            & " CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ", CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",   PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ",   GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text.Trim) & "', TRANSPORTISTA_RUC='" & TxtRucTrasnportista.Text.Trim & "',TRANSPORTISTA_RAZONSOCIAL='" & TxtRazonTransportista.Text.Trim & "'," _
                            & " VEHICU_PLACA='" & TxtNroPlaca.Text.Trim & "',VEHICU_MARCA='" & TxtMarca.Text.Trim & "',VEHICU_CERT_INSCP='" & TxtCertInscripcion.Text.Trim & "',CHOFER_DNI='" & TxtChoferDNI.Text & "',CHOFER_NOMBRES='" & TxtChoferNombre.Text & "',CHOFER_LICENCIA='" & TxtLicencia.Text & "', VEHI_CONFIGURACION = '" & TxtconfVehicular.Text & "', " _
                            & " GUIREM_MOTIVO_DESCRIPCION='" & Trim(Solo_Texto(TxtMotivoDescripcion.Text)) & "' ,GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                            & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_PERSONA_RECIBE = NULL,GUIREM_PERSONA_RETIRA=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()

                If DdlModTransporte.SelectedValue <> "< Seleccionar >" Then
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  TRANSPORTISTA_MODALIDAD='" & DdlModTransporte.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                End If

                If DdlMotivoTraslado.SelectedValue <> "< Seleccionar >" Then
                    CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_MOTIVO_TRASLADO='" & DdlMotivoTraslado.SelectedValue & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                    CmdGlobal.ExecuteNonQuery()
                End If

                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_BULTO='" & TxtNroBulto.Text & "' WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()

            Else 'sin transportista,chofer y vehiculo

                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & psFechaEmision & "', GUIREM_HORA='" & psHoraEmision & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & psFechaTraslado & "', GUIREM_HORA_TRASLADO='" & psHoraTraslado & "', " _
                        & " GUIREM_TIPO_REMITENTE='" & DdlRemitente.SelectedValue & "', ALMACEN_CODIGO_REMITENTE=" & CodAlmacenRem & ",  CECOSE_CODIGO_REMITENTE = " & CodSeccionRem & "," _
                        & " GUIREM_DIRECCION_PARTIDA ='" & Trim(TxtPuntoPartida.Text) & "',     GUIREM_TIPO_DESTINATARIO='" & DdlDestinatario.SelectedValue & "',     ALMACEN_CODIGO_DESTINATARIO=" & CodAlmacenDes & "," _
                        & " CECOSE_CODIGO_DESTINATARIO=" & CodSeccionDes & ",  CLIENTE_CODIGO_DESTINATARIO=" & CodCliente & ",  PROVEEDOR_CODIGO_DESTINATARIO=" & CodProvee & ",   GUIREM_DIRECCION_LLEGADA ='" & Trim(TxtPuntoLlegada.Text) & "', TRANSPORTISTA_RUC=NULL,TRANSPORTISTA_RAZONSOCIAL=NULL," _
                        & " VEHICU_PLACA=NULL,VEHICU_MARCA=NULL,VEHICU_CERT_INSCP=NULL,CHOFER_DNI=NULL,CHOFER_NOMBRES=NULL,CHOFER_LICENCIA=NULL,GUIREM_MOTIVO_DESCRIPCION=NULL , GUIREM_NUMERAC_COMPROB_PAGO=NULL," _
                        & " GUIREM_FECHA_COMPROB_PAGO=NULL , GUIREM_OBSERVACION='" & Solo_Texto(TxtObsGuia.Text) & "', GUIREM_SYS_MOD='" & ValorSys & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  GUIREM_PERSONA_RECIBE = '" & Trim(TxtQuienRecibe.Text) & "' , GUIREM_PERSONA_RETIRA='" & Trim(TxtQuienRetira.Text) & "',GUIREM_BULTO=NULL WHERE GUIREM_CODIGO = " & Session("CodGuia")
                CmdGlobal.ExecuteNonQuery()

            End If

            GenerarSalidaFinal(Session("CodGuia"))

            Dim psMensaje As String = ""

            If Session("TipoGuia") = "1" Then
                psMensaje = "Guía de Remisión N° " & Session("NroGuiaRem") & " Generada."
                btnImprimir.Enabled = False
            Else
                btnImprimir.Enabled = True
                psMensaje = "Guía Interna N° " & Session("CodGuia") & " Generada."
            End If

            Session("ProcesoEjecutado") = True
            LblTituloModal.Text = psMensaje
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)

        End If
    End Sub

    Private Sub GenerarSalidaFinal(ByVal psCodGuia As String)

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim RsRecep As SqlDataReader
        Dim oFuncInv As New clsInv_Procesos

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
        psTipoOrigen = DdlRemitente.SelectedValue
        psTipoDestino = DdlDestinatario.SelectedValue
        If psTipoDestino = "1" Then psDestinoAlm = lblCodDestinatario.Text
        If psTipoDestino = "2" Then psDestinoCC = lblCodDestinatario.Text
        If psTipoDestino = "3" Then DesCodProveedor = lblCodDestinatario.Text
        If psTipoDestino = "6" Then DesCodCliente = lblCodDestinatario.Text
        If psTipoDestino = "5" Then DesCodPersona = lblCodDestinatario.Text
        If lblCodDestinatario.Text <> "" Then psCodDestino = lblCodDestinatario.Text
        If lblCodRemitente.Text <> "" Then psCodOrigen = lblCodRemitente.Text
        StockAc = 0
        CodSalida = ""
        Dim psRecepcion As String : psRecepcion = ""
        i = 0
        Dim psProveedor As String = ""
        Dim psCodRecepcion As String = ""

        cant = 0
        Dim psTipoart As String = ""
        Dim psEstado As String = "2"
        For i = 0 To GvListaArticulos.Rows.Count - 1
            cant = cant + Nz(GvListaArticulos.Rows(i).Cells(5).Text.Trim)
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
                           & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC,GUIREM_CODIGO,DESP_TIPO_DOC_SALIDA) " _
                           & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "'," & HoraActual() & ",'" & Session("User") & "','" & psTipoDestino & "'," _
                           & " " & psDestinoAlm & "," & psDestinoCC & ", " & DesCodPersona & ", " & DesCodProveedor & ", " & DesCodCliente & ", " _
                           & " '" & psEstado & "','0'," & cant & "," & cant & "," & cant & ",0," & psCodOrigen & ", '" & psFechaFormato & "','" & psHoraFormato & "','" & DdlMotivo.SelectedValue & "','" & ValorSys & "'," & psCodGuia & ",'" & IIf(RbGuiaInt.Checked = True, "2", "1") & "')"
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
                        & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL,OSAL_SYS_REC,GUIREM_CODIGO,OSAL_TIPO_DOC_SALIDA) " _
                        & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & psTipoDestino & "'," _
                        & " " & psDestinoAlm & "," & psDestinoCC & ", " & DesCodProveedor & ", " & DesCodCliente & ", " & DesCodPersona & ", " _
                        & " '" & psEstado & "','0'," & cant & ",0," & cant & ",'" & psCodOrigen & "'," _
                        & " '" & psFechaFormato & "','" & HoraActual() & "','" & DdlMotivo.SelectedValue & "', '" & ValorSys & "'," & psCodGuia & ",'" & IIf(RbGuiaInt.Checked = True, "2", "1") & "')"
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
        Rs.Close()
        CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                      & " VALUES ('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & psCodDespacho & ",'" & DdlMotivo.SelectedValue & "','" & psTipoOrigen & "'," & lblCodRemitente.Text & ", " _
                      & " '" & psTipoDestino & "'," & lblCodDestinatario.Text & ",'" & FechaActual() & "','" & HoraActual() & "','" & psEstado & "','0','" & psFechaFormato & "')"
        CmdGlobal.ExecuteNonQuery()
        CodSalida = psCodDespacho
        Dim psCantItem As Double : psCantItem = 0
        Dim psItemSerie As Integer : psItemSerie = 0
        Dim psItemAcc As Integer : psItemAcc = 0

        For i = 0 To GvListaArticulos.Rows.Count - 1
            psSerieNumerar = ""
            'psSerieNumerar = Replace(GvListaArticulos.Rows(i).Cells(14).Text.Trim, "&nbsp;", "")
            psCodArt = GvListaArticulos.Rows(i).Cells(1).Text.Trim
            psTipoart = CDbl(Nz(GvListaArticulos.Rows(i).Cells(4).Text.Trim))
            psCantItem = CDbl(Nz(GvListaArticulos.Rows(i).Cells(5).Text.Trim))
            If psTipoOrigen = "1" Then
                '-----------------------SALIDA DE ALMACEN
                If psTipoart = "73" Or psTipoart = "64" Or psTipoart = "88" Then
                    psItemSerie = psItemSerie + 1
                    CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM,  ALLSALD_SYS_REG, " _
                                  & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & ",1,'" & ValorSys & "'," _
                                  & " '" & ValorSys & "','2','1','0')"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM,  DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ,DESPD_SYS_REC, DESPD_MODO_RECIBIDO) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & "," & psItemSerie & ",'S','0'," & psCodArt & ",'" & DdlMotivo.SelectedValue & "','S','" & ValorSys & "','M')"
                    CmdGlobal.ExecuteNonQuery()
                Else
                    psItemAcc = psItemAcc + 1
                    CmdGlobal.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET_SINSERIE( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM,ARTICULO_CODIGO,DESPD_CANTXDESP,DESPD_CANT_DESP,DESPD_CANT_REC,DESPD_CANT_FALT_REC,DESPD_SYS_EST,DESPD_MOTIVO) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & "," & psItemAcc & "," & psCodArt & "," & psCantItem & "," & psCantItem & ",0," & psCantItem & ",'0','" & DdlMotivo.SelectedValue & "')"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                                  & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & psItemAcc & "," & psCodArt & "," & psCantItem & ",0," _
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


                Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, DdlMotivo.Text, "2", TxtFecha.Text.Trim, psCantItem)
                CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                              & " VALUES ('" & Session("CodEmpresa") & "'," & lblNroMovimiento & ",'2','" & psTipoOrigen & "','" & psCodOrigen & "', " _
                              & " " & psCodArt & "," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue & "','" & Format(TxtFecha.Text, "yyyymmdd") & "','0','" & psCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                CmdGlobal.ExecuteNonQuery()
                ''--------------------------recepcion en ccosto O ALMACEN
                ''STOCK
                ''CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                ''        & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                ''Rs = CmdGlobal.ExecuteReader
                ''If Rs.HasRows Then
                ''    While Rs.Read
                ''        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + psCantItem
                ''        CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                ''                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                ''        CmdGlobal2.ExecuteNonQuery()
                ''    End While
                ''Else
                ''    CmdGlobal2.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                ''                  & " VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & "," & psCantItem & ",'0','" & Session("CodEmpresa") & "')"
                ''    CmdGlobal2.ExecuteNonQuery()
                ''End If
                ''Rs.Close()

                ''MOVIMIENTO GENERAL
                ''CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                ''Rs = CmdGlobal.ExecuteReader
                ''If Rs.HasRows Then
                ''    While Rs.Read
                ''        lblNroMovimiento = Nz(Rs(0)) + 1
                ''    End While
                ''Else
                ''    lblNroMovimiento = 1
                ''End If
                ''Rs.Close()

                ''Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, DdlMotivo.Text, "1", TxtFecha.Text.Trim, psCantItem)
                ''CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                ''              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                ''              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                ''              & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue & "','" & Format(TxtFecha.Text, "yyyymmdd") & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodOrigen & "')"
                ''CmdGlobal.ExecuteNonQuery()
            ElseIf psTipoOrigen = "2" Then 'SALIDA DE CENTRO DE COSTO
                If psTipoart = "73" Or psTipoart = "64" Or psTipoart = "88" Then
                    psItemSerie = psItemSerie + psCantItem
                    CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, OSALD_ARTICULO_CODIGO, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO,OSALD_SYS_REC ,OSALD_MODO_RECIBIDO) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",1," & psCodArt & ",'S','S','0','" & DdlMotivo.SelectedValue & "','" & ValorSys & "','A')"
                    CmdGlobal.ExecuteNonQuery()
                Else
                    psItemAcc = psItemAcc + psCantItem
                    CmdGlobal.CommandText = "INSERT TBINV_CCOSTO_SALIDA_DET_SINSERIE(EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN,ARTICULO_CODIGO,OSALD_CANT_ENV,OSALD_CANT_REC,OSALD_CANT_FALT_REC ,OSALD_SYS_EST,OSALD_MOTIVO,OSALD_FUNCION) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & "," & psItemAcc & "," & psCodArt & "," & psCantItem & ",0," & psCantItem & ",'0','" & DdlMotivo.SelectedValue & "','')"
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
                              & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue & "','" & psFechaFormato & "','0','" & psCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
                CmdGlobal.ExecuteNonQuery()
                '--------------------------recepcion en ccosto O ALMACEN
                'STOCK
                'CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                '                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                'Rs = CmdGlobal.ExecuteReader
                'If Rs.HasRows Then
                '    While Rs.Read
                '        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + psCantItem
                '        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                '                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                '        CmdGlobal2.ExecuteNonQuery()
                '    End While
                'Else
                '    CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                '                  & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & "," & psCantItem & ",'0','" & Session("CodEmpresa") & "')"
                '    CmdGlobal2.ExecuteNonQuery()
                'End If
                'Rs.Close()

                ''MOVIMIENTO GENERAL
                'CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                'Rs = CmdGlobal.ExecuteReader
                'If Rs.HasRows Then
                '    While Rs.Read
                '        lblNroMovimiento = Nz(Rs(0)) + 1
                '    End While
                'Else
                '    lblNroMovimiento = 1
                'End If
                'Rs.Close()

                ''Call Movimiento_Kardex(psCodDespacho, cboRMotivo.ItemData(cboRMotivo.ListIndex), psCodArt, "1", psCodDestino, psTipoOrigen, psCodOrigen, cboRMotivo, "1", txtFechaRecep, psCantItem)

                'Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodDespacho, DdlMotivo.SelectedValue, psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, DdlMotivo.Text, "1", TxtFecha.Text.Trim, psCantItem)
                'CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                '                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                '                      & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                '                      & " '" & psCodArt & "'," & psCantItem & ",'" & ValorSys & "','3','" & DdlMotivo.SelectedValue & "','" & psFechaFormato & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodOrigen & "')"
                'CmdGlobal.ExecuteNonQuery()
            End If
        Next

        If psTipoOrigen = "1" And psItemSerie = 0 Then
            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_ESTADO = '3' WHERE DESP_CODIGO = " & psCodDespacho
            CmdGlobal.ExecuteNonQuery()
        ElseIf psTipoOrigen = "2" And psItemSerie = 0 Then '
            CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_ESTADO = '3' WHERE OSAL_CODIGO = " & psCodDespacho
            CmdGlobal.ExecuteNonQuery()
        End If

    End Sub


    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        Response.Redirect("~/Inventario/Inventario_GenerarGuia_SinSalida.aspx")
    End Sub
    Protected Sub btnRedirectYes_Click(sender As Object, e As EventArgs) Handles btnRedirectYes.Click
        Response.Redirect("~/Inventario/Inventario_GenerarGuia_SinSalida.aspx")
    End Sub
    Protected Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If Session("TipoGuia") = "2" Then
            Dim numeroGuia As String = ""
            Dim fechaEmision As String = ""
            Dim fechaTraslado As String = ""
            Dim HoraEmision As String = ""
            Dim psPtoPartida As String = ""
            Dim psPtoLlegada As String = ""
            Dim motivo As String = ""
            Dim pstextoQR As String = ""
            Dim psDestinatario As String = ""
            Dim psEmpresa As String = ""
            Dim psModalidadTransporte As String = ""
            Dim psUnidadMedida As String = ""
            Dim psPesoBruto As String = ""
            Dim psNroBultos As String = ""
            Dim psTransportista As String = ""
            Dim psTransportistaRUC As String = ""
            Dim psNroRegistroMTC As String = ""
            Dim psRucDestinatario As String = ""
            Dim psUbigeoPartida As String = ""
            Dim psUbigeoLlegada As String = ""
            Dim EmpresaRuc As String = ""
            Dim EmpresaNombre As String = ""
            Dim psRemitente As String = "REMITENTE"
            Dim dt As New DataTable
            Dim dtEmp As New DataTable
            Dim psPeriodo As String = ""
            Dim psCodexRemGuia As String = ""
            Dim psCodexDesc As String = ""
            Dim psGuiaSerie As String = ""
            Dim psRetira As String = ""
            Dim psRecibe As String = ""
            Dim psObs As String = ""

            dt = obj.Lista_GuiaRemision_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), Session("CodGuia"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    psGuiaSerie = Nu(dr("GUIREM_SERIE")) + "-" + Nu(dr("Guia_Numeracion"))
                    numeroGuia = "Nro. T" & Nu(dr("GUIREM_SERIE")) + "-" + Nu(dr("Guia_Numeracion"))
                    fechaEmision = Nu(dr("Fecha_Guia"))
                    psPtoPartida = Nu(dr("GUIREM_DIRECCION_PARTIDA"))
                    psPtoLlegada = Nu(dr("GUIREM_DIRECCION_LLEGADA"))
                    pstextoQR = Nu(dr("GUIREM_QR"))
                    motivo = Nu(dr("MOTIVO_TRASLADO"))
                    psModalidadTransporte = Nu(dr("ModalidadTransporte"))
                    psDestinatario = Nu(dr("DESTINATARIO_NOMBRE"))
                    psUnidadMedida = Nu(dr("GUIREM_UNIDAD_MEDIDA_BRUTO"))
                    psPesoBruto = Nu(dr("GUIREM_PESO_BRUTO"))
                    psNroBultos = Nu(dr("GUIREM_BULTO"))
                    psTransportista = Nu(dr("TRANSPORTISTA_RAZONSOCIAL"))
                    HoraEmision = Nu(dr("Hora_Guia"))
                    fechaTraslado = Nu(dr("Fecha_Traslado"))
                    psRucDestinatario = Nu(dr("DESTINATARIO_CODINTERNO"))
                    psTransportistaRUC = Nu(dr("TRANSPORTISTA_RUC"))
                    psUbigeoPartida = Nu(dr("GUIREM_DIRECCION_PARTIDA_UBIGEO"))
                    psUbigeoLlegada = Nu(dr("GUIREM_DIRECCION_LLEGADA_UBIGEO"))
                    psPeriodo = Nu(dr("Fecha_Periodo"))
                    psNroBultos = Nu(dr("GUIREM_BULTO"))
                    psPesoBruto = Nu(dr("GUIREM_PESO_BRUTO"))
                    psUnidadMedida = Nu(dr("GUIREM_UNIDAD_MEDIDA_BRUTO"))
                    psRemitente = Nu(dr("REMITENTE_NOMBRE"))
                    psCodexRemGuia = Nu(dr("CODEXT_REM"))
                    psCodexDesc = Nu(dr("CODEX_DES"))
                    psRetira = Nu(dr("GUIREM_PERSONA_RETIRA"))
                    psRecibe = Nu(dr("GUIREM_PERSONA_RECIBE"))
                    psObs = Nu(dr("GUIREM_OBSERVACION"))
                    Exit For
                Next
            End If
            dt = Nothing

            dtEmp = objEmp.Datos_Empresa(Session("Ruta_Emp"), Session("CodEmpresa"))
            If dtEmp.Rows.Count > 0 Then
                For Each drEmp As Data.DataRow In dtEmp.Rows
                    EmpresaRuc = Nu(drEmp("emp_ruc"))
                    EmpresaNombre = Nu(drEmp("emp_nombre"))
                Next
            End If
            dtEmp = Nothing
            '
            Dim savePath As String = Server.MapPath("~/Inventario/GuiaInterna/")
            Dim fileName As String = "GuiaInterna_Nro_" & Session("CodGuia") & ".pdf" ' "Informe_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".pdf"
            Dim fullPath As String = Path.Combine(savePath, fileName)

            Dim NombrePdfGuia As String = ""
            NombrePdfGuia = "GuiaInterna_Nro_" & Session("CodGuia")

            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand

            Cn.Open() : CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " UPDATE TBINV_GUIA_REMISION_0001 SET GUIREM_ARCHIVO = '" & fileName & "' WHERE GUIREM_CODIGO =  " & Session("CodGuia")
            CmdGlobal.ExecuteNonQuery()
            Cn.Close()
            ' Crear el documento PDF
            Dim document As New Document(PageSize.A4.Rotate, 5, 5, 2, 2)
            Dim output As New MemoryStream() ' Crear el escritor PDF


            Dim archivo As String = Server.MapPath("~\Inventario\GuiaInterna\" & NombrePdfGuia & ".pdf") ' Ruta y nombre del archivo PDF
            Dim psRuta As String = ""
            'psRuta = "\\" & NomServer & "\GRE_PDF\" & NombrePdfGuia & ".pdf"

            Dim carpeta As String = Server.MapPath("~/Inventario/GuiaInterna/")

            ' Verificar si la carpeta existe
            If Not Directory.Exists(carpeta) Then
                ' Crear la carpeta
                Directory.CreateDirectory(carpeta)
            End If

            Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(archivo, FileMode.Create))
            ' Abrir el documento
            document.Open()

            'linea vertical punteada al centro
            Dim cb As PdfContentByte = writer.DirectContent
            cb.SetLineDash(3, 3) ' Ancho de la línea
            cb.MoveTo(document.PageSize.Width / 2, document.PageSize.Height) ' Inicio de la línea
            cb.LineTo(document.PageSize.Width / 2, 0) ' Fin de la línea
            cb.Stroke()

            ' Crear dos columnas
            Dim leftColumn As New ColumnText(writer.DirectContent)
            Dim rightColumn As New ColumnText(writer.DirectContent)

            ' Establecer coordenadas y dimensiones de las columnas
            leftColumn.SetSimpleColumn(-40, 0, document.PageSize.Width / 2 + 90, document.PageSize.Height - 10)
            rightColumn.SetSimpleColumn(document.PageSize.Width / 2 - 40, 5, document.PageSize.Width + 90, document.PageSize.Height - 10)

            ' Crear un elemento de contenido (párrafo) para las columnas
            'Dim contentParagraph As New Paragraph("Contenido que se repite en ambas columnas")


            '-----------------------------------------
            Dim bf As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 7, BaseColor.BLACK)
            Dim fFont = New iTextSharp.text.Font(bf)
            Dim bf1 As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
            Dim fFont1 = New iTextSharp.text.Font(bf1)
            Dim bf2 As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 6, BaseColor.BLACK)
            Dim fFont2 = New iTextSharp.text.Font(bf2)

            'crea linea
            Dim separator As New LineSeparator() ' Crear una instancia de LineSeparator
            separator.LineColor = New BaseColor(128, 128, 128) ' Negro' Configurar el color y grosor de la línea
            separator.LineWidth = 0 ' Grosor de 1 punto

            ' DateTime.Now.ToString("dd/MM/yyyyy - HH:mm:ss")


            Dim tableCabecera As New PdfPTable(3) ' Crear una tabla con 2 columnas
            Dim widths As Single() = {3.0F, 3.0F, 4.0F} '' Establecer el estilo de borde de la tabla
            tableCabecera.SetWidths(widths)
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("NRO GUIA INTERNA: " & Session("CodGuia"), fFont))
            tableCabecera.AddCell(New Phrase(EmpresaNombre, fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase(DateTime.Now.ToString("dd/MM/yyyyy"), fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase(DateTime.Now.ToString("HH:mm:ss"), fFont))
            tableCabecera.AddCell(New Phrase("", fFont))
            tableCabecera.AddCell(New Phrase("TRASLADO", fFont1))
            tableCabecera.AddCell(New Phrase("", fFont))

            For Each cell As PdfPCell In tableCabecera.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next
            'tabla 2
            Dim tableRemitente As New PdfPTable(3) ' Crear una tabla con 2 columnas
            Dim widthsRe As Single() = {5.0F, 2.0F, 2.0F} '' Establecer el estilo de borde de la tabla
            tableRemitente.SetWidths(widthsRe)
            tableRemitente.AddCell(New Phrase("REMITENTE    : " & psRemitente, fFont))
            tableRemitente.AddCell(New Phrase("", fFont))
            tableRemitente.AddCell(New Phrase(psCodexRemGuia, fFont))
            tableRemitente.AddCell(New Phrase("DESTINATARIO : " & psDestinatario, fFont))
            tableRemitente.AddCell(New Phrase("", fFont))
            tableRemitente.AddCell(New Phrase(psCodexDesc, fFont))

            For Each cell As PdfPCell In tableRemitente.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next

            'tabla 3
            Dim tableDetalleCab As New PdfPTable(4) ' Crear una tabla con 2 columnas
            Dim widths3 As Single() = {1.0F, 3.0F, 3.0F, 7.0F} '' Establecer el estilo de borde de la tabla
            tableDetalleCab.SetWidths(widths3)
            tableDetalleCab.AddCell(New Phrase("Cant.", fFont))
            tableDetalleCab.AddCell(New Phrase("Nro. Serie", fFont))
            tableDetalleCab.AddCell(New Phrase("Nro. Placa", fFont))
            tableDetalleCab.AddCell(New Phrase("Descripción del equipo", fFont))

            For Each cell As PdfPCell In tableDetalleCab.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next

            'tabla 4 detalle
            Dim table As New PdfPTable(4)
            Dim widths4 As Single() = {1.0F, 3.0F, 3.0F, 7.0F} '' Establecer el estilo de borde de la tabla
            table.SetWidths(widths4)
            dt = Nothing
            dt = obj.Lista_GuiaRemision_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), Session("CodGuia"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    table.AddCell(New Phrase(Nu(dr("Cant")), fFont2))
                    table.AddCell(New Phrase(Nu(dr("SERIE_NRO")), fFont2))
                    table.AddCell(New Phrase(Nu(dr("PLACA_NRO")), fFont2))
                    table.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                Next
            End If
            dt = Nothing

            dt = obj.Lista_GuiaRemision_Detalle_Acc(Session("Ruta_Emp"), Session("CodEmpresa"), Session("CodGuia"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    table.AddCell(New Phrase(Nu(dr("Cant")), fFont2))
                    table.AddCell(New Phrase("", fFont2))
                    table.AddCell(New Phrase("", fFont2))
                    table.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                Next
            End If
            dt = Nothing
            For Each cell As PdfPCell In table.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
                cell.Border = Rectangle.NO_BORDER
            Next
            ' Agregar el contenido a ambas columnas columna derecha

            ' Agregar el contenido a ambas columnas
            leftColumn.AddElement(tableCabecera)
            leftColumn.AddElement(New Chunk(separator))
            leftColumn.AddElement(tableRemitente)
            leftColumn.AddElement(New Chunk(separator))
            leftColumn.AddElement(tableDetalleCab)
            leftColumn.AddElement(New Chunk(separator))
            leftColumn.AddElement(table)

            rightColumn.AddElement(tableCabecera)
            rightColumn.AddElement(New Chunk(separator))
            rightColumn.AddElement(tableRemitente)
            rightColumn.AddElement(New Chunk(separator))
            rightColumn.AddElement(tableDetalleCab)
            rightColumn.AddElement(New Chunk(separator))
            rightColumn.AddElement(table)

            ' Agregar las columnas al documento
            leftColumn.Go()
            rightColumn.Go()
            ' Agregar una tabla al final de ambas columnas

            Dim tablaPie As New PdfPTable(2) ' Crear una tabla con 2 columnas
            Dim widths5 As Single() = {4.0F, 10.0F} '' Establecer el estilo de borde de la tabla
            tablaPie.SetWidths(widths5)
            tablaPie.AddCell(New Phrase("PERSONA QUIEN RECIBE :", fFont))
            tablaPie.AddCell(New Phrase(psRecibe, fFont))
            tablaPie.AddCell(New Phrase("PERSONA QUIEN ENTREGA  :", fFont))
            tablaPie.AddCell(New Phrase(psRetira, fFont))
            tablaPie.AddCell(New Phrase("OBSERVACION:", fFont))
            tablaPie.AddCell(New Phrase(psObs, fFont))

            Dim tableAtBottomColumnTextLeft As New ColumnText(writer.DirectContent)
            tableAtBottomColumnTextLeft.AddElement(New Chunk(separator))
            tableAtBottomColumnTextLeft.SetSimpleColumn(-20, 0, document.PageSize.Width / 2 + 20, 120)
            tableAtBottomColumnTextLeft.AddElement(tablaPie)
            tableAtBottomColumnTextLeft.AddElement(New Paragraph("  "))
            tableAtBottomColumnTextLeft.AddElement(New Paragraph("                                             ------------------------------                                                                 ----------------------------- ", fFont))
            tableAtBottomColumnTextLeft.AddElement(New Paragraph("                                         Unidad quien entrega el equipo                                                           Unidad quien recibe el equipo ", fFont))
            tableAtBottomColumnTextLeft.Go()
            Dim tableAtBottomColumnTextRight As New ColumnText(writer.DirectContent)
            tableAtBottomColumnTextRight.AddElement(New Chunk(separator))
            tableAtBottomColumnTextRight.SetSimpleColumn(document.PageSize.Width / 2 - 20, 0, document.PageSize.Width + 20, 120)
            tableAtBottomColumnTextRight.AddElement(tablaPie)
            tableAtBottomColumnTextRight.AddElement(New Paragraph("  "))
            tableAtBottomColumnTextRight.AddElement(New Paragraph("                                            ------------------------------                                                                  ----------------------------- ", fFont))
            tableAtBottomColumnTextRight.AddElement(New Paragraph("                                        Unidad quien entrega el equipo                                                           Unidad quien recibe el equipo ", fFont))


            tableAtBottomColumnTextRight.Go()

            ' Cerrar el documento
            document.Close()

            ' Descargar el PDF generado
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.TransmitFile(fullPath)
            Response.End()

        End If

    End Sub

    Private Sub RbGuiaRem_CheckedChanged(sender As Object, e As EventArgs) Handles RbGuiaRem.CheckedChanged

        Session("TipoGuia") = "1"
        LblTitulo.Text = "Inventario - Genera Guía de Remisión"
        id_GuiaNumero.Visible = True
        id_DatosTransportista.Visible = True
        id_Transportista.Visible = True
        id_Vehiculo.Visible = True
        id_Chofer.Visible = True
        id_ModalidadTransporte.Visible = True
        id_MotivoTrasldo.Visible = True
        id_GuiaInterna.Visible = False
    End Sub

    Private Sub RbGuiaInt_CheckedChanged(sender As Object, e As EventArgs) Handles RbGuiaInt.CheckedChanged
        Session("TipoGuia") = "2"
        LblTitulo.Text = "Inventario - Genera Guía Interna"
        id_GuiaInterna.Visible = True
        id_GuiaNumero.Visible = False
        id_DatosTransportista.Visible = False
        id_Transportista.Visible = False
        id_Vehiculo.Visible = False
        id_Chofer.Visible = False
        id_ModalidadTransporte.Visible = False
        id_MotivoTrasldo.Visible = False
    End Sub

    Private Sub BtnBuscarArt_Click(sender As Object, e As EventArgs) Handles BtnBuscarArt.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
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
        TxtSku.Value = ""
        Dim dt As New DataTable
        dt = Nothing
        GvBusArticulo.DataSource = dt
        GvBusArticulo.DataBind()
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Call Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        'Dim obj As New Cls_Catalogo
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

        'dt = obj.Bus_Articulo(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)

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

            Dim psClasNumero As String = ""
            If TxtClasificacionBA.Value <> "" Then clasificacion = TxtClasificacionBA.Value
            Dim psPosicionguion As Double = 0

            psPosicionguion = InStr(clasificacion, "-")
            If psPosicionguion > 0 Then
                psClasNumero = Left(clasificacion, psPosicionguion - 1)
                psClasNumero = Trim(psClasNumero)
            End If

            dtColum.Columns.Add("ART_CODIGO")
            dtColum.Columns.Add("ART_CODEQUIVA")
            dtColum.Columns.Add("ART_DESCRIPCION")
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

            dt = obj.Lista_ArticuloxBusqueda(psconexion, pdCodArt, psClasNumero, psDescripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
            If dt.Rows.Count > 0 Then
                For Each drDato As DataRow In dt.Rows
                    drT = dtColum.NewRow()
                    drT("ART_CODIGO") = Nu(drDato("ART_CODIGO"))
                    drT("ART_CODEQUIVA") = Nu(drDato("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drDato("ART_DESCRIPCION"))
                    drT("ART_TIPO") = Nu(drDato("ART_TIPO"))
                    drT("ART_SKU") = Nu(drDato("ART_SKU"))
                    dtColum.Rows.Add(drT)
                Next
            End If

            GvBusArticulo.DataSource = dtColum
            GvBusArticulo.DataBind()


        Catch ex As SqlException

        Catch ex As Exception
        Finally
        End Try

    End Sub

    Private Sub GvBusArticulo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusArticulo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dt As New DataTable
        Dim drT As DataRow

        If e.CommandName = "Aceptar" Then
            dt.Columns.Add("ART_CODIGO")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("ART_TIPO")
            dt.Columns.Add("CANT")
            dt.Columns.Add("ART_SKU")

            For Each row As GridViewRow In GvListaArticulos.Rows
                Dim txtValor As System.Web.UI.WebControls.TextBox = CType(row.FindControl("txtCant"), System.Web.UI.WebControls.TextBox)
                ' Aquí puedes acceder y manipular el valor del TextBox
                Dim valor As String = txtValor.Text

                drT = dt.NewRow()
                drT("ART_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("ART_TIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If valor <> "" Then
                    drT("CANT") = valor
                Else
                    drT("CANT") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                End If
                drT("ART_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                dt.Rows.Add(drT)
            Next
            drT = dt.NewRow()
            drT("ART_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
            drT("ART_TIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
            drT("CANT") = ""
            drT("ART_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
            dt.Rows.Add(drT)

            GvListaArticulos.DataSource = dt
            GvListaArticulos.DataBind()

            Limpiar_Cajas_Buscar_Articulos()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
        End If

    End Sub

    Private Sub GvListaArticulos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvListaArticulos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtValorCant As System.Web.UI.WebControls.TextBox = CType(e.Row.FindControl("txtCant"), System.Web.UI.WebControls.TextBox)
            Dim valor As String = txtValorCant.Text
        End If
    End Sub

    Private Sub GvListaArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dt As New DataTable
        Dim drT As DataRow

        dt.Columns.Add("ART_CODIGO")
        dt.Columns.Add("ART_DESCRIPCION")
        dt.Columns.Add("ART_CODEQUIVA")
        dt.Columns.Add("ART_TIPO")
        dt.Columns.Add("CANT")
        dt.Columns.Add("ART_SKU")

        Dim psCodArt As String = ""


        If e.CommandName = "QuitarArt" Then
            GvListaArticulos.Rows(Index).Visible = False
            psCodArt = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            For Each row As GridViewRow In GvListaArticulos.Rows
                If psCodArt <> Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") Then

                    Dim txtValor As System.Web.UI.WebControls.TextBox = CType(row.FindControl("txtCant"), System.Web.UI.WebControls.TextBox)
                    ' Aquí puedes acceder y manipular el valor del TextBox
                    Dim valor As String = txtValor.Text

                    drT = dt.NewRow()
                    drT("ART_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    drT("ART_TIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    If valor <> "" Then
                        drT("CANT") = valor
                    Else
                        drT("CANT") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    End If
                    drT("ART_SKU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                    dt.Rows.Add(drT)
                End If
            Next

            GvListaArticulos.DataSource = dt
            GvListaArticulos.DataBind()

        End If
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
End Class
