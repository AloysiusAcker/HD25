Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Partial Class Inventario_Inventario_Flujo_Atencion_Diaria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            txtFechaIni.Text = FormatoFecha(FechaActual)
            txtFechaFin.Text = ""
            LblError.Text = ""
        End If
    End Sub

    Protected Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click
        Dim obj As New Cls_Inventario
        LblError.Text = ""
        Dim pCodArt As Integer = 0
        Dim TipoLista As String = ""
        Dim pdCodAlmacen As Double = 0
        Dim psTipoGuia As String = ""
        Dim psFechaIni As String = ""
        Dim psFechaFin As String = ""

        psTipoGuia = DdlTipo.SelectedValue
        If psTipoGuia = "< Todos >" Then psTipoGuia = ""
        If txtFechaIni.Text <> "" Then psFechaIni = Right(txtFechaIni.Text, 4) & Mid(txtFechaIni.Text, 4, 2) & Left(txtFechaIni.Text, 2)
        If txtFechaFin.Text <> "" Then psFechaFin = Right(txtFechaFin.Text, 4) & Mid(txtFechaFin.Text, 4, 2) & Left(txtFechaFin.Text, 2)


        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Try
            Flex.DataSource = obj.Inv_Lista_Flujo_Atencion(psConexion, Session("CodEmpresa"), psTipoGuia, psFechaIni, psFechaFin)
            Flex.DataBind()
            LblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim sb As New StringBuilder()
        Dim sw As New StringWriter(sb)
        Dim htw As New HtmlTextWriter(sw)

        Dim page As New Page()
        Dim form As New HtmlForm()

        Flex.EnableViewState = False

        ' Deshabilitar la validación de eventos, sólo asp.net 2 
        page.EnableEventValidation = False

        ' Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD. 
        page.DesignerInitialize()

        page.Controls.Add(form)
        form.Controls.Add(Flex)

        page.RenderControl(htw)

        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=data.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.[Default]
        Response.Write(sb.ToString())
        Response.[End]()
        Me.Page.Session.Timeout = 1080
    End Sub
End Class
