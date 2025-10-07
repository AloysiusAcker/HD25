Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_GuiaRemision_CargaArch_Admin
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New clsInv_Listados
        lblError.Text = ""
        Dim FechaEntrega : FechaEntrega = ""
        Dim FechaFin : FechaFin = ""
        FechaEntrega = Right(txtFecha.Text, 4) + Mid(txtFecha.Text, 4, 2) + Left(txtFecha.Text, 2)
        FechaFin = Right(txtFechaFin.Text, 4) + Mid(txtFechaFin.Text, 4, 2) + Left(txtFechaFin.Text, 2)
        Dim dtListado As New DataTable
        Dim pcodArchivo As Double : pcodArchivo = 0
        Dim i As Integer : i = 0
        Dim psGuiaCod As String = ""
        Try
            If txtBSerie.Text.Trim <> "" Then
                dtListado = obj.Lista_GRxEquipo(Session("Ruta_Emp"), txtBSerie.Text.Trim)
                If dtListado.Rows.Count > 0 Then
                    For Each dr As DataRow In dtListado.Rows
                        If psGuiaCod <> "" Then psGuiaCod = psGuiaCod + ","
                        psGuiaCod = psGuiaCod & Nz(dr("GUIREM_CODIGO"))
                    Next
                End If
            End If
            dtListado = Nothing
            dtListado = obj.Lista_GuiaArchivo(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, 0, FechaFin, psGuiaCod)
            Flex.DataSource = dtListado
            Flex.DataBind()
            lblRegistro.Text = dtListado.Rows.Count
            dtListado = Nothing
            lblRegistro.Text = "Registros Encontrados : " & lblRegistro.Text
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call btnListar_Click(sender, e)
            Call LlenaComboItem("TBOPC405", cboTipoArchivo)
            cboTipoArchivo.Items.Add("< Seleccionar >") : cboTipoArchivo.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInv_Listados
        Dim dtListado As New DataTable
        Dim pcodArchivo As Double : pcodArchivo = 0
        Dim i As Integer : i = 0
        Dim Fila As GridViewRow
        Dim pCodPedido As Double : pCodPedido = 0
        lblError.Text = ""
        If e.CommandName = "Carga" Then
            Flex.Enabled = False
            cboTipoArchivo.SelectedValue = "< Seleccionar >"
            txtArchDescrip.Text = "" : txtNroPedido.Text = "" : txtCodPedido.Text = ""
            txtCodPedido.Text = Flex.Rows(Index).Cells(12).Text
            txtNroPedido.Text = Flex.Rows(Index).Cells(13).Text
            txtCodGuia.Text = Flex.Rows(Index).Cells(11).Text
            txtSerieGuia.Text = Flex.Rows(Index).Cells(3).Text.Replace("&nbsp;", "")
            txtNroGuia.Text = Flex.Rows(Index).Cells(4).Text.Replace("&nbsp;", "")
            txtDestinatario.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            txtCodDestino.Text = Flex.Rows(Index).Cells(6).Text.Replace("&nbsp;", "")
            txtEstado.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            lblEt12.Enabled = True
            lblEt9.Enabled = True
            lblEt13.Enabled = True
            lblEt14.Enabled = True
            fuArchivo.Enabled = True
            btnCancelarTA.Enabled = True
            btnGuardarTA.Enabled = True
            cboTipoArchivo.Enabled = True
            txtArchDescrip.Enabled = True
            txtNroPedido.Enabled = True
            txtCodPedido.Enabled = True
        ElseIf e.CommandName = "Archivo" Then
            DIV2.Visible = True
            pCodPedido = Flex.Rows(Index).Cells(12).Text
            dtListado = obj.Lista_ArchivosxPedido(Session("Ruta_Emp"), Session("CodEmpresa"), pCodPedido)
            FlexDet.DataSource = dtListado
            FlexDet.DataBind()
            dtListado = Nothing
            For i = 0 To FlexDet.Rows.Count - 1
                pcodArchivo = CDbl(FlexDet.Rows(i).Cells(0).Text.Trim.Replace("&nbsp;", "0"))
                dtListado = obj.Lista_PedidoArchivoxCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), pcodArchivo)
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = FlexDet.Rows(i)
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("ARCHIVO_NOMBRE")) & "'TARGET='_blank'>" & Nu(drMenuItem("ARCHIVO_NOMBRE")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        End If
    End Sub
    Protected Sub btnCancelarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelarTA.Click
        fuArchivo.Enabled = False
        cboTipoArchivo.Enabled = False
        txtArchDescrip.Enabled = False
        txtNroPedido.Enabled = False
        txtCodPedido.Enabled = False
        btnCancelarTA.Enabled = False
        btnGuardarTA.Enabled = False
        lblEt12.Enabled = False
        lblEt13.Enabled = False
        lblEt14.Enabled = False
        lblEt1.Enabled = False
        lblEt3.Enabled = False
        lblEt4.Enabled = False
        lblEt5.Enabled = False
        lblEt7.Enabled = False
        lblEt8.Enabled = False
        lblEt9.Enabled = False
        fuArchivo.Enabled = False
        Flex.Enabled = True
        txtCodGuia.Text = ""
        txtSerieGuia.Text = ""
        txtNroGuia.Text = ""
        txtDestinatario.Text = ""
        txtCodDestino.Text = ""
        txtEstado.Text = ""
        txtArchDescrip.Text = ""
        txtNroPedido.Text = ""
        txtCodPedido.Text = ""
        cboTipoArchivo.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub txtBSerie_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBSerie.TextChanged

    End Sub
End Class
