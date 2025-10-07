Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Ventas_Cotizar_Proveedor
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
        '    FlexPedido.DataSource = obj.ListaPedidos_xProveedor(Session("Ruta_Emp"))
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
        lblCodPedido.Text = ""
        FlexDetalle.Visible = False
        lblPedido.Visible = False
        FlexDetalle.Visible = False
        btnGuardar.Visible = False
        FlexPedido.PageIndex = e.NewPageIndex
        Call Llenar_Grilla()
    End Sub
    Protected Sub FlexPedido_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexPedido.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim CodPedido As String = ""
        Dim Cant As Integer : Cant = 0
        Dim dt As New DataTable
        lblCodPedido.Text = ""
        Dim CodProv As Integer : CodProv = 0
        FlexDetalle.Visible = False
        lblPedido.Visible = False
        FlexDetalle.Visible = False
        btnGuardar.Visible = False
        lblError.Text = ""
        lblError2.Text = ""
        Dim obj As New Listados
        If e.CommandName = "Ingresar" Then
            'Cant = 0
            'Try
            '    lblCodPedido.Text = FlexPedido.Rows(Index).Cells(1).Text
            '    CodPedido = lblCodPedido.Text
            '    dt = obj.ListaPedidos_Detalle(Session("Ruta_Emp"), CodPedido, Session("CodProv"), "1")
            '    If dt.Rows.Count > 0 Then
            '        For Each drMenuItem As Data.DataRow In dt.Rows
            '            If Nu(drMenuItem("ESTADO")) = "2" Then Cant = Cant + 1
            '        Next
            '    End If
            '    'If Cant = 0 Then
            '    FlexDetalle.DataSource = obj.ListaPedidos_Detalle(Session("Ruta_Emp"), CodPedido, Session("CodProv"), "1")
            '    FlexDetalle.DataBind()
            '    '    If FlexPedido.Rows.Count > 0 Then
            '    btnGuardar.Visible = True
            '    lblPedido.Visible = True
            '    lblCodPedido.Visible = True
            '    FlexDetalle.Visible = True
            '    '    End If
            '    'ElseIf Cant = dt.Rows.Count Then
            '    '    lblCodPedido.Text = ""
            '    '    lblError2.Text = "Sus datos ya han sido ingresados."
            '    'End If
            '    dt = Nothing
            'Catch ex As SqlException
            '    lblError2.Text = ex.Message
            'Catch ex As Exception
            '    lblError2.Text = ex.Message
            'Finally
            'End Try
        End If
    End Sub
    Protected Sub FlexDetalle_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles FlexDetalle.PageIndexChanging
        lblError.Text = ""
        'Dim obj As New Listados
        'FlexDetalle.PageIndex = e.NewPageIndex
        'FlexDetalle.DataSource = obj.ListaPedidos_Detalle(Session("Ruta_Emp"), lblCodPedido.Text, Session("CodProv"), "1")
        'FlexDetalle.DataBind()
    End Sub
    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer : i = 0
        Dim Act As Integer : Act = 0
        Dim Fecha As String
        Dim Cant As Integer : Cant = 0
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim tFecha As TextBox
        Dim tPrecioUnit As TextBox
        Dim tFormaPago As TextBox
        Dim obj As New Listados
        For i = 0 To FlexDetalle.Rows.Count - 1
            tFecha = FlexDetalle.Rows(i).Cells(7).FindControl("txtFechaEntrega")
            tPrecioUnit = FlexDetalle.Rows(i).Cells(9).FindControl("txtPrecioUnit")
            tFormaPago = FlexDetalle.Rows(i).Cells(11).FindControl("txtFormaPago")
            If tFecha.Text = "" And tPrecioUnit.Text = "" And tFormaPago.Text = "" Then Cant = Cant + 1
        Next
        If FlexDetalle.Rows.Count = Cant And FlexDetalle.Rows.Count = 0 Then lblError2.Text = "Debe ingresar Cotización." : Exit Sub
        Try
            Cn.Open()
            CmdGlobal.Connection = Cn
            For i = 0 To FlexDetalle.Rows.Count - 1
                tFecha = FlexDetalle.Rows(i).Cells(7).FindControl("txtFechaEntrega")
                tPrecioUnit = FlexDetalle.Rows(i).Cells(9).FindControl("txtPrecioUnit")
                tFormaPago = FlexDetalle.Rows(i).Cells(11).FindControl("txtFormaPago")
                Fecha = IIf(tFecha.Text = "", "", Right(tFecha.Text, 4) + Mid(tFecha.Text, 4, 2) + Left(tFecha.Text, 2))
                If tPrecioUnit.Text = "" And Fecha = "" And tFormaPago.Text = "" Then
                Else
                    If tPrecioUnit.Text <> "" And Fecha <> "" And tFormaPago.Text <> "" Then
                        CmdGlobal.CommandText = " UPDATE TBVENTAS_COTIZACION_PEDIDO_DET SET PRECIO_VENTA='" & tPrecioUnit.Text & "',FECHA_ENTREGA='" & Fecha & "',FORMA_PAGO='" & tFormaPago.Text.Trim & "' ,ESTADO='2' " _
                                              & " WHERE (COD_PEDIDO=" & lblCodPedido.Text.Trim & ") AND (COD_ARTICULO=" & FlexDetalle.Rows(i).Cells(1).Text & ") AND (ESTADO IN ('0','1','2')) AND (COD_PROVEEDOR=" & FlexDetalle.Rows(i).Cells(5).Text & ")"
                        CmdGlobal.ExecuteNonQuery()
                    End If
                End If
            Next
            Cant = 0
            'dt = obj.ListaPedidos_Detalle(Session("Ruta_Emp"), lblCodPedido.Text, Session("CodProv"), "0")
            'If dt.Rows.Count > 0 Then
            '    For Each drMenuItem As Data.DataRow In dt.Rows
            '        If Nu(drMenuItem("ESTADO")) = "2" Then Cant = Cant + 1
            '    Next
            '    If Cant = dt.Rows.Count Then
            '        CmdGlobal.CommandText = " UPDATE TBVENTAS_COTIZACION_PEDIDO SET ESTADO='2' WHERE NRO_PEDIDO=" & lblCodPedido.Text.Trim & " AND ESTADO IN ('0','1','2')"
            '        CmdGlobal.ExecuteNonQuery()
            '    End If
            'End If
            'dt = Nothing
        Catch ex As SqlException
            lblError2.Text = ex.Message
        Catch ex As Exception
            lblError2.Text = ex.Message
        Finally
            Cn.Close()
        End Try
        lblError2.Text = "Sus datos han sido guardados."
        'lblCodPedido.Text = ""
        'FlexDetalle.Visible = False
        'lblPedido.Visible = False
        'FlexDetalle.Visible = False
        'btnGuardar.Visible = False
    End Sub
End Class
