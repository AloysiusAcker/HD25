Imports DevExpress.Web
Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports System.Data
Imports DevExpress.XtraGrid
Partial Class CRM_CRM_Lista_Tickect_DevExpress
    Inherits System.Web.UI.Page
    ReadOnly ObjList As New ClsGtp_Listados

    Private Sub CRM_CRM_Lista_Tickect_DevExpress_Load(sender As Object, e As EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then
        ''Create columns on the first load. 
        Call Listar_Ticket("")
        Call Lista_EstadoTicket()
        'Call GridEstado_SelectionChanged(sender, e)
        'End If
    End Sub

    Private Function Listar_Ticket(ByVal psCodEstado As String) As DataTable
        Dim dt As DataTable
        dt = ObjList.GTP_ListaTickect(Session("Ruta_Emp"), psCodEstado)
        grid.DataSource = dt
        grid.DataBind()
        Return dt
    End Function

    Private Sub Lista_EstadoTicket()
        GridEstado.DataSource = ObjList.GTP_ListaEstadoTickect(Session("Ruta_Emp"))
        GridEstado.DataBind()
    End Sub

    Private Sub GridEstado_SelectionChanged(sender As Object, e As EventArgs) Handles GridEstado.SelectionChanged
        Dim keyvalues As List(Of Object) = GridEstado.GetSelectedFieldValues("ticket_estado")
        Call Listar_Ticket(keyvalues(1))
    End Sub

    Private Sub GridEstado_RowCommand(sender As Object, e As ASPxGridViewRowCommandEventArgs) Handles GridEstado.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgs)
        If e.KeyValue = "Seleccionar" Then
            Dim id As String = e.KeyValue
            Dim filename As String = GridEstado.GetRowValuesByKeyValue(id, "TICKET_ESTADO").ToString()
            Call Listar_Ticket(filename)
        End If
    End Sub

End Class
