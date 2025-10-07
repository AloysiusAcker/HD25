Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Imports System.IO
Public Class Inventario_EquiposConciliados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim psMensaje As String = ""

        If Not String.IsNullOrEmpty(Session("GlobalErrorMessage")) Then
            psMensaje = Session("GlobalErrorMessage")
            ' Limpia el mensaje global para que no se muestre en las próximas solicitudes.
            Session.Remove("GlobalErrorMessage")
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('" & psMensaje & "';", True)
            Response.Redirect("~/default2.aspx?")
        End If
        If Not Page.IsPostBack Then

            'Ficha.ActiveTabIndex = 1 : Ficha.Enabled = False
            'Ficha.ActiveTabIndex = 0
            'Ficha_ActiveTabChanged(sender, e)
            Llenar_Combo_Inventario()
            Llenar_Combo_Estado_Inventario()
            Llenar_Combo_Estado_Conciliacion()
            Llenar_Combo_Tipo()
            Llenar_Combo_Ubicacion()
            DdlInventario.SelectedValue = "1"
            ListaResumen()
            'Ficha.ActiveTabIndex = 0
            'Ficha_ActiveTabChanged(sender, e)
        End If
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "CallMyFunction", "MantenSesion();", True)
    End Sub
    Protected Sub Lista_Equipos_Conciliados()
        Dim obj As New Cls_Inventario
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim EstInventario As String = "%"
        If DdlEstInventario.SelectedValue <> "< Seleccionar >" Then
            EstInventario = DdlEstInventario.SelectedValue
        End If
        Dim CodArticulo As Double = 0
        Dim DesArticulo As String = txtDescArticulo.Text.ToString
        Dim Descripcion As String = txtDescripcion.Text.ToString
        Dim NroSerie As String = txtNroSerie.Text.ToString
        Dim NroPlaca As Double = 0
        Dim EstConciliacion As String = "%"
        If DdlEstConciliacion.SelectedValue <> "< Seleccionar >" Then
            EstConciliacion = DdlEstConciliacion.SelectedValue
        End If
        Dim Clasifi As String = LblCodClasificacionBA.Text
        Dim CodArea As Double = 0
        If DdlUbicacion.SelectedValue <> "< Seleccionar >" Then
            CodArea = DdlUbicacion.SelectedValue
        End If
        Dim TipoListaArea As String = "0"
        Dim CodUbicacion As Double = 0
        Dim codInventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            codInventario = Nz(DdlInventario.SelectedValue)
        End If
        Dim psTipoLista As String = "0"
        If txtCodArticulo.Text.ToString <> "" Then
            CodArticulo = txtCodArticulo.Text.ToString
            psTipoLista = "1"
        End If
        If txtNroPlaca.Text.ToString <> "" Then
            NroPlaca = txtNroPlaca.Text.ToString
        End If
        If EstInventario = "6" Then
            EstInventario = "%"
        End If
        If EstConciliacion = "3" Then
            EstConciliacion = "%"
        End If
        Dim psTipoDestino As String = "0"
        If chckArea.Checked = True Then
            If RBAlmacen.Checked = True Then psTipoDestino = "1"
            If RBSeccion.Checked = True Then psTipoDestino = "2"
        End If
        If txtArea.Text.ToString <> "" Then
            CodUbicacion = Nz(lblcodUbicaInv.Text)
        End If
        If CodArea = 0 Then
            CodArea = 0
            TipoListaArea = "0"
        Else
            TipoListaArea = "1"
        End If

        Dim pdCodInvUbica As Double = 0
        Dim pdCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInv = DdlInventario.SelectedValue
        End If
        If lblcodUbicaInv.Text <> "" Then
            pdCodInvUbica = lblcodUbicaInv.Text
        End If

        Dim numeroClas() As String
        numeroClas = Clasifi.Split(" ")
        Dim clasificacion As String = numeroClas(0)
        Try
            Convert.ToInt32(NroPlaca)
            dt = obj.Conciliacion_Lista_Inventariado(Session("Ruta_Emp"), pdCodInv, NroPlaca, NroSerie, CodArticulo, DesArticulo, pdCodInvUbica)

            GvListaConciliados.DataSource = dt
            GvListaConciliados.DataBind()

            If dt.Rows.Count > 1 Then lblRegistroInv.Text = "Hay " & dt.Rows.Count & " registros."
            If dt.Rows.Count = 1 Then lblRegistroInv.Text = "Hay 1 registro."
            If dt.Rows.Count = 0 Then lblRegistroInv.Text = "No hay registro."

        Catch ex As FormatException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El número de placa debe ser número');", True)
        End Try

    End Sub
    Protected Sub ListaCantidadesXActivos()
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim EstInventario As String = "%"
        If DdlEstInventario.SelectedValue <> "< Seleccionar >" Then
            EstInventario = DdlEstInventario.SelectedValue
        End If
        Dim CodArticulo As Double = 0
        If Nz(txtCodArticulo.Text) = 0 Then
            CodArticulo = Nz(txtCodArticulo.Text)
        End If
        Dim DesArticulo As String = txtDescArticulo.Text.ToString
        Dim Descripcion As String = txtDescripcion.Text.ToString
        Dim NroSerie As String = txtNroSerie.Text.ToString
        Dim NroPlaca As Double = 0
        Dim EstConciliacion As String = "%"
        If DdlEstConciliacion.SelectedValue <> "< Seleccionar >" Then
            EstConciliacion = DdlEstConciliacion.SelectedValue
        End If
        Dim Clasifi As String = txtClasificacion.Text
        Dim CodArea As Double = 0
        If DdlUbicacion.SelectedValue <> "< Seleccionar >" Then
            CodArea = Nz(DdlUbicacion.SelectedValue)
        End If
        Dim TipoListaArea As String = txtDescripcionArea.Text.ToString
        Dim CodUbicacion As Double = 0
        If lblCodUbica.Text <> "" Then
            CodUbicacion = lblCodUbica.Text
        End If
        Dim codInventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            codInventario = Nz(DdlInventario.SelectedValue)
        End If
        If EstInventario = "6" Then
            EstInventario = "%"
        End If
        If EstConciliacion = "3" Then
            EstConciliacion = "%"
        End If
        Dim psTipoDestino As String = ""
        If chckArea.Checked = True Then
            If RBAlmacen.Checked = True Then psTipoDestino = "1"
            If RBSeccion.Checked = True Then psTipoDestino = "2"
        End If
        If txtNroPlaca.Text.ToString <> "" Then
            NroPlaca = Nz(txtNroPlaca.Text.ToString)
        End If
        Dim numeroClas() As String
        numeroClas = Clasifi.Split(" ")
        Dim clasificacion As String = numeroClas(0)
        Try
            dt = obj.ListaCantidadesXActivos(psconexion, Session("CodEmpresa"), EstInventario, EstConciliacion, NroSerie, NroPlaca, CodArticulo, DesArticulo, Descripcion, psTipoDestino, CodUbicacion, CodArea, "0", TipoListaArea, clasificacion, codInventario)
            GvListaCantidadesXActivos.DataSource = dt
            GvListaCantidadesXActivos.DataBind()
        Catch ex As FormatException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El número de placa deben ser números');", True)
        End Try
    End Sub

    Protected Sub Busqueda_Articulos()
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psListaArt As String = "1"
        Dim psListaMarca As String = "1"
        Dim psListaModelo As String = "1"
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As String = TxtCodArticuloBA.Value.ToString
        Dim Clasificacion As String = LblCodClasModal.Text.ToString
        Dim Descripcion As String = TxtDescripcionBA.Value.ToString
        Dim Tipo As String = DdlTipoBA.SelectedValue.ToString
        Dim NuPart As String = TxtNumParteBA.Value.ToString
        Dim CodEs As String = TxtCodEspecificoBA.Value.ToString
        Dim marca As String = LblCodMarcaBA.Text.ToString
        Dim modelo As String = LblCodModeloBA.Text.ToString

        If marca <> "" Then psListaMarca = ""
        If modelo <> "" Then psListaModelo = ""
        If Codigo <> "" Then psListaArt = ""
        If Tipo = "< Seleccionar >" Then Tipo = ""

        dt = obj.Bus_Articulo(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)
        GvBuscarArticulos.DataSource = dt
        GvBuscarArticulos.DataBind()
    End Sub
    Protected Sub ListaResumen()
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim pdCodInventario As Double = 0
        Dim pdCodUbicaInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInventario = Nz(DdlInventario.SelectedValue)
        End If
        Dim psconexion As String = Session("Ruta_Emp")
        If lblcodUbicaInv.Text <> "" Then
            pdCodUbicaInv = Nz(lblcodUbicaInv.Text)
            dt = obj.Lista_Resumen_xUbicacion(psconexion, pdCodInventario, pdCodUbicaInv)
        Else
            dt = obj.Lista_Resumen(psconexion, pdCodInventario)
        End If
        GvListaResumen.DataSource = dt
        GvListaResumen.DataBind()
    End Sub

    Protected Sub ListaResumen_xUbicacion()
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim pdCodUbicacion As Double = 0
        Dim pdCodInventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInventario = Nz(DdlInventario.SelectedValue)
        End If 'Lista_Resumen_xUbicacion

        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Lista_Resumen_xUbicacion(psconexion, pdCodInventario, pdCodUbicacion)
        GvListaResumen.DataSource = dt
        GvListaResumen.DataBind()
    End Sub
    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Try
            GvLista.DataSource = Nothing
            GvLista.DataBind()
            GvListaVerificarInventarioNuevos.DataSource = Nothing
            GvListaVerificarInventarioNuevos.DataBind()
            GvListaVerificarInventario.DataSource = Nothing
            GvListaVerificarInventario.DataBind()
            Lista_Equipos_Conciliados()
            ListaCantidadesXActivos()
            ListaResumen()
            Listar_Equipos_Nuevos()
            ListaEquiposConciliados()
        Catch ex As SqlException

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "';", True)
        Catch ex As Exception

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicacion: " & ex.Message & "';", True)
        End Try
    End Sub

    Protected Sub Listar_Equipos_Nuevos()

        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        dt = Nothing

        Dim dtO As New DataTable
        dtO = Nothing
        Dim codigo As Double = 0
        If lblcodUbicaInv.Text <> "" Then
            codigo = lblcodUbicaInv.Text
        End If

        Dim pdCodInvUbica As Double = 0
        Dim pdCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInv = DdlInventario.SelectedValue
        End If
        If lblcodUbicaInv.Text <> "" Then
            pdCodInvUbica = lblcodUbicaInv.Text
        End If

        Dim tipo As String = ""
        Dim ubicacion As Double = 0
        If lblCodUbica.Text <> "" Then
            ubicacion = lblCodUbica.Text
        End If
        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBSeccion.Checked Then
            tipo = "2"
        End If

        dtO = obj.Lista_Inventario_Verificacion_Nuevos(Session("Ruta_Emp"), pdCodInv, tipo, pdCodInvUbica)
        GvListaVerificarInventarioNuevos.DataSource = dtO
        GvListaVerificarInventarioNuevos.DataBind()

        If dtO.Rows.Count > 1 Then lblNuevos.Text = "Hay " & dtO.Rows.Count & " registros."
        If dtO.Rows.Count = 1 Then lblNuevos.Text = "Hay 1 registro."
        If dtO.Rows.Count = 0 Then lblNuevos.Text = "No hay registro."

    End Sub

    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As Double = 0
        Dim psCodInterno As String = ""
        Dim pdCodInventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            pdCodInventario = Nz(DdlInventario.SelectedValue)
        End If
        Dim descripcion As String = BuscarDescripcion.Value.ToString
        If TituloPopup.Text = "Búsqueda Almacén" Then
            If BuscarCodigo.Value <> "" Then codigo = Nz(BuscarCodigo.Value.ToString)
            dt = obj.Listar_Almacenes_Inventario_Verificacion(psconexion, pdCodInventario, codigo, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
            psCodInterno = Nu(BuscarCodigo.Value.ToString)
            dt = obj.Listar_CentroC_Inventario_Verificacion(psconexion, pdCodInventario, psCodInterno, descripcion)
        End If
        GvBuscAlmacen.DataSource = dt
        GvBuscAlmacen.DataBind()
    End Sub

    Protected Sub Llenar_Combo_Inventario()
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Inventario(psconexion)
        DdlInventario.DataSource = dt
        DdlInventario.DataValueField = "INVENT_CODIGO"
        DdlInventario.DataTextField = "INVENT_DESC"
        DdlInventario.Items.Add("< Seleccionar >")
        DdlInventario.DataSource = dt

        DdlInventario.DataBind()
        DdlInventario.Items.Add("< Seleccionar >")
        DdlInventario.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Llenar_Combo_Tipo()
        Dim objC As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = objC.Lista_Tipo(psconexion)
        DdlTipoBA.DataSource = dt
        DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipoBA.DataBind()
        DdlTipoBA.Items.Add("< Seleccionar >")
        DdlTipoBA.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Llenar_Combo_Estado_Inventario()
        Call LlenaComboItem("TBOPC244", DdlEstInventario)
        Call LlenaComboItem("TBOPC244", DdlEstInventarioEST)
        DdlEstInventario.SelectedValue = 6
        DdlEstInventarioEST.SelectedValue = 6
    End Sub
    Protected Sub Llenar_Combo_Estado_Conciliacion()
        Call LlenaComboItem("TBOPC544", DdlEstConciliacion)
        Call LlenaComboItem("TBOPC544", DdlEstConciliacionEST)
        DdlEstConciliacion.SelectedValue = 3
        DdlEstConciliacionEST.SelectedValue = 3
    End Sub

    Private Sub Llena_Ubicacion(ByVal combo As DropDownList, ByVal psTipo As String)
        'Lista_Ubicaciones
        Dim obj As New clsInv_Listados
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = obj.Lista_Ubicaciones2(Session("Ruta_Emp"), Session("CodEmpresa"), psTipo)
        combo.DataTextField = "Ubicacion"
        combo.DataValueField = "UBICACION_CODIGO"
        combo.DataBind()
        combo.Items.Add("< Seleccionar >")
        combo.SelectedValue = "< Seleccionar >"
    End Sub

    Protected Sub Llenar_Combo_Ubicacion()
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Ubicacion(psconexion)
        DdlUbicacion.Items.Clear()
        DdlUbicacion.DataSource = dt
        DdlUbicacion.DataValueField = "UBICACION_CODIGO"
        DdlUbicacion.DataTextField = "UBIC_DESC"
        DdlUbicacion.DataBind()
        DdlUbicacion.Items.Add("< Seleccionar >")
        DdlUbicacion.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Limpiar_Cajas_Conciliados_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
    End Sub
    Private Sub BtnBuscaArticulo_Click(sender As Object, e As EventArgs) Handles BtnBuscaArticulo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
    End Sub
    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Limpiar_Cajas_Conciliados_Articulos()
    End Sub
    Private Sub PopularRootLevel()
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))


        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO, CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " _
                                      & " (SELECT count(clas_codigo) FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL1=c1.CLAS_CODIGO and clas_cod_nivel = 2 ) as CountHijos " _
                                      & " FROM TBINV_ARTICULO_CLASIFICACION c1  WHERE clas_sys_est = '0' and CLAS_COD_NIVEL=1 ORDER BY CLAS_NUMERACION", objConn)
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()

        da.Fill(dt)
        NodosPopulares(dt, trvClasificacion.Nodes)

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
    Private Sub BtnBuscaMarcaBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarcaBA.Click
        TituloPopupMM.Text = "Busca Marca"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaModeloBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaModeloBA.Click
        TituloPopupMM.Text = "Busca Modelo"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        If TituloPopupMM.Text = "Busca Marca" Or TituloPopupMM.Text = "Busca Modelo" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()
    End Sub
    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigoMM.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        GvBusquedaM.DataSource = Nothing
        GvBusquedaM.DataBind()
    End Sub
    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
    End Sub
    Protected Sub BtnBuscaClasificacion_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacion.Click
        PopularRootLevel()
    End Sub

    Private Sub NodosHijos(ByVal nodoPadreId As Integer, ByVal nodePadre As TreeNode)
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))
        Dim objComand As New SqlCommand(" SELECT CLAS_CODIGO as CODIGO, CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " _
                                      & " (SELECT count(clas_codigo) FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL2=c1.CLAS_CODIGO and clas_cod_nivel = 3 ) as CountHijos " _
                                      & " FROM TBINV_ARTICULO_CLASIFICACION c1 WHERE CLAS_NIVEL1=@parentID  and clas_sys_est = '0' and clas_cod_nivel = 2 ORDER BY CLAS_NUMERACION", objConn)
        objComand.Parameters.Add("@parentID", SqlDbType.Int).Value = nodoPadreId
        Dim da As New SqlDataAdapter(objComand)
        Dim dt As New DataTable()
        da.Fill(dt)
        NodosPopulares(dt, nodePadre.ChildNodes)
    End Sub
    Protected Sub trvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles trvClasificacion.TreeNodePopulate
        NodosHijos(CInt(e.Node.Value), e.Node)
    End Sub

    Protected Sub BtnBuscaAlmacen_Click(sender As Object, e As EventArgs) Handles BtnBuscaAlmacen.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBSeccion.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAlmacen').modal('show');", True)
    End Sub

    Private Sub BtnCerrarAlmacen_Click(sender As Object, e As EventArgs) Handles BtnCerrarAlmacen.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAlmacen').modal('hide');", True)
    End Sub
    Private Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged

        If trvClasificacion.SelectedNode.Selected = True Then
            If TituloClasificacion.Text = "Buscar Clasificacion" Then
                txtClasificacion.Text = TrvClasificacion.SelectedNode.Text
                Dim psPosicion As Long = 0
                Dim psNumero As Integer = 0
                psNumero = InStr(1, txtClasificacion.Text, "-")
                LblCodClasificacionBA.Text = Left(txtClasificacion.Text, psNumero - 2)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)
                TrvClasificacion.Nodes.Clear()
            Else
                TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
                Dim psPosicion As Long = 0
                Dim psNumero As Integer = 0
                psNumero = InStr(1, TxtClasificacionBA.Value, "-")
                LblCodClasModal.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
                TrvClasificacion.Nodes.Clear()
            End If
        End If
    End Sub

    Private Sub GvBuscAlmacen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscAlmacen.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            txtArea.Text = GvBuscAlmacen.Rows(Index).Cells(1).Text.ToString
            txtDescripcionArea.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscAlmacen.Rows(Index).Cells(2).Text.ToString, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblCodUbica.Text = GvBuscAlmacen.Rows(Index).Cells(3).Text.ToString
            lblcodUbicaInv.Text = GvBuscAlmacen.Rows(Index).Cells(4).Text.ToString
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalAlmacen').modal('hide');", True)
        End If
        Dim psTipo As String = ""
        If chckArea.Checked = True Then
            If RBAlmacen.Checked = True Then psTipo = "1"
            If RBSeccion.Checked = True Then psTipo = "2"
        End If
        If psTipo <> "" Then
            Call Llena_Ubicacion(DdlUbicacion, psTipo)
        Else
            Call Llenar_Combo_Ubicacion()
        End If
    End Sub
    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Busqueda_Articulos()
    End Sub

    Private Sub GvBuscarArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            txtCodArticulo.Text = GvBuscarArticulos.Rows(Index).Cells(1).Text.ToString
            txtDescArticulo.Text = GvBuscarArticulos.Rows(Index).Cells(2).Text.ToString
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
            TxtClasificacionBA.Value = ""
            LblCodClasModal.Text = ""
            GvBuscarArticulos.DataSource = Nothing
            GvBuscarArticulos.DataBind()
        End If
    End Sub
    Private Sub chckCodArticulo_CheckedChanged(sender As Object, e As EventArgs) Handles chckCodArticulo.CheckedChanged
        If chckCodArticulo.Checked = True Then
            BtnBuscaArticulo.Enabled = True
        Else
            BtnBuscaArticulo.Enabled = False
            txtCodArticulo.Text = ""
            txtDescArticulo.Text = ""
        End If
    End Sub

    Private Sub chckDescripcion_CheckedChanged(sender As Object, e As EventArgs) Handles chckDescripcion.CheckedChanged
        If chckDescripcion.Checked = True Then
            txtDescripcion.Enabled = True
        Else
            txtDescripcion.Enabled = False
        End If

    End Sub
    Private Sub chckArea_CheckedChanged(sender As Object, e As EventArgs) Handles chckArea.CheckedChanged
        If chckArea.Checked = True Then
            RBAlmacen.Enabled = True
            RBSeccion.Enabled = True
            BtnBuscaAlmacen.Enabled = True
            txtArea.Text = ""
            txtDescripcionArea.Text = ""
            lblcodUbicaInv.Text = ""
            lblCodUbica.Text = ""
        Else
            RBAlmacen.Enabled = False
            RBSeccion.Enabled = False
            BtnBuscaAlmacen.Enabled = False
            txtArea.Text = ""
            txtDescripcionArea.Text = ""
            lblcodUbicaInv.Text = ""
            lblCodUbica.Text = ""
            Call Llenar_Combo_Ubicacion()
        End If
    End Sub
    Private Sub chckClasificacion_CheckedChanged(sender As Object, e As EventArgs) Handles chckClasificacion.CheckedChanged
        If chckClasificacion.Checked = True Then
            BtnBuscaClasificacionM.Enabled = True
            LblCodClasificacionBA.Text = ""
            txtClasificacion.Text = ""
        Else
            BtnBuscaClasificacionM.Enabled = False
            txtClasificacion.Text = ""
            LblCodClasificacionBA.Text = ""
        End If
    End Sub

    Private Sub chckUbicacion_CheckedChanged(sender As Object, e As EventArgs) Handles chckUbicacion.CheckedChanged
        If chckUbicacion.Checked = True Then
            DdlUbicacion.Enabled = True
        Else
            DdlUbicacion.Enabled = False
        End If

    End Sub

    Private Sub BtnCerrarEST_Click(sender As Object, e As EventArgs) Handles BtnCerrarEST.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCambiarEstado').modal('hide');", True)
        TxtMensajeError.Text = ""
    End Sub
    Private Sub BtnGuardarEST_Click(sender As Object, e As EventArgs) Handles BtnGuardarEST.Click
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim SerieNum As String = LblSerieNum.Text.ToString
        Dim conciliados As String = DdlEstConciliacionEST.SelectedValue.ToString
        Dim estado As String = DdlEstInventarioEST.SelectedValue.ToString
        Dim psconexion As String = Session("Ruta_Emp")
        TxtMensajeError.Text = ""
        If estado = 6 Then
            TxtMensajeError.Text = "Seleccione un Estado valido"
        ElseIf ChckEstConciliacionEST.Checked = False Then
            obj.Actualizar_Estado_Inventario(psconexion, SerieNum, "", estado, 0)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCambiarEstado').modal('hide');", True)
            Lista_Equipos_Conciliados()
        Else
            If conciliados = 3 Then
                TxtMensajeError.Text = "Selecciona un campo valido"
            Else
                obj.Actualizar_Estado_Inventario(psconexion, SerieNum, conciliados, estado, 0)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCambiarEstado').modal('hide');", True)
                Lista_Equipos_Conciliados()
            End If
        End If
    End Sub

    Private Sub ChckEstConciliacionEST_CheckedChanged(sender As Object, e As EventArgs) Handles ChckEstConciliacionEST.CheckedChanged
        If ChckEstConciliacionEST.Checked = True Then
            DdlEstConciliacionEST.Enabled = True
        Else
            DdlEstConciliacionEST.Enabled = False
            DdlEstConciliacionEST.SelectedValue = 3
        End If
    End Sub

    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        TituloClasificacion.Text = "Articulo - Buscar Clasificacion"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnBuscaClasificacionM_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionM.Click
        TituloClasificacion.Text = "Buscar Clasificacion"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('show');", True)
    End Sub

    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click
        TrvClasificacion.Nodes.Clear()
        If TituloClasificacion.Text = "Buscar Clasificacion" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)
        ElseIf TituloClasificacion.Text = "Articulo - Buscar Clasificacion" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        End If
    End Sub

    Private Sub BtnBuscarMM_Click(sender As Object, e As EventArgs) Handles BtnBuscarMM.Click
        Dim obj As New Cls_Conciliados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim dtU As New DataTable
        Dim dtM As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = BuscarCodigoMM.Value.ToString
        Dim descripcion As String = BuscarDescripcionMM.Value.ToString
        Dim codMarca As String = LblCodMarcaBA.Text.ToString

        If TituloPopupMM.Text = "Busca Marca" Then
            dtM = obj.Buscar_Marca(psconexion, codigo, descripcion)
        ElseIf TituloPopupMM.Text = "Busca Modelo" Then
            dtM = obj.Buscar_Modelo(psconexion, codigo, descripcion, codMarca)
        End If

        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()

        GvBusquedaM.DataSource = dtM
        GvBusquedaM.DataBind()
    End Sub

    Private Sub GvBusquedaM_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusquedaM.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And TituloPopupMM.Text = "Busca Marca" Then
            TxtMarcaBA.Value = GvBusquedaM.Rows(Index).Cells(2).Text
            LblCodMarcaBA.Text = GvBusquedaM.Rows(Index).Cells(3).Text
        ElseIf e.CommandName = "Aceptar" And TituloPopupMM.Text = "Busca Modelo" Then
            TxtModeloBA.Value = GvBusquedaM.Rows(Index).Cells(2).Text
            LblCodModeloBA.Text = GvBusquedaM.Rows(Index).Cells(3).Text
        End If
        Limpiar_Cajas_Popup()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub GvListaVerificarInventarioNuevos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaVerificarInventarioNuevos.RowCommand

        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Conciliados
        Dim objListaInv As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim NroPlaca As String = GvListaConciliados.Rows(Index).Cells(5).Text
        Dim NroSerie As String = GvListaConciliados.Rows(Index).Cells(4).Text
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psCodInventario As Double = 0
        Dim pdCodArticulo As Double = 0
        Dim psCodInvUbica As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInventario = Nz(DdlInventario.SelectedValue)
        End If
        If lblcodUbicaInv.Text <> "" Then
            psCodInvUbica = Nz(lblcodUbicaInv.Text)
        End If
        lblCodInventarioUbica.Text = ""
        Try
            If e.CommandName = "CambiarEstado" Then
                dt = obj.Buscar_Serie_Numerar(psconexion, NroPlaca, NroSerie)
                If dt.Rows.Count > 0 Then
                    Dim DvRow As DataRow = dt.Rows(0)
                    LblSerieNum.Text = DvRow(0)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCambiarEstado').modal('show');", True)
                End If
            ElseIf e.CommandName = "Conciliar" Then
                gvNEUsuario.DataSource = Nothing
                gvNEUsuario.DataBind()
                gvNoInventariado.DataSource = Nothing
                gvNoInventariado.DataBind()
                lblCodInventarioUbica.Text = Nz(GvListaVerificarInventarioNuevos.Rows(Index).Cells(12).Text)
                lblModalUbicaTipo.Text = GvListaVerificarInventarioNuevos.Rows(Index).Cells(13).Text
                lblModalUbicaCodigo.Text = Nz(GvListaVerificarInventarioNuevos.Rows(Index).Cells(14).Text)
                TxtBusModalArtCod.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaVerificarInventarioNuevos.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                TxtBusModalArtNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaVerificarInventarioNuevos.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                txtModalArtCodigo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaVerificarInventarioNuevos.Rows(Index).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                txtModalArtNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaVerificarInventarioNuevos.Rows(Index).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                txtModalNroSerie.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaVerificarInventarioNuevos.Rows(Index).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                txtModalNroPlaca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaVerificarInventarioNuevos.Rows(Index).Cells(5).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                lblModalSerieNumerar.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvListaVerificarInventarioNuevos.Rows(Index).Cells(8).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                If txtModalArtCodigo.Text <> "" Then
                    pdCodArticulo = Nz(txtModalArtCodigo.Text)
                End If
                psCodUbicacionArea.Text = Nz(GvListaVerificarInventarioNuevos.Rows(Index).Cells(11).Text)
                BtnModalBuscar_Click(sender, e)

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalConciliar').modal('show');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un erro en la aplicacion:" & ex.Message & " ');", True)

        End Try
    End Sub

    Private Sub BtnModalCerrar_Click(sender As Object, e As EventArgs) Handles BtnModalCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalConciliar').modal('hide');", True)
    End Sub

    Private Sub BtnModalBuscar_Click(sender As Object, e As EventArgs) Handles BtnModalBuscar.Click
        Dim psCodInventario As Double = 0
        Dim pdCodArticulo As Double = 0
        Dim psCodInvUbica As Double = 0
        Dim objListaCon As New Cls_Conciliados
        Dim psNombreArt As String = ""
        Dim dt As New DataTable
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInventario = Nz(DdlInventario.SelectedValue)
        End If
        If lblcodUbicaInv.Text <> "" Then
            psCodInvUbica = Nz(lblcodUbicaInv.Text)
        End If
        If TxtBusModalArtCod.Text <> "" Then
            pdCodArticulo = TxtBusModalArtCod.Text
        End If
        If TxtBusModalArtNombre.Text <> "" Then
            psNombreArt = TxtBusModalArtNombre.Text
        End If
        dt = objListaCon.Inventario_Conciliar_ListaNoInventariado(Session("Ruta_Emp"), psCodInventario, psCodInvUbica, pdCodArticulo, psNombreArt)
        gvNoInventariado.DataSource = dt
        gvNoInventariado.DataBind()
    End Sub

    Private Sub BtnModalConciliar_Click(sender As Object, e As EventArgs) Handles BtnModalConciliar.Click
        Dim psMarcar As CheckBox
        Dim pdCantMarcar As Double = 0
        Dim psCodInventario As Double = 0
        Dim psCodInvUbica As Double = 0
        Dim psSerieNumerar As Double = 0
        Dim psUbicatipo As String = ""
        If RBAlmacen.Checked = True Then psUbicatipo = "1"
        If RBSeccion.Checked = True Then psUbicatipo = "2"
        Dim pdUbicaCodigo As Double = 0
        Dim psSerieNro As String = ""
        Dim pdCodArt As Double = 0
        Dim pdNroPlaca As Double = 0

        If lblCodUbica.Text<>"" Then pdUbicaCodigo=Nz(lblCodUbica.text)
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInventario = Nz(DdlInventario.SelectedValue)
        End If
        If lblcodUbicaInv.Text <> "" Then
            psCodInvUbica = Nz(lblcodUbicaInv.Text)
        End If
        Dim pdUbicaInvConciliar As Double = 0
        Dim psTipoArt As Double = 0
        Dim psCodUbicainventario As Double = 0
        Dim pdCodUbicacionArea As Double = 0
        If psCodUbicacionArea.Text <> "" And psCodUbicacionArea.Text > 0 Then
            pdCodUbicacionArea = psCodUbicacionArea.Text
        End If
        For i = 0 To gvNoInventariado.Rows.Count - 1
            psMarcar = gvNoInventariado.Rows(i).Cells(0).FindControl("chkPag")
            If psMarcar.Checked = True And psMarcar.Enabled = True Then
                psSerieNumerar = Nz(gvNoInventariado.Rows(i).Cells(7).Text)
                pdNroPlaca = Nz(gvNoInventariado.Rows(i).Cells(4).Text)
                psSerieNro = Nu(gvNoInventariado.Rows(i).Cells(3).Text)
                pdCodArt = Nz(gvNoInventariado.Rows(i).Cells(1).Text)
                pdCantMarcar = pdCantMarcar + 1
                pdUbicaInvConciliar = Nz(gvNoInventariado.Rows(i).Cells(8).Text)
                psTipoArt = Nz(gvNoInventariado.Rows(i).Cells(9).Text)
                psUbicatipo = gvNoInventariado.Rows(i).Cells(10).Text
                pdUbicaCodigo = Nz(gvNoInventariado.Rows(i).Cells(11).Text)

            End If
        Next
        For i = 0 To gvNEUsuario.Rows.Count - 1
            psMarcar = gvNEUsuario.Rows(i).Cells(0).FindControl("chkPag")
            If psMarcar.Checked = True And psMarcar.Enabled = True Then
                psSerieNumerar = Nz(gvNEUsuario.Rows(i).Cells(8).Text)
                pdNroPlaca = Nz(gvNEUsuario.Rows(i).Cells(5).Text)
                psSerieNro = Nu(gvNEUsuario.Rows(i).Cells(4).Text)
                pdCodArt = Nz(gvNEUsuario.Rows(i).Cells(1).Text)
                pdCantMarcar = pdCantMarcar + 1
                pdUbicaInvConciliar = Nz(gvNEUsuario.Rows(i).Cells(8).Text)
                psTipoArt = Nz(gvNEUsuario.Rows(i).Cells(9).Text)
                psUbicatipo = gvNEUsuario.Rows(i).Cells(10).Text
                pdUbicaCodigo = Nz(gvNEUsuario.Rows(i).Cells(11).Text)
            End If
        Next
        Dim Rs As SqlDataReader
        Dim pdCodReg As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim ValorSys As String = ""
        ValorSys = Session("User") & FechaActual() & HoraActual()
        Cn.Open() : CmdGlobal.Connection = Cn
        If psSerieNumerar <> 0 And pdCantMarcar = 1 Then
            If pdUbicaInvConciliar = 379 Then
                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET  INVDET_ESTADO_CONCILIADO = '3' , INVDET_PLACA_NRO = " & pdNroPlaca & ", INVDET_CONCILIADO = 'X', INVDET_CONCILIADO_SERIE_NUMERAR =" & lblModalSerieNumerar.Text & "  " _
                                              & " WHERE (INVDET_SERIE_NUMERAR=" & psSerieNumerar & ") AND (INVDET_INVENTUBIC_CODIGO=" & pdUbicaInvConciliar & ") " _
                                              & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0') "
                CmdGlobal.ExecuteNonQuery()
            End If
            If pdUbicaInvConciliar = Nz(lblCodInventarioUbica.Text) Then
                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '8', INVDET_ESTADO_CONCILIADO = '1' , INVDET_PLACA_NRO = " & pdNroPlaca & ", INVDET_CONCILIADO = 'X', INVDET_CONCILIADO_SERIE_NUMERAR =" & lblModalSerieNumerar.Text & "  " _
                                              & " WHERE (INVDET_SERIE_NUMERAR=" & psSerieNumerar & ") AND (INVDET_INVENTUBIC_CODIGO=" & Nz(lblCodInventarioUbica.Text) & ") " _
                                              & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0') "
                CmdGlobal.ExecuteNonQuery()
                Dim pdCorrelativo As Double = 0
                CmdGlobal.CommandText = " SELECT MAX(VERIF_CORRELATIVO) FROM TBINVENTARIO_VERIFICACION"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pdCorrelativo = Nz(Rs(0)) + 1
                    End While
                Else
                    pdCorrelativo = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = " INSERT INTO TBINVENTARIO_VERIFICACION (EMPRESA_CODIGO, INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR, VERIF_PLACA_NRO, VERIF_SERIE_NRO, VERIF_ESTADO_ACTIVO ," _
                                                & " VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO,  VERIF_ESTADO_BIEN,  VERIF_ESTADO, VERIF_ART_CODIGO,VERIF_ESTADO_INVENTARIO, " _
                                                & " VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_FECHA, VERIF_HORA,VERIF_SERIE_NRO_REAL,VERIF_PLACA_NRO_REAL,VERIF_REGULARIZAR,VERIF_CORRELATIVO, VERIF_ART_CODIGO_REAL,VERIF_CONCILIADO,VERIF_SERIE_NUMERAR_CONCILIADO) VALUES ( " _
                                                & " '" & Session("CodEmpresa") & "'  , " & Nz(lblCodInventarioUbica.Text) & ", " & psSerieNumerar & ", " & pdNroPlaca & ", '" & psSerieNro & "', '0', " _
                                                & " '" & psUbicatipo & "'," & pdUbicaCodigo & ",'1',  '8', " & pdCodArt & ",'8', " _
                                                & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & psSerieNro & "'," & pdNroPlaca & " ,'2' ," & pdCorrelativo & " , " & pdCodArt & ",'X'," & lblModalSerieNumerar.Text & ")"
                CmdGlobal.ExecuteNonQuery()

                If pdCodUbicacionArea > 0 Then
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_AREA_UBICACION = " & pdCodUbicacionArea & " " _
                                          & " WHERE INVENTUBIC_CODIGO = " & psCodInvUbica & " AND VERIF_SERIE_NUMERAR = " & psSerieNumerar
                    CmdGlobal.ExecuteNonQuery()
                End If
            End If
            If pdUbicaInvConciliar <> Nz(lblCodInventarioUbica.Text) Then
                CmdGlobal.CommandText = " INSERT INTO TBINVENTARIO_DETALLE (EMPRESA_CODIGO,INVDET_INVENTUBIC_CODIGO,INVDET_ART_CODIGO, INVDET_SERIE_ESTADO_EQUIPO, INVDET_ESTADO_ACTIVO,  " _
                                                & " INVDET_SERIE_NUMERAR, INVDET_SERIE_NRO, INVDET_SYS_EST,INVDET_FECHA,INVDET_ESTADO_INGRESO,INVDET_ESTADO_INVENTARIO,INVDET_ESTADO_CONCILIADO ,INVDET_ESTADO_REGULARIZAR, " _
                                                & " INVDET_UBIC_TIPO,INVDET_UBIC_CODIGO,INVDET_SYS_CRE,INVDET_ART_TIPO,INVDET_CANTIDAD, INVDET_PLACA_NRO,INVDET_PLACA_NRO_REAL,INVDET_SERIE_NRO_REAL, INVDET_ART_CODIGO_REAL,INVDET_CONCILIADO,INVDET_CONCILIADO_SERIE_NUMERAR)" _
                                                & " VALUES ('" & Session("CodEmpresa") & "'," & Nz(lblCodInventarioUbica.Text) & "," & pdCodArt & ", '1', '0' , " _
                                                & " " & psSerieNumerar & ",'" & psSerieNro & "','0','" & FechaActual() & "','2','8','1', '2'," _
                                                & " '" & psUbicatipo & "'," & pdUbicaCodigo & ",'" & ValorSys & "'," & psTipoArt & ",1, " & pdNroPlaca & "," & pdNroPlaca & ", '" & psSerieNro & "', " & pdCodArt & ",'X'," & Nz(lblModalSerieNumerar.Text) & ") "
                CmdGlobal.ExecuteNonQuery()

                Dim pdCorrelativo As Double = 0
                CmdGlobal.CommandText = " SELECT MAX(VERIF_CORRELATIVO) FROM TBINVENTARIO_VERIFICACION"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pdCorrelativo = Nz(Rs(0)) + 1
                    End While
                Else
                    pdCorrelativo = 1
                End If
                Rs.Close()

                CmdGlobal.CommandText = " INSERT INTO TBINVENTARIO_VERIFICACION (EMPRESA_CODIGO, INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR, VERIF_PLACA_NRO, VERIF_SERIE_NRO, VERIF_ESTADO_ACTIVO ," _
                                                & " VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO,  VERIF_ESTADO_BIEN,  VERIF_ESTADO, VERIF_ART_CODIGO,VERIF_ESTADO_INVENTARIO, " _
                                                & " VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_FECHA, VERIF_HORA,VERIF_SERIE_NRO_REAL,VERIF_PLACA_NRO_REAL,VERIF_REGULARIZAR,VERIF_CORRELATIVO, VERIF_ART_CODIGO_REAL,VERIF_CONCILIADO,VERIF_SERIE_NUMERAR_CONCILIADO) VALUES ( " _
                                                & " '" & Session("CodEmpresa") & "'  , " & Nz(lblCodInventarioUbica.Text) & ", " & psSerieNumerar & ", " & pdNroPlaca & ", '" & psSerieNro & "', '0', " _
                                                & " '" & psUbicatipo & "'," & pdUbicaCodigo & ",'1',  '8', " & pdCodArt & ",'8', " _
                                                & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & psSerieNro & "'," & pdNroPlaca & " ,'2' ," & pdCorrelativo & " , " & pdCodArt & ",'X'," & Nz(lblModalSerieNumerar.Text) & ")"
                CmdGlobal.ExecuteNonQuery()
                If pdCodUbicacionArea > 0 Then
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_SERIE_AREA = " & pdCodUbicacionArea & " " _
                                          & " WHERE INVDET_INVENTUBIC_CODIGO = " & psCodInvUbica & " AND INVDET_SERIE_NUMERAR = " & psSerieNumerar
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_AREA_UBICACION = " & pdCodUbicacionArea & " " _
                                          & " WHERE INVENTUBIC_CODIGO = " & psCodInvUbica & " AND VERIF_SERIE_NUMERAR = " & psSerieNumerar
                    CmdGlobal.ExecuteNonQuery()
                End If
            End If

            If lblModalSerieNumerar.Text <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_CONCILIADO = '3',INVDET_CONCILIADO_SERIE_NUMERAR = " & psSerieNumerar & ", INVDET_SERIE_ESTADO_EQUIPO = '1', INVDET_CONCILIADO = 'X'  " _
                                                & " WHERE (INVDET_SERIE_NUMERAR=" & Nz(lblModalSerieNumerar.Text) & ") AND (INVDET_INVENTUBIC_CODIGO=" & Nz(lblCodInventarioUbica.Text) & ") " _
                                                & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET  VERIF_ESTADO_CONCILIADO = '3' , VERIF_SERIE_NUMERAR_CONCILIADO  = " & psSerieNumerar & ", VERIF_CONCILIADO = 'X' " _
                                                & " WHERE (VERIF_SERIE_NUMERAR=" & Nz(lblModalSerieNumerar.Text) & ") AND (INVENTUBIC_CODIGO=" & Nz(lblCodInventarioUbica.Text) & ") " _
                                                & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                CmdGlobal.ExecuteNonQuery()

                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_SYS_EST = '1' where SERIE_NUMERAR = " & Nz(lblModalSerieNumerar.Text)
                CmdGlobal.ExecuteNonQuery()

                If pdNroPlaca <> 0 Then
                    CmdGlobal.CommandText = " DELETE FROM TBINV_PLACA_CORRELATIVA  WHERE PLACA_CORRELATIVA = " & pdNroPlaca
                    CmdGlobal.ExecuteNonQuery()
                End If
            End If

            CmdGlobal.CommandText = " SELECT MAX(CONCILIA_CODIGO) FROM TBINVENTARIO_EQUIPOS_CONCILIADOS"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdCodReg = Nz(Rs(0)) + 1
                End While
            Else
                pdCodReg = "1"
            End If
            Rs.Close()

            CmdGlobal.CommandText = " INSERT INTO  TBINVENTARIO_EQUIPOS_CONCILIADOS ( EMPRESA_CODIGO, CONCILIA_CODIGO, INVENTARIO_CODIGO, INVENT_UBICAC_TIPO, INVENT_UBICAC_CODIGO,INVENT_UBIC_INVENTARIO, " _
                                                & " CONCILIA_SERIE_NUMERAR, CONCILIA_SERIE_NRO, CONCILIA_PLACA_NRO, CONCILIA_ART_CODIGO, INVENT_UBIC_SERIE_NUMERAR, INVENT_UBIC_SERIE_NRO, " _
                                                & " INVENT_UBIC_PLACA_NRO, INVENT_UBIC_ART_CODIGO, CONCILIA_ESTADO, CONCILIA_REG_FECHA, CONCILIA_REG_HORA, CONCILIA_REG_USER, CONCILIA_SYS_CRE, " _
                                                & " CONCILIA_SYS_EST, CONCILIA_INVENTUBIC_CODIGO,	CONCILIA_INVENT_UBIC_TIPO,	CONCILIA_INVENT_UBIC_CODIGO ) " _
                                                & " VALUES ('" & Session("CodEmpresa") & "', " & pdCodReg & ", " & psCodInventario & ", '" & lblModalUbicaTipo.Text & "', " & Nz(lblModalUbicaCodigo.Text) & ", " & Nz(lblCodInventarioUbica.Text) & ", " _
                                                & " " & psSerieNumerar & ", '" & psSerieNro & "', " & pdNroPlaca & ", " & pdCodArt & ", " & Nz(lblModalSerieNumerar.Text) & ", '" & txtModalNroSerie.Text & "', " _
                                                & " " & Nz(txtModalNroPlaca.Text) & ", " & Nz(txtModalArtCodigo.Text) & ", '1', '" & FechaActual() & "', '" & HoraActual() & "','" & Session("User") & "','" & Session("User") & FechaActual() & HoraActual() & "' , " _
                                                & " '0', " & pdUbicaInvConciliar & ", '" & psUbicatipo & "', " & pdUbicaCodigo & ")"
            CmdGlobal.ExecuteNonQuery()
            BtnListar_Click(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalConciliar').modal('hide');", True)

        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Debe seleccionar un bien para conciliar.');", True)
        End If
    End Sub

    Private Sub GvListaCantidadesXActivos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaCantidadesXActivos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pdCodArt As Double = 0
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim dtO As New DataTable
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0

        If e.CommandName = "Detalle" Then
            pdCodArt = Nz(GvListaCantidadesXActivos.Rows(Index).Cells(3).Text)
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then pdCodInv = Nz(DdlInventario.SelectedValue)
            pdCodUbicInv = Nz(lblcodUbicaInv.Text)
            Try
                dt = obj.Inventario_Verificacion_ListaxArticulo(Session("Ruta_Emp"), pdCodInv, pdCodUbicInv, pdCodArt)
                GvListaVerificarInventario.DataSource = dt
                GvListaVerificarInventario.DataBind()
                If dt.Rows.Count > 1 Then
                    lblRegDet.Text = "Hay " & dt.Rows.Count & " registros."
                ElseIf dt.Rows.Count = 1 Then
                    lblRegDet.Text = "Hay 1 registro."
                ElseIf dt.Rows.Count = 0 Then
                    lblRegDet.Text = "Hay 0 registro."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
            Catch ex As SqlException
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & " ');", True)
            Catch ex As Exception
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicacion: " & ex.Message & " ');", True)
            Finally
            End Try
        End If
    End Sub

    Private Sub BtnCerrarModal_Click(sender As Object, e As EventArgs) Handles BtnCerrarModal.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub


    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click

        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        GvListaVerificarInventarioNuevos.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(GvListaVerificarInventarioNuevos)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Inv_Estadistica.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()

    End Sub

    Private Sub BtnListarNE_Click(sender As Object, e As EventArgs) Handles BtnListarNE.Click
        Dim objUbic As New Cls_Inventario
        Dim pdCodInvUbi As Double = 0
        Dim dt As New DataTable
        dt = Nothing
        gvNEUsuario.Visible = False
        LblCantNE.Text = ""
        Dim pdCodArt As Double = 0
        Dim psDescripcion As String = ""

        If TxtBusModalArtCod.Text <> "" Then
            pdCodArt = TxtBusModalArtCod.Text
        End If

        If TxtBusModalArtNombre.Text <> "" Then
            psDescripcion = TxtBusModalArtNombre.Text
        End If

        Try
            dt = objUbic.Inventario_NoEncontrados_Lista_xUsuario(Session("Ruta_Emp"), Session("User"), psDescripcion, pdCodArt)
            gvNEUsuario.DataSource = dt
            gvNEUsuario.DataBind()
            If dt.Rows.Count = 0 Then
                LblCantNE.Text = "No hay registros"
            ElseIf dt.Rows.Count = 1 Then
                LblCantNE.Text = "Hay 1 registro."
                gvNEUsuario.Visible = True
            ElseIf dt.Rows.Count > 0 Then
                LblCantNE.Text = "Hay " & dt.Rows.Count & " registros."
                gvNEUsuario.Visible = True
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub ListaEquiposConciliados()
        Try
            Dim obj As New Cls_Inventario
            Dim dt As New DataTable
            lblRegistro.Text = ""
            GvLista.DataSource = Nothing
            GvLista.DataBind()
            Dim pdCodInvUbica As Double = 0
            Dim pdCodInv As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If
            If lblcodUbicaInv.Text <> "" Then
                pdCodInvUbica = lblcodUbicaInv.Text
            End If
            dt = obj.Invenatrio_Conciliar_Listas(Session("Ruta_Emp"), pdCodInvUbica, 0, pdCodInv)
            GvLista.DataSource = dt
            GvLista.DataBind()
            If dt.Rows.Count > 1 Then
                lblRegistro.Text = "Hay " & dt.Rows.Count & " equipos conciliados."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro.Text = "Hay 1 equipo conciliado."
            Else
                lblRegistro.Text = "No hay equipos conciliado."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "';", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicacion: " & ex.Message & "';", True)
        End Try
    End Sub

End Class