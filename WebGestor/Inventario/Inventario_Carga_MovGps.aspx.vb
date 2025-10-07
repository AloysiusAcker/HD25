Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports Excel
Imports OfficeOpenXml
Imports System.IO
Imports DataTable = System.Data.DataTable
Partial Class Inventario_Inventario_Carga_MovGps
    Inherits System.Web.UI.Page
    Private Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click
        If RBAlmacen.Checked Then
            TituloPopup.Text = "Búsqueda Almacén"
        ElseIf RBCentroC.Checked Then
            TituloPopup.Text = "Búsqueda Sección de Centro de Costo"
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
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

            DdlInventario.Items.Add("< Seleccionar >")
            DdlInventario.SelectedValue = "< Seleccionar >"
            Session("UnaVez") = "1"
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

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        LblRegistro2.Text = ""
        lblError.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable

        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)

        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")
        Try

            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else
            dt = obj.Lista_BienesNoEncontrados_Gps(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica)
            gvListaGps.DataSource = dt
            gvListaGps.DataBind()

            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
            End If
            dt = Nothing
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub RBAlmacen_CheckedChanged(sender As Object, e As EventArgs) Handles RBAlmacen.CheckedChanged
        LblUbicaCodigo.Text = ""
        LblUbicaCodigoInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub RBCentroC_CheckedChanged(sender As Object, e As EventArgs) Handles RBCentroC.CheckedChanged
        LblUbicaCodigo.Text = ""
        LblUbicaCodigoInv.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        Dim dt As New DataTable
        dt = Nothing
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

        Limpiar_Cajas_Popup()
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
        Dim psconexion As String = Session("Ruta_Emp")
        Dim inventario As Double = 0
        inventario = Nz(DdlInventario.SelectedValue.ToString)
        Dim codigo As Double = 0
        Dim descripcion As String = BuscarDescripcion.Value.ToString
        Dim psCodInterno As String = ""

        If TituloPopup.Text = "Búsqueda Almacén" Then
            codigo = Nz(BuscarCodigo.Value.ToString)
            dt = obj.Listar_Almacenes_Inventario_Verificacion(psconexion, inventario, codigo, descripcion)
        ElseIf TituloPopup.Text = "Búsqueda Sección de Centro de Costo" Then
            psCodInterno = BuscarCodigo.Value.ToString
            dt = obj.Listar_CentroC_Inventario_Verificacion(psconexion, inventario, psCodInterno, descripcion)
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

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)

        'If psCodInv_Ubica = 0 Then
        '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
        'Else
        If nz(TxtIni.Text) = 0 Or nz(Txtfin.Text) = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el Inicio y fin de las filas a cargar.');", True)
        Else

            If fileUpload.HasFile Then
                Dim excelFilePath As String = Server.MapPath("~/Inventario/ArchivoTemp/Excel.xlsx")

                ' Guardar el archivo subido
                fileUpload.SaveAs(excelFilePath)

                ' Leer datos desde Excel y cargarlos en la base de datos
                LoadDataFromExcel(excelFilePath)

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de archivo.');", True)
                ' Opcional: Eliminar el archivo Excel después de cargar los datos
                ' File.Delete(excelFilePath)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar archivo.');", True)
            End If

        End If
    End Sub
    Private Sub LoadDataFromExcel(excelFilePath As String)
        Dim connectionString As String = Session("Ruta_Emp")
        Dim pdCodInv_Ubica As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim Rs As SqlDataReader
        pdCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        Session("UnaVez") = "1"
        Dim psSerieEquipo As String = ""
        Dim pdPlacaNro As String = ""
        Dim psSerieNro As String = ""
        Dim psMaterial As String = ""
        Dim psMaterialDescripcion As String = ""
        Dim psFechaMov As String = ""
        Dim psAlmacen As String = ""
        Dim psCentroCosto As String = ""
        Dim psStatusUsu As String = ""
        Dim psTipoEquipo As String = ""
        Dim Valorsys As String = Session("User") & FechaActual() & HoraActual()
        Dim psProveedor As String = ""

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        filaIni = TxtIni.Text
        filafin = Txtfin.Text
        Dim dt As New DataTable
        Dim valor As String = ""
        Dim objGps As New Cls_Inventario


        Using package As New ExcelPackage(New FileInfo(excelFilePath))
            Dim workbook As ExcelWorkbook = package.Workbook
            If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)

                ' Recorrer las celdas del archivo Excel
                For row As Integer = filaIni To filafin

                    If pdCodInv_Ubica = 0 Then

                        psSerieEquipo = Nu(worksheet.Cells(row, 1).Value) ' Nu(excelWorksheet.Cells(row, 1).Value)
                        pdPlacaNro = Nu(worksheet.Cells(row, 1).Value)
                        psSerieNro = Nu(worksheet.Cells(row, 6).Value)
                        psMaterial = Nu(worksheet.Cells(row, 4).Value)
                        psMaterialDescripcion = Nu(worksheet.Cells(row, 5).Value)
                        psFechaMov = Nu(worksheet.Cells(row, 17).Value)
                        psAlmacen = Nu(worksheet.Cells(row, 8).Value)
                        psCentroCosto = Nu(worksheet.Cells(row, 9).Value)
                        psStatusUsu = Nu(worksheet.Cells(row, 14).Value)
                        psTipoEquipo = Nu(worksheet.Cells(row, 48).Value)
                        psProveedor = Nu(worksheet.Cells(row, 15).Value)
                        pdPlacaNro = Mid(psSerieEquipo, 12, 7)
                        pdCodInv_Ubica = 0
                        dt = objGps.Lista_Bienes_Gps_xSerieEquipoNoEncontrado(Session("Ruta_Emp"), psSerieEquipo)
                        If dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                pdCodInv_Ubica = Nz(dr("INVDET_INVENTUBIC_CODIGO"))
                            Next
                        End If
                        If pdCodInv_Ubica > 0 Then
                            CmdGlobal.CommandText = " select * from TBINVENTARIO_GPS_DATOS where GPS_PLACA_NRO = " & pdPlacaNro
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then

                            Else
                                CmdGlobal2.CommandText = " INSERT INTO TBINVENTARIO_GPS_DATOS (EMPRESA_CODIGO,INVUBIC_CODIGO,GPS_SERIE_EQUIPO ,GPS_PLACA_NRO, " _
                                                    & " GPS_SERIE_NRO,GPS_MATERIAL,GPS_DESCRIPCION,GPS_FECHA_MOV,GPS_ALMACEN,GPS_CENTRO_COSTO,GPS_STATUS_USU, " _
                                                    & " GPS_TIPO_EQUIPO,GPS_SYS_EST,GPS_SYS_CRE,GPS_PROVEEDOR,GPS_NOENCONTRADO) VALUES ('" & Session("CodEmpresa") & "', " & pdCodInv_Ubica & ", '" & psSerieEquipo & "', " & pdPlacaNro & ", " _
                                                    & " '" & psSerieNro & "','" & psMaterial & "','" & psMaterialDescripcion & "','" & psFechaMov & "','" & psAlmacen & "','" & psCentroCosto & "','" & psStatusUsu & "', " _
                                                    & "  '" & psTipoEquipo & "', '0', '" & Valorsys & "','" & psProveedor & "','1')"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                            Rs.Close()
                        Else
                            CmdGlobal.CommandText = " select * from TBINVENTARIO_GPS_DATOS where GPS_PLACA_NRO = " & pdPlacaNro
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then

                            Else
                                CmdGlobal2.CommandText = " INSERT INTO TBINVENTARIO_GPS_DATOS (EMPRESA_CODIGO,GPS_SERIE_EQUIPO ,GPS_PLACA_NRO, " _
                                                & " GPS_SERIE_NRO,GPS_MATERIAL,GPS_DESCRIPCION,GPS_FECHA_MOV,GPS_ALMACEN,GPS_CENTRO_COSTO,GPS_STATUS_USU, " _
                                                & " GPS_TIPO_EQUIPO,GPS_SYS_EST,GPS_SYS_CRE,GPS_PROVEEDOR,GPS_NOENCONTRADO) VALUES ('" & Session("CodEmpresa") & "', '" & psSerieEquipo & "', " & pdPlacaNro & ", " _
                                                & " '" & psSerieNro & "','" & psMaterial & "','" & psMaterialDescripcion & "','" & psFechaMov & "','" & psAlmacen & "','" & psCentroCosto & "','" & psStatusUsu & "', " _
                                                & "  '" & psTipoEquipo & "', '0', '" & Valorsys & "','" & psProveedor & "','1')"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                            Rs.Close()
                        End If
                        pdCodInv_Ubica = 0
                    Else
                        psSerieEquipo = Nu(worksheet.Cells(row, 1).Value) ' Nu(excelWorksheet.Cells(row, 1).Value)
                        pdPlacaNro = Nu(worksheet.Cells(row, 1).Value)
                        psSerieNro = Nu(worksheet.Cells(row, 6).Value)
                        psMaterial = Nu(worksheet.Cells(row, 4).Value)
                        psMaterialDescripcion = Nu(worksheet.Cells(row, 5).Value)
                        psFechaMov = Nu(worksheet.Cells(row, 17).Value)
                        psAlmacen = Nu(worksheet.Cells(row, 8).Value)
                        psCentroCosto = Nu(worksheet.Cells(row, 9).Value)
                        psStatusUsu = Nu(worksheet.Cells(row, 14).Value)
                        psTipoEquipo = Nu(worksheet.Cells(row, 48).Value)
                        psProveedor = Nu(worksheet.Cells(row, 15).Value)
                        pdPlacaNro = Mid(psSerieEquipo, 12, 7)

                        CmdGlobal.CommandText = " select * from TBINVENTARIO_GPS_DATOS where INVUBIC_CODIGO =  " & pdCodInv_Ubica & " and GPS_PLACA_NRO =" & pdPlacaNro
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then

                        Else
                            CmdGlobal2.CommandText = " INSERT INTO TBINVENTARIO_GPS_DATOS (EMPRESA_CODIGO,INVUBIC_CODIGO,GPS_SERIE_EQUIPO ,GPS_PLACA_NRO, " _
                                                    & " GPS_SERIE_NRO,GPS_MATERIAL,GPS_DESCRIPCION,GPS_FECHA_MOV,GPS_ALMACEN,GPS_CENTRO_COSTO,GPS_STATUS_USU, " _
                                                    & " GPS_TIPO_EQUIPO,GPS_SYS_EST,GPS_SYS_CRE,GPS_PROVEEDOR,GPS_NOENCONTRADO) VALUES ('" & Session("CodEmpresa") & "', " & pdCodInv_Ubica & ", '" & psSerieEquipo & "', " & pdPlacaNro & ", " _
                                                    & " '" & psSerieNro & "','" & psMaterial & "','" & psMaterialDescripcion & "','" & psFechaMov & "','" & psAlmacen & "','" & psCentroCosto & "','" & psStatusUsu & "', " _
                                                    & "  '" & psTipoEquipo & "', '0', '" & Valorsys & "','" & psProveedor & "','1')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                        Rs.Close()

                    End If
                Next
            End If
        End Using
    End Sub

    Private Sub BtnListarMob_Click(sender As Object, e As EventArgs) Handles BtnListarMob.Click
        LblRegistro2.Text = ""
        lblError.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable

        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)

        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else

            dt = obj.Lista_Bienes_GpsNoEncontrados_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica)
            gvListaGps.DataSource = dt
            gvListaGps.DataBind()

            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
            End If
            dt = Nothing
            'End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub BtnListaInf_Click(sender As Object, e As EventArgs) Handles BtnListaInf.Click
        LblRegistro2.Text = ""
        lblError.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable

        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)

        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else
            dt = obj.Lista_Bienes_GpsNoEncontrados_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica)
            gvListaGps.DataSource = dt
            gvListaGps.DataBind()

            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
            End If
            dt = Nothing
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click
        Call GenerarGps_Datos501_Mob()
    End Sub
    Private Sub ExportarDatosGps_501Mob()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else


            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsNoEncontrado_Generar501_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov)

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("501_MOV_MOB")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=501_" & TxtDescripcion.Text & "_NoEncontradoMOB.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub ExportarDatosGps_501Inf()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsNoEncontrado_Generar501_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov)

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("501_MOV_INF")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=501_" & TxtDescripcion.Text & "_NoEncontradoINF.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub GenerarGps_Datos501_Mob()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Invenatrio/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim pdRegistro As Double = 0
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else


            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsNoEncontrado_Generar501_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov)

            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "501_" & TxtDescripcion.Text & "_NoEncontradoMOB.txt"
            Dim fullPath As String = Path.Combine(savePath, fileName)
            If dt1.Rows.Count > 0 Then
                Using fileWriter As New System.IO.StreamWriter(fullPath)
                    For Each dr As DataRow In dt1.Rows
                        pdRegistro = pdRegistro + 1
                        If pdRegistro = 1 Then
                            fileWriter.Write(Nz(dr("Numeral")).ToString() & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                            & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                            & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                            & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & vbTab & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                            & Nu(dr("CCosto_NroSerie")).ToString() & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                            & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                            & Nu(dr("Texto")).ToString() & vbTab & Nu(dr("Caract")).ToString() & vbTab)
                            fileWriter.WriteLine()
                        Else
                            fileWriter.Write(Nz(dr("Numeral")).ToString() & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                            & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                            & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                            & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                            & Nu(dr("CCosto_NroSerie")).ToString() & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                            & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                            & Nu(dr("Texto")).ToString() & vbTab & Nu(dr("Caract")).ToString() & vbTab)
                            fileWriter.WriteLine()
                        End If
                    Next
                End Using
            End If


            Response.Clear()
            Response.ContentType = "application/txt"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.TransmitFile(fullPath)
            Response.End()
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try

    End Sub

    Private Sub GenerarGps_Datos501_Inf()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Invenatrio/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim pdRegistro As Double = 0
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else


            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsNoEncontrado_Generar501_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov)

            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "501_" & TxtDescripcion.Text & "_NoEncontradoINF.txt"
            Dim fullPath As String = Path.Combine(savePath, fileName)
            If dt1.Rows.Count > 0 Then
                Using fileWriter As New System.IO.StreamWriter(fullPath)
                    For Each dr As DataRow In dt1.Rows
                        pdRegistro = pdRegistro + 1
                        If pdRegistro = 1 Then
                            fileWriter.Write(Nz(dr("Numeral")).ToString() & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                            & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                            & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                            & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                            & Nu(dr("CCosto_NroSerie")).ToString() & vbTab & vbTab & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                            & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                            & Nu(dr("Texto")).ToString() & vbTab & Nu(dr("Caract")).ToString() & vbTab)
                            fileWriter.WriteLine()
                        Else
                            fileWriter.Write(Nz(dr("Numeral")).ToString() & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                            & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                            & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                            & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                            & Nu(dr("CCosto_NroSerie")).ToString() & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                            & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                            & Nu(dr("Texto")).ToString() & vbTab & Nu(dr("Caract")).ToString() & vbTab)
                            fileWriter.WriteLine()
                        End If
                    Next
                End Using
            End If

            'File.Move(savePath & fileName, "D:\INVENTARIO 20223\" & fileName)
            Response.Clear()
            Response.ContentType = "application/txt"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.TransmitFile(fullPath)
            Response.End()
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try

    End Sub
    Private Sub GenerarGps_Datos201_Inf()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Inventario/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim pdRegistro As Double = 0
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else
            Dim pdSIAAF As Double = 0

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsNoEncontrado_Generar201_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov)
            Dim psCaract As String = ""
            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "201_BIENESNoEncontrados_INF.txt"
            Dim fullPath As String = Path.Combine(savePath, fileName)
            If dt1.Rows.Count > 0 Then
                Using fileWriter As New System.IO.StreamWriter(fullPath)
                    For Each dr As DataRow In dt1.Rows
                        pdRegistro = pdRegistro + 1
                        psCaract = ""
                        dt2 = objdatos.BuscarCaracteristica_xCecoseInterno(Session("Ruta_Emp"), "PE11000904")
                        For Each dr2 As DataRow In dt2.Rows
                            psCaract = Nu(dr2("CARACTERISTICA"))
                        Next

                        If Nu(dr("Fecha_Almacen")) <> FormatoFecha(FechaActual) Then
                            fileWriter.Write("1001" & vbTab & "1" & vbTab & "201" & vbTab & "" & vbTab _
                                                & psFechaMov & vbTab & "" & vbTab & "                " & vbTab & pdSIAAF & vbTab _
                                                & "PE11000904" & vbTab & vbTab & vbTab & "PE")
                            fileWriter.WriteLine()
                            fileWriter.Write(Nz(dr("Numeral")).ToString() & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                                & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                                & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                                & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                                & "" & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                                & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                                & Nu(dr("Texto")).ToString() & vbTab & psCaract & vbTab)
                            fileWriter.WriteLine()
                        End If
                    Next
                End Using
            End If

            Response.Clear()
            Response.ContentType = "application/txt"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.TransmitFile(fullPath)
            Response.End()
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try

    End Sub
    Private Sub GenerarGps_Datos201_Mob()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Inventario/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim psCaract As String = ""
        Dim dt2 As New DataTable
        Dim pdRegistro As Double = 0
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else
            Dim pdSIAAF As Double = 0

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsNoEncontrado_Generar201_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov)

            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "201_BIENESNoEncontrados_MOB.txt"
            Dim fullPath As String = Path.Combine(savePath, fileName)
            If dt1.Rows.Count > 0 Then
                Using fileWriter As New System.IO.StreamWriter(fullPath)
                    For Each dr As DataRow In dt1.Rows
                        pdRegistro = pdRegistro + 1
                        psCaract = ""
                        dt2 = objdatos.BuscarCaracteristica_xCecoseInterno(Session("Ruta_Emp"), "PE11000904")
                        For Each dr2 As DataRow In dt2.Rows
                            psCaract = Nu(dr2("CARACTERISTICA"))
                        Next

                        If Nu(dr("Fecha_Almacen")) <> FormatoFecha(FechaActual) Then
                            fileWriter.Write("1001" & vbTab & "1" & vbTab & "201" & vbTab & "" & vbTab _
                                                & psFechaMov & vbTab & "" & vbTab & "                " & vbTab & pdSIAAF & vbTab _
                                                & "PE11000904" & vbTab & vbTab & vbTab & "PE")
                            fileWriter.WriteLine()
                            fileWriter.Write(Nz(dr("Numeral")).ToString() & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                                & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                                & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                                & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                                & "" & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                                & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                                & Nu(dr("Texto")).ToString() & vbTab & Nu(dr("Caract")).ToString() & vbTab)
                            fileWriter.WriteLine()
                        End If
                    Next
                End Using
            End If

            'File.Move(savePath & fileName, "D:\INVENTARIO 20223\" & fileName)
            Response.Clear()
            Response.ContentType = "application/txt"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.TransmitFile(fullPath)
            Response.End()
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try

    End Sub
    Private Sub BtnGenerarInf_Click(sender As Object, e As EventArgs) Handles BtnGenerarInf.Click

        Call GenerarGps_Datos501_Inf()
    End Sub

    Private Sub BtnExcelInf_Click(sender As Object, e As EventArgs) Handles BtnExcelInf.Click
        Call ExportarDatosGps_501Inf()
    End Sub

    Private Sub BtnExcelMob_Click(sender As Object, e As EventArgs) Handles BtnExcelMob.Click
        Call ExportarDatosGps_501Mob()
    End Sub

    Private Sub Btn201Inf_Click(sender As Object, e As EventArgs) Handles Btn201Inf.Click
        Call GenerarGps_Datos201_Inf()
    End Sub

    Private Sub Btn201Mob_Click(sender As Object, e As EventArgs) Handles Btn201Mob.Click
        GenerarGps_Datos201_Mob()
    End Sub

    Private Sub BtnLista201M_Click(sender As Object, e As EventArgs) Handles BtnLista201M.Click
        LblRegistro2.Text = ""
        lblError.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable

        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)

        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else

            dt = obj.Lista_BienesNoencontrados_Gps201_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica)
            gvListaGps.DataSource = dt
            gvListaGps.DataBind()

            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
            End If
            dt = Nothing
            'End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub BtnLista201I_Click(sender As Object, e As EventArgs) Handles BtnLista201I.Click
        LblRegistro2.Text = ""
        lblError.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable

        Dim psCodInv As Double = 0
        If DdlInventario.SelectedValue <> "< Seleccionar >" Then
            psCodInv = DdlInventario.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)

        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else

            dt = obj.Lista_BienesNoEncontrado_Gps201_Inf(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica)
            gvListaGps.DataSource = dt
            gvListaGps.DataBind()

            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro."
            End If
            dt = Nothing
            'End If

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub BtnCargarInvOk_Click(sender As Object, e As EventArgs) Handles BtnCargarInvOk.Click

        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else

            obj.Inventariados_CargaTabla_BienesOk(Session("Ruta_Emp"), Session("User"), FechaActual, HoraActual)

            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de los Bienes Inventariado Ok.');", True)

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
            lblError.Text = "Ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Sub

    Private Sub BtnExportar_Click(sender As Object, e As EventArgs) Handles BtnExportar.Click
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        Dim drT As DataRow
        'If DdlInventario.SelectedValue <> "< Seleccionar >" Then
        '    psCodInv = DdlInventario.SelectedValue
        'End If
        Dim iRow As Double = 0
        Dim dt2 As New DataTable
        Try

            Dim dt As New DataTable()
            dt.Columns.Add("GPS_SERIE_EQUIPO", GetType(String))
            dt.Columns.Add("GPS_PLACA_NRO", GetType(String))
            dt.Columns.Add("GPS_SERIE_NRO", GetType(String))
            dt.Columns.Add("GPS_MATERIAL", GetType(String))
            dt.Columns.Add("GPS_DESCRIPCION", GetType(String))
            dt.Columns.Add("GPS_FECHA_MOV", GetType(String))
            dt.Columns.Add("GPS_ALMACEN", GetType(String))
            dt.Columns.Add("UBICACION_ACTUAL", GetType(String))
            dt.Columns.Add("GPS_STATUS_USU", GetType(String))
            dt.Columns.Add("GPS_TIPO_EQUIPO", GetType(String))
            dt.Columns.Add("UBICACION_FINAL", GetType(String))

            If gvListaGps.Rows.Count > 0 Then
                For iRow = 0 To gvListaGps.Rows.Count - 1
                    drT = dt.NewRow()
                    drT("GPS_SERIE_EQUIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(0).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(1).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_MATERIAL") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_FECHA_MOV") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(5).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("UBICACION_ACTUAL") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(7).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_STATUS_USU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(8).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPS_TIPO_EQUIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(9).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("UBICACION_FINAL") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(10).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    dt.Rows.Add(drT)
                Next
                Using excelPackage As New ExcelPackage()
                    ' Agregar hojas al archivo de Excel
                    Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("BienesNoEncontrados")

                    ' Llenar Hoja1 con los datos de dt1
                    worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                    ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                    Response.Clear()
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment; filename=BienesNoEncontrados_xMover.xlsx")
                    Response.BinaryWrite(excelPackage.GetAsByteArray())
                    Response.End()
                End Using
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('No hay bienes que exportar.');", True)
            End If


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)

        End Try
    End Sub
End Class
