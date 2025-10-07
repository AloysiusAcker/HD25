Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports ZXing
Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports ZXing.Common

Partial Class Inventario_Inventario_Imprimir_Placas
    Inherits System.Web.UI.Page
    Protected Sub btnGenerate_Click(sender As Object, e As EventArgs)
        ' Generar código de barras
        'Dim result = barcodeWriter.Write("1234567890")
        If TxtPlacaIni.Text = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar placa a imprimir');", True)
        Else

            ' Guardar el código de barras en un MemoryStream
            ' Generar códigos de barras

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
                psPlacaIni = Nz(TxtPlacaIni.Text)
                psPlacaFin = Nz(TxtPlacaFin.Text)
                'psPlacaIni = 4092678
                i = psPlacaIni
                If psPlacaFin = 0 Then i = psPlacaIni
                'psPlacaFin = 4092684
                Using memoryStream As New MemoryStream()
                    Dim writer = PdfWriter.GetInstance(pdfDoc, memoryStream)

                    pdfDoc.Open()
continuar:

                    image.SetAbsolutePosition(70, 42)
                    image.ScaleAbsolute(30, 5)
                    'pdfDoc.Add(image)

                    image2.SetAbsolutePosition(195, 42)
                    image2.ScaleAbsolute(30, 5)

                    Dim result1 = barcodeWriter.Write(i)
                    Dim result2 = barcodeWriter.Write(i + 1)
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
                    table.AddCell(cell1)

                    Dim cell2 As New PdfPCell()
                    cell2.Border = PdfPCell.NO_BORDER
                    If i + 1 <= psPlacaFin Then cell2.AddElement(text2)
                    table.AddCell(cell2)

                    If i + 1 <= psPlacaFin Then
                        pdfDoc.Add(barcodeImage2)
                        'pdfDoc.Add(image2)
                    End If

                    pdfDoc.Add(table)

                    ' Añadir texto centrado en una posición específica
                    Dim cb As PdfContentByte = writer.DirectContent
                    Dim baseFont As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.WINANSI, BaseFont.EMBEDDED)
                    cb.BeginText()
                    cb.SetFontAndSize(baseFont, 12)
                    cb.ShowTextAligned(Element.ALIGN_CENTER, i.ToString(), 55, 10, 0) ' Coordenadas (x, y)
                    cb.EndText()
                    If i + 1 <= psPlacaFin Then
                        cb.BeginText()
                        cb.SetFontAndSize(baseFont, 12)
                        cb.ShowTextAligned(Element.ALIGN_CENTER, (i + 1).ToString(), 180, 10, 0) ' Coordenadas (x, y)
                        cb.EndText()
                    End If


                    ' Añadir la tabla al documento PDF
                    If i + 2 < psPlacaFin Then
                        pdfDoc.NewPage()
                        i = i + 2
                        GoTo continuar
                    ElseIf i + 1 < psPlacaFin Then
                        pdfDoc.NewPage()
                        i = i + 2
                        GoTo continuar
                    End If

                    'cerrar el documento pdf
                    pdfDoc.Close()
                    ' Enviar el PDF al cliente
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("Content-Disposition", "attachment;filename=Barcodes.pdf")
                    Response.BinaryWrite(memoryStream.ToArray())
                    Response.End()
                End Using
            End Using

        End If
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
