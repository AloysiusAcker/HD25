Imports System.Data.SqlClient
Imports System.Data
Imports WebGEstor
Partial Class General_Relacion_Categoria_Usuario
    Inherits System.Web.UI.Page
    Dim psConexion As String = ConfigurationManager.AppSettings("cnTecnicos")
    Dim psCodEmpresa As String = ConfigurationManager.AppSettings("CodEmpresa")
    Dim psEmpSeg As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp_Seg") 'cnTecnicosGrEmp
    Dim psGrpEmp As String = ConfigurationManager.AppSettings("cnTecnicosGrEmp")
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New ModuloGeneral
        Try
            lblError.Text = ""
            Flex.DataSource = obj.Lista_UsuarioxCategoria(Session("Ruta_Emp"), Session("CodEmpresa"))
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & Flex.Rows.Count & " registros."
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            Call btnListar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnAsignar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAsignar.Click
        Try
            lblError.Text = ""
            tbAsignar.Visible = True
            cboCategoria.Items.Clear()
            Call LlenaComboItem("TBOPC413", cboCategoria)
            cboCategoria.Items.Add("< Seleccionar >") : cboCategoria.SelectedValue = "< Seleccionar >"
            FlexUsuario.DataSource = Nothing
            FlexUsuario.DataBind()
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Private Sub Marcar_Usuario()
        Try
            lblError.Text = ""
            Dim Check As CheckBox
            Dim obj As New ModuloGeneral
            Dim dt As DataTable
            Dim i As Integer = 0
            dt = obj.Lista_UsuarioxCategoria_Marcar(Session("Ruta_Emp"), Session("CodEmpresa"), cboCategoria.SelectedValue.Trim)
            For Each dr As Data.DataRow In dt.Rows
                For i = 0 To FlexUsuario.Rows.Count - 1
                    If FlexUsuario.Rows(i).Cells(1).Text = dr("USUARI_CODIGO").ToString Then
                        Check = CType(FlexUsuario.Rows(i).Cells(0).FindControl("chkUsuario"), CheckBox)
                        Check.Checked = True
                        Check.Enabled = False
                    End If
                Next
            Next
            dt = Nothing
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub cboCategoria_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCategoria.SelectedIndexChanged
        Try
            lblError.Text = ""
            If cboCategoria.SelectedValue.Trim <> "< Seleccionar >" Then
                Dim obj As New ModuloSeguridad
                FlexUsuario.DataSource = obj.Listar_Usuarios_SinAdm(Ruta_Ng)
                FlexUsuario.DataBind()
                Call Marcar_Usuario()
            Else
                FlexUsuario.DataSource = Nothing
                FlexUsuario.DataBind()
            End If
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        tbAsignar.Visible = False
        Call btnListar_Click(sender, e)
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblError.Text = ""
            Dim dt As DataTable
            Dim obj As New ModuloGeneral
            Dim Usuario As CheckBox
            Dim i As Integer = 0
            Dim a As Integer = 0
            For i = 0 To FlexUsuario.Rows.Count - 1
                Usuario = FlexUsuario.Rows(i).Cells(0).FindControl("chkUsuario")
                If Usuario.Checked = True And Usuario.Enabled = True Then a = 1 : Exit For
            Next
            If cboCategoria.SelectedValue.Trim = "< Seleccionar >" Then lblError.Text = "<br> - Seleccionar categoria."
            If a = 0 Then lblError.Text = lblError.Text & "<br> - Debe de marcar al menos un Usuario."
            If lblError.Text <> "" Then
                Exit Sub
            End If
            For i = 0 To FlexUsuario.Rows.Count - 1
                Usuario = FlexUsuario.Rows(i).Cells(0).FindControl("chkUsuario")
                dt = obj.Existe_UsuarioxCategoria(Session("Ruta_Emp"), Session("CodEmpresa"), cboCategoria.SelectedValue.Trim, FlexUsuario.Rows(i).Cells(1).Text)
                If dt.Rows.Count = 0 Then
                    If Usuario.Checked = True And Usuario.Enabled = True Then
                        obj.Insertar_Usuario_Categoria(Session("Ruta_Emp"), Session("CodEmpresa"), cboCategoria.SelectedValue.Trim, FlexUsuario.Rows(i).Cells(1).Text)
                    End If
                End If
                dt = Nothing
            Next
            Call btnCancelar_Click(sender, e)
        Catch ex As SqlException
            lblError.Text = "Ha ocurrido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Ha ocurrido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        If e.CommandName = "Quitar" Then
            Try
                Dim obj As New ModuloGeneral
                obj.Delete_Usuario_Categoria(Session("Ruta_Emp"), Session("CodEmpresa"), Flex.Rows(Index).Cells(1).Text, Flex.Rows(Index).Cells(3).Text)
            Catch Ex As SqlException
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                lblError.Visible = True
                lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            btnListar_Click(sender, e)
        End If
    End Sub
End Class
