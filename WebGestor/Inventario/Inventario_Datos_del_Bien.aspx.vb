Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class Inventario_Datos_del_Bien
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combo_Ubicacion()
            Llenar_Combos()
            Session("CodEmpresa") = "0001"
        End If
    End Sub
    Private Sub BtnIngresarEquipo_Click(sender As Object, e As EventArgs) Handles BtnIngresarEquipo.Click
        Dim obj As New Cls_Datos_del_Bien
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim NroSerie As String = TxtSerieNroM.Value.ToString
        Dim NroPlaca As String = TxtPlacaNroM.Value.ToString
        Dim SerieResponsableObservacion As String = TxtObservacionM.Value.ToString
        Dim SerieEstado As String = DdlEstadoM.SelectedValue.ToString
        Dim SerieResponsable As String = DdlResponsableM.SelectedValue.ToString
        Dim SerieArea As String = TxtCodUbicacionM.Value.ToString
        Dim SerieNumerar As String = LblSerieNum.Text.ToString
        obj.Ingresar_Equipo(psconexion, NroSerie, NroPlaca, SerieResponsableObservacion, SerieEstado, SerieResponsable, SerieArea, SerieNumerar)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('hide');", True)
        Lista_Datos_del_Bien()
    End Sub
    Protected Sub Lista_Datos_del_Bien()
        Dim obj As New Cls_Datos_del_Bien
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim CodArticulo As String = txtCodArticulo.Text.ToString
        Dim DesArticulo As String = txtDescArticulo.Text.ToString
        Dim Descripcion As String = txtDescripcion.Text.ToString
        Dim NroSerie As String = txtNroSerie.Text.ToString
        Dim NroPlaca As String = "0"
        Dim CodArea As String = DdlUbicacion.SelectedValue.ToString
        Dim CodRelacionador As String = txtCodRelacionado.Text.ToString
        If CodArea = "< Seleccionar >" Then
            CodArea = "%"
        End If
        If txtNroPlaca.Text.ToString <> "" Then
            NroPlaca = txtNroPlaca.Text.ToString
        End If
        Try
            Convert.ToInt32(NroPlaca)
            dt = obj.Lista_Datos_del_Bien(psconexion, "0001", NroSerie, NroPlaca, CodArea, DesArticulo, Descripcion, CodRelacionador)
            GvListaDatosBien.DataSource = dt
            GvListaDatosBien.DataBind()
        Catch ex As FormatException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El número de placa deben ser números');", True)
        End Try
    End Sub
    Private Sub chckCodArticulo_CheckedChanged(sender As Object, e As EventArgs) Handles chckCodArticulo.CheckedChanged
        If chckCodArticulo.Checked = True Then
            BtnBuscaArticulo.Enabled = True
            txtDescripcion.Enabled = False
        Else
            BtnBuscaArticulo.Enabled = False
            txtCodArticulo.Text = ""
            txtDescArticulo.Text = ""
            txtDescripcion.Enabled = True
        End If
    End Sub

    Private Sub BtnBuscaArticulo_Click(sender As Object, e As EventArgs) Handles BtnBuscaArticulo.Click
        TituloBuscarArticulos.Text = "Búsqueda de Artículos"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
    End Sub
    Protected Sub Limpiar_Cajas_Conciliados_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        LblCodClasificacionBA.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('hide');", True)
    End Sub
    Protected Sub Llenar_Combo_Ubicacion()
        Dim obj As New Cls_Datos_del_Bien
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Ubicacion(psconexion)
        DdlUbicacion.DataSource = dt
        DdlUbicacion.DataValueField = "UBICACION_CODIGO"
        DdlUbicacion.DataTextField = "UBIC_DESC"
        DdlUbicacion.DataBind()
        DdlUbicacion.Items.Add("< Seleccionar >")
        DdlUbicacion.SelectedValue = "< Seleccionar >"
    End Sub
    Private Sub chckUbicacion_CheckedChanged(sender As Object, e As EventArgs) Handles chckUbicacion.CheckedChanged
        If chckUbicacion.Checked = True Then
            DdlUbicacion.Enabled = True
        Else
            DdlUbicacion.Enabled = False
        End If
    End Sub
    Private Sub GvBuscarArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            If TituloBuscarArticulos.Text = "Búsqueda de Artículos" Then
                txtCodArticulo.Text = GvBuscarArticulos.Rows(Index).Cells(1).Text.ToString
                txtDescArticulo.Text = GvBuscarArticulos.Rows(Index).Cells(3).Text.ToString
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
            ElseIf TituloBuscarArticulos.Text = "Búsqueda de Artículo" Then
                TxtCodArticuloM.Value = GvBuscarArticulos.Rows(Index).Cells(1).Text.ToString
                TxtDescArticuloM.Value = GvBuscarArticulos.Rows(Index).Cells(3).Text.ToString
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
            End If
        End If
        Call Limpiar_Cajas_Conciliados_Articulos()
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Lista_Datos_del_Bien()
    End Sub

    Private Sub GvListaDatosBien_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaDatosBien.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim obj As New Cls_Datos_del_Bien
        Dim objCn As New Cls_Conexion
        Dim NroPlaca As String = GvListaDatosBien.Rows(Index).Cells(6).Text
        Dim NroSerie As String = GvListaDatosBien.Rows(Index).Cells(5).Text
        Dim psconexion As String = Session("Ruta_Emp")
        Dim dt As DataTable = obj.Buscar_Serie_Numerar(psconexion, NroPlaca, NroSerie)
        Dim DvRow As DataRow = dt.Rows(0)
        LblSerieNum.Text = DvRow(0)
        If e.CommandName = "Modificar" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('show');", True)
            Cargar_Datos_Articulos(GvListaDatosBien.Rows(Index).Cells(5).Text.ToString, GvListaDatosBien.Rows(Index).Cells(6).Text.ToString)
            If dt.Rows.Count = 1 Then
                For Each drow As DataRow In dt.Rows
                    LblSerieNum.Text = (drow("SERIE_NUMERAR"))
                Next
            End If
        End If
    End Sub
    Protected Sub Limpiar_Cajas_Articulos()
        txtNroPlaca.Text = ""
        txtNroSerie.Text = ""
        TxtPlacaNroM.Value = ""
        TxtSerieNroM.Value = ""
        DdlEstadoM.SelectedValue = "< Seleccionar >"
        DdlResponsableM.SelectedValue = "< Seleccionar >"
        TxtCodArticuloM.Value = ""
        TxtDescArticuloM.Value = ""
        TxtCodAreaM.Value = ""
        TxtDescAreaM.Value = ""
        TxtCodUbicacionM.Value = ""
        TxtDescUbicacionM.Value = ""
        TxtObservacionM.Value = ""
        LblCodAreaM.Text = ""
        LblCodUbicacionM.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('hide');", True)
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
    Protected Sub Llenar_Combos()
        Dim obj As New Cls_Datos_del_Bien
        Dim objC As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Llenar_Combo_Estado(psconexion)
        DdlEstadoM.DataSource = dt
        DdlEstadoM.DataValueField = "ELEMEN_CODIGO"
        DdlEstadoM.DataTextField = "ELEMEN_VALOR"
        DdlEstadoM.DataBind()
        DdlEstadoM.Items.Add("< Seleccionar >")
        DdlEstadoM.SelectedValue = "< Seleccionar >"

        dt = obj.Llenar_Combo_Personal(psconexion)
        DdlResponsableM.DataSource = dt
        DdlResponsableM.DataValueField = "PERSON_CODIGO"
        DdlResponsableM.DataTextField = "PERSON_NOMBRE"
        DdlResponsableM.DataBind()
        DdlResponsableM.Items.Add("< Seleccionar >")
        DdlResponsableM.SelectedValue = "< Seleccionar >"

        dt = objC.Lista_Tipo(psconexion)
        DdlTipoBA.DataSource = dt
        DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
        DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
        DdlTipoBA.DataBind()
        DdlTipoBA.Items.Add("< SELECCIONAR >")
        DdlTipoBA.SelectedValue = "< SELECCIONAR >"
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New Cls_Datos_del_Bien
        Dim objU As New Cls_Inventario_Ubicacion
        Dim objMo As New Cls_Modelo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim dtU As New DataTable
        Dim dtM As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim codigo As String = BuscarCodigo.Value.ToString
        Dim descripcion As String = BuscarDescripcion.Value.ToString
        Dim codMarca As String = LblCodMarcaBA.Text.ToString

        If TituloPopup.Text = "Busca Almacén" Then
            dtU = objU.Lista_Almacenes_Inventario(psconexion, codigo, descripcion)
        ElseIf TituloPopup.Text = "Busca Sección de Centro de Costo" Then
            dtU = objU.Lista_CentroC_Inventario(psconexion, codigo, descripcion)
        ElseIf TituloPopup.Text = "Busca Ubicaciones" Then
            dtU = objU.Lista_Ubicaciones_Inventario(psconexion, codigo, descripcion)
        ElseIf TituloPopup.Text = "Busca Marca" Then
            dtM = obj.Buscar_Marca(psconexion, codigo, descripcion)
        ElseIf TituloPopup.Text = "Busca Modelo" Then
            dtM = obj.Buscar_Modelo(psconexion, codigo, descripcion, codMarca)
        End If

        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()

        GvBusquedaU.DataSource = dtU
        GvBusquedaU.DataBind()

        GvBusquedaM.DataSource = dtM
        GvBusquedaM.DataBind()
    End Sub
    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        GvBusquedaU.DataSource = Nothing
        GvBusquedaU.DataBind()
        GvBusquedaM.DataSource = Nothing
        GvBusquedaM.DataBind()
    End Sub
    Private Sub BtnCerrarArticulo_Click(sender As Object, e As EventArgs) Handles BtnCerrarArticulo.Click
        Limpiar_Cajas_Articulos()
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
    Private Sub BtnBuscaUbicacionM_Click(sender As Object, e As EventArgs) Handles BtnBuscaUbicacionM.Click
        TituloPopup.Text = "Busca Ubicaciones"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaArticuloM_Click(sender As Object, e As EventArgs) Handles BtnBuscaArticuloM.Click
        TituloBuscarArticulos.Text = "Búsqueda de Artículo"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaAreaM_Click(sender As Object, e As EventArgs) Handles BtnBuscaAreaM.Click
        If RBAlmacenArea.Checked Then
            TituloPopup.Text = "Busca Almacén"
        ElseIf RBCentroCArea.Checked Then
            TituloPopup.Text = "Busca Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click

        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
        If TituloBuscarArticulos.Text = "Búsqueda de Artículo" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
        End If
        Limpiar_Cajas_Buscar_Articulos()
    End Sub
    Protected Sub Cargar_Datos_Articulos(ByVal NroSerie As String, ByVal NroPlaca As String)
        Dim obj As New Cls_Datos_del_Bien
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim dbRow As DataRow
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Buscar_Serie_Numerar(psconexion, NroPlaca, NroSerie)
        dbRow = dt.Rows(0)
        Dim numerar As String = dbRow(0).ToString

        dt1 = obj.Cargar_Articulos1(psconexion, numerar)
        GvArticulo1.DataSource = dt1
        GvArticulo1.DataBind()

        dt = obj.Cargar_Articulos(psconexion, numerar)
        GvArticulo2.DataSource = dt
        GvArticulo2.DataBind()

        dbRow = dt.Rows(0)
        TxtSerieNroM.Value = dbRow(3).ToString
        TxtPlacaNroM.Value = dbRow(13).ToString
        If dbRow(26).ToString.Equals("") Then
            DdlEstadoM.SelectedValue = "< Seleccionar >"
        Else
            DdlEstadoM.SelectedValue = dbRow(26).ToString
        End If
        TxtCodRelacionadoM.Value = dbRow(55).ToString
        If dbRow(40).ToString.Equals("") Then
            DdlResponsableM.SelectedValue = "< Seleccionar >"
        Else
            DdlResponsableM.SelectedValue = dbRow(40).ToString
        End If
        TxtCodArticuloM.Value = dbRow(0).ToString
        TxtDescArticuloM.Value = dbRow(1).ToString
        LblArticuloM.Text = dbRow(6).ToString
        If dbRow(5).ToString.Equals("1") Then
            RBCentroCArea.Checked = False
            RBAlmacenArea.Checked = True
        Else
            RBAlmacenArea.Checked = False
            RBCentroCArea.Checked = True
        End If
        LblCodAreaM.Text = dbRow(45).ToString
        TxtCodAreaM.Value = dbRow(46).ToString
        TxtDescAreaM.Value = dbRow(47).ToString
        TxtCodUbicacionM.Value = dbRow(57).ToString
        LblCodUbicacionM.Text = dbRow(58).ToString
        TxtDescUbicacionM.Value = dbRow(59).ToString
        TxtObservacionM.Value = dbRow(27).ToString
    End Sub
    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        'Dim obj As New Cls_Datos_del_Bien
        'Dim objCn As New Cls_Conexion
        'Dim dt As New DataTable
        'Dim psListaArt As String = "1"
        'Dim psListaMarca As String = "1"
        'Dim psListaModelo As String = "1"
        'Dim psconexion As String = Session("Ruta_Emp")
        'Dim codigo As String = TxtCodArticuloBA.Value.ToString
        'Dim clasificacion As String = LblCodClasificacionBA.Text.ToString
        'Dim descripcion As String = TxtDescripcionBA.Value.ToString
        'Dim tipo As String = DdlTipoBA.SelectedValue.ToString
        'Dim numPart As String = TxtNumParteBA.Value.ToString
        'Dim especifico As String = TxtCodEspecificoBA.Value.ToString
        'Dim marca As String = LblCodMarcaBA.Text.ToString
        'Dim modelo As String = LblCodModeloBA.Text.ToString
        'If marca <> "" Then psListaMarca = ""
        'If modelo <> "" Then psListaModelo = ""
        'If codigo <> "" Then psListaArt = ""
        'If tipo = "< SELECCIONAR >" Then tipo = ""

        'dt = obj.Bus_Articulo(psconexion, codigo, clasificacion, descripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
        'GvBuscarArticulos.DataSource = dt
        'GvBuscarArticulos.DataBind()

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


            dtColum.Columns.Add("ART_CODIGO")
            dtColum.Columns.Add("ART_CODEQUIVA")
            dtColum.Columns.Add("ART_DESCRIPCION")
            dtColum.Columns.Add("TIPO_ART")
            dtColum.Columns.Add("ART_TIPO")
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

            dt = obj.Lista_ArticuloxBusqueda(psconexion, pdCodArt, clasificacion, psDescripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
            If dt.Rows.Count > 0 Then
                For Each drDato As DataRow In dt.Rows
                    drT = dtColum.NewRow()
                    drT("ART_CODIGO") = Nu(drDato("ART_CODIGO"))
                    drT("ART_CODEQUIVA") = Nu(drDato("ART_CODEQUIVA"))
                    drT("ART_DESCRIPCION") = Nu(drDato("ART_DESCRIPCION"))
                    drT("TIPO_ART") = Nu(drDato("TIPO_ART"))
                    drT("ART_TIPO") = Nu(drDato("ART_TIPO"))
                    drT("ART_SKU") = Nu(drDato("ART_SKU"))
                    dtColum.Rows.Add(drT)
                Next
            End If

            GvBuscarArticulos.DataSource = dtColum
            GvBuscarArticulos.DataBind()
            If dtColum.Rows.Count > 1 Then
                LblCantArtReg.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dtColum.Rows.Count = 1 Then
                LblCantArtReg.Text = "Hay 1 registro."
            ElseIf dtColum.Rows.Count = 0 Then
                LblCantArtReg.Text = "No hay registro."
            End If

        Catch ex As SqlException
            lblError.Text = ex.Message
        Catch ex As Exception
            lblError.Text = ex.Message
        Finally
        End Try


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
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaMarcaBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaMarcaBA.Click
        TituloPopup.Text = "Busca Marca"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaModeloBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaModeloBA.Click
        TituloPopup.Text = "Busca Modelo"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub GvBusquedaU_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusquedaU.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And (TituloPopup.Text = "Busca Almacén" Or TituloPopup.Text = "Busca Sección de Centro de Costo") Then
            TxtCodAreaM.Value = GvBusquedaU.Rows(Index).Cells(1).Text
            TxtDescAreaM.Value = GvBusquedaU.Rows(Index).Cells(2).Text
            LblCodAreaM.Text = GvBusquedaU.Rows(Index).Cells(3).Text
        ElseIf e.CommandName = "Aceptar" And TituloPopup.Text = "Busca Ubicaciones" Then
            TxtCodUbicacionM.Value = GvBusquedaU.Rows(Index).Cells(1).Text
            TxtDescUbicacionM.Value = GvBusquedaU.Rows(Index).Cells(2).Text
            LblCodUbicacionM.Text = GvBusquedaU.Rows(Index).Cells(3).Text
        End If

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
        Limpiar_Cajas_Popup()
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
        LblCodClasificacionBA.Text = TrvClasificacion.SelectedValue
        Dim psPosicion As Long = 0
        psPosicion = InStr(TxtClasificacionBA.Value, "-")
        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psPosicion - 2)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click
        TrvClasificacion.Nodes.Clear()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
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
            'If lblCodClas.Text <> "" Then psCodClasif = lblCodClas.Text
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
End Class