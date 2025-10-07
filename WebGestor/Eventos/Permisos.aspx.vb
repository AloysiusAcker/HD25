Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor
Partial Class Eventos_Permisos
    Inherits System.Web.UI.Page

    Dim obj As New Cls_Eventos
    Dim objSeg As New ModuloSeguridad
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LlenaComboItem("TBOPC563", DdlTipo)
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            Dim obj As New Cls_Eventos
            GvPermiso.DataSource = obj.Lista_Permisos(Session("Ruta_Emp"), Session("CodEmpresa"))
            GvPermiso.DataBind()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Limpiar()
        TxtPerCodigo.Text = obj.Ultimo_Permiso(Session("Ruta_Emp"))
        Session("EditarPermiso") = "No"
        Dim dt As New DataTable
        dt = objSeg.Listar_Usuarios_SinAdm(Ruta_Ng)
        DdlPersonal.DataSource = dt
        DdlPersonal.DataValueField = "USUARI_CODIGO"
        DdlPersonal.DataTextField = "NOMBRES"
        DdlPersonal.DataBind()
        DdlPersonal.Items.Add("< Seleccionar >") : DdlPersonal.SelectedValue = "< Seleccionar >"
        DivPermiso.Visible = True
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Limpiar()
        DivPermiso.Visible = False
    End Sub

    Private Sub Limpiar()
        TxtPerCodigo.Text = ""
        TxtPerMotivo.Text = ""
        DdlTipo.SelectedValue = "< Seleccionar >"
        TxtFechaIni.Text = ""
        TxtFechaFin.Text = ""
        TxtHoraIni.Text = ""
        TxtHoraFin.Text = ""
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        '

        Try
            If DdlTipo.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar tipo de persmiso');", True)
            ElseIf DdlPersonal.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar personal.');", True)
            ElseIf TxtFechaIni.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Fecha que inicia el permiso');", True)
            ElseIf TxtFechaFin.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Fecha que termina el permiso');", True)
            ElseIf TxtPerMotivo.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el motivo del permiso');", True)
            Else

                Dim psFechaIni As String = ""
                Dim psFechaFin As String = ""
                Dim psHoraIni As String = ""
                Dim psHoraFin As String = ""

                If TxtFechaIni.Text <> "" Then
                    psFechaIni = Left(TxtFechaIni.Text, 4) & Mid(TxtFechaIni.Text, 6, 2) & Right(TxtFechaIni.Text, 2)
                End If
                If TxtFechaFin.Text <> "" Then
                    psFechaFin = Left(TxtFechaFin.Text, 4) & Mid(TxtFechaFin.Text, 6, 2) & Right(TxtFechaFin.Text, 2)
                End If
                If TxtHoraIni.Text <> "" Then
                    psHoraIni = Left(TxtHoraIni.Text, 2) & Right(TxtHoraIni.Text, 2)
                End If
                If TxtHoraFin.Text <> "" Then
                    psHoraFin = Left(TxtHoraFin.Text, 2) & Right(TxtHoraFin.Text, 2)
                End If
                obj.Insertar_Permisos(Session("Ruta_Emp"), Session("CodEmpresa"), Session("USer"), DdlTipo.SelectedValue, DdlPersonal.SelectedValue, psFechaIni, psFechaFin, psHoraIni, psHoraFin, TxtPerMotivo.Text)

                Limpiar()
                BtnCancelar_Click(sender, e)
                BtnListar_Click(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub GvPermiso_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvPermiso.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim cn As String = Session("Ruta_Emp")
        Dim dt As New DataTable
        Dim pdCodPermiso As Integer = 0
        Try
            If e.CommandName = "Quitar" Then
                pdCodPermiso = Nz(GvPermiso.Rows(Index).Cells(1).Text)
                dt = obj.Eliminar_Permisos(Session("Ruta_Emp"), Session("CodEmpresa"), 0)
                BtnListar_Click(sender, e)
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
End Class
