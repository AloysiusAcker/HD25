Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class Inventario_Ubicacion
    Inherits System.Web.UI.Page
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combo()
            LblRegistro.Text = ""
            Call LLenar_TipoaArticulo()
            Dim dt As New DataTable
            dt = Nothing
            GvListaUbicacion.DataSource = dt
            GvListaUbicacion.DataBind()
            Session("BusViene") = False
            rbBusTodos.Checked = True
        End If
    End Sub
    Private Sub LLenar_TipoaArticulo()
        Dim dt As New DataTable
        dt = Nothing
        dt = objCat.Lista_Tipo(Session("Ruta_Emp"))
        DdlTipoBA.DataSource = dt
        DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipoBA.DataBind()
        DdlTipoBA.Items.Add("< Seleccionar >")
        DdlTipoBA.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        DdlTipoBA.SelectedValue = "< SELECCIONAR >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
    End Sub

    Protected Sub Listar_Inventario_Ubicacion()
        Dim obj As New Cls_Inventario_Ubicacion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodInventario As Double = 0
        Dim pdCodInvUbica As Double = 0

        If ddlBusInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInventario = Nz(ddlBusInventario.SelectedValue)
            If lblBusUbicaCodInv.Text <> "" Then
                pdCodInvUbica = Nz(lblBusUbicaCodInv.Text)
            End If
            dt = obj.Inventario_ListaUbicaciones_xInventario(Session("Ruta_Emp"), pdCodInventario, pdCodInvUbica)
        Else
            dt = obj.Lista_Inventario_Ubicacion(psconexion)
        End If
        GvListaUbicacion.DataSource = dt
        GvListaUbicacion.DataBind()
        LblRegistro.Text = ""
        If dt.Rows.Count = 0 Then
            LblRegistro.Text = "No hay registro."
        ElseIf dt.Rows.Count = 1 Then
            LblRegistro.Text = "Hay 1 registro."
        ElseIf dt.Rows.Count > 1 Then
            LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
        End If
    End Sub

    Protected Sub Llenar_Combo()
        Dim obj As New Cls_Inventario_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo(psconexion)
        DdlInventario.DataSource = dt
        DdlInventario.DataValueField = "INVENT_CODIGO"
        DdlInventario.DataTextField = "INVENT_DESC"
        DdlInventario.DataBind()
        DdlInventario.Items.Add("< Seleccionar >")
        DdlInventario.SelectedValue = "< Seleccionar >"

        ddlBusInventario.DataSource = dt
        ddlBusInventario.DataValueField = "INVENT_CODIGO"
        ddlBusInventario.DataTextField = "INVENT_DESC"
        ddlBusInventario.DataBind()
        ddlBusInventario.Items.Add("< Seleccionar >")
        ddlBusInventario.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub Cargar_Equipos_Seriados(ByVal inventario As String, ByVal ubicacion As String, ByVal tipo As String, ByVal articulo As String)
        Dim obj As New Cls_Inventario_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        obj.Cargar_Equipos_Seriados(psconexion, inventario, ubicacion, tipo, articulo)
    End Sub

    Protected Sub Cargar_Equipos_SeriadosU(ByVal inventario As String, ByVal ubicacion As String, ByVal articulo As String)
        Dim obj As New Cls_Inventario_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim psconexion As String = Session("Ruta_Emp")
        obj.Cargar_Equipos_SeriadosU(psconexion, inventario, ubicacion, articulo)
    End Sub

    Protected Sub Cargar_Accesorios(ByVal inventario As Double, ByVal ubicacion As Double, ByVal tipo As String)
        Dim obj As New Cls_Inventario_Ubicacion
        Dim psconexion As String = Session("Ruta_Emp")
        obj.Cargar_Accesorios(psconexion, inventario, ubicacion, tipo)
    End Sub

    Protected Sub Ocultar_Mostrar_Ubicaciones_Inventario(ByVal vf As Boolean)
        LblInventario.Visible = vf
        DdlInventario.Visible = vf
        BtnAgregar.Visible = vf
        LblResponsable.Visible = vf
        TxtResponsable.Visible = vf
        BtnCancelar.Visible = vf
        LblUbicacion.Visible = vf
        RBAlmacen.Visible = vf
        RBCentroC.Visible = vf
        RBUbicaciones.Visible = vf
        LblCodigo.Visible = vf
        TxtCodigo.Visible = vf
        BtnBusca.Visible = vf
        TxtDescripcion.Visible = vf
        LlbEtiqFecha.Visible = vf
        TxtFecha.Visible = vf
        TxtFechaCierre.Visible = vf
        LlbEtiqFechaC.Visible = vf

        Costotitulo.Visible = False
        Costos.Visible = False
        CostosBoton.Visible = False
        BtnCostos.Visible = False
        DdlInventario.Enabled = True
        BtnBusca.Enabled = True
        TxtCodigo.ReadOnly = False
        TxtDescripcion.ReadOnly = False
        TxtResponsable.ReadOnly = False
        BtnPersonal.Visible = False

    End Sub

    Protected Sub Limpiar_Cajas()
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        TxtResponsable.Text = ""
        DdlInventario.SelectedIndex = 0
        RBAlmacen.Checked = True
    End Sub

    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        Session("BusViene") = False
    End Sub

    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Listar_Inventario_Ubicacion()
    End Sub

    Protected Sub BtnIngresaUbic_Click(sender As Object, e As EventArgs) Handles BtnIngresaUbic.Click
        Ocultar_Mostrar_Ubicaciones_Inventario(True)
        Ingresardatos.Visible = True
        BtnAgregar.Text = "Agregar"
    End Sub

    Protected Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        ElseIf RBUbicaciones.Checked Then
            TituloPopup.Text = "Búsqueda Ubicaciones"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Ocultar_Mostrar_Ubicaciones_Inventario(False)

        Ingresardatos.Visible = False
        Limpiar_Cajas()
        Personal.Visible = False
        Costos.Visible = False
        CostosBoton.Visible = False
        Dim dt As New DataTable
        dt = Nothing
        GvPersonal.DataSource = dt
        GvPersonal.DataBind()
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New Cls_Inventario_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim objBusUbica As New Cls_Inventario_Verificacion
        Dim objMa As New Cls_Marcas
        Dim objMo As New Cls_Modelo
        Dim dt As New DataTable
        Dim dtM As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psCodInv As Double = 0
        Dim pscodInterno As String = ""
        If ddlBusInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = Nz(ddlBusInventario.SelectedValue)
        End If
        Dim codigo As Double = 0
        If BuscarCodigo.Value <> "" Then
            codigo = Nz(BuscarCodigo.Value)
            pscodInterno = BuscarCodigo.Value.ToString
        End If
        Dim descripcion As String = BuscarDescripcion.Value.ToString
        Dim codMarca As String = LblCodMarcaBA.Text.ToString

        If TituloPopup.Text = "Búsqueda Almacén" Then
            If Session("BusViene") = True Then
                dt = objBusUbica.Listar_Almacenes_Inventario_Verificacion(Session("Ruta_Emp"), psCodInv, codigo, descripcion)
            Else
                dt = obj.Lista_Almacenes_Inventario(psconexion, codigo, descripcion)
            End If
        ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
            If Session("BusViene") = True Then
                dt = objBusUbica.Listar_CentroC_Inventario_Verificacion(Session("Ruta_Emp"), psCodInv, pscodInterno, descripcion)
            Else
                dt = obj.Lista_CentroC_Inventario(psconexion, pscodInterno, descripcion)
            End If
        ElseIf TituloPopup.Text = "Búsqueda Ubicaciones" Then
            dt = obj.Lista_Ubicaciones_Inventario(psconexion, pscodInterno, descripcion)
        ElseIf TituloPopup.Text = "Busca Marca" Then
            dtM = objMa.Buscar_Marca(psconexion, pscodInterno, descripcion)
        ElseIf TituloPopup.Text = "Busca Modelo" Then
            dtM = objMo.Buscar_Modelo(psconexion, pscodInterno, descripcion, codMarca)
        End If

        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()




        GvBusquedaM.DataSource = dtM
        GvBusquedaM.DataBind()
    End Sub

    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            If Session("BusViene") = True Then
                txtBusUbicaCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
                txtBusUbicaNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                lblBusUbicaCod.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                lblBusUbicaCodInv.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('hide');", True)
            Else
                TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
                TxtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                TxtCodUbica.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                If TituloPopup.Text = "Búsqueda Almacén" Or TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
                    TxtCodInventarioUbicacion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                End If
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('hide');", True)
            End If
            Limpiar_Cajas_Popup()
        End If
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        If TituloPopup.Text = "Busca Marca" Or TituloPopup.Text = "Busca Modelo" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()
    End Sub

    Protected Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click
        Dim obj As New Cls_Inventario_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim codigo As Double = 0
        Dim inventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            inventario = Nz(DdlInventario.SelectedValue.ToString)
        End If
        Dim tipo As String = ""
        Dim ubicacion As Double = 0
        If TxtCodUbica.Text <> "" Then
            ubicacion = Nz(TxtCodUbica.Text.ToString)
        End If
        Dim responsable As String = TxtResponsable.Text.ToString
        Dim psconexion As String = Session("Ruta_Emp")
        Dim dt As DataTable
        Dim psFechaPrograma As String = ""
        Dim psFechaInicia As String = ""
        Dim psFechaCierre As String = ""
        Dim pdCodInvUbi As Double = 0
        pdCodInvUbi = Nz(TxtCodInventarioUbicacion.Text)
        If TxtFecha.Text <> "" Then
            psFechaPrograma = Mid(TxtFecha.Text, 7, 4) & Mid(TxtFecha.Text, 4, 2) & Left(TxtFecha.Text, 4)
        End If
        If TxtFechaCierre.Text <> "" Then
            psFechaCierre = Mid(TxtFechaCierre.Text, 7, 4) & Mid(TxtFechaCierre.Text, 4, 2) & Left(TxtFechaCierre.Text, 4)
        End If

        If BtnAgregar.Text = "Agregar" Then
            codigo = Nz(obj.Codigo(psconexion))
            If RBAlmacen.Checked Then
                tipo = "1"
            ElseIf RBCentroC.Checked Then
                tipo = "2"
            ElseIf RBUbicaciones.Checked Then
                tipo = "9"
            End If
            If inventario = 0 Or ubicacion = 0 Or responsable = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Llene todos los campos');", True)
            Else
                dt = obj.Agregar_Inventario_Ubicacion(psconexion, codigo, inventario, tipo, ubicacion, responsable, psFechaPrograma)
                If dt Is Nothing Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Datos duplicados');", True)
                End If
            End If
        ElseIf BtnAgregar.Text = "Modificar" Then
            dt = obj.Inventario_Ubicacion_IngFechas(Session("Ruta_Emp"), pdCodInvUbi, psFechaPrograma, psFechaInicia, psFechaCierre)
        End If
        Listar_Inventario_Ubicacion()
        Ocultar_Mostrar_Ubicaciones_Inventario(False)
        Limpiar_Cajas()

    End Sub

    Private Sub GvListaUbicacion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaUbicacion.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Inventario_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        LblRegistroDetalle.Text = ""
        Dim psconexion As String = Session("Ruta_Emp")
        Dim inventario As Double = 0
        If Replace(GvListaUbicacion.Rows(Index).Cells(10).Text, "&nbsp;", "") <> "" Then
            inventario = Nz(GvListaUbicacion.Rows(Index).Cells(10).Text)
        End If
        Dim tipo As String = GvListaUbicacion.Rows(Index).Cells(6).Text
        Dim ubicacion As Double = 0
        If Replace(GvListaUbicacion.Rows(Index).Cells(11).Text, "&nbsp;", "") <> "" Then
            ubicacion = Nz(GvListaUbicacion.Rows(Index).Cells(11).Text)
        End If
        Dim articulo As String = TxtCodArticulo.Text.ToString

        If tipo = "Almac&#233;n" Then
            tipo = "1"
        ElseIf tipo = "Centro Costo" Then
            tipo = "2"
        ElseIf tipo = "Ubicaci&#243;n" Then
            tipo = "9"
        End If

        If e.CommandName = "DetalleInventario" Then
            TxtCodUbica.Text = inventario
            dt = obj.Lista_Inventario_Ubicacion_Detalle(psconexion, inventario)
            GvListaDetalleInventario.DataSource = dt
            GvListaDetalleInventario.DataBind()
            If dt.Rows.Count > 1 Then
                LblRegistroDetalle.Text = "Hay " & dt.Rows.Count & " bienes."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistroDetalle.Text = "Hay 1 bien."
            ElseIf dt.Rows.Count = 0 Then
                LblRegistroDetalle.Text = "No hay bienes."
            End If
        ElseIf e.CommandName = "EliminaInventario" Then
            dt = obj.Elimina_Inventario_Ubicacion(psconexion, inventario)
            If dt.Rows.Count > 0 Then
                Dim dbRow As DataRow = dt.Rows(0)
                If dbRow(0).ToString.Equals("1") Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se puede eliminar la ubicación');", True)
                ElseIf dbRow(0).ToString.Equals("2") Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ubicación eliminada');", True)
                    Listar_Inventario_Ubicacion()
                End If
            End If
        ElseIf e.CommandName = "CargarInventario" Then
            If GvListaUbicacion.Rows(Index).Cells(8).Text = "Generado" Then
                dt = obj.Lista_Inventario_Ubicacion_Detalle(psconexion, inventario)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
                TxtCodInventarioUbicacion.Text = Index
            ElseIf GvListaUbicacion.Rows(Index).Cells(8).Text = "No Generado" Then
                If articulo = "" Then articulo = "%"
                If tipo = "1" Or tipo = "2" Then
                    Cargar_Equipos_Seriados(inventario, ubicacion, tipo, articulo)
                    Cargar_Accesorios(inventario, ubicacion, tipo)
                ElseIf tipo = "9" Then
                    Cargar_Equipos_SeriadosU(inventario, ubicacion, articulo)
                End If
                obj.Actualizar_Inventario_Ubicacion(psconexion, inventario, "1")
                Listar_Inventario_Ubicacion()
                GvListaDetalleInventario.DataSource = dt
                GvListaDetalleInventario.DataBind()
                BtnBuscarArticulo.Enabled = False
                TxtCodArticulo.Text = ""
                TxtDescArticulo.Text = ""
            End If
        End If
        If e.CommandName = "Editar" Then
            Ocultar_Mostrar_Ubicaciones_Inventario(True)
            BtnAgregar.Text = "Modificar"
            Ingresardatos.Visible = True
            TxtResponsable.Text = ""
            TxtCodigo.Text = ""
            TxtDescripcion.Text = ""
            TxtCodUbica.Text = ""
            TxtCodInventarioUbicacion.Text = ""
            TxtFecha.Text = ""
            TxtFechaCierre.Text = ""
            DdlInventario.Enabled = False
            BtnPersonal.Visible = True
            BtnCostos.Visible = True
            BtnBusca.Enabled = False
            TxtCodigo.ReadOnly = True
            TxtDescripcion.ReadOnly = True
            TxtResponsable.ReadOnly = True
            Dim pdCodInventario As Double = 0
            pdCodInventario = ddlBusInventario.SelectedValue
            dt = obj.Inventario_ListaUbicaciones_xInventario(Session("Ruta_Emp"), pdCodInventario, inventario)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    TxtCodInventarioUbicacion.Text = Llenar_Ceros(Nu(dr("INVENTUBIC_CODIGO")), 6)
                    TxtCodUbica.Text = Nu(dr("INVENTUBIC_UBIC_CODIGO"))
                    TxtDescripcion.Text = Nu(dr("Descripcion_Ubi"))
                    TxtResponsable.Text = Nu(dr("INVENTUBIC_RESPONSABLE"))
                    TxtFecha.Text = FormatoFecha(Nu(dr("INVENTUBIC_FECHA_PROGRAMACION")))
                    TxtFechaCierre.Text = FormatoFecha(Nu(dr("INVENTUBIC_FECHA_CIERRE")))
                    TxtCodigo.Text = Nu(dr("Cod_Interno"))
                    DdlInventario.SelectedValue = Nu(dr("INVENTUBIC_NRO"))
                    If Nu(dr("INVENTUBIC_UBIC_TIPO")) = "1" Then RBAlmacen.Checked = True : RBCentroC.Checked = False : RBUbicaciones.Checked = False
                    If Nu(dr("INVENTUBIC_UBIC_TIPO")) = "2" Then RBCentroC.Checked = True : RBAlmacen.Checked = False : RBUbicaciones.Checked = False
                    If Nu(dr("INVENTUBIC_UBIC_TIPO")) = "9" Then RBUbicaciones.Checked = True : RBCentroC.Checked = False : RBAlmacen.Checked = False
                Next
            End If
        End If

        If e.CommandName = "Cerrar" Then

            Dim objUbic As New Cls_Inventario_Ubicacion
            Dim pdCodInvUbi As Double = 0
            Try
                pdCodInvUbi = inventario
                objUbic.Cierre_Inventario_xUbicacion_(Session("Ruta_Emp"), pdCodInvUbi, FechaActual)
                BtnListar_Click(sender, e)
            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
            End Try
        End If

    End Sub

    Protected Sub CbArticuloCargar_CheckedChanged(sender As Object, e As EventArgs) Handles CbArticuloCargar.CheckedChanged
        If CbArticuloCargar.Checked = True Then
            BtnBuscarArticulo.Enabled = True
        Else
            BtnBuscarArticulo.Enabled = False
            TxtCodArticulo.Text = ""
            TxtDescArticulo.Text = ""
        End If
    End Sub

    Private Sub BtnSi_Click(sender As Object, e As EventArgs) Handles BtnSi.Click
        Dim obj As New Cls_Inventario_Ubicacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim fila As Integer = Convert.ToInt32(TxtCodInventarioUbicacion.Text)
        Dim psconexion As String = Session("Ruta_Emp")
        Dim inventario As Double = 0
        If Replace(GvListaUbicacion.Rows(fila).Cells(10).Text, "&nbsp;", "") <> "" Then
            inventario = Nz(GvListaUbicacion.Rows(fila).Cells(10).Text)
        End If
        Dim tipo As String = GvListaUbicacion.Rows(fila).Cells(6).Text
        Dim ubicacion As Double = 0
        If Replace(GvListaUbicacion.Rows(fila).Cells(11).Text, "&nbsp;", "") <> "" Then
            ubicacion = Nz(GvListaUbicacion.Rows(fila).Cells(11).Text)
        End If
        Dim articulo As String = TxtCodArticulo.Text.ToString

        If tipo = "Almac&#233;n" Then
            tipo = "1"
        ElseIf tipo = "Centro Costo" Then
            tipo = "2"
        ElseIf tipo = "Ubicaci&#243;n" Then
            tipo = "9"
        End If
        If articulo = "" Then articulo = "%"
        obj.Eliminar_Detalle_Ubicacion(psconexion, inventario)
        If tipo = "1" Or tipo = "2" Then
            Cargar_Equipos_Seriados(inventario, ubicacion, tipo, articulo)
            Cargar_Accesorios(inventario, ubicacion, tipo)
        ElseIf tipo = "9" Then
            Cargar_Equipos_SeriadosU(inventario, ubicacion, articulo)
        End If
        obj.Actualizar_Inventario_Ubicacion(psconexion, inventario, "1")
        Listar_Inventario_Ubicacion()
        GvListaDetalleInventario.DataSource = dt
        GvListaDetalleInventario.DataBind()
        BtnBuscarArticulo.Enabled = False
        TxtCodArticulo.Text = ""
        TxtDescArticulo.Text = ""

        TxtCodInventarioUbicacion.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('hide');", True)
    End Sub

    Private Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click
        TxtCodInventarioUbicacion.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('hide');", True)
    End Sub


    Private Sub GvBusquedaM_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusquedaM.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And TituloPopup.Text = "Busca Marca" Then
            TxtMarcaBA.Value = GvBusquedaM.Rows(Index).Cells(2).Text
            LblCodMarcaBA.Text = GvBusquedaM.Rows(Index).Cells(3).Text
        ElseIf e.CommandName = "Aceptar" And TituloPopup.Text = "Busca Modelo" Then
            TxtModeloBA.Value = GvBusquedaM.Rows(Index).Cells(2).Text
            LblCodModeloBA.Text = GvBusquedaM.Rows(Index).Cells(3).Text
        End If
        Limpiar_Cajas_Popup()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnBuscaMarcaBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarcaBA.Click
        TituloPopup.Text = "Busca Marca"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#myModal').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnBuscaModeloBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaModeloBA.Click
        TituloPopup.Text = "Busca Modelo"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#myModal').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub PopularRootLevel()
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))

        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO, CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion,  " _
                                      & " (SELECT count(clas_codigo) frOM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL1=c1.CLAS_CODIGO and clas_cod_nivel = 2 ) as CountHijos " _
                                      & " FROM TBINV_ARTICULO_CLASIFICACION c1  WHERE CLAS_COD_NIVEL=1 ORDER BY CLAS_NUMERACION", objConn)
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()

        da.Fill(dt)
        NodosPopulares(dt, TrvClasificacion.Nodes)
    End Sub


    Private Sub NodosPopulares(ByVal dt As DataTable, ByVal nodes As TreeNodeCollection)
        nodes.Clear()
        For Each dr As DataRow In dt.Rows
            Dim tn As New TreeNode()
            tn.Text = dr("clasificacion").ToString()
            tn.Value = dr("CODIGO").ToString()
            nodes.Add(tn)
            tn.PopulateOnDemand = (CInt(dr("CountHijos")) > 0)
        Next
    End Sub

    Private Sub BtnBuscaClasificacion_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacion.Click
        PopularRootLevel()
    End Sub

    Private Sub TrvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles TrvClasificacion.TreeNodePopulate
        NodosHijos(CInt(e.Node.Value), e.Node)
    End Sub

    Private Sub NodosHijos(ByVal nodoPadreId As Integer, ByVal nodePadre As TreeNode)
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))

        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO, CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " _
                                      & " (SELECT count(clas_codigo) FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL2=c1.CLAS_CODIGO and clas_cod_nivel = 3 ) as CountHijos " _
                                      & " FROM TBINV_ARTICULO_CLASIFICACION c1 WHERE CLAS_NIVEL1=@parentID and clas_cod_nivel = 2 ORDER BY CLAS_NUMERACION", objConn)

        objComand.Parameters.Add("@parentID", SqlDbType.Int).Value = nodoPadreId
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()
        da.Fill(dt)

        NodosPopulares(dt, nodePadre.ChildNodes)
    End Sub

    Protected Sub TrvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TrvClasificacion.SelectedNodeChanged
        TrvClasificacion.SelectedNode.Selected = True
        TxtClasificacionBA.Value = TrvClasificacion.SelectedNode.Text
        Dim psNumero As Integer = 0
        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        TrvClasificacion.Nodes.Clear()
    End Sub

    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        TrvClasificacion.Nodes.Clear()
    End Sub

    Private Sub BtnBuscarArticulo_Click(sender As Object, e As EventArgs) Handles BtnBuscarArticulo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Dim obj As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psListaArt As String = "1"
        Dim psListaMarca As String = "1"
        Dim psListaModelo As String = "1"
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = TxtCodArticuloBA.Value.ToString
        Dim clasificacion As String = LblCodClasificacionBA.Text.ToString
        Dim descripcion As String = TxtDescripcionBA.Value.ToString
        Dim tipo As String = DdlTipoBA.SelectedValue.ToString
        Dim numPart As String = TxtNumParteBA.Value.ToString
        Dim especifico As String = TxtCodEspecificoBA.Value.ToString
        Dim marca As String = LblCodMarcaBA.Text.ToString
        Dim modelo As String = LblCodModeloBA.Text.ToString

        If marca <> "" Then psListaMarca = ""
        If modelo <> "" Then psListaModelo = ""
        If codigo <> "" Then psListaArt = ""
        If tipo = "< SELECCIONAR >" Then tipo = ""

        dt = obj.Bus_Articulo(psconexion, codigo, clasificacion, descripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
        GvBuscarArticulos.DataSource = dt
        GvBuscarArticulos.DataBind()
    End Sub

    Private Sub GvBuscarArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodArticulo.Text = GvBuscarArticulos.Rows(Index).Cells(1).Text
            TxtDescArticulo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
        End If

        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        TxtCodUbica.Text = ""
        TxtCodigo.Text = ""
        TxtCodInventarioUbicacion.Text = ""
        TxtDescripcion.Text = ""
    End Sub

    Private Sub RBUbicaciones_CheckedChanged(sender As Object, e As EventArgs) Handles RBUbicaciones.CheckedChanged
        TxtCodUbica.Text = ""
        TxtCodigo.Text = ""
        TxtCodInventarioUbicacion.Text = ""
        TxtDescripcion.Text = ""
    End Sub

    Private Sub btnBusUbicaion_Click(sender As Object, e As EventArgs) Handles btnBusUbicaion.Click
        If rbBusAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
            Session("BusViene") = True
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
        ElseIf rbBusCCosto.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
            Session("BusViene") = True
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
        End If
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        TxtCodUbica.Text = ""
        TxtCodInventarioUbicacion.Text = ""
        TxtCodigo.Text = ""
        TxtDescripcion.Text = ""
    End Sub

    Private Sub rbBusAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles rbBusAlmacen.CheckedChanged
        txtBusUbicaCodigo.Text = ""
        txtBusUbicaNombre.Text = ""
        lblBusUbicaCod.Text = ""
        lblBusUbicaCodInv.Text = ""
        LblRegistroDetalle.Text = ""
        btnBusUbicaion.Enabled = True
    End Sub

    Private Sub rbBusCCosto_CheckedChanged(sender As Object, e As EventArgs) Handles rbBusCCosto.CheckedChanged
        txtBusUbicaCodigo.Text = ""
        txtBusUbicaNombre.Text = ""
        lblBusUbicaCod.Text = ""
        lblBusUbicaCodInv.Text = ""
        LblRegistroDetalle.Text = ""
        btnBusUbicaion.Enabled = True
    End Sub

    Private Sub rbBusTodos_CheckedChanged(sender As Object, e As EventArgs) Handles rbBusTodos.CheckedChanged
        txtBusUbicaCodigo.Text = ""
        txtBusUbicaNombre.Text = ""
        lblBusUbicaCod.Text = ""
        lblBusUbicaCodInv.Text = ""
        LblRegistroDetalle.Text = ""
        btnBusUbicaion.Enabled = False
    End Sub

    Private Sub BtnResumen_Click(sender As Object, e As EventArgs) Handles BtnResumen.Click
        BindGridViewPrincipal()
        gvEmployeeDetails.Visible = True
        BtnCerrarResumen.Visible = True
    End Sub


    Private Sub BindGridViewPrincipal()
        Dim pdCodInv As Double = 0
        If ddlBusInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInv = Nz(ddlBusInventario.SelectedValue)
        End If
        Dim pdCodUbi As Double = 0
        If lblBusUbicaCodInv.Text <> "" Then
            pdCodUbi = Nz(lblBusUbicaCodInv.Text)
        End If
        Dim Cn As New SqlConnection(Session("Ruta_Emp"))
        Dim Cmd As New SqlCommand("Prc_Inventario_Resumen_xUbicacion_xIngreso", Cn)
        Cmd.CommandType = CommandType.StoredProcedure
        Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
        'Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pdCodInv
        Cmd.Parameters.Add("@CodUbicacion", SqlDbType.Float).Value = pdCodUbi
        Dim Da As New SqlDataAdapter(Cmd)
        Dim Dt As New DataTable("Prc_Inventario_Resumen_xUbicacion_xIngreso")
        Da.Fill(Dt)
        gvEmployeeDetails.DataSource = Dt
        gvEmployeeDetails.DataBind()
    End Sub

    Private Sub BtnCerrarResumen_Click(sender As Object, e As EventArgs) Handles BtnCerrarResumen.Click
        Dim dt As New DataTable
        dt = Nothing
        gvEmployeeDetails.DataSource = dt
        gvEmployeeDetails.DataBind()
        gvEmployeeDetails.Visible = True
        BtnCerrarResumen.Visible = False
    End Sub


    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim detalleGridView As GridView = TryCast(e.Row.FindControl("gv_Child"), GridView)

            Dim pdCodInv As Double = 0
            If ddlBusInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = Nz(ddlBusInventario.SelectedValue)
            End If
            Dim pdCodUbi As Double = 0
            If lblBusUbicaCodInv.Text <> "" Then
                pdCodUbi = Nz(lblBusUbicaCodInv.Text)
            End If
            If detalleGridView IsNot Nothing Then
                Dim primaryKey As Integer = Convert.ToInt32(gvEmployeeDetails.DataKeys(e.Row.RowIndex).Values("Estado_Cod"))
                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim Cmd As New SqlCommand("Prc_Inventario_Resumen_xUbicacion_xIngreso_2", Cn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
                'Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pdCodInv
                Cmd.Parameters.Add("@CodUbicacion", SqlDbType.Float).Value = pdCodUbi
                Cmd.Parameters.Add("@CodEstado", SqlDbType.Float).Value = primaryKey
                Dim Da As New SqlDataAdapter(Cmd)
                Dim Dt As New DataTable("Prc_Inventario_Resumen_xUbicacion_xIngreso_2")
                Da.Fill(Dt)
                detalleGridView.DataSource = Dt
                detalleGridView.DataBind()
            End If
        End If
    End Sub

    Protected Sub OnRowDataBound2(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim detalleGridView As GridView = TryCast(e.Row.FindControl("gv_Child2"), GridView)

            Dim pdCodInv As Double = 0
            If ddlBusInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = Nz(ddlBusInventario.SelectedValue)
            End If
            Dim pdCodUbi As Double = 0
            If lblBusUbicaCodInv.Text <> "" Then
                pdCodUbi = Nz(lblBusUbicaCodInv.Text)
            End If
            If detalleGridView IsNot Nothing Then
                Dim primaryKey As Integer = Convert.ToInt32(gvEmployeeDetails.DataKeys(e.Row.RowIndex).Values("Estado_Cod"))
                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim Cmd As New SqlCommand("Prc_Inventario_Resumen_xUbicacion_xIngreso_2", Cn)
                Cmd.CommandType = CommandType.StoredProcedure
                Cmd.Parameters.Add("@CodEmpresa", SqlDbType.VarChar).Value = Session("CodEmpresa")
                'Cmd.Parameters.Add("@CodInventario", SqlDbType.Float).Value = pdCodInv
                Cmd.Parameters.Add("@CodUbicacion", SqlDbType.Float).Value = pdCodUbi
                Cmd.Parameters.Add("@CodEstado", SqlDbType.Float).Value = 3
                Dim Da As New SqlDataAdapter(Cmd)
                Dim Dt As New DataTable("Prc_Inventario_Resumen_xUbicacion_xIngreso_2")
                Da.Fill(Dt)
                detalleGridView.DataSource = Dt
                detalleGridView.DataBind()
            End If
        End If
    End Sub

    Private Sub BtnPCerrar_Click(sender As Object, e As EventArgs) Handles BtnPCerrar.Click
        Personal.Visible = False
        Dim dt As New DataTable
        dt = Nothing
        GvPersonal.DataSource = dt
        GvPersonal.DataBind()
    End Sub

    Private Sub BtnPAgregar_Click(sender As Object, e As EventArgs) Handles BtnPAgregar.Click

        Dim dt As New DataTable


        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim dtG As New DataTable
        Dim pdCodInvUbicacion As Double = 0
        If TxtCodInventarioUbicacion.Text <> "" Then pdCodInvUbicacion = Nz(TxtCodInventarioUbicacion.Text)
        Dim psInvPersonal As String = ""
        If DdlPersonal.SelectedValue <> "< Seleccionar >" Then psInvPersonal = DdlPersonal.SelectedValue
        Try
            If psInvPersonal = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar personal.');", True)
            Else
                objUbic.Inventario_Ubicaciones_InsPersonal(Session("Ruta_Emp"), pdCodInvUbicacion, psInvPersonal)

                dt = objUbic.Inventario_Ubicaciones_Personal(Session("Ruta_Emp"), pdCodInvUbicacion)

                GvPersonal.DataSource = dt
                GvPersonal.DataBind()

            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try


    End Sub

    Private Sub Llenar_Usuarios()
        Dim objSeg As New ModuloSeguridad
        Dim dt As New DataTable
        dt = objSeg.Listar_Usuarios
        DdlPersonal.DataSource = dt
        DdlPersonal.DataValueField = "CODIGO"
        DdlPersonal.DataTextField = "nombre"
        DdlPersonal.DataBind()
        DdlPersonal.Items.Add("< Seleccionar >")
        DdlPersonal.SelectedValue = "< Seleccionar >"

    End Sub

    Private Sub BtnPersonal_Click(sender As Object, e As EventArgs) Handles BtnPersonal.Click

        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim dtG As New DataTable
        Dim dtDatos As New DataTable
        Dim pdCodInvUbicacion As Double = 0
        If TxtCodInventarioUbicacion.Text <> "" Then pdCodInvUbicacion = Nz(TxtCodInventarioUbicacion.Text)
        Try

            Personal.Visible = True
            dtDatos = objUbic.Inventario_Ubicaciones_Personal(Session("Ruta_Emp"), pdCodInvUbicacion)
            GvPersonal.DataSource = dtDatos
            GvPersonal.DataBind()
            Call Llenar_Usuarios()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub GvPersonal_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvPersonal.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim dt As New DataTable
        Dim dtG As New DataTable
        Dim pdCodInvUbicacion As Double = 0
        If TxtCodInventarioUbicacion.Text <> "" Then pdCodInvUbicacion = Nz(TxtCodInventarioUbicacion.Text)
        Dim psInvPersonal As String = ""
        Try
            If e.CommandName = "Quitar" Then
                psInvPersonal = GvPersonal.Rows(Index).Cells(1).Text
                objUbic.Inventario_Ubicaciones_DelPersonal(Session("Ruta_Emp"), pdCodInvUbicacion, psInvPersonal)
                dt = objUbic.Inventario_Ubicaciones_Personal(Session("Ruta_Emp"), pdCodInvUbicacion)
                GvPersonal.DataSource = dt
                GvPersonal.DataBind()
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnCostos_Click(sender As Object, e As EventArgs) Handles BtnCostos.Click
        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim dtG As New DataTable
        Dim dtDatos As New DataTable
        Dim pdCodInvUbicacion As Double = 0
        If TxtCodInventarioUbicacion.Text <> "" Then pdCodInvUbicacion = Nz(TxtCodInventarioUbicacion.Text)
        Try

            Costos.Visible = True
            CostosBoton.Visible = True
            Costotitulo.Visible = True
            Label9.Visible () = True : TxtCostoMovilidad.Visible = True : TxtCostoMovilidad.Text = "0.00"
            Label10.Visible = True : TxtCostoPlacado.Visible = True : TxtCostoPlacado.Text = "0.00"
            Label11.Visible = True : TxtCostoRecojo.Visible = True : TxtCostoRecojo.Text = "0.00"
            Label12.Visible = True : TxtCostoVerif.Visible = True : TxtCostoVerif.Text = "0.00"
            Label13.Visible = True : TxtCostoxBien.Visible = True : TxtCostoxBien.Text = "1.84"
            BtnCostoCerrar.Visible = True
            BtnCostoGuardar.Visible = True
            dtDatos = objUbic.Inventario_Costos_xUbicacion(Session("Ruta_Emp"), pdCodInvUbicacion)
            If dtDatos.Rows.Count > 0 Then
                For Each dr As DataRow In dtDatos.Rows
                    TxtCostoMovilidad.Text = Nz(dr("INVENTARIO_COSTO_MOVILIDAD"))
                    TxtCostoPlacado.Text = Nz(dr("INVENTARIO_COSTO_PLACADO"))
                    TxtCostoRecojo.Text = Nz(dr("INVENTARIO_COSTO_RECOJODEVOLUCION_LLAVES"))
                    TxtCostoVerif.Text = Nz(dr("INVENTARIO_COSTO_VERIFICACION"))
                    TxtCostoxBien.Text = Nz(dr("INVENTARIO_COSTO_XBIEN"))
                Next
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnCostoCerrar_Click(sender As Object, e As EventArgs) Handles BtnCostoCerrar.Click

        Costos.Visible = False
        CostosBoton.Visible = False
        Costotitulo.Visible = False
        Label9.Visible = False : TxtCostoVerif.Visible = False : TxtCostoVerif.Text = "0.00"
        Label10.Visible = False : TxtCostoRecojo.Visible = False : TxtCostoRecojo.Text = "0.00"
        Label11.Visible = False : TxtCostoMovilidad.Visible = False : TxtCostoMovilidad.Text = "0.00"
        Label12.Visible = False : TxtCostoPlacado.Visible = False : TxtCostoPlacado.Text = "0.00"
        Label13.Visible = False : TxtCostoxBien.Visible = False : TxtCostoxBien.Text = "0.00"
        BtnCostoCerrar.Visible = False
        BtnCostoGuardar.Visible = False
    End Sub

    Private Sub BtnCostoGuardar_Click(sender As Object, e As EventArgs) Handles BtnCostoGuardar.Click
        'Inventario_InsertarCostos_xUbicacion
        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim dtG As New DataTable
        Dim dtDatos As New DataTable
        Dim pdCodInvUbicacion As Double = 0
        If TxtCodInventarioUbicacion.Text <> "" Then pdCodInvUbicacion = Nz(TxtCodInventarioUbicacion.Text)
        Try

            Dim pdCosto_xBien As Decimal = 0
            Dim pdCosto_Recojo As Decimal = 0
            Dim pdCosto_Movilidad As Decimal = 0
            Dim pdCosto_Verificacion As Decimal = 0
            Dim pdCosto_Placado As Decimal = 0

            pdCosto_xBien = Nz(TxtCostoxBien.Text)
            pdCosto_Recojo = Nz(TxtCostoRecojo.Text)
            pdCosto_Movilidad = Nz(TxtCostoMovilidad.Text)
            pdCosto_Verificacion = Nz(TxtCostoVerif.Text)
            pdCosto_Placado = Nz(TxtCostoPlacado.Text)
            objUbic.Inventario_InsertarCostos_xUbicacion(Session("Ruta_Emp"), pdCodInvUbicacion, pdCosto_xBien, pdCosto_Recojo, pdCosto_Movilidad, pdCosto_Verificacion, pdCosto_Placado)

            Costos.Visible = False
            CostosBoton.Visible = False
            Costotitulo.Visible = False
            Label9.Visible = False : TxtCostoMovilidad.Visible = False : TxtCostoMovilidad.Text = ""
            Label10.Visible = False : TxtCostoPlacado.Visible = False : TxtCostoPlacado.Text = ""
            Label11.Visible = False : TxtCostoRecojo.Visible = False : TxtCostoRecojo.Text = ""
            Label12.Visible = False : TxtCostoVerif.Visible = False : TxtCostoVerif.Text = ""
            Label13.Visible = False : TxtCostoxBien.Visible = False : TxtCostoxBien.Text = ""
            BtnCostoCerrar.Visible = False
            BtnCostoGuardar.Visible = False

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub
End Class