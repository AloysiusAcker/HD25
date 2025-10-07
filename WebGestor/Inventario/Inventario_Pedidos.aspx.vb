Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inventario_Inventario_Pedidos
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objProceso As New clsInv_Procesos
    Dim oFunc As New clsCont_Funciones
    Dim oFuncInv As New clsInv_Procesos
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            DdlTipoPedido.Items.Clear()
            Dim lst1 As New ListItem : lst1.Text = "Recojo de equipos" : lst1.Value = 1 : DdlTipoPedido.Items.Add(lst1)
            Dim lst2 As New ListItem : lst2.Text = "Requerimiento" : lst2.Value = 2 : DdlTipoPedido.Items.Add(lst2)
            Dim lst3 As New ListItem : lst3.Text = "Envio de Equipos" : lst3.Value = 3 : DdlTipoPedido.Items.Add(lst3)
            DdlTipoPedido.Items.Add("< Seleccionar >") : DdlTipoPedido.SelectedValue = "< Seleccionar >"
            DdlTipoAtencion.Items.Clear()
            Dim lst4 As New ListItem : lst4.Text = "Requerimiento" : lst4.Value = 1 : DdlTipoAtencion.Items.Add(lst4)
            Dim lst5 As New ListItem : lst5.Text = "Req - Onboarding" : lst5.Value = 2 : DdlTipoAtencion.Items.Add(lst5)
            DdlTipoAtencion.Items.Add("< Seleccionar >") : DdlTipoAtencion.SelectedValue = "< Seleccionar >"
            Call LLenar_TipoaArticulo()
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

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles BtnAgregar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
    End Sub

    Protected Sub GvBuscarArticulos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            Dim psArtCodigo As Double = 0
            psArtCodigo = GvBuscarArticulos.Rows(Index).Cells(1).Text
            TxtCodArticuloBA.Value = ""
            TxtDescripcionBA.Value = ""
            GvBuscarArticulos.DataSource = Nothing
            GvBuscarArticulos.DataBind()
            Dim dtListado As New DataTable
            Dim drT As DataRow
            Dim i As Long = 0
            Dim a As Long = 0
            dtListado.Columns.Add("c0")
            dtListado.Columns.Add("c1")
            dtListado.Columns.Add("c2")
            dtListado.Columns.Add("c3")
            dtListado.Columns.Add("c4")
            If FlexItem.Rows.Count > 0 Then
                For i = 0 To FlexItem.Rows.Count - 1
                    a = a + 1
                    drT = dtListado.NewRow()
                    Dim psCant As TextBox
                    psCant = CType(FlexItem.Rows(i).Cells(6).FindControl("txtCant"), TextBox)
                    drT("c0") = Llenar_Ceros(a, 3)
                    drT("c1") = FlexItem.Rows(i).Cells(2).Text
                    drT("c2") = FlexItem.Rows(i).Cells(3).Text
                    drT("c3") = FlexItem.Rows(i).Cells(4).Text
                    drT("c4") = FlexItem.Rows(i).Cells(5).Text
                    CType(FlexItem.Rows(i).Cells(6).FindControl("txtCant"), TextBox).Text = psCant.Text
                    dtListado.Rows.Add(drT)
                Next
            End If
            Dim dt As New DataTable
            dt = obj.Lista_ArtxCodigo(Session("Ruta_Emp"), Session("Codempresa"), psArtCodigo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    a = a + 1
                    drT = dtListado.NewRow()
                    drT("c0") = Llenar_Ceros(a, 3)
                    drT("c1") = Nu(dr("COD_ARTICULO"))
                    drT("c2") = Nu(dr("ART_CODEQUIVA"))
                    drT("c3") = Nu(dr("ART_DESCRIPCION"))
                    drT("c4") = Nu(dr("TIPO"))
                    dtListado.Rows.Add(drT)
                Next
            End If
            FlexItem.DataSource = dtListado
            FlexItem.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
            Limpiar_Cajas_Buscar_Articulos()
        End If
    End Sub
    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub Limpiar_Cajas_Buscar_Articulos()
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
    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Try
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
            If tipo = "< Seleccionar >" Then tipo = ""

            dt = obj.Bus_Articulo(psconexion, codigo, clasificacion, descripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
            GvBuscarArticulos.DataSource = dt
            GvBuscarArticulos.DataBind()

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub trvClasificacion_TreeNodePopulate(sender As Object, e As TreeNodeEventArgs) Handles trvClasificacion.TreeNodePopulate
        Dim obj As New Cls_Clasificacion
        Dim dt As DataTable = obj.NumeroNodo(Session("Ruta_Emp"), CInt(e.Node.Value))
        Dim dbRow As DataRow = dt.Rows(0)
        Dim nivelPrincipal As Integer = CInt(dbRow(1).ToString)
        Dim nodo As Integer = CInt(dbRow(0).ToString) + 1
        Dim nodoAyuda As Integer = CInt(dbRow(0).ToString)
        Dim codigo As Integer = CInt(e.Node.Value)
        If nodo = 2 Then
            dt = obj.NodosHijos1(Session("Ruta_Emp"), nivelPrincipal, nodo)
            NodosPopulares(dt, e.Node.ChildNodes)
        Else
            dt = obj.NodosHijos(Session("Ruta_Emp"), nivelPrincipal, nodo, nodoAyuda, codigo)
            NodosPopulares(dt, e.Node.ChildNodes)
        End If
    End Sub

    Private Sub PopularRootLevel()
        Dim objConn As New SqlConnection(Session("Ruta_Emp"))

        Dim objComand As New SqlCommand(" Select CLAS_CODIGO As CODIGO, " +
                                        " CLAS_NUMERO +' - '+ CLAS_NOMBRE as clasificacion, " +
                                        " (SELECT count(clas_codigo) " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c2  WHERE c2.CLAS_NIVEL1=c1.CLAS_CODIGO and clas_cod_nivel = 2 ) as CountHijos " +
                                        " FROM TBINV_ARTICULO_CLASIFICACION c1  WHERE CLAS_COD_NIVEL=1 and clas_sys_est = '0' ORDER BY CLAS_NUMERACION", objConn)
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
    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        TituloPopupp.Text = "Busca Clasificaciones"
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)
    End Sub

    Private Sub btnModalBuscarClas_Click(sender As Object, e As EventArgs) Handles btnModalBuscarClas.Click
        PopularRootLevel()
    End Sub

    Private Sub BtnCC_Click(sender As Object, e As EventArgs) Handles BtnCC.Click
        TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New clsInv_Listados
        Dim objMa As New Cls_Marcas
        Dim objMo As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim dtU As New DataTable
        Dim dtM As New DataTable
        Dim inventario As String = ""
        Dim codigo As Double = 0
        Dim CodInterno As String = ""
        Dim descripcion As String = ""
        Dim codMarca As String = ""

        Try

            CodInterno = BuscarCodigo.Value.ToString
            descripcion = BuscarDescripcion.Value.ToString

            dt = obj.Lista_Oficina(Session("Ruta_Emp"), Session("CodEmpresa"), CodInterno, descripcion)

            GvBusqueda.DataSource = dt
            GvBusqueda.DataBind()


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try

    End Sub
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

            Limpiar_Cajas_Popup()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCC_CodInterno.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtCC_Descripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            TxtCC_Codigo.Text = GvBusqueda.Rows(Index).Cells(3).Text

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()

    End Sub
    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub

    Private Sub DdlTipoPedido_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlTipoPedido.SelectedIndexChanged
        If DdlTipoPedido.SelectedValue = "2" Then
            DdlTipoAtencion.Visible = True
            lblEtiq12.Visible = True
            DdlTipoAtencion.SelectedValue = "< Seleccionar >"
        Else
            DdlTipoAtencion.Visible = False
            lblEtiq12.Visible = False
            DdlTipoAtencion.SelectedValue = "< Seleccionar >"
        End If
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles BtnGuardar.Click
        Response.Redirect("Inventario_Pedidos.aspx")
    End Sub

    Private Sub FlexItem_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexItem.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Quitar" Then
            FlexItem.Rows(Index).Visible = False
        End If
    End Sub
End Class
