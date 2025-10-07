Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Garantia_ConsultaEquipo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtNroSerie.Text = ""
            Call btnLimpiar_Click(sender, e)
        End If
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim obj As New clsInv_Listados
        Dim dt As DataTable
        Dim psSerie As String
        lblError.Text = ""
        psSerie = txtNroSerie.Text
        If psSerie = "" Then lblError.Text = "Debe ingresar el Nro de Serie." : Exit Sub
        Try
            Dim psConexion As String = Session("Ruta_Emp") 'ConfigurationManager.AppSettings("cnTecnicos")
            dt = obj.Consulta_Garantia_xEquipo(psConexion, Session("CodEmpresa"), psSerie)
            If dt.Rows.Count = 1 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    txtCodGarantia.Text = Nu(drMenuItem("COD_GARANTIA"))
                    txtSerie.Text = Nu(drMenuItem("GARANTIA_SERIE"))
                    txtOCompra.Text = Nu(drMenuItem("COD_OCOMPRA"))
                    txtArtCodigo.Text = Nu(drMenuItem("COD_ARTICULO"))
                    txtArtDescripcion.Text = Nu(drMenuItem("ARTDESCRIPCION"))
                    txtArtParte.Text = Nu(drMenuItem("ARTCODEQUIVA"))
                    txtArtModelo.Text = Nu(drMenuItem("GARANTIA_MODELO"))
                    txtRucProveedor.Text = Nu(drMenuItem("RUCPROVEEDOR"))
                    txtProveedor.Text = Nu(drMenuItem("PROVEEDOR"))
                    If Nu(drMenuItem("GARANTIA_TIPO_CLIENTE")) = "6" Then
                        txtRucCliente.Text = Nu(drMenuItem("RUCCLIENTE"))
                        txtCliente.Text = Nu(drMenuItem("CLILENTE"))
                    ElseIf Nu(drMenuItem("GARANTIA_TIPO_CLIENTE")) = "5" Then
                        txtRucCliente.Text = Nu(drMenuItem("DNI_PERSONA"))
                        txtCliente.Text = Nu(drMenuItem("PERSONA"))
                    End If
                    txtFecCompra.Text = Nu(drMenuItem("FECHA_COMPRA"))
                    txtFinCompra.Text = Nu(drMenuItem("FECHA_COMPRA_FIN"))
                    txtFecSalida.Text = Nu(drMenuItem("FECHA_SALIDA"))
                    txtFinSalida.Text = Nu(drMenuItem("FECHA_SALIDA_FIN"))
                    txtFactura.Text = Nu(drMenuItem("GARANTIA_FACTURA"))
                Next
            End If
            dt = Nothing
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub btnLimpiar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLimpiar.Click
        txtNroSerie.Text = ""
        txtCodGarantia.Text = ""
        txtSerie.Text = ""
        txtOCompra.Text = ""
        txtArtCodigo.Text = ""
        txtArtDescripcion.Text = ""
        txtArtParte.Text = ""
        txtArtModelo.Text = ""
        txtRucProveedor.Text = ""
        txtProveedor.Text = ""
        txtRucCliente.Text = ""
        txtCliente.Text = ""
        txtFecCompra.Text = ""
        txtFinCompra.Text = ""
        txtFecSalida.Text = ""
        txtFinSalida.Text = ""
        txtFactura.Text = ""
    End Sub
End Class
