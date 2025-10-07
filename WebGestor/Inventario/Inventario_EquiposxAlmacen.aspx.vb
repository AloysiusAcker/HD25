Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Partial Class Inventario_EquiposxAlmacen
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblError.Text = ""
            'btnListar_Click(sender, e)
            'Page.Session.Timeout = 1080
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
    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        txtUbicacion.Text = ""
        TxtUbiCodigo.Text = ""
        TxtUbiDescripcion.Text = ""
        lblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        Flex.DataSource = dt
        Flex.DataBind()
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        txtUbicacion.Text = ""
        TxtUbiCodigo.Text = ""
        TxtUbiDescripcion.Text = ""
        lblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        Flex.DataSource = dt
        Flex.DataBind()
    End Sub
    Private Sub RBUbicaciones_CheckedChanged(sender As Object, e As EventArgs) Handles RBUbicaciones.CheckedChanged
        txtUbicacion.Text = ""
        TxtUbiCodigo.Text = ""
        TxtUbiDescripcion.Text = ""
        lblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        Flex.DataSource = dt
        Flex.DataBind()
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        Dim objProceso As New clsInv_Procesos
        lblError.Text = ""
        Dim pCodArt As Integer
        Dim TipoLista As String
        Dim pdCodRelacionado As String = ""
        Dim psNroPlaca As Double = 0
        Dim pdCodAlmacen As Double = 0
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProceso.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        If txtCodArt.Text.Trim <> "" Then
            pCodArt = txtCodArt.Text.Trim : TipoLista = "1"
        Else
            pCodArt = 0 : TipoLista = "0"
        End If
        Dim dt As DataTable
        If txtUbicacion.Text.Trim <> "" Then pdCodAlmacen = txtUbicacion.Text.Trim
        If txtPlaca.Text.Trim <> "" Then psNroPlaca = txtPlaca.Text.Trim
        If TxtCodRelacionado.Text.Trim <> "" Then pdCodRelacionado = TxtCodRelacionado.Text

        Dim psTipoUbicacion As String = "0"
        If RBAlmacen.Checked = True Then psTipoUbicacion = "1"
        If RBCentroC.Checked = True Then psTipoUbicacion = "2"
        Try

            dt = obj.Lista_EquiposAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), pCodArt, psTipoUbicacion, pdCodAlmacen, TipoLista, txtNroSerie.Text.Trim, psNroPlaca, pdCodRelacionado)
            Flex.DataSource = dt
            Flex.DataBind()
            lblRegistro.Text = "Se encontrarón " & dt.Rows.Count & " registros."
            '  Page.Session.Timeout = 1080


        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Private Sub Exportar_Excel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        Flex.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(Flex)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Lista_de_Bienes.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Private Sub Exportar_Excel2(ByVal dt As DataTable)
        Dim StwWriter As New System.IO.StringWriter
        Dim htwWriter As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(StwWriter)
        Dim dgGrid As DataGrid = New DataGrid
        dgGrid.DataSource = dt
        dgGrid.HeaderStyle.Font.Bold = True
        dgGrid.DataBind()
        dgGrid.RenderControl(htwWriter)
        Response.ContentType = "application/vnd.ms-excel"
        Me.EnableViewState = False
        Response.Write(StwWriter.ToString)
        Response.End()
    End Sub
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportar.Click
        Call Exportar_Excel()
    End Sub
    Protected Sub txtUbiCodigo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUbicacion.Text = ""
        txtUbiDescripcion.Text = ""
    End Sub

    Private Sub BtnUbica_Click(sender As Object, e As EventArgs) Handles BtnUbica.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub


    Protected Sub BtnArtBuscar_Click(sender As Object, e As EventArgs) Handles BtnArtBuscar.Click
        Call Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click

        Dim obj As New clsInv_Listados
        Dim dt As New DataTable
        Dim dtU As New DataTable
        Dim dtM As New DataTable
        Dim inventario As String = ""
        Dim psBusCodigo As Double = 0
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""
        Dim codMarca As String = ""

        Try

            If TituloPopup.Text = "Búsqueda Almacén" Then
                If BuscarCodigo.Value.ToString <> "" Then psBusCodigo = BuscarCodigo.Value
                descripcion = BuscarDescripcion.Value.Trim.ToString
                dt = obj.Lista_BusquedaAlmacen(Session("Ruta_Emp"), Session("CodEmpresa"), psBusCodigo, descripcion)
            ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
                If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
                descripcion = BuscarDescripcion.Value.Trim.ToString
                dt = obj.Lista_BusquedaCentroCosto(Session("Ruta_Emp"), Session("CodEmpresa"), psBusCodInterno, descripcion)
            End If

            GvBusqueda.DataSource = dt
            GvBusqueda.DataBind()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try

    End Sub
    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        If TituloPopup.Text = "Busca Sección de Centro de Costo" Or TituloPopup.Text = "Busca Almacén" Or TituloPopup.Text = "Busca Ubicaciones" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
        ElseIf TituloPopup.Text = "Busca Marca" Or TituloPopup.Text = "Busca Modelo" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtUbiCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtUbiDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            txtUbicacion.Text = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()

    End Sub

    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        LblCodClasificacionBA.Text = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        TxtSku.Value = ""
        lblCodClas.Text = ""
        Dim dtArt As New DataTable
        dtArt = Nothing
        GvBusArticulo.DataSource = dtArt
        GvBusArticulo.DataBind()
    End Sub
    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Call Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        'Dim dt As New DataTable
        'Dim psListaArt As String = "1"
        'Dim psListaMarca As String = "1"
        'Dim psListaModelo As String = "1"
        'Dim psconexion As String = Session("Ruta_Emp")
        'Dim Codigo As Double = 0
        'Codigo = Nz(TxtCodArticuloBA.Value.ToString)
        'Dim Clasificacion As String = LblCodClasificacionBA.Text.ToString

        'Dim Descripcion As String = TxtDescripcionBA.Value.ToString
        'Dim Tipo As String = DdlTipoBA.SelectedValue.ToString
        'Dim NuPart As String = TxtNumParteBA.Value.ToString
        'Dim CodEs As String = TxtCodEspecificoBA.Value.ToString
        'Dim marca As Double = 0
        'marca = Nz(LblCodMarcaBA.Text.ToString)
        'Dim modelo As Double = 0
        'modelo = Nz(LblCodModeloBA.Text.ToString)

        'If marca <> 0 Then psListaMarca = ""
        'If modelo <> 0 Then psListaModelo = ""
        'If Codigo <> 0 Then psListaArt = ""
        'If Tipo = "< Seleccionar >" Then Tipo = ""

        'dt = objCat.Lista_ArticuloxBusqueda(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)

        'If dt.Rows.Count > 0 Then
        '    GvBusArticulo.DataSource = dt
        '    GvBusArticulo.DataBind()
        'Else
        '    GvBusArticulo.DataSource = Nothing
        '    GvBusArticulo.DataBind()
        'End If


        Try
            Dim obj As New Cls_Catalogo
            Dim objCn As New Cls_Conexion
            Dim objListaInv As New Cls_Inventario_Verificacion
            Dim dt As New DataTable
            Dim psListaArt As String = "1"
            Dim psListaMarca As String = "1"
            Dim psListaModelo As String = "1"
            Dim psconexion As String = Session("Ruta_Emp")
            Dim pdCodArt As Double = 0
            If TxtCodArticuloBA.Value <> "" Then
                pdCodArt = Nz(TxtCodArticuloBA.Value.ToString)
            End If
            Dim clasificacion As String = ""
            Dim psDescripcion As String = TxtDescripcionBA.Value.ToString
            Dim tipo As String = DdlTipoBA.SelectedValue.ToString
            Dim numPart As String = TxtNumParteBA.Value.ToString
            Dim especifico As String = TxtCodEspecificoBA.Value.ToString
            Dim psSku As String = ""
            Dim marca As Double = 0
            Dim modelo As Double = 0
            Dim pdCodUbicacion As Double = 0

            If marca <> 0 Then psListaMarca = ""
            If modelo <> 0 Then psListaModelo = ""
            If pdCodArt <> 0 Then psListaArt = ""
            If tipo = "< Seleccionar >" Then tipo = ""

            Dim psCodArtSku As String = ""

            If TxtSku.Value <> "" Then
                psSku = TxtSku.Value
            End If

            Dim drT As DataRow
            Dim dtColum As New DataTable
            Dim psClasNumero As String = ""
            If TxtClasificacionBA.Value <> "" Then clasificacion = TxtClasificacionBA.Value
            Dim psPosicionguion As Double = 0

            psPosicionguion = InStr(clasificacion, "-")
            If psPosicionguion > 0 Then
                psClasNumero = Left(clasificacion, psPosicionguion - 1)
                psClasNumero = Trim(psClasNumero)
            End If
            dtColum.Columns.Add("ART_CODIGO")
            dtColum.Columns.Add("ART_CODEQUIVA")
            dtColum.Columns.Add("ART_DESCRIPCION")
            dtColum.Columns.Add("TIPO_ART")
            dtColum.Columns.Add("ART_SKU")

            If psSku <> "" Then

                Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim CmdGlobal2 As New SqlCommand
                Cn.Open() : CmdGlobal.Connection = Cn
                Cn2.Open() : CmdGlobal2.Connection = Cn2
                Dim Rs As SqlDataReader

                CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS WHERE UPPER(ART_SKU) = '" & UCase(psSku) & "'  "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodArtSku = Nu(Rs("ART_CODIGO"))
                        psDescripcion = Nu(Rs("ART_DESCRIPCION"))
                        TxtDescripcionBA.Value = Nu(Rs("ART_DESCRIPCION"))
                    End While
                End If
                Rs.Close()
                If psCodArtSku = "" Then

                    CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS_IMAGENES WHERE ART_SKU = '" & psSku & "'  "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psDescripcion = Nu(Rs("ART_DESCRIPCION"))
                            TxtDescripcionBA.Value = Nu(Rs("ART_DESCRIPCION"))
                        End While
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = " SELECT * FROM TBINV_ARTICULOS WHERE UPPER(ART_DESCRIPCION) = '" & UCase(TxtDescripcionBA.Value) & "'  "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            psCodArtSku = Nu(Rs("ART_CODIGO"))
                            CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS SET ART_SKU = '" & psSku & "' WHERE ART_CODIGO =  " & psCodArtSku
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    Rs.Close()
                End If


            End If

            dt = obj.Lista_ArticuloxBusqueda(psconexion, pdCodArt, psClasNumero, psDescripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
            If dt.Rows.Count > 0 Then
                For Each drDato As DataRow In dt.Rows
                    drT = dtColum.NewRow()
                    drT("ART_CODIGO") = Nu(drDato("ART_CODIGO"))
                    drT("ART_CODEQUIVA") = Nu(drDato("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drDato("ART_DESCRIPCION"))
                    drT("TIPO_ART") = Nu(drDato("TIPO_ART"))
                    drT("ART_SKU") = Nu(drDato("ART_SKU"))
                    dtColum.Rows.Add(drT)
                Next
            End If

            If dtColum.Rows.Count > 0 Then
                GvBusArticulo.DataSource = dtColum
                GvBusArticulo.DataBind()
            Else
                GvBusArticulo.DataSource = Nothing
                GvBusArticulo.DataBind()
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try

    End Sub

    Private Sub GvBusArticulo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusArticulo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            txtCodArt.Text = GvBusArticulo.Rows(Index).Cells(1).Text
            txtNomArt.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Call Limpiar_Cajas_Buscar_Articulos()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
        End If
    End Sub

    Private Sub BtnNuevoBA_Click(sender As Object, e As EventArgs) Handles BtnNuevoBA.Click
        Dim obj As New Cls_Catalogo
        Dim psCodClasif As Double = 0
        Dim pdCodArt As Double = 0
        Dim pdTipoArt As Double = 0
        Try
            If DdlTipoBA.SelectedValue <> "< Seleccionar >" Then
                pdTipoArt = Nz(DdlTipoBA.SelectedValue)
            End If
            pdCodArt = obj.Codigo(Session("Ruta_Emp"))
            If lblCodClas.Text <> "" Then psCodClasif = lblCodClas.Text
            Dim psArtDescripcion As String = ""
            If TxtDescripcionBA.Value <> "" Then psArtDescripcion = TxtDescripcionBA.Value
            If psArtDescripcion = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar descripción del bien.');", True)
            ElseIf psCodClasif = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Clasificación.');", True)
            ElseIf pdTipoArt = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Tipo.');", True)
            Else
                obj.RegistrarCatalogo(Session("Ruta_Emp"), pdCodArt, pdTipoArt, psCodClasif, 0, 0, 0, psArtDescripcion, Left(psArtDescripcion, 19), TxtNumParteBA.Value, "", 34, 0, "", 0, 0, 0, 0, 0, Session("User"), TxtSku.Value)
            End If

            BtnNuevoBA.Visible = True
            BtnBuscarBA_Click(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        TituloPopupp.Text = "Busca Clasificaciones"
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)

    End Sub
    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalArticulo').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub
    Private Sub btnModalBuscarClas_Click(sender As Object, e As EventArgs) Handles btnModalBuscarClas.Click
        PopularRootLevel()
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
    Private Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        trvClasificacion.SelectedNode.Selected = True
        TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
        Dim psNumero As Integer = 0
        lblCodClas.Text = trvClasificacion.SelectedValue
        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalArticulo').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub

End Class
