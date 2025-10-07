Imports System.Data
Imports System.Data.SqlClient
Imports WebGestor

Public Class Catalogo_PlacaIni_Prop
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ocultar_Visible(False)

            Llenar_Propietario()
            Session("CodEmpresa") = "0001"

        End If
    End Sub
    Protected Sub Ocultar_Visible(ByVal vf As Boolean)
        lblPropietario.Visible = vf
        drpProp.Visible = vf
        lblPlacaInicial.Visible = vf
        txtPlacaIn.Visible = vf
        txtPlacaFin.Visible = vf
        lblPlacaFin.Visible = vf
        btnGuardar.Visible = vf
        btnCancelar.Visible = vf

    End Sub
    Protected Sub Limpiar_Cajas()
        txtPlacaIn.Text = ""
        txtPlacaFin.Text = ""

    End Sub


    Protected Sub ListaPlaca()
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = objCn.strConexion
        dt = obj.Lista_Placa(psconexion, "0001")
        grvDatosPlaca.DataSource = dt
        grvDatosPlaca.DataBind()

    End Sub

    Protected Sub Llenar_Propietario()
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = objCn.strConexion
        dt = obj.Lista_Prop(psconexion)
        drpProp.DataSource = dt
        drpProp.DataValueField = "ALTIBI_CODIGO"
        drpProp.DataTextField = "ALTIBI_DESCRIPCION"
        drpProp.DataBind()
    End Sub

    Protected Sub btnListar_Click(sender As Object, e As EventArgs) Handles btnListar.Click

        ListaPlaca()
        Ocultar_Visible(False)

    End Sub

    Protected Sub grvDatosPlaca_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grvDatosPlaca.RowCommand

        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion
        If e.CommandName = "Editar" Then
            Ocultar_Visible(True)



            txtPlacaIn.Text = grvDatosPlaca.Rows(Index).Cells(4).Text
            txtPlacaFin.Text = grvDatosPlaca.Rows(Index).Cells(5).Text



            btnGuardar.Text = "Actualizar"
        End If
        If e.CommandName = "Eliminar" Then
            obj.EliminarPlaca_Prop(cn, grvDatosPlaca.Rows(Index).Cells(2).Text)
            ListaPlaca()
            Ocultar_Visible(False)
            Limpiar_Cajas()
        End If


    End Sub

    Protected Sub btnNuevo_Click(sender As Object, e As EventArgs) Handles btnNuevo.Click
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion

        Limpiar_Cajas()


        Ocultar_Visible(True)

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click


        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = objCn.strConexion

        Dim Prop As String = drpProp.SelectedValue.ToString
        Dim PlacInc As String = txtPlacaIn.Text
        Dim PlacFin As String = txtPlacaFin.Text


        If btnGuardar.Text = "Guardar" Then
            obj.RegistrarPlaca_Prop(psconexion, Prop, PlacInc, PlacFin)
            Ocultar_Visible(False)
            ListaPlaca()

        End If

        If btnGuardar.Text = "Actualizar" Then
            obj.ActualizaPlaca_Prop(psconexion, Prop, PlacInc, PlacFin)
            ListaPlaca()
        End If


    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Ocultar_Visible(False)
        Limpiar_Cajas()
    End Sub
End Class