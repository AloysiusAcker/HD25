Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports WebGestor
Partial Class Inventario_Inventario_Verificar_Masivo
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        btnOpen.Attributes.Add("OnClick", "window.open('Inventario_PopPud_DatosOficina.aspx',null,'height=600,width=500');")

        'Dim ValorLatitud As String = objUbicacion.ObtenerValorLatitud
        'Dim ValorLongitud As String = objUbicacion.ObtenerValorLongitud

        If Not Page.IsPostBack Then
            Dim obj As New Cls_Inventario_Verificacion
            Dim dt As New DataTable

            dt = obj.Llenar_Combo_Inventario(Session("Ruta_Emp"))
            DdlInventario.DataSource = dt
            DdlInventario.DataValueField = "INVENT_CODIGO"
            DdlInventario.DataTextField = "INVENT_DESC"
            DdlInventario.DataBind()
            Call Llena_Ubicacion(ddlUbicacion)
            ddlUbicacion.SelectedValue = "< Seleccionar >"
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

    Private Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        ElseIf RBUbicaciones.Checked Then
            TituloPopup.Text = "Búsqueda Ubicaciones"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvPlacaNoExite.DataSource = dt
        gvPlacaNoExite.DataBind()
        lblPlacaNoExite.Text = ""
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
    End Sub
    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvPlacaNoExite.DataSource = dt
        gvPlacaNoExite.DataBind()
        lblPlacaNoExite.Text = ""
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
    End Sub
    Private Sub RBUbicaciones_CheckedChanged(sender As Object, e As EventArgs) Handles RBUbicaciones.CheckedChanged
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvPlacaNoExite.DataSource = dt
        gvPlacaNoExite.DataBind()
        lblPlacaNoExite.Text = ""
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim objUbic As New Cls_Inventario_Ubicacion
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Dim dt As New DataTable
        LblContador.Text = ""
        Dim pdCodInvUbi As Double = 0
        dt = Nothing
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvPlacaNoExite.DataSource = dt
        gvPlacaNoExite.DataBind()
        lblPlacaNoExite.Text = ""
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Try
            If TxtCodigoAyuda.Text <> "" Then pdCodInvUbi = Nz(TxtCodigoAyuda.Text)
            dt = objUbic.Inventario_Ubicacion_xCodigo(Session("CodEmpresa"), Session("Ruta_Emp"), pdCodInvUbi)
            If dt.Rows.Count > 0 Then
                'For Each dr As DataRow In dt.Rows
                '    If Nu(dr("INVENTUBIC_ESTADO")) = "5" Then
                '        BtnIniciarVerificacion.Visible = True
                '        'BtnCerrarInv.Visible = True
                '        BtnIniciarVerificacion.Enabled = False
                '        'BtnCerrarInv.Enabled = False
                '    Else
                '        BtnIniciarVerificacion.Visible = True
                '        'BtnCerrarInv.Visible = True
                '        BtnIniciarVerificacion.Enabled = True
                '        'BtnCerrarInv.Enabled = True
                '    End If
                'Next
                Listar_Inventario_Verificacion()
                accordion.Visible = True
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "')", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "')", True)
        End Try
    End Sub

    Private Sub Listar_Inventario_Verificacion()
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
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        gvPlacaNoExite.DataSource = dt
        gvPlacaNoExite.DataBind()
        lblPlacaNoExite.Text = ""
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        Dim psconexion As String = Session("Ruta_Emp")
        Dim pdCodArt As Double = 0
        Dim pdCodInv As Double = 0
        Dim pdCodUbicInv As Double = 0
        Try

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


            dt = obj.Inventario_ListaPendiente_CargaMasiva(psconexion, pdCodInvUbica, "")
            gvPlacaNoExite.DataSource = dt
            gvPlacaNoExite.DataBind()
            ' 
            If dt.Rows.Count > 1 Then
                lblPlacaNoExite.Text = "Hay " & dt.Rows.Count & " placas que no existen."
            ElseIf dt.Rows.Count = 1 Then
                lblPlacaNoExite.Text = "Hay 1 placa que no exite."
            End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & "');", True)
        End Try
    End Sub


    Protected Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New Cls_Inventario_Verificacion
        Dim dt As New DataTable
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
    Private Sub Llenar_DatosOficina()
        Dim pdCCCodigo As Double = 0
        Dim dt As New DataTable
        Dim objCC As New clsLogis_Listado
        pdCCCodigo = Nz(Session("CodSeccion"))
        Try
            dt = objCC.Busca_Centro_Costos_Seccion_xCodigo(Session("Ruta_Emp"), Session("CodEmpresa"), 0, pdCCCodigo)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    'txtCCCod.Text = Nu(dr("CECOSE_COD_INTERNO"))
                    'txtCCDescripcion.Text = Nu(dr("CECOSE_DESCRIPCION"))
                    'txtCCCargo.Text = Nu(dr("CARGO"))
                    'txtCCNombre.Text = Nu(dr("NOMBRE"))
                    'txtCCAnexo.Text = Nu(dr("ANEXO"))
                    'txtCCTelefono.Text = Nu(dr("TELEFONO"))
                    'txtCCCelular.Text = Nu(dr("CEL_BANCO"))
                    'txtCCCorreo.Text = Nu(dr("CORREO"))
                    'txtCCTipo.Text = Nu(dr("TIPO_OFICINA"))
                Next
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos:" & ex.Message & "');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('ha ocurrido un error en la aplicacion:" & ex.Message & "');", True)
        End Try
    End Sub
    Protected Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub

    Private Sub DdlInventario_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlInventario.SelectedIndexChanged
        TxtCodigoAyuda.Text = ""
        TxtCodigoAyudaUbicacion.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        TxtCodigo.Text = ""
        LblContador.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        accordion.Visible = False
        gvListaTop5.DataSource = dt
        gvListaTop5.DataBind()
        GvListaVerificarInventario.DataSource = dt
        GvListaVerificarInventario.DataBind()
        GvListaVerificarInventarioOtros.DataSource = dt
        GvListaVerificarInventarioOtros.DataBind()
        gvPlacaNoExite.DataSource = dt
        gvPlacaNoExite.DataBind()
        lblPlacaNoExite.Text = ""
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
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
                            If fileContent <> "" Then
                                Cargar_Placa_aVerificar(Nz(fileContent))
                            End If
                            Session("Fin") = "Si"
                        End While
                    End Using
                    '' Muestra el contenido en la página
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga.');", True)
                    BtnListar_Click(sender, e)
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

    Private Sub Cargar_Placa_aVerificar(ByVal pdPlacaNro As Double)

        Dim obj As New Cls_Inventario_Verificacion
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim pd_Ubicacion As Double = 0

        Dim dt1 As New DataTable
        Dim placa As Double = 0
        Dim serie As String = ""
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim RsL As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim pd_ArticuloCodigo As Double = 0
        Dim ValorSys As String = ""
        ValorSys = Session("User") & FechaActual() & HoraActual()
        Dim pdCodInvUbicacion As Double = 0
        If TxtCodigoAyuda.Text <> "" Then pdCodInvUbicacion = Nz(TxtCodigoAyuda.Text)
        Try
            Dim ps_UbicaTipo As String = ""
            Dim pd_UbicaCodigo As Double = 0
            Dim pd_InvUbicaCodigo As Double = 0
            Dim ps_InvUbicaTipo As String = ""
            Dim psInventarioCod As Double = 0
            Dim pdCorrelativo As Double = 0

            If DdlInventario.SelectedValue <> "< Seleccionar >" Then
                psInventarioCod = DdlInventario.SelectedValue
            End If

            If RBAlmacen.Checked = True Then ps_InvUbicaTipo = "1"
            If RBCentroC.Checked = True Then ps_InvUbicaTipo = "2"
            pd_InvUbicaCodigo = Nz(TxtCodigoAyudaUbicacion.Text)
            Dim psUbicacion As String = ""
            If ddlUbicacion.SelectedValue <> "< Seleccionar >" Then
                pd_Ubicacion = ddlUbicacion.SelectedValue
                psUbicacion = ddlUbicacion.Items(ddlUbicacion.SelectedIndex).Text
            End If
            Dim psSerie_Nro As String = ""

            Cn.Open()
            CmdGlobal.Connection = Cn
            Cn2.Open()
            CmdGlobal2.Connection = Cn2
            Cn3.Open()
            CmdGlobal3.Connection = Cn3
            placa = pdPlacaNro
            dt = obj.Buscar_Serie_Numerar(Session("Ruta_Emp"), placa, serie)
            Dim numerar As Double = 0
            If dt.Rows.Count > 0 Then
                For Each drow As DataRow In dt.Rows
                    numerar = Nz(drow("SERIE_NUMERAR"))
                Next
            Else
                CmdGlobal2.CommandText = " INSERT INTO TBINVENTARIO_VERIFICAR_MASIVO_PENDIENTE ( PLACA_NRO, INVENT_UBICACODIGO, UBICACION_CODIGO,INVENT_TIPOREGISTRO,INVENT_FECHA,INVENT_HORA,INVENT_USER)  " _
                                       & " VALUES (" & placa & "," & pdCodInvUbicacion & ",'" & psUbicacion & "','1','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "') "
                CmdGlobal2.ExecuteNonQuery()
            End If
            dt = Nothing

            Dim psEstadoInv_Tabla As String = ""

            CmdGlobal2.CommandText = " SELECT * FROM TBINVENTARIO_DETALLE WHERE (INVDET_INVENTUBIC_CODIGO=" & pdCodInvUbicacion & ") AND  INVDET_SERIE_NUMERAR =" & numerar & "  " _
                                                    & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
            RsL = CmdGlobal2.ExecuteReader
            If RsL.HasRows Then
                While RsL.Read
                    psEstadoInv_Tabla = Nu(RsL("INVDET_ESTADO_INVENTARIO"))
                End While
            End If
            RsL.Close()

            If psEstadoInv_Tabla = "2" Or psEstadoInv_Tabla = "" Then

                If numerar > 0 Then

                    dt = obj.Cargar_Datos_Bien(Session("Ruta_Emp"), Session("CodEmpresa"), numerar, "", "")
                    If dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            ps_UbicaTipo = Nu(dr("UBICACT_TIPO"))
                            pd_UbicaCodigo = Nu(dr("UBICACT_CODIGO"))
                            pd_ArticuloCodigo = Nz(dr("COD_ARTICULO"))
                            psSerie_Nro = Nu(dr("SERIE_NRO"))
                        Next
                    End If
                    Dim psExiste As String = "N"
                    Dim psEstadoInv As String = "1"
                    If ps_InvUbicaTipo = ps_UbicaTipo And pd_InvUbicaCodigo = pd_UbicaCodigo Then
                        psEstadoInv = "1"
                    Else
                        psEstadoInv = "3"
                    End If

                    CmdGlobal.CommandText = " SELECT IUBIC.INVENTUBIC_CODIGO, IUBIC.INVENTUBIC_NRO, " _
                                        & " IUBIC.INVENTUBIC_UBIC_TIPO, IUBIC.INVENTUBIC_UBIC_CODIGO,IUBIC.INVENTUBIC_ESTADO " _
                                        & " FROM dbo.TBINVENTARIO I INNER JOIN dbo.TBINVENTARIO_UBICACIONES IUBIC ON " _
                                        & " I.INVENT_CODIGO = IUBIC.INVENTUBIC_NRO AND i.EMPRESA_CODIGO = IUBIC.EMPRESA_CODIGO " _
                                        & " WHERE (I.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (IUBIC.EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') " _
                                        & " AND (I.INVENT_SYS_EST = '0') AND (IUBIC.INVENTUBIC_UBIC_TIPO='" & ps_InvUbicaTipo & "') " _
                                        & " AND IUBIC.INVENTUBIC_UBIC_CODIGO='" & pd_InvUbicaCodigo & "' AND IUBIC.INVENTUBIC_ESTADO='2'  " _
                                        & " AND (IUBIC.INVENTUBIC_SYS_EST = '0') AND (IUBIC.INVENTUBIC_NRO='" & psInventarioCod & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            CmdGlobal2.CommandText = " SELECT * FROM TBINVENTARIO_DETALLE WHERE (INVDET_INVENTUBIC_CODIGO='" & Nz(Rs!INVENTUBIC_CODIGO) & "') AND  INVDET_SERIE_NUMERAR =" & numerar & "  " _
                                                        & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                            RsL = CmdGlobal2.ExecuteReader
                            If RsL.HasRows Then
                                While RsL.Read
                                    CmdGlobal3.CommandText = "  UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_ACTIVO = '1' " _
                                                           & " WHERE  INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                    CmdGlobal3.ExecuteNonQuery()
                                    CmdGlobal3.CommandText = "  UPDATE TBINVENTARIO_VERIFICACION SET VERIF_ESTADO_ACTIVO = '1' " _
                                                           & " WHERE  VERIF_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                    CmdGlobal3.ExecuteNonQuery()
                                    If psEstadoInv <> "7" Then
                                        CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '" & psEstadoInv & "', INVDET_ESTADO_CONCILIADO = '1' ,  INVDET_ESTADO_ACTIVO = '0' , " _
                                                               & " INVDET_SERIE_ESTADO_EQUIPO = '1', INVDET_RESPONSABLE_OBSERVACION = 'CARGA MASIVA', INVDET_INVENTUBIC_CODIGO='" & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & "' ,INVDET_PLACA_NRO = " & placa & " " _
                                                               & " WHERE (INVDET_INVENTUBIC_CODIGO='" & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & "') AND (INVDET_SERIE_NUMERAR='" & Nz(RsL!INVDET_SERIE_NUMERAR) & "')  " _
                                                               & " AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "') AND (INVDET_SYS_EST='0')"
                                        CmdGlobal3.ExecuteNonQuery() '
                                    End If
                                    If pd_Ubicacion > 0 Then
                                        CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_SERIE_AREA = " & pd_Ubicacion & " " _
                                                                & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    If psEstadoInv = "1" Then
                                        CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '1' " _
                                                                & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                        CmdGlobal3.ExecuteNonQuery() '
                                    Else
                                        CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_REGULARIZAR = '2' " _
                                                                & " WHERE INVDET_INVENTUBIC_CODIGO = " & Nz(RsL!INVDET_INVENTUBIC_CODIGO) & " AND INVDET_SERIE_NUMERAR = " & Nz(RsL!INVDET_SERIE_NUMERAR)
                                        CmdGlobal3.ExecuteNonQuery() ' 
                                    End If

                                    CmdGlobal3.CommandText = " SELECT * FROM TBINVENTARIO_VERIFICACION WHERE INVENTUBIC_CODIGO= " & Nz(Rs!INVENTUBIC_CODIGO) & " AND VERIF_SERIE_NUMERAR = " & numerar
                                    Rs2 = CmdGlobal3.ExecuteReader '
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
                                        CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_VERIFICACION (EMPRESA_CODIGO, INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR,  VERIF_SERIE_NRO, VERIF_ESTADO_ACTIVO, " _
                                            & " VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO,  VERIF_ESTADO_BIEN, VERIF_AREA_UBICACION, VERIF_ESTADO, VERIF_ART_CODIGO, " _
                                            & " VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_FECHA, VERIF_HORA,VERIF_SERIE_NRO_REAL,VERIF_REGULARIZAR, VERIF_CORRELATIVO,VERIF_OBSERVACION, VERIF_ART_CODIGO_REAL) VALUES ( " _
                                            & " '" & Session("CodEmpresa") & "'  , " & Nz(Rs!INVENTUBIC_CODIGO) & ", " & numerar & ", '" & psSerie_Nro & "','0', " _
                                            & " '" & ps_InvUbicaTipo & "', " & pd_InvUbicaCodigo & ", '1', " & pd_Ubicacion & ", '" & psEstadoInv & "', " & pd_ArticuloCodigo & ", " _
                                            & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & psSerie_Nro & "'  ,'" & IIf(psEstadoInv = "1", "1", "2") & "', " & pdCorrelativo & ", 'CARGA MASIVA', " & pd_ArticuloCodigo & ")"
                                        CmdGlobal3.ExecuteNonQuery()
                                        If placa > 0 Then
                                            CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_VERIFICACION SET VERIF_PLACA_NRO = " & placa & "  " _
                                                                       & " WHERE (INVENTUBIC_CODIGO='" & Nz(Rs!INVENTUBIC_CODIGO) & "') AND (VERIF_SERIE_NUMERAR='" & numerar & "')  "
                                            CmdGlobal3.ExecuteNonQuery()
                                        End If '
                                    ElseIf psExiste = "S" Then
                                        CmdGlobal3.CommandText = " update TBINVENTARIO_VERIFICACION SET VERIF_SERIE_NRO='" & psSerie_Nro & "', VERIF_ESTADO_ACTIVO ='0', VERIF_PLACA_NRO = " & placa & ", " _
                                                               & " VERIF_OBSERVACION = 'CARGA MASIVA', VERIF_AREA_UBICACION = " & pd_Ubicacion & " ,VERIF_ART_CODIGO = " & pd_ArticuloCodigo & "  " _
                                                               & " where INVENTUBIC_CODIGO= " & Nz(Rs!INVENTUBIC_CODIGO) & " AND VERIF_SERIE_NUMERAR = " & numerar
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    If psEstadoInv <> "7" Then
                                        CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                                            & " SERIE_CONCILIADO = '1', " _
                                                            & " SERIE_ESTADO_INVENTARIO = '" & psEstadoInv & "', " _
                                                            & " SERIE_ESTADO = '1' " _
                                                            & " WHERE SERIE_NUMERAR = " & numerar
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                End While
                                RsL.Close()
                            Else
                                CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_DETALLE (EMPRESA_CODIGO,INVDET_INVENTUBIC_CODIGO,INVDET_ART_CODIGO, INVDET_SERIE_ESTADO_EQUIPO, INVDET_ESTADO_ACTIVO,  " _
                                                                & " INVDET_SERIE_NUMERAR, INVDET_SERIE_NRO, INVDET_SYS_EST,INVDET_FECHA,INVDET_ESTADO_INGRESO,INVDET_ESTADO_INVENTARIO,INVDET_ESTADO_CONCILIADO ,INVDET_ESTADO_REGULARIZAR, " _
                                                                & " INVDET_UBIC_TIPO,INVDET_UBIC_CODIGO,INVDET_SYS_CRE,INVDET_ART_TIPO,INVDET_CANTIDAD,INVDET_SERIE_AREA, INVDET_PLACA_NRO,INVDET_PLACA_NRO_REAL,INVDET_SERIE_NRO_REAL, INVDET_ART_CODIGO_REAL)" _
                                                                & " VALUES ('" & Session("CodEmpresa") & "','" & Nz(Rs!INVENTUBIC_CODIGO) & "'," & pd_ArticuloCodigo & ", '1', '0' , " _
                                                                & " " & numerar & ",'" & psSerie_Nro & "','0','" & FechaActual() & "','2','" & psEstadoInv & "','1', '2'," _
                                                                & " '" & ps_UbicaTipo & "'," & pd_UbicaCodigo & ",'" & ValorSys & "'," & pd_ArticuloCodigo & ",1," & pd_Ubicacion & ", " & IIf(placa = 0, "NULL", placa) & "," & IIf(placa = 0, "NULL", placa) & ", '" & psSerie_Nro & "', " & pd_ArticuloCodigo & ") "
                                CmdGlobal3.ExecuteNonQuery()
                                CmdGlobal3.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET " _
                                                        & " SERIE_CONCILIADO = '1', " _
                                                        & " SERIE_ESTADO_INVENTARIO = '" & psEstadoInv & "', " _
                                                        & " SERIE_ESTADO = '1' " _
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
                                CmdGlobal3.CommandText = " INSERT INTO TBINVENTARIO_VERIFICACION (EMPRESA_CODIGO, INVENTUBIC_CODIGO, VERIF_SERIE_NUMERAR, VERIF_PLACA_NRO, VERIF_SERIE_NRO, VERIF_ESTADO_ACTIVO ," _
                                        & " VERIF_UBIC_TIPO, VERIF_UBIC_CODIGO,  VERIF_ESTADO_BIEN, VERIF_AREA_UBICACION, VERIF_ESTADO, VERIF_ART_CODIGO, " _
                                        & " VERIF_SYS_EST, VERIF_SYS_CRE, VERIF_FECHA, VERIF_HORA,VERIF_SERIE_NRO_REAL,VERIF_PLACA_NRO_REAL,VERIF_REGULARIZAR,VERIF_CORRELATIVO, VERIF_OBSERVACION, VERIF_ART_CODIGO_REAL) VALUES ( " _
                                        & " '" & Session("CodEmpresa") & "'  , " & Nz(Rs!INVENTUBIC_CODIGO) & ", " & numerar & ", " & placa & ", '" & psSerie_Nro & "', '0', " _
                                        & " '" & ps_UbicaTipo & "', " & pd_UbicaCodigo & ", '1', " & pd_Ubicacion & ", '" & psEstadoInv & "', " & pd_ArticuloCodigo & ", " _
                                        & " '0', '" & ValorSys & "', '" & FechaActual() & "', '" & HoraActual() & "', '" & psSerie_Nro & "'," & placa & " ,'2' ," & pdCorrelativo & " ,'CARGA MASIVA', " & pd_ArticuloCodigo & ")"
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                        End While
                    End If
                    Rs.Close()
                End If
            Else
                CmdGlobal2.CommandText = " INSERT INTO TBINVENTARIO_VERIFICAR_MASIVO_PENDIENTE ( PLACA_NRO, INVENT_UBICACODIGO, UBICACION_CODIGO,INVENT_TIPOREGISTRO,INVENT_FECHA,INVENT_HORA,INVENT_USER)  " _
                                       & " VALUES (" & placa & "," & pdCodInvUbicacion & ",'" & psUbicacion & "','2','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "') "
                CmdGlobal2.ExecuteNonQuery()

            End If
        Catch ex As Exception

        End Try


    End Sub

End Class
