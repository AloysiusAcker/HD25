Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_Personas
    Inherits System.Web.UI.Page
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Call Llenar_Grilla()
    End Sub
    Private Sub Llenar_Grilla()
        Dim obj As New clsInv_Listados
        lblError.Text = ""
        Try
            Flex.DataSource = obj.Lista_Persona(Session("Ruta_Emp"), Session("CodEmpresa"))
            Flex.DataBind()
            lblRegistro.Text = obj.Lista_Persona(Session("Ruta_Emp"), Session("CodEmpresa")).Rows.Count
            lblRegistro.Text = "Registros Encontrados : " & lblRegistro.Text
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

    Protected Sub Flex_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New clsInv_Listados
        If obj.Existe_PersonaFecha(Session("Ruta_Emp"), Session("CodEmpresa"), Flex.Rows(Index).Cells(1).Text, "").Rows.Count > 0 Then
            lblError.Text = "La Persona ya tiene una Fecha de Entrega."
            Exit Sub
        End If
        Flex.Enabled = False
        lblIngresarFecha.Visible = True
        lblError.Text = ""
        txtFecha.Text = ""
        lblEtiqueta.Text = "Ingresar Fecha de Entrega"
        If e.CommandName = "Editar" Then
            Try
                txtCodPer.Text = Flex.Rows(Index).Cells(1).Text
                txtTipoDoc.Text = Flex.Rows(Index).Cells(2).Text.Replace("&nbsp;", "")
                txtNroDoc.Text = Flex.Rows(Index).Cells(3).Text.Replace("&nbsp;", "")
                txtRazonSocial.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtNroPedido.Text = Flex.Rows(Index).Cells(7).Text.Replace("&nbsp;", "")
                txtDireccion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
                txtTelef.Text = Flex.Rows(Index).Cells(5).Text.Replace("&nbsp;", "")
            Catch ex As SqlException
                lblError.Text = ex.Message
            Catch ex As Exception
                lblError.Text = ex.Message
            Finally
            End Try
        End If
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        lblError.Text = ""
        If txtFecha.Text = "" Then
            lblError.Text = "Ingresar Fecha de Entrega"
            Exit Sub
        End If
        Dim CodPer As Double : CodPer = 0
        CodPer = txtCodPer.Text
        Dim obj As New clsInv_Listados
        Dim objInsUpdDel As New clsInv_InsUpdDel
        Dim FechaEntrega : FechaEntrega = ""
        FechaEntrega = Right(txtFecha.Text, 4) + Mid(txtFecha.Text, 4, 2) + Left(txtFecha.Text, 2)
        Try
            If obj.Existe_PersonaFecha(Session("Ruta_Emp"), Session("CodEmpresa"), CodPer, FechaEntrega).Rows.Count = 0 Then
                objInsUpdDel.Ins_PersonaFecha(Session("Ruta_Emp"), Session("CodEmpresa"), CodPer, FechaEntrega, txtNroPedido.Text.Trim)
            Else
                lblError.Text = "La Persona ya tiene una Fecha de Entrega."
                Exit Sub
            End If
        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
        Flex.Enabled = True
        lblIngresarFecha.Visible = False
        lblError.Text = ""
        btnListar_Click(sender, e)
    End Sub

    Protected Sub Flex_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Flex.Enabled = True
        lblIngresarFecha.Visible = False
        lblError.Text = ""
    End Sub
End Class
