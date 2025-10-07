
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports WebGestor
Imports AspNet
Imports System.Runtime.Serialization
Imports System.Web.Services

Public Class Catalogo_Articulo
    Inherits System.Web.UI.Page
    Public psRutaEmp As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'btnTomarFoto.Attributes.Add("OnClick", "window.open('Capturar_Almacenar_Foto.aspx',null,'height=800,width=700');")
            Llenar_Combos()
            Ocultar_Visible(False)
            psRutaEmp = Session("Ruta_Emp")
        End If
    End Sub
    Protected Sub Concatenar()
        Dim clasificacion As String = Session("Clasificacion")
        Dim marca As String = txtMarca.Text.ToString.Trim
        Dim modelo As String = txtModelo.Text.ToString.Trim
        Dim detalle As String = txtDetalleModelo.Text.ToString.Trim

        If clasificacion = "" And marca = "" And modelo = "" Then
            txtDesc.Text = detalle
        ElseIf detalle = "" And marca = "" And modelo = "" Then
            txtDesc.Text = clasificacion
        ElseIf clasificacion = "" And detalle = "" And modelo = "" Then
            txtDesc.Text = marca
        ElseIf clasificacion = "" And marca = "" And detalle = "" Then
            txtDesc.Text = modelo


        ElseIf clasificacion = "" And marca = "" Then
            txtDesc.Text = modelo + " " + detalle
        ElseIf modelo = "" And marca = "" Then
            txtDesc.Text = clasificacion + detalle
        ElseIf detalle = "" And marca = "" Then
            txtDesc.Text = clasificacion + " " + modelo
        ElseIf clasificacion = "" And modelo = "" Then
            txtDesc.Text = marca + " " + detalle
        ElseIf clasificacion = "" And detalle = "" Then
            txtDesc.Text = marca + " " + modelo
        ElseIf detalle = "" And modelo = "" Then
            txtDesc.Text = clasificacion + " " + marca


        ElseIf clasificacion = "" Then
            txtDesc.Text = marca + " " + modelo + " " + detalle
        ElseIf marca = "" Then
            txtDesc.Text = clasificacion + " " + modelo + " " + detalle
        ElseIf modelo = "" Then
            txtDesc.Text = clasificacion + " " + marca + " " + detalle
        ElseIf detalle = "" Then
            txtDesc.Text = clasificacion + " " + marca + " " + modelo
        End If

        If clasificacion.Length() > 10 Then
            txtAbreviatura.Text = clasificacion.Substring(0, 10)
        Else
            txtAbreviatura.Text = clasificacion
        End If
    End Sub

    Sub VerImg(ByVal image As String)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalImagen').modal('show');", True)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenVisualizar').setAttribute('src', '" + image + "');", True)
    End Sub

    Protected Function VerDatos(ByVal codigo As String) As DataTable
        Dim cn As New SqlConnection(Session("Ruta_Emp"))
        Dim cmd As New SqlCommand
        cmd.CommandText = "select A.ART_CODIGO, A.ART_IMG, A.ART_ABREV, A.ART_TIPO, " &
                            " A.ART_CLASIFICACION, A.ARTMAR_CODIGO, A.ARTMOD_CODIGO, " &
                            " A.ARMODE_CODIGO, A.ART_PESO, A.ART_VOLUMEN, A.ART_VOL_ALTO, " &
                            " A.ART_VOL_ANCHO, A.ART_VOL_LARGO, (SELECT CONCAT(C.CLAS_NUMERO, ' - ', C.CLAS_NOMBRE) " &
                            " FROM TBINV_ARTICULO_CLASIFICACION C WHERE C.CLAS_CODIGO = A.ART_CLASIFICACION),  ART_SKU " &
                            " from TBINV_ARTICULOS A WHERE A.ART_CODIGO = " + codigo
        cmd.CommandType = CommandType.Text
        cmd.Connection = cn
        cn.Open()
        Dim imagen As New DataTable
        imagen.Load(cmd.ExecuteReader())
        cn.Close()

        Return imagen
    End Function

    Protected Sub BtnListarArticulos_Click(sender As Object, e As EventArgs) Handles BtnListarArticulos.Click
        Listar()
    End Sub

    Protected Sub Ocultar_Visible(ByVal vf As Boolean)
        txtCodArt.Visible = vf
        lblCodigo.Visible = vf
        lblTipo.Visible = vf
        DdlTipo.Visible = vf
        lblClasificacion.Visible = vf
        txtClasificacion.Visible = vf
        lblMarca.Visible = vf
        txtMarca.Visible = vf
        lblModelo.Visible = vf
        txtModelo.Visible = vf
        lblDescr.Visible = vf
        txtDesc.Visible = vf
        lblAbrev.Visible = vf
        txtAbreviatura.Visible = vf
        lblNro.Visible = vf
        txtNumP.Visible = vf
        lblCodE.Visible = vf
        txtCodEsp.Visible = vf
        lblUnidad.Visible = vf
        DdlMedida.Visible = vf
        BtnAgregarArticulo.Visible = vf
        BtnBuscarMar.Visible = vf
        BtnBuscarClas.Visible = vf
        BtnBuscarMod.Visible = vf
        lblDetalleModelo.Visible = vf
        txtDetalleModelo.Visible = vf
        BtnModeloDetalleMod.Visible = vf
        BtnCancelarArituclo.Visible = vf
        lblPeso.Visible = vf
        txtPeso.Visible = vf
        lblVolumen.Visible = vf
        txtVolumen.Visible = vf
        lblLargo.Visible = vf
        txtLargo.Visible = vf
        lblAncho.Visible = vf
        txtAncho.Visible = vf
        lblAlto.Visible = vf
        txtAlto.Visible = vf
        BtnCalcularVolumen.Visible = vf
        TxtArtSku.Visible = vf
        LblSku.Visible = vf
    End Sub

    Protected Sub Limpiar_Cajas()
        lblCodClas.Text = ""
        lblCodMar.Text = ""
        lblCodMo.Text = ""
        lblCodDetaMod.Text = ""
        txtCodArt.Text = ""
        txtClasificacion.Text = ""
        txtMarca.Text = ""
        txtModelo.Text = ""
        txtDetalleModelo.Text = ""
        txtDesc.Text = ""
        txtAbreviatura.Text = ""
        txtNumP.Text = ""
        txtCodEsp.Text = ""
        DdlTipo.SelectedValue = "< SELECCIONAR >"
        DdlMedida.SelectedValue = "34"
        txtPeso.Text = ""
        txtVolumen.Text = ""
        txtLargo.Text = ""
        txtAncho.Text = ""
        txtAlto.Text = ""
        TxtArtSku.Text = ""
    End Sub

    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        LblCodClasificacionBA.Text = ""
        DdlTipoBA.SelectedValue = "< SELECCIONAR >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
    End Sub

    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
    End Sub

    Protected Sub Listar()
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Catalogo(psconexion, Session("CodEmpresa"))
        GvListaArticulos.DataSource = dt
        GvListaArticulos.DataBind()
        LblRegistro.Text = ""
        If dt.Rows.Count = 1 Then
            LblRegistro.Text = "Hay 1 registro."
        ElseIf dt.Rows.Count > 1 Then
            LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
        End If
    End Sub

    Protected Sub Llenar_Combos()
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Lista_Tipo(psconexion)
        DdlTipoBA.DataSource = dt
        DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipoBA.DataBind()
        DdlTipoBA.Items.Add("< SELECCIONAR >")
        DdlTipoBA.SelectedValue = "< SELECCIONAR >"

        dt = obj.Lista_Tipo2(psconexion)
        DdlTipo.DataSource = dt
        DdlTipo.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipo.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipo.DataBind()
        DdlTipo.Items.Add("< SELECCIONAR >")
        DdlTipo.SelectedValue = "< SELECCIONAR >"

        dt = obj.Lista_Unidad(psconexion)
        DdlMedida.DataSource = dt
        DdlMedida.DataValueField = "ELEMENTO_CODUNICO"
        DdlMedida.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlMedida.DataBind()

        dt = obj.Lista_Detraccion(psconexion)
        DdlDetraccion.DataSource = dt
        DdlDetraccion.DataValueField = "ELEMEN_CODIGO"
        DdlDetraccion.DataTextField = "ELEMEN_VALOR"
        DdlDetraccion.DataBind()
        DdlDetraccion.Items.Add("--")
        DdlDetraccion.SelectedValue = "--"

        dt = obj.Lista_Tipo_Bien(psconexion)
        DdlTipoBien.DataSource = dt
        DdlTipoBien.DataValueField = "COD_BIEN"
        DdlTipoBien.DataTextField = "DESC_BIEN"
        DdlTipoBien.DataBind()
        DdlTipoBien.Items.Add("< SELECCIONAR >")
        DdlTipoBien.SelectedValue = "< SELECCIONAR >"
    End Sub

    Private Sub Buscar_xCod()
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psListaArt As String = "1"
        Dim psListaMarca As String = "1"
        Dim psListaModelo As String = "1"
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As String = txtCodArt.Text
        Dim Clasificacion As String = ""

        Dim Descripcion As String = ""
        Dim Tipo As String = ""
        Dim NuPart As String = ""
        Dim CodEs As String = ""
        Dim marca As String = ""
        Dim modelo As String = ""

        If marca <> "" Then psListaMarca = ""
        If modelo <> "" Then psListaModelo = ""
        If Codigo <> "" Then psListaArt = ""
        If Tipo = "< SELECCIONAR >" Then Tipo = ""

        dt = obj.Bus_Articulo(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)

        If dt.Rows.Count > 0 Then
            GvListaArticulos.DataSource = dt
            GvListaArticulos.DataBind()
        Else
            GvListaArticulos.DataSource = Nothing
            GvListaArticulos.DataBind()
        End If
        LblRegistro.Text = ""

        If dt.Rows.Count = 1 Then
            LblRegistro.Text = "Hay 1 registro."
        ElseIf dt.Rows.Count > 1 Then
            LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Protected Sub Busqueda_Articulos()
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psListaArt As String = "1"
        Dim psListaMarca As String = "1"
        Dim psListaModelo As String = "1"
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As String = TxtCodArticuloBA.Value.ToString
        Dim Clasificacion As String = LblCodClasificacionBA.Text.ToString
        Dim ps_SKU As String = ""
        Dim Descripcion As String = TxtDescripcionBA.Value.ToString
        Dim Tipo As String = DdlTipoBA.SelectedValue.ToString
        Dim NuPart As String = TxtNumParteBA.Value.ToString
        Dim CodEs As String = TxtCodEspecificoBA.Value.ToString
        Dim marca As String = LblCodMarcaBA.Text.ToString
        Dim modelo As String = LblCodModeloBA.Text.ToString
        If TxtSku.Value <> "" Then ps_SKU = TxtSku.Value
        If marca <> "" Then psListaMarca = ""
        If modelo <> "" Then psListaModelo = ""
        If Codigo <> "" Then psListaArt = ""
        If Tipo = "< SELECCIONAR >" Then Tipo = ""

        dt = obj.Bus_Articulo_xSKU(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo, ps_SKU)

        If dt.Rows.Count > 0 Then
            GvListaArticulos.DataSource = dt
            GvListaArticulos.DataBind()
        Else
            GvListaArticulos.DataSource = Nothing
            GvListaArticulos.DataBind()
        End If
        LblRegistro.Text = ""

        If dt.Rows.Count = 1 Then
            LblRegistro.Text = "Hay 1 registro."
        ElseIf dt.Rows.Count > 1 Then
            LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
        End If

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Protected Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Busqueda_Articulos()
    End Sub

    Protected Sub BtnNuevoArticulo_Click(sender As Object, e As EventArgs) Handles BtnNuevoArticulo.Click
        Dim obj As New Cls_Catalogo
        Dim cn As String = Session("Ruta_Emp")
        Session("Clasificacion") = ""
        Limpiar_Cajas()
        Ocultar_Visible_Imagen(False)
        txtCodArt.Text = obj.Codigo(cn)
        Ocultar_Visible(True)
        BtnAgregarArticulo.Text = "Agregar"
    End Sub
    Protected Sub BtnAgregarArticulo_Click(sender As Object, e As EventArgs) Handles BtnAgregarArticulo.Click
        Dim obj As New Cls_Catalogo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As Double = 0
        codigo = txtCodArt.Text
        Dim Tipo As Double = 0
        If DdlTipo.SelectedValue <> "< SELECCIONAR >" Then
            Tipo = DdlTipo.SelectedValue
        End If
        Dim Clasifi As Double = 0
        Clasifi = Val(lblCodClas.Text)
        Dim codMar As Double = 0
        codMar = Val(lblCodMar.Text)
        Dim codMod As Double = 0
        codMod = Val(lblCodMo.Text)
        Dim codDetaMo As Double = 0
        codDetaMo = Val(lblCodDetaMod.Text)
        Dim descripcion As String = txtDesc.Text
        Dim abrev As String = txtAbreviatura.Text
        Dim parte As String = txtNumP.Text
        Dim codEs As String = txtCodEsp.Text
        Dim uniMe As Double = 0
        If DdlMedida.SelectedValue <> "< SELECCIONAR >" Then
            uniMe = DdlMedida.SelectedValue.ToString
        End If
        Dim nomImg As String = FileUpload1.FileName.ToString
        Dim peso As Double = 0
        peso = Val(txtPeso.Text)
        Dim volumen As Double = 0
        volumen = Val(txtVolumen.Text)
        Dim alto As Double = 0
        alto = Val(txtAlto.Text)
        Dim ancho As Double = 0
        ancho = Val(txtAncho.Text)
        Dim largo As Double = 0
        largo = Val(txtLargo.Text)

        If BtnAgregarArticulo.Text = "Guardar Imagen" Then
            If FileUpload1.HasFile Then
                Using readerI As New BinaryReader(FileUpload1.PostedFile.InputStream)
                    Dim imageI As Byte() = readerI.ReadBytes(FileUpload1.PostedFile.ContentLength)
                    Dim viImagen = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream)
                    Dim vnAncho = viImagen.PhysicalDimension.Width
                    Dim vnAlto = viImagen.PhysicalDimension.Height
                    obj.GuardarImagen(psconexion, codigo, imageI, nomImg)
                    Ocultar_Visible_Imagen(False)
                    btnTomarFoto.Visible = False
                    Session("nomImagen") = ""
                    Session("Imagen") = ""
                    If (vnAncho < 640 OrElse vnAlto < 480) Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar una imagen mayor a 640 x 480');", True)
                End Using
            ElseIf Session("nomImagen").ToString() <> "" Then
                obj.GuardarImagen(psconexion, codigo, Session("Imagen"), Session("nomImagen"))
                Buscar_xCod()
                Ocultar_Visible_Imagen(False)
                btnTomarFoto.Visible = False
                Session("nomImagen") = ""
                Session("Imagen") = ""
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una imagen');", True)
            End If
        Else


            If Tipo = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar el tipo de Artículo');", True)
            ElseIf Clasifi = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe ingresar Clasificación');", True)
            ElseIf descripcion = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe ingresar Descripción');", True)
            ElseIf parte = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe ingresar Número de Parte');", True)
            ElseIf abrev = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe ingresar la Abreviatura');", True)
            ElseIf TxtArtSku.text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe ingresar SKU');", True)
            Else
                Try
                    If peso <> 0 Then
                        peso = Convert.ToDouble(peso)
                    End If
                    If Tipo = 92 Then
                        Dim detraccion As Double = 0
                        If DdlDetraccion.SelectedValue <> "--" Then
                            detraccion = DdlDetraccion.SelectedValue.ToString()
                        End If

                        Dim tipoBien As String = ""
                        If DdlTipoBien.SelectedValue <> "< SELECCIONAR >" Then
                            tipoBien = DdlTipoBien.SelectedValue.ToString()
                        End If
                        If detraccion = 0 Then
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar la Detracción');", True)
                        ElseIf tipoBien = "" Then
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar el Tipo del Bien');", True)
                        Else
                            If BtnAgregarArticulo.Text = "Agregar" Then
                                obj.RegistrarCatalogo(psconexion, codigo, Tipo, Clasifi, codMar, codMod, codDetaMo, descripcion, abrev, parte, codEs, uniMe, detraccion, tipoBien, peso, volumen, alto, ancho, largo, Session("User"), TxtArtSku.Text)
                            ElseIf BtnAgregarArticulo.Text = "Actualizar" Then
                                obj.ActualizaCatalogo(psconexion, codigo, Tipo, Clasifi, codMar, codMod, descripcion, abrev, parte, codEs, uniMe, detraccion, tipoBien, peso, volumen, alto, ancho, largo, TxtArtSku.Text)
                            End If
                            Buscar_xCod()
                            Limpiar_Cajas()
                            Ocultar_Visible(False)
                        End If
                    Else
                        If BtnAgregarArticulo.Text = "Agregar" Then
                            obj.RegistrarCatalogo(psconexion, codigo, Tipo, Clasifi, codMar, codMod, codDetaMo, descripcion, abrev, parte, codEs, uniMe, 0, "", peso, volumen, alto, ancho, largo, Session("User"), TxtArtSku.Text)
                        ElseIf BtnAgregarArticulo.Text = "Actualizar" Then
                            obj.ActualizaCatalogo(psconexion, codigo, Tipo, Clasifi, codMar, codMod, descripcion, abrev, parte, codEs, uniMe, 0, "", peso, volumen, alto, ancho, largo, TxtArtSku.Text)
                        End If
                        Buscar_xCod()
                        Limpiar_Cajas()
                        Ocultar_Visible(False)
                    End If
                Catch ex As FormatException
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El Peso debe ser un número');", True)
                End Try
            End If
        End If
    End Sub

    Sub Ayuda(sender As Object, e As FileUpload)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
    End Sub

    Protected Sub GvListaArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Catalogo
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable
        Dim nomImg As String = FileUpload1.FileName.ToString

        If e.CommandName = "Editar" Then
            Ocultar_Visible_Imagen(False)
            Ocultar_Visible(True)
            txtCodArt.Text = GvListaArticulos.Rows(Index).Cells(4).Text
            txtDesc.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtMarca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(12).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtModelo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(13).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtNumP.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(9).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            If Replace(GvListaArticulos.Rows(Index).Cells(10).Text, "&nbsp;", "") <> "" Then DdlMedida.SelectedValue = GvListaArticulos.Rows(Index).Cells(10).Text

            dt = VerDatos(txtCodArt.Text)
            Dim datos As DataRow = dt.Rows(0)
            If Nu(datos(2)) <> "" Then txtAbreviatura.Text = datos(2)
            If Nu(datos(3)) <> "" Then DdlTipo.SelectedValue = datos(3)
            If Nu(datos(4)) <> "" Then lblCodClas.Text = datos(4)
            If Nu(datos(5)) <> "" Then lblCodMar.Text = datos(5)
            If Nu(datos(6)) <> "" Then lblCodMo.Text = datos(6)
            If Nu(datos(7)) <> "" Then lblCodDetaMod.Text = datos(7)
            If Nu(datos(8)) <> "" Then txtPeso.Text = datos(8)
            If Nu(datos(9)) <> "" Then txtVolumen.Text = datos(9)
            If Nu(datos(10)) <> "" Then txtAlto.Text = datos(10)
            If Nu(datos(11)) <> "" Then txtAncho.Text = datos(11)
            If Nu(datos(12)) <> "" Then txtLargo.Text = datos(12)
            If Nu(datos(3)) = "" Then DdlTipo.SelectedValue = "< SELECCIONAR >"
            If Nu(datos(13)) <> "" Then txtClasificacion.Text = datos(13)
            If Nu(datos(14)) <> "" Then TxtArtSku.Text = datos(14)

            BtnAgregarArticulo.Text = "Actualizar"
        ElseIf e.CommandName = "Eliminar" Then
            dt = obj.EliminarArticulo(cn, GvListaArticulos.Rows(Index).Cells(4).Text)
            Dim dbRow As DataRow = dt.Rows(0)
            If dbRow(0) = "1" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede eliminar el artículo');", True)
            Else
                Listar()
                Ocultar_Visible_Imagen(False)
                Ocultar_Visible(False)
                Limpiar_Cajas()
            End If
        ElseIf e.CommandName = "Imagen" Then
            Dim nombreImg As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(14).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            BtnAgregarArticulo.Text = "Guardar Imagen"
            Ocultar_Visible(False)
            Ocultar_Visible_Imagen(True)
            txtCodArt.Text = GvListaArticulos.Rows(Index).Cells(4).Text
            Dim idCodArticulo As Integer = Convert.ToInt32(txtCodArt.Text)
            Dim imagen As Imagenes = ImagenesArt.GetImagenById(idCodArticulo, Session("Ruta_Emp"))
            Session("nomImagen") = imagen.ART_IMG_NOM
            Session("Imagen") = imagen.Imagen
            If nombreImg = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', 'data:image/jpg;base64," + Convert.ToBase64String(Session("Imagen")) + "');", True)
            End If
            btnTomarFoto.Visible = True
            Session("CodArt") = GvListaArticulos.Rows(Index).Cells(4).Text
            Session("NombreArt") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Session("NombreImg") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaArticulos.Rows(Index).Cells(14).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
        ElseIf e.CommandName = "EliminarImagen" Then
            Dim img As Byte() = System.Text.Encoding.Unicode.GetBytes("0")
            obj.GuardarImagen(Session("Ruta_Emp"), GvListaArticulos.Rows(Index).Cells(4).Text, img, "")
            Ocultar_Visible_Imagen(False)
            Session("nomImagen") = ""
            Session("Imagen") = ""
        End If
    End Sub
    Sub Ocultar_Visible_Imagen(ByVal vf As Boolean)
        txtCodArt.Text = ""
        FileUpload1.Visible = vf
        FileNombre.Visible = vf
        BtnAgregarArticulo.Visible = vf
        BtnCancelarArituclo.Visible = vf
    End Sub
    Sub Listar_Nivel_1()
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
    End Sub

    Private Sub trvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles trvClasificacion.TreeNodePopulate
        Dim obj As New Cls_Clasificacion
        Dim dt As DataTable = obj.NumeroNodo(Session("Ruta_Emp"), CInt(e.Node.Value))
        Dim dbRow As DataRow = dt.Rows(0)
        Dim nivelPrincipal As Integer = CInt(dbRow(1).ToString)
        Dim nodo As Integer = CInt(dbRow(0).ToString) + 1
        Dim nodoAyuda As Integer = CInt(dbRow(0).ToString)
        Dim codigo As Integer = CInt(e.Node.Value)
        If nodo = 2 Then
            dt = obj.NodosHijos1(Session("Ruta_Emp"), nivelPrincipal, nodo)
            obj.NodosPopulares(dt, e.Node.ChildNodes)
        Else
            dt = obj.NodosHijos(Session("Ruta_Emp"), nivelPrincipal, nodo, nodoAyuda, codigo)
            obj.NodosPopulares(dt, e.Node.ChildNodes)
        End If
    End Sub

    Protected Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim dbRow As DataRow
        trvClasificacion.SelectedNode.Selected = True

        If TituloPopupp.Text = "Busca Clasificaciones" Then
            TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
            Dim psNumero As Integer = InStr(1, TxtClasificacionBA.Value, "-")
            LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBusqueda').modal('show'); }).modal('hide');", True)
        Else
            txtClasificacion.Text = trvClasificacion.SelectedNode.Text
            lblCodClas.Text = trvClasificacion.SelectedValue
            dt = obj.Concat_Clasificacion(Session("Ruta_emp"), lblCodClas.Text)
            dbRow = dt.Rows(0)
            Dim c As Integer = Convert.ToInt64(dbRow(0))
            For index = 2 To c
                If index = 2 Then
                    Session("Clasificacion") = dbRow(index)
                Else
                    Session("Clasificacion") += " " + dbRow(index)
                End If
            Next
            Concatenar()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)
        End If
        trvClasificacion.Nodes.Clear()
    End Sub

    Protected Sub BtnCancelarArituclo_Click(sender As Object, e As EventArgs) Handles BtnCancelarArituclo.Click
        Limpiar_Cajas()
        Ocultar_Visible(False)
        Ocultar_Visible_Imagen(False)
        Session("nomImagen") = ""
        Session("Imagen") = ""
        btnTomarFoto.Visible = False
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub BtnCalcularVolumen_Click(sender As Object, e As EventArgs) Handles BtnCalcularVolumen.Click
        Dim Talto As String = txtAlto.Text.ToString
        Dim Tancho As String = txtAncho.Text.ToString
        Dim Tlargo As String = txtLargo.Text.ToString
        Try
            If Talto = "" Or Tancho = "" Or Tlargo = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Datos en *Alto*, *Ancho* y *Largo*');", True)
            Else
                Dim alto As Double = Convert.ToDouble(Talto)
                Dim ancho As Double = Convert.ToDouble(Tancho)
                Dim largo As Double = Convert.ToDouble(Tlargo)
                Dim volumen As Double = alto * largo * ancho
                txtVolumen.Text = volumen.ToString("0.##########")
            End If
        Catch ex As FormatException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Datos Numéricos en *Alto*, *Ancho* y *Largo*');", True)
        End Try
    End Sub


    '------------------ MODAL PRINCIPAL ------------------'
    Private Sub BtnBuscarClas_Click(sender As Object, e As EventArgs) Handles BtnBuscarClas.Click
        Listar_Nivel_1()
        TituloPopupp.Text = "Búsqueda de Clasificación"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('show');", True)
    End Sub

    Protected Sub BtnBuscarMar_Click(sender As Object, e As EventArgs) Handles BtnBuscarMar.Click
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Buscar_Marca(psconexion, 0, "%")
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
        TituloPopup.Text = "Búsqueda de Marcas"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub

    Protected Sub BtnBuscarMod_Click(sender As Object, e As EventArgs) Handles BtnBuscarMod.Click
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codMarca As Double = 0
        If lblCodMar.Text <> "" Then
            codMarca = Val(lblCodMar.Text)
        End If

        If codMarca.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Selecciona una Marca');", True)
        Else
            dt = obj.Buscar_Modelo(psconexion, 0, "%", codMarca)
            If dt.Rows.Count() = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Modelos de la Marca seleccionada');", True)
            Else
                TituloPopup.Text = "Búsqueda de Modelo"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
                GvBusqueda.DataSource = dt
                GvBusqueda.DataBind()
            End If
        End If
    End Sub

    Protected Sub BtnModeloDetalleMod_Click(sender As Object, e As EventArgs) Handles BtnModeloDetalleMod.Click
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codModelo As String = 0
        If lblCodMo.Text <> "" Then
            codModelo = Val(lblCodMo.Text)
        End If
        If codModelo.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Selecciona un Modelo');", True)
        Else
            dt = obj.Buscar_Modelo_Detalle(psconexion, 0, "%", codModelo)
            If dt.Rows.Count() = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Detalles del Modelo seleccionado');", True)
            Else
                TituloPopup.Text = "Búsqueda de Detalle del Modelo"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
                GvBusqueda.DataSource = dt
                GvBusqueda.DataBind()
            End If
        End If
    End Sub


    '------------------ MODAL BUSQUEDA ------------------'
    Protected Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        TituloPopupp.Text = "Busca Clasificaciones"
        Listar_Nivel_1()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    End Sub

    Protected Sub BtnBuscaMarcaBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarcaBA.Click
        TituloPopup.Text = "Busca Marcas"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub

    Protected Sub BtnBuscaModeloBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaModeloBA.Click
        TituloPopup.Text = "Busca Modelos"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub


    '------------------ MODAL BUSQUEDA ------------------'
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = BuscarCodigo.Value.ToString
        Dim codMarca As String = lblCodMar.Text.ToString
        Dim CodModelo As String = lblCodMo.Text.ToString
        Dim descripcion As String = BuscarDescripcion.Value.ToString

        If TituloPopup.Text = "Búsqueda de Marcas" Or TituloPopup.Text = "Busca Marcas" Then
            dt = obj.Buscar_Marca(psconexion, codigo, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda de Modelo" Or TituloPopup.Text = "Busca Modelos" Then
            If TituloPopup.Text = "Busca Modelos" Then
                codMarca = LblCodMarcaBA.Text.ToString
            End If
            If codMarca = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una Marca');", True)
            Else
                dt = obj.Buscar_Modelo(psconexion, codigo, descripcion, codMarca)
                If dt.Rows.Count() = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Modelos de la Marca seleccionada');", True)
                End If
            End If
        ElseIf TituloPopup.Text = "Búsqueda de Detalle del Modelo" Then
            If CodModelo = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Modelo');", True)
            Else
                dt = obj.Buscar_Modelo_Detalle(psconexion, codigo, descripcion, CodModelo)
                If dt.Rows.Count() = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Detalles del Modelo seleccionado');", True)
                End If
            End If
        End If
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    '------------------ MODAL gridview ------------------'
    Protected Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If TituloPopup.Text = "Búsqueda de Marcas" Or TituloPopup.Text = "Búsqueda de Modelo" Or TituloPopup.Text = "Búsqueda de Detalle del Modelo" Then
            If e.CommandName = "Aceptar" And TituloPopup.Text = "Búsqueda de Marcas" Then
                lblCodMar.Text = GvBusqueda.Rows(Index).Cells(3).Text
                txtMarca.Text = GvBusqueda.Rows(Index).Cells(2).Text
                lblCodMo.Text = ""
                txtModelo.Text = ""
                lblCodDetaMod.Text = ""
                txtDetalleModelo.Text = ""
            ElseIf e.CommandName = "Aceptar" And TituloPopup.Text = "Búsqueda de Modelo" Then
                lblCodMo.Text = GvBusqueda.Rows(Index).Cells(3).Text
                txtModelo.Text = GvBusqueda.Rows(Index).Cells(2).Text
                lblCodDetaMod.Text = ""
                txtDetalleModelo.Text = ""
            ElseIf e.CommandName = "Aceptar" And TituloPopup.Text = "Búsqueda de Detalle del Modelo" Then
                lblCodDetaMod.Text = GvBusqueda.Rows(Index).Cells(3).Text
                txtDetalleModelo.Text = GvBusqueda.Rows(Index).Cells(2).Text
            End If
            Concatenar()
        ElseIf TituloPopup.Text = "Busca Modelos" Or TituloPopup.Text = "Busca Marcas" Then
            If e.CommandName = "Aceptar" And TituloPopup.Text = "Busca Marcas" Then
                LblCodMarcaBA.Text = GvBusqueda.Rows(Index).Cells(3).Text
                TxtMarcaBA.Value = GvBusqueda.Rows(Index).Cells(2).Text
                LblCodModeloBA.Text = ""
                TxtModeloBA.Value = ""
            ElseIf e.CommandName = "Aceptar" And TituloPopup.Text = "Busca Modelos" Then
                LblCodModeloBA.Text = GvBusqueda.Rows(Index).Cells(3).Text
                TxtModeloBA.Value = GvBusqueda.Rows(Index).Cells(2).Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBusqueda').modal('show'); }).modal('hide');", True)
        End If
        Limpiar_Popup()
    End Sub

    Private Sub btnCancela_Click(sender As Object, e As EventArgs) Handles btnCancela.Click
        If TituloPopup.Text = "Búsqueda de Marcas" Or TituloPopup.Text = "Búsqueda de Modelo" Or TituloPopup.Text = "Búsqueda de Detalle del Modelo" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        ElseIf TituloPopup.Text = "Busca Modelos" Or TituloPopup.Text = "Busca Marcas" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBusqueda').modal('show'); }).modal('hide');", True)
        End If
        Limpiar_Popup()
    End Sub


    Protected Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click
        If TituloPopupp.Text = "Busca Clasificaciones" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)
        End If
    End Sub


    Private Sub BtnCerrarImagen_Click(sender As Object, e As EventArgs) Handles BtnCerrarImagen.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalImagen').modal('hide');", True)
    End Sub

    Private Sub DdlTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipo.SelectedIndexChanged
        If DdlTipo.SelectedItem.ToString = "SERVICIOS" Then
            DdlDetraccion.Visible = True
            lblDetraccion.Visible = True
            DdlTipoBien.Visible = True
            lblTipoBien.Visible = True
        Else
            DdlDetraccion.Visible = False
            lblDetraccion.Visible = False
            DdlTipoBien.Visible = False
            lblTipoBien.Visible = False
        End If
    End Sub

    Private Sub BtnSalirClas_Click(sender As Object, e As EventArgs) Handles BtnSalirClas.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBusqueda').modal('show'); }).modal('hide');", True)
    End Sub
End Class