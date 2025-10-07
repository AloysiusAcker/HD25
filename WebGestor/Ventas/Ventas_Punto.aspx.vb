Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Ventas_Ventas_Punto
    Inherits System.Web.UI.Page
    Dim ObjVenta As New ClsVentas_Listados
    Dim ObjInv As New clsInv_Listados
    Dim ObjCont As New clsCont_Listados
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            btnOpen.Attributes.Add("OnClick", "window.open('Ventas_BusArticulos.aspx',null,'height=400,width=480');")
            BtnCliente.Attributes.Add("OnClick", "window.open('Ventas_Punto_Cliente.aspx',null,'height=400,width=480');")
            lblError.Text = ""
            Dim dt As DataTable
            dt = ObjVenta.PtoVenta_ListaCaja(Session("CodEmpresa"), Session("Ruta_Emp"))
            DdlCaja.DataSource = dt
            DdlCaja.DataTextField = "nro_caja"
            DdlCaja.DataValueField = "CAJA_CODIGO"
            DdlCaja.DataBind()
            DdlCaja.Items.Add("< Seleccionar >") : DdlCaja.SelectedValue = "< Seleccionar >"
            If DdlCaja.Items.Count = 1 Then
                DdlCaja.SelectedIndex = 0
            Else
                DdlCaja.SelectedValue = "< Seleccionar >"
            End If
            Call ObjInv.Llena_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), DdlAlmacen)
            DdlAlmacen.SelectedValue = "1"
        End If
    End Sub
    Protected Sub btnSeleccionar_Click(sender As Object, e As EventArgs)
        '
    End Sub
    Protected Sub btnCerrarArt_Click(sender As Object, e As EventArgs) Handles BtnCerrarArt.Click
        txtPArtCodigo.Text = ""
        txtPArtDescripcion.Text = ""
        FlexArt.DataSource = Nothing
        FlexArt.DataBind()
    End Sub
    Protected Sub btnListarArt_Click(sender As Object, e As EventArgs) Handles btnListarArt.Click
        Try
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            lblErrorArt.Text = ""
            If txtPArtCodigo.Text.Trim <> "" Then pdCodArt = txtPArtCodigo.Text.Trim
            FlexArt.DataSource = ObjVenta.PtoVenta_ListaArticulos(Session("CodEmpresa"), Session("Ruta_Emp"), pdCodArt, txtPArtDescripcion.Text.Trim, "")
            FlexArt.DataBind()
            lblRegArt.Text = "Se encontrarón " & FlexArt.Items.Count & " registros."
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblErrorArt.Text = ex.Message
        Catch ex As Exception
            lblErrorArt.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub TxtCant_TextChanged(sender As Object, e As EventArgs)

    End Sub
    Protected Sub txtProducto_TextChanged(sender As Object, e As EventArgs) Handles txtProducto.TextChanged
        Try
            lblError.Text = ""
            If txtProducto.Text = "" Then Exit Sub
            Dim dt2 As DataTable
            dt2 = ObjVenta.PtoVenta_ListaArticulos(Session("CodEmpresa"), Session("Ruta_Emp"), 0, "", txtProducto.Text)
            If dt2.Rows.Count > 0 Then
                For Each dr2 As DataRow In dt2.Rows
                    For i = 0 To Flex.Rows.Count - 1
                        If Flex.Rows(i).Cells(2).Text = dr2("PRODUCTO_COD") Then lblError.Text = "El producto esta agregado." : Exit Sub
                    Next
                Next
            End If
            Dim pdCodArt As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
            lblErrorArt.Text = ""
            Dim drArt As DataRow
            Dim dtArt As New DataTable
            Dim dt As DataTable
            Dim pdIgv As Double = ObjVenta.Obtener_ValorIgv(Ruta_GrEmp)
            dtArt.Columns.Add("c1")
            dtArt.Columns.Add("c2")
            dtArt.Columns.Add("c3")
            'dtArt.Columns.Add("c4")
            dtArt.Columns.Add("c5")
            dtArt.Columns.Add("c6")
            dtArt.Columns.Add("c7")
            dtArt.Columns.Add("c8")
            dtArt.Columns.Add("c9")
            Dim pdPrecioVenta As Double = 0
            Dim pdPrecioLista As Double = 0
            Dim pdPrecioSubTotal As Double = 0
            Dim pdPrecioIGV As Double = 0
            Dim pdPrecioTotal As Double = 0
            For i = 0 To Flex.Rows.Count - 1
                drArt = dtArt.NewRow()
                Dim txtCantP As TextBox = Flex.Rows(i).Cells(4).FindControl("TxtCant")
                Dim txtPrecio As TextBox = Flex.Rows(i).Cells(6).FindControl("TxtPV")
                drArt("c1") = Flex.Rows(i).Cells(2).Text
                drArt("c2") = Flex.Rows(i).Cells(3).Text
                drArt("c3") = txtCantP.Text
                'drArt("c4") = Flex.Rows(i).Cells(5).Text
                drArt("c5") = txtPrecio.Text
                drArt("c6") = Flex.Rows(i).Cells(7).Text
                drArt("c7") = Flex.Rows(i).Cells(8).Text
                drArt("c8") = Flex.Rows(i).Cells(9).Text
                drArt("c9") = Flex.Rows(i).Cells(10).Text
                dtArt.Rows.Add(drArt)
            Next
            dt = ObjVenta.PtoVenta_ListaArticulos(Session("CodEmpresa"), Session("Ruta_Emp"), pdCodArt, "", txtProducto.Text)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If Nz(dr("PRECIO_VENTA_IGV")) > 0 Then
                        pdPrecioVenta = Nz(dr("PRECIO_VENTA_IGV"))
                        pdPrecioLista = Nz(dr("PRECIO_VENTA_IGV")) / (pdIgv + 1)
                        pdPrecioSubTotal = Nz(dr("PRECIO_VENTA_IGV")) / (pdIgv + 1)
                        pdPrecioIGV = Nz(dr("PRECIO_VENTA_IGV")) - pdPrecioSubTotal
                        pdPrecioTotal = Nz(dr("PRECIO_VENTA_IGV"))
                    End If
                    drArt = dtArt.NewRow()
                    drArt("c1") = dr("PRODUCTO_COD")
                    drArt("c2") = dr("ART_DESCRIPCION")
                    drArt("c3") = 1
                    'drArt("c4") = pdPrecioLista
                    drArt("c5") = pdPrecioVenta
                    drArt("c6") = pdPrecioSubTotal
                    drArt("c7") = pdPrecioIGV
                    drArt("c8") = pdPrecioTotal
                    drArt("c9") = Nz(dr("STOCK_ACTUAL"))
                    dtArt.Rows.Add(drArt)
                Next
            End If
            Flex.DataSource = dtArt
            Flex.DataBind()
            txtProducto.Text = ""
            lblRegArt.Text = "Se encontrarón " & dt.Rows.Count & " registros."
            ModalPopupExtender2.Show()
        Catch ex As SqlException
            lblErrorArt.Text = ex.Message
        Catch ex As Exception
            lblErrorArt.Text = ex.Message
        Finally
        End Try
    End Sub

    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        lblError.Text = ""
        Dim obj As New clsInv_Listados
        Dim drArt As DataRow
        Dim psCodArt As String = ""
        Dim i As Long = 0
        Dim dtArt As New DataTable
        Dim dt As DataTable
        Dim pdIgv As Double = ObjVenta.Obtener_ValorIgv(Ruta_GrEmp)
        dt = Nothing
        dtArt.Columns.Add("c1")
        dtArt.Columns.Add("c2")
        dtArt.Columns.Add("c3")
        'dtArt.Columns.Add("c4")
        dtArt.Columns.Add("c5")
        dtArt.Columns.Add("c6")
        dtArt.Columns.Add("c7")
        dtArt.Columns.Add("c8")
        dtArt.Columns.Add("c9")
        Dim pdPrecioVenta As Double = 0
        Dim pdPrecioLista As Double = 0
        Dim pdPrecioSubTotal As Double = 0
        Dim pdPrecioIGV As Double = 0
        Dim pdPrecioTotal As Double = 0
        Try
            If e.CommandName = "Quitar" Then
                psCodArt = Flex.Rows(Index).Cells(2).Text
                For i = 0 To Flex.Rows.Count - 1
                    If Flex.Rows(i).Cells(2).Text <> psCodArt Then
                        drArt = dtArt.NewRow()
                        Dim txtCantP As TextBox = Flex.Rows(i).Cells(4).FindControl("TxtCant")
                        Dim txtPrecio As TextBox = Flex.Rows(i).Cells(6).FindControl("TxtPV")
                        drArt("c1") = Flex.Rows(i).Cells(2).Text
                        drArt("c2") = Flex.Rows(i).Cells(3).Text
                        drArt("c3") = txtCantP.Text
                        'drArt("c4") = Flex.Rows(i).Cells(5).Text
                        drArt("c5") = txtPrecio.Text
                        drArt("c6") = Flex.Rows(i).Cells(7).Text
                        drArt("c7") = Flex.Rows(i).Cells(8).Text
                        drArt("c8") = Flex.Rows(i).Cells(9).Text
                        drArt("c9") = Flex.Rows(i).Cells(10).Text
                        dtArt.Rows.Add(drArt)
                    End If
                Next
                If dtArt.Rows.Count > 0 Then
                    lblRegArt.Text = "Hay " & dtArt.Rows.Count & " equipo."
                    Flex.DataSource = dtArt
                    Flex.DataBind()
                Else
                    Flex.DataSource = dt
                    Flex.DataBind()
                    lblRegArt.Text = "No hay equipos."
                End If
            ElseIf e.CommandName = "Calcular" Then
                Dim txtCantP As TextBox = Flex.Rows(Index).Cells(4).FindControl("TxtCant")
                Dim txtPrecio As TextBox = Flex.Rows(Index).Cells(6).FindControl("TxtPV")
                pdPrecioVenta = txtPrecio.Text
                pdPrecioSubTotal = pdPrecioVenta * Nz(txtCantP.Text)
                pdPrecioIGV = (pdPrecioVenta * pdIgv) * Nz(txtCantP.Text)
                pdPrecioTotal = pdPrecioSubTotal + pdPrecioIGV
                txtCantP.Text = Nz(txtCantP.Text)
                txtPrecio.Text = pdPrecioVenta
                Flex.Rows(Index).Cells(7).Text = pdPrecioSubTotal
                Flex.Rows(Index).Cells(8).Text = pdPrecioIGV
                Flex.Rows(Index).Cells(9).Text = pdPrecioTotal
            End If
            Me.Page.Session.Timeout = 1080
        Catch ex As SqlException
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Catch ex As Exception
            lblError.Text = ex.Message
            lblError.ForeColor = System.Drawing.Color.Red
        Finally
        End Try
    End Sub
    Protected Sub TxtPV_OnTextChanged(sender As Object, e As EventArgs)
        'Dim txtCantP As TextBox = Flex.Rows(Index).Cells(4).FindControl("TxtCant")
        'Dim txtPrecio As TextBox = Flex.Rows(Index).Cells(6).FindControl("TxtPV")
        'pdPrecioVenta = txtPrecio.Text
        'pdPrecioSubTotal = pdPrecioVenta * Nz(txtCantP.Text)
        'pdPrecioIGV = (pdPrecioVenta * pdIgv) * Nz(txtCantP.Text)
        'pdPrecioTotal = pdPrecioSubTotal + pdPrecioIGV
        'txtCantP.Text = Nz(txtCantP.Text)
        'txtPrecio.Text = pdPrecioVenta
        'Flex.Rows(Index).Cells(7).Text = pdPrecioSubTotal
        'Flex.Rows(Index).Cells(8).Text = pdPrecioIGV
        'Flex.Rows(Index).Cells(9).Text = pdPrecioTotal

        'Dim tb1 As TextBox =
        'TextBox tb1 = ((TextBox)(sender));
        '        GridViewRow gv1 = ((GridViewRow)(tb1.NamingContainer))


    End Sub
End Class
