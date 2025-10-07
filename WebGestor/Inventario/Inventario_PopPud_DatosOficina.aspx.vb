Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Inventario_PopPud_DatosOficina
    Inherits System.Web.UI.Page

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim pdCCCodigo As Double = 0
            Dim dt As New DataTable
            Dim objCC As New clsLogis_Listado
            pdCCCodigo = Nz(Session("CodSeccion"))
            dt = objCC.Busca_Centro_Costos_Seccion_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), 0, pdCCCodigo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtCCCod.Text = Nu(dr("CECOSE_COD_INTERNO"))
                    txtCCDescripcion.Text = Nu(dr("CECOSE_DESCRIPCION"))
                    txtCCCargo.Text = Nu(dr("CARGO"))
                    txtCCNombre.Text = Nu(dr("NOMBRE"))
                    txtCCAnexo.Text = Nu(dr("ANEXO"))
                    txtCCTelefono.Text = Nu(dr("TELEFONO"))
                    txtCCCelular.Text = Nu(dr("CEL_BANCO"))
                    txtCCCorreo.Text = Nu(dr("CORREO"))
                    txtCCTipo.Text = Nu(dr("TIPO_OFICINA"))
                    txtCCDireccion.Text = Nu(dr("CECOSE_DIRECCION"))
                Next
            End If
        End If
    End Sub
    Private Sub Llenar_DatosOficina()
        Dim pdCCCodigo As Double = 0
        Dim dt As New DataTable
        Dim objCC As New clsLogis_Listado
        pdCCCodigo = Nz(Session("CodSeccion"))
        Try
            dt = objCC.Busca_Centro_Costos_Seccion_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), 0, pdCCCodigo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtCCCod.Text = Nu(dr("CECOSE_COD_INTERNO"))
                    txtCCDescripcion.Text = Nu(dr("CECOSE_DESCRIPCION"))
                    txtCCCargo.Text = Nu(dr("CARGO"))
                    txtCCNombre.Text = Nu(dr("NOMBRE"))
                    txtCCAnexo.Text = Nu(dr("ANEXO"))
                    txtCCTelefono.Text = Nu(dr("TELEFONO"))
                    txtCCCelular.Text = Nu(dr("CEL_BANCO"))
                    txtCCCorreo.Text = Nu(dr("CORREO"))
                    txtCCTipo.Text = Nu(dr("TIPO_OFICINA"))
                Next
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos:" & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('ha ocurrido un error en la aplicacion:" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub Cerrar_Click(sender As Object, e As EventArgs) Handles Cerrar.Click
        Response.Write("<script>window.close();</script>")
    End Sub
End Class
