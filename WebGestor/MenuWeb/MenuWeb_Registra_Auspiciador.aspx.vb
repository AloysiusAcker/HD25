Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class MenuWeb_MenuWeb_Registra_Auspiciador
    Inherits System.Web.UI.Page
    Dim obj As New clsMenuWeb_Consultas
    Dim clFuncion As New clsMenuWeb_Funciones
    Dim clProceso As New clsGeneral_Proceso
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            Call Llenar_GrupoEmpresa(cboGrupo, cboEmpresa)
        End If
    End Sub
    Private Sub Llenar_GrupoEmpresa(ByVal cboG As DropDownList, ByVal cboE As DropDownList)
        cboG.Items.Clear() : cboE.Items.Clear()
        Call clProceso.Llena_GrupoEmpresa(cboG, HttpContext.Current.User.Identity.Name)
        cboG.Items.Add("< Seleccionar >") : cboG.SelectedValue = "< Seleccionar >"
        cboE.Items.Add("< Seleccionar >") : cboE.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub cboGrupo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboGrupo.SelectedIndexChanged
        Try
            If cboGrupo.SelectedValue <> "< Seleccionar >" Then
                Dim pdCodGrupo As Double = 0
                pdCodGrupo = cboGrupo.SelectedValue.Trim
                clProceso.Llena_Empresa(HttpContext.Current.User.Identity.Name, pdCodGrupo, cboEmpresa)
            End If
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Private Sub Listar_Auspiciador()
        lblError.Text = ""
        Try
            If cboGrupo.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Seleccionar Grupo."
            If cboEmpresa.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Empresa."
            If lblError.Text <> "" Then
                lblError.Text = "Se han encontrado las sgtes. observaciones:" & lblError.Text
                Exit Sub
            End If
            Dim pdGrupo As Double = 0
            pdGrupo = cboGrupo.SelectedValue.Trim
            Flex.DataSource = obj.Lista_Auspiciador(pdGrupo, cboEmpresa.SelectedValue.Trim)
            Flex.DataBind()
            If Flex.Rows.Count > 0 Then
                lblRegistro.Text = "Se han encontrado " & Flex.Rows.Count & " registros"
            Else
                lblRegistro.Text = "No hay Registros"
            End If
        Catch ex As SqlException
            lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Listar_Auspiciador()
    End Sub
    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        lblError.Text = ""
        If cboGrupo.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Seleccionar Grupo."
        If cboEmpresa.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Seleccionar Empresa."
        If lblError.Text <> "" Then
            lblError.Text = "Se han encontrado las sgtes. observaciones:" & lblError.Text
            Exit Sub
        End If
        lblIngreso.Visible = True
        lblIngEtiqueta.Text = "Nuevo Auspiciador"
        Call Limpiar()
    End Sub
    Private Sub Limpiar()
        txtNombre.Text = ""
        txtDescripcion.Text = ""
        txtPagina.Text = ""
        Session("Ingreso") = "Si"
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Call Limpiar()
        lblIngreso.Visible = False
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Eliminar" Then
            Try
                Dim fso = CreateObject("scripting.filesystemobject")
                Dim NombreImagen As String = ""
                NombreImagen = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                Dim FileToDelete As String
                FileToDelete = Server.MapPath("ImagesAusp_" & Session("SiglaGrupoEmpresa") & "/" & NombreImagen)
                If System.IO.File.Exists(FileToDelete) = True Then
                    System.IO.File.Delete(FileToDelete)
                End If
                Dim pdCodigo As Double = 0
                Dim pdGrupo As Double = 0
                pdGrupo = cboGrupo.SelectedValue.Trim
                pdCodigo = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                obj.Del_Auspiciador(pdGrupo, cboEmpresa.SelectedValue.Trim, pdCodigo)
                btnListar_Click(sender, e)
            Catch ex As SqlException
                lblError.Text = "Se ha encontrado un error en la base de datos: " & ex.Message
            Catch ex As Exception
                lblError.Text = "Se ha encontrado un error en la aplicación: " & ex.Message
            End Try
        End If
    End Sub
End Class
