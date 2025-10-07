Imports System.Data
Imports WebGestor
Public Class Inventario_Ingreso_Placas
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
            Ficha.ActiveTabIndex = 0
            Llenar_Combo_Tipo_Placa()
            Llenar_Combo_Almacen()
            'Ficha_ActiveTabChanged(sender, e)
        End If
    End Sub
    Protected Sub Listar_Recepcion()
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = DdlAlmacén.SelectedValue.ToString
        dt = obj.Lista_Recepcion(psconexion, codigo)
        GridView_Lista_Recepción.DataSource = dt
        GridView_Lista_Recepción.DataBind()
    End Sub
    Protected Sub Listar_Detalle_Recepción(ByVal pscod_recepcion As Double)
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Detalle_Recepcion(psconexion, pscod_recepcion)
        GridView_Detalle_Recepción.DataSource = dt
        GridView_Detalle_Recepción.DataBind()
        Dim check As CheckBox
        For i = 0 To GridView_Detalle_Recepción.Rows.Count - 1
            If GridView_Detalle_Recepción.Rows(i).Cells(5).Text <> "&nbsp;" And GridView_Detalle_Recepción.Rows(i).Cells(5).Text <> "" Then
                check = CType(GridView_Detalle_Recepción.Rows(i).Cells(0).FindControl("Check"), CheckBox)
                check.Checked = True
            End If
        Next
        ChkMarcarTodo.Checked = False
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Listar_Recepcion()
    End Sub

    Protected Sub GridView_Lista_Recepción_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView_Lista_Recepción.SelectedIndexChanged

    End Sub

    Private Sub GridView_Lista_Recepción_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView_Lista_Recepción.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Marcas
        Dim obj1 As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As Double = GridView_Lista_Recepción.Rows(Index).Cells(1).Text
        Dim dt As New DataTable
        If e.CommandName = "Detalle" Then
            Listar_Detalle_Recepción(codigo)
            Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
            txtNumRecepcion.Text = codigo
            Ficha.ActiveTabIndex = 1
            BtnIngreso.Enabled = True
            BtnCerrar.Enabled = True
            BtnCancelar.Visible = False
            BtnGenerar.Visible = False
        End If
    End Sub
    Protected Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        Ficha.ActiveTabIndex = 1 : Ficha.ActiveTab.Enabled = True
        Ficha.ActiveTabIndex = 0 : Ficha.ActiveTab.Enabled = True
    End Sub
    Protected Sub BtnIngreso_Click(sender As Object, e As EventArgs) Handles BtnIngreso.Click
        FramePlaca.Visible = True
        BtnCancelar.Visible = True
        BtnGenerar.Visible = True
        BtnIngreso.Enabled = False
        BtnCerrar.Enabled = False
        BtnBorrar.Enabled = False
        UltimaPlaca.Text = ""
        IniciarPlaca.Text = ""
        DdlTipoPlaca.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Llenar_Combo_Almacen()
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Listar_Combo_Almacen(psconexion)

        DdlAlmacén.DataSource = dt
        DdlAlmacén.DataValueField = "ALMACEN_CODIGO"
        DdlAlmacén.DataTextField = "ALMACEN_NOMBRE"

        DdlAlmacén.DataBind()

    End Sub
    Protected Sub Llenar_Combo_Tipo_Placa()
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Listar_Combo_Tipo_Placa(psconexion)

        DdlTipoPlaca.DataSource = dt
        DdlTipoPlaca.DataValueField = "ALTIBI_CODIGO"
        DdlTipoPlaca.DataTextField = "ALTIBI_DESCRIPCION"

        DdlTipoPlaca.DataBind()
        DdlTipoPlaca.Items.Add("< Seleccionar >")
        DdlTipoPlaca.SelectedValue = "< Seleccionar >"
    End Sub

    Private Sub DdlTipoPlaca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoPlaca.SelectedIndexChanged
        If DdlTipoPlaca.SelectedValue = "< Seleccionar >" Then Exit Sub
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim cn As String = Session("Ruta_Emp")
        Dim tabla As DataTable
        UltimaPlaca.Text = ""
        IniciarPlaca.Text = ""
        tabla = obj.Monstrar_Ultima_Placa(cn, DdlTipoPlaca.SelectedValue)
        If tabla.Rows.Count > 0 Then
            For Each dbRow As DataRow In tabla.Rows
                UltimaPlaca.Text = dbRow(0)
            Next dbRow
            IniciarPlaca.Text = (UltimaPlaca.Text + 1).ToString
        End If
    End Sub
    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        FramePlaca.Visible = False
        BtnCancelar.Visible = False
        BtnGenerar.Visible = False
        BtnIngreso.Enabled = True
        BtnCerrar.Enabled = True
        BtnBorrar.Enabled = True
        UltimaPlaca.Text = ""
        IniciarPlaca.Text = ""
        DdlTipoPlaca.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub ChkMarcarTodo_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMarcarTodo.CheckedChanged
        Dim Check As CheckBox
        If ChkMarcarTodo.Checked = True Then
            For i = 0 To GridView_Detalle_Recepción.Rows.Count - 1
                Check = CType(GridView_Detalle_Recepción.Rows(i).Cells(0).FindControl("Check"), CheckBox)
                Check.Checked = True
            Next
        End If
    End Sub

    Protected Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim Placa As Double = Convert.ToInt16(IniciarPlaca.Text)
        Dim dt As DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim numPlaca As String = IniciarPlaca.Text
        Dim tabla As DataTable
        Dim tipoArt As String = DdlTipoPlaca.SelectedValue
        Dim ulPlaca As String = UltimaPlaca.Text
        Dim check As CheckBox
        For i = 0 To GridView_Detalle_Recepción.Rows.Count - 1
            check = CType(GridView_Detalle_Recepción.Rows(i).Cells(0).FindControl("Check"), CheckBox)
            If check.Checked = True Then
VerificarPlaca:
                dt = obj.Verificar_Placa(psconexion, numPlaca)

                If dt.Rows.Count = 0 Then
                    dt = obj.Generar_Placa(psconexion, numPlaca, GridView_Detalle_Recepción.Rows(i).Cells(7).Text, GridView_Detalle_Recepción.Rows(i).Cells(6).Text)
                    numPlaca = numPlaca + 1
                Else
                    numPlaca = numPlaca + 1
                    dt = obj.Actualizar_Ultima_Placa(psconexion, numPlaca, tipoArt)
                    GoTo VerificarPlaca
                End If
            End If
        Next
        tabla = obj.Monstrar_Ultima_Placa(psconexion, DdlTipoPlaca.SelectedValue)
        If tabla.Rows.Count > 0 Then
            For Each dbRow As DataRow In tabla.Rows
                UltimaPlaca.Text = dbRow(0)
            Next dbRow
            IniciarPlaca.Text = numPlaca
        End If

        Listar_Detalle_Recepción(txtNumRecepcion.Text)
        BtnCancelar_Click(sender, e)
    End Sub

    Protected Sub BtnBorrar_Click(sender As Object, e As EventArgs) Handles BtnBorrar.Click
        Dim obj As New Cls_Placas
        Dim objCn As New Cls_Conexion
        Dim dt As DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psSerieNum As String = IniciarPlaca.Text
        Dim tipoArt As String = DdlTipoPlaca.SelectedValue
        Dim ulPlaca As String = UltimaPlaca.Text

        Dim check As CheckBox
        For i = 0 To GridView_Detalle_Recepción.Rows.Count - 1
            check = CType(GridView_Detalle_Recepción.Rows(i).Cells(0).FindControl("Check"), CheckBox)
            If check.Checked = True Then
                dt = obj.Borrar_Placa(psconexion, GridView_Detalle_Recepción.Rows(i).Cells(7).Text, GridView_Detalle_Recepción.Rows(i).Cells(6).Text)
            End If
        Next


        Listar_Detalle_Recepción(txtNumRecepcion.Text)
    End Sub
End Class