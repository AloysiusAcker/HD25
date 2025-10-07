Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net
Imports System.IO
Imports QRCoder
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Drawing
Imports Image = iTextSharp.text.Image
Imports Rectangle = iTextSharp.text.Rectangle
Imports Font = iTextSharp.text.Font
Imports iTextSharp.text.pdf.draw
Imports ImageResizer

Partial Class Inventario_Inventario_Relacion_GuiaRemision
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objEmp As New ModuloGeneral
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        LblError.Text = ""
        Dim pCodGuia As Integer = 0
        Dim TipoLista As String = ""
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
        Dim psRemTipo As String = ""
        Dim pdRemCodigo As Double = 0
        psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        If TxtFechaFin.Text = "" Then
            psFechaFin = psFecha
        Else
            psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
        End If
        If DdlRemitente.SelectedValue <> "< Seleccionar >" Then
            psRemTipo = DdlRemitente.SelectedValue
        End If
        If lblCodRemitente.Text <> "" Then
            pdRemCodigo = lblCodRemitente.Text
        End If
        Try
            gridGuia.DataSource = obj.Lista_GuiaRemision(psConexion, Session("CodEmpresa"), 0, psFecha, psFechaFin, "1", psRemTipo, pdRemCodigo)
            gridGuia.DataBind()
            LblRegistro.Text = "Se encontrarón " & gridGuia.Rows.Count & " registros."

        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                'Call Cargar_Correos()
                'Session("TipoRemitente") = "1"
                Session("UnaVez") = "1"
                Ocultar_Visible_Imagen(False)
                div_imagen.Visible = False
                TxtNombreImagen.Text = ""
                TxtNombreImagen.Visible = False
                lblNombreimg.Visible = False
            Catch Ex As SqlException
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                LblError.Visible = True
                LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
    Protected Sub BtnCargaArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargaArchivo.Click
        Dim sourceFolderPath As String = "C:\GuiaRemision"
        Dim destinationFolderPath As String = Server.MapPath("GuiaRemision")

        Try

            'If Not Directory.Exists(sourceFolderPath) OrElse Not Directory.Exists(destinationFolderPath) Then
            '    LblError.Text = "Las carpetas de origen y destino deben existir."
            '    Return
            'End If

            Dim psGuiaSerie As String = ""
            Dim psGuiaNro As String = ""
            Dim psGuion As Double = 0
            Dim psDoc As String = ""
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand

            Cn.Open() : CmdGlobal.Connection = Cn
            'Dim Files As String() = Directory.GetFiles(sourceFolderPath)


            Dim folderInfo As New DirectoryInfo(sourceFolderPath)

            If folderInfo.Exists Then
                If Session("UnaVez") = "1" Then
                    Dim files As FileInfo() = folderInfo.GetFiles()
                    For Each fileArc As FileInfo In files
                        ' Mueve el archivo a la carpeta de destino
                        Dim destinationFilePath As String = Path.Combine(destinationFolderPath, fileArc.Name)

                        psGuiaSerie = fileArc.Name.Substring(0, 4)
                        psGuion = InStr(fileArc.Name, "-") - 1
                        psGuiaNro = fileArc.Name.Substring(4, psGuion - 4)
                        psGuiaNro = Llenar_Ceros(psGuiaNro, 8)
                        psDoc = fileArc.Name
                        CmdGlobal.CommandText = " UPDATE TBINV_GUIA_REMISION_0001 SET GUIREM_ARCHIVO = '" & psDoc & "' " _
                                          & " WHERE GUIREM_SERIE = '" & psGuiaSerie & "' AND GUIREM_NUMERO = '" & psGuiaNro & "'"
                        CmdGlobal.ExecuteNonQuery()
                        ' Verifica si el archivo existe en la carpeta de origen antes de moverlo

                        If Not File.Exists(destinationFilePath) Then
                            fileArc.CopyTo(destinationFilePath)
                        End If
                    Next
                End If
                LblError.Text = ""
                If Session("UnaVez") = "2" Then
                    Session("UnaVez") = "1"
                ElseIf Session("UnaVez") = "1" Then
                    Session("UnaVez") = "2"
                End If
                'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Archivos movidos exitosamente.')", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('La carpeta de origen no existe.')", True)
                LblError.Text = ""
            End If

            Cn.Close()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Archivos movidos exitosamente.')", True)
        Catch ex As Exception
            LblError.Text = "Error al mover archivos: " & ex.Message
        End Try
    End Sub
    Private Sub gridGuia_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridGuia.RowCommand
        Dim Index As Integer = 0
        Dim cn As String = Session("Ruta_Emp")
        Dim psCodguia As String = ""
        Dim dtListado As New DataTable

        If e.CommandName = "CrearXml" Then
            Index = Convert.ToInt32(e.CommandArgument)
            psCodguia = gridGuia.Rows(Index).Cells(5).Text.Trim
            Call EjecutarCrearXmlPHP(psCodguia)
            BtnListar_Click(sender, e)
        End If
        If e.CommandName = "Detalle" Then
            Index = Convert.ToInt32(e.CommandArgument)
            psCodguia = gridGuia.Rows(Index).Cells(5).Text.Trim
            LblTituloModal.Text = "Guia Remisión Nro. " & psCodguia
            Try
                gridDetalle.DataSource = obj.Lista_GuiaRemision_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), psCodguia)
                gridDetalle.DataBind()

                gridDetalleAcc.DataSource = obj.Lista_GuiaRemision_Detalle_Acc(Session("Ruta_Emp"), Session("CodEmpresa"), psCodguia)
                gridDetalleAcc.DataBind()

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
            Catch ex As SqlException
                LblError.Text = ex.Message
            Catch ex As Exception
                LblError.Text = ex.Message
            Finally
            End Try
        End If
        If e.CommandName = "QR" Then
            Index = Convert.ToInt32(e.CommandArgument)
            psCodguia = gridGuia.Rows(Index).Cells(5).Text.Trim
            Call GenerarPdfGuia(psCodguia)
        End If
        If e.CommandName = "Imagen" Then
            Index = Convert.ToInt32(e.CommandArgument)
            TxtNombreImagen.Text = ""
            TxtNombreImagen.Visible = True
            Ocultar_Visible_Imagen(True)
            div_imagen.Visible = True
            imagenCarga.Visible = False
            TxtNombreImagen.Visible = True
            lblNombreimg.Visible = True
            psCodguia = gridGuia.Rows(Index).Cells(5).Text.Trim
            Dim nombreImg As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridGuia.Rows(Index).Cells(19).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            BtnGuargarImg.Text = "Guardar Imagen"
            Ocultar_Visible_Imagen(True)
            lblCodigoGuia.Text = psCodguia
            txtCodGuia.Text = gridGuia.Rows(Index).Cells(6).Text.Trim & "-" & gridGuia.Rows(Index).Cells(7).Text.Trim
            lblNombreimg.Text = "Nombre de la imagen"

            If psCodguia > 0 Then
                Dim dt As New DataTable
                Dim connectionString As String = Session("Ruta_Emp")
                TxtNombreImagen.Text = Replace(gridGuia.Rows(Index).Cells(19).Text.Trim, "&nbsp;", "")
                If TxtNombreImagen.Text <> "" Then
                    ComprimirImagenEnBaseDeDatos(psCodguia)
                End If

                Dim query As String = "SELECT GUIREM_CODIGO, GUIA_IMG_NOMBRE, GUIA_IMG AS Imagen FROM TBINV_GUIA_REMISION_0001 WHERE GUIREM_CODIGO = @GUIREM_CODIGO"
                Using connection As New SqlConnection(connectionString)
                    Using cmd As New SqlCommand(query, connection)
                        cmd.Parameters.Add("@GUIREM_CODIGO", SqlDbType.Float).Value = psCodguia ' Ajusta el valor del ID según el registro que desees mostrar
                        connection.Open()

                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                If Not IsDBNull(reader("Imagen")) Then
                                    TxtNombreImagen.Text = Nu(reader("GUIA_IMG_NOMBRE").ToString)
                                    Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                    Dim base64String As String = Convert.ToBase64String(imageData)
                                    imagenCarga.ImageUrl = "data:image/jpeg;base64," + base64String
                                    imagenCarga.Visible = True
                                    Session("NuevaImagen") = "No"
                                Else
                                    TxtNombreImagen.Text = Nu(reader("GUIA_IMG_NOMBRE").ToString)
                                    Dim nombreImagen As String = Nu(reader("GUIA_IMG_NOMBRE").ToString)
                                    Dim rutaImagen As String = Server.MapPath("~/Inventario/GuardarImagen/" + nombreImagen)
                                End If
                            End If
                        End Using
                    End Using
                End Using
            End If


        End If

    End Sub

    Protected Sub ComprimirImagenEnBaseDeDatos(ByVal pdCodArt As Double)
        ' Cadena de conexión a la base de datos
        Dim connectionString As String = Session("Ruta_Emp")

        ' Establece la consulta para recuperar la imagen
        Dim query As String = "SELECT GUIA_IMG FROM TBINV_GUIA_REMISION_0001 WHERE  GUIREM_CODIGO =  " & pdCodArt

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Using command As New SqlCommand(query, connection)

                ' Lee la imagen de la base de datos
                Dim bytesImagenOriginal As Byte() = DirectCast(command.ExecuteScalar(), Byte())

                ' Guarda los bytes en un archivo temporal
                Dim rutaTemporal As String = Path.GetTempFileName()
                File.WriteAllBytes(rutaTemporal, bytesImagenOriginal)

                ' Comprime la imagen utilizando ImageResizer
                Dim settings As New ResizeSettings("maxwidth=600&maxheight=600&format=jpg")
                ImageBuilder.Current.Build(rutaTemporal, rutaTemporal, settings)

                ' Lee los bytes de la imagen comprimida
                Dim bytesImagenComprimida As Byte() = File.ReadAllBytes(rutaTemporal)

                ' Actualiza los bytes de la imagen comprimida en la base de datos
                Dim updateQuery As String = "UPDATE TBINV_GUIA_REMISION_0001 SET GUIA_IMG = @Imagen WHERE GUIREM_CODIGO = " & pdCodArt

                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@Imagen", bytesImagenComprimida)
                    updateCommand.ExecuteNonQuery()
                End Using


                ' Elimina el archivo temporal
                File.Delete(rutaTemporal)
            End Using
        End Using
    End Sub
    Private Sub MostrarImagen(ByVal datosImagen As Byte())
        If datosImagen IsNot Nothing AndAlso datosImagen.Length > 0 Then
            ' Crear una imagen desde los datos binarios
            Using ms As New MemoryStream(datosImagen)
                Dim imagen As System.Drawing.Image = System.Drawing.Image.FromStream(ms)

                ' Configurar el control Image
                imagenCarga.ImageUrl = "data:image/jpeg;base64," & Convert.ToBase64String(datosImagen)
                imagenCarga.Width = imagen.Width
                imagenCarga.Height = imagen.Height
                imagenCarga.Visible = True
            End Using
        End If
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Ocultar_Visible_Imagen(False)
        Session("nomImagen") = ""
        Session("Imagen") = ""
        div_imagen.Visible = False
        TxtNombreImagen.Visible = False
        lblNombreimg.Visible = False
        TxtNombreImagen.Text = ""
    End Sub
    Protected Sub ComprimirImagen(rutaOriginal As String, rutaComprimida As String)
        Dim settings As New ResizeSettings("maxwidth=800&maxheight=600&format=jpg")
        ImageBuilder.Current.Build(rutaOriginal, rutaComprimida, settings)
    End Sub
    Private Sub BtnGuargarImg_Click(sender As Object, e As EventArgs) Handles BtnGuargarImg.Click
        Try
            Dim obj As New clsInv_InsUpdDel

            If FileUpload1.HasFile Then

                Dim rutaOriginal As String = Server.MapPath("~/Inventario/ArchivoTemp/original.jpg")
                Dim rutaComprimida As String = Server.MapPath("~/Inventario/ArchivoTemp/comprimida.jpg")
                FileUpload1.SaveAs(rutaOriginal)
                ComprimirImagen(rutaOriginal, rutaComprimida)
                Dim bytesImagen As Byte() = File.ReadAllBytes(rutaComprimida)

                Dim filename As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim cmdSql As New SqlCommand
                'Dim Rs As SqlDataReader
                Dim pdCodImg As Double = 0
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = " update TBINV_GUIA_REMISION_0001 set GUIA_IMG_NOMBRE = '" & filename & "' where GUIREM_CODIGO =  " & Nz(lblCodigoGuia.Text)
                cmdSql.ExecuteNonQuery()

                Dim psCodart As Double = 0
                If Nz(lblCodigoGuia.Text) > 0 Then
                    psCodart = lblCodigoGuia.Text
                End If

                Dim inputStream As System.IO.Stream = FileUpload1.PostedFile.InputStream
                Dim tamaño As Integer = FileUpload1.PostedFile.ContentLength
                Dim imagenData(tamaño - 1) As Byte
                inputStream.Read(imagenData, 0, tamaño)
                obj.GuardarImagenGuia(Session("Ruta_Emp"), psCodart, bytesImagen, filename)

            End If

            Using connection As New SqlConnection(Session("Ruta_Emp"))
                Using cmd As New SqlCommand("SELECT GUIREM_CODIGO, GUIA_IMG_NOMBRE, GUIA_IMG AS Imagen FROM TBINV_GUIA_REMISION_0001 WHERE  GUIREM_CODIGO = @GUIREM_CODIGO", connection)
                    cmd.Parameters.Add("@GUIREM_CODIGO", SqlDbType.Int).Value = Nz(lblCodigoGuia.Text) ' Ajusta el valor del ID según el registro que desees mostrar
                    connection.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("Imagen")) Then
                                TxtNombreImagen.Text = Nu(reader("GUIA_IMG_NOMBRE").ToString)
                                Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imagenCarga.ImageUrl = "data:image/jpeg;base64," + base64String
                                imagenCarga.Visible = True
                                div_imagen.Visible = True
                            Else
                                imagenCarga.Visible = False
                                div_imagen.Visible = False
                            End If
                        End If
                    End Using
                End Using
            End Using
            BtnCancelar_Click(sender, e)
            BtnListar_Click(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Sub Ocultar_Visible_Imagen(ByVal vf As Boolean)
        txtCodGuia.Text = ""
        lblCodigo.Visible = vf
        lblImagen.Visible = vf
        txtCodGuia.Visible = vf
        FileUpload1.Visible = vf
        FileNombre.Visible = vf
        BtnGuargarImg.Visible = vf
        BtnCancelar.Visible = vf
    End Sub

    Sub Ayuda(sender As Object, e As FileUpload)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
    End Sub
    Sub VerImg(ByVal image As String)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalImagen').modal('show');", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenVisualizar').setAttribute('src', '" + image + "');", True)
    End Sub


    Private Sub BtnEnviarTodo_Click(sender As Object, e As EventArgs) Handles BtnEnviarTodo.Click

        Dim Check As CheckBox
        Dim psCodguia As String = ""
        Dim i As Integer
        Dim dt As New Data.DataTable
        Try
            For i = 0 To gridGuia.Rows.Count - 1
                psCodguia = gridGuia.Rows(i).Cells(5).Text
                dt = obj.Lista_GuiaRemision_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), psCodguia)
                If dt.Rows.Count > 0 Then
                    For Each dr As Data.DataRow In dt.Rows
                        If Nu(dr("GUIREM_QR")) = "" Then
                            Check = gridGuia.Rows(i).Cells(0).FindControl("chkPag")
                            If Check.Checked = True And Check.Enabled = True Then
                                Call EjecutarCrearXmlPHP(psCodguia)
                            End If
                        End If
                    Next
                End If
            Next
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Genera_QR(ByVal ps_TextoQR As String)
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(ps_TextoQR, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)
        Dim qrCodeImage As Bitmap = qrCode.GetGraphic(1)

        Dim ms As New MemoryStream()
        qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png)
        imgQRCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray())
    End Sub
    Private Sub GenerarPdfGuia(ByVal psCodGuia As String)
        ' Datos de la guía de remisión
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
        Dim psGuiaSerie As String = ""

        dt = obj.Lista_GuiaRemision_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
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

        'genero qr
        Dim qrGenerator As New QRCodeGenerator()
        Dim qrCodeData As QRCodeData = qrGenerator.CreateQrCode(pstextoQR, QRCodeGenerator.ECCLevel.Q)
        Dim qrCode As New QRCode(qrCodeData)
        Dim qrCodeImage As Bitmap = qrCode.GetGraphic(10)

        '
        Dim NombrePdfGuia As String = ""
        NombrePdfGuia = EmpresaRuc & "-09-" & psGuiaSerie

        ' Crear el documento PDF
        Dim document As New Document(PageSize.A4, 10, 10, 10, 10)
        Dim output As New MemoryStream() ' Crear el escritor PDF
        'Dim writer As PdfWriter = PdfWriter.GetInstance(document, output)

        Dim archivo As String = Server.MapPath("~\Inventario\GRE_PDF\" & NombrePdfGuia & ".pdf") ' Ruta y nombre del archivo PDF
        Dim psRuta As String = ""
        'psRuta = "\\" & NomServer & "\GRE_PDF\" & NombrePdfGuia & ".pdf"

        Dim carpeta As String = Server.MapPath("~/Inventario/GRE_PDF/")

        ' Verificar si la carpeta existe
        If Not Directory.Exists(carpeta) Then
            ' Crear la carpeta
            Directory.CreateDirectory(carpeta)
        End If

        Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(archivo, FileMode.Create))
        ' Abrir el documento
        document.Open()

        Dim bf As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 7, BaseColor.BLACK)
        Dim fFont = New iTextSharp.text.Font(bf)
        Dim bf1 As iTextSharp.text.Font = FontFactory.GetFont(FontFactory.HELVETICA, 7, iTextSharp.text.Font.BOLD, BaseColor.BLACK)
        Dim fFont1 = New iTextSharp.text.Font(bf1)

        'crea linea
        Dim separator As New LineSeparator() ' Crear una instancia de LineSeparator
        separator.LineColor = New BaseColor(128, 128, 128) ' Negro' Configurar el color y grosor de la línea
        separator.LineWidth = 0.5 ' Grosor de 1 punto

        document.Add(New Paragraph("                                                                                                            ", fFont))
        document.Add(New Paragraph("                                                                                                                                                                                                                                        RUC N° " & EmpresaRuc, fFont1))
        document.Add(New Paragraph("                                                                                                                                                                                                                            GUIA DE REMISION ELECTRONICA", fFont1))

        Dim tableRemitente As New PdfPTable(2) ' Crear una tabla con 2 columnas
        Dim widths As Single() = {8.0F, 1.0F} '' Establecer el estilo de borde de la tabla
        tableRemitente.SetWidths(widths)
        tableRemitente.AddCell(New Phrase("      " & EmpresaNombre, New Font(Font.FontFamily.HELVETICA, 7)))
        tableRemitente.AddCell(New Phrase(psRemitente, New Font(Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.BOLD)))

        For Each cell As PdfPCell In tableRemitente.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next
        document.Add(tableRemitente) ' Agregar la tabla al documento

        document.Add(New Paragraph("                                                                                                                                                                                                                                           " & numeroGuia, fFont))
        document.Add(New Paragraph("                                                                                                                                                                                                                                        ", fFont))
        document.Add(New Paragraph("                                                                                                                                                                                                                                        ", fFont))
        document.Add(New Paragraph("                                                                                                                                                                                                                                        ", fFont))
        document.Add(New Paragraph("   DATOS DE INICIO DEL TRASLADO", fFont1))

        Dim tableDatosTraslado As New PdfPTable(4) ' Crear una tabla con 2 columnas
        tableDatosTraslado.SetTotalWidth(New Single() {150, 150, 150, 300}) ' Establecer el ancho de las columnas
        tableDatosTraslado.AddCell(New Phrase("    Fecha de Emisión:   ", fFont))
        tableDatosTraslado.AddCell(New Phrase(fechaEmision, fFont))
        tableDatosTraslado.AddCell(New Phrase("Motivo de Traslado: ", fFont))
        tableDatosTraslado.AddCell(New Phrase(motivo, fFont))
        tableDatosTraslado.AddCell(New Phrase("    Fecha Inicio Traslado:   ", fFont))
        tableDatosTraslado.AddCell(New Phrase(fechaTraslado, fFont))
        tableDatosTraslado.AddCell(New Phrase("Modalidad de Transporte: ", fFont))
        tableDatosTraslado.AddCell(New Phrase(psModalidadTransporte, fFont))
        tableDatosTraslado.AddCell(New Phrase("    Fecha Entrega:   ", fFont))
        tableDatosTraslado.AddCell(New Phrase(fechaEmision, fFont))
        tableDatosTraslado.AddCell(New Phrase("Peso Bruto: ", fFont))
        tableDatosTraslado.AddCell(New Phrase(psPesoBruto & " " & psUnidadMedida, fFont))

        tableDatosTraslado.HorizontalAlignment = Element.ALIGN_LEFT

        For Each cell As PdfPCell In tableDatosTraslado.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next

        document.Add(tableDatosTraslado) ' Agregar la tabla al documento

        document.Add(New Chunk(separator)) ' Agregar la línea al documento
        document.Add(New Paragraph("   DATOS DEL DESTINATARIO", fFont1))
        Dim tableDatosDestinatario As New PdfPTable(4) ' Crear una tabla con 2 columnas
        tableDatosDestinatario.SetTotalWidth(New Single() {150, 150, 150, 300}) ' Establecer el ancho de las columnas
        tableDatosDestinatario.AddCell(New Phrase("    RUC: ", fFont))
        tableDatosDestinatario.AddCell(New Phrase(psRucDestinatario, fFont))
        tableDatosDestinatario.AddCell(New Phrase("Razón social :  ", fFont))
        tableDatosDestinatario.AddCell(New Phrase(psDestinatario, fFont))
        tableDatosDestinatario.HorizontalAlignment = Element.ALIGN_LEFT

        For Each cell As PdfPCell In tableDatosDestinatario.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next

        document.Add(tableDatosDestinatario) ' Agregar la tabla al documento

        document.Add(New Chunk(separator)) ' Agregar la línea al documento
        document.Add(New Paragraph("   DATOS DEL PUNTO DE PARTIDA Y PUNTO DE LLEGADA", fFont1))
        document.Add(New Paragraph("    Dirección del punto de partida: " & psUbigeoPartida & " - " & psPtoPartida, fFont))
        document.Add(New Paragraph("    Dirección del punto de llegada: " & psUbigeoLlegada & " - " & psPtoLlegada, fFont))

        document.Add(New Chunk(separator)) ' Agregar la línea al documento
        document.Add(New Paragraph("   DATOS DEL TRANSPORTISTA", fFont1))
        Dim tableDatosTransportista As New PdfPTable(4) ' Crear una tabla con 2 columnas
        tableDatosTransportista.SetTotalWidth(New Single() {150, 150, 150, 300}) ' Establecer el ancho de las columnas
        tableDatosTransportista.AddCell(New Phrase("    RUC: ", fFont))
        tableDatosTransportista.AddCell(New Phrase(psTransportistaRUC, fFont))
        tableDatosTransportista.AddCell(New Phrase("Razón social :  ", fFont))
        tableDatosTransportista.AddCell(New Phrase(psTransportista, fFont))
        tableDatosTransportista.HorizontalAlignment = Element.ALIGN_LEFT

        For Each cell As PdfPCell In tableDatosTransportista.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next

        document.Add(tableDatosTransportista) ' Agregar la tabla al documento

        document.Add(New Paragraph("                                                                                                            ", fFont))
        document.Add(New Paragraph("                                                                                                            ", fFont))
        document.Add(New Paragraph("   INFORMACION DE BIENES TRASLADADOS", fFont1))

        ' Dibujar el recuadro en la esquina superior derecha
        Dim cb As PdfContentByte = writer.DirectContent
        cb.SetLineWidth(0.5) ' Establecer el grosor de línea
        cb.SetColorFill(New BaseColor(255, 200, 200))
        cb.Rectangle(document.PageSize.Width - 170, document.PageSize.Height - 70, 150, 50) ' Especificar las coordenadas y el tamaño del recuadro
        cb.Stroke() ' Dibujar el recuadro

        ' Agregar el código QR al documento
        Dim qrImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Png)
        qrImage.ScalePercent(9)
        'qrImage.SetAbsolutePosition(document.LeftMargin, document.BottomMargin)
        qrImage.SetAbsolutePosition(document.LeftMargin, document.PageSize.Height - document.TopMargin - qrImage.ScaledHeight)
        document.Add(qrImage)

        ' Crear una tabla con 3 columnas
        Dim table As New PdfPTable(4)

        ' Establecer el ancho de las columnas de la tabla
        Dim widths3 As Single() = {1.0F, 4.5F, 1.5F, 1.5F}
        table.SetWidths(widths3)

        ' Establecer el tamaño de letra y alineación de la tabla
        table.DefaultCell.Phrase = New Phrase() ' Reiniciar el estilo de celda por defecto
        table.DefaultCell.Phrase.Font = FontFactory.GetFont(FontFactory.HELVETICA, 5) ' Tamaño de letra

        table.AddCell(GetStyledCell("Item", Font.BOLD, 9))
        table.AddCell(GetStyledCell("Descripcion", Font.BOLD, 9))
        table.AddCell(GetStyledCell("U. M.", Font.BOLD, 9))

        table.AddCell(GetStyledCell("Cantidad", Font.BOLD, 9))
        ' Obtener los datos de la base de datos
        Dim datos As List(Of Datos) = ObtenerDatosDesdeSQL(psCodGuia) ' Obtener los datos de la base de datos SQL

        ' Agregar los datos a la tabla
        For Each dato As Datos In datos
            table.AddCell(GetStyledCell(dato.Item, Font.NORMAL, 7))
            table.AddCell(GetStyledCell(dato.Descripcion, Font.NORMAL, 7))
            table.AddCell(GetStyledCell(dato.UM, Font.NORMAL, 7))
            table.AddCell(GetStyledCell(dato.Cantidad, Font.NORMAL, 7))
        Next

        ' Agregar la tabla al documento
        table.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin
        table.WriteSelectedRows(0, -1, document.LeftMargin, 550, writer.DirectContent)

        ' Cerrar el documento
        document.Close()


    End Sub

    Private Function GetStyledCell(ByVal text As String, ByVal style As Integer, ByVal fontSize As Single) As PdfPCell
        Dim cell As New PdfPCell(New Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, fontSize, style, BaseColor.BLACK)))
        cell.Padding = 5
        Return cell
    End Function

    Private Function ObtenerDatosDesdeSQL(ByVal psCodGuia As Double) As List(Of Datos)
        Dim connectionString As String = Session("Ruta_Emp")
        Dim query As String = " Select A.ART_CODEQUIVA, '' AS Item, " _
                            & " A.ART_DESCRIPCION,  " _
                            & "  Elemento_codigo as UnidadMedida, SUM( g.GUREDE_CANTIDAD) AS CANTIDAD," _
                            & " RIGHT('00000000' + CONVERT(VARCHAR(10), G.ARTICULO_CODIGO), 8) AS Cod_Articulo,G.ARTICULO_CODIGO " _
                            & " FROM TBINV_GUIA_REMISION_DETALLE_0001 G " _
                            & " INNER JOIN TBINV_ARTICULOS A ON G.ARTICULO_CODIGO = A.ART_CODIGO " _
                            & " INNER JOIN TBINV_TABLAS_INFO AS I ON ELEMENTO_CODUNICO = a.ART_UNIDAD_MEDIDA " _
                            & " AND I.EMPRESA_CODIGO=A.EMPRESA_CODIGO " _
                            & " WHERE A.EMPRESA_CODIGO = " & Session("CodEmpresa") & " And G.GUIREM_CODIGO = " & psCodGuia _
                            & " GROUP BY A.ART_CODEQUIVA,G.ARTICULO_CODIGO,A.ART_DESCRIPCION,Elemento_codigo "

        Dim datos As New List(Of Datos)()
        Dim i As Integer = 0
        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Using command As New SqlCommand(query, connection)
                Using reader As SqlDataReader = command.ExecuteReader()
                    While reader.Read()
                        i = i + 1
                        Dim Item As String = Format(i, "000")
                        Dim Descripcion As String = reader("ART_DESCRIPCION").ToString()
                        Dim UM As String = reader("UnidadMedida").ToString()
                        Dim Cantidad As String = reader("CANTIDAD").ToString()

                        datos.Add(New Datos(Item, Descripcion, UM, Cantidad))
                    End While
                End Using
            End Using

            connection.Close()
        End Using

        Return datos
    End Function

    Public Class Datos
        Public Property Item As String
        Public Property Descripcion As String
        Public Property UM As String
        Public Property Cantidad As String

        Public Sub New(ByVal Item As String, ByVal Descripcion As String, ByVal UM As String, ByVal Cantidad As String)
            Me.Item = Item
            Me.Descripcion = Descripcion
            Me.UM = UM
            Me.Cantidad = Cantidad
        End Sub
    End Class
    Private Sub EjecutarCrearXmlPHP(ByVal psCodGuia As String)
        Dim variable As String = psCodGuia ' Valor de la variable a pasar al proceso PHP
        Dim phpScriptURL As String = "http://localhost:82/php_gre/ProcesoCrearXML.php" ' URL del archivo PHP en el servidor

        ' Crear una solicitud HTTP para ejecutar el proceso PHP
        Dim request As WebRequest = WebRequest.Create(phpScriptURL & "?variable=" & Uri.EscapeDataString(variable))
        request.Method = "GET"

        ' Obtener la respuesta del servidor PHP
        Dim response As WebResponse = request.GetResponse()
        Dim dataStream As Stream = response.GetResponseStream()
        Dim reader As New StreamReader(dataStream)
        Dim responseText As String = reader.ReadToEnd()

        ' Cerrar los objetos de lectura
        reader.Close()
        dataStream.Close()
        response.Close()
        Console.WriteLine(responseText)
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
        Session("TipoRemitente") = ""
        If DdlRemitente.SelectedValue = "1" Then
            Session("TipoRemitente") = "1"
        ElseIf DdlRemitente.SelectedValue = "2" Then
            Session("TipoRemitente") = "2"
        End If
    End Sub

    Private Sub btnBusCancelar_Click(sender As Object, e As EventArgs) Handles BtnBusCancelar.Click
        Call Limpiar_Popup()
    End Sub
    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        Session("TipoRemitente") = ""
        Session("TipoBusqueda") = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('hide');", True)
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And Session("TipoBusqueda") = "Remitente" Then
            TxtRemCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtRemDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodRemitente.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Popup()
        End If
    End Sub
    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodigo As Double = 0
        Dim objCont As New clsCont_Listados
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
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub
End Class
