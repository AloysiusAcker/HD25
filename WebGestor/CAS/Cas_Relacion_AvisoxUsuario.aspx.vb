Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class CAS_Cas_Relacion_AvisoxUsuario
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                Call LlenaComboItem("TBOPC333", DdlTipo, "", "Seleccionar Tipo")
                Call LlenaComboItem("TBOPC334", DdlEstado, "", "Seleccionar Estado")
                'Call LlenaComboItem("TBOPC335", DdlAviso)
                'DdlAviso.SelectedValue = "1"
                'Call ListaAvisos()
                'DivAviso.Visible = False
                'DivAvisoDet.Visible = False
            Catch Ex As SqlException
                'LblError.Visible = True
                'LblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
            Catch Ex As Exception
                'LblError.Visible = True
                'LblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
            Finally
            End Try
            Me.Page.Session.Timeout = 1080
        End If
    End Sub
End Class
