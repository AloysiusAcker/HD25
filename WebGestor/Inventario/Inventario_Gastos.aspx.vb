Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ImageResizer
Imports System.Math

Partial Class Inventario_Inventario_Gastos
    Inherits System.Web.UI.Page
    Dim FunCont As New clsCont_Funciones
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Call LlenaComboItem("TBOPC553", DdlTipo)
            Call LlenaComboItem("TBOPC554", DdlTipoMov)
            Call LlenaComboItem("TBOPC553", DdlBusTipo)
            Call LlenaComboItem("TBOPC554", DdlBusTipoMov)
            Call LlenaComboItem("TBOPC015", DdlMoneda)
            Call LlenaComboItem("TBOPC350", DdlDoc)
            DdlMoneda.SelectedValue = "2"
            TxtFechaReg.Text = FormatoFecha(FechaActual())
            TxtFechaGasto.Text = FormatoFecha(FechaActual())
            Llenar_Usuarios(DdlUsuario)
            Llenar_Usuarios(DdlBusUsuario)
            DdlUsuario.SelectedValue = Session("User")
            DdlBusUsuario.SelectedValue = "< Seleccionar >"
            TxtImporte.Text = "0.00"
        End If

    End Sub
    Private Sub Llenar_Usuarios(ByVal Ddl As DropDownList)
        Dim objSeg As New ModuloSeguridad
        Dim dt As New DataTable
        dt = objSeg.Listar_Usuarios
        Ddl.DataSource = dt
        Ddl.DataValueField = "CODIGO"
        Ddl.DataTextField = "nombre"
        Ddl.DataBind()
        Ddl.Items.Add("< Seleccionar >")
        Ddl.SelectedValue = "< Seleccionar >"

    End Sub

    'Private Sub ChkCCostos_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCCostos.CheckedChanged
    '    If ChkCCostos.Checked = True Then
    '        TxtRuc.Enabled = True
    '        TxtRazonSocial.Enabled = True
    '        BtnBusca.Enabled = True
    '        TxtRuc.Text = ""
    '        TxtRazonSocial.Text = ""
    '        TxtCodPersona.Text = ""
    '    Else
    '        TxtRuc.Enabled = False
    '        TxtRazonSocial.Enabled = False
    '        BtnBusca.Enabled = False
    '        TxtRuc.Text = ""
    '        TxtRazonSocial.Text = ""
    '        TxtCodPersona.Text = ""
    '    End If
    'End Sub

    Protected Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click

        Dim objU As New Cls_Inventario_Ubicacion
        Dim dt As New DataTable
        Dim CodInterno As String = ""
        Dim descripcion As String = ""

        Try
            CodInterno = BuscarCodigo.Value.ToString
            descripcion = BuscarDescripcion.Value.ToString
            dt = objU.Lista_CentroC_Inventario(Session("Ruta_Emp"), CodInterno, descripcion)
            GvBusqueda.DataSource = dt
            GvBusqueda.DataBind()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try

    End Sub
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

            Limpiar_Cajas_Popup()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtRuc.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtRazonSocial.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtCodPersona.Text = GvBusqueda.Rows(Index).Cells(3).Text
            'TxtCodigoAyuda.Text = GvBusqueda.Rows(Index).Cells(4).Text
            Session("CodSeccion") = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()


    End Sub
    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub

    Private Sub DdlDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlDoc.SelectedIndexChanged
        If DdlDoc.SelectedValue = "1" Then
            TxtDocSerie.Enabled = False : TxtDocSerie.Text = ""
            TxtDocNumero.Enabled = True : TxtDocNumero.Text = ""
        ElseIf DdlDoc.SelectedValue = "2" Or DdlDoc.SelectedValue = "3" Then
            TxtDocSerie.Enabled = True : TxtDocSerie.Text = ""
            TxtDocNumero.Enabled = True : TxtDocNumero.Text = ""
        Else
            TxtDocSerie.Enabled = False : TxtDocSerie.Text = ""
            TxtDocNumero.Enabled = False : TxtDocNumero.Text = ""
        End If
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        'Inventario_Gastos_xPersonal
        Dim objU As New Cls_Inventario_Ubicacion
        Dim dt As New DataTable
        Dim psFecha As String = ""
        Dim psHora As String = ""
        Dim psUser As String = ""
        Dim psGastoTipo As String = ""
        Dim psGastoTipoMov As String = ""
        Dim pdCCosto As Double = 0
        Dim psGastoDocTipo As String = ""
        Dim psGastoDocSerie As String = ""
        Dim pdGastoDocNumero As Double = 0
        Dim psGastoMoneda As String = ""
        Dim pdGastoImporte As Double = 0
        Dim psGastoGlosa As String = ""
        Dim psValorSys As String = ""
        Dim psGasto_Fecha As String = ""
        Dim pdRegistro As Double = 0

        Try
            If pdRegistro = 0 Then
                BtnGuardar2_Click(sender, e)
                If String.IsNullOrEmpty(TxtCodPersona.Text) Then

                ElseIf DdlUsuario.SelectedValue = "< Seleccionar >" Then
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Usuario.'); $('#ModalGasto').modal('show');", True)
                ElseIf DdlTipo.SelectedValue = "< Seleccionar >" Then
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar tipo de gasto.'); $('#ModalGasto').modal('show');", True)
                ElseIf DdlDoc.SelectedValue = "< Seleccionar >" Then
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar tipo de documento.'); $('#ModalGasto').modal('show');", True)
                ElseIf TxtDocSerie.Enabled = True And TxtDocSerie.Text = "" Then
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Serie de documento.'); $('#ModalGasto').modal('show');", True)
                ElseIf TxtDocNumero.Enabled = True And TxtDocNumero.Text = "" Then
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Número de documento.'); $('#ModalGasto').modal('show');", True)
                ElseIf Nz(TxtImporte.Text) = 0 Then
                    'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Importe.'); $('#ModalGasto').modal('show');", True)

                Else

                    psValorSys = Session("User") & FechaActual() & HoraActual()
                    'If ChkCCostos.Checked = True Then
                    pdCCosto = Nz(TxtCodPersona.Text)
                    'End If
                    If TxtDocNumero.Enabled = True Then
                        pdGastoDocNumero = Nz(TxtDocNumero.Text)
                    End If
                    If Nz(TxtImporte.Text) > 0 Then
                        pdGastoImporte = Nz(TxtImporte.Text)
                    End If
                    If TxtDocSerie.Enabled = True Then
                        psGastoDocSerie = TxtDocSerie.Text
                    End If
                    psHora = HoraActual()
                    psGastoGlosa = TxtGlosa.Text
                    If DdlTipo.SelectedValue <> "< Seleccionar >" Then psGastoTipo = DdlTipo.SelectedValue
                    If DdlTipoMov.SelectedValue <> "< Seleccionar >" Then psGastoTipoMov = DdlTipoMov.SelectedValue
                    If DdlDoc.SelectedValue <> "< Seleccionar >" Then psGastoDocTipo = DdlDoc.SelectedValue
                    If DdlMoneda.SelectedValue <> "< Seleccionar >" Then psGastoMoneda = DdlMoneda.SelectedValue
                    If DdlUsuario.SelectedValue <> "< Seleccionar >" Then psUser = DdlUsuario.SelectedValue
                    psFecha = Mid(TxtFechaReg.Text, 7, 4) & Mid(TxtFechaReg.Text, 4, 2) & Left(TxtFechaReg.Text, 2)
                    psGasto_Fecha = Mid(TxtFechaGasto.Text, 7, 4) & Mid(TxtFechaGasto.Text, 4, 2) & Left(TxtFechaGasto.Text, 2)
                    If LblModalGasto.Text = "Registro de Gastos" Then
                        dt = objU.Inventario_Gastos_xPersonal(Session("Ruta_Emp"), psFecha, psUser, psHora, pdCCosto, psGastoTipo, psGastoTipoMov, psGastoDocTipo, psGastoDocSerie,
                                                  pdGastoDocNumero, psGastoMoneda, pdGastoImporte, psGastoGlosa, psValorSys, psGasto_Fecha)
                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                pdRegistro = Nz(dr(0))
                            Next
                        End If
                    Else
                        pdRegistro = Nz(TxtNroRegistro.Text)
                        dt = objU.Inventario_Gastos_xPersonal_Update(Session("Ruta_Emp"), psFecha, psUser, psHora, pdCCosto, psGastoTipo, psGastoTipoMov, psGastoDocTipo, psGastoDocSerie,
                                                  pdGastoDocNumero, psGastoMoneda, pdGastoImporte, psGastoGlosa, psValorSys, psGasto_Fecha, pdRegistro)

                    End If
                    'guardar archivo
                    Dim strSaveFileAs As String = ""
                    Dim strStatusMessage As String = ""
                    Dim posicion As Integer = 0
                    Dim i As Integer = 0
                    Dim NCant As String = 0
                    Dim Variable As String = ""
                    Dim NombreArchivo As String = ""
                    Dim Mensaje As String = ""
                    Dim objCas As New Cls_Documentos
                    Dim CodTemaAyuda As Double = 0
                    Dim psCodModulo As String = "18"
                    Dim psNombreCarpeta As String = ""
                    Dim objSeg As New ModuloSeguridad
                    Dim psNombrePag As String = ""

                    psNombrePag = "CRM/Documentacion_Mantenimiento.aspx"
                    'End If
                    lblError.Text = ""
                    dt = Nothing

                    dt = Nothing
                    Dim Ruta_Final As String = Server.MapPath("Gastos")

                    If Not System.IO.Directory.Exists(Ruta_Final) Then
                        ' Crear la carpeta
                        System.IO.Directory.CreateDirectory(Ruta_Final)
                    End If

                    If (fileUpload.HasFile) Then
                        Dim FileName As String = Server.HtmlEncode(fileUpload.FileName)
                        Dim Extensión As String = ""
                        FileName = System.IO.Path.GetExtension(FileName)
                        Extensión = FileName
                        For i = 1 To Len(fileUpload.PostedFile.FileName)
                            If Mid(fileUpload.PostedFile.FileName, i, 1) = "\" Then NCant = NCant + 1
                        Next
                        Variable = UCase(fileUpload.PostedFile.FileName)
                        For i = 1 To NCant
                            posicion = InStr(Variable, "\")
                            Variable = Mid(Variable, posicion + 1)
                            If i = NCant Then NombreArchivo = Variable
                        Next
                        If NombreArchivo = "" Then NombreArchivo = fileUpload.PostedFile.FileName
                        strSaveFileAs = Ruta_Final & "/" & fileUpload.FileName
                        fileUpload.SaveAs(strSaveFileAs)

                        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                        Dim CmdGlobal As New SqlCommand

                        Cn.Open()
                        CmdGlobal.Connection = Cn
                        CmdGlobal.CommandText = " UPDATE TBINVENTARIO_GASTOS SET INVGASTOS_ARCHIVO = '" & NombreArchivo & "' WHERE INVGASTOS_REGISTRO = " & pdRegistro
                        CmdGlobal.ExecuteNonQuery()

                    End If

                    'guardar imagen
                    If pdRegistro > 0 Then
                        If FileUpload1.HasFile Then

                            Dim rutaOriginal As String = Server.MapPath("~/Inventario/ArchivoTemp/original.jpg")
                            Dim rutaComprimida As String = Server.MapPath("~/Inventario/ArchivoTemp/comprimida.jpg")
                            FileUpload1.SaveAs(rutaOriginal)
                            ComprimirImagen(rutaOriginal, rutaComprimida)
                            Dim bytesImagen As Byte() = File.ReadAllBytes(rutaComprimida)

                            Dim filename As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

                            Dim inputStream As System.IO.Stream = FileUpload1.PostedFile.InputStream
                            Dim tamaño As Integer = FileUpload1.PostedFile.ContentLength
                            Dim imagenData(tamaño - 1) As Byte
                            inputStream.Read(imagenData, 0, tamaño)

                            objU.GuardarImagen_Gastos(Session("Ruta_Emp"), pdRegistro, bytesImagen)

                        End If
                    End If
                    BtnLimpiar_Click(sender, e)
                    BtnListar_Click(sender, e)
                End If
            End If
            imagenCarga = Nothing
            imagenCarga.Visible = False
            div_imagen.Visible = False
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub ComprimirImagen(rutaOriginal As String, rutaComprimida As String)
        Dim settings As New ResizeSettings("maxwidth=800&maxheight=600&format=jpg")
        ImageBuilder.Current.Build(rutaOriginal, rutaComprimida, settings)
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        'Response.Redirect("Inventario_Gastos.aspx")
        DdlUsuario.SelectedValue = Session("User")
        DdlTipo.SelectedValue = "< Seleccionar >"
        DdlMoneda.SelectedValue = "2"
        DdlTipoMov.SelectedValue = "< Seleccionar >"
        DdlDoc.SelectedValue = "< Seleccionar >"
        TxtFechaReg.Text = FormatoFecha(FechaActual())
        TxtFechaGasto.Text = FormatoFecha(FechaActual())
        TxtRuc.Enabled = True
        TxtRazonSocial.Enabled = True
        BtnBusca.Enabled = True
        TxtRuc.Text = ""
        TxtRazonSocial.Text = ""
        TxtCodPersona.Text = ""
        TxtDocSerie.Enabled = False : TxtDocSerie.Text = ""
        TxtDocNumero.Enabled = False : TxtDocNumero.Text = ""
        TxtImporte.Text = "0.00"
        TxtGlosa.Text = ""
        'imagenCarga2.Src = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalGasto').modal('hide');", True)
    End Sub

    Sub Ayuda(sender As Object, e As FileUpload)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click

        Dim objU As New Cls_Inventario_Ubicacion
        Dim dt As New DataTable
        Dim psFechaIni As String = ""
        Dim psFechaFin As String = ""
        Dim psUsuario As String = ""
        Dim psTipo As String = ""
        Dim psTipoMov As String = ""
        Try
            If DdlBusUsuario.SelectedValue <> "< Seleccionar >" Then psUsuario = DdlBusUsuario.SelectedValue
            psFechaIni = Mid(TxtBusFecha.Text, 7, 4) & Mid(TxtBusFecha.Text, 4, 2) & Left(TxtBusFecha.Text, 2)
            If TxtBusFecha2.Text <> "" Then
                psFechaFin = Mid(TxtBusFecha2.Text, 7, 4) & Mid(TxtBusFecha2.Text, 4, 2) & Left(TxtBusFecha2.Text, 2)
            Else
                psFechaIni = Mid(TxtBusFecha.Text, 7, 4) & Mid(TxtBusFecha.Text, 4, 2) & Left(TxtBusFecha.Text, 2)
            End If
            If DdlBusTipo.SelectedValue <> "< Seleccionar >" Then psTipo = DdlBusTipo.SelectedValue
            If DdlBusTipoMov.SelectedValue <> "< Seleccionar >" Then psTipoMov = DdlBusTipoMov.SelectedValue
            dt = objU.Inventario_Gastos_Lista(Session("Ruta_Emp"), psUsuario, psFechaIni, psFechaFin, psTipo, psTipoMov)
            GvGastos.DataSource = dt
            GvGastos.DataBind()
            If dt.Rows.Count = 1 Then
                LblRegistro.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count > 1 Then
                LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
            Else
                LblRegistro.Text = "No hay registros."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub

    Private Sub BtnIngresarGastos_Click(sender As Object, e As EventArgs) Handles BtnIngresarGastos.Click
        LblModalGasto.Text = "Registro de Gastos"
        'imagenCarga = Nothing
        imagenCarga.Visible = False
        div_imagen.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalGasto').modal('show');", True)
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalObservaciones').one('hidden.bs.modal', function() { $('#ModalGasto').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnGuardar2_Click(sender As Object, e As EventArgs) Handles BtnGuardar2.Click
        If String.IsNullOrEmpty(TxtCodPersona.Text) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Pop", "alert('Seleccionar Centro de Costos.');$('#ModalGasto').modal('show');", True)
        ElseIf DdlUsuario.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Usuario.');$('#ModalGasto').modal('show');", True)
        ElseIf DdlTipo.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar tipo de gasto.');$('#ModalGasto').modal('show');", True)
        ElseIf DdlDoc.SelectedValue = "< Seleccionar >" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar tipo de documento.');$('#ModalGasto').modal('show');", True)
            'ElseIf TxtDocSerie.Enabled = True And TxtDocSerie.Text = "" Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Serie de documento.');$('#ModalGasto').modal('show');", True)
            'ElseIf TxtDocNumero.Enabled = True And TxtDocNumero.Text = "" Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Número de documento.');$('#ModalGasto').modal('show');", True)
        ElseIf Nz(TxtImporte.Text) = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Importe.');$('#ModalGasto').modal('show');", True)
        Else
        End If
    End Sub

    Private Sub GvGastos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvGastos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Inventario_Ubicacion
        Dim dt As New DataTable
        Dim pdNroReg As Double = 0
        imagenCarga.Visible = False
        div_imagen.Visible = False
        If e.CommandName = "Editar" Then
            LblModalGasto.Text = "Editar de Gastos"
            TxtNroRegistro.Text = GvGastos.Rows(Index).Cells(1).Text
            pdNroReg = Nz(TxtNroRegistro.Text)
            dt = obj.Inventario_GastosLista_xCodigo(Session("Ruta_Emp"), pdNroReg)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    DdlUsuario.SelectedValue = Nu(dr("INVGASTOS_REG_USER"))
                    DdlTipo.SelectedValue = Nu(dr("INVGASTOS_TIPO"))
                    DdlMoneda.SelectedValue = Nu(dr("INVGASTOS_MONEDA"))
                    If Nu(dr("INVGASTOS_TIPO_MOVILIDAD")) <> "" Then DdlTipoMov.SelectedValue = Nu(dr("INVGASTOS_TIPO_MOVILIDAD"))
                    DdlDoc.SelectedValue = Nu(dr("INVGASTOS_DOC_TIPO")) : DdlDoc_SelectedIndexChanged(sender, e)
                    TxtFechaReg.Text = Nu(dr("FECHA_REG"))
                    TxtFechaGasto.Text = Nu(dr("GASTO_FECHA"))
                    TxtRuc.Enabled = True
                    TxtRazonSocial.Enabled = True
                    BtnBusca.Enabled = True
                    TxtRuc.Text = Nu(dr("CECOSE_COD_INTERNO"))
                    TxtRazonSocial.Text = Nu(dr("CECOSE_DESCRIPCION"))
                    TxtCodPersona.Text = Nu(dr("INVGASTOS_CCOSTOS"))
                    TxtDocSerie.Text = Nu(dr("INVGASTOS_DOC_SERIE"))
                    TxtDocNumero.Text = Nu(dr("INVGASTOS_DOC_NUMERO"))
                    TxtImporte.Text = dr("INVGASTOS_IMPORTE")
                    TxtGlosa.Text = Nu(dr("INVGASTOS_GLOSA"))
                Next
            End If
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Dim connectionString As String = Session("Ruta_Emp")

            Dim query As String = "SELECT INVGASTOS_IMAGEN AS Imagen FROM TBINVENTARIO_GASTOS WHERE INVGASTOS_REGISTRO = @CodGasto"
            Using connection As New SqlConnection(connectionString)
                Using cmd As New SqlCommand(query, connection)
                    cmd.Parameters.Add("@CodGasto", SqlDbType.Int).Value = pdNroReg ' Ajusta el valor del ID según el registro que desees mostrar
                    connection.Open()

                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("Imagen")) Then
                                Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imagenCarga.ImageUrl = "data:image/jpeg;base64," + base64String
                                Session("NuevaImagen") = "No"
                                imagenCarga.Visible = True
                                div_imagen.Visible = True
                            End If
                        End If
                    End Using
                End Using
            End Using

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalGasto').modal('show');", True)
        End If
    End Sub
End Class
