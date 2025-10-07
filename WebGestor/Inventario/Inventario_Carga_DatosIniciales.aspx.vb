Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Imports OfficeOpenXml
Imports DataTable = System.Data.DataTable
Partial Class Inventario_Inventario_Carga_DatosIniciales
    Inherits System.Web.UI.Page
    Dim oFuncInv As New clsInv_Procesos

    Dim CodSalida As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New Cls_Inventario_Verificacion
            Dim objC As New Cls_Catalogo
            Dim objCn As New Cls_Conexion
        End If
    End Sub

    Private Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click
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
        Dim Cn3 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal3 As New SqlCommand
        Cn.Close()
        Cn2.Close()
        Cn3.Close()
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim RsRecep As SqlDataReader
        pdCodInv_Ubica = 0 ' Nz(LblUbicaCodigoInv.Text.ToString)
        Session("UnaVez") = "1"

        Dim Valorsys As String = Session("User") & FechaActual() & HoraActual()
        Dim psProveedor As String = ""
        Dim psTipoMov As String = ""
        Dim psCodArt As Double = 0

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        filaIni = TxtIni.Text
        filafin = Txtfin.Text
        Dim dt As New DataTable
        Dim valor As String = ""
        Dim psEstado As String = ""
        Dim objGps As New Cls_Inventario
        Dim psCodRecepcion As Double
        Dim StockAc As Double = 0
        Dim pdNroMovimiento As Double = 0

        Dim pdCantBien As Double = 0
        Using package As New ExcelPackage(New FileInfo(excelFilePath))
            Dim workbook As ExcelWorkbook = package.Workbook
            If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)


                CmdGlobal.CommandText = "SELECT MAX(ISNULL(RECEP_CODIGO,0)) FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psCodRecepcion = Nz(Rs(0)) + 1
                    End While
                Else
                    psCodRecepcion = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,  ALTIBI_CODIGO, RECEP_PROYECTO, RECEP_FECHA_REC , RECEP_HORA_REC, RECEP_TIPODOC,  " _
                                  & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, " _
                                  & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO, RECEP_CREADO_DESDE) " _
                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodRecepcion & ",1, '1','1', '" & FechaActual() & "', '" & HoraActual() & "', '7', " _
                                  & " '" & FechaActual() & "','" & FechaActual() & "','" & HoraActual() & "','" & Session("User") & "',''," & pdCantBien & ",'2'," _
                                  & " '0','" & Valorsys & "',1,1,0,0,'N','20','','1', '', '1','I')"
                CmdGlobal.ExecuteNonQuery()

                Dim pdCosReg As Double = 0
                Dim pdNroReg As String = ""
                Dim psNombreOficina As String = ""
                Dim psClase As String = ""
                Dim psfamilia As String = ""
                Dim psCodinv As String = ""
                Dim psCiudad As String = ""
                Dim psDistrito As String = ""
                Dim psAgencia As String = ""
                Dim psNombreComercial As String = ""
                Dim psUsuario As String = ""
                Dim psDni As String = ""
                Dim psCargo As String = ""
                Dim psCECO As String = ""
                Dim psArea As String = ""
                Dim psRazonSocial As String = ""
                Dim psActivofijo As String = ""
                Dim psTipoEquipo As String = ""
                Dim psModelo As String = ""
                Dim psMarca As String = ""
                Dim psSerieNro As String = ""
                Dim psHostName As String = ""
                Dim psCodMarca As Double = 0
                Dim psCodModelo As Double = 0
                Dim SerieNum As Double = 0
                Dim psCodCECosto As Double = 0
                Dim psCodSeccion As Double = 0
                Dim psCecoseInterno As String = ""
                Dim psCecoseInternoCod As Double = 0
                Dim psCostoInterno As String = ""
                Dim pdRecepItem As Double = 0
                Dim pdCodReg2 As Double = 0
                Dim pdSerieCorrelativo As Double = 1
                Dim psExcelCodArt As String = ""
                Dim psExcelInternoSeccion As String = ""
                ' Recorrer las celdas del archivo Excel
                For row As Integer = filaIni To filafin
                    psCodSeccion = 0
                    psCodCECosto = 0
                    pdNroReg = Nz(worksheet.Cells(row, 1).Value)
                    psNombreOficina = QuitaComilla(Nu(worksheet.Cells(row, 2).Value))
                    psClase = QuitaComilla(Nu(worksheet.Cells(row, 3).Value))
                    psfamilia = QuitaComilla(Nu(worksheet.Cells(row, 4).Value))
                    psCodinv = QuitaComilla(Nu(worksheet.Cells(row, 5).Value))
                    psCiudad = QuitaComilla(Nu(worksheet.Cells(row, 6).Value))
                    psDistrito = QuitaComilla(Nu(worksheet.Cells(row, 7).Value))
                    psAgencia = QuitaComilla(Nu(worksheet.Cells(row, 8).Value))
                    psNombreComercial = QuitaComilla(Nu(worksheet.Cells(row, 9).Value))
                    psUsuario = QuitaComilla(Nu(worksheet.Cells(row, 10).Value))
                    psDni = QuitaComilla(Nu(worksheet.Cells(row, 11).Value))
                    psCargo = QuitaComilla(Nu(worksheet.Cells(row, 12).Value))
                    psCECO = QuitaComilla(Nu(worksheet.Cells(row, 13).Value))
                    psArea = QuitaComilla(Nu(worksheet.Cells(row, 14).Value))
                    psRazonSocial = QuitaComilla(Nu(worksheet.Cells(row, 15).Value))
                    psActivofijo = QuitaComilla(Nu(worksheet.Cells(row, 16).Value))
                    psTipoEquipo = QuitaComilla(Nu(worksheet.Cells(row, 17).Value))
                    psModelo = QuitaComilla(Nu(worksheet.Cells(row, 18).Value))
                    psMarca = QuitaComilla(Nu(worksheet.Cells(row, 19).Value))
                    psSerieNro = QuitaComilla(Nu(worksheet.Cells(row, 20).Value))
                    psHostName = QuitaComilla(Nu(worksheet.Cells(row, 21).Value))
                    psEstado = QuitaComilla(Nu(worksheet.Cells(row, 22).Value))
                    psExcelCodArt = QuitaComilla(Nu(worksheet.Cells(row, 23).Value))
                    psExcelInternoSeccion = QuitaComilla(Nu(worksheet.Cells(row, 24).Value))

                    CmdGlobal.CommandText = " select * from TBINV_DATOS_INICIALES where DATOSINI_SERIE = '" & psSerieNro & "' "
                    RsRecep = CmdGlobal.ExecuteReader
                    If RsRecep.HasRows Then
                        If UCase(psSerieNro) = "ILEGIBLE" Or UCase(psSerieNro) = "SERIE ILEGIBLE" Or UCase(psSerieNro) = "SIN SERIE" Then
                            pdSerieCorrelativo = pdSerieCorrelativo + 1
                            psSerieNro = psSerieNro & " " & pdSerieCorrelativo
                            RsRecep.Close()
                            GoTo Seguir
                        Else
                            RsRecep.Close()
                            CmdGlobal2.CommandText = " select MAX(ISNULL(DATOSINI_NRO_CORRELATIVO,0)) from TBINV_DATOS_INICIALES_NOCARGADOS"
                            Rs = CmdGlobal2.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    pdCosReg = Nz(Rs(0)) + 1
                                End While
                            Else
                                pdCosReg = 1
                            End If
                            Rs.Close()
                            CmdGlobal2.CommandText = " INSERT INTO TBINV_DATOS_INICIALES_NOCARGADOS ( DATOSINI_NRO_CORRELATIVO, DATOSINI_NRO, DATOSINI_NOMBRE, DATOSINI_CLASE, DATOSINI_FAMILIA,DATOSINI_CODINVENTARIO, DATOSINI_CIUDAD, DATOSINI_DISTRITO, DATOSINI_AGENCIA, " _
                                                   & " DATOSINI_NOMBRE_COMERCIAL, DATOSINI_USUARIO, DATOSINI_DNI, DATOSINI_CARGO, DATOSINI_CECO, DATOSINI_AREA, DATOSINI_RAZON_SOCIAL, DATOSINI_ACTIVO_FIJO, DATOSINI_TIPO_EQUIPO, " _
                                                   & " DATOSINI_MODELO, DATOSINI_MARCA, DATOSINI_SERIE, DATOSINI_HOST_NAME, DATOSINI_STOCK, DATOSINI_FECHA_REG, DATOSINI_SYS_CRE ) " _
                                                   & " VALUES ( " & pdCosReg & "," & pdNroReg & ", '" & psNombreOficina & "', '" & psClase & "', '" & psfamilia & "','" & psCodinv & "', '" & psCiudad & "','" & psDistrito & "', '" & psAgencia & "', " _
                                                   & " '" & psNombreComercial & "', '" & psUsuario & "', '" & psDni & "', '" & psCargo & "', '" & psCECO & "', '" & psArea & "', '" & psRazonSocial & "', '" & psActivofijo & "', '" & psTipoEquipo & "', " _
                                                   & " '" & psModelo & "', '" & psMarca & "', '" & psSerieNro & "', '" & psHostName & "', '" & psEstado & "','" & FechaActual() & "', '" & Valorsys & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                    Else
                            RsRecep.Close()
Seguir:
                        CmdGlobal.CommandText = " select MAX(ISNULL(DATOSINI_NRO_CORRELATIVO,0)) from TBINV_DATOS_INICIALES"
                        Rs = CmdGlobal.ExecuteReader
                        If Rs.HasRows Then
                            While Rs.Read
                                pdCosReg = Nz(Rs(0)) + 1
                            End While
                        Else
                            pdCosReg = 1
                        End If
                        Rs.Close()
                        pdCantBien = pdCantBien + 1
                        CmdGlobal2.CommandText = " INSERT INTO TBINV_DATOS_INICIALES ( DATOSINI_NRO_CORRELATIVO, DATOSINI_NRO, DATOSINI_NOMBRE, DATOSINI_CLASE, DATOSINI_FAMILIA,DATOSINI_CODINVENTARIO, DATOSINI_CIUDAD, DATOSINI_DISTRITO, DATOSINI_AGENCIA, " _
                                               & " DATOSINI_NOMBRE_COMERCIAL, DATOSINI_USUARIO, DATOSINI_DNI, DATOSINI_CARGO, DATOSINI_CECO, DATOSINI_AREA, DATOSINI_RAZON_SOCIAL, DATOSINI_ACTIVO_FIJO, DATOSINI_TIPO_EQUIPO, " _
                                               & " DATOSINI_MODELO, DATOSINI_MARCA, DATOSINI_SERIE, DATOSINI_HOST_NAME, DATOSINI_STOCK, DATOSINI_FECHA_REG, DATOSINI_SYS_CRE ) " _
                                               & " VALUES ( " & pdCosReg & "," & pdNroReg & ", '" & psNombreOficina & "', '" & psClase & "', '" & psfamilia & "','" & psCodinv & "', '" & psCiudad & "','" & psDistrito & "', '" & psAgencia & "', " _
                                               & " '" & psNombreComercial & "', '" & psUsuario & "', '" & psDni & "', '" & psCargo & "', '" & psCECO & "', '" & psArea & "', '" & psRazonSocial & "', '" & psActivofijo & "', '" & psTipoEquipo & "', " _
                                               & " '" & psModelo & "', '" & psMarca & "', '" & psSerieNro & "', '" & psHostName & "', '" & psEstado & "','" & FechaActual() & "', '" & Valorsys & "')"
                        CmdGlobal2.ExecuteNonQuery()
                        If psExcelCodArt = "" Then

                            CmdGlobal2.CommandText = " SELECT ARTMAR_CODIGO, ARTMAR_DESCRIPCION " _
                            & " From TBINV_ARTICULO_MARCA " _
                            & " WHERE ARTMAR_SYS_EST = '0' AND UPPER(ARTMAR_DESCRIPCION) = '" & psMarca & "'"
                            Rs = CmdGlobal2.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    psCodMarca = Nu(Rs!ARTMAR_CODIGO)
                                    CmdGlobal3.CommandText = " SELECT ARTMOD_CODIGO, ARTMAR_CODIGO, ARTMOD_DESCRIPCION " _
                                                        & " From TBINV_ARTICULO_MODELO " _
                                                        & " WHERE ARTMOD_SYS_EST = '0' AND ARTMAR_CODIGO = " & psCodMarca & " AND UPPER(ARTMOD_DESCRIPCION) = '" & psModelo & "'"
                                    Rs2 = CmdGlobal3.ExecuteReader
                                    If Rs2.HasRows Then
                                        While Rs2.Read
                                            psCodModelo = Nu(Rs2!ARTMOD_CODIGO)
                                        End While
                                        Rs2.Close()
                                    Else
                                        Rs2.Close()
                                        CmdGlobal.CommandText = "SELECT MAX(ARTMOD_CODIGO) FROM TBINV_ARTICULO_MODELO  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"

                                        Rs2 = CmdGlobal.ExecuteReader
                                        If Rs2.HasRows Then
                                            While Rs2.Read
                                                psCodModelo = Nz(Rs2(0)) + 1
                                            End While
                                            Rs2.Close()
                                        Else
                                            psCodModelo = "1"
                                            Rs2.Close()
                                            CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULO_MODELO( EMPRESA_CODIGO, ARTMOD_CODIGO, ARTMAR_CODIGO, ARTMOD_DESCRIPCION, ARTMOD_SYS_CRE, ARTMOD_SYS_EST) " _
                                                      & " VALUES('" & Session("CodEmpresa") & "'," & psCodModelo & "," & psCodMarca & ",'" & psModelo & "','" & Valorsys & "','0')"
                                            CmdGlobal.ExecuteNonQuery()
                                        End If
                                    End If
                                    psCodModelo = 1
                                End While
                                Rs.Close()
                            Else
                                Rs.Close()
                                CmdGlobal.CommandText = "SELECT MAX(isnull(ARTMAR_CODIGO,0)) FROM TBINV_ARTICULO_MARCA WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psCodMarca = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    psCodMarca = "1"
                                End If
                                Rs.Close()
                                CmdGlobal.CommandText = "SELECT MAX(isnull(ARTMOD_CODIGO,0)) FROM   TBINV_ARTICULO_MODELO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                Rs = CmdGlobal.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psCodModelo = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    psCodModelo = "1"
                                End If
                                Rs.Close()
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULO_MARCA(EMPRESA_CODIGO, ARTMAR_CODIGO, ARTMAR_DESCRIPCION, ARTMAR_SYS_CRE, ARTMAR_SYS_EST) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodMarca & ",'" & psMarca & "','" & Valorsys & "','0')"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULO_MODELO( EMPRESA_CODIGO, ARTMOD_CODIGO, ARTMAR_CODIGO, ARTMOD_DESCRIPCION, ARTMOD_SYS_CRE, ARTMOD_SYS_EST) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & psCodModelo & "," & psCodMarca & ",'" & psModelo & "','" & Valorsys & "','0')"
                                CmdGlobal.ExecuteNonQuery()
                            End If

                            CmdGlobal3.CommandText = " SELECT ART_DESCRIPCION, ART_CODIGO " _
                                            & " FROM TBINV_ARTICULOS WHERE ART_DESCRIPCION = '" & psTipoEquipo & " " & psMarca & " " & psModelo & "' "
                            Rs2 = CmdGlobal3.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    psCodArt = Nu(Rs2!art_codigo)
                                End While
                                Rs2.Close()
                            Else
                                Rs2.Close()
                                CmdGlobal3.CommandText = "SELECT MAX(ART_CODIGO) FROM TBINV_ARTICULOS WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                Rs = CmdGlobal3.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psCodArt = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    psCodArt = "1"
                                End If
                                Rs.Close()
                                CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS(ART_CODIGO, ART_SYS_EST, ART_SYS_CRE,EMPRESA_CODIGO,ART_COMPUESTO) " _
                                          & " VALUES(" & psCodArt & ",'0','" & Valorsys & "','" & Session("CodEmpresa") & "','NO')"
                                CmdGlobal.ExecuteNonQuery()
                                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS SET ART_TIPO=73, ART_DESCRIPCION='" & QuitaComilla(Trim(psTipoEquipo & " " & psMarca & " " & psModelo)) & "', ART_ABREV='" & Left(QuitaComilla(Trim(psTipoEquipo)), 20) & "', ART_CODEQUIVA='" & Left(QuitaComilla(Trim(Nu(psTipoEquipo))), 50) & "',  " _
                                              & " ART_UNIDAD_MEDIDA = 34, ART_CLASIFICACION=2, ART_SITUACION = 28, " _
                                              & " ART_STOCK_MINIMO = 0, ART_CLASE=31 " _
                                              & " WHERE ART_CODIGO = " & psCodArt & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                CmdGlobal.ExecuteNonQuery()

                                If psCodMarca <> 0 Then
                                    CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS SET ARTMAR_CODIGO = " & psCodMarca & " " _
                                              & " WHERE ART_CODIGO=" & psCodArt & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                    CmdGlobal.ExecuteNonQuery()

                                End If
                                If psCodModelo <> 0 Then
                                    CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS SET ARTMOD_CODIGO = " & psCodModelo & " " _
                                              & " WHERE ART_CODIGO=" & psCodArt & " AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
                                    CmdGlobal.ExecuteNonQuery()
                                End If
                            End If
                        Else
                            psCodArt = Nz(psExcelCodArt)
                        End If

                        CmdGlobal2.CommandText = " SELECT * FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO= " & psCodRecepcion & " AND ARTICULO_CODIGO =" & psCodArt
                        Rs2 = CmdGlobal2.ExecuteReader
                        If Rs2.HasRows Then
                            While Rs2.Read
                                CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_CANT_XREC = " & Nz(Rs2("RECEPD_CANT_XREC")) + 1 & "  , RECEPD_CANT_REC = " & Nz(Rs2("RECEPD_CANT_REC")) + 1 & " ," _
                                                       & " RECEPD_CANT_ING = " & Nz(Rs2("RECEPD_CANT_ING")) + 1 & "  " _
                                                       & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND RECEP_CODIGO = " & psCodRecepcion & " AND ARTICULO_CODIGO= " & psCodArt
                                CmdGlobal.ExecuteNonQuery()
                            End While
                            Rs2.Close()
                        Else
                            Rs2.Close()
                            CmdGlobal2.CommandText = " SELECT MAX(ISNULL(RECEPD_ITEM,0)) FROM TBINV_ALMACEN_RECEPCION_DET WHERE RECEP_CODIGO= " & psCodRecepcion
                            Rs2 = CmdGlobal2.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    pdRecepItem = Nz(Rs2(0)) + 1
                                End While
                            Else
                                pdRecepItem = 1
                            End If
                            Rs2.Close()
                            CmdGlobal2.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                                & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                                & "'" & Session("CodEmpresa") & "'," & psCodRecepcion & ", " & pdRecepItem & "  ," & psCodArt & ",1 ,1," _
                                                & " 0 ,0,1,'1','0','20','S')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If

                        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = 1) AND (UBICACT_TIPO='1')" _
                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                        RsRecep = CmdGlobal.ExecuteReader
                        If RsRecep.HasRows Then
                            While RsRecep.Read
                                StockAc = Nz(RsRecep!SAA_STOCK_ACTUAL)
                                StockAc = StockAc + 1
                                CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = 1) AND (UBICACT_TIPO='1')" _
                                                                  & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        Else
                            CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                                  & "VALUES('1',1," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End If
                        RsRecep.Close()

                        CmdGlobal.CommandText = "SELECT MAX(isnull(MOV_NRO,0)) FROM TBINV_MOVIMIENTO_GENERAL "
                        RsRecep = CmdGlobal.ExecuteReader
                        If RsRecep.HasRows Then
                            While RsRecep.Read
                                pdNroMovimiento = Nz(RsRecep(0)) + 1
                            End While
                        Else
                            pdNroMovimiento = "00000001"
                        End If
                        RsRecep.Close()

                        Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), psCodRecepcion, "20", psCodArt, 1, 1, 1, 1, "POR INVENTARIO", "1", FormatoFecha(FechaActual), 1)

                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO,CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                              & " values('" & Session("CodEmpresa") & "','" & pdNroMovimiento & "','1','1','1','1','1','" & psCodRecepcion & "','" & psCodArt & "',1,'" & Valorsys & "','2','20','" & FechaActual() & "','0')"
                        CmdGlobal.ExecuteNonQuery()

                        CmdGlobal.CommandText = "SELECT MAX(isnull(SERIE_NUMERAR,0)) FROM TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & ""
                        RsRecep = CmdGlobal.ExecuteReader
                        If RsRecep.HasRows Then
                            While RsRecep.Read
                                SerieNum = Nz(RsRecep(0)) + 1
                            End While
                        Else
                            SerieNum = 1
                        End If
                        RsRecep.Close()
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & "(SERIE_NUMERAR, RECEP_CODIGO, ARTICULO_CODIGO,SERIE_NRO,UBICACT_TIPO,UBICACT_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO,CRITI_CODIGO,CONFIDENCIALIDAD,DISPONIBILIDAD,SERIE_ESTADO,TIPO_GARANTIA) " _
                                          & "VALUES(" & SerieNum & "," & psCodRecepcion & "," & psCodArt & ",'" & psSerieNro & "','1',1,'N','" & Valorsys & "','0','S','1','2','1','2','0','')"
                        CmdGlobal.ExecuteNonQuery()
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL) " _
                                              & "VALUES(" & SerieNum & ",'1',1,'0','0','" & Valorsys & "','" & FechaActual() & "','1'," & psCodRecepcion & ")"
                        CmdGlobal.ExecuteNonQuery()

                        If psExcelInternoSeccion = "" Then
                            CmdGlobal.CommandText = "SELECT CCOSTO_CODIGO, CCOSTO_COD_INTERNO FROM TBLOGIS_CENTRO_COSTOS WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CCOSTO_DESCRIPCION)='" & psAgencia & "' AND (CCOSTO_SYS_EST = '0')"
                            RsRecep = CmdGlobal.ExecuteReader
                            If RsRecep.HasRows Then
                                While RsRecep.Read
                                    psCodCECosto = Nz(RsRecep("CCOSTO_CODIGO"))
                                    psCostoInterno = Nu(RsRecep("CCOSTO_COD_INTERNO"))
                                    CmdGlobal2.CommandText = "SELECT CECOSE_CODIGO, CECOSE_COD_INTERNO FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND (CECOSE_DESCRIPCION)='" & psNombreComercial & "' AND (CECOSE_SYS_EST = '0') AND CCOSTO_CODIGO = " & psCodCECosto
                                    Rs2 = CmdGlobal2.ExecuteReader
                                    If Rs2.HasRows Then
                                        While Rs2.Read
                                            psCodSeccion = Nz(Rs2("CECOSE_CODIGO"))
                                            psCecoseInterno = Nu(Rs2("CECOSE_COD_INTERNO"))
                                            psCecoseInternoCod = Nz(Rs2("CECOSE_COD_INTERNO"))
                                        End While
                                        Rs2.Close()
                                    Else
                                        Rs2.Close()
                                        CmdGlobal2.CommandText = "SELECT MAX(isnull(CECOSE_CODIGO,0)) FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                                        Rs = CmdGlobal2.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                psCodSeccion = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            psCodSeccion = "1"
                                        End If
                                        Rs.Close()
                                        CmdGlobal2.CommandText = "SELECT COUNT(CECOSE_COD_INTERNO) AS CANT FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CCOSTO_CODIGO = " & psCodCECosto & " AND  (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                                        Rs = CmdGlobal2.ExecuteReader
                                        If Rs.HasRows Then
                                            While Rs.Read
                                                psCecoseInternoCod = Nz(Rs(0)) + 1
                                            End While
                                        Else
                                            psCecoseInternoCod = "1"
                                        End If
                                        Rs.Close()
                                        CmdGlobal2.CommandText = "INSERT INTO TBLOGIS_CENTRO_COSTO_SECCION(EMPRESA_CODIGO,CECOSE_CODIGO,CECOSE_COD_INTERNO,CECOSE_DESCRIPCION,CECOSE_SYS_EST,CCOSTO_CODIGO,CECOSE_ACTIVO) " _
                                                      & "VALUES('" & Session("CodEmpresa") & "'," & psCodSeccion & ",'" & psCostoInterno & Llenar_Ceros(psCecoseInternoCod, 3) & "','" & psNombreComercial & "','0','" & psCodCECosto & "','S')"
                                        CmdGlobal2.ExecuteNonQuery()
                                    End If
                                End While
                                RsRecep.Close()
                            Else
                                RsRecep.Close()
CrearSeccion:
                                CmdGlobal2.CommandText = "SELECT MAX(isnull(CCOSTO_CODIGO,0)) FROM TBLOGIS_CENTRO_COSTOS WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                                Rs = CmdGlobal2.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psCodCECosto = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    psCodCECosto = "1"
                                End If
                                Rs.Close()
                                If Session("CrearIgual") = "No" Then psCostoInterno = Llenar_Ceros(psCodCECosto, 3)
                                CmdGlobal2.CommandText = "INSERT INTO TBLOGIS_CENTRO_COSTOS(EMPRESA_CODIGO, CCOSTO_CODIGO,CCOSTO_COD_INTERNO, CCOSTO_DESCRIPCION,CCOSTO_SYS_EST,CCOSTO_ACTIVO) " _
                                                  & "VALUES('" & Session("CodEmpresa") & "'," & psCodCECosto & ",'" & psCostoInterno & "','" & psAgencia & "','0','S')"
                                CmdGlobal2.ExecuteNonQuery()
                                CmdGlobal2.CommandText = "SELECT MAX(isnull(CECOSE_CODIGO,0)) FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "')"
                                Rs = CmdGlobal2.ExecuteReader
                                If Rs.HasRows Then
                                    While Rs.Read
                                        psCodSeccion = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    psCodSeccion = "1"
                                End If
                                Rs.Close()
                                If Session("CrearIgual") = "No" Then psCecoseInternoCod = psCostoInterno & "001"
                                CmdGlobal2.CommandText = "INSERT INTO TBLOGIS_CENTRO_COSTO_SECCION(EMPRESA_CODIGO,CECOSE_CODIGO,CECOSE_COD_INTERNO,CECOSE_DESCRIPCION,CECOSE_SYS_EST,CCOSTO_CODIGO,CECOSE_ACTIVO) " _
                                                  & "VALUES('" & Session("CodEmpresa") & "'," & psCodSeccion & ",'" & psCecoseInternoCod & "','" & psNombreComercial & "','0','" & psCodCECosto & "','S')"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                        Else
                            CmdGlobal2.CommandText = "SELECT CECOSE_CODIGO FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE (EMPRESA_CODIGO = '" & Session("CodEmpresa") & "') AND CECOSE_COD_INTERNO LIKE '%" & psExcelInternoSeccion & "'"
                            Rs = CmdGlobal2.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    psCodSeccion = Nz(Rs(0))
                                End While
                            End If
                            Rs.Close()
                            Session("CrearIgual") = "No"
                            If psCodSeccion = 0 Then
                                Session("CrearIgual") = "Si"
                                psAgencia = psExcelInternoSeccion
                                psNombreComercial = psExcelInternoSeccion
                                psCostoInterno = psExcelInternoSeccion
                                psCecoseInternoCod = psExcelInternoSeccion
                                GoTo CrearSeccion
                            End If
                        End If

                        CmdGlobal.CommandText = " UPDATE  TBINV_DATOS_INICIALES SET DATOSINI_ART_CODIGO = " & psCodArt & ", DATOSINI_SERIE_NUMERAR = " & SerieNum & " , DATOSINI_CECOSE_CODIGO = " & psCodSeccion & " where DATOSINI_NRO_CORRELATIVO = " & pdCosReg & " and DATOSINI_SERIE = '" & psSerieNro & "' "
                        CmdGlobal.ExecuteNonQuery()

                        Call Despacho_unoxuno(SerieNum, FechaActual, psCodSeccion, "2", "20", 1, "1", psCodArt, "")

                    End If
                Next

                CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_NRO_ITEM = " & pdRecepItem & " ,RECEP_CANT_XREC = " & pdCantBien & ", RECEP_CANT_REC = " & pdCantBien & "  " _
                                       & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND RECEP_CODIGO = " & psCodRecepcion
                CmdGlobal.ExecuteNonQuery()
            End If
        End Using
    End Sub

    Private Sub Despacho_unoxuno(ByVal psSerieCodigo As String, ByVal psFecha As String, ByVal psDestino As String,
                             ByVal psTipoDestino As String, ByVal psMotivo As String, ByVal psUbicaCodigo As String,
                             ByVal psUbicaTipo As String, ByVal psCodArticulo As String, ByVal psMotivoDescripcion As String)

        Dim ValorSys As String : ValorSys = FechaActual() + HoraActual() + Session("User")
        Dim psCodCECosto As String : psCodCECosto = ""
        Dim psCodSeccion As String : psCodSeccion = ""
        Dim psCodArt As String : psCodArt = ""
        Dim psSerieNumerar As String : psSerieNumerar = psSerieCodigo
        Dim psSerieNro As String : psSerieNro = ""
        Dim psPlacaNro As String : psPlacaNro = ""
        Dim psT As String : psT = ""
        Dim psFechaAdq As String : psFechaAdq = ""
        Dim lblNroMovimiento As String : lblNroMovimiento = ""
        Dim StockAc As Double : StockAc = 0
        Dim cant As Double : cant = 0
        Dim i As Long : i = 0
        Dim psTipoOrigen As String : psTipoOrigen = ""
        Dim lblCodAlmacen As String : lblCodAlmacen = ""
        Dim lblCodDespacho As String : lblCodDespacho = ""
        Dim psCodDestino As String : psCodDestino = ""
        Dim psOrigenAlm As String : psOrigenAlm = "NULL"
        Dim psOrigenCC As String : psOrigenCC = "NULL"
        Dim psDestinoAlm As String : psDestinoAlm = "NULL"
        Dim psDestinoCC As String : psDestinoCC = "NULL"
        Dim psUbicaAlm As String : psUbicaAlm = "NULL"
        Dim psUbicaCC As String : psUbicaCC = "NULL"
        Dim DesCodProveedor As String : DesCodProveedor = "NULL"
        Dim DesCodCliente As String : DesCodCliente = "NULL"
        Dim DesCodPersona As String : DesCodPersona = "NULL"
        lblCodAlmacen = 1
        psTipoOrigen = "1"
        psTipoDestino = "2"
        psDestinoCC = psDestino
        CodSalida = ""
        psCodDestino = psDestino
        StockAc = 0
        Dim psRecepcion As String : psRecepcion = ""
        i = 0
        Dim psProveedor As String = ""
        Dim psCodRecepcion As String = ""
        Dim psCodAllSal As String = ""
        Dim dtArt As New DataTable


        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2

        Dim psFechaFormato As String = ""
        Dim psHoraFormato As String = ""
        psFechaFormato = psFecha
        psHoraFormato = HoraActual()

        psSerieNumerar = psSerieCodigo
        psCodArt = psCodArticulo
        psRecepcion = ""
        If psTipoOrigen = psTipoDestino And lblCodAlmacen = psCodDestino Then
        ElseIf psUbicaCodigo = "" And psUbicaTipo = "" Then
            ' GoTo SalidaBien
        Else
            '-----------------------SALIDA DE ALMACEN
            CmdGlobal.CommandText = "SELECT MAX(isnull(DESP_CODIGO,0)) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblCodDespacho = Nz(Rs(0)) + 1
                End While
            Else
                lblCodDespacho = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = "SELECT MAX(isnull(ALLSAL_CODIGO,0)) FROM TBINV_SALIDA_MOTIVO"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodAllSal = Nz(Rs(0)) + 1
                End While
            Else
                psCodAllSal = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                                      & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                                      & " VALUES ('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & lblCodDespacho & ",'" & psMotivo & "','1'," & psUbicaCodigo & ", " _
                                      & " '" & psTipoDestino & "'," & psCodDestino & ",'" & psFechaFormato & "','" & HoraActual() & "','3','0','" & psFechaFormato & "')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET (EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, SERIE_NUMERAR, ALLSALD_SYS_REG, " _
                                      & " ALLSALD_SYS_ENVIO, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST ) " _
                                      & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & ",1," & psSerieNumerar & ",'" & ValorSys & "'," _
                                      & " '" & ValorSys & "','2','1','0')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                       & " CECOSE_CODIGO_DESTINO, " _
                                       & " DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                       & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                       & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",'" & FechaActual() & "'," & HoraActual() & ",'" & Session("User") & "','" & psTipoDestino & "'," _
                                       & " " & psDestinoCC & ",  " _
                                       & " '2', '0', 1, 1, 0, 1, " & lblCodAlmacen & ", '" & psFechaFormato & "', '" & psHoraFormato & "', '" & psMotivo & "', '" & ValorSys & "')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                                  & " VALUES('" & Session("CodEmpresa") & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','0',NULL,'" & psMotivo & "','N')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
            CmdGlobal.ExecuteNonQuery()
            'STOCK
            StockAc = 0
            CmdGlobal.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psUbicaCodigo & ") AND (UBICACT_TIPO='" & psUbicaTipo & "') " _
                                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psUbicaCodigo & ") AND (UBICACT_TIPO='" & psUbicaTipo & "') " _
                                                     & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()

            'MOVIMIENTO GENERAL
            CmdGlobal.CommandText = "SELECT MAX(isnull(MOV_NRO,0)) FROM TBINV_MOVIMIENTO_GENERAL "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroMovimiento = Nz(Rs(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            Rs.Close()


            Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psUbicaTipo, psUbicaCodigo, psTipoDestino, psCodDestino, psMotivoDescripcion, "2", FormatoFecha(psFecha), 1)
            CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','2','" & psUbicaTipo & "','" & psUbicaCodigo & "', " _
                                              & " '" & psCodArt & "','1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaFormato & "','0','" & lblCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
            CmdGlobal.ExecuteNonQuery()
            '--------------------------recepcion en ccosto O ALMACEN
            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & Session("CodEmpressa") & "' AND DESP_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='3',DESP_CANT_REC='1',DESP_CANT_FALT_REC='0' WHERE DESP_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            CmdGlobal.ExecuteNonQuery()
            'STOCK
            CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                                     & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            Else
                CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                     & "VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & ",1,'0','" & Session("CodEmpresa") & "')"
                CmdGlobal2.ExecuteNonQuery()
            End If
            Rs.Close()

            'MOVIMIENTO GENERAL
            CmdGlobal.CommandText = "SELECT MAX(isnull(MOV_NRO,0)) FROM TBINV_MOVIMIENTO_GENERAL "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroMovimiento = Nz(Rs(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            Rs.Close()

            Call oFuncInv.Movimiento_Kardex(Session("Ruta_Emp"), Session("CodEmpresa"), lblCodDespacho, psMotivo, psCodArt, psTipoDestino, psCodDestino, psUbicaTipo, psUbicaCodigo, psMotivoDescripcion, "1", FormatoFecha(psFecha), 1)
            CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                       & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                       & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                                       & " " & psCodArt & ",'1','" & ValorSys & "','3','" & psMotivo & "','" & psFechaFormato & "','0','" & lblCodDespacho & "','" & psUbicaTipo & "','" & psUbicaCodigo & "')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & Session("CodEmpresa") & " SET UBICACT_TIPO='" & psTipoDestino & "',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & psSerieNumerar
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & Session("CodEmpresa") & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                              & " VALUES ('" & psSerieNumerar & "','" & psTipoDestino & "'," & psCodDestino & ",'" & psMotivo & "','0','" & ValorSys & "','" & psFechaFormato & "','1','" & lblCodDespacho & "')"
            CmdGlobal.ExecuteNonQuery()
        End If
        '

        If lblCodDespacho <> "" Then CodSalida = lblCodDespacho

    End Sub


End Class
