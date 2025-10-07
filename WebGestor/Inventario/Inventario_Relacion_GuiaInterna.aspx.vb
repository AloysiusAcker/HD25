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
Partial Class Inventario_Inventario_Relacion_GuiaInterna
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objEmp As New ModuloGeneral
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        'LblError.Text = ""
        Dim pCodGuia As Double = 0
        Dim TipoLista As String = ""
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp")
        Dim psRemTipo As String = ""
        Dim pdRemCodigo As Double = 0
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
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
            If TxtNroGuia.Text <> "" Then pCodGuia = Nz(TxtNroGuia.Text)
            gridGuia.DataSource = obj.Lista_GuiaRemision(psConexion, Session("CodEmpresa"), pCodGuia, psFecha, psFechaFin, "2", psRemTipo, pdRemCodigo)
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
                Ocultar_Visible_Imagen(False)
                'Session("TipoRemitente") = "1"
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
    Private Sub gridGuia_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridGuia.RowCommand
        Dim Index As Integer = 0
        Dim cn As String = Session("Ruta_Emp")
        Dim psCodguia As String = ""
        Dim dtListado As New DataTable
        Dim nomImg As String = FileUpload1.FileName.ToString
        txtCodGuia.Text = ""
        If e.CommandName = "Detalle" Then
            Index = Convert.ToInt32(e.CommandArgument)
            psCodguia = gridGuia.Rows(Index).Cells(3).Text.Trim
            LblTituloModal.Text = "Guia Interna Nro. " & psCodguia
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
        If e.CommandName = "Pdf" Then
            Index = Convert.ToInt32(e.CommandArgument)
            psCodguia = gridGuia.Rows(Index).Cells(3).Text.Trim
            Call GenerarPdfGuia(psCodguia)
        End If
        If e.CommandName = "Imagen" Then
            Index = Convert.ToInt32(e.CommandArgument)
            Dim nombreImg As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridGuia.Rows(Index).Cells(11).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            BtnGuargarImg.Text = "Guardar Imagen"
            Ocultar_Visible_Imagen(True)
            txtCodGuia.Text = gridGuia.Rows(Index).Cells(3).Text
            Dim idCodArticulo As Integer = Convert.ToInt32(txtCodGuia.Text)
            Dim imagen As ImgGuia = ImagenesGuia.GetImagenById(idCodArticulo, Session("Ruta_Emp"))
            Session("nomImagen") = imagen.GUIA_IMG_NOMBRE
            Session("Imagen") = imagen.Imagen
            If nombreImg = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', 'data:image/jpg;base64," + Convert.ToBase64String(Session("Imagen")) + "');", True)
            End If
            Session("CodGuia") = gridGuia.Rows(Index).Cells(3).Text
            Session("NombreGuiaImg") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridGuia.Rows(Index).Cells(11).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")

            Index = Convert.ToInt32(e.CommandArgument)
            TxtNombreImagen.Text = ""
            TxtNombreImagen.Visible = True
            Ocultar_Visible_Imagen(True)
            div_imagen.Visible = True
            imagenCarga.Visible = False
            TxtNombreImagen.Visible = True
            lblNombreimg.Visible = True
            psCodguia = gridGuia.Rows(Index).Cells(5).Text.Trim
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
                    'ComprimirImagenEnBaseDeDatos(psCodguia)
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
    Private Sub BtnGuargarImg_Click(sender As Object, e As EventArgs) Handles BtnGuargarImg.Click
        Dim obj As New clsInv_InsUpdDel
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As Double = 0
        codigo = txtCodGuia.Text
        Dim nomImg As String = FileUpload1.FileName.ToString
        Try
            If FileUpload1.HasFile Then
                Using readerI As New BinaryReader(FileUpload1.PostedFile.InputStream)
                    Dim imageI As Byte() = readerI.ReadBytes(FileUpload1.PostedFile.ContentLength)
                    Dim viImagen = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream)
                    Dim vnAncho = viImagen.PhysicalDimension.Width
                    Dim vnAlto = viImagen.PhysicalDimension.Height
                    obj.GuardarImagenGuia(psconexion, codigo, imageI, nomImg)
                    Ocultar_Visible_Imagen(False)
                    Session("nomImagen") = ""
                    Session("Imagen") = ""
                    If (vnAncho < 640 OrElse vnAlto < 480) Then
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar una imagen mayor a 640 x 480');", True)
                    End If
                    BtnCancelar_Click(sender, e)
                End Using
            ElseIf Session("nomImagen").ToString() <> "" Then
                obj.GuardarImagenGuia(psconexion, codigo, Session("Imagen"), Session("nomImagen"))
                Ocultar_Visible_Imagen(False)
                Session("nomImagen") = ""
                Session("Imagen") = ""
                BtnCancelar_Click(sender, e)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una imagen');", True)
            End If
        Catch ex As SqlException
            Dim psMensaje As String = ""
            psMensaje = "Ha ocurrido un error en la base de datos: " & ex.Message
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('" & psMensaje & "');", True)
        Catch ex As Exception
            Dim psMensaje As String = ""
            psMensaje = "Ha ocurrido un error en la aplicación: " & ex.Message
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('" & psMensaje & "');", True)
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
        'Label6.Visible = vf
        'Label7.Visible = vf
    End Sub

    Sub Ayuda(sender As Object, e As FileUpload)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
    End Sub
    Sub VerImg(ByVal image As String)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalImagen').modal('show');", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenVisualizar').setAttribute('src', '" + image + "');", True)
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
        Dim psCodexRemGuia As String = ""
        Dim psCodexDesc As String = ""
        Dim psGuiaSerie As String = ""
        Dim psRetira As String = ""
        Dim psRecibe As String = ""
        Dim psObs As String = ""

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
        Dim fileName As String = "GuiaInterna_Nro_" & psCodGuia & ".pdf" ' "Informe_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".pdf"
        Dim fullPath As String = Path.Combine(savePath, fileName)

        Dim NombrePdfGuia As String = ""
        NombrePdfGuia = "GuiaInterna_Nro_" & psCodGuia

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand

        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " UPDATE TBINV_GUIA_REMISION_0001 SET GUIREM_ARCHIVO = '" & fileName & "' WHERE GUIREM_CODIGO =  " & psCodGuia
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
        cb.SetLineWidth(1)
        ' Crear dos columnas
        Dim leftColumn As New ColumnText(writer.DirectContent)
        Dim rightColumn As New ColumnText(writer.DirectContent)

        ' Establecer coordenadas y dimensiones de las columnas
        leftColumn.SetSimpleColumn(-40, 0, document.PageSize.Width / 2 + 90, document.PageSize.Height - 10)
        rightColumn.SetSimpleColumn(document.PageSize.Width / 2 - 40, 5, document.PageSize.Width + 90, document.PageSize.Height - 10)

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

        Dim tableCabecera As New PdfPTable(3) ' Crear una tabla con 2 columnas
        Dim widths As Single() = {3.0F, 3.0F, 4.0F} '' Establecer el estilo de borde de la tabla
        tableCabecera.SetWidths(widths)
        tableCabecera.AddCell(New Phrase("", fFont))
        tableCabecera.AddCell(New Phrase("", fFont))
        tableCabecera.AddCell(New Phrase("NRO GUIA INTERNA: " & psCodGuia, fFont))
        tableCabecera.AddCell(New Phrase(EmpresaNombre, fFont))
        tableCabecera.AddCell(New Phrase("", fFont))
        tableCabecera.AddCell(New Phrase(DateTime.Now.ToString("dd/MM/yyyyy"), fFont2))
        tableCabecera.AddCell(New Phrase("", fFont))
        tableCabecera.AddCell(New Phrase("", fFont))
        tableCabecera.AddCell(New Phrase(DateTime.Now.ToString("HH:mm:ss"), fFont2))
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
        tableDetalleCab.AddCell(New Phrase("Cant.", fFont1))
        tableDetalleCab.AddCell(New Phrase("Nro. Serie", fFont1))
        tableDetalleCab.AddCell(New Phrase("Nro. Placa", fFont1))
        tableDetalleCab.AddCell(New Phrase("Descripción del equipo", fFont1))

        For Each cell As PdfPCell In tableDetalleCab.Rows.SelectMany(Function(row) row.GetCells()) 'Eliminar los bordes de las celdas individuales
            cell.Border = Rectangle.NO_BORDER
        Next

        'tabla 4 detalle
        Dim table As New PdfPTable(4)
        Dim widths4 As Single() = {1.0F, 3.0F, 3.0F, 7.0F} '' Establecer el estilo de borde de la tabla
        table.SetWidths(widths4)
        dt = Nothing
        dt = obj.Lista_GuiaRemision_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
        If dt.Rows.Count > 0 Then
            For Each dr As Data.DataRow In dt.Rows
                table.AddCell(New Phrase(Nu(dr("Cant")), fFont2))
                table.AddCell(New Phrase(Nu(dr("SERIE_NRO")), fFont2))
                table.AddCell(New Phrase(Nu(dr("PLACA_NRO")), fFont2))
                table.AddCell(New Phrase(Nu(dr("ART_DESCRIPCION")), fFont2))
            Next
        End If
        dt = Nothing
        dt = obj.Lista_GuiaRemision_Detalle_Acc(Session("Ruta_Emp"), Session("CodEmpresa"), psCodGuia)
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
        tableAtBottomColumnTextLeft.AddElement(New Paragraph("                                         Unidad quien entrega el equipo                                                           Unidad quien retira el equipo ", fFont))
        tableAtBottomColumnTextLeft.Go()
        Dim tableAtBottomColumnTextRight As New ColumnText(writer.DirectContent)
        tableAtBottomColumnTextRight.AddElement(New Chunk(separator))
        tableAtBottomColumnTextRight.SetSimpleColumn(document.PageSize.Width / 2 - 20, 0, document.PageSize.Width + 20, 120)
        tableAtBottomColumnTextRight.AddElement(tablaPie)
        tableAtBottomColumnTextRight.AddElement(New Paragraph("  "))
        tableAtBottomColumnTextRight.AddElement(New Paragraph("                                            ------------------------------                                                                  ----------------------------- ", fFont))
        tableAtBottomColumnTextRight.AddElement(New Paragraph("                                        Unidad quien entrega el equipo                                                           Unidad quien retira el equipo ", fFont))

        tableAtBottomColumnTextRight.Go()

        ' Cerrar el documento
        document.Close()


    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Ocultar_Visible_Imagen(False)
        Session("nomImagen") = ""
        Session("Imagen") = ""
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
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
        If (Session("TipoRemitente") = "1" And Session("TipoBusqueda") = "Remitente") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodigo = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaAlmacen(psconexion, Session("CodEmpresa"), psBusCodigo, descripcion)
        ElseIf (Session("TipoRemitente") = "2" And Session("TipoBusqueda") = "Remitente") Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaCentroCosto(psconexion, Session("CodEmpresa"), psBusCodInterno, descripcion)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalUbicacion').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub gridGuia_Sorting(sender As Object, e As GridViewSortEventArgs) Handles gridGuia.Sorting
        Dim dataTable As DataTable ' Supongamos que tienes un DataTable como fuente de datos

        Dim pCodGuia As Double = 0
        Dim TipoLista As String = ""
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp")
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

        If TxtNroGuia.Text <> "" Then pCodGuia = Nz(TxtNroGuia.Text)
        dataTable = obj.Lista_GuiaRemision(psConexion, Session("CodEmpresa"), pCodGuia, psFecha, psFechaFin, "2", psRemTipo, pdRemCodigo)

        ' Realiza la ordenación del DataTable en función de la columna seleccionada
        If ViewState("SortExpression") IsNot Nothing AndAlso ViewState("SortExpression").ToString() = e.SortExpression Then
            ' Si ya se está ordenando por la misma columna, cambia la dirección
            If ViewState("SortDirection") IsNot Nothing AndAlso ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            Else
                ViewState("SortDirection") = "ASC"
            End If
        Else
            ' Si se está ordenando por una columna diferente, establece la dirección en ASC
            ViewState("SortExpression") = e.SortExpression
            ViewState("SortDirection") = "ASC"
        End If

        ' Aplica la dirección de ordenación y columna al DataView
        dataTable.DefaultView.Sort = ViewState("SortExpression") & " " & ViewState("SortDirection")

        '' Realiza la ordenación del DataTable en función de la columna seleccionada
        'dataTable.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression)

        ' Vuelve a enlazar los datos al GridView
        gridGuia.DataSource = dataTable
        gridGuia.DataBind()
    End Sub

    Private Function GetSortDirection(ByVal column As String) As String
        ' Determina la dirección de ordenación actual para la columna
        Dim sortDirection As String = "ASC"

        ' Obtiene la dirección de ordenación de la ViewState
        Dim lastDirection As String = DirectCast(ViewState("SortDirection"), String)

        If lastDirection IsNot Nothing Then
            ' Cambia la dirección de ordenación si la columna actual ya está ordenada
            If lastDirection = "ASC" AndAlso lastDirection = column Then
                sortDirection = "DESC"
            ElseIf lastDirection = "DESC" AndAlso lastDirection = column Then
                sortDirection = "ASC"
            End If

        End If

        ' Guarda la dirección de ordenación en la ViewState
        ViewState("SortDirection") = sortDirection

        Return sortDirection
    End Function
End Class
