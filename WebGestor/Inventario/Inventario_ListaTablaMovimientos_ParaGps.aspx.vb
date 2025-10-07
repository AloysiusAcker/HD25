Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient

Partial Class Inventario_Inventario_ListaTablaMovimientos_ParaGps
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            '
        End If
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New clsInv_Listados
        Try
            Dim ptipoPedido As String = ""
            Dim dt As DataTable
            dt = obj.Lista_Tabla_movimientos_gps(Session("Ruta_Emp"))
            Flex.DataSource = dt
            Flex.DataBind()
            If dt.Rows.Count = 1 Then
                lblRegistro3.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro3.Text = "No hay registros."
            ElseIf dt.Rows.Count > 0 Then
                lblRegistro3.Text = "Hay " & dt.Rows.Count & " registros."
            End If
        Catch ex As SqlException
            lblError.Text = "Se ha producido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha producido un error en la aplicación: <br>" & ex.Message
        End Try
        'Lista_Pedidos
    End Sub
End Class
