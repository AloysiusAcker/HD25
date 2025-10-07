Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports System.Reflection
Imports WebGestor

Partial Class Inventario_Inventario_Regularizar_Equipos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combos()
        End If
    End Sub
    Protected Sub Listar_Resumen_Inventario_xUbicacion()
        LblRegistro.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim dtO As New DataTable
        Dim tipo As String = ""
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim ubicacion As String = LblUbicaCodigo.Text.ToString
        Dim psUbicaCodigo As Double = 0
        psUbicaCodigo = Nz(LblUbicaCodigo.Text.ToString)
        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBCentroC.Checked Then
            tipo = "2"
        End If
        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")

        dt = obj.Resumen_Invenatrio_xUbicacion(Session("Ruta_Emp"), Session("CodEmpresa"), tipo, psUbicaCodigo, psCodInv)
        gvResumen.DataSource = dt
        gvResumen.DataBind()

        If dt.Rows.Count > 1 Then
            LblRegistro.Text = "Hay " & dt.Rows.Count & " registros."
        ElseIf dt.Rows.Count = 1 Then
            LblRegistro.Text = "Hay 1 registro."
        End If
        dt = Nothing

    End Sub
    Protected Sub Llenar_Combos()
        Dim obj As New Cls_Inventario_Verificacion
        Dim objC As New Cls_Catalogo
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        dt = obj.Llenar_Combo_Inventario(psconexion)
        DdlInventario.DataSource = dt
        DdlInventario.DataValueField = "INVENT_CODIGO"
        DdlInventario.DataTextField = "INVENT_DESC"
        DdlInventario.DataBind()

    End Sub
    Protected Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
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
        Dim psconexion As String = Session("Ruta_Emp")
        Dim inventario As String = DdlInventario.SelectedValue.ToString
        Dim codigo As String = BuscarCodigo.Value.ToString
        Dim descripcion As String = BuscarDescripcion.Value.ToString
        Dim Codigoalm As Double = Nz(BuscarCodigo.Value.ToString)


        If TituloPopup.Text = "Búsqueda Almacén" Then
            dt = obj.Listar_Almacenes_Inventario_Verificacion(psconexion, inventario, Codigoalm, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
            dt = obj.Listar_CentroC_Inventario_Verificacion(psconexion, inventario, codigo, descripcion)
        End If

        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub
    Private Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            LblUbicaCodigo.Text = GvBusqueda.Rows(Index).Cells(3).Text
            LblUbicaCodigoInv.Text = GvBusqueda.Rows(Index).Cells(4).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If

        Limpiar_Cajas_Popup()
    End Sub
    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

        Limpiar_Cajas_Popup()
    End Sub
    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged

        LblUbicaCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblRegistro.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Listar_Resumen_Inventario_xUbicacion()
        BtnRegularizar.Visible = False
        lblCodEstado.Text = ""
        Dim dt As New DataTable
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        LblRegistro2.Text = ""
    End Sub

    Private Sub gvResumen_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvResumen.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblRegistro2.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psUbicaCodigo As Double = 0
        Dim psTipoUbica As String = ""
        If RBAlmacen.Checked = True Then psTipoUbica = "1"
        If RBCentroC.Checked = True Then psTipoUbica = "2"
        psUbicaCodigo = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim psEstado As String = ""
        Dim pdSerieNumerar As Double = 0
        If e.CommandName = "Detalle" Then
            BtnRegularizar.Visible = False
            psEstado = gvResumen.Rows(Index).Cells(3).Text
            If psEstado = "3" Then BtnRegularizar.Visible = True
            If psEstado = "6" Then BtnRegularizar.Visible = True
            If psEstado = "8" Then BtnRegularizar.Visible = True
            lblCodEstado.Text = psEstado
            dt = obj.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigo, 0, "", psEstado, psTipoUbica)
            GvListaVerificarInventario.DataSource = dt
            GvListaVerificarInventario.DataBind()
            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
            End If
        End If

    End Sub

    Private Sub GvListaVerificarInventario_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaVerificarInventario.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblRegistro2.Text = ""
        Dim obj As New Cls_Inventario_Verificacion
        Dim objProceso As New clsInv_Procesos
        Dim dt As New DataTable
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psUbicaCodigoInv As Double = 0
        psUbicaCodigoInv = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim psEstado As String = ""
        Dim psSerieNueva As String = ""
        Dim psSeriaAnterior As String = ""
        Dim pdCambio As Double = 0
        Dim pdSerieNumerar As Double = 0
        Dim psUbicaCodigoDest As Double = 0
        Dim psUbicaTipoDest As String = ""
        psUbicaCodigoDest = Nz(LblUbicaCodigo.Text.ToString)
        If RBAlmacen.Checked Then
            psUbicaTipoDest = "1"
        ElseIf RBCentroC.Checked Then
            psUbicaTipoDest = "2"
        End If
        Try
            Dim psOrigenTipo As String = ""
        Dim pdCodSalida As Double = 0
        Dim pdOrigenCodigo As Double = 0
        Dim pdCodArt As Double = 0
            If e.CommandName = "Regularizar" Then
                Dim psRegularizar As String = GvListaVerificarInventario.Rows(Index).Cells(14).Text
                If psRegularizar = "No" Then
                    Dim Rs As SqlDataReader
                    Dim pdCodReg As Double = 0
                    Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                    Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
                    Dim CmdGlobal As New SqlCommand
                    Dim CmdGlobal2 As New SqlCommand
                    Cn.Open() : CmdGlobal.Connection = Cn
                    Cn2.Open() : CmdGlobal2.Connection = Cn2
                    If lblCodEstado.Text = "3" Then
                        pdSerieNumerar = Nz(GvListaVerificarInventario.Rows(Index).Cells(1).Text)
                        psOrigenTipo = Nu(GvListaVerificarInventario.Rows(Index).Cells(12).Text)
                        pdCodArt = Nz(GvListaVerificarInventario.Rows(Index).Cells(2).Text)
                        pdOrigenCodigo = Nz(GvListaVerificarInventario.Rows(Index).Cells(13).Text)
                        If psOrigenTipo = psUbicaTipoDest And pdOrigenCodigo <> psUbicaCodigoDest Then
                            pdCodSalida = objProceso.Invnetario_Salida_Ingreso_Automatico(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), psOrigenTipo, psUbicaTipoDest, pdOrigenCodigo, psUbicaCodigoDest, pdSerieNumerar, pdCodArt)
                            CmdGlobal.CommandText = "SELECT max(CAMBIO_CODIGO) FROM TBINV_ARTICULOS_SERIES_CAMBIO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    pdCambio = Nz(Rs(0)) + 1
                                End While
                            Else
                                pdCambio = 1
                            End If
                            Rs.Close()
                            CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_VERIFICACION  WHERE (VERIF_SERIE_NUMERAR=" & pdSerieNumerar & ") AND (INVENTUBIC_CODIGO=" & psUbicaCodigoInv & ") " _
                                          & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '1', INVDET_SALIDA_CODIGO = " & pdCodSalida & "  " _
                                                  & " WHERE (INVDET_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVDET_INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_REGULARIZAR = '1', VERIF_ESTADO_CONCILIADO = '1',VERIF_ESTADO_INVENTARIO = '3', VERIF_SALIDA_CODIGO = " & pdCodSalida & "  " _
                                                  & " WHERE (VERIF_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                                    CmdGlobal2.ExecuteNonQuery()  '
                                    CmdGlobal2.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_CAMBIO(EMPRESA_CODIGO, CAMBIO_CODIGO, SERIE_NUMERAR,CAMBIO_CAMPO, CAMBIO_DAFECTADO, CAMBIO_DACTUAL," _
                                                  & " CAMBIO_TIPOAFEC,CAMBIO_TIPOACT, CAMBIO_FECHA, CAMBIO_HORA, CAMBIO_MOTIVO,CAMBIO_SYS_EST,CAMBIO_SYS_CRE) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & pdCambio & "," & Nz(Rs!VERIF_SERIE_NUMERAR) & ", '3'," & pdOrigenCodigo & "," & psUbicaCodigoDest & ", " _
                                                  & " '" & psOrigenTipo & "','" & psUbicaTipoDest & "','" & FechaActual() & "','" & HoraActual() & "','4','0','" & Session("User") & FechaActual() & HoraActual() & "')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ESTADO_EQUIPO=  '" & Nu(Rs!VERIF_ESTADO_BIEN) & "',  " _
                                                   & " SERIE_CONCILIADO = '1', SERIE_ESTADO_INVENTARIO = '3' where SERIE_NUMERAR = " & Nz(Rs!VERIF_SERIE_NUMERAR)
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()
                        End If
                    ElseIf lblCodEstado.Text = "6" Then
                        psSeriaAnterior = Nu(GvListaVerificarInventario.Rows(Index).Cells(4).Text)
                        psSerieNueva = Nu(GvListaVerificarInventario.Rows(Index).Cells(5).Text)
                        pdSerieNumerar = Nz(GvListaVerificarInventario.Rows(Index).Cells(1).Text)
                        CmdGlobal.CommandText = "SELECT max(CAMBIO_CODIGO) FROM TBINV_ARTICULOS_SERIES_CAMBIO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                pdCambio = Nz(Rs(0)) + 1
                            End While
                        Else
                            pdCambio = 1
                        End If
                        Rs.Close()
                        CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_VERIFICACION  WHERE (VERIF_SERIE_NUMERAR=" & pdSerieNumerar & ") AND (INVENTUBIC_CODIGO=" & psUbicaCodigoInv & ") " _
                                          & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '1'  " _
                                                  & " WHERE (INVDET_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVDET_INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                                CmdGlobal2.ExecuteNonQuery()
                                CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_REGULARIZAR = '1', VERIF_ESTADO_CONCILIADO = '1',VERIF_ESTADO_INVENTARIO = '6'  " _
                                                  & " WHERE (VERIF_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                                CmdGlobal2.ExecuteNonQuery()  '
                                CmdGlobal2.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_CAMBIO(EMPRESA_CODIGO, CAMBIO_CODIGO, SERIE_NUMERAR,CAMBIO_CAMPO, CAMBIO_SERIEAFEC, CAMBIO_SERIEACT," _
                                                  & "  CAMBIO_FECHA, CAMBIO_HORA, CAMBIO_MOTIVO,CAMBIO_SYS_EST,CAMBIO_SYS_CRE) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & pdCambio & "," & Nz(Rs!VERIF_SERIE_NUMERAR) & ", '1','" & Nu(Rs!VERIF_SERIE_NRO_REAL) & "','" & Nu(Rs!VERIF_SERIE_NRO) & "', " _
                                                  & " '" & FechaActual() & "','" & HoraActual() & "','2','0','" & Session("User") & FechaActual() & HoraActual() & "')"
                                CmdGlobal2.ExecuteNonQuery()
                                CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ESTADO_EQUIPO=  '" & Nu(Rs!VERIF_ESTADO_BIEN) & "' , " _
                                                   & " SERIE_NRO = '" & Nu(Rs!VERIF_SERIE_NRO) & "',  SERIE_CONCILIADO = '1', SERIE_ESTADO_INVENTARIO = '6' where SERIE_NUMERAR = " & Nz(Rs!VERIF_SERIE_NUMERAR)
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        End If
                        Rs.Close()
                    End If
                    BtnListar_Click(sender, e)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('El bien ya ha sido regularizado');", True)
                End If
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub Regularizar_todo()
        Dim obj As New Cls_Inventario_Verificacion
        Dim objProceso As New clsInv_Procesos
        Dim dt As New DataTable
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psUbicaCodigoInv As Double = 0
        psUbicaCodigoInv = Nz(LblUbicaCodigoInv.Text.ToString)
        Dim psEstado As String = ""
        Dim psSerieNueva As String = ""
        Dim psSeriaAnterior As String = ""
        Dim pdCambio As Double = 0
        Dim pdSerieNumerar As Double = 0
        Dim psUbicaCodigoDest As Double = 0
        Dim psUbicaTipoDest As String = ""
        psUbicaCodigoDest = Nz(LblUbicaCodigo.Text.ToString)
        If RBAlmacen.Checked Then
            psUbicaTipoDest = "1"
        ElseIf RBCentroC.Checked Then
            psUbicaTipoDest = "2"
        End If
        Try
            Dim pdId As Double = 0
            Dim psOrigenTipo As String = ""
            Dim pdCodSalida As Double = 0
            Dim pdOrigenCodigo As Double = 0
            Dim pdCodArt As Double = 0
            Dim Rs As SqlDataReader
            Dim pdCodReg As Double = 0
            Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
            Dim CmdGlobal As New SqlCommand
            Dim CmdGlobal2 As New SqlCommand
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Dim pdPlaca As Double = 0
            Dim psSerieNro As String = ""

            For i = 0 To GvListaVerificarInventario.Rows.Count - 1
                Dim psRegularizar As String = GvListaVerificarInventario.Rows(i).Cells(14).Text
                If psRegularizar = "No" Then
                    If lblCodEstado.Text = "3" Or lblCodEstado.Text = "8" Then
                        pdSerieNumerar = Nz(GvListaVerificarInventario.Rows(i).Cells(1).Text)
                        psOrigenTipo = Nu(GvListaVerificarInventario.Rows(i).Cells(12).Text)
                        pdCodArt = Nz(GvListaVerificarInventario.Rows(i).Cells(2).Text)
                        pdOrigenCodigo = Nz(GvListaVerificarInventario.Rows(i).Cells(13).Text)
                        pdPlaca = Nz(GvListaVerificarInventario.Rows(i).Cells(6).Text)
                        psSerieNro = Nu(GvListaVerificarInventario.Rows(i).Cells(4).Text)
                        If psOrigenTipo = psUbicaTipoDest And pdOrigenCodigo <> psUbicaCodigoDest Then
                            pdCodSalida = objProceso.Invnetario_Salida_Ingreso_Automatico(Session("Ruta_Emp"), Session("CodEmpresa"), Session("User"), psOrigenTipo, psUbicaTipoDest, pdOrigenCodigo, psUbicaCodigoDest, pdSerieNumerar, pdCodArt)
                            CmdGlobal.CommandText = "SELECT max(CAMBIO_CODIGO) FROM TBINV_ARTICULOS_SERIES_CAMBIO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    pdCambio = Nz(Rs(0)) + 1
                                End While
                            Else
                                pdCambio = 1
                            End If
                            Rs.Close()
                            CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_VERIFICACION  WHERE (VERIF_SERIE_NUMERAR=" & pdSerieNumerar & ") AND (INVENTUBIC_CODIGO=" & psUbicaCodigoInv & ") " _
                                          & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '1', INVDET_SALIDA_CODIGO = " & pdCodSalida & "  " _
                                                  & " WHERE (INVDET_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVDET_INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_REGULARIZAR = '1', VERIF_ESTADO_CONCILIADO = '1',VERIF_ESTADO_INVENTARIO = '3', VERIF_SALIDA_CODIGO = " & pdCodSalida & "  " _
                                                  & " WHERE (VERIF_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                                    CmdGlobal2.ExecuteNonQuery()  '
                                    CmdGlobal2.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_CAMBIO(EMPRESA_CODIGO, CAMBIO_CODIGO, SERIE_NUMERAR,CAMBIO_CAMPO, CAMBIO_DAFECTADO, CAMBIO_DACTUAL," _
                                                  & " CAMBIO_TIPOAFEC,CAMBIO_TIPOACT, CAMBIO_FECHA, CAMBIO_HORA, CAMBIO_MOTIVO,CAMBIO_SYS_EST,CAMBIO_SYS_CRE) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & pdCambio & "," & Nz(Rs!VERIF_SERIE_NUMERAR) & ", '3'," & pdOrigenCodigo & "," & psUbicaCodigoDest & ", " _
                                                  & " '" & psOrigenTipo & "','" & psUbicaTipoDest & "','" & FechaActual() & "','" & HoraActual() & "','4','0','" & Session("User") & FechaActual() & HoraActual() & "')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ESTADO_EQUIPO=  '" & Nu(Rs!VERIF_ESTADO_BIEN) & "',  " _
                                                   & " SERIE_CONCILIADO = '1', SERIE_ESTADO_INVENTARIO = '3' where SERIE_NUMERAR = " & Nz(Rs!VERIF_SERIE_NUMERAR)
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()
                            CmdGlobal.CommandText = " SELECT MAX(ISNULL(ID_REGISTRO,0)) FROM TBINVENTARIO_REGULARIZACION_DATOS "
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    pdId = Nz(Rs(0)) + 1
                                End While
                            Else
                                pdId = 1
                            End If
                            Rs.Close()
                            CmdGlobal.CommandText = " INSERT INTO TBINVENTARIO_REGULARIZACION_DATOS ( EMPRESA_CODIGO, ID_REGISTRO, INV_CODIGO, INV_CODIGO_UBICACION, INV_ESTADO, SERIE_NUMERAR, PLACA_NRO, SERIE_NRO, " _
                                              & " SERIE_UBICA_ORIGEN_TIPO, SERIE_UBICA_ORIGEN_CODIGO, SERIE_UBICA_DESTINO_TIPO, SERIE_UBICA_DESTINO_CODIGO, SALIDA_CODIGO, SALIDA_FECHA, SYS_EST ) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "'," & pdId & ", " & psCodInv & ", " & psUbicaCodigoInv & " , '" & lblCodEstado.Text & "', " & pdSerieNumerar & ", " & pdPlaca & ", '" & psSerieNro & "', " _
                                              & " '" & psOrigenTipo & "', " & pdOrigenCodigo & ", '" & psUbicaTipoDest & "', " & psUbicaCodigoDest & ", " & pdCodSalida & ", '" & FechaActual() & "','0')"
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    ElseIf lblCodEstado.Text = "6" Then
                        psSeriaAnterior = Nu(GvListaVerificarInventario.Rows(i).Cells(4).Text)
                            psSerieNueva = Nu(GvListaVerificarInventario.Rows(i).Cells(5).Text)
                            pdSerieNumerar = Nz(GvListaVerificarInventario.Rows(i).Cells(1).Text)
                            CmdGlobal.CommandText = "SELECT max(CAMBIO_CODIGO) FROM TBINV_ARTICULOS_SERIES_CAMBIO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    pdCambio = Nz(Rs(0)) + 1
                                End While
                            Else
                                pdCambio = 1
                            End If
                            Rs.Close()
                            CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_VERIFICACION  WHERE (VERIF_SERIE_NUMERAR=" & pdSerieNumerar & ") AND (INVENTUBIC_CODIGO=" & psUbicaCodigoInv & ") " _
                                          & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '1'  " _
                                                  & " WHERE (INVDET_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVDET_INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_REGULARIZAR = '1', VERIF_ESTADO_CONCILIADO = '1',VERIF_ESTADO_INVENTARIO = '6'  " _
                                                  & " WHERE (VERIF_SERIE_NUMERAR=" & Nz(Rs!VERIF_SERIE_NUMERAR) & ") AND (INVENTUBIC_CODIGO=" & Nz(Rs!INVENTUBIC_CODIGO) & ") " _
                                                  & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                                    CmdGlobal2.ExecuteNonQuery()  '
                                    CmdGlobal2.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_CAMBIO(EMPRESA_CODIGO, CAMBIO_CODIGO, SERIE_NUMERAR,CAMBIO_CAMPO, CAMBIO_SERIEAFEC, CAMBIO_SERIEACT," _
                                                  & "  CAMBIO_FECHA, CAMBIO_HORA, CAMBIO_MOTIVO,CAMBIO_SYS_EST,CAMBIO_SYS_CRE) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & pdCambio & "," & Nz(Rs!VERIF_SERIE_NUMERAR) & ", '1','" & Nu(Rs!VERIF_SERIE_NRO_REAL) & "','" & Nu(Rs!VERIF_SERIE_NRO) & "', " _
                                                  & " '" & FechaActual() & "','" & HoraActual() & "','2','0','" & Session("User") & FechaActual() & HoraActual() & "')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_ESTADO_EQUIPO=  '" & Nu(Rs!VERIF_ESTADO_BIEN) & "' , " _
                                                   & " SERIE_NRO = '" & Nu(Rs!VERIF_SERIE_NRO) & "',  SERIE_CONCILIADO = '1', SERIE_ESTADO_INVENTARIO = '6' where SERIE_NUMERAR = " & Nz(Rs!VERIF_SERIE_NUMERAR)
                                    CmdGlobal2.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()
                        End If
                End If
            Next
            Dim dtLista As New DataTable
            dtLista = obj.Lista_Equipos_Inventariados_xEstado(Session("Ruta_Emp"), psCodInv, psUbicaCodigoInv, 0, "", lblCodEstado.Text, psUbicaTipoDest)
            GvListaVerificarInventario.DataSource = dtLista
            GvListaVerificarInventario.DataBind()
            If dtLista.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dtLista.Rows.Count & " registros."
            ElseIf dtLista.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Terminó la regularización');", True)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnRegularizar_Click(sender As Object, e As EventArgs) Handles BtnRegularizar.Click
        Call Regularizar_todo()
    End Sub
End Class
