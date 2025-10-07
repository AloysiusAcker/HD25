Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Menu_Contacto
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblTitulo.InnerText = Session("MenuNom")
            Title = Session("MenuNom")
            txtEmpresa.Text = ""
            txtPerContacto.Text = ""
            txtDireccion.Text = ""
            txtCodPostal.Text = ""
            txtTelefono.Text = ""
            txtEmail.Text = ""
            txtComentario.Text = ""
            Call LlenaComboItem("TBOPC006", cboPais)
            cboPais.SelectedValue = "51"
            cboPais_SelectedIndexChanged(sender, e)
        End If
    End Sub
    Private Sub Limpiar()
        txtEmpresa.Text = ""
        txtPerContacto.Text = ""
        txtDireccion.Text = ""
        txtCodPostal.Text = ""
        txtTelefono.Text = ""
        txtEmail.Text = ""
        txtComentario.Text = ""
        cboPais.Items.Clear()
        cboProv.Items.Clear()
        cboDpto.Items.Clear()
        cboDist.Items.Clear()
        Call LlenaComboItem("TBOPC006", cboPais)
        cboPais.SelectedValue = "51"
        Call LlenaComboItem("TBOPC002", cboDpto)
        cboDpto.Items.Add("(Seleccionar)") : cboDpto.SelectedValue = "(Seleccionar)"
        cboProv.Items.Add("(Seleccionar)") : cboProv.SelectedValue = "(Seleccionar)"
        cboDist.Items.Add("(Seleccionar)") : cboDist.SelectedValue = "(Seleccionar)"
    End Sub
    Protected Sub cboPais_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPais.SelectedIndexChanged
        If cboPais.SelectedValue = "51" Then
            Call LlenaComboItem("TBOPC002", cboDpto)
            cboDpto.Items.Add("(Seleccionar)") : cboDpto.SelectedValue = "(Seleccionar)"
        Else
            cboDpto.Items.Clear()
            cboDpto.Items.Add("(Seleccionar)") : cboDpto.SelectedValue = "(Seleccionar)"
        End If
        cboDpto_SelectedIndexChanged(sender, e)
    End Sub
    Protected Sub cboDpto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As New Listados
            cboProv.DataSource = obj.Listar_Provincia(cboDpto.SelectedValue.Trim)
            cboProv.DataTextField = "ELEMEN_VALOR"
            cboProv.DataValueField = "ELEMEN_CODIGO"
            cboProv.DataBind()
            cboProv.Items.Add("(Seleccionar)") : cboProv.SelectedValue = "(Seleccionar)"
            If cboDpto.Text.Trim = "(Seleccionar)" Then cboProv.Items.Clear() : cboProv.Items.Add("(Seleccionar)") : cboProv.SelectedValue = "(Seleccionar)"
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch Ex As Exception
            'lblError.Text = Ex.Message
        Finally
        End Try
        cboProv_SelectedIndexChanged(sender, e)
    End Sub
    Protected Sub cboProv_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Dim obj As New Listados
            cboDist.DataSource = obj.Listar_Distrito(cboDpto.SelectedValue.Trim, cboProv.SelectedValue.Trim)
            cboDist.DataTextField = "ELEMEN_VALOR"
            cboDist.DataValueField = "ELEMEN_CODIGO"
            cboDist.DataBind()
            cboDist.Items.Add("(Seleccionar)") : cboDist.SelectedValue = "(Seleccionar)"
            If cboProv.Text.Trim = "(Seleccionar)" Then cboDist.Items.Clear() : cboDist.Items.Add("(Seleccionar)") : cboDist.SelectedValue = "(Seleccionar)"
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch Ex As Exception
            'lblError.Text = Ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        lblError.Text = ""
        If txtEmpresa.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Empresa."
        If txtPerContacto.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Persona de Contacto."
        If txtDireccion.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Dirección."
        If txtCodPostal.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Código Postal."
        If txtTelefono.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Teléfono."
        If txtEmail.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar E-Mail."
        If txtComentario.Text.Trim = "" Then lblError.Text = lblError.Text & " <br> - Ingresar Comentario."
        If lblError.Text.Trim <> "" Then
            lblError.Text = "Existe las siguientes observaciones, favor de corregir:" & lblError.Text
            Exit Sub
        End If
        lblError.Text = ""
        Try
            Dim obj As New Insertar
            obj.Insertar_Comentario(Session("CodEmpresa"), txtEmpresa.Text.Trim, txtPerContacto.Text.Trim, txtDireccion.Text.Trim, txtCodPostal.Text.Trim, cboPais.SelectedValue.Trim, cboProv.SelectedValue.Trim, cboDist.SelectedValue.Trim, txtTelefono.Text.Trim, txtEmail.Text.Trim, txtComentario.Text.Trim, cboDpto.SelectedValue.Trim)
        Catch ex As SqlException
            'lblError.Text = ex.Message
        Catch Ex As Exception
            'lblError.Text = Ex.Message
        Finally
        End Try
        Call Limpiar()
        lblError.Text = "Los datos han sido enviados."
    End Sub
End Class
