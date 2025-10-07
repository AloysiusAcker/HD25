Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_CatalogoArticulo
    Inherits System.Web.UI.Page
    Dim ObjVenta As New ClsVentas_Listados
    Dim ObjInv As New clsInv_Listados
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblErrorArt.Text = ""
        End If
    End Sub
    Protected Sub BtnListaArt_Click(sender As Object, e As EventArgs) Handles BtnListaArt.Click
        Try
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            lblErrorArt.Text = ""
            If txtBusArtC.Text.Trim <> "" Then pdCodArt = txtBusArtC.Text.Trim
            FlexArt.DataSource = ObjVenta.PtoVenta_ListaArticulos(Session("CodEmpresa"), Session("Ruta_Emp"), pdCodArt, txtBusArtD.Text.Trim, "")
            FlexArt.DataBind()
            lblRegArt.Text = "Se encontrarón " & FlexArt.Items.Count & " registros."
        Catch ex As SqlException
            lblErrorArt.Text = ex.Message
        Catch ex As Exception
            lblErrorArt.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Cerrar_Click(sender As Object, e As EventArgs) Handles Cerrar.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Private Sub FlexArt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FlexArt.SelectedIndexChanged

    End Sub
    Private Sub FlexArt_ItemCommand(source As Object, e As DataListCommandEventArgs) Handles FlexArt.ItemCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "btnSeleccionar" Then
            FlexArt.SelectedIndex = e.Item.ItemIndex
            Dim lblArtCod As Label = FlexArt.SelectedItem.FindControl("lblCodigo")
            Dim lblArtDesc As Label = FlexArt.SelectedItem.FindControl("lblNombre")
            Dim lblArtPrecio As Label = FlexArt.SelectedItem.FindControl("lblPrecio")
            Dim lblArtPrecioIgv As Label = FlexArt.SelectedItem.FindControl("lblPrecioIgv")
            Dim lblArtStock As Label = FlexArt.SelectedItem.FindControl("lblStock")
            Session("PV_ArtCod") = lblArtCod.Text
            Session("PV_ArtNombre") = lblArtDesc.Text
            Session("PV_ArtPrecio") = lblArtPrecio.Text
            Session("PV_ArtPrecioIgv") = lblArtPrecioIgv.Text
            Session("PV_ArtStock") = lblArtStock.Text
        End If
    End Sub
End Class
