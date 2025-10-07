Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inventario_Movimiento_Equipos
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("UnaVez") = "NO"
            Session("UnaVezLista") = "NO"
            Session("ModoB") = ""
        End If
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim fInv As New clsInv_Procesos
        Dim psArticulo As String = ""
        Dim pdTipoUbica As String = ""
        Dim pdTipoDestino As String = ""
        Dim dt As New DataTable
        Dim dtLista As New DataTable
        Dim psCodArticulo As String = ""
        Dim pdSaldo As Double = 0
        Dim ListaArt As String = ""
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        lblError.Text = ""
        If cboUbica.Text <> "< Seleccionar >" Then pdTipoUbica = cboUbica.SelectedValue.Trim
        If cboDestino.Text <> "< Seleccionar >" Then pdTipoDestino = cboDestino.SelectedValue.Trim
        If txtArtCodigo.Text.Trim = "" Then
            If Left(fInv.Lista_Articulos_xClasif(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name), 13) = "En la función" Then
                lblError.Text = lblError.Text & "<br> - " & fInv.Lista_Articulos_xClasif(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
            Else
                ListaArt = fInv.Lista_Articulos_xClasif(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
            End If
        ElseIf txtArtCodigo.Text.Trim <> "" Then
            If Left(fInv.Verificar_ArtExiste(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim), 13) = "En la función" Then
                lblError.Text = lblError.Text & "<br> - " & fInv.Verificar_ArtExiste(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim)
            ElseIf fInv.Verificar_ArtExiste(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim) = "SI" Then
                ListaArt = txtArtCodigo.Text.Trim
            Else
                lblError.Text = lblError.Text & "<br> - No se encontró artículo."
            End If
        End If
        If ListaArt = "" Then lblError.Text = lblError.Text & "<br> - No se encontró artículo."
        If Left(fInv.Grabar_ArtxUsuario(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim), 13) = "En la función" Then
            lblError.Text = lblError.Text & "<br> - " & fInv.Grabar_ArtxUsuario(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim)
        ElseIf fInv.Grabar_ArtxUsuario(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, txtArtCodigo.Text.Trim) = "" Then
            '
        End If
        If Left(fInv.Crear_Vista_Movimiento(psConexion, Session("CodEmpresa"), pdTipoUbica, txtUbicaCodigo.Text.Trim, pdTipoDestino, txtDesCodigo.Text.Trim), 13) = "En la función" Then
            lblError.Text = lblError.Text & "<br> - " & fInv.Crear_Vista_Movimiento(Session("Ruta_Emp"), Session("CodEmpresa"), pdTipoUbica, txtUbicaCodigo.Text.Trim, pdTipoDestino, txtDesCodigo.Text.Trim)
        ElseIf fInv.Crear_Vista_Movimiento(psConexion, Session("CodEmpresa"), pdTipoUbica, txtUbicaCodigo.Text.Trim, pdTipoDestino, txtDesCodigo.Text.Trim) = "" Then
            '
        End If
        If lblError.Text <> "" Then
            lblError.Text = "Se han encontrado las sgtes. observaciones: " & lblError.Text.Trim
            Exit Sub
        End If
        Dim psFechaIni As String = ""
        Dim psFechaFin As String = ""
        If txtFechaIni.Text.Trim <> "" Then psFechaIni = Right(txtFechaIni.Text.Trim, 4) & Mid(txtFechaIni.Text.Trim, 4, 2) & Left(txtFechaIni.Text.Trim, 2)
        If txtFechaFin.Text.Trim <> "" Then psFechaFin = Right(txtFechaFin.Text.Trim, 4) & Mid(txtFechaFin.Text.Trim, 4, 2) & Left(txtFechaFin.Text.Trim, 2)
        Try
            dt = obj.Lista_MovimientoEquipos(psConexion, Session("CodEmpresa"), psFechaIni, psFechaFin, txtSerie.Text.Trim)
            Flex.DataSource = dt
            Flex.DataBind()
            lblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros"
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboUbica_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboUbica.SelectedIndexChanged
        If cboUbica.SelectedValue.Trim = "1" Then
            Session("ModoB") = "Origen"
            lblEtiqUbicacion.Text = "Búsqueda de Almacén"
            btnBusUbicacion.Enabled = True
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboUbica.SelectedValue.Trim = "2" Then
            Session("ModoB") = "Origen"
            lblEtiqUbicacion.Text = "Búsqueda de Sección"
            btnBusUbicacion.Enabled = True
            txtUbicaCodInterno.Text = ""
            txtUbicaDescripcion.Text = ""
            txtUbicaCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboUbica.SelectedValue.Trim = "< Seleccionar >" Then
            Session("ModoB") = ""
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
        Dim cbo As DropDownList = cboUbica
        If Session("ModoB") = "Origen" Then cbo = cboUbica
        If Session("ModoB") = "Destino" Then cbo = cboDestino
        Try
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            If cbo.SelectedValue.Trim = "2" Then
                FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, Session("CodEmpresa"), txtBusUbicCodInterno.Text.Trim, txtBusUbicDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            ElseIf cbo.SelectedValue.Trim = "1" Then
                If txtBusUbicCodInterno.Text.Trim <> "" Then pdCodAlmacen = txtBusUbicCodInterno.Text.Trim
                FlexUbicacion.DataSource = obj.Lista_Almacen(psConexion, Session("CodEmpresa"), pdCodAlmacen, txtBusUbicDescripcion.Text.Trim)
                FlexUbicacion.DataBind()
            End If
            If Session("ModoB") = "Origen" Then
                ModalPopupExtender1.Show()
            ElseIf Session("ModoB") = "Destino" Then
                ModalPopupExtender3.Show()
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Try
            Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim CodModulo As String : CodModulo = ""
            lblError.Text = ""
            If Session("UnaVez") = "NO" Then
                If e.CommandName = "Aceptar" Then
                    If Session("ModoB") = "Origen" Then
                        txtUbicaCodInterno.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        txtUbicaDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        txtUbicaCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        Session("UnaVez") = "SI"
                        txtBusUbicCodInterno.Text = ""
                        txtBusUbicDescripcion.Text = ""
                        FlexUbicacion.DataSource = Nothing
                        FlexUbicacion.DataBind()
                        ModalPopupExtender1.Hide()
                    ElseIf Session("ModoB") = "Destino" Then
                        txtDesCodInterno.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        txtDesDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        txtDesCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                        Session("UnaVez") = "SI"
                        txtBusUbicCodInterno.Text = ""
                        txtBusUbicDescripcion.Text = ""
                        FlexUbicacion.DataSource = Nothing
                        FlexUbicacion.DataBind()
                        ModalPopupExtender3.Hide()
                    End If
                End If
            Else
                Session("UnaVez") = "NO"
                ModalPopupExtender1.Hide()
                ModalPopupExtender3.Hide()
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
            '
        End Try
    End Sub
    Protected Sub btnListarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As New clsInv_Listados
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            If txtPArtCodigo.Text.Trim <> "" Then pdCodArt = txtPArtCodigo.Text.Trim
            FlexArt.DataSource = obj.BuscarX_Articulos(psConexion, Session("CodEmpresa"), pdCodArt, txtPArtDescripcion.Text.Trim, "")
            FlexArt.DataBind()
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
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
            ModalPopupExtender2.Hide()
            txtPArtCodigo.Text = ""
            txtPArtDescripcion.Text = ""
            FlexArt.DataSource = Nothing
            FlexArt.DataBind()
        End If
    End Sub
    Protected Sub btnCerrarArt_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtPArtCodigo.Text = ""
        txtPArtDescripcion.Text = ""
        FlexArt.DataSource = Nothing
        FlexArt.DataBind()
        ModalPopupExtender2.Hide()
    End Sub
    Protected Sub btnUbicCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtBusUbicCodInterno.Text = ""
        txtBusUbicDescripcion.Text = ""
        FlexUbicacion.DataSource = Nothing
        FlexUbicacion.DataBind()
        ModalPopupExtender1.Hide()
    End Sub
    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        txtArtDescripcion.Text = ""
        txtArtCodigo.Text = ""
        txtUbicaCodigo.Text = ""
        txtUbicaCodInterno.Text = ""
        txtUbicaDescripcion.Text = ""
        txtFechaIni.Text = ""
        txtFechaFin.Text = ""
        Flex.DataSource = Nothing
        Flex.DataBind()
        lblRegistro.Text = ""
        txtSerie.Text = ""
        txtDesCodigo.Text = ""
        txtDesCodInterno.Text = ""
        txtDesDescripcion.Text = ""
        cboUbica.SelectedValue = "< Seleccionar >"
        cboDestino.SelectedValue = "< Seleccionar >"
        btnBusUbicacion.Enabled = False
        btnDestino.Enabled = False
    End Sub
    Protected Sub cboDestino_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDestino.SelectedIndexChanged
        If cboDestino.SelectedValue.Trim = "1" Then
            Session("ModoB") = "Destino"
            lblEtiqUbicacion.Text = "Búsqueda de Almacén"
            btnDestino.Enabled = True
            txtDesCodInterno.Text = ""
            txtDesDescripcion.Text = ""
            txtDesCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboDestino.SelectedValue.Trim = "2" Then
            Session("ModoB") = "Destino"
            lblEtiqUbicacion.Text = "Búsqueda de Sección"
            btnDestino.Enabled = True
            txtDesCodInterno.Text = ""
            txtDesDescripcion.Text = ""
            txtDesCodigo.Text = ""
            Session("UnaVez") = "NO"
        ElseIf cboDestino.SelectedValue.Trim = "< Seleccionar >" Then
            Session("ModoB") = ""
            btnDestino.Enabled = False
            txtDesCodInterno.Text = ""
            txtDesDescripcion.Text = ""
            txtDesCodigo.Text = ""
            Session("UnaVez") = "NO"
        End If
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
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
        Response.AddHeader("Content-Disposition", "attachment;filename=MovimientoEquipos.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class