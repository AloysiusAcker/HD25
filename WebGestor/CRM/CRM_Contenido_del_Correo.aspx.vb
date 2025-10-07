Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class CRM_Contenido_del_Correo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combo_Tipo_AGREGAR()
        End If
    End Sub
    Protected Sub Lista_Contenido_Correo()
        Dim obj As New Cls_Contenido_Correo
        Dim dt As New DataTable
        Dim Tipo As String = DdlTipoCorreo.SelectedValue.ToString
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Lista_Contenido_Correo(psconexion, Tipo)
        GvListaCorreo.DataSource = dt
        GvListaCorreo.DataBind()

        LblTotalCorreosL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalCorreos.Visible = True
        LblTotalCorreosL.Visible = True
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        System.Threading.Thread.Sleep(1000)
        Lista_Contenido_Correo()
    End Sub
    Private Sub chckTipoCorreo_CheckedChanged(sender As Object, e As EventArgs) Handles chckTipoCorreo.CheckedChanged
        If chckTipoCorreo.Checked = True Then
            DdlTipoCorreo.Enabled = True
            Llenar_Combo_Tipo()
        Else
            DdlTipoCorreo.Enabled = False
            DdlTipoCorreo.Items.Clear()
        End If
    End Sub
    Protected Sub Llenar_Combo_Tipo()
        Dim obj As New Cls_Contenido_Correo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Tipo(psconexion)
        DdlTipoCorreo.DataSource = dt
        DdlTipoCorreo.DataValueField = "ADMIN_TCORREO_CODIGO"
        DdlTipoCorreo.DataTextField = "TIPO"
        DdlTipoCorreo.DataBind()
        DdlTipoCorreo.Items.Add("< Seleccionar >")
        DdlTipoCorreo.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Llenar_Combo_Tipo_AGREGAR()
        Dim obj As New Cls_Contenido_Correo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Tipo(psconexion)
        DdlTipoCorreoAGREGAR.DataSource = dt
        DdlTipoCorreoAGREGAR.DataValueField = "ADMIN_TCORREO_CODIGO"
        DdlTipoCorreoAGREGAR.DataTextField = "TIPO"
        DdlTipoCorreoAGREGAR.DataBind()
        DdlTipoCorreoAGREGAR.Items.Add("< Seleccionar >")
        DdlTipoCorreoAGREGAR.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click
        LblTituloAgregarCorreo.Visible = True
        LblTipoCorreo.Visible = True
        DdlTipoCorreoAGREGAR.Visible = True
        DdlTipoCorreoAGREGAR.Enabled = True
        LblDescripcion.Visible = True
        TxtDescripcion.Visible = True
        LblAsunto.Visible = True
        TxtAsunto.Visible = True
        LblSaludo.Visible = True
        TxtSaludo.Visible = True
        LblCuerpo.Visible = True
        TxtCuerpo.Visible = True
        LblDespedida.Visible = True
        TxtDespedida.Visible = True
        LblFirma.Visible = True
        TxtFirma.Visible = True
        UploadImagen.Visible = True
        UploadFirmaImagen.Visible = True
        BtnGuardarCorreo.Visible = True
        BtnGuardarCorreo.Text = "Guardar"
        BtnCancelarCorreo.Visible = True
    End Sub

    Private Sub BtnCancelarCorreo_Click(sender As Object, e As EventArgs) Handles BtnCancelarCorreo.Click
        LblTituloAgregarCorreo.Visible = False
        LblTipoCorreo.Visible = False
        DdlTipoCorreoAGREGAR.Visible = False
        LblDescripcion.Visible = False
        TxtDescripcion.Visible = False
        LblAsunto.Visible = False
        TxtAsunto.Visible = False
        LblSaludo.Visible = False
        TxtSaludo.Visible = False
        LblCuerpo.Visible = False
        TxtCuerpo.Visible = False
        LblDespedida.Visible = False
        TxtDespedida.Visible = False
        LblFirma.Visible = False
        TxtFirma.Visible = False
        UploadImagen.Visible = False
        UploadFirmaImagen.Visible = False
        BtnGuardarCorreo.Visible = False
        BtnCancelarCorreo.Visible = False
        Limpiar_Cajas_Email()
    End Sub
    Protected Sub Limpiar_Cajas_Email()
        DdlTipoCorreoAGREGAR.SelectedValue = "< Seleccionar >"
        TxtDescripcion.Text = ""
        TxtAsunto.Value = ""
        TxtSaludo.Value = ""
        TxtCuerpo.Value = ""
        TxtDespedida.Value = ""
        TxtFirma.Value = ""
    End Sub
    Private Sub BtnAgregarTipo_Click(sender As Object, e As EventArgs) Handles BtnAgregarTipo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAgregarTipoCorreo').modal('show');", True)
        System.Threading.Thread.Sleep(1000)
        Lista_Tipo()
    End Sub

    Private Sub BtnCancelarTipo_Click(sender As Object, e As EventArgs) Handles BtnCancelarTipo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAgregarTipoCorreo').modal('hide');", True)
    End Sub
    Protected Sub Lista_Tipo()
        Dim obj As New Cls_Contenido_Correo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Tipo(psconexion)
        GvListaTipo.DataSource = dt
        GvListaTipo.DataBind()

        LblTotalTipoL.InnerHtml = " " + CStr(dt.Rows.Count())
        LblTotalTipo.Visible = True
        LblTotalTipoL.Visible = True
    End Sub

    Private Sub GvListaTipo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaTipo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Contenido_Correo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As String = GvListaTipo.Rows(Index).Cells(1).Text
        Dim Nombre As String = GvListaTipo.Rows(Index).Cells(2).Text
        Dim dt As New DataTable

        If e.CommandName = "Eliminar" Then
            dt = obj.Elimina_Cliente(psconexion, Codigo, Nombre)
            GvListaTipo.DataSource = dt
            GvListaTipo.DataBind()
        End If
        Lista_Tipo()
    End Sub
    Private Sub BtnGuardarTipo_Click(sender As Object, e As EventArgs) Handles BtnGuardarTipo.Click
        Dim obj As New Cls_Contenido_Correo
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Nombre As String = TxtTipo.Text.ToString
        Dim dt As DataTable
        If Nombre.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese un Tipo');", True)
        Else
            dt = obj.Insertar_Tipo(psconexion, Nombre)
            Dim dvRow As DataRow = dt.Rows(0)
            If dvRow(0) = "2" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ya existe el Tipo de Correo en la Tabla');", True)
            End If
            Lista_Tipo()
        End If
    End Sub
    Private Sub BtnGuardarCorreo_Click(sender As Object, e As EventArgs) Handles BtnGuardarCorreo.Click
        Dim obj As New Cls_Contenido_Correo
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Tipo As String = DdlTipoCorreoAGREGAR.SelectedValue.ToString
        Dim Nombre As String = TxtDescripcion.Text.ToString
        Dim Asunto As String = TxtAsunto.Value.ToString
        Dim Saludo As String = TxtSaludo.Value.ToString
        Dim Cuerpo As String = TxtCuerpo.Value.ToString
        Dim NombreImagen As String = UploadImagen.FileName.ToString
        Dim Despedida As String = TxtDespedida.Value.ToString
        Dim Firma As String = TxtFirma.Value.ToString
        Dim NombreFirmaImagen As String = UploadFirmaImagen.FileName.ToString
        Dim Correlativo As String = LblCodTIPO.Text.ToString
        Dim dt As DataTable

        If Tipo.Equals("< Seleccionar >") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Seleccionar un Tipo');", True)
        ElseIf Nombre.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Descripción es un campo Obligatorio');", True)
        ElseIf Asunto.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Asunto es un campo Obligatorio');", True)
        ElseIf Saludo.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Saludo es un campo Obligatorio');", True)
        ElseIf Cuerpo.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Cuerpo es un campo Obligatorio');", True)
        ElseIf Despedida.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Despedida es un campo Obligatorio');", True)
        ElseIf Firma.Equals("") Then
            ScriptManager.RegisterStartupScript(Me, Page.GetType, "", "alert('Firma es un campo Obligatorio');", True)
        Else
            If UploadFirmaImagen.HasFile = Nothing And UploadImagen.HasFile = Nothing And BtnGuardarCorreo.Text = "Actualizar" Then
                TituloPregunta.Text = "¿Desea actualizar el correo sin imagen?"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
            ElseIf UploadFirmaImagen.HasFile = Nothing And UploadImagen.HasFile = Nothing And BtnGuardarCorreo.Text = "Guardar" Then
                TituloPregunta.Text = "¿Desea ingresar el correo sin imagen?"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
            ElseIf UploadImagen.HasFile = Nothing And BtnGuardarCorreo.Text = "Actualizar" Then
                TituloPregunta.Text = "¿Desea actualizar el correo sin imagen?"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
            ElseIf UploadFirmaImagen.HasFile = Nothing And BtnGuardarCorreo.Text = "Actualizar" Then
                TituloPregunta.Text = "¿Desea actualizar el correo sin imagen?"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
            ElseIf UploadImagen.HasFile = Nothing And BtnGuardarCorreo.Text = "Guardar" Then
                TituloPregunta.Text = "¿Desea ingresar el correo sin imagen?"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
            ElseIf UploadFirmaImagen.HasFile = Nothing And BtnGuardarCorreo.Text = "Guardar" Then
                TituloPregunta.Text = "¿Desea ingresar el correo sin imagen?"
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
            Else
                If UploadImagen.HasFile And UploadFirmaImagen.HasFile Then
                    Using reader As New BinaryReader(UploadImagen.PostedFile.InputStream)
                        Using readerF As New BinaryReader(UploadFirmaImagen.PostedFile.InputStream)
                            Dim Imagen As Byte() = reader.ReadBytes(UploadImagen.PostedFile.ContentLength)
                            Dim FirmaImagen As Byte() = readerF.ReadBytes(UploadFirmaImagen.PostedFile.ContentLength)

                            Dim viImagen = System.Drawing.Image.FromStream(UploadImagen.PostedFile.InputStream)
                            Dim viImagenF = System.Drawing.Image.FromStream(UploadFirmaImagen.PostedFile.InputStream)
                            Dim vnAncho = viImagen.PhysicalDimension.Width And viImagenF.PhysicalDimension.Width
                            Dim vnAlto = viImagen.PhysicalDimension.Height And viImagenF.PhysicalDimension.Height
                            If (vnAncho < 800 OrElse vnAlto < 600) Then
                                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe seleccionar una imagen mayor a 800 x 600');", True)
                            Else
                                If BtnGuardarCorreo.Text = "Guardar" Then
                                    dt = obj.Insertar_Email(psconexion, Tipo, Nombre, Asunto, Saludo, Cuerpo, NombreImagen, Despedida, Firma, NombreFirmaImagen, Imagen, FirmaImagen)
                                ElseIf BtnGuardarCorreo.Text = "Actualizar" Then
                                    dt = obj.Actualizar_Email(psconexion, Tipo, Nombre, Asunto, Saludo, Cuerpo, NombreImagen, Despedida, Firma, NombreFirmaImagen, Imagen, FirmaImagen, Correlativo)
                                End If
                                Lista_Contenido_Correo()
                            End If
                        End Using
                    End Using
                End If
            End If
        End If

    End Sub

    Private Sub BtnSi_Click(sender As Object, e As EventArgs) Handles BtnSi.Click
        Dim obj As New Cls_Contenido_Correo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Tipo As String = DdlTipoCorreoAGREGAR.SelectedValue.ToString
        Dim Nombre As String = TxtDescripcion.Text.ToString
        Dim Asunto As String = TxtAsunto.Value.ToString
        Dim Saludo As String = TxtSaludo.Value.ToString
        Dim Cuerpo As String = TxtCuerpo.Value.ToString
        Dim NombreImagen As String = UploadImagen.FileName.ToString
        Dim Despedida As String = TxtDespedida.Value.ToString
        Dim Firma As String = TxtFirma.Value.ToString
        Dim NombreFirmaImagen As String = UploadFirmaImagen.FileName.ToString
        Dim Correlativo As String = LblCodTIPO.Text.ToString

        Using reader As New BinaryReader(UploadImagen.PostedFile.InputStream)
            Using readerF As New BinaryReader(UploadFirmaImagen.PostedFile.InputStream)
                Dim Imagen As Byte() = reader.ReadBytes(UploadImagen.PostedFile.ContentLength)
                Dim FirmaImagen As Byte() = reader.ReadBytes(UploadFirmaImagen.PostedFile.ContentLength)

                If TituloPregunta.Text = "¿Desea ingresar el correo sin imagen?" Then
                    obj.Insertar_Email(psconexion, Tipo, Nombre, Asunto, Saludo, Cuerpo, NombreImagen, Despedida, Firma, NombreFirmaImagen, Imagen, FirmaImagen)
                ElseIf TituloPregunta.Text = "¿Desea actualizar el correo sin imagen?" Then
                    obj.Actualizar_Email(psconexion, Tipo, Nombre, Asunto, Saludo, Cuerpo, NombreImagen, Despedida, Firma, NombreFirmaImagen, Imagen, FirmaImagen, Correlativo)
                End If
            End Using
        End Using
        Lista_Contenido_Correo()
    End Sub

    Private Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('hide');", True)
    End Sub

    Private Sub GvListaCorreo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaCorreo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Contenido_Correo
        Dim psconexion As String = Session("Ruta_Emp")
        Dim dt As New DataTable

        If e.CommandName = "Editar" Then
            LblTituloAgregarCorreo.Visible = True
            LblTipoCorreo.Visible = True
            DdlTipoCorreoAGREGAR.Visible = True
            DdlTipoCorreoAGREGAR.Enabled = False
            LblDescripcion.Visible = True
            TxtDescripcion.Visible = True
            LblAsunto.Visible = True
            TxtAsunto.Visible = True
            LblSaludo.Visible = True
            TxtSaludo.Visible = True
            LblCuerpo.Visible = True
            TxtCuerpo.Visible = True
            LblDespedida.Visible = True
            TxtDespedida.Visible = True
            LblFirma.Visible = True
            TxtFirma.Visible = True
            UploadImagen.Visible = True
            UploadFirmaImagen.Visible = True
            BtnGuardarCorreo.Visible = True
            BtnGuardarCorreo.Text = "Actualizar"
            BtnCancelarCorreo.Visible = True
            ''
            LblCodTIPO.Text = GvListaCorreo.Rows(Index).Cells(1).Text
            DdlTipoCorreoAGREGAR.SelectedValue = GvListaCorreo.Rows(Index).Cells(2).Text
            TxtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCorreo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtAsunto.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCorreo.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtSaludo.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCorreo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtCuerpo.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCorreo.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtDespedida.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCorreo.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtFirma.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaCorreo.Rows(Index).Cells(8).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        End If
    End Sub
    Private Sub BtnCerrarImagen_Click(sender As Object, e As EventArgs) Handles BtnCerrarImagen.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalImagen').modal('hide');", True)
    End Sub
End Class