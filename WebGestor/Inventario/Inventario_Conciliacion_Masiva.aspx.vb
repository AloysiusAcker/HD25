Imports WebGestor
Imports System.Data.SqlClient
Imports OfficeOpenXml
Imports System.Data
Partial Class Inventario_Inventario_Conciliacion_Masiva
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Llenar_Combos()
            DdlInventario.SelectedValue = "< Seleccionar >"
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
            DdlInventario.Items.Add("< Seleccionar >")
            DdlInventario.SelectedValue = "< Seleccionar >"

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        Finally
        End Try
    End Sub
    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click

        Try
            Listar_Equipos_Nuevos()
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la base de datos: " & ex.Message & "';", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Ha ocurrido un error en la aplicacion: " & ex.Message & "';", True)
        End Try

    End Sub
    Protected Sub Listar_Equipos_Nuevos()

        Dim obj As New Cls_Inventario_Verificacion
        Dim objInv As New Cls_Inventario
        Dim dt As New DataTable
        Dim drT As DataRow
        Dim drT2 As DataRow
        Dim dtNE As New DataTable
        Dim dtNI As New DataTable
        Dim dtO As New DataTable
        Dim pdCodinventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then pdCodinventario = DdlInventario.SelectedValue
        Dim i As Long = 0
        Dim codigo As Double = 0
        If TxtCodUbicaInv.Text <> "" Then
            codigo = TxtCodUbicaInv.Text
        End If

        Dim tipo As String = ""
        Dim ubicacion As Double = 0
        If TxtCodUbica.Text <> "" Then
            ubicacion = TxtCodUbica.Text
        End If

        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBCentroC.Checked Then
            tipo = "2"
        End If

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn

        CmdGlobal.CommandText = " delete from TBINVENTARIO_PLACA_TEMPORAL"
        CmdGlobal.ExecuteNonQuery()
        Cn.Close()

        Dim dtIns As New DataTable

        dt.Columns.Add("ART_CODIGO")
        dt.Columns.Add("ART_DESCRIPCION")
        dt.Columns.Add("SERIE_NRO")
        dt.Columns.Add("PLACA_NRO")
        dt.Columns.Add("ESTADO_INVENTARIO")
        dt.Columns.Add("NOMUSUARIO")
        dt.Columns.Add("SERIE_NUMERAR")
        dt.Columns.Add("AREA_NOMBRE")
        dt.Columns.Add("SERIE_STATUSU")

        Dim psSerieNumerar As String = ""

        Dim tempList As New List(Of String)
        Dim psPasar As String = "si"

        Dim psFiltros As String = ""
        psFiltros = " ART_CODIGO "

        dtO = obj.Inventario_BienesNuevos(Session("Ruta_Emp"), pdCodinventario, codigo, tipo, ubicacion, 0)
        If dtO.Rows.Count > 0 Then
            For Each dr As DataRow In dtO.Rows
                drT2 = dt.NewRow()
                drT2("ART_CODIGO") = Nu(dr("ART_CODIGO"))
                drT2("ART_DESCRIPCION") = Nu(dr("ART_DESCRIPCION"))
                drT2("SERIE_NRO") = Nu(dr("SERIE_NRO"))
                drT2("PLACA_NRO") = Nu(dr("PLACA_NRO"))
                drT2("ESTADO_INVENTARIO") = Nu(dr("ESTADO_INVENTARIO"))
                drT2("NOMUSUARIO") = Nu(dr("NOMUSUARIO"))
                drT2("SERIE_NUMERAR") = Nu(dr("SERIE_NUMERAR"))
                drT2("AREA_NOMBRE") = Nu(dr("AREA_NOMBRE"))
                drT2("SERIE_STATUSU") = Nu(dr("SERIE_STATUSU"))
                dt.Rows.Add(drT2)
                dtNI = objInv.Invenatrio_Conciliar_EquiposNoEncontrados(Session("Ruta_Emp"), Nz(dr("INVDET_INVENTUBIC_CODIGO")), Nz(dr("ART_CODIGO")), psSerieNumerar)
                If dtNI.Rows.Count > 0 Then
                    For Each drNI As DataRow In dtNI.Rows
                        drT = dt.NewRow()
                        drT("ART_CODIGO") = Nu(drNI("ART_CODIGO"))
                        drT("ART_DESCRIPCION") = Nu(drNI("ART_DESCRIPCION"))
                        drT("SERIE_NRO") = Nu(drNI("SERIE_NRO"))
                        drT("PLACA_NRO") = Nu(drNI("PLACA_NRO"))
                        drT("ESTADO_INVENTARIO") = Nu(drNI("ESTADO_INVENTARIO"))
                        drT("NOMUSUARIO") = Nu(drNI("NOMUSUARIO"))
                        drT("SERIE_NUMERAR") = Nu(drNI("SERIE_NUMERAR"))
                        drT("AREA_NOMBRE") = Nu(drNI("AREA_NOMBRE"))
                        drT("SERIE_STATUSU") = Nu(drNI("SERIE_STATUSU"))
                        dt.Rows.Add(drT)
                        dtIns = objInv.Inventario_TablaTemporal(Session("Ruta_Emp"), Nz(drNI("SERIE_NUMERAR")), Nz(drNI("PLACA_NRO")), Nu(drNI("SERIE_NRO")))
                    Next
                Else
                    dtNE = objInv.Invenatrio_EquiposNoEncontrados_xubi(Session("Ruta_Emp"), 379, Nz(dr("ART_CODIGO")), psSerieNumerar)
                    For Each drNE As DataRow In dtNE.Rows
                        drT = dt.NewRow()
                        drT("ART_CODIGO") = Nu(drNE("ART_CODIGO"))
                        drT("ART_DESCRIPCION") = Nu(drNE("ART_DESCRIPCION"))
                        drT("SERIE_NRO") = Nu(drNE("SERIE_NRO"))
                        drT("PLACA_NRO") = Nu(drNE("PLACA_NRO"))
                        drT("ESTADO_INVENTARIO") = Nu(drNE("ESTADO_INVENTARIO"))
                        drT("NOMUSUARIO") = Nu(drNE("NOMUSUARIO"))
                        drT("SERIE_NUMERAR") = Nu(drNE("SERIE_NUMERAR"))
                        drT("AREA_NOMBRE") = Nu(drNE("AREA_NOMBRE"))
                        drT("SERIE_STATUSU") = Nu(drNE("SERIE_STATUSU"))
                        dt.Rows.Add(drT)
                        dtIns = objInv.Inventario_TablaTemporal(Session("Ruta_Emp"), Nz(drNE("SERIE_NUMERAR")), Nz(drNE("PLACA_NRO")), Nu(drNE("SERIE_NRO")))
                    Next
                End If
siguiente:
            Next
        End If

        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()

        If dt.Rows.Count > 1 Then lblRegistro3.Text = "Hay " & dt.Rows.Count & " registros."
        If dt.Rows.Count = 1 Then lblRegistro3.Text = "Hay 1 registro."
        If dt.Rows.Count = 0 Then lblRegistro3.Text = "No hay registro."

    End Sub
    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        TxtCodUbica.Text = ""
        TxtCodUbicaInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        lblRegistro3.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        TxtCodUbica.Text = ""
        TxtCodUbicaInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        lblRegistro3.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvListaVerificarInventarioNuevos.DataSource = dt
        GvListaVerificarInventarioNuevos.DataBind()
    End Sub
    Private Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
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
        If TituloPopup.Text = "Busca Sección de Centro de Costo" Or TituloPopup.Text = "Busca Almacén" Or TituloPopup.Text = "Busca Ubicaciones" Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').one('hidden.bs.modal', function() { $('#ModalArticulos').modal('show'); }).modal('hide');", True)
        Else
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

    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            TxtCodUbica.Text = GvBusqueda.Rows(Index).Cells(3).Text
            TxtCodUbicaInv.Text = GvBusqueda.Rows(Index).Cells(4).Text
            Session("CodSeccion") = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If
        Limpiar_Cajas_Popup()
    End Sub

    Private Sub BtnConciliar_Click(sender As Object, e As EventArgs) Handles BtnConciliar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
        dt = Nothing

        Dim dtNuevos As New DataTable
        dtNuevos = Nothing
        Dim dt90423 As New DataTable
        dt90423 = Nothing
        Dim psCodInventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInventario = DdlInventario.SelectedValue
        End If
        Dim objInv As New Cls_Inventario

        Dim dtNE As New DataTable
        Dim dtNI As New DataTable
        Dim dtO As New DataTable
        Dim pdCodinventario As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then pdCodinventario = DdlInventario.SelectedValue
        Dim i As Long = 0
        Dim codigo As Double = 0
        If TxtCodUbicaInv.Text <> "" Then
            codigo = TxtCodUbicaInv.Text
        End If

        Dim tipo As String = ""
        Dim ubicacion As Double = 0
        If TxtCodUbica.Text <> "" Then
            ubicacion = TxtCodUbica.Text
        End If

        If RBAlmacen.Checked Then
            tipo = "1"
        ElseIf RBCentroC.Checked Then
            tipo = "2"
        End If

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn

        Dim Rs As SqlDataReader
        Dim pdCodReg As Double = 0
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim ValorSys As String = ""
        ValorSys = Session("User") & FechaActual() & HoraActual()
        Cn2.Open() : CmdGlobal2.Connection = Cn2


        CmdGlobal.CommandText = " delete from TBINVENTARIO_PLACA_TEMPORAL"
        CmdGlobal.ExecuteNonQuery()
        Cn.Close()

        Dim dtIns As New DataTable
        'variables de datos del bien con quien conciliar
        Dim psSerieNumerar As String = ""
        Dim pdCodArt As Double = 0
        Dim pdNroPlaca As Double = 0
        Dim psSerieNro As String = ""
        Dim pdUbicaInvConciliar As Double = 0
        Dim psTipoArt As Double = 0
        Dim pdAreaUbicacion As Double = 0
        Dim psUbicatipo As String = ""
        Dim pdUbicaCodigo As Double = 0


        'variables de los bienes nuevos a conciliar
        Dim psBNSerieNumerar As String = ""
        Dim pdBNCodArt As Double = 0
        Dim pdBNNroPlaca As Double = 0
        Dim psBNSerieNro As String = ""
        Dim pdBNUbicaInvConciliar As Double = 0
        Dim psBNTipoArt As Double = 0
        Dim pdBNAreaUbicacion As Double = 0
        Dim psBNUbicatipo As String = ""
        Dim pdBNUbicaCodigo As Double = 0
        Dim pbGuardar As Boolean = False
        Cn.Open()
        dtO = obj.Inventario_BienesNuevos(Session("Ruta_Emp"), pdCodinventario, codigo, tipo, ubicacion, 0)
        If dtO.Rows.Count > 0 Then
            For Each dr As DataRow In dtO.Rows
                psBNSerieNumerar = Nz(dr("SERIE_NUMERAR"))
                pdBNNroPlaca = Nz(dr("PLACA_NRO"))
                psBNSerieNro = Nu(dr("SERIE_NRO"))
                pdBNCodArt = Nz(dr("ART_CODIGO"))
                pdBNUbicaInvConciliar = Nu(dr("INVDET_INVENTUBIC_CODIGO"))
                psBNTipoArt = Nz(dr("ART_TIPO"))
                pdBNAreaUbicacion = Nz(dr("VERIF_AREA_UBICACION"))
                psBNUbicatipo = Nu(dr("INVENTUBIC_UBIC_TIPO"))
                pdBNUbicaCodigo = Nz(dr("INVENTUBIC_UBIC_CODIGO"))
                pbGuardar = False
                dtNI = objInv.Invenatrio_Conciliar_EquiposNoEncontrados_C(Session("Ruta_Emp"), Nz(dr("INVDET_INVENTUBIC_CODIGO")), Nz(dr("ART_CODIGO")), psSerieNumerar)
                If dtNI.Rows.Count > 0 Then
                    For Each drNI As DataRow In dtNI.Rows
                        psSerieNumerar = Nz(drNI("SERIE_NUMERAR"))
                        pdNroPlaca = Nz(drNI("PLACA_NRO"))
                        psSerieNro = Nu(drNI("SERIE_NRO"))
                        pdCodArt = Nz(drNI("ART_CODIGO"))
                        pdUbicaInvConciliar = Nu(drNI("INVDET_INVENTUBIC_CODIGO"))
                        psTipoArt = Nz(drNI("ART_TIPO"))
                        psUbicatipo = Nu(drNI("INVENTUBIC_UBIC_TIPO"))
                        pdUbicaCodigo = Nz(drNI("INVENTUBIC_UBIC_CODIGO"))
                        dtIns = objInv.Inventario_TablaTemporal(Session("Ruta_Emp"), Nz(drNI("SERIE_NUMERAR")), Nz(drNI("PLACA_NRO")), Nu(drNI("SERIE_NRO")))
                        pbGuardar = True
                    Next
                Else
                    dtNE = objInv.Invenatrio_EquiposNoEncontrados_xubi_C(Session("Ruta_Emp"), 379, Nz(dr("ART_CODIGO")), psSerieNumerar)
                    For Each drNE As DataRow In dtNE.Rows
                        psSerieNumerar = Nz(drNE("SERIE_NUMERAR"))
                        pdNroPlaca = Nz(drNE("PLACA_NRO"))
                        psSerieNro = Nu(drNE("SERIE_NRO"))
                        pdCodArt = Nz(drNE("ART_CODIGO"))
                        pdUbicaInvConciliar = Nu(drNE("INVDET_INVENTUBIC_CODIGO"))
                        psTipoArt = Nz(drNE("ART_TIPO"))
                        psUbicatipo = "2"
                        pdUbicaCodigo = 957
                        dtIns = objInv.Inventario_TablaTemporal(Session("Ruta_Emp"), Nz(drNE("SERIE_NUMERAR")), Nz(drNE("PLACA_NRO")), Nu(drNE("SERIE_NRO")))
                        pbGuardar = True
                    Next
                End If

                If pbGuardar = True Then
                    If psBNSerieNumerar <> 0 Then
                        If pdUbicaInvConciliar = 379 Then
                            CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET  INVDET_ESTADO_CONCILIADO = '3' , INVDET_PLACA_NRO = " & pdNroPlaca & ", INVDET_CONCILIADO = 'X', INVDET_CONCILIADO_SERIE_NUMERAR =" & psBNSerieNumerar & "  " _
                                              & " WHERE (INVDET_SERIE_NUMERAR=" & psSerieNumerar & ") AND (INVDET_INVENTUBIC_CODIGO=" & pdUbicaInvConciliar & ") " _
                                              & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0') "
                            CmdGlobal.ExecuteNonQuery()
                        End If
                        If pdUbicaInvConciliar = pdBNUbicaInvConciliar Then
                            CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '8', INVDET_ESTADO_CONCILIADO = '1' , INVDET_PLACA_NRO = " & pdNroPlaca & ", INVDET_CONCILIADO = 'X', INVDET_CONCILIADO_SERIE_NUMERAR =" & psBNSerieNumerar & "  " _
                                              & " WHERE (INVDET_SERIE_NUMERAR=" & psSerieNumerar & ") AND (INVDET_INVENTUBIC_CODIGO=" & pdBNUbicaInvConciliar & ") " _
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
                                                & " '" & Session("CodEmpresa") & "'  , " & pdBNUbicaInvConciliar & ", " & psSerieNumerar & ", " & pdNroPlaca & ", '" & psSerieNro & "', '0', " _
                                                & " '" & psUbicatipo & "'," & pdUbicaCodigo & ",'1',  '8', " & pdCodArt & ",'8', " _
                                                & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & psSerieNro & "'," & pdNroPlaca & " ,'2' ," & pdCorrelativo & " , " & pdCodArt & ",'X'," & psBNSerieNumerar & ")"
                            CmdGlobal.ExecuteNonQuery()
                            If pdBNAreaUbicacion > 0 Then
                                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_AREA_UBICACION = " & pdBNAreaUbicacion & " " _
                                                        & " WHERE INVENTUBIC_CODIGO = " & pdBNUbicaInvConciliar & " AND VERIF_SERIE_NUMERAR = " & psSerieNumerar
                                CmdGlobal.ExecuteNonQuery()
                            End If
                        End If
                        If pdUbicaInvConciliar <> pdBNUbicaInvConciliar Then

                            CmdGlobal.CommandText = " INSERT INTO TBINVENTARIO_DETALLE (EMPRESA_CODIGO,INVDET_INVENTUBIC_CODIGO,INVDET_ART_CODIGO, INVDET_SERIE_ESTADO_EQUIPO, INVDET_ESTADO_ACTIVO,  " _
                                                & " INVDET_SERIE_NUMERAR, INVDET_SERIE_NRO, INVDET_SYS_EST,INVDET_FECHA,INVDET_ESTADO_INGRESO,INVDET_ESTADO_INVENTARIO,INVDET_ESTADO_CONCILIADO ,INVDET_ESTADO_REGULARIZAR, " _
                                                & " INVDET_UBIC_TIPO,INVDET_UBIC_CODIGO,INVDET_SYS_CRE,INVDET_ART_TIPO,INVDET_CANTIDAD, INVDET_PLACA_NRO,INVDET_PLACA_NRO_REAL,INVDET_SERIE_NRO_REAL, INVDET_ART_CODIGO_REAL,INVDET_CONCILIADO,INVDET_CONCILIADO_SERIE_NUMERAR)" _
                                                & " VALUES ('" & Session("CodEmpresa") & "'," & pdBNUbicaInvConciliar & "," & pdCodArt & ", '1', '0' , " _
                                                & " " & psSerieNumerar & ",'" & psSerieNro & "','0','" & FechaActual() & "','2','8','1', '2'," _
                                                & " '" & psUbicatipo & "'," & pdUbicaCodigo & ",'" & ValorSys & "'," & psTipoArt & ",1, " & pdNroPlaca & "," & pdNroPlaca & ", '" & psSerieNro & "', " & pdCodArt & ",'X'," & psBNSerieNumerar & ") "
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
                                                & " '" & Session("CodEmpresa") & "'  , " & pdBNUbicaInvConciliar & ", " & psSerieNumerar & ", " & pdNroPlaca & ", '" & psSerieNro & "', '0', " _
                                                & " '" & psUbicatipo & "'," & pdUbicaCodigo & ",'1',  '8', " & pdCodArt & ",'8', " _
                                                & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & psSerieNro & "'," & pdNroPlaca & " ,'2' ," & pdCorrelativo & " , " & pdCodArt & ",'X'," & psBNSerieNumerar & ")"
                            CmdGlobal.ExecuteNonQuery()

                            If pdBNAreaUbicacion > 0 Then
                                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_SERIE_AREA = " & pdBNAreaUbicacion & " " _
                                                        & " WHERE INVDET_INVENTUBIC_CODIGO = " & pdBNUbicaInvConciliar & " AND INVDET_SERIE_NUMERAR = " & psSerieNumerar
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_AREA_UBICACION = " & pdBNAreaUbicacion & " " _
                                                        & " WHERE INVENTUBIC_CODIGO = " & pdBNUbicaInvConciliar & " AND VERIF_SERIE_NUMERAR = " & psSerieNumerar
                                CmdGlobal.ExecuteNonQuery()
                            End If
                        End If

                        If psBNSerieNumerar <> "" Then

                            CmdGlobal.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_CONCILIADO = '3',INVDET_CONCILIADO_SERIE_NUMERAR = " & psSerieNumerar & ", INVDET_SERIE_ESTADO_EQUIPO = '1', INVDET_CONCILIADO = 'X'  " _
                                                & " WHERE (INVDET_SERIE_NUMERAR=" & Nz(psBNSerieNumerar) & ") AND (INVDET_INVENTUBIC_CODIGO=" & pdBNUbicaInvConciliar & ") " _
                                                & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET  VERIF_ESTADO_CONCILIADO = '3' , VERIF_SERIE_NUMERAR_CONCILIADO  = " & psSerieNumerar & ", VERIF_CONCILIADO = 'X' " _
                                                & " WHERE (VERIF_SERIE_NUMERAR=" & Nz(psBNSerieNumerar) & ") AND (INVENTUBIC_CODIGO=" & pdBNUbicaInvConciliar & ") " _
                                                & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (VERIF_SYS_EST='0')"
                            CmdGlobal.ExecuteNonQuery()

                            CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_SYS_EST ='1' where SERIE_NUMERAR = " & Nz(psBNSerieNumerar)
                            CmdGlobal.ExecuteNonQuery()
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
                                                & " VALUES ('" & Session("CodEmpresa") & "', " & pdCodReg & ", " & psCodInventario & ", '" & psBNUbicatipo & "', " & pdBNUbicaCodigo & ", " & pdBNUbicaInvConciliar & ", " _
                                                & " " & psSerieNumerar & ", '" & psSerieNro & "', " & pdNroPlaca & ", " & pdCodArt & ", " & Nz(psBNSerieNumerar) & ", '" & psBNSerieNro & "', " _
                                                & " " & pdBNNroPlaca & ", " & pdBNCodArt & ", '1', '" & FechaActual() & "', '" & HoraActual() & "','" & Session("User") & "','" & Session("User") & FechaActual() & HoraActual() & "' , " _
                                                & " '0', " & pdUbicaInvConciliar & ", '" & psUbicatipo & "', " & pdUbicaCodigo & ")"
                        CmdGlobal.ExecuteNonQuery()



                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "Alert('Debe seleccionar un bien para conciliar.');", True)
                    End If
                End If
            Next
        End If
        Cn.Close()
        Cn2.Close()

        BtnListar_Click(sender, e)


    End Sub

    Private Sub BtnListaConciliados_Click(sender As Object, e As EventArgs) Handles BtnListaConciliados.Click
        Try
            Dim obj As New Cls_Inventario
            Dim dt As New DataTable
            lblRegistro.Text = ""
            GvLista.DataSource = Nothing
            GvLista.DataBind()
            GvListaVerificarInventarioNuevos.DataSource = Nothing
            GvListaVerificarInventarioNuevos.DataBind()
            Dim pdCodInvUbica As Double = 0
            Dim pdCodInv As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If
            If TxtCodUbicaInv.Text <> "" Then
                pdCodInvUbica = TxtCodUbicaInv.Text
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

    Private Sub BtnExportarConciliados_Click(sender As Object, e As EventArgs) Handles BtnExportarConciliados.Click


        Dim psFechaMov As String = ""
        Try
            Dim obj As New Cls_Inventario
            Dim dt As New DataTable
            lblRegistro.Text = ""
            GvLista.DataSource = Nothing
            GvLista.DataBind()
            GvListaVerificarInventarioNuevos.DataSource = Nothing
            GvListaVerificarInventarioNuevos.DataBind()
            Dim pdCodInvUbica As Double = 0
            Dim pdCodInv As Double = 0
            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                pdCodInv = DdlInventario.SelectedValue
            End If
            If TxtCodUbicaInv.Text <> "" Then
                pdCodInvUbica = TxtCodUbicaInv.Text
            End If
            dt = obj.Invenatrio_Conciliar_Listas_Exportar(Session("Ruta_Emp"), pdCodInvUbica, 0, pdCodInv)



            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("BienesConciliados")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=BienesConciliados.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()


            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub
End Class
