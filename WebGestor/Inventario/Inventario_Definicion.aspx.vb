Imports System.Data
Imports WebGestor
Public Class Inventario_Definicion
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call Ocultar_Visible(False)
            Dim fechaF As Date = Date.Today.ToString
            TxtFecha.Text = FormatoFecha(FechaActual)
        End If
    End Sub
    Protected Sub Ocultar_Visible(ByVal vf As Boolean)
        TxtCodigo.Visible = vf
        TxtDescripcion.Visible = vf
        LblCodigo.Visible = vf
        LblDescripción.Visible = vf
        LblResponsable.Visible = vf
        LblFecha.Visible = vf
        txtResponsable.Visible = vf
        TxtFecha.Visible = vf
        BtnCancelar.Visible = vf
        BtnGrabar.Visible = vf
    End Sub

    Protected Sub Limpiar_Cajas()
        TxtCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtFecha.Text = FormatoFecha(FechaActual)
        txtResponsable.Text = ""
    End Sub

    Protected Sub Listar()
        Dim obj As New Cls_Inventario
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Inventario(psconexion)
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Listar()
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Inventario
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")
        If e.CommandName = "Editar" Then
            Ocultar_Visible(True)
            TxtCodigo.Text = GridView1.Rows(Index).Cells(2).Text
            TxtDescripcion.Text = GridView1.Rows(Index).Cells(4).Text
            txtResponsable.Text = GridView1.Rows(Index).Cells(5).Text
            TxtFecha.Text = GridView1.Rows(Index).Cells(3).Text
            BtnGrabar.Text = "Actualizar"
        End If
        If e.CommandName = "Eliminar" Then
            Dim dt As New DataTable
            dt = obj.Eliminar_Inventario(cn, GridView1.Rows(Index).Cells(2).Text)
            If dt.Rows.Count > 0 Then
                MensajeUbic.Text = "No se puede eliminar. La Ubicacion esta siendo utilizado."
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeUbic').modal('show');", True)
            Else
                Listar()
                Ocultar_Visible(False)
                Limpiar_Cajas()
            End If
        End If
    End Sub
    Private Sub BtnUOk_Click(sender As Object, e As EventArgs) Handles BtnUOk.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalMensajeUbic').modal('hide');", True)
    End Sub

    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Dim obj As New Cls_Inventario
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")

        Limpiar_Cajas()

        TxtCodigo.Text = obj.Codigo(cn)
        Ocultar_Visible(True)
        BtnGrabar.Text = "Grabar"

        Dim fechaF As Date = Date.Today
        TxtFecha.Text = FormatoFecha(FechaActual)

    End Sub

    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Ocultar_Visible(False)
        Limpiar_Cajas()
    End Sub

    Protected Sub BtnGrabar_Click(sender As Object, e As EventArgs) Handles BtnGrabar.Click
        Dim obj As New Cls_Inventario
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As Double = 0
        codigo = Nz(TxtCodigo.Text)
        Dim fecha As String
        Dim descripcion As String = TxtDescripcion.Text
        Dim responsable As String = txtResponsable.Text

        If BtnGrabar.Text = "Grabar" Then
            fecha = Right(TxtFecha.Text, 4) + Mid(TxtFecha.Text, 4, 2) + Left(TxtFecha.Text, 2)
            obj.Registra_Inventario(psconexion, codigo, fecha, descripcion, responsable)
            Ocultar_Visible(False)
            Listar()
        End If

        If BtnGrabar.Text = "Actualizar" Then
            fecha = Right(TxtFecha.Text, 4) + Mid(TxtFecha.Text, 4, 2) + Left(TxtFecha.Text, 2)
            obj.Actualiza_Inventario(psconexion, codigo, fecha, descripcion, responsable)
            Listar()
        End If
    End Sub
End Class