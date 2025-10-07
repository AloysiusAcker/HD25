Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Partial Class Inventario_Inventario_BienesNuevos_NoConsiderar
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combos()
        End If
    End Sub

    Protected Sub Llenar_Combos()
        Dim objC As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Try
            dt = obj.Llenar_Combo_Inventario(Session("Ruta_Emp"))
            DdlInventario.DataSource = dt
            DdlInventario.DataValueField = "INVENT_CODIGO"
            DdlInventario.DataTextField = "INVENT_DESC"
            DdlInventario.DataBind()

            dt = objC.Lista_Tipo(Session("Ruta_Emp"))
            DdlTipoBA.DataSource = dt
            DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
            DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
            DdlTipoBA.DataBind()
            DdlTipoBA.Items.Add("< Seleccionar >")
            DdlTipoBA.SelectedValue = "< Seleccionar >"
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        ElseIf RBtodos.Checked Then

        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub
    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim objU As New Cls_Inventario_Ubicacion
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
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                inventario = DdlInventario.SelectedValue.ToString
            End If
            descripcion = BuscarDescripcion.Value.ToString
            If TituloPopup.Text = "Búsqueda Almacén" Then
                codigo = Nz(BuscarCodigo.Value.ToString)
                dt = obj.Listar_Almacenes_Inventario_Verificacion(Session("Ruta_Emp"), inventario, codigo, descripcion)
            ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
                dt = obj.Listar_CentroC_Inventario_Verificacion(Session("Ruta_Emp"), inventario, CodInterno, descripcion)
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
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        Limpiar_Cajas_Popup()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            txtOficina_CodInterno.Text = GvBusqueda.Rows(Index).Cells(1).Text
            txtOficina_Descripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            lblOficina_Codigo.Text = GvBusqueda.Rows(Index).Cells(3).Text
            lblCodInv_Ubica.Text = GvBusqueda.Rows(Index).Cells(4).Text
            Session("CodSeccion") = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()

    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click

        lblRegistro3.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        dt = Nothing

        Dim pdCodInvUbica As Double = 0
        Dim pdOficinaCodigo As Double = 0
        Dim psOficinatipo As String = ""


        pdOficinaCodigo = Nz(lblOficina_Codigo.Text.ToString)
        pdCodInvUbica = Nz(lblCodInv_Ubica.Text.ToString)

        Dim pdCodInventario As Double = 0
        If RBAlmacen.Checked Then
            psOficinatipo = "1"
        ElseIf RBCentroC.Checked Then
            psOficinatipo = "2"
        Else
            psOficinatipo = ""
        End If
        If psOficinatipo <> "" Then
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInventario = DdlInventario.SelectedValue
            End If
        End If
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        If Nz(TxtCodArticulo.Text) <> 0 Then
            pdCodArt = Nz(TxtCodArticulo.Text)
        End If
        Try
            'Inventario_BienesNoconsiderados
            dt = obj.Inventario_BienesNuevos(psconexion, pdCodInventario, pdCodInvUbica, psOficinatipo, pdOficinaCodigo, pdCodArt)
            GvListaVerificarInventarioNuevos.DataSource = dt
            GvListaVerificarInventarioNuevos.DataBind()

            If dt.Rows.Count > 1 Then
                lblRegistro3.Text = "Hay " & dt.Rows.Count & " registros nuevos."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro3.Text = "Hay 1 registro nuevo."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro3.Text = "Hay 0 registro nuevo."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try


        'Listar_Inventario_Verificacion()
    End Sub
    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        lblCodInv_Ubica.Text = ""
        lblOficina_Codigo.Text = ""
        txtOficina_Descripcion.Text = ""
        txtOficina_CodInterno.Text = ""
        lblRegistro3.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        lblCodInv_Ubica.Text = ""
        lblOficina_Codigo.Text = ""
        txtOficina_Descripcion.Text = ""
        txtOficina_CodInterno.Text = ""
        lblRegistro3.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
    End Sub

    Private Sub RBTodos_CheckedChanged(sender As Object, e As EventArgs) Handles RBTodos.CheckedChanged
        lblCodInv_Ubica.Text = ""
        lblOficina_Codigo.Text = ""
        txtOficina_Descripcion.Text = ""
        txtOficina_CodInterno.Text = ""
        lblRegistro3.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
    End Sub
    Protected Sub BtnNoconsiderar_Click(sender As Object, e As EventArgs) Handles BtnNoconsiderar.Click
        Dim Check As CheckBox
        Dim psPlacaNro As String = ""
        Dim i As Integer
        Dim dt As New Data.DataTable
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psSerieNumerar As String = ""
        Dim pdCodInvUbica As String = ""

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Try
            For i = 0 To GvListaVerificarInventarioNuevos.Rows.Count - 1
                psPlacaNro = GvListaVerificarInventarioNuevos.Rows(i).Cells(5).Text
                psSerieNumerar = GvListaVerificarInventarioNuevos.Rows(i).Cells(11).Text
                pdCodInvUbica = GvListaVerificarInventarioNuevos.Rows(i).Cells(12).Text
                Check = GvListaVerificarInventarioNuevos.Rows(i).Cells(0).FindControl("chk")
                If Check.Checked = True And Check.Enabled = True Then
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '10'  " _
                                          & " WHERE INVDET_SERIE_NUMERAR = " & psSerieNumerar & " AND INVDET_INVENTUBIC_CODIGO = " & pdCodInvUbica
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_ESTADO_INVENTARIO = '10' , VERIF_ESTADO = '10'  " _
                                          & " WHERE VERIF_SERIE_NUMERAR = " & psSerieNumerar & " AND INVENTUBIC_CODIGO = " & pdCodInvUbica
                    CmdGlobal.ExecuteNonQuery()
                End If
            Next
            BtnListar_Click(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicacion: " & ex.Message & "')", True)
        Finally
        End Try
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

    Private Sub BtnListarNo_Click(sender As Object, e As EventArgs) Handles BtnListarNo.Click

        lblRegistro3.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        dt = Nothing

        Dim pdCodInvUbica As Double = 0
        Dim pdOficinaCodigo As Double = 0
        Dim psOficinatipo As String = ""


        pdOficinaCodigo = Nz(lblOficina_Codigo.Text.ToString)
        pdCodInvUbica = Nz(lblCodInv_Ubica.Text.ToString)

        Dim pdCodInventario As Double = 0
        If RBAlmacen.Checked Then
            psOficinatipo = "1"
        ElseIf RBCentroC.Checked Then
            psOficinatipo = "2"
        Else
            psOficinatipo = ""
        End If
        If psOficinatipo <> "" Then
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInventario = DdlInventario.SelectedValue
            End If
        End If
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        If Nz(TxtCodArticulo.Text) <> 0 Then
            pdCodArt = Nz(TxtCodArticulo.Text)
        End If
        Try
            '
            dt = obj.Inventario_BienesNoconsiderados(psconexion, pdCodInventario, pdCodInvUbica, psOficinatipo, pdOficinaCodigo, pdCodArt)
            GvListaVerificarInventarioNuevos.DataSource = dt
            GvListaVerificarInventarioNuevos.DataBind()

            If dt.Rows.Count > 1 Then
                lblRegistro3.Text = "Hay " & dt.Rows.Count & " registros no considerados."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro3.Text = "Hay 1 registro no considerado."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro3.Text = "Hay 0 registro."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try

    End Sub

    Private Sub BtnBuscarArticulo_Click(sender As Object, e As EventArgs) Handles BtnBuscarArticulo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show'); ", True)
    End Sub
    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide'); ", True)
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        lblCantArtReg.Text = ""
        TxtClasificacionBA.Value = ""
        LblCodClasificacionBA.Text = ""
        TxtDescripcionBA.Value = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
        lblCodClas.Text = ""
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
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
        Dim clasificacion As String = LblCodClasificacionBA.Text.ToString
        Dim psDescripcion As String = TxtDescripcionBA.Value.ToString
        Dim tipo As String = DdlTipoBA.SelectedValue.ToString
        Dim numPart As String = TxtNumParteBA.Value.ToString
        Dim especifico As String = TxtCodEspecificoBA.Value.ToString
        Dim marca As Double = 0
        If LblCodMarcaBA.Text <> "" Then
            marca = Nz(LblCodMarcaBA.Text)
        End If
        Dim modelo As Double = 0
        If LblCodModeloBA.Text <> "" Then
            modelo = Nz(LblCodModeloBA.Text)
        End If

        If marca <> 0 Then psListaMarca = ""
        If modelo <> 0 Then psListaModelo = ""
        If pdCodArt <> 0 Then psListaArt = ""
        If tipo = "< Seleccionar >" Then tipo = ""
        Try
            dt = obj.Lista_ArticuloxBusqueda(psconexion, pdCodArt, clasificacion, psDescripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
            GvBuscarArticulos.DataSource = dt
            GvBuscarArticulos.DataBind()
            If dt.Rows.Count > 1 Then
                lblCantArtReg.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblCantArtReg.Text = "Hay 1 registros."
            ElseIf dt.Rows.Count = 0 Then
                lblCantArtReg.Text = "Hay 0 registros."
                lblCodClas.Text = ""
                TxtClasificacionBA.Value = ""
            End If

            Dim psCodInventario As Double = 0
            Dim psCodInvUbica As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                psCodInventario = Nz(DdlInventario.SelectedValue)
            End If
            dt = Nothing

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub GvBuscarArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodArticulo.Text = GvBuscarArticulos.Rows(Index).Cells(1).Text

            TxtDescArticulo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide'); ", True)

        End If

        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub BtnRegresar_Click(sender As Object, e As EventArgs) Handles BtnRegresar.Click
        Dim Check As CheckBox
        Dim psPlacaNro As String = ""
        Dim i As Integer
        Dim dt As New Data.DataTable
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psSerieNumerar As String = ""
        Dim pdCodInvUbica As String = ""

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Try
            For i = 0 To GvListaVerificarInventarioNuevos.Rows.Count - 1
                psPlacaNro = GvListaVerificarInventarioNuevos.Rows(i).Cells(5).Text
                psSerieNumerar = GvListaVerificarInventarioNuevos.Rows(i).Cells(11).Text
                pdCodInvUbica = GvListaVerificarInventarioNuevos.Rows(i).Cells(12).Text
                Check = GvListaVerificarInventarioNuevos.Rows(i).Cells(0).FindControl("chk")
                If Check.Checked = True And Check.Enabled = True Then
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '7'  " _
                                          & " WHERE INVDET_SERIE_NUMERAR = " & psSerieNumerar & " AND INVDET_INVENTUBIC_CODIGO = " & pdCodInvUbica
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_ESTADO_INVENTARIO = '7' , VERIF_ESTADO = '7'  " _
                                          & " WHERE VERIF_SERIE_NUMERAR = " & psSerieNumerar & " AND INVENTUBIC_CODIGO = " & pdCodInvUbica
                    CmdGlobal.ExecuteNonQuery()
                End If
            Next
            BtnListar_Click(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicacion: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
End Class