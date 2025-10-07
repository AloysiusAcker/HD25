Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Imports ImageResizer
Imports System.IO
Partial Class Inventario_Inventario_Recepcion_Listado
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cboBusEstado.Items.Clear()
            Dim lst1 As New ListItem : lst1.TEXT = "Recepción Generada" : lst1.Value = 1 : cboBusEstado.Items.Add(lst1)
            Dim lst2 As New ListItem : lst2.Text = "Recepción Recibida Ok" : lst2.Value = 2 : cboBusEstado.Items.Add(lst2)
            Dim lst3 As New ListItem : lst3.Text = "Recepción Recibida No Ok" : lst3.Value = 3 : cboBusEstado.Items.Add(lst3)
            Dim lst4 As New ListItem : lst4.Text = "Recepciones Recibidas" : lst4.Value = -1 : cboBusEstado.Items.Add(lst4)
            Dim lst5 As New ListItem : lst5.Text = "Todas las Recepciones" : lst5.Value = -2 : cboBusEstado.Items.Add(lst5)
            Dim lst6 As New ListItem : lst6.Text = "Recepción Anulada" : lst6.Value = 4 : cboBusEstado.Items.Add(lst6)
            cboBusEstado.Items.Add("< Seleccionar >") : cboBusEstado.SelectedValue = "< Seleccionar >"
            obj.Llena_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), cboBusAlmacen, Session("User"))
            obj.Llena_Motivo_Ing(Session("Ruta_Emp"), Session("CodEmpresa"), CboBusMotivo)
            Call LLenar_TipoaArticulo()
            Ocultar_Visible_Imagen(False)
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

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnListar.Click
        Try
            LblError.Text = ""
            lblReg.Text = ""
            lblRegDet.Text = ""
            If cboBusAlmacen.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Falta seleccionar el Almacén.');", True)
            ElseIf ChkBusMotivo.Checked = True And CboBusMotivo.SelectedValue = "< Seleccionar >" Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Falta seleccionar el Motivo de Ingreso.');", True)
            ElseIf ChkBusProveedor.Checked = True Then
                If txtBusProvCodigo.Text = "" Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Falta seleccionar proveedor de busqueda.');", True)
                End If
            Else
                Dim CampoClasif As String = ""
                Dim Clasificacion As String = ""
                Dim Sql As String = ""
                Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                Dim CmdGlobal As New SqlCommand
                Dim CmdGlobal2 As New SqlCommand
                Dim CmdGlobal3 As New SqlCommand
                Dim Rs As SqlDataReader
                Dim psFechaIni As String = ""
                Dim psFechaFin As String = ""
                Dim psCodMotivo As String = ""


                Dim CodigoRecep As String = ""
                Clasificacion = ""
                Cn.Open() : Cn2.Open() : Cn3.Open()
                CmdGlobal.Connection = Cn
                CmdGlobal2.Connection = Cn2
                CmdGlobal.Connection = Cn3
                Dim pslblCodNivel As String = ""
                'buscar art por clasificacion

                If lblCodClas.Text <> "" Then
                    Sql = " SELECT CLAS_COD_NIVEL, CLAS_CODIGO, CLAS_NIVEL2 " _
                        & " From dbo.TBINV_ARTICULO_CLASIFICACION WHERE (CLAS_SYS_EST = '0') AND CLAS_CODIGO = " & lblCodClas.Text
                    CmdGlobal.CommandText = Sql
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            pslblCodNivel = Nu(Rs!CLAS_COD_NIVEL)
                        End While
                    End If
                    Rs.Close()
                End If

                If ChkClasificacion.Checked = True And pslblCodNivel <> "" And lblCodClas.Text <> "" Then
                    CampoClasif = "AND CLAS_NIVEL" & pslblCodNivel & "  ='" & lblCodClas.Text & "' "
                    Sql = " SELECT CLAS_COD_NIVEL, CLAS_CODIGO, CLAS_NIVEL2 " _
                        & " From dbo.TBINV_ARTICULO_CLASIFICACION WHERE (CLAS_SYS_EST = '0')"
                    Sql = Sql & CampoClasif
                    CmdGlobal.CommandText = Sql
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            If Clasificacion <> "" Then Clasificacion = Clasificacion & ","
                            Clasificacion = Clasificacion & Nu(Rs!CLAS_CODIGO)
                        End While
                    End If
                    Rs.Close()
                End If

                If (ChkBusArticulo.Checked = True And TxtBusArtCodigo.Text <> "") Or (TxtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" And Clasificacion <> "") Then
                    Sql = " SELECT R.RECEP_CODIGO, RD.ARTICULO_CODIGO " _
                        & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
                        & " dbo.TBINV_ALMACEN_RECEPCION_DET RD ON R.EMPRESA_CODIGO = RD.EMPRESA_CODIGO AND R.RECEP_CODIGO = RD.RECEP_CODIGO INNER JOIN " _
                        & " TBINV_ARTICULOS A ON A.EMPRESA_CODIGO=RD.EMPRESA_CODIGO AND RD.ARTICULO_CODIGO=A.ART_CODIGO " _
                        & " WHERE (RD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (R.RECEP_SYS_EST = '0') " _
                        & " AND (R.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RD.RECEPD_SYS_EST = '0') AND (ART_SYS_EST='0')"
                    If ChkClasificacion.Checked = True And TxtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" Then Sql = Sql & " AND A.ART_CLASIFICACION IN (" & Clasificacion & ")"
                    If ChkBusArticulo.Checked = True And TxtBusArtCodigo.Text <> "" Then Sql = Sql & " AND rd.ARTICULO_CODIGO = " & TxtBusArtCodigo.Text.Trim & ""
                    Sql = Sql & " ORDER BY r.RECEP_CODIGO ASC "
                    CmdGlobal.CommandText = Sql
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            If CodigoRecep <> "" Then CodigoRecep = CodigoRecep & ","
                            CodigoRecep = CodigoRecep & Nu(Rs!RECEP_CODIGO)
                        End While
                    End If
                    Rs.Close()
                End If
                Dim n As String = cboBusEstado.SelectedValue.Trim
                Dim pdCodalmacen As Double = 0
                If cboBusAlmacen.SelectedValue <> "< Seleccionar >" Then
                    pdCodalmacen = Nz(cboBusAlmacen.SelectedValue.Trim)
                End If
                Dim pdCodRecep As Double = 0
                Dim pdCodProv As Double = 0
                If txtBusProvCodigo.Text <> "" Then pdCodProv = txtBusProvCodigo.Text
                Dim psEstado As String = ""
                If n = "-1" Then
                    psEstado = "2,3"
                ElseIf n = "-2" Or n = "< Seleccionar >" Then
                    psEstado = "1,2,3,4"
                Else
                    psEstado = n
                End If
                If ChkBusArticulo.Checked = True And CodigoRecep = "" Then Exit Sub
                If ChkBusFecha.Checked = True Then
                    psFechaIni = Right(txtBusFecIni.Text, 4) + Mid(txtBusFecIni.Text, 4, 2) + Left(txtBusFecIni.Text, 2)
                    psFechaFin = Right(txtBusFecFin.Text, 4) + Mid(txtBusFecFin.Text, 4, 2) + Left(txtBusFecFin.Text, 2)
                End If
                If CboBusMotivo.SelectedValue <> "< Seleccionar >" Then
                    psCodMotivo = CboBusMotivo.SelectedValue
                End If
                Dim psNroOCompra As String = ""
                If ChkOC.Checked = True And TxtNroOC.Text.Trim <> "" Then
                    psNroOCompra = TxtNroOC.Text.Trim
                End If
                Dim dt As DataTable
                dt = obj.Lista_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodalmacen, CodigoRecep, psEstado, pdCodProv, psFechaIni, psFechaFin, psCodMotivo, psNroOCompra)
                Flex.DataSource = dt
                Flex.DataBind()
                lblReg.Text = dt.Rows.Count & " registros"
                FlexDet.DataSource = Nothing
                FlexDet.DataBind()
            End If
        Catch ex As SqlException
            LblError.Text = "Se ha producido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            LblError.Text = "Se ha producido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub BtnBuscarBA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBuscarBA.Click
        'Try
        '    Dim obj As New Cls_Catalogo
        '    Dim objCn As New Cls_Conexion
        '    Dim dt As New DataTable
        '    Dim psListaArt As String = "1"
        '    Dim psListaMarca As String = "1"
        '    Dim psListaModelo As String = "1"
        '    Dim psconexion As String = Session("Ruta_Emp")
        '    Dim codigo As String = TxtCodArticuloBA.Value.ToString
        '    Dim clasificacion As String = LblCodClasificacionBA.Text.ToString
        '    Dim descripcion As String = TxtDescripcionBA.Value.ToString
        '    Dim tipo As String = DdlTipoBA.SelectedValue.ToString
        '    Dim numPart As String = TxtNumParteBA.Value.ToString
        '    Dim especifico As String = TxtCodEspecificoBA.Value.ToString
        '    Dim marca As String = LblCodMarcaBA.Text.ToString
        '    Dim modelo As String = LblCodModeloBA.Text.ToString

        '    If marca <> "" Then psListaMarca = ""
        '    If modelo <> "" Then psListaModelo = ""
        '    If codigo <> "" Then psListaArt = ""
        '    If tipo = "< Seleccionar >" Then tipo = ""

        '    dt = obj.Bus_Articulo(psconexion, codigo, clasificacion, descripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
        '    GvBuscarArticulos.DataSource = dt
        '    GvBuscarArticulos.DataBind()

        'Catch ex As SqlException
        '    LblError.Text = ex.Message
        'Catch ex As Exception
        '    LblError.Text = ex.Message
        'Finally
        'End Try
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

            dt = obj.Lista_ArticuloxBusqueda(psconexion, pdCodArt, psClasNumero, psDescripcion, tipo, numPart, especifico, marca, modelo, psListaArt, psListaMarca, psListaModelo)
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
            'If dtColum.Rows.Count > 1 Then
            '    LblCantArtReg.Text = "Hay " & dt.Rows.Count & " registros."
            'ElseIf dtColum.Rows.Count = 1 Then
            '    LblCantArtReg.Text = "Hay 1 registro."
            'ElseIf dtColum.Rows.Count = 0 Then
            '    LblCantArtReg.Text = "No hay registro."
            'End If

        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try


    End Sub
    Protected Sub GvBuscarArticulos_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GvBuscarArticulos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            TxtBusArtNombre.Text = ""
            TxtBusArtCodigo.Text = ""
            TxtBusArtCodigo.Text = GvBuscarArticulos.Rows(Index).Cells(1).Text
            TxtBusArtNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), " &gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Limpiar_Cajas_Buscar_Articulos()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('hide');", True)
        End If
    End Sub
    Protected Sub chkBusArticulo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBusArticulo.CheckedChanged
        If ChkBusArticulo.Checked = True Then
            TxtBusArtCodigo.Enabled = True
            TxtBusArtNombre.Enabled = True
            TxtBusArtCodigo.Text = ""
            TxtBusArtNombre.Text = ""
            BtnBusArt.Enabled = True
        Else
            TxtBusArtCodigo.Enabled = False
            TxtBusArtNombre.Enabled = False
            BtnBusArt.Enabled = False
            TxtBusArtCodigo.Text = ""
            TxtBusArtNombre.Text = ""
        End If
    End Sub
    Protected Sub chkBusProveedor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBusProveedor.CheckedChanged
        If ChkBusProveedor.Checked = True Then
            txtBusProvCodigo.Enabled = True
            TxtBusProvNombre.Enabled = True
            txtBusProvCodigo.Text = ""
            TxtBusProvNombre.Text = ""
            TxtBusProvRuc.Text = ""
            BtnBusProv.Enabled = True
        Else
            txtBusProvCodigo.Enabled = False
            TxtBusProvNombre.Enabled = False
            txtBusProvCodigo.Text = ""
            TxtBusProvNombre.Text = ""
            TxtBusProvRuc.Text = ""
            BtnBusProv.Enabled = False
        End If
    End Sub
    Protected Sub chkBusMotivo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBusMotivo.CheckedChanged
        If ChkBusMotivo.Checked = True Then
            CboBusMotivo.Enabled = True
            CboBusMotivo.SelectedValue = "<Seleccionar>"
        Else
            CboBusMotivo.Enabled = False
            CboBusMotivo.SelectedValue = "<Seleccionar>"
        End If
    End Sub
    Protected Sub ChkClasificacion_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkClasificacion.CheckedChanged
        If ChkClasificacion.Checked = True Then
            BtnBuscarClas.Enabled = True
            TxtClasificacion.Text = ""
            lblCodClas.Text = ""
        Else
            BtnBuscarClas.Enabled = False
            TxtClasificacion.Text = ""
            lblCodClas.Text = ""
        End If
    End Sub
    Protected Sub chkBusFecha_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkBusFecha.CheckedChanged
        If ChkBusFecha.Checked = True Then
            txtBusFecIni.Enabled = True
            txtBusFecFin.Enabled = True
            txtBusFecIni.Text = FormatoFecha(FechaActual)
            txtBusFecFin.Text = FormatoFecha(FechaActual)
        Else
            txtBusFecIni.Text = FormatoFecha(FechaActual)
            txtBusFecFin.Text = FormatoFecha(FechaActual)
            txtBusFecIni.Enabled = False
            txtBusFecFin.Enabled = False
        End If
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnNuevo.Click
        Response.Redirect("Inventario_Recepcion_Registrar.aspx")
    End Sub


    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psCodRecep As Double = 0
        Dim dt As DataTable
        dt = Nothing
        If e.CommandName = "Ver" Then
            GvDetalleItem.DataSource = dt
            GvDetalleItem.DataBind()
            LblRegistroDetItem.Text = ""
            psCodRecep = Flex.Rows(Index).Cells(3).Text
            LblTituloModal.Text = "Recepción Nro. " & psCodRecep
            dt = obj.Lista_Recepcion_Detalle(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
            FlexDet.DataSource = dt
            FlexDet.DataBind()
            lblRegDet.Text = dt.Rows.Count & " registros"
            Session("CodRecep") = psCodRecep
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
            'Response.Redirect("../Inventario/InvReport_Recepcion.aspx")
        ElseIf e.CommandName = "Anular" Then
            psCodRecep = Flex.Rows(Index).Cells(3).Text
            Session("CodRecep") = psCodRecep
            LblTituloModal.Text = "Recepción de Almacén Nro. " & Llenar_Ceros(psCodRecep, 6)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModalAnular').modal('show');", True)
        ElseIf e.CommandName = "Imagen" Then
            Index = Convert.ToInt32(e.CommandArgument)
            TxtNombreImagen.Text = ""
            TxtNombreImagen.Visible = True
            Ocultar_Visible_Imagen(True)
            div_imagen.Visible = True
            div_OC.Visible = True
            imagenCarga.Visible = False
            imageCargaGuia.Visible = False
            TxtNombreImagen.Visible = True
            lblNombreimg.Visible = True
            psCodRecep = Flex.Rows(Index).Cells(3).Text.Trim
            Dim nombreImg As String = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(24).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            BtnGuargarImg.Text = "Guardar Imagen"
            Ocultar_Visible_Imagen(True)
            lblCodigoRecep.Text = psCodRecep
            txtCodRecepcion.Text = psCodRecep
            TxtImagenNroOC.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(8).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            TxtNroGuia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(7).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            lblNombreimg.Text = "Nombre de la imagen"
            TxtNombreImagenGuia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(25).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            If psCodRecep > 0 Then
                Dim connectionString As String = Session("Ruta_Emp")
                TxtNombreImagen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(24).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                TxtNombreImagenGuia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Flex.Rows(Index).Cells(25).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                If TxtNombreImagen.Text <> "" Then
                    ComprimirImagenEnBaseDeDatosOC(psCodRecep)
                End If
                If TxtNombreImagenGuia.Text <> "" Then
                    ComprimirImagenEnBaseDeDatos(psCodRecep)
                End If
                'TxtNombreImagenGuia
                Dim query As String = "SELECT RECEP_CODIGO, RECEP_IMAGEN_OC_NOMBRE,RECEP_IMAGEN_GUIA_NOMBRE, RECEP_IMAGEN_OC AS ImagenOC,RECEP_IMAGEN_GUIA AS Imagen FROM TBINV_ALMACEN_RECEPCION WHERE RECEP_CODIGO = @RECEP_CODIGO"
                Using connection As New SqlConnection(connectionString)
                    Using cmd As New SqlCommand(query, connection)
                        cmd.Parameters.Add("@RECEP_CODIGO", SqlDbType.Float).Value = psCodRecep ' Ajusta el valor del ID según el registro que desees mostrar
                        connection.Open()

                        Using reader As SqlDataReader = cmd.ExecuteReader()
                            If reader.Read() Then
                                If Not IsDBNull(reader("ImagenOC")) Then
                                    TxtNombreImagen.Text = Nu(reader("RECEP_IMAGEN_OC_NOMBRE").ToString)
                                    Dim imageData As Byte() = DirectCast(reader("ImagenOC"), Byte())
                                    Dim base64String As String = Convert.ToBase64String(imageData)
                                    imagenCarga.ImageUrl = "data:image/jpeg;base64," + base64String
                                    imagenCarga.Visible = True
                                    Session("NuevaImagen") = "No"
                                Else
                                    TxtNombreImagen.Text = Nu(reader("RECEP_IMAGEN_OC_NOMBRE").ToString)
                                    Dim nombreImagen As String = Nu(reader("RECEP_IMAGEN_OC_NOMBRE").ToString)
                                    Dim rutaImagen As String = Server.MapPath("~/Inventario/GuardarImagen/" + nombreImagen)
                                End If
                                If Not IsDBNull(reader("Imagen")) Then
                                    TxtNombreImagenGuia.Text = Nu(reader("RECEP_IMAGEN_GUIA_NOMBRE").ToString)
                                    Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                    Dim base64String As String = Convert.ToBase64String(imageData)
                                    imageCargaGuia.ImageUrl = "data:image/jpeg;base64," + base64String
                                    imageCargaGuia.Visible = True
                                    Session("NuevaImagen") = "No"
                                Else
                                    TxtNombreImagenGuia.Text = Nu(reader("RECEP_IMAGEN_GUIA_NOMBRE").ToString)
                                    Dim nombreImagen As String = Nu(reader("RECEP_IMAGEN_GUIA_NOMBRE").ToString)
                                    Dim rutaImagen As String = Server.MapPath("~/Inventario/GuardarImagen/" + nombreImagen)
                                End If
                            End If
                        End Using
                    End Using
                End Using
            End If
        End If
    End Sub
    Protected Sub ComprimirImagenEnBaseDeDatos(ByVal pdCodArt As Double)
        ' Cadena de conexión a la base de datos
        Dim connectionString As String = Session("Ruta_Emp")

        ' Establece la consulta para recuperar la imagen
        Dim query As String = "SELECT RECEP_IMAGEN_GUIA FROM TBINV_ALMACEN_RECEPCION WHERE  RECEP_CODIGO =  " & pdCodArt

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Using command As New SqlCommand(query, connection)

                ' Lee la imagen de la base de datos
                Dim bytesImagenOriginal As Byte() = DirectCast(command.ExecuteScalar(), Byte())

                ' Guarda los bytes en un archivo temporal
                Dim rutaTemporal As String = Path.GetTempFileName()
                File.WriteAllBytes(rutaTemporal, bytesImagenOriginal)

                ' Comprime la imagen utilizando ImageResizer
                Dim settings As New ResizeSettings("maxwidth=600&maxheight=600&format=jpg")
                ImageBuilder.Current.Build(rutaTemporal, rutaTemporal, settings)

                ' Lee los bytes de la imagen comprimida
                Dim bytesImagenComprimida As Byte() = File.ReadAllBytes(rutaTemporal)

                ' Actualiza los bytes de la imagen comprimida en la base de datos
                Dim updateQuery As String = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_IMAGEN_GUIA = @Imagen WHERE RECEP_CODIGO = " & pdCodArt

                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@Imagen", bytesImagenComprimida)
                    updateCommand.ExecuteNonQuery()
                End Using


                ' Elimina el archivo temporal
                File.Delete(rutaTemporal)
            End Using
        End Using
    End Sub
    Protected Sub ComprimirImagenEnBaseDeDatosOC(ByVal pdCodArt As Double)
        ' Cadena de conexión a la base de datos
        Dim connectionString As String = Session("Ruta_Emp")

        ' Establece la consulta para recuperar la imagen
        Dim query As String = "SELECT RECEP_IMAGEN_OC FROM TBINV_ALMACEN_RECEPCION WHERE  RECEP_CODIGO =  " & pdCodArt

        Using connection As New SqlConnection(connectionString)
            connection.Open()

            Using command As New SqlCommand(query, connection)

                ' Lee la imagen de la base de datos
                Dim bytesImagenOriginal As Byte() = DirectCast(command.ExecuteScalar(), Byte())

                ' Guarda los bytes en un archivo temporal
                Dim rutaTemporal As String = Path.GetTempFileName()
                File.WriteAllBytes(rutaTemporal, bytesImagenOriginal)

                ' Comprime la imagen utilizando ImageResizer
                Dim settings As New ResizeSettings("maxwidth=600&maxheight=600&format=jpg")
                ImageBuilder.Current.Build(rutaTemporal, rutaTemporal, settings)

                ' Lee los bytes de la imagen comprimida
                Dim bytesImagenComprimida As Byte() = File.ReadAllBytes(rutaTemporal)

                ' Actualiza los bytes de la imagen comprimida en la base de datos
                Dim updateQuery As String = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_IMAGEN_OC = @Imagen WHERE RECEP_CODIGO = " & pdCodArt

                Using updateCommand As New SqlCommand(updateQuery, connection)
                    updateCommand.Parameters.AddWithValue("@Imagen", bytesImagenComprimida)
                    updateCommand.ExecuteNonQuery()
                End Using


                ' Elimina el archivo temporal
                File.Delete(rutaTemporal)
            End Using
        End Using
    End Sub


    Sub Ocultar_Visible_Imagen(ByVal vf As Boolean)
        txtCodRecepcion.Text = ""
        lblCodigo.Visible = vf
        lblImagenNroOC.Visible = vf
        TxtImagenNroOC.Visible = vf
        lblNombreimg.Visible = vf
        TxtNombreImagen.Visible = vf
        lblImagen.Visible = vf
        FileUpload1.Visible = vf
        BtnGuargarImg.Visible = vf
        BtnCancelar.Visible = vf
        BtnGuargarImgGuia.Visible = vf
        BtnCancelarGuia.Visible = vf
        BtnCerrarImg.Visible = vf
        LblNroGuia.Visible = vf
        TxtNroGuia.Visible = vf
        LblNombreImagenGuia.Visible = vf
        TxtNombreImagenGuia.Visible = vf
        lblImagen2.Visible = vf
        FileUpload2.Visible = vf
        div_imagen.Visible = vf
        div_OC.Visible = vf
    End Sub

    Sub Ayuda(sender As Object, e As FileUpload)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCarga').setAttribute('src', '');", True)
    End Sub

    Sub AyudaGuia(sender As Object, e As FileUpload)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "document.getElementById('imagenCargaGuia').setAttribute('src', '');", True)
    End Sub

    Protected Sub btnAnularCompleto_Click(sender As Object, e As EventArgs) Handles btnAnularCompleto.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        If Session("CodRecep") = 0 Then Exit Sub
        Dim psCodRecep As Double = 0
        psCodRecep = Session("CodRecep")
        Cn.Open() : CmdGlobal.Connection = Cn
        Dim psOpcion As Boolean = True
        Dim dt As New DataTable
        dt = obj.Lista_xCodRecepcion(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
        For Each dr As DataRow In dt.Rows
            If dr("RECEP_ESTADO") = "1" And psOpcion = True Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay un estado anterior')", True)
            Else
                Call AnularRecepcion(psCodRecep, Nu(dr("RECEP_ESTADO")), Nz(dr("RECEP_PROVEEDOR")), Nu(dr("RECEP_MOTIVO_GRAL")), "4")
                Call Actualizar_CantOC_Elimina(psCodRecep)
                Call Eliminar_Comprobante(psCodRecep)
                Call Eliminar_Guia(psCodRecep)
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se anuló la recepción Nro. " & Llenar_Ceros(psCodRecep, 8) & "')", True)
                btnListar_Click(sender, e)
            End If
        Next
    End Sub

    Protected Sub btnCambiarEstado_Click(sender As Object, e As EventArgs) Handles btnCambiarEstado.Click
        If Session("CodRecep") = "" Then Exit Sub
        Dim psCodRecep As Double = 0
        Dim psOpcion As Boolean = True
        Dim dt As New DataTable
        Try

            dt = obj.Lista_xCodRecepcion(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
            For Each dr As DataRow In dt.Rows
                If dr("RECEP_ESTADO") <> "1" Then
                    Call AnularRecepcion(psCodRecep, Nu(dr("RECEP_ESTADO")), Nu(dr("RECEP_PROVEEDOR")), Nu(dr("RECEP_MOTIVO_GRAL")), "1")
                    Call Actualizar_CantOC_Elimina(psCodRecep)
                    Call Eliminar_Comprobante(psCodRecep)
                    Call Eliminar_Guia(psCodRecep)
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Se anuló la recepción Nro. " & Llenar_Ceros(psCodRecep, 8) & "')", True)
                    btnListar_Click(sender, e)
                End If
            Next

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch Ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & Ex.Message & "')", True)
        End Try
    End Sub
    Private Sub AnularRecepcion(ByVal psCodRecepcion As Double, ByVal psEstado As String, ByVal psProveedor As Double, ByVal psMotivo As String, ByVal psEstadoAnulado As String)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim pdNroAnulacion As Double = 0
        Try


            Dim ValorSys As String = ""
            ValorSys = Session("User") & FechaActual() & HoraActual()
            If psEstado = "1" Then

                CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND RECEP_ESTADO ='1' AND RECEP_SYS_EST = '0' AND RECEP_CODIGO =" & psCodRecepcion
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        CmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_ESTADO ='4' WHERE EMPRESA_CODIGO ='" & Session("CodEmpresa") & "' AND RECEP_SYS_EST ='0' AND RECEP_CODIGO=" & psCodRecepcion
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()


                CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND RECEPD_ESTADO ='1' AND RECEPD_SYS_EST = '0' AND RECEP_CODIGO =" & psCodRecepcion
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_ESTADO ='4' WHERE EMPRESA_CODIGO ='" & Session("CodEmpresa") & "' AND RECEPD_SYS_EST ='0' AND RECEP_CODIGO=" & psCodRecepcion
                        CmdGlobal.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()

                CmdGlobal.CommandText = "SELECT * FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " WHERE SERIE_SYS_EST = '0' AND RECEP_CODIGO =" & psCodRecepcion
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "  SET SERIE_ESTADO ='1',SERIE_NRO = NULL WHERE SERIE_SYS_EST ='0' AND RECEP_CODIGO=" & psCodRecepcion
                        CmdGlobal.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()

                CmdGlobal.CommandText = "SELECT MAX(ANUL_NRO) FROM TBINV_ANULACIONES "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pdNroAnulacion = Nz(Rs(0)) + 1
                    End While
                Else
                    pdNroAnulacion = 1
                End If
                Rs.Close()

                CmdGlobal.CommandText = " INSERT INTO TBINV_ANULACIONES (EMPRESA_CODIGO, ANUL_NRO, ANUL_FECHA, ANUL_TIPO, ANUL_CODIGO,ANUL_ESTADO, ANUL_SYS_EST, ANUL_SYS_CRE, " _
                                      & " ANUL_TIPO_ORIGEN,  ANUL_TIPO_DESTINO, ANUL_COD_DESTINO, ANUL_MOTIVO)" _
                                      & " VALUES ('" & Session("CodEmpresa") & "','" & pdNroAnulacion & "','" & FechaActual() & "','2'," & psCodRecepcion & ",'" & psEstado & "','0','" & ValorSys & "', " _
                                      & " '3','1','" & cboBusAlmacen.SelectedValue & "','" & psMotivo & "')"
                CmdGlobal.ExecuteNonQuery()
                If psProveedor <> 0 Then
                    CmdGlobal.CommandText = " UPDATE TBINV_ANULACIONES SET ANUL_COD_ORIGEN = " & psProveedor & "  WHERE RECEP_CODIGO = '" & psCodRecepcion & "' "
                    CmdGlobal.ExecuteNonQuery()
                End If

            ElseIf psEstado = "2" Or psEstado = "3" Then
                Dim pdNroMovimiento As Double = 0
                Dim Series As Double = 0
                Dim StockAc As Double = 0
                Series = 0
                Dim dt As New DataTable
                Dim objLis As New clsInv_Listados
                Dim ofun As New clsInv_Procesos
                Dim pdCantBienesxRecep As Double = 0
                Dim pdCantMismoSitio As Double = 0
                dt = objLis.Lista_bienes_xCodRecepcion(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecepcion)
                pdCantBienesxRecep = dt.Rows.Count
                For Each dr As DataRow In dt.Rows
                    If Nu(dr("UBICACT_TIPO")) = "1" And Nz(dr("UBICACT_CODIGO")) = cboBusAlmacen.SelectedValue Then
                        pdCantMismoSitio = pdCantMismoSitio + 1
                    End If
                Next
                Series = pdCantMismoSitio
                If pdCantBienesxRecep <> pdCantMismoSitio Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('se encontrarón bienes que han sido movido del sitio de donde se recepcionó. No se puede anular la recepción')", True)
                Else

                    'EQUIPOS QUE INGRESAM POR PRIMERA VEZ
                    CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_ESTADO ='" & psEstadoAnulado & "' " _
                                          & " WHERE (EMPRESA_CODIGO ='" & Session("CodEmpresa") & "') AND (RECEP_SYS_EST ='0') AND (RECEP_CODIGO=" & psCodRecepcion & ") AND (RECEP_ESTADO IN ('2','3'))"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_ESTADO ='" & psEstadoAnulado & "'  " _
                                            & " WHERE (EMPRESA_CODIGO ='" & Session("CodEmpresa") & "') AND (RECEPD_SYS_EST ='0') AND (RECEP_CODIGO=" & psCodRecepcion & ")"
                    CmdGlobal.ExecuteNonQuery()

                    If Series = pdCantBienesxRecep Then
                        CmdGlobal.CommandText = " DELETE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "  " _
                                              & " WHERE SERIE_SYS_EST ='0' AND RECEP_CODIGO=" & psCodRecepcion
                        CmdGlobal.ExecuteNonQuery()
                        'EQUIPO
                        CmdGlobal.CommandText = "SELECT RECEPD_ITEM,ARTICULO_CODIGO,RECEPD_CANT_REC,RECEPD_COSTO_VENTA_S,RECEPD_COSTO_VENTA_D FROM TBINV_ALMACEN_RECEPCION_DET WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RECEP_CODIGO =" & psCodRecepcion & ") AND (RECEPD_SYS_EST = '0') AND (RECEPD_INGRESAR_SERIE = 'S') ORDER BY RECEPD_ITEM"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                If psEstadoAnulado = "1" Then
                                    CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_REC = 0 , RECEPD_CANT_XREC = " & Nz(Rs!RECEPD_CANT_REC) & ", RECEPD_CANT_FALT_REC= " & Nz(Rs!RECEPD_CANT_REC) & ",RECEPD_CANT_ING=0 " _
                                                                & " WHERE (EMPRESA_CODIGO ='" & Session("CodEmpresa") & "') AND (RECEPD_SYS_EST ='0') AND (RECEP_CODIGO=" & psCodRecepcion & ") AND (ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ")  and RECEPD_ITEM = " & Nu(Rs!RECEPD_ITEM) & ""
                                    CmdGlobal2.ExecuteNonQuery()
                                End If
                                CmdGlobal2.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = '" & cboBusAlmacen.SelectedValue & "') AND (UBICACT_TIPO='1')" _
                                        & " AND (ARTICULO_CODIGO = " & Nz(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                Rs2 = CmdGlobal2.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        StockAc = Nz(Rs2!SAA_STOCK_ACTUAL) - Nz(Rs!RECEPD_CANT_REC)
                                        CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = '" & cboBusAlmacen.SelectedValue & "') AND (UBICACT_TIPO='1')" _
                                                                    & " AND (ARTICULO_CODIGO = " & Nz(Rs2!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End While
                                End If
                                Rs2.Close()
                                'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                                CmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                Rs2 = CmdGlobal2.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        pdNroMovimiento = Nz(Rs2(0)) + 1
                                    End While
                                Else
                                    pdNroMovimiento = 1
                                End If
                                Rs2.Close()

                                If psEstadoAnulado = "4" Then
                                    'Call Movimiento_Kardex(Flex.TextMatrix(Flex.row, 1), "22", Nu(Rs2!ARTICULO_CODIGO), "1", cboBusAlmacen.SelectedValue, "1", cboAlmacen.ItemData(cboAlmacen.ListIndex), "", "2", FormatoFecha(FechaServer), CDbl(Rs2!RECEPD_CANT_REC), "S", CDbl(Nz(Rs2!RECEPD_COSTO_VENTA_S)), CDbl(Nz(Rs2!RECEPD_COSTO_VENTA_D)))
                                    ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecepcion, "8", Nu(Rs("ARTICULO_CODIGO")), "1", cboBusAlmacen.SelectedValue, "1", cboBusAlmacen.SelectedValue, "", "2", FormatoFecha(FechaActual), Nz(Rs("RECEPD_CANT_REC")))
                                Else
                                    CmdGlobal2.CommandText = " DELETE FROM TBINV_KARDEX_COSTO WHERE COSTO_TRANS_COD = " & psCodRecepcion & " AND COSTO_TIPO_MOV = '1' "
                                    CmdGlobal2.ExecuteNonQuery()
                                End If

                                CmdGlobal2.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO," _
                                                            & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                            & " values('" & Session("CodEmpresa") & "','" & pdNroMovimiento & "','2','1','" & cboBusAlmacen.SelectedValue & "','1','" & cboBusAlmacen.SelectedValue & "', " _
                                                            & " " & psCodRecepcion & ",'" & Nz(Rs!ARTICULO_CODIGO) & "','" & CDbl(Rs!RECEPD_CANT_REC) & "','" & ValorSys & "','4','22','" & FechaActual() & "','0')"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        End If
                        Rs.Close()
                    End If
                    Dim lblCodDespacho As Double = 0
                    Dim psCodArt As Double = 0
                    CmdGlobal2.CommandText = " SELECT * FROM TBINV_RECEPCION_DETALLE_SERIES WHERE RECEP_CODIGO = " & psCodRecepcion
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            'SERIE_ORIG_TIPO, SERIE_ORIG_CODIGO
                            If Nu(Rs2("SERIE_ORIG_TIPO")) <> "" And Nz(Rs2("SERIE_ORIG_CODIGO")) <> 0 Then
                                CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        lblCodDespacho = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    lblCodDespacho = 1
                                End If
                                Rs.Close()
                                CmdGlobal.CommandText = "SELECT articulo_codigo FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " WHERE SERIE_NUMERAR=" & Nu(Rs2!Serie_Numerar)
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psCodArt = Nz(Rs(0)) + 1
                                    End While
                                End If
                                Rs.Close()
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                                           & " CECOSE_CODIGO_DESTINO,DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                                           & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                                           & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "','" & Nu(Rs2!SERIE_ORIG_TIPO) & "'," _
                                                           & " " & Nu(Rs2!SERIE_ORIG_CODIGO) & ",'2','0',1,1,0,1," & cboBusAlmacen.SelectedIndex & "," _
                                                           & " '" & FechaActual() & "','" & HoraActual() & "','8','" & ValorSys & "')"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                                          & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & Nu(Rs2!Serie_Numerar) & ",'S','0',NULL,'8','N')"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & Nu(Rs2!Serie_Numerar)
                                CmdGlobal.ExecuteNonQuery()
                                'STOCK
                                StockAc = 0
                                '--------------------------recepcion en ccosto O ALMACEN
                                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & Nu(Rs2!Serie_Numerar)
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='3',DESP_CANT_REC='1',DESP_CANT_FALT_REC='0' WHERE DESP_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                CmdGlobal.ExecuteNonQuery()
                                'STOCK
                                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & Nu(Rs2!SERIE_ORIG_CODIGO) & ") AND (UBICACT_TIPO='" & Nu(Rs2!SERIE_ORIG_TIPO) & "') " _
                                                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                                        CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(Rs2!SERIE_ORIG_CODIGO) & ") AND (UBICACT_TIPO='" & Nu(Rs2!SERIE_ORIG_TIPO) & "') " _
                                                                         & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End While
                                Else
                                    CmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                                         & "VALUES(" & Nu(Rs2!SERIE_ORIG_CODIGO) & ",'" & Nu(Rs2!SERIE_ORIG_TIPO) & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                Rs.Close()

                                'MOVIMIENTO GENERAL
                                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        pdNroMovimiento = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    pdNroMovimiento = 1
                                End If
                                Rs.Close()
                                'Call Movimiento_Kardex(lblCodDespacho, "8", psCodArt, Nu(Rs2!SERIE_ORIG_TIPO), Nu(Rs2!SERIE_ORIG_CODIGO), "1", cboAlmacen.ItemData(cboAlmacen.ListIndex), "Por traslado", "1", FormatoFecha(FechaServer), 1)
                                ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, "8", psCodArt, Nu(Rs2!SERIE_ORIG_TIPO), Nu(Rs2!SERIE_ORIG_CODIGO), "1", cboBusAlmacen.SelectedValue, "", "1", FormatoFecha(FechaActual), 1)

                                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                           & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                           & " VALUES ('" & Session("CodEmpresa") & "'," & pdNroMovimiento & ",'1','" & Nu(Rs2!SERIE_ORIG_TIPO) & "'," & Nz(Rs2!SERIE_ORIG_CODIGO) & ", " _
                                                           & " " & psCodArt & ",'1','" & ValorSys & "','3','8','" & FechaActual() & "','0'," & lblCodDespacho & ",'1'," & cboBusAlmacen.SelectedValue & ")"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & Nu(Rs2!SERIE_ORIG_TIPO) & "',UBICACT_CODIGO=" & Nu(Rs2!SERIE_ORIG_CODIGO) & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & Nu(Rs2!Serie_Numerar)
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                                                  & " VALUES ('" & Nu(Rs2!Serie_Numerar) & "','" & Nu(Rs2!SERIE_ORIG_TIPO) & "'," & Nu(Rs2!SERIE_ORIG_CODIGO) & ",'20','0','" & ValorSys & "','" & FechaActual() & "','1','" & lblCodDespacho & "')"
                                CmdGlobal.ExecuteNonQuery()
                            End If
                        End While
                    End If
                    Rs2.Close()
                    '============================================================================
                    'ACCESORIO
                    CmdGlobal2.CommandText = "SELECT RECEPD_ITEM,ARTICULO_CODIGO,RECEPD_CANT_REC,RECEPD_COSTO_VENTA_S,RECEPD_COSTO_VENTA_D FROM TBINV_ALMACEN_RECEPCION_DET WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RECEP_CODIGO =" & psCodRecepcion & ") AND (RECEPD_SYS_EST = '0') AND (RECEPD_INGRESAR_SERIE ='N') ORDER BY RECEPD_ITEM"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_ESTADO ='" & psEstadoAnulado & "' " _
                                                      & " WHERE (EMPRESA_CODIGO ='" & Session("CodEmpresa") & "') AND (RECEP_SYS_EST ='0') AND (RECEP_CODIGO='" & psCodRecepcion & "') AND (RECEP_ESTADO IN ('2','3'))"
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_ESTADO ='" & psEstadoAnulado & "'  " _
                                                      & " WHERE (EMPRESA_CODIGO ='" & Session("CodEmpresa") & "') AND (RECEPD_SYS_EST ='0') AND (RECEP_CODIGO='" & psCodRecepcion & "')"
                            CmdGlobal.ExecuteNonQuery()
                            If psEstadoAnulado = "1" Then
                                CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_REC = 0 , RECEPD_CANT_XREC = " & Nz(Rs2!RECEPD_CANT_REC) & ", RECEPD_CANT_FALT_REC= " & Nz(Rs2!RECEPD_CANT_REC) & ",RECEPD_CANT_ING=0 " _
                                                          & " WHERE (EMPRESA_CODIGO ='" & Session("CodEmpresa") & "') AND (RECEPD_SYS_EST ='0') AND (RECEP_CODIGO='" & psCodRecepcion & "') AND (ARTICULO_CODIGO = " & Nz(Rs2!ARTICULO_CODIGO) & ") and RECEPD_ITEM = " & Nu(Rs2!RECEPD_ITEM) & ""
                                CmdGlobal.ExecuteNonQuery()
                            End If
                            CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = '" & cboBusAlmacen.SelectedValue & "') AND (UBICACT_TIPO='1')" _
                                    & " AND (ARTICULO_CODIGO = " & Nz(Rs2!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - Nz(Rs2!RECEPD_CANT_REC)
                                    If StockAc < 0 Then StockAc = 0
                                    CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = '" & cboBusAlmacen.SelectedValue & "') AND (UBICACT_TIPO='1')" _
                                                              & " AND (ARTICULO_CODIGO = " & Nz(Rs2!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                    CmdGlobal3.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()
                            'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    pdNroMovimiento = Nz(Rs(0)) + 1
                                End While
                            Else
                                pdNroMovimiento = 1
                            End If
                            Rs.Close()

                            If psEstadoAnulado = "4" Then
                                'Call Movimiento_Kardex(Flex.TextMatrix(Flex.row, 1), "22", Nu(Rs2!ARTICULO_CODIGO), "1", cboAlmacen.ItemData(cboAlmacen.ListIndex), "1", cboAlmacen.ItemData(cboAlmacen.ListIndex), "", "2", FormatoFecha(FechaServer), CDbl(Rs2!RECEPD_CANT_REC), "S", CDbl(Nz(Rs2!RECEPD_COSTO_VENTA_S)), CDbl(Nz(Rs2!RECEPD_COSTO_VENTA_D)))
                                ofun.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecepcion, "8", Nu(Rs2("ARTICULO_CODIGO")), "1", cboBusAlmacen.SelectedValue, "1", cboBusAlmacen.SelectedValue, "", "2", FormatoFecha(FechaActual), 1)
                            Else
                                CmdGlobal.CommandText = " DELETE FROM TBINV_KARDEX_COSTO WHERE COSTO_TRANS_COD = " & psCodRecepcion & " AND COSTO_TIPO_MOV = '1' "
                                CmdGlobal.ExecuteNonQuery()
                            End If
                            CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO," _
                                                      & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                      & " values('" & Session("CodEmpresa") & "','" & pdNroMovimiento & "','2','1','" & cboBusAlmacen.SelectedValue & "','1','" & cboBusAlmacen.SelectedValue & "', " _
                                                      & " '" & psCodRecepcion & "','" & Nz(Rs2!ARTICULO_CODIGO) & "','" & CDbl(Rs2!RECEPD_CANT_REC) & "','" & ValorSys & "','4','22','" & FechaActual() & "','0')"
                            CmdGlobal.ExecuteNonQuery()
                        End While
                    End If
                    Rs2.Close()

                    If psEstadoAnulado = "4" Then
                        CmdGlobal.CommandText = "SELECT MAX(ANUL_NRO) FROM TBINV_ANULACIONES "
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                pdNroAnulacion = Nz(Rs(0)) + 1
                            End While
                        Else
                            pdNroAnulacion = 1
                        End If
                        Rs.Close()
                        CmdGlobal.CommandText = " INSERT INTO TBINV_ANULACIONES (EMPRESA_CODIGO, ANUL_NRO,  ANUL_FECHA, ANUL_TIPO, ANUL_CODIGO, ANUL_ESTADO, ANUL_SYS_EST, ANUL_SYS_CRE, " _
                                                  & " ANUL_TIPO_ORIGEN, ANUL_COD_ORIGEN, ANUL_TIPO_DESTINO, ANUL_COD_DESTINO, ANUL_MOTIVO)" _
                                                  & " VALUES ('" & Session("CodEmpresa") & "','" & pdNroAnulacion & "','" & FechaActual() & "','2','" & psCodRecepcion & "','" & psEstado & "','0','" & ValorSys & "', " _
                                                  & " '3','" & psProveedor & "','1','" & cboBusAlmacen.SelectedValue & "','" & psMotivo & "')"
                        CmdGlobal.ExecuteNonQuery()
                    End If
                    Session("CodRecep") = 0
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Private Sub Actualizar_CantOC_Elimina(ByVal psCodRecep As Double)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn

        Try
        Catch ex As SqlException
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Eliminar_Comprobante(ByVal psCodRecep As Double)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn

        Try
        Catch ex As SqlException
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Eliminar_Guia(ByVal psCodRecep As Double)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn

        Try

        Catch ex As SqlException

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('hide');", True)
    End Sub

    Private Sub BtnBuscarClas_Click(sender As Object, e As EventArgs) Handles BtnBuscarClas.Click
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

    Protected Sub ChkOC_CheckedChanged(sender As Object, e As EventArgs) Handles ChkOC.CheckedChanged
        If ChkOC.Checked = True Then
            TxtNroOC.Text = ""
            TxtNroOC.Enabled = True
        Else
            TxtNroOC.Text = ""
            TxtNroOC.Enabled = False
        End If
    End Sub

    Private Sub Flex_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Flex.Sorting
        Dim dataTable As DataTable ' Supongamos que tienes un DataTable como fuente de datos

        LblError.Text = ""
        lblReg.Text = ""
        lblRegDet.Text = ""
        If cboBusAlmacen.SelectedValue = "< Seleccionar >" Then LblError.Text = "<br> - Falta seleccionar el Almacén."
        If ChkBusMotivo.Checked = True And CboBusMotivo.SelectedValue = "< Seleccionar >" Then LblError.Text = LblError.Text & "<br> - Falta seleccionar el Motivo de Ingreso."
        If ChkBusProveedor.Checked = True Then
            If txtBusProvCodigo.Text = "" Then LblError.Text = LblError.Text & "<br> - Falta seleccionar proveedor de busqueda."
        End If
        If LblError.Text <> "" Then
            LblError.Text = "Se ha encontrado las sgtes. observaciones: <br>" & LblError.Text
            Exit Sub
        End If
        Dim CampoClasif As String = ""
        Dim Clasificacion As String = ""
        Dim Sql As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psFechaIni As String = ""
        Dim psFechaFin As String = ""
        Dim psCodMotivo As String = ""


        Dim CodigoRecep As String = ""
        Clasificacion = ""
        Cn.Open() : Cn2.Open() : Cn3.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal2.Connection = Cn2
        CmdGlobal.Connection = Cn3
        Dim pslblCodNivel As String = ""
        'buscar art por clasificacion

        If lblCodClas.Text <> "" Then
            Sql = " SELECT CLAS_COD_NIVEL, CLAS_CODIGO, CLAS_NIVEL2 " _
                    & " From dbo.TBINV_ARTICULO_CLASIFICACION WHERE (CLAS_SYS_EST = '0') AND CLAS_CODIGO = " & lblCodClas.Text
            CmdGlobal.CommandText = Sql
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    pslblCodNivel = Nu(Rs!CLAS_COD_NIVEL)
                End While
            End If
            Rs.Close()
        End If

        If ChkClasificacion.Checked = True And pslblCodNivel <> "" And lblCodClas.Text <> "" Then
            CampoClasif = "AND CLAS_NIVEL" & pslblCodNivel & "  ='" & lblCodClas.Text & "' "
            Sql = " SELECT CLAS_COD_NIVEL, CLAS_CODIGO, CLAS_NIVEL2 " _
                    & " From dbo.TBINV_ARTICULO_CLASIFICACION WHERE (CLAS_SYS_EST = '0')"
            Sql = Sql & CampoClasif
            CmdGlobal.CommandText = Sql
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If Clasificacion <> "" Then Clasificacion = Clasificacion & ","
                    Clasificacion = Clasificacion & Nu(Rs!CLAS_CODIGO)
                End While
            End If
            Rs.Close()
        End If

        If (ChkBusArticulo.Checked = True And TxtBusArtCodigo.Text <> "") Or (TxtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" And Clasificacion <> "") Then
            Sql = " SELECT R.RECEP_CODIGO, RD.ARTICULO_CODIGO " _
                    & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
                    & " dbo.TBINV_ALMACEN_RECEPCION_DET RD ON R.EMPRESA_CODIGO = RD.EMPRESA_CODIGO AND R.RECEP_CODIGO = RD.RECEP_CODIGO INNER JOIN " _
                    & " TBINV_ARTICULOS A ON A.EMPRESA_CODIGO=RD.EMPRESA_CODIGO AND RD.ARTICULO_CODIGO=A.ART_CODIGO " _
                    & " WHERE (RD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (R.RECEP_SYS_EST = '0') " _
                    & " AND (R.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RD.RECEPD_SYS_EST = '0') AND (ART_SYS_EST='0')"
            If ChkClasificacion.Checked = True And TxtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" Then Sql = Sql & " AND A.ART_CLASIFICACION IN (" & Clasificacion & ")"
            If ChkBusArticulo.Checked = True And TxtBusArtCodigo.Text <> "" Then Sql = Sql & " AND rd.ARTICULO_CODIGO = " & TxtBusArtCodigo.Text.Trim & ""
            Sql = Sql & " ORDER BY r.RECEP_CODIGO ASC "
            CmdGlobal.CommandText = Sql
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If CodigoRecep <> "" Then CodigoRecep = CodigoRecep & ","
                    CodigoRecep = CodigoRecep & Nu(Rs!RECEP_CODIGO)
                End While
            End If
            Rs.Close()
        End If
        Dim n As String = cboBusEstado.SelectedValue.Trim
        Dim pdCodalmacen As Double = 0
        If cboBusAlmacen.SelectedValue <> "< Seleccionar >" Then
            pdCodalmacen = Nz(cboBusAlmacen.SelectedValue.Trim)
        End If
        Dim pdCodRecep As Double = 0
        Dim pdCodProv As Double = 0
        If txtBusProvCodigo.Text <> "" Then pdCodProv = txtBusProvCodigo.Text
        Dim psEstado As String = ""
        If n = "-1" Then
            psEstado = "2,3"
        ElseIf n = "-2" Or n = "< Seleccionar >" Then
            psEstado = "1,2,3,4"
        Else
            psEstado = n
        End If
        If ChkBusArticulo.Checked = True And CodigoRecep = "" Then Exit Sub
        If ChkBusFecha.Checked = True Then
            psFechaIni = Right(txtBusFecIni.Text, 4) + Mid(txtBusFecIni.Text, 4, 2) + Left(txtBusFecIni.Text, 2)
            psFechaFin = Right(txtBusFecFin.Text, 4) + Mid(txtBusFecFin.Text, 4, 2) + Left(txtBusFecFin.Text, 2)
        End If
        If CboBusMotivo.SelectedValue <> "< Seleccionar >" Then
            psCodMotivo = CboBusMotivo.SelectedValue
        End If
        Dim psNroOCompra As String = ""
        If ChkOC.Checked = True And TxtNroOC.Text.Trim <> "" Then
            psNroOCompra = TxtNroOC.Text.Trim
        End If
        Dim dt As DataTable
        dt = obj.Lista_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodalmacen, CodigoRecep, psEstado, pdCodProv, psFechaIni, psFechaFin, psCodMotivo, psNroOCompra)

        dataTable = obj.Lista_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodalmacen, CodigoRecep, psEstado, pdCodProv, psFechaIni, psFechaFin, psCodMotivo, psNroOCompra)

        ' Realiza la ordenación del DataTable en función de la columna seleccionada
        If ViewState("SortExpression") IsNot Nothing AndAlso ViewState("SortExpression").ToString() = e.SortExpression Then
            ' Si ya se está ordenando por la misma columna, cambia la dirección
            If ViewState("SortDirection") IsNot Nothing AndAlso ViewState("SortDirection").ToString() = "ASC" Then
                ViewState("SortDirection") = "DESC"
            Else
                ViewState("SortDirection") = "ASC"
            End If
        Else
            ' Si se está ordenando por una columna diferente, establece la dirección en ASC
            ViewState("SortExpression") = e.SortExpression
            ViewState("SortDirection") = "ASC"
        End If

        ' Aplica la dirección de ordenación y columna al DataView
        dataTable.DefaultView.Sort = ViewState("SortExpression") & " " & ViewState("SortDirection")

        '' Realiza la ordenación del DataTable en función de la columna seleccionada
        'dataTable.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression)

        ' Vuelve a enlazar los datos al GridView
        Flex.DataSource = dataTable
        Flex.DataBind()
    End Sub

    Private Sub BtnBusArt_Click(sender As Object, e As EventArgs) Handles BtnBusArt.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').modal('show');", True)
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

    Private Sub BtnCerrarClasificacion_Click(sender As Object, e As EventArgs) Handles BtnCerrarClasificacion.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub
    Private Sub btnModalBuscarClas_Click(sender As Object, e As EventArgs) Handles btnModalBuscarClas.Click
        PopularRootLevel()
    End Sub

    Private Sub BtnBuscaClasificacionBA_Click(sender As Object, e As EventArgs) Handles BtnBuscaClasificacionBA.Click
        TituloPopupp.Text = "Busca Clasificaciones"
        Dim obj As New Cls_Clasificacion
        Dim dt As New DataTable
        dt = obj.PopularRootLevel(Session("Ruta_Emp"))
        obj.NodosPopulares(dt, trvClasificacion.Nodes)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBuscaArticulos').one('hidden.bs.modal', function() { $('#ModalClasificacion').modal('show'); }).modal('hide');", True)

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
    Private Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        trvClasificacion.SelectedNode.Selected = True
        TxtClasificacionBA.Value = trvClasificacion.SelectedNode.Text
        Dim psNumero As Integer = 0
        lblCodClas.Text = trvClasificacion.SelectedValue
        psNumero = InStr(1, TxtClasificacionBA.Value, "-")
        LblCodClasificacionBA.Text = Left(TxtClasificacionBA.Value, psNumero - 2)
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').one('hidden.bs.modal', function() { $('#ModalBuscaArticulos').modal('show'); }).modal('hide');", True)
        trvClasificacion.Nodes.Clear()
    End Sub

    Private Sub btnCancela_Click(sender As Object, e As EventArgs) Handles btnCancela.Click
        If TituloPopup.Text = "Busca Modelos" Or TituloPopup.Text = "Busca Marcas" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalBusqueda').modal('show'); }).modal('hide');", True)
        End If
        Limpiar_Popup()
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

    Private Sub BtnBusProv_Click(sender As Object, e As EventArgs) Handles BtnBusProv.Click
        lblEtq_BusDestino.Text = "Busqueda de Proveedores"
        TxtBusProvNombre.Text = ""
        TxtBusProvRuc.Text = ""
        txtBusProvCodigo.Text = ""
        FlexTipoPers.DataSource = Nothing
        FlexTipoPers.DataBind()
        txtBusCod.Text = ""
        txtBusDescripcion.Text = ""
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('show');", True)
    End Sub
    Protected Sub btnUbiListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiListar.Click
        Try
            Dim psConexion As String = Session("Ruta_Emp")
            Dim obj As New clsInv_Listados
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            FlexTipoPers.DataSource = obj.Lista_Proveedor(psConexion, Session("CodEmpresa"), txtBusCod.Text.Trim, txtBusDescripcion.Text.Trim)
            FlexTipoPers.DataBind()
        Catch ex As SqlException
            LblError.Text = ex.Message
        Catch ex As Exception
            LblError.Text = ex.Message
        Finally
        End Try
    End Sub
    Protected Sub FlexTipoPers_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles FlexTipoPers.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Aceptar" Then
            TxtBusProvRuc.Text = ""
            TxtBusProvNombre.Text = ""
            txtBusProvCodigo.Text = ""
            Session("DestinoCodExt") = FlexTipoPers.Rows(Index).Cells(1).Text
            Session("DestinoDescrip") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(FlexTipoPers.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´")
            Session("DestinoCodigo") = FlexTipoPers.Rows(Index).Cells(3).Text
            TxtBusProvNombre.Text = Session("DestinoDescrip")
            TxtBusProvRuc.Text = Session("DestinoCodExt")
            txtBusProvCodigo.Text = Session("DestinoCodigo")
            FlexTipoPers.DataSource = Nothing
            FlexTipoPers.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
        End If
    End Sub
    Protected Sub btnUbiCerrar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUbiCerrar.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalBusqueda').modal('hide');", True)
    End Sub

    Private Sub BtnGuargarImg_Click(sender As Object, e As EventArgs) Handles BtnGuargarImg.Click
        Try
            Dim obj As New clsInv_InsUpdDel

            If FileUpload1.HasFile Then

                Dim rutaOriginal As String = Server.MapPath("~/Inventario/ArchivoTemp/original.jpg")
                Dim rutaComprimida As String = Server.MapPath("~/Inventario/ArchivoTemp/comprimida.jpg")
                FileUpload1.SaveAs(rutaOriginal)
                ComprimirImagen(rutaOriginal, rutaComprimida)
                Dim bytesImagen As Byte() = File.ReadAllBytes(rutaComprimida)

                Dim filename As String = Path.GetFileName(FileUpload1.PostedFile.FileName)

                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim cmdSql As New SqlCommand
                'Dim Rs As SqlDataReader
                Dim pdCodImg As Double = 0
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = " update TBINV_ALMACEN_RECEPCION set RECEP_IMAGEN_OC_NOMBRE = '" & filename & "' where RECEP_CODIGO =  " & Nz(lblCodigoRecep.Text)
                cmdSql.ExecuteNonQuery()

                Dim psCodart As Double = 0
                If Nz(lblCodigoRecep.Text) > 0 Then
                    psCodart = lblCodigoRecep.Text
                End If

                Dim inputStream As System.IO.Stream = FileUpload1.PostedFile.InputStream
                Dim tamaño As Integer = FileUpload1.PostedFile.ContentLength
                Dim imagenData(tamaño - 1) As Byte
                inputStream.Read(imagenData, 0, tamaño)
                obj.GuardarImagenRecep_OC(Session("Ruta_Emp"), psCodart, bytesImagen, filename)

            End If

            Using connection As New SqlConnection(Session("Ruta_Emp"))
                Using cmd As New SqlCommand("SELECT RECEP_CODIGO, RECEP_IMAGEN_OC_NOMBRE,RECEP_IMAGEN_GUIA_NOMBRE, RECEP_IMAGEN_OC AS Imagen , RECEP_IMAGEN_GUIA AS ImagenGuia FROM TBINV_ALMACEN_RECEPCION WHERE  RECEP_CODIGO = @RECEP_CODIGO", connection)
                    cmd.Parameters.Add("@RECEP_CODIGO", SqlDbType.Int).Value = Nz(lblCodigoRecep.Text) ' Ajusta el valor del ID según el registro que desees mostrar
                    connection.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("Imagen")) Then
                                TxtNombreImagen.Text = Nu(reader("RECEP_IMAGEN_OC_NOMBRE").ToString)
                                Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imagenCarga.ImageUrl = "data:image/jpeg;base64," + base64String
                                imagenCarga.Visible = True
                                div_imagen.Visible = True
                            Else
                                imagenCarga.Visible = False
                                div_imagen.Visible = False
                            End If
                            If Not IsDBNull(reader("Imagen")) Then
                                TxtNombreImagen.Text = Nu(reader("RECEP_IMAGEN_GUIA_NOMBRE").ToString)
                                Dim imageData As Byte() = DirectCast(reader("ImagenGuia"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imageCargaGuia.ImageUrl = "data:image/jpeg;base64," + base64String
                                imageCargaGuia.Visible = True
                                div_OC.Visible = True
                            Else
                                imageCargaGuia.Visible = False
                                div_OC.Visible = False
                            End If
                        End If
                    End Using
                End Using
            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub ComprimirImagen(rutaOriginal As String, rutaComprimida As String)
        Dim settings As New ResizeSettings("maxwidth=800&maxheight=600&format=jpg")
        ImageBuilder.Current.Build(rutaOriginal, rutaComprimida, settings)
    End Sub

    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Ocultar_Visible_Imagen(False)

    End Sub

    Private Sub BtnGuargarImgGuia_Click(sender As Object, e As EventArgs) Handles BtnGuargarImgGuia.Click
        Try
            Dim obj As New clsInv_InsUpdDel

            If FileUpload2.HasFile Then

                Dim rutaOriginal As String = Server.MapPath("~/Inventario/ArchivoTemp/original.jpg")
                Dim rutaComprimida As String = Server.MapPath("~/Inventario/ArchivoTemp/comprimida.jpg")
                FileUpload2.SaveAs(rutaOriginal)
                ComprimirImagen(rutaOriginal, rutaComprimida)
                Dim bytesImagen As Byte() = File.ReadAllBytes(rutaComprimida)

                Dim filename As String = Path.GetFileName(FileUpload2.PostedFile.FileName)

                Dim Cn As New SqlConnection(Session("Ruta_Emp"))
                Dim cmdSql As New SqlCommand
                'Dim Rs As SqlDataReader
                Dim pdCodImg As Double = 0
                Cn.Open()
                cmdSql.Connection = Cn
                cmdSql.CommandText = " update TBINV_ALMACEN_RECEPCION set RECEP_IMAGEN_GUIA_NOMBRE = '" & filename & "' where RECEP_CODIGO =  " & Nz(lblCodigoRecep.Text)
                cmdSql.ExecuteNonQuery()

                Dim psCodart As Double = 0
                If Nz(lblCodigoRecep.Text) > 0 Then
                    psCodart = lblCodigoRecep.Text
                End If

                Dim inputStream As System.IO.Stream = FileUpload2.PostedFile.InputStream
                Dim tamaño As Integer = FileUpload2.PostedFile.ContentLength
                Dim imagenData(tamaño - 1) As Byte
                inputStream.Read(imagenData, 0, tamaño)
                obj.GuardarImagenRecep_Guia(Session("Ruta_Emp"), psCodart, bytesImagen, filename)

            End If

            Using connection As New SqlConnection(Session("Ruta_Emp"))
                Using cmd As New SqlCommand("SELECT RECEP_CODIGO, RECEP_IMAGEN_OC_NOMBRE,RECEP_IMAGEN_GUIA_NOMBRE, RECEP_IMAGEN_OC AS Imagen , RECEP_IMAGEN_GUIA AS ImagenGuia FROM TBINV_ALMACEN_RECEPCION WHERE  RECEP_CODIGO = @RECEP_CODIGO", connection)
                    cmd.Parameters.Add("@RECEP_CODIGO", SqlDbType.Int).Value = Nz(lblCodigoRecep.Text) ' Ajusta el valor del ID según el registro que desees mostrar
                    connection.Open()
                    Using reader As SqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            If Not IsDBNull(reader("Imagen")) Then
                                TxtNombreImagen.Text = Nu(reader("RECEP_IMAGEN_OC_NOMBRE").ToString)
                                Dim imageData As Byte() = DirectCast(reader("Imagen"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imagenCarga.ImageUrl = "data:image/jpeg;base64," + base64String
                                imagenCarga.Visible = True
                                div_imagen.Visible = True
                            Else
                                imagenCarga.Visible = False
                                div_imagen.Visible = False
                            End If
                            If Not IsDBNull(reader("Imagen")) Then
                                TxtNombreImagen.Text = Nu(reader("RECEP_IMAGEN_GUIA_NOMBRE").ToString)
                                Dim imageData As Byte() = DirectCast(reader("ImagenGuia"), Byte())
                                Dim base64String As String = Convert.ToBase64String(imageData)
                                imageCargaGuia.ImageUrl = "data:image/jpeg;base64," + base64String
                                imageCargaGuia.Visible = True
                                div_OC.Visible = True
                            Else
                                imageCargaGuia.Visible = False
                                div_OC.Visible = False
                            End If
                        End If
                    End Using
                End Using
            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub

    Private Sub BtnCancelarGuia_Click(sender As Object, e As EventArgs) Handles BtnCancelarGuia.Click
        Ocultar_Visible_Imagen(False)
    End Sub

    Private Sub BtnCerrarImg_Click(sender As Object, e As EventArgs) Handles BtnCerrarImg.Click
        Ocultar_Visible_Imagen(False)
    End Sub

    Private Sub FlexDet_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles FlexDet.RowCommand
        'Lista_Recepcion_Detalle_xItem
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psCodRecep As Double = 0
        Dim psCodArticulo As Double = 0
        Dim dt As DataTable
        If e.CommandName = "Ver" Then
            psCodRecep = Nz(Session("CodRecep"))
            psCodArticulo = Nz(FlexDet.Rows(Index).Cells(2).Text)
            LblTituloModal.Text = "Recepción Nro. " & psCodRecep
            dt = obj.Lista_Recepcion_Detalle_xItem(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep, psCodArticulo)
            GvDetalleItem.DataSource = dt
            GvDetalleItem.DataBind()
            LblRegistroDetItem.Text = dt.Rows.Count & " registros"
        End If
    End Sub
End Class

