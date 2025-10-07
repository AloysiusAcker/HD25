Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Ventas_Ventas_Punto_Cliente
    Inherits System.Web.UI.Page
    Dim ObjVenta As New ClsVentas_Listados
    Dim ObjInv As New clsInv_Listados
    Dim ObjCont As New clsCont_Listados
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            Dim dt As DataTable
            dt = ObjCont.Cont_ListaDocumentos(Session("CodEmpresa"), "2019", Session("Ruta_Emp"))
            DdlDocTipo.DataSource = dt
            DdlDocTipo.DataTextField = "DOC_DOCUMENTO"
            DdlDocTipo.DataValueField = "DOC_CODIGO"
            DdlDocTipo.DataBind()
            DdlDocTipo.Items.Add("< Seleccionar >") : DdlDocTipo.SelectedValue = "< Seleccionar >"
            If DdlDocTipo.Items.Count = 1 Then
                DdlDocTipo.SelectedIndex = 0
            Else
                DdlDocTipo.SelectedValue = "< Seleccionar >"
            End If
        End If
    End Sub

    Protected Sub Cerrar_Click(sender As Object, e As EventArgs) Handles Cerrar.Click
        Response.Write("<script>window.close();</script>")
    End Sub
    'Cont_ListaClientes
    Protected Sub btnListaCC_Click(sender As Object, e As EventArgs) Handles btnListaCC.Click
        lblError.Text = ""
        Try
            Dim obj As New clsInv_Listados
            gridCentroCosto.DataSource = Nothing
            Dim pdCodAlmacen As Double = 0
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            gridCentroCosto.DataSource = ObjCont.Cont_ListaClientes(Session("CodEmpresa"), Session("Ruta_Emp"), txtRuc.Text.Trim, txtRazonSocial.Text.Trim)
            gridCentroCosto.DataBind()
            ModalPopupExtender1.Show()
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnCerrarCC_Click(sender As Object, e As EventArgs) Handles btnCerrarCC.Click
        ModalPopupExtender1.Hide()
        gridCentroCosto.DataSource = Nothing
        gridCentroCosto.DataBind()
    End Sub

    Private Sub gridCentroCosto_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gridCentroCosto.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "sel_detalle" Then
            txtCodCliente.Text = ""
            txtRuc.Text = ""
            txtRazonSocial.Text = ""
            txtRuc.Text = gridCentroCosto.Rows(Index).Cells(1).Text
            txtRazonSocial.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridCentroCosto.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtCodCliente.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gridCentroCosto.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            gridCentroCosto.DataSource = Nothing
            gridCentroCosto.DataBind()
            ModalPopupExtender1.Hide()
        End If
    End Sub
End Class
