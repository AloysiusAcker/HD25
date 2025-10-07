Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Ventas_Ingresa_OrdenCompra
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Grilla()
        End If
    End Sub
    Private Sub Llenar_Grilla()
        'Dim obj As New Listados
        'lblError.Text = ""
        'lblError2.Text = ""
        'Try
        '    FlexPedido.DataSource = obj.Lista_Cotizacion(Session("Ruta_Emp"), Session("CodProv"), Session("CodEmpresa"), "2")
        '    FlexPedido.DataBind()
        'Catch Ex As SqlException
        '    lblError.Visible = True
        '    lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        'Catch Ex As Exception
        '    lblError.Visible = True
        '    lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        'Finally

        'End Try
    End Sub
    Protected Sub FlexPedido_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexPedido.PageIndexChanging
        lblError.Text = ""
        lblCodCotiz.Text = ""
        FlexDetalle.Visible = False
        lblCotizacion.Visible = False
        FlexDetalle.Visible = False
        btnGuardar.Visible = False
        lblOrdenCompra.Visible = False
        txtOrdenCompra.Visible = False
        FlexPedido.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub FlexPedido_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPedido.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim CodCotizacion As String = ""
        Dim Cant As Integer : Cant = 0
        Dim dt As New DataTable
        lblCodCotiz.Text = ""
        Dim CodProv As Integer : CodProv = 0
        FlexDetalle.Visible = False
        lblCotizacion.Visible = False
        FlexDetalle.Visible = False
        btnGuardar.Visible = False
        lblOrdenCompra.Visible = False
        txtOrdenCompra.Visible = False
        lblError.Text = ""
        lblError2.Text = ""
        Dim obj As New Listados
        If e.CommandName = "Ingresar" Then
            Try
                'lblCodCotiz.Text = FlexPedido.Rows(Index).Cells(1).Text
                'txtOrdenCompra.Text = FlexPedido.Rows(Index).Cells(2).Text.Replace("&nbsp;", "")
                'CodCotizacion = lblCodCotiz.Text
                'FlexDetalle.DataSource = obj.Lista_CotizacionDet(Session("Ruta_Emp"), Session("CodEmpresa"), CodCotizacion)
                'FlexDetalle.DataBind()
                'btnGuardar.Visible = True
                'lblCotizacion.Visible = True
                'lblCodCotiz.Visible = True
                'FlexDetalle.Visible = True
                'lblOrdenCompra.Visible = True
                'txtOrdenCompra.Visible = True
                'dt = Nothing
            Catch ex As SqlException
                lblError2.Text = ex.Message
            Catch ex As Exception
                lblError2.Text = ex.Message
            Finally
            End Try
        End If
        If txtOrdenCompra.Text.Trim <> "" Then lblError2.Text = "Su Orden de Compra ya fue ingresada."
    End Sub
    Protected Sub FlexDetalle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexDetalle.PageIndexChanging
        lblError.Text = ""
        Dim obj As New Listados
        FlexDetalle.PageIndex = e.NewPageIndex
        'FlexDetalle.DataSource = obj.ListaPedidos_Detalle(Session("Ruta_Emp"), lblCodCotiz.Text, Session("CodProv"), "1")
        'FlexDetalle.DataBind()
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer : i = 0
        Dim Act As Integer : Act = 0
        Dim Cant As Integer : Cant = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim obj As New Listados
        If txtOrdenCompra.Text.Trim = "" Then lblError2.Text = "Debe ingresar la Oredn de Compra." : Exit Sub
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            CmdGlobal.CommandText = " UPDATE TBVENTAS_COTIZACION SET NRO_ORDEN='" & txtOrdenCompra.Text.Trim & "',ESTADO='3' WHERE NRO_COTIZACION=" & lblCodCotiz.Text.Trim & " AND ESTADO ='2'"
            CmdGlobal.ExecuteNonQuery()
        Catch ex As SqlException
            lblError2.Text = ex.Message
        Catch ex As Exception
            lblError2.Text = ex.Message
        Finally
            Cn.Close()
        End Try
        lblError2.Text = "La Orden de Compra ha sido guardada."
    End Sub
End Class
