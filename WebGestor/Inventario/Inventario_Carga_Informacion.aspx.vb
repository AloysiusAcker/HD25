Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Inventario_Inventario_Carga_Informacion
    Inherits System.Web.UI.Page

    Dim obj As New clsInv_Listados
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblRegistro.Text = "0 Registro"
            lblError.Text = ""
            Call Llenar_Marca()
            Call Llenar_Denominacion()
        End If
    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            lblError.Text = ""
            Call Listar_Carga()
        Catch ex As SqlException
            lblError.Text = "Se ha producido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha producido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub

    Private Sub Listar_Carga()
        Dim dt As DataTable
        Dim psMarca As String = ""
        Dim psDenominacion As String = ""
        If cboMarca.SelectedValue <> "< Seleccionar >" Then psMarca = cboMarca.Text
        If cboDenominacion.SelectedValue <> "< Seleccionar >" Then psDenominacion = cboDenominacion.Text
        dt = obj.Lista_Carga(Session("Ruta_Emp"), psMarca, psDenominacion)
        Flex.DataSource = dt
        Flex.DataBind()
        If dt.Rows.Count = 0 Then
            lblRegistro.Text = "0 Registro"
        Else
            lblRegistro.Text = dt.Rows.Count & " Registros"
        End If
    End Sub
    Private Sub Flex_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Flex.PageIndexChanging
        lblError.Text = ""
        Flex.PageIndex = e.NewPageIndex
        Call Listar_Carga()
    End Sub
    Private Sub Llenar_Marca()
        Dim dt As DataTable
        cboMarca.Items.Clear()
        Try
            dt = obj.Llena_Marca_Carga(Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    cboMarca.Items.Add(dr("SERIE_MARCA"))
                Next
            End If
            cboMarca.Items.Add("< Seleccionar >")
            cboMarca.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblError.Text = "Se ha producido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha producido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Private Sub Llenar_Denominacion()
        Dim dt As DataTable
        cboDenominacion.Items.Clear()
        Try
            dt = obj.Llena_Denominacion_Carga(Session("Ruta_Emp"))
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    cboDenominacion.Items.Add(dr("SERIE_DENOMINACION"))
                Next
            End If
            cboDenominacion.Items.Add("< Seleccionar >")
            cboDenominacion.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            lblError.Text = "Se ha producido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha producido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub chkDenominacion_CheckedChanged(sender As Object, e As EventArgs) Handles chkDenominacion.CheckedChanged
        If chkDenominacion.Checked = True Then
            cboDenominacion.Enabled = True
            cboDenominacion.SelectedValue = "< Seleccionar >"
        Else
            cboDenominacion.Enabled = False
            cboDenominacion.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Private Sub chkMarca_CheckedChanged(sender As Object, e As EventArgs) Handles chkMarca.CheckedChanged
        Flex.DataSource = Nothing
        Flex.DataBind()
        lblRegistro.Text = "0 Registro"
        If chkMarca.Checked = True Then
            cboMarca.Enabled = True
            cboMarca.SelectedValue = "< Seleccionar >"
        Else
            cboMarca.Enabled = False
            cboMarca.SelectedValue = "< Seleccionar >"
        End If
    End Sub
End Class
