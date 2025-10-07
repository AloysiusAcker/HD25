Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_GuiaArchivo
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New clsInv_Listados
        lblError.Text = ""
        Dim FechaEntrega : FechaEntrega = ""
        Dim FechaFin : FechaFin = ""
        Dim CodCurrier As Double : CodCurrier = 0
        Dim dtListado As New DataTable
        Dim CantReg As Double : CantReg = 0
        Dim pcodArchivo As Double : pcodArchivo = 0
        Dim i As Integer : i = 0
        Try
            dtListado = obj.Lista_GuiaArchivo(Session("Ruta_Emp"), Session("CodEmpresa"), FechaEntrega, CodCurrier, FechaFin, "")
            Flex.DataSource = dtListado
            Flex.DataBind()
            CantReg = dtListado.Rows.Count
            dtListado = Nothing
            lblRegistro.Text = "Registros Encontrados : " & CantReg
        Catch Ex As SqlException
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        Catch Ex As Exception
            lblError.Visible = True
            lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        Finally

        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call btnListar_Click(sender, e)
        End If
    End Sub

    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
      '
    End Sub
End Class
