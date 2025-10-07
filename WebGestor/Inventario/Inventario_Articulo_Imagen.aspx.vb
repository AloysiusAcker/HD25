Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports ClosedXML.Excel
Imports System.Net
Imports ClosedXML.Excel.Drawings
Imports System.Drawing.Imaging

Partial Class Inventario_Inventario_Articulo_Imagen
    Inherits System.Web.UI.Page

    Protected Sub btnExportImages_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'Dim folderPath As String = Server.MapPath("~/Inventario/ArchivoTemp/IMAGENES INFORMATICA/")
            'SaveImagesToDatabase(folderPath)

            'Dim folderPath2 As String = Server.MapPath("~/Inventario/ArchivoTemp/IMAGENES MOBILIARIO/")
            'SaveImagesToDatabase(folderPath2)

            Dim folderPath3 As String = Server.MapPath("~/Inventario/ArchivoTemp/CATALOGO IMAGENES FALTANTES/")
            SaveImagesToDatabase(folderPath3)
            ''Dim imagesList As List(Of ImageData) = GetImagesFromDatabase()
            'ExportImagesToExcel(imagesList)
            If GvLista.Rows.Count > 0 Then
                ExportGridViewToExcel()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Terminó la exportada de imágenes')", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Listar antes de Exportar')", True)
            End If



            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Terminó la carga de imágenes')", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Sub SaveImagesToDatabase(ByVal folderPath As String)
        Dim connectionString As String = Session("Ruta_Emp")

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Dim imageFiles As String() = Directory.GetFiles(folderPath, "*.*").Where(Function(s) s.EndsWith(".jpg") OrElse s.EndsWith(".png") OrElse s.EndsWith(".webp") OrElse s.EndsWith(".jpeg")).ToArray() ' Cambia la extensión según tus imágenes

            For Each filePath As String In imageFiles
                Dim fileName As String = Path.GetFileNameWithoutExtension(filePath)
                Dim imageData As Byte() = File.ReadAllBytes(filePath)

                Dim query As String = "UPDATE TBINV_ARTICULOS_IMAGENES SET ART_IMAGEN = @ImageData WHERE ART_SKU = @Description"

                Using command As New SqlCommand(query, connection)
                    command.Parameters.AddWithValue("@ImageData", imageData)
                    command.Parameters.AddWithValue("@Description", fileName)
                    command.ExecuteNonQuery()
                End Using
            Next
        End Using
    End Sub
    Private Function GetImagesFromDatabase() As List(Of ImageData)
        Dim imagesList As New List(Of ImageData)()

        Using connection As New SqlConnection(Session("Ruta_Emp"))
            connection.Open()
            Dim command As New SqlCommand("SELECT ART_CODIGO, ART_DESCRIPCION, ART_IMAGEN FROM TBINV_ARTICULOS_IMAGENES ", connection)
            Using reader As SqlDataReader = command.ExecuteReader()
                While reader.Read()
                    Dim id As Integer = reader.GetDouble(0)
                    Dim imageName As String = reader.GetString(1)
                    Dim imageData As Byte() = If(reader.IsDBNull(reader.GetOrdinal("ART_IMAGEN")), Nothing, CType(reader("ART_IMAGEN"), Byte()))
                    imagesList.Add(New ImageData With {.Id = id, .ImageName = imageName, .ImageData = imageData})
                End While
            End Using
        End Using

        Return imagesList
    End Function

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            Dim psSku As String = ""
            Dim psFamilia As String = ""
            Dim psDescripcion As String = ""
            Dim obj As New clsInv_Listados
            Dim dt As New DataTable
            If TxtSku.Text <> "" Then psSku = TxtSku.Text
            If TxtFamilia.Text <> "" Then psFamilia = TxtFamilia.Text
            If TxtDescripcion.Text <> "" Then psDescripcion = TxtDescripcion.Text
            dt = obj.Lista_Sku(Session("Ruta_Emp"), Session("CodEmpresa"), psSku, psFamilia, psDescripcion)
            GvLista.DataSource = dt
            GvLista.DataBind()
            If dt.Rows.Count > 1 Then
                lblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro.Text = "Hay 1 registro."
            Else
                lblRegistro.Text = "No hay registros."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Class ImageData
        Public Property Id As Integer
        Public Property ImageName As String
        Public Property ImageData As Byte()
    End Class

    Private Sub ExportGridViewToExcel()
        ' Crear una carpeta temporal para guardar las imágenes
        Dim tempFolder As String = Server.MapPath("~/TempImages/")
        Try
            If Not Directory.Exists(tempFolder) Then
                Directory.CreateDirectory(tempFolder)
            End If

            Dim wb As New XLWorkbook()
            Dim ws As IXLWorksheet = wb.Worksheets.Add("Productos con Imágenes")

            ' Escribir encabezados
            For i As Integer = 0 To GvLista.HeaderRow.Cells.Count - 1
                ws.Cell(1, i + 1).Value = GvLista.HeaderRow.Cells(i).Text
            Next

            ' Obtener datos y guardar imágenes
            For i As Integer = 0 To GvLista.Rows.Count - 1
                For j As Integer = 0 To GvLista.Rows(i).Cells.Count - 2 ' Excluye la última columna que tiene la imagen
                    ws.Cell(i + 2, j + 1).Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvLista.Rows(i).Cells(j).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                Next

                ' Insertar la imagen
                Try
                    Dim image As Image = CType(GvLista.Rows(i).FindControl("Image1"), Image)
                    Dim relativeUrl As String = image.ImageUrl
                    Dim absoluteUrl As String = New Uri(Request.Url, relativeUrl).ToString()

                    ' Descargar la imagen desde la URL absoluta
                    Dim imageBytes As Byte()
                    Using webClient As New WebClient()
                        imageBytes = webClient.DownloadData(absoluteUrl)
                    End Using

                    ' Convertir la imagen a un formato compatible si es necesario
                    Dim imageFileName As String = Path.Combine(tempFolder, Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) & ".jpg")
                    Using ms As New MemoryStream(imageBytes)
                        Using img As System.Drawing.Image = System.Drawing.Image.FromStream(ms)
                            img.Save(imageFileName, ImageFormat.Jpeg)
                        End Using
                    End Using

                    ' Insertar la imagen en el Excel
                    Using imgStream As New FileStream(imageFileName, FileMode.Open, FileAccess.Read)
                        Dim img As IXLPicture = ws.AddPicture(imgStream).MoveTo(ws.Cell(i + 2, GvLista.Rows(i).Cells.Count))

                        ' Ajustar el tamaño de la celda para la imagen
                        img.Width = 100
                        img.Height = 100

                        ' Ajustar la altura de la fila en función de la altura de la imagen
                        ws.Row(i + 2).Height = img.Height * 0.75 ' Ajustar el factor según sea necesario
                    End Using
                Catch ex As Exception
                    ' Manejar errores de descarga
                    ws.Cell(i + 2, GvLista.Rows(i).Cells.Count).Value = "Error al cargar la imagen"
                    ws.Cell(i + 2, GvLista.Rows(i).Cells.Count).GetComment.AddText(ex.Message)
                End Try
            Next

            ' Ajustar el ancho de las columnas
            ws.Columns().AdjustToContents()

            ' Guardar el archivo en la respuesta HTTP
            Response.Clear()
            Response.Buffer = True
            Response.Charset = ""
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            Response.AddHeader("content-disposition", "attachment;filename=ProductosConImagenes.xlsx")

            Using memoryStream As New MemoryStream()
                wb.SaveAs(memoryStream)
                memoryStream.Seek(0, SeekOrigin.Begin)
                memoryStream.CopyTo(Response.OutputStream)
                Response.Flush()
                Response.End()
            End Using

            ' Limpiar la carpeta temporal
            Directory.Delete(tempFolder, True)
        Catch ex As Threading.ThreadAbortException
            ' Ignorar excepción de subproceso abortado
        Catch ex As Exception
            ' Manejar otras excepciones
            Response.Write("<script>alert('Error al exportar a Excel: " & ex.Message & "');</script>")
        Finally
            ' Limpiar la carpeta temporal
            If Directory.Exists(tempFolder) Then
                Directory.Delete(tempFolder, True)
            End If
        End Try
    End Sub

    Private Sub BtnListarSI_Click(sender As Object, e As EventArgs) Handles BtnListarSI.Click
        Try
            Dim psSku As String = ""
            Dim psFamilia As String = ""
            Dim psDescripcion As String = ""
            Dim obj As New clsInv_Listados
            Dim dt As New DataTable
            If TxtSku.Text <> "" Then psSku = TxtSku.Text
            If TxtFamilia.Text <> "" Then psFamilia = TxtFamilia.Text
            If TxtDescripcion.Text <> "" Then psDescripcion = TxtDescripcion.Text
            dt = obj.Lista_Sku_SinImagen(Session("Ruta_Emp"), Session("CodEmpresa"), psSku, psFamilia, psDescripcion)
            GvLista.DataSource = dt
            GvLista.DataBind()
            If dt.Rows.Count > 1 Then
                lblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro.Text = "Hay 1 registro."
            Else
                lblRegistro.Text = "No hay registros."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
End Class