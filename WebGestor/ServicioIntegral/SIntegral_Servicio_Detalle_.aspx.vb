Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Servicios_SIntegral_Servicio_Detalle_
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Listar_Detalle()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = False
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub Ficha_ActiveTabChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Ficha.ActiveTabChanged
        If Ficha.TabIndex = "0" Then
            Call LLenaComboItemTabEsp(cboSector, "", "", "TBESP_SER1", "TBESP_SER2", "TBESP_SER3", 1, Session("CodEmpresa"), Session("Ruta_Emp"))
            Call cboSector_SelectedIndexChanged(sender, e)
            cboTipo.Items.Add("< Seleccionar >") : cboTipo.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Private Sub Listar_Detalle()
        lblerror.Text = ""
        Dim obj As New clsSIntegral
        Dim psTipoEcon As Double = 0
        Dim psTipo As Double = 0
        Dim dt As DataTable
        Dim i As Long = 0
        Dim psNroServicio As Double = 0
        Try
            If cboSector.SelectedValue = "< Seleccionar >" Then
                psTipoEcon = 0
            Else
                psTipoEcon = cboSector.SelectedValue.Trim
            End If
            If cboTipo.SelectedValue = "< Seleccionar >" Then
                psTipo = 0
            Else
                psTipo = cboTipo.SelectedValue.Trim
            End If
            dt = obj.Listar_DetalleServicio(Session("Ruta_Emp"), Session("CodEmpresa"), psTipoEcon, psTipo)
            Flex.DataSource = dt
            Flex.DataBind()
            dt = Nothing
        Catch ex As SqlException
            lblerror.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            lblerror.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub
    Protected Sub cboSector_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSector.SelectedIndexChanged
        lblError.Visible = False
        cboTipo.Items.Clear()
        cboTipo.Enabled = False
        If cboSector.SelectedIndex = -1 Or cboSector.Items.Count = 0 Then Exit Sub
        If cboSector.SelectedValue = "< Seleccionar >" Then Exit Sub
        Call LLenaComboItemTabEsp(cboTipo, cboSector.SelectedValue.Trim, "", "TBESP_SER1", "TBESP_SER2", "TBESP_SER3", 2, Session("CodEmpresa"), Session("Ruta_Emp"))
        If cboSector.SelectedValue = "< Seleccionar >" Then
            cboTipo.Enabled = False
            cboTipo.Items.Add("< Seleccionar >") : cboTipo.SelectedValue = "< Seleccionar >"
        Else
            cboTipo.Enabled = True
        End If
    End Sub
End Class
