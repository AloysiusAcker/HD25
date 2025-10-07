Imports System.Data.SqlClient
Imports WebGestor
Imports System.Data
Imports System.IO
Imports System.Net
Imports System.Drawing.Imaging
Partial Class Menu_IngresarElemento
    Inherits System.Web.UI.Page
    Dim imgThum As String
    Const ArchImagen As String = "\\Tecnologias\Imagenes\MImg.jpg"
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        ' Response.Redirect("_Default.aspx")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New Listados
            Dim dt As New Data.DataTable
            'Dim Nombre As String
            Dim lbl As New Label
            lblTitulo.InnerText = Session("MenuNom")
            Title = Session("MenuNom")
            dt = obj.Listar_Campos(Session("MenuCod"))
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    If ELEMENTO_NOMBRE.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_NOMBRE.Visible = True : ELEMENTO_NOMBRE.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : ELEMENTO_NOMBRE2.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) & " (Html)" : ELEMENTO_NOMBRE2.Visible = True : txtNombre.Visible = True : txtNombreHtml.Visible = True
                    If ELEMENTO_CATEGORIA.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_CATEGORIA.Visible = True : ELEMENTO_CATEGORIA.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : cboCategoria.Visible = True
                    If ELEMENTO_DESCRIP_CORTA.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_DESCRIP_CORTA.Visible = True : ELEMENTO_DESCRIP_CORTA.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : txtDescripCorta.Visible = True
                    If ELEMENTO_DESCRIP_LARGA.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_DESCRIP_LARGA.Visible = True : ELEMENTO_DESCRIP_LARGA.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : txtDescripcCompleta.Visible = True
                    If ELEMENTO_FECHA1.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_FECHA1.Visible = True : ELEMENTO_FECHA1.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : txtFecha.Visible = True
                    If ELEMENTO_IMAGEN.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_IMAGEN.Visible = True : ELEMENTO_IMAGEN.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : Archivo.Visible = True
                    If ELEMENTO_COMPLETAR1.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_COMPLETAR1.Visible = True : ELEMENTO_COMPLETAR1.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : txtComentario1.Visible = True
                    If ELEMENTO_COMPLETAR2.ID = Nu(drMenuItem("CAMPO_NOMBRE")) Then ELEMENTO_COMPLETAR2.Visible = True : ELEMENTO_COMPLETAR2.Text = Nu(drMenuItem("CAMPO_ETIQUETA")) : txtComentario2.Visible = True
                Next
            End If
            dt = Nothing
            Call Cargar_Categoria()
            If Session("Modificar") = "S" Then Call Llenar_Datos()
        End If
    End Sub
    Public Function GetPhoto(ByVal filePath As String) As Byte()
        Dim stream As FileStream = New FileStream(filePath, FileMode.Open, FileAccess.Read)
        Dim reader As BinaryReader = New BinaryReader(stream)
        Dim photo() As Byte = reader.ReadBytes(stream.Length)
        reader.Close()
        stream.Close()
        Return photo
    End Function

    Private Sub Llenar_Datos()
        Dim obj As New Listados
        Dim dt As New Data.DataTable
        Try
            dt = obj.Listar_Elemento(Session("MenucodElement"), Session("CodGrupoEmpresa"), Session("CodEmpresa"), Session("MenuCod"))
            If dt.Rows.Count > 0 Then
                For Each dr As Data.DataRow In dt.Rows
                    If ELEMENTO_NOMBRE.Visible = True And txtNombre.Visible = True Then txtNombre.Text = dr("ELEMENTO_NOMBRE").ToString
                    If ELEMENTO_NOMBRE2.Visible = True And txtNombreHtml.Visible = True Then txtNombreHtml.Text = dr("ELEMENTO_NOMBRE2").ToString
                    If ELEMENTO_CATEGORIA.Visible = True And cboCategoria.Visible = True Then cboCategoria.SelectedValue = dr("ELEMENTO_CATEGORIA").ToString
                    If ELEMENTO_DESCRIP_CORTA.Visible = True And txtDescripCorta.Visible = True Then txtDescripCorta.Text = dr("ELEMENTO_DESCRIP_CORTA").ToString
                    If ELEMENTO_DESCRIP_LARGA.Visible = True And txtDescripcCompleta.Visible = True Then txtDescripcCompleta.Text = dr("ELEMENTO_DESCRIP_LARGA").ToString
                    If ELEMENTO_FECHA1.Visible = True And txtFecha.Visible = True Then txtFecha.Text = FormatoFecha(dr("ELEMENTO_FECHA1").ToString)
                    If ELEMENTO_COMPLETAR1.Visible = True And txtComentario1.Visible = True Then txtComentario1.Text = dr("ELEMENTO_COMPLETAR1").ToString
                    If ELEMENTO_COMPLETAR2.Visible = True And txtComentario2.Visible = True Then txtComentario2.Text = dr("ELEMENTO_COMPLETAR2").ToString
                Next
            End If
            dt = Nothing
            Img.Visible = True
            Img.ImageUrl = "ArchModMenu\Imagenes\m" & Session("MenuCodElement") & ".jpg"
            FileCopy("ArchModMenu\Imagenes\m" & Session("MenuCodElement") & ".jpg", ArchImagen)
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch Ex As Exception
            'lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Sub Cargar_Categoria()
        Dim obj As New Listados
        cboCategoria.Items.Clear()
        cboCategoria.DataSource = obj.Listar_Categoria(Session("CodGrupoEmpresa"), Session("CodEmpresa"), Session("MenuCod"))
        cboCategoria.DataTextField = "CATEG_NOMBRE"
        cboCategoria.DataValueField = "CATEG_CODIGO"
        cboCategoria.DataBind()
        cboCategoria.Items.Add("(Seleccionar)") : cboCategoria.SelectedValue = "(Seleccionar)"
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        lblError.Text = ""
        If ELEMENTO_NOMBRE.Visible = True And txtNombre.Visible = True And txtNombre.Text = "" Then lblError.Text = lblError.Text & " <br> - Ingresar " & ELEMENTO_NOMBRE.Text & "."
        If ELEMENTO_NOMBRE2.Visible = True And txtNombreHtml.Visible = True And txtNombreHtml.Text = "" Then lblError.Text = lblError.Text & " <br> - Ingresar " & ELEMENTO_NOMBRE2.Text & "."
        If ELEMENTO_CATEGORIA.Visible = True And cboCategoria.Visible = True And cboCategoria.SelectedValue = "(Seleccionar)" Then lblError.Text = lblError.Text & " <br> - Seleccionar " & ELEMENTO_CATEGORIA.Text & "."
        If ELEMENTO_DESCRIP_CORTA.Visible = True And txtDescripCorta.Visible = True And txtDescripCorta.Text = "" Then lblError.Text = lblError.Text & " <br> - Ingresar " & ELEMENTO_DESCRIP_CORTA.Text & "."
        If ELEMENTO_DESCRIP_LARGA.Visible = True And txtDescripcCompleta.Visible = True And txtDescripcCompleta.Text = "" Then lblError.Text = lblError.Text & " <br> - Ingresar " & ELEMENTO_DESCRIP_LARGA.Text & "."
        If ELEMENTO_FECHA1.Visible = True And txtFecha.Visible = True And txtFecha.Text = "" Then lblError.Text = lblError.Text & " <br> - Ingresar " & ELEMENTO_FECHA1.Text & "."
        If ELEMENTO_COMPLETAR1.Visible = True And txtComentario1.Visible = True And txtComentario1.Text = "" Then lblError.Text = lblError.Text & " <br> - Ingresar " & ELEMENTO_COMPLETAR1.Text & "."
        If ELEMENTO_COMPLETAR2.Visible = True And txtComentario2.Visible = True And txtComentario2.Text = "" Then lblError.Text = lblError.Text & " <br> - Ingresar " & ELEMENTO_COMPLETAR2.Text & "."
        If lblError.Text.Trim <> "" Then
            lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
            Exit Sub
        End If
        Dim FechaIng As String
        lblError.Text = ""
        FechaIng = Right(txtFecha.Text.Trim, 4) + Mid(txtFecha.Text.Trim, 4, 2) + Left(txtFecha.Text.Trim, 2)
        Dim ImgCod As String = ""
        Dim dt As New Data.DataTable
        Try
            Dim obj As New Insertar
            Dim obj2 As New Listados
            'Session("Foto") = ObtenerBytes(Archivo)
            If Session("Modificar") = "N" Then
                Call obj.Insertar_Elemento(Session("CodGrupoEmpresa"), Session("CodEmpresa"), Session("MenuCod"), txtNombre.Text.Trim, txtNombreHtml.Text.Trim, cboCategoria.SelectedValue.Trim, txtDescripCorta.Text.Trim, txtDescripcCompleta.Text.Trim, User.Identity.Name, FechaIng, txtComentario1.Text.Trim, txtComentario2.Text.Trim)
                If Img.Visible = True And Img.ImageUrl <> "" Then
                    dt = obj2.Ultimo_Elemento
                    If dt.Rows.Count > 0 Then
                        For Each dr As Data.DataRow In dt.Rows
                            ImgCod = dr("MAX_ELEMENTO") + 1
                        Next
                    Else
                        ImgCod = 1
                    End If
                    dt = Nothing
                    System.IO.File.Copy(ArchImagen, "\\" & NomServer & "\\ArchModMenu\Imagenes\m" + ImgCod & ".jpg")
                    'FileCopy(ArchImagen, "\\" & NomServer & "\\ArchModMenu\Imagenes\m" + ImgCod & ".jpg")
                End If
            ElseIf Session("Modificar") = "S" Then
                Call obj.Modificar_Elemento(Session("MenuCodElement"), Session("CodGrupoEmpresa"), Session("CodEmpresa"), Session("MenuCod"), txtNombre.Text.Trim, txtNombreHtml.Text.Trim, cboCategoria.SelectedValue.Trim, txtDescripCorta.Text.Trim, txtDescripcCompleta.Text.Trim, User.Identity.Name, FechaIng, txtComentario1.Text.Trim, txtComentario2.Text.Trim)
                If Img.Visible = True And Img.ImageUrl <> "" Then
                    System.IO.File.Copy(ArchImagen, "\\" & NomServer & "\\ArchModMenu\Imagenes\m" & Session("MenuCodElement") & ".jpg")
                End If
            End If
            Call Limpiar()
            lblError.Text = "Los datos han sido ingresados."
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch Ex As Exception
            lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Private Function ObtenerBytes(ByVal ObjetoInput As HtmlInputFile) As Byte()
        'Este función obtiene los Bytes que contiene el fichero del Input File y retorna un Matriz de Bytes
        Dim BytesDeLaImagen(ObjetoInput.PostedFile.ContentLength) As Byte
        ObjetoInput.PostedFile.InputStream.Position = 0
        ObjetoInput.PostedFile.InputStream.Read(BytesDeLaImagen, 0, ObjetoInput.PostedFile.ContentLength - 1)
        Return BytesDeLaImagen
    End Function
    Private Sub Limpiar()
        txtNombre.Text = ""
        txtNombreHtml.Text = ""
        txtDescripCorta.Text = ""
        txtDescripcCompleta.Text = ""
        txtFecha.Text = ""
        txtComentario1.Text = ""
        txtComentario2.Text = ""
        cboCategoria.Items.Add("(Seleccionar)") : cboCategoria.SelectedValue = "(Seleccionar)"
        Img.Visible = False
        Img.ImageUrl = ""
    End Sub
    Protected Sub btnImg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImg.Click
        Img.Visible = False
        Img.ImageUrl = ""
        Dim strPostedFileName As String = Archivo.PostedFile.FileName
        Dim strExtn As String = System.IO.Path.GetExtension(strPostedFileName).ToLower
        If (strExtn = ".jpg") Or (strExtn = ".bpm") Or (strExtn = ".gif") Then
            If Archivo.PostedFile.FileName <> "" Then
                Try
                    Dim strFileName As String = ""
                    Dim strFilePath As String = ""
                    Dim strFolder As String
                    strFolder = "\\" & NomServer & "\\Imagenes\"
                    'Obtener el nombre del archivo que se expone. 
                    strFileName = Archivo.PostedFile.FileName
                    strFileName = Path.GetFileName(strFileName)
                    'Crear el directorio si no existe. 
                    If (Not Directory.Exists(strFolder)) Then
                        Directory.CreateDirectory(strFolder)
                    End If
                    'Guardar el archivo cargado en el servidor. 
                    strFilePath = strFolder & strFileName
                    If File.Exists(strFilePath) Then
                        lblError.Text = strFileName & " ya existe en el servidor."
                    Else
                        Archivo.PostedFile.SaveAs(strFilePath)
                        lblError.Text = strFileName & " se ha cargado correctamente."
                    End If
                    Archivo.PostedFile.SaveAs(strFolder + Archivo.Name)
                    FileCopy(strFolder + Archivo.Name, ArchImagen)
                    Img.Visible = True
                    Img.ImageUrl = ArchImagen
                Catch
                    lblError.Text = "Ha ocurrido un error"
                End Try
            Else
                lblError.Text = "Seleccionar un archivo"
            End If
        Else
        lblError.Text = "no es el formato"
        End If
    End Sub
End Class
