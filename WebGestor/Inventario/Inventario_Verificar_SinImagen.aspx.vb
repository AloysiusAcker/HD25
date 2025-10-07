Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports WebGestor
Partial Class Inventario_Inventario_Verificar_SinImagen
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnOpen.Attributes.Add("OnClick", "window.open('Inventario_PopPud_DatosOficina.aspx',null,'height=600,width=500');")
        If Not Page.IsPostBack Then
            Llenar_Combos()
            TxtNroPlaca.Focus()
            Call Llena_Ubicacion(ddlUbicacion)
            ddlUbicacion.SelectedValue = "< Seleccionar >"
        End If
    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim searchTerm As String = txtSearch.Text
        Dim pageContent As String = Me.Page.ToString()

        If pageContent.Contains(searchTerm) Then
            Dim highlightedContent As String = pageContent.Replace(searchTerm, "<span style='background-color: yellow;'>" & searchTerm & "</span>")
            lblResults.Text = highlightedContent
        Else
            lblResults.Text = "La palabra no se encontró en la página."
        End If
    End Sub
    Private Sub Llena_Ubicacion(ByVal combo As DropDownList)
        'Lista_Ubicaciones
        Dim obj As New clsInv_Listados
        combo.Items.Clear() 'Listar_Usuarios
        combo.DataSource = obj.Lista_Ubicaciones(Session("Ruta_Emp"), Session("CodEmpresa"))
        combo.DataTextField = "Ubicacion"
        combo.DataValueField = "UBICACION_CODIGO"
        combo.DataBind()
        combo.Items.Add("< Seleccionar >")
        combo.SelectedValue = "< Seleccionar >"
    End Sub
    Protected Sub Ocultar_Mostrar_Cajas(ByVal vf As Boolean)
        LblNroSerie.Visible = vf
        TxtNroSerie.Visible = vf
        LblNroPlaca.Visible = vf
        TxtNroPlaca.Visible = vf
        lblIngObs.Visible = vf
        BtnIngObs.Visible = vf
        Label1.Visible = vf
        TxtNroAtm.Visible = vf
        lblCancelar.Visible = vf
        lblEtiquetaUbi.Visible = vf
        ddlUbicacion.Visible = vf
        BtnCancelar.Visible = vf
        BtnCargaArchivo.Visible = vf
        txtBusArticulo.Visible = vf
        lblBusArticulo.Visible = vf
    End Sub
    Protected Sub Habilitar_Desabilitar(ByVal vf As Boolean)
        DdlInventario.Enabled = vf
        RBAlmacen.Enabled = vf
        RBCentroC.Enabled = vf
        RBUbicaciones.Enabled = vf
        BtnBusca.Enabled = vf
        BtnListar.Enabled = vf
    End Sub
    Protected Sub Listar_Inventario_Verificacion()
        LblContador.Text = ""
        lblRegistro3.Text = ""
        lblRegistro2.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        dt = Nothing

        Dim pdCodInvUbica As Double = 0
        Dim pdUbicaCodigo As Double = 0
        pdCodInvUbica = Nz(TxtCodigoAyuda.Text.ToString)
        Dim dtO As New DataTable
        dtO = Nothing
        Dim codigo As String = TxtCodigoAyuda.Text.ToString

        Dim tipo As String = ""
        Dim ubicacion As String = TxtCodigoAyudaUbicacion.Text.ToString
        pdUbicaCodigo = Nz(TxtCodigoAyudaUbicacion.Text.ToString)
        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBCentroC.Checked Then
            tipo = "2"
        ElseIf RBUbicaciones.Checked Then
            tipo = "9"
        End If
        GvListaVerificarInventarioOtros.DataSource = dtO
        GvListaVerificarInventarioOtros.DataBind()
        GvListaVerificarInventarioNuevos.DataSource = dtO
        GvListaVerificarInventarioNuevos.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Try

            If lblCodArticuloBus.Text <> "" Then
                pdCodArt = Nz(lblCodArticuloBus.Text)
                If DdlInventario.SelectedValue <> "< Seleccionar >" Then pdCodInv = Nz(DdlInventario.SelectedValue)
                pdCodUbicInv = Nz(TxtCodigoAyuda.Text)

                dt = obj.Inventario_Verificacion_ListaxArticulo(Session("Ruta_Emp"), pdCodInv, pdCodUbicInv, pdCodArt)
                GvListaVerificarInventario.DataSource = dt
                GvListaVerificarInventario.DataBind()

                If dt.Rows.Count > 1 Then
                    LblContador.Text = "Hay " & dt.Rows.Count & " registros."
                ElseIf dt.Rows.Count = 1 Then
                    LblContador.Text = "Hay 1 registro."
                ElseIf dt.Rows.Count = 0 Then
                    LblContador.Text = "Hay 0 registro."
                End If

            Else

                dt = obj.ListaTop5_Inventario_Verificacion(psconexion, codigo, tipo, ubicacion, Session("User"))
                gvListaTop5.DataSource = dt
                gvListaTop5.DataBind()
                If dt.Rows.Count > 0 Then
                    LblContador.Text = "Los ultimos 5 inventariados."
                End If


                dt = obj.Lista_Inventario_Verificacion(psconexion, pdCodInvUbica, tipo, pdUbicaCodigo)
                GvListaVerificarInventario.DataSource = dt
                GvListaVerificarInventario.DataBind()

                If dt.Rows.Count > 1 Then
                    lblRegistraTodo.Text = "Hay " & dt.Rows.Count & " registros."
                ElseIf dt.Rows.Count = 1 Then
                    lblRegistraTodo.Text = "Hay 1 registro."
                ElseIf dt.Rows.Count = 0 Then
                    lblRegistraTodo.Text = "Hay 0 registro."
                End If

                dt = obj.Lista_NoInventario_Verificacion(psconexion, codigo, tipo, ubicacion)
                gvListaNoInventariado.DataSource = dt
                gvListaNoInventariado.DataBind()
                If dt.Rows.Count > 1 Then
                    lblContador2.Text = "Hay " & dt.Rows.Count & " registros."
                ElseIf dt.Rows.Count = 1 Then
                    lblContador2.Text = "Hay 1 registro."
                ElseIf dt.Rows.Count = 0 Then
                    lblContador2.Text = "Hay 0 registro."
                End If

            End If

            dtO = obj.Lista_Inventario_Verificacion_Otros(psconexion, codigo, tipo, ubicacion)
            GvListaVerificarInventarioOtros.DataSource = dtO
            GvListaVerificarInventarioOtros.DataBind()

            If dt.Rows.Count > 1 Then
                lblRegistro2.Text = "Hay " & dtO.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro2.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro2.Text = "Hay 0 registro."
            End If


            dtO = obj.Lista_Inventario_Verificacion_Nuevos(psconexion, codigo, tipo, ubicacion)
            GvListaVerificarInventarioNuevos.DataSource = dtO
            GvListaVerificarInventarioNuevos.DataBind()

            If dt.Rows.Count > 1 Then
                lblRegistro3.Text = "Hay " & dtO.Rows.Count & " registros nuevos."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro3.Text = "Hay 1 registro nuevo."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro3.Text = "Hay 0 registro nuevo."
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub Cargar_Datos_Articulos(ByVal psEstado As String)
        Dim obj As New Cls_Inventario_Verificacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim placa As Double = 0
        Dim serie As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader

        Try

            Cn.Open()
            CmdGlobal.Connection = Cn
            placa = Nz(TxtNroPlaca.Text.ToString)
            serie = TxtNroSerie.Text.ToString
            dt = obj.Buscar_Serie_Numerar(Session("Ruta_Emp"), placa, serie)
            Dim numerar As Double = 0
            If dt.Rows.Count > 0 Then
                For Each drow As DataRow In dt.Rows
                    numerar = Nz(drow("SERIE_NUMERAR"))
                Next
            End If
            dt = Nothing

            dt = obj.Cargar_Datos_Bien(Session("Ruta_Emp"), Session("CodEmpresa"), numerar, "", "")
            If dt.Rows.Count > 0 Then
                For Each drow As DataRow In dt.Rows
                    lblSerieNumerar.Text = Nz(drow("SERIE_NUMERAR").ToString)
                    TxtSerieNroM.Text = Nu(drow("SERIE_NRO").ToString)
                    TxtPlacaNroM.Value = Nz(drow("PLACA_NRO").ToString)
                    lblSerieReal.Text = Nu(drow("SERIE_NRO").ToString)
                    lblPlacaReal.Text = Nz(drow("PLACA_NRO").ToString)
                    lblArtCodReal.Text = Nz(drow("COD_ARTICULO").ToString)
                    txtNroAtmM.Text = Nz(drow("SERIE_ATM_NROTERMINAL").ToString)

                    If Nu(drow("SERIE_ESTADO_EQUIPO").ToString) <> "" Then
                        DdlEstadoM.SelectedValue = Nu(drow("SERIE_ESTADO_EQUIPO").ToString)
                    Else
                        DdlEstadoM.SelectedValue = "1"
                    End If
                    TxtCodRelacionadoM.Value = Nu(drow("ART_CODEQUIVA").ToString)

                    TxtCodArticuloM.Value = Nz(drow("COD_ARTICULO").ToString)
                    TxtDescArticuloM.Value = Nu(drow("ART_DESCRIPCION").ToString)
                    LblArticuloM.Text = Nu(drow("art_tipo").ToString)
                    If Nu(drow("UBICACT_TIPO").ToString) = "1" Then
                        RBCentroCArea.Checked = False
                        RBAlmacenArea.Checked = True
                    Else
                        RBAlmacenArea.Checked = False
                        RBCentroCArea.Checked = True
                    End If

                    LblCodAreaM.Text = Nu(drow("UBICACT_CODIGO").ToString) ' Nu(drow("SERIE_CUSTODIA_CCOSTO").ToString)
                    TxtCodAreaM.Value = Nu(drow("COD_ALMACEN").ToString)
                    TxtDescAreaM.Value = (drow("ALMACEN_NOMBRE").ToString)
                    TxtCodUbicacionM.Value = Nu(drow("AREA_CODINTERNO").ToString)
                    LblCodUbicacionM.Text = Nu(drow("SERIE_AREA").ToString)
                    TxtDescUbicacionM.Value = Nu(drow("AREA_NOMBRE").ToString)
                    TxtObservacionM.Value = Nu(drow("SERIE_RESPONSABLE_OBSERVACION").ToString)

                    CmdGlobal.CommandText = " SELECT INVDET_RESPONSABLE_OBSERVACION,INVDET_SERIE_AREA,INVDET_RESPONSABLE_NOMBRE,  INVDET_ESTADO_INVENTARIO, " _
                                          & " (SELECT UBICACION_CODINTERNO FROM TBAREA_UBICACION WHERE UBICACION_SYS_EST = '0' And UBICACION_CODIGO = INVDET_SERIE_AREA) AS AREA_CODINTERNO, " _
                                          & " (SELECT UBICACION_DESCRIPCION FROM TBAREA_UBICACION  WHERE UBICACION_SYS_EST = '0' AND UBICACION_CODIGO = INVDET_SERIE_AREA) AS AREA_NOMBRE " _
                                          & " FROM TBINVENTARIO_DETALLE WHERE INVDET_INVENTUBIC_CODIGO = " & TxtCodigoAyuda.Text & " and INVDET_SERIE_NUMERAR = " & Nz(drow("SERIE_NUMERAR").ToString)
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            TxtObservacionM.Value = Nu(Rs("INVDET_RESPONSABLE_OBSERVACION").ToString)
                            TxtCodUbicacionM.Value = Nu(Rs("AREA_CODINTERNO").ToString)
                            LblCodUbicacionM.Text = Nu(Rs("INVDET_SERIE_AREA").ToString)
                            TxtDescUbicacionM.Value = Nu(Rs("AREA_NOMBRE").ToString)
                            TxtResponsable.Value = Nu(Rs("INVDET_RESPONSABLE_NOMBRE").ToString)
                            lblEstadoInventario.Text = Nu(Rs("INVDET_ESTADO_INVENTARIO").ToString)
                        End While
                    End If
                    Rs.Close()

                    If TxtCodUbicacionM.Value = "" Then
                        CmdGlobal.CommandText = " SELECT  INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR, VERIF_PLACA_NRO, VERIF_SERIE_NRO, VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO, VERIF_RESPONSABLE, VERIF_ESTADO_BIEN,  " _
                                          & " VERIF_AREA_UBICACION, VERIF_ESTADO, VERIF_ART_CODIGO, VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_SYS_MOD, VERIF_FECHA, VERIF_HORA, VERIF_SERIE_NRO_REAL, VERIF_PLACA_NRO_REAL, " _
                                          & " VERIF_REGULARIZAR, VERIF_SALIDA_CODIGO, VERIF_ESTADO_INVENTARIO, VERIF_ESTADO_CONCILIADO, VERIF_SERIE_NUMERAR_CONCILIADO, VERIF_CORRELATIVO, VERIF_OBSERVACION ,  " _
                                          & " (SELECT UBICACION_CODINTERNO FROM TBAREA_UBICACION WHERE UBICACION_SYS_EST = '0' And UBICACION_CODIGO = VERIF_AREA_UBICACION) AS AREA_CODINTERNO, " _
                                          & " (SELECT UBICACION_DESCRIPCION FROM TBAREA_UBICACION  WHERE UBICACION_SYS_EST = '0' AND UBICACION_CODIGO = VERIF_AREA_UBICACION) AS AREA_NOMBRE " _
                                          & " FROM TBINVENTARIO_VERIFICACION WHERE iNVENTUBIC_CODIGO = " & TxtCodigoAyuda.Text & " and VERIF_SERIE_NUMERAR  = " & Nz(drow("SERIE_NUMERAR").ToString)
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                TxtObservacionM.Value = Nu(Rs("VERIF_OBSERVACION").ToString)
                                TxtCodUbicacionM.Value = Nu(Rs("AREA_CODINTERNO").ToString)
                                LblCodUbicacionM.Text = Nu(Rs("VERIF_AREA_UBICACION").ToString)
                                TxtDescUbicacionM.Value = Nu(Rs("AREA_NOMBRE").ToString)
                                TxtResponsable.Value = Nu(Rs("VERIF_RESPONSABLE").ToString)
                                lblEstadoInventario.Text = Nu(Rs("VERIF_ESTADO_INVENTARIO").ToString)
                            End While
                        End If
                        Rs.Close()
                    End If
                Next
            End If
            dt = Nothing

            If LblCodUbicacionM.Text = "" And ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
                LblCodUbicacionM.Text = ddlUbicacion.SelectedValue
                TxtCodUbicacionM.Value = Left(ddlUbicacion.SelectedItem.Text, 4)
                TxtDescUbicacionM.Value = Mid(ddlUbicacion.SelectedItem.Text, 8)
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
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
    Protected Sub Limpiar_Cajas_Articulos()
        TxtNroPlaca.Text = ""
        TxtNroSerie.Text = ""
        TxtNroAtm.Text = ""
        txtNroAtmM.Text = ""
        TxtPlacaNroM.Value = ""
        TxtSerieNroM.Text = ""
        TxtCodRelacionadoM.Value = ""
        txtCantEq.Text = "1"
        DdlEstadoM.SelectedValue = "< Seleccionar >"
        TxtResponsable.Value = ""
        TxtCodArticuloM.Value = ""
        TxtDescArticuloM.Value = ""
        TxtCodAreaM.Value = ""
        TxtDescAreaM.Value = ""
        TxtCodUbicacionM.Value = ""
        TxtDescUbicacionM.Value = ""
        TxtObservacionM.Value = ""
        LblCodAreaM.Text = ""
        LblCodUbicacionM.Text = ""
        lblArtCodReal.Text = ""
        lblSerieReal.Text = ""
        lblPlacaReal.Text = ""
        chkPlaca.Checked = False
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('hide');", True)
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
        lblNroEqNoInv.Text = ""
        gvEqNoInventariado.DataSource = Nothing
        gvEqNoInventariado.DataBind()
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

            dt = obj.Llenar_Combo_Estado(Session("Ruta_Emp"))
            DdlEstadoM.DataSource = dt
            DdlEstadoM.DataValueField = "ELEMEN_CODIGO"
            DdlEstadoM.DataTextField = "ELEMEN_VALOR"
            DdlEstadoM.DataBind()
            DdlEstadoM.Items.Add("< Seleccionar >")
            DdlEstadoM.SelectedValue = "< Seleccionar >"


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
        ElseIf RBUbicaciones.Checked Then
            TituloPopup.Text = "Búsqueda Ubicaciones"
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
            codMarca = LblCodMarcaBA.Text.ToString
            descripcion = BuscarDescripcion.Value.ToString
            If TituloPopup.Text = "Búsqueda Almacén" Then
                codigo = Nz(BuscarCodigo.Value.ToString)
                dt = obj.Listar_Almacenes_Inventario_Verificacion(Session("Ruta_Emp"), inventario, codigo, descripcion)
            ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
                dt = obj.Listar_CentroC_Inventario_Verificacion(Session("Ruta_Emp"), inventario, CodInterno, descripcion)
            ElseIf TituloPopup.Text = "Búsqueda Ubicaciones" Then
                dt = obj.Listar_Ubicaciones_Inventario_Verificacion(Session("Ruta_Emp"), inventario, CodInterno, descripcion)
            ElseIf TituloPopup.Text = "Busca Almacén" Then
                codigo = Nz(BuscarCodigo.Value.ToString)
                dtU = objU.Lista_Almacenes_Inventario(Session("Ruta_Emp"), codigo, descripcion)
            ElseIf TituloPopup.Text = "Busca Sección de Centro de Costo" Then
                dtU = objU.Lista_CentroC_Inventario(Session("Ruta_Emp"), CodInterno, descripcion)
            ElseIf TituloPopup.Text = "Busca Ubicaciones" Then
                dtU = objU.Lista_Ubicaciones_Inventario(Session("Ruta_Emp"), CodInterno, descripcion)
            ElseIf TituloPopup.Text = "Busca Marca" Then
                dtM = objMa.Buscar_Marca(Session("Ruta_Emp"), CodInterno, descripcion)
            ElseIf TituloPopup.Text = "Busca Modelo" Then
                dtM = objMo.Buscar_Modelo(Session("Ruta_Emp"), CodInterno, descripcion, codMarca)
            End If

            GvBusqueda.DataSource = dt
            GvBusqueda.DataBind()

            GvBusquedaU.DataSource = dtU
            GvBusquedaU.DataBind()

            GvBusquedaM.DataSource = dtM
            GvBusquedaM.DataBind()

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try

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
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            TxtCodigoAyudaUbicacion.Text = GvBusqueda.Rows(Index).Cells(3).Text
            TxtCodigoAyuda.Text = GvBusqueda.Rows(Index).Cells(4).Text
            Session("CodSeccion") = GvBusqueda.Rows(Index).Cells(3).Text
            Llenar_DatosOficina()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()

    End Sub
    Protected Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dt As New DataTable
        lblCodArticuloBus.Text = ""
        LblContador.Text = ""
        dt = Nothing
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Try

            Listar_Inventario_Verificacion()
            accordion.Visible = True
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try


        'Listar_Inventario_Verificacion()
    End Sub

    Protected Sub BtnIniciarVerificacion_Click(sender As Object, e As EventArgs) Handles BtnIniciarVerificacion.Click
        Try
            If DdlInventario.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe de seleccionar el inventario.');", True)
            ElseIf TxtCodigoAyudaUbicacion.Text = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Debe de seleccionar la ubicación.');", True)
            Else
                Dim obj As New Cls_Inventario_Verificacion
                Dim dt As New DataTable
                Dim pdCodInv As Double = 0
                Dim psTipoUbic As String = ""
                If RBAlmacen.Checked = True Then psTipoUbic = "1"
                If RBCentroC.Checked = True Then psTipoUbic = "2"
                If RBUbicaciones.Checked = True Then psTipoUbic = "9"
                Dim pdCodUbica As Double = 0
                pdCodUbica = Nz(TxtCodigoAyudaUbicacion.Text)
                pdCodInv = Nz(DdlInventario.SelectedValue)
                Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Cn.Open() : CmdGlobal.Connection = Cn
                '
                dt = obj.ListaUbicacionInventario_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodInv, psTipoUbic, pdCodUbica)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        CmdGlobal.CommandText = " UPDATE TBINVENTARIO_UBICACIONES SET INVENTUBIC_ESTADO='2' WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' " _
                                               & " AND INVENTUBIC_CODIGO=" & Nz(dr("INVENTUBIC_CODIGO")) & " AND INVENTUBIC_ESTADO='1' AND INVENTUBIC_SYS_EST='0' "
                        CmdGlobal.ExecuteNonQuery()
                    Next
                End If
                Ocultar_Mostrar_Cajas(True)
                Habilitar_Desabilitar(False)
                TxtNroSerie.Text = ""
                TxtNroPlaca.Text = ""
                txtBusArticulo.Text = ""
                BtnNuevoEquipo.Visible = True
                ddlUbicacion.SelectedValue = "< Seleccionar >"
                TxtNroPlaca.Focus()
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Ocultar_Mostrar_Cajas(False)
        Habilitar_Desabilitar(True)
        TxtMensajeVerificar.Visible = False
        BtnNuevoEquipo.Visible = False
        LblContador.Text = ""
        lblRegistro2.Text = ""
        lblRegistro3.Text = ""
        lblRegistroCant.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaCantxArt.DataSource = dt
        GvListaCantxArt.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
        accordion.Visible = False
        LbltotalEquipos.Text = ""
        LbltotalEquipos.Visible = False
    End Sub
    Protected Sub Verificar()
        Dim obj As New Cls_Inventario_Verificacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim pdSerieNumerar As Double = 0
        Dim psconexion As String = Session("Ruta_Emp")
        Dim inventario As String = TxtCodigoAyuda.Text.ToString
        Dim pdInvUbiCodigo As Double = 0
        If TxtCodigoAyudaUbicacion.Text <> "" Then
            pdInvUbiCodigo = Nz(TxtCodigoAyudaUbicacion.Text)
        End If
        lblError.Text = ""
        Dim psInvUbicTipo As String = ""
        If RBAlmacen.Checked = True Then
            psInvUbicTipo = "1"
        ElseIf RBCentroC.Checked = True Then
            psInvUbicTipo = "2"
        ElseIf RBUbicaciones.Checked = True Then
            psInvUbicTipo = "9"
        End If
        Dim psInventarioCod As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psInventarioCod = DdlInventario.SelectedValue
        End If
        Dim dtInv As New DataTable
        Dim placa As Double = Nz(TxtNroPlaca.Text.Trim)
        Dim serie As String = TxtNroSerie.Text.ToString
        Dim tipo As String = ""
        Dim psEstadoInv As String = ""
        Dim ubicacion As String = TxtCodigoAyudaUbicacion.Text.ToString
        Try
            If ddlUbicacion.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingrese Ubicación.');", True)
            ElseIf placa = 0 And serie = "" Then
                TxtMensajeVerificar.Text = "Ingrese datos"
                TxtMensajeVerificar.Visible = True
            Else
                dt = obj.Inventario_BienExiste(Session("Ruta_Emp"), Session("CodEmpresa"), serie, placa)
                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        pdSerieNumerar = Nz(dr("SERIE_NUMERAR"))
                        dtInv = obj.Bien_Inventariado(Session("Ruta_Emp"), Session("CodEmpresa"), psInvUbicTipo, pdInvUbiCodigo, pdSerieNumerar, psInventarioCod)
                        If dtInv.Rows.Count > 0 Then
                            For Each drE As DataRow In dtInv.Rows
                                psEstadoInv = Nu(drE("INVDET_ESTADO_INVENTARIO"))
                            Next
                            TituloPregunta.Text = "El bien ya está verificado. ¿Desea actualizar datos de este artículo?"
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
                        Else
                            If serie <> "" And placa = 0 Then
                                TituloArticulo.Text = "Actualizar Artículo - Encontrado sin placa"
                                Cargar_Datos_Articulos("5")
                            ElseIf serie = "" And placa > 0 Then
                                TituloArticulo.Text = "Actualiza Artículo"
                                Cargar_Datos_Articulos("")
                            End If
                            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('show');", True)
                        End If
                    Next
                Else
                    TituloPregunta.Text = "El bien no existe. ¿Desea ingresarlo?"
                    If RBAlmacen.Checked = True Then
                        RBCentroCArea.Checked = False
                        RBAlmacenArea.Checked = True
                    ElseIf RBCentroC.Checked = True Then
                        RBAlmacenArea.Checked = False
                        RBCentroCArea.Checked = True
                    End If

                    LblCodAreaM.Text = TxtCodigoAyudaUbicacion.Text
                    TxtCodAreaM.Value = TxtCodigo.Text
                    TxtDescAreaM.Value = TxtDescripcion.Text
                    If LblCodUbicacionM.Text = "" And ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
                        LblCodUbicacionM.Text = ddlUbicacion.SelectedValue
                        TxtCodUbicacionM.Value = Left(ddlUbicacion.SelectedItem.Text, 4)
                        TxtDescUbicacionM.Value = Mid(ddlUbicacion.SelectedItem.Text, 8)
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
                End If
                Listar_Inventario_Verificacion()
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub DdlInventario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInventario.SelectedIndexChanged
        RBCentroC.Checked = False
        RBUbicaciones.Checked = False
        RBAlmacen.Checked = True
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
    End Sub
    Protected Sub TxtNroPlaca_TextChanged(sender As Object, e As EventArgs) Handles TxtNroPlaca.TextChanged
        Try
            Verificar()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Protected Sub TxtNroSerie_TextChanged(sender As Object, e As EventArgs) Handles TxtNroSerie.TextChanged
        Try
            Verificar()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Private Sub BtnBuscaAreaM_Click(sender As Object, e As EventArgs) Handles BtnBuscaAreaM.Click
        If RBAlmacenArea.Checked Then
            TituloPopup.Text = "Busca Almacén"
        ElseIf RBCentroCArea.Checked Then
            TituloPopup.Text = "Busca Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnCerrarArticulo_Click(sender As Object, e As EventArgs) Handles BtnCerrarArticulo.Click
        Limpiar_Cajas_Articulos()
        'Ocultar_Visible_Imagen(False)
    End Sub
    Private Sub BtnSi_Click(sender As Object, e As EventArgs) Handles BtnSi.Click
        Dim placa As String = TxtNroPlaca.Text.ToString
        Dim serie As String = TxtNroSerie.Text.ToString
        TxtNombreImagen.Value = ""
        DdlEstadoM.SelectedValue = "1"
        lblArtCodReal.Text = ""
        If RBAlmacen.Checked = True Then
            RBAlmacenArea.Checked = True
            TxtCodAreaM.Value = TxtCodigo.Text
            TxtDescAreaM.Value = TxtDescripcion.Text
            LblCodAreaM.Text = TxtCodigoAyudaUbicacion.Text
        End If
        If RBCentroC.Checked = True Then
            RBCentroCArea.Checked = True
            TxtCodAreaM.Value = TxtCodigo.Text
            TxtDescAreaM.Value = TxtDescripcion.Text
            LblCodAreaM.Text = TxtCodigoAyudaUbicacion.Text
        End If
        If LblCodUbicacionM.Text = "" And ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
            LblCodUbicacionM.Text = ddlUbicacion.SelectedValue
            TxtCodUbicacionM.Value = Left(ddlUbicacion.SelectedItem.Text, 4)
            TxtDescUbicacionM.Value = Mid(ddlUbicacion.SelectedItem.Text, 8)
        End If
        txtCantEq.Text = "1"
        imagenCarga.Visible = False
        If TituloPregunta.Text = "El bien ya está verificado. ¿Desea actualizar datos de este artículo?" Then
            TituloArticulo.Text = "Actualiza Artículo"
            BtnAgregarArticulo.Text = "Actualizar"
            Cargar_Datos_Articulos("placa")
        ElseIf TituloPregunta.Text = "El bien no existe. ¿Desea ingresarlo?" Then
            TituloArticulo.Text = "Registrar Artículo"
            BtnAgregarArticulo.Text = "Agregar"
            TxtPlacaNroM.Value = placa
            chkPlaca.Checked = True
            chkPlaca_CheckedChanged(sender, e)
            TxtSerieNroM.Text = serie
            TxtPlacaNroM.Value = placa
        ElseIf TituloArticulo.Text = "Actualizar Artículo - Encontrado sin placa" Then
            TituloArticulo.Text = "Actualiza Artículo"
            BtnAgregarArticulo.Text = "Actualizar"
            Cargar_Datos_Articulos("serie")
        Else
            TituloArticulo.Text = "Registrar Artículo"
            BtnAgregarArticulo.Text = "Agregar"
            TxtSerieNroM.Text = serie
            TxtPlacaNroM.Value = placa
            chkPlaca.Checked = True
            chkPlaca_CheckedChanged(sender, e)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnNo_Click(sender As Object, e As EventArgs) Handles BtnNo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('hide');", True)
        Listar_Inventario_Verificacion()
        TxtNroPlaca.Text = ""
        TxtNroSerie.Text = ""
    End Sub
    Private Sub BtnAgregarArticulo_Click(sender As Object, e As EventArgs) Handles BtnAgregarArticulo.Click
        Try
            Dim objV As New Cls_Inventario_Verificacion
            Dim objCn As New Cls_Conexion
            Dim objU As New Cls_Inventario_Ubicacion
            Dim dt As New DataTable
            Dim psconexion As String = Session("Ruta_Emp")
            Dim numerar As String = ""
            Dim psCodUbiInventario As String = TxtCodigoAyuda.Text.ToString

            Dim placa As String = TxtPlacaNroM.Value.ToString
            Dim serie As String = TxtSerieNroM.Text.ToString
            Dim codRelacionado As String = TxtCodRelacionadoM.Value.ToString
            Dim estado As String = DdlEstadoM.SelectedValue.ToString
            Dim responsable As String = ""
            If TxtResponsable.Value <> "" Then
                responsable = TxtResponsable.Value
            End If
            Dim codArticulo As String = TxtCodArticuloM.Value.ToString
            Dim tipoArticulo As String = LblArticuloM.Text.ToString
            Dim pdDestinoCodigo As String = LblCodAreaM.Text.ToString
            Dim psDestinoTipo As String = ""
            Dim psCodUbicacion As String = LblCodUbicacionM.Text.ToString
            Dim observacion As String = TxtObservacionM.Value.ToString

            Dim objFunInv As New clsInv_Procesos

            If RBAlmacenArea.Checked Then
                psDestinoTipo = "1"
            ElseIf RBCentroCArea.Checked Then
                psDestinoTipo = "2"
            End If

            Dim psTipoUbicacion As String = ""
            If RBAlmacen.Checked = True Then
                psTipoUbicacion = "1"
            ElseIf RBCentroC.Checked = True Then
                psTipoUbicacion = "2"
            ElseIf RBUbicaciones.Checked = True Then
                psTipoUbicacion = "9"
            End If
            Dim psEstadoInv As String = ""
            If TituloArticulo.Text = "Actualizar Artículo - Encontrado sin placa" Then
                psEstadoInv = "5"
            Else
                psEstadoInv = "1"
            End If
            Dim pdPlacaNro As Double = 0
            If TxtPlacaNroM.Value <> "" Then pdPlacaNro = Nz(TxtPlacaNroM.Value)
            Dim psSerieNro As String = Nu(TxtSerieNroM.Text)
            If pdPlacaNro = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar placa');", True)
            ElseIf psSerieNro = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar serie');", True)
            ElseIf estado = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione Estado');", True)
            ElseIf tipoArticulo = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione Artículo');", True)
            ElseIf pdDestinoCodigo = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccione Área');", True)
            ElseIf psCodUbicacion = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicación');", True)
            Else
                Call Verificar_Bien(sender, e)
                TxtNroSerie.Text = ""
                lblPlacaReal.Text = ""
                lblSerieReal.Text = ""
                TxtCodArticuloM.Value = ""
                TxtDescArticuloM.Value = ""
                lblSerieNumerar.Text = ""
                TxtPlacaNroM.Value = ""
                TxtSerieNroM.Text = ""
                TxtObservacionM.Value = ""
                TxtResponsable.Value = ""
                LblCodUbicacionM.Text = ""
                TxtCodRelacionadoM.Value = ""
                LblCodAreaM.Text = ""
                TxtCodAreaM.Value = ""
                txtCantEq.Text = "1"
                TxtDescAreaM.Value = ""
                TxtCodUbicacionM.Value = ""
                TxtDescUbicacionM.Value = ""
                TxtBuscarArticulo.Value = ""
                TxtBuscarSerie.Value = ""
                TxtDescripcionBA.Value = ""
                txtNroAtmM.Text = ""
                DdlEstadoM.SelectedValue = "< Seleccionar >"
                chkPlaca.Checked = False
                TxtNroPlaca.Text = ""
                TxtNroAtm.Text = ""
                Listar_Inventario_Verificacion()
                TxtNroPlaca.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('hide');", True)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub Verificar_Bien(sender As Object, e As EventArgs)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal3 As New SqlCommand
        Dim ValorSys As String = ""
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim RsL As SqlDataReader
        Dim psInvUbicTipo As String = ""
        ValorSys = Session("User") & FechaActual() & HoraActual()
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Dim pdUbiCodigo As Double = 0
        Dim psUbicTipo As String = ""
        Dim pdInvUbiCodigo As Double = 0
        Dim psInventarioCod As Double = 0
        Dim objV As New Cls_Inventario_Verificacion
        Dim numerar As Double = 0
        Dim dt As New DataTable
        Dim pdCodArt As Double = 0
        Dim pdCodArtReal As Double = 0
        pdCodArt = Nz(TxtCodArticuloM.Value)
        pdCodArtReal = Nz(lblArtCodReal.Text)
        Dim psSerieReal As String = lblSerieReal.Text
        Dim placa As String = TxtPlacaNroM.Value.ToString
        Dim serie As String = TxtSerieNroM.Text.ToString
        Dim pdPlacaReal As Double = 0

        If Nz(lblPlacaReal.Text) > 0 Then
            pdPlacaReal = Nz(lblPlacaReal.Text)
        End If

        If LblCodAreaM.Text <> "" Then
            pdUbiCodigo = Nz(LblCodAreaM.Text)
        End If
        If RBAlmacenArea.Checked = True Then
            psUbicTipo = "1"
        ElseIf RBCentroCArea.Checked = True Then
            psUbicTipo = "2"
        End If
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psInventarioCod = DdlInventario.SelectedValue
        End If

        If RBAlmacen.Checked = True Then
            psInvUbicTipo = "1"
        ElseIf RBCentroC.Checked = True Then
            psInvUbicTipo = "2"
        ElseIf RBUbicaciones.Checked = True Then
            psInvUbicTipo = "9"
        End If
        If TxtCodigoAyudaUbicacion.Text.ToString <> "" Then
            pdInvUbiCodigo = TxtCodigoAyudaUbicacion.Text.ToString()
        End If
        numerar = Nz(lblSerieNumerar.Text)

        Dim psSerieNuevo As String = ""
        Dim psBienIngresar As String = "SI"

        Dim psNroReg As String = ""
        Dim psEstadoInv As String = ""
        If lblEstadoInventario.Text = "2" Then
        If TituloArticulo.Text = "Actualizar Artículo - Encontrado sin placa" Then
            psEstadoInv = "5"
        ElseIf psSerieReal <> serie And numerar > 0 Then
            psEstadoInv = "6"
        ElseIf numerar = 0 Then
            psEstadoInv = "7"
        Else
            psEstadoInv = "1"
        End If
        Else
            'psEstadoInv = lblEstadoInventario.Text
            psBienIngresar = "NO"
        End If

        Dim pdCodUbicacion As Double = 0
        If LblCodUbicacionM.Text <> "" Then
            pdCodUbicacion = Nz(LblCodUbicacionM.Text)
        End If
        Dim pdCorrelativo As Double = 0
        Dim psExiste As String = ""
        Dim psRegularizar As String = ""
        If lblEqxPlacar.Text = "Si" Then
            psEstadoInv = "9"
        End If

        Dim pdCantEq As Double = 0
        pdCantEq = Nz(txtCantEq.Text)
        Dim ii As Long = 0
        Dim dtPlaca As New DataTable
        Dim objInv As New clsInv_InsUpdDel
        If numerar > 0 Then pdCantEq = 1

        Try
            If chkPlaca.Checked = True Then
                dtPlaca = objInv.InsDevolver_UltimaPlaca(Session("Ruta_Emp"), Session("CodEmpresa"))
                If dtPlaca.Rows.Count > 0 Then
                    For Each drP As DataRow In dtPlaca.Rows
                        placa = Nz(drP("Placa_Nro"))
                        serie = placa
                        psSerieReal = placa
                        pdPlacaReal = placa
                        lblSerieReal.Text = placa
                        lblPlacaReal.Text = placa
                    Next
                End If
            End If

            For ii = 1 To pdCantEq
                If ii > 1 Then
                    dtPlaca = objInv.InsDevolver_UltimaPlaca(Session("Ruta_Emp"), Session("CodEmpresa"))
                    If dtPlaca.Rows.Count > 0 Then
                        For Each drP As DataRow In dtPlaca.Rows
                            placa = Nz(drP("Placa_Nro"))
                        Next
                    End If
                    serie = placa
                    psSerieReal = placa
                    pdPlacaReal = placa
                    lblSerieReal.Text = placa
                    lblPlacaReal.Text = placa
                    numerar = 0
                End If
                If numerar <> 0 Then
                    If chkPlaca.Checked = True Then
                        dtPlaca = objInv.InsDevolver_UltimaPlaca(Session("Ruta_Emp"), Session("CodEmpresa"))
                        If dtPlaca.Rows.Count > 0 Then
                            For Each drP As DataRow In dtPlaca.Rows
                                placa = Nz(drP("Placa_Nro"))
                            Next
                        End If
                    End If
                Else
                    CmdGlobal.CommandText = " SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa")
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            numerar = Nz(Rs(0)) + 1
                        End While
                    Else
                        numerar = "1"
                    End If
                    Rs.Close()
                    psSerieNuevo = "SI"
                    If placa = "" Then psRegularizar = "P"
                    If serie = "" Then psRegularizar = "S"
                    CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " (SERIE_NUMERAR, ARTICULO_CODIGO, " _
                                          & " SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO, SERIE_REGULARIZAR, " _
                                          & " SERIE_NRO, PLACA_NRO, SERIE_ESTADO, SERIE_RESPONSABLE_OBSERVACION, SERIE_VALIDADO, SERIE_CONCILIADO,SERIE_ESTADO_INVENTARIO ) " _
                                          & " VALUES( " & numerar & "," & TxtCodArticuloM.Value & ",'N','" & ValorSys & "','0','S','1', " _
                                          & " '" & psRegularizar & "', '" & serie & "', " & IIf(placa = "", "NULL", placa) & ", '0', '" & TxtObservacionM.Value & "','0','2','7')"
                    CmdGlobal.ExecuteNonQuery()
                End If

                CmdGlobal.CommandText = " SELECT IUBIC.INVENTUBIC_CODIGO, IUBIC.INVENTUBIC_NRO, " _
                                & " IUBIC.INVENTUBIC_UBIC_TIPO, IUBIC.INVENTUBIC_UBIC_CODIGO,IUBIC.INVENTUBIC_ESTADO " _
                                & " FROM dbo.TBINVENTARIO I INNER JOIN dbo.TBINVENTARIO_UBICACIONES IUBIC ON " _
                                & " I.INVENT_CODIGO = IUBIC.INVENTUBIC_NRO AND i.EMPRESA_CODIGO = IUBIC.EMPRESA_CODIGO " _
                                & " WHERE (I.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (IUBIC.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                & " AND (I.INVENT_SYS_EST = '0') AND (IUBIC.INVENTUBIC_UBIC_TIPO='" & psInvUbicTipo & "') " _
                                & " AND IUBIC.INVENTUBIC_UBIC_CODIGO='" & pdInvUbiCodigo & "' AND IUBIC.INVENTUBIC_ESTADO='2'  " _
                                & " AND (IUBIC.INVENTUBIC_SYS_EST = '0') AND (IUBIC.INVENTUBIC_NRO='" & psInventarioCod & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        CmdGlobal2.CommandText = " SELECT * FROM TBINVENTARIO_DETALLE WHERE (INVDET_INVENTUBIC_CODIGO='" & Nz(Rs!INVENTUBIC_CODIGO) & "') AND INVDET_SERIE_NUMERAR =" & numerar & "  " _
                                                & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                        RsL = CmdGlobal2.ExecuteReader
                        If RsL.HasRows Then
                            While RsL.Read
                                If psEstadoInv <> "7" Then
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '" & psEstadoInv & "', INVDET_ESTADO_CONCILIADO = '1' , INVDET_PLACA_NRO = " & placa & ",  " _
                                                           & " INVDET_SERIE_ESTADO_EQUIPO = '" & DdlEstadoM.SelectedValue & "', INVDET_RESPONSABLE_OBSERVACION = '" & TxtObservacionM.Value & "', " _
                                                           & " INVDET_ART_CODIGO_REAL = " & pdCodArtReal & " " _
                                                           & " WHERE (INVDET_INVENTUBIC_CODIGO='" & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & "') AND (INVDET_SERIE_NUMERAR='" & Nz(RsL!INVDET_SERIE_NUMERAR) & "')  " _
                                                           & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                                    CmdGlobal3.ExecuteNonQuery()
                                Else
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '" & psEstadoInv & "', INVDET_PLACA_NRO = " & placa & ",  " _
                                                           & " INVDET_SERIE_ESTADO_EQUIPO = '" & DdlEstadoM.SelectedValue & "',INVDET_SERIE_NRO = '" & serie & "', " _
                                                           & " INVDET_RESPONSABLE_OBSERVACION = '" & TxtObservacionM.Value & "' , INVDET_ART_CODIGO_REAL = " & pdCodArtReal & " " _
                                                        & " WHERE (INVDET_INVENTUBIC_CODIGO='" & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & "') AND (INVDET_SERIE_NUMERAR='" & Nz(RsL!INVDET_SERIE_NUMERAR) & "')  " _
                                                        & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                If TxtResponsable.Value <> "" Then
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_RESPONSABLE_NOMBRE = '" & TxtResponsable.Value & "' " _
                                                            & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                    CmdGlobal3.ExecuteNonQuery()
                                End If 'INVDET_ESTADO_REGULARIZAR
                                If LblCodUbicacionM.Text <> "" Then
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_SERIE_AREA = " & LblCodUbicacionM.Text & " " _
                                                            & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                If psEstadoInv <> "7" And pdCodArt <> pdCodArtReal Then
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '11' " _
                                                            & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                If psEstadoInv = "1" Then
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '1' " _
                                                            & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                    CmdGlobal3.ExecuteNonQuery()
                                Else
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '2' " _
                                                            & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                psExiste = "N"

                                CmdGlobal3.CommandText = " SELECT * FROM TBINVENTARIO_VERIFICACION WHERE INVENTUBIC_CODIGO= " & Nz(Rs!INVENTUBIC_CODIGO) & " AND VERIF_SERIE_NUMERAR = " & numerar
                                Rs2 = CmdGlobal3.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        psExiste = "S"
                                        pdCorrelativo = Nz(Rs2("VERIF_CORRELATIVO"))
                                    End While
                                End If
                                Rs2.Close()

                                If psExiste = "N" Then
                                    CmdGlobal3.CommandText = " SELECT MAX(VERIF_CORRELATIVO) FROM TBINVENTARIO_VERIFICACION"
                                    Rs2 = CmdGlobal3.ExecuteReader
                                    If Rs2.HasRows Then
                                        While Rs2.Read
                                            pdCorrelativo = Nz(Rs2(0)) + 1
                                        End While
                                    Else
                                        pdCorrelativo = "1"
                                    End If
                                    Rs2.Close()
                                    CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_VERIFICACION (EMPRESA_CODIGO, INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR, VERIF_PLACA_NRO, VERIF_SERIE_NRO, " _
                                        & " VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO, VERIF_RESPONSABLE, VERIF_ESTADO_BIEN, VERIF_AREA_UBICACION, VERIF_ESTADO, VERIF_ART_CODIGO, " _
                                        & " VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_FECHA, VERIF_HORA,VERIF_SERIE_NRO_REAL,VERIF_PLACA_NRO_REAL,VERIF_REGULARIZAR, VERIF_CORRELATIVO,VERIF_OBSERVACION, VERIF_ART_CODIGO_REAL) VALUES ( " _
                                        & " '" & Session("CodEmpresa") & "'  , " & Nz(Rs!INVENTUBIC_CODIGO) & ", " & numerar & ", " & placa & ", '" & serie & "', " _
                                        & " '" & psInvUbicTipo & "', " & pdInvUbiCodigo & ", '" & TxtResponsable.Value & "', '" & DdlEstadoM.SelectedValue & "', " & pdCodUbicacion & ", '" & psEstadoInv & "', " & pdCodArt & ", " _
                                        & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & lblSerieReal.Text & "' ," & pdPlacaReal & " ,'" & IIf(psEstadoInv = "1", "1", "2") & "', " & pdCorrelativo & ", '" & TxtObservacionM.Value & "', " & pdCodArtReal & ")"
                                    CmdGlobal3.ExecuteNonQuery()
                                ElseIf psExiste = "S" Then
                                    CmdGlobal3.CommandText = " update TBINVENTARIO_VERIFICACION set VERIF_RESPONSABLE =  '" & TxtResponsable.Value & "', VERIF_PLACA_NRO= " & placa & ", VERIF_SERIE_NRO='" & serie & "'," _
                                                           & " VERIF_OBSERVACION = '" & TxtObservacionM.Value & "', VERIF_AREA_UBICACION = " & pdCodUbicacion & "  " _
                                                           & " where INVENTUBIC_CODIGO= " & Nz(Rs!INVENTUBIC_CODIGO) & " AND VERIF_SERIE_NUMERAR = " & numerar
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                If pdCodArt <> pdCodArtReal And psEstadoInv <> "7" Then
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_ESTADO_INVENTARIO = '11', VERIF_ESTADO = '11' " _
                                                            & " where INVENTUBIC_CODIGO= " & Nz(Rs!INVENTUBIC_CODIGO) & " AND VERIF_SERIE_NUMERAR = " & numerar
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                If psEstadoInv <> "7" Then
                                    CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                                        & " SERIE_CONCILIADO = '1', " _
                                                        & " SERIE_ESTADO_INVENTARIO = '" & psEstadoInv & "' " _
                                                        & " WHERE SERIE_NUMERAR = " & numerar
                                    CmdGlobal3.ExecuteNonQuery()
                                Else
                                    CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                                        & " SERIE_CONCILIADO = '2', " _
                                                        & " SERIE_NRO = '" & serie & "', " _
                                                        & " SERIE_ESTADO_INVENTARIO = '" & psEstadoInv & "' " _
                                                        & " WHERE SERIE_NUMERAR = " & numerar
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                            End While
                        Else
                            If psSerieNuevo <> "SI" Then
                                If pdInvUbiCodigo <> pdUbiCodigo Then
                                    If TituloArticulo.Text = "Actualizar Artículo - Encontrado sin placa" Then
                                        psEstadoInv = "8"
                                    Else
                                        psEstadoInv = "3"
                                    End If
                                End If
                                CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_DETALLE (EMPRESA_CODIGO,INVDET_INVENTUBIC_CODIGO,INVDET_ART_CODIGO, INVDET_SERIE_ESTADO_EQUIPO, " _
                                                        & " INVDET_SERIE_NUMERAR, INVDET_SERIE_NRO, INVDET_SYS_EST,INVDET_FECHA,INVDET_ESTADO_INGRESO,INVDET_ESTADO_INVENTARIO,INVDET_ESTADO_CONCILIADO ,INVDET_ESTADO_REGULARIZAR, " _
                                                        & " INVDET_UBIC_TIPO,INVDET_UBIC_CODIGO,INVDET_SYS_CRE,INVDET_ART_TIPO,INVDET_CANTIDAD,INVDET_SERIE_AREA, INVDET_PLACA_NRO,INVDET_PLACA_NRO_REAL,INVDET_SERIE_NRO_REAL, INVDET_ART_CODIGO_REAL)" _
                                                        & " VALUES ('" & Session("CodEmpresa") & "','" & Nz(Rs!INVENTUBIC_CODIGO) & "','" & TxtCodArticuloM.Value & "', '" & DdlEstadoM.SelectedValue & "', " _
                                                        & " '" & numerar & "','" & serie & "','0','" & FechaActual() & "','2','" & psEstadoInv & "','1', '2'," _
                                                        & " '" & psUbicTipo & "'," & pdUbiCodigo & ",'" & ValorSys & "','" & LblArticuloM.Text & "',1," & pdCodUbicacion & ", " & IIf(placa = "", "NULL", placa) & "," & pdPlacaReal & ", '" & psSerieReal & "', " & pdCodArtReal & ") "
                                CmdGlobal3.ExecuteNonQuery()
                                CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                                        & " SERIE_CONCILIADO = '1', " _
                                                        & " SERIE_ESTADO_INVENTARIO = '" & psEstadoInv & "' " _
                                                        & " WHERE SERIE_NUMERAR = " & numerar
                                CmdGlobal3.ExecuteNonQuery()
                                CmdGlobal3.CommandText = " SELECT MAX(VERIF_CORRELATIVO) FROM TBINVENTARIO_VERIFICACION"
                                Rs2 = CmdGlobal3.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        pdCorrelativo = Nz(Rs2(0)) + 1
                                    End While
                                Else
                                    pdCorrelativo = "1"
                                End If
                                Rs2.Close()
                                CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_VERIFICACION (EMPRESA_CODIGO, INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR, VERIF_PLACA_NRO, VERIF_SERIE_NRO, " _
                                        & " VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO, VERIF_RESPONSABLE, VERIF_ESTADO_BIEN, VERIF_AREA_UBICACION, VERIF_ESTADO, VERIF_ART_CODIGO, " _
                                        & " VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_FECHA, VERIF_HORA,VERIF_SERIE_NRO_REAL,VERIF_PLACA_NRO_REAL,VERIF_REGULARIZAR,VERIF_CORRELATIVO, VERIF_OBSERVACION, VERIF_ART_CODIGO_REAL) VALUES ( " _
                                        & " '" & Session("CodEmpresa") & "'  , " & Nz(Rs!INVENTUBIC_CODIGO) & ", " & numerar & ", " & placa & ", '" & serie & "', " _
                                        & " '" & psUbicTipo & "', " & pdUbiCodigo & ", '" & TxtResponsable.Value & "', '" & DdlEstadoM.SelectedValue & "', " & pdCodUbicacion & ", '" & psEstadoInv & "', " & Nz(TxtCodArticuloM.Value) & ", " _
                                        & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & lblSerieReal.Text & "'," & pdPlacaReal & " ,'2' ," & pdCorrelativo & " ,'" & TxtObservacionM.Value & "', " & pdCodArtReal & ")"
                                CmdGlobal3.ExecuteNonQuery()
                            ElseIf psSerieNuevo = "SI" Then
                                CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                                        & " UBICACT_TIPO = '" & psUbicTipo & "', " _
                                                        & " SERIE_CONCILIADO = '2', " _
                                                        & " SERIE_ESTADO_INVENTARIO = '7', " _
                                                        & " UBICACT_CODIGO = " & pdUbiCodigo & " " _
                                                        & " WHERE SERIE_NUMERAR = " & numerar
                                CmdGlobal3.ExecuteNonQuery()
                                CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_DETALLE (EMPRESA_CODIGO,INVDET_INVENTUBIC_CODIGO,INVDET_ART_CODIGO,INVDET_SERIE_ESTADO_EQUIPO, " _
                                                        & " INVDET_SERIE_NUMERAR,INVDET_SERIE_NRO, INVDET_SYS_EST,INVDET_FECHA,INVDET_ESTADO_INGRESO,INVDET_ESTADO_INVENTARIO,INVDET_ESTADO_CONCILIADO ,INVDET_ESTADO_REGULARIZAR," _
                                                        & " INVDET_UBIC_TIPO,INVDET_UBIC_CODIGO,INVDET_SYS_CRE,INVDET_ART_TIPO,INVDET_CANTIDAD,INVDET_SERIE_AREA, INVDET_PLACA_NRO,INVDET_PLACA_NRO_REAL,INVDET_SERIE_NRO_REAL, INVDET_ART_CODIGO_REAL)" _
                                                        & " VALUES ('" & Session("CodEmpresa") & "','" & Nz(Rs!INVENTUBIC_CODIGO) & "','" & Nz(TxtCodArticuloM.Value) & "', '" & DdlEstadoM.SelectedValue & "', " _
                                                        & " '" & numerar & "','" & serie & "','0','" & FechaActual() & "','3','7', '2','2'," _
                                                        & " '" & psUbicTipo & "','" & pdUbiCodigo & "','" & ValorSys & "','" & LblArticuloM.Text & "',1," & pdCodUbicacion & ", " & IIf(placa = "", "NULL", placa) & "," & pdPlacaReal & ", '" & psSerieReal & "', " & pdCodArtReal & ") "
                                CmdGlobal3.ExecuteNonQuery()

                                CmdGlobal3.CommandText = " SELECT MAX(VERIF_CORRELATIVO) FROM TBINVENTARIO_VERIFICACION"
                                Rs2 = CmdGlobal3.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        pdCorrelativo = Nz(Rs2(0)) + 1
                                    End While
                                Else
                                    pdCorrelativo = "1"
                                End If
                                Rs2.Close()
                                CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_VERIFICACION (EMPRESA_CODIGO, INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR, VERIF_PLACA_NRO, VERIF_SERIE_NRO, " _
                                        & " VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO, VERIF_RESPONSABLE, VERIF_ESTADO_BIEN, VERIF_AREA_UBICACION, VERIF_ESTADO, VERIF_ART_CODIGO, " _
                                        & " VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_FECHA, VERIF_HORA,VERIF_SERIE_NRO_REAL,VERIF_PLACA_NRO_REAL,VERIF_REGULARIZAR,VERIF_CORRELATIVO,VERIF_OBSERVACION, VERIF_ART_CODIGO_REAL) VALUES ( " _
                                        & " '" & Session("CodEmpresa") & "'  , " & Nz(Rs!INVENTUBIC_CODIGO) & ", " & numerar & ", " & placa & ", '" & serie & "', " _
                                        & " '" & psUbicTipo & "', " & pdUbiCodigo & ", '" & TxtResponsable.Value & "', '" & DdlEstadoM.SelectedValue & "', " & pdCodUbicacion & ", '" & psEstadoInv & "', " & Nz(TxtCodArticuloM.Value) & ", " _
                                        & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & lblSerieReal.Text & "' ," & pdPlacaReal & ",'2'," & pdCorrelativo & " ,'" & TxtObservacionM.Value & "', " & pdCodArtReal & " )"
                                CmdGlobal3.ExecuteNonQuery()
                                If TxtResponsable.Value <> "" Then
                                    CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_RESPONSABLE_NOMBRE = '" & TxtResponsable.Value & "' " _
                                                            & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(Rs!INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & numerar
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                            End If
                        End If
                        RsL.Close()
                    End While
                End If
                Rs.Close()
            Next
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnBuscaUbicacionM_Click(sender As Object, e As EventArgs) Handles BtnBuscaUbicacionM.Click
        TituloPopup.Text = "Busca Ubicaciones"
        BtnBuscar_Click(sender, e)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').one('hidden.bs.modal', function() { $('#Modal').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnBuscaArticuloM_Click(sender As Object, e As EventArgs) Handles BtnBuscaArticuloM.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
    End Sub
    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        GvBuscarArticulos.DataSource = Nothing
        GvBuscarArticulos.DataBind()
        gvEqNoInventariado.DataSource = Nothing
        gvEqNoInventariado.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
        Limpiar_Cajas_Buscar_Articulos()
        BtnNuevoBA.Visible = True
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
        If pdCodart <> 0 Then psListaArt = ""
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
                BtnNuevoBA.Visible = True
                lblCodClas.Text = ""
                TxtClasificacionBA.Value = ""
            End If

            Dim psCodInventario As Double = 0
            Dim psCodInvUbica As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                psCodInventario = Nz(DdlInventario.SelectedValue)
            End If
            If TxtCodigoAyuda.Text <> "" Then
                psCodInvUbica = Nz(TxtCodigoAyuda.Text)
            End If
            dt = Nothing

            dt = objListaInv.Inventario_ListaNoInventariado(Session("Ruta_Emp"), psCodInventario, psCodInvUbica, pdCodArt, psDescripcion)
            gvEqNoInventariado.DataSource = dt
            gvEqNoInventariado.DataBind()
            If dt.Rows.Count > 1 Then
                lblNroEqNoInv.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblNroEqNoInv.Text = "Hay 1 registros."
            ElseIf dt.Rows.Count = 0 Then
                lblNroEqNoInv.Text = "Hay 0 registros."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
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
    Private Sub GvBuscarArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodArticuloM.Value = GvBuscarArticulos.Rows(Index).Cells(1).Text
            lblArtCodReal.Text = GvBuscarArticulos.Rows(Index).Cells(1).Text
            If Nz(lblArtCodReal.Text) = 0 Then lblArtCodReal.Text = GvBuscarArticulos.Rows(Index).Cells(1).Text
            TxtDescArticuloM.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            LblArticuloM.Text = GvBuscarArticulos.Rows(Index).Cells(4).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)

        End If

        Limpiar_Cajas_Buscar_Articulos()
    End Sub
    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""
        lblRegistroCant.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        GvListaCantxArt.DataSource = dt
        GvListaCantxArt.DataBind()
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""
        lblRegistroCant.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        GvListaCantxArt.DataSource = dt
        GvListaCantxArt.DataBind()
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
    End Sub
    Private Sub RBUbicaciones_CheckedChanged(sender As Object, e As EventArgs) Handles RBUbicaciones.CheckedChanged
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""
        lblRegistroCant.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        GvListaCantxArt.DataSource = dt
        GvListaCantxArt.DataBind()
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
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
        Dim psNumero As Integer = 0
        lblCodClas.Text = TrvClasificacion.SelectedValue
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
    Private Sub BtnCargaArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargaArchivo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalCargarArchivo').modal('show');", True)
    End Sub
    Protected Sub BtnCargarArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargarArchivo.Click

        Dim Linea As String = ""
        Try
            If fileUpload.HasFile Then
                Dim fileName As String = Path.GetFileName(fileUpload.PostedFile.FileName)
                Dim fileExtension As String = Path.GetExtension(fileName)

                ' Verifica que el archivo sea un archivo de texto
                If fileExtension.ToLower() = ".txt" Then
                    ' Lee el contenido del archivo de texto
                    Dim fileContent As String = ""
                    Using reader As New StreamReader(fileUpload.PostedFile.InputStream)
                        While Not reader.EndOfStream
                            ' Lee cada línea del archivo y agrega un salto de línea
                            fileContent = reader.ReadLine()
                            ' Actualiza el contenido del UpdatePanel
                            TxtNroPlaca.Text = CDbl(Val(fileContent))
                            TxtNroPlaca_TextChanged(sender, e)
                            Session("Fin") = "Si"
                        End While
                    End Using
                    '' Muestra el contenido en la página
                Else
                    Session("Fin") = ""
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El archivo seleccionado no es un archivo de texto válido.');", True)
                End If
            Else

            End If
        Catch Ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & Ex.Message & " .');", True)

        Catch Ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicacion: " & Ex.Message & " .');", True)
        Finally
        End Try
    End Sub

    Private Sub BtnNuevoEquipo_Click(sender As Object, e As EventArgs) Handles BtnNuevoEquipo.Click
        DdlEstadoM.SelectedValue = "1"
        If RBAlmacen.Checked = True Then
            RBAlmacenArea.Checked = True
            TxtCodAreaM.Value = TxtCodigo.Text
            TxtDescAreaM.Value = TxtDescripcion.Text
            LblCodAreaM.Text = TxtCodigoAyudaUbicacion.Text
        End If
        If RBCentroC.Checked = True Then
            RBCentroCArea.Checked = True
            TxtCodAreaM.Value = TxtCodigo.Text
            TxtDescAreaM.Value = TxtDescripcion.Text
            LblCodAreaM.Text = TxtCodigoAyudaUbicacion.Text
        End If
        If LblCodUbicacionM.Text = "" And ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
            LblCodUbicacionM.Text = ddlUbicacion.SelectedValue
            TxtCodUbicacionM.Value = Left(ddlUbicacion.SelectedItem.Text, 4)
            TxtDescUbicacionM.Value = Mid(ddlUbicacion.SelectedItem.Text, 8)
        End If
        txtCantEq.Text = "1"
        TituloPregunta.Text = "¿Desea ingresar un nuevo equipo?"
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalPregunta').modal('show');", True)
    End Sub

    Private Sub chkPlaca_CheckedChanged(sender As Object, e As EventArgs) Handles chkPlaca.CheckedChanged
        If chkPlaca.Checked = True Then
            Call UltimaPlaca()
        Else
            TxtPlacaNroM.Value = ""
        End If
    End Sub
    Private Sub UltimaPlaca()
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim ValorSys As String = ""
        Dim Rs As SqlDataReader
        Dim psInvUbicTipo As String = ""
        ValorSys = Session("User") & FechaActual() & HoraActual()
        Cn.Open() : CmdGlobal.Connection = Cn


        CmdGlobal.CommandText = "SELECT MAX(PLACA_CORRELATIVA) FROM TBINV_PLACA_CORRELATIVA WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                TxtPlacaNroM.Value = Nz(Rs(0)) + 1
            End While
        End If
        TxtSerieNroM.Text = TxtPlacaNroM.Value
        Rs.Close()

    End Sub

    Protected Sub btnCerrarModal_Click(sender As Object, e As EventArgs) Handles btnCerrarModal.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').one('hidden.bs.modal', function() { $('#myModalError').modal('hide'); }).modal('show');", True)
    End Sub

    Private Sub GvListaCantxArt_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaCantxArt.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim pdCodArt As Double = 0
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim dtO As New DataTable
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Try
            If e.CommandName = "Detalle" Then

                pdCodArt = Nz(GvListaCantxArt.Rows(Index).Cells(1).Text)
                lblCodArticuloBus.Text = Nz(GvListaCantxArt.Rows(Index).Cells(1).Text)
                If DdlInventario.SelectedValue <> "< Seleccionar >" Then pdCodInv = Nz(DdlInventario.SelectedValue)
                pdCodUbicInv = Nz(TxtCodigoAyuda.Text)

                dt = obj.Inventario_Verificacion_ListaxArticulo(Session("Ruta_Emp"), pdCodInv, pdCodUbicInv, pdCodArt)
                GvListaVerificarInventario.DataSource = dt
                GvListaVerificarInventario.DataBind()
                If dt.Rows.Count > 1 Then
                    LblContador.Text = "Hay " & dt.Rows.Count & " registros."
                    lblObsArticulo.Visible = True
                    lblObsArticulo.Text = "Observación del artículo " &
                    txtObsArticulo.Visible = True
                    lblGuardar.Visible = True
                    BtnGuardarObs.Visible = True
                ElseIf dt.Rows.Count = 1 Then
                    LblContador.Text = "Hay 1 registro."
                    lblObsArticulo.Visible = True
                    txtObsArticulo.Visible = True
                    lblGuardar.Visible = True
                    BtnGuardarObs.Visible = True
                ElseIf dt.Rows.Count = 0 Then
                    LblContador.Text = "Hay 0 registro."
                End If

            End If

            dtO = Nothing
            Dim codigo As String = TxtCodigoAyuda.Text.ToString

            Dim tipo As String = ""
            Dim ubicacion As String = TxtCodigoAyudaUbicacion.Text.ToString
            If RBAlmacen.Checked Then
                tipo = "1"
            ElseIf RBCentroC.Checked Then
                tipo = "2"
            ElseIf RBUbicaciones.Checked Then
                tipo = "9"
            End If
            dtO = obj.Lista_Inventario_Verificacion_Otros(Session("Ruta_Emp"), codigo, tipo, ubicacion)
            GvListaVerificarInventarioOtros.DataSource = dtO
            GvListaVerificarInventarioOtros.DataBind()

            If dt.Rows.Count > 1 Then
                lblRegistro2.Text = "Hay " & dtO.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro2.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro2.Text = "Hay 0 registro."
            End If

            dtO = obj.Lista_Inventario_Verificacion_Nuevos(Session("Ruta_Emp"), codigo, tipo, Ubicacion)
            GvListaVerificarInventarioNuevos.DataSource = dtO
            GvListaVerificarInventarioNuevos.DataBind()

            If dt.Rows.Count > 1 Then
                lblRegistro3.Text = "Hay " & dtO.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistro3.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistro3.Text = "Hay 0 registro."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
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
            If psCodClasif = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar descripción del bien.');", True)
            ElseIf psCodClasif = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Clasificación.');", True)
            ElseIf pdTipoArt = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Tipo.');", True)
            Else
                obj.RegistrarCatalogo(Session("Ruta_Emp"), pdCodArt, pdTipoArt, psCodClasif, 0, 0, 0, psArtDescripcion, Left(psArtDescripcion, 19), TxtNumParteBA.Value, "", 34, 0, "", 0, 0, 0, 0, 0, Session("User"))
            End If
            BtnNuevoBA.Visible = True
            BtnBuscarBA_Click(sender, e)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub TxtNroAtm_TextChanged(sender As Object, e As EventArgs) Handles TxtNroAtm.TextChanged
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim ValorSys As String = ""
        Dim Rs As SqlDataReader
        Dim pdNorPlaca As Double = 0
        Dim pdNroAtm As Double = 0
        If TxtNroAtm.Text <> "" Then
            pdNroAtm = Nz(TxtNroAtm.Text)
        End If

        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            CmdGlobal.CommandText = "SELECT PLACA_NRO FROM TBINV_ARTICULOS_SERIES_0001 WHERE SERIE_ATM_NROTERMINAL = " & pdNroAtm
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pdNorPlaca = Nz(Rs(0))
                End While
            End If
            Rs.Close()
            If pdNorPlaca <> 0 Then
                TxtNroPlaca.Text = pdNorPlaca
                Verificar()
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No se ha encontrado la placa del terminal ATM.');", True)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try

    End Sub

    Private Sub gvEqNoInventariado_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvEqNoInventariado.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            chkPlaca.Checked = False
            TxtPlacaNroM.Value = gvEqNoInventariado.Rows(Index).Cells(5).Text
            TxtSerieNroM.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvEqNoInventariado.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblSerieNumerar.Text = gvEqNoInventariado.Rows(Index).Cells(6).Text
            lblSerieReal.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvEqNoInventariado.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblPlacaReal.Text = gvEqNoInventariado.Rows(Index).Cells(5).Text
            TxtCodArticuloM.Value = gvEqNoInventariado.Rows(Index).Cells(1).Text
            TxtDescArticuloM.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvEqNoInventariado.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtCodRelacionadoM.Value = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvEqNoInventariado.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            LblArticuloM.Text = gvEqNoInventariado.Rows(Index).Cells(7).Text
            lblArtCodReal.Text = gvEqNoInventariado.Rows(Index).Cells(1).Text
            lblEqxPlacar.Text = "Si"
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
        End If

        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub TxtDescripcion_TextChanged(sender As Object, e As EventArgs) Handles TxtDescripcion.TextChanged
        BtnBuscarBA_Click(sender, e)
    End Sub


    Private Sub Llenar_DatosOficina()
        Dim pdCCCodigo As Double = 0
        Dim dt As New DataTable
        Dim objCC As New clsLogis_Listado
        pdCCCodigo = Nz(Session("CodSeccion"))
        Try
            dt = objCC.Busca_Centro_Costos_Seccion_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), 0, pdCCCodigo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtCCCod.Text = Nu(dr("CECOSE_COD_INTERNO"))
                    txtCCDescripcion.Text = Nu(dr("CECOSE_DESCRIPCION"))
                    txtCCCargo.Text = Nu(dr("CARGO"))
                    txtCCNombre.Text = Nu(dr("NOMBRE"))
                    txtCCAnexo.Text = Nu(dr("ANEXO"))
                    txtCCTelefono.Text = Nu(dr("TELEFONO"))
                    txtCCCelular.Text = Nu(dr("CEL_BANCO"))
                    txtCCCorreo.Text = Nu(dr("CORREO"))
                    txtCCTipo.Text = Nu(dr("TIPO_OFICINA"))
                Next
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos:" & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('ha ocurrido un error en la aplicacion:" & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnMostrarModal_Click(sender As Object, e As EventArgs) ' Handles BtnMostrarModal.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#miModalDatos').modal('show');", True)
    End Sub

    Private Sub TxtBusArticulo_TextChanged(sender As Object, e As EventArgs) Handles txtBusArticulo.TextChanged

        Dim obj As New clsInv_InsUpdDel
        Dim dt As New DataTable
        Dim pdArtCodigo As Double = 0
        pdArtCodigo = Nz(TxtBusArticulo.Text)

        LblError.Text = ""
        Try

            dt = obj.Existe_articulo(Session("Ruta_Emp"), Session("CodEmpresa"), pdArtCodigo)
            If dt.Rows.Count = 0 Then
                TxtBusArticulo.Text = ""
                LblError.Text = "No existe el Artículo."
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No existe el Artículo.');", True)
            Else

                txtBusArticulo.Text = txtBusArticulo.Text
                LblArticuloM.Text = txtBusArticulo.Text
                TxtCodArticuloM.Value = txtBusArticulo.Text
                txtCantEq.Text = "1"
                imagenCarga.Visible = False
                DdlEstadoM.SelectedValue = "1"

                If LblCodUbicacionM.Text = "" And ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
                    LblCodUbicacionM.Text = ddlUbicacion.SelectedValue
                    TxtCodUbicacionM.Value = Left(ddlUbicacion.SelectedItem.Text, 4)
                    TxtDescUbicacionM.Value = Mid(ddlUbicacion.SelectedItem.Text, 8)
                End If

                If RBAlmacen.Checked = True Then RBAlmacenArea.Checked = True
                If RBCentroC.Checked = True Then RBCentroCArea.Checked = True
                TxtCodAreaM.Value = TxtCodigo.Text
                TxtDescAreaM.Value = TxtDescripcion.Text
                LblCodAreaM.Text = TxtCodigoAyudaUbicacion.Text

                TxtNombreImagen.Visible = True
                Ocultar_Visible_Imagen(True)
                div_imagen.Visible = True
                imagenCarga.Visible = False
                TxtNombreImagen.Visible = True
                lblNombreimg.Text = "Nombre de la imagen"

                If dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        If Not IsDBNull(dr("Imagen")) Then
                            TxtNombreImagen.Value = Nu(dr("ART_IMG_NOM").ToString)
                            TxtDescArticuloM.Value = Nu(dr("ART_DESCRIPCION").ToString)
                            Dim imageData As Byte() = DirectCast(dr("Imagen"), Byte())
                            Dim base64String As String = Convert.ToBase64String(imageData)
                            imagenCarga.ImageUrl = "data:image/jpeg;base64," + base64String
                            imagenCarga.Visible = True
                        Else
                            TxtDescArticuloM.Value = Nu(dr("ART_DESCRIPCION").ToString)
                            imagenCarga.Visible = False
                        End If
                    Next
                End If

                chkPlaca.Checked = True
                chkPlaca_CheckedChanged(sender, e)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulos').modal('show');", True)
                TxtBusArticulo.Text = ""
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnIngObs_Click(sender As Object, e As EventArgs) Handles BtnIngObs.Click
        Call NuevaObservacion()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalObservaciones').modal('show');", True)
    End Sub

    Private Sub NuevaObservacion()
        Dim obj As New Cls_Inventario
        Dim pdNroObs As Double = 0
        Dim dt As New DataTable
        Dim pdCodUbica As Double = 0
        pdCodUbica = Nz(TxtCodigoAyudaUbicacion.Text)
        dt = obj.Devolver_UltimaObservacion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodUbica)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                pdNroObs = Nz(dr(0))
            Next
        End If
        dt = Nothing
        dt = obj.Lista_Observacion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodUbica)
        If dt.Rows.Count > 0 Then
            gvObservacion.DataSource = dt
            gvObservacion.DataBind()
        End If
        dt = Nothing
        txtObsDetalle.Text = ""
        txtObsNro.Text = pdNroObs
        txtObsNro.ReadOnly = True
    End Sub

    Private Sub BtnObsCancelar_Click(sender As Object, e As EventArgs) Handles BtnObsCancelar.Click
        txtObsDetalle.Text = ""
        txtObsNro.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalObservaciones').modal('hide');", True)
    End Sub

    Private Sub BtnObsGuardar_Click(sender As Object, e As EventArgs) Handles BtnObsGuardar.Click
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Dim psDetalle As String = ""
        Dim pdCodUbicac As Double = 0
        Dim psSysCre As String = ""
        Try
            If txtObsDetalle.Text <> "" Then psDetalle = txtObsDetalle.Text
            pdCodUbicac = Nz(TxtCodigoAyudaUbicacion.Text)
            psSysCre = Session("User") & FechaActual() & HoraActual()
            obj.Insertar_Observacion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodUbicac, psDetalle, psSysCre)
            Call NuevaObservacion()
            'ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalObservaciones').modal('hide');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
End Class
