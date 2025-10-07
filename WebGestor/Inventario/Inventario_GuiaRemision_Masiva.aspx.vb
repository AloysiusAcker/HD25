Imports OfficeOpenXml
Imports WebGestor
Imports System.Data.SqlClient
Imports System.Data
Imports System.Net
Imports System.IO
Imports System.Threading
Imports Excel
Imports DataTable = System.Data.DataTable

Partial Class Inventario_Inventario_GuiaRemision_Masiva
    Inherits System.Web.UI.Page
    Dim psSalida As String = ""
    Dim i As Long
    Dim ls_CodTicket As String = ""
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim obj As New Cls_Catalogo
            Dim dt As New DataTable
            Dim psconexion As String = Session("Ruta_Emp")
            TxtFecha.Text = FormatoFecha(FechaActual)
            TxtHora.Text = FormatoHoraSeg(HoraActual(True))

            dt = obj.Lista_Tipo(psconexion)
            DdlTipoBA.DataSource = dt
            DdlTipoBA.DataValueField = "ELEMENTO_CODUNICO"
            DdlTipoBA.DataTextField = "ELEMENTO_DESCRIPCION"
            DdlTipoBA.DataBind()
            DdlTipoBA.Items.Add("< Seleccionar >")
            DdlTipoBA.SelectedValue = "< Seleccionar >"
            Me.Page.Session.Timeout = 1080
            Call LlenaComboItem("TBOPC549", DdlModTransporte)
        End If
    End Sub
    Protected Sub btnLeerArchivo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLeerArchivo.Click
        LblError.Text = ""
        If TxtGuiaSerie.Text.Trim = "" And TxtGuiaNumero.Text.Trim = "" Then LblError.Text = "Ingresar Serie y número de la Guía de remisión."
        If lblCodOrigen.Text.Trim = "" Then LblError.Text = LblError.Text & "<br/>" & "Ingresar el remitente de la guía."
        If TxtUbigeo.Text = "" Then LblError.Text = LblError.Text & "<br/>" & "Ingresar el ubigeo del remitente de la guía."
        If TxtColCC.Text.Trim = "" Then LblError.Text = LblError.Text & "<br/" & "Ingresar en que columna se encuentra el centro de costos."
        If TxtColObsGuia.Text.Trim = "" Then LblError.Text = LblError.Text & "<br/" & "Ingresar en que columna se encuentra la observacion de la guia."
        If FileUpload1.HasFile = False Then LblError.Text = LblError.Text & "<br/>" & "Ingresar el archivo a leer."
        If GvListaArticulos.Rows.Count < 1 Then LblError.Text = LblError.Text & "<br/>" & "Ingresar productos a cargar."
        If DdlModTransporte.SelectedValue = "< Seleccionar >" Then LblError.Text = LblError.Text & "<br/>" & "Seleccionar modalidad de transporte."
        For i = 0 To GvListaArticulos.Rows.Count - 1
            Dim pstxtCant As TextBox = GvListaArticulos.Rows(i).Cells(6).FindControl("txtCant")
            If pstxtCant.Text = "" Then
                LblError.Text = LblError.Text & "<br/>" & "Ingresar la cantidad del articulo que se van a cargar."
            End If
        Next

        If LblError.Text.Trim <> "" Then Exit Sub
        Dim aa As Long = 0
        Dim psCodCecose As String = ""
        Dim psCecoseCodInt As String = ""
        Dim psCodCecoseAnt As String = ""
        Dim psCecoseDescripcion As String = ""
        Dim psCecoseDireccion As String = ""
        Dim psObservacion As String = ""
        Dim psCecoseUbigeo As String = ""
        Dim ii As Long = 0

        Dim Productos(,) As String
        Productos = New String(GvListaArticulos.Rows.Count, 5) {}
        ReDim Productos(GvListaArticulos.Rows.Count, 5)

        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        If Existe_Tabla("V_CentroCostos_SinDireccion", Session("Ruta_Emp")) = False Then
            CmdGlobal.CommandText = " CREATE TABLE V_CentroCostos_SinDireccion (CC_COSTO_CODIGO float, CC_COSTO_CODINTERNO VARCHAR(5)) "
            CmdGlobal.ExecuteReader()
        End If

        Dim filaIni As Long = 0
        Dim filafin As Long = 0
        Dim colCC As Long = 0
        colCC = TxtColCC.Text
        filaIni = TxtIni.Text
        filafin = Txtfin.Text

        CmdGlobal.CommandText = " DELETE FROM V_CentroCostos_SinDireccion "
        CmdGlobal.ExecuteReader()

        If FileUpload1.HasFile AndAlso FileUpload1.FileName.EndsWith(".xlsx") Then
            Dim archivo As HttpPostedFile = FileUpload1.PostedFile
            Dim rutaArchivo As String = Server.MapPath("~/Inventario/GuiaRemision/CargaMasiva/" + FileUpload1.FileName)
            Dim psValoresExcel As String = ""

            Dim carpeta As String = Server.MapPath("~/Inventario/GuiaRemision/CargaMasiva/")

            ' Verificar si la carpeta existe
            If Not Directory.Exists(carpeta) Then
                ' Crear la carpeta
                Directory.CreateDirectory(carpeta)
            End If

            archivo.SaveAs(rutaArchivo)
            Dim valor As String = String.Empty

            ' Leer el archivo Excel
            Using package As New ExcelPackage(New FileInfo(rutaArchivo))
                Dim workbook As ExcelWorkbook = package.Workbook
                If workbook IsNot Nothing AndAlso workbook.Worksheets.Count > 0 Then
                    Dim worksheet As ExcelWorksheet = workbook.Worksheets(0)

                    ' Recorrer las celdas del archivo Excel
                    For row As Integer = filaIni To filafin
                        If worksheet.Cells(row, colCC).Value IsNot Nothing Then

                            Dim celda As ExcelRange = worksheet.Cells(row, colCC)

                            If celda.Value IsNot Nothing Then
                                valor = celda.Value.ToString()
                            End If

                            CmdGlobal.CommandText = " SELECT CECOSE_CODIGO, CECOSE_COD_INTERNO, CECOSE_DESCRIPCION, CECOSE_DIRECCION, substring(isnull(CECOSE_DPTO,'000000'),1,2)+substring(isnull(CECOSE_PROVINCIA,'000000'),3,2)+substring(isnull(CECOSE_DISTRITO,'000000'),5,2) as ubigeo FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CECOSE_COD_INTERNO = '" & valor & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    psCodCecose = Nz(Rs(0))
                                    psCecoseCodInt = Nz(Rs(1))
                                    psCecoseDescripcion = Nu(Rs(2))
                                    psCecoseDireccion = Nu(Rs(3))
                                    psCecoseUbigeo = Nu(Rs(4))
                                End While
                            End If
                            Rs.Close()

                            CmdGlobal.CommandText = " SELECT CECOSE_CODIGO, CECOSE_COD_INTERNO, CECOSE_DESCRIPCION, CECOSE_DIRECCION FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CECOSE_COD_INTERNO = '" & valor & "'"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    psCodCecoseAnt = Nz(Rs(0))
                                End While
                            End If
                            Rs.Close()
                            psObservacion = ""
                            psObservacion = psCecoseDescripcion & " Cod. " & psCecoseCodInt

                            If psCodCecose <> "" Then
                                For aa = 1 To GvListaArticulos.Rows.Count - 1
                                    If CDbl(Nz(worksheet.Cells(row, aa).Value)) > 0 Then
                                        psSalida = ""
                                        Call GenerarSalidaFinal(psCodCecose, Nz(worksheet.Cells(row, Val(GvListaArticulos.Rows(aa).Cells(5))).Value), Nz(GvListaArticulos.Rows(aa).Cells(1)))
                                        i = i + 1
                                        Productos(i, 0) = psCodCecose
                                        Productos(i, 1) = psSalida
                                        Productos(i, 2) = CDbl(Nz(GvListaArticulos.Rows(aa).Cells(1)))
                                        Productos(i, 3) = CDbl(Nz(worksheet.Cells(row, Val(GvListaArticulos.Rows(aa).Cells(5))).Value))
                                        Productos(i, 4) = IIf(TxtColObsGuia.Text <> "", Nu(worksheet.Cells(row, Val(TxtColObsGuia.Text)).Value), "")
                                    End If
                                Next
                                Call GenerarGuia(sender, e, psCodCecose, psCecoseCodInt, psCecoseDescripcion, psCecoseDireccion, i, Productos, psObservacion)
                                Erase Productos
                                ReDim Productos(GvListaArticulos.Rows.Count, 5)
                                i = 0
                                If psCecoseDireccion = "" Then
                                    CmdGlobal.CommandText = " INSERT INTO V_CentroCostos_SinDireccion (CC_COSTO_CODIGO) VALUES (" & psCodCecose & ") "
                                    Rs = CmdGlobal.ExecuteReader

                                ElseIf psCodCecose = "" Then

                                    CmdGlobal.CommandText = " SELECT CC_COSTO_CODINTERNO FROM V_CentroCostos_SinDireccion WHERE CC_COSTO_CODINTERNO = '" & worksheet.Cells(row, Val(colCC)).Value & "' "
                                    Rs = CmdGlobal.ExecuteReader
                                    If Rs.HasRows Then
                                    Else
                                        CmdGlobal2.CommandText = " INSERT INTO V_CentroCostos_SinDireccion (CC_COSTO_CODINTERNO) VALUES ('" & worksheet.Cells(row, Val(colCC)).Value & "') "
                                        CmdGlobal2.ExecuteNonQuery()
                                    End If
                                    Rs.Close()
                                End If
                            End If

                        End If
                    Next
                End If
            End Using

            ' Actualizar el contenido del UpdatePanel si es necesario
            'UpdatePanel2.Update()
        End If
    End Sub
    Private Sub GenerarSalidaFinal(ByVal psDestinoCodigo As String, ByVal psCant As Double, ByVal psCodArt As String)
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim ValorSys As String = "" : ValorSys = FechaActual() + HoraActual() + Session("User")
        Dim psCodCECosto As String : psCodCECosto = ""
        Dim psCodSeccion As String : psCodSeccion = ""
        Dim psSerieNumerar As String = ""
        Dim psSerieNro As String : psSerieNro = ""
        Dim psPlacaNro As String : psPlacaNro = ""
        Dim lblNroMovimiento As String : lblNroMovimiento = ""
        Dim StockAc As Double : StockAc = 0
        Dim cant As Double : cant = 0
        Dim i As Long : i = 0
        Dim psTipoDestino As String = ""
        Dim psTipoOrigen As String : psTipoOrigen = ""
        Dim psCodOrigen As String : psCodOrigen = ""
        Dim psCodDespacho As String : psCodDespacho = ""
        Dim psCodDestino As String : psCodDestino = psDestinoCodigo
        Dim psDestinoAlm As String : psDestinoAlm = "NULL"
        Dim psDestinoCC As String : psDestinoCC = "NULL"
        Dim DesCodProveedor As String : DesCodProveedor = "NULL"
        Dim DesCodCliente As String : DesCodCliente = "NULL"
        Dim DesCodPersona As String : DesCodPersona = "NULL"

        If optOrigen.Checked = True Then psTipoOrigen = "1"
        If optOrigen2.Checked = True Then psTipoOrigen = "2"

        psTipoDestino = "2"
        If psTipoDestino = "2" Then psDestinoCC = psDestinoCodigo
        If lblCodOrigen.Text <> "" Then psCodOrigen = lblCodOrigen.Text
        StockAc = 0
        psSalida = ""
        Dim psRecepcion As String : psRecepcion = ""
        i = 0
        Dim psProveedor As String = ""
        Dim psCodRecepcion As String = ""

        Dim CantTotal As Double
        CantTotal = psCant
        CantTotal = CantTotal '+ (CantTotal * 2)
        cant = CantTotal
        Dim pd_Secuencia_Accion As String = ""

        If psTipoOrigen = "1" Then
            '-----------------------SALIDA DE ALMACEN
            CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader()
            If Rs.HasRows Then
                While Rs.Read
                    psCodDespacho = Nz(Rs(0)) + 1
                End While
            Else
                psCodDespacho = 1
            End If
            Rs.Close
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                   & " CECOSE_CODIGO_DESTINO, " _
                                   & " DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                   & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                   & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "'," & HoraActual(True) & ",'" & Session("User") & "','" & psTipoDestino & "'," _
                                   & " " & psDestinoCC & ", " _
                                   & " '3','0'," & cant & "," & cant & "," & cant & ",0," & psCodOrigen & ", '" & Format(TxtFecha.Text, "yyyymmdd") & "','" & Format(TxtHora.Text, "HHmm") & "','5','S')"
            CmdGlobal.ExecuteNonQuery()
            'If ls_CodTicket <> "" Then
            '    Call Ticket_GrabarTrackingAcciones(ls_CodTicket, ls_CodAccion, Format(TxtFecha.Text, "yyyymmdd"), Format(TxtHora.Text, "hhmm"), psCodDespacho, "DESP_TICKET", "DESP_CODIGO", "TBINV_ALMACEN_DESPACHO")
            'End If
        ElseIf psTipoOrigen = "2" Then 'SALIDA DE CENTRO DE COSTO
            CmdGlobal.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodDespacho = Nz(Rs(0)) + 1
                End While
            Else
                psCodDespacho = 1
            End If
            Rs.Close
            CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, " _
                                & " ALMACEN_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO, OSAL_PROVEEDOR_CODIGO, OSAL_CLIENTE_CODIGO, OSAL_PERSONA_CODIGO, " _
                                & " OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL) " _
                                & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",'" & FechaActual() & "','" & HoraActual(True) & "','" & Session("User") & "','" & psTipoDestino & "'," _
                                & " " & psDestinoCC & ", " _
                                & " '3','0'," & cant & "," & cant & ",0,'" & psCodOrigen & "'," _
                                & " '" & Format(TxtFecha.Text, "yyyymmdd") & "','" & HoraActual(True) & "','5')"
            CmdGlobal.ExecuteNonQuery()
            'If ls_CodTicket <> "" Then
            '    Call Ticket_GrabarTrackingAcciones(ls_CodTicket, ls_CodAccion, Format(txtFechaRecep, "yyyymmdd"), Format(txtHoraRecep, "hhmm"), psCodDespacho, "OSAL_TICKET", "OSAL_CODIGO", "TBINV_CCOSTO_SALIDA")
            'End If
        End If
        Dim psCodAllSal As String = ""
        CmdGlobal.CommandText = "SELECT MAX(ALLSAL_CODIGO) FROM TBINV_SALIDA_MOTIVO"
        Rs = CmdGlobal.ExecuteReader()
        If Rs.HasRows Then
            While Rs.Read
                psCodAllSal = Nz(Rs(0)) + 1
            End While
        Else
            psCodAllSal = 1
        End If
        Rs.Close
        CmdGlobal.CommandText = " INSERT INTO TBINV_SALIDA_MOTIVO (EMPRESA_CODIGO, ALLSAL_CODIGO, DESP_CODIGO, ALLSAL_MOTIVO, ALLSAL_ORIGEN_TIPO, ALLSAL_ORIGEN_CODIGO, " _
                              & " ALLSAL_DESTINO_TIPO, ALLSAL_DESTINO_CODIGO, ALLSAL_REG_FECHA, ALLSAL_REG_HORA, ALLSAL_ESTADO, ALLSAL_SYS_EST,ALLSAL_FECHA_XDEVOL)" _
                              & " VALUES ('" & Session("CodEmpresa") & "'," & psCodAllSal & "," & psCodDespacho & ",'5','" & IIf(optOrigen.Checked = True, "1", "2") & "'," & lblCodOrigen.Text & ", " _
                              & " '" & psTipoDestino & "'," & psDestinoCodigo & ",'" & FechaActual() & "','" & HoraActual(True) & "','3','0','" & Format(TxtFecha.Text, "yyyymmdd") & "')"
        CmdGlobal.ExecuteNonQuery()
        psSalida = psCodDespacho
        Dim psCantItem As Double : psCantItem = 0
        Dim psItemSerie As Integer : psItemSerie = 0
        Dim psItemAcc As Integer : psItemAcc = 0

        ''detalle

        CmdGlobal.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET_SINSERIE( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM,ARTICULO_CODIGO,DESPD_CANTXDESP,DESPD_CANT_DESP,DESPD_CANT_REC,DESPD_CANT_FALT_REC,DESPD_SYS_EST,DESPD_MOTIVO) " _
                              & " VALUES('" & Session("CodEmpresa") & "'," & psCodDespacho & ",1," & psCodArt & "," & psCant & "," & psCant & "," & psCant & ",0,'0','5')"
        CmdGlobal.ExecuteNonQuery()
        CmdGlobal.CommandText = " INSERT TBINV_SALIDA_MOTIVO_DET_SINSERIE(EMPRESA_CODIGO, ALLSAL_CODIGO, ALLSALD_ITEM, ALLSALD_ARTICULO, ALLSALD_CANT, ALLSALD_CANT_REC, " _
                              & " ALLSALD_CANT_XDEVOL, ALLSALD_CANT_FALTDEVOL, ALLSALD_CANT_DEVOL, ALLSALD_ESTADO_ENVIO, ALLSALD_ESTADO, ALLSALD_SYS_EST) " _
                              & " VALUES('" & Session("CodEmpresa") & "'," & psCodAllSal & ",1," & psCodArt & "," & psCant & "," & psCant & "," _
                              & " " & psCant & "," & psCant & ",0,'2','1','0')"
        CmdGlobal.ExecuteNonQuery()

        'STOCK
        StockAc = 0
        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodOrigen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
        Rs = CmdGlobal.ExecuteReader()
        If Rs.HasRows Then
            While Rs.Read
                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) - psCant
                CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodOrigen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                CmdGlobal2.ExecuteNonQuery()
            End While
        End If
        Rs.Close

        'MOVIMIENTO GENERAL
        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
        Rs = CmdGlobal.ExecuteReader()
        If Rs.HasRows Then
            While Rs.Read
                lblNroMovimiento = Nz(Rs(0)) + 1
            End While
        Else
            lblNroMovimiento = 1
        End If
        Rs.Close

        'Call Movimiento_Kardex(psCodDespacho, "5", psCodArt, psTipoOrigen, psCodOrigen, psTipoDestino, psCodDestino, "", "2", TxtFecha.Text, psCant)

        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                              & " CODIGO_ARTICULO, NRO_ARTICULO,  MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                              & " VALUES ('" & Session("CodEmpresa") & "'," & lblNroMovimiento & ",'2','" & psTipoOrigen & "','" & psCodOrigen & "', " _
                              & " " & psCodArt & "," & psCant & ",'3','5','" & Format(TxtFecha.Text, "yyyymmdd") & "','0','" & psCodDespacho & "','" & psTipoDestino & "'," & psCodDestino & ")"
        CmdGlobal.ExecuteNonQuery()

        '--------------------------recepcion en ccosto O ALMACEN
        'STOCK
        CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                        & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
        Rs = CmdGlobal.ExecuteReader()
        If Rs.HasRows Then
            While Rs.Read
                StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + psCant
                CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & psTipoDestino & "') " _
                                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & Session("CodEmpresa") & "')"
                CmdGlobal2.ExecuteNonQuery()

            End While
        Else
            CmdGlobal.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                  & " VALUES(" & psCodDestino & ",'" & psTipoDestino & "'," & psCodArt & "," & psCant & ",'0','" & Session("CodEmpresa") & "')"
            CmdGlobal.ExecuteNonQuery()
        End If
        Rs.Close

        'MOVIMIENTO GENERAL
        CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
        Rs = CmdGlobal.ExecuteReader()
        If Rs.HasRows Then
            While Rs.Read
                lblNroMovimiento = Nz(Rs(0)) + 1
            End While
        Else
            lblNroMovimiento = 1
        End If
        Rs.Close
        'Call Movimiento_Kardex(psCodDespacho, "5", psCodArt, psTipoDestino, psCodDestino, psTipoOrigen, psCodOrigen, "", "1", TxtFecha.Text, psCant)

        CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                              & " CODIGO_ARTICULO, NRO_ARTICULO,  MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                              & " VALUES ('" & Session("CodEmpresa") & "','" & lblNroMovimiento & "','1','" & psTipoDestino & "'," & psCodDestino & ", " _
                              & " " & psCodArt & "," & psCant & ",'3','5','" & Format(TxtFecha.Text, "yyyymmdd") & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodOrigen & "')"
        CmdGlobal.ExecuteNonQuery()

        psSalida = psCodDespacho
    End Sub
    Private Sub GenerarGuia(sender As Object, e As EventArgs, ByVal psDestinoCodigo As String, ByVal psDestinoCodInterno As String, ByVal psDestino As String, ByVal psDestinoDireccion As String, ByVal psCantProductos As Integer, ByRef pArrayP As String(,), Optional psObservacion As String = "")
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim ValorSys As String
        ValorSys = FechaActual() & HoraActual() & Session("User")

        Dim psGuiaNro2 As Double = 0
        Dim psGuiaNro As String = ""
        psGuiaNro = TxtGuiaNumero.Text
        Dim psGuiaCodigo As String = ""
        CmdGlobal.CommandText = "SELECT MAX(GUIREM_CODIGO) FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psGuiaCodigo = Llenar_Ceros(Nz(Rs(0)) + 1, 10)
            End While
        Else
            psGuiaCodigo = "0000000001"
        End If
        Rs.Close()
NuevaGuia:
        psGuiaNro = TxtGuiaNumero.Text
        CmdGlobal.CommandText = "SELECT GUIREM_CODIGO FROM TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " WHERE GUIREM_SERIE='" & TxtGuiaSerie.Text.Trim & "' AND GUIREM_NUMERO='" & Trim(psGuiaNro) & "'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psGuiaNro2 = Nz(psGuiaNro) + 1
                psGuiaNro = psGuiaNro2
                TxtGuiaNumero.Text = Llenar_Ceros(psGuiaNro2, 8)
                Rs.Close()
                GoTo NuevaGuia
            End While
        Else
            Rs.Close()
        End If
        CmdGlobal.CommandText = "INSERT INTO TBINV_GUIA_REMISION_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUIREM_TIPO, GUIREM_SERIE,GUIREM_NUMERO, GUIREM_SYS_EST,GUIREM_SYS_CRE,GUIREM_RECEPCIONADA,GUIREM_ESTADO) " _
                            & " VALUES(" & psGuiaCodigo & ",'1','" & TxtGuiaSerie.Text & "','" & Trim(psGuiaNro) & "','0','" & ValorSys & "','0','0')"
        CmdGlobal.ExecuteNonQuery()

        CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_SERIE SET GURESE_VALOR_INICIAL = " & Val(psGuiaNro) + 1 & "  WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND GURESE_NUMERO = '" & TxtGuiaSerie.Text & "' AND GURESE_TIPO_DOC='09'"
        CmdGlobal.ExecuteNonQuery()

        Dim psModalidadTransporte As String = ""
        If DdlModTransporte.SelectedValue <> "< Seleccionar >" Then psModalidadTransporte = DdlModTransporte.SelectedValue.Trim

        If ls_CodTicket <> "" Then  'NUM_TICKET
            'Call Ticket_GrabarTrackingAcciones(ls_CodTicket, ls_CodAccion, Format(txtFechaRecep, "yyyymmdd"), Format(txtHoraRecep, "hhmm"), psGuiaCodigo, "NUM_TICKET", "GUIREM_CODIGO", "TBINV_GUIA_REMISION_" & SistCodEmpresa)
        End If
        CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET GUIREM_FECHA='" & Format(TxtFecha.Text, "yyyymmdd") & "', GUIREM_HORA='" & Format(TxtHora.Text, "HHmm") & "', GUIREM_USUARO='" & Session("User") & "',  GUIREM_FECHA_TRASLADO='" & Format(TxtFecha.Text, "yyyymmdd") & "', GUIREM_HORA_TRASLADO='" & Format(TxtHora.Text, "HHmm") & "', " _
                            & " GUIREM_TIPO_REMITENTE='1', ALMACEN_CODIGO_REMITENTE=" & lblCodOrigen.Text & ",CECOSE_CODIGO_REMITENTE = NULL,GUIREM_CURRIER = NULL,GUIREM_ESTADO_ENTREGA='1',GUIREM_ESTADO_SITUACION='2'," _
                            & " GUIREM_DIRECCION_PARTIDA ='" & TxtPuntoPartida.Text & "',GUIREM_TIPO_DESTINATARIO='2',ALMACEN_CODIGO_DESTINATARIO=NULL," _
                            & " CECOSE_CODIGO_DESTINATARIO=" & psDestinoCodigo & ", GUIREM_NOMBRE_DESTINATARIO = '" & Trim(psDestino) & "', GUIREM_DIRECCION_LLEGADA ='" & Trim(psDestinoDireccion) & "'," _
                            & " GUIREM_MOTIVO_TRASLADO='7' ,TRANSPORTISTA_MODALIDAD = '" & psModalidadTransporte & "',GUIREM_DIRECCION_PARTIDA_UBIGEO = '" & IIf(TxtUbigeo.Text <> "000000", TxtUbigeo.Text, "") & "' " _
                            & " WHERE GUIREM_CODIGO = " & psGuiaCodigo
        CmdGlobal.ExecuteNonQuery()
        CmdGlobal.CommandText = "UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  " _
                & " TRANSPORTISTA_RUC='" & TxtRucTrasnportista.Text & "',TRANSPORTISTA_RAZONSOCIAL='" & TxtRazonTransportista.Text & "'," _
                & " VEHICU_PLACA='" & TxtNroPlaca.Text & "',VEHICU_MARCA='" & TxtMarca.Text & "',VEHICU_CERT_INSCP='" & TxtCertInscripcion.Text & "',CHOFER_DNI='" & TxtChoferDNI & "',CHOFER_NOMBRES='" & TxtChoferNombre.Text & "',CHOFER_LICENCIA='" & TxtLicencia.Text & "', VEHI_CONFIGURACION = '" & TxtconfVehicular.Text & "', " _
                & " GUIREM_SYS_MOD='" & ValorSys & "'  WHERE GUIREM_CODIGO = " & psGuiaCodigo
        CmdGlobal.ExecuteNonQuery()
        Dim psObsGuia As String = ""
        For i = 1 To psCantProductos
            psObsGuia = psObservacion & " / " & pArrayP(i, 4)
            CmdGlobal2.CommandText = " UPDATE TBINV_GUIA_REMISION_" & Session("CodEmpresa") & " SET  " _
                                  & " GUIREM_OBSERVACION='" & psObsGuia & "'  WHERE GUIREM_CODIGO = " & psGuiaCodigo
            CmdGlobal2.ExecuteNonQuery()
            CmdGlobal2.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO, ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                  & " VALUES(" & psGuiaCodigo & "," & i & "," & pArrayP(i, 1) & "," & pArrayP(i, 2) & "," & pArrayP(i, 3) & ")"
            CmdGlobal2.ExecuteNonQuery()

            CmdGlobal2.CommandText = "INSERT INTO TBINV_GUIA_REMISION_DETALLE_SINSERIE_" & Session("CodEmpresa") & "(GUIREM_CODIGO, GUREDE_ITEM, DESP_CODIGO,ARTICULO_CODIGO,GUREDE_CANTIDAD) " _
                                  & " VALUES(" & psGuiaCodigo & "," & i & "," & pArrayP(i, 1) & "," & pArrayP(i, 2) & "," & pArrayP(i, 3) & ")"
            CmdGlobal2.ExecuteNonQuery()

            CmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET GUIREM_CODIGO=" & psGuiaCodigo & " WHERE EMPRESA_CODIGO='" & Session("CodEmpresa") & "' AND DESP_CODIGO=" & pArrayP(i, 1)
            CmdGlobal2.ExecuteNonQuery()

            If ls_CodTicket <> "" Then
                CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_TICKET = " & ls_CodTicket & " " _
                                  & " WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' AND DESP_CODIGO = " & pArrayP(i, 1)
                CmdGlobal2.ExecuteNonQuery()
            End If
        Next
        TxtGuiaSerie_TextChanged(sender, e)
    End Sub
    Protected Sub optOrigen_CheckedChanged(sender As Object, e As EventArgs) Handles optOrigen.CheckedChanged
        txtCodOrigen.Text = ""
        lblCodOrigen.Text = ""
        txtDescripcion.Text = ""
        LblError.Text = ""
        If optOrigen.Checked = "true" Then
            BtnBuscarOrigen.Enabled = True
            Lbl6.Text = "Origen - Busqueda Almacen"
        End If
    End Sub

    Protected Sub optOrigen2_CheckedChanged(sender As Object, e As EventArgs) Handles optOrigen2.CheckedChanged
        txtCodOrigen.Text = ""
        lblCodOrigen.Text = ""
        txtDescripcion.Text = ""
        LblError.Text = ""
        If optOrigen2.Checked = "true" Then
            BtnBuscarOrigen.Enabled = True
            Lbl6.Text = "Origen - Busqueda Centro de Costos"
        End If
    End Sub
    Protected Sub BtnBuscarOrigen_Click(sender As Object, e As EventArgs) Handles BtnBuscarOrigen.Click
        TituloPopup.Text = Lbl6.Text
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim obj As New clsInv_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodigo As Double = 0
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""
        If TituloPopup.Text = "Origen - Busqueda Almacen" Then
            If BuscarCodigo.Value.ToString <> "" Then psBusCodigo = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaAlmacen(psconexion, Session("CodEmpresa"), psBusCodigo, descripcion)
        Else
            If BuscarCodigo.Value.ToString <> "" Then psBusCodInterno = BuscarCodigo.Value
            descripcion = BuscarDescripcion.Value.Trim.ToString
            dt = obj.Lista_BusquedaCentroCosto(psconexion, Session("CodEmpresa"), psBusCodInterno, descripcion)
        End If
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('show');", True)
        GvBusqueda.DataSource = dt
        GvBusqueda.DataBind()
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Limpiar_Popup()
    End Sub

    Protected Sub Limpiar_Popup()
        BuscarCodigo.Value = ""
        BuscarDescripcion.Value = ""
        GvBusqueda.DataSource = Nothing
        GvBusqueda.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#myModal').modal('hide');", True)
    End Sub
    Protected Sub GvBusqueda_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusqueda.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "Aceptar" And TituloPopup.Text = "Origen - Busqueda Almacen" Then
            txtCodOrigen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            lblCodOrigen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtPuntoPartida.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtUbigeo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Limpiar_Popup()
        Else
            txtCodOrigen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            txtDescripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            lblCodOrigen.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtPuntoPartida.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            TxtUbigeo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusqueda.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            Limpiar_Popup()
        End If

    End Sub

    Private Sub BtnTransporte_Click(sender As Object, e As EventArgs) Handles BtnTransporte.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('show');", True)
    End Sub

    Private Sub BtnBusTransporte_Click(sender As Object, e As EventArgs) Handles BtnBusTransporte.Click
        Dim obj As New clsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        If TxtBusTransRUC.Value.ToString <> "" Then psBusCodInterno = TxtBusTransRUC.Value
        descripcion = TxtBusTransRazon.Value.Trim.ToString
        dt = obj.Cont_BusquedaPersonas(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion, "4")

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('show');", True)
        GvTransporte.DataSource = dt
        GvTransporte.DataBind()
    End Sub
    Protected Sub Limpiar_Popup_Transporte()
        TxtBusTransRazon.Value = ""
        TxtBusTransRUC.Value = ""
        GvTransporte.DataSource = Nothing
        GvTransporte.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalTransporte').modal('hide');", True)
    End Sub

    Private Sub BtnCancelarTrans_Click(sender As Object, e As EventArgs) Handles BtnCancelarTrans.Click
        Call Limpiar_Popup_Transporte()
    End Sub

    Private Sub GvTransporte_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvTransporte.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtRucTrasnportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtRazonTransportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtCertInscripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodTrasporte.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvTransporte.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Transporte()

    End Sub
    Private Sub BtnVehiculo_Click(sender As Object, e As EventArgs) Handles BtnVehiculo.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('show');", True)
    End Sub

    Private Sub BtnVehiculoBuscar_Click(sender As Object, e As EventArgs) Handles BtnVehiculoBuscar.Click
        Dim obj As New clsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        psBusCodInterno = TxtBusPlaca.Value.Trim.ToString
        descripcion = TxtBusMarca.Value.Trim.ToString
        dt = obj.Cont_BusquedaVehiculo(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('show');", True)
        GvVehiculo.DataSource = dt
        GvVehiculo.DataBind()
    End Sub
    Protected Sub Limpiar_Popup_Vehiculo()
        TxtBusPlaca.Value = ""
        TxtBusMarca.Value = ""
        GvVehiculo.DataSource = Nothing
        GvVehiculo.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalVehiculo').modal('hide');", True)
    End Sub

    Private Sub BtnVehiculoCerrars_Click(sender As Object, e As EventArgs) Handles BtnVehiculoCerrar.Click
        Call Limpiar_Popup_Vehiculo()
    End Sub

    Private Sub GvVehiculo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvVehiculo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtNroPlaca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtMarca.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtconfVehicular.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtRucTrasnportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtRazonTransportista.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        TxtCertInscripcion.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(6).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodVehiculo.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvVehiculo.Rows(Index).Cells(7).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Vehiculo()

    End Sub


    Private Sub BtnChofer_Click(sender As Object, e As EventArgs) Handles BtnChofer.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('show');", True)
    End Sub

    Private Sub BtnChoferBuscar_Click(sender As Object, e As EventArgs) Handles BtnChoferBuscar.Click
        Dim obj As New clsCont_Listados
        Dim objCn As New Cls_Conexion
        Dim dt As New DataTable
        Dim psconexion As String = Session("Ruta_Emp")
        Dim psBusCodInterno As String = ""
        Dim descripcion As String = ""

        psBusCodInterno = TxtBusChoferDni.Value.Trim.ToString
        descripcion = TxtBusChoferNombres.Value.Trim.ToString
        dt = obj.Cont_BusquedaChofer(Session("CodEmpresa"), psconexion, psBusCodInterno, descripcion)

        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('show');", True)
        GvChofer.DataSource = dt
        GvChofer.DataBind()
    End Sub
    Protected Sub Limpiar_Popup_Chofer()
        TxtBusChoferDni.Value = ""
        TxtBusChoferNombres.Value = ""
        GvChofer.DataSource = Nothing
        GvChofer.DataBind()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalChofer').modal('hide');", True)
    End Sub

    Private Sub BtnChoferCerrar_Click(sender As Object, e As EventArgs) Handles BtnChoferCerrar.Click
        Call Limpiar_Popup_Chofer()
    End Sub

    Private Sub GvChofer_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvChofer.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)

        TxtChoferDNI.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtChoferNombre.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        TxtLicencia.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
        lblCodChofer.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvChofer.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
        Limpiar_Popup_Chofer()

    End Sub

    Private Sub BtnBuscarArt_Click(sender As Object, e As EventArgs) Handles BtnBuscarArt.Click
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('show');", True)
    End Sub
    Protected Sub Limpiar_Cajas_Buscar_Articulos()
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtCodArticuloBA.Value = ""
        TxtClasificacionBA.Value = ""
        TxtDescripcionBA.Value = ""
        LblCodClasificacionBA.Text = ""
        DdlTipoBA.SelectedValue = "< Seleccionar >"
        TxtNumParteBA.Value = ""
        TxtCodEspecificoBA.Value = ""
        TxtMarcaBA.Value = ""
        TxtModeloBA.Value = ""
        LblCodMarcaBA.Text = ""
        LblCodModeloBA.Text = ""
    End Sub

    Private Sub BtnCerrarBA_Click(sender As Object, e As EventArgs) Handles BtnCerrarBA.Click
        Call Limpiar_Cajas_Buscar_Articulos()
        ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
    End Sub

    Private Sub BtnBuscarBA_Click(sender As Object, e As EventArgs) Handles BtnBuscarBA.Click
        Dim obj As New Cls_Catalogo
        Dim dt As New DataTable
        Dim psListaArt As String = "1"
        Dim psListaMarca As String = "1"
        Dim psListaModelo As String = "1"
        Dim psconexion As String = Session("Ruta_Emp")
        Dim Codigo As String = TxtCodArticuloBA.Value.ToString
        Dim Clasificacion As String = LblCodClasificacionBA.Text.ToString

        Dim Descripcion As String = TxtDescripcionBA.Value.ToString
        Dim Tipo As String = DdlTipoBA.SelectedValue.ToString
        Dim NuPart As String = TxtNumParteBA.Value.ToString
        Dim CodEs As String = TxtCodEspecificoBA.Value.ToString
        Dim marca As String = LblCodMarcaBA.Text.ToString
        Dim modelo As String = LblCodModeloBA.Text.ToString

        If marca <> "" Then psListaMarca = ""
        If modelo <> "" Then psListaModelo = ""
        If Codigo <> "" Then psListaArt = ""
        If Tipo = "< Seleccionar >" Then Tipo = ""

        dt = obj.Bus_Articulo(psconexion, Codigo, Clasificacion, Descripcion, Tipo, NuPart, CodEs, marca, modelo, psListaArt, psListaMarca, psListaModelo)

        If dt.Rows.Count > 0 Then
            GvBusArticulo.DataSource = dt
            GvBusArticulo.DataBind()
        Else
            GvBusArticulo.DataSource = Nothing
            GvBusArticulo.DataBind()
        End If
        Limpiar_Cajas_Buscar_Articulos()
    End Sub

    Private Sub GvBusArticulo_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvBusArticulo.RowCommand
        Dim Index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim dt As New DataTable
        Dim drT As DataRow

        If e.CommandName = "Aceptar" Then
            dt.Columns.Add("ART_CODIGO")
            dt.Columns.Add("ART_DESCRIPCION")
            dt.Columns.Add("ART_CODEQUIVA")
            dt.Columns.Add("TIPO_ART")
            dt.Columns.Add("CANT_COL_EXCEL")

            For Each row As GridViewRow In GvListaArticulos.Rows
                Dim txtValor As System.Web.UI.WebControls.TextBox = CType(row.FindControl("txtCant"), System.Web.UI.WebControls.TextBox)
                ' Aquí puedes acceder y manipular el valor del TextBox
                Dim valor As String = txtValor.Text

                drT = dt.NewRow()
                drT("ART_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                drT("TIPO_ART") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                If valor <> "" Then
                    drT("CANT_COL_EXCEL") = valor
                Else
                    drT("CANT_COL_EXCEL") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
                End If
                dt.Rows.Add(drT)
            Next
            drT = dt.NewRow()
            drT("ART_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°")
            drT("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
            drT("TIPO_ART") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(GvBusArticulo.Rows(Index).Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°") '
            drT("CANT_COL_EXCEL") = ""
            dt.Rows.Add(drT)

            GvListaArticulos.DataSource = dt
            GvListaArticulos.DataBind()

            Limpiar_Cajas_Buscar_Articulos()
            ScriptManager.RegisterStartupScript(Me, Me.Page.GetType, "", "$('#ModalArticulo').modal('hide');", True)
        End If

    End Sub

    Private Sub GvListaArticulos_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GvListaArticulos.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim txtValorCant As System.Web.UI.WebControls.TextBox = CType(e.Row.FindControl("txtCant"), System.Web.UI.WebControls.TextBox)
            ' Aquí puedes acceder y manipular el valor del TextBox
            Dim valor As String = txtValorCant.Text
        End If
    End Sub
    Protected Sub TxtGuiaSerie_TextChanged(sender As Object, e As EventArgs) Handles TxtGuiaSerie.TextChanged
        Dim psCodGuia As String = ""
        If TxtGuiaSerie.Text.Trim = "" Then Exit Sub
        Dim Cn As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim Cn2 As New SqlClient.SqlConnection(Session("Ruta_Emp"))
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        CmdGlobal.CommandText = " SELECT DOC_CODIGO FROM TBDOCUMENTOS WHERE DOC_EMPRESA = '" & Session("CodEmpresa") & "' " _
                               & " AND DOC_SYS_EST ='0' AND DOC_AÑO='" & AñoActual(Session("CodEmpresa"), Session("Ruta_Emp")) & "' AND (DOC_CODIGO)='09' "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psCodGuia = Nu(Rs!DOC_CODIGO)
            End While
        End If
        Rs.Close()

        'verificar nro serie valida
        CmdGlobal.CommandText = "  SELECT GURESE_VALOR_INICIAL,GURESE_NUMERO FROM TBINV_GUIA_REMISION_SERIE WHERE EMPRESA_CODIGO = '" & Session("CodEmpresa") & "' " _
                              & " AND GURESE_NUMERO LIKE '" & TxtGuiaSerie.Text & "%' AND GURESE_SYS_EST ='0' AND GURESE_TIPO_DOC='09'"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                TxtGuiaSerie.Text = Nu(Rs!GURESE_NUMERO)
                TxtGuiaNumero.Text = Format(Nz(Rs!GURESE_VALOR_INICIAL), "00000000")
            End While
        Else
            LblError.Text = "El Nro de Serie ingresada no es válido."
            TxtGuiaSerie.Text = ""
            TxtGuiaNumero.Text = ""
        End If
        Rs.Close()
        Cn.Close()

    End Sub

    Private Sub GvListaArticulos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvListaArticulos.RowCommand
        Dim dt As New DataTable
        Dim drt As DataRow

        dt.Columns.Add("ART_CODIGO")
        dt.Columns.Add("ART_DESCRIPCION")
        dt.Columns.Add("ART_CODEQUIVA")
        dt.Columns.Add("TIPO_ART")
        dt.Columns.Add("CANT_COL_EXCEL")

        For Each row As GridViewRow In GvListaArticulos.Rows
            drt = dt.NewRow()
            drt("ART_CODIGO") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(1).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("ART_DESCRIPCION") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(2).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("ART_CODEQUIVA") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(3).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("TIPO_ART") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(4).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            drt("CANT_COL_EXCEL") = Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(row.Cells(5).Text.Trim, "&#250;", "ú"), "&#243;", "ó"), "&#237;", "í"), "&#233;", "é"), "&#225;", "á"), "&quot;", """"), "&#191;", "¿"), "&#241;", "ñ"), "&lt;", "<"), "&gt;", ">"), "&#176;", "º"), "&#211;", "Ó"), "&#193;", "Á"), "&#201;", "É"), "&#205;", "Í"), "&#218;", "Ú"), "&nbsp;", ""), "&#242;", "ò"), "&#236;", "ì"), "&#200;", "È"), "&#209;", "Ñ"), "&#180;", "´"), "&#186;", "°"), "&amp;", "&")
            dt.Rows.Add(drt)
        Next

        If e.CommandName = "QuitarArt" Then
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)
            ' Asegúrate de que rowIndex esté dentro del rango válido de filas.
            If rowIndex >= 0 AndAlso rowIndex < dt.Rows.Count Then
                dt.Rows.RemoveAt(rowIndex) ' Elimina la fila del DataTable.
                GvListaArticulos.DataSource = dt ' Vuelve a vincular el GridView para reflejar el cambio.
                GvListaArticulos.DataBind()
            End If
        End If
    End Sub
End Class
