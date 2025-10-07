Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO.Directory
Imports System.Web.Security
Imports System.IO.FileStream
Imports System.Net
Imports System.Drawing.Imaging
Partial Class Cas_DefineTablas1
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Form.Attributes.Add("enctype", "multipart/form-data")
        If Not Page.IsPostBack Then
            Try
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 0
                Ficha.Height = 250
                Ficha_ActiveTabChanged(sender, e)
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
        End If
    End Sub
    Private Sub Llenar_Grilla()
        Try
            Dim dtListado As New DataTable
            Dim dt As New DataTable
            Dim obj As New ModuloCas
            Dim i As Integer
            Dim pCodigo As Double
            Dim Fila As GridViewRow
            dtListado = obj.CasLista_Enlace(Session("Ruta_Emp"))
            Flex.DataSource = dtListado
            Flex.DataBind()
            dtListado = Nothing
            For i = 0 To Flex.Rows.Count - 1
                pCodigo = Flex.Rows(i).Cells(1).Text.Trim
                dtListado = obj.CasLista_Enlace(pCodigo, Session("Ruta_Emp"))
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = Flex.Rows(i)
                        Flex.Rows(i).Cells(4).Text = Nu(drMenuItem("ENLACE_URL")).Length
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Abrir"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='http://" & Nu(drMenuItem("ENLACE_URL")) & "'TARGET='_blank'>" & Nu(drMenuItem("ENLACE_URL")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Llenar_Grilla_TA()
        Try
            Dim dtListado As New DataTable
            Dim obj As New ModuloCas
            Dim i As Integer
            Dim pCodigo As Double
            Dim Fila As GridViewRow
            dtListado = obj.CasLista_TemaAyuda(Session("Ruta_Emp"))
            FlexTA.DataSource = dtListado
            FlexTA.DataBind()
            dtListado = Nothing
            For i = 0 To FlexTA.Rows.Count - 1
                pCodigo = FlexTA.Rows(i).Cells(7).Text.Trim
                dtListado = obj.CasLista_TemaAyuda(pCodigo)
                If dtListado.Rows.Count > 0 Then
                    For Each drMenuItem As Data.DataRow In dtListado.Rows
                        Fila = FlexTA.Rows(i)
                        FlexTA.Rows(i).Cells(11).Text = Nu(drMenuItem("TEMA_NOMBRE_DOC")).Length
                        Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Fila.FindControl("Doc"), System.Web.UI.HtmlControls.HtmlGenericControl)
                        lbl.InnerHtml = "</b><A href='Temas/" & Nu(drMenuItem("TEMA_NOMBRE_DOC")) & "'TARGET='_blank'>" & Nu(drMenuItem("TEMA_NOMBRE_DOC")) & "</A>"
                    Next
                End If
                dtListado = Nothing
            Next
        Catch Ex As SqlException
            lblErrorTA.Visible = True
            lblErrorTA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorTA.Visible = True
            lblErrorTA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub cmdListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListar.Click
        Call Llenar_Grilla()
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.ActiveTabIndex = 0 Then
            Call Llenar_Grilla()
        End If
        If Ficha.ActiveTabIndex = 1 Then
            Call Llenar_Grilla_TA()
        End If
        If Ficha.ActiveTabIndex = 2 Then
            Call LlenaComboItem("TBOPC333", cboTipoAviso)
            Call LlenaComboItem("TBOPC334", cboEstadoAviso)
            cboTipoAviso.Items.Add("< Seleccionar >")
            cboEstadoAviso.Items.Add("< Seleccionar >")
            cboTipoAviso.SelectedValue = "< Seleccionar >"
            cboEstadoAviso.SelectedValue = "< Seleccionar >"
            Call LLenaComboItemTabEsp(DdlBusAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call DdlBusAplicativo_SelectedIndexChanged(sender, e)
            DdlBusProducto.Items.Add("< Seleccionar >") : DdlBusProducto.SelectedValue = "< Seleccionar >"
            DdlBusSubProd.Items.Add("< Seleccionar >") : DdlBusSubProd.SelectedValue = "< Seleccionar >"
            DdlBusProducto.Enabled = False
            DdlBusSubProd.Enabled = False
            Call Llenar_Grilla_A()
            Ficha.Height = 250
        End If
    End Sub
    Protected Sub cmdListarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Call Llenar_Grilla_TA()
    End Sub
    Protected Sub FlexTA_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexTA.PageIndexChanging
        lblErrorTA.Text = ""
        FlexTA.PageIndex = e.NewPageIndex
        Call Llenar_Grilla_TA()
    End Sub
    Protected Sub FlexTA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTA.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblErrorTA.Text = ""
        If e.CommandName = "Ver" Then
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-Disposition", "attachment;  filename=" + FlexTA.Rows(Index).Cells(4).Text)
            Response.Flush()
            Response.WriteFile("\\data\Temas\" & FlexTA.Rows(Index).Cells(4).Text.Trim)
            Response.End()
        End If
    End Sub
    Protected Sub cmdListarA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdListarA.Click
        Call Llenar_Grilla_A()
    End Sub
    Private Sub Llenar_Grilla_A()
        Try
            Dim dtListado As New DataTable
            Dim obj As New ModuloCas
            Dim psCodAplicativo As Double = 0
            Dim psCodProducto As Double = 0
            Dim psCodsubProd As Double = 0
            If DdlBusAplicativo.SelectedValue <> "< Seleccionar >" Then psCodAplicativo = DdlBusAplicativo.SelectedValue.ToString
            If DdlBusProducto.SelectedValue <> "< Seleccionar >" Then psCodProducto = DdlBusProducto.SelectedValue.ToString
            If DdlBusSubProd.SelectedValue <> "< Seleccionar >" Then psCodsubProd = DdlBusSubProd.SelectedValue.ToString
            dtListado = obj.CasLista_Aviso("", "", "", "", "1", Session("Ruta_Emp"), psCodAplicativo, psCodProducto, psCodsubProd, Session("CodEmpresa"))
            FlexA.DataSource = dtListado
            FlexA.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexA_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexA.PageIndexChanging
        lblErrorA.Text = ""
        FlexA.PageIndex = e.NewPageIndex
        Call Llenar_Grilla_A()
    End Sub
    Protected Sub btnEGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim obj As New ModuloCas
        Dim pCodigo As Double : pCodigo = 0
        If Len(txtUrl.Text.Trim) = 0 Then lblError.Text = "Falta ingresar la Dirección de la página." : Exit Sub
        If Len(txtDescripcion.Text.Trim) = 0 Then lblError.Text = "Falta ingresar la Descripción." : Exit Sub
        If lblEtiqueta.Text = "Nuevo Enlace" Then
            obj.InsUpd_Enlace(pCodigo, txtDescripcion.Text.Trim, txtUrl.Text.Trim, "1", Session("Ruta_Emp"))
        ElseIf lblEtiqueta.Text = "Editar Enlace" Then
            pCodigo = txtCodigo.Text.Trim
            obj.InsUpd_Enlace(pCodigo, txtDescripcion.Text.Trim, txtUrl.Text.Trim, "2", Session("Ruta_Emp"))
        End If
        btnECancelar_Click(sender, e)
        cmdListar_Click(sender, e)
    End Sub
    Protected Sub btnENuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoE.Visible = True
        lblEtiqueta.Text = "Nuevo Enlace"
        btnENuevo.Enabled = False
        Ficha.Height = 360
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim Longitud As Integer
        If e.CommandName = "Editar" Then
            lblEtiqueta.Text = "Editar Enlace"
            lblIngresoE.Visible = True
            txtCodigo.Text = Flex.Rows(Index).Cells(1).Text.Trim
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            Longitud = Flex.Rows(Index).Cells(4).Text.Trim
            Dim lbl As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Flex.Rows(Index).FindControl("Abrir"), System.Web.UI.HtmlControls.HtmlGenericControl)
            txtUrl.Text = Mid(lbl.InnerText, 21, Longitud)
            Ficha.Height = 360
        End If
    End Sub
    Protected Sub btnECancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        txtUrl.Text = ""
        lblEtiqueta.Text = ""
        lblIngresoE.Visible = False
        lblError.Text = ""
        btnENuevo.Enabled = True
        Ficha.Height = 250
    End Sub
    Protected Sub btnTANuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoTA.Visible = True
        btnTANuevo.Enabled = False
    End Sub
    Protected Sub btnGuardarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarTA.Click

        'Dim strFolder As String
        'Dim sbServerPath As New StringBuilder(280)
        'Dim sbInfo As New StringBuilder(870)
        'If Upload.HasFile Then
        '    Dim fileExt As String = Path.GetExtension(Upload.FileName)
        '    If fileExt = ".mp3" OrElse fileExt = ".jpg" Then
        '        Try
        '            strFolder = "\\" & NomServer & "\\Temas\"
        '            Upload.SaveAs(strFolder + Upload.PostedFile .FileName)
        '            sbInfo.Append("File Name: ")
        '            sbInfo.Append(Upload.PostedFile.FileName)
        '            sbInfo.Append("<br>")
        '            sbInfo.Append(Upload.PostedFile.ContentLength)
        '            sbInfo.Append(" Kb <br>")
        '            sbInfo.Append("Content Type: ")
        '            sbInfo.Append(Upload.PostedFile.ContentType)
        '            lblErrorTA.Text = sbInfo.ToString()
        '        Catch ex As Exception
        '            lblErrorTA.Text = "Error: " & ex.Message
        '        End Try
        '    Else
        '        lblErrorTA.Text = "Only .mp3 and .jpg files allowed!"
        '    End If
        'Else
        '    lblErrorTA.Text = "Seleccionar Archivo."
        'End If

        'Dim strFolder As String
        'strFolder = "\\" & NomServer & "\\Temas\"
        'If Upload.File_RenameIfAlreadyExists Then
        '    Dim archivo As New Subgurim.Controles.HttpPostedFileAJAX
        '    archivo = Upload.PostedFile
        '    archivo.responseMessage_Uploaded = "Guardado: " + archivo.FileName
        '    Upload.SaveAs(strFolder + archivo.FileName)
        'End If
    End Sub
    Protected Sub btnCancelarTA_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoTA.Visible = False
        txtTADescripcion.Text = ""
        txtTANombre.Text = ""
        btnTANuevo.Enabled = True
    End Sub
    Protected Sub btnNuevoAviso_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoAviso.Visible = True
        lblEtiquetaA.Text = "Nuevo Aviso"
        btnNuevoAviso.Enabled = False
        lblErrorA.Text = ""
        cboTipoAviso.SelectedValue = "< Seleccionar >"
        cboEstadoAviso.SelectedValue = "1"
        cboEstadoAviso.Enabled = False
        Ficha.Height = 500
        GvArchivo.DataSource = Nothing
        GvArchivo.DataBind()
        Call LLenaComboItemTabEsp(DdlAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
        Call DdlAplicativo_SelectedIndexChanged(sender, e)
        DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
        DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        DdlProducto.Enabled = False
        DdlSubProd.Enabled = False
    End Sub
    Protected Sub btnGuardarAviso_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblErrorA.Text = ""
        Dim i As Integer = 0
        Dim strSaveFileAs As String = ""
        Dim strSaveFileAsOrigen As String = ""
        Dim pCodAviso As Double = 0
        Dim obj As New ModuloCas
        Dim dt As New DataTable
        Dim psCodAplicativo As Double = 0
        Dim psCodProducto As Double = 0
        Dim psCodsubProd As Double = 0

        If txtDescripcionAviso.Text.Trim = "" Then lblErrorA.Text = "Debe ingresar la descripción del aviso." : Exit Sub
        If cboTipoAviso.SelectedValue = "< Selecccionar >" Then lblErrorA.Text = "Seleccionar Tipo." : Exit Sub
        If cboEstadoAviso.SelectedValue = "< Selecccionar >" Then lblErrorA.Text = "Seleccionar Estado." : Exit Sub
        If TxtDetalleAviso.Text.Trim = "" Then lblErrorA.Text = "Debe ingresar el detalle del aviso." : Exit Sub
        Try
            If DdlAplicativo.SelectedValue <> "< Seleccionar >" Then psCodAplicativo = DdlAplicativo.SelectedValue
            If DdlProducto.SelectedValue <> "< Seleccionar >" Then psCodProducto = DdlProducto.SelectedValue
            If DdlSubProd.SelectedValue <> "< Seleccionar >" Then psCodsubProd = DdlSubProd.SelectedValue
            If lblEtiquetaA.Text = "Nuevo Aviso" Then
                dt = obj.InsUpd_Aviso(pCodAviso, cboTipoAviso.SelectedValue.Trim, txtDescripcionAviso.Text.Trim, cboEstadoAviso.SelectedValue.Trim, "", "1", Session("Ruta_Emp"), TxtDetalleAviso.Text, psCodAplicativo, psCodProducto, psCodsubProd, Session("CodEmpresa"))
                If dt.Rows.Count = 1 Then
                    For Each dr As DataRow In dt.Rows
                        pCodAviso = Nu(dr("Cod_Aviso").ToString)
                    Next
                End If
            ElseIf lblEtiquetaA.Text = "Editar Aviso" Then
                pCodAviso = txtCodAviso.Text.Trim
                obj.InsUpd_Aviso(pCodAviso, cboTipoAviso.SelectedValue.Trim, txtDescripcionAviso.Text.Trim, cboEstadoAviso.SelectedValue.Trim, "", "2", Session("Ruta_Emp"), TxtDetalleAviso.Text, psCodAplicativo, psCodProducto, psCodsubProd, Session("CodEmpresa"))
            End If
            'guardar archivo
            Dim psArchivo As String = ""
            Dim psRuta As String = ""
            For i = 0 To GvArchivo.Rows.Count - 1
                If Replace(GvArchivo.Rows(i).Cells(2).Text, "&nbsp;", "") <> "" And Replace(GvArchivo.Rows(i).Cells(3).Text, "&nbsp;", "") = "" Then
                    psRuta = "\\" & NomServer & "\Temas_" & Session("SiglaGrupoEmpresa")
                    psArchivo = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                    obj.InsUpd_AvisoArchivo(Session("CodEmpresa"), Session("Ruta_Emp"), pCodAviso, psArchivo, psRuta, "", "", Session("User"))

                    If Exists(Dir(psRuta)) = True Then
                        strSaveFileAs = psRuta & "\" & psArchivo
                    Else
                        CreateDirectory(psRuta)
                        strSaveFileAs = psRuta & "\" & psArchivo
                    End If

                    strSaveFileAsOrigen = Server.MapPath("Temas_" & Session("SiglaGrupoEmpresa")) & "\" & psArchivo
                    FileCopy(strSaveFileAsOrigen, strSaveFileAs)
                    'Kill(strSaveFileAsOrigen)

                End If
            Next
            dt = Nothing

        Catch Ex As SqlException
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorA.Visible = True
            lblErrorA.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
        btnCancelarAviso_Click(sender, e)
        cmdListarA_Click(sender, e)
    End Sub
    Protected Sub btnCancelarAviso_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblIngresoAviso.Visible = False
        txtDescripcionAviso.Text = ""
        txtCodAviso.Text = ""
        TxtDetalleAviso.Text = ""
        btnNuevoAviso.Enabled = True
        lblErrorA.Text = ""
        FlexA.Enabled = True
        Ficha.Height = 250
        GvArchivo.DataSource = Nothing
        GvArchivo.DataBind()
    End Sub
    Protected Sub FlexA_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexA.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim ObjCas As New ModuloCas
        Dim dt As New DataTable
        Dim psCodaviso As Double = 0
        dt = Nothing
        lblErrorA.Text = ""
        Call LLenaComboItemTabEsp(DdlAplicativo, "", "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
        Call DdlAplicativo_SelectedIndexChanged(sender, e)
        DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
        DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        DdlProducto.Enabled = False
        DdlSubProd.Enabled = False
        If e.CommandName = "Editar" Then
            If FlexA.Rows(Index).Cells(8).Text.Trim <> "3" Then
                lblEtiquetaA.Text = "Editar Aviso"
                lblIngresoAviso.Visible = True
                txtCodAviso.Text = FlexA.Rows(Index).Cells(2).Text.Trim
                psCodaviso = FlexA.Rows(Index).Cells(2).Text.Trim
                txtDescripcionAviso.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexA.Rows(Index).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                TxtDetalleAviso.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexA.Rows(Index).Cells(16).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                cboTipoAviso.SelectedValue = FlexA.Rows(Index).Cells(3).Text.Trim
                cboEstadoAviso.SelectedValue = FlexA.Rows(Index).Cells(14).Text.Trim
                DdlAplicativo.SelectedValue = FlexA.Rows(Index).Cells(7).Text.Trim
                Call DdlAplicativo_SelectedIndexChanged(sender, e)
                DdlProducto.SelectedValue = FlexA.Rows(Index).Cells(9).Text.Trim
                Call DdlProducto_SelectedIndexChanged(sender, e)
                DdlSubProd.SelectedValue = FlexA.Rows(Index).Cells(11).Text.Trim
                If cboEstadoAviso.SelectedValue = "0" Then cboEstadoAviso.Enabled = False
                FlexA.Enabled = False
            Else
                lblErrorA.Text = "El Aviso no se puede editar porque ha sido solucionado."
            End If
            dt = ObjCas.ListaArchivo_xAviso(Session("CodEmpresa"), Session("Ruta_Emp"), psCodaviso)
            GvArchivo.DataSource = dt
            GvArchivo.DataBind()
            Ficha.Height = 500
        ElseIf e.CommandName = "Publicar" Then
            If FlexA.Rows(Index).Cells(14).Text.Trim = "1" Then
                Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = False
                Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = True
                txtAvisoDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexA.Rows(Index).Cells(13).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtAvisoCodigo.Text = FlexA.Rows(Index).Cells(2).Text.Trim
                cboANivel.SelectedValue = "< Seleccionar >"
                Call Llenar_GrillaUser()
            Else
                lblErrorA.Text = "El Aviso ya ha sido publicado."
            End If
        End If
    End Sub
    Private Sub Llenar_GrillaUser()
        Try
            Dim dt As New DataTable
            Dim drT As DataRow
            Dim dtListado As New DataTable
            dtListado.Columns.Add("Usuario")
            dtListado.Columns.Add("NomPersonal")
            dtListado.Columns.Add("Nivel1") '
            dtListado.Columns.Add("Nivel") '
            Dim obj As New ModuloCas
            dt = obj.CasLista_UsuarioXNivel("", "1", Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    drT = dtListado.NewRow()
                    drT("Usuario") = Nu(dr("USUARIO"))
                    If Nu(dr("NomPersonal")) = "" Then
                        drT("NomPersonal") = Nu(dr("NOMUSUARIO")) 'NOMUSUARIO
                    Else
                        drT("NomPersonal") = Nu(dr("NomPersonal")) 'NOMUSUARIO
                    End If
                    drT("Nivel1") = Nu(dr("Nivel1"))
                    drT("Nivel") = Nu(dr("Nivel"))
                    dtListado.Rows.Add(drT)
                Next
            End If
            dt = Nothing
            FlexUser.DataSource = dtListado
            FlexUser.DataBind()
            dtListado = Nothing
        Catch Ex As SqlException
            lblErrorUser.Visible = True
            lblErrorUser.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblErrorUser.Visible = True
            lblErrorUser.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnARegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Ficha.ActiveTabIndex = 3 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
        Ficha.ActiveTabIndex = 2 : Ficha.ActiveTab.Enabled = True
        Ficha_ActiveTabChanged(sender, e)
    End Sub
    Protected Sub cboANivel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim User As CheckBox
        Dim i As Integer
        For i = 0 To FlexUser.Rows.Count - 1
            If FlexUser.Rows(i).Cells(4).Text = cboANivel.SelectedValue.Trim Then
                User = FlexUser.Rows(i).Cells(0).FindControl("chk")
                User.Checked = True
            Else
                User = FlexUser.Rows(i).Cells(0).FindControl("chk")
                User.Checked = False
            End If
        Next
    End Sub
    Protected Sub chkMarcartodo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim User As CheckBox
        Dim i As Integer
        For i = 0 To FlexUser.Rows.Count - 1
            User = FlexUser.Rows(i).Cells(0).FindControl("chk")
            If chkMarcartodo.Checked = True Then User.Checked = True Else User.Checked = False
        Next
    End Sub
    Protected Sub btnPublicar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim a As Integer : a = 0
        Dim obj As New ModuloCas
        lblErrorUser.Text = ""
        Dim User As New CheckBox
        For i = 0 To FlexUser.Rows.Count - 1
            User = FlexUser.Rows(i).Cells(0).FindControl("chk")
            If User.Checked = True Then a = 1 : Exit For
        Next
        If a = 0 Then lblErrorUser.Text = "Debe de marcar al menos un usuario."
        If lblErrorUser.Text <> "" Then
            Exit Sub
        End If
        lblErrorUser.Text = ""
        Try
            For i = 0 To FlexUser.Rows.Count - 1
                User = FlexUser.Rows(i).Cells(0).FindControl("chk")
                If User.Checked = True And User.Enabled = True Then
                    obj.InsUpd_PuclicaAviso(txtAvisoCodigo.Text.Trim, FlexUser.Rows(i).Cells(1).Text.Trim, "1", Session("Ruta_Emp"), Session("CodEmpresa"))
                    obj.InsUpd_PuclicaAviso(txtAvisoCodigo.Text.Trim, FlexUser.Rows(i).Cells(1).Text.Trim, "2", Session("Ruta_Emp"), Session("CodEmpresa"))
                End If
            Next
        Catch ex As SqlException
            lblErrorUser.Text = ex.Message
        Catch ex As Exception
            lblErrorUser.Text = ex.Message
        Finally
        End Try
        Call Llenar_GrillaUser()
        Call MarcarUser()
    End Sub
    Protected Sub FlexUser_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexUser.PageIndexChanging
        lblErrorA.Text = ""
        FlexA.PageIndex = e.NewPageIndex
        Call Llenar_GrillaUser()
    End Sub
    Private Sub MarcarUser()
        Dim Check As CheckBox
        Dim i As Integer
        Dim Cn2 As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Try
            Cn2.Open()
            CmdGlobal2.Connection = Cn2
            CmdGlobal2.CommandText = " SELECT USUARIO,ESTADO,AVISO_NRO " _
                                   & " FROM dbo.TBCAS_AVISOS_PUBLICA"
            Rs = CmdGlobal2.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    For i = 0 To FlexUser.Rows.Count - 1
                        If FlexUser.Rows(i).Cells(1).Text = Nu(Rs(0)) And txtAvisoCodigo.Text.Trim = Nu(Rs(2)) Then
                            Check = CType(FlexUser.Rows(i).Cells(0).FindControl("chk"), CheckBox)
                            Check.Checked = True
                            Check.Enabled = False
                        End If
                    Next
                End While
            End If
            Rs.Close()
        Catch ex As SqlException
            lblErrorUser.Text = ex.Message
        Catch ex As Exception
            lblErrorUser.Text = ex.Message
        Finally
            Cn2.Close()
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub
    Protected Sub FlexTA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '
    End Sub

    Protected Sub FlexA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub BtnArchivo_Click(sender As Object, e As EventArgs) Handles BtnArchivo.Click

        Dim i As Integer = 0
        Dim dtListado As New Data.DataTable
        Dim drT As Data.DataRow
        lblErrorA.Text = ""
        dtListado.Columns.Add("ARCHIVO")
        dtListado.Columns.Add("NRO_AVISO")
        dtListado.Columns.Add("AVISOARCH_CODIGO")

        Try
            If FileUpload1.HasFile = False Then
                lblErrorA.Text = "Seleccionar un archivo..."
            Else
                If GvArchivo.Rows.Count > 0 Then
                    For i = 0 To GvArchivo.Rows.Count - 1
                        If FileUpload1.FileName = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´") Then
                            lblErrorA.Text = "El archivo ya se encuentra." : Exit Sub
                        End If
                    Next
                End If
                If GvArchivo.Rows.Count > 0 Then
                    For i = 0 To GvArchivo.Rows.Count - 1
                        drT = dtListado.NewRow()
                        drT("ARCHIVO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        drT("NRO_AVISO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        drT("AVISOARCH_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvArchivo.Rows(i).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                        dtListado.Rows.Add(drT)
                    Next
                End If

                Dim strSaveFileAs As String = ""

                strSaveFileAs = Server.MapPath("Temas_" & Session("SiglaGrupoEmpresa")) ' "\\" & NomServer & "\Temas_" & Session("SiglaGrupoEmpresa")  ' "\\DATA\\Archivos\" + Upload.FileName 

                If Exists(Dir(strSaveFileAs)) = True Then
                    FileUpload1.SaveAs(strSaveFileAs & "\" & FileUpload1.FileName)
                Else
                    CreateDirectory(strSaveFileAs)
                    FileUpload1.SaveAs(strSaveFileAs & "\" & FileUpload1.FileName)
                End If

                drT = dtListado.NewRow()
                drT("ARCHIVO") = FileUpload1.FileName  'FuArchivo.FileName.ToString
                drT("NRO_AVISO") = ""
                drT("AVISOARCH_CODIGO") = ""
                dtListado.Rows.Add(drT)

            End If
            GvArchivo.DataSource = dtListado
            GvArchivo.DataBind()

        Catch ex As Data.SqlClient.SqlException
            lblErrorA.Text = "Ha ocurrido un error en la base de datos. " & ex.Message
        Catch ex As Exception
            lblErrorA.Text = "Ha ocurrido un error en la aplicacion. " & ex.Message
        Finally
        End Try
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub DdlAplicativo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAplicativo.SelectedIndexChanged
        lblError.Visible = False
        DdlProducto.Items.Clear()
        DdlSubProd.Items.Clear()
        DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
        DdlProducto.Enabled = False
        DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        DdlSubProd.Enabled = False
        Call LLenaComboItemTabEsp(DdlProducto, DdlAplicativo.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If DdlAplicativo.SelectedValue = "< Seleccionar >" Then
            DdlProducto.Enabled = False
            DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
            DdlSubProd.Enabled = False
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        Else
            DdlProducto.Enabled = True
            DdlProducto.Items.Add("< Seleccionar >") : DdlProducto.SelectedValue = "< Seleccionar >"
            DdlSubProd.Enabled = False
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        End If
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub DdlProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlProducto.SelectedIndexChanged
        lblError.Visible = False
        DdlSubProd.Items.Clear()
        DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        DdlSubProd.Enabled = False
        If DdlProducto.SelectedIndex = -1 Or DdlProducto.Items.Count = 0 Then Exit Sub
        If DdlProducto.Items(DdlProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(DdlSubProd, DdlAplicativo.SelectedValue.Trim, DdlProducto.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If DdlProducto.SelectedValue = "< Seleccionar >" Then
            DdlSubProd.Enabled = False
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        Else
            DdlSubProd.Enabled = True
            DdlSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        End If
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub DdlBusAplicativo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlBusAplicativo.SelectedIndexChanged
        lblError.Visible = False
        DdlBusProducto.Items.Clear()
        DdlBusSubProd.Items.Clear()
        DdlBusProducto.Items.Add("< Seleccionar >") : DdlBusProducto.SelectedValue = "< Seleccionar >"
        DdlBusProducto.Enabled = False
        DdlBusSubProd.Items.Add("< Seleccionar >") : DdlBusSubProd.SelectedValue = "< Seleccionar >"
        DdlBusSubProd.Enabled = False
        Call LLenaComboItemTabEsp(DdlBusProducto, DdlBusAplicativo.SelectedValue.Trim, "", "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If DdlBusAplicativo.SelectedValue = "< Seleccionar >" Then
            DdlBusProducto.Enabled = False
            DdlBusProducto.Items.Add("< Seleccionar >") : DdlBusProducto.SelectedValue = "< Seleccionar >"
            DdlBusSubProd.Enabled = False
            DdlBusSubProd.Items.Add("< Seleccionar >") : DdlSubProd.SelectedValue = "< Seleccionar >"
        Else
            DdlBusProducto.Enabled = True
            DdlBusProducto.Items.Add("< Seleccionar >") : DdlBusProducto.SelectedValue = "< Seleccionar >"
            DdlBusSubProd.Enabled = False
            DdlBusSubProd.Items.Add("< Seleccionar >") : DdlBusSubProd.SelectedValue = "< Seleccionar >"
        End If
        Me.Page.Session.Timeout = 1080
    End Sub

    Private Sub DdlBusProducto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlBusProducto.SelectedIndexChanged
        lblError.Visible = False
        DdlBusSubProd.Items.Clear()
        DdlBusSubProd.Items.Add("< Seleccionar >") : DdlBusSubProd.SelectedValue = "< Seleccionar >"
        DdlBusSubProd.Enabled = False
        If DdlBusProducto.SelectedIndex = -1 Or DdlBusProducto.Items.Count = 0 Then Exit Sub
        If DdlBusProducto.Items(DdlBusProducto.SelectedIndex).Value = "0" Then Exit Sub
        Call LLenaComboItemTabEsp(DdlBusSubProd, DdlBusAplicativo.SelectedValue.Trim, DdlBusProducto.SelectedValue.Trim, "TBESP_CAS1", "TBESP_CAS2", "TBESP_CAS3", 3, Session("CodEmpresa"), Session("Ruta_Emp"))
        If DdlBusProducto.SelectedValue = "< Seleccionar >" Then
            DdlBusSubProd.Enabled = False
            DdlBusSubProd.Items.Add("< Seleccionar >") : DdlBusSubProd.SelectedValue = "< Seleccionar >"
        Else
            DdlBusSubProd.Enabled = True
            DdlBusSubProd.Items.Add("< Seleccionar >") : DdlBusSubProd.SelectedValue = "< Seleccionar >"
        End If
        Me.Page.Session.Timeout = 1080
    End Sub

End Class
