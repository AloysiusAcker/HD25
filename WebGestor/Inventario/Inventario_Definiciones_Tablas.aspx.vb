Imports System.Data
Imports WebGestor
Partial Class Inventario_Inventario_Definiciones_Tablas
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combo_Marca()
            Llenar_Combo_Proyecto()
            TabContainer1.ActiveTabIndex = 0
            TabContainer1.ActiveTab.Enabled = True
        End If
    End Sub

    '---- CÓDIGO DE LOS ALMACENES ----'


    '---- CÓDIGO DE LAS MARCAS ----'

    '-- LISTAR MARCAS --'
    Protected Sub Listar_Marcas()
        Dim obj As New Cls_Marcas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = objCn.strConexion
        Dim desc As String = TxtDescMarca.Text
        dt = obj.Lista_Marcas(psconexion, desc)
        GvListaMarcas.DataSource = dt
        GvListaMarcas.DataBind()
    End Sub

    '-- LLENAR COMBO MARCAS --'
    Protected Sub Llenar_Combo_Marca()
        Dim obj As New Cls_Marcas
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = objCn.strConexion
        dt = obj.Lista_Marcas(psconexion, "")
        DdlMarca.DataSource = dt
        DdlMarca.DataValueField = "ARTMAR_CODIGO"
        DdlMarca.DataTextField = "ARTMAR_DESCRIPCION"
        DdlMarca.DataBind()
    End Sub

    '-- OCULTAR O MOSTRAR LOS LABEL'S, TEXTBOX'S Y BUTTON'S DE LAS MARCAS --'
    Protected Sub Ocultar_Mostrar_Marcas(ByVal vf As Boolean)
        TxtCodigoMarca.Visible = vf
        TxtDescripcionMarca.Visible = vf
        LblCodigoMarca.Visible = vf
        LblDescripcionMarca.Visible = vf
        BtnCancelarMarca.Visible = vf
        BtnAgregarMarca.Visible = vf
    End Sub

    '-- LIMPIAR LOS TEXTBOX'S DE LAS MARCAS --'
    Protected Sub Limpiar_Cajas_Marca()
        TxtCodigoMarca.Text = ""
        TxtDescripcionMarca.Text = ""
    End Sub


    '---- CÓDIGO DE LOS PROPIETARIOS ----'

    '-- LIMPIAR LOS TEXTBOX'S DE LOS PROPIETARIOS --'
    Protected Sub Limpiar_Cajas_Propietarios()
        TxtCodigoPropietario.Text = ""
        TxtDescripcionPropietario.Text = ""
        TxtPlacabilidadPropietario.Text = ""

    End Sub

    '-- OCULTAR O MOSTRAR LOS LABEL'S, TEXTBOX'S Y BUTTON'S DE LOS PROPIETARIOS --'
    Protected Sub Ocultar_Mostrar_Propietarios(ByVal vf As Boolean)
        LblCodigoPropietario.Visible = vf
        LblDescripcionPropietario.Visible = vf
        LblPlacabilidadPropietario.Visible = vf
        TxtCodigoPropietario.Visible = vf
        TxtDescripcionPropietario.Visible = vf
        TxtPlacabilidadPropietario.Visible = vf
        BtnAgregarPropietario.Visible = vf
        BtnCancelarPropietario.Visible = vf
    End Sub

    '-- LISTAR PROPIETARIOS --'
    Protected Sub Listar_Propietarios()
        Dim obj As New Cls_Propietario
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = objCn.strConexion
        Dim descrip As String = TxtDescripcionPropietario.Text
        dt = obj.Lista_PropXDesc(psconexion, descrip)
        GvListaPropietario.DataSource = dt
        GvListaPropietario.DataBind()
    End Sub

    Protected Sub BtnListarMarca_Click(sender As Object, e As EventArgs) Handles BtnListarMarca.Click
        Listar_Marcas()
    End Sub

    Protected Sub BtnNuevaMarca_Click(sender As Object, e As EventArgs) Handles BtnNuevaMarca.Click
        Dim obj As New Cls_Marcas
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion

        Ocultar_Mostrar_Marcas(True)
        Limpiar_Cajas_Marca()

        TxtCodigoMarca.Text = obj.CodigoMarca(cn)
        BtnAgregarMarca.Text = "Agregar"
    End Sub

    Protected Sub BtnGrabarMarca_Click(sender As Object, e As EventArgs) Handles BtnAgregarMarca.Click
        Dim obj As New Cls_Marcas
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = objCn.strConexion
        Dim codigo As String = TxtCodigoMarca.Text
        Dim descripcion As String = TxtDescripcionMarca.Text

        If BtnAgregarMarca.Text = "Agregar" Then
            obj.Registra_Marca(psconexion, codigo, descripcion)
            Ocultar_Mostrar_Marcas(False)
            Listar_Marcas()
        End If
        If BtnAgregarMarca.Text = "Actualizar" Then
            obj.Actualiza_Marca(psconexion, codigo, descripcion)
            Ocultar_Mostrar_Marcas(False)
            Listar_Marcas()
        End If
    End Sub

    Protected Sub BtnCancelarMarca_Click(sender As Object, e As EventArgs) Handles BtnCancelarMarca.Click
        Limpiar_Cajas_Marca()
        Ocultar_Mostrar_Marcas(False)
    End Sub

    Protected Sub GvListaMarcas_SelectedIndexChanged(sender As Object, e As GridViewCommandEventArgs) Handles GvListaMarcas.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Marcas
        Dim obj1 As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = objCn.strConexion
        Dim codigo As String = GvListaMarcas.Rows(Index).Cells(3).Text
        Dim dt As New DataTable
        If e.CommandName = "DetalleMarca" Then
            dt = obj1.Lista_Marcas_Modelo(psconexion, codigo)
            GvListaModelo.DataSource = dt
            GvListaModelo.DataBind()
            TabContainer1.ActiveTabIndex = 2
            TabContainer1.Enabled = True
            BtnNuevoModelo.Visible = True
            DdlMarca.SelectedValue = codigo
        End If

        If e.CommandName = "EliminaMarca" Then
            obj.Eliminar_Marca(psconexion, GvListaMarcas.Rows(Index).Cells(3).Text)
            Listar_Marcas()
            Ocultar_Mostrar_Marcas(False)
            Limpiar_Cajas_Marca()
        End If

        If e.CommandName = "EditaMarca" Then
            Ocultar_Mostrar_Marcas(True)
            TxtCodigoMarca.Text = GvListaMarcas.Rows(Index).Cells(3).Text
            TxtDescripcionMarca.Text = GvListaMarcas.Rows(Index).Cells(4).Text
            BtnAgregarMarca.Text = "Actualizar"
        End If
    End Sub



    '--------------------- MODELO ----------------------'
    Protected Sub BtnNuevoModelo_Click(sender As Object, e As EventArgs) Handles BtnNuevoModelo.Click
        Ocultar_Mostrar_Modelo(True)
        BtnAgregarModelo.Text = "Agregar"
        TxtDescripcionModelo.Value = ""
    End Sub

    Protected Sub Ocultar_Mostrar_Modelo(ByVal vf As Boolean)
        TxtDescripcionModelo.Visible = vf
        LblDescripcionModelo.Visible = vf
        BtnCancelarModelo.Visible = vf
        BtnAgregarModelo.Visible = vf
    End Sub

    Protected Sub BtnGrabarModelo_Click(sender As Object, e As EventArgs) Handles BtnAgregarModelo.Click
        Dim obj As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = objCn.strConexion
        Dim codMarca As String = DdlMarca.SelectedValue.ToString
        Dim codMod As String = obj.CodigoModelo(psconexion, codMarca)
        Dim descripcion As String = TxtDescripcionModelo.Value.ToString
        Dim dt As DataTable

        If BtnAgregarModelo.Text = "Agregar" Then
            obj.Agregar_Marcas_Modelo(psconexion, codMod, codMarca, descripcion)
            Ocultar_Mostrar_Modelo(False)


            dt = obj.Lista_Marcas_Modelo(psconexion, codMarca)
            GvListaModelo.DataSource = dt
            GvListaModelo.DataBind()
            DdlMarca.Enabled = True
        End If
        If BtnAgregarModelo.Text = "Actualizar" Then
            obj.Actualizar_Marcas_Modelo(psconexion, codigoModelo.Value, codMarca, descripcion)
            Ocultar_Mostrar_Modelo(False)

            dt = obj.Lista_Marcas_Modelo(psconexion, codMarca)
            GvListaModelo.DataSource = dt
            GvListaModelo.DataBind()
            DdlMarca.Enabled = True
        End If
    End Sub

    Protected Sub BtnCancelarModelo_Click(sender As Object, e As EventArgs) Handles BtnCancelarModelo.Click
        Ocultar_Mostrar_Modelo(False)
        DdlMarca.Enabled = True
    End Sub

    Protected Sub GvListaModelo_SelectedIndexChanged(sender As Object, e As GridViewCommandEventArgs) Handles GvListaModelo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion
        Dim dt As DataTable
        If e.CommandName = "EditaModelo" Then
            Ocultar_Mostrar_Modelo(True)
            DdlMarca.SelectedValue = GvListaModelo.Rows(Index).Cells(2).Text
            codigoModelo.Value = GvListaModelo.Rows(Index).Cells(3).Text
            TxtDescripcionModelo.Value = GvListaModelo.Rows(Index).Cells(4).Text
            DdlMarca.Enabled = False
            BtnAgregarModelo.Text = "Actualizar"
        End If
        If e.CommandName = "EliminaModelo" Then
            obj.Eliminar_Marcas_Modelo(cn, GvListaModelo.Rows(Index).Cells(3).Text, GvListaModelo.Rows(Index).Cells(2).Text)
            dt = obj.Lista_Marcas_Modelo(cn, GvListaModelo.Rows(Index).Cells(2).Text)
            GvListaModelo.DataSource = dt
            GvListaModelo.DataBind()
            Ocultar_Mostrar_Modelo(False)
        End If
    End Sub

    Protected Sub DdlMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlMarca.SelectedIndexChanged
        If DdlMarca.SelectedValue > 0 Then
            TxtDescripcionModelo.Value = DdlMarca.SelectedValue.ToString
        End If
    End Sub


    '--------------------- PROPIETARIO ---------------------'
    Protected Sub BtnListarPropietario_Click(sender As Object, e As EventArgs) Handles BtnListarPropietario.Click
        Listar_Propietarios()
    End Sub

    Protected Sub BtnNuevoPropietario_Click(sender As Object, e As EventArgs) Handles BtnNuevoPropietario.Click
        Dim obj As New Cls_Propietario
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion
        Limpiar_Cajas_Propietarios()
        TxtCodigoPropietario.Text = obj.Codigo2(cn)
        Ocultar_Mostrar_Propietarios(True)
        BtnAgregarPropietario.Text = "Agregar"
    End Sub

    Protected Sub BtnAgregarPropietario_Click(sender As Object, e As EventArgs) Handles BtnAgregarPropietario.Click
        Dim obj As New Cls_Propietario
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = objCn.strConexion
        Dim codigo As String = TxtCodigoPropietario.Text
        Dim descripcion As String = TxtDescripcionPropietario.Text
        Dim placabilidad As String = TxtPlacabilidadPropietario.Text

        If BtnAgregarPropietario.Text = "Agregar" Then
            obj.RegistrarPropietario(psconexion, codigo, descripcion, placabilidad)
            Ocultar_Mostrar_Propietarios(False)
            Listar_Propietarios()
        End If
        If BtnAgregarPropietario.Text = "Actualizar" Then
            obj.ActualizaPropietario(psconexion, codigo, descripcion, placabilidad)
            Ocultar_Mostrar_Propietarios(False)
            Listar_Propietarios()
        End If
    End Sub

    Protected Sub BtnCancelarPropietario_Click(sender As Object, e As EventArgs) Handles BtnCancelarPropietario.Click
        Ocultar_Mostrar_Propietarios(False)
        Limpiar_Cajas_Propietarios()
    End Sub

    Protected Sub GvListaPropietario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaPropietario.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Propietario
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion
        If e.CommandName = "EditaPropietario" Then
            Ocultar_Mostrar_Propietarios(True)
            TxtCodigoPropietario.Text = GvListaPropietario.Rows(Index).Cells(2).Text
            TxtDescripcionPropietario.Text = GvListaPropietario.Rows(Index).Cells(3).Text
            TxtPlacabilidadPropietario.Text = GvListaPropietario.Rows(Index).Cells(4).Text
            BtnAgregarPropietario.Text = "Actualizar"
        End If
        If e.CommandName = "Eliminar" Then
            obj.EliminaPropietario(cn, GvListaPropietario.Rows(Index).Cells(2).Text)
            Listar_Propietarios()
            Ocultar_Mostrar_Propietarios(False)
            Limpiar_Cajas_Propietarios()
        End If
    End Sub





    Protected Sub Listar_Proyectos()
        Dim obj As New Cls_Proyectos
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = objCn.strConexion
        Dim año As String = DdlAño.Text
        dt = obj.Lista_Proyectos(psconexion, año)
        GridView_Proyectos.DataSource = dt
        GridView_Proyectos.DataBind()
    End Sub
    Protected Sub BtnListar_Proyectos_Click(sender As Object, e As EventArgs) Handles btnListar_Proyectos.Click
        Listar_Proyectos()
    End Sub

    Protected Sub Limpiar_Cajas()
        txtCodigo_Proy.Text = ""
        txtDescripcion_Proy.Text = ""
    End Sub
    Protected Sub Ocultar_Visible(ByVal vf As Boolean)
        DdlAñoNuevo.Visible = vf
        txtCodigo_Proy.Visible = vf
        txtDescripcion_Proy.Visible = vf
        LblAño_Proy.Visible = vf
        LblCodigo_Proy.Visible = vf
        LblDescripción_Proy.Visible = vf
        BtnCancelar_Proyectos.Visible = vf
        BtnGrabar_Proyectos.Visible = vf
    End Sub
    Protected Sub BtnNuevo_Proyectos_Click(sender As Object, e As EventArgs)
        Dim obj As New Cls_Proyectos
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion

        Limpiar_Cajas()

        txtCodigo_Proy.Text = obj.CodigoProy(cn)
        Ocultar_Visible(True)
        BtnGrabar_Proyectos.Text = "Grabar"

    End Sub

    Protected Sub BtnGrabar_Proyectos_Click(sender As Object, e As EventArgs) Handles BtnGrabar_Proyectos.Click
        Dim obj As New Cls_Proyectos
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = objCn.strConexion
        Dim año As String = DdlAñoNuevo.Text
        Dim codigo As String = txtCodigo_Proy.Text
        Dim descripcion As String = txtDescripcion_Proy.Text

        If BtnGrabar_Proyectos.Text = "Grabar" Then
            obj.Registra_Proyecto(psconexion, año, codigo, descripcion)
            Ocultar_Visible(False)
            Listar_Proyectos()
        End If
        If BtnGrabar_Proyectos.Text = "Actualizar" Then
            obj.Actualiza_Proyecto(psconexion, año, codigo, descripcion)
            Listar_Proyectos()
        End If
    End Sub

    Protected Sub BtnCancelar_Proyectos_Click(sender As Object, e As EventArgs) Handles BtnCancelar_Proyectos.Click
        Ocultar_Visible(False)
        Limpiar_Cajas()
    End Sub

    Protected Sub GridView1_Proyectos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView_Proyectos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Proyectos
        Dim objCn As New Cls_Conexion
        Dim cn As String = objCn.strConexion
        If e.CommandName = "Editar" Then
            Ocultar_Visible(True)
            DdlAñoNuevo.Text = GridView_Proyectos.Rows(Index).Cells(3).Text
            txtCodigo_Proy.Text = GridView_Proyectos.Rows(Index).Cells(2).Text
            txtDescripcion_Proy.Text = GridView_Proyectos.Rows(Index).Cells(4).Text
            BtnGrabar_Proyectos.Text = "Actualizar"
        End If
        If e.CommandName = "Eliminar" Then
            obj.Eliminar_Proyecto(cn, GridView_Proyectos.Rows(Index).Cells(2).Text, GridView_Proyectos.Rows(Index).Cells(3).Text)
            Listar_Proyectos()
            Ocultar_Visible(False)
            Limpiar_Cajas()
        End If
    End Sub

    Protected Sub CboAño_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlAño.SelectedIndexChanged

    End Sub

    Protected Sub Llenar_Combo_Proyecto()
        Dim obj As New Cls_Proyectos
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = objCn.strConexion
        dt = obj.Listar_Combo(psconexion)
        DdlAño.DataSource = dt
        DdlAño.DataMember = "AÑO"
        DdlAño.DataTextField = "AÑO"
        DdlAñoNuevo.DataSource = dt
        DdlAñoNuevo.DataMember = "AÑO"
        DdlAñoNuevo.DataTextField = "AÑO"
        DdlAñoNuevo.DataBind()

        DdlAño.DataBind()
    End Sub

    Protected Sub BtnNuevoProyecto_Click(sender As Object, e As EventArgs) Handles btnNuevo_Proyectos.Click
        Ocultar_Visible(True)
    End Sub

End Class
