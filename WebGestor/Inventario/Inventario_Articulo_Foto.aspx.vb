Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports ImageResizer



Partial Class Inventario_Inventario_Articulo_Foto
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Dim CodSalida As String = ""
    Dim i As Long = 0
    Dim oFuncInv As New clsInv_Procesos
    Dim a As Long = i

    Private Sub BtnArtBuscar_Click(sender As Object, e As EventArgs) Handles BtnArtBuscar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnTomarFoto.Attributes.Add("OnClick", "window.open('Capturar_Almacenar_Foto.aspx',null,'height=800,width=700');")
            Ocultar_Visible_Imagen(False)
            TxtArtCodigo.Text = ""
            TxtArtDescripcion.Text = ""
            Call LLenar_TipoaArticulo()
            DdlTipoBA.SelectedValue = "< Seleccionar >"
            Call Llena_Ubicacion(ddlUbicacion)
        End If
    End Sub
    Protected Sub btnComprimir_Click(sender As Object, e As EventArgs)
        If FileUpload1.HasFile Then
            Dim rutaOriginal As String = Server.MapPath("~/Inventario/ArchivoTemp/original.jpg")
            Dim rutaComprimida As String = Server.MapPath("~/Inventario/ArchivoTemp/comprimida.jpg")

            FileUpload1.SaveAs(rutaOriginal)
            ComprimirImagen(rutaOriginal, rutaComprimida)

        End If
    End Sub
    Protected Sub BtnAgregarArticulo_Click(sender As Object, e As EventArgs) Handles BtnAgregarArticulo.Click
        Dim obj As New Cls_Catalogo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As Double = 0
        codigo = TxtArtCodigo.Text

        Dim nomImg As String = FileUpload1.FileName.ToString


        If BtnAgregarArticulo.Text = "Guardar Imagen" Then
            If FileUpload1.HasFile Then
                Dim rutaOriginal As String = Server.MapPath("~/Inventario/ArchivoTemp/original.jpg")
                Dim rutaComprimida As String = Server.MapPath("~/Inventario/ArchivoTemp/comprimida.jpg")
                FileUpload1.SaveAs(rutaOriginal)
                ComprimirImagen(rutaOriginal, rutaComprimida)
                Dim bytesImagen As Byte() = File.ReadAllBytes(rutaComprimida)

                Using readerI As New BinaryReader(FileUpload1.PostedFile.InputStream)
                    obj.GuardarImagen(psconexion, codigo, bytesImagen, nomImg)
                    Ocultar_Visible_Imagen(False)
                    btnTomarFoto.Visible = False
                    Session("nomImagen") = ""
                    Session("Imagen") = ""
                    'If (vnAncho < 640 OrElse vnAlto < 480) Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar una imagen mayor a 640 x 480');", True)
                    TxtNombreImagen.Text = nomImg
                End Using
            ElseIf Session("nomImagen").ToString() <> "" Then
                obj.GuardarImagen(psconexion, codigo, Session("Imagen"), Session("nomImagen"))
                TxtNombreImagen.Text = Session("nomImagen")
                Ocultar_Visible_Imagen(False)
                btnTomarFoto.Visible = False
                Session("nomImagen") = ""
                Session("Imagen") = ""
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una imagen');", True)
            End If
            BtnListar_Click(sender, e)
        End If
    End Sub

    Protected Sub ComprimirImagen(rutaOriginal As String, rutaComprimida As String)
        Dim settings As New ResizeSettings("maxwidth=800&maxheight=600&format=jpg")
        ImageBuilder.Current.Build(rutaOriginal, rutaComprimida, settings)
    End Sub

    Private Sub BtnMostrar_Click(sender As Object, e As EventArgs) Handles BtnMostrar.Click

        Dim nombreImg As String = TxtNombreImagen.Text
        If nombreImg = "" Then
        Else
            ComprimirImagenEnBaseDeDatos()
        End If
        Session("CodArt") = TxtArtCodigo.Text
        Ocultar_Visible_Imagen(True)
        Dim idCodArticulo As Integer = TxtArtCodigo.Text
        Dim imagen As Imagenes = ImagenesArt.GetImagenById(idCodArticulo, Session("Ruta_Emp"))
        Session("nomImagen") = imagen.ART_IMG_NOM
        Session("Imagen") = imagen.Imagen
        If nombreImg = "" Then
        Else
            ComprimirImagenEnBaseDeDatos()
        End If
        If nombreImg = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', 'data:image/jpg;base64," + Convert.ToBase64String(Session("Imagen")) + "');", True)
        End If

        Session("NombreArt") = TxtArtDescripcion.Text
        Session("NombreImg") = TxtNombreImagen.Text

        div_imagen.Visible = True
        TxtNombreImagen.Visible = True
        lblNombreimg.Text = "Nombre de la imagen"
    End Sub

    Protected Sub ComprimirImagenEnBaseDeDatos()
        ' Cadena de conexión a la base de datos
        Dim connectionString As String = Session("Ruta_Emp")

        ' Establece la consulta para recuperar la imagen
        Dim query As String = "SELECT ART_IMG FROM TBINV_ARTICULOS WHERE ART_CODIGO = " & TxtArtCodigo.Text

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

                '
                ' Lee los bytes de la imagen comprimida
                Dim bytesImagenComprimida As Byte() = File.ReadAllBytes(rutaTemporal)

                ' Actualiza los bytes de la imagen comprimida en la base de datos
                Dim updateQuery As String = "UPDATE TBINV_ARTICULOS SET ART_IMG = @Imagen WHERE ART_CODIGO = " & TxtArtCodigo.Text

                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@Imagen", bytesImagenComprimida)
                    updateCommand.ExecuteNonQuery()
                End Using


                ' Elimina el archivo temporal
                File.Delete(rutaTemporal)
            End Using
        End Using
    End Sub


    Sub Ocultar_Visible_Imagen(ByVal vf As Boolean)
        lblNombreimg.Text = ""
        TxtNombreImagen.Visible = False
        FileUpload1.Visible = vf
        FileNombre.Visible = vf
        BtnAgregarArticulo.Visible = vf
        BtnCancelarArituclo.Visible = vf

        div_imagen.Visible = False

    End Sub
    Private Sub ListaBienesInventariados()
        Dim dt As New DataTable
        Dim obj As New Cls_Inventario_Verificacion

        Dim psUbicatipo As String = ""
        Dim psUbicaCodigo As Double = 0
        Dim pdCodInv_Ubica As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigo.Text.ToString)
        pdCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        If RBAlmacen.Checked Then
            psUbicatipo = "1"
        ElseIf RBCentroC.Checked Then
            psUbicatipo = "2"
        End If
        Dim pdUbicacion As Double = 0
        If ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
            pdUbicacion = Nz(ddlUbicacion.SelectedValue)
        End If
        Try
            If pdUbicacion = 0 Then
                dt = obj.Lista_Articulos_xInventario(Session("Ruta_Emp"), 0, psUbicatipo, psUbicaCodigo, pdCodInv_Ubica, pdUbicacion)
                GvListaBienes.DataSource = dt
                GvListaBienes.DataBind()
                gvListaArticulo.DataSource = Nothing
                gvListaArticulo.DataBind()
                gvArtPlacados.DataSource = Nothing
                gvArtPlacados.DataBind()
                gvArtPlacadosOf.DataSource = Nothing
                gvArtPlacadosOf.DataBind()
                If dt.Rows.Count > 1 Then lblRegistroInv.Text = "Hay " & dt.Rows.Count & " registros."
                If dt.Rows.Count = 1 Then lblRegistroInv.Text = "Hay 1 registro."
                If dt.Rows.Count = 0 Then lblRegistroInv.Text = "No hay registros."
            Else
                dt = obj.Lista_Articulos_xInventario(Session("Ruta_Emp"), 0, psUbicatipo, psUbicaCodigo, pdCodInv_Ubica, pdUbicacion)
                gvListaArticulo.DataSource = dt
                gvListaArticulo.DataBind()
                GvListaBienes.DataSource = Nothing
                GvListaBienes.DataBind()
                gvArtPlacados.DataSource = Nothing
                gvArtPlacados.DataBind()
                gvArtPlacadosOf.DataSource = Nothing
                gvArtPlacadosOf.DataBind()
                If dt.Rows.Count > 1 Then lblRegistroInv.Text = "Hay " & dt.Rows.Count & " registros."
                If dt.Rows.Count = 1 Then lblRegistroInv.Text = "Hay 1 registro."
                If dt.Rows.Count = 0 Then lblRegistroInv.Text = "No hay registros."
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try


    End Sub


    Private Sub ListaBienesxPlacar()
        Dim dt As New DataTable
        Dim obj As New Cls_Inventario_Verificacion

        Dim psNomArt As String = ""
        Dim pdCodArt As Double = 0
        Dim pdCodInv_Ubica As Double = 0
        If TxtArtCodigo.Text.ToString <> "" Then pdCodArt = Nz(TxtArtCodigo.Text.ToString)
        pdCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        If TxtArtDescripcion.Text <> "" Then psNomArt = TxtArtDescripcion.Text
        Dim pdUbicacion As Double = 0
        If ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
            pdUbicacion = Nz(ddlUbicacion.SelectedValue)
        End If
        Try
            If pdUbicacion = 0 Then
                dt = obj.Lista_Articulos_xPlacar(Session("Ruta_Emp"), 0, psNomArt, pdCodArt, pdCodInv_Ubica, pdUbicacion)
                gvArtPlacadosOf.DataSource = dt
                gvArtPlacadosOf.DataBind()
                gvArtPlacados.DataSource = Nothing
                gvArtPlacados.DataBind()
                gvListaArticulo.DataSource = Nothing
                gvListaArticulo.DataBind()
                GvListaBienes.DataSource = Nothing
                GvListaBienes.DataBind()
                If dt.Rows.Count > 1 Then lblRegistroInv.Text = "Hay " & dt.Rows.Count & " registros."
                If dt.Rows.Count = 1 Then lblRegistroInv.Text = "Hay 1 registro."
                If dt.Rows.Count = 0 Then lblRegistroInv.Text = "No hay registros."
            Else
                dt = obj.Lista_Articulos_xPlacar(Session("Ruta_Emp"), 0, psNomArt, pdCodArt, pdCodInv_Ubica, pdUbicacion)
                gvArtPlacados.DataSource = dt
                gvArtPlacados.DataBind()
                gvArtPlacadosOf.DataSource = Nothing
                gvArtPlacadosOf.DataBind()
                gvListaArticulo.DataSource = Nothing
                gvListaArticulo.DataBind()
                GvListaBienes.DataSource = Nothing
                GvListaBienes.DataBind()
                If dt.Rows.Count > 1 Then lblRegistroInv.Text = "Hay " & dt.Rows.Count & " registros."
                If dt.Rows.Count = 1 Then lblRegistroInv.Text = "Hay 1 registro."
                If dt.Rows.Count = 0 Then lblRegistroInv.Text = "No hay registros."
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try


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
    Protected Function VerDatos(ByVal codigo As String) As DataTable
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmd As New SqlCommand
        cmd.CommandText = "select A.ART_CODIGO, A.ART_IMG, A.ART_ABREV, A.ART_TIPO," &
                            " A.ART_CLASIFICACION, A.ARTMAR_CODIGO, A.ARTMOD_CODIGO, " &
                            " A.ARMODE_CODIGO, A.ART_PESO, A.ART_VOLUMEN, A.ART_VOL_ALTO, " &
                            " A.ART_VOL_ANCHO, A.ART_VOL_LARGO, (SELECT CONCAT(C.CLAS_NUMERO, ' - ', C.CLAS_NOMBRE) " &
                            " FROM TBINV_ARTICULO_CLASIFICACION C WHERE C.CLAS_CODIGO = A.ART_CLASIFICACION) " &
                            " from TBINV_ARTICULOS A WHERE A.ART_CODIGO = " + codigo
        cmd.CommandType = CommandType.Text
        cmd.Connection = cn
        cn.Open()
        Dim imagen As New DataTable
        imagen.Load(cmd.ExecuteReader())
        cn.Close()

        Return imagen
    End Function


    Sub VerImg(ByVal image As String)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalImagen').modal('show');", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenVisualizar').setAttribute('src', '" + image + "');", True)
    End Sub
    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        LblCodClasificacionBA.Text = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        Dim dtArt As New DataTable
        dtArt = Nothing
        GvBusArticulo.DataSource = dtArt
        GvBusArticulo.DataBind()
    End Sub
    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Call Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
    End Sub
    Private Sub LLenar_TipoaArticulo()
        Dim dt As New DataTable
        dt = Nothing
        dt = objCat.Lista_Tipo(Session("Ruta_Emp"))
        DdlTipoBA.DataSource = dt
        DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipoBA.DataBind()
        DdlTipoBA.Items.Add("< Seleccionar >")
        DdlTipoBA.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Dim dt As New DataTable
        Dim psListaArt As String = "1"
        Dim psListaMarca As String = "1"
        Dim psListaModelo As String = "1"
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As Double = 0
        Codigo = Nz(TxtCodArticuloBA.Value.ToString)
        Dim Clasificacion As String = LblCodClasificacionBA.Text.ToString

        Dim Descripcion As String = TxtDescripcionBA.Value.ToString
        Dim Tipo As String = DdlTipoBA.SelectedValue.ToString
        Dim NuPart As String = TxtNumParteBA.Value.ToString
        Dim CodEs As String = TxtCodEspecificoBA.Value.ToString
        Dim marca As Double = 0
        marca = Nz(LblCodMarcaBA.Text.ToString)
        Dim modelo As Double = 0
        modelo = Nz(LblCodModeloBA.Text.ToString)

        If marca <> 0 Then psListaMarca = ""
        If modelo <> 0 Then psListaModelo = ""
        If Codigo <> 0 Then psListaArt = ""
        If Tipo = "< Seleccionar >" Then Tipo = ""

        dt = objCat.Lista_ArticuloxBusqueda(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)

        If dt.Rows.Count > 0 Then
            GvBusArticulo.DataSource = dt
            GvBusArticulo.DataBind()
        Else
            GvBusArticulo.DataSource = Nothing
            GvBusArticulo.DataBind()
        End If

    End Sub

    Private Sub GvBusArticulo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusArticulo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            TxtArtCodigo.Text = GvBusArticulo.Rows(Index).Cells(1).Text
            TxtArtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtNombreImagen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
            'BtnMostrar_Click(sender, e)
        End If
    End Sub

    Protected Sub BtnCancelarArituclo_Click(sender As Object, e As EventArgs) Handles BtnCancelarArituclo.Click

        Ocultar_Visible_Imagen(False)
        Session("nomImagen") = ""
        Session("Imagen") = ""
        div_imagen.Visible = False
        TxtNombreImagen.Visible = False
        lblNombreimg.Text = ""
        'btnTomarFoto.Visible = False
    End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        Dim dt As New DataTable
        dt = Nothing
        GvBusArticulo.DataSource = dt
        GvBusArticulo.DataBind()
        TxtArtCodigo.Text = ""
        TxtArtDescripcion.Text = ""
        TxtNombreImagen.Text = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        LblCodClasificacionBA.Text = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        TxtCodigo.Text = ""
        TxtDescripcion.Text = ""
        LblUbicaCodigo.Text = ""
        LblUbicaCodigoInv.Text = ""
        LblUbicacion.Text = ""
        gvListaArticulo.DataSource = Nothing
        gvListaArticulo.DataBind()
        GvListaBienes.DataSource = Nothing
        GvListaBienes.DataBind()
        BtnCancelarArituclo_Click(sender, e)
    End Sub

    Protected Sub BtnCargaArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargaArchivo.Click
        Dim sourceFolderPath As String = "c:\ImagenesArticulos"

        Try
            Dim psGuiaSerie As String = ""
            Dim psDato As String = ""
            Dim psGuion As Double = 0
            Dim psDoc As String = ""
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Session("UnaVez") = "1"
            Cn.Open() : CmdGlobal.Connection = Cn
            Dim psCodart As Double = 0
            Dim folderInfo As New DirectoryInfo(sourceFolderPath)

            If folderInfo.Exists Then
                If Session("UnaVez") = "1" Then
                    Dim files As FileInfo() = folderInfo.GetFiles()
                    For Each fileArc As FileInfo In files
                        ' Mueve el archivo a la carpeta de destino
                        Dim destinationFilePath As String = Path.Combine(sourceFolderPath, fileArc.Name)


                        ' Ruta relativa de la imagen en la carpeta de tu proyecto
                        Dim rutaImagen As String = destinationFilePath
                        Dim psNombreImagen As String = fileArc.Name
                        ' Lee el contenido de la imagen en un arreglo de bytes
                        Dim imagenBytes() As Byte = System.IO.File.ReadAllBytes(rutaImagen)
                        psGuiaSerie = System.IO.Path.GetExtension(rutaImagen)
                        psGuion = Len(fileArc.Name) - Len(psGuiaSerie)
                        psDato = fileArc.Name.Substring(0, psGuion)
                        psCodart = Nz(psDato)
                        ' Guarda la imagen en la base de datos
                        Using con As New SqlConnection(Session("Ruta_Emp"))
                            Using cmd As New SqlCommand("update tbinv_articulos set art_img_nom = @Nombre, art_img = @Imagen  where art_codigo =  " & psCodart & "", con)
                                cmd.Parameters.AddWithValue("@Imagen", imagenBytes)
                                cmd.Parameters.AddWithValue("@Nombre", psNombreImagen)
                                con.Open()
                                cmd.ExecuteNonQuery()
                            End Using
                        End Using
                        ' Verifica si el archivo existe en la carpeta de origen antes de moverlo

                    Next
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Archivos cargados exitosamente.')", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('La carpeta de origen no existe.')", True)

            End If

            Cn.Close()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicacion: " & ex.Message & ".')", True)
        End Try
    End Sub

    Private Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click

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
        'inventario = Nz(DdlInventario.SelectedValue.ToString)
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
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
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
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblUbicaCodigoInv.Text = ""
        lblRegistroInv.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaBienes.DataSource = dt
        GvListaBienes.DataBind()
        gvListaArticulo.DataSource = dt
        gvListaArticulo.DataBind()
        lblRegistroInv.Text = ""
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblUbicaCodigoInv.Text = ""
        lblRegistroInv.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaBienes.DataSource = dt
        GvListaBienes.DataBind()
        lblRegistroInv.Text = ""
        gvListaArticulo.DataSource = dt
        gvListaArticulo.DataBind()
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        ListaBienesInventariados()
    End Sub

    Private Sub GvListaBienes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaBienes.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Imagen" Then
            TxtArtCodigo.Text = GvListaBienes.Rows(Index).Cells(4).Text
            TxtArtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaBienes.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtNombreImagen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaBienes.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
        End If
    End Sub

    Private Sub RbTodos_CheckedChanged(sender As Object, e As EventArgs) Handles RbTodos.CheckedChanged
        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblUbicaCodigoInv.Text = ""
        lblRegistroInv.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaBienes.DataSource = dt
        GvListaBienes.DataBind()
        lblRegistroInv.Text = ""
        gvListaArticulo.DataSource = dt
        GvListaBienes.DataBind()
    End Sub

    Private Sub gvListaArticulo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvListaArticulo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Imagen" Then
            TxtArtCodigo.Text = gvListaArticulo.Rows(Index).Cells(1).Text
            TxtArtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaArticulo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtNombreImagen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaArticulo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            BtnMostrar_Click(sender, e)
        End If
    End Sub

    Private Sub BtnListarxPlacar_Click(sender As Object, e As EventArgs) Handles BtnListarxPlacar.Click
        ListaBienesxPlacar()
    End Sub

    Private Sub gvArtPlacados_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvArtPlacados.RowCommand
        Dim Index As Integer = 0
        Dim cn As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim psNomArt As String = ""
        Dim pdCodInv_Ubica As Double = 0
        Dim pdUbicacion As Double = 0
        pdCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        If ddlUbicacion.SelectedValue <> "< Seleccionar >" Then pdUbicacion = Nz(ddlUbicacion.SelectedValue)
        Dim dtListado As New DataTable
        Dim obj As New Cls_Inventario_Verificacion


        If e.CommandName = "Detalle" Then
            Index = Convert.ToInt32(e.CommandArgument)
            Try
                Index = Convert.ToInt32(e.CommandArgument)
                pdCodArt = gvArtPlacados.Rows(Index).Cells(2).Text.Trim
                'pdCodInv_Ubica = gvArtPlacados.Rows(Index).Cells(10).Text.Trim
                gvDetalle.DataSource = obj.Lista_Articulos_BienesxPlacar(Session("Ruta_Emp"), 0, psNomArt, pdCodArt, pdCodInv_Ubica, pdUbicacion)
                gvDetalle.DataBind()

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
            Catch ex As SqlException

            Catch ex As Exception

            Finally
            End Try
        End If
    End Sub

    Private Sub gvArtPlacadosOf_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvArtPlacadosOf.RowCommand
        Dim Index As Integer = 0
        Dim cn As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim psNomArt As String = ""
        Dim pdCodInv_Ubica As Double = 0
        Dim pdUbicacion As Double = 0
        pdCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        If ddlUbicacion.SelectedValue <> "< Seleccionar >" Then pdUbicacion = Nz(ddlUbicacion.SelectedValue)
        Dim dtListado As New DataTable
        Dim obj As New Cls_Inventario_Verificacion


        If e.CommandName = "Detalle" Then
            Index = Convert.ToInt32(e.CommandArgument)
            Try
                Index = Convert.ToInt32(e.CommandArgument)
                pdCodArt = gvArtPlacadosOf.Rows(Index).Cells(5).Text.Trim
                If pdCodInv_Ubica = 0 Then pdCodInv_Ubica = gvArtPlacadosOf.Rows(Index).Cells(10).Text.Trim
                If pdUbicacion = 0 Then pdUbicacion = gvArtPlacadosOf.Rows(Index).Cells(12).Text.Trim
                gvDetalle.DataSource = obj.Lista_Articulos_BienesxPlacar(Session("Ruta_Emp"), 0, psNomArt, pdCodArt, pdCodInv_Ubica, pdUbicacion)
                gvDetalle.DataBind()

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
            Catch ex As SqlException

            Catch ex As Exception

            Finally
            End Try
        End If
    End Sub

    Private Sub BtnCerrarModal_Click(sender As Object, e As EventArgs) Handles BtnCerrarModal.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub
End Class
