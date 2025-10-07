Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inspeccion_ListaDetalleOficina
    Inherits System.Web.UI.Page
    Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
    Dim psCodEmpresa As String = ConfigurationManager.AppSettings("CodEmpresa")
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            'btnListar_Click(sender, e)
            lblRegistro.Text = "No se encontrarón registros."
            lblRegVisitas.Text = "No se encontrarón registros."
            lblRegistroDoc.Text = "No se encontrarón registros."
        End If
    End Sub
    Private Sub Datos_Oficina()
        Dim obj As New clsLogis_Listado
        Dim dt As New DataTable
        Dim pdCodOficina As Double = 0
        Dim dtListado As New DataTable
        Dim i As Integer = 0
        Dim drT As DataRow
        Dim TipoFlex As String = ""
        Try
            dtListado.Columns.Add("c0")
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            dtListado.Columns.Add("c5")
            dtListado.Columns.Add("c6")
            dtListado.Columns.Add("c7")
            dtListado.Columns.Add("c8")
            dtListado.Columns.Add("c9")
            If txtUbicacion.Text.Trim <> "" Then pdCodOficina = txtUbicacion.Text.Trim
            dt = obj.Lista_Datos_Oficina(psConexion, psCodEmpresa, pdCodOficina)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    i = i + 1
                    drT = dtListado.NewRow()
                    drT("c0") = Nu(dr("CECOSE_DESCRIPCION"))
                    drT("c1") = Nu(dr("CECOSE_DIRECCION"))
                    drT("c4") = Nu(dr("CECOSE_TELEF"))
                    If Nu(dr("CECOSE_TSI")) <> "" Then drT("c5") = "TSI : " & Nu(dr("CECOSE_TSI")) Else drT("c5") = "TSI : No"
                    If Nu(dr("CECOSE_TTA")) <> "" Then drT("c5") = drT("c5") & " " & "TTA : " & Nu(dr("CECOSE_TTA")) Else drT("c5") = drT("c5") & " " & "TTA : No"
                    If Nu(dr("CECOSE_ESTADO")) = "1" Then
                        TipoFlex = "1"
                        drT("c6") = Nu(dr("TIPIFICACION"))
                        drT("c7") = Nu(dr("CECOSE_ESTADO_OBS"))
                        drT("c8") = Nu(dr("RESPONSABLE"))
                        drT("c9") = IIf(Nu(dr("CECOSE_FECHA_SOLUCION")) <> "", Nu(dr("FECHA_SOLUCION")), "")
                    End If
                    dtListado.Rows.Add(drT)
                Next
            End If
            dt = Nothing
            If TipoFlex = "1" Then
                FlexDet2.Visible = False
                FlexDet.Visible = True
                FlexDet.DataSource = dtListado
                FlexDet.DataBind()
            Else
                FlexDet.Visible = False
                FlexDet2.Visible = True
                FlexDet2.DataSource = dtListado
                FlexDet2.DataBind()
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Private Sub Lista_Inspeccion()
        Dim obj As New clsInspeccion_Listado
        Dim pdCodOficina As Double = 0
        Dim FechaIni As String = "20100101"
        Dim FechaFin As String = "21000101"
        If txtUbicacion.Text.Trim <> "" Then pdCodOficina = txtUbicacion.Text.Trim
        Try
            FlexVisita.DataSource = obj.Lista_Inspeccion(psConexion, psCodEmpresa, FechaIni, FechaFin, pdCodOficina)
            FlexVisita.DataBind()
            lblRegVisitas.Text = "Servicios. Se encontraron " & FlexVisita.Rows.Count & " registros."
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        End Try
    End Sub
    Private Sub Llenar_Grilla_TA()
        Try
            Dim dtListado As New DataTable
            Dim obj As New clsInspeccion_Listado
            Dim i As Integer
            Dim pCodigo As Double = 0
            Dim pdOficina As Double = 0
            Dim Fila As GridViewRow
            If txtUbicacion.Text.Trim <> "" Then
                pdOficina = txtUbicacion.Text.Trim
            Else
                pdOficina = 0
            End If
            dtListado = obj.Listar_Ayuda_General(psConexion, pdOficina, "20100101", "2100010", 0, "", Session("CodEmpresa"), User.Identity.Name)
            FlexDoc.DataSource = dtListado
            FlexDoc.DataBind()
            lblRegistroDoc.Text = "Archivos. Se encontraron " & FlexDoc.Rows.Count & " registros."
            dtListado = Nothing
            For i = 0 To FlexDoc.Rows.Count - 1
                pCodigo = FlexDoc.Rows(i).Cells(0).Text.Trim
                dtListado = obj.Listar_TemaAyuda(psConexion, pCodigo)
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = FlexDoc.Rows(i)
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "'TARGET='_blank'>" & Nu(drMenuItem("TEMA_AYUDA_NOMBRE_DOC")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim obj2 As New clsInv_Procesos
        Dim pCodArt As Integer
        Dim pdCodAlmacen As Double = 0
        lblError.Text = ""
        If txtUbicacion.Text.Trim = "" Then lblError.Text = "Debe ingresar la oficina." : Exit Sub
        If txtUbiCodigo.Text.Trim = "" Then lblError.Text = "Debe ingresar la oficina." : Exit Sub
        obj2.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        If txtUbicacion.Text.Trim <> "" Then pdCodAlmacen = txtUbicacion.Text.Trim Else pdCodAlmacen = 0
        Try
            Flex.DataSource = obj.Lista_EquiposAlmacen(psConexion, psCodEmpresa, pCodArt, "2", pdCodAlmacen, "0", "", 0)
            Flex.DataBind()
            lblRegistro.Text = "Equipos. Se encontraron " & Flex.Rows.Count & " registros."
            Call Lista_Inspeccion()
            Call Llenar_Grilla_TA()
            Call Datos_Oficina()
            lblDatos.Visible = True
            lblDatoOf.Visible = True
            lblRegistro.Visible = True
            lblequipo.Visible = True
            lblRegVisitas.Visible = True
            lblVisita.Visible = True
            lblRegistroDoc.Visible = True
            lblDocumento.Visible = True
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Dim obj As New clsInv_Listados
        Flex.PageIndex = e.NewPageIndex
        Flex.DataSource = obj.Lista_StockArticulos(psConexion, psCodEmpresa, 0, 0, "0")
        Flex.DataBind()
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
        Response.AddHeader("Content-Disposition", "attachment;filename=STOCK.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub optUbicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUbicacion.Text = ""
        txtUbiCodigo.Text = ""
        txtUbiDescripcion.Text = ""
    End Sub
    Protected Sub txtUbiCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUbicacion.Text = ""
        txtUbiDescripcion.Text = ""
        FlexDet.DataSource = Nothing
        FlexDet.DataBind()
        Flex.DataSource = Nothing
        Flex.DataBind()
        FlexVisita.DataSource = Nothing
        FlexVisita.DataBind()
        FlexDoc.DataSource = Nothing
        FlexDoc.DataBind()
        lblRegistro.Text = "Equipos."
        lblRegVisitas.Text = "Servicios."
        lblRegistroDoc.Text = "Archivos."
        lblDatos.Visible = False
        lblDatoOf.Visible = False
        lblRegistro.Visible = False
        lblequipo.Visible = False
        lblRegVisitas.Visible = False
        lblVisita.Visible = False
        lblRegistroDoc.Visible = False
        lblDocumento.Visible = False
    End Sub
    Protected Sub FlexUbicacion_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtUbicacion.Text = ""
            txtUbiCodigo.Text = ""
            txtUbiDescripcion.Text = ""
            txtUbiCodigo.Text = FlexUbicacion.Rows(Index).Cells(1).Text
            txtUbiDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexUbicacion.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            ModalPopupExtender2.Hide()
        End If
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim obj As New clsInv_Listados
            FlexUbicacion.DataSource = Nothing
            FlexUbicacion.DataBind()
            'FlexUbicacion.DataSource = obj.Lista_CentroCostos(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, "2")
            FlexUbicacion.DataSource = obj.Lista_Oficina(psConexion, psCodEmpresa, txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexUbicacion.DataBind()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexUbicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub
End Class
