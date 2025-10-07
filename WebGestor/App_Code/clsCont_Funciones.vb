Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports WebGestor
Public Class clsCont_Funciones
    Public Function Hallar_Valor_Compra(ByVal psConexion As String, ByVal Fecha As String) As String
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim RsC As SqlDataReader
        Hallar_Valor_Compra = "0.0000"
        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT TIPCAM_COMPRA FROM TBTIPCAMBIO WHERE (TIPCAM_FECHA = '" & Fecha & "')"
        RsC = CmdGlobal.ExecuteReader
        If RsC.HasRows Then
            While RsC.Read
                Hallar_Valor_Compra = Format(Nu(RsC!TIPCAM_COMPRA), "0.0000")
            End While
        End If
        RsC.Close()
        Cn.Close()
    End Function
    Public Function Hallar_Valor_Venta(ByVal psConexion As String, ByVal Fecha As String) As String
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim RsC As SqlDataReader
        Hallar_Valor_Venta = "0.0000"
        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT TIPCAM_VENTA FROM TBTIPCAMBIO WHERE (TIPCAM_FECHA = '" & Fecha & "')"
        RsC = CmdGlobal.ExecuteReader
        If RsC.HasRows Then
            While RsC.Read
                Hallar_Valor_Venta = Nu(RsC!TIPCAM_VENTA)
            End While
        End If
        RsC.Close()
        Cn.Close()
    End Function
    Public Sub Llena_TipoDocumento(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psAño As String,
                                        ByVal Ddl As DropDownList)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Cn.Open()
        Dim Sql As String = ""
        Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
        Sql = " SELECT DOC_CODIGO, DOC_DOCUMENTO " _
            & " From dbo.TBDOCUMENTOS " _
            & " WHERE (DOC_EMPRESA = '" & psCodEmpresa & "') AND (DOC_AÑO ='" & psAño & "') AND (DOC_SYS_EST = '0')"

        cmdSql = New SqlClient.SqlCommand(Sql, Cn)
        Ddl.DataSource = cmdSql.ExecuteReader
        Ddl.DataTextField = "DOC_CODIGO"
        Ddl.DataValueField = "DOC_DOCUMENTO"
        Ddl.Items.Add("< Seleccionar >") : Ddl.SelectedValue = "< Seleccionar >"
        Cn.Close()
    End Sub
    Public Sub Finanza_ListaCaja(ByVal psConexion As String, ByVal Ddl As DropDownList)
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Cn.Open()
        Dim Sql As String = ""
        Dim cmdSql As New SqlClient.SqlCommand(Sql, Cn)
        Sql = "SELECT CU.CAJA_PERSONAL, RIGHT('00' + CONVERT(VARCHAR(2), CAJA_NRO), 2) as CAJA , CU.CAJA_CODIGO, C.CAJA_NRO,PERSON_APEPAT+' '+PERSON_APEMAT+', '+PERSON_NOMBRES AS NOMBRESP " _
            & " FROM TBVENTAS_CAJA_USUARIOS CU INNER JOIN TBVENTAS_CAJA C ON CU.EMPRESA_CODIGO = C.EMPRESA_CODIGO AND CU.CAJA_CODIGO = C.CAJA_CODIGO " _
            & " INNER JOIN BDGRUPOEMPRESAS.DBO.TBPERSONAL P ON P.PERSON_CODIGO=CU.CAJA_PERSONAL " _
            & " WHERE (CU.SYS_EST = '0') AND (C.CAJA_SYS_EST = '0') "
        Sql = Sql & " ORDER BY C.CAJA_NRO"
        cmdSql = New SqlClient.SqlCommand(Sql, Cn)
        Ddl.DataSource = cmdSql.ExecuteReader
        Ddl.DataTextField = "CAJA"
        Ddl.DataValueField = "CAJA_CODIGO"
        Ddl.Items.Add("< Seleccionar >") : Ddl.SelectedValue = "< Seleccionar >"

    End Sub




    Public Function Convertir_Hora(ByVal NumMin As Integer) As String
        Dim Hx As String
        Hx = Llenar_Ceros(Trim(Str((NumMin \ 60))), 2) + Llenar_Ceros(Trim(Str((NumMin Mod 60))), 2)
        Convertir_Hora = Llenar_Ceros(Hx, 4)
        If Left(Convertir_Hora, 2) = "24" Then Convertir_Hora = "00" + Right(Convertir_Hora, 2)
    End Function
    Function AñoSistema(ByVal psConexion As String, ByVal psCodEmpresa As String) As String
        Dim Cn As New SqlClient.SqlConnection(psConexion)
        Dim CmdGlobal As New SqlCommand
        Dim RsAño As SqlDataReader
        AñoSistema = ""
        Cn.Open()
        CmdGlobal.Connection = Cn
        CmdGlobal.CommandText = "SELECT AÑO From TBAÑOS where ACTIVO='S' AND EMPRESA='" & psCodEmpresa & "'"
        RsAño = CmdGlobal.ExecuteReader
        If RsAño.HasRows Then
            While RsAño.Read
                AñoSistema = RsAño("AÑO")
            End While
        End If
        RsAño.Close()
        Return AñoSistema
    End Function
    'Public Sub Generar_Comprobante(ByVal psConexion As String, ByVal psCodEmpresa As String, ByVal psUser As String, ByVal pTipoParametro As String,
    '                                   ByVal NroDoc As String, ByVal pCodPersona As String, ByVal pDoc As String,
    '                                   ByVal pMoneda As String, ByVal pSubTotal As String, ByVal pIgv As String, ByVal pTotal As String,
    '                                   ByVal pTipoVenta As String, ByVal pFecEmision As String, ByVal pFecCanc As String, ByVal pFecPago As String,
    '                                   ByVal pSigla As String, ByVal pIngGlosa As String, ByVal pMotivo As String, ByVal pTipoMot As String,
    '                                   ByVal pParamDet As String, ByVal psDocRef As String, ByVal psNroDocRef As String,
    '                                   ByVal psMotivoNC As String, ByVal CodRecibo As String, ByVal pParamServ As String,
    '                                   ByVal psNotaCD As String, ByVal psMotivoNCDesc As String, ByVal psTranfGrat As String,
    '                                   ByVal pFecRegistro As String, Optional pCCosto As String, Optional pFinanza As String,
    '                                   ByVal pMCuenta As String, Optional psCtaInafecta As String, Optional psImporteIA As String,
    '                                   Optional psOpcionAI As String, Optional psServicio As String, Optional psCtaServicio As String,
    '                                   Optional psAño As String, Optional pdCodFinanza As String, Optional NC As String,
    '                                   Optional psKardex As String, Optional CodMov As String)
    '    Dim NroPerAct As Integer
    '    Dim Cn As New SqlClient.SqlConnection(psConexion)
    '    Dim Cn2 As New SqlClient.SqlConnection(psConexion)
    '    Dim Cn3 As New SqlClient.SqlConnection(psConexion)
    '    Dim Cn4 As New SqlClient.SqlConnection(psConexion)
    '    Dim Cn5 As New SqlClient.SqlConnection(psConexion)
    '    Dim CnGrupo As New SqlClient.SqlConnection(Ruta_GrEmp)
    '    Dim CmdGlobal As New SqlCommand
    '    Dim CmdGlobal2 As New SqlCommand
    '    Dim CmdGlobal3 As New SqlCommand
    '    Dim CmdGlobal4 As New SqlCommand
    '    Dim CmdGlobal5 As New SqlCommand
    '    Dim CmdGrupo As New SqlCommand
    '    Dim RsAño As SqlDataReader
    '    Dim RsG As SqlDataReader
    '    Dim Rs As SqlDataReader
    '    Dim Rs1 As SqlDataReader
    '    Dim RsGrupo As SqlDataReader
    '    Dim codigo As Long
    '    Dim pPrefVoucher As String, pCodAsiento As String, pNroVoucher As String
    '    Dim Resp As String, Doc As String : Doc = ""
    '    Dim DocRef As String : DocRef = ""
    '    Dim ValorSys As String
    '    Dim TxtImporte As Double : TxtImporte = 0
    '    ValorSys = FechaActual() & HoraActual() & psUser
    '    Dim pGlosa As String
    '    Dim TipoMoneda As String
    '    Dim TipoIngreso As String
    '    Dim vs1, vs2, Dif As Double
    '    If pIngGlosa = "" Then pGlosa = "" Else pGlosa = pIngGlosa
    '    If pMoneda = "2" Then TipoMoneda = "2" Else TipoMoneda = "1"
    '    Dim psCodDetalle As String
    '    Dim PDeudora As String
    '    Dim PAcreedora As String
    '    Dim psañog As String
    '    Dim Ingreso As Boolean
    '    Ingreso = False
    '    Cn.Open() : CmdGlobal.Connection = Cn
    '    Cn2.Open() : CmdGlobal2.Connection = Cn2
    '    Cn3.Open() : CmdGlobal3.Connection = Cn3
    '    Cn4.Open() : CmdGlobal4.Connection = Cn4
    '    Cn5.Open() : CmdGlobal5.Connection = Cn5
    '    If pMotivo <> "" And pTipoMot <> "" Then
    '        CmdGlobal.CommandText = " SELECT ELEMEN_CODIGO FROM TBCELEMEN " _
    '            & " WHERE ELEMEN_TABLA ='TBOPC366' AND ELEMEN_SYS_EST='0'" _
    '            & " AND ELEMEN_CODIGO_MINIS='" & pMotivo & "' AND ELEMEN_VALOR_MINIS = '" & pTipoMot & "'"
    '        Rs = CmdGlobal.ExecuteReader
    '        If Rs.HasRows Then
    '            While Rs.Read
    '                psCodDetalle = Nu(Rs!ELEMEN_CODIGO)
    '            End While
    '        Else
    '            psCodDetalle = pMotivo
    '        End If
    '        Rs.Close()
    '    End If
    '    If psAño <> "" Then psañog = psAño Else psañog = Mid(pFecRegistro, 7, 4)
    '    If pGlosa = "" Then
    '        CmdGlobal.CommandText = " SELECT PERSONA_RUC, PERSONA_RAZON_SOCIAL From TBDATA_PERSONAS " _
    '            & " WHERE (PERSONA_SYS_EST = '0') AND (EMPRESA_CODIGO = '" & psCodEmpresa & "') AND PERSONA_CODIGO =" & pCodPersona
    '        Rs = CmdGlobal.ExecuteReader
    '        If Rs.HasRows Then
    '            While Rs.Read
    '                pGlosa = Nu(Rs!PERSONA_RAZON_SOCIAL)
    '            End While
    '        End If
    '        Rs.Close()
    '    End If
    '    CmdGlobal.CommandText = " SELECT PER_NRO_PERIODOS,PER_PERIODO, PER_NOMBRE, PER_FECHAINI,PER_FECHAFIN, PER_ACTUAL FROM TBPERIODIFICACION " _
    '        & " WHERE (PER_EMPRESA = '" & psCodEmpresa & "') AND (PER_AÑO = '" & psañog & "') AND PER_ACTUAL='S' AND (PER_SYS_EST = '0') ORDER BY PER_PERIODO"
    '    Rs = CmdGlobal.ExecuteReader
    '    If Rs.HasRows Then
    '        While Rs.Read
    '            NroPerAct = Nz(Rs!PER_PERIODO)
    '        End While
    '    End If
    '    Rs.Close()
    '    'pFecRegistro = "27/08/2010"
    '    NroPerAct = Format(Month(pFecRegistro))
    '    CmdGlobal.CommandText = "SELECT DISTINCT PARAMETRO_ASIENTO FROM TBCONT_PARAMETROS WHERE PARAMETRO_TIPO='" & pTipoParametro & "' AND EMPRESA_CODIGO='" & SistCodEmpresa & "' AND PARAMETRO_SYS_EST='0' and PARAMETRO_ESTADO='1'"
    '    RsG = CmdGlobal.ExecuteReader
    '    If RsG.HasRows Then
    '        While Rs.Read
    '            CmdGlobal2.CommandText = "SELECT ASIENTO_PREFIJO,ASIENTO_CODIGO FROM TBASIENTOS WHERE (ASIENTO_CODIGO = '" & Nu(RsG!PARAMETRO_ASIENTO) & "') AND (ASIENTO_EMPRESA = '" & SistCodEmpresa & "') AND (ASIENTO_AÑO = '" & psañog & "')"
    '            Rs = CmdGlobal2.ExecuteReader
    '            If Rs.HasRows Then
    '                While Rs.Read
    '                    pPrefVoucher = Nu(Rs!ASIENTO_PREFIJO) & Format(NroPerAct, "00")
    '                    pCodAsiento = Nu(Rs!ASIENTO_CODIGO)
    '                    Rs.Close()
    '                    CmdGlobal3.CommandText = "SELECT MAX(RIGHT(COMPROB_NRO_VOUCHER, 4)) FROM TBCOMPROB_" & SistCodEmpresa & psañog & " WHERE " _
    '                    & " (COMPROB_PERIODO = " & NroPerAct & " ) AND (COMPROB_ASIENTO_CODIGO = '" & Nu(RsG!PARAMETRO_ASIENTO) & "')"
    '                    Rs = CmdGlobal3.ExecuteReader
    '                    If Rs.HasRows Then
    '                        While Rs.Read
    '                            pNroVoucher = Llenar_Ceros(Nz(Rs(0)) + 1, 3)
    '                        End While
    '                    End If
    '                    Rs.Close()
    '                End While
    '            Else
    '                Rs.Close()
    '            End If
    '        End While
    '    End If
    '    RsG.Close()
    '    CmdGlobal.CommandText = " SELECT *,(SELECT ELEMEN_VALOR From BDGrupoEmpresas.dbo.TBCELEMEN WHERE (ELEMEN_TABLA = 'TBOPC366') AND (ELEMEN_CODIGO = PARAMETRO_DETALLE)) AS GLOSA  " _
    '        & " FROM TBCONT_PARAMETROS WHERE PARAMETRO_TIPO='" & pTipoParametro & "' AND EMPRESA_CODIGO='" & psCodEmpresa & "' AND PARAMETRO_SYS_EST='0' AND PARAMETRO_DETALLE='" & pParamDet & "' " _
    '        & " AND PARAMETRO_ASIENTO='" & pCodAsiento & "'  AND PARAMETRO_INGRESO='1' and PARAMETRO_ESTADO='1' AND PARAMETRO_AÑO='" & psañog & "'"
    '    If pTipoParametro <> "3" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND PARAMETRO_MONEDA='" & TipoMoneda & "' "
    '    CmdGlobal.CommandText = CmdGlobal.CommandText & " ORDER BY PARAMETRO_DETALLE"
    '    Rs = CmdGlobal.ExecuteReader
    '    If Rs.HasRows Then
    '        If Nu(Rs!PARAMETRO_INGRESO) = "1" And pIngGlosa = "" Then
    '            CmdGlobal2.CommandText = " SELECT PLAN_NOMBRE_CUENTA FROM TBPCGR_" & psCodEmpresa & "  " _
    '                & " WHERE (PLAN_AÑO = '" & psañog & "') AND (PLAN_SYS_EST = '0')  AND (PLAN_CODIGO=" & Nu(Rs!PARAMETRO_CUENTA) & ")"
    '            Rs1 = CmdGlobal2.ExecuteReader
    '            If Rs1.HasRows Then
    '                While Rs.Read
    '                    pGlosa = pGlosa & " - " & Nu(Rs1!PLAN_NOMBRE_CUENTA)
    '                End While
    '            End If
    '            Rs1.Close()
    '        End If
    '    End If
    '    Rs.Close()
    '    Doc = ""
    '    If pTipoParametro = "3" Or pTipoParametro = "13" Or pTipoParametro = "16" Or pTipoParametro = "25" Or pTipoParametro = "26" Then
    '        Doc = pDoc
    '    Else
    '        CmdGlobal.CommandText = "SELECT DOC_CODIGO FROM TBDOCUMENTOS WHERE DOC_SYS_EST='0' AND DOC_AÑO='" & psañog & "' AND DOC_EMPRESA='" & SistCodEmpresa & "' AND (DOC_DOCUMENTO)='" & UCase(pDoc) & "'"
    '        RsG = CmdGlobal.ExecuteReader
    '        If RsG.HasRows Then
    '            While Rs.Read
    '                Doc = Nu(RsG(0))
    '            End While
    '        End If
    '        RsG.Close()
    '    End If
    '    If psDocRef <> "" And psNroDocRef <> "" Then
    '        CmdGlobal.CommandText = "SELECT DOC_CODIGO FROM TBDOCUMENTOS WHERE DOC_SYS_EST='0' AND DOC_AÑO='" & psañog & "' AND DOC_EMPRESA='" & SistCodEmpresa & "' AND (DOC_DOCUMENTO)='" & UCase(psDocRef) & "'"
    '        RsG = CmdGlobal.ExecuteReader
    '        If RsG.HasRows Then
    '            While Rs.Read
    '                DocRef = Nu(RsG(0))
    '            End While
    '        End If
    '        RsG.Close()
    '        DocRef = psDocRef
    '    End If
    '    Dim psCentroCosto As String : psCentroCosto = ""
    '    Dim psCuentaContable As String : psCuentaContable = ""
    '    CmdGlobal.CommandText = " SELECT REGISTRO_COMPROBANTE FROM TBVENTAS_REGISTRO_COMPROBANTE WHERE REGISTRO_TIPO='1' AND REGISTRO_AÑO='" & psañog & "' AND REGISTRO_SYS_EST='0'"
    '    Rs = CmdGlobal.ExecuteReader
    '    If Rs.HasRows Then
    '        While Rs.Read
    '            TipoIngreso = Nu(Rs!REGISTRO_COMPROBANTE)
    '        End While
    '    End If
    '    Rs.Close()
    '    Dim Detalle As String
    '    If psServicio = "S" Then Detalle = "2" Else Detalle = "3"
    '    CmdGlobal.CommandText = " SELECT *,(SELECT ELEMEN_VALOR From BDGrupoEmpresas.dbo.TBCELEMEN WHERE (ELEMEN_TABLA = 'TBOPC366') AND (ELEMEN_CODIGO = PARAMETRO_DETALLE)) AS GLOSA  " _
    '        & " FROM TBCONT_PARAMETROS WHERE PARAMETRO_TIPO='" & pTipoParametro & "' AND EMPRESA_CODIGO='" & SistCodEmpresa & "' AND PARAMETRO_SYS_EST='0' " _
    '        & " AND PARAMETRO_ASIENTO='" & pCodAsiento & "' AND PARAMETRO_INGRESO='" & TipoIngreso & "' AND PARAMETRO_ESTADO='1' AND  (PARAMETRO_AÑO = '" & psañog & "')"
    '    If pTipoParametro <> "3" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND PARAMETRO_MONEDA='" & TipoMoneda & "' "
    '    If pTipoParametro = "3" Then CmdGlobal.CommandText = CmdGlobal.CommandText & " AND PARAMETRO_DETALLE='" & psCodDetalle & "' AND PARAMETRO_MOTIVO='" & pTipoMot & "'"
    '    CmdGlobal.CommandText = CmdGlobal.CommandText & " ORDER BY PARAMETRO_DETALLE"
    '    Rs = CmdGlobal.ExecuteReader
    '    If Rs.HasRows Then
    '        While Rs.Read
    '            If Nu(Rs!PARAMETRO_INGRESO) = "1" Then
    '                If pTipoParametro = "3" Or pTipoParametro = "4" Or pTipoParametro = "5" Then
    '                    TxtImporte = Format(pTotal, "0.00")
    '                ElseIf (pTipoParametro = "13" Or pTipoParametro = "16" Or pTipoParametro = "25" Or pTipoParametro = "26") And (Nu(Rs!PARAMETRO_Detalle) <> "4" And Nu(Rs!PARAMETRO_Detalle) <> "5") Then
    '                    Detalle = Nu(Rs!PARAMETRO_Detalle)
    '                    CnGrupo.Open() : CmdGrupo.Connection = CnGrupo
    '                    CmdGrupo.CommandText = " SELECT ELEMEN_VALOR_MINIS FROM TBCELEMEN WHERE ELEMEN_CODIGO = '" & Nu(Rs!PARAMETRO_Detalle) & "' " _
    '                        & " AND ELEMEN_VALOR_MINIS = '" & psMotivoNC & "'"
    '                    RsGrupo = CmdGrupo.ExecuteReader
    '                    If RsGrupo.HasRows Then
    '                        While RsGrupo.Read
    '                            TxtImporte = Format(pSubTotal, "0.00")
    '                            RsGrupo.Close()
    '                        End While
    '                    Else
    '                        RsGrupo.Close()
    '                        GoTo SGTE
    '                    End If
    '                ElseIf (pTipoParametro = "13" Or pTipoParametro = "16" Or pTipoParametro = "21" Or pTipoParametro = "25" Or pTipoParametro = "26") And (Nu(Rs!PARAMETRO_Detalle) = "4" Or Nu(Rs!PARAMETRO_Detalle) = "5") Then
    '                    If Nu(Rs!PARAMETRO_Detalle) = "4" Then TxtImporte = Format(pIgv, "0.00")
    '                    If Nu(Rs!PARAMETRO_Detalle) = "5" Then TxtImporte = Format(pTotal, "0.00")
    '                Else
    '                    If Nu(Rs!PARAMETRO_Detalle) = "2" Then TxtImporte = Format(Nz(pSubTotal), "0.00")
    '                    If Nu(Rs!PARAMETRO_Detalle) = "3" Then TxtImporte = Format(Nz(pSubTotal), "0.00")
    '                    If Nu(Rs!PARAMETRO_Detalle) = "4" Then TxtImporte = Format(Nz(pIgv), "0.00")
    '                    If Nu(Rs!PARAMETRO_Detalle) = "5" Then TxtImporte = Format(Nz(pTotal), "0.00")
    '                End If
    '                If (pFinanza = "1" And Nu(Rs!PARAMETRO_Detalle) = "3") Then
    '                    psCuentaContable = pMCuenta
    '                Else
    '                    psCuentaContable = Nu(Rs!PARAMETRO_CUENTA)
    '                End If
    '                If Nu(Rs!PARAMETRO_Detalle) = "4" Or Nu(Rs!PARAMETRO_Detalle) = "5" Or Nu(Rs!PARAMETRO_Detalle) = "16" Or Nu(Rs!PARAMETRO_Detalle) = "10" Or Nu(Rs!PARAMETRO_Detalle) = "8" Then
    '                    CmdGlobal.CommandText = "SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & psCodEmpresa & psañog & ""
    '                    Rs1 = CmdGlobal.ExecuteReader
    '                    If Rs1.HasRows Then
    '                        While Rs.Read
    '                            codigo = Nz(Rs1(0)) + 1
    '                        End While
    '                    Else
    '                        codigo = 1
    '                    End If
    '                    Rs1.Close()
    '                    CmdGlobal.CommandText = " INSERT INTO TBCOMPROB_" & psCodEmpresa & psañog _
    '                                        & " (COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER, " _
    '                                        & " COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
    '                                        & " COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, " _
    '                                        & " COMPROB_DOC_CODIGO , COMPROB_NRO_DOC, COMPROB_RUC_PERSONA, " _
    '                                        & " COMPROB_GLOSA, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE," _
    '                                        & " COMPROB_OPCION,COMPROB_ESTADO) " _
    '                                        & " VALUES(" & NroPerAct & ",'" & codigo & "','" & pCodAsiento & "','" & (pPrefVoucher & pNroVoucher) & "'," _
    '                                        & " " & psCuentaContable & ",'" & pMoneda & "','" & pTipoVenta & "'," _
    '                                        & " '" & Format(pFecEmision, "yyyymmdd") & "'," & IIf(IsDBNull(pFecCanc) = True, "Null", "'" & Format(pFecPago, "yyyymmdd") & "'") & ",'" & Format(pFecRegistro, "yyyymmdd") & "', " _
    '                                        & " '" & Doc & "','" & Trim(NroDoc) & "','" & pCodPersona & "'," _
    '                                        & " '" & Trim(Nu(pGlosa)) & "','0','" & ValorSys & "'," & Format(CDbl(TxtImporte), "0.0#") & ", " _
    '                                        & " '" & IIf(Nu(Rs!PARAMETRO_DEBE_HABER) = "1", "D", "H") & "','1')"
    '                    CmdGlobal.ExecuteNonQuery()
    '                    If DocRef <> "" And psNroDocRef <> "" Then
    '                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                              & " COMPROB_DOC_REF='" & DocRef & "', COMPROB_NRO_DOC_REF='" & psNroDocRef & "' " _
    '                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                        CmdGlobal.ExecuteNonQuery()
    '                    End If
    '                    If psKardex = "SI" Then
    '                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " " _
    '                                              & " SET COMPROB_CV_KARDEX = '1' " _
    '                                              & " WHERE COMPROB_NUMERAR = " & codigo & ""
    '                        CmdGlobal.ExecuteNonQuery()
    '                    End If
    '                    If psTranfGrat <> "" Then
    '                        CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_TRANF_GRAT='" & psTranfGrat & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                        CmdGlobal.ExecuteNonQuery()
    '                    End If
    '                    If pMoneda = 1 Then
    '                        If Nu(Rs!PARAMETRO_DEBE_HABER) = "2" Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=" & IIf(pCodAsiento = 99, "0.00", TxtImporte) & ",COMPROB_IMPORTE_DEBE_D=NULL," _
    '                                                  & " COMPROB_IMPORTE_HABER_S=" & Format(CDbl(TxtImporte) * CDbl(pTipoVenta), "0.0#") & ",COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        ElseIf Nu(Rs!PARAMETRO_DEBE_HABER) = "1" Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(pCodAsiento = 99, "0.00", TxtImporte) & "'," _
    '                                                  & " COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & Format(CDbl(TxtImporte) * CDbl(pTipoVenta), "0.0#") & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                    ElseIf pMoneda = 2 Then
    '                        If Nu(Rs!PARAMETRO_DEBE_HABER) = "2" Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D='" & IIf(pCodAsiento = 99, "0.00", Format(CDbl(TxtImporte) / CDbl(pTipoVenta), "0.0#")) & "',COMPROB_IMPORTE_DEBE_D=NULL," _
    '                                                  & " COMPROB_IMPORTE_HABER_S='" & TxtImporte & "',COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        ElseIf Nu(Rs!PARAMETRO_DEBE_HABER) = "1" Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(pCodAsiento = 99, "0.00", Format(CDbl(TxtImporte) / CDbl(pTipoVenta), "0.0#")) & "'," _
    '                                                  & " COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & TxtImporte & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                    End If
    '                    If pFinanza = "1" Then psCentroCosto = pCCosto Else psCentroCosto = Nu(Rs!PARAMETRO_CCOSTO)
    '                    If psCentroCosto <> "" Then
    '                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_CENTRO_COSTO=" & psCentroCosto & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                        CmdGlobal.ExecuteNonQuery()
    '                    End If
    '                    If Nu(Rs!PARAMETRO_PARTIDAPRES) <> "" Then
    '                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_PART_PRESUPUESTARIA=" & Nu(Rs!PARAMETRO_PARTIDAPRES) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                        CmdGlobal.ExecuteNonQuery()
    '                    End If
    '                    If Nu(Rs!PARAMETRO_FLUJOCAJA) <> "" Then
    '                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_FLUJOCAJA=" & Nu(Rs!PARAMETRO_FLUJOCAJA) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                        CmdGlobal.ExecuteNonQuery()
    '                    End If
    '                    If psCtaInafecta <> "" Then
    '                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_AFECTO_INAFECTO='2' " _
    '                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                        CmdGlobal.ExecuteNonQuery()
    '                        If pMoneda = 1 Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                  & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                  & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                  & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        ElseIf pMoneda = 2 Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                  & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                  & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                  & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                    End If
    '                End If
    '                If Nu(Rs!PARAMETRO_Detalle) = "2" Or Nu(Rs!PARAMETRO_Detalle) = "3" Or Nu(Rs!PARAMETRO_Detalle) = "21" _
    '                Or Nu(Rs!PARAMETRO_Detalle) = "22" Or Nu(Rs!PARAMETRO_Detalle) = "23" _
    '                Or Nu(Rs!PARAMETRO_Detalle) = "24" Or Nu(Rs!PARAMETRO_Detalle) = "25" Or Nu(Rs!PARAMETRO_Detalle) = "26" Then
    '                    If (Nu(Rs!PARAMETRO_Detalle) = Detalle) Or (psServicio = "S" And Nu(Rs!PARAMETRO_Detalle) = "3") Or ((NC = "S" And pTipoParametro = "21")) Then
    '                        CmdGlobal.CommandText = " SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & psCodEmpresa & psañog & ""
    '                        Rs1 = CmdGlobal.ExecuteReader
    '                        If Rs1.HasRows Then
    '                            While Rs1.Read
    '                                codigo = Nz(Rs1(0)) + 1
    '                            End While
    '                        Else
    '                            codigo = 1
    '                        End If
    '                        Rs1.Close()
    '                        If (Nu(Rs!PARAMETRO_Detalle) = "2" Or Nu(Rs!PARAMETRO_Detalle) = "3") And psServicio = "S" Then psCuentaContable = psCtaServicio
    '                        CmdGlobal.CommandText = " INSERT INTO TBCOMPROB_" & psCodEmpresa & psañog _
    '                                            & " (COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER, " _
    '                                            & " COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
    '                                            & " COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, " _
    '                                            & " COMPROB_DOC_CODIGO , COMPROB_NRO_DOC, COMPROB_RUC_PERSONA, " _
    '                                            & " COMPROB_GLOSA, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE," _
    '                                            & " COMPROB_OPCION,COMPROB_ESTADO) " _
    '                                            & " VALUES(" & NroPerAct & ",'" & codigo & "','" & pCodAsiento & "','" & (pPrefVoucher & pNroVoucher) & "'," _
    '                                            & " " & psCuentaContable & ",'" & pMoneda & "','" & pTipoVenta & "'," _
    '                                            & " '" & Format(pFecEmision, "yyyymmdd") & "'," & IIf(IsDBNull(pFecCanc) = True, "Null", "'" & Format(pFecPago, "yyyymmdd") & "'") & ",'" & Format(pFecRegistro, "yyyymmdd") & "', " _
    '                                            & " '" & Doc & "','" & Trim(NroDoc) & "','" & pCodPersona & "'," _
    '                                            & " '" & Trim(Nu(pGlosa)) & "','0','" & ValorSys & "','" & Format(CDbl(TxtImporte), "0.0#") & "', " _
    '                                            & " '" & IIf(Nu(Rs!PARAMETRO_DEBE_HABER) = "1", "D", "H") & "','1')"
    '                        CmdGlobal.ExecuteNonQuery()
    '                        If DocRef <> "" And psNroDocRef <> "" Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                      & " COMPROB_DOC_REF='" & DocRef & "', COMPROB_NRO_DOC_REF='" & psNroDocRef & "' " _
    '                                                      & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                        If psTranfGrat <> "" Then
    '                            CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_TRANF_GRAT='" & psTranfGrat & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                        If pMoneda = 1 Then
    '                            If Nu(Rs!PARAMETRO_DEBE_HABER) = "2" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=" & IIf(pCodAsiento = 99, "0.00", TxtImporte) & ",COMPROB_IMPORTE_DEBE_D=NULL," _
    '                                    & "COMPROB_IMPORTE_HABER_S=" & Format(CDbl(TxtImporte) * CDbl(pTipoVenta), "0.0#") & ",COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            ElseIf Nu(Rs!PARAMETRO_DEBE_HABER) = "1" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(pCodAsiento = 99, "0.00", TxtImporte) & "'," _
    '                                    & "COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & Format(CDbl(TxtImporte) * CDbl(pTipoVenta), "0.0#") & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                        ElseIf pMoneda = 2 Then
    '                            If Nu(Rs!PARAMETRO_DEBE_HABER) = "2" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D='" & IIf(pCodAsiento = 99, "0.00", Format(CDbl(TxtImporte) / CDbl(pTipoVenta), "0.0#")) & "',COMPROB_IMPORTE_DEBE_D=NULL," _
    '                                    & "COMPROB_IMPORTE_HABER_S='" & TxtImporte & "',COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            ElseIf Nu(Rs!PARAMETRO_DEBE_HABER) = "1" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(pCodAsiento = 99, "0.00", Format(CDbl(TxtImporte) / CDbl(pTipoVenta), "0.0#")) & "'," _
    '                                    & "COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & TxtImporte & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                        End If
    '                        If pFinanza = "1" Then psCentroCosto = pCCosto Else psCentroCosto = Nu(Rs!PARAMETRO_CCOSTO)
    '                        If psCentroCosto <> "" Then
    '                            CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_CENTRO_COSTO=" & psCentroCosto & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                        If Nu(Rs!PARAMETRO_PARTIDAPRES) <> "" Then
    '                            CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_PART_PRESUPUESTARIA=" & Nu(Rs!PARAMETRO_PARTIDAPRES) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                        If Nu(Rs!PARAMETRO_FLUJOCAJA) <> "" Then
    '                            CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_FLUJOCAJA=" & Nu(Rs!PARAMETRO_FLUJOCAJA) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                        End If
    '                        PDeudora = "" : PAcreedora = ""
    '                        If psCtaInafecta <> "" Then
    '                            CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_AFECTO_INAFECTO='2' " _
    '                                                  & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                            CmdGlobal.ExecuteNonQuery()
    '                            If pMoneda = 1 Then
    '                                CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                      & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                      & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                      & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            ElseIf pMoneda = 2 Then
    '                                CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                      & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                      & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                      & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                        End If
    '                        If Nu(Rs!PARAMETRO_Detalle) = "3" And psCtaInafecta <> "" Then
    '                            CmdGlobal.CommandText = "SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & psCodEmpresa & psañog & ""
    '                            Rs1 = CmdGlobal.ExecuteReader
    '                            If Rs1.HasRows Then
    '                                While Rs.Read
    '                                    codigo = Nz(Rs1(0)) + 1
    '                                End While
    '                            Else
    '                                codigo = 1
    '                            End If
    '                            Rs1.Close()
    '                            CmdGlobal.CommandText = "INSERT INTO TBCOMPROB_" & psCodEmpresa & psañog _
    '                                & " (COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER, " _
    '                                & " COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
    '                                & " COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, " _
    '                                & " COMPROB_DOC_CODIGO , COMPROB_NRO_DOC, COMPROB_RUC_PERSONA, " _
    '                                & " COMPROB_GLOSA, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE," _
    '                                & " COMPROB_OPCION,COMPROB_ESTADO,COMPROB_AFECTO_INAFECTO ) " _
    '                                & " VALUES(" & NroPerAct & ",'" & codigo & "','" & pCodAsiento & "','" & (pPrefVoucher & pNroVoucher) & "'," _
    '                                & " " & psCtaInafecta & ",'" & pMoneda & "','" & pTipoVenta & "'," _
    '                                & " '" & Format(pFecEmision, "yyyymmdd") & "'," & IIf(IsDBNull(pFecCanc) = True, "Null", "'" & Format(pFecPago, "yyyymmdd") & "'") & ",'" & Format(pFecRegistro, "yyyymmdd") & "', " _
    '                                & " '" & Doc & "','" & Trim(NroDoc) & "','" & pCodPersona & "'," _
    '                                & " '" & Trim(Nu(pGlosa)) & "','0','" & ValorSys & "','" & Format(CDbl(psImporteIA), "0.0#") & "', " _
    '                                & " '" & psOpcionAI & "','1','2')"
    '                            CmdGlobal.ExecuteNonQuery()
    '                            If psTranfGrat <> "" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_TRANF_GRAT='" & psTranfGrat & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                            If pMoneda = 1 Then
    '                                CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                      & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                      & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                      & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            ElseIf pMoneda = 2 Then
    '                                CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                      & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                      & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                      & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                            If pMoneda = 1 Then
    '                                If psOpcionAI = "H" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=" & IIf(pCodAsiento = 99, "0.00", psImporteIA) & ",COMPROB_IMPORTE_DEBE_D=NULL," _
    '                                    & "COMPROB_IMPORTE_HABER_S=" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & ",COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                ElseIf psOpcionAI = "D" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(pCodAsiento = 99, "0.00", psImporteIA) & "'," _
    '                                    & "COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                            ElseIf pMoneda = 2 Then
    '                                If psOpcionAI = "H" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D='" & IIf(pCodAsiento = 99, "0.00", Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#")) & "',COMPROB_IMPORTE_DEBE_D=NULL," _
    '                                    & "COMPROB_IMPORTE_HABER_S='" & psImporteIA & "',COMPROB_IMPORTE_DEBE_S=NULL WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                ElseIf psOpcionAI = "D" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_IMPORTE_HABER_D=NULL,COMPROB_IMPORTE_DEBE_D='" & IIf(pCodAsiento = 99, "0.00", Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#")) & "'," _
    '                                    & "COMPROB_IMPORTE_HABER_S=NULL,COMPROB_IMPORTE_DEBE_S='" & psImporteIA & "' WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                            End If
    '                            If pFinanza = "1" Then psCentroCosto = pCCosto Else psCentroCosto = Nu(Rs!PARAMETRO_CCOSTO)
    '                            If psCentroCosto <> "" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_CENTRO_COSTO=" & psCentroCosto & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                            If Nu(Rs!PARAMETRO_PARTIDAPRES) <> "" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_PART_PRESUPUESTARIA=" & Nu(Rs!PARAMETRO_PARTIDAPRES) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                            If Nu(Rs!PARAMETRO_FLUJOCAJA) <> "" Then
    '                                CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_FLUJOCAJA=" & Nu(Rs!PARAMETRO_FLUJOCAJA) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                            End If
    '                            If psCtaInafecta <> "" Then
    '                                CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_AFECTO_INAFECTO='2' " _
    '                                                      & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                CmdGlobal.ExecuteNonQuery()
    '                                If pMoneda = 1 Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                          & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                ElseIf pMoneda = 2 Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                          & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                            End If
    '                            PDeudora = ""
    '                            PAcreedora = ""
    '                            CmdGlobal.CommandText = " SELECT PLAN_CUENTA_DEUDORA, PLAN_CUENTA_ACREEDORA From dbo.TBPCGR_" & psCodEmpresa & "  " _
    '                                & " WHERE (PLAN_AÑO = '" & psañog & "') AND (PLAN_CODIGO = '" & psCtaInafecta & "') AND (PLAN_SYS_EST = '0')"
    '                            RsG = CmdGlobal.ExecuteReader
    '                            If RsG.HasRows Then
    '                                While RsG.Read
    '                                    PDeudora = Nu(RsG!PLAN_CUENTA_DEUDORA)
    '                                    PAcreedora = Nu(RsG!PLAN_CUENTA_ACREEDORA)
    '                                End While
    '                            End If
    '                            RsG.Close()
    '                            'CUENTA DEUDORA
    '                            If PDeudora <> "" Then
    '                                CmdGlobal.CommandText = "SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & SistCodEmpresa & psañog & ""
    '                                Rs1 = CmdGlobal.ExecuteReader
    '                                If Rs1.HasRows Then
    '                                    While Rs.Read
    '                                        codigo = Nz(Rs1(0)) + 1
    '                                    End While
    '                                Else
    '                                    codigo = 1
    '                                End If
    '                                Rs1.Close()
    '                                CmdGlobal.CommandText = "INSERT INTO TBCOMPROB_" & psCodEmpresa & psañog _
    '                                    & " (COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER, " _
    '                                    & " COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
    '                                    & " COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, " _
    '                                    & " COMPROB_DOC_CODIGO , COMPROB_NRO_DOC, COMPROB_RUC_PERSONA, " _
    '                                    & " COMPROB_GLOSA, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE," _
    '                                    & " COMPROB_OPCION,COMPROB_ESTADO,COMPROB_RELAC_COMPROB) " _
    '                                    & " VALUES(" & NroPerAct & ",'" & codigo & "','" & pCodAsiento & "','" & (pPrefVoucher & pNroVoucher) & "'," _
    '                                    & " " & PDeudora & ",'" & pMoneda & "','" & pTipoVenta & "'," _
    '                                    & " '" & Format(pFecEmision, "yyyymmdd") & "'," & IIf(IsDBNull(pFecCanc) = True, "Null", "'" & Format(pFecPago, "yyyymmdd") & "'") & ",'" & Format(pFecRegistro, "yyyymmdd") & "', " _
    '                                    & " '" & Doc & "','" & Trim(NroDoc) & "','" & pCodPersona & "'," _
    '                                    & " '" & Trim(Nu(pGlosa)) & "','0','" & ValorSys & "','" & Format(CDbl(psImporteIA), "0.0#") & "', " _
    '                                    & " 'D','1'," & codigo & ")"
    '                                CmdGlobal.ExecuteNonQuery()
    '                                If DocRef <> "" And psNroDocRef <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_DOC_REF='" & DocRef & "', COMPROB_NRO_DOC_REF='" & psNroDocRef & "' " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pMoneda = 1 Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_IMPORTE_HABER_D=NULL, COMPROB_IMPORTE_DEBE_D=" & Format(CDbl(psImporteIA), "0.0#") & " , " _
    '                                                          & " COMPROB_IMPORTE_HABER_S=NULL, COMPROB_IMPORTE_DEBE_S=" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & " " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                Else
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_IMPORTE_HABER_D=NULL, COMPROB_IMPORTE_DEBE_D=" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & " , " _
    '                                                          & " COMPROB_IMPORTE_HABER_S=NULL, COMPROB_IMPORTE_DEBE_S=" & Format(CDbl(psImporteIA), "0.0#") & " " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pFinanza = "1" Then psCentroCosto = pCCosto Else psCentroCosto = Nu(Rs!PARAMETRO_CCOSTO)
    '                                If psCentroCosto <> "" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_CENTRO_COSTO=" & psCentroCosto & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_PARTIDAPRES) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_PART_PRESUPUESTARIA=" & Nu(Rs!PARAMETRO_PARTIDAPRES) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_FLUJOCAJA) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_FLUJOCAJA=" & Nu(Rs!PARAMETRO_FLUJOCAJA) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If psCtaInafecta <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_AFECTO_INAFECTO='2' " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                    If pMoneda = 1 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                              & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    ElseIf pMoneda = 2 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                              & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    End If
    '                                End If
    '                            End If
    '                            'CUENTA ACREEDORA
    '                            If PAcreedora <> "" Then
    '                                CmdGlobal.CommandText = "SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & SistCodEmpresa & psañog & ""
    '                                Rs1 = CmdGlobal.ExecuteReader
    '                                If Rs1.HasRows Then
    '                                    While Rs1.Read
    '                                        codigo = Nz(Rs1(0)) + 1
    '                                    End While
    '                                Else
    '                                    codigo = 1
    '                                End If
    '                                Rs1.Close()
    '                                CmdGlobal.CommandText = "INSERT INTO TBCOMPROB_" & psCodEmpresa & psañog _
    '                                    & " (COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER, " _
    '                                    & " COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
    '                                    & " COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, " _
    '                                    & " COMPROB_DOC_CODIGO , COMPROB_NRO_DOC, COMPROB_RUC_PERSONA, " _
    '                                    & " COMPROB_GLOSA, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE," _
    '                                    & " COMPROB_OPCION,COMPROB_ESTADO,COMPROB_RELAC_COMPROB) " _
    '                                    & " VALUES(" & NroPerAct & ",'" & codigo & "','" & pCodAsiento & "','" & (pPrefVoucher & pNroVoucher) & "'," _
    '                                    & " " & PAcreedora & ",'" & pMoneda & "','" & pTipoVenta & "'," _
    '                                    & " '" & Format(pFecEmision, "yyyymmdd") & "'," & IIf(IsDBNull(pFecCanc) = True, "Null", "'" & Format(pFecPago, "yyyymmdd") & "'") & ",'" & Format(pFecRegistro, "yyyymmdd") & "', " _
    '                                    & " '" & Doc & "','" & Trim(NroDoc) & "','" & pCodPersona & "'," _
    '                                    & " '" & Trim(Nu(pGlosa)) & "','0','" & ValorSys & "','" & Format(CDbl(psImporteIA), "0.0#") & "', " _
    '                                    & " 'D','1'," & codigo & ")"
    '                                CmdGlobal.ExecuteNonQuery()
    '                                If DocRef <> "" And psNroDocRef <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_DOC_REF='" & DocRef & "', COMPROB_NRO_DOC_REF='" & psNroDocRef & "' " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pMoneda = 1 Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_IMPORTE_DEBE_D=NULL, COMPROB_IMPORTE_HABER_D=" & Format(CDbl(psImporteIA), "0.0#") & " , " _
    '                                                          & " COMPROB_IMPORTE_DEBE_S=NULL, COMPROB_IMPORTE_HABER_S=" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & " " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                Else
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_IMPORTE_DEBE_D=NULL, COMPROB_IMPORTE_HABER_D=" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & " , " _
    '                                                          & " COMPROB_IMPORTE_DEBE_S=NULL, COMPROB_IMPORTE_HABER_S=" & Format(CDbl(psImporteIA), "0.0#") & " " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pFinanza = "1" Then psCentroCosto = pCCosto Else psCentroCosto = Nu(Rs!PARAMETRO_CCOSTO)
    '                                If psCentroCosto <> "" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_CENTRO_COSTO=" & psCentroCosto & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_PARTIDAPRES) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_PART_PRESUPUESTARIA=" & Nu(Rs!PARAMETRO_PARTIDAPRES) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_FLUJOCAJA) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                          & " COMPROB_FLUJOCAJA=" & Nu(Rs!PARAMETRO_FLUJOCAJA) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If psCtaInafecta <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_AFECTO_INAFECTO='2' " _
    '                                                          & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                    If pMoneda = 1 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                              & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    ElseIf pMoneda = 2 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                              & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    End If
    '                                End If
    '                            End If
    '                        End If
    '                        If Nu(Rs!PARAMETRO_Detalle) = "2" Or Nu(Rs!PARAMETRO_Detalle) = "3" Then
    '                            CmdGlobal.CommandText = " SELECT PLAN_CUENTA_DEUDORA, PLAN_CUENTA_ACREEDORA From dbo.TBPCGR_" & psCodEmpresa
    '                                    & " WHERE (PLAN_AÑO = '" & psañog & "') AND (PLAN_CODIGO = '" & psCuentaContable & "') AND (PLAN_SYS_EST = '0')"
    '                            RsG = CmdGlobal.ExecuteReader
    '                            If RsG.HasRows Then
    '                                While RsG.Read
    '                                    PDeudora = Nu(RsG!PLAN_CUENTA_DEUDORA)
    '                                    PAcreedora = Nu(RsG!PLAN_CUENTA_ACREEDORA)
    '                                End While
    '                            End If
    '                            RsG.Close()
    '                            'CUENTA DEUDORA
    '                            If PDeudora <> "" Then
    '                                CmdGlobal.CommandText = "SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & psCodEmpresa & psañog & ""
    '                                Rs1 = CmdGlobal.ExecuteReader
    '                                If Rs1.HasRows Then
    '                                    While Rs1.Read
    '                                        codigo = Nz(Rs1(0)) + 1
    '                                    End While
    '                                Else
    '                                    codigo = 1
    '                                End If
    '                                Rs1.Close()
    '                                CmdGlobal.CommandText = "INSERT INTO TBCOMPROB_" & psCodEmpresa & psañog _
    '                                        & " (COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER, " _
    '                                        & " COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
    '                                        & " COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, " _
    '                                        & " COMPROB_DOC_CODIGO , COMPROB_NRO_DOC, COMPROB_RUC_PERSONA, " _
    '                                        & " COMPROB_GLOSA, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE," _
    '                                        & " COMPROB_OPCION,COMPROB_ESTADO,COMPROB_RELAC_COMPROB) " _
    '                                        & " VALUES(" & NroPerAct & ",'" & codigo & "','" & pCodAsiento & "','" & (pPrefVoucher & pNroVoucher) & "'," _
    '                                        & " " & PDeudora & ",'" & pMoneda & "','" & pTipoVenta & "'," _
    '                                        & " '" & Format(pFecEmision, "yyyymmdd") & "'," & IIf(IsDBNull(pFecCanc) = True, "Null", "'" & Format(pFecPago, "yyyymmdd") & "'") & ",'" & Format(pFecRegistro, "yyyymmdd") & "', " _
    '                                        & " '" & Doc & "','" & Trim(NroDoc) & "','" & pCodPersona & "'," _
    '                                        & " '" & Trim(Nu(pGlosa)) & "','0','" & ValorSys & "','" & Format(CDbl(TxtImporte), "0.0#") & "', " _
    '                                        & " 'D','1'," & codigo & ")"
    '                                CmdGlobal.ExecuteNonQuery()
    '                                If DocRef <> "" And psNroDocRef <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_DOC_REF='" & DocRef & "', COMPROB_NRO_DOC_REF='" & psNroDocRef & "' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pMoneda = 1 Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_HABER_D=NULL, COMPROB_IMPORTE_DEBE_D=" & Format(CDbl(TxtImporte), "0.0#") & " , " _
    '                                                              & " COMPROB_IMPORTE_HABER_S=NULL, COMPROB_IMPORTE_DEBE_S=" & Format(CDbl(TxtImporte) * CDbl(pTipoVenta), "0.0#") & " " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                Else
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_HABER_D=NULL, COMPROB_IMPORTE_DEBE_D=" & Format(CDbl(TxtImporte) / CDbl(pTipoVenta), "0.0#") & " , " _
    '                                                              & " COMPROB_IMPORTE_HABER_S=NULL, COMPROB_IMPORTE_DEBE_S=" & Format(CDbl(TxtImporte), "0.0#") & " " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pFinanza = "1" Then psCentroCosto = pCCosto Else psCentroCosto = Nu(Rs!PARAMETRO_CCOSTO)
    '                                If psCentroCosto <> "" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_CENTRO_COSTO=" & psCentroCosto & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_PARTIDAPRES) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_PART_PRESUPUESTARIA=" & Nu(Rs!PARAMETRO_PARTIDAPRES) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_FLUJOCAJA) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_FLUJOCAJA=" & Nu(Rs!PARAMETRO_FLUJOCAJA) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If psCtaInafecta <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_AFECTO_INAFECTO='2' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                    If pMoneda = 1 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                                  & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                                  & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                                  & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    ElseIf pMoneda = 2 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                                  & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                                  & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                                  & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    End If
    '                                End If
    '                            End If
    '                                'CUENTA ACREEDORA
    '                            If PAcreedora <> "" Then
    '                                CmdGlobal.CommandText = "SELECT MAX(COMPROB_NUMERAR) FROM TBCOMPROB_" & psCodEmpresa & psañog & ""
    '                                Rs1 = CmdGlobal.ExecuteReader
    '                                If Rs1.HasRows Then
    '                                    While Rs1.Read
    '                                        codigo = Nz(Rs1(0)) + 1
    '                                    End While
    '                                Else
    '                                    codigo = 1
    '                                End If
    '                                Rs1.Close()
    '                                CmdGlobal.CommandText = "INSERT INTO TBCOMPROB_" & psCodEmpresa & psañog _
    '                                        & " (COMPROB_PERIODO, COMPROB_NUMERAR,COMPROB_ASIENTO_CODIGO, COMPROB_NRO_VOUCHER, " _
    '                                        & " COMPROB_PLAN_CODIGO, COMPROB_MONEDA,COMPROB_TIPOCAM, " _
    '                                        & " COMPROB_FEC_DOC, COMPROB_FEC_VCTO,  COMPROB_FEC_REGISTRO, " _
    '                                        & " COMPROB_DOC_CODIGO , COMPROB_NRO_DOC, COMPROB_RUC_PERSONA, " _
    '                                        & " COMPROB_GLOSA, COMPROB_SYS_EST, COMPROB_SYS_CRE,COMPROB_IMPORTE," _
    '                                        & " COMPROB_OPCION,COMPROB_ESTADO,COMPROB_RELAC_COMPROB) " _
    '                                        & " VALUES(" & NroPerAct & ",'" & codigo & "','" & pCodAsiento & "','" & (pPrefVoucher & pNroVoucher) & "'," _
    '                                        & " " & PAcreedora & ",'" & pMoneda & "','" & pTipoVenta & "'," _
    '                                        & " '" & Format(pFecEmision, "yyyymmdd") & "'," & IIf(IsDBNull(pFecCanc) = True, "Null", "'" & Format(pFecPago, "yyyymmdd") & "'") & ",'" & Format(pFecRegistro, "yyyymmdd") & "', " _
    '                                        & " '" & Doc & "','" & Trim(NroDoc) & "','" & pCodPersona & "'," _
    '                                        & " '" & Trim(Nu(pGlosa)) & "','0','" & ValorSys & "','" & Format(CDbl(TxtImporte), "0.0#") & "', " _
    '                                        & " 'D','1'," & codigo & ")"
    '                                CmdGlobal.ExecuteNonQuery()
    '                                If DocRef <> "" And psNroDocRef <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_DOC_REF='" & DocRef & "', COMPROB_NRO_DOC_REF='" & psNroDocRef & "' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pMoneda = 1 Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_DEBE_D=NULL, COMPROB_IMPORTE_HABER_D=" & Format(CDbl(TxtImporte), "0.0#") & " , " _
    '                                                              & " COMPROB_IMPORTE_DEBE_S=NULL, COMPROB_IMPORTE_HABER_S=" & Format(CDbl(TxtImporte) * CDbl(pTipoVenta), "0.0#") & " " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                Else
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_IMPORTE_DEBE_D=NULL, COMPROB_IMPORTE_HABER_D=" & Format(CDbl(TxtImporte) / CDbl(pTipoVenta), "0.0#") & " , " _
    '                                                              & " COMPROB_IMPORTE_DEBE_S=NULL, COMPROB_IMPORTE_HABER_S=" & Format(CDbl(TxtImporte), "0.0#") & " " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If pFinanza = "1" Then psCentroCosto = pCCosto Else psCentroCosto = Nu(Rs!PARAMETRO_CCOSTO)
    '                                If psCentroCosto <> "" Then
    '                                    CmdGlobal.CommandText = "UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_CENTRO_COSTO=" & psCentroCosto & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_PARTIDAPRES) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_PART_PRESUPUESTARIA=" & Nu(Rs!PARAMETRO_PARTIDAPRES) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If Nu(Rs!PARAMETRO_FLUJOCAJA) <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                              & " COMPROB_FLUJOCAJA=" & Nu(Rs!PARAMETRO_FLUJOCAJA) & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                End If
    '                                If psCtaInafecta <> "" Then
    '                                    CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET COMPROB_AFECTO_INAFECTO='2' " _
    '                                                              & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                    CmdGlobal.ExecuteNonQuery()
    '                                    If pMoneda = 1 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                                  & " COMPROB_IMPORTE_IA_D=" & Format(psImporteIA, "0.0#") & "," _
    '                                                                  & " COMPROB_IMPORTE_IA_S='" & Format(CDbl(psImporteIA) * CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                                  & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    ElseIf pMoneda = 2 Then
    '                                        CmdGlobal.CommandText = " UPDATE TBCOMPROB_" & psCodEmpresa & psañog & " SET " _
    '                                                                  & " COMPROB_IMPORTE_IA_S=" & Format(psImporteIA, "0.0#") & "," _
    '                                                                  & " COMPROB_IMPORTE_IA_D='" & Format(CDbl(psImporteIA) / CDbl(pTipoVenta), "0.0#") & "' " _
    '                                                                  & " WHERE COMPROB_NUMERAR='" & codigo & "'"
    '                                        CmdGlobal.ExecuteNonQuery()
    '                                    End If
    '                                End If
    '                            End If
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '        End While
    '    End If
    '    Rs.Close()
    '    Dim psNCod As String : psNCod = ""
    '    If psKardex <> "SI" Then
    '        MsgBox("Comprobante Nro " & (pPrefVoucher & pNroVoucher) & " GENERADO", vbExclamation)
    '    End If
    '    If pFinanza = "1" Then
    '        Call IniEmpresa()
    '        CmdGlobal.CommandText = " UPDATE TBFINANZA_REGISTRO_" & psCodEmpresa & psañog & " SET FINANZA_VOUCHER = '" & (pPrefVoucher & pNroVoucher) & "' " _
    '                              & " WHERE FINANZA_CODIGO = " & pdCodFinanza & ""
    '        CmdGlobal.Execute()
    '        Call FinEmpresa()
    '    End If
    '    If pTipoParametro = "13" Or pTipoParametro = "16" Then
    '        Call IniEmpresa()
    '        Sql = "SELECT MAX(NC_CODIGO) FROM TBVENTAS_NOTACREDITO"
    '        Rs.Open(Sql, Cn, adOpenKeyset, adLockOptimistic)
    '        If Rs.RecordCount > 0 Then psNCod = Nz(Rs(0)) + 1 Else psNCod = 1
    '        Rs.Close()
    '        CmdGlobal.CommandText = " INSERT INTO TBVENTAS_NOTACREDITO (EMPRESA_CODIGO,NC_CODIGO,RE_CODIGO, NC_NRO, NRO_VOUCHER,NC_MOTIVO,NC_MONTO,NC_SYS_EST,NC_FECHA,NC_IGV,NC_TOTAL,NC_MONEDA,NC_TIPOCAMBIO,NC_SYS_CRE,NOTA_TIPO,NRO_FACTURA,NC_AÑO,NC_FINANZA)" _
    '                              & " VALUES ('" & psCodEmpresa & "'," & psNCod & "," & CodRecibo & ",'" & NroDoc & "','" & pPrefVoucher & pNroVoucher & "','" & psMotivoNC & "'," & CDbl(pSubTotal) & ",'0','" & FechaServer & "'," & CDbl(pIgv) & "," & CDbl(pTotal) & ",'" & pMoneda & "'," & pTipoVenta & ",'" & ValorSys & "','" & psNotaCD & "','" & psNroDocRef & "','" & psañog & "','N')"
    '        CmdGlobal.Execute()
    '        Call FinEmpresa()
    '        Ventas_CotizacionCaja.lblCodNotaCD = psNCod
    '    End If
    '    If pTipoParametro = "25" Or pTipoParametro = "26" Then
    '        Call IniEmpresa()
    '        Sql = "SELECT MAX(NC_CODIGO) FROM TBLOGIS_ORDENES_COMPRA_NOTACREDITO"
    '        Rs.Open(Sql, Cn, adOpenKeyset, adLockOptimistic)
    '        If Rs.RecordCount > 0 Then psNCod = Nz(Rs(0)) + 1 Else psNCod = 1
    '        Rs.Close()
    '        CmdGlobal.CommandText = " INSERT INTO TBLOGIS_ORDENES_COMPRA_NOTACREDITO (EMPRESA_CODIGO,NC_CODIGO,OCOMPRA_CODIGO, NC_NRO, NRO_VOUCHER,NC_MOTIVO,NC_MONTO,NC_SYS_EST,NC_FECHA,NC_IGV,NC_TOTAL,NC_MONEDA,NC_TIPOCAMBIO,NC_SYS_CRE,NOTA_TIPO,NRO_FACTURA,NC_AÑO,NC_FINANZA)" _
    '                              & " VALUES ('" & psCodEmpresa & "'," & psNCod & "," & CodRecibo & ",'" & NroDoc & "','" & pPrefVoucher & pNroVoucher & "','" & psMotivoNC & "'," & CDbl(pSubTotal) & ",'0','" & FechaServer & "'," & CDbl(pIgv) & "," & CDbl(pTotal) & ",'" & pMoneda & "'," & pTipoVenta & ",'" & ValorSys & "','" & psNotaCD & "','" & psNroDocRef & "','" & psañog & "','" & pFinanza & "')"
    '        CmdGlobal.Execute()
    '        Call FinEmpresa()
    '        Ventas_ReciboSeguimiento.lblCodNotaCD = psNCod
    '    End If
    '    Dim mDs As Double, mDd As Double
    '    Call FinEmpresa()
    '    Call Corregir_Nro_Doc(psañog)
    '    Call IniEmpresa()
    '    If pTipoParametro = "3" And CodMov <> "" Then
    '        CmdGlobal.CommandText = " UPDATE TBINV_ALMACEN_RECEPCION SET RECEP_NRO_VOUCHER = '" & pPrefVoucher & pNroVoucher & "'  " _
    '                              & " WHERE RECEP_CODIGO = " & CodMov & ""
    '        CmdGlobal.Execute()
    '    End If
    'End Sub
End Class
