Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class MenuWeb_MenuWeb_Elementos
    Inherits System.Web.UI.Page
    Dim obj As New clsMenuWeb_Consultas
    Dim clFuncion As New clsMenuWeb_Funciones
    Dim clProceso As New clsGeneral_Proceso
    Dim objSeg As New ModuloSeguridad
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
                Flex.DataSource = Nothing
                Flex.DataBind()
            End If
            cboEmpresa_SelectedIndexChanged(sender, e)
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        lblError.Text = ""
        If cboGrupo.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Seleccionar Grupo."
        If cboEmpresa.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Empresa."
        If cboItem.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Item."
        If lblError.Text <> "" Then
            lblError.Text = "Se han encontrado las sgtes. observaciones:" & lblError.Text
            Exit Sub
        End If
        Try
            flex.DataSource = Nothing
            flex.DataBind()
            flex.Columns.Clear()
            Call Crear_Columnas()
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Private Sub Crear_Columnas()
        Dim dt As DataTable
        Dim pdCodItem As Double = 0
        Dim pdCodGrupo As Double = 0
        Dim pdCodCategoria As Double = 0
        Dim ColumnBound1 As New BoundField
        Dim ColumnBound2 As New BoundField
        Dim ColumnBound3 As New BoundField
        Dim ColumnBound4 As New BoundField
        Dim Boton As New ButtonField
        Dim anchoFlex As Long = 0
        Dim Sigla As String = ""
        pdCodGrupo = cboGrupo.SelectedValue.Trim
        pdCodItem = cboItem.SelectedValue.Trim
        'OBTENER SIGLA
        dt = objSeg.Obtener_Sigla(pdCodGrupo)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Sigla = Nu(dr("GE_CODIGO")) & Nu(dr("GE_PREFIJO"))
            Next
        End If
        dt = Nothing
        dt = obj.Lista_Campos_xItems(pdCodItem)
        'crear boton
        Boton.ButtonType = ButtonType.Button
        Boton.Text = "Quitar"
        Boton.CommandName = "Quitar"
        Boton.ControlStyle.CssClass = "EstiloBoton_Ac"
        Boton.ControlStyle.Width = 50
        Boton.ItemStyle.Width = 50
        Boton.ItemStyle.HorizontalAlign = HorizontalAlign.Left
        Boton.ItemStyle.VerticalAlign = VerticalAlign.Top
        Flex.Columns.Add(Boton)
        anchoFlex = anchoFlex + 50
        '1primera columna
        ColumnBound1.DataField = "ELEMENTO_CODIGO"
        ColumnBound1.HeaderText = ""
        ColumnBound1.ItemStyle.Width = 0
        ColumnBound1.ItemStyle.ForeColor = Drawing.Color.White
        ColumnBound1.ItemStyle.HorizontalAlign = HorizontalAlign.Left
        ColumnBound1.ItemStyle.VerticalAlign = VerticalAlign.Top
        Flex.Columns.Add(ColumnBound1)
        '2segunda columna
        ColumnBound3.DataField = "ELEMENTO_IMAGEN_NOMBRE"
        ColumnBound3.HeaderText = ""
        ColumnBound3.ItemStyle.Width = 0
        ColumnBound3.ItemStyle.ForeColor = Drawing.Color.White
        ColumnBound3.ItemStyle.HorizontalAlign = HorizontalAlign.Left
        ColumnBound3.ItemStyle.VerticalAlign = VerticalAlign.Top
        Flex.Columns.Add(ColumnBound3)
        '3tercera columna
        ColumnBound4.DataField = "ELEMENTO_ARCHIVO_NOMBRE"
        ColumnBound4.HeaderText = ""
        ColumnBound4.ItemStyle.HorizontalAlign = HorizontalAlign.Left
        ColumnBound4.ItemStyle.VerticalAlign = VerticalAlign.Top
        ColumnBound4.ItemStyle.Width = 0
        Flex.Columns.Add(ColumnBound4)
        Dim imgS As String = "No"
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                If Nu(dr("CAMPO_NOMBRE")) = "ELEMENTO_IMAGEN_NOMBRE" Or Nu(dr("CAMPO_NOMBRE")) = "ELEMENTO_IMAGEN" Then
                    If imgS = "No" Then
                        Dim Img As New ImageField
                        Img.DataImageUrlField = "ELEMENTO_IMAGEN_NOMBRE"
                        Img.HeaderText = "Imagen"
                        Img.DataImageUrlFormatString = "~/MenuWeb/Imagenes_" & Sigla & "/{0}"
                        Img.ControlStyle.Width = 100
                        Img.ControlStyle.Height = 50
                        Img.ItemStyle.Height = 50
                        Img.ItemStyle.Width = 100
                        anchoFlex = anchoFlex + 100
                        Img.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                        Img.ItemStyle.VerticalAlign = VerticalAlign.Top
                        Flex.Columns.Add(Img)
                        imgS = "Si"
                    End If
                    'ElseIf Nu(dr("CAMPO_NOMBRE")) = "ELEMENTO_IMAGEN" Then
                    '
                Else
                    Dim ColumnBound As New BoundField
                    ColumnBound.HeaderText = Nu(dr("CAMPO_ETIQUETA"))
                    ColumnBound.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                    ColumnBound.ItemStyle.VerticalAlign = VerticalAlign.Top
                    If Nu(dr("CAMPO_NOMBRE")) = "ELEMENTO_CATEGORIA" Then
                        ColumnBound.DataField = "CATEG"
                        ColumnBound.ItemStyle.Width = 70
                        anchoFlex = anchoFlex + 70
                    Else
                        ColumnBound.DataField = Nu(dr("CAMPO_NOMBRE"))
                        ColumnBound.ItemStyle.Width = 150
                        anchoFlex = anchoFlex + 150
                    End If
                    Flex.Columns.Add(ColumnBound)
                End If
            Next
        End If
        'ultima columna
        ColumnBound2.DataField = "COMENTARIO"
        ColumnBound2.HeaderText = "Permite Completar?"
        ColumnBound2.ItemStyle.HorizontalAlign = HorizontalAlign.Left
        ColumnBound2.ItemStyle.VerticalAlign = VerticalAlign.Top
        ColumnBound2.ItemStyle.Width = 100
        anchoFlex = anchoFlex + 100
        Flex.Columns.Add(ColumnBound2)
        If cboCategoria.SelectedValue <> "< Seleccionar >" Then pdCodCategoria = cboCategoria.SelectedValue.Trim
        Flex.DataSource = obj.Lista_ElementosMenuItems(pdCodGrupo, cboEmpresa.SelectedValue.Trim, pdCodItem, pdCodCategoria)
        Flex.DataBind()
        Flex.Width = anchoFlex
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
        Flex.DataSource = Nothing
        Flex.DataBind()
        Flex.Columns.Clear()
        If cboItem.SelectedValue <> "< Seleccionar >" Then
            Dim pdCodGrupo As Double = 0
            Dim CodItem As Double = 0
            CodItem = cboItem.SelectedValue.Trim
            pdCodGrupo = cboGrupo.SelectedValue.Trim
            Call clFuncion.Llena_Categoria_xItem(cboCategoria, pdCodGrupo, cboEmpresa.SelectedValue.Trim, CodItem)
            If cboCategoria.Items.Count > 1 Then
                cboCategoria.Enabled = True
            Else
                cboCategoria.Enabled = False
            End If
        Else
            cboCategoria.Enabled = False
            cboCategoria.Items.Clear()
            cboCategoria.Items.Add("< Seleccionar >") : cboCategoria.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Quitar" Then
            Try
                Dim fso = CreateObject("scripting.filesystemobject")
                Dim NombreImagen As String = ""
                Dim NombreArchivo As String = ""
                NombreImagen = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                NombreArchivo = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                Dim FileToDelete As String
                FileToDelete = Server.MapPath("Imagenes_" & Session("SiglaGrupoEmpresa") & "/" & NombreImagen)
                If System.IO.File.Exists(FileToDelete) = True Then
                    System.IO.File.Delete(FileToDelete)
                End If
                FileToDelete = Server.MapPath("Archivos_" & Session("SiglaGrupoEmpresa") & "/" & NombreImagen)
                If System.IO.File.Exists(FileToDelete) = True Then
                    System.IO.File.Delete(FileToDelete)
                End If
                Dim pdCodigo As Double = 0
                Dim pdGrupo As Double = 0
                pdGrupo = cboGrupo.SelectedValue.Trim
                pdCodigo = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                obj.Del_Elemento_Menu(pdGrupo, cboEmpresa.SelectedValue.Trim, pdCodigo)
                btnListar_Click(sender, e)
            Catch ex As SqlException
                lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
            Catch ex As Exception
                lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
            End Try
        End If
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Response.Redirect("MenuWeb_Registra_Elemento.aspx")
    End Sub
End Class
