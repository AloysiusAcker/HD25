Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class MenuWeb_MenuWeb_Registra_Elemento
    Inherits System.Web.UI.Page
    Dim obj As New clsMenuWeb_Consultas
    Dim clFuncion As New clsMenuWeb_Funciones
    Dim clProceso As New clsGeneral_Proceso
    Private Sub Llenar_GrupoEmpresa(ByVal cboG As DropDownList, ByVal cboE As DropDownList)
        cboG.Items.Clear() : cboE.Items.Clear()
        Call clProceso.Llena_GrupoEmpresa(cboG, HttpContext.Current.User.Identity.Name)
        cboG.Items.Add("< Seleccionar >") : cboG.SelectedValue = "< Seleccionar >"
        cboE.Items.Add("< Seleccionar >") : cboE.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            Call Llenar_GrupoEmpresa(cboGrupo, cboEmpresa)
            cboGrupo_SelectedIndexChanged(sender, e)
            Session("Ingreso") = "Si"
        End If
    End Sub
    Protected Sub cboGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
        Try
            If cboGrupo.SelectedValue <> "< Seleccionar >" Then
                Dim pdCodGrupo As Double = 0
                pdCodGrupo = cboGrupo.SelectedValue.Trim
                clProceso.Llena_Empresa(HttpContext.Current.User.Identity.Name, pdCodGrupo, cboEmpresa)
            Else
                cboEmpresa.Items.Clear() : cboItem.Items.Clear() : cboCategoria.Items.Clear()
                cboEmpresa.Items.Add("< Seleccionar >") : cboEmpresa.SelectedValue = "< Seleccionar >"
                cboItem.Items.Add("< Seleccionar >") : cboItem.SelectedValue = "< Seleccionar >"
                cboCategoria.Items.Add("< Seleccionar >") : cboCategoria.SelectedValue = "< Seleccionar >"
            End If
            cboEmpresa_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboEmpresa_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboEmpresa.SelectedIndexChanged
        Try
            If cboGrupo.SelectedValue <> "< Seleccionar >" And cboEmpresa.SelectedValue <> "< Seleccionar >" Then
                Dim pdCodGrupo As Double = 0
                pdCodGrupo = cboGrupo.SelectedValue.Trim
                Call clFuncion.Llena_ItemsMenu(cboItem, pdCodGrupo, cboEmpresa.SelectedValue.Trim)
            Else
                cboItem.Items.Add("< Seleccionar >") : cboItem.SelectedValue = "< Seleccionar >"
            End If
            cboItem_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboItem.SelectedIndexChanged
        Dim pdCodGrupo As Double = 0
        Dim CodItem As Double = 0
        Dim dt As DataTable
        Dim Nombre As String = ""
        Dim Etiqueta As String = ""
        Dim Obli As String = ""
        Try
            If cboItem.SelectedValue <> "< Seleccionar >" Then
                CodItem = cboItem.SelectedValue.Trim
                pdCodGrupo = cboGrupo.SelectedValue.Trim
                Call clFuncion.Llena_Categoria_xItem(cboCategoria, pdCodGrupo, cboEmpresa.SelectedValue.Trim, CodItem)
                If cboCategoria.Items.Count > 1 Then
                    cboCategoria.Enabled = True
                Else
                    cboCategoria.Enabled = False
                End If
                Call Limpiar_Campos()
                dt = obj.Lista_Campos_xItems(CodItem)
                For Each dr As DataRow In dt.Rows
                    Nombre = "" : Etiqueta = ""
                    Nombre = Nu(dr("CAMPO_NOMBRE"))
                    Etiqueta = Nu(dr("CAMPO_ETIQUETA"))
                    If Nu(dr("CAMPO_OBLIGATORIO")) = "S" Then Obli = " *" Else Obli = ""
                    If Nombre = "ELEMENTO_CATEGORIA" Then lblCategoria.Text = Etiqueta & Obli
                    If Nombre = "ELEMENTO_NOMBRE" Then lblNombre.Text = Etiqueta & Obli : lblNombre.Visible = True : txtNombre.Visible = True : txtNombre.Text = ""
                    If Nombre = "ELEMENTO_NOMBRE2" Then lblNombreHtml.Text = Etiqueta & Obli : lblNombreHtml.Visible = True : txtNombreHtml.Visible = True : txtNombreHtml.Text = ""
                    If Nombre = "ELEMENTO_DESCRIP_CORTA" Then lblDescripcion.Text = Etiqueta & Obli : lblDescripcion.Visible = True : txtDescripcion.Visible = True : txtDescripcion.Text = ""
                    If Nombre = "ELEMENTO_DESCRIP_LARGA" Then lblDetalle.Text = Etiqueta & Obli : lblDetalle.Visible = True : txtDetalle.Visible = True : txtDetalle.Text = ""
                    If Nombre = "ELEMENTO_LINK1" Then lblWeb1.Text = Etiqueta & Obli : lblWeb1.Visible = True : txtPagina1.Visible = True : txtPagina1.Text = ""
                    If Nombre = "ELEMENTO_LINK2" Then lblWeb2.Text = Etiqueta & Obli : lblWeb2.Visible = True : txtPagina2.Visible = True : txtPagina2.Text = ""
                    If Nombre = "ELEMENTO_IMAGEN" Then lblImagen.Text = Etiqueta & Obli
                    If Nombre = "ELEMENTO_FECHA1" Then lblFecha1.Text = Etiqueta & Obli : lblFecha1.Visible = True : txtFecha1.Visible = True : txtFecha1.Text = "" : lblFormato1.Visible = True
                    If Nombre = "ELEMENTO_FECHA2" Then lblFecha2.Text = Etiqueta & Obli : lblFecha2.Visible = True : txtFecha2.Visible = True : txtFecha2.Text = "" : lblFormato2.Visible = True
                    If Nombre = "ELEMENTO_FECHA3" Then lblFecha3.Text = Etiqueta & Obli : lblFecha3.Visible = True : txtFecha3.Visible = True : txtFecha3.Text = "" : lblFormato2.Visible = True
                    If Nombre = "ELEMENTO_COMPLETAR1" Then lblCom1.Text = Etiqueta & Obli : lblCom1.Visible = True : txtCom1.Visible = True : txtCom1.Text = ""
                    If Nombre = "ELEMENTO_COMPLETAR2" Then lblCom2.Text = Etiqueta & Obli : lblCom2.Visible = True : txtCom2.Visible = True : txtCom2.Text = ""
                    If Nombre = "ELEMENTO_COMPLETAR3" Then lblCom3.Text = Etiqueta & Obli : lblCom3.Visible = True : txtCom3.Visible = True : txtCom3.Text = ""
                    If Nombre = "ELEMENTO_COMPLETAR4" Then lblCom4.Text = Etiqueta & Obli : lblCom4.Visible = True : txtCom4.Visible = True : txtCom4.Text = ""
                    If Nombre = "ELEMENTO_COMPLETAR5" Then lblCom5.Text = Etiqueta & Obli : lblCom5.Visible = True : txtCom5.Visible = True : txtCom5.Text = ""
                    If Nombre = "ELEMENTO_ARCHIVO_NOMBRE" Then lblArchivo.Text = Etiqueta & Obli
                Next
            Else
                cboCategoria.Enabled = False
                cboCategoria.Items.Clear()
                cboCategoria.Items.Add("< Seleccionar >") : cboCategoria.SelectedValue = "< Seleccionar >"
            End If
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Private Sub Limpiar_Campos()
        txtNombre.Text = "" : lblNombre.Visible = False : txtNombre.Visible = False
        txtNombreHtml.Text = "" : lblNombreHtml.Visible = False : txtNombreHtml.Visible = False
        txtDescripcion.Text = "" : lblDescripcion.Visible = False : txtDescripcion.Visible = True
        txtDetalle.Text = "" : lblDetalle.Visible = False : txtDetalle.Visible = False
        txtPagina1.Text = "" : lblWeb1.Visible = False : txtPagina1.Visible = False
        txtPagina2.Text = "" : lblWeb2.Visible = False : txtPagina2.Visible = False
        txtFecha1.Text = "" : lblFecha1.Visible = False : txtFecha1.Visible = False : lblFormato1.Visible = False
        txtFecha2.Text = "" : lblFecha2.Visible = False : txtFecha2.Visible = False : lblFormato2.Visible = False
        txtFecha3.Text = "" : lblFecha3.Visible = False : txtFecha3.Visible = False : lblFormato3.Visible = False
        txtCom1.Text = "" : lblCom1.Visible = False : txtCom1.Visible = False
        txtCom2.Text = "" : lblCom2.Visible = False : txtCom2.Visible = False
        txtCom3.Text = "" : lblCom3.Visible = False : txtCom3.Visible = False
        txtCom4.Text = "" : lblCom4.Visible = False : txtCom4.Visible = False
        txtCom5.Text = "" : lblCom5.Visible = False : txtCom5.Visible = False
    End Sub
    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        Response.Redirect("MenuWeb_Registra_Elemento.aspx")
    End Sub
    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect("MenuWeb_Elementos.aspx")
    End Sub
End Class
