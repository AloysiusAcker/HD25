Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports OfficeOpenXml
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports ZXing
Imports Image = iTextSharp.text.Image
Imports Rectangle = iTextSharp.text.Rectangle
Imports Font = iTextSharp.text.Font
'Imports ZXing
Imports DataTable = System.Data.DataTable
Imports System.Diagnostics
Imports ZXing.Common


Partial Class Inventario_Inventario_Resumen
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combos()
            Call Llena_Ubicacion(ddlUbicacion)
        End If
    End Sub

    Private Sub Llena_Ubicacion(ByVal combo As DropDownList)
        'Lista_Ubicaciones
        Dim obj As New clsInv_Listados
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = obj.Lista_Ubicaciones(Session("Ruta_Emp"), Session("CodEmpresa"))
        combo.DataTextField = "Ubicacion"
        combo.DataValueField = "UBICACION_CODIGO"
        combo.DataBind()
        combo.Items.Add("< Seleccionar >")
        combo.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub Listar_Resumen_Inventario_xUbicacion()
        LblRegistro.Text = ""
        lblError.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim dtO As New DataTable
        Dim tipo As String = ""
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim ubicacion As String = LblUbicaCodigo.Text.ToString
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigo.Text.ToString)
        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBCentroC.Checked Then
            tipo = "2"
        End If
        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")
        Try

            dt = obj.Resumen_Invenatrio_xUbicacion(Session("Ruta_Emp"), Session("CodEmpresa"), tipo, psUbicaCodigo, psCodInv)
            gvResumen.DataSource = dt
            gvResumen.DataBind()

            If dt.Rows.Count > 1 Then
                LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro.Text = "Hay 1 registro."
            End If
            dt = Nothing

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub Llenar_Combos()
        Dim obj As New Cls_Inventario_Verificacion
        Dim objC As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Inventario(psconexion)
        DdlInventario.DataSource = dt
        DdlInventario.DataValueField = "INVENT_CODIGO"
        DdlInventario.DataTextField = "INVENT_DESC"
        DdlInventario.DataBind()

        DdlInventario.Items.Add("< Seleccionar >")
        DdlInventario.SelectedValue = "< Seleccionar >"

    End Sub
    Protected Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim objU As New Cls_Inventario_Ubicacion
        Dim objMa As New Cls_Marcas
        Dim objMo As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim dtU As New DataTable
        Dim dtM As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim inventario As Double = 0
        inventario = Nz(DdlInventario.SelectedValue.ToString)
        Dim codigo As Double = 0
        Dim descripcion As String = BuscarDescripcion.Value.ToString
        Dim psCodInterno As String = ""

        If TituloPopup.Text = "Búsqueda Almacén" Then
            codigo = Nz(BuscarCodigo.Value.ToString)
            dt = obj.Listar_Almacenes_Inventario_Verificacion(psconexion, inventario, codigo, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
            psCodInterno = BuscarCodigo.Value.ToString
            dt = obj.Listar_CentroC_Inventario_Verificacion(psconexion, inventario, psCodInterno, descripcion)
        End If

        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub
    Private Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            LblUbicaCodigo.Text = GvBusqueda.Rows(Index).Cells(3).Text
            LblUbicaCodigoInv.Text = GvBusqueda.Rows(Index).Cells(4).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If

        Limpiar_Cajas_Popup()
    End Sub
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

        Limpiar_Cajas_Popup()
    End Sub
    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        LblUbicaCodigo.Text = ""
        LblUbicaCodigoInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblRegistro2.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvListaBienesxPlacar.DataSource = dt
        gvListaBienesxPlacar.DataBind()
        lblCantRegistro.Text = ""
        gvDetalle.DataSource = dt
        gvDetalle.DataBind()
        accordion.Visible = False
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged

        LblUbicaCodigoInv.Text = ""
        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblRegistro2.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvListaBienesxPlacar.DataSource = dt
        gvListaBienesxPlacar.DataBind()
        lblCantRegistro.Text = ""
        gvDetalle.DataSource = dt
        gvDetalle.DataBind()
        accordion.Visible = False
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Listar_Resumen_Inventario_xUbicacion()
        lblCodEstado.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaxPlaca.DataSource = dt
        gvListaxPlaca.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        lblCantRegistro.Text = ""
        gvDetalle.DataSource = dt
        gvDetalle.DataBind()
        accordion.Visible = False
        LblRegistro2.Text = ""
    End Sub

    Private Sub gvResumen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvResumen.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblRegistro2.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim psEstado As String = ""
        Dim pdSerieNumerar As Double = 0
        lblError.Text = ""
        Try
            If e.CommandName = "Detalle" Then
                psEstado = gvResumen.Rows(Index).Cells(3).Text
                lblCodEstado.Text = psEstado
                If psEstado <> "20" Then
                    gvListaBienesxPlacar.DataSource = Nothing
                    gvListaBienesxPlacar.DataBind()
                    dt = obj.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", psEstado, IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
                    GvListaVerificarInventario.DataSource = dt
                    GvListaVerificarInventario.DataBind()
                    gvDetalle.DataSource = dt
                    gvDetalle.DataBind()
                    If dt.Rows.Count > 1 Then
                        LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
                        lblCantRegistro.Text = "Hay " & dt.Rows.Count & " registros."
                        accordion.Visible = True
                    ElseIf dt.Rows.Count = 1 Then
                        LblRegistro2.Text = "Hay 1 registro."
                        lblCantRegistro.Text = "Hay 1 registro."
                    End If
                Else
                    GvListaVerificarInventario.DataSource = Nothing
                    GvListaVerificarInventario.DataBind()
                    gvDetalle.DataSource = Nothing
                    gvDetalle.DataBind()

                    dt = obj.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", psEstado, IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
                    gvListaBienesxPlacar.DataSource = dt
                    gvListaBienesxPlacar.DataBind()
                    If dt.Rows.Count > 1 Then
                        LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
                        lblCantRegistro.Text = "Hay " & dt.Rows.Count & " registros."
                        accordion.Visible = True
                    ElseIf dt.Rows.Count = 1 Then
                        LblRegistro2.Text = "Hay 1 registro."
                        lblCantRegistro.Text = "Hay 1 registro."
                    End If
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la base de datos: " & ex.Message & " .');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la aplicacion: " & ex.Message & " .');", True)
        End Try
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        exportarEstados_xHoja()

    End Sub

    Private Sub exportarEstados_xHoja()
        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable()
        Dim dt3 As New DataTable()
        Dim dt4 As New DataTable()
        Dim dt5 As New DataTable()
        Dim dt6 As New DataTable()
        Dim dt7 As New DataTable()
        Dim dt8 As New DataTable()
        Dim dt9 As New DataTable()
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim objdatos As New Cls_Inventario_Verificacion
        ' Configurar los datos en dt1 y dt2...
        dt1 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "12")
        dt2 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "13")
        dt3 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "2")
        dt4 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "3")
        dt5 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "7")
        dt6 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "5")
        dt7 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "8")
        dt8 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "9")
        dt9 = objdatos.Lista_Equipos_Inventariados_xEstadoExportar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "25")

        ' Crear el archivo de Excel
        Using excelPackage As New ExcelPackage()
            ' Agregar hojas al archivo de Excel
            Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Bienes en GPS")
            Dim worksheet2 = excelPackage.Workbook.Worksheets.Add("Bienes Inventariados")
            Dim worksheet3 = excelPackage.Workbook.Worksheets.Add("B.I. Faltantes")
            Dim worksheet4 = excelPackage.Workbook.Worksheets.Add("B.I. En otra Ubicacion ")
            Dim worksheet5 = excelPackage.Workbook.Worksheets.Add("B.I. Sobrantes")
            Dim worksheet6 = excelPackage.Workbook.Worksheets.Add("B.I. Encontrado x Serie")
            Dim worksheet7 = excelPackage.Workbook.Worksheets.Add("B. x Placar en otra ubicacion")
            Dim worksheet8 = excelPackage.Workbook.Worksheets.Add("B. x Placar Correspondidos")
            Dim worksheet9 = excelPackage.Workbook.Worksheets.Add("Inv. Sin Sobrantes")

            ' Llenar Hoja1 con los datos de dt1
            worksheet1.Cells("A1").LoadFromDataTable(dt1, True)
            worksheet2.Cells("A1").LoadFromDataTable(dt2, True)
            worksheet3.Cells("A1").LoadFromDataTable(dt3, True)
            worksheet4.Cells("A1").LoadFromDataTable(dt4, True)
            worksheet5.Cells("A1").LoadFromDataTable(dt5, True)
            worksheet6.Cells("A1").LoadFromDataTable(dt6, True)
            worksheet7.Cells("A1").LoadFromDataTable(dt7, True)
            worksheet8.Cells("A1").LoadFromDataTable(dt8, True)
            worksheet9.Cells("A1").LoadFromDataTable(dt9, True)

            ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
            Response.Clear()
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment; filename=" & TxtDescripcion.Text & ".xlsx")
            Response.BinaryWrite(excelPackage.GetAsByteArray())
            Response.End()
        End Using
    End Sub


    Private Sub ExportarAWord()
        Dim fileName As String = Server.MapPath("~/Informe/Reporte.docx")

        Dim dt As New DataTable()
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim objdatos As New Cls_Inventario_Verificacion
    End Sub

    'Public Function GenerateBarcode(id As String) As Byte()
    '    Dim barcodeWriter As New BarcodeWriter()
    '    barcodeWriter.Format = BarcodeFormat.CODE_128 ' Puedes ajustar el formato según tus necesidades

    '    barcodeWriter.Options.Width = 200 ' Ancho de la imagen del código de barras
    '    barcodeWriter.Options.Height = 100 ' Altura de la imagen del código de barras


    '    ' Generar código de barras como bytes
    '    Dim barcodeBitmap As Bitmap = barcodeWriter.Write(id)
    '    Dim barcodeBytes As Byte()

    '    Using stream As New MemoryStream()
    '        barcodeBitmap.Save(stream, Imaging.ImageFormat.Png)
    '        barcodeBytes = stream.ToArray()
    '    End Using

    '    Return barcodeBytes
    'End Function

    Private Function GetTempImagePath(barcodeBytes As Byte()) As String
        ' Guardar la imagen temporalmente y devolver la ruta del archivo
        Dim tempPath As String = Server.MapPath("~/Informe/TempBarcodeImage.png")
        File.WriteAllBytes(tempPath, barcodeBytes)
        Return tempPath
    End Function

    Private Sub BtnCC_Click(sender As Object, e As EventArgs) Handles BtnCC.Click
        'Dim sb As StringBuilder = New StringBuilder()
        'Dim sw As System.IO.StringWriter = New System.IO.StringWriter(sb)
        'Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        'Dim pagina As Page
        'Dim form = New HtmlForm
        'gvDetalle.EnableViewState = False
        'pagina.EnableEventValidation = False
        'pagina.DesignerInitialize()
        'pagina.Controls.Add(form)
        'form.Controls.Add(gvDetalle)
        'pagina.RenderControl(htw)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=EquiposInventariados.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default
        'Response.Write(sb.ToString())
        'Response.End()
    End Sub

    Private Sub BtnInforme_Click(sender As Object, e As EventArgs) Handles BtnInforme.Click
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
        Dim obj As New clsInv_Listados

        dt = Nothing
        '
        Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
        Dim fileName As String = "INFORME_FINAL_" & TxtCodigo.Text & "-" & TxtDescripcion.Text & ".pdf"
        Dim fullPath As String = Path.Combine(savePath, fileName)

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand



        ' Crear el documento PDF
        Dim document As New iTextSharp.text.Document(iTextSharp.text.PageSize.A3.Rotate, 5, 5, 2, 2)
        Dim output As New MemoryStream() ' Crear el escritor PDF

        Dim archivo As String = Server.MapPath("~\Inventario\Informe\" & fileName) ' Ruta y nombre del archivo PDF
        Dim psRuta As String = ""
        Dim carpeta As String = Server.MapPath("~/Inventario/Informe/")

        ' Verificar si la carpeta existe
        If Not Directory.Exists(carpeta) Then
            Directory.CreateDirectory(carpeta)
        End If
        Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(archivo, FileMode.Create))
        ' Abrir el documento
        document.Open()

        Dim imagePath As String = Server.MapPath("~/Inventario/Imagenes/imgInforme.jpg")
        Dim imagePath2 As String = Server.MapPath("~/Inventario/Imagenes/FranlaLeft.jpg")

        ' Crear una instancia de la clase Image
        Dim image As Image = Image.GetInstance(imagePath)
        Dim image2 As Image = Image.GetInstance(imagePath2)

        ' Obtener el tamaño de la página
        Dim VarpageSize As Rectangle = document.PageSize

        ' Establecer la posición y las dimensiones de la imagen para cubrir toda la página
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)

        image.SetAbsolutePosition(20, 0)
        image.ScaleAbsolute(VarpageSize.Width, VarpageSize.Height)
        document.Add(image)

        '-----------------------------------------
        Dim bf As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 16, BaseColor.BLACK)
        Dim fFont = New iTextSharp.text.Font(bf)
        Dim bf1 As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
        Dim fFont1 = New iTextSharp.text.Font(bf1)
        Dim bf2 As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK)
        Dim fFont2 = New iTextSharp.text.Font(bf2)

        Dim myColor As New BaseColor(0, 112, 192)

        Dim tbTituos As New PdfPTable(1) ' Crear una tabla con 2 columnas
        Dim widthsT As Single() = {12.0F} '' Establecer el estilo de borde de la tabla
        tbTituos.SetWidths(widthsT)
        tbTituos.AddCell(New Phrase("INFORME DE", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 25, iTextSharp.text.Font.BOLD, BaseColor.BLACK)))
        tbTituos.AddCell(New Phrase("INVENTARIO FÍSICO", New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 25, iTextSharp.text.Font.BOLD, BaseColor.BLACK)))
        tbTituos.AddCell(New Phrase(UCase(TxtDescripcion.Text), New iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 25, iTextSharp.text.Font.BOLD, BaseColor.BLACK)))

        For Each cell As PdfPCell In tbTituos.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next

        'tableCabecera.SetTotalWidth({400})
        tbTituos.TotalWidth = 700

        tbTituos.WriteSelectedRows(0, tbTituos.Rows.Count, 30, 800, writer.DirectContent)

        document.NewPage()

        ' Establecer la posición y las dimensiones de la imagen para cubrir toda la página
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)

        ' Crear una instancia de la clase CustomPageEventHandlerX
        Dim miVariable As String = TxtDescripcion.Text
        Dim eventHandler As New CustomPageEventHandler(miVariable)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler


        Dim psOficina As String = ""
        psOficina = TxtDescripcion.Text
        Dim pdCodCC As Double = 0
        pdCodCC = Nz(LblUbicaCodigo.Text)
        Dim psDireccionCC As String = ""
        dt = obj.Lista_xCentroCostos(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodCC)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                psDireccionCC = Nu(dr("CECOSE_DIRECCION"))
            Next
        End If
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)

        Dim objUbiFecha As New Cls_Inventario_Ubicacion
        dt = objUbiFecha.Inventario_ListaUbicaciones_xInventario(Session("Ruta_Emp"), psCodInv, psUbicaCodigo)

        Dim psFechaProgramacion As String = ""
        For Each dr As DataRow In dt.Rows
            psFechaProgramacion = Nu(dr("Fecha_Programacion"))
        Next

        Dim tableCabecera As New PdfPTable(1) ' Crear una tabla con 2 columnas
        Dim widths As Single() = {12.0F} '' Establecer el estilo de borde de la tabla
        tableCabecera.SetWidths(widths)
        tableCabecera.AddCell(New Phrase("Oficina", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase(psOficina, New Font(Font.FontFamily.HELVETICA, 20, Font.NORMAL, BaseColor.BLACK)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("Direccion", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase(psDireccionCC, New Font(Font.FontFamily.HELVETICA, 20, Font.NORMAL, BaseColor.BLACK)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("Centro de Costo", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase(TxtCodigo.Text, New Font(Font.FontFamily.HELVETICA, 20, Font.NORMAL, BaseColor.BLACK)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("Fecha", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase(psFechaProgramacion, New Font(Font.FontFamily.HELVETICA, 20, Font.NORMAL, BaseColor.BLACK)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        tableCabecera.AddCell(New Phrase("Personal de Inventario", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))



        dt = objUbiFecha.Inventario_Ubicaciones_Personal(Session("Ruta_Emp"), psUbicaCodigo)
        For Each dr As DataRow In dt.Rows
            tableCabecera.AddCell(New Phrase("•	" & Nu(dr("nombre_completo")), New Font(Font.FontFamily.HELVETICA, 20, Font.NORMAL, BaseColor.BLACK)))
        Next


        'tableCabecera.AddCell(New Phrase("•	JOSE ALEGRE", New Font(Font.FontFamily.HELVETICA, 20, Font.NORMAL, BaseColor.BLACK)))
        'tableCabecera.AddCell(New Phrase("•	ALONZO CORNEJO", New Font(Font.FontFamily.HELVETICA, 20, Font.NORMAL, BaseColor.BLACK)))

        For Each cell As PdfPCell In tableCabecera.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next

        'tableCabecera.SetTotalWidth({400})
        tableCabecera.TotalWidth = 700

        tableCabecera.WriteSelectedRows(0, tableCabecera.Rows.Count, 60, 720, writer.DirectContent)


        document.NewPage()

        ' Establecer la posición y las dimensiones de la imagen para cubrir toda la página
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)

        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        Dim pdCantRegistro As Double = 0

        Dim objdatos As New Cls_Inventario_Verificacion
        dt = Nothing
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "12", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        Dim psTotalGps As Double = 0
        psTotalGps = dt.Rows.Count

        Dim table2 As New PdfPTable(1) ' Crear una tabla con 2 columnas
        Dim widths2 As Single() = {12.0F} '' Establecer el estilo de borde de la tabla
        table2.SetWidths(widths2)
        table2.AddCell(New Phrase("Bienes en GPS", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("La Oficina tiene asignado en el sistema antes del inventario, " & psTotalGps & " ítems", fFont))

        ' Bienes en GPS
        'La Oficina tiene asignado en el sistema antes del inventario, 263 ítems

        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1000

        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 780, writer.DirectContent)

        Dim filasPorPagina As Integer = 45
        Dim filaActual As Integer = 0

        ''tabla de generados

        Dim tableGps As New PdfPTable(6) ' Crear una tabla con 2 columnas
        Dim widths3 As Single() = {3.0F, 10.0F, 5.0F, 5.0F, 1.0F, 4.0F} '' Establecer el estilo de borde de la tabla
        tableGps.SetWidths(widths3)

        Dim psCantPag As Double = 1
        Dim celda As New PdfPCell(New Phrase("MATERIAL.", fFont2))
        celda.BackgroundColor = New BaseColor(192, 192, 192) ' Establecer el color que desees
        tableGps.AddCell(celda)
        'tableGps.AddCell(New Phrase("MATERIAL.", fFont2))

        tableGps.AddCell(New Phrase("DENOMINACION", fFont2))
        tableGps.AddCell(New Phrase("NUM SERIE", fFont2))
        tableGps.AddCell(New Phrase("ACTIVO FIJO", fFont2))
        tableGps.AddCell(New Phrase("SN°", fFont2))
        tableGps.AddCell(New Phrase("Ce.COSTE", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGps.AddCell(New Phrase(Nu(dr("SERIE_MATERIAL")), fFont2))
                tableGps.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGps.AddCell(New Phrase(Nu(dr("SERIE_NRO")), fFont2))
                tableGps.AddCell(New Phrase(Nu(dr("SERIE_ACTIVOFIJO")), fFont2))
                tableGps.AddCell(New Phrase(Nu(dr("sub")), fFont2))
                tableGps.AddCell(New Phrase(Nu(dr("SERIE_CE_COSTO")), fFont2))
                pdCantRegistro = pdCantRegistro + 1
                filaActual = filaActual + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGps.TotalWidth = 1000
                    If psCantPag = 1 Then
                        filasPorPagina = 50
                        tableGps.WriteSelectedRows(0, tableGps.Rows.Count, 60, 730, writer.DirectContent)
                    Else
                        tableGps.WriteSelectedRows(0, tableGps.Rows.Count, 60, 780, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    If pdCantRegistro < dt.Rows.Count Then
                        document.NewPage()
                    End If
                    tableGps.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If
        dt = Nothing

        If filaActual > 0 Then
            filaActual = 0
            tableGps.TotalWidth = 1000
            tableGps.WriteSelectedRows(0, tableGps.Rows.Count, 60, 780, writer.DirectContent)
        End If
        Dim posX As Double = 0
        pdCantRegistro = 0
        Dim tableGpsI As New PdfPTable(14) ' Crear una tabla con 2 columnas
        Dim widths4 As Single() = {2.0F, 2.5F, 0.5F, 4.0F, 4.0F, 2.5F, 2.0F, 3.0F, 2.0F, 2.0F, 5.0F, 3.0F, 2.0F, 2.0F} '' Establecer el estilo de borde de la tabla
        tableGpsI.SetWidths(widths4)

        'ínventariados

        document.NewPage()
        tableGps.DeleteBodyRows()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        psCantPag = 1
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "13", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        posX = 780
        table2.AddCell(New Phrase("Bienes Inventariados", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("Se inventariaron un total " & psTotalGps & " ítems.", fFont))
        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1100
        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 800, writer.DirectContent)

        filasPorPagina = 24
        tableGpsI.AddCell(New Phrase("Nro Placa", fFont2))
        tableGpsI.AddCell(New Phrase("CAF", fFont2))
        tableGpsI.AddCell(New Phrase("SUB", fFont2))
        tableGpsI.AddCell(New Phrase("Denominacion", fFont2))
        tableGpsI.AddCell(New Phrase("Nro Serie", fFont2))
        tableGpsI.AddCell(New Phrase("Centro Costos", fFont2))
        tableGpsI.AddCell(New Phrase("Modelo", fFont2))
        tableGpsI.AddCell(New Phrase("Marca", fFont2))
        tableGpsI.AddCell(New Phrase("Estado", fFont2))
        tableGpsI.AddCell(New Phrase("Cod. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Desc. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Ubicacion", fFont2))
        tableGpsI.AddCell(New Phrase("Material", fFont2))
        tableGpsI.AddCell(New Phrase("Est. Inv", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGpsI.AddCell(New Phrase(Nu(dr("PLACA_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("CAF")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SUB")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SERIE_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("Cod_Ubicacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_modelo")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_marca")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODIGO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("UBICACION_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODEQUIVA")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO_INVENTARIO")), fFont2))
                filaActual = filaActual + 1
                pdCantRegistro = pdCantRegistro + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGpsI.TotalWidth = 1100
                    If psCantPag = 1 Then
                        filasPorPagina = 27
                        tableGpsI.WriteSelectedRows(0, 100, 60, posX - 70, writer.DirectContent)
                    Else
                        tableGpsI.WriteSelectedRows(0, 100, 60, 800, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    If pdCantRegistro < dt.Rows.Count Then
                        document.NewPage()
                    End If
                    tableGpsI.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If
        dt = Nothing

        If filaActual > 0 Then
            filaActual = 0
            tableGpsI.TotalWidth = 1000
            If psCantPag = 1 Then
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 710, writer.DirectContent)
            Else
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 800, writer.DirectContent)
            End If
        End If
        pdCantRegistro = 0
        filasPorPagina = 28
        'no inventariados
        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        psCantPag = 1
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "2", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        table2.AddCell(New Phrase("Bienes Inventariados (FALTANTES)", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("No se encontraron  " & psTotalGps & " ítems correspondientes al Listado GPS de la " & TxtDescripcion.Text, fFont))
        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1000
        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 780, writer.DirectContent)

        tableGpsI.DeleteBodyRows()
        tableGpsI.AddCell(New Phrase("Nro Placa", fFont2))
        tableGpsI.AddCell(New Phrase("CAF", fFont2))
        tableGpsI.AddCell(New Phrase("SUB", fFont2))
        tableGpsI.AddCell(New Phrase("Denominacion", fFont2))
        tableGpsI.AddCell(New Phrase("Nro Serie", fFont2))
        tableGpsI.AddCell(New Phrase("Centro Costos", fFont2))
        tableGpsI.AddCell(New Phrase("Modelo", fFont2))
        tableGpsI.AddCell(New Phrase("Marca", fFont2))
        tableGpsI.AddCell(New Phrase("Estado", fFont2))
        tableGpsI.AddCell(New Phrase("Cod. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Desc. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Ubicacion", fFont2))
        tableGpsI.AddCell(New Phrase("Material", fFont2))
        tableGpsI.AddCell(New Phrase("Est. Inv", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGpsI.AddCell(New Phrase(Nu(dr("PLACA_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("CAF")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SUB")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SERIE_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("Cod_Ubicacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_modelo")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_marca")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODIGO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("UBICACION_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODEQUIVA")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO_INVENTARIO")), fFont2))
                filaActual = filaActual + 1
                pdCantRegistro = pdCantRegistro + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGpsI.TotalWidth = 1100
                    If psCantPag = 1 Then
                        filasPorPagina = 30
                        tableGpsI.WriteSelectedRows(0, 100, 60, posX - 70, writer.DirectContent)
                    Else
                        tableGpsI.WriteSelectedRows(0, 100, 60, 800, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    If pdCantRegistro < dt.Rows.Count Then
                        document.NewPage()
                    End If
                    tableGpsI.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If
        posX = 780

        If filaActual > 0 Then
            filasPorPagina = 30 - filaActual
            filaActual = 0
            tableGpsI.TotalWidth = 1100
            If psCantPag = 1 Then
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 710, writer.DirectContent)
            Else
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 800, writer.DirectContent)
            End If
        End If
        '
        pdCantRegistro = 0
        posX = 780
        filasPorPagina = 25
        'no inventariados
        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        psCantPag = 1
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "3", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        table2.AddCell(New Phrase("Bienes Inventariados (ENCONTRADOS EN OTRAS UBICACIONES)", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("Se inventariarón " & psTotalGps & " ítems que fueron encontrados en OTRAS UBICACIONES", fFont))
        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1000
        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 780, writer.DirectContent)

        tableGpsI.DeleteBodyRows()
        tableGpsI.AddCell(New Phrase("Nro Placa", fFont2))
        tableGpsI.AddCell(New Phrase("CAF", fFont2))
        tableGpsI.AddCell(New Phrase("SUB", fFont2))
        tableGpsI.AddCell(New Phrase("Denominacion", fFont2))
        tableGpsI.AddCell(New Phrase("Nro Serie", fFont2))
        tableGpsI.AddCell(New Phrase("Centro Costos", fFont2))
        tableGpsI.AddCell(New Phrase("Modelo", fFont2))
        tableGpsI.AddCell(New Phrase("Marca", fFont2))
        tableGpsI.AddCell(New Phrase("Estado", fFont2))
        tableGpsI.AddCell(New Phrase("Cod. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Desc. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Ubicacion", fFont2))
        tableGpsI.AddCell(New Phrase("Material", fFont2))
        tableGpsI.AddCell(New Phrase("Est. Inv", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGpsI.AddCell(New Phrase(Nu(dr("PLACA_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("CAF")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SUB")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SERIE_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("Cod_Ubicacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_modelo")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_marca")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODIGO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("UBICACION_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODEQUIVA")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO_INVENTARIO")), fFont2))
                filaActual = filaActual + 1
                pdCantRegistro = pdCantRegistro + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGpsI.TotalWidth = 1100
                    If psCantPag = 1 Then
                        filasPorPagina = 30
                        tableGpsI.WriteSelectedRows(0, 100, 60, posX - 70, writer.DirectContent)
                    Else
                        tableGpsI.WriteSelectedRows(0, 100, 60, 800, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    If pdCantRegistro < dt.Rows.Count Then
                        document.NewPage()
                    End If
                    tableGpsI.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If

        If filaActual > 0 Then
            filasPorPagina = 30 - filaActual
            filaActual = 0
            tableGpsI.TotalWidth = 1100
            If psCantPag = 1 Then
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 710, writer.DirectContent)
            Else
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 800, writer.DirectContent)
            End If
        End If
        pdCantRegistro = 0
        filasPorPagina = 25
        'no inventariados
        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        posX = 780
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        psCantPag = 1
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "7", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        table2.AddCell(New Phrase("Bienes Inventariados (SOBRANTES)", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("Se registraron como SOBRANTES, " & psTotalGps & " Items. Estos Items son Por Placar", fFont))
        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1000
        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 780, writer.DirectContent)
        tableGpsI.DeleteBodyRows()
        tableGpsI.AddCell(New Phrase("Nro Placa", fFont2))
        tableGpsI.AddCell(New Phrase("CAF", fFont2))
        tableGpsI.AddCell(New Phrase("SUB", fFont2))
        tableGpsI.AddCell(New Phrase("Denominacion", fFont2))
        tableGpsI.AddCell(New Phrase("Nro Serie", fFont2))
        tableGpsI.AddCell(New Phrase("Centro Costos", fFont2))
        tableGpsI.AddCell(New Phrase("Modelo", fFont2))
        tableGpsI.AddCell(New Phrase("Marca", fFont2))
        tableGpsI.AddCell(New Phrase("Estado", fFont2))
        tableGpsI.AddCell(New Phrase("Cod. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Desc. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Ubicacion", fFont2))
        tableGpsI.AddCell(New Phrase("Material", fFont2))
        tableGpsI.AddCell(New Phrase("Est. Inv", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGpsI.AddCell(New Phrase(Nu(dr("PLACA_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("CAF")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SUB")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SERIE_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("Cod_Ubicacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_modelo")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_marca")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODIGO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("UBICACION_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODEQUIVA")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO_INVENTARIO")), fFont2))
                filaActual = filaActual + 1
                pdCantRegistro = psCantPag + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGpsI.TotalWidth = 1100
                    If psCantPag = 1 Then
                        filasPorPagina = 30
                        tableGpsI.WriteSelectedRows(0, 100, 60, posX - 70, writer.DirectContent)
                    Else
                        tableGpsI.WriteSelectedRows(0, 100, 60, 800, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    If pdCantRegistro < dt.Rows.Count Then
                        document.NewPage()
                    End If
                    tableGpsI.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If


        If filaActual > 0 Then
            filasPorPagina = 30 - filaActual
            filaActual = 0
            tableGpsI.TotalWidth = 1100
            If psCantPag = 1 Then
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 710, writer.DirectContent)
            Else
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 800, writer.DirectContent)
            End If
        End If
        pdCantRegistro = 0
        filasPorPagina = 25
        'no inventariados
        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        psCantPag = 1
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "5", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        table2.AddCell(New Phrase("Bienes Inventariados (ENCONTRADOS POR SERIE)", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("Se inventariarion " & psTotalGps & " Items que fueron Encontrados por Serie", fFont))
        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1000
        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 800, writer.DirectContent)
        posX = 780
        tableGpsI.DeleteBodyRows()
        tableGpsI.AddCell(New Phrase("Nro Placa", fFont2))
        tableGpsI.AddCell(New Phrase("CAF", fFont2))
        tableGpsI.AddCell(New Phrase("SUB", fFont2))
        tableGpsI.AddCell(New Phrase("Denominacion", fFont2))
        tableGpsI.AddCell(New Phrase("Nro Serie", fFont2))
        tableGpsI.AddCell(New Phrase("Centro Costos", fFont2))
        tableGpsI.AddCell(New Phrase("Modelo", fFont2))
        tableGpsI.AddCell(New Phrase("Marca", fFont2))
        tableGpsI.AddCell(New Phrase("Estado", fFont2))
        tableGpsI.AddCell(New Phrase("Cod. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Desc. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Ubicacion", fFont2))
        tableGpsI.AddCell(New Phrase("Material", fFont2))
        tableGpsI.AddCell(New Phrase("Est. Inv", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGpsI.AddCell(New Phrase(Nu(dr("PLACA_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("CAF")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SUB")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SERIE_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("Cod_Ubicacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_modelo")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_marca")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODIGO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("UBICACION_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODEQUIVA")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO_INVENTARIO")), fFont2))
                filaActual = filaActual + 1
                pdCantRegistro = pdCantRegistro + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGpsI.TotalWidth = 1100
                    If psCantPag = 1 Then
                        filasPorPagina = 30
                        tableGpsI.WriteSelectedRows(0, 100, 60, 700, writer.DirectContent)
                    Else
                        tableGpsI.WriteSelectedRows(0, 100, 60, 800, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    If pdCantRegistro < dt.Rows.Count Then
                        document.NewPage()
                    End If
                    tableGpsI.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If


        If filaActual > 0 Then
            filasPorPagina = 30 - filaActual
            filaActual = 0
            tableGpsI.TotalWidth = 1100
            If psCantPag = 1 Then
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 710, writer.DirectContent)
            Else
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 800, writer.DirectContent)
            End If
        End If
        filasPorPagina = 25

        pdCantRegistro = 0

        '
        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        psCantPag = 1
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "8", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        table2.AddCell(New Phrase("Bienes Por Placar (ENCONTRADOS EN OTRAS UBICACIONES)", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("Se tienen por Placar " & psTotalGps & " Items encontrados en Otras Ubicaciones", fFont))
        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1000
        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 800, writer.DirectContent)

        tableGpsI.DeleteBodyRows()
        tableGpsI.AddCell(New Phrase("Nro Placa", fFont2))
        tableGpsI.AddCell(New Phrase("CAF", fFont2))
        tableGpsI.AddCell(New Phrase("SUB", fFont2))
        tableGpsI.AddCell(New Phrase("Denominacion", fFont2))
        tableGpsI.AddCell(New Phrase("Nro Serie", fFont2))
        tableGpsI.AddCell(New Phrase("Centro Costos", fFont2))
        tableGpsI.AddCell(New Phrase("Modelo", fFont2))
        tableGpsI.AddCell(New Phrase("Marca", fFont2))
        tableGpsI.AddCell(New Phrase("Estado", fFont2))
        tableGpsI.AddCell(New Phrase("Cod. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Desc. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Ubicacion", fFont2))
        tableGpsI.AddCell(New Phrase("Material", fFont2))
        tableGpsI.AddCell(New Phrase("Est. Inv", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGpsI.AddCell(New Phrase(Nu(dr("PLACA_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("CAF")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SUB")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SERIE_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("Cod_Ubicacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_modelo")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_marca")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODIGO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("UBICACION_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODEQUIVA")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO_INVENTARIO")), fFont2))
                filaActual = filaActual + 1
                pdCantRegistro = pdCantRegistro + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGpsI.TotalWidth = 1100
                    If psCantPag = 1 Then
                        filasPorPagina = 30
                        tableGpsI.WriteSelectedRows(0, 100, 60, 710, writer.DirectContent)
                    Else
                        tableGpsI.WriteSelectedRows(0, 100, 60, 800, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    If pdCantRegistro < dt.Rows.Count Then
                        document.NewPage()
                    End If
                    tableGpsI.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If


        If filaActual > 0 Then
            filasPorPagina = 30 - filaActual
            filaActual = 0
            tableGpsI.TotalWidth = 1100
            If psCantPag = 1 Then
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 710, writer.DirectContent)
            Else
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 800, writer.DirectContent)
            End If
        End If

        filasPorPagina = 25
        '
        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        psCantPag = 1
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "9", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        table2.AddCell(New Phrase("Bienes Por Placar (Inventariados CORRESPONDIDOS)", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        table2.AddCell(New Phrase("Se tienen por Placar " & psTotalGps & " Items que fueron Inventariados correspondientes al Listado GPS", fFont))
        For Each cell As PdfPCell In table2.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        table2.TotalWidth = 1000
        table2.WriteSelectedRows(0, table2.Rows.Count, 60, 780, writer.DirectContent)

        tableGpsI.DeleteBodyRows()
        tableGpsI.AddCell(New Phrase("Nro Placa", fFont2))
        tableGpsI.AddCell(New Phrase("CAF", fFont2))
        tableGpsI.AddCell(New Phrase("SUB", fFont2))
        tableGpsI.AddCell(New Phrase("Denominacion", fFont2))
        tableGpsI.AddCell(New Phrase("Nro Serie", fFont2))
        tableGpsI.AddCell(New Phrase("Centro Costos", fFont2))
        tableGpsI.AddCell(New Phrase("Modelo", fFont2))
        tableGpsI.AddCell(New Phrase("Marca", fFont2))
        tableGpsI.AddCell(New Phrase("Estado", fFont2))
        tableGpsI.AddCell(New Phrase("Cod. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Desc. Art.", fFont2))
        tableGpsI.AddCell(New Phrase("Ubicacion", fFont2))
        tableGpsI.AddCell(New Phrase("Material", fFont2))
        tableGpsI.AddCell(New Phrase("Est. Inv", fFont2))
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                tableGpsI.AddCell(New Phrase(Nu(dr("PLACA_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("CAF")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SUB")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_denominacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("SERIE_ORIGINAL")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("Cod_Ubicacion")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_modelo")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("serie_marca")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODIGO")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("UBICACION_DESCRIPCION")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ART_CODEQUIVA")), fFont2))
                tableGpsI.AddCell(New Phrase(Nu(dr("ESTADO_INVENTARIO")), fFont2))
                filaActual = filaActual + 1
                If filaActual >= filasPorPagina Then
                    filaActual = 0
                    tableGpsI.TotalWidth = 1100
                    If psCantPag = 1 Then
                        filasPorPagina = 30
                        tableGpsI.WriteSelectedRows(0, 100, 60, 700, writer.DirectContent)
                    Else
                        tableGpsI.WriteSelectedRows(0, 100, 60, 800, writer.DirectContent)
                    End If
                    'document.Add(tableGps)
                    document.NewPage()
                    tableGpsI.DeleteBodyRows()
                    image2.SetAbsolutePosition(0, 0)
                    image2.ScaleAbsolute(20, VarpageSize.Height)
                    document.Add(image2)

                    ' Asignar el evento al escritor
                    writer.PageEvent = eventHandler
                    psCantPag = psCantPag + 1
                End If
            Next
        End If


        If filaActual > 0 Then
            filasPorPagina = 30 - filaActual
            filaActual = 0
            tableGpsI.TotalWidth = 1100
            If psCantPag = 1 Then
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 710, writer.DirectContent)
            Else
                tableGpsI.WriteSelectedRows(0, tableGpsI.Rows.Count, 60, 800, writer.DirectContent)
            End If
        End If


        filasPorPagina = 25


        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler
        dt = Nothing

        Dim tableR1 As New PdfPTable(1) ' Crear una tabla con 2 columnas
        Dim widthsR1 As Single() = {12.0F} '' Establecer el estilo de borde de la tabla
        tableR1.SetWidths(widthsR1)
        tableR1.AddCell(New Phrase("CUADRO RESUMEN", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))

        ' Bienes en GPS
        'La Oficina tiene asignado en el sistema antes del inventario, 263 ítems

        For Each cell As PdfPCell In tableR1.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        tableR1.TotalWidth = 1000

        tableR1.WriteSelectedRows(0, tableR1.Rows.Count, 60, 780, writer.DirectContent)



        Dim tableR As New PdfPTable(4) ' Crear una tabla con 2 columnas
        Dim widthsR As Single() = {5.0F, 5.0F, 5.0F, 5.0F} '' Establecer el estilo de borde de la tabla
        tableR.SetWidths(widthsR)

        Dim celda1 As New PdfPCell(New Phrase("BIENES GPS", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda1.BackgroundColor = New BaseColor(90, 128, 174) ' Establecer el color que desees
        celda1.FixedHeight = 50.0F
        celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda1.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda1)
        Dim celda2 As New PdfPCell(New Phrase("BIENES INVENTARIADOS", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda2.BackgroundColor = New BaseColor(90, 128, 174)
        celda2.FixedHeight = 50.0F
        celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda2.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda2)
        Dim celda3 As New PdfPCell(New Phrase("BIENES FALTANTES", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda3.BackgroundColor = New BaseColor(90, 128, 174)
        celda3.FixedHeight = 50.0F
        celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda3.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda3)
        Dim celda4 As New PdfPCell(New Phrase("SOBRANTES", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda4.BackgroundColor = New BaseColor(90, 128, 174)
        celda4.FixedHeight = 50.0F
        celda4.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda4.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda4)

        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "12", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda5 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda5.FixedHeight = 40.0F
        celda5.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda5.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda5)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "13", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda6 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda6.FixedHeight = 40.0F
        celda6.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda6.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda6)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "2", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda7 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda7.FixedHeight = 40.0F
        celda7.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda7.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda7)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "7", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda8 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda8.FixedHeight = 40.0F
        celda8.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda8.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR.AddCell(celda8)

        tableR.TotalWidth = 1000

        tableR.WriteSelectedRows(0, tableR.Rows.Count, 60, 700, writer.DirectContent)


        document.NewPage()
        table2.DeleteBodyRows()
        image2.SetAbsolutePosition(0, 0)
        image2.ScaleAbsolute(20, VarpageSize.Height)
        document.Add(image2)
        ' Asignar el evento al escritor
        writer.PageEvent = eventHandler

        tableR1.DeleteBodyRows()
        tableR1.AddCell(New Phrase("CONSOLIDACIÓN DE BIENES", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        For Each cell As PdfPCell In tableR1.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        tableR1.TotalWidth = 1000
        tableR1.WriteSelectedRows(0, tableR1.Rows.Count, 60, 750, writer.DirectContent)

        tableR1.DeleteBodyRows()
        tableR1.AddCell(New Phrase("CUADRO FINAL", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.BOLD, myColor)))
        For Each cell As PdfPCell In tableR1.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        tableR1.TotalWidth = 1000
        tableR1.WriteSelectedRows(0, tableR1.Rows.Count, 60, 700, writer.DirectContent)

        ' Bienes en GPS
        'La Oficina tiene asignado en el sistema antes del inventario, 263 ítems


        Dim tableR3 As New PdfPTable(7) ' Crear una tabla con 2 columnas
        Dim widthsR3 As Single() = {3.0F, 3.0F, 3.0F, 4.0F, 3.0F, 3.0F, 2.0F} '' Establecer el estilo de borde de la tabla
        tableR3.SetWidths(widthsR3)
        Dim celda11 As New PdfPCell(New Phrase("BIENES GPS", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda11.BackgroundColor = New BaseColor(90, 128, 174) ' Establecer el color que desees
        celda11.FixedHeight = 50.0F
        celda11.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda11.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda11)
        Dim celda21 As New PdfPCell(New Phrase("BIENES INVENTARIADOS", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda21.BackgroundColor = New BaseColor(90, 128, 174)
        celda21.FixedHeight = 50.0F
        celda21.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda21.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda21)
        Dim celda31 As New PdfPCell(New Phrase("BIENES FALTANTES", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda31.BackgroundColor = New BaseColor(90, 128, 174)
        celda31.FixedHeight = 50.0F
        celda31.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda31.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda31)
        Dim celda311 As New PdfPCell(New Phrase("ENCONTRADOS EN OTRA UBICACIÓN", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda311.BackgroundColor = New BaseColor(90, 128, 174)
        celda311.FixedHeight = 50.0F
        celda311.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda311.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda311)
        Dim celda312 As New PdfPCell(New Phrase("ENCONTRADOS POR SERIE", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda312.BackgroundColor = New BaseColor(90, 128, 174)
        celda312.FixedHeight = 50.0F
        celda312.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda312.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda312)
        Dim celda41 As New PdfPCell(New Phrase("SOBRANTES", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda41.BackgroundColor = New BaseColor(90, 128, 174)
        celda41.FixedHeight = 50.0F
        celda41.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda41.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda41)
        Dim celda411 As New PdfPCell(New Phrase("POR PLACAR", New Font(Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda411.BackgroundColor = New BaseColor(90, 128, 174)
        celda411.FixedHeight = 50.0F
        celda411.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda411.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda411)

        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "12", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda51 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda51.FixedHeight = 40.0F
        celda51.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda51.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda51)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "13", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda61 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda61.FixedHeight = 40.0F
        celda61.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda61.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda61)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "2", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda71 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda71.FixedHeight = 40.0F
        celda71.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda71.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda71)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "3", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda711 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda711.FixedHeight = 40.0F
        celda711.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda711.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda711)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "5", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda712 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda712.FixedHeight = 40.0F
        celda712.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda712.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda712)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "7", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda81 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda81.FixedHeight = 40.0F
        celda81.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda81.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda81)
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "20", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
        psTotalGps = dt.Rows.Count
        Dim celda713 As New PdfPCell(New Phrase(psTotalGps, New Font(Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        celda713.FixedHeight = 40.0F
        celda713.HorizontalAlignment = PdfPCell.ALIGN_CENTER
        celda713.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
        tableR3.AddCell(celda713)

        tableR3.TotalWidth = 1000
        tableR3.WriteSelectedRows(0, tableR3.Rows.Count, 60, 650, writer.DirectContent)

        tableR1.DeleteBodyRows()
        tableR1.AddCell(New Phrase("Nota: Los bienes faltantes, sobrantes y encontrados en otras Ubicaciones se trasladarán en GPS al centro correspondiente.", New Font(Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
        For Each cell As PdfPCell In tableR1.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        tableR1.TotalWidth = 1000
        tableR1.WriteSelectedRows(0, tableR1.Rows.Count, 60, 550, writer.DirectContent)

        tableR1.DeleteBodyRows()
        tableR1.AddCell(New Phrase(" COMENTARIOS, CONCLUSIONES Y RECOMENDACIONES:", New Font(Font.FontFamily.HELVETICA, 20, iTextSharp.text.Font.NORMAL, myColor)))
        For Each cell As PdfPCell In tableR1.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        tableR1.TotalWidth = 1000
        tableR1.WriteSelectedRows(0, tableR1.Rows.Count, 60, 500, writer.DirectContent)

        '•	La oficina fue inventariada sin problemas.
        '•	No se tuvo acceso a la Bóveda ni al cuarto de Limpieza
        '•	Se deben replacar 57 ítems.
        '•	Se entrega el inventario final de la Oficina Las Begonias
        '•	La conciliación es operativa con toda la base del activo fijo.

        ' Cerrar el documento
        document.Close()

        ' Descargar el PDF generado
        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.TransmitFile(fullPath)
        Response.End()

    End Sub

    Private Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click
        Dim dt As New DataTable()
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim objdatos As New Cls_Inventario_Verificacion
        ' Configurar los datos en dt1 y dt2...
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "20", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))

        Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
        Dim fileName As String = "Bienes_xPlacar_" & TxtCodigo.Text & ".txt"
        Dim fullPath As String = Path.Combine(savePath, fileName)
        If dt.Rows.Count > 0 Then
            Using fileWriter As New System.IO.StreamWriter(fullPath)
                For Each dr As DataRow In dt.Rows
                    fileWriter.Write(Nz(dr("PLACA_ORIGINAL")).ToString() & vbTab)
                    fileWriter.WriteLine()
                Next
            End Using
        End If

        Response.Clear()
        Response.ContentType = "application/txt"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
        Response.TransmitFile(fullPath)
        Response.End()

    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click
        'Imprimir_placas()
        'Reporte_placas()

        Dim barcodeWriter As New BarcodeWriter()
        barcodeWriter.Format = BarcodeFormat.CODE_128

        Dim encodingOptions As New EncodingOptions()
        encodingOptions.PureBarcode = True
        barcodeWriter.Options = encodingOptions

        Dim imagePath As String = Server.MapPath("~/Inventario/Imagenes/LOGO WEB HACDATA.jpg")
        Dim imagePath2 As String = Server.MapPath("~/Inventario/Imagenes/LOGO WEB HACDATA.jpg")

        Dim image As Image = Image.GetInstance(imagePath)
        Dim image2 As Image = Image.GetInstance(imagePath2)

        ' Guardar los códigos de barras en MemoryStreams
        Using barcodeStream1 As New MemoryStream(), barcodeStream2 As New MemoryStream()

            ' Crear un documento PDF con márgenes ajustados
            Dim customWidth As Single = 8.4 * 28.35 ' 25 cm * 28.35 puntos por cm
            Dim customHeight As Single = 2 * 28.35 ' 18 cm * 28.35 puntos por cm
            Dim customPageSize As New Rectangle(customWidth, customHeight)
            Dim leftMargin As Single = 0.1 * 28.35 ' 0.5 inch margin
            Dim rightMargin As Single = 0.1 * 28.35 ' 0.5 inch margin
            Dim topMargin As Single = 0.1 * 28.35 ' 0.5 inch margin
            Dim bottomMargin As Single = 0.1 * 28.35 ' 0.5 inch margin

            ' Crear un documento PDF
            Dim pdfDoc As New Document(customPageSize, leftMargin, rightMargin, topMargin, bottomMargin)

            'Dim pdfDoc As New Document(PageSize.B9)


            Dim text1 As New Paragraph("     BBVA", New Font(Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK))
            Dim text2 As New Paragraph("         BBVA", New Font(Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK))



            Dim i As Double = 0
            Dim psPlacaIni As Double = 0
            Dim psPlacaFin As Double = 0
            'psPlacaIni = 4092678
            i = psPlacaIni
            If psPlacaFin = 0 Then i = psPlacaIni
            'psPlacaFin = 4092684
            Using memoryStream As New MemoryStream()
                Dim writer = PdfWriter.GetInstance(pdfDoc, memoryStream)

                pdfDoc.Open()

                For i = 0 To gvListaBienesxPlacar.Rows.Count - 1
                    psPlacaIni = Nz(gvListaBienesxPlacar.Rows(i).Cells(7).Text)
                    If i + 1 <= gvListaBienesxPlacar.Rows.Count - 1 Then
                        psPlacaFin = Nz(gvListaBienesxPlacar.Rows(i + 1).Cells(7).Text)
                    Else
                        psPlacaFin = 0
                    End If

                    image.SetAbsolutePosition(70, 42)
                    image.ScaleAbsolute(30, 5)
                    'pdfDoc.Add(image)

                    image2.SetAbsolutePosition(195, 42)
                    image2.ScaleAbsolute(30, 5)

                    Dim result1 = barcodeWriter.Write(psPlacaIni)
                    Dim result2 = barcodeWriter.Write(psPlacaFin)
                    result1.Save(barcodeStream1, System.Drawing.Imaging.ImageFormat.Png)
                    result2.Save(barcodeStream2, System.Drawing.Imaging.ImageFormat.Png)
                    barcodeStream1.Position = 0
                    barcodeStream2.Position = 0

                    Dim barcodeImage1 = Image.GetInstance(barcodeStream1.ToArray())
                    barcodeImage1.ScaleToFit(50, 50) ' Ajustar el tamaño de la imagen del código de barras 2
                    barcodeImage1.Alignment = Element.ALIGN_CENTER ' Alinear la imagen a la izquierda

                    barcodeImage1.SetAbsolutePosition(5, 20)
                    barcodeImage1.ScaleAbsolute(100, 16)

                    Dim barcodeImage2 = Image.GetInstance(barcodeStream2.ToArray())
                    barcodeImage2.ScaleToFit(50, 50) ' Ajustar el tamaño de la imagen del código de barras 2
                    barcodeImage2.Alignment = Element.ALIGN_CENTER ' Alinear la imagen a la izquierda

                    barcodeImage2.SetAbsolutePosition(130, 20)
                    barcodeImage2.ScaleAbsolute(100, 16)
                    pdfDoc.Add(barcodeImage1)

                    ' Crear una tabla con dos columnas

                    Dim table As New PdfPTable(2)
                    table.WidthPercentage = 100
                    table.SetWidths(New Single() {0.5F, 0.5F})

                    ' Añadir las celdas con texto y códigos de barras
                    Dim cell1 As New PdfPCell()
                    cell1.Border = PdfPCell.NO_BORDER
                    cell1.AddElement(text1)
                    'cell1.AddElement(barcodeImage1)
                    table.AddCell(cell1)

                    Dim cell2 As New PdfPCell()
                    cell2.Border = PdfPCell.NO_BORDER
                    If i + 1 <= gvListaBienesxPlacar.Rows.Count - 1 Then cell2.AddElement(text2)
                    table.AddCell(cell2)

                    If i + 1 <= gvListaBienesxPlacar.Rows.Count - 1 Then
                        pdfDoc.Add(barcodeImage2)
                        'pdfDoc.Add(image2)
                    End If

                    pdfDoc.Add(table)

                    ' Añadir texto centrado en una posición específica
                    Dim cb As PdfContentByte = writer.DirectContent
                    Dim baseFont As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED)
                    cb.BeginText()
                    cb.SetFontAndSize(baseFont, 12)
                    cb.ShowTextAligned(Element.ALIGN_CENTER, psPlacaIni.ToString(), 55, 10, 0) ' Coordenadas (x, y)
                    cb.EndText()
                    If psPlacaFin > 0 Then
                        cb.BeginText()
                        cb.SetFontAndSize(baseFont, 12)
                        cb.ShowTextAligned(Element.ALIGN_CENTER, psPlacaFin.ToString(), 180, 10, 0) ' Coordenadas (x, y)
                        cb.EndText()
                    End If

                    If i + 2 < gvListaBienesxPlacar.Rows.Count - 1 Then
                        pdfDoc.NewPage()
                        i = i + 1
                    ElseIf i + 1 < gvListaBienesxPlacar.Rows.Count - 1 Then
                        pdfDoc.NewPage()
                        i = i + 1
                    ElseIf i + 1 = gvListaBienesxPlacar.Rows.Count - 1 Then
                        i = i + 1
                    End If
                Next

                'cerrar el documento pdf
                pdfDoc.Close()
                ' Enviar el PDF al cliente
                Response.ContentType = "application/pdf"
                Response.AddHeader("Content-Disposition", "attachment;filename=Barcodes.pdf")
                Response.BinaryWrite(memoryStream.ToArray())
                Response.End()
            End Using
        End Using


    End Sub

    Private Sub Reporte_placas()

        Dim dt As New DataTable()
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim objdatos As New Cls_Inventario_Verificacion
        ' Configurar los datos en dt1 y dt2...
        dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "20", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))

        Dim cantidadEtiquetas As Integer = 5

        ' Crear etiquetas
        For Each dr As DataRow In dt.Rows
            Dim etiqueta As New Label()
            etiqueta.CssClass = "etiqueta"


            ' Generar código de barras (puedes cambiar BarcodeFormat según tus necesidades)
            Dim codigoBarras As String = ""
            'GenerarCodigoBarras(Nu(dr("placa_nro")), BarcodeFormat.CODE_128, 150, 50)

            ' Crear etiqueta div
            Dim divCodigoBarras As New HtmlGenericControl("div")
            divCodigoBarras.Attributes("class") = "codigo-barras"
            divCodigoBarras.InnerHtml = codigoBarras

            ' Agregar la etiqueta div al contenedor
            etiqueta.Controls.Add(divCodigoBarras)


            ' Agregar la etiqueta al contenedor
            phEtiquetas.Controls.Add(etiqueta)
        Next
    End Sub

    Private Sub Imprimir_placas()
        Try
            Dim I As Integer = 1
            Dim dt As New DataTable()
            Dim psCodInv As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                psCodInv = Nz(DdlInventario.SelectedValue)
            End If
            Dim psUbicaCodigo As Double = 0
            psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
            Dim objdatos As New Cls_Inventario_Verificacion
            ' Configurar los datos en dt1 y dt2...
            dt = objdatos.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", "20", IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))

            Dim pplaCommands As New StringBuilder()

            ' Inicializar el documento PPLA
            pplaCommands.AppendLine("L") ' Comando de inicio de impresió
            If dt.Rows.Count > 0 Then
                ' Crear etiquetas
                I = I + 1
                For Each dr As DataRow In dt.Rows
                    ' Contenido de la etiqueta
                    Dim contenido As String = "P" & (100 * I) & ",100,0,10,1,1,N," & Nu(dr("placa_nro"))

                    ' Generar código de barras
                    Dim codigoBarras As String = "" ' "B" & (100 * I) & ",200,0,1,2,2,100,N," & GenerarCodigoBarras(Nu(dr("placa_nro")), BarcodeFormat.CODE_128, 200, 50)
                    'Dim comandoTexto As String = $"A100,100,0,3,1,1,N,""Producto {i}"""
                    ' Agregar comando PPLA para imprimir el contenido y el código de barras
                    pplaCommands.AppendLine(contenido)
                    pplaCommands.AppendLine(codigoBarras)
                Next


            End If
            ' Finalizar el documento PPLA
            pplaCommands.AppendLine("P1") ' Comando de impresión
            pplaCommands.AppendLine("E")  ' Comando de fin de impresión


            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "Etiquetas_" & TxtCodigo.Text & ".ppla"
            Dim fullPath As String = Path.Combine(savePath, fileName)
            ' Guardar los comandos PPLA en un archivo temporal
            File.WriteAllText(fullPath, pplaCommands.ToString())

            ' Enviar el archivo PPLA a la impresora
            EnviarPPLALaImpresora(fullPath)

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la base de datos: " & ex.Message & " .');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la aplicacion: " & ex.Message & " .');", True)
        End Try
    End Sub

    Private Sub EnviarPPLALaImpresora(pplaPath As String)
        ' Nombre de la impresora de etiquetas
        Dim impresoraNombre As String = "\\ARGOX OS-2140 PPLA (Copiar 1)" ' en USB003"

        ' Imprimir el archivo PPLA
        Dim printCommand As String = "COPY " & pplaPath & " " & impresoraNombre
        EjecutarComando(printCommand)

        ' Eliminar el archivo temporal
        'File.Delete(pplaPath)
    End Sub

    Private Sub EjecutarComando(command As String)
        Dim processInfo As New ProcessStartInfo("cmd.exe", "/C " & command)
        processInfo.CreateNoWindow = True
        processInfo.UseShellExecute = False
        processInfo.RedirectStandardError = True
        processInfo.RedirectStandardOutput = True

        Using process As New Process()
            process.StartInfo = processInfo
            process.Start()
            process.WaitForExit()
        End Using
    End Sub

    Private Sub ImprimirPlaca_Pdf()

    End Sub

    Private Sub RbTodos_CheckedChanged(sender As Object, e As EventArgs) Handles RbTodos.CheckedChanged
        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        LblUbicaCodigoInv.Text = ""
        TxtCodigo.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvListaBienesxPlacar.DataSource = dt
        gvListaBienesxPlacar.DataBind()
        lblCantRegistro.Text = ""
        gvDetalle.DataSource = dt
        gvDetalle.DataBind()
        accordion.Visible = False
        LblRegistro2.Text = ""
    End Sub

    Private Sub BtnListarPlacdos_Click(sender As Object, e As EventArgs) Handles BtnListarPlacdos.Click

        LblRegistro2.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim psUbicacionDescripcion As String = ""
        If ddlUbicacion.SelectedValue <> "< Seleccionar >" Then psUbicacionDescripcion = ddlUbicacion.SelectedItem.Text
        If psUbicacionDescripcion <> "" Then psUbicacionDescripcion = psUbicacionDescripcion.Substring(8)

        Dim psEstado As String = ""
        Dim pdSerieNumerar As Double = 0
        lblError.Text = ""
        Try
            GvListaVerificarInventario.DataSource = Nothing
            GvListaVerificarInventario.DataBind()
            gvDetalle.DataSource = Nothing
            gvDetalle.DataBind()

            dt = obj.Inventariados_Bienes_xPlacar(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, psUbicacionDescripcion, IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")))
            gvListaBienesxPlacar.DataSource = dt
            gvListaBienesxPlacar.DataBind()
            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
                lblCantRegistro.Text = "Hay " & dt.Rows.Count & " registros."
                accordion.Visible = True
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
                lblCantRegistro.Text = "Hay 1 registro."
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la base de datos: " & ex.Message & " .');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la aplicacion: " & ex.Message & " .');", True)
        End Try

    End Sub

    Protected Sub TxtPlacaNro_TextChanged(sender As Object, e As EventArgs) Handles TxtPlacaNro.TextChanged

        GvListaVerificarInventario.DataSource = Nothing
        GvListaVerificarInventario.DataBind()
        gvDetalle.DataSource = Nothing
        gvDetalle.DataBind()
        gvListaBienesxPlacar.DataSource = Nothing
        gvListaBienesxPlacar.DataBind()
        gvResumen.DataSource = Nothing
        gvResumen.DataBind()
        LblRegistro.Text = ""
        LblRegistro2.Text = ""
        lblCantRegistro.Text = ""
        If TxtPlacaNro.Text <> "" Then

            Dim obj As New Cls_Inventario_Verificacion
            Dim dt As New DataTable
            Dim psCodInv As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                psCodInv = Nz(DdlInventario.SelectedValue)
            End If
            Dim psUbicaCodigo As Double = 0
            psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
            Dim psUbicacionDescripcion As String = ""
            If ddlUbicacion.SelectedValue <> "< Seleccionar >" Then psUbicacionDescripcion = ddlUbicacion.SelectedItem.Text
            If psUbicacionDescripcion <> "" Then psUbicacionDescripcion = psUbicacionDescripcion.Substring(8)

            Dim psEstado As String = ""
            Dim pdSerieNumerar As Double = 0
            lblError.Text = ""
            Dim pdPlacaNro As Double = 0
            If TxtPlacaNro.Text <> "" Then pdPlacaNro = Nz(TxtPlacaNro.Text)
            Try

                dt = obj.Inventariados_Bienes_xPlaca(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, psUbicacionDescripcion, IIf(RBAlmacen.Checked = True, "1", IIf(RBCentroC.Checked = True, "2", "")), pdPlacaNro)
                gvListaxPlaca.DataSource = dt
                gvListaxPlaca.DataBind()
                gvListaxPlaca.Visible = False
                If dt.Rows.Count > 1 Then
                    gvListaxPlaca.Visible = True
                    LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
                ElseIf dt.Rows.Count = 1 Then
                    gvListaxPlaca.Visible = True
                    LblRegistro2.Text = "Hay 1 registro."
                End If
                TxtPlacaNro.Focus()
            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la base de datos: " & ex.Message & " .');", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un erro en la aplicacion: " & ex.Message & " .');", True)
            End Try

        End If
    End Sub

    Private Sub gvListaxPlaca_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvListaxPlaca.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pdPlacaNro As Double = 0
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(DdlInventario.SelectedValue)
        End If
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim pdSerieNumerar As Double = 0
        Dim psEstado As String = ""
        If e.CommandName = "Placado" Then
            pdPlacaNro = Nz(gvListaxPlaca.Rows(Index).Cells(8).Text)
            psEstado = gvListaxPlaca.Rows(Index).Cells(9).Text
            If UCase(psEstado) <> "PLACADO" Then
                Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim Rs As SqlDataReader
                Cn.Open() : CmdGlobal.Connection = Cn

                CmdGlobal.CommandText = " SELECT INVDET_SERIE_NUMERAR FROM TBINVENTARIO_DETALLE WHERE INVDET_INVENTUBIC_CODIGO = " & psUbicaCodigo & " AND INVDET_PLACA_NRO = " & pdPlacaNro
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pdSerieNumerar = Nz(Rs(0))
                    End While
                End If
                Rs.Close() '   TBINVENTARIO_VERIFICACION
                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_SERIE_ESTADO_EQUIPO = '8' WHERE INVDET_INVENTUBIC_CODIGO = " & psUbicaCodigo & " AND INVDET_SERIE_NUMERAR = " & pdSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_ESTADO_BIEN = '8' WHERE INVENTUBIC_CODIGO = " & psUbicaCodigo & " AND VERIF_SERIE_NUMERAR = " & pdSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                If pdSerieNumerar > 0 Then
                    CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_0001 SET SERIE_ESTADO = '8' WHERE SERIE_NUMERAR = " & pdSerieNumerar
                    CmdGlobal.ExecuteNonQuery()
                End If
                Cn.Close()
            End If
        End If
        TxtPlacaNro.Text = pdPlacaNro
        TxtPlacaNro_TextChanged(sender, e)
    End Sub

    Protected Sub BtnCargaArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargaArchivo.Click
        If fileUpload.HasFile Then
            ' Obtiene el nombre del archivo y su extensión
            Dim fileName As String = Path.GetFileName(fileUpload.PostedFile.FileName)
            Dim fileExtension As String = Path.GetExtension(fileName)
            Dim dt As New DataTable
            Dim drT As DataRow
            dt.Columns.Add("PLACA_NRO")


            ' Verifica que el archivo sea un archivo de texto
            If fileExtension.ToLower() = ".txt" Then
                ' Lee el contenido del archivo de texto
                Dim lines As String() = Nothing
                Using reader As New StreamReader(fileUpload.PostedFile.InputStream)
                    lines = reader.ReadToEnd().Split(New String() {Environment.NewLine}, StringSplitOptions.None)
                End Using

                ' StringBuilder para almacenar los resultados
                Dim result As New System.Text.StringBuilder()

                ' Procesar los registros de dos en dos
                For i As Integer = 0 To lines.Length - 1
                    ' Tomar dos registros consecutivos
                    Dim firstRecord As String = lines(i)
                    If firstRecord <> "" Then
                        drT = dt.NewRow()
                        drT("PLACA_NRO") = firstRecord
                        dt.Rows.Add(drT)
                    End If
                Next
                Generar_Placas(dt)

            Else
                Session("Fin") = ""
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El archivo seleccionado no es un archivo de texto válido.');", True)
            End If
        Else
            Session("Fin") = ""
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Por favor, selecciona un archivo.');", True)
        End If
    End Sub

    Private Sub Generar_Placas(ByVal var_dt As DataTable)
        Dim barcodeWriter As New BarcodeWriter()
        barcodeWriter.Format = BarcodeFormat.CODE_128

        Dim encodingOptions As New EncodingOptions()
        encodingOptions.PureBarcode = True
        barcodeWriter.Options = encodingOptions

        Dim imagePath As String = Server.MapPath("~/Inventario/Imagenes/LOGO WEB HACDATA.jpg")
        Dim imagePath2 As String = Server.MapPath("~/Inventario/Imagenes/LOGO WEB HACDATA.jpg")

        Dim image As Image = Image.GetInstance(imagePath)
        Dim image2 As Image = Image.GetInstance(imagePath2)

        ' Guardar los códigos de barras en MemoryStreams
        Using barcodeStream1 As New MemoryStream(), barcodeStream2 As New MemoryStream()

            ' Crear un documento PDF con márgenes ajustados
            Dim customWidth As Single = 8.4 * 28.35 ' 25 cm * 28.35 puntos por cm
            Dim customHeight As Single = 2 * 28.35 ' 18 cm * 28.35 puntos por cm
            Dim customPageSize As New Rectangle(customWidth, customHeight)
            Dim leftMargin As Single = 0.1 * 28.35 ' 0.5 inch margin
            Dim rightMargin As Single = 0.1 * 28.35 ' 0.5 inch margin
            Dim topMargin As Single = 0.1 * 28.35 ' 0.5 inch margin
            Dim bottomMargin As Single = 0.1 * 28.35 ' 0.5 inch margin

            ' Crear un documento PDF
            Dim pdfDoc As New Document(customPageSize, leftMargin, rightMargin, topMargin, bottomMargin)

            'Dim pdfDoc As New Document(PageSize.B9)


            Dim text1 As New Paragraph("     BBVA", New Font(Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK))
            Dim text2 As New Paragraph("         BBVA", New Font(Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK))



            Dim i As Double = 0
            Dim psPlacaIni As Double = 0
            Dim psPlacaFin As Double = 0
            'psPlacaIni = 4092678
            i = psPlacaIni
            If psPlacaFin = 0 Then i = psPlacaIni

            Dim pdCantArchivo As Double = 0
            pdCantArchivo = var_dt.Rows.Count

            'psPlacaFin = 4092684
            Using memoryStream As New MemoryStream()
                Dim writer = PdfWriter.GetInstance(pdfDoc, memoryStream)

                pdfDoc.Open()

                For i = 0 To var_dt.Rows.Count - 1
                    Dim firstRow As DataRow = var_dt.Rows(i)

                    ' Acceder a los datos de las filas
                    Dim id1 As Double = Nz(firstRow("PLACA_NRO"))

                    psPlacaIni = id1


                    psPlacaIni = psPlacaIni
                    If i + 1 <= var_dt.Rows.Count - 1 Then
                        Dim secondRow As DataRow = var_dt.Rows(i + 1)
                        Dim id2 As Double = secondRow("PLACA_NRO")
                        psPlacaFin = psPlacaFin
                        psPlacaFin = id2
                    Else
                        psPlacaFin = 0
                    End If

                    image.SetAbsolutePosition(70, 42)
                    image.ScaleAbsolute(30, 5)
                    'pdfDoc.Add(image)

                    image2.SetAbsolutePosition(195, 42)
                    image2.ScaleAbsolute(30, 5)

                    Dim result1 = barcodeWriter.Write(psPlacaIni)
                    Dim result2 = barcodeWriter.Write(psPlacaFin)
                    result1.Save(barcodeStream1, System.Drawing.Imaging.ImageFormat.Png)
                    result2.Save(barcodeStream2, System.Drawing.Imaging.ImageFormat.Png)
                    barcodeStream1.Position = 0
                    barcodeStream2.Position = 0

                    Dim barcodeImage1 = Image.GetInstance(barcodeStream1.ToArray())
                    barcodeImage1.ScaleToFit(50, 50) ' Ajustar el tamaño de la imagen del código de barras 2
                    barcodeImage1.Alignment = Element.ALIGN_CENTER ' Alinear la imagen a la izquierda

                    barcodeImage1.SetAbsolutePosition(5, 20)
                    barcodeImage1.ScaleAbsolute(100, 16)

                    Dim barcodeImage2 = Image.GetInstance(barcodeStream2.ToArray())
                    barcodeImage2.ScaleToFit(50, 50) ' Ajustar el tamaño de la imagen del código de barras 2
                    barcodeImage2.Alignment = Element.ALIGN_CENTER ' Alinear la imagen a la izquierda

                    barcodeImage2.SetAbsolutePosition(130, 20)
                    barcodeImage2.ScaleAbsolute(100, 16)
                    pdfDoc.Add(barcodeImage1)

                    ' Crear una tabla con dos columnas
                    Dim table As New PdfPTable(2)
                    table.WidthPercentage = 100
                    table.SetWidths(New Single() {0.5F, 0.5F})

                    ' Añadir las celdas con texto y códigos de barras
                    Dim cell1 As New PdfPCell()
                    cell1.Border = PdfPCell.NO_BORDER
                    cell1.AddElement(text1)
                    'cell1.AddElement(barcodeImage1)
                    table.AddCell(cell1)

                    Dim cell2 As New PdfPCell()
                    cell2.Border = PdfPCell.NO_BORDER
                    If i + 1 <= var_dt.Rows.Count - 1 Then cell2.AddElement(text2)
                    table.AddCell(cell2)

                    If i + 1 <= var_dt.Rows.Count - 1 Then
                        pdfDoc.Add(barcodeImage2)
                        'pdfDoc.Add(image2)
                    End If

                    pdfDoc.Add(table)

                    ' Añadir texto centrado en una posición específica
                    Dim cb As PdfContentByte = writer.DirectContent
                    Dim baseFont As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED)
                    cb.BeginText()
                    cb.SetFontAndSize(baseFont, 12)
                    cb.ShowTextAligned(Element.ALIGN_CENTER, psPlacaIni.ToString(), 55, 10, 0) ' Coordenadas (x, y)
                    cb.EndText()
                    If psPlacaFin > 0 Then
                        cb.BeginText()
                        cb.SetFontAndSize(baseFont, 12)
                        cb.ShowTextAligned(Element.ALIGN_CENTER, psPlacaFin.ToString(), 180, 10, 0) ' Coordenadas (x, y)
                        cb.EndText()
                    End If

                    If i + 2 < var_dt.Rows.Count - 1 Then
                        pdfDoc.NewPage()
                        i = i + 1
                    ElseIf i + 1 < var_dt.Rows.Count - 1 Then
                        pdfDoc.NewPage()
                        i = i + 1
                    ElseIf i + 1 = var_dt.Rows.Count - 1 Then
                        i = i + 1
                    End If

                Next



                'cerrar el documento pdf
                pdfDoc.Close()
                ' Enviar el PDF al cliente
                Response.ContentType = "application/pdf"
                Response.AddHeader("Content-Disposition", "attachment;filename=Placa.pdf")
                Response.BinaryWrite(memoryStream.ToArray())
                Response.End()
            End Using
        End Using

    End Sub
End Class
