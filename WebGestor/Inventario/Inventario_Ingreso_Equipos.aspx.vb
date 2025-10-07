Imports WebGestor
Imports System.Data
Imports System.Data.SqlClient
Partial Class Inventario_Inventario_Ingreso_Equipos
    Inherits System.Web.UI.Page
    Dim obj As New clsInv_Listados
    Dim objCat As New Cls_Catalogo
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cboBusEstado.Items.Clear()
            Dim lst1 As New ListItem : lst1.Text = "Recepción Generada" : lst1.Value = 1 : cboBusEstado.Items.Add(lst1)
            Dim lst2 As New ListItem : lst2.Text = "Recepción Recibida Ok" : lst2.Value = 2 : cboBusEstado.Items.Add(lst2)
            Dim lst3 As New ListItem : lst3.Text = "Recepción Recibida No Ok" : lst3.Value = 3 : cboBusEstado.Items.Add(lst3)
            Dim lst4 As New ListItem : lst4.Text = "Recepciones Recibidas" : lst4.Value = -1 : cboBusEstado.Items.Add(lst4)
            Dim lst5 As New ListItem : lst5.Text = "Todas las Recepciones" : lst5.Value = -2 : cboBusEstado.Items.Add(lst5)
            Dim lst6 As New ListItem : lst6.Text = "Recepción Anulada" : lst6.Value = 4 : cboBusEstado.Items.Add(lst6)
            cboBusEstado.Items.Add("< Seleccionar >") : cboBusEstado.SelectedValue = "< Seleccionar >"
            obj.Llena_Almacen(Session("Ruta_Emp"), Session("CodEmpresa"), cboBusAlmacen, Session("User"))
            obj.Llena_Motivo_Ing(Session("Ruta_Emp"), Session("CodEmpresa"), CboBusMotivo)
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

    Protected Sub btnListar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListar.Click
        Try
            lblError.Text = ""
            lblReg.Text = ""
            lblRegDet.Text = ""
            If cboBusAlmacen.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Falta seleccionar el Almacén."
            If chkBusMotivo.Checked = True And cboBusMotivo.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Falta seleccionar el Motivo de Ingreso."
            If chkBusProveedor.Checked = True Then
                If txtBusProvCodigo.Text = "" Then lblError.Text = lblError.Text & "<br> - Falta seleccionar proveedor de busqueda."
            End If
            If lblError.Text <> "" Then
                lblError.Text = "Se ha encontrado las sgtes. observaciones: <br>" & lblError.Text
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

            If chkClasificacion.Checked = True And pslblCodNivel <> "" And lblCodClas.Text <> "" Then
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

            If (chkBusArticulo.Checked = True And txtBusArtCodigo.Text <> "") Or (txtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" And Clasificacion <> "") Then
                Sql = " SELECT R.RECEP_CODIGO, RD.ARTICULO_CODIGO " _
                    & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
                    & " dbo.TBINV_ALMACEN_RECEPCION_DET RD ON R.EMPRESA_CODIGO = RD.EMPRESA_CODIGO AND R.RECEP_CODIGO = RD.RECEP_CODIGO INNER JOIN " _
                    & " TBINV_ARTICULOS A ON A.EMPRESA_CODIGO=RD.EMPRESA_CODIGO AND RD.ARTICULO_CODIGO=A.ART_CODIGO " _
                    & " WHERE (RD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (R.RECEP_SYS_EST = '0') " _
                    & " AND (R.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RD.RECEPD_SYS_EST = '0') AND (ART_SYS_EST='0')"
                If chkClasificacion.Checked = True And txtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" Then Sql = Sql & " AND A.ART_CLASIFICACION IN (" & Clasificacion & ")"
                If chkBusArticulo.Checked = True And txtBusArtCodigo.Text <> "" Then Sql = Sql & " AND rd.ARTICULO_CODIGO = " & txtBusArtCodigo.Text.Trim & ""
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
            If chkBusArticulo.Checked = True And CodigoRecep = "" Then Exit Sub
            If chkBusFecha.Checked = True Then
                psFechaIni = Right(txtBusFecIni.Text, 4) + Mid(txtBusFecIni.Text, 4, 2) + Left(txtBusFecIni.Text, 2)
                psFechaFin = Right(txtBusFecFin.Text, 4) + Mid(txtBusFecFin.Text, 4, 2) + Left(txtBusFecFin.Text, 2)
            End If
            If cboBusMotivo.SelectedValue <> "< Seleccionar >" Then
                psCodMotivo = cboBusMotivo.SelectedValue
            End If
            Dim psNroOCompra As String = ""
            If ChkOC.Checked = True And txtNroOC.Text.Trim <> "" Then
                psNroOCompra = txtNroOC.Text.Trim
            End If
            Dim dt As DataTable
            dt = obj.Lista_Recepcion(Session("Ruta_Emp"), Session("CodEmpresa"), pdCodalmacen, CodigoRecep, psEstado, pdCodProv, psFechaIni, psFechaFin, psCodMotivo, psNroOCompra)
            Flex.DataSource = dt
            Flex.DataBind()
            lblReg.Text = dt.Rows.Count & " registros"
            FlexDet.DataSource = Nothing
            FlexDet.DataBind()
        Catch ex As SqlException
            lblError.Text = "Se ha producido un error en la base de datos: <br>" & ex.Message
        Catch ex As Exception
            lblError.Text = "Se ha producido un error en la aplicación: <br>" & ex.Message
        End Try
    End Sub
    Protected Sub BtnBuscarBA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBuscarBA.Click
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
            TxtBusArtNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBuscarArticulos.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), " &gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Limpiar_Cajas_Buscar_Articulos()
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
        Response.Redirect("Inventario_Ingreso_Equipos_Registrar.aspx")
    End Sub


    Private Sub Flex_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Flex.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim psCodRecep As Double = 0
        Dim dt As DataTable
        If e.CommandName = "Ver" Then
            psCodRecep = Flex.Rows(Index).Cells(2).Text
            LblTituloModal.Text = "Recepción Nro. " & psCodRecep
            dt = obj.Lista_Recepcion_Item(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep, "")
            FlexDet.DataSource = dt
            FlexDet.DataBind()
            lblRegDet.Text = dt.Rows.Count & " registros"
            Session("CodRecep") = psCodRecep
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalDetalle').modal('show');", True)
            'Response.Redirect("../Inventario/InvReport_Recepcion.aspx")
        ElseIf e.CommandName = "Anular" Then
            psCodRecep = Flex.Rows(Index).Cells(2).Text
            Session("CodRecep") = psCodRecep
            LblTituloModal.Text = "Recepción de Almacén Nro. " & Llenar_Ceros(psCodRecep, 6)
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModalAnular').modal('show');", True)
        End If
    End Sub

    Protected Sub btnAnularCompleto_Click(sender As Object, e As EventArgs) Handles btnAnularCompleto.Click
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        If Session("CodRecep") = "" Then Exit Sub
        Dim psCodRecep As Double = 0
        psCodRecep = Session("CodRecep")
        Cn.Open() : CmdGlobal.Connection = Cn
        Dim psOpcion As Boolean = True
        Dim dt As New DataTable
        dt = obj.Lista_xCodRecepcion(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecep)
        For Each dr As DataRow In dt.Rows
            If dr("RECEP_ESTDAO") = "1" And psOpcion = True Then
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay un estado anterior')", True)
            Else
                Call AnularRecepcion(psCodRecep)
                Call Actualizar_CantOC_Elimina(psCodRecep)
                Call Eliminar_Comprobante(psCodRecep)
                Call Eliminar_Guia(psCodRecep)
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
                If dr("RECEP_ESTDAO") <> "1" Then
                    Call AnularRecepcion(psCodRecep)
                    Call Actualizar_CantOC_Elimina(psCodRecep)
                    Call Eliminar_Comprobante(psCodRecep)
                    Call Eliminar_Guia(psCodRecep)
                    btnListar_Click(sender, e)
                End If
            Next

        Catch ex As sqlException
        Catch ex As Exception

        End Try
    End Sub
    Private Sub AnularRecepcion(ByVal psCodRecep As Double)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Try
        Catch ex As SqlException
        Catch ex As Exception

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

        lblError.Text = ""
        lblReg.Text = ""
        lblRegDet.Text = ""
        If cboBusAlmacen.SelectedValue = "< Seleccionar >" Then lblError.Text = "<br> - Falta seleccionar el Almacén."
        If chkBusMotivo.Checked = True And cboBusMotivo.SelectedValue = "< Seleccionar >" Then lblError.Text = lblError.Text & "<br> - Falta seleccionar el Motivo de Ingreso."
        If chkBusProveedor.Checked = True Then
            If txtBusProvCodigo.Text = "" Then lblError.Text = lblError.Text & "<br> - Falta seleccionar proveedor de busqueda."
        End If
        If lblError.Text <> "" Then
            lblError.Text = "Se ha encontrado las sgtes. observaciones: <br>" & lblError.Text
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

        If chkClasificacion.Checked = True And pslblCodNivel <> "" And lblCodClas.Text <> "" Then
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

        If (chkBusArticulo.Checked = True And txtBusArtCodigo.Text <> "") Or (txtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" And Clasificacion <> "") Then
            Sql = " SELECT R.RECEP_CODIGO, RD.ARTICULO_CODIGO " _
                    & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
                    & " dbo.TBINV_ALMACEN_RECEPCION_DET RD ON R.EMPRESA_CODIGO = RD.EMPRESA_CODIGO AND R.RECEP_CODIGO = RD.RECEP_CODIGO INNER JOIN " _
                    & " TBINV_ARTICULOS A ON A.EMPRESA_CODIGO=RD.EMPRESA_CODIGO AND RD.ARTICULO_CODIGO=A.ART_CODIGO " _
                    & " WHERE (RD.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (R.RECEP_SYS_EST = '0') " _
                    & " AND (R.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (RD.RECEPD_SYS_EST = '0') AND (ART_SYS_EST='0')"
            If chkClasificacion.Checked = True And txtClasificacion.Text <> "" And pslblCodNivel <> "" And lblCodClas.Text <> "" Then Sql = Sql & " AND A.ART_CLASIFICACION IN (" & Clasificacion & ")"
            If chkBusArticulo.Checked = True And txtBusArtCodigo.Text <> "" Then Sql = Sql & " AND rd.ARTICULO_CODIGO = " & txtBusArtCodigo.Text.Trim & ""
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
        If chkBusArticulo.Checked = True And CodigoRecep = "" Then Exit Sub
        If chkBusFecha.Checked = True Then
            psFechaIni = Right(txtBusFecIni.Text, 4) + Mid(txtBusFecIni.Text, 4, 2) + Left(txtBusFecIni.Text, 2)
            psFechaFin = Right(txtBusFecFin.Text, 4) + Mid(txtBusFecFin.Text, 4, 2) + Left(txtBusFecFin.Text, 2)
        End If
        If cboBusMotivo.SelectedValue <> "< Seleccionar >" Then
            psCodMotivo = cboBusMotivo.SelectedValue
        End If
        Dim psNroOCompra As String = ""
        If ChkOC.Checked = True And txtNroOC.Text.Trim <> "" Then
            psNroOCompra = txtNroOC.Text.Trim
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

    Protected Sub trvClasificacion_SelectedNodeChanged(sender As Object, e As EventArgs) Handles trvClasificacion.SelectedNodeChanged
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        trvClasificacion.SelectedNode.Selected = True


        txtClasificacion.Text = trvClasificacion.SelectedNode.Text
        lblCodClas.Text = trvClasificacion.SelectedValue

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalClasificacion').modal('hide');", True)

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

End Class
