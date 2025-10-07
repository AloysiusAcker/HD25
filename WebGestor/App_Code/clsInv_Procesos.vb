Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Security
Imports WebGestor
Public Class clsInv_Procesos

    Public Sub Guardar_Serie()
        '
    End Sub
    Public Sub Guardar_UltimosMovimiento_paraGPS(ByVal ps_Conexion As String, ByVal ps_CodEmpresa As String, ByVal pd_PlacaNro As Double,
                                                  ByVal ps_FechaMov As String, ByVal ps_OrigenTipo As String, ByVal pd_OrigenCodigo As Double,
                                                  ByVal ps_DestinoTipo As String, ByVal pd_DestinoCodigo As Double, ByVal ps_SerieNumerar As Double,
                                                  ByVal ps_User As String)
        Dim CnU As New SqlClient.SqlConnection(ps_Conexion)
        Dim CmdGlobalU As New SqlCommand
        Dim RsU As SqlDataReader
        Dim ValorSys As String = ""
        Dim pdCodRegistro As Double = 0

        ValorSys = ps_User & FechaActual() & HoraActual()
        Try

            CnU.Open() : CmdGlobalU.Connection = CnU

            CmdGlobalU.CommandText = "SELECT ISNULL(MAX(isnull(MOVGPS_REGISTRO,0)),0) FROM TBINV_MOVIMIENTO_EQUIPOS_PARAGPS "
            RsU = CmdGlobalU.ExecuteReader
            If RsU.HasRows Then
                While RsU.Read
                    pdCodRegistro = Nz(RsU(0)) + 1
                End While
            Else
                pdCodRegistro = 1
            End If
            RsU.Close()

            CmdGlobalU.CommandText = " INSERT INTO TBINV_MOVIMIENTO_EQUIPOS_PARAGPS (EMPRESA_CODIGO, MOVGPS_REGISTRO, MOVGPS_SERIE_NUMERAR, MOVGPS_PLACA_NRO, " _
                                   & " MOVGPS_FECHA, MOVGPS_ORIGEN_TIPO, MOVGPS_ORIGEN_CODIGO, MOVGPS_DESTINO_TIPO, MOVGPS_DESTINO_CODIGO, MOVGPS_SYS_CRE, MOVGPS_SYS_EST) " _
                                   & " VALUES ('" & ps_CodEmpresa & "', " & pdCodRegistro & "," & ps_SerieNumerar & ", " & pd_PlacaNro & ", '" & ps_FechaMov & "', " _
                                   & " '" & ps_OrigenTipo & "', " & pd_OrigenCodigo & ", '" & ps_DestinoTipo & "', " & pd_DestinoCodigo & ", '" & ValorSys & "','0')"
            CmdGlobalU.ExecuteNonQuery()


        Catch ex As Exception

        End Try

    End Sub

    Public Sub Anular_SalidasRecibidas(ByVal pdCodSalida As Double, ByVal psMotivoAnulacion As String, ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psUser As String)
        Dim Rs As SqlDataReader
        Dim RsSal As SqlDataReader
        Dim RsSalDet As SqlDataReader
        Dim Rs1 As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim fn As New clsInv_Procesos

        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal3 As New SqlCommand
        Dim Cn4 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal4 As New SqlCommand


        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Cn4.Open() : CmdGlobal4.Connection = Cn4

        Dim psCodigoDestino As String = ""
        Dim psNroAnulaciones As String = ""
        Dim Stock As Double = 0
        Dim StockAc As Double = 0
        Dim Sql As String = ""
        Dim ValorSys As String = ""
        ValorSys = psUser & FechaActual() & HoraActual()
        Dim EstadoUbic As String = ""
        Dim EstadoIngreso As String = ""
        Dim EstadoSalida As String = ""
        Dim psMotivo As String = ""
        Dim psNroMovimiento As String = ""
        Dim psNroMovimiento2 As String = ""
        Dim psMensaje As String = ""

        CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO WHERE DESP_CODIGO = " & pdCodSalida & " AND DESP_SYS_EST ='0' AND EMPRESA_CODIGO ='" & psCodEmpresa & "'"
        RsSal = CmdGlobal.ExecuteReader
        If RsSal.HasRows Then
            While RsSal.Read
                psMotivo = Nu(RsSal!DESP_MOTIVO_GRAL)
                If Nu(RsSal!DESP_TIPODESTINO) = "1" Then
                    psCodigoDestino = Nu(RsSal!ALMACEN_CODIGO_DESTINO)
                ElseIf Nu(RsSal!DESP_TIPODESTINO) = "2" Then
                    psCodigoDestino = Nu(RsSal!CECOSE_CODIGO_DESTINO)
                ElseIf Nu(RsSal!DESP_TIPODESTINO) = "3" Then
                    psCodigoDestino = Nu(RsSal!PROVEEDOR_CODIGO_DESTINO)
                ElseIf Nu(RsSal!DESP_TIPODESTINO) = "4" Then
                    psCodigoDestino = Nu(RsSal!EQUIPO_CODIGO_DESTINO)
                ElseIf Nu(RsSal!DESP_TIPODESTINO) = "5" Then
                    psCodigoDestino = Nu(RsSal!PERSONA_CODIGO_DESTINO)
                ElseIf Nu(RsSal!DESP_TIPODESTINO) = "6" Then
                    psCodigoDestino = Nu(RsSal!CLIENTE_CODIGO_DESTINO)
                End If
                EstadoUbic = psMotivo
                EstadoSalida = EstadoUbic
                Select Case EstadoSalida
                    Case "1" : EstadoIngreso = "6"
                    Case "2" : EstadoIngreso = "2"
                    Case "3" : EstadoIngreso = "6"
                    Case "4" : EstadoIngreso = "4"
                    Case "5"
                    Case "6" : EstadoIngreso = "1"
                    Case "7"
                    Case "8" : EstadoIngreso = "5"
                    Case "9" : EstadoIngreso = "15"
                    Case "10" : EstadoIngreso = "7"
                    Case "11" : EstadoIngreso = "3"
                    Case "12" : EstadoIngreso = "1"
                    Case "13" : EstadoIngreso = "3"
                    Case "17" : EstadoIngreso = "14"
                    Case "18" : EstadoIngreso = "13"
                    Case "27" : EstadoIngreso = "22"
                    Case "29" : EstadoIngreso = "29"
                    Case "31" : EstadoIngreso = "31"
                    Case "33" : EstadoIngreso = "33"
                    Case "34" : EstadoIngreso = "34"
                    Case "35" : EstadoIngreso = "35"
                End Select
                EstadoUbic = EstadoIngreso
                If Nu(RsSal!DESP_ESTADO) = "3" Then 'SALIDA RECIBIDA
                    'CON SERIE
                    CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.SERIE_NUMERAR, S.SERIE_PARATRANSITO, S.ARTICULO_CODIGO, DESPD_COSTO_VENTA_S, DESPD_COSTO_VENTA_D " _
                        & " FROM TBINV_ALMACEN_DESPACHO_DET DES INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S " _
                        & " ON DES.SERIE_NUMERAR = S.SERIE_NUMERAR" _
                        & " WHERE (DES.DESP_CODIGO = '" & pdCodSalida & "') AND (DES.DESPD_SYS_EST = '0') AND " _
                        & " (S.SERIE_SYS_EST = '0') AND (DES.EMPRESA_CODIGO = '" & psCodEmpresa & "')"
                    RsSalDet = CmdGlobal2.ExecuteReader
                    If RsSalDet.HasRows Then
                        While RsSalDet.Read
                            'el articulo_serie ya no esta en transito vuelve a estar en la ubicación de donde salió

                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL,UBICACT_TIPO='1', UBICACT_CODIGO=" & Nu(RsSal!ALMACEN_ORIGEN) & " WHERE SERIE_NUMERAR =" & Nu(RsSalDet!Serie_Numerar)
                            CmdGlobal3.ExecuteNonQuery()
                            If Nu(RsSal!DESP_MOTIVO_GRAL) = "25" Then
                                CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_ESTADO='0' WHERE SERIE_NUMERAR=" & Nu(RsSalDet!Serie_Numerar)
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                            CmdGlobal3.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_CRE, SYS_EST, INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL, MOTIVO)" _
                                                  & " VALUES(" & Nu(RsSalDet!Serie_Numerar) & ",'1'," & Nu(RsSal!ALMACEN_ORIGEN) & ",'" & EstadoUbic & "','" & ValorSys & "','0','" & FechaActual() & "','1'," & pdCodSalida & ",'" & EstadoIngreso & "') "
                            CmdGlobal3.ExecuteNonQuery()
                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodigoDestino & ") AND (UBICACT_TIPO='" & Nu(RsSal!DESP_TIPODESTINO) & "')" _
                            & " AND (ARTICULO_CODIGO = " & Nu(RsSalDet!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs = CmdGlobal3.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read


                                    Stock = Nz(Rs!SAA_STOCK_ACTUAL) - 1
                                    CmdGlobal4.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & Stock & " WHERE (ALMACEN_CODIGO = " & psCodigoDestino & ") " _
                                                      & " AND (ARTICULO_CODIGO = " & Nu(RsSalDet!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "') " _
                                                      & " AND UBICACT_TIPO ='" & Nu(RsSal!DESP_TIPODESTINO) & "'"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            End If
                            Rs.Close()

                            'aqui aumenta el stock de la ubicacion de donde salio el articulo_serie
                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & Nu(RsSal!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                & " AND (ARTICULO_CODIGO = " & Nu(RsSalDet!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs = CmdGlobal3.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                                    CmdGlobal4.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(RsSal!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                                      & " AND (ARTICULO_CODIGO = " & Nu(RsSalDet!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal4.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                         & "VALUES(" & Nu(RsSal!ALMACEN_ORIGEN) & ",'1'," & Nu(RsSalDet!ARTICULO_CODIGO) & ",1,'0','" & psCodEmpresa & "')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs.Close()
                            'mov general

                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & Nz(RsSalDet!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & psNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' AND (CODIGO_TRANS = '" & pdCodSalida & "')"
                            Rs = CmdGlobal3.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    fn.Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(RsSalDet!ARTICULO_CODIGO), Nu(RsSal!DESP_TIPODESTINO), psCodigoDestino, "1", Nu(RsSal!ALMACEN_ORIGEN), "", "2", FormatoFecha(FechaActual), 1, "S", CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_S)), CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_D)))

                                    CmdGlobal4.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET NRO_ARTICULO =" & Nz(Rs!NRO_ARTICULO) + 1 & " WHERE (CODIGO_ARTICULO = " & Nz(RsSalDet!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & psNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' AND (CODIGO_TRANS = '" & pdCodSalida & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal4.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                Rs2 = CmdGlobal4.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        psNroMovimiento = Nz(Rs2(0)) + 1
                                    End While
                                Else
                                    psNroMovimiento = "00000001"
                                End If
                                Rs2.Close()
                                fn.Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(RsSalDet!ARTICULO_CODIGO), Nu(RsSal!DESP_TIPODESTINO), psCodigoDestino, "1", Nu(RsSal!ALMACEN_ORIGEN), "", "2", FormatoFecha(FechaActual), 1, "S", CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_S)), CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_D)))

                                CmdGlobal4.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                                      & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                      & " values('" & psCodEmpresa & "','" & psNroMovimiento & "','2','" & Nu(RsSal!DESP_TIPODESTINO) & "'," & psCodigoDestino & ",'1'," & Nu(RsSal!ALMACEN_ORIGEN) & ", " _
                                                      & " '" & pdCodSalida & "','" & Nu(RsSalDet!ARTICULO_CODIGO) & "','1','" & ValorSys & "','3','22','" & FechaActual() & "','0')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs.Close()

                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & Nz(RsSalDet!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & psNroMovimiento2 & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' AND (CODIGO_TRANS = '" & pdCodSalida & "')"
                            Rs = CmdGlobal3.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    fn.Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(RsSalDet!ARTICULO_CODIGO), "1", Nu(RsSal!ALMACEN_ORIGEN), "1", Nu(RsSal!ALMACEN_ORIGEN), "", "1", FormatoFecha(FechaActual), 1, "S", CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_S)), CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_D)))

                                    CmdGlobal4.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET NRO_ARTICULO =" & Nz(Rs!NRO_ARTICULO) + 1 & " WHERE (CODIGO_ARTICULO = " & Nz(RsSalDet!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & psNroMovimiento2 & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' AND (CODIGO_TRANS = '" & pdCodSalida & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal4.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                Rs2 = CmdGlobal4.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs2.Read
                                        psNroMovimiento2 = Nz(Rs2(0)) + 1
                                    End While
                                Else
                                    psNroMovimiento2 = "00000001"
                                End If
                                Rs2.Close()

                                fn.Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(RsSalDet!ARTICULO_CODIGO), "1", Nu(RsSal!ALMACEN_ORIGEN), "1", Nu(RsSal!ALMACEN_ORIGEN), "", "1", FormatoFecha(FechaActual), 1, "S", CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_S)), CDbl(Nz(RsSalDet!DESPD_COSTO_VENTA_D)))

                                CmdGlobal4.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO," _
                                                      & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                      & " values('" & psCodEmpresa & "','" & psNroMovimiento2 & "','1','1'," & Nu(RsSal!ALMACEN_ORIGEN) & ",'1'," & Nu(RsSal!ALMACEN_ORIGEN) & ", " _
                                                      & " '" & pdCodSalida & "','" & Nu(RsSalDet!ARTICULO_CODIGO) & "','1','" & ValorSys & "','6','22','" & FechaActual() & "','0')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs.Close()

                            'Anula Alquiler
                        End While
                    End If
                    RsSalDet.Close()
                    'accesorios

                    CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.ARTICULO_CODIGO, DES.DESPD_CANTXDESP, DESPD_COSTO_VENTA_D, DESPD_COSTO_VENTA_S  " _
                        & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE DES WHERE DES.EMPRESA_CODIGO = '" & psCodEmpresa & "' AND (DES.DESP_CODIGO = '" & pdCodSalida & "') AND (DES.DESPD_SYS_EST = '0')"
                    Rs1 = CmdGlobal2.ExecuteReader
                    If Rs1.HasRows Then
                        While Rs1.Read
                            'DISMINUYE EL STOCK DE SU DESTINO
                            CmdGlobal3.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodigoDestino & ") AND (UBICACT_TIPO='" & Nu(RsSal!DESP_TIPODESTINO) & "')" _
                                & " AND (ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs2 = CmdGlobal3.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    Stock = Nz(Rs2!SAA_STOCK_ACTUAL) - CDbl(Nu(Rs1!DESPD_CANTXDESP))
                                    CmdGlobal4.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & Stock & " WHERE (ALMACEN_CODIGO = " & psCodigoDestino & ") " _
                                                      & " AND (ARTICULO_CODIGO = " & Nu(Rs2!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "') " _
                                                      & " AND UBICACT_TIPO ='" & Nu(RsSal!DESP_TIPODESTINO) & "'"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            End If
                            Rs2.Close()
                            'AUMENTA STOCK DE DONDE SALIO EL ARTICULO
                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & Nu(RsSal!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                & " AND (ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs2 = CmdGlobal3.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    StockAc = Nz(Rs2!SAA_STOCK_ACTUAL) + CDbl(Nu(Rs1!DESPD_CANTXDESP))
                                    CmdGlobal4.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(RsSal!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                                      & " AND (ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                StockAc = CDbl(Nu(Rs1!DESPD_CANTXDESP))
                                CmdGlobal4.CommandText = " INSERT INTO TBINV_STOCK_ARTICULOS_ALMACEN (SAA_STOCK_ACTUAL,ALMACEN_CODIGO,UBICACT_TIPO,EMPRESA_CODIGO,ARTICULO_CODIGO,SAA_SYS_EST)" _
                                                      & " VALUES (" & StockAc & "," & Nu(RsSal!ALMACEN_ORIGEN) & " ,'1','" & psCodEmpresa & "'," & Nu(Rs1!ARTICULO_CODIGO) & ",'0')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs2.Close()
                            'paso2

                            'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                            CmdGlobal3.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            Rs2 = CmdGlobal3.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    psNroMovimiento = Nz(Rs2(0)) + 1
                                End While
                            Else
                                psNroMovimiento = "00000001"
                            End If
                            Rs2.Close()
                            ''''''''''''SALE ERROR
                            fn.Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(Rs1!ARTICULO_CODIGO), "1", Nu(RsSal!ALMACEN_ORIGEN), "1", Nu(RsSal!ALMACEN_ORIGEN), "", "1", FormatoFecha(FechaActual), Nz(Rs1!DESPD_CANTXDESP), "S", CDbl(Nz(Rs1!DESPD_COSTO_VENTA_S)), CDbl(Nz(Rs1!DESPD_COSTO_VENTA_D)))

                            CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO," _
                                                  & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                  & " values('" & psCodEmpresa & "','" & psNroMovimiento & "','1','1'," & Nu(RsSal!ALMACEN_ORIGEN) & ",'1'," & Nu(RsSal!ALMACEN_ORIGEN) & ", " _
                                                  & " '" & pdCodSalida & "','" & Nu(Rs1!ARTICULO_CODIGO) & "'," & Nz(Rs1!DESPD_CANTXDESP) & ",'" & ValorSys & "','6','22','" & FechaActual() & "','0')"
                            CmdGlobal3.ExecuteNonQuery()

                        End While
                    End If
                    Rs1.Close()

                    Dim psGuiaCodigo As String = ""

                    CmdGlobal2.CommandText = " SELECT GUIREM_CODIGO FROM TBINV_ALMACEN_DESPACHO WHERE DESP_CODIGO = " & pdCodSalida & " AND DESP_SYS_EST = '0' AND EMPRESA_CODIGO = '" & psCodEmpresa & "' "
                    Rs1 = CmdGlobal2.ExecuteReader
                    If Rs1.HasRows Then
                        While Rs1.Read
                            psGuiaCodigo = Nu(Rs1!GUIREM_CODIGO)
                        End While
                    End If
                    Rs1.Close()

                    If psGuiaCodigo <> "" Then
                        CmdGlobal2.CommandText = " UPDATE TBINV_PERSONAS_PEDIDO SET " _
                                              & " GUIREM_CODIGO = '', " _
                                              & " PEDIDO_ESTADO = '0', " _
                                              & " ESTADO_GUIA = '0' " _
                                              & " WHERE PEDIDO_SYS_EST = '0' " _
                                              & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "' " _
                                              & " AND GUIREM_CODIGO = " & psGuiaCodigo & ""
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                ElseIf Nu(RsSal!DESP_ESTADO) = "1" Or Nu(RsSal!DESP_ESTADO) = "5" Then
                    'LIBERAR LOS ARTICULOS_SERIES
                    CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.SERIE_NUMERAR, S.SERIE_PARATRANSITO" _
                                           & " FROM TBINV_ALMACEN_DESPACHO_DET DES INNER JOIN " _
                                           & " dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON DES.SERIE_NUMERAR = S.SERIE_NUMERAR" _
                                           & " WHERE (DES.DESP_CODIGO = " & pdCodSalida & ") AND (DES.DESPD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND DES.EMPRESA_CODIGO = '" & psCodEmpresa & "'"
                    Rs1 = CmdGlobal2.ExecuteReader
                    If Rs1.HasRows Then
                        While Not Rs1.Read
                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR =" & Nu(Rs1!Serie_Numerar)
                            CmdGlobal3.ExecuteNonQuery()
                        End While
                    End If
                    Rs1.Close()
                    'LIBERAR LAS CANTIDADES DE ARTICULOS
                    CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.ARTICULO_CODIGO, DES.DESPD_CANTXDESP " _
                                           & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE DES " _
                                           & " WHERE DES.EMPRESA_CODIGO = '" & psCodEmpresa & "' AND (DES.DESP_CODIGO = " & pdCodSalida & ") " _
                                           & " AND (DES.DESPD_SYS_EST = '0')"
                    Rs1 = CmdGlobal2.ExecuteReader
                    If Rs1.HasRows Then
                        While Rs1.Read
                            CmdGlobal3.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET " _
                                                  & " SAA_PARATRANSITO = ISNULL(SAA_PARATRANSITO,0) - " & Nz(Rs1!DESPD_CANTXDESP) & " " _
                                                  & " WHERE UBICACT_TIPO='1' AND ALMACEN_CODIGO=" & Nu(RsSal!ALMACEN_ORIGEN) & " " _
                                                  & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "' AND ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO)
                            CmdGlobal3.ExecuteNonQuery()

                        End While
                    End If
                    Rs1.Close()
                End If
                'PARA CUALQUIER ESTADO DE LA SALIDA GENERADA,GENERADA SIN SERIE,RECIBIDA O ENVIADA
                'se cambia de estado a la salida de 2 a 6:anulada
                CmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_ESTADO ='6',DESP_MOTIVO_ANULACION='POR ERROR' WHERE EMPRESA_CODIGO ='" & psCodEmpresa & "' AND DESP_SYS_EST ='0' AND DESP_CODIGO=" & pdCodSalida
                CmdGlobal2.ExecuteNonQuery()
                'se anulada la guia cambianto de estado para esto agregue un campo mas en la tabla guirem_estado de 0:generado a 1:anulado
                CmdGlobal2.CommandText = " SELECT G.GUIREM_CODIGO, GD.DESP_CODIGO FROM dbo.TBINV_GUIA_REMISION_" & psCodEmpresa & " G INNER JOIN " _
                    & " dbo.TBINV_GUIA_REMISION_DETALLE_" & psCodEmpresa & " GD ON G.GUIREM_CODIGO = GD.GUIREM_CODIGO " _
                    & " WHERE (G.GUIREM_SYS_EST = '0') AND (GD.DESP_CODIGO = " & pdCodSalida & ") AND (G.GUIREM_ESTADO='0')"
                Rs = CmdGlobal2.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        CmdGlobal3.CommandText = " UPDATE TBINV_GUIA_REMISION_" & psCodEmpresa & " SET GUIREM_ESTADO='1',GUIREM_SYS_ANULADA = '" & ValorSys & "' " _
                                          & " WHERE (GUIREM_CODIGO = '" & Nu(Rs!GUIREM_CODIGO) & "') AND (GUIREM_ESTADO='0') AND (GUIREM_SYS_EST='0')"
                        CmdGlobal3.ExecuteNonQuery()
                        If Existe_Tabla("TBINV_PERSONAS_PEDIDO", psConexion) = True Then
                            CmdGlobal3.CommandText = " UPDATE TBINV_PERSONAS_PEDIDO SET " _
                                                  & " ESTADO_GUIA ='0' , " _
                                                  & " GUIREM_CODIGO = NULL " _
                                                  & " " _
                                                  & " WHERE GUIREM_CODIGO = " & Nu(Rs!GUIREM_CODIGO) & " AND PEDIDO_ESTADO = '0' "
                            CmdGlobal3.ExecuteNonQuery()
                        End If
                    End While
                End If
                Rs.Close()
                ' se graba en la tabla de anulaciones
                Dim lblNroAnulaciones As Integer = 0
                CmdGlobal2.CommandText = "SELECT MAX(ANUL_NRO) FROM TBINV_ANULACIONES "
                Rs = CmdGlobal2.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        psNroAnulaciones = Nz(Rs(0)) + 1
                    End While
                Else
                    psNroAnulaciones = 1
                End If
                Rs.Close()

                CmdGlobal2.CommandText = " INSERT INTO TBINV_ANULACIONES (EMPRESA_CODIGO, ANUL_NRO, ANUL_FECHA, ANUL_TIPO, ANUL_CODIGO, ANUL_ESTADO, ANUL_SYS_EST, ANUL_SYS_CRE, " _
                                         & " ANUL_TIPO_ORIGEN, ANUL_COD_ORIGEN, ANUL_TIPO_DESTINO, ANUL_COD_DESTINO, ANUL_MOTIVO,ANUL_DESCRIPCION)" _
                                         & " VALUES ('" & psCodEmpresa & "','" & psNroAnulaciones & "','" & FechaActual() & "','1','" & pdCodSalida & "','" & Nu(RsSal!DESP_ESTADO) & "','0','" & ValorSys & "', " _
                                         & " '1','" & Nu(RsSal!ALMACEN_ORIGEN) & "','" & Nu(RsSal!DESP_TIPODESTINO) & "','" & psCodigoDestino & "','" & psMotivo & "','" & psMotivoAnulacion & "')"
                CmdGlobal2.ExecuteNonQuery()

            End While
        End If
        RsSal.Close()

        AnulaxMotivos(psConexion, psCodEmpresa, pdCodSalida)
    End Sub


    Public Sub AnulaxMotivos(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psCodSalida As Double)
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        ''33 Alquiler

        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal3 As New SqlCommand

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3

        CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_SYS_EST = '0' AND DESP_CODIGO =" & psCodSalida
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                If Nu(Rs!DESP_MOTIVO_GRAL) = "33" Then
                    CmdGlobal2.CommandText = " UPDATE dbo.TBINV_ALQUILER SET ALQUILER_ESTADO = '6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DESP_CODIGO =" & psCodSalida & ")"
                    CmdGlobal2.ExecuteNonQuery()
                    CmdGlobal2.CommandText = "SELECT MAX(ALQUILER_CODIGO) From dbo.TBINV_ALQUILER WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DESP_CODIGO =" & psCodSalida & ")"
                    Rs2 = CmdGlobal.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CmdGlobal3.CommandText = " UPDATE dbo.TBINV_ALQUILER_DETALLE SET ALQUIDET_ESTADO_ALQUILER = '6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (ALQUI_CODIGO = " & Nu(Rs2(0)) & ")"
                            CmdGlobal3.ExecuteNonQuery()
                            CmdGlobal3.CommandText = " UPDATE dbo.TBINV_ALQUILER_DETALLE_SINSERIE SET ALQUIDET_ESTADO_ALQUILER = '6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (ALQUILER_CODIGO = " & Nu(Rs2(0)) & ")"
                            CmdGlobal3.ExecuteNonQuery()
                        End While
                    End If
                    Rs2.Close()
                    '1 Prestamo
                ElseIf Nu(Rs!DESP_MOTIVO_GRAL) = "1" Then
                    CmdGlobal2.CommandText = "SELECT PRESTA_CODIGO From dbo.TBINV_PRESTAMO WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DESP_CODIGO =" & psCodSalida & ")"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CmdGlobal3.CommandText = " UPDATE dbo.TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO='6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (PRESTA_CODIGO = " & Rs2!PRESTA_CODIGO & ")"
                            CmdGlobal3.ExecuteNonQuery()
                            CmdGlobal3.CommandText = " UPDATE dbo.TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = '6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (PRESTA_CODIGO = " & Rs2!PRESTA_CODIGO & ")"
                            CmdGlobal3.ExecuteNonQuery()
                        End While
                    End If
                    Rs2.Close()
                Else
                    CmdGlobal2.CommandText = " UPDATE dbo.TBINV_SALIDA_MOTIVO SET ALLSAL_ESTADO = '6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DESP_CODIGO =" & psCodSalida & ")"
                    CmdGlobal2.ExecuteNonQuery()
                    CmdGlobal2.CommandText = "SELECT ALLSAL_CODIGO From dbo.TBINV_SALIDA_MOTIVO WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DESP_CODIGO =" & psCodSalida & ")"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CmdGlobal3.CommandText = " UPDATE dbo.TBINV_SALIDA_MOTIVO_DET SET ALLSALD_ESTADO_ENVIO='6',ALLSALD_ESTADO='6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (ALLSAL_CODIGO = " & Rs2!ALLSAL_CODIGO & ")"
                            CmdGlobal3.ExecuteNonQuery()
                            CmdGlobal3.CommandText = " UPDATE dbo.TBINV_SALIDA_MOTIVO_DET_SINSERIE SET  ALLSALD_ESTADO_ENVIO='6',ALLSALD_ESTADO='6' WHERE (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (ALLSAL_CODIGO = " & Rs2!ALLSAL_CODIGO & ")"
                            CmdGlobal3.ExecuteNonQuery()
                        End While
                    End If
                    Rs2.Close()
                End If
            End While
        End If
        Rs.Close()
    End Sub
    Public Sub Actualizar_Estado_Orden_Venta(ByVal psCodOV As String, ByVal psConexion As String)

        Dim Rs As SqlDataReader
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal2 As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Dim psCantSalida As Long : psCantSalida = 0
        Dim psCantFactura As Long : psCantFactura = 0
        Dim psEstado As String : psEstado = ""
        Dim psConRelac As String : psConRelac = ""

        If psCodOV <> "" Then
            CmdGlobal.CommandText = " SELECT DESP_CODIGO " _
                & " FROM TBVENTAS_ORDENVENTA_SALIDA " _
                & " WHERE OVDESP_ESTADO = '1' AND OVENTA_CODIGO = " & psCodOV
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCantSalida = psCantSalida + 1
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = " SELECT OVFACT_SERIE, OVFACT_NUMERO " _
                & " FROM TBVENTAS_ORDENVENTA_FACTURA " _
                & " WHERE OVFACT_ESTADO = '1' AND OVENTA_CODIGO = " & psCodOV
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCantFactura = psCantFactura + 1
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = " SELECT OVENTA_ESTADO_RELAC " _
                & " FROM TBVENTAS_ORDENVENTA " _
                & " WHERE OVENTA_CODIGO = " & psCodOV
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psConRelac = Nu(Rs!OVENTA_ESTADO_RELAC)
                End While
            End If
            Rs.Close()
            If psCantSalida = 0 And psCantFactura = 0 And psConRelac = "N" Then psEstado = 2 'autorizada
            If psCantSalida = 0 And psCantFactura = 0 And psConRelac = "S" Then psEstado = 5 'relacionada
            If psCantSalida > 0 And psCantFactura = 0 Then psEstado = 7 'con salida
            If psCantSalida = 0 And psCantFactura > 0 Then psEstado = 3 'con factura
            If psCantSalida > 0 And psCantFactura > 0 Then psEstado = 8 'con salida y factura
            CmdGlobal.CommandText = " UPDATE TBVENTAS_ORDENVENTA SET OVENTA_ESTADO = '" & psEstado & "' " _
                                  & " WHERE OVENTA_CODIGO = " & psCodOV
            CmdGlobal.ExecuteNonQuery()

        End If
    End Sub

    Public Sub Anular_Salida(ByVal pdCodSalida As Double, ByVal psMotivoAnulacion As String, ByVal psConexion As String,
                              ByVal psCodEmpresa As String, ByVal psUser As String, ByVal psEstado As String)
        Dim Rs As SqlDataReader
        Dim Rs1 As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim Rs3 As SqlDataReader
        Dim StockAc As Double
        Dim ValorSys As String
        ValorSys = psUser & FechaActual() & HoraActual()


        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal3 As New SqlCommand
        Dim Cn4 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal4 As New SqlCommand
        Dim Cn5 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal5 As New SqlCommand
        Dim fn As New clsInv_Procesos
        Dim fnCont As New clsCont_Funciones
        Dim psMensaje As String = ""

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        Cn4.Open() : CmdGlobal4.Connection = Cn4
        Cn5.Open() : CmdGlobal4.Connection = Cn5
        Dim lblNroAnulaciones As String = ""
        If psEstado = "1" Or psEstado = "5" Then

            CmdGlobal.CommandText = "SELECT * FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_SYS_EST = '0' AND DESP_CODIGO =" & pdCodSalida
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If Nu(Rs!DESP_ESTADO) = "1" Or Nu(Rs!DESP_ESTADO) = "5" Then
                        CmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_ESTADO ='6',DESP_MOTIVO_ANULACION='" & psMotivoAnulacion & "' WHERE EMPRESA_CODIGO ='" & psCodEmpresa & "' AND DESP_SYS_EST ='0' AND DESP_CODIGO=" & pdCodSalida
                        CmdGlobal2.ExecuteNonQuery()
                        'LIBERAR LOS ARTICULOS_SERIES
                        CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.SERIE_NUMERAR, S.SERIE_PARATRANSITO" _
                            & " FROM TBINV_ALMACEN_DESPACHO_DET DES INNER JOIN " _
                            & " dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON DES.SERIE_NUMERAR = S.SERIE_NUMERAR" _
                            & " WHERE (DES.DESP_CODIGO = '" & pdCodSalida & "') AND (DES.DESPD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND DES.EMPRESA_CODIGO = '" & psCodEmpresa & "'"
                        Rs1 = CmdGlobal2.ExecuteReader
                        If Rs1.HasRows Then
                            While Rs1.Read
                                CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR =" & Nu(Rs1!Serie_Numerar)
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        End If
                        Rs1.Close()
                        'LIBERAR LAS CANTIDADES DE ARTICULOS
                        CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.ARTICULO_CODIGO, DES.DESPD_CANTXDESP " _
                            & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE DES WHERE DES.EMPRESA_CODIGO = '" & psCodEmpresa & "' AND (DES.DESP_CODIGO = '" & pdCodSalida & "') AND (DES.DESPD_SYS_EST = '0')"
                        Rs1 = CmdGlobal2.ExecuteReader
                        If Rs1.HasRows Then
                            While Rs1.Read
                                CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_PARATRANSITO = ISNULL(SAA_PARATRANSITO,0) - " & Nz(Rs1!DESPD_CANTXDESP) & " WHERE UBICACT_TIPO=1 AND ALMACEN_CODIGO='" & Nu(Rs!ALMACEN_ORIGEN) & "' AND EMPRESA_CODIGO = '" & psCodEmpresa & "' AND ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO)
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        End If
                        Rs1.Close()
                    End If
                    ' se graba en la tabla de anulaciones
                    CmdGlobal2.CommandText = "SELECT MAX(ANUL_NRO) FROM TBINV_ANULACIONES "
                    Rs1 = CmdGlobal2.ExecuteReader
                    If Rs1.HasRows Then
                        While Rs1.Read
                            lblNroAnulaciones = Nz(Rs1(0)) + 1
                        End While
                    Else
                        lblNroAnulaciones = 1
                    End If
                    Rs1.Close()
                    CmdGlobal2.CommandText = " INSERT INTO TBINV_ANULACIONES (EMPRESA_CODIGO, ANUL_NRO, ANUL_FECHA, ANUL_TIPO, ANUL_CODIGO, ANUL_ESTADO, ANUL_SYS_EST, ANUL_SYS_CRE, " _
                                          & " ANUL_TIPO_ORIGEN, ANUL_COD_ORIGEN, ANUL_TIPO_DESTINO, ANUL_COD_DESTINO, ANUL_MOTIVO,ANUL_DESCRIPCION)" _
                                          & " VALUES ('" & psCodEmpresa & "','" & lblNroAnulaciones & "','" & FechaActual() & "','1','" & pdCodSalida & "','" & psEstado & "','0','" & ValorSys & "', " _
                                          & " '1','" & Nu(Rs!ALMACEN_ORIGEN) & "','" & Nu(Rs!DESP_TIPODESTINO) & "','" & IIf(Nu(Rs!DESP_TIPODESTINO) = "1", Nu(Rs!ALMACEN_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "2", Nu(Rs!CECOSE_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "3", Nu(Rs!PROVEEDOR_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "4", Nu(Rs!EQUIPO_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "5", Nu(Rs!PERSONA_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "6", Nu(Rs!CLIENTE_CODIGO_DESTINO), "")))))) & "','" & Nu(Rs!DESP_MOTIVO_GRAL) & "','" & psMotivoAnulacion & "')"
                    CmdGlobal2.ExecuteNonQuery()
                    'cmdListar_Click
                End While
            End If

        ElseIf psEstado = "2" Then
            Dim psTipoCambio As String = ""
            Dim lblNroMovimiento As String = ""
            psTipoCambio = fnCont.Hallar_Valor_Compra(psConexion, FechaActual)
            CmdGlobal.CommandText = "SELECT ALMACEN_ORIGEN,DESP_FECHA_SAL FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_SYS_EST = '0' AND DESP_CODIGO ='" & pdCodSalida & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    Actualizar_CostoArticulo(psConexion, psCodEmpresa, pdCodSalida, "3", "20", "1", Nu(Rs!ALMACEN_ORIGEN), psTipoCambio)
                    'paso1
                    'se cambia de estado a la salida de 2 a 6:anulada
                    CmdGlobal2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_ESTADO ='6',DESP_MOTIVO_ANULACION='" & psMotivoAnulacion & "' WHERE EMPRESA_CODIGO ='" & psCodEmpresa & "' AND DESP_SYS_EST ='0' AND DESP_CODIGO='" & pdCodSalida & "'"
                    CmdGlobal2.ExecuteNonQuery()
                    'Equipo
                    CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.SERIE_NUMERAR, S.SERIE_PARATRANSITO, S.ARTICULO_CODIGO, DES.DESPD_COSTO_VENTA_S, DES.DESPD_COSTO_VENTA_D " _
                        & " FROM TBINV_ALMACEN_DESPACHO_DET DES INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S " _
                        & " ON DES.SERIE_NUMERAR = S.SERIE_NUMERAR" _
                        & " WHERE (DES.DESP_CODIGO = '" & pdCodSalida & "') AND (DES.DESPD_SYS_EST = '0') AND " _
                        & " (S.SERIE_SYS_EST = '0') AND (DES.EMPRESA_CODIGO = '" & psCodEmpresa & "')"
                    Rs1 = CmdGlobal2.ExecuteReader
                    If Rs1.HasRows Then
                        While Rs1.Read
                            'paso2
                            'el articulo_serie ya no esta en transito vuelve a estar en la ubicación de donde salió
                            CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = NULL,SERIE_FUNCION = NULL,UBICACT_TIPO='1', UBICACT_CODIGO=" & Nu(Rs!ALMACEN_ORIGEN) & " WHERE SERIE_NUMERAR =" & Nu(Rs1!Serie_Numerar)
                            CmdGlobal3.ExecuteNonQuery()
                            'paso3
                            'aqui aumenta el stock de la ubicacion de donde salio el articulo_serie
                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & Nu(Rs!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                & " AND (ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs2 = CmdGlobal3.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    StockAc = Nz(Rs2!SAA_STOCK_ACTUAL) + 1
                                    CmdGlobal4.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(Rs!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                                            & " AND (ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal4.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                            & "VALUES(" & Nu(Rs!ALMACEN_ORIGEN) & ",'1'," & Nu(Rs1!ARTICULO_CODIGO) & ",1,'0','" & psCodEmpresa & "')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs2.Close()
                            'paso4
                            'se hace un movimiento contrario: motivo 22:por anulación
                            'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & Nz(Rs1!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & lblNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' AND (CODIGO_TRANS = '" & pdCodSalida & "')"
                            Rs3 = CmdGlobal3.ExecuteReader
                            If Rs3.HasRows Then
                                While Rs3.Read
                                    Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(Rs1!ARTICULO_CODIGO), "1", Nu(Rs!ALMACEN_ORIGEN), "1", Nu(Rs!ALMACEN_ORIGEN), "", "1", FormatoFecha(FechaActual), 1, "S", CDbl(Nz(Rs1!DESPD_COSTO_VENTA_S)), CDbl(Nz(Rs1!DESPD_COSTO_VENTA_D)))
                                    CmdGlobal4.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET NRO_ARTICULO =" & Nz(Rs3!NRO_ARTICULO) + 1 & " WHERE (CODIGO_ARTICULO = " & Nz(Rs1!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & lblNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' AND (CODIGO_TRANS = '" & pdCodSalida & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal4.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                Rs2 = CmdGlobal4.ExecuteReader
                                If Rs2.HasRows Then
                                    While Rs.Read
                                        lblNroMovimiento = Nz(Rs2(0)) + 1
                                    End While
                                Else
                                    lblNroMovimiento = "00000001"
                                End If
                                Rs2.Close()
                                Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(Rs1!ARTICULO_CODIGO), "1", Nu(Rs!ALMACEN_ORIGEN), "1", Nu(Rs!ALMACEN_ORIGEN), "", "1", FormatoFecha(FechaActual), 1, "S", CDbl(Nz(Rs1!DESPD_COSTO_VENTA_S)), CDbl(Nz(Rs1!DESPD_COSTO_VENTA_D)))

                                CmdGlobal4.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO," _
                                                        & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                        & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','1'," & Nu(Rs!ALMACEN_ORIGEN) & ",'1'," & Nu(Rs!ALMACEN_ORIGEN) & ", " _
                                                        & " '" & pdCodSalida & "','" & Nu(Rs1!ARTICULO_CODIGO) & "','1','" & ValorSys & "','6','22','" & FechaActual() & "','0')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs3.Close()

                        End While
                    End If
                    Rs1.Close()
                    'accesorios
                    CmdGlobal2.CommandText = " SELECT DES.DESP_CODIGO, DES.ARTICULO_CODIGO, DES.DESPD_CANTXDESP,DESPD_COSTO_VENTA_S ,DESPD_COSTO_VENTA_D " _
                        & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE DES WHERE DES.EMPRESA_CODIGO = '" & psCodEmpresa & "' AND (DES.DESP_CODIGO = '" & pdCodSalida & "') AND (DES.DESPD_SYS_EST = '0')"
                    Rs1 = CmdGlobal2.ExecuteReader
                    If Rs1.HasRows Then
                        While Rs1.Read
                            'DISMINUIR STOCK EN EL DESTINO

                            'AUMENTAR STOCK EN EL ORIGEN
                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & Nu(Rs!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                & " AND (ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs2 = CmdGlobal3.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    StockAc = Nz(Rs2!SAA_STOCK_ACTUAL) + CDbl(Nu(Rs1!DESPD_CANTXDESP))
                                    CmdGlobal4.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & Nu(Rs!ALMACEN_ORIGEN) & ") AND (UBICACT_TIPO='1')" _
                                                        & " AND (ARTICULO_CODIGO = " & Nu(Rs1!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            End If
                            Rs2.Close()
                            'paso2
                            'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL
                            CmdGlobal3.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            Rs2 = CmdGlobal3.ExecuteReader
                            If Rs2.HasRows Then
                                While Rs2.Read
                                    lblNroMovimiento = Nz(Rs2(0)) + 1
                                End While
                            Else
                                lblNroMovimiento = "00000001"
                            End If
                            Rs2.Close()
                            Movimiento_Kardex(psConexion, psCodEmpresa, pdCodSalida, "22", Nu(Rs1!ARTICULO_CODIGO), "1", Nu(Rs!ALMACEN_ORIGEN), "1", Nu(Rs!ALMACEN_ORIGEN), "", "1", FormatoFecha(FechaActual), Nz(Rs1!DESPD_CANTXDESP), "S", CDbl(Nz(Rs1!DESPD_COSTO_VENTA_S)), CDbl(Nz(Rs1!DESPD_COSTO_VENTA_D)))

                            CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT,CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO," _
                                                    & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                    & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','1'," & Nu(Rs!ALMACEN_ORIGEN) & ",'1'," & Nu(Rs!ALMACEN_ORIGEN) & ", " _
                                                    & " '" & pdCodSalida & "','" & Nu(Rs1!ARTICULO_CODIGO) & "'," & Nz(Rs1!DESPD_CANTXDESP) & ",'" & ValorSys & "','6','22','" & FechaActual() & "','0')"
                            CmdGlobal3.ExecuteNonQuery()

                        End While
                    End If
                    Rs1.Close()
                End While
            End If
            Rs.Close()
            'todo dependiendo del nro de salida
            'si la salida es reemplazo por cambio o por averia a un ccosto
            CmdGlobal.CommandText = " UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1 = '5', REEM_ESTADO_2='3' WHERE REEM_SYS_EST='0' AND NRO_SALIDA_ALM = '" & pdCodSalida & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
            CmdGlobal.ExecuteNonQuery()
            'si la salida es por reparacion a un almacen
            CmdGlobal.CommandText = " UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1 = '2', AVERIA_ESTADO_2='1' WHERE AVERIA_SYS_EST='0' AND SALIDA_NRO_ALM = '" & pdCodSalida & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
            CmdGlobal.ExecuteNonQuery()
            ' si la salida por prestamo
            CmdGlobal.CommandText = " SELECT P.PRESTA_CODIGO, P.DESP_CODIGO, PDET.PREDET_ESTADO_PRESTAMO, P.EMPRESA_CODIGO " _
                & " FROM dbo.TBINV_PRESTAMO P INNER JOIN dbo.TBINV_PRESTAMO_DETALLE PDET ON " _
                & " P.PRESTA_CODIGO = PDET.PRESTA_CODIGO AND P.EMPRESA_CODIGO = PDET.EMPRESA_CODIGO " _
                & " WHERE (P.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (P.DESP_CODIGO = '" & pdCodSalida & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '4' WHERE PRESTA_CODIGO = '" & Nu(Rs!PRESTA_CODIGO) & "' AND EMPRESA_CODIGO = '" & psCodEmpresa & "'"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            CmdGlobal.CommandText = " SELECT P.PRESTA_CODIGO, P.DESP_CODIGO, PDET.PREDET_ESTADO_PRESTAMO, P.EMPRESA_CODIGO " _
                & " FROM dbo.TBINV_PRESTAMO P INNER JOIN dbo.TBINV_PRESTAMO_DETALLE_SINSERIE PDET ON " _
                & " P.PRESTA_CODIGO = PDET.PRESTA_CODIGO AND P.EMPRESA_CODIGO = PDET.EMPRESA_CODIGO " _
                & " WHERE (P.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (P.DESP_CODIGO = " & pdCodSalida & ")"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = '4' WHERE PRESTA_CODIGO = '" & Nu(Rs!PRESTA_CODIGO) & "' AND EMPRESA_CODIGO = '" & psCodEmpresa & "'"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            'se anulada la guia cambianto de estado para esto agregue un campo mas en la tabla guirem_estado de 0:generado a 1:anulado
            CmdGlobal.CommandText = " SELECT G.GUIREM_CODIGO, GD.DESP_CODIGO FROM dbo.TBINV_GUIA_REMISION_" & psCodEmpresa & " G INNER JOIN " _
                & " dbo.TBINV_GUIA_REMISION_DETALLE_" & psCodEmpresa & " GD ON G.GUIREM_CODIGO = GD.GUIREM_CODIGO " _
                & " WHERE (G.GUIREM_SYS_EST = '0') AND (GD.DESP_CODIGO = " & pdCodSalida & ") AND (G.GUIREM_ESTADO='0')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " UPDATE TBINV_GUIA_REMISION_" & psCodEmpresa & " SET GUIREM_ESTADO='1',GUIREM_SYS_ANULADA = '" & ValorSys & "' " _
                                        & " WHERE (GUIREM_CODIGO = '" & Nu(Rs!GUIREM_CODIGO) & "') AND (GUIREM_ESTADO='0') AND (GUIREM_SYS_EST='0')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            'se anula la salida a proveedor
            CmdGlobal.CommandText = " SELECT * FROM dbo.TBINV_EQUIPOS_MANTENIMIENTOS " _
                & " WHERE (MANTEN_SYS_EST = '0') AND (SALIDA_NRO = '" & pdCodSalida & "') AND (MANTEN_ESTADO='2')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " UPDATE TBINV_EQUIPOS_MANTENIMIENTOS SET MANTEN_SYS_EST ='1'" _
                                        & " WHERE (MANTEN_NRO = " & Nu(Rs!MANTEN_NRO) & ") AND (MANTEN_ESTADO = '2') AND (MANTEN_SYS_EST = '0') AND (SALIDA_NRO = '" & pdCodSalida & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            ' se grabara en la tabla de anulaciones
            CmdGlobal.CommandText = "SELECT MAX(ANUL_NRO) FROM TBINV_ANULACIONES "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroAnulaciones = Nz(Rs(0)) + 1
                End While
            Else
                lblNroAnulaciones = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = "SELECT ALMACEN_ORIGEN,DESP_TIPODESTINO,ALMACEN_CODIGO_DESTINO,PERSONA_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO,PROVEEDOR_CODIGO_DESTINO,EQUIPO_CODIGO_DESTINO,CLIENTE_CODIGO_DESTINO,DESP_MOTIVO_GRAL FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_SYS_EST = '0' AND DESP_CODIGO ='" & pdCodSalida & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " INSERT INTO TBINV_ANULACIONES (EMPRESA_CODIGO, ANUL_NRO, ANUL_FECHA, ANUL_TIPO, ANUL_CODIGO, ANUL_ESTADO, ANUL_SYS_EST, ANUL_SYS_CRE, " _
                                        & " ANUL_TIPO_ORIGEN, ANUL_COD_ORIGEN, ANUL_TIPO_DESTINO, ANUL_COD_DESTINO, ANUL_MOTIVO,ANUL_DESCRIPCION)" _
                                        & " VALUES ('" & psCodEmpresa & "','" & lblNroAnulaciones & "','" & FechaActual() & "','1','" & pdCodSalida & "','" & psEstado & "','0','" & ValorSys & "', " _
                                        & " '1','" & Nu(Rs!ALMACEN_ORIGEN) & "','" & Nu(Rs!DESP_TIPODESTINO) & "','" & IIf(Nu(Rs!DESP_TIPODESTINO) = "1", Nu(Rs!ALMACEN_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "2", Nu(Rs!CECOSE_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "3", Nu(Rs!PROVEEDOR_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "4", Nu(Rs!EQUIPO_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "5", Nu(Rs!PERSONA_CODIGO_DESTINO), IIf(Nu(Rs!DESP_TIPODESTINO) = "6", Nu(Rs!CLIENTE_CODIGO_DESTINO), "")))))) & "','" & Nu(Rs!DESP_MOTIVO_GRAL) & "','" & psMotivoAnulacion & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
        End If

    End Sub



    Public Sub Guardar_RelacionTicket(ByVal ps_Conexion As String, ByVal psNroTicket As String, psAccion As String, ByVal psCosReferencia As Double, ByVal psUser As String)
        Dim psConexion As String = ps_Conexion
        Dim CnFun As New SqlConnection(psConexion)
        Dim CmdGlobalFun As New SqlCommand
        Dim CmdGlobalFun2 As New SqlCommand
        Dim Rs As SqlDataReader
        Try

            If psNroTicket <> "" Then
                Dim pd_Secuencia_Accion As String = ""
                CnFun.Open() : CmdGlobalFun.Connection = CnFun : CmdGlobalFun2.Connection = CnFun
                CmdGlobalFun.CommandText = "SELECT MAX(ACCION_SECUENCIA) FROM TBTICKET_TRAKING_ACCION WHERE TICKET_CODIGO=" & psNroTicket
                Rs = CmdGlobalFun.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        pd_Secuencia_Accion = Format(Nz(Rs(0)) + 1, "000")
                    End While
                Else
                    pd_Secuencia_Accion = "1"
                End If
                Rs.Close()
                CmdGlobalFun.CommandText = " INSERT INTO TBTICKET_TRAKING_ACCION ( TICKET_CODIGO, ACCION_SECUENCIA, ACCION_CODIGO, ACCION_FECHA, ACCION_HORA, ACCION_USER, ACCION_REFERENCIA) " _
                                      & " VALUES (" & Nz(psNroTicket) & ", " & Nz(pd_Secuencia_Accion) & ", '" & psAccion & "', '" & FechaActual() & "', '" & HoraActual(True) & "', '" & psUser & "', " & Nz(psCosReferencia) & ")"
                CmdGlobalFun.ExecuteNonQuery()
            End If

        Catch ex As SQLException

        Catch ex As Exception
        End Try
    End Sub


    Public Sub GenerarSalidaProduccion(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psCodOCompra As String,
                                       ByVal pdCantxDesp As Double, ByVal psCodAlmacen As String, ByVal psUSer As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CodSalida As Long = 0
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim psestado As String : psestado = ""
        Dim ContarAcc As Long = 0
        Dim Obs As String = ""
        Dim A As Long, S As Long, z As Long = 0
        Dim item As Long = 0
        A = 0

        S = 0 : ContarAcc = 0 : item = 0
        Cn.Open() : Cn2.Open() : Cn3.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2 : CmdGlobal3.Connection = Cn3
        CmdGlobal.CommandText = " SELECT DISTINCT OC.OCOMPRA_CODIGO , OC.OCOMPRA_NUMERAR, RD.DESTINO_TIPO, RD.DESTINO_CODIGO, A.ART_TIPO, A.ART_CODIGO, OC.OCOMPRA_ORDENTRABAJO, R.RECEP_CANT_XREC, R.RECEP_CANT_REC, R.RECEP_CANT_FALT_REC " _
              & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
              & " dbo.TBLOGIS_ORDENES_COMPRA OC ON R.RECEP_COTIZOCOMPRA = OC.OCOMPRA_CODIGO AND  R.EMPRESA_CODIGO = OC.EMPRESA_CODIGO INNER JOIN " _
              & " dbo.TBLOGIS_ORDENES_COMPRA_DETALLE OCD ON OC.OCOMPRA_NUMERAR = OCD.OCOMPRA_NUMERAR AND OC.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO INNER JOIN " _
              & " dbo.TBLOGIS_REQUISICION_DETALLE RD ON OCD.OCOMPRAD_REQUISD_NUMERAR = RD.REQUISD_NUMERAR AND  OCD.EMPRESA_CODIGO = RD.EMPRESA_CODIGO INNER JOIN " _
              & " dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON RD.DESTINO_CODIGO = S.SERIE_NUMERAR INNER JOIN " _
              & " dbo.TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO " _
              & " WHERE (RD.DESTINO_TIPO = '4') AND (OC.OCOMPRA_CODIGO = '" & psCodOCompra & "') AND (A.ART_TIPO = 89) AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND " _
              & " (RD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (OCD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (OC.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (R.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
              & " AND (R.RECEP_SYS_EST = '0') AND (OC.OCOMPRA_SYS_EST = '0') AND (RD.REQUISD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (A.ART_SYS_EST = '0') "
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                CmdGlobal2.CommandText = " SELECT DISTINCT OC.OCOMPRA_CODIGO, OC.OCOMPRA_NUMERAR, RD.DESTINO_TIPO, RD.DESTINO_CODIGO, A.ART_TIPO, OC.OCOMPRA_ORDENTRABAJO, OCD.OCOMPRAD_ARTICULO , OCD.OCOMPRAD_CANTIDAD," _
                    & " (SELECT ART_TIPO FROM TBINV_ARTICULOS B WHERE B.ART_CODIGO = OCOMPRAD_ARTICULO AND B.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO AND B.ART_SYS_EST = '0') AS TIPO_ART " _
                    & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
                    & " dbo.TBLOGIS_ORDENES_COMPRA OC ON R.RECEP_COTIZOCOMPRA = OC.OCOMPRA_CODIGO AND R.EMPRESA_CODIGO = OC.EMPRESA_CODIGO INNER JOIN " _
                    & " dbo.TBLOGIS_ORDENES_COMPRA_DETALLE OCD ON OC.OCOMPRA_NUMERAR = OCD.OCOMPRA_NUMERAR AND OC.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO INNER JOIN " _
                    & " dbo.TBLOGIS_REQUISICION_DETALLE RD ON OCD.OCOMPRAD_REQUISD_NUMERAR = RD.REQUISD_NUMERAR AND OCD.EMPRESA_CODIGO = RD.EMPRESA_CODIGO INNER JOIN " _
                    & " dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON RD.DESTINO_CODIGO = S.SERIE_NUMERAR INNER JOIN " _
                    & " dbo.TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO " _
                    & " WHERE (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (RD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (OCD.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                    & " AND (OC.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (R.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (R.RECEP_SYS_EST = '0') AND (OC.OCOMPRA_SYS_EST = '0') " _
                    & " AND (RD.REQUISD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (A.ART_SYS_EST = '0') AND (OC.OCOMPRA_CODIGO = " & psCodOCompra & ") AND (RD.DESTINO_TIPO = '4') AND (A.ART_TIPO = 89)"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        item = item + 1
                        If Nu(Rs2!TIPO_ART) = 87 Then ContarAcc = ContarAcc + 1
                    End While
                End If
                Rs2.Close()
                If Nz(ContarAcc) = Nz(item) Then psestado = "1" Else psestado = "5"
                CmdGlobal2.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        CodSalida = Nz(Rs2(0)) + 1
                    End While
                Else
                    CodSalida = "000001"
                End If
                Rs2.Close()
                Obs = "Salida para la Orden de Trabajo Nº OT" & Llenar_Ceros(psCodOCompra, 6)
                CmdGlobal2.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                      & " EQUIPO_CODIGO_DESTINO,DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                      & " DESP_MOTIVO_GRAL,DESP_OBSERVACION,DESP_ORDENTRABAJO) " _
                                      & " VALUES('" & psCodEmpresa & "'," & CodSalida & ",'" & FechaActual() & "','" & HoraActual() & "','" & psUSer & "','4'," _
                                      & " '" & Nz(Rs!DESTINO_CODIGO) & "','" & psestado & "','0'," & pdCantxDesp & ",0,0,0,'" & psCodAlmacen & "','31', '" & Obs & "'," & Nu(Rs!OCOMPRA_ORDENTRABAJO) & ")"
                CmdGlobal2.ExecuteNonQuery()
            End While
        End If
        Rs.Close()
        ContarAcc = 0 : item = 0
        z = 0 : S = 0
        CmdGlobal.CommandText = " SELECT DISTINCT OC.OCOMPRA_CODIGO, OC.OCOMPRA_NUMERAR, RD.DESTINO_TIPO, RD.DESTINO_CODIGO, A.ART_TIPO, OC.OCOMPRA_ORDENTRABAJO, OCD.OCOMPRAD_ARTICULO , OCD.OCOMPRAD_CANTIDAD," _
            & " (SELECT ART_TIPO FROM TBINV_ARTICULOS B WHERE B.ART_CODIGO = OCOMPRAD_ARTICULO AND B.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO AND B.ART_SYS_EST = '0') AS TIPO_ART " _
            & " FROM dbo.TBINV_ALMACEN_RECEPCION R INNER JOIN " _
            & " dbo.TBLOGIS_ORDENES_COMPRA OC ON R.RECEP_COTIZOCOMPRA = OC.OCOMPRA_CODIGO AND R.EMPRESA_CODIGO = OC.EMPRESA_CODIGO INNER JOIN " _
            & " dbo.TBLOGIS_ORDENES_COMPRA_DETALLE OCD ON OC.OCOMPRA_NUMERAR = OCD.OCOMPRA_NUMERAR AND OC.EMPRESA_CODIGO = OCD.EMPRESA_CODIGO INNER JOIN " _
            & " dbo.TBLOGIS_REQUISICION_DETALLE RD ON OCD.OCOMPRAD_REQUISD_NUMERAR = RD.REQUISD_NUMERAR AND OCD.EMPRESA_CODIGO = RD.EMPRESA_CODIGO INNER JOIN " _
            & " dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON RD.DESTINO_CODIGO = S.SERIE_NUMERAR INNER JOIN " _
            & " dbo.TBINV_ARTICULOS A ON S.ARTICULO_CODIGO = A.ART_CODIGO " _
            & " WHERE (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (RD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (OCD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (OC.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (R.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (R.RECEP_SYS_EST = '0') AND (OC.OCOMPRA_SYS_EST = '0') AND (RD.REQUISD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (A.ART_SYS_EST = '0') AND (OC.OCOMPRA_CODIGO = " & psCodOCompra & ") AND (RD.DESTINO_TIPO = '4') AND (A.ART_TIPO = 89)"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                If Nu(Rs!TIPO_ART) = 87 Then
                    z = z + 1
                    CmdGlobal2.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET_SINSERIE( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM,ARTICULO_CODIGO,DESPD_CANTXDESP,DESPD_CANT_DESP,DESPD_CANT_REC,DESPD_CANT_FALT_REC,DESPD_SYS_EST,DESPD_MOTIVO) " _
                                          & " VALUES('" & psCodEmpresa & "'," & CodSalida & "," & z & "," & Nz(Rs!OCOMPRAD_ARTICULO) & ",'" & Nz(Rs!OCOMPRAD_CANTIDAD) & "',0,0,0,'0','31')"
                    CmdGlobal2.ExecuteNonQuery()
                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_PARATRANSITO = ISNULL(SAA_PARATRANSITO,0) + " & Nz(Rs!OCOMPRAD_CANTIDAD) & " WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND UBICACT_TIPO='1' AND ALMACEN_CODIGO='" & psCodAlmacen & "' AND ARTICULO_CODIGO = " & Nz(Rs!OCOMPRAD_ARTICULO) & ""
                    CmdGlobal2.ExecuteNonQuery()
                Else
                    A = 1
                    For A = 1 To Nz(Rs!OCOMPRAD_CANTIDAD)
                        S = S + 1
                        CmdGlobal2.CommandText = "INSERT TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO) " _
                                              & " VALUES('" & psCodEmpresa & "'," & CodSalida & "," & S & ",NULL,'N','0'," & Nz(Rs!OCOMPRAD_ARTICULO) & ",'31')"
                        CmdGlobal2.ExecuteNonQuery()
                    Next
                End If
            End While
        End If
        Rs.Close()
    End Sub



    Function Guarda_Recepcion(ByVal psConexion As String, ByVal psCodEmpresa As String,
                              ByVal psProveedor As String, ByVal psPropietario As String,
                              ByVal psTipoDoc As String, ByVal psProyecto As String,
                              ByVal psCodAlmacen As String, ByVal psMotivo As String,
                              ByVal psFechaRecep As String, ByVal psOC As String,
                              ByVal pdNroItem As Double, ByVal pdTotalArt As Double,
                              ByVal psObservacion As String, ByVal psUser As String,
                              ByVal psSerieDoc As String, ByVal psNroDoc As String,
                              ByVal dtItem As DataTable, ByRef psTipoOrigen As String,
                              ByRef psTipoDestino As String, ByRef psTipoRS As String, Optional pstipoTabla As String = "") As String
        Dim Cn As New SqlConnection(psConexion)
        Guarda_Recepcion = ""
        Dim CmdGlobal As New SqlCommand
        Dim RsRecep As SqlDataReader
        Dim psCodRecepcion As String = ""
        Dim ValorSys As String = ""
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT MAX(RECEP_CODIGO) FROM TBINV_ALMACEN_RECEPCION WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
        RsRecep = CmdGlobal.ExecuteReader
        If RsRecep.HasRows Then
            While RsRecep.Read
                psCodRecepcion = Nz(RsRecep(0)) + 1
            End While
        Else
            psCodRecepcion = 1
        End If
        RsRecep.Close()
        Guarda_Recepcion = Llenar_Ceros(psCodRecepcion, 6)
        If psTipoRS = "S" Or psTipoRS = "V" Then
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,   " _
                              & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, " _
                              & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO) " _
                              & " VALUES('" & psCodEmpresa & "'," & psCodRecepcion & "," & psCodAlmacen & ", " _
                              & " '" & psFechaRecep & "','" & FechaActual() & "','" & HoraActual() & "','" & psUser & "','" & psObservacion & "'," & pdNroItem & ",'2'," _
                              & " '0','" & ValorSys & "'," & pdTotalArt & "," & pdTotalArt & ",0,0,'N','" & psMotivo & "','" & psOC & "','1', '" & psTipoOrigen & "', '" & psTipoDestino & "')"
            CmdGlobal.ExecuteNonQuery()
        Else
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,   " _
                              & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG, RECEP_OBSERVACION, RECEP_NRO_ITEM, RECEP_ESTADO, " _
                              & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO, RECEP_TIPOORIGEN, RECEP_TIPODESTINO) " _
                              & " VALUES('" & psCodEmpresa & "'," & psCodRecepcion & "," & psCodAlmacen & ", " _
                              & " '" & psFechaRecep & "','" & FechaActual() & "','" & HoraActual() & "','" & psUser & "','" & psObservacion & "'," & pdNroItem & ",'1'," _
                              & " '0','" & ValorSys & "'," & pdTotalArt & ",0," & pdTotalArt & ",0,'N','" & psMotivo & "','" & psOC & "','1', '" & psTipoOrigen & "', '" & psTipoDestino & "')"
            CmdGlobal.ExecuteNonQuery()
        End If
        If psProveedor <> "" Then
            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_PROVEEDOR='" & psProveedor & "' WHERE RECEP_CODIGO = " & psCodRecepcion
            CmdGlobal.ExecuteNonQuery()
        End If
        If psTipoDoc <> "" Then
            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_TIPODOC='" & psTipoDoc & "', RECEP_DOC_SERIE='" & psSerieDoc & "', RECEP_DOC_NUMERACION='" & psNroDoc & "' WHERE RECEP_CODIGO = " & psCodRecepcion
            CmdGlobal.ExecuteNonQuery()
        End If

        If psPropietario <> "" Then
            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION SET ALTIBI_CODIGO='" & psPropietario & "' WHERE RECEP_CODIGO = " & psCodRecepcion
            CmdGlobal.ExecuteNonQuery()
        End If
        If psProyecto <> "" Then
            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_PROYECTO='" & psProyecto & "' WHERE RECEP_CODIGO = " & psCodRecepcion
            CmdGlobal.ExecuteNonQuery()
        End If
        'RECEPD_ESTADO: 1 generado, 2 ejecutado ok, 3 ejecutado no ok
        Dim i As Long = 0
        Dim sFechaG As Date
        Dim psTiempoGarantia As Double = 0
        Dim psUnidadGarantia As String
        Dim psGarantia As String = ""
        Dim psTipo As String = ""
        Dim SerieNum As String = ""
        Dim n As Double = 0
        Dim psDtAr_Codigo As Double = 0
        Dim psDtArt_Tipo As String = ""
        Dim pdDtArt_Cant_xRec As Double = 0
        Dim pdDtArt_Garantia As String = ""
        Dim psDt As String = ""
        Dim psDtTiempoGarantia As Double = 0
        Dim psDtUnidadGarantia As String = ""
        Dim pdDtORIG_TIPO As String = ""
        Dim pdDtCOD_sALIDA As String = ""
        For Each dr As DataRow In dtItem.Rows
            i = i + 1
            If pstipoTabla = "Si" Then
                psDtAr_Codigo = Nu(dr("COD_ARTICULO"))
                psDtArt_Tipo = Nu(dr("Art_Tipo"))
                pdDtArt_Cant_xRec = 1
            Else
                psDtAr_Codigo = Nu(dr("Art_Codigo"))
                psDtArt_Tipo = Nu(dr("Art_Tipo"))
                pdDtArt_Cant_xRec = Nz(dr("Art_Cant_xRec"))
                psDtUnidadGarantia = Nu(dr("UnidadGarantia"))
                psDtTiempoGarantia = CDbl(Nz(dr("TiempoGarantia")))
                pdDtArt_Garantia = Nu(dr("Art_Garantia"))
                pdDtORIG_TIPO = Nu(dr("ORIG_TIPO"))
                pdDtCOD_sALIDA = Nu(dr("COD_sALIDA"))
            End If

            CmdGlobal.CommandText = "INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                  & "RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR,RECEPD_CANT_ING, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) VALUES(" _
                                  & "'" & psCodEmpresa & "'," & psCodRecepcion & "," & i & "," & psDtAr_Codigo & "," & pdDtArt_Cant_xRec & ",0," _
                                  & pdDtArt_Cant_xRec & ",0,0,'1','0','" & psMotivo & "','" & IIf(psDtArt_Tipo = "73", "S", IIf(psDtArt_Tipo = "64", "S", IIf(psDtArt_Tipo = "88", "S", "N"))) & "')"
            CmdGlobal.ExecuteNonQuery()
            psTiempoGarantia = CDbl(Nz(dr("TiempoGarantia")))
            psUnidadGarantia = Nu(dr("UnidadGarantia"))
            If pdDtArt_Garantia = "" Then
                If psFechaRecep <> "" And psTiempoGarantia <> 0 And psUnidadGarantia <> "" Then
                    Select Case psUnidadGarantia
                        Case "2"
                            psTipo = "d"
                        Case "3"
                            psTipo = "w"
                        Case "4"
                            psTipo = "m"
                        Case "5"
                            psTipo = "yyyy"
                    End Select
                    sFechaG = DateAdd(psTipo, psTiempoGarantia, FormatoFecha(psFechaRecep))
                    psGarantia = Format(sFechaG, "yyyymmdd")
                End If
            Else
                psGarantia = pdDtArt_Garantia
            End If
            If psDtArt_Tipo <> "64" And psDtArt_Tipo <> "73" Then
                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_RECEPCION_DET SET RECEPD_FECHA_GARANTIA_ACC='" & psGarantia & "' WHERE RECEP_CODIGO = '" & psCodRecepcion & "' AND RECEPD_INGRESAR_SERIE='N' AND ARTICULO_CODIGO=" & psDtAr_Codigo
                CmdGlobal.ExecuteNonQuery()
            End If
            For n = 1 To CLng(pdDtArt_Cant_xRec)
                If psDtArt_Tipo = "73" Or psDtArt_Tipo = "64" Then
                    CmdGlobal.CommandText = "SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & psCodEmpresa & ""
                    RsRecep = CmdGlobal.ExecuteReader
                    If RsRecep.HasRows Then
                        While RsRecep.Read
                            SerieNum = Nz(RsRecep(0)) + 1
                        End While
                    Else
                        SerieNum = 1
                    End If
                    RsRecep.Close()
                    If psTipoRS = "R" Then
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_" & psCodEmpresa & "(SERIE_NUMERAR, RECEP_CODIGO, ARTICULO_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,SERIE_NUEVO,ALTIBI_CODIGO,FECHA_VCTO_GARANTIA,CRITI_CODIGO,CONFIDENCIALIDAD,DISPONIBILIDAD,SERIE_ESTADO,TIPO_GARANTIA) " _
                                          & "VALUES(" & SerieNum & "," & psCodRecepcion & "," & psDtAr_Codigo & ",'N','" & ValorSys & "','0','S','" & psPropietario & "','" & psGarantia & "','2','1','2','0','')"
                        CmdGlobal.ExecuteNonQuery()
                        If psProveedor <> "" Then
                            CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET PROVEEDOR=" & psProveedor & " WHERE RECEP_CODIGO = '" & psCodRecepcion & "' AND SERIE_NUMERAR='" & SerieNum & "' AND ARTICULO_CODIGO=" & psDtAr_Codigo
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    End If
                    'SE AUMENTO PARA MUEBLES
                ElseIf psDtArt_Tipo = "88" Then
                    If psTipoRS = "R" Then
                        CmdGlobal.CommandText = "SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & psCodEmpresa & ""
                        RsRecep = CmdGlobal.ExecuteReader
                        If RsRecep.HasRows Then
                            While RsRecep.Read
                                SerieNum = Nz(RsRecep(0)) + 1
                            End While
                        Else
                            SerieNum = 1
                        End If
                        RsRecep.Close()
                        CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_" & psCodEmpresa & "(SERIE_NUMERAR, RECEP_CODIGO, ARTICULO_CODIGO, SERIE_SOBRANTE, SERIE_SYS_CRE,SERIE_SYS_EST,PROVEEDOR,SERIE_NUEVO,ALTIBI_CODIGO,FECHA_VCTO_GARANTIA,CRITI_CODIGO,CONFIDENCIALIDAD,DISPONIBILIDAD,SERIE_ESTADO,TIPO_GARANTIA) " _
                                              & "VALUES(" & SerieNum & "," & psCodRecepcion & "," & psDtAr_Codigo & ",'N','" & ValorSys & "','0'," & psProveedor & ",'S','" & psPropietario & "','" & psGarantia & "','2','1','2','0','')"
                        CmdGlobal.ExecuteNonQuery()
                        If psProveedor <> "" Then
                            CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET PROVEEDOR=" & psProveedor & " WHERE RECEP_CODIGO = '" & psCodRecepcion & "' AND SERIE_NUMERAR='" & SerieNum & "' AND ARTICULO_CODIGO=" & psDtAr_Codigo
                            CmdGlobal.ExecuteNonQuery()
                        End If
                    End If
                End If
            Next
            If psTipoRS = "V" Then
                CmdGlobal.CommandText = " INSERT INTO TBINV_RECEPCION_DETALLE_SERIES (EMPRESA_CODIGO, RECEP_CODIGO, SERIE_NUMERAR, SERIE_ORIG_TIPO, SERIE_ORIG_CODIGO, salida_codigo) " _
                                              & " VALUES ('" & psCodEmpresa & "', " & psCodRecepcion & ", " & psDtAr_Codigo & ", '" & Nu(dr("ORIG_TIPO")) & "', " & Nu(dr("ORIG_CODIGO")) & "," & Nu(dr("COD_sALIDA")) & ")"
                CmdGlobal.ExecuteNonQuery()
            End If
        Next
        Return Guarda_Recepcion
    End Function
    Public Sub Actualizar_CostoArticulo(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                        ByVal psCodigoTrans As String, ByVal psTipoTrans As String,
                                        ByVal psCodMotivo As String, ByVal psTipoUbic As String,
                                        ByVal psCodigoUbic As String, ByVal pdTipoCambio As String,
                                        Optional ByVal psCodOrdenCompra As String = "",
                                        Optional ByVal psMonedaSalida As String = "")
        Dim Cn As New SqlConnection(psConexion)
        Dim Cn2 As New SqlConnection(psConexion)
        Dim Cn3 As New SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim RsCosto As SqlDataReader
        Dim RsTrans As SqlDataReader
        Dim RsOC As SqlDataReader
        Dim RsMotivo As SqlDataReader
        Dim CalcularCosto As Boolean
        Dim TipoTrans_Costo As String
        Dim Sql As String
        Dim pdCostoCompra As Double : pdCostoCompra = 0
        Dim pdCostoTotalCompra As Double : pdCostoTotalCompra = 0
        Dim pdCostoVenta_D As Double : pdCostoVenta_D = 0
        Dim pdCostoVentaTotal_D As Double : pdCostoVentaTotal_D = 0
        Dim pdCostoVenta_S As Double : pdCostoVenta_S = 0
        Dim pdCostoVentaTotal_S As Double : pdCostoVentaTotal_S = 0
        Dim pdCantArtCV As Double : pdCantArtCV = 0
        Dim pdCantArtIS As Double : pdCantArtIS = 0
        Dim pdTotalCant As Double : pdTotalCant = 0
        Dim pdTotalCV_Unit_D As Double : pdTotalCV_Unit_D = 0
        Dim pdTotal_D As Double : pdTotal_D = 0
        Dim pdTotalCV_Unit_S As Double : pdTotalCV_Unit_S = 0
        Dim pdTotal_S As Double : pdTotal_S = 0
        Dim psMoneda As String = ""
        CalcularCosto = True
        Try
            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CmdGlobal3.Connection = Cn3
            If psTipoTrans = "3" Then TipoTrans_Costo = "1" Else TipoTrans_Costo = psTipoTrans

            CmdGlobal.CommandText = " SELECT COSTO_MOTIVO, COSTO_TIPO FROM TBINV_COSTO_VENTA_MOTIVO " _
                                  & " WHERE COSTO_MOTIVO = '" & psCodMotivo & "' AND COSTO_TIPO = '" & TipoTrans_Costo & "'"
            RsMotivo = CmdGlobal.ExecuteReader
            If RsMotivo.HasRows Then
                While RsMotivo.Read
                    CalcularCosto = True
                End While
            Else
                CalcularCosto = False
            End If
            RsMotivo.Close()
            CmdGlobal.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_DETALLE_DESPACHO]') and OBJECTPROPERTY(id, N'IsTABLE') = 1) drop TABLE [dbo].[V_DETALLE_DESPACHO]"
            CmdGlobal.ExecuteNonQuery()
            If Existe_Tabla("V_DETALLE_DESPACHO", psConexion) = False Then
                CmdGlobal.CommandText = " CREATE TABLE [dbo].[V_DETALLE_DESPACHO] ( [DESP_CODIGO] [float] NULL ," _
                                     & " [ARTICULO_CODIGO] [FLOAT] NULL, [CANT] [FLOAT] NULL, [COSTO_VENTA] [decimal](14, 4) NULL ) ON [PRIMARY]"
                CmdGlobal.ExecuteNonQuery()
            End If
            If CalcularCosto = True Then
                If psTipoTrans = "1" And psCodOrdenCompra <> "" Then
                    CmdGlobal.CommandText = " SELECT OCOMPRA_MONEDA " _
                                          & " FROM TBLOGIS_ORDENES_COMPRA " _
                                          & " WHERE OCOMPRA_NUMERAR = " & psCodOrdenCompra & " "
                    RsOC = CmdGlobal.ExecuteReader
                    If RsOC.HasRows Then
                        While RsOC.Read
                            psMoneda = Nu(RsOC!OCOMPRA_MONEDA)
                        End While
                    End If
                    RsOC.Close()
                    CmdGlobal.CommandText = " SELECT ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEP_CODIGO " _
                        & " FROM TBINV_ALMACEN_RECEPCION_DET R " _
                        & " WHERE RECEP_CODIGO = " & psCodigoTrans & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "' AND RECEPD_SYS_EST = '0' "
                    RsTrans = CmdGlobal.ExecuteReader
                    If RsTrans.HasRows Then
                        While RsTrans.Read
                            pdCantArtIS = Nz(RsTrans!RECEPD_CANT_XREC)
                            CmdGlobal2.CommandText = " SELECT DISTINCT OCOMPRAD_PRECIO_UNIT, OCOMPRAD_ARTICULO " _
                                                   & " FROM TBLOGIS_ORDENES_COMPRA_DETALLE " _
                                                   & " WHERE OCOMPRA_NUMERAR = " & psCodOrdenCompra & " AND OCOMPRAD_ARTICULO = " & Nu(RsTrans!ARTICULO_CODIGO) & " "
                            RsOC = CmdGlobal2.ExecuteReader
                            If RsOC.HasRows Then
                                While RsOC.Read
                                    pdCostoCompra = CDbl(Nz(RsOC!OCOMPRAD_PRECIO_UNIT))
                                End While
                            End If
                            RsOC.Close()
                            pdCostoTotalCompra = pdCostoCompra * Nz(RsTrans!RECEPD_CANT_XREC)
                            CmdGlobal2.CommandText = " SELECT ARTICULO_CODIGO, ARTICULO_COSTO_S, ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D " _
                                                   & " FROM TBINV_COSTO_VENTA " _
                                                   & " WHERE ARTICULO_CODIGO = " & Nu(RsTrans!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '" & psTipoUbic & "' " _
                                                   & " AND ARTICULO_UBIC_CODIGO = " & psCodigoUbic & ""
                            RsCosto = CmdGlobal2.ExecuteReader
                            If RsCosto.HasRows Then
                                While RsCosto.Read
                                    pdCostoVenta_D = Nz(RsCosto!ARTICULO_COSTO_D)
                                    pdCostoVentaTotal_D = Nz(RsCosto!ARTICULO_TOTAL_D)
                                    pdCostoVenta_S = Nz(RsCosto!ARTICULO_COSTO_S)
                                    pdCostoVentaTotal_S = Nz(RsCosto!ARTICULO_TOTAL_S)
                                    pdCantArtCV = Nz(RsCosto!ARTICULO_CANT)
                                    If psMoneda = "1" Then
                                        pdTotalCant = pdCantArtIS + pdCantArtCV
                                        pdTotal_D = pdCostoVentaTotal_D + pdCostoTotalCompra
                                        pdTotalCV_Unit_D = pdTotal_D / pdTotalCant
                                        pdTotal_S = pdTotal_D * Nz(pdTipoCambio)
                                        pdTotalCV_Unit_S = pdTotalCV_Unit_D * Nz(pdTipoCambio)
                                    Else
                                        pdTotalCant = pdCantArtIS + pdCantArtCV
                                        pdTotal_S = pdCostoVentaTotal_S + pdCostoTotalCompra
                                        pdTotalCV_Unit_S = pdTotal_S / pdTotalCant
                                        If Nz(pdTipoCambio) > 0 Then pdTotal_D = pdTotal_S / Nz(pdTipoCambio)
                                        If Nz(pdTipoCambio) > 0 Then pdTotalCV_Unit_D = pdTotalCV_Unit_S / Nz(pdTipoCambio)
                                    End If
                                    If pdTotalCant = 0 Then
                                        pdTotalCV_Unit_S = 0 : pdTotalCV_Unit_D = 0
                                        pdTotal_D = 0 : pdTotal_S = 0
                                    End If
                                    CmdGlobal3.CommandText = " UPDATE TBINV_COSTO_VENTA SET " _
                                                          & " ARTICULO_COSTO_S = " & pdTotalCV_Unit_S & " , " _
                                                          & " ARTICULO_COSTO_D = " & pdTotalCV_Unit_D & " , " _
                                                          & " ARTICULO_CANT = " & pdTotalCant & " , " _
                                                          & " ARTICULO_TOTAL_S = " & pdTotal_S & " , " _
                                                          & " ARTICULO_TOTAL_D = " & pdTotal_D & "  " _
                                                          & " WHERE ARTICULO_CODIGO = " & Nu(RsCosto!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '" & psTipoUbic & "' " _
                                                          & " AND ARTICULO_UBIC_CODIGO = " & psCodigoUbic & ""
                                    CmdGlobal3.ExecuteNonQuery()
                                End While
                            Else
                                pdCostoVenta_D = 0
                                pdCostoVentaTotal_D = 0
                                pdCostoVenta_S = 0
                                pdCostoVentaTotal_S = 0
                                pdCantArtCV = 0
                                If psMoneda = "1" Then
                                    pdTotalCant = pdCantArtIS + pdCantArtCV
                                    pdTotal_D = pdCostoVentaTotal_D + pdCostoTotalCompra
                                    pdTotalCV_Unit_D = pdTotal_D / pdTotalCant
                                    pdTotal_S = pdTotal_D * Nz(pdTipoCambio)
                                    pdTotalCV_Unit_S = pdTotalCV_Unit_D * Nz(pdTipoCambio)
                                Else
                                    pdTotalCant = pdCantArtIS + pdCantArtCV
                                    pdTotal_S = pdCostoVentaTotal_S + pdCostoTotalCompra
                                    pdTotalCV_Unit_S = pdTotal_S / pdTotalCant
                                    If Nz(pdTipoCambio) > 0 Then pdTotal_D = pdTotal_S / Nz(pdTipoCambio)
                                    If Nz(pdTipoCambio) > 0 Then pdTotalCV_Unit_D = pdTotalCV_Unit_S / Nz(pdTipoCambio)
                                End If
                                If pdTotalCant = 0 Then
                                    pdTotalCV_Unit_S = 0 : pdTotalCV_Unit_D = 0
                                    pdTotal_D = 0 : pdTotal_S = 0
                                End If
                                CmdGlobal3.CommandText = " INSERT INTO TBINV_COSTO_VENTA ( EMPRESA_CODIGO, ARTICULO_CODIGO, ARTICULO_COSTO_S, " _
                                                      & " ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D, ARTICULO_UBIC_TIPO, " _
                                                      & " ARTICULO_UBIC_CODIGO) " _
                                                      & " VALUES ('" & psCodEmpresa & "', " & Nu(RsTrans!ARTICULO_CODIGO) & ", " & Nz(pdTotalCV_Unit_S) & ", " _
                                                      & " " & pdTotalCV_Unit_D & ", " & pdTotalCant & ", " & pdTotal_S & ", " & pdTotal_D & ", '" & psTipoUbic & "', " _
                                                      & " " & psCodigoUbic & ")"
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                            RsCosto.Close()
                        End While
                    End If
                    RsTrans.Close()
                ElseIf psTipoTrans = "2" Then
                    psMoneda = psMonedaSalida
                    CmdGlobal.CommandText = " DELETE FROM V_DETALLE_DESPACHO "
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " SELECT S.ARTICULO_CODIGO, COUNT(S.ARTICULO_CODIGO) AS CANT, DD.DESP_CODIGO, DD.DESPD_COSTO_VENTA_D " _
                                          & " FROM dbo.TBINV_ALMACEN_DESPACHO_DET AS DD INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " AS S " _
                                          & " ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR " _
                                          & " WHERE (DD.DESPD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (DD.DESP_CODIGO = " & psCodigoTrans & ") " _
                                          & " AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                          & " GROUP BY DD.RECIBIDA_OK, S.ARTICULO_CODIGO, DD.DESP_CODIGO, DD.DESPD_COSTO_VENTA_D  "
                    RsTrans = CmdGlobal.ExecuteReader
                    If RsTrans.HasRows Then
                        While RsTrans.Read
                            CmdGlobal2.CommandText = " INSERT INTO V_DETALLE_DESPACHO (DESP_CODIGO, ARTICULO_CODIGO, CANT, COSTO_VENTA) " _
                                                   & " VALUES (" & Nu(RsTrans!DESP_CODIGO) & ", " & Nu(RsTrans!ARTICULO_CODIGO) & ", " & Nz(RsTrans!cant) & ", " _
                                                   & " " & Nz(RsTrans!DESPD_COSTO_VENTA_D) & ")"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    RsTrans.Close()
                    CmdGlobal.CommandText = " SELECT DD.ARTICULO_CODIGO, DESPD_CANTXDESP AS CANT, DD.DESP_CODIGO, DD.DESPD_COSTO_VENTA_D " _
                        & " FROM dbo.TBINV_ALMACEN_DESPACHO_DET_SINSERIE AS DD  " _
                        & " WHERE (DD.DESPD_SYS_EST = '0') AND (DD.DESP_CODIGO = " & psCodigoTrans & ") " _
                        & " AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') "
                    RsTrans = CmdGlobal.ExecuteReader
                    If RsTrans.HasRows Then
                        While RsTrans.Read
                            CmdGlobal2.CommandText = " INSERT INTO V_DETALLE_DESPACHO (DESP_CODIGO, ARTICULO_CODIGO, CANT, COSTO_VENTA) " _
                                                   & " VALUES (" & Nu(RsTrans!DESP_CODIGO) & ", " & Nu(RsTrans!ARTICULO_CODIGO) & ", " & Nz(RsTrans!cant) & ", " _
                                                   & " " & Nz(RsTrans!DESPD_COSTO_VENTA_D) & ")"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    End If
                    RsTrans.Close()
                    If pdTotalCant > 0 Then
                        CmdGlobal.CommandText = " SELECT * FROM V_DETALLE_DESPACHO WHERE DESP_CODIGO = " & psCodigoTrans & " "
                        RsTrans = CmdGlobal.ExecuteReader
                        If RsTrans.HasRows Then
                            While RsTrans.Read
                                pdCantArtIS = Nz(RsTrans!cant)
                                pdCostoCompra = Nu(RsTrans!COSTO_VENTA)
                                pdCostoTotalCompra = Nz(pdCostoCompra) * Nz(pdCantArtIS)
                                CmdGlobal2.CommandText = " SELECT ARTICULO_CODIGO, ARTICULO_COSTO_S, ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D " _
                                    & " FROM TBINV_COSTO_VENTA " _
                                    & " WHERE ARTICULO_CODIGO = " & Nu(RsTrans!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '" & psTipoUbic & "' " _
                                    & " AND ARTICULO_UBIC_CODIGO = " & psCodigoUbic & ""
                                RsCosto = CmdGlobal2.ExecuteReader
                                If RsCosto.HasRows Then
                                    While RsCosto.Read
                                        pdCostoVenta_D = Nz(RsCosto!ARTICULO_COSTO_D)
                                        pdCostoVentaTotal_D = Nz(RsCosto!ARTICULO_TOTAL_D)
                                        pdCostoVenta_S = Nz(RsCosto!ARTICULO_COSTO_S)
                                        pdCostoVentaTotal_S = Nz(RsCosto!ARTICULO_TOTAL_S)
                                        pdCantArtCV = Nz(RsCosto!ARTICULO_CANT)
                                        If psMoneda = "1" Then
                                            If pdCostoTotalCompra = 0 Then pdCostoTotalCompra = pdCantArtIS * pdCostoVenta_D
                                            pdTotalCant = pdCantArtCV - pdCantArtIS
                                            pdTotal_D = pdCostoVentaTotal_D - pdCostoTotalCompra
                                            If pdTotalCant > 0 Then pdTotalCV_Unit_D = pdTotal_D / pdTotalCant
                                            pdTotal_S = pdTotal_D * Nz(pdTipoCambio)
                                            pdTotalCV_Unit_S = pdTotalCV_Unit_D * Nz(pdTipoCambio)
                                        Else
                                            If pdCostoTotalCompra = 0 Then pdCostoTotalCompra = pdCantArtIS * pdCostoVenta_S
                                            pdTotalCant = pdCantArtCV - pdCantArtIS
                                            pdTotal_S = pdCostoVentaTotal_S - pdCostoTotalCompra
                                            If pdTotalCant > 0 Then pdTotalCV_Unit_S = pdTotal_S / pdTotalCant
                                            If Nz(pdTipoCambio) > 0 Then pdTotal_D = pdTotal_S / Nz(pdTipoCambio)
                                            If Nz(pdTipoCambio) > 0 Then pdTotalCV_Unit_D = pdTotalCV_Unit_S / Nz(pdTipoCambio)
                                        End If
                                        If pdTotalCant = 0 Then
                                            pdTotalCV_Unit_S = 0 : pdTotalCV_Unit_D = 0
                                            pdTotal_D = 0 : pdTotal_S = 0
                                        End If
                                        CmdGlobal3.CommandText = " UPDATE TBINV_COSTO_VENTA SET " _
                                                              & " ARTICULO_COSTO_S = " & pdTotalCV_Unit_S & " , " _
                                                              & " ARTICULO_COSTO_D = " & pdTotalCV_Unit_D & " , " _
                                                              & " ARTICULO_CANT = " & pdTotalCant & " , " _
                                                              & " ARTICULO_TOTAL_S = " & pdTotal_S & " , " _
                                                              & " ARTICULO_TOTAL_D = " & pdTotal_D & "  " _
                                                              & " WHERE ARTICULO_CODIGO = " & Nu(RsCosto!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '" & psTipoUbic & "' " _
                                                              & " AND ARTICULO_UBIC_CODIGO = " & psCodigoUbic & ""
                                        CmdGlobal3.ExecuteNonQuery()
                                    End While
                                Else
                                    pdCostoVenta_D = 0
                                    pdCostoVentaTotal_D = 0
                                    pdCostoVenta_S = 0
                                    pdCostoVentaTotal_S = 0
                                    pdCantArtCV = 0
                                    If psMoneda = "1" Then
                                        pdTotalCant = pdCantArtCV - pdCantArtIS
                                        pdTotal_D = pdCostoVentaTotal_D - pdCostoTotalCompra
                                        If pdTotalCant > 0 Then pdTotalCV_Unit_D = pdTotal_D / pdTotalCant
                                        pdTotal_S = pdTotal_D * Nz(pdTipoCambio)
                                        pdTotalCV_Unit_S = pdTotalCV_Unit_D * Nz(pdTipoCambio)
                                    Else
                                        pdTotalCant = pdCantArtCV - pdCantArtIS
                                        pdTotal_S = pdCostoVentaTotal_S - pdCostoTotalCompra
                                        If pdTotalCant > 0 Then pdTotalCV_Unit_S = pdTotal_S / pdTotalCant
                                        If Nz(pdTipoCambio) > 0 Then pdTotal_D = pdTotal_S / Nz(pdTipoCambio)
                                        If Nz(pdTipoCambio) > 0 Then pdTotalCV_Unit_D = pdTotalCV_Unit_S / Nz(pdTipoCambio)
                                    End If
                                    If pdTotalCant = 0 Then
                                        pdTotalCV_Unit_S = 0 : pdTotalCV_Unit_D = 0
                                        pdTotal_D = 0 : pdTotal_S = 0
                                    End If
                                    CmdGlobal3.CommandText = " INSERT INTO TBINV_COSTO_VENTA ( EMPRESA_CODIGO, ARTICULO_CODIGO, ARTICULO_COSTO_S, " _
                                                          & " ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D, ARTICULO_UBIC_TIPO, " _
                                                          & " ARTICULO_UBIC_CODIGO) " _
                                                          & " VALUES ('" & psCodEmpresa & "', " & Nu(RsTrans!ARTICULO_CODIGO) & ", " & Nz(pdTotalCV_Unit_S) & ", " _
                                                          & " " & pdTotalCV_Unit_D & ", " & pdTotalCant & ", " & pdTotal_S & ", " & pdTotal_D & ", '" & psTipoUbic & "', " _
                                                          & " " & psCodigoUbic & ")"
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                RsCosto.Close()
                            End While
                        End If
                        RsTrans.Close()
                    ElseIf psTipoTrans = "3" Then
                        psMoneda = psMonedaSalida
                        CmdGlobal.CommandText = " DELETE FROM V_DETALLE_DESPACHO "
                        CmdGlobal.ExecuteNonQuery()
                        Sql = " SELECT S.ARTICULO_CODIGO, COUNT(S.ARTICULO_CODIGO) AS CANT, DD.DESP_CODIGO, DD.DESPD_COSTO_VENTA_S, DD.DESPD_COSTO_VENTA_D " _
                            & " FROM dbo.TBINV_ALMACEN_DESPACHO_DET AS DD INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " AS S " _
                            & " ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR " _
                            & " WHERE (DD.DESPD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') AND (DD.DESP_CODIGO = " & psCodigoTrans & ") " _
                            & " AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                            & " GROUP BY DD.RECIBIDA_OK, S.ARTICULO_CODIGO, DD.DESP_CODIGO, DD.DESPD_COSTO_VENTA_S, DD.DESPD_COSTO_VENTA_D "
                        RsTrans = CmdGlobal.ExecuteReader
                        If RsTrans.HasRows Then
                            While RsTrans.Read
                                CmdGlobal2.CommandText = " INSERT INTO V_DETALLE_DESPACHO (DESP_CODIGO, ARTICULO_CODIGO, CANT, COSTO_VENTA_D, COSTO_VENTA_S) " _
                                                      & " VALUES (" & Nu(RsTrans!DESP_CODIGO) & ", " & Nu(RsTrans!ARTICULO_CODIGO) & ", " & Nz(RsTrans!cant) & ", " _
                                                      & " " & Nz(RsTrans!DESPD_COSTO_VENTA_D) & "," & Nz(RsTrans!DESPD_COSTO_VENTA_S) & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        End If
                        RsTrans.Close()
                        CmdGlobal.CommandText = " SELECT DD.ARTICULO_CODIGO, DESPD_CANTXDESP AS CANT, DD.DESP_CODIGO, DD.DESPD_COSTO_VENTA_S, DD.DESPD_COSTO_VENTA_D" _
                            & " FROM dbo.TBINV_ALMACEN_DESPACHO_DET_SINSERIE AS DD  " _
                            & " WHERE (DD.DESPD_SYS_EST = '0') AND (DD.DESP_CODIGO = " & psCodigoTrans & ") " _
                            & " AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') "
                        RsTrans = CmdGlobal.ExecuteReader
                        If RsTrans.HasRows Then
                            While RsTrans.Read
                                CmdGlobal2.CommandText = " INSERT INTO V_DETALLE_DESPACHO (DESP_CODIGO, ARTICULO_CODIGO, CANT,  COSTO_VENTA_D, COSTO_VENTA_S) " _
                                                      & " VALUES (" & Nu(RsTrans!DESP_CODIGO) & ", " & Nu(RsTrans!ARTICULO_CODIGO) & ", " & Nz(RsTrans!cant) & ", " _
                                                      & " " & Nz(RsTrans!DESPD_COSTO_VENTA_D) & "," & Nz(RsTrans!DESPD_COSTO_VENTA_S) & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            End While
                        End If
                        RsTrans.Close()
                        CmdGlobal.CommandText = " SELECT * FROM V_DETALLE_DESPACHO WHERE DESP_CODIGO = " & psCodigoTrans
                        RsTrans = CmdGlobal.ExecuteReader
                        If RsTrans.HasRows Then
                            While RsTrans.Read
                                pdCantArtIS = Nz(RsTrans!cant)
                                If psMoneda = "1" Then
                                    pdCostoCompra = Nu(RsTrans!DESPD_COSTO_VENTA_D)
                                    pdCostoTotalCompra = Nz(pdCostoCompra) * Nz(pdCantArtIS)
                                Else
                                    pdCostoCompra = Nu(RsTrans!DESPD_COSTO_VENTA_S)
                                    pdCostoTotalCompra = Nz(pdCostoCompra) * Nz(pdCantArtIS)
                                End If
                                CmdGlobal2.CommandText = " SELECT ARTICULO_CODIGO, ARTICULO_COSTO_S, ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D " _
                                    & " FROM TBINV_COSTO_VENTA " _
                                    & " WHERE ARTICULO_CODIGO = " & Nu(RsTrans!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '" & psTipoUbic & "' " _
                                    & " AND ARTICULO_UBIC_CODIGO = " & psCodigoUbic & ""
                                RsCosto = CmdGlobal2.ExecuteReader
                                If RsCosto.HasRows Then
                                    While RsCosto.Read
                                        pdCostoVenta_D = Nz(RsCosto!ARTICULO_COSTO_D)
                                        pdCostoVentaTotal_D = Nz(RsCosto!ARTICULO_TOTAL_D)
                                        pdCostoVenta_S = Nz(RsCosto!ARTICULO_COSTO_S)
                                        pdCostoVentaTotal_S = Nz(RsCosto!ARTICULO_TOTAL_S)
                                        pdCantArtCV = Nz(RsCosto!ARTICULO_CANT)
                                        If psMoneda = "1" Then
                                            pdTotalCant = pdCantArtCV + pdCantArtIS
                                            pdTotal_D = pdCostoVentaTotal_D + pdCostoTotalCompra
                                            If pdTotalCant > 0 Then pdTotalCV_Unit_D = pdTotal_D / pdTotalCant
                                            pdTotal_S = pdTotal_D * Nz(pdTipoCambio)
                                            pdTotalCV_Unit_S = pdTotalCV_Unit_D * Nz(pdTipoCambio)
                                        Else
                                            pdTotalCant = pdCantArtCV + pdCantArtIS
                                            pdTotal_S = pdCostoVentaTotal_S + pdCostoTotalCompra
                                            If pdTotalCant > 0 Then pdTotalCV_Unit_S = pdTotal_S / pdTotalCant
                                            If Nz(pdTipoCambio) > 0 Then pdTotal_D = pdTotal_S / Nz(pdTipoCambio)
                                            If Nz(pdTipoCambio) > 0 Then pdTotalCV_Unit_D = pdTotalCV_Unit_S / Nz(pdTipoCambio)
                                        End If
                                        If pdTotalCant = 0 Then
                                            pdTotalCV_Unit_S = 0 : pdTotalCV_Unit_D = 0
                                            pdTotal_D = 0 : pdTotal_S = 0
                                        End If
                                        CmdGlobal3.CommandText = " UPDATE TBINV_COSTO_VENTA SET " _
                                                              & " ARTICULO_COSTO_S = " & pdTotalCV_Unit_S & " , " _
                                                              & " ARTICULO_COSTO_D = " & pdTotalCV_Unit_D & " , " _
                                                              & " ARTICULO_CANT = " & pdTotalCant & " , " _
                                                              & " ARTICULO_TOTAL_S = " & pdTotal_S & " , " _
                                                              & " ARTICULO_TOTAL_D = " & pdTotal_D & "  " _
                                                              & " WHERE ARTICULO_CODIGO = " & Nu(RsCosto!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '" & psTipoUbic & "' " _
                                                              & " AND ARTICULO_UBIC_CODIGO = " & psCodigoUbic & ""
                                        CmdGlobal3.ExecuteNonQuery()
                                    End While
                                Else
                                    pdCostoVenta_D = 0
                                    pdCostoVentaTotal_D = 0
                                    pdCostoVenta_S = 0
                                    pdCostoVentaTotal_S = 0
                                    pdCantArtCV = 0
                                    If psMoneda = "1" Then
                                        pdTotalCant = pdCantArtCV + pdCantArtIS
                                        pdTotal_D = pdCostoVentaTotal_D + pdCostoTotalCompra
                                        If pdTotalCant > 0 Then pdTotalCV_Unit_D = pdTotal_D / pdTotalCant
                                        pdTotal_S = pdTotal_D * Nz(pdTipoCambio)
                                        pdTotalCV_Unit_S = pdTotalCV_Unit_D * Nz(pdTipoCambio)
                                    Else
                                        pdTotalCant = pdCantArtCV + pdCantArtIS
                                        pdTotal_S = pdCostoVentaTotal_S + pdCostoTotalCompra
                                        If pdTotalCant > 0 Then pdTotalCV_Unit_S = pdTotal_S / pdTotalCant
                                        If Nz(pdTipoCambio) > 0 Then pdTotal_D = pdTotal_S / Nz(pdTipoCambio)
                                        If Nz(pdTipoCambio) > 0 Then pdTotalCV_Unit_D = pdTotalCV_Unit_S / Nz(pdTipoCambio)
                                    End If
                                    If pdTotalCant = 0 Then
                                        pdTotalCV_Unit_S = 0 : pdTotalCV_Unit_D = 0
                                        pdTotal_D = 0 : pdTotal_S = 0
                                    End If
                                    CmdGlobal3.CommandText = " INSERT INTO TBINV_COSTO_VENTA ( EMPRESA_CODIGO, ARTICULO_CODIGO, ARTICULO_COSTO_S, " _
                                                          & " ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D, ARTICULO_UBIC_TIPO, " _
                                                          & " ARTICULO_UBIC_CODIGO) " _
                                                          & " VALUES ('" & psCodEmpresa & "', " & Nu(RsTrans!ARTICULO_CODIGO) & ", " & Nz(pdTotalCV_Unit_S) & ", " _
                                                          & " " & pdTotalCV_Unit_D & ", " & pdTotalCant & ", " & pdTotal_S & ", " & pdTotal_D & ", '" & psTipoUbic & "', " _
                                                          & " " & psCodigoUbic & ")"
                                    CmdGlobal3.ExecuteNonQuery()
                                End If
                                RsCosto.Close()
                            End While
                        End If
                        RsTrans.Close()
                    End If
                End If
            End If
        Catch ex As SqlException
        Catch ex As Exception
        Finally
        End Try
        Call IngCosto_SalRecep(psConexion, psCodEmpresa, psCodigoTrans, psTipoTrans, psCodOrdenCompra, pdTipoCambio)
    End Sub
    Private Sub IngCosto_SalRecep(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                  ByVal psCodSalida As String, ByVal psTipoMov As String,
                                  ByVal psOC As String, ByVal psTC As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim RsSal As SqlDataReader
        Dim RsCosto As SqlDataReader
        Try

            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CmdGlobal3.Connection = Cn3

            If psTipoMov = "2" Then
                CmdGlobal.CommandText = " SELECT DD.DESP_CODIGO, S.ARTICULO_CODIGO, DD.SERIE_NUMERAR, D.ALMACEN_ORIGEN  " _
                                      & " FROM dbo.TBINV_ALMACEN_DESPACHO_DET AS DD INNER JOIN TBINV_ALMACEN_DESPACHO AS D ON D.DESP_CODIGO = DD.DESP_CODIGO " _
                                      & " INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " AS S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR " _
                                      & " WHERE (DD.DESPD_SYS_EST = '0') AND (S.SERIE_SYS_EST = '0') " _
                                      & " AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (D.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                      & " AND (DD.DESP_CODIGO = " & psCodSalida & ")"
                RsSal = CmdGlobal.ExecuteReader
                If RsSal.HasRows Then
                    While RsSal.Read
                        CmdGlobal2.CommandText = " SELECT ARTICULO_CODIGO, ARTICULO_COSTO_S, ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D " _
                                               & " FROM TBINV_COSTO_VENTA " _
                                               & " WHERE ARTICULO_CODIGO = " & Nu(RsSal!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '1' " _
                                               & " AND ARTICULO_UBIC_CODIGO = " & Nu(RsSal!ALMACEN_ORIGEN) & ""
                        RsCosto = CmdGlobal2.ExecuteReader
                        If RsCosto.HasRows Then
                            While RsCosto.Read
                                CmdGlobal3.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO_DET SET " _
                                                      & " DESPD_COSTO_VENTA_S = " & Nz(RsCosto!ARTICULO_COSTO_S) & " , " _
                                                      & " DESPD_COSTO_VENTA_D = " & Nz(RsCosto!ARTICULO_COSTO_D) & "  " _
                                                      & " WHERE SERIE_NUMERAR = " & Nu(RsSal!Serie_Numerar) & " " _
                                                      & " AND DESP_CODIGO = " & Nu(RsSal!DESP_CODIGO) & ""
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        End If
                        RsCosto.Close()
                    End While
                End If
                RsSal.Close()
                CmdGlobal.CommandText = " SELECT DD.DESP_CODIGO, DD.ARTICULO_CODIGO, D.ALMACEN_ORIGEN  " _
                                      & " FROM dbo.TBINV_ALMACEN_DESPACHO_DET_SINSERIE AS DD INNER JOIN TBINV_ALMACEN_DESPACHO AS D ON D.DESP_CODIGO = DD.DESP_CODIGO " _
                                      & " WHERE (DD.DESPD_SYS_EST = '0') " _
                                      & " AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (D.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                      & " AND (DD.DESP_CODIGO = " & psCodSalida & ")"
                RsSal = CmdGlobal.ExecuteReader
                If RsSal.HasRows Then
                    While RsSal.Read
                        CmdGlobal2.CommandText = " SELECT ARTICULO_CODIGO, ARTICULO_COSTO_S, ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D " _
                            & " FROM TBINV_COSTO_VENTA " _
                            & " WHERE ARTICULO_CODIGO = " & Nu(RsSal!ARTICULO_CODIGO) & " AND ARTICULO_UBIC_TIPO = '1' " _
                            & " AND ARTICULO_UBIC_CODIGO = " & Nu(RsSal!ALMACEN_ORIGEN) & ""
                        RsCosto = CmdGlobal2.ExecuteReader
                        If RsCosto.HasRows Then
                            While RsCosto.Read
                                CmdGlobal3.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO_DET_SINSERIE SET " _
                                                      & " DESPD_COSTO_VENTA_S = " & Nz(RsCosto!ARTICULO_COSTO_S) & " , " _
                                                      & " DESPD_COSTO_VENTA_D = " & Nz(RsCosto!ARTICULO_COSTO_D) & "  " _
                                                      & " WHERE ARTICULO_CODIGO = " & Nu(RsSal!ARTICULO_CODIGO) & " " _
                                                      & " AND DESP_CODIGO = " & Nu(RsSal!DESP_CODIGO) & ""
                                CmdGlobal3.ExecuteNonQuery()
                            End While
                        End If
                        RsCosto.Close()
                    End While
                End If
                RsSal.Close()
            ElseIf psTipoMov = "1" Then
                Dim psMoneda As String = ""
                Dim pdCostoCompra_S As Double = 0
                Dim pdCostoCompra_D As Double = 0
                CmdGlobal.CommandText = " SELECT OCOMPRA_MONEDA " _
                                      & " FROM TBLOGIS_ORDENES_COMPRA " _
                                      & " WHERE OCOMPRA_NUMERAR = " & psOC
                RsSal = CmdGlobal.ExecuteReader
                If RsSal.HasRows Then
                    While RsSal.Read
                        psMoneda = Nu(RsSal!OCOMPRA_MONEDA)
                    End While
                End If
                RsSal.Close()
                CmdGlobal.CommandText = " SELECT ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEP_CODIGO " _
                                      & " FROM TBINV_ALMACEN_RECEPCION_DET R " _
                                      & " WHERE RECEP_CODIGO = " & psCodSalida & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "' AND RECEPD_SYS_EST = '0' "
                RsSal = CmdGlobal.ExecuteReader
                If RsSal.HasRows Then
                    While RsSal.Read
                        CmdGlobal2.CommandText = " SELECT DISTINCT OCOMPRAD_PRECIO_UNIT, OCOMPRAD_ARTICULO " _
                                               & " FROM TBLOGIS_ORDENES_COMPRA_DETALLE " _
                                               & " WHERE OCOMPRA_NUMERAR = " & psOC & " AND OCOMPRAD_ARTICULO = " & Nu(RsSal!ARTICULO_CODIGO) & " "
                        RsCosto = CmdGlobal2.ExecuteReader
                        If RsCosto.HasRows Then
                            While RsCosto.Read
                                If psMoneda = "1" Then
                                    pdCostoCompra_D = CDbl(Nz(RsCosto!OCOMPRAD_PRECIO_UNIT))
                                    pdCostoCompra_S = CDbl(Nz(RsCosto!OCOMPRAD_PRECIO_UNIT)) * CDbl(Nz(psTC))
                                Else
                                    pdCostoCompra_S = CDbl(Nz(RsCosto!OCOMPRAD_PRECIO_UNIT))
                                    pdCostoCompra_D = CDbl(Nz(RsCosto!OCOMPRAD_PRECIO_UNIT)) / CDbl(Nz(psTC))
                                End If
                            End While
                        End If
                        RsCosto.Close()
                        CmdGlobal3.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION_DET SET " _
                                              & " RECEPD_COSTO_VENTA_S = " & pdCostoCompra_S & " , " _
                                              & " RECEPD_COSTO_VENTA_D = " & pdCostoCompra_D & "  " _
                                              & " WHERE ARTICULO_CODIGO = " & Nu(RsSal!ARTICULO_CODIGO) & " " _
                                              & " AND RECEP_CODIGO = " & psCodSalida & " "
                        CmdGlobal3.ExecuteNonQuery()
                    End While
                End If
                RsSal.Close()
            End If
        Catch ex As SqlException
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Public Sub Movimiento_Kardex(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                 ByVal psCod_SalRecep As String, ByVal psCodMotivo As String, ByVal psCodArticulo As String,
                                 ByVal psTipoOrigen As String, ByVal psCodOrigen As String, ByVal psTipoDestino As String,
                                 ByVal psCodDestino As String, ByVal psMotivoDescrip As String, ByVal psTipoMov As String,
                                 ByVal psFecha As String, ByVal pdCant As Double, Optional ByVal psMovAnular As String = "",
                                 Optional ByVal pdCosto_S As Double = 0, Optional ByVal pdCosto_D As Double = 0,
                                 Optional ByVal psActStockCV As Boolean = False)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim fCmdGlobal As New SqlCommand
        Dim fCmdGlobal2 As New SqlCommand
        Dim fCmdGlobal3 As New SqlCommand
        Dim RsKardex As SqlDataReader
        Dim pdCostoIng_S As Double = 0
        Dim pdTotalIng_S As Double = 0
        Dim pdCostoIng_D As Double = 0
        Dim pdTotalIng_D As Double = 0
        Dim pdCostoSal_S As Double = 0
        Dim pdTotalSal_S As Double = 0
        Dim pdCostoSal_D As Double = 0
        Dim pdTotalSal_D As Double = 0
        Dim psNroParte As String = ""
        Dim psDescripcion As String = ""
        Dim c As String = ""
        pdCostoIng_S = 0 : pdTotalIng_S = 0
        pdCostoSal_S = 0 : pdTotalSal_S = 0
        pdCostoIng_D = 0 : pdTotalIng_D = 0
        pdCostoSal_D = 0 : pdTotalSal_D = 0
        Try
            Cn.Open() : Cn2.Open() : Cn3.Open()
            fCmdGlobal.Connection = Cn : fCmdGlobal2.Connection = Cn2
            fCmdGlobal3.Connection = Cn3

            fCmdGlobal.CommandText = " SELECT MAX(COSTO_CODIGO) FROM TBINV_KARDEX_COSTO"
            RsKardex = fCmdGlobal.ExecuteReader
            If RsKardex.HasRows Then
                While RsKardex.Read
                    c = Nz(RsKardex(0)) + 1
                End While
            Else
                c = 1
            End If
            RsKardex.Close()
            If psMovAnular = "S" Then
                If psTipoMov = "1" Then
                    pdCostoIng_D = pdCosto_D
                    pdTotalIng_D = pdCosto_D * pdCant
                    pdCostoIng_S = pdCosto_S
                    pdTotalIng_S = pdCosto_S * pdCant
                Else
                    pdCostoSal_D = pdCosto_D
                    pdTotalSal_D = pdCosto_D * pdCant
                    pdCostoSal_S = pdCosto_S
                    pdTotalSal_S = pdCosto_S * pdCant
                End If
            Else
                fCmdGlobal.CommandText = " SELECT ARTICULO_CODIGO, ARTICULO_COSTO_S, ARTICULO_COSTO_D, ARTICULO_CANT, ARTICULO_TOTAL_S, ARTICULO_TOTAL_D " _
                                      & " FROM TBINV_COSTO_VENTA " _
                                      & " WHERE ARTICULO_CODIGO = " & psCodArticulo & " AND ARTICULO_UBIC_TIPO = '1' " _
                                      & " AND ARTICULO_UBIC_CODIGO = " & psCodOrigen & ""
                RsKardex = fCmdGlobal.ExecuteReader
                If RsKardex.HasRows Then
                    While RsKardex.Read
                        If psTipoMov = "1" Then
                            pdCostoIng_D = CDbl(Nz(RsKardex!ARTICULO_COSTO_D))
                            pdTotalIng_D = CDbl(Nz(RsKardex!ARTICULO_TOTAL_D))
                            pdCostoIng_S = CDbl(Nz(RsKardex!ARTICULO_COSTO_S))
                            pdTotalIng_S = CDbl(Nz(RsKardex!ARTICULO_TOTAL_S))
                        Else
                            pdCostoSal_D = CDbl(Nz(RsKardex!ARTICULO_COSTO_D))
                            pdTotalSal_D = CDbl(Nz(RsKardex!ARTICULO_TOTAL_D))
                            pdCostoSal_S = CDbl(Nz(RsKardex!ARTICULO_COSTO_S))
                            pdTotalSal_S = CDbl(Nz(RsKardex!ARTICULO_TOTAL_S))
                        End If
                    End While
                End If
                RsKardex.Close()
            End If
            Dim psMoneda As String = ""
            Dim pdCostoCompra As Double : pdCostoCompra = 0
            Dim psCodOrdenCompra As String = ""
            Dim pdCantArtIS As Double : pdCantArtIS = 0
            Dim psTipoCambio As String
            Dim RsOC As SqlDataReader
            Dim RsTrans As SqlDataReader
            Dim objCont As New clsCont_Funciones
            Dim psFechaTC As String = Right(psFecha, 4) + Mid(psFecha, 4, 2) + Left(psFecha, 2)
            If psTipoMov = "1" Then
                fCmdGlobal.CommandText = " SELECT OCOMPRA_MONEDA,OCOMPRA_NUMERAR " _
                                      & " FROM TBLOGIS_ORDENES_COMPRA INNER JOIN TBINV_ALMACEN_RECEPCION ON RECEP_DOC_NUMERACION = RIGHT('000000'+CONVERT(VARCHAR(10),OCOMPRA_NUMERAR),6) " _
                                      & " WHERE RECEP_CODIGO = " & psCod_SalRecep
                RsOC = fCmdGlobal.ExecuteReader
                If RsOC.HasRows Then
                    While RsOC.Read
                        psMoneda = Nu(RsOC!OCOMPRA_MONEDA)
                        psCodOrdenCompra = Nu(RsOC!OCOMPRA_NUMERAR)
                    End While
                End If
                RsOC.Close()
                If psCodOrdenCompra <> "" Then
                    psTipoCambio = objCont.Hallar_Valor_Compra(psConexion, psFechaTC)
                    fCmdGlobal.CommandText = " SELECT ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEP_CODIGO " _
                                          & " FROM TBINV_ALMACEN_RECEPCION_DET R " _
                                          & " WHERE RECEP_CODIGO = " & psCod_SalRecep & " AND EMPRESA_CODIGO = '" & psCodEmpresa & "' AND RECEPD_SYS_EST = '0' AND ARTICULO_CODIGO = " & psCodArticulo & ""
                    RsTrans = fCmdGlobal.ExecuteReader
                    If RsTrans.HasRows Then
                        While RsTrans.Read
                            pdCantArtIS = Nz(RsTrans!RECEPD_CANT_XREC)
                            fCmdGlobal2.CommandText = " SELECT DISTINCT OCOMPRAD_PRECIO_UNIT, OCOMPRAD_ARTICULO " _
                                                   & " FROM TBLOGIS_ORDENES_COMPRA_DETALLE " _
                                                   & " WHERE OCOMPRA_NUMERAR = " & psCodOrdenCompra & " AND OCOMPRAD_ARTICULO = " & Nu(RsTrans!ARTICULO_CODIGO) & " "
                            RsOC = fCmdGlobal2.ExecuteReader
                            If RsOC.HasRows Then
                                While RsOC.Read
                                    pdCostoCompra = CDbl(Nz(RsOC!OCOMPRAD_PRECIO_UNIT))
                                End While
                            End If
                            RsOC.Close()
                        End While
                    End If
                    RsTrans.Close()
                    If psTipoMov = "1" Then
                        pdCostoIng_D = pdCostoCompra
                        pdTotalIng_D = pdCostoCompra * pdCant
                        pdCostoIng_S = pdCostoCompra * Nz(psTipoCambio)
                        pdTotalIng_S = pdCostoSal_S * pdCant
                    Else
                        pdCostoSal_S = pdCostoCompra
                        pdTotalSal_S = pdCostoCompra * pdCant
                        pdCostoSal_D = pdCostoCompra / Nz(psTipoCambio)
                        pdTotalSal_D = pdCostoSal_S * pdCant
                    End If
                End If
            End If
            fCmdGlobal.CommandText = " SELECT ART_CODEQUIVA, ART_DESCRIPCION FROM TBINV_ARTICULOS WHERE ART_CODIGO = " & psCodArticulo
            RsKardex = fCmdGlobal.ExecuteReader
            If RsKardex.HasRows Then
                While RsKardex.Read
                    psNroParte = Nu(RsKardex!ART_CODEQUIVA) : psDescripcion = Nu(RsKardex!ART_DESCRIPCION)
                End While
            End If
            RsKardex.Close()
            fCmdGlobal.CommandText = " INSERT INTO  TBINV_KARDEX_COSTO (EMPRESA_CODIGO, COSTO_CODIGO, COSTO_DOC_NRO, COSTO_FECHA_MOV, COSTO_MOTIVO, COSTO_ART_COD, COSTO_ART_NPARTE, COSTO_ART_DESCRIPCION, " _
                                  & " COSTO_ING_CANT, COSTO_ING_COSTO_S, COSTO_ING_TOTAL_S, COSTO_ING_COSTO_D, COSTO_ING_TOTAL_D, " _
                                  & " COSTO_SAL_CANT, COSTO_SAL_COSTO_S, COSTO_SAL_TOTAL_S, COSTO_SAL_COSTO_D, COSTO_SAL_TOTAL_D," _
                                  & " COSTO_SALDO_CANT, COSTO_SALDO_COSTO_S, COSTO_SALDO_TOTAL_S, COSTO_SALDO_COSTO_D, COSTO_SALDO_TOTAL_D," _
                                  & " COSTO_MOTIVO_COD, COSTO_TRANS_COD, COSTO_TIPO_MOV, COSTO_DOC_TIPO, COSTO_ORIGENDESTINO_COD, COSTO_ORIGENDESTINO_TIPO,COSTO_FACTURA, UBICACT_TIPO, UBICACT_CODIGO)" _
                                  & " VALUES ('" & psCodEmpresa & "', " & c & ", '" & psCod_SalRecep & "', '" & psFecha & "','" & psMotivoDescrip & "', " & psCodArticulo & ", '" & psNroParte & "','" & psDescripcion & "', " _
                                  & " " & pdCant & "," & Format(Nz(pdCostoIng_S), "0.000") & "," & Format(Nz(pdTotalIng_S), "0.000") & ", " & Format(Nz(pdCostoIng_D), "0.000") & "," & Format(Nz(pdTotalIng_D), "0.000") & ", " _
                                  & " " & pdCant & ", " & Format(Nz(pdCostoSal_S), "0.000") & "," & Format(Nz(pdTotalSal_S), "0.000") & ", " & Format(Nz(pdCostoSal_D), "0.000") & "," & Format(Nz(pdTotalSal_D), "0.000") & ", " _
                                  & " 0,0,0,0,0, " _
                                  & " '" & psCodMotivo & "'," & psCod_SalRecep & ",'" & psTipoMov & "','09'," & psCodDestino & ",'" & psTipoDestino & "','','1'," & psCodOrigen & ")"
            fCmdGlobal.ExecuteNonQuery()
        Catch ex As SqlException
        Catch ex As Exception
        Finally
        End Try
        Call Actualizar_StockCV(psConexion, psCodEmpresa, psCodArticulo)
    End Sub
    Public Sub Actualizar_StockCV(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                  Optional ByVal psCodArt As String = "")
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim RsS As SqlDataReader
        Try
            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CmdGlobal3.Connection = Cn3

            CmdGlobal.CommandText = " SELECT UBICACT_TIPO, ALMACEN_CODIGO, ARTICULO_CODIGO, SAA_STOCK_INICIAL, SAA_STOCK_ACTUAL " _
                                  & " FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND SAA_SYS_EST = '0' "
            If psCodArt <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ARTICULO_CODIGO = " & psCodArt
            RsS = CmdGlobal.ExecuteReader
            If RsS.HasRows Then
                While RsS.Read
                    If Nz(RsS!SAA_STOCK_ACTUAL) = 0 Then
                        CmdGlobal2.CommandText = " UPDATE TBINV_COSTO_VENTA SET " _
                                              & " ARTICULO_COSTO_S = 0, " _
                                              & " ARTICULO_COSTO_D = 0, " _
                                              & " ARTICULO_CANT = 0,  " _
                                              & " ARTICULO_TOTAL_S = 0, " _
                                              & " ARTICULO_TOTAL_D = 0 " _
                                              & " WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND ARTICULO_CODIGO = " & Nu(RsS!ARTICULO_CODIGO) & " " _
                                              & " AND ARTICULO_UBIC_TIPO = '" & Nu(RsS!UBICACT_TIPO) & "' AND ARTICULO_UBIC_CODIGO = " & Nu(RsS!ALMACEN_CODIGO)
                        CmdGlobal2.ExecuteNonQuery()
                    Else
                        CmdGlobal2.CommandText = " UPDATE TBINV_COSTO_VENTA SET " _
                                              & " ARTICULO_CANT = " & Nz(RsS!SAA_STOCK_ACTUAL) & ",  " _
                                              & " ARTICULO_TOTAL_S =  ISNULL(ARTICULO_COSTO_S,0) * " & Nz(RsS!SAA_STOCK_ACTUAL) & ", " _
                                              & " ARTICULO_TOTAL_D =  ISNULL(ARTICULO_COSTO_D,0) * " & Nz(RsS!SAA_STOCK_ACTUAL) & " " _
                                              & " WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND ARTICULO_CODIGO = " & Nu(RsS!ARTICULO_CODIGO) & " " _
                                              & " AND ARTICULO_UBIC_TIPO = '" & Nu(RsS!UBICACT_TIPO) & "' AND ARTICULO_UBIC_CODIGO = " & Nu(RsS!ALMACEN_CODIGO)
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                End While
            End If
            RsS.Close()
        Catch ex As SqlException
        Catch ex As Exception
        Finally
        End Try
    End Sub
    Public Sub RecepcionAutomatica(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psUser As String,
                                   ByVal pTipoOrigen As String, ByVal pCodAlmacenO As String, ByVal pCodSeccionO As String,
                                   ByVal pTipoDestino As String, ByVal pCodAlmacenD As String, ByVal pCodSeccionD As String,
                                   ByVal pCodigoSalida As String, ByVal pCodMotivoSalida As String, ByVal psFechaTrans As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim Cn4 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim CmdGlobal4 As New SqlCommand
        Dim FechaServer As String = FechaActual()
        Dim HoraServer As String = HoraActual()
        Dim ValorSys As String = FechaServer & HoraServer & psUser
        Dim objProceso As New clsInv_Procesos
        Dim Rs As SqlDataReader
        Dim Rs2X As SqlDataReader
        Dim Rs1 As SqlDataReader
        Dim EstadoDesp As String = ""
        Dim lblCodTrans As String = ""
        Dim QARec As Long = 0
        Dim QRec As Long = 0
        Dim QFaltRec As Long = 0
        Dim EstPedido As String = ""
        Dim AtencionOk As String = "" 'Este campo sirve para saber si c/item detalle del pedido (sea 1 o n registros x item detalle) ya tiene atención terminada
        Dim StockAc As Double = 0
        Dim lblNroMovimiento As String = ""
        Dim objCont As New clsCont_Funciones
        Try
            Dim lblAño As String = ""
            Dim lblPer As String = ""
            Dim lblCodTransD As String = ""
            Dim lblCodTipoTrans As String = ""
            Dim lblVale As String = ""
            Dim lblCodMov As String = ""
            Dim TipoOperac As String, NomOperac As String = ""
            Dim item As Long = 0
            Dim lblIngresoNumerar As String = ""
            Dim lblCodOrigen As String = ""
            Cn.Open() : Cn2.Open() : Cn3.Open() : Cn4.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CmdGlobal3.Connection = Cn3 : CmdGlobal4.Connection = Cn4

            Select Case pTipoDestino
                Case "1" 'destino almacen
                    lblCodTipoTrans = "0" 'ingreso
                    'datos para el movimiento
                    lblAño = objCont.AñoSistema(psConexion, psCodEmpresa)
                    lblPer = ""
                    CmdGlobal.CommandText = " SELECT PER_PERIODO FROM TBPERIODIFICACION WHERE (PER_EMPRESA = '" & psCodEmpresa & "') AND (PER_AÑO = '" & lblAño & "') AND (PER_ACTUAL = 'S') AND (PER_SYS_EST = '0')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblPer = Nu(Rs!PER_PERIODO)
                        End While
                    End If
                    Rs.Close()
                    If lblPer = "" Then Exit Sub
                    lblCodTrans = ""
                    Dim EstadoIngreso As String
                    Dim EstadoSalida As String
                    EstadoSalida = pCodMotivoSalida
                    EstadoIngreso = Estado_Ingreso(EstadoSalida)
                    pCodMotivoSalida = EstadoIngreso
                    TipoOperac = Tipo_OperacIng(pCodMotivoSalida)
                    NomOperac = Nombre_OperacIng(pCodMotivoSalida)
                    If TipoOperac = "" Then Exit Sub
                    CmdGlobal.CommandText = " SELECT TRANS_CODIGO,TRANS_DESCRIPCION FROM TBINV_TRANSACCIONES_ALMACEN WHERE TRANS_SYS_EST='0' AND " _
                                          & " TRANS_TIPO='" & lblCodTipoTrans & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND " & TipoOperac & "='S' ORDER BY TRANS_CODIGO"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblCodTrans = Nu(Rs!TRANS_codigo)
                        End While
                    End If
                    Rs.Close()
                    If lblCodTrans = "" Then Exit Sub
                    lblVale = ""
                    CmdGlobal.CommandText = " SELECT MAX(M.MOVAL_NRO_VALE) FROM TBINV_MOVIMIENTOS_ALMACEN M INNER JOIN TBINV_TRANSACCIONES_ALMACEN T ON M.TRANS_CODIGO = T.TRANS_CODIGO AND M.EMPRESA_CODIGO=T.EMPRESA_CODIGO " _
                                          & " WHERE (T.TRANS_TIPO = '" & lblCodTipoTrans & "') AND (M.EMPRESA_CODIGO='" & psCodEmpresa & "') AND (ALMACEN_CODIGO='" & pCodAlmacenD & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblVale = Format(Nz(Rs(0)) + 1, "00000000")
                        End While
                    Else
                        lblVale = "00000001"
                    End If
                    Rs.Close()
                    lblCodMov = ""
                    CmdGlobal.CommandText = " SELECT MAX(MOVAL_CODIGO) FROM TBINV_MOVIMIENTOS_ALMACEN WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblCodMov = Format(Nz(Rs(0)) + 1, "00000000")
                        End While
                    Else
                        lblCodMov = "00000001"
                    End If
                    Rs.Close()
                    lblCodTransD = ""
                    CmdGlobal.CommandText = " SELECT TRANSD_CODIGO,TRANSD_VALOR FROM TBINV_TRANS_ALMACEN_DETALLE WHERE (TRANSD_DETALLE = '2') AND " _
                                          & " (TRANS_CODIGO = " & lblCodTrans & ") AND EMPRESA_CODIGO='" & psCodEmpresa & "' ORDER BY TRANSD_CODIGO"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblCodTransD = Nu(Rs!TRANSD_CODIGO)
                        End While
                    End If
                    Rs.Close()

                    CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTOS_ALMACEN(EMPRESA_CODIGO, MOVAL_CODIGO,ALMACEN_CODIGO,MOVAL_SYS_EST,MOVAL_SYS_CRE) " _
                                          & " VALUES('" & psCodEmpresa & "'," & lblCodMov & ",'" & pCodAlmacenD & "','0','" & ValorSys & "')"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " UPDATE TBINV_MOVIMIENTOS_ALMACEN SET CONTABLE_AÑO='" & lblAño & "',CONTABLE_PERIODO=" & lblPer & "," _
                                          & " TRANS_CODIGO=" & lblCodTrans & ",MOVAL_NRO_VALE='" & lblVale & "',MOVAL_FECHA='" & FechaServer & "'," _
                                          & " MOVAL_SYS_MOD='" & ValorSys & "',MOVAL_TOTAL_ART = " & IIf(pTipoDestino = "1", " " _
                                          & " (SELECT DESP_CANTXDESP FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND DESP_CODIGO = " & pCodigoSalida & ")", " " _
                                          & " (SELECT OSAL_CANT_ENV FROM TBINV_CCOSTO_SALIDA WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND OSAL_CODIGO = " & pCodigoSalida & ")") & " " _
                                          & " WHERE MOVAL_CODIGO=" & lblCodMov & " AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                    CmdGlobal.ExecuteNonQuery()

                    item = 0
                    '::::::::::::::::::::::::::::::::: ARTICULOS Q USAN SERIE
                    If pTipoOrigen = "1" Then
                        CmdGlobal.CommandText = " SELECT DD.DESPD_ITEM, DD.SERIE_NUMERAR, DD.DESPD_OK,RECIBIDA_OK,S.ARTICULO_CODIGO, S.SERIE_NRO,S.SERIE_CARACTERISTICAS,S.PLACA_NRO, A.ART_DESCRIPCION,DD.DESPD_FUNCION AS COD_FUNCION " _
                                              & " FROM TBINV_ALMACEN_DESPACHO_DET DD INNER JOIN TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR INNER JOIN" _
                                              & " TBINV_ARTICULOS A ON DD.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND S.ARTICULO_CODIGO = A.ART_CODIGO " _
                                              & " WHERE (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DD.DESP_CODIGO =" & pCodigoSalida & ") AND (DD.DESPD_SYS_EST = '0') AND (DD.DESPD_OK='S') AND (RECIBIDA_OK='N')" _
                                              & " ORDER BY DD.DESPD_ITEM"
                    Else
                        CmdGlobal.CommandText = " SELECT SD.OSALD_ORDEN, SD.SERIE_NUMERAR, SD.ENVIADA_OK,RECIBIDA_OK,S.ARTICULO_CODIGO, S.SERIE_NRO,S.SERIE_CARACTERISTICAS,S.PLACA_NRO,A.ART_DESCRIPCION,SD.OSALD_FUNCION AS COD_FUNCION " _
                                              & " FROM TBINV_CCOSTO_SALIDA_DET SD INNER JOIN" _
                                              & " TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON SD.SERIE_NUMERAR = S.SERIE_NUMERAR INNER JOIN" _
                                              & " TBINV_ARTICULOS A ON SD.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND S.ARTICULO_CODIGO = A.ART_CODIGO " _
                                              & " WHERE (SD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (SD.OSAL_CODIGO =" & pCodigoSalida & ") AND (SD.OSALD_SYS_EST = '0') AND (SD.ENVIADA_OK='S') AND (RECIBIDA_OK='N')" _
                                              & " ORDER BY SD.OSALD_ORDEN"
                    End If
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            item = item + 1
                            CmdGlobal2.CommandText = " SELECT * FROM TBINV_MOV_ALMACEN_ARTICULOS WHERE (MOVAL_CODIGO =" & lblCodMov & ") AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & psCodEmpresa & "') AND (MOVALA_SYS_EST='0')"
                            Rs2X = CmdGlobal2.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    CmdGlobal3.CommandText = " UPDATE TBINV_MOV_ALMACEN_ARTICULOS SET MOVALA_ART_CANTIDAD=" & Nz(Rs2X!MOVALA_ART_CANTIDAD) + 1 & " WHERE (MOVAL_CODIGO =" & lblCodMov & ") AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (EMPRESA_CODIGO='" & psCodEmpresa & "') AND (MOVALA_SYS_EST='0')"
                                    CmdGlobal3.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal3.CommandText = " INSERT INTO TBINV_MOV_ALMACEN_ARTICULOS(MOVAL_CODIGO, ARTICULO_CODIGO,MOVALA_ART_CANTIDAD, MOVALA_ART_ORDEN,EMPRESA_CODIGO,MOVALA_SYS_EST) " _
                                                       & " VALUES(" & lblCodMov & "," & Nu(Rs!ARTICULO_CODIGO) & ",1," & item & ",'" & psCodEmpresa & "','0')"
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                            Rs2X.Close()

                            CmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & pCodAlmacenD & ") AND (UBICACT_TIPO='1')" _
                                                   & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs2X = CmdGlobal2.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    StockAc = Nz(Rs2X!SAA_STOCK_ACTUAL)
                                    If lblCodTipoTrans = "0" Then  'INGRESO
                                        StockAc = StockAc + 1
                                    Else 'SALIDA
                                        StockAc = StockAc - 1
                                    End If
                                    CmdGlobal3.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & pCodAlmacenD & ") AND (UBICACT_TIPO='1') " _
                                                           & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    CmdGlobal3.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal3.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                       & " VALUES('1'," & pCodAlmacenD & "," & Nu(Rs!ARTICULO_CODIGO) & ",1,'0','" & psCodEmpresa & "')"
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                            Rs2X.Close()
                            'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL=========================================================================
                            CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                            Rs2X = CmdGlobal2.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    CmdGlobal3.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET MOV_ESTADO ='3' WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                    CmdGlobal3.ExecuteNonQuery()
                                End While
                            End If
                            Rs2X.Close()
                            If pTipoOrigen = "1" Then
                                lblCodOrigen = pCodAlmacenO
                            ElseIf pTipoOrigen = "2" Then
                                lblCodOrigen = pCodSeccionO
                            End If
                            CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & lblNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' "
                            Rs2X = CmdGlobal2.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    CmdGlobal3.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET NRO_ARTICULO =" & Nz(Rs2X!NRO_ARTICULO) + 1 & " WHERE (CODIGO_ARTICULO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & lblNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                    CmdGlobal3.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal3.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                Rs1 = CmdGlobal3.ExecuteReader
                                If Rs1.HasRows Then
                                    While Rs1.Read
                                        lblNroMovimiento = Nz(Rs1(0)) + 1
                                    End While
                                Else
                                    lblNroMovimiento = "00000001"
                                End If
                                Rs1.Close()
                                '1: INGRESO, 2:SALIDA
                                Call Movimiento_Kardex(psConexion, psCodEmpresa, pCodigoSalida, pCodMotivoSalida, Nu(Rs!ARTICULO_CODIGO), pTipoDestino, pCodAlmacenD, pTipoOrigen, lblCodOrigen, "", "1", psFechaTrans, 1)
                                CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                                       & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                       & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & pTipoDestino & "','" & pCodAlmacenD & "','" & pTipoOrigen & "','" & lblCodOrigen & "', " _
                                                       & " '" & pCodigoSalida & "','" & Nu(Rs!ARTICULO_CODIGO) & "','1','" & ValorSys & "','3','" & pCodMotivoSalida & "','" & FechaServer & "','0')"
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                            Rs2X.Close()

                            If pCodMotivoSalida = "5" Then 'TRANSFERENCIA <- falta chequear la tabla
                                CmdGlobal2.CommandText = "SELECT MAX(INGXT_NUMERAR) FROM TBINV_MOV_ALMACEN_INGXTRANS WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                Rs2X = CmdGlobal2.ExecuteReader
                                If Rs2X.HasRows Then
                                    While Rs2X.Read
                                        lblIngresoNumerar = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    lblIngresoNumerar = "1"
                                End If
                                Rs2X.Close()
                                CmdGlobal2.CommandText = " INSERT INTO TBINV_MOV_ALMACEN_INGXTRANS(EMPRESA_CODIGO,INGXT_NUMERAR,INGRESO_TIPO,INGRESO_CODIGO," _
                                                       & " SERIE_NUMERAR, CANT_INGRESA,ALMACEN_MOV_CODIGO,USUARIO,FECHA,HORA) " _
                                                       & " VALUES('" & psCodEmpresa & "'," & lblIngresoNumerar & "," & pTipoOrigen & ",'" & pCodigoSalida & "'," _
                                                       & Nu(Rs!Serie_Numerar) & ",1," & lblCodMov & ",'" & psUser & "','" & FechaServer & "','" & HoraServer & "')"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                            If pCodMotivoSalida = "2" Then 'MANTENIMIENTO: TIPO(0 INGRESO,1 SALIDA); TIPO_DOC(R ORECEPCION, D ODESPACHO, A OSALIDA); TIPO_ORIGEN(0 SIN ORIGEN,1 ALMACEN, 2 CCOSTO); MANT_ESTADO(1 POR REVISAR,2 REVISADO)
                                CmdGlobal2.CommandText = "SELECT MAX(MANT_CODIGO) FROM TBINV_MOV_ALMACEN_MANTENIMIENTO_" & psCodEmpresa & " "
                                Rs2X = CmdGlobal2.ExecuteReader
                                If Rs2X.HasRows Then
                                    While Rs2X.Read
                                        lblIngresoNumerar = Nz(Rs(0)) + 1
                                    End While
                                Else
                                    lblIngresoNumerar = "1"
                                End If
                                Rs2X.Close()
                                CmdGlobal2.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_MANTENIMIENTO_" & psCodEmpresa & " (MANT_CODIGO, MANT_TIPO, MANT_DOCUME_TIPO, MANT_DOCUME_CODIGO, ALMACEN_DESTINO, MANT_ORIGEN_TIPO," _
                                                       & "MANT_ORIGEN_CODIGO, SERIE_NUMERAR,MANT_FECHA_ING, MANT_HORA_ING, MANT_USUARIO_ING, MANT_SYS_EST,MANT_ESTADO) VALUES(" _
                                                       & lblIngresoNumerar & ",'0','" & IIf(pTipoOrigen = "1", "D", "S") & "'," & pCodigoSalida & "," & pCodAlmacenD & ",'" & pTipoOrigen & "'," _
                                                       & IIf(pTipoOrigen = "1", pCodAlmacenO, pCodSeccionO) & "," & Nu(Rs!Serie_Numerar) & ",'" & FechaServer & "','" & HoraServer & "','" & psUser & "','0','1')"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                            If pTipoOrigen = "1" Then
                                CmdGlobal2.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_CODIGO=" & pCodigoSalida & " AND SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                CmdGlobal2.ExecuteNonQuery()
                            Else
                                CmdGlobal2.CommandText = " UPDATE TBINV_CCOSTO_SALIDA_DET SET RECIBIDA_OK='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO=" & pCodigoSalida & " AND SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                            CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='1',UBICACT_CODIGO=" & pCodAlmacenD & ",UBICACT_SYS='" & ValorSys & "',SERIE_FUNCION = '" & Nu(Rs!COD_FUNCION) & "' WHERE SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                            CmdGlobal2.ExecuteNonQuery()
                            'ESTADO: 0 primera vez, los demas CodMotivo
                            CmdGlobal2.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,SERIE_FUNCION, INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO) " _
                                                   & " VALUES(" & Nu(Rs!Serie_Numerar) & ",'1'," & pCodAlmacenD & ",'" & pCodMotivoSalida & "','0','" & ValorSys & "','" & Nu(Rs!COD_FUNCION) & "','" & FechaServer & "','" & IIf(pTipoOrigen = "1", "1", "2") & "','" & pCodigoSalida & "','" & pCodMotivoSalida & "')"
                            CmdGlobal2.ExecuteNonQuery()
                            '---------------------------------------------------------
                            Select Case EstadoSalida
                                Case 1 'prestamo
                                    'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                    If pTipoOrigen = "1" Then  'O.D.
                                        CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.DESP_CODIGO=" & pCodigoSalida & " AND A.SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                        CmdGlobal2.ExecuteNonQuery()
                                    Else 'O.S.
                                        CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.OSAL_CODIGO=" & pCodigoSalida & " AND A.SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                        CmdGlobal2.ExecuteNonQuery()
                                    End If
                                Case 2 'X REPARACION
                                    If pTipoOrigen = "1" Then
                                        CmdGlobal2.CommandText = "SELECT * FROM TBINV_AVERIA WHERE SALIDA_NRO_ALM ='" & pCodigoSalida & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='3' AND AVERIA_ESTADO_2='1' AND AVERIA_SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar) & " AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                        Rs2X = CmdGlobal2.ExecuteReader
                                        If Rs2X.HasRows Then
                                            While Rs2X.Read
                                                CmdGlobal3.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='4', AVERIA_ESTADO_2='2', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_NRO_ALM ='" & pCodigoSalida & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='3' AND AVERIA_ESTADO_2='1' AND AVERIA_SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar) & " AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End While
                                        End If
                                        Rs2X.Close()
                                    End If
                                Case 3 'devolucion por prestamo
                                    'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                    If pTipoOrigen = "1" Then 'O.D.
                                        CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '3',PREDET_SYS_DEVOLUCION = '" & ValorSys & "' FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                               & " AND (B.PREDET_ESTADO_PRESTAMO = '2') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & pCodAlmacenD & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & pCodAlmacenO & ") AND (B.SERIE_NUMERAR = " & Nu(Rs!Serie_Numerar) & ")"
                                        CmdGlobal2.ExecuteNonQuery()
                                    Else 'O.S.
                                        CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '3',PREDET_SYS_DEVOLUCION = '" & ValorSys & "' FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                               & " AND (B.PREDET_ESTADO_PRESTAMO = '2') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & pCodAlmacenD & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & pCodSeccionO & ") AND (B.SERIE_NUMERAR = " & Nu(Rs!Serie_Numerar) & ")"
                                        CmdGlobal2.ExecuteNonQuery()
                                    End If
                                Case 12
                                    If pTipoOrigen = "2" Then
                                        CmdGlobal2.CommandText = "SELECT * FROM TBINV_REEMPLAZOS WHERE NRO_SALIDA_CC='" & pCodigoSalida & "' AND REEM_ESTADO_1='3' AND REEM_ESTADO_2 ='1' AND SERIE_NUMERAR_REEMPLAZANTE = '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                        Rs1 = CmdGlobal2.ExecuteReader
                                        If Rs1.HasRows Then
                                            While Rs1.Read
                                                CmdGlobal3.CommandText = "UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='4' , REEM_ESTADO_2 ='2' , REEM_SYS_MOD='" & ValorSys & "' WHERE NRO_SALIDA_CC='" & pCodigoSalida & "' AND REEM_ESTADO_1='3' AND REEM_ESTADO_2 ='1' AND SERIE_NUMERAR_REEMPLAZANTE = '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End While
                                        End If
                                        Rs1.Close()
                                    End If
                                Case 13 'DEVOLUCION REEMPLAZO X AVERIA
                                    If pTipoOrigen = "2" Then
                                        CmdGlobal2.CommandText = "SELECT * FROM TBINV_AVERIA WHERE SALIDA_NRO='" & pCodigoSalida & "' AND AVERIA_ESTADO_1='1' AND AVERIA_ESTADO_2 ='1' AND AVERIA_SERIE_NUMERAR = '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                        Rs1 = CmdGlobal2.ExecuteReader
                                        If Rs1.HasRows Then
                                            While Rs1.Read
                                                CmdGlobal3.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='2' , AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_NRO='" & pCodigoSalida & "' AND AVERIA_ESTADO_1='1' AND AVERIA_ESTADO_2 ='1' AND AVERIA_SERIE_NUMERAR= '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End While
                                        End If
                                        Rs1.Close()
                                        CmdGlobal2.CommandText = "SELECT * FROM TBINV_REEMPLAZOS WHERE NRO_SALIDA_CC='" & pCodigoSalida & "' AND REEM_ESTADO_1='3' AND REEM_ESTADO_2 ='1' AND SERIE_NUMERAR_REEMPLAZADO = '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                        Rs1 = CmdGlobal2.ExecuteReader
                                        If Rs1.HasRows Then
                                            While Rs1.Read
                                                CmdGlobal3.CommandText = "UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='4' , REEM_ESTADO_2 ='2' , REEM_SYS_MOD='" & ValorSys & "' WHERE NRO_SALIDA_CC='" & pCodigoSalida & "' AND REEM_ESTADO_1='3' AND REEM_ESTADO_2 ='1' AND SERIE_NUMERAR_REEMPLAZADO = '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End While
                                        End If
                                        Rs1.Close()
                                    End If
                                Case 17 'Ingreso por averia que viene de soto
                                    If pTipoOrigen = "1" Then
                                        CmdGlobal2.CommandText = "SELECT * FROM TBINV_AVERIA WHERE SALIDA_NRO_ALM ='" & pCodigoSalida & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='3' AND AVERIA_ESTADO_2='1' AND AVERIA_SERIE_NUMERAR='" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                        Rs1 = CmdGlobal2.ExecuteReader
                                        If Rs1.HasRows Then
                                            While Rs1.Read
                                                CmdGlobal3.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='4', AVERIA_ESTADO_2='2', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_NRO_ALM ='" & pCodigoSalida & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='3' AND AVERIA_ESTADO_2='1' AND AVERIA_SERIE_NUMERAR='" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End While
                                        End If
                                        Rs1.Close()
                                    ElseIf pTipoOrigen = "2" Then
                                        CmdGlobal2.CommandText = "SELECT * FROM TBINV_AVERIA WHERE SALIDA_NRO ='" & pCodigoSalida & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='1' AND AVERIA_ESTADO_2='1' AND AVERIA_SERIE_NUMERAR='" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                        Rs1 = CmdGlobal2.ExecuteReader
                                        If Rs1.HasRows Then
                                            While Rs1.Read
                                                CmdGlobal3.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='2', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_NRO ='" & pCodigoSalida & "' AND AVERIA_SYS_EST ='0' AND AVERIA_ESTADO_1='1' AND AVERIA_ESTADO_2='1' AND AVERIA_SERIE_NUMERAR='" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                CmdGlobal3.ExecuteNonQuery()
                                            End While
                                        End If
                                        Rs1.Close()
                                    End If
                                Case 18 'devolucion x reparacion
                                    If pTipoOrigen = "1" Then
                                        CmdGlobal2.CommandText = "UPDATE TBINV_AVERIA SET AVERIA_ESTADO_1='7', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_ASOTO='" & pCodigoSalida & "' AND AVERIA_ESTADO_1='6' AND AVERIA_ESTADO_2='5' OR AVERIA_ESTADO_2='6' AND AVERIA_SYS_EST ='0' AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND AVERIA_SERIE_NUMERAR = '" & Nu(Rs!Serie_Numerar) & "'"
                                        CmdGlobal2.ExecuteNonQuery()
                                    End If
                            End Select
                        End While
                    End If
                    Rs.Close()
                    '::::::::::::::::::::::::::::::::: ARTICULOS Q NO USAN SERIE
                    If pTipoOrigen = "1" Then
                        CmdGlobal.CommandText = " SELECT DD.DESPD_ITEM,A.ART_CODIGO, DD.DESPD_CANT_DESP AS CANT_XRECIBIR, DD.DESPD_CANT_REC, DD.DESPD_CANT_FALT_REC " _
                                              & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE DD INNER JOIN TBINV_ARTICULOS A ON DD.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND DD.ARTICULO_CODIGO = A.ART_CODIGO " _
                                              & " WHERE (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DD.DESP_CODIGO =" & pCodigoSalida & ") AND (DD.DESPD_SYS_EST = '0') AND (ISNULL(DD.DESPD_CANT_DESP,0) > 0) AND (ISNULL(DD.DESPD_CANT_FALT_REC,0) > 0) " _
                                              & " ORDER BY DD.DESPD_ITEM"
                    Else
                        CmdGlobal.CommandText = " SELECT SD.OSALD_ORDEN, A.ART_CODIGO,SD.OSALD_CANT_ENV AS CANT_XRECIBIR, SD.OSALD_CANT_REC, SD.OSALD_CANT_FALT_REC " _
                                              & " FROM TBINV_CCOSTO_SALIDA_DET_SINSERIE SD INNER JOIN TBINV_ARTICULOS A ON SD.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND SD.ARTICULO_CODIGO = A.ART_CODIGO " _
                                              & " WHERE (SD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (SD.OSAL_CODIGO =" & pCodigoSalida & ") AND (SD.OSALD_SYS_EST = '0') AND (ISNULL(SD.OSALD_CANT_ENV,0) > 0) AND (ISNULL(SD.OSALD_CANT_FALT_REC,0) > 0) " _
                                              & " ORDER BY SD.OSALD_ORDEN"
                    End If
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            item = item + 1
                            CmdGlobal3.CommandText = " SELECT * FROM TBINV_MOV_ALMACEN_ARTICULOS WHERE (MOVAL_CODIGO =" & lblCodMov & ") AND (ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO) & ") AND (EMPRESA_CODIGO='" & psCodEmpresa & "') AND (MOVALA_SYS_EST='0')"
                            Rs2X = CmdGlobal3.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    CmdGlobal4.CommandText = " UPDATE TBINV_MOV_ALMACEN_ARTICULOS SET MOVALA_ART_CANTIDAD=" & Nz(Rs2X!MOVALA_ART_CANTIDAD) + CDbl(Nz(Rs!CANT_XRECIBIR)) & " WHERE (MOVAL_CODIGO =" & lblCodMov & ") AND (ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO) & ") AND (EMPRESA_CODIGO='" & psCodEmpresa & "') AND (MOVALA_SYS_EST='0')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal4.CommandText = " INSERT INTO TBINV_MOV_ALMACEN_ARTICULOS(MOVAL_CODIGO, ARTICULO_CODIGO,MOVALA_ART_CANTIDAD, MOVALA_ART_ORDEN,EMPRESA_CODIGO,MOVALA_SYS_EST) " _
                                                       & " VALUES(" & lblCodMov & "," & Nu(Rs!ART_CODIGO) & "," & CDbl(Nz(Rs!CANT_XRECIBIR)) & "," & item & ",'" & psCodEmpresa & "','0')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs2X.Close()
                            If pTipoOrigen = "1" Then
                                lblCodOrigen = pCodAlmacenO
                            ElseIf pTipoOrigen = "2" Then
                                lblCodOrigen = pCodSeccionO
                            End If
                            CmdGlobal3.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & pCodAlmacenD & ") AND (UBICACT_TIPO='1')" _
                                                   & " AND (ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            Rs2X = CmdGlobal3.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    StockAc = Nz(Rs2X!SAA_STOCK_ACTUAL)
                                    If lblCodTipoTrans = "0" Then  'INGRESO
                                        StockAc = StockAc + CDbl(Nz(Rs!CANT_XRECIBIR))
                                    Else 'SALIDA
                                        StockAc = StockAc - CDbl(Nz(Rs!CANT_XRECIBIR))
                                    End If
                                    CmdGlobal4.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & pCodAlmacenD & ") AND (UBICACT_TIPO='1')" _
                                                           & " AND (ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobal4.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                       & " VALUES('1'," & pCodAlmacenD & "," & Nu(Rs!ART_CODIGO) & "," & CDbl(Nz(Rs!CANT_XRECIBIR)) & ",'0','" & psCodEmpresa & "')"
                                CmdGlobal4.ExecuteNonQuery()
                            End If
                            Rs2X.Close()

                            'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL=========================================================================
                            CmdGlobal3.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                            Rs2X = CmdGlobal3.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    CmdGlobal4.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET MOV_ESTADO ='3' WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                    CmdGlobal4.ExecuteNonQuery()
                                End While
                            End If
                            Rs2X.Close()

                            CmdGlobal3.CommandText = " SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            Rs2X = CmdGlobal3.ExecuteReader
                            If Rs2X.HasRows Then
                                While Rs2X.Read
                                    lblNroMovimiento = Nz(Rs2X(0)) + 1
                                End While
                            Else
                                lblNroMovimiento = "00000001"
                            End If
                            Rs2X.Close()
                            '1: INGRESO, 2:SALIDA
                            Call Movimiento_Kardex(psConexion, psCodEmpresa, pCodigoSalida, pCodMotivoSalida, Nu(Rs!ART_CODIGO), pTipoDestino, pCodAlmacenD, pTipoOrigen, lblCodOrigen, "", "1", psFechaTrans, CDbl(Nz(Rs!CANT_XRECIBIR)))
                            CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                                   & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                   & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & pTipoDestino & "','" & pCodAlmacenD & "','" & pTipoOrigen & "','" & lblCodOrigen & "', " _
                                                   & " '" & pCodigoSalida & "','" & Nu(Rs!ART_CODIGO) & "','" & CDbl(Nz(Rs!CANT_XRECIBIR)) & "','" & ValorSys & "','3','" & pCodMotivoSalida & "','" & FechaServer & "','0')"
                            CmdGlobal3.ExecuteNonQuery()
                            If pCodMotivoSalida = "5" Then 'TRANSFERENCIA <- falta chequear la tabla
                                '              Sql = "SELECT MAX(INGXT_NUMERAR) FROM TBINV_MOV_ALMACEN_INGXTRANS WHERE EMPRESA_CODIGO='" & SistCodEmpresa & "'"
                                '              Rs2X.Open Sql, Cn, adOpenKeyset, adLockOptimistic
                                '              If Rs2X.RecordCount > 0 Then lblIngresoNumerar = Nz(RS(0)) + 1 Else lblIngresoNumerar = "1"
                                '              Rs2X.Close
                                '              CmdGlobal.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_INGXTRANS(EMPRESA_CODIGO,INGXT_NUMERAR,INGRESO_TIPO,INGRESO_CODIGO," _
                                '                                    & "SERIE_NUMERAR, CANT_INGRESA,ALMACEN_MOV_CODIGO,USUARIO,FECHA,HORA) " _
                                '                                    & "VALUES('" & SistCodEmpresa & "'," & lblIngresoNumerar & "," & pTipoOrigen & ",'" & pCodigoSalida & "'," _
                                '                                    & Nu(RS!SERIE_NUMERAR) & ",1," & lblCodMov & ",'" & User & "','" & FechaServer & "','" & HoraServer & "')"
                                '              CmdGlobal.Execute
                            End If
                            If pCodMotivoSalida = "2" Then 'MANTENIMIENTO: TIPO(0 INGRESO,1 SALIDA); TIPO_DOC(R ORECEPCION, D ODESPACHO, A OSALIDA); TIPO_ORIGEN(0 SIN ORIGEN,1 ALMACEN, 2 CCOSTO); MANT_ESTADO(1 POR REVISAR,2 REVISADO)
                                'SHEILA
                                '              Sql = "SELECT MAX(MANT_CODIGO) FROM TBINV_MOV_ALMACEN_MANTENIMIENTO_" & SistCodEmpresa & " "
                                '              Rs2X.Open Sql, Cn, adOpenKeyset, adLockOptimistic
                                '              If Rs2X.RecordCount > 0 Then lblIngresoNumerar = Nz(RS(0)) + 1 Else lblIngresoNumerar = "1"
                                '              Rs2X.Close
                                '              CmdGlobal.CommandText = "INSERT INTO TBINV_MOV_ALMACEN_MANTENIMIENTO_" & SistCodEmpresa & " (MANT_CODIGO, MANT_TIPO, MANT_DOCUME_TIPO, MANT_DOCUME_CODIGO, ALMACEN_DESTINO, MANT_ORIGEN_TIPO," _
                                '                                    & "MANT_ORIGEN_CODIGO, SERIE_NUMERAR,MANT_FECHA_ING, MANT_HORA_ING, MANT_USUARIO_ING, MANT_SYS_EST,MANT_ESTADO) VALUES(" _
                                '                                    & lblIngresoNumerar & ",'0','" & IIf(pTipoOrigen = "1", "D", "S") & "'," & pCodigoSalida & "," & pCodAlmacenD & ",'" & pTipoOrigen & "'," _
                                '                                    & IIf(pTipoOrigen = "1", pCodAlmacenO, pCodSeccionO) & "," & Nu(RS!SERIE_NUMERAR) & ",'" & FechaServer & "','" & HoraServer & "','" & User & "','0','1')"
                                '              CmdGlobal.Execute
                            End If
                            If pTipoOrigen = "1" Then
                                CmdGlobal3.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO_DET_SINSERIE SET DESPD_CANT_REC = DESPD_CANT_DESP, DESPD_CANT_FALT_REC = 0, DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_CODIGO=" & pCodigoSalida & " AND ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO)
                                CmdGlobal3.ExecuteNonQuery()
                            Else
                                CmdGlobal3.CommandText = " UPDATE TBINV_CCOSTO_SALIDA_DET_SINSERIE SET OSALD_CANT_REC = OSALD_CANT_ENV, OSALD_CANT_FALT_REC = 0, OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO=" & pCodigoSalida & " AND ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO)
                                CmdGlobal3.ExecuteNonQuery()
                            End If
                            '---------------------------------------------------------
                            Select Case EstadoSalida
                                Case 1 'prestamo
                                    'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 por devolver parcial, 5 devuelto parcial
                                    If pTipoOrigen = "1" Then  'O.D.
                                        CmdGlobal3.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_CANT_PRESTADA = PREDET_CANTXPRESTAR, PREDET_CANT_XDEVOLVER = 0, PREDET_CANT_FALT_DEVOLVER = PREDET_CANTXPRESTAR, PREDET_CANT_DEVUELTA = 0, PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE_SINSERIE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.DESP_CODIGO=" & pCodigoSalida & " AND A.ARTICULO_CODIGO=" & Nu(Rs!ART_CODIGO)
                                        CmdGlobal3.ExecuteNonQuery()
                                    Else 'O.S.
                                        CmdGlobal3.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_CANT_PRESTADA = PREDET_CANTXPRESTAR, PREDET_CANT_XDEVOLVER = 0, PREDET_CANT_FALT_DEVOLVER = PREDET_CANTXPRESTAR, PREDET_CANT_DEVUELTA = 0, PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE_SINSERIE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.OSAL_CODIGO=" & pCodigoSalida & " AND A.ARTICULO_CODIGO=" & Nu(Rs!ART_CODIGO)
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                Case 3 'devolucion por prestamo
                                    'estado envio: 0 enviado ,1 recibido; estado prestamo: 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 por devolver parcial, 5 devuelto parcial
                                    'colocar el prestamo Devuelto(3) si la cant falta devolver =  cant q se recibe (cant x devolver)
                                    'colocar el prestamo Devuelto Parcial(5) si la cant falta devolver <> cant q se recibe (cant x devolver)
                                    If pTipoOrigen = "1" Then 'O.D.
                                        CmdGlobal3.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_CANT_FALT_DEVOLVER WHEN " & CDbl(Nz(Rs!CANT_XRECIBIR)) & " THEN '3' ELSE '5' END)," _
                                                               & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) - " & CDbl(Nz(Rs!CANT_XRECIBIR)) & ",PREDET_CANT_FALT_DEVOLVER = ISNULL(PREDET_CANT_FALT_DEVOLVER,0) - " & CDbl(Nz(Rs!CANT_XRECIBIR)) & "," _
                                                               & " PREDET_CANT_DEVUELTA = ISNULL(PREDET_CANT_DEVUELTA,0) + " & CDbl(Nz(Rs!CANT_XRECIBIR)) & " , PREDET_SYS_DEVOLUCION = '" & ValorSys & "' FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE_SINSERIE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                               & " AND (B.PREDET_ESTADO_PRESTAMO IN ('2','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & pCodAlmacenD & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & pCodAlmacenO & ") AND (B.ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO) & ")"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Else 'O.S.
                                        CmdGlobal3.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_CANT_FALT_DEVOLVER WHEN " & CDbl(Nz(Rs!CANT_XRECIBIR)) & " THEN '3' ELSE '5' END)," _
                                                               & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) - " & CDbl(Nz(Rs!CANT_XRECIBIR)) & ",PREDET_CANT_FALT_DEVOLVER = ISNULL(PREDET_CANT_FALT_DEVOLVER,0) - " & CDbl(Nz(Rs!CANT_XRECIBIR)) & "," _
                                                               & " PREDET_CANT_DEVUELTA = ISNULL(PREDET_CANT_DEVUELTA,0) + " & CDbl(Nz(Rs!CANT_XRECIBIR)) & " , PREDET_SYS_DEVOLUCION = '" & ValorSys & "' FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE_SINSERIE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                               & " AND (B.PREDET_ESTADO_PRESTAMO IN ('2','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '1') AND (A.ALMACEN_CODIGO_ORIGEN =" & pCodAlmacenD & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & pCodSeccionO & ") AND (B.ARTICULO_CODIGO = " & Nu(Rs!ART_CODIGO) & ")"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                            End Select
                        End While
                    End If
                    Rs.Close()
                    '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q USA SERIE
                    If pTipoOrigen = "1" Then
                        CmdGlobal.CommandText = " SELECT SUM(CASE WHEN RECIBIDA_OK='N' THEN 1 ELSE 0 END) AS CFALT, SUM(CASE WHEN RECIBIDA_OK='S' THEN 1 ELSE 0 END) AS CREC,COUNT(RECIBIDA_OK) AS CAREC " _
                                              & " FROM TBINV_ALMACEN_DESPACHO_DET WHERE (DESP_CODIGO =" & pCodigoSalida & ") AND (DESPD_OK='S') AND (DESPD_SYS_EST='0')"
                    Else
                        CmdGlobal.CommandText = " SELECT SUM(CASE WHEN RECIBIDA_OK='N' THEN 1 ELSE 0 END) AS CFALT, SUM(CASE WHEN RECIBIDA_OK='S' THEN 1 ELSE 0 END) AS CREC,COUNT(RECIBIDA_OK) AS CAREC " _
                                              & " FROM TBINV_CCOSTO_SALIDA_DET WHERE (OSAL_CODIGO =" & pCodigoSalida & ") AND (ENVIADA_OK='S') AND (OSALD_SYS_EST='0')"
                    End If
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            QARec = Nz(Rs!CAREC)
                            QRec = Nz(Rs!CREC)
                            QFaltRec = Nz(Rs!CFALT)
                        End While
                    End If
                    Rs.Close()
                    '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q NO USA SERIE
                    If pTipoOrigen = "1" Then
                        CmdGlobal.CommandText = " SELECT SUM(DESPD_CANT_FALT_REC) AS CFALT, SUM(DESPD_CANT_REC) AS CREC, SUM(DESPD_CANT_DESP) AS CAREC " _
                                              & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE WHERE (DESP_CODIGO =" & pCodigoSalida & ") AND (DESPD_SYS_EST='0')"
                    Else
                        CmdGlobal.CommandText = " SELECT SUM(OSALD_CANT_FALT_REC) AS CFALT, SUM(OSALD_CANT_REC) AS CREC, SUM(OSALD_CANT_ENV) AS CAREC " _
                                              & " FROM TBINV_CCOSTO_SALIDA_DET_SINSERIE WHERE (OSAL_CODIGO =" & pCodigoSalida & ") AND (OSALD_SYS_EST='0')"
                    End If
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            QARec = QARec + Nz(Rs!CAREC)
                            QRec = QRec + Nz(Rs!CREC)
                            QFaltRec = QFaltRec + Nz(Rs!CFALT)
                        End While
                    End If
                    Rs.Close()
                    If QARec = QRec And QFaltRec = 0 Then EstadoDesp = "3" Else EstadoDesp = "4"
                    If pTipoOrigen = "1" Then
                        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='" & EstadoDesp & "',DESP_CANT_REC=" & QRec & ",DESP_CANT_FALT_REC=" & QFaltRec & " WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND  DESP_CODIGO=" & pCodigoSalida
                    Else
                        CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='" & EstadoDesp & "',OSAL_CANT_REC=" & QRec & ",OSAL_CANT_FALT_REC=" & QFaltRec & " WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND  OSAL_CODIGO=" & pCodigoSalida
                    End If
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = " DELETE FROM TBINV_MOV_ALMACEN_REFERENCIA WHERE MOVAL_CODIGO=" & lblCodMov & " AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                    CmdGlobal.ExecuteNonQuery()
                    If lblCodTransD <> "" Then
                        CmdGlobal.CommandText = " INSERT INTO TBINV_MOV_ALMACEN_REFERENCIA(MOVAL_CODIGO, TRANS_CODIGO, TRANS_REF_CODIGO,MOVALREF_VALOR,EMPRESA_CODIGO,MOVALREF_SYS_EST) " _
                                              & " VALUES(" & lblCodMov & "," & lblCodTrans & "," & lblCodTransD & ",'" & IIf(pTipoOrigen = "1", "Sal.Almaceén ", "Sal.Seción CC") & pCodigoSalida & "','" & psCodEmpresa & "','0')"
                        CmdGlobal.ExecuteNonQuery()
                    End If

                Case "2" 'destino seccion cc
                    lblCodTipoTrans = "0" 'ingreso
                    Select Case pTipoOrigen
                        Case "1" 'almacen
                            'DESPD_TIPO_RECIBIDA: A = AUTOMATICA, M/NULL = MANUAL
                            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_CODIGO=" & pCodigoSalida & ""
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO_DET_SINSERIE SET DESPD_CANT_REC = DESPD_CANT_DESP, DESPD_CANT_FALT_REC = 0, DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_CODIGO=" & pCodigoSalida & ""
                            CmdGlobal.ExecuteNonQuery()
                            '::::::::::::::::::::::::::::::::::::::::::::::::::::: ARTICULOS Q USAN SERIE
                            CmdGlobal.CommandText = " SELECT DD.DESPD_ITEM,DD.DESPD_FUNCION, DD.SERIE_NUMERAR, DD.DESPD_OK,RECIBIDA_OK,S.ARTICULO_CODIGO, S.SERIE_NRO,S.SERIE_CARACTERISTICAS,S.PLACA_NRO, A.ART_DESCRIPCION,DD.DESPD_FUNCION AS COD_FUNCION " _
                                                  & " FROM TBINV_ALMACEN_DESPACHO_DET DD INNER JOIN TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR INNER JOIN" _
                                                  & " TBINV_ARTICULOS A ON DD.EMPRESA_CODIGO = A.EMPRESA_CODIGO AND S.ARTICULO_CODIGO = A.ART_CODIGO " _
                                                  & " WHERE (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DD.DESP_CODIGO =" & pCodigoSalida & ") AND (DD.DESPD_SYS_EST = '0') AND (DD.DESPD_OK='S') AND (RECIBIDA_OK='S')" _
                                                  & " ORDER BY DD.DESPD_ITEM"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & pCodSeccionD & ") AND (UBICACT_TIPO='2')" _
                                                           & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        While Rs2X.Read
                                            StockAc = Nz(Rs2X!SAA_STOCK_ACTUAL)
                                            If lblCodTipoTrans = "0" Then  'INGRESO
                                                StockAc = StockAc + 1
                                            Else 'SALIDA
                                                StockAc = StockAc - 1
                                            End If
                                            CmdGlobal3.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & pCodSeccionD & ") AND (UBICACT_TIPO='2') " _
                                                                   & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    Else
                                        CmdGlobal3.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                               & " VALUES('2'," & pCodSeccionD & "," & Nu(Rs!ARTICULO_CODIGO) & ",1,'0','" & psCodEmpresa & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    Rs2X.Close()
                                    'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL=========================================================================
                                    CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        While Rs2X.Read
                                            CmdGlobal3.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET MOV_ESTADO ='3' WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    End If
                                    Rs2X.Close()

                                    CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & lblNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' "
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        While Rs2X.Read
                                            CmdGlobal3.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET NRO_ARTICULO =" & Nz(Rs2X!NRO_ARTICULO) + 1 & " WHERE (CODIGO_ARTICULO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (MOV_NRO='" & lblNroMovimiento & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    Else
                                        CmdGlobal3.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                        Rs1 = CmdGlobal3.ExecuteReader
                                        If Rs1.HasRows Then
                                            While Rs1.Read
                                                lblNroMovimiento = Nz(Rs1(0)) + 1
                                            End While
                                        Else
                                            lblNroMovimiento = "00000001"
                                        End If
                                        Rs1.Close()
                                        '1: INGRESO, 2:SALIDA
                                        Call Movimiento_Kardex(psConexion, psCodEmpresa, pCodigoSalida, pCodMotivoSalida, Nu(Rs!ARTICULO_CODIGO), pTipoDestino, pCodSeccionD, pTipoOrigen, pCodAlmacenO, "", "1", psFechaTrans, 1)
                                        CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                                               & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                               & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & pTipoDestino & "','" & pCodSeccionD & "','" & pTipoOrigen & "','" & pCodAlmacenO & "', " _
                                                               & " '" & pCodigoSalida & "','" & Nu(Rs!ARTICULO_CODIGO) & "','1','" & ValorSys & "','3','" & pCodMotivoSalida & "','" & FechaServer & "','0')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    Rs2X.Close()

                                    CmdGlobal2.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='2',UBICACT_CODIGO=" & pCodSeccionD & ",UBICACT_SYS='" & ValorSys & "',SERIE_FUNCION = '" & Nu(Rs!DESPD_FUNCION) & "' WHERE SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                    CmdGlobal2.ExecuteNonQuery()
                                    'ESTADO: 0 primera vez, 1 EN TRANSITO,2 OK
                                    CmdGlobal2.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,SERIE_FUNCION,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO) " _
                                                           & " VALUES(" & Nu(Rs!Serie_Numerar) & ",'2'," & pCodSeccionD & ",'2','0','" & ValorSys & "','" & Nu(Rs!DESPD_FUNCION) & "','" & FechaServer & "','1','" & pCodigoSalida & "','" & pCodMotivoSalida & "' )"
                                    CmdGlobal2.ExecuteNonQuery()
                                    '---------------------------------------------------------seguimiento stock
                                    '                If StockAc < 0 Then MsgBox "Stock Negativo", vbExclamation
                                    Dim Log As String = ""
                                    CmdGlobal2.CommandText = " SELECT MAX(LOGSTOCK_NRO) FROM TBINV_LOG_STOCK"
                                    Rs1 = CmdGlobal2.ExecuteReader
                                    If Rs1.HasRows Then
                                        While Rs1.Read
                                            Log = Nz(Rs1(0)) + 1
                                        End While
                                    Else
                                        Log = 1
                                    End If
                                    Rs1.Close()
                                    CmdGlobal2.CommandText = " SELECT S.ARTICULO_CODIGO, A.ART_DESCRIPCION, COUNT(S.ARTICULO_CODIGO) AS CANT, S.UBICACT_TIPO, S.UBICACT_CODIGO " _
                                                           & " FROM  dbo.TBINV_ARTICULOS A INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON A.ART_CODIGO = S.ARTICULO_CODIGO " _
                                                           & " Where (s.UBICACT_TIPO ='2') And (s.SERIE_ESTADO <> 1) and  (S.SERIE_SYS_EST = '0') AND (A.ART_SYS_EST = '0') AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "')" _
                                                           & " AND S.UBICACT_CODIGO=" & pCodSeccionD & " AND S.ARTICULO_CODIGO=" & Nu(Rs!ARTICULO_CODIGO) & ""
                                    CmdGlobal2.CommandText = CmdGlobal2.CommandText & " GROUP BY S.ARTICULO_CODIGO, A.ART_DESCRIPCION, S.ARTICULO_CODIGO, S.UBICACT_TIPO, S.UBICACT_CODIGO"
                                    Rs1 = CmdGlobal2.ExecuteReader
                                    If Rs1.HasRows Then
                                        While Rs1.Read
                                            CmdGlobal3.CommandText = " INSERT INTO TBINV_LOG_STOCK (EMPRESA_CODIGO,LOGSTOCK_NRO, LOGSTOCK_SALIDA, LOGSTOCK_TIPOORIGEN, LOGSTOCK_ORIGEN_CODIGO, LOGSTOCK_TIPODESTINO,LOGSTOCK_DESTINO_CODIGO, LOGSTOCK_ARTICULO_CODIGO, LOGSTOCK_MOTIVO, LOGSTOCK_FECHA, LOGSTOCK_HORA, LOGSTOCK_SYS_CRE, LOGSTOCK_STOCK,LOGSTOCK_MODO,LOGSTOCK_STOCKREAL) " _
                                                                   & " VALUES ('" & psCodEmpresa & "'," & Log & "," & pCodigoSalida & ",'1'," & pCodAlmacenO & ",'2'," & pCodSeccionD & "," & Nu(Rs!ARTICULO_CODIGO) & ",'" & pCodMotivoSalida & "','" & FechaServer & "','" & HoraServer & "','" & ValorSys & "'," & StockAc & ",'A'," & Nu(Rs1!cant) & ")"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    End If
                                    Rs1.Close()
                                    '---------------------------------------------------------
                                    Select Case pCodMotivoSalida
                                        Case 1 'prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                            CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' " _
                                                                & " FROM TBINV_PRESTAMO_DETALLE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.DESP_CODIGO=" & pCodigoSalida & " AND SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                            CmdGlobal2.ExecuteNonQuery()
                                        Case 3 'devolucion por prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                            CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '3',PREDET_SYS_DEVOLUCION = '" & ValorSys & "' " _
                                                                   & " FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B " _
                                                                   & " WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                                   & " AND (B.PREDET_ESTADO_PRESTAMO = '2') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & pCodSeccionD & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & pCodAlmacenO & ") AND (B.SERIE_NUMERAR = " & Nu(Rs!Serie_Numerar) & ")"
                                            CmdGlobal2.ExecuteNonQuery()
                                        Case 6 'REEMPLAZO X CAMBIO
                                            CmdGlobal2.CommandText = " SELECT * FROM TBINV_REEMPLAZOS WHERE REEM_SYS_EST ='0' AND NRO_SALIDA_ALM='" & pCodigoSalida & "' AND SERIE_NUMERAR_REEMPLAZANTE='" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                            Rs1 = CmdGlobal2.ExecuteReader
                                            If Rs1.HasRows Then
                                                While Rs1.Read
                                                    CmdGlobal3.CommandText = " UPDATE TBINV_REEMPLAZOS SET REEM_ESTADO_1='2', REEM_SYS_MOD='" & ValorSys & "'" _
                                                                           & " WHERE REEM_SYS_EST ='0' AND NRO_SALIDA_ALM='" & pCodigoSalida & "' AND SERIE_NUMERAR_REEMPLAZANTE='" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                    CmdGlobal3.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs1.Close()
                                        Case 11 'REEMPLAZO X AVERIA
                                            Dim lblAveria As Integer
                                            CmdGlobal2.CommandText = "SELECT MAX(AVERIA_NRO) FROM TBINV_AVERIA "
                                            Rs1 = CmdGlobal2.ExecuteReader
                                            If Rs1.HasRows Then
                                                While Rs1.Read
                                                    lblAveria = Nz(Rs1(0)) + 1
                                                End While
                                            Else
                                                lblAveria = 1
                                            End If
                                            Rs1.Close()
                                            CmdGlobal2.CommandText = " SELECT * FROM TBINV_REEMPLAZOS WHERE REEM_SYS_EST ='0' AND NRO_SALIDA_ALM='" & pCodigoSalida & "' AND SERIE_NUMERAR_REEMPLAZANTE= '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                            Rs1 = CmdGlobal2.ExecuteReader
                                            If Rs1.HasRows Then
                                                While Rs1.Read
                                                    CmdGlobal3.CommandText = " INSERT INTO TBINV_AVERIA (EMPRESA_CODIGO,AVERIA_NRO,AVERIA_SERIE_NUMERAR,AVERIA_ESTADO_1,AVERIA_ESTADO_2,AVERIA_FECHA, " _
                                                                           & " AVERIA_TIPO_ORIGEN, AVERIA_CODIGO_ORIGEN, AVERIA_TIPO_DESTINO, AVERIA_CODIGO_DESTINO, AVERIA_SYS_CRE, AVERIA_SYS_EST ) " _
                                                                           & " VALUES ('" & psCodEmpresa & "','" & lblAveria & "','" & Nu(Rs1!SERIE_NUMERAR_REEMPLAZADO) & "','0','1','" & FechaServer & "', " _
                                                                           & " '2','" & pCodSeccionD & " ','1','" & pCodAlmacenO & "','" & ValorSys & "','0')"
                                                    CmdGlobal3.ExecuteNonQuery()
                                                    CmdGlobal3.CommandText = " UPDATE TBINV_REEMPLAZOS SET AVERIA_NRO='" & lblAveria & "' ,REEM_ESTADO_1='2', REEM_SYS_MOD='" & ValorSys & "' " _
                                                                           & " WHERE REEM_SYS_EST ='0' AND NRO_SALIDA_ALM='" & pCodigoSalida & "' AND SERIE_NUMERAR_REEMPLAZANTE= '" & Nu(Rs!Serie_Numerar) & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                                    CmdGlobal3.ExecuteNonQuery()
                                                End While
                                            End If
                                            Rs1.Close()
                                        Case Else
                                    End Select
                                End While
                            End If
                            Rs.Close()
                            '::::::::::::::::::::::::::::::::::::::::::::::::::::: ARTICULOS Q NO USAN SERIE
                            CmdGlobal.CommandText = " SELECT * FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_CODIGO=" & pCodigoSalida
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_SINSERIE_CCOSTO WHERE (CECOSE_CODIGO = " & pCodSeccionD & ") " _
                                                           & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        While Rs2X.Read
                                            StockAc = Nz(Rs2X!SKSSCC_STOCK_ACTUAL) + Nu(Rs!DESPD_CANT_DESP)
                                            CmdGlobal3.CommandText = " UPDATE TBINV_STOCK_SINSERIE_CCOSTO SET SKSSCC_STOCK_ACTUAL=" & StockAc & " WHERE (CECOSE_CODIGO = " & pCodSeccionD & ") " _
                                                                   & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    Else
                                        CmdGlobal3.CommandText = " INSERT TBINV_STOCK_SINSERIE_CCOSTO(CECOSE_CODIGO, ARTICULO_CODIGO,SKSSCC_STOCK_ACTUAL,SKSSCC_SYS_EST,EMPRESA_CODIGO) " _
                                                               & " VALUES(" & pCodSeccionD & "," & Nu(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!DESPD_CANT_DESP) & ",'0','" & psCodEmpresa & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    Rs2X.Close()
                                    CmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & pCodSeccionD & ") AND (UBICACT_TIPO='2')" _
                                                           & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        StockAc = Nz(Rs2X!SAA_STOCK_ACTUAL) + CDbl(Nz(Rs!DESPD_CANT_DESP))
                                        CmdGlobal3.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & pCodSeccionD & ") AND (UBICACT_TIPO='2')" _
                                                               & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Else
                                        CmdGlobal3.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                               & " VALUES('2'," & pCodSeccionD & "," & Nu(Rs!ARTICULO_CODIGO) & "," & CDbl(Nz(Rs!DESPD_CANT_DESP)) & ",'0','" & psCodEmpresa & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    Rs2X.Close()

                                    'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL=========================================================================
                                    CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        While Rs2X.Read
                                            CmdGlobal3.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET MOV_ESTADO ='3' WHERE (CODIGO_TRANS='" & pCodigoSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                                            CmdGlobal3.ExecuteNonQuery()
                                        End While
                                    End If
                                    Rs2X.Close()

                                    CmdGlobal2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        While Rs2X.Read
                                            lblNroMovimiento = Nz(Rs2X(0)) + 1
                                        End While
                                    Else
                                        lblNroMovimiento = "00000001"
                                    End If
                                    Rs2X.Close()
                                    '1: INGRESO, 2:SALIDA
                                    Call Movimiento_Kardex(psConexion, psCodEmpresa, pCodigoSalida, pCodMotivoSalida, Nu(Rs!ARTICULO_CODIGO), pTipoDestino, pCodSeccionD, pTipoOrigen, pCodAlmacenO, "", "1", psFechaTrans, CDbl(Nz(Rs!DESPD_CANT_DESP)))
                                    CmdGlobal2.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                                           & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                                           & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & pTipoDestino & "','" & pCodSeccionD & "','" & pTipoOrigen & "','" & pCodAlmacenO & "', " _
                                                           & " '" & pCodigoSalida & "','" & Nu(Rs!ARTICULO_CODIGO) & "','" & CDbl(Nz(Rs!DESPD_CANT_DESP)) & "','" & ValorSys & "','3','" & pCodMotivoSalida & "','" & FechaServer & "','0')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    '---------------------------------------------------------
                                    Select Case pCodMotivoSalida
                                        Case 1 'prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                            CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_CANT_PRESTADA = PREDET_CANTXPRESTAR, PREDET_CANT_XDEVOLVER = 0, PREDET_CANT_FALT_DEVOLVER = PREDET_CANTXPRESTAR, PREDET_CANT_DEVUELTA = 0, PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' " _
                                                                   & " FROM TBINV_PRESTAMO_DETALLE_SINSERIE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.DESP_CODIGO=" & pCodigoSalida & " AND ARTICULO_CODIGO=" & Nu(Rs!ARTICULO_CODIGO)
                                            CmdGlobal2.ExecuteNonQuery()
                                        Case 3 'devolucion por prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                            CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_CANT_FALT_DEVOLVER THEN " & Nz(Rs!DESPD_CANT_DESP) & " THEN '3' ELSE '5' END)," _
                                                                   & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) - " & Nz(Rs!DESPD_CANT_DESP) & ",PREDET_CANT_FALT_DEVOLVER = ISNULL(PREDET_CANT_FALT_DEVOLVER,0) - " & Nz(Rs!DESPD_CANT_DESP) & "," _
                                                                   & " PREDET_CANT_DEVUELTA = ISNULL(PREDET_CANT_DEVUELTA,0) + " & Nz(Rs!DESPD_CANT_DESP) & " , PREDET_SYS_DEVOLUCION = '" & ValorSys & "' " _
                                                                   & " FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE_SINSERIE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                                   & " AND (B.PREDET_ESTADO_PRESTAMO IN ('2','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & pCodSeccionD & ") AND (A.PRESTA_TIPODESTINO = '1') AND (A.ALMACEN_CODIGO_DESTINO = " & pCodAlmacenO & ") AND (B.ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ")"
                                            CmdGlobal2.ExecuteNonQuery()
                                    End Select
                                End While
                            End If
                            Rs.Close()
                            '--------------------------------------------------------------------------------------------------------------
                            'CAMBIAR LOS ESTADOS DE PEDIDOS RELACIONADOS CON LA ORDEN DE DESPACHO
                            'ESTADO ANTERIOR A O.DESPACHO: 11(CON O/C), 12(POR DESPACHAR), 6(ENVIADO A REQUISICION), 3(SIN ATENDER), --->todavia 5(ATENDIDO PARCIAL)
                            'ESTADO ACTUAL: 13(EN TRANSITO X RECIBIR), 14(RECIBIDO PARCIAL)
                            'DE 11,12,6 A ATENDIDO DE UNA O.D.(7); 3 A ATENDIDO(1)
                            CmdGlobal.CommandText = "SELECT POD.PEDIDOD_NUMERAR, COUNT(DD.RECIBIDA_OK) AS CANT_REC, PD.PEDIDOD_ESTADO, PD.PEDIDOD_CANTIDAD, PD.PEDIDOD_CANTIDAD_ATEN,PEDIDOD_EST_ANT_ODESPACHO, PD.PEDIDO_CODIGO " _
                                & " FROM TBLOGIS_PEDIDO_ODESPACHO POD INNER JOIN TBINV_ALMACEN_DESPACHO_DET DD ON POD.EMPRESA_CODIGO = DD.EMPRESA_CODIGO AND  POD.DESPACHO_CODIGO = DD.DESP_CODIGO" _
                                & " INNER JOIN TBLOGIS_PEDIDO_DETALLE PD ON POD.EMPRESA_CODIGO = PD.EMPRESA_CODIGO AND POD.PEDIDOD_NUMERAR = PD.PEDIDOD_NUMERAR" _
                                & " INNER JOIN TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR AND PD.CODIGO_ITEM = S.ARTICULO_CODIGO" _
                                & " WHERE (POD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (POD.DESPACHO_CODIGO = " & pCodigoSalida & ") AND (DD.RECIBIDA_OK = 'S')" _
                                & " GROUP BY POD.PEDIDOD_NUMERAR, PD.PEDIDOD_ESTADO, PD.PEDIDOD_CANTIDAD, PD.PEDIDOD_CANTIDAD_ATEN,PEDIDOD_EST_ANT_ODESPACHO, PD.PEDIDO_CODIGO " _
                                & " HAVING (COUNT(DD.RECIBIDA_OK) > 0) AND (PD.PEDIDOD_ESTADO = '13' OR PD.PEDIDOD_ESTADO = '14')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    If CDbl(Nz(Rs!PEDIDOD_CANTIDAD_ATEN)) < CDbl(Nz(Rs!PEDIDOD_CANTIDAD)) And
                                       CDbl(Nz(Rs!CANT_REC)) <= CDbl(Nz(Rs!PEDIDOD_CANTIDAD)) Then
                                        EstPedido = ""
                                        If CDbl(Nz(Rs!CANT_REC)) < CDbl(Nz(Rs!PEDIDOD_CANTIDAD)) Then 'cuando se recibe parcialmente
                                            EstPedido = "14"
                                            AtencionOk = "N"
                                        ElseIf CDbl(Nz(Rs!CANT_REC)) = CDbl(Nz(Rs!PEDIDOD_CANTIDAD)) Then 'cuando se recibe todo
                                            'If Nu(Rs!PEDIDOD_EST_ANT_ODESPACHO) = "11" Or Nu(Rs!PEDIDOD_EST_ANT_ODESPACHO) = "12" Or Nu(Rs!PEDIDOD_EST_ANT_ODESPACHO) = "6" Then
                                            EstPedido = "7"
                                            'Else
                                            '   EstPedido = "1"
                                            'End If
                                            AtencionOk = "S"
                                        End If
                                        If EstPedido <> "" Then
                                            CmdGlobal2.CommandText = "UPDATE TBLOGIS_PEDIDO_DETALLE SET PEDIDOD_ESTADO='" & EstPedido & "'," _
                                                                  & "PEDIDOD_FECHA_ATENDIDA='" & FechaServer & "',PEDIDOD_CANTIDAD_ATEN=" & CDbl(Nz(Rs!CANT_REC)) & ",PEDIDOD_USUARIO_ATENDIO='" & psUser & "',PEDIDOD_HORA_ATENDIDA='" & HoraServer & "',PEDIDOD_ATENCION_OK='" & AtencionOk & "' " _
                                                                  & "WHERE PEDIDOD_NUMERAR=" & Nu(Rs!PEDIDOD_NUMERAR) & " AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                                            CmdGlobal2.ExecuteNonQuery()
                                            'en caso que PEDIDOD_NUMERAR proviene de item detalle original, tambien actualizar
                                            CmdGlobal2.CommandText = "UPDATE TBLOGIS_PEDIDO_DETALLE SET PEDIDOD_ATENCION_OK='" & AtencionOk & "' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND PEDIDOD_NUMERAR_INSTANCIA=" & Nu(Rs!PEDIDOD_NUMERAR) & " "
                                            CmdGlobal2.ExecuteNonQuery()
                                            'saber si c/item detalle ya tiene atencion terminada para poder cerrar el pedido general
                                            CmdGlobal2.CommandText = "SELECT * FROM TBLOGIS_PEDIDO_DETALLE WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND PEDIDOD_SYS_EST='0' AND PEDIDO_CODIGO=" & Nu(Rs!PEDIDO_CODIGO) & " AND ISNULL(PEDIDOD_ATENCION_OK,'N')='N'"
                                            Rs2X = CmdGlobal2.ExecuteReader
                                            If Rs2X.HasRows Then
                                            Else
                                                'estado 0=cerrado, 1=abierto
                                                CmdGlobal3.CommandText = "UPDATE TBLOGIS_PEDIDO SET PEDIDO_ESTADO='0' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND PEDIDO_CODIGO=" & Nu(Rs!PEDIDO_CODIGO)
                                                CmdGlobal3.ExecuteNonQuery()
                                            End If
                                            Rs2X.Close()
                                        End If
                                    End If
                                End While
                            End If
                            Rs.Close()
                            '--------------------------------------------------------------------------------
                            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q USA SERIE
                            CmdGlobal.CommandText = " SELECT SUM(CASE WHEN RECIBIDA_OK='N' THEN 1 ELSE 0 END) AS CFALT, SUM(CASE WHEN RECIBIDA_OK='S' THEN 1 ELSE 0 END) AS CREC,COUNT(RECIBIDA_OK) AS CAREC " _
                                                  & " FROM TBINV_ALMACEN_DESPACHO_DET WHERE (DESP_CODIGO =" & pCodigoSalida & ") AND (DESPD_OK='S') AND (DESPD_SYS_EST='0')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    QARec = Nz(Rs!CAREC)
                                    QRec = Nz(Rs!CREC)
                                    QFaltRec = Nz(Rs!CFALT)
                                End While
                            End If
                            Rs.Close()
                            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q NO USA SERIE
                            CmdGlobal.CommandText = " SELECT SUM(DESPD_CANT_FALT_REC) AS CFALT, SUM(DESPD_CANT_REC) AS CREC, SUM(DESPD_CANT_DESP) AS CAREC " _
                                                  & " FROM TBINV_ALMACEN_DESPACHO_DET_SINSERIE WHERE (DESP_CODIGO =" & pCodigoSalida & ") AND (DESPD_SYS_EST='0')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    QARec = QARec + Nz(Rs!CAREC)
                                    QRec = QRec + Nz(Rs!CREC)
                                    QFaltRec = QFaltRec + Nz(Rs!CFALT)
                                End While
                            End If
                            Rs.Close()
                            If QARec = QRec And QFaltRec = 0 Then EstadoDesp = "3" Else EstadoDesp = "4"
                            CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='" & EstadoDesp & "',DESP_CANT_REC=" & QRec & ",DESP_CANT_FALT_REC=" & QFaltRec & " WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND  DESP_CODIGO=" & pCodigoSalida
                            CmdGlobal.ExecuteNonQuery()
                        Case "2" 'seccion cc
                            'DESPD_TIPO_RECIBIDA: A = AUTOMATICA, M/NULL = MANUAL
                            CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET SET RECIBIDA_OK='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO=" & pCodigoSalida & ""
                            CmdGlobal.ExecuteNonQuery()
                            CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET_SINSERIE SET OSALD_CANT_REC = OSALD_CANT_ENV, DESPD_CANT_FALT_REC = 0, OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO = 'A' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO=" & pCodigoSalida & ""
                            CmdGlobal.ExecuteNonQuery()
                            '::::::::::::::::::::::::::::::::::::::::::::::::::::: ARTICULOS Q USA SERIE
                            CmdGlobal.CommandText = "SELECT * FROM TBINV_CCOSTO_SALIDA_DET WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO=" & pCodigoSalida
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    CmdGlobal2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='2',UBICACT_CODIGO=" & pCodSeccionD & ",UBICACT_SYS='" & ValorSys & "',SERIE_FUNCION = '" & Nu(Rs!OSALD_FUNCION) & "' WHERE SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                    CmdGlobal2.ExecuteNonQuery()
                                    'ESTADO: 0 primera vez, 1 EN TRANSITO,2 OK
                                    CmdGlobal2.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,SERIE_FUNCION,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL) " _
                                                           & "VALUES(" & Nu(Rs!Serie_Numerar) & ",'2'," & pCodSeccionD & ",'2','0','" & ValorSys & "','" & Nu(Rs!OSALD_FUNCION) & "','" & FechaServer & "','2','" & pCodigoSalida & "')"
                                    CmdGlobal2.ExecuteNonQuery()
                                    '---------------------------------------------------------
                                    Select Case pCodMotivoSalida
                                        Case 1 'prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.OSAL_CODIGO=" & pCodigoSalida & " AND A.SERIE_NUMERAR=" & Nu(Rs!Serie_Numerar)
                                            CmdGlobal3.ExecuteNonQuery()
                                        Case 3 'devolucion por prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto
                                            CmdGlobal3.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '3',PREDET_SYS_DEVOLUCION = '" & ValorSys & "' FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                                  & " AND (B.PREDET_ESTADO_PRESTAMO = '2') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & pCodSeccionD & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & pCodSeccionO & ") AND (B.SERIE_NUMERAR = " & Nu(Rs!Serie_Numerar) & ")"
                                            CmdGlobal3.ExecuteNonQuery()
                                    End Select
                                End While
                            End If
                            Rs.Close()
                            '::::::::::::::::::::::::::::::::::::::::::::::::::::: ARTICULOS Q NO USA SERIE
                            CmdGlobal.CommandText = " SELECT * FROM TBINV_CCOSTO_SALIDA_DET_SINSERIE WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO=" & pCodigoSalida
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Not Rs.Read
                                    CmdGlobal2.CommandText = " SELECT * FROM TBINV_STOCK_SINSERIE_CCOSTO WHERE (CECOSE_CODIGO = " & pCodSeccionD & ") " _
                                                           & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                    Rs2X = CmdGlobal2.ExecuteReader
                                    If Rs2X.HasRows Then
                                        StockAc = Nz(Rs2X!SKSSCC_STOCK_ACTUAL) + Nu(Rs!OSALD_CANT_ENV)
                                        CmdGlobal3.CommandText = " UPDATE TBINV_STOCK_SINSERIE_CCOSTO SET SKSSCC_STOCK_ACTUAL=" & StockAc & " WHERE (CECOSE_CODIGO = " & pCodSeccionD & ") " _
                                                               & " AND (ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ") AND (SKSSCC_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    Else
                                        CmdGlobal3.CommandText = " INSERT TBINV_STOCK_SINSERIE_CCOSTO(CECOSE_CODIGO, ARTICULO_CODIGO,SKSSCC_STOCK_ACTUAL,SKSSCC_SYS_EST,EMPRESA_CODIGO) " _
                                                               & " VALUES(" & pCodSeccionD & "," & Nu(Rs!ARTICULO_CODIGO) & "," & Nz(Rs!OSALD_CANT_ENV) & ",'0','" & psCodEmpresa & "')"
                                        CmdGlobal3.ExecuteNonQuery()
                                    End If
                                    Rs2X.Close()
                                    '---------------------------------------------------------
                                    Select Case pCodMotivoSalida
                                        Case 1 'prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                            CmdGlobal2.CommandText = " UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_CANT_PRESTADA = PREDET_CANTXPRESTAR, PREDET_CANT_XDEVOLVER = 0, PREDET_CANT_FALT_DEVOLVER = PREDET_CANTXPRESTAR, PREDET_CANT_DEVUELTA = 0, PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' " _
                                                                   & " FROM TBINV_PRESTAMO_DETALLE_SINSERIE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.OSAL_CODIGO=" & pCodigoSalida & " AND A.ARTICULO_CODIGO=" & Nu(Rs!ARTICULO_CODIGO)
                                            CmdGlobal2.ExecuteNonQuery()
                                        Case 3 'devolucion por prestamo
                                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                                            CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE_SINSERIE SET PREDET_ESTADO_PRESTAMO = (CASE PREDET_CANT_FALT_DEVOLVER THEN " & Nz(Rs!OSALD_CANT_ENV) & " THEN '3' ELSE '5' END)," _
                                                                   & " PREDET_CANT_XDEVOLVER = ISNULL(PREDET_CANT_XDEVOLVER,0) - " & Nz(Rs!OSALD_CANT_ENV) & ",PREDET_CANT_FALT_DEVOLVER = ISNULL(PREDET_CANT_FALT_DEVOLVER,0) - " & Nz(Rs!OSALD_CANT_ENV) & "," _
                                                                   & " PREDET_CANT_DEVUELTA = ISNULL(PREDET_CANT_DEVUELTA,0) + " & Nz(Rs!OSALD_CANT_ENV) & " , PREDET_SYS_DEVOLUCION = '" & ValorSys & "' " _
                                                                   & " FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE_SINSERIE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                                   & " AND (B.PREDET_ESTADO_PRESTAMO IN ('2','4')) AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '2') AND (A.CECOSE_CODIGO_ORIGEN =" & pCodSeccionD & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & pCodSeccionO & ") AND (B.ARTICULO_CODIGO = " & Nu(Rs!ARTICULO_CODIGO) & ")"
                                            CmdGlobal2.ExecuteNonQuery()
                                    End Select
                                End While
                            End If
                            Rs.Close()
                            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q USA SERIE
                            CmdGlobal.CommandText = " SELECT SUM(CASE WHEN RECIBIDA_OK='N' THEN 1 ELSE 0 END) AS CFALT, SUM(CASE WHEN RECIBIDA_OK='S' THEN 1 ELSE 0 END) AS CREC,COUNT(RECIBIDA_OK) AS CAREC " _
                                                  & " FROM TBINV_CCOSTO_SALIDA_DET WHERE (OSAL_CODIGO =" & pCodigoSalida & ") AND (ENVIADA_OK='S') AND (OSALD_SYS_EST='0')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    QARec = Nz(Rs!CAREC)
                                    QRec = Nz(Rs!CREC)
                                    QFaltRec = Nz(Rs!CFALT)
                                End While
                            End If
                            Rs.Close()
                            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q NO USA SERIE
                            CmdGlobal.CommandText = " SELECT SUM(OSALD_CANT_FALT_REC) AS CFALT, SUM(OSALD_CANT_REC) AS CREC, SUM(OSALD_CANT_ENV) AS CAREC " _
                                                  & " FROM TBINV_CCOSTO_SALIDA_DET_SINSERIE WHERE (OSAL_CODIGO =" & pCodigoSalida & ") AND (OSALD_SYS_EST='0')"
                            Rs = CmdGlobal.ExecuteReader
                            If Rs.HasRows Then
                                While Rs.Read
                                    QARec = QARec + Nz(Rs!CAREC)
                                    QRec = QRec + Nz(Rs!CREC)
                                    QFaltRec = QFaltRec + Nz(Rs!CFALT)
                                End While
                            End If
                            Rs.Close()
                            If QARec = QRec And QFaltRec = 0 Then EstadoDesp = "3" Else EstadoDesp = "4"
                            CmdGlobal.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='" & EstadoDesp & "',OSAL_CANT_REC=" & QRec & ",OSAL_CANT_FALT_REC=" & QFaltRec & " WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND  OSAL_CODIGO=" & pCodigoSalida
                            CmdGlobal.ExecuteNonQuery()
                    End Select
            End Select
        Catch ex As SqlException
            Dim a As String = ""
            a = ex.Message
        Catch ex As Exception
            Dim a As String = ""
            a = ex.Message
        Finally
        End Try
    End Sub
    Public Function Nombre_OperacIng(ByVal pMotivoIng As String) As String
        Nombre_OperacIng = ""
        Select Case pMotivoIng
            Case "1" : Nombre_OperacIng = "Ingreso x Cambio"
            Case "2" : Nombre_OperacIng = "Ingreso x Reparación"
            Case "3" : Nombre_OperacIng = "Ingreso x Devolución"
            Case "4" : Nombre_OperacIng = "Ingreso x Demostración"
            Case "5" : Nombre_OperacIng = "Ingreso x Traslado"
            Case "6" : Nombre_OperacIng = "Ingreso x Prestamo"
            Case "7" : Nombre_OperacIng = "ngreso x Devolución" '--
            Case "8" : Nombre_OperacIng = "Ingreso x Compra"
            Case "9" : Nombre_OperacIng = "Ingreso x Donación"
            Case "10" : Nombre_OperacIng = "Ingreso x Alquiler"
            Case "11" : Nombre_OperacIng = "Ingreso x Respaldo"
            Case "12" : Nombre_OperacIng = "Ingreso de un Equipo"
            Case "13" : Nombre_OperacIng = "Ingreso x Devolución por Reparación"
            Case "14" : Nombre_OperacIng = "Ingreso x Averia"
            Case "15" : Nombre_OperacIng = "Ingreso x Devolución por Amortización"
            Case "16" : Nombre_OperacIng = "Ingreso x Cambio por Proveedor"
            Case "17" : Nombre_OperacIng = "Ingreso x Demostracion"
            Case "18" : Nombre_OperacIng = "Ingreso x Baja"
            Case "20" : Nombre_OperacIng = "Ingreso x Inventario"
            Case "21" : Nombre_OperacIng = "Ingreso x Componente" '--
            Case "22" : Nombre_OperacIng = "Ingreso x Anulación"
            Case "23" : Nombre_OperacIng = "Ingreso x Regularización"
            Case "24" : Nombre_OperacIng = "Ingreso x Devolucion en Mantenimiento en Proveedor"
            Case "25" : Nombre_OperacIng = "Ingreso x Devolución Definitiva a Proveedor"
            Case "27" : Nombre_OperacIng = "Ingreso x Traslado"
            Case "29" : Nombre_OperacIng = "Ingreso x Importación"
            Case "31" : Nombre_OperacIng = "Ingreso x Fabricación"
            Case "32" : Nombre_OperacIng = "Ingreso x Producto Terminado"
        End Select
    End Function
    Public Function Tipo_OperacIng(ByVal pMotivoIng As String) As String
        Tipo_OperacIng = ""
        Select Case pMotivoIng
            Case "1" : Tipo_OperacIng = "TRANS_INGXCAMBIO"
            Case "2" : Tipo_OperacIng = "TRANS_INGXMANTE"
            Case "3" : Tipo_OperacIng = "TRANS_INGXCAMBIO"
            Case "4" : Tipo_OperacIng = "TRANS_INGXDEMOS"
            Case "5" : Tipo_OperacIng = "TRANS_INGXTRANSF"
            Case "6" : Tipo_OperacIng = "TRANS_INGXPRESTA"
            Case "7" : Tipo_OperacIng = "TRANS_INGXDEVOL"
            Case "8" : Tipo_OperacIng = "TRANS_INGXCOMPRA"
            Case "9" : Tipo_OperacIng = "TRANS_INGXCOMPRA"
            Case "10" : Tipo_OperacIng = "TRANS_INGXCOMPRA"
            Case "11" : Tipo_OperacIng = "TRANS_INGXPRESTA"
            Case "12" : Tipo_OperacIng = "TRANS_INGXCAMBIO"
            Case "13" : Tipo_OperacIng = "TRANS_INGXMANTE"
            Case "14" : Tipo_OperacIng = "TRANS_INGXCAMBIO"
            Case "15" : Tipo_OperacIng = "TRANS_INGXDEMOS"
            Case "16" : Tipo_OperacIng = "TRANS_INGXTRANSF"
            Case "17" : Tipo_OperacIng = "TRANS_INGXTRANSF"
            Case "18" : Tipo_OperacIng = "TRANS_INGXTRANSF"
            Case "20" : Tipo_OperacIng = "TRANS_INGXPRESTA"
            Case "21" : Tipo_OperacIng = "TRANS_INGXDEVOL"
            Case "22" : Tipo_OperacIng = "TRANS_INGXCOMPRA"
            Case "23" : Tipo_OperacIng = "TRANS_INGXCOMPRA"
            Case "24" : Tipo_OperacIng = "TRANS_INGXCOMPRA"
            Case "25" : Tipo_OperacIng = "TRANS_INGXPRESTA"
            Case "27" : Tipo_OperacIng = "TRANS_INGXTRANSF"
            Case "29" : Tipo_OperacIng = "TRANS_INGXTRANSF"
            Case "31" : Tipo_OperacIng = "TRANS_INGXTRANSF"
            Case "32" : Tipo_OperacIng = "TRANS_INGXTRANSF"
        End Select
    End Function
    Public Function Estado_Ingreso(ByVal pMotivoIng As String) As String
        Estado_Ingreso = ""
        Select Case pMotivoIng
            Case "1" : Estado_Ingreso = "6"
            Case "2" : Estado_Ingreso = "2"
            Case "3" : Estado_Ingreso = "6"
            Case "4" : Estado_Ingreso = "4"
            Case "5"
            Case "6" : Estado_Ingreso = "1"
            Case "7" : Estado_Ingreso = "18"
            Case "8" : Estado_Ingreso = "5"
            Case "9" : Estado_Ingreso = "7"
            Case "10" : Estado_Ingreso = "15"
            Case "11" : Estado_Ingreso = "3"
            Case "12" : Estado_Ingreso = "1"
            Case "13" : Estado_Ingreso = "3"
            Case "14" : Estado_Ingreso = "11"
            Case "15" : Estado_Ingreso = "17"
            Case "16"
            Case "17" : Estado_Ingreso = "14"
            Case "18" : Estado_Ingreso = "13"
            Case "19"
            Case "20"
            Case "21" : Estado_Ingreso = "21"
            Case "22" : Estado_Ingreso = "22"
            Case "23" : Estado_Ingreso = "23"
            Case "24" : Estado_Ingreso = "24"
            Case "25" : Estado_Ingreso = "25"
            Case "26"
            Case "27"
            Case "28"
            Case "29" : Estado_Ingreso = "29"
            Case "30" : Estado_Ingreso = "30"
            Case "31" : Estado_Ingreso = "31"
            Case "32" : Estado_Ingreso = "32"
        End Select
    End Function
    Public Function Tipo_OperacSal(ByVal pMotivoSal As String) As String
        Tipo_OperacSal = ""
        Select Case pMotivoSal
            Case "1" : Tipo_OperacSal = "TRANS_SALXPRESTA"
            Case "2" : Tipo_OperacSal = "TRANS_SALXMANTE"
            Case "3" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "4" : Tipo_OperacSal = "TRANS_SALXDEMOS"
            Case "5" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "6" : Tipo_OperacSal = "TRANS_SALXCAMBIO"
            Case "7" : Tipo_OperacSal = "TRANS_SALXCAMBIO"
            Case "8" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "9" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "10" : Tipo_OperacSal = "TTRANS_SALXDEVOL"
            Case "11" : Tipo_OperacSal = "TRANS_SALXCAMBIO"
            Case "12" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "13" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "14" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "15" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "16" : Tipo_OperacSal = "TRANS_SALXMANTE"
            Case "17" : Tipo_OperacSal = "TRANS_SALXMANTE"
            Case "18" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "19" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "20" : Tipo_OperacSal = "TRANS_SALXMANTE"
            Case "21" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "22" : Tipo_OperacSal = "TRANS_SALXMANTE"
            Case "23" : Tipo_OperacSal = "TRANS_SALXMANTE"
            Case "24" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "25" : Tipo_OperacSal = "TRANS_SALXDEVOL"
            Case "26" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "27" : Tipo_OperacSal = "TRANS_SALXVENTA"
            Case "28" : Tipo_OperacSal = "TRANS_SALXMANTE"
            Case "29" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "31" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "32" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "33" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "34" : Tipo_OperacSal = "TRANS_SALXTRANSF"
            Case "50" : Tipo_OperacSal = "TRANS_SALXTRANSF"
        End Select
    End Function
    Public Function Nombre_OperacSal(ByVal pMotivoSal As String) As String
        Select Case pMotivoSal
            Case "1" : Nombre_OperacSal = "Salida x Prestamo"
            Case "2" : Nombre_OperacSal = "Salida x Reparación"
            Case "3" : Nombre_OperacSal = "Salida x Devolución de Prestamo"
            Case "4" : Nombre_OperacSal = "Salida x Demostración"
            Case "5" : Nombre_OperacSal = "Salida x Asignación"
            Case "6" : Nombre_OperacSal = "Salida x Reemplazo por Cambio"
            Case "7" : Nombre_OperacSal = "Salida x Baja"
            Case "8" : Nombre_OperacSal = "Salida x Traslado"
            Case "9" : Nombre_OperacSal = "Salida x Devolución por Error"
            Case "10" : Nombre_OperacSal = "Salida x Devolución por Amortización"
            Case "11" : Nombre_OperacSal = "Salida x Reemplazo de Avería"
            Case "12" : Nombre_OperacSal = "Salida x Devolución Reemplazo por Cambio"
            Case "13" : Nombre_OperacSal = "Salida x Devolución Reemplazo por averia"
            Case "14" : Nombre_OperacSal = "Salida x Devolución de Respaldo"
            Case "15" : Nombre_OperacSal = "Salida x Devolución por Demostración"
            Case "16" : Nombre_OperacSal = "Salida x Mantenimiento en Proveedor"
            Case "17" : Nombre_OperacSal = "Salida x Averia"
            Case "18" : Nombre_OperacSal = "Salida x Devolución por Reparación"
            Case "19" : Nombre_OperacSal = "Salida x Devolución de Equipos Averiados"
            Case "20" : Nombre_OperacSal = "Salida x Inventario"
            Case "21" : Nombre_OperacSal = "Salida x Componente"
            Case "22" : Nombre_OperacSal = "Salida x Anulacion"
            Case "23" : Nombre_OperacSal = "Salida x Regularización"
            Case "24" : Nombre_OperacSal = "Salida x Devolución en Mantenimiento en Proveedor"
            Case "25" : Nombre_OperacSal = "Salida x Devolucion Definitiva a Proveedor"
            Case "26" : Nombre_OperacSal = "Salida"
            Case "27" : Nombre_OperacSal = "Salida x Venta"
            Case "28" : Nombre_OperacSal = "Salida x Regularización por Migración"
            Case "29" : Nombre_OperacSal = "Salida x Importación"
            Case "31" : Nombre_OperacSal = "Salida x Fabricación" 'fabricacion
            Case "32" : Nombre_OperacSal = "Salida x Nacionalización"
            Case "33" : Nombre_OperacSal = "Salida x Alquiler"
            Case "34" : Nombre_OperacSal = "Salida x Donación"
            Case "35" : Nombre_OperacSal = "Salida x Transformacion"
            Case "50" : Nombre_OperacSal = "Salida x Reposición de Papel"
        End Select
        Nombre_OperacSal = ""
    End Function
    Public Sub Pedido_Rechazado(ByVal psConexion As String, ByVal psTipoOrigen As String,
                                ByVal pdCodOrigen As Double, ByVal psTipoDestino As String,
                                ByVal pdCodDestino As Double, ByVal psCodEmpresa As String,
                                ByVal psFechaDev As String, ByVal psHoraDev As String,
                                ByVal psUser As String, ByVal psObs As String,
                                ByVal pdCodGuia As Double)
        Dim pdCodDev As Double : pdCodDev = 0
        Dim dt As New DataTable
        Dim cant As Double : cant = 1
        Dim obj As New clsInv_Listados
        Dim objInsUpdDel As New clsInv_InsUpdDel
        Dim pdCodSalida As Double = 0
        Dim pdCodEquipo As Double = 0
        Dim pdCodArt As Double = 0
        Dim pdCant As Double = 1
        Dim i As Long = 0
        Dim a As Long = 0
        Dim CantAcc As Long = 0
        Try
            dt = obj.Ultima_CodDevolucion(psConexion)
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    pdCodDev = Nu(drMenuItem("CODIGO")) + 1
                Next
            Else
                pdCodDev = "00000001"
            End If
            dt = Nothing
            objInsUpdDel.Ins_Devolucion(psConexion, psCodEmpresa, pdCodDev, psFechaDev, psHoraDev,
                               psUser, pdCodDestino, psTipoOrigen, pdCodOrigen, cant, psObs)
            'equipos
            dt = obj.Lista_SalEquipos_xGuia(psConexion, psCodEmpresa, pdCodGuia)
            If dt.Rows.Count > 0 Then
                For Each drItem As Data.DataRow In dt.Rows
                    i = i + 1
                    pdCant = 1
                    pdCodSalida = Nz(drItem("DESP_CODIGO"))
                    pdCodEquipo = Nz(drItem("SERIE_NUMERAR"))
                    pdCodArt = Nz(drItem("ARTICULO_CODIGO"))
                    objInsUpdDel.Upd_Salida(psConexion, psCodEmpresa, pdCodDev, pdCodSalida, psFechaDev, pdCodEquipo)
                    objInsUpdDel.Ins_Devolucion_Detalle(psConexion, psCodEmpresa, pdCodDev, pdCodEquipo, i)
                    objInsUpdDel.InsUpd_StockActual(psConexion, psCodEmpresa, pdCodDev, pdCodArt, pdCodDestino, psTipoOrigen, pdCodOrigen, pdCant)
                    objInsUpdDel.InsUpd_UbicEquipo(psConexion, psCodEmpresa, pdCodDev, pdCodEquipo, pdCodDestino, psUser)
                    objInsUpdDel.Ins_Movimiento(psConexion, psCodEmpresa, pdCodDev, pdCodArt, pdCodDestino, psTipoOrigen, pdCodOrigen, psUser, pdCant)
                Next
            End If
            dt = Nothing
            'accesorios
            dt = obj.Lista_SalAccesorios_xGuia(psConexion, psCodEmpresa, pdCodGuia)
            If dt.Rows.Count > 0 Then
                For Each drItem As Data.DataRow In dt.Rows
                    a = a + 1
                    pdCodSalida = Nz(drItem("DESP_CODIGO"))
                    pdCodArt = Nz(drItem("ARTICULO_CODIGO"))
                    pdCant = Nz(drItem("DESPD_CANT_REC"))
                    CantAcc = CantAcc + pdCant
                    objInsUpdDel.Upd_Salida_SinSerie(psConexion, psCodEmpresa, pdCodDev, pdCodSalida, psFechaDev, pdCodArt, pdCant)
                    objInsUpdDel.Ins_Devolucion_Detalle_SinSerie(psConexion, psCodEmpresa, pdCodDev, pdCodArt, a, pdCant)
                    objInsUpdDel.InsUpd_StockActual(psConexion, psCodEmpresa, pdCodDev, pdCodArt, pdCodDestino, psTipoOrigen, pdCodOrigen, pdCant)
                    objInsUpdDel.Ins_Movimiento(psConexion, psCodEmpresa, pdCodDev, pdCodArt, pdCodDestino, psTipoOrigen, pdCodOrigen, psUser, pdCant)
                Next
            End If
            dt = Nothing
            Dim CantTotal As Long = 0
            CantTotal = i + CantAcc
            objInsUpdDel.Upd_Devolucion(psConexion, psCodEmpresa, pdCodDev, CantTotal)
        Catch ex As SqlException
        Catch ex As Exception
        Finally
        End Try
    End Sub
    Public Sub Almacen_Autorizado(ByVal psConexion As String, ByVal pCodEmpresa As String,
                                  ByVal pUser As String, Optional ByVal pdCodAlmacen As Double = 0)
        Dim pAlmacen As String : pAlmacen = ""
        Dim dt As New DataTable
        Dim pCodAlmacen As Double : pCodAlmacen = 0
        Dim Cn As New SqlConnection(psConexion)
        Dim cmdSql As New SqlCommand
        Dim obj As New clsInv_Listados
        Dim objDel As New clsInv_InsUpdDel
        Try
            Cn.Open()
            cmdSql.Connection = Cn
            If Existe_Tabla("V_ALMXUSUARIO", psConexion) = False Then
                cmdSql.CommandText = " CREATE TABLE [dbo].[V_ALMXUSUARIO] (" _
                                  & " [ALM_CODIGO] [FLOAT] NOT NULL) ON [PRIMARY]"
                cmdSql.ExecuteNonQuery()
            End If
            Cn.Close()
            objDel.Del_AlmacenUsuario(psConexion)
            dt = obj.Lista_AlmacenxUsuario(psConexion, pCodEmpresa, pUser, pdCodAlmacen)
            If dt.Rows.Count > 0 Then
                For Each drMenuItem As Data.DataRow In dt.Rows
                    pCodAlmacen = Nz(drMenuItem("ALMACEN_CODIGO"))
                    objDel.Ins_AlmacenxUsuario(psConexion, pCodAlmacen)
                Next
            End If
            dt = Nothing
        Catch ex As SqlException
        Catch ex As Exception
        Finally
        End Try
    End Sub
    Public Sub Recepcion_Automatica_CentroCosto(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                    ByVal psCodSalida As String, ByVal psUser As String,
                                    ByVal psMotivo As String, ByVal psPerRecibe As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim Rs3 As SqlDataReader
        Dim Stock As Long = 0
        Dim psNroMov As String = ""
        Dim FechaServer As String = FechaActual()
        Dim HoraServer As String = HoraActual()
        Dim ValorSys As String = FechaServer & HoraServer & psUser

        'Dim Estado As String = "2"
        Dim SysEst As String = "0"
        Dim Motivo As String = psMotivo
        Dim psCodDestino As String = ""

        Try
            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CmdGlobal3.Connection = Cn3
            'EQUIPOS
            CmdGlobal.CommandText = " SELECT S.SERIE_NUMERAR, S.ARTICULO_CODIGO, CECOSE_CODIGO_DESTINO, ALMACEN_CODIGO_DESTINO, " _
                                  & " OSAL_TIPODESTINO,CECOSE_CODIGO_ORIGEN,OSAL_MOTIVO_GRAL,SERIE_FUNCION  " _
                                  & " FROM TBINV_CCOSTO_SALIDA_DET OD INNER JOIN TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S " _
                                  & " ON S.SERIE_NUMERAR = OD.SERIE_NUMERAR INNER JOIN TBINV_CCOSTO_SALIDA O " _
                                  & " ON OD.OSAL_CODIGO = O.OSAL_CODIGO AND OD.EMPRESA_CODIGO = O.EMPRESA_CODIGO " _
                                  & " WHERE O.EMPRESA_CODIGO='" & psCodEmpresa & "' AND O.OSAL_CODIGO = " & psCodSalida
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    If Rs("OSAL_TIPODESTINO") = "1" Then
                        psCodDestino = Rs("ALMACEN_CODIGO_DESTINO")
                    ElseIf Rs("OSAL_TIPODESTINO") = "2" Then
                        psCodDestino = Rs("CECOSE_CODIGO_DESTINO")
                    End If
                    CmdGlobal2.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET SET RECIBIDA_OK='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO = 'M',OSALD_PERSONA_REC='" & psPerRecibe & "'  WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO=" & psCodSalida & " AND SERIE_NUMERAR=" & Rs("SERIE_NUMERAR")
                    CmdGlobal2.ExecuteNonQuery()
                    'paso 1
                    'se agrego para poder tener la informacion de stock de centro de costo en una misma tabla dependiendo del tipo de ubicacion
                    'INGRESO EN STOCK ALMACEN
                    CmdGlobal2.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodDestino & ") AND (UBICACT_TIPO='" & Rs("OSAL_TIPODESTINO") & "')" _
                        & " AND (ARTICULO_CODIGO = " & Rs("ARTICULO_CODIGO") & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            Stock = Nz(Rs2("SAA_STOCK_ACTUAL")) + 1
                            CmdGlobal3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & Stock & " WHERE (ALMACEN_CODIGO = " & psCodDestino & ") " _
                                                  & " AND (ARTICULO_CODIGO = " & Rs2("ARTICULO_CODIGO") & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "') AND (UBICACT_TIPO='" & Rs("OSAL_TIPODESTINO") & "')"
                            CmdGlobal3.ExecuteNonQuery()
                        End While
                    Else
                        CmdGlobal3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(UBICACT_TIPO,ALMACEN_CODIGO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                              & "VALUES('" & Rs("OSAL_TIPODESTINO") & "'," & psCodDestino & "," & Rs("ARTICULO_CODIGO") & ",1,'0','" & psCodEmpresa & "')"
                        CmdGlobal3.ExecuteNonQuery()
                    End If
                    Rs2.Close()

                    'paso 2
                    'aqui se guardara el movimiento de ingreso al centro de costo
                    'INGRESO A LA TABLA TBINV_MOVIMIENTO_GENERAL=========================================================================
                    CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_TRANS=" & psCodSalida & ") AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CmdGlobal3.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET MOV_ESTADO ='3' WHERE (CODIGO_TRANS='" & psCodSalida & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                            CmdGlobal3.ExecuteNonQuery()
                        End While
                    End If
                    Rs2.Close()

                    CmdGlobal2.CommandText = "SELECT * FROM TBINV_MOVIMIENTO_GENERAL WHERE (CODIGO_ARTICULO = " & Rs("ARTICULO_CODIGO") & ") AND (MOV_NRO='" & psNroMov & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0' "
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CmdGlobal3.CommandText = " UPDATE TBINV_MOVIMIENTO_GENERAL SET NRO_ARTICULO =" & Nz(Rs2("NRO_ARTICULO")) + 1 & " WHERE (CODIGO_ARTICULO = " & Rs("ARTICULO_CODIGO") & ") AND (MOV_NRO='" & psNroMov & "') AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND MOV_SYS_EST='0'"
                            CmdGlobal3.ExecuteNonQuery()
                        End While
                    Else
                        CmdGlobal3.CommandText = "SELECT MAX(MOV_NRO) as Cant FROM TBINV_MOVIMIENTO_GENERAL "
                        Rs3 = CmdGlobal3.ExecuteReader
                        If Rs3.HasRows Then
                            While Rs2.Read
                                psNroMov = Nz(Rs3("Cant")) + 1
                            End While
                        Else
                            psNroMov = "00000001"
                        End If
                        Rs3.Close()
                        '1: INGRESO, 2:SALIDA
                        CmdGlobal3.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO,CODIGO_ORIGEN_DESTINO, " _
                                              & " CODIGO_TRANS, CODIGO_ARTICULO ,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_MOTIVO,MOV_FECHA,MOV_SYS_EST) " _
                                              & " values('" & psCodEmpresa & "','" & psNroMov & "','1','" & Rs("OSAL_TIPODESTINO") & "','" & psCodDestino & "','2','" & Rs("CECOSE_CODIGO_ORIGEN") & "', " _
                                              & " '" & psCodSalida & "','" & Rs("ARTICULO_CODIGO") & "','1','" & ValorSys & "','3','" & Rs("OSAL_MOTIVO_GRAL") & "','" & FechaServer & "','0')"
                        CmdGlobal3.ExecuteNonQuery()
                    End If
                    Rs2.Close()

                    CmdGlobal3.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='" & Rs("OSAL_TIPODESTINO") & "',UBICACT_CODIGO=" & psCodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_FUNCION = '" & Rs("SERIE_FUNCION") & "' WHERE SERIE_NUMERAR=" & Rs("SERIE_NUMERAR")
                    CmdGlobal3.ExecuteNonQuery()
                    'ESTADO: 0 primera vez, 1 EN TRANSITO,2 OK
                    CmdGlobal3.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,SERIE_FUNCION,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL,MOTIVO) " _
                                           & "VALUES(" & Rs("SERIE_NUMERAR") & ",'" & Rs("OSAL_TIPODESTINO") & "'," & psCodDestino & ",'2','0','" & ValorSys & "','" & Rs("SERIE_FUNCION") & "','" & FechaServer & "','2','" & psCodSalida & "','" & Rs("OSAL_MOTIVO_GRAL") & "')"
                    CmdGlobal3.ExecuteNonQuery()

                    Select Case Rs("OSAL_MOTIVO_GRAL")
                        Case 1 'prestamo
                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 por devuelto parcial
                            CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_ENVIO='1', PREDET_ESTADO_PRESTAMO='1',PREDET_SYS_PRESTAMO ='" & ValorSys & "' FROM TBINV_PRESTAMO_DETALLE A,TBINV_PRESTAMO B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND A.EMPRESA_CODIGO='" & psCodEmpresa & "' AND B.OSAL_CODIGO=" & psCodSalida & " AND A.SERIE_NUMERAR=" & Rs("SERIE_NUMERAR")
                            CmdGlobal2.ExecuteNonQuery()
                        Case 3 'devolucion por prestamo
                            'estado envio 0 enviado ,1 recibido; estado prestamo 0 por prestar, 1 prestado,2 por devolver, 3 devuelto, 4 devuelto parcial
                            If Rs("OSAL_TIPODESTINO") = "1" Then
                                CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '3',PREDET_SYS_DEVOLUCION = '" & ValorSys & "' FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                     & " AND (B.PREDET_ESTADO_PRESTAMO = '2') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '" & Rs("OSAL_TIPODESTINO") & "') AND (A.ALMACEN_CODIGO_ORIGEN =" & psCodDestino & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & Rs("CECOSE_CODIGO_ORIGEN") & ") AND (B.SERIE_NUMERAR = " & Rs("SERIE_NUMERAR") & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            Else
                                CmdGlobal2.CommandText = "UPDATE TBINV_PRESTAMO_DETALLE SET PREDET_ESTADO_PRESTAMO = '3',PREDET_SYS_DEVOLUCION = '" & ValorSys & "' FROM TBINV_PRESTAMO A, TBINV_PRESTAMO_DETALLE B WHERE A.EMPRESA_CODIGO = B.EMPRESA_CODIGO AND A.PRESTA_CODIGO = B.PRESTA_CODIGO AND (A.EMPRESA_CODIGO = '" & psCodEmpresa & "') " _
                                                     & " AND (B.PREDET_ESTADO_PRESTAMO = '2') AND (A.PRESTA_TIPO_MOVIMIENTO = 'S') AND (A.PRESTA_TIPOORIGEN = '" & Rs("OSAL_TIPODESTINO") & "') AND (A.CECOSE_CODIGO_ORIGEN =" & psCodDestino & ") AND (A.PRESTA_TIPODESTINO = '2') AND (A.CECOSE_CODIGO_DESTINO = " & Rs("CECOSE_CODIGO_ORIGEN") & ") AND (B.SERIE_NUMERAR = " & Rs("SERIE_NUMERAR") & ")"
                                CmdGlobal2.ExecuteNonQuery()
                            End If
                        Case 19
                            CmdGlobal2.CommandText = " UPDATE TBINV_AVERIA SET AVERIA_DEVOLVER_CC='3', AVERIA_SYS_MOD='" & ValorSys & "' WHERE SALIDA_DEVOLVER_ALM='" & psCodSalida & "' AND AVERIA_SYS_EST='0' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                            CmdGlobal2.ExecuteNonQuery()
                    End Select
                End While
            End If
            Rs.Close()
            'ACCESORIOS
            'CONTEO DE EQUIPOS CON SERIES
            Dim EstadoDesp As String
            Dim QARec As Long = 0
            Dim QRec As Long = 0
            Dim QFaltRec As Long = 0
            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q USA SERIE
            CmdGlobal2.CommandText = "SELECT SUM(CASE WHEN RECIBIDA_OK='N' THEN 1 ELSE 0 END) AS CFALT, SUM(CASE WHEN RECIBIDA_OK='S' THEN 1 ELSE 0 END) AS CREC,COUNT(RECIBIDA_OK) AS CAREC " _
                & " FROM TBINV_CCOSTO_SALIDA_DET WHERE (OSAL_CODIGO =" & psCodSalida & ") AND (ENVIADA_OK='S') AND (OSALD_SYS_EST='0')"
            Rs2 = CmdGlobal2.ExecuteReader
            If Rs2.HasRows Then
                While Rs2.Read
                    QARec = Nz(Rs2("CAREC"))
                    QRec = Nz(Rs2("CREC"))
                    QFaltRec = Nz(Rs2("CFALT"))
                End While
            End If
            Rs2.Close()
            '::::::::::::::::::::::::::::::: CONTEO ARTICULO Q NO USA SERIE
            CmdGlobal2.CommandText = "SELECT SUM(OSALD_CANT_FALT_REC) AS CFALT, SUM(OSALD_CANT_REC) AS CREC, SUM(OSALD_CANT_ENV) AS CAREC " _
                & " FROM TBINV_CCOSTO_SALIDA_DET_SINSERIE WHERE (OSAL_CODIGO =" & psCodSalida & ") AND (OSALD_SYS_EST='0')"
            Rs2 = CmdGlobal2.ExecuteReader
            If Rs2.HasRows Then
                While Rs2.Read
                    QARec = QARec + Nz(Rs2("CAREC"))
                    QRec = QRec + Nz(Rs2("CREC"))
                    QFaltRec = QFaltRec + Nz(Rs2("CFALT"))
                End While
            End If
            Rs2.Close()
            If QARec = QRec And QFaltRec = 0 Then EstadoDesp = "3" Else EstadoDesp = "4"
            CmdGlobal2.CommandText = "UPDATE TBINV_CCOSTO_SALIDA SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='" & EstadoDesp & "',OSAL_CANT_REC=" & QRec & ",OSAL_CANT_FALT_REC=" & QFaltRec & " WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND  OSAL_CODIGO=" & psCodSalida
            CmdGlobal2.ExecuteNonQuery()
        Catch ex As SqlException

        Catch ex As Exception

        End Try
    End Sub
    Public Function Invnetario_Salida_Ingreso_Automatico(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                                         ByVal psUser As String, ByVal psTipoAnti As String,
                                                         ByVal psTipoActual As String, ByVal psCodAnti As Double,
                                                         ByVal psCodActual As Double, ByVal psSerieNumerar As Double,
                                                         ByVal psCodArticulo As Double) As Double
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim StockAc As Double = 0
        Dim pdSalida As Double = 0
        Dim psNroMov As String = ""
        Dim FechaServer As String = FechaActual()
        Dim HoraServer As String = HoraActual()
        Dim ValorSys As String = FechaServer & HoraServer & psUser

        Dim lblCodAlmacen As String = ""
        Dim lblCodCCosto As String = ""
        Dim lblCodProveedor As String = ""
        Dim lblCodDespacho As String = ""
        Dim lblNroMovimiento As String = ""

        Dim NroSal As String = ""

        lblCodAlmacen = "NULL"
        lblCodCCosto = "NULL"
        lblCodProveedor = "NULL"
        If psTipoActual = "1" Then
            lblCodAlmacen = psCodActual
        ElseIf psTipoActual = "2" Then
            lblCodCCosto = psCodActual
        ElseIf psTipoActual = "3" Then
            lblCodProveedor = psCodActual
        End If
        'Try
        Cn.Open() : Cn2.Open() : Cn3.Open()
        CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
        CmdGlobal3.Connection = Cn3
        If psTipoAnti = "1" Then  'CAMBIAR DE UBICACION
            '-----------------------SALIDA DE ALMACEN
            CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblCodDespacho = Nz(Rs(0)) + 1
                End While
            Else
                lblCodDespacho = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                      & " ALMACEN_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO,PROVEEDOR_CODIGO_DESTINO,DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                      & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",'" & FechaServer & "','" & HoraServer & "','" & psUser & "','" & psTipoActual & "'," _
                                      & lblCodAlmacen & "," & lblCodCCosto & "," & lblCodProveedor & ",'2','0',1,1,0,1,'" & psCodAnti & "'," _
                                      & " '" & FechaServer & "','" & HoraServer & "','23','" & ValorSys & "')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','0',NULL,'23','N')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
            CmdGlobal.ExecuteNonQuery()
            'STOCK
            CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                    & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    StockAc = Nz(Rs("SAA_STOCK_ACTUAL")) - 1
                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                                              & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            'MOVIMIENTO GENERAL
            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroMovimiento = Nz(Rs(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                  & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                  & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','2','" & psTipoAnti & "','" & psCodAnti & "', " _
                                  & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoActual & "','" & psCodActual & "')"
            CmdGlobal.ExecuteNonQuery()
            '--------------------------recepcion
            If psTipoActual = "2" Or psTipoActual = "1" Then     ' en ccosto O ALMACEN
                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='3',DESP_CANT_REC='1',DESP_CANT_FALT_REC='0' WHERE DESP_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                CmdGlobal.ExecuteNonQuery()
                'STOCK
                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                        & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                                                  & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                Else
                    CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                               & "VALUES(" & psCodActual & ",'" & psTipoActual & "'," & psCodArticulo & ",1,'0','" & psCodEmpresa & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End If
                Rs.Close()
                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                      & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & psTipoActual & "','" & psCodActual & "', " _
                                      & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoAnti & "','" & psCodAnti & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='" & psTipoActual & "',UBICACT_CODIGO='" & psCodActual & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR='" & psSerieNumerar & "'"
                CmdGlobal.ExecuteNonQuery()
                Guardar_UltimosMovimiento_paraGPS(psConexion, psCodEmpresa, 0, FechaActual, psTipoAnti, psCodAnti, psTipoActual, psCodActual, psSerieNumerar, psUser)
                CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL)" _
                                              & " VALUES ('" & psSerieNumerar & "','" & psTipoActual & "','" & psCodActual & "','23','0','" & ValorSys & "','" & FechaServer & "','1','" & lblCodDespacho & "')"
                CmdGlobal.ExecuteNonQuery()
            End If
        ElseIf psTipoAnti = "2" Then  'SALIDA DE CENTRO DE COSTO
            CmdGlobal.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblCodDespacho = Nz(Rs(0)) + 1
                End While
            Else
                lblCodDespacho = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, ALMACEN_CODIGO_DESTINO, " _
                                      & " CECOSE_CODIGO_DESTINO, OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                      & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",'" & FechaServer & "','" & HoraServer & "','" & psUser & "','" & psTipoActual & "'," _
                                      & lblCodAlmacen & "," & lblCodCCosto & ",'2','0',1,0,1,'" & psCodAnti & "'," _
                                      & " '" & FechaServer & "','" & HoraServer & "','23')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','N','0','23')"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
            CmdGlobal.ExecuteNonQuery()
            'STOCK
            CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                    & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    StockAc = Nz(Rs("SAA_STOCK_ACTUAL")) - 1
                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                                              & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            'MOVIMIENTO GENERAL
            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroMovimiento = Nz(Rs(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                  & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                  & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','2','" & psTipoAnti & "','" & psCodAnti & "', " _
                                  & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoActual & "','" & psCodActual & "')"
            CmdGlobal.ExecuteNonQuery()
            '--------------------------recepcion
            If psTipoActual = "2" Or psTipoActual = "1" Then     ' en ccosto O ALMACEN
                CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET  SET RECIBIDA_OK ='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO='M' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA  SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='3',OSAL_CANT_REC='1',OSAL_CANT_FALT_REC='0' WHERE OSAL_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                CmdGlobal.ExecuteNonQuery()
                'STOCK
                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                        & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                                                  & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                Else
                    CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                              & "VALUES(" & psCodActual & ",'" & psTipoActual & "'," & psCodArticulo & ",1,'0','" & psCodEmpresa & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End If
                Rs.Close()
                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                      & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & psTipoActual & "','" & psCodActual & "', " _
                                      & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoAnti & "','" & psCodAnti & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='" & psTipoActual & "',UBICACT_CODIGO=" & psCodActual & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR='" & psSerieNumerar & "'"
                CmdGlobal.ExecuteNonQuery()
                Guardar_UltimosMovimiento_paraGPS(psConexion, psCodEmpresa, 0, FechaActual, psTipoAnti, psCodAnti, psTipoActual, psCodActual, psSerieNumerar, psUser)
                CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL,MOTIVO)" _
                                      & " VALUES ('" & psSerieNumerar & "','" & psTipoActual & "','" & psCodActual & "','23','0','" & ValorSys & "','" & FechaServer & "','2','" & lblCodDespacho & "','23')"
                CmdGlobal.ExecuteNonQuery()

            End If
        Else

            CmdGlobal.CommandText = "SELECT MAX(SALDEV_CODIGO) FROM dbo.TBINV_SALDEVOLUCION "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    NroSal = Nz(Rs(0)) + 1
                End While
            Else
                NroSal = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = " INSERT INTO dbo.TBINV_SALDEVOLUCION (EMPRESA_CODIGO, SALDEV_CODIGO, SALDEV_FECHA, SALDEV_FECHA_SAL,SALDEV_HORA, SALDEV_HORA_SAL,  SALDEV_USUARIO, " _
                                    & " SALDEV_TIPO_DESTINO, SALDEV_CODIGO_DESTINO, SALDEV_TIPO_ORIGEN, SALDEV_CODIGO_ORIGEN,  " _
                                    & " SALDEV_CANTIDAD, SALDEV_ESTADO, SALDEV_SYS_EST, SALDEV_OBSERVACION, SALDEV_MOTIVO, SALDEV_SYS_CRE) " _
                                    & " VALUES('" & psCodEmpresa & "'," & NroSal & ",'" & FechaServer & "','" & FechaServer & "','" & HoraServer & "','" & HoraServer & "','" & psUser & "', " _
                                    & " '" & psTipoActual & "'," & psCodActual & ",'" & psTipoAnti & "'," & psCodAnti & ", " _
                                    & " 1,'3','0','Salida por regularización de carga','23','" & ValorSys & "' )"
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = " INSERT INTO dbo.TBINV_SALDEVOLUCION_DET (EMPRESA_CODIGO , SALDEV_CODIGO, SALDEVD_ITEM, SALDEVD_SERIE_NUMERAR, SALDEVD_SYS_EST,ENVIADA_OK,RECIBIDA_OK) " _
                            & " VALUES('" & psCodEmpresa & "','" & NroSal & "',1," & psSerieNumerar & ",'0','S','S') "
            CmdGlobal.ExecuteNonQuery()

            'Stock almacen
            CmdGlobal.CommandText = "SELECT SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND UBICACT_TIPO='" & psTipoActual & "' AND ALMACEN_CODIGO='" & psCodActual & "' AND ARTICULO_CODIGO = " & psCodArticulo & ""
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL =(" & Nz(Rs!SAA_STOCK_ACTUAL) & " +1) WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND UBICACT_TIPO='" & psTipoActual & "' AND ALMACEN_CODIGO='" & psCodActual & "' AND ARTICULO_CODIGO = " & psCodArticulo
                    CmdGlobal2.ExecuteNonQuery()
                End While
            Else
                CmdGlobal2.CommandText = " INSERT INTO TBINV_STOCK_ARTICULOS_ALMACEN ( EMPRESA_CODIGO, UBICACT_TIPO, ALMACEN_CODIGO, ARTICULO_CODIGO, SAA_SYS_EST,SAA_STOCK_ACTUAL) " _
                                    & " VALUES('" & psCodEmpresa & "','" & psTipoActual & "','" & psCodActual & "','" & psCodArticulo & "','0','1') "
                CmdGlobal2.ExecuteNonQuery()
            End If
            Rs.Close()
            'Cambia destino
            CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='" & psTipoActual & "', UBICACT_CODIGO='" & psCodActual & "', UBICACT_SYS='" & ValorSys & "'" _
                                    & " WHERE SERIE_NUMERAR=" & psSerieNumerar
            CmdGlobal.ExecuteNonQuery()
            'UBICT
            CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_CRE,SYS_EST,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL,MOTIVO)" _
                                    & " VALUES('" & psSerieNumerar & "','" & psTipoActual & "','" & psCodActual & "','0','" & ValorSys & "','0','" & FechaServer & "','1','" & NroSal & "','23')"
            CmdGlobal.ExecuteNonQuery()
            'Stock Origen
            CmdGlobal.CommandText = "SELECT SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND UBICACT_TIPO='" & psTipoAnti & "' AND ALMACEN_CODIGO='" & psCodAnti & "' AND ARTICULO_CODIGO = " & psCodArticulo & ""
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL =(" & Nz(Rs!SAA_STOCK_ACTUAL) & " - 1) WHERE EMPRESA_CODIGO = '" & psCodEmpresa & "' AND UBICACT_TIPO='" & psTipoAnti & "' AND ALMACEN_CODIGO='" & psCodAnti & "' AND ARTICULO_CODIGO = " & psCodArticulo
                    CmdGlobal2.ExecuteNonQuery()
                End While
            End If
            Rs.Close()
            'Movimiento
            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroMovimiento = Nz(Rs(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            Rs.Close()

            CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO, " _
                                & " CODIGO_TRANS,CODIGO_ORIGEN_DESTINO,CODIGO_ARTICULO,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_FECHA,MOV_SYS_EST,MOV_MOTIVO) " _
                                & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & psTipoActual & "','" & psCodActual & "','" & psTipoAnti & "'," _
                                & " '" & NroSal & "','" & psCodAnti & "','" & psCodArticulo & "','1','" & ValorSys & "','3','" & FechaServer & "','0','23')"
            CmdGlobal.ExecuteNonQuery()
            'Movimiento INGRESO

            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroMovimiento = Nz(Rs(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT,TIPO_ORIGEN_DESTINO, " _
                                & " CODIGO_TRANS,CODIGO_ORIGEN_DESTINO,CODIGO_ARTICULO,NRO_ARTICULO,MOV_SYS_CRE,MOV_ESTADO,MOV_FECHA,MOV_SYS_EST,MOV_MOTIVO) " _
                                & " values('" & psCodEmpresa & "','" & lblNroMovimiento & "','2','" & psTipoAnti & "','" & psCodAnti & "','1'," _
                                & " '" & NroSal & "','" & psCodActual & "','" & psCodArticulo & "','1','" & ValorSys & "','3','" & FechaServer & "','0','23')"
            CmdGlobal.ExecuteNonQuery()

        End If
        Return lblCodDespacho
        'Catch ex As SqlException

        'Catch ex As Exception

        'End Try
    End Function
    Public Sub Salida_Ingreso_Automatico(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                         ByVal psUser As String, ByVal psTipoAnti As String,
                                         ByVal psTipoActual As String, ByVal psCodAnti As String,
                                         ByVal psCodActual As String, ByVal psSerieNumerar As String,
                                         ByVal psCodArticulo As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim StockAc As Double = 0
        Dim psNroMov As String = ""
        Dim FechaServer As String = FechaActual()
        Dim HoraServer As String = HoraActual()
        Dim ValorSys As String = FechaServer & HoraServer & psUser

        Dim lblCodAlmacen As String = ""
        Dim lblCodCCosto As String = ""
        Dim lblCodProveedor As String = ""
        Dim lblCodDespacho As String = ""
        Dim lblNroMovimiento As String = ""

        lblCodAlmacen = "NULL"
        lblCodCCosto = "NULL"
        lblCodProveedor = "NULL"
        If psTipoActual = "1" Then
            lblCodAlmacen = psCodActual
        ElseIf psTipoActual = "2" Then
            lblCodCCosto = psCodActual
        ElseIf psTipoActual = "3" Then
            lblCodProveedor = psCodActual
        End If
        Try
            Cn.Open() : Cn2.Open() : Cn3.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CmdGlobal3.Connection = Cn3
            If psTipoAnti = "1" Then  'CAMBIAR DE UBICACION
                '-----------------------SALIDA DE ALMACEN
                CmdGlobal.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblCodDespacho = Nz(Rs(0)) + 1
                    End While
                Else
                    lblCodDespacho = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                      & " ALMACEN_CODIGO_DESTINO,CECOSE_CODIGO_DESTINO,PROVEEDOR_CODIGO_DESTINO,DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                      & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",'" & FechaServer & "','" & HoraServer & "','" & psUser & "','" & psTipoActual & "'," _
                                      & lblCodAlmacen & "," & lblCodCCosto & "," & lblCodProveedor & ",'2','0',1,1,0,1,'" & psCodAnti & "'," _
                                      & " '" & FechaServer & "','" & HoraServer & "','23','" & ValorSys & "')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','0',NULL,'23','N')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                'STOCK
                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                    & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs("SAA_STOCK_ACTUAL")) - 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                                              & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                  & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                  & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','2','" & psCodAnti & "','" & psTipoAnti & "', " _
                                  & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoActual & "','" & psCodActual & "')"
                CmdGlobal.ExecuteNonQuery()
                '--------------------------recepcion
                If psTipoActual = "2" Or psTipoActual = "1" Then     ' en ccosto O ALMACEN
                    CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND DESP_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='3',DESP_CANT_REC='1',DESP_CANT_FALT_REC='0' WHERE DESP_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                    CmdGlobal.ExecuteNonQuery()
                    'STOCK
                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                        & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                                                  & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                               & "VALUES(" & psCodActual & ",'" & psTipoActual & "'," & psCodArticulo & ",1,'0','" & psCodEmpresa & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()
                    'MOVIMIENTO GENERAL
                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblNroMovimiento = Nz(Rs(0)) + 1
                        End While
                    Else
                        lblNroMovimiento = 1
                    End If
                    Rs.Close()
                    CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                      & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & psTipoActual & "','" & psCodActual & "', " _
                                      & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoAnti & "','" & psCodAnti & "')"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='" & psTipoActual & "',UBICACT_CODIGO='" & psCodActual & "',UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR='" & psSerieNumerar & "'"
                    CmdGlobal.ExecuteNonQuery()
                    Guardar_UltimosMovimiento_paraGPS(psConexion, psCodEmpresa, 0, FechaActual, psTipoAnti, psCodAnti, psTipoActual, psCodActual, psSerieNumerar, psUser)
                    CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL)" _
                                      & " VALUES ('" & psSerieNumerar & "','" & psTipoActual & "','" & psCodActual & "','23','0','" & ValorSys & "','" & FechaServer & "','1','" & lblCodDespacho & "')"
                    CmdGlobal.ExecuteNonQuery()
                End If
            ElseIf psTipoAnti = "2" Then  'SALIDA DE CENTRO DE COSTO
                CmdGlobal.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & psCodEmpresa & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblCodDespacho = Nz(Rs(0)) + 1
                    End While
                Else
                    lblCodDespacho = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, ALMACEN_CODIGO_DESTINO, " _
                                      & " CECOSE_CODIGO_DESTINO, OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                      & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",'" & FechaServer & "','" & HoraServer & "','" & psUser & "','" & psTipoActual & "'," _
                                      & lblCodAlmacen & "," & lblCodCCosto & ",'2','0',1,0,1,'" & psCodAnti & "'," _
                                      & " '" & FechaServer & "','" & HoraServer & "','23')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO) " _
                                      & " VALUES('" & psCodEmpresa & "'," & lblCodDespacho & ",1," & psSerieNumerar & ",'S','N','0','23')"
                CmdGlobal.ExecuteNonQuery()
                CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & psSerieNumerar
                CmdGlobal.ExecuteNonQuery()
                'STOCK
                CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                    & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        StockAc = Nz(Rs("SAA_STOCK_ACTUAL")) - 1
                        CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodAnti & ") AND (UBICACT_TIPO='" & psTipoAnti & "') " _
                                              & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
                'MOVIMIENTO GENERAL
                CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        lblNroMovimiento = Nz(Rs(0)) + 1
                    End While
                Else
                    lblNroMovimiento = 1
                End If
                Rs.Close()
                CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                  & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                  & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','2','" & psTipoAnti & "','" & psCodAnti & "', " _
                                  & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoActual & "','" & psCodActual & "')"
                CmdGlobal.ExecuteNonQuery()
                '--------------------------recepcion
                If psTipoActual = "2" Or psTipoActual = "1" Then     ' en ccosto O ALMACEN
                    CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET  SET RECIBIDA_OK ='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO='M' WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND OSAL_CODIGO='" & lblCodDespacho & "' AND SERIE_NUMERAR =" & psSerieNumerar
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_CCOSTO_SALIDA  SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='3',OSAL_CANT_REC='1',OSAL_CANT_FALT_REC='0' WHERE OSAL_CODIGO='" & lblCodDespacho & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                    CmdGlobal.ExecuteNonQuery()
                    'STOCK
                    CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                        & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                            CmdGlobal2.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodActual & ") AND (UBICACT_TIPO='" & psTipoActual & "') " _
                                                  & " AND (ARTICULO_CODIGO = " & psCodArticulo & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
                            CmdGlobal2.ExecuteNonQuery()
                        End While
                    Else
                        CmdGlobal2.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                              & "VALUES(" & psCodActual & ",'" & psTipoActual & "'," & psCodArticulo & ",1,'0','" & psCodEmpresa & "')"
                        CmdGlobal2.ExecuteNonQuery()
                    End If
                    Rs.Close()
                    'MOVIMIENTO GENERAL
                    CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                    Rs = CmdGlobal.ExecuteReader
                    If Rs.HasRows Then
                        While Rs.Read
                            lblNroMovimiento = Nz(Rs(0)) + 1
                        End While
                    Else
                        lblNroMovimiento = 1
                    End If
                    Rs.Close()
                    CmdGlobal.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                      & " VALUES ('" & psCodEmpresa & "','" & lblNroMovimiento & "','1','" & psTipoActual & "','" & psCodActual & "', " _
                                      & " '" & psCodArticulo & "','1','" & ValorSys & "','3','23','" & FechaServer & "','0','" & lblCodDespacho & "','" & psTipoAnti & "','" & psCodAnti & "')"
                    CmdGlobal.ExecuteNonQuery()
                    CmdGlobal.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " SET UBICACT_TIPO='" & psTipoActual & "',UBICACT_CODIGO=" & psCodActual & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR='" & psSerieNumerar & "'"
                    CmdGlobal.ExecuteNonQuery()
                    Guardar_UltimosMovimiento_paraGPS(psConexion, psCodEmpresa, 0, FechaActual, psTipoAnti, psCodAnti, psTipoActual, psCodActual, psSerieNumerar, psUser)
                    CmdGlobal.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & psCodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL,MOTIVO)" _
                                      & " VALUES ('" & psSerieNumerar & "','" & psTipoActual & "','" & psCodActual & "','23','0','" & ValorSys & "','" & FechaServer & "','2','" & lblCodDespacho & "','23')"
                    CmdGlobal.ExecuteNonQuery()
                End If
            End If
        Catch ex As SqlException

        Catch ex As Exception

        End Try
    End Sub
    Function Lista_Articulos_xClasif(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                     ByVal psUser As String) As String
        Lista_Articulos_xClasif = ""
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader

        Dim CodNivel As String = ""
        Dim CodClas As String = ""
        Dim CodArt As String = ""
        Dim i As Integer = 0
        Dim j As Integer = 0

        Try
            Cn.Open() : Cn2.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CodClas = ""

            CmdGlobal.CommandText = " SELECT RELACION_COD_CLASIF from dbo.TBINV_RELACION_USUARIO_CLASIFICACION WHERE (RELACION_COD_USUARIO = '" & psUser & "') AND (EMPRESA_CODIGO = '" & psCodEmpresa & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " SELECT CLAS_COD_NIVEL From dbo.TBINV_ARTICULO_CLASIFICACION WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND CLAS_CODIGO = '" & Nu(Rs("RELACION_COD_CLASIF")) & "'"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CodNivel = Nu(Rs2("CLAS_COD_NIVEL"))
                        End While
                    End If
                    Rs2.Close()
                    CmdGlobal2.CommandText = " SELECT CLAS_CODIGO From dbo.TBINV_ARTICULO_CLASIFICACION WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND CLAS_NIVEL" & CodNivel & " ='" & Nu(Rs("RELACION_COD_CLASIF")) & "'"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            If CodClas <> "" Then CodClas = CodClas & ","
                            CodClas = CodClas & Nu(Rs2("CLAS_CODIGO"))
                        End While
                    End If
                    Rs2.Close()
                End While
            End If
            Rs.Close()
            CodArt = ""

            If CodClas <> "" Then '
                CmdGlobal.CommandText = " SELECT DISTINCT ART_CODIGO From dbo.TBINV_ARTICULOS WHERE (ART_CLASIFICACION IN (" & CodClas & ")) AND (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (ART_SYS_EST = '0') order by art_codigo"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        If CodArt <> "" Then CodArt = CodArt & ","
                        CodArt = CodArt & Nu(Rs("ART_CODIGO"))
                    End While
                End If
                Rs.Close()
            End If
            Lista_Articulos_xClasif = CodArt

        Catch ex As SqlException
            Lista_Articulos_xClasif = "En la función Lista_Articulos_xClasif ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            Lista_Articulos_xClasif = "En la función Lista_Articulos_xClasif ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Function
    Function Verificar_ArtExiste(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                 ByVal psUser As String, ByVal psCodArt As String) As String
        Verificar_ArtExiste = "NO"
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand

        Dim CodNivel As String = ""
        Dim CodClas As String = ""
        Dim CodArt As String = ""

        Dim i As Integer = 0
        Dim j As Integer = 0

        CodClas = ""
        Try
            Cn.Open() : Cn2.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2
            CmdGlobal.CommandText = " SELECT RELACION_COD_CLASIF from dbo.TBINV_RELACION_USUARIO_CLASIFICACION " _
                                  & " WHERE (RELACION_COD_USUARIO = '" & psUser & "') AND " _
                                  & " (EMPRESA_CODIGO = '" & psCodEmpresa & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " SELECT CLAS_COD_NIVEL From dbo.TBINV_ARTICULO_CLASIFICACION " _
                        & " WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND " _
                        & " CLAS_CODIGO = '" & Nu(Rs("RELACION_COD_CLASIF")) & "'"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CodNivel = Nu(Rs2("CLAS_COD_NIVEL"))
                        End While
                    End If
                    Rs2.Close()
                    CmdGlobal2.CommandText = " SELECT CLAS_CODIGO From dbo.TBINV_ARTICULO_CLASIFICACION " _
                        & " WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND " _
                        & " CLAS_NIVEL" & CodNivel & " ='" & Nu(Rs("RELACION_COD_CLASIF")) & "'"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            If CodClas <> "" Then CodClas = CodClas & ","
                            CodClas = CodClas & Nu(Rs2("CLAS_CODIGO"))
                        End While
                    End If
                    Rs2.Close()
                End While
            End If
            Rs.Close()
            CodArt = ""
            If CodClas <> "" Then
                CmdGlobal.CommandText = " SELECT DISTINCT ART_CODIGO From dbo.TBINV_ARTICULOS " _
                    & " WHERE (ART_CLASIFICACION IN (" & CodClas & ")) AND " _
                    & " (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (ART_SYS_EST = '0') AND " _
                    & " (ART_CODIGO = " & psCodArt & ")"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        Verificar_ArtExiste = "SI"
                    End While
                End If
                Rs.Close()
            Else
                Verificar_ArtExiste = "NO"
            End If
        Catch ex As SqlException
            Verificar_ArtExiste = "En la función Verificar_ArtExiste ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            Verificar_ArtExiste = "En la función Verificar_ArtExiste ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Function
    Function Grabar_ArtxUsuario(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                  ByVal psUser As String, ByVal psCodArt As String) As String
        Grabar_ArtxUsuario = ""
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand

        Dim CodNivel As String = ""
        Dim CodClas As String = ""
        Dim CodArt As String = ""
        Dim i As Integer = 0
        Dim j As Integer = 0

        Try
            Cn.Open() : Cn2.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2

            CmdGlobal.CommandText = " SELECT RELACION_COD_CLASIF from dbo.TBINV_RELACION_USUARIO_CLASIFICACION WHERE (RELACION_COD_USUARIO = '" & psUser & "') AND (EMPRESA_CODIGO = '" & psCodEmpresa & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    CmdGlobal2.CommandText = " SELECT CLAS_COD_NIVEL From dbo.TBINV_ARTICULO_CLASIFICACION WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND CLAS_CODIGO = '" & Nu(Rs("RELACION_COD_CLASIF")) & "'"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            CodNivel = Nu(Rs2("CLAS_COD_NIVEL"))
                        End While
                    End If
                    Rs2.Close()
                    CmdGlobal2.CommandText = " SELECT CLAS_CODIGO From dbo.TBINV_ARTICULO_CLASIFICACION WHERE EMPRESA_CODIGO='" & psCodEmpresa & "' AND CLAS_NIVEL" & CodNivel & " ='" & Nu(Rs("RELACION_COD_CLASIF")) & "'"
                    Rs2 = CmdGlobal2.ExecuteReader
                    If Rs2.HasRows Then
                        While Rs2.Read
                            If CodClas <> "" Then CodClas = CodClas & ","
                            CodClas = CodClas & Nu(Rs2("CLAS_CODIGO"))
                        End While
                    End If
                    Rs2.Close()
                End While
            End If
            Rs.Close()
            If Existe_Tabla("V_ARTXUSUARIO", psConexion) = False Then
                CmdGlobal.CommandText = " CREATE TABLE [dbo].[V_ARTXUSUARIO] (" _
                                      & " [ART_CODIGO] [FLOAT] NOT NULL) ON [PRIMARY]"
                CmdGlobal.ExecuteNonQuery()
            End If
            CmdGlobal.CommandText = "delete from V_ARTXUSUARIO"
            CmdGlobal.ExecuteNonQuery()
            If CodClas <> "" Then
                CmdGlobal.CommandText = " SELECT DISTINCT ART_CODIGO From dbo.TBINV_ARTICULOS " _
                    & " WHERE (ART_CLASIFICACION IN (" & CodClas & ")) " _
                    & " AND (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (ART_SYS_EST = '0')"
                If psCodArt <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ART_CODIGO = " & psCodArt & ""
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        CmdGlobal2.CommandText = " INSERT INTO V_ARTXUSUARIO (ART_CODIGO) VALUES (" & Nu(Rs("ART_CODIGO")) & ") "
                        CmdGlobal2.ExecuteNonQuery()
                    End While
                End If
                Rs.Close()
            End If
            Grabar_ArtxUsuario = ""
        Catch ex As SqlException
            Grabar_ArtxUsuario = "En la función Grabar_ArtxUsuario ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            Grabar_ArtxUsuario = "En la función Grabar_ArtxUsuario ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Function
    Function Crear_Vista_Movimiento(ByVal psConexion As String, ByVal psCodEmpresa As String,
                                    ByVal psTipoOrigen As String, ByVal psCodOrigen As String,
                                    ByVal psTipoDestino As String, ByVal psCodDestino As String) As String
        Crear_Vista_Movimiento = ""
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Rs As SqlDataReader
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim psCodSeccionO As String = ""
        Dim psCodSeccionD As String = ""
        Try
            Cn.Open() : Cn2.Open()
            CmdGlobal.Connection = Cn : CmdGlobal2.Connection = Cn2

            If psTipoOrigen = "5" And psCodOrigen <> "" Then
                psCodSeccionO = ""
                CmdGlobal.CommandText = " SELECT CECOSE_CODIGO FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CCOSTO_CODIGO=" & psCodOrigen & " AND CECOSE_SYS_EST='0' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        If psCodSeccionO <> "" Then psCodSeccionO = psCodSeccionO & ","
                        psCodSeccionO = psCodSeccionO & Nu(Rs("CECOSE_CODIGO"))
                    End While
                End If
                Rs.Close()
            End If

            If psTipoDestino = "5" And psCodDestino <> "" Then
                psCodSeccionD = ""
                CmdGlobal.CommandText = " SELECT CECOSE_CODIGO FROM TBLOGIS_CENTRO_COSTO_SECCION WHERE CCOSTO_CODIGO=" & psCodDestino & " AND CECOSE_SYS_EST='0' AND EMPRESA_CODIGO='" & psCodEmpresa & "'"
                Rs = CmdGlobal.ExecuteReader
                If Rs.HasRows Then
                    While Rs.Read
                        If psCodSeccionD <> "" Then psCodSeccionD = psCodSeccionD & ","
                        psCodSeccionD = psCodSeccionD & Nu(Rs("CECOSE_CODIGO"))
                    End While
                End If
                Rs.Close()
            End If

            CmdGlobal.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_SalAlmacen]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_SalAlmacen]"
            CmdGlobal.ExecuteNonQuery() ' INNER JOIN V_ARTXUSUARIO VA ON VA.ART_CODIGO = S.ARTICULO_CODIGO
            CmdGlobal.CommandText = " CREATE VIEW V_SalAlmacen AS SELECT D.DESP_CODIGO AS SALIDA, '1' AS TIPO_ORIGEN, RIGHT('000' + CONVERT(VARCHAR(5), D.ALMACEN_ORIGEN), 3) AS ORIGEN_CODALM, " _
                                  & " (SELECT AL.ALMACEN_NOMBRE FROM TBINV_ALMACENES AL WHERE AL.ALMACEN_CODIGO = ALMACEN_ORIGEN AND AL.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND ALMACEN_SYS_EST = '0')  AS ORIGEN_ALM," _
                                  & " '' AS ORIGEN_SEC, '' AS ORIGEN_CODSEC, '' AS ORIGEN_PROV, '' AS ORIGEN_CODPROV, " _
                                  & " (SELECT MARCA_DESCRIPCION FROM TBINV_ARTICULOS_SERIES_MARCA WHERE SERIE_MARCA=MARCA_CODIGO AND MARCA_SYS_EST='0' AND D.EMPRESA_CODIGO=EMPRESA_CODIGO) AS MARCA, " _
                                  & " (SELECT MODELO_DESCRIPCION FROM TBINV_ARTICULOS_SERIES_MODELO WHERE SERIE_MODELO=MODELO_CODIGO AND MODELO_SYS_EST='0' AND D.EMPRESA_CODIGO=EMPRESA_CODIGO) AS MODELO, " _
                                  & " DD.SERIE_NUMERAR, D.DESP_FECHA_SAL AS FECHA, D.DESP_HORA_SAL AS HORA, D.DESP_ESTADO AS ESTADO, S.SERIE_NRO, D.DESP_TIPODESTINO AS TIPODESTINO, " _
                                  & " (SELECT CECOSE_DESCRIPCION FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = D.CECOSE_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '2')) AS DESTINO_SEC," _
                                  & " (SELECT CECOSE_COD_INTERNO FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = D.CECOSE_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '2')) AS DESTINO_CODSEC," _
                                  & " (SELECT PERSONA_RAZON_SOCIAL FROM dbo.TBDATA_PERSONAS AS S WHERE (PERSONA_CODIGO = D.PROVEEDOR_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (PERSONA_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '3') AND (PERSONA_TIPO = '2')) AS DESTINO_PROV," _
                                  & " (SELECT PERSONA_RUC FROM dbo.TBDATA_PERSONAS AS S WHERE (PERSONA_CODIGO = D.PROVEEDOR_CODIGO_DESTINO) AND (D.EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (PERSONA_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '3') AND (PERSONA_TIPO = '2')) AS DESTINO_CODPROV," _
                                  & " (SELECT ALMACEN_NOMBRE FROM Dbo.TBINV_ALMACENES AS S WHERE (ALMACEN_CODIGO = D.ALMACEN_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '1')) AS DESTINO_ALM," _
                                  & " (SELECT RIGHT('000' + CONVERT(VARCHAR(5), ALMACEN_CODIGO), 3) FROM dbo.TBINV_ALMACENES AS AL WHERE (ALMACEN_CODIGO = D.ALMACEN_CODIGO_DESTINO) AND (D.EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '1')) AS DESTINO_CODALM, S.PLACA_NRO, S.ARTICULO_CODIGO, 'Salida' AS TIPO_MOV " _
                                  & " FROM dbo.TBINV_ALMACEN_DESPACHO D INNER JOIN dbo.TBINV_ALMACEN_DESPACHO_DET DD ON D.DESP_CODIGO = DD.DESP_CODIGO AND D.EMPRESA_CODIGO = DD.EMPRESA_CODIGO INNER JOIN " _
                                  & " dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR INNER JOIN V_ARTXUSUARIO VA ON VA.ART_CODIGO = S.ARTICULO_CODIGO " _
                                  & " WHERE (D.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (D.DESP_SYS_EST = '0') AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DD.DESPD_SYS_EST = '0') AND" _
                                  & " NOT(S.SERIE_ESTADO IN ('1','6')) AND (S.SERIE_SYS_EST = '0') AND (S.SERIE_PLACABILIDAD = 'S') AND (DESP_ESTADO='3') "
            If psTipoOrigen = "1" And psCodOrigen <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_ORIGEN = " & psCodOrigen & ""
            If psTipoOrigen <> "1" And psTipoOrigen <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_ORIGEN = 0"
            If psTipoDestino <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND DESP_TIPODESTINO = '" & psTipoDestino & "'"
            If psCodDestino <> "" Then
                If psTipoDestino = "1" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_CODIGO_DESTINO = " & psCodDestino
                If psTipoDestino = "2" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND CECOSE_CODIGO_DESTINO = " & psCodDestino
                If psTipoDestino = "3" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND PROVEEDOR_CODIGO_DESTINO = " & psCodDestino
            End If
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_RecepAlmacen]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_RecepAlmacen]"
            CmdGlobal.ExecuteNonQuery() 'INNER JOIN V_ARTXUSUARIO VA ON VA.ART_CODIGO = S.ARTICULO_CODIGO
            CmdGlobal.CommandText = " CREATE VIEW V_RecepAlmacen AS SELECT D.DESP_CODIGO AS SALIDA, DESP_TIPODESTINO AS TIPO_ORIGEN, " _
                                  & " (SELECT RIGHT('000' + CONVERT(VARCHAR(5), ALMACEN_CODIGO), 3) FROM dbo.TBINV_ALMACENES AS AL WHERE (ALMACEN_CODIGO = D.ALMACEN_CODIGO_DESTINO) AND (D.EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '1')) AS ORIGEN_CODALM,  " _
                                  & " (SELECT ALMACEN_NOMBRE FROM Dbo.TBINV_ALMACENES AS S WHERE (ALMACEN_CODIGO = D.ALMACEN_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '1')) AS ORIGEN_ALM, " _
                                  & " (SELECT CECOSE_COD_INTERNO FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = D.CECOSE_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '2')) AS ORIGEN_CODSEC, " _
                                  & " (SELECT CECOSE_DESCRIPCION FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = D.CECOSE_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '2')) AS ORIGEN_SEC, " _
                                  & " (SELECT PERSONA_RUC FROM dbo.TBDATA_PERSONAS AS S WHERE (PERSONA_CODIGO = D.PROVEEDOR_CODIGO_DESTINO) AND (D.EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (PERSONA_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '3') AND (PERSONA_TIPO = '2')) AS ORIGEN_CODPROV,  " _
                                  & " (SELECT PERSONA_RAZON_SOCIAL FROM dbo.TBDATA_PERSONAS AS S WHERE (PERSONA_CODIGO = D.PROVEEDOR_CODIGO_DESTINO) AND (EMPRESA_CODIGO = D.EMPRESA_CODIGO) AND (PERSONA_SYS_EST = '0') AND (D.DESP_TIPODESTINO = '3') AND (PERSONA_TIPO = '2')) AS ORIGEN_PROV, " _
                                  & " (SELECT MARCA_DESCRIPCION FROM TBINV_ARTICULOS_SERIES_MARCA WHERE SERIE_MARCA=MARCA_CODIGO AND MARCA_SYS_EST='0' AND D.EMPRESA_CODIGO=EMPRESA_CODIGO) AS MARCA, " _
                                  & " (SELECT MODELO_DESCRIPCION FROM TBINV_ARTICULOS_SERIES_MODELO WHERE SERIE_MODELO=MODELO_CODIGO AND MODELO_SYS_EST='0' AND D.EMPRESA_CODIGO=EMPRESA_CODIGO) AS MODELO, " _
                                  & " DD.SERIE_NUMERAR, SUBSTRING(D.DESP_SYS_REC,1,8) AS FECHA, SUBSTRING(D.DESP_SYS_REC,9,4) AS HORA, D.DESP_ESTADO AS ESTADO, S.SERIE_NRO, '1' AS TIPODESTINO, " _
                                  & " '' AS DESTINO_SEC, '' AS DESTINO_CODSEC, '' AS DESTINO_PROV, '' AS DESTINO_CODPROV, " _
                                  & " (SELECT AL.ALMACEN_NOMBRE FROM TBINV_ALMACENES AL WHERE AL.ALMACEN_CODIGO = ALMACEN_ORIGEN AND AL.EMPRESA_CODIGO = D.EMPRESA_CODIGO AND ALMACEN_SYS_EST = '0') AS DESTINO_ALM," _
                                  & " RIGHT('000' + CONVERT(VARCHAR(5), D.ALMACEN_ORIGEN), 3) AS DESTINO_CODALM, " _
                                  & " S.PLACA_NRO, S.ARTICULO_CODIGO, 'Ingreso' AS TIPO_MOV " _
                                  & " FROM dbo.TBINV_ALMACEN_DESPACHO D INNER JOIN dbo.TBINV_ALMACEN_DESPACHO_DET DD ON D.DESP_CODIGO = DD.DESP_CODIGO AND D.EMPRESA_CODIGO = DD.EMPRESA_CODIGO INNER JOIN " _
                                  & " dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S ON DD.SERIE_NUMERAR = S.SERIE_NUMERAR  INNER JOIN V_ARTXUSUARIO VA ON VA.ART_CODIGO = S.ARTICULO_CODIGO " _
                                  & " WHERE (D.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (D.DESP_SYS_EST = '0') AND (DD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (DD.DESPD_SYS_EST = '0') AND" _
                                  & " NOT(S.SERIE_ESTADO IN ('1','6')) AND (S.SERIE_SYS_EST = '0') AND (S.SERIE_PLACABILIDAD = 'S') AND (DESP_ESTADO='3') "
            If psTipoDestino = "1" And psCodDestino <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_ORIGEN = " & psCodDestino & ""
            If psTipoDestino <> "1" And psTipoDestino <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_ORIGEN = 0"
            If psTipoDestino <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND DESP_TIPODESTINO = '" & psTipoDestino & "'"
            If psCodOrigen <> "" Then
                If psTipoOrigen = "1" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_CODIGO_DESTINO = " & psCodOrigen
                If psTipoOrigen = "2" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND CECOSE_CODIGO_DESTINO = " & psCodOrigen
                If psTipoOrigen = "3" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND PROVEEDOR_CODIGO_DESTINO = " & psCodOrigen
            End If
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_SalSeccion]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_SalSeccion]"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " CREATE VIEW V_SalSeccion AS SELECT OSAL.OSAL_CODIGO AS SALIDA,'2' AS TIPO_ORIGEN , '' AS ORIGEN_CODALM, '' AS ORIGEN_ALM, " _
                                  & " (SELECT CECOSE_COD_INTERNO FROM TBLOGIS_CENTRO_COSTO_SECCION CC WHERE CC.EMPRESA_CODIGO = OSAL.EMPRESA_CODIGO AND CC.CECOSE_SYS_EST = '0' AND CC.CECOSE_CODIGO = OSAL.CECOSE_CODIGO_ORIGEN) AS ORIGEN_CODSEC, " _
                                  & " (SELECT CECOSE_DESCRIPCION FROM TBLOGIS_CENTRO_COSTO_SECCION CC WHERE CC.EMPRESA_CODIGO = OSAL.EMPRESA_CODIGO AND CC.CECOSE_SYS_EST = '0' AND CC.CECOSE_CODIGO = OSAL.CECOSE_CODIGO_ORIGEN)  AS ORIGEN_SEC," _
                                  & " '' AS ORIGEN_CODPROV, '' AS ORIGEN_PROV, " _
                                  & " (SELECT MARCA_DESCRIPCION FROM TBINV_ARTICULOS_SERIES_MARCA WHERE SERIE_MARCA=MARCA_CODIGO AND MARCA_SYS_EST='0' AND OSAL.EMPRESA_CODIGO=EMPRESA_CODIGO) AS MARCA, " _
                                  & " (SELECT MODELO_DESCRIPCION FROM TBINV_ARTICULOS_SERIES_MODELO WHERE SERIE_MODELO=MODELO_CODIGO AND MODELO_SYS_EST='0' AND OSAL.EMPRESA_CODIGO=EMPRESA_CODIGO) AS MODELO, " _
                                  & " S.SERIE_NUMERAR, OSAL.OSAL_FECHA_SAL AS FECHA,OSAL.OSAL_HORA_SAL AS HORA, OSAL.OSAL_ESTADO AS ESTADO, S.SERIE_NRO, OSAL.OSAL_TIPODESTINO AS TIPODESTINO, " _
                                  & " (SELECT CECOSE_DESCRIPCION FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = OSAL.CECOSE_CODIGO_DESTINO) AND (EMPRESA_CODIGO = OSAL.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (OSAL.OSAL_TIPODESTINO = '2')) AS DESTINO_SEC, " _
                                  & " (SELECT CECOSE_COD_INTERNO FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = OSAL.CECOSE_CODIGO_DESTINO) AND (EMPRESA_CODIGO = OSAL.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (OSAL.OSAL_TIPODESTINO = '2')) AS DESTINO_CODSEC, " _
                                  & " '' AS DESTINO_PROV, '' AS DESTINO_CODPROV, " _
                                  & " (SELECT ALMACEN_NOMBRE FROM dbo.TBINV_ALMACENES AS S WHERE (ALMACEN_CODIGO = OSAL.ALMACEN_CODIGO_DESTINO) AND (EMPRESA_CODIGO = OSAL.EMPRESA_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (OSAL.OSAL_TIPODESTINO = '1')) AS DESTINO_ALM, " _
                                  & " (SELECT RIGHT('000' + CONVERT(VARCHAR(5), ALMACEN_CODIGO), 3) FROM dbo.TBINV_ALMACENES AS AL WHERE (ALMACEN_CODIGO = OSAL.ALMACEN_CODIGO_DESTINO) AND (EMPRESA_CODIGO = OSAL.EMPRESA_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (OSAL.OSAL_TIPODESTINO = '1')) AS DESTINO_CODALM, S.PLACA_NRO, S.ARTICULO_CODIGO, 'Salida' AS TIPO_MOV " _
                                  & " FROM dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " S INNER JOIN dbo.TBINV_CCOSTO_SALIDA_DET OSALD ON S.SERIE_NUMERAR = OSALD.SERIE_NUMERAR INNER JOIN dbo.TBINV_CCOSTO_SALIDA OSAL " _
                                  & " ON OSALD.EMPRESA_CODIGO = OSAL.EMPRESA_CODIGO AND OSALD.OSAL_CODIGO = OSAL.OSAL_CODIGO INNER JOIN V_ARTXUSUARIO VA ON VA.ART_CODIGO = S.ARTICULO_CODIGO " _
                                  & " WHERE (OSAL.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (OSAL.OSAL_SYS_EST = '0') AND (OSALD.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (OSALD.OSALD_SYS_EST = '0') AND NOT(S.SERIE_ESTADO IN ('1','6')) " _
                                  & " AND (S.SERIE_SYS_EST = '0') AND (S.SERIE_PLACABILIDAD = 'S') AND OSAL_ESTADO='3'"
            If psTipoOrigen = "2" And psCodOrigen <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND CECOSE_CODIGO_ORIGEN=" & psCodOrigen & ""
            If psTipoOrigen <> "2" And psTipoOrigen <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND CECOSE_CODIGO_ORIGEN=0"
            If psTipoDestino <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND OSAL_TIPODESTINO = '" & psTipoDestino & "'"
            If psCodDestino <> "" Then
                If psTipoDestino = "1" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_CODIGO_DESTINO = " & psCodDestino
                If psTipoDestino = "2" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND CECOSE_CODIGO_DESTINO = " & psCodDestino
                If psTipoDestino = "3" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND CECOSE_CODIGO_DESTINO = 0"
            End If
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_Recepcion_Mov]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_Recepcion_Mov]"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " CREATE VIEW V_Recepcion_Mov AS SELECT R.RECEP_CODIGO AS SALIDA,'3' AS TIPO_ORIGEN ,'' AS ORIGEN_CODALM, '' AS ORIGEN_ALM," _
                                  & " '' AS ORIGEN_CODSEC, '' AS ORIGEN_SEC, P.PERSONA_RUC AS ORIGEN_CODPROV, P.PERSONA_RAZON_SOCIAL AS ORIGEN_PROV, '' AS MARCA, " _
                                  & " '' AS MODELO, S.SERIE_NUMERAR, R.RECEP_FEC_EMI_DOC AS FECHA, RECEP_HORA_REG AS HORA, R.RECEP_ESTADO AS ESTADO, S.SERIE_NRO, R.RECEP_TIPODESTINO AS TIPODESTINO," _
                                  & " (SELECT CECOSE_DESCRIPCION FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = R.ALMACEN_CODIGO) AND (EMPRESA_CODIGO = R.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (R.RECEP_TIPODESTINO = '2')) AS DESTINO_SEC," _
                                  & " (SELECT CECOSE_COD_INTERNO FROM dbo.TBLOGIS_CENTRO_COSTO_SECCION AS S WHERE (CECOSE_CODIGO = R.ALMACEN_CODIGO) AND (EMPRESA_CODIGO = R.EMPRESA_CODIGO) AND (CECOSE_SYS_EST = '0') AND (R.RECEP_TIPODESTINO = '2')) AS DESTINO_CODSEC, " _
                                  & " '' AS DESTINO_PROV, '' AS DESTINO_CODPROV," _
                                  & " (SELECT ALMACEN_NOMBRE FROM dbo.TBINV_ALMACENES AS ALM WHERE (ALMACEN_CODIGO = R.ALMACEN_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (EMPRESA_CODIGO = R.EMPRESA_CODIGO) AND (R.RECEP_TIPODESTINO = '1')) AS DESTINO_ALM," _
                                  & " (SELECT RIGHT('000' + CONVERT(VARCHAR(5), ALMACEN_CODIGO), 3) FROM dbo.TBINV_ALMACENES AS ALM WHERE (ALMACEN_CODIGO = R.ALMACEN_CODIGO) AND (ALMACEN_SYS_EST = '0') AND (EMPRESA_CODIGO = R.EMPRESA_CODIGO) AND (R.RECEP_TIPODESTINO = '1')) AS DESTINO_CODALM, " _
                                  & " S.PLACA_NRO, S.ARTICULO_CODIGO, 'Ingreso' AS TIPO_MOV " _
                                  & " FROM dbo.TBINV_ALMACEN_RECEPCION AS R INNER JOIN dbo.TBINV_ARTICULOS_SERIES_" & psCodEmpresa & " AS S ON R.RECEP_CODIGO = S.RECEP_CODIGO " _
                                  & " INNER JOIN dbo.TBDATA_PERSONAS AS P ON R.RECEP_PROVEEDOR = P.PERSONA_CODIGO" _
                                  & " WHERE (R.RECEP_ESTADO = '2') AND (P.PERSONA_TIPO = '2') AND (R.RECEP_SYS_EST = '0') AND (R.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND " _
                                  & " (P.PERSONA_SYS_EST = '0') AND (P.EMPRESA_CODIGO = '" & psCodEmpresa & "') AND (S.SERIE_SYS_EST = '0')"
            If psTipoOrigen = "3" And psCodOrigen <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND RECEP_PROVEEDOR=" & psCodDestino & ""
            If psTipoOrigen <> "3" And psTipoOrigen <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND RECEP_PROVEEDOR=0"
            If psTipoDestino <> "" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND RECEP_TIPODESTINO = '" & psTipoDestino & "'"
            If psCodDestino <> "" Then
                If psTipoDestino = "1" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_CODIGO = " & psCodDestino
                If psTipoDestino = "2" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_CODIGO = " & psCodDestino
                If psTipoDestino = "3" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND ALMACEN_CODIGO = 0"
            End If
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = "if exists (select * from sysobjects where id = object_id(N'[dbo].[V_UNION_C]') and OBJECTPROPERTY(id, N'IsView') = 1) drop view [dbo].[V_UNION_C]"
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " CREATE VIEW V_UNION_C AS SELECT SALIDA, TIPO_ORIGEN, ORIGEN_CODALM, ORIGEN_ALM, ORIGEN_CODSEC, ORIGEN_SEC, ORIGEN_PROV, ORIGEN_CODPROV, SERIE_NUMERAR, FECHA, HORA, ESTADO,MARCA,MODELO,PLACA_NRO,SERIE_NRO,TIPODESTINO,DESTINO_SEC,DESTINO_CODSEC,DESTINO_PROV,DESTINO_CODPROV,DESTINO_ALM,DESTINO_CODALM,ARTICULO_CODIGO,TIPO_MOV From dbo.V_SALALMACEN " _
                                  & "                Union All SELECT SALIDA, TIPO_ORIGEN, ORIGEN_CODALM, ORIGEN_ALM, ORIGEN_CODSEC, ORIGEN_SEC, ORIGEN_PROV, ORIGEN_CODPROV, SERIE_NUMERAR, FECHA, HORA, ESTADO,MARCA,MODELO,PLACA_NRO,SERIE_NRO,TIPODESTINO,DESTINO_SEC,DESTINO_CODSEC,DESTINO_PROV,DESTINO_CODPROV,DESTINO_ALM,DESTINO_CODALM,ARTICULO_CODIGO,TIPO_MOV FROM dbo.V_SALSECCION " _
                                  & "                Union All SELECT SALIDA, TIPO_ORIGEN, ORIGEN_CODALM, ORIGEN_ALM, ORIGEN_CODSEC, ORIGEN_SEC, ORIGEN_PROV, ORIGEN_CODPROV, SERIE_NUMERAR, FECHA, HORA, ESTADO,MARCA,MODELO,PLACA_NRO,SERIE_NRO,TIPODESTINO,DESTINO_SEC,DESTINO_CODSEC,DESTINO_PROV,DESTINO_CODPROV,DESTINO_ALM,DESTINO_CODALM,ARTICULO_CODIGO,TIPO_MOV FROM dbo.V_Recepcion_Mov " _
                                  & "                Union All SELECT SALIDA, TIPO_ORIGEN, ORIGEN_CODALM, ORIGEN_ALM, ORIGEN_CODSEC, ORIGEN_SEC, ORIGEN_PROV, ORIGEN_CODPROV, SERIE_NUMERAR, FECHA, HORA, ESTADO,MARCA,MODELO,PLACA_NRO,SERIE_NRO,TIPODESTINO,DESTINO_SEC,DESTINO_CODSEC,DESTINO_PROV,DESTINO_CODPROV,DESTINO_ALM,DESTINO_CODALM,ARTICULO_CODIGO,TIPO_MOV FROM dbo.V_RecepAlmacen "
            CmdGlobal.ExecuteNonQuery()
            Crear_Vista_Movimiento = ""
        Catch ex As SqlException
            Crear_Vista_Movimiento = "En la función Crear_Vista_Movimiento ha ocurrido un error en la base de datos: " & ex.Message
        Catch ex As Exception
            Crear_Vista_Movimiento = "En la función Crear_Vista_Movimiento ha ocurrido un error en la aplicación: " & ex.Message
        End Try
    End Function

    Public Sub Carga_Tabla_Info_Inv(ByVal psCodTabla As String, ByVal Ddl As DropDownList, ByVal psConexion As String, ByVal psCodEmpresa As String)
        Dim Cn As New SqlConnection(psConexion)
        Ddl.Items.Clear()
        Try
            Cn.Open()
            Dim Sql As String = " SELECT ELEMENTO_CODUNICO,ELEMENTO_CODIGO,ELEMENTO_DESCRIPCION,TABLA_CODIGO " _
                              & " FROM TBINV_TABLAS_INFO WHERE (ELEMENTO_SYS_EST = '0') AND (TABLA_CODIGO = '" & psCodTabla & "') AND (EMPRESA_CODIGO='" & psCodEmpresa & "')"
            'If QueCampoLLenar = "" Then Sql = Sql & " ORDER BY ELEMENTO_ORDEN" Else Sql = Sql & " ORDER BY ELEMENTO_CODIGO"
            Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
            Ddl.DataSource = cmdSql.ExecuteReader
            Ddl.DataTextField = "ELEMENTO_DESCRIPCION"
            Ddl.DataValueField = "ELEMENTO_CODUNICO"
            Ddl.DataBind()
            Ddl.Items.Add("< Seleccionar >") : Ddl.SelectedValue = "< Seleccionar >"
        Catch Ex As SqlClient.SqlException
        Catch Ex As Exception
        Finally
            Cn.Close()
        End Try
    End Sub
    Public Function Ingreso_NuevoEquipo(ByVal psConexion As String, ByVal f_psCodEmpresa As String, ByVal f_psCodArt As String,
                                        ByVal f_psNroSerie As String, ByVal f_psNroPlaca As String, ByVal f_psUbicacion As String,
                                        ByVal f_Fecha As String, f_DestinoTipo As String, ByVal f_DestinoCodigo As String,
                                        ByVal f_User As String, ByVal f_psEstEquipo As String, ByVal f_Obs As String) As String
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim CmdGlobal2 As New SqlCommand
        Dim CmdGlobal3 As New SqlCommand
        Dim Rs As SqlDataReader
        Dim psSerieNumerar2 As String = ""
        Dim ValorSys As String = ""
        Dim psCodRecep As String = ""
        Dim lblNroMovimiento As String = ""
        Dim StockAc As Double = 0
        Dim cant As Double = 0
        Ingreso_NuevoEquipo = ""
        StockAc = 0
        ValorSys = ""
        ValorSys = f_User & FechaActual() & HoraActual()
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            Cn2.Open() : CmdGlobal2.Connection = Cn2
            Cn3.Open() : CmdGlobal3.Connection = Cn3
            cant = 1

            CmdGlobal.CommandText = "SELECT Max(Recep_codigo) FROM TBINV_ALMACEN_RECEPCION "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psCodRecep = Nz(Rs(0)) + 1
                End While
            Else
                psCodRecep = 1
            End If
            Rs.Close()

            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION(EMPRESA_CODIGO, RECEP_CODIGO, ALMACEN_CODIGO,   " _
                                  & " RECEP_FEC_EMI_DOC, RECEP_FECHA_REG, RECEP_HORA_REG, RECEP_USUARIO_REG,  RECEP_NRO_ITEM, RECEP_ESTADO, " _
                                  & " RECEP_SYS_EST, RECEP_SYS_CRE,RECEP_CANT_XREC,RECEP_CANT_REC,RECEP_CANT_FALT_REC,RECEP_CANT_SOBR,RECEP_DESDE_OCOMPRA,RECEP_MOTIVO_GRAL,RECEP_NRO_OC,RECEP_ESTADO_CEPRO,  RECEP_TIPODESTINO) " _
                                  & " VALUES('" & f_psCodEmpresa & "'," & psCodRecep & "," & f_DestinoCodigo & ", " _
                                  & " '" & FechaActual() & "','" & FechaActual() & "','" & HoraActual() & "','" & f_User & "',1,'2'," _
                                  & " '0','" & ValorSys & "'," & cant & "," & cant & ",0,0,'N','20','','1', '" & f_DestinoTipo & "')"
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = " INSERT INTO TBINV_ALMACEN_RECEPCION_DET( EMPRESA_CODIGO, RECEP_CODIGO, RECEPD_ITEM, ARTICULO_CODIGO, RECEPD_CANT_XREC, RECEPD_CANT_REC," _
                                  & " RECEPD_CANT_FALT_REC,RECEPD_CANT_SOBR, RECEPD_ESTADO, RECEPD_SYS_EST,RECEPD_MOTIVO,RECEPD_INGRESAR_SERIE) " _
                                  & " VALUES('" & f_psCodEmpresa & "'," & psCodRecep & ",'1'," & f_psCodArt & ",'" & cant & "','" & cant & "'," _
                                  & " 0,0,'2','0','20','N')"
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = "SELECT MAX(SERIE_NUMERAR) FROM TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    psSerieNumerar2 = Nz(Rs(0)) + 1
                End While
            Else
                psSerieNumerar2 = 1
            End If
            Rs.Close()
            Ingreso_NuevoEquipo = psSerieNumerar2
            CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " (SERIE_NUMERAR, RECEP_CODIGO, ARTICULO_CODIGO, SERIE_NRO, SERIE_SOBRANTE," _
                                  & " UBICACT_TIPO, UBICACT_CODIGO, UBICACT_SYS, SERIE_SYS_CRE, SERIE_SYS_EST, SERIE_NUEVO, ALTIBI_CODIGO, SERIE_INGRESO,PROVEEDOR, SERIE_ESTADO, SERIE_ESTADO_EQUIPO, SERIE_CUSTODIA_FECHAFIN, PLACA_NRO, SERIE_RESPONSABLE_OBSERVACION)" _
                                  & " VALUES ('" & psSerieNumerar2 & "'," & psCodRecep & ",'" & f_psCodArt & "','" & f_psNroSerie & "','N', " _
                                  & " '" & f_DestinoTipo & "','" & f_DestinoCodigo & "','" & ValorSys & "','" & ValorSys & "','0','S','1','1','0','0', '" & f_psEstEquipo & "','" & f_Fecha & "', " & f_psNroPlaca & ",'" & f_Obs & "')"
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = " INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & f_psCodEmpresa & "(SERIE_NUMERAR, UBIC_TIPO, UBIC_CODIGO, ESTADO, SYS_EST, SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL) " _
                                      & " VALUES(" & psSerieNumerar2 & ",'" & f_DestinoTipo & "','" & f_DestinoCodigo & "','20','0','" & ValorSys & "','" & FechaActual() & "','3'," & psCodRecep & ")"
            CmdGlobal.ExecuteNonQuery()
            Guardar_UltimosMovimiento_paraGPS(psConexion, f_psCodEmpresa, 0, FechaActual, "1", f_DestinoCodigo, f_DestinoTipo, f_DestinoCodigo, psSerieNumerar2, f_User)


            CmdGlobal.CommandText = "SELECT * FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & f_DestinoCodigo & ") AND (UBICACT_TIPO='" & f_DestinoTipo & "') " _
                            & " AND (ARTICULO_CODIGO = " & f_psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_psCodEmpresa & "')"
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    StockAc = Nz(Rs!SAA_STOCK_ACTUAL) + 1
                    CmdGlobal2.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & f_DestinoCodigo & ") AND (UBICACT_TIPO='" & f_DestinoTipo & "') " _
                                              & " AND (ARTICULO_CODIGO = " & f_psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_psCodEmpresa & "')"
                    CmdGlobal2.ExecuteNonQuery()
                End While
            Else
                CmdGlobal2.CommandText = " INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                          & " VALUES(" & f_DestinoCodigo & ",'" & f_DestinoTipo & "'," & f_psCodArt & ",1,'0','" & f_psCodEmpresa & "')"
                CmdGlobal2.ExecuteNonQuery()
            End If
            Rs.Close()

            CmdGlobal.CommandText = " INSERT INTO TBINV_RECEPCION_DETALLE_SERIES (EMPRESA_CODIGO, RECEP_CODIGO, SERIE_NUMERAR) " _
                                  & " VALUES ('" & f_psCodEmpresa & "', " & psCodRecep & ", " & psSerieNumerar2 & ")"
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    lblNroMovimiento = Nz(Rs(0)) + 1
                End While
            Else
                lblNroMovimiento = 1
            End If
            Rs.Close()
            CmdGlobal.CommandText = " INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                  & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS) " _
                                  & " VALUES ('" & f_psCodEmpresa & "','" & lblNroMovimiento & "','1','2','" & f_DestinoCodigo & "', " _
                                  & " '" & f_psCodArt & "','1','" & ValorSys & "','3','20','" & FechaActual() & "','0'," & psCodRecep & ")"
            CmdGlobal.ExecuteNonQuery()

            CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET PLACA_NRO = " & f_psNroPlaca & ", SERIE_VALIDADO ='0', SERIE_ESTADO_INVENTARIO = '1', SERIE_CONCILIADO = '2', " _
                                  & " SERIE_NRO = '" & f_psNroSerie & "', SERIE_RESPONSABLE_OBSERVACION = '" & f_Obs & "' where SERIE_NUMERAR = " & psSerieNumerar2
            CmdGlobal.ExecuteNonQuery()

            Call Movimiento_Kardex(psConexion, f_psCodEmpresa, psCodRecep, "20", f_psCodArt, f_DestinoTipo, f_DestinoCodigo, "", 0, "Por Inventario", "1", f_Fecha, 1)

            Cn.Close()
            Return Ingreso_NuevoEquipo
        Catch ex As SqlException

        Catch ex As Exception

        End Try
    End Function
    Public Sub Inventaro_UpdBienUbicacion(ByVal psConexion As String, ByVal f_psCodEmpresa As String,
                                          ByVal f_SerieNumerar As String, ByVal f_psCodUbicacion As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Cn.Open() : CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET SERIE_AREA = '" & f_psCodUbicacion & "'  " _
                              & " WHERE SERIE_NUMERAR= " & f_SerieNumerar
        CmdGlobal.ExecuteNonQuery()
        Cn.Close()
    End Sub
    Public Sub Inventario_InsUpdBien(ByVal psConexion As String, ByVal f_psCodEmpresa As String, ByVal f_psCodArt As String,
                                     ByVal f_psNroSerie As String, ByVal f_psNroPlaca As String, f_psEstEquipo As String,
                                     ByVal f_psUbicacion As String, ByVal f_ArtTipo As String, f_DestinoTipo As String,
                                     ByVal f_DestinoCodigo As String, ByVal f_Obs As String, f_Responsable As String,
                                     ByVal f_CodRelacionado As String, ByVal f_SerieNumerar As String, ByVal f_EstIngreso As String,
                                     ByVal f_InvCodUbi As String, ByVal f_User As String, ByVal f_EstInventario As String, ByVal f_CodInventario As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim Cn2 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal2 As New SqlCommand
        Dim Cn3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal3 As New SqlCommand
        Dim ValorSys As String = ""
        Dim Rs As SqlDataReader
        Dim Rs2 As SqlDataReader
        Dim psInvUbicTipo As String = ""
        Dim psInvUbicCodigo As String = ""
        ValorSys = f_User & FechaActual() & HoraActual()
        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3
        If f_EstIngreso <> "3" Then
            CmdGlobal.CommandText = " SELECT INVDET_SERIE_NUMERAR, INVDET_ESTADO_INGRESO FROM TBINVENTARIO_DETALLE WHERE INVDET_INVENTUBIC_CODIGO = " & f_InvCodUbi & " AND INVDET_SERIE_NUMERAR = " & f_SerieNumerar
            Rs = CmdGlobal.ExecuteReader
            If Rs.HasRows Then
                While Rs.Read
                    f_EstInventario = "1"
                    f_EstIngreso = Nu(Rs("INVDET_ESTADO_INGRESO"))
                End While
                Rs.Close()
            Else
                Rs.Close()
                f_EstInventario = "1"
                f_EstIngreso = "2"
            End If
        End If
        CmdGlobal.CommandText = " SELECT * FROM TBINVENTARIO_UBICACIONES WHERE INVENTUBIC_CODIGO = " & f_InvCodUbi
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                psInvUbicTipo = Nu(Rs("INVENTUBIC_UBIC_TIPO"))
                psInvUbicCodigo = Nu(Rs("INVENTUBIC_UBIC_CODIGO"))
            End While
        End If
        Rs.Close()

        If f_EstIngreso <> "1" Then
            CmdGlobal.CommandText = " INSERT INTO TBINVENTARIO_DETALLE (EMPRESA_CODIGO,INVDET_INVENTUBIC_CODIGO,INVDET_ART_CODIGO,INVDET_SERIE_ESTADO_EQUIPO, " _
                                  & " INVDET_SERIE_NUMERAR,INVDET_SERIE_NRO, INVDET_SYS_EST,INVDET_FECHA,INVDET_ESTADO_INGRESO,INVDET_ESTADO_INVENTARIO,INVDET_SERIE_ESTADO, " _
                                  & " INVDET_UBIC_TIPO,INVDET_UBIC_CODIGO,INVDET_SYS_CRE,INVDET_ART_TIPO,INVDET_CANTIDAD,INVDET_SERIE_AREA, INVDET_PLACA_NRO, " _
                                  & " INVDET_RESPONSABLE, INVDET_RESPONSABLE_OBSERVACION)" _
                                  & " VALUES ('" & f_psCodEmpresa & "'," & f_InvCodUbi & "," & f_psCodArt & ", '" & f_psEstEquipo & "', " _
                                  & " '" & f_SerieNumerar & "','" & f_psNroSerie & "','0','" & FechaActual() & "','" & f_EstIngreso & "','" & f_EstInventario & "', '0', " _
                                  & " '" & f_DestinoTipo & "'," & f_DestinoCodigo & ",'" & ValorSys & "','" & f_ArtTipo & "',1," & IIf(f_psUbicacion = "", "NULL", f_psUbicacion) & ", " & IIf(f_psNroPlaca = "", "NULL", f_psNroPlaca) & ", " _
                                  & " '" & f_Responsable & "', '" & f_Obs & "' ) "
            CmdGlobal.ExecuteNonQuery()
        End If
        CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET SERIE_ESTADO_INVENTARIO = '" & f_EstInventario & "'  " _
                              & " WHERE SERIE_NUMERAR= " & f_SerieNumerar
        CmdGlobal.ExecuteNonQuery()
        If f_CodRelacionado <> "" Then
            CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET SERIE_CONCILIADO = '1', SERIE_COD_RELACIONADO = '" & f_CodRelacionado & "' " _
                                  & " WHERE SERIE_NUMERAR = " & f_SerieNumerar
            CmdGlobal.ExecuteNonQuery()
            CmdGlobal.CommandText = " UPDATE TBINVENTARIO_CONCILIADO  SET INVDET_ESTADO_CONCILIADO = '1', INVDET_COD_RELACIONADO = '" & f_CodRelacionado & "'  " _
                                  & " WHERE INVDET_INVENTUBIC_CODIGO = " & f_InvCodUbi & " AND SERIE_NUMERAR= " & f_SerieNumerar
            CmdGlobal.ExecuteNonQuery()
        End If
        Cn.Close()
        If f_EstIngreso = "2" And psInvUbicTipo <> "" And psInvUbicCodigo <> "" Then
            If psInvUbicTipo <> "9" Then Ingreso_Equipo_AAlmacen(psConexion, f_psCodEmpresa, f_SerieNumerar, f_psEstEquipo, FechaActual(), "20", f_User, psInvUbicTipo, psInvUbicCodigo, "")
        End If
        Cn.Close() : Cn2.Close()
        Cn3.Close()
        Rs.Close()

        Cn.Open() : CmdGlobal.Connection = Cn
        Cn2.Open() : CmdGlobal2.Connection = Cn2
        Cn3.Open() : CmdGlobal3.Connection = Cn3

        CmdGlobal.CommandText = " SELECT IUBIC.INVENTUBIC_CODIGO, IUBIC.INVENTUBIC_NRO, " _
        & " IUBIC.INVENTUBIC_UBIC_TIPO, IUBIC.INVENTUBIC_UBIC_CODIGO,IUBIC.INVENTUBIC_ESTADO " _
        & " FROM dbo.TBINVENTARIO I INNER JOIN dbo.TBINVENTARIO_UBICACIONES IUBIC ON " _
        & " I.INVENT_CODIGO = IUBIC.INVENTUBIC_NRO AND i.EMPRESA_CODIGO = IUBIC.EMPRESA_CODIGO " _
        & " WHERE (I.EMPRESA_CODIGO = '" & f_psCodEmpresa & "') AND (IUBIC.EMPRESA_CODIGO = '" & f_psCodEmpresa & "') " _
        & " AND (IUBIC.INVENTUBIC_SYS_EST = '0') AND (I.INVENT_SYS_EST = '0') AND IUBIC.INVENTUBIC_UBIC_CODIGO='" & f_DestinoCodigo & "' " _
        & " AND (IUBIC.INVENTUBIC_UBIC_TIPO='" & f_DestinoTipo & "') AND IUBIC.INVENTUBIC_ESTADO='2' " _
        & " AND (IUBIC.INVENTUBIC_NRO='" & f_CodInventario & "')"
        Rs = CmdGlobal.ExecuteReader
        If Rs.HasRows Then
            While Rs.Read
                CmdGlobal2.CommandText = " SELECT * FROM TBINVENTARIO_DETALLE WHERE INVDET_SERIE_NUMERAR ='" & f_SerieNumerar & "' AND (INVDET_INVENTUBIC_CODIGO='" & Nz(Rs!INVENTUBIC_CODIGO) & "') " _
                & " AND (EMPRESA_CODIGO='" & f_psCodEmpresa & "') AND (INVDET_SYS_EST='0')"
                Rs2 = CmdGlobal2.ExecuteReader
                If Rs2.HasRows Then
                    While Rs2.Read
                        If f_EstInventario <> "3" Then
                            CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_INVENTARIO = '1' " _
                                          & " WHERE (INVDET_SERIE_NUMERAR='" & Nz(Rs2!INVDET_SERIE_NUMERAR) & "') AND (INVDET_INVENTUBIC_CODIGO='" & Nz(Rs2!INVDET_INVENTUBIC_CODIGO) & "') " _
                                          & " AND (EMPRESA_CODIGO='" & f_psCodEmpresa & "') AND (INVDET_SYS_EST='0')"
                            CmdGlobal3.ExecuteNonQuery()
                        End If
                        CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_SERIE_ESTADO_EQUIPO = '" & f_psEstEquipo & "', INVDET_RESPONSABLE_OBSERVACION = '" & f_Obs & "' " _
                                      & " WHERE (INVDET_SERIE_NUMERAR='" & Nz(Rs2!INVDET_SERIE_NUMERAR) & "') AND (INVDET_INVENTUBIC_CODIGO='" & Nz(Rs2!INVDET_INVENTUBIC_CODIGO) & "') " _
                                      & " AND (EMPRESA_CODIGO='" & f_psCodEmpresa & "') AND (INVDET_SYS_EST='0')"
                        CmdGlobal3.ExecuteNonQuery()

                        If f_CodRelacionado <> "" Then
                            CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_ESTADO_CONCILIADO = '1', INVDET_COD_RELACIONADO = '" & f_CodRelacionado & "' " _
                                          & " WHERE INVDET_SERIE_NUMERAR = " & Nz(Rs2!INVDET_SERIE_NUMERAR) & " AND INVDET_INVENTUBIC_CODIGO = " & Nz(Rs2!INVDET_INVENTUBIC_CODIGO)
                            CmdGlobal3.ExecuteNonQuery()
                        End If
                        If f_Responsable <> "" Then
                            CmdGlobal3.CommandText = " UPDATE TBINVENTARIO_DETALLE SET INVDET_RESPONSABLE = '" & f_Responsable & "' " _
                                          & " WHERE INVDET_SERIE_NUMERAR = " & Nz(Rs2!INVDET_SERIE_NUMERAR) & " AND INVDET_INVENTUBIC_CODIGO = " & Nz(Rs2!INVDET_INVENTUBIC_CODIGO)
                            CmdGlobal3.ExecuteNonQuery()
                        End If
                    End While
                End If
            End While
        End If

    End Sub




    Public Sub Actualizar_Datos_Bien(ByVal psConexion As String, ByVal f_psCodEmpresa As String, ByVal f_psCodArt As String,
                                     ByVal f_psNroSerie As String, ByVal f_psNroPlaca As String, f_psEstEquipo As String,
                                     ByVal f_psUbicacion As String, ByVal f_Fecha As String, f_DestinoTipo As String,
                                     ByVal f_DestinoCodigo As String, ByVal f_Obs As String, f_Responsable As String,
                                     ByVal f_User As String, ByVal f_CodRelacionado As String, ByVal f_Zona As String,
                                     ByVal f_SerieNumerar As String)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim ValorSys As String = ""
        Dim psCodRecep As String = ""
        Dim lblNroMovimiento As String = ""
        Dim StockAc As Double = 0
        Dim cant As Double = 0
        StockAc = 0
        ValorSys = ""
        ValorSys = f_User & FechaActual() & HoraActual()
        Try
            Cn.Open() : CmdGlobal.Connection = Cn
            If f_Zona <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & "  SET SERIE_ZONA = " & f_Zona & "  WHERE SERIE_NUMERAR= " & f_SerieNumerar
                CmdGlobal.ExecuteNonQuery()
            End If
            If f_CodRelacionado <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET SERIE_COD_RELACIONADO = '" & f_CodRelacionado & "', SERIE_CONCILIADO = '1' " _
                                      & " WHERE SERIE_NUMERAR = " & f_SerieNumerar
                CmdGlobal.ExecuteNonQuery()
            End If
            If f_psEstEquipo <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET " _
                                      & " SERIE_ESTADO_EQUIPO = '" & f_psEstEquipo & "' " _
                                      & " WHERE SERIE_NUMERAR = " & f_SerieNumerar
                CmdGlobal.ExecuteNonQuery()
            End If
            If f_Responsable <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET " _
                                      & " SERIE_RESPONSABLE_NOMBRE = '" & f_Responsable & "' " _
                                      & " WHERE SERIE_NUMERAR = " & f_SerieNumerar
                CmdGlobal.ExecuteNonQuery()
            End If
            If f_DestinoCodigo <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET " _
                                      & " SERIE_CUSTODIA_CCOSTO = " & f_DestinoCodigo & ", SERIE_CUSTODIA_TIPO = '" & f_DestinoTipo & "' " _
                                      & " WHERE SERIE_NUMERAR = " & f_SerieNumerar
                CmdGlobal.ExecuteNonQuery()
            End If
            If f_psUbicacion <> "" Then
                CmdGlobal.CommandText = " UPDATE TBINV_ARTICULOS_SERIES_" & f_psCodEmpresa & " SET " _
                                      & " SERIE_AREA = " & f_psUbicacion & " " _
                                      & " WHERE SERIE_NUMERAR = " & f_SerieNumerar
                CmdGlobal.ExecuteNonQuery()
            End If

            Cn.Close()
        Catch ex As SqlException

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Ingreso_Equipo_AAlmacen(ByVal psConexion As String, ByVal f_CodEmpresa As String, ByVal f_SerieNumerar As String,
                                        ByVal f_Estado As String, ByVal f_Fecha As String, ByVal f_Motivo As String,
                                        ByVal f_User As String, ByVal f_TipoDestino As String, ByVal f_CodDestino As String,
                                        ByVal f_CodGuia As String)
        Dim CnU As New SqlClient.SqlConnection(psConexion)
        Dim CnU2 As New SqlClient.SqlConnection(psConexion)
        Dim CnU3 As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobalU As New SqlCommand
        Dim CmdGlobalU2 As New SqlCommand
        Dim CmdGlobalU3 As New SqlCommand
        Dim RsU As SqlDataReader
        Dim RsU2 As SqlDataReader
        Dim psSerieNumerar2 As String = ""
        Dim ValorSys As String = ""
        Dim psCodRecep As String = ""
        Dim lblNroMovimiento As String = ""
        Dim StockAc As Double = 0
        Dim cant As Double = 0
        Dim psCodAlmacen As String = ""
        Dim psTipoOrigen As String = ""
        Dim psCodArt As String = ""
        Dim psRecepcion As String = ""
        Dim psCodDespacho As String = ""

        ValorSys = f_User & FechaActual() & HoraActual()
        Try

            CnU.Open() : CmdGlobalU.Connection = CnU
            CnU2.Open() : CmdGlobalU2.Connection = CnU2
            CnU3.Open() : CmdGlobalU3.Connection = CnU3


            CmdGlobalU.CommandText = " SELECT SERIE_NRO, SERIE_NUMERAR, UBICACT_TIPO, UBICACT_CODIGO,ARTICULO_CODIGO " _
                                  & " FROM TBINV_ARTICULOS_SERIES_" & f_CodEmpresa & " WHERE SERIE_NUMERAR = " & f_SerieNumerar
            RsU = CmdGlobalU.ExecuteReader
            If RsU.HasRows Then
                While RsU.Read
                    psCodAlmacen = Nu(RsU!ubicact_codigo)
                    psTipoOrigen = Nu(RsU!ubicact_tipo)
                    psCodArt = Nu(RsU!ARTICULO_CODIGO)
                    If psTipoOrigen = f_TipoDestino And psCodAlmacen = f_CodDestino Then
                    ElseIf psTipoOrigen = "" And psCodAlmacen = "" Then ' SOLO INGRESO NO HAY SALIDA
                    Else
                        If psTipoOrigen = "1" Then
                            CmdGlobalU2.CommandText = "SELECT MAX(DESP_CODIGO) FROM TBINV_ALMACEN_DESPACHO WHERE EMPRESA_CODIGO='" & f_CodEmpresa & "'"
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows Then
                                While RsU2.Read
                                    psCodDespacho = Nz(RsU(0)) + 1
                                End While
                            Else
                                psCodDespacho = 1
                            End If
                            RsU2.Close()
                            CmdGlobalU2.CommandText = " INSERT INTO TBINV_ALMACEN_DESPACHO(EMPRESA_CODIGO, DESP_CODIGO, DESP_FECHA, DESP_HORA, DESP_USUARIO, DESP_TIPODESTINO," _
                                                  & " CECOSE_CODIGO_DESTINO,ALMACEN_CODIGO_DESTINO,DESP_ESTADO,DESP_SYS_EST,DESP_CANTXDESP,DESP_CANT_DESP,DESP_CANT_REC,DESP_CANT_FALT_REC,ALMACEN_ORIGEN," _
                                                  & " DESP_FECHA_SAL,DESP_HORA_SAL,DESP_MOTIVO_GRAL,DESP_SYS_EJEC) " _
                                                  & " VALUES('" & f_CodEmpresa & "'," & psCodDespacho & ",'" & f_Fecha & "','" & HoraActual() & "','" & f_User & "','" & f_TipoDestino & "'," _
                                                  & " " & IIf(f_TipoDestino = "2", f_CodDestino, "NULL") & "," & IIf(f_TipoDestino = "1", f_CodDestino, "NULL") & ",'2','0',1,1,0,1," & psCodAlmacen & "," _
                                                  & " '" & FechaActual() & "','" & HoraActual() & "','" & f_Motivo & "','" & ValorSys & "')"
                            CmdGlobalU2.ExecuteNonQuery()
                            If f_CodGuia <> "" Then
                                CmdGlobalU2.CommandText = " UPDATE TBINV_ALMACEN_DESPACHO SET GUIREM_CODIGO = " & f_CodGuia & ", DESP_TIPO_DOC_SALIDA_NRO = " & f_CodGuia & " WHERE DESP_CODIGO = " & psCodDespacho
                                CmdGlobalU2.ExecuteNonQuery()
                                If f_SerieNumerar <> "" Then
                                    CmdGlobalU2.CommandText = " UPDATE TBINV_GUIA_REMISION_DETALLE_" & f_CodEmpresa & " SET DESP_CODIGO = " & psCodDespacho & " WHERE GUIREM_CODIGO = " & f_CodGuia & " AND SERIE_NUMERAR = " & f_SerieNumerar
                                    CmdGlobalU2.ExecuteNonQuery()
                                End If
                            End If
                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_ALMACEN_DESPACHO_DET( EMPRESA_CODIGO, DESP_CODIGO, DESPD_ITEM, SERIE_NUMERAR, DESPD_OK, DESPD_SYS_EST,ARTICULO_REF,DESPD_MOTIVO,RECIBIDA_OK ) " _
                                                         & " VALUES('" & f_CodEmpresa & "'," & psCodDespacho & ",1," & f_SerieNumerar & ",'S','0',NULL,'" & f_Motivo & "','N')"
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & f_CodEmpresa & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & f_SerieNumerar
                            CmdGlobalU2.ExecuteNonQuery()
                            'STOCK
                            StockAc = 0
                            CmdGlobalU2.CommandText = " SELECT SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN " _
                                                  & " WHERE (ALMACEN_CODIGO = " & psCodAlmacen & ") And (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                  & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    StockAc = Nz(RsU2!SAA_STOCK_ACTUAL) - 1
                                    CmdGlobalU3.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " " _
                                                          & " WHERE (ALMACEN_CODIGO = " & psCodAlmacen & ") And (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                          & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                                    CmdGlobalU3.ExecuteNonQuery()
                                End While
                            End If
                            RsU2.Close()

                            'MOVIMIENTO GENERAL
                            CmdGlobalU2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    lblNroMovimiento = Nz(RsU2(0)) + 1
                                End While
                            Else
                                lblNroMovimiento = 1
                            End If
                            RsU2.Close()

                            Call Movimiento_Kardex(psConexion, f_CodEmpresa, psCodDespacho, f_Motivo, psCodArt, psTipoOrigen, psCodAlmacen, f_TipoDestino, f_CodDestino, "", "2", FormatoFecha(f_Fecha), 1)

                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                              & " VALUES ('" & f_CodEmpresa & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & psCodAlmacen & "', " _
                                                              & " '" & psCodArt & "','1','" & ValorSys & "','3','" & f_Motivo & "','" & f_Fecha & "','0','" & psCodDespacho & "','" & f_TipoDestino & "'," & f_CodDestino & ")"
                            CmdGlobalU2.ExecuteNonQuery()
                            '--------------------------recepcion en ccosto O ALMACEN
                            CmdGlobalU2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO_DET SET RECIBIDA_OK ='S',DESPD_SYS_REC='" & ValorSys & "',DESPD_MODO_RECIBIDO='M'WHERE EMPRESA_CODIGO='" & f_CodEmpresa & "' AND DESP_CODIGO='" & psCodDespacho & "' AND SERIE_NUMERAR =" & f_SerieNumerar
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "UPDATE TBINV_ALMACEN_DESPACHO SET DESP_SYS_REC='" & ValorSys & "',DESP_ESTADO='3',DESP_CANT_REC='1',DESP_CANT_FALT_REC='0' WHERE DESP_CODIGO='" & psCodDespacho & "' AND EMPRESA_CODIGO='" & f_CodEmpresa & "'"
                            CmdGlobalU2.ExecuteNonQuery()

                            'STOCK
                            CmdGlobalU2.CommandText = "SELECT SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO =" & f_CodDestino & ") AND (UBICACT_TIPO='" & f_TipoDestino & "') " _
                                                & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    StockAc = Nz(RsU2!SAA_STOCK_ACTUAL) + 1
                                    CmdGlobalU3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & f_CodDestino & ") AND (UBICACT_TIPO='" & f_TipoDestino & "') " _
                                                                     & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                                    CmdGlobalU3.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobalU3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                                     & "VALUES(" & f_CodDestino & ",'" & f_TipoDestino & "'," & psCodArt & ",1,'0','" & f_CodEmpresa & "')"
                                CmdGlobalU3.ExecuteNonQuery()
                            End If
                            RsU2.Close()

                            'MOVIMIENTO GENERAL

                            Call Movimiento_Kardex(psConexion, f_CodEmpresa, psCodDespacho, f_Motivo, psCodArt, f_TipoDestino, f_CodDestino, psTipoOrigen, psCodAlmacen, "", "1", FormatoFecha(f_Fecha), 1)

                            CmdGlobalU2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    lblNroMovimiento = Nz(RsU2(0)) + 1
                                End While
                            Else
                                lblNroMovimiento = 1
                            End If
                            RsU2.Close()

                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                       & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                       & " VALUES ('" & f_CodEmpresa & "','" & lblNroMovimiento & "','1','" & f_TipoDestino & "'," & f_CodDestino & ", " _
                                                       & " '" & psCodArt & "','1','" & ValorSys & "','3','" & f_Motivo & "','" & f_Fecha & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodAlmacen & "')"
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & f_CodEmpresa & " SET UBICACT_TIPO='" & f_TipoDestino & "',UBICACT_CODIGO=" & f_CodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & f_SerieNumerar
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & f_CodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA, INGRESO_TIPO, NRO_ING_SAL)" _
                                                              & " VALUES ('" & f_SerieNumerar & "','" & f_TipoDestino & "'," & f_CodDestino & ",'" & f_Motivo & "','0','" & ValorSys & "','" & f_Fecha & "','1','" & psCodDespacho & "')"
                            CmdGlobalU2.ExecuteNonQuery()
                            Guardar_UltimosMovimiento_paraGPS(psConexion, f_psCodEmpresa, 0, FechaActual, "1", psCodAlmacen, f_TipoDestino, f_CodDestino, f_SerieNumerar, f_User)

                        ElseIf psTipoOrigen = "2" Then 'SALIDA DE CENTRO DE COSTO
                            CmdGlobalU2.CommandText = "SELECT MAX(OSAL_CODIGO) FROM TBINV_CCOSTO_SALIDA  WHERE EMPRESA_CODIGO='" & f_CodEmpresa & "'"
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    psCodDespacho = Nz(RsU2(0)) + 1
                                End While
                            Else
                                psCodDespacho = 1
                            End If
                            RsU2.Close()

                            CmdGlobalU2.CommandText = " INSERT INTO TBINV_CCOSTO_SALIDA(EMPRESA_CODIGO,OSAL_CODIGO,OSAL_FECHA,OSAL_HORA,OSAL_USUARIO,OSAL_TIPODESTINO, " _
                                                    & " CECOSE_CODIGO_DESTINO, ALMACEN_CODIGO_DESTINO, OSAL_ESTADO,OSAL_SYS_EST,OSAL_CANT_ENV,OSAL_CANT_REC,OSAL_CANT_FALT_REC,CECOSE_CODIGO_ORIGEN, " _
                                                    & " OSAL_FECHA_SAL,OSAL_HORA_SAL,OSAL_MOTIVO_GRAL) " _
                                                    & " VALUES('" & f_CodEmpresa & "'," & psCodDespacho & ",'" & FechaActual() & "','" & HoraActual() & "','" & f_User & "','" & f_TipoDestino & "'," _
                                                    & " " & IIf(f_TipoDestino = "2", f_CodDestino, "NULL") & "," & IIf(f_TipoDestino = "1", f_CodDestino, "NULL") & ",'2','0',1,0,1,'" & psCodAlmacen & "'," _
                                                    & " '" & f_Fecha & "','" & HoraActual() & "','" & f_Motivo & "')"
                            CmdGlobalU2.ExecuteNonQuery()
                            If f_CodGuia <> "" Then
                                CmdGlobalU2.CommandText = " UPDATE TBINV_CCOSTO_SALIDA SET GUIREM_CODIGO = " & f_CodGuia & " WHERE OSAL_CODIGO = " & psCodDespacho
                                CmdGlobalU2.ExecuteNonQuery()
                                If f_SerieNumerar <> "" Then
                                    CmdGlobalU2.CommandText = " UPDATE TBINV_GUIA_REMISION_DETALLE_" & f_CodEmpresa & " SET OSAL_CODIGO = " & psCodDespacho & " WHERE GUIREM_CODIGO = " & f_CodGuia & " AND SERIE_NUMERAR = " & f_SerieNumerar
                                    CmdGlobalU2.ExecuteNonQuery()
                                End If
                            End If
                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_CCOSTO_SALIDA_DET (EMPRESA_CODIGO, OSAL_CODIGO, OSALD_ORDEN, SERIE_NUMERAR, ENVIADA_OK, RECIBIDA_OK, OSALD_SYS_EST, OSALD_MOTIVO) " _
                                                                  & " VALUES('" & f_CodEmpresa & "'," & psCodDespacho & ",1," & f_SerieNumerar & ",'S','N','0','" & f_Motivo & "')"
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & f_CodEmpresa & " SET SERIE_PARATRANSITO = 'S' WHERE SERIE_NUMERAR=" & f_SerieNumerar
                            CmdGlobalU2.ExecuteNonQuery()
                            'STOCK
                            CmdGlobalU2.CommandText = " SELECT SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN " _
                                                    & " WHERE (ALMACEN_CODIGO = " & psCodAlmacen & ") And (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                    & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    StockAc = Nz(RsU2!SAA_STOCK_ACTUAL) - 1
                                    CmdGlobalU3.CommandText = " UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & psCodAlmacen & ") AND (UBICACT_TIPO='" & psTipoOrigen & "') " _
                                                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                                    CmdGlobalU3.ExecuteNonQuery()
                                End While
                            End If
                            RsU2.Close()

                            'MOVIMIENTO GENERAL

                            Call Movimiento_Kardex(psConexion, f_CodEmpresa, psCodDespacho, f_Motivo, psCodArt, psTipoOrigen, psCodAlmacen, "1", f_CodDestino, "", "2", FormatoFecha(f_Fecha), 1)

                            CmdGlobalU2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    lblNroMovimiento = Nz(RsU2(0)) + 1
                                End While
                            Else
                                lblNroMovimiento = 1
                            End If
                            RsU2.Close()
                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                              & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                              & " VALUES ('" & f_CodEmpresa & "','" & lblNroMovimiento & "','2','" & psTipoOrigen & "','" & psCodAlmacen & "', " _
                                                              & " '" & psCodArt & "','1','" & ValorSys & "','3','" & f_Motivo & "','" & f_Fecha & "','0','" & psCodDespacho & "','" & f_TipoDestino & "'," & f_CodDestino & ")"
                            CmdGlobalU2.ExecuteNonQuery()

                            '--------------------------recepcion en ccosto O ALMACEN
                            CmdGlobalU2.CommandText = "UPDATE TBINV_CCOSTO_SALIDA_DET  SET RECIBIDA_OK ='S',OSALD_SYS_REC='" & ValorSys & "',OSALD_MODO_RECIBIDO='M' WHERE EMPRESA_CODIGO='" & f_CodEmpresa & "' AND OSAL_CODIGO='" & psCodDespacho & "' AND SERIE_NUMERAR =" & f_SerieNumerar
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "UPDATE TBINV_CCOSTO_SALIDA  SET OSAL_SYS_REC='" & ValorSys & "',OSAL_ESTADO='3',OSAL_CANT_REC='1',OSAL_CANT_FALT_REC='0' WHERE OSAL_CODIGO='" & psCodDespacho & "' AND EMPRESA_CODIGO='" & f_CodEmpresa & "'"
                            CmdGlobalU2.ExecuteNonQuery()
                            'STOCK
                            CmdGlobalU2.CommandText = "SELECT SAA_STOCK_ACTUAL FROM TBINV_STOCK_ARTICULOS_ALMACEN WHERE (ALMACEN_CODIGO = " & f_CodDestino & ") AND (UBICACT_TIPO='" & f_TipoDestino & "') " _
                            & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    StockAc = Nz(RsU2!SAA_STOCK_ACTUAL) + 1
                                    CmdGlobalU3.CommandText = "UPDATE TBINV_STOCK_ARTICULOS_ALMACEN SET SAA_STOCK_ACTUAL=" & StockAc & " WHERE (ALMACEN_CODIGO = " & f_CodDestino & ") AND (UBICACT_TIPO='" & f_TipoDestino & "') " _
                                                      & " AND (ARTICULO_CODIGO = " & psCodArt & ") AND (SAA_SYS_EST = '0') AND (EMPRESA_CODIGO='" & f_CodEmpresa & "')"
                                    CmdGlobalU3.ExecuteNonQuery()
                                End While
                            Else
                                CmdGlobalU3.CommandText = "INSERT TBINV_STOCK_ARTICULOS_ALMACEN(ALMACEN_CODIGO,UBICACT_TIPO, ARTICULO_CODIGO,SAA_STOCK_ACTUAL,SAA_SYS_EST,EMPRESA_CODIGO) " _
                                                  & "VALUES(" & f_CodDestino & ",'" & f_TipoDestino & "'," & psCodArt & ",1,'0','" & f_CodEmpresa & "')"
                                CmdGlobalU3.ExecuteNonQuery()
                            End If
                            RsU2.Close()

                            'MOVIMIENTO GENERAL
                            Call Movimiento_Kardex(psConexion, f_CodEmpresa, psCodDespacho, f_Motivo, psCodArt, f_TipoDestino, f_CodDestino, psTipoOrigen, psCodAlmacen, "", "1", FormatoFecha(f_Fecha), 1)

                            CmdGlobalU2.CommandText = "SELECT MAX(MOV_NRO) FROM TBINV_MOVIMIENTO_GENERAL "
                            RsU2 = CmdGlobalU2.ExecuteReader
                            If RsU2.HasRows > 0 Then
                                While RsU2.Read
                                    lblNroMovimiento = Nz(RsU2(0)) + 1
                                End While
                            Else
                                lblNroMovimiento = 1
                            End If
                            RsU2.Close()

                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_MOVIMIENTO_GENERAL (EMPRESA_CODIGO, MOV_NRO, MOV_TIPO, TIPO_UBICACT, CODIGO_UBICACT, " _
                                                      & " CODIGO_ARTICULO, NRO_ARTICULO, MOV_SYS_CRE, MOV_ESTADO, MOV_MOTIVO, MOV_FECHA, MOV_SYS_EST, CODIGO_TRANS, TIPO_ORIGEN_DESTINO, CODIGO_ORIGEN_DESTINO) " _
                                                      & " VALUES ('" & f_CodEmpresa & "','" & lblNroMovimiento & "','1','" & f_TipoDestino & "'," & f_CodDestino & ", " _
                                                      & " '" & psCodArt & "','1','" & ValorSys & "','3','" & f_Motivo & "','" & f_Fecha & "','0','" & psCodDespacho & "','" & psTipoOrigen & "','" & psCodAlmacen & "')"
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "UPDATE TBINV_ARTICULOS_SERIES_" & f_CodEmpresa & " SET UBICACT_TIPO='" & psTipoOrigen & "',UBICACT_CODIGO=" & f_CodDestino & ",UBICACT_SYS='" & ValorSys & "',SERIE_PARATRANSITO = NULL WHERE SERIE_NUMERAR=" & f_SerieNumerar
                            CmdGlobalU2.ExecuteNonQuery()
                            CmdGlobalU2.CommandText = "INSERT INTO TBINV_ARTICULOS_SERIES_UBIC_" & f_CodEmpresa & " (SERIE_NUMERAR,UBIC_TIPO,UBIC_CODIGO,ESTADO,SYS_EST,SYS_CRE,INGRESO_FECHA,INGRESO_TIPO,NRO_ING_SAL)" _
                                                      & " VALUES ('" & f_SerieNumerar & "','" & f_TipoDestino & "'," & f_CodDestino & ",'" & f_Motivo & "','0','" & ValorSys & "','" & f_Fecha & "','2','" & psCodDespacho & "')"
                            CmdGlobalU2.ExecuteNonQuery()
                            Guardar_UltimosMovimiento_paraGPS(psConexion, f_psCodEmpresa, 0, FechaActual, "1", psCodAlmacen, f_TipoDestino, f_CodDestino, f_SerieNumerar, f_User)
                        End If
                    End If
                End While
            End If
            RsU.Close()

        Catch ex As Exception

        End Try
    End Sub

    Private Function f_psCodEmpresa() As String
        Throw New NotImplementedException()
    End Function
End Class
