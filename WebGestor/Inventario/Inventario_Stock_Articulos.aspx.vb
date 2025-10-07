Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports OfficeOpenXml

Partial Class Inventario_Stock_Articulos
    Inherits System.Web.UI.Page
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LblError.Text = ""
            Carga_Almacenes()
            Call LLenar_TipoaArticulo()
            DdlTipoBA.SelectedValue = "< Seleccionar >"
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
    Private Sub Carga_Almacenes()
        Dim obj As New clsInv_Listados
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        objProcesos.Almacen_Autorizado(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        cboAlmacen.DataSource = obj.Lista_Almacenes(psConexion, Session("CodEmpresa"))
        cboAlmacen.DataTextField = "ALMACEN_NOMBRE"
        cboAlmacen.DataValueField = "ALMACEN_CODIGO"
        cboAlmacen.DataBind()
        cboAlmacen.Items.Add("< Todos los Almacenes >") : cboAlmacen.SelectedValue = "< Todos los Almacenes >"
        cboAlmacen.Items.Add("< Seleccionar >") : cboAlmacen.SelectedValue = "< Seleccionar >)"
    End Sub
    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Dim obj As New clsInv_Listados
        LblError.Text = ""
        Dim pCodArt As Integer
        Dim TipoLista As String
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        If cboAlmacen.SelectedValue = "< Seleccionar >" Then
            objProcesos.Almacen_Autorizado(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        ElseIf cboAlmacen.SelectedValue = "< Todos los Almacenes >" Then
            pdCodAlmacen = 0
            objProcesos.Almacen_Autorizado(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        Else
            pdCodAlmacen = cboAlmacen.SelectedValue.Trim
            objProcesos.Almacen_Autorizado(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, pdCodAlmacen)
        End If
        If TxtCodArt.Text <> "" Then
            pCodArt = TxtCodArt.Text : TipoLista = "1"
        Else
            pCodArt = 0 : TipoLista = "0"
        End If
        If cboAlmacen.SelectedValue = "< Seleccionar >" Then LblError.Text = "Debe seleccionar Almacén." : Exit Sub
        Try
            Flex.DataSource = obj.Lista_StockArticulos(psConexion, Session("CodEmpresa"), pdCodAlmacen, pCodArt, TipoLista, lblCodClas.Text)
            Flex.DataBind()
            LblRegistro.Text = "Se encontraron " & Flex.Rows.Count & " registros."
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub Flex_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Flex.PageIndexChanging
        LblError.Text = ""
        Dim pCodArt As Integer
        Dim TipoLista As String
        If TxtCodArt.Text <> "" Then pCodArt = TxtCodArt.Text : TipoLista = "1" Else pCodArt = 0 : TipoLista = "0"
        If cboAlmacen.SelectedValue = "(Seleccionar)" Then LblError.Text = "Debe seleccionar Almacén." : Exit Sub
        Dim obj As New clsInv_Listados
        Dim psConexion As String = Session("Ruta_Emp") '  ConfigurationManager.AppSettings("cnTecnicos")
        Flex.PageIndex = e.NewPageIndex
        Flex.DataSource = obj.Lista_StockArticulos(psConexion, Session("CodEmpresa"), cboAlmacen.SelectedValue.Trim, pCodArt, TipoLista, "")
        Flex.DataBind()
    End Sub
    Private Sub BtnBuscarClas_Click(sender As Object, e As EventArgs) Handles BtnBuscarClas.Click
        Session("TipoModal") = "1"
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
        TituloPopupp.Text = "Búsqueda de Clasificación"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('show');", True)
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
        Response.AddHeader("Content-Disposition", "attachment;filename=STOCK.xls")
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
    Protected Sub btnExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnExportar.Click
        'lblError.Text = ""
        'Dim dt As New DataTable
        'Dim pCodArt As Integer
        'Dim TipoLista As String
        'Dim obj As New Listados
        'Dim pdCodAlmacen As Double = 0
        'Dim objProcesos As New clsInventario_Procesos
        'If cboAlmacen.SelectedValue <> "(Seleccionar)" Then
        '    pdCodAlmacen = cboAlmacen.SelectedValue.Trim
        '    objProcesos.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, pdCodAlmacen)
        'Else
        '    objProcesos.Almacen_Autorizado(Session("Ruta_Emp"), Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        'End If
        'If txtCodArt.Text <> "" Then pCodArt = txtCodArt.Text : TipoLista = "1" Else pCodArt = 0 : TipoLista = "0"
        'If cboAlmacen.SelectedValue = "(Seleccionar)" Then lblError.Text = "Debe seleccionar Almacén." : Exit Sub
        'Try
        '    dt = obj.Lista_StockArticulos(Session("Ruta_Emp"), Session("CodEmpresa"), cboAlmacen.SelectedValue.Trim, pCodArt, TipoLista)
        'Catch Ex As SqlException
        '    lblError.Visible = True
        '    lblError.Text = "Ha ocurrido un error en el registro de la Base de Datos:<br>" & Ex.Message
        'Catch Ex As Exception
        '    lblError.Visible = True
        '    lblError.Text = "Ha ocurrido un error la Aplicacion :<br>" & Ex.Message
        'Finally
        'End Try


        ''Response.Clear()
        ''Response.Buffer = True
        ''Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls")
        ''Response.Charset = ""
        ''Response.ContentType = "application/ms-excel"

        ''Using sw As New StringWriter()
        ''    Dim hw As New HtmlTextWriter(sw)

        ''    ' Renderiza el GridView en el control HtmlTextWriter
        ''    Flex.RenderControl(hw)

        ''    ' Agrega el contenido a la respuesta
        ''    Response.Output.Write(sw.ToString())
        ''    Response.Flush()
        ''    Response.End()
        ''End Using

        Call Exportar_ListaArticulos()
    End Sub

    Private Sub Exportar_ListaArticulos()
        Dim dt1 As New DataTable
        Dim obj As New clsInv_Listados
        LblError.Text = ""
        Dim pCodArt As Integer
        Dim TipoLista As String
        Dim pdCodAlmacen As Double = 0
        Dim objProcesos As New clsInv_Procesos
        Dim psConexion As String = Session("Ruta_Emp") ' ConfigurationManager.AppSettings("cnTecnicos")
        If cboAlmacen.SelectedValue = "< Seleccionar >" Then
            objProcesos.Almacen_Autorizado(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        ElseIf cboAlmacen.SelectedValue = "< Todos los Almacenes >" Then
            pdCodAlmacen = 0
            objProcesos.Almacen_Autorizado(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name)
        Else
            pdCodAlmacen = cboAlmacen.SelectedValue.Trim
            objProcesos.Almacen_Autorizado(psConexion, Session("CodEmpresa"), HttpContext.Current.User.Identity.Name, pdCodAlmacen)
        End If
        If TxtCodArt.Text <> "" Then
            pCodArt = TxtCodArt.Text : TipoLista = "1"
        Else
            pCodArt = 0 : TipoLista = "0"
        End If
        If cboAlmacen.SelectedValue = "< Seleccionar >" Then LblError.Text = "Debe seleccionar Almacén." : Exit Sub
        Try
            dt1 = obj.ListaExportar_StockArticulos(psConexion, Session("CodEmpresa"), pdCodAlmacen, pCodArt, TipoLista, lblCodClas.Text)


            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("StockArticulos")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)
                Dim numberColumn = worksheet1.Column(6) ' 3 es el índice de la columna C, ajusta según tu necesidad
                numberColumn.Style.Numberformat.Format = "0"
                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=BienesInventariados.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()

            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
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
        Dim dtArt As New DataTable
        dtArt = Nothing
        GvBusArticulo.DataSource = dtArt
        GvBusArticulo.DataBind()
    End Sub
    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBuscar.Click
        Call Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        'Dim dt As New DataTable
        'Dim psListaArt As String = "1"
        'Dim psListaMarca As String = "1"
        'Dim psListaModelo As String = "1"
        'Dim psconexion As String = Session("Ruta_Emp")
        'Dim Codigo As String = TxtCodArticuloBA.Value.ToString
        'Dim Clasificacion As String = LblCodClasificacionBA.Text.ToString

        'Dim Descripcion As String = TxtDescripcionBA.Value.ToString
        'Dim Tipo As String = DdlTipoBA.SelectedValue.ToString
        'Dim NuPart As String = TxtNumParteBA.Value.ToString
        'Dim CodEs As String = TxtCodEspecificoBA.Value.ToString
        'Dim marca As String = LblCodMarcaBA.Text.ToString
        'Dim modelo As String = LblCodModeloBA.Text.ToString

        'If marca <> "" Then psListaMarca = ""
        'If modelo <> "" Then psListaModelo = ""
        'If Codigo <> "" Then psListaArt = ""
        'If Tipo = "< Seleccionar >" Then Tipo = ""

        'dt = objCat.Bus_Articulo(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)

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

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Call Limpiar_Cajas_Buscar_Articulos()

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
    End Sub

    Private Sub BtnBuscaMarcaBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarcaBA.Click
        TituloPopup.Text = "Busca Marcas"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub

    Private Sub BtnBuscaMarca_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarca.Click
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = BuscarCodigo.Value.ToString
        Dim codMarca As String = ""
        Dim CodModelo As String = ""
        Dim descripcion As String = BuscarDescripcion.Value.ToString

        If TituloPopup.Text = "Búsqueda de Marcas" Or TituloPopup.Text = "Busca Marcas" Then
            dt = obj.Buscar_Marca(psconexion, codigo, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda de Modelo" Or TituloPopup.Text = "Busca Modelos" Then
            If TituloPopup.Text = "Busca Modelos" Then
                codMarca = LblCodMarcaBA.Text.ToString
            End If
            If codMarca = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione una Marca');", True)
            Else
                dt = obj.Buscar_Modelo(psconexion, codigo, descripcion, codMarca)
                If dt.Rows.Count() = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Modelos de la Marca seleccionada');", True)
                End If
            End If
        ElseIf TituloPopup.Text = "Búsqueda de Detalle del Modelo" Then
            If CodModelo = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione un Modelo');", True)
            Else
                dt = obj.Buscar_Modelo_Detalle(psconexion, codigo, descripcion, CodModelo)
                If dt.Rows.Count() = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay Detalles del Modelo seleccionado');", True)
                End If
            End If
        End If
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub
    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If TituloPopup.Text = "Busca Modelos" Or TituloPopup.Text = "Busca Marcas" Then
            If e.CommandName = "Aceptar" And TituloPopup.Text = "Busca Marcas" Then
                LblCodMarcaBA.Text = GvBusqueda.Rows(Index).Cells(3).Text
                TxtMarcaBA.Value = GvBusqueda.Rows(Index).Cells(2).Text
                LblCodModeloBA.Text = ""
                TxtModeloBA.Value = ""
            ElseIf e.CommandName = "Aceptar" And TituloPopup.Text = "Busca Modelos" Then
                LblCodModeloBA.Text = GvBusqueda.Rows(Index).Cells(3).Text
                TxtModeloBA.Value = GvBusqueda.Rows(Index).Cells(2).Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBusqueda').modal('show'); }).modal('hide');", True)
        End If
        Limpiar_Popup()
    End Sub

    Private Sub btnCancela_Click(sender As Object, e As EventArgs) Handles btnCancela.Click
        If TituloPopup.Text = "Busca Modelos" Or TituloPopup.Text = "Busca Marcas" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBusqueda').modal('show'); }).modal('hide');", True)
        End If
        Limpiar_Popup()
    End Sub

    Private Sub GvBusArticulo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusArticulo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)


        If e.CommandName = "Aceptar" Then
            TxtCodArt.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            txtNomArt.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            Limpiar_Cajas_Buscar_Articulos()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
        End If

    End Sub

    'Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
    '    TituloPopupp.Text = "Busca Clasificaciones"
    '    Dim obj As New Cls_Clasificacion
    '    Dim dt As New DataTable
    '    dt = obj.PopularRootLevel(Session("Ruta_Emp"))
    '    obj.NodosPopulares(dt, trvClasificacion.Nodes)
    '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    'End Sub

    Private Sub BtnLimpiar_Click(sender As Object, e As EventArgs) Handles BtnLimpiar.Click
        lblCodClas.Text = ""
        TxtCodArt.Text = ""
        txtNomArt.Text = ""
        txtClasificacion.Text = ""
    End Sub

    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        Session("TipoModal") = "2"
        TituloPopupp.Text = "Busca Clasificaciones"
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)

    End Sub
    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click
        If Session("TipoModal") = "2" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalArticulo').modal('show'); }).modal('hide');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)
        End If
        trvClasificacion.Nodes.Clear()
    End Sub

    'Private Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
    '    If Session("TipoModal") = "1" Then

    '        trvClasificacion.SelectedNode.Selected = True
    '        TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
    '        Dim psNumero As Integer = 0
    '        lblCodClas.Text = trvClasificacion.SelectedValue
    '        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
    '        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalArticulo').modal('show'); }).modal('hide');", True)
    '        trvClasificacion.Nodes.Clear()
    '    Else
    '        trvClasificacion.SelectedNode.Selected = True
    '        TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
    '        Dim psNumero As Integer = 0
    '        lblCodClas.Text = trvClasificacion.SelectedValue
    '        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
    '        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
    '        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
    '        trvClasificacion.Nodes.Clear()

    '    End If

    'End Sub

    Protected Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        trvClasificacion.SelectedNode.Selected = True

        If Session("TipoModal") = "2" Then
            TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
            Dim psNumero As Integer = InStr(1, TxtClasificacionBA.Value, "-")
            LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalArticulo').modal('show'); }).modal('hide');", True)
        Else
            txtClasificacion.Text = trvClasificacion.SelectedNode.Text
            lblCodClas.Text = trvClasificacion.SelectedValue
            Dim psNumero As Integer = InStr(1, txtClasificacion.Text, "-")
            lblCodClas.Text = Left(txtClasificacion.Text, psNumero - 2)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)
        End If
        trvClasificacion.Nodes.Clear()

    End Sub

End Class
