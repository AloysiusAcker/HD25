Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports OfficeOpenXml
Imports DataTable = System.Data.DataTable
Partial Class Inventario_Inventario_Interface_GPS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New Cls_Inventario_Verificacion
            Dim objC As New Cls_Catalogo
            Dim objCn As New Cls_Conexion
            Dim dt As New DataTable
            '1
            Dim psconexion As String = Session("Ruta_Emp")
            ddlMovTipo.Items.Add("< Seleccionar >")
            ddlMovTipo.SelectedValue = "< Seleccionar >"
            ddlMovTipo.Items.Add("Normal")
            ddlMovTipo.SelectedValue = "1"
            ddlMovTipo.Items.Add("No Encontrados")
            ddlMovTipo.SelectedValue = "2"
        End If
    End Sub

    Private Sub BtnCargaBienes_Click(sender As Object, e As EventArgs) Handles BtnCargaBienes.Click
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Dim pdMovNro As Double = 0
        Dim pdCodUbicaInv As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de los Bienes Inventariado Ok.');", True)
            If ddlMovTipo.SelectedValue = "2" Then
                If lblCodCCostos.Text = "" Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Centro de Costo.');", True)
            Else

                If Nz(TxtIni.Text) = 0 Or Nz(Txtfin.Text) = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el Inicio y fin de las filas a cargar.');", True)
                Else

                    If fileUpload.HasFile Then
                        Dim psNuevoMov As String = ""
                        pdMovNro = Nz(txtMovNro.Text)
                        CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_INTERFACE_GPS where GPS_MOV_NRO = " & pdMovNro
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psNuevoMov = "No"
                            End While
                        Else
                            psNuevoMov = "Si"
                        End If
                        Rs.Close()
                        If psNuevoMov = "Si" Then
                            CmdGlobal.CommandText = " insert into TBINVENTARIO_INTERFACE_GPS (GPS_MOV_NRO, GPS_MOV_TIPO, GPS_MOV_DESCRIPCION, GPS_MOV_FECHA, GPS_MOV_HORA, " _
                                      & " GPS_MOV_USER, GPS_MOV_ESTADO,  GPS_MOV_SYS_EST, GPS_MOV_SYS_CRE) " _
                                      & " VALUES (" & pdMovNro & ", '3', '" & txtMovDescripcion.Text & "', '" & FechaActual() & "', '" & HoraActual() & "', " _
                                      & " '" & Session("User") & "', '1','0','" & Session("User") & FechaActual() & HoraActual() & "')  "
                            CmdGlobal.ExecuteNonQuery()
                        End If

                        Dim excelFilePath As String = Server.MapPath("~/Inventario/ArchivoTemp/Excel.xlsx")

                        ' Guardar el archivo subido
                        fileUpload.SaveAs(excelFilePath)

                        ' Leer datos desde Excel y cargarlos en la base de datos
                        LeerExcel(excelFilePath)

                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de archivo.');", True)
                        ' Opcional: Eliminar el archivo Excel después de cargar los datos
                        ' File.Delete(excelFilePath)
                        BtnListar_Click(sender, e)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar archivo.');", True)
                    End If

                End If

            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try

    End Sub

    Private Sub LeerExcel(excelFilePath As String)
        Dim connectionString As String = Session("Ruta_Emp")
        Dim pdCodInv_Ubica As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal2 As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim Rs As SqlDataReader
        pdCodInv_Ubica = 0 ' Nz(LblUbicaCodigoInv.Text.ToString)
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
        Dim psCentroCostoFinal As String = ""
        Dim Valorsys As String = Session("User") & FechaActual() & HoraActual()
        Dim psProveedor As String = ""
        Dim psTipoMov As String = ""
        psTipoMov = ddlMovTipo.SelectedValue

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        filaIni = TxtIni.Text
        filafin = Txtfin.Text
        Dim dt As New DataTable
        Dim valor As String = ""
        Dim psEstado As String = ""
        Dim objGps As New Cls_Inventario
        Dim psMov501Fecha As String = ""
        Dim psMov201Fecha As String = ""
        Dim psMov501 As String = ""
        Dim psMov201 As String = ""

        Using package As New ExcelPackage(New FileInfo(excelFilePath))
            Dim workbook As ExcelWorkbook = package.Workbook
            If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)
                'serie_equipo    placa	serie	material	descripcion	centro costo actual	centro costo final

                ' Recorrer las celdas del archivo Excel
                For row As Integer = filaIni To filafin
                    psSerieEquipo = Nu(worksheet.Cells(row, 1).Value)
                    pdPlacaNro = Nu(worksheet.Cells(row, 2).Value)
                    psSerieNro = Nu(worksheet.Cells(row, 3).Value)
                    psMaterial = Nu(worksheet.Cells(row, 4).Value)
                    psMaterialDescripcion = Nu(worksheet.Cells(row, 5).Value)
                    psCentroCosto = Nu(worksheet.Cells(row, 6).Value)
                    psCentroCostoFinal = Nu(worksheet.Cells(row, 7).Value)
                    psFechaMov = Nu(worksheet.Cells(row, 8).Value)
                    psAlmacen = Nu(worksheet.Cells(row, 9).Value)
                    psStatusUsu = Nu(worksheet.Cells(row, 10).Value)
                    psTipoEquipo = Nu(worksheet.Cells(row, 11).Value)
                    psProveedor = Nu(worksheet.Cells(row, 12).Value)

                    psEstado = "0"
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS SET GPS_MOV_ESTADO =  '2' where GPS_MOV_NRO = " & Nz(txtMovNro.Text)
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " select * from TBINVENTARIO_INTERFACE_GPS_DETALLE where GPSDET_PLACA_NRO =" & pdPlacaNro & " AND GPS_MOV_NRO = " & Nz(txtMovNro.Text)
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            If psCentroCostoFinal = psCentroCosto Then
                                psEstado = "1"
                            Else
                                If psAlmacen = "" Then
                                    psEstado = "2" : psMov501 = "X"
                                Else
                                    psEstado = "3" : psMov201 = "X"
                                End If
                            End If
                            If Nu(Rs("GPSDET_MOV501")) = "X" Then psMov501Fecha = psFechaMov
                            If Nu(Rs("GPSDET_MOV201")) = "X" Then psMov201Fecha = psFechaMov
                            CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS_DETALLE Set GPSDET_FECHA_MOV = '" & psFechaMov & "' , GPSDET_PROVEEDOR = '" & psProveedor & "' , " _
                                                   & " GPSDET_ALMACEN = '" & psAlmacen & "' , GPSDET_CENTRO_COSTO = '" & psCentroCosto & "' , GPSDET_ESTADO = '" & psEstado & "', " _
                                                   & " GPSDET_MOV501 = '" & psMov501 & "', GPSDET_MOV501_FECHA='" & psMov501Fecha & "', GPSDET_MOV201 = '" & psMov201 & "', GPSDET_MOV201_FECHA='" & psMov201Fecha & "', " _
                                                   & " GPSDET_STATUS_USU = '" & psStatusUsu & "' , GPSDET_TIPO_EQUIPO =  '" & psTipoEquipo & "', GPSDET_SYS_MOD = '" & Valorsys & "' " _
                                                   & " WHERE GPSDET_SERIE_EQUIPO = '" & psSerieEquipo & "' AND GPS_MOV_NRO = " & Nz(txtMovNro.Text) & " AND GPSDET_PLACA_NRO = " & pdPlacaNro
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        If psTipoMov = "3" Then
                            If psAlmacen = "" Then
                                psEstado = "2" : psMov501 = "X"
                            Else
                                psEstado = "3" : psMov201 = "X"
                            End If
                            CmdGlobal2.CommandText = " insert into TBINVENTARIO_INTERFACE_GPS_DETALLE (GPS_MOV_NRO, GPSDET_CENTRO_COSTO_FINAL, " _
                                              & " GPSDET_SERIE_EQUIPO, GPSDET_PLACA_NRO, GPSDET_SERIE_NRO, GPSDET_MATERIAL,  GPSDET_DESCRIPCION, " _
                                              & " GPSDET_FECHA_CARGA, GPSDET_ESTADO, GPSDET_SYS_EST, GPSDET_SYS_CRE) " _
                                              & " VALUES (" & Nz(txtMovNro.Text) & ", '" & psCentroCostoFinal & "',  " & psSerieEquipo & ", " & pdPlacaNro & ", '" & psSerieNro & "', " _
                                              & " '" & psMaterial & "','" & psMaterialDescripcion & "' , '" & FechaActual() & "' ,'" & psEstado & "', '0', '" & Valorsys & "' )  "
                            CmdGlobal2.ExecuteNonQuery()
                            CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS_DETALLE Set GPSDET_FECHA_MOV = '" & psFechaMov & "' , GPSDET_PROVEEDOR = '" & psProveedor & "' , " _
                                                   & " GPSDET_ALMACEN = '" & psAlmacen & "' , GPSDET_CENTRO_COSTO = '" & psCentroCosto & "' , GPSDET_ESTADO = '" & psEstado & "', " _
                                                   & " GPSDET_MOV501 = '" & psMov501 & "', GPSDET_MOV501_FECHA='" & psMov501Fecha & "', GPSDET_MOV201 = '" & psMov201 & "', GPSDET_MOV201_FECHA='" & psMov201Fecha & "', " _
                                                   & " GPSDET_STATUS_USU = '" & psStatusUsu & "' , GPSDET_TIPO_EQUIPO =  '" & psTipoEquipo & "', GPSDET_SYS_MOD = '" & Valorsys & "' " _
                                                   & " WHERE GPSDET_SERIE_EQUIPO = '" & psSerieEquipo & "' AND GPS_MOV_NRO = " & Nz(txtMovNro.Text) & " AND GPSDET_PLACA_NRO = " & pdPlacaNro
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                    End If
                    Rs.Close()
                Next
            End If
        End Using
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

        Dim psCodInv_Ubica As Double = 0

        'If psCodInv_Ubica = 0 Then
        '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
        'Else
        If Nz(TxtIni.Text) = 0 Or Nz(Txtfin.Text) = 0 Then
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
        pdCodInv_Ubica = 0 ' Nz(LblUbicaCodigoInv.Text.ToString)
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
        Dim psTipoMov As String = ""
        psTipoMov = ddlMovTipo.SelectedValue

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        filaIni = TxtIni.Text
        filafin = Txtfin.Text
        Dim dt As New DataTable
        Dim valor As String = ""
        Dim psEstado As String = ""
        Dim objGps As New Cls_Inventario
        Dim psMov501Fecha As String = ""
        Dim psMov201Fecha As String = ""
        Dim psMov501 As String = ""
        Dim psMov201 As String = ""

        Using package As New ExcelPackage(New FileInfo(excelFilePath))
            Dim workbook As ExcelWorkbook = package.Workbook
            If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)

                ' Recorrer las celdas del archivo Excel
                For row As Integer = filaIni To filafin
                    psSerieEquipo = Nu(worksheet.Cells(row, 1).Value) ' Nu(excelWorksheet.Cells(row, 1).Value)
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
                    psMov501 = "" : psMov501Fecha = ""
                    psMov201 = "" : psMov201Fecha = ""
                    psEstado = "0"
                    'estado 0 =carga,1=nada,2=mov501,3=mov201
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS SET GPS_MOV_ESTADO =  '2' where GPS_MOV_NRO = " & Nz(txtMovNro.Text)
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " select * from TBINVENTARIO_INTERFACE_GPS_DETALLE where GPSDET_PLACA_NRO =" & pdPlacaNro & " AND GPS_MOV_NRO = " & Nz(txtMovNro.Text)
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            If Nu(Rs("GPSDET_CENTRO_COSTO_FINAL")) = psCentroCosto Then
                                psEstado = "1"
                            Else
                                If psAlmacen = "" Then
                                    psEstado = "2" : psMov501 = "X"
                                Else
                                    psEstado = "3" : psMov201 = "X"
                                End If
                            End If
                            If Nu(Rs("GPSDET_MOV501")) = "X" Then psMov501Fecha = psFechaMov
                            If Nu(Rs("GPSDET_MOV201")) = "X" Then psMov201Fecha = psFechaMov
                            CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS_DETALLE Set GPSDET_FECHA_MOV = '" & psFechaMov & "' , GPSDET_PROVEEDOR = '" & psProveedor & "' , " _
                                                   & " GPSDET_ALMACEN = '" & psAlmacen & "' , GPSDET_CENTRO_COSTO = '" & psCentroCosto & "' , GPSDET_ESTADO = '" & psEstado & "', " _
                                                   & " GPSDET_MOV501 = '" & psMov501 & "', GPSDET_MOV501_FECHA='" & psMov501Fecha & "', GPSDET_MOV201 = '" & psMov201 & "', GPSDET_MOV201_FECHA='" & psMov201Fecha & "', " _
                                                   & " GPSDET_STATUS_USU = '" & psStatusUsu & "' , GPSDET_TIPO_EQUIPO =  '" & psTipoEquipo & "', GPSDET_SYS_MOD = '" & Valorsys & "' " _
                                                   & " WHERE GPSDET_SERIE_EQUIPO = '" & psSerieEquipo & "' AND GPS_MOV_NRO = " & Nz(txtMovNro.Text) & " AND GPSDET_PLACA_NRO = " & pdPlacaNro
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        If psTipoMov = "2" Then
                            If psAlmacen = "" Then
                                psEstado = "2" : psMov501 = "X"
                            Else
                                psEstado = "3" : psMov201 = "X"
                            End If
                            CmdGlobal2.CommandText = " insert into TBINVENTARIO_INTERFACE_GPS_DETALLE (GPS_MOV_NRO, GPSDET_CENTRO_COSTO_FINAL, " _
                                              & " GPSDET_SERIE_EQUIPO, GPSDET_PLACA_NRO, GPSDET_SERIE_NRO, GPSDET_MATERIAL,  GPSDET_DESCRIPCION, " _
                                              & " GPSDET_FECHA_CARGA, GPSDET_ESTADO, GPSDET_SYS_EST, GPSDET_SYS_CRE, GPSDET_NOENCONTRADO) " _
                                              & " VALUES (" & Nz(txtMovNro.Text) & ", '" & TxtCodigo.Text & "',  " & psSerieEquipo & ", " & pdPlacaNro & ", '" & psSerieNro & "', " _
                                              & " '" & psMaterial & "','" & psMaterialDescripcion & "' , '" & FechaActual() & "' ,'" & psEstado & "', '0', '" & Valorsys & "','1' )  "
                            CmdGlobal2.ExecuteNonQuery()
                            CmdGlobal2.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS_DETALLE Set GPSDET_FECHA_MOV = '" & psFechaMov & "' , GPSDET_PROVEEDOR = '" & psProveedor & "' , " _
                                                   & " GPSDET_ALMACEN = '" & psAlmacen & "' , GPSDET_CENTRO_COSTO = '" & psCentroCosto & "' , GPSDET_ESTADO = '" & psEstado & "', " _
                                                   & " GPSDET_MOV501 = '" & psMov501 & "', GPSDET_MOV501_FECHA='" & psMov501Fecha & "', GPSDET_MOV201 = '" & psMov201 & "', GPSDET_MOV201_FECHA='" & psMov201Fecha & "', " _
                                                   & " GPSDET_STATUS_USU = '" & psStatusUsu & "' , GPSDET_TIPO_EQUIPO =  '" & psTipoEquipo & "', GPSDET_SYS_MOD = '" & Valorsys & "' " _
                                                   & " WHERE GPSDET_SERIE_EQUIPO = '" & psSerieEquipo & "' AND GPS_MOV_NRO = " & Nz(txtMovNro.Text) & " AND GPSDET_PLACA_NRO = " & pdPlacaNro
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                    End If
                    Rs.Close()
                Next
            End If
        End Using
    End Sub

    Private Sub BtnDefinirMov_Click(sender As Object, e As EventArgs) Handles BtnDefinirMov.Click
        Mov.Visible = True
        Mov2.Visible = True
        MovCCostos.Visible = False
        MovFilas.Visible = True
        txtCCosto.Visible = False
        Label4.Visible = False
        File.Visible = True
        txtMovDescripcion.Text = ""
        txtMovNro.Text = ""
        txtMovNro.ReadOnly = True
        Txtfin.Text = ""
        TxtIni.Text = ""
        btnUpload.Visible = True
        BtnCargaBienes.Visible = True
        BtnCancelarMov.Visible = True
        BtnBienesNoMover.Visible = True
        BtnListaBNoMover.Visible = True
        BtnDefinirMov.Enabled = False
        BtnCargaArchivo.Visible = False
        BtnListar.Enabled = True
        Call LlenaComboItem("TBOPC550", ddlMovTipo)
        ddlMovTipo.SelectedValue = "1"

        Dim pdCodMov As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " SELECT ISNULL(MAX(GPS_MOV_NRO),0) FROM TBINVENTARIO_INTERFACE_GPS "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                pdCodMov = Nz(Rs(0)) + 1
            End While
        Else
            pdCodMov = "1"
        End If
        Rs.Close()
        txtMovNro.Text = Llenar_Ceros(pdCodMov, 3)

        txtMovDescripcion.Text = "Movimiento Nro " & Llenar_Ceros(pdCodMov, 3)
    End Sub

    Private Sub BtnCancelarMov_Click(sender As Object, e As EventArgs) Handles BtnCancelarMov.Click
        Mov.Visible = False
        Mov2.Visible = False
        MovFilas.Visible = False
        txtCCosto.Visible = False
        BtnBusca.Visible = False
        File.Visible = False
        txtMovDescripcion.Text = ""
        txtMovNro.Text = ""
        Txtfin.Text = ""
        TxtIni.Text = ""
        txtCCosto.Text = ""
        TxtDescripcion.Text = ""
        TxtCodigo.Text = ""
        btnUpload.Visible = False
        BtnCargaBienes.Visible = False
        BtnListaBNoMover.Visible = False
        BtnBienesNoMover.Visible = False
        accordion.Visible = False
        BtnCancelarMov.Visible = False
        BtnDefinirMov.Enabled = True
        BtnListar.Enabled = True
        BtnListar_Click(sender, e)
    End Sub

    Private Sub BtnCargarInvOk_Click(sender As Object, e As EventArgs) Handles BtnCargarInvOk.Click

        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Dim pdMovNro As Double = 0
        Dim pdCodUbicaInv As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
        psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        If TxtFechaFin.Text = "" Then
            psFechaFin = psFecha
        Else
            psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
        End If
        Try
            If ddlMovTipo.SelectedValue = "2" Then
                If lblCodCCostos.Text = "" Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Centro de Costo.');", True)
                GoTo NuevoMov
            Else

NuevoMov:
                Dim psNuevoMov As String = ""

                pdMovNro = Nz(txtMovNro.Text)

                CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_INTERFACE_GPS where GPS_MOV_NRO = " & pdMovNro
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psNuevoMov = "No"
                    End While
                Else
                    psNuevoMov = "Si"
                End If
                Rs.Close()
                If psNuevoMov = "Si" Then
                    CmdGlobal.CommandText = " insert into TBINVENTARIO_INTERFACE_GPS (GPS_MOV_NRO, GPS_MOV_TIPO, GPS_MOV_DESCRIPCION, GPS_MOV_FECHA, GPS_MOV_HORA, " _
                                      & " GPS_MOV_USER, GPS_MOV_ESTADO,  GPS_MOV_SYS_EST, GPS_MOV_SYS_CRE) " _
                                      & " VALUES (" & pdMovNro & ", '" & ddlMovTipo.SelectedValue & "', '" & txtMovDescripcion.Text & "', '" & FechaActual() & "', '" & HoraActual() & "', " _
                                      & " '" & Session("User") & "', '1','0','" & Session("User") & FechaActual() & HoraActual() & "')  "
                    CmdGlobal.ExecuteNonQuery()
                End If

                If ddlMovTipo.SelectedValue = "2" Then
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS SET GPS_MOV_CECOSE  = " & lblCodCCostos.Text & ", " _
                                          & " GPS_MOV_CECOSE_CODINTERNO = '" & TxtCodigo.Text & "' , GPS_MOV_CECOSE_DESCRIPCION = '" & TxtDescripcion.Text & "' " _
                                          & " WHERE GPS_MOV_NRO = " & pdMovNro
                    CmdGlobal.ExecuteNonQuery()
                End If

                obj.Inventario_InterfaceGPS_CargaTabla_BienesOk(Session("Ruta_Emp"), Session("User"), FechaActual, HoraActual, pdMovNro, pdCodUbicaInv, psFecha, psFechaFin)

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de los Bienes Inventariado Ok.');", True)

                BtnListar_Click(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnListar_Click(sender As Object, e As EventArgs) Handles BtnListar.Click
        LblRegistro2.Text = ""
        'lblError.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Dim pdMovNro As Double = 0
        Lista.Visible = True
        Dim psCodInv As Double = 0

        Dim codigo As String = ""
        Dim psconexion As String = Session("Ruta_Emp")
        dt = Nothing
        Try
            dt = obj.Lista_Movimiento_InterfaceGps(Session("Ruta_Emp"))
            gvListaMoviimentos.DataSource = dt
            gvListaMoviimentos.DataBind()
            gvListaGps.DataSource = Nothing
            gvListaGps.DataBind()
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub gvListaMoviimentos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvListaMoviimentos.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        LblRegistro2.Text = ""
        'lblError.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Dim pdMovNro As Double = 0
        Dim psTipoMov As String = ""

        Dim psCodInv As Double = 0

        Dim codigo As String = ""
        If e.CommandName = "Detalle" Then
            pdMovNro = gvListaMoviimentos.Rows(Index).Cells(2).Text
            If gvListaMoviimentos.Rows(Index).Cells(3).Text = "SOBRANTES" Then
                psTipoMov = "2"
            ElseIf gvListaMoviimentos.Rows(Index).Cells(3).Text = "Bienes a Mover" Then
                psTipoMov = "3"
            Else
                psTipoMov = "1"
            End If
            dt = obj.Lista_Bienes_InterfaceGps(Session("Ruta_Emp"), 0, 0, pdMovNro, psTipoMov)
            gvListaGps.DataSource = dt
            gvListaGps.DataBind()
            Lista.Visible = True

            If dt.Rows.Count > 1 Then
                LblRegistro2.Text = "Hay " & dt.Rows.Count & " registros para mover."
            ElseIf dt.Rows.Count = 1 Then
                LblRegistro2.Text = "Hay 1 registro para mover."
            ElseIf dt.Rows.Count = 0 Then
                LblRegistro2.Text = "No hay registro para mover."
            End If
            dt = Nothing
            ListaPlacasNoMover(pdMovNro)
        End If
        If e.CommandName = "Editar" Then
            pdMovNro = gvListaMoviimentos.Rows(Index).Cells(2).Text

            Call LlenaComboItem("TBOPC550", ddlMovTipo)
            Mov.Visible = True
            Mov2.Visible = True
            MovFilas.Visible = True
            File.Visible = True
            Label4.Visible = False
            BtnUpload.Visible = True
            BtnCargaArchivo.Visible = False
            BtnCargaBienes.Visible = True
            BtnCancelarMov.Visible = True
            BtnDefinirMov.Enabled = False
            BtnBienesNoMover.Visible = True
            BtnListaBNoMover.Visible = True
            BtnListar.Enabled = True
            dt = obj.Lista_MovimientoGPS_xNro(Session("Ruta_Emp"), pdMovNro)
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    txtMovDescripcion.Text = Nu(dr("GPS_MOV_DESCRIPCION"))
                    txtMovNro.Text = Nu(dr("GPS_MOV_NRO"))
                    ddlMovTipo.SelectedValue = Nu(dr("GPS_MOV_TIPO"))
                    If ddlMovTipo.SelectedValue = "2" Then
                        txtCCosto.Visible = True
                        Label4.Visible = True
                        BtnBusca.Visible = True
                        txtCCosto.Text = Nu(dr("GPS_MOV_CECOSE_CODINTERNO")) & " - " & Nu(dr("GPS_MOV_CECOSE_DESCRIPCION"))
                        TxtCodigo.Text = Nu(dr("GPS_MOV_CECOSE_CODINTERNO"))
                        TxtDescripcion.Text = Nu(dr("GPS_MOV_CECOSE_DESCRIPCION"))
                        lblCodCCostos.Text = Nu(dr("GPS_MOV_CECOSE"))
                    End If
                    txtMovNro.ReadOnly = True
                    Txtfin.Text = ""
                    TxtIni.Text = ""
                    If DdlMovTipo.SelectedValue = "3" Then
                        BtnCargaArchivo.Visible = True
                    End If
                Next
            End If
        End If

    End Sub
    Private Sub BtnGenerar_Click(sender As Object, e As EventArgs) Handles BtnGenerar.Click
        Call GenerarGps_Datos501_Mob()
    End Sub
    Private Sub GenerarGps_Datos501_Mob()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Invenatrio/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        Dim psCodInv_Ubica As Double = 0
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim pdRegistro As Double = 0
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        Try

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsInterface_Generar501_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)

            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "501_" & txtMovDescripcion.Text & "_MOB.txt"
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
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try

    End Sub

    Private Sub BtnGenerarInf_Click(sender As Object, e As EventArgs) Handles BtnGenerarInf.Click

        Call GenerarGps_Datos501_Inf()
    End Sub
    Private Sub GenerarGps_Datos501_Inf()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Invenatrio/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        'If DdlInventario.SelectedValue <> "< Seleccionar >" Then
        '    psCodInv = DdlInventario.SelectedValue
        'End If
        Dim psCodInv_Ubica As Double = 0
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        'psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim pdRegistro As Double = 0
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else


            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsInterface_Generar501_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)

            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "501_" & txtMovDescripcion.Text & "_INF.txt"
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
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try

    End Sub
    Private Sub Btn201Inf_Click(sender As Object, e As EventArgs) Handles Btn201Inf.Click
        Call GenerarGps_Datos201_Inf()
    End Sub
    Private Sub GenerarGps_Datos201_Inf()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Inventario/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim dt2 As New DataTable
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        'psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim pdRegistro As Double = 0
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else
            Dim pdSIAAF As Double = 0

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsInterface_Generar201_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)
            Dim psCaract As String = ""
            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "201_BIENES_INF.txt"
            Dim fullPath As String = Path.Combine(savePath, fileName)
            Dim pfila As Long = 0
            pfila = 1001
            If dt1.Rows.Count > 0 Then
                Using fileWriter As New System.IO.StreamWriter(fullPath)
                    For Each dr As DataRow In dt1.Rows
                        pdRegistro = pdRegistro + 1
                        psCaract = ""
                        dt2 = objdatos.BuscarCaracteristica_xCecoseInterno(Session("Ruta_Emp"), Nu(dr("CCosto_NroSerie")).ToString())
                        For Each dr2 As DataRow In dt2.Rows
                            psCaract = Nu(dr2("CARACTERISTICA"))
                        Next

                        If Nu(dr("Fecha_Almacen")) <> FormatoFecha(FechaActual) Then
                            fileWriter.Write(pfila & vbTab & "1" & vbTab & "201" & vbTab & "" & vbTab _
                                                & psFechaMov & vbTab & "" & vbTab & "                " & vbTab & pdSIAAF & vbTab _
                                                & Nu(dr("CCosto_NroSerie")).ToString() & vbTab & vbTab & vbTab & "PE")
                            fileWriter.WriteLine()
                            fileWriter.Write(pfila & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                                & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                                & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                                & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                                & "" & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                                & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                                & Nu(dr("Texto")).ToString() & vbTab & psCaract & vbTab)
                            fileWriter.WriteLine()
                            pfila = pfila + 1
                        End If
                    Next
                End Using
            End If

            Response.Clear()
            Response.ContentType = "application/txt"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName)
            Response.TransmitFile(fullPath)
            Response.End()
            ''End If


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try

    End Sub
    Private Sub Btn201Mob_Click(sender As Object, e As EventArgs) Handles Btn201Mob.Click
        GenerarGps_Datos201_Mob()
    End Sub
    Private Sub GenerarGps_Datos201_Mob()

        ' Ruta donde se guardará el archivo TXT
        Dim rutaArchivo As String = Server.MapPath("~/Inventario/ArchivoGenerado.txt")

        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        'If DdlInventario.SelectedValue <> "< Seleccionar >" Then
        '    psCodInv = DdlInventario.SelectedValue
        'End If
        Dim psCodInv_Ubica As Double = 0
        'psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
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
            dt1 = objdatos.GpsInterface_Generar201_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)

            Dim savePath As String = Server.MapPath("~/Inventario/Informe/")
            Dim fileName As String = "201_BIENES_MOB.txt"
            Dim fullPath As String = Path.Combine(savePath, fileName)
            Dim pfila As Long = 0
            pfila = 1001
            If dt1.Rows.Count > 0 Then
                Using fileWriter As New System.IO.StreamWriter(fullPath)
                    For Each dr As DataRow In dt1.Rows
                        pdRegistro = pdRegistro + 1
                        psCaract = ""
                        dt2 = objdatos.BuscarCaracteristica_xCecoseInterno(Session("Ruta_Emp"), Nu(dr("CCosto_NroSerie")).ToString())
                        For Each dr2 As DataRow In dt2.Rows
                            psCaract = Nu(dr2("CARACTERISTICA"))
                        Next

                        If Nu(dr("Fecha_Almacen")) <> FormatoFecha(FechaActual) Then
                            fileWriter.Write(pfila & vbTab & "1" & vbTab & "201" & vbTab & "" & vbTab _
                                                & psFechaMov & vbTab & "" & vbTab & "                " & vbTab & pdSIAAF & vbTab _
                                                & Nu(dr("CCosto_NroSerie")).ToString() & vbTab & vbTab & vbTab & "PE")
                            fileWriter.WriteLine()
                            fileWriter.Write(pfila & vbTab & Nu(dr("Tipo_Registro")).ToString() & vbTab _
                                                & Nu(dr("TipoMov_CodMaterial")).ToString() & vbTab & Nu(dr("TipoStock_Centro")).ToString() & vbTab _
                                                & Nu(dr("Fecha_Almacen")).ToString() & vbTab & Nu(dr("NroPedCompr_PosPedCompra")).ToString() & vbTab _
                                                & Nu(dr("NroNota_TipoMotivo")).ToString() & vbTab & Nu(dr("SIAAF_Equipo")).ToString() & vbTab _
                                                & "" & vbTab & Nu(dr("Pais_Lote")).ToString() & vbTab _
                                                & Nu(dr("ActivoFijo")).ToString() & vbTab & Nu(dr("SubNro_ActvoFijo")).ToString() & vbTab _
                                                & Nu(dr("Texto")).ToString() & vbTab & psCaract & vbTab)
                            fileWriter.WriteLine()
                            pfila = pfila + 1
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
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try

    End Sub
    Private Sub ExportarDatosGps_201Inf()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim drT As DataRow
        Dim psCodInv As Double = 0
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        'psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim psCaract As String = ""
        Dim dt2 As New DataTable
        Try

            Dim dt As New DataTable()
            dt.Columns.Add("Numeral", GetType(String))
            dt.Columns.Add("Tipo_Registro", GetType(String))
            dt.Columns.Add("TipoMov_CodMaterial", GetType(String))
            dt.Columns.Add("TipoStock_Centro", GetType(String))
            dt.Columns.Add("Fecha_Almacen", GetType(String))
            dt.Columns.Add("NroPedCompr_PosPedCompra", GetType(String))
            dt.Columns.Add("NroNota_TipoMotivo", GetType(String))
            dt.Columns.Add("SIAAF_Equipo", GetType(String))
            dt.Columns.Add("CCosto_NroSerie", GetType(String))
            dt.Columns.Add("Pais_Lote", GetType(String))
            dt.Columns.Add("ActivoFijo", GetType(String))
            dt.Columns.Add("SubNro_ActvoFijo", GetType(String))
            dt.Columns.Add("Texto", GetType(String))
            dt.Columns.Add("Caract", GetType(String))
            Dim pfila As Long = 1001

            Dim pdSIAAF As Double = 0
            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsInterface_Generar201_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)
            If dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    psCaract = ""
                    dt2 = objdatos.BuscarCaracteristica_xCecoseInterno(Session("Ruta_Emp"), Nu(dr("CCosto_NroSerie")).ToString())
                    For Each dr2 As DataRow In dt2.Rows
                        psCaract = Nu(dr2("CARACTERISTICA"))
                    Next

                    drT = dt.NewRow()
                    drT("Numeral") = pfila
                    drT("Tipo_Registro") = "1"
                    drT("TipoMov_CodMaterial") = "201"
                    drT("TipoStock_Centro") = ""
                    drT("Fecha_Almacen") = psFechaMov
                    drT("NroPedCompr_PosPedCompra") = ""
                    drT("NroNota_TipoMotivo") = ""
                    drT("SIAAF_Equipo") = pdSIAAF
                    drT("CCosto_NroSerie") = Nu(dr("CCosto_NroSerie")).ToString()
                    drT("Pais_Lote") = "PE"
                    drT("ActivoFijo") = ""
                    drT("SubNro_ActvoFijo") = ""
                    drT("Texto") = ""
                    drT("Caract") = ""
                    dt.Rows.Add(drT)

                    If Nu(dr("Fecha_Almacen")) <> FormatoFecha(FechaActual) Then
                        drT = dt.NewRow()
                        drT("Numeral") = pfila
                        drT("Tipo_Registro") = Nu(dr("Tipo_Registro")).ToString()
                        drT("TipoMov_CodMaterial") = Nu(dr("TipoMov_CodMaterial")).ToString()
                        drT("TipoStock_Centro") = Nu(dr("TipoStock_Centro")).ToString()
                        drT("Fecha_Almacen") = Nu(dr("Fecha_Almacen")).ToString()
                        drT("NroPedCompr_PosPedCompra") = Nu(dr("NroPedCompr_PosPedCompra")).ToString()
                        drT("NroNota_TipoMotivo") = Nu(dr("NroNota_TipoMotivo")).ToString()
                        drT("SIAAF_Equipo") = Nu(dr("SIAAF_Equipo")).ToString()
                        drT("CCosto_NroSerie") = ""
                        drT("Pais_Lote") = Nu(dr("Pais_Lote")).ToString()
                        drT("ActivoFijo") = Nu(dr("ActivoFijo")).ToString()
                        drT("SubNro_ActvoFijo") = Nu(dr("SubNro_ActvoFijo")).ToString()
                        drT("Texto") = Nu(dr("Texto")).ToString()
                        drT("Caract") = psCaract
                        dt.Rows.Add(drT)
                        pfila = pfila + 1
                    End If
                Next
            End If
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("201_MOV_Inf")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=201_Inf_" & "Movimiento_Nro_" & Nz(TxtMovNro.Text) & ".xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()

            End Using


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)

        End Try
    End Sub
    Private Sub ExportarDatosGps_201Mob()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        Dim drT As DataRow
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        'If DdlInventario.SelectedValue <> "< Seleccionar >" Then
        '    psCodInv = DdlInventario.SelectedValue
        'End If
        Dim psCodInv_Ubica As Double = 0
        'psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim psCaract As String = ""
        Dim dt2 As New DataTable
        Try

            Dim dt As New DataTable()
            dt.Columns.Add("Numeral", GetType(String))
            dt.Columns.Add("Tipo_Registro", GetType(String))
            dt.Columns.Add("TipoMov_CodMaterial", GetType(String))
            dt.Columns.Add("TipoStock_Centro", GetType(String))
            dt.Columns.Add("Fecha_Almacen", GetType(String))
            dt.Columns.Add("NroPedCompr_PosPedCompra", GetType(String))
            dt.Columns.Add("NroNota_TipoMotivo", GetType(String))
            dt.Columns.Add("SIAAF_Equipo", GetType(String))
            dt.Columns.Add("CCosto_NroSerie", GetType(String))
            dt.Columns.Add("Pais_Lote", GetType(String))
            dt.Columns.Add("ActivoFijo", GetType(String))
            dt.Columns.Add("SubNro_ActvoFijo", GetType(String))
            dt.Columns.Add("Texto", GetType(String))
            dt.Columns.Add("Caract", GetType(String))
            Dim pfila As Long = 1001

            Dim pdSIAAF As Double = 0
            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsInterface_Generar201_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)
            If dt1.Rows.Count > 0 Then
                For Each dr As DataRow In dt1.Rows
                    psCaract = ""
                    dt2 = objdatos.BuscarCaracteristica_xCecoseInterno(Session("Ruta_Emp"), Nu(dr("CCosto_NroSerie")).ToString())
                    For Each dr2 As DataRow In dt2.Rows
                        psCaract = Nu(dr2("CARACTERISTICA"))
                    Next

                    drT = dt.NewRow()
                    drT("Numeral") = pfila
                    drT("Tipo_Registro") = "1"
                    drT("TipoMov_CodMaterial") = "201"
                    drT("TipoStock_Centro") = ""
                    drT("Fecha_Almacen") = psFechaMov
                    drT("NroPedCompr_PosPedCompra") = ""
                    drT("NroNota_TipoMotivo") = ""
                    drT("SIAAF_Equipo") = pdSIAAF
                    drT("CCosto_NroSerie") = Nu(dr("CCosto_NroSerie")).ToString()
                    drT("Pais_Lote") = "PE"
                    drT("ActivoFijo") = ""
                    drT("SubNro_ActvoFijo") = ""
                    drT("Texto") = ""
                    drT("Caract") = ""
                    dt.Rows.Add(drT)

                    If Nu(dr("Fecha_Almacen")) <> FormatoFecha(FechaActual) Then
                        drT = dt.NewRow()
                        drT("Numeral") = pfila
                        drT("Tipo_Registro") = Nu(dr("Tipo_Registro")).ToString()
                        drT("TipoMov_CodMaterial") = Nu(dr("TipoMov_CodMaterial")).ToString()
                        drT("TipoStock_Centro") = Nu(dr("TipoStock_Centro")).ToString()
                        drT("Fecha_Almacen") = Nu(dr("Fecha_Almacen")).ToString()
                        drT("NroPedCompr_PosPedCompra") = Nu(dr("NroPedCompr_PosPedCompra")).ToString()
                        drT("NroNota_TipoMotivo") = Nu(dr("NroNota_TipoMotivo")).ToString()
                        drT("SIAAF_Equipo") = Nu(dr("SIAAF_Equipo")).ToString()
                        drT("CCosto_NroSerie") = ""
                        drT("Pais_Lote") = Nu(dr("Pais_Lote")).ToString()
                        drT("ActivoFijo") = Nu(dr("ActivoFijo")).ToString()
                        drT("SubNro_ActvoFijo") = Nu(dr("SubNro_ActvoFijo")).ToString()
                        drT("Texto") = Nu(dr("Texto")).ToString()
                        drT("Caract") = psCaract

                        dt.Rows.Add(drT)
                        pfila = pfila + 1
                    End If
                Next
            End If
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("201_MOV_MOB")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=201_MOB_" & "Movimiento_Nro_" & Nz(TxtMovNro.Text) & ".xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()

            End Using


        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)

        End Try
    End Sub


    Private Sub BtnExcelInf_Click(sender As Object, e As EventArgs) Handles BtnExcelInf.Click
        Call ExportarDatosGps_501Inf()
    End Sub
    Private Sub ExportarDatosGps_501Inf()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        'If DdlInventario.SelectedValue <> "< Seleccionar >" Then
        '    psCodInv = DdlInventario.SelectedValue
        'End If
        Dim psCodInv_Ubica As Double = 0
        'psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        Try
            'If psCodInv_Ubica = 0 Then
            '    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar Ubicacion.');", True)
            'Else

            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsInterface_Generar501_INF(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("501_MOV_INF")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=501_INF_" & "Movimiento_Nro_" & Nz(TxtMovNro.Text) & ".xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()

            End Using
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)

        End Try
    End Sub

    Private Sub BtnExcelMob_Click(sender As Object, e As EventArgs) Handles BtnExcelMob.Click
        Call ExportarDatosGps_501Mob()
    End Sub

    Private Sub ExportarDatosGps_501Mob()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0
        'If DdlInventario.SelectedValue <> "< Seleccionar >" Then
        '    psCodInv = DdlInventario.SelectedValue
        'End If
        Dim psTipoMov As String = ""
        If ddlMovTipo.SelectedValue <> "< Seleccionar >" Then
            psTipoMov = ddlMovTipo.SelectedValue
        End If
        Dim psCodInv_Ubica As Double = 0
        'psCodInv_Ubica = Nz(LblUbicaCodigoInv.Text.ToString)
        ' Configurar los datos en dt1 y dt2...
        Dim psFechaMov As String = ""
        Try
            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.GpsInterface_Generar501_MOB(Session("Ruta_Emp"), psCodInv, psCodInv_Ubica, psFechaMov, Nz(txtMovNro.Text), psTipoMov)

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("501_MOV_MOB")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=501_Mob_" & "Movimiento_Nro_" & Nz(TxtMovNro.Text) & ".xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using
            'End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub ddlMovTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMovTipo.SelectedIndexChanged
        If ddlMovTipo.SelectedValue = "2" Then
            txtCCosto.Visible = True
            BtnBusca.Visible = True
            Label4.Visible = True
            txtCCosto.Text = ""
            TxtCodigo.Text = ""
            TxtDescripcion.Text = ""
            lblCodCCostos.Text = ""
            BtnCargaArchivo.Visible = False
        ElseIf ddlMovTipo.SelectedValue = "3" Then
            BtnBusca.Visible = False
            txtCCosto.Visible = False
            Label4.Visible = False
            txtCCosto.Text = ""
            TxtCodigo.Text = ""
            TxtDescripcion.Text = ""
            lblCodCCostos.Text = ""
            BtnCargaArchivo.Visible = True
        Else
            BtnBusca.Visible = False
            txtCCosto.Visible = False
            Label4.Visible = False
            txtCCosto.Text = ""
            TxtCodigo.Text = ""
            TxtDescripcion.Text = ""
            lblCodCCostos.Text = ""
            BtnCargaArchivo.Visible = False
        End If
    End Sub

    Private Sub BtnBusca_Click(sender As Object, e As EventArgs) Handles BtnBusca.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('show');", True)
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles BtnBuscar.Click
        Dim obj As New clsLogis_Listado
        Dim dt As New DataTable
        Dim dtU As New DataTable
        Dim dtM As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim inventario As Double = 0
        Dim codigo As Double = 0
        Dim descripcion As String = BuscarDescripcion.Value.ToString
        Dim psCodInterno As String = BuscarCodigo.Value.ToString

        dt = obj.ListaTodo_Centro_Costos(Session("Ruta_Emp"), Session("CodEmpresa"), psCodInterno, descripcion)

        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub
    Private Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" Then
            txtCCosto.Text = GvBusqueda.Rows(Index).Cells(1).Text & " - " & GvBusqueda.Rows(Index).Cells(2).Text
            TxtCodigo.Text = GvBusqueda.Rows(Index).Cells(1).Text
            TxtDescripcion.Text = GvBusqueda.Rows(Index).Cells(2).Text
            lblCodCCostos.Text = GvBusqueda.Rows(Index).Cells(3).Text
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)
        End If

        Limpiar_Cajas_Popup()
    End Sub

    Private Sub Limpiar_Cajas_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
    End Sub

    Private Sub BtnCerrar_Click(sender As Object, e As EventArgs) Handles BtnCerrar.Click

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#Modal').modal('hide');", True)

        Limpiar_Cajas_Popup()
    End Sub

    Private Sub btn201InfExcel_Click(sender As Object, e As EventArgs) Handles btn201InfExcel.Click
        ExportarDatosGps_201Inf()
    End Sub

    Private Sub btn201MobExcel_Click(sender As Object, e As EventArgs) Handles btn201MobExcel.Click
        ExportarDatosGps_201Mob()
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
            dt.Columns.Add("GPSDET_CENTRO_COSTO", GetType(String))
            dt.Columns.Add("GPSDET_CENTRO_COSTO_FINAL", GetType(String))
            dt.Columns.Add("GPSDET_SERIE_EQUIPO", GetType(String))
            dt.Columns.Add("GPSDET_PLACA_NRO", GetType(String))
            dt.Columns.Add("GPSDET_SERIE_NRO", GetType(String))
            dt.Columns.Add("GPSDET_MATERIAL", GetType(String))
            dt.Columns.Add("GPSDET_DESCRIPCION", GetType(String))
            dt.Columns.Add("Fecha_Mov", GetType(String))
            dt.Columns.Add("GPSDET_ALMACEN", GetType(String))
            dt.Columns.Add("GPSDET_STATUS_USU", GetType(String))
            dt.Columns.Add("GPSDET_TIPO_EQUIPO", GetType(String))
            dt.Columns.Add("GPSDET_MOV501", GetType(String))
            dt.Columns.Add("Fecha_Mov501", GetType(String))
            dt.Columns.Add("GPSDET_MOV201", GetType(String))
            dt.Columns.Add("Fecha_Mov201", GetType(String))
            dt.Columns.Add("Estado", GetType(String))

            If gvListaGps.Rows.Count > 0 Then
                For iRow = 0 To gvListaGps.Rows.Count - 1
                    drT = dt.NewRow()
                    drT("GPSDET_CENTRO_COSTO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(0).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_CENTRO_COSTO_FINAL") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(1).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_SERIE_EQUIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(2).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_PLACA_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(3).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_SERIE_NRO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(4).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_MATERIAL") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(5).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(6).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("Fecha_Mov") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(7).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_ALMACEN") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(8).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_STATUS_USU") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(9).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_TIPO_EQUIPO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(10).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_MOV501") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(11).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("Fecha_Mov501") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(12).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("GPSDET_MOV201") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(13).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("Fecha_Mov201") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(14).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    drT("Estado") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(gvListaGps.Rows(iRow).Cells(15).Text, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
                    dt.Rows.Add(drT)
                Next
                Using excelPackage As New ExcelPackage()
                    ' Agregar hojas al archivo de Excel
                    Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Bienes_xMover")

                    ' Llenar Hoja1 con los datos de dt1
                    worksheet1.Cells("A1").LoadFromDataTable(dt, True)

                    ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                    Response.Clear()
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    Response.AddHeader("content-disposition", "attachment; filename=Bienes_xMover.xlsx")
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

    Private Sub BtnBienesNoMover_Click(sender As Object, e As EventArgs) Handles BtnBienesNoMover.Click

        If Nz(TxtIni.Text) = 0 Or Nz(Txtfin.Text) = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el Inicio y fin de las filas a cargar.');", True)
        Else

            If fileUpload.HasFile Then
                Dim excelFilePath As String = Server.MapPath("~/Inventario/ArchivoTemp/Excel.xlsx")

                ' Guardar el archivo subido
                fileUpload.SaveAs(excelFilePath)

                ' Leer datos desde Excel y cargarlos en la base de datos
                CargarPlacaNoMover(excelFilePath)

                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de archivo.');", True)
                ' Opcional: Eliminar el archivo Excel después de cargar los datos
                ' File.Delete(excelFilePath)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar archivo.');", True)
            End If

        End If
    End Sub

    Private Sub CargarPlacaNoMover(excelFilePath As String)
        Dim connectionString As String = Session("Ruta_Emp")
        Dim pdCodInv_Ubica As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        pdCodInv_Ubica = 0 ' Nz(LblUbicaCodigoInv.Text.ToString)
        Session("UnaVez") = "1"
        Dim pdNroRegistro As Double = 0
        Dim psSerieEquipo As String = ""
        Dim pdPlacaNro As String = ""

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        filaIni = TxtIni.Text
        filafin = Txtfin.Text
        Dim dt As New DataTable

        Using package As New ExcelPackage(New FileInfo(excelFilePath))
            Dim workbook As ExcelWorkbook = package.Workbook
            If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)

                CmdGlobal.CommandText = " DELETE TBINVENTARIO_INTERFACE_GPS_NOMOVER where GPS_MOV_NRO = " & Nz(TxtMovNro.Text)
                CmdGlobal.ExecuteNonQuery()

                ' Recorrer las celdas del archivo Excel
                For row As Integer = filaIni To filafin
                    psSerieEquipo = Nu(worksheet.Cells(row, 1).Value)
                    pdPlacaNro = Mid(psSerieEquipo, 12, 7)
                    pdNroRegistro = pdNroRegistro + 1
                    CmdGlobal.CommandText = " INSERT INTO [dbo].[TBINVENTARIO_INTERFACE_GPS_NOMOVER] ([GPS_MOV_NRO],[NOMOVER_CORRELATIVO],[NOMOVER_SERIE_EQUIPO],[NOMOVER_PLACA_NRO]) " _
                                          & " VALUES  (" & Nz(TxtMovNro.Text) & ", " & pdNroRegistro & ", '" & psSerieEquipo & "'," & pdPlacaNro & ") "
                    CmdGlobal.ExecuteNonQuery()
                Next
            End If
        End Using

    End Sub

    Private Sub BtnListaBNoMover_Click(sender As Object, e As EventArgs) Handles BtnListaBNoMover.Click
        lblRegistros.Text = ""
        Dim pdMovNro As Double = 0
        Try
            pdMovNro = Nz(TxtMovNro.Text)
            ListaPlacasNoMover(pdMovNro)
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub
    Private Sub ListaPlacasNoMover(ByVal pdNroMov As Double)

        lblRegistros.Text = ""
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        'Dim pdMovNro As Double = 0
        dt = Nothing
        Try
            dt = obj.Lista_PlacasNoMover_InterfaceGps(Session("Ruta_Emp"), pdNroMov)
            GvListaPlacas.DataSource = dt
            GvListaPlacas.DataBind()
            If dt.Rows.Count > 1 Then
                lblRegistros.Text = "Hay " & dt.Rows.Count & " registros."
            ElseIf dt.Rows.Count = 1 Then
                lblRegistros.Text = "Hay 1 registro."
            ElseIf dt.Rows.Count = 0 Then
                lblRegistros.Text = "No hay registros."
            End If
            accordion.Visible = True
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnLista201I_Click(sender As Object, e As EventArgs) Handles BtnLista201I.Click

    End Sub

    Private Sub BtnExportarInvOk_Click(sender As Object, e As EventArgs) Handles BtnExportarInvOk.Click
        Exportar_BienesInvOk()
    End Sub

    Private Sub Exportar_BienesInvOk()
        Dim dt1 As New DataTable()
        Dim objdatos As New Cls_Inventario
        Dim psCodInv As Double = 0


        Dim psFechaMov As String = ""
        Try
            Dim psFechaIni As String = ""
            Dim psfechafin As String = ""
            If TxtFechaIniExportar.Text <> "" Then
                psFechaIni = Right(TxtFechaIniExportar.Text, 4) & Mid(TxtFechaIniExportar.Text, 4, 2) & Left(TxtFechaIniExportar.Text, 2)
            End If
            If TxtFechaFinExportar.Text <> "" Then
                psfechafin = Right(TxtFechaFinExportar.Text, 4) & Mid(TxtFechaFinExportar.Text, 4, 2) & Left(TxtFechaFinExportar.Text, 2)
            End If
            psFechaMov = Mid(FechaActual, 7, 2) + Mid(FechaActual, 5, 2) + Mid(FechaActual, 1, 4)
            dt1 = objdatos.Inventariados_Ok(Session("Ruta_Emp"), psCodInv, psFechaIni, psfechafin)

            ' Crear el archivo de Excel
            Using excelPackage As New ExcelPackage()
                ' Agregar hojas al archivo de Excel
                Dim worksheet1 = excelPackage.Workbook.Worksheets.Add("Inventariado_Ok")

                ' Llenar Hoja1 con los datos de dt1
                worksheet1.Cells("A1").LoadFromDataTable(dt1, True)

                ' Guardar el archivo de Excel en la respuesta HTTP para descargarlo
                Response.Clear()
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Response.AddHeader("content-disposition", "attachment; filename=Inventariado_Ok.xlsx")
                Response.BinaryWrite(excelPackage.GetAsByteArray())
                Response.End()
            End Using

        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnCargarTablaMov_Click(sender As Object, e As EventArgs) Handles BtnCargarTablaMov.Click
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Dim pdMovNro As Double = 0
        Dim pdCodUbicaInv As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Dim psFecha As String = ""
        Dim psFechaFin As String = ""
        psFecha = Mid(TxtFecha.Text, 7, 4) + Mid(TxtFecha.Text, 4, 2) + Mid(TxtFecha.Text, 1, 2)
        If TxtFechaFin.Text = "" Then
            psFechaFin = psFecha
        Else
            psFechaFin = Mid(TxtFechaFin.Text, 7, 4) + Mid(TxtFechaFin.Text, 4, 2) + Mid(TxtFechaFin.Text, 1, 2)
        End If
        Try
            If DdlMovTipo.SelectedValue = "2" Then
                If lblCodCCostos.Text = "" Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Centro de Costo.');", True)
                GoTo NuevoMov
            Else

NuevoMov:
                Dim psNuevoMov As String = ""

                pdMovNro = Nz(TxtMovNro.Text)

                CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_INTERFACE_GPS where GPS_MOV_NRO = " & pdMovNro
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psNuevoMov = "No"
                    End While
                Else
                    psNuevoMov = "Si"
                End If
                Rs.Close()
                If psNuevoMov = "Si" Then
                    CmdGlobal.CommandText = " insert into TBINVENTARIO_INTERFACE_GPS (GPS_MOV_NRO, GPS_MOV_TIPO, GPS_MOV_DESCRIPCION, GPS_MOV_FECHA, GPS_MOV_HORA, " _
                                      & " GPS_MOV_USER, GPS_MOV_ESTADO,  GPS_MOV_SYS_EST, GPS_MOV_SYS_CRE) " _
                                      & " VALUES (" & pdMovNro & ", '" & DdlMovTipo.SelectedValue & "', '" & txtMovDescripcion.Text & "', '" & FechaActual() & "', '" & HoraActual() & "', " _
                                      & " '" & Session("User") & "', '1','0','" & Session("User") & FechaActual() & HoraActual() & "')  "
                    CmdGlobal.ExecuteNonQuery()
                End If

                If DdlMovTipo.SelectedValue = "2" Then
                    CmdGlobal.CommandText = " UPDATE TBINVENTARIO_INTERFACE_GPS SET GPS_MOV_CECOSE  = " & lblCodCCostos.Text & ", " _
                                          & " GPS_MOV_CECOSE_CODINTERNO = '" & TxtCodigo.Text & "' , GPS_MOV_CECOSE_DESCRIPCION = '" & TxtDescripcion.Text & "' " _
                                          & " WHERE GPS_MOV_NRO = " & pdMovNro
                    CmdGlobal.ExecuteNonQuery()
                End If

                obj.InterfaceGps_CargaTablaMov(Session("Ruta_Emp"), Session("User"), FechaActual, HoraActual, pdMovNro, pdCodUbicaInv, psFecha, psFechaFin)


                ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de los Bienes Inventariado Ok.');", True)

                BtnListar_Click(sender, e)
            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub BtnCargaArchivo_Click(sender As Object, e As EventArgs) Handles BtnCargaArchivo.Click
        Dim obj As New Cls_Inventario
        Dim dt As New DataTable
        Dim pdMovNro As Double = 0
        Dim pdCodUbicaInv As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Try
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de los Bienes Inventariado Ok.');", True)
            If DdlMovTipo.SelectedValue = "2" Then
                If lblCodCCostos.Text = "" Then ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar Centro de Costo.');", True)
            Else

                If Nz(TxtIni.Text) = 0 Or Nz(Txtfin.Text) = 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ingresar el Inicio y fin de las filas a cargar.');", True)
                Else

                    If fileUpload.HasFile Then
                        Dim psNuevoMov As String = ""
                        pdMovNro = Nz(TxtMovNro.Text)
                        CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_INTERFACE_GPS where GPS_MOV_NRO = " & pdMovNro
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                psNuevoMov = "No"
                            End While
                        Else
                            psNuevoMov = "Si"
                        End If
                        Rs.Close()
                        If psNuevoMov = "Si" Then
                            CmdGlobal.CommandText = " insert into TBINVENTARIO_INTERFACE_GPS (GPS_MOV_NRO, GPS_MOV_TIPO, GPS_MOV_DESCRIPCION, GPS_MOV_FECHA, GPS_MOV_HORA, " _
                                      & " GPS_MOV_USER, GPS_MOV_ESTADO,  GPS_MOV_SYS_EST, GPS_MOV_SYS_CRE) " _
                                      & " VALUES (" & pdMovNro & ", '3', '" & txtMovDescripcion.Text & "', '" & FechaActual() & "', '" & HoraActual() & "', " _
                                      & " '" & Session("User") & "', '1','0','" & Session("User") & FechaActual() & HoraActual() & "')  "
                            CmdGlobal.ExecuteNonQuery()
                        End If

                        Dim excelFilePath As String = Server.MapPath("~/Inventario/ArchivoTemp/Excel.xlsx")

                        ' Guardar el archivo subido
                        fileUpload.SaveAs(excelFilePath)

                        ' Leer datos desde Excel y cargarlos en la base de datos
                        CargaBienesAMover(excelFilePath)

                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Termino la carga de archivo.');", True)
                        BtnListar_Click(sender, e)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Seleccionar archivo.');", True)
                    End If

                End If

            End If
        Catch ex As SqlException
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la base de datos: " & ex.Message & ".');", True)
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "alert('Ha ocurrido un error en la aplicación: " & ex.Message & ".');", True)
        End Try
    End Sub

    Private Sub CargaBienesAMover(excelFilePath As String)

        Dim connectionString As String = Session("Ruta_Emp")
        Dim pdCodInv_Ubica As Double = 0
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        pdCodInv_Ubica = 0 ' Nz(LblUbicaCodigoInv.Text.ToString)
        Session("UnaVez") = "1"
        Dim pdNroRegistro As Double = 0
        Dim psSerieEquipo As String = ""
        Dim pdPlacaNro As String = "NULL"
        Dim psCCosto_Actual As String = ""
        Dim psCCosto_Final As String = ""
        Dim psMaterial As String = ""
        Dim psSerieNro As String = ""
        Dim psEquipoDescripcion As String = ""
        Dim Valorsys As String = ""
        Dim psFechaMov As String = ""
        Valorsys = Session("User") & FechaActual() & HoraActual()

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        filaIni = TxtIni.Text
        filafin = Txtfin.Text
        Dim dt As New DataTable

        Using package As New ExcelPackage(New FileInfo(excelFilePath))
            Dim workbook As ExcelWorkbook = package.Workbook
            If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)

                'estado 0 =carga,1=nada,2=mov501,3=mov201
                ' Recorrer las celdas del archivo Excel
                For row As Integer = filaIni To filafin
                    psCCosto_Actual = Nu(worksheet.Cells(row, 1).Value)
                    psCCosto_Final = Nu(worksheet.Cells(row, 2).Value)
                    psSerieEquipo = Nu(worksheet.Cells(row, 3).Value)
                    pdPlacaNro = Mid(psSerieEquipo, 12, 7)
                    psSerieNro = Nu(worksheet.Cells(row, 5).Value)
                    psMaterial = Nu(worksheet.Cells(row, 6).Value)
                    psEquipoDescripcion = Nu(worksheet.Cells(row, 7).Value)
                    psFechaMov = Nu(worksheet.Cells(row, 8).Value)
                    CmdGlobal.CommandText = " insert into TBINVENTARIO_INTERFACE_GPS_DETALLE (GPS_MOV_NRO, GPSDET_CENTRO_COSTO, GPSDET_CENTRO_COSTO_FINAL, " _
                                              & " GPSDET_SERIE_EQUIPO, GPSDET_PLACA_NRO, GPSDET_SERIE_NRO, GPSDET_MATERIAL,  GPSDET_DESCRIPCION, " _
                                              & " GPSDET_FECHA_CARGA, GPSDET_ESTADO, GPSDET_SYS_EST, GPSDET_SYS_CRE,GPSDET_FECHA_MOV,GPSDET_MOV501) " _
                                              & " VALUES (" & Nz(TxtMovNro.Text) & ",'" & psCCosto_Actual & "' ,'" & psCCosto_Final & "',  " & psSerieEquipo & ", " & pdPlacaNro & ", '" & psSerieNro & "', " _
                                              & " '" & psMaterial & "','" & psEquipoDescripcion & "' , '" & FechaActual() & "' ,'0', '0', '" & Valorsys & "', '" & psFechaMov & "','X' )  "
                    CmdGlobal.ExecuteNonQuery()
                Next
            End If
        End Using
    End Sub

End Class
